---
author: stevewhims
description: C++/WinRT weak reference support is pay-for-play, in that it doesn't cost you anything unless your object is queried for IWeakReferenceSource.
title: Weak references in C++/WinRT
ms.author: stwhi
ms.date: 04/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, weak, reference
ms.localizationpriority: medium
---

# Weak references in C++/WinRT
You should be able, more often than not, to design your own C++/WinRT APIs in such a way as to avoid the need for cyclic references and weak references. However, when it comes to the native implementation of the XAML-based UI frameworkL&mdash;because of the historic design of the framework&mdash;the weak reference mechanism in C++/WinRT is necessary to handle cyclic references. Outside of XAML, it's unlikely you'll need to use weak references (although, there’s nothing XAML-specific about them in theory).

For any given type that you declare, it's not immediately obvious to C++/WinRT whether or when weak references are needed. So, C++/WinRT provides weak reference support automatically on the struct template [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements), from which your own C++/WinRT types directly or indirectly derive. It's pay-for-play, in that it doesn't cost you anything unless your object is actually queried for [**IWeakReferenceSource**](https://msdn.microsoft.com/en-us/library/br224609). And you can choose explicitly to opt out of that support by passing the `no_weak_ref` marker as a template argument to your **winrt::implements** base class.

## Code examples
The [**winrt::weak_ref**](/uwp/cpp-ref-for-winrt/weak-ref) struct template is one option for getting a weak reference to a class instance.

```cppwinrt
Class c;
winrt::weak_ref<Class> weak{ c };
```
Or, you can use the use the [**winrt::make_weak**](/uwp/cpp-ref-for-winrt/make-weak) helper function.

```cppwinrt
Class c;
auto weak = winrt::make_weak(c);
```

Creating a weak reference doesn't affect the reference count on the object itself; it just causes a control block to be allocated. That control block takes care of implementing the weak reference semantics. You can then try to promote the weak reference to a strong reference and, if successful, use it.

```cppwinrt
if (Class strong = weak.get())
{
    // use strong, for example strong.DoWork();
}
```

Provided that some other strong reference still exists, the [**weak_ref::get**](/uwp/cpp-ref-for-winrt/weak-ref#weakrefget-function) call increments the reference count and returns the strong reference to the caller.

## A weak reference to the *this* pointer
A C++/WinRT object directly or indirectly derives from the struct template [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements). The [**implements::get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsgetweak-function) protected member function returns a weak reference to a C++/WinRT object's *this* pointer. [**implements.get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsgetstrong-function) gets a strong reference.

## Events raised after object destruction
In rare cases with XAML UI framework objects, an event is raised on an object that has been finalized. If you encounter this situation, then register your event handler using a lambda that captures a weak reference to the object's *this* pointer. This code example uses the [**SwapChainPanel.CompositionScaleChanged**](/uwp/api/windows.ui.xaml.controls.swapchainpanel.compositionscalechanged) event as an illustration.

```cppwinrt
winrt::Windows::UI::Xaml::Controls::SwapChainPanel m_swapChainPanel;
winrt::event_token m_compositionScaleChangedEventToken;

void RegisterEventHandler()
{
	m_compositionScaleChangedEventToken = m_swapChainPanel.CompositionScaleChanged([weakReferenceToThis{ get_weak() }]
		(Windows::UI::Xaml::Controls::SwapChainPanel const& sender,
		Windows::Foundation::IInspectable const& object)
	{
		if (auto strongReferenceToThis = weakReferenceToThis.get())
		{
			strongReferenceToThis->OnCompositionScaleChanged(sender, object);
		}
	});
}

void OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel const& sender,
	Windows::Foundation::IInspectable const& object)
{
	// Here, we know that the this pointer is valid to use.
}
```

In the lamba capture clause, a temporary variable is created, representing a weak reference to *this*. In the body of the lambda, if a strong reference to *this* can be obtained, then the event handler function is called. In the handler, *this* can safely be used.

## Important APIs
* [implements::get_weak function](/uwp/cpp-ref-for-winrt/implements#implementsgetweak-function)
* [winrt::make_weak function template](/uwp/cpp-ref-for-winrt/make-weak)
* [winrt::weak_ref struct template](/uwp/cpp-ref-for-winrt/weak-ref)
