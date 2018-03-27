---
author: stevewhims
description: This topic shows how to implement a Windows Runtime interface in C++/WinRT.
title: Interfaces; how to implement them in C++/WinRT
ms.author: stwhi
ms.date: 03/26/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, event, handle, handling
ms.localizationpriority: medium
---

# Interfaces; how to implement them in C++/WinRT
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic shows how to implement a Windows Runtime interface in C++/WinRT.

## If you're *not* authoring a runtime class
The simplest scenario is where you're implementing a Windows Runtime interface on an ordinary C++ class. A good example is when you're writing an app based around [**CoreApplication**](/uwp/api/windows.applicationmodel.core.coreapplication).

The technique is as simple as deriving your type from [winrt::implements](/uwp/cpp-ref-for-winrt/implements), and then implementing the necessary functions.

```cppwinrt
// App.cpp
...
struct App : implements<App, IFrameworkViewSource>
{
    IFrameworkView CreateView()
    {
        return ...
    }
}
...
```

## If you're authoring a runtime class
There are two scenarios in which your type (the type that's implementing a Windows Runtime interface) needs to be a runtime class.

- Your type is packaged in a Windows Runtime Component for consumption from apps, or
- Your type is referenced by your XAML user interface (UI).

For info about authoring a runtime class, see [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md).

## Important APIs
* [winrt::implements (C++/WinRT)](/uwp/cpp-ref-for-winrt/implements)

## Related topics
* [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md)
