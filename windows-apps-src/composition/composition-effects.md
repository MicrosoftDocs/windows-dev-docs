---
ms.assetid: 6e9b9ff2-234b-6f63-0975-1afb2d86ba1a
title: Composition effects
description: The effect APIs enable developers to customize how their UI is rendered.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Composition effects

The [**Windows.UI.Composition**](/uwp/api/Windows.UI.Composition) APIs allows real-time effects to be applied to images and UI with animatable effect properties. In this overview, we’ll run through the functionality available that allows effects to be applied to a composition visual.

To support [Universal Windows Platform (UWP)](../get-started/universal-application-platform-guide.md) consistency for developers describing effects in their applications, composition effects leverage Win2D’s IGraphicsEffect interface to use effect descriptions via the [Microsoft.Graphics.Canvas.Effects](https://microsoft.github.io/Win2D/html/N_Microsoft_Graphics_Canvas_Effects.htm) Namespace.

Brush effects are used to paint areas of an application by applying effects to a set of existing images. Windows 10 composition effect APIs are focused on Sprite Visuals. The SpriteVisual allows for flexibility and interplay in color, image and effect creation. The SpriteVisual is a composition visual type that can fill a 2D rectangle with a brush. The visual defines the bounds of the rectangle and the brush defines the pixels used to paint the rectangle.

Effect brushes are used on composition tree visuals whose content comes from the output of an effect graph. Effects can reference existing surfaces/textures, but not the output of other composition trees.

Effects can also be applied to XAML UIElements using an effect brush with [**XamlCompositionBrushBase**](/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase).

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
| 2D affine transform  | Applies a 2D affine transform matrix to an image. We used this effect to animate alpha mask in our effect [samples](https://github.com/microsoft/WindowsCompositionSamples/tree/master/Demos/Reference Demos/BasicCompositonEffects).       |
| Arithmetic composite | Combines two images using a flexible equation. We used arithmetic composite to create a crossfade effect in our [samples](https://github.com/microsoft/WindowsCompositionSamples/tree/master/Demos/Reference Demos/BasicCompositonEffects). |
| Blend effect         | Creates a blend effect that combines two images. Composition provides 21 of the 26 [blend modes](https://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Effects_BlendEffectMode.htm) supported in Win2D.        |
| Color source         | Generates an image containing a solid color.                                                                                                                                                                               |
| Composite            | Combines two images. Composition provides all 13 [composite modes](https://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_CanvasComposite.htm) supported in Win2D.                                              |
| Contrast             | Increases or decreases the contrast of an image.                                                                                                                                                                           |
| Exposure             | Increases or decreases the exposure of an image.                                                                                                                                                                           |
| Grayscale            | Converts an image to monochromatic gray.                                                                                                                                                                                   |
| Gamma transfer       | Alters the colors of an image by applying a per-channel gamma transfer function.                                                                                                                                           |
| Hue rotate           | Alters the color of an image by rotating its hue values.                                                                                                                                                                   |
| Invert               | Inverts the colors of an image.                                                                                                                                                                                            |
| Saturate             | Alters the saturation of an image.                                                                                                                                                                                         |
| Sepia                | Converts an image to sepia tones.                                                                                                                                                                                          |
| Temperature and tint | Adjusts the temperature and/or tint of an image.                                                                                                                                                                           |

See Win2D’s [Microsoft.Graphics.Canvas.Effects](https://microsoft.github.io/Win2D/html/N_Microsoft_Graphics_Canvas_Effects.htm) Namespace for more detailed information. Effects not supported in composition are noted as \[NoComposition\].

### Chaining Effects

Effects can be chained, allowing an application to simultaneously use multiple effects on an image. Effect graphs can support multiple effects that can refer to one and other. When describing your effect, simply add an effect as input to your effect.

```cs
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
}
```

The example above describes an arithmetic composite effect which has two inputs. The second input has a saturation effect with a .5 saturation property.

### Animation Support

Effect properties support animation, during effect compilation you can specify effect properties can be animated and which can be "baked in" as constants. The animatable properties are specified through strings of the form “effect name.property name”. These properties can be animated independently over multiple instantiations of the effect.

### Constant vs Animated Effect Properties

During effect compilation you can specify effect properties as dynamic or as properties that are "baked in" as constants. The dynamic properties are specified through strings of the form “<effect name>.<property name>”. The dynamic properties can be set to a specific value or can be animated using the composition animation system.

When compiling the effect description above, you have the flexibility of either baking in saturation to be equal to 0.5 or making it dynamic and setting it dynamically or animating it.

Compiling an effect with saturation baked in:

```cs
var effectFactory = _compositor.CreateEffectFactory(graphicsEffect);
```

Compiling an effect with dynamic saturation:

```cs
var effectFactory = _compositor.CreateEffectFactory(graphicsEffect, new[]{"SaturationEffect.Saturation"});
_catEffect = effectFactory.CreateBrush();
_catEffect.SetSourceParameter("mySource", surfaceBrush);
_catEffect.Properties.InsertScalar("saturationEffect.Saturation", 0f);
```

The saturation property of the effect above can then be either set to a static value or animated using either Expression or ScalarKeyFrame animations.

You can create a ScalarKeyFrame that will be used to animate the Saturation property of an effect like this:

```cs
ScalarKeyFrameAnimation effectAnimation = _compositor.CreateScalarKeyFrameAnimation();
            effectAnimation.InsertKeyFrame(0f, 0f);
            effectAnimation.InsertKeyFrame(0.50f, 1f);
            effectAnimation.InsertKeyFrame(1.0f, 0f);
            effectAnimation.Duration = TimeSpan.FromMilliseconds(2500);
            effectAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
```

Start the animation on the Saturation property of the effect like this:

```cs
catEffect.Properties.StartAnimation("saturationEffect.Saturation", effectAnimation);
```

See the [Desaturation - Animation sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/Demos/Reference Demos/BasicCompositonEffects/Desaturation - Animation) for effect properties animated with key frames and the [AlphaMask sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/Demos/Reference Demos/BasicCompositonEffects/AlphaMask) for use of effects and expressions.

### Multiple Effect Instances with Independent Properties

By specifying that a parameter should be dynamic during effect compilation, the parameter can then be changed on a per-effect instance basis. This allows two Visuals to use the same effect but be rendered with different effect properties. See the ColorSource and Blend [sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/Demos/Reference Demos/BasicCompositonEffects/ColorSource and Blend) for more information.

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

- Go to File->New->Project...
- Select 'Visual C#'
- Create a 'Blank App (Windows Universal)' (Visual Studio 2015)
- Enter a project name of your choosing
- Click 'OK'

### Installing Win2D

Win2D is released as a Nuget.org package and needs to be installed before you can use effects.

There are two package versions, one for Windows 10 and one for Windows 8.1. For Composition effects you’ll use the Windows 10 version.

- Launch the NuGet Package Manager by going to Tools → NuGet Package Manager → Manage NuGet Packages for Solution.
- Search for "Win2D" and select the appropriate package for your target version of Windows. Because Windows.UI. Composition supports Windows 10 (not 8.1), select Win2D.uwp.
- Accept the license agreement
- Click 'Close'

In the next few steps we will use composition API’s to apply a saturation effect to this cat image which will remove all saturation. In this model the effect is created and then applied to an image.

![Source image](images/composition-cat-source.png)
### Setting your Composition Basics

See the [Composition Visual Tree Sample](https://github.com/microsoft/WindowsCompositionSamples/tree/master/Demos/Reference Demos/CompositionImageSample) on our GitHub for an example of how to set up Windows.UI.Composition Compositor, root ContainerVisual, and associate with the Core Window.

```cs
_compositor = new Compositor();
_root = _compositor.CreateContainerVisual();
_target = _compositor.CreateTargetForCurrentView();
_target.Root = _root;
_imageFactory = new CompositionImageFactory(_compositor)
Desaturate();
```

### Creating a CompositionSurface Brush

```cs
CompositionSurfaceBrush surfaceBrush = _compositor.CreateSurfaceBrush();
LoadImage(surfaceBrush);
```

### Creating, Compiling and Applying Effects

1. Create the graphics effect

    ```cs
    var graphicsEffect = new SaturationEffect
    {
      Saturation = 0.0f,
      Source = new CompositionEffectSourceParameter("mySource")
    };
    ```

1. Compile the effect and create effect brush

    ```cs
    var effectFactory = _compositor.CreateEffectFactory(graphicsEffect);

    var catEffect = effectFactory.CreateBrush();
    catEffect.SetSourceParameter("mySource", surfaceBrush);
    ```

1. Create a SpriteVisual in the composition tree and apply the effect

    ```cs
    var catVisual = _compositor.CreateSpriteVisual();
      catVisual.Brush = catEffect;
      catVisual.Size = new Vector2(219, 300);
      _root.Children.InsertAtBottom(catVisual);
    }
    ```

1. Create your image source to load.

    ```cs
    CompositionImage imageSource = _imageFactory.CreateImageFromUri(new Uri("ms-appx:///Assets/cat.png"));
    CompositionImageLoadResult result = await imageSource.CompleteLoadAsync();
    if (result.Status == CompositionImageLoadStatus.Success)
    ```

1. Size and brush the surface on the SpriteVisual

    ```cs
    brush.Surface = imageSource.Surface;
    ```

1. Run your app – your results should be a desaturated cat:

![Desaturated image](images/composition-cat-desaturated.png)

## More Information

- [Microsoft – Composition GitHub](https://github.com/microsoft/WindowsCompositionSamples)
- [**Windows.UI.Composition**](/uwp/api/Windows.UI.Composition)
- [Windows Composition team on Twitter](https://twitter.com/wincomposition)
- [Composition Overview](https://blogs.windows.com/buildingapps/2015/12/08/awaken-your-creativity-with-the-new-windows-ui-composition/)
- [Visual Tree Basics](composition-visual-tree.md)
- [Composition Brushes](composition-brushes.md)
- [XamlCompositionBrushBase](/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase)
- [Animation Overview](composition-animation.md)
- [Composition native DirectX and Direct2D interoperation with BeginDraw and EndDraw](composition-native-interop.md)