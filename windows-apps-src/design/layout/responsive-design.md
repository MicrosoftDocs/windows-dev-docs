---
Description: Learn responsive design techniques to tailor your app for specific devices
title: Responsive design techniques
template: detail.hbs
op-migration-status: ready
ms.date: 10/10/2017
ms.topic: article
keywords: windows 10, uwp
localizationpriority: medium
ms.custom: RS5
---
# Responsive design techniques

UWP apps use effective pixels to guarantee that your UI will be legible and usable on all Windows-powered devices. So, why would you ever want to customize your app's UI for a specific device family?

- **To make the most effective use of space and reduce the need to navigate**

    If you design an app to look good on a device that has a small screen, such as a tablet, the app will be usable on a PC with a much bigger display, but there will probably be some wasted space. You can customize the app to display more content when the screen is above a certain size. For example, a shopping app might display one merchandise category at a time on a tablet, but show multiple categories and products simultaneously on a PC or laptop.

    By putting more content on the screen, you reduce the amount of navigation that the user needs to perform.

- **To take advantage of devices' capabilities**

    Certain devices are more likely to have certain device capabilities. For example, laptops are likely to have a location sensor and a camera, while a TV might not have either. Your app can detect which capabilities are available and enable features that use them.

- **To optimize for input**

    The universal control library works with all input types (touch, pen, keyboard, mouse), but you can still optimize for certain input types by re-arranging your UI elements. For example, if you place navigation elements at the bottom of the screen, they'll be easier for phone users to access—but most PC users expect to see navigation elements toward the top of the screen.

When you optimize your app's UI for specific screen widths, we say that you're creating a responsive design. Here are six responsive design techniques you can use to customize your app's UI.

>[!TIP]
> Many UWP controls automatically implement these responsive behaviors. To create a responsive UI, we recommend checking out the [UWP controls](../controls-and-patterns/index.md).

## Reposition

You can alter the location and position of UI elements to make the most of the window size. In this example, the smaller window stacks elements vertically. When the app translates to a larger window, elements can take advantage of the wider window width.

![Reposition](images/rsp-design/rspd-reposition2.gif)

In this example design for a photo app, the photo app repositions its content on larger screens.

## Resize

You can optimize for the window size by adjusting the margins and size of UI elements. For example, this could augment the reading experience on a larger screen by simply growing the content frame.

![Resizing design elements](images/rsp-design/rspd-resize2.gif)

## Reflow

By changing the flow of UI elements based on device and orientation, your app can offer an optimal display of content. For instance, when going to a larger screen, it might make sense to add columns, use larger containers, or generate list items in a different way.

This example shows how a single column of vertically scrolling content on a smaller screen that can be reflowed on a larger screen to display two columns of text.

![Reflowing design elements](images/rsp-design/rspd_reflow.gif)

## Show/hide

You can show or hide UI elements based on screen real estate, or when the device supports additional functionality, specific situations, or preferred screen orientations.

![Hiding design elements](images/rsp-design/rspd-revealhide.gif)

For example, media player controls reduce the button set on smaller screens and expand on larger screens. The media player on a larger window can handle far more on-screen functionality than it can on a smaller window.

Part of the reveal-or-hide technique includes choosing when to display more metadata. With smaller windows, it's best to show a minimal amount of metadata. With larger windows, a significant amount of metadata can be surfaced. Some examples of when to show or hide metadata include:

- In an email app, you can display the user's avatar.
- In a music app, you can display more info about an album or artist.
- In a video app, you can display more info about a film or a show, such as showing cast and crew details.
- In any app, you can break apart columns and reveal more details.
- In any app, you can take something that's vertically stacked and lay it out horizontally. When going from phone or phablet to larger devices, stacked list items can change to reveal rows of list items and columns of metadata.

## Replace

This technique lets you switch the user interface for a specific breakpoints. In this example, the nav pane and its compact, transient UI works well for a smaller screen, but on a larger screen, tabs might be a better choice.

![Replacing design elements](images/rsp-design/rspd-replace.gif)

The [NavigationView](../controls-and-patterns/navigationview.md) control supports this responsive technique, by letting users set the pane position to either top or left.

## Re-architect

You can collapse or fork the architecture of your app to better target specific devices. In this example, expanding the window shows the entire master/details pattern.

![an example of re-architecting a user interface](images/rsp-design/rspd-rearchitect.gif)

## Related topics

- [Screen sizes and breakpoints](screen-sizes-and-breakpoints-for-responsive-design.md)
- [Responsive layouts with XAML](layouts-with-xaml.md)
- [UWP controls and patterns](../controls-and-patterns/index.md)
