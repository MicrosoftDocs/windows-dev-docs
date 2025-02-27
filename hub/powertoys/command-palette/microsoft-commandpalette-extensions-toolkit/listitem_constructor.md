---
title: ListItem Constructors
description: 
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListItem Constructors

## ListItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ListItem](listitem.md) class with an [ICommand](../microsoft-commandpalette-extensions/icommand.md) instance.

```C#
public ListItem(ICommand command)
        : base(command)
    {
    }
```

### Parameters

**`command`** [ICommand](../microsoft-commandpalette-extensions/icommand.md)

## ListItem([ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ListItem](listitem.md) class with an [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md) instance.

```C#
public ListItem(ICommandItem command)
        : base(command)
    {
    }
```

### Parameters

**`command`** [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)
