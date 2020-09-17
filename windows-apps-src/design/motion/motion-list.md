---
Description: List animations let you insert or remove single or multiple items from a collection, such as a photo album or a list of search results.
title: Add and delete animations
ms.assetid: A85006AE-4992-457a-B514-500B8BEF5DC8
label: Motion--add and delete animations
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Add and delete animations



List animations let you insert or remove single or multiple items from a collection, such as a photo album or a list of search results.

> **Important APIs**: [**AddDeleteThemeTransition class**](/uwp/api/windows.ui.xaml.media.animation.adddeletethemetransition)


## Do's and don'ts


-   Use list animations to add a single new item to an existing set of items. For example, use them when a new email arrives or when a new photo is imported into an existing set.
-   Use list animations to add several items to a set at one time. For example, use them when you import a new set of photos to an existing collection. The addition or deletion of multiple items should happen at the same time, with no delay between the action on the individual objects.
-   Use add and delete list animations as a pair. Whenever you use one of these animations, use the corresponding animation for the opposite action.
-   Use list animations with a list of items to which you can add or delete one element or group of elements at once.
-   Don't use list animations to display or remove a container. These animations are for members of a collection or set that is already being displayed. Use pop-up animations to show or hide a transient container on top of the app surface. Use content transition animations to display or replace a container that is part of the app surface.
-   Don't use list animations on an entire set of items. Use the content transition animations to add or remove an entire collection within your container.



## Related articles

* [Animations overview](./xaml-animation.md)
* [Animating list additions and deletions](/previous-versions/windows/apps/jj649430(v=win.10))
* [Quickstart: Animating your UI using library animations](/previous-versions/windows/apps/hh452703(v=win.10))
* [**AddDeleteThemeTransition class**](/uwp/api/windows.ui.xaml.media.animation.adddeletethemetransition)

 

 