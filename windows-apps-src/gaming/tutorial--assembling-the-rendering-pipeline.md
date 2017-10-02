---
author: joannaleecy
title: Assemble the rendering framework
description: Now, it's time to look at how the sample game uses that structure and state to display its graphics.
ms.assetid: 1da3670b-2067-576f-da50-5eba2f88b3e6
ms.author: joanlee
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, rendering
---

# Assemble the rendering framework



By now, you've seen how to structure a Universal Windows Platform (UWP) game to work with the Windows Runtime, and how to define a state machine to handle the flow of the game. Now, it's time to look at how the sample game uses that structure and state to display its graphics. Here, we look at how to implement a rendering framework, starting from the initialization of the graphics device through the presentation of the graphics objects for display.

## Objective


-   To understand how to set up a basic rendering framework to display the graphics output for a UWP DirectX game.

> **Note**   The following code files are not discussed here, but provide classes and methods referred to in this topic and are [provided as code at the end of this topic](#complete-sample-code-for-this-section):
-   **Animate.h/.cpp**.
-   **BasicLoader.h/.cpp**. Provides methods for loading meshes, shaders and textures, both synchronously and asynchronously. Very useful!
-   **MeshObject.h/.cpp**, **SphereMesh.h/.cpp**, **CylinderMesh.h/.cpp**, **FaceMesh.h/.cpp**, and **WorldMesh.h/.cpp**. Contains the definitions of the object primitives used in the game, such as the ammo spheres, the cylinder and cone obstacles, and the walls of the shooting gallery. (**GameObject.cpp**, briefly discussed in this topic, contains the method for rendering these primitives.)
-   **Level.h/.cpp** and **Level\[1-6\].h/.cpp**. Contains the configuration for each of the games six levels, including the success criteria and the number and position of the targets and obstacles.
-   **TargetTexture.h/.cpp**. Contains a set of methods for drawing the bitmaps used as the textures on the targets.

These files contain code that is not specific to UWP DirectX games. But you can review them separately if you'd like more implementation details.

 

This section covers three key files from the game sample ([provided as code at the end of this topic](#complete-sample-code-for-this-section)):

-   **Camera.h/.cpp**
-   **GameRenderer.h/.cpp**
-   **PrimObject.h/.cpp**

Again, we assume that you understand basic 3D programming concepts like meshes, vertices, and textures. For more info about Direct3D 11 programming in general, see [Programming Guide for Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/ff476345).
With that said, let's look at the work that must be done to put our game on the screen.

## An overview of the Windows Runtime and DirectX


DirectX is a fundamental part of the Windows Runtime and of the Windows 10 experience. All of Windows 10's visuals are built on top of DirectX, and you have the same direct line to the same low-level graphics interface, [DXGI](https://msdn.microsoft.com/library/windows/desktop/hh404534), which provides an abstraction layer for the graphics hardware and its drivers. All the Direct3D 11 APIs are available for you to talk to DXGI directly. The result is fast, high performing graphics in your games that give you access to all the latest graphics hardware features.

To add DirectX support to a UWP app, you create a view provider for DirectX resources by implementing the [**IFrameworkViewSource**](https://msdn.microsoft.com/library/windows/apps/hh700482) and [**IFrameworkView**](https://msdn.microsoft.com/library/windows/apps/hh700478) interfaces. These provide a factory pattern for your view provider type and the implementation of your DirectX view provider, respectively. The UWP singleton, represented by the [**CoreApplication**](https://msdn.microsoft.com/library/windows/apps/br225016) object, runs this implementation.

In [Defining the game's UWP framework](tutorial--building-the-games-metro-style-app-framework.md), we looked at how the renderer fit into the game sample's app framework. Now, let's look at how the game renderer connects to the view and builds the graphics that define the look of the game.

## Defining the renderer


The **GameRenderer** abstract type inherits from the **DirectXBase** renderer type, adds support for stereo 3-D, and declares constant buffers and resources for the shaders that create and define our graphic primitives.

Here's the definition of **GameRenderer**.

```cpp
ref class GameRenderer : public DirectXBase
{
internal:
    GameRenderer();

    virtual void Initialize(
        _In_ Windows::UI::Core::CoreWindow^ window,
        float dpi
        ) override;

    virtual void CreateDeviceIndependentResources() override;
    virtual void CreateDeviceResources() override;
    virtual void UpdateForWindowSizeChange() override;
    virtual void Render() override;
    virtual void SetDpi(float dpi) override;

    concurrency::task<void> CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game);
    void FinalizeCreateGameDeviceResources();
    concurrency::task<void> LoadLevelResourcesAsync();
    void FinalizeLoadLevelResources();

    GameInfoOverlay^ InfoOverlay()  { return m_gameInfoOverlay; };

    DirectX::XMFLOAT2 GameInfoOverlayUpperLeft()
    {
        return DirectX::XMFLOAT2(
            (m_windowBounds.Width  - GameInfoOverlayConstant::Width) / 2.0f,
            (m_windowBounds.Height - GameInfoOverlayConstant::Height) / 2.0f
            );
    };
    DirectX::XMFLOAT2 GameInfoOverlayLowerRight()
    {
        return DirectX::XMFLOAT2(
            (m_windowBounds.Width  - GameInfoOverlayConstant::Width) / 2.0f + GameInfoOverlayConstant::Width,
            (m_windowBounds.Height - GameInfoOverlayConstant::Height) / 2.0f + GameInfoOverlayConstant::Height
            );
    };

protected private:
    bool                                                m_initialized;
    bool                                                m_gameResourcesLoaded;
    bool                                                m_levelResourcesLoaded;
    GameInfoOverlay^                                    m_gameInfoOverlay;
    GameHud^                                            m_gameHud;
    Simple3DGame^                                       m_game;

    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_sphereTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_cylinderTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_ceilingTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_floorTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_wallsTexture;

    // Constant Buffers
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferNeverChanges;
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferChangeOnResize;
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferChangesEveryFrame;
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferChangesEveryPrim;
    Microsoft::WRL::ComPtr<ID3D11SamplerState>          m_samplerLinear;
    Microsoft::WRL::ComPtr<ID3D11VertexShader>          m_vertexShader;
    Microsoft::WRL::ComPtr<ID3D11VertexShader>          m_vertexShaderFlat;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>           m_pixelShader;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>           m_pixelShaderFlat;
    Microsoft::WRL::ComPtr<ID3D11InputLayout>           m_vertexLayout;
};
```

Because the Direct3D 11 APIs are defined as COM APIs, you must provide [**ComPtr**](https://msdn.microsoft.com/library/windows/apps/br244983) references to the objects defined by these APIs. These objects are automatically freed when their last reference goes out of scope when the app terminates.

The game sample declares 4 specific constant buffers:

-   **m\_constantBufferNeverChanges**. This constant buffer contains the lighting parameters. It's set one time and never changes again.
-   **m\_constantBufferChangeOnResize**. This constant buffer contains the projection matrix. The projection matrix is dependent on the size and aspect ratio of the window. It's updated only when the window size changes.
-   **m\_constantBufferChangesEveryFrame**. This constant buffer contains the view matrix. This matrix is dependent on the camera position and look direction (the normal to the projection) and changes only one time per frame.
-   **m\_constantBufferChangesEveryPrim**. This constant buffer contains the model matrix and material properties of each primitive. The model matrix transforms vertices from local coordinates into world coordinates. These constants are specific to each primitive and are updated for every draw call.

The whole idea of multiple constant buffers with different frequencies is to reduce the amount of data that must be sent to the GPU per frame. Therefore, the sample separates constants into different buffers based on the frequency that they must be updated. This is a best practice for Direct3D programming.

The renderer contains the shader objects that compute our primitives and textures: **m\_vertexShader** and **m\_pixelShader**. The vertex shader processes the primitives and the basic lighting, and the pixel shader (sometimes called a fragment shader) processes the textures and any per-pixel effects. There are two versions of these shaders (regular and flat) for rendering different primitives. The flat versions are much simpler and don't do specular highlights or any per pixel lighting effects. These are used for the walls and make rendering faster on lower powered devices.

The renderer class contains the [DirectWrite and Direct2D](https://msdn.microsoft.com/library/windows/desktop/ff729481) resources used for the overlay and the Heads Up Display (the **GameHud** object). The overlay and HUD are drawn on top of the render target when projection is complete in the graphics pipeline.

The renderer also defines the shader resource objects that hold the textures for the primitives. Some of these textures are pre-defined (DDS textures for the walls and floor of the world as well as the ammo spheres).

Now, it's time to see how this object is created!

## Initializing the renderer


The sample game calls this **Initialize** method as part of the CoreApplication initialization sequence in **App::SetWindow**.

```cpp
void GameRenderer::Initialize(
    _In_ CoreWindow^ window,
    float dpi
    )
{
    if (!m_initialized)
    {
        m_gameHud = ref new GameHud(
            "Windows 8 Samples",
            "DirectX first-person game sample"
            );
        m_gameInfoOverlay = ref new GameInfoOverlay();
        m_initialized = true;
    }

    DirectXBase::Initialize(window, dpi);

    // Initialize could be called multiple times as a result of an error with the hardware device
    // that requires it to be reinitialized.  Because the m_gameInfoOverlay variable has resources that are
    // dependent on the device, it will need to be reinitialized each time with the new device information.
    m_gameInfoOverlay->Initialize(m_d2dDevice.Get(), m_d2dContext.Get(), m_dwriteFactory.Get(), dpi);
}
```

This is a pretty straightforward method. It checks to see if the renderer had been previously initialized, and if it hasn't, it instantiates the **GameHud** and **GameInfoOverlay** objects.

After that, the renderer initialization process runs the base implementation of **Initialize** provided on the **DirectXBase** class it inherited from.

When the DirectXBase initialization completes, the **GameInfoOverlay** object is initialized. After initialization is complete, it's time to look at the methods for creating and loading the graphics resources for the game.

## Creating and loading DirectX graphics resources


The first order of business in any game is to establish a connection to our graphics interface, create the resources we need to draw the graphics, and then set up a render target into which we can draw those graphics. In the game sample (and in the Microsoft Visual Studio **DirectX 11 App (Universal Windows)** template), this process is implemented with three methods:

-   **CreateDeviceIndependentResources**
-   **CreateDeviceResources**
-   **CreateWindowSizeDependentResources**

Now, in the game sample, we override two of these methods (**CreateDeviceIndependentResources** and **CreateDeviceResources**) provided on the **DirectXBase** class implemented in the **DirectX 11 App (Universal Windows)** template. For each of these override methods, we first call the **DirectXBase** implementations they override, and then add more implementation details specific to the game sample. Be aware that the **DirectXBase** class implementation included with the game sample has been modified from the version provided in the Visual Studio template to include stereoscopic view support and includes pre-rotation of the **SwapBuffer** object.

**CreateWindowSizeDependentResources** is not overridden by the **GameRenderer** object. We use the implementation of it provided in the **DirectXBase** class.

For more info about the **DirectXBase** base implementations of these methods, see [How to set up your UWP DirectX app to display a view](https://msdn.microsoft.com/library/windows/apps/hh465077).

The first of these overridden methods, **CreateDeviceIndependentResources**, calls the **GameHud::CreateDeviceIndependentResources** method to create the [DirectWrite](https://msdn.microsoft.com/library/windows/desktop/dd368038) text resources that use the Segoe UI font, which is the font used by most UWP apps.

CreateDeviceIndependentResources

```cpp
void GameRenderer::CreateDeviceIndependentResources()
{
    DirectXBase::CreateDeviceIndependentResources();
    m_gameHud->CreateDeviceIndependentResources(m_dwriteFactory.Get(), m_wicFactory.Get());
}
```

```cpp
void GameHud::CreateDeviceIndependentResources(
    _In_ IDWriteFactory* dwriteFactory,
    _In_ IWICImagingFactory* wicFactory
    )
{
    m_dwriteFactory = dwriteFactory;
    m_wicFactory = wicFactory;

    DX::ThrowIfFailed(
        m_dwriteFactory->CreateTextFormat(
            L"Segoe UI",
            nullptr,
            DWRITE_FONT_WEIGHT_LIGHT,
            DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH_NORMAL,
            GameConstants::HudBodyPointSize,
            L"en-us",
            &m_textFormatBody
            )
        );
    DX::ThrowIfFailed(
        m_dwriteFactory->CreateTextFormat(
            L"Segoe UI Symbol",
            nullptr,
            DWRITE_FONT_WEIGHT_LIGHT,
            DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH_NORMAL,
            GameConstants::HudBodyPointSize,
            L"en-us",
            &m_textFormatBodySymbol
            )
        );
    DX::ThrowIfFailed(
        m_dwriteFactory->CreateTextFormat(
            L"Segoe UI Light",
            nullptr,
            DWRITE_FONT_WEIGHT_LIGHT,
            DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH_NORMAL,
            GameConstants::HudTitleHeaderPointSize,
            L"en-us",
            &m_textFormatTitleHeader
            )
        );
    DX::ThrowIfFailed(
        m_dwriteFactory->CreateTextFormat(
            L"Segoe UI Light",
            nullptr,
            DWRITE_FONT_WEIGHT_LIGHT,
            DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH_NORMAL,
            GameConstants::HudTitleBodyPointSize,
            L"en-us",
            &m_textFormatTitleBody
            )
        );

    DX::ThrowIfFailed(m_textFormatBody->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_LEADING));
    DX::ThrowIfFailed(m_textFormatBody->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_NEAR));
    DX::ThrowIfFailed(m_textFormatBodySymbol->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_LEADING));
    DX::ThrowIfFailed(m_textFormatBodySymbol->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_NEAR));
    DX::ThrowIfFailed(m_textFormatTitleHeader->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_LEADING));
    DX::ThrowIfFailed(m_textFormatTitleHeader->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_NEAR));
    DX::ThrowIfFailed(m_textFormatTitleBody->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_LEADING));
    DX::ThrowIfFailed(m_textFormatTitleBody->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_NEAR));
}
```

The sample uses four text formatters: two for title header and title body text, and two for body text. This is used in much of the overlay text.

The second method, **CreateDeviceResources**, loads the specific resources for the game that will be computed on the graphics device. Let's look at the code for this method.

CreateDeviceResources

```cpp
void GameRenderer::CreateDeviceResources()
{
    DirectXBase::CreateDeviceResources();

    m_gameHud->CreateDeviceResources(m_d2dContext.Get());

    if (m_game != nullptr)
    {
        // The initial invocation of CreateDeviceResources occurs
        // before the Game State is initialized when the device is first
        // being created, so that the inital loading screen can be displayed.
        // Subsequent invocations of CreateDeviceResources will be a result
        // of an error with the device that requires the resources to be
        // recreated.  In this case, the game state is already initialized
        // so the game device resources need to be recreated.

        // This sample doesn't gracefully handle all the async recreation
        // of resources so an exception is thrown.
        throw Platform::Exception::CreateException(
            DXGI_ERROR_DEVICE_REMOVED,
            "GameRenderer::CreateDeviceResources - Recreation of resources after TDR not available\n"
            );
    }
}
```

```cpp
void GameHud::CreateDeviceResources(_In_ ID2D1DeviceContext* d2dContext)
{
    auto location = Package::Current->InstalledLocation;
    Platform::String^ path = Platform::String::Concat(location->Path, "\\");
    path = Platform::String::Concat(path, "windows-sdk.png");

    ComPtr<IWICBitmapDecoder> wicBitmapDecoder;
    DX::ThrowIfFailed(
        m_wicFactory->CreateDecoderFromFilename(
            path->Data(),
            nullptr,
            GENERIC_READ,
            WICDecodeMetadataCacheOnDemand,
            &wicBitmapDecoder
            )
        );

    ComPtr<IWICBitmapFrameDecode> wicBitmapFrame;
    DX::ThrowIfFailed(
        wicBitmapDecoder->GetFrame(0, &wicBitmapFrame)
        );

    ComPtr<IWICFormatConverter> wicFormatConverter;
    DX::ThrowIfFailed(
        m_wicFactory->CreateFormatConverter(&wicFormatConverter)
        );

    DX::ThrowIfFailed(
        wicFormatConverter->Initialize(
            wicBitmapFrame.Get(),
            GUID_WICPixelFormat32bppPBGRA,
            WICBitmapDitherTypeNone,
            nullptr,
            0.0,
            WICBitmapPaletteTypeCustom  // The BGRA format has no palette so this value is ignored.
            )
        );

    double dpiX = 96.0f;
    double dpiY = 96.0f;
    DX::ThrowIfFailed(
        wicFormatConverter->GetResolution(&dpiX, &dpiY)
        );

    // Create D2D Resources.
    DX::ThrowIfFailed(
        d2dContext->CreateBitmapFromWicBitmap(
            wicFormatConverter.Get(),
            BitmapProperties(
                PixelFormat(DXGI_FORMAT_B8G8R8A8_UNORM, D2D1_ALPHA_MODE_PREMULTIPLIED),
                static_cast<float>(dpiX),
                static_cast<float>(dpiY)
                ),
            &m_logoBitmap
            )
        );

    m_logoSize = m_logoBitmap->GetSize();

    DX::ThrowIfFailed(
        d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::White),
            &m_textBrush
            )
        );
}
```

In this example, in normal execution, the **CreateDeviceResources** method just calls the base class method and then calls the **GameHud::CreateDeviceResources** method (also listed previously). If there's a problem later with the underlying graphics device, it might have to be re-initialized. In this case, the **CreateDeviceResources** method initiates a set of async tasks to create the game device resources. This is done through a sequence of two methods: a call to **CreateDeviceResourcesAsync**, and then, when it completes, **FinalizeCreateGameDeviceResources**.

CreateGameDeviceResourcesAsync and FinalizeCreateGameDeviceResources

```cpp
task<void> GameRenderer::CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game)
{
    // Set the Loading state to wait until any async resources have
    // been loaded before proceeding.
    m_game = game;

    // NOTE: Only the m_d3dDevice is used in this method.  It's expected
    // to not run on the same thread as the GameRenderer was created.
    // Create methods on the m_d3dDevice are free-threaded and are safe while any methods
    // in the m_d3dContext should only be used on a single thread and handled
    // in the FinalizeCreateGameDeviceResources method.

    D3D11_BUFFER_DESC bd;
    ZeroMemory(&bd, sizeof(bd));

    // Create the constant buffers.
    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
    bd.CPUAccessFlags = 0;
    bd.ByteWidth = (sizeof(ConstantBufferNeverChanges) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferNeverChanges)
        );

    bd.ByteWidth = (sizeof(ConstantBufferChangeOnResize) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferChangeOnResize)
        );

    bd.ByteWidth = (sizeof(ConstantBufferChangesEveryFrame) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferChangesEveryFrame)
        );

    bd.ByteWidth = (sizeof(ConstantBufferChangesEveryPrim) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferChangesEveryPrim)
        );

    D3D11_SAMPLER_DESC sampDesc;
    ZeroMemory(&sampDesc, sizeof(sampDesc));

    sampDesc.Filter = D3D11_FILTER_MIN_MAG_MIP_LINEAR;
    sampDesc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.AddressW = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.ComparisonFunc = D3D11_COMPARISON_NEVER;
    sampDesc.MinLOD = 0;
    sampDesc.MaxLOD = FLT_MAX;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateSamplerState(&sampDesc, &m_samplerLinear)
        );

    // Start the async tasks to load the shaders and textures.
    BasicLoader^ loader = ref new BasicLoader(m_d3dDevice.Get());

    std::vector<task<void>> tasks;

    uint32 numElements = ARRAYSIZE(PNTVertexLayout);
    tasks.push_back(loader->LoadShaderAsync("VertexShader.cso", PNTVertexLayout, numElements, &m_vertexShader, &m_vertexLayout));
    tasks.push_back(loader->LoadShaderAsync("VertexShaderFlat.cso", nullptr, numElements, &m_vertexShaderFlat, nullptr));
    tasks.push_back(loader->LoadShaderAsync("PixelShader.cso", &m_pixelShader));
    tasks.push_back(loader->LoadShaderAsync("PixelShaderFlat.cso", &m_pixelShaderFlat));

    // Make sure previous versions if any of the textures are released.
    m_sphereTexture = nullptr;
    m_cylinderTexture = nullptr;
    m_ceilingTexture = nullptr;
    m_floorTexture = nullptr;
    m_wallsTexture = nullptr;

    // Load Game specific Textures.
    tasks.push_back(loader->LoadTextureAsync("seafloor.dds", nullptr, &m_sphereTexture));
    tasks.push_back(loader->LoadTextureAsync("metal_texture.dds", nullptr, &m_cylinderTexture));
    tasks.push_back(loader->LoadTextureAsync("cellceiling.dds", nullptr, &m_ceilingTexture));
    tasks.push_back(loader->LoadTextureAsync("cellfloor.dds", nullptr, &m_floorTexture));
    tasks.push_back(loader->LoadTextureAsync("cellwall.dds", nullptr, &m_wallsTexture));
    tasks.push_back(create_task([]()
    {
        // Simulate loading additional resources.
        wait(GameConstants::InitialLoadingDelay);
    }));

    // Return the task group of all the async tasks for loading the shader and texture assets.
    return when_all(tasks.begin(), tasks.end());
}
```

```cpp
void GameRenderer::FinalizeCreateGameDeviceResources()
{
    // All asynchronously loaded resources have completed loading.
    // Now, associate all the resources with the appropriate
    // Game objects.
    // This method is expected to run in the same thread as the GameRenderer
    // was created. All work will happen behind the "Loading ..." screen after the
    // main loop has been entered.

    // Initialize the Constant buffer with the light positions.
    // These are handled here to ensure that the d3dContext is only
    // used in one thread.

    ConstantBufferNeverChanges constantBufferNeverChanges;
    constantBufferNeverChanges.lightPosition[0] = XMFLOAT4( 3.5f, 2.5f,  5.5f, 1.0f);
    constantBufferNeverChanges.lightPosition[1] = XMFLOAT4( 3.5f, 2.5f, -5.5f, 1.0f);
    constantBufferNeverChanges.lightPosition[2] = XMFLOAT4(-3.5f, 2.5f, -5.5f, 1.0f);
    constantBufferNeverChanges.lightPosition[3] = XMFLOAT4( 3.5f, 2.5f,  5.5f, 1.0f);
    constantBufferNeverChanges.lightColor = XMFLOAT4(0.25f, 0.25f, 0.25f, 1.0f);
    m_d3dContext->UpdateSubresource(m_constantBufferNeverChanges.Get(), 0, nullptr, &constantBufferNeverChanges, 0, 0);

    // For the targets, there are two unique generated textures.
    // Each texture image includes the number of the texture.
    // Make sure the 2-D rendering is occurring on the same thread
    // as the main rendering.

    TargetTexture^ textureGenerator = ref new TargetTexture(
        m_d3dDevice.Get(),
        m_d2dFactory.Get(),
        m_dwriteFactory.Get(),
        m_d2dContext.Get()
        );

    MeshObject^ cylinderMesh = ref new CylinderMesh(m_d3dDevice.Get(), 26);
    MeshObject^ targetMesh = ref new FaceMesh(m_d3dDevice.Get());
    MeshObject^ sphereMesh = ref new SphereMesh(m_d3dDevice.Get(), 26);

    Material^ cylinderMaterial = ref new Material(
        XMFLOAT4(0.8f, 0.8f, 0.8f, .5f),
        XMFLOAT4(0.8f, 0.8f, 0.8f, .5f),
        XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f),
        15.0f,
        m_cylinderTexture.Get(),
        m_vertexShader.Get(),
        m_pixelShader.Get()
        );

    Material^ sphereMaterial = ref new Material(
        XMFLOAT4(0.8f, 0.4f, 0.0f, 1.0f),
        XMFLOAT4(0.8f, 0.4f, 0.0f, 1.0f),
        XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f),
        50.0f,
        m_sphereTexture.Get(),
        m_vertexShader.Get(),
        m_pixelShader.Get()
        );

    auto objects = m_game->RenderObjects();

    // Attach the textures to the appropriate game objects.
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if ((*object)->TargetId() == GameConstants::WorldFloorId)
        {
           (*object)->NormalMaterial(
               ref new Material(
                    XMFLOAT4(0.5f, 0.5f, 0.5f, 1.0f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 1.0f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    150.0f,
                    m_floorTexture.Get(),
                    m_vertexShaderFlat.Get(),
                    m_pixelShaderFlat.Get()
                    )
                );
           (*object)->Mesh(ref new WorldFloorMesh(m_d3dDevice.Get()));
        }
        else if ((*object)->TargetId() == GameConstants::WorldCeilingId)
        {
           (*object)->NormalMaterial(
               ref new Material(
                    XMFLOAT4(0.5f, 0.5f, 0.5f, 1.0f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 1.0f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    150.0f,
                    m_ceilingTexture.Get(),
                    m_vertexShaderFlat.Get(),
                    m_pixelShaderFlat.Get()
                    )
                );
           (*object)->Mesh(ref new WorldCeilingMesh(m_d3dDevice.Get()));
        }
        else if ((*object)->TargetId() == GameConstants::WorldWallsId)
        {
           (*object)->NormalMaterial(
               ref new Material(
                    XMFLOAT4(0.5f, 0.5f, 0.5f, 1.0f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 1.0f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    150.0f,
                    m_wallsTexture.Get(),
                    m_vertexShaderFlat.Get(),
                    m_pixelShaderFlat.Get()
                    )
                );
           (*object)->Mesh(ref new WorldWallsMesh(m_d3dDevice.Get()));
        }
        else if (Cylinder^ cylinder = dynamic_cast<Cylinder^>(*object))
        {
            cylinder->Mesh(cylinderMesh);
            cylinder->NormalMaterial(cylinderMaterial);
        }
        else if (Face^ target = dynamic_cast<Face^>(*object))
        {
            const int bufferLength = 16;
            char16 str[bufferLength];
            int len = swprintf_s(str, bufferLength, L"%d", target->TargetId());
            Platform::String^ string = ref new Platform::String(str, len);

            ComPtr<ID3D11ShaderResourceView> texture;
            textureGenerator->CreateTextureResourceView(string, &texture);
            target->NormalMaterial(
                ref new Material(
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    5.0f,
                    texture.Get(),
                    m_vertexShader.Get(),
                    m_pixelShader.Get()
                    )
                );

            textureGenerator->CreateHitTextureResourceView(string, &texture);
            target->HitMaterial(
                ref new Material(
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    5.0f,
                    texture.Get(),
                    m_vertexShader.Get(),
                    m_pixelShader.Get()
                    )
                );

            target->Mesh(targetMesh);
        }
        else if (Sphere^ sphere = dynamic_cast<Sphere^>(*object))
        {
            sphere->Mesh(sphereMesh);
            sphere->NormalMaterial(sphereMaterial);
        }
    }

    // Ensure that the camera has been initialized with the right Projection
    // matrix.  The camera is not created at the time the first window resize event
    // occurs.
    m_game->GameCamera()->SetProjParams(
        XM_PI / 2, m_renderTargetSize.Width / m_renderTargetSize.Height,
        0.01f,
        100.0f
        );

    // Make sure that Projection matrix has been set in the constant buffer
    // now that all the resources are loaded.
    // DirectXBase is handling screen rotations directly to eliminate an unaligned
    // fullscreen copy.  As a result, it's necessary to post multiply the rotationTransform3D
    // matrix to the camera projection matrix.
    ConstantBufferChangeOnResize changesOnResize;
    XMStoreFloat4x4(
        &changesOnResize.projection,
            XMMatrixMultiply(
                XMMatrixTranspose(m_game->GameCamera()->Projection()),
                XMMatrixTranspose(XMLoadFloat4x4(&m_rotationTransform3D))
                )
        );

    m_d3dContext->UpdateSubresource(
        m_constantBufferChangeOnResize.Get(),
        0,
        nullptr,
        &changesOnResize,
        0,
        0
        );

    m_gameResourcesLoaded = true;
}
```

**CreateDeviceResourcesAsync** is a method that runs as a separate set of async tasks to load the game resources. Because it's expected to run on a separate thread, it only has access to the Direct3D 11 device methods (those defined on [**ID3D11Device**](https://msdn.microsoft.com/library/windows/desktop/ff476379)) and not the device context methods (the methods defined on [**ID3D11DeviceContext**](https://msdn.microsoft.com/library/windows/desktop/ff476385)), so it has the option to not perform any rendering. The **FinalizeCreateGameDeviceResources** method runs on the main thread and does have access to the Direct3D 11 device context methods.

The sequence of events for loading the game devices resources proceeds as follows.

**CreateDeviceResourcesAsync** first initializes constant buffers for the primitives. Constant buffers are low-latency, fixed-width buffers that hold the data that a shader uses during shader execution. (Think of these buffers as passing data to the shader that is constant over the execution of the particular draw call.) In this sample, the buffers contain the data that the shaders will use to:

-   Place the light sources and set their color when the renderer initializes
-   Compute the view matrix whenever the window is resized
-   Compute the projection matrix for every frame update
-   Compute the transformations of the primitives on every render update

The constants receive the source information (vertices) and transform the vertex coordinates and data from model space into the device space. Ultimately, this data results in texel coordinates and pixels in the render target.

Next, the game renderer object creates a loader for the shaders that will perform the computation. (See **BasicLoader.cpp** in the sample for the specific implementation.)

Then, **CreateDeviceResourcesAsync** initiates async tasks for loading all the texture resources into **ShaderResourceViews**. These texture resources are stored in the DirectDraw Surface (DDS) textures that came with the sample. DDS textures are a lossy texture format that work with DirectX Texture Compression (DXTC). We use these textures on the walls, ceiling and floor of the world, and on the ammo spheres and pillar obstacles.

Finally, it returns a task group that contains all the async tasks created by the method. The calling function waits for the completion of all these async tasks, and then calls **FinalizeCreateGameDeviceResources**.

**FinalizeCreateGameDeviceResources** loads the initial data into the constant buffers with a device context method call to [**ID3D11DeviceContext::UpdateSubresource**](https://msdn.microsoft.com/library/windows/desktop/ff476486): `m_deviceContext->UpdateSubresource`. This method creates the mesh objects for the sphere, cylinder, face, and world game objects and the associated materials. It then walks the game object list associating the appropriate device resources with each object.

The textures for the ringed and numbered target objects are procedurally generated using the code in **TargetTexture.cpp**. The renderer creates an instance of the **TargetTexture** type, which creates the bitmap texture for the target objects in the game when we call the **TargetTexture::CreateTextureResourceView** method. The resulting texture is composed of concentric colored rings, with a numeric value on the top. These generated resources are associated with the appropriate target game objects.

Lastly, **FinalizeCreateGameDeviceResources** set the `m_gameResourcesLoaded` Boolean global variable to indicate that all resources are now loaded.

The game has the resources to display the graphics in the current window, and it can recreate those resources as the window changes. Now, let's look at the camera used to define the player's view of the scene in that window.

## Implementing the camera object


The game has the code in place to update the world in its own coordinate system (sometimes called the world space or scene space). All objects, including the camera, are positioned and oriented in this space. In the sample game, the camera's position along with the look vectors (the "look at" vector that points directly into the scene from the camera, and the "look up" vector that is upwards perpendicular to it) define the camera space. The projection parameters determine how much of that space is actually visible in the final scene; and the Field of View (FoV), aspect ratio, and clipping planes define the projection transformation. A vertex shader does the heavy lifting of converting from the model coordinates to device coordinates with the following algorithm (where V is a vector and M is a matrix):

`              V(device) = V(model) x M(model-to-world) x M(world-to-view) x M(view-to-device)           `.

-   `M(model-to-world)` is a transformation matrix for model coordinates to world coordinates. This is provided by the primitive. (We'll review this in the section on primitives, here.)
-   `M(world-to-view)` is a transformation matrix for world coordinates to view coordinates. This is provided by the view matrix of the camera.
-   `M(view-to-device)` is a transformation matrix for view coordinates to device coordinates. This is provided by the projection of the camera.

The shader code in **VertexShader.hlsl** is loaded with these vectors and matrices from the constant buffers, and performs this transformation for every vertex.

The **Camera** object defines the view and projection matrices. Let's look at how the sample game declares it.

```cpp
ref class Camera
{
internal:
    Camera();

    // Call these from client and use Get*Matrix() to read new matrices.
    // Functions to change camera matrices
    void SetViewParams(_In_ DirectX::XMFLOAT3 eye, _In_ DirectX::XMFLOAT3 lookAt, _In_ DirectX::XMFLOAT3 up);
    void SetProjParams(_In_ float fieldOfView, _In_ float aspectRatio, _In_ float nearPlane, _In_ float farPlane);

    void LookDirection (_In_ DirectX::XMFLOAT3 lookDirection);
    void Eye (_In_ DirectX::XMFLOAT3 position);

    DirectX::XMMATRIX View();
    DirectX::XMMATRIX Projection();
    DirectX::XMMATRIX LeftEyeProjection();
    DirectX::XMMATRIX RightEyeProjection();
    DirectX::XMMATRIX World();
    DirectX::XMFLOAT3 Eye();
    DirectX::XMFLOAT3 LookAt();
    DirectX::XMFLOAT3 Up();
    float NearClipPlane();
    float FarClipPlane();
    float Pitch();
    float Yaw();

protected private:
    DirectX::XMFLOAT4X4 m_viewMatrix;                 // View matrix
    DirectX::XMFLOAT4X4 m_projectionMatrix;           // Projection matrix
    DirectX::XMFLOAT4X4 m_projectionMatrixLeft;       // Projection Matrix for Left Eye Stereo
    DirectX::XMFLOAT4X4 m_projectionMatrixRight;      // Projection Matrix for Right Eye Stereo

    DirectX::XMFLOAT4X4 m_inverseView;

    DirectX::XMFLOAT3 m_eye;                        // Camera eye position
    DirectX::XMFLOAT3 m_lookAt;                     // LookAt position
    DirectX::XMFLOAT3 m_up;                         // Up vector
    float             m_cameraYawAngle;             // Yaw angle of camera
    float             m_cameraPitchAngle;           // Pitch angle of camera

    float             m_fieldOfView;                // Field of view
    float             m_aspectRatio;                // Aspect ratio
    float             m_nearPlane;                  // Near plane
    float             m_farPlane;                   // Far plane
};
```

There are two 4x4 matrices that define the transformations to the view and projection coordinates, **m\_viewMatrix** and **m\_projectionMatrix**. (For stereo projection, you use two projection matrices: one for each eye's view.) They are calculated with these two methods, respectively:

-   **SetViewParams**
-   **SetProjParams**

The code for these two methods looks like this:

```cpp
void Camera::SetViewParams(
    _In_ XMFLOAT3 eye,
    _In_ XMFLOAT3 lookAt,
    _In_ XMFLOAT3 up
    )
{
    m_eye = eye;
    m_lookAt = lookAt;
    m_up = up;

    // Calc the view matrix.
    XMMATRIX view = XMMatrixLookAtLH(
        XMLoadFloat3(&m_eye),
        XMLoadFloat3(&m_lookAt),
        XMLoadFloat3(&m_up)
        );

    XMVECTOR det;
    XMMATRIX inverseView = XMMatrixInverse(&det, view);
    XMStoreFloat4x4(&m_viewMatrix, view);
    XMStoreFloat4x4(&m_inverseView, inverseView);

    // The axis basis vectors and camera position are stored inside the
    // position matrix in the 4 rows of the camera's world matrix.
    // To figure out the yaw/pitch of the camera, we just need the Z basis vector
    XMFLOAT3* zBasis = (XMFLOAT3*)&inverseView._31;

    m_cameraYawAngle = atan2f(zBasis->x, zBasis->z);

    float len = sqrtf(zBasis->z * zBasis->z + zBasis->x * zBasis->x);
    m_cameraPitchAngle = atan2f(zBasis->y, len);
}
//--------------------------------------------------------------------------------------
// Calculates the projection matrix based on input params
//--------------------------------------------------------------------------------------
void Camera::SetProjParams(
    _In_ float fieldOfView,
    _In_ float aspectRatio,
    _In_ float nearPlane,
    _In_ float farPlane
    )
{
    // Set attributes for the projection matrix
    m_fieldOfView = fieldOfView;
    m_aspectRatio = aspectRatio;
    m_nearPlane = nearPlane;
    m_farPlane = farPlane;
    XMStoreFloat4x4(
        &m_projectionMatrix,
        XMMatrixPerspectiveFovLH(
            m_fieldOfView,
            m_aspectRatio,
            m_nearPlane,
            m_farPlane
            )
        );

    STEREO_PARAMETERS* stereoParams = nullptr;
    // change the projection matrix
    XMStoreFloat4x4(
        &m_projectionMatrixLeft,
        MatrixStereoProjectionFovLH(
            stereoParams,
            STEREO_CHANNEL::LEFT,
            m_fieldOfView,
            m_aspectRatio,
            m_nearPlane,
            m_farPlane,
            STEREO_MODE::NORMAL
            )
        );

    XMStoreFloat4x4(
        &m_projectionMatrixRight,
        MatrixStereoProjectionFovLH(
            stereoParams,
            STEREO_CHANNEL::RIGHT,
            m_fieldOfView,
            m_aspectRatio,
            m_nearPlane,
            m_farPlane,
            STEREO_MODE::NORMAL
            )
        );
}
```

We get the resulting view and projection data by calling the **View** and **Projection** methods, respectively, on the **Camera** object. These calls occur in the next step we review, the **GameRenderer::Render** method called in the game loop.

Now, let's look at how the game creates the framework to draw our game graphics using the camera. This includes defining the primitives that comprise the game world and its elements.

## Defining the primitives


In the game sample code, we define and implement the primitives in two base classes and the corresponding specializations for each primitive type.

**MeshObject.h/.cpp** defines the base class for all mesh objects. The **SphereMesh.h/.cpp**, **CylinderMesh.h/.cpp**, **FaceMesh.h/.cpp**, and **WorldMesh.h/.cpp** files contain the code that populates the constant buffers for each primitive with the vertex and vertex normal data that defines the primitive's geometry. These code files are a good place to start if you're looking to understand how to create Direct3D primitives in your own game app, but we won't cover them here as it's too specific to this game's implementation. For now, we assume that the vertex buffers for each primitive have been populated, and look at how the game sample handles those buffers to update the game itself.

The base class for objects that represent the primitives from the perspective of the game is defined in **GameObject.h./.cpp.** This class, **GameObject**, defines the fields and methods for the common behaviors across all primitives. Each primitive object type derives from it. Let's look at how it's defined:

```cpp
ref class GameObject
{
internal:
    GameObject();

    // Expect these two functions to be overloaded by subclasses.
    virtual bool IsTouching(
        DirectX::XMFLOAT3 /* point */,
        float /* radius */,
        _Out_ DirectX::XMFLOAT3 *contact,
        _Out_ DirectX::XMFLOAT3 *normal
        )
    {
        *contact = DirectX::XMFLOAT3(0.0f, 0.0f, 0.0f);
        *normal = DirectX::XMFLOAT3(0.0f, 0.0f, 1.0f);
        return false;
    };

    void Render(
        _In_ ID3D11DeviceContext *context,
        _In_ ID3D11Buffer *primitiveConstantBuffer
        );

    void Active(bool active);
    bool Active();
    void Target(bool target);
    bool Target();
    void Hit(bool hit);
    bool Hit();
    void OnGround(bool ground);
    bool OnGround();
    void TargetId(int targetId);
    int  TargetId();
    void HitTime(float t);
    float HitTime();

    void     AnimatePosition(_In_opt_ Animate^ animate);
    Animate^ AnimatePosition();

    void         HitSound(_In_ SoundEffect^ hitSound);
    SoundEffect^ HitSound();

    void PlaySound(float impactSpeed, DirectX::XMFLOAT3 eyePoint);

    void Mesh(_In_ MeshObject^ mesh);

    void NormalMaterial(_In_ Material^ material);
    Material^ NormalMaterial();
    void HitMaterial(_In_ Material^ material);
    Material^ HitMaterial();

    void Position(DirectX::XMFLOAT3 position);
    void Position(DirectX::XMVECTOR position);
    void Velocity(DirectX::XMFLOAT3 velocity);
    void Velocity(DirectX::XMVECTOR velocity);
    DirectX::XMMATRIX ModelMatrix();
    DirectX::XMFLOAT3 Position();
    DirectX::XMVECTOR VectorPosition();
    DirectX::XMVECTOR VectorVelocity();
    DirectX::XMFLOAT3 Velocity();

protected private:
    virtual void UpdatePosition() {};
    // Object Data
    bool                m_active;
    bool                m_target;
    int                 m_targetId;
    bool                m_hit;
    bool                m_ground;

    DirectX::XMFLOAT3   m_position;
    DirectX::XMFLOAT3   m_velocity;
    DirectX::XMFLOAT4X4 m_modelMatrix;

    Material^           m_normalMaterial;
    Material^           m_hitMaterial;

    DirectX::XMFLOAT3   m_defaultXAxis;
    DirectX::XMFLOAT3   m_defaultYAxis;
    DirectX::XMFLOAT3   m_defaultZAxis;

    float               m_hitTime;

    Animate^            m_animatePosition;
    MeshObject^         m_mesh;

    SoundEffect^        m_hitSound;
};
```

Most of the fields contain data about the state, visual properties, or position of the primitive in the game world. There are a few methods in particular that are necessary in most games:

-   **Mesh**. Gets the mesh geometry for the primitive, which is stored in **m\_mesh**. This geometry is defined in **MeshObject.h/.cpp**.
-   **IsTouching**. This method determines if the primitive is within a specific distance of a point, and returns the point on the surface closest to the point and the normal to the surface of the object at that point. Because the sample is only concerned with ammo-primitive collisions, this is enough for the game's dynamics. It is not a general purpose primitive-primitive intersection function, although it could be used as the basis for one.
-   **AnimatePosition**. Updates the movement and animation for the primitive.
-   **UpdatePosition**. Updates the position of the object in the world coordinate space.
-   **Render**. Puts the material properties of the primitive into the primitive constant buffer and then renders (draws) the primitive geometry using the device context.

It's a good practice to create a base object type that defines the minimum set of methods for a primitive because most games have a very large number of primitives, and the code can quickly become difficult to manage. It also simplifies game code when the update loop can treat the primitives polymorphically, letting the objects themselves define their own update and rendering behaviors.

Let's look at the basic rendering of a primitive in the game sample.

## Rendering the primitives


The primitives in the game sample use the base **Render** method implemented on the parent **GameObject** class, as here:

```cpp
void GameObject::Render(
    _In_ ID3D11DeviceContext *context,
    _In_ ID3D11Buffer *primitiveConstantBuffer
    )
{
    if (!m_active || (m_mesh == nullptr) || (m_normalMaterial == nullptr))
    {
        return;
    }

    ConstantBufferChangesEveryPrim constantBuffer;

    XMStoreFloat4x4(
        &constantBuffer.worldMatrix,
        XMMatrixTranspose(ModelMatrix())
        );

    if (m_hit && m_hitMaterial != nullptr)
    {
        m_hitMaterial->RenderSetup(context, &constantBuffer);
    }
    else
    {
        m_normalMaterial->RenderSetup(context, &constantBuffer);
    }
    context->UpdateSubresource(primitiveConstantBuffer, 0, nullptr, &constantBuffer, 0, 0);

    m_mesh->Render(context);
}
```

The **GameObject::Render** method updates the primitive constant buffer with the data specific to a given primitive. The game uses multiple constant buffers, but only needs to update these buffers one time per primitive.

Think of the constant buffers as input to the shaders that run for each primitive. Some data is static (**m\_constantBufferNeverChanges**); some data is constant over the frame (**m\_constantBufferChangesEveryFrame)**, like the position of the camera; and some data is specific to the primitive, like its color and textures (**m\_constantBufferChangesEveryPrim**). The game renderer separates these inputs into different constant buffers to optimize the memory bandwidth that the CPU and GPU use. This approach also helps to minimize the amount of data the GPU needs to keep track of. Remember, the GPU has a big queue of commands, and each time the game calls **Draw**, that command is queued along with the data associated with it. When the game updates the primitive constant buffer and issues the next **Draw** command, the graphics driver adds this next command and the associated data to the queue. If the game draws 100 primitives, it could potentially have 100 copies of the constant buffer data in the queue. We want to minimize the amount of data the game is sending to the GPU, so the game uses a separate primitive constant buffer that only contains the updates for each primitive.

If a collision (a hit) is detected, **GameObject::Render** checks the current context, which indicates whether the target has been hit by an ammo sphere. If the target has been hit, this method applies a hit material, which reverses the colors of the rings of the target to indicate a successful hit to the player. Otherwise, it applies the default material with the same method. In both cases, it sets the material by calling **Material::RenderSetup**, which sets the appropriate constants into the constant buffer. Then, it calls [**ID3D11DeviceContext::PSSetShaderResources**](https://msdn.microsoft.com/library/windows/desktop/ff476473) to set the corresponding texture resource for the pixel shader, and [**ID3D11DeviceContext::VSSetShader**](https://msdn.microsoft.com/library/windows/desktop/ff476493) and [**ID3D11DeviceContext::PSSetShader**](https://msdn.microsoft.com/library/windows/desktop/ff476472) to set the vertex shader and pixel shader objects themselves, respectively.

Here's how **Material::RenderSetup** configures the constant buffers and assigns the shader resources. Again, note that the constant buffer is the one used for updating changes to primitives, specifically.

> **Note**   The **Material** class is defined in **Material.h/.cpp**.

 

```cpp
void Material::RenderSetup(
    _In_ ID3D11DeviceContext* context,
    _Inout_ ConstantBufferChangesEveryPrim* constantBuffer
    )
{
    constantBuffer->meshColor = m_meshColor;
    constantBuffer->specularColor = m_specularColor;
    constantBuffer->specularPower = m_specularExponent;
    constantBuffer->diffuseColor = m_diffuseColor;

    context->PSSetShaderResources(0, 1, m_textureRV.GetAddressOf());
    context->VSSetShader(m_vertexShader.Get(), nullptr, 0);
    context->PSSetShader(m_pixelShader.Get(), nullptr, 0);
}
```

Finally, the **PrimObject::Render** calls the **Render** method for the underlying **MeshObject** object.

```cpp
void MeshObject::Render(_In_ ID3D11DeviceContext *context)
{
    uint32 stride = sizeof(PNTVertex);
    uint32 offset = 0;

    context->IASetVertexBuffers(0, 1, m_vertexBuffer.GetAddressOf(), &stride, &offset);
    context->IASetIndexBuffer(m_indexBuffer.Get(), DXGI_FORMAT_R16_UINT, 0);
    context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
    context->DrawIndexed(m_indexCount, 0, 0);
}
```

Now, the game sample’s **MeshObject::Render** method queues the drawing command to execute the shaders on the GPU using the current scene state. The vertex shader converts the geometry (vertices) from model coordinates into device (world) coordinates, taking into account where the camera is and the perspective transformation. Lastly, the pixel shaders render the transformed triangles into the back buffer using the texture set above.

This happens on the actual rendering process!

## Creating the vertex and pixel shaders


At this point, the game sample has defined the primitives to draw and the constant buffers that define their rendering. These constant buffers serve as the sets of parameters to the shaders that run on the graphics device. These shader programs come in two types:

-   Vertex shaders perform per-vertex operations, such as vertex transformations and lighting.
-   Pixel (or fragment) shaders perform per-pixel operations, such as texturing and per-pixel lighting. They can also be used to perform post-processing effects on bitmaps, such as the final render target.

The shader code is defined using High-Level Shader Language (HLSL), which, in Direct3D 11, is compiled from a program created with a C-like syntax. (The complete syntax can be found [here](https://msdn.microsoft.com/library/windows/desktop/bb509635).) The two principal shaders for the sample game are defined in **PixelShader.hlsl** and **VertexShader.hlsl**. (There are also two "low power" shaders defined for low power devices: **PixelShaderFlat.hlsl** and **VertexShaderFlat.hlsl**. These two shaders provide reduced effects, such as a lack of specular highlights on textures surfaces.) FInally, there is an .hlsli file that contains the format of the constant buffers, **ConstantBuffers.hlsli**.

**ConstantBuffers.hlsli** is defined like this:

```cpp
Texture2D diffuseTexture : register(t0);
SamplerState linearSampler : register(s0);

cbuffer ConstantBufferNeverChanges : register(b0)
{
    float4 lightPosition[4];
    float4 lightColor;
}

cbuffer ConstantBufferChangeOnResize : register(b1)
{
    matrix projection;
};

cbuffer ConstantBufferChangesEveryFrame : register(b2)
{
    matrix view;
};

cbuffer ConstantBufferChangesEveryPrim : register (b3)
{
    matrix world;
    float4 meshColor;
    float4 diffuseColor;
    float4 specularColor;
    float  specularExponent;
};

struct VertextShaderInput
{
    float4 position : POSITION;
    float4 normal : NORMAL;
    float2 textureUV : TEXCOORD0;
};

struct PixelShaderInput
{
    float4 position : SV_POSITION;
    float2 textureUV : TEXCOORD0;
    float3 vertexToEye : TEXCOORD1;
    float3 normal : TEXCOORD2;
    float3 vertexToLight0 : TEXCOORD3;
    float3 vertexToLight1 : TEXCOORD4;
    float3 vertexToLight2 : TEXCOORD5;
    float3 vertexToLight3 : TEXCOORD6;
};

struct PixelShaderFlatInput
{
    float4 position : SV_POSITION;
    float2 textureUV : TEXCOORD0;
    float4 diffuseColor : TEXCOORD1;
};
```

**VertexShader.hlsl** is defined like this:

VertexShader.hlsl

```cpp
#include "ConstantBuffers.hlsli"

PixelShaderInput main(VertextShaderInput input)
{
    PixelShaderInput output = (PixelShaderInput)0;

    output.position = mul(mul(mul(input.position, world), view), projection);
    output.textureUV = input.textureUV;

    // compute view space normal
    output.normal = normalize (mul(mul(input.normal.xyz, (float3x3)world), (float3x3)view));

    // Vertex pos in view space (normalize in pixel shader)
    output.vertexToEye = -mul(mul(input.position, world), view).xyz;

    // Compute view space vertex to light vectors (normalized)
    output.vertexToLight0 = normalize(mul(lightPosition[0], view ).xyz + output.vertexToEye);
    output.vertexToLight1 = normalize(mul(lightPosition[1], view ).xyz + output.vertexToEye);
    output.vertexToLight2 = normalize(mul(lightPosition[2], view ).xyz + output.vertexToEye);
    output.vertexToLight3 = normalize(mul(lightPosition[3], view ).xyz + output.vertexToEye);

    return output;
}
```

The **main** function in **VertexShader.hlsl** performs the vertex transformation sequence we discussed in the camera section. It's run one time per vertex. The resultant outputs are passed to the pixel shader code for texturing and material effects.

PixelShader.hlsl

```cpp
#include "ConstantBuffers.hlsli"

float4 main(PixelShaderInput input) : SV_Target
{
    float diffuseLuminance =
        max(0.0f, dot(input.normal, input.vertexToLight0)) +
        max(0.0f, dot(input.normal, input.vertexToLight1)) +
        max(0.0f, dot(input.normal, input.vertexToLight2)) +
        max(0.0f, dot(input.normal, input.vertexToLight3));

    // Normalize view space vertex-to-eye
    input.vertexToEye = normalize(input.vertexToEye);

    float specularLuminance = 
        pow(max(0.0f, dot(input.normal, normalize(input.vertexToEye + input.vertexToLight0))), specularExponent) +
        pow(max(0.0f, dot(input.normal, normalize(input.vertexToEye + input.vertexToLight1))), specularExponent) +
        pow(max(0.0f, dot(input.normal, normalize(input.vertexToEye + input.vertexToLight2))), specularExponent) +
        pow(max(0.0f, dot(input.normal, normalize(input.vertexToEye + input.vertexToLight3))), specularExponent);

    float4 specular;
    specular = specularColor * specularLuminance * 0.5f;

   return diffuseTexture.Sample(linearSampler, input.textureUV) *  diffuseColor * diffuseLuminance * 0.5f + specular;
}
```

The **main** function in **PixelShader.hlsl** takes the 2-D projections of the triangle surfaces for each primitive in the scene, and computes the color value for each pixel of the visible surfaces based on the textures and effects (in this case, specular lighting) applied to them.

Now, let's bring all these ideas (primitives, camera, and shaders) together, and see how the sample game builds the complete rendering process.

## Rendering the frame for output


We briefly discussed this method in [Defining the main game object](tutorial--defining-the-main-game-loop.md). Now, let's look at it in a little more detail.

```cpp
void GameRenderer::Render()
{
    int renderingPasses = 1;
    if (m_stereoEnabled)
    {
        renderingPasses = 2;
    }

    for (int i = 0; i < renderingPasses; i++)
    {
        if (m_stereoEnabled && i > 0)
        {
            // Doing the Right Eye View
            m_d3dContext->OMSetRenderTargets(1, m_renderTargetViewRight.GetAddressOf(), m_depthStencilView.Get());
            m_d3dContext->ClearDepthStencilView(m_depthStencilView.Get(), D3D11_CLEAR_DEPTH, 1.0f, 0);
            m_d2dContext->SetTarget(m_d2dTargetBitmapRight.Get());
        }
        else
        {
            // Doing the Mono or Left Eye View
            m_d3dContext->OMSetRenderTargets(1, m_renderTargetView.GetAddressOf(), m_depthStencilView.Get());
            m_d3dContext->ClearDepthStencilView(m_depthStencilView.Get(), D3D11_CLEAR_DEPTH, 1.0f, 0);
            m_d2dContext->SetTarget(m_d2dTargetBitmap.Get());
        }

        if (m_game != nullptr && m_gameResourcesLoaded && m_levelResourcesLoaded)
        {
            // This section is only used after the game state has been initialized and all device
            // resources needed for the game have been created and associated with the game objects.
            if (m_stereoEnabled)
            {
                ConstantBufferChangeOnResize changesOnResize;
                XMStoreFloat4x4(
                    &changesOnResize.projection,
                    XMMatrixMultiply(
                        XMMatrixTranspose(
                            i == 0 ?
                            m_game->GameCamera()->LeftEyeProjection() :
                            m_game->GameCamera()->RightEyeProjection()
                            ),
                        XMMatrixTranspose(XMLoadFloat4x4(&m_rotationTransform3D))
                        )
                    );

                m_d3dContext->UpdateSubresource(
                    m_constantBufferChangeOnResize.Get(),
                    0,
                    nullptr,
                    &changesOnResize,
                    0,
                    0
                    );
            }
            // Update variables that change one time per frame

            ConstantBufferChangesEveryFrame constantBufferChangesEveryFrame;
            XMStoreFloat4x4(
                &constantBufferChangesEveryFrame.view,
                XMMatrixTranspose(m_game->GameCamera()->View())
                );
            m_d3dContext->UpdateSubresource(
                m_constantBufferChangesEveryFrame.Get(),
                0,
                nullptr,
                &constantBufferChangesEveryFrame,
                0,
                0
                );

            // Setup Pipeline

            m_d3dContext->IASetInputLayout(m_vertexLayout.Get());
            m_d3dContext->VSSetConstantBuffers(0, 1, m_constantBufferNeverChanges.GetAddressOf());
            m_d3dContext->VSSetConstantBuffers(1, 1, m_constantBufferChangeOnResize.GetAddressOf());
            m_d3dContext->VSSetConstantBuffers(2, 1, m_constantBufferChangesEveryFrame.GetAddressOf());
            m_d3dContext->VSSetConstantBuffers(3, 1, m_constantBufferChangesEveryPrim.GetAddressOf());

            m_d3dContext->PSSetConstantBuffers(2, 1, m_constantBufferChangesEveryFrame.GetAddressOf());
            m_d3dContext->PSSetConstantBuffers(3, 1, m_constantBufferChangesEveryPrim.GetAddressOf());
            m_d3dContext->PSSetSamplers(0, 1, m_samplerLinear.GetAddressOf());

            // Get the all the objects to render from the Game state.
            auto objects = m_game->RenderObjects();
            for (auto object = objects.begin(); object != objects.end(); object++)
            {
                (*object)->Render(m_d3dContext.Get(), m_constantBufferChangesEveryPrim.Get());
            }
        }
        else
        {
            const float ClearColor[4] = {0.1f, 0.1f, 0.1f, 1.0f};

            // Only need to clear the background when not rendering the full 3-D scene because
            // the 3-D world is a fully enclosed box and the dynamics prevents the camera from
            // moving outside this space.
            if (m_stereoEnabled && i > 0)
            {
                // Doing the Right Eye View
                m_d3dContext->ClearRenderTargetView(m_renderTargetViewRight.Get(), ClearColor);
            }
            else
            {
                // Doing the Mono or Left Eye View
                m_d3dContext->ClearRenderTargetView(m_renderTargetView.Get(), ClearColor);
            }
        }

        m_d2dContext->BeginDraw();

        // To handle the swapchain being pre-rotated, set the D2D transformation to include it.
        m_d2dContext->SetTransform(m_rotationTransform2D);

        if (m_game != nullptr && m_gameResourcesLoaded)
        {
            // This is only used after the game state has been initialized.
            m_gameHud->Render(m_game, m_d2dContext.Get(), m_windowBounds);
        }

        if (m_gameInfoOverlay->Visible())
        {
            m_d2dContext->DrawBitmap(
                m_gameInfoOverlay->Bitmap(),
                D2D1::RectF(
                    (m_windowBounds.Width - GameInfoOverlayConstant::Width)/2.0f,
                    (m_windowBounds.Height - GameInfoOverlayConstant::Height)/2.0f,
                    (m_windowBounds.Width - GameInfoOverlayConstant::Width)/2.0f + GameInfoOverlayConstant::Width,
                    (m_windowBounds.Height - GameInfoOverlayConstant::Height)/2.0f + GameInfoOverlayConstant::Height
                    )
                );
        }

        HRESULT hr = m_d2dContext->EndDraw();
        if (hr != D2DERR_RECREATE_TARGET)
        {
            // The D2DERR_RECREATE_TARGET indicates there has been a problem with the underlying
            // D3D device.  All subsequent rendering will be ignored until the device is recreated.
            // This error will be propagated and the appropriate D3D error will be returned from the
            // swapchain->Present(...) call.   At that point, the sample will recreate the device
            // and all associated resources.  As a result the D2DERR_RECREATE_TARGET doesn't
            // need to be handled here.
            DX::ThrowIfFailed(hr);
        }
    }
    Present();
}
```

The game has all the pieces to assemble a view for output: primitives and the rules for their behavior, a camera object to provide the player's view of the game world, and the graphics resources for drawing.

Now, let's look at the process that brings it all together.

1.  If stereo 3D is enabled, set the following rendering process to run two times, one time for each eye.
2.  The whole scene is enclosed in a bounding world volume, so draw every pixel (even those we don’t need) to clear the color planes of the render target. Set the depth stencil buffer to the default value.
3.  Update the constant buffer for frame update data by using the camera's view matrix and data.
4.  Set up the Direct3D context to use the four content buffers that were defined earlier.
5.  Call the **Render** method on each primitive object. This results in a **Draw** or **DrawIndexed** call on the context to draw the geometry of that each primitive. Specifically, this **Draw** call queues commands and data to the GPU, as parameterized by the constant buffer data. Each draw call executes the vertex shader one time per vertex, and then the pixel shader one time for every pixel of each triangle in the primitive. The textures are part of the state that the pixel shader uses to do the rendering.
6.  Draw the HUD and the overlay using the Direct2D context.
7.  Call **DirectXBase::Present**.

And the game has updated the display! Altogether, this is the basic process for implementing the graphics framework of a game. Of course, the larger your game, the more abstractions you must put in place to handle that complexity, such as entire hierarchies of object types and animation behaviors, and more complex methods for loading and managing assets such as meshes and textures.

## Next steps


Moving forward, let's look at a few important parts of the game sample that we've only discussed in passing: [the user interface overlay](tutorial--adding-a-user-interface.md), [the input controls](tutorial--adding-controls.md), and [the sound](tutorial--adding-sound.md).

## Complete sample code for this section


Camera.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

ref class Camera
{
internal:
    Camera();

    // Call these from client and use Get*Matrix() to read new matrices.
    // Functions to change camera matrices.
    void SetViewParams(_In_ DirectX::XMFLOAT3 eye, _In_ DirectX::XMFLOAT3 lookAt, _In_ DirectX::XMFLOAT3 up);
    void SetProjParams(_In_ float fieldOfView, _In_ float aspectRatio, _In_ float nearPlane, _In_ float farPlane);

    void LookDirection (_In_ DirectX::XMFLOAT3 lookDirection);
    void Eye (_In_ DirectX::XMFLOAT3 position);

    DirectX::XMMATRIX View();
    DirectX::XMMATRIX Projection();
    DirectX::XMMATRIX LeftEyeProjection();
    DirectX::XMMATRIX RightEyeProjection();
    DirectX::XMMATRIX World();
    DirectX::XMFLOAT3 Eye();
    DirectX::XMFLOAT3 LookAt();
    DirectX::XMFLOAT3 Up();
    float NearClipPlane();
    float FarClipPlane();
    float Pitch();
    float Yaw();

protected private:
    DirectX::XMFLOAT4X4 m_viewMatrix;                 // View matrix
    DirectX::XMFLOAT4X4 m_projectionMatrix;           // Projection matrix
    DirectX::XMFLOAT4X4 m_projectionMatrixLeft;       // Projection Matrix for Left Eye Stereo
    DirectX::XMFLOAT4X4 m_projectionMatrixRight;      // Projection Matrix for Right Eye Stereo

    DirectX::XMFLOAT4X4 m_inverseView;

    DirectX::XMFLOAT3 m_eye;                        // Camera eye position
    DirectX::XMFLOAT3 m_lookAt;                     // LookAt position
    DirectX::XMFLOAT3 m_up;                         // Up vector
    float             m_cameraYawAngle;             // Yaw angle of camera
    float             m_cameraPitchAngle;           // Pitch angle of camera

    float             m_fieldOfView;                // Field of view
    float             m_aspectRatio;                // Aspect ratio
    float             m_nearPlane;                  // Near plane
    float             m_farPlane;                   // Far plane
};
```

Camera.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Camera.h"
#include "StereoProjection.h"

using namespace DirectX;

#undef min // use __min instead
#undef max // use __max instead
//--------------------------------------------------------------------------------------
// Constructor
//--------------------------------------------------------------------------------------
Camera::Camera()
{
    // Setup the view matrix.
    SetViewParams(
        XMFLOAT3(0.0f, 0.0f, 0.0f),   // default eye position.
        XMFLOAT3(0.0f, 0.0f, 1.0f),   // default look at position.
        XMFLOAT3(0.0f, 1.0f, 0.0f)    // default up vector.
        );

    // Setup the projection matrix.
    SetProjParams(XM_PI / 4, 1.0f, 1.0f, 1000.0f);
}
//--------------------------------------------------------------------------------------
void Camera::LookDirection (_In_ XMFLOAT3 lookDirection)
{
    XMFLOAT3 lookAt;
    lookAt.x = m_eye.x + lookDirection.x;
    lookAt.y = m_eye.y + lookDirection.y;
    lookAt.z = m_eye.z + lookDirection.z;

    SetViewParams(m_eye, lookAt, m_up);
}
//--------------------------------------------------------------------------------------
void Camera::Eye(_In_ XMFLOAT3 eye)
{
    SetViewParams(eye, m_lookAt, m_up);
}
//--------------------------------------------------------------------------------------
void Camera::SetViewParams(
    _In_ XMFLOAT3 eye,
    _In_ XMFLOAT3 lookAt,
    _In_ XMFLOAT3 up
    )
{
    m_eye = eye;
    m_lookAt = lookAt;
    m_up = up;

    // Calculate the view matrix.
    XMMATRIX view = XMMatrixLookAtLH(
        XMLoadFloat3(&m_eye),
        XMLoadFloat3(&m_lookAt),
        XMLoadFloat3(&m_up)
        );

    XMVECTOR det;
    XMMATRIX inverseView = XMMatrixInverse(&det, view);
    XMStoreFloat4x4(&m_viewMatrix, view);
    XMStoreFloat4x4(&m_inverseView, inverseView);

    // The axis basis vectors and camera position are stored inside the
    // position matrix in the four rows of the camera's world matrix.
    // To figure out the yaw/pitch of the camera, we just need the Z basis vector.
    XMFLOAT3 zBasis;
    XMStoreFloat3(&zBasis, inverseView.r[2]);

    m_cameraYawAngle = atan2f(zBasis.x, zBasis.z);

    float len = sqrtf(zBasis.z * zBasis.z + zBasis.x * zBasis.x);
    m_cameraPitchAngle = atan2f(zBasis.y, len);
}
//--------------------------------------------------------------------------------------
// Calculates the projection matrix based on input params.
//--------------------------------------------------------------------------------------
void Camera::SetProjParams(
    _In_ float fieldOfView,
    _In_ float aspectRatio,
    _In_ float nearPlane,
    _In_ float farPlane
    )
{
    // Set attributes for the projection matrix.
    m_fieldOfView = fieldOfView;
    m_aspectRatio = aspectRatio;
    m_nearPlane = nearPlane;
    m_farPlane = farPlane;
    XMStoreFloat4x4(
        &m_projectionMatrix,
        XMMatrixPerspectiveFovLH(
            m_fieldOfView,
            m_aspectRatio,
            m_nearPlane,
            m_farPlane
            )
        );

    STEREO_PARAMETERS* stereoParams = nullptr;
    // Change the projection matrix.
    XMStoreFloat4x4(
        &m_projectionMatrixLeft,
        MatrixStereoProjectionFovLH(
            stereoParams,
            STEREO_CHANNEL::LEFT,
            m_fieldOfView,
            m_aspectRatio,
            m_nearPlane,
            m_farPlane,
            STEREO_MODE::NORMAL
            )
        );

    XMStoreFloat4x4(
        &m_projectionMatrixRight,
        MatrixStereoProjectionFovLH(
            stereoParams,
            STEREO_CHANNEL::RIGHT,
            m_fieldOfView,
            m_aspectRatio,
            m_nearPlane,
            m_farPlane,
            STEREO_MODE::NORMAL
            )
        );
}
//--------------------------------------------------------------------------------------
DirectX::XMMATRIX Camera::View()
{
    return XMLoadFloat4x4(&m_viewMatrix);
}
DirectX::XMMATRIX Camera::Projection()
{
    return XMLoadFloat4x4(&m_projectionMatrix);
}
DirectX::XMMATRIX Camera::LeftEyeProjection()
{
    return XMLoadFloat4x4(&m_projectionMatrixLeft);
}
DirectX::XMMATRIX Camera::RightEyeProjection()
{
    return XMLoadFloat4x4(&m_projectionMatrixRight);
}
DirectX::XMMATRIX Camera::World()
{
    return XMLoadFloat4x4(&m_inverseView);
}
DirectX::XMFLOAT3 Camera::Eye()
{
    return m_eye;
}
DirectX::XMFLOAT3 Camera::LookAt()
{
    return m_lookAt;
}
float Camera::NearClipPlane()
{
    return m_nearPlane;
}
float Camera::FarClipPlane()
{
    return m_farPlane;
}
float Camera::Pitch()
{
    return m_cameraPitchAngle;
}
float Camera::Yaw()
{
    return m_cameraYawAngle;
}
```

GameRenderer.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

#include "DirectXBase.h"
#include "GameInfoOverlay.h"
#include "GameHud.h"
#include "Simple3DGame.h"

ref class Simple3DGame;
ref class GameHud;

ref class GameRenderer : public DirectXBase
{
internal:
    GameRenderer();

    virtual void Initialize(
        _In_ Windows::UI::Core::CoreWindow^ window,
        float dpi
        ) override;

    virtual void CreateDeviceIndependentResources() override;
    virtual void CreateDeviceResources() override;
    virtual void UpdateForWindowSizeChange() override;
    virtual void Render() override;
    virtual void SetDpi(float dpi) override;

    concurrency::task<void> CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game);
    void FinalizeCreateGameDeviceResources();
    concurrency::task<void> LoadLevelResourcesAsync();
    void FinalizeLoadLevelResources();

    GameInfoOverlay^ InfoOverlay()  { return m_gameInfoOverlay; };

    DirectX::XMFLOAT2 GameInfoOverlayUpperLeft()
    {
        return DirectX::XMFLOAT2(
            (m_windowBounds.Width  - GameInfoOverlayConstant::Width) / 2.0f,
            (m_windowBounds.Height - GameInfoOverlayConstant::Height) / 2.0f
            );
    };
    DirectX::XMFLOAT2 GameInfoOverlayLowerRight()
    {
        return DirectX::XMFLOAT2(
            (m_windowBounds.Width  - GameInfoOverlayConstant::Width) / 2.0f + GameInfoOverlayConstant::Width,
            (m_windowBounds.Height - GameInfoOverlayConstant::Height) / 2.0f + GameInfoOverlayConstant::Height
            );
    };

protected private:
    bool                                                m_initialized;
    bool                                                m_gameResourcesLoaded;
    bool                                                m_levelResourcesLoaded;
    GameInfoOverlay^                                    m_gameInfoOverlay;
    GameHud^                                            m_gameHud;
    Simple3DGame^                                       m_game;

    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_sphereTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_cylinderTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_ceilingTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_floorTexture;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView>    m_wallsTexture;

    // Constant Buffers.
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferNeverChanges;
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferChangeOnResize;
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferChangesEveryFrame;
    Microsoft::WRL::ComPtr<ID3D11Buffer>                m_constantBufferChangesEveryPrim;
    Microsoft::WRL::ComPtr<ID3D11SamplerState>          m_samplerLinear;
    Microsoft::WRL::ComPtr<ID3D11VertexShader>          m_vertexShader;
    Microsoft::WRL::ComPtr<ID3D11VertexShader>          m_vertexShaderFlat;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>           m_pixelShader;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>           m_pixelShaderFlat;
    Microsoft::WRL::ComPtr<ID3D11InputLayout>           m_vertexLayout;
};
```

GameRenderer.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "DirectXSample.h"
#include "GameRenderer.h"
#include "ConstantBuffers.h"
#include "TargetTexture.h"
#include "BasicLoader.h"
#include "CylinderMesh.h"
#include "FaceMesh.h"
#include "SphereMesh.h"
#include "WorldMesh.h"
#include "Face.h"
#include "Sphere.h"
#include "Cylinder.h"

using namespace concurrency;
using namespace DirectX;
using namespace Microsoft::WRL;
using namespace Windows::UI::Core;

//----------------------------------------------------------------------

GameRenderer::GameRenderer() :
    m_initialized(false),
    m_gameResourcesLoaded(false),
    m_levelResourcesLoaded(false)
{
}

//----------------------------------------------------------------------

void GameRenderer::Initialize(
    _In_ CoreWindow^ window,
    float dpi
    )
{
    if (!m_initialized)
    {
        m_gameHud = ref new GameHud(
            "Windows 8 Samples",
            "DirectX first-person game sample"
            );
        m_gameInfoOverlay = ref new GameInfoOverlay();
        m_initialized = true;
    }

    DirectXBase::Initialize(window, dpi);

    // Initialize could be called multiple times as a result of an error with the hardware device
    // that requires it to be reinitialized.  Since the m_gameInfoOverlay has resources that are
    // dependent on the device, it will need to be reinitialized each time with the new device information.
    m_gameInfoOverlay->Initialize(m_d2dDevice.Get(), m_d2dContext.Get(), m_dwriteFactory.Get(), dpi);
}

//----------------------------------------------------------------------

void GameRenderer::CreateDeviceIndependentResources()
{
    DirectXBase::CreateDeviceIndependentResources();
    m_gameHud->CreateDeviceIndependentResources(m_dwriteFactory.Get(), m_wicFactory.Get());
}

//----------------------------------------------------------------------

void GameRenderer::CreateDeviceResources()
{
    DirectXBase::CreateDeviceResources();

    m_gameHud->CreateDeviceResources(m_d2dContext.Get());

    if (m_game != nullptr)
    {
        // The initial invocation of CreateDeviceResources will occur
        // before the Game State is initialized when the device is first
        // being created, so that the inital loading screen can be displayed.
        // Subsequent invocations of CreateDeviceResources will be a result
        // of an error with the Device that requires the resources to be
        // recreated.  In this case, the game state is already initialized
        // so the game device resources need to be recreated.

        // This sample doesn't gracefully handle all the async recreation
        // of resources, so an exception is thrown.
        throw Platform::Exception::CreateException(
            DXGI_ERROR_DEVICE_REMOVED,
            "GameRenderer::CreateDeviceResources - Recreation of resources after TDR not available\n"
            );
    }
}

//----------------------------------------------------------------------

void GameRenderer::UpdateForWindowSizeChange()
{
    DirectXBase::UpdateForWindowSizeChange();

    m_gameHud->UpdateForWindowSizeChange(m_windowBounds);

    // Update the Projection Matrix and update the associated Constant Buffer.
    if (m_game != nullptr)
    {
        m_game->GameCamera()->SetProjParams(
            XM_PI / 2, m_renderTargetSize.Width / m_renderTargetSize.Height,
            0.01f,
            100.0f
            );
        ConstantBufferChangeOnResize changesOnResize;
        XMStoreFloat4x4(
            &changesOnResize.projection,
            XMMatrixMultiply(
                XMMatrixTranspose(m_game->GameCamera()->Projection()),
                XMMatrixTranspose(XMLoadFloat4x4(&m_rotationTransform3D))
                )
            );

        m_d3dContext->UpdateSubresource(
            m_constantBufferChangeOnResize.Get(),
            0,
            nullptr,
            &changesOnResize,
            0,
            0
            );
    }
}

//----------------------------------------------------------------------

void GameRenderer::SetDpi(float dpi)
{
    DirectXBase::SetDpi(dpi);

    if (m_gameInfoOverlay)
    {
        m_gameInfoOverlay->SetDpi(dpi);
    }
}

//----------------------------------------------------------------------

task<void> GameRenderer::CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game)
{
    // Set the Loading state to wait until any async resources have
    // been loaded before proceeding.
    m_game = game;

    // NOTE: Only the m_d3dDevice is used in this method.  It is expected
    // to not run on the same thread as the GameRenderer was created.
    // Create methods on the m_d3dDevice are free-threaded and are safe while any methods
    // in the m_d3dContext should only be used on a single thread and handled
    // in the FinalizeCreateGameDeviceResources method.

    D3D11_BUFFER_DESC bd;
    ZeroMemory(&bd, sizeof(bd));

    // Create the constant buffers,
    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
    bd.CPUAccessFlags = 0;
    bd.ByteWidth = (sizeof(ConstantBufferNeverChanges) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferNeverChanges)
        );

    bd.ByteWidth = (sizeof(ConstantBufferChangeOnResize) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferChangeOnResize)
        );

    bd.ByteWidth = (sizeof(ConstantBufferChangesEveryFrame) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferChangesEveryFrame)
        );

    bd.ByteWidth = (sizeof(ConstantBufferChangesEveryPrim) + 15) / 16 * 16;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferChangesEveryPrim)
        );

    D3D11_SAMPLER_DESC sampDesc;
    ZeroMemory(&sampDesc, sizeof(sampDesc));

    sampDesc.Filter = D3D11_FILTER_MIN_MAG_MIP_LINEAR;
    sampDesc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.AddressW = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.ComparisonFunc = D3D11_COMPARISON_NEVER;
    sampDesc.MinLOD = 0;
    sampDesc.MaxLOD = FLT_MAX;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateSamplerState(&sampDesc, &m_samplerLinear)
        );

    // Start the async tasks to load the shaders and textures.
    BasicLoader^ loader = ref new BasicLoader(m_d3dDevice.Get());

    std::vector<task<void>> tasks;

    uint32 numElements = ARRAYSIZE(PNTVertexLayout);
    tasks.push_back(loader->LoadShaderAsync("VertexShader.cso", PNTVertexLayout, numElements, &m_vertexShader, &m_vertexLayout));
    tasks.push_back(loader->LoadShaderAsync("VertexShaderFlat.cso", nullptr, numElements, &m_vertexShaderFlat, nullptr));
    tasks.push_back(loader->LoadShaderAsync("PixelShader.cso", &m_pixelShader));
    tasks.push_back(loader->LoadShaderAsync("PixelShaderFlat.cso", &m_pixelShaderFlat));

    // Make sure previous versions if any of the textures are released.
    m_sphereTexture = nullptr;
    m_cylinderTexture = nullptr;
    m_ceilingTexture = nullptr;
    m_floorTexture = nullptr;
    m_wallsTexture = nullptr;

    // Load Game specific textures.
    tasks.push_back(loader->LoadTextureAsync("seafloor.dds", nullptr, &m_sphereTexture));
    tasks.push_back(loader->LoadTextureAsync("metal_texture.dds", nullptr, &m_cylinderTexture));
    tasks.push_back(loader->LoadTextureAsync("cellceiling.dds", nullptr, &m_ceilingTexture));
    tasks.push_back(loader->LoadTextureAsync("cellfloor.dds", nullptr, &m_floorTexture));
    tasks.push_back(loader->LoadTextureAsync("cellwall.dds", nullptr, &m_wallsTexture));
    tasks.push_back(create_task([]()
    {
        // Simulate loading additional resources.
        wait(GameConstants::InitialLoadingDelay);
    }));

    // Return the task group of all the async tasks for loading the shader and texture assets.
    return when_all(tasks.begin(), tasks.end());
}

//----------------------------------------------------------------------

void GameRenderer::FinalizeCreateGameDeviceResources()
{
    // All asynchronously loaded resources have completed loading.
    // Now, associate all the resources with the appropriate
    // Game objects.
    // This method is expected to run in the same thread as the GameRenderer
    // was created. All work will happen behind the "Loading ..." screen after the
    // main loop has been entered.

    // Initialize the Constant buffer with the light positions.
    // These are handled here to ensure that the d3dContext is only
    // used in one thread.

    ConstantBufferNeverChanges constantBufferNeverChanges;
    constantBufferNeverChanges.lightPosition[0] = XMFLOAT4( 3.5f, 2.5f,  5.5f, 1.0f);
    constantBufferNeverChanges.lightPosition[1] = XMFLOAT4( 3.5f, 2.5f, -5.5f, 1.0f);
    constantBufferNeverChanges.lightPosition[2] = XMFLOAT4(-3.5f, 2.5f, -5.5f, 1.0f);
    constantBufferNeverChanges.lightPosition[3] = XMFLOAT4( 3.5f, 2.5f,  5.5f, 1.0f);
    constantBufferNeverChanges.lightColor = XMFLOAT4(0.25f, 0.25f, 0.25f, 1.0f);
    m_d3dContext->UpdateSubresource(m_constantBufferNeverChanges.Get(), 0, nullptr, &constantBufferNeverChanges, 0, 0);

    // For the targets, there are two unique generated textures.
    // Each texture image includes the number of the texture.
    // Make sure the 2D rendering is occurring on the same thread
    // as the main rendering.

    TargetTexture^ textureGenerator = ref new TargetTexture(
        m_d3dDevice.Get(),
        m_d2dFactory.Get(),
        m_dwriteFactory.Get(),
        m_d2dContext.Get()
        );

    MeshObject^ cylinderMesh = ref new CylinderMesh(m_d3dDevice.Get(), 26);
    MeshObject^ targetMesh = ref new FaceMesh(m_d3dDevice.Get());
    MeshObject^ sphereMesh = ref new SphereMesh(m_d3dDevice.Get(), 26);

    Material^ cylinderMaterial = ref new Material(
        XMFLOAT4(0.8f, 0.8f, 0.8f, .5f),
        XMFLOAT4(0.8f, 0.8f, 0.8f, .5f),
        XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f),
        15.0f,
        m_cylinderTexture.Get(),
        m_vertexShader.Get(),
        m_pixelShader.Get()
        );

    Material^ sphereMaterial = ref new Material(
        XMFLOAT4(0.8f, 0.4f, 0.0f, 1.0f),
        XMFLOAT4(0.8f, 0.4f, 0.0f, 1.0f),
        XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f),
        50.0f,
        m_sphereTexture.Get(),
        m_vertexShader.Get(),
        m_pixelShader.Get()
        );

    auto objects = m_game->RenderObjects();

    // Attach the textures to the appropriate game objects.
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if ((*object)->TargetId() == GameConstants::WorldFloorId)
        {
           (*object)->NormalMaterial(
               ref new Material(
                    XMFLOAT4(0.5f, 0.5f, 0.5f, 1.0f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 1.0f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    150.0f,
                    m_floorTexture.Get(),
                    m_vertexShaderFlat.Get(),
                    m_pixelShaderFlat.Get()
                    )
                );
           (*object)->Mesh(ref new WorldFloorMesh(m_d3dDevice.Get()));
        }
        else if ((*object)->TargetId() == GameConstants::WorldCeilingId)
        {
           (*object)->NormalMaterial(
               ref new Material(
                    XMFLOAT4(0.5f, 0.5f, 0.5f, 1.0f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 1.0f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    150.0f,
                    m_ceilingTexture.Get(),
                    m_vertexShaderFlat.Get(),
                    m_pixelShaderFlat.Get()
                    )
                );
           (*object)->Mesh(ref new WorldCeilingMesh(m_d3dDevice.Get()));
        }
        else if ((*object)->TargetId() == GameConstants::WorldWallsId)
        {
           (*object)->NormalMaterial(
               ref new Material(
                    XMFLOAT4(0.5f, 0.5f, 0.5f, 1.0f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 1.0f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    150.0f,
                    m_wallsTexture.Get(),
                    m_vertexShaderFlat.Get(),
                    m_pixelShaderFlat.Get()
                    )
                );
           (*object)->Mesh(ref new WorldWallsMesh(m_d3dDevice.Get()));
        }
        else if (Cylinder^ cylinder = dynamic_cast<Cylinder^>(*object))
        {
            cylinder->Mesh(cylinderMesh);
            cylinder->NormalMaterial(cylinderMaterial);
        }
        else if (Face^ target = dynamic_cast<Face^>(*object))
        {
            const int bufferLength = 16;
            char16 str[bufferLength];
            int len = swprintf_s(str, bufferLength, L"%d", target->TargetId());
            Platform::String^ string = ref new Platform::String(str, len);

            ComPtr<ID3D11ShaderResourceView> texture;
            textureGenerator->CreateTextureResourceView(string, &texture);
            target->NormalMaterial(
                ref new Material(
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    5.0f,
                    texture.Get(),
                    m_vertexShader.Get(),
                    m_pixelShader.Get()
                    )
                );

            textureGenerator->CreateHitTextureResourceView(string, &texture);
            target->HitMaterial(
                ref new Material(
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.8f, 0.8f, 0.8f, 0.5f),
                    XMFLOAT4(0.3f, 0.3f, 0.3f, 1.0f),
                    5.0f,
                    texture.Get(),
                    m_vertexShader.Get(),
                    m_pixelShader.Get()
                    )
                );

            target->Mesh(targetMesh);
        }
        else if (Sphere^ sphere = dynamic_cast<Sphere^>(*object))
        {
            sphere->Mesh(sphereMesh);
            sphere->NormalMaterial(sphereMaterial);
        }
    }

    // Ensure that the camera has been initialized with the right Projection
    // matrix.  The camera is not created at the time the first window resize event
    // occurs.
    m_game->GameCamera()->SetProjParams(
        XM_PI / 2, m_renderTargetSize.Width / m_renderTargetSize.Height,
        0.01f,
        100.0f
        );

    // Make sure that Projection matrix has been set in the constant buffer
    // now that all the resources are loaded.
    // DirectXBase is handling screen rotations directly to eliminate an unaligned
    // fullscreen copy.  As a result, it is necessary to post multiply the rotationTransform3D
    // matrix to the camera projection matrix.
    ConstantBufferChangeOnResize changesOnResize;
    XMStoreFloat4x4(
        &changesOnResize.projection,
            XMMatrixMultiply(
                XMMatrixTranspose(m_game->GameCamera()->Projection()),
                XMMatrixTranspose(XMLoadFloat4x4(&m_rotationTransform3D))
                )
        );

    m_d3dContext->UpdateSubresource(
        m_constantBufferChangeOnResize.Get(),
        0,
        nullptr,
        &changesOnResize,
        0,
        0
        );

    m_gameResourcesLoaded = true;
}

//----------------------------------------------------------------------

task<void> GameRenderer::LoadLevelResourcesAsync()
{
    m_levelResourcesLoaded = false;

    return create_task([this]()
    {
        // This is where additional async loading of level specific resources
        // would be done.  Because there aren't any to load, just simulate
        // by delaying for some time.
        wait(GameConstants::LevelLoadingDelay);
    });
}

//----------------------------------------------------------------------

void GameRenderer::FinalizeLoadLevelResources()
{
    // After the level specific resources had been loaded, this method is
    // where D3D context specific actions would be handled.  This method
    // runs in the same thread context as the GameRenderer was created.

    m_levelResourcesLoaded = true;
}

//----------------------------------------------------------------------

void GameRenderer::Render()
{
    int renderingPasses = 1;
    if (m_stereoEnabled)
    {
        renderingPasses = 2;
    }

    for (int i = 0; i < renderingPasses; i++)
    {
        if (m_stereoEnabled && i > 0)
        {
            // Doing the Right Eye View.
            m_d3dContext->OMSetRenderTargets(1, m_renderTargetViewRight.GetAddressOf(), m_depthStencilView.Get());
            m_d3dContext->ClearDepthStencilView(m_depthStencilView.Get(), D3D11_CLEAR_DEPTH, 1.0f, 0);
            m_d2dContext->SetTarget(m_d2dTargetBitmapRight.Get());
        }
        else
        {
            // Doing the Mono or Left Eye View.
            m_d3dContext->OMSetRenderTargets(1, m_renderTargetView.GetAddressOf(), m_depthStencilView.Get());
            m_d3dContext->ClearDepthStencilView(m_depthStencilView.Get(), D3D11_CLEAR_DEPTH, 1.0f, 0);
            m_d2dContext->SetTarget(m_d2dTargetBitmap.Get());
        }

        if (m_game != nullptr && m_gameResourcesLoaded && m_levelResourcesLoaded)
        {
            // This section is only used after the game state has been initialized and all device
            // resources needed for the game have been created and associated with the game objects.
            if (m_stereoEnabled)
            {
                ConstantBufferChangeOnResize changesOnResize;
                XMStoreFloat4x4(
                    &changesOnResize.projection,
                    XMMatrixMultiply(
                        XMMatrixTranspose(
                            i == 0 ?
                            m_game->GameCamera()->LeftEyeProjection() :
                            m_game->GameCamera()->RightEyeProjection()
                            ),
                        XMMatrixTranspose(XMLoadFloat4x4(&m_rotationTransform3D))
                        )
                    );

                m_d3dContext->UpdateSubresource(
                    m_constantBufferChangeOnResize.Get(),
                    0,
                    nullptr,
                    &changesOnResize,
                    0,
                    0
                    );
            }
            // Update variables that change one time per frame.

            ConstantBufferChangesEveryFrame constantBufferChangesEveryFrame;
            XMStoreFloat4x4(
                &constantBufferChangesEveryFrame.view,
                XMMatrixTranspose(m_game->GameCamera()->View())
                );
            m_d3dContext->UpdateSubresource(
                m_constantBufferChangesEveryFrame.Get(),
                0,
                nullptr,
                &constantBufferChangesEveryFrame,
                0,
                0
                );

            // Set up Pipeline.

            m_d3dContext->IASetInputLayout(m_vertexLayout.Get());
            m_d3dContext->VSSetConstantBuffers(0, 1, m_constantBufferNeverChanges.GetAddressOf());
            m_d3dContext->VSSetConstantBuffers(1, 1, m_constantBufferChangeOnResize.GetAddressOf());
            m_d3dContext->VSSetConstantBuffers(2, 1, m_constantBufferChangesEveryFrame.GetAddressOf());
            m_d3dContext->VSSetConstantBuffers(3, 1, m_constantBufferChangesEveryPrim.GetAddressOf());

            m_d3dContext->PSSetConstantBuffers(2, 1, m_constantBufferChangesEveryFrame.GetAddressOf());
            m_d3dContext->PSSetConstantBuffers(3, 1, m_constantBufferChangesEveryPrim.GetAddressOf());
            m_d3dContext->PSSetSamplers(0, 1, m_samplerLinear.GetAddressOf());

            // Get all the objects to render from the Game state.
            auto objects = m_game->RenderObjects();
            for (auto object = objects.begin(); object != objects.end(); object++)
            {
                (*object)->Render(m_d3dContext.Get(), m_constantBufferChangesEveryPrim.Get());
            }
        }
        else
        {
            const float ClearColor[4] = {0.1f, 0.1f, 0.1f, 1.0f};

            // Only need to clear the background when not rendering the full 3-D scene because
            // the 3-D world is a fully enclosed box and the dynamics prevents the camera from
            // moving outside this space.
            if (m_stereoEnabled && i > 0)
            {
                // Doing the Right Eye View.
                m_d3dContext->ClearRenderTargetView(m_renderTargetViewRight.Get(), ClearColor);
            }
            else
            {
                // Doing the Mono or Left Eye View.
                m_d3dContext->ClearRenderTargetView(m_renderTargetView.Get(), ClearColor);
            }
        }

        m_d2dContext->BeginDraw();

        // To handle the swapchain being pre-rotated, set the D2D transformation to include it.
        m_d2dContext->SetTransform(m_rotationTransform2D);

        if (m_game != nullptr && m_gameResourcesLoaded)
        {
            // This is only used after the game state has been initialized.
            m_gameHud->Render(m_game, m_d2dContext.Get(), m_windowBounds);
        }

        if (m_gameInfoOverlay->Visible())
        {
            m_d2dContext->DrawBitmap(
                m_gameInfoOverlay->Bitmap(),
                D2D1::RectF(
                    (m_windowBounds.Width - GameInfoOverlayConstant::Width)/2.0f,
                    (m_windowBounds.Height - GameInfoOverlayConstant::Height)/2.0f,
                    (m_windowBounds.Width - GameInfoOverlayConstant::Width)/2.0f + GameInfoOverlayConstant::Width,
                    (m_windowBounds.Height - GameInfoOverlayConstant::Height)/2.0f + GameInfoOverlayConstant::Height
                    )
                );
        }

        HRESULT hr = m_d2dContext->EndDraw();
        if (hr != D2DERR_RECREATE_TARGET)
        {
            // The D2DERR_RECREATE_TARGET indicates there has been a problem with the underlying
            // D3D device.  All subsequent rendering will be ignored until the device is recreated.
            // This error will be propagated and the appropriate D3D error will be returned from the
            // swapchain->Present(...) call.   At that point, the sample will recreate the device
            // and all associated resources.  As a result, the D2DERR_RECREATE_TARGET doesn't
            // need to be handled here.
            DX::ThrowIfFailed(hr);
        }
    }
    Present();
}
```

GameObject.h

```cpp
/// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

#include "MeshObject.h"
#include "SoundEffect.h"
#include "Animate.h"
#include "Material.h"

ref class GameObject
{
internal:
    GameObject();

    // Expect these two functions to be overloaded by subclasses.
    virtual bool IsTouching(
        DirectX::XMFLOAT3 /* point */,
        float /* radius */,
        _Out_ DirectX::XMFLOAT3 *contact,
        _Out_ DirectX::XMFLOAT3 *normal
        )
    {
        *contact = DirectX::XMFLOAT3(0.0f, 0.0f, 0.0f);
        *normal = DirectX::XMFLOAT3(0.0f, 0.0f, 1.0f);
        return false;
    };

    void Render(
        _In_ ID3D11DeviceContext *context,
        _In_ ID3D11Buffer *primitiveConstantBuffer
        );

    void Active(bool active);
    bool Active();
    void Target(bool target);
    bool Target();
    void Hit(bool hit);
    bool Hit();
    void OnGround(bool ground);
    bool OnGround();
    void TargetId(int targetId);
    int  TargetId();
    void HitTime(float t);
    float HitTime();

    void     AnimatePosition(_In_opt_ Animate^ animate);
    Animate^ AnimatePosition();

    void         HitSound(_In_ SoundEffect^ hitSound);
    SoundEffect^ HitSound();

    void PlaySound(float impactSpeed, DirectX::XMFLOAT3 eyePoint);

    void Mesh(_In_ MeshObject^ mesh);

    void NormalMaterial(_In_ Material^ material);
    Material^ NormalMaterial();
    void HitMaterial(_In_ Material^ material);
    Material^ HitMaterial();

    void Position(DirectX::XMFLOAT3 position);
    void Position(DirectX::XMVECTOR position);
    void Velocity(DirectX::XMFLOAT3 velocity);
    void Velocity(DirectX::XMVECTOR velocity);
    DirectX::XMMATRIX ModelMatrix();
    DirectX::XMFLOAT3 Position();
    DirectX::XMVECTOR VectorPosition();
    DirectX::XMVECTOR VectorVelocity();
    DirectX::XMFLOAT3 Velocity();

protected private:
    virtual void UpdatePosition() {};
    // Object Data.
    bool                m_active;
    bool                m_target;
    int                 m_targetId;
    bool                m_hit;
    bool                m_ground;

    DirectX::XMFLOAT3   m_position;
    DirectX::XMFLOAT3   m_velocity;
    DirectX::XMFLOAT4X4 m_modelMatrix;

    Material^           m_normalMaterial;
    Material^           m_hitMaterial;

    DirectX::XMFLOAT3   m_defaultXAxis;
    DirectX::XMFLOAT3   m_defaultYAxis;
    DirectX::XMFLOAT3   m_defaultZAxis;

    float               m_hitTime;

    Animate^            m_animatePosition;
    MeshObject^         m_mesh;

    SoundEffect^        m_hitSound;
};


__forceinline void GameObject::Active(bool active)
{
    m_active = active;
}

__forceinline bool GameObject::Active()
{
    return m_active;
}

__forceinline void GameObject::Target(bool target)
{
    m_target = target;
}

__forceinline bool GameObject::Target()
{
    return m_target;
}

__forceinline void GameObject::Hit(bool hit)
{
    m_hit = hit;
}

__forceinline bool GameObject::Hit()
{
    return m_hit;
}

__forceinline void GameObject::OnGround(bool ground)
{
    m_ground = ground;
}

__forceinline bool GameObject::OnGround()
{
    return m_ground;
}

__forceinline void GameObject::TargetId(int targetId)
{
    m_targetId = targetId;
}

__forceinline int GameObject::TargetId()
{
    return m_targetId;
}

__forceinline void GameObject::HitTime(float t)
{
    m_hitTime = t;
}

__forceinline float GameObject::HitTime()
{
    return m_hitTime;
}

__forceinline void GameObject::Position(DirectX::XMFLOAT3 position)
{
    m_position = position;
    // Update any internal states that are dependent on the position.
    // UpdatePosition is a virtual function that is specific to the derived class.
    UpdatePosition();
}

__forceinline void GameObject::Position(DirectX::XMVECTOR position)
{
    XMStoreFloat3(&m_position, position);
   // Update any internal states that are dependent on the position.
    // UpdatePosition is a virtual function that is specific to the derived class.
    UpdatePosition();
}

__forceinline DirectX::XMFLOAT3 GameObject::Position()
{
    return m_position;
}

__forceinline DirectX::XMVECTOR GameObject::VectorPosition()
{
    return DirectX::XMLoadFloat3(&m_position);
}

__forceinline void GameObject::Velocity(DirectX::XMFLOAT3 velocity)
{
    m_velocity = velocity;
}

__forceinline void GameObject::Velocity(DirectX::XMVECTOR velocity)
{
    XMStoreFloat3(&m_velocity, velocity);
}

__forceinline DirectX::XMFLOAT3 GameObject::Velocity()
{
    return m_velocity;
}

__forceinline DirectX::XMVECTOR GameObject::VectorVelocity()
{
    return DirectX::XMLoadFloat3(&m_velocity);
}

__forceinline void GameObject::AnimatePosition(_In_opt_ Animate^ animate)
{
    m_animatePosition = animate;
}

__forceinline Animate^ GameObject::AnimatePosition()
{
    return m_animatePosition;
}

__forceinline void GameObject::NormalMaterial(_In_ Material^ material)
{
    m_normalMaterial = material;
}

__forceinline Material^ GameObject::NormalMaterial()
{
    return m_normalMaterial;
}

__forceinline void GameObject::HitMaterial(_In_ Material^ material)
{
    m_hitMaterial = material;
}

__forceinline Material^ GameObject::HitMaterial()
{
    return m_hitMaterial;
}

__forceinline void GameObject::Mesh(_In_ MeshObject^ mesh)
{
    m_mesh = mesh;
}

__forceinline void GameObject::HitSound(_In_ SoundEffect^ hitSound)
{
    m_hitSound = hitSound;
}

__forceinline SoundEffect^ GameObject::HitSound()
{
    return m_hitSound;
}

__forceinline DirectX::XMMATRIX GameObject::ModelMatrix()
{
    return DirectX::XMLoadFloat4x4(&m_modelMatrix);
}
```

GameObject.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "GameObject.h"
#include "ConstantBuffers.h"
#include "GameConstants.h"

using namespace DirectX;

//--------------------------------------------------------------------------------

GameObject::GameObject() :
    m_normalMaterial(nullptr),
    m_hitMaterial(nullptr)
{
    m_active          = false;
    m_target          = false;
    m_targetId        = 0;
    m_hit             = false;
    m_ground          = true;

    m_position        = XMFLOAT3(0.0f, 0.0f, 0.0f);
    m_velocity        = XMFLOAT3(0.0f, 0.0f, 0.0f);
    m_defaultXAxis    = XMFLOAT3(1.0f, 0.0f, 0.0f);
    m_defaultYAxis    = XMFLOAT3(0.0f, 1.0f, 0.0f);
    m_defaultZAxis    = XMFLOAT3(0.0f, 0.0f, 1.0f);
    XMStoreFloat4x4(&m_modelMatrix, XMMatrixIdentity());

    m_hitTime         = 0.0f;

    m_animatePosition = nullptr;
}

//----------------------------------------------------------------------

void GameObject::Render(
    _In_ ID3D11DeviceContext *context,
    _In_ ID3D11Buffer *primitiveConstantBuffer
    )
{
    if (!m_active || (m_mesh == nullptr) || (m_normalMaterial == nullptr))
    {
        return;
    }

    ConstantBufferChangesEveryPrim constantBuffer;

    XMStoreFloat4x4(
        &constantBuffer.worldMatrix,
        XMMatrixTranspose(ModelMatrix())
        );

    if (m_hit && m_hitMaterial != nullptr)
    {
        m_hitMaterial->RenderSetup(context, &constantBuffer);
    }
    else
    {
        m_normalMaterial->RenderSetup(context, &constantBuffer);
    }
    context->UpdateSubresource(primitiveConstantBuffer, 0, nullptr, &constantBuffer, 0, 0);

    m_mesh->Render(context);
}

//----------------------------------------------------------------------

void GameObject::PlaySound(float impactSpeed, XMFLOAT3 eyePoint)
{
    if (m_hitSound != nullptr)
    {
        // Determine the sound volume adjustment based on velocity.
        float adjustment;
        if (impactSpeed < GameConstants::Sound::MinVelocity)
        {
            adjustment = 0.0f;  // Too soft.  Don't play sound.
        }
        else
        {
            adjustment = min(1.0f, impactSpeed / GameConstants::Sound::MaxVelocity);
            adjustment = GameConstants::Sound::MinAdjustment + adjustment * (1.0f - GameConstants::Sound::MinAdjustment);
        }

        // Compute Distance to eyePoint to adjust the volume based on that distance.
        XMVECTOR cameraToPosition = XMLoadFloat3(&eyePoint) - VectorPosition();
        float distToPositionSquared = XMVectorGetX(XMVector3LengthSq(cameraToPosition));

        float volume = min(distToPositionSquared, 1.0f);
        // Scale
        // Sound is proportional to how hard the ball is hitting.
        volume = adjustment * volume;

        m_hitSound->PlaySound(volume);
    }
}
```

Animate.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Animate:
// This is an abstract class for animations.  It defines a set of
// capabilities for animating XMFLOAT3 variables.  An animation has the following
// characteristics:
//     Start - the time for the animation to start.
//     Duration - the length of time the animation is to run.
//     Continuous - whether the animation loops after duration is reached or just
//         stops.
// There are two query functions:
//     IsActive - determines if the animation is active at time t.
//     IsFinished - determines if the animation is done at time t.
// It is expected that each derived class will provide an Evaluate method for the
// specific kind of animation.

ref class Animate abstract
{
internal:
    Animate();

    virtual DirectX::XMFLOAT3 Evaluate (_In_ float t) = 0;

    bool  IsActive(_In_ float t) { return ((t >= m_startTime) && (m_continuous || (t < (m_startTime + m_duration)))); };
    bool  IsFinished(_In_ float t) { return (!m_continuous && (t >= (m_startTime + m_duration))); }
    float Start();
    void  Start(_In_ float start);
    float Duration();
    void  Duration(_In_ float duration);
    bool  Continuous();
    void  Continuous(_In_ bool continuous);

protected private:
    bool  m_continuous;      // if true means at end cycle back to beginning
    float m_startTime;       // time at which animation begins
    float m_duration;        // for continuous, this is the duration of 1 cycle through path
};

//----------------------------------------------------------------------

// AnimateLinePosition:
// This class is a specialization of Animate that defines an animation of a position vector
// along a straight line defined in world coordinates from startPosition to endPosition.

ref class AnimateLinePosition: public Animate
{
internal:
    AnimateLinePosition(
        _In_ DirectX::XMFLOAT3 startPosition,
        _In_ DirectX::XMFLOAT3 endPosition,
        _In_ float duration,
        _In_ bool continuous
        );
    virtual DirectX::XMFLOAT3 Evaluate(_In_ float t) override;

private:
    DirectX::XMFLOAT3 m_startPosition;
    DirectX::XMFLOAT3 m_endPosition;
    float             m_length;
};

//----------------------------------------------------------------------

struct LineSegment
{
    DirectX::XMFLOAT3 position;
    float             length;
    float             uStart;
    float             uLength;
};

// AnimateLineListPosition:
// This class is a specialization of Animate that defines an animation of a position vector
// along a set of line segments defined by a set of points.  The animation along the path is
// such that the evaluation of the position along the path will be uniform independent of
// the length of each of the line segments.  A continuous loop can be achieved by having the
// first and last points of the list be the same.

ref class AnimateLineListPosition: public Animate
{
internal:
    AnimateLineListPosition(
        _In_ unsigned int count,
        _In_reads_(count) DirectX::XMFLOAT3 position[],
        _In_ float duration,
        _In_ bool continuous
        );
    virtual DirectX::XMFLOAT3 Evaluate(_In_ float t) override;

private:
    unsigned int             m_count;
    float                    m_totalLength;
    std::vector<LineSegment> m_segment;
};

//----------------------------------------------------------------------

// AnimateCirclePosition:
// This class is a specialization of Animate that defines an animation of a position vector
// along a circular path centered at 'center' with a starting point of 'startPosition'.  The
// distance between 'center' and 'startPosition' defines the radius of the circle.  The plane
// of the circle will pass through 'center' and 'startPosition' and have a normal of 'planeNormal'.
// The direction of the animation can be either clockwise or counterclockwise based
// on the 'clockwise' parameter.

ref class AnimateCirclePosition: public Animate
{
internal:
    AnimateCirclePosition(
        _In_ DirectX::XMFLOAT3 center,
        _In_ DirectX::XMFLOAT3 startPosition,
        _In_ DirectX::XMFLOAT3 planeNormal,
        _In_ float duration,
        _In_ bool continuous,
        _In_ bool clockwise
        );
    virtual DirectX::XMFLOAT3 Evaluate(_In_ float t) override;

private:
    DirectX::XMFLOAT4X4 m_rotationMatrix;
    DirectX::XMFLOAT3   m_center;
    DirectX::XMFLOAT3   m_planeNormal;
    DirectX::XMFLOAT3   m_startPosition;
    float               m_radius;
    bool                m_clockwise;
};

//----------------------------------------------------------------------
            
            
```

Animate.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Animate.h"

using namespace DirectX;

//----------------------------------------------------------------------

Animate::Animate():
    m_continuous(false),
    m_startTime(0.0f),
    m_duration(10.0f)
{
}

//----------------------------------------------------------------------

float Animate::Start()
{
    return m_startTime;
}

//----------------------------------------------------------------------

void Animate::Start(_In_ float start)
{
    m_startTime = start;
}

//----------------------------------------------------------------------

float Animate::Duration()
{
    return m_duration;
}

//----------------------------------------------------------------------

void Animate::Duration(_In_ float duration)
{
    m_duration = duration;
}

//----------------------------------------------------------------------

bool Animate::Continuous()
{
    return m_continuous;
}

//----------------------------------------------------------------------

void Animate::Continuous(_In_ bool continuous)
{
    m_continuous = continuous;
}

//----------------------------------------------------------------------

AnimateLinePosition::AnimateLinePosition(
    _In_ XMFLOAT3 startPosition,
    _In_ XMFLOAT3 endPosition,
    _In_ float duration,
    _In_ bool continuous)
{
    m_startPosition = startPosition;
    m_endPosition = endPosition;
    m_duration = duration;
    m_continuous = continuous;

    m_length = XMVectorGetX(
        XMVector3Length(XMLoadFloat3(&endPosition) - XMLoadFloat3(&startPosition))
        );
}

//----------------------------------------------------------------------

XMFLOAT3 AnimateLinePosition::Evaluate(_In_ float t)
{
    if (t <= m_startTime)
    {
        return m_startPosition;
    }

    if ((t >= (m_startTime + m_duration)) && !m_continuous)
    {
        return m_endPosition;
    }

    float startTime = m_startTime;
    if (m_continuous)
    {
        // For continuous operation, move the start time forward to
        // eliminate previous iterations.
        startTime += ((int)((t - m_startTime) / m_duration)) * m_duration;
    }

    float u = (t - startTime) / m_duration;
    XMFLOAT3 currentPosition;
    currentPosition.x = m_startPosition.x + (m_endPosition.x - m_startPosition.x)*u;
    currentPosition.y = m_startPosition.y + (m_endPosition.y - m_startPosition.y)*u;
    currentPosition.z = m_startPosition.z + (m_endPosition.z - m_startPosition.z)*u;

    return currentPosition;
}

//----------------------------------------------------------------------

AnimateLineListPosition::AnimateLineListPosition(
    _In_ unsigned int count,
    _In_reads_(count) XMFLOAT3 position[],
    _In_ float duration,
    _In_ bool continuous)
{
    m_duration = duration;
    m_continuous = continuous;
    m_count = count;

    std::vector<LineSegment> segment(m_count);
    m_segment = segment;
    m_totalLength = 0.0f;

    m_segment[0].position = position[0];
    for (unsigned int i = 1; i < count; i++)
    {
        m_segment[i].position = position[i];
        m_segment[i - 1].length = XMVectorGetX(
            XMVector3Length(
                XMLoadFloat3(&m_segment[i].position) -
                XMLoadFloat3(&m_segment[i - 1].position)
                )
            );
        m_totalLength += m_segment[i - 1].length;
    }

    // Parameterize the segments to ensure uniform evaluation along the path.
    float u = 0.0f;
    for (unsigned int i = 0; i < (count - 1); i++)
    {
        m_segment[i].uStart = u;
        m_segment[i].uLength = (m_segment[i].length / m_totalLength);
        u += m_segment[i].uLength;
    }
    m_segment[count-1].uStart = 1.0f;
}

//----------------------------------------------------------------------

XMFLOAT3 AnimateLineListPosition::Evaluate(_In_ float t)
{
    if (t <= m_startTime)
    {
        return m_segment[0].position;
    }

    if ((t >= (m_startTime + m_duration)) && !m_continuous)
    {
        return m_segment[m_count-1].position;
    }

    float startTime = m_startTime;
    if (m_continuous)
    {
        // For continuous operation, move the start time forward to
        // eliminate previous iterations.
        startTime += ((int)((t - m_startTime) / m_duration)) * m_duration;
    }

    float u = (t - startTime) / m_duration;
    // Find the right segment.
    unsigned int i = 0;
    while (u > m_segment[i + 1].uStart)
    {
        i++;
    }

    u -= m_segment[i].uStart;
    u /= m_segment[i].uLength;

    XMFLOAT3 currentPosition;
    currentPosition.x = m_segment[i].position.x + (m_segment[i + 1].position.x - m_segment[i].position.x)*u;
    currentPosition.y = m_segment[i].position.y + (m_segment[i + 1].position.y - m_segment[i].position.y)*u;
    currentPosition.z = m_segment[i].position.z + (m_segment[i + 1].position.z - m_segment[i].position.z)*u;

    return currentPosition;
}

//----------------------------------------------------------------------

AnimateCirclePosition:: AnimateCirclePosition(
    _In_ XMFLOAT3 center,
    _In_ XMFLOAT3 startPosition,
    _In_ XMFLOAT3 planeNormal,
    _In_ float duration,
    _In_ bool continuous,
    _In_ bool clockwise)
{
    m_center = center;
    m_planeNormal = planeNormal;
    m_startPosition = startPosition;
    m_duration = duration;
    m_continuous = continuous;
    m_clockwise = clockwise;

    XMVECTOR coordX = XMLoadFloat3(&m_startPosition) - XMLoadFloat3(&m_center);
    m_radius = XMVectorGetX(XMVector3Length(coordX));

    XMVector3Normalize(coordX);

    XMVECTOR coordZ = XMLoadFloat3(&m_planeNormal);
    XMVector3Normalize(coordZ);

    XMVECTOR coordY;
    if (m_clockwise)
    {
        coordY = XMVector3Cross(coordZ, coordX);
    }
    else
    {
        coordY = XMVector3Cross(coordX, coordZ);
    }

    XMVECTOR vectorX = XMVectorSet(1.0f, 0.0f, 0.0f, 1.0f);
    XMVECTOR vectorY = XMVectorSet(0.0f, 1.0f, 0.0f, 1.0f);
    XMMATRIX mat1 = XMMatrixIdentity();
    XMMATRIX mat2 = XMMatrixIdentity();

    if (!XMVector3Equal(coordX, vectorX))
    {
        float angle;
        angle = XMVectorGetX(
            XMVector3AngleBetweenVectors(vectorX, coordX)
            );
        if ((angle * angle) > 0.025)
        {
            XMVECTOR axis1 = XMVector3Cross(vectorX, coordX);

            mat1 = XMMatrixRotationAxis(axis1, angle);
            vectorY = XMVector3TransformCoord(vectorY, mat1);
        }
    }
    if (!XMVector3Equal(vectorY, coordY))
    {
        float angle;
        angle = XMVectorGetX(
            XMVector3AngleBetweenVectors(vectorY, coordY)
            );
        if ((angle * angle) > 0.025)
        {
            XMVECTOR axis2 = XMVector3Cross(vectorY, coordY);
            mat2 = XMMatrixRotationAxis(axis2, angle);
        }
    }
    XMStoreFloat4x4(
        &m_rotationMatrix,
        mat1 *
        mat2 *
        XMMatrixTranslation(m_center.x, m_center.y, m_center.z)
        );
}

//----------------------------------------------------------------------

XMFLOAT3 AnimateCirclePosition::Evaluate(_In_ float t)
{
    if (t <= m_startTime)
    {
        return m_startPosition;
    }

    if ((t >= (m_startTime + m_duration)) && !m_continuous)
    {
        return m_startPosition;
    }

    float startTime = m_startTime;
    if (m_continuous)
    {
        // For continuous operation, move the start time forward to
        // eliminate previous iterations.
        startTime += ((int)((t - m_startTime) / m_duration)) * m_duration;
    }

    float u = (t - startTime) / m_duration * XM_2PI;

    XMFLOAT3 currentPosition;
    currentPosition.x = m_radius * cos(u);
    currentPosition.y = m_radius * sin(u);
    currentPosition.z = 0.0f;

    XMStoreFloat3(
        &currentPosition,
        XMVector3TransformCoord(
            XMLoadFloat3(&currentPosition),
            XMLoadFloat4x4(&m_rotationMatrix)
            )
        );

    return currentPosition;
}

//----------------------------------------------------------------------
            
            
```

BasicLoader.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

#include "BasicReaderWriter.h"

// A simple loader class that provides support for loading shaders, textures,
// and meshes from files on disk. Provides synchronous and asynchronous methods.
ref class BasicLoader
{
internal:
    BasicLoader(
        _In_ ID3D11Device* d3dDevice,
        _In_opt_ IWICImagingFactory2* wicFactory = nullptr
        );

    void LoadTexture(
        _In_ Platform::String^ filename,
        _Out_opt_ ID3D11Texture2D** texture,
        _Out_opt_ ID3D11ShaderResourceView** textureView
        );

    concurrency::task<void> LoadTextureAsync(
        _In_ Platform::String^ filename,
        _Out_opt_ ID3D11Texture2D** texture,
        _Out_opt_ ID3D11ShaderResourceView** textureView
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC layoutDesc[],
        _In_ uint32 layoutDescNumElements,
        _Out_ ID3D11VertexShader** shader,
        _Out_opt_ ID3D11InputLayout** layout
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC layoutDesc[],
        _In_ uint32 layoutDescNumElements,
        _Out_ ID3D11VertexShader** shader,
        _Out_opt_ ID3D11InputLayout** layout
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _Out_ ID3D11PixelShader** shader
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _Out_ ID3D11PixelShader** shader
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _Out_ ID3D11ComputeShader** shader
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _Out_ ID3D11ComputeShader** shader
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _Out_ ID3D11GeometryShader** shader
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _Out_ ID3D11GeometryShader** shader
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _In_reads_opt_(numEntries) const D3D11_SO_DECLARATION_ENTRY* streamOutDeclaration,
        _In_ uint32 numEntries,
        _In_reads_opt_(numStrides) const uint32* bufferStrides,
        _In_ uint32 numStrides,
        _In_ uint32 rasterizedStream,
        _Out_ ID3D11GeometryShader** shader
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _In_reads_opt_(numEntries) const D3D11_SO_DECLARATION_ENTRY* streamOutDeclaration,
        _In_ uint32 numEntries,
        _In_reads_opt_(numStrides) const uint32* bufferStrides,
        _In_ uint32 numStrides,
        _In_ uint32 rasterizedStream,
        _Out_ ID3D11GeometryShader** shader
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _Out_ ID3D11HullShader** shader
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _Out_ ID3D11HullShader** shader
        );

    void LoadShader(
        _In_ Platform::String^ filename,
        _Out_ ID3D11DomainShader** shader
        );

    concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _Out_ ID3D11DomainShader** shader
        );

    void LoadMesh(
        _In_ Platform::String^ filename,
        _Out_ ID3D11Buffer** vertexBuffer,
        _Out_ ID3D11Buffer** indexBuffer,
        _Out_opt_ uint32* vertexCount,
        _Out_opt_ uint32* indexCount
        );

    concurrency::task<void> LoadMeshAsync(
        _In_ Platform::String^ filename,
        _Out_ ID3D11Buffer** vertexBuffer,
        _Out_ ID3D11Buffer** indexBuffer,
        _Out_opt_ uint32* vertexCount,
        _Out_opt_ uint32* indexCount
        );

private:
    Microsoft::WRL::ComPtr<ID3D11Device> m_d3dDevice;
    Microsoft::WRL::ComPtr<IWICImagingFactory2> m_wicFactory;
    BasicReaderWriter^ m_basicReaderWriter;

    template <class DeviceChildType>
    inline void SetDebugName(
        _In_ DeviceChildType* object,
        _In_ Platform::String^ name
        );

    Platform::String^ GetExtension(
        _In_ Platform::String^ filename
        );

    void CreateTexture(
        _In_ bool decodeAsDDS,
        _In_reads_bytes_(dataSize) byte* data,
        _In_ uint32 dataSize,
        _Out_opt_ ID3D11Texture2D** texture,
        _Out_opt_ ID3D11ShaderResourceView** textureView,
        _In_opt_ Platform::String^ debugName
        );

    void CreateInputLayout(
        _In_reads_bytes_(bytecodeSize) byte* bytecode,
        _In_ uint32 bytecodeSize,
        _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC* layoutDesc,
        _In_ uint32 layoutDescNumElements,
        _Out_ ID3D11InputLayout** layout
        );

    void CreateMesh(
        _In_ byte* meshData,
        _Out_ ID3D11Buffer** vertexBuffer,
        _Out_ ID3D11Buffer** indexBuffer,
        _Out_opt_ uint32* vertexCount,
        _Out_opt_ uint32* indexCount,
        _In_opt_ Platform::String^ debugName
        );
};
```

BasicLoader.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "BasicLoader.h"
#include "BasicShapes.h"
#include "DDSTextureLoader.h"
#include "DirectXSample.h"
#include <memory>

using namespace Microsoft::WRL;
using namespace Windows::Storage;
using namespace Windows::Storage::Streams;
using namespace Windows::Foundation;
using namespace Windows::ApplicationModel;
using namespace std;
using namespace concurrency;

BasicLoader::BasicLoader(
    _In_ ID3D11Device* d3dDevice,
    _In_opt_ IWICImagingFactory2* wicFactory
    ) :
    m_d3dDevice(d3dDevice),
    m_wicFactory(wicFactory)
{
    // Create a new BasicReaderWriter to do raw file I/O.
    m_basicReaderWriter = ref new BasicReaderWriter();
}

template <class DeviceChildType>
inline void BasicLoader::SetDebugName(
    _In_ DeviceChildType* object,
    _In_ Platform::String^ name
    )
{
#if defined(_DEBUG)
    // Only assign debug names in debug builds.

    char nameString[1024];
    int nameStringLength = WideCharToMultiByte(
        CP_ACP,
        0,
        name->Data(),
        -1,
        nameString,
        1024,
        nullptr,
        nullptr
        );

    if (nameStringLength == 0)
    {
        char defaultNameString[] = "BasicLoaderObject";
        DX::ThrowIfFailed(
            object->SetPrivateData(
                WKPDID_D3DDebugObjectName,
                sizeof(defaultNameString) - 1,
                defaultNameString
                )
            );
    }
    else
    {
        DX::ThrowIfFailed(
            object->SetPrivateData(
                WKPDID_D3DDebugObjectName,
                nameStringLength - 1,
                nameString
                )
            );
    }
#endif
}

Platform::String^ BasicLoader::GetExtension(
    _In_ Platform::String^ filename
    )
{
    int lastDotIndex = -1;
    for (int i = filename->Length() - 1; i >= 0 && lastDotIndex == -1; i--)
    {
        if (*(filename->Data() + i) == '.')
        {
            lastDotIndex = i;
        }
    }
    if (lastDotIndex != -1)
    {
        std::unique_ptr<wchar_t[]> extension(new wchar_t[filename->Length() - lastDotIndex]);
        for (unsigned int i = 0; i < filename->Length() - lastDotIndex; i++)
        {
            extension[i] = tolower(*(filename->Data() + lastDotIndex + 1 + i));
        }
        return ref new Platform::String(extension.get());
    }
    return "";
}

void BasicLoader::CreateTexture(
    _In_ bool decodeAsDDS,
    _In_reads_bytes_(dataSize) byte* data,
    _In_ uint32 dataSize,
    _Out_opt_ ID3D11Texture2D** texture,
    _Out_opt_ ID3D11ShaderResourceView** textureView,
    _In_opt_ Platform::String^ debugName
    )
{
    ComPtr<ID3D11ShaderResourceView> shaderResourceView;
    ComPtr<ID3D11Texture2D> texture2D;

    if (decodeAsDDS)
    {
        ComPtr<ID3D11Resource> resource;

        if (textureView == nullptr)
        {
            CreateDDSTextureFromMemory(
                m_d3dDevice.Get(),
                data,
                dataSize,
                &resource,
                nullptr
                );
        }
        else
        {
            CreateDDSTextureFromMemory(
                m_d3dDevice.Get(),
                data,
                dataSize,
                &resource,
                &shaderResourceView
                );
        }

        DX::ThrowIfFailed(
            resource.As(&texture2D)
            );
    }
    else
    {
        if (m_wicFactory.Get() == nullptr)
        {
            // A WIC factory object is required in order to load texture
            // assets stored in non-DDS formats.  If BasicLoader was not
            // initialized with one, create one as needed.
            DX::ThrowIfFailed(
                CoCreateInstance(
                    CLSID_WICImagingFactory,
                    nullptr,
                    CLSCTX_INPROC_SERVER,
                    IID_PPV_ARGS(&m_wicFactory)
                    )
                );
        }

        ComPtr<IWICStream> stream;
        DX::ThrowIfFailed(
            m_wicFactory->CreateStream(&stream)
            );

        DX::ThrowIfFailed(
            stream->InitializeFromMemory(
                data,
                dataSize
                )
            );

        ComPtr<IWICBitmapDecoder> bitmapDecoder;
        DX::ThrowIfFailed(
            m_wicFactory->CreateDecoderFromStream(
                stream.Get(),
                nullptr,
                WICDecodeMetadataCacheOnDemand,
                &bitmapDecoder
                )
            );

        ComPtr<IWICBitmapFrameDecode> bitmapFrame;
        DX::ThrowIfFailed(
            bitmapDecoder->GetFrame(0, &bitmapFrame)
            );

        ComPtr<IWICFormatConverter> formatConverter;
        DX::ThrowIfFailed(
            m_wicFactory->CreateFormatConverter(&formatConverter)
            );

        DX::ThrowIfFailed(
            formatConverter->Initialize(
                bitmapFrame.Get(),
                GUID_WICPixelFormat32bppPBGRA,
                WICBitmapDitherTypeNone,
                nullptr,
                0.0,
                WICBitmapPaletteTypeCustom
                )
            );

        uint32 width;
        uint32 height;
        DX::ThrowIfFailed(
            bitmapFrame->GetSize(&width, &height)
            );

        std::unique_ptr<byte[]> bitmapPixels(new byte[width * height * 4]);
        DX::ThrowIfFailed(
            formatConverter->CopyPixels(
                nullptr,
                width * 4,
                width * height * 4,
                bitmapPixels.get()
                )
            );

        D3D11_SUBRESOURCE_DATA initialData;
        ZeroMemory(&initialData, sizeof(initialData));
        initialData.pSysMem = bitmapPixels.get();
        initialData.SysMemPitch = width * 4;
        initialData.SysMemSlicePitch = 0;

        CD3D11_TEXTURE2D_DESC textureDesc(
            DXGI_FORMAT_B8G8R8A8_UNORM,
            width,
            height,
            1,
            1
            );

        DX::ThrowIfFailed(
            m_d3dDevice->CreateTexture2D(
                &textureDesc,
                &initialData,
                &texture2D
                )
            );

        if (textureView != nullptr)
        {
            CD3D11_SHADER_RESOURCE_VIEW_DESC shaderResourceViewDesc(
                texture2D.Get(),
                D3D11_SRV_DIMENSION_TEXTURE2D
                );

            DX::ThrowIfFailed(
                m_d3dDevice->CreateShaderResourceView(
                    texture2D.Get(),
                    &shaderResourceViewDesc,
                    &shaderResourceView
                    )
                );
        }
    }

    SetDebugName(texture2D.Get(), debugName);

    if (texture != nullptr)
    {
        *texture = texture2D.Detach();
    }
    if (textureView != nullptr)
    {
        *textureView = shaderResourceView.Detach();
    }
}

void BasicLoader::CreateInputLayout(
    _In_reads_bytes_(bytecodeSize) byte* bytecode,
    _In_ uint32 bytecodeSize,
    _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC* layoutDesc,
    _In_ uint32 layoutDescNumElements,
    _Out_ ID3D11InputLayout** layout
    )
{
    if (layoutDesc == nullptr)
    {
        // If no input layout is specified, use the BasicVertex layout.
        const D3D11_INPUT_ELEMENT_DESC basicVertexLayoutDesc[] =
        {
            { "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0,  D3D11_INPUT_PER_VERTEX_DATA, 0 },
            { "NORMAL",   0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0 },
            { "TEXCOORD", 0, DXGI_FORMAT_R32G32_FLOAT,    0, 24, D3D11_INPUT_PER_VERTEX_DATA, 0 },
        };

        DX::ThrowIfFailed(
            m_d3dDevice->CreateInputLayout(
                basicVertexLayoutDesc,
                ARRAYSIZE(basicVertexLayoutDesc),
                bytecode,
                bytecodeSize,
                layout
                )
            );
    }
    else
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreateInputLayout(
                layoutDesc,
                layoutDescNumElements,
                bytecode,
                bytecodeSize,
                layout
                )
            );
    }
}

void BasicLoader::CreateMesh(
    _In_ byte* meshData,
    _Out_ ID3D11Buffer** vertexBuffer,
    _Out_ ID3D11Buffer** indexBuffer,
    _Out_opt_ uint32* vertexCount,
    _Out_opt_ uint32* indexCount,
    _In_opt_ Platform::String^ debugName
    )
{
    // The first 4 bytes of the BasicMesh format define the number of vertices in the mesh.
    uint32 numVertices = *reinterpret_cast<uint32*>(meshData);

    // The following 4 bytes define the number of indices in the mesh.
    uint32 numIndices = *reinterpret_cast<uint32*>(meshData + sizeof(uint32));

    // The next segment of the BasicMesh format contains the vertices of the mesh.
    BasicVertex* vertices = reinterpret_cast<BasicVertex*>(meshData + sizeof(uint32) * 2);

    // The last segment of the BasicMesh format contains the indices of the mesh.
    uint16* indices = reinterpret_cast<uint16*>(meshData + sizeof(uint32) * 2 + sizeof(BasicVertex) * numVertices);

    // Create the vertex and index buffers with the mesh data.

    D3D11_SUBRESOURCE_DATA vertexBufferData = {0};
    vertexBufferData.pSysMem = vertices;
    vertexBufferData.SysMemPitch = 0;
    vertexBufferData.SysMemSlicePitch = 0;
    CD3D11_BUFFER_DESC vertexBufferDesc(numVertices * sizeof(BasicVertex), D3D11_BIND_VERTEX_BUFFER);
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(
            &vertexBufferDesc,
            &vertexBufferData,
            vertexBuffer
            )
        );

    D3D11_SUBRESOURCE_DATA indexBufferData = {0};
    indexBufferData.pSysMem = indices;
    indexBufferData.SysMemPitch = 0;
    indexBufferData.SysMemSlicePitch = 0;
    CD3D11_BUFFER_DESC indexBufferDesc(numIndices * sizeof(uint16), D3D11_BIND_INDEX_BUFFER);
    DX::ThrowIfFailed(
        m_d3dDevice->CreateBuffer(
            &indexBufferDesc,
            &indexBufferData,
            indexBuffer
            )
        );

    SetDebugName(*vertexBuffer, Platform::String::Concat(debugName, "_VertexBuffer"));
    SetDebugName(*indexBuffer, Platform::String::Concat(debugName, "_IndexBuffer"));

    if (vertexCount != nullptr)
    {
        *vertexCount = numVertices;
    }
    if (indexCount != nullptr)
    {
        *indexCount = numIndices;
    }
}

void BasicLoader::LoadTexture(
    _In_ Platform::String^ filename,
    _Out_opt_ ID3D11Texture2D** texture,
    _Out_opt_ ID3D11ShaderResourceView** textureView
    )
{
    Platform::Array<byte>^ textureData = m_basicReaderWriter->ReadData(filename);

    CreateTexture(
        GetExtension(filename) == "dds",
        textureData->Data,
        textureData->Length,
        texture,
        textureView,
        filename
        );
}

task<void> BasicLoader::LoadTextureAsync(
    _In_ Platform::String^ filename,
    _Out_opt_ ID3D11Texture2D** texture,
    _Out_opt_ ID3D11ShaderResourceView** textureView
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ textureData)
    {
        CreateTexture(
            GetExtension(filename) == "dds",
            textureData->Data,
            textureData->Length,
            texture,
            textureView,
            filename
            );
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC layoutDesc[],
    _In_ uint32 layoutDescNumElements,
    _Out_ ID3D11VertexShader** shader,
    _Out_opt_ ID3D11InputLayout** layout
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreateVertexShader(
            bytecode->Data,
            bytecode->Length,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);

    if (layout != nullptr)
    {
        CreateInputLayout(
            bytecode->Data,
            bytecode->Length,
            layoutDesc,
            layoutDescNumElements,
            layout
            );

        SetDebugName(*layout, filename);
    }
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC layoutDesc[],
    _In_ uint32 layoutDescNumElements,
    _Out_ ID3D11VertexShader** shader,
    _Out_opt_ ID3D11InputLayout** layout
    )
{
    // This method assumes that the lifetime of input arguments may be shorter
    // than the duration of this task.  In order to ensure accurate results, a
    // copy of all arguments passed by pointer must be made.  The method then
    // ensures that the lifetime of the copied data exceeds that of the task.

    // Create copies of the layoutDesc array as well as the SemanticName strings,
    // both of which are pointers to data whose lifetimes may be shorter than that
    // of this method's task.
    shared_ptr<vector<D3D11_INPUT_ELEMENT_DESC>> layoutDescCopy;
    shared_ptr<vector<string>> layoutDescSemanticNamesCopy;
    if (layoutDesc != nullptr)
    {
        layoutDescCopy.reset(
            new vector<D3D11_INPUT_ELEMENT_DESC>(
                layoutDesc,
                layoutDesc + layoutDescNumElements
                )
            );

        layoutDescSemanticNamesCopy.reset(
            new vector<string>(layoutDescNumElements)
            );

        for (uint32 i = 0; i < layoutDescNumElements; i++)
        {
            layoutDescSemanticNamesCopy->at(i).assign(layoutDesc[i].SemanticName);
        }
    }

    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreateVertexShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);

        if (layout != nullptr)
        {
            if (layoutDesc != nullptr)
            {
                // Reassign the SemanticName elements of the layoutDesc array copy to point
                // to the corresponding copied strings. Performing the assignment inside the
                // lambda body ensures that the lambda will take a reference to the shared_ptr
                // that holds the data.  This will guarantee that the data is still valid when
                // CreateInputLayout is called.
                for (uint32 i = 0; i < layoutDescNumElements; i++)
                {
                    layoutDescCopy->at(i).SemanticName = layoutDescSemanticNamesCopy->at(i).c_str();
                }
            }

            CreateInputLayout(
                bytecode->Data,
                bytecode->Length,
                layoutDesc == nullptr ? nullptr : layoutDescCopy->data(),
                layoutDescNumElements,
                layout
                );

            SetDebugName(*layout, filename);
        }
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _Out_ ID3D11PixelShader** shader
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreatePixelShader(
            bytecode->Data,
            bytecode->Length,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11PixelShader** shader
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreatePixelShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _Out_ ID3D11ComputeShader** shader
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreateComputeShader(
            bytecode->Data,
            bytecode->Length,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11ComputeShader** shader
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreateComputeShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _Out_ ID3D11GeometryShader** shader
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreateGeometryShader(
            bytecode->Data,
            bytecode->Length,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11GeometryShader** shader
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreateGeometryShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _In_reads_opt_(numEntries) const D3D11_SO_DECLARATION_ENTRY* streamOutDeclaration,
    _In_ uint32 numEntries,
    _In_reads_opt_(numStrides) const uint32* bufferStrides,
    _In_ uint32 numStrides,
    _In_ uint32 rasterizedStream,
    _Out_ ID3D11GeometryShader** shader
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreateGeometryShaderWithStreamOutput(
            bytecode->Data,
            bytecode->Length,
            streamOutDeclaration,
            numEntries,
            bufferStrides,
            numStrides,
            rasterizedStream,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _In_reads_opt_(numEntries) const D3D11_SO_DECLARATION_ENTRY* streamOutDeclaration,
    _In_ uint32 numEntries,
    _In_reads_opt_(numStrides) const uint32* bufferStrides,
    _In_ uint32 numStrides,
    _In_ uint32 rasterizedStream,
    _Out_ ID3D11GeometryShader** shader
    )
{
    // This method assumes that the lifetime of input arguments may be shorter
    // than the duration of this task.  In order to ensure accurate results, a
    // copy of all arguments passed by pointer must be made.  The method then
    // ensures that the lifetime of the copied data exceeds that of the task.

    // Create copies of the streamOutDeclaration array as well as the SemanticName
    // strings, both of which are pointers to data whose lifetimes may be shorter
    // than that of this method's task.
    shared_ptr<vector<D3D11_SO_DECLARATION_ENTRY>> streamOutDeclarationCopy;
    shared_ptr<vector<string>> streamOutDeclarationSemanticNamesCopy;
    if (streamOutDeclaration != nullptr)
    {
        streamOutDeclarationCopy.reset(
            new vector<D3D11_SO_DECLARATION_ENTRY>(
                streamOutDeclaration,
                streamOutDeclaration + numEntries
                )
            );

        streamOutDeclarationSemanticNamesCopy.reset(
            new vector<string>(numEntries)
            );

        for (uint32 i = 0; i < numEntries; i++)
        {
            streamOutDeclarationSemanticNamesCopy->at(i).assign(streamOutDeclaration[i].SemanticName);
        }
    }
    
    // Create a copy of the bufferStrides array, which is a pointer to data
    // whose lifetime may be shorter than that of this method's task.
    shared_ptr<vector<uint32>> bufferStridesCopy;
    if (bufferStrides != nullptr)
    {
        bufferStridesCopy.reset(
            new vector<uint32>(
                bufferStrides,
                bufferStrides + numStrides
                )
            );
    }

    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        if (streamOutDeclaration != nullptr)
        {
            // Reassign the SemanticName elements of the streamOutDeclaration array copy to
            // point to the corresponding copied strings. Performing the assignment inside the
            // lambda body ensures that the lambda will take a reference to the shared_ptr
            // that holds the data.  This will guarantee that the data is still valid when
            // CreateGeometryShaderWithStreamOutput is called.
            for (uint32 i = 0; i < numEntries; i++)
            {
                streamOutDeclarationCopy->at(i).SemanticName = streamOutDeclarationSemanticNamesCopy->at(i).c_str();
            }
        }

        DX::ThrowIfFailed(
            m_d3dDevice->CreateGeometryShaderWithStreamOutput(
                bytecode->Data,
                bytecode->Length,
                streamOutDeclaration == nullptr ? nullptr : streamOutDeclarationCopy->data(),
                numEntries,
                bufferStrides == nullptr ? nullptr : bufferStridesCopy->data(),
                numStrides,
                rasterizedStream,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _Out_ ID3D11HullShader** shader
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreateHullShader(
            bytecode->Data,
            bytecode->Length,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11HullShader** shader
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreateHullShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);
    });
}

void BasicLoader::LoadShader(
    _In_ Platform::String^ filename,
    _Out_ ID3D11DomainShader** shader
    )
{
    Platform::Array<byte>^ bytecode = m_basicReaderWriter->ReadData(filename);

    DX::ThrowIfFailed(
        m_d3dDevice->CreateDomainShader(
            bytecode->Data,
            bytecode->Length,
            nullptr,
            shader
            )
        );

    SetDebugName(*shader, filename);
}

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11DomainShader** shader
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        DX::ThrowIfFailed(
            m_d3dDevice->CreateDomainShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader
                )
            );

        SetDebugName(*shader, filename);
    });
}

void BasicLoader::LoadMesh(
    _In_ Platform::String^ filename,
    _Out_ ID3D11Buffer** vertexBuffer,
    _Out_ ID3D11Buffer** indexBuffer,
    _Out_opt_ uint32* vertexCount,
    _Out_opt_ uint32* indexCount
    )
{
    Platform::Array<byte>^ meshData = m_basicReaderWriter->ReadData(filename);

    CreateMesh(
        meshData->Data,
        vertexBuffer,
        indexBuffer,
        vertexCount,
        indexCount,
        filename
        );
}

task<void> BasicLoader::LoadMeshAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11Buffer** vertexBuffer,
    _Out_ ID3D11Buffer** indexBuffer,
    _Out_opt_ uint32* vertexCount,
    _Out_opt_ uint32* indexCount
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ meshData)
    {
        CreateMesh(
            meshData->Data,
            vertexBuffer,
            indexBuffer,
            vertexCount,
            indexCount,
            filename
            );
    });
}
```

MeshObject.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// MeshObject:
// This class is the generic (abstract) representation of D3D11 Indexed triangle
// list.  Each of the derived classes is just the constructor for the specific
// geometry primitive.  This abstract class does not place any requirements on
// the format of the geometry directly.
// The primary method of the MeshObject is Render.  The default implementation
// just sets the IndexBuffer, VertexBuffer, and topology to a TriangleList and
// makes a  DrawIndexed call on the context.  It assumes all other state has
// already been set on the context.

ref class MeshObject abstract
{
internal:
    MeshObject();

    virtual void Render(_In_ ID3D11DeviceContext *context);

protected private:
    Microsoft::WRL::ComPtr<ID3D11Buffer>  m_vertexBuffer;
    Microsoft::WRL::ComPtr<ID3D11Buffer>  m_indexBuffer;
    int                                   m_vertexCount;
    int                                   m_indexCount;
};
            
            
```

MeshObject.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "MeshObject.h"
#include "DirectXSample.h"
#include "ConstantBuffers.h"

using namespace Microsoft::WRL;
using namespace DirectX;

MeshObject::MeshObject():
    m_vertexCount(0),
    m_indexCount(0)
{
}

//--------------------------------------------------------------------------------

void MeshObject::Render(_In_ ID3D11DeviceContext *context)
{
    uint32 stride = sizeof(PNTVertex);
    uint32 offset = 0;

    context->IASetVertexBuffers(0, 1, m_vertexBuffer.GetAddressOf(), &stride, &offset);
    context->IASetIndexBuffer(m_indexBuffer.Get(), DXGI_FORMAT_R16_UINT, 0);
    context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
    context->DrawIndexed(m_indexCount, 0, 0);
}

//--------------------------------------------------------------------------------
            
            
```

SphereMesh.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// SphereMesh:
// This class derives from MeshObject and creates a ID3D11Buffer of
// vertices and indices to represent a canonical sphere that is
// positioned at the origin with a radius of 1.0.

#include "MeshObject.h"

ref class SphereMesh: public MeshObject
{
internal:
    SphereMesh(_In_ ID3D11Device *device, uint32 segments);
};
            
            
```

SphereMesh.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "SphereMesh.h"
#include "DirectXSample.h"
#include "ConstantBuffers.h"

using namespace Microsoft::WRL;
using namespace DirectX;

SphereMesh::SphereMesh(_In_ ID3D11Device *device, uint32 segments)
{
    D3D11_BUFFER_DESC bd = {0};
    D3D11_SUBRESOURCE_DATA initData = {0};

    uint32 slices = segments / 2;
    uint32 numVertices = (slices + 1) * (segments + 1) + 1;
    uint32 numIndices = slices * segments * 3 * 2;

    std::vector<PNTVertex> point(numVertices);
    std::vector<uint16> index(numIndices);

    // To make the texture look right on the top and bottom of the sphere,
    // each slice will have 'segments + 1' vertices.  The top and bottom
    // vertices will all be coincident, but have different U texture cooordinates.
    uint32 p = 0;
    for (uint32 a = 0; a <= slices; a++)
    {
        float angle1 = static_cast<float>(a) / static_cast<float>(slices) * XM_PI;
        float z = cos(angle1);
        float r = sin(angle1);
        for (uint32 b = 0; b <= segments; b++)
        {
            float angle2 = static_cast<float>(b) / static_cast<float>(segments) * XM_2PI;
            point[p].position = XMFLOAT3(r * cos(angle2), r * sin(angle2), z);
            point[p].normal = point[p].position;
            point[p].textureCoordinate = XMFLOAT2((1.0f-z) / 2.0f, static_cast<float>(b) / static_cast<float>(segments));
            p++;
        }
    }
    m_vertexCount = p;

    p = 0;
    for (uint16 a = 0; a < slices; a++)
    {
        uint16 p1 = a * (segments + 1);
        uint16 p2 = (a + 1) * (segments + 1);

        // Generate two triangles for each segment around the slice.
        for (uint16 b = 0; b < segments; b++)
        {
            if (a < (slices - 1))
            {
                // For all but the bottom slice, add the triangle with one
                // vertex in the a slice and two vertices in the a + 1 slice.
                // Skip it for the bottom slice since the triangle would be
                // degenerate as all the vertices in the bottom slice are coincident.
                index[p] = b + p1;
                index[p+1] = b + p2;
                index[p+2] = b + p2 + 1;
                p = p + 3;
            }
            if (a > 0)
            {
                // For all but the top slice, add the triangle with two
                // vertices in the a slice and one vertex in the a + 1 slice.
                // Skip it for the top slice since the triangle would be
                // degenerate as all the vertices in the top slice are coincident.
                index[p] = b + p1;
                index[p+1] = b + p2 + 1;
                index[p+2] = b + p1 + 1;
                p = p + 3;
            }
        }
    }
    m_indexCount = p;

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(PNTVertex) * m_vertexCount;
    bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = point.data();
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_vertexBuffer)
        );

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(uint16) * m_indexCount;
    bd.BindFlags = D3D11_BIND_INDEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = index.data();
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_indexBuffer)
        );
}
            
            
```

CylinderMesh.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// CylinderMesh:
// This class derives from MeshObject and creates a ID3D11Buffer of
// vertices and indices to represent a canonical cylinder (capped at
// both ends) that is positioned at the origin with a radius of 1.0,
// a height of 1.0 and with its axis in the +Z direction.

#include "MeshObject.h"

ref class CylinderMesh: public MeshObject
{
internal:
    CylinderMesh(_In_ ID3D11Device *device, uint32 segments);
};
            
            
```

CylinderMesh.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "CylinderMesh.h"
#include "DirectXSample.h"
#include "ConstantBuffers.h"

using namespace Microsoft::WRL;
using namespace DirectX;

CylinderMesh::CylinderMesh(_In_ ID3D11Device *device, uint32 segments)
{
    D3D11_BUFFER_DESC bd = {0};
    D3D11_SUBRESOURCE_DATA initData = {0};

    uint32 numVertices = 6 * (segments + 1) + 1;
    uint32 numIndices = 3 * segments * 3 * 2;

    std::vector<PNTVertex> point(numVertices);
    std::vector<uint16> index(numIndices);

    uint32 p = 0;
    // Top center point (multiple points for texture coordinates).
    for (uint32 a = 0; a <= segments; a++)
    {
        point[p].position = XMFLOAT3(0.0f, 0.0f, 1.0f);
        point[p].normal = XMFLOAT3(0.0f, 0.0f, 1.0f);
        point[p].textureCoordinate = XMFLOAT2(static_cast<float>(a) / static_cast<float>(segments), 0.0f);
        p++;
    }
    // Top edge of cylinder: Normals point up for lighting of top surface.
    for (uint32 a = 0; a <= segments; a++)
    {
        float angle = static_cast<float>(a) / static_cast<float>(segments) * XM_2PI;
        point[p].position = XMFLOAT3(cos(angle), sin(angle), 1.0f);
        point[p].normal = XMFLOAT3(0.0f, 0.0f, 1.0f);
        point[p].textureCoordinate = XMFLOAT2(static_cast<float>(a) / static_cast<float>(segments), 0.0f);
        p++;
    }
    // Top edge of cylinder: Normals point out for lighting of the side surface.
    for (uint32 a = 0; a <= segments; a++)
    {
        float angle = static_cast<float>(a) / static_cast<float>(segments) * XM_2PI;
        point[p].position = XMFLOAT3(cos(angle), sin(angle), 1.0f);
        point[p].normal = XMFLOAT3(cos(angle), sin(angle), 0.0f);
        point[p].textureCoordinate = XMFLOAT2(static_cast<float>(a) / static_cast<float>(segments), 0.0f);
        p++;
    }
    // Bottom edge of cylinder: Normals point out for lighting of the side surface.
    for (uint32 a = 0; a <= segments; a++)
    {
        float angle = static_cast<float>(a) / static_cast<float>(segments) * XM_2PI;
        point[p].position = XMFLOAT3(cos(angle), sin(angle), 0.0f);
        point[p].normal = XMFLOAT3(cos(angle), sin(angle), 0.0f);
        point[p].textureCoordinate = XMFLOAT2(static_cast<float>(a) / static_cast<float>(segments), 1.0f);
        p++;
    }
    // Bottom edge of cylinder: Normals point down for lighting of the bottom surface.
    for (uint32 a = 0; a <= segments; a++)
    {
        float angle = static_cast<float>(a) / static_cast<float>(segments) * XM_2PI;
        point[p].position = XMFLOAT3(cos(angle), sin(angle), 0.0f);
        point[p].normal = XMFLOAT3(0.0f, 0.0f, -1.0f);
        point[p].textureCoordinate = XMFLOAT2(static_cast<float>(a) / static_cast<float>(segments), 1.0f);
        p++;
    }
    // Bottom center of cylinder: Normals point down for lighting on the bottom surface.
    for (uint32 a = 0; a <= segments; a++)
    {
        point[p].position = XMFLOAT3(0.0f, 0.0f, 0.0f);
        point[p].normal = XMFLOAT3(0.0f, 0.0f, -1.0f);
        point[p].textureCoordinate = XMFLOAT2(static_cast<float>(a) / static_cast<float>(segments), 1.0f);
        p++;
    }
    m_vertexCount = p;

    p = 0;
    for (uint16 a = 0; a < 6; a += 2)
    {
        uint16 p1 = a*(segments + 1);
        uint16 p2 = (a+1)*(segments + 1);
        for (uint16 b = 0; b < segments; b++)
        {
            if (a < 4)
            {
                index[p] = b + p1;
                index[p+1] = b + p2;
                index[p+2] = b + p2 + 1;
                p = p + 3;
            }
            if (a > 0)
            {
                index[p] = b + p1;
                index[p+1] = b + p2 + 1;
                index[p+2] = b + p1 + 1;
                p = p + 3;
            }
        }
    }
    m_indexCount = p;

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(PNTVertex) * m_vertexCount;
    bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = point.data();
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_vertexBuffer)
        );

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(uint16) * m_indexCount;
    bd.BindFlags = D3D11_BIND_INDEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = index.data();
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_indexBuffer)
        );
}
            
            
```

FaceMesh.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// FaceMesh:
// This class derives from MeshObject and creates a ID3D11Buffer of
// vertices and indices to represent a canonical face defined as a
// rectangle at the origin extending 1 unit in the +X and
// 1 unit in the +Y direction.
// The face is defined to be two sided, so it is visible from either
// side.

#include "MeshObject.h"

ref class FaceMesh: public MeshObject
{
internal:
    FaceMesh(_In_ ID3D11Device *device);
};
            
            
```

FaceMesh.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "FaceMesh.h"
#include "DirectXSample.h"
#include "ConstantBuffers.h"

using namespace Microsoft::WRL;
using namespace DirectX;

FaceMesh::FaceMesh(_In_ ID3D11Device *device)
{
    D3D11_BUFFER_DESC bd = {0};
    D3D11_SUBRESOURCE_DATA initData = {0};

    PNTVertex target_vertices[] =
    {
        {XMFLOAT3(0.0f, 0.0f, 0.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(1.0f, 1.0f)},
        {XMFLOAT3(1.0f, 0.0f, 0.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(0.0f, 1.0f)},
        {XMFLOAT3(1.0f, 1.0f, 0.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(0.0f, 0.0f)},
        {XMFLOAT3(0.0f, 1.0f, 0.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(1.0f, 0.0f)}
    };
    WORD target_indices[] =
    {
        0, 1, 2,
        0, 2, 3,
        0, 2, 1,
        0, 3, 2
    };

    m_vertexCount = 4;
    m_indexCount = 12;

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(PNTVertex) * m_vertexCount;
    bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = target_vertices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_vertexBuffer)
        );

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(WORD) * m_indexCount;
    bd.BindFlags = D3D11_BIND_INDEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = target_indices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_indexBuffer)
        );
}
            
            
```

WorldMesh.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

#include "MeshObject.h"

// WorldCeilingMesh:
// This class derives from MeshObject and creates a ID3D11Buffer of
// vertices and indices to represent the ceiling of the bounding cube
// of the world.
// The vertices are defined by a position, a normal and a single
// 2D texture coordinate.

ref class WorldCeilingMesh: public MeshObject
{
internal:
    WorldCeilingMesh(_In_ ID3D11Device *device);
};

// WorldFloorMesh:
// This class derives from MeshObject and creates a ID3D11Buffer of
// vertices and indices to represent the floor of the bounding cube
// of the world.

ref class WorldFloorMesh: public MeshObject
{
internal:
    WorldFloorMesh(_In_ ID3D11Device *device);
};

// WorldWallsMesh:
// This class derives from MeshObject and creates a ID3D11Buffer of
// vertices and indices to represent the walls of the bounding cube
// of the world.

ref class WorldWallsMesh: public MeshObject
{
internal:
    WorldWallsMesh(_In_ ID3D11Device *device);
};
            
            
```

WorldMesh.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "WorldMesh.h"
#include "DirectXSample.h"
#include "ConstantBuffers.h"

using namespace Microsoft::WRL;
using namespace DirectX;

WorldCeilingMesh::WorldCeilingMesh(_In_ ID3D11Device *device)
{
    PNTVertex cellVertices[] =
    {
        // CEILING
        {XMFLOAT3(-4.0f,  3.0f, -6.0f), XMFLOAT3(0.0f, -1.0f, 0.0f), XMFLOAT2(-0.15f, 0.0f)},
        {XMFLOAT3( 4.0f,  3.0f, -6.0f), XMFLOAT3(0.0f, -1.0f, 0.0f), XMFLOAT2( 1.25f, 0.0f)},
        {XMFLOAT3(-4.0f,  3.0f,  6.0f), XMFLOAT3(0.0f, -1.0f, 0.0f), XMFLOAT2(-0.15f, 2.1f)},
        {XMFLOAT3( 4.0f,  3.0f,  6.0f), XMFLOAT3(0.0f, -1.0f, 0.0f), XMFLOAT2( 1.25f, 2.1f)},
    };

    WORD cellIndices[] = {
        0, 1, 2,
        1, 3, 2,
    };

    m_vertexCount = 4;
    m_indexCount = 6;

    D3D11_BUFFER_DESC bd = {0};
    D3D11_SUBRESOURCE_DATA initData = {0};

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(PNTVertex) * m_vertexCount;
    bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = cellVertices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_vertexBuffer)
        );

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(WORD) * m_indexCount;
    bd.BindFlags = D3D11_BIND_INDEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = cellIndices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_indexBuffer)
        );
}

WorldFloorMesh::WorldFloorMesh(_In_ ID3D11Device *device)
{
    PNTVertex cellVertices[] =
    {
        // FLOOR
        {XMFLOAT3(-4.0f, -3.0f,  6.0f), XMFLOAT3(0.0f, 1.0f, 0.0f), XMFLOAT2(0.0f, 0.0f)},
        {XMFLOAT3( 4.0f, -3.0f,  6.0f), XMFLOAT3(0.0f, 1.0f, 0.0f), XMFLOAT2(1.0f, 0.0f)},
        {XMFLOAT3(-4.0f, -3.0f, -6.0f), XMFLOAT3(0.0f, 1.0f, 0.0f), XMFLOAT2(0.0f, 1.5f)},
        {XMFLOAT3( 4.0f, -3.0f, -6.0f), XMFLOAT3(0.0f, 1.0f, 0.0f), XMFLOAT2(1.0f, 1.5f)},
    };

    WORD cellIndices[] = {
        0, 1, 2,
        1, 3, 2,
    };

    m_vertexCount = 4;
    m_indexCount = 6;

    D3D11_BUFFER_DESC bd = {0};
    D3D11_SUBRESOURCE_DATA initData = {0};

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(PNTVertex) * m_vertexCount;
    bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = cellVertices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_vertexBuffer)
        );

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(WORD) * m_indexCount;
    bd.BindFlags = D3D11_BIND_INDEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = cellIndices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_indexBuffer)
        );
}

WorldWallsMesh::WorldWallsMesh(_In_ ID3D11Device *device)
{
    PNTVertex cellVertices[] =
    {
        // WALL
        {XMFLOAT3(-4.0f,  3.0f,  6.0f), XMFLOAT3(0.0f, 0.0f, -1.0f), XMFLOAT2(0.0f, 0.0f)},
        {XMFLOAT3( 4.0f,  3.0f,  6.0f), XMFLOAT3(0.0f, 0.0f, -1.0f), XMFLOAT2(2.0f, 0.0f)},
        {XMFLOAT3(-4.0f, -3.0f,  6.0f), XMFLOAT3(0.0f, 0.0f, -1.0f), XMFLOAT2(0.0f, 1.5f)},
        {XMFLOAT3( 4.0f, -3.0f,  6.0f), XMFLOAT3(0.0f, 0.0f, -1.0f), XMFLOAT2(2.0f, 1.5f)},
        // WALL
        {XMFLOAT3(4.0f,  3.0f,  6.0f), XMFLOAT3(-1.0f, 0.0f, 0.0f), XMFLOAT2(0.0f, 0.0f)},
        {XMFLOAT3(4.0f,  3.0f, -6.0f), XMFLOAT3(-1.0f, 0.0f, 0.0f), XMFLOAT2(3.0f, 0.0f)},
        {XMFLOAT3(4.0f, -3.0f,  6.0f), XMFLOAT3(-1.0f, 0.0f, 0.0f), XMFLOAT2(0.0f, 1.5f)},
        {XMFLOAT3(4.0f, -3.0f, -6.0f), XMFLOAT3(-1.0f, 0.0f, 0.0f), XMFLOAT2(3.0f, 1.5f)},
        // WALL
        {XMFLOAT3( 4.0f,  3.0f, -6.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(0.0f, 0.0f)},
        {XMFLOAT3(-4.0f,  3.0f, -6.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(2.0f, 0.0f)},
        {XMFLOAT3( 4.0f, -3.0f, -6.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(0.0f, 1.5f)},
        {XMFLOAT3(-4.0f, -3.0f, -6.0f), XMFLOAT3(0.0f, 0.0f, 1.0f), XMFLOAT2(2.0f, 1.5f)},
        // WALL
        {XMFLOAT3(-4.0f,  3.0f, -6.0f), XMFLOAT3(1.0f, 0.0f, 0.0f), XMFLOAT2(0.0f, 0.0f)},
        {XMFLOAT3(-4.0f,  3.0f,  6.0f), XMFLOAT3(1.0f, 0.0f, 0.0f), XMFLOAT2(3.0f, 0.0f)},
        {XMFLOAT3(-4.0f, -3.0f, -6.0f), XMFLOAT3(1.0f, 0.0f, 0.0f), XMFLOAT2(0.0f, 1.5f)},
        {XMFLOAT3(-4.0f, -3.0f,  6.0f), XMFLOAT3(1.0f, 0.0f, 0.0f), XMFLOAT2(3.0f, 1.5f)},
    };

    WORD cellIndices[] = {
         0,  1,  2,
         1,  3,  2,
         4,  5,  6,
         5,  7,  6,
         8,  9, 10,
         9, 11, 10,
        12, 13, 14,
        13, 15, 14,
    };

    m_vertexCount = 16;
    m_indexCount = 24;

    D3D11_BUFFER_DESC bd = {0};
    D3D11_SUBRESOURCE_DATA initData = {0};

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(PNTVertex) * m_vertexCount;
    bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = cellVertices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_vertexBuffer)
        );

    bd.Usage = D3D11_USAGE_DEFAULT;
    bd.ByteWidth = sizeof(WORD) * m_indexCount;
    bd.BindFlags = D3D11_BIND_INDEX_BUFFER;
    bd.CPUAccessFlags = 0;
    initData.pSysMem = cellIndices;
    DX::ThrowIfFailed(
        device->CreateBuffer(&bd, &initData, &m_indexBuffer)
        );
}

            
            
```

Level.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level:
// This is an abstract class from which all of the levels of the game are derived.
// Each level potentially overrides up to four methods:
//     Initialize - (required) takes a list of objects and enables the objects that
//         are active for the level as well as setting their positions and
//         any animations associated with the objects.
//     Update - this method is called once per time step and is expected to
//         determine if the level has been completed.  The Level class provides
//         a 'standard' Update method which checks each object that is a target
//         and disables any active targets that have been hit.  It returns true
//         once there are no active targets remaining.
//     SaveState - method to save any Level specific state.  Default is defined as
//         not saving any state.
//     LoadState - method to restore any Level specific state.  Default is defined
//         as not restoring any state.

#include "GameObject.h"
#include "PersistentState.h"

ref class Level abstract
{
internal:
    virtual void Initialize(
        std::vector<GameObject^> objects
        ) = 0;

    virtual bool Update(
        float time,
        float elapsedTime,
        float timeRemaining,
        std::vector<GameObject^> objects
        );

    virtual void SaveState(PersistentState^ state);
    virtual void LoadState(PersistentState^ state);

    Platform::String^ Objective();
    float TimeLimit();

protected private:
    Platform::String^ m_objective;
    float             m_timeLimit;
};
            
            
```

Level.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level.h"

//----------------------------------------------------------------------

bool Level::Update(
    float /* time */,
    float /* elapsedTime */,
    float /* timeRemaining*/,
    std::vector<GameObject^> objects
    )
{
    int left = 0;

    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if ((*object)->Active() && (*object)->Target())
        {
            if ((*object)->Hit())
            {
                (*object)->Active(false);
            }
            else
            {
                left++;
            }
        }
    }
    return (left == 0);
}

//----------------------------------------------------------------------

void Level::SaveState(PersistentState^ /* state */)
{
}

//----------------------------------------------------------------------

void Level::LoadState(PersistentState^ /* state */)
{
}

//----------------------------------------------------------------------

Platform::String^ Level::Objective()
{
    return m_objective;
}

//----------------------------------------------------------------------

float Level::TimeLimit()
{
    return m_timeLimit;
}

//----------------------------------------------------------------------            
            
```

Level1.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level1:
// This class defines the first level of the game.  There are nine active targets.
// Each of the targets is stationary and can be hit in any order.

#include "Level.h"

ref class Level1: public Level
{
internal:
    Level1();
    virtual void Initialize(std::vector<GameObject^> objects) override;
};
            
```

Level1.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level1.h"
#include "Face.h"

using namespace DirectX;

//----------------------------------------------------------------------

Level1::Level1()
{
    m_timeLimit = 20.0f;
    m_objective =  "Hit each of the targets before time runs out.\nTouch to aim.  Tap in right box to fire.  Drag in left box to move.";
}

//----------------------------------------------------------------------

void Level1::Initialize(std::vector<GameObject^> objects)
{
    XMFLOAT3 position[] =
    {
        XMFLOAT3(-2.5f, -1.0f, -1.5f),
        XMFLOAT3(-1.0f,  1.0f, -3.0f),
        XMFLOAT3( 1.5f,  0.0f, -3.0f),
        XMFLOAT3(-2.5f, -1.0f, -5.5f),
        XMFLOAT3( 0.5f, -2.0f, -5.0f),
        XMFLOAT3( 1.5f, -2.0f, -5.5f),
        XMFLOAT3( 2.0f,  0.0f,  0.0f),
        XMFLOAT3( 0.0f,  0.0f,  0.0f),
        XMFLOAT3(-2.0f,  0.0f,  0.0f)
    };

    int targetCount = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if (Face^ target = dynamic_cast<Face^>(*object))
        {
            if (targetCount < 9)
            {
                target->Active(true);
                target->Target(true);
                target->Hit(false);
                target->AnimatePosition(nullptr);
                target->Position(position[targetCount]);
                targetCount++;
            }
            else
            {
                (*object)->Active(false);
            }
        }
        else
        {
            (*object)->Active(false);
        }
    }
}

//----------------------------------------------------------------------
            
            
```

Level2.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level2:
// This class defines the second level of the game.  It derives from the
// first level.  In this level, the targets must be hit in numeric order.

#include "Level1.h"

ref class Level2: public Level1
{
internal:
    Level2();
    virtual void Initialize(std::vector<GameObject^> objects) override;

    virtual bool Update(
        float time,
        float elapsedTime,
        float timeRemaining,
        std::vector<GameObject^> objects
        ) override;

    virtual void SaveState(PersistentState^ state) override;
    virtual void LoadState(PersistentState^ state) override;

private:
    int m_nextId;
};
            
            
```

Level2.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level2.h"
#include "Face.h"

//----------------------------------------------------------------------

Level2::Level2()
{
    m_timeLimit = 30.0f;
    m_objective = "Hit each of the targets in ORDER before time runs out.";
}

//----------------------------------------------------------------------

void Level2::Initialize(std::vector<GameObject^> objects)
{
    Level1::Initialize(objects);

    int targetCount = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if (Face^ target = dynamic_cast<Face^>(*object))
        {
            if (targetCount < 9)
            {
                target->Target(targetCount == 0 ? true : false);
                targetCount++;
            }
        }
    }
    m_nextId = 1;
}

//----------------------------------------------------------------------

bool Level2::Update(
    float /* time */,
    float /* elapsedTime */,
    float /* timeRemaining */,
    std::vector<GameObject^> objects
    )
{
    int left = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if ((*object)->Active() && ((*object)->TargetId() > 0))
        {
            if ((*object)->Hit()  && ((*object)->TargetId() == m_nextId))
            {
                (*object)->Active(false);
                m_nextId++;
            }
            else
            {
                left++;
            }
        }
        if ((*object)->Active() && ((*object)->TargetId() == m_nextId))
        {
            (*object)->Target(true);
        }
    }
    return (left == 0);
}

//----------------------------------------------------------------------

void Level2::SaveState(PersistentState^ state)
{
    state->SaveInt32(":NextTarget", m_nextId);
}

//----------------------------------------------------------------------

void Level2::LoadState(PersistentState^ state)
{
    m_nextId = state->LoadInt32(":NextTarget", 1);
}

//----------------------------------------------------------------------            
            
```

Level3.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level3:
// This class defines the third level of the game.  In this level, each of the
// nine targets is moving along closed paths and can be hit
// in any order.

#include "Level.h"

ref class Level3: public Level
{
internal:
    Level3();
    virtual void Initialize(std::vector<GameObject^> objects) override;
};
            
            
```

Level3.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level3.h"
#include "Face.h"
#include "Animate.h"

using namespace DirectX;

//----------------------------------------------------------------------

Level3::Level3()
{
    m_timeLimit = 30.0f;
    m_objective = "Hit each of the moving targets before time runs out.";
}

//----------------------------------------------------------------------

void Level3::Initialize(std::vector<GameObject^> objects)
{
    XMFLOAT3 position[] =
    {
        XMFLOAT3(-2.5f, -1.0f, -1.5f),
        XMFLOAT3(-1.0f,  1.0f, -3.0f),
        XMFLOAT3( 1.5f,  0.0f, -5.5f),
        XMFLOAT3(-2.5f, -1.0f, -5.5f),
        XMFLOAT3( 0.5f, -2.0f, -5.0f),
        XMFLOAT3( 1.5f, -2.0f, -5.5f),
        XMFLOAT3( 0.0f, -3.6f,  0.0f),
        XMFLOAT3( 0.0f, -3.6f,  0.0f),
        XMFLOAT3( 0.0f, -3.6f,  0.0f)
    };
    XMFLOAT3 LineList1[] =
    {
        XMFLOAT3(-2.5f, -1.0f, -1.5f),
        XMFLOAT3(-0.5f,  1.0f,  1.0f),
        XMFLOAT3(-0.5f, -2.5f,  1.0f),
        XMFLOAT3(-2.5f, -1.0f, -1.5f),
    };
    XMFLOAT3 LineList2[] =
    {
        XMFLOAT3(-1.0f,  1.0f, -3.0f),
        XMFLOAT3(-2.0f,  2.0f, -1.5f),
        XMFLOAT3(-2.0f, -2.5f, -1.5f),
        XMFLOAT3( 1.5f, -2.5f, -1.5f),
        XMFLOAT3( 1.5f, -2.5f, -3.0f),
        XMFLOAT3(-1.0f,  1.0f, -3.0f),
    };
    XMFLOAT3 LineList3[] =
    {
        XMFLOAT3(1.5f,  0.0f, -5.5f),
        XMFLOAT3(1.5f,  1.0f, -5.5f),
        XMFLOAT3(1.5f, -2.5f, -5.5f),
        XMFLOAT3(1.5f,  0.0f, -5.5f),
    };
    XMFLOAT3 LineList4[] =
    {
        XMFLOAT3(-2.5f, -1.0f, -5.5f),
        XMFLOAT3( 1.0f, -1.0f, -5.5f),
        XMFLOAT3( 1.0f,  1.0f, -5.5f),
        XMFLOAT3(-2.5f,  1.0f, -5.5f),
        XMFLOAT3(-2.5f, -1.0f, -5.5f),
    };
    XMFLOAT3 LineList5[] =
    {
        XMFLOAT3( 0.5f, -2.0f, -5.0f),
        XMFLOAT3( 2.0f, -2.0f, -5.0f),
        XMFLOAT3( 2.0f,  1.0f, -5.0f),
        XMFLOAT3(-2.5f,  1.0f, -5.0f),
        XMFLOAT3(-2.5f, -2.0f, -5.0f),
        XMFLOAT3( 0.5f, -2.0f, -5.0f),
    };
    XMFLOAT3 LineList6[] =
    {
        XMFLOAT3( 1.5f, -2.0f, -5.5f),
        XMFLOAT3(-2.5f, -2.0f, -5.5f),
        XMFLOAT3(-2.5f,  1.0f, -5.5f),
        XMFLOAT3( 1.5f,  1.0f, -5.5f),
        XMFLOAT3( 1.5f, -2.0f, -5.5f),
    };

    int targetCount = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if (Face^ target = dynamic_cast<Face^>(*object))
        {
            if (targetCount < 9)
            {
                target->Active(true);
                target->Target(true);
                target->Hit(false);
                target->Position(position[targetCount]);
                switch (targetCount)
                {
                case 0:
                    target->AnimatePosition(ref new AnimateLineListPosition(4, LineList1, 10.0f, true));
                    break;
                case 1:
                    target->AnimatePosition(ref new AnimateLineListPosition(6, LineList2, 15.0f, true));
                    break;
                case 2:
                    target->AnimatePosition(ref new AnimateLineListPosition(4, LineList3, 15.0f, true));
                    break;
                case 3:
                    target->AnimatePosition(ref new AnimateLineListPosition(5, LineList4, 15.0f, true));
                    break;
                case 4:
                    target->AnimatePosition(ref new AnimateLineListPosition(6, LineList5, 15.0f, true));
                    break;
                case 5:
                    target->AnimatePosition(ref new AnimateLineListPosition(5, LineList6, 15.0f, true));
                    break;
                case 6:
                    target->AnimatePosition(
                        ref new AnimateCirclePosition(
                            XMFLOAT3(0.0f, -2.5f, 0.0f),
                            XMFLOAT3(0.0f, -3.6f, 0.0f),
                            XMFLOAT3(0.0f,  0.0f, 1.0f),
                            9.0f,
                            true,
                            true
                            )
                        );
                    break;
                case 7:
                    target->AnimatePosition(
                        ref new AnimateCirclePosition(
                            XMFLOAT3(0.0f, -2.5f, 0.0f),
                            XMFLOAT3(0.0f, -3.6f, 0.0f),
                            XMFLOAT3(0.0f,  0.0f, 1.0f),
                            9.0f,
                            true,
                            true
                            )
                        );
                    target->AnimatePosition()->Start(3.0f);
                    break;
                case 8:
                    target->AnimatePosition(
                        ref new AnimateCirclePosition(
                            XMFLOAT3(0.0f, -2.5f, 0.0f),
                            XMFLOAT3(0.0f, -3.6f, 0.0f),
                            XMFLOAT3(0.0f,  0.0f, 1.0f),
                            9.0f,
                            true,
                            true
                            )
                        );
                    target->AnimatePosition()->Start(6.0f);
                    break;
                }
                targetCount++;
            }
            else
            {
                target->Active(false);
            }
        }
        else
        {
            (*object)->Active(false);
        }
    }
}

//----------------------------------------------------------------------
            
            
```

Level4.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level4:
// This class defines the fourth level of the game.  It derives from the
// third level.  The targets must be hit in numeric order.

#include "Level3.h"

ref class Level4: public Level3
{
internal:
    Level4();
    virtual void Initialize(std::vector<GameObject^> objects) override;

    virtual bool Update(
        float time,
        float elapsedTime,
        float timeRemaining,
        std::vector<GameObject^> objects
        ) override;

    virtual void SaveState(PersistentState^ state) override;
    virtual void LoadState(PersistentState^ state) override;

private:
    int m_nextId;
};
            
            
```

Level4.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level4.h"
#include "Face.h"

//----------------------------------------------------------------------

Level4::Level4()
{
    m_timeLimit = 30.0f;
    m_objective = "Hit each of the moving targets in ORDER before time runs out.";
}

//----------------------------------------------------------------------

void Level4::Initialize(std::vector<GameObject^> objects)
{
    Level3::Initialize(objects);

    int targetCount = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if (Face^ target = dynamic_cast<Face^>(*object))
        {
            if (targetCount < 9)
            {
                target->Target(targetCount == 0 ? true : false);
                targetCount++;
            }
        }
    }
    m_nextId = 1;
}

//----------------------------------------------------------------------

bool Level4::Update(
    float /* time */,
    float /* elapsedTime */,
    float /* timeRemaining */,
    std::vector<GameObject^> objects
    )
{
    int left = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if ((*object)->Active() && ((*object)->TargetId() > 0))
        {
            if ((*object)->Hit() && ((*object)->TargetId() == m_nextId))
            {
                (*object)->Active(false);
                m_nextId++;
            }
            else
            {
                left++;
            }
        }
        if ((*object)->Active() && ((*object)->TargetId() == m_nextId))
        {
            (*object)->Target(true);
        }
    }
    return (left == 0);
}

//----------------------------------------------------------------------

void Level4::SaveState(PersistentState^ state)
{
    state->SaveInt32(":NextTarget", m_nextId);
}

//----------------------------------------------------------------------

void Level4::LoadState(PersistentState^ state)
{
    m_nextId = state->LoadInt32(":NextTarget", 1);
}

//----------------------------------------------------------------------
            
            
```

Level5.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level5:
// This class defines the fifth level of the game.  It derives from the
// third level.  This level introduces obstacles that move into place
// during game play.  The targets may be hit in any order.

#include "Level3.h"

ref class Level5: public Level3
{
internal:
    Level5();
    virtual void Initialize(std::vector<GameObject^> objects) override;
};
            
            
```

Level5.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level5.h"
#include "Cylinder.h"
#include "Animate.h"

using namespace DirectX;

//----------------------------------------------------------------------

Level5::Level5()
{
    m_timeLimit = 30.0f;
    m_objective = "Hit each of the moving targets while avoiding the obstacles before time runs out.";
}

//----------------------------------------------------------------------

void Level5::Initialize(std::vector<GameObject^> objects)
{
    Level3::Initialize(objects);

    XMFLOAT3 obstacleStartPosition[] =
    {
        XMFLOAT3(-4.5f, -3.0f, 0.0f),
        XMFLOAT3(4.5f, -3.0f, 0.0f),
        XMFLOAT3(0.0f, 3.01f, -2.0f),
        XMFLOAT3(-1.5f, -3.0f, -6.5f),
        XMFLOAT3(1.5f, -3.0f, -6.5f)
    };
    XMFLOAT3 obstacleEndPosition[] =
    {
        XMFLOAT3(-2.0f, -3.0f, 0.0f),
        XMFLOAT3(2.0f, -3.0f, 0.0f),
        XMFLOAT3(0.0f, -3.0f, -2.0f),
        XMFLOAT3(-1.5f, -3.0f, -4.0f),
        XMFLOAT3(1.5f, -3.0f, -4.0f)
    };
    float obstacleStartTime[] =
    {
        2.0f,
        5.0f,
        8.0f,
        11.0f,
        14.0f
    };

    int obstacleCount = 0;
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if (Cylinder^ obstacle = dynamic_cast<Cylinder^>(*object))
        {
            if (obstacleCount < 5)
            {
                obstacle->Active(true);
                obstacle->Position(obstacleStartPosition[obstacleCount]);
                obstacle->AnimatePosition(
                    ref new AnimateLinePosition(
                        obstacleStartPosition[obstacleCount],
                        obstacleEndPosition[obstacleCount],
                        10.0,
                        false
                        )
                    );
                obstacle->AnimatePosition()->Start(obstacleStartTime[obstacleCount]);
                obstacleCount ++;
            }
        }
    }
}

//----------------------------------------------------------------------            
            
```

Level6.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Level6:
// This class defines the sixth and final level of the game.  It derives from the
// fifth level.  In this level, the targets do not disappear when they are hit.
// The target will stay highlighted for two seconds.  As this is the last level,
// the only criteria for completion is time expiring.

#include "Level5.h"

ref class Level6: public Level5
{
internal:
    Level6();
    virtual bool Update(
        float time,
        float elapsedTime,
        float timeRemaining,
        std::vector<GameObject^> objects
        ) override;
};
            
            
```

Level6.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Level6.h"

//----------------------------------------------------------------------

Level6::Level6()
{
    m_timeLimit = 20.0f;
    m_objective = "Hit as many moving targets as possible while avoiding the obstacles before time runs out.";
}

//----------------------------------------------------------------------

bool Level6::Update(
    float time,
    float elapsedTime,
    float timeRemaining,
    std::vector<GameObject^> objects
    )
{
    for (auto object = objects.begin(); object != objects.end(); object++)
    {
        if ((*object)->Active() && (*object)->Target())
        {
            if ((*object)->Hit() && ((*object)->HitTime() < (time - 2.0f)))
            {
                (*object)->Hit(false);
            }
        }
    }
    return ((timeRemaining - elapsedTime) <= 0.0f);
}

//----------------------------------------------------------------------            
            
```

TargetTexture.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// TargetTexture:
// This is a helper class to procedurally generate textures for game
// targets.  There are two versions of the textures, one when it is
// hit and the other is when it is not.
// The class creates the necessary resources to draw the texture into
// an off screen resource at initialization time.

ref class TargetTexture
{
internal:
    TargetTexture(
        _In_ ID3D11Device1*      d3dDevice,
        _In_ ID2D1Factory1*      d2dFactory,
        _In_ IDWriteFactory1*    dwriteFactory,
        _In_ ID2D1DeviceContext* d2dContext
        );

    void CreateTextureResourceView(
        _In_ Platform::String^ name,
        _Out_ ID3D11ShaderResourceView** textureResourceView
        );
    void CreateHitTextureResourceView(
        _In_ Platform::String^ name,
        _Out_ ID3D11ShaderResourceView** textureResourceView
        );

protected private:
    Microsoft::WRL::ComPtr<ID3D11Device1>           m_d3dDevice;
    Microsoft::WRL::ComPtr<ID2D1Factory1>           m_d2dFactory;
    Microsoft::WRL::ComPtr<ID2D1DeviceContext>      m_d2dContext;
    Microsoft::WRL::ComPtr<IDWriteFactory1>         m_dwriteFactory;

    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_redBrush;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_blueBrush;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_greenBrush;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_whiteBrush;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_blackBrush;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_yellowBrush;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_clearBrush;

    Microsoft::WRL::ComPtr<ID2D1EllipseGeometry>    m_circleGeometry1;
    Microsoft::WRL::ComPtr<ID2D1EllipseGeometry>    m_circleGeometry2;
    Microsoft::WRL::ComPtr<ID2D1EllipseGeometry>    m_circleGeometry3;
    Microsoft::WRL::ComPtr<ID2D1EllipseGeometry>    m_circleGeometry4;
    Microsoft::WRL::ComPtr<ID2D1EllipseGeometry>    m_circleGeometry5;

    Microsoft::WRL::ComPtr<IDWriteTextFormat>       m_textFormat;
};            
            
```

TargetTexture.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "TargetTexture.h"
#include "DirectXSample.h"

using namespace Microsoft::WRL;
using namespace Windows::Graphics::Display;

TargetTexture::TargetTexture(
    _In_ ID3D11Device1* d3dDevice,
    _In_ ID2D1Factory1* d2dFactory,
    _In_ IDWriteFactory1* dwriteFactory,
    _In_ ID2D1DeviceContext* d2dContext
    )
{
    m_d3dDevice = d3dDevice;
    m_d2dFactory = d2dFactory;
    m_dwriteFactory = dwriteFactory;
    m_d2dContext = d2dContext;

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::Red, 1.f),
            &m_redBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::CornflowerBlue, 1.0f),
            &m_blueBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::Green, 1.f),
            &m_greenBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::White, 1.f),
            &m_whiteBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::Black, 1.f),
            &m_blackBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::Yellow, 1.f),
            &m_yellowBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dContext->CreateSolidColorBrush(
            D2D1::ColorF(D2D1::ColorF::White, 0.0f),
            &m_clearBrush
            )
        );

    DX::ThrowIfFailed(
        m_d2dFactory->CreateEllipseGeometry(
            D2D1::Ellipse(
                D2D1::Point2F(256.0f, 256.0f),
                50.0f,
                50.0f
                ),
            &m_circleGeometry1)
            );

    DX::ThrowIfFailed(
        m_d2dFactory->CreateEllipseGeometry(
            D2D1::Ellipse(
                D2D1::Point2F(256.0f, 256.0f),
                100.0f,
                100.0f
                ),
            &m_circleGeometry2)
            );

    DX::ThrowIfFailed(
        m_d2dFactory->CreateEllipseGeometry(
            D2D1::Ellipse(
                D2D1::Point2F(256.0f, 256.0f),
                150.0f,
                150.0f
                ),
            &m_circleGeometry3)
            );

    DX::ThrowIfFailed(
        m_d2dFactory->CreateEllipseGeometry(
            D2D1::Ellipse(
                D2D1::Point2F(256.0f, 256.0f),
                200.0f,
                200.0f
                ),
            &m_circleGeometry4)
            );

    DX::ThrowIfFailed(
        m_d2dFactory->CreateEllipseGeometry(
            D2D1::Ellipse(
                D2D1::Point2F(256.0f, 256.0f),
                250.0f,
                250.0f
                ),
            &m_circleGeometry5)
            );

    DX::ThrowIfFailed(
        m_dwriteFactory->CreateTextFormat(
            L"Segoe UI",
            nullptr,
            DWRITE_FONT_WEIGHT_LIGHT,
            DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH_NORMAL,
            425,        // fontsize
            L"en-US",   // locale
            &m_textFormat
            )
        );

    // Center the text horizontally.
    DX::ThrowIfFailed(
        m_textFormat->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER)
        );

    // Center the text vertically.
    DX::ThrowIfFailed(
        m_textFormat->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_CENTER)
        );
}
//----------------------------------------------------------------------
void TargetTexture::CreateTextureResourceView(
    _In_ Platform::String^ name,
    _Out_ ID3D11ShaderResourceView** textureResourceView
    )
{
    // Allocate a offscreen D3D surface for D2D to render our 2D content into
    D3D11_TEXTURE2D_DESC texDesc;
    texDesc.ArraySize = 1;
    texDesc.BindFlags = D3D11_BIND_RENDER_TARGET | D3D11_BIND_SHADER_RESOURCE;
    texDesc.CPUAccessFlags = 0;
    texDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
    texDesc.Height = 512;
    texDesc.Width = 512;
    texDesc.MipLevels = 1;
    texDesc.MiscFlags = 0;
    texDesc.SampleDesc.Count = 1;
    texDesc.SampleDesc.Quality = 0;
    texDesc.Usage = D3D11_USAGE_DEFAULT;

    ComPtr<ID3D11Texture2D> offscreenTexture;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateTexture2D(&texDesc, nullptr, &offscreenTexture)
        );

    // Convert the Direct2D texture into a Shader Resource View
    ComPtr<ID3D11ShaderResourceView> texture;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateShaderResourceView(offscreenTexture.Get(), nullptr, &texture)
        );
#if defined(_DEBUG)
    {
        char debugName[100];
        int l = sprintf_s(debugName, sizeof(debugName) / sizeof(debugName[0]), "Simple3DGame Target %ls", name->Data());
        DX::ThrowIfFailed(
            texture->SetPrivateData(WKPDID_D3DDebugObjectName, l, debugName)
            );
    }
#endif

    ComPtr<IDXGISurface> dxgiSurface;
    DX::ThrowIfFailed(
        offscreenTexture.As(&dxgiSurface)
        );

    // Create a D2D render target which can draw into our offscreen D3D
    // surface. Given that we use a constant size for the texture, we
    // fix the DPI at 96.

    D2D1_BITMAP_PROPERTIES1 properties;
    properties.pixelFormat.format = DXGI_FORMAT_B8G8R8A8_UNORM;
    properties.pixelFormat.alphaMode = D2D1_ALPHA_MODE_PREMULTIPLIED;
    properties.dpiX = 96;
    properties.dpiY = 96;
    properties.bitmapOptions = D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW;
    properties.colorContext = nullptr;

    ComPtr<ID2D1Bitmap1> renderTarget;
    DX::ThrowIfFailed(
        m_d2dContext->CreateBitmapFromDxgiSurface(
            dxgiSurface.Get(),
            &properties,
            &renderTarget
            )
        );

    m_d2dContext->SetTarget(renderTarget.Get());
    float saveDpiX;
    float saveDpiY;

    m_d2dContext->GetDpi(&saveDpiX, &saveDpiY);
    m_d2dContext->SetDpi(96.0f, 96.0f);

    m_d2dContext->BeginDraw();
    m_d2dContext->SetTransform(D2D1::Matrix3x2F::Identity());

    D2D1_SIZE_F renderTargetSize = renderTarget->GetSize();

    m_d2dContext->Clear(D2D1::ColorF(D2D1::ColorF::White));
    m_d2dContext->FillGeometry(m_circleGeometry5.Get(), m_redBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry4.Get(), m_blueBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry3.Get(), m_greenBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry2.Get(), m_yellowBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry1.Get(), m_blackBrush.Get());
    m_d2dContext->DrawText(
        name->Data(),
        name->Length(),
        m_textFormat.Get(),
        D2D1::RectF(0, 0, renderTargetSize.width, renderTargetSize.height),
        m_whiteBrush.Get()
        );

    // We ignore D2DERR_RECREATE_TARGET here. This error indicates that the device
    // is lost. It will be handled during the next call to Present.
    HRESULT hr = m_d2dContext->EndDraw();
    if (hr != D2DERR_RECREATE_TARGET)
    {
        DX::ThrowIfFailed(hr);
    }

    m_d2dContext->SetTarget(nullptr);
    m_d2dContext->SetDpi(saveDpiX, saveDpiY);

    *textureResourceView = texture.Detach();
}
//----------------------------------------------------------------------
void TargetTexture::CreateHitTextureResourceView(
    _In_ Platform::String^ name,
    _Out_ ID3D11ShaderResourceView** textureResourceView
    )
{
    // Allocate a offscreen D3D surface for D2D to render our 2D content into
    D3D11_TEXTURE2D_DESC texDesc;
    texDesc.ArraySize = 1;
    texDesc.BindFlags = D3D11_BIND_RENDER_TARGET | D3D11_BIND_SHADER_RESOURCE;
    texDesc.CPUAccessFlags = 0;
    texDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
    texDesc.Height = 512;
    texDesc.Width = 512;
    texDesc.MipLevels = 1;
    texDesc.MiscFlags = 0;
    texDesc.SampleDesc.Count = 1;
    texDesc.SampleDesc.Quality = 0;
    texDesc.Usage = D3D11_USAGE_DEFAULT;

    ComPtr<ID3D11Texture2D> offscreenTexture;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateTexture2D(&texDesc, nullptr, &offscreenTexture)
        );

    // Convert the Direct2D texture into a Shader Resource View.
    ComPtr<ID3D11ShaderResourceView> texture;
    DX::ThrowIfFailed(
        m_d3dDevice->CreateShaderResourceView(offscreenTexture.Get(), nullptr, &texture)
        );
#if defined(_DEBUG)
    {
        char debugName[100];
        int l = sprintf_s(debugName, sizeof(debugName) / sizeof(debugName[0]), "Simple3DGame HitTarget %ls", name->Data());
        DX::ThrowIfFailed(
            texture->SetPrivateData(WKPDID_D3DDebugObjectName, l, debugName)
            );
    }
#endif

    ComPtr<IDXGISurface> dxgiSurface;
    DX::ThrowIfFailed(
        offscreenTexture.As(&dxgiSurface)
        );

    // Create a D2D render target which can draw into our offscreen D3D
    // surface. Given that we use a constant size for the texture, we
    // fix the DPI at 96.

    D2D1_BITMAP_PROPERTIES1 properties;
    properties.pixelFormat.format = DXGI_FORMAT_B8G8R8A8_UNORM;
    properties.pixelFormat.alphaMode = D2D1_ALPHA_MODE_PREMULTIPLIED;
    properties.dpiX = 96;
    properties.dpiY = 96;
    properties.bitmapOptions = D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW;
    properties.colorContext = nullptr;

    ComPtr<ID2D1Bitmap1> renderTarget;
    DX::ThrowIfFailed(
        m_d2dContext->CreateBitmapFromDxgiSurface(
            dxgiSurface.Get(),
            &properties,
            &renderTarget
            )
        );

    m_d2dContext->SetTarget(renderTarget.Get());
    float saveDpiX;
    float saveDpiY;

    m_d2dContext->GetDpi(&saveDpiX, &saveDpiY);
    m_d2dContext->SetDpi(96.0f, 96.0f);

    m_d2dContext->BeginDraw();
    m_d2dContext->SetTransform(D2D1::Matrix3x2F::Identity());

    D2D1_SIZE_F renderTargetSize = renderTarget->GetSize();

    m_d2dContext->Clear(D2D1::ColorF(D2D1::ColorF::Black));
    m_d2dContext->FillGeometry(m_circleGeometry5.Get(), m_yellowBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry4.Get(), m_greenBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry3.Get(), m_blueBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry2.Get(), m_redBrush.Get());
    m_d2dContext->FillGeometry(m_circleGeometry1.Get(), m_whiteBrush.Get());
    m_d2dContext->DrawText(
        name->Data(),
        name->Length(),
        m_textFormat.Get(),
        D2D1::RectF(0, 0, renderTargetSize.width, renderTargetSize.height),
        m_blackBrush.Get()
        );

    // We ignore D2DERR_RECREATE_TARGET here. This error indicates that the device
    // is lost. It will be handled during the next call to Present.
    HRESULT hr = m_d2dContext->EndDraw();
    if (hr != D2DERR_RECREATE_TARGET)
    {
        DX::ThrowIfFailed(hr);
    }

    m_d2dContext->SetTarget(nullptr);
    m_d2dContext->SetDpi(saveDpiX, saveDpiY);

    *textureResourceView = texture.Detach();
}
//----------------------------------------------------------------------
            
            
```

Material.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Material:
// This class maintains the properties that represent how an object will
// look when it is rendered.  This includes the color of the object, the
// texture used to render the object, and the vertex and pixel shader that
// should be used for rendering.
// The RenderSetup method sets the appropriate values into the constantBuffer
// and calls the appropriate D3D11 context methods to set up the rendering pipeline
// in the graphics hardware.

#include "ConstantBuffers.h"

ref class Material
{
internal:
    Material(
        DirectX::XMFLOAT4 meshColor,
        DirectX::XMFLOAT4 diffuseColor,
        DirectX::XMFLOAT4 specularColor,
        float specularExponent,
        _In_ ID3D11ShaderResourceView* textureResourceView,
        _In_ ID3D11VertexShader* vertexShader,
        _In_ ID3D11PixelShader* pixelShader
        );

    void RenderSetup(
        _In_ ID3D11DeviceContext* context,
        _Inout_ ConstantBufferChangesEveryPrim* constantBuffer
        );

    void SetTexture(_In_ ID3D11ShaderResourceView* textureResourceView)
    {
        m_textureRV = textureResourceView;
    }

protected private:
    DirectX::XMFLOAT4   m_meshColor;
    DirectX::XMFLOAT4   m_diffuseColor;
    DirectX::XMFLOAT4   m_hitColor;
    DirectX::XMFLOAT4   m_specularColor;
    float               m_specularExponent;

    Microsoft::WRL::ComPtr<ID3D11VertexShader>       m_vertexShader;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>        m_pixelShader;
    Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> m_textureRV;
};
            
            
```

Material.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Material.h"
#include "GameConstants.h"

using namespace DirectX;

//--------------------------------------------------------------------------------

Material::Material(
    XMFLOAT4 meshColor,
    XMFLOAT4 diffuseColor,
    XMFLOAT4 specularColor,
    float specularExponent,
    _In_ ID3D11ShaderResourceView* textureResourceView,
    _In_ ID3D11VertexShader* vertexShader,
    _In_ ID3D11PixelShader* pixelShader
    )
{
    m_meshColor = meshColor;
    m_diffuseColor = diffuseColor;
    m_specularColor = specularColor;
    m_specularExponent = specularExponent;

    m_vertexShader = vertexShader;
    m_pixelShader = pixelShader;
    m_textureRV = textureResourceView;
}

//--------------------------------------------------------------------------------

void Material::RenderSetup(
    _In_ ID3D11DeviceContext* context,
    _Inout_ ConstantBufferChangesEveryPrim* constantBuffer
    )
{
    constantBuffer->meshColor = m_meshColor;
    constantBuffer->specularColor = m_specularColor;
    constantBuffer->specularPower = m_specularExponent;
    constantBuffer->diffuseColor = m_diffuseColor;

    context->PSSetShaderResources(0, 1, m_textureRV.GetAddressOf());
    context->VSSetShader(m_vertexShader.Get(), nullptr, 0);
    context->PSSetShader(m_pixelShader.Get(), nullptr, 0);
}            
            
```

 

## Related topics


* [Create a simple UWP game with DirectX](tutorial--create-your-first-metro-style-directx-game.md)
