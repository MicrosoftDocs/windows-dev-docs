---
Description: Content transition animations let you change the content of an area of the screen while keeping the container or background constant. New content fades in. If there is existing content to be replaced, that content fades out.
title: Guidelines for content transition animations
ms.assetid: 0188FDB4-E183-466f-8A03-EE3FF5C474B1
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
pm-contact: stmoy
design-contact: conrwi
doc-status: Published
ms.localizationpriority: medium
---
# Content transition animations



Content transition animations let you change the content of an area of the screen while keeping the container or background constant. New content fades in. If there is existing content to be replaced, that content fades out.

> **Important APIs**: [**ContentThemeTransition class (XAML)**](/uwp/api/windows.ui.xaml.media.animation.contentthemetransition)

## Do's and don'ts


-   Use an entrance animation when there is a set of new items to bring into an empty container. For example, after the initial load of an app, part of the app's content might not be immediately available for display. When that content is ready to be shown, use a content transition animation to bring that late content into the view.
-   Use content transitions to replace one set of content with another set of content that already resides in the same container within a view.
-   When bringing in new content, slide that content up (from bottom to top) into the view against the general page flow or reading order.
-   Introduce new content in a logical manner, for example, introduce the most important piece of content last.
-   If you have more than one container whose content is to be updated, trigger all of the transition animations simultaneously without any staggering or delay.
-   Don't use content transition animations when the entire page is changing. In that case, use the page transition animations instead.
-   Don't use content transition animations if the content is only refreshing. Content transition animations are meant to show movement. For refreshes, use fade animations.



## Related articles

**For developers (XAML)**
* [Animations overview](./xaml-animation.md)
* [Animating content transitions](/previous-versions/windows/apps/jj649426(v=win.10))
* [Quickstart: Animating your UI using library animations](/previous-versions/windows/apps/hh452703(v=win.10))
* [**ContentThemeTransition class**](/uwp/api/windows.ui.xaml.media.animation.contentthemetransition)

 

 