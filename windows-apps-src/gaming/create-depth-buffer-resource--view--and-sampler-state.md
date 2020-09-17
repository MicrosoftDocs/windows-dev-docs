---
title: Create depth buffer device resources
description: Learn how to create the Direct3D device resources necessary to support depth testing for shadow volumes.
ms.assetid: 86d5791b-1faa-17e4-44a8-bbba07062756
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, direct3d, depth buffer
ms.localizationpriority: medium
---
# Create depth buffer device resources




Learn how to create the Direct3D device resources necessary to support depth testing for shadow volumes. Part 1 of [Walkthrough: Implement shadow volumes using depth buffers in Direct3D 11](implementing-depth-buffers-for-shadow-mapping.md).

## Resources you'll need


Rendering a depth map for shadow volumes requires the following Direct3D device-dependent resources:

-   A resource (buffer) for the depth map
-   A depth stencil view and shader resource view for the resource
-   A comparison sampler state object
-   Constant buffers for light POV matrices
-   A viewport for rendering the shadow map (typically a square viewport)
-   A rendering state object to enable front face culling
-   You will also need a rendering state object to switch back to back face culling, if you don't already use one.

Note that creation of these resources needs to be included in a device-dependent resource creation routine, that way your renderer can recreate them if (for example) a new device driver is installed, or the user moves your app to a monitor attached to a different graphics adapter.

## Check feature support


Before creating the depth map, call the [**CheckFeatureSupport**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-checkfeaturesupport) method on the Direct3D device, request **D3D11\_FEATURE\_D3D9\_SHADOW\_SUPPORT**, and provide a [**D3D11\_FEATURE\_DATA\_D3D9\_SHADOW\_SUPPORT**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_feature_data_d3d9_shadow_support) structure.

```cpp
D3D11_FEATURE_DATA_D3D9_SHADOW_SUPPORT isD3D9ShadowSupported;
ZeroMemory(&isD3D9ShadowSupported, sizeof(isD3D9ShadowSupported));
pD3DDevice->CheckFeatureSupport(
    D3D11_FEATURE_D3D9_SHADOW_SUPPORT,
    &isD3D9ShadowSupported,
    sizeof(D3D11_FEATURE_D3D9_SHADOW_SUPPORT)
    );

if (isD3D9ShadowSupported.SupportsDepthAsTextureWithLessEqualComparisonFilter)
{
    // Init shadow map resources

```

If this feature is not supported, do not try to load shaders compiled for shader model 4 level 9\_x that call sample comparison functions. In many cases, lack of support for this feature means that the GPU is a legacy device with a driver that isn't updated to support at least WDDM 1.2. If the device supports at least feature level 10\_0 then you can load a sample comparison shader compiled for shader model 4\_0 instead.

## Create depth buffer


First, try creating the depth map with a higher-precision depth format. Set up matching shader resource view properties first. If the resource creation fails, for example due to low device memory or a format that the hardware doesn't support, try a lower-precision format and change properties to match.

This step is optional if you only need a low-precision depth format, for example when rendering on medium-resolution Direct3D feature level 9\_1 devices.

```cpp
D3D11_TEXTURE2D_DESC shadowMapDesc;
ZeroMemory(&shadowMapDesc, sizeof(D3D11_TEXTURE2D_DESC));
shadowMapDesc.Format = DXGI_FORMAT_R24G8_TYPELESS;
shadowMapDesc.MipLevels = 1;
shadowMapDesc.ArraySize = 1;
shadowMapDesc.SampleDesc.Count = 1;
shadowMapDesc.BindFlags = D3D11_BIND_SHADER_RESOURCE | D3D11_BIND_DEPTH_STENCIL;
shadowMapDesc.Height = static_cast<UINT>(m_shadowMapDimension);
shadowMapDesc.Width = static_cast<UINT>(m_shadowMapDimension);

HRESULT hr = pD3DDevice->CreateTexture2D(
    &shadowMapDesc,
    nullptr,
    &m_shadowMap
    );
```

Then create the resource views. Set the mip slice to zero on the depth stencil view and set mip levels to 1 on the shader resource view. Both have a texture dimension of TEXTURE2D, and both need to use a matching [**DXGI\_FORMAT**](/windows/desktop/api/dxgiformat/ne-dxgiformat-dxgi_format).

```cpp
D3D11_DEPTH_STENCIL_VIEW_DESC depthStencilViewDesc;
ZeroMemory(&depthStencilViewDesc, sizeof(D3D11_DEPTH_STENCIL_VIEW_DESC));
depthStencilViewDesc.Format = DXGI_FORMAT_D24_UNORM_S8_UINT;
depthStencilViewDesc.ViewDimension = D3D11_DSV_DIMENSION_TEXTURE2D;
depthStencilViewDesc.Texture2D.MipSlice = 0;

D3D11_SHADER_RESOURCE_VIEW_DESC shaderResourceViewDesc;
ZeroMemory(&shaderResourceViewDesc, sizeof(D3D11_SHADER_RESOURCE_VIEW_DESC));
shaderResourceViewDesc.ViewDimension = D3D11_SRV_DIMENSION_TEXTURE2D;
shaderResourceViewDesc.Format = DXGI_FORMAT_R24_UNORM_X8_TYPELESS;
shaderResourceViewDesc.Texture2D.MipLevels = 1;

hr = pD3DDevice->CreateDepthStencilView(
    m_shadowMap.Get(),
    &depthStencilViewDesc,
    &m_shadowDepthView
    );

hr = pD3DDevice->CreateShaderResourceView(
    m_shadowMap.Get(),
    &shaderResourceViewDesc,
    &m_shadowResourceView
    );
```

## Create comparison state


Now create the comparison sampler state object. Feature level 9\_1 only supports D3D11\_COMPARISON\_LESS\_EQUAL. Filtering choices are explained more in [Supporting shadow maps on a range of hardware](target-a-range-of-hardware.md) - or you can just pick point filtering for faster shadow maps.

Note that you can specify the D3D11\_TEXTURE\_ADDRESS\_BORDER address mode and it will work on feature level 9\_1 devices. This applies to pixel shaders that don't test whether the pixel is in the light's view frustum before doing the depth test. By specifying 0 or 1 for each border, you can control whether pixels outside the light's view frustum pass or fail the depth test, and therefore whether they are lit or in shadow.

On feature level 9\_1, the following required values must be set: **MinLOD** is set to zero, **MaxLOD** is set to **D3D11\_FLOAT32\_MAX**, and **MaxAnisotropy** is set to zero.

```cpp
D3D11_SAMPLER_DESC comparisonSamplerDesc;
ZeroMemory(&comparisonSamplerDesc, sizeof(D3D11_SAMPLER_DESC));
comparisonSamplerDesc.AddressU = D3D11_TEXTURE_ADDRESS_BORDER;
comparisonSamplerDesc.AddressV = D3D11_TEXTURE_ADDRESS_BORDER;
comparisonSamplerDesc.AddressW = D3D11_TEXTURE_ADDRESS_BORDER;
comparisonSamplerDesc.BorderColor[0] = 1.0f;
comparisonSamplerDesc.BorderColor[1] = 1.0f;
comparisonSamplerDesc.BorderColor[2] = 1.0f;
comparisonSamplerDesc.BorderColor[3] = 1.0f;
comparisonSamplerDesc.MinLOD = 0.f;
comparisonSamplerDesc.MaxLOD = D3D11_FLOAT32_MAX;
comparisonSamplerDesc.MipLODBias = 0.f;
comparisonSamplerDesc.MaxAnisotropy = 0;
comparisonSamplerDesc.ComparisonFunc = D3D11_COMPARISON_LESS_EQUAL;
comparisonSamplerDesc.Filter = D3D11_FILTER_COMPARISON_MIN_MAG_MIP_POINT;

// Point filtered shadows can be faster, and may be a good choice when
// rendering on hardware with lower feature levels. This sample has a
// UI option to enable/disable filtering so you can see the difference
// in quality and speed.

DX::ThrowIfFailed(
    pD3DDevice->CreateSamplerState(
        &comparisonSamplerDesc,
        &m_comparisonSampler_point
        )
    );
```

## Create render states


Now create a render state you can use to enable front face culling. Note that feature level 9\_1 devices require **DepthClipEnable** set to **true**.

```cpp
D3D11_RASTERIZER_DESC drawingRenderStateDesc;
ZeroMemory(&drawingRenderStateDesc, sizeof(D3D11_RASTERIZER_DESC));
drawingRenderStateDesc.CullMode = D3D11_CULL_BACK;
drawingRenderStateDesc.FillMode = D3D11_FILL_SOLID;
drawingRenderStateDesc.DepthClipEnable = true; // Feature level 9_1 requires DepthClipEnable == true
DX::ThrowIfFailed(
    pD3DDevice->CreateRasterizerState(
        &drawingRenderStateDesc,
        &m_drawingRenderState
        )
    );
```

Create a render state you can use to enable back face culling. If your rendering code already turns on back face culling, then you can skip this step.

```cpp
D3D11_RASTERIZER_DESC shadowRenderStateDesc;
ZeroMemory(&shadowRenderStateDesc, sizeof(D3D11_RASTERIZER_DESC));
shadowRenderStateDesc.CullMode = D3D11_CULL_FRONT;
shadowRenderStateDesc.FillMode = D3D11_FILL_SOLID;
shadowRenderStateDesc.DepthClipEnable = true;

DX::ThrowIfFailed(
    pD3DDevice->CreateRasterizerState(
        &shadowRenderStateDesc,
        &m_shadowRenderState
        )
    );
```

## Create constant buffers


Don't forget to create a constant buffer for rendering from the light's point of view. You can also use this constant buffer to specify the light position to the shader. Use a perspective matrix for point lights, and use an orthogonal matrix for directional lights (such as sunlight).

```cpp
DX::ThrowIfFailed(
    m_deviceResources->GetD3DDevice()->CreateBuffer(
        &viewProjectionConstantBufferDesc,
        nullptr,
        &m_lightViewProjectionBuffer
        )
    );
```

Fill the constant buffer data. Update the constant buffers once during initialization, and again if the light values have changed since the previous frame.

```cpp
{
    XMMATRIX lightPerspectiveMatrix = XMMatrixPerspectiveFovRH(
        XM_PIDIV2,
        1.0f,
        12.f,
        50.f
        );

    XMStoreFloat4x4(
        &m_lightViewProjectionBufferData.projection,
        XMMatrixTranspose(lightPerspectiveMatrix)
        );

    // Point light at (20, 15, 20), pointed at the origin. POV up-vector is along the y-axis.
    static const XMVECTORF32 eye = { 20.0f, 15.0f, 20.0f, 0.0f };
    static const XMVECTORF32 at = { 0.0f, 0.0f, 0.0f, 0.0f };
    static const XMVECTORF32 up = { 0.0f, 1.0f, 0.0f, 0.0f };

    XMStoreFloat4x4(
        &m_lightViewProjectionBufferData.view,
        XMMatrixTranspose(XMMatrixLookAtRH(eye, at, up))
        );

    // Store the light position to help calculate the shadow offset.
    XMStoreFloat4(&m_lightViewProjectionBufferData.pos, eye);
}
```

```cpp
context->UpdateSubresource(
    m_lightViewProjectionBuffer.Get(),
    0,
    NULL,
    &m_lightViewProjectionBufferData,
    0,
    0
    );
```

## Create a viewport


You need a separate viewport to render to the shadow map. The viewport isn't a device-based resource; you're free to create it elsewhere in your code. Creating the viewport along with the shadow map can help make it more convenient to keep the dimension of the viewport congruent with the shadow map dimension.

```cpp
// Init viewport for shadow rendering
ZeroMemory(&m_shadowViewport, sizeof(D3D11_VIEWPORT));
m_shadowViewport.Height = m_shadowMapDimension;
m_shadowViewport.Width = m_shadowMapDimension;
m_shadowViewport.MinDepth = 0.f;
m_shadowViewport.MaxDepth = 1.f;
```

In the next part of this walkthrough, learn how to create the shadow map by [rendering to the depth buffer](render-the-shadow-map-to-the-depth-buffer.md).

 

 