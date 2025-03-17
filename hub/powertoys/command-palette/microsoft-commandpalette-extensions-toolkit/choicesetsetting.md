---
title: ChoiceSetSetting Class
description: The ChoiceSetSetting class represents a setting that allows users to select from a predefined set of choices.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ChoiceSetSetting Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Setting\<String\>](setting.md)

The **ChoiceSetSetting** class represents a setting that allows users to select from a predefined set of choices. This is useful for applications that need to provide users with a limited set of options to choose from, such as selecting a theme or a configuration option.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [ChoiceSetSetting(String, List\<Choice\>)](choicesetsetting_constructor.md#choicesetsettingstring-listchoice-constructor) | Initializes a new instance of the **ChoiceSetSetting** class with a specified name and a list of choices. |
| [ChoiceSetSetting(String, String, String, List\<Choice\>)](choicesetsetting_constructor.md#choicesetsettingstring-string-string-listchoice-constructor) | Initializes a new instance of the **ChoiceSetSetting** class with a specified name, description, default value, and a list of choices. |

## Nested classes

| Class | Description |
| :--- | :--- |
| [Choice](choice.md) | Represents a single choice in the choice set. Each choice has a name and a value. The name is displayed to the user, while the value is used internally by the application. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Choices | List\<[Choice](choice.md)\> | Gets or sets the list of choices available in the choice set. Each choice is represented by an instance of the **Choice** class. |

## Methods

| Method | Description |
| :--- | :--- |
| [LoadFromJson(JsonObject)](choicesetsetting_loadfromjson.md) | Loads the setting from a JSON object. This is useful for applications that need to deserialize settings from a configuration file or other JSON source. |
| [ToDictionary()](choicesetsetting_todictionary.md) | Converts the setting to a dictionary representation. This is useful for applications that need to serialize settings to a format that can be easily stored or transmitted. |
| [ToState()](choicesetsetting_tostate.md) | Converts the setting to a state representation. This is useful for applications that need to represent settings in a format that can be easily displayed or manipulated. |
| [Update(JsonObject)](choicesetsetting_update.md) | Updates the setting from a JSON object. This is useful for applications that need to apply changes to settings based on user input or other sources. |
