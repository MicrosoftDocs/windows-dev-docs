---
title: CommandItem Class
description: The CommandItem class represents a command item in the command palette.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md)

The **CommandItem** class represents a command item in the command palette. It encapsulates the properties and behaviors of a command, including its title, subtitle, icon, and associated command logic. This class is used to create and manage command items within the command palette framework.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [CommandItem()](commanditem_constructor.md#commanditem-constructor) | Creates a new instance of the CommandItem class. |
| [CommandItem(ICommand)](commanditem_constructor.md#commanditemicommand-constructor) | Creates a new instance of the CommandItem class with the specified command. |
| [CommandItem(ICommandItem)](commanditem_constructor.md#commanditemicommanditem-constructor) | Creates a new instance of the CommandItem class with the specified command item. |
| [CommandItem(String, String, String, Action, ICommandResult)](commanditem_constructor.md#commanditemstring-string-string-action-icommandresult-constructor) | Creates a new instance of the CommandItem class with the specified title, subtitle, icon, command action, and command result. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Command | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | The command associated with the command item. This property allows access to the command's logic and execution behavior. |
| Icon | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | The icon associated with the command item. This property defines the visual representation of the command in the command palette. |
| MoreCommands | [IContextItem[]](../microsoft-commandpalette-extensions/icontextitem.md) | An array of commands associated with the command item. This property defines the context menu on the bottom right of the Command Palette. |
| Subtitle | **String** | The subtitle of the command item. This property provides additional context or information about the command, enhancing the user experience. |
| Title | **String** | The title of the command item. This property represents the primary label or name of the command, displayed in the command palette. |
