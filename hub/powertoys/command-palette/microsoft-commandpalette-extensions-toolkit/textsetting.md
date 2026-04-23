---
title: TextSetting Class definition
description: The TextSetting class represents a setting that allows users to input text in the PowerToys Command Palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# TextSetting Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Setting\<String\>](setting.md)

The **TextSetting** class represents a setting that allows users to input text. It is a specialized type of setting that can be used in the Command Palette to capture user input in a text format. This class provides properties and methods to manage the text input, including support for multiline input and placeholder text.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [TextSetting(String, String)](textsetting_constructor.md#textsettingstring-string-constructor) | This constructor initializes a new instance of the TextSetting class with a specified name and description. |
| [TextSetting(String, String, String, String)](textsetting_constructor.md#textsettingstring-string-string-string-constructor) | This constructor initializes a new instance of the TextSetting class with a specified name, description, default value, and placeholder text. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Multiline | **Boolean** | Indicates whether the text input should support multiple lines. |
| Placeholder | **String** | The placeholder text that is displayed when the input field is empty. |

## Methods

| Method | Description |
| :--- | :--- |
| [LoadFromJson(JsonObject)](textsetting_loadfromjson.md) | Loads the setting from a JSON object. This method is used to deserialize the setting from a JSON representation. |
| [ToDictionary()](textsetting_todictionary.md) | Converts the setting to a dictionary representation. This method is useful for serializing the setting for storage or transmission. |
| [ToState()](textsetting_tostate.md) | Converts the setting to a state representation. This method is useful for capturing the current state of the setting for storage or transmission. |
| [Update(JsonObject)](textsetting_update.md) | Updates the setting with values from a JSON object. This method is used to apply changes to the setting based on a JSON representation. |
