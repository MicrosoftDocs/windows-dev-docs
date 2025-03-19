---
title: IExtensionHost Interface
description: The IExtensionHost interface is used to provide a host for extensions in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtensionHost Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IExtensionHost** interface is used to provide a host for extensions in the Command Palette. It is responsible for managing the lifecycle of extensions, including loading and unloading them, as well as providing access to shared resources and services.

This is an object which extensions shouldn't implement themselves. Rather, this is implemented by the host app itself.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| HostingHwnd | **UInt64** | The handle to the hosting window. This property is used to provide access to the window that hosts the Command Palette. |
| LanguageOverride | **String** | The language override for the Command Palette. This property is used to define the language used for localization in the Command Palette. |

## Methods

| Method | Description |
| :--- | :--- |
| [HideStatus(IStatusMessage)](iextensionhost_hidestatus.md) | Hides the status message in the Command Palette. This method is used to remove the status message from the Command Palette. |
| [LogMessage(ILogMessage)](iextensionhost_logmessage.md) | Logs a message to the Command Palette. This method is used to provide logging functionality for extensions in the Command Palette. |
| [ShowStatus(IStatusMessage)](iextensionhost_showstatus.md) | Shows a status message in the Command Palette. This method is used to display a status message in the Command Palette. |
