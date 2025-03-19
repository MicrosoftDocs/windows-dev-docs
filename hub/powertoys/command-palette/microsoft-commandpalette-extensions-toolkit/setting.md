---
title: Setting<T> Class
description: The Setting<T> class represents a setting in the command palette extension. It contains properties and methods to define and manage settings.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Setting\<T\> Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [ISettingsForm](isettingsform.md)

The **Setting\<T\>** class represents a setting in the command palette extension. It contains properties and methods to define and manage settings, including their keys, values, labels, descriptions, and error messages. This class is used to create settings that can be accessed and modified by the command palette extension.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [Setting()](setting_constructor.md#setting-constructor) | The default constructor for the Setting class. |
| [Setting(String, T)](setting_constructor.md#settingstring-t-constructor) | The constructor for the Setting class that takes a key and a value. |
| [Setting(String, String, String, T)](setting_constructor.md#settingstring-string-string-t-constructor) | The constructor for the Setting class that takes a key, label, description, and value. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Description | **String** | Gets or sets the description of the setting. |
| ErrorMessage | **String** | Gets or sets the text of the error message. |
| IsRequired | **Boolean** | Gets or sets whether the setting is required. |
| Key | **String** | Gets the key of the setting. |
| Label | **String** | Gets or sets the label of the setting. |
| Value | **T** | Gets or sets the value of the setting. |

## Methods

| Method | Description |
| :--- | :--- |
| [ToDataIdentifier()](setting_todataidentifier.md) | The **ToDataIdentifier** method converts the setting to a data identifier. |
| [ToDictionary()](setting_todictionary.md) | The **ToDictionary** method converts the setting to a dictionary. |
| [ToForm()](setting_toform.md) | The **ToForm** method converts the setting to a form. |
| [ToState()](setting_tostate.md) | The **ToState** method converts the setting to a state. |
| [Update(JsonObject)](setting_update.md) | The **Update** method updates the setting with the specified JSON object. |
