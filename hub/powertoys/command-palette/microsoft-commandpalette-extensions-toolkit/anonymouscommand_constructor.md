---
title: AnonymousCommand Constructors
description: 
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# AnonymousCommand Constructors

## AnonymousCommand(Action) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [AnonymousCommand](anonymouscommand.md) class with an `action`.

```C#
public AnonymousCommand(Action? action)
    {
        Name = "Invoke";
        _action = action;
    }
```

### Parameters

**`action`** Action
