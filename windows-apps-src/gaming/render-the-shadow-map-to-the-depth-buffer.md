---
title: Render the shadow map to the depth buffer
description: Render from the point of view of the light to create a two-dimensional depth map representing the shadow volume.
ms.assetid: 7f3d0208-c379-8871-cc48-027047c6c2d0
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, rendering, shadow map, depth buffer, direct3d
ms.localizationpriority: medium
---
# Render the shadow map to the depth buffer




Render from the point of view of the light to create a two-dimensional depth map representing the shadow volume. The depth map masks the space that will be rendered in shadow. Part 2 of [Walkthrough: Implement shadow volumes using depth buffers in Direct3D 11](implementing-depth-buffers-for-shadow-mapping.md).

## Clear the depth buffer


Always clear the depth buffer before rendering to it.

```cpp
context->ClearRenderTargetView(m_deviceResources->GetBackBufferRenderTargetView(), DirectX::Colors::CornflowerBlue);
context->ClearDepthStencilView(m_shadowDepthView.Get(), D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0);
```

## Render the shadow map to the depth buffer


For the shadow rendering pass, specify a depth buffer but do not specify a render target.

Specify the light viewport, a vertex shader, and set the light space constant buffers. Use front face culling for this pass to optimize the depth values placed in the shadow buffer.

Note that on most devices, you can specify nullptr for the pixel shader (or skip specifying a pixel shader entirely). But some drivers may throw an exception when you call draw on the Direct3D device with a null pixel shader set. To avoid this exception, you can set a minimal pixel shader for the shadow rendering pass. The output of this shader is thrown away; it can call [**discard**](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-discard) on every pixel.

Render the objects that can cast shadows, but don't bother rendering geometry that can't cast a shadow (like a floor in a room, or objects removed from the shadow pass for optimization reasons).

```cpp
void ShadowSceneRenderer::RenderShadowMap()
{
    auto context = m_deviceResources->GetD3DDeviceContext();

    // Render all the objects in the scene that can cast shadows onto themselves or onto other objects.

    // Only bind the ID3D11DepthStencilView for output.
    context->OMSetRenderTargets(
        0,
        nullptr,
        m_shadowDepthView.Get()
        );

    // Note that starting with the second frame, the previous call will display
    // warnings in VS debug output about forcing an unbind of the pixel shader
    // resource. This warning can be safely ignored when using shadow buffers
    // as demonstrated in this sample.

    // Set rendering state.
    context->RSSetState(m_shadowRenderState.Get());
    context->RSSetViewports(1, &m_shadowViewport);

    // Each vertex is one instance of the VertexPositionTexNormColor struct.
    UINT stride = sizeof(VertexPositionTexNormColor);
    UINT offset = 0;
    context->IASetVertexBuffers(
        0,
        1,
        m_vertexBuffer.GetAddressOf(),
        &stride,
        &offset
        );

    context->IASetIndexBuffer(
        m_indexBuffer.Get(),
        DXGI_FORMAT_R16_UINT, // Each index is one 16-bit unsigned integer (short).
        0
        );

    context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
    context->IASetInputLayout(m_inputLayout.Get());

    // Attach our vertex shader.
    context->VSSetShader(
        m_simpleVertexShader.Get(),
        nullptr,
        0
        );

    // Send the constant buffers to the Graphics device.
    context->VSSetConstantBuffers(
        0,
        1,
        m_lightViewProjectionBuffer.GetAddressOf()
        );

    context->VSSetConstantBuffers(
        1,
        1,
        m_rotatedModelBuffer.GetAddressOf()
        );

    // In some configurations, it's possible to avoid setting a pixel shader
    // (or set PS to nullptr). Not all drivers are tolerant of this, so to be
    // safe set a minimal shader here.
    //
    // Direct3D will discard output from this shader because the render target
    // view is unbound.
    context->PSSetShader(
        m_textureShader.Get(),
        nullptr,
        0
        );

    // Draw the objects.
    context->DrawIndexed(
        m_indexCountCube,
        0,
        0
        );
}
```

**Optimize the view frustum:**  Make sure your implementation computes a tight view frustum so that you get the most precision out of your depth buffer. See [Common Techniques to Improve Shadow Depth Maps](/windows/desktop/DxTechArts/common-techniques-to-improve-shadow-depth-maps) for more tips on shadow technique.

## Vertex shader for shadow pass


Use a simplified version of your vertex shader to render just the vertex position in light space. Don't include any lighting normals, secondary transformations, and so on.

```cpp
PixelShaderInput main(VertexShaderInput input)
{
    PixelShaderInput output;
    float4 pos = float4(input.pos, 1.0f);

    // Transform the vertex position into projected space.
    pos = mul(pos, model);
    pos = mul(pos, view);
    pos = mul(pos, projection);
    output.pos = pos;

    return output;
}
```

In the next part of this walkthrough, learn how to add shadows by [rendering with depth testing](render-the-scene-with-depth-testing.md).

 

 