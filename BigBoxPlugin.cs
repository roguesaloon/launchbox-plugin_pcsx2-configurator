using System;
using Unbroken.LaunchBox.Plugins;

namespace PCSX2_Configurator
{
    class BigBoxPlugin : ISystemEventsPlugin
    {
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
        }
    }
}
