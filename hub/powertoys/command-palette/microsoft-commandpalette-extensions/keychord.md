---
title: KeyChord Struct
description: The KeyChord struct represents a combination of key modifiers and a virtual key. It is used to define keyboard shortcuts in the Command Palette.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# KeyChord Struct

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **KeyChord** struct represents a combination of key modifiers and a virtual key. It is used to define keyboard shortcuts in the Command Palette.

## Fields

| Field | Type | Description |
| :--- | :--- | :--- |
| Modifiers | **Windows.System.VirtualKeyModifiers** | The key modifiers (e.g., Control, Shift, Alt) that are part of the key chord. |
| Vkey | **Int32** | The virtual key code that represents the main key in the key chord. |
| ScanCode | **Int32** | The scan code that represents the physical key on the keyboard. This is used for low-level keyboard input handling. |
