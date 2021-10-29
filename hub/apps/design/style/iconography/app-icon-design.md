---
title: Design guidelines for Windows app icons
description: How to create app icons/logos that represent your app in the Start menu, app tiles, the taskbar, the Microsoft Store, and more. 
keywords: windows 10, uwp
author: hickeys
ms.author: hickeys
design-contact: judysa
ms.date: 10/29/2021
ms.topic: article
ms.localizationpriority: medium
---

# Design guidelines for Windows app icons

## Design guidance: Metaphor

:::image type="content" source="images/icon-progressive-abstraction.png" alt-text="Several variations on a record player icon, each progressively more abstract.":::

An icon should be a metaphor for its app: a visual representation of the value proposition, functions, and features of the product.

### Representation

Your icon should illustrate the concept of your app in a singular element using simple forms.

When creating your icon, use clear metaphors and leverage concepts that are largely understood—such as an envelope for mail or magnifying glass for search. The key concept should be the focal point in the icon and not diluted by decorative elements that don’t support the metaphor. To enhance communication clarity, icons should only include one metaphor.

:::image type="content" source="images/abstraction-spectrum.png" alt-text="Examples of less and more abstract icons.":::

Literal metaphors are best for articulating the purpose and promise in a clear way. A good test for an effective icon is when users can tell what it represents without a label.

Only use an abstract metaphor in instances where it's impossible to find a literal, self-evident metaphor to represent the core functionality of a product.

Icons should not include typography as part of the design. Letters and words on your icon should be avoided and only used when it’s essential. The app’s name will appear in association with the icon throughout the operating system.

## Design guidance: Shape

### The grid

:::image type="content" source="images/icon-design-grid.png" alt-text="The grid template used for icon design and alignment.":::

Microsoft aligns its icons to a 48x48 grid to ensure a balanced icon that takes advantage of the space available to it while still maintaining a distinctive shape and silhouette. Whenever possible, align your icon's distinctive features to the grid.

### Rounded corners

:::image type="content" source="images/grid-rounded-corners" alt-text="An image calling out the various rounded corner templates in the grid.":::

**Approachable** is a Microsoft personality principle. We use soft or rounded corners to communicate this trait.

The shapes used to create product icons should be built to align with the icon grid. The corners of these shapes should match the rounded corners in the icon grid.

When rounded corners are applied to an exterior curve, use a 2px radius. When rounded corners are applied to an interior curve, use a 1px radius.

### Silhouette

:::image type="content" source="images/icons-aligned-in-grid.png" alt-text="Several icons aligned within the grid template.":::

A visually balanced silhouette allows good icon scalability and also avoids extremes of thick and thin shapes

Use the grid to design a silhouette that’s distinctive, yet legible at small sizes. Use as few shapes with as few corners as possible to distinguish your product while still feeling at home on Windows.

### Detail

When adding detail, care should be taken to maintain legibility at small sizes. It is recommended to only add additional literal detail to the most prominent layer of an icon.

## Design guidance: Color

:::image type="content" source="images/color-spectrum.png" alt-text="A color picker style image showing all available colors and shades.":::

Pick colors carefully and avoid relying on color alone to convey meaning. Use shape and metaphor with color to communicate.

To avoid complexity when scaling an icon across a range of sizes, treatments to colors should be minimized. Color gradients, overlays of varying opacity, and tints of color should be kept to a minimum.

### The Microsoft color palette

:::image type="content" source="images/create-monochrome-palette" alt-text="An image outlining the steps to create a monochrome color palette":::

> [!WARNING]
> I totally made this part up. Also we need a better image for this section.

Microsoft uses a standard color palette to represent its brand. Using colors from this palette signals a strong association with Windows. This may be appropriate for a system app such as a terminal replacement or disk management software. Other apps, such as games or design tools, may wish to distinguish themselves by avoiding these colors to make their icons stand out.

### Monochrome palette

:::image type="content" source="images/create-monochrome-palette" alt-text="An image outlining the steps to create a monochrome color palette":::

Create a monochrome palette using the following steps:

1. Create three colors from the same hue. In most cases you will have to adjust the light color to be brighter and the dark color to be less saturated, but of course you should use your best judgement.
2. Create three steps in between each base color. This will be your primary lane. Most of the icon should be comprised of these colors.
3. For a wider palette, create tints to white and shades to black using the same method as step 2. These tints and shades should be used only when you need a little more contrast.
4. The tints of the dark colors and shades of the light colors are usually useless and drab. They can be removed.

### Analogous palette

:::image type="content" source="images/create-analogous-palette." alt-text="An image outlining the steps to create an analogous color palette":::

Creating an analogous palette is exactly like creating a monochrome palette, but with more colors. The key to this type of palette is not to overdo it. Be thoughtful with your color transitions.

1. Create three color sets instead of one.
2. Make vertical ramps out of all three color sets.
3. Instead of creating tints and shades using white and black, use your second and third colors instead.

### Gradients

:::image type="content" source="images/icons-with-gradients.png" alt-text="Several icons that use gradients.":::

Gradients should be subtle for the most part. Rule of thumb is one or two steps in the vertical or horizontal ramps. (see step illustrations below) Of course there are always exceptions. Again it’s about being thoughtful with how you apply it.

:::image type="content" source="images/gradient-angle" alt-text="{alt-text}":::

The default angle for gradients is 120 degrees. Start and end points can be adjusted accordingly. The important thing is that it’s a smooth transition. Avoid very tight transitions that would feel like reflections or dimension.

:::image type="content" source="images/monochrome-gradients.png" alt-text="An image showing transitions in monochrome gradients":::

The monochrome gradients are usually used to give a subtle hint toward an ambient light angle coming from the top left. They should not be treated as a direct light source though. The idea is to give the shapes a little movement without being too dramatic.

:::image type="content" source="images/analogous-gradients.png" alt-text="An image showing transitions in analogous gradients":::

The analogous gradients should be at the same angle as the monochrome, but don't always have to be. Typically lighter hues should be on top left to avoid looking overly dramatic but also to be as consistent as possible with the monochrome.

## Design guidance: Contrast, shadow, and perspective

### Color contrast

:::row:::
    :::column:::
        :::image type="content" source="images/contrast-light.png" alt-text="{alt-text}":::
    :::column-end:::
    :::column:::
        :::image type="content" source="images/contrast-dark.png" alt-text="{alt-text}":::
    :::column-end:::
:::row-end:::

Accessibility is a high priority for Microsoft. Because people have the choice of choosing an accent color, it’s difficult to make an icon 100% accessible on every background. There are several things you can do to ensure your icon is as accessible as possible.

> [!WARNING]
> ??? Broken image

- Use color values in all 3 ranges, dark, medium, light.
- Make sure at least half of your icon passes a 3.0:1 contrast ratio on light and dark theme. Enough to make out the shape.
- Some hue values are more difficult than others. Yellow will never pass on light theme until it’s brown. Reds are more difficult on dark theme.
- There is always the option to have a separate light and dark theme asset.

### High contrast

:::row:::
    :::column:::
        :::image type="content" source="images/high-contrast-light.png" alt-text="{alt-text}":::
    :::column-end:::
    :::column:::
        :::image type="content" source="images/high-contrast-dark.png" alt-text="{alt-text}":::
    :::column-end:::
:::row-end:::


High contrast icons are black and white and should be a direct representation of your app icon. Often the high contrast icon can be created from the color version using a solid fill and line. Avoid gradients in high contrast icons.

### Layering and shadow

:::image type="content" source="images/icon-layers-angle.png" alt-text="{alt-text}":::

Icons are composed of layers. Each layer is a flat object sitting on top of the layers below it. Use as few layers as possible, and minimize extreme contrasts of scale between shapes

:::image type="content" source="images/icon-layers-top.png" alt-text="{alt-text}":::

Drop shadows can be used within icons to help create definition between object layers and connect visually with the rest of the icon design system. In general, shadows cast from light onto dark shapes have the best result.

Inner shadows should only cast a shadow on the graphic symbol, not on to the surrounding background.

There are two types of inner shadow both of which have two shadows each.

### Separate metaphor

:::image type="content" source="images/separate-metaphor.png" alt-text="{alt-text}":::

This shadow is used when you have two objects that overlap each other but are not necessarily part of the same metaphor. The shadow should be masked into the shape below it.

### Same metaphor

:::image type="content" source="images/same-metaphor" alt-text="{alt-text}":::

This shadow is used when you have content within a single metaphor that needs some depth. It’s not always necessary to do this, but single object metaphors need some depth to feel like part of the system. the blue on shadow 2 is the only difference.

### Perspective

> [!WARNING]
> ??? Broken images

> [!WARNING]
> I made up everything except the very first and very last sentences in this section.

Shapes can be drawn with either a straight-on or isometric perspective. Apps that primarily present information may want a straisght-on perspective to show that they're straightforward and easy to understand. Isometric icons may fit better for a creative or complex apps, where the icons should communicate dynamism and depth.

Regardless, layers should always be flat and perpendicular to the viewing angle.
