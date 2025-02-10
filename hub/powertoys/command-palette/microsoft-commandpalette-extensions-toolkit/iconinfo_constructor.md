---
title: IconInfo Constructors
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconInfo() Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Initializes a new instance of the [IconInfo](iconinfo.md) class with an empty icon.

```C#
internal IconInfo()
        : this(string.Empty)
    {
    }
```

# IconInfo(IconData, IconData) Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Initializes a new instance of the [IconInfo](iconinfo.md) class with a dark mode and a light mode version of the icon.

```C#
public IconInfo(IconData light, IconData dark)
    {
        Light = light;
        Dark = dark;
    }
```

## Parameters

**`dark`** [IconData](icondata.md)

**`light`** [IconData](icondata.md)

# IconInfo(String?) Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Initializes a new instance of the [IconInfo](iconinfo.md) class with one version of the icon, used for both light and dark modes..

```C#
public IconInfo(string? icon)
    {
        Dark = Light = new(icon);
    }
```

## Parameters

**`icon`** String?
