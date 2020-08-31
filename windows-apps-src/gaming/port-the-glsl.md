---
title: Port the GLSL
description: Once you've moved over the code that creates and configures your buffers and shader objects, it's time to port the code inside those shaders from OpenGL ES 2.0's GL Shader Language (GLSL) to Direct3D 11's High-level Shader Language (HLSL).
ms.assetid: 0de06c51-8a34-dc68-6768-ea9f75dc57ee
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, glsl, port
ms.localizationpriority: medium
---
# Port the GLSL




**Important APIs**

-   [HLSL Semantics](/windows/desktop/direct3dhlsl/dcl-usage---ps)
-   [Shader Constants (HLSL)](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-constants)

Once you've moved over the code that creates and configures your buffers and shader objects, it's time to port the code inside those shaders from OpenGL ES 2.0's GL Shader Language (GLSL) to Direct3D 11's High-level Shader Language (HLSL).

In OpenGL ES 2.0, shaders return data after execution using intrinsics such as **gl\_Position**, **gl\_FragColor**, or **gl\_FragData\[n\]** (where n is the index for a specific render target). In Direct3D, there are no specific intrinsics, and the shaders return data as the return type of their respective main() functions.

Data that you want interpolated between shader stages, such as the vertex position or normal, is handled through the use of the **varying** declaration. However, Direct3D doesn't have this declaration; rather, any data that you want passed between shader stages must be marked with an [HLSL semantic](/windows/desktop/direct3dhlsl/dcl-usage---ps). The specific semantic chosen indicates the purpose of the data, and is. For example, you'd declare vertex data that you want interpolated between the fragment shader as:

`float4 vertPos : POSITION;`

or

`float4 vertColor : COLOR;`

Where POSITION is the semantic used to indicate vertex position data. POSITION is also a special case, since after interpolation, it cannot be accessed by the pixel shader. Therefore, you must specify input to the pixel shader with SV\_POSITION and the interpolated vertex data will be placed in that variable.

`float4 position : SV_POSITION;`

Semantics can be declared on the body (main) methods of shaders. For pixel shaders, SV\_TARGET\[n\], which indicates a render target, is required on the body method. (SV\_TARGET without a numeric suffix defaults to render target index 0.)

Also note that vertex shaders are required to output the SV\_POSITION system value semantic. This semantic resolves the vertex position data to coordinate values where x is between -1 and 1, y is between -1 and 1, z is divided by the original homogeneous coordinate w value (z/w), and w is 1 divided by the original w value (1/w). Pixel shaders use the SV\_POSITION system value semantic to retrieve the pixel location on the screen, where x is between 0 and the render target width and y is between 0 and the render target height (each offset by 0.5). Feature level 9\_x pixel shaders cannot read from the SV\_POSITION value.

Constant buffers must be declared with **cbuffer** and be associated with a specific starting register for lookup.

Direct3D 11: An HLSL constant buffer declaration

``` syntax
cbuffer ModelViewProjectionConstantBuffer : register(b0)
{
  matrix mvp;
};
```

Here, the constant buffer uses register b0 to hold the packed buffer. All registers are referred to in the form b\#. For more information on the HLSL implementation of constant buffers, registers, and data packing, read [Shader Constants (HLSL)](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-constants).

Instructions
------------
### Step 1: Port the vertex shader

In our simple OpenGL ES 2.0 example, the vertex shader has three inputs: a constant model-view-projection 4x4 matrix, and two 4-coordinate vectors. These two vectors contain the vertex position and its color. The shader transforms the position vector to perspective coordinates and assigns it to the gl\_Position intrinsic for rasterization. The vertex color is copied to a varying variable for interpolation during rasterization, as well.

OpenGL ES 2.0: Vertex shader for the cube object (GLSL)

``` syntax
uniform mat4 u_mvpMatrix; 
attribute vec4 a_position;
attribute vec4 a_color;
varying vec4 destColor;

void main()
{           
  gl_Position = u_mvpMatrix * a_position;
  destColor = a_color;
}
```

Now, in Direct3D, the constant model-view-projection matrix is contained in a constant buffer packed at register b0, and the vertex position and color are specifically marked with the appropriate respective HLSL semantics: POSITION and COLOR. Since our input layout indicates a specific arrangement of these two vertex values, you create a struct to hold them and declare it as the type for the input parameter on the shader body function (main). (You could also specify them as two individual parameters, but that could get cumbersome.) You also specify an output type for this stage, which contains the interpolated position and color, and declare it as the return value for the body function of the vertex shader.

Direct3D 11: Vertex shader for the cube object (HLSL)

``` syntax
cbuffer ModelViewProjectionConstantBuffer : register(b0)
{
  matrix mvp;
};

// Per-vertex data used as input to the vertex shader.
struct VertexShaderInput
{
  float3 pos : POSITION;
  float3 color : COLOR;
};

// Per-vertex color data passed through the pixel shader.
struct PixelShaderInput
{
  float3 pos : SV_POSITION;
  float3 color : COLOR;
};

PixelShaderInput main(VertexShaderInput input)
{
  PixelShaderInput output;
  float4 pos = float4(input.pos, 1.0f); // add the w-coordinate

  pos = mul(mvp, projection);
  output.pos = pos;

  output.color = input.color;

  return output;
}
```

The output data type, PixelShaderInput, is populated during rasterization and provided to the fragment (pixel) shader.

### Step 2: Port the fragment shader

Our example fragment shader in GLSL is extremely simple: provide the gl\_FragColor intrinsic with the interpolated color value. OpenGL ES 2.0 will write it to the default render target.

OpenGL ES 2.0: Fragment shader for the cube object (GLSL)

``` syntax
varying vec4 destColor;

void main()
{
  gl_FragColor = destColor;
} 
```

Direct3D is almost as simple. The only significant difference is that the body function of the pixel shader must return a value. Since the color is a 4-coordinate (RGBA) float value, you indicate float4 as the return type, and then specify the default render target as the SV\_TARGET system value semantic.

Direct3D 11: Pixel shader for the cube object (HLSL)

``` syntax
struct PixelShaderInput
{
  float4 pos : SV_POSITION;
  float3 color : COLOR;
};


float4 main(PixelShaderInput input) : SV_TARGET
{
  return float4(input.color, 1.0f);
}
```

The color for the pixel at the position is written to the render target. Now, let's see how to display the contents of that render target in [Draw to the screen](draw-to-the-screen.md)!

## Previous step


[Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md)
Next step
---------
[Draw to the screen](draw-to-the-screen.md)
Remarks
-------
Understanding HLSL semantics and the packing of constant buffers can save you a bit of a debugging headache, as well as provide optimization opportunities. If you get a chance, read through [Variable Syntax (HLSL)](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-variable-syntax), [Introduction to Buffers in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-intro), and [How to: Create a Constant Buffer](/windows/desktop/direct3d11/overviews-direct3d-11-resources-buffers-constant-how-to). If not, though, here's a few starting tips to keep in mind about semantics and constant buffers:

-   Always double check your renderer's Direct3D configuration code to make sure that the structures for your constant buffers match the cbuffer struct declarations in your HLSL, and that the component scalar types match across both declarations.
-   In your renderer's C++ code, use [DirectXMath](/windows/desktop/dxmath/directxmath-portal) types in your constant buffer declarations to ensure proper data packing.
-   The best way to efficiently use constant buffers is to organize shader variables into constant buffers based on their frequency of update. For example, if you have some uniform data that is updated once per frame, and other uniform data that is updated only when the camera moves, consider separating that data into two separate constant buffers.
-   Semantics that you have forgotten to apply or which you have applied incorrectly will be your earliest source of shader compilation (FXC) errors. Double-check them! The docs can be a bit confusing, as many older pages and samples refer to different versions of HLSL semantics prior to Direct3D 11.
-   Make sure you know which Direct3D feature level you are targeting for each shader. The semantics for feature level 9\_\* are different from those for 11\_1.
-   The SV\_POSITION semantic resolves the associated post-interpolation position data to coordinate values where x is between 0 and the render target width, y is between 0 and the render target height, z is divided by the original homogeneous coordinate w value (z/w), and w is 1 divided by the original w value (1/w).

## Related topics


[How to: port a simple OpenGL ES 2.0 renderer to Direct3D 11](port-a-simple-opengl-es-2-0-renderer-to-directx-11-1.md)

[Port the shader objects](port-the-shader-config.md)

[Port the vertex buffers and data](port-the-vertex-buffers-and-data-config.md)

[Draw to the screen](draw-to-the-screen.md)

 

 