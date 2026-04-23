---
title: Command Class
description: The Command class represents a command in the Command Palette SDK and is the primary unit of functionality.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Command Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [ICommand](../microsoft-commandpalette-extensions/icommand.md)

`Command`s are the primary unit of functionality in the Command Palette SDK. They represent "a thing that a user can do". These can be something simple like open a URL in a web browser. Or they can be more complex, with nested commands, custom arguments, and more.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Icon | [IconInfo](iconinfo.md) | Gets or sets the icon for the command. |
| Id | **String** | Gets or sets the ID of the command. |
| Name | **String** | Gets or sets the name of the command. |
