---
description: An overview of common page patterns and UI elements for displaying content in your Windows app.
title: Content design basics for Windows apps
ms.assetid: 3102530A-E0D1-4C55-AEFF-99443D39D567
label: Content design basics
template: detail.hbs
op-migration-status: ready
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Content design basics for Windows apps

This article provides some practical tips and examples to help you design the content of your app: Windows spacing rationale, using the type ramp to demonstrate hierarchy, lists and grids, and how to group controls.

## Spacing and gutters

The use of consistently sized spacing and gutters semantically groups an experience into separate components. These values map to our rounded corner logic and together help create a cohesive and usable layout.

:::row:::
    :::column:::
        :::image type="content" source="images/geometry_spacing_buttons_626.png" alt-text="Two buttons separated by 8 pixels.":::
    :::column-end:::
    :::column span="1":::
        **8epx** between buttons
    :::column-end:::
:::row-end:::

:::row:::
    :::column span="1":::
        :::image type="content" source="images/geometry_spacing_flyout.svg" alt-text="A button and a flyout separated by 8 pixels.":::
    :::column-end:::
    :::column span="1":::
        **8epx** between buttons and flyouts
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/geometry_spacing_header.svg" alt-text="A control and a header separated by 8 pixels.":::
    :::column-end:::
    :::column span="1":::
        **8epx** between control and header
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/geometry_spacing_label.svg" alt-text="A Control and a label separated by 12 pixels":::
    :::column-end:::
    :::column span="1":::
        **12epx** between control and label
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/geometry_spacing_cards.svg" alt-text="Two content areas separated by 12 pixels.":::
    :::column-end:::
    :::column span="1":::
        **12epx** between content areas
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/geometry_spacing_margins.svg" alt-text="A surface containing text with 12 pixel gutters on both sides.":::
    :::column-end:::
    :::column span="1":::
        **16epx** between surface and edge text
    :::column-end:::
:::row-end:::

## Text + hierarchy

Our type ramp (link) is designed to provide an array of sizes that can help communicate hierarchy within an app.  

:::row:::
    :::column:::
        :::image type="content" source="images/title-subtitle-body.png" alt-text="An example of text using title, subtitle, and body styles when there is adequate space.":::
    :::column-end:::
    :::column:::
        Using Title, Subtitle and Body with 12epx spacing.
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/body-strong-confined-space.png" alt-text="An example of using Body Strong instead of Title in a confined space.":::
    :::column-end:::
    :::column:::
        When differentiating a title in a confined UI space, use Body Strong for the title without any additional spacing between text blocks.
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/confined-space-captions.png" alt-text="An example of using the Caption style in a confined space.":::
    :::column-end:::
    :::column:::
        Use caption size for very confined spaces where text is needed, such as command buttons.
    :::column-end:::
:::row-end:::

## Lists and grids

There are a variety of list and grid styles that can be created. Below are a variety of compositions used in Windows.

:::row:::
    :::column:::
        :::image type="content" source="images/multi-line-list.png" alt-text="An example list with multi-element list items.":::
    :::column-end:::
    :::column:::
        For multi-line lists, use Body and Caption from the type ramp, and 32epx icons.<br><br>Use Body Strong for section headers.
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/horizontal-lists.png" alt-text="An example of horizontal lists.":::
    :::column-end:::
    :::column:::
        When using icons or person picture elements for grid items, use Caption text that is center-aligned.
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/album-list.png" alt-text="An example list containing large list items.":::
    :::column-end:::
    :::column:::
         Use Body style for primary text and left-align to the image if your list contains large graphical elements with text.
    :::column-end:::
:::row-end:::

## Using controls

Some examples of how controls can relate to each other in common configurations.

:::row:::
    :::column:::
        :::image type="content" source="images/expander-with-controls.png" alt-text="An example of an expander with child controls.":::
    :::column-end:::
    :::column:::
        Examples showing how to use an expander control (link) with list styles and common controls. Controls should be right-aligned with 16epx between the control and expander button.
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        :::image type="content" source="images/expander-content-alignment.png" alt-text="An example how controls inside an expander are aligned.":::
    :::column-end:::
    :::column:::
        This example shows controls alignment when placed inside the expander. Indent the controls 48epx.
    :::column-end:::
:::row-end:::
