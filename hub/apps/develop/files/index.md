---
ms.assetid: 33d8df1e-898d-48b7-9399-1aee673490d9
title: Files, folders, and libraries with Windows App SDK
description: Learn about reading and writing app settings, file and folder pickers, and special sand-boxed locations such as the Video/Music library with Windows App SDK apps.
ms.date: 06/16/2023
ms.topic: article
keywords: windows 10, windows 11, windows, winui, windows app sdk, winrt, dotnet
ms.localizationpriority: medium
---
# Files, folders, and libraries with Windows App SDK

Packaged Windows App SDK apps can leverage the powerful APIs provided by the [Windows.Storage](/uwp/api/Windows.Storage), [Windows.Storage.Streams](/uwp/api/Windows.Storage.Streams), and [Windows.Storage.Pickers](/uwp/api/Windows.Storage.Pickers) namespaces to efficiently read and write various data formats in files, as well as manage files and folders. This section covers essential topics such as reading and writing app settings, file and folder pickers, and accessing special sand-boxed locations like the Video/Music library. Learn how to optimize your app's file management capabilities with the Windows App SDK.

Windows 10 and later OS releases provide a wide variety of APIs related to files, folders, libraries, and settings for apps. These features are available via a combination of WinRT and .NET APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk) and [.NET SDK](https://dotnet.microsoft.com/en-us/download).

## Read and write data with WinRT storage APIs

Packaged apps have access to all the WinRT storage APIs available to UWP apps. Whether you're migrating an existing UWP app or creating a new app, you can use these APIs to read and write data. For examples of using the storage APIs in a WinUI app, see [Access files and folders with Windows App SDK and WinRT APIs](winrt-files.md).

## Read and write data with .NET file APIs

In addition to the WinRT APIs, packaged apps can use the .NET APIs in the [System.IO](/dotnet/api/system.io) namespace to read and write data. When a new WinUI 3 project is created, its `Package.appxmanifest` file contains the following setting:

``` xml
<Capabilities>
  <rescap:Capability Name="runFullTrust" />
</Capabilities>
```

Declaring this restricted capability provides full access to the file system, registry, and other restricted capabilities. For more info, see [Restricted capability declarations](/windows/uwp/packaging/app-capability-declarations#restricted-capability-declarations). In other words, the app has the same access to the file system as any other .NET desktop app when using the .NET file APIs.

For examples of using the .NET APIs in a WinUI app, see [Access files and folders with Windows App SDK and .NET APIs](dotnet-files.md).

## Additional resources for working with files and folders

If you're developing packaged WinUI apps, the WinRT storage APIs can be a powerful tool for reading and writing data. The following UWP topics provide a wealth of information for developers looking to leverage these APIs in their apps.

| Topic | Description |
|-------|-------------|
| [Enumerate and query files and folders](/windows/uwp/files/quickstart-listing-files-and-folders) | Access files and folders in either a folder, library, device, or network   location. You can also query the files and folders in a location by constructing file and folder queries. |
| [Create, write, and read a file](/windows/uwp/files/quickstart-reading-and-writing-files) | Read and write a file using a [StorageFile](/uwp/api/Windows.Storage.StorageFile) object. |
| [Best practices for writing to files](/windows/uwp/files/best-practices-for-writing-to-files) | Learn best practices for using various file writing methods of the [FileIO](/uwp/api/windows.storage.fileio) and [PathIO](/uwp/api/windows.storage.pathio) classes. |
| [Get file properties](/windows/uwp/files/quickstart-getting-file-properties) | Get properties—top-level, basic, and extended—for a file represented by a   [StorageFile](/uwp/api/Windows.Storage.StorageFile) object. |
| [Open files and folders with a picker](/windows/uwp/files/quickstart-using-file-and-folder-pickers) | Access files and folders by letting the user interact with a picker. You can use the   [FolderPicker](/uwp/api/Windows.Storage.Pickers.FolderPicker) to gain access to a folder.<br/><br/>**NOTE:** In a desktop app (which includes WinUI 3 apps), you can use file and folder pickers from [Windows.Storage.Pickers](/uwp/api/windows.storage.pickers). However, if the desktop app requires elevation to run, you'll need a different approach because these APIs aren't designed to be used in an elevated app. For an example, see [FileSavePicker](/uwp/api/windows.storage.pickers.filesavepicker#in-a-desktop-app-that-requires-elevation). |
| [Save a file with a picker](/windows/uwp/files/quickstart-save-a-file-with-a-picker) | Use [FileSavePicker](/uwp/api/Windows.Storage.Pickers.FileSavePicker) to let users specify the name and location where they want your app to save a file. |
| [Accessing HomeGroup content](/windows/uwp/files/quickstart-accessing-homegroup-content) | Access content stored in the user's HomeGroup folder, including pictures, music, and videos. |
| [Determining availability of Microsoft OneDrive files](/windows/uwp/files/quickstart-determining-availability-of-microsoft-onedrive-files) | Determine if a Microsoft OneDrive file is available using the [StorageFile.IsAvailable](/uwp/api/windows.storage.storagefile.isavailable) property. |
| [Files and folders in the Music, Pictures, and Videos libraries](/windows/uwp/files/quickstart-managing-folders-in-the-music-pictures-and-videos-libraries) | Add existing folders of music, pictures, or videos to the corresponding libraries. You can also remove folders from libraries, get the list of folders in a library, and discover stored photos, music, and videos. |
| [Track recently used files and folders](/windows/uwp/files/how-to-track-recently-used-files-and-folders) | Track files that your user accesses frequently by adding them to your app's most recently used list (MRU). The platform manages the MRU for you by sorting items based on when they were last accessed, and by removing the oldest item when the list's 25-item limit is reached. All apps have their own MRU. |
| [Track file system changes in the background](/windows/uwp/files/change-tracking-filesystem) | Track changes to the file system, even when the app isn't running.|
| [Access the SD card](/windows/uwp/files/access-the-sd-card) | You can store and access non-essential data on an optional microSD card, especially on low-cost mobile devices that have limited internal storage. |
| [Fast access to file properties](/windows/uwp/files/fast-file-properties) | Efficiently gather a list of files and their properties from a library to use via the Windows Runtime APIs. |

## See also

[Access files and folders with Windows App SDK and WinRT APIs](winrt-files.md)

[Access files and folders with Windows App SDK and .NET APIs](dotnet-files.md)

[System.IO](/dotnet/api/system.io)
