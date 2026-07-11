---
title: XAML analysis and best practices
description: Use the XAML analysis tool in Visual Studio to identify and fix common performance issues in your Windows App SDK and WinUI 3 application code.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 07/08/2026
---

# XAML analysis overview

XAML analysis is a tool that notifies you of performance issues in your app. It checks your app code against a set of performance guidelines and best practices, and identifies problems from a ruleset of common issues.

When the tool finds a problem, it points to Visual Studio's diagnostic tools, source information, and documentation so you can investigate and fix the issue.

## Decoded image size larger than render size

Images captured at high resolutions can cause your app to use more CPU when decoding and more memory after loading from disk. There is no advantage to decoding a high-resolution image if you only display it at a smaller size. Instead, create a version of the image at the size you draw it on-screen using the `DecodePixelWidth` and `DecodePixelHeight` properties.

### Impact

Displaying images at sizes other than their native size can negatively impact CPU time (decoding and download) and memory.

### Causes and solutions

#### Image is not set asynchronously

Your app uses `SetSource()` instead of `SetSourceAsync()`. Always use [SetSourceAsync](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapsource.setsourceasync) when setting a stream to decode images asynchronously.

#### Image source is set before the element is in the live tree

The `BitmapImage` is connected to the live XAML tree after setting the content with `SetSourceAsync` or `UriSource`. Always attach a [BitmapImage](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapimage) to the live tree before setting the source. When you specify an image element or brush in markup, attachment happens automatically.

**Good** — source specified in markup using `BitmapImage`:

```xml
<Image x:Name="myImage">
    <Image.Source>
        <BitmapImage UriSource="Assets/cool-image.png"/>
    </Image.Source>
</Image>
```

**Good** — connecting the BitmapImage to the tree before setting its UriSource:

```csharp
Image myImage = new Image();
var bitmapImage = new BitmapImage();
myImage.Source = bitmapImage;
bitmapImage.UriSource = new Uri("ms-appx:///Assets/cool-image.png", UriKind.RelativeOrAbsolute);
```

**Bad** — setting the BitmapImage's UriSource before connecting it to the tree:

```csharp
Image myImage = new Image();
var bitmapImage = new BitmapImage();
bitmapImage.UriSource = new Uri("ms-appx:///Assets/cool-image.png", UriKind.RelativeOrAbsolute);
myImage.Source = bitmapImage;
```

#### Image brush is non-rectangular

When you use an image for a non-rectangular brush, the image uses a software rasterization path, which does not scale images. Additionally, the system stores a copy of the image in both software and hardware memory. When you use a non-rectangular brush, pre-scale your images to approximately the size you render them at.

Alternatively, set an explicit decode size using the [DecodePixelWidth](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapimage.decodepixelwidth) and [DecodePixelHeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapimage.decodepixelheight) properties:

```xml
<Image>
    <Image.Source>
    <BitmapImage UriSource="ms-appx:///Assets/highresCar.jpg"
                 DecodePixelWidth="300" DecodePixelHeight="200"/>
    </Image.Source>
</Image>
```

The units for `DecodePixelWidth` and `DecodePixelHeight` are physical pixels by default. Set [DecodePixelType](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.bitmapimage.decodepixeltype) to **Logical** to have the decode size automatically account for the system's current scale factor, similar to other XAML content.

If you cannot determine an appropriate decode size ahead of time, rely on XAML's automatic right-size-decoding, which makes a best effort attempt to decode the image at the appropriate size.

#### Images used inside BitmapIcons fall back to natural size

Set an explicit decode size using the `DecodePixelWidth` and `DecodePixelHeight` properties.

#### DecodePixelWidth or DecodePixelHeight are larger than display size

If you explicitly set `DecodePixelWidth` or `DecodePixelHeight` larger than the image's on-screen display size, the app uses extra memory (up to 4 bytes per pixel), which becomes expensive for large images.

#### Image is hidden

The image is hidden by setting `Opacity` to 0 or `Visibility` to `Collapsed` on the host image element, brush, or a parent element. Images not visible on screen due to clipping or transparency may fall back to decoding at natural size.

#### Image uses NineGrid property

When you use an image with [NineGrid](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image.ninegrid), the image uses a software rasterization path. Pre-scale your images to approximately the size you render them at.

## Collapsed elements at load time

A common pattern is to hide UI elements initially and show them later. In most cases, you should defer these elements using `x:Load` to avoid paying the cost of creating the element at load time.

### Impact

Collapsed elements load alongside other elements and increase load time.

### Solution

Use the [x:Load attribute](/windows/uwp/xaml-platform/x-load-attribute) to delay the loading of UI. The element loads when you need it, which reduces the initial processing. You can also use `x:Bind` to control the loading state.

## ListView is not virtualized

UI virtualization is the most important improvement you can make for collection performance. UI elements representing items are created on demand. For a control bound to a 1000-item collection, creating all UI at once wastes resources because you can't display all items simultaneously. `ListView` and `GridView` handle UI virtualization for you — they generate UI for items near the viewport and reclaim memory for items scrolled out of view.

### Impact

A non-virtualized `ItemsControl` increases load time and resource usage by loading more child items than necessary.

### Solution

Set a width and height on the `ItemsControl` to define a viewport. When you place a virtualized control in a panel with unlimited space (such as `ScrollViewer` or a `Grid` with auto-sized rows), the control takes enough room for all items, which defeats virtualization.

## UI thread blocked or idle during load

UI thread blocking occurs when synchronous calls to functions executing off-thread block the UI thread. Keep the UI thread responsive by using asynchronous APIs. For more information, see [Keep the UI thread responsive](keep-ui-thread-responsive.md).

## Use {x:Bind} instead of {Binding}

`{Binding}` uses more time and memory than `{x:Bind}`. Creating a `{Binding}` causes a series of allocations, and updating a binding target can involve reflection and boxing. Use `{x:Bind}`, which compiles bindings at build time for better performance and compile-time validation.

> [!NOTE]
> `{x:Bind}` is not suitable for all cases, such as late-bound scenarios. See the [{x:Bind} documentation](/windows/uwp/xaml-platform/x-bind-markup-extension) for details.

## Use x:Key instead of x:Name in ResourceDictionaries

When you use `x:Name` on a resource, the platform instantiates it immediately because `x:Name` creates a field reference. Use `x:Key` instead when you don't need to reference the resource from code-behind.

## Use virtualizing panels for collections

If you provide a custom items panel template (see `ItemsPanel`), use a virtualizing panel such as `ItemsWrapGrid` or `ItemsStackPanel`. Using `VariableSizedWrapGrid`, `WrapGrid`, or `StackPanel` disables virtualization.

## Accessibility: Provide names for UIA elements

Set `AutomationProperties.Name` in your control's XAML to an appropriate localized string. If an element should not appear in the UIA tree, set `AutomationProperties.AccessibilityView = "Raw"`.

Avoid giving two UIA elements with the same parent the same `Name` and `ControlType`. In lists where duplicate names commonly occur, use binding to set `AutomationProperties.Name` from a data source.

## Related content

- [Optimize your XAML markup](optimize-xaml-loading.md)
- [Optimize your XAML layout](optimize-xaml-layout.md)
- [Keep the UI thread responsive](keep-ui-thread-responsive.md)
