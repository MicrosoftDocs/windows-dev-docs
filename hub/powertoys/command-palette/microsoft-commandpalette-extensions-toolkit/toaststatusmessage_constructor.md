---
title: ToastStatusMessage Constructors
description: 
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToastStatusMessage Constructors

## ToastStatusMessage([StatusMessage](statusmessage.md)) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ToastStatusMessage](toaststatusmessage.md) class with its [Message](toaststatusmessage.md#properties) property set to `message`.

```C#
public ToastStatusMessage(StatusMessage message)
    {
        Message = message;
    }
```

### Parameters

**`message`** [StatusMessage](statusmessage.md)

## ToastStatusMessage(String) Constructor

### Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Initializes a new instance of the [ToastStatusMessage](toaststatusmessage.md) class with its [Message](toaststatusmessage.md#properties) property set to `text`.

```C#
public ToastStatusMessage(string text)
    {
        Message = new StatusMessage() { Message = text };
    }
```

### Parameters

**`text`** String
