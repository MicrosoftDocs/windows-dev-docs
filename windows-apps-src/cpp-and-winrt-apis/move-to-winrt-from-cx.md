---
description: This topic describes the technical details involved in porting the source code in a [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) project to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md).
title: Move to C++/WinRT from C++/CX
ms.date: 01/17/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C++/CX
ms.localizationpriority: medium
---

# Move to C++/WinRT from C++/CX

This topic is the first in a series describing how you can port the source code in your [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) project to its equivalent in [C++/WinRT](./intro-to-using-cpp-with-winrt.md).

If your project is also using [Windows Runtime C++ Template Library (WRL)](/cpp/windows/windows-runtime-cpp-template-library-wrl) types, then see [Move to C++/WinRT from WRL](move-to-winrt-from-wrl.md).

## Strategies for porting

It's worth knowing that porting from C++/CX to C++/WinRT is generally straightforward, with the one exception of moving from [Parallel Patterns Library (PPL)](/cpp/parallel/concrt/parallel-patterns-library-ppl) tasks to coroutines. The models are different. There isn't a natural one-to-one mapping from PPL tasks to coroutines, and there's no simple way to mechanically port the code that works for all cases. For help with this specific aspect of porting, and your options for interoperating between the two models, see [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md).

Development teams routinely report that once they're over the hurdle of porting their asynchronous code, the remainder of the porting work is largely mechanical.

### Porting in one pass

If you're in a position to be able to port your entire project in one pass, then you'll need only this topic for the info you need (and you won't need the *interop* topics that follow this one). We recommend that you begin by creating a new project in Visual Studio using one of the C++/WinRT project templates (see [Visual Studio support for C++/WinRT](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)). Then move your source code files over into that new project, and port all of the C++/CX source code to C++/WinRT as you do so.

Alternatively, if you'd prefer to do the porting work in your existing C++/CX project, then you'll need to add C++/WinRT support to it. The steps that you follow to do that are described in [Taking a C++/CX project and adding C++/WinRT support](./interop-winrt-cx.md#taking-a-ccx-project-and-adding-cwinrt-support). By the time you're done porting, you'll have turned what was a pure C++/CX project into a pure C++/WinRT project.

> [!NOTE]
> If you have a Windows Runtime component project, then porting in one pass is your only option. A Windows Runtime component project written in C++ must contain either all C++/CX source code, or all C++/WinRT source code. They can't coexist in this project type.

### Porting a project gradually

With the exception of Windows Runtime component projects, as mentioned in the previous section, if the size or complexity of your codebase makes it necessary to port your project gradually, then you'll need a porting process in which for a time C++/CX and C++/WinRT code exists side by side in the same project. In addition to reading this topic, also see [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md) and [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md). Those topics provide info and code examples showing how to interoperate between the two language projections.

To get a project ready for a gradual porting process, one option is to add C++/WinRT support to your C++/CX project. The steps that you follow to do that are described in [Taking a C++/CX project and adding C++/WinRT support](./interop-winrt-cx.md#taking-a-ccx-project-and-adding-cwinrt-support). You can then port gradually from there.

Another option is to create a new project in Visual Studio using one of the C++/WinRT project templates (see [Visual Studio support for C++/WinRT](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)). And then add C++/CX support to that project. The steps that you follow to do that are described in [Taking a C++/WinRT project and adding C++/CX support](./interop-winrt-cx.md#taking-a-cwinrt-project-and-adding-cx-support). You can then start moving your source code over into that, and porting *some* of the C++/CX source code to C++/WinRT as you do so.

In either case, you'll interoperate (both ways) between your C++/WinRT code and any C++/CX code that you haven't yet ported.

> [!NOTE]
> Both [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) and the Windows SDK declare types in the root namespace **Windows**. A Windows type projected into C++/WinRT has the same fully-qualified name as the Windows type, but it's placed in the C++ **winrt** namespace. These distinct namespaces let you port from C++/CX to C++/WinRT at your own pace.

#### Porting a XAML project gradually

> [!IMPORTANT]
> For a project that uses XAML, at any given time all of your XAML page types need to be either entirely C++/CX or entirely C++/WinRT. You can still mix C++/CX and C++/WinRT *outside* of XAML page types within the same project (in your models and viewmodels, and elsewhere).

For this scenario, the workflow that we recommend is to create a new C++/WinRT project and copy source code and markup over from the C++/CX project. As long as all of your XAML page types are C++/WinRT, then you can add new XAML pages with **Project** \> **Add New Item...** \> **Visual C++** > **Blank Page (C++/WinRT)**.

Alternatively, you can use a Windows Runtime component (WRC) to factor code out of the XAML C++/CX project as you port it.

- You could create a new C++/CX WRC project, move as much C++/CX code as you can into that project, and then change the XAML project to C++/WinRT.
- Or you could create a new C++/WinRT WRC project, leave the XAML project as C++/CX, and begin porting C++/CX to C++/WinRT and moving the resulting code out of the XAML project and into the component project.
- You could also have a C++/CX component project alongside a C++/WinRT component project within the same solution, reference both of them from your application project, and gradually port from one to the other. Again, see [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md) for more details on using the two language projections in the same project.

## First steps in porting a C++/CX project to C++/WinRT

No matter what your porting strategy will be (porting in one pass, or porting gradually), your first step is to prepare your project for porting. Here's a recap of what we described in [Strategies for porting](#strategies-for-porting) in terms of the kind of project you'll be starting with, and how to set it up.

- **Porting in one pass**. Create a new project in Visual Studio using one of the C++/WinRT project templates. Move the files from your C++/CX project into that new project, and port the C++/CX source code.
- **Porting a non-XAML project gradually**. You can choose to add C++/WinRT support to your C++/CX project (see [Taking a C++/CX project and adding C++/WinRT support](./interop-winrt-cx.md#taking-a-ccx-project-and-adding-cwinrt-support)), and port gradually. Or you can choose to create a new C++/WinRT project and add C++/CX support to that (see [Taking a C++/WinRT project and adding C++/CX support](./interop-winrt-cx.md#taking-a-cwinrt-project-and-adding-cx-support)), move files over, and port gradually.
- **Porting a XAML project gradually**. Create a new C++/WinRT project, move files over, and port gradually. At any given time your XAML page types must be *either* all C++/WinRT *or* all C++/CX.

The rest of this topic applies no matter which porting strategy you choose. It contains a catalog of technical details involved in porting source code from C++/CX to C++/WinRT. If you're porting gradually, then you'll likely also want to see [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md) and [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md).

## File-naming conventions

### XAML markup files

| File origin | C++/CX | C++/WinRT |
| - | - | - |
| **Developer XAML files** | MyPage.xaml<br/>MyPage.xaml.h<br/>MyPage.xaml.cpp | MyPage.xaml<br/>MyPage.h<br/>MyPage.cpp<br/>MyPage.idl (see below) |
| **Generated XAML files** | MyPage.xaml.g.h<br/>MyPage.xaml.g.hpp | MyPage.xaml.g.h<br/>MyPage.xaml.g.hpp<br/>MyPage.g.h |

Notice that C++/WinRT removes the `.xaml` from the `*.h` and `*.cpp` file names.

C++/WinRT adds an additional developer file, the **Midl file (.idl)**. C++/CX autogenerates this file internally, adding to it every public and protected member. In C++/WinRT, you add and author the file yourself. For more details, code examples, and a walkthrough of authoring IDL, see [XAML controls; bind to a C++/WinRT property](./binding-property.md).

Also see [Factoring runtime classes into Midl files (.idl)](./author-apis.md#factoring-runtime-classes-into-midl-files-idl)

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

You work with a C++/CX object via a handle to it, commonly known as a hat (\^) reference. You create a new object via the `ref new` keyword, which in turn calls [**RoActivateInstance**](/windows/desktop/api/roapi/nf-roapi-roactivateinstance) to activate a new instance of the runtime class.

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

## How the default constructor affects collections

C++ collection types use the default constructor, which can result in unintended object construction.

| Scenario | C++/CX | C++/WinRT (incorrect) | C++/WinRT (correct) |
| - | - | - | - |
| Local variable, initially empty | `TextBox^ textBox;` | `TextBox textBox; // Creates a TextBox!` | `TextBox textBox{ nullptr };` |
| Member variable, initially empty | `class C {`<br/>&nbsp;&nbsp;`TextBox^ textBox;`<br/>`};` | `class C {`<br/>&nbsp;&nbsp;`TextBox textBox; // Creates a TextBox!`<br/>`};` | `class C {`<br/>&nbsp;&nbsp;`TextBox textbox{ nullptr };`<br/>`};` |
| Global variable, initially empty | `TextBox^ g_textBox;` | `TextBox g_textBox; // Creates a TextBox!` | `TextBox g_textBox{ nullptr };` |
| Vector of empty references | `std::vector<TextBox^> boxes(10);` | `// Creates 10 TextBox objects!`<br/>`std::vector<TextBox> boxes(10);` | `std::vector<TextBox> boxes(10, nullptr);` |
| Set a value in a map | `std::map<int, TextBox^> boxes;`<br/>`boxes[2] = value;` | `std::map<int, TextBox> boxes;`<br/>`// Creates a TextBox at 2,`<br/>`// then overwrites it!`<br/>`boxes[2] = value;` | `std::map<int, TextBox> boxes;`<br/>`boxes.insert_or_assign(2, value);` |
| Array of empty references | `TextBox^ boxes[2];` | `// Creates 2 TextBox objects!`<br/>`TextBox boxes[2];` | `TextBox boxes[2] = { nullptr, nullptr };` |
| Pair | `std::pair<TextBox^, String^> p;` | `// Creates a TextBox!`<br/>`std::pair<TextBox, String> p;` | `std::pair<TextBox, String> p{ nullptr, nullptr };` |

### More about collections of empty references

Whenever you have a **Platform::Array\^** (see [Port **Platform::Array\^**](#port-platformarray)) in C++/CX, you have the choice to port that to a **std::vector** in C++/WinRT (in fact, any contiguous container) rather than leave it as an array. There are advantages to choosing **std::vector**.

For example, while there is shorthand for creating a fixed-sized vector of empty references (see table above), there's no such shorthand for creating an *array* of empty references. You have to repeat `nullptr` for each element in an array. If you have too few, then the extras will be default-constructed.

For a vector, you can fill it with empty references at initialization (as in the table above), or you can fill it with empty references post-initialization with code such as this.

```cppwinrt
std::vector<TextBox> boxes(10); // 10 default-constructed TextBoxes.
boxes.resize(10, nullptr); // 10 empty references.
```

### More about the **std::map** example

The `[]` subscript operator for **std::map** behaves like this.

- If the key is found in the map, return a reference to the existing value (which you can overwrite).
- If the key isn't found in the map, then create a new entry in the map consisting of the key (moved, if movable) and *a default-constructed value*, and return a reference to the value (which you can then overwrite).

In other words, the `[]` operator always creates an entry in the map. This is different from C#, Java, and JavaScript.

## Converting from a base runtime class to a derived one

It's common to have a reference-to-base that you know refers to an object of a derived type. In C++/CX, you use `dynamic_cast` to *cast* the reference-to-base into a reference-to-derived. The `dynamic_cast` is really just a hidden call to [**QueryInterface**](/windows/desktop/api/unknwn/nf-unknwn-iunknown-queryinterface(q_)). Here's a typical example&mdash;you're handling a dependency property changed event, and you want to cast from **DependencyObject** back to the actual type that owns the dependency property.

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

## Derived classes

In order to derive from a runtime class, the base class must be *composable*. C++/CX doesn't require that you take any special steps to make your classes composable, but C++/WinRT does. You use the [unsealed keyword](/uwp/midl-3/intro#base-classes) to indicate that you want your class to be usable as a base class.

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

In your implementation header class, you must include the base class header file before you include the autogenerated header for the derived class. Otherwise you'll get errors such as "Illegal use of this type as an expression".

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

## Boxing and unboxing

C++/CX automatically boxes scalars into objects. C++/WinRT requires you to call the [**winrt::box_value**](/uwp/cpp-ref-for-winrt/box-value) function explicitly. Both languages require you to unbox explicitly. See [Boxing and unboxing with C++/WinRT](./boxing.md).

In the tables that follows, we'll use these definitions.

| C++/CX | C++/WinRT|
|-|-|
| `int i;` | `int i;` |
| `String^ s;` | `winrt::hstring s;` |
| `Object^ o;` | `IInspectable o;`|

| Operation | C++/CX | C++/WinRT|
|-|-|-|-|
| Boxing | `o = 1;`<br>`o = "string";` | `o = box_value(1);`<br>`o = box_value(L"string");` |
| Unboxing | `i = (int)o;`<br>`s = (String^)o;` | `i = unbox_value<int>(o);`<br>`s = unbox_value<winrt::hstring>(o);` |

C++/CX and C# raise exceptions if you try to unbox a null pointer to a value type. C++/WinRT considers this a programming error, and it crashes. In C++/WinRT, use the [**winrt::unbox_value_or**](/uwp/cpp-ref-for-winrt/unbox-value-or) function if you want to handle the case where the object is not of the type that you thought it was.

| Scenario | C++/CX | C++/WinRT|
|-|-|-|-|
| Unbox a known integer | `i = (int)o;` | `i = unbox_value<int>(o);` |
| If o is null | `Platform::NullReferenceException` | Crash |
| If o is not a boxed int | `Platform::InvalidCastException` | Crash |
| Unbox int, use fallback if null; crash if anything else | `i = o ? (int)o : fallback;` | `i = o ? unbox_value<int>(o) : fallback;` |
| Unbox int if possible; use fallback for anything else | `auto box = dynamic_cast<IBox<int>^>(o);`<br>`i = box ? box->Value : fallback;` | `i = unbox_value_or<int>(o, fallback);` |

### Boxing and unboxing a string

A string is in some ways a value type, and in other ways a reference type. C++/CX and C++/WinRT treat strings differently.

The ABI type [**HSTRING**](/windows/win32/winrt/hstring) is a pointer to a reference-counted string. But it doesn't derive from [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable), so it's not technically an *object*. Furthermore, a null **HSTRING** represents the empty string. Boxing of things not derived from **IInspectable** is done by wrapping them inside an [**IReference\<T\>**](/uwp/api/windows.foundation.ireference_t_), and the Windows Runtime provides a standard implementation in the form of the [**PropertyValue**](/uwp/api/windows.foundation.propertyvalue) object (custom types are reported as [**PropertyType::OtherType**](/uwp/api/windows.foundation.propertytype)).

C++/CX represents a Windows Runtime string as a reference type; while C++/WinRT projects a string as a value type. This means that a boxed null string can have different representations depending how you got there.

Furthermore, C++/CX allows you to dereference a null **String^**, in which case it behaves like the string `""`.

| Behavior | C++/CX | C++/WinRT|
|-|-|-|
| Declarations | `Object^ o;`<br>`String^ s;` | `IInspectable o;`<br>`hstring s;` |
| String type category | Reference type | Value type |
| null **HSTRING** projects as | `(String^)nullptr` | `hstring{}` |
| Are null and `""` identical? | Yes | Yes |
| Validity of null | `s = nullptr;`<br>`s->Length == 0` (valid) | `s = hstring{};`<br>`s.size() == 0` (valid) |
| If you assign null string to object | `o = (String^)nullptr;`<br>`o == nullptr` | `o = box_value(hstring{});`<br>`o != nullptr` |
| If you assign `""` to object | `o = "";`<br>`o == nullptr` | `o = box_value(hstring{L""});`<br>`o != nullptr` |

Basic boxing and unboxing.

| Operation | C++/CX | C++/WinRT|
|-|-|-|
| Box a string | `o = s;`<br>Empty string becomes nullptr. | `o = box_value(s);`<br>Empty string becomes non-null object. |
| Unbox a known string | `s = (String^)o;`<br>Null object becomes empty string.<br>InvalidCastException if not a string. | `s = unbox_value<hstring>(o);`<br>Null object crashes.<br>Crash if not a string. |
| Unbox a possible string | `s = dynamic_cast<String^>(o);`<br>Null object or non-string becomes empty string. | `s = unbox_value_or<hstring>(o, fallback);`<br>Null or non-string becomes fallback.<br>Empty string preserved. |

## Concurrency and asynchronous operations

The Parallel Patterns Library (PPL) ([**concurrency::task**](/cpp/parallel/concrt/reference/task-class), for example) was updated to support C++/CX hat references.

For C++/WinRT, you should use coroutines and `co_await` instead. For more info, and code examples, see [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md).

## Consuming objects from XAML markup

In a C++/CX project, you can consume private members and named elements from XAML markup. But in C++/WinRT, all entities consumed by using the XAML [**{x:Bind} markup extension**](../xaml-platform/x-bind-markup-extension.md) must be exposed publicly in IDL.

Also, binding to a Boolean displays `true` or `false` in C++/CX, but it shows **Windows.Foundation.IReference`1\<Boolean\>** in C++/WinRT.

For more info, and code examples, see [Consuming objects from markup](./binding-property.md#consuming-objects-from-xaml-markup).

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

In cases where C++/CX requires you to use an array, C++/WinRT allows you to use any contiguous container. See [How the default constructor affects collections](#how-the-default-constructor-affects-collections) for a reason why **std::vector** is a good choice.

So, whenever you have a **Platform::Array\^** in C++/CX, your porting options include using an initializer list, a **std::array**, or a **std::vector**. For more info, and code examples, see [Standard initializer lists](./std-cpp-data-types.md#standard-initializer-lists) and [Standard arrays and vectors](./std-cpp-data-types.md#standard-arrays-and-vectors).

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

With C++/CX, you can access the [**Platform::String::Data**](/cpp/cppcx/platform-string-class?view=vs-2019#data) property to retrieve the string as a C-style **const wchar_t\*** array (for example, to pass it to **std::wcout**).

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

C++/CX types provide the [Object::ToString](/cpp/cppcx/platform-object-class#tostring) method.

```cppcx
int i{ 2 };
auto s{ i.ToString() }; // s is a Platform::String^ with value L"2".
```

C++/WinRT doesn't directly provide this facility, but you can turn to alternatives.

```cppwinrt
int i{ 2 };
auto s{ std::to_wstring(i) }; // s is a std::wstring with value L"2".
```

C++/WinRT also supports [**winrt::to_hstring**](/uwp/cpp-ref-for-winrt/to-hstring) for a limited number of types. You'll need to add overloads for any additional types you want to stringify.

| Language | Stringify int | Stringify enum |
| - | - | - |
| C++/CX | `String^ result = "hello, " + intValue.ToString();` | `String^ result = "status: " + status.ToString();` |
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

#### String-building

C++/CX and C++/WinRT defer to the standard **std::wstringstream** for string building.

| Operation | C++/CX | C++/WinRT |
|-|-|-|
| Append string, preserving nulls | `stream.print(s->Data(), s->Length);` | `stream << std::wstring_view{ s };` |
| Append string, stop on first null | `stream << s->Data();` | `stream << s.c_str();` |
| Extract result | `ws = stream.str();` | `ws = stream.str();` |

#### More examples

In the examples below, *ws* is a variable of type **std::wstring**. Also, while C++/CX can construct a **Platform::String** from an 8-bit string, C++/WinRT doesn't do that.

| Operation | C++/CX | C++/WinRT |
| - | - | - |
| Construct string from literal | `String^ s = "hello";`<br>`String^ s = L"hello";` | `// winrt::hstring s{ "hello" }; // Doesn't compile`<br>`winrt::hstring s{ L"hello" };` |
| Convert from **std::wstring**, preserving nulls | `String^ s = ref new String(ws.c_str(),`<br>&nbsp;&nbsp;`(uint32_t)ws.size());` | `winrt::hstring s{ ws };`<br>`s = winrt::hstring(ws);`<br>`// s = ws; // Doesn't compile` |
| Convert from **std::wstring**, stop on first null | `String^ s = ref new String(ws.c_str());` | `winrt::hstring s{ ws.c_str() };`<br>`s = winrt::hstring(ws.c_str());`<br>`// s = ws.c_str(); // Doesn't compile` |
| Convert to **std::wstring**, preserving nulls | `std::wstring ws{ s->Data(), s->Length };`<br>`ws = std::wstring(s>Data(), s->Length);` | `std::wstring ws{ s };`<br>`ws = s;` |
| Convert to **std::wstring**, stop on first null | `std::wstring ws{ s->Data() };`<br>`ws = s->Data();` | `std::wstring ws{ s.c_str() };`<br>`ws = s.c_str();` |
| Pass literal to method | `Method("hello");`<br>`Method(L"hello");` | `// Method("hello"); // Doesn't compile`<br>`Method(L"hello");` |
| Pass **std::wstring** to method | `Method(ref new String(ws.c_str(),`<br>&nbsp;&nbsp;`(uint32_t)ws.size()); // Stops on first null` | `Method(ws);`<br>`// param::winrt::hstring accepts std::wstring_view` |

## Important APIs

* [winrt::delegate struct template](/uwp/cpp-ref-for-winrt/delegate)
* [winrt::hresult_error struct](/uwp/cpp-ref-for-winrt/error-handling/hresult-error)
* [winrt::hstring struct](/uwp/cpp-ref-for-winrt/hstring)
* [winrt namespace](/uwp/cpp-ref-for-winrt/winrt)

## Related topics

* [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx)
* [Author events in C++/WinRT](./author-events.md)
* [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md)
* [Consume APIs with C++/WinRT](./consume-apis.md)
* [Handle events by using delegates in C++/WinRT](./handle-events.md)
* [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md)
* [Asynchrony, and interop between C++/WinRT and C++/CX](./interop-winrt-cx-async.md)
* [Microsoft Interface Definition Language 3.0 reference](/uwp/midl-3)
* [Move to C++/WinRT from WRL](./move-to-winrt-from-wrl.md)
* [String handling in C++/WinRT](./strings.md)