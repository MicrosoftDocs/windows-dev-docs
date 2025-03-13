---
title: LogMessage Constructors
description: 
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# LogMessage Constructors

## LogMessage(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [LogMessage](logmessage.md) class with a `message`, which defaults to an empty string if no argument is provided.

```C#
public LogMessage(string message = "")
    {
        _message = message;
    }
```

### Parameters

**`message`** String
