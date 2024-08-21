---
description: Help users be more productive by letting them view independent parts of your app in separate windows.
title: Show multiple views for an app
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Show multiple views for an app

![Wireframe showing an app with multiple windows](images/multi-view.gif)

Help your users be more productive by letting them view independent parts of your app in separate windows. When you create multiple windows for an app, the taskbar shows each window separately. Users can move, resize, show, and hide app windows independently and can switch between app windows as if they were separate apps.

> **Important APIs**: [Windows.UI.ViewManagement namespace](/uwp/api/windows.ui.viewmanagement), [Windows.UI.WindowManagement namespace](/uwp/api/windows.ui.windowmanagement)

## When should an app use multiple views?

There are a variety of scenarios that can benefit from multiple views. Here are a few examples:

- An email app that lets users view a list of received messages while composing a new email
- An address book app that lets users compare contact info for multiple people side-by-side
- A music player app that lets users see what's playing while browsing through a list of other available music
- A note-taking app that lets users copy information from one page of notes to another
- A reading app that lets users open several articles for reading later, after an opportunity to peruse all high-level headlines

While each app layout is unique, we recommend including a "new window" button in a predictable location, such as the top right corner of the content that can be opened in a new window. Also consider including a [context menu](../controls/menus.md) option to "Open in a new window".

To create separate instances of your app (rather than separate windows for the same instance), see [Create a multi-instance Windows app](/windows/uwp/launch-resume/multi-instance-uwp).

## Windowing hosts

There are different ways that Windows content can be hosted inside an app.

- [CoreWindow](/uwp/api/windows.ui.core.corewindow)/[ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview)

     An app view is the 1:1 pairing of a thread and a window that the app uses to display content. The first view that's created when your app starts is called the *main view*. Each CoreWindow/ApplicationView operates in its own thread. Having to work on different UI threads can complicate multi-window apps.

    The main view for your app is always hosted in an ApplicationView. Content in a secondary window can be hosted in a ApplicationView or in an AppWindow.

    To learn how to use ApplicationView to show secondary windows in your app, see [Use ApplicationView](application-view.md).
- [AppWindow](/uwp/api/windows.ui.windowmanagement.appwindow)

    AppWindow simplifies the creation of multi-window Windows apps because it operates on the same UI thread that it's created from.

    The AppWindow class and other APIs in the [WindowManagement](/uwp/api/windows.ui.windowmanagement) namespace are available starting in Windows 10, version 1903 (SDK 18362). If your app targets earlier versions of Windows 10, you must use ApplicationView to create secondary windows.

    To learn how to use AppWindow to show secondary windows in your app, see [Use AppWindow](app-window.md).

    > [!NOTE]
    > AppWindow is currently in preview. This means you can submit apps that use AppWindow to the Store, but some platform and framework components are known to not work with AppWindow (see [Limitations](/uwp/api/windows.ui.windowmanagement.appwindow#limitations)).
- [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) (XAML Islands)

     UWP XAML content in a Win32 app (using HWND), also known as XAML Islands, is hosted in a DesktopWindowXamlSource.

    For more info about XAML Islands, see [Using the UWP XAML hosting API in a desktop application](../../desktop/modernize/xaml-islands/using-the-xaml-hosting-api.md)

### Make code portable across windowing hosts

When XAML content is displayed in a [CoreWindow](/uwp/api/windows.ui.core.corewindow), there's always an associated [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview) and XAML [Window](/uwp/api/windows.ui.xaml.window). You can use APIs on these classes to get information such as the window bounds. To retrieve an instance of these classes, you use the static [CoreWindow.GetForCurrentThread](/uwp/api/windows.ui.core.corewindow.getforcurrentthread) method, [ApplicationView.GetForCurrentView](/uwp/api/windows.ui.viewmanagement.applicationview.getforcurrentview) method, or [Window.Current](/uwp/api/windows.ui.xaml.window.current) property. In addition, there are many classes that use the `GetForCurrentView` pattern to retrieve an instance of the class, such as [DisplayInformation.GetForCurrentView](/uwp/api/windows.graphics.display.displayinformation.getforcurrentview).

These APIs work because there is only a single tree of XAML content for a CoreWindow/ApplicationView, so the XAML knows the context in which it's hosted is that CoreWindow/ApplicationView.

When XAML content is running inside an AppWindow or DesktopWindowXamlSource, you can have multiple trees of XAML content running on the same thread at the same time. In this case, these APIs don't give the right information, since the content is no longer running inside the current CoreWindow/ApplicationView (and XAML Window).

To ensure that your code works correctly across all windowing hosts, you should replace APIs that rely on [CoreWindow](/uwp/api/windows.ui.core.corewindow), [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview), and [Window](/uwp/api/windows.ui.xaml.window) with new APIs that get their context from the [XamlRoot](/uwp/api/windows.ui.xaml.xamlroot) class.
The XamlRoot class represents a tree of XAML content and information about the context in which it is hosted, whether it's a CoreWindow, AppWindow, or DesktopWindowXamlSource. This abstraction layer lets you write the same code regardless of which windowing host the XAML runs in.

This table shows code that does not work correctly across windowing hosts, and the new portable code that you can replace it with, as well as some APIs that you don't need to change.

| If you used... | Replace with... |
| - | - |
| CoreWindow.GetForCurrentThread().[Bounds](/uwp/api/windows.ui.core.corewindow.bounds) | _uiElement_.XamlRoot.[Size](/uwp/api/windows.ui.xaml.xamlroot.size) |
| CoreWindow.GetForCurrentThread().[SizeChanged](/uwp/api/windows.ui.core.corewindow.sizechanged) | _uiElement_.XamlRoot.[Changed](/uwp/api/windows.ui.xaml.xamlroot.changed) |
| CoreWindow.[Visible](/uwp/api/windows.ui.core.corewindow.visible) | _uiElement_.XamlRoot.[IsHostVisible](/uwp/api/windows.ui.xaml.xamlroot.ishostvisible) |
| CoreWindow.[VisibilityChanged](/uwp/api/windows.ui.core.corewindow.visibilitychanged) | _uiElement_.XamlRoot.[Changed](/uwp/api/windows.ui.xaml.xamlroot.changed) |
| CoreWindow.GetForCurrentThread().[GetKeyState](/uwp/api/windows.ui.core.corewindow.getkeystate) | Unchanged. This is supported in AppWindow and DesktopWindowXamlSource. |
| CoreWindow.GetForCurrentThread().[GetAsyncKeyState](/uwp/api/windows.ui.core.corewindow.getasynckeystate) | Unchanged. This is supported in AppWindow and DesktopWindowXamlSource. |
| Window.[Current](/uwp/api/windows.ui.xaml.window.current) | Returns the main XAML Window object which is closely bound to the current CoreWindow. See Note after this table. |
| Window.Current.[Bounds](/uwp/api/windows.ui.xaml.window.bounds) | _uiElement_.XamlRoot.[Size](/uwp/api/windows.ui.xaml.xamlroot.size) |
| Window.Current.[Content](/uwp/api/windows.ui.xaml.window.content) | UIElement root =  _uiElement_.XamlRoot.[Content](/uwp/api/windows.ui.xaml.xamlroot.content) |
| Window.Current.[Compositor](/uwp/api/windows.ui.xaml.window.compositor) | Unchanged. This is supported in AppWindow and DesktopWindowXamlSource. |
| VisualTreeHelper.[FindElementsInHostCoordinates](/uwp/api/windows.ui.xaml.media.visualtreehelper.findelementsinhostcoordinates)<br>Although the UIElement param is optional, the method raises an exception if a UIElement isn't supplied when hosted on an Island. | Specify the _uiElement_.XamlRoot as UIElement instead of leaving it blank. |
| VisualTreeHelper.[GetOpenPopups](/uwp/api/windows.ui.xaml.media.visualtreehelper.getopenpopups)<br/>In XAML Islands apps this will throw an error. In AppWindow apps this will return open popups on the main window. | VisualTreeHelper.[GetOpenPopupsForXamlRoot](/uwp/api/windows.ui.xaml.media.visualtreehelper.getopenpopupsforxamlroot)(_uiElement_.XamlRoot) |
| FocusManager.[GetFocusedElement](/uwp/api/windows.ui.xaml.input.focusmanager.getfocusedelement) | FocusManager.[GetFocusedElement](/uwp/api/windows.ui.xaml.input.focusmanager.getfocusedelement#Windows_UI_Xaml_Input_FocusManager_GetFocusedElement_Windows_UI_Xaml_XamlRoot_)(_uiElement_.XamlRoot) |
| contentDialog.ShowAsync() | contentDialog.[XamlRoot](/uwp/api/windows.ui.xaml.uielement.xamlroot) = _uiElement_.XamlRoot;<br/>contentDialog.ShowAsync(); |
| menuFlyout.ShowAt(null, new Point(10, 10)); | menuFlyout.[XamlRoot](/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase.xamlroot) = _uiElement_.XamlRoot;<br/>menuFlyout.ShowAt(null, new Point(10, 10)); |

> [!NOTE]
> For XAML content in a DesktopWindowXamlSource, there does exist a CoreWindow/Window on the thread, but it is always invisible and has a size of 1x1. It is still accessible to the app but won't return meaningful bounds or visibility.
>
>For XAML content in an AppWindow, there will always be exactly one CoreWindow on the same thread. If you call a `GetForCurrentView` or `GetForCurrentThread` API, that API will return an object that reflects the state of the CoreWindow on the thread, not any of the AppWindows that may be running on that thread.


## Do's and don'ts

- Do provide a clear entry point to the secondary view by utilizing the "open new window" glyph.
- Do communicate the purpose of the secondary view to users.
- Do ensure that your app is fully functional in a single view and users will open a secondary view only for convenience.
- Don't rely on the secondary view to provide notifications or other transient visuals.

## Related topics

- [Use AppWindow](app-window.md)
- [Use ApplicationView](application-view.md)
- [ApplicationViewSwitcher](/uwp/api/Windows.UI.ViewManagement.ApplicationViewSwitcher)
- [CreateNewView](/uwp/api/windows.applicationmodel.core.coreapplication.createnewview)