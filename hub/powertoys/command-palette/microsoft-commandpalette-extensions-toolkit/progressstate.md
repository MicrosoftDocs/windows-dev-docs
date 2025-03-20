---
title: ProgressState Class
description: The ProgressState class is used to represent the state of a progress indicator.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ProgressState Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [IProgressState](../microsoft-commandpalette-extensions/iprogressstate.md)

The **ProgressState** class is used to represent the state of a progress indicator. It provides properties to indicate whether the progress is indeterminate and to set the progress percentage.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| IsIndeterminate | **Boolean** | Gets or sets a value indicating whether the progress is indeterminate. |
| ProgressPercent | **UInt** | Gets or sets the progress percentage. This value is between `0` and `100`. |
