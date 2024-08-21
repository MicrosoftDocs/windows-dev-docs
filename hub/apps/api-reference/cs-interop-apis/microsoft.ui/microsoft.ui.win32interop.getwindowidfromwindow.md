---
title: Win32Interop.GetWindowIdFromWindow(IntPtr) method
description: Gets the `WindowId` that corresponds to the specified _hwnd_, if the provided `HWND` is a valid.
ms.topic: article
ms.date: 02/08/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, winui, app sdk, C#, interop, Win32Interop.GetDisplayIdFromMonitor, GetDisplayIdFromMonitor
ms.localizationpriority: low
---

# Win32Interop.GetWindowIdFromWindow(IntPtr) method

Reference

## Definition

Namespace: [Microsoft.UI](microsoft.ui.md)

Gets the `WindowId` that corresponds to the specified _hwnd_, if the provided `HWND` is a valid.

```csharp
public static WindowId GetWindowIdFromWindow(IntPtr hwnd);
```

### Parameters

`hwnd` [IntPtr](/dotnet/api/system.intptr)

The handle of the window for which to get the `WindowId`.

### Returns

[WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid)

The identifier that corresponds to the specified *hwnd*, if the provided *hwnd* is valid. Otherwise, `null`.

## Applies to

| Product | Introduced in |
|-|-|
|**WinUI 3**|Windows App SDK 1.0|

## See also

* [Win32Interop class](microsoft.ui.win32interop.md)
* [Manage app windows](../../../windows-app-sdk/windowing/windowing-overview.md)
* [Call interop APIs from a .NET app](../../../desktop/modernize/winrt-com-interop-csharp.md)