---
title: ICommandProvider2 Interface
description: The ICommandProvider2 interface extends ICommandProvider to add support for extension interface type caching.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider2 Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ICommandProvider2** interface extends [ICommandProvider](icommandprovider.md) to add support for extension interface type caching. This interface requires [ICommandProvider](icommandprovider.md).

## Methods

| Method | Return type | Description |
| :--- | :--- | :--- |
| GetApiExtensionStubs() | **Object[]** | Returns an array of objects that implement extension interfaces for type caching. This is used to manually populate the WinRT type cache with interfaces that might not follow a linear path of `requires`. |
