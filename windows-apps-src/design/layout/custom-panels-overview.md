---
author: muhsinking
Description: You can define custom panels for XAML layout by deriving a custom class from the Panel class.
MS-HAID: dev\_ctrl\_layout\_txt.xaml\_custom\_panels\_overview
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: XAML custom panels overview
ms.assetid: 0CD395CD-E2AB-429D-BB49-56A71C5CC35D
label: XAML custom panels overview (Windows apps)
template: detail.hbs
op-migration-status: ready
ms.author: mukin
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# XAML custom panels overview

 

A *panel* is an object that provides a layout behavior for child elements it contains, when the Extensible Application Markup Language (XAML) layout system runs and your app UI is rendered. 


> **Important APIs**: [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511), [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711), [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730)

You can define custom panels for XAML layout by deriving a custom class from the [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511) class. You provide behavior for your panel by overriding the [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) and [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711), supplying logic that measures and arranges the child elements.

## The **Panel** base class


To define a custom panel class, you can either derive from the [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511) class directly, or derive from one of the practical panel classes that aren't sealed, such as [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) or [**StackPanel**](https://msdn.microsoft.com/library/windows/apps/br209635). It's easier to derive from **Panel**, because it can be difficult to work around the existing layout logic of a panel that already has layout behavior. Also, a panel with behavior might have existing properties that aren't relevant for your panel's layout features.

From [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511), your custom panel inherits these APIs:

-   The [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) property.
-   The [**Background**](https://msdn.microsoft.com/library/windows/apps/br227512), [**ChildrenTransitions**](https://msdn.microsoft.com/library/windows/apps/br227515) and [**IsItemsHost**](https://msdn.microsoft.com/library/windows/apps/br227517) properties, and the dependency property identifiers. None of these properties are virtual, so you don't typically override or replace them. You don't typically need these properties for custom panel scenarios, not even for reading values.
-   The layout override methods [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) and [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711). These were originally defined by [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706). The base [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511) class doesn't override these, but practical panels like [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) do have override implementations that are implemented as native code and are run by the system. Providing new (or additive) implementations for **ArrangeOverride** and **MeasureOverride** is the bulk of the effort you need to define a custom panel.
-   All the other APIs of [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706), [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) and [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356), such as [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height), [**Visibility**](https://msdn.microsoft.com/library/windows/apps/br208992) and so on. You sometimes reference values of these properties in your layout overrides, but they aren't virtual so you don't typically override or replace them.

This focus here is to describe XAML layout concepts, so you can consider all the possibilities for how a custom panel can and should behave in layout. If you'd rather jump right in and see an example custom panel implementation, see [BoxPanel, an example custom panel](boxpanel-example-custom-panel.md).

## The **Children** property


The [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) property is relevant to a custom panel because all classes derived from [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511) use the **Children** property as the place to store their contained child elements in a collection. **Children** is designated as the XAML content property for the **Panel** class, and all classes derived from **Panel** can inherit the XAML content property behavior. If a property is designated the XAML content property, that means that XAML markup can omit a property element when specifying that property in markup, and the values are set as immediate markup children (the "content"). For example, if you derive a class named **CustomPanel** from **Panel** that defines no new behavior, you can still use this markup:

```XAML
<local:CustomPanel>
  <Button Name="button1"/>
  <Button Name="button2"/>
</local:CustomPanel>
```

When a XAML parser reads this markup, [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) is known to be the XAML content property for all [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511) derived types, so the parser will add the two [**Button**](https://msdn.microsoft.com/library/windows/apps/br209265) elements to the [**UIElementCollection**](https://msdn.microsoft.com/library/windows/apps/br227633) value of the **Children** property. The XAML content property facilitates a streamlined parent-child relationship in the XAML markup for a UI definition. For more info about XAML content properties, and how collection properties are populated when XAML is parsed, see the [XAML syntax guide](https://msdn.microsoft.com/library/windows/apps/mt185596).

The collection type that's maintaining the value of the [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) property is the [**UIElementCollection**](https://msdn.microsoft.com/library/windows/apps/br227633) class. **UIElementCollection** is a strongly typed collection that uses [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) as its enforced item type. **UIElement** is a base type that's inherited by hundreds of practical UI element types, so the type enforcement here is deliberately loose. But it does enforce that you couldn't have a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) as a direct child of a [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511), and it generally means that only elements that are expected to be visible in UI and participate in layout will be found as child elements in a **Panel**.

Typically, a custom panel accepts any [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) child element by a XAML definition, by simply using the characteristics of the [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) property as-is. As an advanced scenario, you could support further type checking of child elements, when you iterate over the collection in your layout overrides.

Besides looping through the [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) collection in the overrides, your panel logic might also be influenced by `Children.Count`. You might have logic that is allocating space at least partly based on the number of items, rather than desired sizes and the other characteristics of individual items.

## Overriding the layout methods


The basic model for the layout override methods ([**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) and [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711)) is that they should iterate through all the children and call each child element's specific layout method. The first layout cycle starts when the XAML layout system sets the visual for the root window. Because each parent invokes layout on its children, this propagates a call to layout methods to every possible UI element that is supposed to be part of a layout. In XAML layout, there are two stages: measure, then arrange.

You don't get any built-in layout method behavior for [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) and [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) from the base [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511) class. Items in [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) won't automatically render as part of the XAML visual tree. It is up to you to make the items known to the layout process, by invoking layout methods on each of the items you find in **Children** through a layout pass within your **MeasureOverride** and **ArrangeOverride** implementations.

There's no reason to call base implementations in layout overrides unless you have your own inheritance. The native methods for layout behavior (if they exist) run regardless, and not calling base implementation from overrides won't prevent the native behavior from happening.

During the measure pass, your layout logic queries each child element for its desired size, by calling the [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) method on that child element. Calling the **Measure** method establishes the value for the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) property. The [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) return value is the desired size for the panel itself.

During the arrange pass, the positions and sizes of child elements are determined in x-y space and the layout composition is prepared for rendering. Your code must call [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914) on each child element in [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) so that the layout system detects that the element belongs in the layout. The **Arrange** call is a precursor to composition and rendering; it informs the layout system where that element goes, when the composition is submitted for rendering.

Many properties and values contribute to how the layout logic will work at runtime. A way to think about the layout process is that the elements with no children (generally the most deeply nested element in the UI) are the ones that can finalize measurements first. They don't have any dependencies on child elements that influence their desired size. They might have their own desired sizes, and these are size suggestions until the layout actually takes place. Then, the measure pass continues walking up the visual tree until the root element has its measurements and all the measurements can be finalized.

The candidate layout must fit within the current app window or else parts of the UI will be clipped. Panels often are the place where the clipping logic is determined. Panel logic can determine what size is available from within the [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) implementation, and may have to push the size restrictions onto the children and divide space amongst children so that everything fits as best it can. The result of layout is ideally something that uses various properties of all parts of the layout but still fits within the app window. That requires both a good implementation for layout logic of the panels, and also a judicious UI design on the part of any app code that builds a UI using that panel. No panel design is going to look good if the overall UI design includes more child elements than can possibly fit in the app.

A large part of what makes the layout system work is that any element that's based on [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) already has some of its own inherent behavior when acting as a child in a container. For example, there are several APIs of **FrameworkElement** that either inform layout behavior or are needed to make layout work at all. These include:

-   [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) (actually a [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) property)
-   [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) and [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709)
-   [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width)
-   [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724)
-   [**LayoutUpdated**](https://msdn.microsoft.com/library/windows/apps/br208722) event
-   [**HorizontalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208720) and [**VerticalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208749)
-   [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) and [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) methods
-   [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914) and [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) methods: these have native implementations defined at the [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) level, which handle the element-level layout action

## **MeasureOverride**


The [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) method has a return value that's used by the layout system as the starting [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) for the panel itself, when the [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) method is called on the panel by its parent in layout. The logic choices within the method are just as important as what it returns, and the logic often influences what value is returned.

All [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) implementations should loop through [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514), and call the [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) method on each child element. Calling the **Measure** method establishes the value for the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) property. This might inform how much space the panel itself needs, as well as how that space is divided among elements or sized for a particular child element.

Here's a very basic skeleton of a [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) method:

```CSharp
protected override Size MeasureOverride(Size availableSize)
{
    Size returnSize; //TODO might return availableSize, might do something else
     
    //loop through each Child, call Measure on each
    foreach (UIElement child in Children)
    {
        child.Measure(new Size()); // TODO determine how much space the panel allots for this child, that's what you pass to Measure
        Size childDesiredSize = child.DesiredSize; //TODO determine how the returned Size is influenced by each child's DesiredSize
        //TODO, logic if passed-in Size and net DesiredSize are different, does that matter?
    }
    return returnSize;
}
```

Elements often have a natural size by the time they're ready for layout. After the measure pass, the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) might indicate that natural size, if the *availableSize* you passed for [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) was smaller. If the natural size is larger than *availableSize* you passed for **Measure**, the **DesiredSize** is constrained to *availableSize*. That's how **Measure**'s internal implementation behaves, and your layout overrides should take that behavior into account.

Some elements don't have a natural size because they have **Auto** values for [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width). These elements use the full *availableSize*, because that's what an **Auto** value represents: size the element to the maximum available size, which the immediate layout parent communicates by calling [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) with *availableSize*. In practice, there's always some measurement that a UI is sized to (even if that's the top level window.) Eventually, the measure pass resolves all the **Auto** values to parent constraints and all **Auto** value elements get real measurements (which you can get by checking [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) and [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707), after layout completes).

It's legal to pass a size to [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) that has at least one infinite dimension, to indicate that the panel can attempt to size itself to fit measurements of its content. Each child element being measured sets its [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) value using its natural size. Then, during the arrange pass, the panel typically arranges using that size.

Text elements such as [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) have a calculated [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) and [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) based on their text string and text properties even if no [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) or [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) value is set, and these dimensions should be respected by your panel logic. Clipping text is a particularly bad UI experience.

Even if your implementation doesn't use the desired size measurements, it's best to call the [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) method on each child element, because there are internal and native behaviors that are triggered by **Measure** being called. For an element to participate in layout, each child element must have **Measure** called on it during the measure pass and the [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914) method called on it during the arrange pass. Calling these methods sets internal flags on the object and populates values (such as the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) property) that the system's layout logic needs when it builds the visual tree and renders the UI.

The [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) return value is based on the panel's logic interpreting the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) or other size considerations for each of the child elements in [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514) when [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952) is called on them. What to do with **DesiredSize** values from children and how the **MeasureOverride** return value should use them is up to your own logic's interpretation. You don't typically add up the values without modification, because the input of **MeasureOverride** is often a fixed available size that's being suggested by the panel's parent. If you exceed that size, the panel itself might get clipped. You'd typically compare the total size of children to the panel's available size and make adjustments if necessary.

### Tips and guidance

-   Ideally, a custom panel should be suitable for being the first true visual in a UI composition, perhaps at a level immediately under [**Page**](https://msdn.microsoft.com/library/windows/apps/br227503), [**UserControl**](https://msdn.microsoft.com/library/windows/apps/br227647) or another element that is the XAML page root. In [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) implementations, don't routinely return the input [**Size**](https://msdn.microsoft.com/library/windows/apps/br225995) without examining the values. If the return **Size** has an **Infinity** value in it, this can throw exceptions in runtime layout logic. An **Infinity** value can come from the main app window, which is scrollable and therefore doesn't have a maximum height. Other scrollable content might have the same behavior.
-   Another common mistake in [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) implementations is to return a new default [**Size**](https://msdn.microsoft.com/library/windows/apps/br225995) (values for height and width are 0). You might start with that value, and it might even be the correct value if your panel determines that none of the children should be rendered. But, a default **Size** results in your panel not being sized correctly by its host. It requests no space in the UI, and therefore gets no space and doesn't render. All your panel code otherwise might be functioning fine, but you still won't see your panel or contents thereof if it's being composed with zero height, zero width.
-   Within the overrides, avoid the temptation to cast child elements to [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) and use properties that are calculated as a result of layout, particularly [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) and [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707). For most common scenarios, you can base the logic on the child's [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) value and you won't need any of the [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) or [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) related properties of a child element. For specialized cases, where you know the type of element and have additional information, for example the natural size of an image file, you can use your element's specialized information because it's not a value that is actively being altered by layout systems. Including layout-calculated properties as part of layout logic substantially increases the risk of defining an unintentional layout loop. These loops cause a condition where a valid layout can't be created and the system can throw a [**LayoutCycleException**](https://msdn.microsoft.com/library/windows/apps/hh673799) if the loop is not recoverable.
-   Panels typically divide their available space between multiple child elements, although exactly how space is divided varies. For example, [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) implements layout logic that uses its [**RowDefinition**](https://msdn.microsoft.com/library/windows/apps/br227606) and [**ColumnDefinition**](https://msdn.microsoft.com/library/windows/apps/br209324) values to divide the space into the **Grid** cells, supporting both star-sizing and pixel values. If they're pixel values, the size available for each child is already known, so that's what is passed as input size for a grid-style [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952).
-   Panels themselves can introduce reserved space for padding between items. If you do this, make sure to expose the measurements as a property that's distinct from [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724) or any **Padding** property.
-   Elements might have values for their [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) and [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) properties based on a previous layout pass. If values change, app UI code can put handlers for [**LayoutUpdated**](https://msdn.microsoft.com/library/windows/apps/br208722) on elements if there's special logic to run, but panel logic typically doesn't need to check for changes with event handling. The layout system is already making the determinations of when to re-run layout because a layout-relevant property changed value, and a panel's [**MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730) or [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) are called automatically in the appropriate circumstances.

## **ArrangeOverride**


The [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) method has a [**Size**](https://msdn.microsoft.com/library/windows/apps/br225995) return value that's used by the layout system when rendering the panel itself, when the [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914) method is called on the panel by its parent in layout. It's typical that the input *finalSize* and the **ArrangeOverride** returned **Size** are the same. If they aren't, that means the panel is attempting to make itself a different size than what the other participants in layout claim is available. The final size was based on having previously run the measure pass of layout through your panel code, so that's why returning a different size isn't typical: it means you are deliberately ignoring measure logic.

Don't return a [**Size**](https://msdn.microsoft.com/library/windows/apps/br225995) with an **Infinity** component. Trying to use such a **Size** throws an exception from internal layout.

All [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) implementations should loop through [**Children**](https://msdn.microsoft.com/library/windows/apps/br227514), and call the [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914) method on each child element. Like [**Measure**](https://msdn.microsoft.com/library/windows/apps/br208952), **Arrange** doesn't have a return value. Unlike **Measure**, no calculated property gets set as a result (however, the element in question typically fires a [**LayoutUpdated**](https://msdn.microsoft.com/library/windows/apps/br208722) event).

Here's a very basic skeleton of an [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) method:

```CSharp
protected override Size ArrangeOverride(Size finalSize)
{
    //loop through each Child, call Arrange on each
    foreach (UIElement child in Children)
    {
        Point anchorPoint = new Point(); //TODO more logic for topleft corner placement in your panel
       // for this child, and based on finalSize or other internal state of your panel
        child.Arrange(new Rect(anchorPoint, child.DesiredSize)); //OR, set a different Size 
    }
    return finalSize; //OR, return a different Size, but that's rare
}
```

The arrange pass of layout might happen without being preceded by a measure pass. However, this only happens when the layout system has determined no properties have changed that would have affected the previous measurements. For example, if an alignment changes, there's no need to re-measure that particular element because its [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) would not change when its alignment choice changes. On the other hand, if [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) changes on any element in a layout, a new measure pass is needed. The layout system automatically detects true measure changes and invokes the measure pass again, and then runs another arrange pass.

The input for [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914) takes a [**Rect**](https://msdn.microsoft.com/library/windows/apps/br225994) value. The most common way to construct this **Rect** is to use the constructor that has a [**Point**](https://msdn.microsoft.com/library/windows/apps/br225870) input and a [**Size**](https://msdn.microsoft.com/library/windows/apps/br225995) input. The **Point** is the point where the top left corner of the bounding box for the element should be placed. The **Size** is the dimensions used to render that particular element. You often use the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) for that element as this **Size** value, because establishing the **DesiredSize** for all elements involved in layout was the purpose of the measure pass of layout. (The measure pass determines all-up sizing of the elements in an iterative way so that the layout system can optimize how elements are placed once it gets to the arrange pass.)

What typically varies between [**ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711) implementations is the logic by which the panel determines the [**Point**](https://msdn.microsoft.com/library/windows/apps/br225870) component of how it arranges each child. An absolute positioning panel such as [**Canvas**](https://msdn.microsoft.com/library/windows/apps/br209267) uses the explicit placement info that it gets from each element through [**Canvas.Left**](https://msdn.microsoft.com/library/windows/apps/hh759771) and [**Canvas.Top**](https://msdn.microsoft.com/library/windows/apps/hh759772) values. A space-dividing panel such as [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) would have mathematical operations that divided the available space into cells and each cell would have an x-y value for where its content should be placed and arranged. An adaptive panel such as [**StackPanel**](https://msdn.microsoft.com/library/windows/apps/br209635) might be expanding itself to fit content in its orientation dimension.

There are still additional positioning influences on elements in layout, beyond what you directly control and pass to [**Arrange**](https://msdn.microsoft.com/library/windows/apps/br208914). These come from the internal native implementation of **Arrange** that's common to all [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) derived types and augmented by some other types such as text elements. For example, elements can have margin and alignment, and some can have padding. These properties often interact. For more info, see [Alignment, margin, and padding](alignment-margin-padding.md).

## Panels and controls


Avoid putting functionality into a custom panel that should instead be built as a custom control. The role of a panel is to present any child element content that exists within it, as a function of layout that happens automatically. The panel might add decorations to content (similar to how a [**Border**](https://msdn.microsoft.com/library/windows/apps/br209250) adds the border around the element it presents), or perform other layout-related adjustments like padding. But that's about as far as you should go when extending the visual tree output beyond reporting and using information from the children.

If there's any interaction that's accessible to the user, you should write a custom control, not a panel. For example, a panel shouldn't add scrolling viewports to content it presents, even if the goal is to prevent clipping, because the scrollbars, thumbs and so on are interactive control parts. (Content might have scrollbars after all, but you should leave that up to the child's logic. Don't force it by adding scrolling as a layout operation.) You might create a control and also write a custom panel that plays an important role in that control's visual tree, when it comes to presenting content in that control. But the control and the panel should be distinct code objects.

One reason the distinction between control and panel is important is because of Microsoft UI Automation and accessibility. Panels provide a visual layout behavior, not a logical behavior. How a UI element appears visually is not an aspect of UI that is typically important to accessibility scenarios. Accessibility is about exposing the parts of an app that are logically important to understanding a UI. When interaction is required, controls should expose the interaction possibilities to the UI Automation infrastructure. For more info, see [Custom automation peers](https://msdn.microsoft.com/library/windows/apps/mt297667).

## Other layout API


There are some other APIs that are part of the layout system, but aren't declared by [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511). You might use these in a panel implementation or in a custom control that uses panels.

-   [**UpdateLayout**](https://msdn.microsoft.com/library/windows/apps/br208989), [**InvalidateMeasure**](https://msdn.microsoft.com/library/windows/apps/br208930), and [**InvalidateArrange**](https://msdn.microsoft.com/library/windows/apps/br208929) are methods that initiate a layout pass. **InvalidateArrange** might not trigger a measure pass, but the other two do. Never call these methods from within a layout method override, because they're almost sure to cause a layout loop. Control code doesn't typically need to call them either. Most aspects of layout are triggered automatically by detecting changes to the framework-defined layout properties such as [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) and so on.
-   [**LayoutUpdated**](https://msdn.microsoft.com/library/windows/apps/br208722) is an event that fires when some aspect of layout of the element has changed. This isn't specific to panels; the event is defined by [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706).
-   [**SizeChanged**](https://msdn.microsoft.com/library/windows/apps/br208742) is an event that fires only after layout passes are finalized, and indicates that [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) or [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) have changed as a result. This is another [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) event. There are cases where [**LayoutUpdated**](https://msdn.microsoft.com/library/windows/apps/br208722) fires, but **SizeChanged** does not. For example the internal contents might be rearranged, but the element's size didn't change.


## Related topics

**Reference**
* [**FrameworkElement.ArrangeOverride**](https://msdn.microsoft.com/library/windows/apps/br208711)
* [**FrameworkElement.MeasureOverride**](https://msdn.microsoft.com/library/windows/apps/br208730)
* [**Panel**](https://msdn.microsoft.com/library/windows/apps/br227511)

**Concepts**
* [Alignment, margin, and padding](alignment-margin-padding.md)
