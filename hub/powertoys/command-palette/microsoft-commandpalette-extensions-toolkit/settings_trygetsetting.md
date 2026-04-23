---
title: Settings.TryGetSetting<T>(String, T) Method
description: The TryGetSetting method attempts to retrieve a setting value from the command palette extension's settings.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Settings.TryGetSetting\<T\>(String, T) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **TryGetSetting** method attempts to retrieve a setting value from the command palette extension's settings. If the setting is not found, it returns the default value provided.

## Parameters

*key* **String**

The key of the setting to retrieve. This should be the name of the setting as defined in the extension's settings schema.

*val* **T**

The default value to return if the setting is not found. This value is used as a fallback in case the specified setting does not exist in the settings store.

## Returns

A **Boolean** indicating whether the setting was found and successfully retrieved.
