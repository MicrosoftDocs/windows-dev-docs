---
title: CommandContextItem Class
description: The CommandContextItem class represents a command item in the command palette that is associated with a specific context.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandContextItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [CommandItem](commanditem.md)

Implements [ICommandContextItem](../microsoft-commandpalette-extensions/icommandcontextitem.md)

The **CommandContextItem** class represents a command item in the command palette that is associated with a specific context. It extends the functionality of the **CommandItem** class by adding properties and behaviors related to context-specific commands. This class is used to create and manage command items that are relevant to a particular context within the command palette framework.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [CommandContextItem(ICommand)](commandcontextitem_constructor.md#commandcontextitemicommand-constructor) | Initializes a new instance of the **CommandContextItem** class, setting its **Command** property to *command* and its **Title** to the **Name** of the *command*. |
| [CommandContextItem(String, String, String, Action, ICommandResult)](commandcontextitem_constructor.md#commandcontextitemstring-string-string-action-icommandresult-constructor) | Initializes a new instance of the **CommandContextItem** class, setting its **Title**, **Subtitle**, **Icon**, and **Command** properties. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| IsCritical | **Boolean** | Indicates whether the command item is critical. |
| RequestedShortcut | [KeyChord](../microsoft-commandpalette-extensions/keychord.md) | The requested keyboard shortcut for the command item. |
