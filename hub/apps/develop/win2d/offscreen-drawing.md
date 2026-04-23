---
title: Offscreen drawing
description: An introduction to drawing onto render targets to create textures for later use.
ms.date: 05/26/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Offscreen drawing

Apps occasionally need to draw graphics to a target, where that target is not intended for immediate display. This type of drawing is sometimes called "offscreen rendering", or "drawing to a texture". This is useful when, for example, an app's output of a drawing operation is to be saved to a file, returned as an array of pixels, or used as an input to a later operation.

Win2D supports these scenarios, and they are made easy with [`CanvasRenderTarget`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasRenderTarget.htm).

`CanvasRenderTarget` extends [`CanvasBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm), and has the method [`CreateDrawingSession()`](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_CanvasRenderTarget_CreateDrawingSession.htm). Use `CreateDrawingSession` to draw graphics content to a `CanvasRenderTarget`. For example:

```csharp
CanvasDevice device = CanvasDevice.GetSharedDevice();
CanvasRenderTarget offscreen = new CanvasRenderTarget(device, width, height, 96);

using (CanvasDrawingSession ds = offscreen.CreateDrawingSession())
{
    ds.Clear(Colors.Black);
    ds.DrawRectangle(100, 200, 5, 6, Colors.Red);
}
```

Note that there is a method call to `Clear`. Without this, the bitmap will be initialized with undefined content. Drawing sessions created through `CanvasRenderTarget` are different from those created on Win2D's XAML controls, in terms of the `Clear` behavior. Controls are always cleared automatically by Win2D when a drawing session is created. `CanvasRenderTargets` are not. This way, apps have the ability to make incremental changes to `CanvasRenderTarget`-s, and avoid redrawing an entire scene every time.

To draw a `CanvasRenderTarget` to another drawing session, simply use `DrawImage(ICanvasImage)` or one of its overloads. For example:

```csharp
void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
{
    args.DrawingSession.DrawImage(offscreen, 23, 34);
}
```

Or, to use a `CanvasRenderTarget` as an input to an effect, pass it in wherever the effect expects to use an `IGraphicsEffectSource` as a source. For example:

```csharp
GaussianBlurEffect blurEffect = new GaussianBlurEffect()
{
    Source = offscreen,
    BlurAmount = 3.0f
};
```

An app can close, and re-open drawing sessions on a `CanvasRenderTarget` arbitrarily many times.

Drawing operations are not committed to the `CanvasRenderTarget` until the drawing session object is disposed. In C#, a `using` block can organize this.

It's worth pointing out that `CanvasRenderTarget` is not a XAML control, and does not involve the XAML tree at all. It is suitable for both XAML and non-XAML-based apps.