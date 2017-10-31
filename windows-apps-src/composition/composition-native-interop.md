---
author: jwmsft
ms.assetid: 16ad97eb-23f1-0264-23a9-a1791b4a5b95
title: Composition native interoperation
description: The Windows.UI.Composition API provides native interoperation interfaces allowing content to be moved directly into the compositor.
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Composition native interoperation with DirectX and Direct2D

The Windows.UI.Composition API provides the [**ICompositorInterop**](https://msdn.microsoft.com/library/windows/apps/Mt620068), [**ICompositionDrawingSurfaceInterop**](https://msdn.microsoft.com/library/windows/apps/Mt620058), and [**ICompositionGraphicsDeviceInterop**](https://msdn.microsoft.com/library/windows/apps/Mt620065) native interoperation interfaces allowing content to be moved directly into the compositor.

Native interoperation is structured around surface objects that are backed by DirectX textures. The surfaces are created from a factory object called [**CompositionGraphicsDevice**](https://msdn.microsoft.com/library/windows/apps/Dn706749). This object is backed by an underlying Direct2D or Direct3D device object, which it uses to allocate video memory for surfaces. The composition API never creates the underlying DirectX device. It is the responsibility of the application to create one and pass it to the **CompositionGraphicsDevice** object. An application may create more than one **CompositionGraphicsDevice** object at a time, and it may use the same DirectX device as the rendering device for multiple **CompositionGraphicsDevice** objects.

## Creating a surface

Each [**CompositionGraphicsDevice**](https://msdn.microsoft.com/library/windows/apps/Dn706749) serves as a surface factory. Each surface is created with an initial size (which may be 0,0), but no valid pixels. A surface in its initial state may be immediately consumed in a visual tree, for example, via a [**CompositionSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589415) and a [**SpriteVisual**](https://msdn.microsoft.com/library/windows/apps/Mt589433), but in its initial state the surface has no effect on screen output. It is, for all purposes, entirely transparent, even if the specified alpha mode is “opaque”.

Occasionally, DirectX devices may be rendered unusable. This may happen, amongst other reasons, if the application passes invalid arguments to certain DirectX APIs, or if the graphics adapter is reset by the system, or if the driver is updated. Direct3D has an API that an application may use to discover, asynchronously, if the device is lost for any reason. When a DirectX device is lost, the application must discard it, create a new one, and pass it to any [**CompositionGraphicsDevice**](https://msdn.microsoft.com/library/windows/apps/Dn706749) objects previously associated with the bad DirectX device.

## Loading pixels into a surface

To load pixels into the surface, the application must call the [**BeginDraw**](https://msdn.microsoft.com/library/windows/apps/mt620059.aspx) method, which returns a DirectX interface representing a texture or Direct2D context, depending on what the application requests. The application must then render or upload pixels into that texture. When the application is done, it must call the [**EndDraw**](https://msdn.microsoft.com/library/windows/apps/mt620060) method. Only at that point are the new pixels available for composition, but they still don't show up on screen until the next time all changes to the visual tree are committed. If the visual tree is committed before **EndDraw** is called, then the update that is in progress is not visible on screen and the surface continues to display the contents it had prior to **BeginDraw**. When **EndDraw** is called, the texture or Direct2D context pointer returned by BeginDraw is invalidated. An application should never cache that pointer beyond the **EndDraw** call.

The application may only call BeginDraw on one surface at a time, for any given [**CompositionGraphicsDevice**](https://msdn.microsoft.com/library/windows/apps/Dn706749). After calling [**BeginDraw**](https://msdn.microsoft.com/library/windows/apps/mt620059.aspx), the application must call [**EndDraw**](https://msdn.microsoft.com/library/windows/apps/mt620060) on that surface before calling **BeginDraw** on another. As the API is agile, the application is responsible for synchronizing these calls if it wishes to perform rendering from multiple worker threads. If an application wants to interrupt rendering one surface and switch to another temporarily, the application may use the [**SuspendDraw**](https://msdn.microsoft.com/library/windows/apps/mt620064.aspx) method. This allows another **BeginDraw** to succeed, but does not make the first surface update available for on-screen composition. This allows the application to perform multiple updates in a transactional manner. Once a surface is suspended, the application may continue the update by calling the [**ResumeDraw**](https://msdn.microsoft.com/library/windows/apps/mt620062) method, or it may declare that the update is done by calling **EndDraw**. This means only one surface can be actively updated at a time for any given **CompositionGraphicsDevice**. Each graphics device keeps this state independently of the others, so an application may render to two surfaces simultaneously if they belong to different graphics devices. However, this precludes the video memory for those two surfaces from being pooled together and, as such, is less memory efficient.

The [**BeginDraw**](https://msdn.microsoft.com/library/windows/apps/mt620059.aspx), [**SuspendDraw**](https://msdn.microsoft.com/library/windows/apps/mt620064.aspx), [**ResumeDraw**](https://msdn.microsoft.com/library/windows/apps/mt620062) and [**EndDraw**](https://msdn.microsoft.com/library/windows/apps/mt620060) methods return failures if the application performs an incorrect operation (such as passing invalid arguments, or calling **BeginDraw** on a surface before calling **EndDraw** on another). These types of failures represent application bugs and, as such, the expectation is that they are handled with a fail fast. **BeginDraw** may also return a failure if the underlying DirectX device is lost. This failure is not fatal as the application can recreate its DirectX device and try again. As such, the application is expected to handle device loss by simply skipping rendering. If **BeginDraw** fails for any reason, the application should also not call **EndDraw**, as the begin never succeeded in the first place.

## Scrolling

For performance reasons, when an application calls [**BeginDraw**](https://msdn.microsoft.com/library/windows/apps/mt620059.aspx) the contents of the returned texture are not guaranteed to be the previous contents of the surface. The application must assume that the contents are random and, as such, the application must ensure that all pixels are touched, either by clearing the surface before rendering or by drawing enough opaque contents to cover the entire updated rectangle. This, combined with the fact that the texture pointer is only valid between **BeginDraw** and [**EndDraw**](https://msdn.microsoft.com/library/windows/apps/mt620060) calls, makes it impossible for the application to copy previous contents out of the surface. For this reason, we offer a [**Scroll**](https://msdn.microsoft.com/library/windows/apps/mt620063) method, which allows the application to perform a same-surface pixel copy.

## Usage Example

The following sample illustrates a very simple scenario where an application creates drawing surfaces, and uses [**BeginDraw**](https://msdn.microsoft.com/library/windows/apps/mt620059.aspx) and [**EndDraw**](https://msdn.microsoft.com/library/windows/apps/mt620060) to populate the surfaces with text. The application uses DirectWrite to layout the text (details not shown) and then uses Direct2D to render it. The composition graphics device accepts the Direct2D device directly at initialization time. This allows **BeginDraw** to return an ID2D1DeviceContext interface pointer, which is considerably more efficient than having the application create a Direct2D context to wrap a returned ID3D11Texture2D interface at each drawing operation.

```cpp
//------------------------------------------------------------------------------
//
// Copyright (C) Microsoft. All rights reserved.
//
//------------------------------------------------------------------------------

#include "stdafx.h"

using namespace Microsoft::WRL;
using namespace Windows::Foundation;
using namespace Windows::Graphics::DirectX;
using namespace Windows::UI::Composition;

// This is an app-provided helper to render lines of text
class SampleText
{
private:
    // The text to draw
    ComPtr<IDWriteTextLayout> _text;

    // The composition surface that we use in the visual tree
    ComPtr<ICompositionDrawingSurfaceInterop> _drawingSurfaceInterop;

    // The device that owns the surface
    ComPtr<ICompositionGraphicsDevice> _compositionGraphicsDevice;

    // For managing our event notifier
    EventRegistrationToken _deviceReplacedEventToken;

public:
    SampleText(IDWriteTextLayout* text, ICompositionGraphicsDevice* compositionGraphicsDevice) :
        _text(text),
        _compositionGraphicsDevice(compositionGraphicsDevice)
    {
        // Create the surface just big enough to hold the formatted text block.
        DWRITE_TEXT_METRICS metrics;
        FailFastOnFailure(text->GetMetrics(&metrics));
        Windows::Foundation::Size surfaceSize = { metrics.width, metrics.height };
        ComPtr<ICompositionDrawingSurface> drawingSurface;
        FailFastOnFailure(_compositionGraphicsDevice->CreateDrawingSurface(
            surfaceSize,
            DirectXPixelFormat::DirectXPixelFormat_B8G8R8A8UIntNormalized,
            DirectXAlphaMode::DirectXAlphaMode_Ignore,
            &drawingSurface));

        // Cache the interop pointer, since that's what we always use.
        FailFastOnFailure(drawingSurface.As(&_drawingSurfaceInterop));

        // Draw the text
        DrawText();

        // If the rendering device is lost, the application will recreate and replace it. We then
        // own redrawing our pixels.
        FailFastOnFailure(_compositionGraphicsDevice->add_RenderingDeviceReplaced(
            Callback<RenderingDeviceReplacedEventHandler>([this](
                ICompositionGraphicsDevice* source, IRenderingDeviceReplacedEventArgs* args)
                -> HRESULT
            {
                // Draw the text again
                DrawText();
                return S_OK;
            }).Get(),
            &_deviceReplacedEventToken));
    }

    ~SampleText()
    {
        FailFastOnFailure(_compositionGraphicsDevice->remove_RenderingDeviceReplaced(
            _deviceReplacedEventToken));
    }

    // Return the underlying surface to the caller
    ComPtr<ICompositionSurface> get_Surface()
    {
        // To the caller, the fact that we have a drawing surface is an implementation detail.
        // Return the base interface instead
        ComPtr<ICompositionSurface> surface;
        FailFastOnFailure(_drawingSurfaceInterop.As(&surface));
        return surface;
    }

private:
    // We may detect device loss on BeginDraw calls. This helper handles this condition or other
    // errors.
    bool CheckForDeviceRemoved(HRESULT hr)
    {
        if (SUCCEEDED(hr))
        {
            // Everything is fine -- go ahead and draw
            return true;
        }
        else if (hr == DXGI_ERROR_DEVICE_REMOVED)
        {
            // We can't draw at this time, but this failure is recoverable. Just skip drawing for
            // now. We will be asked to draw again once the Direct3D device is recreated
            return false;
        }
        else
        {
            // Any other error is unexpected and, therefore, fatal
            FailFast();
        }
    }

    // Renders the text into our composition surface
    void DrawText()
    {
        // Begin our update of the surface pixels. If this is our first update, we are required
        // to specify the entire surface, which nullptr is shorthand for (but, as it works out,
        // any time we make an update we touch the entire surface, so we always pass nullptr).
        ComPtr<ID2D1DeviceContext> d2dDeviceContext;
        POINT offset;
        if (CheckForDeviceRemoved(_drawingSurfaceInterop->BeginDraw(nullptr,
            __uuidof(ID2D1DeviceContext), &d2dDeviceContext, &offset)))
        {
            // Create a solid color brush for the text. A more sophisticated application might want
            // to cache and reuse a brush across all text elements instead, taking care to recreate
            // it in the event of device removed.
            ComPtr<ID2D1SolidColorBrush> brush;
            FailFastOnFailure(d2dDeviceContext->CreateSolidColorBrush(
                D2D1::ColorF(D2D1::ColorF::Black, 1.0f), &brush));

            // Draw the line of text at the specified offset, which corresponds to the top-left
            // corner of our drawing surface. Notice we don't call BeginDraw on the D2D device
            // context; this has already been done for us by the composition API.
            d2dDeviceContext->DrawTextLayout(D2D1::Point2F(offset.x, offset.y), _text.Get(),
                brush.Get());

            // Our update is done. EndDraw never indicates rendering device removed, so any
            // failure here is unexpected and, therefore, fatal.
            FailFastOnFailure(_drawingSurfaceInterop->EndDraw());
        }
    }
};

class SampleApp
{
    ComPtr<ICompositor> _compositor;
    ComPtr<ID2D1Device> _d2dDevice;
    ComPtr<ICompositionGraphicsDevice> _compositionGraphicsDevice;
    std::vector<ComPtr<SampleText>> _textSurfaces;

public:
    // Run once when the application starts up
    void Initialize(ICompositor* compositor)
    {
        // Cache the compositor (created outside of this method)
        _compositor = compositor;

        // Create a Direct2D device (helper implementation not shown here)
        FailFastOnFailure(CreateDirect2DDevice(&_d2dDevice));

        // To create a composition graphics device, we need to QI for another interface
        ComPtr<ICompositorInterop> compositorInterop;
        FailFastOnFailure(_compositor.As(&compositorInterop));

        // Create a graphics device backed by our D3D device
        FailFastOnFailure(compositorInterop->CreateGraphicsDevice(
            _d2dDevice.Get(),
            &_compositionGraphicsDevice));
    }

    // Called when Direct3D signals the device lost event
    void OnDirect3DDeviceLost()
    {
        // Create a new device
        FailFastOnFailure(CreateDirect2DDevice(_d2dDevice.ReleaseAndGetAddressOf()));

        // Restore our composition graphics device to good health
        ComPtr<ICompositionGraphicsDeviceInterop> compositionGraphicsDeviceInterop;
        FailFastOnFailure(_compositionGraphicsDevice.As(&compositionGraphicsDeviceInterop));
        FailFastOnFailure(compositionGraphicsDeviceInterop->SetRenderingDevice(_d2dDevice.Get()));
    }

    // Create a surface that is asynchronously filled with an image
    ComPtr<ICompositionSurface> CreateSurfaceFromTextLayout(IDWriteTextLayout* text)
    {
        // Create our wrapper object that will handle downloading and decoding the image (assume
        // throwing new here)
        SampleText* textSurface = new SampleText(text, _compositionGraphicsDevice.Get());

        // Keep our image alive
        _textSurfaces.push_back(textSurface);

        // The caller is only interested in the underlying surface
        return textSurface->get_Surface();
    }

    // Create a visual that holds an image
    ComPtr<IVisual> CreateVisualFromTextLayout(IDWriteTextLayout* text)
    {
        // Create a sprite visual
        ComPtr<ISpriteVisual> spriteVisual;
        FailFastOnFailure(_compositor->CreateSpriteVisual(&spriteVisual));

        // The sprite visual needs a brush to hold the image
        ComPtr<ICompositionSurfaceBrush> surfaceBrush;
        FailFastOnFailure(_compositor->CreateSurfaceBrushWithSurface(
            CreateSurfaceFromTextLayout(text).Get(),
            &surfaceBrush));

        // Associate the brush with the visual
        ComPtr<ICompositionBrush> brush;
        FailFastOnFailure(surfaceBrush.As(&brush));
        FailFastOnFailure(spriteVisual->put_Brush(brush.Get()));

        // Return the visual to the caller as the base class
        ComPtr<IVisual> visual;
        FailFastOnFailure(spriteVisual.As(&visual));

        return visual;
    }

private:
    // This helper (implementation not shown here) creates a Direct2D device and registers
    // for a device loss notification on the underlying Direct3D device. When that notification is
    // raised, assume the OnDirect3DDeviceLost method is called.
    HRESULT CreateDirect2DDevice(ID2D1Device** ppDevice);
};
```

 

 




