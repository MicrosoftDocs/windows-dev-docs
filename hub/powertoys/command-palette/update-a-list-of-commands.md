---
title: Update a list of commands
description: Learn how to update the elements in your Command Palette extension.
ms.date: 3/23/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Update a list of commands

**Previous**: [Adding commands](adding-commands.md).

So far we've shown how to return a list of static items in your extension. However, your items can also change, to show real-time data, or to reflect the state of the system. In this article, we'll show you how to update the list of commands in your extension.

## Updating a command

Almost all extension objects in the Command Palette implement the **IPropChanged** interface. This allows them to notify the Command Palette when they've changed, and the Command Palette will update the UI to reflect those changes. If you're using the [toolkit](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/microsoft-commandpalette-extensions-toolkit/microsoft-commandpalette-extensions-toolkit) implementations, this interface has already been implemented for you for properties that support it.

As a simple example, you can update the title of the page. To do this, you can add a command which will simply update the title of the page.

```csharp
public override IListItem[] GetItems()
{
    OpenUrlCommand command = new("https://learn.microsoft.com/windows/powertoys/command-palette/creating-an-extension");

    AnonymousCommand updateCommand = new(action: () => { Title += " Hello"; }) { Result = CommandResult.KeepOpen() };

    return [
        new ListItem(command)
        {
            Title = "Open the Command Palette documentation",
        },
        new ListItem(updateCommand),
    ];
}
```

Here, we're using **AnonymousCommand** to create a command that will update the title of the page. **AnonymousCommand** is a helper that's useful for creating simple, lightweight commands that don't need to be reused.

You can of course create custom **ListItem** objects too:

```csharp
internal sealed partial class IncrementingListItem : ListItem
{
    public IncrementingListItem() :
        base(new NoOpCommand())
    {
        Command = new AnonymousCommand(action: Increment) { Result = CommandResult.KeepOpen() };
        Title = "Increment";
    }

    private void Increment()
    {
        Subtitle = $"Count = {++_count}";
    }

    private int _count;
}
```

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
+            new IncrementingListItem(),
        ];
    }
```

You're on your way to creating your own idle clicker game, as a Command Palette extension.

## Updating the list of commands

You can also change the list of items on the page. This can be useful for pages that load results asynchronously, or for pages that show different commands based on the state of the app.

To do this, you can use the **RaiseItemsChanged** method on the **ListPage** object. This will notify the Command Palette that the list of items has changed, and it should re-fetch the list of items. As an example, let's make the **IncrementingListItem** from above update the list of items on the page.

Update your list item to take a reference to the page, and add a method to increment the count:

```csharp
internal sealed partial class IncrementingListItem : ListItem
{
    public IncrementingListItem(<ExtensionName>Page page) :
        base(new NoOpCommand())
    {
        _page = page;
        Command = new AnonymousCommand(action: _page.Increment) { Result = CommandResult.KeepOpen() };
        Title = "Increment";
    }

    private <ExtensionName>Page _page;
}
```

Then, change your page as follows:

```cs
public <ExtensionName>Page()
{
    Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
    Title = "My sample extension";
    Name = "Open";

    _items = [new IncrementingListItem(this) { Subtitle = $"Item 0" }]; 
}
public override IListItem[] GetItems()
{
    return _items.ToArray();
}
internal void Increment()
{
    _items.Add(new IncrementingListItem(this) { Subtitle = $"Item {_items.Count}" });
    RaiseItemsChanged();
}
private List<ListItem> _items;
```

Now, every time you perform one of the **IncrementingListItem** commands, the list of items on the page will update to add another item. We're using a single **List** owned by the page to own all the items. Notably, we're creating the new items in the **Increment** method, before calling **RaiseItemsChanged**. The **Invoke** of a **IInvokableCommand** can take as long as you'd like. All your code is running in a separate process from the Command Palette, so you won't block the UI. But constructing the items before calling **RaiseItemsChanged** will help keep your extension *feeling* more responsive.

## Showing a loading spinner

Everything so far has been pretty instantaneous. Many extensions however may need to do some work that takes a lot longer. In that case, you can set **Page.IsLoading** to `true` to show a loading spinner. This will help indicate that the extension is doing something in the background.

> [!Note]
> If working from prior section, modify the code below from `Page.IsLoading` to `this.IsLoading`.

```csharp
internal void Increment()
{
    Page.IsLoading = true;
    Task.Run(() =>
    {
        Thread.Sleep(5000);
        _items.Add(new IncrementingListItem(this) { Subtitle = $"Item {_items.Count}" });
        RaiseItemsChanged();
        Page.IsLoading = false;
    });
}
```


Best practice is to set **IsLoading** to `true` before starting the work. Then do all the work to build all the new **ListItems** you need to display to the user. Then, once the items are ready, call **RaiseItemsChanged** and set **IsLoading** back to `false`. This will ensure that the loading spinner is shown for the entire duration of the work, and that the UI is updated as soon as the work is done (without waiting for your extension to construct new **ListItem** objects).

### Next up: [Add top-level commands to your extension](add-top-level-commands-to-your-extension.md)

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
