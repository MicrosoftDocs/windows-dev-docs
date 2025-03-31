---
title: IconInfo Constructors
description: Initializes a new instance of the IconInfo class with overload options to specify icons for light and dark modes.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconInfo Constructors

## IconInfo(IconData, IconData) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [IconInfo](iconinfo.md) class with a dark mode and a light mode version of the icon.

```C#
public IconInfo(IconData light, IconData dark)
    {
        Light = light;
        Dark = dark;
    }
```

### Parameters

*dark* [IconData](icondata.md)

The dark mode version of the icon. This parameter is used to specify the icon that will be displayed when the application is in dark mode.

*light* [IconData](icondata.md)

The light mode version of the icon. This parameter is used to specify the icon that will be displayed when the application is in light mode.

## IconInfo(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [IconInfo](iconinfo.md) class with one version of the icon, used for both light and dark modes.

```C#
public IconInfo(string icon)
    {
        Dark = Light = new(icon);
    }
```

### Parameters

*icon* **String**

The icon that will be displayed in both light and dark modes. This parameter is used to specify a single icon that will be used regardless of the application's theme.
