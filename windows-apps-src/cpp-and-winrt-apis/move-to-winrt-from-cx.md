---
author: stevewhims
description: This topic shows how to port C++/CX code to its equivalent in C++/WinRT.
title: Move to C++/WinRT from C++/CX
ms.author: stwhi
ms.date: 05/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C++/CX
ms.localizationpriority: medium
---

# Move to [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) from C++/CX
This topic shows how to port [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) code to its equivalent in C++/WinRT.

## Parameter-passing
When writing C++/CX source code, you pass C++/CX types as function parameters as hat (\^) references.

```cpp
void LogPresenceRecord(__in PresenceRecord^ record);
```

In C++/WinRT, for synchronous functions, you should use `const&` parameters by default. That will avoid copies and interlocked overhead. But your coroutines should use pass-by-value to ensure that they capture by value and avoid lifetime issues (for more details, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md)).

```cppwinrt
void LogPresenceRecord(__in PresenceRecord const& record);
IASyncAction LogPresenceRecordAsync(__in PresenceRecord const record);
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
void Sample::LogPresenceRecord(__in PresenceRecord^ record)
{
    auto id = record->XboxUserId;
    auto state = record->UserState;
    auto size = record->PresenceDeviceRecords->Size;
}
```

The equivalent C++/WinRT source code calls a function with the same name as the property, but with no parameters.

```cppwinrt
void Sample::LogPresenceRecord(__in PresenceRecord const& record)
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
You work with a C++/CX object via a handle to it, commonly known as a hat (\^) reference. You create an instance on the heap via the `ref new` keyword.

```cpp
using namespace Windows::Storage::Streams;

class Sample
{
private:
    Buffer^ m_gamerPicBuffer = ref new Buffer(MAX_IMAGE_SIZE);
};
```

A C++/WinRT object is a value; so you can allocate it on the stack, or as a field of an object. You *never* use `ref new` (nor `new`) to allocate a C++/WinRT object.

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

## Event-handling with a lambda function
Here's a typical example of handling an event in C++/CX using a lambda function.

```cpp
myButton->Click += ref new RoutedEventHandler([&](Platform::Object^ sender, RoutedEventArgs^ args)
{
    // Handle the event.
});
```

This is the equivalent in C++/WinRT.

```cppwinrt
myButton().Click([&](IInspectable const& sender, RoutedEventArgs const& args)
{
    // Handle the event.
});
```

For more info, see [Handle events by using delegates in C++/WinRT](handle-events.md).

## Related topics
* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Concurrency and asynchronous operations with C++/WinRT](concurrency.md)
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Handle events by using delegates in C++/WinRT](handle-events.md)