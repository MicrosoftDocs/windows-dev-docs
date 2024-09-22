---
title: Win32Interop.GetWindowFromWindowId(WindowId) method
description: Gets the window handle that corresponds to the specified *windowId*, if the provided *windowId* is valid and the system has an `HWND` that represents the window.
ms.topic: article
ms.date: 02/08/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, winui, app sdk, C#, interop, Win32Interop.GetDisplayIdFromMonitor, GetDisplayIdFromMonitor
ms.localizationpriority: low
---

# Win32Interop.GetWindowFromWindowId(WindowId) method

Reference

## Definition

Namespace: [Microsoft.UI](microsoft.ui.md)

Gets the window handle that corresponds to the specified *windowId*, if the provided *windowId* is valid and the system has an `HWND` that represents the window.

```csharp
public static IntPtr GetWindowFromWindowId(WindowId windowId);
```

### Parameters

`windowId` [WindowId](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowid)

The identifier for the window.

### Returns

[IntPtr](/dotnet/api/system.intptr)

The window handle that corresponds to the specified *windowId*, if the provided *windowId* is valid and the system has an `HWND` that represents the window. Otherwise, `null`.

## Applies to

| Product | Introduced in |
|-|-|
|**WinUI 3**|Windows App SDK 1.0|

## See also

* [Win32Interop class](microsoft.ui.win32interop.md)
* [Manage app windows](../../../windows-app-sdk/windowing/windowing-overview.md)
* [Call interop APIs from a .NET app](../../../desktop/modernize/winrt-com-interop-csharp.md)