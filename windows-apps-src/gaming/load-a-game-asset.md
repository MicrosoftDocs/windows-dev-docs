---
title: Load resources in your DirectX game
description: Most games, at some point, load resources and assets (such as shaders, textures, predefined meshes or other graphics data) from local storage or some other data stream.
ms.assetid: e45186fa-57a3-dc70-2b59-408bff0c0b41
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, loading resources
ms.localizationpriority: medium
---
# Load resources in your DirectX game



Most games, at some point, load resources and assets (such as shaders, textures, predefined meshes or other graphics data) from local storage or some other data stream. Here, we walk you through a high-level view of what you must consider when loading these files to use in your DirectX C/C++ Universal Windows Platform (UWP) game.

For example, the meshes for polygonal objects in your game might have been created with another tool, and exported to a specific format. The same is true for textures, and more so: while a flat, uncompressed bitmap can be commonly written by most tools and understood by most graphics APIs, it can be extremely inefficient for use in your game. Here, we guide you through the basic steps for loading three different types of graphic resources for use with Direct3D: meshes (models), textures (bitmaps), and compiled shader objects.

## What you need to know


### Technologies

-   Parallel Patterns Library (ppltasks.h)

### Prerequisites

-   Understand the basic Windows Runtime
-   Understand asynchronous tasks
-   Understand the basic concepts of 3-D graphics programming.

This sample also includes three code files for resource loading and management. You'll encounter the code objects defined in these files throughout this topic.

-   BasicLoader.h/.cpp
-   BasicReaderWriter.h/.cpp
-   DDSTextureLoader.h/.cpp

The complete code for these samples can be found in the following links.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="complete-code-for-basicloader.md">Complete code for BasicLoader</a></p></td>
<td align="left"><p>Complete code for a class and methods that convert and load graphics mesh objects into memory.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="complete-code-for-basicreaderwriter.md">Complete code for BasicReaderWriter</a></p></td>
<td align="left"><p>Complete code for a class and methods for reading and writing binary data files in general. Used by the <a href="complete-code-for-basicloader.md">BasicLoader</a> class.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="complete-code-for-ddstextureloader.md">Complete code for DDSTextureLoader</a></p></td>
<td align="left"><p>Complete code for a class and method that loads a DDS texture from memory.</p></td>
</tr>
</tbody>
</table>

 

## Instructions

### Asynchronous loading

Asynchronous loading is handled using the **task** template from the Parallel Patterns Library (PPL). A **task** contains a method call followed by a lambda that processes the results of the async call after it completes, and usually follows the format of:

`task<generic return type>(async code to execute).then((parameters for lambda){ lambda code contents });`.

Tasks can be chained together using the **.then()** syntax, so that when one operation completes, another async operation that depends on the results of the prior operation can be run. In this way, you can load, convert, and manage complex assets on separate threads in a way that appears almost invisible to the player.

For more details, read [Asynchronous programming in C++](../threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps.md).

Now, let's look at the basic structure for declaring and creating an async file loading method, **ReadDataAsync**.

```cpp
#include <ppltasks.h>

// ...
concurrency::task<Platform::Array<byte>^> ReadDataAsync(
        _In_ Platform::String^ filename);

// ...

using concurrency;

task<Platform::Array<byte>^> BasicReaderWriter::ReadDataAsync(
    _In_ Platform::String^ filename
    )
{
    return task<StorageFile^>(m_location->GetFileAsync(filename)).then([=](StorageFile^ file)
    {
        return FileIO::ReadBufferAsync(file);
    }).then([=](IBuffer^ buffer)
    {
        auto fileData = ref new Platform::Array<byte>(buffer->Length);
        DataReader::FromBuffer(buffer)->ReadBytes(fileData);
        return fileData;
    });
}
```

In this code, when your code calls the **ReadDataAsync** method defined above, a task is created to read a buffer from the file system. Once it completes, a chained task takes the buffer and streams the bytes from that buffer into an array using the static [**DataReader**](/uwp/api/Windows.Storage.Streams.DataReader) type.

```cpp
m_basicReaderWriter = ref new BasicReaderWriter();

// ...
return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
      // Perform some operation with the data when the async load completes.          
    });
```

Here's the call you make to **ReadDataAsync**. When it completes, your code receives an array of bytes read from the provided file. Since **ReadDataAsync** itself is defined as a task, you can use a lambda to perform a specific operation when the byte array is returned, such as passing that byte data to a DirectX function that can use it.

If your game is sufficiently simple, load your resources with a method like this when the user starts the game. You can do this before you start the main game loop from some point in the call sequence of your [**IFrameworkView::Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) implementation. Again, you call your resource loading methods asynchronously so the game can start quicker and so the player doesn't have to wait until the loading completes before engaging in early interactions.

However, you don't want to start the game proper until all of the async loading has completed! Create some method for signaling when loading is complete, such as a specific field, and use the lambdas on your loading method(s) to set that signal when finished. Check the variable before starting any components that use those loaded resources.

Here's an example using the async methods defined in BasicLoader.cpp to load shaders, a mesh, and a texture when the game starts up. Notice that it sets a specific field on the game object, **m\_loadingComplete**, when all of the loading methods finish.

```cpp
void ResourceLoading::CreateDeviceResources()
{
    // DirectXBase is a common sample class that implements a basic view provider. 
    
    DirectXBase::CreateDeviceResources(); 

    // ...

    // This flag will keep track of whether or not all application
    // resources have been loaded.  Until all resources are loaded,
    // only the sample overlay will be drawn on the screen.
    m_loadingComplete = false;

    // Create a BasicLoader, and use it to asynchronously load all
    // application resources.  When an output value becomes non-null,
    // this indicates that the asynchronous operation has completed.
    BasicLoader^ loader = ref new BasicLoader(m_d3dDevice.Get());

    auto loadVertexShaderTask = loader->LoadShaderAsync(
        "SimpleVertexShader.cso",
        nullptr,
        0,
        &m_vertexShader,
        &m_inputLayout
        );

    auto loadPixelShaderTask = loader->LoadShaderAsync(
        "SimplePixelShader.cso",
        &m_pixelShader
        );

    auto loadTextureTask = loader->LoadTextureAsync(
        "reftexture.dds",
        nullptr,
        &m_textureSRV
        );

    auto loadMeshTask = loader->LoadMeshAsync(
        "refmesh.vbo",
        &m_vertexBuffer,
        &m_indexBuffer,
        nullptr,
        &m_indexCount
        );

    // The && operator can be used to create a single task that represents
    // a group of multiple tasks. The new task's completed handler will only
    // be called once all associated tasks have completed. In this case, the
    // new task represents a task to load various assets from the package.
    (loadVertexShaderTask && loadPixelShaderTask && loadTextureTask && loadMeshTask).then([=]()
    {
        m_loadingComplete = true;
    });

    // Create constant buffers and other graphics device-specific resources here.
}
```

Note that the tasks have been aggregated using the && operator such that the lambda that sets the loading complete flag is triggered only when all of the tasks complete. Note that if you have multiple flags, you have the possibility of race conditions. For example, if the lambda sets two flags sequentially to the same value, another thread may only see the first flag set if it examines them before the second flag is set.

You've seen how to load resource files asynchronously. Synchronous file loads are much simpler, and you can find examples of them in [Complete code for BasicReaderWriter](complete-code-for-basicreaderwriter.md) and [Complete code for BasicLoader](complete-code-for-basicloader.md).

Of course, different resource and asset types often require additional processing or conversion before they are ready to be used in your graphics pipeline. Let's take a look at three specific types of resources: meshes, textures, and shaders.

### Loading meshes

Meshes are vertex data, either generated procedurally by code within your game or exported to a file from another app (like 3DStudio MAX or Alias WaveFront) or tool. These meshes represent the models in your game, from simple primitives like cubes and spheres to cars and houses and characters. They often contain color and animation data, as well, depending on their format. We'll focus on meshes that contain only vertex data.

To load a mesh correctly, you must know the format of the data in the file for the mesh. Our simple **BasicReaderWriter** type above simply reads the data in as a byte stream; it doesn't know that the byte data represents a mesh, much less a specific mesh format as exported by another application! You must perform the conversion as you bring the mesh data into memory.

(You should always try to package asset data in a format that's as close to the internal representation as possible. Doing so will reduce resource utilization and save time.)

Let's get the byte data from the mesh's file. The format in the example assumes that the file is a sample-specific format suffixed with .vbo. (Again, this format is not the same as OpenGL's VBO format.) Each vertex itself maps to the **BasicVertex** type, which is a struct defined in the code for the obj2vbo converter tool. The layout of the vertex data in the .vbo file looks like this:

-   The first 32 bits (4 bytes) of the data stream contain the number of vertices (numVertices) in the mesh, represented as a uint32 value.
-   The next 32 bits (4 bytes) of the data stream contain the number of indices in the mesh (numIndices), represented as a uint32 value.
-   After that, the subsequent (numVertices \* sizeof(**BasicVertex**)) bits contain the vertex data.
-   The last (numIndices \* 16) bits of data contain the index data, represented as a sequence of uint16 values.

The point is this: know the bit-level layout of the mesh data you have loaded. Also, be sure you are consistent with endian-ness. All Windows 8 platforms are little-endian.

In the example, you call a method, CreateMesh, from the **LoadMeshAsync** method to perform this bit-level interpretation.

```cpp
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

**CreateMesh** interprets the byte data loaded from the file, and creates a vertex buffer and an index buffer for the mesh by passing the vertex and index lists, respectively, to [**ID3D11Device::CreateBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createbuffer) and specifying either D3D11\_BIND\_VERTEX\_BUFFER or D3D11\_BIND\_INDEX\_BUFFER. Here's the code used in **BasicLoader**:

```cpp
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

    m_d3dDevice->CreateBuffer(
            &vertexBufferDesc,
            &vertexBufferData,
            vertexBuffer
            );
    
    D3D11_SUBRESOURCE_DATA indexBufferData = {0};
    indexBufferData.pSysMem = indices;
    indexBufferData.SysMemPitch = 0;
    indexBufferData.SysMemSlicePitch = 0;
    CD3D11_BUFFER_DESC indexBufferDesc(numIndices * sizeof(uint16), D3D11_BIND_INDEX_BUFFER);
    
    m_d3dDevice->CreateBuffer(
            &indexBufferDesc,
            &indexBufferData,
            indexBuffer
            );
  
    if (vertexCount != nullptr)
    {
        *vertexCount = numVertices;
    }
    if (indexCount != nullptr)
    {
        *indexCount = numIndices;
    }
}
```

You typically create a vertex/index buffer pair for every mesh you use in your game. Where and when you load the meshes is up to you. If you have a lot of meshes, you may only want to load some from the disk at specific points in the game, such as during specific, pre-defined loading states. For large meshes, like terrain data, you can stream the vertices from a cache, but that is a more complex procedure and not in the scope of this topic.

Again, know your vertex data format! There are many, many ways to represent vertex data across the tools used to create models. There are also many different ways to represent the input layout of the vertex data to Direct3D, such as triangle lists and strips. For more information about vertex data, read [Introduction to Buffers in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-intro) and [Primitives](/windows/desktop/direct3d9/primitives).

Next, let's look at loading textures.

### Loading textures

The most common asset in a game—and the one that comprises most of the files on disk and in memory—are textures. Like meshes, textures can come in a variety of formats, and you convert them to a format that Direct3D can use when you load them. Textures also come in a wide variety of types and are used to create different effects. MIP levels for textures can be used to improve the look and performance of distance objects; dirt and light maps are used to layer effects and detail atop a base texture; and normal maps are used in per-pixel lighting calculations. In a modern game, a typical scene can potentially have thousands of individual textures, and your code must effectively manage them all!

Also like meshes, there are a number of specific formats that are used to make memory usage for efficient. Since textures can easily consume a large portion of the GPU (and system) memory, they are often compressed in some fashion. You aren't required to use compression on your game's textures, and you can use any compression/decompression algorithm(s) you want as long as you provide the Direct3D shaders with data in a format it can understand (like a [**Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d) bitmap).

Direct3D provides support for the DXT texture compression algorithms, although every DXT format may not be supported in the player's graphics hardware. DDS files contain DXT textures (and other texture compression formats as well), and are suffixed with .dds.

A DDS file is a binary file that contains the following information:

-   A DWORD (magic number) containing the four character code value 'DDS ' (0x20534444).

-   A description of the data in the file.

    The data is described with a header description using [**DDS\_HEADER**](/windows/desktop/direct3ddds/dds-header); the pixel format is defined using [**DDS\_PIXELFORMAT**](/windows/desktop/direct3ddds/dds-pixelformat). Note that the **DDS\_HEADER** and **DDS\_PIXELFORMAT** structures replace the deprecated DDSURFACEDESC2, DDSCAPS2 and DDPIXELFORMAT DirectDraw 7 structures. **DDS\_HEADER** is the binary equivalent of DDSURFACEDESC2 and DDSCAPS2. **DDS\_PIXELFORMAT** is the binary equivalent of DDPIXELFORMAT.

    ```cpp
    DWORD               dwMagic;
    DDS_HEADER          header;
    ```

    If the value of **dwFlags** in [**DDS\_PIXELFORMAT**](/windows/desktop/direct3ddds/dds-pixelformat) is set to DDPF\_FOURCC and **dwFourCC** is set to "DX10" an additional [**DDS\_HEADER\_DXT10**](/windows/desktop/direct3ddds/dds-header-dxt10) structure will be present to accommodate texture arrays or DXGI formats that cannot be expressed as an RGB pixel format such as floating point formats, sRGB formats etc. When the **DDS\_HEADER\_DXT10** structure is present, the entire data description will looks like this.

    ```cpp
    DWORD               dwMagic;
    DDS_HEADER          header;
    DDS_HEADER_DXT10    header10;
    ```

-   A pointer to an array of bytes that contains the main surface data.
    ```cpp
    BYTE bdata[]
    ```

-   A pointer to an array of bytes that contains the remaining surfaces such as; mipmap levels, faces in a cube map, depths in a volume texture. Follow these links for more information about the DDS file layout for a: [texture](/windows/desktop/direct3ddds/dds-file-layout-for-textures), a [cube map](/windows/desktop/direct3ddds/dds-file-layout-for-cubic-environment-maps), or a [volume texture](/windows/desktop/direct3ddds/dds-file-layout-for-volume-textures).

    ```cpp
    BYTE bdata2[]
    ```

Many tools export to the DDS format. If you don't have a tool to export your texture to this format, consider creating one. For more detail on the DDS format and how to work with it in your code, read [Programming Guide for DDS](/windows/desktop/direct3ddds/dx-graphics-dds-pguide). In our example, we'll use DDS.

As with other resource types, you read the data from a file as a stream of bytes. Once your loading task completes, the lambda call runs code (the **CreateTexture** method) to process the stream of bytes into a format that Direct3D can use.

```cpp
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
```

In the previous snippet, the lambda checks to see if the filename has an extension of "dds". If it does, you assume that it is a DDS texture. If not, well, use the Windows Imaging Component (WIC) APIs to discover the format and decode the data as a bitmap. Either way, the result is a [**Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d) bitmap (or an error).

```cpp
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

        resource.As(&texture2D);
    }
    else
    {
        if (m_wicFactory.Get() == nullptr)
        {
            // A WIC factory object is required in order to load texture
            // assets stored in non-DDS formats.  If BasicLoader was not
            // initialized with one, create one as needed.
            CoCreateInstance(
                    CLSID_WICImagingFactory,
                    nullptr,
                    CLSCTX_INPROC_SERVER,
                    IID_PPV_ARGS(&m_wicFactory));
        }

        ComPtr<IWICStream> stream;
        m_wicFactory->CreateStream(&stream);

        stream->InitializeFromMemory(
                data,
                dataSize);

        ComPtr<IWICBitmapDecoder> bitmapDecoder;
        m_wicFactory->CreateDecoderFromStream(
                stream.Get(),
                nullptr,
                WICDecodeMetadataCacheOnDemand,
                &bitmapDecoder);

        ComPtr<IWICBitmapFrameDecode> bitmapFrame;
        bitmapDecoder->GetFrame(0, &bitmapFrame);

        ComPtr<IWICFormatConverter> formatConverter;
        m_wicFactory->CreateFormatConverter(&formatConverter);

        formatConverter->Initialize(
                bitmapFrame.Get(),
                GUID_WICPixelFormat32bppPBGRA,
                WICBitmapDitherTypeNone,
                nullptr,
                0.0,
                WICBitmapPaletteTypeCustom);

        uint32 width;
        uint32 height;
        bitmapFrame->GetSize(&width, &height);

        std::unique_ptr<byte[]> bitmapPixels(new byte[width * height * 4]);
        formatConverter->CopyPixels(
                nullptr,
                width * 4,
                width * height * 4,
                bitmapPixels.get());

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

        m_d3dDevice->CreateTexture2D(
                &textureDesc,
                &initialData,
                &texture2D);

        if (textureView != nullptr)
        {
            CD3D11_SHADER_RESOURCE_VIEW_DESC shaderResourceViewDesc(
                texture2D.Get(),
                D3D11_SRV_DIMENSION_TEXTURE2D
                );

            m_d3dDevice->CreateShaderResourceView(
                    texture2D.Get(),
                    &shaderResourceViewDesc,
                    &shaderResourceView);
        }
    }


    if (texture != nullptr)
    {
        *texture = texture2D.Detach();
    }
    if (textureView != nullptr)
    {
        *textureView = shaderResourceView.Detach();
    }
}
```

When this code completes, you have a [**Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d) in memory, loaded from an image file. As with meshes, you probably have a lot of them in your game and in any given scene. Consider creating caches for regularly accessed textures per-scene or per-level, rather than loading them all when the game or level starts.

(The **CreateDDSTextureFromMemory** method called in the above sample can be explored in full in [Complete code for DDSTextureLoader](complete-code-for-ddstextureloader.md).)

Also, individual textures or texture "skins" may map to specific mesh polygons or surfaces. This mapping data is usually exported by the tool an artist or designer used to create the model and the textures. Make sure that you capture this information as well when you load the exported data, as you will use it map the correct textures to the corresponding surfaces when you perform fragment shading.

### Loading shaders

Shaders are compiled High Level Shader Language (HLSL) files that are loaded into memory and invoked at specific stages of the graphics pipeline. The most common and essential shaders are the vertex and pixel shaders, which process the individual vertices of your mesh and the pixels in the scene's viewport(s), respectively. The HLSL code is executed to transform the geometry, apply lighting effects and textures, and perform post-processing on the rendered scene.

A Direct3D game can have a number of different shaders, each one compiled into a separate CSO (Compiled Shader Object, .cso) file. Normally, you don't have so many that you need to load them dynamically, and in most cases, you can simply load them when the game is starting, or on a per-level basis (such as a shader for rain effects).

The code in the **BasicLoader** class provides a number of overloads for different shaders, including vertex, geometry, pixel, and hull shaders. The code below covers pixel shaders as an example. (You can review the complete code in [Complete code for BasicLoader](complete-code-for-basicloader.md).)

```cpp
concurrency::task<void> LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11PixelShader** shader
    );

// ...

task<void> BasicLoader::LoadShaderAsync(
    _In_ Platform::String^ filename,
    _Out_ ID3D11PixelShader** shader
    )
{
    return m_basicReaderWriter->ReadDataAsync(filename).then([=](const Platform::Array<byte>^ bytecode)
    {
        
       m_d3dDevice->CreatePixelShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader);
    });
}

```

In this example, you use the **BasicReaderWriter** instance (**m\_basicReaderWriter**) to read in the supplied compiled shader object (.cso) file as a byte stream. Once that task completes, the lambda calls [**ID3D11Device::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader) with the byte data loaded from the file. Your callback must set some flag indicating that the load was successful, and your code must check this flag before running the shader.

Vertex shaders are bit more complex. For a vertex shader, you also load a separate input layout that defines the vertex data. The following code can be used to asynchronously load a vertex shader along with a custom vertex input layout. Be sure that the vertex information that you load from your meshes can be correctly represented by this input layout!

Let's create the input layout before you load the vertex shader.

```cpp
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

        m_d3dDevice->CreateInputLayout(
                basicVertexLayoutDesc,
                ARRAYSIZE(basicVertexLayoutDesc),
                bytecode,
                bytecodeSize,
                layout);
    }
    else
    {
        m_d3dDevice->CreateInputLayout(
                layoutDesc,
                layoutDescNumElements,
                bytecode,
                bytecodeSize,
                layout);
    }
}
```

In this particular layout, each vertex has the following data processed by the vertex shader:

-   A 3D coordinate position (x, y, z) in the model's coordinate space, represented as a trio of 32-bit floating point values.
-   A normal vector for the vertex, also represented as three 32-bit floating point values.
-   A transformed 2D texture coordinate value (u, v) , represented as a pair of 32-bit floating values.

These per-vertex input elements are called [HLSL semantics](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-semantics), and they are a set of defined registers used to pass data to and from your compiled shader object. Your pipeline runs the vertex shader once for every vertex in the mesh that you've loaded. The semantics define the input to (and output from) the vertex shader as it runs, and provide this data for your per-vertex computations in your shader's HLSL code.

Now, load the vertex shader object.

```cpp
concurrency::task<void> LoadShaderAsync(
        _In_ Platform::String^ filename,
        _In_reads_opt_(layoutDescNumElements) D3D11_INPUT_ELEMENT_DESC layoutDesc[],
        _In_ uint32 layoutDescNumElements,
        _Out_ ID3D11VertexShader** shader,
        _Out_opt_ ID3D11InputLayout** layout
        );

// ...

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
       m_d3dDevice->CreateVertexShader(
                bytecode->Data,
                bytecode->Length,
                nullptr,
                shader);

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
                layout);   
        }
    });
}
```

In this code, once you've read in the byte data for the vertex shader's CSO file, you create the vertex shader by calling [**ID3D11Device::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader). After that, you create your input layout for the shader in the same lambda.

Other shader types, such as hull and geometry shaders, can also require specific configuration. Complete code for a variety of shader loading methods is provided in [Complete code for BasicLoader](complete-code-for-basicloader.md) and in the [Direct3D resource loading sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%2B%2B%5D-Windows%208%20app%20samples/C%2B%2B/Windows%208%20app%20samples/Direct3D%20resource%20loading%20sample%20(Windows%208)/C%2B%2B).

## Remarks

At this point, you should understand and be able to create or modify methods for asynchronously loading common game resources and assets, such as meshes, textures, and compiled shaders.

## Related topics

* [Direct3D resource loading sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%2B%2B%5D-Windows%208%20app%20samples/C%2B%2B/Windows%208%20app%20samples/Direct3D%20resource%20loading%20sample%20(Windows%208)/C%2B%2B)
* [Complete code for BasicLoader](complete-code-for-basicloader.md)
* [Complete code for BasicReaderWriter](complete-code-for-basicreaderwriter.md)
* [Complete code for DDSTextureLoader](complete-code-for-ddstextureloader.md)

 

 