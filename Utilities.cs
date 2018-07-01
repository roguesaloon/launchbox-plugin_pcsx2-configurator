using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using IniParser;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    internal static class Utilities
    {
        public static string PluginDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string LaunchboxDirectory => Application.StartupPath;

        public static string SettingsFile => PluginDirectory + "\\Settings.ini";

        public static bool IsLaunchBoxNext() => Process.GetCurrentProcess().ProcessName.ToLowerInvariant().Contains("next");

        public static string ConfigsDir
        {
            get
            {
                var iniParser = new FileIniDataParser();
                var pluginSettings = iniParser.ReadFile(SettingsFile)["PCSX2_Configurator"];
                var configsDirectoryPath = pluginSettings["ConfigsDirectoryPath"];
                configsDirectoryPath = string.IsNullOrEmpty(configsDirectoryPath)
                    ? Path.GetDirectoryName(FullEmulatorPath) + "\\inis"
                    : configsDirectoryPath;
                return configsDirectoryPath;
            }
        }

        public static string FullEmulatorPath
        {
            get
            {
                foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
                {
                    if (!emulator.Title.Contains("PCSX2")) continue;
                    var appPath = emulator.ApplicationPath;
                    appPath = (!Path.IsPathRooted(appPath)) ? LaunchboxDirectory + "\\" + appPath : appPath;

                    return File.Exists(appPath) ? appPath : null;
                }

                return null;
            }
        }

        public static Icon EmulatorIcon => (FullEmulatorPath != null) ? Icon.ExtractAssociatedIcon(FullEmulatorPath) : null;

        public static IEmulator GetAssociatedEmulator(IGame game)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(game.EmulatorId);
            return emulator;
        }

        public static string GetSafeTitle(IGame game)
        {
            var launchBoxDbFile = new XmlDocument();
            launchBoxDbFile.Load(LaunchboxDirectory + "\\Metadata\\Metadata.xml");

            var gameDbEntry = launchBoxDbFile.SelectSingleNode("//Game[DatabaseID=\"" + game.LaunchBoxDbId + "\"]");
            var gameName = gameDbEntry?.SelectSingleNode("descendant::Name")?.InnerText ?? game.Title;
            var safeName = Path.GetInvalidFileNameChars().Aggregate(gameName, (current, c) => current.Replace(c.ToString(), ""));

            return safeName;
        }

        public static string GetConfigDir(IGame game)
        {
            var configDir = ConfigsDir + "\\" + GetSafeTitle(game);
            return configDir;
        }

        public static string GetRemoteUrl(IGame game, bool svnStyle = false)
        {
            var gameTitle = GetSafeTitle(game);
            gameTitle = gameTitle.Replace(" ", "%20");
            gameTitle = gameTitle.Replace("&", "and");
            gameTitle = gameTitle.ToLower();

            var url = "http://github.com/roguesaloon/launchbox-plugin_pcsx2-configurator" + (svnStyle ? "/trunk/" : "/tree/master/") + "Game%20Configs/";
            url = url + gameTitle;

            return url;
        }

        public static IGame GetSelectedGame()
        {
            var gamesControlPropertyInfo = PluginHelper.LaunchBoxMainForm.GetType().GetProperty("GamesControl");
            var gamesControl = gamesControlPropertyInfo?.GetValue(PluginHelper.LaunchBoxMainForm);

            var selectedGamePropertyInfo = gamesControl?.GetType().GetProperty("ActiveGame");
            var selectedGame = selectedGamePropertyInfo?.GetValue(gamesControl) as IGame;

            return selectedGame;
        }

        public static bool IsGameValid(IGame game)
        {
            var emulator = GetAssociatedEmulator(game);
            return emulator != null && (emulator.Title.Contains("PCSX2") || IsUsingRocketLauncher(game)) && game.Platform == "Sony Playstation 2";
        }

        public static bool IsUsingRocketLauncher(IGame game)
        {
            var emulator = GetAssociatedEmulator(game);
            return emulator != null && (emulator.Title.Contains("Rocket Launcher") || emulator.Title.Contains("RocketLauncher"));
        }

        public static void HideContextMenuItem(string itemText, bool hide)
        {
            // Gets the context menu from Launchbox Main Form
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField?.GetValue(PluginHelper.LaunchBoxMainForm);

            if (contextMenuStrip == null) return;

            // Hides or shows the menu item with given caption
            for (var i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Text != itemText) continue;
                contextMenuStrip.Items[i].Visible = !hide;
                break;
            }
        }

        public static void HidePluginSeparatorIfApplicable(bool hide)
        {
            // Count Items After Final Seperator That are visible (not hidden)
            // If There are none hide final (plugin) seperator

            // Gets the context menu from Launchbox Main Form
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField?.GetValue(PluginHelper.LaunchBoxMainForm);

            if (contextMenuStrip == null) return;

            var indexOfPluginSeparator = IndexOfPluginSeparator();

            if (!hide)
            {
                contextMenuStrip.Items[(int) indexOfPluginSeparator].Visible = true;
                return;
            }

            var numberOfPluginsAfterSeparator = (contextMenuStrip.Items.Count - 1) - (int) indexOfPluginSeparator;
            var numberOfHiddenPluginsAfterSeparator = HiddenItemsAfterPluginSeparator().Count();

            if (numberOfHiddenPluginsAfterSeparator == numberOfPluginsAfterSeparator && GetSelectedGame() != null)
            {
                contextMenuStrip.Items[(int) indexOfPluginSeparator].Visible = false;
                return;
            }

            if (GetSelectedGame() == null)
            {
                contextMenuStrip.Items[(int)indexOfPluginSeparator].Visible = numberOfPluginsAfterSeparator > 1;
                return;
            }

            contextMenuStrip.Items[(int) indexOfPluginSeparator].Visible = true;

            IEnumerable<ToolStripItem> HiddenItemsAfterPluginSeparator()
            {
                var hiddenItems = new List<ToolStripItem>();

                for (var i = (int) indexOfPluginSeparator + 1; i < contextMenuStrip.Items.Count; ++i)
                {
                    if (!contextMenuStrip.Items[i].Visible)
                    {
                        hiddenItems.Add(contextMenuStrip.Items[i]);
                    }
                }

                return hiddenItems;
            }
            
            int? IndexOfPluginSeparator()
            {
                for (var i = contextMenuStrip.Items.Count - 1; i > 0; --i)
                {
                    if (contextMenuStrip.Items[i].GetType() == typeof(ToolStripSeparator))
                    {
                        return i;
                    }
                }

                return null;
            }
        }

        public static string SetPrivateField(IGame game, string key, string value)
        {
            var file = new XmlDocument();
            var filePath = PluginDirectory + "\\GameOptions.xml";
            file.Load(filePath);

            var rootNode = file.SelectSingleNode("//GameOptions");

            var existing =
                rootNode?.SelectSingleNode("//PrivateField[GameID=\"" + game.Id + "\"][Key=\"" + key + "\"]");

            XmlNode fieldNode;

            if (existing != null)
            {
                fieldNode = existing;
                existing.RemoveAll();
            }
            else
            {
                fieldNode = file.CreateElement("PrivateField");
                rootNode?.AppendChild(fieldNode);
            }

            var gameIdNode = file.CreateElement("GameID");
            gameIdNode.InnerText = game.Id;
            fieldNode.AppendChild(gameIdNode);

            var keyNode = file.CreateElement("Key");
            keyNode.InnerText = key;
            fieldNode.AppendChild(keyNode);

            var valueNode = file.CreateElement("Value");
            valueNode.InnerText = value;
            fieldNode.AppendChild(valueNode);

            file.Save(filePath);

            return value;
        }

        public static string GetPrivateField(IGame game, string key)
        {
            var file = new XmlDocument();
            var filePath = PluginDirectory + "\\GameOptions.xml";
            file.Load(filePath);

            var field = file.SelectSingleNode("//GameOptions/PrivateField[GameID=\"" + game.Id + "\"][Key=\"" + key + "\"]");
            var value = field?.SelectSingleNode("descendant::Value")?.InnerText;

            return value;
        }

        public static ToolStripMenuItem AddContextMenuItemToSubMenu(string itemText, string subMenuItem)
        {
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField?.GetValue(PluginHelper.LaunchBoxMainForm);

            for (var i = 0; i < contextMenuStrip?.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Text == itemText)
                {
                    return (ToolStripMenuItem)((ToolStripMenuItem)contextMenuStrip.Items[i]).DropDownItems.Add(subMenuItem);
                }
            }

            return null;
        }

        public static ToolStripMenuItem GetContextMenuItem(string itemName)
        {
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField?.GetValue(PluginHelper.LaunchBoxMainForm);

            for (var i = 0; i < contextMenuStrip?.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Name == itemName)
                {
                    return (ToolStripMenuItem)contextMenuStrip.Items[i];
                }
            }

            return null;
        }

        public static void SetPrePlayEvent(Action eventAction)
        {
            var gamesControlPropertyInfo = PluginHelper.LaunchBoxMainForm.GetType().GetProperty("GamesControl");
            var gamesControl = gamesControlPropertyInfo?.GetValue(PluginHelper.LaunchBoxMainForm);

            var playEventInfo = PluginHelper.LaunchBoxMainForm.GetType().GetMethod("playButton_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            var playEvent = playEventInfo?.CreateDelegate(typeof(EventHandler), PluginHelper.LaunchBoxMainForm) as EventHandler;

            void EventHandler(object sender, EventArgs args) => eventAction.Invoke();

            var doubleClickEventInfo = gamesControl?.GetType().GetEvent("GameDoubleClick");

            doubleClickEventInfo?.RemoveEventHandler(gamesControl, playEvent);
            doubleClickEventInfo?.AddEventHandler(gamesControl, (EventHandler) EventHandler);
            doubleClickEventInfo?.AddEventHandler(gamesControl, playEvent);

            var playContextMenuInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("playContextToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (playContextMenuInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem playContextMenuItem)
            {
                playContextMenuItem.Click -= playEvent;
                playContextMenuItem.Click += EventHandler;
                playContextMenuItem.Click += playEvent;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            var playMenuInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("playToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (playMenuInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem playMenuItem)
            {
                playMenuItem.Click -= playEvent;
                playMenuItem.Click += EventHandler;
                playMenuItem.Click += playEvent;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            var playButtonInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("playButton", BindingFlags.NonPublic | BindingFlags.Instance);

            if (playButtonInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is Button playButton)
            {
                playButton.Click -= playEvent;
                playButton.Click += EventHandler;
                playButton.Click += playEvent;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }
        }

        public static void SetConfigureEvent(Action eventAction)
        {
            void EventHandler(object sender, EventArgs args) => eventAction.Invoke();

            var configureContextMenuInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("configureContextToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (configureContextMenuInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem configureContextMenuItem)
            {
                configureContextMenuItem.Click += EventHandler;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            var configureMenuInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("configureToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (configureMenuInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem configureMenuItem)
            {
                configureMenuItem.Click += EventHandler;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            var configureButtonInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("configureButton", BindingFlags.NonPublic | BindingFlags.Instance);

            if (configureButtonInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is Button configureButton)
            {
                configureButton.Click += EventHandler;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }
        }

        public static void SetSelectionChangeEvent(Action eventAction)
        {
            void EventHandler(object sender, EventArgs args) => eventAction.Invoke();

            var gamesControlPropertyInfo = PluginHelper.LaunchBoxMainForm.GetType().GetProperty("GamesControl");
            var gamesControl = gamesControlPropertyInfo?.GetValue(PluginHelper.LaunchBoxMainForm);

            var selectedGameChangedEventInfo = gamesControl?.GetType().GetEvent("SelectedGameChanged");
            selectedGameChangedEventInfo?.AddEventHandler(gamesControl, (EventHandler) EventHandler);
        }

        public static void SetMenuItemEvent(ToolStripMenuItem menuItem, Action eventAction)
        {
            void EventHandler(object sender, EventArgs args) => eventAction.Invoke();

            menuItem.Click += EventHandler;
        }

        public static void SetChangeViewEvent(Action eventAction)
        {
            void EventHandler(object sender, EventArgs args) => eventAction.Invoke();

            //var gamesControlPropertyInfo = PluginHelper.LaunchBoxMainForm.GetType().GetProperty("GamesControl");
            //var gamesControl = gamesControlPropertyInfo?.GetValue(PluginHelper.LaunchBoxMainForm);

            //var controlType = gamesControl?.GetType().ToString();

            var imagesViewInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("boxArtViewToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var listViewInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("listViewToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (imagesViewInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem imagesView && 
                listViewInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem listView)
            {
                imagesView.Click -= EventHandler;
                listView.Click -= EventHandler;

                if (imagesView.Checked)
                {
                    listView.Click += EventHandler;
                }

                if (listView.Checked)
                {
                    imagesView.Click += EventHandler;
                }
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            /*if (listViewInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem listView)
            {
                listView.Click -= EventHandler;

                if (!listView.Checked)
                {
                    listView.Click += EventHandler;
                }
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }*/

            /*if (controlType != null)
            {
                if (controlType.EndsWith("ImageGamesControl"))
                {

                }

                if (controlType.EndsWith("ListGamesControl"))
                {

                }
            }*/



        }

        public static void EnableConfigure()
        {
            var configureContextMenuInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("configureContextToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (configureContextMenuInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem configureContextMenuItem)
            {
                configureContextMenuItem.Enabled = true;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            var configureMenuInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("configureToolStripMenuItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (configureMenuInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is ToolStripMenuItem configureMenuItem)
            {
                configureMenuItem.Enabled = true;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }

            var configureButtonInfo = PluginHelper.LaunchBoxMainForm.GetType().GetField("configureButton", BindingFlags.NonPublic | BindingFlags.Instance);

            if (configureButtonInfo?.GetValue(PluginHelper.LaunchBoxMainForm) is Button configureButton)
            {
                configureButton.Enabled = true;
            }
            else
            {
                Console.WriteLine(@"Fatal Error");
            }
        }
    }
}
