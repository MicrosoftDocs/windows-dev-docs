---
Description: Learn to write code for a custom Panel class, implementing ArrangeOverride and MeasureOverride methods, and using the Children property.
MS-HAID: dev\_ctrl\_layout\_txt.boxpanel\_example\_custom\_panel
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: BoxPanel, an example custom panel (Windows apps)
ms.assetid: 981999DB-81B1-4B9C-A786-3025B62B74D6
label: BoxPanel, an example custom panel
template: detail.hbs
op-migration-status: ready
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# BoxPanel, an example custom panel

 

Learn to write code for a custom [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel) class, implementing [**ArrangeOverride**](/uwp/api/windows.ui.xaml.frameworkelement.arrangeoverride) and [**MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride) methods, and using the [**Children**](/uwp/api/windows.ui.xaml.controls.panel.children) property. 

> **Important APIs**: [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel), [**ArrangeOverride**](/uwp/api/windows.ui.xaml.frameworkelement.arrangeoverride),[**MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride) 

The example code shows a custom panel implementation, but we don't devote a lot of time explaining the layout concepts that influence how you can customize a panel for different layout scenarios. If you want more info about these layout concepts and how they might apply to your particular layout scenario, see [XAML custom panels overview](custom-panels-overview.md).

A *panel* is an object that provides a layout behavior for child elements it contains, when the XAML layout system runs and your app UI is rendered. You can define custom panels for XAML layout by deriving a custom class from the [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel) class. You provide behavior for your panel by overriding the [**ArrangeOverride**](/uwp/api/windows.ui.xaml.frameworkelement.arrangeoverride) and [**MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride) methods, supplying logic that measures and arranges the child elements. This example derives from **Panel**. When you start from **Panel**, **ArrangeOverride** and **MeasureOverride** methods don't have a starting behavior. Your code is providing the gateway by which child elements become known to the XAML layout system and get rendered in the UI. So, it's really important that your code accounts for all child elements and follows the patterns the layout system expects.

## Your layout scenario

When you define a custom panel, you're defining a layout scenario.

A layout scenario is expressed through:

-   What the panel will do when it has child elements
-   When the panel has constraints on its own space
-   How the logic of the panel determines all the measurements, placement, positions, and sizings that eventually result in a rendered UI layout of children

With that in mind, the `BoxPanel` shown here is for a particular scenario. In the interest of keeping the code foremost in this example, we won't explain the scenario in detail yet, and instead concentrate on the steps needed and the coding patterns. If you want to know more about the scenario first, skip ahead to ["The scenario for `BoxPanel`"](#the-scenario-for-boxpanel), and then come back to the code.

## Start by deriving from **Panel**

Start by deriving a custom class from [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel). Probably the easiest way to do this is to define a separate code file for this class, using the **Add** | **New Item** | **Class** context menu options for a project from the **Solution Explorer** in Microsoft Visual Studio. Name the class (and file) `BoxPanel`.

The template file for a class doesn't start with many **using** statements because it's not specifically for Windows apps. So first, add **using** statements. The template file also starts with a few **using** statements that you probably don't need, and can be deleted. Here's a suggested list of **using** statements that can resolve types you'll need for typical custom panel code:

```CSharp
using System;
using System.Collections.Generic; // if you need to cast IEnumerable for iteration, or define your own collection properties
using Windows.Foundation; // Point, Size, and Rect
using Windows.UI.Xaml; // DependencyObject, UIElement, and FrameworkElement
using Windows.UI.Xaml.Controls; // Panel
using Windows.UI.Xaml.Media; // if you need Brushes or other utilities
```

Now that you can resolve [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel), make it the base class of `BoxPanel`. Also, make `BoxPanel` public:

```CSharp
public class BoxPanel : Panel
{
}
```

At the class level, define some **int** and **double** values that will be shared by several of your logic functions, but which won't need to be exposed as public API. In the example, these are named: `maxrc`, `rowcount`, `colcount`, `cellwidth`, `cellheight`, `maxcellheight`, `aspectratio`.

After you've done this, the complete code file looks like this (removing comments on **using**, now that you know why we have them):

```CSharp
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

public class BoxPanel : Panel 
{
    int maxrc, rowcount, colcount;
    double cellwidth, cellheight, maxcellheight, aspectratio;
}
```

From here on out, we'll be showing you one member definition at a time, be that a method override or something supporting such as a dependency property. You can add these to the skeleton above in any order, and we won't be showing the **using** statements or the definition of the class scope again in the snippets until we show the final code.

## **MeasureOverride**


```CSharp
protected override Size MeasureOverride(Size availableSize)
{
    Size returnSize;
    // Determine the square that can contain this number of items.
    maxrc = (int)Math.Ceiling(Math.Sqrt(Children.Count));
    // Get an aspect ratio from availableSize, decides whether to trim row or column.
    aspectratio = availableSize.Width / availableSize.Height;

    // Now trim this square down to a rect, many times an entire row or column can be omitted.
    if (aspectratio > 1)
    {
        rowcount = maxrc;
        colcount = (maxrc > 2 && Children.Count < maxrc * (maxrc - 1)) ? maxrc - 1 : maxrc;
    } 
    else 
    {
        rowcount = (maxrc > 2 && Children.Count < maxrc * (maxrc - 1)) ? maxrc - 1 : maxrc;
        colcount = maxrc;
    }

    // Now that we have a column count, divide available horizontal, that's our cell width.
    cellwidth = (int)Math.Floor(availableSize.Width / colcount);
    // Next get a cell height, same logic of dividing available vertical by rowcount.
    cellheight = Double.IsInfinity(availableSize.Height) ? Double.PositiveInfinity : availableSize.Height / rowcount;
           
    foreach (UIElement child in Children)
    {
        child.Measure(new Size(cellwidth, cellheight));
        maxcellheight = (child.DesiredSize.Height > maxcellheight) ? child.DesiredSize.Height : maxcellheight;
    }
    return LimitUnboundedSize(availableSize);
}
```

The necessary pattern of a [**MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride) implementation is the loop through each element in [**Panel.Children**](/uwp/api/windows.ui.xaml.controls.panel.children). Always call the [**Measure**](/uwp/api/windows.ui.xaml.uielement.measure) method on each of these elements. **Measure** has a parameter of type [**Size**](/uwp/api/Windows.Foundation.Size). What you're passing here is the size that your panel is committing to have available for that particular child element. So, before you can do the loop and start calling **Measure**, you need to know how much space each cell can devote. From the **MeasureOverride** method itself, you have the *availableSize* value. That is the size that the panel's parent used when it called **Measure**, which was the trigger for this **MeasureOverride** being called in the first place. So a typical logic is to devise a scheme whereby each child element divides the space of the panel's overall *availableSize*. You then pass each division of size to **Measure** of each child element.

How `BoxPanel` divides size is fairly simple: it divides its space into a number of boxes that's largely controlled by the number of items. Boxes are sized based on row and column count and the available size. Sometimes one row or column from a square isn't needed, so it's dropped and the panel becomes a rectangle rather than square in terms of its row : column ratio. For more info about how this logic was arrived at, skip ahead to ["The scenario for BoxPanel"](#the-scenario-for-boxpanel).

So what does the measure pass do? It sets a value for the read-only [**DesiredSize**](/uwp/api/windows.ui.xaml.uielement.desiredsize) property on each element where [**Measure**](/uwp/api/windows.ui.xaml.uielement.measure) was called. Having a **DesiredSize** value is possibly important once you get to the arrange pass, because the **DesiredSize** communicates what the size can or should be when arranging and in the final rendering. Even if you don't use **DesiredSize** in your own logic, the system still needs it.

It's possible for this panel to be used when the height component of *availableSize* is unbounded. If that's true, the panel doesn't have a known height to divide. In this case, the logic for the measure pass informs each child that it doesn't have a bounded height, yet. It does so by passing a [**Size**](/uwp/api/Windows.Foundation.Size) to the [**Measure**](/uwp/api/windows.ui.xaml.uielement.measure) call for children where [**Size.Height**](/uwp/api/windows.foundation.size.height) is infinite. That's legal. When **Measure** is called, the logic is that the [**DesiredSize**](/uwp/api/windows.ui.xaml.uielement.desiredsize) is set as the minimum of these: what was passed to **Measure**, or that element's natural size from factors such as explicitly-set [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width).

> [!NOTE]
> The internal logic of [**StackPanel**](/uwp/api/Windows.UI.Xaml.Controls.StackPanel) also has this behavior: **StackPanel** passes an infinite dimension value to [**Measure**](/uwp/api/windows.ui.xaml.uielement.measure) on children, indicating that there is no constraint on children in the orientation dimension. **StackPanel** typically sizes itself dynamically, to accommodate all children in a stack that grows in that dimension.

However, the panel itself can't return a [**Size**](/uwp/api/Windows.Foundation.Size) with an infinite value from [**MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride); that throws an exception during layout. So, part of the logic is to find out the maximum height that any child requests, and use that height as the cell height in case that isn't coming from the panel's own size constraints already. Here's the helper function `LimitUnboundedSize` that was referenced in previous code, which then takes that maximum cell height and uses it to give the panel a finite height to return, as well as assuring that `cellheight` is a finite number before the arrange pass is initiated:

```CSharp
// This method is called only if one of the availableSize dimensions of measure is infinite.
// That can happen to height if the panel is close to the root of main app window.
// In this case, base the height of a cell on the max height from desired size
// and base the height of the panel on that number times the #rows.

Size LimitUnboundedSize(Size input)
{
    if (Double.IsInfinity(input.Height))
    {
        input.Height = maxcellheight * colcount;
        cellheight = maxcellheight;
    }
    return input;
}
```

## **ArrangeOverride**

```CSharp
protected override Size ArrangeOverride(Size finalSize)
{
     int count = 1
     double x, y;
     foreach (UIElement child in Children)
     {
          x = (count - 1) % colcount * cellwidth;
          y = ((int)(count - 1) / colcount) * cellheight;
          Point anchorPoint = new Point(x, y);
          child.Arrange(new Rect(anchorPoint, child.DesiredSize));
          count++;
     }
     return finalSize;
}
```

The necessary pattern of an [**ArrangeOverride**](/uwp/api/windows.ui.xaml.frameworkelement.arrangeoverride) implementation is the loop through each element in [**Panel.Children**](/uwp/api/windows.ui.xaml.controls.panel.children). Always call the [**Arrange**](/uwp/api/windows.ui.xaml.uielement.arrange) method on each of these elements.

Note how there aren't as many calculations as in [**MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride); that's typical. The size of children is already known from the panel's own **MeasureOverride** logic, or from the [**DesiredSize**](/uwp/api/windows.ui.xaml.uielement.desiredsize) value of each child set during the measure pass. However, we still need to decide the location within the panel where each child will appear. In a typical panel, each child should render at a different position. A panel that creates overlapping elements isn't desirable for typical scenarios (although it's not out of the question to create panels that have purposeful overlaps, if that's really your intended scenario).

This panel arranges by the concept of rows and columns. The number of rows and columns was already calculated (it was necessary for measurement). So now the shape of the rows and columns plus the known sizes of each cell contribute to the logic of defining a rendering position (the `anchorPoint`) for each element that this panel contains. That [**Point**](/uwp/api/Windows.Foundation.Point), along with the [**Size**](/uwp/api/Windows.Foundation.Size) already known from measure, are used as the two components that construct a [**Rect**](/uwp/api/Windows.Foundation.Rect). **Rect** is the input type for [**Arrange**](/uwp/api/windows.ui.xaml.uielement.arrange).

Panels sometimes need to clip their content. If they do, the clipped size is the size that's present in [**DesiredSize**](/uwp/api/windows.ui.xaml.uielement.desiredsize), because the [**Measure**](/uwp/api/windows.ui.xaml.uielement.measure) logic sets it as the minimum of what was passed to **Measure**, or other natural size factors. So you don't typically need to specifically check for clipping during [**Arrange**](/uwp/api/windows.ui.xaml.uielement.arrange); the clipping just happens based on passing the **DesiredSize** through to each **Arrange** call.

You don't always need a count while going through the loop if all the info you need for defining the rendering position is known by other means. For example, in [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) layout logic, the position in the [**Children**](/uwp/api/windows.ui.xaml.controls.panel.children) collection doesn't matter. All the info needed to position each element in a **Canvas** is known by reading [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) and [**Canvas.Top**](/dotnet/api/system.windows.controls.canvas.top) values of children as part of the arrange logic. The `BoxPanel` logic happens to need a count to compare to the *colcount* so it's known when to begin a new row and offset the *y* value.

It's typical that the input *finalSize* and the [**Size**](/uwp/api/Windows.Foundation.Size) you return from a [**ArrangeOverride**](/uwp/api/windows.ui.xaml.frameworkelement.arrangeoverride) implementation are the same. For more info about why, see "**ArrangeOverride**" section of [XAML custom panels overview](custom-panels-overview.md).

## A refinement: controlling the row vs. column count

You could compile and use this panel just as it is now. However, we'll add one more refinement. In the code just shown, the logic puts the extra row or column on the side that's longest in aspect ratio. But for greater control over the shapes of cells, it might be desirable to choose a 4x3 set of cells instead of 3x4 even if the panel's own aspect ratio is "portrait." So we'll add an optional dependency property that the panel consumer can set to control that behavior. Here's the dependency property definition, which is very basic:

```CSharp
public static readonly DependencyProperty UseOppositeRCRatioProperty =
   DependencyProperty.Register("UseOppositeRCRatio", typeof(bool), typeof(BoxPanel), null);

public bool UseSquareCells
{
    get { return (bool)GetValue(UseOppositeRCRatioProperty); }
    set { SetValue(UseOppositeRCRatioProperty, value); }
}
```

And here's how using `UseOppositeRCRatio` impacts the measure logic. Really all it's doing is changing how `rowcount` and `colcount` are derived from `maxrc` and the true aspect ratio, and there are corresponding size differences for each cell because of that. When `UseOppositeRCRatio` is **true**, it inverts the value of the true aspect ratio before using it for row and column counts.

```CSharp
if (UseOppositeRCRatio) { aspectratio = 1 / aspectratio;}
```

## The scenario for BoxPanel

The particular scenario for `BoxPanel` is that it's a panel where one of the main determinants of how to divide space is by knowing the number of child items, and dividing the known available space for the panel. Panels are innately rectangle shapes. Many panels operate by dividing that rectangle space into further rectangles; that's what [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) does for its cells. In **Grid**'s case, the size of the cells is set by [**ColumnDefinition**](/uwp/api/Windows.UI.Xaml.Controls.ColumnDefinition) and [**RowDefinition**](/uwp/api/Windows.UI.Xaml.Controls.RowDefinition) values, and elements declare the exact cell they go into with [**Grid.Row**](/dotnet/api/system.windows.controls.grid.row) and [**Grid.Column**](/dotnet/api/system.windows.controls.grid.column) attached properties. Getting good layout from a **Grid** usually requires knowing the number of child elements beforehand, so that there are enough cells and each child element sets its attached properties to fit into its own cell.

But what if the number of children is dynamic? That's certainly possible; your app code can add items to collections, in response to any dynamic run-time condition you consider to be important enough to be worth updating your UI. If you're using data binding to backing collections/business objects, getting such updates and updating the UI is handled automatically, so that's often the preferred technique (see [Data binding in depth](../../data-binding/data-binding-in-depth.md)).

But not all app scenarios lend themselves to data binding. Sometimes, you need to create new UI elements at runtime and make them visible. `BoxPanel` is for this scenario. A changing number of child items is no problem for `BoxPanel` because it's using the child count in calculations, and adjusts both the existing and new child elements into a new layout so they all fit.

An advanced scenario for extending `BoxPanel` further (not shown here) could both accommodate dynamic children and use a child's [**DesiredSize**](/uwp/api/windows.ui.xaml.uielement.desiredsize) as a stronger factor for the sizing of individual cells. This scenario might use varying row or column sizes or non-grid shapes so that there's less "wasted" space. This requires a strategy for how multiple rectangles of various sizes and aspect ratios can all fit into a containing rectangle both for aesthetics and smallest size. `BoxPanel` doesn't do that; it's using a simpler technique for dividing space. `BoxPanel`'s technique is to determine the least square number that's greater than the child count. For example, 9 items would fit in a 3x3 square. 10 items require a 4x4 square. However, you can often fit items while still removing one row or column of the starting square, to save space. In the count=10 example, that fits in a 4x3 or 3x4 rectangle.

You might wonder why the panel wouldn't instead choose 5x2 for 10 items, because that fits the item number neatly. However, in practice, panels are sized as rectangles that seldom have a strongly oriented aspect ratio. The least-squares technique is a way to bias the sizing logic to work well with typical layout shapes and not encourage sizing where the cell shapes get odd aspect ratios.

## Related topics

**Reference**

* [**FrameworkElement.ArrangeOverride**](/uwp/api/windows.ui.xaml.frameworkelement.arrangeoverride)
* [**FrameworkElement.MeasureOverride**](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride)
* [**Panel**](/uwp/api/Windows.UI.Xaml.Controls.Panel)

**Concepts**

* [Alignment, margin, and padding](alignment-margin-padding.md)