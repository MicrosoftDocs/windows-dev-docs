---
description: This article describes how to import media from a device, including searching for available media sources, importing files such as photos and sidecar files, and deleting the imported files from the source device.
title: Import media from a device
ms.date: 08/23/2026
ms.topic: article
keywords: windows, winui, photo import, media import, camera, device
ms.localizationpriority: medium
---
# Import media from a device

This article describes how to import media from a device, including searching for available media sources, importing files such as videos, photos, and sidecar files, and deleting the imported files from the source device.

> [!NOTE]
> The code in this article was adapted from the [**MediaImport UWP app sample**](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/MediaImport). You can clone or download this sample from the [**Universal Windows app samples Git repo**](https://github.com/Microsoft/Windows-universal-samples) to see the code in context or to use it as a starting point for your own app.

## Create a simple media import UI

The example in this article uses a minimal UI to enable the core media import scenarios. To see how to create a more robust UI for a media import app, see the [**MediaImport sample**](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/MediaImport). The following XAML creates a stack panel with the following controls:

* A [**Button**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.button) to initiate searching for sources from which media can be imported.
* A [**ComboBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.combobox) to list and select from the media import sources that are found.
* A [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) control to display and select from the media items from the selected import source.
* A **Button** to initiate importing media items from the selected source.
* A **Button** to initiate deleting the items that have been imported from the selected source.
* A **Button** to cancel an asynchronous media import operation.

```xml
<ScrollViewer VerticalScrollBarVisibility="Auto">
    <StackPanel Padding="24" Spacing="16">
        <TextBlock
            Text="Import media from device"
            Style="{StaticResource SubtitleTextBlockStyle}" />

        <TextBlock
            Text="Choose a connected device source, review the files that can be imported, and then import or delete imported files."
            TextWrapping="WrapWholeWords" />

        <StackPanel Spacing="8">
            <TextBlock
                Text="Source"
                Style="{StaticResource BodyStrongTextBlockStyle}" />
            <Button
                x:Name="findSourcesButton"
                Content="Find sources"
                Click="findSourcesButton_Click" />
            <ComboBox
                x:Name="sourcesComboBox"
                PlaceholderText="Select a device source"
                SelectionChanged="sourcesComboBox_SelectionChanged" />
        </StackPanel>

        <StackPanel Spacing="8">
            <TextBlock
                Text="Importable items"
                Style="{StaticResource BodyStrongTextBlockStyle}" />
            <ListView
                x:Name="fileListView"
                MinHeight="320"
                MaxHeight="480"
                SelectionMode="None"
                BorderBrush="{ThemeResource CardStrokeColorDefaultBrush}"
                BorderThickness="1"
                ScrollViewer.VerticalScrollBarVisibility="Visible">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <Grid ColumnSpacing="12" Padding="0,8">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="Auto" />
                                <ColumnDefinition Width="Auto" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>

                            <CheckBox
                                Grid.Column="0"
                                VerticalAlignment="Center"
                                IsChecked="{Binding ImportableItem.IsSelected, Mode=TwoWay}" />

                            <Image
                                Grid.Column="1"
                                Width="120"
                                Height="120"
                                Source="{Binding Thumbnail}"
                                Stretch="UniformToFill" />

                            <TextBlock
                                Grid.Column="2"
                                VerticalAlignment="Center"
                                Text="{Binding ImportableItem.Name}"
                                TextWrapping="WrapWholeWords" />
                        </Grid>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </StackPanel>

        <StackPanel Spacing="8">
            <TextBlock
                Text="Import actions"
                Style="{StaticResource BodyStrongTextBlockStyle}" />
            <StackPanel Orientation="Horizontal" Spacing="12">
                <Button
                    x:Name="importButton"
                    Content="Import"
                    Click="importButton_Click" />
                <Button
                    x:Name="deleteButton"
                    Content="Delete imported"
                    Click="deleteButton_Click" />
                <Button
                    x:Name="cancelButton"
                    Content="Cancel"
                    Click="cancelButton_Click" />
            </StackPanel>
            <ProgressBar
                x:Name="progressBar"
                Minimum="0"
                Maximum="1"
                ShowError="False"
                ShowPaused="False" />
        </StackPanel>

        <TextBlock
            x:Name="statusTextBlock"
            Style="{StaticResource CaptionTextBlockStyle}"
            Foreground="{ThemeResource TextFillColorSecondaryBrush}"
            TextWrapping="WrapWholeWords" />
    </StackPanel>
</ScrollViewer>
```

## Set up task cancellation for media import operations

Because media import operations can take a long time, they are performed asynchronously using [**IAsyncOperationWithProgress**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_). Declare a class member variable of type [**CancellationTokenSource**](/dotnet/api/system.threading.cancellationtokensource) that will be used to cancel an in-progress operation if the user clicks the cancel button.

```csharp
CancellationTokenSource? cts;
```

Implement a handler for the cancel button. The examples shown later in this article will initialize the **CancellationTokenSource** when an operation begins and set it to null when the operation completes. In the cancel button handler, check to see if the token is null, and if not, call [**Cancel**](/dotnet/api/system.threading.cancellationtokensource.cancel) to cancel the operation.

```csharp
private void cancelButton_Click(object sender, RoutedEventArgs e)
{
    if (cts is not null)
    {
        cts.Cancel();
        SetStatus("Operation canceled by the Cancel button.");
    }
}
```

## Data binding helper classes

In a typical media import scenario you show the user a list of available media items to import. There can be a large number of media files to choose from and, typically, you want to show a thumbnail for each media item. For this reason, this example uses three helper classes to incrementally load entries into the ListView control as the user scrolls down through the list.

* **IncrementalLoadingBase** class - Implements the [**IList**](/dotnet/api/system.collections.ilist), [**ISupportIncrementalLoading**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading), and [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged) to provide the base incremental loading behavior.
* **GeneratorIncrementalLoadingClass** class - Provides an implementation of the incremental loading base class.
* **ImportableItemWrapper** class - A thin wrapper around the [**PhotoImportItem**](/uwp/api/Windows.Media.Import.PhotoImportItem) class to add a bindable [**BitmapImage**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapimage) property for the thumbnail image for each imported item.

These classes are provided in the [**MediaImport sample**](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/MediaImport) and can be added to your project without modifications. After adding the helper classes to your project, declare a class member variable of type **GeneratorIncrementalLoadingClass** that will be used later in this example.

```csharp
GeneratorIncrementalLoadingClass<ImportableItemWrapper>? itemsToImport = null;
```

## Find available sources from which media can be imported

In the click handler for the find sources button, call the static method [**PhotoImportManager.FindAllSourcesAsync**](/uwp/api/windows.media.import.photoimportmanager.findallsourcesasync) to start the system searching for devices from which media can be imported. After awaiting the completion of the operation, loop through each [**PhotoImportSource**](/uwp/api/Windows.Media.Import.PhotoImportSource) object in the returned list and add an entry to the **ComboBox**, setting the **Tag** property to the source object itself so it can be easily retrieved when the user makes a selection.

```csharp
private async void findSourcesButton_Click(object sender, RoutedEventArgs e)
{
    var sources = await PhotoImportManager.FindAllSourcesAsync();
    sourcesComboBox.Items.Clear();

    foreach (PhotoImportSource source in sources)
    {
        ComboBoxItem item = new();
        item.Content = source.DisplayName;
        item.Tag = source;
        sourcesComboBox.Items.Add(item);
    }

    SetStatus($"Found {sources.Count} import source(s).");
}
```

Declare a class member variable to store the user's selected import source.

```csharp
PhotoImportSource? importSource;
```

In the [**SelectionChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectionchanged) handler for the import source **ComboBox**, set the class member variable to the selected source and then call the **FindItems** helper method which will be shown later in this article.

```csharp
private void sourcesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (sourcesComboBox.SelectedItem is ComboBoxItem item && item.Tag is PhotoImportSource selectedSource)
    {
        importSource = selectedSource;
        FindItems();
    }
}
```

## Find items to import

Add class member variables of type [**PhotoImportSession**](/uwp/api/Windows.Media.Import.PhotoImportSession) and [**PhotoImportFindItemsResult**](/uwp/api/Windows.Media.Import.PhotoImportFindItemsResult) which will be used in the following steps.

```csharp
PhotoImportSession? importSession;
PhotoImportFindItemsResult? itemsResult;
```

In the **FindItems** method, initialize the **CancellationTokenSource** variable so it can be used to cancel the find operation if necessary. Within a **try** block, create a new import session by calling [**CreateImportSession**](/uwp/api/windows.media.import.photoimportsource.createimportsession) on the [**PhotoImportSource**](/uwp/api/Windows.Media.Import.PhotoImportSource) object selected by the user. Create a new [**Progress**](/dotnet/api/system.progress-1) object to provide a callback to display the progress of the find operation. Next, call **[FindItemsAsync](/uwp/api/windows.media.import.photoimportsession.finditemsasync)** to start the find operation. Provide a [**PhotoImportContentTypeFilter**](/uwp/api/Windows.Media.Import.PhotoImportContentTypeFilter) value to specify whether photos, videos, or both should be returned. Provide a [**PhotoImportItemSelectionMode**](/uwp/api/Windows.Media.Import.PhotoImportItemSelectionMode) value to specify whether all, none, or only the new media items are returned with their [**IsSelected**](/uwp/api/windows.media.import.photoimportitem.isselected) property set to true. This property is bound to a checkbox for each media item in our ListView item template.

**FindItemsAsync** returns an [**IAsyncOperationWithProgress**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_). The extension method [**AsTask**](/dotnet/api/system) is used to create a task that can be awaited, can be cancelled with the cancellation token, and that reports progress using the supplied **Progress** object.

Next the data binding helper class, **GeneratorIncrementalLoadingClass** is initialized. **FindItemsAsync**, when it returns from being awaited, returns a [**PhotoImportFindItemsResult**](/uwp/api/Windows.Media.Import.PhotoImportFindItemsResult) object. This object contains status information about the find operation, including the success of the operation and the count of different types of media items that were found. The [**FoundItems**](/uwp/api/windows.media.import.photoimportfinditemsresult.founditems) property contains a list of [**PhotoImportItem**](/uwp/api/Windows.Media.Import.PhotoImportItem) objects representing the found media items. The **GeneratorIncrementalLoadingClass** constructor takes as arguments the total count of items that will be loaded incrementally, and a function that generates new items to be loaded as needed. In this case, the provided lambda expression creates a new instance of the **ImportableItemWrapper** which wraps **PhotoImportItem** and includes a thumbnail for each item. Once the incremental loading class has been initialized, set it to the [**ItemsSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemssource) property of the **ListView** control in the UI. Now, the found media items will be loaded incrementally and displayed in the list.

Finally, set the cancellation token to null because the operation is complete.

```csharp
private async void FindItems()
{
    if (importSource is null)
    {
        SetStatus("Select an import source first.");
        return;
    }

    cts = new CancellationTokenSource();

    try
    {
        importSession = importSource.CreateImportSession();

        var progress = new Progress<uint>((result) =>
        {
            SetStatus($"Found {result} file(s)...");
        });

        var currentItemsResult =
            await importSession.FindItemsAsync(PhotoImportContentTypeFilter.ImagesAndVideos, PhotoImportItemSelectionMode.SelectAll)
            .AsTask(cts.Token, progress);

        itemsResult = currentItemsResult;

        itemsToImport = new GeneratorIncrementalLoadingClass<ImportableItemWrapper>(currentItemsResult.TotalCount,
        (int index) =>
        {
            return new ImportableItemWrapper(currentItemsResult.FoundItems[index]);
        });

        fileListView.ItemsSource = itemsToImport;

        var findResultProperties = new StringBuilder();
        findResultProperties.AppendLine($"Photos\t\t\t :  {currentItemsResult.PhotosCount} \t\t Selected Photos\t\t:  {currentItemsResult.SelectedPhotosCount}");
        findResultProperties.AppendLine($"Videos\t\t\t :  {currentItemsResult.VideosCount} \t\t Selected Videos\t\t:  {currentItemsResult.SelectedVideosCount}");
        findResultProperties.AppendLine($"SideCars\t\t :  {currentItemsResult.SidecarsCount} \t\t Selected Sidecars\t:  {currentItemsResult.SelectedSidecarsCount}");
        findResultProperties.AppendLine($"Siblings\t\t\t :  {currentItemsResult.SiblingsCount} \t\t Selected Sibilings\t:  {currentItemsResult.SelectedSiblingsCount}");
        findResultProperties.AppendLine($"Total Items Items\t :  {currentItemsResult.TotalCount} \t\t Selected TotalCount \t:  {currentItemsResult.SelectedTotalCount}");
        System.Diagnostics.Debug.WriteLine(findResultProperties.ToString());

        if (currentItemsResult.HasSucceeded)
        {
            SetStatus("FindItemsAsync succeeded.");
        }
        else
        {
            SetStatus("FindItemsAsync did not succeed or was not completed.");
        }
    }
    catch (Exception ex)
    {
        SetStatus("Photo import find items operation failed. " + ex.Message);
    }

    cts = null;
}
```

## Import media items

Before implementing the import operation, declare a [**PhotoImportImportItemsResult**](/uwp/api/Windows.Media.Import.PhotoImportImportItemsResult) object to store the results of the import operation. This will be used later to delete media items that were successfully imported from the source.

```csharp
private PhotoImportImportItemsResult? importedResult;
```

Before starting the media import operation, initialize the **CancellationTokenSource** variable and set the value of the [**ProgressBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.progressbar) control to 0.

If there are no selected items in the **ListView** control, then there is nothing to import. Otherwise, initialize a [**Progress**](/dotnet/api/system.progress-1) object to provide a progress callback which updates the value of the progress bar control. Register a handler for the [**ItemImported**](/uwp/api/windows.media.import.photoimportfinditemsresult.itemimported) event of the [**PhotoImportFindItemsResult**](/uwp/api/Windows.Media.Import.PhotoImportFindItemsResult) returned by the find operation. This event will be raised whenever an item is imported and, in this example, outputs the name of each imported file to the debug console.

Call [**ImportItemsAsync**](/uwp/api/windows.media.import.photoimportfinditemsresult.importitemsasync) to begin the import operation. Just as with the find operation, the [**AsTask**](/dotnet/api/system) extension method is used to convert the returned operation to a task that can be awaited, reports progress, and can be cancelled.

After the import operation is complete, the operation status can be obtained from the [**PhotoImportImportItemsResult**](/uwp/api/Windows.Media.Import.PhotoImportImportItemsResult) object returned by [**ImportItemsAsync**](/uwp/api/windows.media.import.photoimportfinditemsresult.importitemsasync). This example outputs the status information to the debug console and then finally, sets the cancellation token to null.

```csharp
private async void importButton_Click(object sender, RoutedEventArgs e)
{
    if (itemsResult is null)
    {
        SetStatus("Find importable items before importing.");
        return;
    }

    cts = new CancellationTokenSource();
    progressBar.Value = 0;

    try
    {
        if (itemsResult.SelectedTotalCount <= 0)
        {
            SetStatus("Nothing Selected for Import.");
        }
        else
        {
            var progress = new Progress<PhotoImportProgress>((result) =>
            {
                progressBar.Value = result.ImportProgress;
            });

            itemsResult.ItemImported += (s, a) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"Imported: {a.ImportedItem.Name}");
                });
            };

            importedResult = await itemsResult.ImportItemsAsync().AsTask(cts.Token, progress);

            if (importedResult is not null)
            {
                StringBuilder importedSummary = new();
                importedSummary.AppendLine($"Photos Imported   \t:  {importedResult.PhotosCount} ");
                importedSummary.AppendLine($"Videos Imported    \t:  {importedResult.VideosCount} ");
                importedSummary.AppendLine($"SideCars Imported   \t:  {importedResult.SidecarsCount} ");
                importedSummary.AppendLine($"Siblings Imported   \t:  {importedResult.SiblingsCount} ");
                importedSummary.AppendLine($"Total Items Imported \t:  {importedResult.TotalCount} ");
                importedSummary.AppendLine($"Total Bytes Imported \t:  {importedResult.TotalSizeInBytes} ");

                System.Diagnostics.Debug.WriteLine(importedSummary.ToString());
            }

            if (importedResult is null || !importedResult.HasSucceeded)
            {
                SetStatus("ImportItemsAsync did not succeed or was not completed");
            }
            else
            {
                SetStatus("Import completed.");
            }
        }
    }
    catch (Exception ex)
    {
        SetStatus("Files could not be imported. Exception: " + ex);
    }

    cts = null;
}
```

## Delete imported items

To delete the successfully imported items from the source from which they were imported, first initialize the cancellation token so that the delete operation can be cancelled and set the progress bar value to 0. Make sure that the [**PhotoImportImportItemsResult**](/uwp/api/Windows.Media.Import.PhotoImportImportItemsResult) returned from [**ImportItemsAsync**](/uwp/api/windows.media.import.photoimportfinditemsresult.importitemsasync) is not null. If not, once again create a [**Progress**](/dotnet/api/system.progress-1) object to provide a progress callback for the delete operation. Call [**DeleteImportedItemsFromSourceAsync**](/uwp/api/windows.media.import.photoimportimportitemsresult.deleteimporteditemsfromsourceasync) to start deleting the imported items. Use **AsTask** to convert the result to an awaitable task with progress and cancellation capabilities. After awaiting, the returned [**PhotoImportDeleteImportedItemsFromSourceResult**](/uwp/api/Windows.Media.Import.PhotoImportDeleteImportedItemsFromSourceResult) object can be used to get and display status information about the delete operation.

```csharp
private async void deleteButton_Click(object sender, RoutedEventArgs e)
{
    cts = new CancellationTokenSource();
    progressBar.Value = 0;

    try
    {
        if (importedResult is null)
        {
            SetStatus("Nothing was imported for deletion.");
        }
        else
        {
            var progress = new Progress<double>((result) =>
            {
                progressBar.Value = result;
            });

            PhotoImportDeleteImportedItemsFromSourceResult? deleteResult = await importedResult.DeleteImportedItemsFromSourceAsync().AsTask(cts.Token, progress);

            if (deleteResult is not null)
            {
                StringBuilder deletedResults = new();
                deletedResults.AppendLine($"Total Photos Deleted:\t{deleteResult.PhotosCount} ");
                deletedResults.AppendLine($"Total Videos Deleted:\t{deleteResult.VideosCount} ");
                deletedResults.AppendLine($"Total Sidecars Deleted:\t{deleteResult.SidecarsCount} ");
                deletedResults.AppendLine($"Total Sibilings Deleted:\t{deleteResult.SiblingsCount} ");
                deletedResults.AppendLine($"Total Files Deleted:\t{deleteResult.TotalCount} ");
                deletedResults.AppendLine($"Total Bytes Deleted:\t{deleteResult.TotalSizeInBytes} ");
                System.Diagnostics.Debug.WriteLine(deletedResults.ToString());
            }

            if (deleteResult is null || !deleteResult.HasSucceeded)
            {
                SetStatus("Delete operation did not succeed or was not completed");
            }
            else
            {
                SetStatus("Delete operation completed.");
            }
        }
    }
    catch (Exception ex)
    {
        SetStatus("Files could not be deleted. Exception: " + ex);
    }

    cts = null;
}
```

## Related topics

* [**PhotoImportManager**](/uwp/api/Windows.Media.Import.PhotoImportManager)
* [**MediaImport UWP app sample**](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/MediaImport)
