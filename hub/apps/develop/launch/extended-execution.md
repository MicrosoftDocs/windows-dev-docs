---
title: Use extended execution in Windows App SDK desktop apps
description: Learn how to use extended execution sessions in Windows App SDK desktop apps for battery-aware background work.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Use extended execution in Windows App SDK desktop apps

Extended execution sessions let your app request that the system keep it running for specific scenarios, even when it would otherwise be a candidate for resource reduction on battery-powered devices. While desktop apps are not automatically suspended, extended execution is still useful for signaling intent to the system.

> [!IMPORTANT]
> Desktop apps continue running whether or not they use extended execution. The primary benefit of `ExtendedExecutionSession` in a desktop app is communicating to the system that your work is important and should not be interrupted by power-saving measures such as Modern Standby or battery saver.

## When to use extended execution

Extended execution sessions are most useful in desktop apps for:

- **Saving critical data** — Ensuring that a large save operation completes before the device enters standby.
- **Location tracking** — Keeping GPS data flowing while the app is minimized on a laptop.
- **Long-running computations** — Completing a render, compilation, or data analysis task.

For scenarios where your desktop app simply needs to keep running (for example, a chat app or music player), you do **not** need extended execution — the app continues running normally.

## Create an extended execution session

Use the [ExtendedExecutionSession](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionsession) class:

```csharp
using Windows.ApplicationModel.ExtendedExecution;

private ExtendedExecutionSession? _session;

private async Task StartExtendedSessionAsync()
{
    var session = new ExtendedExecutionSession
    {
        Reason = ExtendedExecutionReason.Unspecified,
        Description = "Completing critical data save"
    };

    session.Revoked += ExtendedSession_Revoked;

    ExtendedExecutionResult result = await session.RequestExtensionAsync();

    if (result == ExtendedExecutionResult.Allowed)
    {
        _session = session;
        // Proceed with the long-running work
    }
    else
    {
        session.Dispose();
        // The system denied the request — handle gracefully
    }
}

private void ExtendedSession_Revoked(object sender,
    ExtendedExecutionRevokedEventArgs args)
{
    // The session was revoked. Save state quickly.
    if (args.Reason == ExtendedExecutionRevokedReason.SystemPolicy)
    {
        // System needs resources — wrap up immediately
    }
}
```

## Extended execution reasons

| Reason | Use case |
|--------|----------|
| `SavingData` | Your app needs to complete a save operation |
| `LocationTracking` | Your app is tracking the user's location |
| `Unspecified` | Other long-running work |

> [!NOTE]
> `LocationTracking` requires the `location` capability in your app manifest and user consent.

## End the session

When your work completes, dispose the session to release the request:

```csharp
private ExtendedExecutionSession? _session;

private void ExtendedSession_Revoked(object sender,
    ExtendedExecutionRevokedEventArgs args)
{
}

private void StopExtendedSession()
{
    if (_session != null)
    {
        _session.Revoked -= ExtendedSession_Revoked;
        _session.Dispose();
        _session = null;
    }
}
```

## Differences from UWP

| Behavior | UWP | Desktop |
|----------|-----|---------|
| Purpose | Prevents suspension | Signals importance to power management |
| App keeps running without it | No — UWP apps suspend | Yes — desktop apps run normally |
| Session revocation | Can cause suspension | Does not stop the app, but power savings may apply |
| `SavingData` reason | Prevents suspend during save | Same API, less critical since app doesn't suspend |

## Best practices

- Only request extended execution when you have specific work that should not be interrupted by power-saving.
- Always handle the `Revoked` event, even in desktop apps.
- Dispose the session as soon as your work completes.
- Do not hold an extended session indefinitely — it can impact device battery life.

## Related content

- [App lifecycle for Windows App SDK](app-lifecycle.md)
- [Background execution in desktop apps](background-execution.md)
- [ExtendedExecutionSession class](/uwp/api/windows.applicationmodel.extendedexecution.extendedexecutionsession)
