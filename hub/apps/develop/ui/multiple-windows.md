---
description: Use multiple windows to show your app content.
title: Use the Window class to show secondary windows for an app
ms.date: 06/27/2025
ms.topic: article
ms.localizationpriority: medium
no-loc: [Window, Page, AppWindow, Frame, Dictionary, WindowId]
appliesto:
  - ✅ <a href="https://learn.microsoft.com/en-us/windows/apps/winui/winui3/" target="_blank">WinUI</a>
  - ✅ <a href="hhttps://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/" target="_blank">Windows App SDK</a>
---
# Show multiple windows for your app

In your WinUI 3 app, you can show your app content in secondary windows while still working on the same UI thread across each window.

> [!div class="checklist"]
>
> - **Important APIs**: [Microsoft.UI.Windowing namespace](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing), [Window class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window), [AppWindow class](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see multiple windows in action](winui3gallery://item/CreateMultipleWindows)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

> [!TIP]
> A common reason to use multiple windows is to allow [TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) tabs to be torn out into a new window. For information and examples specific to this scenario, see [Tab tear-out](/windows/apps/design/controls/tab-view#tab-tear-out) in the [Tab view](controls/tab-view.md) article.

## API overview

Here are some of the important APIs you use to show content in multiple windows.

### XAML Window and AppWindow

The [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) and [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) classes can be used to display a portion of an app in a secondary window. An important feature of WinUI windows is that each instance shares the same UI processing thread (including the event dispatcher) from which they were created, which simplifies multi-window apps.

See [Windowing overview for WinUI and Windows App SDK](windowing-overview.md) for a more detailed explanation of Window and AppWindow.

### AppWindowPresenter

The [AppWindowPresenter](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter) API lets you easily switch windows into pre-defined configurations like `FullScreen` or `CompactOverlay`. For more info, see [Manage app windows](windowing-overview.md).

### XamlRoot

The [XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot) class holds a XAML element tree, connects it to the window host object, and provides info such as size and visibility. You don't create a XamlRoot object directly. Instead, one is created when you attach a XAML element to a Window. You can then use the [UIElement.XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.xamlroot) property to retrieve the XamlRoot.

### WindowId

[WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid) is a unique identifier for an app window. It's created automatically, and identifies both the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) and the top-level Win32 HWND it's associated with.

From a visual element, you can access [UIElement.XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.xamlroot); then [XamlRoot.ContentIslandEnvironment](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.xamlroot.contentislandenvironment); then the [ContentIslandEnvironment.AppWindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.content.contentislandenvironment.appwindowid) property contains the ID of window that the UIElement is in.

## Show a new window

You can create a new Window in XAML or in code. If you create a Window in XAML, you're actually creating a sub-class of the Window class. For example, see `MainWindow.xaml` that's created by the Visual Studio app template.

Let's take a look at the steps to show content in a new window.

**To create a new window with XAML**

1. In the **Solution Explorer** pane, right-click on the project name and select **Add > New Item...**
1. In the **Add New Item** dialog, select **WinUI** in the template list on the left-side of the window.
1. Select the **Blank Window** template.
1. Name the file.
1. Press **Add**.

**To show a new window**

1. Instantiate a new instance of [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window), or a Window subclass if you created a Window subclass with a `.xaml` file.

    ```csharp
    Window newWindow = new Window();
    ```

1. Create the window content.

    If you created a [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) subclass with a `.xaml` file, you can add the window content directly in XAML. Otherwise, you add the content in code as shown here.

    It's common to create a XAML [Frame](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame), then navigate the Frame to a XAML [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page where you've defined your app content. For more info about frames and pages, see [Peer-to-peer navigation between two pages](/windows/apps/design/basics/navigate-between-two-pages).

    ```csharp
    Frame contentFrame = new Frame();
    contentFrame.Navigate(typeof(SecondaryPage));
    ```

    However, you can show any XAML content in the AppWindow, not just a Frame and Page. For example, you can show just a single control, like [ColorPicker](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.colorpicker), as shown later.

1. Set your XAML content to the [Content](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.content) property of the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window).

    ```csharp
    newWindow.Content = contentFrame;
    ```

1. Call the [Window.Activate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.activate) method to show the new window.

    ```csharp
    newWindow.Activate();
    ```

## Track instances of Window

You might want to have access to the [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) instances from other parts of your app, but after you create an instance of a Window, there's no way to access it from other code unless you keep a reference to it. For example, you might want to handle the [Window.SizeChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.sizechanged) event in `MainPage` to rearrange UI elements when the window is resized, or you could have a 'close all' button that closes all the tracked instances of Window.

In this case, you should use the [WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid) unique identifier to track the window instances in a [Dictionary](/dotnet/api/system.collections.generic.dictionary-2?view=dotnet-uwp-10.0&preserve-view=true), with the WindowId as the `Key` and the Window instance as the `Value`. ([TabView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.tabview) tab tear-out APIs also use WindowId to track Windows.)

In your `App` class, create the [Dictionary](/dotnet/api/system.collections.generic.dictionary-2?view=dotnet-uwp-10.0&preserve-view=true) as a static property. Then, add each page to the Dictionary when you create it, and remove it when the page is closed.

```csharp
// App.xaml.cs
public partial class App : Application
{
    private Window? _window;
    public static Dictionary<WindowId, Window> ActiveWindows { get; set; } = new Dictionary<WindowId, Window>();

    // ...

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
        // Track the new window in the dictionary.
        ActiveWindows.Add(_window.AppWindow.Id, _window);
    }
}
```

The following code creates a new window when a button is clicked in `MainPage`. The `TrackWindow` method adds the new window to the `ActiveWindows` [Dictionary](/dotnet/api/system.collections.generic.dictionary-2?view=dotnet-uwp-10.0&preserve-view=true), and handles the [Window.Closed](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.closed) event to remove it from `ActiveWindows` when the window is closed.

```csharp
// MainPage.xaml.cs
private Window CreateWindow()
{
    Window newWindow = new Window();

    // Configure the window.
    newWindow.AppWindow.Resize(new SizeInt32(1200, 800));
    newWindow.Title = "Window " + newWindow.AppWindow.Id.Value.ToString();
    newWindow.SystemBackdrop = new MicaBackdrop();

    TrackWindow(newWindow);
    return newWindow;
}

private void TrackWindow(Window window)
{
    window.Closed += (sender, args) => {
        App.ActiveWindows.Remove(window.AppWindow.Id, out window);
    };
    App.ActiveWindows.Add(window.AppWindow.Id, window);
}

```

### Get a tracked window from your app code

To access a [Window](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window) instance from your app code, you need to get the [WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid) for the current window to retrieve it from the static [Dictionary](/dotnet/api/system.collections.generic.dictionary-2?view=dotnet-uwp-10.0&preserve-view=true) in your `App` class. You should do this in the page's [Loaded](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.loaded) event handler rather than in the constructor so that [XamlRoot](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.xamlroot) is not `null`.

```csharp
public sealed partial class SecondaryPage : Page
{
    Window window;

    public SecondaryPage()
    {
        InitializeComponent();
        Loaded += AppWindowPage_Loaded;
    }

    private void AppWindowPage_Loaded(object sender, RoutedEventArgs e)
    {
        // Get the reference to this Window that was stored when it was created.
        // Do this in the Page Loaded handler rather than the constructor to
        // ensure that the XamlRoot is created and attached to the Window.
        WindowId windowId = this.XamlRoot.ContentIslandEnvironment.AppWindowId;

        if (App.ActiveWindows.ContainsKey(windowId))
        {
            window = App.ActiveWindows[windowId];
        }
    }
}
```

## Related topics

- [Windowing overview](windowing-overview.md)
- [Manage app windows](manage-app-windows.md)
- [Tab view](controls/tab-view.md)
- [Show multiple views (UWP)](/windows/uwp/ui-input/show-multiple-views)
