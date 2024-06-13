---
title: Experimental channel release notes for the Windows App SDK 1.5
description: Learn about the experimental channel release notes for the Windows App SDK 1.5
ms.topic: article
ms.date: 05/30/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK 1.5

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel provides releases of the Windows App SDK that include [experimental channel features](../release-channels.md#features-available-by-release-channel) that are in early stages of development. APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel might have breaking changes in future releases, but experimental APIs are especially subject to change. Experimental features may be removed from the next release, or may never be released.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on experimental releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest experimental channel release:**

- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Version 1.5 Experimental (1.5.0-experimental2)

This is the latest release of the experimental channel.
To download, retarget your WinAppSDK NuGet version to `1.5.240124002-experimental2`.

### Bug fixes

This release includes the following bug fixes:

- Fixed an issue causing apps to crash on startup when using a custom `NavigationViewItem`. For more info, see GitHub issue [#8814](https://github.com/microsoft/microsoft-ui-xaml/issues/8814).
- Fixed a `NavigationView` issue where the ellipsis button would incorrectly generate an error. For more info, see GitHub issue [#8380](https://github.com/microsoft/microsoft-ui-xaml/issues/8380).
- Fixed an issue where a `SystemBackdrop` would not render properly in a multi-window app. For more info, see GitHub issue [#8423](https://github.com/microsoft/microsoft-ui-xaml/issues/8423).
- Fixed a duplication issue when inserting into the beginning of an `ObservableCollection`. For more info, see GitHub issue [#8370](https://github.com/microsoft/microsoft-ui-xaml/issues/8370).

### New APIs for 1.5-experimental2

1.5-experimental2 includes the following new APIs. These APIs are not experimental, but are not yet included in a stable release version of the WinAppSDK.

```C#
Microsoft.Graphics.DirectX
 
    DirectXPixelFormat
        A4B4G4R4
```

```C#
Microsoft.UI.Xaml
 
    DebugSettings
        LayoutCycleDebugBreakLevel
        LayoutCycleTracingLevel
 
    LayoutCycleDebugBreakLevel
    LayoutCycleTracingLevel
```

```C#
Microsoft.UI.Xaml.Automation.Peers
 
    SelectorBarItemAutomationPeer
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
Microsoft.Windows.Management.Deployment
 
    EnsureReadyOptions
    PackageDeploymentManager
        AddPackageSetAsync
        DeprovisionPackageAsync
        DeprovisionPackageByUriAsync
        DeprovisionPackageSetAsync
        EnsurePackageReadyAsync
        EnsurePackageReadyByUriAsync
        EnsurePackageSetReadyAsync
        IsPackageReady
        IsPackageReadyByUri
        IsPackageRegistrationPending
        IsPackageRegistrationPendingForUser
        ProvisionPackageAsync
        ProvisionPackageByUriAsync
        ProvisionPackageSetAsync
        RegisterPackageAsync
        RegisterPackageByUriAsync
        RegisterPackageSetAsync
        RemovePackageByUriAsync
        RepairPackageAsync
        RepairPackageByUriAsync
        RepairPackageSetAsync
        ResetPackageAsync
        ResetPackageByUriAsync
        ResetPackageSetAsync
        StagePackageAsync
        StagePackageByUriAsync
        StagePackageSetAsync
 
    PackageDeploymentProgress
    PackageDeploymentResult
        Error
        ErrorText
 
    PackageSet
        Items
        PackageUri
 
    PackageVolume
        FindPackageVolumeByName
        FindPackageVolumeByPath
        FindPackageVolumes
        IsRepairNeeded
        Repair
 
    ProvisionPackageOptions
    RegisterPackageOptions
        DependencyPackageFamilyNames
 
    RemovePackageOptions
        FailIfNotFound
```

### Additional 1.5-experimental2 APIs

This release includes the following new and modified experimental APIs:

```C#
Microsoft.UI.Xaml
 
    Application
        DispatcherShutdownMode
 
    DispatcherShutdownMode
    XamlIsland
        SystemBackdrop
```

```C#
Microsoft.UI.Xaml.Hosting
 
    WindowsXamlManager
        IsXamlRunningOnCurrentThread
        XamlShutdownCompletedOnThread
 
    XamlShutdownCompletedOnThreadEventArgs
```

```C#
Microsoft.Windows.System.Workloads
 
    IWorkloadHandler
    Workload
    WorkloadManager
    WorkloadProgress
    WorkloadProgressStatus
    WorkloadResult
    WorkloadsContract
    WorkloadStatus
```

## Version 1.5 Experimental (1.5.0-experimental1)

This is the latest release of the experimental channel.
To download, retarget your WinAppSDK NuGet version to `1.5.231202003-experimental1`.

### New APIs for 1.5-experimental1

1.5-experimental1 includes the following new APIs. These APIs are not experimental, but are not yet included in a stable release version of the WinAppSDK.

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

### Additional 1.5-experimental1 APIs

This release includes the following new and modified experimental APIs:

```C#
Microsoft.UI.Content
 
    ContentIsland
        ConnectionInfo
        ConnectRemoteEndpoint
        IsRemoteEndpointConnected
 
    ContentIslandEnvironment
        CurrentOrientation
        DisplayScale
        NativeOrientation
        ThemeChanged
 
    ContentSiteEnvironment
        CurrentOrientation
        DisplayScale
        NativeOrientation
        NotifyThemeChanged
 
    DesktopChildSiteBridge
        AcceptRemoteEndpoint
        ConnectionInfo
        IsRemoteEndpointConnected
        RemoteEndpointConnecting
        RemoteEndpointDisconnected
        RemoteEndpointRequestedStateChanged
 
    EndpointConnectionEventArgs
    EndpointRequestedStateChangedEventArgs
    IContentIslandEndpointConnectionPrivate
    IContentSiteBridgeEndpointConnectionPrivate
    ProcessStarter
    SystemVisualSiteBridge
        IsClosed
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
 
    DebugSettings
        LayoutCycleDebugBreaks
        LayoutCycleTracing
 
    LayoutCycleDebugBreakLevel
    LayoutCycleTracingLevel
    XamlIsland
```

```C#
Microsoft.UI.Xaml.Controls
 
    SelectionModel
        SelectAllFlat
```

```C#
Microsoft.UI.Xaml.Core.Direct
 
    XamlPropertyIndex
        FlyoutBase_SystemBackdrop
        Popup_SystemBackdrop
```

```C#
Microsoft.Windows.Management.Deployment
 
    AddPackageOptions
    AddPackageSetOptions
    DeploymentPriority
    DeploymentProcessingModel
    EnsureIsReadyOptions
    FindPackageSetOptions
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
    PackageSetManager
    PackageSetRuntimeDisposition
    PackageVolume
    PackageVolumeManager
    PackageVolumeStatus
    RegisterPackageOptions
    RemovePackageOptions
    StagePackageOptions
    StubPackageOption
```

## Related topics

- [Stable channel](../stable-channel.md)
- [Preview channel](../preview-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../../package-and-deploy/index.md#use-the-windows-app-sdk)
