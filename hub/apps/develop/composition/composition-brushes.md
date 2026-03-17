---
title: Composition brushes in WinUI
description: A composition brush paints the area of a Visual with its output in WinUI and Windows App SDK apps.
ms.date: 03/16/2026
ms.topic: article
ms.custom: 19H1
ms.localizationpriority: medium
---
# Composition brushes
Everything visible on your screen in a WinUI app is visible because it was painted by a brush. Brushes enable you to paint user interface (UI) objects with content ranging from simple solid colors to images, drawings, and complex effect chains. This topic introduces the concepts of painting with CompositionBrush.

When working with a WinUI XAML app, you can choose to paint a UIElement with a [XAML brush](/windows/apps/design/style/brushes) or a [CompositionBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbrush). Typically, it is easier to choose a XAML brush if your scenario is already supported by one. For example, animating the color of a button or changing the fill of text or a shape with an image. If you need something that is not supported by a XAML brush, such as an animated mask, an animated nine-grid stretch, or an effect chain, you can use a CompositionBrush to paint a UIElement through [XamlCompositionBrushBase](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.xamlcompositionbrushbase).

When working with the Visual layer, a CompositionBrush must be used to paint the area of a [SpriteVisual](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.spritevisual).

-   [Prerequisites](./composition-brushes.md#prerequisites)
-   [Paint with CompositionBrush](./composition-brushes.md#paint-with-a-compositionbrush)
	-   [Paint with a solid color](./composition-brushes.md#paint-with-a-solid-color)
	-   [Paint with a linear gradient](./composition-brushes.md#paint-with-a-linear-gradient) 
	-   [Paint with a radial gradient](./composition-brushes.md#paint-with-a-radial-gradient)
	-   [Paint with an image](./composition-brushes.md#paint-with-an-image)
	-   [Paint with a custom drawing](./composition-brushes.md#paint-with-a-custom-drawing)
	-   [Paint with a video](./composition-brushes.md#paint-with-a-video)
	-   [Paint with a filter effect](./composition-brushes.md#paint-with-a-filter-effect)
	-   [Paint with a CompositionBrush with an opacity mask](./composition-brushes.md#paint-with-a-compositionbrush-with-opacity-mask-applied)
	-   [Paint with a CompositionBrush using NineGrid stretch](./composition-brushes.md#paint-with-a-compositionbrush-using-ninegrid-stretch)
	-   [Paint using Background Pixels](./composition-brushes.md#paint-using-background-pixels)
-   [Combining CompositionBrushes](./composition-brushes.md#combining-compositionbrushes)
-   [Using a XAML Brush vs. CompositionBrush](./composition-brushes.md#using-a-xaml-brush-vs-compositionbrush)
-   [Related Topics](./composition-brushes.md#related-topics)

## Prerequisites
This overview assumes that you are familiar with the structure of a basic Composition application, as described in the [Visual layer overview](visual-layer.md).

## Paint with a CompositionBrush

A [CompositionBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbrush) "paints" an area with its output. Different brushes have different types of output. Some brushes paint an area with a solid color, others with a gradient, image, custom drawing, or effect. There are also specialized brushes that modify the behavior of other brushes. For example, opacity mask can be used to control which area is painted by a CompositionBrush, or a nine-grid can be used to control the stretch applied to a CompositionBrush when painting an area. CompositionBrush can be of one of the following types:

|Class                                   |Details                                         |
|-------------------------------------|---------------------------------------------------------|
|[CompositionColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioncolorbrush)         |Paints an area with a solid color                        |
|[CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush)       |Paints an area with the contents of an [ICompositionSurface](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.icompositionsurface)|
|[CompositionEffectBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioneffectbrush)        |Paints an area with the contents of a composition effect |
|[CompositionMaskBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionmaskbrush)          |Paints a visual with a CompositionBrush with an opacity mask |
|[CompositionNineGridBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionninegridbrush)      |Paints an area with a CompositionBrush using a NineGrid stretch |
|[CompositionLinearGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionlineargradientbrush)|Paints an area with a linear gradient                    |
|[CompositionRadialGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionradialgradientbrush)|Paints an area with a radial gradient                    |
|[CompositionBackdropBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbackdropbrush)     |Paints an area by sampling background pixels from either the application or pixels directly behind the application's window on desktop. Used as an input to another CompositionBrush like a CompositionEffectBrush |

### Paint with a solid color

A [CompositionColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioncolorbrush) paints an area with a solid color. There are a variety of ways to specify the color of a SolidColorBrush. For example, you can specify its alpha, red, blue, and green (ARGB) channels or use one of the predefined colors provided by the [Colors](/windows/windows-app-sdk/api/winrt/microsoft.ui.colors) class.

The following illustration and code shows a small visual tree to create a rectangle that is stroked with a black color brush and painted with a solid color brush that has the color value of 0x9ACD32.

![CompositionColorBrush](images/composition-compositioncolorbrush.png)

```csharp
Compositor _compositor;
ContainerVisual _container;
SpriteVisual _colorVisual1, _colorVisual2;
CompositionColorBrush _blackBrush, _greenBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
_container = _compositor.CreateContainerVisual();

_blackBrush = _compositor.CreateColorBrush(Colors.Black);
_colorVisual1 = _compositor.CreateSpriteVisual();
_colorVisual1.Brush = _blackBrush;
_colorVisual1.Size = new Vector2(156, 156);
_colorVisual1.Offset = new Vector3(0, 0, 0);
_container.Children.InsertAtBottom(_colorVisual1);

_greenBrush = _compositor.CreateColorBrush(Color.FromArgb(0xff, 0x9A, 0xCD, 0x32));
_colorVisual2 = _compositor.CreateSpriteVisual();
_colorVisual2.Brush = _greenBrush;
_colorVisual2.Size = new Vector2(150, 150);
_colorVisual2.Offset = new Vector3(3, 3, 0);
_container.Children.InsertAtBottom(_colorVisual2);
```

### Paint with a linear gradient

A [CompositionLinearGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionlineargradientbrush) paints an area with a linear gradient. A linear gradient blends two or more colors across a line, the gradient axis. You use GradientStop objects to specify the colors in the gradient and their positions.

The following illustration and code shows a SpriteVisual painted with a LinearGradientBrush with 2 stops using a red and yellow color.

![CompositionLinearGradientBrush](images/composition-compositionlineargradientbrush.png)

```csharp
Compositor _compositor;
SpriteVisual _gradientVisual;
CompositionLinearGradientBrush _redyellowBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

_redyellowBrush = _compositor.CreateLinearGradientBrush();
_redyellowBrush.ColorStops.Add(_compositor.CreateColorGradientStop(0, Colors.Red));
_redyellowBrush.ColorStops.Add(_compositor.CreateColorGradientStop(1, Colors.Yellow));
_gradientVisual = _compositor.CreateSpriteVisual();
_gradientVisual.Brush = _redyellowBrush;
_gradientVisual.Size = new Vector2(156, 156);
```

### Paint with a radial gradient

A [CompositionRadialGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionradialgradientbrush) paints an area with a radial gradient. A radial gradient blends two or more colors with the gradient starting from the center of the ellipse and ending at the ellipse's radius. GradientStop objects are used to define the colors and their location in the gradient.

The following illustration and code shows a SpriteVisual painted with a RadialGradientBrush with 2 GradientStops.

![CompositionRadialGradientBrush](images/radial-gradient-brush.png)

```csharp
Compositor _compositor;
SpriteVisual _gradientVisual;
CompositionRadialGradientBrush RGBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

RGBrush = _compositor.CreateRadialGradientBrush();
RGBrush.ColorStops.Add(_compositor.CreateColorGradientStop(0, Colors.Aquamarine));
RGBrush.ColorStops.Add(_compositor.CreateColorGradientStop(1, Colors.DeepPink));
_gradientVisual = _compositor.CreateSpriteVisual();
_gradientVisual.Brush = RGBrush;
_gradientVisual.Size = new Vector2(200, 200);
```

### Paint with an image

A [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) paints an area with pixels rendered onto an ICompositionSurface. For example, a CompositionSurfaceBrush can be used to paint an area with an image rendered onto an ICompositionSurface by using the [LoadedImageSurface](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.loadedimagesurface) API.

The following illustration and code shows a SpriteVisual painted with a bitmap of a licorice rendered onto an ICompositionSurface using LoadedImageSurface. The properties of CompositionSurfaceBrush can be used to stretch and align the bitmap within the bounds of the visual.

![CompositionSurfaceBrush](images/composition-compositionsurfacebrush.png)

```csharp
Compositor _compositor;
SpriteVisual _imageVisual;
CompositionSurfaceBrush _imageBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

_imageBrush = _compositor.CreateSurfaceBrush();

// The loadedSurface has a size of 0x0 till the image has been downloaded, decoded and loaded to the surface. We can assign the surface to the CompositionSurfaceBrush and it will show up once the image is loaded to the surface.
LoadedImageSurface _loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/licorice.jpg"));
_imageBrush.Surface = _loadedSurface;

_imageVisual = _compositor.CreateSpriteVisual();
_imageVisual.Brush = _imageBrush;
_imageVisual.Size = new Vector2(156, 156);
```

### Paint with a custom drawing
A [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) can also be used to paint an area with pixels from an ICompositionSurface rendered using [Win2D](https://microsoft.github.io/Win2D/html/Introduction.htm) (or D2D).

The following code shows a SpriteVisual painted with a text run rendered onto an ICompositionSurface by using Win2D. To use Win2D with WinUI, install the [Microsoft.Graphics.Win2D NuGet package](https://www.nuget.org/packages/Microsoft.Graphics.Win2D) in your project.

```csharp
Compositor _compositor;
CanvasDevice _device;
CompositionGraphicsDevice _compositionGraphicsDevice;
SpriteVisual _drawingVisual;
CompositionSurfaceBrush _drawingBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
_device = CanvasDevice.GetSharedDevice();
_compositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(_compositor, _device);

_drawingBrush = _compositor.CreateSurfaceBrush();
CompositionDrawingSurface _drawingSurface = _compositionGraphicsDevice.CreateDrawingSurface(
    new Size(256, 256),
    DirectXPixelFormat.B8G8R8A8UIntNormalized,
    DirectXAlphaMode.Premultiplied);

using (var ds = CanvasComposition.CreateDrawingSession(_drawingSurface))
{
    ds.Clear(Colors.Transparent);
    var rect = new Rect(new Point(2, 2), (_drawingSurface.Size.ToVector2() - new Vector2(4, 4)).ToSize());
    ds.FillRoundedRectangle(rect, 15, 15, Colors.LightBlue);
    ds.DrawRoundedRectangle(rect, 15, 15, Colors.Gray, 2);
    ds.DrawText("This is a composition drawing surface", rect, Colors.Black, new CanvasTextFormat()
    {
        FontFamily = "Comic Sans MS",
        FontSize = 32,
        WordWrapping = CanvasWordWrapping.WholeWord,
        VerticalAlignment = CanvasVerticalAlignment.Center,
        HorizontalAlignment = CanvasHorizontalAlignment.Center
    });
}

_drawingBrush.Surface = _drawingSurface;

_drawingVisual = _compositor.CreateSpriteVisual();
_drawingVisual.Brush = _drawingBrush;
_drawingVisual.Size = new Vector2(156, 156);
```

Similarly, the CompositionSurfaceBrush can also be used to paint a SpriteVisual with a SwapChain using Win2D interop. [This sample](https://github.com/Microsoft/Win2D-Samples/tree/master/CompositionExample) provides an example of how to use Win2D to paint a SpriteVisual with a swapchain.

### Paint with a video
A [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) can also be used to paint an area with pixels from an ICompositionSurface rendered using a video loaded through the [MediaPlayer](/uwp/api/Windows.Media.Playback.MediaPlayer) class.

The following code shows a SpriteVisual painted with a video loaded onto an ICompositionSurface.

```csharp
Compositor _compositor;
SpriteVisual _videoVisual;
CompositionSurfaceBrush _videoBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

// MediaPlayer setup with a source URI.
_mediaPlayer = new MediaPlayer();

// Get a source from a URI. This could also come from a file or a stream.
var source = MediaSource.CreateFromUri(new Uri("https://go.microsoft.com/fwlink/?LinkID=809007&clcid=0x409"));
var item = new MediaPlaybackItem(source);
_mediaPlayer.Source = item;
_mediaPlayer.IsLoopingEnabled = true;

// Get the surface from MediaPlayer and put it on a brush.
_videoSurface = _mediaPlayer.GetSurface(_compositor);
_videoBrush = _compositor.CreateSurfaceBrush(_videoSurface.CompositionSurface);

_videoVisual = _compositor.CreateSpriteVisual();
_videoVisual.Brush = _videoBrush;
_videoVisual.Size = new Vector2(156, 156);
```

### Paint with a filter effect

A [CompositionEffectBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioneffectbrush) paints an area with output of a CompositionEffect. Effects in the Visual Layer may be thought of as animatable filter effects applied to a collection of source content such as colors, gradients, images, videos, swapchains, regions of your UI, or trees of Visuals. The source content is typically specified using another CompositionBrush.

The following illustration and code shows a SpriteVisual painted with an image of a cat that has desaturation filter effect applied.

![CompositionEffectBrush](images/composition-cat-desaturated.png)

```csharp
Compositor _compositor;
SpriteVisual _effectVisual;
CompositionEffectBrush _effectBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

var graphicsEffect = new SaturationEffect
{
    Saturation = 0.0f,
    Source = new CompositionEffectSourceParameter("mySource")
};

var effectFactory = _compositor.CreateEffectFactory(graphicsEffect);
_effectBrush = effectFactory.CreateBrush();

CompositionSurfaceBrush surfaceBrush = _compositor.CreateSurfaceBrush();
LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/cat.jpg"));
surfaceBrush.Surface = loadedSurface;

_effectBrush.SetSourceParameter("mySource", surfaceBrush);

_effectVisual = _compositor.CreateSpriteVisual();
_effectVisual.Brush = _effectBrush;
_effectVisual.Size = new Vector2(156, 156);
```

For more information on creating an Effect using CompositionBrushes see [Effects in Visual layer](./composition-effects.md)

### Paint with a CompositionBrush with opacity mask applied

A [CompositionMaskBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionmaskbrush) paints an area with a CompositionBrush with an opacity mask applied to it. The source of the opacity mask can be any CompositionBrush of type CompositionColorBrush, CompositionLinearGradientBrush, CompositionSurfaceBrush, CompositionEffectBrush, or CompositionNineGridBrush. The opacity mask must be specified as a CompositionSurfaceBrush.

The following illustration and code shows a SpriteVisual painted with a CompositionMaskBrush. The source of the mask is a CompositionLinearGradientBrush which is masked to look like a circle using an image of circle as a mask.

![CompositionMaskBrush](images/composition-compositionmaskbrush.png)

```csharp
Compositor _compositor;
SpriteVisual _maskVisual;
CompositionMaskBrush _maskBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

_maskBrush = _compositor.CreateMaskBrush();

CompositionLinearGradientBrush _sourceGradient = _compositor.CreateLinearGradientBrush();
_sourceGradient.ColorStops.Add(_compositor.CreateColorGradientStop(0,Colors.Red));
_sourceGradient.ColorStops.Add(_compositor.CreateColorGradientStop(1,Colors.Yellow));
_maskBrush.Source = _sourceGradient;

LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/circle.png"), new Size(156.0, 156.0));
_maskBrush.Mask = _compositor.CreateSurfaceBrush(loadedSurface);

_maskVisual = _compositor.CreateSpriteVisual();
_maskVisual.Brush = _maskBrush;
_maskVisual.Size = new Vector2(156, 156);
```

### Paint with a CompositionBrush using NineGrid stretch

A [CompositionNineGridBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionninegridbrush) paints an area with a CompositionBrush that is stretched using the nine-grid metaphor. The nine-grid metaphor enables you to stretch edges and corners of a CompositionBrush differently than its center. The source of the nine-grid stretch can by any CompositionBrush of type CompositionColorBrush, CompositionSurfaceBrush, or CompositionEffectBrush.

The following code shows a SpriteVisual painted with a CompositionNineGridBrush. The source of the mask is a CompositionSurfaceBrush which is stretched using a Nine-Grid.

```csharp
Compositor _compositor;
SpriteVisual _nineGridVisual;
CompositionNineGridBrush _nineGridBrush;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

_nineGridBrush = _compositor.CreateNineGridBrush();

// nineGridImage.png is 50x50 pixels; nine-grid insets, as measured relative to the actual size of the image, are: left = 1, top = 5, right = 10, bottom = 20 (in pixels)
LoadedImageSurface _imageSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/nineGridImage.png"));
CompositionSurfaceBrush sourceBrush = _compositor.CreateSurfaceBrush(_imageSurface);
_nineGridBrush.Source = sourceBrush;

// Set nine-grid insets.
_nineGridBrush.SetInsets(1, 5, 10, 20);

// Set the appropriate stretch on the SurfaceBrush for the center of the nine-grid.
sourceBrush.Stretch = CompositionStretch.Fill;

_nineGridVisual = _compositor.CreateSpriteVisual();
_nineGridVisual.Brush = _nineGridBrush;
_nineGridVisual.Size = new Vector2(100, 75);
```

### Paint using Background Pixels

A [CompositionBackdropBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbackdropbrush)  paints an area with the content behind the area. A CompositionBackdropBrush is never used on its own, but instead is used as an input to another CompositionBrush like an EffectBrush. For example, by using a CompositionBackdropBrush as an input to a Blur effect, you can achieve a frosted glass effect.

The following code shows a small visual tree to create an image using CompositionSurfaceBrush and a frosted glass overlay above the image. The frosted glass overlay is created by placing a SpriteVisual filled with an EffectBrush above the image. The EffectBrush uses a CompositionBackdropBrush as an input to the blur effect.

```csharp
Compositor _compositor;
ContainerVisual _containerVisual;
SpriteVisual _imageVisual;
SpriteVisual _backdropVisual;

_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

// Create a container visual to host the visual tree.
_containerVisual = _compositor.CreateContainerVisual();

// Create _imageVisual and add it to the bottom of the container visual.
CompositionSurfaceBrush _licoriceBrush = _compositor.CreateSurfaceBrush();
LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/licorice.jpg"));
_licoriceBrush.Surface = loadedSurface;

_imageVisual = _compositor.CreateSpriteVisual();
_imageVisual.Brush = _licoriceBrush;
_imageVisual.Size = new Vector2(156, 156);
_imageVisual.Offset = new Vector3(0, 0, 0);
_containerVisual.Children.InsertAtBottom(_imageVisual);

// Create a SpriteVisual and add it to the top of the container visual.
// Paint the visual with an EffectBrush that applies blur to the content underneath it.
GaussianBlurEffect blurEffect = new GaussianBlurEffect()
{
    Name = "Blur",
    BlurAmount = 1.0f,
    BorderMode = EffectBorderMode.Hard,
    Source = new CompositionEffectSourceParameter("source")
};

CompositionEffectFactory blurEffectFactory = _compositor.CreateEffectFactory(blurEffect);
CompositionEffectBrush _backdropBrush = blurEffectFactory.CreateBrush();

// Create a BackdropBrush and bind it to the EffectSourceParameter source.
_backdropBrush.SetSourceParameter("source", _compositor.CreateBackdropBrush());

_backdropVisual = _compositor.CreateSpriteVisual();
_backdropVisual.Brush = _backdropBrush;
_backdropVisual.Size = new Vector2(78, 78);
_backdropVisual.Offset = new Vector3(39, 39, 0);
_containerVisual.Children.InsertAtTop(_backdropVisual);
```

## Combining CompositionBrushes
A number of CompositionBrushes use other CompositionBrushes as inputs. For example, using the SetSourceParameter method can be used to set another CompositionBrush as an input to a CompositionEffectBrush. The table below outlines the supported combinations of CompositionBrushes. Note, that using an unsupported combination will throw an exception.

<table>
<tbody>
<tr>
<th>Brush</th>
<th>EffectBrush.SetSourceParameter()</th>
<th>MaskBrush.Mask</th>
<th>MaskBrush.Source</th>
<th>NineGridBrush.Source</th>
</tr>
<tr>
<td>CompositionColorBrush</td>
<td>YES</td>
<td>YES</td>
<td>YES</td>
<td>YES</td>
</tr>
<tr>
<td>CompositionLinear<br />GradientBrush</td>
<td>YES</td>
<td>YES</td>
<td>YES</td>
<td>NO</td>
</tr>
<tr>
<td>CompositionSurfaceBrush</td>
<td>YES</td>
<td>YES</td>
<td>YES</td>
<td>YES</td>
</tr>
<tr>
<td>CompositionEffectBrush</td>
<td>NO</td>
<td>NO</td>
<td>YES</td>
<td>NO</td>
</tr>
<tr>
<td>CompositionMaskBrush</td>
<td>NO</td>
<td>NO</td>
<td>NO</td>
<td>NO</td>
</tr>
<tr>
<td>CompositionNineGridBrush</td>
<td>YES</td>
<td>YES</td>
<td>YES</td>
<td>NO</td>
</tr>
<tr>
<td>CompositionBackdropBrush</td>
<td>YES</td>
<td>NO</td>
<td>NO</td>
<td>NO</td>
</tr>
</tbody>
</table>


## Using a XAML Brush vs. CompositionBrush

The following table provides a list of scenarios and whether XAML or Composition brush use is prescribed when painting a UIElement or a SpriteVisual in your application. 

> [!NOTE]
> If a CompositionBrush is suggested for a XAML UIElement, it is assumed that the CompositionBrush is packaged using a XamlCompositionBrushBase.

| Scenario | XAML UIElement | Composition SpriteVisual |
|---|---|---|
| Paint an area with solid color | [SolidColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.solidcolorbrush) | [CompositionColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioncolorbrush) |
| Paint an area with animated color | [SolidColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.solidcolorbrush) | [CompositionColorBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioncolorbrush) |
| Paint an area with a static gradient | [LinearGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.lineargradientbrush) | [CompositionLinearGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionlineargradientbrush) |
| Paint an area with animated gradient stops | [CompositionLinearGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionlineargradientbrush) | [CompositionLinearGradientBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionlineargradientbrush) |
| Paint an area with an image | [ImageBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imagebrush) | [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) |
| Paint an area with a webpage | WebView2 | N/A |
| Paint an area with an image using NineGrid stretch | [Image Control](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) | [CompositionNineGridBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionninegridbrush) |
| Paint an area with animated NineGrid stretch | [CompositionNineGridBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionninegridbrush) | [CompositionNineGridBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionninegridbrush) |
| Paint an area with a swapchain | [SwapChainPanel](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.swapchainpanel) | [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) w/ swapchain interop |
| Paint an area with a video | MediaPlayerElement | [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) w/ media interop |
| Paint an area with custom 2D drawing | [CanvasControl](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm) from Win2D | [CompositionSurfaceBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) w/ Win2D interop |
| Paint an area with non-animated mask | Use XAML [shapes](/windows/apps/design/controls/shapes) to define a mask | [CompositionMaskBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionmaskbrush) |
| Paint an area with an animated mask | [CompositionMaskBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionmaskbrush) | [CompositionMaskBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionmaskbrush) |
| Paint an area with an animated filter effect | [CompositionEffectBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioneffectbrush) | [CompositionEffectBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioneffectbrush) |
| Paint an area with an effect applied to background pixels | [CompositionBackdropBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbackdropbrush) | [CompositionBackdropBrush](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbackdropbrush) |

## Related Topics

[Composition native DirectX and Direct2D interop with BeginDraw and EndDraw](composition-native-interop.md)

[XAML brush interop with XamlCompositionBrushBase](/windows/apps/design/style/brushes#xamlcompositionbrushbase)
