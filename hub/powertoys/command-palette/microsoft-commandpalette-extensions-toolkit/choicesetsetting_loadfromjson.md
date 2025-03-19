---
title: ChoiceSetSetting.LoadFromJson(JsonObject) Method
description: The LoadFromJson method loads the setting from a JSON object.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ChoiceSetSetting.LoadFromJson(JsonObject) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **LoadFromJson** method loads the setting from a JSON object. This is useful for applications that need to initialize settings from a configuration file or other sources.

## Parameters

*jsonObject* **JsonObject**

The **JsonObject** that contains the values for the setting. This object should include the properties that need to be set, such as the name, description, default value, and list of choices.

## Returns

A [ChoiceSetSetting](choicesetsetting.md) object that represents the loaded setting.
