---
title: Win32Interop.GetIconIdFromIcon(IntPtr) method
description: Gets the `IconId` that corresponds to the specified *hicon*, if the provided `hicon` is valid.
ms.topic: article
ms.date: 02/08/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, winui, app sdk, C#, interop, Win32Interop.GetDisplayIdFromMonitor, GetDisplayIdFromMonitor
ms.localizationpriority: low
---

# Win32Interop.GetIconIdFromIcon(IntPtr) method

Reference

## Definition

Namespace: [Microsoft.UI](microsoft.ui.md)

Gets the `IconId` that corresponds to the specified *hicon*, if the provided `hicon` is valid.

```csharp
public static IconId GetIconIdFromIcon(IntPtr hicon);
```

### Parameters

`hicon` [IntPtr](/dotnet/api/system.intptr)

The handle of the icon for which to get the `IconId`.

### Returns

[IconId](/windows/windows-app-sdk/api/winrt/microsoft.ui.iconid)

The icon identifier that corresponds to the specified *hicon*, if the provided *hicon* is valid. Otherwise, `null`.

## Applies to

| Product | Introduced in |
|-|-|
|**WinUI 3**|Windows App SDK 1.0|

## See also

* [Win32Interop class](microsoft.ui.win32interop.md)
* [Manage app windows](../../../windows-app-sdk/windowing/windowing-overview.md)
* [Call interop APIs from a .NET app](../../../desktop/modernize/winrt-com-interop-csharp.md)