---
title: Preview release channel for the Windows App SDK 1.4
description: Provides info about the preview release channel for the Windows App SDK 1.4.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Preview channel release notes for the Windows App SDK 1.4

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](../release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest preview channel release:**

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Version 1.4 Preview 2 (1.4.0-preview2)

This is the latest release of the preview channel for version 1.4.

In an existing Windows App SDK 1.3 (from the stable channel) app, you can update your Nuget package to 1.4.0-preview2 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

### XAML Islands no longer experimental

XAML Islands and the underlying ContentIslands platform are no longer experimental.

- Currently XAML Islands are only tested for use in C++ apps. This release does not include any convenient wrapper elements for use in WPF or WinForms.
- `DesktopWindowXamlSource` and related types have been added in the Microsoft.UI.Xaml.Hosting namespace for XAML Islands. `XamlRoot.ContentIslandEnvironment` was added to help access the underlying Island information for an element.
- Many new types have been introduced in the Microsoft.UI.Content namespace and the Microsoft.UI.Input namespace as the underlying support for XAML Islands or for using this ContentIslands functionality without XAML.
- A new `DragDropManager` (plus related types) has been added in the Microsoft.UI.Input.DragDrop namespace for Island scenarios.

### ItemsView updates

The new `ItemsView` class that was introduced in version 1.4-preview1 has been substantially updated with new properties and a new supporting class.

- The new `ItemsView` control displays a data collection. `ItemsView` is similar to the `ListView` and `GridView` controls, but is built using the `ItemsRepeater`, `ScrollView`, `ItemContainer` and `ItemCollectionTransitionProvider` components. It offers the unique ability to plug in custom `Layout` or `ItemCollectionTransitionProvider` implementations. Another key advantage is the ability to switch the layout on the fly while preserving items selection. The inner `ScrollView` control also offers features unavailable in `ListView`/`GridView`'s `ScrollViewer` control such as the ability to control the animation during programmatic scrolls.
  - A new `ItemTransitionProvider` property on `ItemsRepeater` (and the new `ItemsView` control) lets you specify an `ItemCollectionTransitionProvider` object to control transition animations on that control. A `CreateDefaultItemTransitionProvider` method has also been added to `Layout`, which enables a layout object to provide a fallback transition to accompany it if you do not provide one explicitly on the `ItemsView` control.
  - A new `IndexBasedLayoutOrientation` property on `Layout` where the layout orientation, if any, of items is based on their index in the source collection. The default value is `IndexBasedLayoutOrientation.None`. Custom layouts set this property by calling the new (protected) `SetIndexBasedLayoutOrientation` method.
  - A new `VisibleRect` property on `VirtualizingLayoutContext` gets the visible viewport rectangle within the `FrameworkElement` associated with the `Layout`. The protected virtual `VirtualizingLayoutContext.VisibleRectCore` method can be overridden to provide the value that will be returned from the `VisibleRect` property.
- The new `LinedFlowLayout` class is typically used to lay out the items of the `ItemsView` collection control. It is particularly useful for displaying collection of pictures. It does so by laying them out from left to right, and top to bottom, in lines of equal height. The pictures fill a horizontal line and then wrap to a next line. Pictures may be cropped at the left and right edges to fit into a line. They may also be expanded horizontally and cropped at the top and bottom edges to fill a line when the stretching mode is employed.

### New features and updates from across the WinAppSDK

- `Popup/FlyoutBase.IsConstrainedToRootBounds = false` is now supported, allowing a popup/flyout to extend outside the bounds of the parent window. A `SystemBackdrop` property has been added to these types to support having acrylic in these unconstrained popups. Menus by default use this to have acrylic.
- `Closed`, `FrameworkClosed`, and `IsClosed` have been added to `DesktopAcrylicController` and `MicaController` to improve handling during object/thread shutdown.
- `DesktopAcrylicController.Kind` can now be set to choose between some standard acrylic appearances.
- `DispatcherQueue` has some new events and helpers to facilitate better organized shutdown and for apps using Islands to easily run a standard supported event loop.
- `InputNonClientPointerSource` in the Microsoft.UI.Input namespace can be used for custom titlebar scenarios to define non-client area regions. Code can register for corresponding events like hover and click events on these regions.
- `AppWindow` has some new helpers to get and associate with a `DispatcherQueue`.
- The new `TreeView.SelectionChanged` event allows developers to respond when the user or code-behind changes the set of selected nodes in the `TreeView` control.
- The new `ScrollView` control provides a new alternative to `ScrollViewer`. This new control is highly aligned in behavior and API with the existing `ScrollViewer` control, but is based on `InteractionTracker`, has new features such as animation-driven view changes, and is also designed to ensure full functionality of `ItemsRepeater`. See [A more flexible ScrollViewer · Issue #108 · microsoft/microsoft-ui-xaml (github.com)](https://github.com/Microsoft/microsoft-ui-xaml/issues/108) for more details. Various new types, including `ScrollPresenter`, are part of the overall `ScrollView` model.
- The new `AnnotatedScrollBar` control extends a regular scrollbar's functionality by providing an easy way to navigate through a large collection of items. This is achieved through a clickable rail with labels that act as markers. It also allows for a more granular understanding of the scrollable content by displaying a tooltip when hovering over the clickable rail.

### New APIs in 1.4.0-preview2

Version 1.4-preview2 includes the following new APIs compared to the previous 1.4-preview1 release:

```C#
Microsoft.UI
 
    ClosableNotifierHandler
    IClosableNotifier
```

```C#
Microsoft.UI.Composition.SystemBackdrops
 
    DesktopAcrylicController
        Closed
        FrameworkClosed
        IsClosed
        Kind
 
    DesktopAcrylicKind
    MicaController
        Closed
        FrameworkClosed
        IsClosed
```

```C#
Microsoft.UI.Content
 
    ContentCoordinateConverter
    ContentCoordinateRoundingMode
    ContentDeferral
    ContentEnvironmentSettingChangedEventArgs
    ContentEnvironmentStateChangedEventArgs
    ContentIsland
    ContentIslandAutomationProviderRequestedEventArgs
    ContentIslandEnvironment
    ContentIslandStateChangedEventArgs
    ContentLayoutDirection
    ContentSite
    ContentSiteEnvironment
    ContentSiteEnvironmentView
    ContentSiteRequestedStateChangedEventArgs
    ContentSiteView
    ContentSizePolicy
    DesktopChildSiteBridge
    DesktopSiteBridge
    IContentSiteBridge
```

```C#
Microsoft.UI.Dispatching
 
    DispatcherExitDeferral
    DispatcherQueue
        EnqueueEventLoopExit
        EnsureSystemDispatcherQueue
        FrameworkShutdownCompleted
        FrameworkShutdownStarting
        RunEventLoop
        RunEventLoop
 
    DispatcherQueueController
        ShutdownQueue
 
    DispatcherRunOptions
```

```C#
Microsoft.UI.Input
 
    CharacterReceivedEventArgs
    ContextMenuKeyEventArgs
    FocusChangedEventArgs
    InputActivationListener
        GetForIsland
 
    InputFocusChangedEventArgs
    InputFocusController
    InputKeyboardSource
        CharacterReceived
        ContextMenuKey
        GetCurrentKeyState
        GetForIsland
        GetKeyState
        KeyDown
        KeyUp
        SystemKeyDown
        SystemKeyUp
 
    InputNonClientPointerSource
    InputPointerSource
        GetForIsland
 
    InputPreTranslateKeyboardSource
    KeyEventArgs
    NonClientCaptionTappedEventArgs
    NonClientPointerEventArgs
    NonClientRegionKind
    NonClientRegionsChangedEventArgs
    PhysicalKeyStatus
    VirtualKeyStates
```

```C#
Microsoft.UI.Input.DragDrop
 
    DragDropManager
    DragDropModifiers
    DragInfo
    DragOperation
    DragUIContentMode
    DragUIOverride
    DropOperationTargetRequestedEventArgs
    IDropOperationTarget
```

```C#
Microsoft.UI.Windowing
 
    AppWindow
        AssociateWithDispatcherQueue
        Create
        DispatcherQueue
```

```C#
Microsoft.UI.Xaml
 
    XamlRoot
        ContentIslandEnvironment
```

```C#
Microsoft.UI.Xaml.Automation.Peers
 
    ItemsViewAutomationPeer
```

```C#
Microsoft.UI.Xaml.Controls
 
    AnnotatedScrollBar
    AnnotatedScrollBarDetailLabelRequestedEventArgs
    AnnotatedScrollBarLabel
    AnnotatedScrollBarScrollingEventArgs
    AnnotatedScrollBarScrollingEventKind
    IndexBasedLayoutOrientation
    ItemCollectionTransition
    ItemCollectionTransitionCompletedEventArgs
    ItemCollectionTransitionOperation
    ItemCollectionTransitionProgress
    ItemCollectionTransitionProvider
    ItemCollectionTransitionTriggers
    ItemsRepeater
        ItemTransitionProvider
        ItemTransitionProviderProperty
 
    ItemsView
    ItemsViewItemInvokedEventArgs
    ItemsViewSelectionChangedEventArgs
    ItemsViewSelectionMode
    Layout
        CreateDefaultItemTransitionProvider
        IndexBasedLayoutOrientation
        SetIndexBasedLayoutOrientation
 
    LinedFlowLayout
    LinedFlowLayoutItemCollectionTransitionProvider
    LinedFlowLayoutItemsInfoRequestedEventArgs
    LinedFlowLayoutItemsJustification
    LinedFlowLayoutItemsStretch
    ScrollingAnchorRequestedEventArgs
    ScrollingAnimationMode
    ScrollingBringingIntoViewEventArgs
    ScrollingChainMode
    ScrollingContentOrientation
    ScrollingInputKinds
    ScrollingInteractionState
    ScrollingRailMode
    ScrollingScrollAnimationStartingEventArgs
    ScrollingScrollBarVisibility
    ScrollingScrollCompletedEventArgs
    ScrollingScrollMode
    ScrollingScrollOptions
    ScrollingSnapPointsMode
    ScrollingZoomAnimationStartingEventArgs
    ScrollingZoomCompletedEventArgs
    ScrollingZoomMode
    ScrollingZoomOptions
    ScrollView
    TreeView
        SelectionChanged
 
    TreeViewSelectionChangedEventArgs
    VirtualizingLayoutContext
        VisibleRect
        VisibleRectCore
```

```C#
Microsoft.UI.Xaml.Controls.Primitives
 
    FlyoutBase
        SystemBackdrop
        SystemBackdropProperty
 
    IScrollController
    IScrollControllerPanningInfo
    Popup
        SystemBackdrop
        SystemBackdropProperty
 
    RepeatedScrollSnapPoint
    RepeatedZoomSnapPoint
    ScrollControllerAddScrollVelocityRequestedEventArgs
    ScrollControllerPanRequestedEventArgs
    ScrollControllerScrollByRequestedEventArgs
    ScrollControllerScrollToRequestedEventArgs
    ScrollPresenter
    ScrollSnapPoint
    ScrollSnapPointBase
    ScrollSnapPointsAlignment
    SnapPointBase
    ZoomSnapPoint
    ZoomSnapPointBase
```

```C#
Microsoft.UI.Xaml.Hosting
 
    DesktopWindowXamlSource
    DesktopWindowXamlSourceGotFocusEventArgs
    DesktopWindowXamlSourceTakeFocusRequestedEventArgs
    WindowsXamlManager
    XamlSourceFocusNavigationReason
    XamlSourceFocusNavigationRequest
    XamlSourceFocusNavigationResult
```

## Version 1.4 Preview 1 (1.4.0-preview1)

This is the latest release of the preview channel for version 1.4.

In an existing Windows App SDK 1.3 (from the stable channel) app, you can update your Nuget package to 1.4.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

### Widgets updates

Three new interfaces have been added for Widget Providers to implement: `IWidgetProvider2`, `IWidgetProviderAnalytics`, and `IWidgetProviderErrors`. `IWidgetProvider2` allows providers to respond to the *Customize* action invoked by the user, which is identical to what is available for 1st party Widgets. The `IWidgetProviderAnalytics` and `IWidgetProviderErrors` interfaces are used by providers to gather telemetry for their widgets; analytics and failure events about widgets are communicated to the respective widget providers. The `WidgetCustomizationRequestedArgs`, `WidgetAnalyticsInfoReportedArgs`, and `WidgetErrorInfoReportedArgs` classes are used to communicate relevant information to support new functionalities.

### New features from across the WinAppSDK

- A new `ThemeSettings` class that allows Win32 WinRT apps to detect when the system's High Contrast setting has changed, similar to UWP's [AccessibilitySettings](/uwp/api/windows.ui.viewmanagement.accessibilitysettings) class. See the [ThemeSettings API spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/themes/ThemeSettings.md) on GitHub for more information.
- `Popup/FlyoutBase.ShouldConstrainToRootBounds` is now supported to allow tooltips, menus, and other popups to extend outside the bounds of the main window. Preview 1 does not yet fully support having Acrylic or other SystemBackdrops on a popup/flyout; additional APIs and implementation for this will be included in the next 1.4 release.
- `AccessKeyManager.EnterDisplayMode` is a new method to display access keys for the current focused element of a provided root. Access keys are in "display mode" when showing a key tip to invoke a command, such as pressing the Alt key in Paint to show what keys correspond to what controls. This method allows for programmatically entering display mode.
- `Application.ResourceManagerRequested` provides a mechanism to provide a different `IResourceManager` to resolve resource URIs for scenarios when the default `ResourceManager` won't work. For more information, see the [Application.ResourceManagerRequested API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/custom-iresourcemanager-spec.md) on GitHub.
- We're introducing a new list control called the `ItemsView` and a corresponding concrete `ItemContainer` class. `ItemContainer` is a lightweight container with built-in selection states and visuals, which can easily wrap desired content and be used with `ItemsView` for a collection control scenario. `ItemsView` is still marked experimental in Preview 1 but will be included in the next 1.4 release.
- The version of the WebView2 SDK was updated from 1661.34 to [1823.32](/microsoft-edge/webview2/release-notes?tabs=winrtcsharp#10182332).

### New APIs in 1.4.0-preview1

Version 1.4-preview1 includes the following new APIs compared to the stable 1.3 release:

```C#
Microsoft.UI.System
 
    ThemeSettings
```

```C#
Microsoft.UI.Xaml
 
    Application
        ResourceManagerRequested
 
    ResourceManagerRequestedEventArgs
```

```C#
Microsoft.UI.Xaml.Automation.Peers
 
    ItemContainerAutomationPeer
```

```C#
Microsoft.UI.Xaml.Controls
 
    ItemContainer
```

```C#
Microsoft.UI.Xaml.Controls.Primitives
 
    CommandBarFlyoutCommandBar
        SystemBackdrop
        SystemBackdropProperty
```

```C#
Microsoft.UI.Xaml.Input
 
    AccessKeyManager
        EnterDisplayMode
```

```C#
Microsoft.Web.WebView2.Core
 
    CoreWebView2
        LaunchingExternalUriScheme
        MemoryUsageTargetLevel
 
    CoreWebView2File
    CoreWebView2LaunchingExternalUriSchemeEventArgs
    CoreWebView2MemoryUsageTargetLevel
    CoreWebView2PermissionKind
        WindowManagement
 
    CoreWebView2Profile
        CookieManager
        IsGeneralAutofillEnabled
        IsPasswordAutosaveEnabled
 
    CoreWebView2Settings
        IsReputationCheckingRequired
 
    CoreWebView2WebMessageReceivedEventArgs
        AdditionalObjects
```

```C#
Microsoft.Windows.Widgets.Providers
 
    IWidgetProvider2
    IWidgetProviderAnalytics
    IWidgetProviderErrors
    WidgetAnalyticsInfoReportedArgs
    WidgetCustomizationRequestedArgs
    WidgetErrorInfoReportedArgs
```

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
