---
title: IListPage Interface
description: The IListPage interface represents a page in the Command Palette that displays a list of items.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IListPage Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IListPage** interface represents a page in the Command Palette that displays a list of items. It is used to present a collection of items in a structured format, allowing users to interact with and select from the list.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| EmptyContent | [ICommandItem](icommanditem.md) | The content to display when the list is empty. This property can be used to provide a message or action for the user when there are no items to display. |
| Filters | [IFilters](ifilters.md) | The filters applied to the list. This property allows users to refine the items displayed in the list based on specific criteria. |
| GridProperties | [IGridProperties](igridproperties.md) | The properties of the grid layout used to display the list items. This property defines how the items are arranged and presented in the grid. |
| HasMoreItems | **Boolean** | Indicates whether there are more items to load in the list. This property is used to determine if additional items can be fetched or displayed. |
| PlaceholderText | **String** | The text to display when the list is empty or no items match the current filters. This property provides context to the user about the state of the list. |
| SearchText | **String** | The text used to filter the list items. This property allows users to search for specific items within the list. |
| ShowDetails | **Boolean** | Indicates whether to show detailed information about the items in the list. This property can be used to toggle between a summary view and a detailed view of the items. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetItems()](ilistpage_getitems.md) | Retrieves the items to be displayed in the list. This method is used to fetch the data that populates the list. |
| [LoadMore()](ilistpage_loadmore.md) | Loads more items into the list. This method is used to fetch additional data when the user requests more items, such as when scrolling or clicking a "Load More" button. |
