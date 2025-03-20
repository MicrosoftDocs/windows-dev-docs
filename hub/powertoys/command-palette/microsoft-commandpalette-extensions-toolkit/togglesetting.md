---
title: ToggleSetting Class
description: The ToggleSetting class represents a toggle setting in the Command Palette Extensions Toolkit and can be used to define settings that can be toggled on or off.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToggleSetting Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Setting\<Boolean\>](setting.md)

The **ToggleSetting** class represents a toggle setting in the Command Palette Extensions Toolkit. It is used to define settings that can be toggled on or off, such as enabling or disabling a feature.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [ToggleSetting(String, Boolean)](togglesetting_constructor.md#togglesettingstring-boolean-constructor) | Creates a new instance of the **ToggleSetting** class with the specified name and default value. |
| [ToggleSetting(String, String, String, Boolean)](togglesetting_constructor.md#togglesettingstring-string-string-boolean-constructor) | Creates a new instance of the **ToggleSetting** class with the specified name, description, category, and default value. |

## Methods

| Method | Description |
| :--- | :--- |
| [LoadFromJson(JsonObject)](togglesetting_loadfromjson.md) | Loads the toggle setting from a JSON object. |
| [ToDictionary()](togglesetting_todictionary.md) | Converts the toggle setting to a dictionary representation. |
| [ToState()](togglesetting_tostate.md) | Converts the toggle setting to a state representation. |
| [Update(JsonObject)](togglesetting_update.md) | Updates the toggle setting with the specified JSON object. |
