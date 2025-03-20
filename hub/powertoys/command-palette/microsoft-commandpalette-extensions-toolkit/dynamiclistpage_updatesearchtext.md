---
title: DynamicListPage.UpdateSearchText(String, String) Method
description: The UpdateSearchText method is used to update the search text for the dynamic list page.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# DynamicListPage.UpdateSearchText(String, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **UpdateSearchText** method is used to update the search text for the dynamic list page. This allows the extension to modify the search criteria dynamically based on user interactions or other events.

## Parameters

*oldSearch* **String**

The previous search text that was used for filtering the list items. This parameter is useful for determining what has changed in the search criteria.

*newSearch* **String**

The new search text that will be used for filtering the list items. This parameter represents the updated search criteria that should be applied to the list of items displayed in the command palette.
