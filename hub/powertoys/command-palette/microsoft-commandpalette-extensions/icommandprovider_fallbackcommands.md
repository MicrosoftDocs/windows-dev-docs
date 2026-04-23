---
title: ICommandProvider.FallbackCommands() Method
description: The FallbackCommands are special items that allow extensions to have dynamic top-level items which respond to the text the user enters on the main page.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider.FallbackCommands() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **FallbackCommands** are special top-level items which allow extensions to have dynamic top-level items which respond to the text the user types on the main list page. These are not shown in the top-level list of commands, but are shown when the user types text in the Command Palette. This allows extensions to provide dynamic commands that are not shown in the top-level list.

## Returns

An [IFallbackCommandItem[]](ifallbackcommanditem.md) that contains the commands that should be shown in the Command Palette. The commands will be displayed in the order that they are returned by this method.
