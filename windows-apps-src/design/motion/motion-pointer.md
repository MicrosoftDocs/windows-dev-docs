---
author: mijacobs
Description: Use pointer animations to provide users with visual feedback when the user taps on an item.
title: Pointer click animations in UWP apps
ms.assetid: EEB10A2C-629A-4705-8468-4D019D74DDFF
ms.author: jimwalk
ms.date: 08/9/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Pointer click animations



Use pointer animations to provide users with visual feedback when the user taps on an item. The pointer down animation slightly shrinks and tilts the pressed item, and plays when an item is first tapped. The pointer up animation, which restores the item to its original position, is played when the user releases the pointer.


> **Important APIs**: [**PointerUpThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/hh969168), [**PointerDownThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/hh969164)


## Do's and don'ts

-   When you use a pointer up animation, immediately trigger the animation when the user releases the pointer. This provides instant feedback to the user that their action has been recognized, even if the action triggered by the tap (such as navigating to a new page) is slower to respond.

## Related articles

* [Animations overview](https://msdn.microsoft.com/library/windows/apps/mt187350)
* [Animating pointer clicks](https://msdn.microsoft.com/library/windows/apps/xaml/jj649432)
* [Quickstart: Animating your UI using library animations](https://msdn.microsoft.com/library/windows/apps/xaml/hh452703)
* [**PointerUpThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/hh969168)
* [**PointerDownThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/hh969164)

 

 




