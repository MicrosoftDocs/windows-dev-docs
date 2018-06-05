---
author: stevewhims
description: This topic shows how to author C++/WinRT APIs by using the **winrt::implements** base struct, either directly or indirectly.
title: Author APIs with C++/WinRT
ms.author: stwhi
ms.date: 05/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projected, projection, implementation, implement, runtime class, activation
ms.localizationpriority: medium
---

# Author APIs with [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
This topic shows how to author C++/WinRT APIs by using the [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) base struct, either directly or indirectly. Synonyms for *author* in this context are *produce*, or *implement*. This topic covers the following scenarios for implementing APIs on a C++/WinRT type, in this order.

- You're *not* authoring a Windows Runtime class (runtime class); you just want to implement one or more Windows Runtime interfaces for local consumption within your app. You derive directly from **winrt::implements** in this case, and implement functions.
- You *are* authoring a runtime class. You might be authoring a component to be consumed from an app. Or you might be authoring a type to be consumed from XAML user interface (UI), and in that case you're both implementing and consuming a runtime class within the same compilation unit. In these cases, you let the tools generate classes for you that derive from **winrt::implements**.

In both cases, the type that implements your C++/WinRT APIs is called the *implementation type*.

> [!IMPORTANT]
> It's important to distinguish the concept of an implementation type from that of a projected type. The projected type is described in [Consume APIs with C++/WinRT](consume-apis.md).

## If you're *not* authoring a runtime class
The simplest scenario is where you're implementing a Windows Runtime interface for local consumption. You don't need a runtime class; just an ordinary C++ class. For example, you might be writing an app based around [**CoreApplication**](/uwp/api/windows.applicationmodel.core.coreapplication).

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

In Visual Studio, the **Visual C++ Core App (C++/WinRT)** project template illustrates the **CoreApplication** pattern. The pattern begins with passing an implementation of [**Windows::ApplicationModel::Core::IFrameworkViewSource**](/uwp/api/windows.applicationmodel.core.iframeworkviewsource) to [**CoreApplication::Run**](/uwp/api/windows.applicationmodel.core.coreapplication.run).

```cppwinrt
using namespace Windows::ApplicationModel::Core;
int __stdcall wWinMain(HINSTANCE, HINSTANCE, PWSTR, int)
{
    IFrameworkViewSource source = ...
    CoreApplication::Run(source);
}
```

**CoreApplication** uses the interface to create the app's first view. Conceptually, **IFrameworkViewSource** looks like this.

```cppwinrt
struct IFrameworkViewSource : IInspectable
{
    IFrameworkView CreateView();
};
```

Again conceptually, the implementation of **CoreApplication::Run** does this.

```cppwinrt
void Run(IFrameworkViewSource viewSource) const
{
    IFrameworkView view = viewSource.CreateView();
    ...
}
```

So you, as the developer, implement the **IFrameworkViewSource** interface. C++/WinRT has the base struct template [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) to make it easy to implement an interface (or several) without resorting to COM-style programming. You just derive your type from **implements**, and then implement the interface's functions. Here's how.

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

That's taken care of **IFrameworkViewSource**. The next step is to return an object that implements the **IFrameworkView** interface. You can choose to implement that interface on **App**, too. This next code example represents a minimal app that will at least get a window up and running on the desktop.

```cppwinrt
// App.cpp
...
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    IFrameworkView CreateView()
    {
        return *this;
    }

    void Initialize(CoreApplicationView const &) {}

    void Load(hstring const&) {}

    void Run()
    {
        CoreWindow window = CoreWindow::GetForCurrentThread();
        window.Activate();

        CoreDispatcher dispatcher = window.Dispatcher();
        dispatcher.ProcessEvents(CoreProcessEventsOption::ProcessUntilQuit);
    }

    void SetWindow(CoreWindow const & window)
    {
        // Prepare app visuals here
    }

    void Uninitialize() {}
};
...
```

Since your **App** type *is an* **IFrameworkViewSource**, you can just pass one to **Run**.

```cppwinrt
using namespace Windows::ApplicationModel::Core;
int __stdcall wWinMain(HINSTANCE, HINSTANCE, PWSTR, int)
{
    CoreApplication::Run(App{});
}
```

## If you're authoring a runtime class in a Windows Runtime Component
If your type is packaged in a Windows Runtime Component for consumption from an application, then it needs to be a runtime class.

> [!TIP]
> We recommend that you declare each runtime class in its own Interface Definition Language (IDL) (.idl) file, in order to optimize build performance when you edit an IDL file, and for logical correspondence of an IDL file to its generated source code files. Visual Studio merges all of the resulting `.winmd` files into a single file with the same name as the root namespace. That final `.winmd` file will be the one that the consumers of your component will reference.

Here's an example.

```idl
// MyRuntimeClass.idl
namespace MyProject
{
    runtimeclass MyRuntimeClass
    {
        // Declaring a constructor (or constructors) in the IDL causes the runtime class to be
        // activatable from outside the compilation unit.
        MyRuntimeClass();
        String Name;
    }
}
```

This IDL declares a Windows Runtime (runtime) class. A runtime class is a type that can be activated and consumed via modern COM interfaces, typically across executable boundaries. When you add an IDL file to your project, and build, the C++/WinRT toolchain (`midl.exe` and `cppwinrt.exe`) generate an implementation type for you. Using the example IDL above, the implementation type is a C++ struct stub named **winrt::MyProject::implementation::MyRuntimeClass** in source code files named `\MyProject\MyProject\Generated Files\sources\MyRuntimeClass.h` and `MyRuntimeClass.cpp`.

The implementation type looks like this.

```cppwinrt
// MyRuntimeClass.h
...
namespace winrt::MyProject::implementation
{
    struct MyRuntimeClass : MyRuntimeClassT<MyRuntimeClass>
    {
        MyRuntimeClass() = default;

        hstring Name();
        void Name(hstring const& value);
    };
}

// winrt::MyProject::factory_implementation::MyRuntimeClass is here, too.
```

Note the F-bound polymorphism pattern being used (**MyRuntimeClass** uses itself as a template argument to its base, **MyRuntimeClassT**). This is also called the curiously recurring template pattern (CRTP). If you follow the inheritance chain upwards, you'll come across **MyRuntimeClass_base**.

```cppwinrt
template <typename D, typename... I>
struct MyRuntimeClass_base : implements<D, MyProject::IMyRuntimeClass, I...>
```

So, in this scenario, at the root of the inheritance hierarchy is the [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) base struct template once again.

For more details, code, and a walkthrough of authoring APIs in a Windows Runtime component, see [Author events in C++/WinRT](author-events.md#create-a-core-app-bankaccountcoreapp-to-test-the-windows-runtime-component).

## If you're authoring a runtime class to be referenced in your XAML UI
If your type is referenced by your XAML UI, then it needs to be a runtime class, even though it's in the same project as the XAML. Although they are typically activated across executable boundaries, a runtime class can instead be used within the compilation unit that implements it.

In this scenario, you're both authoring *and* consuming the APIs. The procedure for implementing your runtime class is essentially the same as that for a Windows Runtime Component. So, see the previous section&mdash;[If you're authoring a runtime class in a Windows Runtime Component](#if-youre-authoring-a-runtime-class-in-a-windows-runtime-component). The only detail that differs is that, from the IDL, the C++/WinRT toolchain generates not only an implementation type but also a projected type. It's important to appreciate that saying only "**MyRuntimeClass**" in this scenario may be ambiguous; there are several entities with that name, of different kinds.

- **MyRuntimeClass** is the name of a runtime class. But this is really an abstraction: declared in IDL, and implemented in some programming language.
- **MyRuntimeClass** is the name of the C++ struct **winrt::MyProject::implementation::MyRuntimeClass**, which is the C++/WinRT implementation of the runtime class. As we've seen, if there are separate implementing and consuming projects, then this struct exists only in the implementing project. This is *the implementation type*, or *the implementation*. This type is generated (by the `cppwinrt.exe` tool) in the files `\MyProject\MyProject\Generated Files\sources\MyRuntimeClass.h` and `MyRuntimeClass.cpp`.
- **MyRuntimeClass** is the name of the projected type in the form of the C++ struct **winrt::MyProject::MyRuntimeClass**. If there are separate implementing and consuming projects, then this struct exists only in the consuming project. This is *the projected type*, or *the projection*. This type is generated (by `cppwinrt.exe`) in the file `\MyProject\MyProject\Generated Files\winrt\impl\MyProject.2.h`.

![Projected type and implementation type](images/myruntimeclass.png)

Here are the parts of the projected type that are relevant to this topic.

```cppwinrt
// MyProject.2.h
...
namespace winrt::MyProject
{
    struct MyRuntimeClass : MyProject::IMyRuntimeClass
    {
        MyRuntimeClass(std::nullptr_t) noexcept {}
        MyRuntimeClass();
    };
}
```

For an example walkthrough of implementing the **INotifyPropertyChanged** interface on a runtime class, see [XAML controls; bind to a C++/WinRT property](binding-property.md).

The procedure for consuming your runtime class in this scenario is described in [Consume APIs with C++/WinRT](consume-apis.md#if-the-api-is-implemented-in-the-consuming-project).

## Runtime class constructors
Here are some points to take away from the listings we've seen above.

- Each constructor you declare in your IDL causes a constructor to be generated on both your implementation type and on your projected type. IDL-declared constructors are used to consume the runtime class from *a different* compilation unit.
- Whether you have IDL-declared constructor(s) or not, a constructor overload that takes `nullptr_t` is generated on your projected type. Calling the `nullptr_t` constructor is *the first of two steps* in consuming the runtime class from *the same* compilation unit. For more details, and a code example, see [Consume APIs with C++/WinRT](consume-apis.md#if-the-api-is-implemented-in-the-consuming-project).
- If you're consuming the runtime class from *the same* compilation unit, then you can also implement non-default constructors directly on the implementation type (which, remember, is in `MyRuntimeClass.h`).

> [!NOTE]
> If you expect your runtime class to be consumed from a different compilation unit (which is common), then include constructor(s) in your IDL (at least a default constructor). By doing that, you'll also get a factory implementation alongside your implementation type.
> 
> If you want to author and consume your runtime class only within the same compilation unit, then don't declare any constructor(s) in your IDL. You don't need a factory implementation, and one won't be generated. Your implementation type's default constructor will be deleted, but you can easily edit it and default it instead.
> 
> If you want to author and consume your runtime class only within the same compilation unit, and you need constructor parameters, then author the constructor(s) that you need directly on your implementation type.

## Instantiating and returning implementation types and interfaces
For this section, let's take as an example an implementation type named **MyType**, which implements the [**IStringable**](/uwp/api/windows.foundation.istringable) and [**IClosable**](/uwp/api/windows.foundation.iclosable) interfaces.

You can derive **MyType** directly from [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements) (it's not a runtime class).

```cppwinrt
#include <winrt/Windows.Foundation.h>

using namespace winrt;
using namespace Windows::Foundation;

struct MyType : implements<MyType, IStringable, IClosable>
{
    winrt::hstring ToString(){ ... }
    void Close(){}
};
```

Or you can generate it from IDL (it's a runtime class).

```idl
// MyType.idl
namespace MyProject
{
    runtimeclass MyType: Windows.Foundation.IStringable, Windows.Foundation.IClosable
    {
        MyType();
    }    
}
```

To go from **MyType** to an **IStringable** or **IClosable** object that you can use or return as part of your projection, you can call the [**winrt::make**](/uwp/cpp-ref-for-winrt/make) function template. [**make**] returns the implementation type's default interface.

```cppwinrt
IStringable istringable = winrt::make<MyType>();
```

> [!NOTE]
> However, if you're referencing your type from your XAML UI, then there will be both an implementation type and a projected type in the same project. In that case, [**make**] returns an instance of the projected type. For a code example of that scenario, see [XAML controls; bind to a C++/WinRT property](binding-property.md#add-a-property-of-type-bookstoreviewmodel-to-mainpage).

We can only use `istringable` (in the code example above) to call the members of the **IStringable** interface. But a C++/WinRT interface (which is a projected interface) derives from [**winrt::Windows::Foundation::IUnknown**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown). So, you can call [**IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) on it to query for other interfaces, which you can also either use or return.

```cppwinrt
istringable.ToString();
IClosable iclosable = istringable.as<IClosable>();
iclosable.Close();
```

If you need to access all of the implementation's members, and then later return an interface to a caller, then use the [**winrt::make_self**](/uwp/cpp-ref-for-winrt/make-self) function template. **make_self** returns a [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) wrapping the implementation type. You can access the members of all of its interfaces (using the arrow operator), you can return it to a caller as-is, or you can call **as** on it and return the resulting interface object to a caller.

```cppwinrt
winrt::com_ptr<MyType> myimpl = winrt::make_self<MyType>();
myimpl->ToString();
myimpl->Close();
IClosable iclosable = myimpl.as<IClosable>();
iclosable.Close();
```

The **MyType** class is not part of the projection; it's the implementation. But this way you can call its implementation methods directly, without the overhead of a virtual function call. In the example above, even though **MyType::ToString** uses the same signature as the projected method on **IStringable**, we're calling the non-virtual method directly, without crossing the application binary interface (ABI). The **com_ptr** simply holds a pointer to the **MyType** struct, so you can also access any other internal details of **MyType** via the `myimpl` variable and the arrow operator.

In the case where you have an interface object, and you happen to know that it's an interface on your implementation, then you can get back to the implementation using the [**from_abi**](/uwp/cpp-ref-for-winrt/from-abi) function template. Again, it's a technique that avoids virtual function calls, and lets you get directly at the implementation. Here's an example.

```cppwinrt
void ImplFromIClosable(IClosable const& from)
{
    MyType* myimpl = winrt::from_abi<MyType>(from);
    myimpl->ToString();
    myimpl->Close();
}
```

But only the original interface object holds on to a reference. If *you* want to hold on to it, then you can call [**com_ptr::copy_from**](/uwp/cpp-ref-for-winrt/com-ptr#comptrcopyfrom-function).

```cppwinrt
winrt::com_ptr<MyType> impl;
impl.copy_from(winrt::from_abi<MyType>(from));
// com_ptr::copy_from ensures that AddRef is called.
```

The implementation type itself doesn't derive from **winrt::Windows::Foundation::IUnknown**, so it has no **as** function. Even so, you can instantiate one, and access the members of all of its interfaces. But if you do this, then don't return the raw implementation type instance to the caller. Instead, use one of the techniques shown above and return a projected interface, or a **com_ptr**.

```cppwinrt
MyType myimpl;
myimpl.ToString();
myimpl.Close();
IClosable ic1 = myimpl.as<IClosable>(); // error
```

If you have an instance of your implementation type, and you need to pass it to a function that expects the corresponding projected type, then you can do so. A conversion operator exists on your implementation type (provided that the implementation type was generated by the `cppwinrt.exe` tool) that makes this possiblle.

## Deriving from a type that has a non-trivial constructor
[**ToggleButtonAutomationPeer::ToggleButtonAutomationPeer(ToggleButton)**](/uwp/api/windows.ui.xaml.automation.peers.togglebuttonautomationpeer.-ctor#Windows_UI_Xaml_Automation_Peers_ToggleButtonAutomationPeer__ctor_Windows_UI_Xaml_Controls_Primitives_ToggleButton_) is an example of a non-trivial constructor. There's no default constructor so, to construct a **ToggleButtonAutomationPeer**, you need to pass an *owner*. Consequently, if you derive from **ToggleButtonAutomationPeer**, then you need to provide a constructor that takes an *owner* and passes it to the base. Let's see what that looks like in practice.

```idl
// MySpecializedToggleButton.idl
namespace MyNamespace
{
    runtimeclass MySpecializedToggleButton :
        Windows.UI.Xaml.Controls.Primitives.ToggleButton
    {
        ...
    };
}
```

```idl
// MySpecializedToggleButtonAutomationPeer.idl
namespace MyNamespace
{
    runtimeclass MySpecializedToggleButtonAutomationPeer :
        Windows.UI.Xaml.Automation.Peers.ToggleButtonAutomationPeer
    {
        MySpecializedToggleButtonAutomationPeer(MySpecializedToggleButton owner);
    };
}
```

The generated constructor for your implementation type looks like this.

```cppwinrt
// MySpecializedToggleButtonAutomationPeer.cpp
...
MySpecializedToggleButtonAutomationPeer::MySpecializedToggleButtonAutomationPeer
    (MyNamespace::MySpecializedToggleButton const& owner)
{
    ...
}
...
```

The only piece missing is that you need to pass that constructor parameter on to the base class. Remember the F-bound polymorphism pattern that we mentioned above? Once you're familiar with the details of that pattern as used by C++/WinRT, you can figure out what your base class is called (or you can just look in your implementation class's header file). This is how to call the base class constructor in this case.

```cppwinrt
// MySpecializedToggleButtonAutomationPeer.cpp
...
MySpecializedToggleButtonAutomationPeer::MySpecializedToggleButtonAutomationPeer
    (MyNamespace::MySpecializedToggleButton const& owner) : 
    MySpecializedToggleButtonAutomationPeerT<MySpecializedToggleButtonAutomationPeer>(owner)
{
    ...
}
...
```

The base class constructor expects a **ToggleButton**. And **MySpecializedToggleButton** *is a* **ToggleButton**.

Until you make the edit described above (to pass that constructor parameter on to the base class), the compiler will flag your constructor and point out that there's no appropriate default constructor available on a type called (in this case) **MySpecializedToggleButtonAutomationPeer_base&lt;MySpecializedToggleButtonAutomationPeer&gt;**. That's actually the base class of the bass class of your implementation type.

## Important APIs
* [winrt::com_ptr struct template](/uwp/cpp-ref-for-winrt/com-ptr)
* [winrt::com_ptr::copy_from](/uwp/cpp-ref-for-winrt/com-ptr#comptrcopyfrom-function)
* [winrt::from_abi function template](/uwp/cpp-ref-for-winrt/from-abi)
* [winrt::implements struct template](/uwp/cpp-ref-for-winrt/implements)
* [winrt::make function template](/uwp/cpp-ref-for-winrt/make)
* [winrt::make_self function template](/uwp/cpp-ref-for-winrt/make-self)
* [winrt::Windows::Foundation::IUnknown::as](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function)

## Related topics
* [Consume APIs with C++/WinRT](consume-apis.md)
* [XAML controls; bind to a C++/WinRT property](binding-property.md#add-a-property-of-type-bookstoreviewmodel-to-mainpage)
