---
title: Optimize background activity
description: Learn how to manage background activity in your Windows App SDK application for better battery life and system efficiency.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Optimize background activity

Background tasks let your app run code when the app is not in the foreground. In a desktop Windows App SDK application, background activity has more flexibility than in UWP apps, but you should still manage it responsibly to ensure a good experience for your users.

> [!IMPORTANT]
> Desktop Windows App SDK apps are not subject to the same background execution restrictions as UWP apps. Desktop apps can run background threads freely, maintain long-running tasks, and use standard .NET threading patterns. However, being a good citizen of the system means minimizing unnecessary work when the user is not actively using your app.

## Best practices for background activity

Follow these guidelines to keep your app efficient:

### Reduce work when the user is not interacting with your app

When the user switches to a different app or minimizes your window, reduce the frequency of timers, network polling, and other background work. This conserves battery and frees system resources for the app the user is actively working with.

### Use efficient scheduling patterns

Use timer-based scheduling rather than tight polling loops. For periodic work, use `DispatcherTimer` or `System.Threading.Timer` with intervals appropriate to your scenario.

```csharp
private DispatcherTimer _updateTimer;

private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
{
    if (args.WindowActivationState == WindowActivationState.Deactivated)
    {
        // Window is no longer active — slow down updates.
        _updateTimer.Interval = TimeSpan.FromSeconds(30);
    }
    else
    {
        // Window is active — use normal update rate.
        _updateTimer.Interval = TimeSpan.FromSeconds(2);
    }
}
```

### Respond to power and battery states

Check the system's power status and adjust your app's behavior accordingly. Reduce background activity when the device runs on battery and consider deferring non-essential work until the device is plugged in.

```csharp
// Check power status using Windows.System.Power
var batteryStatus = Windows.System.Power.PowerManager.BatteryStatus;
var energySaverStatus = Windows.System.Power.PowerManager.EnergySaverStatus;

if (energySaverStatus == Windows.System.Power.EnergySaverStatus.On)
{
    // Reduce background work when Battery Saver is active.
    DisableNonEssentialUpdates();
}

void DisableNonEssentialUpdates()
{
    // Pause non-essential timers, syncs, and animations here.
}
```

### Cancel background work when the app closes

When your app shuts down, cancel any remaining background tasks using `CancellationToken` patterns. This prevents your app from holding system resources after it is no longer visible.

```csharp
private CancellationTokenSource _cts = new();

private async Task DoBackgroundWorkAsync()
{
    while (!_cts.Token.IsCancellationRequested)
    {
        await Task.Delay(TimeSpan.FromSeconds(10), _cts.Token);
        // Perform periodic work.
    }
}

private void MainWindow_Closed(object sender, WindowEventArgs args)
{
    _cts.Cancel();
}
```

## Use background tasks with package identity

If your app is packaged with MSIX, you can also register background tasks that run in response to system triggers (such as network state changes, time zones, or user presence). These tasks use the same `BackgroundTaskBuilder` API available in UWP. For more information, see [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks).

> [!NOTE]
> Packaged apps (MSIX) can use `BackgroundTaskBuilder` for system-triggered work. Unpackaged desktop apps should use standard .NET threading patterns instead.

## Related content

- [Keep the UI thread responsive](keep-ui-thread-responsive.md)
- [Plan and measure performance](planning-measuring-performance.md)
