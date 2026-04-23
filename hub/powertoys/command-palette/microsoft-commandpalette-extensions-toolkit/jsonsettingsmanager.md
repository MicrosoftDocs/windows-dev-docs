---
title: JsonSettingsManager Class
description: The JsonSettingsManager class provides functionality for managing extension settings in JSON format.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# JsonSettingsManager Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **JsonSettingsManager** class provides functionality for managing settings in JSON format. It allows loading and saving settings to a specified file path.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| FilePath | **String** | The file path where the settings are stored. |
| Settings | [Settings](settings.md) | The settings object that contains the configuration data. |

## Methods

| Method | Description |
| :--- | :--- |
| [LoadSettings()](jsonsettingsmanager_loadsettings.md) | Loads settings from the specified file path. |
| [SaveSettings()](jsonsettingsmanager_savesettings.md) | Saves the current settings to the specified file path. |
