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

> [!NOTE]
> For info about the current availability of the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt).

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

Since your **App** type *is an* **IFrameworkViewSource**, you just pass one to **Run**.

```cppwinrt
using namespace Windows::ApplicationModel::Core;
int __stdcall wWinMain(HINSTANCE, HINSTANCE, PWSTR, int)
{
    CoreApplication::Run(App{});
}
```

## If you're authoring a runtime class
There are two scenarios in which your type (the type that's implementing a Windows Runtime interface) needs to be a runtime class.

- Your type is packaged in a Windows Runtime Component for consumption from apps, or
- Your type is referenced by your XAML user interface (UI).

You define a runtime class in Interface Definition Language (IDL). From your IDL, the C++/WinRT tooling generates source code stubs in which you implement your type. For background info about authoring (and consuming) runtime classes, see [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md). For an example walkthrough of implementing the **INotifyPropertyChanged** interface on a runtime class, see [XAML controls; binding to a C++/WinRT property](binding-property.md#add-a-property-of-type-bookstoreviewmodel-to-mainpage).

## Important APIs
* [winrt::implements struct template](/uwp/cpp-ref-for-winrt/implements)

## Related topics
* [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md)
* [XAML controls; binding to a C++/WinRT property](binding-property.md#add-a-property-of-type-bookstoreviewmodel-to-mainpage)