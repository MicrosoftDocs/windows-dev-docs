---
title: Preview release channel for the Windows App SDK 
description: Provides info about the preview release channel for the Windows App SDK.
ms.topic: article
ms.date: 09/06/2022
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Preview channel release notes for the Windows App SDK

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel provides a preview of the next upcoming stable release. There may be breaking API changes between a given preview channel release and the next stable release. Preview channel releases do not include experimental APIs.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

## Version 1.5 Preview 1 (1.5.0-preview1)

This is the latest release of the preview channel for version 1.5.

In an existing Windows App SDK 1.4 (from the stable channel) app, you can update your Nuget package to 1.5.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### XAML Islands runtime and shutdown updates

- There is a behavioral difference between WinAppSDK 1.4 and WinAppSDK 1.5 for Xaml Islands-based apps when the last Xaml Window on any thread is closed.
    - In WinAppSDK 1.4, the Xaml runtime always exits the thread's event loop when the last Xaml window on a thread is closed.
    - In WinAppSDK 1.5:
        - If your app is a WinUI Desktop app, the default behavior is still the same as in WinAppSDK 1.4.
        - If you're using Xaml for the DesktopWindowXamlSource ("Xaml Islands") API, the default behavior is now that Xaml does not automatically exit the thread's event loop.
        - In both modes, you can change this behavior by setting the `Application.DispatcherShutdownMode` property.
    - For more information, see the documentation for the `Application.DispatcherShutdownMode` property when available.
- There is a behavioral difference between WinAppSDK 1.4 and WinAppSDK 1.5 for Xaml Islands-based apps in the lifetime of the Xaml runtime:
    - In WinAppSDK 1.4, the Xaml runtime shuts down on a thread if either all `WindowsXamlManager` and `DesktopWindowXamlSource` objects on a given thread are closed or shut down, or the `DispatcherQueue` running on that thread is shut down (the Xaml runtime shuts down during the `DispatcherQueue.FrameworkShutdownStarting` stage).
    - In WinAppSDK 1.5, the Xaml runtime shuts down on a thread only when the DispatcherQueue running on that thread is shut down (the Xaml runtime shuts   down during the `DispatcherQueue.FrameworkShutdownStarting` stage).
    - For more information, see the documentation for the `WindowsXamlManager` class when available.

### WinUI Maps control

The initial release of the WinUI `Maps` control is now available! This control is powered by WebView2 and Azure Maps, providing the following features:

- Panning and zooming with either the map buttons or touch.
- Changing the style of the map to satellite, terrain, or street view. 
- Programatically adding interactable pins with developer-customizable icons to the map. 
- Developer customization for where the map is centered on initial load.
- Control for developers over hiding or showing the buttons for panning, zooming, and map styles.

> [!NOTE]
> To use the `Maps` control, you'll need an Azure Maps key. To create the key, see the [Azure Maps documentation page for creating a web app](/azure/azure-maps/quick-demo-map-app#get-the-subscription-key-for-your-account).

The `Maps` control is entirely new and we welcome your feedback to evaluate its future direction!

### Other new features from across the WinAppSDK

- Added support for the PublishSingleFile deployment model. For more info about PublishSingleFile, see the [Single-file deployment documentation](/dotnet/core/deploying/single-file/overview).

### Bug fixes

- Fixed an issue from the 1.5-experimental2 release where the projection DLL was not generated. For more info, see GitHub issue [#4152](https://github.com/microsoft/WindowsAppSDK/issues/4152).
- Fixed an issue where the ellipsis button on the text formatting popup of the `RichEditBox` was not displaying the list of actions properly. For more info, see GitHub issue [#9140](https://github.com/microsoft/microsoft-ui-xaml/issues/9140).
- Fixed an issue where `ListView` didn't handle keyboard accelerators properly. For more info, see GitHub issue [#8063](https://github.com/microsoft/microsoft-ui-xaml/issues/8063).
- Fixed an access violation issue with using `AccessKey` to close a window. For more info, see GitHub issue [#8648](https://github.com/microsoft/microsoft-ui-xaml/issues/8648).
- Fixed an issue affecting text alignment in a `MenuFlyoutItem` within a `MenuBar`. For more info, see GitHub issue [#8755](https://github.com/microsoft/microsoft-ui-xaml/issues/8755).
- Fixed an issue where highlighted text would not remain highlighted upon right-click. For more info, see GitHub issue [#1801](https://github.com/microsoft/microsoft-ui-xaml/issues/1801).
- Fixed an issue causing inactive windows to crash the app when closed. For more info, see GitHub issue [#8913](https://github.com/microsoft/microsoft-ui-xaml/issues/8913).
- Fixed an issue that could hang applications when scrolling with the middle mouse button and left-clicking immediately afterwards. For more info, see GitHub issue [#9233](https://github.com/microsoft/microsoft-ui-xaml/issues/9233).

### New APIs in 1.5.0-preview1

Version 1.5-preview1 includes the following new APIs compared to the stable 1.4 release:

```C#
Microsoft.Graphics.DirectX
 
    DirectXPixelFormat
        A4B4G4R4
```

```C#
Microsoft.UI.Input
 
    FocusNavigationReason
    FocusNavigationRequest
    FocusNavigationRequestEventArgs
    FocusNavigationResult
    InputFocusController
        DepartFocus
        NavigateFocusRequested
 
    InputFocusNavigationHost
```

```C#
Microsoft.UI.Xaml
 
    Application
        DispatcherShutdownMode

    DebugSettings
        LayoutCycleDebugBreakLevel
        LayoutCycleTracingLevel

    DispatcherShutdownMode
    LayoutCycleDebugBreakLevel
    LayoutCycleTracingLevel
```

```C# 
Microsoft.UI.Xaml.Controls
 
    MapControl
    MapControlMapServiceErrorOccurredEventArgs
    MapElement
    MapElementClickEventArgs
    MapElementsLayer
    MapIcon
    MapLayer
    SelectorBar
    SelectorBarItem
    SelectorBarSelectionChangedEventArgs
    WebView2
        EnsureCoreWebView2Async
        EnsureCoreWebView2Async
```

```C#
Microsoft.UI.Xaml.Hosting
 
    WindowsXamlManager
        GetForCurrentThread
        XamlShutdownCompletedOnThread
 
    XamlShutdownCompletedOnThreadEventArgs
```

```C#
Microsoft.Web.WebView2.Core
 
    CoreWebView2
        FrameId

    CoreWebView2AcceleratorKeyPressedEventArgs
        IsBrowserAcceleratorKeyEnabled

    CoreWebView2BrowserExtension
    CoreWebView2BrowsingDataKinds
        ServiceWorkers

    CoreWebView2CustomSchemeRegistration
        CoreWebView2CustomSchemeRegistration (String)
        AllowedOrigins
        SchemeName

    CoreWebView2Environment
        GetProcessExtendedInfosAsync

    CoreWebView2EnvironmentOptions
        AreBrowserExtensionsEnabled
        CustomSchemeRegistrations

    CoreWebView2Frame
        FrameId

    CoreWebView2FrameInfo
        FrameId
        FrameKind
        ParentFrameInfo

    CoreWebView2FrameKind
    CoreWebView2MouseEventKind
        NonClientRightButtonDown
        NonClientRightButtonUp

    CoreWebView2NavigationKind
    CoreWebView2NavigationStartingEventArgs
        NavigationKind

    CoreWebView2NewWindowRequestedEventArgs
        OriginalSourceFrameInfo

    CoreWebView2ProcessExtendedInfo
    CoreWebView2Profile
        AddBrowserExtensionAsync
        Delete
        Deleted
```

```C#
Microsoft.Windows.Management.Deployment
 
    AddPackageOptions
    EnsureReadyOptions
    PackageDeploymentContract
    PackageDeploymentManager
    PackageDeploymentProgress
    PackageDeploymentProgressStatus
    PackageDeploymentResult
    PackageDeploymentStatus
    PackageRuntimeManager
    PackageSet
    PackageSetItem
    PackageSetItemRuntimeDisposition
    PackageSetRuntimeDisposition
    PackageVolume
    ProvisionPackageOptions
    RegisterPackageOptions
    RemovePackageOptions
    StagePackageOptions
    StubPackageOption
```

```C#
Microsoft.Windows.Widgets.Feeds.Providers
 
    CustomQueryParametersRequestedArgs
    CustomQueryParametersUpdateOptions
    FeedDisabledArgs
    FeedEnabledArgs
    FeedManager
    FeedProviderDisabledArgs
    FeedProviderEnabledArgs
    FeedProviderInfo
    IFeedManager
    IFeedProvider
```

## Version 1.4 Preview 2 (1.4.0-preview2)

This is the latest release of the preview channel for version 1.4.

In an existing Windows App SDK 1.3 (from the stable channel) app, you can update your Nuget package to 1.4.0-preview2 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

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
-  The new `TreeView.SelectionChanged` event allows developers to respond when the user or code-behind changes the set of selected nodes in the `TreeView` control.
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

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

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

## Version 1.3 Preview 1 (1.3.0-preview1)

This is the latest release of the preview channel for version 1.3. This release includes previews for new features across WinAppSDK and several performance, security, accessibility and reliability bug fixes.

In an existing Windows App SDK 1.2 (from the stable channel) app, you can update your Nuget package to 1.3.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### XAML Backdrop APIs
With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI 3 app.
See the [Xaml Backdrop API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/xaml-backdrop-api.md?plain=1#L39) on GitHub for more information about the **Window.SystemBackdrop** property.

```csharp
public MainWindow()
{
    this.InitializeComponent();

    this.SystemBackdrop = new MicaBackdrop();
}
```

### Window.AppWindow
Replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from an **Window** through `Window.AppWindow`. See the [Window.AppWindow API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/appwindow-spec.md) on GitHub for additional background and usage information.

### New features from across WinAppSDK
- `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
- Environment Manager: `EnvironmentManager.AreChangesTracked` to inform you whether changes to the environment manager are able to be tracked in your application. See the [Environment Manager API spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppLifecycle/EnvironmentVariables/AppLifecycle%20-%20Environment%20Variables%20(EV).md) on GitHub for more information.
- MRT Core: A new event, `Application.ResourceManagerInitializing` allows your app to provide its own implementation of the `IResourceManager` interface, and gives you access to the ResourceManager that WinUI uses to resolve resource URIs. See the [IResourceManager API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/custom-iresourcemanager-spec.md) on GitHub for more information.
- With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.
- A new event, `DebugSettings.XamlResourceReferenceFailed` is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see the [API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/xaml-resource-references-tracing-spec.md) and issues [4972](https://github.com/microsoft/microsoft-ui-xaml/issues/4972), [2350](https://github.com/microsoft/microsoft-ui-xaml/issues/2350), and [6073](https://github.com/microsoft/microsoft-ui-xaml/issues/6073) on GitHub.
- Deployment: To manage and repair the Windows App Runtime, `DeploymentRepairOptions` is now available as part of the `DeploymentManager`. For more information, see the Repair section of the [Deployment API Spec](https://github.com/microsoft/WindowsAppSDK/blob/user/sachinta/DeploymentRepairAPISpec/specs/Deployment/DeploymentAPI.md#repair) on GitHub.

### Known issues
- The Pivot control causes a runtime crash with a XAML parsing error. See issue [#8160](https://github.com/microsoft/microsoft-ui-xaml/issues/8160) on GitHub for more info.
- When the DatePicker or TimePicker flyout is opened, the app crashes.
- The `WindowsAppRuntime.ReleaseInfo` and `WindowsAppRuntime.RuntimeInfo` APIs introduced in 1.3 releases are not yet supported as they contain a critical bug.

## Version 1.2 Preview 2 (1.2.0-preview2)

This is the latest release of the preview channel for version 1.2.

In an existing Windows App SDK 1.1 (from the stable channel) app, you can update your Nuget package to 1.2.0-preview2 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

> [!Important]
> Visual Studio 2019 and .NET 5 is no longer supported for building C# apps (see [Windows App SDK 1.2 moving to C# WinRT 2.0](https://github.com/microsoft/WindowsAppSDK/discussions/2879)). You will need Visual Studio 2022 and one of the following .NET SDK versions: 6.0.401 (or later), 6.0.304, 6.0.109.
>
> To update your .NET SDK version, install the latest version of Visual Studio 2022 or visit [.NET Downloads](https://dotnet.microsoft.com/download). When updating your NuGet package without the required .NET SDK version, you will see an error like: *"This version of WindowsAppSDK requires .NET 6+ and WinRT.Runtime.dll version 2.0 or greater."*. To update the project from .NET 5.0 to .NET 6.0, open the project file and change "TargetFramework" to `net6.0` and "Target OS version" to the appropriate value (such as `net6.0-windows10.0.19041.0`).

### Third-party Widgets in Windows

The Widgets Board was first introduced in Windows 11 and was limited to displaying first party Widgets. Widgets are small UI containers that display text and graphics on the Widgets Board, and are associated with an app installed on the device. With Windows App SDK, as third party developers you can now create Widgets for your packaged Win32 apps and test them locally on the Windows 11 Widgets Board.

For more information about Widgets, check out [Widgets Overview](../design/widgets/index.md).

To get started developing Widgets for your app, check out the [Widget service providers](../develop/widgets/widget-providers.md) development docs and [Widgets design fundamentals](../design/widgets/widgets-design-fundamentals.md) for prerequisites, guidance and best practices.

Prerequisites for this release include:

- Developer mode enabled on the development machine.
- The development machine is running a version of Windows from the Dev Channel of the Windows Insider Program (WIP) with Widgets Board version 521.20060.1205.0 or above.

#### Known limitations when developing Widgets

- Third-party Widgets can only be tested locally on devices enrolled in WIP for this preview release. In Windows App SDK 1.2.0, users on retail versions of Windows can begin acquiring 3P Widgets via Microsoft Store shipped versions of your app.
- Widgets can only be created for packaged, Win32 apps. Widgets for Progressive Web Apps (PWA) are planned to be supported as part of [Microsoft Edge 108](/deployedge/microsoft-edge-release-schedule).

### Trimming for apps developed with .NET

.NET developers are now able to publish their WinAppSDK apps trimmed. With CsWinRT 2.0, the C#/WinRT projections distributed in WinAppSDK are now trimmable. Publishing your app trimmed can reduce the disk footprint of your app by removing any unused code from trimmable binaries.  Apps may also see a startup performance improvement. With a basic Hello World app, we have seen a ~80% disk footprint improvement and a ~7% startup performance improvement when published trimmed. With WinUI gallery, we have seen a ~45% disk footprint improvement.

For more details on how to enable trimming, trimming limitations (such as reflection against trimmable types), and trim warnings, see [Trim self-contained deployments and executables](/dotnet/core/deploying/trimming/trim-self-contained). Developers should thoroughly test their apps after trimming to ensure everything works as expected. For more information, check out issue [2478](https://github.com/microsoft/WindowsAppSDK/issues/2478) on GitHub.

### DisplayInformation
Win32 apps can now support High Dynamic Range (HDR) through the DisplayInformation class in WinAppSDK. The DisplayInformation class enables you to monitor display-related information for an application view. This includes events to allow clients to monitor for changes in the application view affecting which display(s) the view resides on, as well as changes in displays that can affect the application view.

### Fixed issues in WinUI 3
- Acrylic backdrop material via [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController) is now supported in Windows 10 apps. For more information, check out issue [7112](https://github.com/microsoft/microsoft-ui-xaml/issues/7112) on GitHub.
- Fixed issue causing App.UnhandledException to fail to be routed to the application. For more information, check out issue [5221](https://github.com/microsoft/microsoft-ui-xaml/issues/5221) on GitHub.
- Fixed issue causing ListView styles to regress and change from WinAppSDK 1.1. For more information, check out issue [7666](https://github.com/microsoft/microsoft-ui-xaml/issues/7666) on GitHub.

### Other limitations and known issues

> [!Important]
> When you reference WinAppSDK 1.2 from a project you might see an error similar to: "_Detected package downgrade: Microsoft.Windows.SDK.BuildTools from 10.0.22621.1 to 10.0.22000.194._", which is caused by incompatible references to the package from the app project and the WinAppSDK package. To resolve this you can update the reference in the project to a more recent and compatible version of Microsoft.Windows.SDK.BuildTools, or simply remove the reference from your project. If you remove it from your project, a compatible version will be implicitly referenced by the WinAppSDK package.

- Building with [Arm64 Visual Studio](https://devblogs.microsoft.com/visualstudio/arm64-visual-studio/) is not currently supported.
- Bootstrapper and Undocked RegFree WinRT auto-initializer defaults is (now) only set for projects that produce an executable (OutputType=Exe or WinExe). This prevents adding auto-initializers into class library DLLs and other non-executables by default.
  - If you need an auto-initializer in a non-executable (e.g. a test DLL loaded by a generic executable that doesn't initialize the Bootstrapper) you can explicitly enable an auto-initializer in your project via `<WindowsAppSdkBootstrapInitialize>true</WindowsAppSdkBootstrapInitialize>` or `<WindowsAppSdkUndockedRegFreeWinRTInitialize>true</WindowsAppSdkUndockedRegFreeWinRTInitialize>`.
- The version information APIs (ReleaseInfo and RuntimeInfo) can be called but return version 0 (not the actual version information).


## Version 1.2 Preview 1 (1.2.0-preview1)

In an existing Windows App SDK 1.1 (from the stable channel) app, you can update your Nuget package to 1.2.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### WinUI 3

WinUI 3 apps can play audio and video with the [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) and [**MediaTransportControls**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediatransportcontrols) media playback controls. For more info on how and when to use media controls, see [Media players](../design/controls/media-playback.md).

WinUI 3 has been updated with the latest controls, styles, and behaviors from WinUI 2.8. These updates include the addition of the [**InfoBadge**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobadge) control, improvements to accessibility and high contrast mode, as well as bug fixes across controls. For more details, see the release notes for [WinUI 2.7](../winui/winui2/release-notes/winui-2.7.md) and [WinUI 2.8](../winui/winui2/release-notes/winui-2.8.md).

#### Known issue

[**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) styles regressed and changed from WinAppSDK 1.1.

### Notifications

[**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) introduced as an alternative to XML payload for creating and defining App Notifications.

For usage information, see the [AppNotificationBuilder spec](https://github.com/microsoft/WindowsAppSDK/blob/release/1.2-preview1/specs/AppNotifications/AppNotificationContentSpec/AppNotificationBuilder-spec.md) on GitHub.

Also see [Quickstart: App notifications in the Windows App SDK](./notifications/app-notifications/app-notifications-quickstart.md) for an example of how to create a desktop Windows application that sends and receives local app notifications.

#### Breaking change

For push notifications, when making a channel request call, apps will need to use the Azure Object ID instead of the Azure App ID. See [Quickstart: Push notification in the Windows App SDK](./notifications/push-notifications/push-quickstart.md) for details on finding your Azure Object ID.

#### Fixed issue

[**PushNotificationManager.IsSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.issupported) will perform a check for elevated mode. It will return `false` if the app is elevated.

#### Known limitations (Notifications)

- In [**AppNotificationScenario**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationscenario), `Urgent` is only supported for Windows builds 19041 and later. You can use [**AppNotificationBuilder.IsUrgentScenarioSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.isurgentscenariosupported) to check whether the feature is available at runtime.
- In [**AppNotificationButton**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton), `hint-toolTip` and `hint-buttonStyle` are only supported for builds 19041 and later. You can use [**IsButtonStyleSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.isbuttonstylesupported) and [**IsToolTipSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.istooltipsupported) to check whether the feature is available at runtime.
- In [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), when used in XAML markup for an unpackaged app, the [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.source) property cannot be set with an ms-appx or ms-resource URI. As an alternative, set the Source using a file URI, or set from code.

### Windowing

Full title bar customization is now available on Windows 10, version 1809 and later through the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) class. You can set [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) to `true` to extend content into the title bar area, and [**SetDragRectangles**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles#microsoft-ui-windowing-appwindowtitlebar-setdragrectangles(windows-graphics-rectint32())) to define drag regions (in addition to other customization options).

If you've been using the [**AppWindowTitleBar.IsCustomizationSupported**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) property to check whether you can call the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) APIs, it now returns `true` on supported Windows App SDK Windows 10 versions (1809 and later).

#### Known limitations (Windowing)

Simple title bar customizations are not supported on Windows 10. These include [**BackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.backgroundcolor), [**InactiveBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactivebackgroundcolor), [**ForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.foregroundcolor), [**InactiveForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactiveforegroundcolor) and [**IconShowOptions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions). If you call these properties, they will be ignored silently. All other **AppWindowTitleBar** APIs work in Windows 10, version 1809 and later. For the caption button color APIs (among others) and [**Height**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height), [**ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) must be set to `true`, otherwise they will also be ignored silently.

### Access control

Introduced [security.accesscontrol.h](/windows/windows-app-sdk/api/win32/security.accesscontrol/) with the [**GetSecurityDescriptorForAppContainerNames**](/windows/windows-app-sdk/api/win32/security.accesscontrol/nf-security-accesscontrol-getsecuritydescriptorforappcontainernames?branch=main) function to ease and streamline named object sharing between packaged processes and general Win32 APIs. This method takes a list of Package Family Names (PFNs) and access masks, and returns a security descriptor. For more information, see the [GetSecurityDescriptorForAppContainerNames spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/GetSecurityDescriptorForAppContainerNames/GetSecurityDescriptorForAppContainerNames.md) on GitHub.

### Other limitations and known issues

- .NET PublishSingleFile isn't supported.

## Version 1.1 Preview 3 (1.1.0-preview3)

This is the latest release of the preview channel for version 1.1. It supports all preview channel features (see [Features available by release channel](release-channels.md#features-available-by-release-channel)).

In an existing app using Windows App SDK 1.0, you can update your Nuget package to 1.1.0-preview3 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)). Additionally, see [Downloads for the Windows App SDK](./downloads.md) for the updated runtime and MSIX.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

In addition to all of the [Preview 2](#version-11-preview-2-110-preview2) features, the following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3
Mica and Background Acrylic are now available for WinUI 3 applications.

For more information about these materials, check out [Materials in Windows 11](../design/signature-experiences/materials.md). Check out our sample code for applying Mica in C++ applications at [Using a SystemBackdropController with WinUI 3 XAML](system-backdrop-controller.md) and in C# applications [on GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/ControlPagesSampleCode/SystemBackdrops) as part of the WinUI Controls Gallery.

### Notifications

**Fixed issues:**
- In 1.1.0-preview1 and 1.1.0-preview2, some unpackaged apps will have seen their app icons incorrectly copied to AppData\LocalMicrosoftWindowsAppSDK. For this release, they will be copied to AppData\Local\Microsoft\WindowsAppSDK instead. To avoid leaking icons, you should manually delete the app icon at the incorrect path after updating to 1.1.0-preview3. 
- App icon and app display name retrieval for app notifications via Shortcuts is now supported. This app icon will be prioritized over any icon specified in resource files.
- Support for push notifications for unpackaged apps has been restored (see **Limitations** for noted exception). We've introduced the PushNotificationManager::IsSupported API to check if your app supports push notifications.

**Limitations:**
- Notifications for an *elevated* unpackaged app is not supported. PushNotificationManager::IsSupported will not perform a check for elevated mode. However, we are working on supporting this in a future release.

### MSIX packaging
We've enhanced MSIX adding new and extending existing functionality via the extension categories:

- windows.appExecutionAlias
- windows.customDesktopEventLog
- windows.dataShortcuts
- windows.fileTypeAssociation
- windows.fileTypeAssociation.iconHandler
- windows.folder
- windows.shortcut

These require the Windows App SDK framework package to be installed. See [Downloads for the Windows App SDK](./downloads.md) to install the runtime.

### Environment manager
API set that allows developers to add, remove, and modify environment variables without having to directly use the registry API.

Clarification from 1.1 Preview 1: Automatic removal of any environment variable changes when an app that used environment manager is uninstalled is only available for packaged apps. Additionally, reverting environment variable changes requires installation of the Windows App SDK framework package, see [Downloads for the Windows App SDK](./downloads.md) for the runtime.

### Other known limitations
Regressions from 1.1 Preview 2:

- For .NET apps using MRT Core APIs and WinUI apps that *don't* deploy with single-project MSIX:
  - RESW and image files that were added to the project as Existing Items and  previously automatically included to the PRIResource and Content ItemGroups, respectively, won't be included in those ItemGroups. As a result, these resources won't be indexed during PRI generation, so they won't be available during runtime. 
    - Workaround: Manually include the resources in the project file and remove them from the None ItemGroup.
    - Alternative workaround: When available, upgrade your apps' .NET SDK to 6.0.300. See [Version requirements for .NET SDK](/dotnet/core/compatibility/sdk/6.0/vs-msbuild-version) for additional information.
- For .NET apps that *don't* deploy with single-project MSIX:
  - If a file is added to the Content ItemGroup twice or more, then there will be a build error.
    - Workaround: Delete the duplicate inclusion/s or set EnableDefaultContentItems to false in the project file.

Both regressions will be restored in the next stable release.
## Version 1.1 Preview 2 (1.1.0-preview2)
This is the second release of the preview channel for version 1.1. It supports all preview channel features (see [Features available by release channel](release-channels.md#features-available-by-release-channel)).

In an existing app using Windows App SDK 1.0, you can update your Nuget package to 1.1.0-preview2 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)). Additionally, see [Downloads for the Windows App SDK](./downloads.md) for the updated runtime and MSIX.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

In addition to all of the [Preview 1](#version-11-preview-1-110-preview1) features, the following sections describe new and updated features, limitations, and known issues for this release.

### Notifications

**Fixed issues:**
- An app without package identity sending notifications will now see its app icon in the notification if the icon is a part of the app's resource. If the app resource has no icon, the Windows default app icon is used.
- A WinUI 3 app that's not running can now be background-activated via a notification.

**Regression from 1.1 Preview 1:** Push notifications support for unpackaged apps. Expected to be restored in the next release. 

**Known limitations:**
- We've introduced the PushNotificationManager::IsSupported API to check if self-contained apps support push notifications. However, this API is not yet working as intended, so keep an eye out in the next preview release for full support of the IsSupported API.
- Some unpackaged apps will see their app icons incorrectly copied to AppData\LocalMicrosoftWindowsAppSDK. For the next release, they will be copied to AppData\Local\Microsoft\WindowsAppSDK instead. To avoid leaking icons, the developer should manually delete their app icon at the incorrect path after upgrading to the next release.
- App icon and app display name retrieval for notifications via Shortcuts is not supported. But we're working on supporting that in a future release. 

### Deployment
**New features:**
- Packaged apps can now force deploy the Windows App SDK runtime packages using the **DeploymentManager.Initialize** API.
- The Bootstrapper API now includes new options for improved usability and troubleshooting. For more details, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md) and [Rich information on Bootstrap initalization failure](https://github.com/microsoft/WindowsAppSDK/pull/2316).

**Known limitations:**
- Self-contained deployment is supported only on Windows 10, 1903 and later. 

### Windowing
For easier programming access to functionality that's implemented in `USER32.dll` (see [Windows and messages](/windows/win32/api/_winmsg/)), this release surfaces more of that functionality in [**AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself.

**New features:**
- Apps with existing windows have more control over how a window is shown, by calling **AppWindow.ShowOnceWithRequestedStartupState**&mdash;the equivalent of `ShowWindow(SW_SHOWDEFAULT)`.
- Apps can show, minimize, or restore a window and specify whether the window should be activated or not at the time the call is made.
- Apps can now set a window's client area size in Win32 coordinates.
- We've added APIs to support z-order management of windows.
- Apps drawing custom titlebars with [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) can set a *PreferredTitleBarHeight* option. You have a choice of a *standard height* titlebar, or a *tall* titlebar that provides more room for interactive content. See [Title bar](../design/basics/titlebar-design.md) in the Fluent design guidelines for advice about when to use a tall titlebar.

**Known limitations:**
- Tall titlebar support is available only on Windows 11. We are working to bring this downlevel along with other custom titlebar APIs.

### WinUI 3

**Fixed issues**:
- Fixed issue causing C# apps with WebView2 to crash on launch when the C/C++ Runtime (CRT) isn't installed by upgrading the WebView2 SDK from 1020.46 to 1185.39.
- Fixed issue causing some rounded corners to show a gradient when they should be a solid color. For more information see [issue 6076](https://github.com/microsoft/microsoft-ui-xaml/issues/6076) & [issue 6194](https://github.com/microsoft/microsoft-ui-xaml/issues/6194) on GitHub.
- Fixed issue where updated styles were missing from generic.xaml.
- Fixed layout cycle issue causing an app to crash when scrolling to the end of a ListView. For more information see [issue 6218](https://github.com/microsoft/microsoft-ui-xaml/issues/6218) on GitHub.

### Performance

C# applications have several performance improvements. For more details, see the [C#/WinRT 1.6.1 release notes](https://github.com/microsoft/CsWinRT/releases/tag/1.6.1.220314.1).
 
## Version 1.1 Preview 1 (1.1.0-preview1)
This is the first release of the preview channel for version 1.1. It supports all preview channel features (see [Features available by release channel](release-channels.md#features-available-by-release-channel)).

In an existing app using Windows App SDK 1.0, you can update your Nuget package to 1.1.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)). Additionally, see [Downloads for the Windows App SDK](./downloads.md) for the updated runtime and MSIX.

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3
**Known issue**: Users are unable to drop an element when drag-and-drop is enabled.

### Elevated (admin) support
Using Windows App SDK 1.1 Preview 1, apps (including WinUI 3) will be able to run with elevated privilege.

**Important limitations**
- Currently available only on Windows 11. But we're evaluating bringing this support downlevel in a later release.

**Known issues**
- WinUI 3 apps crash when dragging an element during a drag-and-drop interaction.

### Self-contained deployment
Windows App SDK 1.1 will introduce support for self-contained deployment. Our [Deployment overview](../package-and-deploy/deploy-overview.md) details the differences between framework-dependent and self-contained deployment, and how to get started.

**Known issues:**
- A C++ app that's packaged needs to add the below to the bottom of its project file to work around a bug in the self-contained `.targets` file that removes framework references to VCLibs:

    ```xml
    <PropertyGroup>
        <IncludeGetResolvedSDKReferences>true</IncludeGetResolvedSDKReferences>
    </PropertyGroup>

    <Target Name="_RemoveFrameworkReferences"
        BeforeTargets="_ConvertItems;_CalculateInputsForGenerateCurrentProjectAppxManifest">
        <ItemGroup>
            <FrameworkSdkReference Remove="@(FrameworkSdkReference)" Condition="'%(FrameworkSdkReference.SDKName)' == 'Microsoft.WindowsAppRuntime.1.1-preview1'" />
        </ItemGroup>
    </Target>
     ```

- Supported only on Windows 10, 1903 and later.

### Notifications
Developers of packaged (including packaged with external location) and unpackaged apps can now send Windows notifications.

**New features:**
- Support for app notifications for packaged and unpackaged apps. Full details on [GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppNotifications/AppNotifications-spec.md)
  - Developers can send app notifications, also known as toast notifications, locally or from their own cloud service.
- Support for push notification for packaged and unpackaged apps. Full details on [GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/PushNotifications/PushNotifications-spec.md)
  - Developers can send raw notifications or app notifications from their own cloud service.

**Limitations:**
- Apps published as self-contained may not have push notifications support. Keep an eye out in the next preview release for an **IsSupported** API to check for push notifications support.
- Apps that are unpackaged sending app notifications will not see their app icon in the app notification unless they are console applications. Console apps that are unpackaged should follow the patterns shown in the [ToastNotificationsDemoApp](https://github.com/microsoft/WindowsAppSDK/blob/main/test/TestApps/ToastNotificationsDemoApp/main.cpp) sample.
- Windows App SDK runtime must be installed to support push notifications, see [Downloads for the Windows App SDK](./downloads.md) for the installer.
- A WinUI 3 app that's not running can't be background-activated via a notification. But we're working on supporting that in a future release.

### Environment manager
API set that allows developers to add, remove, and modify environment variables without having to directly use the registry API.

**New features**
- Provides automatic removal of any environment variables changes when an app that used environment manager is uninstalled.

**Limitations**
- Currently unavailable in C# apps. But we're evaluating bringing this feature to C# apps in a later release.

### Other limitations and known issues

- If you're using C# with 1.1.0 Preview 1, then you must use one of the following .NET SDK versions at a minimum: .NET SDK 6.0.201, 6.0.103, 5.0.212, or 5.0.406. To upgrade your .NET SDK, you can update to the latest version of Visual Studio, or visit [Download .NET](https://dotnet.microsoft.com/en-us/download).

## Version 1.0 Preview 3 (1.0.0-preview3)

Preview 3 is the latest release of the preview channel for version 1.0 of the Windows App SDK. Preview 3 supports all [preview channel features](release-channels.md#features-available-by-release-channel).

### Download 1.0 Preview 3 Visual Studio extensions (VSIX)

> [!NOTE]
> If you have Windows App SDK Visual Studio extensions (VSIX) already installed, then uninstall them before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

From the table below you can download the Visual Studio extensions (VSIX) for the 1.0 Preview 3 release. For all versions, see [Downloads for the Windows App SDK](downloads.md). If you haven't done so already, start by configuring your development environment, using the steps in [Install tools for the Windows App SDK](set-up-your-development-environment.md?tabs=preview).

The extensions below are tailored for your programming language and version of Visual Studio.

| **1.0 Preview 3 downloads** | **Description** |
| ----------- | ----------- |
| [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp) | Build C# apps with the Windows App SDK Visual Studio 2019 extension. |
| [C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp) |  Build C++ apps with the Windows App SDK Visual Studio 2019 extension. |
| [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp) | Build C# apps with the Windows App SDK Visual Studio 2022 extension. |
| [C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp) | Build C++ apps with the Windows App SDK Visual Studio 2022 extension. |
| [The `.exe` installer, and MSIX packages](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer) | Deploy the Windows App SDK with your app using the `.exe` installer, and MSIX packages. |

The following sections describe new and updated features, limitations, and known issues for 1.0 Preview 3.

### WinUI 3

We now support deploying WinUI 3 apps without MSIX packaging. See [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md) to configure your WinUI 3 application to support unpackaged deployment.

**Important limitations**

- Unpackaged WinUI 3 applications are **supported only on Windows versions 1909 and later**.
- Unpackaged WinUI 3 applications are **supported on x86 and x64**; arm64 support will be added in the next stable release.
- **Single-project MSIX Packaging Tools** for [Visual Studio 2019](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) or [Visual Studio 2022](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingToolsDev17) is required for unpackaged apps.
- In an unpackaged app, you might receive a prompt to install .NET 3.5; if you do, then you can ignore it.
- Some APIs are not currently supported in unpackaged apps. We're aiming to fix this in the next stable release. A few examples:
  - [ApplicationData](/uwp/api/Windows.Storage.ApplicationData)
  - [StorageFile.GetFileFromApplicationUriAsync](/uwp/api/Windows.Storage.StorageFile.GetFileFromApplicationUriAsync)
  - [ApiInformation](/uwp/api/Windows.Foundation.Metadata.ApiInformation) (not supported on Windows 10)
  - [Package.Current](/uwp/api/windows.applicationmodel.package.current)
- ListView, CalendarView, and GridView controls are using the incorrect styles, and we're aiming to fix this in the next stable release.

For more info, or to get started developing with WinUI 3, see:

- [Windows UI Library (WinUI) 3](../winui/index.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)

### Other limitations and known issues

- **Unpackaged apps are not supported on Windows 10 version 1809**. We're aiming to fix this in the next release in the stable channel.

- **C# Single-project MSIX app doesn't compile if C++ UWP Tools aren't installed**. If you have a C# Single-project MSIX project, then you'll need to install the **C++ (v14x) Universal Windows Platform Tools** optional component. 

- This release introduces the **Blank App, Packaged (WinUI 3 in Desktop)** project templates for C# and C++. These templates enable you to build your app into an MSIX package without the use of a separate packaging project (see [Package your app using single-project MSIX](single-project-msix.md)). These templates have some known issues in this release:

  - **Missing Publish menu item until you restart Visual Studio**. When creating a new app in both Visual Studio 2019 and Visual Studio 2022 using the **Blank App, Packaged (WinUI 3 in Desktop)** project template, the command to publish the project doesn't appear in the menu until you close and re-open Visual Studio.

  - **Error when adding C++ static/dynamic library project references to C++ apps using Single-project MSIX Packaging**. Visual Studio displays an error that the project can't be added as a reference because the project types are not compatible.
  
  - **Error when referencing a custom user control in a class library project**. The application will crash with the error that the system can't find the path specified.

  - **C# or C++ template for Visual Studio 2019**. When you try to build the project, you'll encounter the error "The project doesn't know how to run the profile *project name*". To resolve this issue, install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools).

  - **C# template for Visual Studio 2019 and Visual Studio 2022**. In Visual Studio when you **Start Debugging** or **Start Without Debugging**, if your app doesn't deploy and run (and there's no feedback from Visual Studio), then click on the project node in **Solution Explorer** to select it, and try again.

  - **C# template for Visual Studio 2019 and Visual Studio 2022**. You will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager." To resolve this issue, enable deployment for your project in **Configuration Manager**. For detailed instructions, see the [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md).

  - **C++ template for Visual Studio 2022 version 17.0 releases up to Preview 4**. You will encounter the following error the first time you try to run your project: "There were deployment errors". To resolve this issue, run or deploy your project a second time. This issue will be fixed in Visual Studio 2022 version 17.0 Preview 7.

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **C# projects using 1.0 Preview 3 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

## Important issue impacting 1.0 Preview 1 and Preview 2

Version 1.0 Preview 1 and Preview 2 of the Windows App SDK includes a mechanism to clean up any environment variable changes made by a packaged app when that app is uninstalled. This feature is in an experimental state, and the first release includes a known bug that can corrupt the system **PATH** environment variable.

Preview 1 and Preview 2 corrupts any **PATH** environment variable that contains the expansion character `%`. This happens whenever any packaged app is uninstalled, regardless of whether that app uses the Windows App SDK.

Also see [PATH environment variable corruption issue](https://github.com/microsoft/WindowsAppSDK/issues/1599).

### Details

The system **PATH** entry is stored in the **Path** value in the following key in the Windows Registry:

```
Computer\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment
```

If you launch the Registry Editor (`regedit.exe`), then you can copy and paste the path above into the breadcrumb bar (immediately below the menu bar), and press Enter to locate the key.

The **Path** value of that key *should* be of type **REG_EXPAND_SZ**, but the bug changes it to **REG_SZ**. And that makes the system **PATH** environment variable unusable if it contains the variable expansion character `%`.

### Affected releases

* [Windows App SDK 1.0 Preview 1 (1.0.0-preview1)](#version-10-preview-1-100-preview1)
* [Windows App SDK 1.0 Preview 2 (1.0.0-preview2)](#version-10-preview-2-100-preview2)

### Mitigation

To get your machine back into a good state, take the following steps:

1. Check whether the **PATH** in the Registry is corrupt and, if so, reset it by running the script below.

   You can accomplish step 1 with the following Windows PowerShell script (PowerShell Core won't work). Run it elevated.

   ```Powershell
   # This script must be run from an elevated Windows PowerShell
   # window (right-click Windows PowerShell in the Start menu,
   # and select Run as Administrator).

   # If the PATH in the Registry has been set to REG_SZ, then delete
   # it, and recreate it as REG_EXPAND_SZ.
   
   $EnvPath = 'Registry::HKLM\System\CurrentControlSet\Control\Session Manager\Environment'
   $Environment=Get-Item $EnvPath
   $PathKind = $Environment.GetValueKind('Path')

   if ($PathKind -ne 'ExpandString') {
     $Path = $Environment.GetValue('Path')
     Remove-ItemProperty $EnvPath -Name Path
     New-ItemProperty $EnvPath -Name Path -PropertyType ExpandString -Value $Path
   }
   ```

2. Uninstall all apps that use the Windows App SDK 1.0 Preview1 or Preview2 (see the script below).
3. Uninstall the Windows App SDK 1.0 Preview1/Preview2 packages, including the package that contains the bug (see the script below).

   You can accomplish steps 2 and 3 with the following Windows PowerShell script (PowerShell Core won't work). Run it elevated.

   ```Powershell
   # This script must be run from an elevated Windows PowerShell
   # window (right-click Windows PowerShell in the Start menu,
   # and select Run as Administrator).

   # Remove the Windows App SDK 1.0 Preview1/2, and all apps that use it.

   $winappsdk = "Microsoft.WindowsAppRuntime.1.0-preview*"
   Get-AppxPackage | Where-Object { $_.Dependencies -like $winappsdk } | Remove-AppxPackage
   Get-AppxPackage $winappsdk | Remove-AppxPackage
   ```

### Fix in Windows App SDK 1.0 Preview 3

The feature causing the **PATH** environment variable to be corrupted will be removed in the upcoming Windows App SDK 1.0 Preview 3 release. It might be reintroduced at a later date, when all bugs have been fixed and thoroughly tested.

We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3).  

## Version 1.0 Preview 2 (1.0.0-preview2)

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you’ve already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead. 

This is the latest release of the preview channel for version 1.0. It supports all [preview channel features](release-channels.md#features-available-by-release-channel).

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

**New updates**:

- Controls have been updated to reflect the latest Windows styles from [WinUI 2.6](../winui/winui2/release-notes/winui-2.6.md#visual-style-updates).
- Single-project MSIX is supported.
- WinUI 3 package can now target build 17763 and later. See [issue #921](https://github.com/microsoft/WindowsAppSDK/issues/921) for more info.
- In-app toolbar is supported. However, the in-app toolbar and existing Hot Reload/Live Visual Tree support require the upcoming Visual Studio 17.0 Preview 5 release, available later in October.

**Bug fixed**: WebView2Runtime text is now localized.

For more info or to get started developing with WinUI 3, see:

- [Windows UI Library (WinUI) 3](../winui/index.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)

### Windowing

This release introduces updates to the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class. There are no major new features added in this release, but there are changes to method names, properties, and some return values have been removed. See the documentation and samples for detailed updates. If you worked with [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) in the 1.0 Experimental or 1.0 Preview 1 releases, expect some changes to your code.

**New updates**:

- The **AppWindowConfiguration** class has been removed. The properties of this class is now available on the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself, or on the **Presenter** classes.
- Most `bool` return values for the WinRT API methods in this space has been removed and are now `void` since these methods would always succeed.
- The C# ImportDll calls are no longer needed for [GetWindowIdFromWindow](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowidfromwindow) and [GetWindowFromWindowId](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowfromwindowid). Use the .NET wrapper methods available in the [**Microsoft.UI.Win32Interop**](../api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop.md) class instead.

**Important limitations**:

- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow); you're limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more info, see [Manage app windows](windowing/windowing-overview.md).

### Input

**New updates**:

- Improved support for precision touchpad input.

**Important limitations**:

- All [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint) static factory functions have been removed: **GetCurrentPoint**, **GetCurrentPointTransformed**, **GetIntermediatePoints**, and **GetIntermediatePointsTransformed**.
- The Windows App SDK does not support retrieving **PointerPoint** objects with pointer IDs. Instead, you can use the **PointerPoint** member function **GetTransformedPoint** to retrieve a transformed version of an existing **PointerPoint** object. For intermediate points, you can use the **PointerEventArgs** member functions [GetIntermediatePoints](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointereventargs.getintermediatepoints) and **GetTransformedIntermediatePoints**. See the documentation for additional details.

### MRT Core

**New updates**:

- App developers can now opt out an image file or a RESW file from being indexed in the PRI file in .NET projects. See [issue 980](https://github.com/microsoft/WindowsAppSDK/issues/980) for more info.  

**Important limitations**:

- In .NET projects, resource files copy-pasted into the project folder aren't indexed on F5 if the app was already built. As a workaround, rebuild the app. See [issue 1503](https://github.com/microsoft/WindowsAppSDK/issues/1503) for more info].
- In .NET projects, existing resource files added from an external folder aren't indexed without manual setting of the Build Action. To work around this issue, set the Build Action in Visual Studio: **Content** for image files and **PRIResource** for RESW files. See issue [1504](https://github.com/microsoft/WindowsAppSDK/issues/1504) for more info.

### Deployment for unpackaged apps

**New features**:

- Windows App SDK 1.0 Preview 2 introduces a .NET wrapper for the bootstrapper API (see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md)). The bootstrapper API is a set of native C/C++ functions that unpackaged apps must use to dynamically take a dependency on the Windows App SDK framework package at run time. The .NET wrapper provides an easier way to call the bootstrapper API from .NET apps, including Windows Forms and WPF apps. The .NET wrapper for the bootstrapper API is available in the Microsoft.WindowsAppRuntime.Bootstrap.Net.dll assembly, which is local to your app project. For more info about the .NET wrapper, see [.NET wrapper library](use-windows-app-sdk-run-time.md#net-wrapper-for-the-bootstrapper-api).
- Packaged apps can now use the deployment API to get the main and singleton MSIX packages installed on the machine. The main and singleton packages are part of the framework package that is installed with the app, but due to a limitation with the Windows application model, packaged apps will need to take this additional step in order to get those packages installed. For more info about how the deployment API works, see [Windows App SDK deployment guide for framework-dependent packaged apps](deploy-packaged-apps.md).

**Important limitations**:

- The .NET wrapper for the bootstrapper API only is only intended for use by unpackaged .NET applications to simplify access to the Windows App SDK.
- Only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the deployment API to install the main and singleton package dependencies. Support for partial-trust packaged apps will be coming in later releases. 
- When F5 testing an x86 app which uses the [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) method on an x64 system, ensure that the x64 framework is first installed by running the [WindowsAppRuntimeInstall.exe](https://aka.ms/windowsappsdk/1.0-preview2/msix-installer). Otherwise, you will encounter a **NOT_FOUND** error due to Visual Studio not deploying the x64 framework, which normally occurs through Store deployment or sideloading.

### App lifecycle

Most of the App Lifecycle features already exist in the UWP platform, and have been brought into the Windows App SDK for use by desktop app types, especially unpackaged Console apps, Win32 apps, Windows Forms apps, and WPF apps. The Windows App SDK implementation of these features cannot be used in UWP apps, since there are equivalent features in the UWP platform itself. 

Non-UWP apps can also be packaged into MSIX packages. While these apps can use some of the Windows App SDK App Lifecycle features, they must use the manifest approach where this is available. For example, they cannot use the Windows App SDK **RegisterForXXXActivation** APIs and must instead register for rich activation via the manifest.

All the constraints for packaged apps also apply to WinUI 3 apps that are packaged; and there are additional considerations as described below.

**Important considerations**:

- Rich activation: [GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs)
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Usable, but these apps can also use the platform `GetActivatedEventArgs`. Note that the platform defines [Windows.ApplicationModel.AppInstance](/uwp/api/windows.applicationmodel.appinstance) whereas the Windows App SDK defines [Microsoft.Windows.AppLifecycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance). And while UWP apps can use the `ActivatedEventArgs` classes, such as `FileActivatedEventArgs` and `LaunchActivatedEventArgs`, apps that use the Windows App SDK AppLifecycle feature must use the interfaces not the classes (e.g, `IFileActivatedEventArgs`, `ILaunchActivatedEventArgs`, and so on).
  - _WinUI 3 apps_: WinUI 3's App.OnLaunched is given a [Microsoft.UI.Xaml.LaunchActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.launchactivatedeventargs), whereas the platform `GetActivatedEventArgs` returns a [Windows.ApplicationModel.IActivatedEventArgs](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs), and the WindowsAppSDK `GetActivatedEventArgs` returns a [Microsoft.Windows.AppLifecycle.AppActivationArguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments) object which can represent a platform `LaunchActivatedEventArgs`.
  - For more info, see [Rich activation](applifecycle/applifecycle-rich-activation.md).

- Register/Unregister for rich activation
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Not usable use the app's MSIX manifest instead.
  - For more info, see [Rich activation](applifecycle/applifecycle-rich-activation.md).

- Single/Multi-instancing
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Fully usable.
  - _WinUI 3 apps_: If an app wants to detect other instances and redirect an activation, it must do so as early as possible, and before initializing any windows, etc. To enable this, the app must define DISABLE_XAML_GENERATED_MAIN, and write a custom Main (C#) or WinMain (C++) where it can do the detection and redirection.
  - [RedirectActivationToAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.redirectactivationtoasync) is an async call, and you should not wait on an async call if your app is running in an STA. For Windows Forms and C# WinUI 3 apps, you can declare Main to be async, if necessary. For C++ WinUI 3 and C# WPF apps, you cannot declare Main to be async, so instead you need to move the redirect call to another thread to ensure you don't block the STA. 
  - For more info, see [App instancing](applifecycle/applifecycle-instancing.md).

- Power/State notifications
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Fully usable.
  - For more info, see [Power management](applifecycle/applifecycle-power.md).

**Known issue**:

File Type associations incorrectly encode %1 to be %251 when setting the Verb handler's command line template, which crashes unpackaged Win32 apps. You can manually edit the Registry value to be %1 instead as a partial workaround. If the target file path has a space in it, then it will still fail and there is no workaround for that scenario.

### Other limitations and known issues

- Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you’ve already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead. 

- This release introduces the **Blank App, Packaged (WinUI 3 in Desktop)** templates for C# and C++ projects. These templates enable you to [build your app into an MSIX package without the use of a separate packaging project](single-project-msix.md). These templates have some known issues in this release:

  - **C# template for Visual Studio 2019.** You will encounter the error when you try to build the project: "The project doesn't know how to run the profile *project name*". To resolve this issue, install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools).

  - **C# template for Visual Studio 2019 and Visual Studio 2022.** You will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager." To resolve this issue, enable deployment for your project in **Configuration Manager**. For detailed instructions, see the [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md).

  - **C++ template for Visual Studio 2019 and Visual Studio 2022.** In this release, these projects are restricted to calling the subset of Win32 APIs that can be called by UWP apps. The **Blank App, Packaged with WAP (WinUI 3 in Desktop)** template is not affected by this issue.

  - **C++ template for Visual Studio 2022 version 17.0 releases up to Preview 4.** You will encounter the following error the first time you try to run your project: "There were deployment errors". To resolve this issue, run or deploy your project a second time. This issue will be fixed in Visual Studio 2022 version 17.0 Preview 5.

- **Push notifications API (Microsoft.Windows.PushNotifications namespace) incorrectly included in the 1.0 Preview 2 release.** This is still an experimental feature, and to you use it you must install the 1.0 Experimental release instead. This feature will be removed from the upcoming 1.0 release.

- **App lifecycle API (Microsoft.Windows.AppLifecycle namespace) incorrectly includes the Experimental attribute in the 1.0 Preview 2 release.** The Experimental attribute will be removed from this API in the next release.

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **C# projects using 1.0 Preview 2 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.
    
## Version 1.0 Preview 1 (1.0.0-preview1)

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you’ve already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead. 

This is the first release of the preview channel for version 1.0. It supports all [preview channel features](release-channels.md#features-available-by-release-channel).

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release of WinUI 3 is focused on building towards 1.0 with bug fixes.

- **New features**: No new features in Preview 1.
- **Fixed issues**: For the full list of issues addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-preview-release-notes).

For more info or to get started developing with WinUI 3, see:

- [Windows UI Library (WinUI) 3](../winui/index.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)

### Windowing

This release brings the Windowing API we introduced in Experimental 1 to a Preview state. There are no major new features areas in this release as it is focused on bugfixes, stability, and adjustments to the API signature. The noteworthy changes and additions are called out below.

**New features**:

- [DisplayAreaWatcher](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayareawatcher) has been added to the Windowing APIs. This allows a developer to observe changes in the display topology and enumerate DisplayAreas currently defined in the system.
- [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) now supports setting the window icon via the [SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon) method, and [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) now supports selecting whether to show/hide the window icon along with the system menu via the [IconShowOptions](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions) property.

**Important limitations**:

- This release of [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow); you're limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more info, see [Manage app windows](windowing/windowing-overview.md).

### Input

This release brings some new features to the Input API. The noteworthy changes and additions are called out below.

**New features and updates**:

- [PointerPredictor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpredictor) gives input latency sensitive applications such inking applications the ability to predict input point locations up to 15ms in the future to achieve better latency and smooth animation.  
- [PenDeviceInterop](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop) enables you to acquire a reference to the [Windows.Devices.Input.PenDevice](/uwp/api/windows.devices.input.pendevice) by using the [FromPointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop.frompointerpoint) method.
- [InputCursor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor) provides an explicit distinction between preset system cursor types and custom cursor types by removing the "Custom" type present in `CoreCursor`, and splitting the `CoreCursor` object into separate objects.
- Updates to [InputCursor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor) APIs.
- [GestureRecognizer](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.gesturerecognizer) moved out of experimental to Microsoft.UI.Input.
- [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint) moved out of experimental to Microsoft.UI.Input.
- Mouse, touch, and pen input fully supported for WinUI 3 drag and drop.

**Important limitations**:

- This release of Input APIs has known issues with Windows version 1809.  
- MRT Core is not yet supported by any subtype of [InputCursor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor).
- Direct use of the platform SDK API [Windows.UI.Core.CoreDragOperation](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragoperation) will not work with WinUI 3 applications.
- PointerPoint properties RawPosition and ContactRectRaw were removed because they referred to non-predicted values, which were the same as the normal values in the OS. Use [Position](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint.position) and [ContactRect](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties.contactrect) instead. Pointer prediction is now handled with the Microsoft.UI.Input.PointerPredictor API object.

### MRT Core

Starting in version 1.0 Preview 1, MRT Core APIs have moved from the [Microsoft.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.applicationmodel.resources) namespace to the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace.

### Other limitations and known issues

- Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you’ve already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead. 

- Projects created by using the C++ **Blank App, Packaged with WAP (WinUI 3 in Desktop)** project template encounter the following build error by default: `fatal error C1083: Cannot open include file: 'winrt/microsoft.ui.dispatching.co_await.h': No such file or directory`. To resolve this issue, remove the following line of code from the **pch.h** file. This issue will be fixed in the next release.

    ```cpp
    #include <winrt/microsoft.ui.dispatching.co_await.h>
    ```

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).

- **C# projects using 1.0 Preview 1 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- **Unpackaged apps not supported on Windows 10 version 1809**: This should be resolved in the next release.

## Related topics

- [Stable channel](stable-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
