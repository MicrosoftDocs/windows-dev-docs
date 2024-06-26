---
title: Mapping UWP features to the Windows App SDK
description: This topic compares major feature areas in the different forms in which they appear in UWP and in the Windows App SDK.
ms.topic: article
ms.date: 10/01/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, mapping, mappings, uwp
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Mapping UWP features to the Windows App SDK

This topic compares major feature areas in the different forms in which they appear in UWP and in the Windows App SDK. The content in this migration guide supports moving from UWP XAML to Windows App SDK XAML&mdash;moving to a different UI framework, such as Windows Presentation Foundation (WPF), is outside of the scope of this guidance.

| Feature | UWP | Windows App SDK (packaged apps) | Migration notes |
| - | - | - | - |
| Packaging | MSIX<br/>App has identity | MSIX<br/>App has identity | UWP apps migrating to the Windows App SDK should stay on MSIX to ensure trusted clean install and uninstall experience, as well as access to all APIs, including those that require identity. |
| Container | App container:<br/>- security = LowIL<br/>- file system access is brokered<br/>- no registry access | MSIX Container:<br/>- security = MediumIL<br/>- file system access same as user, AppData writes virtualized<br/>- HKCU registry writes virtualized | Moving to a higher integrity level with the Windows App SDK allows your app to have greater functionality. However, be aware of virtualization if you want to expand the capabilities of your migrated application to write to HKCU or AppData. |
| Activation and instancing | Package identity + CoreApplication activation, single-instanced by default | Package identity, Main/WinMain + Windows App SDK activation, multi-instanced by default | Ensure your application can handle multi-instance behavior, or use [**AppInstance**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance) to manage your instances. |
| Lifecycle-managed | Suspend/resume | Power/State notifications |  You can use Power/State change notifications to reduce system load. |
| Background tasks | InProc and OOP background tasks | Inproc COM and OOP background tasks | You can continue to use your OOP background tasks. If the app requires communication to your main process, then evaluate your IPC mechanism, as the OOP background task is running in LowIL, and your Windows App SDK main process is running in MediumIL.<br/><br/>Any inproc background tasks need to be migrated to COM background tasks&mdash;see [Create and register a winmain COM background task](/windows/uwp/launch-resume/create-and-register-a-winmain-background-task).<br/><br/>For C# OOP background tasks, see [Author Windows Runtime components with C#/WinRT](../../develop/platform/csharp-winrt/authoring.md) and the [Background task sample](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/BgTaskComponent). |
| Windowing | CoreWindow, AppWindow (preview) | HWND, AppWindow v2 | Windowing behavior has significantly changed in Windows App SDK. See [Windowing functionality migration](guides/windowing.md). |
| Messaging | CoreDispatcher and DispatcherQueue | DispatcherQueue, WndProc | [**DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) supports Win32 apps. For additional details on moving from CoreDispatcher to DispatcherQueue see [Threading functionality migration](guides/threading.md). |
| UI Platform| System XAML, WebView, DirectX, and others | WinUI 3, Webview2, DirectX, and others | For more info, see [WinUI migration](guides/winui3.md). |
| Text-rendering | DirectWrite | DWriteCore | Enables applications to access the latest DWrite features downlevel and receive new DWrite updates separate from the OS release schedule. For more info, see [DirectWrite to DWriteCore migration](guides/dwritecore.md). |
| Resources | MRT | MRTCore | For more info, see [MRT to MRTCore migration](guides/mrtcore.md). |
| .NET Runtime | .NET Native / C# 7 | .NET 6+/C# 9 | The Windows App SDK provides access to the modern .NET runtime, and access to new language features. However, .NET [ReadyToRun compilation](/dotnet/core/deploying/ready-to-run) is not the same as .NET Native, so you should evaluate performance tradeoffs. |
| 2D Graphics | Win2D | Win2D for WinUI 3 | We're currently working on a version of Win2D that works with the Windows App SDK, in progress. See the [documentation](https://microsoft.github.io/Win2D/WinUI3/html/Introduction.htm) for more information. |
| Windows Runtime components | Windows Runtime component project templates for UWP |-  C++: Use the **Windows Runtime Component (WinUI 3)** project template. <br> - C#: Use C#/WinRT to author Windows Runtime Components in a .NET Class Library. | We're currently working on support to [Author Windows Runtime Components using C#/WinRT](../../develop/platform/csharp-winrt/authoring.md) for use in the Windows App SDK and WinUI 3. |

## See Also

- [Windows App SDK and suppported Windows releases](../support.md)
