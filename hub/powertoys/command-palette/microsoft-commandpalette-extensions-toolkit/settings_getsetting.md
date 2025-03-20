---
title: Settings.GetSetting<T>(String) Method
description: The GetSetting method retrieves the value of a specific setting from the command palette extension.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Settings.GetSetting\<T\>(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **GetSetting** method retrieves the value of a specific setting from the command palette extension. This method is used to access the settings defined in the command palette extension's configuration.

## Parameters

*key* **String**

The key of the setting to retrieve. This should match the key used when the setting was defined in the command palette extension.

## Returns

An object of type **T** representing the value of the setting. The type **T** should match the type of the setting defined in the command palette extension's configuration. If the setting does not exist or cannot be converted to the specified type, an exception may be thrown.
