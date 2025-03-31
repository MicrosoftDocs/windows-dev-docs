---
title: PropChangedEventArgs Constructors
description: Initializes a new instance of the PropChangedEventArgs class with the specified property name.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# PropChangedEventArgs Constructors

## PropChangedEventArgs(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [PropChangedEventArgs](propchangedeventargs.md) class with the [PropertyName](propchangedeventargs.md#properties) property set to *propertyName*.

```C#
public PropChangedEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
```

### Parameters

*propertyName* **String**

The name of the property that has changed. This value is used to set the [PropertyName](propchangedeventargs.md#properties) property of the [PropChangedEventArgs](propchangedeventargs.md) class.
