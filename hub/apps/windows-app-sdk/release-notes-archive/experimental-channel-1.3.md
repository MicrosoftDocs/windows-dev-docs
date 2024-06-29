---
title: Experimental channel release notes for the Windows App SDK 1.3
description: Learn about the experimental channel release notes for the Windows App SDK 1.3
ms.topic: article
ms.date: 04/19/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK 1.3

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel provides releases of the Windows App SDK that include [experimental channel features](../release-channels.md#features-available-by-release-channel) that are in early stages of development. APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel might have breaking changes in future releases, but experimental APIs are especially subject to change. Experimental features may be removed from the next release, or may never be released.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on experimental releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest experimental channel release:**

- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

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

Replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from a **Window** through `Window.AppWindow`. See the [Window.AppWindow API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/appwindow-spec.md) on GitHub for additional background and usage information.

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

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
