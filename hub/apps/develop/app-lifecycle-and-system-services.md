---
title: App lifecycle and system services
description: This article provides an index of development features that are related to scenarios involving managing the lifecycle of Windows apps and system-level services.
ms.topic: article
ms.date: 10/07/2021
ms.localizationpriority: medium
---

# App lifecycle and system services

This article provides an index of development features that are related to scenarios involving managing the lifecycle of Windows apps and using system-level services provided by the Windows OS.

## Windows App SDK features

The [Windows App SDK](../windows-app-sdk/index.md) provides the following features related to app lifecycle and system services for Windows 10 and later OS releases.

[!INCLUDE [UWP migration guidance](../windows-app-sdk/includes/uwp-app-sdk-migration-pointer.md)]

| Feature | Description |
|---------|-------------|
| [App lifecycle](../windows-app-sdk/applifecycle/applifecycle.md) | Get an overview of managing the lifecycle of your app. |
| [App instancing](../windows-app-sdk/applifecycle/applifecycle-instancing.md) | Control whether multiple instances of your app's process can run at the same time. |
| [Rich activation](../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) | Receive information about different kinds activations for your app. |
| [Power management](../windows-app-sdk/applifecycle/applifecycle-power.md) | Get visibility into how your app affects the device's power state, and enable your app to make intelligent decisions about resource usage.
| [Restart](../windows-app-sdk/applifecycle/applifecycle-restart.md) | Programmatically restart your application and set restart options after app termination. |

## Windows OS features

Windows 10 and later OS releases provide a wide variety of APIs related to app lifecycle and system services for apps. These features are available via a combination of WinRT and Win32 (C++ and COM) APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk).

### WinRT APIs

The following articles provide information about features available via WinRT APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Use app services and extensions](/windows/uwp/launch-resume/app-services) | Learn how to integrate app services into your Windows app to allow the sharing of data and functionality across apps. |
| [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks) | Learn how make lightweight code run in the background in response to triggers. |
| [Launch an app through file activation](/windows/uwp/launch-resume/launch-app-from-file) | Learn how to set up your app to launch when a file of a certain type is opened. |
| [Launch an app with a URI](/windows/uwp/launch-resume/launch-app-with-uri) | Learn how to use a Uniform Resource Identifier (URI) to launch one app from another app, enabling helpful app-to-app scenarios. |
| [Threading and async programming](/windows/uwp/threading-async/) | Learn how to use the thread pool to accomplish work asynchronously in parallel threads. |

### Win32 (C++ and COM) APIs

The following articles provide information about features available via Win32 (C++ and COM) APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [System services](/windows/desktop/system-services) | Learn about system services available to apps, including COM, Windows messaging, service applications, and much more. |
| [Memory management](/windows/desktop/memory/memory-management) | Learn how to use virtual memory, memory mapped files, copy-on-write memory, large memory support, and other memory related features in your app. |
| [Processes and threads](/windows/desktop/procthread/processes-and-threads) | Learn how to control processes, threads, jobs, and other units of code execution in your app. |
| [Windows system information](/windows/desktop/sysinfo/windows-system-information) | Learn how to access system information including the registry, handles and objects, and more. |

## .NET features

The .NET SDK also provides APIs related to system services for WPF and Windows Forms apps.

| Article | Description |
|---------|-------------|
| [Threading model (WPF)](/dotnet/framework/wpf/advanced/threading-model) | Learn about the threading model of WPF apps. |
| [System information](/dotnet/framework/winforms/advanced/system-information-and-windows-forms) | Learn how to access system information in Windows Forms apps. |
