---
title: ExtensionHost Class
description: The ExtensionHost class is a base class for creating extension hosts in the Command Palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ExtensionHost Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ExtensionHost** class is a base class for creating extension hosts in the Command Palette. It provides methods for logging messages, showing and hiding status messages, and initializing the extension host with an instance of [IExtensionHost](../microsoft-commandpalette-extensions/iextensionhost.md).

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Host | [IExtensionHost](../microsoft-commandpalette-extensions/iextensionhost.md) | Gets the instance of the extension host that is associated with this extension. This property is used to access the extension host's methods and properties. |

## Methods

| Method | Description |
| :--- | :--- |
| [HideStatus(IStatusMessage)](extensionhost_hidestatus.md) | Hides the status message that is currently being displayed in the command palette. |
| [Initialize(IExtensionHost)](extensionhost_initialize.md) | Initializes the extension host with an instance of [IExtensionHost](../microsoft-commandpalette-extensions/iextensionhost.md). |
| [LogMessage(ILogMessage)](extensionhost_logmessage_ilogmessage.md) | Logs a message to the command palette. |
| [LogMessage(String)](extensionhost_logmessage_string.md) | Logs a message to the command palette. |
| [ShowStatus(IStatusMessage)](extensionhost_showstatus.md) | Shows a status message in the command palette. |
