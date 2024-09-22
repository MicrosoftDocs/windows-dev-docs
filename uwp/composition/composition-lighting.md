---
title: Composition Lighting
description: The Composition Lighting APIs can be used to add dynamic 3D lighting to your application.
ms.date: 03/16/2023
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Using lights in Windows UI

The Windows.UI.Composition APIs enable you to create real-time animations and effects. Composition Lighting enables 3D lighting in 2D applications. In this overview, we will run through the functionality of how to setup composition lights, identify visuals to receive each light, and use effects to define materials for your content.

> [!NOTE]
> To read how [XamlLight](/uwp/api/windows.ui.xaml.media.xamllight) objects apply [CompositionLights](/uwp/api/Windows.UI.Composition.CompositionLight) to illuminate XAML UIElements, see [XAML lighting](xaml-lighting.md).

Composition lighting lets you create interesting UI by allowing:

- Transformation of a light independent of other objects in the scene to enable immersive scenarios like music playback scenes.
- The ability to pair an object with a light so they move together independent of the rest of the scene to enable scenarios like Fluent [Reveal](/windows/apps/design/style/index) highlight.
- Transformation of the light and entire scene as a group to create materials and depth.

Composition lighting supports three key concepts: **Light**, **Targets**, and **SceneLightingEffect**.

## Light

[CompositionLight](/uwp/api/windows.ui.composition.compositionlight) allows you to create various lights and place them in coordinate space. These lights target visuals that you wish to identify as lit by the light.

### Light Types

| Type | Description |
| --- | --- |
| [AmbientLight](/uwp/api/windows.ui.composition.ambientlight) | A light source that emits non-directional light that appears reflected by everything in the scene. |
| [DistantLight](/uwp/api/windows.ui.composition.distantlight) | An infinitely large distant light source that emits light in a single direction. Like the sun. |
| [PointLight](/uwp/api/windows.ui.composition.pointlight) | A point source of light that emits light in all directions. Like a lightbulb. |
| [SpotLight](/uwp/api/windows.ui.composition.spotlight) | A light source that emits inner and outer cones of light. Like a flashlight. |

## Targets

When lights target a Visual (add to [Targets](/uwp/api/windows.ui.composition.compositionlight.targets) list), the Visual and all its descendants are aware of and respond to this light source. This can be something as simple as a setting a PointLight source at the root of a tree and all visuals below react to the animation of the point lights direction.

**ExclusionsFromTargets** gives you the ability to remove the lighting of a visual or of a subtree of visuals in a similar manner as adding targets. Children in the tree rooted by the visual that's excluded are not lit as a result.

### Sample (Targets)

In the sample below, we use a CompositionPointLight to target a XAML TextBlock.

```cs
    _pointLight = _compositor.CreatePointLight();
    _pointLight.Color = Colors.White;
    _pointLight.CoordinateSpace = text; //set up co-ordinate space for offset
    _pointLight.Targets.Add(text); //target XAML TextBlock
```

By adding animation to the offset of the point light, a shimmering effect is easily achieved.

```cs
_pointLight.Offset = new Vector3(-(float)TextBlock.ActualWidth, (float)TextBlock.ActualHeight / 2, (float)TextBlock.FontSize);
```

See the complete [Text Shimmer](https://github.com/microsoft/WindowsCompositionSamples/tree/master/SampleGallery/Samples/SDK%2014393/TextShimmer) sample on GitHub to learn more.

## Restrictions

There are several factors to consider when determining which content will be lit by CompositionLight.

Concept | Details
--- | ---
**Ambient Light** | Adding a non-ambient light to your scene will turn off all existing light.  Items not targeted by a non-ambient light will appear black.  To illuminate surrounding visuals not targeted by the light in a natural way, use an ambient light in conjunction with other lights.
**Number of Lights** | You may use any two non-ambient composition lights in any combination to target your UI. Ambient lights are not restricted; spot, point, and distant lights are.
**Lifetime** | CompositionLight may experience lifetime conditions (example: the garbage collector may recycle the light object before it is used).  We recommended keeping a reference to your lights by adding lights as a member to help the application manage lifetime.
**Transforms** | Lights must be placed in a node above UI that uses effects like [perspective transforms](/windows/apps/design/layout/3-d-perspective-effects) in your visual structure to be drawn properly.
**Targets and Coordinate Space** | CoordinateSpace is the visual space in which all the lights properties must be set. CompositionLight.Targets must be within the CoordinateSpace tree.

## Lighting Properties

Depending on the type of light used, a light can have properties for attenuation and space. Not all types of lights use all properties.

Property | Description
--- | ---
**Color** | The [Color](/uwp/api/windows.ui.color) of the light. Lighting color values are defined by [D3D](../graphics-concepts/light-properties.md) Diffuse, Ambient, and Specular that defines the color being emitted. Lighting uses RGBA values for lights; the alpha color component is not used.
**Direction** | The direction of light. The direction in which the light is pointing is specified relative to its [CoordinateSpace](/uwp/api/windows.ui.composition.distantlight.coordinatespace) Visual.
**Coordinate Space** | Every Visual has an implicit 3D coordinate space. X direction is from left to right. Y direction is from top to bottom. Z direction is a point out of the plane. The original point of this coordinate is the upper-left corner of the visual, and the unit is Device Independent Pixel (DIP). A light's offset defined in this coordinate.
**Inner and Outer Cones** | Spotlights emit a cone of light that has two parts: a bright inner cone and an outer cone. Composition allows you control over inner and outer cone angles and color.
**Offset** | Offset of the light source relative to its coordinate space Visual.

> [!NOTE]
> When multiple lights hit the same Visual, or whenever a light's color value gets large enough to exceed 1.0, the color of the light may change because of clamping of a lights color channel.

### Advanced Lighting Properties

Property | Description
--- | ---
**Intensity** | Controls the brightness of the light.
**Attenuation** | Attenuation controls how a light's intensity decreases toward the maximum distance specified by the range property.  Constant, Quadratic and Linear attenuation properties can be used.

## Getting Started with Lighting

Follow these general steps to add lights:

- Create and place the lights: Create lights and place them in a specified coordinate space.
- Identify objects to light: Target light at relevant visuals.
- [Optional] Define how individual objects react to lights: Use SceneLightingEffect with an EffectBrush to customize light reflection for displaying the SpriteVisual. Reflection defaults support the lighting of children of a light source’s CoordinateSpace.  A visual painted with a SceneLightingEffect overwrites the default lighting for that visual.

## SceneLightingEffect

[SceneLightingEffect](/uwp/api/Windows.UI.Composition.Effects.SceneLightingEffect) is used to modify the default lighting applied to the contents of a [SpriteVisual](/uwp/api/Windows.UI.Composition.SpriteVisual) targeted by a [CompositionLight](/uwp/api/windows.ui.composition.compositionlight).

[SceneLightingEffect](/uwp/api/Windows.UI.Composition.Effects.SceneLightingEffect) is frequently used for material creation. A SceneLightingEffect is an effect used when you want to achieve something more complex, like enabling reflective properties on an image and/or providing an illusion of depth with a normal map. A SceneLightingEffect provides the ability to customize your UI by using the lighting properties like specular and diffuse amounts. You can further customize lighting effects with the rest of the effects pipeline allowing you individually to blend and compose different lighting reactions with your content.

> [!NOTE]
> Scene lighting does not produce shadows; it is an effect focused on 2D rendering.  It does not take into consideration 3D lighting scenarios that include real lighting models, including shadows.


Property | Description
--- | ---
**Normal Map** | NormalMaps create an effect of a texture where a normal pointing towards the light will be brighter and a normal pointing away will be dimmer. To add a NormalMap to your targeted visual use a [CompositionSurfaceBrush](/uwp/api/Windows.UI.Composition.CompositionSurfaceBrush) using LoadedImageSurface to load a NormalMap asset.
**Ambient** | Ambient properties are mostly used to control the overall color reflection.
**Specular** | Specular reflection creates highlights on objects, making them appear shiny. You can control the level of specular reflection as well as the level of shine.  These properties are manipulated to create material effects like shinny metals or glossy paper.
**Diffuse** | Diffused reflection scatters the light in all directions.
**Reflectance Model** | [Reflectance Model](/uwp/api/windows.ui.composition.effects.scenelightingeffectreflectancemodel) allows you to choose between [Blinn Phong](/visualstudio/designers/how-to-create-a-basic-phong-shader) and Physically Based Blinn Phong.  You would choose Physically Based Blinn Phong when you want to have condensed specular highlights.

### Sample (SceneLightingEffect)

The sample below shows how to add a normal map to a SceneLightingEffect.

```cs
CompositionBrush CreateNormalMapBrush(ICompositionSurface normalMapImage)
{
    var colorSourceEffect = new ColorSourceEffect()
    {
        Color = Colors.White
    };
    var sceneLightingEffect = new SceneLightingEffect()
    {
        NormalMapSource = new CompositionEffectSourceParameter("NormalMap")
    };

    var compositeEffect = new ArithmeticCompositeEffect()
    {
        Source1 = colorSourceEffect,
        Source2 = sceneLightingEffect,
    };

    var factory = _compositor.CreateEffectFactory(sceneLightingEffect);

    var normalMapBrush = _compositor.CreateSurfaceBrush();
    normalMapBrush.Surface = normalMapImage;
    normalMapBrush.Stretch = CompositionStretch.Fill;

    var brush = factory.CreateBrush();
    brush.SetSourceParameter("NormalMap", normalMapBrush);

    return brush;
}
```

## Related articles

- [Creating Materials and Lights in the Visual Layer](https://blogs.windows.com/buildingapps/2017/08/04/creating-materials-lights-visual-layer/)
- [Lighting Overview](../graphics-concepts/lighting-overview.md)
- [CompositionCapabilities API](/uwp/api/windows.ui.composition.compositioncapabilities)
- [Mathematics of Lighting](../graphics-concepts/mathematics-of-lighting.md)
- [SceneLightingEffect](/uwp/api/windows.ui.composition.effects.scenelightingeffect)
- [WindowsCompositionSamples GitHub Repo](https://github.com/microsoft/WindowsCompositionSamples)