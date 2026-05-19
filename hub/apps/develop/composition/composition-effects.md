---
title: Composition effects in WinUI
description: The composition effect APIs let you customize how WinUI and Windows App SDK UI is rendered.
ms.date: 03/16/2026
ms.topic: article
ms.localizationpriority: medium
---
# Composition effects

The [**Microsoft.UI.Composition**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition) APIs allow real-time effects to be applied to images and UI with animatable effect properties. In this overview, we’ll walk through the functionality that lets you apply effects to a composition visual in WinUI and Windows App SDK apps.

To support consistent effect authoring in WinUI and Windows App SDK apps, composition effects leverage Win2D’s IGraphicsEffect interface so you can describe effects by using the [Microsoft.Graphics.Canvas.Effects](https://microsoft.github.io/Win2D/WinUI2/html/N_Microsoft_Graphics_Canvas_Effects.htm) namespace.

Brush effects are used to paint areas of an application by applying effects to a set of existing images. Windows App SDK composition effect APIs are focused on SpriteVisuals. A SpriteVisual gives you flexibility when combining color, image, and effect creation. The visual defines the bounds of the rectangle, and the brush defines the pixels used to paint it.

Effect brushes are used on composition tree visuals whose content comes from the output of an effect graph. Effects can reference existing surfaces/textures, but not the output of other composition trees.

Effects can also be applied to XAML UIElements by using an effect brush with [**XamlCompositionBrushBase**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.xamlcompositionbrushbase).

## Effect Features

- [Effect Library](./composition-effects.md#effect-library)
- [Chaining Effects](./composition-effects.md#chaining-effects)
- [Animation Support](./composition-effects.md#animation-support)
- [Constant vs. Animated Effect Properties](./composition-effects.md#constant-vs-animated-effect-properties)
- [Multiple Effect Instances with Independent Properties](./composition-effects.md#multiple-effect-instances-with-independent-properties)

### Effect Library

Currently composition supports the following effects:

| Effect               | Description                                                                                                                                                                                                                |
|----------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 2D affine transform  | Applies a 2D affine transform matrix to an image.  |
| Arithmetic composite | Combines two images using a flexible equation. |
| Blend effect         | Creates a blend effect that combines two images. Composition provides 21 of the 26 [blend modes](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_BlendEffectMode.htm) supported in Win2D.        |
| Color source         | Generates an image containing a solid color.                                                                                                                                                                               |
| Composite            | Combines two images. Composition provides all 13 [composite modes](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasComposite.htm) supported in Win2D.                                              |
| Contrast             | Increases or decreases the contrast of an image.                                                                                                                                                                           |
| Exposure             | Increases or decreases the exposure of an image.                                                                                                                                                                           |
| Grayscale            | Converts an image to monochromatic gray.                                                                                                                                                                                   |
| Gamma transfer       | Alters the colors of an image by applying a per-channel gamma transfer function.                                                                                                                                           |
| Hue rotate           | Alters the color of an image by rotating its hue values.                                                                                                                                                                   |
| Invert               | Inverts the colors of an image.                                                                                                                                                                                            |
| Saturate             | Alters the saturation of an image.                                                                                                                                                                                         |
| Sepia                | Converts an image to sepia tones.                                                                                                                                                                                          |
| Temperature and tint | Adjusts the temperature and/or tint of an image.                                                                                                                                                                           |

See Win2D’s [Microsoft.Graphics.Canvas.Effects](https://microsoft.github.io/Win2D/WinUI2/html/N_Microsoft_Graphics_Canvas_Effects.htm) Namespace for more detailed information. Effects not supported in composition are noted as \[NoComposition\].

### Chaining Effects

Effects can be chained, allowing an application to simultaneously use multiple effects on an image. Effect graphs can support multiple effects that can refer to one and other. When describing your effect, simply add an effect as input to your effect.

```csharp
IGraphicsEffect graphicsEffect =
new Microsoft.Graphics.Canvas.Effects.ArithmeticCompositeEffect
{
  Source1 = new CompositionEffectSourceParameter("source1"),
  Source2 = new SaturationEffect
  {
    Saturation = 0,
    Source = new CompositionEffectSourceParameter("source2")
  },
  MultiplyAmount = 0,
  Source1Amount = 0.5f,
  Source2Amount = 0.5f,
  Offset = 0
};
```

The example above describes an arithmetic composite effect which has two inputs. The second input has a saturation effect with a .5 saturation property.

### Animation Support

Effect properties support animation, during effect compilation you can specify effect properties can be animated and which can be "baked in" as constants. The animatable properties are specified through strings of the form “effect name.property name”. These properties can be animated independently over multiple instantiations of the effect.

### Constant vs Animated Effect Properties

During effect compilation you can specify effect properties as dynamic or as properties that are "baked in" as constants. The dynamic properties are specified through strings of the form “\<effect name>.\<property name>”. The dynamic properties can be set to a specific value or can be animated using the composition animation system.

When compiling the effect description above, you have the flexibility of either baking in saturation to be equal to 0.5 or making it dynamic and setting it dynamically or animating it.

Compiling an effect with saturation baked in:

```csharp
var effectFactory = _compositor.CreateEffectFactory(graphicsEffect);
```

Compiling an effect with dynamic saturation:

```csharp
var effectFactory = _compositor.CreateEffectFactory(graphicsEffect, new[]{"SaturationEffect.Saturation"});
_catEffect = effectFactory.CreateBrush();
_catEffect.SetSourceParameter("mySource", surfaceBrush);
_catEffect.Properties.InsertScalar("saturationEffect.Saturation", 0f);
```

The saturation property of the effect above can then be either set to a static value or animated using either Expression or ScalarKeyFrame animations.

You can create a ScalarKeyFrame that will be used to animate the Saturation property of an effect like this:

```csharp
ScalarKeyFrameAnimation effectAnimation = _compositor.CreateScalarKeyFrameAnimation();
            effectAnimation.InsertKeyFrame(0f, 0f);
            effectAnimation.InsertKeyFrame(0.50f, 1f);
            effectAnimation.InsertKeyFrame(1.0f, 0f);
            effectAnimation.Duration = TimeSpan.FromMilliseconds(2500);
            effectAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
```

Start the animation on the Saturation property of the effect like this:

```csharp
catEffect.Properties.StartAnimation("saturationEffect.Saturation", effectAnimation);
```

### Multiple Effect Instances with Independent Properties

By specifying that a parameter should be dynamic during effect compilation, the parameter can then be changed on a per-effect instance basis. This allows two Visuals to use the same effect but be rendered with different effect properties.

## Getting Started with Composition Effects

This quick start tutorial shows you how to make use of some of the basic capabilities of effects.

- [Installing Visual Studio](./composition-effects.md#installing-visual-studio)
- [Creating a new project](./composition-effects.md#creating-a-new-project)
- [Installing Win2D](./composition-effects.md#installing-win2d)
- [Setting your Composition Basics](./composition-effects.md#setting-your-composition-basics)
- [Creating a CompositionSurface Brush](./composition-effects.md#creating-a-compositionsurface-brush)
- [Creating, Compiling and Applying Effects](./composition-effects.md#creating-compiling-and-applying-effects)

### Installing Visual Studio

- If you don't have a supported version of Visual Studio installed, go to the Visual Studio Downloads page [here](https://visualstudio.microsoft.com/downloads/download-visual-studio-vs).

### Creating a new project

- Go to **File** > **New** > **Project**.
- Select the **WinUI Blank App (Packaged)** template.
- Enter a project name of your choosing.
- Click **Create**.

### Installing Win2D

Win2D is released as a NuGet.org package and needs to be installed before you can use these effects in a WinUI project.

- Launch the NuGet Package Manager by going to **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**.
- Search for **Win2D.WinUI** and install that package for your project.
- Accept the license agreement.
- Click **Close**.

In the next few steps we will use composition API’s to apply a saturation effect to this cat image which will remove all saturation. In this model the effect is created and then applied to an image.

![Source image](images/composition-cat-source.png)

### Setting your Composition Basics

```csharp
_compositor = ElementCompositionPreview.GetElementVisual(MyHost).Compositor;
_root = _compositor.CreateContainerVisual();
ElementCompositionPreview.SetElementChildVisual(MyHost, _root);
```

### Creating a CompositionSurface Brush

```csharp
CompositionSurfaceBrush surfaceBrush = _compositor.CreateSurfaceBrush();
LoadedImageSurface imageSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/cat.png"));
surfaceBrush.Surface = imageSurface;
```

### Creating, Compiling and Applying Effects

1. Create the graphics effect.

    ```csharp
    var graphicsEffect = new SaturationEffect
    {
        Saturation = 0.0f,
        Source = new CompositionEffectSourceParameter("mySource")
    };
    ```

1. Compile the effect and create the effect brush.

    ```csharp
    var effectFactory = _compositor.CreateEffectFactory(graphicsEffect);

    var catEffect = effectFactory.CreateBrush();
    catEffect.SetSourceParameter("mySource", surfaceBrush);
    ```

1. Create a SpriteVisual in the composition tree and apply the effect.

    ```csharp
    var catVisual = _compositor.CreateSpriteVisual();
    catVisual.Brush = catEffect;
    catVisual.Size = new Vector2(219, 300);
    _root.Children.InsertAtBottom(catVisual);
    ```

1. Run your app. Your results should be a desaturated cat:

![Desaturated image](images/composition-cat-desaturated.png)

## More Information

- [Microsoft – Composition GitHub](https://github.com/microsoft/WindowsCompositionSamples)
- [**Microsoft.UI.Composition**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition)
- [Composition Overview](https://blogs.windows.com/buildingapps/2015/12/08/awaken-your-creativity-with-the-new-windows-ui-composition/)
- [Visual Tree Basics](composition-visual-tree.md)
- [Composition Brushes](composition-brushes.md)
- [XamlCompositionBrushBase](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.xamlcompositionbrushbase)
- [Animation Overview](composition-animation.md)
- [Composition native DirectX and Direct2D interoperation with BeginDraw and EndDraw](composition-native-interop.md)
