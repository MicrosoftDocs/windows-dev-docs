---
title: ChoiceSetSetting Class
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ChoiceSetSetting Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Inherits [Setting<String>](setting.md)

## Constructors

| Constructor | Description |
| :--- | :--- |
| [ChoiceSetSetting()](choicesetsetting_constructor.md#choicesetsetting-constructor) | Initializes `Choices` to an empty list. |
| [ChoiceSetSetting(String, List<Choice>)](choicesetsetting_constructor.md#choicesetsettingstring-list-constructor) | |
| [ChoiceSetSetting(String, String, String, List<Choice>)](choicesetsetting_constructor.md#choicesetsettingstring-string-string-list-constructor) | |

## Nested classes

| Class | Description |
| :--- | :--- |
| [Choice](choice.md) | |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Choices | List<[Choice](choice.md)> | |

## Methods

| Method | Description |
| :--- | :--- |
| [LoadFromJson(JsonObject)](choicesetsetting_loadfromjson.md) | |
| [ToDictionary()](choicesetsetting_todictionary.md) | |
| [ToState()](choicesetsetting_tostate.md) | |
| [Update(JsonObject)](choicesetsetting_update.md) | |
