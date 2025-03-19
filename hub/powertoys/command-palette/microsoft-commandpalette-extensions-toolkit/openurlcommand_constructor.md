---
title: OpenUrlCommand Constructors
description: Initializes a new instance of the OpenUrlCommand class with the URL target set to target, its name set to "Open", and an icon added.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OpenUrlCommand Constructors

## OpenUrlCommand(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [OpenUrlCommand](openurlcommand.md) class with the URL target set to *target*, its name set to `Open`, and an icon added.

```C#
public OpenUrlCommand(string target)
    {
        _target = target;
        Name = "Open";
        Icon = new IconInfo("\uE8A7");
    }
```

### Parameters

*target* **String**

The URL to be opened by the command.
