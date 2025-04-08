---
title: IProgressState Interface
description: The IProgressState interface represents the state of a progress indicator in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IProgressState Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IProgressState** interface represents the state of a progress indicator in the Command Palette. It is used to provide information about the progress of an operation, such as loading or processing data.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| IsIndeterminate | **Boolean** | Indicates whether the progress is indeterminate (true) or determinate (false). |
| ProgressPercent | **UInt32** | Gets the progress percentage (`0`-`100`) if the progress is determinate. |
