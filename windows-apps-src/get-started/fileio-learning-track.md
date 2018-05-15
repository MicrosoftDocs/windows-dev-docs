---
author: TylerMSFT
title: Work with files
description: Learn how to work with files in the Universal Windows Platform.
ms.author: twhitney
ms.date: 05/01/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: get started, uwp, windows 10, learning track, files, file io, read file, write file, create file, write text, read text
ms.localizationpriority: medium
---
# Work with files

This topic covers what you need to know to get started reading from, and writing to, files in a UWP app. The main APIs and types are introduced, and links are provided to help you learn more.

This is not a tutorial. If you want a tutorial, see [Create, write, and read a file](https://docs.microsoft.com/windows/uwp/files/quickstart-reading-and-writing-files) which, in addition to demonstrating how to create, read, and write a file, shows how to use buffers and streams. You may also be interested in the [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess) which shows how to create, read, write, copy and delete a file, as well as how to retrieve file properties and remember a file or folder so that your app can easily access it again.

We’ll look at code to write and read text from a file, and how to access the app's local, roaming, and temporary folders (as well as the rest of the file system).

## What do you need to know

Here are the main types you need to know about to read or write text from/to a file:

- [Windows.Storage.StorageFile](https://docs.microsoft.com/uwp/api/windows.storage.storagefile) represents a file. This class has properties that provide information about the file, as well as methods for creating, opening, copying, deleting, and renaming files. You may be used to dealing with string paths. Although there are some UWP APIs that take a string path, more often you will probably use a  **StorageFile** to represent a file because some files you work with in UWP may not have a path, or may have an unwiedly path. Use [StorageFile.GetFileFromPathAsync()](https://docs.microsoft.com/uwp/api/windows.storage.storagefile.getfilefrompathasync) to convert a file at a location described by a string path to a **StorageFile**. 
- The [FileIO](https://docs.microsoft.com/uwp/api/windows.storage.fileio) class provides an easy way to read and write text. This class can also read/write an array of bytes, or the contents of a buffer. This class is very similar to the [PathIO](https://docs.microsoft.com/uwp/api/windows.storage.pathio) class. The main difference is that instead of taking a string path, as **PathIO** does, it takes a **StorageFile**.
- [Windows.Storage.StorageFolder](https://docs.microsoft.com/uwp/api/windows.storage.storagefolder) represents a folder (directory). This class has methods for creating files, querying the contents of a folder, creating, renaming, and deleting folders, and properties that provide information about a folder. 

Some common ways to get a **StorageFolder** include by using [Windows.Storage.Pickers.FolderPicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.folderpicker) in which the user navigates to a folder to use, [Windows.Storage.ApplicationData.Current](https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.current) which provides the **StorageFolder** specific to one of folders local to the app like the local, roaming, and temporary folder, and [Windows.Storage.KnownFolders](https://docs.microsoft.com/uwp/api/windows.storage.knownfolders) which provides the **StorageFolder** for known libraries such as the Music library, Pictures library.

There is a close relationship between a **StorageFolder** and a **StorageFile**. For example, you will often use a **StorageFolder** to create, or get, instances of a **StorageFile**. Which is shown in the example that follows.

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

We first identify where the file is located that we will write to. `Windows.Storage.ApplicationData.Current.LocalFolder` provides access to the local data folder, which is created for your app when it is installed. See [Access the file system](#access-the-file-system) for details about the folders your app can access.

Then, we use **StorageFolder** to create the file (or open it if it already exists).

The **FileIO** class provides a convenient way to write text to the file. `FileIO.WriteTextAsync()` replaces the entire contents of the file with the provided text. `FileIO.AppendLinesAsync()` appends a collection of strings to the file--writing one string per line.

## Read text from a file

As with writing a file, reading a file starts with identifying where the file is located. In this case, we use the same location used in the example above. Then we'll use the the **FileIO** class to read its contents.

```csharp
Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile file = await storageFolder.GetFileAsync("test.txt");

string text = await Windows.Storage.FileIO.ReadTextAsync(file);
```

Read each line of the file into individual strings in a collection with `IList<string> contents = await Windows.Storage.FileIO.ReadLinesAsync(sampleFile);`

## Access the file system

In the UWP platform, folder access is restricted by default to ensure the integrity and privacy of the user's data. 

### App folders

When a UWP app is installed, several folders are created for it under c:\users\<user name>\AppData\Local\Packages\<app package identifier>\, to store, among other things, the app's local, roaming, and temporary files. The app doesn't need to declare any capabilities to access these folders, and these folders are not accessible by other apps. These folders are also removed when the app is uninstalled, making it easier to cleanly uninstall apps.

These are some of the app folders you will commonly use:

- **LocalState**: For data local to the current device goes. When the device is backed up, data in this directory is backed up to a device backup image in OneDrive. If the user resets or replaces the device, the data will be restored. Save local data that you don't want backed up to OneDrive in the **LocalCacheFolder**.
Access the local folder with `Windows.Storage.ApplicationData.Current.LocalFolder`.  Access the local cache folder with `Windows.Storage.ApplicationData.Current.LocalCacheFolder`.

- **RoamingState**: For data that should be replicated on all devices where the app is installed. Windows limits the amount of data that can be roamed, so only save user settings and small files here. Access the roaming folder with `Windows.Storage.ApplicationData.Current.RoamingFolder`.

- **TempState**: For data that may be deleted any time the app isn't running. Access the temporary folder with `Windows.Storage.ApplicationData.Current.TemporaryFolder`.

### Access the rest of the file system

To access the file system outside of these folders, a UWP app must declare its intent to access a specific user library by adding the corresponding capability to its manifest. The user is prompted when the app is installed to verify that they authorize accessing the specified library. If not, the app is not installed. There are capabilities to access the pictures, videos, and music libraries. See  [App capability declaration](https://docs.microsoft.com/windows/uwp/packaging/app-capability-declarations) for a full list. To get a **StorageFolder** for these libraries, use the [Windows.Storage.KnownFolders](https://docs.microsoft.com/uwp/api/windows.storage.knownfolders) class.

#### Documents library

Although there is a capability to access the user's documents library, that capability is restricted (meaning an app declaring it will be rejected in the Microsoft Store unless you follow a process to get special approval) and is not intended for general use. Instead, use the file or folder pickers (see [Open files and folders with a picker](https://docs.microsoft.com/windows/uwp/files/quickstart-using-file-and-folder-pickers) and [Save a file with a picker](https://docs.microsoft.com/windows/uwp/files/quickstart-save-a-file-with-a-picker)) to allow the user to navigate to the folder or file. When the user navigates to a folder or file, they have implicitly given permission for the app to access it and the system will allow access.

#### General access

Alternatively, your app can declare the restricted **broadFileSystem** capability in its manifest, which also requires Microsoft Store approval, and then the app can access any file that the user has access to without requiring the intervention of a file or folder picker.

For a comprehensive list of the locations that apps can access, see [File access permissions](https://docs.microsoft.com/windows/uwp/files/file-access-permissions).

## Useful APIs and docs

Here is a quick summary of APIs, and other useful documentation, to help get you started with working with files and folders.

### Useful APIs

| API | Description |
|------|---------------|
|  [Windows.Storage.StorageFile](https://docs.microsoft.com/uwp/api/windows.storage.storagefile) | Provides information about the file, as well as methods for creating, opening, copying, deleting, renaming files, and more. |
| [Windows.Storage.StorageFolder](https://docs.microsoft.com/uwp/api/windows.storage.storagefolder) | Provides methods for creating files, querying the contents of a folder, creating, renaming, and deleting folders, and properties that provide information about the folder. |
| [FileIO](https://docs.microsoft.com/uwp/api/windows.storage.fileio) |  Provides an easy way to read and write text. This class can also read/write an array of bytes, or the contents of a buffer. |
| [PathIO](https://docs.microsoft.com/uwp/api/windows.storage.pathio) | Provides an easy way to read/write text from/to a file given a string path for the file. This class can also read/write an array of bytes, or the contents of a buffer. |
| [DataReader](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader) & [DataWriter](https://docs.microsoft.com/uwp/api/windows.storage.streams.datawriter) |  Read and write buffers, and data types such as bytes, integers, Guids, TimeSpans, and more, from/to a stream. |
| [Windows.Storage.ApplicationData.Current](https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.current) | Provides access to the folders created for the app such as the local folder, roaming folder, temp folder, and more. |
| [Windows.Storage.Pickers.FolderPicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.folderpicker) |  Lets the user choose a folder and returns a **StorageFolder**. This is how you get access to locations that the app can't access by default. |
| [Windows.Storage.Pickers.FileOpenPicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.fileopenpicker) | Let's the user choose a file to open and returns a **StorageFile**. this is how you get access to a file that the app can't access by default. |
| [Windows.Storage.Pickers.FileSavePicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.filesavepicker) | Let's the user choose the file name, extension, and storage location for a file. Returns a **StorageFile**. this is how you save a file to a locaiton that the app can't access by default. |
|  [Windows.Storage.Streams namespace](https://docs.microsoft.com/uwp/api/windows.storage.streams) | Covers reading and writing with streams. In particular, look at the [DataReader](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader) and [DataWriter](https://docs.microsoft.com/uwp/api/windows.storage.streams.datawriter) classes which read and write buffers, datatypes such as bytes, integers, GUIDs, TimeSpans, and more. |

### Useful docs

| Topic | Description |
|-------|----------------|
| [Windows.Storage Namespace](https://docs.microsoft.com/uwp/api/windows.storage) | API docs. |
| [Files, folders, and libraries](https://docs.microsoft.com/windows/uwp/files/) | Conceptual docs. |
| [Create, write, and read a file](https://docs.microsoft.com/windows/uwp/files/quickstart-reading-and-writing-files) | Covers creating, reading, and writing a file for text, binary, and streams. |
| [Getting started storing app data locally](https://blogs.windows.com/buildingapps/2016/05/10/getting-started-storing-app-data-locally/#pCbJKGjcShh5DTV5.97) | In addition to covering best practices for saving local data, it discusses the purpose of the LocalSettings and LocalCache folder. |
| [Getting Started with Roaming App Data](https://blogs.windows.com/buildingapps/2016/05/03/getting-started-with-roaming-app-data/#RgjgLt5OkU9DbVV8.97) | A two-part series about how to use roaming app data to give your users a mobile experience. |
| [Guidelines for roaming application data](http://msdn.microsoft.com/library/windows/apps/hh465094) | Follow these guidelines when you design your app to include roaming app data. |
| [Store and retrieve settings and other app data](https://docs.microsoft.com/windows/uwp/design/app-settings/store-and-retrieve-app-data) | Gives a good overview of the various app data stores such as the local, roaming, and temporary folders. See the [Roaming data](https://docs.microsoft.com/windows/uwp/design/app-settings/store-and-retrieve-app-data#roaming-data) section in that topic for guidelines and additional information about writing data that roams between devices. |
| [Roaming ap data and the user experience](https://blogs.windows.com/buildingapps/2016/05/04/roaming-app-data-and-the-user-experience/#Y20DGEjSXlMyPOgl.97) | |
| [File access permissions](https://docs.microsoft.com/windows/uwp/files/file-access-permissions) | Information about which file system locations your app can access. |
| [Open files and folders with a picker](https://docs.microsoft.com/windows/uwp/files/quickstart-using-file-and-folder-pickers) | Shows how to access files and folders by letting the user decide via a picker UI. |
| [Windows.Storage.Streams](https://docs.microsoft.com/uwp/api/windows.storage.streams) | Covers types used to read and write streams. |
| [Files and folders in the Music, Pictures, and Videos libraries](https://docs.microsoft.com/windows/uwp/files/quickstart-managing-folders-in-the-music-pictures-and-videos-libraries) | Shows how to add existing folders of music, pictures, or videos to the corresponding libraries. You can also remove folders from libraries, get the list of folders in a library, and discover stored photos, music, and videos. |

## Useful code samples

| Code sample | Description |
|-----------------|---------------|
| [Application data sample](https://code.msdn.microsoft.com/windowsapps/ApplicationData-sample-fb043eb2) | Shows you how to store and retrieve data that is specific to each user and Windows Runtime app by using the Windows Runtime application data APIs (Windows.Storage.ApplicationData and so on). |
| [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess) | Shows how to create, read, write, copy and delete a file, how to retrieve file properties, and how to track a file or folder so that your app can access it again. |
| [File picker sample](http://code.msdn.microsoft.com/windowsapps/File-picker-sample-9f294cba) | Shows how to access files and folders by letting the user choose them through the file pickers and how to save a file so that the user can specify the name, file type, and location of a file to save. |
| [JSON sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Json) | Shows how to encode and decode JavaScript Object Notation (JSON) objects, arrays, strings, numbers and booleans using classes in the [Windows.Data.Json namespace](https://docs.microsoft.com/uwp/api/Windows.Data.Json). |
| [Additional code samples](https://developer.microsoft.com//windows/samples) | Choose **Files, folder, and libraries** in the category drop-down list to see the file related code samples. |
