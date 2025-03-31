---
title: DetailsLink Constructors
description: Initializes a new instance of the DetailsLink class.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# DetailsLink Constructors

## DetailsLink() Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [DetailsLink](detailslink.md) class.

```C#
public DetailsLink()
    {
    }
```

## DetailsLink(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [DetailsLink](detailslink.md) class, setting its [Link](detailslink.md#properties) and [Text](detailslink.md#properties) properties to *url*.

```C#
public DetailsLink(string url)
        : this(url, url)
    {
    }
```

### Parameters

*url* **String**

The URL to be set as both the link and text for the [DetailsLink](detailslink.md) instance.

## DetailsLink(String, String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Initializes a new instance of the [DetailsLink](detailslink.md) class, setting its [Link](detailslink.md#properties) property to *url* and [Text](detailslink.md#properties) to *text*.

```C#
public DetailsLink(string url, string text)
    {
        if (Uri.TryCreate(url, default(UriCreationOptions), out var newUri))
        {
            Link = newUri;
        }

        Text = text;
    }
```

### Parameters

*url* *String*

The URL to be set as the link for the [DetailsLink](detailslink.md) instance.

*text* *String*

The text to be displayed for the link in the Command Palette. This text is what users will see and click on to access the link.
