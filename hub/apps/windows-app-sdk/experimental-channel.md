---
title: Experimental release channel for the Windows App SDK
description: Learn about the latest experimental releases of the Windows App SDK.
ms.topic: article
ms.date: 11/16/2021
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel provides releases of the Windows App SDK that include [experimental channel features](release-channels.md#features-available-by-release-channel) that are in early stages of development. APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel might have breaking changes in future releases, but experimental APIs are especially subject to change. Experimental features may be removed from the next release, or may never be released.

**Important links**: 
- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on experimental releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

**Experimental channel releases:**
- [Version 1.5 Experimental](#version-15-experimental-150-experimental1)
- [Version 1.4 Experimental](#version-14-experimental-140-experimental1)
- [Version 1.3 Experimental](#version-13-experimental-130-experimental1)
- [Version 1.2 Experimental](#version-12-experimental-120-experimental2)
- [Version 1.0 Experimental](#version-10-experimental-100-experimental1)
- [Version 0.8 Preview](#version-08-preview-080-preview)

## Version 1.5 Experimental (1.5.0-experimental1)

This is the latest release of the experimental channel.
To download, retarget your WinAppSDK NuGet version to `1.5.231202003-experimental1`.

### New APIs

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

### Additional Experimental APIs

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

## Version 1.4 Experimental (1.4.0-experimental1)

This is the latest release of the experimental channel.
To download, retarget your WinAppSDK NuGet version to `1.4.230518007-experimental1`.

### Bug fixes
This release contains miscellaneous fixes, including the following:

- Fixed an issue where enabling the debug settings framerate counter in a new WinUI desktop application caused an access violation. For more information, see issue [2835](https://github.com/microsoft/microsoft-ui-xaml/issues/2835) on GitHub.
- Fixed an issue where horizontal scrolling on a touchpad did not work in a WebView2 web page. For more information, see issue [7772](https://github.com/microsoft/microsoft-ui-xaml/issues/7772) on GitHub.

### Additional Experimental APIs
This release includes the following new and modified experimental APIs:

```C#
Microsoft.UI

   IClosableNotifier
```

```C#
Microsoft.UI.Composition.SystemBackdrops

   DesktopAcrylicController
       Closed
       FrameworkClosed
       Kind

   DesktopAcrylicKind
   MicaController
       Closed
       FrameworkClosed
```
```C#
Microsoft.UI.Content

   ContentAppWindowBridge
       SettingChanged

   ContentEnvironmentSettingChangedEventArgs
   ContentExternalBackdropLink
   ContentExternalOutputLink
       ExternalOutputBorderMode

   ContentIsland
       FrameworkClosed

   ContentIslandWindow
       SettingChanged

   ContentSite
       FrameworkClosed

   ContentSiteWindow
       NotifySettingChanged
       SettingChanged

   CoreWindowTopLevelWindowBridge
       SettingChanged

   DesktopSiteBridge
       Closed
       FrameworkClosed

   IContentWindow
       SettingChanged

   SystemVisualSiteBridge
       Closed
       FrameworkClosed
```

```C#
Microsoft.UI.Input

   InputLayoutPolicy
   InputNonClientPointerSource
       ConfigurationChanged
       GetForWindowId

   NonClientRegionConfigurationChangedEventArgs
```

```C#
Microsoft.UI.System

   ThemeSettings
```

```C#
Microsoft.UI.Windowing

   DisplayArea
       GetMetricsFromWindowId
```

```C#
Microsoft.UI.Xaml

   XamlRoot
       ContentWindow
```

```C#
Microsoft.UI.Xaml.Controls

   ItemContainer
       Child
       ChildProperty

   ItemContainerMultiSelectMode
       Extended

   ItemsView
       Animator
       AnimatorProperty

   MenuFlyoutPresenter
       SystemBackdrop
       SystemBackdropProperty

   RiverFlowLayout
       InvalidateItemsInfo
       RequestedRangeCount
       RequestedRangeStartIndex

   RiverFlowLayoutElementAnimator
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

## Version 1.3 Experimental (1.3.0-experimental1)

This is the latest release of the experimental channel.
To download, retarget your WinAppSDK NuGet version to `1.3.230202101-experimental1`.

### XAML Backdrop APIs
With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI 3 app.
See the [Xaml Backdrop API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/xaml-backdrop-api.md?plain=1#L39) on GitHub for more information about the **Window.SystemBackdrop** property.

Of note in this release, you're able to set the backdrop only in code-behind, as below. Setting `<Window.SystemBackdrop>` in markup results in a compile error. 
Additionally, the Xaml Backdrop APIs are currently missing an 'experimental' tag as they are under active development.

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
- MRT Core: A new event, `Application.ResourceManagerInitializing` allows your app to provide its own implementation of the `IResourceManager` interface, and gives you access to the ResourceManager that WinUI uses to resolve resource URIs.
- With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.
- A new event, `DebugSettings.XamlResourceReferenceFailed` is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see issues [4972](https://github.com/microsoft/microsoft-ui-xaml/issues/4972), [2350](https://github.com/microsoft/microsoft-ui-xaml/issues/2350), and [6073](https://github.com/microsoft/microsoft-ui-xaml/issues/6073) on GitHub.

### Bug fixes
- Fixed issues with touch input causing the soft keyboard to not appear on text boxes. For more information, see issue [6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.
- Fixed issue causing an ItemsRepeater with an IElementFactory as its ItemTemplate to throw an ArgumentException. For more info, see issue [4705](https://github.com/microsoft/microsoft-ui-xaml/issues/4705) on GitHub.

### Additional Experimental APIs
This release also includes several APIs that are in early development.

The list below details the APIs introduced in this experimental release that we don't plan to ship in the 1.3.0 stable release.

```csharp
**Microsoft.UI.Content**

    DesktopSiteBridge
        GetInputEnabledToRoot
        GetVisibleToRoot
        InputEnabled
```

```csharp
**Microsoft.UI.Dispatching**

    DispatcherQueue
        FrameworkShutdownStarting
```

```csharp
**Microsoft.UI.Input**

    InputLightDismissAction
        GetForIsland

    InputNonClientPointerSource
    InputPointerActivationBehavior
    InputPointerSource
        ActivationBehavior

    NonClientRegionCaptionTappedEventArgs
    NonClientRegionHoverEventArgs
    NonClientRegionKind
```

```csharp
**Microsoft.UI.Input.DragDrop**

    DragDropManager
    DragDropModifiers
    DragInfo
    DragOperation
    DragUIContentMode
    DragUIOverride
    DropOperationTargetRequestedEventArgs
    IDropOperationTarget
```

```csharp
**Microsoft.UI.Xaml.Automation.Peers**

    ItemContainerAutomationPeer
    ItemsViewAutomationPeer
```

```csharp
**Microsoft.UI.Xaml.Controls**

    AnnotatedScrollBar
    AnnotatedScrollBarLabel
    AnnotatedScrollBarScrollEventArgs
    AnnotatedScrollBarScrollEventType
    AnnotatedScrollBarScrollOffsetRequestedEventArgs
    AnnotatedScrollBarSubLabelRequestedEventArgs
    AnnotatedScrollBarValueRequestedEventArgs
    ElementFactory
        GetElement
        GetElementCore
        RecycleElement
        RecycleElementCore

    IndexBasedLayoutOrientation
    ItemContainer
    ItemContainerInteractionTrigger
    ItemContainerInvokedEventArgs
    ItemContainerMultiSelectMode
    ItemContainerUserInvokeMode
    ItemContainerUserSelectMode
    ItemsView
    ItemsViewItemInvokedEventArgs
    ItemsViewItemInvokeMode
    ItemsViewSelectionMode
    Layout
        IndexBasedLayoutOrientation

    NonVirtualizingLayout
        IndexBasedLayoutOrientationCore

    RiverFlowLayout
    RiverFlowLayoutItemsInfoRequestedEventArgs
    RiverFlowLayoutItemsJustification
    RiverFlowLayoutItemsStretch
    VirtualizingLayout
        IndexBasedLayoutOrientationCore

    VirtualizingLayoutContext
        VisibleRect
        VisibleRectCore
```

```csharp
**Microsoft.Graphics.Display**

    DisplayInformation
        AngularOffsetFromNativeOrientation
        DpiChanged
        OrientationChanged
        RawDpi
        RawPixelsPerViewPixel

    DisplayOrientation
```

```csharp
**Microsoft.UI.Xaml.Hosting**

    DesktopWindowXamlSource
        CreateSiteBridge
        SiteBridge
        SystemBackdrop
```
## Version 1.2 Experimental (1.2.0-experimental2)

This is the latest release of the experimental channel. It supports all [experimental channel features](release-channels.md#features-available-by-release-channel) and features from [1.2.0-preview 1](preview-channel.md#version-12-preview-1-120-preview1).

To download, retarget your WinAppSDK NuGet version to `1.2.220909.2-experimental2`.

### Fixed issue
In upcoming Windows Insider Preview builds, applications using Windows App SDK would fail to launch.

## Version 1.2 Experimental (1.2.0-experimental1)

This is the latest release of the experimental channel. It supports all [experimental channel features](release-channels.md#features-available-by-release-channel).

To download, retarget your WinAppSDK NuGet version to `1.2.220727.1-experimental1`.

### Input & Composition
First introduced in Windows App SDK 0.8, there are several experimental classes in the
[Microsoft.UI.Input.Experimental](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.experimental) & [Microsoft.UI.Composition.Experimental](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.experimental) namespaces.

New to this release:
- [InputPointerSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputpointersource) has a new static factory, GetforWindowId.

### Content
New to this release, the experimental classes in the Microsoft.UI.Content namespace provide the building blocks of interactive content. These are low level primitives that can be assembled into content to provide the interactive experience for an end user. The content defines the structure for: rendering output with animations, processing input on different targets, providing accessibility representation, and handling host state changes.

Notable APIs:
- `ContentIsland` - brings together Output, Input, and Accessibility and provides the abstraction for interactive content. A custom visual tree can be constructed and made interactive with these APIs.
- `DesktopChildSiteBridge` - enables a `ContentIsland` to be connected into a HWND-based hierarchy.

Check out the [sample on GitHub](https://aka.ms/windowsappsdk/1.2/1.2.0-experimental1/content-islands-sample) for more information.

### Dispatching
[DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) now dispatches as reentrant. Previously, no more than a single [DispatcherQueueHandler](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueuehandler) callback could be active on a single thread at a time. Now, if a handler starts a nested message pump, additional callbacks dispatch as reentrant. This matches Win32 behavior around window messages and nested message pumps.

### Notifications
Registering app display name and icon for app notification is now supported. Check out the [spec on GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppNotifications/AppNotifications-spec.md) for additional information.

### WinUI 3

- Controls and styles are up to date with the [WinUI 2.8](../winui/winui2/release-notes/winui-2.8.md) release.
- UWP is no longer supported in the experimental releases.

### Other limitations and known issues

- Apps need to be rebuilt after updating to Windows App SDK 1.2-experimental1 due to a breaking change introduced in the ABI.
- Apps that reference a package that depends on WebView2 (like Microsoft.Identity.Client) fail to build. This is caused by conflicting binaries at build time. See [issue 2492](https://github.com/microsoft/WindowsAppSDK/issues/2492) on GitHub for more information.
- Using `dotnet build` with a WinAppSDK C# class library project may see a build error "Microsoft.Build.Packaging.Pri.Tasks.ExpandPriContent task could not be loaded". To resolve this issue set `<EnableMsixTooling>true</EnableMsixTooling>` in your project file.
- The default WinAppSDK templates note that the MaxVersionTested="10.0.19041.0" when it should be "10.0.22000.0". For full support of some features, notably UnlockedDEHs, update the MaxVersionTested to "10.0.22000.0" in your project file.

## Version 1.0 Experimental (1.0.0-experimental1)

This release supports all [experimental channel features](release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](https://aka.ms/windowsappsdk/experimental-vsix)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release of WinUI 3 is focused on building towards new features for 1.0 stable and fixing bugs.

- **New features**: Support for showing a ContentDialog per window rather than per thread.
- **Bugs**: For the full list of bugs addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-exp-announcement). 
- **Samples**: To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 Gallery app [from GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main), or download the app [from the Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC).

For more information or to get started developing with WinUI, see:

- [Windows UI 3 Library (WinUI)](../winui/index.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)

### Push notifications (experimental feature)

This release introduces a push notifications API that can be used by packaged desktop apps with Azure app registration-based identities. To use this feature, you must [sign up for our private preview](https://aka.ms/windowsappsdk/push-private-preview).

Important limitations:

- Push notifications are only supported in MSIX packaged apps that are running on Windows 10 version 2004 (build 19041) or later releases.
- Microsoft reserves the right to disable or revoke apps from push notifications during the private preview.
- Microsoft does not guarantee the reliability or latency of push notifications.
- During the private preview, push notification volume is limited to 1 million per month.

For more information, see [Push notifications](notifications/push-notifications/index.md).

### Windowing 

This release includes updates to the windowing APIs. These are a set of high-level windowing APIs, centered around the AppWindow class, which allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and other apps. This is similar to, but not the same as, the UWP AppWindow.

Important limitations:

- This release of `AppWindow` is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an `AppWindow`; you're limited to using the `HWND` interop access methods.
- The Windowing API's will currently not work on Windows version 1809 and 1903 for AMD64.

For more information, see [Manage app windows](windowing/windowing-overview.md).

### Deployment for unpackaged apps 

This release introduces updates to the *dynamic dependencies* feature, including the *bootstrapper API*.

Important limitations:

- The dynamic dependencies feature is only supported for unpackaged apps.
- Elevated callers aren't supported.

For more information, see the following articles:

- [Use MSIX framework packages dynamically from your desktop app](../desktop/modernize/framework-packages/framework-packages-overview.md)
- [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md)

### Other limitations and known issues

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).
- **C# apps using 1.0 Experimental must use one of the following .NET SDKs**: 
	- .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

## Version 0.8 Preview (0.8.0-preview)

This release supports all [experimental channel features](release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/previewdownload)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release introduces critical bug fixes (including those fixed in 0.5 servicing releases) and other changes for WinUI. For a complete list, see the [WinUI 3 - Windows App SDK 0.8 release notes](../winui/winui3/release-notes/release-notes-08-preview.md).

### App lifecycle (experimental feature)

This release introduces new experimental features related to managing the app lifecycle of your app.

- All apps (packaged and unpackaged) can use **GetActivatedEventArgs** (although packaged apps can already use the implementation of this in the platform).
- Only unpackaged apps can use the **RegisterForXXXActivation** functions.
- Packaged desktop apps can use app lifecycle instancing.


For more information, see [App instancing](applifecycle/applifecycle-instancing.md) and [Rich activation](applifecycle/applifecycle-rich-activation.md).

### Deployment for unpackaged apps (experimental feature)

This release introduces new experimental deployment features for unpackaged apps. Unpackaged apps can now dynamically take a dependency on the Windows App SDK runtime packages so you can continue using your existing MSI or setup program for app deployment. This is available through the following features:

- Standalone installer for Windows App SDK.
- MSIX package bundle that includes dynamic dependencies functionality.

For more more info, see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md).

### DWriteCore

This release adds the following features:  

- Text decorations (underline and strikethrough in the text layout API)
- Vertical text layout
- Font face kerning API
- Experimental support for unpackaged apps

For more more information, see [Render text with DWriteCore](dwritecore.md).

### MRT Core

This release adds the following features:

- The build action for resources is now automatically set, reducing the need for manual project configuration.
- Experimental support for unpackaged apps.

For more more information, see [Manage resources with MRT Core](mrtcore/mrtcore-overview.md).

### Limitations and known issues

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).

### Samples

The [Windows App SDK samples](https://github.com/microsoft/Project-Reunion-Samples) do not yet work with this release. New and updated samples, including samples that demonstrate new features such as unpackaged app deployment, are coming soon.

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
