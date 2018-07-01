using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using IniParser;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    internal static class Configurator
    {
        public static ToolStripMenuItem UseRemoteSettings { get; set; }

        private static bool IsKnownGame(IGame game)
        {
            var url = Utilities.GetRemoteUrl(game);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowReadStreamBuffering = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = "HEAD";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                response.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return response != null && response.StatusCode == HttpStatusCode.OK;
        }

        private static string GetConfigParam(IGame game)
        {
            var configParam = $"--cfgpath \"{Utilities.GetConfigDir(game)}\"";
            return configParam;
        }

        private static void SetCommandLineParameters(IGame game)
        {
            var emulator = Utilities.GetAssociatedEmulator(game);
            if (emulator.Title.Contains("PCSX2") && string.IsNullOrEmpty(game.CommandLine))
            {
                game.CommandLine = $"{emulator.CommandLine} {GetConfigParam(game)}";
            }
            else if (Utilities.IsUsingRocketLauncher(game))
            {
                var rlPcsx2ConfigFile =
                    Path.GetDirectoryName(Utilities.FullEmulatorPath) + "\\Modules\\PCSX2\\PCSX2.ini";

                var iniParser = new FileIniDataParser();

                var rlPcsx2Config = iniParser.ReadFile(rlPcsx2ConfigFile);

                if (rlPcsx2Config != null && rlPcsx2Config["Settings"]["cfgPath"] != Utilities.ConfigsDir)
                {
                    rlPcsx2Config["Settings"]["cfgPath"] = Utilities.ConfigsDir;
                }

                iniParser.WriteFile(rlPcsx2ConfigFile, rlPcsx2Config);
            }
        }

        private static void ConfigureGame(IGame game)
        {
            if (!Utilities.IsGameValid(game)) return;

            OnSelectionChange();

            var configCreator = new ConfigCreator();
            
            configCreator.CreateConfig(game);
            
            SetCommandLineParameters(game);
        }

        public static void OnChangeView()
        {
            Utilities.SetSelectionChangeEvent(OnSelectionChange);
            Utilities.SetChangeViewEvent(OnChangeView);
        }

        public static void OnPrePlay()
        {
            var selectedGame = Utilities.GetSelectedGame();

            if (!Utilities.IsGameValid(selectedGame)) return;

            Cursor.Current = Cursors.WaitCursor;

            if (!bool.TryParse(Utilities.GetPrivateField(selectedGame, "UseRemoteSettings"), out var remoteSettingsForGame))
            {
                OnSelectionChange();
                remoteSettingsForGame = bool.Parse(Utilities.GetPrivateField(selectedGame, "UseRemoteSettings")); 
            }

            var directoryForGameExists = Directory.Exists(Utilities.GetConfigDir(selectedGame));

            if (!remoteSettingsForGame && !directoryForGameExists)
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            ConfigureGame(selectedGame);

            Cursor.Current = Cursors.Default;
        }

        public static void OnConfigure()
        {
            var selectedGame = Utilities.GetSelectedGame();

            if (!Utilities.IsGameValid(selectedGame)) return;

            Cursor.Current = Cursors.WaitCursor;
            ConfigureGame(selectedGame);
            Cursor.Current = Cursors.Default;

            Process.Start(Utilities.FullEmulatorPath, GetConfigParam(selectedGame));
        }

        public static void OnSelectionChange()
        {
            var selectedGame = Utilities.GetSelectedGame();

            if (selectedGame != null && Utilities.IsGameValid(selectedGame))
            {
                Utilities.EnableConfigure();
                Utilities.HideContextMenuItem("PCSX2 Configurator", hide: false);
                Utilities.HidePluginSeparatorIfApplicable(hide: false);

                if (IsKnownGame(selectedGame))
                {
                    UseRemoteSettings.Enabled = true;

                    var useRemoteSettingsField = Utilities.GetPrivateField(selectedGame, "UseRemoteSettings") ??
                                                  Utilities.SetPrivateField(selectedGame, "UseRemoteSettings", 
                                                      new FileIniDataParser().ReadFile(Utilities.SettingsFile)["PCSX2_Configurator"]["EnableRemoteSettingsByDefault"]);

                    if (UseRemoteSettings != null)
                    {
                        UseRemoteSettings.Checked = bool.Parse(useRemoteSettingsField);
                    }
                }
                else
                {
                    Utilities.SetPrivateField(selectedGame, "UseRemoteSettings", bool.FalseString);

                    if (UseRemoteSettings == null) return;
                    UseRemoteSettings.Checked = false;
                    UseRemoteSettings.Enabled = false;
                }
            }
            else
            {
                Utilities.HideContextMenuItem("PCSX2 Configurator", hide: true);
                Utilities.HidePluginSeparatorIfApplicable(hide: true);
            }
        }

        public static void OnUseRemoteSettingsChange()
        {
            var selectedGame = Utilities.GetSelectedGame();

            UseRemoteSettings.Checked = !UseRemoteSettings.Checked;
            Utilities.SetPrivateField(selectedGame, "UseRemoteSettings", UseRemoteSettings.Checked.ToString());
        }
    }
}
