---
description: Learn how to use built-in XAML layout panels like RelativePanel, StackPanel, Grid, and Canvas to arrange and group UI elements in your app.
title: Layout panels for Windows apps
ms.date: 10/13/2026
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Layout panels

Layout panels are containers that allow you to arrange and group UI elements in your app. The built-in XAML layout panels include [**RelativePanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RelativePanel), [**StackPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.StackPanel), [**Grid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Grid), [**VariableSizedWrapGrid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.VariableSizedWrapGrid), and [**Canvas**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Canvas). Here, we describe each panel and show how to use it to layout XAML UI elements.

There are several things to consider when choosing a layout panel:
- How the panel positions its child elements.
- How the panel sizes its child elements.
- How overlapping child elements are layered on top of each other (z-order).
- The number and complexity of nested panel elements needed to create your desired layout.

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see layout panels in action](winui3gallery://category/Layout)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## Panel properties

Before we discuss individual panels, let's go over some common properties that all panels have. 

### Panel attached properties

Most XAML layout panels use attached properties to let their child elements inform the parent panel about how they should be positioned in the UI. Attached properties use the syntax *AttachedPropertyProvider.PropertyName*. If you have panels that are nested inside other panels, attached properties on UI elements that specify layout characteristics to a parent are interpreted by the most immediate parent panel only.

Here is an example of how you can set the [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) attached property on a Button control in XAML. This informs the parent Canvas that the Button should be positioned 50 effective pixels from the left edge of the Canvas.

```xaml
<Canvas>
  <Button Canvas.Left="50">Hello</Button>
</Canvas>
```

For more info about attached properties, see [Attached properties overview](../platform/xaml/attached-properties-overview.md).

### Panel borders

The RelativePanel, StackPanel, and Grid panels define border properties that let you draw a border around the panel without wrapping them in an additional Border element. The border properties are **BorderBrush**, **BorderThickness**, **CornerRadius**, and **Padding**.

Here’s an example of how to set border properties on a Grid.

```xaml
<Grid BorderBrush="Blue" BorderThickness="12" CornerRadius="12" Padding="12">
    <TextBlock Text="Hello World!"/>
</Grid>
```

![A Grid with borders](../../design/layout/images/layout-panel-grid-border.png)

Using the built-in border properties reduces the XAML element count, which can improve the UI performance of your app. For more info about layout panels and UI performance, see [Optimize your XAML layout](../performance/optimize-xaml-layout.md).

## RelativePanel

[**RelativePanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RelativePanel) lets you layout UI elements by specifying where they go in relation to other elements and in relation to the panel. By default, an element is positioned in the upper left corner of the panel. You can use RelativePanel with [**VisualStateManager**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.VisualStateManager) and [**AdaptiveTrigger**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.AdaptiveTrigger) to rearrange your UI for different window sizes.

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the RelativePanel in action](winui3gallery://item/RelativePanel)

This table shows the attached properties you can use to align an element in relation to the panel or other elements.

Panel alignment | Sibling alignment | Sibling position
----------------|-------------------|-----------------
[**AlignTopWithPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.aligntopwithpanelproperty) | [**AlignTopWith**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.aligntopwithproperty) | [**Above**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel)  
[**AlignBottomWithPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignbottomwithpanelproperty) | [**AlignBottomWith**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignbottomwithproperty) | [**Below**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.belowproperty)  
[**AlignLeftWithPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel) | [**AlignLeftWith**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.getalignleftwith) | [**LeftOf**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.leftofproperty)  
[**AlignRightWithPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignrightwithpanelproperty) | [**AlignRightWith**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignrightwithproperty) | [**RightOf**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.setrightof)  
[**AlignHorizontalCenterWithPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignhorizontalcenterwithpanelproperty) | [**AlignHorizontalCenterWith**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignhorizontalcenterwithproperty) | &nbsp;   
[**AlignVerticalCenterWithPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignverticalcenterwithpanelproperty) | [**AlignVerticalCenterWith**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.relativepanel.alignverticalcenterwithproperty) | &nbsp;   

 
This XAML shows how to arrange elements in a RelativePanel.

```xaml
<RelativePanel BorderBrush="Gray" BorderThickness="1">
    <Rectangle x:Name="RedRect" Fill="Red" Height="44" Width="44"/>
    <Rectangle x:Name="BlueRect" Fill="Blue"
               Height="44" Width="88"
               RelativePanel.RightOf="RedRect" />

    <Rectangle x:Name="GreenRect" Fill="Green" 
               Height="44"
               RelativePanel.Below="RedRect" 
               RelativePanel.AlignLeftWith="RedRect" 
               RelativePanel.AlignRightWith="BlueRect"/>
    <Rectangle Fill="Orange"
               RelativePanel.Below="GreenRect" 
               RelativePanel.AlignLeftWith="BlueRect" 
               RelativePanel.AlignRightWithPanel="True"
               RelativePanel.AlignBottomWithPanel="True"/>
</RelativePanel>
```

The result looks like this. 

![Relative panel](../../design/layout/images/layout-panel-relative-panel.png)

Here are a few things to note about the sizing of the rectangles:
- The red rectangle is given an explicit size of 44x44. It's placed in the upper left corner of the panel, which is the default position.
- The green rectangle is given an explicit height of 44. Its left side is aligned with the red rectangle, and its right side is aligned with the blue rectangle, which determines its width.
- The orange rectangle isn't given an explicit size. Its left side is aligned with the blue rectangle. Its right and bottom edges are aligned with the edge of the panel. Its size is determined by these alignments and it will resize as the panel resizes.

## StackPanel

[**StackPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.StackPanel) arranges its child elements into a single line that can be oriented horizontally or vertically. StackPanel is typically used to arrange a small subsection of the UI on a page.

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the StackPanel in action](winui3gallery://item/StackPanel)

You can use the [**Orientation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stackpanel.orientation) property to specify the direction of the child elements. The default orientation is [**Vertical**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Orientation).

The following XAML shows how to create a vertical StackPanel of items.

```xaml
<StackPanel>
    <Rectangle Fill="Red" Height="44"/>
    <Rectangle Fill="Blue" Height="44"/>
    <Rectangle Fill="Green" Height="44"/>
    <Rectangle Fill="Orange" Height="44"/>
</StackPanel>
```


The result looks like this.

![Stack panel](../../design/layout/images/layout-panel-stack-panel.png)

In a StackPanel, if a child element's size is not set explicitly, it stretches to fill the available width (or height if the Orientation is **Horizontal**). In this example, the width of the rectangles is not set. The rectangles expand to fill the entire width of the StackPanel.

## Grid

The [**Grid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Grid) panel supports fluid layouts and allows you to arrange controls in multi-row and multi-column layouts. You specify a Grid's rows and columns by using the [**RowDefinitions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.grid.rowdefinitions) and [**ColumnDefinitions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.grid.columndefinitions) properties.

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the Grid in action](winui3gallery://item/Grid)

To position objects in specific cells of the Grid, use the [**Grid.Column**](/dotnet/api/system.windows.controls.grid.column) and [**Grid.Row**](/dotnet/api/system.windows.controls.grid.row) attached properties.

To make content span across multiple rows and columns, use the [**Grid.RowSpan**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/ms605035(v=vs.95)) and [**Grid.ColumnSpan**](/dotnet/api/system.windows.controls.grid.columnspan) attached properties.

This XAML example shows how to create a Grid with two rows and two columns.

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition/>
        <RowDefinition Height="44"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto"/>
        <ColumnDefinition/>
    </Grid.ColumnDefinitions>
    <Rectangle Fill="Red" Width="44"/>
    <Rectangle Fill="Blue" Grid.Row="1"/>
    <Rectangle Fill="Green" Grid.Column="1"/>
    <Rectangle Fill="Orange" Grid.Row="1" Grid.Column="1"/>
</Grid>
```


The result looks like this.

![Grid](../../design/layout/images/layout-panel-grid.png)

In this example, the sizing works like this: 
- The second row has an explicit height of 44 effective pixels. By default, the height of the first row fills whatever space is left over.
- The width of the first column is set to **Auto**, so it's as wide as needed for its children. In this case, it's 44 effective pixels wide to accommodate the width of the red rectangle.
- There are no other size constraints on the rectangles, so each one stretches to fill the grid cell it's in.

You can distribute space within a column or a row by using **Auto** or star sizing. You use auto sizing to let UI elements resize to fit their content or parent container. You can also use auto sizing with the rows and columns of a grid. To use auto sizing, set the Height and/or Width of UI elements to **Auto**.

You use proportional sizing, also called *star sizing*, to distribute available space among the rows and columns of a grid by weighted proportions. In XAML, star values are expressed as \* (or *n*\* for weighted star sizing). For example, to specify that one column is 5 times wider than the second column in a 2-column layout, use "5\*" and "\*" for the [**Width**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.columndefinition.width) properties in the [**ColumnDefinition**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ColumnDefinition) elements.

This example combines fixed, auto, and proportional sizing in a [**Grid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Grid) with 4 columns.

| Column | Sizing | Description |
| ------ | ------ | ----------- |
Column_1 | **Auto** | The column will size to fit its content.
Column_2 | * | After the Auto columns are calculated, the column gets part of the remaining width. Column_2 will be one-half as wide as Column_4.
Column_3 | **44** | The column will be 44 pixels wide.
Column_4 | **2**\* | After the Auto columns are calculated, the column gets part of the remaining width. Column_4 will be twice as wide as Column_2.

The default column width is "*", so you don't need to explicitly set this value for the second column.

```xaml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto"/>
        <ColumnDefinition/>
        <ColumnDefinition Width="44"/>
        <ColumnDefinition Width="2*"/>
    </Grid.ColumnDefinitions>
    <TextBlock Text="Column 1 sizes to its content." FontSize="24"/>
</Grid>
```

In the Visual Studio XAML designer, the result looks like this.

![A 4 column grid in the Visual Studio designer](../../design/layout/images/xaml-layout-grid-in-designer.png)

## VariableSizedWrapGrid

[**VariableSizedWrapGrid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.VariableSizedWrapGrid) is a Grid-style layout panel where rows or columns automatically wrap to a new row or column when the [**MaximumRowsOrColumns**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.variablesizedwrapgrid.maximumrowsorcolumns) value is reached. 

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the VariableSizedWrapGrid in action](winui3gallery://item/VariableSizedWrapGrid)

The [**Orientation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.variablesizedwrapgrid.orientation) property specifies whether the grid adds its items in rows or columns before wrapping. The default orientation is **Vertical**, which means the grid adds items from top to bottom until a column is full, then wraps to a new column. When the value is **Horizontal**, the grid adds items from left to right, then wraps to a new row.

Cell dimensions are specified by the [**ItemHeight**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.variablesizedwrapgrid.itemheight) and [**ItemWidth**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.variablesizedwrapgrid.itemwidth). Each cell is the same size. If ItemHeight or ItemWidth is not specified, then the first cell sizes to fit its content, and every other cell is the size of the first cell.

You can use the [**VariableSizedWrapGrid.ColumnSpan**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.variablesizedwrapgrid) and [**VariableSizedWrapGrid.RowSpan**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.variablesizedwrapgrid.getrowspan) attached properties to specify how many adjacent cells a child element should fill.

Here's how to use a VariableSizedWrapGrid in XAML.

```xaml
<VariableSizedWrapGrid MaximumRowsOrColumns="3" ItemHeight="44" ItemWidth="44">
    <Rectangle Fill="Red"/>
    <Rectangle Fill="Blue" 
               VariableSizedWrapGrid.RowSpan="2"/>
    <Rectangle Fill="Green" 
               VariableSizedWrapGrid.ColumnSpan="2"/>
    <Rectangle Fill="Orange" 
               VariableSizedWrapGrid.RowSpan="2" 
               VariableSizedWrapGrid.ColumnSpan="2"/>
</VariableSizedWrapGrid>
```


The result looks like this.

![Variable size wrap grid](../../design/layout/images/layout-panel-variable-size-wrap-grid.png)

In this example, the maximum number of rows in each column is 3. The first column contains only 2 items (the red and blue rectangles) because the blue rectangle spans 2 rows. The green rectangle then wraps to the top of the next column.

## Canvas

The [**Canvas**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Canvas) panel positions its child elements using fixed coordinate points and does not support fluid layouts. You specify the points on individual child elements by setting the [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) and [**Canvas.Top**](/dotnet/api/system.windows.controls.canvas.top) attached properties on each element. The parent Canvas reads these attached property values from its children during the [Arrange](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.arrange) pass of layout.

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the Canvas in action](winui3gallery://item/Canvas)

Objects in a Canvas can overlap, where one object is drawn on top of another object. By default, the Canvas renders child objects in the order in which they’re declared, so the last child is rendered on top (each element has a default z-index of 0). This is the same as other built-in panels. However, Canvas also supports the [**Canvas.ZIndex**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/cc190397(v=vs.95)) attached property that you can set on each of the child elements. You can set this property in code to change the draw order of elements during run time. The element with the highest Canvas.ZIndex value draws last and therefore draws over any other elements that share the same space or overlap in any way. Note that alpha value (transparency) is respected, so even if elements overlap, the contents shown in overlap areas might be blended if the top one has a non-maximum alpha value.

The Canvas does not do any sizing of its children. Each element must specify its size.

Here's an example of a Canvas in XAML.

```xaml
<Canvas Width="120" Height="120">
    <Rectangle Fill="Red" Height="44" Width="44"/>
    <Rectangle Fill="Blue" Height="44" Width="44" Canvas.Left="20" Canvas.Top="20"/>
    <Rectangle Fill="Green" Height="44" Width="44" Canvas.Left="40" Canvas.Top="40"/>
    <Rectangle Fill="Orange" Height="44" Width="44" Canvas.Left="60" Canvas.Top="60"/>
</Canvas>
```

The result looks like this.

![Canvas](../../design/layout/images/layout-panel-canvas.png)

Use the Canvas panel with discretion. While it's convenient to be able to precisely control positions of elements in UI for some scenarios, a fixed positioned layout panel causes that area of your UI to be less adaptive to overall app window size changes. App window resize might come from device orientation changes, split app windows, changing monitors, and a number of other user scenarios.

## WrapPanel

> [!IMPORTANT]
> `WrapPanel` is available starting in [Windows App SDK 2.0 Experimental 2](../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-20-experimental-2-200-experimental2). [Windows App SDK 2.0 Experimental 4](../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-20-experimental-4-200-experimental4) renamed its spacing properties to `ItemSpacing` and `LineSpacing`. The APIs are marked experimental and might change or be removed in a future release. Don't use them in production apps.

[**WrapPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanel?view=windows-app-sdk-2.0-experimental&preserve-view=true) arranges its child elements one after another in a line. When the next element doesn't fit in the space that remains on the line, the panel moves it to the start of a new line. Use a WrapPanel for a small set of elements, such as tags or filter buttons, that should reflow when the available space changes. WrapPanel is a port of the [Windows Community Toolkit WrapPanel](/dotnet/communitytoolkit/windows/primitives/wrappanel).

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the WrapPanel in action](winui3gallery://item/WrapPanel)

The [**Orientation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanel.orientation?view=windows-app-sdk-2.0-experimental&preserve-view=true) property specifies the direction in which the panel adds items. The default orientation is **Horizontal**, which means the panel adds items from left to right, then wraps to a new row. When the value is **Vertical**, the panel adds items from top to bottom, then wraps to a new column. When the panel's [**FlowDirection**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.flowdirection) is **RightToLeft**, for example in an app that's localized for a right-to-left language, the panel mirrors this layout. It fills each row from right to left, and it adds each new column to the left of the previous one.

Unlike VariableSizedWrapGrid, WrapPanel doesn't use cells of a fixed size. Each child keeps its desired size in the direction of the orientation. Within a row, each child gets a slot as tall as the tallest child in that row. Within a column, each child gets a slot as wide as the widest child in that column.

Use these properties to add space to the layout:

- [**ItemSpacing**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanel.itemspacing?view=windows-app-sdk-2.0-experimental&preserve-view=true) sets the space between adjacent items on the same line. The default is 0.
- [**LineSpacing**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanel.linespacing?view=windows-app-sdk-2.0-experimental&preserve-view=true) sets the space between lines. The default is 0.
- [**Padding**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanel.padding?view=windows-app-sdk-2.0-experimental&preserve-view=true) sets the space between the edges of the panel and its items.

Here's how to use a WrapPanel in XAML.

```xaml
<WrapPanel Width="132" ItemSpacing="4" LineSpacing="4">
    <Rectangle Fill="Red" Width="44" Height="44"/>
    <Rectangle Fill="Blue" Width="80" Height="44"/>
    <Rectangle Fill="Green" Width="44" Height="44"/>
    <Rectangle Fill="Orange" Width="60" Height="44"/>
</WrapPanel>
```

The result looks like this.

![A WrapPanel with red and blue rectangles on the first row, and green and orange rectangles on the second row](../../design/layout/images/layout-panel-wrap-panel.png)

In this example, the red and blue rectangles and the item spacing between them use 128 of the panel's 132 pixels, so they share the first row. The green rectangle doesn't fit in the remaining space, so it wraps to a second row. The orange rectangle fits next to the green rectangle on the second row.

To make the last child fill the space that remains on its line, set the [**ItemsStretch**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanel.itemsstretch?view=windows-app-sdk-2.0-experimental&preserve-view=true) property to **Last**. The default value, **None**, keeps every child at its desired size. Don't set an explicit width on the last child, or an explicit height when the orientation is **Vertical**, because an explicit size prevents the child from stretching.

```xaml
<WrapPanel Width="132" ItemSpacing="4" LineSpacing="4" ItemsStretch="Last">
    <Rectangle Fill="Red" Width="44" Height="44"/>
    <Rectangle Fill="Blue" Width="80" Height="44"/>
    <Rectangle Fill="Green" Width="44" Height="44"/>
    <Rectangle Fill="Orange" Height="44"/>
</WrapPanel>
```

The result looks like this.

![A WrapPanel in which the orange rectangle stretches to fill the rest of the second row](../../design/layout/images/layout-panel-wrap-panel-items-stretch.png)

In this example, the orange rectangle fills the rest of the second row. To set the value in code, use the [**WrapPanelItemsStretch**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.wrappanelitemsstretch?view=windows-app-sdk-2.0-experimental&preserve-view=true) enumeration.

```csharp
// Stretch the last child of a WrapPanel named tagPanel.
tagPanel.ItemsStretch = WrapPanelItemsStretch.Last;
```

WrapPanel wraps its children only when the space available to it in the direction of the orientation is limited. For example, a horizontal StackPanel doesn't limit the width of its children, so a horizontal WrapPanel inside it shows all of its children in one row. The panel doesn't reserve space or add spacing for children whose **Visibility** is **Collapsed**.

WrapPanel doesn't virtualize its children. It creates and lays out every child, including children that are scrolled out of view. To show a large collection that wraps, use a [GridView](controls/listview-and-gridview.md) or an [ItemsRepeater](controls/items-repeater.md) with a [UniformGridLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout). These controls support UI virtualization.

## Panels for ItemsControl

There are several special-purpose panels that can be used only as an [**ItemsPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemspanel) to display items in an [**ItemsControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ItemsControl). These are [**ItemsStackPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ItemsStackPanel), [**ItemsWrapGrid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ItemsWrapGrid), [**VirtualizingStackPanel**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.VirtualizingStackPanel), and [**WrapGrid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.WrapGrid). You can't use these panels for general UI layout.

## Get the sample code

- [WinUI 3 Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.
