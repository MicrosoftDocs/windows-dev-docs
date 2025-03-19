---
title: CommandItem Constructors
description: Initializes a new instance of the CommandItem class.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandItem Constructors

## CommandItem() Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CommandItem](commanditem.md) class.

```C#
public CommandItem()
        : this(new NoOpCommand())
    {
    }
```

## CommandItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CommandItem](commanditem.md) class, setting its [Command](commanditem.md#properties) property to *command* and its [Title](commanditem.md#properties) to the *command*'s [Name](../microsoft-commandpalette-extensions/icommand.md#properties).

```C#
public CommandItem(ICommand command)
    {
        Command = command;
        Title = command.Name;
    }
```

### Parameters

*command* [ICommand](../microsoft-commandpalette-extensions/icommand.md)

The command associated with the command item. This property allows access to the command's logic and execution behavior.

## CommandItem([ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CommandItem](commanditem.md) class, setting its [Command](commanditem.md#properties) property to *other*'s [Command](../microsoft-commandpalette-extensions/icommanditem.md#properties), its [Title](commanditem.md#properties) to *other*'s [Title](../microsoft-commandpalette-extensions/icommand.md#properties), its [Subtitle](commanditem.md#properties) to *other*'s [Subtitle](../microsoft-commandpalette-extensions/icommand.md#properties), its [Icon](commanditem.md#properties) to *other*'s [Icon](../microsoft-commandpalette-extensions/icommand.md#properties), and its [MoreCommands](commanditem.md#properties) to *other*'s [MoreCommands](../microsoft-commandpalette-extensions/icommand.md#properties). 

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

*other* [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)

The command item to copy. This parameter is used to initialize the new command item with the properties of an existing command item.

## CommandItem(String, String, String, Action, [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CommandItem](commanditem.md) class, setting its [Title](commanditem.md#properties) property to *title*, its [Subtitle](commanditem.md#properties) to *subtitle*, and creates a new [AnonymousCommand](anonymouscommand.md) object with a *name*, *action*, and *result*. 

```C#
 public CommandItem(
        string title,
        string subtitle = "",
        string name = "",
        Action? action = null,
        ICommandResult? result = null)
    {
        var c = new AnonymousCommand(action);
        if (!string.IsNullOrEmpty(name))
        {
            c.Name = name;
        }

        if (result != null)
        {
            c.Result = result;
        }

        Command = c;

        Title = title;
        Subtitle = subtitle;
    }
```

### Parameters

*title* **String**

The title of the command item. This property represents the primary label or name of the command, displayed in the command palette.

*subtitle* **String**

The subtitle of the command item. This property provides additional context or information about the command, enhancing the user experience.

*name* **String**

The name of the command. This property is used to identify the command within the command palette.

*action* **Action**

The action to be performed when the command is executed. This property defines the logic or behavior associated with the command.

*result* [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md)

The result of the command execution. This property provides information about the outcome of the command, such as success or failure, and any relevant data returned by the command.
