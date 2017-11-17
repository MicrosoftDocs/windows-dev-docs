---
author: Jwmsft
ms.assetid: 02141F86-355E-4046-86EA-2A89D615B7DB
title: Use brushes
description: Brush objects are used to paint the interiors or outlines of shapes, text, and parts of controls, so that the object being painted is visible in a UI.
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Using brushes to paint backgrounds, foregrounds, and outlines

You use [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) objects to paint the interiors or outlines of XAML shapes, text, and parts of controls, so that the object being painted is visible in a UI. Let's look at the available brushes and how to use them.

> **Important APIs**:  [Brush class](/uwp/api/Windows.UI.Xaml.Media.Brush)

## Introduction to brushes

To paint an object such as a [**Shape**](/uwp/api/Windows.UI.Xaml.Shapes.Shape) or the parts of a [**Control**](https://msdn.microsoft.com/library/windows/apps/BR209390) that is displayed on the app canvas, you use a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush). For example, you set the [**Fill**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.shapes.shape.fill.aspx) property of the **Shape** or the [**Background**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.control.background.aspx) and [**Foreground**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.control.foreground.aspx) properties of a **Control** to a **Brush** value, and that **Brush** determines how the UI element paints or is rendered in UI. 

The different types of brushes are: 
-   [**AcrylicBrush**](/uwp/api/windows.ui.xaml.media.acrylicbrush)
-   [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962)
-   [**LinearGradientBrush**](https://msdn.microsoft.com/library/windows/apps/BR210108) 
-   [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101)
-   [**WebViewBrush**](https://msdn.microsoft.com/library/windows/apps/BR227703)
-   [**XamlCompositionBrushBase**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase)

## Solid color brushes

A [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) paints an area with a single [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723), such as red or blue. This is the most basic brush. There are three ways in XAML to define a **SolidColorBrush** and the solid color it specifies: predefined color names, hexadecimal color values, or the property element syntax.

### Predefined color names

You can use a predefined color name, such as [**Yellow**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.colors.yellow.aspx) or [**Magenta**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.colors.magenta.aspx). There are 256 available named colors. The XAML parser converts the color name to a [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723) structure with the correct color channels. The 256 named colors are based on the *X11* color names from the Cascading Style Sheets, Level 3 (CSS3) specification, so you may already be familiar with this list of named colors if you have previous experience with web development or design.

Here's an example that sets the [**Fill**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.shapes.shape.fill.aspx) property of a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) to the predefined color [**Red**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.colors.red.aspx).

```xml
<Rectangle Width="100" Height="100" Fill="Red" />
```

This image shows the [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) as applied to the [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle).

![A rendered SolidColorBrush.](images/brushes-solidcolorbrush.jpg)

If you are defining a [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) using code rather than XAML, each named color is available as a static property value of the [**Colors**](https://msdn.microsoft.com/library/windows/apps/windows.ui.colors) class. For example, to declare a [**Color**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.solidcolorbrush.color.aspx) value of a **SolidColorBrush** to represent the named color "Orchid", set the **Color** value to the static value [**Colors.Orchid**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.colors.orchid.aspx).

### Hexadecimal color values

You can use a hexadecimal format string to declare precise 24-bit color values with 8-bit alpha channel for a [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962). Two characters in the range 0 to F define each component value, and the component value order of the hexadecimal string is: alpha channel (opacity), red channel, green channel, and blue channel (**ARGB**). For example, the hexadecimal value "\#FFFF0000" defines fully opaque red (alpha="FF", red="FF", green="00", and blue="00").

This XAML example sets the [**Fill**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.shapes.shape.fill.aspx) property of a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) to the hexadecimal value "\#FFFF0000", and gives an identical result to using the named color [**Colors.Red**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.colors.red.aspx).

```xml
<StackPanel>
  <Rectangle Width="100" Height="100" Fill="#FFFF0000" />
</StackPanel>
```

### <span id="Property_element_syntax__"></span><span id="property_element_syntax__"></span><span id="PROPERTY_ELEMENT_SYNTAX__"></span>Property element syntax

You can use property element syntax to define a [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962). This syntax is more verbose than the previous methods, but you can specify additional property values on an element, such as the [**Opacity**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.brush.opacity.aspx). For more info on XAML syntax, including property element syntax, see [XAML overview](https://msdn.microsoft.com/library/windows/apps/Mt185595) and [XAML syntax guide](https://msdn.microsoft.com/library/windows/apps/Mt185596).

In the previous examples, you never even saw the string "SolidColorBrush" appear in the syntax. The brush being created is created implicitly and automatically, as part of a deliberate XAML language shorthand that helps keep UI definition simple for the most common cases. The next example creates a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) and explicitly creates the [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) as an element value for a [**Rectangle.Fill**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.shapes.shape.fill.aspx) property. The [**Color**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.solidcolorbrush.color.aspx) of the **SolidColorBrush** is set to [**Blue**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.colors.blue.aspx) and the [**Opacity**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.brush.opacity.aspx) is set to 0.5.

```xml
<Rectangle Width="200" Height="150">
    <Rectangle.Fill>
        <SolidColorBrush Color="Blue" Opacity="0.5" />
    </Rectangle.Fill>
</Rectangle>
```

## <span id="Linear_gradient_brushes_"></span><span id="linear_gradient_brushes_"></span><span id="LINEAR_GRADIENT_BRUSHES_"></span>Linear gradient brushes

A [**LinearGradientBrush**](https://msdn.microsoft.com/library/windows/apps/BR210108) paints an area with a gradient that's defined along a line. This line is called the *gradient axis*. You specify the gradient's colors and their locations along the gradient axis using [**GradientStop**](https://msdn.microsoft.com/library/windows/apps/BR210078) objects. By default, the gradient axis runs from the upper left corner to the lower right corner of the area that the brush paints, resulting in a diagonal shading.

The [**GradientStop**](https://msdn.microsoft.com/library/windows/apps/BR210078) is the basic building block of a gradient brush. A gradient stop specifies what the [**Color**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.gradientstop.color.aspx) of the brush is at an [**Offset**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.gradientstop.offset.aspx) along the gradient axis, when the brush is applied to the area being painted.

The gradient stop's [**Color**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.gradientstop.color.aspx) property specifies the color of the gradient stop. You can set the color by using a predefined color name or by specifying the hexadecimal **ARGB** values.

The [**Offset**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.gradientstop.offset.aspx) property of a [**GradientStop**](https://msdn.microsoft.com/library/windows/apps/BR210078) specifies the position of each **GradientStop** along the gradient axis. The **Offset** is a **double** that ranges from 0 to 1. An **Offset** of 0 places the **GradientStop** at the start of the gradient axis, in other words near its [**StartPoint**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.lineargradientbrush.startpoint.aspx). An **Offset** of 1 places the **GradientStop** at the [**EndPoint**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.lineargradientbrush.endpoint.aspx). At a minimum, a useful [**LinearGradientBrush**](https://msdn.microsoft.com/library/windows/apps/BR210108) should have two **GradientStop** values, where each **GradientStop** should specify a different [**Color**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.gradientstop.color.aspx) and have a different **Offset** between 0 and 1.

This example creates a linear gradient with four colors and uses it to paint a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle).

```xml
<!-- This rectangle is painted with a diagonal linear gradient. -->
<Rectangle Width="200" Height="100">
    <Rectangle.Fill>
        <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
            <GradientStop Color="Yellow" Offset="0.0" x:Name="GradientStop1"/>
            <GradientStop Color="Red" Offset="0.25" x:Name="GradientStop2"/>
            <GradientStop Color="Blue" Offset="0.75" x:Name="GradientStop3"/>
            <GradientStop Color="LimeGreen" Offset="1.0" x:Name="GradientStop4"/>
        </LinearGradientBrush>
    </Rectangle.Fill>
</Rectangle>
```

The color of each point between gradient stops is linearly interpolated as a combination of the color specified by the two bounding gradient stops. The illustration highlights the gradient stops in the previous example. The circles mark the position of the gradient stops, and the dashed line shows the gradient axis.

![Gradient stops](images/linear-gradients-stops.png)
You can change the line at which the gradient stops are positioned by setting the [**StartPoint**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.lineargradientbrush.startpoint.aspx) and [**EndPoint**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.lineargradientbrush.endpoint.aspx) properties to be different values than the `(0,0)` and `(1,1)` starting defaults. By changing the **StartPoint** and **EndPoint** coordinate values, you can create horizontal or vertical gradients, reverse the gradient direction, or condense the gradient spread to apply to a smaller range than the full painted area. To condense the gradient, you set values of **StartPoint** and/or **EndPoint** to be something that is between the values 0 and 1. For example, if you want a horizontal gradient where the fade all happens on the left half of the brush and the right side is solid to your last [**GradientStop**](https://msdn.microsoft.com/library/windows/apps/BR210078) color, you specify a **StartPoint** of `(0,0)` and an **EndPoint** of `(0.5,0)`.

### <span id="Use_tools_to_make_gradients"></span><span id="use_tools_to_make_gradients"></span><span id="USE_TOOLS_TO_MAKE_GRADIENTS"></span>Use tools to make gradients

Now that you know how linear gradients work, you can use Visual Studio or Blend to make creating these gradients easier. To create a gradient, select the object you want to apply a gradient to on the design surface or in XAML view. Expand **Brush** and select the **Linear Gradient** tab (see next screenshot).

![Create linear gradient using Visual Studio.](images/tool-gradient-brush-1.png)

Now you can change the colors of the gradient stops and slide their positions using the bar on the bottom. You can also add new gradient stops by clicking on the bar and remove them by dragging the stops off of the bar (see next screenshot).

![Bar at bottom of properties window that controls gradient stops.](images/tool-gradient-brush-2.png)

## <span id="Image_brushes"></span><span id="image_brushes"></span><span id="IMAGE_BRUSHES"></span>Image brushes

An [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101) paints an area with an image, with the image to paint coming from an image file source. You set the [**ImageSource**](https://msdn.microsoft.com/library/windows/apps/BR210107) property with the path of the image to load. Typically, the image source comes from a **Content** item that is part of your app's resources.

By default, an [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101) stretches its image to completely fill the painted area, possibly distorting the image if the painted area has a different aspect ratio than the image. You can change this behavior by changing the [**Stretch**](https://msdn.microsoft.com/library/windows/apps/BR242975) property from its default value of **Fill** and setting it as **None**, **Uniform**, or **UniformToFill**.

The next example creates an [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101) and sets the [**ImageSource**](https://msdn.microsoft.com/library/windows/apps/BR210107) to an image named licorice.jpg, which you must include as a resource in the app. The **ImageBrush** then paints the area defined by an [**Ellipse**](/uwp/api/Windows.UI.Xaml.Shapes.Ellipse) shape.

```xml
<Ellipse Height="200" Width="300">
   <Ellipse.Fill>
     <ImageBrush ImageSource="licorice.jpg" />
   </Ellipse.Fill>
</Ellipse>
```

Here's what the rendered [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101) looks like.

![A rendered ImageBrush.](images/brushes-imagebrush.jpg)

[**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101) and [**Image**](https://msdn.microsoft.com/library/windows/apps/BR242752) both reference an image source file by Uniform Resource Identifier (URI), where that image source file uses several possible image formats. These image source files are specified as URIs. For more info about specifying image sources, the usable image formats, and packaging them in an app, see [Image and ImageBrush](https://msdn.microsoft.com/library/windows/apps/Mt280382).

## Brushes and text

You can also use brushes to apply rendering characteristics to text elements. For example, the [**Foreground**](https://msdn.microsoft.com/library/windows/apps/BR209665) property of [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652) takes a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush). You can apply any of the brushes described here to text. But be careful with brushes applied to text, because it's possible to make the text unreadable if you use brushes that bleed into whatever background the text is rendered on top of, or that distract from the outlines of text characters. Use [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) for readability of text elements in most cases, unless you want the text element to be mostly decorative.

Even when you use a solid color, make sure that the text color you choose has enough contrast against the background color of the text's layout container. The level of contrast between text foreground and text container background is an accessibility consideration.

## WebViewBrush

A [**WebViewBrush**](https://msdn.microsoft.com/library/windows/apps/BR227703) is a special type of brush that can access the content normally viewed in a [**WebView**](https://msdn.microsoft.com/library/windows/apps/BR227702) control. Instead of rendering the content in the rectangular **WebView** control area, **WebViewBrush** paints that content onto another element that has a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush)-type property for a render surface. **WebViewBrush** isn't appropriate for every brush scenario, but is useful for transitions of a **WebView**. For more info, see [**WebViewBrush**](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.WebViewBrush).

## XamlCompositionBrushBase

[**XamlCompositionBrushBase**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase) is a base class used to create custom brushes that use [**CompositionBrush**](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Composition.CompositionBrush) to paint XAML UI elements.

This enables "drop down" interoperation between the Windows.UI.Xaml and Windows.UI.Composition layers as described in the [**Visual Layer overview**](/windows/uwp/composition/visual-layer). 

To create a custom brush, create a new class that inherits from XamlCompositionBrushBase and implements the required methods.

For example, this can be used to apply [**effects**](/windows/uwp/composition/composition-effects) to XAML UIElements using a [**CompositionEffectBrush**](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Composition.CompositionEffectBrush), such as a **GaussianBlurEffect** or a [**SceneLightingEffect**](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Composition.Effects.SceneLightingEffect) that controls the reflective properties of a XAML UIElement when being lit by a [**XamlLight**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.xamllight).

For code examples, see the reference page for [**XamlCompositionBrushBase**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase).

## Brushes as XAML resources

You can declare any brush to be a keyed XAML resource in a XAML resource dictionary. This makes it easy to replicate the same brush values as applied to multiple elements in a UI. The brush values are then shared and applied to any case where you reference the brush resource as a [{StaticResource}](https://msdn.microsoft.com/library/windows/apps/Mt185588) usage in your XAML. This includes cases where you have a XAML control template that references the shared brush, and the control template is itself a keyed XAML resource.

## Brushes in code

It's much more typical to specify brushes using XAML than it is to use code to define brushes. This is because brushes are usually defined as XAML resources, and because brush values are often the output of design tools or otherwise as part of a XAML UI definition. Still, for the occasional case where you might want to define a brush using code, all the [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) types are available for code instantiation.

To create a [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) in code, use the constructor that takes a [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723) parameter. Pass a value that is a static property of the [**Colors**](https://msdn.microsoft.com/library/windows/apps/windows.ui.colors) class, like this:

```cs
SolidColorBrush blueBrush = new SolidColorBrush(Windows.UI.Colors.Blue);
```

```vb
Dim blueBrush as SolidColorBrush = New SolidColorBrush(Windows.UI.Colors.Blue)
```

```cpp
blueBrush = ref new SolidColorBrush(Windows::UI::Colors::Blue);
```

For [**WebViewBrush**](https://msdn.microsoft.com/library/windows/apps/BR227703) and [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101), use the default constructor and then call other APIs before you attempt to use that brush for a UI property.

-   [**ImageSource**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.imagebrush.imagesourceproperty.aspx) requires a [**BitmapImage**](https://msdn.microsoft.com/library/windows/apps/BR243235) (not a URI) when you define an [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101) using code. If your source is a stream , use the [**SetSourceAsync**](https://msdn.microsoft.com/library/windows/apps/JJ191522) method to initialize the value. If your source is a URI, which includes content in your app that uses the **ms-appx** or **ms-resource** schemes, use the [**BitmapImage**](https://msdn.microsoft.com/library/windows/apps/br243238.aspx) constructor that takes a URI. You might also consider handling the [**ImageOpened**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.imagebrush.imageopened.aspx) event if there are any timing issues with retrieving or decoding the image source, where you might need alternate content to display until the image source is available.
-   For [**WebViewBrush**](https://msdn.microsoft.com/library/windows/apps/BR227703) you might need to call [**Redraw**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewbrush.redraw.aspx) if you've recently reset the [**SourceName**](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewbrush.sourcename.aspx) property or if the content of the [**WebView**](https://msdn.microsoft.com/library/windows/apps/BR227702) is also being changed with code.

For code examples, see reference pages for [**WebViewBrush**](https://msdn.microsoft.com/library/windows/apps/BR227703),  [**ImageBrush**](https://msdn.microsoft.com/library/windows/apps/BR210101), and [**XamlCompositionBrushBase**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase).
 

 




