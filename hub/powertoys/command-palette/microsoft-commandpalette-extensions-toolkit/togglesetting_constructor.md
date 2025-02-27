---
title: ToggleSetting Constructors
description: 
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToggleSetting Constructors

## ToggleSetting(String, Boolean) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ToggleSetting](togglesetting.md) class with a `key` and a `defaultValue`.

```C#
public ToggleSetting(string key, bool defaultValue)
        : base(key, defaultValue)
    {
    }
```

### Parameters

**`key`** String

**`defaultValue`** Boolean

## ToggleSetting(String, String, String, Boolean) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ToggleSetting](togglesetting.md) class with a `key`, `label`, `description`, and `defaultValue`.

```C#
public ToggleSetting(string key, string label, string description, bool defaultValue)
        : base(key, label, description, defaultValue)
    {
    }
```

### Parameters

**`key`** String

**`label`** String

**`description`** String

**`defaultValue`** Boolean
