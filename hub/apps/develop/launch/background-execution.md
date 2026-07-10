---
title: Background execution in Windows App SDK desktop apps
description: Learn how Windows App SDK desktop apps handle background execution and best practices for efficient long-running work.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Background execution in Windows App SDK desktop apps

Desktop apps built with the Windows App SDK run continuously, even when the user switches to another app. This article explains how background execution works and recommends best practices for efficiency.

> [!IMPORTANT]
> Desktop apps are **not automatically suspended or terminated by the system** the way UWP apps are. Your app continues executing until the user closes it or the process exits. No special APIs or restricted capabilities are required to run indefinitely. Use system memory notifications to respond to resource pressure when appropriate.

## Desktop apps run in the background natively

In UWP, running indefinitely in the background required the `extendedBackgroundTaskTime` restricted capability and enterprise deployment. Desktop apps do not have this restriction. Your app's threads continue executing, timers keep firing, and network connections remain open regardless of whether your window is visible.

This means you can:

- Maintain persistent connections to servers (for example, chat apps, real-time monitoring).
- Run long computations (for example, video encoding, data analysis).
- Poll for updates on a schedule.
- Play audio and update media controls.

## Best practices for battery efficiency

Although desktop apps can run without restriction, being a good citizen on battery-powered devices improves the user experience. The system may flag your app as a heavy resource consumer if it uses excessive CPU or network in the background.

### Reduce work when the window is not visible

```csharp
// m_window is your app's main Window; _updateTimer is a DispatcherTimer field.
Microsoft.UI.Xaml.Window m_window = this;
Microsoft.UI.Xaml.DispatcherTimer _updateTimer = new();

m_window.Activated += (sender, args) =>
{
    if (args.WindowActivationState == WindowActivationState.Deactivated)
    {
        // Window lost focus — slow down background processing
        _updateTimer.Interval = TimeSpan.FromSeconds(30);
    }
    else
    {
        // Window is active — resume full-speed processing
        _updateTimer.Interval = TimeSpan.FromSeconds(1);
    }
};
```

### Use power-aware patterns

- **Reduce timer frequency** — Switch from sub-second to multi-second intervals when the app is in the background.
- **Pause non-essential rendering** — Stop animations, GPU work, and screen updates.
- **Batch network requests** — Combine multiple requests into fewer, larger ones.
- **Respect battery saver** — Check [PowerManager.BatteryStatus](/uwp/api/windows.system.power.powermanager.batterystatus) and reduce work when the device is on battery.

```csharp
using Windows.System.Power;

private bool _enableBackgroundSync = true;

private void CheckPowerState()
{
    if (PowerManager.BatteryStatus == BatteryStatus.Discharging &&
        PowerManager.RemainingChargePercent < 20)
    {
        // Reduce background work significantly
        _enableBackgroundSync = false;
    }
}
```

### Use background tasks for event-driven work

If your app needs to respond to system events (such as a timer, push notification, or network change) without running continuously, consider using a background task instead:

```csharp
var builder = new BackgroundTaskBuilder
{
    Name = "PeriodicSync",
    TaskEntryPoint = "MyApp.Background.SyncTask"
};
builder.SetTrigger(new TimeTrigger(15, false));
builder.Register();
```

> [!NOTE]
> Background tasks require MSIX package identity for registration. If your app is unpackaged, use in-process timers or scheduled tasks instead.

## Extended execution for critical work

When you have work that should not be interrupted by power management (for example, saving a large file), use an [extended execution session](extended-execution.md). This signals to the system that your work is important, even on battery-powered devices.

## Comparison with UWP background execution

| Feature | UWP | Desktop |
|---------|-----|---------|
| App runs in background | Only with background tasks or restricted capabilities | Always |
| Requires capability declaration | Yes (`extendedBackgroundTaskTime`) | No |
| System terminates to save resources | Yes | No |
| Battery saver throttling | System enforces restrictions | App should self-regulate |
| Background tasks for event-driven work | Primary mechanism | Optional complement |

## Related content

- [App lifecycle for Windows App SDK](app-lifecycle.md)
- [Extended execution for desktop apps](extended-execution.md)
- [Create and register a background task](/windows/apps/develop/launch/create-and-register-a-background-task)
- [Manage memory usage](reduce-memory-usage.md)
