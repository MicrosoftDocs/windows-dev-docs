---
title: Windows App SDK app lifecycle
description: This article provides an overview of managing the lifecycle of Windows App SDK apps.
ms.topic: article
ms.date: 01/26/2023
keywords: windows, desktop development, Windows App SDK, .net, windows 10, windows 11, winui, app lifecycle
ms.localizationpriority: medium
---

# Windows App SDK app lifecycle

This article provides an overview of managing the lifecycle of **Windows App SDK** desktop apps.

## Microsoft.UI.Xaml.Application lifecycle

The [Application](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application) object is the main entry point for a Windows App SDK app. It's similar to the UWP [Application](/uwp/api/windows.ui.xaml.application) class, but with some important differences. The `Application` object is created by the Windows App SDK framework and is accessible from the `Microsoft.UI.Xaml.Application.Current` property.

The `Application` class in Windows App SDK has only one lifecycle method, [OnLaunched](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.onlaunched), which is called when the app is launched. The `OnLaunched` method is responsible for creating the app's main window and displaying it. The `OnLaunched` method is also responsible for initializing the Windows App SDK framework and starting the app. When you create a new Windows App SDK app, the `OnLaunched` method is automatically generated for you.

In contrast, the UWP `Application` class has three activation-related lifecycle methods: [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched), [OnActivated](/uwp/api/windows.ui.xaml.application.onactivated), and [OnBackgroundActivated](/uwp/api/windows.ui.xaml.application.onbackgroundactivated). The `OnActivated` and `OnBackgroundActivated` methods are called when the app is activated. The `OnActivated` method is called when the app is activated by the user, and the `OnBackgroundActivated` method is called when the app is activated by the system.

UWP's `Application` class also several lifecycle events: [Suspending](/uwp/api/windows.ui.xaml.application.suspending), [Resuming](/uwp/api/windows.ui.xaml.application.resuming), [EnteredBackground](/uwp/api/windows.ui.xaml.application.enteredbackground), and [LeavingBackground](/uwp/api/windows.ui.xaml.application.leavingbackground). The `Suspending` event is raised when the app is suspended, and the `Resuming` event is raised when the app is resumed. The `EnteredBackground` event is raised when the app enters the background, and the `LeavingBackground` event is raised when the app leaves the background. For a full explanation of UWP lifecycle events, see [Windows 10 UWP app lifecycle](/windows/uwp/launch-resume/app-lifecycle).

If you are migrating a UWP app to Windows App SDK, you can use the [Application lifecycle functionality migration](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/applifecycle) guide to understand the differences between the UWP and Windows App SDK app lifecycles.

## Microsoft.UI.Xaml.Window lifecycle

## See also

[App lifecycle and system services](/windows/apps/develop/app-lifecycle-and-system-services)

[UWP app lifecycle](/windows/uwp/launch-resume/app-lifecycle)
