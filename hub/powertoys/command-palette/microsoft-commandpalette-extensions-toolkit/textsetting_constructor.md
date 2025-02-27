---
title: TextSetting Constructors
description: 
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# TextSetting Constructors

## TextSetting(String, String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [TextSetting](textsetting.md) class with a `key` and a `defaultValue`.

```C#
public TextSetting(string key, string defaultValue)
        : base(key, defaultValue)
    {
    }
```

### Parameters

**`key`** String

**`defaultValue`** String

## TextSetting(String, String, String, String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [TextSetting](textsetting.md) class with a `key`, `label`, `description`, and `defaultValue`.

```C#
public TextSetting(string key, string label, string description, string defaultValue)
        : base(key, label, description, defaultValue)
    {
    }
```

### Parameters

**`key`** String

**`label`** String

**`description`** String

**`defaultValue`** String
