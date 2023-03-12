---
title: Build desktop Windows apps with the Windows App SDK 
description: Learn about the Windows App SDK, benefits it provides to developers, what is ready for developers now, and how to give feedback.
ms.topic: article
ms.date: 07/29/2022
keywords: windows win32, desktop development, Windows App SDK
ms.localizationpriority: medium
---

# Windows App SDK

The Windows App SDK is a set of new developer components and tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 and downlevel to Windows 10, version 1809.


> [!VIDEO ff5c6036-2911-4ea8-80da-89befb39267b]

The Windows App SDK doesn't replace the Windows SDK or existing desktop Windows app types such as .NET (including Windows Forms and WPF) and desktop Win32 with C++. Instead, the Windows App SDK complements these existing tools and app types with a common set of APIs that developers can rely on across these platforms. For more details, see [Benefits of the Windows App SDK](#benefits-of-the-windows-app-sdk-for-windows-developers).

[!INCLUDE [UWP migration guidance](../windows-app-sdk/includes/uwp-app-sdk-migration-pointer.md)]

## Get started with the Windows App SDK

The Windows App SDK provides extensions for Visual Studio 2019 and Visual Studio 2022. These extensions include project templates configured to use the Windows App SDK components in new projects. The Windows App SDK libraries are also available via a NuGet package that you can install in existing projects.

> [!div class="button"]
> [Install tools for the Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)

If you have already installed the required developer tools, you're ready to [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md). You can also [use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md).

For guidance on specific Windows App SDK versions, see [Release channels](release-channels.md) and [Downloads](downloads.md).

### Windows App SDK features

The following table highlights the development features that are provided by the current releases of the Windows App SDK. For more details about the release channels of the Windows App SDK that include each of these features, see [Features available by release channel](release-channels.md#features-available-by-release-channel).

| Feature | Description |
|--|--|
| [WinUI 3](../winui/index.md) | The premiere native user interface (UI) framework for Windows desktop apps, including managed apps that use C# and .NET and native apps that use C++ with the Win32 API. WinUI 3 provides consistent, intuitive, and accessible experiences using the latest user interface (UI) patterns. |
| [Render text with DWriteCore](dwritecore.md) | Render text using a device-independent text layout system, high quality sub-pixel Microsoft ClearType text rendering, hardware-accelerated text, multi-format text, wide language support, and much more. |
| [Manage resources with MRT Core](mrtcore/mrtcore-overview.md) | Manage app resources such as strings and images in multiple languages, scales, and contrast variants independently of your app's logic. |
| [App lifecycle: App instancing](applifecycle/applifecycle-instancing.md) | Control whether multiple instances of your app's process can run at the same time. |
| [App lifecycle: Rich activation](applifecycle/applifecycle-rich-activation.md) | Process information about different kinds activations for your app. |
| [App lifecycle: Power management](applifecycle/applifecycle-power.md) | Gain visibility into how your app affects the device's power state, and enable the app to make intelligent decisions about resource usage. |
| [Manage app windows](windowing/windowing-overview.md) | Create and manage the windows associated with your app. |
| [Push notifications](../windows-app-sdk/notifications/push-notifications/index.md) | Send raw notifications and app notifications to your app from the cloud using Azure App Registration identities. |
| [App notifications](../windows-app-sdk/notifications/app-notifications/app-notifications-quickstart.md) | Send app notifications to your app locally. |
| [Deployment](deployment-architecture.md) | Deploy Windows App SDK runtime with your unpackaged and packaged app |

### Windows App SDK release channels

The following table provides an overview of the different release channels.

| Release channel | Description |
|--|--|
| [Stable](stable-channel.md) | This channel is supported for use by apps in production environments. It only includes stable APIs. By default, the Windows App SDK docs focus on the Stable channel. |
| [Preview](preview-channel.md) | This channel provides a preview of the next stable release. There may be breaking API changes between a given preview channel release and the next stable release. For documentation on using the Preview release, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md). |
| [Experimental](experimental-channel.md) | This channel includes experimental features that are in early stages of development. Experimental features may be removed from the next release, or may never be released. For documentation on using the Experimental release, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md). |

 For more details about the release channels of the Windows App SDK, see [Windows App SDK release channels](release-channels.md).

## Benefits of the Windows App SDK for Windows developers

The Windows App SDK provides a broad set of Windows APIs with implementations that are decoupled from the OS and released to developers via NuGet packages. The Windows App SDK is not meant to replace the Windows SDK. The Windows SDK will continue to work as is, and there are many core components of Windows that will continue to evolve via APIs that are delivered via OS and Windows SDK releases. Developers are encouraged to adopt the Windows App SDK at their own pace.

### Unified API surface across desktop app platforms

Developers who want to create desktop Windows apps must choose between several app platforms and frameworks. Although each platform provides many features and APIs that can be used by apps that are built using other platforms, some features and APIs can only be used by specific platforms. The Windows App SDK unifies access to Windows APIs for desktop Windows 11 and Windows 10 apps. No matter which app model you choose, you will have access to the same set of Windows APIs that are available in the Windows App SDK.

Over time, we plan to make further investments in the Windows App SDK that remove more distinctions between the different app models. The Windows App SDK will include both WinRT APIs and native C APIs.

### Consistent experience across Windows versions

As the Windows APIs continue to evolve with new OS versions, developers must use techniques such as [version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code) to account for all the differences in versions to reach their application audience. This can add complexity to the code and the development experience.

Windows App SDK APIs will work on Windows 11 and downlevel to Windows 10, version 1809. This means that as long as your customers are on Windows 10, version 1809, or any later version of Windows, you can use new Windows App SDK APIs and features as soon as they are released, and without having to write version adaptive code.

### Faster release cadence

New Windows APIs and features are usually shipped within major Windows OS updates that release about once a year (often less frequently). Windows App SDK ships new releases about once every six months. This release cadence ensures that you continuously have access to the latest innovations in the Windows development platform.

## Developer roadmap

For the latest Windows App SDK plans, see our [roadmap](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/roadmap.md).

## Give feedback and contribute

We are building the Windows App SDK as an open source project. We have a lot more information on our [Github page](https://github.com/microsoft/WindowsAppSDK) about how we're building the Windows App SDK, and how you can be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that the Windows App SDK brings the biggest benefits to developers like you.

## Related topics

- [Release channels and release notes](release-channels.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
