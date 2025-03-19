---
title: ToggleSetting.LoadFromJson(JsonObject) Method
description: The ToggleSetting.LoadFromJson method loads a toggle setting from a JSON object and returns the loaded ToggleSetting object.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToggleSetting.LoadFromJson(JsonObject) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **LoadFromJson** method loads a toggle setting from a *jsonObject* parameter. This is used to initializing the toggle setting with data that has been serialized in JSON format.

## Parameters

*jsonObject* **JsonObject**

The **JsonObject** that contains the toggle setting data. This object is typically loaded from a JSON file or string.

## Returns

A [ToggleSetting](togglesetting.md) object that represents the loaded toggle setting.
