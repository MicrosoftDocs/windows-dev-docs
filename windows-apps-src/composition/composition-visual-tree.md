---

ms.assetid: f1297b7d-1a10-52ae-dd84-6d1ad2ae2fe6
title: Composition visual
description: Composition Visuals make up the visual tree structure which all other features of the composition API use and build on. The API allows developers to define and create one or many visual objects each representing a single node in a visual tree.

ms.date: 02/08/2017
ms.topic: article


keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Composition visual

Composition Visuals make up the visual tree structure which all other features of the composition API use and build on. The API allows developers to define and create one or many visual objects each representing a single node in a visual tree.

## Visuals

There are three visual types that make up the visual tree structure plus a base brush class with multiple subclasses that affect the content of a visual:

- [**Visual**](/uwp/api/Windows.UI.Composition.Visual) – base object, the majority of the properties are here, and inherited by the other Visual objects.
- [**ContainerVisual**](/uwp/api/Windows.UI.Composition.ContainerVisual) – derives from [**Visual**](/uwp/api/Windows.UI.Composition.Visual), and adds the ability to create children.
- [**SpriteVisual**](/uwp/api/Windows.UI.Composition.SpriteVisual) – derives from [**ContainerVisual**](/uwp/api/Windows.UI.Composition.ContainerVisual) and adds the ability to associate a brush so that the Visual can render pixels including images, effects or a solid color.

You can apply content and effects to SpriteVisuals using the [**CompositionBrush**](/uwp/api/Windows.UI.Composition.CompositionBrush) and its subclasses including the [**CompositionColorBrush**](/uwp/api/Windows.UI.Composition.CompositionColorBrush), [**CompositionSurfaceBrush**](/uwp/api/Windows.UI.Composition.CompositionSurfaceBrush) and [**CompositionEffectBrush**](/uwp/api/Windows.UI.Composition.CompositionEffectBrush). To learn more about brushes see our [**CompositionBrush Overview**](./composition-brushes.md).

## The CompositionVisual Sample

Here, we'll look at some sample code that demonstrates the three different visual types listed previously. While this sample doesn’t cover concepts like Animations or more complex effects, it contains the building blocks that all of those systems use. (The full sample code is listed at the end of this article.)

In the sample are a number of solid color squares that can be clicked on and dragged about the screen. When a square is clicked on, it will come to the front, rotate 45 degrees, and become opaque when dragged about.

This shows a number of basic concepts for working with the API including:

- Creating a compositor
- Creating a SpriteVisual with a CompositionColorBrush
- Clipping the Visual
- Rotating the Visual
- Setting Opacity
- Changing the Visual’s position in the collection.

## Creating a Compositor

Creating a [**Compositor**](/uwp/api/Windows.UI.Composition.Compositor) and storing it in a variable for use as a factory is a simple task. The following snippet shows creating a new **Compositor**:

```cs
_compositor = new Compositor();
```

## Creating a SpriteVisual and ColorBrush

Using the [**Compositor**](/uwp/api/Windows.UI.Composition.Compositor) it's easy to create objects whenever you need them, such as a [**SpriteVisual**](/uwp/api/Windows.UI.Composition.SpriteVisual) and a [**CompositionColorBrush**](/uwp/api/Windows.UI.Composition.CompositionColorBrush):

```cs
var visual = _compositor.CreateSpriteVisual();
visual.Brush = _compositor.CreateColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
```

While this is only a few lines of code, it demonstrates a powerful concept: [**SpriteVisual**](/uwp/api/Windows.UI.Composition.SpriteVisual) objects are the heart of the effects system. The **SpriteVisual** allows for great flexibility and interplay in color, image and effect creation. The **SpriteVisual** is a single visual type that can fill a 2D rectangle with a brush, in this case, a solid color.

## Clipping a Visual

The [**Compositor**](/uwp/api/Windows.UI.Composition.Compositor) can also be used to create clips to a [**Visual**](/uwp/api/Windows.UI.Composition.Visual). Below is an example from the sample of using the [**InsetClip**](/uwp/api/Windows.UI.Composition.InsetClip) to trim each side of the visual:

```cs
var clip = _compositor.CreateInsetClip();
clip.LeftInset = 1.0f;
clip.RightInset = 1.0f;
clip.TopInset = 1.0f;
clip.BottomInset = 1.0f;
_currentVisual.Clip = clip;
```

Like other objects in the API, [**InsetClip**](/uwp/api/Windows.UI.Composition.InsetClip) can have animations applied to its properties.

## <span id="Rotating_a_Clip"></span><span id="rotating_a_clip"></span><span id="ROTATING_A_CLIP"></span>Rotating a Clip

A [**Visual**](/uwp/api/Windows.UI.Composition.Visual) can be transformed with a rotation. Note that [**RotationAngle**](/uwp/api/windows.ui.composition.visual.rotationangle) supports both radians and degrees. It defaults to radians, but it’s easy to specify degrees as shown in the following snippet:

```cs
child.RotationAngleInDegrees = 45.0f;
```

Rotation is just one example of a set of transform components provided by the API to make these tasks easier. Others include Offset, Scale, Orientation, RotationAxis, and 4x4 TransformMatrix.

## Setting Opacity

Setting the opacity of a visual is a simple operation using a float value. For example, in the sample all the squares start at .8 opacity:

```cs
visual.Opacity = 0.8f;
```

Like rotation, the [**Opacity**](/uwp/api/windows.ui.composition.visual.opacity) property can be animated.

## Changing the Visual's position in the collection

The Composition API allows for a Visual's position in a [**VisualCollection**](/uwp/api/windows.ui.composition.visualcollection) to be changed in a number of ways. It can be placed above another Visual with [**InsertAbove**](/uwp/api/windows.ui.composition.visualcollection.insertabove), placed below with [**InsertBelow**](/uwp/api/windows.ui.composition.visualcollection.insertbelow), moved to the top with [**InsertAtTop**](/uwp/api/windows.ui.composition.visualcollection.insertattop), or the bottom with [**InsertAtBottom**](/uwp/api/windows.ui.composition.visualcollection.insertatbottom).

In the sample, a [**Visual**](/uwp/api/Windows.UI.Composition.Visual) that has been clicked is sorted to the top:

```cs
parent.Children.InsertAtTop(_currentVisual);
```

## Full Example

In the full sample, all of the concepts above are used together to construct and walk a simple tree of [**Visual**](/uwp/api/Windows.UI.Composition.Visual) objects to change opacity without using XAML, WWA, or DirectX. This sample shows how child **Visual** objects are created and added and how properties are changed.

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;

namespace compositionvisual
{
    class VisualProperties : IFrameworkView
    {
        //------------------------------------------------------------------------------
        //
        // VisualProperties.Initialize
        //
        // This method is called during startup to associate the IFrameworkView with the
        // CoreApplicationView.
        //
        //------------------------------------------------------------------------------

        void IFrameworkView.Initialize(CoreApplicationView view)
        {
            _view = view;
            _random = new Random();
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.SetWindow
        //
        // This method is called when the CoreApplication has created a new CoreWindow,
        // allowing the application to configure the window and start producing content
        // to display.
        //
        //------------------------------------------------------------------------------

        void IFrameworkView.SetWindow(CoreWindow window)
        {
            _window = window;
            InitNewComposition();
            _window.PointerPressed += OnPointerPressed;
            _window.PointerMoved += OnPointerMoved;
            _window.PointerReleased += OnPointerReleased;
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.OnPointerPressed
        //
        // This method is called when the user touches the screen, taps it with a stylus
        // or clicks the mouse.
        //
        //------------------------------------------------------------------------------

        void OnPointerPressed(CoreWindow window, PointerEventArgs args)
        {
            Point position = args.CurrentPoint.Position;

            //
            // Walk our list of visuals to determine who, if anybody, was selected
            //
            foreach (var child in _root.Children)
            {
                //
                // Did we hit this child?
                //
                Vector3 offset = child.Offset;
                Vector2 size = child.Size;

                if ((position.X >= offset.X) &&
                    (position.X < offset.X + size.X) &&
                    (position.Y >= offset.Y) &&
                    (position.Y < offset.Y + size.Y))
                {
                    //
                    // This child was hit. Since the children are stored back to front,
                    // the last one hit is the front-most one so it wins
                    //
                    _currentVisual = child as ContainerVisual;
                    _offsetBias = new Vector2((float)(offset.X - position.X),
                                              (float)(offset.Y - position.Y));
                }
            }

            //
            // If a visual was hit, bring it to the front of the Z order
            //
            if (_currentVisual != null)
            {
                ContainerVisual parent = _currentVisual.Parent as ContainerVisual;
                parent.Children.Remove(_currentVisual);
                parent.Children.InsertAtTop(_currentVisual);
            }
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.OnPointerMoved
        //
        // This method is called when the user moves their finger, stylus or mouse with
        // a button pressed over the screen.
        //
        //------------------------------------------------------------------------------

        void OnPointerMoved(CoreWindow window, PointerEventArgs args)
        {
            //
            // If a visual is selected, drag it with the pointer position and
            // make it opaque while we drag it
            //
            if (_currentVisual != null)
            {
                //
                // Set up the properties of the visual the first time it is
                // dragged. This will last for the duration of the drag
                //
                if (!_dragging)
                {
                    _currentVisual.Opacity = 1.0f;

                    //
                    // Transform the first child of the current visual so that
                    // the image is rotated
                    //
                    foreach (var child in _currentVisual.Children)
                    {
                        child.RotationAngleInDegrees = 45.0f;
                        child.CenterPoint = new Vector3(_currentVisual.Size.X / 2, _currentVisual.Size.Y / 2, 0);
                        break;
                    }

                    //
                    // Clip the visual to its original layout rect by using an inset
                    // clip with a one-pixel margin all around
                    //
                    var clip = _compositor.CreateInsetClip();
                    clip.LeftInset = 1.0f;
                    clip.RightInset = 1.0f;
                    clip.TopInset = 1.0f;
                    clip.BottomInset = 1.0f;
                    _currentVisual.Clip = clip;

                    _dragging = true;
                }

                Point position = args.CurrentPoint.Position;
                _currentVisual.Offset = new Vector3((float)(position.X + _offsetBias.X),
                                                    (float)(position.Y + _offsetBias.Y),
                                                    0.0f);
            }
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.OnPointerReleased
        //
        // This method is called when the user lifts their finger or stylus from the
        // screen, or lifts the mouse button.
        //
        //------------------------------------------------------------------------------

        void OnPointerReleased(CoreWindow window, PointerEventArgs args)
        {
            //
            // If a visual was selected, make it transparent again when it is
            // released and restore the transform and clip
            //
            if (_currentVisual != null)
            {
                if (_dragging)
                {
                    //
                    // Remove the transform from the first child
                    //
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

        //------------------------------------------------------------------------------
        //
        // VisualProperties.Load
        //
        // This method is called when a specific page is being loaded in the
        // application.  It is not used for this application.
        //
        //------------------------------------------------------------------------------

        void IFrameworkView.Load(string unused)
        {

        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.Run
        //
        // This method is called by CoreApplication.Run() to actually run the
        // dispatcher's message pump.
        //
        //------------------------------------------------------------------------------

        void IFrameworkView.Run()
        {
            _window.Activate();
            _window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.Uninitialize
        //
        // This method is called during shutdown to disconnect the CoreApplicationView,
        // and CoreWindow from the IFrameworkView.
        //
        //------------------------------------------------------------------------------

        void IFrameworkView.Uninitialize()
        {
            _window = null;
            _view = null;
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.InitNewComposition
        //
        // This method is called by SetWindow(), where we initialize Composition after
        // the CoreWindow has been created.
        //
        //------------------------------------------------------------------------------

        void InitNewComposition()
        {
            //
            // Set up Windows.UI.Composition Compositor, root ContainerVisual, and associate with
            // the CoreWindow.
            //

            _compositor = new Compositor();

            _root = _compositor.CreateContainerVisual();



            _compositionTarget = _compositor.CreateTargetForCurrentView();
            _compositionTarget.Root = _root;

            //
            // Create a few visuals for our window
            //
            for (int index = 0; index < 20; index++)
            {
                _root.Children.InsertAtTop(CreateChildElement());
            }
        }

        //------------------------------------------------------------------------------
        //
        // VisualProperties.CreateChildElement
        //
        // Creates a small sub-tree to represent a visible element in our application.
        //
        //------------------------------------------------------------------------------

        Visual CreateChildElement()
        {
            //
            // Each element consists of three visuals, which produce the appearance
            // of a framed rectangle
            //
            var element = _compositor.CreateContainerVisual();
            element.Size = new Vector2(100.0f, 100.0f);

            //
            // Position this visual randomly within our window
            //
            element.Offset = new Vector3((float)(_random.NextDouble() * 400), (float)(_random.NextDouble() * 400), 0.0f);

            //
            // The outer rectangle is always white
            //
            //Note to preview API users - SpriteVisual and Color Brush replace SolidColorVisual
            //for example instead of doing
            //var visual = _compositor.CreateSolidColorVisual() and
            //visual.Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            //we now use the below

            var visual = _compositor.CreateSpriteVisual();
            element.Children.InsertAtTop(visual);
            visual.Brush = _compositor.CreateColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            visual.Size = new Vector2(100.0f, 100.0f);

            //
            // The inner rectangle is inset from the outer by three pixels all around
            //
            var child = _compositor.CreateSpriteVisual();
            visual.Children.InsertAtTop(child);
            child.Offset = new Vector3(3.0f, 3.0f, 0.0f);
            child.Size = new Vector2(94.0f, 94.0f);

            //
            // Pick a random color for every rectangle
            //
            byte red = (byte)(0xFF * (0.2f + (_random.NextDouble() / 0.8f)));
            byte green = (byte)(0xFF * (0.2f + (_random.NextDouble() / 0.8f)));
            byte blue = (byte)(0xFF * (0.2f + (_random.NextDouble() / 0.8f)));
            child.Brush = _compositor.CreateColorBrush(Color.FromArgb(0xFF, red, green, blue));

            //
            // Make the subtree root visual partially transparent. This will cause each visual in the subtree
            // to render partially transparent, since a visual's opacity is multiplied with its parent's
            // opacity
            //
            element.Opacity = 0.8f;

            return element;
        }

        // CoreWindow / CoreApplicationView
        private CoreWindow _window;
        private CoreApplicationView _view;

        // Windows.UI.Composition
        private Compositor _compositor;
        private CompositionTarget _compositionTarget;
        private ContainerVisual _root;
        private ContainerVisual _currentVisual;
        private Vector2 _offsetBias;
        private bool _dragging;

        // Helpers
        private Random _random;
    }


    public sealed class VisualPropertiesFactory : IFrameworkViewSource
    {
        //------------------------------------------------------------------------------
        //
        // VisualPropertiesFactory.CreateView
        //
        // This method is called by CoreApplication to provide a new IFrameworkView for
        // a CoreWindow that is being created.
        //
        //------------------------------------------------------------------------------

        IFrameworkView IFrameworkViewSource.CreateView()
        {
            return new VisualProperties();
        }


        //------------------------------------------------------------------------------
        //
        // main
        //
        //------------------------------------------------------------------------------

        static int Main(string[] args)
        {
            CoreApplication.Run(new VisualPropertiesFactory());

            return 0;
        }
    }
}
```