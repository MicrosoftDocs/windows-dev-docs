---
ms.assetid: 1901c4c2-5161-435d-bc7b-f40c69cdb138
title: Files, folders, and libraries
description: Learn about reading and writing app settings, file and folder pickers, and special sand-boxed locations such as the Video/Music library.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Files, folders, and libraries

You use the APIs in the [Windows.Storage](/uwp/api/Windows.Storage), [Windows.Storage.Streams](/uwp/api/Windows.Storage.Streams), and [Windows.Storage.Pickers](/uwp/api/Windows.Storage.Pickers) namespaces to read and write text and other data formats in files, and to manage files and folders. In this section, you'll also learn about reading and writing app settings, about file and folder pickers, and about special sand-boxed locations such as the Video/Music library.

| Topic | Description  |
|-------|--------------|
| [Enumerate and query files and folders](/windows/apps/develop/files/list-files-folders) | Access files and folders in either a folder, library, device, or network   location. You can also query the files and folders in a location by constructing file and folder queries. |
| [Create, write, and read a file](/windows/apps/develop/files/create-read-write-files) | Read and write a file using a [StorageFile](/uwp/api/Windows.Storage.StorageFile) object. |
| [Best practices for writing to files](/windows/apps/develop/files/best-practices-writing-files) | Learn best practices for using various file writing methods of the [FileIO](/uwp/api/windows.storage.fileio) and [PathIO](/uwp/api/windows.storage.pathio) classes. |
| [Get file properties](/windows/apps/develop/files/file-properties) | Get properties—top-level, basic, and extended—for a file represented by a   [StorageFile](/uwp/api/Windows.Storage.StorageFile) object. |
| [Open files and folders with a picker](quickstart-using-file-and-folder-pickers.md) | Access files and folders by letting the user interact with a picker. You can use the   [FolderPicker](/uwp/api/Windows.Storage.Pickers.FolderPicker) to gain access to a folder. |
| [Save a file with a picker](quickstart-save-a-file-with-a-picker.md) | Use [FileSavePicker](/uwp/api/Windows.Storage.Pickers.FileSavePicker) to let users specify the name and location where they want your app to save a file. |
| [Accessing HomeGroup content](quickstart-accessing-homegroup-content.md) | Access content stored in the user's HomeGroup folder, including pictures, music, and videos. |
| [Determining availability of Microsoft OneDrive files](/windows/apps/develop/files/determine-availability-microsoft-onedrive-files) | Determine if a Microsoft OneDrive file is available using the [StorageFile.IsAvailable](/uwp/api/windows.storage.storagefile.isavailable) property. |
| [Files and folders in the Music, Pictures, and Videos libraries](/windows/apps/develop/files/music-pictures-videos-libraries) | Add existing folders of music, pictures, or videos to the corresponding libraries. You can also remove folders from libraries, get the list of folders in a library, and discover stored photos, music, and videos. |
| [Track recently used files and folders](/windows/apps/develop/files/track-recently-used-files-folders) | Track files that your user accesses frequently by adding them to your app's most recently used list (MRU). The platform manages the MRU for you by sorting items based on when they were last accessed, and by removing the oldest item when the list's 25-item limit is reached. All apps have their own MRU. |
| [Track file system changes in the background](/windows/apps/develop/files/change-tracking-filesystem) | Track changes to the file system, even when the app isn't running.|
| [Access the SD card](access-the-sd-card.md) | You can store and access non-essential data on an optional microSD card, especially on low-cost mobile devices that have limited internal storage. |
| [File access permissions](/windows/apps/develop/files/file-access-permissions) | Apps can access certain file system locations by default. Apps can also access additional locations through the file picker, or by declaring capabilities. |
| [Fast access to file properties in UWP](/windows/apps/develop/files/fast-file-properties) | Efficiently gather a list of files and their properties from a library to use in a UWP app. |

## Related samples

[Folder enumeration sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FolderEnumeration)

[File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess)

[File picker sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FilePicker)
