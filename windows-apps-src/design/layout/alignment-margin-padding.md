---
author: muhsinking
Description: Use alignment, margin, and padding to influence the layout of elements on a page.
title: Alignment, margin, and padding for Universal Windows Platform (UWP) apps
ms.assetid: 9412ABD4-3674-4865-B07D-64C7C26E4842
label: Alignment, margin, and padding
template: detail.hbs
op-migration-status: ready
ms.author: mukin
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Alignment, margin, and padding

In addition to dimension properties (width, height, and constraints), elements can also have alignment, margin, and padding properties that influence the layout behavior when an element goes through a layout pass and is rendered in a UI. There are relationships between alignment, margin, padding and dimension properties that have a typical logic flow when a [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) object is positioned, such that values are sometimes used and sometimes ignored depending on the circumstances.

![xaml margins and padding diagram](images/xaml-layout-margins-padding.png)

## Alignment properties

The [**HorizontalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208720) and [**VerticalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208749) properties describe how a child element should be positioned within a parent element's allocated layout space. By using these properties together, layout logic for a container can position child elements within the container (either a panel or a control). Alignment properties are intended to hint the desired layout to an adaptive layout container, so basically they're set on [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) children and interpreted by another **FrameworkElement** container parent. Alignment values can specify whether elements align to one of the two edges of an orientation, or to the center. However, the default value for both alignment properties is **Stretch**. With **Stretch** alignment, elements fill the space they're provided in layout. **Stretch** is the default so that it's easier to use adaptive layout techniques in the cases where there is no explicit measurement or no [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) value that came from the measure pass of layout. With this default, there's no risk of an explicit height/width not fitting within the container and being clipped until you size each container.

> [!NOTE]
> As a general layout principle, it's best to only apply measurements to certain key elements and use the adaptive layout behavior for the other elements. This provides flexible layout behavior for when the user sizes the top app window, which typically is possible to do at any time.

 
If there are either [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Width) values or clipping within an adaptive container, even if **Stretch** is set as an alignment value, the layout is controlled by the behavior of its container. In panels, a **Stretch** value that's been obviated by **Height** and **Width** acts as if the value is **Center**.

If there are no natural or calculated height and width values, these dimension values are mathematically **NaN** (Not A Number). The elements are waiting for their layout container to give them dimensions. After layout is run, there will be values for [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) and [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) properties for elements where a **Stretch** alignment was used. The **NaN** values remain in [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Width) for the child elements so that the adaptive behavior can run again, for example, if layout-related changes such as app window sizing causes another layout cycle.

Text elements such as [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) don't usually have an explicitly declared width, but they do have a calculated width that you can query with [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709), and that width also cancels out a **Stretch** alignment. (The [**FontSize**](https://msdn.microsoft.com/library/windows/apps/br209657) property and other text properties, as well as the text itself, are already hinting the intended layout size. You don't typically want your text to be stretched.) Text used as content within a control has the same effect; the presence of text that needs presenting causes an **ActualWidth** to be calculated, and this also commutes a desired width and size to the containing control. Text elements also have an [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) based on font size per line, line breaks, and other text properties.

A panel such as [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) already has other logic for layout (row and column definitions, and attached properties such as [**Grid.Row**](https://msdn.microsoft.com/library/windows/apps/hh759795) set on elements to indicate which cell to be drawn in). In that case, the alignment properties influence how the content is aligned within the area of that cell, but the cell structure and sizing is controlled by settings on the **Grid**.

Item controls sometimes display items where the base types of the items are data. This involves an [**ItemsPresenter**](https://msdn.microsoft.com/library/windows/apps/br242843). Although the data itself is not a [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) derived type, **ItemsPresenter** is, so you can set [**HorizontalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208720) and [**VerticalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208749) for the presenter and that alignment applies to the data items when presented in the items control.

Alignment properties are only relevant for cases when there's extra space available in a dimension of the parent layout container. If a layout container is already clipping content, alignment can affect the area of the element where the clipping will apply. For example, if you set `HorizontalAlignment="Left"`, the right size of the element gets clipped.

## Margin

The [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724) property describes the distance between an element and its peers in a layout situation, and also the distance between an element and the content area of a container that contains the element. If you think of elements as bounding boxes or rectangles where the dimensions are the [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) and [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709), the **Margin** layout applies to the outside of that rectangle and does not add pixels to the **ActualHeight** and **ActualWidth**. The margin is also not considered part of the element for purposes of hit testing and sourcing input events.

In general layout behavior, components of a [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724) value are constrained last, and are constrained only after [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Width) are already constrained all the way to 0. So, be careful with margins when the container is already clipping or constraining the element; otherwise, your margin could be the cause of an element not appearing to render (because one of its dimensions has been constrained to 0 after the margin was applied).

Margin values can be uniform, by using syntax like `Margin="20"`. With this syntax, a uniform margin of 20 pixels would be applied to the element, with a 20-pixel margin on the left, top, right, and bottom sides. Margin values can also take the form of four distinct values, each value describing a distinct margin to apply to the left, top, right, and bottom (in that order). For example, `Margin="0,10,5,25"`. The underlying type for the [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724) property is a [**Thickness**](https://msdn.microsoft.com/library/windows/apps/br208864) structure, which has properties that hold the [**Left**](https://msdn.microsoft.com/library/windows/apps/hh673893), [**Top**](https://msdn.microsoft.com/library/windows/apps/hh673840), [**Right**](https://msdn.microsoft.com/library/windows/apps/hh673881), and [**Bottom**](https://msdn.microsoft.com/library/windows/apps/hh673775) values as separate **Double** values.

Margins are additive. For example, if two elements each specify a uniform margin of 10 pixels and they are adjacent peers in any orientation, the distance between the elements is 20 pixels.

Negative margins are permitted. However, using a negative margin can often cause clipping, or overdraws of peers, so it's not a common technique to use negative margins.

Proper use of the [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724) property enables very fine control of an element's rendering position and the rendering position of its neighbor elements and children. When you use element dragging to position elements within the XAML designer in Visual Studio, you'll see that the modified XAML typically has values for **Margin** of that element that were used to serialize your positioning changes back into the XAML.

The [**Block**](https://msdn.microsoft.com/library/windows/apps/br244379) class, which is a base class for [**Paragraph**](https://msdn.microsoft.com/library/windows/apps/br244503), also has a [**Margin**](https://msdn.microsoft.com/library/windows/apps/jj191725) property. It has an analogous effect on how that **Paragraph** is positioned within its parent container, which is typically a [**RichTextBlock**](https://msdn.microsoft.com/library/windows/apps/br227565) or [**RichEditBox**](https://msdn.microsoft.com/library/windows/apps/br227548) object, and also how more than one paragraph is positioned relative to other **Block** peers from the [**RichTextBlock.Blocks**](https://msdn.microsoft.com/library/windows/apps/br244347) collection.

## Padding

A **Padding** property describes the distance between an element and any child elements or content that it contains. Content is treated as a single bounding box that encloses all the content, if it's an element that permits more than one child. For example, if there's an [**ItemsControl**](https://msdn.microsoft.com/library/windows/apps/br242803) that contains two items, the [**Padding**](https://msdn.microsoft.com/library/windows/apps/br209459) is applied around the bounding box that contains the items. **Padding** subtracts from the available size when it comes to the measure and arrange pass calculations for that container and are part of the desired size values when the container itself goes through the layout pass for whatever contains it. Unlike [**Margin**](https://msdn.microsoft.com/library/windows/apps/br208724), **Padding** is not a property of [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706), and in fact there are several classes which each define their own **Padding** property:

-   [**Control.Padding**](https://msdn.microsoft.com/library/windows/apps/br209459): inherits to all [**Control**](https://msdn.microsoft.com/library/windows/apps/br209390) derived classes. Not all controls have content, so for some controls (for example [**AppBarSeparator**](https://msdn.microsoft.com/library/windows/apps/dn279268)) setting the property does nothing. If the control has a border (see [**Control.BorderThickness**](https://msdn.microsoft.com/library/windows/apps/br209399)), the padding applies inside that border.
-   [**Border.Padding**](https://msdn.microsoft.com/library/windows/apps/br209263): defines space between the rectangle line created by [**BorderThickness**](https://msdn.microsoft.com/library/windows/apps/br209256)/[**BorderBrush**](https://msdn.microsoft.com/library/windows/apps/br209254) and the [**Child**](https://msdn.microsoft.com/library/windows/apps/br209258) element.
-   [**ItemsPresenter.Padding**](https://msdn.microsoft.com/library/windows/apps/hh968021): contributes to appearance of the generated visuals for items in item controls, placing the specified padding around each item.
-   [**TextBlock.Padding**](https://msdn.microsoft.com/library/windows/apps/br209673) and [**RichTextBlock.Padding**](https://msdn.microsoft.com/library/windows/apps/br227596): expands the bounding box around the text of the text element. These text elements don't have a **Background**, so it can be visually difficult to see what's the text's padding versus other layout behavior applied by the text element's container. For that reason, text element padding is seldom used and it's more typical to use [**Margin**](https://msdn.microsoft.com/library/windows/apps/jj191725) settings on contained [**Block**](https://msdn.microsoft.com/library/windows/apps/br244379) containers (for the [**RichTextBlock**](https://msdn.microsoft.com/library/windows/apps/br227565) case).

In each of these cases, the same element also has a **Margin** property. If both margin and padding are applied, they are additive in the sense that the apparent distance between an outer container and any inner content will be margin plus padding. If there are different background values applied to content, element or container, the point at which margin ends and padding begins is potentially visible in the rendering.

## Dimensions (Height, Width)

The [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Width) properties of a [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/br208706) often influence how the alignment, margin, and padding properties behave when a layout pass happens. In particular, real-number **Height** and **Width** value cancels **Stretch** alignments, and is also promoted as a possible component of the [**DesiredSize**](https://msdn.microsoft.com/library/windows/apps/br208921) value that's established during the measure pass of the layout. **Height** and **Width** have constraint properties: the **Height** value can be constrained with [**MinHeight**](https://msdn.microsoft.com/library/windows/apps/br208731) and [**MaxHeight**](https://msdn.microsoft.com/library/windows/apps/br208726), the **Width** value can be constrained with [**MinWidth**](https://msdn.microsoft.com/library/windows/apps/br208733) and [**MaxWidth**](https://msdn.microsoft.com/library/windows/apps/br208728). Also, [**ActualWidth**](https://msdn.microsoft.com/library/windows/apps/br208709) and [**ActualHeight**](https://msdn.microsoft.com/library/windows/apps/br208707) are calculated, read-only properties that only contain valid values after a layout pass has completed. For more info about how the dimensions and constraints or calculated properties interrelate, see Remarks in [**FrameworkElement.Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Height) and [**FrameworkElement.Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Width).

## Related topics

**Reference**

* [**FrameworkElement.Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Height)
* [**FrameworkElement.Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement#Windows_UI_Xaml_FrameworkElement_Width)
* [**FrameworkElement.HorizontalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208720)
* [**FrameworkElement.VerticalAlignment**](https://msdn.microsoft.com/library/windows/apps/br208749)
* [**FrameworkElement.Margin**](https://msdn.microsoft.com/library/windows/apps/br208724)
* [**Control.Padding**](https://msdn.microsoft.com/library/windows/apps/br209459)
