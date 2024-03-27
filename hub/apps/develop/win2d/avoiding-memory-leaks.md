---
title: Avoiding memory leaks
description: A guide on how to make sure not to introduce memory leaks in XAML applications using Win2D.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Avoiding memory leaks

When using Win2D controls in managed XAML applications, care must be taken to avoid reference count cycles that could prevent these controls ever being reclaimed by the garbage collector.

## You have a problem if...

- You are using Win2D from a .NET language such as C# (not native C++)
- You use one of the Win2D XAML controls:
  - [`CanvasControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm)
  - [`CanvasVirtualControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualControl.htm)
  - [`CanvasAnimatedControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl.htm)
  - [`CanvasSwapChainPanel`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasSwapChainPanel.htm)
- You subscribe to events of the Win2D control (eg. `Draw`, `CreateResources`, `SizeChanged`...)
- Your app moves back and forth between more than one XAML page

If all these conditions are met, a reference count cycle will keep the Win2D control from ever being garbage collected. New Win2D resources are allocated each time the app moves to a different page, but the old ones are never freed so memory is leaked. To avoid this, you must add code to explicitly break the cycle.

## How to fix it

To break the reference count cycle and let your page be garbage collected:

- Hook the `Unloaded` event of the XAML page which contains the Win2D control
- In the `Unloaded` handler, call `RemoveFromVisualTree` on the Win2D control
- In the `Unloaded` handler, release (by setting to `null`) any explicit references to the Win2D control

Example code:

```csharp
void page_Unloaded(object sender, RoutedEventArgs e)
{
    this.canvas.RemoveFromVisualTree();
    this.canvas = null;
}
```

For working examples, see any of the [Example Gallery](https://github.com/microsoft/Win2D-Samples/tree/master/ExampleGallery/) demo pages.

## How to test for cycle leaks

To test whether your application is correctly breaking refcount cycles, add a finalizer method to any XAML pages which contain Win2D controls:

```csharp
~MyPage()
{
    System.Diagnostics.Debug.WriteLine("~" + GetType().Name);
}
```

In your `App` constructor, set up a timer that will make sure garbage collection occurs at regular intervals:

```csharp
var gcTimer = new DispatcherTimer();
gcTimer.Tick += (sender, e) => { GC.Collect(); };
gcTimer.Interval = TimeSpan.FromSeconds(1);
gcTimer.Start();
```

Navigate to the page, then away from it to some other page. If all cycles have been broken, you will see `Debug.WriteLine` output in the Visual Studio output pane within a second or two.

Note that calling `GC.Collect` is disruptive and hurts performance, so you should remove this test code as soon as you finish testing for leaks!

## The gory details

A cycle occurs when an object A has a reference to B, at the same time as B also has a reference to A. Or when A references B, and B references C, while C references A, etc.

When subscribing to events of a XAML control, this sort of cycle is pretty much inevitable:
- XAML page holds references to all the controls contained within it
- Controls hold references to the handler delegates that have been subscribed to their events
- Each delegate holds a reference to its target instance
- Event handlers are typically instance methods of the XAML page class, so their target instance references point back to the XAML page, creating a cycle

If all the objects involved are implemented in .NET, such cycles are not a problem because .NET is garbage collected, and the garbage collection algorithm is able to identify and reclaim groups of objects even if they are linked in a cycle.

Unlike .NET, C++ manages memory by reference counting, which is unable to detect and reclaim cycles of objects. In spite of this limitation, C++ apps using Win2D have no problem because C++ event handlers default to holding weak rather than strong references to their target instance. Therefore the page references the control, and the control references the event handler delegate, but this delegate does not reference back to the page so there is no cycle.

The problem comes when a C++ WinRT component such as Win2D is used by a .NET application:
- The XAML page is part of the application, so uses garbage collection
- The Win2D control is implemented in C++, so uses reference counting
- The event handler delegate is part of the application, so uses garbage collection and holds a strong reference to its target instance

A cycle is present, but the Win2D objects participating in this cycle are not using .NET garbage collection. This means the garbage collector is unable to see the entire chain, so it cannot detect or reclaim the objects. When this occurs, the application must help out by explicitly breaking the cycle. This can be done either by releasing all references from the page to the control (as recommended above) or by releasing all references from the control to event handler delegates that might point back to the page (using the page Unloaded event to unsubscribe all event handlers).
