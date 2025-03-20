---
title: IExtensionHost.HideStatus(IStatusMessage) Method
description: The HideStatus method is used to hide a status message in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtensionHost.HideStatus(IStatusMessage) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **HideStatus** method is used to hide a status message in the Command Palette. This method is used to remove the status message from the Command Palette, providing a way to clear feedback or notifications that are no longer relevant.

## Parameters

*message* [IStatusMessage](istatusmessage.md)

The status message to be hidden in the Command Palette. This parameter is used to define the content of the status message that should be removed.

## Returns

A **Windows.Foundation.IAsyncAction** object that represents the asynchronous operation. This method does not return a value, but it can be awaited to ensure that the status message is hidden before proceeding with other operations.
