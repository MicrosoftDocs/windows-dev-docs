---
description: The list/detail pattern displays a list of items and the details for the currently selected item. This pattern is frequently used for email and contact lists/address books.
title: List/details
ms.assetid: 45C9FE8B-ECA6-44BF-8DDE-7D12ED34A7F7
label: List/details
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# List/details pattern

The list/details pattern has a list pane (usually with a [list view](lists.md)) and a details pane for content. When an item in the list is selected, the details pane is updated. This pattern is frequently used for email and address books.

> **Important APIs**: [ListView class](/uwp/api/Windows.UI.Xaml.Controls.ListView), [SplitView class](/uwp/api/windows.ui.xaml.controls.splitview)

![Example of list-details pattern](images/list-detail-pattern.png)

> [!TIP]
> If you'd like to use a XAML control that implements this pattern for you, we recommend the [ListDetailsView XAML Control](/windows/communitytoolkit/controls/masterdetailsview) from the Windows Community Toolkit.

## Is this the right pattern?

The list/details pattern works well if you want to:

- Build an email app, address book, or any app that is based on a list-details layout.
- Locate and prioritize a large collection of content.
- Allow the quick addition and removal of items from a list while working back-and-forth between contexts.

## Choose the right style

When implementing the list/details pattern, we recommend that you use either the stacked style or the side-by-side style, based on the amount of available screen space.

| Available window width | Recommended style |
|------------------------|-------------------|
| 320 epx-640 epx        | Stacked           |
| 641 epx or wider       | Side-by-side      |

## Stacked style

In the stacked style, only one pane is visible at a time: the list or the details.

![A list detail in stacked mode](images/patterns-md-stacked.png)

The user starts at the list pane and "drills down" to the details pane by selecting an item in the list. To the user, it appears as though the list and details views exist on two separate pages.

### Create a stacked list/details pattern

One way to create the stacked list/details pattern is to use separate pages for the list pane and the details pane. Place the list view on one page, and the details pane on a separate page.

![Parts for the stacked-style list detail](images/patterns-ld-stacked-parts.png)

For the list view page, a [list view](lists.md) control works well for presenting lists that can contain images and text.

For the details view page, use the [content element](../layout/layout-panels.md) that makes the most sense. If you have a lot of separate fields, consider using a **Grid** layout to arrange elements into a form.

For navigation between pages, see [navigation history and backwards navigation for Windows apps](../basics/navigation-history-and-backwards-navigation.md).

## Side-by-side style

In the side-by-side style, the list pane and details pane are visible at the same time.

![The list/detail pattern](images/patterns-listdetail-400x227.png)

The list in the list pane has a selection visual to indicate the currently selected item. Selecting a new item in the list updates the details pane.

### Create a side-by-side list/details pattern

One way to create a side-by-side list/details pattern is to use the [split view](split-view.md) control. Place the list view in the split view pane, and the details view in the split view content.

![list detail split view parts](images/patterns-ld-splitview-parts.png)

For the list pane, a [list view](lists.md) control works well for presenting lists that can contain images and text.

For the details content, use the [content element](../layout/layout-panels.md) that makes the most sense. If you have a lot of separate fields, consider using a **Grid** layout to arrange elements into a form.

## Adaptive layout

To implement a list/details pattern for any screen size, create a responsive UI with an [adaptive layout](../layout/layouts-with-xaml.md).

![adaptive list detail layout](images/patterns_listdetail.png)

### Create an adaptive list/details pattern
To create an adaptive layout, define different [**VisualStates**](/uwp/api/windows.ui.xaml.visualstate) for your UI, and declare breakpoints for the different states with [**AdaptiveTriggers**](/uwp/api/Windows.UI.Xaml.AdaptiveTrigger).

## Get the sample code

The following samples implement the list/details pattern with adaptive layouts and demonstrate data binding to static, database, and online resources: 
- [Master/detail sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlMasterDetail) 
- [ListView and GridView sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlListView)
- [Windows Template Studio Master/Detail sample](https://github.com/Microsoft/WindowsTemplateStudio/tree/master/templates/Uwp/Pages/MasterDetail)
- [Customer orders database sample](https://github.com/Microsoft/Windows-appsample-customers-orders-database)
- [RSS Reader sample](https://github.com/Microsoft/Windows-appsample-rssreader)

> [!TIP]
> If you'd like to use a XAML control that implements this pattern for you, we recommend the [ListDetailsView XAML Control](/windows/communitytoolkit/controls/masterdetailsview) from the Windows Community Toolkit.

## Related articles

- [Lists](lists.md)
- [App and command bars](command-bar.md)
- [ListView class](/uwp/api/Windows.UI.Xaml.Controls.ListView)
- [SplitView class](/uwp/api/windows.ui.xaml.controls.splitview)
