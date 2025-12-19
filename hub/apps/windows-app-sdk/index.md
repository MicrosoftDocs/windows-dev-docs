---
title: Build desktop Windows apps with the Windows App SDK 
description: Learn about the Windows App SDK, benefits it provides to developers, what is ready for developers now, and how to give feedback.
ms.topic: article
ms.date: 07/14/2025
keywords: windows win32, desktop development, Windows App SDK
ms.localizationpriority: medium
---

# Windows App SDK

:::image type="icon" source="images/windows-app-sdk-hero.png":::

The Windows App SDK provides a unified set of APIs and tools that you can use to build modern Windows apps. It brings the latest Windows platform features to both [WinUI](../winui/winui3/index.md) and existing desktop app frameworks such as WPF, Windows Forms, or Win32.


Windows App SDK is built in the open, and contributions or discussions are welcome. Visit the repository for details:

<div class="buttons margin-top-xs">
	<a href="https://github.com/microsoft/WindowsAppSDK" class="button button-sm"><span class="icon docon docon-brand-github" role="img"></span><span>View on GitHub</span></a>
</div>

### What you can do with the Windows App SDK

Whether you are building a new app with WinUI 3 or enhancing an existing one with modern capabilities, the Windows App SDK helps you take advantage of the latest Windows APIs.

:::image type="content" source="images/windows-app-sdk-overview.png" alt-text="Diagram of the Windows App SDK architecture" border="false":::

Here's a breakdown of the major features that Windows App SDK provides:

| Feature | Description |
|--|--|
| [WinUI](../winui/winui3/index.md) | The modern native UI (user interface) framework for Windows apps, supporting both .NET (C#) and C++ projects. WinUI enables consistent, accessible, and beautiful user experiences that align with the Fluent Design system. |
| [Windows AI APIs](/windows/ai/apis/) | Bring powerful, hardware-accelerated artificial intelligence (AI) capabilities to your apps, running local models seamlessly and efficiently on Copilot+ PCs. |
| [Windows ML](/windows/ai/new-windows-ml/overview) | Run ONNX AI models locally on Windows, automatically optimizing performance across available hardware such as CPUs, GPUs, and NPUs for fast, efficient inference. |
| [Modern text rendering](dwritecore.md) | Use a device-independent text layout and rendering system with [ClearType](/typography/cleartype/) subpixel rendering, hardware acceleration, and broad language support for precise, high-quality text. |
| [Resource management](mrtcore/mrtcore-overview.md) | Manage app resources such as strings and images in multiple languages, scales, and contrast variants independently of your app's logic. |
| [App lifecycle](applifecycle/applifecycle.md) | Manage key aspects of your app's runtime behavior, including [instance management](applifecycle/applifecycle-instancing.md), [rich activation](applifecycle/applifecycle-rich-activation.md) (file, protocol, restart, and more), and [power management](applifecycle/applifecycle-power.md). Gain control over how your app starts, runs, and conserves system resources. |
| [Windowing](windowing/windowing-overview.md) | Create, position, and customize app windows with modern windowing APIs. |
| Notifications | Send local or cloud-based notifications to engage users and keep your app connected, including both [app notifications](../develop/notifications/app-notifications/index.md) and [push notifications](../develop/notifications/push-notifications/index.md). |
| [Widgets](../develop/widgets/widget-providers.md) | Bring personalized, glanceable information to the Windows widget board, allowing users to stay connected to your app's live content right from their desktop. |
| [XAML Islands](../desktop/modernize/xaml-islands/xaml-islands.md) | Embed modern Windows UI controls inside existing desktop apps to refresh your interface and add new capabilities without a full rewrite. |
| [Deployment](deployment-architecture.md) | Deploy the Windows App SDK runtime with your app, whether it is packaged or unpackaged, to ensure consistent and reliable operation across devices. |

---

### Benefits of using the Windows App SDK

The Windows App SDK provides a modern foundation for building Windows apps with a unified set of APIs delivered through NuGet. It works alongside the Windows SDK and gives developers access to new Windows features on a faster release cycle.

- **Modern UI out of the box:** The Windows App SDK includes [WinUI](../winui/winui3/index.md), a powerful native UI framework for creating modern, high-performance interfaces that align with the Fluent Design system. You can use it to build new desktop apps or refresh existing experiences with modern visuals and controls.


- **Works with your existing app and development stack:** The Windows App SDK can be added to existing apps built with WPF, Windows Forms, Win32, or other application frameworks.

- **Modular SDK design:** Employs a metapackage structure that lets apps reference the full SDK or only specific components, for example WinUI, AI, or text rendering. This enables incremental adoption and reduces overall package size.

- **Consistent across Windows versions:** Windows App SDK APIs run on Windows 11 and earlier versions starting from Windows 10, version 1809. This allows you to use new features as soon as they are released without depending on operating system updates or writing version adaptive code.

- **Faster release cadence:** Because the Windows App SDK is released independently from the operating system, new APIs and improvements become available several times per year. This faster cadence gives developers earlier access to the latest Windows features without waiting for major OS updates.

---

### Windows App SDK release channels

The Windows App SDK is available through multiple release channels that let you choose the right balance between stability and early access.

| Release channel | Description |
|-----------------|-------------|
| **Stable** | Intended for production apps and includes only stable, supported APIs. This is the default channel used throughout the Windows App SDK documentation. |
| **Preview** | Offers an early look at what’s coming in the next stable release. API changes can occur between a preview release and its corresponding stable version. |
| **Experimental** | Contains features that are early in development and may change or be removed before future releases. |


 For more details about the release channels of the Windows App SDK, see [Windows App SDK release channels](release-channels.md).

### Get started with the Windows App SDK

- For new apps, explore [WinUI](../winui/winui3/index.md) and [get started building your first WinUI app](../get-started/start-here.md).

- To integrate the Windows App SDK into an existing WPF, Windows Forms, Win32, or cross-platform project, see [use the Windows App SDK in an existing app](use-windows-app-sdk-in-existing-project.md)  guidance.
- For version-specific details, visit [Release channels](release-channels.md) and [Downloads](downloads.md).

[!INCLUDE [UWP migration guidance](../windows-app-sdk/includes/uwp-app-sdk-migration-pointer.md)]


### Give feedback and contribute

We are building the Windows App SDK as an open source project. We have a lot more information on our [GitHub page](https://github.com/microsoft/WindowsAppSDK) about how we're building the Windows App SDK, and how you can be a part of the development process. Check out our [contributor guide](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/contributor-guide.md) to ask questions, start discussions, or make feature proposals. We want to make sure that the Windows App SDK brings the biggest benefits to developers like you.

## Related topics

- [Release channels and release notes](release-channels.md)
- [Create your first WinUI 3 project](../get-started/start-here.md)