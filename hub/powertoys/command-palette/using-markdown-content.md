---
title: Display markdown content
description: Learn how to display markdown in your Command Palette extension.
ms.date: 3/23/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Display markdown content

**Previous**: [Command Results](command-results.md)

So far, we've only shown how to display a list of commands in a **ListPage**. However, you can also display rich content in your extension, such as markdown. This can be useful for showing documentation, or a preview of a document.

## Working with markdown content

[IContentPage](./microsoft-commandpalette-extensions/icontentpage.md) (and its toolkit implementation, [ContentPage](microsoft-commandpalette-extensions-toolkit/contentpage.md)) is the base for displaying all types of rich content in the Command Palette. To display markdown content, you can use the [MarkdownContent](microsoft-commandpalette-extensions-toolkit/markdowncontent.md) class.

As a simple example, we can create the following page:

```csharp
public class MarkdownPage : ContentPage
{
    public MarkdownPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "Markdown page";
    }

    public override IContent[] GetContent()
    {
        return [
            new MarkdownContent("# Hello, world!\n This is a **markdown** page."),
        ];
    }
}
```

In this example, a new **MarkdownPage** that displays a simple markdown string is created. The **MarkdownContent** class takes a string of markdown content and renders it in the Command Palette. 

You can also add multiple blocks of content to a page. For example, you can add two blocks of markdown content:

```csharp
public override IContent[] GetContent()
{
    return [
        new MarkdownContent("# Hello, world!\n This is a **markdown** page."),
        new MarkdownContent("## Second block\n This is another block of content."),
    ];
}
```

This allows you to mix-and-match different types of content on a single page.

## Adding commands

You can also add commands to a **ContentPage**. This allows you to add additional commands to be invoked by the user, while in the context of the content. For example, if you had a page that displayed a document, you could add a command to open the document in File Explorer:

```csharp 

public class MarkdownExamplePage : ContentPage
{
    public MarkdownExamplePage()
    {
        Icon = new("\uE8A5"); // Document icon
        Title = "Markdown page";
        Name = "Preview file";

        Commands = [
            new CommandContextItem(new OpenUrlCommand("C:\\Path\\to\\file.txt")) { Title = "Open in File Explorer" },
        ];
    }
    public override IContent[] GetContent()
    {
        return [
            new MarkdownContent("# Hello, world!\n This is a **markdown** document.\nI live at `C:\\Path\\to\\file.txt`"),
        ];
    }
}
```

### Next up: [Get user input with forms](using-form-pages.md)

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
