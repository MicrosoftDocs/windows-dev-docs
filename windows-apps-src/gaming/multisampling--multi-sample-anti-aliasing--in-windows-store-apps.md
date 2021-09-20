---
title: Multisampling in UWP apps
description: Learn how to use multisampling in Universal Windows Platform (UWP) apps built with Direct3D.
ms.assetid: 1cd482b8-32ff-1eb0-4c91-83eb52f08484
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, multisampling, direct3d
ms.localizationpriority: medium
---
# <span id="dev_gaming.multisampling__multi-sample_anti_aliasing__in_windows_store_apps"></span> Multisampling in Universal Windows Platform (UWP) apps



Learn how to use multisampling in Universal Windows Platform (UWP) apps built with Direct3D. Multisampling, also known as multi-sample antialiasing, is a graphics technique used to reduce the appearance of aliased edges. It works by drawing more pixels than are actually in the final render target, then averaging values to maintain the appearance of a "partial" edge in certain pixels. For a detailed description of how multisampling actually works in Direct3D, see [Multisample Anti-Aliasing Rasterization Rules](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-rasterizer-stage-rules).

## Multisampling and the flip model swap chain


UWP apps that use DirectX must use flip model swap chains. Flip model swap chains don't support multisampling directly, but multisampling can still be applied in a different way by rendering the scene to a multisampled render target view, and then resolving the multisampled render target to the back buffer before presenting. This article explains the steps required to add multisampling to your UWP app.

### How to use multisampling

Direct3D feature levels guarantee support for specific, minimum sample count capabilities, and guarantee certain buffer formats will be available that support multisampling. Graphics devices often support a wider range of formats and sample counts than the minimum required. Multisampling support can be determined at run-time by checking feature support for multisampling with specific DXGI formats, and then checking the sample counts you can use with each supported format.

1.  Call [**ID3D11Device::CheckFeatureSupport**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-checkfeaturesupport) to find out which DXGI formats can be used with multisampling. Supply the render target formats your game can use. Both the render target and resolve target must use the same format, so check for both [**D3D11\_FORMAT\_SUPPORT\_MULTISAMPLE\_RENDERTARGET**](/windows/desktop/api/d3d11/ne-d3d11-d3d11_format_support) and **D3D11\_FORMAT\_SUPPORT\_MULTISAMPLE\_RESOLVE**.

    **Feature level 9:  ** Although feature level 9 devices [guarantee support for multisampled render target formats](/previous-versions/ff471324(v=vs.85)), support is not guaranteed for multisample resolve targets. So this check is necessary before trying to use the multisampling technique described in this topic.

    The following code checks multisampling support for all the DXGI\_FORMAT values:

    ```cpp
    // Determine the format support for multisampling.
    for (UINT i = 1; i < DXGI_FORMAT_MAX; i++)
    {
        DXGI_FORMAT inFormat = safe_cast<DXGI_FORMAT>(i);
        UINT formatSupport = 0;
        HRESULT hr = m_d3dDevice->CheckFormatSupport(inFormat, &formatSupport);

        if ((formatSupport & D3D11_FORMAT_SUPPORT_MULTISAMPLE_RESOLVE) &&
            (formatSupport & D3D11_FORMAT_SUPPORT_MULTISAMPLE_RENDERTARGET)
            )
        {
            m_supportInfo->SetFormatSupport(i, true);
        }
        else
        {
            m_supportInfo->SetFormatSupport(i, false);
        }
    }
    ```

2.  For each supported format, query for sample count support by calling [**ID3D11Device::CheckMultisampleQualityLevels**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-checkmultisamplequalitylevels).

    The following code checks sample size support for supported DXGI formats:

    ```cpp
    // Find available sample sizes for each supported format.
    for (unsigned int i = 0; i < DXGI_FORMAT_MAX; i++)
    {
        for (unsigned int j = 1; j < MAX_SAMPLES_CHECK; j++)
        {
            UINT numQualityFlags;

            HRESULT test = m_d3dDevice->CheckMultisampleQualityLevels(
                (DXGI_FORMAT) i,
                j,
                &numQualityFlags
                );

            if (SUCCEEDED(test) && (numQualityFlags > 0))
            {
                m_supportInfo->SetSampleSize(i, j, 1);
                m_supportInfo->SetQualityFlagsAt(i, j, numQualityFlags);
            }
        }
    }
    ```

    > **Note**   Use [**ID3D11Device2::CheckMultisampleQualityLevels1**](/windows/desktop/api/d3d11_2/nf-d3d11_2-id3d11device2-checkmultisamplequalitylevels1) instead if you need to check multisample support for tiled resource buffers.

     

3.  Create a buffer and render target view with the desired sample count. Use the same DXGI\_FORMAT, width, and height as the swap chain, but specify a sample count greater than 1 and use a multisampled texture dimension (**D3D11\_RTV\_DIMENSION\_TEXTURE2DMS** for example). If necessary, you can re-create the swap chain with new settings that are optimal for multisampling.

    The following code creates a multisampled render target:

    ```cpp
    float widthMulti = m_d3dRenderTargetSize.Width;
    float heightMulti = m_d3dRenderTargetSize.Height;

    D3D11_TEXTURE2D_DESC offScreenSurfaceDesc;
    ZeroMemory(&offScreenSurfaceDesc, sizeof(D3D11_TEXTURE2D_DESC));

    offScreenSurfaceDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
    offScreenSurfaceDesc.Width = static_cast<UINT>(widthMulti);
    offScreenSurfaceDesc.Height = static_cast<UINT>(heightMulti);
    offScreenSurfaceDesc.BindFlags = D3D11_BIND_RENDER_TARGET;
    offScreenSurfaceDesc.MipLevels = 1;
    offScreenSurfaceDesc.ArraySize = 1;
    offScreenSurfaceDesc.SampleDesc.Count = m_sampleSize;
    offScreenSurfaceDesc.SampleDesc.Quality = m_qualityFlags;

    // Create a surface that's multisampled.
    DX::ThrowIfFailed(
        m_d3dDevice->CreateTexture2D(
        &offScreenSurfaceDesc,
        nullptr,
        &m_offScreenSurface)
        );

    // Create a render target view. 
    CD3D11_RENDER_TARGET_VIEW_DESC renderTargetViewDesc(D3D11_RTV_DIMENSION_TEXTURE2DMS);
    DX::ThrowIfFailed(
        m_d3dDevice->CreateRenderTargetView(
        m_offScreenSurface.Get(),
        &renderTargetViewDesc,
        &m_d3dRenderTargetView
        )
        );
    ```

4.  The depth buffer must have the same width, height, sample count, and texture dimension to match the multisampled render target.

    The following code creates a multisampled depth buffer:

    ```cpp
    // Create a depth stencil view for use with 3D rendering if needed.
    CD3D11_TEXTURE2D_DESC depthStencilDesc(
        DXGI_FORMAT_D24_UNORM_S8_UINT,
        static_cast<UINT>(widthMulti),
        static_cast<UINT>(heightMulti),
        1, // This depth stencil view has only one texture.
        1, // Use a single mipmap level.
        D3D11_BIND_DEPTH_STENCIL,
        D3D11_USAGE_DEFAULT,
        0,
        m_sampleSize,
        m_qualityFlags
        );

    ComPtr<ID3D11Texture2D> depthStencil;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateTexture2D(
        &depthStencilDesc,
        nullptr,
        &depthStencil
        )
        );

    CD3D11_DEPTH_STENCIL_VIEW_DESC depthStencilViewDesc(D3D11_DSV_DIMENSION_TEXTURE2DMS);
    DX::ThrowIfFailed(
        m_d3dDevice->CreateDepthStencilView(
        depthStencil.Get(),
        &depthStencilViewDesc,
        &m_d3dDepthStencilView
        )
        );
    ```

5.  Now is a good time to create the viewport, because the viewport width and height must also match the render target.

    The following code creates a viewport:

    ```cpp
    // Set the 3D rendering viewport to target the entire window.
    m_screenViewport = CD3D11_VIEWPORT(
        0.0f,
        0.0f,
        widthMulti / m_scalingFactor,
        heightMulti / m_scalingFactor
        );

    m_d3dContext->RSSetViewports(1, &m_screenViewport);
    ```

6.  Render each frame to the multisampled render target. When rendering is complete, call [**ID3D11DeviceContext::ResolveSubresource**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-resolvesubresource) before presenting the frame. This instructs Direct3D to peform the multisampling operation, computing the value of each pixel for display and placing the result in the back buffer. The back buffer then contains the final anti-aliased image and can be presented.

    The following code resolves the subresource before presenting the frame:

    ```cpp
    if (m_sampleSize > 1)
    {
        unsigned int sub = D3D11CalcSubresource(0, 0, 1);

        m_d3dContext->ResolveSubresource(
            m_backBuffer.Get(),
            sub,
            m_offScreenSurface.Get(),
            sub,
            DXGI_FORMAT_B8G8R8A8_UNORM
            );
    }

    // The first argument instructs DXGI to block until VSync, putting the application
    // to sleep until the next VSync. This ensures that we don't waste any cycles rendering
    // frames that will never be displayed to the screen.
    hr = m_swapChain->Present(1, 0);
    ```

 

 