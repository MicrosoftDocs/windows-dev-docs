---
description: Learn about the move and draw commands (a mini-language) that you can use to specify path geometries as a XAML attribute value.
title: Move and draw commands syntax
ms.assetid: 7772BC3E-A631-46FF-9940-3DD5B9D0E0D9
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Move and draw commands syntax


Learn about the move and draw commands (a mini-language) that you can use to specify path geometries as a XAML attribute value. Move and draw commands are used by many design and graphics tools that can output a vector graphic or shape, as a serialization and interchange format.

## Properties that use move and draw command strings

The move and draw command syntax is supported by an internal type converter for XAML, which parses the commands and produces a run-time graphics representation. This representation is basically a finished set of vectors that is ready for presentation. The vectors themselves don't complete the presentation details; you'll still need to set other values on the elements. For a [**Path**](/uwp/api/Windows.UI.Xaml.Shapes.Path) object you also need values for [**Fill**](/uwp/api/Windows.UI.Xaml.Shapes.Shape.Fill), [**Stroke**](/uwp/api/windows.ui.xaml.shapes.shape.stroke), and other properties, and then that **Path** must be connected to the visual tree somehow. For a [**PathIcon**](/uwp/api/Windows.UI.Xaml.Controls.PathIcon) object, set the [**Foreground**](/uwp/api/windows.ui.xaml.controls.iconelement.foreground) property.

There are two properties in the Windows Runtime that can use a string representing move and draw commands: [**Path.Data**](/uwp/api/windows.ui.xaml.shapes.path.data) and [**PathIcon.Data**](/uwp/api/windows.ui.xaml.controls.pathicon.data). If you set one of these properties by specifying move and draw commands, you typically set it as a XAML attribute value along with other required attributes of that element. Without getting into the specifics, here's what that looks like:

```xml
<Path x:Name="Arrow" Fill="White" Height="11" Width="9.67"
  Data="M4.12,0 L9.67,5.47 L4.12,10.94 L0,10.88 L5.56,5.47 L0,0.06" />
```

[**PathGeometry.Figures**](/uwp/api/windows.ui.xaml.media.pathgeometry.figures) can also use move and draw commands. You might combine a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) object that uses move and draw commands with other [**Geometry**](/uwp/api/Windows.UI.Xaml.Media.Geometry) types in a [**GeometryGroup**](/uwp/api/Windows.UI.Xaml.Media.GeometryGroup) object, which you'd then use as the value for [**Path.Data**](/uwp/api/windows.ui.xaml.shapes.path.data). But that isn't nearly as common as using move and draw commands for attribute-defined data.

## Using move and draw commands versus using a **PathGeometry**

For Windows Runtime XAML, the move and draw commands produce a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) with a single [**PathFigure**](/uwp/api/Windows.UI.Xaml.Media.PathFigure) object with a [**Figures**](/uwp/api/windows.ui.xaml.media.pathgeometry.figures) property value. Each draw command produces a [**PathSegment**](/uwp/api/Windows.UI.Xaml.Media.PathSegment) derived class in that single **PathFigure**'s [**Segments**](/uwp/api/windows.ui.xaml.media.pathfigure.segments) collection, the move command changes the [**StartPoint**](/uwp/api/windows.ui.xaml.media.pathfigure.startpoint), and existence of a close command sets [**IsClosed**](/uwp/api/windows.ui.xaml.media.pathfigure.isclosed) to **true**. You can navigate this structure as an object model if you examine the **Data** values at run time.

## The basic syntax

The syntax for move and draw commands can be summarized like this:

1.  Start with an optional fill rule. Typically you specify this only if you don't want the **EvenOdd** default. (More about **EvenOdd** later.)
2.  Specify exactly one move command.
3.  Specify one or more draw commands.
4.  Specify a close command. You can omit a close command , but that would leave your figure open (that's uncommon).

General rules of this syntax are:

-   Each command is represented by exactly one letter.
-   That letter can be upper-case or lower-case. Case matters, as we'll describe.
-   Each command except the close command is typically followed by one or more numbers.
-   If more than one number for a command, separate with a comma or space.

**\[**_fillRule_**\]** _moveCommand_ _drawCommand_ **\[**_drawCommand_**\*\]** **\[**_closeCommand_**\]**

Many of the draw commands use points, where you provide an _x,y_ value. Whenever you see a \*_points_ placeholder you can assume you're giving two decimal values for the _x,y_ value of a point.

White space can often be omitted when the result is not ambiguous. You can in fact omit all white space if you use commas as your separator for all number sets (points and size). For example, this usage is legal: `F1M0,58L2,56L6,60L13,51L15,53L6,64z`. But it's more typical to include white space between commands for clarity.

Don't use commas as the decimal point for decimal numbers; the command string is interpreted by XAML and doesn't account for culture-specific number-formatting conventions that differ from those used in the **en-us** locale.

## Syntax specifics

**Fill rule**

There are two possible values for the optional fill rule: **F0** or **F1**. (The **F** is always uppercase.) **F0** is the default value; it produces **EvenOdd** fill behavior, so you don't typically specify it. Use **F1** to get the **Nonzero** fill behavior. These fill values align with the values of the [**FillRule**](/uwp/api/Windows.UI.Xaml.Media.FillRule) enumeration.

**Move command**

Specifies the start point of a new figure.

| Syntax |
|--------|
| `M ` _startPoint_ <br/>- or -<br/>`m` _startPoint_|

| Term | Description |
|------|-------------|
| _startPoint_ | [**Point**](/uwp/api/Windows.Foundation.Point) <br/>The start point of a new figure.|

An uppercase **M** indicates that *startPoint* is an absolute coordinate; a lowercase **m** indicates that *startPoint* is an offset to the previous point, or (0,0) if there was no previous point.

**Note**  It's legal to specify multiple points after the move command. A line is drawn to those points as if you specified the line command. However that's not a recommended style; use the dedicated line command instead.

**Draw commands**

A draw command can consist of several shape commands: line, horizontal line, vertical line, cubic Bezier curve, quadratic Bezier curve, smooth cubic Bezier curve, smooth quadratic Bezier curve, and elliptical arc.

For all draw commands, case matters. Uppercase letters denote absolute coordinates and lowercase letters denote coordinates relative to the previous command.

The control points for a segment are relative to the end point of the preceding segment. When sequentially entering more than one command of the same type, you can omit the duplicate command entry. For example, `L 100,200 300,400` is equivalent to `L 100,200 L 300,400`.

**Line command**

Creates a straight line between the current point and the specified end point. `l 20 30` and `L 20,30` are examples of valid line commands. Defines the equivalent of a [**LineGeometry**](/uwp/api/Windows.UI.Xaml.Media.LineGeometry) object.

| Syntax |
|--------|
| `L` _endPoint_ <br/>- or -<br/>`l` _endPoint_ |

| Term | Description |
|------|-------------|
| endPoint | [**Point**](/uwp/api/Windows.Foundation.Point)<br/>The end point of the line.|

**Horizontal line command**

Creates a horizontal line between the current point and the specified x-coordinate. `H 90` is an example of a valid horizontal line command.

| Syntax |
|--------|
| `H ` _x_ <br/> - or - <br/>`h ` _x_ |

| Term | Description |
|------|-------------|
| x | [**Double**](/dotnet/api/system.double) <br/> The x-coordinate of the end point of the line. |

**Vertical line command**

Creates a vertical line between the current point and the specified y-coordinate. `v 90` is an example of a valid vertical line command.

| Syntax |
|--------|
| `V ` _y_ <br/> - or - <br/> `v ` _y_ |

| Term | Description |
|------|-------------|
| *y* | [**Double**](/dotnet/api/system.double) <br/> The y-coordinate of the end point of the line. |

**Cubic Bézier curve command**

Creates a cubic Bézier curve between the current point and the specified end point by using the two specified control points (*controlPoint1* and *controlPoint2*). `C 100,200 200,400 300,200` is an example of a valid curve command. Defines the equivalent of a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) object with a [**BezierSegment**](/uwp/api/Windows.UI.Xaml.Media.BezierSegment) object.

| Syntax |
|--------|
| `C ` *controlPoint1* *controlPoint2* *endPoint* <br/> - or - <br/> `c ` *controlPoint1* *controlPoint2* *endPoint* |

| Term | Description |
|------|-------------|
| *controlPoint1* | [**Point**](/uwp/api/Windows.Foundation.Point) <br/> The first control point of the curve, which determines the starting tangent of the curve. |
| *controlPoint2* | [**Point**](/uwp/api/Windows.Foundation.Point) <br/> The second control point of the curve, which determines the ending tangent of the curve. |
| *endPoint* | [**Point**](/uwp/api/Windows.Foundation.Point) <br/> The point to which the curve is drawn. | 

**Quadratic Bézier curve command**

Creates a quadratic Bézier curve between the current point and the specified end point by using the specified control point (*controlPoint*). `q 100,200 300,200` is an example of a valid quadratic Bézier curve command. Defines the equivalent of a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) with a [**QuadraticBezierSegment**](/uwp/api/Windows.UI.Xaml.Media.QuadraticBezierSegment).

| Syntax |
|--------|
| `Q ` *controlPoint endPoint* <br/> - or - <br/> `q ` *controlPoint endPoint* |

| Term | Description |
|------|-------------|
| *controlPoint* | [**Point**](/uwp/api/Windows.Foundation.Point) <br/> The control point of the curve, which determines the starting and ending tangents of the curve. |
| *endPoint* | [**Point**](/uwp/api/Windows.Foundation.Point)<br/> The point to which the curve is drawn. |

**Smooth cubic Bézier curve command**

Creates a cubic Bézier curve between the current point and the specified end point. The first control point is assumed to be the reflection of the second control point of the previous command relative to the current point. If there is no previous command or if the previous command was not a cubic Bézier curve command or a smooth cubic Bézier curve command, assume the first control point is coincident with the current point. The second control point—the control point for the end of the curve—is specified by *controlPoint2*. For example, `S 100,200 200,300` is a valid smooth cubic Bézier curve command. This command defines the equivalent of a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) with a [**BezierSegment**](/uwp/api/Windows.UI.Xaml.Media.BezierSegment) where there was preceding curve segment.

| Syntax |
|--------|
| `S` *controlPoint2* *endPoint* <br/> - or - <br/>`s` *controlPoint2 endPoint* |

| Term | Description |
|------|-------------|
| *controlPoint2* | [**Point**](/uwp/api/Windows.Foundation.Point) <br/> The control point of the curve, which determines the ending tangent of the curve. |
| *endPoint* | [**Point**](/uwp/api/Windows.Foundation.Point)<br/> The point to which the curve is drawn. |

**Smooth quadratic Bézier curve command**

Creates a quadratic Bézier curve between the current point and the specified end point. The control point is assumed to be the reflection of the control point of the previous command relative to the current point. If there is no previous command or if the previous command was not a quadratic Bézier curve command or a smooth quadratic Bézier curve command, the control point is coincident with the current point. This command defines the equivalent of a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) with a [**QuadraticBezierSegment**](/uwp/api/Windows.UI.Xaml.Media.QuadraticBezierSegment) where there was preceding curve segment.

| Syntax |
|--------|
| `T` *controlPoint* *endPoint* <br/> - or - <br/> `t` *controlPoint* *endPoint* |

| Term | Description |
|------|-------------|
| *controlPoint* | [**Point**](/uwp/api/Windows.Foundation.Point)<br/> The control point of the curve, which determines the starting and tangent of the curve. |
| *endPoint* | [**Point**](/uwp/api/Windows.Foundation.Point)<br/> The point to which the curve is drawn. |

**Elliptical arc command**

Creates an elliptical arc between the current point and the specified end point. Defines the equivalent of a [**PathGeometry**](/uwp/api/Windows.UI.Xaml.Media.PathGeometry) with an [**ArcSegment**](/uwp/api/Windows.UI.Xaml.Media.ArcSegment).

| Syntax |
|--------|
| `A ` *size* *rotationAngle* *isLargeArcFlag* *sweepDirectionFlag* *endPoint* <br/> - or - <br/>`a ` *sizerotationAngleisLargeArcFlagsweepDirectionFlagendPoint* |

| Term | Description |
|------|-------------|
| *size* | [**Size**](/uwp/api/Windows.Foundation.Size)<br/>The x-radius and y-radius of the arc. |
| *rotationAngle* | [**Double**](/dotnet/api/system.double) <br/> The rotation of the ellipse, in degrees. |
| *isLargeArcFlag* | Set to 1 if the angle of the arc should be 180 degrees or greater; otherwise, set to 0. |
| *sweepDirectionFlag* | Set to 1 if the arc is drawn in a positive-angle direction; otherwise, set to 0. |
| *endPoint* | [**Point**](/uwp/api/Windows.Foundation.Point) <br/> The point to which the arc is drawn.|
 
**Close command**

Ends the current figure and creates a line that connects the current point to the starting point of the figure. This command creates a line-join (corner) between the last segment and the first segment of the figure.

| Syntax |
|--------|
| `Z` <br/> - or - <br/> `z ` |

**Point syntax**

Describes the x-coordinate and y-coordinate of a point. See also [**Point**](/uwp/api/Windows.Foundation.Point).

| Syntax |
|--------|
| *x*,*y*<br/> - or - <br/>*x* *y* |

| Term | Description |
|------|-------------|
| *x* | [**Double**](/dotnet/api/system.double) <br/> The x-coordinate of the point. |
| *y* | [**Double**](/dotnet/api/system.double) <br/> The y-coordinate of the point. |

**Additional notes**

Instead of a standard numerical value, you can also use the following special values. These values are case sensitive.

-   **Infinity**: Represents **PositiveInfinity**.
-   **\-Infinity**: Represents **NegativeInfinity**.
-   **NaN**: Represents **NaN**.

Instead of using decimals or integers, you can use scientific notation. For example, `+1.e17` is a valid value.

## Design tools that produce move and draw commands

Using the **Pen** tool and other drawing tools in Blend for Microsoft Visual Studio 2015 will usually produce a [**Path**](/uwp/api/Windows.UI.Xaml.Shapes.Path) object, with move and draw commands.

You might see existing move and draw command data in some of the control parts defined in the Windows Runtime XAML default templates for controls. For example, some controls use a [**PathIcon**](/uwp/api/Windows.UI.Xaml.Controls.PathIcon) that has the data defined as move and draw commands.

There are exporters or plug-ins available for other commonly used vector-graphics design tools that can output the vector in XAML form. These usually create [**Path**](/uwp/api/Windows.UI.Xaml.Shapes.Path) objects in a layout container, with move and draw commands for [**Path.Data**](/uwp/api/windows.ui.xaml.shapes.path.data). There may be multiple **Path** elements in the XAML so that different brushes can be applied. Many of these exporters or plug-ins were originally written for Windows Presentation Foundation (WPF)  XAML or Silverlight, but the XAML path syntax is identical with Windows Runtime XAML. Usually, you can use chunks of XAML from an exporter and paste them right into a Windows Runtime XAML page. (However, you won't be able to use a **RadialGradientBrush**, if that was part of the converted XAML, because Windows Runtime XAML doesn't support that brush.)

## Related topics

* [Draw shapes](../design/controls-and-patterns/shapes.md)
* [Use brushes](../design/style/brushes.md)
* [**Path.Data**](/uwp/api/windows.ui.xaml.shapes.path.data)
* [**PathIcon**](/uwp/api/Windows.UI.Xaml.Controls.PathIcon)