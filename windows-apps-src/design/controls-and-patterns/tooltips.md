---
author: Jwmsft
Description: Use a tooltip to reveal more info about a control before asking the user to perform an action.
title: Tooltips
ms.assetid: A21BB12B-301E-40C9-B84B-C055FD43D307
label: Tooltips
template: detail.hbs
ms.author: jimwalk
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: yulikl
design-contact: kimsea
dev-contact: stpete
doc-status: Published
ms.localizationpriority: medium
---
# Tooltips

A tooltip is a short description that is linked to another control or object. Tooltips help users understand unfamiliar objects that aren't described directly in the UI. They display automatically when the user moves focus to, presses and holds, or hovers the mouse pointer over a control. The tooltip disappears after a few seconds, or when the user moves the finger, pointer or keyboard/gamepad focus.

![A tooltip](images/controls/tool-tip.png)

> **Important APIs**: [ToolTip class](/uwp/api/Windows.UI.Xaml.Controls.ToolTip), [ToolTipService class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.tooltipservice)

## Is this the right control?

Use a tooltip to reveal more info about a control before asking the user to perform an action. Tooltips should be used sparingly, and only when they are adding distinct value for the user who is trying to complete a task. One rule of thumb is that if the information is available elsewhere in the same experience, you do not need a tooltip. A valuable tooltip will clarify an unclear action.

When should you use a tooltip? To decide, consider these questions:

- **Should info become visible based on pointer hover?**
    If not, use another control. Display tips only as the result of user interaction, never display them on their own.

- **Does a control have a text label?**
    If not, use a tooltip to provide the label. It is a good UX design practice to label most controls inline and for these you don't need tooltips. Toolbar controls and command buttons showing only icons need tooltips.

- **Does an object benefit from a description or further info?**
    If so, use a tooltip. But the text must be supplemental — that is, not essential to the primary tasks. If it is essential, put it directly in the UI so that users don't have to discover or hunt for it.

- **Is the supplemental info an error, warning, or status?**
    If so, use another UI element, such as a flyout.

- **Do users need to interact with the tip?**
    If so, use another control. Users can't interact with tips because moving the mouse makes them disappear.

- **Do users need to print the supplemental info?**
    If so, use another control.

- **Will users find the tips annoying or distracting?**
    If so, consider using another solution — including doing nothing at all. If you do use tips where they might be distracting, allow users to turn them off.

## Example

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/ToolTip">open the app and see the ToolTip in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

A tooltip in the Bing Maps app.

![A tooltip in the Bing Maps app](images/control-examples/tool-tip-maps.png)

## Create a tooltip

A [ToolTip](/uwp/api/Windows.UI.Xaml.Controls.ToolTip) must be assigned to another UI element that is its owner. The [ToolTipService](/uwp/api/windows.ui.xaml.controls.tooltipservice) class provides static methods to display a ToolTip.

In XAML, use the **ToolTipService.Tooltip** attached property to assign the ToolTip to an owner.

```xaml
<Button Content="Submit" ToolTipService.ToolTip="Click to submit"/>
```

In code, use the [ToolTipService.SetToolTip](/uwp/api/windows.ui.xaml.controls.tooltipservice.settooltip) method to assign the ToolTip to an owner.

```xaml
<Button x:Name="submitButton" Content="Submit"/>
```

```csharp
ToolTip toolTip = new ToolTip();
toolTip.Content = "Click to submit";
ToolTipService.SetToolTip(submitButton, toolTip);
```

### Content

You can use any object as the [Content](/uwp/api/windows.ui.xaml.controls.contentcontrol.content) of a ToolTip. Here's an example of using an [Image](/uwp/api/windows.ui.xaml.controls.image) in a ToolTip.

```xaml
<TextBlock Text="store logo">
    <ToolTipService.ToolTip>
        <Image Source="Assets/StoreLogo.png"/>
    </ToolTipService.ToolTip>
</TextBlock>
```

### Placement

By default, a ToolTip is displayed centered above the pointer. The placement is not constrained by the app window, so the ToolTip might be displayed partially or completely outside of the app window bounds.

If a ToolTip obscures the content it is referring to, you can adjust its placement. Use the [Placement](/uwp/api/windows.ui.xaml.controls.tooltip.placement) property or **ToolTipService.Placement** attached property to place the ToolTip above, below, left, or right of the pointer. You can set the [VerticalOffset](/uwp/api/windows.ui.xaml.controls.tooltip.verticaloffset) and [HorizontalOffset](/uwp/api/windows.ui.xaml.controls.tooltip.horizontaloffset) properties to change the distance between the pointer and the ToolTip.

```xaml
<!-- A TextBlock with an offset ToolTip. -->
<TextBlock Text="TextBlock with an offset ToolTip.">
    <ToolTipService.ToolTip>
        <ToolTip Content="Offset ToolTip."
                 HorizontalOffset="20" VerticalOffset="30"/>
    </ToolTipService.ToolTip>
</TextBlock>
```

## Recommendations

- Use tooltips sparingly (or not at all). Tooltips are an interruption. A tooltip can be as distracting as a pop-up, so don't use them unless they add significant value.
- Keep the tooltip text concise. Tooltips are perfect for short sentences and sentence fragments. Large blocks of text can be overwhelming and the tooltip may time out before the user has finished reading.
- Create helpful, supplemental tooltip text. Tooltip text must be informative. Don't make it obvious or just repeat what is already on the screen. Because tooltip text isn't always visible, it should be supplemental info that users don't have to read. Communicate important info using self-explanatory control labels or in-place supplemental text.
- Use images when appropriate. Sometimes it's better to use an image in a tooltip. For example, when the user hovers over a hyperlink, you can use a tooltip to show a preview of the linked page.
- Don't use a tooltip to display text already visible in the UI. For example, don't put a tooltip on a button that shows the same text of the button.
- Don't put interactive controls inside the tooltip.
- Don't put images that look like they are interactive inside the tooltip.

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics) - See all the XAML controls in an interactive format.

## Related articles

- [ToolTip class](https://msdn.microsoft.com/library/windows/apps/br227608)
