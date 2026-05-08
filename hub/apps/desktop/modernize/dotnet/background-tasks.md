---
title: Run code in the background in a .NET app
description: This topic provides guidance for running background tasks in a .NET app that use the Windows App SDK.
ms.topic: how-to
ms.date: 05/07/2026
keywords: windows win32, windows app development, Windows App SDK, Windows Forms, WinForms
ms.localizationpriority: medium
---

# Run code in the background

For .NET apps that need to execute code when the app isn't running, there are three approaches depending on your packaging and workload:

| Approach | Packaging required | Best for |
|---|---|---|
| [Windows App SDK background tasks](../../../windows-app-sdk/applifecycle/background-tasks.md) | Yes (MSIX) | Power-efficient system-managed triggers (time/system) |
| [Task Scheduler](/windows/win32/taskschd/task-scheduler-start-page) | No | Periodic sync, unpackaged apps |
| [.NET Worker Services](/dotnet/core/extensions/workers) | No | Long-running headless workloads, any deployment model |

For Windows App SDK background tasks, your .NET app registers a COM component using `BackgroundTaskBuilder` just like a WinUI 3 app — the `Application.Startup` event in WPF maps to the role that `App.OnLaunched` plays in WinUI 3. See [Using background tasks in Windows apps](../../../windows-app-sdk/applifecycle/background-tasks.md) for the full walkthrough.

> [!NOTE]
> Windows App SDK background tasks require MSIX packaging. For unpackaged .NET apps, use [Task Scheduler](/windows/win32/taskschd/task-scheduler-start-page) or [.NET Worker Services](/dotnet/core/extensions/workers) instead.

## Related topics

- [Windows App SDK background tasks](../../../windows-app-sdk/applifecycle/background-tasks.md)
- [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
- [Windows Forms (WinForms)](/dotnet/desktop/winforms/)
- [Call Windows Runtime APIs](../../../desktop/modernize/winrt-apis-desktop-apps.md)
- [Packaging overview](../../../package-and-deploy/packaging/index.md)
- [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps)
