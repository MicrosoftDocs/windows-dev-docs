---
title: CommandItem Class
description: 
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md) 

Implements [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)

## Constructors

| Constructor | Description |
| :--- | :--- |
| [CommandItem()](commanditem_constructor.md#commanditem-constructor) | |
| [CommandItem(ICommand)](commanditem_constructor.md#commanditemicommand-constructor) | |
| [CommandItem(ICommandItem)](commanditem_constructor.md#commanditemicommanditem-constructor) | |
| [CommandItem(String, String, String, Action, ICommandResult)](commanditem_constructor.md#commanditemstring-string-string-action-icommandresult-constructor) | |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Command | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | |
| Icon | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | |
| MoreCommands | [IContextItem[]](../microsoft-commandpalette-extensions/icontextitem.md) | |
| Subtitle | String | |
| Title | String | |
