---
title: IWindowNative interface
description: WinUI COM interface that provides interoperation between XAML and a native window. 
ms.topic: reference
ms.date: 03/09/2021
keywords: winui, Windows UI Library
---

# IWindowNative interface (microsoft.ui.xaml.window.h)

Enables interoperation between XAML and a native window.

This interface is implemented by [Window](/windows/winui/api/microsoft.ui.xaml.window), which desktop apps can use to get the underlying HWND of the window.

## Inheritance

The IWindowNative interface inherits from the IUnknown interface. IWindowNative also has these types of members:

- [Properties](#properties)

## Properties

The IWindowNative interface has these properties.

| Property | Description |
| --- | --- |
| [IWindowNative::WindowHandle](iwindownative-windowhandle.md) | Gets the requested window HWND. |

## Applies to

| Product | Versions |
| --- | --- |
| WinUI | 3.0.0-project-reunion-0.5, 3.0.0-project-reunion-preview-0.5 |

## See also

[Windows UI Library (WinUI)](../index.md)