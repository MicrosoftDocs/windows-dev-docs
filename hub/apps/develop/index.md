---
description: This section of the documentation provides information about APIs and features you can use while developing a Windows desktop apps.
title: Develop Windows desktop apps
ms.topic: article
ms.date: 10/07/2021
ms.localizationpriority: medium
---

# Develop Windows desktop apps

This section of the documentation provides information about APIs and features you can use while developing Windows desktop apps. Some of these features are available by using APIs in the [Windows App SDK](../windows-app-sdk/index.md). Other features are available by using APIs in the Windows OS (via the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk)) and .NET, and don't require use of the Windows App SDK.

[!INCLUDE [UWP migration guidance](../windows-app-sdk/includes/uwp-app-sdk-migration-pointer.md)]

## Windows App SDK features

The following table highlights the development features that are provided by the current releases of the Windows App SDK. For more details about the release channels of the Windows App SDK that include each of these features, see [Features available by release channel](../windows-app-sdk/release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Learn about the Windows App SDK](../windows-app-sdk/index.md)

| Feature | Description |
|--|--|
| [WinUI 3](../winui/index.md) | The premiere native user interface (UI) framework for Windows desktop apps, including managed apps that use C# and .NET and native apps that use C++ with the Win32 API. WinUI 3 provides consistent, intuitive, and accessible experiences using the latest user interface (UI) patterns. |
| [Render text with DWriteCore](../windows-app-sdk/dwritecore.md) | Render text using a device-independent text layout system, high quality sub-pixel Microsoft ClearType text rendering, hardware-accelerated text, multi-format text, wide language support, and much more. |
| [Manage resources with MRT Core](../windows-app-sdk/mrtcore/mrtcore-overview.md) | Manage app resources such as strings and images in multiple languages, scales, and contrast variants independently of your app's logic. |
| [App lifecycle: App instancing](../windows-app-sdk/applifecycle/applifecycle-instancing.md) | Control whether multiple instances of your app's process can run at the same time. |
| [App lifecycle: Rich activation](../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) | Process information about different kinds activations for your app. |
| [App lifecycle: Power management](../windows-app-sdk/applifecycle/applifecycle-power.md) | Gain visibility into how your app affects the device's power state, and enable the app to make intelligent decisions about resource usage. |
| [Manage app windows](../windows-app-sdk/windowing/windowing-overview.md) | Create and manage the windows associated with your app. |
| [Push notifications](../windows-app-sdk/notifications/push-notifications/index.md) | Send raw notifications and app notifications to your app from the cloud using Azure App Registration identities. |
| [App notifications](../windows-app-sdk/notifications/app-notifications/index.md) | Deliver messages to your user with app notifications. |
| [Deployment](../windows-app-sdk/deployment-architecture.md) | Deploy the Windows App SDK runtime with your unpackaged and packaged app |

## Windows app development features organized by scenario

The following articles provide information to help you get started using features of the full Windows app development platform for common app scenarios, including features provided by the Windows App SDK, the Windows SDK, and the .NET SDK.

* [Modernize your existing desktop apps](../desktop/modernize/index.md)
* [User interface and input](user-interface.md)
* [App lifecycle and system services](app-lifecycle-and-system-services.md)
* [Communication](communication.md)
* [Accessibility](accessibility.md)
* [Audio, video, and camera](audio-video-camera.md)
* [Graphics](graphics.md)
* [Data and files](data-and-files.md)
* [Windows AI and machine learning](/windows/ai/)
* [Devices and sensors](devices-and-sensors.md)
* [Security and identity](security-and-identity.md)
* [Deployment overview](../package-and-deploy/index.md)

For information about setting up your development environment and getting started creating a new app, see the following articles:

* [Install tools for the Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)
* [Get started developing apps](../get-started/index.md)

## Related topics

* [Windows App SDK](../windows-app-sdk/index.md)
* [WinUI](../winui/index.md)
* [Deployment overview](../package-and-deploy/index.md)