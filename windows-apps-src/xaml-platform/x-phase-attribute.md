---
title: xPhase attribute
description: Use xPhase with the xBind markup extension to render ListView and GridView items incrementally and improve the panning experience.
ms.assetid: BD17780E-6A34-4A38-8D11-9703107E247E
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:Phase attribute


Use **x:Phase** with the [{x:Bind} markup extension](x-bind-markup-extension.md) to render [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) items incrementally and improve the panning experience. **x:Phase** provides a declarative way of achieving the same effect as using the [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging) event to manually control the rendering of list items. Also see [Update ListView and GridView items incrementally](../debug-test-perf/optimize-gridview-and-listview.md#update-items-incrementally).

## XAML attribute usage


``` syntax
<object x:Phase="PhaseValue".../>
```

## XAML values


| Term | Description |
|------|-------------|
| PhaseValue | A number that indicates the phase in which the element will be processed. The default is 0. | 

## Remarks

If a list is panned fast with touch, or using the mouse wheel, then depending on the complexity of the data template, the list may not be able to render items fast enough to keep up with the speed of scrolling. This is particularly true for a portable device with a power-efficient CPU such as a tablet.

Phasing enables incremental rendering of the data template so that the contents can be prioritized, and the most important elements rendered first. This enables the list to show some content for each item if panning fast, and will render more elements of each template as time permits.

## Example

```xml
<DataTemplate x:Key="PhasedFileTemplate" x:DataType="model:FileItem">
    <Grid Width="200" Height="80">
        <Grid.ColumnDefinitions>
           <ColumnDefinition Width="75" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Image Grid.RowSpan="4" Source="{x:Bind ImageData}" MaxWidth="70" MaxHeight="70" x:Phase="3"/>
        <TextBlock Text="{x:Bind DisplayName}" Grid.Column="1" FontSize="12"/>
        <TextBlock Text="{x:Bind prettyDate}"  Grid.Column="1"  Grid.Row="1" FontSize="12" x:Phase="1"/>
        <TextBlock Text="{x:Bind prettyFileSize}"  Grid.Column="1"  Grid.Row="2" FontSize="12" x:Phase="2"/>
        <TextBlock Text="{x:Bind prettyImageSize}"  Grid.Column="1"  Grid.Row="3" FontSize="12" x:Phase="2"/>
    </Grid>
</DataTemplate>
```

The data template describes 4 phases:

1.  Presents the DisplayName text block. All controls without a phase specified will be implicitly considered to be part of phase 0.
2.  Shows the prettyDate text block.
3.  Shows the prettyFileSize and prettyImageSize text blocks.
4.  Shows the image.

Phasing is a feature of [{x:Bind}](x-bind-markup-extension.md) that works with controls derived from [**ListViewBase**](/uwp/api/Windows.UI.Xaml.Controls.ListViewBase) and that incrementally processes the item template for data binding. When rendering list items, **ListViewBase** renders a single phase for all items in the view before moving onto the next phase. The rendering work is performed in time-sliced batches so that as the list is scrolled, the work required can be re-assessed, and not performed for items that are no longer visible.

The **x:Phase** attribute can be specified on any element in a data template that uses [{x:Bind}](x-bind-markup-extension.md). When an element has a phase other than 0, the element will be hidden from view (via **Opacity**, not **Visibility**) until that phase is processed and bindings are updated. When a [**ListViewBase**](/uwp/api/Windows.UI.Xaml.Controls.ListViewBase)-derived control is scrolled, it will recycle the item templates from items that are no longer on screen to render the newly visible items. UI elements within the template will retain their old values until they are data-bound again. Phasing causes that data-binding step to be delayed, and therefore phasing needs to hide the UI elements in case they are stale.

Each UI element may have only one phase specified. If so, that will apply to all bindings on the element. If a phase is not specified, phase 0 is assumed.

Phase numbers do not need to be contiguous and are the same as the value of [**ContainerContentChangingEventArgs.Phase**](/uwp/api/windows.ui.xaml.controls.containercontentchangingeventargs.phase). The [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging) event will be raised for each phase before the **x:Phase** bindings are processed.

Phasing only affects [{x:Bind}](x-bind-markup-extension.md) bindings, not [{Binding}](binding-markup-extension.md) bindings.

Phasing will only apply when the item template is rendered using a control that is aware of phasing. For Windows 10, that means [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView). Phasing will not apply to data templates used in other item controls, or for other scenarios such as [**ContentTemplate**](/uwp/api/windows.ui.xaml.controls.contentcontrol.contenttemplate) or [**Hub**](/uwp/api/Windows.UI.Xaml.Controls.Hub) sections—in those cases, all the UI elements will be data bound at once.