---
title: Compare EGL code to DXGI and Direct3D
description: The DirectX Graphics Interface (DXGI) and several Direct3D APIs serve the same role as EGL. This topic helps you understand DXGI and Direct3D 11 from the perspective of EGL.
ms.assetid: 90f5ecf1-dd5d-fea3-bed8-57a228898d2a
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, egl, dxgi, direct3d
ms.localizationpriority: medium
---
# Compare EGL code to DXGI and Direct3D




**Important APIs**

-   [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1)
-   [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1)
-   [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow)

The DirectX Graphics Interface (DXGI) and several Direct3D APIs serve the same role as EGL. This topic helps you understand DXGI and Direct3D 11 from the perspective of EGL.

DXGI and Direct3D, like EGL, provide methods to configure graphics resources, obtain a rendering context for your shaders to draw into, and to display the results in a window. However, DXGI and Direct3D have quite a few more options, and require more effort to set up correctly when porting from EGL.

> **Note**   This guidance is based off the Khronos Group's open specification for EGL 1.4, found here: [Khronos Native Platform Graphics Interface (EGL Version 1.4 - April 6, 2011) \[PDF\]](https://www.khronos.org/registry/egl/specs/eglspec.1.4.20110406.pdf). Differences in syntax specific to other platforms and development languages are not covered in this guidance.

 

## How does DXGI and Direct3D compare?


The big advantage of EGL over DXGI and Direct3D is that it is relatively simple to start drawing to a window surface. This is because OpenGL ES 2.0—and therefore EGL—is a specification implemented by multiple platform providers, whereas DXGI and Direct3D are a single reference that hardware vendor drivers must conform to. This means that Microsoft must implement a set of APIs that enable the broadest possible set of vendor features, rather than focusing on a functional subset offered by a specific vendor, or by combining vendor-specific setup commands into simpler APIs. On the other hand, Direct3D provides a single set of APIs that cover a very broad range of graphics hardware platforms and feature levels, and offer more flexibility for developers experienced with the platform.

Like EGL, DXGI and Direct3D provide APIs for the following behaviors:

-   Obtaining, and reading and writing to a frame buffer (called a "swap chain" in DXGI).
-   Associating the frame buffer with a UI window.
-   Obtaining and configuring rendering contexts in which to draw.
-   Issuing commands to the graphics pipeline for a specific rendering context.
-   Creating and managing shader resources, and associating them with a rendering content.
-   Rendering to specific render targets (such as textures).
-   Updating the window's display surface with the results of rendering with the graphics resources.

To see the basic Direct3D process for configuring the graphics pipeline, check out the DirectX 11 App (Universal Windows) template in Microsoft Visual Studio 2015. The base rendering class in it provides a good baseline for setting up the Direct3D 11 graphics infrastructure and configuring basic resources on it, as well as supporting Universal Windows Platform (UWP) app features such as screen rotation.

EGL has very few APIs relative to Direct3D 11, and navigating the latter can be a challenge if you aren't familiar with the naming and jargon particular to the platform. Here's a simple overview to help you get oriented.

First, review the basic EGL object to Direct3D interface mapping:

| EGL abstraction | Similar Direct3D representation                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
|-----------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **EGLDisplay**  | In Direct3D (for UWP apps), the display handle is obtained through the [**Windows::UI::CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) API (or the **ICoreWindowInterop** interface that exposes the HWND). The adapter and hardware configuration are set with the [**IDXGIAdapter**](/windows/desktop/api/dxgi/nn-dxgi-idxgiadapter) and [**IDXGIDevice1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgidevice2) COM interfaces, respectively.                                                                                                                                                                                                                                                           |
| **EGLSurface**  | In Direct3D, the buffers and other window resources (visible or offscreen) are created and configured by specific DXGI interfaces, including [**IDXGIFactory2**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgifactory2) (a factory pattern implementation used to acquire DXGI resources such as the[**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1) (display buffers). The [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) that represents the graphics device and its resources, is acquired with [**D3D11Device::CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice). For render targets, use the [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) interface. |
| **EGLContext**  | In Direct3D, you configure and issue commands to the graphics pipeline with the [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) interface.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| **EGLConfig**   | In Direct3D 11, you create and configure graphics resources such as a buffers, textures, stencils and shaders with methods on the [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) interface.                                                                                                                                                                                                                                                                                                                                                                                                                                                      |

 

Now, here's the most basic process for setting up a simple graphics display, resources and context in DXGI and Direct3D for a UWP app.

1.  Obtain a handle to the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object for the app's core UI thread by calling [**CoreWindow::GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread).
2.  For UWP apps, acquire a swap chain from the [**IDXGIAdapter2**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiadapter2) with [**IDXGIFactory2::CreateSwapChainForCoreWindow**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgifactory2-createswapchainforcorewindow), and pass it the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) reference you obtained in step 1. You will get an [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1) instance in return. Scope it to your renderer object and its rendering thread.
3.  Obtain [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) and [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) instances by calling the [**D3D11Device::CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) method. Scope them to your renderer object as well.
4.  Create shaders, textures, and other resources using methods on your renderer's [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) object.
5.  Define buffers, run shaders and manage the pipeline stages using methods on your renderer's [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) object.
6.  When the pipeline has executed and a frame is drawn to the back buffer, present it to the screen with [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1).

To examine this process in more detail, review [Getting started with DirectX graphics](/windows/desktop/getting-started-with-directx-graphics). The rest of this article covers many of the common steps for basic graphics pipeline setup and management.
> **Note**   Windows Desktop apps have different APIs for obtaining a Direct3D swap chain, such as [**D3D11Device::CreateDeviceAndSwapChain**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdeviceandswapchain), and do not use a [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object.

 

## Obtaining a window for display


In this example, eglGetDisplay is passed an HWND for a window resource specific to the Microsoft Windows platform. Other platforms, such as Apple's iOS (Cocoa) and Google's Android, have different handles or references to window resources, and may have different calling syntax altogether. After obtaining a display, you initialize it, set the preferred configuration, and create a surface with a back buffer you can draw into.

Obtaining a display and configuring it with EGL..

``` syntax
// Obtain an EGL display object.
EGLDisplay display = eglGetDisplay(GetDC(hWnd));
if (display == EGL_NO_DISPLAY)
{
  return EGL_FALSE;
}

// Initialize the display
if (!eglInitialize(display, &majorVersion, &minorVersion))
{
  return EGL_FALSE;
}

// Obtain the display configs
if (!eglGetConfigs(display, NULL, 0, &numConfigs))
{
  return EGL_FALSE;
}

// Choose the display config
if (!eglChooseConfig(display, attribList, &config, 1, &numConfigs))
{
  return EGL_FALSE;
}

// Create a surface
surface = eglCreateWindowSurface(display, config, (EGLNativeWindowType)hWnd, NULL);
if (surface == EGL_NO_SURFACE)
{
  return EGL_FALSE;
}
```

In Direct3D, a UWP app's main window is represented by the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object, which can be obtained from the app object by calling [**CoreWindow::GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread) as part of the initialization process of the "view provider" you construct for Direct3D. (If you are using Direct3D-XAML interop, you use the XAML framework's view provider.) The process for creating a Direct3D view provider is covered in [How to set up your app to display a view](/previous-versions/windows/apps/hh465077(v=win.10)).

Obtaining a CoreWindow for Direct3D.

``` syntax
CoreWindow::GetForCurrentThread();
```

Once the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) reference is obtained, the window must be activated, which executes the **Run** method of your main object and begins window event processing. After that, create an [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) and an [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1), and use them to get the underlying [**IDXGIDevice1**](/windows/desktop/api/dxgi/nn-dxgi-idxgidevice1) and [**IDXGIAdapter**](/windows/desktop/api/dxgi/nn-dxgi-idxgiadapter) so you can obtain an [**IDXGIFactory2**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgifactory2) object to create a swap chain resource based on your [**DXGI\_SWAP\_CHAIN\_DESC1**](/windows/desktop/api/dxgi1_2/ns-dxgi1_2-dxgi_swap_chain_desc1) configuration.

Configuring and setting the DXGI swap chain on the CoreWindow for Direct3D.

``` syntax
// Called when the CoreWindow object is created (or re-created).
void SimpleDirect3DApp::SetWindow(CoreWindow^ window)
{
  // Register event handlers with the CoreWindow object.
  // ...

  // Obtain your ID3D11Device1 and ID3D11DeviceContext1 objects
  // In this example, m_d3dDevice contains the scoped ID3D11Device1 object
  // ...

  ComPtr<IDXGIDevice1>  dxgiDevice;
  // Get the underlying DXGI device of the Direct3D device.
  m_d3dDevice.As(&dxgiDevice);

  ComPtr<IDXGIAdapter> dxgiAdapter;
  dxgiDevice->GetAdapter(&dxgiAdapter);

  ComPtr<IDXGIFactory2> dxgiFactory;
  dxgiAdapter->GetParent(
    __uuidof(IDXGIFactory2), 
    &dxgiFactory);

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

  // ...

  Windows::UI::Core::CoreWindow^ window = m_window.Get();
  dxgiFactory->CreateSwapChainForCoreWindow(
    m_d3dDevice.Get(),
    reinterpret_cast<IUnknown*>(window),
    &swapChainDesc,
    nullptr, // Allow on all displays.
    &m_swapChainCoreWindow);
}
```

Call the [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1) method after you prepare a frame in order to display it.

Note that in Direct3D 11, there isn't an abstraction identical to EGLSurface. (There is [**IDXGISurface1**](/windows/desktop/api/dxgi/nn-dxgi-idxgisurface1), but it is used differently.) The closest conceptual approximation is the [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) object that we use to assign a texture ([**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d)) as the back buffer that our shader pipeline will draw into.

Setting up the back buffer for the swap chain in Direct3D 11

``` syntax
ComPtr<ID3D11RenderTargetView>    m_d3dRenderTargetViewWin; // scoped to renderer object

// ...

ComPtr<ID3D11Texture2D> backBuffer2;
    
m_swapChainCoreWindow->GetBuffer(0, IID_PPV_ARGS(&backBuffer2));

m_d3dDevice->CreateRenderTargetView(
  backBuffer2.Get(),
  nullptr,
    &m_d3dRenderTargetViewWin);
```

A good practice is to call this code whenever the window is created or changes size. During rendering, set the render target view with [**ID3D11DeviceContext1::OMSetRenderTargets**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-omsetrendertargets) before setting up any other subresources like vertex buffers or shaders.

``` syntax
// Set the render target for the draw operation.
m_d3dContext->OMSetRenderTargets(
        1,
        d3dRenderTargetView.GetAddressOf(),
        nullptr);
```

## Creating a rendering context


In EGL 1.4, a "display" represents a set of window resources. Typically, you configure a "surface" for the display by supplying a set of attributes to the display object and getting a surface in return. You create a context for displaying the contents of the surface by creating that context and binding it to the surface and the display.

The call flow usually looks similar to this:

-   Call eglGetDisplay with the handle to a display or window resource and obtain a display object.
-   Initialize the display with eglInitialize.
-   Obtain the available display configuration and select one with eglGetConfigs and eglChooseConfig.
-   Create a window surface with eglCreateWindowSurface.
-   Create a display context for drawing with eglCreateContext.
-   Bind the display context to the display and the surface with eglMakeCurrent.

n the previous section, we created the EGLDisplay and the EGLSurface, and now we use the EGLDisplay to create a context and associate that context with the display, using the configured EGLSurface to parameterize the output.

Obtaining a rendering context with EGL 1.4

```cpp
// Configure your EGLDisplay and obtain an EGLSurface here ...
// ...

// Create a drawing context from the EGLDisplay
context = eglCreateContext(display, config, EGL_NO_CONTEXT, contextAttribs);
if (context == EGL_NO_CONTEXT)
{
  return EGL_FALSE;
}   
   
// Make the context current
if (!eglMakeCurrent(display, surface, surface, context))
{
  return EGL_FALSE;
}
```

A rendering context in Direct3D 11 is represented by an [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) object, which represents the adapter and allows you to create Direct3D resources such as buffers and shaders; and by the [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) object, which allows you to manage the graphics pipeline and execute the shaders.

Be aware of Direct3D feature levels! These are used to support older Direct3D hardware platforms, from DirectX 9.1 to DirectX 11. Many platforms that use low power graphics hardware, such as tablets, only have access to DirectX 9.1 features, and older supported graphics hardware could be from 9.1 through 11.

Creating a rendering context with DXGI and Direct3D

```cpp

// ... 

UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;
ComPtr<IDXGIDevice> dxgiDevice;

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

// Create the Direct3D 11 API device object and a corresponding context.
ComPtr<ID3D11Device> device;
ComPtr<ID3D11DeviceContext> d3dContext;

D3D11CreateDevice(
  nullptr, // Specify nullptr to use the default adapter.
  D3D_DRIVER_TYPE_HARDWARE,
  nullptr,
  creationFlags, // Set debug and Direct2D compatibility flags.
  featureLevels, // List of feature levels this app can support.
  ARRAYSIZE(featureLevels),
  D3D11_SDK_VERSION, // Always set this to D3D11_SDK_VERSION for UWP apps.
  &device, // Returns the Direct3D device created.
  &m_featureLevel, // Returns feature level of device created.
  &d3dContext // Returns the device immediate context.
);
```

## Drawing into a texture or pixmap resource


To draw into a texture with OpenGL ES 2.0, configure a pixel buffer, or PBuffer. After you successfully a configure and create an EGLSurface for it you can supply it with a rendering context and execute the shader pipeline to draw into the texture.

Draw into a pixel buffer with OpenGL ES 2.0

``` syntax
// Create a pixel buffer surface to draw into
EGLConfig pBufConfig;
EGLint totalpBufAttrs;

const EGLint pBufConfigAttrs[] =
{
    // Configure the pBuffer here...
};
 
eglChooseConfig(eglDsplay, pBufConfigAttrs, &pBufConfig, 1, &totalpBufAttrs);
EGLSurface pBuffer = eglCreatePbufferSurface(eglDisplay, pBufConfig, EGL_TEXTURE_RGBA); 
```

In Direct3D 11, you create an [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d) resource and makei it a render target. Configure the render target using [**D3D11\_RENDER\_TARGET\_VIEW\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_render_target_view_desc). When you call the [**ID3D11DeviceContext::Draw**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-draw) method(or a similar Draw\* operation on the device context) using this render target, the results are drawn into a texture.

Draw into a texture with Direct3D 11

``` syntax
ComPtr<ID3D11Texture2D> renderTarget1;

D3D11_RENDER_TARGET_VIEW_DESC renderTargetDesc = {0};
// Configure renderTargetDesc here ...

m_d3dDevice->CreateRenderTargetView(
  renderTarget1.Get(),
  nullptr,
  &m_d3dRenderTargetViewWin);

// Later, in your render loop...

// Set the render target for the draw operation.
m_d3dContext->OMSetRenderTargets(
        1,
        d3dRenderTargetView.GetAddressOf(),
        nullptr);
```

This texture can be passed to a shader if it is associated with an [**ID3D11ShaderResourceView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11shaderresourceview).

## Drawing to the screen


Once you have used your EGLContext to configure your buffers and update your data, you run the shaders bound to it and draw the results to the back buffer with glDrawElements. You display the back buffer by calling eglSwapBuffers.

Open GL ES 2.0: Drawing to the screen.

``` syntax
glDrawElements(GL_TRIANGLES, renderer->numIndices, GL_UNSIGNED_INT, 0);

eglSwapBuffers(drawContext->eglDisplay, drawContext->eglSurface);
```

In Direct3D 11, you configure your buffers and bind shaders with your [**IDXGISwapChain::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1). Then you call one of the [**ID3D11DeviceContext1::Draw**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-draw)\* methods to run the shaders and draw the results to a render target configured as the back buffer for the swap chain. After that, you simply present the back buffer to the display by calling **IDXGISwapChain::Present1**.

Direct3D 11: Drawing to the screen.

``` syntax

m_d3dContext->DrawIndexed(
        m_indexCount,
        0,
        0);

// ...

m_swapChainCoreWindow->Present1(1, 0, &parameters);
```

## Releasing graphics resources


In EGL, you release the window resources by passing the EGLDisplay to eglTerminate.

Terminating a display with EGL 1.4

```cpp
EGLBoolean eglTerminate(eglDisplay);
```

In a UWP app, you can close the CoreWindow with [**CoreWindow::Close**](/uwp/api/windows.ui.core.corewindow.close), although this can only be used for secondary UI windows. The primary UI thread and its associated CoreWindow cannot be closed; rather, they are expired by the operating system. However, when a secondary CoreWindow is closed, the [**CoreWindow::Closed**](/uwp/api/windows.ui.core.corewindow.closed) event is raised.

## API Reference mapping for EGL to Direct3D 11


| EGL API                          | Similar Direct3D 11 API or behavior                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
|----------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| eglBindAPI                       | N/A.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| eglBindTexImage                  | Call [**ID3D11Device::CreateTexture2D**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createtexture2d) to set a 2D texture.                                                                                                                                                                                                                                                                                                                                                                                          |
| eglChooseConfig                  | Direct3D does not supply a set of default frame buffer configurations. The swap chain's configuration                                                                                                                                                                                                                                                                                                                                                                                           |
| eglCopyBuffers                   | To copy a buffer data, call [**ID3D11DeviceContext::CopyStructureCount**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-copystructurecount). To copy a resource, call [**ID3DDeviceCOntext::CopyResource**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-copyresource).                                                                                                                                                                                                                                                      |
| eglCreateContext                 | Create a Direct3D device context by calling [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice), which returns both a handle to a Direct3D device and a default Direct3D immediate context ([**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) object). You can also create a Direct3D deferred context by calling [**ID3D11Device2::CreateDeferredContext**](/windows/desktop/api/d3d11_2/nf-d3d11_2-id3d11device2-createdeferredcontext2) on the returned [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) object. |
| eglCreatePbufferFromClientBuffer | All buffers are read and written as a Direct3D subresource, such as an [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d). Copy from one to another compatible subresource type with a methods such as [**ID3D11DeviceContext1:CopyResource**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-copyresource).                                                                                                                                                                                                     |
| eglCreatePbufferSurface          | To create a Direct3D device with no swap chain, call the static [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) method. For a Direct3D render target view, call [**ID3D11Device::CreateRenderTargetView**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createrendertargetview).                                                                                                                                                                                                                               |
| eglCreatePixmapSurface           | To create a Direct3D device with no swap chain, call the static [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) method. For a Direct3D render target view, call [**ID3D11Device::CreateRenderTargetView**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createrendertargetview).                                                                                                                                                                                                                               |
| eglCreateWindowSurface           | Ontain an [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1) (for the display buffers) and an [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) (a virtual interface for the graphics device and its resources). Use the **ID3D11Device1** to define an [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) that you can use to create the frame buffer you supply to the **IDXGISwapChain1**.                                                                                         |
| eglDestroyContext                | N/A. Use [**ID3D11DeviceContext::DiscardView1**](/windows/desktop/api/d3d11_1/nf-d3d11_1-id3d11devicecontext1-discardview1) to get rid of a render target view. To close the parent [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1), set the instance to null and wait for the platform to reclaim its resources. You cannot destroy the device context directly.                                                                                                                                                |
| eglDestroySurface                | N/A. Graphics resources are cleaned up when the UWP app's CoreWindow is closed by the platform.                                                                                                                                                                                                                                                                                                                                                                                                 |
| eglGetCurrentDisplay             | Call [**CoreWindow::GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread) to get a reference to the current main app window.                                                                                                                                                                                                                                                                                                                                                         |
| eglGetCurrentSurface             | This is the current [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview). Typically, this is scoped to your renderer object.                                                                                                                                                                                                                                                                                                                                                         |
| eglGetError                      | Errors are obtained as HRESULTs returned by most methods on DirectX interfaces. If the method does not return an HRESULT, call [**GetLastError**](/windows/desktop/api/errhandlingapi/nf-errhandlingapi-getlasterror). To convert a system error into an HRESULT value, use the [**HRESULT\_FROM\_WIN32**](/windows/desktop/api/winerror/nf-winerror-hresult_from_win32) macro.                                                                                                                                                                                                  |
| eglInitialize                    | Call [**CoreWindow::GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread) to get a reference to the current main app window.                                                                                                                                                                                                                                                                                                                                                         |
| eglMakeCurrent                   | Set a render target for drawing on the current context with [**ID3D11DeviceContext1::OMSetRenderTargets**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-omsetrendertargets).                                                                                                                                                                                                                                                                                                                                  |
| eglQueryContext                  | N/A. However, you may acquire rendering targets from an [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) instance, as well as some configuration data. (See the link for the list of available methods.)                                                                                                                                                                                                                                                                                           |
| eglQuerySurface                  | N/A. However, you may acquire data about viewports and the current graphics hardware from methods on an [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) instance. (See the link for the list of available methods.)                                                                                                                                                                                                                                                                               |
| eglReleaseTexImage               | N/A.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| eglReleaseThread                 | For general GPU multithreading, read [Multithreading](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-intro).                                                                                                                                                                                                                                                                                                                                                                              |
| eglSurfaceAttrib                 | Use [**D3D11\_RENDER\_TARGET\_VIEW\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_render_target_view_desc) to configure a Direct3D render target view,                                                                                                                                                                                                                                                                                                                                                               |
| eglSwapBuffers                   | Use [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1).                                                                                                                                                                                                                                                                                                                                                                                                                     |
| eglSwapInterval                  | See [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1).                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| eglTerminate                     | The CoreWindow used to display the output of the graphics pipeline is managed by the operating system.                                                                                                                                                                                                                                                                                                                                                                                          |
| eglWaitClient                    | For shared surfaces, use IDXGIKeyedMutex. For general GPU multithreading, read [Multithreading](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-intro).                                                                                                                                                                                                                                                                                                                                    |
| eglWaitGL                        | For shared surfaces, use IDXGIKeyedMutex. For general GPU multithreading, read [Multithreading](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-intro).                                                                                                                                                                                                                                                                                                                                    |
| eglWaitNative                    | For shared surfaces, use IDXGIKeyedMutex. For general GPU multithreading, read [Multithreading](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-intro).                                                                                                                                                                                                                                                                                                                                    |

 

 

 