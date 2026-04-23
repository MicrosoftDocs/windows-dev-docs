---
title: IStatusMessage Interface
description: The IStatusMessage interface is used to define a status message in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IStatusMessage Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IStatusMessage** interface is used to define a status message in the Command Palette. It provides properties to specify the message text, progress state, and overall state of the message.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Message | **String** | The message text to be displayed in the Command Palette. This property is used to provide information or feedback to the user. |
| Progress | [IProgressState](iprogressstate.md) | The progress state of the message. This property is used to indicate the progress of an operation or task associated with the message. |
| State | [MessageState](messagestate.md) | The overall state of the message. This property is used to categorize the message, such as success, error, or warning. |
