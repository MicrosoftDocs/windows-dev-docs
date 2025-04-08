---
title: ICommandProvider.InitializeWithHost(IExtensionHost) Method
description: The InitializeWithHost method is called when the extension is loaded. This method is used to initialize the extension with the host application.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider.InitializeWithHost(IExtensionHost) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **InitializeWithHost** method is called when the extension is loaded. This method is used to initialize the extension with the host application. The host application provides the extension with a reference to the [IExtensionHost](iextensionhost.md) interface, which allows the extension to interact with the host application.

## Parameters

*host* [IExtensionHost](iextensionhost.md)

The host application that is loading the extension. The host application provides the extension with a reference to the [IExtensionHost](iextensionhost.md) interface, which allows the extension to interact with the host application.
