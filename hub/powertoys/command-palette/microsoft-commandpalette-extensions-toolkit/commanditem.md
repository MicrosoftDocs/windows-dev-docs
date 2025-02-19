---
title: CommandItem Class
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Inherits [BaseObservable](baseobservable.md) 

Implements [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)

## Constructors

| Constructor | Description |
| :--- | :--- |
| [CommandItem(ICommand)](commanditem_constructor.md) | |
| [CommandItem(ICommandItem)](commanditem_constructor.md) | |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Command | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | |
| Icon | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | |
| MoreCommands | [IContextItem[]](../microsoft-commandpalette-extensions/icontextitem.md) | |
| Subtitle | String | |
| Title | String | |
