---
title: Set up
description: Learn how to assemble the rendering pipeline to display graphics. Game rendering, set up and prepare data.
ms.assetid: 7720ac98-9662-4cf3-89c5-7ff81896364a
ms.date: 10/24/2017
ms.topic: article
keywords: windows 10, uwp, games, rendering
ms.localizationpriority: medium
---
# Rendering framework II: Game rendering

In [Rendering framework I](tutorial--assembling-the-rendering-pipeline.md), we've covered how we take the scene info and present it to the display screen. Now, we'll take a step back and learn how to prepare the data for rendering.

>[!Note]
>If you haven't downloaded the latest game code for this sample, go to [Direct3D game sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Simple3DGameDX). This sample is part of a large collection of UWP feature samples. For instructions on how to download the sample, see [Get the UWP samples from GitHub](https://docs.microsoft.com/windows/uwp/get-started/get-uwp-app-samples).

## Objective

Quick recap on the objective. It is to understand how to set up a basic rendering framework to display the graphics output for a UWP DirectX game. We can loosely group them into these three steps.

 1. Establish a connection to our graphics interface
 2. Preparation: Create the resources we need to draw the graphics
 3. Display the graphics: Render the frame

[Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md) explained how graphics are rendered, covering Steps 1 and 3. 

This article explains how to set up other pieces of this framework and prepare the required data before rendering can happen, which is Step 2 of the process.

## Design the renderer

The renderer is responsible for creating and maintaining all the D3D11 and D2D objects used to generate the game visuals. The __GameRenderer__ class is the renderer for this sample game and is designed to meet the game's rendering needs.

These are some concepts you can use to help design the renderer for your game:
* Because Direct3D 11 APIs are defined as [COM](https://docs.microsoft.com/windows/desktop/com/the-component-object-model) APIs, you must provide [ComPtr](https://docs.microsoft.com/cpp/windows/comptr-class) references to the objects defined by these APIs. These objects are automatically freed when their last reference goes out of scope when the app terminates. For more information, see [ComPtr](https://github.com/Microsoft/DirectXTK/wiki/ComPtr). Example of these objects: constant buffers, shader objects - [vertex shader](tutorial--assembling-the-rendering-pipeline.md#vertex-shaders-and-pixel-shaders), [pixel shader](tutorial--assembling-the-rendering-pipeline.md#vertex-shaders-and-pixel-shaders), and shader resource objects.
* Constant buffers are defined in this class to hold various data needed for rendering.
    * Use multiple constant buffers with different frequencies to reduce the amount of data that must be sent to the GPU per frame. This sample separates constants into different buffers based on the frequency that they must be updated. This is a best practice for Direct3D programming. 
    * In this game sample, 4 constant buffers are defined.
        1. __m\_constantBufferNeverChanges__ contains the lighting parameters. It's set one time in the __FinalizeCreateGameDeviceResources__ method and never changes again.
        2. __m\_constantBufferChangeOnResize__ contains the projection matrix. The projection matrix is dependent on the size and aspect ratio of the window. It's set in [__CreateWindowSizeDependentResources__](#createwindowsizedependentresource-method) and then updated after resources are loaded in the [__FinalizeCreateGameDeviceResources__](#finalizecreategamedeviceresources-method) method. If rendering in 3D, it is also changed twice per frame.
        3. __m\_constantBufferChangesEveryFrame__ contains the view matrix. This matrix is dependent on the camera position and look direction (the normal to the projection) and changes one time per frame in the __Render__ method. This was discussed earlier in __Rendering framework I: Intro to rendering__, under the [__GameRenderer::Render__ method](tutorial--assembling-the-rendering-pipeline.md#gamerendererrender-method).
        4. __m\_constantBufferChangesEveryPrim__ contains the model matrix and material properties of each primitive. The model matrix transforms vertices from local coordinates into world coordinates. These constants are specific to each primitive and are updated for every draw call. This was discussed earlier in __Rendering framework I: Intro to rendering__, under the [Primitive rendering](tutorial--assembling-the-rendering-pipeline.md#primitive-rendering).
* Shader resource objects that hold textures for the primitives are also defined in this class.
    * Some textures are pre-defined ([DDS](https://docs.microsoft.com/windows/desktop/direct3ddds/dx-graphics-dds-pguide) is a file format that can be used to store compressed and uncompressed textures. DDS textures are used for the walls and floor of the world as well as the ammo spheres.)
    * In this game sample, shader resource objects are: __m\_sphereTexture__, __m\_cylinderTexture__, __m\_ceilingTexture__, __m\_floorTexture__, __m\_wallsTexture__.
* Shader objects are defined in this class to compute our primitives and textures. 
    * In this game sample, the shader objects are __m\_vertexShader__, __m\_vertexShaderFlat__, and __m\_pixelShader__, __m\_pixelShaderFlat__.
    * The vertex shader processes the primitives and the basic lighting, and the pixel shader (sometimes called a fragment shader) processes the textures and any per-pixel effects.
    * There are two versions of these shaders (regular and flat) for rendering different primitives. The reason we have different versions is that the flat versions are much simpler and don't do specular highlights or any per pixel lighting effects. These are used for the walls and make rendering faster on lower powered devices.

## GameRenderer.h

Now let's look at the code in the game sample's renderer class object and compare it with the __Sample3DSceneRenderer.h__ provided in the DirectX 11 App template.

```cpp
// Class object handling the rendering of the game
ref class GameRenderer
{
internal:
    GameRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    
    // Compared with Sample3DSceneRenderer.h in the DirectX 11 App template sample. 
    
    // These methods are present.
    void CreateDeviceDependentResources();
    void CreateWindowSizeDependentResources();
    void ReleaseDeviceDependentResources();
    void Render();

    // Added: Async related methods to prepare 3D game objects for rendering
    concurrency::task<void> CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game);
    void FinalizeCreateGameDeviceResources();
    concurrency::task<void> LoadLevelResourcesAsync();
    void FinalizeLoadLevelResources();
    // --- end of async related methods section

    // Added: Methods for rendering overlay
    Simple3DGameDX::IGameUIControl^ GameUIControl()  { return m_gameInfoOverlay; };

    DirectX::XMFLOAT2 GameInfoOverlayUpperLeft()
    {
        return DirectX::XMFLOAT2(m_gameInfoOverlayRect.left, m_gameInfoOverlayRect.top);
    };
    DirectX::XMFLOAT2 GameInfoOverlayLowerRight()
    {
        return DirectX::XMFLOAT2(m_gameInfoOverlayRect.right, m_gameInfoOverlayRect.bottom);
    };
    bool GameInfoOverlayVisible() { return m_gameInfoOverlay->Visible(); }
    // --- end of rendering overlay section

    //...
    protected private:
    // Cached pointer to device resources.
    std::shared_ptr<DX::DeviceResources>                m_deviceResources;

    // ...

    // Shader resource objects
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

    // Texture sampler
    Microsoft::WRL::ComPtr<ID3D11SamplerState>          m_samplerLinear;

    // Shader objects: Vertex shaders and pixel shaders
    Microsoft::WRL::ComPtr<ID3D11VertexShader>          m_vertexShader;
    Microsoft::WRL::ComPtr<ID3D11VertexShader>          m_vertexShaderFlat;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>           m_pixelShader;
    Microsoft::WRL::ComPtr<ID3D11PixelShader>           m_pixelShaderFlat;
    Microsoft::WRL::ComPtr<ID3D11InputLayout>           m_vertexLayout;
};
```

## Constructor

Next, let's examine the game sample's __GameRenderer__ constructor and compare it with the __Sample3DSceneRenderer__ constructor provided in the DirectX 11 App template.

```cpp
// Constructor method of the main rendering class object
GameRenderer::GameRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources) : //...
{
    // Compared with Sample3DSceneRenderer::Sample3DSceneRenderer in the DirectX 11 App template sample. 
    
    // Added: Create a new GameHud object to rendered text on the top left corner of the screen
    m_gameHud = ref new GameHud(
        deviceResources,
        "Windows platform samples",
        "DirectX first-person game sample"
        );
    //--- end of new GameHud object section
        
    // Added: Game info rendered as an overlay on the top right corner of the screen (eg. Hits, Shots, Time)    
    m_gameInfoOverlay = ref new GameInfoOverlay(deviceResources);
    //--- end of game info rendered as overlay section

    // These methods are present.
    CreateDeviceDependentResources();
    CreateWindowSizeDependentResources();
}
```

## Create and load DirectX graphic resources

In the game sample (and in Visual Studio's __DirectX 11 App (Universal Windows)__ template), creating and loading game resources is implemented using these two methods that are called from __GameRenderer__ constructor:

* [__CreateDeviceDependentResources__](#createdevicedependentresources-method)
* [__CreateWindowSizeDependentResources__](#createwindowsizedependentresource-method)

## CreateDeviceDependentResources method

In the DirectX 11 App template, this method is used to load vertex and pixel shader asynchronously, create the shader and constant buffer, create a mesh with vertices that contain position and color info. 

In the sample game, these operations of the scene objects are instead split among the [__CreateGameDeviceResourcesAsync__](#creategamedeviceresourcesasync-method) and [__FinalizeCreateGameDeviceResources__](#finalizecreategamedeviceresources-method) methods. 

For this game sample, what goes into this method?

* Instantiated variables (__m\_gameResourcesLoaded__ = false and __m\_levelResourcesLoaded__ = false) that indicate whether resources have been loaded before moving forward to render, since we're loading them asynchronously. 
* Since HUD and overlay rendering are in separate class objects, call __GameHud::CreateDeviceDependentResources__ and __GameInfoOverlay::CreateDeviceDependentResources__ methods here.

Here's the code for __GameRenderer::CreateDeviceDependentResources__.

```cpp
// This method is called in GameRenderer constructor when it's created in GameMain constructor.
void GameRenderer::CreateDeviceDependentResources()
{
    // instantiate variables that indicate if resources were loaded.
    m_gameResourcesLoaded = false;
    m_levelResourcesLoaded = false;

    // game HUD and overlay are design as separate class objects.
    m_gameHud->CreateDeviceDependentResources();
    m_gameInfoOverlay->CreateDeviceDependentResources();
}
```
The table below lists the methods that are used to create and load resources. __CreateGameDeviceResourcesAsync__ and __FinalizeCreateGameDeviceResources__ are added in the sample game so that resources are loaded asynchronously.

|Original DirectX 11 App template           |Sample game                                                                |
|-------------------------------------------|---------------------------------------------------------------------------|
|CreateDeviceDependentResources             |CreateDeviceDependentResources                                             |
|                                           | - CreateGameDeviceResourcesAsync (Added)                                  |
|                                           | - FinalizeCreateGameDeviceResources (Added)                               |
|CreateWindowSizeDependentResources         |CreateWindowSizeDependentResources                                         |

Before diving into the other methods that are used to create and load resources, let's first create the renderer and see how it fits into the game loop.

## Create the renderer

The __GameRenderer__ is created in the __GameMain__'s constructor. It also calls the two other methods, [__CreateGameDeviceResourcesAsync__](#creategamedeviceresourcesasync-method) and [__FinalizeCreateGameDeviceResources__](#finalizecreategamedeviceresources-method) that are added to help create and load resources.

```cpp

GameMain::GameMain(const std::shared_ptr<DX::DeviceResources>& deviceResources) : // ...
{
    m_deviceResources->RegisterDeviceNotify(this);

    // These methods are used in the DirectX 11 App template to create the class objects used for rendering. 
    // But are replaced in this game sample with GameRenderer as shown below.
	// m_sceneRenderer = std::unique_ptr<Sample3DSceneRenderer>(new Sample3DSceneRenderer(m_deviceResources));
    // m_fpsTextRenderer = std::unique_ptr<SampleFpsTextRenderer>(new SampleFpsTextRenderer(m_deviceResources));
    
    // Creation of GameRenderer
    m_renderer = ref new GameRenderer(m_deviceResources);
    
    //...

     create_task([this]()
    {
        // Asynchronously initialize the game class and load the renderer device resources.
        // By doing all this asynchronously, the game gets to its main loop more quickly
        // and in parallel all the necessary resources are loaded on other threads.
        m_game->Initialize(m_controller, m_renderer);

        return m_renderer->CreateGameDeviceResourcesAsync(m_game);

    }).then([this]()
    {
        // The finalize code needs to run in the same thread context
        // as the m_renderer object was created because the D3D device context
        // can ONLY be accessed on a single thread.
        m_renderer->FinalizeCreateGameDeviceResources();

        InitializeGameState();
    
    //...
}
```

## CreateGameDeviceResourcesAsync method

__CreateGameDeviceResourcesAsync__ is called from the __GameMain__ constructor method in the __create\_task__ loop since we're loading game resources asynchronously.
        
__CreateDeviceResourcesAsync__ is a method that runs as a separate set of async tasks to load the game resources. Because it's expected to run on a separate thread, it only has access to the Direct3D 11 device methods (those defined on __ID3D11Device__) and not the device context methods (the methods defined on __ID3D11DeviceContext__), so it does not perform any rendering.

__FinalizeCreateGameDeviceResources__ method runs on the main thread and does have access to the Direct3D 11 device context methods.

In principle:
* Use only __ID3D11Device__ methods in __CreateGameDeviceResourcesAsync__ because they are free-threaded, which means that they are able to run on any thread. It is also expected that they do not run on the same thread as the one __GameRenderer__ was created on. 
* Do not use methods in __ID3D11DeviceContext__ here because they need to run on a single thread and on the same thread as __GameRenderer__.
* Use this method to create constant buffers.
* Use this method to load textures (like the .dds files) and shader info (like the .cso files) into the [shaders](tutorial--assembling-the-rendering-pipeline.md#shaders).

This method is used to:
* Create the 4 [constant buffers](tutorial--assembling-the-rendering-pipeline.md#buffer): __m\_constantBufferNeverChanges__, __m\_constantBufferChangeOnResize__, __m\_constantBufferChangesEveryFrame__, __m\_constantBufferChangesEveryPrim__
* Create a [sampler-state](tutorial--assembling-the-rendering-pipeline.md#sampler-state) object that encapsulates sampling information for a texture
* Create a task group that contains all async tasks created by the method. It waits for the completion of all these async tasks, and then calls __FinalizeCreateGameDeviceResources__.
* Create a loader using [Basic Loader](tutorial--assembling-the-rendering-pipeline.md#basicloader). Add the loader's async loading operations as tasks into the task group created earlier.
* Methods like __BasicLoader::LoadShaderAsync__ and  __BasicLoader::LoadTextureAsync__ are used to load:
    * compiled shader objects (VertextShader.cso, VertexShaderFlat.cso, PixelShader.cso, and PixelShaderFlat.cso). For more info, go to [Various shader file formats](tutorial--assembling-the-rendering-pipeline.md#various-shader-file-formats).
    * game specific textures (Assets\\seafloor.dds, metal_texture.dds, cellceiling.dds, cellfloor.dds, cellwall.dds).

```cpp
task<void> GameRenderer::CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game)
{
    // Create the device dependent game resources.
    // Only the d3dDevice is used in this method.  It is expected
    // to not run on the same thread as the GameRenderer was created.
    // Create methods on the d3dDevice are free-threaded and are safe while any methods
    // in the d3dContext should only be used on a single thread and handled
    // in the FinalizeCreateGameDeviceResources method.
    m_game = game;

    auto d3dDevice = m_deviceResources->GetD3DDevice();

    // Define D3D11_BUFFER_DESC.
    // For API ref, go to: https://msdn.microsoft.com/library/windows/desktop/ff476092.aspx
    D3D11_BUFFER_DESC bd;
    ZeroMemory(&bd, sizeof(bd));
    
    bd.Usage = D3D11_USAGE_DEFAULT;
    // ...
    
    // Create the constant buffers: m_constantBufferNeverChanges, m_constantBufferChangeOnResize,
    // m_constantBufferChangesEveryFrame, m_constantBufferChangesEveryPrim
    // CreateBuffer is used to create one of these buffers: vertex buffer, index buffer, or 
    // shader-constant buffer. For CreateBuffer API ref info, go to: https://msdn.microsoft.com/library/windows/desktop/ff476501.aspx
    
    DX::ThrowIfFailed(
        d3dDevice->CreateBuffer(&bd, nullptr, &m_constantBufferNeverChanges) 
        );
    // ...
    
    // Define D3D11_SAMPLER_DESC. For API ref, go to: https://msdn.microsoft.com/library/windows/desktop/ff476207.aspx
    D3D11_SAMPLER_DESC sampDesc;

    // ZeroMemory fills a block of memory with zeros. 
    // For API ref, go to: https://msdn.microsoft.com/library/windows/desktop/aa366920(v=vs.85).aspx
    ZeroMemory(&sampDesc, sizeof(sampDesc));

    sampDesc.Filter = D3D11_FILTER_MIN_MAG_MIP_LINEAR;
    sampDesc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
    sampDesc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
    // ...
    
    // Create a sampler-state object that encapsulates sampling information for a texture.
    // The sampler-state interface holds a description for sampler state that you can bind to any 
    // shader stage of the pipeline for reference by texture sample operations.
    DX::ThrowIfFailed(
        d3dDevice->CreateSamplerState(&sampDesc, &m_samplerLinear)
        );

    // Start the async tasks to load the shaders and textures (resources).
    
    // Load compiled shader objects (VertextShader.cso, VertexShaderFlat.cso, PixelShader.cso, and PixelShaderFlat.cso).
    // The BasicLoader class is used to convert and load common graphics resources, such as meshes, textures, 
    // and various shader objects into the constant buffers. 
    // For more info, go to: https://docs.microsoft.com/windows/uwp/gaming/complete-code-for-basicloader
    BasicLoader^ loader = ref new BasicLoader(d3dDevice);

    std::vector<task<void>> tasks;

    uint32 numElements = ARRAYSIZE(PNTVertexLayout);

    // Load shaders asynchronously with the shader and pixel data using the BasicLoader::LoadShaderAsync method
    // Push these method calls into a list of tasks
    tasks.push_back(loader->LoadShaderAsync("VertexShader.cso", PNTVertexLayout, numElements, &m_vertexShader, &m_vertexLayout));
    tasks.push_back(loader->LoadShaderAsync("VertexShaderFlat.cso", nullptr, numElements, &m_vertexShaderFlat, nullptr));
    tasks.push_back(loader->LoadShaderAsync("PixelShader.cso", &m_pixelShader));
    tasks.push_back(loader->LoadShaderAsync("PixelShaderFlat.cso", &m_pixelShaderFlat));

    // Make sure the previous versions are set to NULL before any of the textures are loaded.
    m_sphereTexture = nullptr;
    // ...

    // Load Game specific textures (Assets\\seafloor.dds, metal_texture.dds, cellceiling.dds, cellfloor.dds, cellwall.dds).
    // Push these method calls also into a list of tasks
    tasks.push_back(loader->LoadTextureAsync("Assets\\seafloor.dds", nullptr, &m_sphereTexture));
    // ...
    
    tasks.push_back(create_task([]()
    {
        // Simulate loading additional resources by introducing a delay.
        wait(GameConstants::InitialLoadingDelay);
    }));

    // Returns when all the async tasks for loading the shader and texture assets have completed.
    return when_all(tasks.begin(), tasks.end());
}
```

## FinalizeCreateGameDeviceResources method

__FinalizeCreateGameDeviceResources__ method is called after all the load resources tasks that are in the __CreateGameDeviceResourcesAsync__ method completes. 

* Initialize constantBufferNeverChanges with the light positions and color. Loads the initial data into the constant buffers with a device context method call to __ID3D11DeviceContext::UpdateSubresource__.
* Since asynchronously loaded resources have completed loading, it's time to associate them with the appropriate game objects.
* For each game object, create the mesh and the material using the textures that have been loaded. Then associate the mesh and material to the game object.
* For the targets game object, the texture which is composed of concentric colored rings, with a numeric value on the top, is not loaded from a texture file. Instead, it's procedurally generated using the code in __TargetTexture.cpp__. The __TargetTexture__ class creates the necessary resources to draw the texture into an off screen resource at initialization time. The resulting texture is then associated with the appropriate target game objects.

__FinalizeCreateGameDeviceResources__ and [__CreateWindowSizeDependentResources__](#createwindowsizedependentresource-method) share similar portions of code for these:
* Use __SetProjParams__ to ensure that the camera has the right projection matrix. For more info, go to [Camera and coordinate space](tutorial--assembling-the-rendering-pipeline.md#camera-and-coordinate-space).
* Handle screen rotation by post multiplying the 3D rotation matrix to the camera's projection matrix. Then update the __ConstantBufferChangeOnResize__ constant buffer with the resulting projection matrix.
* Set the __m\_gameResourcesLoaded__ __Boolean__ global variable to indicate that the resources are now loaded in the buffers, ready for the next step. Recall that we first initialized this variable as __FALSE__ in the __GameRenderer__'s constructor method, through the __GameRenderer::CreateDeviceDependentResources__ method. 
* When this __m\_gameResourcesLoaded__ is __TRUE__, rendering of the scene objects can take place. This was covered in the __Rendering framework I: Intro to rendering__ article, under [__GameRenderer::Render method__](tutorial--assembling-the-rendering-pipeline.md#gamerendererrender-method).

```cpp
// When creating this sample game using the DirectX 11 App template, this method needs to be created.
// This new method is called from GameMain constructor in the .then loop.
// Make sure the 2D rendering is occurring on the same thread as the main rendering.
// Note: Helper class .h and .cpp files used in this method are located in the SharedContent/cpp/GameContent folder
void GameRenderer::FinalizeCreateGameDeviceResources()
{
    // All asynchronously loaded resources have completed loading.
    // Now associate all the resources with the appropriate game objects.
    // This method is expected to run in the same thread as the GameRenderer
    // was created. All work will happen behind the "Loading ..." screen after the
    // main loop has been entered.

    // Initialize constantBufferNeverChanges with the light positions and color.
    // These are handled here to ensure that the d3dContext is only
    // used in one thread.

    auto d3dDevice = m_deviceResources->GetD3DDevice();
    ConstantBufferNeverChanges constantBufferNeverChanges;
    constantBufferNeverChanges.lightPosition[0] = XMFLOAT4( 3.5f, 2.5f,  5.5f, 1.0f);
    // ...
    constantBufferNeverChanges.lightColor = XMFLOAT4(0.25f, 0.25f, 0.25f, 1.0f);

    // CPU copies data from memory (constantBufferNeverChanges) to a subresource 
    // created in non-mappable memory (m_constantBufferNeverChanges) which was created in the earlier 
    // CreateGameDeviceResourcesAsync method. For UpdateSubresource API ref info, 
    // go to: https://msdn.microsoft.com/library/windows/desktop/ff476486.aspx
    // To learn more about what a subresource is, go to:
    // https://msdn.microsoft.com/library/windows/desktop/ff476901.aspx

    m_deviceResources->GetD3DDeviceContext()->UpdateSubresource(
        m_constantBufferNeverChanges.Get(),
        0,
        nullptr,
        &constantBufferNeverChanges,
        0,
        0
        );

    // For the objects that function as targets, they have two unique generated textures.
    // One version is used to show that they have never been hit and the other is 
    // used to show that they have been hit.
    // TargetTexture is a helper class to procedurally generate textures for game
    // targets. The class creates the necessary resources to draw the texture into 
    // an off screen resource at initialization time.

    TargetTexture^ textureGenerator = ref new TargetTexture(
        d3dDevice,
        m_deviceResources->GetD2DFactory(),
        m_deviceResources->GetDWriteFactory(),
        m_deviceResources->GetD2DDeviceContext()
        );

    // CylinderMesh is a class derived from MeshObject and creates a ID3D11Buffer of
    // vertices and indices to represent a canonical cylinder (capped at
    // both ends) that is positioned at the origin with a radius of 1.0,
    // a height of 1.0 and with its axis in the +Z direction.
    // In the game sample, there are various types of mesh types:
    // CylinderMesh (vertical rods), SphereMesh (balls that the player shoots), 
    // FaceMesh (target objects), and WorldMesh (Floors and ceilings that define the enclosed area)

    MeshObject^ cylinderMesh = ref new CylinderMesh(d3dDevice, 26);
    // ...

    // The Material class maintains the properties that represent how an object will
    // look when it is rendered.  This includes the color of the object, the
    // texture used to render the object, and the vertex and pixel shader that
    // should be used for rendering.

    Material^ cylinderMaterial = ref new Material(
        XMFLOAT4(0.8f, 0.8f, 0.8f, .5f),
        XMFLOAT4(0.8f, 0.8f, 0.8f, .5f),
        XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f),
        15.0f,
        m_cylinderTexture.Get(),
        m_vertexShader.Get(),
        m_pixelShader.Get()
        );

    // ...
    auto objects = m_game->RenderObjects();

    // Attach the textures to the appropriate game objects.
    // We'll loop through all the objects that need to be rendered.
    for (auto object = objects.begin(); object != objects.end(); object++)
    {

        if ((*object)->TargetId() == GameConstants::WorldFloorId)
        {
            // Assign a normal material for the floor object.
            // This normal material uses the floor texture (cellfloor.dds) that was loaded asynchronously from
            // the Assets folder using BasicLoader::LoadTextureAsync method in the earlier 
            // CreateGameDeviceResourcesAsync loop

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
            // Creates a mesh object called WorldFloorMesh and assign it to the floor object.
            (*object)->Mesh(ref new WorldFloorMesh(d3dDevice));
        }
        // ...
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
        // ...
    }


    // The SetProjParams method calculates the projection matrix based on input params and
    // ensures that the camera has been initialized with the right projection
    // matrix.  
    // The camera is not created at the time the first window resize event occurs.

    auto renderTargetSize = m_deviceResources->GetRenderTargetSize();
    m_game->GameCamera()->SetProjParams(
        XM_PI / 2,
        renderTargetSize.Width / renderTargetSize.Height,
        0.01f,
        100.0f
        );

    // Make sure that the correct projection matrix is set in the ConstantBufferChangeOnResize buffer.

    // Get the 3D rotation transform matrix. We are handling screen rotations directly to eliminate an unaligned 
    // fullscreen copy. So it is necessary to post multiply the 3D rotation matrix to the camera's projection matrix
    // to get the projection matrix that we need.

    auto orientation = m_deviceResources->GetOrientationTransform3D();

    ConstantBufferChangeOnResize changesOnResize;

    // The matrices are transposed due to the shader code expecting the matrices in the opposite
    // row/column order from the DirectX math library.

    // XMStoreFloat4x4 takes a matrix and writes the components out to sixteen single-precision floating-point values at the given address. 
    // The most significant component of the first row vector is written to the first four bytes of the address, 
    // followed by the second most significant component of the first row, and so on. The second row is then written out in a 
    // like manner to memory beginning at byte 16, followed by the third row to memory beginning at byte 32, and finally 
    // the fourth row to memory beginning at byte 48. For more API ref info, go to: 
    // https://msdn.microsoft.com/library/windows/desktop/microsoft.directx_sdk.storing.xmstorefloat4x4.aspx

    XMStoreFloat4x4(
        &changesOnResize.projection,
        XMMatrixMultiply(
            XMMatrixTranspose(m_game->GameCamera()->Projection()),
            XMMatrixTranspose(XMLoadFloat4x4(&orientation))
            )
        );

    // UpdateSubresource method instructs CPU to copy data from memory (changesOnResize) to a subresource 
    // created in non-mappable memory (m_constantBufferChangeOnResize ) which was created in the earlier 
    // CreateGameDeviceResourcesAsync method.

    m_deviceResources->GetD3DDeviceContext()->UpdateSubresource(
        m_constantBufferChangeOnResize.Get(),
        0,
        nullptr,
        &changesOnResize,
        0,
        0
        );

    // Finally we set the m_gameResourcesLoaded as TRUE, so we can start rendering.
    m_gameResourcesLoaded = true;
}
```

## CreateWindowSizeDependentResource method

CreateWindowSizeDependentResources methods are called every time the window size, orientation, stereo-enabled rendering, or resolution changes. In the sample game, it updates the projection matrix in __ConstantBufferChangeOnResize__.

Window size resources are updated in this manner: 
* The App framework gets one of several possible events indicating a change in the window state. 
* Your main game loop is then informed about the event and calls __CreateWindowSizeDependentResources__ on the main class (__GameMain__) instance, which then calls the __CreateWindowSizeDependentResources__ implementation in the game renderer (__GameRenderer__) class.
* The primary job of this method is to make sure the visuals don't become confused or invalid because of a change in window properties.

For this game sample, a number of method calls are the same as the [__FinalizeCreateGameDeviceResources__](#finalizecreategamedeviceresources-method) method. For code walkthrough, go to the previous section.

The game HUD and overlay window size rendering adjustments is covered under [Add a user interface](tutorial--adding-a-user-interface.md).

```cpp
// Initializes view parameters when the window size changes.
void GameRenderer::CreateWindowSizeDependentResources()
{

    // Game HUD and overlay window size rendering adjustments are done here
    // but they'll be covered in the UI section instead.

    m_gameHud->CreateWindowSizeDependentResources();

    // ...

    auto d3dContext = m_deviceResources->GetD3DDeviceContext();
    // In Sample3DSceneRenderer::CreateWindowSizeDependentResources, we had:
    // Size outputSize = m_deviceResources->GetOutputSize();

    auto renderTargetSize = m_deviceResources->GetRenderTargetSize();

    // ...

    m_gameInfoOverlay->CreateWindowSizeDependentResources(m_gameInfoOverlaySize);

    if (m_game != nullptr)
    {
        // Similar operations as the last section of FinalizeCreateGameDeviceResources method
        m_game->GameCamera()->SetProjParams(
            XM_PI / 2, renderTargetSize.Width / renderTargetSize.Height,
            0.01f,
            100.0f
            );

        XMFLOAT4X4 orientation = m_deviceResources->GetOrientationTransform3D();

        ConstantBufferChangeOnResize changesOnResize;
        XMStoreFloat4x4(
            &changesOnResize.projection,
            XMMatrixMultiply(
                XMMatrixTranspose(m_game->GameCamera()->Projection()),
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
}
```

## Next steps

This is the basic process for implementing the graphics rendering framework of a game. The larger your game, the more abstractions you would have to put in place to handle hierarchies of object types and animation behaviors. You need to implement more complex methods for loading and managing assets such as meshes and textures. Next, let's learn how to [add a user interface](tutorial--adding-a-user-interface.md).