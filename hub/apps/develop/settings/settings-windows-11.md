---
title: Reference for Windows 11 settings
description: This article provides reference information for accessing settings values on devices running Windows 11.
ms.date: 04/14/2026
ms.topic: how-to
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Reference for Windows 11 settings

The information provided on this page includes details for accessing the status of Windows Backup and Restore settings that are supported in Windows 11. This public documentation ensures effective data portability by providing third-party developers with a streamlined process to access the data. Settings that are supported on both Windows 10 and Windows 11 are documented in [Reference for common Windows settings](settings-common.md).

Settings status is accessed in one of two ways:

1. Via the Windows registry: For settings below that include registry details, please use that information to access the settings.
1. Via the Cloud Data Store Reader tool. These settings must be extracted from a data store to be readable. If the setting below does not list registry details, then the settings must be extracted using the Cloud Data Store Reader tool. For information on how to use this tool, see [Cloud Data Store Settings Reader Tool (readCloudDataSettings.exe)](readclouddatasettings-exe.md).

## Cellular

User preferences that customize the Windows behavior when a cellular connection is available.

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\CellularFailover

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| :::no-loc text="AllowFailover"::: | REG_DWORD | 0 or 1 | Use cellular whenever Wi-Fi is poor.  |

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\SubscriptionManager\\&lt;ICCID&gt;\\&lt;IMSI&gt;

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| :::no-loc text="UserCost"::: | REG_DWORD | 0 or 1 | Maps the metered/unmetered state to each IMSI. 0: Unmetered. 1: Metered.   |

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WwanSvc\DisallowAutoConnectByClient

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| :::no-loc text="<interface GUID>":::| REG_DWORD | 0 or 1 | Let Windows keep the device connected to cellular. |

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WwanSvc\RoamingPolicyForPhone\\&lt;interface GUID&gt;

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| :::no-loc text="InternetAlwaysOn"::: | REG_DWORD | 0 or 1 | Roaming or no roaming. When entering a roaming area, your data connection will be turned off if roaming is not allowed. |

## Gaming: Game Bar, Game Mode, Gaming Shortcuts

This setting controls settings related to gaming and controls such as Game bar and gaming shortcuts.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| :::no-loc text="VKMSaveHistoricalVideo"::: | REG_DWORD | 0 or 1 | Toggles save historical video enabled. |
| :::no-loc text="VKMTakeScreenshot"::: | REG_DWORD | 0 or 1 | Toggles take screenshot enabled. |
| :::no-loc text="VKTakeScreenshot"::: | REG_DWORD | ASCII Value for keys | Key binding for take screenshot. |
| :::no-loc text="VKSaveHistoricalVideo"::: | REG_DWORD | ASCII Value for keys | Key binding for save historical video. |
| :::no-loc text="VKMToggleBroadcast"::: | REG_DWORD | 0 or 1 | Toggles broadcast enabled. |
| :::no-loc text="VKToggleBroadcast"::: | REG_DWORD | ASCII Value for keys | Key binding for take broadcast. |
| :::no-loc text="VKMToggleCameraCapture"::: | REG_DWORD | 0 or 1 | Toggles camera capture enabled. |
| :::no-loc text="VKToggleCameraCapture"::: | REG_DWORD | ASCII Value for keys | Key binding for camera capture. |
| :::no-loc text="VKMToggleGameBar"::: | REG_DWORD | 0 or 1 | Toggles game bar enabled. |
| :::no-loc text="VKToggleGameBar"::: | REG_DWORD | ASCII Value for keys | Key binding for game bar. |
| :::no-loc text="VKMToggleMicrophoneCapture"::: | REG_DWORD | 0 or 1 | Toggles microphone capture enabled. |
| :::no-loc text="VKToggleMicrophoneCapture"::: | REG_DWORD | ASCII Value for keys | Key binding for microphone capture. |
| :::no-loc text="VKMToggleRecording"::: | REG_DWORD | 0 or 1 | Toggles recording enabled. |
| :::no-loc text="VKToggleRecording"::: | REG_DWORD | ASCII Value for keys | Key binding for recording. |
| :::no-loc text="VKMToggleRecordingIndicator"::: | REG_DWORD | 0 or 1 | Toggles recording indicator enabled. |
| :::no-loc text="VKToggleRecordingIndicator"::: | REG_DWORD | ASCII Value for keys | Key binding for recording indicator. |

### Registry values under HKCU\Software\Microsoft\GameBar

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="UseNexusForGameBarEnabled"::: | REG_DWORD | 0 or 1 | Key associated with toggle Gaming -> Game Bar -> "Allow your controller to open Game Bar" |
| :::no-loc text="AutoGameModeEnabled"::: | REG_DWORD | 0 or 1 | Key associated with toggle Gaming -> Game Mode -> Game Mode. |

## Personalization - Start - Layout - Pins and recommendations

Specifies the start layout type.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="Start_Layout"::: | REG_DWORD | 0 [Default], 1 [More Pins], 2 [More Recommendations] | The start layout. |

## Personalization - Start - Show all pins by default

Specifies whether all pins show by default in Start.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Start

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="ShowAllPinsList"::: | REG_DWORD | 0 or 1 | Specifies if showing all pins by default |

## Personalization - Start - Recommendations

Specifies whether recommendations for tips, shortcuts, new apps and more are shown.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="Start_IrisRecommendations"::: | REG_DWORD | 0 or 1 | Specifies whether recommendations are enabled. |

## Personalization - Taskbar - Alignment

This setting sets the alignment of the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarAl

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Al"::: | REG_SZ | 0 or 1 | Specifies the taskbar alignment. 0 is left aligned. 1 is center aligned. |

## Personalization - Taskbar - Autohide

This setting sets the auto-hide behavior of the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3\

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Autohide"::: | REG_BINARY | A binary blob. | This is an opaque binary blob copied from the following location on the backed up. |
| :::no-loc text="SystemSettings_Taskbar_Autohide"::: | REG_BINARY | A binary blob. | This is an opaque binary blob copied from the following location on the backed up. |

## Personalization - Taskbar - Combine buttons

This setting enables combining buttons and hiding labels on the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarGlomLevel

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_GroupingMode"::: | REG_SZ | 0, 1, or 2 | 0: Always, 1: When taskbar is full, 2: Never. |

## Personalization - Taskbar - Flashing

This setting enables flashing for taskbar apps.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarFlashing

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Flashing"::: | REG_SZ | 0 or 1 | Enables flashing for taskbar apps. |

## Personalization - Taskbar - Share window

This setting enables sharing any window from taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarSn

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Sn"::: | REG_SZ | 0 or 1 | Enables sharing any window from taskbar. |

## Personalization - Taskbar - Show desktop

This setting enables showing the desktop by clicking the far corner of the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarSd

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Sd"::: | REG_SZ | 0 or 1 | Enables showing the desktop by clicking the far corner of the taskbar. |

## Personalization - Taskbar - Show recent searches

This setting enables showing recent searches when hovering on the search icon in taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Feeds\DSB\OpenOnHover

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Sh"::: | REG_SZ | 0 or 1 | Enables showing recent searches when hovering on the search icon in taskbar. |

## Personalization - Taskbar - Task view

This setting shows or hides the Task View button on the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowTaskViewButton

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_TaskView"::: | REG_SZ | 0 or 1 | Specifies whether the task view button is shown on the taskbar. |

## Personalization - Taskbar - Widgets button

This setting shows or hides the Widgets button on the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarDa

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="SystemSettings_DesktopTaskbar_Da"::: | REG_SZ | 0 or 1 | Specifies whether the Widgets button is shown on the taskbar. |

## Personalization - Themes

This setting is used to set a personalized theme.

### Type: Windows.Data.PersonalizationThemes.CurrentThemeType enumeration

### CurrentThemeType Values

| Name | Value | Description |
|------|-------|---------|
| `InboxTheme` | 0   | In-box theme. |
| `ContrastTheme` | 1   | Contrast theme. |

### Type: Windows.Data.PersonalizationThemes.CurrentThemeType structure

#### CurrentThemeType Properties

| Name | Type | Description |
|------|------|-------------|
| `type` | **`CurrentThemeType`** | The current theme type. |
| `basePersonalizationThemeName` | `wstring` | The name of the current inbox theme applied in the system. The user may have done customization on top of this inbox theme.  |

## Text Input

This setting helps to choose a theme for touch keyboard, voice typing, emoji and more, and input method editors.

### Registry values under HKCU\Software\Microsoft\TabletTip\1.7

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="IsKeyBackgroundEnabled"::: | REG_DWORD | 0 or 1 | Indicates if key background is enabled. |
| :::no-loc text="KeyLabelSize"::: | REG_DWORD | Min value: 0, Max value: 2 | Key label size. |
| :::no-loc text="UserKeyboardScalingFactor"::: | REG_DWORD | Min value: 1, Max value: 1000 | User keyboard scaling factor. |
| :::no-loc text="SelectedThemeIndex"::: | REG_DWORD | Min value: 0, Max value: 15 | Selected theme index. |
| :::no-loc text="SelectedThemeName"::: | REG_SZ | One of the following values: ":::no-loc text="LightTheme":::", ":::no-loc text="DarkTheme":::", ":::no-loc text="ColorPopTheme":::", ":::no-loc text="BlackWhiteTheme":::", ":::no-loc text="PoppyRedTheme":::", ":::no-loc text="IceBlueTheme":::", ":::no-loc text="PlatinumTheme":::", ":::no-loc text="TangerineTidesTheme":::", ":::no-loc text="LilacRiverTheme":::", ":::no-loc text="SilkyDawnTheme":::", ":::no-loc text="IndigoBreezeTheme":::", ":::no-loc text="PinkBlueTheme":::", ":::no-loc text="GreenPurpleTheme":::", ":::no-loc text="PinkOrangeTheme":::", ":::no-loc text="CustomTheme":::" | Selected theme name. |
| :::no-loc text="ThemeDataVersion"::: | REG_DWORD | 2 | Theme data version. |

## Typing

This setting contains toggles and other settings related to touch keyboard, text suggestions and preferences.

### Registry values under HKCU\Software\Microsoft\input\Settings

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="EnableAutoShiftEngage"::: | REG_DWORD | 1 or 0 | Enables auto shift engage. |
| :::no-loc text="EnableDoubleTapSpace"::: | REG_DWORD | 1 or 0 | Enables double-tap space. |
| :::no-loc text="EnableAutocorrection"::: | REG_DWORD | 1 or 0 | Enables auto-correction. |
| :::no-loc text="IsVoiceTypingKeyEnabled"::: | REG_DWORD | 1 or 0 | Indicates if voice typing key is enabled. |
| :::no-loc text="MultilingualEnabled"::: | REG_DWORD | 1 or 0 | Indicates if multilingual is enabled. |
| :::no-loc text="EnableHwkbTextPrediction"::: | REG_DWORD | An integer. | ASCII Value for keys. |

## VPN

VPN settings that apply to all VPN connections configured in Windows Settings.

### Registry values under HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasMan\Parameters\Config\VpnCostedNetworkSettings

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="NoCostedNetwork"::: | REG_DWORD | 0 or 1 | Block VPN over metered networks. |
| :::no-loc text="NoRoamingNetwork"::: | REG_DWORD | 0 or 1 | Block VPN while roaming. |

## Wi-Fi

Global random hardware addresses preference.

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WlanSvc\Interfaces\\&lt;Interface GUID&gt;

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| :::no-loc text="RandomMacState"::: | REG_BINARY | 00 00 00 00 or 01 00 00 00 | Whether to use random hardware addresses for newly configured Wi-Fi networks. |
