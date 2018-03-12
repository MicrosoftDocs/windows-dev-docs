---
author: mijacobs
Description: Edge-based animations show or hide UI that originates from the edge of the screen.
title: Edge-based UI animations in UWP apps
ms.assetid: 5A8F73B1-F4F6-424b-9EDF-A9766C5DEAE8
label: Motion--edge-based UI
template: detail.hbs
ms.author: mijacobs
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Edge-based UI animations





Edge-based animations show or hide UI that originates from the edge of the screen. The show and hide actions can be initiated either by the user or by the app. The UI can either overlay the app or be part of the main app surface. If the UI is part of the app surface, the rest of the app might need to be resized to accommodate it.

> **Important APIs**: [**EdgeUIThemeTransition class**](https://msdn.microsoft.com/library/windows/apps/hh702324)


## Do's and don'ts


-   Use edge UI animations to show or hide a custom message or error bar that does not extend far into the screen.
-   Use panel animations to show UI that slides a significant distance into the screen, such as a task pane or a custom soft keyboard.
-   Slide the UI in from the same edge it will be attached to.
-   Slide the UI out to the same edge it came from.
-   If the contents of the app need to resize in response to the UI sliding in or out, use fade animations for the resize.
    -   If the UI is sliding in, use a fade animation after the edge UI or panel animation.
    -   If the UI is sliding out, use a fade animation at the same time as the edge UI or panel animation.
-   Don't apply these animations to notifications. Notifications should not be housed within edge-based UI.
-   Don't apply the edge UI or panel animations to any UI container or control that is not at the edge of the screen. These animations are used only for showing, resizing, and dismissing UI at the edges of the screen. To move other types of UI, use reposition animations.

    ![illustrates when to use edge ui or panel animations and when to use reposition.](images/edgevsreposition.png)

## Related articles


**For developers**
* [Animations overview](https://msdn.microsoft.com/library/windows/apps/mt187350)
* [Animating edge-based UI](https://msdn.microsoft.com/library/windows/apps/xaml/jj649428)
* [Quickstart: Animating your UI using library animations](https://msdn.microsoft.com/library/windows/apps/xaml/hh452703)
* [**EdgeUIThemeTransition class**](https://msdn.microsoft.com/library/windows/apps/hh702324)
* [**PaneThemeTransition class**](https://msdn.microsoft.com/library/windows/apps/hh969160)
* [Animating fades](https://msdn.microsoft.com/library/windows/apps/xaml/jj649429)
* [Animating repositions](https://msdn.microsoft.com/library/windows/apps/xaml/jj649434)

 

 




