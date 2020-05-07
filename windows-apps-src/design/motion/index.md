---
Description: Purposeful, well-designed motion brings your app to life and makes the experience feel crafted and polished. Help users understand context changes, and tie experiences together with visual transitions.
title: Motion for Windows apps
ms.assetid: 21AA1335-765E-433A-85D8-560B340AE966
label: Motion
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
pm-contact: stmoy
design-contact: jeffarn
doc-status: Published
ms.localizationpriority: medium
ms.custom: RS5
---
# Motion for Windows apps

![Motion icon](../images/motion-2x.png)

Fluent motion serves a purpose in your app. It gives intelligent feedback based on the user's behavior, keeps the UI feeling alive, and guides the user's navigation through your app. Fluent motion elicits an emotional connection between a user and their digital experience. We build on a foundation of natural movement the user already understands from the physical world, and we extend our system from there.

## Examples

<table>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon.png" alt="XAML controls gallery" width="168"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/category/Motion">open the app and see Motion in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Fluent motion principles

### Physical

Objects in motion exhibit behaviors of objects in the real world.​ Fluid, responsive movement makes the experience feel natural, creating emotional connections and adding personality.

![UI example of physical motion](images/Physical.gif)
> When you interact with UI via touch, the movement of the UI is directly related to the velocity of the interaction. And because touch is direct manipulation, the object you interact with affects the objects around it.

### Functional

Motion serves a purpose and has conviction. It guides the user through complexity and helps establish hierarchy. Movement gives the impression of enhanced performance and optimizes the user experience by hiding perceived latency.

![UI example of functional motion](images/functional.gif)
> Page transitions are purpose-built. They give hints about how pages are related to each other. They move in a manner that's perceived as fast even when performance is not optimal.

### Continuous

Fluid movement from point to point naturally draws the eye and guides the user.​ It elegantly stitches together a user’s task, making it feel more consumable and friendly.

![UI example of continuous motion](images/continuous3.gif)
> Objects can travel from scene to scene or morph within a scene to provide continuity and help the user maintain context.

### Contextual

Intelligent motion provides feedback to the user in a manner that's aligned with how they manipulated the UI. Interaction is centered around the user.​ The movement feels appropriate to the form-factor and designed around the scenario.​ It should be comfortable for each user.​

![UI example of contextual motion](images/Contextual.gif)
> Animation should tie back to the user interaction. A context menu is deployed from a point where the user activated it.

## Motion articles

:::row:::
    :::column:::
### [Timing and easing](timing-and-easing.md)
Timing and easing are important elements that make motion feel natural for objects entering, exiting, or moving within the UI.​
    :::column-end:::
    :::column:::
### [Directionality and gravity](directionality-and-gravity.md)
Directional signals help provide a solid mental model of the journey a user takes across experiences. Directional movement is subject to forces like gravity, which reinforces the natural feel of the movement.​
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
### [Page transitions](page-transitions.md)
Page transitions navigate users between pages in an app, providing feedback about the relationship between pages. They help users understand where they are in the navigation hierarchy.
    :::column-end:::
    :::column:::
### [Connected animation](connected-animation.md)
Connected animations let you create a dynamic and compelling navigation experience by animating the transition of an element between two different views.​
    :::column-end:::
:::row-end:::
