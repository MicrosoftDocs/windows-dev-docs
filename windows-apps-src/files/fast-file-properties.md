---
title: Fast access to file properties in UWP
description: Efficiently gather a list of files and their properties from a library to use in a UWP app.
ms.date: 02/06/2019
ms.topic: article
keywords: windows 10, uwp, file, properties
ms.localizationpriority: medium
---
# Fast access to file properties in UWP 

Learn how to quickly gather a list of files and their properties from a library and use those properties in an app.  

Prerequisites 
- **Asynchronous programming for Universal Windows Platform (UWP) apps**     
You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](../threading-async/call-asynchronous-apis-in-csharp-or-visual-basic.md). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md). 
- **Access permissions to Libraries**  
The code in these examples requires the **picturesLibrary** capability, but your file location may require a different capability, or no capability at all. To learn more, see [File access permissions](./file-access-permissions.md). 
- **Simple file enumeration**   
This example uses [QueryOptions](/uwp/api/Windows.Storage.Search.QueryOptions) to set a few advanced enumeration properties. To learn more about just getting a simple list of files for a smaller directory, see [Enumerate and query files and folders](./quickstart-listing-files-and-folders.md). 

## Usage  
Many apps need to list the properties of a group of files, but don't always need to interact with the files directly. For example, a music app plays (opens) one file at a time, but it needs the properties of all of the files in a folder so the app can show the song queue, or so the user can choose a valid file to play. 

The examples on this page shouldn't be used in apps that will modify the metadata of every file or apps that interact with all the resulting StorageFiles beyond reading their properties. See [Enumerate and query files and folders](./quickstart-listing-files-and-folders.md) for more information. 

## Enumerate all the pictures in a location 
In this example, we will
-  Build a [QueryOptions](/uwp/api/Windows.Storage.Search.QueryOptions) object to specify that the app wants to enumerate the files as quickly as possible.
-  Fetch file properties by paging StorageFile objects into the app. Paging the files in reduces the memory used by the app and improves its perceived responsiveness.

### Creating the query 
To build the query, we use a QueryOptions object to specify that the app is interested in enumerating only certain types of images files and to filter out files protected with Windows Information Protection (System.Security.EncryptionOwners). 

It is important to set the properties the app is going to access using [QueryOptions.SetPropertyPrefetch](/uwp/api/windows.storage.search.queryoptions.setpropertyprefetch). If the app accesses a property that isn’t prefetched, it will incur a significant performance penalty.

Setting [IndexerOption.OnlyUseIndexerAndOptimzeForIndexedProperties](/uwp/api/Windows.Storage.Search.IndexerOption) tells the system to return results as quickly as possible, but to only include the properties specified in [SetPropertyPrefetch](/uwp/api/windows.storage.search.queryoptions.setpropertyprefetch). 

### Paging in the results 
Users may have thousands or millions of files in their pictures library, so calling [GetFilesAsync](/uwp/api/windows.storage.search.storagefilequeryresult.getfilesasync) would overwhelm their machine because it creates a StorageFile for each image. This can be solved by creating a fixed number of StorageFiles at one time, processing them into the UI, and then releasing the memory. 

In our example, we do this by using [StorageFileQueryResult.GetFilesAsync(UInt32 StartIndex, UInt32 maxNumberOfItems)](/uwp/api/windows.storage.search.storagefilequeryresult.getfilesasync) to only fetch 100 files at a time. The app will then process the files and allow the OS to release that memory afterwards. This technique caps the maximum memory of the app and ensures the system stays responsive. Of course, you will need to adjust the number of files returned for your scenario, but to ensure a responsive experience for all users, it's recommended to not fetch more than 500 files at one time.


**Example**  
```csharp
StorageFolder folderToEnumerate = KnownFolders.PicturesLibrary; 
// Check if the folder is indexed before doing anything. 
IndexedState folderIndexedState = await folderToEnumerate.GetIndexedStateAsync(); 
if (folderIndexedState == IndexedState.NotIndexed || folderIndexedState == IndexedState.Unknown) 
{ 
    // Only possible in indexed directories.  
    return; 
} 
 
QueryOptions picturesQuery = new QueryOptions() 
{ 
    FolderDepth = FolderDepth.Deep, 
    // Filter out all files that have WIP enabled
    ApplicationSearchFilter = "System.Security.EncryptionOwners:[]", 
    IndexerOption = IndexerOption.OnlyUseIndexerAndOptimizeForIndexedProperties 
}; 

picturesQuery.FileTypeFilter.Add(".jpg"); 
string[] otherProperties = new string[] 
{ 
    SystemProperties.GPS.LatitudeDecimal, 
    SystemProperties.GPS.LongitudeDecimal 
}; 
 
picturesQuery.SetPropertyPrefetch(PropertyPrefetchOptions.BasicProperties | PropertyPrefetchOptions.ImageProperties, 
                                    otherProperties); 
SortEntry sortOrder = new SortEntry() 
{ 
    AscendingOrder = true, 
    PropertyName = "System.FileName" // FileName property is used as an example. Any property can be used here.  
}; 
picturesQuery.SortOrder.Add(sortOrder); 
 
// Create the query and get the results 
uint index = 0; 
const uint stepSize = 100; 
if (!folderToEnumerate.AreQueryOptionsSupported(picturesQuery)) 
{ 
    log("Querying for a sort order is not supported in this location"); 
    picturesQuery.SortOrder.Clear(); 
} 
StorageFileQueryResult queryResult = folderToEnumerate.CreateFileQueryWithOptions(picturesQuery); 
IReadOnlyList<StorageFile> images = await queryResult.GetFilesAsync(index, stepSize); 
while (images.Count != 0 || index < 10000) 
{ 
    foreach (StorageFile file in images) 
    { 
        // With the OnlyUseIndexerAndOptimizeForIndexedProperties set, this won't  
        // be async. It will run synchronously. 
        var imageProps = await file.Properties.GetImagePropertiesAsync(); 
 
        // Build the UI 
        log(String.Format("{0} at {1}, {2}", 
                    file.Path, 
                    imageProps.Latitude, 
                    imageProps.Longitude)); 
    } 
    index += stepSize; 
    images = await queryResult.GetFilesAsync(index, stepSize); 
} 
```

### Results 
The resulting StorageFile files only contain the properties requested, but are returned 10 times faster compared to the other IndexerOptions. The app can still request access to properties not already included in the query, but there is a performance penalty to open the file and retrieve those properties.  

## Adding folders to Libraries 
Apps can request the user to add the location to the index using [StorageLibrary.RequestAddFolderAsync](/uwp/api/Windows.Storage.StorageLibrary.RequestAddFolderAsync). Once the location is included, it will be automatically indexed and apps can use this technique to enumerate the files.
 
## See also
[QueryOptions API Reference](/uwp/api/windows.storage.search.queryoptions)  
[Enumerate and query files and folders](./quickstart-listing-files-and-folders.md)  
[File access permissions](./file-access-permissions.md)  
 
 