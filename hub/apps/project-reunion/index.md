---
title: Build desktop Windows apps with Project Reunion 
description: Learn about Project Reunion, benefits it provides to developers, what is ready for developers now, and how to give feedback.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, desktop development, project reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Build desktop Windows apps with Project Reunion

Project Reunion is a set of new developer components and tools that represent the next evolution in the Windows app development platform. Project Reunion provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on a broad set of target Windows 10 OS versions.

Project Reunion does not replace the existing desktop Windows app types such as .NET (including Windows Forms and WPF) and desktop Win32 with C++. Instead, it complements these existing platforms with a common set of APIs and tools that developers can rely on across these platforms. For more details, see [Benefits of Project Reunion](#benefits-of-project-reunion-for-windows-developers).

> [!NOTE]
>**Project Reunion** is a code name that may change in a future release.

## Get started with Project Reunion

Project Reunion provides an extension for Visual Studio 2019 that includes project templates configured to use Project Reunion components in new projects. The Project Reunion libraries are also available via a NuGet package that you can install in existing projects. 

For more information about getting started with Project Reunion, see these articles:

- [Release channels and release notes](release-channels.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)

## Benefits of Project Reunion for Windows developers

Project Reunion provides a broad set of Windows APIs with implementations that are decoupled from the OS and released to developers via NuGet packages. Project Reunion is not meant to replace the Windows SDK. The Windows SDK will continue to work as is, and there are many core components of Windows that will continue to evolve via APIs that are delivered via OS and Windows SDK releases. Developers are encouraged to adopt Project Reunion at their own pace.

### Unified API surface across desktop app platforms

Developers who want to create desktop Windows apps must choose between several app platforms and frameworks. Although each platform provides many features and APIs that can be used by apps that are built using other platforms, some features and APIs can only be used by specific platforms. Project Reunion will unify access to Windows APIs for all desktop Windows 10 apps. No matter which app model you choose, you will have access to the same set of Windows APIs that are available in Project Reunion.

Over time, we plan to make further investments in Project Reunion that remove more distinctions between the different app models. Project Reunion will include both WinRT APIs and native C APIs.

### Consistent support across Windows 10 versions

As the Windows APIs continue to evolve with new OS versions, developers must use techniques such as [version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code) to account for all the differences in versions to reach their application audience. This can add complexity to the code and the development experience.

Project Reunion APIs will work on Windows 10, version 1809, and all later versions of Windows 10. This means that as long as your customers are on Windows 10, version 1809, or any later version, you can use new Project Reunion APIs and features as soon as they are released, and without having to write version adaptive code.

### Faster release cadence

New Windows APIs and features have typically been tied to OS releases that happen on a once or twice a year release cadence. Project Reunion will ship updates on a faster cadence, enabling you to get earlier and more rapid access to innovations in the Windows development platform as soon as they are created.

## Developer roadmap

For the latest Project Reunion plans, see our [roadmap](https://github.com/microsoft/ProjectReunion/blob/main/docs/roadmap.md).

## Give feedback and contribute

We are building Project Reunion as an open source project. We have a lot more information on our [Github page](https://github.com/microsoft/ProjectReunion) about how we're building Project Reunion, and how you can be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/ProjectReunion/blob/master/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that Project Reunion brings the biggest benefits to developers like you.

## Related topics

- [Release channels and release notes](release-channels.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)
