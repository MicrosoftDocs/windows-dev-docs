---
title: IconData Constructors
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconData() Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Initializes a new instance of the [IconData](icondata.md) class with the [Icon](icondata.md#properties) property set to an empty string.

```C#
internal IconData()
        : this(string.Empty)
    {
    }
```

# IconData(IRandomAccessStreamReference) Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Initializes a new instance of the [IconData](icondata.md) class with the [Data](icondata.md#properties) property set to `data`.

```C#
public IconData(IRandomAccessStreamReference data)
    {
        Data = data;
    }
```

## Parameters

**`data`** Windows.Storage.Streams.IRandomAccessStreamReference

# IconData(String?) Constructor

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Initializes a new instance of the [IconData](icondata.md) class with the [Icon](icondata.md#properties) property set to `icon`.

```C#
public IconData(string? icon)
    {
        Icon = icon;
    }
```

## Parameters

**`icon`** String?
