---
title: FallbackCommandItem Constructors
description: 
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# FallbackCommandItem Constructors

## FallbackCommandItem([ICommand](../microsoft-commandpalette-extensions/icommand.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [FallbackCommandItem](fallbackcommanditem.md) class with the provided `command` and sets its [_fallbackHandler](fallbackcommanditem.md#properties) property if `command` implements [IFallbackHandler](../microsoft-commandpalette-extensions/ifallbackhandler.md).

```C#
public FallbackCommandItem(ICommand command)
        : base(command)
    {
        if (command is IFallbackHandler f)
        {
            _fallbackHandler = f;
        }
    }
```

### Parameters

**`command`** [ICommand](../microsoft-commandpalette-extensions/icommand.md)
