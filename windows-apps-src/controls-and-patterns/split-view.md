---
author: Jwmsft
title: Split view
ms.assetid: E9E4537F-1160-4183-9A83-26602FCFDC9A
description: A split view control has an expandable/collapsible pane and a content area.
label: Split view
template: detail.hbs
ms.author: jimwalk
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: yulikl
design-contact: kimsea
dev-contact: tpaine
doc-status: Published
---
# Split view control

<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css"> 

A split view control has an expandable/collapsible pane and a content area.

> **Important APIs**: [SplitView class](https://msdn.microsoft.com/library/windows/apps/dn864360)

Here is an example of the Microsoft Edge app using SplitView to show its Hub.

![Microsoft Edge split view example](images/split_view_Edge.png)


 A split view's content area is always visible. The pane can expand and collapse or remain in an open state, and can present itself from either the left side or right side of an app window. The pane has four modes:

-   **Overlay**

    The pane is hidden until opened. When open, the pane overlays the content area.

-   **Inline**

    The pane is always visible and doesn't overlay the content area. The pane and content areas divide the available screen real estate.

-   **CompactOverlay**

    A narrow portion of the pane is always visible in this mode, which is just wide enough to show icons. The default closed pane width is 48px, which can be modified with `CompactPaneLength`. If the pane is opened, it will overlay the content area.

-   **CompactInline**

    A narrow portion of the pane is always visible in this mode, which is just wide enough to show icons. The default closed pane width is 48px, which can be modified with `CompactPaneLength`. If the pane is opened, it will reduce the space available for content, pushing the content out of its way.

## Is this the right control?

The split view control can be used to make a [navigation pane](navigationview.md). To build this pattern, add an expand/collapse button (the "hamburger" button) and a list view representing the nav items.

The split view control can also be used to create any "drawer" experience where users can open and close the supplemental pane.

## Create a split view

Here's a SplitView control with an open Pane appearing inline next to the Content.
```xaml
<SplitView IsPaneOpen="True"
           DisplayMode="Inline"
           OpenPaneLength="296">
    <SplitView.Pane>
        <TextBlock Text="Pane"
                   FontSize="24"
                   VerticalAlignment="Center"
                   HorizontalAlignment="Center"/>
    </SplitView.Pane>

    <Grid>
        <TextBlock Text="Content"
                   FontSize="24"
                   VerticalAlignment="Center"
                   HorizontalAlignment="Center"/>
    </Grid>
</SplitView>
```



## Related topics
* [Nav pane pattern](navigationview.md)
* [List view](lists.md)
 

 
