---
description: Get item templates that you can use with a ListView control to display single, double, triple, and tabular list items.
title: Item templates for list view
template: detail.hbs
ms.date: 11/03/2017
ms.topic: article
keywords: windows 10, uwp, fluent
---
# Item templates for list view

This section contains item templates that you can use with a [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ListView) control. Use these templates to get the look of common app types.

To demonstrate data binding, these templates bind **ListViewItems** to the example Recording class from the [data binding overview](/windows/uwp/data-binding/data-binding-quickstart).

> [!NOTE] 
> Currently, when a **DataTemplate** contains multiple controls (for example, more than a single **TextBlock**), the default accessible name for screenreaders comes from .ToString() on the item. As a convenience you can instead set the [**AutomationProperties.Name**](/uwp/api/windows.ui.xaml.automation.automationproperties) on the root element of the **DataTemplate**. For more on accessibility, see [Accessibility overview](../accessibility/accessibility-overview.md).

## Single line list item
Use this template to display a list of items with an image and a single line of text.

![single line list item example](images/listitems/singlelineexample.png)
![single line list item](images/listitems/singlelineicon.png)
```xaml
<ListView ItemsSource="{x:Bind ViewModel.Recordings}">
    <ListView.ItemTemplate>
        <DataTemplate x:Name="SingleLineDataTemplate" x:DataType="local:Recording">
            <StackPanel Orientation="Horizontal" Height="44" Padding="12" AutomationProperties.Name="{x:Bind CompositionName}">
                <Image Source="Placeholder.png" Height="16" Width="16" VerticalAlignment="Center"/>
                <TextBlock Text="{x:Bind CompositionName}" VerticalAlignment="Center" Style="{ThemeResource BaseTextBlockStyle}" Foreground="{ThemeResource SystemControlPageTextBaseHighBrush}" Margin="12,0,0,0"/>
            </StackPanel>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Double line list item 
Use this template to display a list of items with an image and two lines of text.

![double line list item with icon example](images/listitems/doublelineexample.png) 
![double line list item with icon](images/listitems/doublelineicon.png)

```xaml
<ListView ItemsSource="{x:Bind ViewModel.Recordings}">
    <ListView.ItemTemplate>
        <DataTemplate x:Name="DoubleLineDataTemplate" x:DataType="local:Recording">
            <StackPanel Orientation="Horizontal" Height="64" AutomationProperties.Name="{x:Bind CompositionName}">
                <Ellipse Height="48" Width="48" VerticalAlignment="Center">
                    <Ellipse.Fill>
                        <ImageBrush ImageSource="Placeholder.png"/>
                    </Ellipse.Fill>
                </Ellipse>
                <StackPanel Orientation="Vertical" VerticalAlignment="Center" Margin="12,0,0,0">
                    <TextBlock Text="{x:Bind CompositionName}"  Style="{ThemeResource BaseTextBlockStyle}" Foreground="{ThemeResource SystemControlPageTextBaseHighBrush}" />
                    <TextBlock Text="{x:Bind ArtistName}" Style="{ThemeResource BodyTextBlockStyle}" Foreground="{ThemeResource SystemControlPageTextBaseMediumBrush}"/>
                </StackPanel>
            </StackPanel>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Triple line list item
Use this template to display a list of items with three lines of text.

![triple line list item example](images/listitems/triplelineexample.png)
![triple line list item](images/listitems/tripleline.png)

```xaml
<ListView ItemsSource="{x:Bind ViewModel.Recordings}">
    <ListView.ItemTemplate>
        <DataTemplate x:Name="TripleLineDataTemplate" x:DataType="local:Recording">
            <StackPanel Height="84" Padding="20" AutomationProperties.Name="{x:Bind CompositionName}">
                <TextBlock Text="{x:Bind CompositionName}"  Style="{ThemeResource BaseTextBlockStyle}" Margin="0,4,0,0"/>
                <TextBlock Text="{x:Bind ArtistName}" Style="{ThemeResource CaptionTextBlockStyle}" Opacity=".8" Margin="0,4,0,0"/>
                <TextBlock Text="{x:Bind ReleaseDateTime}" Style="{ThemeResource CaptionTextBlockStyle}" Opacity=".6" Margin="0,4,0,0"/>
            </StackPanel>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Table list item
Use this template to display a list of items with text in defined columns.

![table list item example](images/listitems/tablelist.png)
```xaml
<ListView  ItemsSource="{x:Bind ViewModel.Recordings}">
    <ListView.HeaderTemplate>
        <DataTemplate>
            <Grid Padding="12" Background="{ThemeResource SystemBaseLowColor}">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="408"/>
                    <ColumnDefinition Width="360"/>
                    <ColumnDefinition Width="360"/>
                </Grid.ColumnDefinitions>
                <TextBlock Text="Composition" Style="{ThemeResource CaptionTextBlockStyle}"/>
                <TextBlock Grid.Column="1" Text="Artist" Style="{ThemeResource CaptionTextBlockStyle}"/>
                <TextBlock Grid.Column="2" Text="Release Date" Style="{ThemeResource CaptionTextBlockStyle}"/>
            </Grid>
        </DataTemplate>
    </ListView.HeaderTemplate>
    <ListView.ItemTemplate>
        <DataTemplate x:Name="TableDataTemplate" x:DataType="local:Recording">
            <Grid Height="48" AutomationProperties.Name="{x:Bind CompositionName}">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="48"/>
                    <ColumnDefinition Width="360"/>
                    <ColumnDefinition Width="360"/>
                    <ColumnDefinition Width="360"/>
                </Grid.ColumnDefinitions>
                <Ellipse Height="32" Width="32" VerticalAlignment="Center">
                    <Ellipse.Fill>
                        <ImageBrush ImageSource="Placeholder.png"/>
                    </Ellipse.Fill>
                </Ellipse>
                <TextBlock Grid.Column="1" VerticalAlignment="Center" Style="{ThemeResource BaseTextBlockStyle}" Text="{x:Bind CompositionName}" />
                <TextBlock Grid.Column="2" VerticalAlignment="Center" Text="{x:Bind ArtistName}"/>
                <TextBlock Grid.Column="3" VerticalAlignment="Center" Text="{x:Bind ReleaseDateTime}"/>
            </Grid>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Related articles
- [ListView class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview)
- [Data binding overview](/windows/uwp/data-binding/data-binding-quickstart)
- [Accessibility overview](../accessibility/accessibility-overview.md)
- [ListView and GridView sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlListView)
- [Thumbnail images](/windows/uwp/files/thumbnails)
