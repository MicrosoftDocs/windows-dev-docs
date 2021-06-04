---
description: The Expander control provides a standard interaction for showing more content in a container that pushes adjacent content while expanding and collapsing. 
title: Expander
template: detail.hbs
ms.date: 3/9/2021
ms.topic: article
keywords: windows 10, winui, uwp
pm-contact: kayang
design-contact: jknudsen
dev-contact: ranjeshj
ms.custom: 21H1
ms.localizationpriority: medium
---

# Expander
Expander provides a standard interaction for showing more content in a container that pushes adjacent content while expanding and collapsing. It is often used when some grouped content is only relevant some of the time (for example to show additional options that are all related to an overall item).

An Expander cannot be light dismissed and is independent of the contents inside it, including controls.
 
![A collapsed Expander that is expanded and then collapsed. The Header has the text "This is in the header" and the Content has the text "This is in the content".](images/expander-default.gif)

**Get the Windows UI Library**

:::row:::
   :::column:::
      ![WinUI logo](images/winui-logo-64x64.png)
   :::column-end:::
   :::column span="3":::
      The **Expander** control requires the Windows UI Library, a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](/uwp/toolkits/winui/).
   :::column-end:::
   :::column:::

   :::column-end:::
:::row-end:::
## Is this the right control?
Use an Expander when some primary content is always relevant and some related secondary content may be relevant some of the time. This UI is commonly used when display space is limited and when information and options can be grouped together. An Expander can expand upwards or downwards.

## Examples
<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/Expander">open the app and see Expander in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

### Create an Expander

The XAML below describes an Expander with the default styling. By default the Expander will be collapsed and expand downwards. Set the IsExpanded property to default to an expanded Expander. Set the ExpandDirection property to change the direction of expansion.

```XAML
<muxc:Expander x:Name="DefaultExpander" 
    Header="Header area"  
    Content="Content area"/>
```

![A collapsed Expander that is expanded and then collapsed. The Header has the text "This is in the header" and the Content has the text "This is in the content".](images/expander-default.gif)

### Put other controls inside

The content of the Expander can be set using the Header and Content properties. 

```XAML
<muxc:Expander x:Name="Expander2" Header="This is in the header"> 
    <muxc:Expander.Header>
        <ToggleButton>This ToggleButton is in the Header</ToggleButton>
    </muxc:Expander.Header>
    <muxc:Expander.Content>
        <ToggleButton>This ToggleButton is in the Content</ToggleButton>
    </muxc:Expander.Content>
</muxc:Expander>
```
![A collapsed Expander. The Header has a button with the text "This ToggleButton is in the Header" and the Content has a button with the text "This ToggleButton is in the Content". The button in the Header is toggled on and off, the Expander is expanded, the button in the Content is toggled on and off, and the Expander is collapsed.](images/expander-with-togglebuttons.gif)

### Lightweight styling

You can modify the default Style and ControlTemplate to give the control a unique appearance. See the Control Style and Template section of the Expander API docs for a list of the availble theme resources. For more info, see the [Light-weight styling section](https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/xaml-styles#lightweight-styling) of the [Styling controls](https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/xaml-styles) article. 

## Recommendations

* Common UI patterns where an Expander is used is when display space is limited some primary content is always relevant and some related secondary content may be relevant some of the time.