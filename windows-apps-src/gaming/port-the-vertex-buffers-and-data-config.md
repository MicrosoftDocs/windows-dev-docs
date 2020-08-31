---
title: Port the vertex buffers and data
description: In this step, you'll define the vertex buffers that will contain your meshes and the index buffers that allow the shaders to traverse the vertices in a specified order.
ms.assetid: 9a8138a5-0797-8532-6c00-58b907197a25
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, port, vertex buffers, data, direct3d
ms.localizationpriority: medium
---
# Port the vertex buffers and data




**Important APIs**

-   [**ID3DDevice::CreateBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createbuffer)
-   [**ID3DDeviceContext::IASetVertexBuffers**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetvertexbuffers)
-   [**ID3D11DeviceContext::IASetIndexBuffer**](/windows/desktop/api/d3d10/nf-d3d10-id3d10device-iasetindexbuffer)

In this step, you'll define the vertex buffers that will contain your meshes and the index buffers that allow the shaders to traverse the vertices in a specified order.

At this point, let's examine the hardcoded model for the cube mesh we are using. Both representations have the vertices organized as a triangle list (as opposed to a strip or other more efficient triangle layout). All vertices in both representations also have associated indices and color values. Much of the Direct3D code in this topic refers to variables and objects defined in the Direct3D project.

Here's the cube for processing by OpenGL ES 2.0. In the sample implementation, each vertex is 7 float values: 3 position coordinates followed by 4 RGBA color values.

```cpp
#define CUBE_INDICES 36
#define CUBE_VERTICES 8

GLfloat cubeVertsAndColors[] = 
{
  -0.5f, -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f,
  -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f,
  -0.5f,  0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f,
  -0.5f,  0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 1.0f,
  0.5f, -0.5f,  0.5f, 1.0f, 0.0f, 1.0f, 1.0f,
  0.5f, -0.5f, -0.5f, 1.0f, 0.0f, 0.0f, 1.0f,  
  0.5f,  0.5f,  0.5f, 1.0f, 1.0f, 1.0f, 1.0f,
  0.5f,  0.5f, -0.5f, 1.0f, 1.0f, 0.0f, 1.0f
};

GLuint cubeIndices[] = 
{
  0, 1, 2, // -x
  1, 3, 2,

  4, 6, 5, // +x
  6, 7, 5,

  0, 5, 1, // -y
  5, 6, 1,

  2, 6, 3, // +y
  6, 7, 3,

  0, 4, 2, // +z
  4, 6, 2,

  1, 7, 3, // -z
  5, 7, 1
};
```

And here's the same cube for processing by Direct3D 11.

```cpp
VertexPositionColor cubeVerticesAndColors[] = 
// struct format is position, color
{
  {XMFLOAT3(-0.5f, -0.5f, -0.5f), XMFLOAT3(0.0f, 0.0f, 0.0f)},
  {XMFLOAT3(-0.5f, -0.5f,  0.5f), XMFLOAT3(0.0f, 0.0f, 1.0f)},
  {XMFLOAT3(-0.5f,  0.5f, -0.5f), XMFLOAT3(0.0f, 1.0f, 0.0f)},
  {XMFLOAT3(-0.5f,  0.5f,  0.5f), XMFLOAT3(0.0f, 1.0f, 1.0f)},
  {XMFLOAT3( 0.5f, -0.5f, -0.5f), XMFLOAT3(1.0f, 0.0f, 0.0f)},
  {XMFLOAT3( 0.5f, -0.5f,  0.5f), XMFLOAT3(1.0f, 0.0f, 1.0f)},
  {XMFLOAT3( 0.5f,  0.5f, -0.5f), XMFLOAT3(1.0f, 1.0f, 0.0f)},
  {XMFLOAT3( 0.5f,  0.5f,  0.5f), XMFLOAT3(1.0f, 1.0f, 1.0f)},
};
        
unsigned short cubeIndices[] = 
{
  0, 2, 1, // -x
  1, 2, 3,

  4, 5, 6, // +x
  5, 7, 6,

  0, 1, 5, // -y
  0, 5, 4,

  2, 6, 7, // +y
  2, 7, 3,

  0, 4, 6, // -z
  0, 6, 2,

  1, 3, 7, // +z
  1, 7, 5
};
```

Reviewing this code, you notice that the cube in the OpenGL ES 2.0 code is represented in a right-hand coordinate system, whereas the cube in the Direct3D-specific code is represented in a left-hand coordinate system. When importing your own mesh data, you must reverse the z-axis coordinates for your model and change the indices for each mesh accordingly to traverse the triangles according to the change in the coordinate system.

Assuming that we have successfully moved the cube mesh from the right-handed OpenGL ES 2.0 coordinate system to the left-handed Direct3D one, let's see how to load the cube data for processing in both models.

## Instructions

### Step 1: Create an input layout

In OpenGL ES 2.0, your vertex data is supplied as attributes that will be supplied to and read by the shader objects. You typically provide a string that contains the attribute name used in the shader's GLSL to the shader program object, and get a memory location back that you can supply to the shader. In this example, a vertex buffer object contains a list of custom Vertex structures, defined and formatted as follows:

OpenGL ES 2.0: Configure the attributes that contain the per-vertex information.

``` syntax
typedef struct 
{
  GLfloat pos[3];        
  GLfloat rgba[4];
} Vertex;
```

In OpenGL ES 2.0, input layouts are implicit; you take a general purpose GL\_ELEMENT\_ARRAY\_BUFFER and supply the stride and offset such that the vertex shader can interpret the data after uploading it. You inform the shader before rendering which attributes map to which portions of each block of vertex data with **glVertexAttribPointer**.

In Direct3D, you must provide an input layout to describe the structure of the vertex data in the vertex buffer when you create the buffer, instead of before you draw the geometry. To do this, you use an input layout which corresponds to layout of the data for our individual vertices in memory. It is very important to specify this accurately!

Here, you create an input description as an array of [**D3D11\_INPUT\_ELEMENT\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_input_element_desc) structures.

Direct3D: Define an input layout description.

``` syntax
struct VertexPositionColor
{
  DirectX::XMFLOAT3 pos;
  DirectX::XMFLOAT3 color;
};

// ...

const D3D11_INPUT_ELEMENT_DESC vertexDesc[] = 
{
  { "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0,  D3D11_INPUT_PER_VERTEX_DATA, 0 },
  { "COLOR",    0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0 },
};

```

This input description defines a vertex as a pair of 2 3-coordinate vectors: one 3D vector to store the position of the vertex in model coordinates, and another 3D vector to store the RGB color value associated with the vertex. In this case, you use 3x32 bit floating point format, elements of which we represent in code as `XMFLOAT3(X.Xf, X.Xf, X.Xf)`. You should use types from the [DirectXMath](/windows/desktop/dxmath/ovw-xnamath-reference) library whenever you are handling data that will be used by a shader, as it ensure the proper packing and alignment of that data. (For example, use [**XMFLOAT3**](/windows/desktop/api/directxmath/ns-directxmath-xmfloat3) or [**XMFLOAT4**](/windows/desktop/api/directxmath/ns-directxmath-xmfloat4) for vector data, and [**XMFLOAT4X4**](/windows/desktop/api/directxmath/ns-directxmath-xmfloat4x4) for matrices.)

For a list of all the possible format types, refer to [**DXGI\_FORMAT**](/windows/desktop/api/dxgiformat/ne-dxgiformat-dxgi_format).

With the per-vertex input layout defined, you create the layout object. In the following code, you write it to **m\_inputLayout**, a variable of type **ComPtr** (which points to an object of type [**ID3D11InputLayout**](/windows/desktop/api/d3d11/nn-d3d11-id3d11inputlayout)). **fileData** contains the compiled vertex shader object from the previous step, [Port the shaders](port-the-shader-config.md).

Direct3D: Create the input layout used by the vertex buffer.

``` syntax
Microsoft::WRL::ComPtr<ID3D11InputLayout>      m_inputLayout;

// ...

m_d3dDevice->CreateInputLayout(
  vertexDesc,
  ARRAYSIZE(vertexDesc),
  fileData->Data,
  fileShaderData->Length,
  &m_inputLayout
);
```

We've defined the input layout. Now, let's create a buffer that uses this layout and load it with the cube mesh data.

### Step 2: Create and load the vertex buffer(s)

In OpenGL ES 2.0, you create a pair of buffers, one for the position data and one for the color data. (You could also create a struct that contains both and a single buffer.) You bind each buffer and write position and color data into them. Later, during your render function, bind the buffers again and provide the shader with the format of the data in the buffer so it can correctly interpret it.

OpenGL ES 2.0: Bind the vertex buffers

``` syntax
// upload the data for the vertex position buffer
glGenBuffers(1, &renderer->vertexBuffer);    
glBindBuffer(GL_ARRAY_BUFFER, renderer->vertexBuffer);
glBufferData(GL_ARRAY_BUFFER, sizeof(VERTEX) * CUBE_VERTICES, renderer->vertices, GL_STATIC_DRAW);   
```

In Direct3D, shader-accessible buffers are represented as [**D3D11\_SUBRESOURCE\_DATA**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_subresource_data) structures. To bind the location of this buffer to shader object, you need to create a CD3D11\_BUFFER\_DESC structure for each buffer with [**ID3DDevice::CreateBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createbuffer), and then set the buffer of the Direct3D device context by calling a set method specific to the buffer type, such as [**ID3DDeviceContext::IASetVertexBuffers**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-iasetvertexbuffers).

When you set the buffer, you must set the stride (the size of the data element for an individual vertex) as well the offset (where the vertex data array actually starts) from the beginning of the buffer.

Notice that we assign the pointer to the **vertexIndices** array to the **pSysMem** field of the [**D3D11\_SUBRESOURCE\_DATA**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_subresource_data) structure. If this isn't correct, your mesh will be corrupt or empty!

Direct3D: Create and set the vertex buffer

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
        
// ...

UINT stride = sizeof(VertexPositionColor);
UINT offset = 0;
m_d3dContext->IASetVertexBuffers(
  0,
  1,
  m_vertexBuffer.GetAddressOf(),
  &stride,
  &offset);
```

### Step 3: Create and load the index buffer

Index buffers are an efficient way to allow the vertex shader to look up individual vertices. Although they are not required, we use them in this sample renderer. As with vertex buffers in OpenGL ES 2.0, an index buffer is created and bound as a general purpose buffer and the vertex indices you created earlier are copied into it.

When you're ready to draw, you bind both the vertex and the index buffer again, and call **glDrawElements**.

OpenGL ES 2.0: Send the index order to the draw call.

``` syntax
GLuint indexBuffer;

// ...

glGenBuffers(1, &renderer->indexBuffer);    
glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, renderer->indexBuffer);   
glBufferData(GL_ELEMENT_ARRAY_BUFFER, 
  sizeof(GLuint) * CUBE_INDICES, 
  renderer->vertexIndices, 
  GL_STATIC_DRAW);

// ...
// Drawing function

// Bind the index buffer
glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, renderer->indexBuffer);
glDrawElements (GL_TRIANGLES, renderer->numIndices, GL_UNSIGNED_INT, 0);
```

With Direct3D, it's a bit very similar process, albeit a bit more didactic. Supply the index buffer as a Direct3D subresource to the [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) you created when you configured Direct3D. You do this by calling [**ID3D11DeviceContext::IASetIndexBuffer**](/windows/desktop/api/d3d10/nf-d3d10-id3d10device-iasetindexbuffer) with the configured subresource for the index array, as follows. (Again, notice that you assign the pointer to the **cubeIndices** array to the **pSysMem** field of the [**D3D11\_SUBRESOURCE\_DATA**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_subresource_data) structure.)

Direct3D: Create the index buffer.

``` syntax
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

// ...

m_d3dContext->IASetIndexBuffer(
  m_indexBuffer.Get(),
  DXGI_FORMAT_R16_UINT,
  0);
```

Later, you will draw the triangles with a call to [**ID3D11DeviceContext::DrawIndexed**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-drawindexed) (or [**ID3D11DeviceContext::Draw**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-draw) for unindexed vertices), as follows. (For more details, jump ahead to [Draw to the screen](draw-to-the-screen.md).)

Direct3D: Draw the indexed vertices.

``` syntax
m_d3dContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
m_d3dContext->IASetInputLayout(m_inputLayout.Get());

// ...

m_d3dContext->DrawIndexed(
  m_indexCount,
  0,
  0);
```

## Previous step


[Port the shader objects](port-the-shader-config.md)

## Next step

[Port the GLSL](port-the-glsl.md)

## Remarks

When structuring your Direct3D, separate the code that calls methods on [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device) into a method that is called whenever the device resources need to be recreated. (In the Direct3D project template, this code is in the renderer object's **CreateDeviceResource** methods. The code that updates the device context ([**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext)), on the other hand, is placed in the **Render** method, since this is where you actually construct the shader stages and bind the data.

## Related topics


* [How to: port a simple OpenGL ES 2.0 renderer to Direct3D 11](port-a-simple-opengl-es-2-0-renderer-to-directx-11-1.md)
* [Port the shader objects](port-the-shader-config.md)
* [Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md)
* [Port the GLSL](port-the-glsl.md)

 

 