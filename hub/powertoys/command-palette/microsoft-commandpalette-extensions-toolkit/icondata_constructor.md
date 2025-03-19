---
title: IconData Constructors
description: Initializes a new instance of the IconData class with the specified parameters.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconData Constructors

## IconData(IRandomAccessStreamReference) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [IconData](icondata.md) class with the [Data](icondata.md#properties) property set to *data*.

```C#
public IconData(IRandomAccessStreamReference data)
    {
        Data = data;
    }
```

### Parameters

*data* **Windows.Storage.Streams.IRandomAccessStreamReference**

The icon data as a stream reference. This property is used to load the icon data from a stream.

## IconData(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [IconData](icondata.md) class with the [Icon](icondata.md#properties) property set to *icon*.

```C#
public IconData(string? icon)
    {
        Icon = icon;
    }
```

### Parameters

*icon* **String**

The icon URI. This property is used to load the icon from a URI.
