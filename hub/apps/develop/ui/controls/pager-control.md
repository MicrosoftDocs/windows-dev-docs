---
title: PagerControl for WinUI apps
description: Learn how to use the experimental WinUI PagerControl to let users move between numbered pages of content and respond when the page changes.
author: GrantMeStrength
ms.author: jken
ms.date: 10/13/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# PagerControl for WinUI apps

> [!IMPORTANT]
> `PagerControl` is an experimental control. In Windows App SDK 2.0, it's available starting in [Windows App SDK 2.0 Experimental 1](../../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-20-experimental-1-200-experimental1). The APIs are marked experimental and might change or be removed in a future release. Don't use them in production apps.

Use the experimental [`PagerControl`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol?view=windows-app-sdk-2.0-experimental&preserve-view=true) to let users move through content that's divided into numbered pages, such as search results, a product catalog, or a long table. The control shows the current page and, when it's known, the total number of pages. Users can select a page number directly, or use buttons to go to the first, previous, next, or last page.

PagerControl doesn't load or display your content. When the selected page changes, your app gets the items for that page and shows them in another control, such as a [ListView or GridView](listview-and-gridview.md).

To try the control, install a Windows App SDK experimental package and the matching runtime. For download links and setup information, see [Latest Windows App SDK downloads](../../../windows-app-sdk/downloads.md#experimental-release).

## Is this the right control?

Use a PagerControl when:

- Your content is divided into numbered pages, and the page number helps users keep track of where they are.
- Users need to go directly to a specific page, or to the first or last page.
- Your app loads content one page at a time, for example, from a web service that returns paged results.

Consider a different control when:

- Pages don't need visible numbers, or the available space is limited. Use a [PipsPager](pipspager.md).
- Users browse items one at a time, such as photos in an album. Use a [FlipView](flipview.md), optionally with a PipsPager.
- Users scroll through a long, continuous collection. Use a [ListView or GridView](listview-and-gridview.md), which virtualizes its items, instead of dividing the content into pages.

The following table compares PagerControl with [PipsPager](pipspager.md).

| Feature | PagerControl | PipsPager |
|---|---|---|
| Page representation | Page numbers in a combo box, a number box, or a row of buttons | Glyphs, typically dots, without page numbers |
| Total number of pages | Shown when the number of pages is known | Not shown |
| Navigation buttons | First, previous, next, and last page | Previous and next page |
| Typical use | Search results, data tables, and catalogs with many pages | Photo viewers, carousels, and app lists where space is limited |
| API status | Experimental | Stable |

## Create a PagerControl

> [!div class="checklist"]
>
> - **Important APIs**: [PagerControl class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol?view=windows-app-sdk-2.0-experimental&preserve-view=true), [NumberOfPages property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.numberofpages?view=windows-app-sdk-2.0-experimental&preserve-view=true), [SelectedPageIndex property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.selectedpageindex?view=windows-app-sdk-2.0-experimental&preserve-view=true), [SelectedIndexChanged event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.selectedindexchanged?view=windows-app-sdk-2.0-experimental&preserve-view=true)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the PagerControl in action](winui3gallery://item/PagerControl)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

Set the [NumberOfPages](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.numberofpages?view=windows-app-sdk-2.0-experimental&preserve-view=true) property to the total number of pages. Use the [SelectedPageIndex](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.selectedpageindex?view=windows-app-sdk-2.0-experimental&preserve-view=true) property to get or set the current page. SelectedPageIndex is zero-based, so a value of 0 selects page 1.

```xaml
<PagerControl
    AutomationProperties.Name="Search results pages"
    NumberOfPages="12"
    SelectedPageIndex="2" />
```

This example creates a pager for 12 pages and selects page 3. By default, the control shows the text **Page**, a box that contains the current page number, and the text **of 12**. The first, previous, next, and last page buttons appear on either side.

:::image type="content" source="images/pager-control/default.png" alt-text="A PagerControl with first page and previous page buttons, the text Page, a number box that contains 3, the text of 12, and next page and last page buttons.":::

## Choose a display mode

The [DisplayMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.displaymode?view=windows-app-sdk-2.0-experimental&preserve-view=true) property determines how users select a page number. Set it to one of the [PagerControlDisplayMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontroldisplaymode?view=windows-app-sdk-2.0-experimental&preserve-view=true) values.

| Value | Description |
|---|---|
| **Auto** | The default. The control shows a combo box when there are fewer than 10 pages. It shows a number box when there are 10 or more pages, or when the number of pages is unknown. |
| **ComboBox** | Users select the page number from a drop-down list. |
| **NumberBox** | Users type the page number in a [NumberBox](number-box.md). |
| **ButtonPanel** | Users select the page number from a row of buttons. When there are too many pages to show a button for each one, the control shows the page numbers around the current page and replaces the others with an ellipsis. |

The following example creates a pager in each of the **ComboBox**, **NumberBox**, and **ButtonPanel** display modes.

```xaml
<StackPanel Spacing="20">
    <PagerControl
        AutomationProperties.Name="Chapter pages"
        DisplayMode="ComboBox"
        NumberOfPages="5"
        SelectedPageIndex="1" />
    <PagerControl
        AutomationProperties.Name="Catalog pages"
        DisplayMode="NumberBox"
        NumberOfPages="50"
        SelectedPageIndex="9" />
    <PagerControl
        AutomationProperties.Name="Article pages"
        DisplayMode="ButtonPanel"
        NumberOfPages="20"
        SelectedPageIndex="4" />
</StackPanel>
```

:::image type="content" source="images/pager-control/display-modes.png" alt-text="Three PagerControls. The first shows a combo box set to 2, followed by the text of 5. The second shows a number box that contains 10, followed by the text of 50. The third shows buttons for pages 1, 4, 5, 6, and 20, with an ellipsis after page 1 and after page 6.":::

In this example, the **ButtonPanel** pager has 20 pages and page 5 is selected, so it shows buttons for pages 4, 5, and 6 and replaces the other pages with an ellipsis. In **ButtonPanel** mode, the control always shows buttons for the first and last pages by default. To show only the page numbers around the current page, set [ButtonPanelAlwaysShowFirstLastPageIndex](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.buttonpanelalwaysshowfirstlastpageindex?view=windows-app-sdk-2.0-experimental&preserve-view=true) to **false**.

## Configure the navigation buttons

PagerControl has four navigation buttons: first page, previous page, next page, and last page. Use the [FirstButtonVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.firstbuttonvisibility?view=windows-app-sdk-2.0-experimental&preserve-view=true), [PreviousButtonVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.previousbuttonvisibility?view=windows-app-sdk-2.0-experimental&preserve-view=true), [NextButtonVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.nextbuttonvisibility?view=windows-app-sdk-2.0-experimental&preserve-view=true), and [LastButtonVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.lastbuttonvisibility?view=windows-app-sdk-2.0-experimental&preserve-view=true) properties to set when each button appears. Set each property to one of the [PagerControlButtonVisibility](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrolbuttonvisibility?view=windows-app-sdk-2.0-experimental&preserve-view=true) values.

| Value | Description |
|---|---|
| **Visible** | The default. The button is always shown. It's disabled when it doesn't apply, such as the previous page button on the first page. |
| **HiddenOnEdge** | The button is hidden and disabled when it doesn't apply: on the first page for the first and previous page buttons, and on the last page for the next and last page buttons. The hidden button still takes up layout space, so the rest of the control doesn't move. |
| **Hidden** | The button isn't shown and doesn't take up layout space. |

The following example hides the first and last page buttons, and hides the previous and next page buttons when they don't apply.

```xaml
<PagerControl
    AutomationProperties.Name="Search results pages"
    NumberOfPages="12"
    FirstButtonVisibility="Hidden"
    PreviousButtonVisibility="HiddenOnEdge"
    NextButtonVisibility="HiddenOnEdge"
    LastButtonVisibility="Hidden" />
```

On the first page, the previous page button is hidden but keeps its space, so the page selector stays in place when the user moves to the next page.

:::image type="content" source="images/pager-control/button-visibility.png" alt-text="A PagerControl with an empty space where the previous page button is hidden, followed by the text Page, a number box that contains 1, the text of 12, and a next page button.":::

When the user selects a navigation button, the control updates SelectedPageIndex. If you set the matching [FirstButtonCommand](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.firstbuttoncommand?view=windows-app-sdk-2.0-experimental&preserve-view=true), [PreviousButtonCommand](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.previousbuttoncommand?view=windows-app-sdk-2.0-experimental&preserve-view=true), [NextButtonCommand](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.nextbuttoncommand?view=windows-app-sdk-2.0-experimental&preserve-view=true), or [LastButtonCommand](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.lastbuttoncommand?view=windows-app-sdk-2.0-experimental&preserve-view=true) property, the control then executes that command with a `null` parameter. To change the appearance of the buttons, use the [FirstButtonStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.firstbuttonstyle?view=windows-app-sdk-2.0-experimental&preserve-view=true), [PreviousButtonStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.previousbuttonstyle?view=windows-app-sdk-2.0-experimental&preserve-view=true), [NextButtonStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.nextbuttonstyle?view=windows-app-sdk-2.0-experimental&preserve-view=true), and [LastButtonStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.lastbuttonstyle?view=windows-app-sdk-2.0-experimental&preserve-view=true) properties.

## Handle page changes

PagerControl raises the [SelectedIndexChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.selectedindexchanged?view=windows-app-sdk-2.0-experimental&preserve-view=true) event whenever SelectedPageIndex changes, whether the user selects a page or your code sets the property. The [PagerControlSelectedIndexChangedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrolselectedindexchangedeventargs?view=windows-app-sdk-2.0-experimental&preserve-view=true) object provides the zero-based [NewPageIndex](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrolselectedindexchangedeventargs.newpageindex?view=windows-app-sdk-2.0-experimental&preserve-view=true) and [PreviousPageIndex](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrolselectedindexchangedeventargs.previouspageindex?view=windows-app-sdk-2.0-experimental&preserve-view=true) values.

The following example shows 20 search results per page in a ListView and updates the list when the user selects another page.

```xaml
<Grid RowSpacing="12">
    <Grid.RowDefinitions>
        <RowDefinition Height="*" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    <ListView x:Name="ResultsList" />
    <PagerControl
        x:Name="ResultsPager"
        Grid.Row="1"
        HorizontalAlignment="Center"
        AutomationProperties.Name="Search results pages"
        SelectedIndexChanged="ResultsPager_SelectedIndexChanged" />
</Grid>
```

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;

public sealed partial class SearchResultsPage : Page
{
    private const int PageSize = 20;

    // Sample data. In a real app, results typically come from a search service.
    private readonly List<string> _results =
        Enumerable.Range(1, 235).Select(i => $"Result {i}").ToList();

    public SearchResultsPage()
    {
        InitializeComponent();

        // 235 results at 20 per page require 12 pages.
        ResultsPager.NumberOfPages = (int)Math.Ceiling(_results.Count / (double)PageSize);
        ShowPage(0);
    }

    private void ResultsPager_SelectedIndexChanged(
        PagerControl sender,
        PagerControlSelectedIndexChangedEventArgs args)
    {
        ShowPage(args.NewPageIndex);
    }

    private void ShowPage(int pageIndex)
    {
        // Show only the results that belong to the selected page.
        ResultsList.ItemsSource = _results
            .Skip(pageIndex * PageSize)
            .Take(PageSize)
            .ToList();
    }
}
```

The control also raises SelectedIndexChanged when it applies its template, with PreviousPageIndex set to -1. Make sure that your handler can run before the user interacts with the control.

When the number of pages changes, for example after the user filters the results, update NumberOfPages and set SelectedPageIndex to a valid page, such as 0 for the first page.

## Show an unknown number of pages

If you don't know how many pages there are, set NumberOfPages to -1. For example, a web service might return one page of results at a time without a total count. In this mode, the control doesn't show the total number of pages or the last page button, and the next page button stays enabled.

```xaml
<PagerControl
    AutomationProperties.Name="Activity feed pages"
    DisplayMode="NumberBox"
    NumberOfPages="-1" />
```

:::image type="content" source="images/pager-control/unknown-number-of-pages.png" alt-text="A PagerControl with disabled first page and previous page buttons, the text Page, a number box that contains 1, and a next page button. It doesn't show a total number of pages or a last page button.":::

When you learn the total number of pages, set NumberOfPages to that value.

## Customize the text

In the **Auto**, **ComboBox**, and **NumberBox** display modes, the control shows text before and after the page selector, followed by the total number of pages. By default, the [PrefixText](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.prefixtext?view=windows-app-sdk-2.0-experimental&preserve-view=true) property is **Page** and the [SuffixText](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol.suffixtext?view=windows-app-sdk-2.0-experimental&preserve-view=true) property is **of**, localized for the user's language. If you set these properties, provide localized strings. For more info, see [Localize strings in your UI and app package manifest](../../../windows-app-sdk/mrtcore/localize-strings.md).

## Make a PagerControl accessible

PagerControl provides the following accessibility support by default:

- The default style sets the [AutomationProperties.LandmarkType](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.landmarktype) attached property to **Navigation**, so screen reader users can move to the pager by using landmark navigation.
- The navigation buttons have localized accessible names and tooltips: **First page**, **Previous page**, **Next page**, and **Last page**.
- In **ButtonPanel** mode, each page button has an accessible name such as **Page 3** and reports its position in the set of pages. In the other display modes, the combo box or number box has the accessible name **Page**.
- Users can press the Left and Right arrow keys, or press left and right on a gamepad's directional pad, to move focus between the elements of the control.

To make the control easier to use with assistive technologies:

- Set [AutomationProperties.Name](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.name) to describe the content that the pager controls, for example, "Search results pages". This is especially important when a page has more than one pager.
- Show the current page number in your content, for example, in a heading. In the experimental release, the control's automation peer, [PagerControlAutomationPeer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.pagercontrolautomationpeer?view=windows-app-sdk-2.0-experimental&preserve-view=true), supports the Selection pattern but doesn't report a selected item.

For more info, see [Accessibility overview](../../../design/accessibility/accessibility-overview.md) and [Landmarks and headings](../../../design/accessibility/landmarks-and-headings.md).

## See also

- [PagerControl class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pagercontrol?view=windows-app-sdk-2.0-experimental&preserve-view=true)
- [PipsPager](pipspager.md)
- [ListView and GridView](listview-and-gridview.md)
- [ItemsRepeater](items-repeater.md)
- [Windows App SDK 2.0 Experimental 1 release notes](../../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-20-experimental-1-200-experimental1)
