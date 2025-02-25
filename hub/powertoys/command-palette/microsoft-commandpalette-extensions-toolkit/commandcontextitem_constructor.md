---
title: CommandContextItem Constructors
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandContextItem Constructors

## CommandContextItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [CommandContextItem](commandcontextitem.md) class from the base [CommandItem](commanditem.md) class, setting its [Command](commanditem.md#properties) property to `command`. 

```C#
public CommandContextItem(ICommand command)
        : base(command)
    {
    }
```

### Parameters

**`command`** [ICommand](../microsoft-commandpalette-extensions/icommand.md)
