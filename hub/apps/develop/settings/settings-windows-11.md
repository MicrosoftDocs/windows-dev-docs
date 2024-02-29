---
title: Reference for Windows 11 settings
description: This article provides reference information for accessing settings values on devices running Windows 11.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Reference for Windows 11 settings

Overview content TBD. Link to [Settings back up and restore overview](index.md). Link to [Cloud Data Store Settings Reader Tool (readsettingdata.exe)](readsettingsdata-exe.md). Link to [Reference for common Windows settings](settings-common.md).


## Ease cursor movement across displays 

When enabled, Windows makes it easier to move your cursor and windows between displays by letting your cursor jump over areas where it would previously get stuck.

This setting is single-instance.

### Type: Windows.Data.Settings.DisplaySettings.MultipleDisplays

### MultipleDisplays Properties

| Name | Type | Description |
|------|------|-------------|
| rememberWindowLocationsPerMonitorConnection | `nullable<bool>` | Remember window locations based on monitor connection. |
| minimizeWindowsOnMonitorDisconnect | `nullable<bool>` | Minimize windows when a monitor is disconnected. |
| easeCursorMovementBetweenDisplays | `nullable<bool>` | Ease cursor movement between displays. |


**TBD - Document Multiple Displays as reg keys rather than schema - waiting for info about the following:**

Copy of what the user has saved in user profile for system-wide parameter SPI_GETCURSORDEADZONEJUMPING

### Registry values under Computer\HKEY_USERS\{The users generated unique id}\Software\Microsoft\Windows\CurrentVersion\CloudStore\Store\DefaultAccount\Current\default$windows.data.settings.displaysettings.multipledisplays 

Computer\HKEY_USERS\{The users generated unique id}\Software\Microsoft\Windows\CurrentVersion\CloudStore\Store\DefaultAccount\Current\default$windows.data.settings.displaysettings.multipledisplays

Name: (Default)
Type: REG_SZ
Data: (value not set)

Name: Data
Type: REG_Binary
Data: {binary data}


## Gaming: Game Bar, Game Mode, Gaming Shortcuts

This setting controls settings related to gaming and controls such as Game bar and gaming shortcuts.

This setting is single instance.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR

**TBD - ALL DESCRIPTIONS TENTATIVE**

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| VKMSaveHistoricalVideo | REG_DWORD | 0 or 1 | Toggles save historical video enabled. |
| VKMTakeScreenshot | REG_DWORD | 0 or 1 | Toggles take screenshot enabled. |
| VKTakeScreenshot | REG_DWORD | ASCII Value for keys | Key binding for save historical screenshot. |
| VKSaveHistoricalVideo | REG_DWORD | ASCII Value for keys | Key binding for take screenshot. |
| VKMToggleBroadcast | REG_DWORD | 0 or 1 | Toggles broadcast enabled. |
| VKToggleBroadcast | REG_DWORD | ASCII Value for keys | Key binding for take broadcast. |
| VKMToggleCameraCapture | REG_DWORD | 0 or 1 | Toggles camera capture enabled. |
| VKToggleCameraCapture | REG_DWORD | ASCII Value for keys | Key binding for camera capture. |
| VKMToggleGameBar | REG_DWORD | 0 or 1 | Toggles game bar enabled. |
| VKToggleGameBar | REG_DWORD | ASCII Value for keys | Key binding for game bar. |
| VKMToggleMicrophoneCapture | REG_DWORD | 0 or 1 | Toggles microphone capture enabled. |
| VKToggleMicrophoneCapture | REG_DWORD | ASCII Value for keys | Key binding for microphone capture. |
| VKMToggleRecording | REG_DWORD | 0 or 1 | Toggles recording enabled. |
| VKToggleRecording | REG_DWORD | ASCII Value for keys | Key binding for recording. |
| VKMToggleRecordingIndicator | REG_DWORD | 0 or 1 | Toggles recording indicator enabled. |
| VKToggleRecordingIndicator | REG_DWORD | ASCII Value for keys | Key binding for recording indicator. |

### Registry values under HKCU\Software\Microsoft\GameBar

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| UseNexusForGameBarEnabled | REG_DWORD | 0 or 1 | TBD |
| AutoGameModeEnabled | REG_DWORD | 0 or 1 | TBD |

## Lunar calendar

**TBD - Lunar calendar appears in the "legacy" Word doc, so it probably should go in the "Common" settings page**

Settings related to the lunar Calendar in the task bar. 

This setting is single-instance.

### Type: Windows.Data.LunarCalendar Structure

The scope of this type is per user.

### Type: Windows.Data.LunarCalendarPerDevice Structure

This type inherits from **LunarCalendar**. The scope of this type is per device.

### LunarCalendarPerDevice Properties

| Name | Type | Description |
|------|------|-------------|
| languageType   | **LunarCalendarLanguageType** | A member of the **LunarCalendarLanguageType** enumeration. The default value is **Default**. |

### Type: Windows.Data.LunarCalendarLanguageType Enumeration

### Values

| Name | Value | Description |
|------|-------|---------|
| Default | 0   | The default lunar calendar configuration. |
| None   | 1 | No lunar calendar. |
| SimplifiedChinese  | 2 | The Simplified Chinese lunar calendar. |
| TraditionalChinese | 2   | The Traditional Chinese calendar. |


## Text Input

This setting helps to chose a theme for touch keyboard, voice typing, emoji and more, and input method editors.

### Registry values under HKCU\Software\Microsoft\TabletTip\1.7

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| IsKeyBackgroundEnabled | REG_DWORD | 0 or 1 | Indicates if key background is enabled. |
| KeyLabelSize | REG_DWORD | Min value: 0, Max value: 2 | Key label size. |
| UserKeyboardScalingFactor | REG_DWORD | Min value: 1, Max value: 1000 | User keyboard scaling factor. |
| SelectedThemeIndex | REG_DWORD | Min value: 0, Max value: 15 | Selected theme index. |
| SelectedThemeName | REG_SZ | One of the following values: "LightTheme", "DarkTheme", "ColorPopTheme", "BlackWhiteTheme", "PoppyRedTheme", "IceBlueTheme", "PlatinumTheme", "TangerineTidesTheme", "LilacRiverTheme", "SilkyDawnTheme", "IndigoBreezeTheme", "PinkBlueTheme", "GreenPurpleTheme", "PinkOrangeTheme", "CustomTheme" | Selected theme name. |
| ThemeDataVersion | REG_DWORD | 2 | Theme data version. |


## Typing

This setting contains toggles and other settings related to touch keyboard, text suggestions and preferences.

This setting is single-instance.


### Registry values under HKCU\Software\Microsoft\input\Settings

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| EnableAutoShiftEngage | REG_DWORD | 1 or 0 | Enables auto shift engage. |
| EnableDoubleTapSpace | REG_DWORD | 1 or 0 | Enables double-tap space. |
| EnableAutocorrection | REG_DWORD | 1 or 0 | Enables auto-correction. |
| IsVoiceTypingKeyEnabled | REG_DWORD | 1 or 0 | Indicates if voice typing key is enabled. |
| MultilingualEnabled | REG_DWORD | 1 or 0 | Indicates in multingual is enabled. |
| EnableHwkbTextPrediction | REG_DWORD | 1 or 0 | ASCII Value for keys. |

## USB - notifications

This setting controls toggles such as connection notifications, battery saver and other notifications related to charging of PC.

This setting is single-instance.

### Registry values under HKCU\Software\Microsoft\Shell\USB

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| NotifyOnUsbErrors | REG_DWORD | 0 or 1 | Enables notifications for USB errors. |
| NotifyOnWeakCharger | REG_DWORD | 0 or 1 | Enables notifications for weak charger detected. |


