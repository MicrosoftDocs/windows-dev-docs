---
ms.assetid: CAC6A7C7-3348-4EC4-8327-D47EB6E0C238
title: Access the SD card
description: You can store and access non-essential data on an optional microSD card, especially on low-cost mobile devices that have limited internal storage.
ms.date: 03/08/2017
ms.topic: article
keywords: windows 10, uwp, sd card, storage
ms.localizationpriority: medium
---
# Access the SD card



You can store and access non-essential data on an optional microSD card, especially on low-cost mobile devices that have limited internal storage and have a slot for an SD card.

In most cases, you have to specify the **removableStorage** capability in the app manifest file before your app can store and access files on the SD card. Typically you also have to register to handle the type of files that your app stores and accesses.

You can store and access files on the optional SD card by using the following methods:
- File pickers.
- The [**Windows.Storage**](/uwp/api/Windows.Storage) APIs.

## What you can and can't access on the SD card

### What you can access

- Your app can only read and write files of file types that the app has registered to handle in the app manifest file.
- Your app can also create and manage folders.

### What you can't access

- Your app can't see or access system folders and the files that they contain.
- Your app can't see files that are marked with the Hidden attribute. The Hidden attribute is typically used to reduce the risk of deleting data accidentally.
- Your app can't see or access the Documents library by using [**KnownFolders.DocumentsLibrary**](/uwp/api/windows.storage.knownfolders.documentslibrary). However you can access the Documents library on the SD card by traversing the file system.

## Security and privacy considerations

When an app saves files in a global location on the SD card, those files are not encrypted so they are typically accessible to other apps.

- While the SD card is in the device, your files are accessible to other apps that have registered to handle the same file type.
- When the SD card is removed from the device and opened from a PC, your files are visible in File Explorer and accessible to other apps.

When an app installed on the SD card saves files in its [**LocalFolder**](/uwp/api/windows.storage.applicationdata.localfolder), however, those files are encrypted and are not accessible to other apps.

## Requirements for accessing files on the SD card

To access files on the SD card, typically you have to specify the following things.

1.  You have to specify the **removableStorage** capability in the app manifest file.
2.  You also have to register to handle the file extensions associated with the type of media that you want to access.

Use the preceding method also to access media files on the SD card without referencing a known folder like **KnownFolders.MusicLibrary**, or to access media files that are stored outside of the media library folders.

To access media files stored in the media libraries—Music, Photos, or Videos—by using known folders, you only have to specify the associated capability in the app manifest file—**musicLibrary**, **picturesLibrary**, or **videoLibrary**. You do not have to specify the **removableStorage** capability. For more info, see [Files and folders in the Music, Pictures, and Videos libraries](quickstart-managing-folders-in-the-music-pictures-and-videos-libraries.md).

## Accessing files on the SD card

### Getting a reference to the SD card

The [**KnownFolders.RemovableDevices**](/uwp/api/windows.storage.knownfolders.removabledevices) folder is the logical root [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) for the set of removable devices currently connected to the device. If an SD card is present, the first (and only) **StorageFolder** underneath the **KnownFolders.RemovableDevices** folder represents the SD card.

Use code like the following to determine whether an SD card is present and to get a reference to it as a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder).

```csharp
using Windows.Storage;

// Get the logical root folder for all external storage devices.
StorageFolder externalDevices = Windows.Storage.KnownFolders.RemovableDevices;

// Get the first child folder, which represents the SD card.
StorageFolder sdCard = (await externalDevices.GetFoldersAsync()).FirstOrDefault();

if (sdCard != null)
{
    // An SD card is present and the sdCard variable now contains a reference to it.
}
else
{
    // No SD card is present.
}
```

> [!NOTE]
> If your SD card reader is an embedded reader (for example, a slot in the laptop or PC itself), it may not be accessible through KnownFolders.RemovableDevices.

### Querying the contents of the SD card

The SD card can contain many folders and files that aren't recognized as known folders and can't be queried by using a location from [**KnownFolders**](/uwp/api/Windows.Storage.KnownFolders). To find files, your app has to enumerate the contents of the card by traversing the file system recursively. Use [**GetFilesAsync (CommonFileQuery.DefaultQuery)**](/uwp/api/windows.storage.storagefolder.getfilesasync) and [**GetFoldersAsync (CommonFolderQuery.DefaultQuery)**](/uwp/api/windows.storage.storagefolder.getfoldersasync) to get the contents of the SD card efficiently.

We recommend that you use a background thread to traverse the SD card. An SD card may contain many gigabytes of data.

Your app can also require the user to choose specific folders by using the folder picker.

When you access the file system on the SD card with a path that you derived from [**KnownFolders.RemovableDevices**](/uwp/api/windows.storage.knownfolders.removabledevices), the following methods behave in the following way.

-   The [**GetFilesAsync**](/uwp/api/windows.storage.storagefolder.getfilesasync) method returns the union of the file extensions that you have registered to handle and the file extensions associated with any media library capabilities that you have specified.
-   The [**GetFileFromPathAsync**](/uwp/api/windows.storage.storagefile.getfilefrompathasync) method fails if you have not registered to handle the file extension of the file you are trying to access.

## Identifying the individual SD card

When the SD card is first mounted, the operating system generates a unique identifier for the card. It stores this ID in a file in the WPSystem folder at the root of the card. An app can use this ID to determine whether it recognizes the card. If an app recognizes the card, the app may be able to postpone certain operations that were completed previously. However the contents of the card may have changed since the card was last accessed by the app.

For example, consider an app that indexes ebooks. If the app has previously scanned the whole SD card for ebook files and created an index of the ebooks, it can display the list immediately if the card is reinserted and the app recognizes the card. Separately it can start a low-priority background thread to search for new ebooks. It can also handle a failure to find an ebook that existed previously when the user tries to access the deleted ebook.

The name of the property that contains this ID is **WindowsPhone.ExternalStorageId**.

```csharp
using Windows.Storage;

// Get the logical root folder for all external storage devices.
StorageFolder externalDevices = Windows.Storage.KnownFolders.RemovableDevices;

// Get the first child folder, which represents the SD card.
StorageFolder sdCard = (await externalDevices.GetFoldersAsync()).FirstOrDefault();

if (sdCard != null)
{
    var allProperties = sdCard.Properties;
    IEnumerable<string> propertiesToRetrieve = new List<string> { "WindowsPhone.ExternalStorageId" };

    var storageIdProperties = await allProperties.RetrievePropertiesAsync(propertiesToRetrieve);

    string cardId = (string)storageIdProperties["WindowsPhone.ExternalStorageId"];

    if (...) // If cardID matches the cached ID of a recognized card.
    {
        // Card is recognized. Index contents opportunistically.
    }
    else
    {
        // Card is not recognized. Index contents immediately.
    }
}
```

 

 