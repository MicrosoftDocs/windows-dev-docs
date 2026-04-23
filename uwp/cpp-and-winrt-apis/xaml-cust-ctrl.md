---
description: This topic walks you through the steps of creating a simple custom control using C++/WinRT. You can build on the info here to create your own feature-rich and customizable UI controls.
title: XAML custom (templated) controls with C++/WinRT
ms.date: 04/24/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, custom, templated, control
ms.localizationpriority: medium
ms.custom: RS5
---

# XAML custom (templated) controls with C++/WinRT

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with [C++/WinRT](./intro-to-using-cpp-with-winrt.md), see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

One of the most powerful features of the Universal Windows Platform (UWP) is the flexibility that the user-interface (UI) stack provides to create custom controls based on the XAML [**Control**](/uwp/api/windows.ui.xaml.controls.control) type. The XAML UI framework provides features such as [custom dependency properties](../xaml-platform/custom-dependency-properties.md) and [attached properties](../xaml-platform/custom-attached-properties.md), and [control templates](/windows/apps/design/style/xaml-control-templates), which make it easy to create feature-rich and customizable controls. This topic walks you through the steps of creating a custom (templated) control with C++/WinRT.

## Create a Blank App (BgLabelControlApp)

Begin by creating a new project in Microsoft Visual Studio. Create a **Blank App (C++/WinRT)** project, set its name to *BgLabelControlApp*, and (so that your folder structure will match the walkthrough) make sure that **Place solution and project in the same directory** is unchecked. Target the latest generally-available (that is, not preview) version of the Windows SDK.

In a later section of this topic, you'll be directed to build your project (but don't build until then).

> [!NOTE]
> For info about setting up Visual Studio for C++/WinRT development&mdash;including installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support)&mdash;see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

We're going to author a new class to represent a custom (templated) control. We're authoring and consuming the class within the same compilation unit. But we want to be able to instantiate this class from XAML markup, and for that reason it's going to be a runtime class. And we're going to use C++/WinRT to both author and consume it.

The first step in authoring a new runtime class is to add a new **Midl File (.idl)** item to the project. Name it `BgLabelControl.idl`. Delete the default contents of `BgLabelControl.idl`, and paste in this runtime class declaration.

```idl
// BgLabelControl.idl
namespace BgLabelControlApp
{
    runtimeclass BgLabelControl : Windows.UI.Xaml.Controls.Control
    {
        BgLabelControl();
        static Windows.UI.Xaml.DependencyProperty LabelProperty{ get; };
        String Label;
    }
}
```

The listing above shows the pattern that you follow when declaring a dependency property (DP). There are two pieces to each DP. First, you declare a read-only static property of type [**DependencyProperty**](/uwp/api/windows.ui.xaml.dependencyproperty). It has the name of your DP plus *Property*. You'll use this static property in your implementation. Second, you declare a read-write instance property with the type and name of your DP. If you wish to author an *attached property* (rather than a DP), then see the code examples in [Custom attached properties](../xaml-platform/custom-attached-properties.md).

> [!NOTE]
> If you want a DP with a floating-point type, then make it `double` (`Double` in [MIDL 3.0](/uwp/midl-3/)). Declaring and implementing a DP of type `float` (`Single` in MIDL), and then setting a value for that DP in XAML markup, results in the error *Failed to create a 'Windows.Foundation.Single' from the text '\<NUMBER>'*.

Save the file. The project won't build to completion at the moment, but building now is a useful thing to do because it generates the source code files in which you'll implement the **BgLabelControl** runtime class. So go ahead and build now (the build errors you can expect to see at this stage have to do with an "unresolved external symbol").

During the build process, the `midl.exe` tool is run to create a Windows Runtime metadata file (`\BgLabelControlApp\Debug\BgLabelControlApp\Unmerged\BgLabelControl.winmd`) describing the runtime class. Then, the `cppwinrt.exe` tool is run to generate source code files to support you in authoring and consuming your runtime class. These files include stubs to get you started implementing the **BgLabelControl** runtime class that you declared in your IDL. Those stubs are `\BgLabelControlApp\BgLabelControlApp\Generated Files\sources\BgLabelControl.h` and `BgLabelControl.cpp`.

Copy the stub files `BgLabelControl.h` and `BgLabelControl.cpp` from `\BgLabelControlApp\BgLabelControlApp\Generated Files\sources\` into the project folder, which is `\BgLabelControlApp\BgLabelControlApp\`. In **Solution Explorer**, make sure **Show All Files** is toggled on. Right-click the stub files that you copied, and click **Include In Project**.

You'll see a `static_assert` at the top of `BgLabelControl.h` and `BgLabelControl.cpp`, which you'll need to remove. Now the project will build.

## Implement the **BgLabelControl** custom control class
Now, let's open `\BgLabelControlApp\BgLabelControlApp\BgLabelControl.h` and `BgLabelControl.cpp` and implement our runtime class. In `BgLabelControl.h`, change the constructor to set the default style key, implement **Label** and **LabelProperty**, add a static event handler named **OnLabelChanged** to process changes to the value of the dependency property, and add a private member to store the backing field for **LabelProperty**.

After adding those, your `BgLabelControl.h` looks like this. You can copy and paste this code listing to replace the contents of `BgLabelControl.h`.

```cppwinrt
// BgLabelControl.h
#pragma once
#include "BgLabelControl.g.h"

namespace winrt::BgLabelControlApp::implementation
{
    struct BgLabelControl : BgLabelControlT<BgLabelControl>
    {
        BgLabelControl() { DefaultStyleKey(winrt::box_value(L"BgLabelControlApp.BgLabelControl")); }

        winrt::hstring Label()
        {
            return winrt::unbox_value<winrt::hstring>(GetValue(m_labelProperty));
        }

        void Label(winrt::hstring const& value)
        {
            SetValue(m_labelProperty, winrt::box_value(value));
        }

        static Windows::UI::Xaml::DependencyProperty LabelProperty() { return m_labelProperty; }

        static void OnLabelChanged(Windows::UI::Xaml::DependencyObject const&, Windows::UI::Xaml::DependencyPropertyChangedEventArgs const&);

    private:
        static Windows::UI::Xaml::DependencyProperty m_labelProperty;
    };
}
namespace winrt::BgLabelControlApp::factory_implementation
{
    struct BgLabelControl : BgLabelControlT<BgLabelControl, implementation::BgLabelControl>
    {
    };
}
```

In `BgLabelControl.cpp`, define the static members like this. You can copy and paste this code listing to replace the contents of `BgLabelControl.cpp`.

```cppwinrt
// BgLabelControl.cpp
#include "pch.h"
#include "BgLabelControl.h"
#include "BgLabelControl.g.cpp"

namespace winrt::BgLabelControlApp::implementation
{
    Windows::UI::Xaml::DependencyProperty BgLabelControl::m_labelProperty =
        Windows::UI::Xaml::DependencyProperty::Register(
            L"Label",
            winrt::xaml_typename<winrt::hstring>(),
            winrt::xaml_typename<BgLabelControlApp::BgLabelControl>(),
            Windows::UI::Xaml::PropertyMetadata{ winrt::box_value(L"default label"), Windows::UI::Xaml::PropertyChangedCallback{ &BgLabelControl::OnLabelChanged } }
    );

    void BgLabelControl::OnLabelChanged(Windows::UI::Xaml::DependencyObject const& d, Windows::UI::Xaml::DependencyPropertyChangedEventArgs const& /* e */)
    {
        if (BgLabelControlApp::BgLabelControl theControl{ d.try_as<BgLabelControlApp::BgLabelControl>() })
        {
            // Call members of the projected type via theControl.

            BgLabelControlApp::implementation::BgLabelControl* ptr{ winrt::get_self<BgLabelControlApp::implementation::BgLabelControl>(theControl) };
            // Call members of the implementation type via ptr.
        }
    }
}
```

In this walkthrough, we won't be using **OnLabelChanged**. But it's there so that you can see how to register a dependency property with a property-changed callback. The implementation of **OnLabelChanged** also shows how to obtain a derived projected type from a base projected type (the base projected type is **DependencyObject**, in this case). And it shows how to then obtain a pointer to the type that implements the projected type. That second operation will naturally only be possible in the project that implements the projected type (that is, the project that implements the runtime class).

> [!NOTE]
> If you haven't installed the Windows SDK version 10.0.17763.0 (Windows 10, version 1809), or later, then you need to call [**winrt::from_abi**](/uwp/cpp-ref-for-winrt/from-abi) in the dependency property changed event handler above, instead of [**winrt::get_self**](/uwp/cpp-ref-for-winrt/get-self).

## Design the default style for **BgLabelControl**

In its constructor, **BgLabelControl** sets a default style key for itself. But what *is* a default style? A custom (templated) control needs to have a default style&mdash;containing a default control template&mdash;which it can use to render itself with in case the consumer of the control doesn't set a style and/or template. In this section we'll add a markup file to the project containing our default style.

Make sure that **Show All Files** is still toggled on (in **Solution Explorer**). Under your project node, create a new folder (not a filter, but a folder) and name it "Themes". Under `Themes`, add a new item of type **Visual C++** > **XAML** > **XAML View**, and name it "Generic.xaml". The folder and file names have to be like this in order for the XAML framework to find the default style for a custom control. Delete the default contents of `Generic.xaml`, and paste in the markup below.

```xaml
<!-- \Themes\Generic.xaml -->
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:BgLabelControlApp">

    <Style TargetType="local:BgLabelControl" >
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="local:BgLabelControl">
                    <Grid Width="100" Height="100" Background="{TemplateBinding Background}">
                        <TextBlock HorizontalAlignment="Center" VerticalAlignment="Center" Text="{TemplateBinding Label}"/>
                    </Grid>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
</ResourceDictionary>
```

In this case, the only property that the default style sets is the control template. The template consists of a square (whose background is bound to the **Background** property that all instances of the XAML [**Control**](/uwp/api/windows.ui.xaml.controls.control) type have), and a text element (whose text is bound to the **BgLabelControl::Label** dependency property).

## Add an instance of **BgLabelControl** to the main UI page

Open `MainPage.xaml`, which contains the XAML markup for our main UI page. Immediately after the **Button** element (inside the **StackPanel**), add the following markup.

```xaml
<local:BgLabelControl Background="Red" Label="Hello, World!"/>
```

Also, add the following include directive to `MainPage.h` so that the **MainPage** type (a combination of compiling XAML markup and imperative code) is aware of the **BgLabelControl** custom control type. If you want to use **BgLabelControl** from another XAML page, then add this same include directive to the header file for that page, too. Or, alternatively, just put a single include directive in your precompiled header file.

```cppwinrt
// MainPage.h
...
#include "BgLabelControl.h"
...
```

Now build and run the project. You'll see that the default control template is binding to the background brush, and to the label, of the **BgLabelControl** instance in the markup.

This walkthrough showed a simple example of a custom (templated) control in C++/WinRT. You can make your own custom controls arbitrarily rich and full-featured. For example, a custom control can take the form of something as complicated as an editable data grid, a video player, or a visualizer of 3D geometry.

## Implementing *overridable* methods, such as **MeasureOverride** and **OnApplyTemplate**

See the section in [Calling and overriding your base type with C++/WinRT](base-type.md#implementing-overridable-methods-such-as-measureoverride-and-onapplytemplate).

## Important APIs
* [Control class](/uwp/api/windows.ui.xaml.controls.control)
* [DependencyProperty class](/uwp/api/windows.ui.xaml.dependencyproperty)
* [FrameworkElement class](/uwp/api/windows.ui.xaml.frameworkelement)
* [UIElement class](/uwp/api/windows.ui.xaml.uielement)

## Related topics
* [Control templates](/windows/apps/design/style/xaml-control-templates)
* [Custom dependency properties](../xaml-platform/custom-dependency-properties.md)
