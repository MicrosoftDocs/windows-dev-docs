---
description: A scalar value needs to be wrapped inside a reference class object before being passed to a function that expects **IInspectable**. That wrapping process is known as *boxing* the value.
title: Boxing and unboxing scalar values to IInspectable with C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, control, boxing, scalar, value
ms.localizationpriority: medium
---

# Boxing and unboxing scalar values to IInspectable with C++/WinRT
 
The [**IInspectable interface**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) is the root interface of every runtime class in the Windows Runtime (WinRT). This is an analogous idea to [**IUnknown**](/windows/desktop/api/unknwn/nn-unknwn-iunknown) being at the root of every COM interface and class; and **System.Object** being at the root of every [Common Type System](/dotnet/standard/base-types/common-type-system) class.

In other words, a function that expects **IInspectable** can be passed an instance of any runtime class. But you can't directly pass a scalar value, such as a numeric or text value, to such a function. Instead, a scalar value needs to be wrapped inside a reference class object. That wrapping process is known as *boxing* the value.

> [!IMPORTANT]
> You can box and unbox any type that you can pass to a Windows Runtime API. In other words, a Windows Runtime type. Numeric and text values (strings) are the examples given above. Another example is a `struct` that you define in IDL. If you try to box a regular C++ `struct` (one that's not defined in IDL), then the compiler will remind you that you can box only a Windows Runtime type. A runtime class is a Windows Runtime type, but you can of course pass runtime classes to Windows Runtime APIs without boxing them.

[C++/WinRT](./intro-to-using-cpp-with-winrt.md)  provides the [**winrt::box_value**](/uwp/cpp-ref-for-winrt/box-value) function, which takes a scalar value and returns the value boxed into an **IInspectable**. For unboxing an **IInspectable** back into a scalar value, there are the [**winrt::unbox_value**](/uwp/cpp-ref-for-winrt/unbox-value) and  [**winrt::unbox_value_or**](/uwp/cpp-ref-for-winrt/unbox-value-or) functions.

## Examples of boxing a value
The [**LaunchActivatedEventArgs::Arguments**](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs.Arguments) accessor function returns a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring), which is a scalar value. We can box that **hstring** value and pass it to a function that expects **IInspectable** like this.

```cppwinrt
void App::OnLaunched(LaunchActivatedEventArgs const& e)
{
    ...
    rootFrame.Navigate(winrt::xaml_typename<BlankApp1::MainPage>(), winrt::box_value(e.Arguments()));
    ...
}
```

To set the content property of a XAML [**Button**](/uwp/api/windows.ui.xaml.controls.button), you call the [**Button::Content**](/uwp/api/windows.ui.xaml.controls.contentcontrol.content?) mutator function. To set the content property to a string value, you can use this code.

```cppwinrt
Button().Content(winrt::box_value(L"Clicked"));
```

First, the [**hstring**](/uwp/cpp-ref-for-winrt/hstring) conversion constructor converts the string literal into an **hstring**. Then the overload of **winrt::box_value** that takes an **hstring** is invoked.

## Examples of unboxing an IInspectable
In your own functions that expect **IInspectable**, you can use [**winrt::unbox_value**](/uwp/cpp-ref-for-winrt/unbox-value) to unbox, and you can use [**winrt::unbox_value_or**](/uwp/cpp-ref-for-winrt/unbox-value-or) to unbox with a default value.

```cppwinrt
void Unbox(winrt::Windows::Foundation::IInspectable const& object)
{
    hstring hstringValue = unbox_value<hstring>(object); // Throws if object is not a boxed string.
    hstringValue = unbox_value_or<hstring>(object, L"Default"); // Returns L"Default" if object is not a boxed string.
    float floatValue = unbox_value_or<float>(object, 0.f); // Returns 0.0 if object is not a boxed float.
}
```

## Determine the type of a boxed value
If you receive a boxed value and you're unsure what type it contains (you need to know its type in order to unbox it), then you can query the boxed value for its [**IPropertyValue**](/uwp/api/windows.foundation.ipropertyvalue) interface, and then call **Type** on that. Here's a code example.

`WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
float pi = 3.14f;
auto piInspectable = winrt::box_value(pi);
auto piPropertyValue = piInspectable.as<winrt::Windows::Foundation::IPropertyValue>();
WINRT_ASSERT(piPropertyValue.Type() == winrt::Windows::Foundation::PropertyType::Single);
```

## Important APIs
* [IInspectable interface](/windows/desktop/api/inspectable/nn-inspectable-iinspectable)
* [winrt::box_value function template](/uwp/cpp-ref-for-winrt/box-value)
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt::unbox_value function template](/uwp/cpp-ref-for-winrt/unbox-value)
* [winrt::unbox_value_or function template](/uwp/cpp-ref-for-winrt/unbox-value-or)