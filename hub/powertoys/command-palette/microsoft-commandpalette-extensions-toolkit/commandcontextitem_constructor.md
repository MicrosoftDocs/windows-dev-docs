---
title: CommandContextItem Constructors
description: Initializes a new instance of the CommandContextItem class.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandContextItem Constructors

## CommandContextItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CommandContextItem](commandcontextitem.md) class from the base [CommandItem](commanditem.md) class, setting its [Command](commanditem.md#properties) property to *command*.

```C#
public CommandContextItem(ICommand command)
        : base(command)
    {
    }
```

### Parameters

*command* [ICommand](../microsoft-commandpalette-extensions/icommand.md)

The command to be associated with the command item. This parameter is required and cannot be null.

## CommandContextItem(String, String, String, Action, [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CommandContextItem](commandcontextitem.md) class from the base [CommandItem](commanditem.md) class, setting its [Title](commanditem.md#properties) property to *title*, its [Subtitle](commanditem.md#properties) to *subtitle*, and creates a new [AnonymousCommand](anonymouscommand.md) object with a *name*, *action*, and *result*.

```C#
public CommandContextItem(
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

The title of the command item.

*subtitle* **String**

The subtitle of the command item.

*name* **String**

The name of the command item.

*action* **Action**

The action to be performed when the command item is executed.

*result* [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md)

The result of the command item execution. This parameter is optional and can be `null`.
