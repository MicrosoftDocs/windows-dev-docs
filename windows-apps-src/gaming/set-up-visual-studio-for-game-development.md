---
title: Visual Studio tools for game programming
description: Learn about the tools for DirectX game programming that are available in Visual Studio, including the Image Editor, Model Editor, and Shader Designer.
ms.assetid: 43137bfc-7876-70e0-515c-4722f68bd064
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, visual studio, tools, directx
ms.localizationpriority: medium
---
# Visual Studio tools for game programming



**Summary**

-   [Create a DirectX game project from a template](user-interface.md)
-   Visual Studio tools for DirectX game programming


If you use Visual Studio Ultimate to develop DirectX apps, there are additional tools available for creating, editing, previewing, and exporting image, model, and shader resources. There are also tools that you can use to convert resources at build time and debug DirectX graphics code.

This topic gives an overview of these graphics tools.

## Image Editor


Use the Image Editor to work with the kinds of rich texture and image formats that DirectX uses. The Image Editor supports the following formats.

-   .png
-   .jpg, .jpeg, .jpe, .jfif
-   .dds
-   .gif
-   .bmp
-   .dib
-   .tif, .tiff
-   .tga

Create [build customization files](#build-customizations-for-3d-assets) to convert these to .dds files at build time.

For more information, see [Working with Textures and Images](/visualstudio/designers/working-with-textures-and-images?view=vs-2015).

> **Note**  The Image Editor is not intended to be a replacement for a full feature image editing app, but is appropriate for many simple viewing and editing scenarios.

 

## Model Editor


You can use the Model Editor to create basic 3D models from scratch, or to view and modify more-complex 3D models from full-featured 3D modeling tools. The Model Editor supports several 3D model formats that are used in DirectX app development. You can create [build customization files](#build-customizations-for-3d-assets) to convert these to .cmo files at build time.

-   .fbx
-   .dae
-   .obj

Here's a screenshot of a model in the editor with lighting applied.

![teapot](images/modeleditor.png)

For more information, see [Working with 3-D Models](/visualstudio/designers/working-with-3-d-models?view=vs-2015).

> **Note**  The Model Editor is not intended to be a replacement for a full feature model editing app, but is appropriate for many simple viewing and editing scenarios.

 

## Shader Designer


Use the Shader Designer to create custom visual effects for your game or app even if you don't know HLSL programming.

You create a shader visually as a graph. Each node displays a preview of the output up to that operation. Here's an example that applies Lambert lighting with a sphere preview.

![visual shader graph](images/shaderdesigner.png)

Use the Shader Editor to design, edit, and save shaders in the .dgsl format. It also exports the following formats.

-   .hlsl (source code)
-   .cso (bytecode)
-   .h (HLSL bytecode array)

Create [build customization files](#build-customizations-for-3d-assets) to convert any of these formats to .cso files at build time.

Here is a portion of HLSL code that is exported by the Shader Editor. This is only the code for the Lambert lighting node.

```hlsl
//
// Lambert lighting function
//
float3 LambertLighting(
    float3 lightNormal,
    float3 surfaceNormal,
    float3 materialAmbient,
    float3 lightAmbient,
    float3 lightColor,
    float3 pixelColor
    )
{
    // Compute the amount of contribution per light.
    float diffuseAmount = saturate(dot(lightNormal, surfaceNormal));
    float3 diffuse = diffuseAmount * lightColor * pixelColor;

    // Combine ambient with diffuse.
    return saturate((materialAmbient * lightAmbient) + diffuse);
}
```

For more information, see [Working with Shaders](/visualstudio/designers/working-with-shaders?view=vs-2015).

## Build customizations for 3D assets


You can add build customizations to your project so that Visual Studio converts resources to usable formats. After that, you can load the assets into your app and use them by creating and filling DirectX resources just like you would in any other DirectX app.

To add a build customization, you right-click on the project in the **Solution Explorer** and select **Build Customizations...**. You can add the following types of build customizations to your project.

-   Image Content Pipeline takes image files as input and outputs DirectDraw Surface (.dds) files.
-   Mesh Content Pipeline takes mesh files (such as .fbx) and outputs .cmo mesh files.
-   Shader Content Pipeline takes Visual Shader Graph (.dgsl) from the Visual Studio Shader Editor and outputs a Compiled Shader Output (.cso) file.

For more information, see [Using 3-D Assets in Your Game or App](/visualstudio/designers/using-3-d-assets-in-your-game-or-app?view=vs-2015).

## Debugging DirectX graphics


Visual Studio provides graphics-specific debugging tools. Use these tools to debug things like:

-   The graphics pipeline.
-   The event call stack.
-   The object table.
-   The device state.
-   Shader bugs.
-   Uninitialized or incorrect constant buffers and parameters.
-   DirectX version compatibility.
-   Limited Direct2D support.
-   Operating system and SDK requirements.

For more information, see [Debugging DirectX Graphics](/visualstudio/debugger/visual-studio-graphics-diagnostics?view=vs-2015).


 

 

 