---
title: Introduction to Xbox Live APIs
author: KevinAsgari
description: Learn about the various API models that you can use to interact with the Xbox Live service.
ms.assetid: 5918c3a2-6529-4f07-b44d-51f9861f91ec
ms.author: kevinasg
ms.date: 06/05/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Introduction to Xbox Live APIs

## Use Xbox Live services

There are two ways to get information from the Xbox Live services:

- Use a client side API called Xbox Live Services API (**XSAPI**)
- Call the **Xbox Live REST endpoints** directly

The advantages of using the Xbox Live Services API (**XSAPI**) include:

- Details of authentication, encoding, and HTTP sending and receiving are taken care of for you.
- Arguments to, and data returned from, the wrapper API is handled in native data types; so, you don't need to perform JSON encoding and decoding.
- Calling web services directly involves multiple asynchronous steps, which the wrapper API encapsulates; this makes title code easier to read and write.
- Some functionality, such as writing game events, is only available in XSAPI.

The advantages of using the **Xbox Live REST endpoints** directly include:

- The ability to call Xbox Live endpoints from a web service
- The ability to call endpoints which aren't included in XSAPI.  XSAPI only includes APIs that we believe games will use, so if there's anything missing let us know via the forums.
- Some functionality available via the REST endpoints may not have a corresponding XSAPI wrapper.

Your games and apps are not limited to using just one of these methods. You can use the XSAPI wrapper and still call the REST endpoints directly if needed.

## Xbox Live Services API Overview ##

The Xbox Live Services API (**XSAPI**) exposes three sets of client side APIs that support a wide range of customer scenarios:

- [XSAPI WinRT API](#xsapi-winrt-based-api)
- [XSAPI C++11 based API](#xsapi-c++11-based-api)
- [XSAPI C based API](#xsapi-c-based-api) (**New as of June 2018**)

Comparing the APIs:

### XSAPI WinRT based API

- Supports applications written with C++/CX, C#, and JavaScript.
    - C++/CX is a Microsoft C++ extension to make WinRT programming easy for example using ^ as WinRT pointers.
- Supports applications targeting Xbox One XDK platform, and Universal Windows Platform (UWP) x86, x64 and ARM architectures.
- Errors are handled via exceptions in all languages including C++/CX.
- C++/WinRT is also supported.  More information about C++/WinRT can be found at [https://moderncpp.com/2016/10/13/cppwinrt-available-on-github/](https://moderncpp.com/2016/10/13/cppwinrt-available-on-github/)

Here's an example of calling the XSAPI WinRT API using C++/WinRT:

```c++
winrt::Windows::Xbox::System::User cppWinrtUser = winrt::Windows::Xbox::System::User::Users().GetAt(0);
winrt::Microsoft::Xbox::Services::XboxLiveContext xblContext(cppWinrtUser);
```

If you want to mix C++/CX and C++/WinRT as you are migrating code you can do this too but is a little more complex.  
Here's an example of calling the XSAPI WinRT API using C++/WinRT given C++/CX User^ object.

```c++
::Windows::Xbox::System::User^ user1 = ::Windows::Xbox::System::User::Users->GetAt(0);
winrt::Windows::Xbox::System::User cppWinrtUser(nullptr);
winrt::copy_from(cppWinrtUser, reinterpret_cast<winrt::ABI::Windows::Xbox::System::IUser*>(user1));
winrt::Microsoft::Xbox::Services::XboxLiveContext xblContext(cppWinrtUser);
```


### XSAPI C++11 based API

- Uses cross platform ISO standard C++11
- Supports applications written with C++
- Supports applications targeting Xbox One XDK platform, and Universal Windows Platform (UWP) x86, x64 and ARM architectures.
- Errors are handled via std::error_code.
- The C++11 based API is the recommended API to use for C++ game engines for better performance, and better debugging.
- If you are in the Xbox Live Creators Program, before including the XSAPI header define XBOX_LIVE_CREATORS_SDK. This limits the API surface area to only those that are usable by developers in the Xbox Live Creators Program and changes the sign-in method to work for titles in the Creators program.  For example:

```c++
#define XBOX_LIVE_CREATORS_SDK
#include "xsapi\services.h"
```

- C++/WinRT is also supported.  More information about C++/WinRT can be found at [https://moderncpp.com/2016/10/13/cppwinrt-available-on-github/](https://moderncpp.com/2016/10/13/cppwinrt-available-on-github/)

To use C++/WinRT with the XSAPI C++ API, before including the XSAPI header define XSAPI_CPPWINRT.  For example:

```c++
#define XSAPI_CPPWINRT
#include "xsapi\services.h"
```

Here's an example of calling the XSAPI C++ API using C++/WinRT:

```c++
winrt::Windows::Xbox::System::User cppWinrtUser = winrt::Windows::Xbox::System::User::Users().GetAt(0);
std::shared_ptr<xbox::services::xbox_live_context> xboxLiveContext = std::make_shared<xbox::services::xbox_live_context>(cppWinrtUser);
```

### XSAPI C based API

- Allows titles to control the memory allocations when calling XSAPI.
- Allows titles to gain full control of thread handling when calling XSAPI.
- Uses a new HTTP library, libHttpClient, designed for game developers.

For more information, see [Introduction to the Xbox Live C APIs](xsapi-flat-c.md).
