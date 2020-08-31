---
title: App analysis
description: Learn about the App Analysis tool and view the performance guidelines and best practices that it uses to evaluate your app code.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# App Analysis overview

App Analysis  is a tool that provides developers with a heads up notification of performance issues. App Analysis runs your app code against a set of performance guidelines and best practices.

App Analysis identifies problems from a ruleset of common performance issues that apps run into. When appropriate, app analysis will point to Visual Studio's timeline tool, source information, and documentation to give you the means to investigate.

Rules in App Analysis refer to a guideline or best practice that your app is being checked against.

## Decoded image size larger than render size

Images are captured at very high resolutions, which can lead to apps using more CPU when decoding the image data and more memory after it’s loaded from disk. But there’s no sense decoding and saving a high-resolution image in memory only to display it smaller than its native size. Instead, create a version of the image at the exact size it will be drawn on-screen using the DecodePixelWidth and DecodePixelHeight properties.

### Impact

Displaying images at their non-native sizes can negatively impact both CPU time (due to decoding to the proper size and download time) and memory.

### Causes and solutions

#### Image is not being set asynchronously

App is using SetSource() instead of SetSourceAsync(). You should always avoid using [**SetSource**](/uwp/api/windows.ui.xaml.media.imaging.bitmapsource.setsource) and instead use [**SetSourceAsync**](/uwp/api/windows.ui.xaml.media.imaging.bitmapsource.setsourceasync) when setting a stream to decode images asynchronously. 

#### Image is being called when the ImageSource is not in the live tree

The BitmapImage is connected to the live XAML tree after setting the content with SetSourceAsync or UriSource. You should always attach a [**BitmapImage**](/uwp/api/Windows.UI.Xaml.Media.Imaging.BitmapImage) to the live tree before setting the source. Any time an image element or brush is specified in markup, this will automatically be the case. Examples are provided below. 

**Live tree examples**

Example 1 (good)—Uniform Resource Identifier (URI) specified in markup.

```xml
<Image x:Name="myImage" UriSource="Assets/cool-image.png"/>
```

Example 2 markup—URI specified in code-behind.

```xml
<Image x:Name="myImage"/>
```

Example 2 code-behind (good)—connecting the BitmapImage to the tree before setting its UriSource.

```vb
var bitmapImage = new BitmapImage();
myImage.Source = bitmapImage;
bitmapImage.UriSource = new URI("ms-appx:///Assets/cool-image.png", UriKind.RelativeOrAbsolute);
```

Example 2 code-behind (bad)—setting the BitmapImage's UriSource before connecting it to the tree.

```vb
var bitmapImage = new BitmapImage();
bitmapImage.UriSource = new URI("ms-appx:///Assets/cool-image.png", UriKind.RelativeOrAbsolute);
myImage.Source = bitmapImage;
```

#### Image brush is non-rectangular 

When an image is used for a non-rectangular brush, the image will use a software rasterization path, which will not scale images at all. Additionally, it must store a copy of the image in both software and hardware memory. For example, if an image is used as a brush for an ellipse, the potentially large full image will be stored twice internally. When using a non-rectangular brush, your app should pre-scale its images to approximately the size they will be rendered at.

Alternatively, you can set an explicit decode size to create a version of the image at the exact size it will be drawn on-screen using the [**DecodePixelWidth**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelwidth) and [**DecodePixelHeight**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelheight) properties.

```xml
<Image>
    <Image.Source>
    <BitmapImage UriSource="ms-appx:///Assets/highresCar.jpg" 
                 DecodePixelWidth="300" DecodePixelHeight="200"/>
    </Image.Source>
</Image>
```

The units for [**DecodePixelWidth**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelwidth) and [**DecodePixelHeight**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelheight) are by default physical pixels. The [**DecodePixelType**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixeltype) property can be used to change this behavior: setting **DecodePixelType** to **Logical** results in the decode size automatically accounting for the system’s current scale factor, similar to other XAML content. It would therefore be generally appropriate to set **DecodePixelType** to **Logical** if, for example, you want **DecodePixelWidth** and **DecodePixelHeight** to match the Height and Width properties of the Image control that the image will be displayed in. With the default behavior of using physical pixels, you must account for the system’s current scale factor yourself, and you should listen for scale change notifications in case the user changes their display preferences.

In some cases where an appropriate decode size cannot be determined ahead of time, you should defer to XAML’s automatic right-size-decoding, which will make a best effort attempt to decode the image at the appropriate size if an explicit DecodePixelWidth/DecodePixelHeight is not specified.

You should set an explicit decode size if you know the size of the image content ahead of time. You should also in conjunction set [**DecodePixelType**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixeltype) to **Logical** if the supplied decode size is relative to other XAML element sizes. For example, if you explicitly set the content size with Image.Width and Image.Height, you could set DecodePixelType to DecodePixelType.Logical to use the same logical pixel dimensions as an Image control, and then explicitly use BitmapImage.DecodePixelWidth and/or BitmapImage.DecodePixelHeight to control the size of the image to achieve potentially large memory savings.

Note that Image.Stretch should be considered when determining the size of the decoded content.

#### Images used inside of BitmapIcons fall back to decoding to natural size 

Set an explicit decode size to create a version of the image at the exact size it will be drawn on-screen by using the [**DecodePixelWidth**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelwidth) and [**DecodePixelHeight**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelheight) properties.

#### Images that appear extremely large on screen fall back to decoding to natural size 

Images that appear extremely large on screen fall back to decoding to natural size. Set an explicit decode size to create a version of the image at the exact size it will be drawn on-screen by using the [**DecodePixelWidth**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelwidth) and [**DecodePixelHeight**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelheight) properties.

#### Image is hidden

The image is hidden via setting Opacity to 0 or Visibility to Collapsed on the host image element or brush or any parent element. Images not visible on screen due to clipping or transparency may fall back to decoding to natural size. 

#### Image is using NineGrid property

When an image is used for a [**NineGrid**](/uwp/api/windows.ui.xaml.controls.image.ninegrid), the image will use a software rasterization path, which will not scale images at all. Additionally, it must store a copy of the image in both software and hardware memory. When using **NineGrid**, your app should pre-scale its images to approximately the size they will be rendered at.

Images that use the NineGrid property fall back to decoding to natural size. Consider adding the ninegrid effect to the original image.

#### DecodePixelWidth or DecodePixelHeight are set to a size that's larger than the image will appear on screen 

If DecodePixelWidth/Height are explicitly set larger than the image that will be displayed on-screen, the app will unnecessarily use extra memory, up to 4 bytes per pixel, which quickly becomes expensive for large images. The image will also be scaled down using bilinear scaling, which could cause it to appear blurry for large scale factors.

#### Image is decoded as part of producing a Drag and Drop image

Set an explicit decode size to create a version of the image at the exact size it will be drawn on-screen by using the [**DecodePixelWidth**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelwidth) and [**DecodePixelHeight**](/uwp/api/windows.ui.xaml.media.imaging.bitmapimage.decodepixelheight) properties.

## Collapsed elements at load time

A common pattern in apps is to hide elements in the UI initially and show them at a later time. In most cases these elements should be deferred using x:Load or x:DeferLoadStrategy to avoid paying the cost of creating the element at load time.

This includes cases where a boolean to visibility converter is used to hide items until a later time.

### Impact

Collapsed elements are loaded alongside other elements and contribute to an increase in load time.

### Cause

This rule was triggered because an element was collapsed at load time. Collapsing an element, or setting its opacity to 0, does not prevent the element from being created. This rule could be caused by an app that uses a Boolean to visibility converter that defaults to false.

### Solution

Using [x:Load attribute](../xaml-platform/x-load-attribute.md) or [x:DeferLoadStrategy](../xaml-platform/x-deferloadstrategy-attribute.md), you can delay the loading of a piece of UI, and load it when needed. This is good way to delay processing UI that is not visible in the first frame. You can opt to load the element when needed, or as part of a set of delayed logic. To trigger loading, call findName on the element you wish to load. x:Load extends the capabilities of x:DeferLoadStrategy enabling elements to be unloaded, and for the loading state to be controlled via x:Bind.

In some cases, using findName to show a piece of UI may not be the answer. This is true if you are expecting to realize a significant piece of UI on the click of a button with very low latency. In this case, you may want to trade off faster UI latency at the cost of additional memory, if so you should use x:DeferLoadStrategy and set Visibility to Collapsed on the element you wish to realize. After the page has been loaded and the UI thread is free, you can call findName when necessary to load the elements. The elements won't be visible to the user until you set the Visibility of the element to Visible.

## ListView is not virtualized

UI virtualization is the most important improvement you can make to improve collection performance. This means that UI elements representing the items are created on demand. For an items control bound to a 1000-item collection, it would be a waste of resources to create the UI for all the items at the same time because they can't all be displayed at the same time. ListView and GridView (and other standard ItemsControl-derived controls) perform UI virtualization for you. When items are close to being scrolled into view (a few pages away), the framework generates the UI for the items and caches them. When it's unlikely that the items will be shown again, the framework re-claims the memory.

UI virtualization is just one of several key factors to improving collection performance. Reducing the complexity of collection items and data virtualization are two other important aspects to improving collection performance. For more information about improving collection performance within ListViews and GridViews, see the articles on [ListView and GridView UI optimization](./optimize-gridview-and-listview.md) and [ListView and Gridview data virtualization](./listview-and-gridview-data-optimization.md).

### Impact

A non-virtualized ItemsControl will increase load time and resource usage by loading more of its child items than necessary.

### Cause

The concept of a viewport is critical to UI virtualization because the framework must create the elements that are likely to be shown. In general, the viewport of an ItemsControl is the extent of the logical control. For example, the viewport of a ListView is the width and height of the ListView element. Some panels allow child elements unlimited space, examples being ScrollViewer and a Grid, with auto-sized rows or columns. When a virtualized ItemsControl is placed in a panel like that, it takes enough room to display all of its items, which defeats virtualization. 

### Solution

Restore virtualization by setting a width and height on the ItemsControl you are using.

## UI thread blocked or idle during load

UI thread blocking refers to synchronous calls to functions executing off-thread that block the UI thread.  

For a full list of best practices to improve your app's startup performance, see [Best practices for your app's startup performance](./best-practices-for-your-app-s-startup-performance.md) and [Keep the UI thread responsive](./keep-the-ui-thread-responsive.md).

### Impact

A blocked or idle UI thread during load time will prevent layout and other UI operations, increasing startup time.

### Cause

Platform code for UI and your app’s code for UI all are executed on the same UI thread. Only one instruction can execute on that thread at a time, so if your app code takes too long to process an event, the framework can’t run layout or raise new events representing user interaction. The responsiveness of your app is related to the availability of the UI thread to process work.

### Solution

Your app can be interactive even though there are parts of the app that aren't fully functional. For example, if your app displays data that takes a while to retrieve, you can make that code execute independently of the app's startup code by retrieving the data asynchronously. When the data is available, populate the app's user interface with the data. To help keep your app responsive, the platform provides asynchronous versions of many of its APIs. An asynchronous API ensures that your active execution thread never blocks for a significant amount of time. When you call an API from the UI thread, use the asynchronous version if it's available.

## {Binding} is being used instead of {x:Bind}

This rule is fired when your app uses a {Binding} statement. To improve app performance, apps should consider using {x:Bind}.

### Impact

{Binding} runs in more time and more memory than {x:Bind}.

### Cause

App is using {Binding} instead of {x:Bind}. {Binding} brings with it non-trivial working set and CPU overhead. Creating a {Binding} causes a series of allocations, and updating a binding target can cause reflection and boxing.

### Solution

Use the {x:Bind} markup extension, which compiles bindings at build time. {x:Bind} bindings (often referred-to as compiled bindings) have great performance, provide compile-time validation of your binding expressions, and support debugging by enabling you to set breakpoints in the code files that are generated as the partial class for your page. 

Note that x:Bind is not suitable in all cases such as late-bound scenarios. For a full list of cases not covered by {x:Bind}, see the {x:Bind} documentation.

## x:Name is being used instead of x:Key

ResourceDictionaries are generally used to store your resources at a somewhat global level, that is, resources that your app wants to reference in multiple places; for example, styles, brushes, templates, and so on. In general, we have optimized ResourceDictionaries to not instantiate resources unless they're asked for. But there are few places where you need to be a little careful.

### Impact

Any resource with x:Name will be instantiated as soon as the ResourceDictionary is created. This happens because x:Name tells the platform that your app needs field access to this resource, so the platform needs to create something to create a reference to.

### Cause

Your app is setting x:Name on a resource.

### Solution

Use x:Key instead of x:Name when you are not referencing resources from code-behind.

## Collections control is using a non-virtualizing panel

If you provide a custom items panel template (see ItemsPanel), make sure you use a virtualizing panel such as ItemsWrapGrid or ItemsStackPanel. If you use VariableSizedWrapGrid, WrapGrid, or StackPanel, you will not get virtualization. Additionally, the following ListView events are raised only when using an ItemsWrapGrid or an ItemsStackPanel: ChoosingGroupHeaderContainer, ChoosingItemContainer, and ContainerContentChanging.

UI virtualization is the most important improvement you can make to improve collection performance. This means that UI elements representing the items are created on demand. For an items control bound to a 1000-item collection, it would be a waste of resources to create the UI for all the items at the same time because they can't all be displayed at the same time. ListView and GridView (and other standard ItemsControl-derived controls) perform UI virtualization for you. When items are close to being scrolled into view (a few pages away), the framework generates the UI for the items and caches them. When it's unlikely that the items will be shown again, the framework re-claims the memory.

UI virtualization is just one of several key factors to improving collection performance. Reducing the complexity of collection items and data virtualization are two other important aspects to improving collection performance. For more information about improving collection performance within ListViews and GridViews, see the articles on [ListView and GridView UI optimization](./optimize-gridview-and-listview.md) and [ListView and Gridview data virtualization](./listview-and-gridview-data-optimization.md).

### Impact

A non-virtualized ItemsControl will increase load time and resource usage by loading more of its child items than necessary.

### Cause

You are using a panel that does not support virtualization.

### Solution

Use a virtualizing panel such as ItemsWrapGrid or ItemsStackPanel.

## Accessibility: UIA elements with no name

In XAML, you can provide a name by setting AutomationProperties.Name. Many automation peers provide a default name to UIA if AutomationProperties.Name is unset. 

### Impact

If a user reaches an element with no name, they often will have no way of knowing what the element relates to. 

### Cause

Element’s UIA name is null or empty. This rule checks what UIA sees, not the value of the AutomationProperties.Name.

### Solution

Set the AutomationProperties.Name property in the control's XAML to an appropriate localized string.

Sometimes the right application fix isn't to provide a name, it's to remove the UIA element from all but the raw trees. You can do that in XAML by setting `AutomationProperties.AccessibilityView = "Raw"`.

## Accessibility: UIA elements with the same Controltype should not have the same name

Two UIA elements with the same UIA parent must not have the same Name and ControlType. It's okay to have two controls with the same Name if they have different ControlTypes. 

This rule doesn't check for duplicate names with different parents. However, in most cases, you shouldn't duplicate Names and ControlTypes within an entire window, even with different parents. Cases where duplicate names within a window are acceptable are two lists with identical items. In this case, the list items are expected to have identical Names and ControlTypes.

### Impact

If a user reaches an element with the same Name and ControlType as another element with the same UIA parent, the user may not be able to distinguish the difference between the elements.

### Cause

UIA elements with the same UIA parent have the same Name and ControlType.

### Solution

Set a name in XAML using AutomationProperties.Name. In lists where this commonly occurs, use binding to bind the value of the AutomationProperties.Name to a data source.