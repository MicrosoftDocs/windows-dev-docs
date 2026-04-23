---
title: IFormContent Interface
description: The IFormContent interface represents the content of a form in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IFormContent Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IFormContent** interface represents the content of a form in the Command Palette. It is used to define the properties and methods for creating and managing form content, such as displaying data, handling user input, and validating data.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| DataJson | **String** | The JSON representation of the data associated with the form content. |
| StateJson | **String** | The JSON representation of the state associated with the form content. |
| TemplateJson | **String** | The JSON representation of the template associated with the form content. |

## Methods

| Method | Description |
| :--- | :--- |
| [SubmitForm(String)](iformcontent_submitform.md) | Submits the form data. |
