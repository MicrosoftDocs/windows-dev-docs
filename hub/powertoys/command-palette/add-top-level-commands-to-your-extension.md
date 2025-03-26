---
title: Add top-level commands to your extension
description: Learn how to add new top-level commands to your Command Palette extension.
ms.date: 3/23/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Adding top-level commands to your extension

**Previous**: [Update a list of commands](update-a-list-of-commands.md).

So far, you've only added commands to a single page within your extension. You can also add more commands directly to the top-level list of commands too. 

## Adding the top-level commands

To do that, head on over to the `ExtensionNameCommandsProvider.cs` file. This file is where you'll add commands that should be shown at the top-level of the Command Palette. As you can see, there's currently only a single item there:

```csharp
public ExtensionNameCommandsProvider()
{
    DisplayName = "My sample extension";
    Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
    _commands = [
        new CommandItem(new ExtensionNamePage()) { Title = DisplayName },
    ];
}

public override ICommandItem[] TopLevelCommands()
{
    return _commands;
}
```

This sample extension creates a list of commands when the extension is created and returns that list whenever it's asked for the top-level commands. This prevents the extension from re-creating the list of commands every time the top-level commands are requested. This is a performance optimization.

If you want to add another command to the top-level list of commands, you can add another **CommandItem**:

```csharp
public ExtensionNameCommandsProvider()
{
    DisplayName = "My sample extension";
    Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
    _commands = [
        new CommandItem(new ExtensionNamePage()) { Title = DisplayName },
        new CommandItem(new ShowMessageCommand()) { Title = "Send a message" },
    ];
}
```

There you have it. Now you can add additional top-level commands to your extension.

If you'd like to update the list of top-level commands dynamically, you can do so in the same way as you would update a list page. This can be useful for cases like an extension that might first require the user to log in, before showing certain commands. In that case, you can show the "log in" command at the top level initially. Then, once the user logs in successfully, you can update the list of top-level commands to include the commands that required authentication.

Once you've determined that you need to change the top level list, call [RaiseItemsChanged](./microsoft-commandpalette-extensions-toolkit/commandprovider_raiseitemschanged.md) on your **CommandProvider**. Command Palette will then request the top-level commands via **TopLevelCommands** again, and you can return the updated list.

> [!TIP]
> Create the **CommandItem** objects for the top-level commands before calling **RaiseItemsChanged**. This will ensure that the new commands are available when Command Palette requests the top-level commands. This will ensure that the work being executed in each call to **TopLevelCommands** method to a minimum.

### Next up: [Command Results](command-results.md)

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
