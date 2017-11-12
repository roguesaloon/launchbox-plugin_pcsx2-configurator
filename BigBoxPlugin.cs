using System.Diagnostics;
using System.Drawing;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    public class BigBoxPlugin : IGameMenuItemPlugin, ISystemEventsPlugin
    {
        public void OnEventRaised(string eventType)
        {
            if (eventType == "BigBoxStartupCompleted")
            {
                foreach(var game in PluginHelper.DataManager.GetAllGames())
                {
                    if(GetIsValidForGame(game))
                        game.ConfigurationPath = "";
                }
            }
        }

        public string Caption
        {
            get
            {
                return "Create/Download Config";
            }
        }

        public Image IconImage
        {
            get
            {
                return null;
            }
        }

        public bool ShowInBigBox
        {
            get
            {
                return true;
            }
        }

        public bool ShowInLaunchBox
        {
            get
            {
                return false;
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

            if (emulator != null && (emulator.Title.Contains("PCSX2") || ((emulator.Title.Contains("Rocket Launcher") || emulator.Title.Contains("RocketLauncher")) && selectedGame.Platform == "Sony Playstation 2")))
            {
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
            var emulator = PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId);

            var configParams = LaunchBoxPlugin.GetConfigParams(selectedGame, emulator);
            LaunchBoxPlugin.SetConfigDirectories(selectedGame, emulator, configParams[2]);

            Process.Start(configParams[0], configParams[1]);

            // Kills PCSX2 as Soon as it starts, in external Thread to Keep BigBox Responsive
            new System.Threading.Tasks.Task(() =>
            {
                while (Process.GetProcessesByName("PCSX2").Length == 0)
                    System.Threading.Thread.Sleep(1);

                Process.GetProcessesByName("PCSX2")[0].Kill();
            }).Start();

            return;
        }
    }
}
