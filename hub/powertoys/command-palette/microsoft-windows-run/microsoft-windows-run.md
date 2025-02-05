---
title: Microsoft.Windows.Run Namespace
description: The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. Find info about how to use the Microsoft.Windows.Run namespace to author an extension.
ms.date: 2/5/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to use the Command Palette SDK to create an extension.
---

# Microsoft.Windows.Run Namespace

Contains the interfaces to create extensions for the Command Palette.

## Interfaces

| Interface | Description |
| :--- | :--- |
| [ICommand](icommand.md) | Action a user can take within the Command Palette. |
| ICommandResultArgs | |
| ICommandResult | |
| IGoToPageArgs | |
| IInvokableCommand | |

## Enums

| Enum | Description |
| :--- | :--- |
| [CommandResultKind](commandresultkind.md) | Specifies what kind of command it is. |
| [NavigationMode](navigationmode.md) | Specifies which navigation direction to take. |