---
title: Win2D quickstart
description: A quickstart tutorial to using Win2D to perform some basic drawing.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Win2D quickstart

This quick start tutorial introduces some of the basic capabilities of Win2D. You will learn how to:
- Add Win2D to a XAML C# Windows project
- Draw text and geometry
- Apply filter effects
- Animate your Win2D content
- Follow Win2D best practices

## Setup your dev machine

Make sure to set up your machine with all the necessary tools:
- [Install Visual Studio](https://visualstudio.microsoft.com/it/downloads/)
  - Include the UWP or Windows SDK (depending on your needs), 17763+
- If using UWP, ensure to also [enable developer mode](/windows/apps/get-started/developer-mode-features-and-debugging)

## Create a new Win2D project

Follow these steps in the [hello Win2D guide](./hellowin2dworld.md) to create a new project using Win2D. You can either use UWP or WinAppSDK, whichever framework you prefer.

## Add a Win2D CanvasControl to your app's XAML

1. In order to use Win2D, you need somewhere to draw your graphics. In a XAML app, the simplest way to do this is to add a [`CanvasControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm) to your XAML page.

Before you continue, first ensure that the project's Architecture option is set to `x86` or `x64` and **not** to `Any CPU`. Win2D is implemented in C++ and therefore projects that use Win2D need to be targeted to a specific CPU architecture.

1. Navigate to `MainPage.xaml` in your project by double clicking on it in Solution Explorer. This will open the file. For convenience, you can double click on the XAML button in the Designer tab; this will hide the visual designer and reserve all of the space for the code view.

3. Before you add the control, you first have to tell XAML where `CanvasControl` is defined. To do this, go to the definition of the `Page` element, and add this statement: `xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"`. Your XAML should now look like this:

```XAML
<Page
    ...
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
    mc:Ignorable="d">
```

4. Now, add a new `canvas:CanvasControl` as a child element to the root `Grid` element. Give the control a name, e.g. "canvas". Your XAML should now look like this:

```XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <canvas:CanvasControl x:Name="canvas"/>
</Grid>
```

5. Next, define an event handler for the [`Draw`](https://microsoft.github.io/Win2D/WinUI2/html/E_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl_Draw.htm) event. `CanvasControl` raises `Draw` whenever your app needs to draw or redraw its content. The easiest way is to let Visual Studio AutoComplete assist you. In the `CanvasControl` definition, begin typing a new attribute for the `Draw` event handler:

```XAML
<canvas:CanvasControl x:Name="canvas" Draw="canvas_Draw" />
```

> [!NOTE]
> Once you have entered in `Draw="`, Visual Studio should pop up a box prompting you to let it automatically fill out the right definition for the event handler. Press TAB to accept Visual Studio's default event handler. This will also automatically add a correctly formatted event handler method in your code behind (MainPage.xaml.cs). Don't worry if you didn't use AutoComplete; you can manually add the event handler method in the next step.

## Draw your first text in Win2D

1. Now, let's go to the C# code behind. Open `MainPage.xaml.cs` from Solution Explorer.

2. At the top of the C# file are various namespace definitions. Add the following namespaces:

```csharp
using Windows.UI;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
```

3. Next, you should see the following blank event handler which was inserted by AutoComplete:

```csharp
private void canvas_Draw(
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender,
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
{
}
```

(If you didn't use AutoComplete in the previous step, add this code now.)

4. The [`CanvasDrawEventArgs`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasDrawEventArgs.htm) parameter exposes a member, `DrawingSession`, which is of the type [`CanvasDrawingSession`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasDrawingSession.htm). This class provides most of the basic drawing functionality in Win2D: it has methods such as [`CanvasDrawingSession.DrawRectangle`](https://microsoft.github.io/Win2D/WinUI2/html/Overload_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawRectangle.htm), [`CanvasDrawingSession.DrawImage`](https://microsoft.github.io/Win2D/WinUI2/html/Overload_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawImage.htm), and the method you need to draw text, [`CanvasDrawingSession.DrawText`](https://microsoft.github.io/Win2D/WinUI2/html/Overload_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawText.htm).

Add the following code to the `canvas_Draw` method:

```csharp
args.DrawingSession.DrawText("Hello, World!", 100, 100, Colors.Black);
```

The first argument, `"Hello, World!"`, is the string that you want Win2D to display. The two "100"s tell Win2D to offset this text by 100 DIPs (device-independent pixels) to the right and down. Finally, `Colors.Black` defines the color of the text.

5. Now you are ready to run your first Win2D app. Press the F5 key to compile and launch. You should see an empty window with "Hello, world!" in black.

## Correctly dispose of Win2D resources

1. Before continuing on to draw other kinds of content, you first should add some code to ensure your app avoids memory leaks. Most Win2D applications written in a .NET language and using a Win2D control like [`CanvasControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm) need to follow the below steps. Strictly speaking, your simple "Hello, world" app is not affected, but this is a good practice to follow in general.

For more information, see [Avoiding memory leaks](./avoiding-memory-leaks.md).

2. Open `MainPage.xaml` and find the Page XAML element that contains your `CanvasControl`. It should be the first element in the file.

3. Add a handler for the `Unloaded` event. Your XAML should look like this:

```XAML
<Page
    ...
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
    mc:Ignorable="d"
    Unloaded="Page_Unloaded">
```

4. Go to `MainPage.xaml.cs` and find the `Page_Unloaded` event handler. Add the following code:

```csharp
void Page_Unloaded(object sender, RoutedEventArgs e)
{
    this.canvas.RemoveFromVisualTree();
    this.canvas = null;
}
```

5. If your app contains multiple Win2D controls, then you need to repeat the above steps for each XAML page that contains a Win2D control. Your app currently only has a single `CanvasControl` so you're all done.

## Draw some shapes

1. It's just as easy to add 2D geometry to your app. Add the following code to the end of `canvas_Draw`:

```csharp
args.DrawingSession.DrawCircle(125, 125, 100, Colors.Green);
args.DrawingSession.DrawLine(0, 0, 50, 200, Colors.Red);
```

The arguments to these two methods are similar to `DrawText`. A circle is defined by a center point (125, 125), a radius (100), and a color (Green). A line is defined by a beginning (0, 0), an end (50, 200) and a color (Red).

12. Now, press F5 to run the app. You should see "Hello, world!" along with a green circle and red line.

You may be wondering how to control more advanced drawing options, such as line thickness and dashes, or more complex fill options such as using brushes. Win2D provides all of these options and more, and makes it easy to use them when you want. All of the `Draw(...)` methods offer many overloads that can accept additional parameters such as [`CanvasTextFormat`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Text_CanvasTextFormat.htm) (font family, size, etc) and [`CanvasStrokeStyle`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasStrokeStyle.htm) (dashes, dots, endcaps, etc). Feel free to explore the API surface to learn more about these options.

## Dynamically generate drawing parameters

1. Now, let's add some variety by drawing a bunch of shapes and text with randomized colors.

Add the following code to the top of your `MainPage` class. This is helper functionality to generate random values that you will use when drawing:

```csharp
Random rnd = new Random();
private Vector2 RndPosition()
{
    double x = rnd.NextDouble() * 500f;
    double y = rnd.NextDouble() * 500f;
    return new Vector2((float)x, (float)y);
}

private float RndRadius()
{
    return (float)rnd.NextDouble() * 150f;
}

private byte RndByte()
{
    return (byte)rnd.Next(256);
}
```

2. Modify your `canvas_Draw` method to draw using these random parameters:

```csharp
private void canvas_Draw(
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender,
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
{
    args.DrawingSession.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
    args.DrawingSession.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
    args.DrawingSession.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
}
```

Let's break down how `DrawText` has changed. `"Hello, World!"` remains the same as before. The x and y offset parameters have been replaced with a single [`System.Numerics.Vector2`](https://msdn.microsoft.com/library/windows/apps/System.Numerics.Vector2) which is generated by `RndPosition`. Finally, instead of using a predefined color, `Color.FromArgb` allows you to define a color using A, R, G and B values. A is alpha, or the opacity level; in this case you always want fully opaque (255).

`DrawCircle` and `DrawLine` operate similarly to `DrawText`.

3. Finally, wrap your drawing code in a `for` loop. You should end up with the following `canvas_Draw` code:

```csharp
for (int i = 0; i < 100; i++)
{
    args.DrawingSession.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
    args.DrawingSession.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
    args.DrawingSession.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
}
```

4. Run the app again. You should see a whole bunch of text, lines and circles with random positions and sizes.

## Apply an image effect to your content

Image effects, also known as filter effects, are graphical transformations that are applied to pixel data. Saturation, hue rotation, and Gaussian blur are some common image effects. Image effects can be chained together, producing sophisticated visual appearance for minimal effort.

You use image effects by providing a source image (the content you're starting with), creating an effect such as [`GaussianBlurEffect`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_GaussianBlurEffect.htm), setting properties such as [`BlurAmount`](https://microsoft.github.io/Win2D/WinUI2/html/P_Microsoft_Graphics_Canvas_Effects_GaussianBlurEffect_BlurAmount.htm), and then drawing the effect's output with `DrawImage`.

To apply an image effect to your text and shapes, you need to first render that content into a [`CanvasCommandList`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasCommandList.htm). This object is usable as an input to your effect.

2. Change your `canvas_Draw` method to use the following code:

```csharp
CanvasCommandList cl = new CanvasCommandList(sender);

using (CanvasDrawingSession clds = cl.CreateDrawingSession())
{
    for (int i = 0; i < 100; i++)
    {
        clds.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
        clds.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
        clds.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
    }
}
```

Just like how you obtain a `CanvasDrawingSession` from `CanvasDrawEventArgs` which you can draw with, you can create a `CanvasDrawingSession` from a `CanvasCommandList`. The only difference is that when you draw to the command list's drawing session (clds), you are not directly rendering to the `CanvasControl`. Instead, the command list is an intermediate object that stores the results of rendering for later use.

You may have noticed the `using` block that wraps the command list's drawing session. Drawing sessions implement [`IDisposable`](https://msdn.microsoft.com/library/system.idisposable) and must be disposed when you are done rendering (the `using` block does this). The `CanvasDrawingSession` that you obtain from `CanvasDrawEventArgs` automatically is closed for you, but you must dispose any drawing sessions that you explicitly created.

3. Finally, define the `GaussianBlurEffect` by adding the following code to the end of the `canvas_Draw` method:

```csharp
GaussianBlurEffect blur = new GaussianBlurEffect();
blur.Source = cl;
blur.BlurAmount = 10.0f;
args.DrawingSession.DrawImage(blur);
```

4. Run the app again. You should see your lines, text and circles with a blurry appearance.

## Animate your app with CanvasAnimatedControl

. Win2D gives you the ability to update and animate your content in realtime, for example by changing the blur radius of the Gaussian blur with every frame. To do this, you will use [`CanvasAnimatedControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl.htm).

`CanvasControl` is best suited for mostly static graphics content - it only raises the `Draw` event when your content needs to be updated or redrawn. If you have continually changing content then you should consider using `CanvasAnimatedControl` instead. The two controls operate very similarly, except `CanvasAnimatedControl` raises the `Draw` event on a periodic basis; by default it is called 60 times per second.

2. To switch to `CanvasAnimatedControl`, go to `MainPage.xaml`, delete the `CanvasControl` line, and replace it with the following XAML:

```XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <canvas:CanvasAnimatedControl x:Name="canvas" Draw="canvas_DrawAnimated" CreateResources="canvas_CreateResources"/>
</Grid>
```

Just like with `CanvasControl`, let AutoComplete create the `Draw` event handler for you. By default, Visual Studio will name this handler `canvas_Draw_1` because `canvas_Draw` already exists; here, we've renamed the method `canvas_AnimatedDraw` to make it clear that this is a different event.

In addition, you are also handling a new event, [`CreateResources`](https://microsoft.github.io/Win2D/WinUI2/html/E_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl_CreateResources.htm). Once again, let AutoComplete create the handler.

Now that your app will be redrawing at 60 frames per second, it is more efficient to create your Win2D visual resources once and reuse them with every frame. It is inefficient to create a `CanvasCommandList` and draw 300 elements into it 60 times per second when the content remains static. `CreateResources` is an event that is fired only when Win2D determines you need to recreate your visual resources, such as when the page is loaded.

3. Switch back to `MainPage.xaml.cs`. Find your `canvas_Draw` method which should look like this:

```csharp
private void canvas_Draw(
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender,
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
{
    CanvasCommandList cl = new CanvasCommandList(sender);
    using (CanvasDrawingSession clds = cl.CreateDrawingSession())
    {
        for (int i = 0; i < 100; i++)
        {
            clds.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            clds.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            clds.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
        }
    }

    GaussianBlurEffect blur = new GaussianBlurEffect();
    blur.Source = cl;
    blur.BlurAmount = 10.0f;
    args.DrawingSession.DrawImage(blur);
}
```

The vast majority of this existing draw code does not need to be executed with every frame: the command list containing the text, lines and circles remains the same with every frame, and the only thing that changes is the blur radius. Therefore, you can move this "static" code into `CreateResources`.

To do this, first cut (CTRL+X) the entire contents of `canvas_Draw`, except for the very last line (`args.DrawingSession.DrawImage(blur);`). You can now delete the remainder of `canvas_Draw` as it is no longer needed: recall that `CanvasAnimatedControl` has its own distinct `Draw` event.

4. Find the automatically generated `canvas_CreateResources` method:

```csharp
private void canvas_CreateResources(
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, 
    Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
{}
```

Paste (CTRL+V) your previously cut code into this method. Next, move the declaration of `GaussianBlurEffect` outside the method body so the variable becomes a member of the MainPage class. Your code should now look like the following:

```csharp
GaussianBlurEffect blur;
private void canvas_CreateResources(
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender,
    Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
{
    CanvasCommandList cl = new CanvasCommandList(sender);
    using (CanvasDrawingSession clds = cl.CreateDrawingSession())
    {
        for (int i = 0; i < 100; i++)
        {
            clds.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            clds.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            clds.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
        }
    }

    blur = new GaussianBlurEffect()
    {
        Source = cl,
        BlurAmount = 10.0f
    };
}
```

5. Now you can animate the Gaussian blur. Find the `canvas_DrawAnimated` method and add the following code:

```csharp
private void canvas_DrawAnimated(
    Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender,
    Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
{
    float radius = (float)(1 + Math.Sin(args.Timing.TotalTime.TotalSeconds)) * 10f;
    blur.BlurAmount = radius;
    args.DrawingSession.DrawImage(blur);
}
```

This reads the total elapsed time provided by [`CanvasAnimatedDrawEventArgs`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedDrawEventArgs.htm) and uses this to calculate the desired blur amount; the sine function provides an interesting variation over time. Finally, the `GaussianBlurEffect` is rendered.

6. Run the app to see the blurred content change over time.

Congratulations on completing this quick start tutorial! Hopefully you have seen how you can use Win2D to create a rich, animated visual scene with just a few lines of C# and XAML code.