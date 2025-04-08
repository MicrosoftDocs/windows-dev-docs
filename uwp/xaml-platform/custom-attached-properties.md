---
description: Explains how to implement a XAML attached property as a dependency property and how to define the accessor convention that is necessary for your attached property to be usable in XAML.
title: Custom attached properties
ms.assetid: E9C0C57E-6098-4875-AA3E-9D7B36E160E0
ms.date: 07/18/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
  - csharp
  - vb
  - cppwinrt
  - cpp
---

# Custom attached properties

An *attached property* is a XAML concept. Attached properties are typically defined as a specialized form of dependency property. This topic explains how to implement an attached property as a dependency property and how to define the accessor convention that is necessary for your attached property to be usable in XAML.

## Prerequisites

We assume that you understand dependency properties from the perspective of a consumer of existing dependency properties, and that you have read the [Dependency properties overview](dependency-properties-overview.md). You should also have read [Attached properties overview](attached-properties-overview.md). To follow the examples in this topic, you should also understand XAML and know how to write a basic Windows Runtime app using C++, C#, or Visual Basic.

## Scenarios for attached properties

You might create an attached property when there is a reason to have a property-setting mechanism available for classes other than the defining class. The most common scenarios for this are layout and services support. Examples of existing layout properties are [**Canvas.ZIndex**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/cc190397(v=vs.95)) and [**Canvas.Top**](/dotnet/api/system.windows.controls.canvas.top). In a layout scenario, elements that exist as child elements to layout-controlling elements can express layout requirements to their parent elements individually, each setting a property value that the parent defines as an attached property. An example of the services-support scenario in the Windows Runtime API is set of the attached properties of [**ScrollViewer**](/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer), such as [**ScrollViewer.IsZoomChainingEnabled**](/uwp/api/windows.ui.xaml.controls.scrollviewer.iszoomchainingenabled).

> [!WARNING]
> An existing limitation of the Windows Runtime XAML implementation is that you cannot animate your custom attached property.

## Registering a custom attached property

If you are defining the attached property strictly for use on other types, the class where the property is registered does not have to derive from [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject). But you do need to have the target parameter for accessors use **DependencyObject** if you follow the typical model of having your attached property also be a dependency property, so that you can use the backing property store.

Define your attached property as a dependency property by declaring a **public** **static** **readonly** property of type [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty). You define this property by using the return value of the [**RegisterAttached**](/uwp/api/windows.ui.xaml.dependencyproperty.registerattached) method. The property name must match the attached property name you specify as the **RegisterAttached** *name* parameter, with the string "Property" added to the end. This is the established convention for naming the identifiers of dependency properties in relation to the properties that they represent.

The main area where defining a custom attached property differs from a custom dependency property is in how you define the accessors or wrappers. Instead of the using the wrapper technique described in [Custom dependency properties](custom-dependency-properties.md), you must also provide static **Get**_PropertyName_ and **Set**_PropertyName_ methods as accessors for the attached property. The accessors are used mostly by the XAML parser, although any other caller can also use them to set values in non-XAML scenarios.

> [!IMPORTANT]
> If you don't define the accessors correctly, the XAML processor can't access your attached property and anyone who tries to use it will probably get a XAML parser error. Also, design and coding tools often rely on the "\*Property" conventions for naming identifiers when they encounter a custom dependency property in a referenced assembly.

## Accessors

The signature for the **Get**_PropertyName_ accessor must be this.

`public static` _valueType_ **Get**_PropertyName_ `(DependencyObject target)`

For Microsoft Visual Basic, it is this.

`Public Shared Function Get`_PropertyName_`(ByVal target As DependencyObject) As `_valueType_`)`

The *target* object can be of a more specific type in your implementation, but must derive from [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject). The *valueType* return value can also be of a more specific type in your implementation. The basic **Object** type is acceptable, but often you'll want your attached property to enforce type safety. The use of typing in the getter and setter signatures is a recommended type-safety technique.

The signature for the **Set**_PropertyName_ accessor must be this.

`public static void Set`_PropertyName_` (DependencyObject target , `_valueType_` value)`

For Visual Basic, it is this.

`Public Shared Sub Set`_PropertyName_` (ByVal target As DependencyObject, ByVal value As `_valueType_`)`

The *target* object can be of a more specific type in your implementation, but must derive from [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject). The *value* object and its *valueType* can be of a more specific type in your implementation. Remember that the value for this method is the input that comes from the XAML processor when it encounters your attached property in markup. There must be type conversion or existing markup extension support for the type you use, so that the appropriate type can be created from an attribute value (which is ultimately just a string). The basic **Object** type is acceptable, but often you'll want further type safety. To accomplish that, put type enforcement in the accessors.

> [!NOTE]
> It's also possible to define an attached property where the intended usage is through property element syntax. In that case you don't need type conversion for the values, but you do need to assure that the values you intend can be constructed in XAML. [**VisualStateManager.VisualStateGroups**](/dotnet/api/system.windows.visualstatemanager) is an example of an existing attached property that only supports property element usage.

## Code example

This example shows the dependency property registration (using the [**RegisterAttached**](/uwp/api/windows.ui.xaml.dependencyproperty.registerattached) method), as well as the **Get** and **Set** accessors, for a custom attached property. In the example, the attached property name is `IsMovable`. Therefore, the accessors must be named `GetIsMovable` and `SetIsMovable`. The owner of the attached property is a service class named `GameService` that doesn't have a UI of its own; its purpose is only to provide the attached property services when the **GameService.IsMovable** attached property is used.

Defining the attached property in C++/CX is a bit more complex. You have to decide how to factor between the header and code file. Also, you should expose the identifier as a property with only a **get** accessor, for reasons discussed in [Custom dependency properties](custom-dependency-properties.md). In C++/CX you must define this property-field relationship explicitly rather than relying on .NET **readonly** keywording and implicit backing of simple properties. You also need to perform the registration of the attached property within a helper function that only gets run once, when the app first starts but before any XAML pages that need the attached property are loaded. The typical place to call your property registration helper functions for any and all dependency or attached properties is from within the **App** / [**Application**](/uwp/api/windows.ui.xaml.application.-ctor) constructor in the code for your app.xaml file.

```csharp
public class GameService : DependencyObject
{
    public static readonly DependencyProperty IsMovableProperty = 
    DependencyProperty.RegisterAttached(
      "IsMovable",
      typeof(Boolean),
      typeof(GameService),
      new PropertyMetadata(false)
    );
    public static void SetIsMovable(UIElement element, Boolean value)
    {
        element.SetValue(IsMovableProperty, value);
    }
    public static Boolean GetIsMovable(UIElement element)
    {
        return (Boolean)element.GetValue(IsMovableProperty);
    }
}
```

```vb
Public Class GameService
    Inherits DependencyObject

    Public Shared ReadOnly IsMovableProperty As DependencyProperty = 
        DependencyProperty.RegisterAttached("IsMovable",  
        GetType(Boolean), 
        GetType(GameService), 
        New PropertyMetadata(False))

    Public Shared Sub SetIsMovable(ByRef element As UIElement, value As Boolean)
        element.SetValue(IsMovableProperty, value)
    End Sub

    Public Shared Function GetIsMovable(ByRef element As UIElement) As Boolean
        GetIsMovable = CBool(element.GetValue(IsMovableProperty))
    End Function
End Class
```

```cppwinrt
// GameService.idl
namespace UserAndCustomControls
{
    [default_interface]
    runtimeclass GameService : Windows.UI.Xaml.DependencyObject
    {
        GameService();
        static Windows.UI.Xaml.DependencyProperty IsMovableProperty{ get; };
        static Boolean GetIsMovable(Windows.UI.Xaml.DependencyObject target);
        static void SetIsMovable(Windows.UI.Xaml.DependencyObject target, Boolean value);
    }
}

// GameService.h
...
    static Windows::UI::Xaml::DependencyProperty IsMovableProperty() { return m_IsMovableProperty; }
    static bool GetIsMovable(Windows::UI::Xaml::DependencyObject const& target) { return winrt::unbox_value<bool>(target.GetValue(m_IsMovableProperty)); }
    static void SetIsMovable(Windows::UI::Xaml::DependencyObject const& target, bool value) { target.SetValue(m_IsMovableProperty, winrt::box_value(value)); }

private:
    static Windows::UI::Xaml::DependencyProperty m_IsMovableProperty;
...

// GameService.cpp
...
Windows::UI::Xaml::DependencyProperty GameService::m_IsMovableProperty =
    Windows::UI::Xaml::DependencyProperty::RegisterAttached(
        L"IsMovable",
        winrt::xaml_typename<bool>(),
        winrt::xaml_typename<UserAndCustomControls::GameService>(),
        Windows::UI::Xaml::PropertyMetadata{ winrt::box_value(false) }
);
...
```

```cpp
// GameService.h
#pragma once

#include "pch.h"
//namespace WUX = Windows::UI::Xaml;

namespace UserAndCustomControls {
    public ref class GameService sealed : public WUX::DependencyObject {
    private:
        static WUX::DependencyProperty^ _IsMovableProperty;
    public:
        GameService::GameService();
        void GameService::RegisterDependencyProperties();
        static property WUX::DependencyProperty^ IsMovableProperty
        {
            WUX::DependencyProperty^ get() {
                return _IsMovableProperty;
            }
        };
        static bool GameService::GetIsMovable(WUX::UIElement^ element) {
            return (bool)element->GetValue(_IsMovableProperty);
        };
        static void GameService::SetIsMovable(WUX::UIElement^ element, bool value) {
            element->SetValue(_IsMovableProperty,value);
        }
    };
}

// GameService.cpp
#include "pch.h"
#include "GameService.h"

using namespace UserAndCustomControls;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Documents;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Interop;
using namespace Windows::UI::Xaml::Media;

GameService::GameService() {};

GameService::RegisterDependencyProperties() {
    DependencyProperty^ GameService::_IsMovableProperty = DependencyProperty::RegisterAttached(
         "IsMovable", Platform::Boolean::typeid, GameService::typeid, ref new PropertyMetadata(false));
}
```

## Setting your custom attached property from XAML markup

After you have defined your attached property and included its support members as part of a custom type, you must then make the definitions available for XAML usage. To do this, you must map a XAML namespace that will reference the code namespace that contains the relevant class. In cases where you have defined the attached property as part of a library, you must include that library as part of the app package for the app.

An XML namespace mapping for XAML is typically placed in the root element of a XAML page. For example, for the class named `GameService` in the namespace `UserAndCustomControls` that contains the attached property definitions shown in preceding snippets, the mapping might look like this.

```xaml
<UserControl
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:uc="using:UserAndCustomControls"
  ... >
```

Using the mapping, you can set your `GameService.IsMovable` attached property on any element that matches your target definition, including an existing type that Windows Runtime defines.

```xaml
<Image uc:GameService.IsMovable="True" .../>
```

If you are setting the property on an element that is also within the same mapped XML namespace, you still must include the prefix on the attached property name. This is because the prefix qualifies the owner type. The attached property's attribute cannot be assumed to be within the same XML namespace as the element where the attribute is included, even though, by normal XML rules, attributes can inherit namespace from elements. For example, if you are setting `GameService.IsMovable` on a custom type of `ImageWithLabelControl` (definition not shown), and even if both were defined in the same code namespace mapped to same prefix, the XAML would still be this.

```xaml
<uc:ImageWithLabelControl uc:GameService.IsMovable="True" .../>
```

> [!NOTE]
> If you are writing a XAML UI with C++/CX, then you must include the header for the custom type that defines the attached property, any time that a XAML page uses that type. Each XAML page has an associated code-behind header (.xaml.h). This is where you should include (using **\#include**) the header for the definition of the attached property's owner type.

## Setting your custom attached property imperatively

You can also access a custom attached property from imperative code. The code below shows how.

```xaml
<Image x:Name="gameServiceImage"/>
```

```cppwinrt
// MainPage.h
...
#include "GameService.h"
...

// MainPage.cpp
...
MainPage::MainPage()
{
    InitializeComponent();

    GameService::SetIsMovable(gameServiceImage(), true);
}
...
```

## Value type of a custom attached property

The type that is used as the value type of a custom attached property affects the usage, the definition, or both the usage and definition. The attached property's value type is declared in several places: in the signatures of both the **Get** and **Set** accessor methods, and also as the *propertyType* parameter of the [**RegisterAttached**](/uwp/api/windows.ui.xaml.dependencyproperty.registerattached) call.

The most common value type for attached properties (custom or otherwise) is a simple string. This is because attached properties are generally intended for XAML attribute usage, and using a string as the value type keeps the properties lightweight. Other primitives that have native conversion to string methods, such as integer, double, or an enumeration value, are also common as value types for attached properties. You can use other value types—ones that don't support native string conversion—as the attached property value. However, this entails making a choice about either the usage or the implementation:

- You can leave the attached property as it is, but the attached property can support usage only where the attached property is a property element, and the value is declared as an object element. In this case, the property type does have to support XAML usage as an object element. For existing Windows Runtime reference classes, check the XAML syntax to make sure that the type supports XAML object element usage.
- You can leave the attached property as it is, but use it only in an attribute usage through a XAML reference technique such as a **Binding** or **StaticResource** that can be expressed as a string.

## More about the **Canvas.Left** example

In earlier examples of attached property usages we showed different ways to set the [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) attached property. But what does that change about how a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) interacts with your object, and when does that happen? We'll examine this particular example further, because if you implement an attached property, it's interesting to see what else a typical attached property owner class intends to do with its attached property values if it finds them on other objects.

The main function of a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) is to be an absolute-positioned layout container in UI. The children of a **Canvas** are stored in a base-class defined property [**Children**](/uwp/api/windows.ui.xaml.controls.panel.children). Of all the panels **Canvas** is the only one that uses absolute positioning. It would've bloated the object model of the common [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) type to add properties that might only be of concern to **Canvas** and those particular **UIElement** cases where they are child elements of a **UIElement**. Defining the layout control properties of a **Canvas** to be attached properties that any **UIElement** can use keeps the object model cleaner.

In order to be a practical panel, [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) has behavior that overrides the framework-level [**Measure**](/uwp/api/windows.ui.xaml.uielement.measure) and [**Arrange**](/uwp/api/windows.ui.xaml.uielement.arrange) methods. This is where **Canvas** actually checks for attached property values on its children. Part of both the **Measure** and **Arrange** patterns is a loop that iterates over any content, and a panel has the [**Children**](/uwp/api/windows.ui.xaml.controls.panel.children) property that makes it explicit what's supposed to be considered the child of a panel. So the **Canvas** layout behavior iterates through these children, and makes static [**Canvas.GetLeft**](/uwp/api/windows.ui.xaml.controls.canvas.getleft) and [**Canvas.GetTop**](/uwp/api/windows.ui.xaml.controls.canvas.gettop) calls on each child to see whether those attached properties contain a non-default value (default is 0). These values are then used to absolutely position each child in the **Canvas** available layout space according to the specific values provided by each child, and committed using **Arrange**.

The code looks something like this pseudocode.

```syntax
protected override Size ArrangeOverride(Size finalSize)
{
    foreach (UIElement child in Children)
    {
        double x = (double) Canvas.GetLeft(child);
        double y = (double) Canvas.GetTop(child);
        child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
    }
    return base.ArrangeOverride(finalSize); 
    // real Canvas has more sophisticated sizing
}
```

> [!NOTE]
> For more info on how panels work, see [XAML custom panels overview](/windows/apps/design/layout/custom-panels-overview).

## Related topics

* [**RegisterAttached**](/uwp/api/windows.ui.xaml.dependencyproperty.registerattached)
* [Attached properties overview](attached-properties-overview.md)
* [Custom dependency properties](custom-dependency-properties.md)
* [XAML overview](xaml-overview.md)
