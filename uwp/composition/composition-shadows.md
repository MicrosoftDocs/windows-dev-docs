---
title: Composition Shadows
description: The shadow APIs let you add dynamic customizable shadows to UI content.
ms.date: 07/16/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Shadows in Windows UI

The [DropShadow](/uwp/api/Windows.UI.Composition.DropShadow) class provides means of creating a configurable shadow that can be applied to a [SpriteVisual](/uwp/api/windows.ui.composition.spritevisual) or [LayerVisual](/uwp/api/windows.ui.composition.layervisual) (subtree of Visuals). As is customary for objects in the Visual Layer, all properties of the DropShadow can be animated using CompositionAnimations.

## Basic drop shadow

To create a basic shadow, simply create a new DropShadow and associate it to your visual. The shadow is rectangular by default. A standard set of properties are available to tweak the look and feel of your shadow.

```cs
var basicRectVisual = _compositor.CreateSpriteVisual();
basicRectVisual.Brush = _compositor.CreateColorBrush(Colors.Blue);
basicRectVisual.Offset = new Vector3(100, 100, 20);
basicRectVisual.Size = new Vector2(300, 300);

var basicShadow = _compositor.CreateDropShadow();
basicShadow.BlurRadius = 25f;
basicShadow.Offset = new Vector3(20, 20, 20);

basicRectVisual.Shadow = basicShadow;
```

![Rectangular Visual with Basic DropShadow](images/rectangular-dropshadow.png)

## Shaping the shadow

There are a few ways to define the shape for your DropShadow:

- **Use the default** - By default the DropShadow shape is defined by the ‘Default’ mode on CompositionDropShadowSourcePolicy. For SpriteVisual, the Default is Rectangular unless a mask is provided. For LayerVisual, Default is to inherit a mask using the alpha of the visual’s brush.
- **Set a mask** – You may set the [Mask](/uwp/api/windows.ui.composition.dropshadow.mask) property to define an opacity mask for the shadow.
- **Specify to use Inherited mask** – Set the [SourcePolicy](/uwp/api/windows.ui.composition.dropshadow.sourcepolicy) property to use [CompositionDropShadowSourcePolicy](/uwp/api/windows.ui.composition.compositiondropshadowsourcepolicy). InheritFromVisualContent to use the mask generated from the alpha of the visual’s brush.

## Masking to match your content

If you want your shadow to match the Visual’s content you can either use the Visual’s brush for your Shadow mask property, or set the shadow to automatically inherit mask from the content. If using a LayerVisual, the shadow will inherit the mask by default.

```cs
var imageSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/myImage.png"));
var imageBrush = _compositor.CreateSurfaceBrush(imageSurface);

var imageSpriteVisual = _compositor.CreateSpriteVisual();
imageSpriteVisual.Size = new Vector2(400,400);
imageSpriteVisual.Offset = new Vector3(100, 500, 20);
imageSpriteVisual.Brush = imageBrush;

var shadow = _compositor.CreateDropShadow();
shadow.Mask = imageBrush;
// or use shadow.SourcePolicy = CompositionDropShadowSourcePolicy.InheritFromVisualContent;
shadow.BlurRadius = 25f;
shadow.Offset = new Vector3(20, 20, 20);

imageSpriteVisual.Shadow = shadow;
```

![Connected web image with masked drop shadow](images/ms-brand-web-dropshadow.png)

## Using an alternative mask

In some cases, you may want to shape the shadow such that it doesn’t match your Visual’s content. To achieve this effect, you’ll need to explicitly set the Mask property using a brush with alpha.

In the below example, we load two surfaces - one for the Visual content and one for the Shadow mask:

```cs
var imageSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/myImage.png"));
var imageBrush = _compositor.CreateSurfaceBrush(imageSurface);

var circleSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/myCircleImage.png"));
var customMask = _compositor.CreateSurfaceBrush(circleSurface);

var imageSpriteVisual = _compositor.CreateSpriteVisual();
imageSpriteVisual.Size = new Vector2(400,400);
imageSpriteVisual.Offset = new Vector3(100, 500, 20);
imageSpriteVisual.Brush = imageBrush;

var shadow = _compositor.CreateDropShadow();
shadow.Mask = customMask;
shadow.BlurRadius = 25f;
shadow.Offset = new Vector3(20, 20, 20);

imageSpriteVisual.Shadow = shadow;
```

![Connected web image with circle masked drop shadow](images/ms-brand-web-masked-dropshadow.png)

## Animating

As is standard in the Visual Layer, DropShadow properties can be animated using Composition Animations. Below, we modify the code from the sprinkles sample above to animate the blur radius for the shadow.

```cs
ScalarKeyFrameAnimation blurAnimation = _compositor.CreateScalarKeyFrameAnimation();
blurAnimation.InsertKeyFrame(0.0f, 25.0f);
blurAnimation.InsertKeyFrame(0.7f, 50.0f);
blurAnimation.InsertKeyFrame(1.0f, 25.0f);
blurAnimation.Duration = TimeSpan.FromSeconds(4);
blurAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
shadow.StartAnimation("BlurRadius", blurAnimation);
```

## Shadows in XAML

If you want to add a shadow to more complex framework elements, there are a couple ways to interop with shadows between XAML and Composition:

1. Use the [DropShadowPanel](https://github.com/windows-toolkit/WindowsCommunityToolkit/blob/master/Microsoft.Toolkit.Uwp.UI.Controls/DropShadowPanel/DropShadowPanel.Properties.cs) available in the Windows Community Toolkit. See the [DropShadowPanel documentation](/windows/uwpcommunitytoolkit/controls/DropShadowPanel) for details on how to use it.
1. Create a Visual to use as the shadow host & tie it to the XAML handout Visual.
1. Use the Composition Sample Gallery’s [SamplesCommon](https://github.com/microsoft/WindowsCompositionSamples/tree/master/SamplesCommon/SamplesCommon) custom CompositionShadow control. See the example here for usage.

## Performance

Although the Visual Layer has many optimizations in place to make effects efficient and usable, generating shadows can be a relatively expensive operation depending on what options you set. Below are high level 'costs' for different types of shadows. Note that although certain shadows may be expensive they may still be appropriate to use sparingly in certain scenarios.

Shadow Characteristics| Cost
------------- | -------------
Rectangular    | Low
Shadow.Mask      | High
CompositionDropShadowSourcePolicy.InheritFromVisualContent | High
Static Blur Radius | Low
Animating Blur Radius | High

## Additional Resources

- [Composition DropShadow API](/uwp/api/Windows.UI.Composition.DropShadow)
- [WindowsUIDevLabs GitHub Repo](https://github.com/microsoft/WindowsCompositionSamples)