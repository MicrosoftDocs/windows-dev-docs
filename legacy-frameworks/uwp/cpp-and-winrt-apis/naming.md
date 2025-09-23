---
title: C++/WinRT naming conventions
description: This topic explains naming conventions that C++/WinRT has established.
ms.date: 10/05/2021
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, naming conventions
ms.localizationpriority: medium
---

# C++/WinRT naming conventions

C++/WinRT has established the following naming conventions:

* The **winrt::impl** namespace is reserved for C++/WinRT, and you shouldn't use it in your application.
* In the **winrt** namespace, names that begin with a lowercase letter belong to C++/WinRT, but you may use them in your application. The documentation calls out those names that you can overload or specialize. For example, your application is permitted to specialize the [winrt::is_guid_of](/uwp/cpp-ref-for-winrt/is-guid-of) function template.
* In sub-namespaces of the **winrt** namespace (except for **winrt::impl**), names that begin with an uppercase letter are available to your application.
* In all namespaces, names beginning with **WINRT_IMPL_** are reserved for C++/WinRT, and you shouldn't use them in your application.
* In all namespaces, names beginning with **WINRT_** (except those that begin with **WINRT_IMPL_**) are reserved for C++/WinRT. You may use them, and the documentation calls out those names that may be defined by your application, such as **WINRT_LEAN_AND_MEAN**.

It's common for applications to perform namespace composition, and import sub-namespaces of the **winrt** root namespace into the **winrt** root namespace:

```cpp
namespace winrt
{
    using namespace winrt::Windows::Foundation;
}
```

Therefore, your application should adhere to the naming conventions above in sub-namespaces of the **winrt** namespace.

Here's a summary.
 
| Namespace | Name | Apps may define | Apps may use |
|-|-|-|-|
| **winrt::impl** | Any | No | No |
| **winrt** and sub-namespaces (except **impl**) | Starts with lowercase letter | No | Yes |
| **winrt** and sub-namespaces (except **impl**) | Starts with uppercase letter | Yes | Yes |
| Any | **WINRT_IMPL_\*** | No | No |
| Any | **WINRT_\*** (except **WINRT_IMPL_\***) | Case-by-case | Yes |
