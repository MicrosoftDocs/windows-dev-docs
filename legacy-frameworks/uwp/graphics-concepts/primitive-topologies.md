---
title: Primitive topologies
description: Direct3D supports several primitive topologies, which define how vertices are interpreted and rendered by the pipeline, such as point lists, line lists, and triangle strips.
ms.assetid: 7AA5A4A2-0B7C-431D-B597-684D58C02BA5
keywords:
- Primitive topologies
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Primitive topologies


Direct3D supports several primitive topologies, which define how vertices are interpreted and rendered by the pipeline, such as point lists, line lists, and triangle strips.

## <span id="Primitive_Types"></span><span id="primitive_types"></span><span id="PRIMITIVE_TYPES"></span>Basic primitive topologies


The following basic primitive topologies (or primitive types) are supported:

-   [Point lists](point-lists.md)
-   [Line lists](line-lists.md)
-   [Line strips](line-strips.md)
-   [Triangle lists](triangle-lists.md)
-   [Triangle strips](triangle-strips.md)

For a visualization of each primitive type, see the diagram later in this topic in [Winding direction and leading vertex positions](#winding-direction-and-leading-vertex-positions).

The [Input Assembler (IA) stage](input-assembler-stage--ia-.md) reads data from vertex and index buffers, assembles the data into these primitives, and then sends the data to the remaining pipeline stages.

## <span id="Primitive_Adjacency"></span><span id="primitive_adjacency"></span><span id="PRIMITIVE_ADJACENCY"></span>Primitive adjacency


All Direct3D primitive types (except the point list) are available in two versions: one primitive type with adjacency and one primitive type without adjacency. Primitives with adjacency contain some of the surrounding vertices, while primitives without adjacency contain only the vertices of the target primitive. For example, the line list primitive has a corresponding line list primitive that includes adjacency.

Adjacent primitives are intended to provide more information about your geometry and are only visible through a geometry shader. Adjacency is useful for geometry shaders that use silhouette detection, shadow volume extrusion, and so on.

For example, suppose you want to draw a triangle list with adjacency. A triangle list that contains 36 vertices (with adjacency) will yield 6 completed primitives. Primitives with adjacency (except line strips) contain exactly twice as many vertices as the equivalent primitive without adjacency, where each additional vertex is an adjacent vertex.

## <span id="Winding_Direction_and_Leading_Vertex_Positions"></span><span id="winding_direction_and_leading_vertex_positions"></span><span id="WINDING_DIRECTION_AND_LEADING_VERTEX_POSITIONS"></span><span id="winding-direction-and-leading-vertex-positions"></span>Winding direction and leading vertex positions


As shown in the following illustration, a leading vertex is the first non-adjacent vertex in a primitive. A primitive type can have multiple leading vertices defined, as long as each one is used for a different primitive.

-   For a triangle strip with adjacency, the leading vertices are 0, 2, 4, 6, and so on.
-   For a line strip with adjacency, the leading vertices are 1, 2, 3, and so on.
-   An adjacent primitive, on the other hand, has no leading vertex.

The following illustration shows the vertex ordering for all of the primitive types that the input assembler can produce.

![diagram of vertex ordering for primitive types](images/d3d10-primitive-topologies.png)

The symbols in the preceding illustration are described in the following table.

| Symbol                                                                                   | Name              | Description                                                                         |
|------------------------------------------------------------------------------------------|-------------------|-------------------------------------------------------------------------------------|
| ![symbol for a vertex](images/d3d10-primitive-topologies-vertex.png)                     | Vertex            | A point in 3D space.                                                                |
| ![symbol for winding direction](images/d3d10-primitive-topologies-winding-direction.png) | Winding Direction | The vertex order when assembling a primitive. Can be clockwise or counterclockwise. |
| ![symbol for leading vertex](images/d3d10-primitive-topologies-leading-vertex.png)       | Leading Vertex    | The first non-adjacent vertex in a primitive that contains per-constant data.       |

 

## <span id="Generating_Multiple_Strips"></span><span id="generating_multiple_strips"></span><span id="GENERATING_MULTIPLE_STRIPS"></span>Generating multiple strips


You can generate multiple strips through strip cutting. You can perform a strip cut by explicitly calling the [RestartStrip](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-so-restartstrip) HLSL function, or by inserting a special index value into the index buffer. This value is –1, which is 0xffffffff for 32-bit indices or 0xffff for 16-bit indices.

An index of –1 indicates an explicit 'cut' or 'restart' of the current strip. The previous index completes the previous primitive or strip and the next index starts a new primitive or strip.

For more info about generating multiple strips, see [Geometry Shader (GS) stage](geometry-shader-stage--gs-.md).

## <span id="related-topics"></span>Related topics


[Input-Assembler (IA) stage](input-assembler-stage--ia-.md)

[Graphics pipeline](graphics-pipeline.md)

 

 