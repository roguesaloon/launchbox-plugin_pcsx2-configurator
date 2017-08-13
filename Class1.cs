using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{

    public class Class1 : IGameMenuItemPlugin
    {
        public string Caption
        {
            get
            {
                return "Configure PCSX2 Settings";
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

        public bool GetIsValidForGame(IGame selectedGame)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);
            if (emulator != null && emulator.Title == "PCSX2")
                return true;
           
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
            var pcsx2 = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);
            var safeTitle = selectedGame.Title;
            foreach(var c in Path.GetInvalidFileNameChars())
            {
                safeTitle = safeTitle.Replace(c.ToString(),"");
            }
            var configPath = "inis\\" + safeTitle;
            selectedGame.CommandLine = pcsx2.CommandLine + " --cfgpath \"" + configPath + "\"";
            configPath = Path.GetDirectoryName(pcsx2.ApplicationPath) + "\\" + configPath;
            if (!File.Exists(configPath + "\\PCSX2_ui.ini"))
            {
                Directory.CreateDirectory(configPath);
                var uiConfigFile = File.CreateText(configPath + "\\PCSX2_ui.ini");
                uiConfigFile.Write("[MemoryCards]\r\nSlot1_Enable=enabled\r\nSlot1_Filename=" + safeTitle.Replace(" ", "") + ".ps2");
                uiConfigFile.Close();
            }
            Process.Start(pcsx2.ApplicationPath, "--cfgpath \"" +  configPath + "\"");
        }
    }
}
