---
title: CopyTextCommand(String) Constructor
description: Initializes the command with a text parameter, sets its name to "Copy", and adds an icon.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CopyTextCommand(String) Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [CopyTextCommand](copytextcommand.md) class with the command text set to the *text* parameter, its name set to "Copy", and an icon added.

```C#
public CopyTextCommand(string text)
    {
        Text = text;
        Name = "Copy";
        Icon = new IconInfo("\uE8C8");
    }
```

### Parameters

*text* **String**

The text to be copied to the clipboard. This parameter is used to set the **Text** property of the command.
