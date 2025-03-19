---
title: ToggleSetting Constructors
description: The ToggleSetting constructors initialize a new instance of the ToggleSetting class with specified parameters.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToggleSetting Constructors

## ToggleSetting(String, Boolean) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ToggleSetting](togglesetting.md) class with a *key* and a *defaultValue*.

```C#
public ToggleSetting(string key, bool defaultValue)
        : base(key, defaultValue)
    {
    }
```

### Parameters

*key* **String**

The unique identifier for the toggle setting. This key is used to reference the setting in the application.

*defaultValue* **Boolean**

The default value of the toggle setting. This value is used when the setting is first created or when it is reset to its default state.

## ToggleSetting(String, String, String, Boolean) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ToggleSetting](togglesetting.md) class with a *key*, *label*, *description*, and *defaultValue*.

```C#
public ToggleSetting(string key, string label, string description, bool defaultValue)
        : base(key, label, description, defaultValue)
    {
    }
```

### Parameters

*key* **String**

The unique identifier for the toggle setting. This key is used to reference the setting in the application.

*label* **String**

The display name of the toggle setting. This label is shown to users in the user interface.

*description* **String**

The description of the toggle setting. This description provides additional information about the setting and its purpose.

*defaultValue* **Boolean**

The default value of the toggle setting. This value is used when the setting is first created or when it is reset to its default state.
