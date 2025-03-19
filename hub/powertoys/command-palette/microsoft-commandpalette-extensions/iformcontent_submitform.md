---
title: IFormContent.SubmitForm(String) Method
description: The SubmitForm method submits the form data. It is used to send the data entered in the form to the Command Palette for processing.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IFormContent.SubmitForm(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **SubmitForm** method submits the form data. It is used to send the data entered in the form to the Command Palette for processing.

## Parameters

*payload* **String**

The JSON representation of the data to be submitted. This data is typically collected from user input in the form fields.

## Returns

An [ICommandResult](icommandresult.md) object that contains the result of the form submission.
