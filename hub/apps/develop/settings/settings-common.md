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
This page lists the settings that are supported by both Windows 10 and Windows 11. Link to [Settings back up and restore overview](index.md). Link to [Cloud Data Store Settings Reader Tool (readsettingdata.exe)](readsettingsdata-exe.md). Link to [Reference for Windows 11 settings](settings-windows-11.md). Link to [Reference for Windows 11 settings - TBD]().



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

### Type: Windows.Data.SettingsBackup.BackupUnitStore

### Properties

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

## Type: Windows.data.calling.settings

**TBD - No info provided for this type in the "legacy settings" doc**

## Type: Windows.data.calling.callhistoryItem

### Properties

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


## Type: Windows.Data.calling.callhistory

### Properties

| Name | Type | Description |
|------|------|-------------|
| historyItems | Map&lt;string, CallHistoryItem&gt; | A collection of call history items where the keys are each history item’s UniqueId. |
| highestSequenceNumber | Unit32 | Highest sequence number issued, used for internal business logic. |

## Type: Windows.data.calling.callfavorites

| Name | Type | Description |
|------|------|-------------|
| favoriteItems | vector&lt;CallFavoriteItem&gt; | A collection of calling favorites. |

## Type: Windows.data.calling.CallFavoriteItem

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


## File Explorer Classic

Settings related to the classic Windows File Explorer.

### Type: Windows.Data.FileExplorerClassic.ShellStateSetting Structure

### ShellStateSetting Properties

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


### Type: Windows.Data.FileExplorerClassic.CabinetStateSettings Structure

### CabinetStateSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether the cabinet state is migrated from SSF. |
| fullPathTitle | bool | Indicates whether the cabinet state setting should show full path title. |
| saveLocalView | bool | Indicates whether the cabinet state setting should save local view. |
| newWindowMode | bool | Indicates whether the cabinet state setting should open in new window mode. |

### Type: Windows.Data.FileExplorerClassic.AdvancedSettings Structure

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

### Type: Windows.Data.FileExplorerClassic.ExplorerSettings Structure

### ExplorerSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether Explorer settings are migrated from SSF. |
| underlineIconsAlways | bool | Indicates whether Explorer settings should always underline icons. |
| underlineIconsNever | bool | Indicates whether Explorer settings should never underline icons. |
| underlineIconsAsBrowser | bool | Indicates whether Explorer settings should underline icons as browser. |

### Type: Windows.Data.FileExplorerClassic.SearchSettings Structure

### SearchSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether search settings are migrated from SSF. |
| searchOnly | bool | Indicates whether search setting show search only. |
| wholeFileSystem | bool | Indicates whether search setting show the whole file system. |
| systemFolders | bool | Indicates whether search setting show system folders. |
| archivedFiles | bool | Indicates whether search setting show archived files. |

### Type: Type: Windows.Data.FileExplorerClassic.RegistrySettings Structure

These are blobs that are in the registry. There are three things that use registry stream settings:

| Setting | Type |
|---------|------|
| RibbonQat | A single blob |
| Details Preview Settings | Two DWORDs (dwPreviewPaneSettings and dwReadingPaneSettings) |
| Navigation Pane Visibility | A bool |

### RegistrySettingsProperties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether registry settings are migrated from SSF. |
| ribbonQat | blob | A single blob. |
| detailsPreviewPaneSettings | unit32 | Details about preview pane settings. |
| readingPaneSettings | unit32 | Indicates reading pane settings. |
| navigationPaneVisible | bool | Indicates whether navigation pane is visible or not. |

### Type: Windows.Data.FileExplorerClassic.FolderOptionGeneralSettings Structure

These are settings found in File Explorer->Folder options(...)->General tab

•	File explorer can be opened to either Home or This PC 
•	File explorer can be opened to One Drive folder as well if user has signed in to One Drive (This option is available only if user has signed in)

### FolderOptionGeneralSettings Properties

| Name | Type | Description |
|------|------|-------------|
| defaultOpenLocation | unit8 | Indicates whether file explorer can be opened to either home or this PC. Default is home (0). |
| browseFoldersInNewWindow | bool | Indicates whether to browse folder in new window. |
| useDoubleClickToOpen | bool | Indicates whether to use double click to open. |
| showRecentlyUsedFiles | bool | Indicates whether to show recently used files. |
| showFrequentlyUsedFolders | bool | Indicates whether to show frequently used folders. |
| showFilesFromOffice | bool | Indicates whether to show files from MS office. |

### Type: Windows.Data.FileExplorerClassic.FolderOptionsAdvancedSettings Structure

These are settings found in File Explorer->Folder options(...)->View tab->Advanced settings

### FolderOptionsAdvancedSettings Properties

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

### Type: Windows.Data.FileExplorerClassic.RecycleBinSettings Structure

These are settings found in Recycle bin-> right click Properties

### RecycleBinSettings Properties

| Name | Type | Description |
|------|------|-------------|
| displayDeleteConfirmationDialog | bool | Indicates whether to show delete confirmation dialog

### Type: Windows.Data.FileExplorerClassic.DesktopIconSettings Structure

These are settings related to desktop icons.

### DesktopIconSettings Properties

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

## Type: Windows.Data.Input.HistoryFiles

### HistoryFiles Properties

| Name | Type | Description |
|------|------|-------------|
| InputDataFiles | Map&lt;string,FilePathInfo&gt;| A map of input history FilePathInfo structs where the key represents a relative folder path. |

## Type: Windows.Data.Input.FilePathInfo

### FilePathInfo Properties

| Name | Type | Description |
|------|------|-------------|
| filePath | Map&lt;string,fileData&gt;| A map of FileData structs where the key is the file name. |

## Type: Windows.Data.Input.FileData

### FileData Properties

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

### Type: Windows.Data.InternetExplorer.Favorites

The scope of this type is per-user.

### Favorites Properties

| Name | Type | Description |
|------|------|-------------|
| favoriteSettings | Collection of **SettingUnit** structures | **SettingUnit** structures related to Internet Explorer favorites. |

### Type: Windows.Data.InternetExplorer.TypedURLS

The scope of this type is per-user.

### TypedURLS Properties

| Name | Type | Description |
|------|------|-------------|
| typedURLSettings | Collection of **SettingUnit** structures | **SettingUnit** structures related to Internet Explorer  TypedURLS. |

### Type: Windows.Data.InternetExplorer.BrowserHistory

The scope of this type is per-user.

### BrowserHistory Properties

| Name | Type | Description |
|------|------|-------------|
| BrowserHistory | Collection of **SettingUnit** structures | **SettingUnit** structures related to Internet Explorer browser history. |

### Type: Windows.Data.InternetExplorer.AutoComplete

### AutoComplete Properties

| Name | Type | Description |
|------|------|-------------|
| AutoCompleteSetting | Collection of **SettingUnit** structures | **SettingUnit** structures related to Internet Explorer autocomplete. |

### Type: Windows.Data.InternetExplorer.TabRoaming

**TBD - In the "legacy settings" word doc, this entry was mangled by a copy/paste error. A SME should validate the way that I fixed it**

### TabRoaming Properties

| Name | Type | Description |
|------|------|-------------|
| TabRoamingSetting | Collection of **SettingUnit** structures | **SettingUnit** structures related to Internet Explorer tab roaming settings. |

## ManifestBackupStore

 **TBD - I can't infer a description for this setting. Seems to use some non-primitive types that aren't explained.**

### Type: Windows.Data.Platform.BackupRestore.ManifestBackupStore

This type is multi-instance and must be retrieved using the following collection collection names:

* "Deviceprofiles"

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readsettingsdata-exe.md).

### ManifestBackupStore Properties

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


## NlmSignature 

 **TBD - I can't infer a description for this setting.**

### Type: Windows.Data.Nlm. NlmSignature 

This type is multi-instance and must be retrieved using the following collection collection names:

* "wificloudstore3"
* "wifi3_wpa3"
* "wifi3_owe"

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readsettingsdata-exe.md).


### NlmSignature Properties

| Name | Type | Description |
|------|------|-------------|
| category | uint64 | Category of signature? *TBD - description in source seems tentative* |

## Windows Update

**TBD - I am placing this setting in the "common" page for now because the Word doc explicitly calls out that the setting is available and identical on Windows 10 and Windows 11. Catorization of settings is still TBD**

This setting is single instance.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR

**TBD - ALL DESCRIPTIONS TENTATIVE**

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| IsContinuousInnovationOptedIn | REG_DWORD | 0/1 | Enables devices to get the latest updates as soon as they’re released. |
| AllowMUUpdateService | REG_DWORD | 0/1 | Allows users to get other Microsoft products alongside with Windows Updates. |
| IsExpedited | REG_DWORD | 0/1 | User selects this to invoke device restart 15 min after all updates finished installing. |
| RestartNotificationsAllowed2 | REG_DWORD | 0/1 | Users decides if they want to be notified about the updates pending restart. |
