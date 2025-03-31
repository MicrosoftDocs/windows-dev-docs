---
title: ICommandProvider.TopLevelCommands() Method
description: The TopLevelCommands method is called to get the list of actions that should be shown when the user opens the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider.TopLevelCommands() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

**TopLevelCommands** is the method that Command Palette will call to get the list of actions that should be shown when the user opens it. These are the commands that will allow the user to interact with the rest of your extension. They can be simple actions, or they can be pages that the user can navigate to.

## Returns

An [ICommandItem[]](icommanditem.md) array that contains the commands that should be shown in the Command Palette. The commands will be displayed in the order that they are returned by this method.
