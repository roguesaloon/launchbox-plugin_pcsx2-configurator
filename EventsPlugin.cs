using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using IniParser;
using Unbroken.LaunchBox.Plugins;

namespace PCSX2_Configurator
{
    internal class EventsPlugin : ISystemEventsPlugin
    {
        public void OnEventRaised(string eventType)
        {
            switch (eventType)
            {
                case "LaunchBoxStartupCompleted":
                    LaunchBoxStartupCompleted();
                    break;
                case "PluginInitialized":
                    PluginIntialized();
                    break;
                default:
                    Console.WriteLine(eventType);
                    break;
            }
        }

        private static void LaunchBoxStartupCompleted()
        {
            Configurator.UseRemoteSettings =
                Utilities.AddContextMenuItemToSubMenu("PCSX2 Configurator", "Use Remote Settings");

            Utilities.SetChangeViewEvent(Configurator.OnChangeView);
            Utilities.SetPrePlayEvent(Configurator.OnPrePlay);
            Utilities.SetConfigureEvent(Configurator.OnConfigure);
            Utilities.SetSelectionChangeEvent(Configurator.OnSelectionChange);
            Utilities.SetMenuItemEvent(Configurator.UseRemoteSettings, Configurator.OnUseRemoteSettingsChange);
        }

        private static void PluginIntialized()
        {
            if (Utilities.IsLaunchBoxNext()) return;
            DownloadSvn();
            CreateSettingsFile();
            CreateGameOptions();
            ExtractWidescreenPatches();
        }

        private static void DownloadSvn()
        {
            if (Directory.Exists(Utilities.LaunchboxDirectory + "\\SVN")) return;
            try
            {
                new WebClient().DownloadFile("https://www.visualsvn.com/files/Apache-Subversion-1.9.7.zip", Utilities.LaunchboxDirectory + "\\SVN.zip");
                ZipFile.ExtractToDirectory(Utilities.LaunchboxDirectory + "\\SVN.zip", Utilities.LaunchboxDirectory + "\\SVN");
                File.Delete(Utilities.LaunchboxDirectory + "\\SVN.zip");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static bool CreateSettingsFile()
        {
            if (File.Exists(Utilities.SettingsFile)) return false;
            File.Create(Utilities.SettingsFile).Dispose();

            var iniParser = new FileIniDataParser();
            var pluginSettings = iniParser.ReadFile(Utilities.SettingsFile);

            pluginSettings["PCSX2_Configurator"]["UseIndependantMemoryCards"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentFileSettings"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentWindowSettings"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentLogSettings"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["AllowAllSettings"] = bool.FalseString;
            pluginSettings["PCSX2_Configurator"]["EnableRemoteSettingsByDefault"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentFolderSettings"] = bool.FalseString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentVMSettings"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentGSdxPluginSettings"] = bool.TrueString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentLilyPadPluginSettings"] = bool.FalseString;
            pluginSettings["PCSX2_Configurator"]["UseCurrentSPU2xPluginSettings"] = bool.FalseString;
            pluginSettings["PCSX2_Configurator"]["ConfigsDirectoryPath"] = Utilities.ConfigsDir;

            iniParser.WriteFile(Utilities.SettingsFile, pluginSettings);
            return true;
        }

        private static void CreateGameOptions()
        {
            var filePath = Utilities.PluginDirectory + "\\GameOptions.xml";
            if (File.Exists(filePath)) return;
            var file = new XmlDocument();
            var rootElement = file.CreateElement("GameOptions");
            file.AppendChild(rootElement);
            file.Save(filePath);
        }

        private static void ExtractWidescreenPatches()
        {
            try
            {
                ZipFile.ExtractToDirectory(Path.GetDirectoryName(Utilities.FullEmulatorPath) + "\\cheats_ws.zip", Path.GetDirectoryName(Utilities.FullEmulatorPath) + "\\cheats_ws");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
