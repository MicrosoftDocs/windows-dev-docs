---
title: AnonymousCommand Constructors
description: Initializes a new instance of the AnonymousCommand class with the specified action.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# AnonymousCommand Constructors

## AnonymousCommand(Action) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [AnonymousCommand](anonymouscommand.md) class with an *action* parameter.

```C#
public AnonymousCommand(Action? action)
    {
        Name = "Invoke";
        _action = action;
    }
```

### Parameters

*action* **Action**

The action to be executed when the command is invoked. This parameter is optional and can be null.
