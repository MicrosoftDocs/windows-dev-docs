---
title: IExtensionHost.ShowStatus(IStatusMessage) Method
description: The ShowStatus method is used to display a status message in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtensionHost.ShowStatus(IStatusMessage) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ShowStatus** method is used to display a status message in the Command Palette. This method is used to provide feedback to the user about the current state of the Command Palette or the extension.

## Parameters

*message* [IStatusMessage](istatusmessage.md)

The status message to be displayed in the Command Palette. This parameter is used to define the content of the status message.

## Returns

A **Windows.Foundation.IAsyncAction** object that represents the asynchronous operation. This method does not return a value, but it can be awaited to ensure that the status message is displayed before proceeding with other operations.
