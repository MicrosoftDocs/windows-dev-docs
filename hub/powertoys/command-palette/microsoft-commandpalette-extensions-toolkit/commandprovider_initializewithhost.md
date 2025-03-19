---
title: CommandProvider.InitializeWithHost(IExtensionHost) Method
description: The InitializeWithHost method initializes the command provider with the specified host instance.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandProvider.InitializeWithHost([IExtensionHost](../microsoft-commandpalette-extensions/iextensionhost.md)) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **InitializeWithHost** method initializes the command provider with the specified host instance. This method is typically called when the command provider is being registered with the command palette, allowing it to access the host's services and capabilities.

## Parameters

*host* [IExtensionHost](../microsoft-commandpalette-extensions/iextensionhost.md)

The host instance that provides access to the command palette's functionality. This parameter is used to initialize the command provider with the host's capabilities and services.
