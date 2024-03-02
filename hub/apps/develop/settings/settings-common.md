---
title: Reference for common Windows settings
description: This page lists the settings that are supported by both Windows 10 and Windows 11.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Reference for common Windows settings

**TBD - Still working on the language to frame these settings**
This page lists the settings that are supported by both Windows 10 and Windows 11. Link to [Settings back up and restore overview](index.md). Link to [Cloud Data Store Settings Reader Tool (readCloudDataSettings.exe)](readclouddatasettings-exe.md). Link to [Reference for Windows 11 settings](settings-windows-11.md). 



## Autoplay

This setting helps to set defaults for removable drives and memory cards

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers

**TBD - There are some values I couldn't deduce meanings for (e.g. that GUID), but also I can't figure out how these registry keys match with the UI strings in the doc. **


| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| DisableAutoplay | REG_DWORD | 0/1 | Toggle the use of autoplay for all media and devices. |
| UserChosenExecuteHandlers\CameraAlternate\ShowPicturesOnArrival | REG_SZ | 0/1 | User selected default autoplay behavior for removable drive. |
| UserChosenExecuteHandlers\ StorageOnArrival | REG_DWORD | 0/1 | User selected default autoplay behavior for memory card. |
| EventHandlersDefaultSelection\CameraAlternate\ShowPicturesOnArrival | REG_DWORD | 0/1 | System default autoplay behavior for removable drive. |
| EventHandlersDefaultSelection\ StorageOnArrival | REG_DWORD | 0/1 | System default autoplay behavior for memory card. |

#### Supported data values for ShowPicturesOnArrival

| Data value | Description |
|------------|-------------|
| dsd9eksajf9re3669zh5z2jykhws2jy42gypaqjh1qe66nyek1hg!content!import | TBD |
| MSPlayMediaOnArrival | Play media on arrival. |
| MSOpenFolder | Open folder. |
| MSPromptEachTime | Prompt each time. |
| OneDriveAutoPlay | TBD |
| MSTAKENOACTION | Take no action. |

#### Supported data values for StorageOnArrival

| Data value | Description |
|------------|-------------|
| MSTAKENOACTION | Take no action. |
| MSOpenFolder | Open folder. |
| MSStorageSense | TBD |
| MSPromptEachTime | Prompt each time. |



## BackupUnitStore

 **TBD - I can't infer a description for this setting. Seems to use some non-primitive types that aren't explained.**

### Type: Windows.Data.SettingsBackup.BackupUnitStore structure

#### BackupUnitStore Properties

| Name | Type | Description |
|------|------|-------------|
| context | OperationContext | The context of the section within the manifest that defined the collection rules. |
| manifestId | wstring | the name of the manifest that defined the collection rules. |
| scope | wstring | the scope within the manifest. |
| dataSetIdFormat | DataSetIdFormat | the format of the data set id contained in the dataSetId field. |
| dataSetId | wstring | the data set id that should be restored from the data blob. |
| created | int64 | Timestamp when the blob was created on the client. This is a client-based time and not a server based time. |
| lastModified | int64 | Timestamp when the blob was most recently updated on the client. This is a client-based time and not a server based time. |
| osVersion | wstring | OS version. |
| generatorId | bond.GUID | Indicates the component that generated the contents of the data field. |
| dataEncoding | uint64 | Indicates the generator-specific format that the generator used to produce the content. |
| encryptionInfo | EncryptionInfo | If present, the data field is encrypted and this provides details on how to do so. If NOT present, the data field is not encrypted. |
| data | blob | Migration engine generated data. |

## Calling

The settings below are for a deprecated Windows calling experience and are no longer read by the operating system, however the settings data may be present on user devices or in the cloud.

### Type: Windows.data.calling.Settings structure

#### Settings Properties

| Name | Type | Description |
|------|------|-------------|
| perKeySettings | map&lt;wstring, KeyPathSettings&gt; | A map of per-key settings |

### Type: Windows.Data.ContrastThemes.CurrentThemeType enumeration

### Type: Windows.data.calling.KeyPathSettings structure

#### Settings Properties

| Name | Type | Description |
|------|------|-------------|
| values | map&lt;wstring, wstring&gt; | A map of key/path settings |

### Type: Windows.data.calling.callhistoryItem structure

#### callhistoryItem Properties

| Name | Type | Description |
|------|------|-------------|
| uniqueId | String | Unique identifier for the record. |
| phoneNumber | String | Phone number of the caller. |
| calltype | Enum CallType | Possible values: Outgoing, IncomingAnswered, IncomingMissed, IncomingRinging, OutgoingMissed, OutgoingRinging. |
| voicemailCall | Bool | Indicates whether the call was a voice mail. |
| videocall | Bool | Indicates whether the call was a video call. |
| Seen | Bool | Indicates whether the call history was seen by the user. |
| callerIdBlocked | Bool | Indicates whether the call was blocked. |
| emergencycall | Bool | Indicates whether call was an emergency call. |
| linenumber | String | The number of the phone line that received the call. |
| lineName | String | The phone line’s name. |
| callerLocation | String | Caller location. |
| callerCategory | String | Caller category. |
| callerCategoryDescription | String | Caller category description. |
| Calltimestamp | Unit64 | unix time stamp. | 
| Callarrivaltime | Unit64 | Unix time the call arrived on the device. |
| callEndTime | Unit64 | Unix time stamp when the call was ended. |


### Type: Windows.Data.calling.callhistory structure

#### callhistory Properties

| Name | Type | Description |
|------|------|-------------|
| historyItems | Map&lt;string, CallHistoryItem&gt; | A collection of call history items where the keys are each history item’s UniqueId. |
| highestSequenceNumber | Unit32 | Highest sequence number issued, used for internal business logic. |

### Type: Windows.data.calling.callfavorites structure

#### callfavorites Properties

| Name | Type | Description |
|------|------|-------------|
| favoriteItems | vector&lt;CallFavoriteItem&gt; | A collection of calling favorites. |

### Type: Windows.data.calling.CallFavoriteItem structure

#### CallFavoriteItem Properties

| Name | Type | Description |
|------|------|-------------|
| phoneNumber | String | A collection of calling favorites. |
| displayName | String | Display name of the favorite. |
| lineNumber | String | Line number the favorite is associated with. |
| phoneNumberName | Uint32 | Phone number name. |
| remoteIdHash | String | Remote ID hash. |
| propHash | String | Property hash. |
| isVideoCall | Bool | Indicates whether the item is a video call. |
| isPublicSwitchTelephoneNetwork | Bool | Indicates whether the item is a telephone call. |
| applicationId | String | Application ID. |
| callbackToken | String | Callback token. |
| UniqueId | Unit64 | A unique identifier for the item. |

## Contrast themes

Settings related to high-contrast themes.

### Type: Windows.Data.ContrastThemes.CurrentThemeType enumeration

#### CurrentThemeType values

| Name | Value | Description |
|------|-------|---------|
| Contrast_Inbox | 0   | In-box high contrast theme. |
| Contrast_Custom | 1   | Customized high contrast theme. |
| Backup_disabled | 2   | **TBD - This looks like "backup disabled" to me, but the description of the struct property that uses this is "Signifies whether current theme is personalized theme or inbox high contrast theme or customized high contrast theme"** |

### Type: Windows.Data.ContrastThemes.SynchedTheme structure

#### SynchedTheme properties

| Name | Value | Description |
|------|-------|---------|
| type  | **CurrentThemeType**   | Specifies whether current theme is personalized theme or inbox high contrast theme or customized high contrast theme. |
| currentThemePath  | wstring   | The complete path to the currently applied theme file.  |
| contrastThemePath  | wstring   | The complete path to the last applied high contrast theme file.This can be same as of *currentThemePath* if current theme is high contrast. |
| baseContrastThemeName  | wstring | Specifies the base in-box high contrast theme is applied. Any customization may have been applied on top of these themes. Supported values are "High Contrast #1", " or "High Contrast #2", "High Contrast Black", or "High Contrast White" |
| customThemeName  | wstring   | The user-defined name for a customized contrast theme. |
| rgbBackground  | **Windows.Data.Common.Color**   | Theme background color. |
| rgbText  | **Windows.Data.Common.Color**   | Theme text color. |
| rgbHyperlink  | **Windows.Data.Common.Color**   | Theme hyperlink color. |
| rgbInactiveText  | **Windows.Data.Common.Color**   | Theme inactive text color. |
| rgbSelectedText1  | **Windows.Data.Common.Color**   | Theme selected text 1 color. |
| rgbSelectedText2  | **Windows.Data.Common.Color**   | Theme selected text 2 color. |
| rgbButtonText1  | **Windows.Data.Common.Color**   | Theme button text 1 color. |
| rgbButtonText2  | **Windows.Data.Common.Color**   | Theme button text 2 color. |

### Type: Windows.Data.Common.Color structure

#### Color values

| Name | Value | Description |
|------|-------|---------|
| red  | uint8 | Red channel value of an RGBA color. |
| green  | uint8 | Green channel value of an RGBA color.  |
| blue  | uint8 | Blue channel value of an RGBA color.  |
| alpha  | uint8 | Alpha channel value of an RGBA color. |

## Date and Time

Settings related to date and time.

### Registry values under HKLM\SYSTEM\CurrentControlSet\Services\tzautoupdate

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| start | REG_DWORD | 0/1 | Enables set time zone automatically. |

### Registry values under HKLM\SYSTEM\CurrentControlSet\Control\TimeZoneInformation

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| timeZone | REG_SZ | System timezone as string.  | The currently set time zone for the system. |



## Do not disturb

Set "do not disturb" status manually or automatically, so that notifications will be sent directly to the notification center.

This setting is multi-instance. 

### Type: Windows.Data.DoNotDisturb.ChangeReason enumeration

#### ChangeReason values

| Name | Value | Description |
|------|-------|---------|
| Default | 0   | Default setting. |
| User | 1  | User setting.  |

### Type: BoolWithMetadata structure

#### ChangeReason properties

| Name | Value | Description |
|------|-------|---------|
| value  | bool   | The boolean value. |
| changeReason  | **ChangeReason**  | The reason a user changed their profile.|

### Type: Windows.Data.DoNotDisturb.QuietHoursProfile structure

#### QuietHoursProfile Properties

| Name | Type | Description |
|------|------|-------------|
| isInitialized | bool | CDS data initialize status.  |
| settings | map&lt;uint64, bool&gt; | Map of the boolean state of the settings. Specifies when a given settings checkbox is checked or unchecked. |
| allowedContacts | set&lt;wstring&gt; | The set of allowed contacts as a list. |
| allowedApps | set&lt;wstring&gt; | The list of app names to set notifications on. |
| defaultAllowedAppsRemoved | set&lt;wstring&gt; | The list of names of the default app names removed. |

### Type: Windows.Data.DoNotDisturb.QuietHoursSettings structure

#### QuietHoursSettings Properties

| Name | Type | Description |
|------|------|-------------|
| isInitialized | bool | CDS data initialize status. . |
| selectedProfile | wstring | The string value of the user profile. "Unrestricted" or "Priority Only"   |
| shouldShowSummaryToast | BoolWithMetadata | Sets whether the summary toast should be shown and the associated reason. |

### Type: Windows.Data.DoNotDisturb.QuietMoment structure

#### QuietMoment Properties

| Name | Type | Description |
|------|------|-------------|
| isInitialized | bool | CDS data initialize status. Default value is false. |
| isEnabled | bool | Settings enabled or disabled. |
| assignedProfile | wstring | The value of the user profile set by automatic rules. "Unrestricted", "Priority Only", or "Alarms Only"  |
| startTime | TimeSpan | When automatic rules should start. |
| endTime | TimeSpan | When automatic rules should end. |
| repeatType | uint32 | **TBD - The data type is an int. Is there a missing enum?** How frequently the rules should apply. "Daily", "Weekend", or "Weekdays"|
| shouldShowActiveToast | BoolWithMetadata | Sets whether the active toast should be shown and the associated reason.  |

## File Explorer Classic

Settings related to the classic Windows File Explorer.

### Type: Windows.Data.FileExplorerClassic.ShellStateSetting structure

#### ShellStateSetting Properties

| Name | Type | Description |
|------|------|-------------|
| bmigratedFromSSF | bool | Indicates whether the shell state is migrated from SSF. |
| bshowAllObjects | bool | Indicates whether Shell state should show all objects. |
| bshowExtensions | bool | Indicates whether Shell state should show all extensions. |
| bshowCompColor | bool | Indicates whether Shell state should show all colors. |
| bdoubleClickInWebView | bool | Indicates whether Shell state should show all objects. |
| bdontPrettyPath | bool | Indicates whether Shell state should show super hidden. |
| showInfoTip | bool | Indicates whether Shell state should show info tip. |
| noConfirmRecycle | bool | Indicates whether Shell state should show super hidden. |
| showSuperHidden | bool | Indicates whether Shell state should show super hidden. |
| sepProcess | bool | Indicates whether Shell state should show the separation process. |
| iconsOnly | bool | Indicates whether Shell state should show icons only. |
| showTypeOverlay | bool | Indicates whether Shell state should show type overlay. |


### Type: Windows.Data.FileExplorerClassic.CabinetStateSettings structure

#### CabinetStateSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether the cabinet state is migrated from SSF. |
| fullPathTitle | bool | Indicates whether the cabinet state setting should show full path title. |
| saveLocalView | bool | Indicates whether the cabinet state setting should save local view. |
| newWindowMode | bool | Indicates whether the cabinet state setting should open in new window mode. |

### Type: Windows.Data.FileExplorerClassic.AdvancedSettings structure

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether advanced settings are migrated from SSF. |
| alwaysShowMenus | bool | Indicates whether advanced settings should always show menu. |
| folderContentsInfoTip | bool | Indicates whether advanced settings should show folder contents info tip. |
| hideDrivesWithNoMedia | bool | Indicates whether advanced settings should hide the drives with no media present. |
| navPaneExpandToCurrentFolder | bool | Indicates whether advanced settings should expand the navigation pane to current folder. |
| navPaneShowAllFolders | bool | Indicates whether advanced settings should expand the navigation panel to show all folders. |
| navPaneShowFavorites | bool | Indicates whether advanced settings should expand the navigation panel to show all favorites. |
| persistBrowsers | bool | Indicates advanced settings for file explorer should persist browsers. | 
| sharingWizardOn | bool | Indicates whether sharing wizard is on for advanced settings. |
| showDriveLetters | bool | Indicates whether drives letters are shown for advanced settings. |
| showPreviewHandlers | bool | Indicates whether to show preview handlers. |
| typeAhead | bool | Indicates whether to show type ahead. |
| showStatusBar | bool | Indicates whether the file explorer should show status bar. |
| showLibraries | bool | Indicates whether the file explorer advanced settings should show libraries. |
| showCompColor | bool | Indicates whether to show comp color for advanced settings. |

### Type: Windows.Data.FileExplorerClassic.ExplorerSettings structure

#### ExplorerSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether Explorer settings are migrated from SSF. |
| underlineIconsAlways | bool | Indicates whether Explorer settings should always underline icons. |
| underlineIconsNever | bool | Indicates whether Explorer settings should never underline icons. |
| underlineIconsAsBrowser | bool | Indicates whether Explorer settings should underline icons as browser. |

### Type: Windows.Data.FileExplorerClassic.SearchSettings structure

#### SearchSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether search settings are migrated from SSF. |
| searchOnly | bool | Indicates whether search setting show search only. |
| wholeFileSystem | bool | Indicates whether search setting show the whole file system. |
| systemFolders | bool | Indicates whether search setting show system folders. |
| archivedFiles | bool | Indicates whether search setting show archived files. |

### Type: Type: Windows.Data.FileExplorerClassic.RegistrySettings structure

These are blobs that are in the registry. There are three things that use registry stream settings:

| Setting | Type |
|---------|------|
| RibbonQat | A single blob |
| Details Preview Settings | Two DWORDs (dwPreviewPaneSettings and dwReadingPaneSettings) |
| Navigation Pane Visibility | A bool |

#### RegistrySettingsProperties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether registry settings are migrated from SSF. |
| ribbonQat | blob | A single blob. |
| detailsPreviewPaneSettings | unit32 | Details about preview pane settings. |
| readingPaneSettings | unit32 | Indicates reading pane settings. |
| navigationPaneVisible | bool | Indicates whether navigation pane is visible or not. |

### Type: Windows.Data.FileExplorerClassic.FolderOptionGeneralSettings structure

These are settings found in File Explorer->Folder options(...)->General tab

•	File explorer can be opened to either Home or This PC 
•	File explorer can be opened to One Drive folder as well if user has signed in to One Drive (This option is available only if user has signed in)

#### FolderOptionGeneralSettings Properties

| Name | Type | Description |
|------|------|-------------|
| defaultOpenLocation | unit8 | Indicates whether file explorer can be opened to either home or this PC. Default is home (0). |
| browseFoldersInNewWindow | bool | Indicates whether to browse folder in new window. |
| useDoubleClickToOpen | bool | Indicates whether to use double click to open. |
| showRecentlyUsedFiles | bool | Indicates whether to show recently used files. |
| showFrequentlyUsedFolders | bool | Indicates whether to show frequently used folders. |
| showFilesFromOffice | bool | Indicates whether to show files from MS office. |

### Type: Windows.Data.FileExplorerClassic.FolderOptionsAdvancedSettings structure

These are settings found in File Explorer->Folder options(...)->View tab->Advanced settings

#### FolderOptionsAdvancedSettings Properties

| Name | Type | Description |
|------|------|-------------|
| alwaysShowIconsNeverThumbnails | bool | This is used to show icons only. |
| compactView | bool | Decrease space between items. |
| displayFileIconOnThumbnails | bool | Indicates whether to display file icon on thumbnails. |
| displayFileSizeInfoInFolderTips | bool | Indicates whether to display file size information in folder tips. |
| displayFullFilePath | bool | Show folder contents info tip. |
| showSysHiddenFiles | bool | Show/hide hidden files and folders. |
| hideEmptyDrives | bool | Hide drives with no media. |
| showExtensions | bool | Show/hide extensions of known file types. |
| hideFolderMergeConflicts | bool | Indicates whether to show/hide folder merge conflicts. |
| showSuperHiddenOSProtectedFiles | bool | Show/hide OS protected files. |
| showDriveLetters | bool | Show/hide drive letters. |
| colorEncryptedCompressedFiles | bool | Show encrypted and compressed files in color. |
| showPopupDescription | bool | Indicates pop up descriptions. |
| showSyncProviderNotification | bool | Indicates notification provider sync notification. |
| useCheckboxesForSelection | bool | Used to show check boxes for selection. |
| useSharingWizard | bool | Sharing wizard on/off. |
| navPaneAlwaysShowAvailablityStatus | bool | Show Navigation pane all cloud states. |
| navPaneShowLibraries | bool | Show navigation pane libraries. |
| navPaneShowNetwork | bool | Show pane navigation network. |
| navPaneShowThisPC | bool | This is used to show navigation pane to show current folder. |

### Type: Windows.Data.FileExplorerClassic.RecycleBinSettings structure

These are settings found in Recycle bin-> right click Properties

#### RecycleBinSettings Properties

| Name | Type | Description |
|------|------|-------------|
| displayDeleteConfirmationDialog | bool | Indicates whether to show delete confirmation dialog

### Type: Windows.Data.FileExplorerClassic.DesktopIconSettings structure

These are settings related to desktop icons.

#### DesktopIconSettings Properties

| Name | Type | Description |
|------|------|-------------|
| viewAutoArrangeIcons | bool | Indicates values for AutoArrangement of icons. |
| viewAlignIconsToGrid | bool | Indicates values for snapping to grid. |
| viewShowDesktopIcons | bool | Indicates values to view desktop icons. |
| sortColPropertyKeyFmtid | wstring	L"B725F130-47EF-101A-A5F1-02608C9EEBAC". |
| sortColPropertyKeyPid | Unit32 | 10. |
| sortColDirection | bool | 1=Ascending -1=Descending Default value is Ascending (true). |



## Input Method Editors (IME)

History files are used to optimize the Japanese IME user experience across devices.

**TBD - This setting references some non-primitive types. Are these documented somewhere? **

### Type: Windows.Data.Input.HistoryFiles structure

#### HistoryFiles Properties

| Name | Type | Description |
|------|------|-------------|
| InputDataFiles | Map&lt;string,FilePathInfo&gt;| A map of input history FilePathInfo structs where the key represents a relative folder path. |

### Type: Windows.Data.Input.FilePathInfo structure

#### FilePathInfo Properties

| Name | Type | Description |
|------|------|-------------|
| filePath | Map&lt;string,fileData&gt;| A map of FileData structs where the key is the file name. |

### Type: Windows.Data.Input.FileData structure

#### FileData Properties

**TBD - Seems suspicious that this property name is listed as "filePath". Copy and paste error in the doc?**


| Name | Type | Description |
|------|------|-------------|
| filePath | blob | Raw input data, byte array serialized as a collection of integers. |


## Internet Explorer

Although Internet explorer has reached end of life, some settings stored by the browser may remain in the cloud or on Windows through settings backup. 

The **SettingUnit** stucture used with the following settings has the following definition:

```cpp
struct SettingUnit 
{
    String settingType
    String settingUnitID
    FILETIME timeStamp
    blob settingData
}
```

### Type: Windows.Data.InternetExplorer.Favorites structure

#### Favorites Properties

| Name | Type | Description |
|------|------|-------------|
| favoriteSettings | vector&lt;**SettingUnit**&gt; | **SettingUnit** structures related to Internet Explorer favorites. |

### Type: Windows.Data.InternetExplorer.TypedURLS structure

#### TypedURLS Properties

| Name | Type | Description |
|------|------|-------------|
| typedURLSettings | vector&lt;**SettingUnit**&gt; | **SettingUnit** structures related to Internet Explorer  TypedURLS. |

### Type: Windows.Data.InternetExplorer.BrowserHistory structure

#### BrowserHistory Properties

| Name | Type | Description |
|------|------|-------------|
| BrowserHistory | vector&lt;**SettingUnit**&gt; | **SettingUnit** structures related to Internet Explorer browser history. |

### Type: Windows.Data.InternetExplorer.AutoComplete structure

#### AutoComplete Properties

| Name | Type | Description |
|------|------|-------------|
| AutoCompleteSetting | vector&lt;**SettingUnit**&gt; | **SettingUnit** structures related to Internet Explorer autocomplete. |

### Type: Windows.Data.InternetExplorer.TabRoaming structure

#### TabRoaming Properties

| Name | Type | Description |
|------|------|-------------|
| TabRoamingSetting | vector&lt;**SettingUnit**&gt; | **SettingUnit** structures related to Internet Explorer tab roaming settings. |

## Lock screen

Setting for managing and personalizing the lock screen.  

### Type: Windows.Data.LockScreenSettings.LockScreenKind enumeration

#### LockScreenKind values

| Name | Value | Description |
|------|-------|---------|
| Picture | 0   | Picture. |
| Slideshow | 1  | Slideshow  |
| Spotlight | 2  | Spotlight |


### Type: Windows.Data.LockScreenSettings structure

The scope of this type is per device.

#### LockScreenSettings Properties

| Name | Type | Description |
|------|------|-------------|
| kind | **LockScreenKind** | Specifies whether current Lockscreen is set as Wallpaper or Slideshow or Spotlight |
| pictureOnSignInScreen | bool | Specifies whether show the lock screen background picture on the sign-in screen is enabled. |
| funItems | bool | Specifies whether “Get fun facts, tips, tricks, and more on your lock screen” is enabled. |
| itemId | wstring | The unique ID for the lockscreen wallpaper uploaded to OneDrive during backup.  |
| contentUri | wstring | The url for the lockscreen wallpaper uploaded to OneDrive during backup |
| lockScreenStatus | wstring | Not used. |
| slideShowAutoLock | Not used. |  |
| slideShowEnabledOnBattery | Not used. |  |
| slideshowOptimizePhotoSelection | Not used. |  |
| slideShowIncludeCameraRoll | Not used. |  |
| slideShowDuration | Not used. |  |
| syncRootRelativePaths | Not used. |  |


## ManifestBackupStore

 **TBD - I can't infer a description for this setting. Seems to use some non-primitive types that aren't explained.**

### Type: Windows.Data.Platform.BackupRestore.ManifestBackupStore structure

This type is multi-instance and must be retrieved using the following collection collection names:

* "Deviceprofiles"

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readclouddatasettings-exe.md).

#### ManifestBackupStore Properties

| Name | Type | Description |
|------|------|-------------|
| profileId |wstring | Contains unique ID and CDS would also use this for partitionId to store all perDevice settings as part of this profile. This value is case-sensitive with a maximum length of 64 characters. The following characters are not allowed: '\\', '/', ':', '*', '?', '\', '<', '>', '\|', '#', '%', '$'.|
| context | BackupContext | Context. | 
| sourceManifestId | wstring | The ID of the manifest used to create this instance. |
| sourceManifestName | wstring | Name of the manifest used to create this instance. | 
| filters | vector&lt;wstring&gt; | The collection of filters used to partition the manifest used to create this instance. |
| sourceProfileId | wstring | Optional. Source profileId where current profile is created from. Used for internal/debugging purposes only. |
| createdTime | uint64 | UTC timestamp of the first time this item was submitted to the cloud. |
| modifiedTime | uint64 | UTC timestamp of the most recent time this item was submitted to the cloud. |
| OSVersion | uint64 | OS version. |
| isActive | bool | A value indicating if the profile active or not. |
| payload | blob | The data payload. |

## Multiple displays

Settings related to multiple displays. 

This setting is single-instance.

### Type: Windows.Data.Settings.DisplaySettings.MultipleDisplays structure

The scope of this type is per device.

#### CallFavoriteItem Properties

| Name | Type | Description |
|------|------|-------------|
| rememberWindowLocationsPerMonitorConnection | nullable&lt;bool&gt; | Enables remember window locations per monitor connection |
| minimizeWindowsOnMonitorDisconnect | nullable&lt;bool&gt; | Enables minimize windows on monitor disconnect. |
| easeCursorMovementBetweenDisplays | nullable&lt;bool&gt; | Enables minimize windows on monitor disconnect. |


## NlmSignature

 **TBD - I can't infer a description for this setting.**

### Type: Windows.Data.Nlm.NlmSignature structure

This type is multi-instance and must be retrieved using the following collection collection names:

* "wificloudstore3"
* "wifi3_wpa3"
* "wifi3_owe"

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readclouddatasettings-exe.md).


#### NlmSignature Properties

| Name | Type | Description |
|------|------|-------------|
| category | uint64 | Category of signature? *TBD - description in source seems tentative* |


## Personalization - colors

Settings related to system colors.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| AppsUseLightTheme | REG_DWORD | 0/1 | Apps use light theme. |
| SystemUsesLightTheme | REG_DWORD | 0/1 | System uses light theme. |
| EnableTransparency | REG_DWORD | 0/1 | Enable transparency |
| ColorPrevalance | REG_DWORD | 0/1 | **TBD - I can't infer a description for this**|

### Registry values under HKCU\Control Panel\Desktop

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| AutoColorization | REG_DWORD | 0/1 | Enable auto-colorization. |

### Registry values under HKCU\Software\Microsoft\Windows\DWM

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| ColorPrevalence | REG_DWORD | 0/1 | **TBD - I can't derive a description for this** |




## Secondary accounts

Provides information about Microsoft accounts (MSA) and work or school accounts added to the device to sign in to apps or online services, in addition to the account used to log on to the device. On Windows 11, backup and restore of this setting is supported. On Windows 10, backup is supported but restore is not.

### Type: Windows.Data.Account.AccountType enumeration

#### AccountType values

| Name | Value | Description |
|------|-------|---------|
| MSA | 0   | Microsoft account. |
| AAD | 1  | Azure Active Directory account,  |
| Others | 2  | Oher account. |

### Type: Windows.Data.Account.AccountInfo structure

#### AccountInfo Properties

| Name | Type | Description |
|------|------|-------------|
| accountName | wstring | The user name of the account such as "example@outlook.com". |
| accountId | wstring | The unique identifier of the account. |
| accountType | **AccountType** | The type of the account. |
| country | wstring | The code of the country or region in which a MSA is registered.  |
| safeCustomerId | wstring | An alternative identifier for an MSA. |
| ageGroup | wstring | Age group of an MSA, based on the registered birth date of the MSA user. Current values are 0 = unknown, 1 = child, 2 = teen, 3 = adult. |
| scope | wstring | Represents the “Sign in options” setting state. |

### Type: Windows.Data.Account.SecondaryAccounts structure

#### SecondaryAccounts Properties

| Name | Type | Description |
|------|------|-------------|
| accountDetails | vector&lt;AccountInfo&gt; | A vector of **AccountInfo** objects representing secondary accounts.  |

## USB

This setting controls toggles such as connection notifications, battery saver and other notifications related to charging of PC.

This setting is single-instance.

### Registry values under HKCU\Software\Microsoft\Shell\USB

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| NotifyOnUsbErrors | REG_DWORD | 0 or 1 | Enables notifications for USB errors. |
| NotifyOnWeakCharger | REG_DWORD | 0 or 1 | Enables notifications for weak charger detected. |

## Windows Backup - Remember my preferences

Helps user to synchronize the settings and Data on multiple machines.

### Type: Windows.Data.WindowsBackup.Preference.CloudStorePolicyEnum enumeration

#### CloudStorePolicyEnum values

| Name | Value | Description |
|------|-------|---------|
| SyncEnabled | 0   | Sync enabled. |
| SyncDisabledByUser | 1  | Sync disabled by user. |
| SyncDisabledByGroupPolicy | 2  | Sync disabled by group policy. |

#### Toggles properties

| Name | Type | Description |
|------|------|-------------|
| toggleState | map&lt;wstring, **CloudStorePolicyEnum**&gt; | A map of sync policy names that holds the user preference in terms of mapping of the Sync policy and CloudStorePolicy option. |



## Windows Update

Settings related to Windows Update.

This setting is single instance.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR

**TBD - ALL DESCRIPTIONS TENTATIVE**

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| IsContinuousInnovationOptedIn | REG_DWORD | 0/1 | Enables devices to get the latest updates as soon as they’re released. |
| AllowMUUpdateService | REG_DWORD | 0/1 | Allows users to get other Microsoft products alongside with Windows Updates. |
| IsExpedited | REG_DWORD | 0/1 | User selects this to invoke device restart 15 min after all updates finished installing. |
| RestartNotificationsAllowed2 | REG_DWORD | 0/1 | Users decides if they want to be notified about the updates pending restart. |
