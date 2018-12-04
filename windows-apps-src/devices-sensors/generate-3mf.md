---
Description: Describes the structure of the 3D Manufacturing Format file type and how it can be created and manipulated with the Windows.Graphics.Printing3D API.
MS-HAID: dev\_devices\_sensors.generate\_3mf
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Generate a 3MF package
ms.assetid: 968d918e-ec02-42b5-b50f-7c175cc7921b
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Generate a 3MF package

**Important APIs**

-   [**Windows.Graphics.Printing3D**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.aspx)

This guide describes the structure of the 3D Manufacturing Format document and how it can be created and manipulated with the [**Windows.Graphics.Printing3D**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.aspx) API.

## What is 3MF?

The 3D Manufacturing Format is a set of conventions for using XML to describe the appearance and structure of 3D models for the purpose of manufacturing (3D printing). It defines a set of parts (some required and some optional) and their relationships, with the goal of providing all necessary information to a 3D manufacturing device. A data set that adheres to the 3D Manufacturing Format can be saved as a file with the .3mf extension.

In Windows 10, the [**Printing3D3MFPackage**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.printing3d3mfpackage.aspx) class in the **Windows.Graphics.Printing3D** namespace is analogous to a single .3mf file, and other classes map to the particular XML elements in the file. This guide describes how each of the main parts of a 3MF document can be created and set programmatically, how the 3MF Materials Extension can be utilized, and how a **Printing3D3MFPackage** object can be converted and saved as a .3mf file. For more information on the standards of 3MF or the 3MF Materials Extension, see the [3MF Specification](http://3mf.io/what-is-3mf/3mf-specification/).

<!-- >**Note** This guide describes how to construct a 3MF document from scratch. If you wish to make changes to an already existing 3MF document provided in the form of a .3mf file, you simply need to convert it to a **Printing3D3MFPackage** and alter the contained classes/properties in the same way (see [link]) below). -->


## Core classes in the 3MF structure

The **Printing3D3MFPackage** class represents a complete 3MF document, and at the core of a 3MF document is its model part, represented by the [**Printing3DModel**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.printing3dmodel.aspx) class. Most of the information we wish to specify about a 3D model will be stored by setting the properties of the **Printing3DModel** class and the properties of their underlying classes.

[!code-cs[InitClasses](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetInitClasses)]

<!-- >**Note** We do not yet associate the **Printing3D3MFPackage** with its corresponding **Printing3DModel** object. Only after fleshing out the **Printing3DModel** with all of the information we wish to specify will we make that association (see [link]). -->

## Metadata

The model part of a 3MF document can hold metadata in the form of key/value pairs of strings stored in the **Metadata** property. There are a number of predefined names of metadata, but other pairs can be added as part of an extension (described in more detail in the [3MF specification](http://3mf.io/what-is-3mf/3mf-specification/)). It is up to the receiver of the package (a 3D manufacturing device) to determine whether and how to handle metadata, but it is good practice to include as much basic info as possible in the 3MF package:

[!code-cs[Metadata](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetMetadata)]

## Mesh data

In the context of this guide, a mesh is a body of 3-dimensional geometry constructed from a single set of vertices (though it does not have to appear as a single solid). A mesh part is represented by the [**Printing3DMesh**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.printing3dmesh.aspx) class. A valid mesh object must contain information about the location of all of its vertices as well as all the triangle faces that exist between certain sets of vertices.

The following method adds vertices to a mesh and then gives them locations in 3D space:

[!code-cs[Vertices](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetVertices)]

The next method defines all of the triangles to be drawn across these vertices:

[!code-cs[TriangleIndices](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetTriangleIndices)]

> [!NOTE]
> All triangles must have their indices defined in counter-clockwise order (when viewing the triangle from outside of the mesh object), so that their face-normal vectors point outward.

When a Printing3DMesh object contains valid sets of vertices and triangles, it should then be added to the model's **Meshes** property. All **Printing3DMesh** objects in a package must be stored under the **Meshes** property of the **Printing3DModel** class.

[!code-cs[MeshAdd](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetMeshAdd)]


## Create materials


A 3D model can hold data for multiple materials. This convention is intended to take advantage of 3D manufacturing devices that can use multiple materials on a single print job. There are also multiple *types* of material gropus, each one capable of supporting a number of different individual materals. Each material group must have a unique reference id number, and each material within that group must also have a unique id.

The different mesh objects within a model can then reference these materials. Furthermore, individual triangles on each mesh can specify different materials. Further still, different materials can even be represented within a single triangle, with each triangle vertex having a different material assigned to it and the face material calculated as the gradient between them.

This guide will first show how to create different kinds of materials within their respective material groups and store them as resources on the model object. Then, we will go about assigning different materials to individual meshes and individual triangles.

### Base materials

The default material type is **Base Material**, which has both a **Color Material** value (described below) and a name attribute that is intended to specify the *type* of material to use.

[!code-cs[BaseMaterialGroup](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetBaseMaterialGroup)]

> [!NOTE]
> The 3D manufacturing device will determine which available physical materials map to which virtual material elements stored in the 3MF. Material mapping doesn't have to be 1:1: if a 3D printer only uses one material, it will print the whole model in that material, regardless of which objects or faces were assigned different materials.

### Color materials

**Color Materials** are similar to **Base Materials**, but they do not contain a name. Thus, they give no instructions as to what type of material should be used by the machine. They hold only color data, and let the machine choose the material type (and the machine may then prompt the user to choose). In the code below, the `colrMat` objects from the previous method are used on their own.

[!code-cs[ColorMaterialGroup](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetColorMaterialGroup)]

### Composite materials

**Composite Materials** simply instruct the manufacturing device to use a uniform mixture of different **Base Materials**. Each **Composite Material Group** must reference exactly one **Base Material Group** from which to draw ingredients. Additonally, the **Base Materials** within this group that are to be made available must be listed out in a **Material Indices** list, which each **Composite Material** will then reference when specifying the ratios (every **Composite Material** is simply a ratio of **Base Materials**).

[!code-cs[CompositeMaterialGroup](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetCompositeMaterialGroup)]

### Texture coordinate materials

3MF supports the use of 2D images to color the surfaces of 3D models. This way, the model can convey much more color data per triangle face (as opposed to having just one color value per triangle vertex). Like **Color Materials**, texture coordinate materials only convery color data. To use a 2D texture, a texture resource must first be declared:

[!code-cs[TextureResource](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetTextureResource)]

> [!NOTE]
> Texture data belongs to the 3MF Package itself, not to the model part within the package.

Next, we must fill out **Texture3Coord Materials**. Each of these references a texture resource and specifies a particular point on the image (in UV coordinates).

[!code-cs[Texture2CoordMaterialGroup](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetTexture2CoordMaterialGroup)]

## Map materials to faces

In order to dictate which materials are mapped to which vertices on each triangle, we must do some more work on the mesh object of our model (if a model contains multiple meshes, they must each have their materials assigned separately). As mentioned above, materials are assigned per-vertex, per-triangle. Refer to the code below to see how this information is entered and interpreted.

[!code-cs[MaterialIndices](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetMaterialIndices)]

## Components and build

The component structure allows the user to place more than one mesh object in a printable 3D model. A [**Printing3DComponent**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.printing3dcomponent.aspx) object contains a single mesh and a list of references to other components. This is actually a list of [**Printing3DComponentWithMatrix**](https://msdn.microsoft.com/library/windows/apps/windows.graphics.printing3d.printing3dcomponentwithmatrix.aspx) objects. **Printing3DComponentWithMatrix** objects each contain a **Printing3DComponent** and, importantly, a transform matrix that applies to the mesh and contained components of said **Printing3DComponent**.

For example, a model of a car might consist of a "Body" **Printing3DComponent** that holds the mesh for the car's body. The "Body" component may then contain references to four different **Printing3DComponentWithMatrix** objects, which all reference the same **Printing3DComponent** with the "Wheel" mesh and contain four different transform matrices (mapping the wheels to four different positions on the car's body). In this scenario, the "Body" mesh and "Wheel" mesh would each only need to be stored once, even though the final product would feature five meshes in total.

All **Printing3DComponent** objects must be directly referenced in the model's **Components** property. The one particular component that is to be used in the printing job is stored in the **Build** Property.

[!code-cs[Components](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetComponents)]

## Save package
Now that we have a model, with defined materials and components, we can save it to the package.

[!code-cs[SavePackage](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetSavePackage)]

From here, we can either initiate a print job within the app (see [3D printing from your app](https://msdn.microsoft.com/library/windows/apps/mt204541.aspx)), or save this **Printing3D3MFPackage** as a .3mf file.

The following method takes a finished **Printing3D3MFPackage** and saves its data to a .3mf file.

[!code-cs[SaveTo3mf](./code/3dprinthowto/cs/Generate3MFMethods.cs#SnippetSaveTo3mf)]

## Related topics

[3D printing from your app](https://msdn.microsoft.com/windows/uwp/devices-sensors/3d-print-from-app)  
[3D printing UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/3DPrinting)
 

 

 
