---
author: joannaleecy
title: Intro to rendering
description: Learn how to assemble the rendering pipeline to display graphics. Intro to rendering.
ms.assetid: 1da3670b-2067-576f-da50-5eba2f88b3e6
ms.author: joanlee
ms.date: 10/24/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, rendering
ms.localizationpriority: medium
---

# Rendering framework I: Intro to rendering

We've covered how to structure a Universal Windows Platform (UWP) game and how to define a state machine to handle the flow of the game in the earlier topics. Now, it's time to learn how to assemble the rendering framework. Let's look at how the sample game renders the game scene using Direct3D 11 (commonly known as DirectX 11).

>[!Note]
>If you haven't downloaded the latest game code for this sample, go to [Direct3D game sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Simple3DGameDX). This sample is part of a large collection of UWP feature samples. For instructions on how to download the sample, see [Get the UWP samples from GitHub](https://docs.microsoft.com/windows/uwp/get-started/get-uwp-app-samples).

Direct3D 11 contains a set of APIs that provide access to the advanced features of high-performance graphic hardware that can be used to create 3D graphics 
for graphic intensive applications such as games.

Rendering game graphics on-screen is basically rendering a sequence of frames on-screen. In each frame, you have to render objects that are visible in the scene, based on the view. 

In order to render a frame, you have to pass the required scene information to the hardware so that it can be displayed on the screen. If you want to have anything displayed on screen, you need to start rendering as soon as the game starts running.

## Objective

To set up a basic rendering framework to display the graphics output for a UWP DirectX game, you can loosely group them into these three steps.

 1. Establish a connection to the graphics interface
 2. Create the resources needed to draw the graphics
 3. Display the graphics by rendering the frame

This article explains how graphics are rendered, covering steps 1 and 3.

[Rendering framework II: Game rendering](tutorial-game-rendering.md) covers step 2; how to set up the rendering framework and how data is prepared before rendering can happen.

## Get started

Before you begin, you should familiarize yourself with the basic graphics and rendering concepts. If you're new to Direct3D and rendering, see [Terms and concepts](#terms-and-concepts) for a brief description of the graphics and rendering terms used in this article.

For this game, the __GameRenderer__ class object represents the renderer for this sample game.  It's responsible for creating and maintaining all the Direct3D 11 and Direct2D objects used to generate the game visuals.  It also maintains a reference to the __Simple3DGame__ object used to retrieve the list of objects to render as well as status of the game for the Heads Up Display (HUD). 

In this part of the tutorial, we'll focus on rendering 3D objects in the game.

## Establish a connection to the graphics interface

To access to the hardware for rendering, see the UWP framework article under [__App::Initialize__](tutorial--building-the-games-uwp-app-framework.md#appinitialize-method).

The __make\_shared function__, as shown [below](#appinitialize-method), is used to create a __shared\_ptr__ to [__DX::DeviceResources__](#dxdeviceresources), which also provides access to the device. 

In Direct3D 11, a [device](#device) is used to allocate and destroy objects, render primitives, and communicate with the graphics card through the graphics driver.

### App::Initialize method

```cpp
void App::Initialize(
    CoreApplicationView^ applicationView
    )
{
    //...

    // At this point we have access to the device. 
    // We can create the device-dependent resources.
    m_deviceResources = std::make_shared<DX::DeviceResources>();
}
```

## Display the graphics by rendering the frame

The game scene needs to render when the game is launched. The instructions for rendering start in the  [__GameMain::Run__](#gameamainrun-method) method, as shown below.

The simple flow is:
1. __Update__
2. __Render__
3. __Present__

### GameMain::Run method

```cpp
// Comparing this to App::Run() in the DirectX 11 App template
void GameMain::Run()
{
    while (!m_windowClosed)
    {
        if (m_visible) // if the window is visible
        {
            // Added: Switching different game states since this game sample is more complex
            switch (m_updateState) 
            {

                // ...
                CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);
                // 1. Update
                // Also exist in DirectX 11 App template: Update loop to get the latest game changes
                Update();
                
                // 2. Render
                // Present also in DirectX 11 App template: Prepare to render
                m_renderer->Render();
                
                // 3. Present
                // Present also in DirectX 11 App template: Present the 
                // contents of the swap chain to the screen.
                m_deviceResources->Present(); 
                
                // Added: resetting a variable we've added to indicate that 
                // render has been done and no longer needed to render
                m_renderNeeded = false; 
            }
        }
        else
        {   
            // Present also in DirectX 11 App template
            CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending); 
        }
    }
    m_game->OnSuspending();  // exiting due to window close. Make sure to save state.
}
```

### Update

See the [Game flow management](tutorial-game-flow-management.md) article for more information about how game states are updated in the [__App::Update__ and __GameMain::Update__](tutorial-game-flow-management.md#appupdate-method) method.

### Render

Rendering is implemented by calling the [__GameRenderer::Render__](#gamerendererrender-method) method in __GameMain::Run__.

If [stereo rendering](#stereo-rendering) is enabled, there are two rendering passes: one for the right eye and one for the left eye. In each rendering pass, we bind the render target and the [depth-stencil view](#depth-stencil-view) to the device. We also clear the depth-stencil view afterward.

> [!Note]
> Stereo rendering can be achieved using other methods such as single pass stereo using vertex instancing or geometry shaders. The two rendering pass method is a slower, but more convenient way to achieve stereo rendering.

Once the game exists and resources are loaded, update the [projection matrix](#projection-transform-matrix), once per rendering pass. Objects are slightly different from each view. Next, set up the [graphics rendering pipeline](#rendering-pipeline). 

> [!Note]
> See [Create and load DirectX graphic resources](tutorial-game-rendering.md#create-and-load-directx-graphic-resources) for more information on how resources are loaded.

In this game sample, the renderer is designed to use a standard vertex layout across all objects. This simplifies the shader design and allows for easy changes between shaders, independent of the objects' geometry.

#### GameRenderer::Render method

Set the Direct3D context to use an input vertex layout. Input-layout objects describe how vertex buffer data is streamed into the [rendering pipeline](#rendering-pipeline). 

Next, we set the Direct3D context to use the [constant buffers](#constant-buffers) defined earlier, that are used by the [vertex shader](#vertex-shaders-and-pixel-shaders) pipeline stage and the [pixel shader](#vertex-shaders-and-pixel-shaders) pipeline stage. 

> [!Note]
> See [Rendering framework II: Game rendering](tutorial-game-rendering.md) for more information about definition of the constant buffers.

Because the same input layout and set of constant buffers is used for all shaders that are in the pipeline, it's set up once per frame.

```cpp
void GameRenderer::Render()
{
    bool stereoEnabled = m_deviceResources->GetStereoState();

    auto d3dContext = m_deviceResources->GetD3DDeviceContext();
    auto d2dContext = m_deviceResources->GetD2DDeviceContext();

    int renderingPasses = 1;
    if (stereoEnabled)
    {
        renderingPasses = 2;
    }

    for (int i = 0; i < renderingPasses; i++)
    {
        // Iterate through the number of rendering passes to be completed.
        // 2 rendering passes if stereo is enabled
        if (i > 0)
        {
            // Doing the Right Eye View.
            ID3D11RenderTargetView *const targets[1] = { m_deviceResources->GetBackBufferRenderTargetViewRight() };

            // Resets render targets to the screen.
            // OMSetRenderTargets binds 2 things to the device.
            // 1. Binds one render target atomically to the device.
            // 2. Binds the depth-stencil view, as returned by the GetDepthStencilView method, to the device.
            // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476464.aspx

            d3dContext->OMSetRenderTargets(1, targets, m_deviceResources->GetDepthStencilView());
            
            // Clears the depth stencil view.
            // A depth stencil view contains the format and buffer to hold depth and stencil info.
            // For more info about depth stencil view, go to: 
            // https://docs.microsoft.com/windows/uwp/graphics-concepts/depth-stencil-view--dsv-
            // A depth buffer is used to store depth information to control which areas of 
            // polygons are rendered rather than hidden from view. To learn more about a depth buffer,
            // go to: https://docs.microsoft.com/windows/uwp/graphics-concepts/depth-buffers
            // A stencil buffer is used to mask pixels in an image, to produce special effects. 
            // The mask determines whether a pixel is drawn or not,
            // by setting the bit to a 1 or 0. To learn more about a stencil buffer,
            // go to: https://docs.microsoft.com/windows/uwp/graphics-concepts/stencil-buffers

            d3dContext->ClearDepthStencilView(m_deviceResources->GetDepthStencilView(), D3D11_CLEAR_DEPTH, 1.0f, 0);
            
            // d2d -- Discussed later
            d2dContext->SetTarget(m_deviceResources->GetD2DTargetBitmapRight());
        }
        else
        {
            // Doing the Mono or Left Eye View.
            // As compared to the right eye:
            // m_deviceResources->GetBackBufferRenderTargetView instead of GetBackBufferRenderTargetViewRight
            ID3D11RenderTargetView *const targets[1] = { m_deviceResources->GetBackBufferRenderTargetView() }; 
            
            // Same as the Right Eye View.
            d3dContext->OMSetRenderTargets(1, targets, m_deviceResources->GetDepthStencilView());
            d3dContext->ClearDepthStencilView(m_deviceResources->GetDepthStencilView(), D3D11_CLEAR_DEPTH, 1.0f, 0);
            
            // d2d -- Discussed later under Adding UI
            d2dContext->SetTarget(m_deviceResources->GetD2DTargetBitmap()); 
        }

        // Render the scene objects
        if (m_game != nullptr && m_gameResourcesLoaded && m_levelResourcesLoaded)
        {
            // This section is only used after the game state has been initialized and all device
            // resources needed for the game have been created and associated with the game objects.
            if (stereoEnabled)
            {
                // When doing stereo, it is necessary to update the projection matrix once per rendering pass.

                auto orientation = m_deviceResources->GetOrientationTransform3D();

                ConstantBufferChangeOnResize changesOnResize;

                // Apply either a left or right eye projection, which is an offset from the middle
                XMStoreFloat4x4(
                    &changesOnResize.projection,
                    XMMatrixMultiply(
                        XMMatrixTranspose(
                            i == 0 ?
                            m_game->GameCamera()->LeftEyeProjection() :
                            m_game->GameCamera()->RightEyeProjection()
                            ),
                        XMMatrixTranspose(XMLoadFloat4x4(&orientation))
                        )
                    );

                d3dContext->UpdateSubresource(
                    m_constantBufferChangeOnResize.Get(),
                    0,
                    nullptr,
                    &changesOnResize,
                    0,
                    0
                    );
            }

            // Update variables that change once per frame.
            ConstantBufferChangesEveryFrame constantBufferChangesEveryFrame;
            XMStoreFloat4x4(
                &constantBufferChangesEveryFrame.view,
                XMMatrixTranspose(m_game->GameCamera()->View())
                );
            d3dContext->UpdateSubresource(
                m_constantBufferChangesEveryFrame.Get(),
                0,
                nullptr,
                &constantBufferChangesEveryFrame,
                0,
                0
                );

            // Setup the graphics pipeline. This sample uses the same InputLayout and set of
            // constant buffers for all shaders, so they only need to be set once per frame.
            // For more info about the graphics or rendering pipeline, 
            // go to https://msdn.microsoft.com/library/windows/desktop/ff476882.aspx

            // IASetInputLayout binds an input-layout object to the input-assembler (IA) stage. 
            // Input-layout objects describe how vertex buffer data is streamed into the IA pipeline stage.
            // Set up the Direct3D context to use this vertex layout.
            // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476454.aspx
            d3dContext->IASetInputLayout(m_vertexLayout.Get());

            // VSSetConstantBuffers sets the constant buffers used by the vertex shader pipeline stage.
            // Set up the Direct3D context to use these constant buffers.
            // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476491.aspx

            d3dContext->VSSetConstantBuffers(0, 1, m_constantBufferNeverChanges.GetAddressOf());
            d3dContext->VSSetConstantBuffers(1, 1, m_constantBufferChangeOnResize.GetAddressOf());
            d3dContext->VSSetConstantBuffers(2, 1, m_constantBufferChangesEveryFrame.GetAddressOf());
            d3dContext->VSSetConstantBuffers(3, 1, m_constantBufferChangesEveryPrim.GetAddressOf());

            // Sets the constant buffers used by the pixel shader pipeline stage. 
            // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476470.aspx

            d3dContext->PSSetConstantBuffers(2, 1, m_constantBufferChangesEveryFrame.GetAddressOf());
            d3dContext->PSSetConstantBuffers(3, 1, m_constantBufferChangesEveryPrim.GetAddressOf());
            d3dContext->PSSetSamplers(0, 1, m_samplerLinear.GetAddressOf());

            auto objects = m_game->RenderObjects();
            for (auto object = objects.begin(); object != objects.end(); object++)
            {
                // The 3D object render method handles the rendering.
                // For more info, see Primitive rendering below.
                (*object)->Render(d3dContext, m_constantBufferChangesEveryPrim.Get()); /
            }
        }
        else
        {
            const float ClearColor[4] = {0.5f, 0.5f, 0.8f, 1.0f};

            // Only need to clear the background when not rendering the full 3D scene since
            // the 3D world is a fully enclosed box and the dynamics prevents the camera from
            // moving outside this space.
            if (i > 0)
            {
                // Doing the Right Eye View.
                d3dContext->ClearRenderTargetView(m_deviceResources->GetBackBufferRenderTargetViewRight(), ClearColor);
            }
            else
            {
                // Doing the Mono or Left Eye View.
                d3dContext->ClearRenderTargetView(m_deviceResources->GetBackBufferRenderTargetView(), ClearColor);
            }
        }

        // Start of 2D rendering
        d3dContext->BeginEventInt(L"D2D BeginDraw", 1);
        d2dContext->BeginDraw();

        // ...
    }
}
```

### Primitive rendering

When rendering the scene, you loop through all the objects that need to be rendered. The steps below are repeated for each object (primitive).

* Update the constant buffer(__m\_constantBufferChangesEveryPrim__) with the model's [world transformation matrix](#world-transform-matrix) and material information.
* The  __m\_constantBufferChangesEveryPrim__ contains parameters for each object.  It includes the object to world transformation matrix as well as material properties like color and specular exponent for lighting calculations.
* Set Direct3D context to use the input vertex layout for the mesh object data to be streamed into the input-assembler (IA) stage of the [rendering pipeline](#rendering-pipeline)
* Set Direct3D context to use an [index buffer](#index-buffer) in the IA stage. Provide the primitive info: type, data order.
* Submit a draw call to draw the indexed, non-instanced primitive. The __GameObject::Render__ method updates the primitive [constant buffer](#constant-buffer-or-shader-constant-buffer) with the data specific to a given primitive. This results in a __DrawIndexed__ call on the context to draw the geometry of that each primitive. Specifically, this draw call queues commands and data to the graphics processing unit (GPU), as parameterized by the constant buffer data. Each draw call executes the [vertex shader](#vertex-shaders-and-pixel-shaders) one time per vertex, and then the [pixel shader](#vertex-shaders-and-pixel-shaders) one time for every pixel of each triangle in the primitive. The textures are part of the state that the pixel shader uses to do the rendering.

Reasons for multiple constant buffers:
    * The game uses multiple constant buffers but only needs to update these buffers one time per primitive. As mentioned earlier, constant buffers are like inputs to the shaders that run for each primitive. Some data is static (__m\_constantBufferNeverChanges__); some data is constant over the frame (__m\_constantBufferChangesEveryFrame__), like the position of the camera; and some data is specific to the primitive, like its color and textures (__m\_constantBufferChangesEveryPrim__)
    * The game [renderer](#renderer) separates these inputs into different constant buffers to optimize the memory bandwidth that the CPU and GPU use. This approach also helps to minimize the amount of data the GPU needs to keep track of. The GPU has a big queue of commands, and each time the game calls __Draw__, that command is queued along with the data associated with it. When the game updates the primitive constant buffer and issues the next __Draw__ command, the graphics driver adds this next command and the associated data to the queue. If the game draws 100 primitives, it could potentially have 100 copies of the constant buffer data in the queue. To minimize the amount of data the game is sending to the GPU, the game uses a separate primitive constant buffer that only contains the updates for each primitive.

#### GameObject::Render method

```cpp
void GameObject::Render(
    _In_ ID3D11DeviceContext *context,
    _In_ ID3D11Buffer *primitiveConstantBuffer
    )
{
    if (!m\_active || (m\_mesh == nullptr) || (m_normalMaterial == nullptr))
    {
        return;
    }

    ConstantBufferChangesEveryPrim constantBuffer;

    // Put the model matrix info into a constant buffer, in world matrix.
    XMStoreFloat4x4(
        &constantBuffer.worldMatrix,
        XMMatrixTranspose(ModelMatrix())
        );

    // Check to see which material to use on the object.
    // If a collision (a hit) is detected, GameObject::Render checks the current context, which 
    // indicates whether the target has been hit by an ammo sphere. If the target has been hit, 
    // this method applies a hit material, which reverses the colors of the rings of the target to 
    // indicate a successful hit to the player. Otherwise, it applies the default material 
    // with the same method. In both cases, it sets the material by calling Material::RenderSetup, 
    // which sets the appropriate constants into the constant buffer. Then, it calls 
    // ID3D11DeviceContext::PSSetShaderResources to set the corresponding texture resource for the 
    // pixel shader, and ID3D11DeviceContext::VSSetShader and ID3D11DeviceContext::PSSetShader 
    // to set the vertex shader and pixel shader objects themselves, respectively.

    if (m_hit && m_hitMaterial != nullptr)
    {
        m_hitMaterial->RenderSetup(context, &constantBuffer);
    }
    else
    {
        m_normalMaterial->RenderSetup(context, &constantBuffer);
    }

    // Update the primitive constant buffer with the object model's info.
    context->UpdateSubresource(primitiveConstantBuffer, 0, nullptr, &constantBuffer, 0, 0);

    // Render the mesh.
    // See MeshObject::Render method below.
    m_mesh->Render(context);
}

#### MeshObject::Render method

void MeshObject::Render(\_In\_ ID3D11DeviceContext *context)
{
    // PNTVertex is a struct. stride provides us the size required for all the mesh data
    // struct PNTVertex
    //{
    //  DirectX::XMFLOAT3 position;
    //  DirectX::XMFLOAT3 normal;
    //  DirectX::XMFLOAT2 textureCoordinate;
    //};
    uint32 stride = sizeof(PNTVertex);
    uint32 offset = 0;

    // Similar to the main render loop.
    // Input-layout objects describe how vertex buffer data is streamed into the IA pipeline stage.
    context->IASetVertexBuffers(0, 1, m_vertexBuffer.GetAddressOf(), &stride, &offset);

    // IASetIndexBuffer binds an index buffer to the input-assembler stage.
    // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476453.aspx
    context->IASetIndexBuffer(m_indexBuffer.Get(), DXGI_FORMAT_R16_UINT, 0);

    // Binds information about the primitive type, and data order that describes input data for the input assembler stage.
    // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476455.aspx
    context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

    // Draw indexed, non-instanced primitives. A draw API submits work to the rendering pipeline.
    // For more info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476409.aspx
    context->DrawIndexed(m_indexCount, 0, 0);
}
```

### Present

We call the __DX::DeviceResources::Present__ method to put the contents we've placed in the buffers and display it.

We use the term swap chain for a collection of buffers that are used for displaying frames to the user. Each time an application presents a new frame for display, the first buffer in the swap chain takes the place of the displayed buffer. This process is called swapping or flipping. For more information, see [Swap chains](../graphics-concepts/swap-chains.md).

* __IDXGISwapChain1__ interface's __Present__ method the instructs [DXGI](#dxgi) to block until Vertical Synchronization (VSync), putting the application to sleep until the next VSync. This ensures you don't waste any cycles rendering frames that will never be displayed to the screen.
* __ID3D11DeviceContext3__ interface's __DiscardView__ method discards the contents of the [render target](#render-target). This is a valid operation only when the existing contents will be entirely overwritten. If dirty or scroll rects are used, this call should be removed.
* Using the same __DiscardView__ method, discard the contents of the [depth-stencil](#depth-stencil).
* __HandleDeviceLost__ method is used to manage the scenario if the [device](#device) is removed. If the device was removed either by a disconnection or a driver upgrade, you must recreate all device resources. For more information, see [Handle device removed scenarios in Direct3D 11](handling-device-lost-scenarios.md).

> [!Tip]
> To achieve a smooth frame rate, you must ensure that the amount of work to render a frame fits the time between VSyncs.

```cpp
// Present the contents of the swap chain to the screen.
void DX::DeviceResources::Present()
{
    // The first argument instructs DXGI to block until VSync, putting the application
    // to sleep until the next VSync. This ensures we don't waste any cycles rendering
    // frames that will never be displayed to the screen.
    HRESULT hr = m_swapChain->Present(1, 0);

    // Discard the contents of the render target.
    // This is a valid operation only when the existing contents will be entirely
    // overwritten. If dirty or scroll rects are used, this call should be removed.
    m_d3dContext->DiscardView(m_d3dRenderTargetView.Get());

    // Discard the contents of the depth-stencil.
    m_d3dContext->DiscardView(m_d3dDepthStencilView.Get());

    // If the device was removed either by a disconnection or a driver upgrade, we 
    // must recreate all device resources.
    // For more info about how to handle a device lost scenario, go to:
    // https://docs.microsoft.com/windows/uwp/gaming/handling-device-lost-scenarios
    if (hr == DXGI_ERROR_DEVICE_REMOVED || hr == DXGI_ERROR_DEVICE_RESET)
    {
        HandleDeviceLost();
    }
    else
    {
        DX::ThrowIfFailed(hr);
    }
}
```

## Next steps

This article explained how graphics is rendered on the display and provided a short description for some of the rendering terms used. Learn more about rendering in the [Rendering framework II: Game rendering](tutorial-game-rendering.md) article, and learn how to prepare the data needed before rendering.

## Terms and concepts

### Simple game scene

A simple game scene is made up of a few objects with several light sources.

An object's shape is defined by a set of X, Y, Z coordinates in space. The actual render location in the game world can be determined by applying a transformation matrix to the positional X, Y, Z coordinates. It may also have a set of texture coordinates - U and V which specifies how a material is applied to the object. This defines the surface properties of the object and gives you the ability to see if an object has a rough surface like a tennis ball or a smooth glossy surface like a bowling ball.

Scene and object info are used by the rendering framework to recreate the scene frame by frame, making it come alive on your display monitor.

### Rendering pipeline

The rendering pipeline is the process in which 3D scene info is translated to an image displayed on screen. In Direct3D 11, this pipeline is programmable. You can adapt the stages to support your rendering needs. Stages that feature common shader cores are programmable by using the HLSL programming language. It is also known as the graphics rendering pipeline or simply pipeline.

To help you create this pipeline, you need to be familiar with:
* [HLSL](#HLSL). We recommend the use of HLSL Shader Model 5.1 and above for UWP DirectX games.
* [Shaders](#Shaders)
* [Vertex shaders and pixel shaders](#vertext-shaders-pixel-shaders)
* [Shader stages](#shader-stages)
* [Various shader file formats](#various-shader-file-formats)

For more information, see [Understand the Direct3D 11 rendering pipeline](https://msdn.microsoft.com/library/windows/desktop/dn643746.aspx) and [Graphics pipeline](https://msdn.microsoft.com/library/windows/desktop/ff476882.aspx).

#### HLSL

HLSL is the High Level Shading Language for DirectX. Using HLSL, you can create C like programmable shaders for the Direct3D pipeline. For more information, see [HLSL](https://msdn.microsoft.com/library/windows/desktop/bb509561.aspx).

#### Shaders

Shaders can be thought of as a set of instructions that determine how the surface of an object appears when rendered. Those that are programmed using HLSL are known as HLSL shaders. Source code files for [HLSL])(#hlsl) shaders have .hlsl file extension. These shaders can be compiled at author-time or at runtime, and set at runtime into the appropriate pipeline stage; a compiled shader object has a .cso file extension.

Direct3D 9 shaders can be designed using shader model 1, shader model 2 and shader model 3; Direct3D 10 shaders can only be designed on shader model 4. Direct3D 11 shaders can be designed on shader model 5. Direct3D 11.3 and Direct3D 12 can be designed on shader model 5.1, and Direct3D 12 can also be designed on shader model 6.

#### Vertex shaders and pixel shaders

Data enters the graphics pipeline as a stream of primitives and is processed by various shaders such as the vertex shaders and pixel shaders. 

Vertex shaders processes vertices, typically performing operations such as transformations, skinning, and lighting.  Pixel shaders enables rich shading techniques such as per-pixel lighting and post-processing. It combines constant variables, texture data, interpolated per-vertex values, and other data to produce per-pixel outputs. 

#### Shader stages

A sequence of these various shaders defined to process this stream of primitives is known as shader stages in a rendering pipeline. The actual stages depend on the version of Direct3D, but usually include the vertex, geometry, and pixel stages. There are also other stages, such as the hull and domain shaders for tessellation, and the compute shader. All these stages are completely programmable using the [HLSL])(#hlsl). For more information, see [Graphics pipeline](https://msdn.microsoft.com/library/windows/desktop/ff476882.aspx).

#### Various shader file formats

Shader code file extensions:
    * A file with the .hlsl extension holds [HLSL])(#hlsl) source code.
    * A file with the .cso extension holds a compiled shader object.
    * A file with the .h extension is a header file, but in a shader code context, this header file defines a byte array that holds shader data.
    * A file with the .hlsli extension contains the format of the constant buffers. In the game sample, the file is __Shaders__>__ConstantBuffers.hlsli__.

> [!Note]
> You would embed the shader either by loading a .cso file at runtime or adding the .h file in your executable code. But you would not use both for the same shader.

### Deeper understanding of DirectX

Direct3D 11 is a set of APIs that can help us create graphics for graphics intensive applications like games, where we want to have a good graphics card to process intensive computation. This section briefly explains the Direct3D 11 graphics programming concepts: resource, subresource, device, and device context.

#### Resource

For those who are new, you can think of resources (also known as device resources) as info on how to render an object like texture, position, color. Resources provide data to the pipeline and define what is rendered during your scene. Resources can be loaded from your game media or created dynamically at run time.

A resource is, in fact, an area in memory that can be accessed by the Direct3D [pipeline](#rendering-pipeline). In order for the pipeline to access memory efficiently, data that is provided to the pipeline (such as input geometry, shader resources, and textures) must be stored in a resource. There are two types of resources from which all Direct3D resources derive: a buffer or a texture. Up to 128 resources can be active for each pipeline stage. For more information, see [Resources](../graphics-concepts/resources.md).

#### Subresource

The term subresource refers to a subset of a resource. Direct3D can reference an entire resource or it can reference subsets of a resource. For more information, see [Subresource](../graphics-concepts/resource-types.md#subresources).

#### Depth-stencil

A depth-stencil resource contains the format and buffer to hold depth and stencil information. It is created using a texture resource. For more information on how to create a depth-stencil resource, see [Configuring Depth-Stencil Functionality](https://msdn.microsoft.com/library/windows/desktop/bb205074.aspx). We access the depth-stencil resource through the depth-stencil view implemented using the [ID3D11DepthStencilView](https://msdn.microsoft.com/library/windows/desktop/ff476377.aspx) interface.

Depth info tells us which areas of polygons are rendered rather than hidden from view. Stencil info tells us which pixels are masked. It can be used to produce special effects since it determines whether a pixel is drawn or not; sets the bit to a 1 or 0. 

For more information, see: [Depth-stencil view](../graphics-concepts/depth-stencil-view--dsv-.md), [depth buffer](../graphics-concepts/depth-buffers.md), and [stencil buffer](../graphics-concepts/stencil-buffers.md).

#### Render target

A render target is a resource that we can write to at the end of a render pass. It is commonly created using the [ID3D11Device::CreateRenderTargetView](https://msdn.microsoft.com/library/windows/desktop/ff476517.aspx) method using the swap chain back buffer (which is also a resource) as the input parameter. 

Each render target should also have a corresponding depth-stencil view because when we use [OMSetRenderTargets](https://msdn.microsoft.com/library/windows/desktop/ff476464.aspx) to set the render target before using it, it requires also a depth-stencil view. We access the render target resource through the render target view implemented using the [ID3D11RenderTargetView](https://msdn.microsoft.com/library/windows/desktop/ff476582.aspx) interface. 

#### Device

For those who are new to Direct3D 11, you can imagine a device as a way to allocate and destroy objects, render primitives, and communicate with the graphics card through the graphics driver. 

For a more precise explanation, a Direct3D device is the rendering component of Direct3D. A device encapsulates and stores the rendering state, performs transformations and lighting operations, and rasterizes an image to a surface. For more information, see [Devices](../graphics-concepts/devices.md)

A device is represented by the [ID3D11Device](https://msdn.microsoft.com/library/windows/desktop/ff476379.aspx) interface. In other words, the ID3D11Device interface represents a virtual display adapter and is used to create resources that are owned by a device. 

Note that there are different versions of ID3D11Device, [ID3D11Device5](https://msdn.microsoft.com/library/windows/desktop/mt492478.aspx) is the latest version and adds new methods to those in ID3D11Device4. For more information on how Direct3D communicates with the underlying hardware, see [Windows Device Driver Model (WDDM) architecture](https://docs.microsoft.com/windows-hardware/drivers/display/windows-vista-and-later-display-driver-model-architecture).

Each application must have at least one device, most applications only create one device. Create a device for one of the hardware drivers installed on your machine by calling __D3D11CreateDevice__ or __D3D11CreateDeviceAndSwapChain__ and specifying the driver type with the D3D\_DRIVER\_TYPE flag. Each device can use one or more device contexts, depending on the functionality desired. For more information, see [D3D11CreateDevice function](https://msdn.microsoft.com/library/windows/desktop/ff476082.aspx).

#### Device context

A device context is used to set [pipeline](#rendering-pipeline) state and generate rendering commands using the [resources](#resource) owned by a [device](#device). 

Direct3D 11 implements two types of device contexts, one for immediate rendering and the other for deferred rendering; both contexts are represented with an [ID3D11DeviceContext](https://msdn.microsoft.com/library/windows/desktop/ff476385.aspx) interface.  

The __ID3D11DeviceContext__ interfaces have different versions; __ID3D11DeviceContext4__ adds new methods to those in __ID3D11DeviceContext3__.

Note: __ID3D11DeviceContext4__ is introduced in the Windows 10 Creators Update and is the latest version of the __ID3D11DeviceContext__ interface. Applications targeting Windows 10 Creators Update should use this interface instead of earlier versions. For more information, see [ID3D11DeviceContext4](https://msdn.microsoft.com/library/windows/desktop/mt492481.aspx).

#### DX::DeviceResources

The __DX::DeviceResources__ class is in the __DeviceResources.cpp__/__.h__ files and controls all of DirectX device resources. In the sample game project and the DirectX 11 App template project, these files are in the __Commons__ folder. You can grab the latest version of these files when you create a new DirectX 11 App template project in Visual Studio 2015 or later.

### Buffer

A buffer resource is a collection of fully typed data grouped into elements. You can use buffers to store a wide variety of data, including position vectors, normal vectors, texture coordinates in a vertex buffer, indexes in an index buffer, or device state. Buffer elements can include packed data values (like R8G8B8A8 surface values), single 8-bit integers, or four 32-bit floating point values.

There are three types of buffers available: Vertex buffer, index buffer, and constant buffer.

#### Vertex Buffer

Contains the vertex data used to define your geometry. Vertex data includes position coordinates, color data, texture coordinate data, normal data, and so on. 

#### Index Buffer

Contains integer offsets into vertex buffers and are used to render primitives more efficiently. An index buffer contains a sequential set of 16-bit or 32-bit indices; each index is used to identify a vertex in a vertex buffer.

#### Constant Buffer or shader-constant buffer

Allows you to efficiently supply shader data to the pipeline. You can use constant buffers as inputs to the shaders that run for each primitive and store results of the stream-output stage of the rendering pipeline. Conceptually, a constant buffer looks just like a single-element vertex buffer.

#### Design and implementation of buffers

You can design buffers based on the data type, for example, like in our game sample, one buffer is created for static data, another for data that's constant over the frame, and another for data that's specific to a primitive.

All buffer types are encapsulated by the __ID3D11Buffer__ interface and you can create a buffer resource by calling __ID3D11Device::CreateBuffer__. But a buffer must be bound to the pipeline before it can be accessed. Buffers can be bound to multiple pipeline stages simultaneously for reading. A buffer can also be bound to a single pipeline stage for writing; however, the same buffer cannot be bound for reading and writing simultaneously.

Bind buffers to the:
    * Input-assembler stage by calling __ID3D11DeviceContext__ methods like __ID3D11DeviceContext::IASetVertexBuffers__ and __ID3D11DeviceContext::IASetIndexBuffer__
    * Stream-output stage by calling __ID3D11DeviceContext::SOSetTargets__
    * Shader stage by calling shader methods, like __ID3D11DeviceContext::VSSetConstantBuffers__

For more information, see [Introduction to buffers in Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/ff476898.aspx).

### DXGI

Microsoft DirectX Graphics Infrastructure (DXGI) is a new subsystem that was introduced with Windows Vista that encapsulates some of the low-level tasks that are needed by Direct3D 10, 10.1, 11, and 11.1. Special care must be taken when using DXGI in a multithreaded application to ensure that deadlocks do not occur. For more info, see [DirectX Graphics Infrastructure (DXGI): Best Practices- Multithreading](https://msdn.microsoft.com/library/windows/desktop/ee417025.aspx#multithreading_and_dxgi)

### Feature Level

Feature level is a concept introduced in Direct3D 11 to handle the diversity of video cards in new and existing machines. A feature level is a well defined set of graphics processing unit (GPU) functionality. 

Each video card implements a certain level of DirectX functionality depending on the GPUs installed. In prior versions of Microsoft Direct3D, you could find out the version of Direct3D the video card implemented, and then program your application accordingly. 

With feature level, when you create a device, you can attempt to create a device for the feature level that you want to request. If the device creation works, that feature level exists, if not, the hardware does not support that feature level. You can either try to recreate a device at a lower feature level or you can choose to exit the application. For instance, the 12\_0 feature level requires Direct3D 11.3 or Direct3D 12, and shader model 5.1. For more information, see [Direct3D feature levels: Overview for each feature level](https://msdn.microsoft.com/library/windows/desktop/ff476876.aspx#Overview).

Using feature levels, you can develop an application for Direct3D 9, Microsoft Direct3D 10, or Direct3D 11, and then run it on 9, 10, or 11 hardware (with some exceptions). For more information, see [Direct3D feature levels](https://msdn.microsoft.com/library/windows/desktop/ff476876.aspx).

### Stereo rendering

Stereo rendering is used to enhance the illusion of depth. It uses two images, one from the left eye and the other from the right eye to display a scene on the display screen. 

Mathematically, we apply a stereo projection matrix, which is a slight horizontal offset to the right and to the left, of the regular mono projection matrix to achieve this.

We did two rendering passes to achieve stereo rendering in this game sample:
* Bind to right render target, apply right projection, then draw the primitive object.
* Bind to left render target, apply left projection, then draw the primitive object.

### Camera and coordinate space

The game has the code in place to update the world in its own coordinate system (sometimes called the world space or scene space). All objects, including the camera, are positioned and oriented in this space. For more information, see [Coordinate systems](../graphics-concepts/coordinate-systems.md).

A vertex shader does the heavy lifting of converting from the model coordinates to device coordinates with the following algorithm (where V is a vector and M is a matrix).

V(device) = V(model) x M(model-to-world) x M(world-to-view) x M(view-to-device).

where: 
* M(model-to-world) is a transformation matrix for model coordinates to world coordinates, also known as the [World transform matrix](#world-transform-matrix). This is provided by the primitive.
* M(world-to-view) is a transformation matrix for world coordinates to view coordinates, also known as the [View transform matrix](#view-transform-matrix).
    * This is provided by the view matrix of the camera. It's defined by the camera's position along with the look vectors (the "look at" vector that points directly into the scene from the camera, and the "look up" vector that is upwards perpendicular to it)
    * In the sample game, __m\_viewMatrix__ is the view transformation matrix and is calculated using __Camera::SetViewParams__ 
* M(view-to-device) is a transformation matrix for view coordinates to device coordinates, also known as the [Projection transform matrix](#projection-transform-matrix)
    * This is provided by the projection of the camera. It provides info how much of that space is actually visible in the final scene. The Field of View (FoV), aspect ratio, and clipping planes define the projection transform matrix.
    * In the sample game, __m\_projectionMatrix__ defines transformation to the projection coordinates, calculated using __Camera::SetProjParams__ (For stereo projection, you use two projection matrices: one for each eye's view.) 

The shader code in VertexShader.hlsl is loaded with these vectors and matrices from the constant buffers, and performs this transformation for every vertex.

### Coordinate transformation

Direct3D uses three transformations to change your 3D model coordinates into pixel coordinates (screen space). These transformations are world transform, view transform, and projection transform. For more info, go to [Transform overview](../graphics-concepts/transform-overview.md).

#### World transform matrix

A world transform changes coordinates from model space, where vertices are defined relative to a model's local origin, to world space, where vertices are defined relative to an origin common to all the objects in a scene. In essence, the world transform places a model into the world; hence its name. For more information, see [World transform](../graphics-concepts/world-transform.md).

####  View transform matrix

The view transform locates the viewer in world space, transforming vertices into camera space. In camera space, the camera, or viewer, is at the origin, looking in the positive z-direction. For more info, go to [View transform](../graphics-concepts/view-transform.md).

####  Projection transform matrix

The projection transform converts the viewing frustum to a cuboid shape. A viewing frustum is a 3D volume in a scene positioned relative to the viewport's camera. A viewport is a 2D rectangle into which a 3D scene is projected. For more information, see [Viewports and clipping](../graphics-concepts/viewports-and-clipping.md)

Because the near end of the viewing frustum is smaller than the far end, this has the effect of expanding objects that are near to the camera; this is how perspective is applied to the scene. So objects that are closer to the player, appear larger; objects that are further away, appear smaller.

Mathematically, the projection transform is a matrix that is typically both a scale and perspective projection. It functions like the lens of a camera. For more information, see [Projection transform](../graphics-concepts/projection-transform.md).

### Sampler state

Sampler state determines how texture data is sampled using texture addressing modes, filtering, and level of detail. Sampling is done each time a texture pixel, or texel, is read from a texture.

A texture contains an array of texels, or texture pixels. The position of each texel is denoted by (u,v), where u is the width and v is the height, and is mapped between 0 and 1 based on the texture width and height. The resulting texture coordinates are used to address a texel when sampling a texture.

When texture coordinates are below 0 or above 1, the texture address mode defines how the texture coordinate addresses a texel location. For example, when using __TextureAddressMode.Clamp__, any coordinate outside the 0-1 range is clamped to a maximum value of 1, and minimum value of 0 before sampling.

If the texture is too large or too small for the polygon, the texture is filtered to fit the space. A magnification filter enlarges a texture, a minification filter reduces the texture to fit into a smaller area. Texture magnification repeats the sample texel for one or more addresses which yields a blurrier image. Texture minification is more complicated because it requires combining more than one texel value into a single value. This can cause aliasing or jagged edges depending on the texture data. The most popular approach for minification is to use a mipmap. A mipmap is a multi-level texture. The size of each level is a power-of-two smaller than the previous level down to a 1x1 texture. When minification is used, a game chooses the mipmap level closest to the size that is needed at render time. 

### BasicLoader

__BasicLoader__ is a simple loader class that provides support for loading shaders, textures, and meshes from files on disk. It provides both synchronous and asynchronous methods. In this game sample, the BasicLoader.h/.cpp files are found in the __Commons__ folder.

For more information, see [Basic Loader](complete-code-for-basicloader.md).