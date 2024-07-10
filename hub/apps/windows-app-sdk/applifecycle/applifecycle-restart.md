---
title: Restart API (Windows App SDK)
description: Describes how to use the Restart API, AppRestartFailureReason Restart(), with the App Lifecycle API (Windows App SDK).
ms.topic: article
ms.date: 05/19/2022
ms.author: mousma
author: mousma
---

# Restart API

The restart API enables any app, including packaged or unpackaged Win32 apps, to terminate and restart themselves on command, including the ability to provide an arbitrary command-line string for the restarted instance.

## Definition
```public static AppRestartFailureReason Restart(String arguments)```

## Parameters
`arguments`: [**String**](/dotnet/api/system.string)

The arguments to pass to the restarted instance.

## Returns

The Restart API returns an [`AppRestartFailureReason`](/uwp/api/windows.applicationmodel.core.apprestartfailurereason).

## Prerequisites

To use the app lifecycle API in the Windows App SDK:

1. Download and install the latest release of the Windows App SDK. For more information, see [Get started with WinUI](../../get-started/start-here.md).
2. Follow the instructions to [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

## What is this Restart Method?

For Win32 apps, the following exists as possible restart mechanisms:
- The Win32 API [RegisterApplicationRestart](/windows/win32/api/winbase/nf-winbase-registerapplicationrestart) enables an app to register itself to be restarted after termination, and to provide an arbitrary command-line string for the restarted instance. The reasons for termination include app crash or hang, app update, or system update. 

However, a gap existed for the following scenario:
- Win32 apps can register with the OS to restart in specific app/OS states, but **cannot initiate a restart from a healthy state**

This Restart API enables Win32 applications to terminate and restart on command, and aligns with CoreApplication's existing [CoreApplication.RequestRestartAsync](/uwp/api/windows.applicationmodel.core.coreapplication.requestrestartasync).

## Restarting With Command Line Arguments

Simply call the Restart method and specify an arbitrary command-line string for the restarted instance to restart with. The restart is completedly synchronously and no further action or handling is required. If the restart fails for some reason, the Restart method returns a failure reason.

## Examples

```csharp
private void restartAfterUpdate()
{
    AppRestartFailureReason restartError = AppInstance.Restart(restartArgsInput);

    switch (restartError)
    {
        case AppRestartFailureReason.RestartPending:
            SendToast("Another restart is currently pending.");
            break;
        case AppRestartFailureReason.InvalidUser:
            SendToast("Current user is not signed in or not a valid user.");
            break;
        case AppRestartFailureReason.Other:
            SendToast("Failure restarting.");
            break;
    }
}
```

To see Restart samples, visit the [WindowsAppSDK-Samples repository](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/AppLifecycle/Restart).
