---
author: laurenhughes
ms.assetid: 8BDDE64A-77D2-4F9D-A1A0-E4C634BCD890
title: Save a file with a picker
description: Use FileSavePicker to let users specify the name and location where they want your app to save a file.
ms.author: lahugh
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Save a file with a picker

**Important APIs**

-   [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871)
-   [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171)

Use [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871) to let users specify the name and location where they want your app to save a file.

> [!NOTE]
> Also see the [File picker sample](http://go.microsoft.com/fwlink/p/?linkid=619994).

 

## Prerequisites


-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](https://msdn.microsoft.com/library/windows/apps/mt187337). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](https://msdn.microsoft.com/library/windows/apps/mt187334).

-   **Access permissions to the location**

    See [File access permissions](file-access-permissions.md).

## FileSavePicker: step-by-step

Use a [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871) so that your users can specify the name, type, and location of a file to save. Create, customize, and show a file picker object, and then save data via the returned [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) object that represents the file picked.

1.  **Create and customize the FileSavePicker**

```cs
var savePicker = new Windows.Storage.Pickers.FileSavePicker();
savePicker.SuggestedStartLocation =
    Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
// Dropdown of file types the user can save the file as
savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
// Default file name if the user does not type one in or select a file to replace
savePicker.SuggestedFileName = "New Document";
```

Set properties on the file picker object that are relevant to your users and your app.

This example sets three properties: [**SuggestedStartLocation**](https://msdn.microsoft.com/library/windows/apps/br207880), [**FileTypeChoices**](https://msdn.microsoft.com/library/windows/apps/br207875) and [**SuggestedFileName**](https://msdn.microsoft.com/library/windows/apps/br207878).

> [!NOTE]
> [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871) objects display the file picker using the [**PickerViewMode.List**](https://msdn.microsoft.com/library/windows/apps/br207891).
     
- Because our user is saving a document or text file, the sample sets [**SuggestedStartLocation**](https://msdn.microsoft.com/library/windows/apps/br207880) to the app's local folder by using [**LocalFolder**](https://msdn.microsoft.com/library/windows/apps/br241621). Set [**SuggestedStartLocation**](https://msdn.microsoft.com/library/windows/apps/br207854) to a location appropriate for the type of file being saved, for example Music, Pictures, Videos, or Documents. From the start location, the user can navigate to other locations.

- Because we want to make sure our app can open the file after it is saved, we use [**FileTypeChoices**](https://msdn.microsoft.com/library/windows/apps/br207875) to specify file types that the sample supports (Microsoft Word documents and text files). Make sure all the file types that you specify are supported by your app. Users will be able to save their file as any of the file types you specify. They can also change the file type by selecting another of the file types that you specified. The first file type choice in the list will be selected by default: to control that, set the [**DefaultFileExtension**](https://msdn.microsoft.com/library/windows/apps/br207873) property.

> [!NOTE]
> The file picker also uses the currently selected file type to filter which files it displays, so that only file types that match the selected files types are displayed to the user.

- To save the user some typing, the example sets a [**SuggestedFileName**](https://msdn.microsoft.com/library/windows/apps/br207878). Make your suggested file name relevant to the file being saved. For example, like Word, you can suggest the existing file name if there is one, or the first line of a document if the user is saving a file that does not yet have a name.

2.  **Show the FileSavePicker and save to the picked file**

    Display the file picker by calling [**PickSaveFileAsync**](https://msdn.microsoft.com/library/windows/apps/br207876). After the user specifies the name, file type, and location, and confirms to save the file, **PickSaveFileAsync** returns a [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) object that represents the saved file. You can capture and process this file now that you have read and write access to it.

    ```cs
    Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
        if (file != null)
        {
            // Prevent updates to the remote version of the file until
            // we finish making changes and call CompleteUpdatesAsync.
            Windows.Storage.CachedFileManager.DeferUpdates(file);
            // write to file
            await Windows.Storage.FileIO.WriteTextAsync(file, file.Name);
            // Let Windows know that we're finished changing the file so
            // the other app can update the remote version of the file.
            // Completing updates may require Windows to ask for user input.
            Windows.Storage.Provider.FileUpdateStatus status =
                await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
            {
                this.textBlock.Text = "File " + file.Name + " was saved.";
            }
            else
            {
                this.textBlock.Text = "File " + file.Name + " couldn't be saved.";
            }
        }
        else
        {
            this.textBlock.Text = "Operation cancelled.";
        }
    ```

The example checks that the file is valid and writes its own file name into it. Also see [Creating, writing, and reading a file](quickstart-reading-and-writing-files.md).

**Tip**  You should always check the saved file to make sure it is valid before you perform any other processing. Then, you can save content to the file as appropriate for your app, and provide appropriate behavior if the picked file is not valid.
