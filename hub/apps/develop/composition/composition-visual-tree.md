---
title: Composition visuals
description: Composition visuals make up the visual tree structure that Microsoft.UI.Composition uses in WinUI and Windows App SDK apps. You can create one or many visual objects, each representing a single node in a visual tree.
ms.date: 03/16/2026
ms.topic: article
ms.localizationpriority: medium
---
# Composition visuals

Composition Visuals make up the visual tree structure that all other features of the Microsoft.UI.Composition API use and build on. The API allows developers to define and create one or many visual objects, each representing a single node in a visual tree.

## Visuals

There are several visual types that make up the visual tree structure plus a base brush class with multiple subclasses that affect the content of a visual:

- [**Visual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual) – base object, the majority of the properties are here, and inherited by the other Visual objects.
- [**ContainerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.containervisual) – derives from [**Visual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual), and adds the ability to create children.
  - [**SpriteVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.spritevisual) – derives from [**ContainerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.containervisual). Has the ability to associate a brush so that the Visual can render pixels including images, effects, or a solid color.
  - [**LayerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.layervisual) – derives from [**ContainerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.containervisual). Children of the visual are flattened into a single layer.
  - [**ShapeVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.shapevisual) – derives from [**ContainerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.containervisual). A visual tree node that is the root of a CompositionShape.
  - [**RedirectVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.redirectvisual) – derives from [**ContainerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.containervisual). The visual gets its content from another visual.
  - [**SceneVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.scenes.scenevisual) – derives from [**ContainerVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.containervisual). A container visual for the nodes of a 3D scene.

You can apply content and effects to SpriteVisuals using the [**CompositionBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionbrush) and its subclasses including the [**CompositionColorBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioncolorbrush), [**CompositionSurfaceBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositionsurfacebrush) and [**CompositionEffectBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioneffectbrush). To learn more about brushes see [**CompositionBrush Overview**](./composition-brushes.md).

## The CompositionVisual Sample

Here, we'll look at some WinUI 3 sample code that demonstrates the three different visual types listed previously. While this sample doesn't cover concepts like animations or more complex effects, it contains the building blocks that all of those systems use. (The full sample code is listed at the end of this article.)

In the sample are a number of solid color squares that can be clicked on and dragged about the screen. When a square is clicked on, it will come to the front, rotate 45 degrees, and become opaque when dragged about.

This shows a number of basic concepts for working with the API including:

- Creating a compositor
- Creating a SpriteVisual with a CompositionColorBrush
- Clipping the Visual
- Rotating the Visual
- Setting Opacity
- Changing the Visual's position in the collection.

## Creating a Compositor

Creating a [**Compositor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositor) and storing it in a variable for use as a factory is a simple task. In a WinUI app, you typically retrieve the compositor from a XAML element that is already connected to the visual tree:

```csharp
Compositor compositor = ElementCompositionPreview.GetElementVisual(MyHost).Compositor;
```

If you need a compositor and do not have a UIElement available, you can use `CompositionTarget.GetCompositorForCurrentThread()` instead.

## Creating a SpriteVisual and ColorBrush

Using the [**Compositor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositor) it's easy to create objects whenever you need them, such as a [**SpriteVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.spritevisual) and a [**CompositionColorBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositioncolorbrush):

```csharp
var visual = _compositor.CreateSpriteVisual();
visual.Brush = _compositor.CreateColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
```

While this is only a few lines of code, it demonstrates a powerful concept: [**SpriteVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.spritevisual) objects are the heart of the effects system. The **SpriteVisual** allows for great flexibility and interplay in color, image and effect creation. The **SpriteVisual** is a single visual type that can fill a 2D rectangle with a brush, in this case, a solid color.

## Clipping a Visual

The [**Compositor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.compositor) can also be used to create clips to a [**Visual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual). Below is an example from the sample of using the [**InsetClip**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.insetclip) to trim each side of the visual:

```csharp
var clip = _compositor.CreateInsetClip();
clip.LeftInset = 1.0f;
clip.RightInset = 1.0f;
clip.TopInset = 1.0f;
clip.BottomInset = 1.0f;
_currentVisual.Clip = clip;
```

Like other objects in the API, [**InsetClip**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.insetclip) can have animations applied to its properties.

## <span id="Rotating_a_Clip"></span><span id="rotating_a_clip"></span><span id="ROTATING_A_CLIP"></span>Rotating a Clip

A [**Visual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual) can be transformed with a rotation. Note that [**RotationAngle**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual.rotationangle) supports both radians and degrees. It defaults to radians, but it's easy to specify degrees as shown in the following snippet:

```csharp
child.RotationAngleInDegrees = 45.0f;
```

Rotation is just one example of a set of transform components provided by the API to make these tasks easier. Others include Offset, Scale, Orientation, RotationAxis, and 4x4 TransformMatrix.

## Setting Opacity

Setting the opacity of a visual is a simple operation using a float value. For example, in the sample all the squares start at .8 opacity:

```csharp
visual.Opacity = 0.8f;
```

Like rotation, the [**Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual.opacity) property can be animated.

## Changing the Visual's position in the collection

The Composition API allows for a Visual's position in a [**VisualCollection**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visualcollection) to be changed in a number of ways. It can be placed above another Visual with [**InsertAbove**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visualcollection.insertabove), placed below with [**InsertBelow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visualcollection.insertbelow), moved to the top with [**InsertAtTop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visualcollection.insertattop), or the bottom with [**InsertAtBottom**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visualcollection.insertatbottom).

In the sample, a [**Visual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual) that has been clicked is sorted to the top:

```csharp
parent.Children.InsertAtTop(_currentVisual);
```

## Full Example

In the full WinUI sample, all of the concepts above are used together to construct and walk a simple tree of [**Visual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.visual) objects to change opacity without custom DirectX rendering. The sample uses a WinUI `Grid` for pointer input and hosts the composition tree with `ElementCompositionPreview`.

```xaml
<Page
    x:Class="CompositionVisualSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Grid
        x:Name="MyHost"
        Background="Transparent"
        PointerPressed="OnPointerPressed"
        PointerMoved="OnPointerMoved"
        PointerReleased="OnPointerReleased" />
</Page>
```

```csharp
using System;
using System.Numerics;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Windows.Foundation;
using Windows.UI;

namespace CompositionVisualSample
{
    public sealed partial class MainPage : Page
    {
        private readonly Random _random = new();
        private Compositor _compositor;
        private ContainerVisual _root;
        private ContainerVisual _currentVisual;
        private Vector2 _offsetBias;
        private bool _dragging;

        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_root != null)
            {
                return;
            }

            _compositor = ElementCompositionPreview.GetElementVisual(MyHost).Compositor;
            _root = _compositor.CreateContainerVisual();
            ElementCompositionPreview.SetElementChildVisual(MyHost, _root);

            for (int index = 0; index < 20; index++)
            {
                _root.Children.InsertAtTop(CreateChildElement());
            }
        }

        private ContainerVisual CreateChildElement()
        {
            var element = _compositor.CreateContainerVisual();
            element.Size = new Vector2(100.0f, 100.0f);

            var maxX = (float)Math.Max(MyHost.ActualWidth - element.Size.X, 0.0);
            var maxY = (float)Math.Max(MyHost.ActualHeight - element.Size.Y, 0.0);
            element.Offset = new Vector3(
                (float)(_random.NextDouble() * maxX),
                (float)(_random.NextDouble() * maxY),
                0.0f);

            var outer = _compositor.CreateSpriteVisual();
            outer.Size = new Vector2(100.0f, 100.0f);
            outer.Brush = _compositor.CreateColorBrush(Colors.White);
            element.Children.InsertAtTop(outer);

            var inner = _compositor.CreateSpriteVisual();
            inner.Offset = new Vector3(3.0f, 3.0f, 0.0f);
            inner.Size = new Vector2(94.0f, 94.0f);

            byte red = (byte)(0xFF * (0.2f + (_random.NextDouble() / 0.8f)));
            byte green = (byte)(0xFF * (0.2f + (_random.NextDouble() / 0.8f)));
            byte blue = (byte)(0xFF * (0.2f + (_random.NextDouble() / 0.8f)));
            inner.Brush = _compositor.CreateColorBrush(Color.FromArgb(0xFF, red, green, blue));
            outer.Children.InsertAtTop(inner);

            element.Opacity = 0.8f;
            return element;
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Point position = e.GetCurrentPoint(MyHost).Position;
            _currentVisual = null;

            foreach (var child in _root.Children)
            {
                Vector3 offset = child.Offset;
                Vector2 size = child.Size;

                if ((position.X >= offset.X) &&
                    (position.X < offset.X + size.X) &&
                    (position.Y >= offset.Y) &&
                    (position.Y < offset.Y + size.Y))
                {
                    _currentVisual = child as ContainerVisual;
                    _offsetBias = new Vector2(
                        (float)(offset.X - position.X),
                        (float)(offset.Y - position.Y));
                }
            }

            if (_currentVisual != null)
            {
                ContainerVisual parent = (ContainerVisual)_currentVisual.Parent;
                parent.Children.Remove(_currentVisual);
                parent.Children.InsertAtTop(_currentVisual);
            }
        }

        private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_currentVisual == null)
            {
                return;
            }

            if (!_dragging)
            {
                _currentVisual.Opacity = 1.0f;

                foreach (var child in _currentVisual.Children)
                {
                    child.RotationAngleInDegrees = 45.0f;
                    child.CenterPoint = new Vector3(_currentVisual.Size.X / 2, _currentVisual.Size.Y / 2, 0.0f);
                    break;
                }

                var clip = _compositor.CreateInsetClip();
                clip.LeftInset = 1.0f;
                clip.RightInset = 1.0f;
                clip.TopInset = 1.0f;
                clip.BottomInset = 1.0f;
                _currentVisual.Clip = clip;

                _dragging = true;
            }

            Point position = e.GetCurrentPoint(MyHost).Position;
            _currentVisual.Offset = new Vector3(
                (float)(position.X + _offsetBias.X),
                (float)(position.Y + _offsetBias.Y),
                0.0f);
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (_currentVisual == null)
            {
                return;
            }

            if (_dragging)
            {
                foreach (var child in _currentVisual.Children)
                {
                    child.RotationAngle = 0.0f;
                    child.CenterPoint = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                }

                _currentVisual.Opacity = 0.8f;
                _currentVisual.Clip = null;
                _dragging = false;
            }

            _currentVisual = null;
        }
    }
}
```

If you need to initialize a compositor before you have a XAML host element, use `CompositionTarget.GetCompositorForCurrentThread()` and then attach the visuals to your UI when a host element becomes available.
