---
title: Graphics
description: This article provides an index of development features that are related to scenarios involving graphics in Windows apps.
ms.topic: article
ms.date: 05/25/2023
keywords: 
---

# Graphics

This article provides an index of development features that are related to scenarios involving graphics in Windows apps.

## Win2D

[Win2D](/windows/apps/develop/win2d/) is an easy-to-use Windows Runtime API for immediate mode 2D graphics rendering with GPU acceleration.

## Windows App SDK features

The [Windows App SDK](../windows-app-sdk/index.md) provides the following features related to graphics scenarios for Windows 10 and later OS releases.

| Feature | Description |
|---------|-------------|
| [Render text with DWriteCore](../windows-app-sdk/dwritecore.md) | Use the C++/COM APIs in the [DWriteCore headers](/windows/windows-app-sdk/api/win32/_dwritecore/) of the Windows App SDK to render text using a device-independent text layout system, high quality sub-pixel Microsoft ClearType text rendering, hardware-accelerated text, multi-format text, wide language support, and much more. |

## Windows OS features

Windows 10 and later OS releases provide a wide variety of APIs related to graphics scenarios for apps. These features are available via a combination of WinRT and Win32 (C++ and COM) APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk).

#### WinRT APIs

The following articles provide information about features available via WinRT APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Visual layer](../windows-app-sdk/composition.md) | Learn how to use the visual layer in UWP apps. The visual layer provides a high performance, retained-mode API for graphics, effects and animations, and is the foundation for all WinRT XAML-based UI in Windows apps. |
| [Using the Visual layer in desktop apps](../desktop/modernize/ui/visual-layer-in-desktop-apps.md) | Learn how to use the visual layer in WPF, Windows Forms, and C++ Win32 apps. |
| [XAML platform](/windows/uwp/xaml-platform/) | Learn the fundamentals of the XAML language and concepts for WinRT-based graphic components Windows apps. |

#### Win32 (C++ and COM) APIs

The following articles provide information about features available via Win32 (C++ and COM) APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Graphics and gaming](/windows/desktop/graphics-and-multimedia) | Learn about the breadth of Win32 APIs related to graphics, gaming, and imaging. |
| [DirectX](/windows/desktop/getting-started-with-directx-graphics) | DirectX graphics provides a set of APIs that you can use to create games and other high-performance multimedia apps. |
| [Direct2D](/windows/desktop/direct2d/direct2d-portal) | Direct2D is a hardware-accelerated, immediate-mode, 2-D graphics API that provides high performance and high-quality rendering for 2-D geometry, bitmaps, and text. |
| [Direct3D](/windows/desktop/direct3d) | Direct3D enables you to create 3-D graphics for games and scientific apps. |
| [DirectWrite](/windows/win32/directwrite/direct-write-portal) | DirectWrite supports high-quality text rendering, resolution-independent outline fonts, and full Unicode text and layouts. |
| [Windows GDI](/windows/desktop/gdi/windows-gdi) | The graphics device interface (GDI) is a foundational API that enables apps to use graphics and formatted text on both the video display and the printer. |

## .NET features

The .NET SDK also provides APIs related to graphics scenarios for WPF and Windows Forms apps.

| Article | Description |
|---------|-------------|
| [Graphics (WPF)](/dotnet/framework/wpf/graphics-multimedia/graphics) | Learn about ways to integrate support for multimedia, vector graphics, animation, and content composition in WPF apps. |
| [Graphics and drawing (Windows Forms)](/dotnet/framework/winforms/advanced/graphics-and-drawing-in-windows-forms) | Learn about ways to create graphics, draw text, and manipulate graphical images in Windows Forms apps. |
