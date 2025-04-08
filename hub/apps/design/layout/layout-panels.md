---
description: Learn how to use built-in XAML layout panels like RelativePanel, StackPanel, Grid, and Canvas to arrange and group UI elements in your app.
title: Layout panels for Windows apps
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Layout panels

Layout panels are containers that allow you to arrange and group UI elements in your app. The built-in XAML layout panels include [**RelativePanel**](/uwp/api/Windows.UI.Xaml.Controls.RelativePanel), [**StackPanel**](/uwp/api/Windows.UI.Xaml.Controls.StackPanel), [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid), [**VariableSizedWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.VariableSizedWrapGrid), and [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas). Here, we describe each panel and show how to use it to layout XAML UI elements.

There are several things to consider when choosing a layout panel:
- How the panel positions its child elements.
- How the panel sizes its child elements.
- How overlapping child elements are layered on top of each other (z-order).
- The number and complexity of nested panel elements needed to create your desired layout.

## Examples

<table>
<th align="left">WinUI 2 Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-sm.png" alt="WinUI Gallery"></img></td>
<td>
    <p>If you have the <strong>WinUI 2 Gallery</strong> app installed, see the <a href="winui2gallery:/item/RelativePanel">RelativePanel</a>, <a href="winui2gallery:/item/StackPanel">StackPanel</a>, <a href="winui2gallery:/item/Grid">Grid</a>, <a href="winui2gallery:/item/VariableSizedWrapGrid">VariableSizedWrapGrid</a>, and <a href="winui2gallery:/item/Canvas">Canvas</a> in action.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the WinUI 2 Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/WinUI-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

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

For more info about attached properties, see [Attached properties overview](/windows/uwp/xaml-platform/attached-properties-overview).

### Panel borders

The RelativePanel, StackPanel, and Grid panels define border properties that let you draw a border around the panel without wrapping them in an additional Border element. The border properties are **BorderBrush**, **BorderThickness**, **CornerRadius**, and **Padding**.

Here’s an example of how to set border properties on a Grid.

```xaml
<Grid BorderBrush="Blue" BorderThickness="12" CornerRadius="12" Padding="12">
    <TextBlock Text="Hello World!"/>
</Grid>
```

![A Grid with borders](images/layout-panel-grid-border.png)

Using the built-in border properties reduces the XAML element count, which can improve the UI performance of your app. For more info about layout panels and UI performance, see [Optimize your XAML layout](/windows/uwp/debug-test-perf/optimize-your-xaml-layout).

## RelativePanel

[**RelativePanel**](/uwp/api/Windows.UI.Xaml.Controls.RelativePanel) lets you layout UI elements by specifying where they go in relation to other elements and in relation to the panel. By default, an element is positioned in the upper left corner of the panel. You can use RelativePanel with [**VisualStateManager**](/uwp/api/Windows.UI.Xaml.VisualStateManager) and [**AdaptiveTrigger**](/uwp/api/Windows.UI.Xaml.AdaptiveTrigger) to rearrange your UI for different window sizes.

This table shows the attached properties you can use to align an element in relation to the panel or other elements.

Panel alignment | Sibling alignment | Sibling position
----------------|-------------------|-----------------
[**AlignTopWithPanel**](/uwp/api/windows.ui.xaml.controls.relativepanel.aligntopwithpanelproperty) | [**AlignTopWith**](/uwp/api/windows.ui.xaml.controls.relativepanel.aligntopwithproperty) | [**Above**](/uwp/api/windows.ui.xaml.controls.relativepanel)  
[**AlignBottomWithPanel**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignbottomwithpanelproperty) | [**AlignBottomWith**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignbottomwithproperty) | [**Below**](/uwp/api/windows.ui.xaml.controls.relativepanel.belowproperty)  
[**AlignLeftWithPanel**](/uwp/api/windows.ui.xaml.controls.relativepanel) | [**AlignLeftWith**](/uwp/api/windows.ui.xaml.controls.relativepanel.getalignleftwith) | [**LeftOf**](/uwp/api/windows.ui.xaml.controls.relativepanel.leftofproperty)  
[**AlignRightWithPanel**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignrightwithpanelproperty) | [**AlignRightWith**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignrightwithproperty) | [**RightOf**](/uwp/api/windows.ui.xaml.controls.relativepanel.setrightof)  
[**AlignHorizontalCenterWithPanel**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignhorizontalcenterwithpanelproperty) | [**AlignHorizontalCenterWith**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignhorizontalcenterwithproperty) | &nbsp;   
[**AlignVerticalCenterWithPanel**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignverticalcenterwithpanelproperty) | [**AlignVerticalCenterWith**](/uwp/api/windows.ui.xaml.controls.relativepanel.alignverticalcenterwithproperty) | &nbsp;   

 
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

![Relative panel](images/layout-panel-relative-panel.png)

Here are a few things to note about the sizing of the rectangles:
- The red rectangle is given an explicit size of 44x44. It's placed in the upper left corner of the panel, which is the default position.
- The green rectangle is given an explicit height of 44. Its left side is aligned with the red rectangle, and its right side is aligned with the blue rectangle, which determines its width.
- The orange rectangle isn't given an explicit size. Its left side is aligned with the blue rectangle. Its right and bottom edges are aligned with the edge of the panel. Its size is determined by these alignments and it will resize as the panel resizes.

## StackPanel

[**StackPanel**](/uwp/api/Windows.UI.Xaml.Controls.StackPanel) arranges its child elements into a single line that can be oriented horizontally or vertically. StackPanel is typically used to arrange a small subsection of the UI on a page.

You can use the [**Orientation**](/uwp/api/windows.ui.xaml.controls.stackpanel.orientation) property to specify the direction of the child elements. The default orientation is [**Vertical**](/uwp/api/Windows.UI.Xaml.Controls.Orientation).

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

![Stack panel](images/layout-panel-stack-panel.png)

In a StackPanel, if a child element's size is not set explicitly, it stretches to fill the available width (or height if the Orientation is **Horizontal**). In this example, the width of the rectangles is not set. The rectangles expand to fill the entire width of the StackPanel.

## Grid

The [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) panel supports fluid layouts and allows you to arrange controls in multi-row and multi-column layouts. You specify a Grid's rows and columns by using the [**RowDefinitions**](/uwp/api/windows.ui.xaml.controls.grid.rowdefinitions) and [**ColumnDefinitions**](/uwp/api/windows.ui.xaml.controls.grid.columndefinitions) properties.

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

![Grid](images/layout-panel-grid.png)

In this example, the sizing works like this: 
- The second row has an explicit height of 44 effective pixels. By default, the height of the first row fills whatever space is left over.
- The width of the first column is set to **Auto**, so it's as wide as needed for its children. In this case, it's 44 effective pixels wide to accommodate the width of the red rectangle.
- There are no other size constraints on the rectangles, so each one stretches to fill the grid cell it's in.

You can distribute space within a column or a row by using **Auto** or star sizing. You use auto sizing to let UI elements resize to fit their content or parent container. You can also use auto sizing with the rows and columns of a grid. To use auto sizing, set the Height and/or Width of UI elements to **Auto**.

You use proportional sizing, also called *star sizing*, to distribute available space among the rows and columns of a grid by weighted proportions. In XAML, star values are expressed as \* (or *n*\* for weighted star sizing). For example, to specify that one column is 5 times wider than the second column in a 2-column layout, use "5\*" and "\*" for the [**Width**](/uwp/api/windows.ui.xaml.controls.columndefinition.width) properties in the [**ColumnDefinition**](/uwp/api/Windows.UI.Xaml.Controls.ColumnDefinition) elements.

This example combines fixed, auto, and proportional sizing in a [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) with 4 columns.

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

![A 4 column grid in the Visual Studio designer](images/xaml-layout-grid-in-designer.png)

## VariableSizedWrapGrid

[**VariableSizedWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.VariableSizedWrapGrid) is a Grid-style layout panel where rows or columns automatically wrap to a new row or column when the [**MaximumRowsOrColumns**](/uwp/api/windows.ui.xaml.controls.variablesizedwrapgrid.maximumrowsorcolumns) value is reached. 

The [**Orientation**](/uwp/api/windows.ui.xaml.controls.variablesizedwrapgrid.orientation) property specifies whether the grid adds its items in rows or columns before wrapping. The default orientation is **Vertical**, which means the grid adds items from top to bottom until a column is full, then wraps to a new column. When the value is **Horizontal**, the grid adds items from left to right, then wraps to a new row.

Cell dimensions are specified by the [**ItemHeight**](/uwp/api/windows.ui.xaml.controls.variablesizedwrapgrid.itemheight) and [**ItemWidth**](/uwp/api/windows.ui.xaml.controls.variablesizedwrapgrid.itemwidth). Each cell is the same size. If ItemHeight or ItemWidth is not specified, then the first cell sizes to fit its content, and every other cell is the size of the first cell.

You can use the [**VariableSizedWrapGrid.ColumnSpan**](/uwp/api/windows.ui.xaml.controls.variablesizedwrapgrid) and [**VariableSizedWrapGrid.RowSpan**](/uwp/api/windows.ui.xaml.controls.variablesizedwrapgrid.getrowspan) attached properties to specify how many adjacent cells a child element should fill.

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

![Variable size wrap grid](images/layout-panel-variable-size-wrap-grid.png)

In this example, the maximum number of rows in each column is 3. The first column contains only 2 items (the red and blue rectangles) because the blue rectangle spans 2 rows. The green rectangle then wraps to the top of the next column.

## Canvas

The [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) panel positions its child elements using fixed coordinate points and does not support fluid layouts. You specify the points on individual child elements by setting the [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) and [**Canvas.Top**](/dotnet/api/system.windows.controls.canvas.top) attached properties on each element. The parent Canvas reads these attached property values from its children during the [Arrange](/uwp/api/windows.ui.xaml.uielement.arrange) pass of layout.

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

![Canvas](images/layout-panel-canvas.png)

Use the Canvas panel with discretion. While it's convenient to be able to precisely control positions of elements in UI for some scenarios, a fixed positioned layout panel causes that area of your UI to be less adaptive to overall app window size changes. App window resize might come from device orientation changes, split app windows, changing monitors, and a number of other user scenarios.

## Panels for ItemsControl

There are several special-purpose panels that can be used only as an [**ItemsPanel**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemspanel) to display items in an [**ItemsControl**](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl). These are [**ItemsStackPanel**](/uwp/api/Windows.UI.Xaml.Controls.ItemsStackPanel), [**ItemsWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.ItemsWrapGrid), [**VirtualizingStackPanel**](/uwp/api/Windows.UI.Xaml.Controls.VirtualizingStackPanel), and [**WrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.WrapGrid). You can't use these panels for general UI layout.

## Get the sample code

- [WinUI 2 Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.
