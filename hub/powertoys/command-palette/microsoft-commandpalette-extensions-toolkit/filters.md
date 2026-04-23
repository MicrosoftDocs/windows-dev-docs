---
title: Filters Class - Command Palette Extensions Toolkit
description: The Filters class is used to get and manage the filters applied to the command palette.
ms.date: 09/04/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Filters Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements **BaseObservable**, [IFilters](../microsoft-commandpalette-extensions/ifilters.md)

The **Filters** class is used to get and manage the filters applied to the command palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| CurrentFilterId | **String** | The ID of the currently applied filter. |

## Methods

| Method | Description | Returns |
| :--- | :--- | :--- |
| GetFilters | Gets the list of filters. This method should be overridden in derived classes to provide the actual filters. | **IFilterItem\[\]** |

## Example

The following example demonstrates how to create a custom filter class by inheriting from the **Filters** class.

```csharp
public partial class ServiceFilters : Filters
{
    // Constructor where CurrentFilterId is set to the default filter
    public ServiceFilters()
    {
        // This would be a default selection. Not providing this will cause the filter
        // control to display the "Filter" placeholder text.
        CurrentFilterId = "all";
    }

    // Override GetFilters method to provide custom filters
    public override IFilterItem[] GetFilters()
    {
        return [
            new Filter() { Id = "all", Name = "All Services" },
            new Separator(),
            new Filter() { Id = "running", Name = "Running", Icon = Icons.GreenCircleIcon },
            new Filter() { Id = "stopped", Name = "Stopped", Icon = Icons.RedCircleIcon },
            new Filter() { Id = "paused", Name = "Paused", Icon = Icons.PauseIcon },
        ];
    }
}
```
