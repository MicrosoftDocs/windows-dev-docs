---
Description: Use drag-and-drop animations when users move objects, such as moving an item within a list, or dropping an item on top of another.
title: Drag animations
ms.assetid: 6064755F-6E24-4901-A4FF-263F05F0DFD6
label: Motion--Drag and drop
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Drag animations




Use drag-and-drop animations when users move objects, such as moving an item within a list, or dropping an item on top of another.

> **Important APIs**: [**DragItemThemeAnimation class**](/uwp/api/windows.ui.xaml.media.animation.dragitemthemeanimation)


## Do's and don'ts


**Drag start animation**

-   Use the drag start animation when the user begins to move an object.
-   Include affected objects in the animation if and only if there are other objects that can be affected by the drag-and-drop operation.
-   Use the drag end animation to complete any animation sequence that began with the drag start animation. This reverses the size change in the dragged object that was caused by the drag start animation.

**Drag end animation**

-   Use the drag end animation when the user drops a dragged object.
-   Use the drag end animation in combination with add and delete animations for lists.
-   Include affected objects in the drag end animation if and only if you included those same affected objects in the drag start animation.
-   Don't use the drag end animation if you have not first used the drag start animation. You need to use both animations to return objects to their original sizes after the drag sequence is complete.

**Drag between enter animation**

-   Use the drag between enter animation when the user drags the drag source into a drop area where it can be dropped between two other objects.
-   Choose a reasonable drop target area. This area should not be so small that it is difficult for the user to position the drag source for the drop.
-   The recommended direction to move affected objects to show the drop area is directly apart from each other. Whether they move vertically or horizontally depends on the orientation of the affected objects to each other.
-   Don't use the drag between enter animation if the drag source cannot be dropped in an area. The drag between enter animation tells the user that the drag source can be dropped between the affected objects.

**Drag between leave animation**

-   Use the drag between leave animation when the user drags an object away from an area where it could have been dropped between two other objects.
-   Don't use the drag between leave animation if you have not first used the drag between enter animation.


## Related articles

**For developers**
* [Animations overview](./xaml-animation.md)
* [Animating drag-and-drop sequences](/previous-versions/windows/apps/jj649427(v=win.10))
* [Quickstart: Animating your UI using library animations](/previous-versions/windows/apps/hh452703(v=win.10))
* [**DragItemThemeAnimation class**](/uwp/api/windows.ui.xaml.media.animation.dragitemthemeanimation)
* [**DropTargetItemThemeAnimation class**](/uwp/api/windows.ui.xaml.media.animation.droptargetitemthemeanimation)
* [**DragOverThemeAnimation class**](/uwp/api/windows.ui.xaml.media.animation.dragoverthemeanimation)


 