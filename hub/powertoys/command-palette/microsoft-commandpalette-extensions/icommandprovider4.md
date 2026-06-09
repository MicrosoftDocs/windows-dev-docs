---
title: ICommandProvider4 Interface
description: The ICommandProvider4 interface extends ICommandProvider3 to add support for retrieving command items by ID.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider4 Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ICommandProvider4** interface extends [ICommandProvider3](icommandprovider3.md) to add support for retrieving command items by ID. This interface requires [ICommandProvider3](icommandprovider3.md). This enables users to pin nested commands (not just top-level ones) to the Dock.

## Methods

| Method | Return type | Description |
| :--- | :--- | :--- |
| GetCommandItem(String id) | [ICommandItem](icommanditem.md) | Retrieves a specific command item by its ID. This enables users to pin nested commands (not just top-level ones) to the Dock. |
