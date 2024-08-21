---
description: This article provides detailed guidance for designing interaction for Windows widgets.
title: Widget interaction design guidance
ms.date: 01/19/2022
ms.topic: article
keywords: windows 11, widgets
ms.localizationpriority: medium
---

# Widget interaction design guidance

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**
> [!IMPORTANT]
> The feature described in this topic is available in Dev Channel preview builds of Windows starting with build 25217. For information on preview builds of Windows, see [Windows 10 Insider Preview](https://insider.windows.com/en-us/preview-windows).

This article provides detailed guidance for designing interaction for Windows widgets.

## Navigation

A widget should be glanceable and focused, and should represent a single aspect of the app's primary purpose. Widgets may provide one or more calls to action. When the user clicks on a call to action, the widget should launch the associated app or website instead of implementing the action in the widget itself. A widget has only one primary page that can house multiple interactions. Clicking on an item in the widget should never take you to a completely different view of the widget. For example, in a weather widget you might show the weather for multiple days but clicking on one of the days will not expand details inline, but will instead launch the app or web.

The following are the maximum number of touch points recommended for each supported widget size.

| Widget size | Maximum touchpoints |
|-------------|----------------------|
| small    | 1 |
| medium | 3 |
| large | 4 |

The following navigation elements are not supported in Windows Widgets:

- Pivots will not be supported within widgets
- L2 Pages will not be supported within widgets
- Vertical or horizontal scrolling will not be supported within Widgets

## Containers

The following images show example uses of container elements in a widget template. The containers group visual elements into columns and rows to create a hierarchical grid structure.

![Four images of widgets that illustrate containers. The widgets in the images have elements divided into rows and columns to provide a hierarchical grid structure.](./images/widgets-containers.png)



## Image links

The following images show example uses of image link elements in a widget template. 

![Two images of widgets that illustrate image links. The images are arranged in columns and rows making a grid.](./images/widgets-image-links.png)

## Pagination

The following images show examples of pagination in a widget template. The pagination controls can be aligned horizontally or vertically. Navigation arrows appear in response to a cursor hover. 

![This set of two images show horizontal pagination. In the first image, a column of dots is aligned along the right side. One dot is larger to indicate the currently active page. In the second image a cursor mouses over an down-pointing arrow at the bottom of the widget that lets the user move to the next page. There is a matching up-pointing arrow on the top of the widget for navigating to the previous page.](./images/widgets-pagination.png)

![This set of two images show vertical pagination. In the first image, a row of dots is aligned along the bottom. One dot is larger to indicate the currently active page. In the second image a cursor mouses over an right-pointing arrow on the right side of the widget that lets the user move to the next page. There is a matching left-pointing arrow on the left side of the widget for navigating to the previous page.](./images/widgets-pagination-2.png)

![These two images demonstrate how the pagination controls look when the widget has an image background.](./images/widgets-pagination-3.png)



## Hyperlinks

The following images show example of hyperlinks in a widget template.

![Two images of widgets that illustrate hyperlinks. The first image shows a hyperlinked string of text. The text is plain. In the second image, a mouse cursor hovers over the hyperlink, which causes the text to be underlined.](./images/widgets-hyperlinks.png)

![An image showing a hyperlink centered at the bottom of the widget, just above the horizontal row of pagination dots. A red X indicates that the pagination dots and the hyperlink should not be in the same space. On the right another image shows the hyperlink at the bottom, but the pagination dots are in aligned vertically in a column on the right side. A green check indicates that this placement of the two elements is correct.](./images/widgets-hyperlinks-2.png)



## Dropdown menus

![Two images of widgets that illustrate dropdown menus. In the left image the dropdown menu is collapsed. In the right image the dropdown is expanded and extends over the border of the widget](./images/widgets-dropdown-menus.png)

Widgets are able to extend slightly beyond their widget size temporarily if the user is interacting with a menu or dropdown. The menu behavior should be light dismiss and close the menu if a user click outside of the menu / dropdown area.










 

 

