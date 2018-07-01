PCSX2 Configurator v0.995 Alpha
-------------------------------------------------------------------------------
This is a configuration plugin for games being run in PCSX2 through Launchbox. 
It allows users to configure their games on a per-game basis, and download pre-optimized configurations, from right with LaunchBox.
This version is an Alpha, Meaning it has some bugs that I will work with the community to resolve over time.

Installation
-------------------------------------------------------------------------------
You will first need to have PCSX2 (>1.5.0-dev) setup as an emulator in LaunchBox. If you have used any previous version of PCSX2 Configurator, it is advised that you remove them before installing this version. After this has been setup simply download and extract the zip file, place the "PCSX2 Configurator" folder in your LaunchBox Plugins Directory. Then Start LaunchBox and the plugin will be installed.

General Usage
-------------------------------------------------------------------------------
Straight out of the box, configs will automatically be downloaded and applied (also updated) for known games, without you (the user) really needing to do much at all. You can disable this on a per game basis using the "Use Remote Settings" flag from a games context (right click) menu in LaunchBox, in the "PCSX2 Configurator" Sub Menu. 

To create your own config for a game that does not have known settings, you simply use the "Configure" option from within LaunchBox, and then PCSX2 will be started allowing you to configure the settings that you want for that game. You can also use "Configure" to make changes to an existing config (Including Downloaded Ones), though currently any changes made will be overwritten if the config for the game is updated (The Plugin will notify you if this is the case).

Settings
-------------------------------------------------------------------------------
The Plugin Settings Menu is located in LaunchBox's tools Menu. All settings with "Current" in the name are referring to how the base instance of PCSX2 is currently configured. The default settings make sense in most cases, and these settings should only really be tweaked if you fully understand what the setting actually does. Settings are applied by simply closing the settings window. These Settings will make no meaningful difference to downloaded Configs, and are in-part personal preference.

Each of the options is explained breifly below:

**Use Independant Memory Cards?** (default: On) - create a new formatted memory card (In Slot 1) for each game that is configured

**Use Current Plugin & Bios Files?** (default: On) - retains current plugin and bios selection when configuring a game (un-checking this will require them to be re-configured for each game)

**Use Current GS Window Settings?** (default: On) - retains current GS Window Settings (Vsync/Aspect Ratio/Custom Window Size)

**Use Current Program Log Settings?** (default: On) - retains current Program Log Settings, The main purpose of this is to show/hide the console window

**Use Current Folder Settings?** (default: Off) - retains current Folder Settings, This is generally only needed if default folders are not in use (e.g. your folders are in documents, but PCSX2 is in program files)

**Use Current VM Settings?** (default: On) - retains current VM Settings, This Mostly denotes what custom patches/hacks/cheats are enabled, and will usually be overidden by downloaded configs

**Use Current GSdx Plugin Settings?** (default: On)	- retains current GSdx (Graphics Plugin) Settings, This will be overidden by downloaded configs

**Use Current SPU2-X Plugin Settings?** (default : Off) -	retains current SPU2-X (Sound Plugin) Settings, Not needed in most cases, may be overidden by downloaded configs

**Use Current LilyPad Plugin Settings?** (default: Off) - retains current LilyPad (Controller Plugin) Settings, Not needed in most cases, could be useful if you like to play with a non-standard controller layout for some games

**Configuartions Directory** (default: $PCSX2Dir\inis) - the loaction where per-game configurations are stored, This is in the Emulators ini folder by default though can be changed. you will need to chnage this if your PCSX2 Directory is in Program Files and you are not running LB as Admin

This file also contains hidden settings:

**AllowAllSettings** - ensures Presets are disabled and Game Fixes and Speed Hacks are enabled in PCSX2

**EnableRemoteSettingsByDefault** - Will Disable the "Use Remote Settings" flag for all newly imported games

Alpha Testing & Bug Reporting
-------------------------------------------------------------------------------
As alluded to earlier this is an Alpha version (I originally wanted to call this release Beta but I don't have that much confidence in my code) and still has a multitude of issues. Most of these are known quantities and are listed in the "Issues" section on Github. If you have experience or notes about any of the known issues, or you feel that a known issue is affecting your experience, please leave a comment on the issue to start a discuission about a potential fix. If you find any other issues or have any other problems please post on the LaunchBox forums or contact me directly. All issues will be fixed/resolved eventually and then this will finally be repackaged as a stable 1.0 release.

Credit & History
-------------------------------------------------------------------------------
This plugin was created by myself (alec100_94), and began life sometime in June 2017, when I was frustrated with not having a sensible solution for using different Settings with PS2 Games that integrated nicely with LaunchBox. Later that year fellow LaunchBox user Zombeaver messaged me, asking if it was possible to use his pre-made PCSX2 configs with my plugin, so I decided to work with him and add full support for them as a part of the plugin. The plugin has since gained popularity through the LaunchBox community and has evolved into something much more than what is was originally envisoned as. 

A lot of time and effort has gone into creating this plugin and I would like to say a massive thank you to everyone that has given it a shot. I would also like to say a special thank you to a few (awesome) members of the LaunchBox community who have been great and helped shape this plugin into what it currently is:
Zombeaver
Spectral
ckp
neil9000
kmoney

