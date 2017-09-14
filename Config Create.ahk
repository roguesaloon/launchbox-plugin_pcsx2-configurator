﻿#NoTrayIcon

; Takes the Config Directory and Path of the Emualtor as Parameters Both are Absolute
configDir = %1%
emulatorPath = %2%

; Sets the Emulator Directory, and Initialises Default (Base) Config Directory
emulatorDir:= SubStr(emulatorPath, 1, InStr(emulatorPath, "\", false, 0)-1)
defaultDir:= "none"

; Sets the game name from the Config Directory Name
gameName := SubStr(configDir, InStr(configDir, "\", false, 0)+1)
gameNameNoSpace := StrReplace(gameName, " ", "")

; Sets the Default Config Directory
; Based on whether a config file can be found in expected location
IfExist, %emulatorDir%\inis\PCSX2_ui.ini 
	defaultDir = %emulatorDir%\inis
IfExist, %emulatorDir%\inis_1.4.0\PCSX2_ui.ini
	defaultDir = %emulatorDir%\inis_1.4.0

; Sets the location of the base and new config files
configUiFile = %configDir%\PCSX2_ui.ini
defaultUiFile = %defaultDir%\PCSX2_ui.ini

; Creates The Config Directory, and (UI) Config File
FileCreateDir, %configDir%
FileAppend, ,%configUiFile%

; If a base config directory was found
if (defaultDir != "none")
{
	; Ask Settings File, If Program Log Settings are to be imported
	IniRead, useCurrentLogSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentLogSettings
	
	; If they are copy them from the base config and write them to our new file
	if(useCurrentLogSettings == "true")
	{
		IniRead, section, %defaultUiFile%, ProgramLog
		IniWrite, %section%, %configUiFile%, ProgramLog
	}
	
	; Repeat for other settings in UI config file

	IniRead, useCurrentFolderSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentFolderSettings
	
	if(useCurrentFolderSettings == "true")
	{
		IniRead, section, %defaultUiFile%, Folders
		IniWrite, %section%, %configUiFile%, Folders
	}

	IniRead, useCurrentFileSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentFileSettings
	
	if(useCurrentFileSettings == "true")
	{
		IniRead, section, %defaultUiFile%, Filenames
		IniWrite, %section%, %configUiFile%, Filenames
	}

	IniRead, useCurrentWindowSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentWindowSettings
	
	if(useCurrentWindowSettings == "true")
	{
		IniRead, section, %defaultUiFile%, GSWindow
		IniWrite, %section%, %configUiFile%, GSWindow
	}

	; Ask Settings File, if VM Settings are to be imported
	IniRead, useCurrentVMSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentVMSettings

	; If yes copy over the current VM file to our config directory
	if(useCurrentVMSettings == "true")
	{
		FileCopy, %defaultDir%\PCSX2_vm.ini, %configDir%\PCSX2_vm.ini, 1
	}

	; Do Same for specific plugin settings

	IniRead, useCurrentGSdxPluginSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentGSdxPluginSettings

	if(useCurrentGSdxPluginSettings == "true")
	{
		FileCopy, %defaultDir%\GSdx.ini, %configDir%\GSdx.ini, 1
	}

	IniRead, useCurrentLilyPadPluginSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentLilyPadPluginSettings

	if(useCurrentLilyPadPluginSettings == "true")
	{
		FileCopy, %defaultDir%\LilyPad.ini, %configDir%\LilyPad.ini, 1
	}
}

; Ask settings File, If Idependant Memory Cards are Enabled
IniRead, useIndependantMemoryCards, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseIndependantMemoryCards

; If yes add a new memory card to the config file (named after game)
if(useIndependantMemoryCards == "true")
{
	IniWrite, enabled, %configUiFile%, MemoryCards, Slot1_Enable
	IniWrite, %gameNameNoSpace%.ps2, %configUiFile%, MemoryCards, Slot1_Filename
}

; After setting everything up, run the emulator with config directory for initial configuration
Run, %emulatorPath% --cfgpath "%configDir%"