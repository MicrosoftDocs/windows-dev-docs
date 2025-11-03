---
title: ListItem Constructors
description: Initializes a new instance of the ListItem class with the specified command or command item.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListItem Constructors

## ListItem() Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ListItem](listitem.md) class.

```C#
public ListItem()
        : base()
    {
    }
```

## ListItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ListItem](listitem.md) class with an [ICommand](../microsoft-commandpalette-extensions/icommand.md) instance.

```C#
public ListItem(ICommand command)
        : base(command)
    {
    }
```

### Parameters

*command* [ICommand](../microsoft-commandpalette-extensions/icommand.md)

The command associated with the list item. This command is executed when the item is selected in the command palette.

## ListItem([ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ListItem](listitem.md) class with an [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md) instance.

```C#
public ListItem(ICommandItem command)
        : base(command)
    {
    }
```

### Parameters

*command* [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)

The command item associated with the list item. This command item is executed when the item is selected in the command palette.
