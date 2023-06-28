---
ms.assetid: 385ede76-fb55-4ef4-a16b-3d9ccfc7367a
title: Access files and folders with Windows App SDK and WinRT APIs
description: Packaged Windows App SDK apps can leverage WinRT APIs for reading and writing app settings, file and folder pickers, and special sand-boxed locations such as the Video/Music library.
ms.date: 06/16/2023
ms.topic: article
keywords: windows 10, windows 11, windows, winui, windows app sdk, winrt
ms.localizationpriority: medium
---
# Access files and folders with Windows App SDK and WinRT APIs

Packaged Windows App SDK apps can leverage [WinRT APIs](/uwp/api/) for reading and writing app settings, file and folder pickers, and special sand-boxed locations such as the Video/Music library. Additionally, any packaged desktop app can utilize both WinRT and Win32 APIs in the Windows SDK, as well as the APIs provided in the .NET SDK. This article provides guidance on how to use the WinRT storage APIs to query files and folders, retrieve file properties, and work with the **Pictures** library.

## Query files and folders

The following example shows how to use the [StorageFolder](/uwp/api/windows.storage.storagefolder) and [StorageFile](/uwp/api/windows.storage.storagefile) APIs to query the **Documents** library for files and folders. The example uses the `GetFilesInFolderAsync` method to recursively iterate through the folder structure and append the file names to a `StringBuilder` object.

```csharp
using System.Text;
using Windows.Storage;
...
private async Task<string> GetDocumentsContentsAsync()
{
    StorageFolder docsFolder = KnownFolders.DocumentsLibrary;
    StringBuilder outputBuilder = new();
    await GetFilesInFolderAsync(docsFolder, outputBuilder);

    return outputBuilder.ToString();
}

private async Task GetFilesInFolderAsync(StorageFolder folder, StringBuilder outputBuilder)
{
    IReadOnlyList<IStorageItem> storageItem = await folder.GetItemsAsync();

    foreach (var item in storageItem)
    {
        if (item is StorageFolder)
        {
            await GetFilesInFolderAsync(item as StorageFolder, outputBuilder);
        }
        else
        {
            outputBuilder.AppendLine($"Found {item.Name} in folder {folder.Name}");
        }
    }
}
```

## Get basic file properties

The following example takes the `GetFilesInFolderAsync` method from the previous example and adds the ability to retrieve the file size and date modified for each file. The example uses the [BasicProperties](/uwp/api/windows.storage.fileproperties.basicproperties) API to retrieve the file size and date modified for each file, formats the file size, and appends the size and date modified to the `StringBuilder` object after each file and folder name.

```csharp
using System.Text;
using Windows.Storage;
using Windows.Storage.FileProperties;
...
private async Task GetFilesInFolderAsync(StorageFolder folder, StringBuilder outputBuilder)
{
    IReadOnlyList<IStorageItem> storageItem = await folder.GetItemsAsync();

    foreach (var item in storageItem)
    {
        if (item is StorageFolder)
        {
            await GetFilesInFolderAsync(item as StorageFolder, outputBuilder);
        }
        else
        {
            outputBuilder.AppendLine($"Found {item.Name} in folder {folder.Name}");

            // Append each file's size and date modified.
            BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
            string fileSize = string.Format("{0:n0}", basicProperties.Size);
            outputBuilder.AppendLine($" - File size: {fileSize} bytes");
            outputBuilder.AppendLine($" - Date modified: {basicProperties.DateModified}");
        }
    }
}
```

## Working with the Pictures library

In this example, the app is configured to receive notifications when the **Pictures** library is updated. The example uses the [StorageLibrary](/uwp/api/windows.storage.storagelibrary) API to retrieve the **Pictures** library and the [DefinitionChanged](/uwp/api/windows.storage.storagelibrary.definitionchanged) event to receive notifications when the library is updated. The `DefinitionChanged` event is invoked when the list of folders in the current library changes. The example uses the library's `Folders` property to iterate through the folders in the **Pictures** library and writes the folder name to the console.

```csharp
using Windows.Storage;
...
private async Task Configure()
{
    StorageLibrary picturesFolder = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
    picturesFolder.DefinitionChanged += picturesFolder_DefinitionChanged;
}
private void picturesFolder_DefinitionChanged(StorageLibrary sender, object args)
{
    foreach (StorageFolder item in sender.Folders)
    {
        Console.WriteLine($"Folder {item.Name} found.");
    }
}
```

## See also

[Access files and folders with Windows App SDK and .NET APIs](dotnet-files.md)

[Files, folders, and libraries with Windows App SDK](index.md)
