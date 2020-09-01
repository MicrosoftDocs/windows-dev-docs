---
ms.assetid: AC96F645-1BDE-4316-85E0-2FBDE0A0A62A
title: Get file properties
description: Get properties&\#8212;top-level, basic, and extended&\#8212;for a file represented by a StorageFile object.
ms.date: 12/19/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Get file properties

**Important APIs**

-   [**StorageFile.GetBasicPropertiesAsync**](/uwp/api/windows.storage.storagefile.getbasicpropertiesasync)
-   [**StorageFile.Properties**](/uwp/api/windows.storage.storagefile.properties)
-   [**StorageItemContentProperties.RetrievePropertiesAsync**](/uwp/api/windows.storage.fileproperties.storageitemcontentproperties.retrievepropertiesasync)

Get properties—top-level, basic, and extended—for a file represented by a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object.

> [!NOTE]
> For a complete sample, see the [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess).

## Prerequisites

-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](../threading-async/call-asynchronous-apis-in-csharp-or-visual-basic.md). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

-   **Access permissions to the location**

    For example, the code in these examples require the **picturesLibrary** capability, but your location may require a different capability or no capability at all. To learn more, see [File access permissions](file-access-permissions.md).

## Getting a file's top-level properties

Many top-level file properties are accessible as members of the [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) class. These properties include the files attributes, content type, creation date, display name, file type, and so on.

> [!NOTE]
> Remember to declare the **picturesLibrary** capability.

This example enumerates all of the files in the Pictures library, accessing a few of each file's top-level properties.

```csharp
// Enumerate all files in the Pictures library.
var folder = Windows.Storage.KnownFolders.PicturesLibrary;
var query = folder.CreateFileQuery();
var files = await query.GetFilesAsync();

foreach (Windows.Storage.StorageFile file in files)
{
    StringBuilder fileProperties = new StringBuilder();

    // Get top-level file properties.
    fileProperties.AppendLine("File name: " + file.Name);
    fileProperties.AppendLine("File type: " + file.FileType);
}
```

## Getting a file's basic properties

Many basic file properties are obtained by first calling the [**StorageFile.GetBasicPropertiesAsync**](/uwp/api/windows.storage.storagefile.getbasicpropertiesasync) method. This method returns a [**BasicProperties**](/uwp/api/Windows.Storage.FileProperties.BasicProperties) object, which defines properties for the size of the item (file or folder) as well as when the item was last modified.

This example enumerates all of the files in the Pictures library, accessing a few of each file's basic properties.

```csharp
// Enumerate all files in the Pictures library.
var folder = Windows.Storage.KnownFolders.PicturesLibrary;
var query = folder.CreateFileQuery();
var files = await query.GetFilesAsync();

foreach (Windows.Storage.StorageFile file in files)
{
    StringBuilder fileProperties = new StringBuilder();

    // Get file's basic properties.
    Windows.Storage.FileProperties.BasicProperties basicProperties =
        await file.GetBasicPropertiesAsync();
    string fileSize = string.Format("{0:n0}", basicProperties.Size);
    fileProperties.AppendLine("File size: " + fileSize + " bytes");
    fileProperties.AppendLine("Date modified: " + basicProperties.DateModified);
}
 ```

## Getting a file's extended properties

Aside from the top-level and basic file properties, there are many properties associated with the file's contents. These extended properties are accessed by calling the [**BasicProperties.RetrievePropertiesAsync**](/uwp/api/windows.storage.fileproperties.basicproperties.retrievepropertiesasync) method. (A [**BasicProperties**](/uwp/api/Windows.Storage.FileProperties.BasicProperties) object is obtained by calling the [**StorageFile.Properties**](/uwp/api/windows.storage.storagefile.properties) property.) While top-level and basic file properties are accessible as properties of a class—[**StorageFile**](/uwp/api/Windows.Storage.StorageFile) and **BasicProperties**, respectively—extended properties are obtained by passing an [IEnumerable](/dotnet/api/system.collections.ienumerable) collection of [String](/dotnet/api/system.string) objects representing the names of the properties that are to be retrieved to the **BasicProperties.RetrievePropertiesAsync** method. This method then returns an [IDictionary](/dotnet/api/system.collections.idictionary) collection. Each extended property is then retrieved from the collection by name or by index.

This example enumerates all of the files in the Pictures library, specifies the names of desired properties (**DataAccessed** and **FileOwner**) in a [List](/dotnet/api/system.collections.generic.list-1) object, passes that [List](/dotnet/api/system.collections.generic.list-1) object to [**BasicProperties.RetrievePropertiesAsync**](/uwp/api/windows.storage.fileproperties.basicproperties.retrievepropertiesasync) to retrieve those properties, and then retrieves those properties by name from the returned [IDictionary](/dotnet/api/system.collections.idictionary) object.

See the [Windows Core Properties](/windows/desktop/properties/core-bumper) for a complete list of a file's extended properties.

```csharp
const string dateAccessedProperty = "System.DateAccessed";
const string fileOwnerProperty = "System.FileOwner";

// Enumerate all files in the Pictures library.
var folder = KnownFolders.PicturesLibrary;
var query = folder.CreateFileQuery();
var files = await query.GetFilesAsync();

foreach (Windows.Storage.StorageFile file in files)
{
    StringBuilder fileProperties = new StringBuilder();

    // Define property names to be retrieved.
    var propertyNames = new List<string>();
    propertyNames.Add(dateAccessedProperty);
    propertyNames.Add(fileOwnerProperty);

    // Get extended properties.
    IDictionary<string, object> extraProperties =
        await file.Properties.RetrievePropertiesAsync(propertyNames);

    // Get date-accessed property.
    var propValue = extraProperties[dateAccessedProperty];
    if (propValue != null)
    {
        fileProperties.AppendLine("Date accessed: " + propValue);
    }

    // Get file-owner property.
    propValue = extraProperties[fileOwnerProperty];
    if (propValue != null)
    {
        fileProperties.AppendLine("File owner: " + propValue);
    }
}
```

 

 