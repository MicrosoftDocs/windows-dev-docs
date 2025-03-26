---
title: Adding commands to your extension
description: Learn how to add new commands to your Command Palette extension.
ms.date: 3/23/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Adding commands to your extension

**Previous**: [Creating an extension](creating-an-extension.md). We'll be starting with the project created in that article.

Now that you've created your extension, it's time to add some commands to it.

## Add some commands

We can start by navigating to the `ExtensionNamePage.cs` file. This file is the [ListPage](./microsoft-commandpalette-extensions-toolkit/listpage.md) that will be displayed when the user selects your extension. In there you should see:

```csharp   
public ExtensionNamePage()
{
    Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
    Title = "My sample extension";
    Name = "Open";
}
public override IListItem[] GetItems()
{
    return [
        new ListItem(new NoOpCommand()) { Title = "TODO: Implement your extension here" }
    ];
}
```

Here you can see that we've set the icon for the page, the title, and the name that's shown at the top-level when you have the command selected. The **GetItems** method is where you'll return the list of commands that you want to show on this page. Right now, that's just returning a single command that does nothing. Let's instead try making that command open _this_ page in the user's default web browser.

We can change the implementation of **GetItems** to the following:

```csharp
public override IListItem[] GetItems()
{
    var command = new OpenUrlCommand("https://learn.microsoft.com/windows/powertoys/command-palette/adding-commands");
    return [
        new ListItem(command)
        {
            Title = "Open the Command Palette documentation",
        }
    ];
}
```

Re-deploy your app, run the "reload" command to refresh the extensions in the palette, and head to your extension. You should see that the command now opens the Command Palette documentation. 

The **OpenUrlCommand** is a helper for opening a URL in the user's default web browser. You can also implement an extension however you want. Let's instead make a new command, that shows a **MessageBox**. To do that, we need to create a new class that implements **IInvokableCommand**.

```csharp
using System.Runtime.InteropServices;

namespace ExtensionName;

internal sealed partial class ShowMessageCommand : InvokableCommand
{
    public override string Name => "Show message";
    public override IconInfo Icon => new("\uE8A7");

    public override CommandResult Invoke()
    {
        // 0x00001000 is MB_SYSTEMMODAL, which will display the message box on top of other windows.
        _ = MessageBox(0, "I came from the Command Palette", "What's up?", 0x00001000);
        return CommandResult.KeepOpen();
    }


    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
}
```

Now we can add this command to the list of commands in the `ExtensionNamePage.cs` file:

```csharp
public override IListItem[] GetItems()
{
    var command = new OpenUrlCommand("https://learn.microsoft.com/windows/powertoys/command-palette/creating-an-extension");
    var showMessageCommand = new ShowMessageCommand();
    return [
        new ListItem(command)
        {
            Title = "Open the Command Palette documentation",
        },
        new ListItem(showMessageCommand),
    ];
}
```

Deploy and reload, and presto - a command to show a message box!

> [!TIP]
> At about this point, you'll probably want to initialize a git repo / {other source control method of your choice} for your project. This will make it easier to track changes, and to share your extension with others.
> 
> We recommend using GitHub, as it's easy to collaborate on your extension with others, and get feedback, and share it with the world.

## Adding more pages

So far, we've only worked with commands that "do something". However, you can also add commands that show additional pages withing the Command Palette. There are basically two types of "Commands" in the Palette:
- **IInvokableCommand** - These are commands that *do something*.
- **IPage** - These are commands that *show something*.

Because **IPage** implementations are **ICommand**'s, you can use them anywhere you can use commands. This means you can add them to the top-level list of commands, or to a list of commands on a page, the context menu on an item, etc. 

There are two different kinds of pages you can show:
- [ListPage](./microsoft-commandpalette-extensions-toolkit/listpage.md) - This is a page that shows a list of commands. This is what we've been working with so far.
- [ContentPage](./microsoft-commandpalette-extensions-toolkit/contentpage.md) - This is a page that shows rich content to the user. This allows you to specify abstract content, and let Command Palette worry about rendering the content in a native experience. There are two different types of content supported so far:
  - [Markdown content](./using-markdown-content.md) - This is content that's written in Markdown, and is rendered in the Command Palette. See [MarkdownContent](./microsoft-commandpalette-extensions-toolkit/markdowncontent.md) for details.
  - [Form content](./using-form-pages.md) - This is content that shows a form to the user, and then returns the results of that form to the extension. These are powered by [Adaptive Cards](https://aka.ms/adaptive-cards) This is useful for getting user input, or displaying more complex layouts of information. See [FormContent](./microsoft-commandpalette-extensions-toolkit/formcontent.md) for details.


Start by adding a new page that shows a list of commands. Create a new class that implements **ListPage**:

```csharp
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Linq;

namespace ExtensionName;

internal sealed partial class MySecondPage : ListPage
{
    public MySecondPage()
    {
        Icon = new("\uF147"); // Dial2
        Title = "My second page";
        Name = "Open";
    }

    public override IListItem[] GetItems()
    {
        // Return 100 CopyText commands
        return Enumerable
            .Range(0, 100)
            .Select(i => new ListItem(new CopyTextCommand($"{i}")) 
            {
                Title = $"Copy text {i}" 
            }).ToArray();
    }
}
```

Next, update the `ExtensionNamePage.cs` to include this new page:

```diff
    public override IListItem[] GetItems()
    {
        OpenUrlCommand command = new("https://learn.microsoft.com/windows/powertoys/command-palette/creating-an-extension");
        return [
            new ListItem(command)
            {
                Title = "Open the Command Palette documentation",
            },
            new ListItem(new ShowMessageCommand()),
+            new ListItem(new MySecondPage()) { Title = "My second page", Subtitle = "A second page of commands" },
        ];
    }
```

Deploy, reload, and you should now see a new page in your extension that shows 100 commands that copy a number to the clipboard.

### Next up: [Update a list of commands](update-a-list-of-commands.md)

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
