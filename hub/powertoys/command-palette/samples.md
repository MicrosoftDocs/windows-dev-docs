---
title: Command Palette Extension Samples
description: The Command Palette provides a full extension model, allowing you to create custom experiences for the palette. Find samples to start creating an extension.
ms.date: 2/7/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to see samples of Command Palette extensions so I can learn how to write one.
---

# Extension samples

The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. This document provides information about how to create an extension and publish it. It also includes a sample extension that demonstrates the extensibility model.

## Add a command

Create a class that implements [ICommand](./microsoft-commandpalette-extensions/icommand.md) and implement the [Invoke](./microsoft-commandpalette-extensions/iinvokablecommand_invoke.md) method. This method will be called whtn the user selects the command in the Command Palette.

```csharp
class MyPage : Microsoft.CommandPalette.Extensions.Toolkit.InvokableCommand {
    public class MyPage()
    {
        Name = "My Page Name";
        Icon = "PATH_TO_ICO";
    }
    
    // Open MY_WEBSITE_URL in the user's default web browser
    public ICommandResult Invoke() {
        Process.Start(new ProcessStartInfo("MY_WEBSITE_URL") { UseShellExecute = true });
        return CommandResult.Hide();
    }
}
```

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](creating-an-extension.md)
