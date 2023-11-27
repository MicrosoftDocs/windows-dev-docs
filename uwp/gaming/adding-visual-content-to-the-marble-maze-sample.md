---
title: Adding visual content to the Marble Maze sample
description: This document describes how the Marble Maze game uses Direct3D and Direct2D in the Universal Windows Platform (UWP) app environment so that you can learn the patterns and adapt them when you work with your own game content.
ms.assetid: 6e43422e-e1a1-b79e-2c4b-7d5b4fa88647
ms.date: 09/08/2017
ms.topic: article
keywords: windows 10, uwp, games, sample, directx, graphics
ms.localizationpriority: medium
---
# Adding visual content to the Marble Maze sample




This document describes how the Marble Maze game uses Direct3D and Direct2D in the Universal Windows Platform (UWP) app environment so that you can learn the patterns and adapt them when you work with your own game content. To learn how visual game components fit in the overall application structure of Marble Maze, see [Marble Maze application structure](marble-maze-application-structure.md).

We followed these basic steps as we developed the visual aspects of Marble Maze:

1.  Create a basic framework that initializes the Direct3D and Direct2D environments.
2.  Use image and model editing programs to design the 2D and 3D assets that appear in the game.
3.  Ensure that 2D and 3D assets properly load and appear in the game.
4.  Integrate vertex and pixel shaders that enhance the visual quality of the game assets.
5.  Integrate game logic, such as animation and user input.

We also focused first on adding 3D assets and then on 2D assets. For example, we focused on core game logic before we added the menu system and timer.

We also needed to iterate through some of these steps multiple times during the development process. For example, as we made changes to the mesh and marble models, we had to also change some of the shader code that supports those models.

> [!NOTE]
> The sample code that corresponds to this document is found in the [DirectX Marble Maze game sample](https://github.com/microsoft/Windows-appsample-marble-maze).

 
Here are some of the key points that this document discusses for when you work with DirectX and visual game content, namely, when you initialize the DirectX graphics libraries, load scene resources, and update and render the scene:

-   Adding game content typically involves many steps. These steps also often require iteration. Game developers often focus first on adding 3D game content and then on adding 2D content.
-   Reach more customers and give them all a great experience by supporting the greatest range of graphics hardware as possible.
-   Cleanly separate design-time and run-time formats. Structure your design-time assets to maximize flexibility and enable rapid iterations on content. Format and compress your assets to load and render as efficiently as possible at run time.
-   You create the Direct3D and Direct2D devices in a UWP app much like you do in a classic Windows desktop app. One important difference is how the swap chain is associated with the output window.
-   When you design your game, ensure that the mesh format that you choose supports your key scenarios. For example, if your game requires collision, make sure that you can obtain collision data from your meshes.
-   Separate game logic from rendering logic by first updating all scene objects before you render them.
-   You typically draw your 3D scene objects, and then any 2D objects that appear in front of the scene.
-   Synchronize drawing to the vertical blank to ensure that your game does not spend time drawing frames that will never be actually shown on the display. A *vertical blank* is the time between when one frame finishes drawing to the monitor and the next frame begins.

## Getting started with DirectX graphics


When we planned the Marble Maze Universal Windows Platform (UWP) game, we chose C++ and Direct3D 11.1 because they are excellent choices for creating 3D games that require maximum control over rendering and high performance. DirectX 11.1 supports hardware from DirectX 9 to DirectX 11, and therefore can help you reach more customers more efficiently because you don't have to rewrite code for each of the earlier DirectX versions.

Marble Maze uses Direct3D 11.1 to render the 3D game assets, namely the marble and the maze. Marble Maze also uses Direct2D, DirectWrite, and Windows Imaging Component (WIC) to draw the 2D game assets, such as the menus and the timer.

Game development requires planning. If you are new to DirectX graphics, we recommend that you read [DirectX: Getting started](directx-getting-started.md) to familiarize yourself with the basic concepts of creating a UWP DirectX game. As you read this document and work through the Marble Maze source code, you can refer to the following resources for more in-depth information about DirectX graphics:

-   [Direct3D 11 Graphics](/windows/desktop/direct3d11/atoc-dx-graphics-direct3d-11): Describes Direct3D 11, a powerful, hardware-accelerated 3D graphics API for rendering 3D geometry on the Windows platform.
-   [Direct2D](/windows/desktop/Direct2D/direct2d-portal): Describes Direct2D, a hardware-accelerated, 2D graphics API that provides high performance and high-quality rendering for 2D geometry, bitmaps, and text.
-   [DirectWrite](/windows/desktop/DirectWrite/direct-write-portal): Describes DirectWrite, which supports high-quality text rendering.
-   [Windows Imaging Component](/windows/desktop/wic/-wic-lh): Describes WIC, an extensible platform that provides low-level API for digital images.

### Feature levels

Direct3D 11 introduces a paradigm named *feature levels*. A feature level is a well-defined set of GPU functionality. Use feature levels to target your game to run on earlier versions of Direct3D hardware. Marble Maze supports feature level 9.1 because it requires no advanced features from the higher levels. We recommend that you support the greatest range of hardware possible and scale your game content so that your customers that have either high or low-end computers all have a great experience. For more information about feature levels, see [Direct3D 11 on Downlevel Hardware](/windows/desktop/direct3d11/overviews-direct3d-11-devices-downlevel).

## Initializing Direct3D and Direct2D


A device represents the display adapter. You create the Direct3D and Direct2D devices in a UWP app much like you do in a classic Windows desktop app. The main difference is how you connect the Direct3D swap chain to the windowing system.

The **DeviceResources** class is a foundation for managing Direct3D and Direct2D. This class handles general infrastructure, not game-specific assets. Marble Maze defines the **MarbleMazeMain** class to handle game-specific assets, which has a reference to a **DeviceResources** object to give it access to Direct3D and Direct2D.

During initialization, the **DeviceResources** constructor creates device-independent resources and the Direct3D and Direct2D devices.

```cpp
// Initialize the Direct3D resources required to run. 
DX::DeviceResources::DeviceResources() :
    m_screenViewport(),
    m_d3dFeatureLevel(D3D_FEATURE_LEVEL_9_1),
    m_d3dRenderTargetSize(),
    m_outputSize(),
    m_logicalSize(),
    m_nativeOrientation(DisplayOrientations::None),
    m_currentOrientation(DisplayOrientations::None),
    m_dpi(-1.0f),
    m_deviceNotify(nullptr)
{
    CreateDeviceIndependentResources();
    CreateDeviceResources();
}
```

The **DeviceResources** class separates this functionality so that it can more easily respond when the environment changes. For example, it calls the **CreateWindowSizeDependentResources** method when the window size changes.

###  Initializing the Direct2D, DirectWrite, and WIC factories

The **DeviceResources::CreateDeviceIndependentResources** method creates the factories for Direct2D, DirectWrite, and WIC. In DirectX graphics, factories are the starting points for creating graphics resources. Marble Maze specifies **D2D1\_FACTORY\_TYPE\_SINGLE\_THREADED** because it performs all drawing on the main thread.

```cpp
// These are the resources required independent of hardware. 
void DX::DeviceResources::CreateDeviceIndependentResources()
{
    // Initialize Direct2D resources.
    D2D1_FACTORY_OPTIONS options;
    ZeroMemory(&options, sizeof(D2D1_FACTORY_OPTIONS));

#if defined(_DEBUG)
    // If the project is in a debug build, enable Direct2D debugging via SDK Layers.
    options.debugLevel = D2D1_DEBUG_LEVEL_INFORMATION;
#endif

    // Initialize the Direct2D Factory.
    DX::ThrowIfFailed(
        D2D1CreateFactory(
            D2D1_FACTORY_TYPE_SINGLE_THREADED,
            __uuidof(ID2D1Factory2),
            &options,
            &m_d2dFactory
            )
        );

    // Initialize the DirectWrite Factory.
    DX::ThrowIfFailed(
        DWriteCreateFactory(
            DWRITE_FACTORY_TYPE_SHARED,
            __uuidof(IDWriteFactory2),
            &m_dwriteFactory
            )
        );

    // Initialize the Windows Imaging Component (WIC) Factory.
    DX::ThrowIfFailed(
        CoCreateInstance(
            CLSID_WICImagingFactory2,
            nullptr,
            CLSCTX_INPROC_SERVER,
            IID_PPV_ARGS(&m_wicFactory)
            )
        );
}
```

###  Creating the Direct3D and Direct2D devices

The **DeviceResources::CreateDeviceResources** method calls [D3D11CreateDevice](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) to create the device object that represents the Direct3D display adapter. Because Marble Maze supports feature level 9.1 and above, the **DeviceResources::CreateDeviceResources** method specifies levels 9.1 through 11.1 in the **featureLevels** array. Direct3D walks the list in order and gives the app the first feature level that is available. Therefore the **D3D\_FEATURE\_LEVEL** array entries are listed from highest to lowest so that the app will get the highest feature level available. The **DeviceResources::CreateDeviceResources** method obtains the Direct3D 11.1 device by querying the Direct3D 11 device that's returned from **D3D11CreateDevice**.

```cpp
// This flag adds support for surfaces with a different color channel ordering
// than the API default. It is required for compatibility with Direct2D.
UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;

#if defined(_DEBUG)
    if (DX::SdkLayersAvailable())
    {
        // If the project is in a debug build, enable debugging via SDK Layers 
        // with this flag.
        creationFlags |= D3D11_CREATE_DEVICE_DEBUG;
    }
#endif

// This array defines the set of DirectX hardware feature levels this app will support.
// Note the ordering should be preserved.
// Don't forget to declare your application's minimum required feature level in its
// description.  All applications are assumed to support 9.1 unless otherwise stated.
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
ComPtr<ID3D11DeviceContext> context;

HRESULT hr = D3D11CreateDevice(
    nullptr,                    // Specify nullptr to use the default adapter.
    D3D_DRIVER_TYPE_HARDWARE,   // Create a device using the hardware graphics driver.
    0,                          // Should be 0 unless the driver is D3D_DRIVER_TYPE_SOFTWARE.
    creationFlags,              // Set debug and Direct2D compatibility flags.
    featureLevels,              // List of feature levels this app can support.
    ARRAYSIZE(featureLevels),   // Size of the list above.
    D3D11_SDK_VERSION,          // Always set this to D3D11_SDK_VERSION for UWP apps.
    &device,                    // Returns the Direct3D device created.
    &m_d3dFeatureLevel,         // Returns feature level of device created.
    &context                    // Returns the device immediate context.
    );

if (FAILED(hr))
{
    // If the initialization fails, fall back to the WARP device.
    // For more information on WARP, see:
    // https://go.microsoft.com/fwlink/?LinkId=286690
    DX::ThrowIfFailed(
        D3D11CreateDevice(
            nullptr,
            D3D_DRIVER_TYPE_WARP, // Create a WARP device instead of a hardware device.
            0,
            creationFlags,
            featureLevels,
            ARRAYSIZE(featureLevels),
            D3D11_SDK_VERSION,
            &device,
            &m_d3dFeatureLevel,
            &context
            )
        );
}

// Store pointers to the Direct3D 11.1 API device and immediate context.
DX::ThrowIfFailed(
    device.As(&m_d3dDevice)
    );

DX::ThrowIfFailed(
    context.As(&m_d3dContext)
    );
```

The **DeviceResources::CreateDeviceResources** method then creates the Direct2D device. Direct2D uses Microsoft DirectX Graphics Infrastructure (DXGI) to interoperate with Direct3D. DXGI enables video memory surfaces to be shared between graphics runtimes. Marble Maze uses the underlying DXGI device from the Direct3D device to create the Direct2D device from the Direct2D factory.

```cpp
// Create the Direct2D device object and a corresponding context.
ComPtr<IDXGIDevice3> dxgiDevice;
DX::ThrowIfFailed(
    m_d3dDevice.As(&dxgiDevice)
    );

DX::ThrowIfFailed(
    m_d2dFactory->CreateDevice(dxgiDevice.Get(), &m_d2dDevice)
    );

DX::ThrowIfFailed(
    m_d2dDevice->CreateDeviceContext(
        D2D1_DEVICE_CONTEXT_OPTIONS_NONE,
        &m_d2dContext
        )
    );
```

For more information about DXGI and interoperability between Direct2D and Direct3D, see [DXGI Overview](/windows/desktop/direct3ddxgi/d3d10-graphics-programming-guide-dxgi) and [Direct2D and Direct3D Interoperability Overview](/windows/desktop/Direct2D/direct2d-and-direct3d-interoperation-overview).

### Associating Direct3D with the view

The **DeviceResources::CreateWindowSizeDependentResources** method creates the graphics resources that depend on a given window size such as the swap chain and Direct3D and Direct2D render targets. One important way that a DirectX UWP app differs from a desktop app is how the swap chain is associated with the output window. A swap chain is responsible for displaying the buffer to which the device renders on the monitor. [Marble Maze application structure](marble-maze-application-structure.md) describes how the windowing system for a UWP app differs from a desktop app. Because a UWP app does not work with [HWND](/windows/desktop/WinProg/windows-data-types) objects, Marble Maze must use the [IDXGIFactory2::CreateSwapChainForCoreWindow](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgifactory2-createswapchainforcorewindow) method to associate the device output to the view. The following example shows the part of the **DeviceResources::CreateWindowSizeDependentResources** method that creates the swap chain.

```cpp
// Obtain the final swap chain for this window from the DXGI factory.
DX::ThrowIfFailed(
    dxgiFactory->CreateSwapChainForCoreWindow(
        m_d3dDevice.Get(),
        reinterpret_cast<IUnknown*>(m_window.Get()),
        &swapChainDesc,
        nullptr,
        &m_swapChain
        )
    );
```

To minimize power consumption, which is important to do on battery-powered devices such as laptops and tablets, the **DeviceResources::CreateWindowSizeDependentResources** method calls the [IDXGIDevice1::SetMaximumFrameLatency](/windows/desktop/api/dxgi/nf-dxgi-idxgidevice1-setmaximumframelatency) method to ensure that the game is rendered only after the vertical blank. Synchronizing with the vertical blank is described in greater detail in the section [Presenting the scene](#presenting-the-scene) in this document.

```cpp
// Ensure that DXGI does not queue more than one frame at a time. This both 
// reduces latency and ensures that the application will only render after each
// VSync, minimizing power consumption.
DX::ThrowIfFailed(
    dxgiDevice->SetMaximumFrameLatency(1)
    );
```

The **DeviceResources::CreateWindowSizeDependentResources** method initializes graphics resources in a way that works for most games.

> [!NOTE]
> The term *view* has a different meaning in the Windows Runtime than it has in Direct3D. In the Windows Runtime, a view refers to the collection of user interface settings for an app, including the display area and the input behaviors, plus the thread it uses for processing. You specify the configuration and settings you need when you create a view. The process of setting up the app view is described in [Marble Maze application structure](marble-maze-application-structure.md).
> In Direct3D, the term view has multiple meanings. A resource view defines the subresources that a resource can access. For example, when a texture object is associated with a shader resource view, that shader can later access the texture. One advantage of a resource view is that you can interpret data in different ways at different stages in the rendering pipeline. For more information about resource views, see [Resource Views](/windows/desktop/direct3d11/overviews-direct3d-11-resources-intro).
> When used in the context of a view transform or view transform matrix, view refers to the location and orientation of the camera. A view transform relocates objects in the world around the camera’s position and orientation. For more information about view transforms, see [View Transform (Direct3D 9)](/windows/desktop/direct3d9/view-transform). How Marble Maze uses resource and matrix views is described in greater detail in this topic.

 

## Loading scene resources


Marble Maze uses the **BasicLoader** class, which is declared in **BasicLoader.h**, to load textures and shaders. Marble Maze uses the **SDKMesh** class to load the 3D meshes for the maze and the marble.

To ensure a responsive app, Marble Maze loads scene resources asynchronously, or in the background. As assets load in the background, your game can respond to window events. This process is explained in greater detail in [Loading game assets in the background](marble-maze-application-structure.md#loading-game-assets-in-the-background) in this guide.

###  Loading the 2D overlay and user interface

In Marble Maze, the overlay is the image that appears at the top of the screen. The overlay always appears in front of the scene. In Marble Maze, the overlay contains the Windows logo and the text string **DirectX Marble Maze game sample**. The management of the overlay is performed by the **SampleOverlay** class, which is defined in **SampleOverlay.h**. Although we use the overlay as part of the Direct3D samples, you can adapt this code to display any image that appears in front of your scene.

One important aspect of the overlay is that, because its contents do not change, the **SampleOverlay** class draws, or caches, its contents to an [ID2D1Bitmap1](/windows/desktop/api/d2d1_1/nn-d2d1_1-id2d1bitmap1) object during initialization. At draw time, the **SampleOverlay** class only has to draw the bitmap to the screen. In this way, expensive routines such as text drawing do not have to be performed for every frame.

The user interface (UI) consists of 2D components, such as menus and heads-up displays (HUDs), which appear in front of your scene. Marble Maze defines the following UI elements:

-   Menu items that enable the user to start the game or view high scores.
-   A timer that counts down for three seconds before play begins.
-   A timer that tracks the elapsed play time.
-   A table that lists the fastest finish times.
-   Text that reads **Paused** when the game is paused.

Marble Maze defines game-specific UI elements in **UserInterface.h**. Marble Maze defines the **ElementBase** class as a base type for all UI elements. The **ElementBase** class defines attributes such as the size, position, alignment, and visibility of a UI element. It also controls how elements are updated and rendered.

```cpp
class ElementBase
{
public:
    virtual void Initialize() { }
    virtual void Update(float timeTotal, float timeDelta) { }
    virtual void Render() { }

    void SetAlignment(AlignType horizontal, AlignType vertical);
    virtual void SetContainer(const D2D1_RECT_F& container);
    void SetVisible(bool visible);

    D2D1_RECT_F GetBounds();

    bool IsVisible() const { return m_visible; }

protected:
    ElementBase();

    virtual void CalculateSize() { }

    Alignment       m_alignment;
    D2D1_RECT_F     m_container;
    D2D1_SIZE_F     m_size;
    bool            m_visible;
};
```

By providing a common base class for UI elements, the **UserInterface** class, which manages the user interface, need only hold a collection of **ElementBase** objects, which simplifies UI management and provides a user interface manager that is reusable. Marble Maze defines types that derive from **ElementBase** that implement game-specific behaviors. For example, **HighScoreTable** defines the behavior for the high score table. For more info about these types, refer to the source code.

> [!NOTE]
> Because XAML enables you to more easily create complex user interfaces, like those found in simulation and strategy games, consider whether to use XAML to define your UI. For info about how to develop a user interface in XAML in a DirectX UWP game, see [Extend the game sample](tutorial-resources.md), which refers to the DirectX 3D shooting game sample.

 

###  Loading shaders

Marble Maze uses the **BasicLoader::LoadShader** method to load a shader from a file.

Shaders are the fundamental unit of GPU programming in games today. Nearly all 3D graphics processing is driven through shaders, whether it is model transformation and scene lighting, or more complex geometry processing, from character skinning to tessellation. For more information about the shader programming model, see [HLSL](/windows/desktop/direct3dhlsl/dx-graphics-hlsl).

Marble Maze uses vertex and pixel shaders. A vertex shader always operates on one input vertex and produces one vertex as output. A pixel shader takes numeric values, texture data, interpolated per-vertex values, and other data to produce a pixel color as output. Because a shader transforms one element at a time, graphics hardware that provides multiple shader pipelines can process sets of elements in parallel. The number of parallel pipelines that are available to the GPU can be vastly greater than the number that is available to the CPU. Therefore, even basic shaders can greatly improve throughput.

The **MarbleMazeMain::LoadDeferredResources** method loads one vertex shader and one pixel shader after it loads the overlay. The design-time versions of these shaders are defined in **BasicVertexShader.hlsl** and **BasicPixelShader.hlsl**, respectively. Marble Maze applies these shaders to both the ball and the maze during the rendering phase.

The Marble Maze project includes both .hlsl (the design-time format) and .cso (the run-time format) versions of the shader files. At build time, Visual Studio uses the fxc.exe effect-compiler to compile your .hlsl source file into a .cso binary shader. For more information about the effect-compiler tool, see [Effect-Compiler Tool](/windows/desktop/direct3dtools/fxc).

The vertex shader uses the supplied model, view and projection matrices to transform the input geometry. Position data from the input geometry is transformed and output twice: once in screen space, which is necessary for rendering, and again in world space to enable the pixel shader to perform lighting calculations. The surface normal vector is transformed to world space, which is also used by the pixel shader for lighting. The texture coordinates are passed through unchanged to the pixel shader.

```hlsl
sPSInput main(sVSInput input)
{
    sPSInput output;
    float4 temp = float4(input.pos, 1.0f);
    temp = mul(temp, model);
    output.worldPos = temp.xyz / temp.w;
    temp = mul(temp, view);
    temp = mul(temp, projection);
    output.pos = temp;
    output.tex = input.tex;
    output.norm = mul(float4(input.norm, 0.0f), model).xyz;
    return output;
}
```

The pixel shader receives the output of the vertex shader as input. This shader performs lighting calculations to mimic a soft-edged spotlight that hovers over the maze and is aligned with the position of the marble. Lighting is strongest for surfaces that point directly toward the light. The diffuse component tapers off to zero as the surface normal becomes perpendicular to the light, and the ambient term diminishes as the normal points away from the light. Points closer to the marble (and therefore closer to the center of the spotlight) are lit more strongly. However, lighting is modulated for points underneath the marble to simulate a soft shadow. In a real environment, an object like the white marble would diffusely reflect the spotlight onto other objects in the scene. This is approximated for the surfaces that are in view of the bright half of the marble. The additional illumination factors are in relative angle and distance to the marble. The resulting pixel color is a composition of the sampled texture with the result of the lighting calculations.

```hlsl
float4 main(sPSInput input) : SV_TARGET
{
    float3 lightDirection = float3(0, 0, -1);
    float3 ambientColor = float3(0.43, 0.31, 0.24);
    float3 lightColor = 1 - ambientColor;
    float spotRadius = 50;

    // Basic ambient (Ka) and diffuse (Kd) lighting from above.
    float3 N = normalize(input.norm);
    float NdotL = dot(N, lightDirection);
    float Ka = saturate(NdotL + 1);
    float Kd = saturate(NdotL);

    // Spotlight.
    float3 vec = input.worldPos - marblePosition;
    float dist2D = sqrt(dot(vec.xy, vec.xy));
    Kd = Kd * saturate(spotRadius / dist2D);

    // Shadowing from ball.
    if (input.worldPos.z > marblePosition.z)
        Kd = Kd * saturate(dist2D / (marbleRadius * 1.5));

    // Diffuse reflection of light off ball.
    float dist3D = sqrt(dot(vec, vec));
    float3 V = normalize(vec);
    Kd += saturate(dot(-V, N)) * saturate(dot(V, lightDirection))
        * saturate(marbleRadius / dist3D);

    // Final composite.
    float4 diffuseTexture = Texture.Sample(Sampler, input.tex);
    float3 color = diffuseTexture.rgb * ((ambientColor * Ka) + (lightColor * Kd));
    return float4(color * lightStrength, diffuseTexture.a);
}
```

> [!WARNING]
> The compiled pixel shader contains 32 arithmetic instructions and 1 texture instruction. This shader should perform well on desktop computers or higher-performing tablets. However, some computers might not be able to process this shader and still provide an interactive frame rate. Consider the typical hardware of your target audience and design your shaders to meet the capabilities of that hardware.

 

The **MarbleMazeMain::LoadDeferredResources** method uses the **BasicLoader::LoadShader** method to load the shaders. The following example loads the vertex shader. The run-time format for this shader is **BasicVertexShader.cso**. The **m\_vertexShader** member variable is an [ID3D11VertexShader](/windows/desktop/api/d3d11/nn-d3d11-id3d11vertexshader) object.

```cpp
BasicLoader^ loader = ref new BasicLoader(m_deviceResources->GetD3DDevice());

D3D11_INPUT_ELEMENT_DESC layoutDesc [] =
{
    { "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0 },
    { "NORMAL", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0 },
    { "TEXCOORD", 0, DXGI_FORMAT_R32G32_FLOAT, 0, 24, D3D11_INPUT_PER_VERTEX_DATA, 0 },
    { "TANGENT", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 32, D3D11_INPUT_PER_VERTEX_DATA, 0 },
};
m_vertexStride = 44; // must set this to match the size of layoutDesc above

Platform::String^ vertexShaderName = L"BasicVertexShader.cso";
loader->LoadShader(
    vertexShaderName,
    layoutDesc,
    ARRAYSIZE(layoutDesc),
    &m_vertexShader,
    &m_inputLayout
    );
```

The **m\_inputLayout** member variable is an [ID3D11InputLayout](/windows/desktop/api/d3d11/nn-d3d11-id3d11inputlayout) object. The input-layout object encapsulates the input state of the input assembler (IA) stage. One job of the IA stage is to make shaders more efficient by using system-generated values, also known as *semantics*, to process only those primitives or vertices that have not already been processed.

Use the [ID3D11Device::CreateInputLayout](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createinputlayout) method to create an input-layout from an array of input-element descriptions. The array contains one or more input elements; each input element describes one vertex-data element from one vertex buffer. The entire set of input-element descriptions describes all of the vertex-data elements from all of the vertex buffers that will be bound to the IA stage. 

**layoutDesc** in the above code snippet shows the layout description that Marble Maze uses. The layout description describes a vertex buffer that contains four vertex-data elements. The important parts of each entry in the array are the semantic name, data format, and byte offset . For example, the **POSITION** element specifies the vertex position in object space. It starts at byte offset 0 and contains three floating-point components (for a total of 12 bytes). The **NORMAL** element specifies the normal vector. It starts at byte offset 12 because it appears directly after **POSITION** in the layout, which requires 12 bytes. The **NORMAL** element contains a four-component, 32-bit unsigned-integer.

Compare the input layout with the **sVSInput** structure that is defined by the vertex shader, as shown in the following example. The **sVSInput** structure defines the **POSITION**, **NORMAL**, and **TEXCOORD0** elements. The DirectX runtime maps each element in the layout to the input structure that is defined by the shader.

```hlsl
struct sVSInput
{
    float3 pos : POSITION;
    float3 norm : NORMAL;
    float2 tex : TEXCOORD0;
};

struct sPSInput
{
    float4 pos : SV_POSITION;
    float3 norm : NORMAL;
    float2 tex : TEXCOORD0;
    float3 worldPos : TEXCOORD1;
};

sPSInput main(sVSInput input)
{
    sPSInput output;
    float4 temp = float4(input.pos, 1.0f);
    temp = mul(temp, model);
    output.worldPos = temp.xyz / temp.w;
    temp = mul(temp, view);
    temp = mul(temp, projection);
    output.pos = temp;
    output.tex = input.tex;
    output.norm = mul(float4(input.norm, 0.0f), model).xyz;
    return output;
}
```

The document [Semantics](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-semantics) describes each of the available semantics in greater detail.

> [!NOTE]
> In a layout, you can specify additional components that are not used to enable multiple shaders to share the same layout. For example, the **TANGENT** element is not used by the shader. You can use the **TANGENT** element if you want to experiment with techniques such as normal mapping. By using normal mapping, also known as bump mapping, you can create the effect of bumps on the surfaces of objects. For more information about bump mapping, see [Bump Mapping (Direct3D 9)](/windows/desktop/direct3d9/bump-mapping).

 

For more information about the input assembly stage, see [Input-Assembler Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-input-assembler-stage) and [Getting Started with the Input-Assembler Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-input-assembler-stage-getting-started).

The process of using the vertex and pixel shaders to render the scene are described in the section [Rendering the scene](#rendering-the-scene) later in this document.

### Creating the constant buffer

Direct3D buffer groups a collection of data. A constant buffer is a kind of buffer that you can use to pass data to shaders. Marble Maze uses a constant buffer to hold the model (or world) view, and the projection matrices for the active scene object.

The following example shows how the **MarbleMazeMain::LoadDeferredResources** method creates a constant buffer that will later hold matrix data. The example creates a **D3D11\_BUFFER\_DESC** structure that uses the **D3D11\_BIND\_CONSTANT\_BUFFER** flag to specify usage as a constant buffer. This example then passes that structure to the [ID3D11Device::CreateBuffer](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createbuffer) method. The **m\_constantBuffer** variable is an [ID3D11Buffer](/windows/desktop/api/d3d11/nn-d3d11-id3d11buffer) object.

```cpp
// Create the constant buffer for updating model and camera data.
D3D11_BUFFER_DESC constantBufferDesc = {0};

// Multiple of 16 bytes
constantBufferDesc.ByteWidth = ((sizeof(ConstantBuffer) + 15) / 16) * 16;

constantBufferDesc.Usage               = D3D11_USAGE_DEFAULT;
constantBufferDesc.BindFlags           = D3D11_BIND_CONSTANT_BUFFER;
constantBufferDesc.CPUAccessFlags      = 0;
constantBufferDesc.MiscFlags           = 0;

// This will not be used as a structured buffer, so this parameter is ignored.
constantBufferDesc.StructureByteStride = 0;

DX::ThrowIfFailed(
    m_deviceResources->GetD3DDevice()->CreateBuffer(
        &constantBufferDesc,
        nullptr,    // leave the buffer uninitialized
        &m_constantBuffer
        )
    );
```

The **MarbleMazeMain::Update** method later updates **ConstantBuffer** objects, one for the maze and one for the marble. The **MarbleMazeMain::Render** method then binds each **ConstantBuffer** object to the constant buffer before each object is rendered. The following example shows the **ConstantBuffer** structure, which is in **MarbleMazeMain.h**.

```cpp
// Describes the constant buffer that draws the meshes.
struct ConstantBuffer
{
    XMFLOAT4X4 model;
    XMFLOAT4X4 view;
    XMFLOAT4X4 projection;

    XMFLOAT3 marblePosition;
    float marbleRadius;
    float lightStrength;
};
```

To better understand how constant buffers map to shader code, compare the **ConstantBuffer** structure in **MarbleMazeMain.h** to the **ConstantBuffer** constant buffer that is defined by the vertex shader in **BasicVertexShader.hlsl**:

```hlsl
cbuffer ConstantBuffer : register(b0)
{
    matrix model;
    matrix view;
    matrix projection;
    float3 marblePosition;
    float marbleRadius;
    float lightStrength;
};
```

The layout of the **ConstantBuffer** structure matches the **cbuffer** object. The **cbuffer** variable specifies register b0, which means that the constant buffer data is stored in register 0. The **MarbleMazeMain::Render** method specifies register 0 when it activates the constant buffer. This process is described in greater detail later in this document.

For more information about constant buffers, see [Introduction to Buffers in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-intro). For more information about the register keyword, see [register](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-variable-register).

###  Loading meshes

Marble Maze uses SDK-Mesh as the run-time format because this format provides a basic way to load mesh data for sample applications. For production use, you should use a mesh format that meets the specific requirements of your game.

The **MarbleMazeMain::LoadDeferredResources** method loads mesh data after it loads the vertex and pixel shaders. A mesh is a collection of vertex data that often includes information such as positions, normal data, colors, materials, and texture coordinates. Meshes are typically created in 3D authoring software and maintained in files that are separate from application code. The marble and the maze are two examples of meshes that the game uses.

Marble Maze uses the **SDKMesh** class to manage meshes. This class is declared in **SDKMesh.h**. **SDKMesh** provides methods to load, render, and destroy mesh data.

> [!IMPORTANT]
> Marble Maze uses the SDK-Mesh format and provides the **SDKMesh** class for illustration only. Although the SDK-Mesh format is useful for learning, and for creating prototypes, it is a very basic format that might not meet the requirements of most game development. We recommend that you use a mesh format that meets the specific requirements of your game.

 

The following example shows how the **MarbleMazeMain::LoadDeferredResources** method uses the **SDKMesh::Create** method to load mesh data for the maze and for the ball.

```cpp
// Load the meshes.
DX::ThrowIfFailed(
    m_mazeMesh.Create(
        m_deviceResources->GetD3DDevice(),
        L"Media\\Models\\maze1.sdkmesh",
        false
        )
    );

DX::ThrowIfFailed(
    m_marbleMesh.Create(
        m_deviceResources->GetD3DDevice(),
        L"Media\\Models\\marble2.sdkmesh",
        false
        )
    );
```

###  Loading collision data

Although this section does not focus on how Marble Maze implements the physics simulation between the marble and the maze, note that mesh geometry for the physics system is read when the meshes are loaded.

```cpp
// Extract mesh geometry for the physics system.
DX::ThrowIfFailed(
    ExtractTrianglesFromMesh(
        m_mazeMesh,
        "Mesh_walls",
        m_collision.m_wallTriList
        )
    );

DX::ThrowIfFailed(
    ExtractTrianglesFromMesh(
        m_mazeMesh,
        "Mesh_Floor",
        m_collision.m_groundTriList
        )
    );

DX::ThrowIfFailed(
    ExtractTrianglesFromMesh(
        m_mazeMesh,
        "Mesh_floorSides",
        m_collision.m_floorTriList
        )
    );

m_physics.SetCollision(&m_collision);
float radius = m_marbleMesh.GetMeshBoundingBoxExtents(0).x / 2;
m_physics.SetRadius(radius);
```

The way that you load collision data largely depends on the run-time format that you use. For more information about how Marble Maze loads the collision geometry from an SDK-Mesh file, see the **MarbleMazeMain::ExtractTrianglesFromMesh** method in the source code.

## Updating game state


Marble Maze separates game logic from rendering logic by first updating all scene objects before rendering them.

[Marble Maze application structure](marble-maze-application-structure.md) describes the main game loop. Updating the scene, which is part of the game loop, happens after Windows events and input are processed and before the scene is rendered. The **MarbleMazeMain::Update** method handles the update of the UI and the game.

### Updating the user interface

The **MarbleMazeMain::Update** method calls the **UserInterface::Update** method to update the state of the UI.

```cpp
UserInterface::GetInstance().Update(
    static_cast<float>(m_timer.GetTotalSeconds()), 
    static_cast<float>(m_timer.GetElapsedSeconds()));
```

The **UserInterface::Update** method updates each element in the UI collection.

```cpp
void UserInterface::Update(float timeTotal, float timeDelta)
{
    for (auto iter = m_elements.begin(); iter != m_elements.end(); ++iter)
    {
        (*iter)->Update(timeTotal, timeDelta);
    }
}
```

Classes that derive from **ElementBase** (defined in **UserInterface.h**) implement the **Update** method to perform specific behaviors. For example, the **StopwatchTimer::Update** method updates the elapsed time by the provided amount and updates the text that it later displays.

```cpp
void StopwatchTimer::Update(float timeTotal, float timeDelta)
{
    if (m_active)
    {
        m_elapsedTime += timeDelta;

        WCHAR buffer[16];
        GetFormattedTime(buffer);
        SetText(buffer);
    }

    TextElement::Update(timeTotal, timeDelta);
}
```

###  Updating the scene

The **MarbleMazeMain::Update** method updates the game based on the current state of the state machine (the **GameState**, stored in **m_gameState**). When the game is in the active state (**GameState::InGameActive**), Marble Maze updates the camera to follow the marble, updates the view matrix part of the constant buffers, and updates the physics simulation.

The following example shows how the **MarbleMazeMain::Update** method updates the position of the camera. Marble Maze uses the **m\_resetCamera** variable to flag that the camera must be reset to be located directly above the marble. The camera is reset when the game starts or the marble falls through the maze. When the main menu or high score display screen is active, the camera is set at a constant location. Otherwise, Marble Maze uses the *timeDelta* parameter to interpolate the position of the camera between its current and target positions. The target position is slightly above and in front of the marble. Using the elapsed frame time enables the camera to gradually follow, or chase, the marble.

```cpp
static float eyeDistance = 200.0f;
static XMFLOAT3A eyePosition = XMFLOAT3A(0, 0, 0);

// Gradually move the camera above the marble.
XMFLOAT3A targetEyePosition;
XMStoreFloat3A(
    &targetEyePosition, 
    XMLoadFloat3A(&marblePosition) - (XMLoadFloat3A(&g) * eyeDistance));

if (m_resetCamera)
{
    eyePosition = targetEyePosition;
    m_resetCamera = false;
}
else
{
    XMStoreFloat3A(
        &eyePosition, 
        XMLoadFloat3A(&eyePosition) 
            + ((XMLoadFloat3A(&targetEyePosition) - XMLoadFloat3A(&eyePosition)) 
                * min(1, static_cast<float>(m_timer.GetElapsedSeconds()) * 8)
            )
    );
}

// Look at the marble. 
if ((m_gameState == GameState::MainMenu) || (m_gameState == GameState::HighScoreDisplay))
{
    // Override camera position for menus.
    XMStoreFloat3A(
        &eyePosition, 
        XMLoadFloat3A(&marblePosition) + XMVectorSet(75.0f, -150.0f, -75.0f, 0.0f));

    m_camera->SetViewParameters(
        eyePosition, 
        marblePosition, 
        XMFLOAT3(0.0f, 0.0f, -1.0f));
}
else
{
    m_camera->SetViewParameters(eyePosition, marblePosition, XMFLOAT3(0.0f, 1.0f, 0.0f));
}
```

The following example shows how the **MarbleMazeMain::Update** method updates the constant buffers for the marble and the maze. The maze’s model, or world, matrix always remains the identity matrix. Except for the main diagonal, whose elements are all ones, the identity matrix is a square matrix composed of zeros. The marble’s model matrix is based on its position matrix times its rotation matrix.

```cpp
// Update the model matrices based on the simulation.
XMStoreFloat4x4(&m_mazeConstantBufferData.model, XMMatrixIdentity());

XMStoreFloat4x4(
    &m_marbleConstantBufferData.model, 
    XMMatrixTranspose(
        XMMatrixMultiply(
            marbleRotationMatrix, 
            XMMatrixTranslationFromVector(XMLoadFloat3A(&marblePosition))
        )
    )
);

// Update the view matrix based on the camera.
XMFLOAT4X4 view;
m_camera->GetViewMatrix(&view);
m_mazeConstantBufferData.view = view;
m_marbleConstantBufferData.view = view;
```

For information about how the **MarbleMazeMain::Update** method reads user input and simulates the motion of the marble, see [Adding input and interactivity to the Marble Maze sample](adding-input-and-interactivity-to-the-marble-maze-sample.md).

## Rendering the scene


When a scene is rendered, these steps are typically included.

1.  Set the current render target depth-stencil buffer.
2.  Clear the render and stencil views.
3.  Prepare the vertex and pixel shaders for drawing.
4.  Render the 3D objects in the scene.
5.  Render any 2D object that you want to appear in front of the scene.
6.  Present the rendered image to the monitor.

The **MarbleMazeMain::Render** method binds the render target and depth stencil views, clears those views, draws the scene, and then draws the overlay.

###  Preparing the render targets

Before you render your scene, you must set the current render target depth-stencil buffer. If your scene is not guaranteed to draw over every pixel on the screen, also clear the render and stencil views. Marble Maze clears the render and stencil views on every frame to ensure that there are no visible artifacts from the previous frame.

The following example shows how the **MarbleMazeMain::Render** method calls the [ID3D11DeviceContext::OMSetRenderTargets](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-omsetrendertargets) method to set the render target and the depth-stencil buffer as the current ones.

```cpp
auto context = m_deviceResources->GetD3DDeviceContext();

// Reset the viewport to target the whole screen.
auto viewport = m_deviceResources->GetScreenViewport();
context->RSSetViewports(1, &viewport);

// Reset render targets to the screen.
ID3D11RenderTargetView *const targets[1] = 
    { m_deviceResources->GetBackBufferRenderTargetView() };

context->OMSetRenderTargets(1, targets, m_deviceResources->GetDepthStencilView());

// Clear the back buffer and depth stencil view.
context->ClearRenderTargetView(
    m_deviceResources->GetBackBufferRenderTargetView(), 
    DirectX::Colors::Black);

context->ClearDepthStencilView(
    m_deviceResources->GetDepthStencilView(), 
    D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 
    1.0f, 
    0);
```

The [ID3D11RenderTargetView](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) and [ID3D11DepthStencilView](/windows/desktop/api/d3d11/nn-d3d11-id3d11depthstencilview) interfaces support the texture view mechanism that is provided by Direct3D 10 and later. For more information about texture views, see [Texture Views (Direct3D 10)](/windows/desktop/direct3d10/d3d10-graphics-programming-guide-resources-access-views). The [OMSetRenderTargets](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-omsetrendertargets) method prepares the output-merger stage of the Direct3D pipeline. For more information about the output-merger stage, see [Output-Merger Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-output-merger-stage).

### Preparing the vertex and pixel shaders

Before you render the scene objects, perform the following steps to prepare the vertex and pixel shaders for drawing:

1.  Set the shader input layout as the current layout.
2.  Set the vertex and pixel shaders as the current shaders.
3.  Update any constant buffers with data that you have to pass to the shaders.

> [!IMPORTANT]
> Marble Maze uses one pair of vertex and pixel shaders for all 3D objects. If your game uses more than one pair of shaders, you must perform these steps each time you draw objects that use different shaders. To reduce the overhead that is associated with changing the shader state, we recommend that you group render calls for all objects that use the same shaders.

 

The section [Loading shaders](#loading-shaders) in this document describes how the input layout is created when the vertex shader is created. The following example shows how the **MarbleMazeMain::Render** method uses the [ID3D11DeviceContext::IASetInputLayout](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetinputlayout) method to set this layout as the current layout.

```cpp
m_deviceResources->GetD3DDeviceContext()->IASetInputLayout(m_inputLayout.Get());
```

The following example shows how the **MarbleMazeMain::Render** method uses the [ID3D11DeviceContext::VSSetShader](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-vssetshader) and [ID3D11DeviceContext::PSSetShader](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-pssetshader) methods to set the vertex and pixel shaders as the current shaders, respectively.

```cpp
// Set the vertex shader stage state.
m_deviceResources->GetD3DDeviceContext()->VSSetShader(
    m_vertexShader.Get(),   // use this vertex shader
    nullptr,                // don't use shader linkage
    0);                     // don't use shader linkage

m_deviceResources->GetD3DDeviceContext()->PSSetShader(
    m_pixelShader.Get(),    // use this pixel shader
    nullptr,                // don't use shader linkage
    0);                     // don't use shader linkage

m_deviceResources->GetD3DDeviceContext()->PSSetSamplers(
    0,                          // starting at the first sampler slot
    1,                          // set one sampler binding
    m_sampler.GetAddressOf());  // to use this sampler
```

After **MarbleMazeMain::Render** sets the shaders and their input layout, it uses the [ID3D11DeviceContext::UpdateSubresource](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-updatesubresource) method to update the constant buffer with the model, view, and projection matrices for the maze. The **UpdateSubresource** method copies the matrix data from CPU memory to GPU memory. Recall that the model and view components of the **ConstantBuffer** structure are updated in the **MarbleMazeMain::Update** method. The **MarbleMazeMain::Render** method then calls the [ID3D11DeviceContext::VSSetConstantBuffers](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-vssetconstantbuffers) and [ID3D11DeviceContext::PSSetConstantBuffers](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-pssetconstantbuffers) methods to set this constant buffer as the current one.

```cpp
// Update the constant buffer with the new data.
m_deviceResources->GetD3DDeviceContext()->UpdateSubresource(
    m_constantBuffer.Get(),
    0,
    nullptr,
    &m_mazeConstantBufferData,
    0,
    0);

m_deviceResources->GetD3DDeviceContext()->VSSetConstantBuffers(
    0,                                  // starting at the first constant buffer slot
    1,                                  // set one constant buffer binding
    m_constantBuffer.GetAddressOf());   // to use this buffer

m_deviceResources->GetD3DDeviceContext()->PSSetConstantBuffers(
    0,                                  // starting at the first constant buffer slot
    1,                                  // set one constant buffer binding
    m_constantBuffer.GetAddressOf());   // to use this buffer
```

The **MarbleMazeMain::Render** method performs similar steps to prepare the marble to be rendered.

### Rendering the maze and the marble

After you activate the current shaders, you can draw your scene objects. The **MarbleMazeMain::Render** method calls the **SDKMesh::Render** method to render the maze mesh.

```cpp
m_mazeMesh.Render(
    m_deviceResources->GetD3DDeviceContext(), 
    0, 
    INVALID_SAMPLER_SLOT, 
    INVALID_SAMPLER_SLOT);
```

The **MarbleMazeMain::Render** method performs similar steps to render the marble.

As mentioned earlier in this document, the **SDKMesh** class is provided for demonstration purposes, but we do not recommend it for use in a production-quality game. However, notice that the **SDKMesh::RenderMesh** method, which is called by **SDKMesh::Render**, uses the [ID3D11DeviceContext::IASetVertexBuffers](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetvertexbuffers) and [ID3D11DeviceContext::IASetIndexBuffer](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetindexbuffer) methods to set the current vertex and index buffers that define the mesh, and the [ID3D11DeviceContext::DrawIndexed](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-drawindexedinstanced) method to draw the buffers. For more information about how to work with vertex and index buffers, see [Introduction to Buffers in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-intro).

### Drawing the user interface and overlay

After drawing 3D scene objects, Marble Maze draws the 2D UI elements that appear in front of the scene.

The **MarbleMazeMain::Render** method ends by drawing the user interface and the overlay.

```cpp
// Draw the user interface and the overlay.
UserInterface::GetInstance().Render(m_deviceResources->GetOrientationTransform2D());

m_deviceResources->GetD3DDeviceContext()->BeginEventInt(L"Render Overlay", 0);
m_sampleOverlay->Render();
m_deviceResources->GetD3DDeviceContext()->EndEvent();
```

The **UserInterface::Render** method uses an [ID2D1DeviceContext](/windows/desktop/api/d2d1_1/nn-d2d1_1-id2d1devicecontext) object to draw the UI elements. This method sets the drawing state, draws all active UI elements, and then restores the previous drawing state.

```cpp
void UserInterface::Render(D2D1::Matrix3x2F orientation2D)
{
    m_d2dContext->SaveDrawingState(m_stateBlock.Get());
    m_d2dContext->BeginDraw();
    m_d2dContext->SetTransform(orientation2D);

    m_d2dContext->SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE_GRAYSCALE);

    for (auto iter = m_elements.begin(); iter != m_elements.end(); ++iter)
    {
        if ((*iter)->IsVisible())
            (*iter)->Render();
    }

    // We ignore D2DERR_RECREATE_TARGET here. This error indicates that the device
    // is lost. It will be handled during the next call to Present.
    HRESULT hr = m_d2dContext->EndDraw();
    if (hr != D2DERR_RECREATE_TARGET)
    {
        DX::ThrowIfFailed(hr);
    }

    m_d2dContext->RestoreDrawingState(m_stateBlock.Get());
}
```

The **SampleOverlay::Render** method uses a similar technique to draw the overlay bitmap.

###  Presenting the scene

After drawing all 2D and 3D scene objects, Marble Maze presents the rendered image to the monitor. It synchronizes drawing to the vertical blank to ensure that time is not spent time drawing frames that will never be actually shown on the display. Marble Maze also handles device changes when it presents the scene.

After the **MarbleMazeMain::Render** method returns, the game loop calls the **DX::DeviceResources::Present** method to send the rendered image to the monitor or display. The **DX::DeviceResources::Present** method calls [IDXGISwapChain::Present](/windows/desktop/api/dxgi/nf-dxgi-idxgiswapchain-present) to perform the present operation, as shown in the following example:

```cpp
// The first argument instructs DXGI to block until VSync, putting the application
// to sleep until the next VSync. This ensures we don't waste any cycles rendering
// frames that will never be displayed to the screen.
HRESULT hr = m_swapChain->Present(1, 0);
```

In this example, **m\_swapChain** is an [IDXGISwapChain1](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1) object. The initialization of this object is described in the section [Initializing Direct3D and Direct2D](#initializing-direct3d-and-direct2d) in this document.

The first parameter to [IDXGISwapChain::Present](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1), *SyncInterval*, specifies the number of vertical blanks to wait before presenting the frame. Marble Maze specifies 1 so that it waits until the next vertical blank.

The [IDXGISwapChain::Present](/windows/desktop/api/dxgi/nf-dxgi-idxgiswapchain-present) method returns an error code that indicates that the device was removed or otherwise failed. In this case, Marble Maze reinitializes the device.

```cpp
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
```

## Next steps


Read [Adding input and interactivity to the Marble Maze sample](adding-input-and-interactivity-to-the-marble-maze-sample.md) for information about some of the key practices to keep in mind when you work with input devices. This document discusses how Marble Maze supports touch, accelerometer, game controllers, and mouse input.

## Related topics


* [Adding input and interactivity to the Marble Maze sample](adding-input-and-interactivity-to-the-marble-maze-sample.md)
* [Marble Maze application structure](marble-maze-application-structure.md)
* [Developing Marble Maze, a UWP game in C++ and DirectX](developing-marble-maze-a-windows-store-game-in-cpp-and-directx.md)

 

 