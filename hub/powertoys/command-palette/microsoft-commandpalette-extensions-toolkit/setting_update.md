---
title: Setting.Update(JsonObject) Method
description: The Update method updates the setting with the specified JSON object. This method is used to modify the value of the setting based on the provided JSON object.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Setting.Update(JsonObject) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **Update** method updates the setting with the specified *payload* parameter. This method is used to modify the value of the setting based on the provided **JsonObject**. It must contain a property with the same name as the setting's key, and the value of that property must be of the same type as the setting's value.

## Parameters

*payload* **JsonObject**

The **JsonObject** that contains the new value for the setting. The **JsonObject** must contain a property with the same name as the setting's key, and the value of that property must be of the same type as the setting's value.
