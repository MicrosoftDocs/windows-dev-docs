---
Description: Windows apps that support Windows Ink can serialize and deserialize ink strokes to an Ink Serialized Format (ISF) file. The ISF file is a GIF image with additional metadata for all ink stroke properties and behaviors. Apps that are not ink-enabled, can view the static GIF image, including alpha-channel background transparency.
title: Store and retrieve Windows Ink stroke data
ms.assetid: C96C9D2F-DB69-4883-9809-4A0DF7CEC506
label: Store and retrieve Windows Ink stroke data
template: detail.hbs
keywords: Windows Ink, Windows Inking, DirectInk, InkPresenter, InkCanvas, ISF, Ink Serialized Format, user interaction, input
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Store and retrieve Windows Ink stroke data


Windows apps that support Windows Ink can serialize and deserialize ink strokes to an Ink Serialized Format (ISF) file. The ISF file is a GIF image with additional metadata for all ink stroke properties and behaviors. Apps that are not ink-enabled, can view the static GIF image, including alpha-channel background transparency.

> [!NOTE]
> ISF is the most compact persistent representation of ink. It can be embedded within a binary document format, such as a GIF file, or placed directly on the Clipboard.

> **Important APIs**: [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas), [**Windows.UI.Input.Inking**](/uwp/api/Windows.UI.Input.Inking)

## Save ink strokes to a file

Here, we demonstrate how to save ink strokes drawn on an [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas) control.

**Download this sample from [Save and load ink strokes from an Ink Serialized Format (ISF) file](https://github.com/MicrosoftDocs/windows-topic-specific-samples/archive/uwp-ink-store.zip)**

1.  First, we set up the UI.

    The UI includes "Save", "Load", and "Clear" buttons, and the [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas).
```    XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <StackPanel x:Name="HeaderPanel" Orientation="Horizontal" Grid.Row="0">
            <TextBlock x:Name="Header" 
                       Text="Basic ink store sample" 
                       Style="{ThemeResource HeaderTextBlockStyle}" 
                       Margin="10,0,0,0" />
            <Button x:Name="btnSave" 
                    Content="Save" 
                    Margin="50,0,10,0"/>
            <Button x:Name="btnLoad" 
                    Content="Load" 
                    Margin="50,0,10,0"/>
            <Button x:Name="btnClear" 
                    Content="Clear" 
                    Margin="50,0,10,0"/>
        </StackPanel>
        <Grid Grid.Row="1">
            <InkCanvas x:Name="inkCanvas" />
        </Grid>
    </Grid>
```

2.  We then set some basic ink input behaviors.

    The [**InkPresenter**](/uwp/api/windows.ui.xaml.controls.inkcanvas.inkpresenter) is configured to interpret input data from both pen and mouse as ink strokes ([**InputDeviceTypes**](/uwp/api/windows.ui.input.inking.inkpresenter.inputdevicetypes)), and listeners for the click events on the buttons are declared.
```csharp
public MainPage()
    {
        this.InitializeComponent();

        // Set supported inking device types.
        inkCanvas.InkPresenter.InputDeviceTypes =
            Windows.UI.Core.CoreInputDeviceTypes.Mouse |
            Windows.UI.Core.CoreInputDeviceTypes.Pen;

        // Listen for button click to initiate save.
        btnSave.Click += btnSave_Click;
        // Listen for button click to initiate load.
        btnLoad.Click += btnLoad_Click;
        // Listen for button click to clear ink canvas.
        btnClear.Click += btnClear_Click;
    }
```

3.  Finally, we save the ink in the click event handler of the **Save** button.

    A [**FileSavePicker**](/uwp/api/Windows.Storage.Pickers.FileSavePicker) lets the user select both the file and the location where the ink data is saved.

    Once a file is selected, we open an [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream) stream set to [**ReadWrite**](/uwp/api/Windows.Storage.FileAccessMode).

    We then call [**SaveAsync**](/uwp/api/windows.ui.input.inking.iinkstrokecontainer.saveasync) to serialize the ink strokes managed by the [**InkStrokeContainer**](/uwp/api/Windows.UI.Input.Inking.InkStrokeContainer) to the stream.

```csharp
// Save ink data to a file.
    private async void btnSave_Click(object sender, RoutedEventArgs e)
    {
        // Get all strokes on the InkCanvas.
        IReadOnlyList<InkStroke> currentStrokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();

        // Strokes present on ink canvas.
        if (currentStrokes.Count > 0)
        {
            // Let users choose their ink file using a file picker.
            // Initialize the picker.
            Windows.Storage.Pickers.FileSavePicker savePicker = 
                new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = 
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add(
                "GIF with embedded ISF", 
                new List<string>() { ".gif" });
            savePicker.DefaultFileExtension = ".gif";
            savePicker.SuggestedFileName = "InkSample";

            // Show the file picker.
            Windows.Storage.StorageFile file = 
                await savePicker.PickSaveFileAsync();
            // When chosen, picker returns a reference to the selected file.
            if (file != null)
            {
                // Prevent updates to the file until updates are 
                // finalized with call to CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // Open a file stream for writing.
                IRandomAccessStream stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                // Write the ink strokes to the output stream.
                using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                {
                    await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                    await outputStream.FlushAsync();
                }
                stream.Dispose();

                // Finalize write so other apps can update file.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);

                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    // File saved.
                }
                else
                {
                    // File couldn't be saved.
                }
            }
            // User selects Cancel and picker returns null.
            else
            {
                // Operation cancelled.
            }
        }
    }
```

> [!NOTE]
> GIF is the only file format supported for saving ink data. However, the [**LoadAsync**](/uwp/api/windows.ui.input.inking.inkmanager.loadasync) method (demonstrated in the next section) does support additional formats for backward compatibility.

## Load ink strokes from a file

Here, we demonstrate how to load ink strokes from a file and render them on an [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas) control.

**Download this sample from [Save and load ink strokes from an Ink Serialized Format (ISF) file](https://github.com/MicrosoftDocs/windows-topic-specific-samples/archive/uwp-ink-store.zip)**

1.  First, we set up the UI.

    The UI includes "Save", "Load", and "Clear" buttons, and the [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas).
```    XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <StackPanel x:Name="HeaderPanel" Orientation="Horizontal" Grid.Row="0">
            <TextBlock x:Name="Header" 
                       Text="Basic ink store sample" 
                       Style="{ThemeResource HeaderTextBlockStyle}" 
                       Margin="10,0,0,0" />
            <Button x:Name="btnSave" 
                    Content="Save" 
                    Margin="50,0,10,0"/>
            <Button x:Name="btnLoad" 
                    Content="Load" 
                    Margin="50,0,10,0"/>
            <Button x:Name="btnClear" 
                    Content="Clear" 
                    Margin="50,0,10,0"/>
        </StackPanel>
        <Grid Grid.Row="1">
            <InkCanvas x:Name="inkCanvas" />
        </Grid>
    </Grid>
```

2.  We then set some basic ink input behaviors.

    The [**InkPresenter**](/uwp/api/windows.ui.xaml.controls.inkcanvas.inkpresenter) is configured to interpret input data from both pen and mouse as ink strokes ([**InputDeviceTypes**](/uwp/api/windows.ui.input.inking.inkpresenter.inputdevicetypes)), and listeners for the click events on the buttons are declared.
```csharp
public MainPage()
    {
        this.InitializeComponent();

        // Set supported inking device types.
        inkCanvas.InkPresenter.InputDeviceTypes =
            Windows.UI.Core.CoreInputDeviceTypes.Mouse |
            Windows.UI.Core.CoreInputDeviceTypes.Pen;

        // Listen for button click to initiate save.
        btnSave.Click += btnSave_Click;
        // Listen for button click to initiate load.
        btnLoad.Click += btnLoad_Click;
        // Listen for button click to clear ink canvas.
        btnClear.Click += btnClear_Click;
    }
```

3.  Finally, we load the ink in the click event handler of the **Load** button.

    A [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) lets the user select both the file and the location from where to retrieve the saved ink data.

    Once a file is selected, we open an [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream) stream set to [**Read**](/uwp/api/Windows.Storage.FileAccessMode).

    We then call [**LoadAsync**](/uwp/api/windows.ui.input.inking.inkmanager.loadasync) to read, de-serialize, and load the saved ink strokes into the [**InkStrokeContainer**](/uwp/api/Windows.UI.Input.Inking.InkStrokeContainer). Loading the strokes into the **InkStrokeContainer** causes the [**InkPresenter**](/uwp/api/windows.ui.xaml.controls.inkcanvas.inkpresenter) to immediately render them to the [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas).

    > [!NOTE]
    > All existing strokes in the InkStrokeContainer are cleared before new strokes are loaded.

``` csharp
// Load ink data from a file.
private async void btnLoad_Click(object sender, RoutedEventArgs e)
{
    // Let users choose their ink file using a file picker.
    // Initialize the picker.
    Windows.Storage.Pickers.FileOpenPicker openPicker =
        new Windows.Storage.Pickers.FileOpenPicker();
    openPicker.SuggestedStartLocation =
        Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
    openPicker.FileTypeFilter.Add(".gif");
    // Show the file picker.
    Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();
    // User selects a file and picker returns a reference to the selected file.
    if (file != null)
    {
        // Open a file stream for reading.
        IRandomAccessStream stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
        // Read from file.
        using (var inputStream = stream.GetInputStreamAt(0))
        {
            await inkCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
        }
        stream.Dispose();
    }
    // User selects Cancel and picker returns null.
    else
    {
        // Operation cancelled.
    }
}
```

> [!NOTE]
> GIF is the only file format supported for saving ink data. However, the [**LoadAsync**](/uwp/api/windows.ui.input.inking.inkmanager.loadasync) method does support the following formats for backward compatibility.

| Format                    | Description |
|---------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| InkSerializedFormat       | Specifies ink that is persisted using ISF. This is the most compact persistent representation of ink. It can be embedded within a binary document format or placed directly on the Clipboard.                                                                                                                                                                                                         |
| Base64InkSerializedFormat | Specifies ink that is persisted by encoding the ISF as a base64 stream. This format is provided so ink can be encoded directly in an XML or HTML file.                                                                                                                                                                                                                                                |
| Gif                       | Specifies ink that is persisted by using a GIF file that contains ISF as metadata embedded within the file. This enables ink to be viewed in applications that are not ink-enabled and maintain its full ink fidelity when it returns to an ink-enabled application. This format is ideal when transporting ink content within an HTML file and for making it usable by ink and non-ink applications. |
| Base64Gif                 | Specifies ink that is persisted by using a base64-encoded fortified GIF. This format is provided when ink is to be encoded directly in an XML or HTML file for later conversion into an image. A possible use of this is in an XML format generated to contain all ink information and used to generate HTML through Extensible Stylesheet Language Transformations (XSLT). 

## Copy and paste ink strokes with the clipboard

Here, we demonstrate how to use the clipboard to transfer ink strokes between apps.

To support clipboard functionality, the built-in [**InkStrokeContainer**](/uwp/api/Windows.UI.Input.Inking.InkStrokeContainer) cut and copy commands require one or more ink strokes be selected.

For this example, we enable stroke selection when input is modified with a pen barrel button (or right mouse button). For a complete example of how to implement stroke selection, see Pass-through input for advanced processing in [Pen and stylus interactions](pen-and-stylus-interactions.md).

**Download this sample from [Save and load ink strokes from the clipboard](https://github.com/MicrosoftDocs/windows-topic-specific-samples/archive/uwp-ink-store-clipboard.zip)**

1.  First, we set up the UI.

    The UI includes "Cut", "Copy", "Paste", and "Clear" buttons, along with the [**InkCanvas**](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas) and a selection canvas.
```    XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <StackPanel x:Name="HeaderPanel" Orientation="Horizontal" Grid.Row="0">
            <TextBlock x:Name="tbHeader" 
                       Text="Basic ink store sample" 
                       Style="{ThemeResource HeaderTextBlockStyle}" 
                       Margin="10,0,0,0" />
            <Button x:Name="btnCut" 
                    Content="Cut" 
                    Margin="20,0,10,0"/>
            <Button x:Name="btnCopy" 
                    Content="Copy" 
                    Margin="20,0,10,0"/>
            <Button x:Name="btnPaste" 
                    Content="Paste" 
                    Margin="20,0,10,0"/>
            <Button x:Name="btnClear" 
                    Content="Clear" 
                    Margin="20,0,10,0"/>
        </StackPanel>
        <Grid x:Name="gridCanvas" Grid.Row="1">
            <!-- Canvas for displaying selection UI. -->
            <Canvas x:Name="selectionCanvas"/>
            <!-- Inking area -->
            <InkCanvas x:Name="inkCanvas"/>
        </Grid>
    </Grid>
```

2.  We then set some basic ink input behaviors.

    The [**InkPresenter**](/uwp/api/windows.ui.xaml.controls.inkcanvas.inkpresenter) is configured to interpret input data from both pen and mouse as ink strokes ([**InputDeviceTypes**](/uwp/api/windows.ui.input.inking.inkpresenter.inputdevicetypes)). Listeners for the click events on the buttons as well as pointer and stroke events for selection functionality are also declared here.

    For a complete example of how to implement stroke selection, see Pass-through input for advanced processing in [Pen and stylus interactions](pen-and-stylus-interactions.md).
```csharp
public MainPage()
    {
        this.InitializeComponent();

        // Set supported inking device types.
        inkCanvas.InkPresenter.InputDeviceTypes =
            Windows.UI.Core.CoreInputDeviceTypes.Mouse |
            Windows.UI.Core.CoreInputDeviceTypes.Pen;

        // Listen for button click to cut ink strokes.
        btnCut.Click += btnCut_Click;
        // Listen for button click to copy ink strokes.
        btnCopy.Click += btnCopy_Click;
        // Listen for button click to paste ink strokes.
        btnPaste.Click += btnPaste_Click;
        // Listen for button click to clear ink canvas.
        btnClear.Click += btnClear_Click;

        // By default, the InkPresenter processes input modified by 
        // a secondary affordance (pen barrel button, right mouse 
        // button, or similar) as ink.
        // To pass through modified input to the app for custom processing 
        // on the app UI thread instead of the background ink thread, set 
        // InputProcessingConfiguration.RightDragAction to LeaveUnprocessed.
        inkCanvas.InkPresenter.InputProcessingConfiguration.RightDragAction =
            InkInputRightDragAction.LeaveUnprocessed;

        // Listen for unprocessed pointer events from modified input.
        // The input is used to provide selection functionality.
        inkCanvas.InkPresenter.UnprocessedInput.PointerPressed +=
            UnprocessedInput_PointerPressed;
        inkCanvas.InkPresenter.UnprocessedInput.PointerMoved +=
            UnprocessedInput_PointerMoved;
        inkCanvas.InkPresenter.UnprocessedInput.PointerReleased +=
            UnprocessedInput_PointerReleased;

        // Listen for new ink or erase strokes to clean up selection UI.
        inkCanvas.InkPresenter.StrokeInput.StrokeStarted +=
            StrokeInput_StrokeStarted;
        inkCanvas.InkPresenter.StrokesErased +=
            InkPresenter_StrokesErased;
    }
```

3.  Finally, after adding stroke selection support, we implement clipboard functionality in the click event handlers of the **Cut**, **Copy**, and **Paste** buttons.

    For cut, we first call [**CopySelectedToClipboard**](/uwp/api/windows.ui.input.inking.inkstrokecontainer.copyselectedtoclipboard) on the [**InkStrokeContainer**](/uwp/api/Windows.UI.Input.Inking.InkStrokeContainer) of the [**InkPresenter**](/uwp/api/Windows.UI.Input.Inking.InkPresenter).

    We then call [**DeleteSelected**](/uwp/api/windows.ui.input.inking.inkstrokecontainer.deleteselected) to remove the strokes from the ink canvas.

    Finally, we delete all selection strokes from the selection canvas.
    
```csharp
private void btnCut_Click(object sender, RoutedEventArgs e)
    {
        inkCanvas.InkPresenter.StrokeContainer.CopySelectedToClipboard();
        inkCanvas.InkPresenter.StrokeContainer.DeleteSelected();
        ClearSelection();
    }
```
```csharp
// Clean up selection UI.
    private void ClearSelection()
    {
        var strokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
        foreach (var stroke in strokes)
        {
            stroke.Selected = false;
        }
        ClearDrawnBoundingRect();
    }

    private void ClearDrawnBoundingRect()
    {
        if (selectionCanvas.Children.Any())
        {
            selectionCanvas.Children.Clear();
            boundingRect = Rect.Empty;
        }
    }
```

For copy, we simply call [**CopySelectedToClipboard**](/uwp/api/windows.ui.input.inking.inkstrokecontainer.copyselectedtoclipboard) on the [**InkStrokeContainer**](/uwp/api/Windows.UI.Input.Inking.InkStrokeContainer) of the [**InkPresenter**](/uwp/api/Windows.UI.Input.Inking.InkPresenter).


```csharp
private void btnCopy_Click(object sender, RoutedEventArgs e)
    {
        inkCanvas.InkPresenter.StrokeContainer.CopySelectedToClipboard();
    }
```

For paste, we call [**CanPasteFromClipboard**](/uwp/api/windows.ui.input.inking.inkstrokecontainer.canpastefromclipboard) to ensure that the content on the clipboard can be pasted to the ink canvas.

If so, we call [**PasteFromClipboard**](/uwp/api/windows.ui.input.inking.inkstrokecontainer.pastefromclipboard) to insert the clipboard ink strokes into the [**InkStrokeContainer**](/uwp/api/Windows.UI.Input.Inking.InkStrokeContainer) of the [**InkPresenter**](/uwp/api/Windows.UI.Input.Inking.InkPresenter), which then renders the strokes to the ink canvas.

```csharp
private void btnPaste_Click(object sender, RoutedEventArgs e)
    {
        if (inkCanvas.InkPresenter.StrokeContainer.CanPasteFromClipboard())
        {
            inkCanvas.InkPresenter.StrokeContainer.PasteFromClipboard(
                new Point(0, 0));
        }
        else
        {
            // Cannot paste from clipboard.
        }
    }
```

## Related articles

* [Pen and stylus interactions](pen-and-stylus-interactions.md)

**Topic samples**
* [Save and load ink strokes from an Ink Serialized Format (ISF) file](https://github.com/MicrosoftDocs/windows-topic-specific-samples/archive/uwp-ink-store.zip)
* [Save and load ink strokes from the clipboard](https://github.com/MicrosoftDocs/windows-topic-specific-samples/archive/uwp-ink-store-clipboard.zip)

**Other samples**
* [Simple ink sample (C#/C++)](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SimpleInk)
* [Complex ink sample (C++)](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ComplexInk)
* [Ink sample (JavaScript)](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BJavaScript%5D-Windows%208%20app%20samples/JavaScript/Windows%208%20app%20samples/Input%20Ink%20sample%20(Windows%208))
* [Get Started Tutorial: Support ink in your Windows app](https://github.com/Microsoft/Windows-tutorials-inputs-and-devices/tree/master/GettingStarted-Ink)
* [Coloring book sample](https://github.com/Microsoft/Windows-appsample-coloringbook)
* [Family notes sample](https://github.com/Microsoft/Windows-appsample-familynotes)