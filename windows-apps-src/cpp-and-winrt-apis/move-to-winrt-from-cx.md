---
author: stevewhims
description: This topic shows how to port C++/CX code to its equivalent in C++/WinRT.
title: Move to C++/WinRT from C++/CX
ms.author: stwhi
ms.date: 05/30/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C++/CX
ms.localizationpriority: medium
---

# Move to [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) from C++/CX
This topic shows how to port [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) code to its equivalent in C++/WinRT.

> [!NOTE]
> Both [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and the Windows SDK declare types in the root namespace **Windows**. A Windows type projected into C++/WinRT has the same fully-qualified name as the Windows type, but it's placed in the C++ **winrt** namespace. These distinct namespaces let you port from C++/CX to C++/WinRT at your own pace.

The first step in porting to C++/WinRT is to manually add C++/WinRT support to your project (see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix)). To do that, edit your `.vcxproj` file, find `<PropertyGroup Label="Globals">` and, inside that property group, set the property `<CppWinRTEnabled>true</CppWinRTEnabled>`. One effect of that change is that support for C++/CX is turned off in the project. It's a good idea to leave support turned off so that you can find and port all of your dependencies on C++/CX, or you can turn support back on (in project properties, **C/C++** \> **General** \> **Consume Windows Runtime Extension** \> **Yes (/ZW)**), and port gradually.

Set project property **General** \> **Target Platform Version** to 10.0.17134.0 (Windows 10, version 1803) or greater.

In your precompiled header file (usually `pch.h`), include `winrt/base.h`.

```cppwinrt
#include <winrt/base.h>
```

If you include any C++/WinRT projected Windows API headers (for example, `winrt/Windows.Foundation.h`), then you don't need to explicitly include `winrt/base.h` like this because it will be included automatically for you.

If your project is also using [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) types, then see [Move to C++/WinRT from WRL](move-to-winrt-from-wrl.md).

## Parameter-passing
When writing C++/CX source code, you pass C++/CX types as function parameters as hat (\^) references.

```cpp
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

```cpp
IVectorView<User^>^ userList = User::Users;

if (userList != nullptr)
{
    for (UINT32 iUser = 0; iUser < userList->Size; ++iUser)
    ...
```

When converting to the equivalent C++/WinRT code, you basically remove the hats and change the arrow operator (-&gt;) to the dot operator (.), because C++/WinRT projected types are values, and not pointers.

```cppwinrt
IVectorView<User> userList = User::Users();

if (userList != nullptr)
{
    for (UINT32 iUser = 0; iUser < userList.Size(); ++iUser)
    ...
```

## Properties
The C++/CX language extensions include the concept of properties. When writing C++/CX source code, you can access a property as if it were a field. Standard C++ does not have the concept of a property so, in C++/WinRT, you call get and set functions.

In the examples that follow, **XboxUserId**, **UserState**, **PresenceDeviceRecords**, and **Size** are all properties.

### Retrieving a value from a property
Here's how you get a property value in C++/CX.

```cpp
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

```cpp
record->UserState = newValue;
```

To do the equivalent in C++/WinRT, you call a function with the same name as the property, and pass an argument.

```cppwinrt
record.UserState(newValue);
```

## Creating an instance of a class
You work with a C++/CX object via a handle to it, commonly known as a hat (\^) reference. You create a new object via the `ref new` keyword, which in turn calls [**RoActivateInstance**](https://msdn.microsoft.com/library/br224646) to activate a new instance of the runtime class.

```cpp
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

If a resource is expensive to initialize, then it's common to delay initialization of it until it's actually needed.

```cpp
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

The same code ported to C++/WinRT. Note the use of the `nullptr` constructor. For more info about that constructor, see [Consume APIs with C++/WinRT](consume-apis.md).

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

## Event-handling with a delegate
Here's a typical example of handling an event in C++/CX, using a lambda function as a delegate in this case.

```cpp
auto token = myButton->Click += ref new RoutedEventHandler([&](Platform::Object^ sender, RoutedEventArgs^ args)
{
    // Handle the event.
});
```

This is the equivalent in C++/WinRT.

```cppwinrt
auto token = myButton().Click([&](IInspectable const& sender, RoutedEventArgs const& args)
{
    // Handle the event.
});
```

Instead of a lambda function, you can choose to implement your delegate as a free function, or as a pointer-to-member-function. For more info, see [Handle events by using delegates in C++/WinRT](handle-events.md).

If you're porting from a C++/CX codebase where events and delegates are used internally (not across binaries), then [**winrt::delegate**](/uwp/cpp-ref-for-winrt/delegate) will help you to replicate that pattern in C++/WinRT. Also see [winrt::delegate&lt;... T&gt;](author-events.md#winrtdelegate-t).

## Revoking a delegate
In C++/CX you use the `-=` operator to revoke a prior event registration.

```cpp
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
| **Platform::Object\^** | **winrt::Windows::Foundation::IInspectable** |
| **Platform::String\^** | [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) |
| **Platform::Exception\^** | [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) |
| **Platform::InvalidArgumentException\^** | [**winrt::hresult_invalid_argument**](/uwp/cpp-ref-for-winrt/error-handling/hresult-invalid-argument) |

### Port **Platform::Object\^** to **winrt::Windows::Foundation::IInspectable**
Like all C++/WinRT types, **winrt::Windows::Foundation::IInspectable** is a value type. Here's how you initialize a variable of that type to null.

```cppwinrt
winrt::Windows::Foundation::IInspectable var{ nullptr };
```

### Port **Platform::String\^** to **winrt::hstring**
**Platform::String\^** is equivalent to the Windows Runtime HSTRING ABI type. For C++/WinRT, the equivalent is [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring). But with C++/WinRT, you can call Windows Runtime APIs using C++ Standard Library wide string types such as **std::wstring**, and/or wide string literals. For more details, and code examples, see [String handling in C++/WinRT](strings.md).

With C++/CX, you can access the [**Platform::String::Data**](https://docs.microsoft.com/en-us/cpp/cppcx/platform-string-class#data) property to retrieve the string as a C-style **const wchar_t\*** array (for example, to pass it to **std::wcout**).

```C++
auto var = titleRecord->TitleName->Data();
```

To do the same with C++/WinRT, you can use the [**hstring::c_str**](/uwp/api/windows.foundation.uri#hstringcstr-function) function to get a null-terminated C-style string version, just as you can from **std::wstring**.

```C++
auto var = titleRecord.TitleName().c_str();
```

When it comes to implementing APIs that take or return strings, you typically change any C++/CX code that uses **Platform::String\^** to use **winrt::hstring** instead.

Here's an example of a C++/CX API that takes a string.

```cpp
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

### Port **Platform::Exception\^** to **winrt::hresult_error**
The **Platform::Exception\^** type is produced in C++/CX when a Windows Runtime API returns a non S\_OK HRESULT. The C++/WinRT equivalent is [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error).

To port to C++/WinRT, change all code that uses **Platform::Exception\^** to use **winrt::hresult_error**.

In C++/CX.

```cpp
catch (Platform::Exception^ ex)
```

In C++/WinRT.

```cppwinrt
catch (winrt::hresult_error const& ex)
```

C++/WinRT provides these exception classes.

| Exception type | Base class | HRESULT |
| ---- | ---- | ---- |
| [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) | | call [**hresult_error::to_abi**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresulterrortoabi-function) |
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

Note that each class (via the **hresult_error** base class) provides a [**to_abi**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresulterrortoabi-function) function, which returns the HRESULT of the error, and a [**message**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresulterrormessage-function) function, which returns the string representation of that HRESULT.

Here's an example of throwing an exception in C++/CX.

```cpp
throw ref new Platform::InvalidArgumentException(L"A valid User is required");
```

And the equivalent in C++/WinRT.

```cppwinrt
throw winrt::hresult_invalid_argument{ L"A valid User is required" };
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
* [Microsoft Interface Definition Language 3.0 reference](/uwp/midl-3)
* [Move to C++/WinRT from WRL](move-to-winrt-from-wrl.md)
* [String handling in C++/WinRT](strings.md)
