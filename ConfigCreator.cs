using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using IniParser;
using Microsoft.Win32;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    internal class ConfigCreator
    {
        private string _emulatorDirectory;
        private string _baseConfigDirectory, _targetConfigDirectory;
        private string _baseUiFile, _targetUiFile;
        private bool _useRemoteSettings;
        private readonly string _settingsFile = Utilities.PluginDirectory + "\\Settings.ini";
        private readonly string _uiFileName = "PCSX2_ui.ini";

        public void CreateConfig(IGame game)
        {
            _emulatorDirectory = Path.GetDirectoryName(Utilities.FullEmulatorPath);
            _baseConfigDirectory = GetBaseConfigDirectory(_emulatorDirectory, _uiFileName);
            _targetConfigDirectory = Utilities.GetConfigDir(game);
            _baseUiFile = _baseConfigDirectory + "\\" + _uiFileName;
            _targetUiFile = _targetConfigDirectory + "\\" + _uiFileName;
            _useRemoteSettings = bool.Parse(Utilities.GetPrivateField(game, "UseRemoteSettings"));

            if (File.Exists(_targetConfigDirectory + "\\" + _uiFileName) && !_useRemoteSettings)
            {
                return;
            }

            if (DownloadRemoteSettings(_useRemoteSettings, game) && File.Exists(_targetUiFile))
            {
                return;
            }

            WriteUiConfigFile(_targetConfigDirectory, _baseUiFile, _targetUiFile, _settingsFile, _emulatorDirectory, game);
            CopyOtherSettings(_baseConfigDirectory, _targetConfigDirectory, _settingsFile);

            if (_useRemoteSettings)
            {
                ApplyRemoteSettings(_targetConfigDirectory, _emulatorDirectory, _targetUiFile, game);
            }

            RemoveGeneralSettingsHeader(_targetUiFile);

            if (Utilities.IsUsingRocketLauncher(game))
            {
                FixForRocketLauncher(_targetConfigDirectory, game);
            }
        }

        private static string GetBaseConfigDirectory(string emulatorDirectory, string uiFileName)
        {
            string baseConfigDirectory;

            if (File.Exists(emulatorDirectory + "\\inis" + "\\" + uiFileName))
            {
                baseConfigDirectory = emulatorDirectory + "\\inis";
            }
            else if (File.Exists(emulatorDirectory + "\\inis_1.4.0" + "\\" + uiFileName))
            {
                baseConfigDirectory = emulatorDirectory + "\\inis_1.4.0";
            }
            else
            {
                baseConfigDirectory = string.Empty;
                var settingsFolder =
                    Registry.GetValue("HKEY_CURRENT_USER\\Software\\PCSX2", "SettingsFolder", string.Empty) as string;
                if (settingsFolder != string.Empty)
                {
                    baseConfigDirectory = settingsFolder;
                }
            }

            return baseConfigDirectory;
        }

        private static bool DownloadRemoteSettings(bool useRemoteSettings, IGame game)
        {
            if (!useRemoteSettings) return false;

            var remoteDirectoryInfo = Directory.CreateDirectory(Utilities.PluginDirectory + "\\remote");
            remoteDirectoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            var remoteSettingsUrl = Utilities.GetRemoteUrl(game, svnStyle: true);

            var svnProcess = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = Utilities.LaunchboxDirectory + "\\SVN\\bin\\svn.exe",
                    Arguments = "checkout " + remoteSettingsUrl,
                    WorkingDirectory = remoteDirectoryInfo.FullName
                }
            };

            svnProcess.Start();
            var svnOutput = svnProcess.StandardOutput.ReadToEnd();
            svnProcess.WaitForExit();

            if (!svnOutput.Contains("Checked out revision"))
            {
                Console.WriteLine(@"Github Error");
                return true;
            }

            if (svnOutput.Contains("A    "))
            {
                MessageBox.Show(@"Configuration Has Been Downloaded or Updated", @"PCSX2 Configurator");
            }
            else
            {
                return true;
            }

            return false;
        }

        private static void WriteUiConfigFile(string targetConfigDirectory, string baseUiFile, string targetUiFile, string settingsFile, string emulatorDirectory, IGame game)
        {
            Directory.CreateDirectory(targetConfigDirectory);
            var targetUiFileStream = File.CreateText(targetUiFile);
            targetUiFileStream.Write("[GeneralSettings]\r\n");
            targetUiFileStream.Dispose();

            var iniParser = new FileIniDataParser();


            var pluginSettings = iniParser.ReadFile(settingsFile)["PCSX2_Configurator"];
            var baseUiConfig = iniParser.ReadFile(baseUiFile);
            var targetUiConfig = iniParser.ReadFile(targetUiFile);

            // If Program Log Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentLogSettings"]))
            {
                // Copy Program Log Settings From Base Config To Target Config
                targetUiConfig["ProgramLog"].Merge(baseUiConfig["ProgramLog"]);
            }

            // If Folder Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentFolderSettings"]))
            {
                // Copy Folders Settings From Base Config To Target Config
                targetUiConfig["Folders"].Merge(baseUiConfig["Folders"]);
            }

            // If File Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentFileSettings"]))
            {
                // Copy File Names Settings From Base Config To Target Config
                targetUiConfig["Filenames"].Merge(baseUiConfig["Filenames"]);
            }

            // If Window Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentWindowSettings"]))
            {
                // Copy GS Window Settings From Base Config To Target Config
                targetUiConfig["GSWindow"].Merge(baseUiConfig["GSWindow"]);
            }

            // If Independant Memory Cards Are To Be Used
            if (bool.Parse(pluginSettings["UseIndependantMemoryCards"]))
            {
                var memCardsDirectory = emulatorDirectory + "\\memcards";
                var memCardFile = memCardsDirectory + "\\" + Utilities.GetSafeTitle(game).Replace(" ", "") + ".ps2";

                // Set One For This Game
                targetUiConfig["MemoryCards"]["Slot1_Enable"] = "enabled";
                targetUiConfig["MemoryCards"]["Slot1_Filename"] = Path.GetFileName(memCardFile);

                // If It Doesn't Exist
                if (!File.Exists(memCardFile))
                {
                    var sevenZipProcess = new Process
                    {
                        StartInfo =
                        {
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            FileName = Utilities.LaunchboxDirectory + "\\7-Zip\\7z.exe",
                            Arguments = "e \"" + Utilities.PluginDirectory + "\\Assets\\Mcd.7z\" -o\"" + memCardsDirectory + "\""
                        }
                    };

                    // Extract a Newly Formatted Memory Card
                    sevenZipProcess.Start();
                    sevenZipProcess.WaitForExit();
                    File.Move(memCardsDirectory + "\\Mcd.ps2", memCardFile);
                }
            }

            // If All Settings Are To Be Enabled
            if (bool.Parse(pluginSettings["AllowAllSettings"]))
            {
                // Disable Presets, Enabled Game Fixes and Speed Hacks
                targetUiConfig["GeneralSettings"]["EnablePresets"] = "disabled";
                targetUiConfig["GeneralSettings"]["EnableGameFixes"] = "enabled";
                targetUiConfig["GeneralSettings"]["EnableSpeedHacks"] = "enabled";
            }

            // Sets Current Iso To This Game
            var isoPath = !Path.IsPathRooted(game.ApplicationPath)
                ? $"{Utilities.LaunchboxDirectory}\\{game.ApplicationPath}"
                : game.ApplicationPath;

            targetUiConfig["GeneralSettings"]["CurrentIso"] = isoPath.Replace("\\", "\\\\");
            targetUiConfig["GeneralSettings"]["AskOnBoot"] = "disabled";

            // Saves Changes To File
            iniParser.WriteFile(targetUiFile, targetUiConfig);
        }

        private static void CopyOtherSettings(string baseConfigDircetory, string targetConfigDirectory, string settingsFile)
        {
            var iniParser = new FileIniDataParser();

            var pluginSettings = iniParser.ReadFile(settingsFile)["PCSX2_Configurator"];

            // If VM Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentVMSettings"]))
            {
                // Copy Them Over
                File.Copy(baseConfigDircetory + "\\PCSX2_vm.ini", targetConfigDirectory + "\\PCSX2_vm.ini", overwrite: true);
            }

            // If GSdx Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentGSdxPluginSettings"]))
            {
                // Copy Them Over
                File.Copy(baseConfigDircetory + "\\GSdx.ini", targetConfigDirectory + "\\GSdx.ini", overwrite: true);
            }

            // If SPU2-X Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentGSdxPluginSettings"]))
            {
                // Copy Them Over
                File.Copy(baseConfigDircetory + "\\SPU2-X.ini", targetConfigDirectory + "\\SPU2-X.ini", overwrite: true);
            }

            // If Lilypad Settings Are To Be Imported From Base Config
            if (bool.Parse(pluginSettings["UseCurrentGSdxPluginSettings"]))
            {
                // Copy Them Over
                File.Copy(baseConfigDircetory + "\\LilyPad.ini", targetConfigDirectory + "\\LilyPad.ini", overwrite: true);
            }
        }

        private static void ApplyRemoteSettings(string targetConfigDirectory, string emulatorDirectory, string targetUiFile, IGame game)
        {
            var remoteSettingsDirectory = Utilities.PluginDirectory + "\\remote\\" + Utilities.GetSafeTitle(game);

            foreach (var file in Directory.GetFiles(remoteSettingsDirectory))
            {
                File.Copy(file, targetConfigDirectory + "//" + Path.GetFileName(file), overwrite: true);
            }

            if (Utilities.GetSafeTitle(game).Contains("&"))
            {
                Directory.Move(targetConfigDirectory, targetConfigDirectory.Replace("and", "&"));
            }

            var iniParser = new FileIniDataParser();

            var targetUiConfig = iniParser.ReadFile(targetUiFile);

            targetUiConfig["GeneralSettings"]["EnablePresets"] = "disabled";
            targetUiConfig["GeneralSettings"]["EnableGameFixes"] = "enabled";
            targetUiConfig["GeneralSettings"]["EnableSpeedHacks"] = "enabled";

            var uiTweakFile = targetConfigDirectory + "\\PCSX2_ui-tweak.ini";
            if (File.Exists(uiTweakFile))
            {
                var uiTweakConfig = iniParser.ReadFile(uiTweakFile);

                targetUiConfig.Merge(uiTweakConfig);
                iniParser.WriteFile(targetUiFile, targetUiConfig);

                /*foreach (var section in uiTweakConfig)
                {
                    targetUiConfig[section.Name].GetValuesFrom(section);
                }*/

                File.Delete(uiTweakFile);
            }

            foreach (var file in Directory.GetFiles(targetConfigDirectory, "*.pnach"))
            {
                File.Move(file, emulatorDirectory + "\\cheats\\" + Path.GetFileName(file));
            }
            
        }

        private static void RemoveGeneralSettingsHeader(string targetUiFile)
        {
            var text = File.ReadAllText(targetUiFile);
            text = text.Replace("[GeneralSettings]\r\n", "");
            var targetUiFileStream = File.CreateText(targetUiFile);
            targetUiFileStream.Write(text);
            targetUiFileStream.Dispose();
        }

        private static void FixForRocketLauncher(string targetConfigDirectory, IGame game)
        {
            Directory.Move(targetConfigDirectory, Utilities.ConfigsDir + "\\" + Path.GetFileNameWithoutExtension(game.ApplicationPath));
        }
    }
}
