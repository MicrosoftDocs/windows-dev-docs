---
author: drewbatgit
description: This article provides detailed guidance for designing interaction for Windows widgets.
title: Widget interaction design guidance
ms.author: drewbat
ms.date: 01/19/2022
ms.topic: article
keywords: windows 11, widgets
ms.localizationpriority: medium
---

# Widget interaction design guidance

This article provides detailed guidance for designing interaction for Windows widgets.

## Navigation

A widget should be glanceable and focused, and should represent a single aspect of the app’s primary purpose. Widgets may provide one or more calls to action. When the user clicks on a call to action, the widget should launch the associated app or website instead of implementing the action in the widget itself. A widget has only one primary page that can house multiple interactions. Clicking on an item in the widget should never take you to a completely different view of the widget. For example, in a weather widget you might show the weather for multiple days but clicking on one of the days will not expand details inline, but will instead launch the app or web.

The following are the maximum number of touch points recommended for each supported widget size.

| Widget size | Maxiumum touchpoints |
|-------------|----------------------|
| small    | 1 |
| medium | 3 |
| large | 4 |

The following navigation elements are not supported in Windows Widgets:

- Pivots will not be supported within widgets
- L2 Pages will not be supported within widgets
- Vertical or horizontal scrolling will not be supported within Widgets [TBD - Link to info about carousel]

## Containers

![Four images of widgets that illustrate containers. TBD - what is this image demonstrating?](./images/widgets-containers.png)

TBD

## Image links

![Two images of widgets that illustrate image links. TBD - what is this image demonstrating?](./images/widgets-image-links.png)

## Pagination

![Four images of widgets that illustrate pagination. TBD - what is this image demonstrating?](./images/widgets-image-links.png)

TBD

## Hyperlinks

![Four images of widgets that illustrate hyperlinks. TBD - what is this image demonstrating?](./images/widgets-image-links.png)

TBD

## Dropdown menus

![Two images of widgets that illustrate dropdown menus. In the left image the dropdown menu is collapsed. In the right image the dropdown is expanded and extends over the border of the widget](./images/widgets-image-links.png)

Widgets are able to extend slightly beyond their widget size temporarily if the user is interacting with a menu or dropdown. The menu behavior should be light dismiss and close the menu if a user click outside of the menu / dropdown area.










 

 

