---
title: ICommandResult Interface
description: The ICommandResult interface indicates what the Command Palette should do after the command is executed.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandResult Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Indicates what the Command Palette should do after the command is executed. This allows for commands to control the flow of the Command Palette.

Derived [CommandResult](../microsoft-commandpalette-extensions-toolkit/commandresult.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Args | [ICommandResultArgs](icommandresultargs.md) | The arguments for the command result. |
| Kind | [CommandResultKind](commandresultkind.md) | The kind of command result. |
