---
title: Setting Constructors
description: The Setting class provides several constructors for creating instances of the Setting class.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Setting Constructors

## Setting() Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [Setting](setting.md) class with [Value](setting.md#properties) property set to its default value and [Key](setting.md#properties) set to an empty string.

```C#
protected Setting()
    {
        Value = default;
        Key = string.Empty;
    }
```

## Setting(String, T) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [Setting](setting.md) class with its [Key](setting.md#properties) property set to *key* and its [Value](setting.md#properties) set to *defaultValue*.

```C#
public Setting(string key, T defaultValue)
    {
        Key = key;
        Value = defaultValue;
    }
```

### Parameters

*key* **String**

The key of the setting. This is a unique identifier for the setting.

*defaultValue* **T**

The default value of the setting. This is the value that will be used if no value is provided for the setting.

## Setting(String, String, String, T) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [Setting](setting.md) class with its [Key](setting.md#properties) property set to *key*, its [Value](setting.md#properties) set to *defaultValue*, its [Label](setting.md#properties) set to *label*, and its [Description](setting.md#properties) set to *description*.

```C#
public Setting(string key, string label, string description, T defaultValue)
    {
        Key = key;
        Value = defaultValue;
        Label = label;
        Description = description;
    }
```

### Parameters

*key* **String**

The key of the setting. This is a unique identifier for the setting.

*label* **String**

The label of the setting. This is a user-friendly name for the setting that will be displayed in the UI.

*description* **String**

The description of the setting. This is a brief explanation of what the setting does and how it should be used.

*defaultValue* **T**

The default value of the setting. This is the value that will be used if no value is provided for the setting.
