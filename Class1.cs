using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{

    public class Class1 : IGameMenuItemPlugin, ISystemMenuItemPlugin, ISystemEventsPlugin
    {
        private bool independantMemCards = true;
        private bool useCurrentLogs = true;
        private bool useCurrentFolders = true;
        private bool useCurrentFiles = true;
        private bool useCurrentWindow = true;
        private bool useCurrentGSdx = true;
        private bool useCurrentLilyPad = true;
        private string configsDir = "default";

        public void OnEventRaised(string eventType)
        {
            if (eventType == "LaunchBoxStartupCompleted")
            {
                HideContextMenuItem(true);
            }
        }

        private void HideContextMenuItem(bool hide)
        {
            // Gets the context menu from Launchbox Main Form
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField.GetValue(PluginHelper.LaunchBoxMainForm);

            // Hides or shows the menu item with this plugins caption
            for (int i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Text == Caption)
                {
                    contextMenuStrip.Items[i].Visible = !hide;
                    break;
                }
            }


            // If all plugins in context menu are hidden, also hide the last seperator
            for (int i = contextMenuStrip.Items.Count - 1, itemsHidden = 0; i > -1; --i)
            {
                if (contextMenuStrip.Items[i].GetType() == typeof(ToolStripSeparator))
                {
                    contextMenuStrip.Items[i].Visible = (itemsHidden == contextMenuStrip.Items.Count - i - 1) ? false : true;
                    break;
                }

                if (!contextMenuStrip.Items[i].Visible)
                    itemsHidden++;
            }

            // Write the changes to the context menu back to Launchbox Main Form
            contextMenuStripField.SetValue(PluginHelper.LaunchBoxMainForm, contextMenuStrip);
        }

        public string Caption
        {
            get
            {
                return "PCSX2 Configuration Settings";
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

                selectedGame.CommandLine = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId).CommandLine + " " + configParams[1];
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
            var path = Path.GetDirectoryName(pcsx2.ApplicationPath);
            var pathAbs = (!Path.IsPathRooted(path)) ? Directory.GetCurrentDirectory() + "\\" + path : path;

            // Sets the config path from user settings
            var configPath = (configsDir == "default") ? "inis" : configsDir;
            configPath += "\\" + safeTitle;

            // Gets the absolute path of the Game Config Directory
            var configPathAbs = (!Path.IsPathRooted(configPath)) ? pathAbs + "\\" + configPath : configPath;

            // Gets default config file Path (If Exists)
            var defaultConfigPath = (File.Exists(pathAbs + "\\inis\\PCSX2_ui.ini")) ? pathAbs + "\\inis" : (File.Exists(pathAbs + "\\inis_1.4.0\\PCSX2_ui.ini")) ? pathAbs + "\\inis_1.4.0" : "none";

            // If PCSX2 is not already set up in this folder
            if (!File.Exists(configPathAbs + "\\PCSX2_ui.ini"))
            {
                // Create this folder
                Directory.CreateDirectory(configPathAbs);
                var uiConfig = "";

                // If There is a default config path
                if (defaultConfigPath != "none")
                {
                    // Store contents of UI Config File
                    var defaultUiConfig = File.ReadAllText(defaultConfigPath + "\\PCSX2_ui.ini");


                    // If Using Current Log Settings
                    if (useCurrentLogs)
                    {
                        // Get The ProgramLog Section from the default UI config file
                        var section = defaultUiConfig.Substring(defaultUiConfig.IndexOf("[ProgramLog]"));
                        section = section.Substring(0, section.IndexOf('[', 1) - 1);

                        // And add it to the per-game config
                        uiConfig += section;
                    }

                    // If Using Current (Custom) Folders
                    if (useCurrentFolders)
                    {
                        // Get The Folders Section from the default UI config file
                        var section = defaultUiConfig.Substring(defaultUiConfig.IndexOf("[Folders]"));
                        section = section.Substring(0, section.IndexOf('[', 1) - 1);

                        // And add it to the per-game config
                        uiConfig += section;
                    }

                    // If Using Current Files (Bios and Plugins)
                    if (useCurrentFiles)
                    {
                        // Get The Filenames Section from the default UI config file
                        var section = defaultUiConfig.Substring(defaultUiConfig.IndexOf("[Filenames]"));
                        section = section.Substring(0, section.IndexOf('[', 1) - 1);

                        // And add it to the per-game config
                        uiConfig += section;
                    }

                    // If Using Current GS Window Settins
                    if (useCurrentWindow)
                    {
                        // Get The GSWindow Section from the default UI config file
                        var section = defaultUiConfig.Substring(defaultUiConfig.IndexOf("[GSWindow]"));
                        section = section.Substring(0, section.IndexOf('[', 1) - 1);

                        // And add it to the per-game config
                        uiConfig += section;
                    }

                    // If Using Current GSdx (Graphics) Config (and exists)
                    if (useCurrentGSdx && File.Exists(defaultConfigPath + "\\GSdx.ini"))
                    {
                        // Copy the default config to the per-game config
                        File.Copy(defaultConfigPath + "\\GSdx.ini", configPathAbs + "\\GSdx.ini");
                    }

                    // If Using Current LillyPad (Controller) Config (and exists)
                    if (useCurrentLilyPad && File.Exists(defaultConfigPath + "\\LilyPad.ini"))
                    {
                        // Copy the default config to the per-game config
                        File.Copy(defaultConfigPath + "\\LilyPad.ini", configPathAbs + "\\LilyPad.ini");
                    }
                }

                // Add a new Memory Card for this game, if Independant Memory Cards is enabled
                if (independantMemCards) uiConfig += ("[MemoryCards]\r\nSlot1_Enable=enabled\r\nSlot1_Filename=" + safeTitle.Replace(" ", "") + ".ps2");

                // Create and Write UI Config to PCSX2 UI Config File
                var uiConfigFile = File.CreateText(configPathAbs + "\\PCSX2_ui.ini");
                uiConfigFile.Write(uiConfig);
                uiConfigFile.Close();
            }

            // Return the application path, with the config path as a commandline argument
            return new string[2] { pcsx2.ApplicationPath, "--cfgpath \"" + configPath + "\"" };
        }

        public void OnSelected()
        {
            MessageBox.Show("Config Menu");
        }
    }
}
