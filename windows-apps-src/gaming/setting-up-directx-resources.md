---
author: mtoepke
title: Set up DirectX resources and display an image
description: Here, we show you how to create a Direct3D device, swap chain, and render-target view, and how to present the rendered image to the display.
ms.assetid: d54d96fe-3522-4acb-35f4-bb11c3a5b064
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, directx, resources, images
ms.localizationpriority: medium
---

# Set up DirectX resources and display an image



Here, we show you how to create a Direct3D device, swap chain, and render-target view, and how to present the rendered image to the display.

**Objective:** To set up DirectX resources in a C++ Universal Windows Platform (UWP) app and to display a solid color.

## Prerequisites


We assume that you are familiar with C++. You also need basic experience with graphics programming concepts.

**Time to complete:** 20 minutes.

## Instructions

### 1. Declaring Direct3D interface variables with ComPtr

We declare Direct3D interface variables with the ComPtr [smart pointer](https://msdn.microsoft.com/library/windows/apps/hh279674.aspx) template from the Windows Runtime C++ Template Library (WRL), so we can manage the lifetime of those variables in an exception safe manner. We can then use those variables to access the [**ComPtr class**](https://msdn.microsoft.com/library/windows/apps/br244983.aspx) and its members. For example:

```cpp
    ComPtr<ID3D11RenderTargetView> m_renderTargetView;
    m_d3dDeviceContext->OMSetRenderTargets(
        1,
        m_renderTargetView.GetAddressOf(),
        nullptr // Use no depth stencil.
        );
```

If you declare [**ID3D11RenderTargetView**](https://msdn.microsoft.com/library/windows/desktop/ff476582) with ComPtr, you can then use ComPtr’s **GetAddressOf** method to get the address of the pointer to **ID3D11RenderTargetView** (\*\*ID3D11RenderTargetView) to pass to [**ID3D11DeviceContext::OMSetRenderTargets**](https://msdn.microsoft.com/library/windows/desktop/ff476464). **OMSetRenderTargets** binds the render target to the [output-merger stage](https://msdn.microsoft.com/library/windows/desktop/bb205120) to specify the render target as the output target.

After the sample app is started, it initializes and loads, and is then ready to run.

### 2. Creating the Direct3D device

To use the Direct3D API to render a scene, we must first create a Direct3D device that represents the display adapter. To create the Direct3D device, we call the [**D3D11CreateDevice**](https://msdn.microsoft.com/library/windows/desktop/ff476082) function. We specify levels 9.1 through 11.1 in the array of [**D3D\_FEATURE\_LEVEL**](https://msdn.microsoft.com/library/windows/desktop/ff476329) values. Direct3D walks the array in order and returns the highest supported feature level. So, to get the highest feature level available, we list the **D3D\_FEATURE\_LEVEL** array entries from highest to lowest. We pass the [**D3D11\_CREATE\_DEVICE\_BGRA\_SUPPORT**](https://msdn.microsoft.com/library/windows/desktop/ff476107#D3D11_CREATE_DEVICE_BGRA_SUPPORT) flag to the *Flags* parameter to make Direct3D resources interoperate with Direct2D. If we use the debug build, we also pass the [**D3D11\_CREATE\_DEVICE\_DEBUG**](https://msdn.microsoft.com/library/windows/desktop/ff476107#D3D11_CREATE_DEVICE_DEBUG) flag. For more info about debugging apps, see [Using the debug layer to debug apps](https://msdn.microsoft.com/library/windows/desktop/jj200584).

We obtain the Direct3D 11.1 device ([**ID3D11Device1**](https://msdn.microsoft.com/library/windows/desktop/hh404575)) and device context ([**ID3D11DeviceContext1**](https://msdn.microsoft.com/library/windows/desktop/hh404598)) by querying the Direct3D 11 device and device context that are returned from [**D3D11CreateDevice**](https://msdn.microsoft.com/library/windows/desktop/ff476082).

```cpp
        // First, create the Direct3D device.

        // This flag is required in order to enable compatibility with Direct2D.
        UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;

#if defined(_DEBUG)
        // If the project is in a debug build, enable debugging via SDK Layers with this flag.
        creationFlags |= D3D11_CREATE_DEVICE_DEBUG;
#endif

        // This array defines the ordering of feature levels that D3D should attempt to create.
        D3D_FEATURE_LEVEL featureLevels[] =
        {
            D3D_FEATURE_LEVEL_11_1,
            D3D_FEATURE_LEVEL_11_0,
            D3D_FEATURE_LEVEL_10_1,
            D3D_FEATURE_LEVEL_10_0,
            D3D_FEATURE_LEVEL_9_3,
            D3D_FEATURE_LEVEL_9_1
        };

        ComPtr<ID3D11Device> d3dDevice;
        ComPtr<ID3D11DeviceContext> d3dDeviceContext;
        DX::ThrowIfFailed(
            D3D11CreateDevice(
                nullptr,                    // Specify nullptr to use the default adapter.
                D3D_DRIVER_TYPE_HARDWARE,
                nullptr,                    // leave as nullptr if hardware is used
                creationFlags,              // optionally set debug and Direct2D compatibility flags
                featureLevels,
                ARRAYSIZE(featureLevels),
                D3D11_SDK_VERSION,          // always set this to D3D11_SDK_VERSION
                &d3dDevice,
                nullptr,
                &d3dDeviceContext
                )
            );

        // Retrieve the Direct3D 11.1 interfaces.
        DX::ThrowIfFailed(
            d3dDevice.As(&m_d3dDevice)
            );

        DX::ThrowIfFailed(
            d3dDeviceContext.As(&m_d3dDeviceContext)
            );
```

### 3. Creating the swap chain

Next, we create a swap chain that the device uses for rendering and display. We declare and initialize a [**DXGI\_SWAP\_CHAIN\_DESC1**](https://msdn.microsoft.com/library/windows/desktop/hh404528) structure to describe the swap chain. Then, we set up the swap chain as flip-model (that is, a swap chain that has the [**DXGI\_SWAP\_EFFECT\_FLIP\_SEQUENTIAL**](https://msdn.microsoft.com/library/windows/desktop/bb173077#DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL) value set in the **SwapEffect** member) and set the **Format** member to [**DXGI\_FORMAT\_B8G8R8A8\_UNORM**](https://msdn.microsoft.com/library/windows/desktop/bb173059#DXGI_FORMAT_B8G8R8A8_UNORM). We set the **Count** member of the [**DXGI\_SAMPLE\_DESC**](https://msdn.microsoft.com/library/windows/desktop/bb173072) structure that the **SampleDesc** member specifies to 1 and the **Quality** member of **DXGI\_SAMPLE\_DESC** to zero because flip-model doesn’t support multiple sample antialiasing (MSAA). We set the **BufferCount** member to 2 so the swap chain can use a front buffer to present to the display device and a back buffer that serves as the render target.

We obtain the underlying DXGI device by querying the Direct3D 11.1 device. To minimize power consumption, which is important to do on battery-powered devices such as laptops and tablets, we call the [**IDXGIDevice1::SetMaximumFrameLatency**](https://msdn.microsoft.com/library/windows/desktop/ff471334) method with 1 as the maximum number of back buffer frames that DXGI can queue. This ensures that the app is rendered only after the vertical blank.

To finally create the swap chain, we need to get the parent factory from the DXGI device. We call [**IDXGIDevice::GetAdapter**](https://msdn.microsoft.com/library/windows/desktop/bb174531) to get the adapter for the device, and then call [**IDXGIObject::GetParent**](https://msdn.microsoft.com/library/windows/desktop/bb174542) on the adapter to get the parent factory ([**IDXGIFactory2**](https://msdn.microsoft.com/library/windows/desktop/hh404556)). To create the swap chain, we call [**IDXGIFactory2::CreateSwapChainForCoreWindow**](https://msdn.microsoft.com/library/windows/desktop/hh404559) with the swap-chain descriptor and the app’s core window.

```cpp
            // If the swap chain does not exist, create it.
            DXGI_SWAP_CHAIN_DESC1 swapChainDesc = {0};

            swapChainDesc.Stereo = false;
            swapChainDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
            swapChainDesc.Scaling = DXGI_SCALING_NONE;
            swapChainDesc.Flags = 0;

            // Use automatic sizing.
            swapChainDesc.Width = 0;
            swapChainDesc.Height = 0;

            // This is the most common swap chain format.
            swapChainDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;

            // Don't use multi-sampling.
            swapChainDesc.SampleDesc.Count = 1;
            swapChainDesc.SampleDesc.Quality = 0;

            // Use two buffers to enable the flip effect.
            swapChainDesc.BufferCount = 2;

            // We recommend using this swap effect for all applications.
            swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL;


            // Once the swap chain description is configured, it must be
            // created on the same adapter as the existing D3D Device.

            // First, retrieve the underlying DXGI Device from the D3D Device.
            ComPtr<IDXGIDevice2> dxgiDevice;
            DX::ThrowIfFailed(
                m_d3dDevice.As(&dxgiDevice)
                );

            // Ensure that DXGI does not queue more than one frame at a time. This both reduces
            // latency and ensures that the application will only render after each VSync, minimizing
            // power consumption.
            DX::ThrowIfFailed(
                dxgiDevice->SetMaximumFrameLatency(1)
                );

            // Next, get the parent factory from the DXGI Device.
            ComPtr<IDXGIAdapter> dxgiAdapter;
            DX::ThrowIfFailed(
                dxgiDevice->GetAdapter(&dxgiAdapter)
                );

            ComPtr<IDXGIFactory2> dxgiFactory;
            DX::ThrowIfFailed(
                dxgiAdapter->GetParent(IID_PPV_ARGS(&dxgiFactory))
                );

            // Finally, create the swap chain.
            CoreWindow^ window = m_window.Get();
            DX::ThrowIfFailed(
                dxgiFactory->CreateSwapChainForCoreWindow(
                    m_d3dDevice.Get(),
                    reinterpret_cast<IUnknown*>(window),
                    &swapChainDesc,
                    nullptr, // Allow on all displays.
                    &m_swapChain
                    )
                );
```

### 4. Creating the render-target view

To render graphics to the window, we need to create a render-target view. We call [**IDXGISwapChain::GetBuffer**](https://msdn.microsoft.com/library/windows/desktop/bb174570) to get the swap chain’s back buffer to use when we create the render-target view. We specify the back buffer as a 2D texture ([**ID3D11Texture2D**](https://msdn.microsoft.com/library/windows/desktop/ff476635)). To create the render-target view, we call [**ID3D11Device::CreateRenderTargetView**](https://msdn.microsoft.com/library/windows/desktop/ff476517) with the swap chain’s back buffer. We must specify to draw to the entire core window by specifying the view port ([**D3D11\_VIEWPORT**](https://msdn.microsoft.com/library/windows/desktop/ff476260)) as the full size of the swap chain's back buffer. We use the view port in a call to [**ID3D11DeviceContext::RSSetViewports**](https://msdn.microsoft.com/library/windows/desktop/ff476480) to bind the view port to the [rasterizer stage](https://msdn.microsoft.com/library/windows/desktop/bb205125) of the pipeline. The rasterizer stage converts vector information into a raster image. In this case, we don't require a conversion because we are just displaying a solid color.

```cpp
        // Once the swap chain is created, create a render target view.  This will
        // allow Direct3D to render graphics to the window.

        ComPtr<ID3D11Texture2D> backBuffer;
        DX::ThrowIfFailed(
            m_swapChain->GetBuffer(0, IID_PPV_ARGS(&backBuffer))
            );

        DX::ThrowIfFailed(
            m_d3dDevice->CreateRenderTargetView(
                backBuffer.Get(),
                nullptr,
                &m_renderTargetView
                )
            );


        // After the render target view is created, specify that the viewport,
        // which describes what portion of the window to draw to, should cover
        // the entire window.

        D3D11_TEXTURE2D_DESC backBufferDesc = {0};
        backBuffer->GetDesc(&backBufferDesc);

        D3D11_VIEWPORT viewport;
        viewport.TopLeftX = 0.0f;
        viewport.TopLeftY = 0.0f;
        viewport.Width = static_cast<float>(backBufferDesc.Width);
        viewport.Height = static_cast<float>(backBufferDesc.Height);
        viewport.MinDepth = D3D11_MIN_DEPTH;
        viewport.MaxDepth = D3D11_MAX_DEPTH;

        m_d3dDeviceContext->RSSetViewports(1, &viewport);
```

### 5. Presenting the rendered image

We enter an endless loop to continually render and display the scene.

In this loop, we call:

1.  [**ID3D11DeviceContext::OMSetRenderTargets**](https://msdn.microsoft.com/library/windows/desktop/ff476464) to specify the render target as the output target.
2.  [**ID3D11DeviceContext::ClearRenderTargetView**](https://msdn.microsoft.com/library/windows/desktop/ff476388) to clear the render target to a solid color.
3.  [**IDXGISwapChain::Present**](https://msdn.microsoft.com/library/windows/desktop/bb174576) to present the rendered image to the window.

Because we previously set the maximum frame latency to 1, Windows generally slows down the render loop to the screen refresh rate, typically around 60 Hz. Windows slows down the render loop by making the app sleep when the app calls [**Present**](https://msdn.microsoft.com/library/windows/desktop/bb174576). Windows makes the app sleep until the screen is refreshed.

```cpp
        // Enter the render loop.  Note that UWP apps should never exit.
        while (true)
        {
            // Process events incoming to the window.
            m_window->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

            // Specify the render target we created as the output target.
            m_d3dDeviceContext->OMSetRenderTargets(
                1,
                m_renderTargetView.GetAddressOf(),
                nullptr // Use no depth stencil.
                );

            // Clear the render target to a solid color.
            const float clearColor[4] = { 0.071f, 0.04f, 0.561f, 1.0f };
            m_d3dDeviceContext->ClearRenderTargetView(
                m_renderTargetView.Get(),
                clearColor
                );

            // Present the rendered image to the window.  Because the maximum frame latency is set to 1,
            // the render loop will generally be throttled to the screen refresh rate, typically around
            // 60 Hz, by sleeping the application on Present until the screen is refreshed.
            DX::ThrowIfFailed(
                m_swapChain->Present(1, 0)
                );
        }
```

### 6. Resizing the app window and the swap chain’s buffer

If the size of the app window changes, the app must resize the swap chain’s buffers, recreate the render-target view, and then present the resized rendered image. To resize the swap chain’s buffers, we call [**IDXGISwapChain::ResizeBuffers**](https://msdn.microsoft.com/library/windows/desktop/bb174577). In this call, we leave the number of buffers and the format of the buffers unchanged (the *BufferCount* parameter to two and the *NewFormat* parameter to [**DXGI\_FORMAT\_B8G8R8A8\_UNORM**](https://msdn.microsoft.com/library/windows/desktop/bb173059#DXGI_FORMAT_B8G8R8A8_UNORM)). We make the size of the swap chain’s back buffer the same size as the resized window. After we resize the swap chain’s buffers, we create the new render target and present the new rendered image similarly to when we initialized the app.

```cpp
            // If the swap chain already exists, resize it.
            DX::ThrowIfFailed(
                m_swapChain->ResizeBuffers(
                    2,
                    0,
                    0,
                    DXGI_FORMAT_B8G8R8A8_UNORM,
                    0
                    )
                );
```

## Summary and next steps


We created a Direct3D device, swap chain, and render-target view, and presented the rendered image to the display.

Next, we also draw a triangle on the display.

[Creating shaders and drawing primitives](creating-shaders-and-drawing-primitives.md)

 

 




