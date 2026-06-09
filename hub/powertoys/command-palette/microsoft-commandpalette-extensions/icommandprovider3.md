---
title: ICommandProvider3 Interface
description: The ICommandProvider3 interface extends ICommandProvider2 to add support for dock bands.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider3 Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ICommandProvider3** interface extends [ICommandProvider2](icommandprovider2.md) to add support for dock bands. This interface requires [ICommandProvider2](icommandprovider2.md).

## Methods

| Method | Return type | Description |
| :--- | :--- | :--- |
| GetDockBands() | [ICommandItem[]](icommanditem.md) | Returns the dock bands provided by this command provider. Dock bands are strips of items that appear on the Dock toolbar. Each [ICommandItem](icommanditem.md) returned is treated as one atomic band. If the command on an item is an [IListPage](ilistpage.md), then all items on that page are rendered as one band. |
