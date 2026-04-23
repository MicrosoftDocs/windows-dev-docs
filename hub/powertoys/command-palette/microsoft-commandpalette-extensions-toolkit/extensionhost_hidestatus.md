---
title: ExtensionHost.HideStatus(IStatusMessage) Method
description: The HideStatus method hides a status message that was previously displayed in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ExtensionHost.HideStatus([IStatusMessage](../microsoft-commandpalette-extensions/istatusmessage.md)) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **HideStatus** method is used to hide a status message that was previously displayed in the command palette. This is useful for clearing the status area when the operation associated with the status message is complete or no longer relevant.

## Parameters

*message* [IStatusMessage](../microsoft-commandpalette-extensions/istatusmessage.md)

The status message to be hidden, provided as an object implementing **IStatusMessage**. This parameter identifies the specific status message that should be removed from the command palette's status area.
