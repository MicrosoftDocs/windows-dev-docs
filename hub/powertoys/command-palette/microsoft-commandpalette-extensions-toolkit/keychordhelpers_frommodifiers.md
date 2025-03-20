---
title: KeyChordHelpers.FromModifiers(Boolean, Boolean, Boolean, Boolean, Integer, Integer) Method
description: The KeyChordHelpers.FromModifiers method creates a new KeyChord object from the specified modifier keys and virtual key code.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# KeyChordHelpers.FromModifiers(Boolean, Boolean, Boolean, Boolean, Integer, Integer) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FromModifiers** method creates a new [KeyChord](../microsoft-commandpalette-extensions/keychord.md) object from the specified modifier keys and virtual key code.

## Parameters

*ctrl* **Boolean**

Indicates whether the `Control` key is pressed.

*alt* **Boolean**

The `Alt` key is pressed.

*shift* **Boolean**

The `Shift` key is pressed.

*win* **Boolean**

The `Windows` key is pressed.

*vkey* **Integer**

The virtual key code of the key that is pressed. This value is typically obtained from the **KeyInterop** class.

*scanCode* **Integer**

The scan code of the key that is pressed. This value is typically obtained from the **KeyInterop** class.

## Returns

A [KeyChord](../microsoft-commandpalette-extensions/keychord.md) object that represents the specified modifier keys and virtual key code.
