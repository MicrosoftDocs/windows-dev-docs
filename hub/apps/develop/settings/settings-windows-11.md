---
title: Reference for Windows 11 settings
description: This article provides reference information for accessing settings values on devices running Windows 11.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Reference for Windows 11 settings

Overview content TBD. Link to [Settings back up and restore overview](index.md). Link to [Cloud Data Store Settings Reader Tool (readsettingdata.exe)](readsettingsdata-exe.md).


## Ease cursor movement across displays 

When enabled, Windows makes it easier to move your cursor and windows between displays by letting your cursor jump over areas where it would previously get stuck.

### Namespace: Windows.Data.Settings.DisplaySettings

### Type: MultipleDisplays

### Properties

| Name | Type | Description |
|------|------|-------------|
| rememberWindowLocationsPerMonitorConnection | `nullable<bool>` | Remember window locations based on monitor connection. |
| minimizeWindowsOnMonitorDisconnect | `nullable<bool>` | Minimize windows when a monitor is disconnected. |
| easeCursorMovementBetweenDisplays | `nullable<bool>` | Ease cursor movement between displays. |


** TBD - Document Multiple Displays as reg keys rather than schema - waiting for info about the following:

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

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR

**TBD - ALL DESCRIPTIONS TENTATIVE**

| Registry key | Type | Value | Description |
|---------------|------|-------|-------------|
| VKMSaveHistoricalVideo | REG_DWORD | 0 or 1 | Toggles save historical video enabled. |
| VKMTakeScreenshot | REG_DWORD | 0 or 1 | Toggles take screenshot enabled. |
| VKTakeScreenshot | REG_DWORD | ASCII Value for keys | Key binding for take screenshot. |
| VKSaveHistoricalVideo | REG_DWORD | ASCII Value for keys |
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

| Registry key | Type | Value | Description |
|---------------|------|-------|------------|
| UseNexusForGameBarEnabled | REG_DWORD | 0 or 1 | TBD |
| AutoGameModeEnabled | REG_DWORD | 0 or 1 | TBD |

## Lunar calendar

Settings related to the lunar Calendar in the task bar. 

### Type: Windows.Data.LunarCalendar Structure

The scope of this type is per user.

### Type: Windows.Data.LunarCalendarPerDevice Structure

This type inherits from **LunarCalendar**. The scope of this type is per device.

### Properties

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