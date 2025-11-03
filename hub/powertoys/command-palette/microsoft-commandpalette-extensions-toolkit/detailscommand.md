---
title: DetailsCommand Class
description: The DetailsCommand class is used to define a command that can be executed in the Command Palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# DetailsCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IDetailsCommand](../microsoft-commandpalette-extensions/idetailscommand.md)

The **DetailsCommand** class is used to define a command that can be executed in the Command Palette. It is a specialized command that provides additional details about the command's execution.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Command | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | The command associated with the details command. This is the command that will be executed when the details command is invoked. |
