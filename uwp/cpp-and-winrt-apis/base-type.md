---
title: Calling and overriding your base type with C++/WinRT
description: How to call and override your base type with C++/WinRT.
ms.date: 09/11/2023
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, access, call, base type, base, type
ms.localizationpriority: medium
---

# Calling and overriding your base type with C++/WinRT

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with [C++/WinRT](./intro-to-using-cpp-with-winrt.md), see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Implementing *overridable* methods, such as **MeasureOverride** and **OnApplyTemplate**

There are some extension points in XAML that your application can plug into, for example:

- [MeasureOverride](/uwp/api/windows.ui.xaml.frameworkelement.measureoverride)
- [OnApplyTemplate](/uwp/api/windows.ui.xaml.frameworkelement.onapplytemplate)
- [GoToStateCore](/uwp/api/windows.ui.xaml.visualstatemanager.gotostatecore)

You derive a custom control from the [**Control**](/uwp/api/windows.ui.xaml.controls.control) runtime class, which itself further derives from base runtime classes. And there are `overridable` methods of **Control**, [**FrameworkElement**](/uwp/api/windows.ui.xaml.frameworkelement), and [**UIElement**](/uwp/api/windows.ui.xaml.uielement) that you can override in your derived class. Here's a code example showing you how to do that.

```cppwinrt
struct BgLabelControl : BgLabelControlT<BgLabelControl>
{
...
    // Control overrides.
    void OnPointerPressed(Windows::UI::Xaml::Input::PointerRoutedEventArgs const& /* e */) const { ... };

    // FrameworkElement overrides.
    Windows::Foundation::Size MeasureOverride(Windows::Foundation::Size const& /* availableSize */) const { ... };
    void OnApplyTemplate() const { ... };

    // UIElement overrides.
    Windows::UI::Xaml::Automation::Peers::AutomationPeer OnCreateAutomationPeer() const { ... };
...
};
```

*Overridable* methods present themselves differently in different language projections. In C#, for example, overridable methods typically appear as protected virtual methods. In C++/WinRT, they're neither virtual nor protected, but you can still override them and provide your own implementation, as shown above.

If you're overriding one of these overridable methods in C++/WinRT, then your `runtimeclass` IDL mustn't declare the method.

**IDL**

```ìdl
namespace Example
{
    runtimeclass CustomVSM : Windows.UI.Xaml.VisualStateManager
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

        bool GoToStateCore(winrt::Windows::UI::Xaml::Controls::Control const& control, winrt::Windows::UI::Xaml::FrameworkElement const& templateRoot, winrt::hstring const& stateName, winrt::Windows::UI::Xaml::VisualStateGroup const& group, winrt::Windows::UI::Xaml::VisualState const& state, bool useTransitions) {
            return base_type::GoToStateCore(control, templateRoot, stateName, group, state, useTransitions);
        }
    };
}
```

## Calling your base type

You can access your bass type, and call methods on it, by using the type alias `base_type`. This is just an example of the syntax of calling `base_type` (this isn't a full illustration of how to perform layout in a custom layout panel):

```cppwinrt
struct CustomGrid : CustomGridT<CustomGrid>
{
    CustomGrid() {}

    Size ArrangeOverride(Size const& finalSize)
    {
        return base_type::ArrangeOverride(finalSize);
    }
};
```

## Important APIs
* [Control class](/uwp/api/windows.ui.xaml.controls.control)
