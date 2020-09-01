---
title: Work with files
description: Learn about the main APIs and types you need to get started reading from, and writing to, files in a Universal Windows Platform (UWP) app.
ms.date: 05/01/2018
ms.topic: article
keywords: get started, uwp, windows 10, learning track, files, file io, read file, write file, create file, write text, read text
ms.localizationpriority: medium
ms.custom: RS5
---
# Work with files

This topic covers what you need to know to get started reading from, and writing to, files in a Universal Windows Platform (UWP) app. The main APIs and types are introduced, and links are provided to help you learn more.

This is not a tutorial. If you want a tutorial, see [Create, write, and read a file](../files/quickstart-reading-and-writing-files.md) which, in addition to demonstrating how to create, read, and write a file, shows how to use buffers and streams. You may also be interested in the [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess) which shows how to create, read, write, copy and delete a file, as well as how to retrieve file properties and remember a file or folder so that your app can easily access it again.

We’ll look at code to write and read text from a file and how to access the app's local, roaming, and temporary folders.

## What do you need to know

Here are the main types you need to know about to read or write text from/to a file:

- [Windows.Storage.StorageFile](/uwp/api/windows.storage.storagefile) represents a file. This class has properties that provide information about the file, and methods for creating, opening, copying, deleting, and renaming files.
You may be used to dealing with string paths. There are some Windows Runtime APIs that take a string path, but more often you will use a  **StorageFile** to represent a file because some files you work with in UWP may not have a path, or may have an unwieldy path. Use [StorageFile.GetFileFromPathAsync()](/uwp/api/windows.storage.storagefile.getfilefrompathasync) to convert a string path to a **StorageFile**. 

- The [FileIO](/uwp/api/windows.storage.fileio) class provides an easy way to read and write text. This class can also read/write an array of bytes, or the contents of a buffer. This class is very similar to the [PathIO](/uwp/api/windows.storage.pathio) class. The main difference is that instead of taking a string path, as **PathIO** does, it takes a **StorageFile**.
- [Windows.Storage.StorageFolder](/uwp/api/windows.storage.storagefolder) represents a folder (directory). This class has methods for creating files, querying the contents of a folder, creating, renaming, and deleting folders, and properties that provide information about a folder. 

Common ways to get a **StorageFolder** include:

- [Windows.Storage.Pickers.FolderPicker](/uwp/api/windows.storage.pickers.folderpicker) which allows the user to navigate to the folder they want to use.
- [Windows.Storage.ApplicationData.Current](/uwp/api/windows.storage.applicationdata.current) which provides the **StorageFolder** specific to one of folders local to the app like the local, roaming, and temporary folder.
- [Windows.Storage.KnownFolders](/uwp/api/windows.storage.knownfolders) which provides the **StorageFolder** for known libraries such as the Music or Picture libraries.

## Write text to a file

 For this introduction, we will focus on a simple scenario: reading and writing text. Let's start by looking at some code that uses the **FileIO** class to write text to a file.

```csharp
Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync("test.txt",
        Windows.Storage.CreationCollisionOption.OpenIfExists);

await Windows.Storage.FileIO.WriteTextAsync(file, "Example of writing a string\r\n");

// Append a list of strings, one per line, to the file
var listOfStrings = new List<string> { "line1", "line2", "line3" };
await Windows.Storage.FileIO.AppendLinesAsync(file, listOfStrings); // each entry in the list is written to the file on its own line.
```

We first identify where the file should be located. `Windows.Storage.ApplicationData.Current.LocalFolder` provides access to the local data folder, which is created for your app when it is installed. See [Access the file system](#access-the-file-system) for details about the folders your app can access.

Then, we use **StorageFolder** to create the file (or open it if it already exists).

The **FileIO** class provides a convenient way to write text to the file. `FileIO.WriteTextAsync()` replaces the entire contents of the file with the provided text. `FileIO.AppendLinesAsync()` appends a collection of strings to the file--writing one string per line.

## Read text from a file

As with writing a file, reading a file starts with specifying where the file is located. We'll use the same location as in the example above. Then we'll use the **FileIO** class to read its contents.

```csharp
Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile file = await storageFolder.GetFileAsync("test.txt");

string text = await Windows.Storage.FileIO.ReadTextAsync(file);
```

You can also read each line of the file into individual strings in a collection with `IList<string> contents = await Windows.Storage.FileIO.ReadLinesAsync(sampleFile);`

## Access the file system

In the UWP platform, folder access is restricted to ensure the integrity and privacy of the user's data. 

### App folders

When a UWP app is installed, several folders are created under c:\users\<user name>\AppData\Local\Packages\<app package identifier>\ to store, among other things, the app's local, roaming, and temporary files. The app doesn't need to declare any capabilities to access these folders, and these folders are not accessible by other apps. These folders are also removed when the app is uninstalled.

These are some of the app folders you will commonly use:

- **LocalState**: For data local to the current device. When the device is backed up, data in this directory is saved in a backup image in OneDrive. If the user resets or replaces the device, the data will be restored. Access this folder with `Windows.Storage.ApplicationData.Current.LocalFolder.` Save local data that you don't want backed up to OneDrive in the **LocalCacheFolder**, which you can access with `Windows.Storage.ApplicationData.Current.LocalCacheFolder`.

- **RoamingState**: For data that should be replicated on all devices where the app is installed. Windows limits the amount of data that will roam, so only save user settings and small files here. Access the roaming folder with `Windows.Storage.ApplicationData.Current.RoamingFolder`.

- **TempState**: For data that may be deleted when the app isn't running. Access this folder with `Windows.Storage.ApplicationData.Current.TemporaryFolder`.

### Access the rest of the file system

A UWP app must declare its intent to access a specific user library by adding the corresponding capability to its manifest. The user is then prompted when the app is installed to verify that they authorize accessing the specified library. If not, the app is not installed. There are capabilities to access the pictures, videos, and music libraries. See  [App capability declaration](../packaging/app-capability-declarations.md) for a full list. To get a **StorageFolder** for these libraries, use the [Windows.Storage.KnownFolders](/uwp/api/windows.storage.knownfolders) class.

#### Documents library

Although there is a capability to access the user's documents library, that capability is restricted which means that an app declaring it will be rejected by the Microsoft Store unless you follow a process to get special approval. It is not intended for general use. Instead, use the file or folder pickers (see [Open files and folders with a picker](../files/quickstart-using-file-and-folder-pickers.md) and [Save a file with a picker](../files/quickstart-save-a-file-with-a-picker.md)) which allow the user to navigate to the folder or file. When the user navigates to a folder or file, they have implicitly given permission for the app to access it and the system allows access.

#### General access

Alternatively, your app can declare the restricted [broadFileSystem](../packaging/app-capability-declarations.md) capability in its manifest, which also requires Microsoft Store approval. Then the app can access any file that the user has access to without requiring the intervention of a file or folder picker.

For a comprehensive list of the locations that apps can access, see [File access permissions](../files/file-access-permissions.md).

## Useful APIs and docs

Here is a quick summary of APIs, and other useful documentation, to help get you started with files and folders.

### Useful APIs

| API | Description |
|------|---------------|
|  [Windows.Storage.StorageFile](/uwp/api/windows.storage.storagefile) | Provides information about the file, and methods for creating, opening, copying, deleting, and renaming files. |
| [Windows.Storage.StorageFolder](/uwp/api/windows.storage.storagefolder) | Provides information about the folder, methods for creating files, and methods for creating, renaming, and deleting folders. |
| [FileIO](/uwp/api/windows.storage.fileio) |  Provides an easy way to read and write text. This class can also read/write an array of bytes or the contents of a buffer. |
| [PathIO](/uwp/api/windows.storage.pathio) | Provides an easy way to read/write text from/to a file given a string path for the file. This class can also read/write an array of bytes or the contents of a buffer. |
| [DataReader](/uwp/api/windows.storage.streams.datareader) & [DataWriter](/uwp/api/windows.storage.streams.datawriter) |  Read and write buffers, bytes, integers, GUIDs, TimeSpans, and more, from/to a stream. |
| [Windows.Storage.ApplicationData.Current](/uwp/api/windows.storage.applicationdata.current) | Provides access to the folders created for the app such as the local folder, roaming folder, and temporary files folder. |
| [Windows.Storage.Pickers.FolderPicker](/uwp/api/windows.storage.pickers.folderpicker) |  Lets the user choose a folder and returns a **StorageFolder** for it. This is how you get access to locations that the app can't access by default. |
| [Windows.Storage.Pickers.FileOpenPicker](/uwp/api/windows.storage.pickers.fileopenpicker) | Lets the user choose a file to open and returns a **StorageFile** for it. This is how you get access to a file that the app can't access by default. |
| [Windows.Storage.Pickers.FileSavePicker](/uwp/api/windows.storage.pickers.filesavepicker) | Lets the user choose the file name, extension, and storage location for a file. Returns a **StorageFile**. This is how you save a file to a location that the app can't access by default. |
|  [Windows.Storage.Streams namespace](/uwp/api/windows.storage.streams) | Covers reading and writing streams. In particular, look at the [DataReader](/uwp/api/windows.storage.streams.datareader) and [DataWriter](/uwp/api/windows.storage.streams.datawriter) classes which read and write buffers, bytes, integers, GUIDs, TimeSpans, and more. |

### Useful docs

| Topic | Description |
|-------|----------------|
| [Windows.Storage Namespace](/uwp/api/windows.storage) | API reference docs. |
| [Files, folders, and libraries](../files/index.md) | Conceptual docs. |
| [Create, write, and read a file](../files/quickstart-reading-and-writing-files.md) | Covers creating, reading, and writing text, binary data, and streams. |
| [Getting started storing app data locally](https://blogs.windows.com/buildingapps/2016/05/10/getting-started-storing-app-data-locally/#pCbJKGjcShh5DTV5.97) | In addition to covering best practices for saving local data, covers  the purpose of the LocalSettings and LocalCache folder. |
| [Getting Started with Roaming App Data](https://blogs.windows.com/buildingapps/2016/05/03/getting-started-with-roaming-app-data/#RgjgLt5OkU9DbVV8.97) | A two-part series about how to use roaming app data. |
| [Guidelines for roaming application data](../design/app-settings/store-and-retrieve-app-data.md) | Follow these data roaming guidelines when you design your app. |
| [Store and retrieve settings and other app data](../design/app-settings/store-and-retrieve-app-data.md) | Provides an overview of the various app data stores such as the local, roaming, and temporary folders. See the [Roaming data](../design/app-settings/store-and-retrieve-app-data.md#roaming-data) section for guidelines and additional information about writing data that roams between devices. |
| [File access permissions](../files/file-access-permissions.md) | Information about which file system locations your app can access. |
| [Open files and folders with a picker](../files/quickstart-using-file-and-folder-pickers.md) | Shows how to access files and folders by letting the user decide via a picker UI. |
| [Windows.Storage.Streams](/uwp/api/windows.storage.streams) | Types used to read and write streams. |
| [Files and folders in the Music, Pictures, and Videos libraries](../files/quickstart-managing-folders-in-the-music-pictures-and-videos-libraries.md) | Covers how to remove folders from libraries, get the list of folders in a library, and discover stored photos, music, and videos. |

## Useful code samples

| Code sample | Description |
|-----------------|---------------|
| [Application data sample](/samples/microsoft/windows-universal-samples/applicationdata/) | Shows  how to store and retrieve data that is specific to each user by using the application data APIs. |
| [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess) | Shows how to create, read, write, copy and delete a file. |
| [File picker sample](/samples/microsoft/windows-universal-samples/filepicker/) | Shows how to access files and folders by letting the user choose them with UI, and how to save a file so that the user can specify the name, file type, and location of a file to save. |
| [JSON sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Json) | Shows how to encode and decode JavaScript Object Notation (JSON) objects, arrays, strings, numbers and booleans using the [Windows.Data.Json namespace](/uwp/api/Windows.Data.Json). |
| [Additional code samples](https://developer.microsoft.com/windows/samples) | Choose **Files, folder, and libraries** in the category drop-down list. |