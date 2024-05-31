---
title: Win32Interop.GetIconFromIconId(IconId) method
description: Gets the icon handle that corresponds to the specified *iconId*, if the provided *iconId* is valid and the system has an `HICON` that represents the icon.
ms.topic: article
ms.date: 02/08/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, winui, app sdk, C#, interop, Win32Interop.GetDisplayIdFromMonitor, GetDisplayIdFromMonitor
ms.author: stwhi
author: stevewhims
ms.localizationpriority: low
---

# Win32Interop.GetIconFromIconId(IconId) method

Reference

## Definition

Namespace: [Microsoft.UI](microsoft.ui.md)

Gets the icon handle that corresponds to the specified *iconId*, if the provided *iconId* is valid and the system has an `HICON` that represents the icon.

```csharp
public static IntPtr GetIconFromIconId(IconId iconId);
```

### Parameters

`iconId` [IconId](/windows/windows-app-sdk/api/winrt/microsoft.ui.iconid)

The identifier for the icon.

### Returns

[IntPtr](/dotnet/api/system.intptr)

The icon handle that corresponds to the specified *iconId*, if the provided *iconId* is valid and the system has an `HICON` that represents the icon. Otherwise, `null`.

## Applies to

| Product | Introduced in |
|-|-|
|**WinUI 3**|Windows App SDK 1.0|

## See also

* [Win32Interop class](microsoft.ui.win32interop.md)
* [Manage app windows](../../../windows-app-sdk/windowing/windowing-overview.md)
* [Call interop APIs from a .NET app](../../../desktop/modernize/winrt-com-interop-csharp.md)