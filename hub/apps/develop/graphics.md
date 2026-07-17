---
title: Graphics
description: This article provides an index of development features that are related to scenarios involving graphics in Windows apps.
ms.topic: article
ms.date: 07/15/2026
author: GrantMeStrength
ms.author: jken
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
| [Visual layer](composition/visual-layer.md) | Learn how to use the Visual layer in WinUI and Windows App SDK apps. The Visual layer provides a high performance, retained-mode API for graphics, effects and animations, and is the foundation for all WinRT XAML-based UI in Windows apps. |
| [Using the Visual layer in desktop apps](/windows/uwp/composition/visual-layer-in-desktop-apps) | Learn how to use the visual layer in WPF, Windows Forms, and C++ Win32 apps. |
| [Screen capture](media-authoring-processing/screen-capture.md) | Use the Windows.Graphics.Capture APIs to acquire frames from a display or application window, to create video streams or snapshots for collaborative and interactive experiences. |
| [XAML platform](/windows/apps/develop/platform/xaml/) | Learn the fundamentals of the XAML language and concepts for WinRT-based graphic components Windows apps. |

#### Win32 (C++ and COM) APIs

The following articles provide information about features available via Win32 (C++ and COM) APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Graphics and gaming](/windows/desktop/graphics-and-multimedia) | Learn about the breadth of Win32 APIs related to graphics, gaming, and imaging. |
| [DirectX](/windows/desktop/getting-started-with-directx-graphics) | DirectX graphics provides a set of APIs that you can use to create games and other high-performance multimedia apps. |
| [Direct3D 12](/windows/desktop/direct3d12/direct3d-12-graphics) | Direct3D 12 is the current generation graphics API for high-performance game and real-time 3-D rendering. It provides lower-level hardware access and reduced driver overhead compared to Direct3D 11. |
| [Direct3D 11](/windows/desktop/direct3d11/atoc-dx-graphics-direct3d-11) | Direct3D 11 is the previous generation 3-D graphics API. Consider Direct3D 12 for new projects requiring maximum GPU performance. |
| [Direct2D](/windows/desktop/direct2d/direct2d-portal) | Direct2D is a hardware-accelerated, immediate-mode, 2-D graphics API that provides high performance and high-quality rendering for 2-D geometry, bitmaps, and text. |
| [DirectWrite](/windows/win32/directwrite/direct-write-portal) | DirectWrite supports high-quality text rendering, resolution-independent outline fonts, and full Unicode text and layouts. |
| [DirectXMath](/windows/win32/dxmath/directxmath-portal) | DirectXMath provides an optimal math library for types and functions common to graphics programs that require high performance on Windows. |
| [DXGI](/windows/win32/direct3ddxgi/dx-graphics-dxgi) | Microsoft DirectX Graphics Infrastructure (DXGI) handles enumerating display adapters and monitors, managing swap chains for rendering, and associating drawing surfaces with windows. |
| [Windows Imaging Component (WIC)](/windows/win32/wic/-wic-about-windows-imaging-codec) | WIC provides an extensible framework for working with images and image metadata. It supports the BMP, GIF, ICO, JPEG, PNG, TIFF, and DDS formats. |
| [Windows GDI](/windows/desktop/gdi/windows-gdi) | The graphics device interface (GDI) is a foundational API that enables apps to use graphics and formatted text on both the video display and the printer. |

## .NET features

The .NET SDK also provides APIs related to graphics scenarios for WPF and Windows Forms apps.

| Article | Description |
|---------|-------------|
| [Graphics (WPF)](/dotnet/framework/wpf/graphics-multimedia/graphics) | Learn about ways to integrate support for multimedia, vector graphics, animation, and content composition in WPF apps. |
| [Graphics and drawing (Windows Forms)](/dotnet/framework/winforms/advanced/graphics-and-drawing-in-windows-forms) | Learn about ways to create graphics, draw text, and manipulate graphical images in Windows Forms apps. |

## Game development

For full-featured game development on Windows, use the [Microsoft Game Development Kit (GDK)](https://github.com/microsoft/GDK), which provides APIs for Xbox Live services, game packaging, and deployment to both PC and Xbox.

For game-specific graphics programming, Direct3D 12 is the recommended API. It provides explicit control over GPU resources, multi-threaded command submission, and the lowest-overhead access to modern GPU hardware.

## See also

- [DirectX Graphics Samples on GitHub](https://github.com/microsoft/DirectX-Graphics-Samples) — Official Direct3D 12 and DirectX sample code
- [DWriteCore (Windows App SDK)](../windows-app-sdk/dwritecore.md) — The Windows App SDK implementation of DirectWrite
- [Screen capture](media-authoring-processing/screen-capture.md) — Capture frames from a display or application window using Windows.Graphics.Capture
- [DirectX tool kit for Direct3D 12](https://github.com/microsoft/DirectXTK12) — Helper library for Direct3D 12 game development
- [PIX on Windows](/windows/desktop/direct3dtools/pix) — Performance tuning and debugging tool for DirectX 12
- [XInput (game controller input)](/windows/desktop/xinput/getting-started-with-xinput) — API for Xbox controller input in Windows games
- [ONNX Runtime](/windows/ai/new-windows-ml/run-onnx-models) — Run machine-learning models on-device for game AI and real-time inference
