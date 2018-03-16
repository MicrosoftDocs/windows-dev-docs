---
author: laurenhughes
ms.assetid: BF929A68-9C82-4866-BC13-A32B3A550005
title: Track recently used files and folders
description: Track files that your user accesses frequently by adding them to your app's most recently used list (MRU).
ms.author: lahugh
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Track recently used files and folders

**Important APIs**

- [**MostRecentlyUsedList**](https://msdn.microsoft.com/library/windows/apps/br207458)
- [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/hh738369)

Track files that your user accesses frequently by adding them to your app's most recently used list (MRU). The platform manages the MRU for you by sorting items based on when they were last accessed, and by removing the oldest item when the list's 25-item limit is reached. All apps have their own MRU.

Your app's MRU is represented by the [**StorageItemMostRecentlyUsedList**](https://msdn.microsoft.com/library/windows/apps/br207475) class, which you obtain from the static [**StorageApplicationPermissions.MostRecentlyUsedList**](https://msdn.microsoft.com/library/windows/apps/br207458) property. MRU items are stored as [**IStorageItem**](https://msdn.microsoft.com/library/windows/apps/br227129) objects, so both [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) objects (which represent files) and [**StorageFolder**](https://msdn.microsoft.com/library/windows/apps/br227230) objects (which represent folders) can be added to the MRU.

> [!NOTE]
> Also see the [File picker sample](http://go.microsoft.com/fwlink/p/?linkid=619994) and the [File access sample](http://go.microsoft.com/fwlink/p/?linkid=619995).

 

## Prerequisites

-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](https://msdn.microsoft.com/library/windows/apps/mt187337). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](https://msdn.microsoft.com/library/windows/apps/mt187334).

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

    [**StorageItemMostRecentlyUsedList.Add**](https://msdn.microsoft.com/library/windows/apps/br207476) is overloaded. In the example, we use [**Add(IStorageItem, String)**](https://msdn.microsoft.com/library/windows/apps/br207481) so that we can associate metadata with the file. Setting metadata lets you record the item's purpose, for example "profile pic". You can also add the file to the MRU without metadata by calling [**Add(IStorageItem)**](https://msdn.microsoft.com/library/windows/apps/br207480). When you add an item to the MRU, the method returns a uniquely identifying string, called a token, which is used to retrieve the item.

> [!TIP]
> You'll need the token to retrieve an item from the MRU, so persist it somewhere. For more info about app data, see [Managing application data](https://msdn.microsoft.com/library/windows/apps/hh465109).

## Use a token to retrieve an item from the MRU

Use the retrieval method most appropriate for the item you want to retrieve.

-   Retrieve a file as a [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) by using [**GetFileAsync**](https://msdn.microsoft.com/library/windows/apps/br207486).
-   Retrieve a folder as a [**StorageFolder**](https://msdn.microsoft.com/library/windows/apps/br227230) by using [**GetFolderAsync**](https://msdn.microsoft.com/library/windows/apps/br207489).
-   Retrieve a generic [**IStorageItem**](https://msdn.microsoft.com/library/windows/apps/br227129), which can represent either a file or folder, by using [**GetItemAsync**](https://msdn.microsoft.com/library/windows/apps/br207492).

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

The [**AccessListEntryView**](https://msdn.microsoft.com/library/windows/apps/br227349) lets you iterate entries in the MRU. These entries are [**AccessListEntry**](https://msdn.microsoft.com/library/windows/apps/br227348) structures that contain the token and metadata for an item.

## Removing items from the MRU when it's full

When the MRU's 25-item limit is reached and you try to add a new item, the item that was accessed the longest time ago is automatically removed. So, you never need to remove an item before you add a new one.

## Future-access list

As well as an MRU, your app also has a future-access list. By picking files and folders, your user grants your app permission to access items that might not be accessible otherwise. If you add these items to your future-access list then you'll retain that permission when your app wants to access those items again later. Your app's future-access list is represented by the [**StorageItemAccessList**](https://msdn.microsoft.com/library/windows/apps/br207459) class, which you obtain from the static [**StorageApplicationPermissions.FutureAccessList**](https://msdn.microsoft.com/library/windows/apps/br207457) property.

When a user picks an item, consider adding it to your future-access list as well as your MRU.

-   The [**FutureAccessList**](https://msdn.microsoft.com/library/windows/apps/br207457) can hold up to 1000 items. Remember: it can hold folders as well as files, so that's a lot of folders.
-   The platform never removes items from the [**FutureAccessList**](https://msdn.microsoft.com/library/windows/apps/br207457) for you. When you reach the 1000-item limit, you can't add another until you make room with the [**Remove**](https://msdn.microsoft.com/library/windows/apps/br207497) method.
