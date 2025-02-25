---
title: CommandItem Constructors
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandItem Constructors

## CommandItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [CommandItem](commanditem.md) class, setting its [Command](commanditem.md#properties) property to `command` and its [Title](commanditem.md#properties) to the `command`'s [Name](../microsoft-commandpalette-extensions/icommand.md#properties). 

```C#
public CommandItem(ICommand command)
    {
        Command = command;
        Title = command.Name;
    }
```

### Parameters

**`command`** [ICommand](../microsoft-commandpalette-extensions/icommand.md)

## CommandItem([ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [CommandItem](commanditem.md) class, setting its [Command](commanditem.md#properties) property to `other`'s [Command](../microsoft-commandpalette-extensions/icommanditem.md#properties), its [Title](commanditem.md#properties) to `other`'s [Title](../microsoft-commandpalette-extensions/icommand.md#properties), its [Subtitle](commanditem.md#properties) to `other`'s [Subtitle](../microsoft-commandpalette-extensions/icommand.md#properties), its [Icon](commanditem.md#properties) to `other`'s [Icon](../microsoft-commandpalette-extensions/icommand.md#properties), and its [MoreCommands](commanditem.md#properties) to `other`'s [MoreCommands](../microsoft-commandpalette-extensions/icommand.md#properties). 

```C#
public CommandItem(ICommandItem other)
    {
        Command = other.Command;
        Title = other.Title;
        Subtitle = other.Subtitle;
        Icon = (IconInfo?)other.Icon;
        MoreCommands = other.MoreCommands;
    }
```

### Parameters

**`other`** [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)
