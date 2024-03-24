---
title: Important changes from Direct3D 9 to Direct3D 11
description: This topic explains the high-level differences between DirectX 9 and DirectX 11.
ms.assetid: 35a9e388-b25e-2aac-0534-577b15dae364
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, direct3d 9, direct3d 11, changes
ms.localizationpriority: medium
---
# Important changes from Direct3D 9 to Direct3D 11



**Summary**

-   [Plan your DirectX port](plan-your-directx-port.md)
-   Important changes from Direct3D 9 to Direct3D 11
-   [Feature mapping](feature-mapping.md)


This topic explains the high-level differences between DirectX 9 and DirectX 11.

Direct3D 11 is fundamentally the same type of API as Direct3D 9 - a low-level, virtualized interface into graphics hardware. It still allows you to perform graphics drawing operations on a variety of hardware implementations. The layout of the graphics API has changed since Direct3D 9; the concept of a device context has been expanded, and an API has been added specifically for graphics infrastructure. Resources stored on the Direct3D device have a novel mechanism for data polymorphism called a resource view.

## Core API functions


In Direct3D 9 you had to create an interface to the Direct3D API before you could start using it. In your Direct3D 11 Universal Windows Platform (UWP) game, you call a static function called [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) to create the device and the device context.

## Devices and device context


A Direct3D 11 device represents a virtualized graphics adapter. It's used to create resources in video memory, for example: uploading textures to the GPU, creating views on texture resources and swap chains, and creating texture samplers. For a complete list of what a Direct3D 11 device interface is used for see [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device) and [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1).

A Direct3D 11. device context is used to set pipeline state and generate rendering commands. For example, a Direct3D 11 rendering chain uses a device context to set up the rendering chain and draw the scene (see below). The device context is used to access (map) video memory used by Direct3D device resources; it's also used to update subresource data, for example constant buffer data. For a complete list of what a Direct3D 11 device context is used for see [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) and [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1). Note that most of our samples use an immediate context to render directly to the device, but Direct3D 11 also supports deferred device contexts, which are primarily used for multithreading.

In Direct3D 11, the device handle and device context handle are both obtained by calling [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice). This method is also where you request a specific set of hardware features and retrieve information on Direct3D feature levels supported by the graphics adapter. See [Introduction to a Device in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-devices-intro) for more info on devices, device contexts, and threading considerations.

## Device infrastructure, frame buffers, and render target views


In Direct3D 11, the device adapter and hardware configuration are set with the DirectX Graphics Infrastructure (DXGI) API using the [**IDXGIAdapter**](/windows/desktop/api/dxgi/nn-dxgi-idxgiadapter) and [**IDXGIDevice1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgidevice2) COM interfaces. Buffers and other window resources (visible or offscreen) are created and configured by specific DXGI interfaces; the [**IDXGIFactory2**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgifactory2) factory pattern implementation acquires DXGI resources such as the frame buffer. Since DXGI owns the swap chain, a DXGI interface is used to present frames to the screen - see [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1).

Use [**IDXGIFactory2**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgifactory2) to create a swap chain compatible with your game. You need to create a swap chain for a core window, or for composition (XAML interop), instead of creating a swap chain for an HWND.

## Device resources and resource views


Direct3D 11 supports an additional level of polymorphism on video memory resources known as views. Essentially, where you had a single Direct3D 9 object for a texture, you now have two objects: the texture resource, which holds the data, and the resource view, which indicates how the view is used for rendering. A view based on a resource enables that resource to be used for a specific purpose. For example, a 2D texture resource is created as an [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d), then a shader resource view ([**ID3D11ShaderResourceView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11shaderresourceview)) is created on it so it can be used as a texture in a shader. A render target view ([**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview)) can also be created on the same 2D texture resource so that it can be used as a drawing surface. In another example, the same pixel data is represented in 2 different pixel formats by using 2 separate views on a single texture resource.

The underlying resource must be created with properties that are compatible with the type of views that will be created from it. For example, if an [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) is applied to a surface, that surface is created with the [**D3D11\_BIND\_RENDER\_TARGET**](/windows/desktop/api/d3d11/ne-d3d11-d3d11_bind_flag) flag. The surface also has to have a DXGI surface format compatible with rendering (see [**DXGI\_FORMAT**](/windows/desktop/api/dxgiformat/ne-dxgiformat-dxgi_format)).

Most of the resources you use for rendering inherit from the [**ID3D11Resource**](/windows/desktop/api/d3d11/nn-d3d11-id3d11resource) interface, which inherits from [**ID3D11DeviceChild**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicechild). Vertex buffers, index buffers, constant buffers, and shaders are all Direct3D 11 resources. Input layouts and sampler states inherit directly from [**ID3D11DeviceChild**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicechild).

Resources views use a DXGI\_FORMAT enum value to indicate the pixel format. Not every D3DFMT is supported as a DXGI\_FORMAT. For example, there is no 24bpp RGB format in DXGI that is equivalent to D3DFMT\_R8G8B8. There are also not BGR equivalents to every RGB format (DXGI\_FORMAT\_R10G10B10A2\_UNORM is equivalent to D3DFMT\_A2B10G10R10, but there’s no direct equivalent to D3DFMT\_A2R10G10B10). You should plan to convert any content in these legacy formats to supported formats at build-time. For a complete list of DXGI formats see the [**DXGI\_FORMAT**](/windows/desktop/api/dxgiformat/ne-dxgiformat-dxgi_format) enumeration.

Direct3D device resources (and resource views) are created before the scene is rendered. Device contexts are used to set up the rendering chain, as explained below.

## Device context and the rendering chain


In Direct3D 9 and Direct3D 10.x, there was a single Direct3D device object which managed resource creation, state, and drawing. In Direct3D 11, the Direct3D device interface still manages resource creation, but all state and drawing operations are handled by using a Direct3D device context. Here's an example of how the device context ([**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) interface) is used to set up the rendering chain:

-   Set and clear render target views (and depth stencil view)
-   Set the vertex buffer, index buffer, and input layout for the input assembler stage (IA stage)
-   Bind vertex and pixel shaders to the pipeline
-   Bind constant buffers to shaders
-   Bind texture views and samplers to the pixel shader
-   Draw the scene

When one of the [**ID3D11DeviceContext::Draw**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-draw) methods is called, the scene is drawn on the render target view. When you're done will all your drawing the DXGI adapter is used to present the completed frame by calling [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1).

## State management


Direct3D 9 managed state settings with a large set of individual toggles set with the SetRenderState, SetSamplerState, and SetTextureStageState methods. Since Direct3D 11 does not support the legacy fixed-function pipeline, the SetTextureStageState is replaced by writing pixel shaders (PS). There is no equivalent to a Direct3D 9 state block. Direct3D 11 instead manages state through the use of 4 kinds of state objects which provide a more streamlined way to group the rendering state.

For example, instead of using SetRenderState with D3DRS\_ZENABLE, you create a DepthStencilState object with this and other related state settings and use it to change state while rendering.

When porting Direct3D 9 applications to state objects, be aware that your various state combinations are represented as immutable state objects. They should be created once and re-used as long as they are valid.

## Direct3D feature levels


Direct3D has a new mechanism for determining hardware support called feature levels. Feature levels simplify the task of figuring out what the graphics adapter can do by allowing you to request a well-defined set of GPU functionality. For example, the 9\_1 feature level implements the functionality provided by Direct3D 9 graphics adapters, including shader model 2.x. Since 9\_1 is the lowest feature level, you can expect all devices to support a vertex shader and a pixel shader, which were the same stages supported by the Direct3D 9 programmable shader model.

Your game will use [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) to create the Direct3D device and device context. When you call this function you provide a list of feature levels that your game can support. It will return the highest supported feature level from that list. For example if your game can use BC4/BC5 textures (a feature of DirectX 10 hardware), you would include at least 9\_1 and 10\_0 in the list of supported feature levels. If the game is running on DirectX 9 hardware and BC4/BC5 textures can't be used, then [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) will return 9\_1. Then your game can fall back to a different texture format (and smaller textures).

If you decide to extend your Direct3D 9 game to support higher Direct3D feature levels then it's better to finish porting your existing Direct3D 9 graphics code first. After you have your game working in Direct3D 11, it's easier to add additional rendering paths with enhanced graphics.

See [Direct3D feature levels](/windows/desktop/direct3d11/overviews-direct3d-11-devices-downlevel-intro) for a detailed explanation of feature level support. See [Direct3D 11 Features](/windows/desktop/direct3d11/direct3d-11-features) and [Direct3D 11.1 Features](/windows/desktop/direct3d11/direct3d-11-1-features) for a full list of Direct3D 11 features.

## Feature levels and the programmable pipeline


Hardware has continue to evolve since Direct3D 9, and several new optional stages have been added to the programmable graphics pipeline. The set of options you have for the graphics pipeline varies with the Direct3D feature level. Feature level 10.0 includes the geometry shader stage with optional stream out for multipass rendering on the GPU. Feature level 11\_0 include the hull shader and domain shader for use with hardware tessellation. Feature level 11\_0 also includes full support for DirectCompute shaders, while feature levels 10.x only include support for a limited form of DirectCompute.

All shaders are written in HLSL using a shader profile that corresponds to a Direct3D feature level. Shader profiles are upwards compatible, so an HLSL shader that compiles using vs\_4\_0\_level\_9\_1 or ps\_4\_0\_level\_9\_1 will work across all devices. Shader profiles are not downlevel compatible, so a shader compiled using vs\_4\_1 will only work on feature level 10\_1, 11\_0, or 11\_1 devices.

Direct3D 9 managed constants for shaders using a shared array with SetVertexShaderConstant and SetPixelShaderConstant. Direct3D 11 uses constant buffers, which are resources like a vertex buffer or index buffer. Constant buffers are designed to be updated efficiently. Instead of having all the shader contents organized into a single global array you organize your constants into logical groupings and manage them through one or more constant buffers. When you port your Direct3D 9 game to Direct3D 11, plan to organize your constant buffers so that you can update them appropriately. For example, group shader constants that aren't updated every frame into a separate constant buffer, so that you don't have to constantly upload that data to the graphics adapter along with your more dynamic shader constants.

> **Note**   Most Direct3D 9 applications made extensive use of shaders, but occasionally mixed in use of the legacy fixed-function behavior. Note that Direct3D 11 only uses a programmable shading model. The legacy fixed-function features of Direct3D 9 are deprecated.

 

 

 