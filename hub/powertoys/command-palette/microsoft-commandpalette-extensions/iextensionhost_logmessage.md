---
title: IExtensionHost.LogMessage(ILogMessage) Method
description: The LogMessage method is used to log a message to the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtensionHost.LogMessage(ILogMessage) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **LogMessage** method is used to log a message to the Command Palette. This method is used to provide logging functionality for extensions in the Command Palette.

## Parameters

*message* [ILogMessage](ilogmessage.md)

The message to be logged in the Command Palette. This parameter is used to define the content of the log message.

## Returns

A **Windows.Foundation.IAsyncAction** object that represents the asynchronous operation. This method does not return a value, but it can be awaited to ensure that the log message is processed before proceeding with other operations.
