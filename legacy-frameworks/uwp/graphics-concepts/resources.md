---
title: Resources
description: A resource is an area in memory that can be accessed by the Direct3D pipeline.
ms.assetid: 2E68E5A8-83DA-4DC8-B7F3-B8988CF8090C
keywords:
- Resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Resources


A resource is an area in memory that can be accessed by the Direct3D pipeline. In order for the pipeline to access memory efficiently, data that is provided to the pipeline (such as input geometry, shader resources, and textures) must be stored in a resource. There are two types of resources from which all Direct3D resources derive: a buffer or a texture. Up to 128 resources can be active for each pipeline stage.

Each application will typically create many resources. Examples of resource include: vertex buffers, index buffer, constant buffer, textures, and shader resources. There are several options that determine how resources can be used. You can create resources that are strongly typed or type less; you can control whether resources have both read and write access; you can make resources accessible to only the CPU, GPU, or both. Naturally, there will be speed vs. functionality tradeoff - the more functionality you allow a resource to have, the less performance you should expect.

Since an application often uses many textures, Direct3D also has the concept of a texture array to simplify texture management. A texture array contains one or more textures (all of the same type and dimensions) that can be indexed from within an application or by shaders. Texture arrays allow you to use a single interface with multiple indexes to access many textures. You can create as many texture arrays to manage different texture types as you need.

Once you have created the resources that your application will use, you connect or bind each resource to the pipeline stages that will use them. This is accomplished by calling a bind API, which takes a pointer to the resource. Since more than one pipeline stage may need access to the same resource, Direct3D has the concept of a resource view. A view identifies the portion of a resource that can be accessed. You can create *m* views or a resource and bind them to *n* pipeline stages, assuming you follow binding rules for shared resource (the runtime will generate errors at compile time if you don't).

A resource view provides a general model for access to a resource (such as textures or buffers). Because you can use a view to tell the runtime what data to access and how to access it, resource views allow you create type less resources. That is, you can create resources for a given size at compile time, and then declare the data type within the resource when the resource gets bound to the pipeline. Views expose many capabilities for using resources, such as the ability to read back depth/stencil surfaces in the shader, generating dynamic cubemaps in a single pass, and rendering simultaneously to multiple slices of a volume.

## <span id="in-this-section"></span>In this section


<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="resource-types.md">Resource types</a></p></td>
<td align="left"><p>Different types of resources have a distinct layout (or memory footprint). All resources used by the Direct3D pipeline derive from two basic resource types: <a href="resource-types.md#buffer-resources">buffers</a> and <a href="resource-types.md#texture-resources">textures</a>. A buffer is a collection of raw data (elements); a texture is a collection of texels (texture elements).</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="choosing-a-resource.md">Choosing a resource</a></p></td>
<td align="left"><p>A resource is a collection of data that is used by the 3D pipeline. Creating resources and defining their behavior is the first step toward programming your application. This guide covers basic topics for choosing the resources required by your application.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="copying-and-accessing-resource-data.md">Copying and accessing resource data</a></p></td>
<td align="left"><p>Usage flags indicate how the application intends to use the resource data, to place resources in the most performant area of memory possible. Resource data is copied across resources so that the CPU or GPU can access it without impacting performance.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="texture-views.md">Texture views</a></p></td>
<td align="left"><p>In Direct3D, texture resources are accessed with a view, which is a mechanism for hardware interpretation of a resource in memory. A view allows a particular pipeline stage to access only the <a href="resource-types.md">subresources</a> it needs, in the representation desired by the application.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Coordinate systems](coordinate-systems.md)

[Direct3D Graphics Learning Guide](index.md)

[Floating-point rules](floating-point-rules.md)

[Data type conversion](data-type-conversion.md)
