---
ms.assetid: 3A404CC0-A997-45C8-B2E8-44745539759D
title: File access permissions
description: Apps can access certain file system locations by default. Apps can also access additional locations through the file picker, or by declaring capabilities.
ms.date: 01/28/2022
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
  - cpp
  - javascript
---

# File access permissions

Universal Windows Platform (UWP) apps can access certain file system locations by default. Apps can also access additional locations through the file picker, or by declaring capabilities.

### Locations that UWP apps can access

When you create a new app, you can access the following file system locations by default:

### Application install directory

The folder where your app is installed on the user's system.

There are two primary ways to access files and folders in your app's install directory:

1. You can retrieve a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) that represents your app's install directory, like this:

    ```csharp
    Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
    ```

    ```javascript
    var installDirectory = Windows.ApplicationModel.Package.current.installedLocation;
    ```

    ```cppwinrt
    #include <winrt/Windows.Storage.h>
    ...
    Windows::Storage::StorageFolder installedLocation{ Windows::ApplicationModel::Package::Current().InstalledLocation() };
    ```

    ```cpp
    Windows::Storage::StorageFolder^ installedLocation = Windows::ApplicationModel::Package::Current->InstalledLocation;
    ```

    You can then access files and folders in the directory using [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) methods. In the example, this **StorageFolder** is stored in the `installDirectory` variable. You can learn more about working with your app package and install directory from the [App package information sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Package) on GitHub.

2. You can retrieve a file directly from your app's install directory by using an app URI, like this:

    ```csharp
    using Windows.Storage;            
    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///file.txt"));
    ```

    ```javascript
    Windows.Storage.StorageFile.getFileFromApplicationUriAsync("ms-appx:///file.txt").done(
        function(file) {
            // Process file
        }
    );
    ```

    ```cppwinrt
    Windows::Foundation::IAsyncAction ExampleCoroutineAsync()
    {
        Windows::Storage::StorageFile file{
            co_await Windows::Storage::StorageFile::GetFileFromApplicationUriAsync(Windows::Foundation::Uri{L"ms-appx:///file.txt"})
        };
        // Process file
    }
    ```

    ```cpp
    auto getFileTask = create_task(StorageFile::GetFileFromApplicationUriAsync(ref new Uri("ms-appx:///file.txt")));
    getFileTask.then([](StorageFile^ file) 
    {
        // Process file
    });
    ```

    When [**GetFileFromApplicationUriAsync**](/uwp/api/windows.storage.storagefile.getfilefromapplicationuriasync) completes, it returns a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) that represents the `file.txt` file in the app's install directory (`file` in the example).

    The "ms-appx:///" prefix in the URI refers to the app's install directory. You can learn more about using app URIs in [How to use URIs to reference content](/previous-versions/windows/apps/hh781215(v=win.10)).

In addition, and unlike other locations, you can also access files in your app install directory by using some [Win32 and COM for Universal Windows Platform (UWP) apps](/uwp/win32-and-com/win32-and-com-for-uwp-apps) and some [C/C++ Standard Library functions from Microsoft Visual Studio](/cpp/cpp/c-cpp-language-and-standard-libraries).

The app's install directory is a read-only location. You can't gain access to the install directory through the file picker.

### Access application data locations

The folders where your app can store data. These folders (local, roaming and temporary) are created when your app is installed.

There are two primary ways to access files and folders from your app's data locations:

1. Use [**ApplicationData**](/uwp/api/Windows.Storage.ApplicationData) properties to retrieve an app data folder.

    For example, you can use [**ApplicationData**](/uwp/api/Windows.Storage.ApplicationData).[**LocalFolder**](/uwp/api/windows.storage.applicationdata.localfolder) to retrieve a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) that represents your app's local folder like this:

    ```csharp
    using Windows.Storage;
    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
    ```

    ```javascript
    var localFolder = Windows.Storage.ApplicationData.current.localFolder;
    ```

    ```cppwinrt
    Windows::Storage::StorageFolder storageFolder{
        Windows::Storage::ApplicationData::Current().LocalFolder()
    };
    ```

    ```cpp
    using namespace Windows::Storage;
    StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
    ```

    If you want to access your app's roaming or temporary folder, use the [**RoamingFolder**](/uwp/api/windows.storage.applicationdata.roamingfolder) or [**TemporaryFolder**](/uwp/api/windows.storage.applicationdata.temporaryfolder) property instead.

    After you retrieve a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) that represents an app data location, you can access files and folders in that location by using **StorageFolder** methods. In the example, these **StorageFolder** objects are stored in the `localFolder` variable. You can learn more about using app data locations from the guidance on the [ApplicationData class](/uwp/api/windows.storage.applicationdata) page, and by downloading the [Application data sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ApplicationData) from GitHub.

2. You can retrieve a file directly from your app's local folder by using an app URI, like this:

    ```csharp
    using Windows.Storage;
    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/file.txt"));
    ```

    ```javascript
    Windows.Storage.StorageFile.getFileFromApplicationUriAsync("ms-appdata:///local/file.txt").done(
        function(file) {
            // Process file
        }
    );
    ```

    ```cppwinrt
    Windows::Storage::StorageFile file{
        co_await Windows::Storage::StorageFile::GetFileFromApplicationUriAsync(Windows::Foundation::Uri{ L"ms-appdata:///local/file.txt" })
    };
    // Process file
    ```

    ```cpp
    using Windows::Storage;
    auto getFileTask = create_task(StorageFile::GetFileFromApplicationUriAsync(ref new Uri("ms-appdata:///local/file.txt")));
    getFileTask.then([](StorageFile^ file) 
    {
        // Process file
    });
    ```

    When [**GetFileFromApplicationUriAsync**](/uwp/api/windows.storage.storagefile.getfilefromapplicationuriasync) completes, it returns a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) that represents the `file.txt` file in the app's local folder (`file` in the example).

    The "ms-appdata:///local/" prefix in the URI refers to the app's local folder. To access files in the app's roaming or temporary folders use "ms-appdata:///roaming/" or "ms-appdata:///temporary/" instead. You can learn more about using app URIs in [How to load file resources](/previous-versions/windows/apps/hh781229(v=win.10)).

In addition, and unlike other locations, you can also access files in your app data locations by using some [Win32 and COM for UWP apps](/uwp/win32-and-com/win32-and-com-for-uwp-apps) and some C/C++ Standard Library functions from Visual Studio.

You can't access the local, roaming, or temporary folders through the file picker.

### Access removable devices

Additionally, your app can access some of the files on connected devices by default. This is an option if your app uses the [AutoPlay extension](/previous-versions/windows/apps/hh464906(v=win.10)) to launch automatically when users connect a device, like a camera or USB thumb drive, to their system. The files your app can access are limited to specific file types that are specified via File Type Association declarations in your app manifest.

Of course, you can also gain access to files and folders on a removable device by calling the file picker (using [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) and [**FolderPicker**](/uwp/api/Windows.Storage.Pickers.FolderPicker)) and letting the user pick files and folders for your app to access. Learn how to use the file picker in [Open files and folders with a picker](quickstart-using-file-and-folder-pickers.md).

> [!NOTE]
> For more info about accessing an SD card or other removable devices, see [Access the SD card](access-the-sd-card.md).

### User's Downloads folder

The folder where downloaded files are saved by default.

By default, your app can only access files and folders in the user's Downloads folder that your app created. However, you can gain access to files and folders in the user's Downloads folder by calling a file picker ([**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) or [**FolderPicker**](/uwp/api/Windows.Storage.Pickers.FolderPicker)) so that users can navigate and pick files or folders for your app to access.

- You can create a file in the user's Downloads folder like this:

    ```csharp
    using Windows.Storage;
    StorageFile newFile = await DownloadsFolder.CreateFileAsync("file.txt");
    ```

    ```javascript
    Windows.Storage.DownloadsFolder.createFileAsync("file.txt").done(
        function(newFile) {
            // Process file
        }
    );
    ```

    ```cppwinrt
    Windows::Storage::StorageFile newFile{
        co_await Windows::Storage::DownloadsFolder::CreateFileAsync(L"file.txt")
    };
    // Process file
    ```

    ```cpp
    using Windows::Storage;
    auto createFileTask = create_task(DownloadsFolder::CreateFileAsync(L"file.txt"));
    createFileTask.then([](StorageFile^ newFile)
    {
        // Process file
    });
    ```

    [**DownloadsFolder**](/uwp/api/Windows.Storage.DownloadsFolder).[**CreateFileAsync**](/uwp/api/windows.storage.downloadsfolder.createfileasync) is overloaded so that you can specify what the system should do if there is already an existing file in the Downloads folder that has the same name. When these methods complete, they return a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) that represents the file that was created. This file is called `newFile` in the example.

- You can create a subfolder in the user's Downloads folder like this:

    ```csharp
    using Windows.Storage;
    StorageFolder newFolder = await DownloadsFolder.CreateFolderAsync("New Folder");
    ```

    ```javascript
    Windows.Storage.DownloadsFolder.createFolderAsync("New Folder").done(
        function(newFolder) {
            // Process folder
        }
    );
    ```

    ```cppwinrt
    Windows::Storage::StorageFolder newFolder{
        co_await Windows::Storage::DownloadsFolder::CreateFolderAsync(L"New Folder")
    };
    // Process folder
    ```

    ```cpp
    using Windows::Storage;
    auto createFolderTask = create_task(DownloadsFolder::CreateFolderAsync(L"New Folder"));
    createFolderTask.then([](StorageFolder^ newFolder)
    {
        // Process folder
    });
    ```

    [**DownloadsFolder**](/uwp/api/Windows.Storage.DownloadsFolder).[**CreateFolderAsync**](/uwp/api/windows.storage.downloadsfolder.createfolderasync) is overloaded so that you can specify what the system should do if there is already an existing subfolder in the Downloads folder that has the same name. When these methods complete, they return a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) that represents the subfolder that was created. This file is called `newFolder` in the example.

### Accessing additional locations

In addition to the default locations, an app can access additional files and folders by [declaring capabilities in the app manifest](../packaging/app-capability-declarations.md) or by [calling a file picker](quickstart-using-file-and-folder-pickers.md) to let the user pick files and folders for the app to access.

Apps that declare the [AppExecutionAlias](/uwp/schemas/appxpackage/uapmanifestschema/element-uap5-appexecutionalias) extension have file-system permissions from the directory that they are launched from in the console window, and downwards.

### Retaining access to files and folders

When your app retrieves a file or folder via a picker, a file activation, a drag-and-drop operation, etc. it only has access to that file or folder until the app is terminated. If you would like to automatically access the file or folder in the future, you can add it to the [**FutureAccessList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.futureaccesslist) so that your app can readily access that item in the future. You can also use the [**MostRecentlyUsedList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.mostrecentlyusedlist) to easily manage a list of recently-used files.

### Capabilities for accessing other locations

The following table lists additional locations that you can access by declaring one or more capabilities and using the associated [**Windows.Storage**](/uwp/api/Windows.Storage) API.

| Location | Capability | Windows.Storage API |
|----------|------------|---------------------|
| All files that the user has access to. For example: documents, pictures, photos, downloads, desktop, OneDrive, etc. | **broadFileSystemAccess**<br><br>This is a restricted capability. Access is configurable in **Settings** > **Privacy** > **File system**. Because users can grant or deny the permission any time in **Settings**, you should ensure that your app is resilient to those changes. If you find that your app does not have access, you may choose to prompt the user to change the setting by providing a link to the [Windows file system access and privacy](https://support.microsoft.com/windows/-windows-file-system-access-and-privacy-a7d90b20-b252-0e7b-6a29-a3a688e5c7be) article. Note that the user must close the app, toggle the setting, and restart the app. If they toggle the setting while the app is running, the platform will suspend your app so that you can save the state, then forcibly terminate the app in order to apply the new setting. In the April 2018 update, the default for the permission is On. In the October 2018 update, the default is Off.<br /><br />If you submit an app to the Store that declares this capability, you will need to supply additional descriptions of why your app needs this capability, and how it intends to use it.<br/><br/>This capability works for APIs in the [**Windows.Storage**](/uwp/api/Windows.Storage) namespace. See the **Example** section at the end of this article for an example of how to enable this capability in your app.<br/><br/>**Note:** This capability is not supported on Xbox. | n/a |
| Documents | **documentsLibrary**<br><br>Note: You must add File Type Associations to your app manifest that declare specific file types that your app can access in this location. <br><br>Use this capability if your app:<br>- Facilitates cross-platform offline access to specific OneDrive content using valid OneDrive URLs or Resource IDs<br>- Saves open files to the user's OneDrive automatically while offline | [KnownFolders.DocumentsLibrary](/uwp/api/windows.storage.knownfolders.documentslibrary) |
| Music     | **musicLibrary** <br>Also see [Files and folders in the Music, Pictures, and Videos libraries](quickstart-managing-folders-in-the-music-pictures-and-videos-libraries.md). | [KnownFolders.MusicLibrary](/uwp/api/windows.storage.knownfolders.musiclibrary) |    
| Pictures  | **picturesLibrary**<br> Also see [Files and folders in the Music, Pictures, and Videos libraries](quickstart-managing-folders-in-the-music-pictures-and-videos-libraries.md). | [KnownFolders.PicturesLibrary](/uwp/api/windows.storage.knownfolders.pictureslibrary) |  
| Videos    | **videosLibrary**<br>Also see [Files and folders in the Music, Pictures, and Videos libraries](quickstart-managing-folders-in-the-music-pictures-and-videos-libraries.md). | [KnownFolders.VideosLibrary](/uwp/api/windows.storage.knownfolders.videoslibrary) |   
| Removable devices  | **removableStorage**  <br><br>Note  You must add File Type Associations to your app manifest that declare specific file types that your app can access in this location. <br><br>Also see [Access the SD card](access-the-sd-card.md). | [KnownFolders.RemovableDevices](/uwp/api/windows.storage.knownfolders.removabledevices) |  
| Homegroup libraries  | At least one of the following capabilities is needed. <br>- **musicLibrary** <br>- **picturesLibrary** <br>- **videosLibrary** | [KnownFolders.HomeGroup](/uwp/api/windows.storage.knownfolders.homegroup) |      
| Media server devices (DLNA) | At least one of the following capabilities is needed. <br>- **musicLibrary** <br>- **picturesLibrary** <br>- **videosLibrary** | [KnownFolders.MediaServerDevices](/uwp/api/windows.storage.knownfolders.mediaserverdevices) |
| Universal Naming Convention (UNC) folders | A combination of the following capabilities is needed. <br><br>The home and work networks capability: <br>- **privateNetworkClientServer** <br><br>And at least one internet and public networks capability: <br>- **internetClient** <br>- **internetClientServer** <br><br>And, if applicable, the domain credentials capability:<br>- **enterpriseAuthentication** <br><br>**Note:** You must add File Type Associations to your app manifest that declare specific file types that your app can access in this location. | Retrieve a folder using: <br>[StorageFolder.GetFolderFromPathAsync](/uwp/api/windows.storage.storagefolder.getfolderfrompathasync) <br><br>Retrieve a file using: <br>[StorageFile.GetFileFromPathAsync](/uwp/api/windows.storage.storagefile.getfilefrompathasync) |

### Example

This example adds the restricted **broadFileSystemAccess** capability. In addition to specifying the capability, the `rescap` namespace must be added, and is also added to `IgnorableNamespaces`.

```xaml
<Package
  ...
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  IgnorableNamespaces="uap mp rescap">
...
<Capabilities>
    <rescap:Capability Name="broadFileSystemAccess" />
</Capabilities>
```

> [!NOTE]
> For a complete list of app capabilities, see [App capability declarations](../packaging/app-capability-declarations.md).
