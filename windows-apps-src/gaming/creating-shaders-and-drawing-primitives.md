---
title: Create shaders and drawing primitives
description: Here, we show you how to use HLSL source files to compile and create shaders that you can then use to draw primitives on the display.
ms.assetid: 91113bbe-96c9-4ef9-6482-39f1ff1a70f4
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, shaders, primitives, directx
ms.localizationpriority: medium
---
# Create shaders and drawing primitives



Here, we show you how to use HLSL source files to compile and create shaders that you can then use to draw primitives on the display.

We create and draw a yellow triangle by using vertex and pixel shaders. After we create the Direct3D device, the swap chain, and the render-target view, we read data from binary shader object files on the disk.

**Objective:** To create shaders and to draw primitives.

## Prerequisites


We assume that you are familiar with C++. You also need basic experience with graphics programming concepts.

We also assume that you went through [Quickstart: setting up DirectX resources and displaying an image](setting-up-directx-resources.md).

**Time to complete:** 20 minutes.

## Instructions

### 1. Compiling HLSL source files

Microsoft Visual Studio uses the [fxc.exe](/windows/desktop/direct3dtools/fxc) HLSL code compiler to compile the .hlsl source files (SimpleVertexShader.hlsl and SimplePixelShader.hlsl) into .cso binary shader object files (SimpleVertexShader.cso and SimplePixelShader.cso). For more info about the HLSL code compiler, see Effect-Compiler Tool. For more info about compiling shader code, see [Compiling Shaders](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-part1).

Here is the code in SimpleVertexShader.hlsl:

```hlsl
struct VertexShaderInput
{
    DirectX::XMFLOAT2 pos : POSITION;
};

struct PixelShaderInput
{
    float4 pos : SV_POSITION;
};

PixelShaderInput SimpleVertexShader(VertexShaderInput input)
{
    PixelShaderInput vertexShaderOutput;

    // For this lesson, set the vertex depth value to 0.5, so it is guaranteed to be drawn.
    vertexShaderOutput.pos = float4(input.pos, 0.5f, 1.0f);

    return vertexShaderOutput;
}
```

Here is the code in SimplePixelShader.hlsl:

```hlsl
struct PixelShaderInput
{
    float4 pos : SV_POSITION;
};

float4 SimplePixelShader(PixelShaderInput input) : SV_TARGET
{
    // Draw the entire triangle yellow.
    return float4(1.0f, 1.0f, 0.0f, 1.0f);
}
```

### 2. Reading data from disk

We use the DX::ReadDataAsync function from DirectXHelper.h in the DirectX 11 App (Universal Windows) template to asynchronously read data from a file on the disk.

### 3. Creating vertex and pixel shaders

We read data from the SimpleVertexShader.cso file and assign the data to the *vertexShaderBytecode* byte array. We call [**ID3D11Device::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader) with the byte array to create the vertex shader ([**ID3D11VertexShader**](/windows/desktop/api/d3d11/nn-d3d11-id3d11vertexshader)). We set the vertex depth value to 0.5 in the SimpleVertexShader.hlsl source to guarantee that our triangle is drawn. We populate an array of [**D3D11\_INPUT\_ELEMENT\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_input_element_desc) structures to describe the layout of the vertex shader code and then call [**ID3D11Device::CreateInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createinputlayout) to create the layout. The array has one layout element that defines the vertex position. We read data from the SimplePixelShader.cso file and assign the data to the *pixelShaderBytecode* byte array. We call [**ID3D11Device::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader) with the byte array to create the pixel shader ([**ID3D11PixelShader**](/windows/desktop/api/d3d11/nn-d3d11-id3d11pixelshader)). We set the pixel value to (1,1,1,1) in the SimplePixelShader.hlsl source to make our triangle yellow. You can change the color by changing this value.

We create vertex and index buffers that define a simple triangle. To do this, we first define the triangle, next describe the vertex and index buffers ([**D3D11\_BUFFER\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_buffer_desc) and [**D3D11\_SUBRESOURCE\_DATA**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_subresource_data)) using the triangle definition, and finally call [**ID3D11Device::CreateBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createbuffer) once for each buffer.

```cpp
        auto loadVSTask = DX::ReadDataAsync(L"SimpleVertexShader.cso");
        auto loadPSTask = DX::ReadDataAsync(L"SimplePixelShader.cso");
        
        // Load the raw vertex shader bytecode from disk and create a vertex shader with it.
        auto createVSTask = loadVSTask.then([this](const std::vector<byte>& vertexShaderBytecode) {


          ComPtr<ID3D11VertexShader> vertexShader;
          DX::ThrowIfFailed(
              m_d3dDevice->CreateVertexShader(
                  vertexShaderBytecode->Data,
                  vertexShaderBytecode->Length,
                  nullptr,
                  &vertexShader
                  )
              );

          // Create an input layout that matches the layout defined in the vertex shader code.
          // For this lesson, this is simply a DirectX::XMFLOAT2 vector defining the vertex position.
          const D3D11_INPUT_ELEMENT_DESC basicVertexLayoutDesc[] =
          {
              { "POSITION", 0, DXGI_FORMAT_R32G32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0 },
          };

          ComPtr<ID3D11InputLayout> inputLayout;
          DX::ThrowIfFailed(
              m_d3dDevice->CreateInputLayout(
                  basicVertexLayoutDesc,
                  ARRAYSIZE(basicVertexLayoutDesc),
                  vertexShaderBytecode->Data,
                  vertexShaderBytecode->Length,
                  &inputLayout
                  )
              );
        });
        
        // Load the raw pixel shader bytecode from disk and create a pixel shader with it.
        auto createPSTask = loadPSTask.then([this](const std::vector<byte>& pixelShaderBytecode) {
          ComPtr<ID3D11PixelShader> pixelShader;
          DX::ThrowIfFailed(
              m_d3dDevice->CreatePixelShader(
                  pixelShaderBytecode->Data,
                  pixelShaderBytecode->Length,
                  nullptr,
                  &pixelShader
                  )
              );
        });

        // Create vertex and index buffers that define a simple triangle.
        auto createTriangleTask = (createPSTask && createVSTask).then([this] () {

          DirectX::XMFLOAT2 triangleVertices[] =
          {
              float2(-0.5f, -0.5f),
              float2( 0.0f,  0.5f),
              float2( 0.5f, -0.5f),
          };

          unsigned short triangleIndices[] =
          {
              0, 1, 2,
          };

          D3D11_BUFFER_DESC vertexBufferDesc = {0};
          vertexBufferDesc.ByteWidth = sizeof(float2) * ARRAYSIZE(triangleVertices);
          vertexBufferDesc.Usage = D3D11_USAGE_DEFAULT;
          vertexBufferDesc.BindFlags = D3D11_BIND_VERTEX_BUFFER;
          vertexBufferDesc.CPUAccessFlags = 0;
          vertexBufferDesc.MiscFlags = 0;
          vertexBufferDesc.StructureByteStride = 0;

          D3D11_SUBRESOURCE_DATA vertexBufferData;
          vertexBufferData.pSysMem = triangleVertices;
          vertexBufferData.SysMemPitch = 0;
          vertexBufferData.SysMemSlicePitch = 0;

          ComPtr<ID3D11Buffer> vertexBuffer;
          DX::ThrowIfFailed(
              m_d3dDevice->CreateBuffer(
                  &vertexBufferDesc,
                  &vertexBufferData,
                  &vertexBuffer
                  )
              );

          D3D11_BUFFER_DESC indexBufferDesc;
          indexBufferDesc.ByteWidth = sizeof(unsigned short) * ARRAYSIZE(triangleIndices);
          indexBufferDesc.Usage = D3D11_USAGE_DEFAULT;
          indexBufferDesc.BindFlags = D3D11_BIND_INDEX_BUFFER;
          indexBufferDesc.CPUAccessFlags = 0;
          indexBufferDesc.MiscFlags = 0;
          indexBufferDesc.StructureByteStride = 0;

          D3D11_SUBRESOURCE_DATA indexBufferData;
          indexBufferData.pSysMem = triangleIndices;
          indexBufferData.SysMemPitch = 0;
          indexBufferData.SysMemSlicePitch = 0;

          ComPtr<ID3D11Buffer> indexBuffer;
          DX::ThrowIfFailed(
              m_d3dDevice->CreateBuffer(
                  &indexBufferDesc,
                  &indexBufferData,
                  &indexBuffer
                  )
              );
        });
```

We use the vertex and pixel shaders, the vertex shader layout, and the vertex and index buffers to draw a yellow triangle.

### 4. Drawing the triangle and presenting the rendered image

We enter an endless loop to continually render and display the scene. We call [**ID3D11DeviceContext::OMSetRenderTargets**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-omsetrendertargets) to specify the render target as the output target. We call [**ID3D11DeviceContext::ClearRenderTargetView**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-clearrendertargetview) with { 0.071f, 0.04f, 0.561f, 1.0f } to clear the render target to a solid blue color.

In the endless loop, we draw a yellow triangle on the blue surface.

**To draw a yellow triangle**

1.  First, we call [**ID3D11DeviceContext::IASetInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetinputlayout) to describe how vertex buffer data is streamed into the input-assembler stage.
2.  Next, we call [**ID3D11DeviceContext::IASetVertexBuffers**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetvertexbuffers) and [**ID3D11DeviceContext::IASetIndexBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetindexbuffer) to bind the vertex and index buffers to the input-assembler stage.
3.  Next, we call [**ID3D11DeviceContext::IASetPrimitiveTopology**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetprimitivetopology) with the [**D3D11\_PRIMITIVE\_TOPOLOGY\_TRIANGLESTRIP**](/previous-versions/windows/desktop/legacy/ff476189(v=vs.85)) value to specify for the input-assembler stage to interpret the vertex data as a triangle strip.
4.  Next, we call [**ID3D11DeviceContext::VSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-vssetshader) to initialize the vertex shader stage with the vertex shader code and [**ID3D11DeviceContext::PSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-pssetshader) to initialize the pixel shader stage with the pixel shader code.
5.  Finally, we call [**ID3D11DeviceContext::DrawIndexed**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-drawindexed) to draw the triangle and submit it to the rendering pipeline.

We call [**IDXGISwapChain::Present**](/windows/desktop/api/dxgi/nf-dxgi-idxgiswapchain-present) to present the rendered image to the window.

```cpp
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

            m_d3dDeviceContext->IASetInputLayout(inputLayout.Get());

            // Set the vertex and index buffers, and specify the way they define geometry.
            UINT stride = sizeof(float2);
            UINT offset = 0;
            m_d3dDeviceContext->IASetVertexBuffers(
                0,
                1,
                vertexBuffer.GetAddressOf(),
                &stride,
                &offset
                );

            m_d3dDeviceContext->IASetIndexBuffer(
                indexBuffer.Get(),
                DXGI_FORMAT_R16_UINT,
                0
                );

            m_d3dDeviceContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

            // Set the vertex and pixel shader stage state.
            m_d3dDeviceContext->VSSetShader(
                vertexShader.Get(),
                nullptr,
                0
                );

            m_d3dDeviceContext->PSSetShader(
                pixelShader.Get(),
                nullptr,
                0
                );

            // Draw the cube.
            m_d3dDeviceContext->DrawIndexed(
                ARRAYSIZE(triangleIndices),
                0,
                0
                );

            // Present the rendered image to the window.  Because the maximum frame latency is set to 1,
            // the render loop will generally be throttled to the screen refresh rate, typically around
            // 60 Hz, by sleeping the application on Present until the screen is refreshed.
            DX::ThrowIfFailed(
                m_swapChain->Present(1, 0)
                );
```

## Summary and next steps


We created and drew a yellow triangle by using vertex and pixel shaders.

Next, we create an orbiting 3D cube and apply lighting effects to it.

[Using depth and effects on primitives](using-depth-and-effects-on-primitives.md)

 

 