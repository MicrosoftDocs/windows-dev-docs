---
title: Port OpenGL ES 2.0 buffers, uniforms, vertexes to Direct3D
description: During the process of porting to Direct3D 11 from OpenGL ES 2.0, you must change the syntax and API behavior for passing data between the app and the shader programs.
ms.assetid: 9b215874-6549-80c5-cc70-c97b571c74fe
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, opengl, direct3d, buffers, uniforms, vertex attributes
ms.localizationpriority: medium
---
# Compare OpenGL ES 2.0 buffers, uniforms, and vertex attributes to Direct3D




**Important APIs**

-   [**ID3D11Device1::CreateBuffer**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1)
-   [**ID3D11Device1::CreateInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createinputlayout)
-   [**ID3D11DeviceContext1::IASetInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetinputlayout)

During the process of porting to Direct3D 11 from OpenGL ES 2.0, you must change the syntax and API behavior for passing data between the app and the shader programs.

In OpenGL ES 2.0, data is passed to and from shader programs in four ways: as uniforms for constant data, as attributes for vertex data, as buffer objects for other resource data (such as textures). In Direct3D 11, these roughly map to constant buffers, vertex buffers, and subresources. Despite the superficial commonality, they are handled quite different in usage.

Here's the basic mapping.

| OpenGL ES 2.0             | Direct3D 11                                                                                                                                                                         |
|---------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| uniform                   | constant buffer (**cbuffer**) field.                                                                                                                                                |
| attribute                 | vertex buffer element field, designated by an input layout and marked with a specific HLSL semantic.                                                                                |
| buffer object             | buffer; See [**D3D11\_SUBRESOURCE\_DATA**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_subresource_data) and [**D3D11\_BUFFER\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_buffer_desc) and for a general-use buffer definitions. |
| frame buffer object (FBO) | render target(s); See [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) with [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d).                                       |
| back buffer               | swap chain with "back buffer" surface; See [**IDXGISwapChain1**](/windows/desktop/api/dxgi1_2/nn-dxgi1_2-idxgiswapchain1) with attached [**IDXGISurface1**](/windows/desktop/api/dxgi/nn-dxgi-idxgisurface1).                       |

 

## Port buffers


In OpenGL ES 2.0, the process for creating and binding any kind of buffer generally follows this pattern

-   Call glGenBuffers to generate one or more buffers and return the handles to them.
-   Call glBindBuffer to define the layout of a buffer, such as GL\_ELEMENT\_ARRAY\_BUFFER.
-   Call glBufferData to populate the buffer with specific data (such as vertex structures, index data, or color data) in a specific layout.

The most common kind of buffer is the vertex buffer, which minimally contains the positions of the vertices in some coordinate system. In typical use, a vertex is represented by a structure that contains the position coordinates, a normal vector to the vertex position, a tangent vector to the vertex position, and texture lookup (uv) coordinates. The buffer contains a contiguous list of these vertices, in some order (like a triangle list, or strip, or fan), and which collectively represent the visible polygons in your scene. (In Direct3D 11 as well as OpenGL ES 2.0 it is inefficient to have multiple vertex buffers per draw call.)

Here's an example a vertex buffer and an index buffer created with OpenGL ES 2.0:

OpenGL ES 2.0: Creating and populating a vertex buffer and an index buffer.

``` syntax
glGenBuffers(1, &renderer->vertexBuffer);
glBindBuffer(GL_ARRAY_BUFFER, renderer->vertexBuffer);
glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * CUBE_VERTICES, renderer->vertices, GL_STATIC_DRAW);

glGenBuffers(1, &renderer->indexBuffer);
glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, renderer->indexBuffer);
glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(int) * CUBE_INDICES, renderer->vertexIndices, GL_STATIC_DRAW);
```

Other buffers include pixel buffers and maps, like textures. The shader pipeline can render into texture buffers (pixmaps) or render buffer objects and use those buffers in future shader passes. In the simplest case, the call flow is:

-   Call glGenFramebuffers to generate a frame buffer object.
-   Call glBindFramebuffer to bind the frame buffer object for writing.
-   Call glFramebufferTexture2D to draw into a specified texture map.

In Direct3D 11, buffer data elements are considered "subresources," and can range from individual vertex data elements to MIP-map textures.

-   Populate a [**D3D11\_SUBRESOURCE\_DATA**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_subresource_data) structure with the configuration for a buffer data element.
-   Populate a [**D3D11\_BUFFER\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_buffer_desc) structure with the size of the individual elements in the buffer as well as the buffer type.
-   Call [**ID3D11Device1::CreateBuffer**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) with these two structures.

Direct3D 11: Creating and populating a vertex buffer and an index buffer.

``` syntax
D3D11_SUBRESOURCE_DATA vertexBufferData = {0};
vertexBufferData.pSysMem = cubeVertices;
vertexBufferData.SysMemPitch = 0;
vertexBufferData.SysMemSlicePitch = 0;
CD3D11_BUFFER_DESC vertexBufferDesc(sizeof(cubeVertices), D3D11_BIND_VERTEX_BUFFER);

m_d3dDevice->CreateBuffer(
  &vertexBufferDesc,
  &vertexBufferData,
  &m_vertexBuffer);

m_indexCount = ARRAYSIZE(cubeIndices);

D3D11_SUBRESOURCE_DATA indexBufferData = {0};
indexBufferData.pSysMem = cubeIndices;
indexBufferData.SysMemPitch = 0;
indexBufferData.SysMemSlicePitch = 0;
CD3D11_BUFFER_DESC indexBufferDesc(sizeof(cubeIndices), D3D11_BIND_INDEX_BUFFER);

m_d3dDevice->CreateBuffer(
  &indexBufferDesc,
  &indexBufferData,
  &m_indexBuffer);
    
```

Writable pixel buffers or maps, such as a frame buffer, can be created as [**ID3D11Texture2D**](/windows/desktop/api/d3d11/nn-d3d11-id3d11texture2d) objects. These can be bound as resources to an [**ID3D11RenderTargetView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11rendertargetview) or [**ID3D11ShaderResourceView**](/windows/desktop/api/d3d11/nn-d3d11-id3d11shaderresourceview), which, once drawn into, can be displayed with the associated swap chain or passed to a shader, respectively.

Direct3D 11: Creating a frame buffer object.

``` syntax
ComPtr<ID3D11RenderTargetView> m_d3dRenderTargetViewWin;
// ...
ComPtr<ID3D11Texture2D> frameBuffer;

m_swapChainCoreWindow->GetBuffer(0, IID_PPV_ARGS(&frameBuffer));
m_d3dDevice->CreateRenderTargetView(
  frameBuffer.Get(),
  nullptr,
  &m_d3dRenderTargetViewWin);
```

## Change uniforms and uniform buffer objects to Direct3D constant buffers


In Open GL ES 2.0, uniforms are the mechanism to supply constant data to individual shader programs. This data cannot be altered by the shaders.

Setting a uniform typically involves providing one of the glUniform\* methods with the upload location in the GPU along with a pointer to the data in app memory. After ithe glUniform\* method executes, the uniform data is in the GPU memory and accessible by the shaders that have declared that uniform. You are expected to ensure that the data is packed in such a way that the shader can interpret it based on the uniform declaration in the shader (by using compatible types).

OpenGL ES 2.0 Creating a uniform and uploading data to it

``` syntax
renderer->mvpLoc = glGetUniformLocation(renderer->programObject, "u_mvpMatrix");

// ...

glUniformMatrix4fv(renderer->mvpLoc, 1, GL_FALSE, (GLfloat*) &renderer->mvpMatrix.m[0][0]);
```

In a shader's GLSL, the corresponding uniform declaration looks like this:

Open GL ES 2.0: GLSL uniform declaration

``` syntax
uniform mat4 u_mvpMatrix;
```

Direct3D designates uniform data as "constant buffers," which, like uniforms, contain constant data provided to individual shaders. As with uniform buffers, it is important to pack the constant buffer data in memory identically to the way the shader expects to interpret it. Using DirectXMath types (such as [**XMFLOAT4**](/windows/desktop/api/directxmath/ns-directxmath-xmfloat4)) instead of platform types (such as **float\*** or **float\[4\]**) guarantees proper data element alignment.

Constant buffers must have an associated GPU register used to reference that data on the GPU. The data is packed into the register location as indicated by the layout of the buffer.

Direct3D 11: Creating a constant buffer and uploading data to it

``` syntax
struct ModelViewProjectionConstantBuffer
{
     DirectX::XMFLOAT4X4 mvp;
};

// ...

ModelViewProjectionConstantBuffer   m_constantBufferData;

// ...

XMStoreFloat4x4(&m_constantBufferData.mvp, mvpMatrix);

CD3D11_BUFFER_DESC constantBufferDesc(sizeof(ModelViewProjectionConstantBuffer), D3D11_BIND_CONSTANT_BUFFER);
m_d3dDevice->CreateBuffer(
  &constantBufferDesc,
  nullptr,
  &m_constantBuffer);
```

In a shader's HLSL, the corresponding constant buffer declaration looks like this:

Direct3D 11: Constant buffer HLSL declaration

``` syntax
cbuffer ModelViewProjectionConstantBuffer : register(b0)
{
  matrix mvp;
};
```

Note that a register must be declared for each constant buffer. Different Direct3D feature levels have different maximum available registers, so do not exceed the maximum number for the lowest feature level you are targeting.

## Port vertex attributes to a Direct3D input layouts and HLSL semantics


Since vertex data can be modified by the shader pipeline, OpenGL ES 2.0 requires that you specify them as "attributes" instead of "uniforms". (This has changed in later versions of OpenGL and GLSL.) Vertex-specific data such the vertex position, normals, tangents, and color values are supplied to the shaders as attribute values. These attribute values correspond to specific offsets for each element in the vertex data; for example, the first attribute could point to the position component of an individual vertex, and the second to the normal, and so on.

The basic process for moving the vertex buffer data from main memory to the GPU looks like this:

-   Upload the vertex data with glBindBuffer.
-   Get the location of the attributes on the GPU with glGetAttribLocation. Call it for each attribute in the vertex data element.
-   Call glVertexAttribPointer to provide set the correct attribute size and offset inside an individual vertex data element. Do this for each attribute.
-   Enable the vertex data layout information with glEnableVertexAttribArray.

OpenGL ES 2.0: Uploading vertex buffer data to the shader attribute

``` syntax
glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, renderer->vertexBuffer);
loc = glGetAttribLocation(renderer->programObject, "a_position");

glVertexAttribPointer(loc, 3, GL_FLOAT, GL_FALSE, 
  sizeof(Vertex), 0);
loc = glGetAttribLocation(renderer->programObject, "a_color");
glEnableVertexAttribArray(loc);

glVertexAttribPointer(loc, 4, GL_FLOAT, GL_FALSE, 
  sizeof(Vertex), (GLvoid*) (sizeof(float) * 3));
glEnableVertexAttribArray(loc);
```

Now, in your vertex shader, you declare attributes with the same names you defined in your call to glGetAttribLocation.

OpenGL ES 2.0: Declaring an attribute in GLSL

``` syntax
attribute vec4 a_position;
attribute vec4 a_color;                     
```

In some ways, the same process holds for Direct3D. Instead of attributes, vertex data is provided in input buffers, which include vertex buffers and the corresponding index buffers. However, since Direct3D does not have the "attribute" declaration, you must specify an input layout which declares the individual component of the data elements in the vertex buffer and the HLSL semantics that indicate where and how those components are to be interpreted by the vertex shader. HLSL semantics require that you define the usage of each component with a specific string that informs the shader engine as to its purpose. For example, vertex position data is marked as POSITION, normal data is marked as NORMAL, and vertex color data is marked as COLOR. (Other shader stages also require specific semantics, and those semantics have different interpretations based on the shader stage.) For more info on HLSL semantics, read [Port your shader pipeline](change-your-shader-loading-code.md) and [HLSL Semantics](/windows/desktop/direct3dhlsl/dcl-usage---ps).

Collectively, the process of setting the vertex and index buffers, and setting the input layout is called the "Input Assembly" (IA) stage of the Direct3D graphics pipeline.

Direct3D 11: Configuring the input assembly stage

``` syntax
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
```

An input layout is declared and associated with a vertex shader by declaring the format of the vertex data element and the semantic used for each component. The vertex element data layout described in the D3D11\_INPUT\_ELEMENT\_DESC you create must correspond to the layout of the corresponding structure. Here, you create a layout for vertex data that has two components:

-   A vertex position coordinate, represented in main memory as an XMFLOAT3, which is an aligned array of 3 32-bit floating point values for the (x, y, z) coordinates.
-   A vertex color value, represented as an XMFLOAT4, which is an aligned array of 4 32-bit floating point values for the color (RGBA).

You assign a semantic for each one, as well as a format type. You then pass the description to [**ID3D11Device1::CreateInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createinputlayout). The input layout is used when we call [**ID3D11DeviceContext1::IASetInputLayout**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetinputlayout) when you set up the input assembly during our render method.

Direct3D 11: Describing an input layout with specific semantics

``` syntax
ComPtr<ID3D11InputLayout> m_inputLayout;

// ...

const D3D11_INPUT_ELEMENT_DESC vertexDesc[] = 
{
  { "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0,  D3D11_INPUT_PER_VERTEX_DATA, 0 },
  { "COLOR",    0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0 },
};

m_d3dDevice->CreateInputLayout(
  vertexDesc,
  ARRAYSIZE(vertexDesc),
  fileData->Data,
  fileData->Length,
  &m_inputLayout);

// ...
// When we start the drawing process...

m_d3dContext->IASetInputLayout(m_inputLayout.Get());
```

Finally, you make sure that the shader can understand the input data by declaring the input. The semantics you assigned in the layout are used to select the correct locations in GPU memory.

Direct3D 11: Declaring shader input data with HLSL semantics

``` syntax
struct VertexShaderInput
{
  float3 pos : POSITION;
  float3 color : COLOR;
};
```

 

 