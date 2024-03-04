---
title: Reference for Windows 11 settings
description: This article provides reference information for accessing settings values on devices running Windows 11.
ms.date: 02/27/2024
ms.topic: article
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
| AllowFailover | REG_DWORD | 0 or 1 | Use cellular whenever Wi-Fi is poor.  |

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\SubscriptionManager\&lt;ICCID&gt;\&lt;IMSI&gt;

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| UserCost | REG_DWORD | 0 or 1 | Maps the metered/unmetered state to each IMSI.   |

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WwanSvc\DisallowAutoConnectByClient

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| &lt;interface GUID&gt;| REG_DWORD | 0 or 1 | Let Windows keep the device connected to cellular. |

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WwanSvc\RoamingPolicyForPhone\&lt;interface GUID&gt;

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| InternetAlwaysOn| REG_DWORD | 0 or 1 | Roaming or no roaming. When entering a roaming area, your data connection will be turned off if roaming is not allowed. |

## Ease cursor movement across displays

When enabled, Windows makes it easier to move your cursor and windows between displays by letting your cursor jump over areas where it would previously get stuck.

This setting is single-instance.

### Type: Windows.Data.Settings.DisplaySettings.MultipleDisplays structure

#### MultipleDisplays Properties

| Name | Type | Description |
|------|------|-------------|
| rememberWindowLocationsPerMonitorConnection | `nullable<bool>` | Remember window locations based on monitor connection. |
| minimizeWindowsOnMonitorDisconnect | `nullable<bool>` | Minimize windows when a monitor is disconnected. |
| easeCursorMovementBetweenDisplays | `nullable<bool>` | Ease cursor movement between displays. |

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

### Type: Windows.Data.LunarCalendar structure

#### LunarCalendar Properties

| Name | Type | Description |
|------|------|-------------|
| languageType   | **LunarCalendarLanguageType** | A member of the **LunarCalendarLanguageType** enumeration. The default value is **Default**. |

### Type: Windows.Data.LunarCalendarPerDevice structure

This type inherits from **LunarCalendar**. The scope of this type is per device.

### Type: Windows.Data.LunarCalendarLanguageType enumeration

### LunarCalendarLanguageType Values

| Name | Value | Description |
|------|-------|---------|
| Default | 0   | The default lunar calendar configuration. |
| None   | 1 | No lunar calendar. |
| SimplifiedChinese  | 2 | The Simplified Chinese lunar calendar. |
| TraditionalChinese | 3   | The Traditional Chinese calendar. |


## Personalization - Start - Layout - Pins and recomendations

Specifies the start layout type.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| Start_Layout | REG_DWORD | 0 [Default], 1 [More Pins], 2 [More Recommendations] | The start layout. |

## Personalization - Start - Recommendations

Specifies whether recommendations for tips, shortcuts, new apps and more are shown.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| Start_IrisRecommendations | REG_BOOL | 0 or 1 | Specifies whether recommendations are enabled. |

## Personalization - Themes

This setting is used to set a personalized theme.

### Type: Windows.Data.PersonalizationThemes.CurrentThemeType enumeration

### CurrentThemeType Values

| Name | Value | Description |
|------|-------|---------|
| InboxTheme | 0   | In-box theme. |
| ContrastTheme | 1   | Contrast theme. |


### Type: Windows.Data.PersonalizationThemes.CurrentThemeType structure

#### CurrentThemeType Properties

| Name | Type | Description |
|------|------|-------------|
| type   | **CurrentThemeType** | The current theme type. |
| basePersonalizationThemeName   | wstring | The name of the current inbox theme applied in the system. The user may have done customization on top of this inbox theme.  |



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


## VPN

VPN settings that apply to all VPN connections configured in the Settings app.

### Registry values under HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasMan\Parameters\Config\VpnCostedNetworkSettings

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| NoCostedNetwork | REG_DWORD | 0 or 1 | Block VPN over metered networks. |
| NoRoamingNetwork | REG_DWORD | 0 or 1 | Block VPN while roaming. |

## Wi-Fi

Global random hardware addresses preference.

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WlanSvc\Interfaces\&lt;Interface GUID&gt;

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| RandomMacState | REG_DWORD | 0 or 1 | Whether to use random hardware addresses for newly configured Wi-Fi networks. |



