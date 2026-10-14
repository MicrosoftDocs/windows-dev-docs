---
title: Inking controls (InkCanvas and InkToolbar) for WinUI apps
description: Learn how to add experimental WinUI ink input with InkCanvas and InkToolbar, including input devices, drawing attributes, saving, and toolbars.
author: GrantMeStrength
ms.author: jken
ms.date: 10/13/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Inking controls for WinUI apps

> [!IMPORTANT]
> `InkCanvas`, `InkToolbar`, and `InkPresenter` are experimental APIs. Inking support was added in [Windows App SDK 2.4.1-experimental](../../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-24-experimental-241-experimental). This article describes [Windows App SDK 2.5.4-experimental](../../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-25-experimental-254-experimental), which changed how you connect an `InkToolbar`. These APIs aren't available in the stable channel. They might change or be removed in a future release. Don't use them in production apps.

Use the experimental [`InkCanvas`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkcanvas?view=windows-app-sdk-2.0-experimental&preserve-view=true) control to let users draw ink with a pen, mouse, or touch. Add an [`InkToolbar`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar?view=windows-app-sdk-2.0-experimental&preserve-view=true) to give users a ballpoint pen, pencil, highlighter, and eraser, with color and size options. Each canvas has an [`InkPresenter`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter?view=windows-app-sdk-2.0-experimental&preserve-view=true) that collects and renders ink input. The presenter's [`InkStrokeContainer`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer?view=windows-app-sdk-2.0-experimental&preserve-view=true) stores the strokes, so you can save, load, copy, and create ink in code.

To try the controls, install the Windows App SDK 2.5.4-experimental package and matching runtime. For download links and setup information, see [Latest Windows App SDK downloads](../../../windows-app-sdk/downloads.md#experimental-release).

## Is this the right control?

Use an ink canvas when users need to:

- Take handwritten notes or annotate content.
- Sketch, draw diagrams, or sign their name.
- Create freehand content that your app saves and lets them edit later.

The experimental inking APIs don't include handwriting or shape recognition. To collect text, use a [text box](text-box.md).

If you need inking in a production app, see [Known control gaps](../../../windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported.md#known-control-gaps) for the status of these controls in the stable channel and for alternatives.

## Understand the inking model

The inking APIs work together:

| API | Role |
|---|---|
| `InkCanvas` | The XAML element that displays ink and receives ink input. Its `InkPresenter` property returns the presenter for the canvas. |
| `InkPresenter` | Collects ink input, renders strokes, and holds the input settings and default drawing attributes. You can't create an `InkPresenter` yourself. Get it from `InkCanvas.InkPresenter`. |
| `InkStrokeContainer` | Stores strokes. Use it to save, load, select, copy, and paste ink. Get it from `InkPresenter.StrokeContainer`. |
| `InkToolbar` | An optional toolbar that lets users choose a pen, pencil, highlighter, or eraser, and change ink color and size. |

`InkCanvas` doesn't have a `Background` property. To give the drawing area a background and a border, place the canvas inside a container such as a `Border`, and set the size on the container. The default ink color is black, so the examples in this article use a white background.

Several of these types have the same names as types in the Windows Runtime [Windows.UI.Input.Inking](/uwp/api/windows.ui.input.inking) namespace, such as `InkPresenter`, `InkStrokeContainer`, and `InkHighContrastAdjustment`. The inking controls use the `Microsoft.UI.Xaml.Controls` versions. Strokes and drawing attributes still use Windows Runtime types, such as `InkStroke`, `InkDrawingAttributes`, and `InkStrokeBuilder`. If you import both namespaces and use an ambiguous name, the C# compiler reports error CS0104.

The examples in this article import `Microsoft.UI.Xaml.Controls` and use alias directives for the `Windows.UI.Input.Inking` types that they need:

```csharp
using Microsoft.UI.Xaml.Controls;
using InkDrawingAttributes = Windows.UI.Input.Inking.InkDrawingAttributes;
using InkStroke = Windows.UI.Input.Inking.InkStroke;
```

You can also import both namespaces and use fully qualified names for the ambiguous types, as the WinUI 3 Gallery sample does.

`InkPresenter` also provides lower-level APIs that this article doesn't cover:

- [`StrokeInput`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.strokeinput?view=windows-app-sdk-2.0-experimental&preserve-view=true) raises events when a stroke starts, continues, ends, or is canceled.
- [`UnprocessedInput`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.unprocessedinput?view=windows-app-sdk-2.0-experimental&preserve-view=true) raises pointer events for input that the presenter doesn't process as ink.
- [`InputConfiguration`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.inputconfiguration?view=windows-app-sdk-2.0-experimental&preserve-view=true) sets whether the presenter processes input from a pen's eraser and primary barrel button.
- [`InputProcessingConfiguration`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.inputprocessingconfiguration?view=windows-app-sdk-2.0-experimental&preserve-view=true) sets whether input draws ink, erases ink, or isn't processed, and how the presenter handles right-drag input.
- [`ActivateCustomDrying`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.activatecustomdrying?view=windows-app-sdk-2.0-experimental&preserve-view=true) returns an [`InkSynchronizer`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inksynchronizer?view=windows-app-sdk-2.0-experimental&preserve-view=true) for apps that render committed ink themselves.

## Set up your project

The inking APIs are marked experimental. When you use them in a C# project, the C# compiler reports warning CS8305 and the XAML compiler reports warning WMC1501. If your project treats warnings as errors, these warnings stop the build.

After you decide to use the experimental APIs, you can suppress these warnings. Add the following properties to your C# project file (.csproj):

```xml
<PropertyGroup>
  <NoWarn>$(NoWarn);CS8305</NoWarn>
  <MSBuildWarningsAsMessages>$(MSBuildWarningsAsMessages);WMC1501</MSBuildWarningsAsMessages>
</PropertyGroup>
```

The examples in this article use C#. Each complete example shows the XAML for a page and the page's code-behind class, `MainPage`.

## Create an ink canvas

> [!div class="checklist"]
>
> - **Important APIs**: [InkCanvas class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkcanvas?view=windows-app-sdk-2.0-experimental&preserve-view=true), [InkPresenter class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter?view=windows-app-sdk-2.0-experimental&preserve-view=true), [InkStrokeContainer class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer?view=windows-app-sdk-2.0-experimental&preserve-view=true), [InkToolbar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar?view=windows-app-sdk-2.0-experimental&preserve-view=true), [Windows.UI.Input.Inking namespace](/uwp/api/windows.ui.input.inking)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the InkCanvas in action](winui3gallery://item/InkCanvas)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

The following example places a canvas inside a white `Border`. The code-behind sets the input device types so that users can draw with a pen, mouse, or touch.

```xaml
<Border
    Height="300"
    Background="White"
    BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
    BorderThickness="1">
    <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
</Border>
```

```csharp
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Core;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        // Mouse input doesn't draw ink by default. Choose every device type that can draw ink.
        inkCanvas.InkPresenter.InputDeviceTypes =
            CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch;
    }
}
```

To keep them short, some later examples don't repeat this code. Set `InputDeviceTypes` in every page where users draw with a mouse.

## Connect an ink toolbar

Place an `InkToolbar` near the canvas, and set its [`TargetInkCanvas`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.targetinkcanvas?view=windows-app-sdk-2.0-experimental&preserve-view=true) property to connect the two:

```xaml
<StackPanel Spacing="8">
    <InkToolbar
        x:Name="inkToolbar"
        AutomationProperties.Name="Drawing tools"
        TargetInkCanvas="{x:Bind inkCanvas}" />
    <Border
        Height="300"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

Starting with Windows App SDK 2.5.4-experimental, you can instead connect a toolbar directly to an `InkPresenter` by setting the [`TargetInkPresenter`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.targetinkpresenter?view=windows-app-sdk-2.0-experimental&preserve-view=true) property. Set it in code:

```csharp
inkToolbar.TargetInkPresenter = inkCanvas.InkPresenter;
```

You can also set it in XAML: `TargetInkPresenter="{x:Bind inkCanvas.InkPresenter}"`. Set only one of the two target properties.

By default, the toolbar shows ballpoint pen, pencil, highlighter, eraser, and stencil (ruler) buttons. To change which buttons appear, see [Customize the ink toolbar](#customize-the-ink-toolbar).

## Choose input devices

The [`InputDeviceTypes`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.inputdevicetypes?view=windows-app-sdk-2.0-experimental&preserve-view=true) property uses the [`CoreInputDeviceTypes`](/uwp/api/windows.ui.core.coreinputdevicetypes) flags enumeration from the `Windows.UI.Core` namespace. Mouse input doesn't draw ink by default. Combine values to choose which devices draw ink:

```csharp
// Add mouse input and keep the other device types.
inkCanvas.InkPresenter.InputDeviceTypes |= CoreInputDeviceTypes.Mouse;

// Stop accepting ink from touch input.
inkCanvas.InkPresenter.InputDeviceTypes &= ~CoreInputDeviceTypes.Touch;

// Check whether pen input is enabled.
bool isPenEnabled = inkCanvas.InkPresenter.InputDeviceTypes.HasFlag(CoreInputDeviceTypes.Pen);
```

To stop collecting ink from every device, for example to show a drawing that users can't change, set [`IsInputEnabled`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.isinputenabled?view=windows-app-sdk-2.0-experimental&preserve-view=true) to `false`:

```csharp
inkCanvas.InkPresenter.IsInputEnabled = false;
```

To let users turn an input device on or off from the toolbar, see [Add a custom toggle button](#add-a-custom-toggle-button).

## Set drawing attributes

Drawing attributes, such as color, size, and pen tip shape, control how strokes look. They use the Windows Runtime [`InkDrawingAttributes`](/uwp/api/windows.ui.input.inking.inkdrawingattributes) class. To change the attributes for new strokes, call [`CopyDefaultDrawingAttributes`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.copydefaultdrawingattributes?view=windows-app-sdk-2.0-experimental&preserve-view=true), change the copy, and pass it to [`UpdateDefaultDrawingAttributes`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.updatedefaultdrawingattributes?view=windows-app-sdk-2.0-experimental&preserve-view=true):

```csharp
InkDrawingAttributes attributes = inkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
attributes.Color = Colors.DarkBlue;
attributes.Size = new Size(4, 4);
inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(attributes);
```

`Colors` is in the `Microsoft.UI` namespace, and `Size` is in the `Windows.Foundation` namespace. The new attributes apply to strokes that users draw after the call. Existing strokes keep their attributes.

When you connect an `InkToolbar`, the active tool and its color and size settings determine the drawing attributes. To read the current attributes, use the toolbar's [`InkDrawingAttributes`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.inkdrawingattributes?view=windows-app-sdk-2.0-experimental&preserve-view=true) property.

### Create pen presets

If you don't use a toolbar, you can offer your own pen presets. The following example applies ballpoint, pencil, highlighter, or calligraphy attributes when the user selects a preset or changes the size.

```xaml
<StackPanel Spacing="12">
    <RadioButtons
        x:Name="presetButtons"
        Header="Pen preset"
        MaxColumns="4"
        SelectedIndex="0"
        SelectionChanged="PresetButtons_SelectionChanged">
        <x:String>Ballpoint</x:String>
        <x:String>Pencil</x:String>
        <x:String>Highlighter</x:String>
        <x:String>Calligraphy</x:String>
    </RadioButtons>
    <Slider
        x:Name="sizeSlider"
        Width="240"
        HorizontalAlignment="Left"
        Header="Size"
        Maximum="16"
        Minimum="1"
        Value="4"
        ValueChanged="SizeSlider_ValueChanged" />
    <Border
        Width="480"
        Height="280"
        HorizontalAlignment="Left"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

```csharp
using System;
using System.Numerics;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.Foundation;
using Windows.UI.Core;
using InkDrawingAttributes = Windows.UI.Input.Inking.InkDrawingAttributes;
using PenTipShape = Windows.UI.Input.Inking.PenTipShape;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        inkCanvas.InkPresenter.InputDeviceTypes =
            CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch;
        ApplyPreset();
    }

    private void PresetButtons_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyPreset();

    private void SizeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) => ApplyPreset();

    private void ApplyPreset()
    {
        // SelectionChanged and ValueChanged can occur while the page is still loading.
        if (inkCanvas is null || presetButtons is null || sizeSlider is null)
        {
            return;
        }

        double size = sizeSlider.Value;
        InkDrawingAttributes attributes;

        switch (presetButtons.SelectedItem as string)
        {
            case "Pencil":
                // CreateForPencil returns attributes for pencil-style strokes.
                attributes = InkDrawingAttributes.CreateForPencil();
                attributes.Color = Colors.DimGray;
                attributes.Size = new Size(size, size);
                break;

            case "Highlighter":
                attributes = new InkDrawingAttributes
                {
                    Color = Colors.Yellow,
                    DrawAsHighlighter = true,
                    PenTip = PenTipShape.Rectangle,
                    Size = new Size(size, size * 3),
                };
                break;

            case "Calligraphy":
                attributes = new InkDrawingAttributes
                {
                    Color = Colors.DarkBlue,
                    PenTip = PenTipShape.Rectangle,
                    Size = new Size(Math.Max(1, size / 2), size * 2),
                    // Rotate the nib 45 degrees so the stroke width changes with the stroke direction.
                    PenTipTransform = Matrix3x2.CreateRotation(MathF.PI / 4),
                };
                break;

            default:
                attributes = new InkDrawingAttributes
                {
                    Color = Colors.Black,
                    PenTip = PenTipShape.Circle,
                    Size = new Size(size, size),
                };
                break;
        }

        // The new attributes apply to strokes that the user draws after this call.
        inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(attributes);
    }
}
```

:::image type="content" source="images/inking/drawing-attributes.png" alt-text="Pen preset options for Ballpoint, Pencil, Highlighter, and Calligraphy, with Calligraphy selected, above a Size slider and a white canvas. The canvas shows looped strokes in black ballpoint ink, gray pencil ink, yellow highlighter ink, and dark blue calligraphy ink.":::

- [`InkDrawingAttributes.CreateForPencil`](/uwp/api/windows.ui.input.inking.inkdrawingattributes.createforpencil) returns attributes for pencil strokes. The `Kind` property is read-only, so use this method to create pencil attributes.
- `DrawAsHighlighter` renders the stroke as a highlighter overlay.
- `PenTip` sets the shape of the pen tip, and `PenTipTransform` rotates or scales it. A rotated rectangular tip produces calligraphy-style strokes.

## Respond to ink changes

The presenter raises [`StrokesCollected`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.strokescollected?view=windows-app-sdk-2.0-experimental&preserve-view=true) after a user draws strokes and [`StrokesErased`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.strokeserased?view=windows-app-sdk-2.0-experimental&preserve-view=true) after a user erases them. The `Strokes` property of each event's arguments contains the affected strokes. The following code keeps a stroke count up to date in a `TextBlock` named `strokeCountText`:

```csharp
inkCanvas.InkPresenter.StrokesCollected += (sender, args) => UpdateStrokeCount();
inkCanvas.InkPresenter.StrokesErased += (sender, args) => UpdateStrokeCount();
```

```csharp
private void UpdateStrokeCount()
{
    int count = inkCanvas.InkPresenter.StrokeContainer.GetStrokes().Count;
    strokeCountText.Text = $"{count} strokes";
}
```

These events occur when users draw or erase ink. When you change strokes in code, for example with `Clear`, `LoadAsync`, or `AddStroke`, update your UI after the call.

## Save and load ink

`InkStrokeContainer` saves ink in Ink Serialized Format (ISF). To choose how the data is stored, pass an [`InkPersistenceFormat`](/uwp/api/windows.ui.input.inking.inkpersistenceformat) value to [`SaveAsync`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer.saveasync?view=windows-app-sdk-2.0-experimental&preserve-view=true):

| Format | Output |
|---|---|
| `GifWithEmbeddedIsf` | A GIF image that contains the ink data. Image viewers can display the file, and your app can load the ink again for editing. |
| `Isf` | Ink data only, without an image. This is the most compact format. |

Add the following methods to the code-behind of a page that contains an `InkCanvas` named `inkCanvas`. `SaveInkAsync` writes the ink to a file, and `LoadInkAsync` replaces the ink on the canvas with the ink from a file. [`LoadAsync`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer.loadasync?view=windows-app-sdk-2.0-experimental&preserve-view=true) removes the existing strokes before it adds the loaded strokes, so you don't need to call `Clear` first.

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Streams;
using InkPersistenceFormat = Windows.UI.Input.Inking.InkPersistenceFormat;

public sealed partial class MainPage : Page
{
    private async Task SaveInkAsync(string path)
    {
        InkStrokeContainer container = inkCanvas.InkPresenter.StrokeContainer;

        // File.Create replaces any existing file, so the file contains only the new ink.
        using FileStream fileStream = File.Create(path);
        using IOutputStream output = fileStream.AsOutputStream();

        // GifWithEmbeddedIsf writes a GIF image that also contains the editable ink data.
        await container.SaveAsync(output, InkPersistenceFormat.GifWithEmbeddedIsf);
        await output.FlushAsync();
    }

    private async Task LoadInkAsync(string path)
    {
        InkStrokeContainer container = inkCanvas.InkPresenter.StrokeContainer;

        using FileStream fileStream = File.OpenRead(path);
        using IInputStream input = fileStream.AsInputStream();

        // LoadAsync removes the current strokes before it adds the loaded strokes.
        await container.LoadAsync(input);
    }
}
```

`AsOutputStream` and `AsInputStream` convert .NET streams to the Windows Runtime stream types that `SaveAsync` and `LoadAsync` use. To await these Windows Runtime methods, the file needs the `using System;` directive. Without it, the compiler reports error CS4036.

To let users choose a file, use the Windows App SDK file pickers, which return the path of the selected file. For more information, see [Save a file with a Windows App SDK picker](../../files/pickers-save-file.md) and [Open files and folders with Windows App SDK pickers](../../files/using-file-folder-pickers.md).

To keep ink in memory, for example to restore it later in the same session, save it to an `InMemoryRandomAccessStream`. Pass the stream's `GetOutputStreamAt(0)` result to `SaveAsync`, and pass its `GetInputStreamAt(0)` result to `LoadAsync`. The WinUI 3 Gallery sample uses this approach.

## Copy and paste ink

`InkStrokeContainer` can copy strokes to the clipboard and paste ink from it. [`CopySelectedToClipboard`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer.copyselectedtoclipboard?view=windows-app-sdk-2.0-experimental&preserve-view=true) copies only selected strokes, so the following example selects every stroke on the left canvas before it copies them. [`CanPasteFromClipboard`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer.canpastefromclipboard?view=windows-app-sdk-2.0-experimental&preserve-view=true) checks whether the clipboard contains ink. [`PasteFromClipboard`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer.pastefromclipboard?view=windows-app-sdk-2.0-experimental&preserve-view=true) adds the ink at the position that you pass, and returns the bounding rectangle of the area that changed.

```xaml
<StackPanel Spacing="12">
    <StackPanel Orientation="Horizontal" Spacing="8">
        <Button Click="CopyButton_Click" Content="Copy left canvas" />
        <Button
            x:Name="pasteButton"
            Click="PasteButton_Click"
            Content="Paste into right canvas"
            IsEnabled="False" />
    </StackPanel>
    <StackPanel Orientation="Horizontal" Spacing="12">
        <Border
            Width="300"
            Height="220"
            Background="White"
            BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
            BorderThickness="1">
            <InkCanvas x:Name="sourceCanvas" AutomationProperties.Name="Source drawing surface" />
        </Border>
        <Border
            Width="300"
            Height="220"
            Background="White"
            BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
            BorderThickness="1">
            <InkCanvas x:Name="targetCanvas" AutomationProperties.Name="Target drawing surface" />
        </Border>
    </StackPanel>
    <TextBlock x:Name="statusText" Text="Draw on the left canvas, then copy it." TextWrapping="Wrap" />
</StackPanel>
```

```csharp
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.Core;
using InkStroke = Windows.UI.Input.Inking.InkStroke;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        foreach (InkCanvas canvas in new[] { sourceCanvas, targetCanvas })
        {
            canvas.InkPresenter.InputDeviceTypes =
                CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch;
        }
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        InkStrokeContainer source = sourceCanvas.InkPresenter.StrokeContainer;
        IReadOnlyList<InkStroke> strokes = source.GetStrokes();
        if (strokes.Count == 0)
        {
            statusText.Text = "Draw on the left canvas first.";
            return;
        }

        // CopySelectedToClipboard copies only selected strokes, so select all strokes first.
        foreach (InkStroke stroke in strokes)
        {
            stroke.Selected = true;
        }

        source.CopySelectedToClipboard();

        foreach (InkStroke stroke in strokes)
        {
            stroke.Selected = false;
        }

        pasteButton.IsEnabled = targetCanvas.InkPresenter.StrokeContainer.CanPasteFromClipboard();
        statusText.Text = $"Copied {strokes.Count} stroke(s) to the clipboard.";
    }

    private void PasteButton_Click(object sender, RoutedEventArgs e)
    {
        InkStrokeContainer target = targetCanvas.InkPresenter.StrokeContainer;
        if (!target.CanPasteFromClipboard())
        {
            statusText.Text = "The clipboard doesn't contain ink.";
            return;
        }

        // Place the pasted ink near the upper-left corner of the target canvas.
        Rect pasted = target.PasteFromClipboard(new Point(16, 16));
        statusText.Text = $"Pasted ink into a {pasted.Width:0} x {pasted.Height:0} area.";
    }
}
```

:::image type="content" source="images/inking/clipboard.png" alt-text="Copy left canvas and Paste into right canvas buttons above two white canvases that show the same looped, spiral, and wavy strokes. Text below reads Pasted ink into a 255 x 69 area.":::

To work with some of the strokes, set the `Selected` property of each stroke that you want, or call `SelectWithLine` or `SelectWithPolyLine`. To delete or move the selected strokes, call `DeleteSelected` or `MoveSelected`.

## Create strokes in code

Use the Windows Runtime [`InkStrokeBuilder`](/uwp/api/windows.ui.input.inking.inkstrokebuilder) class to create strokes from points, and add them to the canvas with [`AddStroke`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkstrokecontainer.addstroke?view=windows-app-sdk-2.0-experimental&preserve-view=true). The following example adds a star and a signature line.

```xaml
<StackPanel Spacing="12">
    <StackPanel Orientation="Horizontal" Spacing="8">
        <Button Click="AddStarButton_Click" Content="Add star" />
        <Button Click="AddSignatureLineButton_Click" Content="Add signature line" />
        <Button Click="ClearButton_Click" Content="Clear" />
    </StackPanel>
    <Border
        Width="480"
        Height="280"
        HorizontalAlignment="Left"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;
using InkDrawingAttributes = Windows.UI.Input.Inking.InkDrawingAttributes;
using InkPoint = Windows.UI.Input.Inking.InkPoint;
using InkStroke = Windows.UI.Input.Inking.InkStroke;
using InkStrokeBuilder = Windows.UI.Input.Inking.InkStrokeBuilder;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void AddStarButton_Click(object sender, RoutedEventArgs e)
    {
        // Alternate between outer and inner points to draw a five-pointed star.
        Point center = new(inkCanvas.ActualWidth / 2, 90);
        List<Point> points = new();
        for (int i = 0; i <= 10; i++)
        {
            double radius = i % 2 == 0 ? 70 : 28;
            double angle = -Math.PI / 2 + i * Math.PI / 5;
            points.Add(new Point(center.X + radius * Math.Cos(angle), center.Y + radius * Math.Sin(angle)));
        }

        AddStroke(points, Colors.Goldenrod, 4);
    }

    private void AddSignatureLineButton_Click(object sender, RoutedEventArgs e)
    {
        double left = 40;
        double right = inkCanvas.ActualWidth - 40;
        double baseline = inkCanvas.ActualHeight - 40;

        AddStroke(new[] { new Point(left, baseline), new Point(right, baseline) }, Colors.Gray, 2);
        AddStroke(new[] { new Point(left, baseline - 28), new Point(left + 18, baseline - 10) }, Colors.Gray, 2);
        AddStroke(new[] { new Point(left + 18, baseline - 28), new Point(left, baseline - 10) }, Colors.Gray, 2);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        inkCanvas.InkPresenter.StrokeContainer.Clear();
    }

    private void AddStroke(IEnumerable<Point> points, Color color, double size)
    {
        InkStrokeBuilder builder = new();
        builder.SetDefaultDrawingAttributes(new InkDrawingAttributes
        {
            Color = color,
            Size = new Size(size, size),
            // Keep straight segments and sharp corners instead of smoothing them into curves.
            FitToCurve = false,
        });

        // Give every point the same pressure so that the stroke has a uniform width.
        IEnumerable<InkPoint> inkPoints = points.Select(point => new InkPoint(point, 0.5f));
        InkStroke stroke = builder.CreateStrokeFromInkPoints(inkPoints, Matrix3x2.Identity);
        inkCanvas.InkPresenter.StrokeContainer.AddStroke(stroke);
    }
}
```

:::image type="content" source="images/inking/stroke-builder.png" alt-text="Add star, Add signature line, and Clear buttons above a white canvas that shows a gold five-pointed star and a gray signature line with an X at its left end.":::

- [`CreateStrokeFromInkPoints`](/uwp/api/windows.ui.input.inking.inkstrokebuilder.createstrokefrominkpoints) creates a stroke from [`InkPoint`](/uwp/api/windows.ui.input.inking.inkpoint) values. Each point combines a position on the canvas with a pressure value.
- The Windows Runtime documentation recommends `CreateStrokeFromInkPoints` instead of [`CreateStroke`](/uwp/api/windows.ui.input.inking.inkstrokebuilder.createstroke) for strokes that you add to an ink presenter.
- When `FitToCurve` is `false`, the stroke uses straight segments between points instead of curves.

A stroke can belong to only one container. To copy strokes to another canvas, add clones of the strokes:

```csharp
private void CopyStrokesTo(InkCanvas target)
{
    foreach (InkStroke stroke in inkCanvas.InkPresenter.StrokeContainer.GetStrokes())
    {
        target.InkPresenter.StrokeContainer.AddStroke(stroke.Clone());
    }
}
```

## Customize the ink toolbar

### Choose the initial controls

The [`InitialControls`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.initialcontrols?view=windows-app-sdk-2.0-experimental&preserve-view=true) property determines which built-in buttons the toolbar creates:

| Value | Built-in buttons |
|---|---|
| `All` (default) | Ballpoint pen, pencil, highlighter, eraser, and stencil (ruler) |
| `PensOnly` | Ballpoint pen, pencil, and highlighter |
| `AllExceptPens` | Eraser and stencil (ruler) |
| `None` | None. Declare the buttons that you want as children of the toolbar. |

:::image type="content" source="images/inking/initial-controls.png" alt-text="Four ink toolbars, each above a canvas with sample strokes. The toolbar labeled InitialControls All (default) shows ballpoint pen, pencil, highlighter, eraser, and ruler buttons. PensOnly shows the three pen buttons. AllExceptPens shows the eraser and ruler buttons. None plus a pencil shows only a pencil button, and its canvas shows gray pencil strokes.":::

The following example declares the same four buttons as the WinUI 3 Gallery sample, gives each one an accessible name, and selects the ballpoint pen when the toolbar loads:

```xaml
<StackPanel Spacing="8">
    <InkToolbar
        x:Name="inkToolbar"
        AutomationProperties.Name="Drawing tools"
        InitialControls="None"
        TargetInkCanvas="{x:Bind inkCanvas}">
        <InkToolbarBallpointPenButton x:Name="ballpointButton" AutomationProperties.Name="Ballpoint pen" />
        <InkToolbarPencilButton AutomationProperties.Name="Pencil" />
        <InkToolbarHighlighterButton AutomationProperties.Name="Highlighter" />
        <InkToolbarEraserButton AutomationProperties.Name="Eraser" />
    </InkToolbar>
    <Border
        Height="300"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

```csharp
using Microsoft.UI.Xaml.Controls;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        // Select a starting tool after the toolbar loads.
        inkToolbar.Loaded += (sender, args) =>
        {
            if (inkToolbar.ActiveTool is null)
            {
                inkToolbar.ActiveTool = ballpointButton;
            }
        };
    }
}
```

To show a command that erases all ink, set the eraser button's [`IsClearAllVisible`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbareraserbutton.isclearallvisible?view=windows-app-sdk-2.0-experimental&preserve-view=true) property to `true`. The toolbar raises the [`EraseAllClicked`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.eraseallclicked?view=windows-app-sdk-2.0-experimental&preserve-view=true) event when the user selects the command.

### Customize a pen's colors and widths

Each pen button has a [`Palette`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbarpenbutton.palette?view=windows-app-sdk-2.0-experimental&preserve-view=true) of brushes and stroke width settings. Set `Palette` in code. For details, see [Known limitations](#known-limitations). The following example replaces a ballpoint pen's palette with four brand colors, limits the stroke width, and shows the attributes of the active pen.

```xaml
<StackPanel Spacing="12">
    <InkToolbar
        x:Name="inkToolbar"
        AutomationProperties.Name="Drawing tools"
        InitialControls="AllExceptPens"
        TargetInkCanvas="{x:Bind inkCanvas}">
        <InkToolbarBallpointPenButton
            x:Name="brandPen"
            AutomationProperties.Name="Brand pen"
            MaxStrokeWidth="16"
            MinStrokeWidth="2"
            SelectedStrokeWidth="6" />
    </InkToolbar>
    <RadioButtons
        x:Name="colorButtons"
        Header="Pen color"
        MaxColumns="4"
        SelectedIndex="0"
        SelectionChanged="ColorButtons_SelectionChanged">
        <x:String>Blue</x:String>
        <x:String>Green</x:String>
        <x:String>Orange</x:String>
        <x:String>Purple</x:String>
    </RadioButtons>
    <TextBlock x:Name="penInfo" />
    <Border
        Width="480"
        Height="240"
        HorizontalAlignment="Left"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

```csharp
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

public sealed partial class MainPage : Page
{
    private static readonly Color[] BrandColors =
    {
        Color.FromArgb(0xFF, 0x00, 0x78, 0xD4), // Blue
        Color.FromArgb(0xFF, 0x10, 0x7C, 0x10), // Green
        Color.FromArgb(0xFF, 0xD8, 0x3B, 0x01), // Orange
        Color.FromArgb(0xFF, 0x5C, 0x2D, 0x91), // Purple
    };

    public MainPage()
    {
        InitializeComponent();

        // Replace the pen's default palette and select the first color.
        brandPen.Palette = BrandColors.Select(color => (Brush)new SolidColorBrush(color)).ToList();
        brandPen.SelectedBrushIndex = 0;

        inkToolbar.InkDrawingAttributesChanged += (sender, args) => ShowPenInfo();

        // The toolbar sets up its buttons when it loads, so select the starting tool then.
        inkToolbar.Loaded += (sender, args) =>
        {
            inkToolbar.ActiveTool = brandPen;
            ShowPenInfo();
        };
    }

    private void ColorButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // SelectionChanged can occur while the page is still loading.
        if (brandPen is null || colorButtons is null || penInfo is null || colorButtons.SelectedIndex < 0)
        {
            return;
        }

        // SelectedBrushIndex selects an entry in Palette, like choosing a color in the pen flyout.
        brandPen.SelectedBrushIndex = colorButtons.SelectedIndex;
        inkToolbar.ActiveTool = brandPen;
        ShowPenInfo();
    }

    private void ShowPenInfo()
    {
        // InkDrawingAttributes describes the ink that the active tool draws.
        if (inkToolbar.InkDrawingAttributes is { } attributes)
        {
            penInfo.Text = $"Color {attributes.Color}, width {attributes.Size.Width:0} (range {brandPen.MinStrokeWidth:0}-{brandPen.MaxStrokeWidth:0})";
        }
    }
}
```

:::image type="content" source="images/inking/custom-pen-palette.png" alt-text="An ink toolbar with ballpoint pen, eraser, and ruler buttons above Blue, Green, Orange, and Purple pen color options. Text reads Color #FF0078D4, width 6 (range 2-16), and the canvas shows looped strokes in blue, green, orange, and purple.":::

`SelectedBrushIndex` selects a color from `Palette`. `SelectedStrokeWidth`, `MinStrokeWidth`, and `MaxStrokeWidth` set the current stroke width and the range that users can choose from. The toolbar raises [`InkDrawingAttributesChanged`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.inkdrawingattributeschanged?view=windows-app-sdk-2.0-experimental&preserve-view=true) when the attributes of the active tool change.

### Select the active tool

Set [`ActiveTool`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.activetool?view=windows-app-sdk-2.0-experimental&preserve-view=true) to select a tool, and handle [`ActiveToolChanged`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.activetoolchanged?view=windows-app-sdk-2.0-experimental&preserve-view=true) to respond when the user selects another one. [`GetToolButton`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.gettoolbutton?view=windows-app-sdk-2.0-experimental&preserve-view=true) returns a built-in tool button of the kind that you pass. The built-in buttons might not exist until the toolbar loads, so select a starting tool in the toolbar's `Loaded` event handler.

```xaml
<StackPanel Spacing="12">
    <InkToolbar
        x:Name="inkToolbar"
        AutomationProperties.Name="Drawing tools"
        TargetInkCanvas="{x:Bind inkCanvas}" />
    <TextBlock x:Name="toolInfo" />
    <Border
        Width="480"
        Height="280"
        HorizontalAlignment="Left"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

```csharp
using Microsoft.UI.Xaml.Controls;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        inkToolbar.ActiveToolChanged += (sender, args) => ShowToolInfo();
        inkToolbar.InkDrawingAttributesChanged += (sender, args) => ShowToolInfo();

        // The toolbar creates its built-in buttons when it loads, so select the starting tool then.
        inkToolbar.Loaded += (sender, args) =>
        {
            inkToolbar.ActiveTool = inkToolbar.GetToolButton(InkToolbarTool.Pencil);
            ShowToolInfo();
        };
    }

    private void ShowToolInfo()
    {
        if (inkToolbar.ActiveTool is not { } tool)
        {
            toolInfo.Text = "No active tool";
            return;
        }

        toolInfo.Text = inkToolbar.InkDrawingAttributes is { } attributes
            ? $"{tool.ToolKind}: {attributes.Kind} ink, color {attributes.Color}, size {attributes.Size.Width:0}"
            : $"{tool.ToolKind}";
    }
}
```

:::image type="content" source="images/inking/active-tool-changed.png" alt-text="An ink toolbar with the pencil button selected. Text below it reads Pencil: Pencil ink, color #FF000000, size 4, and the canvas shows gray pencil strokes.":::

### Change the orientation

To show the toolbar vertically, for example next to the canvas, set [`Orientation`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.orientation?view=windows-app-sdk-2.0-experimental&preserve-view=true) to `Vertical`:

```xaml
<Grid HorizontalAlignment="Left" ColumnSpacing="8">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="Auto" />
    </Grid.ColumnDefinitions>
    <InkToolbar
        VerticalAlignment="Top"
        AutomationProperties.Name="Drawing tools"
        Orientation="Vertical"
        TargetInkCanvas="{x:Bind inkCanvas}" />
    <Border
        Grid.Column="1"
        Width="440"
        Height="320"
        VerticalAlignment="Top"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</Grid>
```

:::image type="content" source="images/inking/vertical-toolbar.png" alt-text="A vertical ink toolbar with ballpoint pen, pencil, highlighter, eraser, and ruler buttons to the left of a canvas with black ink strokes.":::

The [`ButtonFlyoutPlacement`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbar.buttonflyoutplacement?view=windows-app-sdk-2.0-experimental&preserve-view=true) property sets where button flyouts open relative to the toolbar. The default value, `Auto`, lets the toolbar choose the position.

### Add a custom toggle button

Use an [`InkToolbarCustomToggleButton`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbarcustomtogglebutton?view=windows-app-sdk-2.0-experimental&preserve-view=true) to add a setting that users turn on and off from the toolbar. The following example adds a toggle that controls touch input. Pen and mouse input always draw ink.

```xaml
<StackPanel Spacing="12">
    <InkToolbar AutomationProperties.Name="Drawing tools" TargetInkCanvas="{x:Bind inkCanvas}">
        <InkToolbarCustomToggleButton
            x:Name="touchWritingToggle"
            AutomationProperties.Name="Touch writing"
            Checked="TouchWritingToggle_Changed"
            ToolTipService.ToolTip="Touch writing"
            Unchecked="TouchWritingToggle_Changed">
            <SymbolIcon Symbol="TouchPointer" />
        </InkToolbarCustomToggleButton>
    </InkToolbar>
    <TextBlock x:Name="deviceInfo" />
    <Border
        Width="480"
        Height="280"
        HorizontalAlignment="Left"
        Background="White"
        BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
        BorderThickness="1">
        <InkCanvas x:Name="inkCanvas" AutomationProperties.Name="Drawing surface" />
    </Border>
</StackPanel>
```

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Core;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        // Pen and mouse input always draw ink. The custom toggle controls touch input.
        inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse;
        ShowDevices();
    }

    private void TouchWritingToggle_Changed(object sender, RoutedEventArgs e)
    {
        if (touchWritingToggle.IsChecked == true)
        {
            inkCanvas.InkPresenter.InputDeviceTypes |= CoreInputDeviceTypes.Touch;
        }
        else
        {
            inkCanvas.InkPresenter.InputDeviceTypes &= ~CoreInputDeviceTypes.Touch;
        }

        ShowDevices();
    }

    private void ShowDevices()
    {
        deviceInfo.Text = $"InputDeviceTypes = {inkCanvas.InkPresenter.InputDeviceTypes}";
    }
}
```

:::image type="content" source="images/inking/custom-toggle.png" alt-text="An ink toolbar with ballpoint pen, pencil, highlighter, eraser, and ruler buttons followed by a custom toggle button with a touch icon, which is turned on. Text reads InputDeviceTypes = Touch, Pen, Mouse, and the canvas shows black ink strokes.":::

The toggle shows only an icon, so the example gives it an accessible name and a tooltip. In this example, the toolbar also creates its default buttons, and the custom toggle appears after them.

The toolbar supports other custom buttons:

- [`InkToolbarCustomToolButton`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbarcustomtoolbutton?view=windows-app-sdk-2.0-experimental&preserve-view=true) adds a tool of your own.
- [`InkToolbarCustomPenButton`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbarcustompenbutton?view=windows-app-sdk-2.0-experimental&preserve-view=true) adds a pen that creates its own drawing attributes. Set its `CustomPen` property to an instance of a class that derives from [`InkToolbarCustomPen`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inktoolbarcustompen?view=windows-app-sdk-2.0-experimental&preserve-view=true) and overrides the `CreateInkDrawingAttributesCore` method.

### Create the toolbar in code

You can also create the canvas and the toolbar in code. The following example adds them to a `Border` named `inkHost`, connects the toolbar with `TargetInkPresenter`, and selects the pencil when the toolbar loads.

```xaml
<Border x:Name="inkHost" HorizontalAlignment="Left" />
```

```csharp
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Core;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        CreateInkingUI();
    }

    private void CreateInkingUI()
    {
        InkCanvas canvas = new();
        AutomationProperties.SetName(canvas, "Drawing surface");
        canvas.InkPresenter.InputDeviceTypes =
            CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch;

        InkToolbar toolbar = new() { TargetInkPresenter = canvas.InkPresenter };
        AutomationProperties.SetName(toolbar, "Drawing tools");

        // The toolbar creates its built-in buttons when it loads.
        toolbar.Loaded += (sender, args) => toolbar.ActiveTool = toolbar.GetToolButton(InkToolbarTool.Pencil);

        Border canvasFrame = new()
        {
            Width = 480,
            Height = 280,
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Colors.DarkGray),
            BorderThickness = new Thickness(1),
            Child = canvas,
        };

        StackPanel layout = new() { Spacing = 8 };
        layout.Children.Add(toolbar);
        layout.Children.Add(canvasFrame);

        inkHost.Child = layout;
    }
}
```

:::image type="content" source="images/inking/code-only.png" alt-text="An ink toolbar with the pencil button selected above a canvas with gray pencil strokes.":::

## Known limitations

- The inking controls are available only in experimental releases of the Windows App SDK. They aren't available in the stable channel.
- The inking APIs don't include handwriting or shape recognition.
- Mouse input doesn't draw ink until you add `CoreInputDeviceTypes.Mouse` to `InputDeviceTypes`.

The following issues were reported with Windows App SDK 2.4.1-experimental. The 2.5.4-experimental release notes don't list them as fixed:

- The ruler and protractor stencils don't work.
- `RenderTargetBitmap` doesn't capture ink. To create an image of the ink, save it in the `GifWithEmbeddedIsf` format.
- Setting a pen button's `Palette` property in XAML causes a XAML parse error. Set `Palette` in code instead, as shown in [Customize a pen's colors and widths](#customize-a-pens-colors-and-widths).
- Button flyouts can appear empty, and changing `ButtonFlyoutPlacement` might not change where they open.

### Changes from 2.4.1-experimental

If you used the inking APIs in Windows App SDK 2.4.1-experimental, update your code for these changes in 2.5.4-experimental:

| API in 2.4.1-experimental | Change in 2.5.4-experimental |
|---|---|
| `InkToolbar.TargetInkPresenter` (type `Object`) | The property type is `InkPresenter`. |
| `InkPresenter.GetHighContrastAdjustment` and `InkPresenter.SetHighContrastAdjustment` methods | Replaced by the `InkPresenter.HighContrastAdjustment` property. |
| `InkStrokeInput.GetInkPresenter` and `InkUnprocessedInput.GetInkPresenter` methods | Replaced by an `InkPresenter` property on each class. |
| `InkToolbarEraserButton.SelectedEraser`, `IsStrokeEraserVisible`, and `ArePrecisionErasersVisible` properties; `InkToolbar.EraserFlyoutItemClicked` event; `InkToolbarEraserKind`, `InkToolbarEraserFlyoutItemKind`, and `InkToolbarEraserFlyoutItemClickedEventArgs` types | Removed. |
| Not available | `InkPresenter.ActivateCustomDrying` method and `InkSynchronizer` class added. |

## Accessibility and high contrast

Ink is visual content, so plan how all users can use your inking features:

- Give each canvas and toolbar an accessible name with `AutomationProperties.Name`, as the examples in this article do. Give custom buttons that show only an icon a name and a tooltip.
- [`InkCanvasAutomationPeer`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.inkcanvasautomationpeer?view=windows-app-sdk-2.0-experimental&preserve-view=true) exposes the canvas to UI Automation, but it doesn't describe the strokes. If the ink has meaning, such as a signature or a labeled diagram, provide a text alternative.
- Drawing ink requires pointer input. Don't make ink the only way to complete a task. For example, let users type their name as an alternative to signing it.
- The automation peers for the built-in tool buttons support the SelectionItem and ExpandCollapse control patterns. Test keyboard navigation and the button flyouts with the toolbar configuration that your app uses.

When a contrast theme is active, the presenter can replace ink colors with system colors. To choose this behavior, set the [`HighContrastAdjustment`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkpresenter.highcontrastadjustment?view=windows-app-sdk-2.0-experimental&preserve-view=true) property to an [`InkHighContrastAdjustment`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.inkhighcontrastadjustment?view=windows-app-sdk-2.0-experimental&preserve-view=true) value from the `Microsoft.UI.Xaml.Controls` namespace. The Windows Runtime documentation for the [enumeration with the same name](/uwp/api/windows.ui.input.inking.inkhighcontrastadjustment) describes the values this way:

| Value | Ink color in a contrast theme |
|---|---|
| `UseSystemColorsWhenNecessary` | The ink's own color if it contrasts enough with the background. Otherwise, a system color. |
| `UseSystemColors` | A system color. Highlighter ink uses the system highlighter color. |
| `UseOriginalColors` | The ink's own color. |

```csharp
inkCanvas.InkPresenter.HighContrastAdjustment = InkHighContrastAdjustment.UseSystemColorsWhenNecessary;
```

To keep them short, the examples in this article set a white background directly. In your app, define the canvas background in theme dictionaries so that it follows the active theme, as described in [Contrast themes](../../../design/accessibility/high-contrast-themes.md#creating-theme-dictionaries). Then test your app with each contrast theme, and check that the ink, the background, and the border stay distinguishable.

For more information, see [Expose basic accessibility information](../../../design/accessibility/basic-accessibility-information.md) and [Keyboard accessibility](../../../design/accessibility/keyboard-accessibility.md).

## See also

- [WinUI 3 Gallery InkCanvas sample source code](https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/Samples/InkCanvas)
- [Windows App SDK 2.5.4-experimental release notes](../../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-25-experimental-254-experimental)
- [Microsoft.UI.Xaml.Controls namespace](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls?view=windows-app-sdk-2.0-experimental&preserve-view=true)
- [Windows.UI.Input.Inking namespace](/uwp/api/windows.ui.input.inking)
- [Pen haptic feedback in Windows apps](../../input/pen-haptics.md)
- [Pen interactions and Windows Ink (UWP)](/windows/uwp/ui-input/pen-and-stylus-interactions)
- [Inking controls (UWP)](/windows/uwp/ui-input/inking-controls)
- [What's supported when migrating from UWP to WinUI 3](../../../windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported.md)
