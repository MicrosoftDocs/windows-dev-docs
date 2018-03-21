---
author: jwmsft
description: Explains how to define and implement custom dependency properties for a Windows Runtime app using C++, C#, or Visual Basic.
title: Custom dependency properties
ms.assetid: 5ADF7935-F2CF-4BB6-B1A5-F535C2ED8EF8
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Custom dependency properties


Here we explain how to define and implement your own dependency properties for a Windows Runtime app using C++, C#, or Visual Basic. We list reasons why app developers and component authors might want to create custom dependency properties. We describe the implementation steps for a custom dependency property, as well as some best practices that can improve performance, usability, or versatility of the dependency property.

## Prerequisites


We assume that you have read the [Dependency properties overview](dependency-properties-overview.md) and that you understand dependency properties from the perspective of a consumer of existing dependency properties. To follow the examples in this topic, you should also understand XAML and know how to write a basic Windows Runtime app using C++, C#, or Visual Basic.

## What is a dependency property?


To support styling, data binding, animations, and default values for a property, then it should be implemented as a dependency property. Dependency property values are not stored as fields on the class, they are stored by the xaml framework, and are referenced using a key, which is retrieved when the property is registered with the Windows Runtime property system by calling the [**DependencyProperty.Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) method.   Dependency properties can be used only by types deriving from [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356). But **DependencyObject** is quite high in the class hierarchy, so the majority of classes that are intended for UI and presentation support can support dependency properties. For more information about dependency properties and some of the terminology and conventions used for describing them in this documentation, see [Dependency properties overview](dependency-properties-overview.md).

Examples of dependency properties in the Windows Runtime are: [**Control.Background**](https://msdn.microsoft.com/library/windows/apps/br209395), [**FrameworkElement.Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width), and [**TextBox.Text**](https://msdn.microsoft.com/library/windows/apps/br209702), among many others.

Convention is that each dependency property exposed by a class has a corresponding **public static readonly** property of type [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/br242362) that is exposed on that same class the provides the identifier for the dependency property. The identifier's name follows this convention: the name of the dependency property, with the string "Property" added to the end of the name. For example, the corresponding **DependencyProperty** identifier for the **Control.Background** property is [**Control.BackgroundProperty**](https://msdn.microsoft.com/library/windows/apps/br209396). The identifier stores the information about the dependency property as it was registered, and can then be used for other operations involving the dependency property, such as calling [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361).

##  Property wrappers

Dependency properties typically have a wrapper implementation. Without the wrapper, the only way to get or set the properties would be to use the dependency property utility methods [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) and [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) and to pass the identifier to them as a parameter. This is a rather unnatural usage for something that is ostensibly a property. But with the wrapper, your code and any other code that references the dependency property can use a straightforward object-property syntax that is natural for the language you're using.

If you implement a custom dependency property yourself and want it to be public and easy to call, define the property wrappers too. The property wrappers are also useful for reporting basic information about the dependency property to reflection or static analysis processes. Specifically, the wrapper is where you place attributes such as [**ContentPropertyAttribute**](https://msdn.microsoft.com/library/windows/apps/br228011).

## When to implement a property as a dependency property

Whenever you implement a public read/write property on a class, as long as your class derives from [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356), you have the option to make your property work as a dependency property. Sometimes the typical technique of backing your property with a private field is adequate. Defining your custom property as a dependency property is not always necessary or appropriate. The choice will depend on the scenarios that you intend your property to support.

You might consider implementing your property as a dependency property when you want it to support one or more of these features of the Windows Runtime or of Windows Runtime apps:

-   Setting the property through a [**Style**](https://msdn.microsoft.com/library/windows/apps/br208849)
-   Acting as valid target property for data binding with [**{Binding}**](binding-markup-extension.md)
-   Supporting animated values through a [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/br210490)
-   Reporting when the value of the property has been changed by:
    -   Actions taken by the property system itself
    -   The environment
    -   User actions
    -   Reading and writing styles

## Checklist for defining a dependency property

Defining a dependency property can be thought of as a set of concepts. These concepts are not necessarily procedural steps, because several concepts can be addressed in a single line of code in the implementation. This list gives just a quick overview. We'll explain each concept in more detail later in this topic, and we'll show you example code in several languages.

-   Register the property name with the property system (call [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829)), specifying an owner type and the type of the property value. 
    -  There's a required parameter for [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) that expects property metadata. Specify **null** for this, or if you want property-changed behavior, or a metadata-based default value that can be restored by calling [**ClearValue**](https://msdn.microsoft.com/library/windows/apps/br242357), specify an instance of [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.propertymetadata).
-   Define a [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/br242362) identifier as a **public static readonly** property member on the owner type.
-   Define a wrapper property, following the property accessor model that's used in the language you are implementing. The wrapper property name should match the *name* string that you used in [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829). Implement the **get** and **set** accessors to connect the wrapper with the dependency property that it wraps, by calling [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) and [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) and passing your own property's identifier as a parameter.
-   (Optional) Place attributes such as [**ContentPropertyAttribute**](https://msdn.microsoft.com/library/windows/apps/br228011) on the wrapper.

**Note**  If you are defining a custom attached property, you generally omit the wrapper. Instead, you write a different style of accessor that a XAML processor can use. See [Custom attached properties](custom-attached-properties.md). 

## Registering the property

For your property to be a dependency property, you must register the property into a property store maintained by the Windows Runtime property system.  To register the property, you call the [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) method.

For Microsoft .NET languages (C# and Microsoft Visual Basic) you call [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) within the body of your class (inside the class, but outside any member definitions). The identifier is provided by the [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) method call, as the return value. The [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) call is typically made as a static constructor or as part of the initialization of a **public static readonly** property of type [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/br242362) as part of your class. This property exposes the identifier for your dependency property. Here are examples of the [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) call.

> [!div class="tabbedCodeSnippets"]
```csharp
public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
  "Label",
  typeof(String),
  typeof(ImageWithLabelControl),
  new PropertyMetadata(null)
);
```
```vb
Public Shared ReadOnly LabelProperty As DependencyProperty = 
    DependencyProperty.Register("Label", 
      GetType(String), 
      GetType(ImageWithLabelControl), 
      New PropertyMetadata(Nothing))
```

**Note**  Registering the dependency property as part of the identifier property definition is the typical implementation, but you can also register a dependency property in the class static constructor. This approach may make sense if you need more than one line of code to initialize the dependency property.

For C++, you have options for how you split the implementation between the header and the code file. The typical split is to declare the identifier itself as **public static** property in the header, with a **get** implementation but no **set**. The **get** implementation refers to a private field, which is an uninitialized [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/br242362) instance. You can also declare the wrappers and the **get** and **set** implementations of the wrapper. In this case the header includes some minimal implementation. If the wrapper needs Windows Runtime attribution, attribute in the header too. Put the [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) call in the code file, within a helper function that only gets run when the app initializes the first time. Use the return value of **Register** to fill the static but uninitialized identifiers that you declared in the header, which you initially set to **nullptr** at the root scope of the implementation file.

```cpp
//.h file
//using namespace Windows::UI::Xaml::Controls;
//using namespace Windows::UI::Xaml::Interop;
//using namespace Windows::UI::Xaml;
//using namespace Platform;

public ref class ImageWithLabelControl sealed : public Control
{  
private:
    static DependencyProperty^ _LabelProperty;
...
public:
    static void RegisterDependencyProperties(); 
    static property DependencyProperty^ LabelProperty
    {
        DependencyProperty^ get() {return _LabelProperty;}
    }
...
};
```

```cpp
//.cpp file
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml.Interop;

DependencyProperty^ ImageWithLabelControl::_LabelProperty = nullptr;

// This function is called from the App constructor in App.xaml.cpp 
// to register the properties
void ImageWithLabelControl::RegisterDependencyProperties() 
{ 
    if (_LabelProperty == nullptr) 
    { 
        _LabelProperty = DependencyProperty::Register(
          "Label", Platform::String::typeid, ImageWithLabelControl::typeid, nullptr); 
    } 
}
```

**Note**  For the C++ code, the reason why you have a private field and a public read-only property that surfaces the [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/br242362) is so that other callers who use your dependency property can also use property-system utility APIs that require the identifier to be public. If you keep the identifier private, people can't use these utility APIs. Examples of such API and scenarios include [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) or [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) by choice, [**ClearValue**](https://msdn.microsoft.com/library/windows/apps/br242357), [**GetAnimationBaseValue**](https://msdn.microsoft.com/library/windows/apps/br242358), [**SetBinding**](https://msdn.microsoft.com/library/windows/apps/br244257), and [**Setter.Property**](https://msdn.microsoft.com/library/windows/apps/br208836). You can't use a public field for this, because Windows Runtime metadata rules don't allow for public fields.

## Dependency property name conventions

There are naming conventions for dependency properties; follow them in all but exceptional circumstances. The dependency property itself has a basic name ("Label" in the preceding example) that is given as the first parameter of [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829). The name must be unique within each registering type, and the uniqueness requirement also applies to any inherited members. Dependency properties inherited through base types are considered to be part of the registering type already; names of inherited properties cannot be registered again.

**Caution**  Although the name you provide here can be any string identifier that is valid in programming for your language of choice, you usually want to be able to set your dependency property in XAML too. To be set in XAML, the property name you choose must be a valid XAML name. For more info, see [XAML overview](xaml-overview.md).

When you create the identifier property, combine the name of the property as you registered it with the suffix "Property" ("LabelProperty", for example). This property is your identifier for the dependency property, and it is used as an input for the [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) and [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) calls you make in your own property wrappers. It is also used by the property system and other XAML processors such as [**{x:Bind}**](x-bind-markup-extension.md)

## Implementing the wrapper

Your property wrapper should call [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) in the **get** implementation, and [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) in the **set** implementation.

**Caution**  In all but exceptional circumstances, your wrapper implementations should perform only the [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) and [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) operations. Otherwise, you'll get different behavior when your property is set via XAML versus when it is set via code. For efficiency, the XAML parser bypasses wrappers when setting dependency properties; and talks to the backing store via **SetValue**.

> [!div class="tabbedCodeSnippets"]
```csharp
public String Label
{
    get { return (String)GetValue(LabelProperty); }
    set { SetValue(LabelProperty, value); }
}
```
```vb
Public Property Label() As String 
    Get 
        Return DirectCast(GetValue(LabelProperty), String) 
    End Get 
    Set(ByVal value As String) 
        SetValue(LabelProperty, value) 
    End Set 
End Property
```
```cpp
//using namespace Platform;
public:
...
  property String^ Label
  {
    String^ get() {
      return (String^)GetValue(LabelProperty);
    }
    void set(String^ value) {
      SetValue(LabelProperty, value); 
    }
  }
```

## Property metadata for a custom dependency property

When property metadata is assigned to a dependency property, the same metadata is applied to that property for every instance of the property-owner type or its subclasses. In property metadata, you can specify two behaviors:

-   A default value that the property system assigns to all cases of the property.
-   A static callback method that is automatically invoked within the property system whenever a property value change is detected.

### Calling Register with property metadata

In the previous examples of calling [**DependencyProperty.Register**](https://msdn.microsoft.com/library/windows/apps/hh701829), we passed a null value for the *propertyMetadata* parameter. To enable a dependency property to provide a default value or use a property-changed callback, you must define a [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771) instance that provides one or both of these capabilities.

Typically you provide a [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771) as an inline-created instance, within the parameters for [**DependencyProperty.Register**](https://msdn.microsoft.com/library/windows/apps/hh701829).

**Note**  If you are defining a [**CreateDefaultValueCallback**](https://msdn.microsoft.com/library/windows/apps/hh701812) implementation, you must use the utility method [**PropertyMetadata.Create**](https://msdn.microsoft.com/library/windows/apps/hh702099) rather than calling a [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771) constructor to define the **PropertyMetadata** instance.

This next example modifies the previously shown [**DependencyProperty.Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) examples by referencing a [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771) instance with a [**PropertyChangedCallback**](https://msdn.microsoft.com/library/windows/apps/br208770) value. The implementation of the "OnLabelChanged" callback will be shown later in this section.

> [!div class="tabbedCodeSnippets"]
```csharp
public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
  "Label",
  typeof(String),
  typeof(ImageWithLabelControl),
  new PropertyMetadata(null,new PropertyChangedCallback(OnLabelChanged))
);
```
```vb
Public Shared ReadOnly LabelProperty As DependencyProperty = 
    DependencyProperty.Register("Label", 
      GetType(String), 
      GetType(ImageWithLabelControl), 
      New PropertyMetadata(
        Nothing, new PropertyChangedCallback(AddressOf OnLabelChanged)))
```
```cpp
DependencyProperty^ ImageWithLabelControl::_LabelProperty = 
    DependencyProperty::Register("Label", 
    Platform::String::typeid,
    ImageWithLabelControl::typeid, 
    ref new PropertyMetadata(nullptr,
      ref new PropertyChangedCallback(&ImageWithLabelControl::OnLabelChanged))
    );
```

### Default value

You can specify a default value for a dependency property such that the property always returns a particular default value when it is unset. This value can be different than the inherent default value for the type of that property.

If a default value is not specified, the default value for a dependency property is null for a reference type, or the default of the type for a value type or language primitive (for example, 0 for an integer or an empty string for a string). The main reason for establishing a default value is that this value is restored when you call [**ClearValue**](https://msdn.microsoft.com/library/windows/apps/br242357) on the property. Establishing a default value on a per-property basis might be more convenient than establishing default values in constructors, particularly for value types. However, for reference types, make sure that establishing a default value does not create an unintentional singleton pattern. For more info, see [Best practices](#best-practices) later in this topic

**Note**  Do not register with a default value of [**UnsetValue**](https://msdn.microsoft.com/library/windows/apps/br242371). If you do, it will confuse property consumers and will have unintended consequences within the property system.

### CreateDefaultValueCallback

In some scenarios, you are defining dependency properties for objects that are used on more than one UI thread. This might be the case if you are defining a data object that is used by multiple apps, or a control that you use in more than one app. You can enable the exchange of the object between different UI threads by providing a [**CreateDefaultValueCallback**](https://msdn.microsoft.com/library/windows/apps/hh701812) implementation rather than a default value instance, which is tied to the thread that registered the property. Basically a [**CreateDefaultValueCallback**](https://msdn.microsoft.com/library/windows/apps/hh701812) defines a factory for default values. The value returned by **CreateDefaultValueCallback** is always associated with the current UI **CreateDefaultValueCallback** thread that is using the object.

To define metadata that specifies a [**CreateDefaultValueCallback**](https://msdn.microsoft.com/library/windows/apps/hh701812), you must call [**PropertyMetadata.Create**](https://msdn.microsoft.com/library/windows/apps/hh702115) to return a metadata instance; the [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771) constructors do not have a signature that includes a **CreateDefaultValueCallback** parameter.

The typical implementation pattern for a [**CreateDefaultValueCallback**](https://msdn.microsoft.com/library/windows/apps/hh701812) is to create a new [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356) class, set the specific property value of each property of the **DependencyObject** to the intended default, and then return the new class as an **Object** reference via the return value of the **CreateDefaultValueCallback** method.

### Property-changed callback method

You can define a property-changed callback method to define your property's interactions with other dependency properties, or to update an internal property or state of your object whenever the property changes. If your callback is invoked, the property system has determined that there is an effective property value change. Because the callback method is static, the *d* parameter of the callback is important because it tells you which instance of the class has reported a change. A typical implementation uses the [**NewValue**](https://msdn.microsoft.com/library/windows/apps/br242364) property of the event data and processes that value in some manner, usually by performing some other change on the object passed as *d*. Additional responses to a property change are to reject the value reported by **NewValue**, to restore [**OldValue**](https://msdn.microsoft.com/library/windows/apps/br242365), or to set the value to a programmatic constraint applied to the **NewValue**.

This next example shows a [**PropertyChangedCallback**](https://msdn.microsoft.com/library/windows/apps/br208770) implementation. It implements the method you saw referenced in the previous [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) examples, as part of the construction arguments for the [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771). The scenario addressed by this callback is that the class also has a calculated read-only property named "HasLabelValue" (implementation not shown). Whenever the "Label" property gets reevaluated, this callback method is invoked, and the callback enables the dependent calculated value to remain in synchronization with changes to the dependency property.

> [!div class="tabbedCodeSnippets"]
```csharp
private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
    ImageWithLabelControl iwlc = d as ImageWithLabelControl; //null checks omitted
    String s = e.NewValue as String; //null checks omitted
    if (s == String.Empty)
    {
        iwlc.HasLabelValue = false;
    } else {
        iwlc.HasLabelValue = true;
    }
}
```
```vb
    Private Shared Sub OnLabelChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim iwlc As ImageWithLabelControl = CType(d, ImageWithLabelControl) ' null checks omitted
        Dim s As String = CType(e.NewValue,String) ' null checks omitted
        If s Is String.Empty Then
            iwlc.HasLabelValue = False
        Else
            iwlc.HasLabelValue = True
        End If
    End Sub
```
```cpp
static void OnLabelChanged(DependencyObject^ d, DependencyPropertyChangedEventArgs^ e)
{
    ImageWithLabelControl^ iwlc = (ImageWithLabelControl^)d;
    Platform::String^ s = (Platform::String^)(e->NewValue);
    if (s->IsEmpty()) {
        iwlc->HasLabelValue=false;
    }
}
```

### Property changed behavior for structures and enumerations

If the type of a [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/br242362) is an enumeration or a structure, the callback may be invoked even if the internal values of the structure or the enumeration value did not change. This is different from a system primitive such as a string where it only is invoked if the value changed. This is a side effect of box and unbox operations on these values that is done internally. If you have a [**PropertyChangedCallback**](https://msdn.microsoft.com/library/windows/apps/br208770) method for a property where your value is an enumeration or structure, you need to compare the [**OldValue**](https://msdn.microsoft.com/library/windows/apps/br242365) and [**NewValue**](https://msdn.microsoft.com/library/windows/apps/br242364) by casting the values yourself and using the overloaded comparison operators that are available to the now-cast values. Or, if no such operator is available (which might be the case for a custom structure), you may need to compare the individual values. You would typically choose to do nothing if the result is that the values have not changed.

> [!div class="tabbedCodeSnippets"]
```csharp
private static void OnVisibilityValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
    if ((Visibility)e.NewValue != (Visibility)e.OldValue)
    {
        //value really changed, invoke your changed logic here
    } // else this was invoked because of boxing, do nothing
}
```
```vb
Private Shared Sub OnVisibilityValueChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
    If CType(e.NewValue,Visibility) != CType(e.OldValue,Visibility) Then
        '  value really changed, invoke your changed logic here
    End If
    '  else this was invoked because of boxing, do nothing
End Sub
```
```cpp
static void OnVisibilityValueChanged(DependencyObject^ d, DependencyPropertyChangedEventArgs^ e)
{
    if ((Visibility)e->NewValue != (Visibility)e->OldValue)
    {
        //value really changed, invoke your changed logic here
    } 
    // else this was invoked because of boxing, do nothing
    }
}
```

## Best practices

Keep the following considerations in mind as best practices when as you define your custom dependency property.

### DependencyObject and threading

All [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356) instances must be created on the UI thread which is associated with the current [**Window**](https://msdn.microsoft.com/library/windows/apps/br209041) that is shown by a Windows Runtime app. Although each **DependencyObject** must be created on the main UI thread, the objects can be accessed using a dispatcher reference from other threads, by calling [**Dispatcher**](https://msdn.microsoft.com/library/windows/apps/br230616).

The threading aspects of [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356) are relevant because it generally means that only code that runs on the UI thread can change or even read the value of a dependency property. Threading issues can usually be avoided in typical UI code that makes correct use of **async** patterns and background worker threads. You typically only run into **DependencyObject**-related threading issues if you are defining your own **DependencyObject** types and you attempt to use them for data sources or other scenarios where a **DependencyObject** isn't necessarily appropriate.

### Avoiding unintentional singletons

An unintentional singleton can happen if you are declaring a dependency property that takes a reference type, and you call a constructor for that reference type as part of the code that establishes your [**PropertyMetadata**](https://msdn.microsoft.com/library/windows/apps/br208771). What happens is that all usages of the dependency property share just one instance of **PropertyMetadata** and thus try to share the single reference type you constructed. Any subproperties of that value type that you set through your dependency property then propagate to other objects in ways you may not have intended.

You can use class constructors to set initial values for a reference-type dependency property if you want a non-null value, but be aware that this would be considered a local value for purposes of [Dependency properties overview](dependency-properties-overview.md). It might be more appropriate to use a template for this purpose, if your class supports templates. Another way to avoid a singleton pattern, but still provide a useful default, is to expose a static property on the reference type that provides a suitable default for the values of that class.

### Collection-type dependency properties

Collection-type dependency properties have some additional implementation issues to consider.

Collection-type dependency properties are relatively rare in the Windows Runtime API. In most cases, you can use collections where the items are a [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356) subclass, but the collection property itself is implemented as a conventional CLR or C++ property. This is because collections do not necessarily suit some typical scenarios where dependency properties are involved. For example:

-   You do not typically animate a collection.
-   You do not typically prepopulate the items in a collection with styles or a template.
-   Although binding to collections is a major scenario, a collection does not need to be a dependency property to be a binding source. For binding targets, it is more typical to use subclasses of [**ItemsControl**](https://msdn.microsoft.com/library/windows/apps/br242803) or [**DataTemplate**](https://msdn.microsoft.com/library/windows/apps/br242348) to support collection items, or to use view-model patterns. For more info about binding to and from collections, see [Data binding in depth](https://msdn.microsoft.com/library/windows/apps/mt210946).
-   Notifications for collection changes are better addressed through interfaces such as **INotifyPropertyChanged** or **INotifyCollectionChanged**, or by deriving the collection type from [**ObservableCollection&lt;T&gt;**](https://msdn.microsoft.com/library/windows/apps/ms668604.aspx).

Nevertheless, scenarios for collection-type dependency properties do exist. The next three sections provide some guidance on how to implement a collection-type dependency property.

### Initializing the collection

When you create a dependency property, you can establish a default value by means of dependency property metadata. But be careful to not use a singleton static collection as the default value. Instead, you must deliberately set the collection value to a unique (instance) collection as part of class-constructor logic for the owner class of the collection property.

### Change notifications

Defining the collection as a dependency property does not automatically provide change notification for the items in the collection by virtue of the property system invoking the "PropertyChanged" callback method. If you want notifications for collections or collection items—for example, for a data-binding scenario— implement the **INotifyPropertyChanged** or **INotifyCollectionChanged** interface. For more info, see [Data binding in depth](https://msdn.microsoft.com/library/windows/apps/mt210946).

### Dependency property security considerations

Declare dependency properties as public properties. Declare dependency property identifiers as **public static readonly** members. Even if you attempt to declare other access levels permitted by a language (such as **protected**), a dependency property can always be accessed through the identifier in combination with the property-system APIs. Declaring the dependency property identifier as internal or private will not work, because then the property system cannot operate properly.

Wrapper properties are really just for convenience, Security mechanisms applied to the wrappers can be bypassed by calling [**GetValue**](https://msdn.microsoft.com/library/windows/apps/br242359) or [**SetValue**](https://msdn.microsoft.com/library/windows/apps/br242361) instead. So keep wrapper properties public; otherwise you just make your property harder for legitimate callers to use without providing any real security benefit.

The Windows Runtime does not provide a way to register a custom dependency property as read-only.

### Dependency properties and class constructors

There is a general principle that class constructors should not call virtual methods. This is because constructors can be called to accomplish base initialization of a derived class constructor, and entering the virtual method through the constructor might occur when the object instance being constructed is not yet completely initialized. When you derive from any class that already derives from [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356), remember that the property system itself calls and exposes virtual methods internally as part of its services. To avoid potential problems with run-time initialization, don't set dependency property values within constructors of classes.

### Registering the dependency properties for C++/CX apps

The implementation for registering a property in C++/CX is trickier than C#, both because of the separation into header and implementation file and also because initialization at the root scope of the implementation file is a bad practice. (Visual C++ component extensions (C++/CX) puts static initializer code from the root scope directly into **DllMain**, whereas C# compilers assign the static initializers to classes and thus avoid **DllMain** load lock issues.). The best practice here is to declare a helper function that does all your dependency property registration for a class, one function per class. Then for each custom class your app consumes, you'll have to reference the helper registration function that's exposed by each custom class you want to use. Call each helper registration function once as part of the [**Application constructor**](https://msdn.microsoft.com/library/windows/apps/br242325) (`App::App()`), prior to `InitializeComponent`. That constructor only runs when the app is really referenced for the first time, it won't run again if a suspended app resumes, for example. Also, as seen in the previous C++ registration example, the **nullptr** check around each [**Register**](https://msdn.microsoft.com/library/windows/apps/hh701829) call is important: it's insurance that no caller of the function can register the property twice. A second registration call would probably crash your app without such a check because the property name would be a duplicate. You can see this implementation pattern in the [XAML user and custom controls sample](http://go.microsoft.com/fwlink/p/?linkid=238581) if you look at the code for the C++/CX version of the sample.

## Related topics

* [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/br242356)
* [**DependencyProperty.Register**](https://msdn.microsoft.com/library/windows/apps/hh701829)
* [Dependency properties overview](dependency-properties-overview.md)
* [XAML user and custom controls sample](http://go.microsoft.com/fwlink/p/?linkid=238581)
 

