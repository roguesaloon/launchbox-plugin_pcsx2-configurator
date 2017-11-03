using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{

    public class Class1 : IGameMenuItemPlugin, ISystemMenuItemPlugin, ISystemEventsPlugin
    {
        private static string pluginDir =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string settingsFile = pluginDir + "\\Settings.ini";
        private static string configsDir = 
            IniFileHelper.ReadValue("PCSX2_Configurator", "ConfigsDirectoryPath", settingsFile, "default");

        public void OnEventRaised(string eventType)
        {
            if (eventType == "LaunchBoxStartupCompleted")
            {
                HideContextMenuItem(true);

                if(!File.Exists(settingsFile))
                    WriteDefaultIniFile();
            }
        }

        private void WriteDefaultIniFile()
        {
            File.Create(settingsFile).Dispose();

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseIndependantMemoryCards", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFileSettings", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentWindowSettings", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLogSettings", "true", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFolderSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentVMSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings", "false", settingsFile);
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings", "false", settingsFile);
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
                    hiddenItems[i] = true;
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
                return Properties.Resources.pcsx2.ToBitmap();
            }
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

        public bool GetIsValidForGame(IGame selectedGame)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);
            if (emulator != null && emulator.Title == "PCSX2")
            {
                var configParams = GetConfigParams(selectedGame);
                selectedGame.ConfigurationPath = configParams[0];
                selectedGame.ConfigurationCommandLine = configParams[1];

                selectedGame.CommandLine = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId).CommandLine + " " + configParams[2];
                return true;
            }

            return false;
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

        private string[] GetConfigParams(IGame selectedGame)
        {
            // Gets the emulator associated with this game (should be PCSX2)
            var pcsx2 = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);

            // Gets the safe title of the game (with no illegal characters)
            var safeTitle = selectedGame.Title;
            foreach (char c in Path.GetInvalidFileNameChars())
                safeTitle = safeTitle.Replace(c.ToString(), "");

            // Gets the given and absolute path of the PCSX2 emulator
            var appPath = pcsx2.ApplicationPath;
            var appPathAbs = (!Path.IsPathRooted(appPath)) ? Directory.GetCurrentDirectory() + "\\" + appPath : appPath;

            // Sets the config path from user settings
            var configPath = (configsDir == "default") ? "inis" : configsDir;
            configPath += "\\" + safeTitle;

            // Gets the absolute path of the Game Config Directory
            var configPathAbs = (!Path.IsPathRooted(configPath)) ? Path.GetDirectoryName(appPathAbs) + "\\" + configPath : configPath;

            // If PCSX2 is not already set up in this folder
            if (!File.Exists(configPathAbs + "\\PCSX2_ui.ini"))
            {
                return new string[3]
                {
                    Directory.GetCurrentDirectory() + "\\AutoHotkey\\AutoHotkey.exe",
                    "\"" + pluginDir + "\\Config Create.ahk\" \"" + configPathAbs + "\"  \"" + appPathAbs + "\"",
                    "--cfgpath \"" + configPath + "\""
                };
            }

            // Return the application path, with the config path as a commandline argument
            return new string[3] { pcsx2.ApplicationPath, "--cfgpath \"" + configPath + "\"", "--cfgpath \"" + configPath + "\""};
        }

        public void OnSelected()
        {
            var settingsForm = new Form1();
            settingsForm.StartPosition = FormStartPosition.Manual;
            settingsForm.Location = new Point(
                PluginHelper.LaunchBoxMainForm.Location.X + (int)((PluginHelper.LaunchBoxMainForm.Width - settingsForm.Width) * 0.5f), 
                PluginHelper.LaunchBoxMainForm.Location.Y + (int)((PluginHelper.LaunchBoxMainForm.Height - settingsForm.Height) * 0.5f));
            settingsForm.Show();
        }
    }
}
