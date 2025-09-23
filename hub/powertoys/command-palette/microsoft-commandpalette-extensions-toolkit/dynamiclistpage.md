---
title: DynamicListPage Class
description: The DynamicListPage class allows for dynamic updates to the list of items displayed in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# DynamicListPage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [ListPage](listpage.md)

Implements [IDynamicListPage](../microsoft-commandpalette-extensions/idynamiclistpage.md)

The **DynamicListPage** class is a specialized version of the [ListPage](listpage.md) class that allows for dynamic updates to the list of items displayed in the command palette. This class is useful for scenarios where the list of items may change frequently or needs to be updated based on user interactions or other events.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| SearchText | **String** | Overrides the [SearchText](listpage.md#properties) property of the [ListPage](listpage.md) class. |

## Methods

| Method | Description |
| :--- | :--- |
| [UpdateSearchText(String, String)](dynamiclistpage_updatesearchtext.md) | Updates the search text for the dynamic list page. |

## Example

The following example demonstrates how to create a custom dynamic list page by inheriting from the **DynamicListPage** class.

```csharp
internal sealed partial class ServicesListPage : DynamicListPage
{
    public ServicesListPage()
    {
        Icon = Icons.ServicesIcon;
        Name = "Windows Services";

        var filters = new ServiceFilters();
        filters.PropChanged += Filters_PropChanged;
        Filters = filters;
    }

    private void Filters_PropChanged(object sender, IPropChangedEventArgs args) => RaiseItemsChanged();

    public override void UpdateSearchText(string oldSearch, string newSearch) => RaiseItemsChanged();

    public override IListItem[] GetItems()
    {
       // ServiceHelper.Search knows how to filter based on the CurrentFilterIds provided
        var items = ServiceHelper.Search(SearchText, Filters.CurrentFilterIds).ToArray();

        return items;
    }
}
```

The ServicesListPage utilizes a custom filter class **ServiceFilters** that inherits from the [Filters](filters.md) class to provide filtering capabilities for the list of services displayed in the command palette.
