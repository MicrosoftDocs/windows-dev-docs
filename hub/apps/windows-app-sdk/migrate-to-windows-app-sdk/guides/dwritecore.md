---
title: DirectWrite to DWriteCore migration
description: DWriteCore is the [Windows App SDK](../../index.md) implementation of [DirectWrite](/windows/win32/directwrite/direct-write-portal).
ms.topic: article
ms.date: 09/16/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, DirectWrite, DWriteCore
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# DirectWrite to DWriteCore migration

DWriteCore is the [Windows App SDK](../../index.md) implementation of [DirectWrite](/windows/win32/directwrite/direct-write-portal). For more info, see [DWriteCore overview](/windows/win32/directwrite/dwritecore-overview).

## Summary of API and/or feature differences

Nearly all DirectWrite APIs remain unchanged in DWriteCore. There are a few differences, as described in [APIs that are new, or different, for DWriteCore](/windows/win32/directwrite/dwritecore-overview#apis-that-are-new-or-different-for-dwritecore).

As you'll see in that topic, DWriteCore has a more locked-down factory type, and has the ability to retrieve pixel data without using GDI.

## Migration guidance 

The only change necessary when moving from DirectWrite to DWriteCore is to include the `dwrite_core.h` header file. For more info, and code examples, see [Programming with DWriteCore](/windows/win32/directwrite/dwritecore-overview#programming-with-dwritecore).

>[!WARNING]
> DWriteCore does not currently support hardware-accelerated text rendering with Direct2D (D2D). It supports software text rendering only. This prevents apps that require D2D support from adopting DWriteCore at this time.

## The DWriteCoreGallery sample app

Also see the [DWriteCoreGallery](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/TextRendering) sample app project, which demonstrates the DWriteCore API surface.

## Related topics

* [Windows App SDK and supported Windows releases](../../support.md)
* [DWriteCore overview](/windows/win32/directwrite/dwritecore-overview)
* [DWriteCoreGallery](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/TextRendering) sample app
* [DirectWrite](/windows/win32/directwrite/direct-write-portal)
* [Windows App SDK](../../index.md)