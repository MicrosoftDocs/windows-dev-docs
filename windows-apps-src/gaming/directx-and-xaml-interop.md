---
title: DirectX and XAML interop
description: You can use Extensible Application Markup Language (XAML) together with Microsoft DirectX together in your Universal Windows Platform (UWP) game.
ms.assetid: 0fb2819a-61ed-129d-6564-0b67debf5c6b
ms.date: 04/27/2021
ms.topic: article
keywords: windows 10, uwp, games, directx, xaml interop
ms.localizationpriority: medium
---

# DirectX and XAML interop

You can use Extensible Application Markup Language (XAML) together with Microsoft DirectX together in your Universal Windows Platform (UWP) game or app. The combination of XAML and DirectX lets you build flexible user interface frameworks that interoperate with your DirectX-rendered content; which is particularly useful for graphics-intensive apps. This topic explains the structure of a UWP app that uses DirectX, and identifies the important types to use when building your UWP app to work with DirectX.

If your app mainly focuses on 2D rendering, then you might want to use the [Win2D](https://github.com/microsoft/win2d) Windows Runtime library. That library is maintained by Microsoft, and is built on top of the core [Direct2D](/windows/win32/direct2d/direct2d-portal) technology. Win2D greatly simplifies the usage pattern to implement 2D graphics, and includes helpful abstractions for some of the techniques described in this document. See the project page for more details. This document covers guidance for app developers who choose *not* to use Win2D.

> [!NOTE]
> DirectX APIs are not defined as Windows Runtime types, but you can use [C++/WinRT](../cpp-and-winrt-apis/index.md) to develop XAML UWP apps that interoperate with DirectX. If you factor the code that calls DirectX into its own C++/WinRT [Windows Runtime component](/windows/uwp/winrt-components/create-a-windows-runtime-component-in-cppwinrt) (WRC), then you can use that WRC in a UWP app (even a C# one) that then combines XAML and DirectX.

## XAML and DirectX

DirectX provides two powerful libraries for 2D and 3D graphics, respectively&mdash;Direct2D and Direct3D. Although XAML provides support for basic 2D primitives and effects, many modeling and gaming apps need more complex graphics support. For these, you can use Direct2D and Direct3D to render the more complex graphics, and use XAML for more traditional user interface (UI) elements.

If you're implementing custom XAML and DirectX interop, then you need to know these two concepts.

* Shared surfaces are sized regions of the display, defined by XAML, that you can use DirectX to draw into indirectly by using [**Windows::UI::Xaml::Media::ImageSource**](/uwp/api/windows.ui.xaml.media.imagesource) types. For shared surfaces, you don't control the precise timing of when new content appears on-screen. Rather, updates to the shared surface are sync'd to the XAML framework's updates.
* A [swap chain](/windows/win32/direct3d9/what-is-a-swap-chain-) represents a collection of buffers that are used to display graphics with minimal latency. Typically, a swap chain is updated at 60 frames per second separately from the UI thread. However, a swap chain uses more memory and CPU resources in order to support rapid updates, and is relatively difficult to use, since you have to manage multiple threads.

Consider what you're using DirectX for. Will you use it to composite or animate a single control that fits within the dimensions of the display window? Will it contain output that needs to be rendered and controlled in real-time, as in a game? If so, then you'll probably need to implement a swap chain. Otherwise, you should be fine using a shared surface.

Once you've determined how you intend to use DirectX, you use one of the following Windows Runtime types to incorporate DirectX rendering into your UWP app.

* If you want to compose a static image, or draw a complex image at event-driven intervals, then draw to a shared surface with [**Windows::UI::Xaml::Media::Imaging::SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource). That type handles a sized DirectX drawing surface. Typically, you use this type when composing an image or texture as a bitmap for display in a document or UI element. It doesn't work well for real-time interactivity, such as a high-performance game. That's because updates to a **SurfaceImageSource** object are synced to XAML user interface updates, and that can introduce latency into the visual feedback you provide to the user, such as a fluctuating frame rate, or a perceived poor response to real-time input. Updates are still quick enough for dynamic controls or data simulations, though.
* If the image is larger than the provided screen real estate, and can be panned or zoomed by the user, then use [**Windows::UI::Xaml::Media::Imaging::VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource). That type handles a sized DirectX drawing surface that is larger than the screen. Like [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource), you use this when composing a complex image or control dynamically. And, also like **SurfaceImageSource**, it doesn't work well for high-performance games. Some examples of XAML elements that could use a **VirtualSurfaceImageSource** are map controls, or a large, image-dense document viewer.
* If you're using DirectX to present graphics updated in real-time, or in a situation where the updates must come on regular low-latency intervals, then use the [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) class, so that you can refresh the graphics without syncing to the XAML framework refresh timer. With **SwapChainPanel** you can access the graphics device's swap chain ([**IDXGISwapChain1**](/windows/win32/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1)) directly, and layer XAML on top of the render target. **SwapChainPanel** works great for games and full-screen DirectX apps that require a XAML-based user interface. You must know DirectX well to use this approach, including the [Microsoft DirectX Graphics Infrastructure](/windows/win32/direct3ddxgi/d3d10-graphics-programming-guide-dxgi) (DXGI), Direct2D, and Direct3D technologies. For more info, see [Programming Guide for Direct3D 11](/windows/win32/direct3d11/dx-graphics-overviews).

## SurfaceImageSource

[**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) provides you with a DirectX shared surface to draw into; it then composes the bits into app content.

The [Line spacing (DirectWrite)](https://docs.microsoft.com/samples/microsoft/windows-universal-samples/dwritelinespacingmodes/) and [Downloadable fonts (DirectWrite)](https://docs.microsoft.com/samples/microsoft/windows-universal-samples/dwritetextlayoutcloudfont/) sample applications demonstrate **SurfaceImageSource**.

At a very high level, here's the process for creating and updating a **SurfaceImageSource**.

* Create a Direct 3D device, a Direct 2D device, and a Direct 2D device context.
* Create a **SurfaceImageSource**, and set the Direct 2D (or Direct 3D) device on that.
* Begin drawing on the **SurfaceImageSource** in order to obtain a DXGI surface.
* Draw to the DXGI surface with Direct2D (or Direct3D).
* End drawing on the **SurfaceImageSource** when you're done.
* Set the **SurfaceImageSource** on a XAML **Image** or **ImageBrush** in order to display it in your XAML UI.

And here's a deeper dive into those steps, with source code examples.

1. You can follow along with the code shown and described below by creating a new project in Microsoft Visual Studio. Create a **Blank App (C++/WinRT)** project. Target the latest generally-available (that is, not preview) version of the Windows SDK.

2. Open `pch.h`, and *add* the following includes below the ones already there.

    ```cppwinrt
    // pch.h
    ...
    #include <d3d11_4.h>
    #include <d2d1_1.h>
    #include <windows.ui.xaml.media.dxinterop.h>
    #include <winrt/Windows.UI.Xaml.Media.Imaging.h>
    ```

3. Add the `using` directive shown below to the top of `MainPage.cpp`, below the ones already there. Also in `MainPage.cpp`, replace the existing implementation of **MainPage::ClickHandler** with the listing shown below. The code creates a Direct 3D device, a Direct 2D device, and a Direct 2D device context. To do that, it calls [**D3D11CreateDevice**](/windows/win32/api/d3d11/nf-d3d11-d3d11createdevice), [**D2D1CreateDevice**](/windows/win32/api/d2d1_1/nf-d2d1_1-d2d1createdevice), and [**ID2D1Device::CreateDeviceContext**](/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1device-createdevicecontext). 

    ```cppwinrt
    // MainPage.cpp | paste this below the existing using directives
    using namespace Windows::UI::Xaml::Media::Imaging;
    ```    

    ```cppwinrt
    // MainPage.cpp | paste this to replace the existing MainPage::ClickHandler
    void MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
    {
        myButton().Content(box_value(L"Clicked"));

        uint32_t creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;

        D3D_FEATURE_LEVEL featureLevels[] =
        {
            D3D_FEATURE_LEVEL_11_1,
            D3D_FEATURE_LEVEL_11_0,
            D3D_FEATURE_LEVEL_10_1,
            D3D_FEATURE_LEVEL_10_0,
            D3D_FEATURE_LEVEL_9_3,
            D3D_FEATURE_LEVEL_9_2,
            D3D_FEATURE_LEVEL_9_1
        };

        // Create the Direct3D device.
        winrt::com_ptr<::ID3D11Device> d3dDevice;
        D3D_FEATURE_LEVEL supportedFeatureLevel;
        winrt::check_hresult(::D3D11CreateDevice(
            nullptr,
            D3D_DRIVER_TYPE_HARDWARE,
            0,
            creationFlags,
            featureLevels,
            ARRAYSIZE(featureLevels),
            D3D11_SDK_VERSION,
            d3dDevice.put(),
            &supportedFeatureLevel,
            nullptr)
        );

        // Get the DXGI device.
        winrt::com_ptr<::IDXGIDevice> dxgiDevice{
            d3dDevice.as<::IDXGIDevice>() };

        // Create the Direct2D device and a corresponding context.
        winrt::com_ptr<::ID2D1Device> d2dDevice;
        ::D2D1CreateDevice(dxgiDevice.get(), nullptr, d2dDevice.put());

        winrt::com_ptr<::ID2D1DeviceContext> d2dDeviceContext;
        winrt::check_hresult(
            d2dDevice->CreateDeviceContext(
                D2D1_DEVICE_CONTEXT_OPTIONS_NONE,
                d2dDeviceContext.put()
            )
        );
    ```

4. Next, add code to create a [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource), and set the Direct 2D (or Direct 3D) device on that by calling [**ISurfaceImageSourceNativeWithD2D::SetDevice**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d-setdevice).

    > [!NOTE]
    > If you'll be drawing to your **SurfaceImageSource** from a background thread, then you'll also need to ensure that the DXGI device has multi-threaded access enabled (as shown in the code below). For performance reasons you should do that *only* if you'll be drawing from a background thread.

    Define the size of the shared surface by passing the height and width to the [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) constructor. You can also indicate whether the surface needs alpha (opacity) support.

    To set the device, and run the draw operations, we'll need a pointer to [**ISurfaceImageSourceNativeWithD2D**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d). To obtain one, query the **SurfaceImageSource** object for its underlying **ISurfaceImageSourceNativeWithD2D** interface.

    ```cppwinrt
    // MainPage.cpp | paste this at the end of MainPage::ClickHandler
    SurfaceImageSource surfaceImageSource(500, 500);

    winrt::com_ptr<::ISurfaceImageSourceNativeWithD2D> sisNativeWithD2D{
        surfaceImageSource.as<::ISurfaceImageSourceNativeWithD2D>() };

    // Associate the Direct2D device with the SurfaceImageSource.
    sisNativeWithD2D->SetDevice(d2dDevice.get());

    // To enable multi-threaded access (optional)
    winrt::com_ptr<::ID3D11Multithread> d3dMultiThread{
        d3dDevice.as<::ID3D11Multithread>() };
    d3dMultiThread->SetMultithreadProtected(true);
    ```

5. Call [**ISurfaceImageSourceNativeWithD2D::BeginDraw**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d-begindraw) to retrieve a DXGI surface (an [**IDXGISurface**](/windows/win32/api/dxgi/nn-dxgi-idxgisurface) interface). You can call **ISurfaceImageSourceNativeWithD2D::BeginDraw** (and the later drawing commands) from a background thread if you've enabled multi-threaded access. In this step you also create a bitmap from the DXGI surface, and set that into the Direct 2D device context.

    In the *offset* parameter, **ISurfaceImageSourceNativeWithD2D::BeginDraw** returns the point offset (an x,y value) of the updated target rectangle. You can use that offset to determine where to draw your updated content with the **ID2D1DeviceContext**.

    ```cppwinrt
    // MainPage.cpp | paste this at the end of MainPage::ClickHandler
    winrt::com_ptr<::IDXGISurface> dxgiSurface;

    RECT updateRect{ 0, 0, 500, 500 };
    POINT offset{ 0, 0 };
    HRESULT beginDrawHR = sisNativeWithD2D->BeginDraw(
        updateRect,
        __uuidof(::IDXGISurface),
        dxgiSurface.put_void(),
        &offset);

    // Create render target.
    winrt::com_ptr<::ID2D1Bitmap1> bitmap;
    winrt::check_hresult(
        d2dDeviceContext->CreateBitmapFromDxgiSurface(
            dxgiSurface.get(),
            nullptr,
            bitmap.put()
        )
    );

    // Set context's render target.
    d2dDeviceContext->SetTarget(bitmap.get());
    ```

6. Use the Direct 2D device context to draw the contents of the **SurfaceImageSource**. Only the area specified for update in the previous step in the *updateRect* parameter is drawn.

    ```cppwinrt
    // MainPage.cpp | paste this at the end of MainPage::ClickHandler
    if (beginDrawHR == DXGI_ERROR_DEVICE_REMOVED ||
        beginDrawHR == DXGI_ERROR_DEVICE_RESET ||
        beginDrawHR == D2DERR_RECREATE_TARGET)
    {
        // The Direct3D and Direct2D devices were lost and need to be re-created.
        // Recovery steps are:
        // 1) Re-create the Direct3D and Direct2D devices
        // 2) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the new Direct2D
        //    device
        // 3) Redraw the contents of the SurfaceImageSource
    }
    else if (beginDrawHR == E_SURFACE_CONTENTS_LOST)
    {
        // The devices were not lost but the entire contents of the surface
        // were. Recovery steps are:
        // 1) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the Direct2D 
        //    device again
        // 2) Redraw the entire contents of the SurfaceImageSource
    }
    else
    {
        // Draw using Direct2D context.
        d2dDeviceContext->BeginDraw();

        d2dDeviceContext->Clear(D2D1::ColorF(D2D1::ColorF::Orange));

        winrt::com_ptr<::ID2D1SolidColorBrush> brush;
        winrt::check_hresult(d2dDeviceContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::Chocolate),
            D2D1::BrushProperties(0.8f),
            brush.put()));

        D2D1_SIZE_F const size{ 500, 500 };
        D2D1_RECT_F const rect{ 100.0f, 100.0f, size.width - 100.0f, size.height - 100.0f };
        d2dDeviceContext->DrawRectangle(rect, brush.get(), 100.0f);

        d2dDeviceContext->EndDraw();
    }
    ```

7. Call [**ISurfaceImageSourceNativeWithD2D::EndDraw**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d-enddraw) to complete the bitmap (you must call **ISurfaceImageSourceNativeWithD2D::EndDraw** only from the UI thread). Then set the **SurfaceImageSource** on a XAML [**Image**](/uwp/api/windows.ui.xaml.controls.image) (or [**ImageBrush**](/uwp/api/windows.ui.xaml.media.imagebrush)) in order to display it in your XAML UI.

    ```cppwinrt
    // MainPage.cpp | paste this at the end of MainPage::ClickHandler
    sisNativeWithD2D->EndDraw();

    // The SurfaceImageSource object's underlying 
    // ISurfaceImageSourceNativeWithD2D object will contain the completed bitmap.

    theImage().Source(surfaceImageSource);
    ```

    > [!NOTE]
    > Calling [**SurfaceImageSource::SetSource**](/uwp/api/windows.ui.xaml.media.imaging.bitmapsource.setsource) (inherited from **IBitmapSource::SetSource**) currently throws an exception. Don't call it from your [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) object.

    > [!NOTE]
    > Avoid drawing to **SurfaceImageSource** while your [**Window**](/uwp/api/Windows.UI.Xaml.Window) is hidden or inactive, otherwise **ISurfaceImageSourceNativeWithD2D** APIs will fail. Handle events around window visibility and application suspension to accomplish that.

8. Finally, add the following **Image** element inside the existing XAML markup in `MainPage.xaml`.

    ```xaml
    <!-- MainPage.xaml -->
    ...
    <Image x:Name="theImage" Width="500" Height="500" />
    ...
    ```

9. You can now build and run the app. Click the button to see the contents of the **SurfaceImageSource** displayed in the **Image**.

![A thick, dark orange rectanglular outline against a lighter orange background](images/sis.png)

## VirtualSurfaceImageSource

[**VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) extends [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) when the content is potentially larger than what can fit on screen and so the content must be virtualized to render optimally.

[**VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) differs from [**SurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.SurfaceImageSource) in that it uses a callback, [**IVirtualSurfaceImageSourceCallbacksNative::UpdatesNeeded**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceupdatescallbacknative-updatesneeded), that you implement to update regions of the surface as they become visible on the screen. You do not need to clear regions that are hidden, as the XAML framework takes care of that for you.

Here is the basic process for creating and updating a [**VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) object in the code-behind:

1.  Create an instance of [**VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) with the size that you want. For example:

    ```cpp
    VirtualSurfaceImageSource^ virtualSIS = 
        ref new VirtualSurfaceImageSource(2000, 2000);
    ```

2.  Get pointers to [**IVirtualSurfaceImageSourceNative**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative) and [**ISurfaceImageSourceNativeWithD2D**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-isurfaceimagesourcenativewithd2d). Cast the [**VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) object as [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable) or [**IUnknown**](/windows/win32/api/unknwn/nn-unknwn-iunknown), and call [**QueryInterface**](/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(q_)) on it to get the underlying **IVirtualSurfaceImageSourceNative** and **ISurfaceImageSourceNativeWithD2D** implementations. You use the methods defined on these implementations to set the device and run the draw operations.

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

3.  Create the DXGI and Direct2D devices by first calling **D3D11CreateDevice** and **D2D1CreateDevice**, and then pass the Direct2D device to **ISurfaceImageSourceNativeWithD2D::SetDevice**.

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

    // Create the Direct2D device
    D2D1CreateDevice(m_dxgiDevice.Get(), NULL, &m_d2dDevice);

    // Set the Direct2D device
    m_vsisNativeWithD2D->SetDevice(m_d2dDevice.Get());

    m_vsisNative->SetDevice(dxgiDevice.Get());
    ```

4.  Call [**IVirtualSurfaceImageSourceNative::RegisterForUpdatesNeeded**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-registerforupdatesneeded), passing in a reference to your implementation of [**IVirtualSurfaceUpdatesCallbackNative**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-ivirtualsurfaceupdatescallbacknative).

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

    The framework calls your implementation of [**IVirtualSurfaceUpdatesCallbackNative::UpdatesNeeded**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-registerforupdatesneeded) when a region of the [**VirtualSurfaceImageSource**](/uwp/api/Windows.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource) needs to be updated.

    This can happen either when the framework determines the region needs to be drawn (such as when the user pans or zooms the view of the surface), or after the app has called [**IVirtualSurfaceImageSourceNative::Invalidate**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-invalidate) on that region.

5.  In [**IVirtualSurfaceImageSourceNative::UpdatesNeeded**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceupdatescallbacknative-updatesneeded), use the [**IVirtualSurfaceImageSourceNative::GetUpdateRectCount**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-getupdaterectcount) and [**IVirtualSurfaceImageSourceNative::GetUpdateRects**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-ivirtualsurfaceimagesourcenative-getupdaterects) methods to determine which region(s) of the surface must be drawn.

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
            // The Direct3D and Direct2D devices were lost and need to be re-created.
            // Recovery steps are:
            // 1) Re-create the Direct3D and Direct2D devices
            // 2) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the 
            //    new Direct2D device
            // 3) Redraw the contents of the VirtualSurfaceImageSource
        }
        else if (beginDrawHR == E_SURFACE_CONTENTS_LOST)
        {
            // The devices were not lost but the entire contents of the 
            // surface were lost. Recovery steps are:
            // 1) Call ISurfaceImageSourceNativeWithD2D::SetDevice with the 
            //    Direct2D device again
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
> Applications must avoid drawing to **SurfaceImageSource** while their associated [**Window**](/uwp/api/Windows.UI.Xaml.Window) is hidden, otherwise **ISurfaceImageSourceNativeWithD2D** APIs will fail. To accomplish this, register as an event listener for the [**Window.VisibilityChanged**](/uwp/api/Windows.UI.Xaml.Window.VisibilityChanged) event to track visibility changes.

## SwapChainPanel and gaming

[**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) is the Windows Runtime type designed to support high-performance graphics and gaming, where you manage the swap chain directly. In this case, you create your own DirectX swap chain and manage the presentation of your rendered content.

To ensure good performance, there are certain limitations to the [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) type:

* There are no more than 4 [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) instances per app.
* You should set the DirectX swap chain's height and width (in [**DXGI_SWAP_CHAIN_DESC1**](/windows/win32/api/dxgi1_2/ns-dxgi1_2-dxgi_swap_chain_desc1)) to the current dimensions of the swap chain element. If you don't, the display content will be scaled (using **DXGI\_SCALING\_STRETCH**) to fit.
* You must set the DirectX swap chain's scaling mode (in [**DXGI_SWAP_CHAIN_DESC1**](/windows/win32/api/dxgi1_2/ns-dxgi1_2-dxgi_swap_chain_desc1)) to **DXGI\_SCALING\_STRETCH**.
* You must create the DirectX swap chain by calling [**IDXGIFactory2::CreateSwapChainForComposition**](/windows/win32/api/dxgi1_2/nf-dxgi1_2-idxgifactory2-createswapchainforcomposition).

You update the [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) based on the needs of your app, and not the updates of the XAML framework. If you need to synchronize the updates of **SwapChainPanel** to those of the XAML framework, register for the [**Windows::UI::Xaml::Media::CompositionTarget::Rendering**](/uwp/api/windows.ui.xaml.media.compositiontarget.rendering) event. Otherwise, you must consider any cross-thread issues if you try to update the XAML elements from a different thread than the one updating the **SwapChainPanel**.

If you need to receive low-latency pointer input to your **SwapChainPanel**, use [**SwapChainPanel::CreateCoreIndependentInputSource**](/uwp/api/windows.ui.xaml.controls.swapchainpanel.createcoreindependentinputsource). This method returns a [**CoreIndependentInputSource**](/uwp/api/windows.ui.core.coreindependentinputsource) object that can be used to receive input events at minimal latency on a background thread. Note that once this method is called, normal XAML pointer input events will not be fired for the **SwapChainPanel**, since all input will be redirected to the background thread.


> **Note**   In general, your DirectX apps should create swap chains in landscape orientation, and equal to the display window size (which is usually the native screen resolution in most Microsoft Store games). This ensures that your app uses the optimal swap chain implementation when it doesn't have any visible XAML overlay. If the app is rotated to portrait mode, your app should call [**IDXGISwapChain1::SetRotation**](/windows/win32/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-setrotation) on the existing swap chain, apply a transform to the content if needed, and then call [**SetSwapChain**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-iswapchainpanelnative-setswapchain) again on the same swap chain. Similarly, your app should call **SetSwapChain** again on the same swap chain whenever the swap chain is resized by calling [**IDXGISwapChain::ResizeBuffers**](/windows/win32/api/dxgi/nf-dxgi-idxgiswapchain-resizebuffers).

Here is basic process for creating and updating a [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) object in the code-behind:

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

2.  Get a pointer to [**ISwapChainPanelNative**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-iswapchainpanelnative). Cast the [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) object as [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable) (or **IUnknown**), and call **QueryInterface** on it to get the underlying **ISwapChainPanelNative** implementation.

    ```cpp
    Microsoft::WRL::ComPtr<ISwapChainPanelNative> m_swapChainNative;
    // ...
    IInspectable* panelInspectable = (IInspectable*) reinterpret_cast<IInspectable*>(swapChainPanel);
    panelInspectable->QueryInterface(__uuidof(ISwapChainPanelNative), (void **)&m_swapChainNative);
    ```

3.  Create the DXGI device and the swap chain, and set the swap chain to [**ISwapChainPanelNative**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-iswapchainpanelnative) by passing it to [**SetSwapChain**](/windows/win32/api/windows.ui.xaml.media.dxinterop/nf-windows-ui-xaml-media-dxinterop-iswapchainpanelnative-setswapchain).

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
* [ISwapChainPanelNative](/windows/win32/api/windows.ui.xaml.media.dxinterop/nn-windows-ui-xaml-media-dxinterop-iswapchainpanelnative)
* [Programming Guide for Direct3D 11](/windows/win32/direct3d11/dx-graphics-overviews)
