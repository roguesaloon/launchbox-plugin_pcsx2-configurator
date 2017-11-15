using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{

    public class LaunchBoxPlugin : IGameMenuItemPlugin, ISystemMenuItemPlugin, ISystemEventsPlugin
    {
        public static string pluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string settingsFile = pluginDir + "\\Settings.ini";
        private static string configsDir = IniFileHelper.ReadValue("PCSX2_Configurator", "ConfigsDirectoryPath", settingsFile, "default");

        private static Form settingsForm;

        public void OnEventRaised(string eventType)
        {
            // Hides Configure in BigBox
            if (eventType == "BigBoxStartupCompleted")
            {
                foreach (var game in PluginHelper.DataManager.GetAllGames())
                {
                    if (LaunchBoxPlugin.IsGameValid(game))
                        game.ConfigurationPath = "";
                }
            }

            if (eventType == "LaunchBoxStartupCompleted")
            {
                HideContextMenuItem(true);
            }

            if (eventType == "PluginInitialized")
            {
                // Downloads and Extracts SVN
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\SVN"))
                {
                    new WebClient().DownloadFile("https://www.visualsvn.com/files/Apache-Subversion-1.9.7.zip", Directory.GetCurrentDirectory() + "\\SVN.zip");
                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + "\\SVN.zip", Directory.GetCurrentDirectory() + "\\SVN");
                    File.Delete(Directory.GetCurrentDirectory() + "\\SVN.zip");
                }

                // Create Settings File (If Not There)
                if (!File.Exists(settingsFile))
                {
                    WriteDefaultIniFile();
                }

                // Extracts Widescreen Patches that are not present
                try
                {
                    ZipFile.ExtractToDirectory(Path.GetDirectoryName(GetFullEmulatorPath()) + "\\cheats_ws.zip", Path.GetDirectoryName(GetFullEmulatorPath()) + "\\cheats_ws");
                }
                catch { }
            }
        }

        private static string GetFullEmulatorPath()
        {
            foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
            {
                if (emulator.Title.Contains("PCSX2"))
                {
                    var appPath = emulator.ApplicationPath;
                    appPath = (!Path.IsPathRooted(appPath)) ? Directory.GetCurrentDirectory() + "\\" + appPath : appPath;

                    return appPath;
                }
            }

            MessageBox.Show("It appears you do not have PCSX2 added to LaunchBox\nWhich this plugin needs to function correctly");

            return null;
        }

        private void WriteDefaultIniFile()
        {
            File.Create(settingsFile).Dispose();

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseIndependantMemoryCards", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFileSettings", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentWindowSettings", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLogSettings", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "AllowAllSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFolderSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentVMSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentSPU2xPluginSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "ConfigsDirectoryPath", configsDir, settingsFile);
        }

        private void HideContextMenuItem(bool hide)
        {
            // Gets the context menu from Launchbox Main Form
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField.GetValue(PluginHelper.LaunchBoxMainForm);

            var hiddenItems = new bool[contextMenuStrip.Items.Count];

            // Hides or shows the menu item with this plugins caption
            for (int i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Text == Caption)
                {
                    contextMenuStrip.Items[i].Visible = !hide;
                    hiddenItems[i] = hide;
                    break;
                }
            }


            // If all plugins in context menu are hidden, also hide the last seperator
            for (int i = contextMenuStrip.Items.Count - 1, itemsHidden = 0; i > -1; --i)
            {
                if (contextMenuStrip.Items[i].GetType() == typeof(ToolStripSeparator))
                {
                    contextMenuStrip.Items[i].Visible = (itemsHidden == contextMenuStrip.Items.Count-i-1) ? false : true;
                    break;
                }

                if (hiddenItems[i] == true)
                {
                    itemsHidden++;
                }
            }
        }

        public string Caption
        {
            get
            {
                return "PCSX2 Configurator Settings";
            }
        }

        public System.Drawing.Image IconImage
        {
            get
            {
                return EmulatorIcon().ToBitmap();
            }
        }

        public static System.Drawing.Icon EmulatorIcon()
        {
            return Icon.ExtractAssociatedIcon(GetFullEmulatorPath());
        }

        public bool ShowInBigBox
        {
            get
            {
                return false;
            }
        }

        public bool ShowInLaunchBox
        {
            get
            {
                return true;
            }
        }

        public bool SupportsMultipleGames
        {
            get
            {
                return false;
            }
        }

        public bool AllowInBigBoxWhenLocked
        {
            get
            {
                return ShowInBigBox;
            }
        }

        private static bool IsGameValid(IGame game)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(game.EmulatorId);
            return (emulator != null && (emulator.Title.Contains("PCSX2") || ((emulator.Title.Contains("Rocket Launcher") || emulator.Title.Contains("RocketLauncher")) && game.Platform == "Sony Playstation 2")));

        }

        public bool GetIsValidForGame(IGame selectedGame)
        {
            System.EventHandler onConfigureClick = null;

            if (IsGameValid(selectedGame))
            {
                // Set The Configuration Parameters For Selected Game
                var configParams = GetConfigParams(selectedGame);
                selectedGame.ConfigurationPath = configParams[0];
                selectedGame.ConfigurationCommandLine = configParams[1];

                // Sets Action To Complete When Configure is Pressed On Selected Game
                onConfigureClick = (sender, e) => SetConfigDirectories(selectedGame, configParams[2]);
                SetConfigureOnClick(onConfigureClick);
            }

            

            return onConfigureClick != null;
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return SupportsMultipleGames;
        }

        public void OnSelected(IGame[] selectedGames)
        {
           return;
        }

        public void OnSelected(IGame selectedGame)
        {
            return;
        }

        private static string[] GetConfigParams(IGame selectedGame)
        {
            // Finds Emulator Associated with Game
            var emulator = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);

            // Gets the safe title of the game (with no illegal characters)
            var safeTitle = selectedGame.Title;
            foreach (char c in Path.GetInvalidFileNameChars())
                safeTitle = safeTitle.Replace(c.ToString(), "");

            // Sets the config path from user settings
            var configPath = (configsDir == "default") ? "inis" : configsDir;
            configPath += "\\" + safeTitle;

            // Gets the absolute path of the Game Config Directory
            var configPathAbs = (!Path.IsPathRooted(configPath)) ? Path.GetDirectoryName(GetFullEmulatorPath()) + "\\" + configPath : configPath;

            var parameters = new string[3]
            {
                Directory.GetCurrentDirectory() + "\\AutoHotkey\\AutoHotkey.exe",
                "\"" + pluginDir + "\\LoadConfig.ahk\" \"" + configPathAbs + "\"  \"" + GetFullEmulatorPath() + "\"",
                "--cfgpath \"" + configPath + "\""
            };

            // Add Another Param (Rom Name) for RocketLauncher
            if (emulator.Title.Contains("Rocket Launcher") || emulator.Title.Contains("RocketLauncher"))
            {
                var appPath = emulator.ApplicationPath;
                appPath = (!Path.IsPathRooted(appPath)) ? Directory.GetCurrentDirectory() + "\\" + appPath : appPath;

                var gamePath = selectedGame.ApplicationPath;
                gamePath = (!Path.IsPathRooted(gamePath)) ? Directory.GetCurrentDirectory() + "\\" + gamePath : gamePath;

                var appDir = Path.GetDirectoryName(appPath);
                var romName = Path.GetFileNameWithoutExtension(gamePath);

                parameters[1] += " \"" + romName + "\"";
            }

            return parameters;
        }

        private static void SetConfigDirectories(IGame selectedGame, string configPath)
        {
            // Finds Emulator Associated with Game
            var emulator = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);

            if (emulator.Title.Contains("PCSX2"))
            {
                // Set Game to use Custom Config (On Command-Line in LaunchBox)
                selectedGame.CommandLine = emulator.CommandLine + " " + configPath;
            }

            if ((emulator.Title.Contains("Rocket Launcher") || emulator.Title.Contains("RocketLauncher")) && selectedGame.Platform == "Sony Playstation 2")
            {
                var filePath = emulator.ApplicationPath;
                filePath = (!Path.IsPathRooted(filePath)) ? Directory.GetCurrentDirectory() + "\\" + filePath : filePath;
                filePath = Path.GetDirectoryName(filePath) + "\\Modules\\PCSX2\\PCSX2.ini";

                var fullConfigsDir = (configsDir == "default") ? Path.GetDirectoryName(GetFullEmulatorPath()) + "\\inis" : configsDir;

                // Tells RocketLauncher Where to Find Custom Configs, If not already known
                if (IniFileHelper.ReadValue("Settings", "cfgpath", filePath) != fullConfigsDir)
                    IniFileHelper.WriteValue("Settings", "cfgpath", fullConfigsDir, filePath);
            }
        }

        private void SetConfigureOnClick(System.EventHandler onConfigureClick)
        {
            // Gets the context menu from Launchbox Main Form
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField.GetValue(PluginHelper.LaunchBoxMainForm);

            var hiddenItems = new bool[contextMenuStrip.Items.Count];

            // Adds an On Click Listener to The Configure Button
            for (int i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Name == "configureContextToolStripMenuItem")
                {
                    // Setting Event to Subscribe to on Configure Click
                    contextMenuStrip.Items[i].Click += onConfigureClick;
                    break;
                }
            }
        }

        public void OnSelected()
        {
           if(settingsForm != null) settingsForm.Close();
           settingsForm  = new SettingsForm();

            settingsForm.StartPosition = FormStartPosition.Manual;
            settingsForm.Location = new Point(
                PluginHelper.LaunchBoxMainForm.Location.X + (int)((PluginHelper.LaunchBoxMainForm.Width - settingsForm.Width) * 0.5f), 
                PluginHelper.LaunchBoxMainForm.Location.Y + (int)((PluginHelper.LaunchBoxMainForm.Height - settingsForm.Height) * 0.5f));
            settingsForm.Show();
        }
    }
}
