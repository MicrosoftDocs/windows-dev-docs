---
title: DirectX and XAML interop
description: You can use Extensible Application Markup Language (XAML) and Microsoft DirectX together in your Universal Windows Platform (UWP) game.
ms.assetid: 0fb2819a-61ed-129d-6564-0b67debf5c6b
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, xaml interop
ms.localizationpriority: medium
---

# DirectX and XAML interop

You can use Extensible Application Markup Language (XAML) and Microsoft DirectX together in your Universal Windows Platform (UWP) game or app. The combination of XAML and DirectX lets you build flexible user interface frameworks that interop with your DirectX-rendered content, and is particularly useful for graphics-intensive apps. This topic explains the structure of a UWP app that uses DirectX and identifies the important types to use when building your UWP app to work with DirectX.

If your app mainly focuses on 2D rendering, you may want to use the [Win2D](https://github.com/microsoft/win2d) Windows Runtime library. This library is maintained by Microsoft and built on top of the core Direct2D technologies. It greatly simplifies the usage pattern to implement 2D graphics and includes helpful abstractions for some of the techniques described in this document. See the project page for more details. This document covers guidance for app developers who choose *not* to use Win2D.

> [!NOTE]
> DirectX APIs are not defined as Windows Runtime types, but you can you typically use [C++/WinRT](../cpp-and-winrt-apis/index.md) to develop XAML UWP components that interoperate with DirectX. Also, you can create a UWP app with C# and XAML that uses DirectX, if you wrap the DirectX calls in a separate Windows Runtime metadata file.

## XAML and DirectX

DirectX provides two powerful libraries for 2D and 3D graphics: Direct2D and Microsoft Direct3D. Although XAML provides support for basic 2D primitives and effects, many apps, such as modeling and gaming, need more complex graphics support. For these, you can use Direct2D and Direct3D to render part or all of the graphics and use XAML for everything else.

If you are implementing custom XAML and DirectX interop, you need to know these two concepts:

-   Shared surfaces are sized regions of the display, defined by XAML, that you can use DirectX to draw into indirectly, using [Windows::UI::Xaml::Media::ImageSource](/uwp/api/windows.ui.xaml.media.imagesource) types. For shared surfaces, you don't control the precise timing for when new content appears on-screen. Rather, updates to the shared surface are synced to the XAML framework's updates.
-   [Swap chains](/windows/desktop/direct3d9/what-is-a-swap-chain-) represent a collection of buffers used to display graphics at minimal latency. Typically, swap chains are updated at 60 frames per second separately from the UI thread. However, swap chains use more memory and CPU resources in order to support rapid updates, and are more difficult to use since you have to manage multiple threads.

Consider what you are using DirectX for. Will it be used to composite or animate a single control that fits within the dimensions of the display window? Will it contain output that needs to be rendered and controlled in real-time, as in a game? If so, you will probably need to implement a swap chain. Otherwise, should be fine using a shared surface.

Once you've determined how you intend to use DirectX, you use one of these Windows Runtime types to incorporate DirectX rendering into your UWP app:

-   If you want to compose a static image, or draw a complex image on event-driven intervals, draw to a shared surface with [Windows::UI::Xaml::Media::Imaging::SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource). This type handles a sized DirectX drawing surface. Typically, you use this type when composing an image or texture as a bitmap for display in a document or UI element. It doesn't work well for real-time interactivity, such as a high-performance game. That's because updates to a **SurfaceImageSource** object are synced to XAML user interface updates, and that can introduce latency into the visual feedback you provide to the user, like a fluctuating frame rate or a perceived poor response to real-time input. Updates are still quick enough for dynamic controls or data simulations, though!

-   If the image is larger than the provided screen real estate, and can be panned or zoomed by the user, use [Windows::UI::Xaml::Media::Imaging::VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource). This type handles a sized DirectX drawing surface that is larger than the screen. Like [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource), you use this when composing a complex image or control dynamically. And, also like **SurfaceImageSource**, it doesn't work well for high-performance games. Some examples of XAML elements that could use a **VirtualSurfaceImageSource** are map controls, or a large, image-dense document viewer.

-   If you are using DirectX to present graphics updated in real-time, or in a situation where the updates must come on regular low-latency intervals, use the [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) class, so you can refresh the graphics without syncing to the XAML framework refresh timer. This type enables you to access the graphics device's swap chain ([IDXGISwapChain1](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1)) directly and layer XAML atop the render target. This type works great for games and full-screen DirectX apps that require a XAML-based user interface. You must know DirectX well to use this approach, including the Microsoft DirectX Graphics Infrastructure (DXGI), Direct2D, and Direct3D technologies. For more info, see [Programming Guide for Direct3D 11](/windows/desktop/direct3d11/dx-graphics-overviews).

## SurfaceImageSource

[SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) provides DirectX shared surfaces to draw into and then composes the bits into app content.

Here is the basic process for creating and updating a [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) object in the code-behind:

1.  Define the size of the shared surface by passing the height and width to the [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) constructor. You can also indicate whether the surface needs alpha (opacity) support.

    For example:

    `SurfaceImageSource^ surfaceImageSource = ref new SurfaceImageSource(400, 300);`

2.  Get a pointer to [ISurfaceImageSourceNativeWithD2D](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d). Cast the [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) object as [IInspectable](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) (or **IUnknown**), and call **QueryInterface** on it to get the underlying **ISurfaceImageSourceNativeWithD2D** implementation. You use the methods defined on this implementation to set the device and run the draw operations.

    ```cpp
    Microsoft::WRL::ComPtr<ISurfaceImageSourceNativeWithD2D> m_sisNativeWithD2D;

    // ...

    IInspectable* sisInspectable = 
        (IInspectable*) reinterpret_cast<IInspectable*>(surfaceImageSource);

    sisInspectable->QueryInterface(
        __uuidof(ISurfaceImageSourceNativeWithD2D), 
        (void **)&m_sisNativeWithD2D);
    ```

3.  Create the DXGI and D2D devices by first calling [D3D11CreateDevice](https://docs.microsoft.com/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) and [D2D1CreateDevice](https://docs.microsoft.com/windows/desktop/api/d2d1_1/nf-d2d1_1-d2d1createdevice) then passing the device and context to [ISurfaceImageSourceNativeWithD2D::SetDevice](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d-setdevice). 

    > [!NOTE]
    > If you will be drawing to your **SurfaceImageSource** from a background thread, you'll also need to ensure that the DXGI device has enabled multi-threaded access. This should only be done if you will be drawing from a background thread, for performance reasons.

    For example:

    ```cpp
    Microsoft::WRL::ComPtr<ID3D11Device> m_d3dDevice;
    Microsoft::WRL::ComPtr<ID3D11DeviceContext> m_d3dContext;
    Microsoft::WRL::ComPtr<ID2D1Device> m_d2dDevice;

    // Create the DXGI device
    D3D11CreateDevice(
            NULL,
            D3D_DRIVER_TYPE_HARDWARE,
            NULL,
            flags,
            featureLevels,
            ARRAYSIZE(featureLevels),
            D3D11_SDK_VERSION,
            &m_d3dDevice,
            NULL,
            &m_d3dContext);

    Microsoft::WRL::ComPtr<IDXGIDevice> dxgiDevice;
    m_d3dDevice.As(&dxgiDevice);

    // To enable multi-threaded access (optional)
    Microsoft::WRL::ComPtr<ID3D10Multithread> d3dMultiThread;

    m_d3dDevice->QueryInterface(
        __uuidof(ID3D10Multithread), 
        (void **) &d3dMultiThread);

    d3dMultiThread->SetMultithreadProtected(TRUE);

    // Create the D2D device
    D2D1CreateDevice(m_dxgiDevice.Get(), NULL, &m_d2dDevice);

    // Set the D2D device
    m_sisNativeWithD2D->SetDevice(m_d2dDevice.Get());
    ```

4.  Provide a pointer to [ID2D1DeviceContext](https://docs.microsoft.com/windows/desktop/api/dxgi/nn-dxgi-idxgisurface) object to [ISurfaceImageSourceNativeWithD2D::BeginDraw](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d-begindraw), and use the returned drawing context to draw the contents of the desired rectangle within the **SurfaceImageSource**. **ISurfaceImageSourceNativeWithD2D::BeginDraw** and the drawing commands can be called from a background thread. Only the area specified for update in the *updateRect* parameter is drawn.

    This method returns the point (x,y) offset of the updated target rectangle in the *offset* parameter. You use this offset to determine where to draw your updated content with the **ID2D1DeviceContext**.

    ```cpp
    Microsoft::WRL::ComPtr<ID2D1DeviceContext> drawingContext;

    HRESULT beginDrawHR = m_sisNative->BeginDraw(
        updateRect, 
        &drawingContext, 
        &offset);

    if (beginDrawHR == DXGI_ERROR_DEVICE_REMOVED 
        || beginDrawHR == DXGI_ERROR_DEVICE_RESET
        || beginDrawHR == D2DERR_RECREATE_TARGET)
    {
        // The D3D and D2D device was lost and need to be re-created.
        // Recovery steps are:
        // 1) Re-create the D3D and D2D devices
        // 2) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the new D2D
        //    device
        // 3) Redraw the contents of the SurfaceImageSource
    }
    else if (beginDrawHR == E_SURFACE_CONTENTS_LOST)
    {
        // The devices were not lost but the entire contents of the surface
        // were. Recovery steps are:
        // 1) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the D2D 
        //    device again
        // 2) Redraw the entire contents of the SurfaceImageSource
    }
    else 
    {
        // Draw your updated rectangle with the drawingContext
    }
    ```

5. Call [ISurfaceImageSourceNativeWithD2D::EndDraw](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d-enddraw) to complete the bitmap. The bitmap can be used as a source for a XAML [Image](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.image) or [ImageBrush](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Media.ImageBrush). **ISurfaceImageSourceNativeWithD2D::EndDraw** must be called only from the UI thread.

    ```cpp
    m_sisNative->EndDraw();

    // ...
    // The SurfaceImageSource object's underlying 
    // ISurfaceImageSourceNativeWithD2D object contains the completed bitmap.

    ImageBrush^ brush = ref new ImageBrush();
    brush->ImageSource = surfaceImageSource;
    ```

    > [!NOTE]
    > Calling [SurfaceImageSource::SetSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.imaging.bitmapsource.setsource) (inherited from **IBitmapSource::SetSource**) currently throws an exception. Do not call it from your [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) object.

    > [!NOTE]
    > Applications must avoid drawing to **SurfaceImageSource** while their associated [Window](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Window) is hidden, otherwise **ISurfaceImageSourceNativeWithD2D** APIs will fail. To accomplish this, register as an event listener for the [Window.VisibilityChanged](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Window.VisibilityChanged) event to track visibility changes.

## VirtualSurfaceImageSource

[VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) extends [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) when the content is potentially larger than what can fit on screen and so the content must be virtualized to render optimally.

[VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) differs from [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) in that it uses a callback, [IVirtualSurfaceImageSourceCallbacksNative::UpdatesNeeded](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceupdatescallbacknative-updatesneeded), that you implement to update regions of the surface as they become visible on the screen. You do not need to clear regions that are hidden, as the XAML framework takes care of that for you.

Here is the basic process for creating and updating a [VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) object in the code-behind:

1.  Create an instance of [VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) with the size that you want. For example:

    ```cpp
    VirtualSurfaceImageSource^ virtualSIS = 
        ref new VirtualSurfaceImageSource(2000, 2000);
    ```

2.  Get pointers to [IVirtualSurfaceImageSourceNative](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative) and [ISurfaceImageSourceNativeWithD2D](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d). Cast the [VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) object as [IInspectable](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) or [IUnknown](https://docs.microsoft.com/windows/desktop/api/unknwn/nn-unknwn-iunknown), and call [QueryInterface](https://docs.microsoft.com/windows/desktop/api/unknwn/nf-unknwn-iunknown-queryinterface(q_)) on it to get the underlying **IVirtualSurfaceImageSourceNative** and **ISurfaceImageSourceNativeWithD2D** implementations. You use the methods defined on these implementations to set the device and run the draw operations.

    ```cpp
    Microsoft::WRL::ComPtr<IVirtualSurfaceImageSourceNative>  m_vsisNative;
    Microsoft::WRL::ComPtr<ISurfaceImageSourceNativeWithD2D> m_sisNativeWithD2D;

    // ...

    IInspectable* vsisInspectable = 
        (IInspectable*) reinterpret_cast<IInspectable*>(virtualSIS);

    vsisInspectable->QueryInterface(
        __uuidof(IVirtualSurfaceImageSourceNative), 
        (void **) &m_vsisNative);

    vsisInspectable->QueryInterface(
        __uuidof(ISurfaceImageSourceNativeWithD2D), 
        (void **) &m_sisNativeWithD2D);
    ```

3.  Create the DXGI and D2D devices by first calling **D3D11CreateDevice** and **D2D1CreateDevice**, and then pass the D2D device to **ISurfaceImageSourceNativeWithD2D::SetDevice**.

    > [!NOTE]
    > If you will be drawing to your **VirtualSurfaceImageSource** from a background thread, you'll also need to ensure that the DXGI device has enabled multi-threaded access. This should only be done if you will be drawing from a background thread, for performance reasons.

    For example:

    ```cpp
    Microsoft::WRL::ComPtr<ID3D11Device> m_d3dDevice;
    Microsoft::WRL::ComPtr<ID3D11DeviceContext> m_d3dContext;
    Microsoft::WRL::ComPtr<ID2D1Device> m_d2dDevice;

    // Create the DXGI device
    D3D11CreateDevice(
            NULL,
            D3D_DRIVER_TYPE_HARDWARE,
            NULL,
            flags,
            featureLevels,
            ARRAYSIZE(featureLevels),
            D3D11_SDK_VERSION,
            &m_d3dDevice,
            NULL,
            &m_d3dContext);  

    Microsoft::WRL::ComPtr<IDXGIDevice> dxgiDevice;
    m_d3dDevice.As(&dxgiDevice);
    
    // To enable multi-threaded access (optional)
    Microsoft::WRL::ComPtr<ID3D10Multithread> d3dMultiThread;

    m_d3dDevice->QueryInterface(
        __uuidof(ID3D10Multithread), 
        (void **) &d3dMultiThread);

    d3dMultiThread->SetMultithreadProtected(TRUE);

    // Create the D2D device
    D2D1CreateDevice(m_dxgiDevice.Get(), NULL, &m_d2dDevice);

    // Set the D2D device
    m_vsisNativeWithD2D->SetDevice(m_d2dDevice.Get());

    m_vsisNative->SetDevice(dxgiDevice.Get());
    ```

4.  Call [IVirtualSurfaceImageSourceNative::RegisterForUpdatesNeeded](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-registerforupdatesneeded), passing in a reference to your implementation of [IVirtualSurfaceUpdatesCallbackNative](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-ivirtualsurfaceupdatescallbacknative).

    ```cpp
    class MyContentImageSource : public IVirtualSurfaceUpdatesCallbackNative
    {
    // ...
      private:
         virtual HRESULT STDMETHODCALLTYPE UpdatesNeeded() override;
    }

    // ...

    HRESULT STDMETHODCALLTYPE MyContentImageSource::UpdatesNeeded()
    {
      // .. Perform drawing here ...
    }

    void MyContentImageSource::Initialize()
    {
      // ...
      m_vsisNative->RegisterForUpdatesNeeded(this);
      // ...
    }
    ```

    The framework calls your implementation of [IVirtualSurfaceUpdatesCallbackNative::UpdatesNeeded](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-registerforupdatesneeded) when a region of the [VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) needs to be updated.

    This can happen either when the framework determines the region needs to be drawn (such as when the user pans or zooms the view of the surface), or after the app has called [IVirtualSurfaceImageSourceNative::Invalidate](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-invalidate) on that region.

5.  In [IVirtualSurfaceImageSourceNative::UpdatesNeeded](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceupdatescallbacknative-updatesneeded), use the [IVirtualSurfaceImageSourceNative::GetUpdateRectCount](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-getupdaterectcount) and [IVirtualSurfaceImageSourceNative::GetUpdateRects](https://docs.microsoft.com/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-getupdaterects) methods to determine which region(s) of the surface must be drawn.

    ```cpp
    HRESULT STDMETHODCALLTYPE MyContentImageSource::UpdatesNeeded()
    {
        HRESULT hr = S_OK;

        try
        {
            ULONG drawingBoundsCount = 0;  
            m_vsisNative->GetUpdateRectCount(&drawingBoundsCount);

            std::unique_ptr<RECT[]> drawingBounds(
                new RECT[drawingBoundsCount]);

            m_vsisNative->GetUpdateRects(
                drawingBounds.get(), 
                drawingBoundsCount);
            
            for (ULONG i = 0; i < drawingBoundsCount; ++i)
            {
                // Drawing code here ...
            }
        }
        catch (Platform::Exception^ exception)
        {
            hr = exception->HResult;
        }

        return hr;
    }
    ```

6.  Lastly, for each region that must be updated:

    1.  Provide a pointer to the **ID2D1DeviceContext** object to **ISurfaceImageSourceNativeWithD2D::BeginDraw**, and use the returned drawing context to draw the contents of the desired rectangle within the **SurfaceImageSource**. **ISurfaceImageSourceNativeWithD2D::BeginDraw** and the drawing commands can be called from a background thread. Only the area specified for update in the *updateRect* parameter is drawn.

        This method returns the point (x,y) offset of the updated target rectangle in the *offset* parameter. You use this offset to determine where to draw your updated content with the **ID2D1DeviceContext**.

        ```cpp
        Microsoft::WRL::ComPtr<ID2D1DeviceContext> drawingContext;

        HRESULT beginDrawHR = m_sisNative->BeginDraw(
            updateRect, 
            &drawingContext, 
            &offset);

        if (beginDrawHR == DXGI_ERROR_DEVICE_REMOVED 
            || beginDrawHR == DXGI_ERROR_DEVICE_RESET 
            || beginDrawHR == D2DERR_RECREATE_TARGET)
        {
            // The D3D and D2D devices were lost and need to be re-created.
            // Recovery steps are:
            // 1) Re-create the D3D and D2D devices
            // 2) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the 
            //    new D2D device
            // 3) Redraw the contents of the VirtualSurfaceImageSource
        }
        else if (beginDrawHR == E_SURFACE_CONTENTS_LOST)
        {
            // The devices were not lost but the entire contents of the 
            // surface were lost. Recovery steps are:
            // 1) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the 
            //    D2D device again
            // 2) Redraw the entire contents of the VirtualSurfaceImageSource
        }
        else
        {
            // Draw your updated rectangle with the drawingContext
        }
        ```

    2.  Draw the specific content to that region, but constrain your drawing to the bounded regions for better performance.

    3.  Call **ISurfaceImageSourceNativeWithD2D::EndDraw**. The result is a bitmap.

> [!NOTE]
> Applications must avoid drawing to **SurfaceImageSource** while their associated [Window](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Window) is hidden, otherwise **ISurfaceImageSourceNativeWithD2D** APIs will fail. To accomplish this, register as an event listener for the [Window.VisibilityChanged](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Window.VisibilityChanged) event to track visibility changes.

## SwapChainPanel and gaming


[SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) is the Windows Runtime type designed to support high-performance graphics and gaming, where you manage the swap chain directly. In this case, you create your own DirectX swap chain and manage the presentation of your rendered content.

To ensure good performance, there are certain limitations to the [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) type:

-   There are no more than 4 [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) instances per app.
-   You should set the DirectX swap chain's height and width (in [DXGI\_SWAP\_CHAIN\_DESC1](https://docs.microsoft.com/windows/desktop/api/dxgi1_2/ns-dxgi1_2-dxgi_swap_chain_desc1)) to the current dimensions of the swap chain element. If you don't, the display content will be scaled (using **DXGI\_SCALING\_STRETCH**) to fit.
-   You must set the DirectX swap chain's scaling mode (in [DXGI\_SWAP\_CHAIN\_DESC1](https://docs.microsoft.com/windows/desktop/api/dxgi1_2/ns-dxgi1_2-dxgi_swap_chain_desc1)) to **DXGI\_SCALING\_STRETCH**.
-   You must create the DirectX swap chain by calling [IDXGIFactory2::CreateSwapChainForComposition](https://docs.microsoft.com/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgifactory2-createswapchainforcomposition).

You update the [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) based on the needs of your app, and not the updates of the XAML framework. If you need to synchronize the updates of **SwapChainPanel** to those of the XAML framework, register for the [Windows::UI::Xaml::Media::CompositionTarget::Rendering](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.compositiontarget.rendering) event. Otherwise, you must consider any cross-thread issues if you try to update the XAML elements from a different thread than the one updating the **SwapChainPanel**.

If you need to receive low-latency pointer input to your **SwapChainPanel**, use [SwapChainPanel::CreateCoreIndependentInputSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.swapchainpanel.createcoreindependentinputsource). This method returns a [CoreIndependentInputSource](https://docs.microsoft.com/uwp/api/windows.ui.core.coreindependentinputsource) object that can be used to receive input events at minimal latency on a background thread. Note that once this method is called, normal XAML pointer input events will not be fired for the **SwapChainPanel**, since all input will be redirected to the background thread.


> **Note**   In general, your DirectX apps should create swap chains in landscape orientation, and equal to the display window size (which is usually the native screen resolution in most Microsoft Store games). This ensures that your app uses the optimal swap chain implementation when it doesn't have any visible XAML overlay. If the app is rotated to portrait mode, your app should call [IDXGISwapChain1::SetRotation](https://docs.microsoft.com/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-setrotation) on the existing swap chain, apply a transform to the content if needed, and then call [SetSwapChain](/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-iswapchainpanelnative-setswapchain) again on the same swap chain. Similarly, your app should call **SetSwapChain** again on the same swap chain whenever the swap chain is resized by calling [IDXGISwapChain::ResizeBuffers](https://docs.microsoft.com/windows/desktop/api/dxgi/nf-dxgi-idxgiswapchain-resizebuffers).


 

Here is basic process for creating and updating a [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) object in the code-behind:

1.  Get an instance of a swap chain panel for your app. The instances are indicated in your XAML with the `<SwapChainPanel>` tag.

    `Windows::UI::Xaml::Controls::SwapChainPanel^ swapChainPanel;`

    Here is an example `<SwapChainPanel>` tag.

    ```xml
    <SwapChainPanel x:Name="swapChainPanel">
        <SwapChainPanel.ColumnDefinitions>
            <ColumnDefinition Width="300*"/>
            <ColumnDefinition Width="1069*"/>
        </SwapChainPanel.ColumnDefinitions>
    …
    ```

2.  Get a pointer to [ISwapChainPanelNative](/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-iswapchainpanelnative). Cast the [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) object as [IInspectable](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) (or **IUnknown**), and call **QueryInterface** on it to get the underlying **ISwapChainPanelNative** implementation.

    ```cpp
    Microsoft::WRL::ComPtr<ISwapChainPanelNative> m_swapChainNative;
    // ...
    IInspectable* panelInspectable = (IInspectable*) reinterpret_cast<IInspectable*>(swapChainPanel);
    panelInspectable->QueryInterface(__uuidof(ISwapChainPanelNative), (void **)&m_swapChainNative);
    ```

3.  Create the DXGI device and the swap chain, and set the swap chain to [ISwapChainPanelNative](/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-iswapchainpanelnative) by passing it to [SetSwapChain](/windows/desktop/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-iswapchainpanelnative-setswapchain).

    ```cpp
    Microsoft::WRL::ComPtr<IDXGISwapChain1>               m_swapChain;    
    // ...
    DXGI_SWAP_CHAIN_DESC1 swapChainDesc = {0};
            swapChainDesc.Width = m_bounds.Width;
            swapChainDesc.Height = m_bounds.Height;
            swapChainDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;           // This is the most common swapchain format.
            swapChainDesc.Stereo = false; 
            swapChainDesc.SampleDesc.Count = 1;                          // Don't use multi-sampling.
            swapChainDesc.SampleDesc.Quality = 0;
            swapChainDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
            swapChainDesc.BufferCount = 2;
            swapChainDesc.Scaling = DXGI_SCALING_STRETCH;
            swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL; // We recommend using this swap effect for all. applications
            swapChainDesc.Flags = 0;
                    
    // QI for DXGI device
    Microsoft::WRL::ComPtr<IDXGIDevice> dxgiDevice;
    m_d3dDevice.As(&dxgiDevice);

    // Get the DXGI adapter.
    Microsoft::WRL::ComPtr<IDXGIAdapter> dxgiAdapter;
    dxgiDevice->GetAdapter(&dxgiAdapter);

    // Get the DXGI factory.
    Microsoft::WRL::ComPtr<IDXGIFactory2> dxgiFactory;
    dxgiAdapter->GetParent(__uuidof(IDXGIFactory2), &dxgiFactory);
    // Create a swap chain by calling CreateSwapChainForComposition.
    dxgiFactory->CreateSwapChainForComposition(
                m_d3dDevice.Get(),
                &swapChainDesc,
                nullptr,        // Allow on any display. 
                &m_swapChain
                );
            
    m_swapChainNative->SetSwapChain(m_swapChain.Get());
    ```

4.  Draw to the DirectX swap chain, and present it to display the contents.

    ```cpp
    HRESULT hr = m_swapChain->Present(1, 0);
    ```

    The XAML elements are refreshed when the Windows Runtime layout/render logic signals an update.

## Related topics

* [Win2D](https://microsoft.github.io/Win2D/html/Introduction.htm)
* [SurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource)
* [VirtualSurfaceImageSource](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource)
* [SwapChainPanel](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel)
* [ISwapChainPanelNative](/windows/desktop/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-iswapchainpanelnative)
* [Programming Guide for Direct3D 11](/windows/desktop/direct3d11/dx-graphics-overviews)