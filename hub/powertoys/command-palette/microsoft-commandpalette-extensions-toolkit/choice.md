---
title: Choice Class
description: The Choice class represents a single choice in a choice set.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Choice Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **Choice** class represents a single choice in a choice set. It contains properties for the display text and value of the choice.

## Choice(String, String) Constructor

### Definition

Initializes a new instance of the `Choice` class with the given `title` and `value`.

```C#
public Choice(string title, string value)
        {
            Value = value;
            Title = title;
        }
```

### Parameters

*title* **String**

The display text for the choice. This is the text that will be shown to the user in the UI.

*value* **String**

The value for the choice. This is the value that will be returned when the choice is selected. It can be any string that represents the choice in a meaningful way.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Title | **String** | The display text for the choice. |
| Value | **String** | The value for the choice. |
