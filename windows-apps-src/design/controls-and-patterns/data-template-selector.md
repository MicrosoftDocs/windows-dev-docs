---
Description: Use data template selectors to customize the styles of your items based on the item properties.
title: Data template selection
label: Data template selection
template: detail.hbs
ms.date: 10/18/2019
ms.topic: article
keywords: windows 10, uwp
pm-contact: anawish
---

# Data template selection: Styling items based on their properties

The customized design of collections controls are managed by a [DataTemplate](/uwp/api/windows.ui.xaml.datatemplate). Data templates define how each item should be laid out and styled, and that markup is applied to every item in the collection. This article explains how to use a [DataTemplateSelector](/uwp/api/windows.ui.xaml.controls.datatemplateselector) to apply different data templates on a collection and select which data template to use, based on certain item properties or values of your choosing.

> **Important APIs**: [DataTemplateSelector](/uwp/api/windows.ui.xaml.controls.datatemplateselector), [DataTemplate](/uwp/api/windows.ui.xaml.datatemplate)

[DataTemplateSelector](/uwp/api/windows.ui.xaml.controls.datatemplateselector) is a class that enables custom template selection logic. It lets you define rules that specify which data template to use for certain items in a collection. To implement this logic, you create a subclass of DataTemplateSelector in your code-behind and define the logic that determines which data template to use for which category of items (for example, items of a certain type or items with a certain property value, etc). You declare an instance of this class in the Resources section of your XAML file, along with the definitions of the data templates you'll be using. You identify these resources with an `x:Key` value, which lets you reference them in your XAML.

## Prerequisites

- How to [use and create a collection control, such as a ListView or GridView.](listview-and-gridview.md)
- How to [customize the look of your items using a DataTemplate.](item-containers-templates.md#data-template)

## When not to use a DataTemplateSelector

Generally, you should not give every item in a ListView or GridView a completely different layout/style - this would be poor use of a DataTemplateSelector, and negatively impact performance.

Certain elements of the visual display of a list item can be controlled by using just one data template, through binding certain properties. For example, items can each have a different icon by binding to an icon source property in the data template, and giving each item a different value for that icon source property. This would achieve better performance than using a DataTemplateSelector.

## When to use a DataTemplateSelector

You should create a [DataTemplateSelector](/uwp/api/windows.ui.xaml.controls.datatemplateselector) when you want to use multiple data templates in one collection control. A `DataTemplateSelector` gives you the flexibility to make certain items stand out, while still keeping items in a similar layout. There are many use cases in which a DataTemplateSelector is helpful, and a few scenarios in which it is better to re-think the control and strategy that you're using.

Collection controls are typically bound to a collection of items that are all of one type. However, even though items may be of the same type, they may have different values for certain properties or represent different meanings. Certain items also may be more important than others, or one item may be particularly important or different and has a need to visually stand out. In these situations, a DataTemplateSelector will be very helpful.

You can also bind to a collection that contains different types of items - the bound collection can have a mix of strings, ints, custom class objects, and more. This makes DataTemplateSelector especially useful as it can assign different data templates based on the object type of an item.

Here are some examples of when you might use a data template selector:

- Representing different levels of employees within a ListView - each type/level of employee may need a different color background to be easily distinguishable.
- Representing sale items in a product gallery that uses a GridView - a sale item may need a red background, or a different color font to make it stand out from regular-priced items.
- Representing winners/top photos in a photo gallery using FlipView.
- Needing to represent negative/positive numbers in a ListView differently, or short strings/long strings.

## Create a DataTemplateSelector

When you create a data template selector, you define the template selection logic in your code, and you define the data templates in your XAML.

### Code-behind component

To use a data template selector, you first create a subclass of [DataTemplateSelector](/uwp/api/windows.ui.xaml.controls.datatemplateselector) (a class that derives from it) in your code-behind. In your class, you declare each template as a property of the class. Then, you override the [SelectTemplateCore](/uwp/api/windows.ui.xaml.controls.datatemplateselector.selecttemplatecore) method to include your own template selection logic.

Here is an example of a simple `DataTemplateSelector` subclass called `MyDataTemplateSelector`.

```csharp
public class MyDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Normal { get; set; }
    public DataTemplate Accent { get; set; }

    protected override DataTemplate SelectTemplateCore(object item)
    {
        if ((int)item % 2 == 0)
        {
            return Normal;
        }
        else
        {
            return Accent;
        }
    }
}
```

The `MyDataTemplateSelector` class derives from the `DataTemplateSelector` class and first defines two [DataTemplate](/uwp/api/windows.ui.xaml.datatemplate) objects: `Normal` and `Accent`. These are empty declarations for now, but will be "filled in" with markup in the XAML file.

The `SelectTemplateCore` method takes in an item object (i.e. each collection item), and is overridden with rules on which `DataTemplate` to return under which circumstances. In this case, if the item is an odd number, it receives the `Accent` data template, while if it is an even number it receives the `Normal` data template.

### XAML component

Second, you must create an instance of this new `MyDataTemplateSelector` class in the Resources section of your XAML file. All resources require an `x:Key`, which you use to bind it to the `ItemTemplateSelector` property of your collection control (in a later step). You also create two instances of `DataTemplate` objects and define their layout in the resources section. You assign these data templates to the `Accent` and `Normal` properties you declared in the `MyDataTemplateSelector` class.

Here's an example of the XAML resources and markup needed:

```xaml
<Page.Resources>

<DataTemplate x:Key="NormalItemTemplate" x:DataType="x:Int32">
    <Button HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Background="{ThemeResource SystemChromeLowColor}">
        <TextBlock Text="{x:Bind}" />
    </Button>
</DataTemplate>

<DataTemplate x:Key="AccentItemTemplate" x:DataType="x:Int32">
    <Button HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Background="{ThemeResource SystemAccentColor}">
        <TextBlock Text="{x:Bind}" />
    </Button>
</DataTemplate>

<l:MyDataTemplateSelector x:Key="MyDataTemplateSelector"
    Normal="{StaticResource NormalItemTemplate}"
    Accent="{StaticResource AccentItemTemplate}"/>

</Page.Resources>
```

As you can see above, the two data templates `Normal` and `Accent` are defined - they both display items as buttons, however the `Accent` data template uses an accent color brush for the background, while the `Normal` data template uses a gray color brush (`SystemChromeLowColor`). These two data templates are then assigned to the `Normal` and `Accent` DataTemplate objects that are attributes of the MyDataTemplateSelector class, created in the C# code-behind.

The last step is to bind your `DataTemplateSelector` to the `ItemTemplateSelector` property of your collections control (in this case, a ListView). This replaces the need for assigning a value to the `ItemTemplate` property. 

```xaml
<ListView x:Name = "TestListView"
          ItemsSource = "{x:Bind NumbersList}"
          ItemTemplateSelector = "{StaticResource MyDataTemplateSelector}">
</ListView>
```

Once your code compiles, each collection item will run through the overridden `SelectTemplateCore` method in `MyDataTemplateSelector`, and will be rendered with the appropriate DataTemplate.

> [!IMPORTANT]
> When using `DataTemplateSelector` with an [ItemsRepeater](/uwp/api/microsoft.ui.xaml.controls.itemsrepeater?view=winui-2.2), you bind the `DataTemplateSelector` to the `ItemTemplate` property. `ItemsRepeater` doesn't have an `ItemTemplateSelector` property.

## DataTemplateSelector performance considerations

When you use a ListView or GridView with a large data collection, scrolling and panning performance can be a concern. To keep large collections performing well, there are some steps you can take to improve the performance of your data templates. These are described in more detail in [ListView and GridView UI optimization](../../debug-test-perf/optimize-gridview-and-listview.md).

- _Element reduction per item_ - Keep the number of UI elements in a data template to a reasonable minimum.
- Container-recycling with heterogeneous collections
  - Use _the ChoosingItemContainer event_  - This event is a high-performance way to use different data templates for different items. To achieve best performance, you should optimize caching and selecting data templates for your specific data.
  - Use an _item template selector_ - An item template selector (`DataTemplateSelector`) should be avoided in some instances due to its impact on performance.