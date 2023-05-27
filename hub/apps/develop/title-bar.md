---
description: Customize the title bar of a desktop app to match the personality of the app.
title: Title bar customization
template: detail.hbs
ms.date: 05/27/2023
ms.topic: article
keywords: windows 10, uwp, title bar
doc-status: Draft
ms.localizationpriority: medium
ms.author: jimwalk
author: jwmsft
---
# Title bar customization

Windows provides a default title bar for every window and lets you to customize it to match the personality of your app. The default title bar comes with some standard components and core functionality such as dragging and resizing the window.

:::image type="content" source="images/titlebar-overview.png" alt-text="A Windows app showing the title bar" border="false":::

See the [Title bar](../design/basics/titlebar-design.md) design article for guidance on customizing your app's title bar, acceptable title bar area content, and recommended UI patterns.

> [!div class="nextstepaction"]
> [See the Windows 11 Fluent Design guidance for title bar](../design/basics/titlebar-design.md)

## Title bar components

This list describes the components of the standard title bar.

- Title bar rectangle
- Title text
- System icon (except for UWP)
- System menu - accessed by clicking the app icon or right-clicking the title bar
- Caption controls
  - Minimize button
  - Maximize/Restore button
  - Close button

## Platform options

The exact features of the title bar and the options available to customize it depend on your UI platform and app requirements. This article shows how to customize the title bar for apps that use either the Windows App SDK, WinUI 3, or UWP with WinUI 2.

> [!NOTE]
> For a detailed comparison of the windowing models used by the Windows App SDK and UWP, see [Windowing functionality migration](../windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing.md).

### [Windows App SDK](#tab/wasdk)

> [!div class="checklist"]
>
> - **Applies to**: Windows App SDK
> - **Important APIs**: [AppWindow.TitleBar property](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.titlebar), [AppWindowTitleBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar), [AppWindow class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow)

Windowing functionality in the [Windows App SDK](./index.md) is through the [Microsoft.UI.Windowing.AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class, which is based on the Win32 HWND model. There's a 1:1 mapping between an AppWindow and a top-level HWND in your app. AppWindow and its related classes provide APIs that let you manage many aspects of your app's top-level windows, including customization of the title bar. You can modify the default title bar that Windows provides so that it blends with the rest of your UI, or extend your app canvas into the title bar area and provide your own title bar content.

> [!IMPORTANT]
> Support for title bar customization APIs varies across different versions of Windows and different versions of Windows App SDK. This table describes the details.
>
> | Customization option | Windows 10 | Windows 11 |
> | - | - | - |
> | Simple customization | Partially, since Windows App SDK 1.2 (Color customization is not supported) | Yes, all versions of Windows App SDK |
> | Full customization | Yes, since Windows App SDK 1.2 | Yes, all versions of Windows App SDK |
>
> For information on which APIs are supported on Windows 10 since Windows App SDK 1.2, refer to the [Windows App SDK Release Notes](/windows/apps/windows-app-sdk/stable-channel#version-12-stable) page under "Windowing" section for details.
>
> We recommend that you check [AppWindowTitleBar.IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) in your code before you call these APIs to ensure your app doesn't crash on other versions of Windows.

For XAML apps that use WinUI 3, XAML Window APIs provide a simpler way to customize the title bar that also works on Windows 10. These APIs can be used in conjunction with the Windows App SDK APIs (see the WinUI 3 tab).

### How to work with AppWindow

You can use AppWindow APIs with any UI framework that the Windows App SDK supports - Win32, WPF, WinForms, or WinUI 3 - and you can adopt them incrementally, using only the APIs you need. You get an AppWindow object from an existing window using the interop APIs. With this AppWindow object you have access to the title bar customization APIs. For more about the interop APIs, see [Manage app windows - UI framework and HWND interop](../windows-app-sdk/windowing/windowing-overview.md#ui-framework-and-hwnd-interop) and the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing).

### [WinUI 3](#tab/winui3)

> [!div class="checklist"]
>
> - **Applies to**: WinUI 3 desktop apps
> - **Important APIs**: [Microsoft.UI.Xaml.Window class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window), [Window.ExtendsContentIntoTitleBar property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.extendscontentintotitlebar), [Window.SetTitleBar method](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.settitlebar)

Windowing functionality in [WinUI 3](./index.md) is through the [Microsoft.UI.Xaml.Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) class, which is based on the Win32 HWND model. The Window class includes APIs that let you replace the standard title bar with your own custom content.

WinUI 3 is also part of the Windows App SDK, so both the Window class and the AppWindow class are available to customize the title bar. You can pass the window handle of the XAML Window to the AppWindow object and use the AppWindow functionality in conjunction with the Window APIs (see the Windows App SDK tab).

This table describes the differences between Window and AppWindow.

| Feature | Window class | AppWindow class |
| - | - | - |
| Windows 10 support | Yes | Partially, since Windows App SDK 1.2 (see the Windows App SDK tab). |
| Simple customization | Title | Title, Colors, Icon and System menu |
| Replace system title bar | Window.ExtendsContentIntoTitleBar | AppWindowTitleBar.ExtendsContentIntoTitleBar |
| Set title bar content | Define your title bar in a XAML UIElement, then call SetTitleBar(UIElement). | Write custom code to calculate and set drag rectangles, including when the window size changes. |
| Set caption button colors | No | Yes |
| Caption button size information | N/A | RightInset and LeftInset properties |

### [UWP/WinUI 2](#tab/winui2)

> [!div class="checklist"]
>
> - **Applies to**: UWP/WinUI 2
> - **Important APIs**: [ApplicationView.TitleBar property](/uwp/api/windows.ui.viewmanagement.applicationview), [ApplicationViewTitleBar class](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar), [CoreApplicationViewTitleBar class](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar)

In UWP applications, you can customize the title bar by using members of the ApplicationView and CoreApplicationView classes. There are multiple APIs to progressively modify the appearance of your title bar based on the level of customization needed.

> [!NOTE]
> The [Windows.UI.WindowManagement.AppWindow](/uwp/api/windows.ui.windowmanagement.appwindow) class, used for secondary windows in UWP apps, does not support title bar customization. To customize the title bar of a UWP app that uses secondary windows, use ApplicationView as described in [Show multiple views with ApplicationView](../design/layout/application-view.md).

If you are considering migrating your UWP app to Windows App SDK, please view our windowing functionality migration guide. See [Windowing functionality migration](../windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing.md) for more information.

---

## How much to customize the title bar

There are two levels of customization that you can apply to the title bar: apply minor modifications to the default title bar, or extend your app canvas into the title bar area and provide completely custom content.

### Simple

Simple customization is only available for Windows App SDK and UWP/WinUI 2.

For simple customization, such as changing the title bar color, you can set properties on your app window's title bar object to specify the colors you want to use for title bar elements. In this case, the system retains responsibility for all other aspects of the title bar, such as drawing the app title and defining drag areas.

### Full

Your other option is to hide the default title bar and replace it with your own custom content. For example, you can place text, a search box, or custom menus in the title bar area. You will also need to use this option to extend a [material](../design/signature-experiences/materials.md) backdrop, like [Mica](../design/style/mica.md), into the title bar area.

When you opt for full customization, you are responsible for putting content in the title bar area, and you can define your own drag region. The caption controls (system Close, Minimize, and Maximize buttons) are still available and handled by the system, but elements like the app title are not. You will need to create those elements yourself as needed by your app.

## Simple customization

If you want only to customize the title bar colors or icon, you can set properties on the title bar object for your app window.

### [Windows App SDK](#tab/wasdk)

> (Partially supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

These examples show how to get an instance of [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) and set its properties.

#### Title

By default, the title bar shows the app type as the window title (for example, "WinUI Desktop"). To change the window title, set the [AppWindow.Title](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.title) property to a single-line text value.

```csharp
using Microsoft.UI;           // Needed for WindowId.
using Microsoft.UI.Windowing; // Needed for AppWindow.
using WinRT.Interop;          // Needed for XAML/HWND interop.

private AppWindow m_AppWindow;

public MainWindow()
{
    this.InitializeComponent();

    m_AppWindow = GetAppWindowForCurrentWindow();
    m_AppWindow.Title = "App title";
}

private AppWindow GetAppWindowForCurrentWindow()
{
    IntPtr hWnd = WindowNative.GetWindowHandle(this);
    WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
    return AppWindow.GetFromWindowId(wndId);
}
```

#### Colors

This example shows how to get an instance of [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) and set its color properties.

```csharp
private bool SetTitleBarColors()
{
    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (AppWindowTitleBar.IsCustomizationSupported())
    {
        if (m_AppWindow is null)
        {
            m_AppWindow = GetAppWindowForCurrentWindow();
        }
        var titleBar = m_AppWindow.TitleBar;

        // Set active window colors
        // Note: No effect when app is running on Windows 10 since color customization is not
        // supported.
        titleBar.ForegroundColor = Colors.White;
        titleBar.BackgroundColor = Colors.Green;
        titleBar.ButtonForegroundColor = Colors.White;
        titleBar.ButtonBackgroundColor = Colors.SeaGreen;
        titleBar.ButtonHoverForegroundColor = Colors.Gainsboro;
        titleBar.ButtonHoverBackgroundColor = Colors.DarkSeaGreen;
        titleBar.ButtonPressedForegroundColor = Colors.Gray;
        titleBar.ButtonPressedBackgroundColor = Colors.LightGreen;

        // Set inactive window colors
        // Note: No effect when app is running on Windows 10 since color customization is not
        // supported.
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

You can also hide the system icon, or replace it with a custom icon. The system icon shows the system menu when right clicked or tapped once. It closes the window when double clicked/tapped.

To show or hide the system icon and associated behaviors, set the title bar [IconShowOptions](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions) property.

```csharp
titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
```

> [!NOTE]
> The `IconShowOptions` enumeration allows the possibility of other options being added in future releases. You can give us feedback on the [Windows App SDK repository on GitHub]() if this is of interest to you.

To use a custom window icon, call one of the [AppWindow.SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon) methods to set the new icon.

- `SetIcon(String)`

  The [SetIcon(String)](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon#Microsoft_UI_Windowing_AppWindow_SetIcon_System_String_) method currently works only with .ico files. The string you pass to this method is the fully qualified path to the .ico file.

  ```csharp
  m_AppWindow.SetIcon("iconPath/iconName.ico");
  ```

- `SetIcon(IconId)`

  If you already have a handle to an icon (`HICON`) from one of the [Icon functions](/windows/win32/menurc/icon-functions) like [CreateIcon](/windows/win32/api/winuser/nf-winuser-createicon), you can use the [GetIconIdFromIcon](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-geticonidfromicon) interop API to get an [IconId](/windows/windows-app-sdk/api/winrt/microsoft.ui.iconid). You can then pass the `IconId` to the [SetIcon(IconId)](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon#Microsoft_UI_Windowing_AppWindow_SetIcon_Microsoft_UI_IconId_) method to set your window icon.  

  ```csharp
  m_AppWindow.SetIcon(iconId));
  ```

### [WinUI 3](#tab/winui3)

#### Title

By default, the title bar shows the app's display name as the window title. The display name is set in the `Package.appxmanifest` file.

To replace the title with custom text, set the [Window.Title](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.title) property to a text value, as shown here.

```csharp
public MainWindow()
{
    InitializeComponent();
    Title = "App title";
}
```

```xaml
<Window
    ...
    Title="App title">
    ...
</Window>
```

> [!NOTE]
> To add color to the default title bar or to change the window icon that comes with a WinUI 3 window, you will need to use the Windows App SDK AppWindow APIs or opt to fully customize your titlebar.

### [UWP/WinUI 2](#tab/winui2)

#### Title

By default, the title bar shows the app's display name as the window title. The display name is set in the `Package.appxmanifest` file.

To add custom text to the title, set the [ApplicationView.Title](/uwp/api/windows.ui.viewmanagement.applicationview.title) property to a text value, as shown here.

```csharp
public MainPage()
{
    this.InitializeComponent();

    ApplicationView.GetForCurrentView().Title = "Custom text";
}
```

Your text is prepended to the window title, which will be displayed as "_custom text - app display name_". To show a custom title without the app display name, you have to replace the default title bar as shown in the [Full customization](#full-customization) section.

#### Colors

This example shows how to get an instance of [ApplicationViewTitleBar](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar) and set its color properties.

This code can be placed in your app's [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method (_App.xaml.cs_), after the call to [Window.Activate](/uwp/api/windows.ui.xaml.window.Activate), or in your app's first page.

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

There are a few things to be aware of when setting title bar colors:

- The button background color is not applied to the close button _hover_ and _pressed_ states. The close button always uses the system-defined color for those states.
- Setting a color property to `null` resets it to the default system color.
- You can't set transparent colors. The color's alpha channel is ignored.

Windows gives a user the option to apply their selected [accent color](../design/style/color.md#accent-color) to the title bar. If you set any title bar color, we recommend that you explicitly set all the colors. This ensures that there are no unintended color combinations that occur because of user defined color settings.

## Full customization

When you opt-in to full title bar customization, your app's client area is extended to cover the entire window, including the title bar area. You are responsible for drawing and input-handling for the entire window except the caption buttons, which are still provided by the window.

To hide the default title bar and extend your content into the title bar area, set the property that extends app content into the title bar area to `true`. In a XAML app, this property can be set in your app's `OnLaunched` method (_App.xaml.cs_), or in your app's first page.

> [!TIP]
> See the [Full customization example](./title-bar.md#full-customization-example) section to see all the code at once.

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

This example shows how to get the [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) and set the [ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) property to `true`.

> [!IMPORTANT]
> Title bar customization APIs are not supported on all versions of Windows where your app might run, so be sure to check [AppWindowTitleBar.IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) in your code before you call these APIs. If title bar customization is not supported, you will typically hide your custom title bar UI by setting `Visibility` to `Collapsed`.

```csharp
using Microsoft.UI;           // Needed for WindowId
using Microsoft.UI.Windowing; // Needed for AppWindow
using WinRT.Interop;          // Needed for XAML/HWND interop

private AppWindow m_AppWindow;

public MainWindow()
{
    this.InitializeComponent();

    m_AppWindow = GetAppWindowForCurrentWindow();
    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (AppWindowTitleBar.IsCustomizationSupported())
    {
        var titleBar = m_AppWindow.TitleBar;
        // Hide default title bar.
        titleBar.ExtendsContentIntoTitleBar = true;
    }
    else
    {
        // In the case that title bar customization is not supported, hide the custom title bar
        // element.
        AppTitleBar.Visibility = Visibility.Collapsed;
    }
}

private AppWindow GetAppWindowForCurrentWindow()
{
    IntPtr hWnd = WindowNative.GetWindowHandle(this);
    WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
    return AppWindow.GetFromWindowId(wndId);
}
```

### [WinUI 3](#tab/winui3)

This example shows how to set the [Window.ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.extendscontentintotitlebar) property to `true`.

```csharp
public MainWindow()
{
    this.InitializeComponent();

    // Hide default title bar.
    ExtendsContentIntoTitleBar = true;
}
```

> [!CAUTION]
> `ExtendsContentIntoTitleBar` shows in the XAML IntelliSense for `Window`, but setting it in XAML causes an error. Set this property in code instead.

> [!WARNING]
> If you set `ExtendsContentIntoTitleBar=true` without specifying a drag region by calling `SetTitleBar` with a `UIElement` the application will create a fallback title bar that only contains the minimum drag region and caption controls.

### [UWP/WinUI 2](#tab/winui2)

This example shows how to get the [CoreApplicationViewTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar) and set the [ExtendViewIntoTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.extendviewintotitlebar) property to `true`.

```csharp
using Windows.ApplicationModel.Core;

public MainPage()
{
    this.InitializeComponent();

    // Hide default title bar.
    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;
}
```

> [!TIP]
> This setting persists when your app is closed and restarted. In Visual Studio, if you set `ExtendViewIntoTitleBar` to `true`, and then want to revert to the default, you should explicitly set it to `false` and run your app to overwrite the persisted setting.

---

### Title bar content and drag regions

When your app is extended into the title bar area, you're responsible for defining and managing the UI for the title bar. This typically includes, at a minimum, specifying title text and the drag region. The drag region of the title bar defines where the user can click and drag to move the window around. It's also where the user can right-click to show the system menu.

To learn more about acceptable title bar content and recommended UI patterns, see [Title bar design](../design/basics/titlebar-design.md).

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

When you extend your content into the title bar area, the system by default retains the entire title bar area except for the caption buttons as the drag region. If you don't place interactive content in your title bar, you can leave this default drag region as-is. If you place interactive content in your title bar, you need to specify the drag regions, which we cover in the next section.

This example shows the XAML for a custom title bar UI without interactive content.

```xaml
<Grid x:Name="AppTitleBar"  
      Height="32">
    <Grid.ColumnDefinitions>
        <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
        <ColumnDefinition/>
        <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
    </Grid.ColumnDefinitions>
    <Image x:Name="TitleBarIcon" Source="/Images/WindowIcon.png"
           Grid.Column="1"
           HorizontalAlignment="Left"
           Width="16" Height="16"
           Margin="8,0,0,0"/>
    <TextBlock x:Name="TitleTextBlock" 
               Text="App title" 
               Style="{StaticResource CaptionTextBlockStyle}"
               Grid.Column="1"
               VerticalAlignment="Center"
               Margin="28,0,0,0"/>
</Grid>
```

> [!IMPORTANT]
> The `LeftPaddingColumn` and `RightPaddingColumn` are used to reserve space for the caption buttons. The `Width` values for these columns are set in code, which is shown later. See the [_System caption buttons_](#system-caption-buttons) section for the code and explanation.

### [WinUI 3](#tab/winui3)

You specify the drag region by calling the [Window.SetTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.settitlebar) method and passing in a [UIElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement) that defines the drag region. (The `UIElement` is typically a panel that contains other elements.) The `ExtendsContentIntoTitleBar` property must be set to `true` in order for the call to `SetTitleBar` to have any effect.

Here's how to set a `Grid` of content as the draggable title bar region. This code goes in the XAML and code-behind for your app's first page.

```xaml
<Grid x:Name="AppTitleBar">
    <Image Source="Images/WindowIcon.png"
           HorizontalAlignment="Left" 
           Width="16" Height="16" 
           Margin="8,0"/>
    <TextBlock x:Name="AppTitleTextBlock" Text="App title"
               TextWrapping="NoWrap"
               Style="{StaticResource CaptionTextBlockStyle}" 
               VerticalAlignment="Center"
               Margin="28,0,0,0"/>
</Grid>
```

```csharp
public MainWindow()
{
    this.InitializeComponent();

    ExtendsContentIntoTitleBar = true;
    SetTitleBar(AppTitleBar);
}
```

By default, the system title bar shows the app's display name as the window title. The display name is set in the Package.appxmanifest file. You can get this value and use it in your custom title bar like this.

```csharp
AppTitleTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
```

### [UWP/WinUI 2](#tab/winui2)

You specify the drag region by calling the [Window.SetTitleBar](/uwp/api/windows.ui.xaml.window.settitlebar) method and passing in a [UIElement](/uwp/api/windows.ui.xaml.uielement) that defines the drag region. (The `UIElement` is typically a panel that contains other elements.) The `ExtendViewIntoTitleBar` property must be set to `true` in order for the call to `SetTitleBar` to have any effect.

Here's how to set a `Grid` of content as the draggable title bar region. This code goes in the XAML and code-behind for your app's first page.

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
    <Image Source="Assets/WindowIcon.png" 
           Grid.Column="1"
           HorizontalAlignment="Left"
           Width="16" Height="16"
           Margin="8,0,0,0"/>
    <TextBlock x:Name="AppTitleTextBlock"
               Text="App title" 
               Style="{StaticResource CaptionTextBlockStyle}" 
               Grid.Column="1"
               VerticalAlignment="Center"
               Margin="28,0,0,0"/>
</Grid>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;

    // Set XAML element as a drag region.
    Window.Current.SetTitleBar(AppTitleBar);
}
```

By default, the system title bar shows the app's display name as the window title. The display name is set in the Package.appxmanifest file. You can get this value and use it in your custom title bar like this.

```csharp
AppTitleTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
```

> [!IMPORTANT]
> The drag region you specify needs to be hit testable. By default, some UI elements, such as `Grid`, do not participate in hit testing when they don't have a background set. This means, for some elements, you might need to set a transparent background brush. See the remarks on [VisualTreeHelper.FindElementsInHostCoordinates](/uwp/api/windows.ui.xaml.media.visualtreehelper.findelementsinhostcoordinates) for more info.
>
>For example, if you define a Grid as your drag region, set `Background="Transparent"` to make it draggable.
>
>This Grid is not drag (but visible elements within it are): `<Grid x:Name="AppTitleBar">`.
>
>This Grid looks the same, but the whole Grid is draggable: `<Grid x:Name="AppTitleBar" Background="Transparent">`.

---

### Interactive content

You can place interactive controls, like buttons, menus, or a search box, in the top part of the app so they appear to be in the title bar. However, there are a few rules you must follow to ensure that your interactive elements receive user input while still allowing users to move your window around.

:::image type="content" source="images/titlebar-search.png" alt-text="A Windows app with a search box in the title bar" border="false":::

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

If you add interactive content in the title bar area, you should define explicit drag regions around that content so that users can interact with it. After you set a custom drag region, the default drag region is removed and the system does not reserve any mandatory drag region. You are responsible for ensuring that there is enough space in your title bar for your users to move your window.

To set the drag regions, call the [AppWindowTitleBar.SetDragRectangles](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles) method. This method takes an array of rectangles, each of which defines a drag region. When the size of the title bar changes, you need to recalculate the drag regions to match the new size, and call `SetDragRectangles` with the new values.

Your custom title bar will not be shown if it's not supported on the system where it's run. You should provide an alternative UI for any functionality that you placed in your custom title bar.

This example shows a custom title bar UI with a search box and demonstrates how to calculate and set the drag rectangles on either side of the search box. Here are a few important points to notice in the code.

- Set the `AppTitleBar` Grid height to 48 to follow the [Title bar](../design/basics/titlebar-design.md) design guidance for interactive content.
- To make calculating the drag rectangles easier, use a `Grid` with multiple named columns for the layout.
- Set `ExtendsContentIntoTitleBar` to `true` in the MainWindow constructor. If you set it in code that gets called later, the default system title bar might be shown first, and then hidden.
- Make the initial call to calculate drag regions after the `AppTitleBar` element has loaded (`AppTitleBar_Loaded`). Otherwise, there's no guarantee that the elements used for the calculation will have their correct values.
- Update the drag rectangle calculations only after the `AppTitleBar` element has changed size (`AppTitleBar_SizeChanged`). If you depend on the window `Changed` event, there will be situations (like window maximize/minimize) where the event occurs before `AppTitleBar` is resized and the calculations will use incorrect values.
- Call `SetDragRectangles` only after you check `IsCustomizationSupported` and `ExtendsContentIntoTitleBar` to confirm that a custom title bar is supported and being used.

```xaml
<Grid x:Name="AppTitleBar"  
      Height="48">
    <Grid.ColumnDefinitions>
        <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
        <ColumnDefinition x:Name="IconColumn" Width="Auto"/>
        <ColumnDefinition x:Name="TitleColumn" Width="Auto"/>
        <ColumnDefinition x:Name="LeftDragColumn" Width="*"/>
        <ColumnDefinition x:Name="SearchColumn" Width="Auto"/>
        <ColumnDefinition x:Name="RightDragColumn" Width="*"/>
        <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
    </Grid.ColumnDefinitions>
    <Image x:Name="TitleBarIcon" Source="/Images/WindowIcon.png"
           Grid.Column="1"
           Width="16" Height="16"
           Margin="8,0,0,0"/>
    <TextBlock x:Name="TitleTextBlock" 
               Text="App title" 
               Style="{StaticResource CaptionTextBlockStyle}"
               Grid.Column="2"
               VerticalAlignment="Center"
               Margin="4,0,0,0"/>
    <AutoSuggestBox Grid.Column="4" QueryIcon="Find"
                    PlaceholderText="Search"
                    VerticalAlignment="Center"
                    Width="260" Margin="4,0"/>
</Grid>
```

```csharp
using System.Runtime.InteropServices;

private AppWindow m_AppWindow;

public MainWindow()
{
    this.InitializeComponent();

    m_AppWindow = GetAppWindowForCurrentWindow();

    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (AppWindowTitleBar.IsCustomizationSupported())
    {
        var titleBar = m_AppWindow.TitleBar;
        titleBar.ExtendsContentIntoTitleBar = true;
        AppTitleBar.Loaded += AppTitleBar_Loaded;
        AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
    }
    else
    {
        // In the case that title bar customization is not supported, hide the custom title bar
        // element.
        AppTitleBar.Visibility = Visibility.Collapsed;

        // Show alternative UI for any functionality in
        // the title bar, such as search.
    }

}

private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
{
    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (AppWindowTitleBar.IsCustomizationSupported())
    {
        SetDragRegionForCustomTitleBar(m_AppWindow);
    }
}

private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
{
    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (AppWindowTitleBar.IsCustomizationSupported()
        && m_AppWindow.TitleBar.ExtendsContentIntoTitleBar)
    {
        // Update drag region if the size of the title bar changes.
        SetDragRegionForCustomTitleBar(m_AppWindow);
    }
}

private AppWindow GetAppWindowForCurrentWindow()
{
    IntPtr hWnd = WindowNative.GetWindowHandle(this);
    WindowId wndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
    return AppWindow.GetFromWindowId(wndId);
}

[DllImport("Shcore.dll", SetLastError = true)]
internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

internal enum Monitor_DPI_Type : int
{
    MDT_Effective_DPI = 0,
    MDT_Angular_DPI = 1,
    MDT_Raw_DPI = 2,
    MDT_Default = MDT_Effective_DPI
}

private double GetScaleAdjustment()
{
    IntPtr hWnd = WindowNative.GetWindowHandle(this);
    WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
    DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
    IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

    // Get DPI.
    int result = GetDpiForMonitor(hMonitor, Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
    if (result != 0)
    {
        throw new Exception("Could not get DPI for monitor.");
    }

    uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
    return scaleFactorPercent / 100.0;
}

private void SetDragRegionForCustomTitleBar(AppWindow appWindow)
{
    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (AppWindowTitleBar.IsCustomizationSupported()
        && appWindow.TitleBar.ExtendsContentIntoTitleBar)
    {
        double scaleAdjustment = GetScaleAdjustment();

        RightPaddingColumn.Width = new GridLength(appWindow.TitleBar.RightInset / scaleAdjustment);
        LeftPaddingColumn.Width = new GridLength(appWindow.TitleBar.LeftInset / scaleAdjustment);

        List<Windows.Graphics.RectInt32> dragRectsList = new();

        Windows.Graphics.RectInt32 dragRectL;
        dragRectL.X = (int)((LeftPaddingColumn.ActualWidth) * scaleAdjustment);
        dragRectL.Y = 0;
        dragRectL.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
        dragRectL.Width = (int)((IconColumn.ActualWidth
                                + TitleColumn.ActualWidth
                                + LeftDragColumn.ActualWidth) * scaleAdjustment);
        dragRectsList.Add(dragRectL);

        Windows.Graphics.RectInt32 dragRectR;
        dragRectR.X = (int)((LeftPaddingColumn.ActualWidth
                            + IconColumn.ActualWidth
                            + TitleTextBlock.ActualWidth
                            + LeftDragColumn.ActualWidth
                            + SearchColumn.ActualWidth) * scaleAdjustment);
        dragRectR.Y = 0;
        dragRectR.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
        dragRectR.Width = (int)(RightDragColumn.ActualWidth * scaleAdjustment);
        dragRectsList.Add(dragRectR);

        Windows.Graphics.RectInt32[] dragRects = dragRectsList.ToArray();

        appWindow.TitleBar.SetDragRectangles(dragRects);
    }
}
```

> [!WARNING]
> `AppWindow` uses physical pixels for compatibility with UI frameworks that don't use logical coordinates. If you use WPF or WinUI 3, `RightInset`, `LeftInset`, and the values passed to `SetDragRectangles` need to be adjusted if the display scale is not 100%. In this example, we calculate a `scaleAdjustment` value to account for the display scale setting.
>
> For WPF, you can handle the [Window.DpiChanged](/dotnet/api/system.windows.window.dpichanged) event to get the [NewDpi](/dotnet/api/system.windows.dpichangedeventargs.newdpi) value.
>
> For WinUI 3, use [Platform Invoke (P/Invoke)](/dotnet/standard/native-interop/pinvoke) to call the native [GetDpiForMonitor](/windows/win32/api/shellscalingapi/nf-shellscalingapi-getdpiformonitor) function, as shown in the preceding example.

> [!TIP]
> You can get the height of the system TitleBar (`int titleBarHeight = appWindow.TitleBar.Height;`) and use that to set the height of your custom title bar and drag regions. However, the [design guidance](../design/basics/titlebar-design.md) recommends setting the title bar height to 48px if you add other controls. In this case, the height of the system title bar will not match your content, so instead, use the [ActualHeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.actualheight) of the title bar element to set the drag region height.

### [WinUI 3](#tab/winui3)

The element passed to [SetTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.settitlebar) supports the same system interactions as the standard title bar, including drag, double-click to resize, and right-click to show the window context menu. As a result, all pointer input (mouse, touch, pen, and so on) is handled by the system. It is no longer recognized by the title bar element and its child elements. The rectangular area occupied by the title bar element acts as the title bar for pointer purposes, even if the element is blocked by another element, or the element is transparent. However, keyboard input is recognized and child elements can receive keyboard focus.

This means that you can't interact with elements in the title bar area except through keyboard input and focus. We don't recommend this because it presents discoverability and accessibility issues.

### [UWP/WinUI 2](#tab/winui2)

- You must call [SetTitleBar](/uwp/api/windows.ui.xaml.window.settitlebar) to define an area as the draggable title bar region. If you don't, the system sets the default drag region at the top of the page. The system will then handle all user input to this area, and prevent input from reaching your controls.
- Place your interactive controls over the top of the drag region defined by the call to [SetTitleBar](/uwp/api/windows.ui.xaml.window.settitlebar) (with a higher z-order). Don't make your interactive controls children of the [UIElement](/uwp/api/windows.ui.xaml.uielement) passed to `SetTitleBar`. After you pass an element to `SetTitleBar`, the system treats it like the system title bar and handles all pointer input to that element.

Here, the [AutoSuggestBox](/uwp/api/Windows.UI.Xaml.Controls.AutoSuggestBox) element has a higher z-order than `AppTitleBar`, so it receives user input.

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="48"/>
        <RowDefinition/>
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
        <Image Source="Assets/WindowIcon.png" 
               Grid.Column="1"
               HorizontalAlignment="Left"
               Width="16" Height="16"
               Margin="8,0,0,0"/>
        <TextBlock x:Name="AppTitleTextBlock"
                   Text="App title" 
                   Style="{StaticResource CaptionTextBlockStyle}" 
                   Grid.Column="1"
                   VerticalAlignment="Center"
                   Margin="28,0,0,0"/>
    </Grid>

    <!-- This control has a higher z-order than AppTitleBar, 
         so it receives user input. -->
    <AutoSuggestBox QueryIcon="Find"
                    PlaceholderText="Search"
                    HorizontalAlignment="Center"
                    Width="260" Height="32"/>
</Grid>
```

---

#### System caption buttons

#### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

The system reserves the upper-left or upper-right corner of the app window for the system caption buttons (minimize, maximize/restore, close). The system retains control of the caption button area to guarantee that minimum functionality is provided for dragging, minimizing, maximizing, and closing the window. The system draws the Close button in the upper-right for left-to-right languages and the upper-left for right-to-left languages.

You can draw content underneath the caption control area, such as your app background, but you should not put any UI that you expect the user to be able to interact with. It does not receive any input because input for the caption controls is handled by the system.

These lines from the previous example show the padding columns in the XAML that defines the title bar. Using padding columns instead of margins ensures that the background paints the area under the caption control buttons (for transparent buttons). Using both right and left padding columns ensures that your title bar behaves correctly in both right-to-left and left-to-right layouts.

```xaml
<Grid.ColumnDefinitions>
    <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
    <ColumnDefinition/>
    <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
</Grid.ColumnDefinitions>
```

The dimensions and position of the caption control area is communicated by the [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) class so that you can account for it in the layout of your title bar UI. The width of the reserved region on each side is given by the [LeftInset](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.leftinset) or [RightInset](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.rightinset) properties, and its height is given by the [Height](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height) property.

Here's how the width of the padding columns is specified when the drag regions are calculated and set.

```csharp
// Get caption button occlusion information.
int CaptionButtonOcclusionWidthRight = appWindow.TitleBar.RightInset;
int CaptionButtonOcclusionWidthLeft = appWindow.TitleBar.LeftInset;

// Set the width of padding columns in the UI.
RightPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthRight);
LeftPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthLeft);
```

> [!IMPORTANT]
> See important information in the [_Interactive content_](#interactive-content) section about how display scaling affects these values.

##### Tall title bar support for custom title bars

When you add interactive content like a search box or person picture in the title bar, we recommend that you increase the height of your title bar to provide more room for these elements. A taller title bar also makes touch manipulation easier. The [AppWindowTitleBar.PreferredHeightOption](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.preferredheightoption) property gives you the option of increasing your title bar height from the standard height, which is the default, to a taller height. When you select `Tall` title bar mode, the caption buttons that the system draws as an overlay in the client area are rendered taller with their min/max/close glyphs centered. If you haven't specified a drag region, the system will draw one that extends the width of your window and the height determined by the `PreferredHeightOption` value that you set.

The [AppWindowTitleBar.ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) property must be `true` for the `PreferredHeightOption` property to take effect. If you set the `PreferredHeightOption` before setting `ExtendsContentIntoTitlebar` to `true`, the proprty is silently ignored until you set `ExtendsContentIntoTitlebar` to `true`, at which point it takes effect.

This example shows how you can set the `PreferredHeightOption` property.

```csharp
bool isTallTitleBar = true;

// A taller title bar is only supported when drawing a fully custom title bar
if (AppWindowTitleBar.IsCustomizationSupported() && m_AppWindow.TitleBar.ExtendsContentIntoTitleBar)
{
       if (isTallTitleBar)
       {
            // Choose a tall title bar to provide more room for interactive elements 
            // like search box or person picture controls.
            m_AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
       }
       else
       {
            _mainAppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Standard;
       }
       // Recalculate the drag region for the custom title bar 
       // if you explicitly defined new draggable areas.
       SetDragRegionForCustomTitleBar(_m_AppWindow);
}
```


#### [WinUI 3](#tab/winui3)

The system reserves the upper-left or upper-right corner of the app window for the system caption buttons (minimize, maximize/restore, close). The system retains control of the caption button area to guarantee that minimum functionality is provided for dragging, minimizing, maximizing, and closing the window. The system draws the Close button in the upper-right for left-to-right languages and the upper-left for right-to-left languages.

#### [UWP/WinUI 2](#tab/winui2)

The system reserves the upper-left or upper-right corner of the app window for the system caption buttons (minimize, maximize/restore, close). The system retains control of the caption button area to guarantee that minimum functionality is provided for dragging, minimizing, maximizing, and closing the window. The system draws the Close button in the upper-right for left-to-right languages and the upper-left for right-to-left languages.

You can draw content underneath the caption control area, such as your app background, but you should not put any UI that you expect the user to be able to interact with. It does not receive any input because input for the caption controls is handled by the system.

These lines from the previous example show the padding columns in the XAML that defines the title bar. Using padding columns instead of margins ensures that the background paints the area under the caption control buttons (for transparent buttons). Using both right and left padding columns ensures that your title bar behaves correctly in both right-to-left and left-to-right layouts.

```xaml
<Grid.ColumnDefinitions>
    <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
    <ColumnDefinition/>
    <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
</Grid.ColumnDefinitions>
```

The dimensions and position of the caption control area is communicated by the [CoreApplicationViewTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar) class so that you can account for it in the layout of your title bar UI. The width of the reserved region on each side is given by the [SystemOverlayLeftInset](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.SystemOverlayLeftInset) or [SystemOverlayRightInset](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.SystemOverlayRightInset) properties, and its height is given by the [Height](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.Height) property.

You can handle the [LayoutMetricsChanged](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.LayoutMetricsChanged) event to respond to changes in the size of the caption buttons. For example, this can happen if the app layout changes from left-to-right to right-to-left. Handle this event to verify and update the positioning of UI elements that depend on the title bar's size.

This example shows how to adjust the layout of your title bar to account for changes in the title bar metrics. `AppTitleBar`, `LeftPaddingColumn`, and `RightPaddingColumn` are declared in the XAML shown previously.

```csharp
private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    // Get the size of the caption controls and set padding.
    LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
    RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
}
```

---

### Color and transparency in caption buttons

When you extend your app content into the title bar area, you can make the background of the caption buttons transparent to let your app background show through. You typically set the background to `Colors.Transparent` for full transparency. For partial transparency, set the alpha channel for the `Color` you set the property to.

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

These title bar properties can be transparent:

- [ButtonBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonbackgroundcolor)
- [ButtonHoverBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonhoverbackgroundcolor)
- [ButtonPressedBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonpressedbackgroundcolor)
- [ButtonInactiveBackgroundColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttoninactivebackgroundcolor)

All other color properties will continue to ignore the alpha channel. If `ExtendsContentIntoTitleBar` is set to `false`, the alpha channel is always ignored for all `AppWindowTitleBar` color properties.

Reference: [Colors.Transparent](/windows/windows-app-sdk/api/winrt/microsoft.ui.colors.transparent), [ColorHelper](/windows/windows-app-sdk/api/winrt/microsoft.ui.colorhelper)

### [WinUI 3](#tab/winui3)

When you use a custom title bar, you can modify the colors of the caption buttons to match your app. To do so, override the following resources (shown here with their default values):

```xaml
<StaticResource x:Key="WindowCaptionBackground" ResourceKey="SystemControlBackgroundBaseLowBrush" />
<StaticResource x:Key="WindowCaptionBackgroundDisabled" ResourceKey="SystemControlBackgroundBaseLowBrush" />
<StaticResource x:Key="WindowCaptionForeground" ResourceKey="SystemControlForegroundBaseHighBrush" />
<StaticResource x:Key="WindowCaptionForegroundDisabled" ResourceKey="SystemControlDisabledBaseMediumLowBrush" />
```

This example shows how to override the default values in App.xaml.

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
            <!-- Other merged dictionaries here -->
        </ResourceDictionary.MergedDictionaries>
        <!-- Other app resources here -->
        <SolidColorBrush x:Key="WindowCaptionBackground">Green</SolidColorBrush>
        <SolidColorBrush x:Key="WindowCaptionBackgroundDisabled">LightGreen</SolidColorBrush>
        <SolidColorBrush x:Key="WindowCaptionForeground">Red</SolidColorBrush>
        <SolidColorBrush x:Key="WindowCaptionForegroundDisabled">Pink</SolidColorBrush>
    </ResourceDictionary>
</Application.Resources>
```

Transparency is supported in the `WindowCaptionBackground` and `WindowCaptionForegroundDisabled` brushes.

### [UWP/WinUI 2](#tab/winui2)

These title bar properties can be transparent:

- [ButtonBackgroundColor](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonbackgroundcolor)
- [ButtonHoverBackgroundColor](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonhoverbackgroundcolor)
- [ButtonPressedBackgroundColor](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonpressedbackgroundcolor)
- [ButtonInactiveBackgroundColor](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttoninactivebackgroundcolor)

All other color properties will continue to ignore the alpha channel. If `ExtendViewIntoTitleBar` is set to `false`, the alpha channel is always ignored for all `ApplicationViewTitleBar` color properties.

Reference: [Colors.Transparent](/uwp/api/windows.ui.colors.Transparent), [Color](/uwp/api/windows.ui.color)

---

The button background color is not applied to the Close button _hover_ and _pressed_ states. The close button always uses the system-defined color for those states.

> [!TIP]
> [Mica](../design/style/mica.md) is a delightful [material](../design/signature-experiences/materials.md) that helps distinguish the window that's in focus. We recommend it as the background for long-lived windows in Windows 11. If you have applied Mica in the client area of your window, you can extend it into the titlebar area and make your caption buttons transparent for the Mica to show through. See [Mica material](../design/style/mica.md) for more info.

### Dim the title bar when the window is inactive

You should make it obvious when your window is active or inactive. At a minimum, you should change the color of the text, icons, and buttons in your title bar.

### [Windows App SDK](#tab/wasdk)

Handle an event to determine the activation state of the window, and update your title bar UI as needed. How you determine the state of the window depends on the UI framework you use for your app.

- **Win32**: Listen and respond to the [WM_ACTIVATE](/windows/win32/inputdev/wm-activate) message.
- **WPF**: Handle [Window.Activated](/dotnet/api/system.windows.window.activated), [Window.Deactivated](/dotnet/api/system.windows.window.deactivated).
- **WinForms**: Handle [Form.Activated](/dotnet/api/system.windows.forms.form.activated), [Form.Deactivate](/dotnet/api/system.windows.forms.form.deactivate).
- **WinUI 3 with Windows App SDK title bar APIs**: Handle [Window.Activated](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.activated) (See the WinUI 3 tab).

### [WinUI 3](#tab/winui3)

Handle the [Window.Activated](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.activated) event to determine the activation state of the window, and update your title bar UI as needed.

```csharp
public MainWindow()
{
    ...
    Activated += MainWindow_Activated;
}

private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
{
    if (args.WindowActivationState == WindowActivationState.Deactivated)
    {
        AppTitleTextBlock.Foreground =
            (SolidColorBrush)App.Current.Resources["WindowCaptionForegroundDisabled"];
    }
    else
    {
        AppTitleTextBlock.Foreground =
            (SolidColorBrush)App.Current.Resources["WindowCaptionForeground"];
    }
}
```

### [UWP/WinUI 2](#tab/winui2)

Handle the [CoreWindow.Activated](/uwp/api/windows.ui.core.corewindow.activated) event to determine the activation state of the window, and update your title bar UI as needed.

```csharp
public MainPage()
{
    ...
    Window.Current.CoreWindow.Activated += CoreWindow_Activated;
}

private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args)
{
    UISettings settings = new UISettings();
    if (args.WindowActivationState == CoreWindowActivationState.Deactivated)
    {
        AppTitleTextBlock.Foreground = 
            new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
    }
    else
    {
        AppTitleTextBlock.Foreground = 
            new SolidColorBrush(settings.UIElementColor(UIElementType.WindowText));
    }
}
```

---

### Reset the title bar

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

To reset or switch to the system title bar while your app is running, you can call [AppWindowTitleBar.ResetToDefault](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.resettodefault).

```csharp
m_AppWindow.TitleBar.ResetToDefault();
```

### [WinUI 3](#tab/winui3)

You can call [SetTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.settitlebar) to switch to a new title bar element while your app is running. You can also pass `null` as the parameter to `SetTitleBar` and set [ExtendsContentIntoTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) to `false` to revert to the default system title bar.

### [UWP/WinUI 2](#tab/winui2)

You can call [SetTitleBar](/uwp/api/windows.ui.xaml.window.settitlebar) to switch to a new title bar element while your app is running. You can also pass `null` as the parameter to `SetTitleBar` and set [ExtendViewIntoTitleBar](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.extendviewintotitlebar) to `false` to revert to the default system title bar.

---

### Show and hide the title bar

If you add support for _full screen_ or _compact overlay_ modes to your app, you might need to make changes to your title bar when your app switches between these modes.

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

When your app runs in _full screen_ mode, the system hides the title bar and caption control buttons. You can handle the [AppWindow.Changed](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event and check the event args [DidPresenterChange](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didpresenterchange) property to determine if you should show, hide, or change the title bar in response to a new window presentation.

This example shows how to handle the `Changed` event to show and hide the `AppTitleBar` element from previous examples. If the window is put in _compact overlay_ mode, the title bar is reset to the default system title bar (or you could provide a custom title bar optimized for compact overlay).

```csharp
public MainWindow()
{
    this.InitializeComponent();

    m_AppWindow = GetAppWindowForCurrentWindow();
    m_AppWindow.Changed += AppWindow_Changed;
}

private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
{
    // Check to see if customization is supported.
    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    // Windows App SDK on Windows 11.
    if (args.DidPresenterChange
        && AppWindowTitleBar.IsCustomizationSupported())
    {
        switch (sender.Presenter.Kind)
        {
            case AppWindowPresenterKind.CompactOverlay:
                // Compact overlay - hide custom title bar
                // and use the default system title bar instead.
                AppTitleBar.Visibility = Visibility.Collapsed;
                sender.TitleBar.ResetToDefault();
                break;

            case AppWindowPresenterKind.FullScreen:
                // Full screen - hide the custom title bar
                // and the default system title bar.
                AppTitleBar.Visibility = Visibility.Collapsed;
                sender.TitleBar.ExtendsContentIntoTitleBar = true;
                break;

            case AppWindowPresenterKind.Overlapped:
                // Normal - hide the system title bar
                // and use the custom title bar instead.
                AppTitleBar.Visibility = Visibility.Visible;
                sender.TitleBar.ExtendsContentIntoTitleBar = true;
                SetDragRegionForCustomTitleBar(sender);
                break;

            default:
                // Use the default system title bar.
                sender.TitleBar.ResetToDefault();
                break;
        }
    }
}
```

>[!NOTE]
>_Full screen_ and _compact overlay_ modes can be entered only if supported by your app. See [Manage app windows](../windows-app-sdk/windowing/windowing-overview.md), [FullScreenPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.fullscreenpresenter), and [CompactOverlayPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.compactoverlaypresenter) for more info.

### [WinUI 3](#tab/winui3)

WinUI 3 does not provide any APIs to support full screen mode. A WinUI 3 app can use Windows App SDK APIs for this, but the XAML [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) does not hide the title bar in this case.

### [UWP/WinUI 2](#tab/winui2)

When your app runs in _full screen_ or [_tablet mode_ (Windows 10 only)](/windows-hardware/design/device-experiences/continuum), the system hides the title bar and caption control buttons. However, the user can invoke the title bar to have it shown as an overlay on top of the app's UI.

You can handle the [CoreApplicationViewTitleBar.IsVisibleChanged](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.IsVisibleChanged) event to be notified when the title bar is hidden or invoked, and show or hide your custom title bar content as needed.

This example shows how to handle the `IsVisibleChanged` event to show and hide the `AppTitleBar` element from previous examples.

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

- Do make it obvious when your window is active or inactive. At a minimum, change the color of the text, icons, and buttons in your title bar.
- Do define a drag region along the top edge of the app canvas. Matching the placement of system title bars makes it easier for users to find.
- Do define a drag region that matches the visual title bar (if any) on the app's canvas.

## Full customization example

This examples shows all the code described in the Full customization section.

### [Windows App SDK](#tab/wasdk)

> (Supported on Windows 10 since Windows App SDK 1.2 and fully supported on Windows 11. See [Platform options](#platform-options) for more info.)

```xaml
<Window
    x:Class="WASDK_ExtendedTitleBar.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:WASDK_ExtendedTitleBar"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition />
        </Grid.RowDefinitions>
        <Grid x:Name="AppTitleBar"  
      Height="48">
            <Grid.ColumnDefinitions>
                <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
                <ColumnDefinition x:Name="IconColumn" Width="Auto"/>
                <ColumnDefinition x:Name="TitleColumn" Width="Auto"/>
                <ColumnDefinition x:Name="LeftDragColumn" Width="*"/>
                <ColumnDefinition x:Name="SearchColumn" Width="Auto"/>
                <ColumnDefinition x:Name="RightDragColumn" Width="*"/>
                <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
            </Grid.ColumnDefinitions>
            <Image x:Name="TitleBarIcon" Source="/Images/WindowIcon.png"
           Grid.Column="1"
           Width="16" Height="16"
           Margin="8,0,0,0"/>
            <TextBlock x:Name="TitleTextBlock" 
               Text="App title" 
               Style="{StaticResource CaptionTextBlockStyle}"
               Grid.Column="2"
               VerticalAlignment="Center"
               Margin="4,0,0,0"/>
            <AutoSuggestBox Grid.Column="4" QueryIcon="Find"
                    PlaceholderText="Search"
                    VerticalAlignment="Center"
                    Width="260" Margin="4,0"/>
        </Grid>

        <NavigationView Grid.Row="1"
                        IsBackButtonVisible="Collapsed" 
                        IsSettingsVisible="False">
            <StackPanel>
                <TextBlock Text="Content" 
                           Style="{ThemeResource TitleTextBlockStyle}"
                           Margin="32,0,0,0"/>
                <StackPanel Grid.Row="1" VerticalAlignment="Center">
                    <Button Margin="4" x:Name="CompactoverlaytBtn"
                            Content="Enter CompactOverlay"
                            Click="SwitchPresenter"/>
                    <Button Margin="4" x:Name="FullscreenBtn" 
                            Content="Enter FullScreen"
                            Click="SwitchPresenter"/>
                    <Button Margin="4" x:Name="OverlappedBtn"
                            Content="Revert to default (Overlapped)"
                            Click="SwitchPresenter"/>
                </StackPanel>
            </StackPanel>
        </NavigationView>
    </Grid>
</Window>
```

```csharp
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WinRT.Interop;

namespace WASDK_ExtendedTitleBar
{
    public sealed partial class MainWindow : Window
    {
        private AppWindow m_AppWindow;

        public MainWindow()
        {
            this.InitializeComponent();

            m_AppWindow = GetAppWindowForCurrentWindow();
            m_AppWindow.Changed += AppWindow_Changed;

            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions
            // of Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = m_AppWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;
                AppTitleBar.Loaded += AppTitleBar_Loaded;
                AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
            }
            else
            {
                // In the case that title bar customization is not supported, hide the custom title
                // bar element.
                AppTitleBar.Visibility = Visibility.Collapsed;

                // Show alternative UI for any functionality in
                // the title bar, such as search.
            }

        }

        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                SetDragRegionForCustomTitleBar(m_AppWindow);
            }
        }

        private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AppWindowTitleBar.IsCustomizationSupported()
                && m_AppWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                // Update drag region if the size of the title bar changes.
                SetDragRegionForCustomTitleBar(m_AppWindow);
            }
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }

        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

        internal enum Monitor_DPI_Type : int
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = MDT_Effective_DPI
        }

        private double GetScaleAdjustment()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
            IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

            // Get DPI.
            int result = GetDpiForMonitor(hMonitor, Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
            if (result != 0)
            {
                throw new Exception("Could not get DPI for monitor.");
            }

            uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
            return scaleFactorPercent / 100.0;
        }

        private void SetDragRegionForCustomTitleBar(AppWindow appWindow)
        {
            if (AppWindowTitleBar.IsCustomizationSupported()
                && appWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                double scaleAdjustment = GetScaleAdjustment();

                RightPaddingColumn.Width = new GridLength(appWindow.TitleBar.RightInset / scaleAdjustment);
                LeftPaddingColumn.Width = new GridLength(appWindow.TitleBar.LeftInset / scaleAdjustment);

                List<Windows.Graphics.RectInt32> dragRectsList = new();

                Windows.Graphics.RectInt32 dragRectL;
                dragRectL.X = (int)((LeftPaddingColumn.ActualWidth) * scaleAdjustment);
                dragRectL.Y = 0;
                dragRectL.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
                dragRectL.Width = (int)((IconColumn.ActualWidth
                                        + TitleColumn.ActualWidth
                                        + LeftDragColumn.ActualWidth) * scaleAdjustment);
                dragRectsList.Add(dragRectL);

                Windows.Graphics.RectInt32 dragRectR;
                dragRectR.X = (int)((LeftPaddingColumn.ActualWidth
                                    + IconColumn.ActualWidth
                                    + TitleTextBlock.ActualWidth
                                    + LeftDragColumn.ActualWidth
                                    + SearchColumn.ActualWidth) * scaleAdjustment);
                dragRectR.Y = 0;
                dragRectR.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
                dragRectR.Width = (int)(RightDragColumn.ActualWidth * scaleAdjustment);
                dragRectsList.Add(dragRectR);

                Windows.Graphics.RectInt32[] dragRects = dragRectsList.ToArray();

                appWindow.TitleBar.SetDragRectangles(dragRects);
            }
        }

        private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
        {
            if (args.DidPresenterChange
                && AppWindowTitleBar.IsCustomizationSupported())
            {
                switch (sender.Presenter.Kind)
                {
                    case AppWindowPresenterKind.CompactOverlay:
                        // Compact overlay - hide custom title bar
                        // and use the default system title bar instead.
                        AppTitleBar.Visibility = Visibility.Collapsed;
                        sender.TitleBar.ResetToDefault();
                        break;

                    case AppWindowPresenterKind.FullScreen:
                        // Full screen - hide the custom title bar
                        // and the default system title bar.
                        AppTitleBar.Visibility = Visibility.Collapsed;
                        sender.TitleBar.ExtendsContentIntoTitleBar = true;
                        break;

                    case AppWindowPresenterKind.Overlapped:
                        // Normal - hide the system title bar
                        // and use the custom title bar instead.
                        AppTitleBar.Visibility = Visibility.Visible;
                        sender.TitleBar.ExtendsContentIntoTitleBar = true;
                        SetDragRegionForCustomTitleBar(sender);
                        break;

                    default:
                        // Use the default system title bar.
                        sender.TitleBar.ResetToDefault();
                        break;
                }
            }
        }

        private void SwitchPresenter(object sender, RoutedEventArgs e)
        {
            if (m_AppWindow != null)
            {
                AppWindowPresenterKind newPresenterKind;
                switch ((sender as Button).Name)
                {
                    case "CompactoverlaytBtn":
                        newPresenterKind = AppWindowPresenterKind.CompactOverlay;
                        break;

                    case "FullscreenBtn":
                        newPresenterKind = AppWindowPresenterKind.FullScreen;
                        break;

                    case "OverlappedBtn":
                        newPresenterKind = AppWindowPresenterKind.Overlapped;
                        break;

                    default:
                        newPresenterKind = AppWindowPresenterKind.Default;
                        break;
                }

                // If the same presenter button was pressed as the
                // mode we're in, toggle the window back to Default.
                if (newPresenterKind == m_AppWindow.Presenter.Kind)
                {
                    m_AppWindow.SetPresenter(AppWindowPresenterKind.Default);
                }
                else
                {
                    // Else request a presenter of the selected kind
                    // to be created and applied to the window.
                    m_AppWindow.SetPresenter(newPresenterKind);
                }
            }
        }
    }
}
```

### [WinUI 3](#tab/winui3)

```xaml
<Window
    x:Class="WinUI3_ExtendedTitleBar.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:WinUI3_ExtendedTitleBar"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="32"/>
            <RowDefinition/>
        </Grid.RowDefinitions>

        <Grid x:Name="AppTitleBar">
            <Image Source="Images/WindowIcon.png"
                   HorizontalAlignment="Left" 
                   Width="16" Height="16" 
                   Margin="8,0"/>
            <TextBlock x:Name="AppTitleTextBlock" Text="App title"
                       TextWrapping="NoWrap"
                       Style="{StaticResource CaptionTextBlockStyle}" 
                       VerticalAlignment="Center"
                       Margin="28,0,0,0"/>
        </Grid>

        <NavigationView Grid.Row="1"
                        IsBackButtonVisible="Collapsed"
                        IsSettingsVisible="False">
            <StackPanel>
                <TextBlock Text="Content" 
                           Style="{ThemeResource TitleTextBlockStyle}"
                           Margin="32,0,0,0"/>
            </StackPanel>
        </NavigationView>
    </Grid>
</Window>
```

```csharp
using Microsoft.UI.Xaml;

namespace WinUI3_ExtendedTitleBar
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
        }
    }
}
```

### [UWP/WinUI 2](#tab/winui2)

```xaml
<Page
    x:Class="WinUI2_ExtendedTitleBar.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:WinUI2_ExtendedTitleBar"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    xmlns:muxc="using:Microsoft.UI.Xaml.Controls"
    muxc:BackdropMaterial.ApplyToRootOrPageBackground="True">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="48"/>
            <RowDefinition/>
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
            <Image Source="Assets/WindowIcon.png" 
                   Grid.Column="1"
                   HorizontalAlignment="Left"
                   Width="16" Height="16"
                   Margin="8,0,0,0"/>
            <TextBlock x:Name="AppTitleTextBlock"
                       Text="App title" 
                       Style="{StaticResource CaptionTextBlockStyle}" 
                       Grid.Column="1"
                       VerticalAlignment="Center"
                       Margin="28,0,0,0"/>
        </Grid>

        <!-- This control has a higher z-order than AppTitleBar, 
             so it receives user input. -->
        <AutoSuggestBox QueryIcon="Find"
                        PlaceholderText="Search"
                        HorizontalAlignment="Center"
                        Width="260" Height="32"/>

        <muxc:NavigationView Grid.Row="1"
                             IsBackButtonVisible="Collapsed"
                             IsSettingsVisible="False">
            <StackPanel>
                <TextBlock Text="Content" 
                           Style="{ThemeResource TitleTextBlockStyle}"
                           Margin="12,0,0,0"/>
            </StackPanel>
        </muxc:NavigationView>
    </Grid>
</Page>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    // Hide default title bar.
    CoreApplicationViewTitleBar coreTitleBar = 
        CoreApplication.GetCurrentView().TitleBar;
    coreTitleBar.ExtendViewIntoTitleBar = true;

    // Set caption buttons background to transparent.
    ApplicationViewTitleBar titleBar = 
        ApplicationView.GetForCurrentView().TitleBar;
    titleBar.ButtonBackgroundColor = Colors.Transparent;

    // Set XAML element as a drag region.
    Window.Current.SetTitleBar(AppTitleBar);

    // Register a handler for when the size of the overlaid caption control changes.
    coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

    // Register a handler for when the title bar visibility changes.
    // For example, when the title bar is invoked in full screen mode.
    coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

    // Register a handler for when the window activation changes.
    Window.Current.CoreWindow.Activated += CoreWindow_Activated;
}

private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
{
    // Get the size of the caption controls and set padding.
    LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
    RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
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

 private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args)
 {
     UISettings settings = new UISettings();
     if (args.WindowActivationState == CoreWindowActivationState.Deactivated)
     {
         AppTitleTextBlock.Foreground = 
            new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
     }
     else
     {
         AppTitleTextBlock.Foreground = 
            new SolidColorBrush(settings.UIElementColor(UIElementType.WindowText));
     }
 }
```

---

## Related articles

- [Acrylic](../design/style/acrylic.md)
- [Mica](../design/style/mica.md)
- [Color](../design/style/color.md)