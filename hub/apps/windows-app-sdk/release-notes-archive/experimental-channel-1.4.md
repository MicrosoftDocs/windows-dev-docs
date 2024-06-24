---
title: Experimental channel release notes for the Windows App SDK 1.4
description: Learn about the experimental channel release notes for the Windows App SDK 1.4
ms.topic: article
ms.date: 04/19/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK 1.4

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

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
