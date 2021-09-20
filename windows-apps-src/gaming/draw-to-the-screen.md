---
title: Draw to the screen
description: Learn how to use GXDI and Direct3D APIs to port OpenGL code to DirectX and draw a spinning cube to the screen.
ms.assetid: cc681548-f694-f613-a19d-1525a184d4ab
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, graphics
ms.localizationpriority: medium
---
# Draw to the screen




**Important APIs**

-   [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d)
-   [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview)
-   [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1)

Finally, we port the code that draws the spinning cube to the screen.

In OpenGL ES 2.0, your drawing context is defined as an EGLContext type, which contains the window and surface parameters as well the resources necessary for drawing to the render targets that will be used to compose the final image displayed to the window. You use this context to configure the graphics resources to correctly display the results of your shader pipeline on the display. One of the primary resources is the "back buffer" (or "frame buffer object") that contains the final, composited render targets, ready for presentation to the display.

With Direct3D, the process of configuring the graphics resources for drawing to the display is more didactic, and requires quite a few more APIs. (A Microsoft Visual Studio Direct3D template can significantly simplify this process, though!) To obtain a context (called a Direct3D device context), you must first obtain an [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) object, and use it to create and configure an [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) object. These two objects are used in conjunction to configure the specific resources you need for drawing to the display.

In short, the DXGI APIs contain primarily APIs for managing resources that directly pertain to the graphics adapter, and Direct3D contains the APIs that allow you to interface between the GPU and your main program running on the CPU.

For the purposes of comparison in this sample, here are the relevant types from each API:

-   [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1): provides a virtual representation of the graphics device and its resources.
-   [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1): provides the interface to configure buffers and issue rendering commands.
-   [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1): the swap chain is analogous to the back buffer in OpenGL ES 2.0. It is the region of memory on the graphics adapter that contains the final rendered image(s) for display. It is called the "swap chain" because it has several buffers that can be written to and "swapped" to present the latest render to the screen.
-   [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview): this contains the 2D bitmap buffer that the Direct3D device context draws into, and which is presented by the swap chain. As with OpenGL ES 2.0, you can have multiple render targets, some of which are not bound to the swap chain but are used for multi-pass shading techniques.

In the template, the renderer object contains the following fields:

Direct3D 11: Device and device context declarations

``` syntax
Platform::Agile<Windows::UI::Core::CoreWindow>       m_window;

Microsoft::WRL::ComPtr<ID3D11Device1>                m_d3dDevice;
Microsoft::WRL::ComPtr<ID3D11DeviceContext1>          m_d3dContext;
Microsoft::WRL::ComPtr<IDXGISwapChain1>                      m_swapChainCoreWindow;
Microsoft::WRL::ComPtr<ID3D11RenderTargetView>          m_d3dRenderTargetViewWin;
```

Here's how the back buffer is configured as a render target and provided to the swap chain.

``` syntax
ComPtr<ID3D11Texture2D> backBuffer;
m_swapChainCoreWindow->GetBuffer(0, IID_PPV_ARGS(backBuffer));
m_d3dDevice->CreateRenderTargetView(
  backBuffer.Get(),
  nullptr,
  &m_d3dRenderTargetViewWin);
```

The Direct3D runtime implicitly creates an [**IDXGISurface1**](/windows/desktop/api/dxgi/nn-dxgi-idxgisurface1) for the [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d), which represents the texture as a "back buffer" that the swap chain can use for display.

The initialization and configuration of the Direct3D device and device context, as well as the render targets, can be found in the custom **CreateDeviceResources** and **CreateWindowSizeDependentResources** methods in the Direct3D template.

For more info on Direct3D device context as it relates to EGL and the EGLContext type, read [Port EGL code to DXGI and Direct3D](moving-from-egl-to-dxgi.md).

## Instructions

### Step 1: Rendering the scene and displaying it

After updating the cube data (in this case, by rotating it slightly around the y axis), the Render method sets the viewport to the dimensions of he drawing context (an EGLContext). This context contains the color buffer that will be displayed to the window surface (an EGLSurface), using the configured display (EGLDisplay). At this time, the example updates the vertex data attributes, re-binds the index buffer, draws the cube, and swaps in color buffer drawn by the shading pipeline to the display surface.

OpenGL ES 2.0: Rendering a frame for display

``` syntax
void Render(GraphicsContext *drawContext)
{
  Renderer *renderer = drawContext->renderer;

  int loc;
   
  // Set the viewport
  glViewport ( 0, 0, drawContext->width, drawContext->height );
   
   
  // Clear the color buffer
  glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
  glEnable(GL_DEPTH_TEST);


  // Use the program object
  glUseProgram (renderer->programObject);

  // Load the a_position attribute with the vertex position portion of a vertex buffer element
  loc = glGetAttribLocation(renderer->programObject, "a_position");
  glVertexAttribPointer(loc, 3, GL_FLOAT, GL_FALSE, 
      sizeof(Vertex), 0);
  glEnableVertexAttribArray(loc);

  // Load the a_color attribute with the color position portion of a vertex buffer element
  loc = glGetAttribLocation(renderer->programObject, "a_color");
  glVertexAttribPointer(loc, 4, GL_FLOAT, GL_FALSE, 
      sizeof(Vertex), (GLvoid*) (sizeof(float) * 3));
  glEnableVertexAttribArray(loc);

  // Bind the index buffer
  glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, renderer->indexBuffer);

  // Load the MVP matrix
  glUniformMatrix4fv(renderer->mvpLoc, 1, GL_FALSE, (GLfloat*) &renderer->mvpMatrix.m[0][0]);

  // Draw the cube
  glDrawElements(GL_TRIANGLES, renderer->numIndices, GL_UNSIGNED_INT, 0);

  eglSwapBuffers(drawContext->eglDisplay, drawContext->eglSurface);
}
```

In Direct3D 11, the process is very similar. (We're assuming that you're using the viewport and render target configuration from the Direct3D template.

-   Update the constant buffers (the model-view-projection matrix, in this case) with calls to [**ID3D11DeviceContext1::UpdateSubresource**](/windows/desktop/api/d3d11_1/nf-d3d11_1-id3d11devicecontext1-updatesubresource1).
-   Set the vertex buffer with [**ID3D11DeviceContext1::IASetVertexBuffers**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetvertexbuffers).
-   Set the index buffer with [**ID3D11DeviceContext1::IASetIndexBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetindexbuffer).
-   Set the specific triangle topology (a triangle list) with [**ID3D11DeviceContext1::IASetPrimitiveTopology**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetprimitivetopology).
-   Set the input layout of the vertex buffer with [**ID3D11DeviceContext1::IASetInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetinputlayout).
-   Bind the vertex shader with [**ID3D11DeviceContext1::VSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-vssetshader).
-   Bind the fragment shader with [**ID3D11DeviceContext1::PSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-pssetshader).
-   Send the indexed vertices through the shaders and output the color results to the render target buffer with [**ID3D11DeviceContext1::DrawIndexed**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-drawindexed).
-   Display the render target buffer with [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1).

Direct3D 11: Rendering a frame for display

``` syntax
void RenderObject::Render()
{
  // ...

  // Only update shader resources that have changed since the last frame.
  m_d3dContext->UpdateSubresource(
    m_constantBuffer.Get(),
    0,
    NULL,
    &m_constantBufferData,
    0,
    0);

  // Set up the IA stage corresponding to the current draw operation.
  UINT stride = sizeof(VertexPositionColor);
  UINT offset = 0;
  m_d3dContext->IASetVertexBuffers(
    0,
    1,
    m_vertexBuffer.GetAddressOf(),
    &stride,
    &offset);

  m_d3dContext->IASetIndexBuffer(
    m_indexBuffer.Get(),
    DXGI_FORMAT_R16_UINT,
    0);

  m_d3dContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
  m_d3dContext->IASetInputLayout(m_inputLayout.Get());

  // Set up the vertex shader corresponding to the current draw operation.
  m_d3dContext->VSSetShader(
    m_vertexShader.Get(),
    nullptr,
    0);

  m_d3dContext->VSSetConstantBuffers(
    0,
    1,
    m_constantBuffer.GetAddressOf());

  // Set up the pixel shader corresponding to the current draw operation.
  m_d3dContext->PSSetShader(
    m_pixelShader.Get(),
    nullptr,
    0);

  m_d3dContext->DrawIndexed(
    m_indexCount,
    0,
    0);

    // ...

  m_swapChainCoreWindow->Present1(1, 0, &parameters);
}

```

Once [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1) is called, your frame is output to the configured display.

## Previous step


[Port the GLSL](port-the-glsl.md)

## Remarks

This example glosses over much of the complexity that goes into configuring device resources, especially for Universal Windows Platform (UWP) DirectX apps. We suggest you review the full template code, especially the parts that perform the window and device resource setup and management. UWP apps have to support rotation events as well as suspend/resume events, and the template demonstrates best practices for handling the loss of an interface or a change in the display parameters.

## Related topics


* [How to: port a simple OpenGL ES 2.0 renderer to Direct3D 11](port-a-simple-opengl-es-2-0-renderer-to-directx-11-1.md)
* [Port the shader objects](port-the-shader-config.md)
* [Port the GLSL](port-the-glsl.md)
* [Draw to the screen](draw-to-the-screen.md)

 

 