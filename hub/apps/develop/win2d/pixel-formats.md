---
title: Pixel formats
description: An explanation of the various pixels formats used by Direct3D and DXGI, and how to use them through Win2D.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Pixel formats

The [`DirectXPixelFormat`(https://msdn.microsoft.com/library/windows/apps/windows.graphics.directx.directxpixelformat.aspx) enum includes all the many and varied pixel formats used by Direct3D and DXGI, but only a few of these options are supported by Win2D (or by Direct2D upon which Win2D is built).

If in doubt, pixel format `B8G8R8A8UIntNormalized` and [`CanvasAlphaMode.Premultiplied`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasAlphaMode.htm) are good defaults for most purposes.

All the formats listed below are supported by Direct3D feature level 11 GPU hardware (used in desktop computers and most tablets). Feature level 9 GPUs (which are found in phones) only support a subset.

> [!NOTE]
> If you want to use one of the formats marked as "Not supported on all devices", you should first check [`IsPixelFormatSupported(DirectXPixelFormat)`](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasDevice_IsPixelFormatSupported.htm), or catch exceptions if the resource creation fails and be prepared to fall back on one of the universally available options.

## CanvasBitmap formats

| `DirectXPixelFormat` | Compatible [`CanvasAlphaMode`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasAlphaMode.htm)-s | Notes
| -- | -- | -- |
| `B8G8R8A8UIntNormalized` | `Premultiplied`, `Ignore` | The default format |
| `B8G8R8A8UIntNormalizedSrgb` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `B8G8R8X8UIntNormalized` | `Ignore` | |
| `R8G8B8A8UIntNormalized` | `Premultiplied`, `Ignore` | |
| `R8G8B8A8UIntNormalizedSrgb` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R16G16B16A16Float` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R32G32B32A32Float	` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R10G10B10A2UIntNormalized` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R16G16B16A16UIntNormalized` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R8G8UIntNormalized` | `Ignore` | Not supported on all devices. Bitmaps of this format can be used as effect sources, but are not directly drawable. |
| `R8UIntNormalized` | `Ignore` | Not supported on all devices. Bitmaps of this format can be used as effect sources, but are not directly drawable. |
| `A8UIntNormalized` | `Premultiplied`, `Straight` | Bitmaps of this format can be used as opacity masks or effect sources, but are not directly drawable. |
| `BC1UIntNormalized` | `Premultiplied`, `Ignore` | |
| `BC2UIntNormalized` | `Premultiplied`, `Ignore` | |
| `BC3UIntNormalized` | `Premultiplied`, `Ignore` | |

## CanvasRenderTarget formats

| `DirectXPixelFormat` | Compatible [`CanvasAlphaMode`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasAlphaMode.htm)-s | Notes
| -- | -- | -- |
| `B8G8R8A8UIntNormalized` | `Premultiplied`, `Ignore` | The default format |
| `B8G8R8A8UIntNormalizedSrgb` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R8G8B8A8UIntNormalized` | `Premultiplied`, `Ignore` | |
| `R8G8B8A8UIntNormalizedSrgb` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R16G16B16A16Float` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R32G32B32A32Float	` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `R16G16B16A16UIntNormalized` | `Premultiplied`, `Ignore` | Not supported on all devices |
| `A8UIntNormalized` | `Premultiplied`, `Straight` | |

## CanvasSwapChain formats

| `DirectXPixelFormat` | Compatible [`CanvasAlphaMode`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasAlphaMode.htm)-s | Notes
| -- | -- | -- |
| `B8G8R8A8UIntNormalized` | `Premultiplied`, `Ignore` | The default format |
| `R8G8B8A8UIntNormalized` | `Premultiplied`, `Ignore` | |
| `R16G16B16A16Float` | `Premultiplied`, `Ignore` | Not supported on all devices |
