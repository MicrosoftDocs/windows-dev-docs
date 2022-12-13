---
title: Using supplemental properties
description: Introduction to using supplemental properties and details on how they were implemented in Windows
ms.date: 01/10/2017
ms.topic: article
keywords: windows 10, uwp, WinRT API, Indexer, Search
localizationpriority: medium
---
# Using supplemental properties  

## Summary  
- Supplemental properties allow apps to tag files with properties without changing the file 
- Useful for cases where you have properties that are hard to calculate, or the file can’t be modified 
- Using supplemental properties is the same as using any other property on the Windows Property system  

## Introduction 
Many of the exciting new apps in the last few years require running CPU intensive operations on user files to extract useful properties from the files beyond basic things like date created. These apps do object recognition in images, intent extraction in emails, and text analysis to group documents together. This is being driven by how powerful computing is now available on most consumer PCs.   

Making this metadata searchable instantly allows for users to be exponentially more productive. Simply knowing your daughter is in a picture is interesting but being able to search for the picture of her with her grandmother is so much more useful. It makes the experience of using a computer feel more personal, and more alive. Like someone in the machine is reaching out to help you find your treasured memories. 

For decades, the solution for fast searching on Windows has been the indexer, and in the Creators Update it has been updated to support these new scenarios. Apps are now able to tag files with additional properties beyond those which are extracted by the system. These properties are treated as first class citizens  

## Windows Properties 
The [Windows Property system](/windows/desktop/properties/windows-properties-system) has been a key part of interacting with files for years. It allows apps to read properties from files without having to understand the internals of all the different file formats or languages a file could be in. All that is abstracted away for you as a developer, all you have to do is ask for a list and specify ascending or descending.  

The property system is intertwined with the Windows Indexer – it reads all the properties from files within its scope and stores them. Later when an app asks for a list of all the .docx in a folder to be sorted by date modified, excluding those authored by John Smith the indexer can return the list instantly.  

The downside to how these systems work together is that the indexer used to require all the properties it would store about a file to be available instantly. This limited it from knowing about more interesting properties that take longer to calculate since there is a hard time requirement.  

Using properties though is easy, the app can either request a sorted set of properties about a file, much like working with a database, or it can pass a query like using a search engine. The indexer will process the query and return the results. This give developers the flexibility to combine their filters (for example only search jpg files) with the user’s query (file name starting with “bird”). 

## Supplemental properties 

Supplemental properties behave identically to regular Windows properties with one very important difference – they are not going to be written when the file is added to the indexer. A supplemental property must be added by another app on the system later. It could be two minutes later once object recognition completes or it could be days later. 

Once the property is written it can be searched, filtered, sorted, or grouped just like any other property on the system. As well it can be used in combined queries with other properties on the system, either supplemental or not. This give you the flexibility to combine supplemental properties easily with your existing file system code without having to do a rewrite.  

### Example scenarios 

There are thousands of different properties you could write to a supplemental property, but there are a couple of key scenarios that this tutorial will keep going back to:  

#### Tagging pictures with extracted properties 
These apps can use an ML trained model to extract features from an image that the system doesn’t know about such as object in the image. It can then take the objects it identifies in the image and add them to the property system for later searching or grouping.  

#### Tagging files with an app specific ID 
Many file sync apps use their own unique ID to track files as they move between the server and various client devices. The sync client can write this ID to the property system without effecting the file. This ID is now available to the app later for fast access and available for any other app on the system to read when communicating with the sync provider. 

There are many other options for using supplemental properties but both of these make good examples because they require fast lookup or search, are pieces of information the system doesn’t know about, and can’t be added to the file itself.  

### Using supplemental properties 
Using the supplemental properties is the same as writing a normal property to the file system. If you are comfortable with using StorageFiles and properties, you can skip over this. Otherwise, let’s walk through a quick sample of writing out a single property to a file and then reading the same property back later.  

### Writing supplemental properties  
The sample will only modify the first file that it finds for simplicity, but generally an app will add the property to every file that it finds.  

```csharp
// Only indexed jpg files are going to be used 
QueryOptions option = new QueryOptions(CommonFileQuery.DefaultQuery, new string[] { ".jpg" }); 
option.IndexerOption = IndexerOption.OnlyUseIndexer; 
// Typically an app would loop over all the files in the library, updating them all with the new 
// value. To make the sample easier to understand however, this app is only going to update the  
// first file it finds 
var query = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(option); 
StorageFile file = (await query.GetFilesAsync()).FirstOrDefault(); 
if (file == null) 
{ 
    log("No jpg file found in the library. Stopping"); 
    return; 
} 
log("Found file: " + file.Path); 
// Selecting the property to modify and writing it out 
List<KeyValuePair<string, object>> props = new List<KeyValuePair<string, object>>();             
props.Add(new KeyValuePair<string, object>("System.Supplemental.ResourceId", fileId)); 
await file.Properties.SavePropertiesAsync(props); 
```

There is an important check if the location is indexed before writing out a property. In this sample we are using the query options to filter to only indexed locations. If this isn’t feasible you can check the indexed state of the parent folder (file.GetParentAsync().GetIndexedStateAsync()). Either way will yield the same results 

### Reading supplemental properties 
Again, reading a supplemental property is the same as reading any other file system property. In this sample the app will just read one property from a file it already has a StorageFile for, but it could also read other properties at the same time.  

```csharp
// An object to hold the result from the indexer, and a string to store  
// the value in once we have confirmed it is valid. 
object uncheckedResourceId; 
string resourceId = ""; 
// Fetching the key value pair from the indexer 
IDictionary<string,object> returnedProps =  
    await file.Properties.RetrievePropertiesAsync(new string[] { "System.Supplemental.ResourceId" });             
if (returnedProps.TryGetValue("System.Supplemental.ResourceId", out uncheckedResourceId)) 
{ 
    if (uncheckedResourceId != null && !String.IsNullOrEmpty(uncheckedResourceId.ToString())) 
    { 
        resourceId = uncheckedResourceId.ToString(); 
    } 
} 
```
There is a check to make sure the value coming back from the property system is what you expect. Although it is unlikely, it is possible the value has been cleared since your app has written it. This will be covered in detail below.  

### Implementation notes 
There are a few subtle choices that were made in the design of the supplemental properties. To help with your implementation the following sections were copied from the engineering design spec for the feature. They provide a glimpse into how the feature was designed, and why some of the limitations exist. 

### Supplemental properties available 
There are only two properties available to use for apps initially: System.Supplemental.ResourceId and System.Supplemental.AlbumID. If there is a need for more they can be added. The album ID is a multi-value string that can be used for many different applications and the ResourceId is used as a unique ID for cloud sync providers. 

#### File System Support 
Since FAT formatted removable media is an important scenario, supplemental properties will support FAT and NTFS drives. This will ensure that the supplemental properties are going to be available for all users, regardless of their device type.   

### Non-Indexed Locations  
On the desktop there are a number of folders that are not indexed. In these cases, apps may still want to have access to supplemental properties. However, supplemental properties are not available outside of indexed locations. This trade-off was made for a couple of reasons:  

- All libraries and cloud storage locations are indexed by default.   
  These are the locations that UWP apps to be primarily using. There are other locations that are not indexed (system or network drives) but they are less commonly used for storing user data. 

- The WinRT API surface design assumes the indexer is almost always available.  
  Thus, the indexer is already available in most of the locations apps are interested in. If users are found to be storing data in non-indexed locations, the easiest solution is going to be to add that location to the index. Then supplemental properties work, enumeration will be faster, and apps will be able to change track the location.

### Reading or Writing Supplemental Properties from a file in a Non-Indexed Location 
In the case that an app attempts to write a supplemental property to a location that is not currently indexed, then the API call will throw an exception. This will be the same exception that is thrown as when someone tries to update the System.Music.AlbumArtist on a .docx file (Invalid Args).  
 
### Change notifications:  
The UWP change notifications and change tracking will continue to work for the supplemental properties as they do for the standard properties. This will enable apps who are providing data to track all the changes happening to one of their apps 
  
### Invalidating properties:  
The supplemental properties on a file may become out of date whenever a file is modified or moved on the system. Apps pushing the data will be the ones with the information about if the data is valid or needs to be updated so the system will just provide the tools for them to figure it out themselves.  
 
In the case a file is modified, but not moved or renamed, all the supplemental properties on the file will remain unchanged. The apps will be able to register for change notifications through the existing API surface and update the properties as needed. 
 
If the file is moved, the properties are going to be invalidated. The app will receive the change notifications of either delete, create, renamed, or moved depending on how exactly the operation is done. Once the app has received the change notification it will be able to inspect the file and update the supplemental properties on the file as needed. 
 
### Indexer rebuilds  
Occasionally the system index has to be rebuilt for one of a number of reasons – the property schema could change, the user could enable EDP, or simply the database file could be corrupted. In these cases, the supplemental properties will not be preserved. We considered doing the work to try to preserve the supplemental properties when the index is being rebuilt, but there were a couple of major blockers:  

### Protecting the data 
In the case where the database file is corrupted, either by disk errors or rogue software, it is going to be impossible to protect the data that was stored in that file. It would have to be stored somewhere else on the system or somehow isolated from the rest of the database. 

Since we are already doing a lot of work to make the index less likely to be corrupted, this will reduce the incidence rate of this case anyways.  
Maintaining the mapping between files and their metadata during the rebuilds 

Even if the index can protect the data across a rebuild, it is impossible to know if the file has changed while the index was being rebuilt. The data that the index is protecting from the file might be no longer valid if the file is modified or moved.  
Behaviour 

In the case of an indexer rebuild, all supplemental data will be lost. Apps will be responsible for putting the data back into the indexer that was lost during the rebuild. This does put an extra burden on the apps but is deemed reasonable since they will always hold the master state for all their data.  

### Recovering 
Once the apps have noticed that the index is being rebuilt, they will be responsible for updating the supplemental properties at their convenience.  
### Privacy 
Some of the properties that might be written to the files are going to such that users may not want them to be shared with other applications. Apps should be able to indicate that the information they are writing to the properties is going to be private to either their applications, shared with just a few other applications, or public to every app on the system.  

Although this is potentially an interesting feature for some of the early adopters of the feature, they feel that getting public properties is still going to add a lot of value to the design. Thus, this is marked as a nice to have, and we should continue to build the feature without support for hiding the values if required. Adding it later will open up more scenarios, so it will be important to consider in any designs.  

## Conclusions 
That’s it, supplemental properties are an easy way to store more file properties in the system. Using them is of course optional, but it can give your app an edge over other apps which aren’t able to sort and search their data as quickly. 

We’re looking forward to seeing apps start to use these properties. If you have any questions about how using the header please let us know in the comments below