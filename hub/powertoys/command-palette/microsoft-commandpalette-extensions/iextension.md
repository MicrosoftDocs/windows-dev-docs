---
title: IExtension Interface
description: The IExtension interface is used to define the properties and methods that an extension must implement in order to be used in the Command Palette.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtension Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IExtension** interface is used to define the properties and methods that an extension must implement in order to be used in the Command Palette.

## Methods

| Method | Description |
| :--- | :--- |
| [Dispose()](iextension_dispose.md) | Disposes of the extension. This method is used to clean up resources and perform any necessary cleanup when the extension is no longer needed. |
| [GetProvider(ProviderType)](iextension_getprovider.md) | Gets the provider type. |
