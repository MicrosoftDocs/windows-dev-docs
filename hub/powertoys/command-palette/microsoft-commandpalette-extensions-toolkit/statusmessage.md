---
title: StatusMessage Class
description: The StatusMessage class is used to represent a status message in the PowerToys Command Palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# StatusMessage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [IStatusMessage](../microsoft-commandpalette-extensions/istatusmessage.md)

The **StatusMessage** class is used to represent a status message in the command palette. It provides properties to set the message text, progress state, and message state.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Message | **String** | The text of the message. This property is used to display a message to the user in the command palette. |
| Progress | [IProgressState](../microsoft-commandpalette-extensions/iprogressstate.md) | The progress state of the message. This property is used to indicate the progress of an operation. |
| State | [MessageState](../microsoft-commandpalette-extensions/messagestate.md) | The state of the message. This property is used to indicate the status of the message, such as success or error. |
