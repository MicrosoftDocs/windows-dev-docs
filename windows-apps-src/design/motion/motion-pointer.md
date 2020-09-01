---
Description: Use pointer animations to provide users with visual feedback when the user taps on an item.
title: Pointer click animations
ms.assetid: EEB10A2C-629A-4705-8468-4D019D74DDFF
ms.date: 08/09/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Pointer click animations



Use pointer animations to provide users with visual feedback when the user taps on an item. The pointer down animation slightly shrinks and tilts the pressed item, and plays when an item is first tapped. The pointer up animation, which restores the item to its original position, is played when the user releases the pointer.


> **Important APIs**: [**PointerUpThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PointerUpThemeAnimation), [**PointerDownThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PointerDownThemeAnimation)


## Do's and don'ts

-   When you use a pointer up animation, immediately trigger the animation when the user releases the pointer. This provides instant feedback to the user that their action has been recognized, even if the action triggered by the tap (such as navigating to a new page) is slower to respond.

## Related articles

* [Animations overview](./xaml-animation.md)
* [Animating pointer clicks](/previous-versions/windows/apps/jj649432(v=win.10))
* [Quickstart: Animating your UI using library animations](/previous-versions/windows/apps/hh452703(v=win.10))
* [**PointerUpThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PointerUpThemeAnimation)
* [**PointerDownThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PointerDownThemeAnimation)

 

 