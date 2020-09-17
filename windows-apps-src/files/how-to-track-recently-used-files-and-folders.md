---
ms.assetid: BF929A68-9C82-4866-BC13-A32B3A550005
title: Track recently used files and folders
description: Track files that your user accesses frequently by adding them to your app's most recently used list (MRU).
ms.date: 12/19/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Track recently used files and folders

**Important APIs**

- [**MostRecentlyUsedList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.mostrecentlyusedlist)
- [**FileOpenPicker**](/uwp/schemas/appxpackage/appxmanifestschema/element-fileopenpicker)

Track files that your user accesses frequently by adding them to your app's most recently used list (MRU). The platform manages the MRU for you by sorting items based on when they were last accessed, and by removing the oldest item when the list's 25-item limit is reached. All apps have their own MRU.

Your app's MRU is represented by the [**StorageItemMostRecentlyUsedList**](/uwp/api/Windows.Storage.AccessCache.StorageItemMostRecentlyUsedList) class, which you obtain from the static [**StorageApplicationPermissions.MostRecentlyUsedList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.mostrecentlyusedlist) property. MRU items are stored as [**IStorageItem**](/uwp/api/Windows.Storage.IStorageItem) objects, so both [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) objects (which represent files) and [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) objects (which represent folders) can be added to the MRU.

> [!NOTE]
> For complete samples, see the [File picker sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FilePicker) and the [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess).

## Prerequisites

-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](../threading-async/call-asynchronous-apis-in-csharp-or-visual-basic.md). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

-   **Access permissions to the location**

    See [File access permissions](file-access-permissions.md).

-   [Open files and folders with a picker](quickstart-using-file-and-folder-pickers.md)

    Picked files are often the same files that users return to again and again.

 ## Add a picked file to the MRU

-   The files that your user picks are often files that they return to repeatedly. So consider adding picked files to your app's MRU as soon as they are picked. Here's how.

    ```cs
    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

    var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
    string mruToken = mru.Add(file, "profile pic");
    ```

    [**StorageItemMostRecentlyUsedList.Add**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.add) is overloaded. In the example, we use [**Add(IStorageItem, String)**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.add) so that we can associate metadata with the file. Setting metadata lets you record the item's purpose, for example "profile pic". You can also add the file to the MRU without metadata by calling [**Add(IStorageItem)**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.add). When you add an item to the MRU, the method returns a uniquely identifying string, called a token, which is used to retrieve the item.

> [!TIP]
> You'll need the token to retrieve an item from the MRU, so persist it somewhere. For more info about app data, see [Managing application data](/previous-versions/windows/apps/hh465109(v=win.10)).

## Use a token to retrieve an item from the MRU

Use the retrieval method most appropriate for the item you want to retrieve.

-   Retrieve a file as a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) by using [**GetFileAsync**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.getfileasync).
-   Retrieve a folder as a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) by using [**GetFolderAsync**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.getfolderasync).
-   Retrieve a generic [**IStorageItem**](/uwp/api/Windows.Storage.IStorageItem), which can represent either a file or folder, by using [**GetItemAsync**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.getitemasync).

Here's how to get back the file we just added.

```cs
StorageFile retrievedFile = await mru.GetFileAsync(mruToken);
```

Here's how to iterate all the entries to get tokens and then items.

```cs
foreach (Windows.Storage.AccessCache.AccessListEntry entry in mru.Entries)
{
    string mruToken = entry.Token;
    string mruMetadata = entry.Metadata;
    Windows.Storage.IStorageItem item = await mru.GetItemAsync(mruToken);
    // The type of item will tell you whether it's a file or a folder.
}
```

The [**AccessListEntryView**](/uwp/api/Windows.Storage.AccessCache.AccessListEntryView) lets you iterate entries in the MRU. These entries are [**AccessListEntry**](/uwp/api/Windows.Storage.AccessCache.AccessListEntry) structures that contain the token and metadata for an item.

## Removing items from the MRU when it's full

When the MRU's 25-item limit is reached and you try to add a new item, the item that was accessed the longest time ago is automatically removed. So, you never need to remove an item before you add a new one.

## Future-access list

As well as an MRU, your app also has a future-access list. By picking files and folders, your user grants your app permission to access items that might not be accessible otherwise. If you add these items to your future-access list then you'll retain that permission when your app wants to access those items again later. Your app's future-access list is represented by the [**StorageItemAccessList**](/uwp/api/Windows.Storage.AccessCache.StorageItemAccessList) class, which you obtain from the static [**StorageApplicationPermissions.FutureAccessList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.futureaccesslist) property.

When a user picks an item, consider adding it to your future-access list as well as your MRU.

-   The [**FutureAccessList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.futureaccesslist) can hold up to 1000 items. Remember: it can hold folders as well as files, so that's a lot of folders.
-   The platform never removes items from the [**FutureAccessList**](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.futureaccesslist) for you. When you reach the 1000-item limit, you can't add another until you make room with the [**Remove**](/uwp/api/windows.storage.accesscache.storageitemmostrecentlyusedlist.remove) method.