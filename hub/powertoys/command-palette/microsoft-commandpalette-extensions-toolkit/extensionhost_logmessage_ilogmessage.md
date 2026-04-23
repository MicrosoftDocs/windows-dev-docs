---
title: ExtensionHost.LogMessage(ILogMessage) Method
description: The LogMessage method logs a message to the command palette's output window, accepting an ILogMessage object.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ExtensionHost.LogMessage([ILogMessage](../microsoft-commandpalette-extensions/ilogmessage.md)) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **LogMessage** method logs a message to the command palette's output window. This is useful for debugging and providing feedback to the user about the extension's operations.

## Parameters

*message* [ILogMessage](../microsoft-commandpalette-extensions/ilogmessage.md)

The message to be logged, provided as an object implementing **ILogMessage**. This message can contain any relevant information that the developer wants to provide to the user or for debugging purposes.
