---
title: Win32Interop.GetDisplayIdFromMonitor(IntPtr) method
description: Gets the `DisplayId` that corresponds to the specified *hmonitor*, if the provided `hmonitor` is valid.
ms.topic: article
ms.date: 02/08/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, winui, app sdk, C#, interop, Win32Interop.GetDisplayIdFromMonitor, GetDisplayIdFromMonitor
ms.localizationpriority: low
---

# Win32Interop.GetDisplayIdFromMonitor(IntPtr) method

Reference

## Definition

Namespace: [Microsoft.UI](microsoft.ui.md)

Gets the `DisplayId` that corresponds to the specified *hmonitor*, if the provided `hmonitor` is valid.

```csharp
public static DisplayId GetDisplayIdFromMonitor(IntPtr hmonitor);
```

### Parameters

`hmonitor` [IntPtr](/dotnet/api/system.intptr)

The handle of the display monitor for which to get the `DisplayId`.

### Returns

[DisplayId](/windows/windows-app-sdk/api/winrt/microsoft.ui.displayid)

The display monitor identifier that corresponds to the specified *hmonitor*, if the provided *hmonitor* is valid. Otherwise, `null`.

## Applies to

| Product | Introduced in |
|-|-|
|**WinUI 3**|Windows App SDK 1.0|

## See also

* [Win32Interop class](microsoft.ui.win32interop.md)
* [Manage app windows](../../../windows-app-sdk/windowing/windowing-overview.md)
* [Call interop APIs from a .NET app](../../../desktop/modernize/winrt-com-interop-csharp.md)