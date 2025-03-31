---
title: TextSetting Constructors
description: The TextSetting class constructors allow you to create instances of the class with different parameters.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# TextSetting Constructors

The [TextSetting](textsetting.md) class has constructors that allow you to create instances of the class with different parameters.

## TextSetting(String, String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [TextSetting](textsetting.md) class with a *key* and a *defaultValue*.

```C#
public TextSetting(string key, string defaultValue)
        : base(key, defaultValue)
    {
    }
```

### Parameters

*key* **String**

The unique identifier for the setting.

*defaultValue* **String**

The default value for the setting.

## TextSetting(String, String, String, String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [TextSetting](textsetting.md) class with a *key*, *label*, *description*, and *defaultValue*.

```C#
public TextSetting(string key, string label, string description, string defaultValue)
        : base(key, label, description, defaultValue)
    {
    }
```

### Parameters

*key* **String**

The unique identifier for the setting.

*label* **String**

The display name of the setting.

*description* **String**

The description of the setting.

*defaultValue* **String**

The default value for the setting.
