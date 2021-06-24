---
title: Build desktop Windows apps with the Windows App SDK 
description: Learn about the Windows App SDK, benefits it provides to developers, what is ready for developers now, and how to give feedback.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, desktop development, Windows App SDK
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Windows App SDK

The Windows App SDK is a set of new developer components and tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 and downlevel to Windows 10, version 1809.

The Windows App SDK does not replace the existing desktop Windows app types such as .NET (including Windows Forms and WPF) and desktop Win32 with C++. Instead, it complements these existing platforms with a common set of APIs and tools that developers can rely on across these platforms. For more details, see [Benefits of the Windows App SDK](#benefits-of-the-windows-app-sdk-for-windows-developers).

> [!NOTE]
> The Windows App SDK was previously known by the code name **Project Reunion**. Some SDK assets such as the VSIX extension and NuGet packages still use the code name, but these assets will be renamed in a future release. Some areas of the documentation still use **Project Reunion** when referring to an existing asset or a specified earlier release.

## Get started with the Windows App SDK

The Windows App SDK provides an extension for Visual Studio 2019 that includes project templates configured to use the Windows App SDK components in new projects. The Windows App SDK libraries are also available via a NuGet package that you can install in existing projects. 

For more information about getting started with the Windows App SDK, see these articles:

- [Release channels and release notes](release-channels.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)

## Benefits of the Windows App SDK for Windows developers

The Windows App SDK provides a broad set of Windows APIs with implementations that are decoupled from the OS and released to developers via NuGet packages. The Windows App SDK is not meant to replace the Windows SDK. The Windows SDK will continue to work as is, and there are many core components of Windows that will continue to evolve via APIs that are delivered via OS and Windows SDK releases. Developers are encouraged to adopt the Windows App SDK at their own pace.

### Unified API surface across desktop app platforms

Developers who want to create desktop Windows apps must choose between several app platforms and frameworks. Although each platform provides many features and APIs that can be used by apps that are built using other platforms, some features and APIs can only be used by specific platforms. The Windows App SDK unifies access to Windows APIs for desktop Windows 11 and Windows 10 apps. No matter which app model you choose, you will have access to the same set of Windows APIs that are available in the Windows App SDK.

Over time, we plan to make further investments in the Windows App SDK that remove more distinctions between the different app models. The Windows App SDK will include both WinRT APIs and native C APIs.

### Consistent support across Windows versions

As the Windows APIs continue to evolve with new OS versions, developers must use techniques such as [version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code) to account for all the differences in versions to reach their application audience. This can add complexity to the code and the development experience.

Windows App SDK APIs will work on Windows 11 and downlevel to Windows 10, version 1809. This means that as long as your customers are on Windows 10, version 1809, or any later version of Windows, you can use new Windows App SDK APIs and features as soon as they are released, and without having to write version adaptive code.

### Faster release cadence

New Windows APIs and features have typically been tied to OS releases that happen on a once or twice a year release cadence. The Windows App SDK will ship updates on a faster cadence, enabling you to get earlier and more rapid access to innovations in the Windows development platform as soon as they are created.

## Developer roadmap

For the latest Windows App SDK plans, see our [roadmap](https://github.com/microsoft/ProjectReunion/blob/main/docs/roadmap.md).

## Give feedback and contribute

We are building the Windows App SDK as an open source project. We have a lot more information on our [Github page](https://github.com/microsoft/ProjectReunion) about how we're building the Windows App SDK, and how you can be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/ProjectReunion/blob/master/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that the Windows App SDK brings the biggest benefits to developers like you.

## Related topics

- [Release channels and release notes](release-channels.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)
