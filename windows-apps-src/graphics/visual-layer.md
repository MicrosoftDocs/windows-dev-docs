---
author: scottmill
ms.assetid: a2751e22-6842-073a-daec-425fb981bafe
title: Visual Layer
description: The Windows.UI.Composition API gives you access to the composition layer between the framework layer (XAML), and the graphics layer (DirectX).
ms.author: scotmi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Visual Layer

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

In Windows 10, significant work was done to create a new unified compositor and rendering engine for all Windows applications, be it desktop or mobile. A result of that work was the unified Composition WinRT API called Windows.UI.Composition that offers access to new lightweight Composition objects along with new Compositor driven Animations and Effects.

Windows.UI.Composition is a declarative, [Retained-Mode](https://msdn.microsoft.com/library/windows/desktop/ff684178.aspx) API that can be called from any Universal Windows Platform (UWP) Application to create composition objects, animations and effects directly in an application. The API is a powerful supplement to existing frameworks such as XAML to give developers of UWP applications a familiar C# surface to add to their application. These APIs can also be used to create DX style framework-less applications.

A XAML developer can “drop down” to the composition layer in C# to do custom work in the composition layer using WinRT, rather than dropping all the way down to the graphics layer and using DirectX and C++ for any custom UI work. This technique can be used to animate an existing element using Composition API's, or to augment a UI by creating a "Visual Island" of Windows.UI.Composition content within the XAML element tree.

![UI framework layering: the framework layer (Windows.UI.XAML) is built on the visual layer (Windows.UI.Composition) which is build on the graphics layer (DirectX)](images/layers-win-ui-composition.png)
## <span id="Composition_Objects_and_The_Compositor"></span><span id="composition_objects_and_the_compositor"></span><span id="COMPOSITION_OBJECTS_AND_THE_COMPOSITOR"></span>Composition Objects and the Compositor

Composition objects are created by the [**Compositor**](https://msdn.microsoft.com/library/windows/apps/Dn706789) which acts as a factory for composition objects. The compositor can create [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858) objects, which allow for the creation of a visual tree structure on which all other features and Composition objects in the API use and build on.

The API allows developers to define and create one or many [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858) objects each representing a single node in a Visual tree.

Visuals can be containers for other Visuals or can host content Visuals. The API allows for ease of use by providing a clear set of [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858) objects for specific tasks that exist in a hierarchy:

-   [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858) – The base object. The majority of the properties are here, and inherited by the other Visual objects.
-   [**ContainerVisual**](https://msdn.microsoft.com/library/windows/apps/Dn706810) – Derives from [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858), and adds the ability to insert child visuals.
-   [**SpriteVisual**](https://msdn.microsoft.com/library/windows/apps/Mt589433) – Derives from [**ContainerVisual**](https://msdn.microsoft.com/library/windows/apps/Dn706810), and contains content in the form of images, effects, and swapchains.
-   [**LayerVisual**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.layervisual.aspx) - A ContainerVisual whose children are flattened into a single layer.  
-   [**Compositor**](https://msdn.microsoft.com/library/windows/apps/Dn706789) – The object factory that manages the relationship between an application and the system compositor process.

The compositor is also a factory for a number of other composition objects used to clip or transform visuals in the tree as well as a rich set of animations and effects.

## <span id="Effects_System"></span><span id="effects_system"></span><span id="EFFECTS_SYSTEM"></span>Effects System

Windows.UI.Composition supports real time effects that can be animated, customized and chained. Effects include 2D affine transforms, arithmetic composites, blends, color source, composite, contrast, exposure, grayscale, gamma transfer, hue rotate, invert, saturate, sepia, temperature and tint.

For more information, see the [Composition Effects](composition-effects.md) overview.

## <span id="Animation_System"></span><span id="animation_system"></span><span id="ANIMATION_SYSTEM"></span>Animation System

Windows.UI.Composition contains an expressive, framework agnostic animation system that allows you to set up two types of Animations: key frame animations and expression animations. These are used to move visual objects, drive a transform or a clip, or animate an effect. By running directly in the compositor process, this ensures smoothness and scale, letting you run large numbers of concurrent, unique animations.

For more information, see the [Composition animations](composition-animation.md) overview.

## <span id="XAML_Interoperation"></span><span id="xaml_interoperation"></span><span id="XAML_INTEROPERATION"></span>XAML Interoperation

In addition to creating a visual tree from scratch, the Composition API can interoperate with an existing XAML UI using the [**ElementCompositionPreview**](https://msdn.microsoft.com/library/windows/apps/Mt608976) class in [**Windows.UI.Xaml.Hosting**](https://msdn.microsoft.com/library/windows/apps/Hh701908).

- [**ElementCompositionPreview.GetElementVisual()**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.hosting.elementcompositionpreview.getelementvisual): Get the backing Visual of an element to animate it using Composition API's
- [**ElementCompositionPreview.SetChildVisual()**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.hosting.elementcompositionpreview.setelementchildvisual): Add a "Visual island" of Composition content to a XAML tree.
- [**ElementCompositionPreview.GetScrollViewerManipulationPropertySet()**](https://msdn.microsoft.com/library/windows/apps/mt608980.aspx): Use manipulation of a [**ScrollViewer**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.scrollviewer.aspx) as input to a Composition animation


**Note**  
This article is for Windows 10 developers writing Universal Windows Platform (UWP) apps. If you’re developing for Windows 8.x or Windows Phone 8.x, see the [archived documentation](http://go.microsoft.com/fwlink/p/?linkid=619132).

 

## <span id="Additional_Resources_"></span><span id="additional_resources_"></span><span id="ADDITIONAL_RESOURCES_"></span>Additional Resources:

-   Read Kenny Kerr's MSDN Article on this API: [Graphics and Animation - Windows Composition Turns 10](https://msdn.microsoft.com/magazine/mt590968)
-   Advanced UI and Composition samples in the [WindowsUIDevLabs GitHub](https://github.com/microsoft/windowsuidevlabs).
-   [**Full reference documentation for the API**](https://msdn.microsoft.com/library/windows/apps/Dn706878).


 

 




