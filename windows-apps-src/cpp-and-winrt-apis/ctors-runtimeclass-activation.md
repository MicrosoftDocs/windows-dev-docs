---
author: stevewhims
description: This topic describes two different ways to instantiate a runtime class with C++/WinRT. The way you go depends on whether the runtime class is implemented in the same compilation unit as the consuming code, or in a different one.
title: Runtime class instantiation, activation, and construction
ms.author: stwhi
ms.date: 03/05/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, runtime class, activation
ms.localizationpriority: medium
---

# Runtime class instantiation, activation, and construction
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic describes two different ways to instantiate a Windows Runtime (runtime) class with C++/WinRT. The way you go depends on whether the runtime class is implemented in the same project (the same compilation unit) as the consuming code, or in a different one.

So that we have an example project to refer to, we'll imagine that your project name (and therefore your root namespace name) is MyProject, and that you have an IDL file named `MyRuntimeClass.idl` in which you declare a runtime class named MyRuntimeClass (we recommend that you declare each runtime class in its own IDL file). There are actually several forms of *MyRuntimeClass*.

- *MyRuntimeClass* is the name of a runtime class&mdash;a type that can be activated and consumed via modern COM interfaces across executable boundaries.
- *MyRuntimeClass* is also the name of the C++ struct `winrt::MyProject::implementation::MyRuntimeClass`, which is the C++ implementation of the runtime class. If there are separate implementing and consuming projects, then this struct exists only in the implementing project.
- And *MyRuntimeClass* is also the name of a projected type (that is, a consumption wrapper over the runtime class) in the form of the C++ struct `winrt::MyProject::MyRuntimeClass`. If there are separate implementing and consuming projects, then this struct exists in both.

That consumption wrapper (`winrt::MyProject::MyRuntimeClass`) is generated into both implementing and consuming projects (if they're different projects) by `cppwinrt.exe` in the header file `\Generated Files\winrt\impl\MyProject.2.h`. Here are the parts of that header that are relevant to this topic.

```cppwinrt
// MyProject.2.h

namespace winrt::MyProject {
	struct MyRuntimeClass :
		MyProject::IMyRuntimeClass
	{
		MyRuntimeClass(std::nullptr_t) noexcept {}
		MyRuntimeClass();
	};
}
```

As you can see, the consumption wrapper has two constructors, and they're for the following two different use cases.

## The runtime class is implemented in the same project as the consuming code
If you have a XAML-based UWP app (perhaps using the **Blank App (C++/WinRT)** Visual Studio project template), and you want to consume a local runtime class from within a XAML page, then there's no need to register the runtime class nor instantiate it via WinRT/COM activation. In that case, you initialize an instance of the consumption wrapper (`winrt::MyProject::MyRuntimeClass`) by calling the constructor that takes `nullptr`.

```cppwinrt
// MainPage.h
...
#include "MainPage.g.h"
#include "MyRuntimeClass.h"

namespace winrt::MyProject::implementation
{
	struct MainPage : MainPageT<MainPage>
	{
...
	private:
		MyProject::MyRuntimeClass myRC{ nullptr };
...
	};
}
...
```

That constructor doesn't perform any initialization, so you then assign a value to that instance via the [**winrt::make**](/uwp/cpp-ref-for-winrt/make?branch=live) free function.

```cppwinrt
// MainPage.cpp
namespace winrt::MyProject::implementation
{
	MainPage::MainPage()
	{
		this->myRC = make<MyRuntimeClass>();
		InitializeComponent();
	}
...
```

Note that, even though `myRC` is a consumption wrapper (of type `winrt::MyProject::MyRuntimeClass`), the template parameter that you use with **make** is the C++ implementation type (`winrt::MyProject::implementation::MyRuntimeClass`).

## The runtime class is implemented in a different project from the consuming code
If you have a runtime class implemented in a Windows Runtime Component, and you have a separate project containing a consuming app (so, the runtime class implementation and the consuming code are in different compilation units), then the runtime class must be registered, and instantiated via WinRT/COM activation. In that case, you call the default constructor of the consumption wrapper (`winrt::MyWindowsRuntimeComponentProject::MyRuntimeClass`).

```cppwinrt
// App.cpp
...
#include "winrt/MyRuntimeClass.h"
...
struct MyCoreApp : implements<MyCoreApp, IFrameworkViewSource, IFrameworkView>
{
	MyWindowsRuntimeComponentProject::MyRuntimeClass runtimeClassInstance;
	...
}
```

In this scenario, the consuming project's startup code registers the runtime class. And the consuming wrapper constructor shown above calls [RoActivateInstance](https://msdn.microsoft.com/library/br224646) to activate the runtime class from the referenced Windows Runtime component.

## Important APIs
[winrt::make](/uwp/cpp-ref-for-winrt/make?branch=live)
