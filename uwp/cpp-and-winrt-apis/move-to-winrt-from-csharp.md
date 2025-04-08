---
description: This topic comprehensively catalogs the technical details involved in porting the source code in a [C#](/visualstudio/get-started/csharp) project to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md).
title: Move to C++/WinRT from C#
ms.date: 07/15/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C#
ms.localizationpriority: medium
---

# Move to C++/WinRT from C#

> [!TIP]
> If you've read this topic before, and you're returning to it with a particular task in mind, then you can jump to the [Find content based on the task you're performing](#find-content-based-on-the-task-youre-performing) section of this topic.

This topic comprehensively catalogs the technical details involved in porting the source code in a [C#](/visualstudio/get-started/csharp) project to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md).

For a case study of porting one of the Universal Windows Platform (UWP) app samples, see the companion topic [Porting the Clipboard sample to C++/WinRT from C#](./clipboard-to-winrt-from-csharp.md). You can gain porting practice and experience by following along with that walkthrough, and porting the sample for yourself as you go.

## How to prepare, and what to expect

The case study [Porting the Clipboard sample to C++/WinRT from C#](./clipboard-to-winrt-from-csharp.md) illustrates examples of the kinds of software design decisions that you'll make while porting a project to C++/WinRT. So, it's a good idea to prepare for porting by gaining a solid understanding of how the existing code works. That way, you'll get a good overview of the app's functionality, and the code's structure, and then the decisions that you make will always take you forward, and in the right direction.

In terms of what kinds of porting changes to expect, you could group them into four categories.

- [**Port the language projection**](#changes-that-involve-the-language-projection). The Windows Runtime (WinRT) is *projected* into various programming languages. Each of those language projections is designed to feel idiomatic to the programming language in question. For C#, some Windows Runtime types are projected as .NET types. So for example you'll be translating [**System.Collections.Generic.IReadOnlyList\<T\>**](/dotnet/api/system.collections.generic.ireadonlylist-1) back to [**Windows.Foundation.Collections.IVectorView\<T\>**](/uwp/api/windows.foundation.collections.ivectorview-1). Also in C#, some Windows Runtime operations are projected as convenient C# language features. An example is that in C# you use the `+=` operator syntax to register an event-handling delegate. So you'll be translating language features such as that back to the fundamental operation that's being performed (event registration, in this example).
- [**Port language syntax**](#changes-that-involve-the-language-syntax). Many of these changes are simple mechanical transforms, replacing one symbol for another. For example, changing dot (`.`) to double-colon (`::`).
- [**Port language procedure**](#changes-that-involve-procedures-within-the-language). Some of these can be simple, repetitive changes (such as `myObject.MyProperty` to `myObject.MyProperty()`). Others need deeper changes (for example, porting a procedure that involves the use of **System.Text.StringBuilder** to one that involves the use of **std::wostringstream**).
- [**Porting-related tasks that are specific to C++/WinRT**](#porting-related-tasks-that-are-specific-to-cwinrt). Certain details of the Windows Runtime are taken care of implicitly by C#, behind the scenes. Those details are done explicitly in C++/WinRT. An example is that you use an `.idl` file to define your runtime classes.

After the task-based index that follows, the rest of the sections in this topic are structured according to the taxonomy above.

## Find content based on the task you're performing

| Task | Content |
| - | - |
|Author a Windows Runtime component (WRC)|Certain functionality can be achieved (or certain APIs called) only with C++. You can factor that functionality into a C++/WinRT WRC, and then consume the WRC from (for example) a C# app. See [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md) and [If you're authoring a runtime class in a Windows Runtime component](./author-apis.md#if-youre-authoring-a-runtime-class-in-a-windows-runtime-component).|
|Port an async method|It's a good idea for the first line of an asynchronous method in a C++/WinRT runtime class to be `auto lifetime = get_strong();` (see [Safely accessing the *this* pointer in a class-member coroutine](./weak-references.md#safely-accessing-the-this-pointer-in-a-class-member-coroutine)).<br><br>Porting from `Task`, see <a href="#id_async_action">Async action</a>.<br>Porting from `Task<T>`, see <a href="#id_async_operation">Async operation</a>.<br>Porting from `async void`, see <a href="#id_fire_and_forget">Fire-and-forget method</a>.|
|Port a class|First, determine whether the class needs to be a runtime class, or whether it can be an ordinary class. To help you decide that, see the very beginning of [Author APIs with C++/WinRT](./author-apis.md). Then, see the next three rows below.|
|Port a runtime class|A class that shares functionality outside of the C++ app, or a class that's used in XAML data binding. See [If you're authoring a runtime class in a Windows Runtime component](./author-apis.md#if-youre-authoring-a-runtime-class-in-a-windows-runtime-component), or [If you're authoring a runtime class to be referenced in your XAML UI](./author-apis.md#if-youre-authoring-a-runtime-class-to-be-referenced-in-your-xaml-ui).<br><br>Those links describe this in more detail, but a runtime class must be declared in IDL. If your project already contains an IDL file (for example, `Project.idl`), then we recommend that you declare any new runtime class in that file. In IDL, declare any methods and data members that will be used outside your app, or that will be used in XAML. After updating the IDL file, rebuild, and look at the generated stub files (`.h` and `.cpp`) in your project's `Generated Files` folder (In **Solution Explorer**, with the project node selected, make sure **Show All Files** is toggled on). Compare the stub files with the files already in your project, adding files or adding/updating function signatures as necessary. Stub file syntax is always correct, so we recommend that you use it in order to minimize build errors. Once the stubs in your project match those in the stub files, you can go ahead and implement them by porting the C# code over. |
|Port an ordinary class|See [If you're *not* authoring a runtime class](./author-apis.md#if-youre-not-authoring-a-runtime-class).|
|Author IDL|[Introduction to Microsoft Interface Definition Language 3.0](/uwp/midl-3/intro)<br>[If you're authoring a runtime class to be referenced in your XAML UI](./author-apis.md#if-youre-authoring-a-runtime-class-to-be-referenced-in-your-xaml-ui)<br>[Consuming objects from XAML markup](./binding-property.md#consuming-objects-from-xaml-markup)<br>[Define your runtime classes in IDL](#define-your-runtime-classes-in-idl)|
|Port a collection|[Collections with C++/WinRT](./collections.md)<br>[Making a data source available to XAML markup](#making-a-data-source-available-to-xaml-markup)<br><a href="#id_associative_container">Associative container</a><br><a href="#id_vector_member_access">Vector member access</a>|
|Port an event|<a href="#id_event_handler_delegate_as_class_member">Event handler delegate as class member</a><br><a href="#id_revoke_event_handler_delegate">Revoke event handler delegate</a>|
|Port a method|From C#: `private async void SampleButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) { ... }`<br>To the C++/WinRT `.h` file: `fire_and_forget SampleButton_Tapped(IInspectable const&, RoutedEventArgs const&);`<br>To the C++/WinRT `.cpp` file: `fire_and_forget OcrFileImage::SampleButton_Tapped(IInspectable const&, RoutedEventArgs const&) {...}`<br>|
|Port strings|[String handling in C++/WinRT](./strings.md)<br>[ToString](#tostring)<br>[String-building](#string-building)<br>[Boxing and unboxing a string](#boxing-and-unboxing-a-string)|
|Type conversion (type casting)|C#: `o.ToString()`<br>C++/WinRT: `to_hstring(static_cast<int>(o))`<br>Also see [ToString](#tostring).<br><br>C#: `(Value)o`<br>C++/WinRT: `unbox_value<Value>(o)`<br>Throws if unboxing fails. Also see [Boxing and unboxing](./boxing.md).<br><br>C#: `o as Value? ?? fallback`<br>C++/WinRT: `unbox_value_or<Value>(o, fallback)`<br>Returns fallback if unboxing fails. Also see [Boxing and unboxing](./boxing.md).<br><br>C#: `(Class)o`<br>C++/WinRT: `o.as<Class>()`<br>Throws if conversion fails.<br><br>C#: `o as Class`<br>C++/WinRT: `o.try_as<Class>()`<br>Returns null if conversion fails.|

## Changes that involve the language projection

| Category | C# | C++/WinRT | See also |
| -------- | -- | --------- | -------- |
|Untyped object|`object`, or [**System.Object**](/dotnet/api/system.object)|[**Windows::Foundation::IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable)|[Porting the **EnableClipboardContentChangedNotifications** method](./clipboard-to-winrt-from-csharp.md#enableclipboardcontentchangednotifications)|
|Projection namespaces|`using System;`|`using namespace Windows::Foundation;`||
||`using System.Collections.Generic;`|`using namespace Windows::Foundation::Collections;`||
|Size of a collection|`collection.Count`|`collection.Size()`|[Porting the **BuildClipboardFormatsOutputString** method](./clipboard-to-winrt-from-csharp.md#buildclipboardformatsoutputstring)|
|Typical collection type|[**IList\<T\>**](/dotnet/api/system.collections.generic.ilist-1), and **Add** to add an element.|[**IVector\<T\>**](/uwp/api/windows.foundation.collections.ivector-1), and **Append** to add an element. If you use a **std::vector** anywhere, then **push_back** to add an element.||
|Read-only collection type|[**IReadOnlyList\<T\>**](/dotnet/api/system.collections.generic.ireadonlylist-1)|[**IVectorView\<T\>**](/uwp/api/windows.foundation.collections.ivectorview-1)|[Porting the **BuildClipboardFormatsOutputString** method](./clipboard-to-winrt-from-csharp.md#buildclipboardformatsoutputstring)|
|<a name="id_event_handler_delegate_as_class_member"></a>Event handler delegate as class member|`myObject.EventName += Handler;`|`token = myObject.EventName({ get_weak(), &Class::Handler });`|[Porting the **EnableClipboardContentChangedNotifications** method](./clipboard-to-winrt-from-csharp.md#enableclipboardcontentchangednotifications)|
|<a name="id_revoke_event_handler_delegate"></a>Revoke event handler delegate|`myObject.EventName -= Handler;`|`myObject.EventName(token);`|[Porting the **EnableClipboardContentChangedNotifications** method](./clipboard-to-winrt-from-csharp.md#enableclipboardcontentchangednotifications)|
|<a name="id_associative_container"></a>Associative container|[**IDictionary\<K, V\>**](/dotnet/api/system.collections.generic.idictionary-2)|[**IMap\<K, V\>**](/uwp/api/windows.foundation.collections.imap-2)||
|<a name="id_vector_member_access"></a>Vector member access|`x = v[i];`<br>`v[i] = x;`|`x = v.GetAt(i);`<br>`v.SetAt(i, x);`||

### Register/revoke an event handler

In C++/WinRT, you have several syntactic options to register/revoke an event handler delegate, as described in [Handle events by using delegates in C++/WinRT](./handle-events.md). Also see [Porting the **EnableClipboardContentChangedNotifications** method](./clipboard-to-winrt-from-csharp.md#enableclipboardcontentchangednotifications).

Sometimes, for example when an event recipient (an object handling an event) is about to be destroyed, you'll want to revoke an event handler so that the event source (the object raising the event) doesn't call into a destroyed object. See [Revoke a registered delegate](./handle-events.md#revoke-a-registered-delegate). In cases like that, create an **event_token** member variable for your event handlers. For an example, see [Porting the **EnableClipboardContentChangedNotifications** method](./clipboard-to-winrt-from-csharp.md#enableclipboardcontentchangednotifications).

You can also register an event handler in XAML markup.

```xaml
<Button x:Name="OpenButton" Click="OpenButton_Click" />
```

In C#, your **OpenButton_Click** method can be private, and XAML will still be able to connect it to the [**ButtonBase.Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event raised by *OpenButton*.

In C++/WinRT, your **OpenButton_Click** method must be public in your [implementation type](./author-apis.md) *if you want to register it in XAML markup*. If you register an event handler only in imperative code, then the event handler doesn't need to be public.

```cppwinrt
namespace winrt::MyProject::implementation
{
    struct MyPage : MyPageT<MyPage>
    {
        void OpenButton_Click(
            winrt::Windows:Foundation::IInspectable const& sender,
            winrt::Windows::UI::Xaml::RoutedEventArgs const& args);
    }
};
```

Alternatively, you can make the registering XAML page a friend of your implementation type, and **OpenButton_Click** private.

```cppwinrt
namespace winrt::MyProject::implementation
{
    struct MyPage : MyPageT<MyPage>
    {
    private:
        friend MyPageT;
        void OpenButton_Click(
            winrt::Windows:Foundation::IInspectable const& sender,
            winrt::Windows::UI::Xaml::RoutedEventArgs const& args);
    }
};
```

One final scenario is where the C# project that you're porting *binds* to the event handler from markup (for more background on that scenario, see [Functions in x:Bind](../data-binding/function-bindings.md)).

```xaml
<Button x:Name="OpenButton" Click="{x:Bind OpenButton_Click}" />
```

You could just change that markup to the more simple `Click="OpenButton_Click"`. Or, if you prefer, you can keep that markup as it is. All you have to do to support it is to declare the event handler in IDL.

```idl
void OpenButton_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e);
```

> [!NOTE]
> Declare the function as `void` even if you *implement* it as [Fire and forget](./concurrency-2.md#fire-and-forget).

## Changes that involve the language syntax

| Category | C# | C++/WinRT | See also |
| -------- | -- | --------- | -------- |
|Access modifiers|`public \<member\>`|`public:`<br>&nbsp;&nbsp;&nbsp;&nbsp;`\<member\>`|[Porting the **Button_Click** method](./clipboard-to-winrt-from-csharp.md#button_click)|
|Access a data member|`this.variable`|`this->variable`|&nbsp;|
|<a name="id_async_action"></a>Async action|`async Task ...`|`IAsyncAction ...`| [**IAsyncAction** interface](/uwp/api/windows.foundation.iasyncaction), [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md) |
|<a name="id_async_operation"></a>Async operation|`async Task<T> ...`|`IAsyncOperation<T> ...`| [**IAsyncOperation** interface](/uwp/api/windows.foundation.iasyncoperation-1), [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md) |
|<a name="id_fire_and_forget"></a>Fire-and-forget method (implies async)|`async void ...`|`winrt::fire_and_forget ...`|[Porting the **CopyButton_Click** method](./clipboard-to-winrt-from-csharp.md#copybutton_click), [Fire and forget](./concurrency-2.md#fire-and-forget)|
|Access an enumerated constant|`E.Value`|`E::Value`|[Porting the **DisplayChangedFormats** method](./clipboard-to-winrt-from-csharp.md#displaychangedformats)|
|Cooperatively wait|`await ...`|`co_await ...`|[Porting the **CopyButton_Click** method](./clipboard-to-winrt-from-csharp.md#copybutton_click)|
|Collection of projected types as a private field|`private List<MyRuntimeClass> myRuntimeClasses = new List<MyRuntimeClass>();`|`std::vector`<br>`<MyNamespace::MyRuntimeClass>`<br>`m_myRuntimeClasses;`||
|GUID construction|`private static readonly Guid myGuid = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");`|`winrt::guid myGuid{ 0xC380465D, 0x2271, 0x428C, { 0x9B, 0x83, 0xEC, 0xEA, 0x3B, 0x4A, 0x85, 0xC1} };`||
|Namespace separator|`A.B.T`|`A::B::T`||
|Null|`null`|`nullptr`|[Porting the **UpdateStatus** method](./clipboard-to-winrt-from-csharp.md#updatestatus)|
|Obtain a type object|`typeof(MyType)`|`winrt::xaml_typename<MyType>()`|[Porting the **Scenarios** property](./clipboard-to-winrt-from-csharp.md#scenarios)|
|Parameter declaration for a method|`MyType`|`MyType const&`|[Parameter-passing](./concurrency.md#parameter-passing)|
|Parameter declaration for an async method|`MyType`|`MyType`|[Parameter-passing](./concurrency.md#parameter-passing)|
|Call a static method|`T.Method()`|`T::Method()`||
|Strings|`string`, or **System.String**|[**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring)|[String handling in C++/WinRT](./strings.md)|
|String literal|`"a string literal"`|`L"a string literal"`|[Porting the constructor, **Current**, and **FEATURE_NAME**](./clipboard-to-winrt-from-csharp.md#the-constructor-current-and-feature_name)|
|Inferred (or deduced) type|`var`|`auto`|[Porting the **BuildClipboardFormatsOutputString** method](./clipboard-to-winrt-from-csharp.md#buildclipboardformatsoutputstring)|
|Using-directive|`using A.B.C;`|`using namespace A::B::C;`|[Porting the constructor, **Current**, and **FEATURE_NAME**](./clipboard-to-winrt-from-csharp.md#the-constructor-current-and-feature_name)|
|Verbatim/raw string literal|`@"verbatim string literal"`|`LR"(raw string literal)"`|[Porting the **DisplayToast** method](./clipboard-to-winrt-from-csharp.md#displaytoast)|

> [!NOTE]
> If a header file doesn't contain a `using namespace` directive for a given namespace, then you'll have to fully-qualify all type names for that namespace; or at least qualify them sufficiently for the compiler to find them. For an example, see [Porting the **DisplayToast** method](./clipboard-to-winrt-from-csharp.md#displaytoast).

### Porting classes and members

You'll need to decide, for each C# type, whether to port it to a Windows Runtime type, or to a regular C++ class/struct/enumeration. For more info, and detailed examples illustrating how to make those decisions, see [Porting the Clipboard sample to C++/WinRT from C#](./clipboard-to-winrt-from-csharp.md).

A C# property typically becomes an accessor function, a mutator function, and a backing data member. For more info, and an example, see [Porting the **IsClipboardContentChangedEnabled** property](./clipboard-to-winrt-from-csharp.md#isclipboardcontentchangedenabled).

For non-static fields, make them data members of your [implementation type](./author-apis.md).

A C# static field becomes a C++/WinRT static accessor and/or mutator function. For more info, and an example, see [Porting the constructor, **Current**, and **FEATURE_NAME**](./clipboard-to-winrt-from-csharp.md#the-constructor-current-and-feature_name).

For member functions, again, you'll need to decide for each one whether or not it belongs in the IDL, or whether it's a public or private member function of your implementation type. For more info, and examples of how to decide, see [IDL for the **MainPage** type](./clipboard-to-winrt-from-csharp.md#idl-for-the-mainpage-type).

### Porting XAML markup, and asset files

In the case of [Porting the Clipboard sample to C++/WinRT from C#](./clipboard-to-winrt-from-csharp.md), we were able to use *the same* XAML markup (including resources) and asset files across the C# and the C++/WinRT project. In some cases, edits to markup will be necessary to achieve that. See [Copy the XAML and styles necessary to finish up porting **MainPage**](./clipboard-to-winrt-from-csharp.md#copy-the-xaml-and-styles-necessary-to-finish-up-porting-mainpage).

## Changes that involve procedures within the language

| Category | C# | C++/WinRT | See also |
| -------- | -- | --------- | -------- |
|Lifetime management in an async method|N/A|`auto lifetime{ get_strong() };` or<br>`auto lifetime = get_strong();`|[Porting the **CopyButton_Click** method](./clipboard-to-winrt-from-csharp.md#copybutton_click)|
|Disposal|`using (var t = v)`|`auto t{ v };`<br>`t.Close(); // or let wrapper destructor do the work`|[Porting the **CopyImage** method](./clipboard-to-winrt-from-csharp.md#copyimage)|
|Construct object|`new MyType(args)`|`MyType{ args }` or<br>`MyType(args)`|[Porting the **Scenarios** property](./clipboard-to-winrt-from-csharp.md#scenarios)|
|Create uninitialized reference|`MyType myObject;`|`MyType myObject{ nullptr };` or<br>`MyType myObject = nullptr;`|[Porting the constructor, **Current**, and **FEATURE_NAME**](./clipboard-to-winrt-from-csharp.md#the-constructor-current-and-feature_name)|
|Construct object into variable with args|`var myObject = new MyType(args);`|`auto myObject{ MyType{ args } };` or <br>`auto myObject{ MyType(args) };` or <br>`auto myObject = MyType{ args };` or <br>`auto myObject = MyType(args);` or <br>`MyType myObject{ args };` or <br>`MyType myObject(args);`|[Porting the **Footer_Click** method](./clipboard-to-winrt-from-csharp.md#footer_click)|
|Construct object into variable without args|`var myObject = new T();`|`MyType myObject;`|[Porting the **BuildClipboardFormatsOutputString** method](./clipboard-to-winrt-from-csharp.md#buildclipboardformatsoutputstring)|
|Object initialization shorthand|`var p = new FileOpenPicker{`<br>&nbsp;&nbsp;&nbsp;&nbsp;`ViewMode = PickerViewMode.List`<br>`};`|`FileOpenPicker p;`<br>`p.ViewMode(PickerViewMode::List);`||
|Bulk vector operation|`var p = new FileOpenPicker{`<br>&nbsp;&nbsp;&nbsp;&nbsp;`FileTypeFilter = { ".png", ".jpg", ".gif" }`<br>`};`|`FileOpenPicker p;`<br>`p.FileTypeFilter().ReplaceAll({ L".png", L".jpg", L".gif" });`|[Porting the **CopyButton_Click** method](./clipboard-to-winrt-from-csharp.md#copybutton_click)|
|Iterate over collection|`foreach (var v in c)`|`for (auto&& v : c)`|[Porting the **BuildClipboardFormatsOutputString** method](./clipboard-to-winrt-from-csharp.md#buildclipboardformatsoutputstring)|
|Catch an exception|`catch (Exception ex)`|`catch (winrt::hresult_error const& ex)`|[Porting the **PasteButton_Click** method](./clipboard-to-winrt-from-csharp.md#pastebutton_click)|
|Exception details|`ex.Message`|`ex.message()`|[Porting the **PasteButton_Click** method](./clipboard-to-winrt-from-csharp.md#pastebutton_click)|
|Get a property value|`myObject.MyProperty`|`myObject.MyProperty()`|[Porting the **NotifyUser** method](./clipboard-to-winrt-from-csharp.md#notifyuser)|
|Set a property value|`myObject.MyProperty = value;`|`myObject.MyProperty(value);`||
|Increment a property value|`myObject.MyProperty += v;`|`myObject.MyProperty(thing.Property() + v);`<br>For strings, switch to a builder||
|ToString()|`myObject.ToString()`|`winrt::to_hstring(myObject)`|[ToString()](#tostring)|
|Language string to Windows Runtime string|N/A|`winrt::hstring{ s }`||
|String-building|`StringBuilder builder;`<br>`builder.Append(...);`|`std::wostringstream builder;`<br>`builder << ...;`|[String-building](#string-building)|
|String interpolation|`$"{i++}) {s.Title}"`|[**winrt::to_hstring**](/uwp/cpp-ref-for-winrt/to-hstring), and/or [**winrt::hstring::operator+**](/uwp/cpp-ref-for-winrt/hstring#operator-concatenation-operator)|[Porting the **OnNavigatedTo** method](./clipboard-to-winrt-from-csharp.md#onnavigatedto)|
|Empty string for comparison|**System.String.Empty**|[**winrt::hstring::empty**](/uwp/cpp-ref-for-winrt/hstring#hstringempty-function)|[Porting the **UpdateStatus** method](./clipboard-to-winrt-from-csharp.md#updatestatus)|
|Create empty string|`var myEmptyString = String.Empty;`|`winrt::hstring myEmptyString{ L"" };`||
|Dictionary operations|`map[k] = v; // replaces any existing`<br>`v = map[k]; // throws if not present`<br>`map.ContainsKey(k)`|`map.Insert(k, v); // replaces any existing`<br>`v = map.Lookup(k); // throws if not present`<br>`map.HasKey(k)`||
|Type conversion (throw on failure)|`(MyType)v`|`v.as<MyType>()`|[Porting the **Footer_Click** method](./clipboard-to-winrt-from-csharp.md#footer_click)|
|Type conversion (null on failure)|`v as MyType`|`v.try_as<MyType>()`|[Porting the **PasteButton_Click** method](./clipboard-to-winrt-from-csharp.md#pastebutton_click)|
|XAML elements with x:Name are properties|`MyNamedElement`|`MyNamedElement()`|[Porting the constructor, **Current**, and **FEATURE_NAME**](./clipboard-to-winrt-from-csharp.md#the-constructor-current-and-feature_name)|
|Switch to the UI thread|**CoreDispatcher.RunAsync**|**CoreDispatcher.RunAsync**, or [**winrt::resume_foreground**](/uwp/cpp-ref-for-winrt/resume-foreground)|[Porting the **NotifyUser** method](./clipboard-to-winrt-from-csharp.md#notifyuser), and [Porting the **HistoryAndRoaming** method](./clipboard-to-winrt-from-csharp.md#historyandroaming)|
|UI element construction in imperative code in a XAML page|See [UI element construction](#ui-element-construction)|See [UI element construction](#ui-element-construction)||

The following sections go into more detail regarding some of the items in the table.

### UI element construction

These code examples show the construction of a UI element in the imperative code of a XAML page.

```csharp
var myTextBlock = new TextBlock()
{
    Text = "Text",
    Style = (Windows.UI.Xaml.Style)this.Resources["MyTextBlockStyle"]
};
```

```cppwinrt
TextBlock myTextBlock;
myTextBlock.Text(L"Text");
myTextBlock.Style(
    winrt::unbox_value<Windows::UI::Xaml::Style>(
        Resources().Lookup(
            winrt::box_value(L"MyTextBlockStyle")
        )
    )
);
```

### ToString()

C# types provide the [Object.ToString](/dotnet/api/system.object.tostring) method.

```csharp
int i = 2;
var s = i.ToString(); // s is a System.String with value "2".
```

C++/WinRT doesn't directly provide this facility, but you can turn to alternatives.

```cppwinrt
int i{ 2 };
auto s{ std::to_wstring(i) }; // s is a std::wstring with value L"2".
```

C++/WinRT also supports [**winrt::to_hstring**](/uwp/cpp-ref-for-winrt/to-hstring) for a limited number of types. You'll need to add overloads for any additional types you want to stringify.

| Language | Stringify int | Stringify enum |
| - | - | - |
| C# | `string result = "hello, " + intValue.ToString();`<br>`string result = $"hello, {intValue}";` | `string result = "status: " + status.ToString();`<br>`string result = $"status: {status}";` |
| C++/WinRT | `hstring result = L"hello, " + to_hstring(intValue);` | `// must define overload (see below)`<br>`hstring result = L"status: " + to_hstring(status);` |

In the case of stringifying an enum, you will need to provide the implementation of **winrt::to_hstring**.

```cppwinrt
namespace winrt
{
    hstring to_hstring(StatusEnum status)
    {
        switch (status)
        {
        case StatusEnum::Success: return L"Success";
        case StatusEnum::AccessDenied: return L"AccessDenied";
        case StatusEnum::DisabledByPolicy: return L"DisabledByPolicy";
        default: return to_hstring(static_cast<int>(status));
        }
    }
}
```

These stringifications are often consumed implicitly by data binding.

```xaml
<TextBlock>
You have <Run Text="{Binding FlowerCount}"/> flowers.
</TextBlock>
<TextBlock>
Most recent status is <Run Text="{x:Bind LatestOperation.Status}"/>.
</TextBlock>
```

These bindings will perform **winrt::to_hstring** of the bound property. In the case of the second example (the **StatusEnum**), you must provide your own overload of **winrt::to_hstring**, otherwise you'll get a compiler error.

Also see [Porting the **Footer_Click** method](./clipboard-to-winrt-from-csharp.md#footer_click).

### String-building

For string building, C# has a built-in [**StringBuilder**](/dotnet/api/system.text.stringbuilder) type.

| Category | C# | C++/WinRT |
| -------- | -- | --------- |
| String-building | `StringBuilder builder;`<br>`builder.Append(...);` | `std::wostringstream builder;`<br>`builder << ...;` |
| Append a Windows Runtime string, preserving nulls | `builder.Append(s);` | `builder << std::wstring_view{ s };` |
| Add a newline |`builder.Append(Environment.NewLine);` | `builder << std::endl;` |
| Access the result | `s = builder.ToString();` | `ws = builder.str();` |

Also see [Porting the **BuildClipboardFormatsOutputString** method](./clipboard-to-winrt-from-csharp.md#buildclipboardformatsoutputstring), and [Porting the **DisplayChangedFormats** method](./clipboard-to-winrt-from-csharp.md#displaychangedformats).

### Running code on the main UI thread 

This example is taken from the [Barcode scanner sample](/samples/microsoft/windows-universal-samples/barcodescanner/).

When you want to do work on the main UI thread in a C# project, you typically use the [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) method, like this.

```csharp
private async void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        // Do work on the main UI thread here.
    });
}
```

It's much simpler to express that in C++/WinRT. Notice that we're accepting parameters by value on the assumption we'll want to access them after the first suspension point (the `co_await`, in this case). For more info, see [Parameter-passing](./concurrency.md#parameter-passing).

```cppwinrt
winrt::fire_and_forget Watcher_Added(DeviceWatcher sender, winrt::DeviceInformation args)
{
    co_await Dispatcher();
    // Do work on the main UI thread here.
}
```

If you need to do the work at a priority other than the default, then see the [**winrt::resume_foreground**](/uwp/cpp-ref-for-winrt/resume-foreground) function, which has an overload that takes a priority. For code examples showing how to await a call to **winrt::resume_foreground**, see [Programming with thread affinity in mind](./concurrency-2.md#programming-with-thread-affinity-in-mind).

## Porting-related tasks that are specific to C++/WinRT

### Define your runtime classes in IDL

See [IDL for the **MainPage** type](./clipboard-to-winrt-from-csharp.md#idl-for-the-mainpage-type), and [Consolidate your `.idl` files](./clipboard-to-winrt-from-csharp.md#consolidate-your-idl-files).

### Include the C++/WinRT Windows namespace header files that you need

In C++/WinRT, whenever you want to use a type from a Windows namespaces, you need to include the corresponding C++/WinRT Windows namespace header file. For an example, see [Porting the **NotifyUser** method](./clipboard-to-winrt-from-csharp.md#notifyuser).

### Boxing and unboxing

C# automatically boxes scalars into objects. C++/WinRT requires you to call the [**winrt::box_value**](/uwp/cpp-ref-for-winrt/box-value) function explicitly. Both languages require you to unbox explicitly. See [Boxing and unboxing with C++/WinRT](./boxing.md).

In the tables that follows, we'll use these definitions.

| C# | C++/WinRT|
|-|-|
| `int i;` | `int i;` |
| `string s;` | `winrt::hstring s;` |
| `object o;` | `IInspectable o;`|

| Operation | C# | C++/WinRT|
|-|-|-|
| Boxing | `o = 1;`<br>`o = "string";` | `o = box_value(1);`<br>`o = box_value(L"string");` |
| Unboxing | `i = (int)o;`<br>`s = (string)o;` | `i = unbox_value<int>(o);`<br>`s = unbox_value<winrt::hstring>(o);` |

C++/CX and C# raise exceptions if you try to unbox a null pointer to a value type. C++/WinRT considers this a programming error, and it crashes. In C++/WinRT, use the [**winrt::unbox_value_or**](/uwp/cpp-ref-for-winrt/unbox-value-or) function if you want to handle the case where the object is not of the type that you thought it was.

| Scenario | C# | C++/WinRT|
|-|-|-|
| Unbox a known integer |`i = (int)o;` | `i = unbox_value<int>(o);` |
| If o is null | `System.NullReferenceException` | Crash |
| If o is not a boxed int | `System.InvalidCastException` | Crash |
| Unbox int, use fallback if null; crash if anything else | `i = o != null ? (int)o : fallback;` | `i = o ? unbox_value<int>(o) : fallback;` |
| Unbox int if possible; use fallback for anything else | `i = as int? ?? fallback;` | `i = unbox_value_or<int>(o, fallback);` |

For an example, see [Porting the **OnNavigatedTo** method](./clipboard-to-winrt-from-csharp.md#onnavigatedto), and [Porting the **Footer_Click** method](./clipboard-to-winrt-from-csharp.md#footer_click).

#### Boxing and unboxing a string

A string is in some ways a value type, and in other ways a reference type. C# and C++/WinRT treat strings differently.

The ABI type [**HSTRING**](/windows/win32/winrt/hstring) is a pointer to a reference-counted string. But it doesn't derive from [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable), so it's not technically an *object*. Furthermore, a null **HSTRING** represents the empty string. Boxing of things not derived from **IInspectable** is done by wrapping them inside an [**IReference\<T\>**](/uwp/api/windows.foundation.ireference_t_), and the Windows Runtime provides a standard implementation in the form of the [**PropertyValue**](/uwp/api/windows.foundation.propertyvalue) object (custom types are reported as [**PropertyType::OtherType**](/uwp/api/windows.foundation.propertytype)).

C# represents a Windows Runtime string as a reference type; while C++/WinRT projects a string as a value type. This means that a boxed null string can have different representations depending how you got there.

| Behavior | C# | C++/WinRT|
|-|-|-|
| Declarations | `object o;`<br>`string s;` | `IInspectable o;`<br>`hstring s;` |
| String type category | Reference type | Value type |
| null **HSTRING** projects as | `""` | `hstring{}` |
| Are null and `""` identical? | No | Yes |
| Validity of null | `s = null;`<br>`s.Length` raises NullReferenceException | `s = hstring{};`<br>`s.size() == 0` (valid) |
| If you assign null string to object | `o = (string)null;`<br>`o == null` | `o = box_value(hstring{});`<br>`o != nullptr` |
| If you assign `""` to object | `o = "";`<br>`o != null` | `o = box_value(hstring{L""});`<br>`o != nullptr` |

Basic boxing and unboxing.

| Operation | C# | C++/WinRT|
|-|-|-|
| Box a string | `o = s;`<br>Empty string becomes non-null object. | `o = box_value(s);`<br>Empty string becomes non-null object. |
| Unbox a known string | `s = (string)o;`<br>Null object becomes null string.<br>InvalidCastException if not a string. | `s = unbox_value<hstring>(o);`<br>Null object crashes.<br>Crash if not a string. |
| Unbox a possible string | `s = o as string;`<br>Null object or non-string becomes null string.<br><br>OR<br><br>`s = o as string ?? fallback;`<br>Null or non-string becomes fallback.<br>Empty string preserved. | `s = unbox_value_or<hstring>(o, fallback);`<br>Null or non-string becomes fallback.<br>Empty string preserved. |

### Making a class available to the {Binding} markup extension

If you intend to use the {Binding} markup extension to data bind to your data type, then see [Binding object declared using {Binding}](../data-binding/data-binding-in-depth.md#binding-object-declared-using-binding).

### Consuming objects from XAML markup

In a C# project, you can consume private members and named elements from XAML markup. But in C++/WinRT, all entities consumed by using the XAML [**{x:Bind} markup extension**](../xaml-platform/x-bind-markup-extension.md) must be exposed publicly in IDL.

Also, binding to a Boolean displays `true` or `false` in C#, but it shows **Windows.Foundation.IReference`1\<Boolean\>** in C++/WinRT.

For more info, and code examples, see [Consuming objects from markup](./binding-property.md#consuming-objects-from-xaml-markup).

### Making a data source available to XAML markup

In C++/WinRT version 2.0.190530.8 or later, [**winrt::single_threaded_observable_vector**](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector) creates an observable vector that supports both **[IObservableVector](/uwp/api/windows.foundation.collections.iobservablevector_t_)\<T\>** and **IObservableVector\<IInspectable\>**. For an example, see [Porting the **Scenarios** property](./clipboard-to-winrt-from-csharp.md#scenarios).

You can author your **Midl file (.idl)** like this (also see [Factoring runtime classes into Midl files (.idl)](./author-apis.md#factoring-runtime-classes-into-midl-files-idl)).

```idl
namespace Bookstore
{
    runtimeclass BookSku { ... }

    runtimeclass BookstoreViewModel
    {
        Windows.Foundation.Collections.IObservableVector<BookSku> BookSkus{ get; };
    }

    runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
    {
        MainPage();
        BookstoreViewModel MainViewModel{ get; };
    }
}
```

And implement like this.

```cppwinrt
// BookstoreViewModel.h
...
struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel>
{
    BookstoreViewModel()
    {
        m_bookSkus = winrt::single_threaded_observable_vector<Bookstore::BookSku>();
        m_bookSkus.Append(winrt::make<Bookstore::implementation::BookSku>(L"To Kill A Mockingbird"));
    }
    
	Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> BookSkus();
    {
        return m_bookSkus;
    }

private:
    Windows::Foundation::Collections::IObservableVector<Bookstore::BookSku> m_bookSkus;
};
...
```

For more info, see [XAML items controls; bind to a C++/WinRT collection](./binding-collection.md), and [Collections with C++/WinRT](./collections.md).

### Making a data source available to XAML markup (prior to C++/WinRT 2.0.190530.8)

XAML data binding requires that an items source implements **[IIterable](/uwp/api/windows.foundation.collections.iiterable_t_)\<IInspectable\>**, as well as one of the following combinations of interfaces.

- **IObservableVector\<IInspectable\>**
- **IBindableVector** and **INotifyCollectionChanged**
- **IBindableVector** and **IBindableObservableVector**
- **IBindableVector** by itself (will not respond to changes)
- **IVector\<IInspectable\>**
- **IBindableIterable** (will iterate and save elements into a private collection)

A generic interface such as **IVector\<T\>** can't be detected at runtime. Each **IVector\<T\>** has a different interface identifier (IID), which is a function of **T**. Any developer can expand the set of **T** arbitrarily, so clearly the XAML binding code can never know the full set to query for. That restriction isn't a problem for C# because every CLR object that implements **IEnumerable\<T\>** automatically implements **IEnumerable**. At the ABI level, that means that every object that implements **IObservableVector\<T\>** automatically implements **IObservableVector\<IInspectable\>**.

C++/WinRT doesn't offer that guarantee. If a C++/WinRT runtime class implements **IObservableVector\<T\>**, then we can't assume that an implementation of **IObservableVector\<IInspectable\>** is somehow also provided.

Consequently, here's how the previous example will need to look.

```idl
...
runtimeclass BookstoreViewModel
{
    // This is really an observable vector of BookSku.
    Windows.Foundation.Collections.IObservableVector<Object> BookSkus{ get; };
}
```

And the implementation.

```cppwinrt
// BookstoreViewModel.h
...
struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel>
{
    BookstoreViewModel()
    {
        m_bookSkus = winrt::single_threaded_observable_vector<Windows::Foundation::IInspectable>();
        m_bookSkus.Append(winrt::make<Bookstore::implementation::BookSku>(L"To Kill A Mockingbird"));
    }
    
    // This is really an observable vector of BookSku.
	Windows::Foundation::Collections::IObservableVector<Windows::Foundation::IInspectable> BookSkus();
    {
        return m_bookSkus;
    }

private:
    Windows::Foundation::Collections::IObservableVector<Windows::Foundation::IInspectable> m_bookSkus;
};
...
```

If you need to access objects in *m_bookSkus*, then you'll need to QI them back to **Bookstore::BookSku**.

```cppwinrt
Widget MyPage::BookstoreViewModel(winrt::hstring title)
{
    for (auto&& obj : m_bookSkus)
    {
        auto bookSku = obj.as<Bookstore::BookSku>();
        if (bookSku.Title() == title) return bookSku;
    }
    return nullptr;
}
```

### Derived classes

In order to derive from a runtime class, the base class must be *composable*. C# doesn't require that you take any special steps to make your classes composable, but C++/WinRT does. You use the [unsealed keyword](/uwp/midl-3/intro#base-classes) to indicate that you want your class to be usable as a base class.

```idl
unsealed runtimeclass BasePage : Windows.UI.Xaml.Controls.Page
{
    ...
}
runtimeclass DerivedPage : BasePage
{
    ...
}
```

In the header file for your [implementation type](./author-apis.md), you must include the base class header file before you include the autogenerated header for the derived class. Otherwise you'll get errors such as "Illegal use of this type as an expression".

```cppwinrt
// DerivedPage.h
#include "BasePage.h"       // This comes first.
#include "DerivedPage.g.h"  // Otherwise this header file will produce an error.

namespace winrt::MyNamespace::implementation
{
    struct DerivedPage : DerivedPageT<DerivedPage>
    {
        ...
    }
}
```

## Important APIs
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C# tutorials](/visualstudio/get-started/csharp)
* [C++/WinRT](./index.md)
* [Data binding in depth](../data-binding/data-binding-in-depth.md)