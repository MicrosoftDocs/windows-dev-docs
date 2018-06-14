---
title: Xbox Live C APIs
author: KevinAsgari
description: Learn about the flat C API model that you can use to interact with the Xbox Live service.
ms.author: kevinasg
ms.date: 06/05/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, c, xsapi
ms.localizationpriority: low
---

# Introduction to the Xbox Live C APIs

In June, 2018, a new flat C API layer was added to XSAPI. This new API layer solves some issues that occurred with the C++ and WinRT API layers.

The C API does not yet cover all XSAPI features, but additional features are being worked on. All 3 API layers, C, C++, and WinRT will continue to be supported and have additional features added over time.

> [!NOTE]
> The C APIs currently only work with titles that use the Xbox Developer Kit (XDK). They do not support UWP games at this time.

## Features covered by the C APIs

The C API currently supports the following features and services:

- Achievements
- Presence
- Profile
- Social
- Social Manager

## Benefits of the C API for XSAPI

- Allows titles to control the memory allocations when calling XSAPI.
- Allows titles to gain full control of thread handling when calling XSAPI.
- Uses a new HTTP library, libHttpClient, designed for game developers.

You can use the C APIs alongside C++ XSAPI, but you will not gain the previously listed benefits with the C++ APIs.

### Managing memory allocations

With the new C API, you can now specify a function callback that XSAPI will call whenever it tries to allocate memory. If you do not specify function callbacks, XSAPI will use standard memory allocation routines.

To manually specify your memory routines, you can do the following:

- At the start of the game:
  - Call `XblMemSetFunctions(memAllocFunc, memFreeFunc)` to specify the allocation callbacks for assigning and releasing memory.
  - Call `XblInitialize()` to initialize the library instance.  
- While the game is running:
  - Calling any of the new C APIs in XSAPI that allocate or free memory will cause XSAPI to call the specified memory handling callbacks.  
- When the game exits:
  - Call `XblCleanup()` to reclaim all resources associated with the XSAPI library.
  - Clean up your game's custom memory manager.

### Managing asynchronous threads

The C API introduces a new asynchronous thread calling pattern that allows developers full control over the threading model. For more information, see [Calling pattern for XSAPI flat C layer async calls](flatc-async-patterns.md).

## Migrating code to use C XSAPI

The XSAPI C APIs can be used alongside the XSAPI C++ APIs in a project, so we recommend that you migrate one feature at a time.

The C APIs and C++ APIs are really just thin wrappers around a common core, just with different entry points, so the functionality should be unchanged. However, only the C APIs can take advantage of the custom memory and thread management features.

> [!IMPORTANT]
> You cannot mix XSAPI WinRT APIs with the C APIs.

## Where to view the C APIs

- [C API header files](https://github.com/Microsoft/xbox-live-api/tree/master/Include/xsapi-c)
- [Sample code using the new C APIs](https://github.com/Microsoft/xbox-live-api/tree/master/InProgressSamples/Social/Xbox/C)
- [libHttpClient](https://github.com/Microsoft/libHttpClient)
