---
author: Jwmsft
Description: A pattern with a content area and a command area for single-view apps or modal experiences, such as photo viewers/editors, document viewers, maps, painting, or other apps that make use of a free-scrolling view.
title: Active canvas layout pattern
ms.assetid: 4D768472-64D6-406C-9E87-F750F6B981A0
label: TBD
template: detail.hbs
op-migration-status: ready
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Active canvas layout pattern

An active canvas is a pattern with a content area and a command area. It's for single-view apps or modal experiences, such as photo viewers/editors, document viewers, maps, painting, or other apps that make use of a free-scrolling view. For taking actions, an active canvas can be paired with a command bar or just buttons, depending on the number and types of actions you need.

## Examples

This design of a photo editing app features an active canvas pattern, with a mobile example on the left, and a desktop example on the right. The image editing surface is a canvas, and the command bar at the bottom contains all of the contextual actions for the app.

![Example of a photo editor using active canvas pattern](images/uap-photo-pc-phone-700.png)

This design of a subway map app makes use of an active canvas with a simple UI strip at the top that has only two actions and a search box. Contextual actions are shown in the context menu, as seen on the right image.

![Example of a maps app using active canvas pattern](images/uap-subway-pc-phone-700.png)


## Implementing this pattern

The active canvas pattern consists of a content area and a command area.

**Content area.**  The content area is usually a free-scrolling canvas. Multiple content areas can exist within an app.

**Command area.**  If you're placing a lot of commands, then a command bar, which responds based on screen size, could be the way to go. If you're not placing that many commands and aren't as concerned with a responsive UI, space-saving buttons work well.



## Related articles

-   [**App bar and command bar**](../controls-and-patterns/app-bars.md)
