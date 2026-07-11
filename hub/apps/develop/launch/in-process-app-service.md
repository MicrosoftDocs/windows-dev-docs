---
title: In-process app services aren't supported in Windows App SDK
description: Learn why Windows App SDK doesn't support running an app service in-process, and what to use instead.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/10/2026
---

# In-process app services aren't supported in Windows App SDK

In UWP, you could host an app service in the same process as your app by overriding `OnBackgroundActivated` on your `Application` class. Windows App SDK doesn't support this pattern.

## Why in-process app services aren't available

`Microsoft.UI.Xaml.Application` doesn't define an `OnBackgroundActivated` override, and [ExtendedActivationKind](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.extendedactivationkind) — the enum Windows App SDK apps use to inspect how they were activated — has no `AppService` member. Because of this, there's no supported way for a Windows App SDK app's own process to receive app-service activation. App services on Windows App SDK must always run as an out-of-process background task.

## What to use instead

- **To provide an app service**, implement it out-of-process. Create a separate Windows Runtime Component project that implements `IBackgroundTask`, and reference it as the `EntryPoint` in your manifest's `appService` extension. This pattern works for both UWP and Windows App SDK provider apps. For steps, see [Create and consume an app service](app-services.md).
- **If you only need to share data or call functionality within the same app**, you don't need an app service or `AppServiceConnection` at all. Since the code runs in the same process, call the shared class or method directly instead of routing the call through inter-process communication.

## Related content

- [Create and consume an app service](app-services.md)
- [App extensions](app-extensions.md)
- [Extensibility overview](extensibility-overview.md)
