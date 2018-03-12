---
author: mijacobs
Description: Use fade animations to bring items into a view or to take items out of a view. The two common fade animations are fade-in and fade-out.
title: Fade animations in UWP apps
ms.assetid: 975E5EE3-EFBE-4159-8D10-3C94143DD07F
label: Motion--fades
template: detail.hbs
ms.author: mijacobs
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Fade animations



Use fade animations to bring items into a view or to take items out of a view. The two common fade animations are fade-in and fade-out.

> **Important APIs**: [**FadeInThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210298), [**FadeOutThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210302)


## Do's and don'ts


-   When your app transitions between unrelated or text-heavy elements, use a fade-out followed by a fade-in. This allows the outgoing object to completely disappear before the incoming object is visible.
-   Fade in the incoming element or elements on top of the outgoing elements if the size of the elements remains constant, and if you want the user to feel that they're looking at the same item. Once the fade-in is complete, the outgoing item can be removed. This is only a viable option when the outgoing item will be completely covered by the incoming item.
-   Avoid fade animations to add or delete items in a list. Instead, use the list animations created for that purpose.
-   Avoid fade animations to change the entire contents of a page. Instead, use the page transition animations created for that purpose.
-   Fade-out is a subtle way to remove an element.
## Related articles

* [Animations overview](https://msdn.microsoft.com/library/windows/apps/mt187350)
* [Animating fades](https://msdn.microsoft.com/library/windows/apps/xaml/jj649429)
* [Quickstart: Animating your UI using library animations](https://msdn.microsoft.com/library/windows/apps/xaml/hh452703)
* [**FadeInThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210298)
* [**FadeOutThemeAnimation class**](https://msdn.microsoft.com/library/windows/apps/br210302)

 

 




