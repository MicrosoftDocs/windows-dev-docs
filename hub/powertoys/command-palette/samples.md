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

For the most up-to-date samples, check out [the samples project on GitHub](https://github.com/microsoft/PowerToys/tree/main/src/modules/cmdpal/ext/SamplePagesExtension). This contains up-to-date samples of everything that's possible with Command Palette.

## Create a command to do something

Create a class that implements [IInvokableCommand](microsoft-commandpalette-extensions/icommand.md) and implement the [Invoke](microsoft-commandpalette-extensions/iinvokablecommand_invoke.md) method. This method will be called when the user selects the command in the Command Palette.

```csharp
class MyCommand : Microsoft.CommandPalette.Extensions.Toolkit.InvokableCommand {
    public class MyCommand()
    {
        Name = "Do it"; // A short name for the command
        Icon = new("\uE945"); // Segoe UI LightningBolt
    }
    
    // Open GitHub in the user's default web browser
    public ICommandResult Invoke() {
        Process.Start(new ProcessStartInfo("https://github.com") { UseShellExecute = true });

        // Hides the Command Palette window, without changing the page that's open
        return CommandResult.Hide();
    }
}
```

## Create a page of commands

The following example shows how to create a page of commands. This page will be shown when the user selects the "Open" command in the Command Palette:

```csharp
using Microsoft.CommandPalette.Extensions.Toolkit;

class MyPage : ListPage {
    public MyPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "My sample extension";
        Name = "Open";
    }
    
    public override IListItem[] GetItems()
    {
        return [
            new ListItem(new OpenUrlCommand("https://github.com"))
            {
                Title = "Open GitHub",
            },
            new ListItem(new OpenUrlCommand("https://learn.microsoft.com"))
            {
                Title = "Open Microsoft Learn",
            },
            new ListItem(new OpenUrlCommand("https://github.com/microsoft/PowerToys"))
            {
                Title = "Open PowerToys on GitHub",
            },
            new ListItem(new CopyTextCommand("Foo bar"))
            {
                Title = "Copy 'Foo bar' to the clipboard",
            },
        ];
    }
}
```

## Icons

Icons using the [IIconInfo](./microsoft-commandpalette-extensions/iiconinfo.md) class can be specified in a number of ways. Here are some examples:

```csharp

using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ExtensionName;

internal sealed class Icons
{
    // Icons can be specified as a Segoe Fluent icon, ...
    internal static IconInfo OpenFile { get; } = new("\uE8E5"); // OpenFile

    // ... or as an emoji, ...
    internal static IconInfo OpenFileEmoji { get; } = new("📂");

    // ... Or as a path to an image file, ...
    internal static IconInfo FileExplorer { get; } = IconHelpers.FromRelativePath("Assets\\FileExplorer.png");

    // ... which can be on a remote server, or svg's, or ...
    internal static IconInfo FileExplorerSvg { get; } = new("https://raw.githubusercontent.com/microsoft/PowerToys/refs/heads/main/src/modules/cmdpal/Exts/Microsoft.CmdPal.Ext.Indexer/Assets/FileExplorer.svg");

    // Or they can be embedded in a exe / dll:
    internal static IconInfo FolderIcon { get; } =  new("%systemroot%\\system32\\system32\\shell32.dll,3");
}
```

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
