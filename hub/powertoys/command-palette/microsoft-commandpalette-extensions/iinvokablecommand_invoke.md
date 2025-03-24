---
title: IInvokableCommand.Invoke(Object) Method
description: The method called when a user selects a command.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IInvokableCommand.Invoke(Object) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The method called when a user selects a command.

## Parameters

*sender* **Object**

Represents the context of where the command was invoked from. This can be different types depending on where the command is being used:

- [TopLevelCommands](icommandprovider_toplevelcommands.md) (and fallbacks): *sender* is the [ICommandItem](icommanditem.md) for the top-level command that was invoked.
- [IListPage.GetItems()](ilistpage_getitems.md): *sender* is the [IListItem](ilistitem.md) for the list item selected for that command.
- [ICommandItem.MoreCommands](icommanditem.md) (context menus): *sender* is either the [IListItem](ilistitem.md) which the command was attached to for a list page or the [ICommandItem](icommanditem.md) of the top-level command (if this is a context item on a top-level command).
- [IContentPage.Commands](icontentpage.md): *sender* is the [IContentPage](icontentpage.md) itself.

Using the *sender* parameter can be useful for big lists of items where the actionable information for each item is somewhat the same. One example would be a long list of links. You can implement this as a single [IInvokableCommand](iinvokablecommand.md) that opens a URL based on the *sender* object passed in. Then, each list item would store the URL to open and the title of the link. This creates less overhead for the extension and host to communicate.

## Returns

An [ICommandResult](icommandresult.md) object that represents the result of the command invocation. This object can contain information about the success or failure of the command, as well as any additional data that may be relevant to the command's execution.

## Example

See [Add a command](../adding-commands.md) for a sample of how to implement this method.
