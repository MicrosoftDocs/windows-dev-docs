---
title: IForm.SubmitForm(String) Method
description: The SubmitForm method submits the form data.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IForm.SubmitForm(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **SubmitForm** method submits the form data. It is used to send the data entered in the Adaptive Card to the Command Palette for processing.

## Parameters

*payload* **String**

The JSON representation of the data to be submitted. This data is typically collected from user input in the form fields.

## Returns

An [ICommandResult](icommandresult.md) object that contains the result of the form submission.
