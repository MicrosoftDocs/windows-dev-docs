---
title: Settings Class definition
description: The Settings class is used to manage the settings of a command palette extension. It provides methods to add, retrieve, and update settings.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Settings Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [ICommandSettings](../microsoft-commandpalette-extensions/icommandsettings.md)

The **Settings** class is used to manage the settings of a command palette extension. It provides methods to add, retrieve, and update settings, as well as to convert settings to JSON format.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| SettingsPage | [IContentPage](../microsoft-commandpalette-extensions/icontentpage.md) | The settings page associated with the command palette extension. |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler\<object, Settings\> SettingsChanged | Occurs when the settings of the command palette extension are changed. |

## Methods

| Method | Description |
| :--- | :--- |
| [Add\<T\>(Setting\<T\>)](settings_add.md) | Adds a new setting to the command palette extension. |
| [GetSetting\<T\>(String)](settings_getsetting.md) | Retrieves the value of a setting by its name. |
| [ToContent()](settings_tocontent.md) | Converts the settings to a content page. |
| [ToJson()](settings_tojson.md) | Converts the settings to JSON format. |
| [TryGetSetting\<T\>(String, T)](settings_trygetsetting.md) | Attempts to retrieve the value of a setting by its name. If the setting does not exist, it returns the default value provided. |
| [Update(String)](settings_update.md) | Updates the settings of the command palette extension. |
