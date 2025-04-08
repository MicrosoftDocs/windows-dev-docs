---

title: Port the shader objects
description: When porting the simple renderer from OpenGL ES 2.0, the first step is to set up the equivalent vertex and fragment shader objects in Direct3D 11, and to make sure that the main program can communicate with the shader objects after they are compiled.
ms.assetid: 0383b774-bc1b-910e-8eb6-cc969b3dcc08

ms.date: 02/08/2017
ms.topic: article


keywords: windows 10, uwp, games, port, shader, direct3d, opengl
ms.localizationpriority: medium
---

# Port the shader objects

**Important APIs**

-   [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device)
-   [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext)

When porting the simple renderer from OpenGL ES 2.0, the first step is to set up the equivalent vertex and fragment shader objects in Direct3D 11, and to make sure that the main program can communicate with the shader objects after they are compiled.

> [!NOTE]
> Have you created a new Direct3D project? If not, follow the instructions in [Create a new DirectX 11 project for Universal Windows Platform (UWP)](user-interface.md). This walkthrough assumes that you have the created the DXGI and Direct3D resources for drawing to the screen, and which are provided in the template.

Much like OpenGL ES 2.0, the compiled shaders in Direct3D must be associated with a drawing context. However, Direct3D does not have the concept of a shader program object per se; instead, you must assign the shaders directly to an [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext). This step follows the OpenGL ES 2.0 process for creating and binding shader objects, and provides you with the corresponding API behaviors in Direct3D.

## Instructions

### Step 1: Compile the shaders

In this simple OpenGL ES 2.0 sample, the shaders are stored as text files and loaded as string data for run-time compilation.

OpenGL ES 2.0: Compile a shader

``` syntax
GLuint __cdecl CompileShader (GLenum shaderType, const char *shaderSrcStr)
// shaderType can be GL_VERTEX_SHADER or GL_FRAGMENT_SHADER. Returns 0 if compilation fails.
{
  GLuint shaderHandle;
  GLint compiledShaderHandle;
   
  // Create an empty shader object.
  shaderHandle = glCreateShader(shaderType);

  if (shaderHandle == 0)
  return 0;

  // Load the GLSL shader source as a string value. You could obtain it from
  // from reading a text file or hardcoded.
  glShaderSource(shaderHandle, 1, &shaderSrcStr, NULL);
   
  // Compile the shader.
  glCompileShader(shaderHandle);

  // Check the compile status
  glGetShaderiv(shaderHandle, GL_COMPILE_STATUS, &compiledShaderHandle);

  if (!compiledShaderHandle) // error in compilation occurred
  {
    // Handle any errors here.
              
    glDeleteShader(shaderHandle);
    return 0;
  }

  return shaderHandle;

}
```

In Direct3D, shaders are not compiled during run-time; they are always compiled to CSO files when the rest of the program is compiled. When you compile your app with Microsoft Visual Studio, the HLSL files are compiled to CSO (.cso) files that your app must load. Make sure you include these CSO files with your app when you package it!

> **Note**   The following example performs the shader loading and compilation asynchronously using the **auto** keyword and lambda syntax. ReadDataAsync() is a method implemented for the template that reads in a CSO file as an array of byte data (fileData).

 

Direct3D 11: Compile a shader

``` syntax
auto loadVSTask = DX::ReadDataAsync(m_projectDir + "SimpleVertexShader.cso");
auto loadPSTask = DX::ReadDataAsync(m_projectDir + "SimplePixelShader.cso");

auto createVSTask = loadVSTask.then([this](Platform::Array<byte>^ fileData) {

m_d3dDevice->CreateVertexShader(
  fileData->Data,
  fileData->Length,
  nullptr,
  &m_vertexShader);

auto createPSTask = loadPSTask.then([this](Platform::Array<byte>^ fileData) {
  m_d3dDevice->CreatePixelShader(
    fileData->Data,
    fileData->Length,
    nullptr,
    &m_pixelShader;
};
```

### Step 2: Create and load the vertex and fragment (pixel) shaders

OpenGL ES 2.0 has the notion of a shader "program", which serves as the interface between the main program running on the CPU and the shaders, which are executed on the GPU. Shaders are compiled (or loaded from compiled sources) and associated with a program, which enables execution on the GPU.

OpenGL ES 2.0: Loading the vertex and fragment shaders into a shading program

``` syntax
GLuint __cdecl LoadShaderProgram (const char *vertShaderSrcStr, const char *fragShaderSrcStr)
{
  GLuint programObject, vertexShaderHandle, fragmentShaderHandle;
  GLint linkStatusCode;

  // Load the vertex shader and compile it to an internal executable format.
  vertexShaderHandle = CompileShader(GL_VERTEX_SHADER, vertShaderSrcStr);
  if (vertexShaderHandle == 0)
  {
    glDeleteShader(vertexShaderHandle);
    return 0;
  }

   // Load the fragment/pixel shader and compile it to an internal executable format.
  fragmentShaderHandle = CompileShader(GL_FRAGMENT_SHADER, fragShaderSrcStr);
  if (fragmentShaderHandle == 0)
  {
    glDeleteShader(fragmentShaderHandle);
    return 0;
  }

  // Create the program object proper.
  programObject = glCreateProgram();
   
  if (programObject == 0)    return 0;

  // Attach the compiled shaders
  glAttachShader(programObject, vertexShaderHandle);
  glAttachShader(programObject, fragmentShaderHandle);

  // Compile the shaders into binary executables in memory and link them to the program object..
  glLinkProgram(programObject);

  // Check the project object link status and determine if the program is available.
  glGetProgramiv(programObject, GL_LINK_STATUS, &linkStatusCode);

  if (!linkStatusCode) // if link status <> 0
  {
    // Linking failed; delete the program object and return a failure code (0).

    glDeleteProgram (programObject);
    return 0;
  }

  // Deallocate the unused shader resources. The actual executables are part of the program object.
  glDeleteShader(vertexShaderHandle);
  glDeleteShader(fragmentShaderHandle);

  return programObject;
}

// ...

glUseProgram(renderer->programObject);
```

Direct3D does not have the concept of a shader program object. Rather, the shaders are created when one of the shader creation methods on the [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device) interface (such as [**ID3D11Device::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader) or [**ID3D11Device::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader)) is called. To set the shaders for the current drawing context, we provide them to corresponding [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) with a set shader method, such as [**ID3D11DeviceContext::VSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-vssetshader) for the vertex shader or [**ID3D11DeviceContext::PSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-pssetshader) for the fragment shader.

Direct3D 11: Set the shaders for the graphics device drawing context.

``` syntax
m_d3dContext->VSSetShader(
  m_vertexShader.Get(),
  nullptr,
  0);

m_d3dContext->PSSetShader(
  m_pixelShader.Get(),
  nullptr,
  0);
```

### Step 3: Define the data to supply to the shaders

In our OpenGL ES 2.0 example, we have one **uniform** to declare for the shader pipeline:

-   **u\_mvpMatrix**: a 4x4 array of floats that represents the final model-view-projection transformation matrix that takes the model coordinates for the cube and transforms them into 2D projection coordinates for scan conversion.

And two **attribute** values for the vertex data:

-   **a\_position**: a 4-float vector for the model coordinates of a vertex.
-   **a\_color**: A 4-float vector for the RGBA color value associated with the vertex.

Open GL ES 2.0: GLSL definitions for the uniforms and attributes

``` syntax
uniform mat4 u_mvpMatrix;
attribute vec4 a_position;
attribute vec4 a_color;
```

The corresponding main program variables are defined as fields on the renderer object, in this case. (Refer to the header in [How to: port a simple OpenGL ES 2.0 renderer to Direct3D 11](port-a-simple-opengl-es-2-0-renderer-to-directx-11-1.md).) Once we've done that, we need to specify the locations in memory where the main program will supply these values for the shader pipeline, which we typically do right before a draw call:

OpenGL ES 2.0: Marking the location of the uniform and attribute data

``` syntax

// Inform the shader of the attribute locations
loc = glGetAttribLocation(renderer->programObject, "a_position");
glVertexAttribPointer(loc, 3, GL_FLOAT, GL_FALSE, 
    sizeof(Vertex), 0);
glEnableVertexAttribArray(loc);

loc = glGetAttribLocation(renderer->programObject, "a_color");
glVertexAttribPointer(loc, 4, GL_FLOAT, GL_FALSE, 
    sizeof(Vertex), (GLvoid*) (sizeof(float) * 3));
glEnableVertexAttribArray(loc);


// Inform the shader program of the uniform location
renderer->mvpLoc = glGetUniformLocation(renderer->programObject, "u_mvpMatrix");
```

Direct3D does not have the concept of an "attribute" or a "uniform" in the same sense (or, at least, it does not share this syntax). Rather, it has constant buffers, represented as Direct3D subresources -- resources that are shared between the main program and the shader programs. Some of these subresources, such as vertex positions and colors, are described as HLSL semantics. For more info on constant buffers and HLSL semantics as they relate to OpenGL ES 2.0 concepts, read [Port frame buffer objects, uniforms, and attributes](porting-uniforms-and-attributes.md).

When moving this process to Direct3D, we convert the uniform to a Direct3D constant buffer (cbuffer) and assign it a register for lookup with the **register** HLSL semantic. The two vertex attributes are handled as input elements to the shader pipeline stages, and are also assigned [HLSL semantics](/windows/desktop/direct3dhlsl/dcl-usage---ps) (POSITION and COLOR0) that inform the shaders. The pixel shader takes an SV\_POSITION, with the SV\_ prefix indicating that it is a system value generated by the GPU. (In this case, it is a pixel position generated during scan conversion.) VertexShaderInput and PixelShaderInput are not declared as constant buffers because the former will be used to define the vertex buffer (see [Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md)), and the data for the latter is generated as the result of a previous stage in the pipeline, which in this case is the vertex shader.

Direct3D: HLSL definitions for the constant buffers and vertex data

``` syntax
cbuffer ModelViewProjectionConstantBuffer : register(b0)
{
  matrix mvp;
};

// Per-vertex data used as input to the vertex shader.
struct VertexShaderInput
{
  float4 pos : POSITION;
  float4 color : COLOR0;
};

// Per-vertex color data passed through the pixel shader.
struct PixelShaderInput
{
  float4 pos : SV_POSITION;
  float3 color : COLOR0;
};
```

For more info on porting to constant buffers and the application of HLSL semantics, read [Port frame buffer objects, uniforms, and attributes](porting-uniforms-and-attributes.md).

Here are the structures for the layout of the data passed to the shader pipeline with a constant or vertex buffer.

Direct3D 11: Declaring the constant and vertex buffers layout

``` syntax
// Constant buffer used to send MVP matrices to the vertex shader.
struct ModelViewProjectionConstantBuffer
{
  DirectX::XMFLOAT4X4 modelViewProjection;
};

// Used to send per-vertex data to the vertex shader.
struct VertexPositionColor
{
  DirectX::XMFLOAT4 pos;
  DirectX::XMFLOAT4 color;
};
```

Use the DirectXMath XM\* types for your constant buffer elements, since they provide proper packing and alignment for the contents when they are sent to the shader pipeline. If you use standard Windows platform float types and arrays, you must perform the packing and alignment yourself.

To bind a constant buffer, create a layout description as a [**CD3D11\_BUFFER\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-cd3d11_buffer_desc) structure, and pass it to [**ID3DDevice::CreateBuffer**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createbuffer). Then, in your render method, pass the constant buffer to [**ID3D11DeviceContext::UpdateSubresource**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-updatesubresource) before drawing.

Direct3D 11: Bind the constant buffer

``` syntax
CD3D11_BUFFER_DESC constantBufferDesc(sizeof(ModelViewProjectionConstantBuffer), D3D11_BIND_CONSTANT_BUFFER);

m_d3dDevice->CreateBuffer(
  &constantBufferDesc,
  nullptr,
  &m_constantBuffer);

// ...

// Only update shader resources that have changed since the last frame.
m_d3dContext->UpdateSubresource(
  m_constantBuffer.Get(),
  0,
  NULL,
  &m_constantBufferData,
  0,
  0);
```

The vertex buffer is created and updated similarly, and is discussed in the next step, [Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md).

## Next step

[Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md)

## Related topics

[How to: port a simple OpenGL ES 2.0 renderer to Direct3D 11](port-a-simple-opengl-es-2-0-renderer-to-directx-11-1.md)

[Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md)

[Port the GLSL](port-the-glsl.md)

[Draw to the screen](draw-to-the-screen.md)
