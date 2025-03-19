---
title: FormContent Class
description: The FormContent class is used to create a form within the Command Palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# FormContent Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [IFormContent](../microsoft-commandpalette-extensions/iformcontent.md)

The **FormContent** class is used to create a form within the Command Palette. It provides properties and methods to manage the form's data, state, and template.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| DataJson | **String** | The JSON representation of the form's data. |
| StateJson | **String** | The JSON representation of the form's state. |
| TemplateJson | **String** | The JSON representation of the form's template. |

## Methods

| Method | Description |
| :--- | :--- |
| [SubmitForm(String)](formcontent_submitform_string.md) | Submits the form with the specified JSON data. |
| [SubmitForm(String, String)](formcontent_submitform_stringstring.md) | Submits the form with the specified JSON data and state. |
