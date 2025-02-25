---
title: ChoiceSetSetting Constructors
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ChoiceSetSetting Constructors

## ChoiceSetSetting() Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ChoiceSetSetting](choicesetsetting.md) class with [Choices](choicesetsetting.md#properties) set to an empty list.

```C#
private ChoiceSetSetting()
        : base()
    {
        Choices = [];
    }
```

## ChoiceSetSetting(String, List<Choice>) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ChoiceSetSetting](choicesetsetting.md) class from the base [Setting](setting.md) class, setting its [Key](setting.md#properties) property to `key` and its [Choices](choicesetsetting.md#properties) set to `choices`. 

```C#
public ChoiceSetSetting(string key, List<Choice> choices)
        : base(key, choices.ElementAt(0).Value)
    {
        Choices = choices;
    }
```

### Parameters

**`key`** String

**`choices`** List<[Choice](choice.md)>

## ChoiceSetSetting(String, String, String, List<Choice>) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ChoiceSetSetting](choicesetsetting.md) class from the base [Setting](setting.md) class, setting its [Key](setting.md#properties) property to `key`, its [Label](setting.md#properties) to `label`, its [Description](setting.md#properties) to `description`, and its [Choices](choicesetsetting.md#properties) to `choices`. 

```C#
public ChoiceSetSetting(string key, string label, string description, List<Choice> choices)
        : base(key, label, description, choices.ElementAt(0).Value)
    {
        Choices = choices;
    }
```

### Parameters

**`key`** String

**`label`** String

**`description`** String

**`choices`** List<[Choice](choice.md)>
