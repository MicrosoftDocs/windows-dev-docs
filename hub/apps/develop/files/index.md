---
ms.assetid: 33d8df1e-898d-48b7-9399-1aee673490d9
title: Windows App SDK File Management - Read, Write, and Access Files
description: Learn how to read and write files, use file pickers, and access sandboxed libraries in Windows App SDK apps. Get code examples and best practices for file management.
ms.date: 09/10/2025
ms.topic: concept-article
keywords: windows 10, windows 11, windows, winui, windows app sdk, winrt, dotnet
ms.localizationpriority: medium
# customer intent: As a Windows app developer, I want to understand how to manage files and folders in my Windows App SDK app, so that I can read and write data, use file pickers, and access libraries.
---

# Manage files, folders, and libraries with Windows App SDK

Windows App SDK provides powerful file management capabilities for packaged apps. You can use APIs from the [Windows.Storage](/uwp/api/Windows.Storage), [Windows.Storage.Streams](/uwp/api/Windows.Storage.Streams), and [Microsoft.Windows.Storage.Pickers](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers) namespaces to efficiently read and write various data formats in files, as well as manage files and folders.

There are a wide variety of APIs related to files, folders, libraries, and settings for Windows desktop apps beginning in Windows 10. These features are available via a combination of Windows App SDK, Windows Runtime (WinRT), and .NET APIs provided by the [Windows App SDK](/windows/apps/windows-app-sdk/downloads), [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk), and [.NET SDK](https://dotnet.microsoft.com/en-us/download).

## Use Windows App SDK pickers to read and write data

There are picker APIs available in the Windows App SDK version 1.8 and later. The following topics provide information about using the picker APIs to let users open and save files and folders in your app:

| Topic | Description |
|-------|-------------|
| [Open files and folders with pickers in WinUI](using-file-folder-pickers.md) | Use Windows App SDK file and folder pickers to let users browse and select files or folders in your WinUI app. The picker APIs provide a familiar Windows experience that helps users navigate their device and cloud storage locations. Learn how to let users select a file or folder to open using the [FileOpenPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker) and [FolderPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.folderpicker) classes. |
| [Save a file with Windows App SDK picker in WinUI](pickers-save-file.md) | When building WinUI apps and other Windows client apps, users often need to save files like documents, images, or other content to specific locations on their device. The Windows App SDK provides the [FileSavePicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.filesavepicker) class to create a consistent, user-friendly interface that lets users choose where to save files and what to name them. |

### Key differences between WinRT and Windows App SDK pickers

Here are some key differences from the WinRT [Windows.Storage.Pickers](/uwp/api/windows.storage.pickers) used by UWP apps:

- Unlike the existing **Windows.Storage.Pickers** API which returns [StorageFile](/uwp/api/windows.storage.storagefile) and [StorageFolder](/uwp/api/windows.storage.storagefolder) objects, this new API returns string-based paths through [PickFileResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfileresult) and [PickFolderResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfolderresult) classes. This simplifies the API and avoids complications with storage capabilities in elevated scenarios.
- Similarly, the [FileSavePicker.SuggestedSaveFile](/uwp/api/windows.storage.pickers.filesavepicker.suggestedsavefile) property (which returned a **StorageFile**) has been replaced. Its functionality is now covered by two string properties: [SuggestedFolder](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.filesavepicker.suggestedfolder) and [SuggestedFileName](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.filesavepicker.suggestedfilename). These allow you to suggest the folder and file name for the save dialog.
- All new pickers are designed specifically for desktop apps and use a **WindowId** property to link the picker to its host window, replacing the `WinRT.Interop.InitializeWithWindow.Initialize` pattern.
- The new pickers allow developers to use them without requiring [FileTypeFilter](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.filetypefilter) or [FileTypeChoices](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.filesavepicker.filetypechoices) to be specified. While UWP pickers throw exceptions when these properties are not set, the new pickers default to showing all files when developers do not explicitly configure these properties.
- The **HomeGroup** location has been excluded from the new [PickerLocationId](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickerlocationid) enum as it is no longer supported starting in Windows 10.
- [FolderPicker.FileTypeFilter](/uwp/api/windows.storage.pickers.folderpicker.filetypefilter) has been excluded as it was not functionally appropriate for folder selection.
- Excluding methods and properties that are already marked as deprecated or "Do not use". e.g., **PickSingleFileAndContinue**, **ContinuationData**, **ResumePickSingleFileAsync**, etc.
- Excluding methods and properties for multi-user mode, because the new APIs are currently designed for desktop scenarios where each user has their own interactive session, and each session is completely independent of the other sessions on the device. This is in contrast to Xbox or other multi-user devices.

## Access data with WinRT storage APIs

Packaged apps have access to all the WinRT storage APIs available to UWP apps. Whether you're migrating an existing UWP app or creating a new app, you can use these APIs to read and write data. For examples of using the storage APIs in a WinUI 3 app, see [Access files and folders with Windows App SDK and WinRT APIs](winrt-files.md).

## Use .NET file APIs for data access

In addition to the WinRT APIs, packaged desktop apps can use the .NET APIs in the [System.IO](/dotnet/api/system.io) namespace to read and write data. When a new WinUI project is created, its `Package.appxmanifest` file contains the following setting:

``` xml
<Capabilities>
  <rescap:Capability Name="runFullTrust" />
</Capabilities>
```

Declaring this restricted capability provides full access to the file system, registry, and other restricted capabilities. For more info, see [Restricted capability declarations](/windows/uwp/packaging/app-capability-declarations#restricted-capability-declarations). In other words, the app has the same access to the file system as any other .NET desktop app when using the .NET file APIs.

For examples of using the .NET APIs in a WinUI app, see [Access files and folders with Windows App SDK and .NET APIs](dotnet-files.md).

## Additional resources for working with files and folders

If you're developing packaged WinUI apps, the WinRT storage APIs can be a powerful tool for reading and writing data. The following topics provide a wealth of information for developers looking to leverage these APIs in their apps.

> [!NOTE]
> The topics below link to WinRT API documentation which is applicable to both UWP and WinUI 3 (Windows App SDK) apps.

| Topic | Description |
|-------|-------------|
| [Enumerate and query files and folders](/windows/uwp/files/quickstart-listing-files-and-folders) | Access files and folders in either a folder, library, device, or network   location. You can also query the files and folders in a location by constructing file and folder queries. |
| [Create, write, and read a file](/windows/uwp/files/quickstart-reading-and-writing-files) | Read and write a file using a [StorageFile](/uwp/api/Windows.Storage.StorageFile) object. |
| [Best practices for writing to files](/windows/uwp/files/best-practices-for-writing-to-files) | Learn best practices for using various file writing methods of the [FileIO](/uwp/api/windows.storage.fileio) and [PathIO](/uwp/api/windows.storage.pathio) classes. |
| [Get file properties](/windows/uwp/files/quickstart-getting-file-properties) | Get properties—top-level, basic, and extended—for a file represented by a   [StorageFile](/uwp/api/Windows.Storage.StorageFile) object. |
| [Accessing HomeGroup content](/windows/uwp/files/quickstart-accessing-homegroup-content) | ~~Access content stored in the user's HomeGroup folder.~~ **Note: HomeGroup was removed in Windows 10 version 1803.** |
| [Determining availability of Microsoft OneDrive files](/windows/uwp/files/quickstart-determining-availability-of-microsoft-onedrive-files) | Determine if a Microsoft OneDrive file is available using the [StorageFile.IsAvailable](/uwp/api/windows.storage.storagefile.isavailable) property. |
| [Files and folders in the Music, Pictures, and Videos libraries](/windows/uwp/files/quickstart-managing-folders-in-the-music-pictures-and-videos-libraries) | Add existing folders of music, pictures, or videos to the corresponding libraries. You can also remove folders from libraries, get the list of folders in a library, and discover stored photos, music, and videos. |
| [Track recently used files and folders](/windows/uwp/files/how-to-track-recently-used-files-and-folders) | Track files that your user accesses frequently by adding them to your app's most recently used list (MRU). The platform manages the MRU for you by sorting items based on when they were last accessed, and by removing the oldest item when the list's 25-item limit is reached. All apps have their own MRU. |
| [Track file system changes in the background](/windows/uwp/files/change-tracking-filesystem) | Track changes to the file system, even when the app isn't running.|
| [Access the SD card](/windows/uwp/files/access-the-sd-card) | Store and access non-essential data on removable storage media (primarily applicable to devices with SD card slots). |
| [Fast access to file properties](/windows/uwp/files/fast-file-properties) | Efficiently gather a list of files and their properties from a library to use via the Windows Runtime APIs. |

## See also

[Access files and folders with Windows App SDK and WinRT APIs](winrt-files.md)

[Access files and folders with Windows App SDK and .NET APIs](dotnet-files.md)

[System.IO](/dotnet/api/system.io)
