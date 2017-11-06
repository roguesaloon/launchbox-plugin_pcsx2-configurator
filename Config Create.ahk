#NoTrayIcon

; Takes the Config Directory and Path of the Emualtor as Parameters Both are Absolute
configDir = %1%
emulatorPath = %2%

; Sets the Emulator Directory, and Initialises Default (Base) Config Directory
emulatorDir:= SubStr(emulatorPath, 1, InStr(emulatorPath, "\", false, 0)-1)
defaultDir:= "none"

; Sets the game name from the Config Directory Name
gameName := SubStr(configDir, InStr(configDir, "\", false, 0)+1)

; Sets the Default Config Directory
; Based on whether a config file can be found in expected location
IfExist, %emulatorDir%\inis\PCSX2_ui.ini 
	defaultDir = %emulatorDir%\inis
IfExist, %emulatorDir%\inis_1.4.0\PCSX2_ui.ini
	defaultDir = %emulatorDir%\inis_1.4.0
	
if(defaultDir == "none")
{
	MsgBox, 0, PCSX2 Configurator, No Default PCSX2 Configuration was found`nThe Plugin Will now Stop`nPlease contact the developer for support
	exit
}

; Sets the location of the base and new config files
configUiFile = %configDir%\PCSX2_ui.ini
defaultUiFile = %defaultDir%\PCSX2_ui.ini

; Creates The Config Directory, and (UI) Config File
FileCreateDir, %configDir%
FileAppend, ,%configUiFile%

; Add Dummy General Settings Header to The UI File As Ini Read/Write Workaround
IniWrite, `n , %configUiFile%, GeneralSettings

; All UI File Settings From Current Config

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

; Ask settings File, If Idependant Memory Cards are Enabled
IniRead, useIndependantMemoryCards, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseIndependantMemoryCards

; If yes add a new memory card to the config file (named after game)
if(useIndependantMemoryCards == "true")
{
	IniWrite, enabled, %configUiFile%, MemoryCards, Slot1_Enable
	IniWrite,% StrReplace(gameName, " ", "") . ".ps2", %configUiFile%, MemoryCards, Slot1_Filename
}

; Ask settings File, If All Settings Should Be Enabled
IniRead, allowAllSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, AllowAllSettings

if(allowAllSettings == "true")
{
	; Enable Game Fixes and Speed Hacks, With Presets Disabled
	IniWrite, disabled, %configUiFile%, GeneralSettings, EnablePresets
	IniWrite, enabled, %configUiFile%, GeneralSettings, EnableGameFixes
	IniWrite, enabled, %configUiFile%, GeneralSettings, EnableSpeedHacks
}

; VM Settings

; Ask Settings File, if VM Settings are to be imported
IniRead, useCurrentVMSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentVMSettings

; If yes copy over the current VM file to our config directory
if(useCurrentVMSettings == "true")
{
	FileCopy, %defaultDir%\PCSX2_vm.ini, %configDir%\PCSX2_vm.ini, 1
}

; Do Same for specific plugin settings

; GSdx
IniRead, useCurrentGSdxPluginSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentGSdxPluginSettings

if(useCurrentGSdxPluginSettings == "true")
{
	FileCopy, %defaultDir%\GSdx.ini, %configDir%\GSdx.ini, 1
}

; LilyPad
IniRead, useCurrentLilyPadPluginSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentLilyPadPluginSettings

if(useCurrentLilyPadPluginSettings == "true")
{
	FileCopy, %defaultDir%\LilyPad.ini, %configDir%\LilyPad.ini, 1
}
	
; Is it a game in the Remote Database of Configs
isKnownGame(game, ByRef url)
{
	game := StrReplace(game, " ", "%20")
	StringLower, game, game
	url := "https://github.com/roguesaloon/launchbox-plugin_pcsx2-configurator/tree/master/Game%20Configs/"
	url = %url%%game%
	
	whr := ComObjCreate("WinHttp.WinHttpRequest.5.1")
	whr.Open("GET", url, true)
	whr.Send()
	whr.WaitForResponse()
	response := whr.ResponseText
	
	if(response == "Not Found")
	{
		return false
	}
	else
	{
		return true
	}
}

useRemoteSettings := false
remoteSettingsUrl = null

; If Yes Then ask User if they want to use the remote settings
if(isKnownGame(gameName, remoteSettingsUrl))
{
	MsgBox, 4, PCSX2 Configurator, You are creating a config for a known game`nWould you like to import optimized settings for this game?`nSettings can still be altered later
	IfMsgBox, Yes
		useRemoteSettings := true
}

; Then download them (Using SVN), overwriting what is there
if(useRemoteSettings)
{
	remoteSettingsUrl := StrReplace(remoteSettingsUrl, "/tree/master/", "/trunk/")
	RunWait, %A_ScriptDir%\..\..\SVN\bin\svn.exe export %remoteSettingsUrl% --force, %configDir%\.., HIDE
	
	; Always enable Game Fixes and Speed Hacks, With Presets Disabled For Remote Configs
	IniWrite, disabled, %configUiFile%, GeneralSettings, EnablePresets
	IniWrite, enabled, %configUiFile%, GeneralSettings, EnableGameFixes
	IniWrite, enabled, %configUiFile%, GeneralSettings, EnableSpeedHacks

	; If there is a UI Tweak File
	uiTweakFile := configDir . "\PCSX2_ui-tweak.ini"
	if(FileExist(uiTweakFile))
	{
	
		; Parse it and append changes to UI Config File
		IniRead, tweakSections, %uiTweakFile%
		
		Loop, Parse, tweakSections, `n
		{
			IniRead, tweakSection, %uiTweakFile%, %A_LoopField%
			
			sectionName = %A_LoopField%
			
			Loop, Parse, tweakSection, `n
			{
				key := SubStr(A_LoopField, 1, InStr(A_LoopField, "=")-1)
				value := SubStr(A_LoopField, InStr(A_LoopField, "=")+1)
				
				IniWrite, %value%, %configUiFile%, %sectionName%, %key%
			}
		}
		
		FileDelete, %uiTweakFile%
	}
	
	; Moves Included Cheats to emulators Cheats folder
	FileMove, %configDir%\*.pnach, %emulatorDir%\cheats\*.pnach
}

; Remove The General Settings Header From UI File
FileRead, configUiFileText, %configUiFile%
configUiFileText := StrReplace(configUiFileText, "[GeneralSettings]`r`n", "")
FileDelete, %configUiFile%
FileAppend, %configUiFileText%, %configUiFile%

; After setting everything up, run the emulator with config directory for initial configuration
Run, %emulatorPath% --cfgpath "%configDir%"