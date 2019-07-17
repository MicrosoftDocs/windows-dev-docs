---
description: This topic shows how to port C++/CX code to its equivalent in C++/WinRT.
title: Move to C++/WinRT from C++/CX
ms.date: 01/17/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C++/CX
ms.localizationpriority: medium
---

# Move to C++/WinRT from C++/CX

This topic shows how to port the code in a [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) project to its equivalent in [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt).

## Porting strategies

If you want to gradually port your C++/CX code to C++/WinRT, then you can. C++/CX and C++/WinRT code can coexist in the same project, with the exceptions of XAML compiler support and Windows Runtime Components. For those two exceptions, you'll need to target either C++/CX or C++/WinRT within the same project.

> [!IMPORTANT]
> If your project builds a XAML application, then one workflow that we recommend is to first create a new project in Visual Studio using one of the C++/WinRT project templates (see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)). Then, start copying source code and markup over from the C++/CX project. You can add new XAML pages with **Project** \> **Add New Item...** \> **Visual C++** > **Blank Page (C++/WinRT)**.
>
> Alternatively, you can use a Windows Runtime Component to factor code out of the XAML C++/CX project as you port it. Either move as much C++/CX code as you can into a component, and then change the XAML project to C++/WinRT. Or else leave the XAML project as C++/CX, create a new C++/WinRT component, and begin porting C++/CX code out of the XAML project and into the component. You could also have a C++/CX component project alongside a C++/WinRT component project within the same solution, reference both of them from your application project, and gradually port from one to the other. See [Interop between C++/WinRT and C++/CX](interop-winrt-cx.md) for more details on using the two language projections in the same project.

> [!NOTE]
> Both [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and the Windows SDK declare types in the root namespace **Windows**. A Windows type projected into C++/WinRT has the same fully-qualified name as the Windows type, but it's placed in the C++ **winrt** namespace. These distinct namespaces let you port from C++/CX to C++/WinRT at your own pace.

Bearing in mind the exceptions mentioned above, the first step in porting a C++/CX project to C++/WinRT is to manually add C++/WinRT support to it (see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)). To do that, install the [Microsoft.Windows.CppWinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) into your project. Open the project in Visual Studio, click **Project** \> **Manage NuGet Packages...** \> **Browse**, type or paste **Microsoft.Windows.CppWinRT** in the search box, select the item in search results, and then click **Install** to install the package for that project. One effect of that change is that support for C++/CX is turned off in the project. It's a good idea to leave support turned off so that build messages help you find (and port) all of your dependencies on C++/CX, or you can turn support back on (in project properties, **C/C++** \> **General** \> **Consume Windows Runtime Extension** \> **Yes (/ZW)**), and port gradually.

Ensure that project property **General** \> **Target Platform Version** is set to 10.0.17134.0 (Windows 10, version 1803) or greater.

In your precompiled header file (usually `pch.h`), include `winrt/base.h`.

```cppwinrt
#include <winrt/base.h>
```

If you include any C++/WinRT projected Windows API headers (for example, `winrt/Windows.Foundation.h`), then you don't need to explicitly include `winrt/base.h` like this because it will be included automatically for you.

If your project is also using [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) types, then see [Move to C++/WinRT from WRL](move-to-winrt-from-wrl.md).

## File-naming conventions

### XAML markup files

| | C++/CX | C++/WinRT |
| - | - | - |
| **Developer XAML files** | MyPage.xaml<br/>MyPage.xaml.h<br/>MyPage.xaml.cpp | MyPage.xaml<br/>MyPage.h<br/>MyPage.cpp<br/>MyPage.idl (see below) |
| **Generated XAML files** | MyPage.xaml.g.h<br/>MyPage.xaml.g.hpp | MyPage.xaml.g.h<br/>MyPage.xaml.g.hpp<br/>MyPage.g.h |

Notice that C++/WinRT removes the `.xaml` from the `*.h` and `*.cpp` file names.

C++/WinRT adds an additional developer file, the **Midl file (.idl)**. C++/CX autogenerates this file internally, adding to it every public and protected member. In C++/WinRT, you add and author the file yourself. For more details, code examples, and a walkthrough of authoring IDL, see [XAML controls; bind to a C++/WinRT property](/windows/uwp/cpp-and-winrt-apis/binding-property).

Also see [Factoring runtime classes into Midl files (.idl)](/windows/uwp/cpp-and-winrt-apis/author-apis#factoring-runtime-classes-into-midl-files-idl)

### Runtime classes

C++/CX doesn't impose restrictions on the names of your header files; it's common to put multiple runtime class definitions into a single header file, especially for small classes. But C++/WinRT requires that each runtime class has its own header file named after the class name. 

| C++/CX | C++/WinRT |
| - | - |
| **Common.h**<br>`ref class A { ... }`<br>`ref class B { ... }` | **Common.idl**<br>`runtimeclass A { ... }`<br>`runtimeclass B { ... }` |
|  | **A.h**<br>`namespace implements {`<br>&nbsp;&nbsp;`struct A { ... };`<br>`}` |
|  | **B.h**<br>`namespace implements {`<br>&nbsp;&nbsp;`struct B { ... };`<br>`}` |

Less common (but still legal) in C++/CX is to use differently-named header files for XAML custom controls. You'll need to rename these header file to match the class name.

| C++/CX | C++/WinRT |
| - | - |
| **A.xaml**<br>`<Page x:Class="LongNameForA" ...>` | **A.xaml**<br>`<Page x:Class="LongNameForA" ...>` |
| **A.h**<br>`partial ref class LongNameForA { ... }` | **LongNameForA.h**<br>`namespace implements {`<br>&nbsp;&nbsp;`struct LongNameForA { ... };`<br>`}` |

## Header file requirements

C++/CX doesn't require you to include any special header files, because it internally autogenerates header files from `.winmd` files. It's common in C++/CX to use `using` directives for namespaces that you consume by name.

```cppcx
using namespace Windows::Media::Playback;

String^ NameOfFirstVideoTrack(MediaPlaybackItem^ item)
{
    return item->VideoTracks->GetAt(0)->Name;
}
```

The `using namespace Windows::Media::Playback` directive lets us write `MediaPlaybackItem` without a namespace prefix. We also touched the `Windows.Media.Core` namespace, because `item->VideoTracks->GetAt(0)` returns a **Windows.Media.Core.VideoTrack**. But we didn't have to type the name **VideoTrack** anywhere, so we didn't need a `using Windows.Media.Core` directive.

But C++/WinRT requires you to include a header file corresponding to each namespace that you consume, even if you don't name it.

```cppwinrt
#include <winrt/Windows.Media.Playback.h>
#include <winrt/Windows.Media.Core.h> // !!This is important!!

using namespace winrt;
using namespace Windows::Media::Playback;

winrt::hstring NameOfFirstVideoTrack(MediaPlaybackItem const& item)
{
    return item.VideoTracks().GetAt(0).Name();
}
```

On the other hand, even though the **MediaPlaybackItem.AudioTracksChanged** event is of type **TypedEventHandler\<MediaPlaybackItem, Windows.Foundation.Collections.IVectorChangedEventArgs\>**, we don't need to include `winrt/Windows.Foundation.Collections.h` because we didn't use that event.

C++/WinRT also requires you to include header files for namespaces that are consumed by XAML markup.

```xaml
<!-- MainPage.xaml -->
<Rectangle Height="400"/>
```

Using the **Rectangle** class means that you have to add this include.

```cppwinrt
// MainPage.h
#include <winrt/Windows.UI.Xaml.Shapes.h>
```

If you forget a header file, then everything will compile okay but you'll get linker errors because the `consume_` classes are missing.

## Parameter-passing

When writing C++/CX source code, you pass C++/CX types as function parameters as hat (\^) references.

```cppcx
void LogPresenceRecord(PresenceRecord^ record);
```

In C++/WinRT, for synchronous functions, you should use `const&` parameters by default. That will avoid copies and interlocked overhead. But your coroutines should use pass-by-value to ensure that they capture by value and avoid lifetime issues (for more details, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md)).

```cppwinrt
void LogPresenceRecord(PresenceRecord const& record);
IASyncAction LogPresenceRecordAsync(PresenceRecord const record);
```

A C++/WinRT object is fundamentally a value that holds an interface pointer to the backing Windows Runtime object. When you copy a C++/WinRT object, the compiler copies the encapsulated interface pointer, incrementing its reference count. Eventual destruction of the copy involves decrementing the reference count. So, only incur the overhead of a copy when necessary.

## Variable and field references

When writing C++/CX source code, you use hat (\^) variables to reference Windows Runtime objects, and the arrow (-&gt;) operator to dereference a hat variable.

```cppcx
IVectorView<User^>^ userList = User::Users;

if (userList != nullptr)
{
    for (UINT32 iUser = 0; iUser < userList->Size; ++iUser)
    ...
```

When porting to the equivalent C++/WinRT code, you can get a long way by removing the hats, and changing the arrow operator (-&gt;) to the dot operator (.). C++/WinRT projected types are values, and not pointers.

```cppwinrt
IVectorView<User> userList = User::Users();

if (userList != nullptr)
{
    for (UINT32 iUser = 0; iUser < userList.Size(); ++iUser)
    ...
```

The default constructor for a C++/CX hat reference initializes it to null. Here's a C++/CX code example in which we create a variable/field of the correct type, but one that's uninitialized. In other words, it doesn't initially refer to a **TextBlock**; we intend to assign a reference later.

```cppcx
TextBlock^ textBlock;

class MyClass
{
    TextBlock^ textBlock;
};
```

For the equivalent in C++/WinRT, see [Delayed initialization](consume-apis.md#delayed-initialization).

## Properties

The C++/CX language extensions include the concept of properties. When writing C++/CX source code, you can access a property as if it were a field. Standard C++ does not have the concept of a property so, in C++/WinRT, you call get and set functions.

In the examples that follow, **XboxUserId**, **UserState**, **PresenceDeviceRecords**, and **Size** are all properties.

### Retrieving a value from a property

Here's how you get a property value in C++/CX.

```cppcx
void Sample::LogPresenceRecord(PresenceRecord^ record)
{
    auto id = record->XboxUserId;
    auto state = record->UserState;
    auto size = record->PresenceDeviceRecords->Size;
}
```

The equivalent C++/WinRT source code calls a function with the same name as the property, but with no parameters.

```cppwinrt
void Sample::LogPresenceRecord(PresenceRecord const& record)
{
    auto id = record.XboxUserId();
    auto state = record.UserState();
    auto size = record.PresenceDeviceRecords().Size();
}
```

Note that the **PresenceDeviceRecords** function returns a Windows Runtime object that itself has a **Size** function. As the returned object is also a C++/WinRT projected type, we dereference using the dot operator to call **Size**.

### Setting a property to a new value

Setting a property to a new value follows a similar pattern. First, in C++/CX.

```cppcx
record->UserState = newValue;
```

To do the equivalent in C++/WinRT, you call a function with the same name as the property, and pass an argument.

```cppwinrt
record.UserState(newValue);
```

## Creating an instance of a class

You work with a C++/CX object via a handle to it, commonly known as a hat (\^) reference. You create a new object via the `ref new` keyword, which in turn calls [**RoActivateInstance**](https://docs.microsoft.com/windows/desktop/api/roapi/nf-roapi-roactivateinstance) to activate a new instance of the runtime class.

```cppcx
using namespace Windows::Storage::Streams;

class Sample
{
private:
    Buffer^ m_gamerPicBuffer = ref new Buffer(MAX_IMAGE_SIZE);
};
```

A C++/WinRT object is a value; so you can allocate it on the stack, or as a field of an object. You *never* use `ref new` (nor `new`) to allocate a C++/WinRT object. Behind the scenes, **RoActivateInstance** is still being called.

```cppwinrt
using namespace winrt::Windows::Storage::Streams;

struct Sample
{
private:
    Buffer m_gamerPicBuffer{ MAX_IMAGE_SIZE };
};
```

If a resource is expensive to initialize, then it's common to delay initialization of it until it's actually needed. As already mentioned, the default constructor for a C++/CX hat reference initializes it to null.

```cppcx
using namespace Windows::Storage::Streams;

class Sample
{
public:
    void DelayedInit()
    {
        // Allocate the actual buffer.
        m_gamerPicBuffer = ref new Buffer(MAX_IMAGE_SIZE);
    }

private:
    Buffer^ m_gamerPicBuffer;
};
```

The same code ported to C++/WinRT. Note the use of the **std::nullptr_t** constructor. For more info about that constructor, see [Delayed initialization](consume-apis.md#delayed-initialization).

```cppwinrt
using namespace winrt::Windows::Storage::Streams;

struct Sample
{
    void DelayedInit()
    {
        // Allocate the actual buffer.
        m_gamerPicBuffer = Buffer(MAX_IMAGE_SIZE);
    }

private:
    Buffer m_gamerPicBuffer{ nullptr };
};
```


## Converting from a base runtime class to a derived one

It's common to have a reference-to-base that you know refers to an object of a derived type. In C++/CX, you use `dynamic_cast` to *cast* the reference-to-base into a reference-to-derived. The `dynamic_cast` is really just a hidden call to [**QueryInterface**](https://docs.microsoft.com/windows/desktop/api/unknwn/nf-unknwn-iunknown-queryinterface(q_)). Here's a typical example&mdash;you're handling a dependency property changed event, and you want to cast from **DependencyObject** back to the actual type that owns the dependency property.

```cppcx
void BgLabelControl::OnLabelChanged(Windows::UI::Xaml::DependencyObject^ d, Windows::UI::Xaml::DependencyPropertyChangedEventArgs^ e)
{
    BgLabelControl^ theControl{ dynamic_cast<BgLabelControl^>(d) };

    if (theControl != nullptr)
    {
        // succeeded ...
    }
}
```

The equivalent C++/WinRT code replaces the `dynamic_cast` with a call to the [**IUnknown::try_as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntry_as-function) function, which encapsulates **QueryInterface**. You also have the option to call [**IUnknown::as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function), instead, which throws an exception if querying for the required interface (the default interface of the type you're requesting) is not returned. Here's a C++/WinRT code example.

```cppwinrt
void BgLabelControl::OnLabelChanged(Windows::UI::Xaml::DependencyObject const& d, Windows::UI::Xaml::DependencyPropertyChangedEventArgs const& e)
{
    if (BgLabelControlApp::BgLabelControl theControl{ d.try_as<BgLabelControlApp::BgLabelControl>() })
    {
        // succeeded ...
    }

    try
    {
        BgLabelControlApp::BgLabelControl theControl{ d.as<BgLabelControlApp::BgLabelControl>() };
        // succeeded ...
    }
    catch (winrt::hresult_no_interface const&)
    {
        // failed ...
    }
}
```

## Event-handling with a delegate

Here's a typical example of handling an event in C++/CX, using a lambda function as a delegate in this case.

```cppcx
auto token = myButton->Click += ref new RoutedEventHandler([=](Platform::Object^ sender, RoutedEventArgs^ args)
{
    // Handle the event.
    // Note: locals are captured by value, not reference, since this handler is delayed.
});
```

This is the equivalent in C++/WinRT.

```cppwinrt
auto token = myButton().Click([=](IInspectable const& sender, RoutedEventArgs const& args)
{
    // Handle the event.
    // Note: locals are captured by value, not reference, since this handler is delayed.
});
```

Instead of a lambda function, you can choose to implement your delegate as a free function, or as a pointer-to-member-function. For more info, see [Handle events by using delegates in C++/WinRT](handle-events.md).

If you're porting from a C++/CX codebase where events and delegates are used internally (not across binaries), then [**winrt::delegate**](/uwp/cpp-ref-for-winrt/delegate) will help you to replicate that pattern in C++/WinRT. Also see [Parameterized delegates, simple signals, and callbacks within a project](author-events.md#parameterized-delegates-simple-signals-and-callbacks-within-a-project).

## Revoking a delegate

In C++/CX you use the `-=` operator to revoke a prior event registration.

```cppcx
myButton->Click -= token;
```

This is the equivalent in C++/WinRT.

```cppwinrt
myButton().Click(token);
```

For more info and options, see [Revoke a registered delegate](handle-events.md#revoke-a-registered-delegate).

## Mapping C++/CX **Platform** types to C++/WinRT types

C++/CX provides several data types in the **Platform** namespace. These types are not standard C++, so you can only use them when you enable Windows Runtime language extensions (Visual Studio project property **C/C++** > **General** > **Consume Windows Runtime Extension** > **Yes (/ZW)**). The table below helps you port from **Platform** types to their equivalents in C++/WinRT. Once you've done that, since C++/WinRT is standard C++, you can turn off the `/ZW` option.

| C++/CX | C++/WinRT |
| ---- | ---- |
| **Platform::Agile\^** | [**winrt::agile_ref**](/uwp/cpp-ref-for-winrt/agile-ref) |
| **Platform::Array\^** | See [Port **Platform::Array\^**](#port-platformarray) |
| **Platform::Exception\^** | [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) |
| **Platform::InvalidArgumentException\^** | [**winrt::hresult_invalid_argument**](/uwp/cpp-ref-for-winrt/error-handling/hresult-invalid-argument) |
| **Platform::Object\^** | **winrt::Windows::Foundation::IInspectable** |
| **Platform::String\^** | [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) |

### Port **Platform::Agile\^** to **winrt::agile_ref**

The **Platform::Agile\^** type in C++/CX represents a Windows Runtime class that can be accessed from any thread. The C++/WinRT equivalent is [**winrt::agile_ref**](/uwp/cpp-ref-for-winrt/agile-ref).

In C++/CX.

```cppcx
Platform::Agile<Windows::UI::Core::CoreWindow> m_window;
```

In C++/WinRT.

```cppwinrt
winrt::agile_ref<Windows::UI::Core::CoreWindow> m_window;
```

### Port **Platform::Array\^**

Your options include using an initializer list, a **std::array**, or a **std::vector**. For more info, and code examples, see [Standard initializer lists](/windows/uwp/cpp-and-winrt-apis/std-cpp-data-types#standard-initializer-lists) and [Standard arrays and vectors](/windows/uwp/cpp-and-winrt-apis/std-cpp-data-types#standard-arrays-and-vectors).

### Port **Platform::Exception\^** to **winrt::hresult_error**

The **Platform::Exception\^** type is produced in C++/CX when a Windows Runtime API returns a non S\_OK HRESULT. The C++/WinRT equivalent is [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error).

To port to C++/WinRT, change all code that uses **Platform::Exception\^** to use **winrt::hresult_error**.

In C++/CX.

```cppcx
catch (Platform::Exception^ ex)
```

In C++/WinRT.

```cppwinrt
catch (winrt::hresult_error const& ex)
```

C++/WinRT provides these exception classes.

| Exception type | Base class | HRESULT |
| ---- | ---- | ---- |
| [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) | | call [**hresult_error::to_abi**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errorto_abi-function) |
| [**winrt::hresult_access_denied**](/uwp/cpp-ref-for-winrt/error-handling/hresult-access-denied) | **winrt::hresult_error** | E_ACCESSDENIED |
| [**winrt::hresult_canceled**](/uwp/cpp-ref-for-winrt/error-handling/hresult-canceled) | **winrt::hresult_error** | ERROR_CANCELLED |
| [**winrt::hresult_changed_state**](/uwp/cpp-ref-for-winrt/error-handling/hresult-changed-state) | **winrt::hresult_error** | E_CHANGED_STATE |
| [**winrt::hresult_class_not_available**](/uwp/cpp-ref-for-winrt/error-handling/hresult-class-not-available) | **winrt::hresult_error** | CLASS_E_CLASSNOTAVAILABLE |
| [**winrt::hresult_illegal_delegate_assignment**](/uwp/cpp-ref-for-winrt/error-handling/hresult-illegal-delegate-assignment) | **winrt::hresult_error** | E_ILLEGAL_DELEGATE_ASSIGNMENT |
| [**winrt::hresult_illegal_method_call**](/uwp/cpp-ref-for-winrt/error-handling/hresult-illegal-method-call) | **winrt::hresult_error** | E_ILLEGAL_METHOD_CALL |
| [**winrt::hresult_illegal_state_change**](/uwp/cpp-ref-for-winrt/error-handling/hresult-illegal-state-change) | **winrt::hresult_error** | E_ILLEGAL_STATE_CHANGE |
| [**winrt::hresult_invalid_argument**](/uwp/cpp-ref-for-winrt/error-handling/hresult-invalid-argument) | **winrt::hresult_error** | E_INVALIDARG |
| [**winrt::hresult_no_interface**](/uwp/cpp-ref-for-winrt/error-handling/hresult-no-interface) | **winrt::hresult_error** | E_NOINTERFACE |
| [**winrt::hresult_not_implemented**](/uwp/cpp-ref-for-winrt/error-handling/hresult-not-implemented) | **winrt::hresult_error** | E_NOTIMPL |
| [**winrt::hresult_out_of_bounds**](/uwp/cpp-ref-for-winrt/error-handling/hresult-out-of-bounds) | **winrt::hresult_error** | E_BOUNDS |
| [**winrt::hresult_wrong_thread**](/uwp/cpp-ref-for-winrt/error-handling/hresult-wrong-thread) | **winrt::hresult_error** | RPC_E_WRONG_THREAD |

Note that each class (via the **hresult_error** base class) provides a [**to_abi**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errorto_abi-function) function, which returns the HRESULT of the error, and a [**message**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errormessage-function) function, which returns the string representation of that HRESULT.

Here's an example of throwing an exception in C++/CX.

```cppcx
throw ref new Platform::InvalidArgumentException(L"A valid User is required");
```

And the equivalent in C++/WinRT.

```cppwinrt
throw winrt::hresult_invalid_argument{ L"A valid User is required" };
```

### Port **Platform::Object\^** to **winrt::Windows::Foundation::IInspectable**

Like all C++/WinRT types, **winrt::Windows::Foundation::IInspectable** is a value type. Here's how you initialize a variable of that type to null.

```cppwinrt
winrt::Windows::Foundation::IInspectable var{ nullptr };
```

### Port **Platform::String\^** to **winrt::hstring**

**Platform::String\^** is equivalent to the Windows Runtime HSTRING ABI type. For C++/WinRT, the equivalent is [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring). But with C++/WinRT, you can call Windows Runtime APIs using C++ Standard Library wide string types such as **std::wstring**, and/or wide string literals. For more details, and code examples, see [String handling in C++/WinRT](strings.md).

With C++/CX, you can access the [**Platform::String::Data**](https://docs.microsoft.com/cpp/cppcx/platform-string-class?view=vs-2019#data) property to retrieve the string as a C-style **const wchar_t\*** array (for example, to pass it to **std::wcout**).

```cppcx
auto var{ titleRecord->TitleName->Data() };
```

To do the same with C++/WinRT, you can use the [**hstring::c_str**](/uwp/api/windows.foundation.uri.-ctor#Windows_Foundation_Uri__ctor_System_String_) function to get a null-terminated C-style string version, just as you can from **std::wstring**.

```cppwinrt
auto var{ titleRecord.TitleName().c_str() };
```

When it comes to implementing APIs that take or return strings, you typically change any C++/CX code that uses **Platform::String\^** to use **winrt::hstring** instead.

Here's an example of a C++/CX API that takes a string.

```cppcx
void LogWrapLine(Platform::String^ str);
```

For C++/WinRT you could declare that API in [MIDL 3.0](/uwp/midl-3) like this.

```idl
// LogType.idl
void LogWrapLine(String str);
```

The C++/WinRT toolchain will then generate source code for you that looks like this.

```cppwinrt
void LogWrapLine(winrt::hstring const& str);
```

#### ToString()

C++/CX provides the [Object::ToString](/cpp/cppcx/platform-object-class?view=vs-2017#tostring) method.

```cppcx
int i{ 2 };
auto s{ i.ToString() }; // s is a Platform::String^ with value L"2".
```

C++/WinRT doesn't directly provide this facility, but you can turn to alternatives.

```cppwinrt
int i{ 2 };
auto s{ std::to_wstring(i) }; // s is a std::wstring with value L"2".
```

## Important APIs
* [winrt::delegate struct template](/uwp/cpp-ref-for-winrt/delegate)
* [winrt::hresult_error struct](/uwp/cpp-ref-for-winrt/error-handling/hresult-error)
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Author events in C++/WinRT](author-events.md)
* [Concurrency and asynchronous operations with C++/WinRT](concurrency.md)
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Handle events by using delegates in C++/WinRT](handle-events.md)
* [Interop between C++/WinRT and C++/CX](interop-winrt-cx.md)
* [Microsoft Interface Definition Language 3.0 reference](/uwp/midl-3)
* [Move to C++/WinRT from WRL](move-to-winrt-from-wrl.md)
* [String handling in C++/WinRT](strings.md)
