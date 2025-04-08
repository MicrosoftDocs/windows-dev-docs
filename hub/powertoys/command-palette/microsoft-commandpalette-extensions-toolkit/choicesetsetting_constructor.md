---
title: ChoiceSetSetting Constructors
description: Initializes a new instance of the ChoiceSetSetting class.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ChoiceSetSetting Constructors

## ChoiceSetSetting(String, List\<Choice\>) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ChoiceSetSetting](choicesetsetting.md) class from the base [Setting](setting.md) class, setting its [Key](setting.md#properties) property to *key* and its [Choices](choicesetsetting.md#properties) set to *choices*.

```C#
public ChoiceSetSetting(string key, List<Choice> choices)
        : base(key, choices.ElementAt(0).Value)
    {
        Choices = choices;
    }
```

### Parameters

*key* **String**

The key for the setting. This is a unique identifier that is used to reference the setting in the application.

*choices* **List\<[Choice](choice.md)\>**

The list of choices for the setting. Each choice is represented by a [Choice](choice.md) object, which contains the display text and value for the choice. The first choice in the list is used as the default value for the setting.

## ChoiceSetSetting(String, String, String, List\<Choice\>) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [ChoiceSetSetting](choicesetsetting.md) class from the base [Setting](setting.md) class, setting its [Key](setting.md#properties) property to *key*, its [Label](setting.md#properties) to *label*, its [Description](setting.md#properties) to *description*, and its [Choices](choicesetsetting.md#properties) to *choices*.

```C#
public ChoiceSetSetting(string key, string label, string description, List<Choice> choices)
        : base(key, label, description, choices.ElementAt(0).Value)
    {
        Choices = choices;
    }
```

### Parameters

*key* **String**

The key for the setting. This is a unique identifier that is used to reference the setting in the application.

*label* **String**

The label for the setting. This is a user-friendly name that is displayed to the user in the application.

*description* **String**

The description for the setting. This provides additional information about the setting and its purpose.

*choices* **List\<[Choice](choice.md)\>**

The list of choices for the setting. Each choice is represented by a [Choice](choice.md) object, which contains the display text and value for the choice. The first choice in the list is used as the default value for the setting.
