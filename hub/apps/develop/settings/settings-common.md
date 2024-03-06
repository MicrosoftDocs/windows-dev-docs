---
title: Reference for Windows 11 and Windows 10 settings
description: This page lists the settings that are supported by both Windows 10 and Windows 11.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Reference for Windows 11 and Windows 10 settings

The information provided on this page includes details for accessing the status of Windows Backup and Restore settings that are supported for both Windows 10 and Windows 11. This public documentation ensures effective data portability by providing third-party developers with a streamlined process to access the data. Settings that are supported on Windows 11 only are documented in [Reference for Windows 11 settings](settings-windows-11.md).

Settings status is accessed in one of two ways:

1. Via the Windows registry: For settings below that include registry details, please use that information to access the settings.
1. Via the Cloud Data Store Reader tool. These settings must be extracted from a data store to be readable. If the setting below does not list registry details, then the settings must be extracted using the Cloud Data Store Reader tool. For information on how to use this tool, see [Cloud Data Store Settings Reader Tool (readCloudDataSettings.exe)](readclouddatasettings-exe.md).

## App Compatability

The app compatibility setting is a JSON file that describes compatibility information for apps installed on the device. The path to this JSON is:

`C:\Windows\appcompat\Backup\[user SID].json`

The format of the backup JSON file. Install, Update, and Uninstall nodes contain arrays of applications, which contain arrays of files. The following code segment describes the format of the file and provides descriptions for each field.

```json
{
  "Install": [
    {
      "path": App's uninstall registry path,
      "programId": The unique identifier of the installed Win32 application,
      "compatFlags": Applicable backup/restore compatibility flags OR'ed together,
      "restoreAction": Actions to be taken on app restore,
      "files": [
        {
          "name": File name,
          "path": File path,
          "osComponent": Boolean stating if the file is an OS file,
          "size": The file size as a 32-bit value,
          "magic": The PE header's magic number,
          "peHeaderHash": Hash of the file's PE header,
          "sizeOfImage": PE header's SizeOfImage value,
          "peChecksum": PE header's CheckSum value,
          "linkDate": PE header's TimeDateStamp value,
          "linkerVersion": PE header's MarjorImageVersion and MinorImageVersion,
          "binFileVersion": File version obtained from GetFileVersionInfo,
          "binProductVersion": Product version obtained from GetFileVersionInfo,
          "binaryType": Type of binary (e.g. PE64_AMD64),
          "created": File creation time obtained from file system,
          "modified": File modification time obtained from file system,
          "lastAccessed": File access time obtained from file system,
          "verLanguage": Language obtained from GetFileVersionInfo,
          "id": Unique identifier obtained from hashing file contents,
          "switchBackContext": Value for OS runtime compatibility fixes,
          "sigDisplayName": Display name obtained from the file signature,
          "sigPublisherName": Publisher name obtained from the file signature,
          "sigMoreInfoURL": URL obtained from the file signature,
          "fileVersion": File version obtained from GetFileVersionInfo,
          "companyName": Company name obtained from GetFileVersionInfo,
          "fileDescription": File description obtained from GetFileVersionInfo,
          "internalName": Internal name obtained from GetFileVersionInfo,
          "legalCopyright": Copyright information obtained from GetFileVersionInfo,
          "originalFileName": Original filename obtained from GetFileVersionInfo,
          "productName": Product name obtained from GetFileVersionInfo,
          "productVersion": Product version obtained from GetFileVersionInfo,
          "peImageType": Image type obtained from PE header,
          "peSubsystem": Subsystem obtained from PE header,
          "runLevel": Executable's runl”evel obtained from app manifest,
          "uiAccess": UI access obtained from app manifest,
          "crcChecksum": File's CRC checksum,
          "clrVersion": CLR version obtained from app manifest,
          "boeProgramId": Unique ID describing the application,
          "boeProgramName": Same as "productName", if it exists. Otherwise same as "name",
          "boeProgramPublisher": Same as "companyName", if it exists. Otherwise same as "fileDescription", if it exists,
          "boeProgramVersion": Same as "productVersion", if it exists. Otherwise same as "fileVersion", if it exists. Otherwise same as "binProductVersion", if it exists. Otherwise same as "binFileVersion", if it exists,
          "boeProgramLanguage": Same as "verLanguage", if it exists,
          "fileSize": File's size as a 64-bit number,
          "peCharacteristics": Image characteristics obtained from PE header,
          "sha256": SHA256 hash of file,
        }
      ]
    },
  ],
  "Update": [
    { }
  ],
  "Uninstalled": [
    { }
  ]
}

```

## Autoplay

This setting helps to set defaults for removable drives and memory cards

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers


| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| DisableAutoplay | REG_DWORD | 0/1 | Toggle the use of autoplay for all media and devices. |
| UserChosenExecuteHandlers\CameraAlternate\ShowPicturesOnArrival | REG_SZ | A string. | User selected default autoplay behavior for removable drive. See table below for supported values. |
| UserChosenExecuteHandlers\ StorageOnArrival | REG_SZ | A string. | User selected default autoplay behavior for memory card. See table below for supported values. |
| EventHandlersDefaultSelection\CameraAlternate\ShowPicturesOnArrival | REG_SZ | A string. | System default autoplay behavior for removable drive. See table below for supported values. |
| EventHandlersDefaultSelection\StorageOnArrival | REG_SZ | A string. | System default autoplay behavior for memory card. See table below for supported values. |

#### Supported data values for ShowPicturesOnArrival

| Data value | Description |
|------------|-------------|
| dsd9eksajf9re3669zh5z2jykhws2jy42gypaqjh1qe66nyek1hg!content!import | Import Photos and Videos (Photos) |
| MSPlayMediaOnArrival | Play media on arrival. |
| MSOpenFolder | Open folder. |
| MSPromptEachTime | Prompt each time. |
| OneDriveAutoPlay | Import Photos and Videos (OneDrive). |
| MSTAKENOACTION | Take no action. |

#### Supported data values for StorageOnArrival

| Data value | Description |
|------------|-------------|
| MSTAKENOACTION | Take no action. |
| MSOpenFolder | Open folder. |
| MSStorageSense | Configure Storage Settings (Settings). |
| MSPromptEachTime | Prompt each time. |

## Backgound

Setting for managing and personalizing the desktop background.  

### Type: Windows.Data.Background.WallpaperPosition enumeration

#### WallpaperPosition values

| Name | Value | Description |
|------|-------|---------|
| Fill | 0   | Fill. |
| Fit  | 1  | Fit.  |
| Stretch | 2  | Stretch. |
| Tile | 3  | Tile. |
| Center | 4  | Center. |
| Span | 5  | Span |

### Type: Windows.Data.Background.WallpaperKind enumeration

#### WallpaperKind values

| Name | Value | Description |
|------|-------|---------|
| SolidColor | 0   | Solid color. |
| Image | 1  | Image.  |
| Slideshow | 2  | Slideshow. |
| Spotlight | 3  | Spotlight. |


### Type: Windows.Data.Background.DesktopWallpaper structure

The scope of this type is per device.

#### DesktopWallpaper Properties

| Name | Type | Description |
|------|------|-------------|
| kind | **WallpaperKind** | Specifies whether current background is set as Wallpaper or Solid Color or Slideshow or Spotlight. |
| position | **WallpaperPosition** | Specifies how wallpaper or slideshow images are positioned on background. |
| color | Windows.Data.Common.Color | Specifies the solid color value if background is selected as Solid Color |
| itemId | wstring | The unique ID for the wallpaper or slideshow uploaded during backup.  |
| contentUri | wstring | The url ffor the wallpaper or slideshow uploaded during backup.  |
| intervalInSeconds | uint64 | The interval between images of slideshow if background is selected as slideshow. |
| shuffle | bool | Signifies whether slideshow images are shuffled if background is selected as slideshow |
| syncRootRelativePath | wstring | Signifies path to slideshow folder if background is selected as slideshow. |



## Calling

The settings below are for a deprecated Windows calling experience and are no longer read by the operating system, however the settings data may be present on user devices or in the cloud.

### Type: Windows.data.calling.Settings structure

#### Settings Properties

| Name | Type | Description |
|------|------|-------------|
| perKeySettings | map&lt;wstring, KeyPathSettings&gt; | A map of per-key settings |

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
| Backup_disabled | 2   | Backup disablesd. |

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
| rgbSelectedText1  | **Windows.Data.Common.Color**   | Signifies color value of Text of highlighted text. |
| rgbSelectedText2  | **Windows.Data.Common.Color**   | Signifies color value of Text of highlight. |
| rgbButtonText1  | **Windows.Data.Common.Color**   | Signifies color value of Text of button text. |
| rgbButtonText2  | **Windows.Data.Common.Color**   | Signifies color value of Face of button. |

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
| start | REG_DWORD | 3/4 | 3: Set timezone toggle value on. 4: Set Timezone toggle value off. |

### Registry values under HKLM\SYSTEM\CurrentControlSet\Control\TimeZoneInformation

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| timeZone | REG_SZ | System timezone as string.  | The currently set time zone for the system. |



## Do not disturb

Set "do not disturb" status manually or automatically, so that notifications will be sent directly to the notification center. 

### Type: Windows.Data.DoNotDisturb.ChangeReason enumeration

#### ChangeReason values

| Name | Value | Description |
|------|-------|---------|
| Default | 0   | Default setting. |
| User | 1  | User setting.  |

### Type: BoolWithMetadata structure

#### BoolWithMetadata properties

| Name | Value | Description |
|------|-------|---------|
| value  | bool   | The boolean value. |
| changeReason  | **ChangeReason**  | The reason a user changed their profile.|

### Type: Windows.Data.DoNotDisturb.QuietHoursProfile structure

This setting is multi-instance. The following is an example command line for retrieving this type:

`readCloudDataSettings.exe enum -type:windows.data.donotdisturb.QuietHoursProfile`

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readclouddatasettings-exe.md).

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

This setting is multi-instance. The following is an example command line for retrieving this type:

`readCloudDataSettings.exe enum -type:windows.data.donotdisturb.quietmoment`

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readclouddatasettings-exe.md).

#### QuietMoment Properties

| Name | Type | Description |
|------|------|-------------|
| isInitialized | bool | CDS data initialize status. Default value is false. |
| isEnabled | bool | Settings enabled or disabled. |
| assignedProfile | wstring | The value of the user profile set by automatic rules. "Unrestricted", "Priority Only", or "Alarms Only"  |
| startTime | TimeSpan | When automatic rules should start. |
| endTime | TimeSpan | When automatic rules should end. |
| repeatType | uint32 | How frequently the rules should apply. "Daily", "Weekend", or "Weekdays"|
| shouldShowActiveToast | BoolWithMetadata | Sets whether the active toast should be shown and the associated reason.  |

## File Explorer Classic

Settings related to the classic Windows File Explorer.

### Type: Windows.Data.FileExplorerClassic.ShellStateSetting structure

#### ShellStateSetting Properties

| Name | Type | Description |
|------|------|-------------|
| bmigratedFromSSF | bool | Indicates whether the shell state is migrated from legacy setting framework to current solution. |
| bshowAllObjects | bool | Indicates whether Shell state should show all objects. |
| bshowExtensions | bool | Indicates whether Shell state should show all extensions. |
| bshowCompColor | bool | Indicates whether Shell state should show all colors. |
| bdoubleClickInWebView | bool | Indicates whether Shell state should show all objects. |
| bdontPrettyPath | bool | Indicates whether Shell state should show pretty path |
| showInfoTip | bool | Indicates whether Shell state should show info tip. |
| noConfirmRecycle | bool | Indicates whether Shell state should should confirm recycle option. |
| showSuperHidden | bool | Indicates whether Shell state should show super hidden. |
| sepProcess | bool | Indicates whether Shell state should show the separation process. |
| iconsOnly | bool | Indicates whether Shell state should show icons only. |
| showTypeOverlay | bool | Indicates whether Shell state should show type overlay. |


### Type: Windows.Data.FileExplorerClassic.CabinetStateSettings structure

#### CabinetStateSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether the cabinet state is migrated from legacy setting framework to current solution. |
| fullPathTitle | bool | Indicates whether the cabinet state setting should show full path title. |
| saveLocalView | bool | Indicates whether the cabinet state setting should save local view. |
| newWindowMode | bool | Indicates whether the cabinet state setting should open in new window mode. |

### Type: Windows.Data.FileExplorerClassic.AdvancedSettings structure

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether advanced settings are migrated from legacy setting framework to current solution. |
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
| migratedFromSSF | bool | Indicates whether the explorer settings are migrated from legacy setting framework to current solution. |
| underlineIconsAlways | bool | Indicates whether Explorer settings should always underline icons. |
| underlineIconsNever | bool | Indicates whether Explorer settings should never underline icons. |
| underlineIconsAsBrowser | bool | Indicates whether Explorer settings should underline icons as browser. |

### Type: Windows.Data.FileExplorerClassic.SearchSettings structure

#### SearchSettings Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether the search settings are migrated from legacy setting framework to current solution. |
| searchOnly | bool | Indicates whether search setting show search only. |
| wholeFileSystem | bool | Indicates whether search setting show the whole file system. |
| systemFolders | bool | Indicates whether search setting show system folders. |
| archivedFiles | bool | Indicates whether search setting show archived files. |

### Type: Type: Windows.Data.FileExplorerClassic.RegistrySettings structure

These are blobs that are in the registry. There are three things that use registry stream settings:

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether the registry settings are migrated from legacy setting framework to current solution. |
| RibbonQat | blob | A single blob. |
| detailsPreviewPaneSettings| unit32 | Details about preview pane settings. |
| readingPaneSettings | unit32 | Indicates reading pane settings. |
| navigationPaneVisible | bool | Indicates whether navigation pane is visible or not.  |

#### RegistrySettingsProperties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether registry settings are migrated from legacy setting framework to current solution. |
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
| sortColPropertyKeyFmtid | wstring | L"B725F130-47EF-101A-A5F1-02608C9EEBAC". |
| sortColPropertyKeyPid | Unit32 | 10. |
| sortColDirection | bool | 1=Ascending -1=Descending Default value is Ascending (true). |



## Input Method Editors (IME)

History files are used to optimize the Japanese IME user experience across devices.

### Type: Windows.Data.Input.HistoryFiles structure

This type is multi-instance and must be retrieved using the following collection collection names:

* "historyfiles"

The following is an example command line for retrieving this type:

`readCloudDataSettings.exe enum -type:Windows.Data.Input.HistoryFiles -collection:historyfiles`

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readclouddatasettings-exe.md).

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
| funItems | bool | Specifies whether “Get fun facts, tips, tricks, and more on your lock screen" is enabled. |
| itemId | wstring | The unique ID for the lockscreen wallpaper uploaded to during backup.  |
| contentUri | wstring | The url for the lockscreen wallpaper uploaded to during backup |
| lockScreenStatus | wstring | Not used. |
| slideShowAutoLock |   | Not used. |
| slideShowEnabledOnBattery |   | Not used. |
| slideshowOptimizePhotoSelection |   |Not used.  |
| slideShowIncludeCameraRoll |  |Not used.  |
| slideShowDuration |   | Not used.  |
| syncRootRelativePaths |   | Not used. |


## Lunar calendar

Settings related to the lunar Calendar in the task bar. 


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


## Multiple displays

Settings related to multiple displays. 


### Type: Windows.Data.Settings.DisplaySettings.MultipleDisplays structure

The scope of this type is per device.

#### MultipleDisplays Properties

| Name | Type | Description |
|------|------|-------------|
| rememberWindowLocationsPerMonitorConnection | nullable&lt;bool&gt; | Enables remember window locations per monitor connection |
| minimizeWindowsOnMonitorDisconnect | nullable&lt;bool&gt; | Enables minimize windows on monitor disconnect. |
| easeCursorMovementBetweenDisplays | nullable&lt;bool&gt; | Enables minimize windows on monitor disconnect. |

## Nightlight

Settings related to changing screen lighting or set brightness for certain hours.

### Type: Windows.Data.BlueLightReduction.ScheduleTime structure

The scope of this type is per device.

#### ScheduleTime Properties

| Name | Type | Description |
|------|------|-------------|
| hour | int8 | Hours. |
| minute | int8 | Minutes.  |

### Type: Windows.Data.BlueLightReduction.Settings structure

#### BlueLightReduction.Settings Properties

| Name | Type | Description |
|------|------|-------------|
| automaticOnSchedule | bool | Specifies whether blue light reduction is automatically turned on or off based on a schedule. |
| automaticOnSunset | bool | Specifies if blue light reduction schedule is automatically set based on sunrise and sunset. |
| manualScheduleBlueLightReductionOnTime | **ScheduleTime** | The start time of blue light reduction for a user manually setting their schedule. |
| manualScheduleBlueLightReductionOffTime | **ScheduleTime** | The end time of blue light reduction for a user manually setting their schedule. |
| targetColorTemperature | **int16** | The target color temperature (in Kelvin) for blue light reduction. |
| sunsetTime | **ScheduleTime** | The scheduled sunset time for blue light reduction.  |
| sunriseTime | **ScheduleTime** | he scheduled sunrise time for blue light reduction.  |
| previewColorTemperatureChanges | bool | Specifies whether blue light reduction color temperature changes should be previewed. |
| darkMode | bool | Specifies whether app mode should change when blue light reduction is turned on or off |

### Type: Windows.Data.BlueLightReduction.ActiveState enumeration

#### ActiveState values

| Name | Value | Description |
|------|-------|---------|
| BlueLightReductionOn | 0   |  |
| BlueLightReductionOff | 1  |  |

### Type: Windows.Data.BlueLightReduction.ChangeSource enumeration

#### ChangeSource values

| Name | Value | Description |
|------|-------|---------|
| Schedule | 0   |  |
| User | 1  |  |

### Type: Windows.Data.BlueLightReduction.BlueLightReductionState structure

#### BlueLightReductionState Properties

| Name | Type | Description |
|------|------|-------------|
| state | **ActiveState** | The current state of blue light reduction. |
| source | **ChangeSource** | Where the change came from, user change or scheduled change. |
| timestampUTC | int64 | The time the change in active state was applied. |
| isSupported | bool |  Whether or not current configuration supports blue light reduction. |


## NlmSignature

Settings related to Nlm signatures. Each network is uniquely identified with a network signature based on the uniquely identifiable properties of that network.

### Type: Windows.Data.Nlm.NlmSignature structure

This type is multi-instance and must be retrieved using the following collection names:

* "wificloudstore3"
* "wifi3_wpa3"
* "wifi3_owe"

The following is an example command line for retrieving this type:

`readCloudDataSettings.exe enum -type:windows.data.nlm.nlmsignature -collection:wificloudstore3`

For more information on retrieving multi-instance settings, see [Cloud Data Store Settings Reader Tool](readclouddatasettings-exe.md).


#### NlmSignature Properties

| Name | Type | Description |
|------|------|-------------|
| category | uint64 | Category of signature. |

## Pen and Windows Ink

Settings related to pen and Windows Ink.

### Type: Windows.Data.Input.Devices.PenPerDevice structure

#### PenPerDevice Properties

| Name | Type | Description |
|------|------|-------------|
| singleClickOverride | uint64 | Any integer value 0 - 13. Value corresponds to options available for Single Click setting dropdown. |
| singleClickPenWorkspaceVerb | uint64 | Any integer value 0-3. Value corresponds to options available for Single Click setting dropdown. |
| doubleClickOverride | uint64 | Any integer value 0 - 13. Value corresponds to options available for Double Click setting dropdown. |
| doubleClickPenWorkspaceVerb | uint64 | Any integer value 0-3. Value corresponds to options available for Double Click setting dropdown. |
| longPressOverride | uint64 | Any integer value 0 - 13. Value corresponds to options available for long press setting dropdown. |
| longPressPenWorkspaceVerb | uint64 | Any integer value 0-3. Value corresponds to options available for Long Press setting dropdown. |
| penWorkspaceAppLaunchOnPenDetachEnabled | bool | Specifies whether the pen menu is shown after pen is removed from storage. |
| penEnablePenButtonOverride | bool | Specifies whether apps are allowed to override the shortcut button behavior. |
| singleClickCustomAppPath | wstring | Specifies the path to the app opened on single-click.|
| doubleClickCustomAppPath | wstring | Specifies the path to the app opened on double-click. |
| longPressCustomAppPath | wstring | Specifies the path to the app opened on long-press. |
| singleClickCustomAppID | wstring |  Specifies the app ID app opened on single-click. |
| doubleClickCustomAppID | wstring | Specifies the app ID app opened on double-click. |
| longPressCustomAppID | wstring | Specifies the app ID app opened on long-press. |

Supported  override and verb values:

| Dropdown Option Selected | Override | PenWorkspaceVerb | Override (Win10) | PenWorkspaceVerb (Win10) |
|--------------------------|----------|------------------|------------------|--------------------------|
| Nothing | 1 | 0/1 | 1 | 0 |
| Screen Snipping | 8 | 0/1 | 8 | 0 |
| Pen Menu | 5 | 0 | 5 | 0 |
| Whiteboard | 9 | 0 | 5 | 2 |
| One Note | 6 | 0/1 | 0 | 0 |
| One Note Quick Notes | 13 | 0/1 | NA | NA |
| Sticky Notes | 5 | 1 | 5 | 1 |
| Snip & Sketch | NA | NA | 5 | 3 |
| Open Program | 3 | 0 | 3 | 0 |
| Open App | 2 | 0 | 2 | 0 |


## Personalization - colors

Settings related to system colors.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| AppsUseLightTheme | REG_DWORD | 0/1 | Signifies light/dark color mode for an app. |
| SystemUsesLightTheme | REG_DWORD | 0/1 | Signifies light/dark color mode for Windows. |
| EnableTransparency | REG_DWORD | 0/1 | Signifies the transparency effect on windows and surfaces. |
| ColorPrevalance | REG_DWORD | 0/1 | Signifies the toggle state for "Show Accent Color on Start and Taskbar". |

### Registry values under HKCU\Control Panel\Desktop

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| AutoColorization | REG_DWORD | 0/1 | signifies auto-apply accent color based on background or manually. |

### Registry values under HKCU\Software\Microsoft\Windows\DWM

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| ColorPrevalence | REG_DWORD | 0/1 | Signifies toggle state for "show Accent color in title bar and windows borders". |

## Personalization - Start - Folders

Specifies the folders that are shown at the bottom of the Start menu.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Start

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| VisiblePlaces | REG_BINARY | A vector of GUIDs. | A list of GUIDs indicating the folders that are shown at the bottom of Start.  |

The folder GUIDs that are supported for *Start_Layout* are:
• Documents: {2D34D5CE-FA5A-4543-82F2-22E6EAF7773C}
• Downloads: {E367B32F-89DE-4355-BFCE-61F37B18A937}
• Music: {B00B0620-7F51-4C32-AA1E-34CC547F7315}
• Pictures: {383F07A0-E80A-4C80-B05A-86DB845DBC4D}
• Videos: {42B3A5C5-7D86-42F4-80A4-93FACA7A88B5}
• Network: {FE758144-080D-42AE-8BDA-34ED97B66394}
• UserProfile: {74BDB04A-F94A-4F68-8BD6-4398071DA8BC}
• Explorer: {148A24BC-D60C-4289-A080-6ED9BBA24882}
• Settings: {52730886-51AA-4243-9F7B-2776584659D4}

## Personalization - Start - Layout

Specifies the start layout.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Start

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| Config | REG_BINARY | A JSON file.  | The contents of the Pinned Layout in JSON. |

## Personalization - Start - Show recent apps

Specifies whether apps that were recently installed in Start in various surfaces on are shown on Start.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Start

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| ShowRecentList | REG_BOOL | 0 or 1  | Specifies whether apps that were recently installed in Start in various surfaces on are shown on Start. |

## Personalization - Start - Show recommended apps

Specifies whether recommended files in Start, recent files in File Explorer, and items in Jump Lists are shown.

### Registry values under HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| Start_TrackDocs | REG_BOOL | 0 or 1  | Specifies whether recommended files in Start, recent files in File Explorer, and items in Jump Lists are shown. |

## Personalization - Taskbar - Badges

This setting enables badges for apps on the taskbar.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarBadges

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| SystemSettings_Taskbar_Badging | REG_SZ | 0 or 1 | Enables badges for apps on the taskbar. |
| SystemSettings_DesktopTaskbar_Badging | REG_SZ | 0 or 1 | Enables badges for apps on the taskbar. |

## Personalization - Taskbar - Combine buttons, mult-monitor

This setting enables combining buttons and hiding labels on the taskbar on multiple monitors.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\MMTaskbarGlomLevel

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| SystemSettings_DesktopTaskbar_GroupingMode | REG_SZ | 0, 1, or 2 | 0: Always, 1: When taskbar is full, 2: Never. |

## Personalization - Taskbar - Multi-monitor

Enables showing the taskbar on multiple displays.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\MMTaskbarEnabled

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| SystemSettings_Taskbar_MultiMon | REG_SZ | 0 or 1 | Enables showing the taskbar on multiple displays. |
| SystemSettings_DesktopTaskbar_MultiMon | REG_SZ | 0 or 1 | Enables showing the taskbar on multiple displays. |

## Personalization - Taskbar - Multi-monitor taskbar mode

Specifies the behavior of the taskbar when displayed on multiple monitors.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\MMTaskbarMode

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| SystemSettings_Taskbar_MultiMonTaskbarMode | REG_SZ | 0, 1, or 2 | 0: Duplicate, 1: Primary and monitor window is on, 2: Monitor window is on. |
| SystemSettings_DesktopTaskbar_MultiMonTaskbarMode | REG_SZ | 0  1 | 0: Duplicate, 1: Primary and monitor window is on, 2: Monitor window is on. |

## Personalization - Taskbar - Pinned apps from other devices

Specifies the set of apps pinned to the taskbar from another device.

### Registry values under HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband\FavoritesMigration

| Registry value | Type | Data | Description |
|---------------|------|-------|------------|
| FavoritesMigration | REG_BLOB | A binary blob. | This is an opaque binary blob copied from the following location on the backed up.  |
| Favorites | REG_SZ | 0  1 | The format of this key is undocumented. |

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
| scope | wstring | Represents the “Sign in options" setting state. |

### Type: Windows.Data.Account.SecondaryAccounts structure

#### SecondaryAccounts Properties

| Name | Type | Description |
|------|------|-------------|
| accountDetails | vector&lt;AccountInfo&gt; | A vector of **AccountInfo** objects representing secondary accounts.  |

## Spelling dictionary

The user's custom spelling dictionary is stored in a file in the following file path:

`%userprofile%\AppData\Roaming\Microsoft\Spelling\neutral\default.dic`

## USB

This setting controls toggles such as connection notifications, battery saver and other notifications related to charging of PC.


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
| toggleState | map&lt;wstring, **CloudStorePolicyEnum**&gt; | The user preference in terms of mapping of sync policy and CloudStorePolicy option. Allowed values for sync policy names are listed below. |

Valid sync policy names:

* Microsoft
* MicrosoftDevice
* MicrosoftUserProfile
* Microsoft.Accessibility
* MicrosoftDevice.Accessibility
* MicrosoftUserProfile.Accessibility
* Microsoft.Credentials
* MicrosoftDevice.Credentials
* MicrosoftUserProfile.Credentials
* Microsoft.Personalization
* MicrosoftDevice.Personalization
* MicrosoftUserProfile.Personalization
* Microsoft.Language
* MicrosoftDevice.Language
* MicrosoftUserProfile.Language
* Microsoft.Default
* MicrosoftDevice.Default
* MicrosoftUserProfile.Default
* Microsoft.Ink
* MicrosoftDevice.Ink
* MicrosoftUserProfile.Ink



## Windows Update

Settings related to Windows Update.

### Registry values under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings

| Registry value | Type | Data | Description |
|---------------|------|-------|-------------|
| IsContinuousInnovationOptedIn | REG_DWORD | 0/1 | Enables devices to get the latest updates as soon as they’re released. |
| AllowMUUpdateService | REG_DWORD | 0/1 | Allows users to get other Microsoft products alongside with Windows Updates. |
| IsExpedited | REG_DWORD | 0/1 | User selects this to invoke device restart 15 min after all updates finished installing. |
| RestartNotificationsAllowed2 | REG_DWORD | 0/1 | Users decides if they want to be notified about the updates pending restart. |
