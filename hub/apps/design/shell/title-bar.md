---
description: Customize the title bar of a desktop app to match the personality of the app.
title: Title bar customization
template: detail.hbs
ms.date: 11/15/2021
ms.topic: article
keywords: windows 10, uwp, title bar
doc-status: Draft
ms.localizationpriority: medium
---
# Title bar customization

Windows provides a default title bar for every app window and lets you to customize it to match the personality of your app. The default title bar comes with some standard components: the title bar rectangle, the title, caption controls (Close, Minimize, Maximize), and core functionality such as drag regions and sizing.


:::image type="content" source="images/titlebar-overview.png" alt-text="Title bar":::

See the [Title bar](../../design/basics/app-titlebar.md) design article for guidance on customizing your app's title bar, acceptable title bar area content, and recommended UI patterns.

> [!div class="nextstepaction"]
> [See the Windows 11 Fluent Design guidance for title bar](../../design/basics/app-titlebar.md)

## Platform options

The exact features of the title bar and the options available to customize it depend on your UI platform and app requirements. This article shows how to customize the title bar for apps that use either the Windows App SDK with WinUI 3 or UWP with WinUI 2.

> [!NOTE]
> This article assumes that you are already using either the Windows App SDK/WinUI 3 or UWP/WinUI 2, and therefore does not emphasize any comparison between the two. For a detailed comparison of the windowing models used by the Windows App SDK and UWP,see [Windowing functionality migration](/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing.md).

### [Windows App SDK/WinUI 3](#tab/winui3)

> [!div class="checklist"]
> * **Applies to**: Windows App SDK/WinUI 3
> * **Important APIs**: [AppWindow.TitleBar property](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.titlebar), [AppWindowTitleBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar), [AppWindow class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow)

The [Windows App SDK](/windows/apps/develop) provides the [Microsoft.UI.Windowing.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class that's based on the Win32 HWND model. There's a 1:1 mapping between an AppWindow and a top-level HWND in your app. AppWindow and its related classes provide APIs that let you manage many aspects of your app's top-level windows, including customization of the title bar. You can modify the default title bar that Windows provides so that it blends with the rest of your UI, or extend your app canvas into the title bar area and provide your own title bar content.

In addition to the standard components listed previously, the default title bar includes an app icon and the system menu, which is accessed by clicking the app icon or right-clicking the title bar.

:::image type="content" source="images/titlebar-overview.png" alt-text="Title bar":::

> WinUI 3 desktop apps can also use Windows App SDK to extend their windowing features. WinUI3 as a platform offers some customization APIs e.g., SetTitleBar.  With Windows App SDK you have access to more customization APIs.
>
> ***REVIEW: Need more info. SetTitleBar doesn't seem to do anything in WinUI 3. Are there any other APIs to mention?***

### How to work with AppWindow

You can use AppWindow APIs with any UI framework that the Windows App SDK supports - Win32, WPF, WinForms, or WinUI 3 - and you can adopt them incrementally, using only the APIs you need. You get an AppWindow object from an existing window using the interop APIs. With this AppWindow object you have access to the title bar customization APIs. For more about the interop APIs, see [Manage app windows - UI framework and HWND interop](/windows/apps/windows-app-sdk/windowing/windowing-overview#ui-framework-and-hwnd-interop) and the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing).

> [!IMPORTANT]
> Title bar customization APIs currently work on Windows 11 only. We recommend that you check [AppWindowTitleBar.IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) in your code before you call these APIs.

### Win32/DWM

The Desktop Window Manager (DWM) APIs let you remove the non-client area of your window, which makes your title bar area completely customizable. While this does allow for extensive customization, you lose a lot of core windowing functionality that you have to re-implement yourself; for example, the title, icon, caption buttons, system menu, and the drag and sizing behavior. What's more, picking this option means that you must constantly emulate the new Windows design every time Window's styles are updated. In Windows 11, for example, we introduced rounded corners, updated caption buttons, and the snap flyout menu. Apps with custom title bars and frames need to be updated to look at home in Windows 11. (For more info, see [Apply rounded corners](/windows/apps/desktop/modernize/apply-rounded-corners) and [Support snap layouts](/windows/apps/desktop/modernize/apply-snap-layout-menu)). For new apps, we highly recommend that you use the Windows App SDK APIs to customize your title bar.

For more info about DWM, see [Custom Window Frame Using DWM](/windows/win32/dwm/customframe).

### [UWP/WinUI 2](#tab/winui2)

> [!div class="checklist"]
> * **Applies to**: UWP/WinUI 2
> * **Important APIs**: [ApplicationView.TitleBar property](/uwp/api/windows.ui.viewmanagement.applicationview), [ApplicationViewTitleBar class](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar), [CoreApplicationViewTitleBar class](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar)

In UWP applications, you can customize the title bar by using APIs on the ApplicationView and CoreApplicationView classes. There are multiple APIs to progressively modify the appearance of your title bar based on the level of customization needed. By default, UWP defines the title bar of your application to include the app title from the display name property in the package manifest.

> [!NOTE]
> The [Windows.UI.WindowManagement.AppWindow](/uwp/api/windows.ui.windowmanagement.appwindow) class, used for secondary windows in UWP apps, does not support title bar customization. To customize the title bar of a UWP app that uses secondary windows, use ApplicationView as described in [Show multiple views with ApplicationView](/windows/apps/design/layout/application-view).

If you are considering migrating your UWP app to Windows App SDK, please view our windowing functionality migration guide. See [Windowing functionality migration](/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing.md) for more information.

---

## How much to customize the title bar

There are two levels of customization that you can apply to the title bar: apply minor modifications to the default title bar, or extend your app canvas into the title bar area and provide completely custom content.

For simple customization, such as changing the title bar color you can set properties on the title bar object for your app window to specify the colors you want to use for title bar elements. In this case, the system retains responsibility for all other aspects of the title bar, such as drawing the app title and defining draggable areas.

Your other option is to hide the default title bar and replace it with your own custom content. For example, you can place text, a search box, or custom menus in the title bar area. You will also need to use this option to extend a [material](../../design/signature-experiences/materials.md) backdrop, like [Mica](../../design/style/mica.md), into the title bar area.

When you opt for full customization, you are responsible for putting content in the title bar area, and you can define your own draggable region. The caption controls (system Close, Minimize, and Maximize buttons) are still available and handled by the system, but elements like the app title are not. You will need to create those elements yourself as needed by your app.

## Simple customization

If you want only to customize the title bar colors or icon, you can set properties on the title bar object for your app window.

### [Windows App SDK/WinUI 3](#tab/winui3)

These examples shows how to get an instance of [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) and set its properties.

#### Title

By default, the title bar shows the app type as the window title (for example, "WinUI Desktop"). To change the window title, set the [AppWindow.Title](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.title) property to a single-line text value.

```csharp
using Microsoft.UI;           // Needed for WindowId
using Microsoft.UI.Windowing; // Needed for AppWindow
using WinRT.Interop;          // Needed for XAML hwnd interop

private AppWindow m_AppWindow;

public MainWindow()
{
    this.InitializeComponent();

    m_AppWindow = GetAppWindowForCurrentWindow();
    m_AppWindow.Title = "My app title";
}

private AppWindow GetAppWindowForCurrentWindow()
{
    IntPtr hWnd = WindowNative.GetWindowHandle(this);
    WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
    return AppWindow.GetFromWindowId(myWndId);
}
```

#### Colors

This example shows how to get an instance of [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) and set its color properties.

```csharp
private bool SetTitleBarColors()
{
    if (AppWindowTitleBar.IsCustomizationSupported())
    {
        if (m_AppWindow is null)
        {
            m_AppWindow = GetAppWindowForCurrentWindow();
        }
        var titleBar = m_AppWindow.TitleBar;

        // Set active window colors
        titleBar.ForegroundColor = Colors.White;
        titleBar.BackgroundColor = Colors.Green;
        titleBar.ButtonForegroundColor = Colors.White;
        titleBar.ButtonBackgroundColor = Colors.SeaGreen;
        titleBar.ButtonHoverForegroundColor = Colors.Gainsboro;
        titleBar.ButtonHoverBackgroundColor = Colors.DarkSeaGreen;
        titleBar.ButtonPressedForegroundColor = Colors.Gray;
        titleBar.ButtonPressedBackgroundColor = Colors.LightGreen;

        // Set inactive window colors
        titleBar.InactiveForegroundColor = Colors.Gainsboro;
        titleBar.InactiveBackgroundColor = Colors.SeaGreen;
        titleBar.ButtonInactiveForegroundColor = Colors.Gainsboro;
        titleBar.ButtonInactiveBackgroundColor = Colors.SeaGreen;
        return true;
    }
    return false;
}

```

#### Icon and system menu

You can also hide the system icon, or replace it with a custom icon.

To show or hide the system icon and the system menu, set the title bar [IconShowOptions](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions) property.

```csharp
titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
```

To use a custom icon, call the [AppWindow.SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon) method to set the new icon. The SetIcon(string) method currently only works with .ico files.

```csharp
m_AppWindow.SetIcon("iconPath/iconName.ico");
```

>**TODO: Need more info about SetString. Can only get it to work with a full path to the icon; are there other options? What about IconID?**

> **TODO: Talk about ResetToDefault?**

### [UWP/WinUI 2](#tab/winui2)

#### Title

By default, the title bar shows the app's display name as the window title. The display name is set in the ``Package.appxmanifest`` file.

To add custom text to the title, set the [ApplicationView.Title](/uwp/api/windows.ui.viewmanagement.applicationview.title) property to a text value.

```csharp
ApplicationView.GetForCurrentView().Title = "Custom text";
```

Your text is prepended to the window title, which will be displayed as "_custom text - app display name_". To show a custom title without the app display name, you have to replace the default title bar as shown in the _Full customization_ section.

#### Colors

This example shows how to get an instance of [ApplicationViewTitleBar](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar) and set its color properties.

```csharp
// using Windows.UI;
// using Windows.UI.ViewManagement;

var titleBar = ApplicationView.GetForCurrentView().TitleBar;

// Set active window colors
titleBar.ForegroundColor = Colors.White;
titleBar.BackgroundColor = Colors.Green;
titleBar.ButtonForegroundColor = Colors.White;
titleBar.ButtonBackgroundColor = Colors.SeaGreen;
titleBar.ButtonHoverForegroundColor = Colors.White;
titleBar.ButtonHoverBackgroundColor = Colors.DarkSeaGreen;
titleBar.ButtonPressedForegroundColor = Colors.Gray;
titleBar.ButtonPressedBackgroundColor = Colors.LightGreen;

// Set inactive window colors
titleBar.InactiveForegroundColor = Colors.Gainsboro;
titleBar.InactiveBackgroundColor = Colors.SeaGreen;
titleBar.ButtonInactiveForegroundColor = Colors.Gainsboro;
titleBar.ButtonInactiveBackgroundColor = Colors.SeaGreen;
```

---

> [!NOTE]
> This code can be placed in your app's [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method (_App.xaml.cs_), after the call to [Window.Activate](/uwp/api/windows.ui.xaml.window.Activate), or in your app's first page.

There are a few things to be aware of when setting title bar colors:

* The button background color is not applied to the close button hover and pressed states. The close button always uses the system-defined color for those states.
* Setting a color property to **null** resets it to the default system color.
* You can't set transparent colors. The color's alpha channel is ignored.

Windows gives a user the option to apply their selected [accent color](../style/color.md#accent-color) to the title bar. If you set any title bar color, we recommend that you explicitly set all the colors. This ensures that there are no unintended color combinations that occur because of user defined color settings.

## Full customization

When you opt-in to full title bar customization, your app's client area is extended to cover the entire window, including the title bar area. You are responsible for drawing and input-handling for the entire window except the caption buttons, which are overlaid on top of the app's canvas.

To hide the default title bar and extend your content into the title bar area, set the title bar property that extends app content to **true**.

### [Windows App SDK/WinUI 3](#tab/winui3)

This example shows how to get the [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) and set the [ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) property to **true**. This property can be set in your app's [OnLaunched](/windows/winui/api/microsoft.ui.xaml.application.onlaunched) method (_App.xaml.cs_), or in your app's first page.

```csharp
using Microsoft.UI;           // Needed for WindowId
using Microsoft.UI.Windowing; // Needed for AppWindow
using WinRT.Interop;          // Needed for XAML hwnd interop

private AppWindow m_AppWindow;

public MainWindow()
{
    this.InitializeComponent();

    _ = SetCustomTitleBar();
}

private bool SetCustomTitleBar()
{
    if (AppWindowTitleBar.IsCustomizationSupported())
    {
        // Get the AppWindow.
        if (m_AppWindow is null) m_AppWindow = GetAppWindowForCurrentWindow();
        // Get the TitleBar.
        var titleBar = m_AppWindow.TitleBar;
        // Extend the app content into the title bar area.        
        titleBar.ExtendsContentIntoTitleBar = true;
        // Make the caption buttons transparent.
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        
        // Set the drag region for the custom title bar.
        SetDragRegionForCustomTitleBar(m_AppWindow);
        // Handle window changes to recalculate the
        // drag regions when the size changes.
        m_AppWindow.Changed += AppWindow_Changed;
        return true;
    }
    return false;
}

private AppWindow GetAppWindowForCurrentWindow()
{
    IntPtr hWnd = WindowNative.GetWindowHandle(this);
    WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
    return AppWindow.GetFromWindowId(myWndId);
}
```

### [UWP/WinUI 2](#tab/winui2)

This example shows how to get the [CoreApplicationViewTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar) and set the [ExtendViewIntoTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.extendviewintotitlebar) property to **true**. This can be done in your app's [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method (_App.xaml.cs_), or in your app's first page.

```csharp
// using Windows.ApplicationModel.Core;

// Hide default title bar.
var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
coreTitleBar.ExtendViewIntoTitleBar = true;
```

> [!TIP]
> This setting persists when your app is closed and restarted. In Visual Studio, if you set ExtendViewIntoTitleBar to **true**, and then want to revert to the default, you should explicitly set it to **false** and run your app to overwrite the persisted setting.

---

### Title bar content and draggable regions

When your app is extended into the title bar area, you're responsible for defining and managing the UI for the title bar. This typically includes, at a minimum, specifying title text and the draggable region. The draggable region of the title bar defines where the user can click and drag to move the window around.

To learn more about acceptable title bar content and recommended UI patterns, see [Title bar design](/windows/apps/design/basics/titlebar-design).

### [Windows App SDK/WinUI 3](#tab/winui3)

When you're using the Windows App SDK, the draggable region is also where the user can right-click to show the system menu.

If your title bar content is not interactive, you should typically define the entire title bar area as the draggable region. This matches the behavior of the system title bar and is what users expect. If you add interactive content in the title bar area, such as buttons, menus, or a searchbox, you can define explicit drag regions around that content so that users can interact with it. The system does not reserve any drag regions and you are responsible for ensuring that there is enough room in your title bar for your users to move your window.

To set the draggable regions, call the [AppWindowTitleBar.SetDragRectangles](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles) method. This method takes an array of rectangles, each of which defines a draggable region.

When the size of the window changes, you need to update the draggable region to match the new size. You can handle the [AppWindow.Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event and check the event arg's [DidSizeChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didsizechange) property to respond to changes in the window size.

This example shows the custom title bar UI and how to calculate and set the draggable region for this title bar.

```xaml
 <Grid Grid.Row="0" x:Name="AppTitleBar" Height="32" 
       Background="{ThemeResource NavigationViewExpandedPaneBackground}">
     <Grid.ColumnDefinitions>
         <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
         <ColumnDefinition/>
         <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
     </Grid.ColumnDefinitions>
     <Image x:Name="MyWindowIcon" Source="/Assets/window_icon.png"
            Grid.Column="1"
            HorizontalAlignment="Left" VerticalAlignment="Center"
            Width="20" Height="20" Margin="12,0,0,0"/>
     <TextBlock x:Name="TitleTextBlock" 
                Text="Custom titlebar" 
                Style="{StaticResource CaptionTextBlockStyle}"
                Foreground="{ThemeResource SystemColorActiveCaptionBrush}"
                Grid.Column="1"
                VerticalAlignment="Center"
                Margin="44,0,0,0"/>
 </Grid>
```

```csharp
private void SetDragRegionForCustomTitleBar(AppWindow appWindow)
{
    // Get caption button occlusion information.
    int CaptionButtonOcclusionWidthRight = appWindow.TitleBar.RightInset;
    int CaptionButtonOcclusionWidthLeft = appWindow.TitleBar.LeftInset;

    // Set the width of padding columns in the UI.
    RightPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthRight);
    LeftPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthLeft);

    // Define your drag Regions
    int windowIconWidthAndPadding = (int)MyWindowIcon.ActualWidth 
                                  + (int)MyWindowIcon.Margin.Right;
    int dragRegionWidth = appWindow.Size.Width 
                        - (CaptionButtonOcclusionWidthRight 
                         + CaptionButtonOcclusionWidthLeft 
                         + windowIconWidthAndPadding);

    Windows.Graphics.RectInt32[] dragRects = new Windows.Graphics.RectInt32[] { };
    Windows.Graphics.RectInt32 dragRect;

    dragRect.X = windowIconWidthAndPadding;
    dragRect.Y = 0;
    dragRect.Height = (int)AppTitleBar.ActualHeight;
    dragRect.Width = dragRegionWidth;

    appWindow.TitleBar.SetDragRectangles(dragRects.Append(dragRect).ToArray());
}

private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
{
    if (args.DidSizeChange && sender.TitleBar.ExtendsContentIntoTitleBar)
    {
        // Need to update the drag region if the size of the window changes.
        SetDragRegionForCustomTitleBar(sender);
    }
}
```

> [!TIP]
> You can get the height of the system TitleBar (`int titleBarHeight = appWindow.TitleBar.Height;`) and use that to set the height of your custom title bar and drag regions. However, the [design guidance](/windows/apps/design/basics/titlebar-design) recommends setting the title bar height to 48px if you add other controls. In this case, the height of the system title bar will not match your content, so instead, use the [ActualHeight](/windows/winui/api/microsoft.ui.xaml.frameworkelement.actualheight) of the title bar element to set the drag region height.

### [UWP/WinUI 2](#tab/winui2)

You specify the draggable region by calling the [Window.SetTitleBar](/uwp/api/windows.ui.xaml.window.settitlebar) method and passing in a [UIElement]() that defines the draggable region. (The UIElement is often a panel that contains other elements.)

Here's how to set a Grid of content as the draggable title bar region. This code goes in the XAML and code-behind for your app's first page. See the [Full customization example](./title-bar.md#full-customization-example) section for the full code.

> **TODO: tile bar height(?)**

```xaml
<Grid x:Name="AppTitleBar" Background="Transparent">
    <!-- Width of the padding columns is set in LayoutMetricsChanged handler. -->
    <!-- Using padding columns instead of Margin ensures that the background
         paints the area under the caption control buttons (for transparent buttons). -->
    <Grid.ColumnDefinitions>
        <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
        <ColumnDefinition/>
        <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
    </Grid.ColumnDefinitions>
    <Image Source="Assets/Square44x44Logo.png" 
           Grid.Column="1" HorizontalAlignment="Left" 
           Width="20" Height="20" Margin="12,0"/>
    <TextBlock Text="Custom Title Bar" 
               Grid.Column="1" 
               Style="{StaticResource CaptionTextBlockStyle}" 
               Margin="44,8,0,0"/>
</Grid>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;
    coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
    // Set XAML element as a draggable region.
    Window.Current.SetTitleBar(AppTitleBar);
}

private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    AppTitleBar.Height = sender.Height;
}
```

> [!IMPORTANT]
> The draggable region you specify needs to be hit testable. By default, some UI elements, such as `Grid`, do not participate in hit testing when they don't have a background set. This means, for some elements, you might need to set a transparent background brush. See the remarks on [VisualTreeHelper.FindElementsInHostCoordinates](/uwp/api/windows.ui.xaml.media.visualtreehelper.findelementsinhostcoordinates) for more info.
>
>For example, if you define a Grid as your draggable region, set `Background="Transparent"` to make it draggable.
>
>This Grid is not draggable (but visible elements within it are): `<Grid x:Name="AppTitleBar">`.
>
>This Grid looks the same, but the whole Grid is draggable: `<Grid x:Name="AppTitleBar" Background="Transparent">`.

---

#### Multi-page apps

The UIElement (`AppTitleBar`) is part of the XAML for your app. You can either declare and set the title bar in a root page that doesn't change, or declare and set a title bar region in each page that your app can navigate to. If you set it in each page, you should make sure the draggable region stays consistent as a user navigates around your app.

#### Resetting the title bar

#### [Windows App SDK/WinUI 3](#tab/winui3)

To reset or switch to the system title bar while your app is running, you can call [AppWindowTitleBar.ResetToDefault](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.resettodefault).

```csharp
m_AppWindow.TitleBar?.ResetToDefault();
```

#### [UWP/WinUI 2](#tab/winui2)

You can call SetTitleBar to switch to a new title bar element while your app is running. You can also pass **null** as the parameter to SetTitleBar to revert to the default dragging behavior. (See "Default draggable region" for more info.)

---

#### Default draggable region

#### [Windows App SDK/WinUI 3](#tab/winui3)

The system does not reserve any default drag regions. You are responsible for ensuring that there is enough room in your title bar for your users to move your window, and that the drag regions are defined appropriately.

#### [UWP/WinUI 2](#tab/winui2)

If you don't specify a draggable region, a rectangle that is the width of the window, the height of the caption buttons, and positioned along the top edge of the window is set as the default draggable region.

If you do define a draggable region, the system shrinks the default draggable region down to a small area the size of a caption button, positioned to the left of the caption buttons (or to the right if the captions buttons are on the left side of the window). This ensures that there is always a consistent area the user can drag.

> **TODO: Image of system reserved drag region.**

---

#### System caption buttons

The system reserves the upper-left or upper-right corner of the app window for the system caption buttons (Minimize, Maximize, Close). The system retains control of the caption button area to guarantee that minimum functionality is provided for dragging, minimizing, maximizing, and closing the window. The system draws the Close button in the upper-right for left-to-right languages and the upper-left for right-to-left languages.

You can draw content underneath the caption control area, such as your app background, but you should not put any UI that you expect the user to be able to interact with. It does not receive any input because input for the caption controls is handled by the system.

These lines from the previous example show the padding columns in the XAML that defines the title bar. Using padding columns instead of margins ensures that the background paints the area under the caption control buttons (for transparent buttons). Using both right and left padding columns ensures that your title bar behaves correctly in both right-to-left and left-to-right layouts.

```xaml
<Grid.ColumnDefinitions>
    <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
    <ColumnDefinition/>
    <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
</Grid.ColumnDefinition
```

> **TODO: tile bar height(?)**

#### [Windows App SDK/WinUI 3](#tab/winui3)

The dimensions and position of the caption control area is communicated by the AppWindowTitleBar class so that you can account for it in the layout of your title bar UI. The width of the reserved region on each side is given by the [LeftInset](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.leftinset) or [RightInset](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.rightinset) properties, and its height is given by the [Height](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height) property.

Here's how the width of the padding columns is specified when the drag regions are calculated and set.

```csharp
// Get caption button occlusion information.
int CaptionButtonOcclusionWidthRight = appWindow.TitleBar.RightInset;
int CaptionButtonOcclusionWidthLeft = appWindow.TitleBar.LeftInset;

// Set the width of padding columns in the UI.
RightPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthRight);
LeftPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthLeft);
```

#### [UWP/WinUI 2](#tab/winui2)

The dimensions and position of the caption control area is communicated by the CoreApplicationViewTitleBar class so that you can account for it in the layout of your title bar UI. The width of the reserved region on each side is given by the [SystemOverlayLeftInset](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.SystemOverlayLeftInset) or [SystemOverlayRightInset](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.SystemOverlayRightInset) properties, and its height is given by the [Height](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.Height) property.

You can handle the [LayoutMetricsChanged](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.LayoutMetricsChanged) event to respond to changes in the size of the caption buttons. For example, this can happen when the system back button is shown or hidden. Handle this event to verify and update the positioning of UI elements that depend on the title bar's size.

This example shows how to adjust the layout of your title bar to account for changes like the system back button being shown or hidden. `AppTitleBar`, `LeftPaddingColumn`, and `RightPaddingColumn` are declared in the XAML shown previously.

```csharp
private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    UpdateTitleBarLayout(sender);
}

private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
{
    // Get the size of the caption controls area and back button 
    // (returned in logical pixels), and move your content around as necessary.
    LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
    RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);

    // Update title bar control size as needed to account for system size changes.
    AppTitleBar.Height = coreTitleBar.Height;
}
```

---

### Interactive content

You can place interactive controls, like buttons, menus, or a search box, in the top part of the app so they appear to be in the title bar. However, there are a few rules you must follow to ensure that your interactive elements receive user input while users are still able to move your window around.

### [Windows App SDK/WinUI 3](#tab/winui3)

> **TODO: `Refer back to setting drag rectangles. Provide example that shows adding a reserved drag region (for when the window shrinks), and calculating rectangles around the interactive elements.`**

### [UWP/WinUI 2](#tab/winui2)

* You must call SetTitleBar to define an area as the draggable title bar region. If you don't, the system sets the default draggable region at the top of the page. The system will then handle all user input to this area, and prevent input from reaching your controls.
* Place your interactive controls over the top of the draggable region defined by the call to SetTitleBar (with a higher z-order). Don't make your interactive controls children of the UIElement passed to SetTitleBar. After you pass an element to SetTitleBar, the system treats it like the system title bar and handles all pointer input to that element.

Here, the `TitleBarButton` element has a higher z-order than `AppTitleBar`, so it receives user input.

```xaml
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition />
    </Grid.RowDefinitions>

    <Grid x:Name="AppTitleBar" Background="Transparent">
        <!-- Width of the padding columns is set in LayoutMetricsChanged handler. -->
        <!-- Using padding columns instead of Margin ensures that the background
             paints the area under the caption control buttons (for transparent buttons). -->
        <Grid.ColumnDefinitions>
            <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
            <ColumnDefinition/>
            <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
        </Grid.ColumnDefinitions>
        <Image Source="Assets/Square44x44Logo.png"
               Grid.Column="1" HorizontalAlignment="Left"
               Width="20" Height="20" Margin="12,0"/>
        <TextBlock Text="Custom Title Bar"
                   Grid.Column="1"
                   Style="{StaticResource CaptionTextBlockStyle}"
                   Margin="44,8,0,0"/>
    </Grid>

    <!-- This Button has a higher z-order than AppTitleBar, 
         so it receives user input. -->
    <Button x:Name="TitleBarButton" Content="Button in the title bar"
        HorizontalAlignment="Right"/>
</Grid>
```

---

### Transparency in caption buttons

When you extend your app content into the title bar area, you can make the background of the caption buttons transparent to let your app background show through. You typically set the background to `Colors.Transparent` for full transparency. For partial transparency, set the alpha channel for the `Color` you set the property to.

These title bar properties can be transparent:

### [Windows App SDK/WinUI 3](#tab/winui3)

* [ButtonBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonbackgroundcolor)
* [ButtonHoverBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonhoverbackgroundcolor)
* [ButtonPressedBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonpressedbackgroundcolor)
* [ButtonInactiveBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttoninactivebackgroundcolor)

All other color properties will continue to ignore the alpha channel. If ExtendsContentIntoTitleBar is set to **false**, the alpha channel is always ignored for all AppWindowTitleBar color properties.

Reference: [Colors.Transparent](/windows/winui/api/microsoft.ui.colors), [ColorHelper](/windows/winui/api/microsoft.ui.colorhelper)

### [UWP/WinUI 2](#tab/winui2)

* ButtonBackgroundColor(/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonbackgroundcolor)
* ButtonHoverBackgroundColor(/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonhoverbackgroundcolor)
* ButtonPressedBackgroundColor(/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonpressedbackgroundcolor)
* ButtonInactiveBackgroundColor(/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttoninactivebackgroundcolor)

All other color properties will continue to ignore the alpha channel. If ExtendViewIntoTitleBar is set to **false**, the alpha channel is always ignored for all ApplicationViewTitleBar color properties.

Reference: [Colors.Transparent](/uwp/api/windows.ui.colors.Transparent), [Color](/uwp/api/windows.ui.color)

---

The button background color is not applied to the Close button _hover_ and _pressed_ states. The close button always uses the system-defined color for those states.

> [!TIP]
> [Mica](/windows/apps/design/style/mica) is a delightful [material](/windows/apps/design/signature-experiences/materials) that helps distinguish the window thats in focus. We recommend it as the background for long-lived windows in Windows 11. If you have applied mica in the client area of your window, you can extend it into the titlebar area and make your caption buttons transparent for the mica to show through. See [Mica material](/windows/apps/design/style/mica) for more info.

### Show and hide the title bar

When your app runs in _full screen_ or [_tablet mode_ (Windows 10 only)](/windows-hardware/design/device-experiences/continuum), the system hides the title bar and caption control buttons. However, the user can invoke the title bar to have it shown as an overlay on top of the app's UI.

### [Windows App SDK/WinUI 3](#tab/winui3)

> **TODO: `Does this work the same way in an App SDK window? Need code example for the best way to handle this. Handle WindowChanged and check for full screen presenter?`**

>[!NOTE]
>_Full screen_ mode can be entered only if supported by your app. See [Manage app windows](/windows/apps/windows-app-sdk/windowing/windowing-overview) and [FullScreenPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.fullscreenpresenter) for more info.

### [UWP/WinUI 2](#tab/winui2)

You can handle the [CoreApplicationViewTitleBar.IsVisibleChanged](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.IsVisibleChanged) event to be notified when the title bar is hidden or invoked, and show or hide your custom title bar content as needed.

This example shows how to handle IsVisibleChanged to show and hide the `AppTitleBar` element shown previously.

```csharp
public MainPage()
{
    this.InitializeComponent();

    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

    // Register a handler for when the title bar visibility changes.
    // For example, when the title bar is invoked in full screen mode.
    coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
}

private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
{
    if (sender.IsVisible)
    {
        AppTitleBar.Visibility = Visibility.Visible;
    }
    else
    {
        AppTitleBar.Visibility = Visibility.Collapsed;
    }
}
```

>[!NOTE]
>_Full screen_ mode can be entered only if supported by your app. See [ApplicationView.IsFullScreenMode](/uwp/api/windows.ui.viewmanagement.applicationview.IsFullScreenMode) for more info. [_Tablet mode_ (Windows 10 only)](/windows-hardware/design/device-experiences/continuum) is a user option in Windows 10 on supported hardware, so a user can choose to run any app in tablet mode.

---

## Do's and don'ts

* Do make it obvious when your window is active or inactive. At a minimum, change the color of the text, icons, and buttons in your title bar.
* Do define a draggable region along the top edge of the app canvas. Matching the placement of system title bars makes it easier for users to find.
* Do define a draggable region that matches the visual title bar (if any) on the app's canvas.

## Full customization example

This examples shows all the code described in the Full customization section.

### [Windows App SDK/WinUI 3](#tab/winui3)

```xaml

```

```csharp

```

### [UWP/WinUI 2](#tab/winui2)

```xaml
<Page
    x:Class="CustomTitleBar.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:CustomTitleBar"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition />
        </Grid.RowDefinitions>

        <Grid x:Name="AppTitleBar" Background="Transparent">
            <!-- Width of the padding columns is set in LayoutMetricsChanged handler. -->
            <!-- Using padding columns instead of Margin ensures that the background
                 paints the area under the caption control buttons (for transparent buttons). -->
            <Grid.ColumnDefinitions>
                <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
                <ColumnDefinition/>
                <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
            </Grid.ColumnDefinitions>
            <Image Source="Assets/Square44x44Logo.png" 
                   Grid.Column="1" HorizontalAlignment="Left" 
                   Width="20" Height="20" Margin="12,0"/>
            <TextBlock Text="Custom Title Bar" 
                       Grid.Column="1" 
                       Style="{StaticResource CaptionTextBlockStyle}" 
                       Margin="44,8,0,0"/>
        </Grid>

        <!-- This Button has a higher z-order than MyTitleBar, 
             so it receives user input. -->
        <Button x:Name="TitleBarButton" Content="Button in the title bar"
                HorizontalAlignment="Right"/>
    </Grid>
</Page>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    // Hide default title bar.
    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;
    UpdateTitleBarLayout(coreTitleBar);

    // Set XAML element as a draggable region.
    Window.Current.SetTitleBar(AppTitleBar);

    // Register a handler for when the size of the overlaid caption control changes.
    // For example, when the app moves to a screen with a different DPI.
    coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

    // Register a handler for when the title bar visibility changes.
    // For example, when the title bar is invoked in full screen mode.
    coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
}

private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    UpdateTitleBarLayout(sender);
}

private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
{
    // Get the size of the caption controls area and back button 
    // (returned in logical pixels), and move your content around as necessary.
    LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
    RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
    TitleBarButton.Margin = new Thickness(0,0,coreTitleBar.SystemOverlayRightInset,0);

    // Update title bar control size as needed to account for system size changes.
    AppTitleBar.Height = coreTitleBar.Height;
}

private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
{
    if (sender.IsVisible)
    {
        AppTitleBar.Visibility = Visibility.Visible;
    }
    else
    {
        AppTitleBar.Visibility = Visibility.Collapsed;
    }
}
```

---

## Related articles

* [Acrylic](../style/acrylic.md)
* [Color](../style/color.md)
