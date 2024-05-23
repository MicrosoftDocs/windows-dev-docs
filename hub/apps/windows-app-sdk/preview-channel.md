---
title: Preview release channel for the Windows App SDK 
description: Provides info about the preview release channel for the Windows App SDK.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Latest preview channel release notes for the Windows App SDK

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

**Preview channel release note archive:**

- [Preview channel release notes for the Windows App SDK 1.4](release-notes-archive/experimental-channel-1.4.md)
- [Preview channel release notes for the Windows App SDK 1.3](release-notes-archive/experimental-channel-1.3.md)
- [Preview channel release notes for the Windows App SDK 1.2](release-notes-archive/experimental-channel-1.2.md)
- [Preview channel release notes for the Windows App SDK 1.0](release-notes-archive/experimental-channel-1.0.md)
- [Preview channel release notes for the Windows App SDK 0.8](release-notes-archive/experimental-channel-0.8.md)

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

## Related topics

- [Stable channel](stable-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
