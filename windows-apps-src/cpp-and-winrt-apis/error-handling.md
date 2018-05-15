---
author: stevewhims
description: This topic discusses strategies for handling errors when programming with C++/WinRT.
title: Error handling with C++/WinRT
ms.author: stwhi
ms.date: 05/14/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, error, handling, exception
ms.localizationpriority: medium
---

# Error handling with [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
This topic discusses strategies for handling errors when programming with C++/WinRT. For more general info, and background, see [Errors and Exception Handling (Modern C++)](/cpp/cpp/errors-and-exception-handling-modern-cpp).

We recommend that you continue to write [exception-safe code](/cpp/cpp/how-to-design-for-exception-safety), but that you prefer *not* catching exceptions whenever possible. If there is no handler for an exception, then Windows automatically generates an error report (including a minidump of the crash) that will help you track down where the problem is. Throw an exception *only when an unexpected runtime error occurs*, and handle everything else with error/result codes. That way, when an exception *is* thrown, you know that the cause is either a bug in your code, or an exceptional error state in the system.

Consider the scenario of accessing the Windows Registry. If your app fails to read a value from the Registry, then that's to be expected, and you should handle it gracefully. Don't throw an exception; rather return a `bool` or `enum` value indicating that, and perhaps why, the value wasn't read. Failing to *write* a value to the Registry, on the other hand, is likely to indicate that there's a bigger problem than you can handle sensibly in your application. In a case like that, it's appropriate for you to throw an exception.

Here's how to proceed once you've decided that, should your call to a given function fail, your application won't be able to recover (you'll no longer be able to rely on it to function predictably). The code example below uses a **winrt::handle** value as a wrapper around the HANDLE returned from [**CreateEvent**](https://msdn.microsoft.com/library/windows/desktop/ms682396). It then passes the handle (creating a `bool` value from it) to the [**winrt::check_bool**](/uwp/cpp-ref-for-winrt/check-bool) function template. **check_bool** works with a `bool`, or with any value that's convertible to `false` (an error condition), or `true` (a success condition). If the value is false, then **check_bool** throws a C++/WinRT object (a [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/hresult-error)) that represents the calling thread's last-error code value.

```cppwinrt
winrt::handle h{ ::CreateEvent(nullptr, false, false, nullptr) };
winrt::check_bool(bool{ h });
winrt::check_bool(::SetEvent(h.get()));
```

Here are the other check functions.
- [**winrt::check_hresult**](/uwp/cpp-ref-for-winrt/check-hresult)
- [**winrt::check_nt**](/uwp/cpp-ref-for-winrt/check-nt)
- [**winrt::check_pointer**](/uwp/cpp-ref-for-winrt/check-pointer)
- [**winrt::check_win32**](/uwp/cpp-ref-for-winrt/check-win32)

## Exception handling, and performance
Throwing exceptions tends to be slower than using error codes. If you only throw an exception when a fatal error occurs, then if all goes well you'll never pay the performance price.

But a more likely performance hit involves the runtime overhead of ensuring that the appropriate destructors are called in the unlikely event that an exception is thrown. The cost of this assurance comes whether an exception is actually thrown or not. So, you should ensure that the compiler has a good idea of what functions can potentially throw exceptions. If the compiler can prove that there won't be any exceptions from certain functions (the `noexcept` specification), then it can optimize the code it generates.

## Important APIs
* [winrt::check_bool](/uwp/cpp-ref-for-winrt/check-bool)
* [winrt::check_hresult](/uwp/cpp-ref-for-winrt/check-hresult)
* [winrt::check_nt](/uwp/cpp-ref-for-winrt/check-nt)
* [winrt::check_pointer](/uwp/cpp-ref-for-winrt/check-pointer)
* [winrt::check_win32](/uwp/cpp-ref-for-winrt/check-win32)
* [winrt::hresult_error](/uwp/cpp-ref-for-winrt/hresult-error)

## Related topics
* [Errors and Exception Handling (Modern C++)](/cpp/cpp/errors-and-exception-handling-modern-cpp)
* [How to: Design for Exception Safety](/cpp/cpp/how-to-design-for-exception-safety)
