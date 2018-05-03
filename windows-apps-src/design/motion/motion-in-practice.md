---
author: jwmsft
Description: Learn how Fluent motion fundamentals come together in your app.
title: Motion in practice - animation in UWP apps
label: Motion in practice
template: detail.hbs
ms.author: jimwalk
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: stmoy
design-contact: jeffarn
doc-status: Draft
ms.localizationpriority: medium
---
# Bringing it together

> [!IMPORTANT]
> This article describes functionality that hasn’t been released yet and may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.

Timing, easing, directionality, and gravity work together to form the foundation of Fluent motion. Each has to be considered in the context of the others, and applied appropriately in the context of your app.

Here are 3 ways to apply Fluent motion fundamentals in your app.

:::row:::
    :::column:::
        **Implicit animation**

        Automatic tween and timing between values in a parameter change to achieve very simple Fluent motion using the standardized values.​
    :::column-end:::
    :::column:::
        **Built-in animation**

        System components, such as common controls and shared motion, are "Fluent by default". Fundamentals have been applied in a manner consistent with their implied usage.
    :::column-end:::
    :::column:::
        **Custom animation following guidance recommendations**

        There may be times when the system does not yet provide an exact motion solution for your scenario. In those cases, use the baseline fundamental recommendations as a starting point for your experiences.
    :::column-end:::
:::row-end:::

### **Transition example**

![functional animation](images/pageRefresh.gif)

:::row:::
    :::column:::
        <b>Direction Forward Out:</b><br>
        Fade out: 150m; Easing: Default Accelerate

        <b>Direction Forward In:</b><br>
        Slide up 150px: 300ms; Easing: Default Decelerate
    :::column-end:::
    :::column:::
         <b>Direction Backward Out:</b><br>
        Slide down 150px: 150ms; Easing: Default Accelerate
      
        <b>Direction Backward In:</b><br>
        Fade in: 300ms; Easing: Default Decelerate
    :::column-end:::
:::row-end:::

 ### **Object example**

 ![300ms motion](images/control.gif)
 
:::row:::
    :::column:::
        <b>Direction Expand:</b><br>
        Grow: 300ms; Easing: Standard
    :::column-end:::
    :::column:::
        <b>Direction Contract:</b><br>
        Grow: 150ms; Easing: Default Accelerate
    :::column-end:::
:::row-end:::

## Related articles

- [Motion overview](index.md)
- [Timing and easing](timing-and-easing.md)
- [Directionality and gravity](directionality-and-gravity.md)