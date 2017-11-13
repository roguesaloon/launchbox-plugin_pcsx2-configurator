PCSX2 Configurator v0.98
-------------------------------------------------------------------------------
This is a configuration plugin for games being run in PCSX2 through Launchbox. 
It allows users to configure their games on a per-game basis, and download pre-optimized configurations, form right with LaunchBox.

Installation
-------------------------------------------------------------------------------
Simply Download and extract the zip file, place the "PCSX2 Configurator" folder in your LaunchBox Plugins Directory. Then Start LaunchBox and the plugin will be installed.

Settings
-------------------------------------------------------------------------------
The Plugin Settings Menu is located in LaunchBox's tools Menu. All Current settings are defined how PCSX2 is currently configured.
Each of the options is explained breifly below:

**Use Independant Memory Cards?** - create a new formatted memory card (In Slot 1) for each game that is configured

**Use Current Plugin & Bios Files?** - retains current plugin and bios selection when configuring a game (leaving this un-checked will require them to be re-configured)

**Use Current GS Window Settings?** - retains current GS Window Settings (Vsync/Aspect Ratio/Custom Window Size)

**Use Current Program Log Settings?** - retains current Program Log Settings, The main purpose of this is to show/hide the console window

**Use Current Folder Settings?** - retains current Folder Settings, This is generally only needed if default folders are not in use

**Use Current VM Settings?** - retains current VM Settings, This Mostly denotes what custom patches/hacks/cheats are enabled, and will usually be overidden by downloaded configs

**Use Current GSdx Plugin Settings?**	- retains current GSdx (Graphics Plugin) Settings, This will be overidden by downloaded configs

**Use Current SPU2-X Plugin Settings?** -	retains current SPU2-X (Sound Plugin) Settings, Not needed in most cases, May be overidden by downloaded configs

**Use Current LilyPad Plugin Settings?** - retains current LilyPad (Controller Plugin) Settings, Not needed in most cases
Configuartions Directory - the loaction where per-game configurations are stored, This is in the Emulators ini folder by default though can be changed

Closing the window will automatically apply the settings. Settings can also be changed directly using the Settings.ini file found in the Plugins Directory ("Plugins\PCSX2 Configurator")
This file also contains hidden settings:

**AllowAllSettings** - ensures Presets are disabled and Game Fixes and Speed Hacks are enabled in PCSX2

Usage
-------------------------------------------------------------------------------
The plugin has a minimalistic interface. To Create a custom configuration for a PS2 game with PCSX2 (or RocketLauncher) as it's default emulator, Simply right click on it and press configure. If the game already has optimized settings (and is named in accordance to the LaunchboxDB) then you will be asked if you want to use them, PCSX2 will then open to allow you to make tweaks to your config (for selected game). When you are happy with your current config you can close PCSX2, and simply run your game in LaunchBox as normal to play with configured settings. All optimized settings are designed for PCSX2 1.5.0-dev and later builds and may not work correctly and require some tweaking for older versions of PCSX2. To force a check for optimized settings (after already creating a config) hold left control when pressing configure, You can also quickly close the PCSX2 window with CTRL-ESC. For those using RocketLaucnher, this plugin should mostly just work, though it will set your PCSX2 Game Configs folder (In Rocket Launcher) to the folder specified in this plugin (PCSX2\inis by default), and you also need to have PCSX2 setup as an emulator in LaunchBox so the plugin knows where to find it.

Credit & Support
-------------------------------------------------------------------------------
The Plugin was developed by two members of The LaunchBox community with alec100_94 being responsible for the original idea, and bulk of the plugins development and Zombeaver, creating and working on the PCSX2 Configuration Project (Optimized Settings) and the artwork used within the plugin, as well as being a tester for the latest version.

For Support, bug reporting, feature request, or a desire to contribute to this plugin please contact alec100_94, by commenting on the file, mentioning him on the forums, or directly through personal message.
