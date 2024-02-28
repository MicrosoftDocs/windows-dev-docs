---
title: Reference for common Windows settings
description: This page lists the settings that are supported on Windows 10 and Windows 11, but were introduced in earlier Windows versions.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Reference for common Windows settings

**TBD - Still working on the language to frame these settings**
This page lists the settings that are supported on Windows 10 and Windows 11, but were introduced in earlier Windows versions. Link to [Settings back up and restore overview](index.md). Link to [Cloud Data Store Settings Reader Tool (readsettingdata.exe)](readsettingsdata-exe.md). Link to [Reference for Windows 11 settings](settings-windows-11.md). Link to [Reference for Windows 11 settings - TBD]().

## File Explorer Classic

Settings related to the classic Windows File Explorer.

### Type: Windows.Data.FileExplorerClassic.ShellStateSetting

### Properties

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


### Type: Windows.Data.FileExplorerClassic.CabinetStateSettings

### Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether the cabinet state is migrated from SSF. |
| fullPathTitle | bool | Indicates whether the cabinet state setting should show full path title. |
| saveLocalView | bool | Indicates whether the cabinet state setting should save local view. |
| newWindowMode | bool | Indicates whether the cabinet state setting should open in new window mode. |

### Type: Windows.Data.FileExplorerClassic.AdvancedSettings

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

### Type: Windows.Data.FileExplorerClassic.ExplorerSettings

### Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether Explorer settings are migrated from SSF. |
| underlineIconsAlways | bool | Indicates whether Explorer settings should always underline icons. |
| underlineIconsNever | bool | Indicates whether Explorer settings should never underline icons. |
| underlineIconsAsBrowser | bool | Indicates whether Explorer settings should underline icons as browser. |

### Type: Windows.Data.FileExplorerClassic.SearchSettings

### Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether search settings are migrated from SSF. |
| searchOnly | bool | Indicates whether search setting show search only. |
| wholeFileSystem | bool | Indicates whether search setting show the whole file system. |
| systemFolders | bool | Indicates whether search setting show system folders. |
| archivedFiles | bool | Indicates whether search setting show archived files. |

### Type: Type: Windows.Data.FileExplorerClassic.RegistrySettings

These are blobs that are in the registry. There are three things that use registry stream settings:

| Setting | Type |
|---------|------|
| RibbonQat | A single blob |
| Details Preview Settings | Two DWORDs (dwPreviewPaneSettings and dwReadingPaneSettings) |
| Navigation Pane Visibility | A bool |

### Properties

| Name | Type | Description |
|------|------|-------------|
| migratedFromSSF | bool | Indicates whether registry settings are migrated from SSF. |
| ribbonQat | blob | A single blob. |
| detailsPreviewPaneSettings | unit32 | Details about preview pane settings. |
| readingPaneSettings | unit32 | Indicates reading pane settings. |
| navigationPaneVisible | bool | Indicates whether navigation pane is visible or not. |

### Type: Windows.Data.FileExplorerClassic.FolderOptionGeneralSettings

These are settings found in File Explorer->Folder options(...)->General tab

•	File explorer can be opened to either Home or This PC 
•	File explorer can be opened to One Drive folder as well if user has signed in to One Drive (This option is available only if user has signed in)

### Properties

| Name | Type | Description |
|------|------|-------------|
| defaultOpenLocation | unit8 | Indicates whether file explorer can be opened to either home or this PC. Default is home (0). |
| browseFoldersInNewWindow | bool | Indicates whether to browse folder in new window. |
| useDoubleClickToOpen | bool | Indicates whether to use double click to open. |
| showRecentlyUsedFiles | bool | Indicates whether to show recently used files. |
| showFrequentlyUsedFolders | bool | Indicates whether to show frequently used folders. |
| showFilesFromOffice | bool | Indicates whether to show files from MS office. |

### Type: Windows.Data.FileExplorerClassic.FolderOptionsAdvancedSettings

These are settings found in File Explorer->Folder options(...)->View tab->Advanced settings

### Properties

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

### Type: Windows.Data.FileExplorerClassic.RecycleBinSettings

These are settings found in Recycle bin-> right click Properties

### Properties

| Name | Type | Description |
|------|------|-------------|
| displayDeleteConfirmationDialog | bool | Indicates whether to show delete confirmation dialog

### Type: Windows.Data.FileExplorerClassic.DesktopIconSettings

These are settings related to desktop icons.

### Properties

| Name | Type | Description |
|------|------|-------------|
| viewAutoArrangeIcons | bool | Indicates values for AutoArrangement of icons. |
| viewAlignIconsToGrid | bool | Indicates values for snapping to grid. |
| viewShowDesktopIcons | bool | Indicates values to view desktop icons. |
| sortColPropertyKeyFmtid | wstring	L"B725F130-47EF-101A-A5F1-02608C9EEBAC". |
| sortColPropertyKeyPid | Unit32 | 10. |
| sortColDirection | bool | 1=Ascending -1=Descending Default value is Ascending (true). |

