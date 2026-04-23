---
title: LogMessage Class
description: The LogMessage class is used to define a log message in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# LogMessage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [ILogMessage](../microsoft-commandpalette-extensions/ilogmessage.md)

The **LogMessage** class is used to define a log message in the command palette. It provides properties to specify the message content and its state.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [LogMessage(String)](logmessage_constructor.md) | Initializes a new instance of the LogMessage class with the specified message. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Message | **String** | The content of the log message. |
| State | [MessageState](../microsoft-commandpalette-extensions/messagestate.md) | The state of the log message. |
