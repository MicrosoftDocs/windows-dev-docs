---
title: ISettingsForm Interface
description: The ISettingsForm interface is used to represent a form in the Command Palette settings.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ISettingsForm Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ISettingsForm** interface is used to represent a form in the Command Palette settings. It provides methods for converting the form to different representations, such as a data identifier, dictionary, or state.

## Methods

| Method | Description |
| :--- | :--- |
| [ToDataIdentifier()](isettingsform_todataidentifier.md) | Converts the settings form to a data identifier. |
| [ToDictionary()](isettingsform_todictionary.md) | Converts the settings form to a dictionary representation. |
| [ToForm()](isettingsform_toform.md) | Converts the settings form to a form representation. |
| [ToState()](isettingsform_tostate.md) | Converts the settings form to a state representation. |
| [Update(JsonObject)](isettingsform_update.md) | Updates the settings form with the specified JSON object. |
