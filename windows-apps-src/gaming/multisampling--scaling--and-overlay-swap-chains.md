---
title: Swap chain scaling and overlays
description: Learn how to create scaled swap chains for faster rendering on mobile devices, and use overlay swap chains (when available) to increase the visual quality.
ms.assetid: 3e4d2d19-cac3-eebc-52dd-daa7a7bc30d1
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, swap chain scaling, overlays, directx
ms.localizationpriority: medium
---
# Swap chain scaling and overlays



Learn how to create scaled swap chains for faster rendering on mobile devices, and use overlay swap chains (when available) to increase the visual quality.

## Swap chains in DirectX 11.2


Direct3D 11.2 allows you to create Universal Windows Platform (UWP) apps with swap chains that are scaled up from non-native (reduced) resolutions, enabling faster fill rates. Direct3D 11.2 also includes APIs for rendering with hardware overlays so that you can present a UI in another swap chain at native resolution. This allows your game to draw UI at full native resolution while maintaining a high framerate, thereby making the best use of mobile devices and high DPI displays (such as 3840 by 2160). This article explains how to use overlapping swap chains.

Direct3D 11.2 also introduces a new feature for reduced latency with flip model swap chains. See [Reduce latency with DXGI 1.3 swap chains](reduce-latency-with-dxgi-1-3-swap-chains.md).

## Use swap chain scaling


When your game is running on downlevel hardware - or hardware optimized for power savings - it can be beneficial to render real-time game content at a lower resolution than the display is natively capable of. To do this, the swap chain that is used for rendering game content must be smaller than the native resolution, or a subregion of the swapchain must be used.

1.  First, create a swap chain at full native resolution.

    ```cpp
    DXGI_SWAP_CHAIN_DESC1 swapChainDesc = {0};

    swapChainDesc.Width = static_cast<UINT>(m_d3dRenderTargetSize.Width); // Match the size of the window.
    swapChainDesc.Height = static_cast<UINT>(m_d3dRenderTargetSize.Height);
    swapChainDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM; // This is the most common swap chain format.
    swapChainDesc.Stereo = false;
    swapChainDesc.SampleDesc.Count = 1; // Don't use multi-sampling.
    swapChainDesc.SampleDesc.Quality = 0;
    swapChainDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
    swapChainDesc.BufferCount = 2; // Use double-buffering to minimize latency.
    swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL; // All UWP apps must use this SwapEffect.
    swapChainDesc.Flags = 0;
    swapChainDesc.Scaling = DXGI_SCALING_STRETCH;

    // This sequence obtains the DXGI factory that was used to create the Direct3D device above.
    ComPtr<IDXGIDevice3> dxgiDevice;
    DX::ThrowIfFailed(
        m_d3dDevice.As(&dxgiDevice)
        );

    ComPtr<IDXGIAdapter> dxgiAdapter;
    DX::ThrowIfFailed(
        dxgiDevice->GetAdapter(&dxgiAdapter)
        );

    ComPtr<IDXGIFactory2> dxgiFactory;
    DX::ThrowIfFailed(
        dxgiAdapter->GetParent(IID_PPV_ARGS(&dxgiFactory))
        );

    ComPtr<IDXGISwapChain1> swapChain;
    DX::ThrowIfFailed(
        dxgiFactory->CreateSwapChainForCoreWindow(
            m_d3dDevice.Get(),
            reinterpret_cast<IUnknown*>(m_window.Get()),
            &swapChainDesc,
            nullptr,
            &swapChain
            )
        );

    DX::ThrowIfFailed(
        swapChain.As(&m_swapChain)
        );
    ```

2.  Then, choose a subregion of the swap chain to scale up by setting the source size to a reduced resolution.

    The DX Foreground Swap Chains sample calculates a reduced size based on a percentage:

    ```cpp
    m_d3dRenderSizePercentage = percentage;

    UINT renderWidth = static_cast<UINT>(m_d3dRenderTargetSize.Width * percentage + 0.5f);
    UINT renderHeight = static_cast<UINT>(m_d3dRenderTargetSize.Height * percentage + 0.5f);

    // Change the region of the swap chain that will be presented to the screen.
    DX::ThrowIfFailed(
        m_swapChain->SetSourceSize(
            renderWidth,
            renderHeight
            )
        );
    ```

3.  Create a viewport to match the subregion of the swap chain.

    ```cpp
    // In Direct3D, change the Viewport to match the region of the swap
    // chain that will now be presented from.
    m_screenViewport = CD3D11_VIEWPORT(
        0.0f,
        0.0f,
        static_cast<float>(renderWidth),
        static_cast<float>(renderHeight)
        );

    m_d3dContext->RSSetViewports(1, &m_screenViewport);
    ```

4.  If Direct2D is being used, the rotation transform needs to be adjusted to compensate for the source region.

## Create a hardware overlay swap chain for UI elements


When using swap chain scaling, there is an inherent disadvantage in that the UI is also scaled down, potentially making it blurry and harder to use. On devices with hardware support for overlay swap chains, this problem is alleviated entirely by rendering the UI at native resolution in a swap chain that's separate from the real-time game content. Note that this technique applies only to [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) swap chains - it cannot be used with XAML interop.

Use the following steps to create a foreground swap chain that uses hardware overlay capability. These steps are performed after first creating a swap chain for real-time game content as described above.

1.  First, determine whether the DXGI adapter supports overlays. Get the DXGI output adapter from the swap chain:

    ```cpp
    ComPtr<IDXGIAdapter> outputDxgiAdapter;
    DX::ThrowIfFailed(
        dxgiFactory->EnumAdapters(0, &outputDxgiAdapter)
        );

    ComPtr<IDXGIOutput> dxgiOutput;
    DX::ThrowIfFailed(
        outputDxgiAdapter->EnumOutputs(0, &dxgiOutput)
        );

    ComPtr<IDXGIOutput2> dxgiOutput2;
    DX::ThrowIfFailed(
        dxgiOutput.As(&dxgiOutput2)
        );
    ```

    The DXGI adapter supports overlays if the output adapter returns True for [**SupportsOverlays**](/windows/desktop/api/dxgi1_3/nf-dxgi1_3-idxgioutput2-supportsoverlays).

    ```cpp
    m_overlaySupportExists = dxgiOutput2->SupportsOverlays() ? true : false;
    ```
    
    > **Note**   If the DXGI adapter supports overlays, continue to the next step. If the device does not support overlays, rendering with multiple swap chains will not be efficient. Instead, render the UI at reduced resolution in the same swap chain as real-time game content.

     

2.  Create the foreground swap chain with [**IDXGIFactory2::CreateSwapChainForCoreWindow**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgifactory2-createswapchainforcorewindow). The following options must be set in the [**DXGI\_SWAP\_CHAIN\_DESC1**](/windows/desktop/api/dxgi1_2/ns-dxgi1_2-dxgi_swap_chain_desc1) supplied to the *pDesc* parameter:

    -   Specify the [**DXGI\_SWAP\_CHAIN\_FLAG\_FOREGROUND\_LAYER**](/windows/desktop/api/dxgi/ne-dxgi-dxgi_swap_chain_flag) swap chain flag to indicate a foreground swap chain.
    -   Use the [**DXGI\_ALPHA\_MODE\_PREMULTIPLIED**](/windows/desktop/api/dxgi1_2/ne-dxgi1_2-dxgi_alpha_mode) alpha mode flag. Foreground swap chains are always premultiplied.
    -   Set the [**DXGI\_SCALING\_NONE**](/windows/desktop/api/dxgi1_2/ne-dxgi1_2-dxgi_scaling) flag. Foreground swap chains always run at native resolution.

    ```cpp
     foregroundSwapChainDesc.Flags = DXGI_SWAP_CHAIN_FLAG_FOREGROUND_LAYER;
     foregroundSwapChainDesc.Scaling = DXGI_SCALING_NONE;
     foregroundSwapChainDesc.AlphaMode = DXGI_ALPHA_MODE_PREMULTIPLIED; // Foreground swap chain alpha values must be premultiplied.
    ```

    > **Note**   Set the [**DXGI\_SWAP\_CHAIN\_FLAG\_FOREGROUND\_LAYER**](/windows/desktop/api/dxgi/ne-dxgi-dxgi_swap_chain_flag) again every time the swap chain is resized.

    ```cpp
    HRESULT hr = m_foregroundSwapChain->ResizeBuffers(
        2, // Double-buffered swap chain.
        static_cast<UINT>(m_d3dRenderTargetSize.Width),
        static_cast<UINT>(m_d3dRenderTargetSize.Height),
        DXGI_FORMAT_B8G8R8A8_UNORM,
        DXGI_SWAP_CHAIN_FLAG_FOREGROUND_LAYER // The FOREGROUND_LAYER flag cannot be removed with ResizeBuffers.
        );
    ```

3.  When two swap chains are being used, increase the maximum frame latency to 2 so that the DXGI adapter has time to present both swap chains simultaneously (within the same VSync interval).

    ```cpp
    // Create a render target view of the foreground swap chain's back buffer.
    if (m_foregroundSwapChain)
    {
        ComPtr<ID3D11Texture2D> foregroundBackBuffer;
        DX::ThrowIfFailed(
            m_foregroundSwapChain->GetBuffer(0, IID_PPV_ARGS(&foregroundBackBuffer))
            );

        DX::ThrowIfFailed(
            m_d3dDevice->CreateRenderTargetView(
                foregroundBackBuffer.Get(),
                nullptr,
                &m_d3dForegroundRenderTargetView
                )
            );
    }
    ```

4.  Foreground swap chains always use premultiplied alpha. Each pixel's color values are expected to be already multiplied by the alpha value before the frame is presented. For example, a 100% white BGRA pixel at 50% alpha is set to (0.5, 0.5, 0.5, 0.5).

    The alpha premultiplication step can be done in the output-merger stage by applying an app blend state (see [**ID3D11BlendState**](/windows/desktop/api/d3d11/nn-d3d11-id3d11blendstate)) with the [**D3D11\_RENDER\_TARGET\_BLEND\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_render_target_blend_desc) structure's **SrcBlend** field set to **D3D11\_SRC\_ALPHA**. Assets with pre-multiplied alpha values can also be used.

    If the alpha premultiplication step is not done, colors on the foreground swap chain will be brighter than expected.

5.  Depending on whether the foreground swap chain was created, the Direct2D drawing surface for UI elements might need be associated with the foreground swap chain.

    Creating render target views:

    ```cpp
    // Create a render target view of the foreground swap chain's back buffer.
    if (m_foregroundSwapChain)
    {
        ComPtr<ID3D11Texture2D> foregroundBackBuffer;
        DX::ThrowIfFailed(
            m_foregroundSwapChain->GetBuffer(0, IID_PPV_ARGS(&foregroundBackBuffer))
            );

        DX::ThrowIfFailed(
            m_d3dDevice->CreateRenderTargetView(
                foregroundBackBuffer.Get(),
                nullptr,
                &m_d3dForegroundRenderTargetView
                )
            );
    }
    ```

    Creating the Direct2D drawing surface:

    ```cpp
    if (m_foregroundSwapChain)
    {
        // Create a Direct2D target bitmap for the foreground swap chain.
        ComPtr<IDXGISurface2> dxgiForegroundSwapChainBackBuffer;
        DX::ThrowIfFailed(
            m_foregroundSwapChain->GetBuffer(0, IID_PPV_ARGS(&dxgiForegroundSwapChainBackBuffer))
            );

        DX::ThrowIfFailed(
            m_d2dContext->CreateBitmapFromDxgiSurface(
                dxgiForegroundSwapChainBackBuffer.Get(),
                &bitmapProperties,
                &m_d2dTargetBitmap
                )
            );
    }
    else
    {
        // Create a Direct2D target bitmap for the swap chain.
        ComPtr<IDXGISurface2> dxgiSwapChainBackBuffer;
        DX::ThrowIfFailed(
            m_swapChain->GetBuffer(0, IID_PPV_ARGS(&dxgiSwapChainBackBuffer))
            );

        DX::ThrowIfFailed(
            m_d2dContext->CreateBitmapFromDxgiSurface(
                dxgiSwapChainBackBuffer.Get(),
                &bitmapProperties,
                &m_d2dTargetBitmap
                )
            );
    }

    m_d2dContext->SetTarget(m_d2dTargetBitmap.Get());
    ```

    ```cpp
    // Create a render target view of the swap chain's back buffer.
    ComPtr<ID3D11Texture2D> backBuffer;
    DX::ThrowIfFailed(
        m_swapChain->GetBuffer(0, IID_PPV_ARGS(&backBuffer))
        );

    DX::ThrowIfFailed(
        m_d3dDevice->CreateRenderTargetView(
            backBuffer.Get(),
            nullptr,
            &m_d3dRenderTargetView
            )
        );
    ```

6.  Present the foreground swap chain together with the scaled swap chain used for real-time game content. Since frame latency was set to 2 for both swap chains, DXGI can present them both within the same VSync interval.

    ```cpp
    // Present the contents of the swap chain to the screen.
    void DX::DeviceResources::Present()
    {
        // The first argument instructs DXGI to block until VSync, putting the application
        // to sleep until the next VSync. This ensures that we don't waste any cycles rendering
        // frames that will never be displayed to the screen.
        HRESULT hr = m_swapChain->Present(1, 0);

        if (SUCCEEDED(hr) && m_foregroundSwapChain)
        {
            m_foregroundSwapChain->Present(1, 0);
        }

        // Discard the contents of the render targets.
        // This is a valid operation only when the existing contents will be entirely
        // overwritten. If dirty or scroll rects are used, this call should be removed.
        m_d3dContext->DiscardView(m_d3dRenderTargetView.Get());
        if (m_foregroundSwapChain)
        {
            m_d3dContext->DiscardView(m_d3dForegroundRenderTargetView.Get());
        }

        // Discard the contents of the depth stencil.
        m_d3dContext->DiscardView(m_d3dDepthStencilView.Get());

        // If the device was removed either by a disconnection or a driver upgrade, we
        // must recreate all device resources.
        if (hr == DXGI_ERROR_DEVICE_REMOVED)
        {
            HandleDeviceLost();
        }
        else
        {
            DX::ThrowIfFailed(hr);
        }
    }
    ```

 

 