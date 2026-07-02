---
title: Calling and overriding your base type with C++/WinRT
description: How to call and override your base type with C++/WinRT.
ms.date: 07/02/2026
ms.topic: concept-article
keywords: windows 10, standard, c++, cpp, winrt, projection, XAML, access, call, base type, base, type, windows app sdk, winui 3
ms.localizationpriority: medium
---

# Calling and overriding your base type with C++/WinRT

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with [C++/WinRT](./intro-to-using-cpp-with-winrt.md), see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Implementing *overridable* methods, such as **MeasureOverride** and **OnApplyTemplate**

There are some extension points in XAML that your application can plug into, for example:

- [MeasureOverride](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.measureoverride)
- [OnApplyTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.onapplytemplate)
- [GoToStateCore](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visualstatemanager.gotostatecore)

You derive a custom control from the [**Control**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control) runtime class, which itself further derives from base runtime classes. And there are `overridable` methods of **Control**, [**FrameworkElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement), and [**UIElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement) that you can override in your derived class. Here's a code example showing you how to do that.

```cppwinrt
struct BgLabelControl : BgLabelControlT<BgLabelControl>
{
...
    // Control overrides.
    void OnPointerPressed(Microsoft::UI::Xaml::Input::PointerRoutedEventArgs const& /* e */) const { ... };

    // FrameworkElement overrides.
    Windows::Foundation::Size MeasureOverride(Windows::Foundation::Size const& /* availableSize */) const { ... };
    void OnApplyTemplate() const { ... };

    // UIElement overrides.
    Microsoft::UI::Xaml::Automation::Peers::AutomationPeer OnCreateAutomationPeer() const { ... };
...
};
```

*Overridable* methods present themselves differently in different language projections. In C#, for example, overridable methods typically appear as protected virtual methods. In C++/WinRT, they're neither virtual nor protected, but you can still override them and provide your own implementation, as shown above.

If you're overriding one of these overridable methods in C++/WinRT, then your `runtimeclass` IDL mustn't declare the method. For more info about the `base_type` syntax shown, see the next section in this topic ([Calling your base type](#calling-your-base-type)).

**IDL**

```idl
namespace Example
{
    runtimeclass CustomVSM : Microsoft.UI.Xaml.VisualStateManager
    {
        CustomVSM();
        // note that we don't declare GoToStateCore here
    }
}
```

**C++/WinRT**

```cppwinrt
namespace winrt::Example::implementation
{
    struct CustomVSM : CustomVSMT<CustomVSM>
    {
        CustomVSM() {}

        bool GoToStateCore(winrt::Microsoft::UI::Xaml::Controls::Control const& control, winrt::Microsoft::UI::Xaml::FrameworkElement const& templateRoot, winrt::hstring const& stateName, winrt::Microsoft::UI::Xaml::VisualStateGroup const& group, winrt::Microsoft::UI::Xaml::VisualState const& state, bool useTransitions) {
            return base_type::GoToStateCore(control, templateRoot, stateName, group, state, useTransitions);
        }
    };
}
```

## Calling your base type

You can access your base type, and call methods on it, by using the type alias `base_type`. We saw an example of this in the previous section; but you can use `base_type` to access any base class member (not just overridden methods). Here's an example:

```cppwinrt
struct MyDerivedRuntimeClass : MyDerivedRuntimeClassT<MyDerivedRuntimeClass>
{
    ...

    void Foo()
    {
        // Call my base type's Bar method.
        base_type::Bar();
    }
};
```

## Important APIs
* [Control class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control)
