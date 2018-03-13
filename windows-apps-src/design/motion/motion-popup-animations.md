---
author: mijacobs
Description: Use pop-up animations to show and hide pop-up UI for flyouts or custom pop-up UI elements. Pop-up elements are containers that appear over the app's content and are dismissed if the user taps or clicks outside of the pop-up element.
title: Pop-up UI animations in UWP apps
ms.assetid: 4E9025CE-FC90-4d4c-9DE6-EC6B6F2AD9DF
label: Motion--Pop-up animations
template: detail.hbs
ms.author: mijacobs
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Pop-up UI animations



Use pop-up animations to show and hide pop-up UI for flyouts or custom pop-up UI elements. Pop-up elements are containers that appear over the app's content and are dismissed if the user taps or clicks outside of the pop-up element.

> **Important APIs**: [**PopInThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210383), [**PopupThemeTransition class**](https://msdn.microsoft.com/library/windows/apps/hh969172)


## Do's and don'ts


-   Use pop-up animations to show or hide custom pop-up UI elements that aren't a part of the app page itself. The common controls provided by Windows already have these animations built in.
-   Don't use pop-up animations for tooltips or dialogs.
-   Don't use pop-up animations to show or hide UI within the main content of your app; only use pop-up animations to show or hide a pop-up container that displays on top of the main app content.

## Related articles

* [Animations overview](https://msdn.microsoft.com/library/windows/apps/mt187350)
* [Animating pop-up UI](https://msdn.microsoft.com/library/windows/apps/xaml/jj649433)
* [Quickstart: Animating your UI using library animations](https://msdn.microsoft.com/library/windows/apps/xaml/hh452703)
* [**PopInThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210383)
* [**PopOutThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210391)
* [**PopupThemeTransition class**](https://msdn.microsoft.com/library/windows/apps/hh969172)

 

 




