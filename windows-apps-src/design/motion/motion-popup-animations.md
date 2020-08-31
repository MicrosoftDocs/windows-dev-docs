---
Description: Use pop-up animations to show and hide pop-up UI for flyouts or custom pop-up UI elements. Pop-up elements are containers that appear over the app's content and are dismissed if the user taps or clicks outside of the pop-up element.
title: Pop-up UI animations
ms.assetid: 4E9025CE-FC90-4d4c-9DE6-EC6B6F2AD9DF
label: Motion--Pop-up animations
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Pop-up UI animations



Use pop-up animations to show and hide pop-up UI for flyouts or custom pop-up UI elements. Pop-up elements are containers that appear over the app's content and are dismissed if the user taps or clicks outside of the pop-up element.

> **Important APIs**: [**PopInThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PopInThemeAnimation), [**PopupThemeTransition class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PopupThemeTransition)


## Do's and don'ts


-   Use pop-up animations to show or hide custom pop-up UI elements that aren't a part of the app page itself. The common controls provided by Windows already have these animations built in.
-   Don't use pop-up animations for tooltips or dialogs.
-   Don't use pop-up animations to show or hide UI within the main content of your app; only use pop-up animations to show or hide a pop-up container that displays on top of the main app content.

## Related articles

* [Animations overview](./xaml-animation.md)
* [Animating pop-up UI](/previous-versions/windows/apps/jj649433(v=win.10))
* [Quickstart: Animating your UI using library animations](/previous-versions/windows/apps/hh452703(v=win.10))
* [**PopInThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PopInThemeAnimation)
* [**PopOutThemeAnimation class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PopOutThemeAnimation)
* [**PopupThemeTransition class**](/uwp/api/Windows.UI.Xaml.Media.Animation.PopupThemeTransition)

 

 