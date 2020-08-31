---
title: Initialize Direct3D 11
description: Shows how to convert Direct3D 9 initialization code to Direct3D 11, including how to get handles to the Direct3D device and the device context and how to use DXGI to set up a swap chain.
ms.assetid: 1bd5e8b7-fd9d-065c-9ff3-1a9b1c90da29
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, direct3d 11, initialization, porting, direct3d 9
ms.localizationpriority: medium
---
# Initialize Direct3D 11



**Summary**

-   Part 1: Initialize Direct3D 11
-   [Part 2: Convert the rendering framework](simple-port-from-direct3d-9-to-11-1-part-2--rendering.md)
-   [Part 3: Port the game loop](simple-port-from-direct3d-9-to-11-1-part-3--viewport-and-game-loop.md)


Shows how to convert Direct3D 9 initialization code to Direct3D 11, including how to get handles to the Direct3D device and the device context and how to use DXGI to set up a swap chain. Part 1 of the [Port a simple Direct3D 9 app to DirectX 11 and Universal Windows Platform (UWP)](walkthrough--simple-port-from-direct3d-9-to-11-1.md) walkthrough.

## Initialize the Direct3D device


In Direct3D 9, we created a handle to the Direct3D device by calling [**IDirect3D9::CreateDevice**](/windows/desktop/api/d3d9/nf-d3d9-idirect3d9-createdevice). We started by getting a pointer to [**IDirect3D9 interface**](/windows/desktop/api/d3d9helper/nn-d3d9helper-idirect3d9) and we specified a number of parameters to control the configuration of the Direct3D device and the swap chain. Before doing this we called [**GetDeviceCaps**](/windows/desktop/api/wingdi/nf-wingdi-getdevicecaps) to make sure we weren't asking the device to do something it couldn't do.

Direct3D 9

```cpp
UINT32 AdapterOrdinal = 0;
D3DDEVTYPE DeviceType = D3DDEVTYPE_HAL;
D3DCAPS9 caps;
m_pD3D->GetDeviceCaps(AdapterOrdinal, DeviceType, &caps); // caps bits

D3DPRESENT_PARAMETERS params;
ZeroMemory(&params, sizeof(D3DPRESENT_PARAMETERS));

// Swap chain parameters:
params.hDeviceWindow = m_hWnd;
params.AutoDepthStencilFormat = D3DFMT_D24X8;
params.BackBufferFormat = D3DFMT_X8R8G8B8;
params.MultiSampleQuality = D3DMULTISAMPLE_NONE;
params.MultiSampleType = D3DMULTISAMPLE_NONE;
params.SwapEffect = D3DSWAPEFFECT_DISCARD;
params.Windowed = true;
params.PresentationInterval = 0;
params.BackBufferCount = 2;
params.BackBufferWidth = 0;
params.BackBufferHeight = 0;
params.EnableAutoDepthStencil = true;
params.Flags = 2;

m_pD3D->CreateDevice(
    0,
    D3DDEVTYPE_HAL,
    m_hWnd,
    64,
    &params,
    &m_pd3dDevice
    );
```

In Direct3D 11, the device context and graphics infrastructure is considered separate from the device itself. Initialization is divided into multiple steps.

First we create the device. We get a list of the feature levels the device supports - this informs most of what we need to know about the GPU. Also, we don't need to create an interface just to access Direct3D. Instead we use the [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) core API. This gives us a handle to the device and the device's immediate context. The device context is used to set pipeline state and generate rendering commands.

After creating the Direct3D 11 device and context, we can take advantage of COM pointer functionality to get the most recent version of the interfaces, which include additional capability and are always recommended.

> **Note**   D3D\_FEATURE\_LEVEL\_9\_1 (which corresponds to shader model 2.0) is the minimum level your Microsoft Store game is required to support. (Your game's ARM packages will fail certification if you don't support 9\_1.) If your game also includes a rendering path for shader model 3 features, then you should include D3D\_FEATURE\_LEVEL\_9\_3 in the array.

 

Direct3D 11

```cpp
// This flag adds support for surfaces with a different color channel 
// ordering than the API default. It is required for compatibility with
// Direct2D.
UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;

#if defined(_DEBUG)
// If the project is in a debug build, enable debugging via SDK Layers.
creationFlags |= D3D11_CREATE_DEVICE_DEBUG;
#endif

// This example only uses feature level 9.1.
D3D_FEATURE_LEVEL featureLevels[] = 
{
    D3D_FEATURE_LEVEL_9_1
};

// Create the Direct3D 11 API device object and a corresponding context.
ComPtr<ID3D11Device> device;
ComPtr<ID3D11DeviceContext> context;
D3D11CreateDevice(
    nullptr, // Specify nullptr to use the default adapter.
    D3D_DRIVER_TYPE_HARDWARE,
    nullptr,
    creationFlags,
    featureLevels,
    ARRAYSIZE(featureLevels),
    D3D11_SDK_VERSION, // UWP apps must set this to D3D11_SDK_VERSION.
    &device, // Returns the Direct3D device created.
    nullptr,
    &context // Returns the device immediate context.
    );

// Store pointers to the Direct3D 11.2 API device and immediate context.
device.As(&m_d3dDevice);

context.As(&m_d3dContext);
```

## Create a swap chain


Direct3D 11 includes a device API called DirectX graphics infrastructure (DXGI). The DXGI interface allows us to (for example) control how the swap chain is configured and set up shared devices. At this step in initializing Direct3D, we're going to use DXGI to create a swap chain. Since we created the device, we can follow an interface chain back to the DXGI adapter.

The Direct3D device implements a COM interface for DXGI. First we need to get that interface and use it to request the DXGI adapter hosting the device. Then we use the DXGI adapter to create a DXGI factory.

> **Note**   These are COM interfaces so your first response might be to use [**QueryInterface**](/windows/desktop/api/unknwn/nf-unknwn-iunknown-queryinterface(q_)). You should use [**Microsoft::WRL::ComPtr**](/cpp/windows/comptr-class) smart pointers instead. Then just call the [**As()**](/previous-versions/br230426(v=vs.140)) method, supplying an empty COM pointer of the correct interface type.

 

**Direct3D 11**

```cpp
ComPtr<IDXGIDevice2> dxgiDevice;
m_d3dDevice.As(&dxgiDevice);

// Then, the adapter hosting the device;
ComPtr<IDXGIAdapter> dxgiAdapter;
dxgiDevice->GetAdapter(&dxgiAdapter);

// Then, the factory that created the adapter interface:
ComPtr<IDXGIFactory2> dxgiFactory;
dxgiAdapter->GetParent(
    __uuidof(IDXGIFactory2),
    &dxgiFactory
    );
```

Now that we have the DXGI factory, we can use it to create the swap chain. Let's define the swap chain parameters. We need to specify the surface format; we'll choose [**DXGI\_FORMAT\_B8G8R8A8\_UNORM**](/windows/desktop/api/dxgiformat/ne-dxgiformat-dxgi_format) because it's compatible with Direct2D. We'll turn off display scaling, multisampling, and stereo rendering because they aren't used in this example. Since we are running directly in a CoreWindow we can leave the width and height set to 0 and get full-screen values automatically.

> **Note**   Always set the *SDKVersion* parameter to D3D11\_SDK\_VERSION for UWP apps.

 

**Direct3D 11**

```cpp
ComPtr<IDXGISwapChain1> swapChain;
dxgiFactory->CreateSwapChainForCoreWindow(
    m_d3dDevice.Get(),
    reinterpret_cast<IUnknown*>(window),
    &swapChainDesc,
    nullptr,
    &swapChain
    );
swapChain.As(&m_swapChain);
```

To ensure we aren't rendering more often than the screen can actually display, we set frame latency to 1 and use [**DXGI\_SWAP\_EFFECT\_FLIP\_SEQUENTIAL**](/windows/desktop/api/dxgi/ne-dxgi-dxgi_swap_effect). This saves power and is a store certification requirement; we'll learn more about presenting to the screen in part 2 of this walkthrough.

> **Note**   You can use multithreading (for example, [**ThreadPool**](/uwp/api/Windows.System.Threading) work items) to continue other work while the rendering thread is blocked.

 

**Direct3D 11**

```cpp
dxgiDevice->SetMaximumFrameLatency(1);
```

Now we can set up the back buffer for rendering.

## Configure the back buffer as a render target


First we have to get a handle to the back buffer. (Note that the back buffer is owned by the DXGI swap chain, whereas in DirectX 9 it was owned by the Direct3D device.) Then we tell the Direct3D device to use it as the render target by creating a render target *view* using the back buffer.

**Direct3D 11**

```cpp
ComPtr<ID3D11Texture2D> backBuffer;
m_swapChain->GetBuffer(
    0,
    __uuidof(ID3D11Texture2D),
    &backBuffer
    );

// Create a render target view on the back buffer.
m_d3dDevice->CreateRenderTargetView(
    backBuffer.Get(),
    nullptr,
    &m_renderTargetView
    );
```

Now the device context comes into play. We tell Direct3D to use our newly-created render target view by using the device context interface. We'll retrieve the width and height of the back buffer so that we can target the whole window as our viewport. Note that the back buffer is attached to the swap chain, so if the window size changes (for example, the user drags the game window to another monitor) the back buffer will need to be resized and some setup will need to be redone.

**Direct3D 11**

```cpp
D3D11_TEXTURE2D_DESC backBufferDesc = {0};
backBuffer->GetDesc(&backBufferDesc);

CD3D11_VIEWPORT viewport(
    0.0f,
    0.0f,
    static_cast<float>(backBufferDesc.Width),
    static_cast<float>(backBufferDesc.Height)
    );

m_d3dContext->RSSetViewports(1, &viewport);
```

Now that we have a device handle and a full-screen render target, we are ready to load and draw geometry. Continue to [Part 2: Rendering](simple-port-from-direct3d-9-to-11-1-part-2--rendering.md).

 

 