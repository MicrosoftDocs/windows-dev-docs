---
title: IInvokableCommand.Invoke(Object) Method
description: 
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IInvokableCommand.Invoke(Object) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The method called when a user selects a command.

## Parameters

`sender` Object
Represents the context of where the command was invoked from. This can be different types depending on where the command is being used:

- [TopLevelCommands](icommandprovider_toplevelcommands.md) (and fallbacks): `sender` is the [ICommandItem](icommanditem.md) for the top-level command that was invoked
- [IListPage.GetItems()](ilistpage_getitems.md): `sender` is the [IListItem](ilistitem.md) for the list item selected for that command
- [ICommandItem.MoreCommands](icommanditem.md) (context menus): `sender` is either the [IListItem](ilistitem.md) which the command was attached to for a list page or the [ICommandItem](icommanditem.md) of the top-level command (if this is a context item on a top-level command)
- [IContentPage.Commands](icontentpage.md): `sender` is the [IContentPage](icontentpage.md) itself

## Returns

[ICommandResult](icommandresult.md)

## Samples

[Add a command](../samples.md#add-a-command)
