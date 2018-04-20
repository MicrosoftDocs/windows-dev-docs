---
author: stevewhims
description: This topic shows how to consume C++/WinRT APIs, whether they're implemented by Windows, a third-party component vendor, or by yourself.
title: Consume APIs with C++/WinRT
ms.author: stwhi
ms.date: 04/18/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projected, projection, implementation, runtime class, activation
ms.localizationpriority: medium
---

# Consume APIs with [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md)
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic shows how to consume C++/WinRT APIs, whether they're part of Windows, implemented by a third-party component vendor, or implemented by yourself.

## If the API is in a Windows namespace
This is the most common case in which you'll consume a Windows Runtime API. Here's a simple code example.

```cppwinrt
#include "winrt/Windows.Foundation.h"

using namespace winrt;
using namespace Windows::Foundation;

int main()
{
	winrt::init_apartment();
	Uri contosoUri{ L"http://www.contoso.com" };
}
```

The included header `winrt/Windows.Foundation.h` is part of the SDK, found inside the folder `%WindowsSdkDir%Include<WindowsTargetPlatformVersion>\cppwinrt\winrt\`. The headers in that folder contain Windows APIs projected into C++/WinRT. Whenever you want to use a type from a Windows namespace, include the C++/WinRT projection header corresponding to that namespace. The `using namespace` directives are optional, but convenient.

In this example, `winrt/Windows.Foundation.h` contains the projected type for the runtime class [**Windows::Foundation::Uri**](/uwp/api/windows.foundation.uri).

> [!TIP]
> A *projected type* is a wrapper over a runtime class for purposes of consuming its APIs. A *projected interface* is a wrapper over a Windows Runtime interface.

In the code example above, after initializing C++/WinRT, we construct the **Uri** projected type via one of its publicly documented constructors ([**Uri(String)**](/uwp/api/windows.foundation.uri#Windows_Foundation_Uri__ctor_System_String_), in this example). For this, the most common use case, that's all you have to do.

## If the API is implemented in a Windows Runtime component
This section applies whether you authored the component yourself, or it came from a vendor.

> [!NOTE]
> For info about the current availability of the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt).

In your application project, reference the Windows Runtime component's Windows Runtime metadata (`.winmd`) file, and build. During the build, the `cppwinrt.exe` tool generates a standard C++ library that fully describes&mdash;or *projects*&mdash;the API surface for the component. In other words, the generated library contains the projected types for the component.

Then, just as for a Windows namespace type, you include a header and construct the projected type via one of its constructors. Your application project's startup code registers the runtime class, and the projected type's constructor calls [**RoActivateInstance**](https://msdn.microsoft.com/library/br224646) to activate the runtime class from the referenced component.

```cppwinrt
#include "winrt/BankAccountWRC.h"

struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
	BankAccountWRC::BankAccount bankAccount;
	...
};
```

For more details, code, and a walkthrough of consuming APIs implemented in a Windows Runtime component, see [Events; how to author and handle them in C++/WinRT](events-author-handle.md#create-a-core-app-bankaccountcoreapp-to-test-the-windows-runtime-component).

## If the API is implemented in the consuming project
A type that's consumed from XAML UI must be a runtime class, even if it's in the same project as the XAML.

For this scenario, you generate a projected type from the runtime class's Windows Runtime metadata (`.winmd`). Again, you include a header, but this time you construct the projected type via its `nullptr` constructor. That constructor doesn't perform any initialization, so you must next assign a value to the instance via the [**winrt::make**](/uwp/cpp-ref-for-winrt/make) helper function, passing any necessary constructor arguments. A runtime class implemented in the same project as the consuming code doesn't need to be registered, nor instantiated via Windows Runtime/COM activation.

```cppwinrt
// MainPage.h
...
struct MainPage : MainPageT<MainPage>
{
	...
	private:
		Bookstore::BookstoreViewModel m_mainViewModel{ nullptr };
		...
    };
}
...
// MainPage.cpp
...
#include "BookstoreViewModel.h"

MainPage::MainPage()
{
	m_mainViewModel = make<Bookstore::implementation::BookstoreViewModel>();
	...
}
```

For more details, code, and a walkthrough of consuming a runtime class implemented in the consuming project, see [XAML controls; binding to a C++/WinRT property](binding-property.md#add-a-property-of-type-bookstoreviewmodel-to-mainpage).

## Instantiating and returning projected types and interfaces
Here's an example of what projected types and interfaces might look like in your consuming project.

```cppwinrt
struct MyRuntimeClass : MyProject::IMyRuntimeClass, impl::require<MyRuntimeClass,
	Windows::Foundation::IStringable, Windows::Foundation::IClosable>
```

**MyRuntimeClass** is a projected type; projected interfaces include **IMyRuntimeClass**, **IStringable**, and **IClosable**. This topic has shown the different ways in which you can instantiate a projected type. Here's a reminder and summary, using **MyRuntimeClass** as an example.

```cppwinrt
// The runtime class is implemented in another compilation unit (it's either a Windows API,
// or it's implemented in a second- or third-party component).
MyProject::MyRuntimeClass myrc1;

// The runtime class is implemented in the same compilation unit.
MyProject::MyRuntimeClass myrc2{ nullptr };
myrc2 = winrt::make<MyProject::implementation::MyRuntimeClass>();
```

- You can access the members of all of the interfaces of a projected type.
- You can return a projected type to a caller.
- Projected types and interfaces derive from [**winrt::Windows::Foundation::IUnknown**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown). So, you can call [**IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) on a projected type or interface to query for other projected interfaces, which you can also either use or return to a caller. The **as** member function works like [**QueryInterface**](https://msdn.microsoft.com/library/windows/desktop/ms682521).

```cppwinrt
void f(MyProject::MyRuntimeClass const& myrc)
{
	myrc.ToString();
	myrc.Close();
	IClosable iclosable = myrc.as<IClosable>();
	iclosable.Close();
}
```

## Important APIs
* [winrt::make function template](/uwp/cpp-ref-for-winrt/make)
* [winrt::Windows::Foundation::IUnknown::as](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function)

## Related topics
* [Events; how to author and handle them in C++/WinRT](events-author-handle.md#create-a-core-app-bankaccountcoreapp-to-test-the-windows-runtime-component)
* [XAML controls; binding to a C++/WinRT property](binding-property.md#add-a-property-of-type-bookstoreviewmodel-to-mainpage)
