---
title: SettingsForm.SubmitForm(String, String) Method
description: The **SubmitForm** method is used to submit the form data to a specified URL using a specified HTTP method.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# SettingsForm.SubmitForm(String, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **SubmitForm** method is used to submit the form data to a specified URL using a specified HTTP method. This method is typically used to send user input from the form to a server or API for processing.

## Parameters

*inputs* **String**

The inputs to be submitted with the form. This can include user-entered data, selections, or any other relevant information that needs to be sent to the destination.

*data* **String**

The data to be submitted with the form. This can include additional information or parameters that are required for the submission process.

## Returns

An [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md) object that represents the result of the form submission. This object can contain information about the success or failure of the submission, as well as any relevant data returned from the server or API.
