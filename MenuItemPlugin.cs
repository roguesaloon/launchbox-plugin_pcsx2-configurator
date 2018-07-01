using System;
using System.Drawing;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    internal class MenuItemPlugin : IGameMenuItemPlugin
    {
        public string Caption => "PCSX2 Configurator";

        public Image IconImage => Utilities.EmulatorIcon?.ToBitmap();

        public bool ShowInBigBox => false;

        public bool ShowInLaunchBox => !Utilities.IsLaunchBoxNext();

        public bool SupportsMultipleGames => false;

        public bool GetIsValidForGame(IGame selectedGame) => Utilities.IsGameValid(selectedGame);

        public bool GetIsValidForGames(IGame[] selectedGames) => SupportsMultipleGames;

        public void OnSelected(IGame[] selectedGames) => Console.WriteLine("");

        public void OnSelected(IGame selectedGame) => Console.WriteLine("");
    }
}
