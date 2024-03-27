---
ms.assetid: 3604524F-112A-474F-B0CA-0726DC8DB885
title: Determining availability of Microsoft OneDrive files
description: Determine if a Microsoft OneDrive file is available using the StorageFile.IsAvailable property.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Determining availability of Microsoft OneDrive files


**Important APIs**

-   [**FileIO class**](/uwp/api/Windows.Storage.FileIO)
-   [**StorageFile class**](/uwp/api/Windows.Storage.StorageFile)
-   [**StorageFile.IsAvailable property**](/uwp/api/windows.storage.storagefile.isavailable)

Determine if a Microsoft OneDrive file is available using the [**StorageFile.IsAvailable**](/uwp/api/windows.storage.storagefile.isavailable) property.

## Prerequisites

-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](../threading-async/call-asynchronous-apis-in-csharp-or-visual-basic.md). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

-   **App capability declarations**

    See [File access permissions](file-access-permissions.md).

## Using the StorageFile.IsAvailable property

Users are able to mark OneDrive files as either available-offline (default) or online-only. This capability enables users to move large files (such as pictures and videos) to their OneDrive, mark them as online-only, and save disk space (the only thing kept locally is a metadata file).

[**StorageFile.IsAvailable**](/uwp/api/windows.storage.storagefile.isavailable), is used to determine if a file is currently available. The following table shows the value of the **StorageFile.IsAvailable** property in various scenarios.

| Type of file                              | Online | Metered network        | Offline |
|-------------------------------------------|--------|------------------------|---------|
| Local file                                | True   | True                   | True    |
| OneDrive file marked as available-offline | True   | True                   | True    |
| OneDrive file marked as online-only       | True   | Based on user settings | False   |
| Network file                              | True   | Based on user settings | False   |

 

The following steps illustrate how to determine if a file is currently available.

1.  Declare a capability appropriate for the library you want to access.
2.  Include the [**Windows.Storage**](/uwp/api/Windows.Storage) namespace. This namespace includes the types for managing files, folders, and application settings. It also includes the needed [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) type.
3.  Acquire a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object for the desired file(s). If you are enumerating a library, this step is usually accomplished by calling the [**StorageFolder.CreateFileQuery**](/uwp/api/windows.storage.storagefolder.createfilequery) method and then calling the resulting [**StorageFileQueryResult**](/uwp/api/Windows.Storage.Search.StorageFileQueryResult) object's [**GetFilesAsync**](/uwp/api/windows.storage.storagefolder.getfilesasync) method. The **GetFilesAsync** method returns an [IReadOnlyList](/dotnet/api/system.collections.generic.ireadonlylist-1) collection of **StorageFile** objects.
4.  Once you have the access to a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object representing the desired file(s), the value of the [**StorageFile.IsAvailable**](/uwp/api/windows.storage.storagefile.isavailable) property reflects whether or not the file is available.

The following generic method illustrates how to enumerate any folder and return the collection of [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) objects for that folder. The calling method then iterates over the returned collection referencing the [**StorageFile.IsAvailable**](/uwp/api/windows.storage.storagefile.isavailable) property for each file.

```cs
/// <summary>
/// Generic function that retrieves all files from the specified folder.
/// </summary>
/// <param name="folder">The folder to be searched.</param>
/// <returns>An IReadOnlyList collection containing the file objects.</returns>
async Task<System.Collections.Generic.IReadOnlyList<StorageFile>> GetLibraryFilesAsync(StorageFolder folder)
{
    var query = folder.CreateFileQuery();
    return await query.GetFilesAsync();
}

private async void CheckAvailabilityOfFilesInPicturesLibrary()
{
    // Determine availability of all files within Pictures library.
    var files = await GetLibraryFilesAsync(KnownFolders.PicturesLibrary);
    for (int i = 0; i < files.Count; i++)
    {
        StorageFile file = files[i];

        StringBuilder fileInfo = new StringBuilder();
        fileInfo.AppendFormat("{0} (on {1}) is {2}",
                    file.Name,
                    file.Provider.DisplayName,
                    file.IsAvailable ? "available" : "not available");
    }
}
```