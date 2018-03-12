---
author: mijacobs
Description: Good icons harmonize with typography and with the rest of the design language. They don’t mix metaphors, and they communicate only what’s needed, as speedily and simply as possible.
title: Icons
ms.assetid: b90ac02d-5467-4304-99bd-292d6272a014
label: Icons
template: detail.hbs
ms.author: mijacobs
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
design-contact: Judysa
doc-status: Published
ms.localizationpriority: medium
---

# Icons for UWP apps



Good icons harmonize with typography and with the rest of the design language. They don’t mix metaphors, and they communicate only what’s needed, as speedily and simply as possible. 

## Linear scaling size ramps 

<table>
    <tr> 
        <td>16px x 16px</td>
        <td>24px x 24px</td>
        <td>32px x 32px</td>
        <td>48px x 48px</td>
    </tr>
    <tr> 
        <td><img src="images/icons-16x16.png" alt="Icons at 16x16 effective pixels" /></td>
        <td><img src="images/icons-24x24.png" alt="Icons at 24x24 effective pixels" /></td>
        <td><img src="images/icons-32x32.png" alt="Icons at 32x32 effective pixels" /></td>
        <td><img src="images/icons-48x48.png" alt="Icons at 48x48 effective pixels" /></td>
    </tr>
</table>

## Common shapes

Icons should generally maximize their given space with little padding. These shapes provide starting points for sizing basic shapes. 

![32px by 32px grid](images/icons-common-shapes.png)

Use the shape that corresponds to the icon's orientation and compose around these basic parameters. Icons don't necessarily need to fill or fit completely inside the shape and may be adjusted as needed to ensure optimal balance. 

<table class="uwpd-noborder">
    <tr>
        <td>Circle<td>
        <td>Square</td>
        <td>Triangle</td>
    </tr>
    <tr>
        <td><img src="images/icons-common-shapes-examples-1.png" alt="A circle" /><td>
        <td><img src="images/icons-common-shapes-examples-2.png" alt="A square" /></td>
        <td><img src="images/icons-common-shapes-examples-3.png" alt="A triangle " /></td>
    </tr>
        <tr>
        <td>Horizontal rectangle<td>
        <td colspan="2">Vertical rectangle</td>        
        </tr>
    <tr>
        <td><img src="images/icons-common-shapes-examples-4.png" alt="A horizontal rectangle" /><td>
        <td colspan="2"><img src="images/icons-common-shapes-examples-5.png" alt="A vertical rectangle" /></td>
         
    </tr>

</table>

## Angles

In addition to using the same grid and line weight, icons are constructed with common elements. 

Using only these angles in building shapes creates consistency across all our icons, and ensures the icons render correctly. 

These lines can be combined, joined, rotated, and reflected in creating icons. 

<table>
    <tr>
        <td><b>1:1</b><br/>45°</td>
        <td><b>1:2</b><br />26.57° (vertical)<br/>63.43° (horizontal)</td>
        <td><b>1:3</b><br/>18.43° (vertical)<br/>71.57° (horizontal)</td>
        <td><b>1:4</b><br/>14.04° (vertical)<br/>75.96° (horizontal)</td>
    </tr>
    <tr>
        
        <td><img src="images/icons-grid-1-1.png" alt="1:1" /></td>
        <td><img src="images/icons-grid-1-2.png" alt="1:2" /></td>
        <td><img src="images/icons-grid-1-3.png" alt="1:3" /></td>
        <td><img src="images/icons-grid-1-4.png" alt="1:4" /></td>
    </tr>  
</table>

<p>Here are some examples:</p>

<table>
    <tr>
        <td><img src="images/icons-angles-examples-1.png" alt="A 1:1 angle example" /></td>
        <td><img src="images/icons-angles-examples-2.png" alt="A 1:2 angle example" /></td>
        <td><img src="images/icons-angles-examples-3.png" alt="A 1:3 angle example" /></td>
        <td><img src="images/icons-angles-examples-4.png" alt="A 1:4 angle example" /></td>
    </tr>
</table>

## Curves

Curved lines are constructed from sections of a whole circle and should not be skewed unless needed to snap to the pixel grid. 

<table>
    <tr>
        <td>1/4 circle</td>
        <td>1/8 circle</td>
    </tr>
    <tr>
        <td><img src="images/icons-curves-14circle.png" alt="1/4 circle" /></td>
        <td><img src="images/icons-curves-18circle.png" alt="1/8 circle" /></td>
    </tr>
    <tr>
        <td><img src="images/icons-curves-examples-1.png" alt="1/4 cirlce example" /></td>
        <td><img src="images/icons-curves-examples-2.png" alt="1/8 circle example" /></td>
    </tr>    
</table>

## Geometric construction

We recommend using only pure geometric shapes when constructing icons.

![Guitar icon with geometric overlay ](images/icons-geometric-construction.png)

## Filled shapes 

Icons can contain filled shapes when needed, but they should not be more than 4px at 32px × 32px. Filled circles should not be larger than 6px × 6px. 

![5px by 8px fill ](images/icons-filled-shapes.png)

## Badges

A "badge" is a generic term used to describe an element added to an icon that's not meant to be integrated with the base icon element. These usually convey other pieces of information about the icon like status or action. Other commons terms include: overlay, annotation, or modifier. 

![Status badge ](images/icons-badge-status.png)

![Action badge ](images/icons-badge-action.png)

Status badges utilize a filled, colored object that is on top of the icon, whereas action badges are integrated into the icon in the same monochrome style and line weight.

<table>
<tr>
	<td>Common status badges</td>
	<td>Common action badges</td>
</tr>
<tr>
	<td><img src="images/icons-badge-common-states-1.png" alt="Status badge " /></td>
	<td><img src="images/icons-badge-common-states-2.png" alt="Action badge " /></td>
</tr>
</table>
<p></p>

### Badge color 

Color badging should only be used to convey the state of an icon. The colors used in status badging convey specific emotional messages to the user. 

<table>
<tr><td>Green - #128B44</td><td>Blue - #2C71B9</td><td>Yellow - #FDC214</td></tr>
<tr><td>Positive: done, 
completed </td><td>Neutral: help, 
notification </td><td>Cautionary: alert, warning </td></tr>
<tr><td><img src="images/icons-color-inbadging-1.png" alt="Green status" /></td><td><img src="images/icons-color-inbadging-2.png" alt="Blue status" /></td>
<td><img src="images/icons-color-inbadging-3.png" alt="Yellow status" /></td></tr>
</table>
<p></p>

### Badge position

The default position for any status or action is the bottom right. Only use the other positions when the design will not allow it. 

### Badge sizing

Badges should be sized to 10–18 px on a 32 px × 32 px grid. 

## Related articles

* [Guidelines for tile and icon assets](../shell/tiles-and-notifications/app-assets.md)
