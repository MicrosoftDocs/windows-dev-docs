---
title: Windowing functionality migration
description: This topic contains guidance related to window management, including migrating from UWP's [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**CoreWindow**](/uwp/api/windows.ui.core.corewindow) or [**AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow) to the Window App SDK [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow).
ms.topic: article
ms.date: 09/02/2022
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, windowing
ms.localizationpriority: medium
---

# Windowing functionality migration

This topic contains guidance related to window management, including migrating from UWP's [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**CoreWindow**](/uwp/api/windows.ui.core.corewindow) or [**AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow) to the Window App SDK [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow).

## Important APIs

* [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow)
* [**Windows.UI.Core.CoreWindow.Dispatcher**](/uwp/api/windows.ui.core.corewindow.dispatcher) property
* [**Microsoft.UI.Window.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcherqueue) property

## Summary of API and/or feature differences

The Windows App SDK provides a [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class that's based on the Win32 HWND model. That **AppWindow** class is the Windows App SDK's version of UWP's [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**CoreWindow**](/uwp/api/windows.ui.core.corewindow) and [**AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow).

To take advantage of the Windows App SDK windowing APIs means that you'll migrate your UWP code to use the Win32 model. For more info about the Windows App SDK **AppWindow**, see [Manage app windows](../../windowing/windowing-overview.md).

> [!TIP]
> The [Manage app windows](../../windowing/windowing-overview.md) topic contains a code example demonstrating how to retrieve an **AppWindow** from a WinUI 3 window. In your WinUI 3 app, use that code pattern so that you can call the **AppWindow** APIs mentioned in the rest of this topic.

## Window types in UWP versus the Windows App SDK

In a UWP app, you can host window content using [**ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**CoreWindow**](/uwp/api/windows.ui.core.corewindow), or [**AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow). The work involved in migrating that code to the Windows App SDK depends on which of those two windowing models your UWP app uses. If you're familiar with UWP's [**Windows.UI.WindowManagement.AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow), then you might see similarities between that and [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow).

### UWP window types

* [**Windows.UI.ViewManagement.ApplicationView**](/uwp/api/windows.ui.viewmanagement.applicationview)/[**Windows.UI.Core.CoreWindow**](/uwp/api/windows.ui.core.corewindow).
* [**Windows.UI.WindowManagement.AppWindow**](/uwp/api/windows.ui.windowmanagement.appwindow). **AppWindow** consolidates the UI thread and the window that the app uses to display content. UWP apps that use **AppWindow** will have less work to do than **ApplicationView**/**CoreWindow** apps to migrate to the Windows App SDK **AppWindow**.

### Windows App SDK window type

* [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is the high-level abstraction of a system-managed container of the content of an app.

Keep in mind that the differences in windowing models between UWP and Win32 mean that there's not a direct 1:1 mapping between the UWP API surface and the Windows App SDK API surface. Even for class and member names that *do* carry over from UWP (reflected in this topic's mapping tables), behavior might also differ.

## Splash screens

Unlike UWP apps, Win32 apps don't by default show a splash screen on launch. UWP apps relying on this feature for their launch experience might choose to implement a custom transition to their first app window.

## Create, show, close, and destroy a window

The lifetime of a [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is the same as for an HWND; meaning that the **AppWindow** object is available immediately after the window has been created, and is destroyed when the window is closed.

### Create and show

[**AppWindow.Create**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.create) creates an app window with the default configuration. Creating and showing a window is only necessary for scenarios where you're not working with a UI framework. If you're migrating your UWP app to a Win32-compatible UI framework, then you can still reach your **AppWindow** object from an already-created window by using the windowing interop methods.

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**CoreApplication.CreateNewView**](/uwp/api/windows.applicationmodel.core.coreapplication.createnewview)<br/>or<br/>[**CoreWindow.GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread)|[**AppWindow.TryCreateAsync**](/uwp/api/windows.ui.windowmanagement.appwindow.trycreateasync)|[**AppWindow.Create**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.create)|
|[**CoreWindow.Activate**](/uwp/api/windows.ui.core.corewindow.activate)|[**AppWindow.TryShowAsync**](/uwp/api/windows.ui.windowmanagement.appwindow.tryshowasync)|[**AppWindow.Show**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.show)|

### Close

In UWP, [**ApplicationView.TryConsolidateAsync**](/uwp/api/windows.ui.viewmanagement.applicationview.tryconsolidateasync) is the programmatic equivalent of the user initiating a close gesture. This concept of *consolidation* (in UWP's **ApplicationView**/**CoreWindow** windowing model) doesn't exist in Win32. Win32 doesn't require that windows exist in separate threads. Replicating the UWP's **ApplicationView**/**CoreWindow** windowing model would require the developer to create a new thread, and create a new window there. Under the Win32 model, the default system behavior is **Close** > **Hide** > **Destroy**.

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**ApplicationView.TryConsolidateAsync**](/uwp/api/windows.ui.viewmanagement.applicationview.tryconsolidateasync)|[**AppWindow.CloseAsync**](/uwp/api/windows.ui.windowmanagement.appwindow.closeasync)|[**AppWindow.Destroy**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.destroy)|

## Basic window customization

As you migrate from UWP to the Windows App SDK, you can expect the same experience from your default **AppWindow**. But if needed, you can change the default **Microsoft.UI.Windowing.AppWindow** for customized windowing experiences. See [**Microsoft.UI.Windowing.AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) for more info on how to customize your windows.

### Resizing a window

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**ApplicationView.TryResizeView**](/uwp/api/windows.ui.viewmanagement.applicationview.tryresizeview)|[**AppWindow.RequestSize**](/uwp/api/windows.ui.windowmanagement.appwindow.requestsize)|[**AppWindow.Resize**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.resize)|
|[**CoreWindow.Bounds**](/uwp/api/windows.ui.core.corewindow.bounds) (commonly appears in C# as `CoreWindow.GetForCurrentThread.Bounds`)|[**AppWindowPlacement.Size**](/uwp/api/windows.ui.windowmanagement.appwindowplacement.size)|[**AppWindow.Size**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.size)|

### Positioning a window

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|Not possible|[**AppWindow.GetPlacement**](/uwp/api/windows.ui.windowmanagement.appwindow.getplacement)|[**AppWindow.Position**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.position)|
|Not possible|[**Appwindow.RequestMoveXxx**](/uwp/api/windows.ui.windowmanagement.appwindow.requestmoveadjacenttocurrentview)|[**AppWindow.Move**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.move)|

### Window title

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**ApplicationView.Title**](/uwp/api/windows.ui.viewmanagement.applicationview.title)|[**AppWindow.Title**](/uwp/api/windows.ui.windowmanagement.appwindow.title)|[**AppWindow.Title**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.title)|

## Compact overlay, and full-screen

Apps that enter into compact overlay, or full-screen, should take advantage of the Windows App SDK [**AppWindowPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenter). If you're familiar with the UWP **AppWindow**, then you might already be familiar with the concept of presenters.

There isn't a 1:1 mapping of functionality and behavior from UWP app window presenters to Windows App SDK app window presenters. If you have a UWP **ApplicationView**/**CoreWindow** app, then you can still have compact overlay (picture-in-picture) or full-screen windowing experiences in your app, but the concept of presenters might be new to you. For more information on app window presenters, see [Presenters](../../windowing/windowing-overview.md). By default, an overlapped presenter is applied to an **AppWindow** at creation time. **CompactOverlay** and **FullScreen** are the only available presenters, aside from default.

### Compact overlay

If you used UWP's **ApplicationViewMode** or **AppWindowPresentionKind** to present a compact overlay window, then you should use the compact overlay **AppWindowPresenterKind**. The [**Microsoft.UI.Windowing.CompactOverlayPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.compactoverlaypresenter) supports only three fixed window sizes at a 16:9 aspect ratio, and can't be resized by the user. Instead of **ApplicationViewMode.TryEnterViewModeAsync** or **AppWindowPresenterKind.RequestPresentation**, you should use [**AppWindow.SetPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter#Microsoft_UI_Windowing_AppWindow_SetPresenter_Microsoft_UI_Windowing_AppWindowPresenter_) to change the presentation of the **AppWindow**.

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**ApplicationViewMode.CompactOverlay**](/uwp/api/windows.ui.viewmanagement.applicationviewmode)|[**AppWindowPresentationKind.CompactOverlay**](/uwp/api/windows.ui.windowmanagement.appwindowpresentationkind)|[**AppWindowPresenterKind.CompactOverlay**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenterkind)|
|[**ApplicationView.TryEnterViewModeAsync**](/uwp/api/windows.ui.viewmanagement.applicationview.tryenterviewmodeasync) with **ApplicationViewMode.CompactOverlay**|[**AppWindowPresenter.RequestPresentation**](/uwp/api/windows.ui.windowmanagement.appwindowpresenter.requestpresentation#Windows_UI_WindowManagement_AppWindowPresenter_RequestPresentation_Windows_UI_WindowManagement_AppWindowPresentationKind_) with **AppWindowPresenterKind.CompactOverlay**|[**AppWindow.SetPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter#Microsoft_UI_Windowing_AppWindow_SetPresenter_Microsoft_UI_Windowing_AppWindowPresenterKind_) with **AppWindowPresenterKind.CompactOverla**y|

### Full-screen

If you used UWP's **ApplicationViewWindowingMode** or **AppWindowPresentionKind** classes to present a full-screen window, then you should use the full-screen **AppWindowPresenterKind**. The Windows App SDK supports only the most restrictive full-screen experience (that is, when **FullScreen** is **IsExclusive**). For **ApplicationView**/**CoreWindow**, you can use the [**ApplicationView.ExitFullScreenMode**](/uwp/api/windows.ui.viewmanagement.applicationview.exitfullscreenmode) to take the app out of full-screen. When using presenters, you can take an app out of full-screen by setting the presenter back to overlapped/default by using [**AppWindow.SetPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter#Microsoft_UI_Windowing_AppWindow_SetPresenter_Microsoft_UI_Windowing_AppWindowPresenterKind_).

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**ApplicationViewWindowingMode.FullScreen**](/uwp/api/windows.ui.viewmanagement.applicationviewwindowingmode)|[**AppWindowPresentationKind.FullScreen**](/uwp/api/windows.ui.windowmanagement.appwindowpresentationkind)|[**AppWindowPresenterKind.FullScreen**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowpresenterkind)|
|[**ApplicationView.TryEnterFullScreenMode**](/uwp/api/windows.ui.viewmanagement.applicationview.tryenterfullscreenmode)|[**AppWindowPresenter.RequestPresentation**](/uwp/api/windows.ui.windowmanagement.appwindowpresenter.requestpresentation#Windows_UI_WindowManagement_AppWindowPresenter_RequestPresentation_Windows_UI_WindowManagement_AppWindowPresentationKind_) with **AppWindowPresenterKind.FullScreen**|[**AppWindow.SetPresenter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.setpresenter#Microsoft_UI_Windowing_AppWindow_SetPresenter_Microsoft_UI_Windowing_AppWindowPresenterKind_) with **AppWindowPresenterKind.FullScreen**|

For more details on how to work with app window presenters, see the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing). It demonstrates how to toggle different app window presenter states.

## Custom title bar

> [!NOTE]
> Title bar customization APIs currently work on Windows 11 only. We recommend that you check [**AppWindowTitleBar.IsCustomizationSupported**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) in your code before you call these APIs.

If your app uses a default title bar, then there's no additional title bar work needed when you migrate to Win32. If on the other hand your UWP app has a custom title bar, then it's possible to recreate the following scenarios in your Windows App SDK app.

1. Customize the system-drawn title bar
2. App-drawn custom title bar

Code that uses the UWP [**ApplicationViewTitleBar**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar), [**CoreApplicationViewTitleBar**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar), and [**AppWindowTitleBar**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar) classes migrates to using the Windows App SDK [**Microsoft.UI.Windowing.AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) class.

### Customize the system-drawn title bar

Here's a table of the color customization APIs.

> [!NOTE]
> When [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) is `true`, transparency is supported only for the following properties: **AppWindowTitleBar.ButtonBackgroundColor**, **AppWindowTitleBar.ButtonInactiveBackgroundColor**, **AppWindowTitleBar.ButtonPressedBackgroundColor**, **AppWindowTitleBar.ButtonHoverBackgroundColor** and **AppWindowTitleBar.BackgroundColor** (implicitly set).

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|Properties of [**ApplicationViewTitleBar**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar)|Properties of [**AppWindowTitleBar**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar)|Properties of [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar)|
|[**BackgroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.backgroundcolor)|[**BackgroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.backgroundcolor)|[**BackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.backgroundcolor)|
|[**ButtonBackgroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonbackgroundcolor)|[**ButtonBackgroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttonbackgroundcolor)|[**ButtonBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonbackgroundcolor)|
|[**ButtonForegroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonforegroundcolor)|[**ButtonForegroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttonforegroundcolor)|[**ButtonForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonforegroundcolor)|
|[**ButtonHoverBackgroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonhoverbackgroundcolor)|[**ButtonHoverBackgroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttonhoverbackgroundcolor)|[**ButtonHoverBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonhoverbackgroundcolor)|
|[**ButtonHoverForegroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonhoverforegroundcolor)|[**ButtonHoverForegroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttonhoverforegroundcolor)|[**ButtonHoverForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonhoverforegroundcolor)|
|[**ButtonInactiveBackgroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttoninactivebackgroundcolor)|[**ButtonInactiveBackgroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttoninactivebackgroundcolor)|[**ButtonInactiveBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttoninactivebackgroundcolor)|
|[**ButtonInactiveForegroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttoninactiveforegroundcolor)|[**ButtonInactiveForegroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttoninactiveforegroundcolor)|[**ButtonInactiveForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttoninactiveforegroundcolor)|
|[**ButtonPressedBackgroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonpressedbackgroundcolor)|[**ButtonPressedBackgroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttonpressedbackgroundcolor)|[**ButtonPressedBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonpressedbackgroundcolor)|
|[**ButtonPressedForegroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.buttonpressedforegroundcolor)|[**ButtonPressedForegroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.buttonpressedforegroundcolor)|[**ButtonPressedForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.buttonpressedforegroundcolor)|
|[**ForegroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.foregroundcolor)|[**ForegroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.foregroundcolor)|[**ForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.foregroundcolor)|
|[**InactiveBackgroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.inactivebackgroundcolor)|[**InactiveBackgroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.inactivebackgroundcolor)|[**InactiveBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactivebackgroundcolor)|
|[**InactiveForegroundColor**](/uwp/api/windows.ui.viewmanagement.applicationviewtitlebar.inactiveforegroundcolor)|[**InactiveForegroundColor**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.inactiveforegroundcolor)|[**InactiveForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactiveforegroundcolor)|

These Windows App SDK APIs are for further customization of the system-drawn title bar in addition to the [**AppWindow.Title**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.title) API.

* [**AppWindow.SetIcon**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon). Sets the title bar and taskbar icon picture using either an hIcon handle or a string path to a resource or to a file.
* [**AppWindowTitleBar.IconShowOptions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions). Gets or sets a value that specifies how the window icon is displayed in the title bar. Supports two values currently&mdash;**HideIconAndSystemMenu** and **ShowIconAndSystemMenu**.
* [**AppWindowTitleBar.ResetToDefault**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.resettodefault). Resets the current title bar back to the default settings for the window.

### App-drawn custom title bar (full customization)

If you're migrating to using **AppWindowTitleBar**, then we recommend that you check [**AppWindowTitleBar.IsCustomizationSupported**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) in your code before calling the following custom title bar APIs.

|UWP ApplicationView/CoreWindow|Windows App SDK AppWindow|
|-|-|
|[**CoreApplicationViewTitleBar.ExtendViewIntoTitleBar**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.extendviewintotitlebar)|[**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar)<br/>The platform continues to draw the **Minimize**/**Maximize**/**Close** buttons for you, and reports the occlusion information.|
|[**CoreApplicationViewTitleBar.SystemOverlayLeftInset**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.systemoverlayleftinset)|[**AppWindowTitleBar.LeftInset**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.leftinset)|
|[**CoreApplicationViewTitleBar.SystemOverlayRightInset**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.systemoverlayrightinset)|[**AppWindowTitleBar.RightInset**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.rightinset)|
|[**CoreApplicationViewTitleBar.Height**](/uwp/api/windows.applicationmodel.core.coreapplicationviewtitlebar.height)|[**AppWindowTitleBar.Height**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height)|
|[**AppWindowTitleBarOcclusion**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebarocclusion)<br/>[**AppWindowTitleBar.GetTitleBarOcclusions**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.gettitlebarocclusions)|Represents the system-reserved regions of the app window that will occlude app content if **ExtendsContentIntoTitleBar** is true. The Windows App SDK **AppWindow** left and right inset information, coupled with title bar height, provide the same information.<br/>[**AppWindowTitleBar.LeftInset**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.leftinset), [**AppWindowTitleBar.RightInset**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.rightinset), [**AppWindowTitleBar.Height**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height)|

These Windows App SDK APIs are for full title bar customization.

* [**AppWindowTitleBar.SetDragRectangles**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles). Sets the drag regions for the window.
* [**AppWindowTitleBar.ResetToDefault**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.resettodefault). Resets the current title bar back to the default settings for the window.

These UWP **AppWindow** APIs have no direct 1:1 mapping to a Windows App SDK API.

* [**AppWindowTitleBarVisibility**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebarvisibility). Defines constants that specify the preferred visibility of an **AppWindowTitleBar**.
* [**AppWindowTitleBar.GetPreferredVisibility**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.getpreferredvisibility). Retrieves the preferred visibility mode for the title bar.
* [**AppWindowTitleBar.SetPreferredVisibility**](/uwp/api/windows.ui.windowmanagement.appwindowtitlebar.setpreferredvisibility). Sets the preferred visibility mode for the title bar.

For more details on how to work with **AppWindowTitleBar**, see the [Windowing gallery sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing). It demonstrates how to create a custom color title bar and how to draw a custom title bar.

## Event handling

If your UWP app uses the [**AppWindow.Changed**](/uwp/api/windows.ui.windowmanagement.appwindow.changed) event, then you can migrate that code to the [**Microsoft.UI.Windowing.AppWindow.Changed**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.changed) event.

### Size changed event

When migrating size changed event-handling code, you should switch to using the Windows App SDK [**AppWindowChangedEventArgs.DidSizeChange**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didsizechange) property. The value is `true` if the size of the app window changed, otherwise it's `false`.

|UWP ApplicationView/CoreWindow|UWP AppWindow|Windows App SDK|
|-|-|-|
|[**CoreWindow.SizeChanged**](/uwp/api/windows.ui.core.corewindow.sizechanged)|[**AppWindowChangedEventArgs.DidSizeChange**](/uwp/api/windows.ui.windowmanagement.appwindowchangedeventargs.didsizechange)|[**AppWindowChangedEventArgs.DidSizeChange**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowchangedeventargs.didsizechange)|

## MainPage and MainWindow

When you create a new UWP project in Visual Studio, the project template provides you with a **MainPage** class. For your app, you might have renamed that class (and/or added more pages and user controls). The project template also provides you with navigation code in the methods of the **App** class.

When you create a new Windows App SDK project in Visual Studio, the project template provides you with a **MainWindow** class (of type [**Microsoft.UI.Xaml.Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window)), but no **Page**. And the project template doesn't provide any navigation code.

However, you have the option to add pages and user controls to your Windows App SDK project. For example, you could add a new page item to the project (**WinUI** > **Blank Page (WinUI 3)**), and name it `MainPage.xaml`, or some other name. That would add to your project a new class of type [**Microsoft.UI.Xaml.Controls.Page**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page). Then, for info about adding navigation code to the project, see [Do I need to implement page navigation?](winui3.md#do-i-need-to-implement-page-navigation).

For Windows App SDK apps that are simple enough, you needn't create pages or user controls, and you can copy your XAML markup and code-behind into **MainWindow**. But for info about exceptions to that workflow, see [Visual State Manager, and Page.Resources](winui3.md#visual-state-manager-and-pageresources).

## Change CoreWindow.Dispatcher to Window.DispatcherQueue

Some use cases for UWP's [**Windows.UI.Core.CoreWindow**](/uwp/api/windows.ui.core.corewindow) class migrate to the Windows App SDK's [**Microsoft.UI.Xaml.Window**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window).

For example, if you're using the [**Windows.UI.Core.CoreWindow.Dispatcher**](/uwp/api/windows.ui.core.corewindow.dispatcher) property in your UWP app, then the solution is *not* to migrate to the [**Microsoft.UI.Xaml.Window.Dispatcher**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcher) property (which always returns null). Instead, migrate to the [**Microsoft.UI.Xaml.Window.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcherqueue) property, which returns a [**Microsoft.UI.Dispatching.DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.dispatcherqueue).

For more info, and code examples, see [Change Windows.UI.Core.CoreDispatcher to Microsoft.UI.Dispatching.DispatcherQueue](threading.md#change-windowsuicorecoredispatcher-to-microsoftuidispatchingdispatcherqueue).

## Related topics

* [Windows App SDK and supported Windows releases](../../support.md)
* [Migrate your threading functionality](threading.md)
