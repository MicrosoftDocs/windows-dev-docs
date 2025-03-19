---
title: ILogMessage Interface
description: The ILogMessage interface represents a log message in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ILogMessage Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ILogMessage** interface represents a log message in the Command Palette. It is used to provide information about the state of the application or specific operations, and can be used for debugging or informational purposes.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Message | **String** | The message text that describes the log entry. |
| State | [MessageState](messagestate.md) | The state of the log message. This property indicates the severity or type of the log message, such as informational, warning, or error. |
