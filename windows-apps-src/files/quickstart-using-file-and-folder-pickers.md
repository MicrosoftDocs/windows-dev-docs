---
author: laurenhughes
ms.assetid: F87DBE2F-77DB-4573-8172-29E11ABEFD34
title: Open files and folders with a picker
description: Access files and folders by letting the user interact with a picker. You can use the FileOpenPicker and FileSavePicker classes to gain access to files, and the FolderPicker to gain access to a folder.
ms.author: lahugh
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Open files and folders with a picker

**Important APIs**

-   [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/br207847)
-   [**FolderPicker**](https://msdn.microsoft.com/library/windows/apps/br207881)
-   [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171)

Access files and folders by letting the user interact with a picker. You can use the [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/br207847) and [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871) classes to access files, and the [**FolderPicker**](https://msdn.microsoft.com/library/windows/apps/br207881) to access a folder.

> [!NOTE]
> For a complete sample, see the [File picker sample](http://go.microsoft.com/fwlink/p/?linkid=619994).

## Prerequisites


-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](https://msdn.microsoft.com/library/windows/apps/mt187337). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](https://msdn.microsoft.com/library/windows/apps/mt187334).

-   **Access permissions to the location**

    See [File access permissions](file-access-permissions.md).

## File picker UI


A file picker displays information to orient users and provide a consistent experience when opening or saving files.

That information includes:

-   The current location
-   The item or items that the user picked
-   A tree of locations that the user can browse to. These locations include file system locations—such as the Music or Downloads folder—as well as apps that implement the file picker contract (such as Camera, Photos, and Microsoft OneDrive).

An email app might display a file picker for the user to pick attachments.

![a file picker with two files picked to be opened.](images/picker-multifile-600px.png)

## How pickers work


With a picker your app can access, browse, and save files and folders on the user's system. Your app receives those picks as [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) and [**StorageFolder**](https://msdn.microsoft.com/library/windows/apps/br227230) objects, which you can then operate on.

The picker uses a single, unified interface to let the user pick files and folders from the file system or from other apps. Files picked from other apps are like files from the file system: they are returned as [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) objects. In general, your app can operate on them in the same ways as other objects. Other apps make files available by participating in file picker contracts. If you want your app to provide files, a save location, or file updates to other apps, see [Integrating with file picker contracts](https://msdn.microsoft.com/library/windows/apps/hh465192).

For example, you might call the file picker in your app so that your user can open a file. This makes your app the calling app. The file picker interacts with the system and/or other apps to let the user navigate and pick the file. When your user chooses a file, the file picker returns that file to your app. Here's the process for the case of the user choosing a file from a providing app, such as OneDrive.

![a diagram that shows the process of one app getting a file to open from another app using the file picker as an interface bewteen the two apps.](images/app-to-app-diagram-600px.png)

## Pick a single file: complete code listing


```cs
var picker = new Windows.Storage.Pickers.FileOpenPicker();
picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
picker.FileTypeFilter.Add(".jpg");
picker.FileTypeFilter.Add(".jpeg");
picker.FileTypeFilter.Add(".png");

Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
if (file != null)
{
    // Application now has read/write access to the picked file
    this.textBlock.Text = "Picked photo: " + file.Name;
}
else
{
    this.textBlock.Text = "Operation cancelled.";
}
```

## Pick a single file: step-by-step


Using a file picker involves creating and customizing a file picker object, and then showing the file picker so the user can pick one or more items.

1.  **Create and customize a FileOpenPicker**

    ```cs
    var picker = new Windows.Storage.Pickers.FileOpenPicker();
    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
    picker.FileTypeFilter.Add(".jpg");
    picker.FileTypeFilter.Add(".jpeg");
    picker.FileTypeFilter.Add(".png");
    ```
    Set properties on the file picker object relevant to your users and app.

    This example creates a rich, visual display of pictures in a convenient location that the user can pick from by setting three properties: [**ViewMode**](https://msdn.microsoft.com/library/windows/apps/br207855), [**SuggestedStartLocation**](https://msdn.microsoft.com/library/windows/apps/br207854), and [**FileTypeFilter**](https://msdn.microsoft.com/library/windows/apps/br207850).

    -   Setting [**ViewMode**](https://msdn.microsoft.com/library/windows/apps/br207855) to the [**PickerViewMode**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.storage.pickers.pickerviewmode.aspx#thumbnail) **Thumbnail** enum value creates a rich, visual display by using picture thumbnails to represent files in the file picker. Do this for picking visual files such as pictures or videos. Otherwise, use [**PickerViewMode.List**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.storage.pickers.pickerviewmode.aspx#list). A hypothetical email app with **Attach Picture or Video** and **Attach Document** features would set the **ViewMode** appropriate to the feature before showing the file picker.

    -   Setting [**SuggestedStartLocation**](https://msdn.microsoft.com/library/windows/apps/br207854) to Pictures using [**PickerLocationId.PicturesLibrary**](https://msdn.microsoft.com/library/windows/apps/br207890) starts the user in a location where they're likely to find pictures. Set **SuggestedStartLocation** to a location appropriate for the type of file being picked, for example Music, Pictures, Videos, or Documents. From the start location, the user can navigate to other locations.

    -   Using [**FileTypeFilter**](https://msdn.microsoft.com/library/windows/apps/br207850) to specify file types keeps the user focused on picking files that are relevant. To replace previous file types in the **FileTypeFilter** with new entries, use [**ReplaceAll**](https://msdn.microsoft.com/library/windows/apps/br207844) instead of [**Add**](https://msdn.microsoft.com/library/windows/apps/br207834).

2.  **Show the FileOpenPicker**

    - **To pick a single file**

    ```cs
    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
    if (file != null)
    {
        // Application now has read/write access to the picked file
        this.textBlock.Text = "Picked photo: " + file.Name;
    }
    else
    {
        this.textBlock.Text = "Operation cancelled.";
    }
    ```

    - **To pick multiple files**  

    ```cs
    var files = await picker.PickMultipleFilesAsync();
    if (files.Count > 0)
    {
        StringBuilder output = new StringBuilder("Picked files:\n");

        // Application now has read/write access to the picked file(s)
        foreach (Windows.Storage.StorageFile file in files)
        {
            output.Append(file.Name + "\n");
        }
        this.textBlock.Text = output.ToString();
    }
    else
    {
        this.textBlock.Text = "Operation cancelled.";
    }
    ```

## Pick a folder: complete code listing


```cs
var folderPicker = new Windows.Storage.Pickers.FolderPicker();
folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
folderPicker.FileTypeFilter.Add("*");

Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
if (folder != null)
{
    // Application now has read/write access to all contents in the picked folder
    // (including other sub-folder contents)
    Windows.Storage.AccessCache.StorageApplicationPermissions.
    FutureAccessList.AddOrReplace("PickedFolderToken", folder);
    this.textBlock.Text = "Picked folder: " + folder.Name;
}
else
{
    this.textBlock.Text = "Operation cancelled.";
}
```

> [!TIP]
> Whenever your app accesses a file or folder through a picker, add it to your app's [**FutureAccessList**](https://msdn.microsoft.com/library/windows/apps/br207457) or [**MostRecentlyUsedList**](https://msdn.microsoft.com/library/windows/apps/br207458) to keep track of it. You can learn more about using these lists in [How to track recently-used files and folders](how-to-track-recently-used-files-and-folders.md).