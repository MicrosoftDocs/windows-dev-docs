---
title: Render the scene with depth testing
description: Create a shadow effect by adding depth testing to your vertex (or geometry) shader and your pixel shader.
ms.assetid: bf496dfb-d7f5-af6b-d588-501164608560
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, rendering, scene, depth testing, direct3d, shadows
ms.localizationpriority: medium
---
# Render the scene with depth testing




Create a shadow effect by adding depth testing to your vertex (or geometry) shader and your pixel shader. Part 3 of [Walkthrough: Implement shadow volumes using depth buffers in Direct3D 11](implementing-depth-buffers-for-shadow-mapping.md).

## Include transformation for light frustum


Your vertex shader needs to compute the transformed light space position for each vertex. Provide the light space model, view, and projection matrices using a constant buffer. You can also use this constant buffer to provide the light position and normal for lighting calculations. The transformed position in light space will be used during the depth test.

```cpp
PixelShaderInput main(VertexShaderInput input)
{
    PixelShaderInput output;
    float4 pos = float4(input.pos, 1.0f);

    // Transform the vertex position into projected space.
    float4 modelPos = mul(pos, model);
    pos = mul(modelPos, view);
    pos = mul(pos, projection);
    output.pos = pos;

    // Transform the vertex position into projected space from the POV of the light.
    float4 lightSpacePos = mul(modelPos, lView);
    lightSpacePos = mul(lightSpacePos, lProjection);
    output.lightSpacePos = lightSpacePos;

    // Light ray
    float3 lRay = lPos.xyz - modelPos.xyz;
    output.lRay = lRay;
    
    // Camera ray
    output.view = eyePos.xyz - modelPos.xyz;

    // Transform the vertex normal into world space.
    float4 norm = float4(input.norm, 1.0f);
    norm = mul(norm, model);
    output.norm = norm.xyz;
    
    // Pass through the color and texture coordinates without modification.
    output.color = input.color;
    output.tex = input.tex;

    return output;
}
```

Next, the pixel shader will use the interpolated light space position provided by the vertex shader to test whether the pixel is in shadow.

## Test whether the position is in the light frustum


First, check that the pixel is in the view frustum of the light by normalizing the X and Y coordinates. If they are both within the range \[0, 1\] then it's possible for the pixel to be in shadow. Otherwise you can skip the depth test. A shader can test for this quickly by calling [Saturate](/windows/desktop/direct3dhlsl/saturate) and comparing the result against the original value.

```cpp
// Compute texture coordinates for the current point's location on the shadow map.
float2 shadowTexCoords;
shadowTexCoords.x = 0.5f + (input.lightSpacePos.x / input.lightSpacePos.w * 0.5f);
shadowTexCoords.y = 0.5f - (input.lightSpacePos.y / input.lightSpacePos.w * 0.5f);
float pixelDepth = input.lightSpacePos.z / input.lightSpacePos.w;

float lighting = 1;

// Check if the pixel texture coordinate is in the view frustum of the 
// light before doing any shadow work.
if ((saturate(shadowTexCoords.x) == shadowTexCoords.x) &&
    (saturate(shadowTexCoords.y) == shadowTexCoords.y) &&
    (pixelDepth > 0))
{
```

## Depth test against the shadow map


Use a sample comparison function (either [SampleCmp](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-to-samplecmp) or [SampleCmpLevelZero](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-to-samplecmplevelzero)) to test the pixel's depth in light space against the depth map. Compute the normalized light space depth value, which is `z / w`, and pass the value to the comparison function. Since we use a LessOrEqual comparison test for the sampler, the intrinsic function returns zero when the comparison test passes; this indicates that the pixel is in shadow.

```cpp
// Use an offset value to mitigate shadow artifacts due to imprecise 
// floating-point values (shadow acne).
//
// This is an approximation of epsilon * tan(acos(saturate(NdotL))):
float margin = acos(saturate(NdotL));
#ifdef LINEAR
// The offset can be slightly smaller with smoother shadow edges.
float epsilon = 0.0005 / margin;
#else
float epsilon = 0.001 / margin;
#endif
// Clamp epsilon to a fixed range so it doesn't go overboard.
epsilon = clamp(epsilon, 0, 0.1);

// Use the SampleCmpLevelZero Texture2D method (or SampleCmp) to sample from 
// the shadow map, just as you would with Direct3D feature level 10_0 and
// higher.  Feature level 9_1 only supports LessOrEqual, which returns 0 if
// the pixel is in the shadow.
lighting = float(shadowMap.SampleCmpLevelZero(
    shadowSampler,
    shadowTexCoords,
    pixelDepth + epsilon
    )
    );
```

## Compute lighting in or out of shadow


If the pixel is not in shadow, the pixel shader should compute direct lighting and add it to the pixel value.

```cpp
return float4(input.color * (ambient + DplusS(N, L, NdotL, input.view)), 1.f);
```

```cpp
float3 DplusS(float3 N, float3 L, float NdotL, float3 view)
{
    const float3 Kdiffuse = float3(.5f, .5f, .4f);
    const float3 Kspecular = float3(.2f, .2f, .3f);
    const float exponent = 3.f;

    // Compute the diffuse coefficient.
    float diffuseConst = saturate(NdotL);

    // Compute the diffuse lighting value.
    float3 diffuse = Kdiffuse * diffuseConst;

    // Compute the specular highlight.
    float3 R = reflect(-L, N);
    float3 V = normalize(view);
    float3 RdotV = dot(R, V);
    float3 specular = Kspecular * pow(saturate(RdotV), exponent);

    return (diffuse + specular);
}
```

Otherwise, the pixel shader should compute the pixel value using ambient lighting.

```cpp
return float4(input.color * ambient, 1.f);
```

In the next part of this walkthrough, learn how to [Support shadow maps on a range of hardware](target-a-range-of-hardware.md).

 

 