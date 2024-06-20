---
title: Compare the OpenGL ES 2.0 shader pipeline to Direct3D
description: Conceptually, the Direct3D 11 shader pipeline is very similar to the one in OpenGL ES 2.0.
ms.assetid: 3678a264-e3f9-72d2-be91-f79cd6f7c4ca
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, opengl, direct3d, shader pipeline
ms.localizationpriority: medium
---
# Compare the OpenGL ES 2.0 shader pipeline to Direct3D




**Important APIs**

-   [Input-Assembler Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-input-assembler-stage)
-   [Vertex-Shader Stage](/previous-versions/bb205146(v=vs.85))
-   [Pixel-Shader Stage](/previous-versions/bb205146(v=vs.85))

Conceptually, the Direct3D 11 shader pipeline is very similar to the one in OpenGL ES 2.0. In terms of API design, however, the major components for creating and managing the shader stages are parts of two primary interfaces, [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) and [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1). This topic attempts to map common OpenGL ES 2.0 shader pipeline API patterns to the Direct3D 11 equivalents in these interfaces.

## Reviewing the Direct3D 11 shader pipeline


The shader objects are created with methods on the [**ID3D11Device1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1) interface, such as [**ID3D11Device1::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader) and [**ID3D11Device1::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader).

The Direct3D 11 graphics pipeline is managed by instances of the [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) interface, and has the following stages:

-   [Input-Assembler Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-input-assembler-stage). The input-assembler stage supplies data (triangles, lines and points) to the pipeline. [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) methods that support this stage are prefixed with "IA".
-   [Vertex-Shader Stage](/previous-versions/bb205146(v=vs.85)) - The vertex-shader stage processes vertices, typically performing operations such as transformations, skinning, and lighting. A vertex shader always takes a single input vertex and produces a single output vertex. [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) methods that support this stage are prefixed with "VS".
-   [Stream-Output Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-output-stream-stage) - The stream-output stage streams primitive data from the pipeline to memory on its way to the rasterizer. Data can be streamed out and/or passed into the rasterizer. Data streamed out to memory can be recirculated back into the pipeline as input data or read-back from the CPU. [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) methods that support this stage are prefixed with "SO".
-   [Rasterizer Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-rasterizer-stage) - The rasterizer clips primitives, prepares primitives for the pixel shader, and determines how to invoke pixel shaders. You can disable rasterization by telling the pipeline there is no pixel shader (set the pixel shader stage to NULL with [**ID3D11DeviceContext::PSSetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-pssetshader)), and disabling depth and stencil testing (set DepthEnable and StencilEnable to FALSE in [**D3D11\_DEPTH\_STENCIL\_DESC**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_depth_stencil_desc)). While disabled, rasterization-related pipeline counters will not update.
-   [Pixel-Shader Stage](/previous-versions/bb205146(v=vs.85)) - The pixel-shader stage receives interpolated data for a primitive and generates per-pixel data such as color. [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) methods that support this stage are prefixed with "PS".
-   [Output-Merger Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-output-merger-stage) - The output-merger stage combines various types of output data (pixel shader values, depth and stencil information) with the contents of the render target and depth/stencil buffers to generate the final pipeline result. [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) methods that support this stage are prefixed with "OM".

(There are also stages for geometry shaders, hull shaders, tesselators, and domain shaders, but since they have no analogues in OpenGL ES 2.0, we won't discuss them here.)
For a complete list of the methods for these stages, refer to the [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) and [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) reference pages. **ID3D11DeviceContext1** extends **ID3D11DeviceContext** for Direct3D 11.

## Creating a shader


In Direct3D, shader resources are not created before compiling and loading them; rather, the resource is created when the HLSLis loaded. Therefore, there is no directly analogous function to glCreateShader, which creates an initialized shader resource of a specific type (such as GL\_VERTEX\_SHADER or GL\_FRAGMENT\_SHADER). Rather, shaders are created after the HLSL is loaded with specific functions like [**ID3D11Device1::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader) and [**ID3D11Device1::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader), and which take the type and the compiled HLSL as parameters.

| OpenGL ES 2.0  | Direct3D 11                                                                                                                                                                                                                                                             |
|----------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| glCreateShader | Call [**ID3D11Device1::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader) and [**ID3D11Device1::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader) after successfully loading the compiled shader object, passing them the CSO as a buffer. |

 

## Compiling a shader


Direct3D shaders must be precompiled as Compiled Shader Object (.cso) files in Universal Windows Platform (UWP) apps and loaded using one of the Windows Runtime file APIs. (Desktop apps can compile the shaders from text files or string at run-time.) The CSO files are built from any .hlsl files that are part of your Microsoft Visual Studio project, and retain the same names, only with a .cso file extension. Ensure that they are included with your package when you ship!

| OpenGL ES 2.0                          | Direct3D 11                                                                                                                                                                   |
|----------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| glCompileShader                        | N/A. Compile the shaders to .cso files in Visual Studio and include them in your package.                                                                                     |
| Using glGetShaderiv for compile status | N/A. See the compilation output from Visual Studio's FX Compiler (FXC) if there are errors in compilation. If compilation is successful, a corresponding CSO file is created. |

 

## Loading a shader


As noted in the section on creating a shader, Direct3D 11 creates the shader when the corresponding CSO file is loaded into a buffer and passed to one of the methods in the following table.

| OpenGL ES 2.0 | Direct3D 11                                                                                                                                                                                                                           |
|---------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ShaderSource  | Call [**ID3D11Device1::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader) and [**ID3D11Device1::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader) after successfully loading the compiled shader object. |

 

## Setting up the pipeline


OpenGL ES 2.0 has the "shader program" object, which contains multiple shaders for execution. Individual shaders are attached to the shader program object. However, in Direct3D 11, you work with the rendering context ([**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1)) directly and create shaders on it.

| OpenGL ES 2.0   | Direct3D 11                                                                                   |
|-----------------|-----------------------------------------------------------------------------------------------|
| glCreateProgram | N/A. Direct3D 11 does not use the shader program object abstraction.                          |
| glLinkProgram   | N/A. Direct3D 11 does not use the shader program object abstraction.                          |
| glUseProgram    | N/A. Direct3D 11 does not use the shader program object abstraction.                          |
| glGetProgramiv  | Use the reference you created to [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1). |

 

Create an instance of [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1) and [**ID3D11Device1**](/windows/desktop/api/d3d11_2/nn-d3d11_2-id3d11device2) with the static [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) method.

``` syntax
Microsoft::WRL::ComPtr<ID3D11Device1>          m_d3dDevice;
Microsoft::WRL::ComPtr<ID3D11DeviceContext1>  m_d3dContext;

// ...

D3D11CreateDevice(
  nullptr, // Specify nullptr to use the default adapter.
  D3D_DRIVER_TYPE_HARDWARE,
  nullptr,
  creationFlags, // Set set debug and Direct2D compatibility flags.
  featureLevels, // List of feature levels this app can support.
  ARRAYSIZE(featureLevels),
  D3D11_SDK_VERSION, // Always set this to D3D11_SDK_VERSION for UWP apps.
  &device, // Returns the Direct3D device created.
  &m_featureLevel, // Returns feature level of device created.
  &m_d3dContext // Returns the device's immediate context.
);
```

## Setting the viewport(s)


Setting a viewport in Direct3D 11 is very similar to how you set a viewport in OpenGL ES 2.0. In Direct3D 11, call [**ID3D11DeviceContext::RSSetViewports**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-rssetviewports) with a configured [**CD3D11\_VIEWPORT**](/previous-versions/windows/desktop/legacy/jj151722(v=vs.85)).

Direct3D 11: Setting a viewport.

``` syntax
CD3D11_VIEWPORT viewport(
        0.0f,
        0.0f,
        m_d3dRenderTargetSize.Width,
        m_d3dRenderTargetSize.Height
        );
m_d3dContext->RSSetViewports(1, &viewport);
```

| OpenGL ES 2.0 | Direct3D 11                                                                                                                                  |
|---------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| glViewport    | [**CD3D11\_VIEWPORT**](/previous-versions/windows/desktop/legacy/jj151722(v=vs.85)), [**ID3D11DeviceContext::RSSetViewports**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-rssetviewports) |

 

## Configuring the vertex shaders


Configuring a vertex shader in Direct3D 11 is done when the shader is loaded. Uniforms are passed as constant buffers using [**ID3D11DeviceContext1::VSSetConstantBuffers1**](/windows/desktop/api/d3d11_1/nf-d3d11_1-id3d11devicecontext1-vssetconstantbuffers1).

| OpenGL ES 2.0                    | Direct3D 11                                                                                               |
|----------------------------------|-----------------------------------------------------------------------------------------------------------|
| glAttachShader                   | [**ID3D11Device1::CreateVertexShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createvertexshader)                       |
| glGetShaderiv, glGetShaderSource | [**ID3D11DeviceContext1::VSGetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-vsgetshader)                       |
| glGetUniformfv, glGetUniformiv   | [**ID3D11DeviceContext1::VSGetConstantBuffers1**](/windows/desktop/api/d3d11_1/nf-d3d11_1-id3d11devicecontext1-vsgetconstantbuffers1). |

 

## Configuring the pixel shaders


Configuring a pixel shader in Direct3D 11 is done when the shader is loaded. Uniforms are passed as constant buffers using [**ID3D11DeviceContext1::PSSetConstantBuffers1.**](/windows/desktop/api/d3d11_1/nf-d3d11_1-id3d11devicecontext1-pssetconstantbuffers1)

| OpenGL ES 2.0                    | Direct3D 11                                                                                               |
|----------------------------------|-----------------------------------------------------------------------------------------------------------|
| glAttachShader                   | [**ID3D11Device1::CreatePixelShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-createpixelshader)                         |
| glGetShaderiv, glGetShaderSource | [**ID3D11DeviceContext1::PSGetShader**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-psgetshader)                       |
| glGetUniformfv, glGetUniformiv   | [**ID3D11DeviceContext1::PSGetConstantBuffers1**](/windows/desktop/api/d3d11_1/nf-d3d11_1-id3d11devicecontext1-psgetconstantbuffers1). |

 

## Generating the final results


When the pipeline completes, you draw the results of the shader stages into the back buffer. In Direct3D 11, just as it is with Open GL ES 2.0, this involves calling a draw command to output the results as a color map in the back buffer, and then sending that back buffer to the display.

| OpenGL ES 2.0  | Direct3D 11                                                                                                                                                                                                                                         |
|----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| glDrawElements | [**ID3D11DeviceContext1::Draw**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-draw), [**ID3D11DeviceContext1::DrawIndexed**](/windows/desktop/api/d3d11/nf-d3d11-id3d11devicecontext-drawindexed) (or other Draw\* methods on [**ID3D11DeviceContext1**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext)). |
| eglSwapBuffers | [**IDXGISwapChain1::Present1**](/windows/desktop/api/dxgi1_2/nf-dxgi1_2-idxgiswapchain1-present1)                                                                                                                                                                              |

 

## Porting GLSL to HLSL


GLSL and HLSL are not very different beyond complex type support and syntax some overall syntax. Many developers find it easiest to port between the two by aliasing common OpenGL ES 2.0 instructions and definitions to their HLSL equivalent. Note that Direct3D uses the Shader Model version to express the feature set of the HLSL supported by a graphics interface; OpenGL has a different version specification for HLSL. The following table attempts to give you some approximate idea of the shader language feature sets defined for Direct3D 11 and OpenGL ES 2.0 in the terms of the other's version.

| Shader language           | GLSL feature version                                                                                                                                                                                                      | Direct3D Shader Model |
|---------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------|
| Direct3D 11 HLSL          | ~4.30.                                                                                                                                                                                                                    | SM 5.0                |
| GLSL ES for OpenGL ES 2.0 | 1.40. Older implementations of GLSL ES for OpenGL ES 2.0 may use 1.10 through 1.30. Check your original code with glGetString(GL\_SHADING\_LANGUAGE\_VERSION) or glGetString(SHADING\_LANGUAGE\_VERSION) to determine it. | ~SM 2.0               |

 

For more details of differences between the two shader languages, as well as common syntax mappings, read the [GLSL-to-HLSL reference](glsl-to-hlsl-reference.md).

## Porting the OpenGL intrinsics to HLSL semantics


Direct3D 11 HLSL semantics are strings that, like a uniform or attribute name, are used to identify a value passed between the app and a shader program. While they can be any of a variety of possible strings, the best practice is to use a string like POSITION or COLOR that indicates the usage. You assign these semantics when you are constructing a constant buffer or buffer input layout. You can also append a number between 0 and 7 to the semantic so that you use separate registers for similar values. For example: COLOR0, COLOR1, COLOR2...

Semantics that are prefixed with "SV\_" are system value semantics that are written to by your shader program; your app itself (running on the CPU) cannot modify them. Typically, these contain values that are inputs or outputs from another shader stage in the graphics pipeline, or are generated entirely by the GPU.

Additionally, SV\_ semantics have different behaviors when they are used to specify input to or output from a shader stage. For example, SV\_POSITION (output) contains the vertex data transformed during the vertex shader stage, and SV\_POSITION (input) contains the pixel position values interpolated during rasterization.

Here are a few mappings for common OpenGL ES 2.0 shader intrinsics:

| OpenGL system value | Use this HLSL Semantic                                                                                                                                                   |
|---------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| gl\_Position        | POSITION(n) for vertex buffer data. SV\_POSITION provides a pixel position to the pixel shader and cannot be written by your app.                                        |
| gl\_Normal          | NORMAL(n) for normal data provided by the vertex buffer.                                                                                                                 |
| gl\_TexCoord\[n\]   | TEXCOORD(n) for texture UV (ST in some OpenGL documentation) coordinate data supplied to a shader.                                                                       |
| gl\_FragColor       | COLOR(n) for RGBA color data supplied to a shader. Note that it is treated identically to coordinate data; the semantic simply helps you identify that it is color data. |
| gl\_FragData\[n\]   | SV\_Target\[n\] for writing from a pixel shader to a target texture or other pixel buffer.                                                                               |

 

The method by which you code for semantics is not the same as using intrinsics in OpenGL ES 2.0. In OpenGL, you can access many of the intrinsics directly without any configuration or declaration; in Direct3D, you must declare a field in a specific constant buffer to use a particular semantic, or you declare it as the return value for a shader's **main()** method.

Here's an example of a semantic used in a constant buffer definition:

```cpp
struct VertexShaderInput
{
  float3 pos : POSITION;
  float3 color : COLOR0;
};

// The position is interpolated to the pixel value by the system. The per-vertex color data is also interpolated and passed through the pixel shader. 
struct PixelShaderInput
{
  float4 pos : SV_POSITION;
  float3 color : COLOR0;
};
```

This code defines a pair of simple constant buffers

And here's an example of a semantic used to define the value returned by a fragment shader:

```cpp
// A pass-through for the (interpolated) color data.
float4 main(PixelShaderInput input) : SV_TARGET
{
  return float4(input.color,1.0f);
}
```

In this case, SV\_TARGET is the location of the render target that the pixel color (defined as a vector with four float values) is written to when the shader completes execution.

For more details on the use of semantics with Direct3D, read [HLSL Semantics](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-semantics).

 

 