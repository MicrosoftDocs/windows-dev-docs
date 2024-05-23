---
title: Experimental channel release notes for the Windows App SDK 1.2
description: Learn about the experimental channel release notes for the Windows App SDK 1.2
ms.topic: article
ms.date: 04/19/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Experimental channel release notes for the Windows App SDK 1.2

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

## Version 1.2 Experimental (1.2.0-experimental2)

This is the latest release of the experimental channel. It supports all [experimental channel features](../release-channels.md#features-available-by-release-channel) and features from [Version 1.2 Preview 1 (1.2.0-preview1)](preview-channel-1.2.md#version-12-preview-1-120-preview1).

To download, retarget your WinAppSDK NuGet version to `1.2.220909.2-experimental2`.

### Fixed issue

In upcoming Windows Insider Preview builds, applications using Windows App SDK would fail to launch.

## Version 1.2 Experimental (1.2.0-experimental1)

This is the latest release of the experimental channel. It supports all [experimental channel features](../release-channels.md#features-available-by-release-channel).

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

- Controls and styles are up to date with the [Windows UI Library 2.8](../../winui/winui2/release-notes/winui-2.8.md) release.
- UWP is no longer supported in the experimental releases.

### Other limitations and known issues

- Apps need to be rebuilt after updating to Windows App SDK 1.2-experimental1 due to a breaking change introduced in the ABI.
- Apps that reference a package that depends on WebView2 (like Microsoft.Identity.Client) fail to build. This is caused by conflicting binaries at build time. See [issue 2492](https://github.com/microsoft/WindowsAppSDK/issues/2492) on GitHub for more information.
- Using `dotnet build` with a WinAppSDK C# class library project may see a build error "Microsoft.Build.Packaging.Pri.Tasks.ExpandPriContent task could not be loaded". To resolve this issue set `<EnableMsixTooling>true</EnableMsixTooling>` in your project file.
- The default WinAppSDK templates note that the MaxVersionTested="10.0.19041.0" when it should be "10.0.22000.0". For full support of some features, notably UnlockedDEHs, update the MaxVersionTested to "10.0.22000.0" in your project file.

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
