---
title: Manage memory usage in Windows App SDK desktop apps
description: Learn strategies for reducing memory usage in Windows App SDK desktop apps when your app moves to the background.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Manage memory usage in Windows App SDK desktop apps

Efficient memory management helps your app perform well, especially on devices with limited resources. This article describes strategies for managing memory in a Windows App SDK desktop app, particularly when your app window is not visible.

> [!IMPORTANT]
> Desktop apps are **not automatically suspended** by the system. They continue to consume memory while running. Proactive memory management improves overall system responsiveness and battery life.

## Monitor memory usage

Use the [Windows.System.MemoryManager](/uwp/api/windows.system.memorymanager) class to check your app's memory consumption and the system-wide memory state:

```csharp
ulong appMemoryUsage = Windows.System.MemoryManager.AppMemoryUsage;
ulong appMemoryLimit = Windows.System.MemoryManager.AppMemoryUsageLimit;
Windows.System.AppMemoryUsageLevel level = Windows.System.MemoryManager.AppMemoryUsageLevel;

System.Diagnostics.Debug.WriteLine(
    $"Memory: {appMemoryUsage / 1024 / 1024} MB used, " +
    $"limit: {appMemoryLimit / 1024 / 1024} MB, level: {level}");
```

You can also subscribe to [MemoryManager.AppMemoryUsageLimitChanging](/uwp/api/windows.system.memorymanager.appmemoryusagelimitchanging) and [MemoryManager.AppMemoryUsageIncreased](/uwp/api/windows.system.memorymanager.appmemoryusageincreased) to respond to memory pressure.

## Reduce memory when the window is hidden

Although the system does not suspend your desktop app, you can reduce memory consumption when your window is minimized or hidden. This makes more resources available for the foreground app and extends battery life.

### Detect when the window is hidden

`Microsoft.UI.Xaml.Window` doesn't expose a `VisibilityChanged` event. Instead, use the `Window.Activated` event to detect when your window loses focus, or use `AppWindow.Changed` to detect when it's minimized:

```csharp
var m_window = App_Window;

void ReduceMemoryUsage() { }
void RestoreResources() { }

m_window.Activated += (sender, args) =>
{
    if (args.WindowActivationState == WindowActivationState.Deactivated)
    {
        // Window lost focus — consider reducing memory usage
        ReduceMemoryUsage();
    }
    else
    {
        // Window is active — reload resources
        RestoreResources();
    }
};

// To specifically detect minimize, use AppWindow.Changed
var appWindow = m_window.AppWindow;
appWindow.Changed += (sender, args) =>
{
    if (args.DidPresenterChange &&
        sender.Presenter is OverlappedPresenter presenter &&
        presenter.State == OverlappedPresenterState.Minimized)
    {
        // Window is minimized — reduce memory usage
        ReduceMemoryUsage();
    }
};
```

### What to release

When your window is hidden, consider releasing:

- **Cached images and bitmaps** — Dispose large bitmaps that are only needed for display.
- **Render-target resources** — Release GPU resources used for rendering.
- **View-model data** — Clear collections or cached data that can be reloaded when the user returns.
- **Navigation history** — If your app has a navigation stack, clear cached pages.

```csharp
private System.Collections.Generic.List<object>? _imageCache;
private global::Microsoft.UI.Xaml.Window m_window = App_Window;

private void ReduceMemoryUsage()
{
    // Release cached images
    _imageCache?.Clear();

    // Clear navigation cache
    if (m_window.Content is Frame rootFrame)
    {
        rootFrame.CacheSize = 0;
    }

    // Hint the GC to collect if appropriate, without forcing a blocking collection.
    // Only call this in response to system memory pressure, not on every hide.
    GC.Collect(2, GCCollectionMode.Optimized, blocking: false);
}

private void RestoreResources()
{
    // Restore navigation cache
    if (m_window.Content is Frame rootFrame)
    {
        rootFrame.CacheSize = 10;
    }

    // Reload other resources as needed
}
```

## Handle system memory pressure

Register for the [MemoryManager.AppMemoryUsageIncreased](/uwp/api/windows.system.memorymanager.appmemoryusageincreased) event to detect when system memory pressure rises:

```csharp
void ReduceMemoryUsage() { }

Windows.System.MemoryManager.AppMemoryUsageIncreased += (sender, e) =>
{
    var level = Windows.System.MemoryManager.AppMemoryUsageLevel;

    if (level == Windows.System.AppMemoryUsageLevel.OverLimit ||
        level == Windows.System.AppMemoryUsageLevel.High)
    {
        ReduceMemoryUsage();
    }
};
```

> [!TIP]
> Even though desktop apps are not automatically terminated by the system to free memory (unlike UWP), reducing your footprint during memory pressure keeps the system responsive and avoids potential out-of-memory conditions.

## Best practices

- **Release what you can reload.** Only release resources that you can reconstruct later from disk, network, or computation.
- **Reduce gradually.** Start by releasing the largest items first (images, caches, data collections).
- **Test on low-memory devices.** Verify your memory management works on devices with 4 GB or less of RAM.
- **Avoid holding large objects in static fields.** Use lazy loading patterns instead.

## Related content

- [App lifecycle for Windows App SDK](app-lifecycle.md)
- [MemoryManager class](/uwp/api/windows.system.memorymanager)
- [Extended execution for desktop apps](extended-execution.md)
