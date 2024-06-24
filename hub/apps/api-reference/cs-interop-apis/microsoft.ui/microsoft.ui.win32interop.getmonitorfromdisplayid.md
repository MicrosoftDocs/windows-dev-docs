---
title: Win32Interop.GetMonitorFromDisplayId(DisplayId) method
description: Gets the display monitor handle that corresponds to the specified *displayId*, if the provided *displayId* is valid and the system has an `HMONITOR` that represents the display monitor.
ms.topic: article
ms.date: 02/08/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, winui, app sdk, C#, interop, Win32Interop.GetDisplayIdFromMonitor, GetDisplayIdFromMonitor
ms.author: stwhi
author: stevewhims
ms.localizationpriority: low
---

# Win32Interop.GetMonitorFromDisplayId(DisplayId) method

Reference

## Definition

Namespace: [Microsoft.UI](microsoft.ui.md)

Gets the display monitor handle that corresponds to the specified *displayId*, if the provided *displayId* is valid and the system has an `HMONITOR` that represents the display monitor.

```csharp
public static IntPtr GetMonitorFromDisplayId(DisplayId displayId);
```

### Parameters

`displayId` [DisplayId](/windows/windows-app-sdk/api/winrt/microsoft.ui.displayid)

The identifier for the display.

### Returns

[IntPtr](/dotnet/api/system.intptr)

The display monitor handle that corresponds to the specified *displayId*, if the provided *displayId* is valid and the system has an `HMONITOR` that represents the display monitor. Otherwise, `null`.

## Applies to

| Product | Introduced in |
|-|-|
|**WinUI 3**|Windows App SDK 1.0|

## See also

* [Win32Interop class](microsoft.ui.win32interop.md)
* [Manage app windows](../../../windows-app-sdk/windowing/windowing-overview.md)
* [Call interop APIs from a .NET app](../../../desktop/modernize/winrt-com-interop-csharp.md)