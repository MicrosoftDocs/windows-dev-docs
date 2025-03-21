---
title: Command Palette Extension Samples
description: The Command Palette provides a full extension model, allowing you to create custom experiences for the palette. Find samples to start creating an extension.
ms.date: 2/7/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to see samples of Command Palette extensions so I can learn how to write one.
---

# Extension samples

The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. 

For the most up-to-date samples, check out [the samples project on GitHub](https://github.com/microsoft/PowerToys/tree/main/src/modules/cmdpal/Exts/SamplePagesExtension). This contains up-to-date samples of everything that's possible with Command Palette.

## Create a command to do something

Create a class that implements [IInvokableCommand](./microsoft-commandpalette-extensions/icommand.md) and implement the [Invoke](./microsoft-commandpalette-extensions/iinvokablecommand_invoke.md) method. This method will be called whtn the user selects the command in the Command Palette.

```csharp
class MyCommand : Microsoft.CommandPalette.Extensions.Toolkit.InvokableCommand {
    public class MyCommand()
    {
        Name = "Do it"; // A short name for the command
        Icon = new("\uE945"); // Segoe UI LightningBolt
    }
    
    // Open MY_WEBSITE_URL in the user's default web browser
    public ICommandResult Invoke() {
        Process.Start(new ProcessStartInfo("MY_WEBSITE_URL") { UseShellExecute = true });

        // Hides the Command Palette window, without changing the page that's open
        return CommandResult.Hide();
    }
}
```

## Create a page of commands

TODO!

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
