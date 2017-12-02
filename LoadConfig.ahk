#NoTrayIcon

; Takes the Config Directory and Path of the Emulator as Parameters Both are Absolute
configDir = %1%
emulatorPath = %2%

if(configDir == "")
{
	MsgBox, 0, PCSX2 Configurator, This Script Should Only Be Run From LaunchBox Through`nThe PCSX2 Configurator Plugin And Will Now Close
	ExitApp
}

romName = %3%

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
	RegRead, settingsFolder, HKEY_CURRENT_USER\Software\PCSX2, SettingsFolder
	
	if(settingsFolder != "")
	{
		defaultDir = settingsFolder
	}
	else
	{
		MsgBox, 0, PCSX2 Configurator, No Default PCSX2 Configuration was found`nThe Plugin Will now Stop`nPlease contact the developer for support
		ExitApp
	}
}

; Sets the location of the base and new config files
configUiFile = %configDir%\PCSX2_ui.ini
defaultUiFile = %defaultDir%\PCSX2_ui.ini

; Config Files/Dirs for Rocket Launcher (Matching Rom Name)
romConfigUiFile = null
if(romName != "")
{
	romConfigDir := SubStr(configDir, 1, InStr(configDir, "\", false, 0))
	romConfigDir = %romConfigDir%%romName%
	romConfigUiFile = %romConfigDir%\PCSX2_ui.ini
}

; If Config Has Already Been Created And Left Control Not Pressed
if((FileExist(configUiFile) || FileExist(romConfigUiFile)) && !GetKeyState("LControl"))
{
	; Start The Emulator For Configuration
	GoSub, StartEmulator
}

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
	MemCardName := StrReplace(gameName, " ", "") . ".ps2"
	MemCardFile = %emulatorDir%\memcards\%MemCardName%
	
	IniWrite, enabled, %configUiFile%, MemoryCards, Slot1_Enable
	IniWrite, %MemCardName%, %configUiFile%, MemoryCards, Slot1_Filename
	
	; If there is No Mem Card for This Game
	if(!FileExist(MemCardFile))
	{
		; Create a Fresh One (Formatted)
		RunWait, %A_ScriptDir%\..\..\7-Zip\7z.exe e "%A_ScriptDir%\Assets\Mcd.7z" -o"%emulatorDir%\memcards", %A_ScriptDir%\..\..\7-Zip, Hide
		FileMove, %emulatorDir%\memcards\Mcd.ps2, %MemCardFile%
	}
}

; Ask settings File, If All Settings Should Be Enabled
IniRead, allowAllSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, AllowAllSettings

; If yes then all the settings (GameFixes, SpeedHacks) and disable prests
if(allowAllSettings == "true")
{
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

;SPU2-X
IniRead, useCurrentSPU2xPluginSettings, %A_ScriptDir%\Settings.ini, PCSX2_Configurator, UseCurrentSPU2xPluginSettings

if(useCurrentSPU2xPluginSettings == "true")
{
	FileCopy, %defaultDir%\SPU2-X.ini, %configDir%\SPU2-X.ini, 1
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
	game := StrReplace(game, "&", "and")
	StringLower, game, game
	url := "https://github.com/roguesaloon/launchbox-plugin_pcsx2-configurator/tree/master/Game%20Configs/"
	url = %url%%game%
	
	whr := ComObjCreate("WinHttp.WinHttpRequest.5.1")
	ComObjError(false)
	whr.Open("GET", url, true)
	whr.Send()
	whr.WaitForResponse()
	response := whr.ResponseText

	if(response == "Not Found" || response == "")
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
	UseRemoteSettingsPopup()
	{
	global
	
		Gui, New, , PCSX2_Configurator
		Gui, Add, Picture, w400 h200, %A_ScriptDir%\Assets\knowngame.png
		Gui -caption +lastfound +alwaysontop
		Gui, Color, EEAA99
		WinSet, TransColor, EEAA99
		Gui, Add, Text, BackgroundTrans x218 y154 w60 h30 gYes
		Gui, Add, Text, BackgroundTrans x304 y154 w60 h30 gNo
		Gui, Add, Hotkey, Hide vHotKey gHotkeys
		Gui, Add, Text, BackgroundTrans x374 y16 w16 h16 gGuiClose
		Gui, Show
		OnMessage(0x201, "WM_LBUTTONDOWN")

		WinWaitClose, PCSX2_Configurator
		return %flag%
		
		Hotkeys:
		{
			if(Hotkey = "y")
			{
				GoSub, Yes
				return
			}
			
			if(Hotkey = "n")
			{
				GoSub, No
				return
			}
			
			return
		}
		
		Yes:
		{
			flag := true
			Gui, Submit
			return
		}

		No:
		{
			flag := false
			Gui, Submit
			return
		}
		
		GuiClose:
		{
			Gui, Cancel
			Process, Close, %controllerMapperProcess%
			FileDelete, %configUiFile%
			ExitApp
			return
		}
	}

	WM_LBUTTONDOWN()
	{
		PostMessage, 0xA1, 2
	}
	
	useRemoteSettings := UseRemoteSettingsPopup()
}

; Then download them (Using SVN), overwriting what is there
if(useRemoteSettings)
{
	remoteSettingsUrl := StrReplace(remoteSettingsUrl, "/tree/master/", "/trunk/")
	RunWait,%comspec% /c ""%A_ScriptDir%\..\..\SVN\bin\svn.exe" export %remoteSettingsUrl% --force > "%configDir%\SVNout.txt"", %configDir%\.., Hide
	
	IfInString, gameName, &
		FileMoveDir, % StrReplace(configDir, "&", "and"), %configDir%, 2
	
	FileRead, SVNout, %configDir%\SVNout.txt
	FileDelete, %configDir%\SVNout.txt
	
	if(!InStr(SVNout, "Exported revision"))
	{
		MsgBox, 0, PCSX2 Configurator, There was a Problem Downloading The Config`nNo Config Will Be Downloaded
		GoSub, StartEmulator
	}
	
	; Always allow All Settings For Remote Configs
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

StartEmulator:

	; If Using Rocket Launcher
	if(romName != "")
	{
		FileMoveDir, %configDir%, %romConfigDir%, 2
		configDir = %romConfigDir%
	}
	
	; After setting everything up, run the emulator with config directory configuration
	RunWait, %emulatorPath% --cfgpath "%configDir%",,, emulatorProcess
	ExitApp
	
	^Esc:: Process, Close, %emulatorProcess%