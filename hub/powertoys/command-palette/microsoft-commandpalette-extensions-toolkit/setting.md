---
title: Setting<T> Class
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Setting<T> Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [ISettingsForm](isettingsform.md)

## Constructors

| Constructor | Description |
| :--- | :--- |
| [Setting()](setting_constructor.md#setting-constructor) | |
| [Setting(String, T)](setting_constructor.md#settingstring-t-constructor) | |
| [Setting(String, String, String, T)](setting_constructor.md#settingstring-string-string-t-constructor) | |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Description | String | Gets or sets the description of the setting. |
| ErrorMessage | String | Gets or sets the text of the error message. |
| IsRequired | Boolean | Gets or sets whether the setting is required. |
| Key | String | Gets the key of the setting. |
| Label | String | Gets or sets the label of the setting. |
| Value | T | Gets or sets the value of the setting. |

## Methods

| Method | Description |
| :--- | :--- |
| [ToDataIdentifier()](setting_todataidentifier.md) | |
| [ToDictionary()](setting_todictionary.md) | |
| [ToForm()](setting_toform.md) | |
| [ToState()](setting_tostate.md) | |
| [Update(JsonObject)](setting_update.md) | |
