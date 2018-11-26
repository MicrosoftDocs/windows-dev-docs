---
title: State objects
description: Device state is grouped into state objects which greatly reduce the cost of state changes. There are several state objects, and each one is designed to initialize a set of state for a particular pipeline stage. State objects vary by version of Direct3D.
ms.assetid: D998745C-2B75-4E59-9923-AD1A17A92E05
keywords:
- State objects
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# State objects


Device state is grouped into state objects which greatly reduce the cost of state changes. There are several state objects, and each one is designed to initialize a set of state for a particular pipeline stage. State objects vary by version of Direct3D.

## <span id="Input_Layout"></span><span id="input_layout"></span><span id="INPUT_LAYOUT"></span>Input-Layout State


This group of state dictates how the [Input Assembler (IA) stage](input-assembler-stage--ia-.md) reads data out of the input buffers and assembles it for use by the vertex shader. This includes state such as the number of elements in the input buffer and the signature of the input data. The Input Assembler (IA) stage streams primitives from memory into the pipeline.

## <span id="Rasterizer"></span><span id="rasterizer"></span><span id="RASTERIZER"></span>Rasterizer State


This group of state initializes the [Rasterizer (RS) stage](rasterizer-stage--rs-.md). This object includes state such as fill or cull modes, enabling a scissor rectangle for clipping, and setting multisample parameters. This stage rasterizes primitives into pixels, performing operations like clipping and mapping primitives to the viewport.

## <span id="DepthStencil"></span><span id="depthstencil"></span><span id="DEPTHSTENCIL"></span>Depth-Stencil State


This group of state initializes the depth-stencil portion of the [Output Merger (OM) stage](output-merger-stage--om-.md). More specifically, this object initializes depth and stencil testing.

## <span id="Blend"></span><span id="blend"></span><span id="BLEND"></span>Blend State


This group of state initializes the blending portion of the [Output Merger (OM) stage](output-merger-stage--om-.md).

## <span id="Sampler"></span><span id="sampler"></span><span id="SAMPLER"></span>Sampler State


This group of state initializes a sampler object. A sampler object is used by the shader stages to filter textures in memory.

In Direct3D, the sampler object is not bound to a specific texture, it just describes how to do filtering given any attached resource.

## <span id="Performance_Considerations"></span><span id="performance_considerations"></span><span id="PERFORMANCE_CONSIDERATIONS"></span>Performance Considerations


Designing the API to use state objects creates several performance advantages. These include validating state at object creation time, enabling caching of state objects in hardware, and greatly reducing the amount of state that is passed during a state-setting API call (by passing a handle to the state object instead of the state).

To achieve these performance improvements, you should create your state objects when your application starts up, well before your render loop. State objects are immutable, that is, once they are created, you cannot change them. Instead you must destroy and recreate them.

You could create several sampler objects with various sampler-state combinations. Changing the sampler state is then accomplished by calling the appropriate "Set" API which passes a handle to the object (as opposed to the sampler state). This significantly reduces the amount of overhead during each render frame for changing state since the number of calls and the amount of data are greatly reduced.

Alternatively, you can choose to use the effect system which will automatically manage efficient creation and destruction of state objects for your application.

## <span id="related-topics"></span>Related topics


[Graphics pipeline](graphics-pipeline.md)

[Views](views.md)

 

 




