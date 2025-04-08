---
ms.assetid: 12ECEA89-59D2-4BCE-B24C-5A4DD525E0C7
title: Accessing HomeGroup content
description: Access content stored in the user's HomeGroup folder, including pictures, music, and videos.
ms.date: 12/19/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Accessing HomeGroup content



**Important APIs**

-   [**Windows.Storage.KnownFolders class**](/uwp/api/Windows.Storage.KnownFolders)

Access content stored in the user's HomeGroup folder, including pictures, music, and videos.

## Prerequisites

-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](../threading-async/call-asynchronous-apis-in-csharp-or-visual-basic.md). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

-   **App capability declarations**

    To access HomeGroup content, the user's machine must have a HomeGroup set up and your app must have at least one of the following capabilities: **picturesLibrary**, **musicLibrary**, or **videosLibrary**. When your app accesses the HomeGroup folder, it will see only the libraries that correspond to the capabilities declared in your app's manifest. To learn more, see [File access permissions](file-access-permissions.md).

    > [!NOTE]
    > Content in the Documents library of a HomeGroup isn't visible to your app regardless of the capabilities declared in your app's manifest and regardless of the user's sharing settings.     

-   **Understand how to use file pickers**

    You typically use the file picker to access files and folders in the HomeGroup. To learn how to use the file picker, see [Open files and folders with a picker](quickstart-using-file-and-folder-pickers.md).

-   **Understand file and folder queries**

    You can use queries to enumerate files and folders in the HomeGroup. To learn about file and folder queries, see [Enumerating and querying files and folders](quickstart-listing-files-and-folders.md).

## Open the file picker at the HomeGroup

Follow these steps to open an instance of the file picker that lets the user pick files and folders from the HomeGroup:

1.  **Create and customize the file picker**

    Use [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) to create the file picker, and then set the picker's [**SuggestedStartLocation**](/uwp/api/windows.storage.pickers.fileopenpicker.suggestedstartlocation) to [**PickerLocationId.HomeGroup**](/uwp/api/Windows.Storage.Pickers.PickerLocationId). Or, set other properties that are relevant to your users and your app. For guidelines to help you decide how to customize the file picker, see [Guidelines and checklist for file pickers](./quickstart-using-file-and-folder-pickers.md)

    This example creates a file picker that opens at the HomeGroup, includes files of any type, and displays the files as thumbnail images:
    ```cs
    Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.HomeGroup;
    picker.FileTypeFilter.Clear();
    picker.FileTypeFilter.Add("*");
    ```

2.  **Show the file picker and process the picked file.**

    After you create and customize the file picker, let the user pick one file by calling [**FileOpenPicker.PickSingleFileAsync**](/uwp/api/windows.storage.pickers.fileopenpicker.picksinglefileasync), or multiple files by calling [**FileOpenPicker.PickMultipleFilesAsync**](/uwp/api/windows.storage.pickers.fileopenpicker.pickmultiplefilesasync).

    This example displays the file picker to let the user pick one file:
    ```cs
    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

    if (file != null)
    {
        // Do something with the file.
    }
    else
    {
        // No file returned. Handle the error.
    }   
    ```

## Search the HomeGroup for files

This section shows how to find HomeGroup items that match a query term provided by the user.

1.  **Get the query term from the user.**

    Here we get a query term that the user has entered into a [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox) control called `searchQueryTextBox`:
    ```cs
    string queryTerm = this.searchQueryTextBox.Text;    
    ```

2.  **Set the query options and search filter.**

    Query options determine how the search results are sorted, while the search filter determines which items are included in the search results.

    This example sets query options that sort the search results by relevance and then the date modified. The search filter is the query term that the user entered in the previous step:
    ```cs
    Windows.Storage.Search.QueryOptions queryOptions =
            new Windows.Storage.Search.QueryOptions
                (Windows.Storage.Search.CommonFileQuery.OrderBySearchRank, null);
    queryOptions.UserSearchFilter = queryTerm.Text;
    Windows.Storage.Search.StorageFileQueryResult queryResults =
            Windows.Storage.KnownFolders.HomeGroup.CreateFileQueryWithOptions(queryOptions);    
    ```

3.  **Run the query and process the results.**

    The following example runs the search query in the HomeGroup and saves the names of any matching files as a list of strings.
    ```cs
    System.Collections.Generic.IReadOnlyList<Windows.Storage.StorageFile> files =
        await queryResults.GetFilesAsync();

    if (files.Count > 0)
    {
        outputString += (files.Count == 1) ? "One file found\n" : files.Count.ToString() + " files found\n";
        foreach (Windows.Storage.StorageFile file in files)
        {
            outputString += file.Name + "\n";
        }
    }    
    ```


## Search the HomeGroup for a particular user's shared files

This section shows you how to find HomeGroup files that are shared by a particular user.

1.  **Get a collection of HomeGroup users.**

    Each of the first-level folders in the HomeGroup represents an individual HomeGroup user. So, to get the collection of HomeGroup users, call [**GetFoldersAsync**](/uwp/api/windows.storage.storagefolder.getfoldersasync) retrieve the top-level HomeGroup folders.
    ```cs
    System.Collections.Generic.IReadOnlyList<Windows.Storage.StorageFolder> hgFolders =
        await Windows.Storage.KnownFolders.HomeGroup.GetFoldersAsync();    
    ```

2.  **Find the target user's folder, and then create a file query scoped to that user's folder.**

    The following example iterates through the retrieved folders to find the target user's folder. Then, it sets query options to find all files in the folder, sorted first by relevance and then by the date modified. The example builds a string that reports the number of files found, along with the names of the files.
    ```cs
    bool userFound = false;
    foreach (Windows.Storage.StorageFolder folder in hgFolders)
    {
        if (folder.DisplayName == targetUserName)
        {
            // Found the target user's folder, now find all files in the folder.
            userFound = true;
            Windows.Storage.Search.QueryOptions queryOptions =
                new Windows.Storage.Search.QueryOptions
                    (Windows.Storage.Search.CommonFileQuery.OrderBySearchRank, null);
            queryOptions.UserSearchFilter = "*";
            Windows.Storage.Search.StorageFileQueryResult queryResults =
                folder.CreateFileQueryWithOptions(queryOptions);
            System.Collections.Generic.IReadOnlyList<Windows.Storage.StorageFile> files =
                await queryResults.GetFilesAsync();

            if (files.Count > 0)
            {
                string outputString = "Searched for files belonging to " + targetUserName + "'\n";
                outputString += (files.Count == 1) ? "One file found\n" : files.Count.ToString() + " files found\n";
                foreach (Windows.Storage.StorageFile file in files)
                {
                    outputString += file.Name + "\n";
                }
            }
        }
    }    
    ```

## Stream video from the HomeGroup

Follow these steps to stream video content from the HomeGroup:

1.  **Include a MediaElement in your app.**

    A [**MediaElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaElement) lets you play back audio and video content in your app. For more information on audio and video playback, see [Create custom transport controls](/windows/apps/design/controls/custom-transport-controls) and [Audio, video, and camera](../audio-video-camera/index.md).
    ```HTML
    <Grid x:Name="Output" HorizontalAlignment="Left" VerticalAlignment="Top" Grid.Row="1">
        <MediaElement x:Name="VideoBox" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="0" Width="400" Height="300"/>
    </Grid>    
    ```

2.  **Open a file picker at the HomeGroup and apply a filter that includes video files in the formats that your app supports.**

    This example includes .mp4 and .wmv files in the file open picker.
    ```cs
    Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.HomeGroup;
    picker.FileTypeFilter.Clear();
    picker.FileTypeFilter.Add(".mp4");
    picker.FileTypeFilter.Add(".wmv");
    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();   
    ```

3.  **Open the user's file selection for read access, and set the file stream as the source for the** [**MediaElement**](/uwp/api/Windows.UI.Xaml.Controls.MediaElement), and then play the file.
    ```cs
    if (file != null)
    {
        var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
        VideoBox.SetSource(stream, file.ContentType);
        VideoBox.Stop();
        VideoBox.Play();
    }
    else
    {
        // No file selected. Handle the error here.
    }    
    ```