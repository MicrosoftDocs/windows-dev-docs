---
title: Tag Constructors definition
description: The Tag class has two constructors for creating new tag instances with or without text initialization.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Tag Constructors

## Tag() Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [Tag](tag.md) class.

```C#
public Tag()
    {
    }
```

## Tag(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [Tag](tag.md) class with its **_text** set to *text*.

```C#
public Tag(string text)
    {
        _text = text;
    }
```

### Parameters

*text* **String**

The text to be set for the tag. This parameter allows you to initialize the tag with a specific string value at the time of its creation.
