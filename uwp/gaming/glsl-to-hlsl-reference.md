---
title: GLSL-to-HLSL reference
description: You port your OpenGL Shader Language (GLSL) code to Microsoft High Level Shader Language (HLSL) code when you port your graphics architecture from OpenGL ES 2.0 to Direct3D 11 to create a game for Universal Windows Platform (UWP).
ms.assetid: 979d19f6-ef0c-64e4-89c2-a31e1c7b7692
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, glsl, hlsl, opengl, directx, shaders
ms.localizationpriority: medium
---
# GLSL-to-HLSL reference



You port your OpenGL Shader Language (GLSL) code to Microsoft High Level Shader Language (HLSL) code when you [port your graphics architecture from OpenGL ES 2.0 to Direct3D 11](port-from-opengl-es-2-0-to-directx-11-1.md) to create a game for Universal Windows Platform (UWP). The GLSL that is referred to herein is compatible with OpenGL ES 2.0; the HLSL is compatible with Direct3D 11. For info about the differences between Direct3D 11 and previous versions of Direct3D, see [Feature mapping](feature-mapping.md).

-   [Comparing OpenGL ES 2.0 with Direct3D 11](#comparing-opengl-es-20-with-direct3d-11)
-   [Porting GLSL variables to HLSL](#porting-glsl-variables-to-hlsl)
-   [Porting GLSL types to HLSL](#porting-glsl-types-to-hlsl)
-   [Porting GLSL pre-defined global variables to HLSL](#porting-glsl-pre-defined-global-variables-to-hlsl)
-   [Examples of porting GLSL variables to HLSL](#examples-of-porting-glsl-variables-to-hlsl)
    -   [Uniform, attribute, and varying in GLSL](#uniform-attribute-and-varying-in-glsl)
    -   [Constant buffers and data transfers in HLSL](#constant-buffers-and-data-transfers-in-hlsl)
-   [Examples of porting OpenGL rendering code to Direct3D](#examples-of-porting-opengl-rendering-code-to-direct3d)
-   [Related topics](#related-topics)

## Comparing OpenGL ES 2.0 with Direct3D 11


OpenGL ES 2.0 and Direct3D 11 have many similarities. They both have similar rendering pipelines and graphics features. But Direct3D 11 is a rendering implementation and API, not a specification; OpenGL ES 2.0 is a rendering specification and API, not an implementation. Direct3D 11 and OpenGL ES 2.0 generally differ in these ways:

| OpenGL ES 2.0                                                                                         | Direct3D 11                                                                                                            |
|-------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------|
| Hardware and operating system agnostic specification with vendor provided implementations             | Microsoft implementation of hardware abstraction and certification on Windows platforms                                |
| Abstracted for hardware diversity, runtime manages most resources                                     | Direct access to hardware layout; app can manage resources and processing                                              |
| Provides higher-level modules via third-party libraries (for example, Simple DirectMedia Layer (SDL)) | Higher-level modules, like Direct2D, are built upon lower modules to simplify development for Windows apps             |
| Hardware vendors differentiate via extensions                                                         | Microsoft adds optional features to the API in a generic way so they aren't specific to any particular hardware vendor |

 

GLSL and HLSL generally differ in these ways:

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">GLSL</th>
<th align="left">HLSL</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">Procedural, step-centric (C like)</td>
<td align="left">Object oriented, data-centric (C++ like)</td>
</tr>
<tr class="even">
<td align="left">Shader compilation integrated into the graphics API</td>
<td align="left">The HLSL compiler <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-part1">compiles the shader</a> to an intermediate binary representation before Direct3D passes it to the driver.
<div class="alert">
<strong>Note</strong>  This binary representation is hardware independent. It's typically compiled at app build time, rather than at app run time.
</div>
<div>
 
</div></td>
</tr>
<tr class="odd">
<td align="left"><a href="#porting-glsl-variables-to-hlsl">Variable</a> storage modifiers</td>
<td align="left">Constant buffers and data transfers via input layout declarations</td>
</tr>
<tr class="even">
<td align="left"><p><a href="#porting-glsl-types-to-hlsl">Types</a></p>
<p>Typical vector type: vec2/3/4</p>
<p>lowp, mediump, highp</p></td>
<td align="left"><p>Typical vector type: float2/3/4</p>
<p>min10float, min16float</p></td>
</tr>
<tr class="odd">
<td align="left">texture2D [Function]</td>
<td align="left"><a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-to-sample">texture.Sample</a> [datatype.Function]</td>
</tr>
<tr class="even">
<td align="left">sampler2D [datatype]</td>
<td align="left"><a href="/windows/desktop/direct3dhlsl/sm5-object-texture2d">Texture2D</a> [datatype]</td>
</tr>
<tr class="odd">
<td align="left">Row-major matrices (default)</td>
<td align="left">Column-major matrices (default)
<div class="alert">
<strong>Note</strong>   Use the <strong>row_major</strong> type-modifier to change the layout for one variable. For more info, see <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-variable-syntax">Variable Syntax</a>. You can also specify a compiler flag or a pragma to change the global default.
</div>
<div>
 
</div></td>
</tr>
<tr class="even">
<td align="left">Fragment shader</td>
<td align="left">Pixel shader</td>
</tr>
</tbody>
</table>

 

> **Note**  HLSL has textures and samplers as two separate objects. In GLSL, like Direct3D 9, the texture binding is part of the sampler state.

 

In GLSL, you present much of the OpenGL state as pre-defined global variables. For example, with GLSL, you use the **gl\_Position** variable to specify vertex position and the **gl\_FragColor** variable to specify fragment color. In HLSL, you pass Direct3D state explicitly from the app code to the shader. For example, with Direct3D and HLSL, the input to the vertex shader must match the data format in the vertex buffer, and the structure of a constant buffer in the app code must match the structure of a constant buffer ([cbuffer](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-constants)) in shader code.

## Porting GLSL variables to HLSL


In GLSL, you apply modifiers (qualifiers) to a global shader variable declaration to give that variable a specific behavior in your shaders. In HLSL, you don’t need these modifiers because you define the flow of the shader with the arguments that you pass to your shader and that you return from your shader.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">GLSL variable behavior</th>
<th align="left">HLSL equivalent</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><strong>uniform</strong></p>
<p>You pass a uniform variable from the app code into either or both vertex and fragment shaders. You must set the values of all uniforms before you draw any triangles with those shaders so their values stay the same throughout the drawing of a triangle mesh. These values are uniform. Some uniforms are set for the entire frame and others uniquely to one particular vertex-pixel shader pair.</p>
<p>Uniform variables are per-polygon variables.</p></td>
<td align="left"><p>Use constant buffer.</p>
<p>See <a href="/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-constant-how-to">How to: Create a Constant Buffer</a> and <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-constants">Shader Constants</a>.</p></td>
</tr>
<tr class="even">
<td align="left"><p><strong>varying</strong></p>
<p>You initialize a varying variable inside the vertex shader and pass it through to an identically named varying variable in the fragment shader. Because the vertex shader only sets the value of the varying variables at each vertex, the rasterizer interpolates those values (in a perspective-correct manner) to generate per fragment values to pass into the fragment shader. These variables vary across each triangle.</p></td>
<td align="left">Use the structure that you return from your vertex shader as the input to your pixel shader. Make sure the semantic values match.</td>
</tr>
<tr class="odd">
<td align="left"><p><strong>attribute</strong></p>
<p>An attribute is a part of the description of a vertex that you pass from the app code to the vertex shader alone. Unlike a uniform, you set each attribute’s value for each vertex, which, in turn, allows each vertex to have a different value. Attribute variables are per-vertex variables.</p></td>
<td align="left"><p>Define a vertex buffer in your Direct3D app code and match it to the vertex input defined in the vertex shader. Optionally, define an index buffer. See <a href="/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-vertex-how-to">How to: Create a Vertex Buffer</a> and <a href="/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-index-how-to">How to: Create an Index Buffer</a>.</p>
<p>Create an input layout in your Direct3D app code and match semantic values with those in the vertex input. See <a href="/windows/desktop/direct3d11/d3d10-graphics-programming-guide-input-assembler-stage-getting-started">Create the input layout</a>.</p></td>
</tr>
<tr class="even">
<td align="left"><p><strong>const</strong></p>
<p>Constants that are compiled into the shader and never change.</p></td>
<td align="left">Use a <strong>static const</strong>. <strong>static</strong> means the value isn't exposed to constant buffers, <strong>const</strong> means the shader can't change the value. So, the value is known at compile time based on its initializer.</td>
</tr>
</tbody>
</table>

 

In GLSL, variables without modifiers are just ordinary global variables that are private to each shader.

When you pass data to textures ([Texture2D](/windows/desktop/direct3dhlsl/sm5-object-texture2d) in HLSL) and their associated samplers ([SamplerState](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-sampler) in HLSL), you typically declare them as global variables in the pixel shader.

## Porting GLSL types to HLSL


Use this table to port your GLSL types to HLSL.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">GLSL type</th>
<th align="left">HLSL type</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">scalar types: float, int, bool</td>
<td align="left"><p>scalar types: float, int, bool</p>
<p>also, uint, double</p>
<p>For more info, see <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-scalar">Scalar Types</a>.</p></td>
</tr>
<tr class="even">
<td align="left"><p>vector type</p>
<ul>
<li>floating-point vector: vec2, vec3, vec4</li>
<li>Boolean vector: bvec2, bvec3, bvec4</li>
<li>signed integer vector: ivec2, ivec3, ivec4</li>
</ul></td>
<td align="left"><p>vector type</p>
<ul>
<li>float2, float3, float4, and float1</li>
<li>bool2, bool3, bool4, and bool1</li>
<li>int2, int3, int4, and int1</li>
<li><p>These types also have vector expansions similar to float, bool, and int:</p>
<ul>
<li>uint</li>
<li>min10float, min16float</li>
<li>min12int, min16int</li>
<li>min16uint</li>
</ul></li>
</ul>
<p>For more info, see <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-vector">Vector Type</a> and <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-appendix-keywords">Keywords</a>.</p>
<p>vector is also type defined as float4 (typedef vector &lt;float, 4&gt; vector;). For more info, see <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-user-defined">User-Defined Type</a>.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>matrix type</p>
<ul>
<li>mat2: 2x2 float matrix</li>
<li>mat3: 3x3 float matrix</li>
<li>mat4: 4x4 float matrix</li>
</ul></td>
<td align="left"><p>matrix type</p>
<ul>
<li>float2x2</li>
<li>float3x3</li>
<li>float4x4</li>
<li>also, float1x1, float1x2, float1x3, float1x4, float2x1, float2x3, float2x4, float3x1, float3x2, float3x4, float4x1, float4x2, float4x3</li>
<li><p>These types also have matrix expansions similar to float:</p>
<ul>
<li>int, uint, bool</li>
<li>min10float, min16float</li>
<li>min12int, min16int</li>
<li>min16uint</li>
</ul></li>
</ul>
<p>You can also use the <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-matrix">matrix type</a> to define a matrix.</p>
<p>For example: matrix &lt;float, 2, 2&gt; fMatrix = {0.0f, 0.1, 2.1f, 2.2f};</p>
<p>matrix is also type defined as float4x4 (typedef matrix &lt;float, 4, 4&gt; matrix;). For more info, see <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-user-defined">User-Defined Type</a>.</p></td>
</tr>
<tr class="even">
<td align="left"><p>precision qualifiers for float, int, sampler</p>
<ul>
<li><p>highp</p>
<p>This qualifier provides minimum precision requirements that are greater than that provided by min16float and less than a full 32-bit float. Equivalent in HLSL is:</p>
<p>highp float -&gt; float</p>
<p>highp int -&gt; int</p></li>
<li><p>mediump</p>
<p>This qualifier applied to float and int is equivalent to min16float and min12int in HLSL. Minimum 10 bits of mantissa, not like min10float.</p></li>
<li><p>lowp</p>
<p>This qualifier applied to float provides a floating point range of -2 to 2. Equivalent to min10float in HLSL.</p></li>
</ul></td>
<td align="left"><p>precision types</p>
<ul>
<li>min16float: minimum 16-bit floating point value</li>
<li><p>min10float</p>
<p>Minimum fixed-point signed 2.8 bit value (2 bits of whole number and 8 bits fractional component). The 8-bit fractional component can be inclusive of 1 instead of exclusive to give it the full inclusive range of -2 to 2.</p></li>
<li>min16int: minimum 16-bit signed integer</li>
<li><p>min12int: minimum 12-bit signed integer</p>
<p>This type is for 10Level9 (<a href="/windows/desktop/direct3d11/overviews-direct3d-11-devices-downlevel-intro">9_x feature levels</a>) in which integers are represented by floating point numbers. This is the precision you can get when you emulate an integer with a 16-bit floating point number.</p></li>
<li>min16uint: minimum 16-bit unsigned integer</li>
</ul>
<p>For more info, see <a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-scalar">Scalar Types</a> and <a href="/windows/desktop/direct3dhlsl/using-hlsl-minimum-precision">Using HLSL minimum precision</a>.</p></td>
</tr>
<tr class="odd">
<td align="left">sampler2D</td>
<td align="left"><a href="/windows/desktop/direct3dhlsl/sm5-object-texture2d">Texture2D</a></td>
</tr>
<tr class="even">
<td align="left">samplerCube</td>
<td align="left"><a href="/windows/desktop/direct3dhlsl/dx-graphics-hlsl-to-type">TextureCube</a></td>
</tr>
</tbody>
</table>

 

## Porting GLSL pre-defined global variables to HLSL


Use this table to port GLSL pre-defined global variables to HLSL.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">GLSL pre-defined global variable</th>
<th align="left">HLSL semantics</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><strong>gl_Position</strong></p>
<p>This variable is type <strong>vec4</strong>.</p>
<p>Vertex position</p>
<p>for example - gl_Position = position;</p></td>
<td align="left"><p>SV_Position</p>
<p>POSITION in Direct3D 9</p>
<p>This semantic is type <strong>float4</strong>.</p>
<p>Vertex shader output</p>
<p>Vertex position</p>
<p>for example - float4 vPosition : SV_Position;</p></td>
</tr>
<tr class="even">
<td align="left"><p><strong>gl_PointSize</strong></p>
<p>This variable is type <strong>float</strong>.</p>
<p>Point size</p></td>
<td align="left"><p>PSIZE</p>
<p>No meaning unless you target Direct3D 9</p>
<p>This semantic is type <strong>float</strong>.</p>
<p>Vertex shader output</p>
<p>Point size</p></td>
</tr>
<tr class="odd">
<td align="left"><p><strong>gl_FragColor</strong></p>
<p>This variable is type <strong>vec4</strong>.</p>
<p>Fragment color</p>
<p>for example - gl_FragColor = vec4(colorVarying, 1.0);</p></td>
<td align="left"><p>SV_Target</p>
<p>COLOR in Direct3D 9</p>
<p>This semantic is type <strong>float4</strong>.</p>
<p>Pixel shader output</p>
<p>Pixel color</p>
<p>for example - float4 Color[4] : SV_Target;</p></td>
</tr>
<tr class="even">
<td align="left"><p><strong>gl_FragData[n]</strong></p>
<p>This variable is type <strong>vec4</strong>.</p>
<p>Fragment color for color attachment n</p></td>
<td align="left"><p>SV_Target[n]</p>
<p>This semantic is type <strong>float4</strong>.</p>
<p>Pixel shader output value that is stored in n render target, where 0 &lt;= n &lt;= 7.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><strong>gl_FragCoord</strong></p>
<p>This variable is type <strong>vec4</strong>.</p>
<p>Fragment position within frame buffer</p></td>
<td align="left"><p>SV_Position</p>
<p>Not available in Direct3D 9</p>
<p>This semantic is type <strong>float4</strong>.</p>
<p>Pixel shader input</p>
<p>Screen space coordinates</p>
<p>for example - float4 screenSpace : SV_Position</p></td>
</tr>
<tr class="even">
<td align="left"><p><strong>gl_FrontFacing</strong></p>
<p>This variable is type <strong>bool</strong>.</p>
<p>Determines whether fragment belongs to a front-facing primitive.</p></td>
<td align="left"><p>SV_IsFrontFace</p>
<p>VFACE in Direct3D 9</p>
<p>SV_IsFrontFace is type <strong>bool</strong>.</p>
<p>VFACE is type <strong>float</strong>.</p>
<p>Pixel shader input</p>
<p>Primitive facing</p></td>
</tr>
<tr class="odd">
<td align="left"><p><strong>gl_PointCoord</strong></p>
<p>This variable is type <strong>vec2</strong>.</p>
<p>Fragment position within a point (point rasterization only)</p></td>
<td align="left"><p>SV_Position</p>
<p>VPOS in Direct3D 9</p>
<p>SV_Position is type <strong>float4</strong>.</p>
<p>VPOS is type <strong>float2</strong>.</p>
<p>Pixel shader input</p>
<p>The pixel or sample position in screen space</p>
<p>for example - float4 pos : SV_Position</p></td>
</tr>
<tr class="even">
<td align="left"><p><strong>gl_FragDepth</strong></p>
<p>This variable is type <strong>float</strong>.</p>
<p>Depth buffer data</p></td>
<td align="left"><p>SV_Depth</p>
<p>DEPTH in Direct3D 9</p>
<p>SV_Depth is type <strong>float</strong>.</p>
<p>Pixel shader output</p>
<p>Depth buffer data</p></td>
</tr>
</tbody>
</table>

 

You use semantics to specify position, color, and so on for vertex shader input and pixel shader input. You must match the semantics values in the input layout with the vertex shader input. For examples, see [Examples of porting GLSL variables to HLSL](#examples-of-porting-glsl-variables-to-hlsl). For more info about the HLSL semantics, see [Semantics](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-semantics).

## Examples of porting GLSL variables to HLSL


Here we show examples of using GLSL variables in OpenGL/GLSL code and then the equivalent example in Direct3D/HLSL code.

### Uniform, attribute, and varying in GLSL

OpenGL app code

``` syntax
// Uniform values can be set in app code and then processed in the shader code.
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

// Incoming position of vertex
attribute vec4 position;
 
// Incoming color for the vertex
attribute vec3 color;
 
// The varying variable tells the shader pipeline to pass it  
// on to the fragment shader.
varying vec3 colorVarying;
```

GLSL vertex shader code

``` syntax
//The shader entry point is the main method.
void main()
{
colorVarying = color; //Use the varying variable to pass the color to the fragment shader
gl_Position = position; //Copy the position to the gl_Position pre-defined global variable
}
```

GLSL fragment shader code

``` syntax
void main()
{
//Pad the colorVarying vec3 with a 1.0 for alpha to create a vec4 color
//and assign that color to the gl_FragColor pre-defined global variable
//This color then becomes the fragment's color.
gl_FragColor = vec4(colorVarying, 1.0);
}
```

### Constant buffers and data transfers in HLSL

Here is an example of how you pass data to the HLSL vertex shader that then flows through to the pixel shader. In your app code, define a vertex and a constant buffer. Then, in your vertex shader code, define the constant buffer as a [cbuffer](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-constants) and store the per-vertex data and the pixel shader input data. Here we use structures called **VertexShaderInput** and **PixelShaderInput**.

Direct3D app code

```cpp
struct ConstantBuffer
{
    XMFLOAT4X4 model;
    XMFLOAT4X4 view;
    XMFLOAT4X4 projection;
};
struct SimpleCubeVertex
{
    XMFLOAT3 pos;   // position
    XMFLOAT3 color; // color
};

 // Create an input layout that matches the layout defined in the vertex shader code.
 const D3D11_INPUT_ELEMENT_DESC basicVertexLayoutDesc[] =
 {
     { "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0,  0, D3D11_INPUT_PER_VERTEX_DATA, 0 },
     { "COLOR",    0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0 },
 };

// Create vertex and index buffers that define a geometry.
```

HLSL vertex shader code

``` syntax
cbuffer ModelViewProjectionCB : register( b0 )
{
    matrix model; 
    matrix view;
    matrix projection;
};
// The POSITION and COLOR semantics must match the semantics in the input layout Direct3D app code.
struct VertexShaderInput
{
    float3 pos : POSITION; // Incoming position of vertex 
    float3 color : COLOR; // Incoming color for the vertex
};

struct PixelShaderInput
{
    float4 pos : SV_Position; // Copy the vertex position.
    float4 color : COLOR; // Pass the color to the pixel shader.
};

PixelShaderInput main(VertexShaderInput input)
{
    PixelShaderInput vertexShaderOutput;

    // shader source code

    return vertexShaderOutput;
}
```

HLSL pixel shader code

``` syntax
// Collect input from the vertex shader. 
// The COLOR semantic must match the semantic in the vertex shader code.
struct PixelShaderInput
{
    float4 pos : SV_Position;
    float4 color : COLOR; // Color for the pixel
};

// Set the pixel color value for the renter target. 
float4 main(PixelShaderInput input) : SV_Target
{
    return input.color;
}
```

## Examples of porting OpenGL rendering code to Direct3D


Here we show an example of rendering in OpenGL ES 2.0 code and then the equivalent example in Direct3D 11 code.

OpenGL rendering code

``` syntax
// Bind shaders to the pipeline. 
// Both vertex shader and fragment shader are in a program.
glUseProgram(m_shader->getProgram());
 
// Input assembly 
// Get the position and color attributes of the vertex.

m_positionLocation = glGetAttribLocation(m_shader->getProgram(), "position");
glEnableVertexAttribArray(m_positionLocation);

m_colorLocation = glGetAttribColor(m_shader->getProgram(), "color");
glEnableVertexAttribArray(m_colorLocation);
 
// Bind the vertex buffer object to the input assembler.
glBindBuffer(GL_ARRAY_BUFFER, m_geometryBuffer);
glVertexAttribPointer(m_positionLocation, 4, GL_FLOAT, GL_FALSE, 0, NULL);
glBindBuffer(GL_ARRAY_BUFFER, m_colorBuffer);
glVertexAttribPointer(m_colorLocation, 3, GL_FLOAT, GL_FALSE, 0, NULL);
 
// Draw a triangle with 3 vertices.
glDrawArray(GL_TRIANGLES, 0, 3);
```

Direct3D rendering code

```cpp
// Bind the vertex shader and pixel shader to the pipeline.
m_d3dDeviceContext->VSSetShader(vertexShader.Get(),nullptr,0);
m_d3dDeviceContext->PSSetShader(pixelShader.Get(),nullptr,0);
 
// Declare the inputs that the shaders expect.
m_d3dDeviceContext->IASetInputLayout(inputLayout.Get());
m_d3dDeviceContext->IASetVertexBuffers(0, 1, vertexBuffer.GetAddressOf(), &stride, &offset);

// Set the primitive's topology.
m_d3dDeviceContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

// Draw a triangle with 3 vertices. triangleVertices is an array of 3 vertices.
m_d3dDeviceContext->Draw(ARRAYSIZE(triangleVertices),0);
```

## Related topics


* [Port from OpenGL ES 2.0 to Direct3D 11](port-from-opengl-es-2-0-to-directx-11-1.md)

 

 