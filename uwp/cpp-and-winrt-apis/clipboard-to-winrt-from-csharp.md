---
title: Porting the Clipboard sample to C++/WinRT from C# (a case study)
description: This topic presents a case study of porting one of the [Universal Windows Platform (UWP) app samples](https://github.com/microsoft/Windows-universal-samples) from [C#](/visualstudio/get-started/csharp) to [C++/WinRT](./intro-to-using-cpp-with-winrt.md).
ms.date: 09/06/2022
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, C#, sample, clipboard, case, study
ms.localizationpriority: medium
---

# Porting the Clipboard sample to C++/WinRT from C#&mdash;a case study

This topic presents a case study of porting one of the [Universal Windows Platform (UWP) app samples](https://github.com/microsoft/Windows-universal-samples) from [C#](/visualstudio/get-started/csharp) to [C++/WinRT](./intro-to-using-cpp-with-winrt.md). You can gain porting practice and experience by following along with the walkthrough and porting the sample for yourself as you go.

For a comprehensive catalog of the technical details involved in porting to C++/WinRT from C#, see the companion topic [Move to C++/WinRT from C#](./move-to-winrt-from-csharp.md).

## A brief preface about C# and C++ source code files

In a C# project, your source code files are primarily `.cs` files. When you move to C++, you'll notice that there are more kinds of source code files to get used to. The reason is to do with the difference between compilers, the way C++ source code is reused, and the notions of *declaring* and *defining* a type and its functions (its methods).

A function *declaration* describes just the function's *signature* (its return type, its name, and its parameter types and names). A function *definition* includes the function's *body* (its implementation).

It's a little different when it comes to types. You *define* a type by providing its name and by (at a minimum) just *declaring* all of its member functions (and other members). That's right, you can *define* a type even if you don't define its member functions.

- Common C++ source code files are `.h` (*dot aitch*) and `.cpp` files. A `.h` file is a *header* file, and it defines one or more types. While you *can* define member functions in a header, that's typically what a `.cpp` file is for. So for a hypothetical C++ type **MyClass**, you'd define **MyClass** in `MyClass.h`, and you'd define its member functions in `MyClass.cpp`. For other developers to re-use your classes, you'd share out just the `.h` files and object code. You'd keep your `.cpp` files secret, because the implementation constitutes your intellectual property.
- Precompiled header (`pch.h`). Typically there's a set of header files that you include in your application, and you don't change those files very often. So rather than processing the contents of that set of headers each time you compile, you can aggregate those headers into one file, compile that once, and then use the output of that precompilation step each time you build. You do that via a *precompiled header* file (usually named `pch.h`).
- `.idl` files. These files contain Interface Definition Language (IDL). You can think of IDL as header files for Windows Runtime types. We'll talk more about IDL in the section [IDL for the **MainPage** type](#idl-for-the-mainpage-type).

## Download and test the Clipboard sample

Visit the [Clipboard sample](/samples/microsoft/windows-universal-samples/clipboard/) web page, and click **Download ZIP**. Unzip the downloaded file, and take a look at the folder structure.

- The C# version of the sample source code is contained in the folder named `cs`.
- The C++/WinRT version of the sample source code is contained in the folder named `cppwinrt`.
- Other files&mdash;used by both the C# version and the C++/WinRT version&mdash;can be found in the `shared` and `SharedContent` folders.

The walkthrough in this topic shows how you can recreate the C++/WinRT version of the Clipboard sample by porting it from the C# source code. That way, you can see how you can port your own C# projects to C++/WinRT.

To get a feel for what the sample does, open the C# solution (`\Clipboard_sample\cs\Clipboard.sln`), change the configuration as appropriate (perhaps to *x64*), build, and run. The sample's own user interface (UI) guides you through its various features, step by step.

> [!TIP]
> The root folder of the sample that you downloaded might be named `Clipboard` rather than `Clipboard_sample`. But we'll continue to refer to that folder as `Clipboard_sample` to distinguish it from the C++/WinRT version that you'll be creating in a later step.

## Create a Blank App (C++/WinRT), named Clipboard

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

Begin the porting process by creating a new C++/WinRT project in Microsoft Visual Studio. Create a new project using the **Blank App (C++/WinRT)** project template. Set its name to *Clipboard*, and (so that your folder structure will match the walkthrough) make sure that **Place solution and project in the same directory** is unchecked.

Just to get a baseline, make sure that this new, empty, project builds and runs.

## Package.appxmanifest, and asset files

If the C# and C++/WinRT versions of the sample don't need to be installed side by side on the same machine, then the two projects' app package manifest source files (`Package.appxmanifest`) can be identical. In that case, you can just copy `Package.appxmanifest` from the C# project to the C++/WinRT project, and you're done.

For the two versions of the sample to coexist, they need different identifiers. In that case, in the C++/WinRT project, open the `Package.appxmanifest` file in an XML editor, and make a note of these three values.

- Inside the **/Package/Identity** element, note the value of the **Name** attribute. This is the *package name*. For a newly-created project, the project will give it an initial value of a unique GUID.
- Inside the **/Package/Applications/Application** element, note the value of the **Id** attribute. This is the *application id*.
- Inside the **/Package/mp:PhoneIdentity** element, note the value of the **PhoneProductId** attribute. Again, for a newly-created project, this will be set to the same GUID as the package name is set to.

Then copy `Package.appxmanifest` from the C# project to the C++/WinRT project. Finally, you can restore the three values that you noted. Or you can edit the copied values to make them unique and/or appropriate for the application and for your organization (as you ordinarily would for a new project). For example, in this case instead of restoring the value of the package name, we can just change the copied value from *Microsoft.SDKSamples.Clipboard.CS* to *Microsoft.SDKSamples.Clipboard.CppWinRT*. And we can leave the application id set to *App*. As long as either the package name *or* the application id are different, then the two applications will have different Application User Model IDs (AUMIDs). And that's what's necessary for two apps to be installed side by side on the same machine.

For the purposes of this walkthrough, it makes sense to make a few other changes in `Package.appxmanifest`. There are three occurrences of the string *Clipboard C# Sample*. Change that to *Clipboard C++/WinRT Sample*.

In the C++/WinRT project, the `Package.appxmanifest` file and the project are now out of sync with respect to the asset files that they reference. To remedy that, first remove the assets from the C++/WinRT project by selecting all the files in the `Assets` folder (in Solution Explorer in Visual Studio) and removing them (choose **Delete** in the dialog).

The C# project references asset files from a shared folder. You can do the same in the C++/WinRT project, or you can copy the files as we'll do in this walkthrough.

Navigate to the `\Clipboard_sample\SharedContent\media` folder. Select the seven files that the C# project includes (`microsoft-sdk.png`, `smalltile-sdk.png`, `splash-sdk.png`, `squaretile-sdk.png`, `storelogo-sdk.png`, `tile-sdk.png`, and `windows-sdk.png`), copy them, and paste them into the `\Clipboard\Clipboard\Assets` folder in the new project.

Right-click the `Assets` folder (in Solution Explorer in the C++/WinRT project) > **Add** > **Existing item...** and navigate to `\Clipboard\Clipboard\Assets`. In the file picker, select the seven files and click **Add**.

`Package.appxmanifest` is now back in sync with the project's asset files.

## **MainPage**, including the functionality that configures the sample

The Clipboard sample&mdash;like all of the [Universal Windows Platform (UWP) app samples](https://github.com/microsoft/Windows-universal-samples)&mdash;consists of a collection of scenarios that the user can step through one at a time. The collection of scenarios in a given sample is configured in the sample's source code. Each scenario in the collection is a data item that stores a title, as well as the type of the class in the project that implements the scenario.

In the C# version of the sample, if you look in the `SampleConfiguration.cs` source code file, you'll see two classes. Most of the configuration logic is in the **MainPage** class, which is a partial class (it forms a complete class when combined with the markup in `MainPage.xaml` and the imperative code in `MainPage.xaml.cs`). The other class in this source code file is **Scenario**, with its **Title** and **ClassType** properties.

Over the next few subsections, we'll look at how to port **MainPage** and **Scenario**.

### IDL for the **MainPage** type

Let's begin this section by talking briefly about Interface Definition Language (IDL), and how it helps us when we're programming with C++/WinRT. IDL is a kind of source code that describes the callable surface of a Windows Runtime type. The callable (or public) surface of a type is *projected* out into the world, so that the type can be consumed. That *projected* portion of the type contrasts with the actual internal implementation of the type, which is of course not callable, and not public. It's only the projected portion that we define in IDL.

Having authored IDL source code (within an `.idl` file), you can then compile the IDL into machine-readable metadata files (also known as Windows Metadata). Those metadata files have the extension `.winmd`, and here are some of their uses.

- A `.winmd` can describe the Windows Runtime types in a component. When you reference a Windows Runtime Component (WRC) from an application project, the application project reads the Windows Metadata belonging to the WRC (that metadata may be in a separate file, or it may be packaged into the same file as the WRC itself) so that you can consume the WRC's types from within the application.
- A `.winmd` can describe the Windows Runtime types in one part of your application so that they can be consumed by a different part of the same application. For example, a Windows Runtime type that's consumed by a XAML page in the same app.
- To make it easier for you to consume Windows Runtime types (built-in or third party), the C++/WinRT build system uses `.winmd` files to generate wrapper types to represent the projected portions of those Windows Runtime types.
- To make it easier for you to implement your own Windows Runtime types, the C++/WinRT build system turns your IDL into a `.winmd` file, and then uses that to generate wrappers for your projection, as well as stubs on which to base your implementation (we'll talk more about these stubs later in this topic).

The specific version of IDL that we use with C++/WinRT is [Microsoft Interface Definition Language 3.0](/uwp/midl-3/intro). In the remainder of this section of the topic, we'll examine the C# **MainPage** type in some detail. We'll decide which parts of it need to be in the *projection* of the C++/WinRT **MainPage** type (that is, in its callable, or public, surface), and which can be just part of its implementation. That distinction is important because when we come to author our IDL (which we'll do in the section after this one), we'll be defining only the callable parts in there.

The C# source code files that together implement the **MainPage** type are: `MainPage.xaml` (which we'll port soon, by copying it), `MainPage.xaml.cs`, and `SampleConfiguration.cs`.

In the C++/WinRT version, we'll factor our **MainPage** type into source code files in a similar way. We'll take the logic in `MainPage.xaml.cs` and translate it for the most part to `MainPage.h` and `MainPage.cpp`. And for the logic in `SampleConfiguration.cs`, we'll translate that to `SampleConfiguration.h` and `SampleConfiguration.cpp`.

The classes in a C# Universal Windows Platform (UWP) application are of course Windows Runtime types. But when you author a type in a C++/WinRT application, you can choose whether that type is a Windows Runtime type, or a regular C++ class/struct/enumeration.

Any XAML page in our project needs to be a Windows Runtime type, so **MainPage** needs to be a Windows Runtime type. In the C++/WinRT project, **MainPage** is already a Windows Runtime type, so we don't need to change that aspect of it. Specifically, it's a *runtime class*.

- For more details about whether or not you should author a runtime class for a given type, see the topic [Author APIs with C++/WinRT](./author-apis.md).
- In C++/WinRT, the internal implementation of a runtime class, and the projected (public) parts of it, exist in the form of two different classes. These are known as the *implementation type* and the *projected type*. You can learn more about them in the topic mentioned in the previous bullet-point, and also in [Consume APIs with C++/WinRT](./consume-apis.md).
- For more info about the connection between runtime classes and IDL (`.idl` files), you can read and follow along with the topic [XAML controls; bind to a C++/WinRT property](./binding-property.md). That topic walks through the process of authoring a new runtime class, the first step of which is to add a new **Midl File (.idl)** item to the project.

For **MainPage**, we actually have the necessary `MainPage.idl` file already in the C++/WinRT project. That's because the project template created it for us. But later in this walkthrough we'll be adding further `.idl` files to the project.

We'll shortly see a listing of exactly what IDL we need to add to the existing `MainPage.idl` file. Before that, we have some reasoning to do about what does, and what doesn't, need to go in the IDL.

To determine which members of **MainPage** we need to declare in `MainPage.idl` (so that they become part of the **MainPage** runtime class), and which can simply be members of the **MainPage** implementation type, let's make a list of the members of the C# **MainPage** class. We find those members by looking in `MainPage.xaml.cs` and in `SampleConfiguration.cs`.

We find a total of twelve `protected` and `private` fields and methods. And we find the following `public` members.

- The default constructor `MainPage()`.
- The static fields **Current** and **FEATURE_NAME**.
- The properties **IsClipboardContentChangedEnabled** and **Scenarios**.
- The methods **BuildClipboardFormatsOutputString**, **DisplayToast**, **EnableClipboardContentChangedNotifications**, and **NotifyUser**.

It's those `public` members that are candidates for declaring in `MainPage.idl`. So let's examine each one and see whether they need to be part of the **MainPage** runtime class, or whether they need only to be part of its implementation.

- The default constructor `MainPage()`. For a XAML **Page**, it's normal to declare a default constructor in its IDL. That way, the XAML UI framework can activate the type.
- The static field **Current** is used from within the individual scenario XAML pages to access the application's instance of **MainPage**. Since **Current** isn't being used to interoperate with the XAML framework (nor is it used across compilation units), we could reserve it to be solely a member of the implementation type. With your own projects in cases like this, you might choose to do that. But since the field is an instance of the projected type, it feels logical to declare it in the IDL. So that's what we'll do here (and doing so also makes the code slightly cleaner).
- It's a similar case for the static **FEATURE_NAME** field, which is accessed within the **MainPage** type. Again, choosing to declare it in the IDL makes our code slightly cleaner.
- The property **IsClipboardContentChangedEnabled** is used only in the **OtherScenarios** class. So during the port, we'll simplify things a little, and make it a private field of the **OtherScenarios** runtime class. So that one won't go in the IDL.
- The property **Scenarios** is a collection of objects of type **Scenario** (a type that we mentioned earlier). We'll talk about **Scenario** in the next subsection, so let's leave the **Scenarios** property until then, too.
- The methods **BuildClipboardFormatsOutputString**, **DisplayToast**, and **EnableClipboardContentChangedNotifications** are utility functions that feel more to do with the general state of the sample than about the main page. So during the port, we'll refactor these three methods onto a new utility type named **SampleState** (which doesn't need to be a Windows Runtime type). For that reason, these three methods won't go in the IDL.
- The method **NotifyUser** is called from within the individual scenario XAML pages on the instance of **MainPage** that's returned from the static *Current* field. Since (as already noted) **Current** is an instance of the projected type, we need to declare **NotifyUser** in the IDL. **NotifyUser** takes a parameter of type **NotifyType**. We'll talk about that in the next subsection.

Any member that you want to databind to also needs to be declared in IDL (whether you're using `{x:Bind}` or `{Binding}`). For more info, see [Data binding](../data-binding/index.md).

We're making progress: we're developing a list of which members to add, and which not to add, to the `MainPage.idl` file. But we still have to discuss the **Scenarios** property, and the **NotifyType** type. So let's do that next.

### IDL for the **Scenario** and **NotifyType** types

The **Scenario** class is defined in `SampleConfiguration.cs`. We have a decision to make about how to port that class to C++/WinRT. By default, we would probably make it an ordinary C++ `struct`. But if **Scenario** is being used across binaries, or to interoperate with the XAML framework, then it needs to be declared in IDL as a Windows Runtime type.

Studying the C# source code, we find that **Scenario** is used in this context.

```xaml
<ListBox x:Name="ScenarioControl" ... >
```

```csharp
var itemCollection = new List<Scenario>();
int i = 1;
foreach (Scenario s in scenarios)
{
    itemCollection.Add(new Scenario { Title = $"{i++}) {s.Title}", ClassType = s.ClassType });
}
ScenarioControl.ItemsSource = itemCollection;
```

A collection of **Scenario** objects is being assigned to the [**ItemsSource**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource) property of a **ListBox** (which is an items control). Since **Scenario** *does* need to interoperate with XAML, it needs to be a Windows Runtime type. So it needs to be defined in IDL. Defining the **Scenario** type in IDL causes the C++/WinRT build system to generate a source code definition of **Scenario** for you in a behind-the-scenes header file (the name and location of which are not important for this walkthrough).

And you'll recall that **MainPage.Scenarios** is a collection of **Scenario** objects, which we've just said need to be in IDL. For that reason, **MainPage.Scenarios** itself also needs to be declared in the IDL.

**NotifyType** is an `enum` declared in C#'s `MainPage.xaml.cs`. Because we pass **NotifyType** to a method belonging to the **MainPage** runtime class, **NotifyType** too needs to be a Windows Runtime type; and it needs to be defined in `MainPage.idl`.

Now let's add to the `MainPage.idl` file the new types and the new member of **Mainpage** that we've decided to declare in IDL. At the same time, we'll remove from the IDL the placeholder members of **Mainpage** that the Visual Studio project template gave us.

So, in your C++/WinRT project, open `MainPage.idl`, and edit it so that it looks like the listing below. Note that one of the edits is to change the namespace name from **Clipboard** to **SDKTemplate**. If you like, you can just replace the entire contents of `MainPage.idl` with the following code. Another tweak to note is that we're changing the name of **Scenario::ClassType** to **Scenario::ClassName**.

```idl
// MainPage.idl
namespace SDKTemplate
{
    struct Scenario
    {
        String Title;
        Windows.UI.Xaml.Interop.TypeName ClassName;
    };

    enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

    [default_interface]
    runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
    {
        MainPage();

        static MainPage Current{ get; };
        static String FEATURE_NAME{ get; };

        static Windows.Foundation.Collections.IVector<Scenario> scenarios{ get; };

        void NotifyUser(String strMessage, NotifyType type);
    };
}
```

> [!NOTE]
> For more info about the contents of an `.idl` file in a C++/WinRT project, see [Microsoft Interface Definition Language 3.0](/uwp/midl-3/).

With your own porting work, you might not want or need to change the namespace name like we did above. We're doing it here only because the default namespace of the C# project that we're porting is **SDKTemplate**; while the name of the project and of the assembly is **Clipboard**.

But, as we proceed with the port in this walkthrough, we'll be changing every occurrence in source code of the **Clipboard** namespace name to **SDKTemplate**. There's also a place in C++/WinRT project properties where the **Clipboard** namespace name appears, so we'll take the opportunity to change that now.

In Visual Studio, for the C++/WinRT project, set the project property **Common Properties** \> **C++/WinRT** \> **Root Namespace** to the value *SDKTemplate*.

### Save the IDL, and re-generate stub files

The topic [XAML controls; bind to a C++/WinRT property](./binding-property.md) introduces the notion of *stub files*, and shows you a walkthrough of them in action. We also mentioned stubs earlier in this topic when we mentioned that C++/WinRT build system turns the contents of your `.idl` files into Windows Metadata, and then from that metadata a tool named `cppwinrt.exe` generates stubs on which you can base your implementation.

Each time you add, remove, or change something in your IDL, and build, the build system updates the stub implementations in those stubs files. So each time you change your IDL and build, we recommend that you view those stubs files, copy any changed signatures, and paste them into your project. We'll give more specifics and examples of exactly how to do that in a moment. But the advantage of doing this is to give you an error-free way of knowing at all times what the shape of your implementation type should be, and what the signature of its methods should be.

At this point in the walkthrough, we're done editing the `MainPage.idl` file for the time being, so you should save it now. The project won't build to completion at the moment, but performing a build now is a useful thing to do because it regenerates the stub files for **MainPage**. So build the project now, and disregard any build errors.

For this C++/WinRT project, the stub files are generated in the `\Clipboard\Clipboard\Generated Files\sources` folder. You'll find them there after the partial build has come to an end (again, as expected, the build won't succeed entirely. But the step that we're interested in&mdash;generating stubs&mdash;*will* have succeeded). The files we're interested in are `MainPage.h` and `MainPage.cpp`.

In those two stub files, you'll see new stub implementations of the members of **MainPage** that we added to the IDL (**Current** and **FEATURE_NAME**, for example). You'll want to copy those stub implementations into the `MainPage.h` and `MainPage.cpp` files that are already in the project. At the same time, just as we did with the IDL, we'll remove from those existing files the placeholder members of **Mainpage** that the Visual Studio project template gave us (the dummy property named **MyProperty**, and the event handler named **ClickHandler**).

In fact, the only member of the current version of **MainPage** that we want to keep is the constructor.

Once you've copied the new members from the stub files, deleted the members we don't want, and updated the namespace, the `MainPage.h` and `MainPage.cpp` files in your project should look like the code listings below. Notice that there are two **MainPage** types. One in the **implementation** namespace, and a second one in the **factory_implementation** namespace. The only change we've made to the **factory_implementation** one is to add **SDKTemplate** to its namespace.

```cppwinrt
// MainPage.h
#pragma once
#include "MainPage.g.h"

namespace winrt::SDKTemplate::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
        MainPage();

        static SDKTemplate::MainPage Current();
        static hstring FEATURE_NAME();
        static Windows::Foundation::Collections::IVector<SDKTemplate::Scenario> scenarios();
        void NotifyUser(hstring const& strMessage, SDKTemplate::NotifyType const& type);
    };
}
namespace winrt::SDKTemplate::factory_implementation
{
    struct MainPage : MainPageT<MainPage, implementation::MainPage>
    {
    };
}
```

```cppwinrt
// MainPage.cpp
#include "pch.h"
#include "MainPage.h"
#include "MainPage.g.cpp"

namespace winrt::SDKTemplate::implementation
{
    MainPage::MainPage()
    {
        InitializeComponent();
    }
    SDKTemplate::MainPage MainPage::Current()
    {
        throw hresult_not_implemented();
    }
    hstring MainPage::FEATURE_NAME()
    {
        throw hresult_not_implemented();
    }
    Windows::Foundation::Collections::IVector<SDKTemplate::Scenario> MainPage::scenarios()
    {
        throw hresult_not_implemented();
    }
    void MainPage::NotifyUser(hstring const& strMessage, SDKTemplate::NotifyType const& type)
    {
        throw hresult_not_implemented();
    }
}
```

For strings, C# uses **System.String**. See the **MainPage.NotifyUser** method for an example. In our IDL, we declare a string with **String**, and when the `cppwinrt.exe` tool generates C++/WinRT code for us, it uses the [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) type. Any time we come across a string in C# code, we'll port that to **winrt::hstring**. For more info, see [String handling in C++/WinRT](./strings.md).

For an explanation of the `const&` parameters in the method signatures, see [Parameter-passing](./concurrency.md#parameter-passing).

### Update all remaining namespace declarations/references, and build

Before building the C++/WinRT project, find any declarations of (and references to) the **Clipboard** namespace, and change them to **SDKTemplate**.

- `MainPage.xaml` and `App.xaml`. The namespace appears in the values of the `x:Class` and `xmlns:local` attributes.
- `App.idl`.
- `App.h`.
- `App.cpp`. There are two `using namespace` directives (search for the substring `using namespace Clipboard`), and two qualifications of the **MainPage** type (search for `Clipboard::MainPage`). Those need changing.

Since we removed the event handler from **MainPage**, also go into `MainPage.xaml` and delete the **Button** element from the markup.

Save all the files. Clean the solution (**Build** > **Clean Solution**), and then build it. Having followed all of the changes so far, exactly as written, the build is expected to succeed.

### Implement the **MainPage** members that we declared in IDL

#### The constructor, **Current**, and **FEATURE_NAME**

Here's the relevant code (from the C# project) that we need to port.

```xaml
<!-- MainPage.xaml -->
...
<TextBlock x:Name="SampleTitle" ... />
...
```

```csharp
// MainPage.xaml.cs
...
public sealed partial class MainPage : Page
{
    public static MainPage Current;

    public MainPage()
    {
        InitializeComponent();
        Current = this;
        SampleTitle.Text = FEATURE_NAME;
    }
...
}
...

// SampleConfiguration.cs
...
public partial class MainPage : Page
{
    public const string FEATURE_NAME = "Clipboard C# sample";
...
}
...
```

Soon, we'll be re-using `MainPage.xaml` in its entirety (by copying it). For now (below), we'll temporarily add a **TextBlock** element, with the appropriate name, into the `MainPage.xaml` of the C++/WinRT project.

**FEATURE_NAME** is a static field of **MainPage** (a C# `const` field is essentially static in its behavior), defined in `SampleConfiguration.cs`. For C++/WinRT, instead of a (static) field, we'll make it the C++/WinRT expression of a (static) read-only property. The C++/WinRT way of expressing a property getter is as a function that returns the property value, and takes no parameters (an accessor). So the C# **FEATURE_NAME** static field becomes the C++/WinRT **FEATURE_NAME** static accessor function (in this case, returning the string literal).

Incidentally, we'd do the same thing if we were porting a C# read-only property. For a C# writeable property, the C++/WinRT way of expressing a property setter is as a `void` function that takes the property value as a parameter (a mutator). In either case, if the C# field or property is static, then so is the C++/WinRT accessor and/or mutator.

**Current** is a static (not a constant) field of **MainPage**. Again, we'll make it (the C++/WinRT expression of) a read-only property, and again make it static. Where **FEATURE_NAME** is constant, **Current** is not. So in C++/WinRT we'll need a backing field, and our accessor will return that. So, in the C++/WinRT project, we'll declare in `MainPage.h` a private static field named **current**, we'll define/initialize **current** in `MainPage.cpp` (because it has static storage duration), and we'll access it via a public static accessor function named **Current**.

The constructor itself performs a couple of assignments, which are straightforward to port.

In the C++/WinRT project, add a new **Visual C++** > **Code** > **C++ File (.cpp)** item with the name `SampleConfiguration.cpp`.

Edit `MainPage.xaml`, `MainPage.h`, `MainPage.cpp`, and `SampleConfiguration.cpp` to match the listings below.

```xaml
<!-- MainPage.xaml -->
...
<StackPanel ...>
    <TextBlock x:Name="SampleTitle" />
</StackPanel>
...
```

```cppwinrt
// MainPage.h
...
namespace winrt::SDKTemplate::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
...
        static SDKTemplate::MainPage Current() { return current; }
...
    private:
        static SDKTemplate::MainPage current;
...
    };
...
}

// MainPage.cpp
...
namespace winrt::SDKTemplate::implementation
{
    SDKTemplate::MainPage MainPage::current{ nullptr };

    MainPage::MainPage()
    {
        InitializeComponent();
        MainPage::current = *this;
        SampleTitle().Text(FEATURE_NAME());
    }
...
}

// SampleConfiguration.cpp
#include "pch.h"
#include "MainPage.h"

using namespace winrt;
using namespace SDKTemplate;

hstring implementation::MainPage::FEATURE_NAME()
{
    return L"Clipboard C++/WinRT Sample";
}
```

Also, be sure to delete the existing function bodies from `MainPage.cpp` for **MainPage::Current()** and **MainPage::FEATURE_NAME()**, because we're now defining those methods elsewhere.

As you can see, **MainPage::current** is declared as being of type **SDKTemplate::MainPage**, which is the projected type. It's not of type **SDKTemplate::implementation::MainPage**, which is the implementation type. The projected type is the one that's designed to be consumed either within the project for XAML interoperation, or across binaries. The implementation type is what you use to implement the facilities that you've exposed on your projected type. Because the declaration of **MainPage::current** (in `MainPage.h`) appears within the implementation namespace (**winrt::SDKTemplate::implementation**), an unqualified **MainPage** would have referred to the implementation type. So, we qualify with **SDKTemplate::** in order to be clear that we want **MainPage::current** to be an instance of the projected type **winrt::SDKTemplate::MainPage**.

In the constructor, there are some points related to `MainPage::current = *this;` that deserve an explanation.
- When you use the `this` pointer inside a member of the implementation type, the `this` pointer is of course *a pointer to the implementation type*.
- To convert the `this` pointer to the corresponding projected type, dereference it. Provided you generate your implementation type from IDL (as we have here), the implementation type has a conversion operator that converts to its projected type. That's why the assignment here works.

For more info about those details, see [Instantiating and returning implementation types and interfaces](./author-apis.md#instantiating-and-returning-implementation-types-and-interfaces).

Also in the constructor is `SampleTitle().Text(FEATURE_NAME());`. The `SampleTitle()` part is a call to a simple accessor function named **SampleTitle**, which returns the **TextBlock** that we added to the XAML. Whenever you `x:Name` a XAML element, the XAML compiler generates an accessor for you that's named for the element. The `.Text(...)` part calls the **Text** mutator function on the **TextBlock** object that the **SampleTitle** accessor returned. And `FEATURE_NAME()` calls our static **MainPage::FEATURE_NAME** accessor function to return the string literal. Altogether, that line of code sets the **Text** property of the **TextBlock** named *SampleTitle*.

Note that since strings are wide in the Windows Runtime, to port a string literal we prefix it with the wide-char encoding prefix `L`. So we change (for example) "a string literal" to L"a string literal". Also see [Wide string literals](/cpp/cpp/string-and-character-literals-cpp#wide-string-literals).

#### **Scenarios**

Here's the relevant C# code that we need to port.

```csharp
// MainPage.xaml.cs
...
public sealed partial class MainPage : Page
{
...
    public List<Scenario> Scenarios
    {
        get { return this.scenarios; }
    }
...
}
...

// SampleConfiguration.cs
...
public partial class MainPage : Page
{
...
    List<Scenario> scenarios = new List<Scenario>
    {
        new Scenario() { Title = "Copy and paste text", ClassType = typeof(CopyText) },
        new Scenario() { Title = "Copy and paste an image", ClassType = typeof(CopyImage) },
        new Scenario() { Title = "Copy and paste files", ClassType = typeof(CopyFiles) },
        new Scenario() { Title = "Other Clipboard operations", ClassType = typeof(OtherScenarios) }
    };
...
}
...
```

From our earlier investigation, we know that this collection of **Scenario** objects is being displayed in a **ListBox**. In C++/WinRT, there are limits to *the kind of collection* that we can assign to the **ItemsSource** property of an items control. The collection must be either a vector or an observable vector, and its elements must be one of the following:

- Either runtime classes, or
- [**IInspectable**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable).

For the **IInspectable** case, if the elements are not themselves runtime classes, then those elements need to be of a kind that can be boxed and unboxed to and from [**IInspectable**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable). And that means that they have to be Windows Runtime types (see [Boxing and unboxing values to IInspectable](./boxing.md)).

For this case study, we didn't make **Scenario** a runtime class. That is still a reasonable option, though. And there'll be cases in your own porting work where a runtime class will definitely be the way to go. For example, if you need to make the element type *observable* (see [XAML controls; bind to a C++/WinRT property](./binding-property.md)), or if the element needs to have methods for any other reason, and it's more than just a set of data members.

Since, in this walkthrough, we're *not* going with a runtime class for the **Scenario** type, then we need to think about boxing. If we'd made **Scenario** a regular C++ `struct`, then we wouldn't be able to box it. But we declared **Scenario** as a `struct` in IDL, and so we *can* box it.

We're left with the choice of boxing the **Scenario** ahead of time, or waiting until we're about to assign to the **ItemsSource**, and box them on a just-in-time basis. Here are some considerations regarding those two options.

- Boxing ahead of time. For this option, our data member is a collection of **IInspectable** ready to assign to the UI. On initialization, we box the **Scenario** objects into that data member. We need only one copy of that collection, but we have to unbox an element every time we needed to read its fields.
- Boxing just in time. For this option, our data member is a collection of **Scenario**. When the time comes to assign to the UI, we box the **Scenario** objects from the data member into a new collection of **IInspectable**. We can read the fields of the elements in the data member without unboxing, but we need two copies of the collection.

As you can see, for a small collection like this, the pros and cons make it something of a wash. So, for this case study, we'll go with the just-in-time option.

The **scenarios** member is a field of **MainPage**, defined and initialized in `SampleConfiguration.cs`. And **Scenarios** is a read-only property of **MainPage**, defined in `MainPage.xaml.cs` (and implemented to simply return the **scenarios** field). We'll do something similar in the C++/WinRT project; but we'll make the two members static (since we need only one instance across the application; and so that we can access them without needing a class instance). And we'll name them *scenariosInner* and *scenarios*, respectively. We'll declare *scenariosInner* in `MainPage.h`. And, because it has static storage duration, we'll define/initialize it in a `.cpp` file (`SampleConfiguration.cpp`, in this case).

Edit `MainPage.h` and `SampleConfiguration.cpp` to match the listings below.

```cppwinrt
// MainPage.h
...
struct MainPage : MainPageT<MainPage>
{
...
    static Windows::Foundation::Collections::IVector<Scenario> scenarios() { return scenariosInner; }
...
private:
    static winrt::Windows::Foundation::Collections::IVector<Scenario> scenariosInner;
...
};

// SampleConfiguration.cpp
...
using namespace Windows::Foundation::Collections;
...
IVector<Scenario> implementation::MainPage::scenariosInner = winrt::single_threaded_observable_vector<Scenario>(
{
    Scenario{ L"Copy and paste text", xaml_typename<SDKTemplate::CopyText>() },
    Scenario{ L"Copy and paste an image", xaml_typename<SDKTemplate::CopyImage>() },
    Scenario{ L"Copy and paste files", xaml_typename<SDKTemplate::CopyFiles>() },
    Scenario{ L"History and roaming", xaml_typename<SDKTemplate::HistoryAndRoaming>() },
    Scenario{ L"Other Clipboard operations", xaml_typename<SDKTemplate::OtherScenarios>() },
});
```

Also, be sure to delete the existing function body from `MainPage.cpp` for **MainPage::scenarios()**, because we're now defining that method in the header file.

As you can see, in `SampleConfiguration.cpp`, we initialize the static data member *scenariosInner* by calling a C++/WinRT helper function named [winrt::single_threaded_observable_vector](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector). That function creates a new Windows Runtime collection object for us, and returns it as an [**IObservableVector**](/uwp/api/windows.foundation.collections.iobservablevector_t_) interface. Since, in this sample, the collection is not *observable* (it doesn't need to be, because it doesn't add nor remove elements after initialization), we could instead have opted to call [winrt::single_threaded_vector](/uwp/cpp-ref-for-winrt/single-threaded-vector). That function returns the collection as an [**IVector**](/uwp/api/windows.foundation.collections.ivector_t_) interface.

For more info about collections, and binding to them, see [XAML items controls; bind to a C++/WinRT collection](./binding-collection.md), and [Collections with C++/WinRT](./collections.md).

The initialization code you just added references types that aren't yet in the project (for example, **winrt::SDKTemplate::CopyText**. To remedy that, let's go ahead and add five new blank XAML pages to the project.

#### Add five new blank XAML pages

Add a new **Visual C++** > **Blank Page (C++/WinRT)** item to the project (be certain that it's the **Blank Page (C++/WinRT)** item template, and not the **Blank Page** one). Name it `CopyText`. The new XAML page is defined within the **SDKTemplate** namespace, which is what we want.

Repeat the above process another four times, and named the XAML pages `CopyImage`, `CopyFiles`, `HistoryAndRoaming`, and `OtherScenarios`.

You'll now be able to build again, if you wish.

#### **NotifyUser**

In the C# project, you'll find the implementation of the **MainPage.NotifyUser** method in `MainPage.xaml.cs`. **MainPage.NotifyUser** has a dependency on **MainPage.UpdateStatus**, and that method in turn has dependencies on XAML elements that we haven't yet ported. So for now we'll just stub out an **UpdateStatus** method in the C++/WinRT project, and we'll port that later.

Here's the relevant C# code that we need to port.

```csharp
// MainPage.xaml.cs
...
public void NotifyUser(string strMessage, NotifyType type)
if (Dispatcher.HasThreadAccess)
{
    UpdateStatus(strMessage, type);
}
else
{
    var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateStatus(strMessage, type));
}
private void UpdateStatus(string strMessage, NotifyType type) { ... }{
...
```

**NotifyUser** uses the [**Windows.UI.Core.CoreDispatcherPriority**](/uwp/api/windows.ui.core.coredispatcherpriority) enum. In C++/WinRT, whenever you want to use a type from a Windows namespaces, you need to include the corresponding C++/WinRT Windows namespace header file (for more info about that, see [Get started with C++/WinRT](./get-started.md)). In this case, as you'll see in the code listing below, the header is `winrt/Windows.UI.Core.h`, and we'll be including it in `pch.h`.

**UpdateStatus** is private. So we'll make that a private method on our **MainPage** implementation type. **UpdateStatus** isn't meant to be called on the runtime class, so we won't declare it in IDL.

After porting **MainPage.NotifyUser**, and stubbing out **MainPage.UpdateStatus**, this is what we have in the C++/WinRT project. After this code listing, we'll examine some of the details.

```cppwinrt
// pch.h
...
#include <winrt/Windows.UI.Core.h>
...

// MainPage.h
...
struct MainPage : MainPageT<MainPage>
{
...
    void NotifyUser(hstring const& strMessage, SDKTemplate::NotifyType const& type);
...
private:
    void UpdateStatus(hstring const& strMessage, SDKTemplate::NotifyType const& type);
...
};

// MainPage.cpp
...
void MainPage::NotifyUser(hstring const& strMessage, SDKTemplate::NotifyType const& type)
{
    if (Dispatcher().HasThreadAccess())
    {
        UpdateStatus(strMessage, type);
    }
    else
    {
        Dispatcher().RunAsync(Windows::UI::Core::CoreDispatcherPriority::Normal, [strMessage, type, this]()
            {
                UpdateStatus(strMessage, type);
            });
    }
}
void MainPage::UpdateStatus(hstring const& strMessage, SDKTemplate::NotifyType const& type)
{
    throw hresult_not_implemented();
}
...
```

In C#, you can use dot notation to *dot into* nested properties. So, the C# **MainPage** type can access its own **Dispatcher** property with the syntax `Dispatcher`. And C# can further *dot into* that value with syntax such as `Dispatcher.HasThreadAccess`. In C++/WinRT, properties are implemented as accessor functions, so the syntax differs only in that you add parentheses for each function call.

|C#|C++/WinRT|
|-|-|
|`Dispatcher.HasThreadAccess`|`Dispatcher().HasThreadAccess()`|

When the C# version of **NotifyUser** calls [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync), it implements the asynchronous callback delegate as a lambda function. The C++/WinRT version does the same thing, but the syntax is a little different. In C++/WinRT, we *capture* the two parameters that we're going to use, as well as the `this` pointer (since we're going to call a member function). There's more info about implementing delegates as lambdas, and code examples, in the topic [Handle events by using delegates in C++/WinRT](./handle-events.md). Also, we can disregard the `var task =` part in this particular case. We're not waiting on the returned asynchronous object, so there's no need to store it. 

### Implement the remaining **MainPage** members

Let's make a full list of the members of **MainPage** (implemented across `MainPage.xaml.cs` and `SampleConfiguration.cs`) so that we can see which ones we've ported so far, and which ones are yet to do.

|Member|Access|Status|
|-|-|-|
|**MainPage** constructor|`public`|Ported|
|**Current** property|`public`|Ported|
|**FEATURE_NAME** property|`public`|Ported|
|**IsClipboardContentChangedEnabled** property|`public`|Not started|
|**Scenarios** property|`public`|Ported|
|**BuildClipboardFormatsOutputString** method|`public`|Not started|
|**DisplayToast** method|`public`|Not started|
|**EnableClipboardContentChangedNotifications** method|`public`|Not started|
|**NotifyUser** method|`public`|Ported|
|**OnNavigatedTo** method|`protected`|Not started|
|**isApplicationWindowActive** field|`private`|Not started|
|**needToPrintClipboardFormat** field|`private`|Not started|
|**scenarios** field|`private`|Ported|
|**Button_Click** method|`private`|Not started|
|**DisplayChangedFormats** method|`private`|Not started|
|**Footer_Click** method|`private`|Not started|
|**HandleClipboardChanged** method|`private`|Not started|
|**OnClipboardChanged** method|`private`|Not started|
|**OnWindowActivated** method|`private`|Not started|
|**ScenarioControl_SelectionChanged** method|`private`|Not started|
|**UpdateStatus** method|`private`|Stubbed out|

We'll talk about the as-yet-unported members in the next few subsections, then.

> [!NOTE]
> From time to time, we'll come across references in the source code to UI elements in the XAML markup (in `MainPage.xaml`). As we come to these references, we'll temporarily work around them by adding simple placeholder elements to the XAML. That way, the project will continue to build after each subsection. The alternative is to resolve the references by copying the *entire* contents of `MainPage.xaml` from the C# project to the C++/WinRT project now. But if we do that then it'll be a long time before we can come to a pit stop and build again (thus potentially obscuring any typos or other errors that we make along the way).
>
> Once we're done porting the imperative code for the **MainPage** class, *then* we'll copy the contents of the XAML file and be confident that the project will still build.

#### **IsClipboardContentChangedEnabled**

This is a get-set C# property that defaults to `false`. It's a member of **MainPage**, and is defined in `SampleConfiguration.cs`.

For C++/WinRT, we'll need an accessor function, a mutator function, and a backing data member as a field. Since **IsClipboardContentChangedEnabled** represents the state of one of the scenarios in the sample, rather than the state of **MainPage** itself, we'll create the new members on a new utility type called **SampleState**. And we'll implement that in our `SampleConfiguration.cpp` source code file, and we'll make the members `static` (since we need only one instance across the application; and so that we can access them without needing a class instance).

To accompany our `SampleConfiguration.cpp` in the C++/WinRT project, add a new **Visual C++** > **Code** > **Header File (.h)** item with the name `SampleConfiguration.h`. Edit `SampleConfiguration.h` and `SampleConfiguration.cpp` to match the listings below.

```cppwinrt
// SampleConfiguration.h
#pragma once 
#include "pch.h"

namespace winrt::SDKTemplate
{
    struct SampleState
    {
        static bool IsClipboardContentChangedEnabled();
        static void IsClipboardContentChangedEnabled(bool checked);
    private:
        static bool isClipboardContentChangedEnabled;
    };
}

// SampleConfiguration.cpp
...
#include "SampleConfiguration.h"
...
bool SampleState::isClipboardContentChangedEnabled = false;
...
bool SampleState::IsClipboardContentChangedEnabled()
{
    return isClipboardContentChangedEnabled;
}
void SampleState::IsClipboardContentChangedEnabled(bool checked)
{
    if (isClipboardContentChangedEnabled != checked)
    {
        isClipboardContentChangedEnabled = checked;
    }
}
```

Again, a field with `static` storage (such as **SampleState::isClipboardContentChangedEnabled**) must be defined once in the application, and a `.cpp` file is a good place for that (`SampleConfiguration.cpp` in this case).

#### **BuildClipboardFormatsOutputString**

This method is a public member of **MainPage**, and it's defined in `SampleConfiguration.cs`.

```csharp
// SampleConfiguration.cs
...
public string BuildClipboardFormatsOutputString()
{
    DataPackageView clipboardContent = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
    StringBuilder output = new StringBuilder();

    if (clipboardContent != null && clipboardContent.AvailableFormats.Count > 0)
    {
        output.Append("Available formats in the clipboard:");
        foreach (var format in clipboardContent.AvailableFormats)
        {
            output.Append(Environment.NewLine + " * " + format);
        }
    }
    else
    {
        output.Append("The clipboard is empty");
    }
    return output.ToString();
}
...
```

In C++/WinRT, we'll make **BuildClipboardFormatsOutputString** a public static method of **SampleState**. We can make it `static` because it doesn't access any instance members.

To use the **Clipboard** and **DataPackageView** types in C++/WinRT, we'll need to include the C++/WinRT Windows namespace header file `winrt/Windows.ApplicationModel.DataTransfer.h`.

In C#, the **DataPackageView.AvailableFormats** property is an **IReadOnlyList**, so we can access the **Count** property of that. In C++/WinRT, the **DataPackageView::AvailableFormats** accessor function returns an **IVectorView**, which has a **Size** accessor function that we can call.

To port the use of the C# **System.Text.StringBuilder** type, we'll make use of the standard C++ type [**std::wostringstream**](/cpp/standard-library/sstream-typedefs#wostringstream). That type is an output stream for wide strings (and to use it we'll need to include the `sstream` header file). Instead of using an **Append** method like you do with a **StringBuilder**, you use the [insertion operator](/cpp/standard-library/using-insertion-operators-and-controlling-format) (`<<`) with an output stream such as **wostringstream**. For more info, see [iostream programming](/cpp/standard-library/iostream-programming), and [Formatting C++/WinRT strings](./strings.md#formatting-strings).

The C# code constructs a **StringBuilder** with the `new` keyword. In C#, objects are reference types by default, declared on the heap with `new`. In modern standard C++, objects are value types by default, declared on the stack (without using `new`). So we port `StringBuilder output = new StringBuilder();` to C++/WinRT as simply `std::wostringstream output;`.

The C# `var` keyword asks the compiler to infer a type. You port `var` to `auto` in C++/WinRT. But in C++/WinRT, there are cases where (in order to avoid copies) you want a *reference* to an inferred (or deduced) type, and you express an lvalue reference to a deduced type with `auto&`. There are also cases where you want a special kind of reference that binds correctly whether it's initialized with an *lvalue* or with an *rvalue*. And you express that with `auto&&`. That's the form that you see used in the `for` loop in the ported code below. For an introduction to *lvalues* and *rvalues*, see [Value categories, and references to them](./cpp-value-categories.md).

Edit `pch.h`, `SampleConfiguration.h`, and `SampleConfiguration.cpp` to match the listings below.

```cppwinrt
// pch.h
...
#include <sstream>
#include "winrt/Windows.ApplicationModel.DataTransfer.h"
...

// SampleConfiguration.h
...
struct SampleState
{
    static hstring BuildClipboardFormatsOutputString();
    ...
}
...

// SampleConfiguration.cpp
...
using namespace Windows::ApplicationModel::DataTransfer;
...
hstring SampleState::BuildClipboardFormatsOutputString()
{
    DataPackageView clipboardContent{ Clipboard::GetContent() };
    std::wostringstream output;

    if (clipboardContent && clipboardContent.AvailableFormats().Size() > 0)
    {
        output << L"Available formats in the clipboard:";
        for (auto&& format : clipboardContent.AvailableFormats())
        {
            output << std::endl << L" * " << std::wstring_view(format);
        }
    }
    else
    {
        output << L"The clipboard is empty";
    }

    return hstring{ output.str() };
}
```

> [!NOTE]
> The syntax in the line of code `DataPackageView clipboardContent{ Clipboard::GetContent() };` uses a feature of modern standard C++ called *uniform initialization*, with its characteristic use of curly brackets instead of an `=` sign. That syntax makes it clear that initialization, rather than assignment, is taking place. If you prefer the form of syntax that *looks* like assignment (but actually isn't), then you can replace the syntax above with the equivalent `DataPackageView clipboardContent = Clipboard::GetContent();`. It's a good idea to become comfortable with both ways of expressing initialization, though, because you're likely to see both used frequently in the code you encounter.

#### **DisplayToast**

**DisplayToast** is a public static method of the C# **MainPage** class, and you'll find it defined in `SampleConfiguration.cs`. In C++/WinRT, we'll make it a public static method of **SampleState**.

We've already encountered most of the details and techniques that are relevant to porting this method. One new item to note is that you port a C# verbatim string literal (`@`) to a standard C++ [raw string literal](/cpp/cpp/string-and-character-literals-cpp#raw-string-literals-c11) (`LR`).

Also, when you reference the [**ToastNotification**](/uwp/api/windows.ui.notifications.toastnotification) and [**XmlDocument**](/uwp/api/windows.data.xml.dom.xmldocument) types in C++/WinRT, you can either qualify them by namespace name, or you can edit `SampleConfiguration.cpp` and add `using namespace` directives such as the following example.

```cppwinrt
using namespace Windows::UI::Notifications;
```

You have the same choice when you reference the [**XmlDocument**](/uwp/api/windows.data.xml.dom.xmldocument) type, and whenever you reference any other Windows Runtime type.

Apart from those items, just follow the same guidance that you did previously to accomplish the following steps.

- Declare the method in `SampleConfiguration.h`, and define it in `SampleConfiguration.cpp`.
- Edit `pch.h` to include any necessary C++/WinRT Windows namespace header files.
- Construct C++/WinRT objects on the stack, not on the heap.
- Replace calls to property get accessors with function-call syntax (`()`).

A very common cause of compiler/linker errors is forgetting to include the C++/WinRT Windows namespace header files that you need. For more info about one possible error, see [C3779: Why is the compiler giving me a "consume_Something: function that returns 'auto' cannot be used before it is defined" error?](./faq.yml#why-is-the-compiler-giving-me-a--c3779--consume-something--function-that-returns--auto--cannot-be-used-before-it-is-defined--error-).

If you want to follow along with the walkthrough and port **DisplayToast** yourself, then you can compare your results to the code in the C++/WinRT version in the ZIP of the [Clipboard sample](/samples/microsoft/windows-universal-samples/clipboard/) source code that you downloaded.

#### **EnableClipboardContentChangedNotifications**

**EnableClipboardContentChangedNotifications** is a public static method of the C# **MainPage** class, and it's defined in `SampleConfiguration.cs`.

```csharp
// SampleConfiguration.cs
...
public bool EnableClipboardContentChangedNotifications(bool enable)
{
    if (IsClipboardContentChangedEnabled == enable)
    {
        return false;
    }

    IsClipboardContentChangedEnabled = enable;
    if (enable)
    {
        Clipboard.ContentChanged += OnClipboardChanged;
        Window.Current.Activated += OnWindowActivated;
    }
    else
    {
        Clipboard.ContentChanged -= OnClipboardChanged;
        Window.Current.Activated -= OnWindowActivated;
    }
    return true;
}
...
private void OnClipboardChanged(object sender, object e) { ... }
private void OnWindowActivated(object sender, WindowActivatedEventArgs e) { ... }
...
```

In C++/WinRT, we'll make it a public static method of **SampleState**.

In C#, you use the `+=` and `-=` operator syntax to register and revoke event-handling delegates. In C++/WinRT, you have several syntactic options to register/revoke a delegate, as described in [Handle events by using delegates in C++/WinRT](./handle-events.md). But the general form is that you register and revoke with calls to a pair of functions named for the event. To register, you pass your delegate to the registering function, and you retrieve a revocation token in return (a [**winrt::event_token**](/uwp/cpp-ref-for-winrt/event-token)). To revoke, you pass that token to the revocation function. In this case, the hander is static and (as you can see in the following code listing) the function call syntax is straightforward.

Similar tokens *are* actually used, behind the scenes, in C#. But the language makes that detail implicit. C++/WinRT makes it explicit.

The **object** type appears in the C# event handler signatures. In the C# language, **object** is an [alias](/dotnet/csharp/language-reference/builtin-types/reference-types) for the .NET [**System.Object**](/dotnet/api/system.object) type. The equivalent in C++/WinRT is [**winrt::Windows::Foundation::IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable). So, you'll see **IInspectable** in the C++/WinRT event handlers.

Edit `SampleConfiguration.h` and `SampleConfiguration.cpp` to match the listings below.

```cppwinrt
// SampleConfiguration.h
...
    static bool EnableClipboardContentChangedNotifications(bool enable);
    ...
private:
    ...
    static event_token clipboardContentChangedToken;
    static event_token activatedToken;
    static void OnClipboardChanged(Windows::Foundation::IInspectable const& sender, Windows::Foundation::IInspectable const& e);
    static void OnWindowActivated(Windows::Foundation::IInspectable const& sender, Windows::UI::Core::WindowActivatedEventArgs const& e);
...

// SampleConfiguration.cpp
...
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml;
...
event_token SampleState::clipboardContentChangedToken;
event_token SampleState::activatedToken;
...
bool SampleState::EnableClipboardContentChangedNotifications(bool enable)
{
    if (isClipboardContentChangedEnabled == enable)
    {
        return false;
    }

    IsClipboardContentChangedEnabled(enable);
    if (enable)
    {
        clipboardContentChangedToken = Clipboard::ContentChanged(OnClipboardChanged);
        activatedToken = Window::Current().Activated(OnWindowActivated);
    }
    else
    {
        Clipboard::ContentChanged(clipboardContentChangedToken);
        Window::Current().Activated(activatedToken);
    }
    return true;
}
void SampleState::OnClipboardChanged(IInspectable const&, IInspectable const&){}
void SampleState::OnWindowActivated(IInspectable const&, WindowActivatedEventArgs const& e){}
```

Leave the event-handling delegates themselves (**OnClipboardChanged** and **OnWindowActivated**) as stubs for now. They're already on our list of members to port, so we'll get to them in later subsections.

#### **OnNavigatedTo**

**OnNavigatedTo** is a protected method of the C# **MainPage** class, and it's defined in `MainPage.xaml.cs`. Here it is, together with XAML **ListBox** that it references.

```xaml
<!-- MainPage.xaml -->
...
<ListBox x:Name="ScenarioControl" ... />
...
```

```csharp
// MainPage.xaml.cs
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    // Populate the scenario list from the SampleConfiguration.cs file
    var itemCollection = new List<Scenario>();
    int i = 1;
    foreach (Scenario s in scenarios)
    {
        itemCollection.Add(new Scenario { Title = $"{i++}) {s.Title}", ClassType = s.ClassType });
    }
    ScenarioControl.ItemsSource = itemCollection;

    if (Window.Current.Bounds.Width < 640)
    {
        ScenarioControl.SelectedIndex = -1;
    }
    else
    {
        ScenarioControl.SelectedIndex = 0;
    }
}
```

It's an important and interesting method, because here's where our collection of **Scenario** objects is assigned to the UI. The C# code builds a [**System.Collections.Generic.List**](/dotnet/api/system.collections.generic.list-1) of **Scenario** objects, and assigns that to the [**ItemsSource**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource) property of a **ListBox** (which is an items control). And, in C#, we use [string interpolation](/dotnet/csharp/language-reference/tokens/interpolated) to build the title for each **Scenario** object (note the use of the `$` special character).

In C++/WinRT, we'll make **OnNavigatedTo** a public method of **MainPage**. And we'll add a stub **ListBox** element to the XAML so that a build will succeed. After the code listing, we'll examine some of the details.

```xaml
<!-- MainPage.xaml -->
...
<StackPanel ...>
    ...
    <ListBox x:Name="ScenarioControl" />
</StackPanel>
...
```

```cppwinrt
// MainPage.h
...
void OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs const& e);
...

// MainPage.cpp
...
using namespace winrt::Windows::UI::Xaml;
using namespace winrt::Windows::UI::Xaml::Navigation;
...
void MainPage::OnNavigatedTo(NavigationEventArgs const& /* e */)
{
    auto itemCollection = winrt::single_threaded_observable_vector<IInspectable>();
    int i = 1;
    for (auto s : MainPage::scenarios())
    {
        s.Title = winrt::to_hstring(i++) + L") " + s.Title;
        itemCollection.Append(winrt::box_value(s));
    }
    ScenarioControl().ItemsSource(itemCollection);

    if (Window::Current().Bounds().Width < 640)
    {
        ScenarioControl().SelectedIndex(-1);
    }
    else
    {
        ScenarioControl().SelectedIndex(0);
    }
}
...
```

Again, we're calling the [winrt::single_threaded_observable_vector](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector) function, but this time to create a collection of [**IInspectable**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable). That was part of the decision we made to perform the boxing of our **Scenario** objects on a just-in-time basis.

And, in place of C#'s use of [string interpolation](/dotnet/csharp/language-reference/tokens/interpolated) here, we use a combination of the [**to_hstring**](/uwp/cpp-ref-for-winrt/to-hstring) function and the [concatenation operator](/uwp/cpp-ref-for-winrt/hstring#operator-concatenation-operator) of **winrt::hstring**.

#### **isApplicationWindowActive**

In C#, **isApplicationWindowActive** is a simple private `bool` field belonging to the **MainPage** class, and it's defined in `SampleConfiguration.cs`. It defaults to `false`. In C++/WinRT, we'll make it a private static field of **SampleState** (for the reasons we've already described) in the `SampleConfiguration.h` and `SampleConfiguration.cpp` files, with the same default.

We've already seen how to declare, define, and initialize a static field. For a refresher, look back to what we did with the **isClipboardContentChangedEnabled** field, and do the same with **isApplicationWindowActive**.

#### **needToPrintClipboardFormat**

Same pattern as **isApplicationWindowActive** (see the heading immediately before this one).

#### **Button_Click**

**Button_Click** is a private (event-handling) method of the C# **MainPage** class, and it's defined in `MainPage.xaml.cs`. Here it is, together with the XAML **SplitView** that it references, and the **ToggleButton** that registers it.

```xaml
<!-- MainPage.xaml -->
...
<SplitView x:Name="Splitter" ... />
...
<ToggleButton Click="Button_Click" .../>
...
```

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
}
```

And here's the equivalent, ported to C++/WinRT. Note that in the C++/WinRT version, the event handler is `public` (as you can see, you declare it *before* the `private:`declarations). This is because an event handler that's registered in XAML markup, like this one is, needs to be `public` in C++/WinRT in order for the XAML markup to access it. On the other hand, if you register an event handler in imperative code (like we did in **MainPage::EnableClipboardContentChangedNotifications** earlier), then the event handler doesn't need to be `public`.

```xaml
<!-- MainPage.xaml -->
...
<StackPanel ...>
    ...
    <SplitView x:Name="Splitter" />
</StackPanel>
...
```

```cppwinrt
// MainPage.h
...
    void Button_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& e);
private:
...

// MainPage.cpp
void MainPage::Button_Click(Windows::Foundation::IInspectable const& /* sender */, Windows::UI::Xaml::RoutedEventArgs const& /* e */)
{
    Splitter().IsPaneOpen(!Splitter().IsPaneOpen());
}
```

#### **DisplayChangedFormats**

In C#, **DisplayChangedFormats** is a private method belonging to the **MainPage** class, and it's defined in `SampleConfiguration.cs`.

```csharp
private void DisplayChangedFormats()
{
    string output = "Clipboard content has changed!" + Environment.NewLine;
    output += BuildClipboardFormatsOutputString();
    NotifyUser(output, NotifyType.StatusMessage);
}
```

In C++/WinRT, we'll make it a private static field of **SampleState** (it doesn't access any instance members), in the `SampleConfiguration.h` and `SampleConfiguration.cpp` files. The C# code for this method doesn't use **System.Text.StringBuilder**; but it does enough string formatting that for the C++/WinRT version this is another good place to use **std::wostringstream**.

Instead of the static [**System.Environment.NewLine**](/dotnet/api/system.environment.newline) property, which is used in the C# code, we'll insert the standard C++ `std::endl` (a newline character) into the output stream.

```cppwinrt
// SampleConfiguration.h
...
private:
    static void DisplayChangedFormats();
...

// SampleConfiguration.cpp
void SampleState::DisplayChangedFormats()
{
    std::wostringstream output;
    output << L"Clipboard content has changed!" << std::endl;
    output << BuildClipboardFormatsOutputString().c_str();
    MainPage::Current().NotifyUser(output.str(), NotifyType::StatusMessage);
}
```

There is a small inefficiency in the design of the C++/WinRT version above. First, we create a **std::wostringstream**. But we also call the **BuildClipboardFormatsOutputString** method (which we ported earlier). That method creates its own **std::wostringstream**. And it turns its stream into a **winrt::hstring** and returns that. We call the [**hstring::c_str**](/uwp/cpp-ref-for-winrt/hstring#hstringc_str-function) function to turn that returned **hstring** back into a C-style string, and then we insert that into our stream. It would be more efficient to create just one **std::wostringstream**, and pass (a reference to) that around, so that methods can insert strings into it directly.

That's what we do in the C++/WinRT version of the [Clipboard sample](/samples/microsoft/windows-universal-samples/clipboard/) source code (in the ZIP that you downloaded). In that source code, there's a new private static method named **SampleState::AddClipboardFormatsOutputString**, which takes and operates on a reference to an output stream. And then the methods **SampleState::DisplayChangedFormats** and **SampleState::BuildClipboardFormatsOutputString** are refactored to call that new method. It's functionally equivalent to the code listings in this topic, but it's more efficient.

#### **Footer_Click**

**Footer_Click** is an asynchronous event handler belonging to the C# **MainPage** class, and it's defined in `MainPage.xaml.cs`. The code listing below is functionally equivalent to the method in the source code that you downloaded. But here I've unpacked it from one line to four, to make it easier to see what it's doing, and consequently how we should port it.

```csharp
async void Footer_Click(object sender, RoutedEventArgs e)
{
    var hyperlinkButton = (HyperlinkButton)sender;
    string tagUrl = hyperlinkButton.Tag.ToString();
    Uri uri = new Uri(tagUrl);
    await Windows.System.Launcher.LaunchUriAsync(uri);
}
```

While, technically, the method is asynchronous, it doesn't do anything after the `await`, so it doesn't need the `await` (nor the `async` keyword). It probably uses them in order to avoid the IntelliSense message in Visual Studio.

The equivalent C++/WinRT method will also be asynchronous (because it calls [**Launcher.LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync)). But it doesn't need to `co_await`, nor to return an asynchronous object. For info about `co_await` and asynchronous objects, see [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md).

Now let's talk about what the method is doing. Because this is an event handler for the **Click** event of a **HyperlinkButton**, the object named *sender* is actually a **HyperlinkButton**. So the type conversion is safe (we could alternatively have expressed this conversion as `sender as HyperlinkButton`). Next, we retrieve the value of the **Tag** property (if you look at the XAML markup in the C# project, you'll see that this is set to a string representing a web url). Although the **FrameworkElement.Tag** property (**HyperlinkButton** is a **FrameworkElement**) is of type **object**, in C# we can stringify that with [**Object.ToString**](/dotnet/api/system.object.tostring). From the resulting string, we construct a **Uri** object. And finally (with the help of the Shell) we launch a browser and navigate to the url.

Here's the method ported to C++/WinRT (again, expanded for clarity), after which is a description of the details.

```cppwinrt
// pch.h
...
#include "winrt/Windows.System.h"
...

// MainPage.h
...
    void Footer_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& e);
private:
...

// MainPage.cpp
...
using namespace winrt::Windows::Foundation;
using namespace winrt::Windows::UI::Xaml::Controls;
...
void MainPage::Footer_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const&)
{
    auto hyperlinkButton{ sender.as<HyperlinkButton>() };
    hstring tagUrl{ winrt::unbox_value<hstring>(hyperlinkButton.Tag()) };
    Uri uri{ tagUrl };
    Windows::System::Launcher::LaunchUriAsync(uri);
}
```

As always, we make the event handler `public`. We use the [**as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) function on the *sender* object to convert it to **HyperlinkButton**. In C++/WinRT, the **Tag** property is an [**IInspectable**](/windows/desktop/api/inspectable/nn-inspectable-iinspectable) (the equivalent of [**Object**](/dotnet/api/system.object)). But there's no **Tostring** on **IInspectable**. Instead, we have to unbox the **IInspectable** to a scalar value (a string, in this case). Again, for more info on boxing and unboxing, see [Boxing and unboxing values to IInspectable](./boxing.md).

The last two lines repeat porting patterns we've seen before, and they pretty much echo the C# version.

#### **HandleClipboardChanged**

There's nothing new involved in porting this method. You can compare the C# and C++/WinRT versions in the ZIP of the [Clipboard sample](/samples/microsoft/windows-universal-samples/clipboard/) source code that you downloaded.

#### **OnClipboardChanged** and **OnWindowActivated**

So far we have only empty stubs for these two event handlers. But porting them is straightforward, and it doesn't raise anything new to discuss.

#### **ScenarioControl_SelectionChanged**

This is another private event handler belonging to the C# **MainPage** class, and defined in `MainPage.xaml.cs`. In C++/WinRT, we'll make it public, and implement it in `MainPage.h` and `MainPage.cpp`.

For this method, we'll need **MainPage::navigating**, which is a private Boolean field, initialized to `false`. And you'll need a **Frame** in `MainPage.xaml`, named *ScenarioFrame*. But, apart from those details, porting this method reveals no new techniques.

If, instead of porting by hand, you're copying code from the C++/WinRT version in the ZIP of the [Clipboard sample](/samples/microsoft/windows-universal-samples/clipboard/) source code that you downloaded, then you'll see a **MainPage::NavigateTo** being used there. For now, just refactor the contents of **NavigateTo** into **ScenarioControl_SelectionChanged**.

#### **UpdateStatus**

We have only a stub so far for **MainPage.UpdateStatus**. Porting its implementation, again, covers largely old ground. One new point to note is that while in C# we can compare a **string** to **String.Empty**, In C++/WinRT we instead call the [**winrt::hstring::empty**](/uwp/cpp-ref-for-winrt/hstring#hstringempty-function) function. Another is that `nullptr` is the standard C++ equivalent of C#'s `null`.

You can perform the rest of the port with techniques we've already covered. Here's a list of the kinds of things you'll need to do before the ported version of this method will compile.

- To `MainPage.xaml`, add a **Border** named *StatusBorder*.
- To `MainPage.xaml`, add a **TextBlock** named *StatusBlock*.
- To `MainPage.xaml`, add a **StackPanel** named *StatusPanel*.
- To `pch.h`, add `#include "winrt/Windows.UI.Xaml.Media.h"`.
- To `pch.h`, add `#include "winrt/Windows.UI.Xaml.Automation.Peers.h"`.
- To `MainPage.cpp` add `using namespace winrt::Windows::UI::Xaml::Media;`.
- To `MainPage.cpp` add `using namespace winrt::Windows::UI::Xaml::Automation::Peers;`.

### Copy the XAML and styles necessary to finish up porting **MainPage**

For XAML, the ideal case is that you can use *the same* XAML markup across a C# and a C++/WinRT project. And the Clipboard sample is one of those cases.

In its `Styles.xaml` file, the Clipboard sample has a XAML **ResourceDictionary** of styles, which are applied to the buttons, menus, and other UI elements across the UI of the application. The `Styles.xaml` page is merged into `App.xaml`. And then there's the standard `MainPage.xaml` starting point for the UI, which we've already seen briefly. We can now re-use those three `.xaml` files, unchanged, in the C++/WinRT version of the project.

As with asset files, you can choose to reference the same, shared XAML files from multiple versions of your application. In this walkthrough, just for the sake of simplicity, we'll copy files into the C++/WinRT project and add them that way.

Navigate to the `\Clipboard_sample\SharedContent\xaml` folder, select and copy `App.xaml` and `MainPage.xaml`, and then paste those two files into the `\Clipboard\Clipboard` folder in your C++/WinRT project, choosing to replace files when prompted.

In the C++/WinRT project in Visual Studio, click **Show All Files** to toggle it on. Now add a new folder, immediately under the project node, and name it `Styles`. In File Explorer, navigate to the `\Clipboard_sample\SharedContent\xaml` folder, select and copy `Styles.xaml`, and paste it into the `\Clipboard\Clipboard\Styles` folder that you just created. Back in Solution Explorer in the C++/WinRT project, right-click the `Styles` folder > **Add** > **Existing item...** and navigate to `\Clipboard\Clipboard\Styles`. In the file picker, select `Styles` and click **Add**.

Add a new folder to the C++/WinRT project, immediately under the project node, and named `Styles`. Navigate to the `\Clipboard_sample\SharedContent\xaml` folder, select and copy `Styles.xaml`, and paste it into the `\Clipboard\Clipboard\Styles` folder in your C++/WinRT project. Right-click the `Styles` folder (in Solution Explorer in the C++/WinRT project) > **Add** > **Existing item...** and navigate to `\Clipboard\Clipboard\Styles`. In the file picker, select `Styles` and click **Add**.

Click **Show All Files** again to toggle it off.

We've now finished porting **MainPage**, and if you've been following along with the steps then your C++/WinRT project will now build and run.

## Consolidate your `.idl` files

In addition to the standard `MainPage.xaml` starting point for the UI, the Clipboard sample has five other scenario-specific XAML pages, together with their corresponding code-behind files. We'll be re-using the actual XAML markup of all of these pages, unchanged, in the C++/WinRT version of the project. And we'll look at how to port the code-behind in the next few major sections. But before that, let's talk about IDL.

There's value in consolidating the IDL for your runtime classes into a single IDL file. To learn about that value, see [Factoring runtime classes into Midl files (.idl)](./author-apis.md#factoring-runtime-classes-into-midl-files-idl). So next we'll consolidate the contents of `CopyFiles.idl`, `CopyImage.idl`, `CopyText.idl`, `HistoryAndRoaming.idl`, and `OtherScenarios.idl` by moving that IDL into a single file named `Project.idl` (and then deleting the original files).

While we're doing that, let's also remove the auto-generated dummy property (`Int32 MyProperty;`, and its implementation) from each of those five XAML page types.

First, add a new **Midl File (.idl)** item to the C++/WinRT project. Name it `Project.idl`. Replace the entire contents of `Project.idl` with the following code.

```idl
// Project.idl
namespace SDKTemplate
{
    [default_interface]
    runtimeclass CopyFiles : Windows.UI.Xaml.Controls.Page
    {
        CopyFiles();
    }

    [default_interface]
    runtimeclass CopyImage : Windows.UI.Xaml.Controls.Page
    {
        CopyImage();
    }

    [default_interface]
    runtimeclass CopyText : Windows.UI.Xaml.Controls.Page
    {
        CopyText();
    }

    [default_interface]
    runtimeclass HistoryAndRoaming : Windows.UI.Xaml.Controls.Page
    {
        HistoryAndRoaming();
    }

    [default_interface]
    runtimeclass OtherScenarios : Windows.UI.Xaml.Controls.Page
    {
        OtherScenarios();
    }
}
```

As you can see, that's just a copy of the contents of the individual `.idl` files, all inside one namespace, and with `MyProperty` removed from each runtime class.

In Solution Explorer in Visual Studio, multiple-select all of the original IDL files (`CopyFiles.idl`, `CopyImage.idl`, `CopyText.idl`, `HistoryAndRoaming.idl`, and `OtherScenarios.idl`) and **Edit** > **Remove** them (choose **Delete** in the dialog).

Finally&mdash;and to complete the removal of `MyProperty`&mdash;in the `.h` and `.cpp` files for each of those same five XAML page types, delete the declarations and definitions of the `int32_t MyProperty()` accessor and `void MyProperty(int32_t)` mutator functions.

Incidentally, it's always a good idea to have the name of your XAML files match the name of the class that they represent. For example, if you have `x:Class="MyNamespace.MyPage"` in a XAML markup file, then that file should be named `MyPage.xaml`. While this isn't a technical requirement, not having to juggle different names for the same artifact will make your project more understandable and maintainable, and easier to work with.

## **CopyFiles**

In the C# project, the **CopyFiles** XAML page type is implemented in the `CopyFiles.xaml` and `CopyFiles.xaml.cs` source code files. Let's take a look at each of the members of **CopyFiles** in turn.

### **rootPage**

This is a private field.

```csharp
// CopyFiles.xaml.cs
...
public sealed partial class CopyFiles : Page
{
    MainPage rootPage = MainPage.Current;
    ...
}
...
```

In C++/WinRT, we can define and initialize it like this.

```cppwinrt
// CopyFiles.h
...
struct CopyFiles : CopyFilesT<CopyFiles>
{
    ...
private:
    SDKTemplate::MainPage rootPage{ MainPage::Current() };
};
...
```

Again (just like with **MainPage::current**), **CopyFiles::rootPage** is declared as being of type **SDKTemplate::MainPage**, which is the projected type, and not the implementation type.

### **CopyFiles** (the constructor)

In the C++/WinRT project, the **CopyFiles** type already has a constructor containing the code we want (it just calls **InitializeComponent**).

### **CopyButton_Click**

The C# **CopyButton_Click** method is an event handler, and from the `async` keyword in its signature we can tell that the method does asynchronous work. In C++/WinRT, we implement an asynchronous method as a *coroutine*. For an introduction to concurrency in C++/WinRT, together with a description of what a *coroutine* is, see [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md).

It's common to want to schedule further work after a coroutine completes, and for such cases the coroutine would return some asynchronous object type that can be awaited, and that optionally reports progress. But those considerations typically don't apply to an event handler. So when you have an event handler that performs asynchronous operations, you can implement that as a coroutine that returns **winrt::fire_and_forget**. For more info, see [Fire and forget](./concurrency-2.md#fire-and-forget).

Although the idea of a fire-and-forget coroutine is that you don't care when it completes, work is still continuing (or is suspended, awaiting resumption) in the background. You can see from the C# implementation that **CopyButton_Click** depends on the `this` pointer (it accesses the instance data member `rootPage`). So we must be sure that the `this` pointer (a pointer to a **CopyFiles** object) outlives the **CopyButton_Click** coroutine. In a situation like this sample application, where the user navigates between UI pages, we can't directly control the lifetime of those pages. Should the **CopyFiles** page be destroyed (by navigating away from it) while **CopyButton_Click** is still in flight on a background thread, it won't be safe to access `rootPage`. To make the coroutine correct, it needs to obtain a strong reference to the `this` pointer, and keep that reference for the duration of the coroutine. For more info, see [Strong and weak references in C++/WinRT](./weak-references.md).

If you look in the C++/WinRT version of the sample, at **CopyFiles::CopyButton_Click**, you'll see that it's done with a simple declaration on the stack.

```cppwinrt
fire_and_forget CopyFiles::CopyButton_Click(IInspectable const&, RoutedEventArgs const&)
{
    auto lifetime{ get_strong() };
    ...
}
```

Let's look at the other aspects of the ported code that are noteworthy.

In the code, we instantiate a [**FileOpenPicker**](/uwp/api/windows.storage.pickers.fileopenpicker) object, and two lines later we access that object's [**FileTypeFilter**](/uwp/api/windows.storage.pickers.fileopenpicker.filetypefilter) property. The return type of that property implements an **IVector** of strings. And on that **IVector**, we call the [IVector\<T>.ReplaceAll(T[])](/uwp/api/windows.foundation.collections.ivector-1.replaceall) method. The interesting aspect is the value that we're passing to that method, where an array is expected. Here's the line of code.

```cppwinrt
filePicker.FileTypeFilter().ReplaceAll({ L"*" });
```

The value that we're passing (`{ L"*" }`) is a standard C++ *initializer list*. It contains a single object, in this case, but an initializer list can contain any number of comma-separated objects. The pieces of C++/WinRT that allow you the convenience of passing an initializer list to a method such as this are explained in [Standard initializer lists](./std-cpp-data-types.md#standard-initializer-lists).

We port the C# `await` keyword to `co_await` in C++/WinRT. Here's the example from the code.

```cppwinrt
auto storageItems{ co_await filePicker.PickMultipleFilesAsync() };
```

Next, consider this line of C# code.

```csharp
dataPackage.SetStorageItems(storageItems);
```

C# is able to implicitly convert the **IReadOnlyList\<StorageFile>** represented by *storageItems* into the **IEnumerable\<IStorageItem>** expected by [**DataPackage.SetStorageItems**](/uwp/api/windows.applicationmodel.datatransfer.datapackage.setstorageitems). But in C++/WinRT we need to explicitly convert from **IVectorView\<StorageFile>** to **IIterable\<IStorageItem>**. And so we have another example of the [**as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) function in action.

```cppwinrt
dataPackage.SetStorageItems(storageItems.as<IVectorView<IStorageItem>>());
```

Where we use the `null` keyword in C# (for example, `Clipboard.SetContentWithOptions(dataPackage, null)`), we use `nullptr` in C++/WinRT (for example, `Clipboard::SetContentWithOptions(dataPackage, nullptr)`).

### **PasteButton_Click**

This is another event handler in the form of a fire-and-forget coroutine. Let's look at the aspects of the ported code that are noteworthy.

In the C# version of the sample, we catch exceptions with `catch (Exception ex)`. In the ported C++/WinRT code, you'll see the expression `catch (winrt::hresult_error const& ex)`. For more info about [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) and how to work with it, see [Error handling with C++/WinRT](./error-handling.md).

An example of testing whether a C# object is `null` or not is `if (storageItems != null)`. In C++/WinRT, we can rely on a conversion operator to `bool`, which does the test against `nullptr` internally.

Here's a slightly simplified version of a fragment of code from the ported C++/WinRT version of the sample.

```cppwinrt
std::wostringstream output;
output << std::wstring_view(ApplicationData::Current().LocalFolder().Path());
```

Constructing a **std::wstring_view** from a **winrt::hstring** like that illustrates an alternative to calling the [**hstring::c_str**](/uwp/cpp-ref-for-winrt/hstring#hstringc_str-function) function (to turn the **winrt::hstring** into a C-style string). This alternative works thanks to **hstring**'s [conversion operator to **std::wstring_view**](/uwp/cpp-ref-for-winrt/hstring#hstringoperator-stdwstring_view).

Consider this fragment of C#.

```csharp
var file = storageItem as StorageFile;
if (file != null)
...
```

To port the C# `as` keyword to C++/WinRT, so far we've seen the [**as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknownas-function) function used a couple of times. That function throws an exception if the type conversion fails. But if we want the conversion to return `nullptr` if it fails (so that we can handle that condition in the code), then we instead use the [**try_as**](/uwp/cpp-ref-for-winrt/windows-foundation-iunknown#iunknowntry_as-function) function.

```cppwinrt
auto file{ storageItem.try_as<StorageFile>() };
if (file)
...
```

### Copy the XAML necessary to finish up porting **CopyFiles**

You can now select the entire contents of the `CopyFiles.xaml` file from the `shared` folder of the original sample source code download, and paste that into the `CopyFiles.xaml` file in the C++/WinRT project (replacing the existing contents of that file in the C++/WinRT project).

Finally, edit `CopyFiles.h` and `.cpp` and delete the dummy **ClickHandler** function, since we just overwrote the corresponding XAML markup.

We've now finished porting **CopyFiles**, and if you've been following along with the steps then your C++/WinRT project will now build and run, and the **CopyFiles** scenario will be functional.

## **CopyImage**

To port the **CopyImage** XAML page type, you follow the same process as for **CopyFiles**. While porting **CopyImage**, you'll encounter the use of the C# [*using statement*](/dotnet/csharp/language-reference/keywords/using-statement), which ensures that objects that implement the [**IDisposable**](/dotnet/api/system.idisposable) interface are disposed correctly.

```csharp
if (imageReceived != null)
{
    using (var imageStream = await imageReceived.OpenReadAsync())
    {
        ... // Pass imageStream to other APIs, and do other work.
    }
}
```

The equivalent interface in C++/WinRT is [**IClosable**](/uwp/api/windows.foundation.iclosable), with its single **Close** method. Here's the C++/WinRT equivalent of the C# code above.

```cppwinrt
if (imageReceived)
{
    auto imageStream{ co_await imageReceived.OpenReadAsync() };
    ... // Pass imageStream to other APIs, and do other work.
    imageStream.Close();
}
```

C++/WinRT objects implement **IClosable** primarily for the benefit of languages that lack deterministic finalization. C++/WinRT has deterministic finalization, and so we often don't need to call **IClosable::Close** when we're writing C++/WinRT. But there are times when it's good to call it, and this is one of those times. Here, the *imageStream* identifier is a reference-counted wrapper around an underlying Windows Runtime object (in this case, an object that implements [**IRandomAccessStreamWithContentType**](/uwp/api/windows.storage.streams.irandomaccessstreamwithcontenttype)). Although we can determine that the finalizer of *imageStream* (its destructor) will run at the end of the enclosing scope (the curly brackets), we can't be certain that that finalizer will call **Close**. That's because we passed *imageStream* to other APIs, and they might still be contributing to the reference count of the underlying Windows Runtime object. So this is a case where it's a good idea to call **Close** explicitly. For more info, see [Do I need to call IClosable::Close on runtime classes that I consume?](./faq.yml#do-i-need-to-call-iclosable--close-on-runtime-classes-that-i-consume-).

Next, consider the C# expression `(uint)(imageDecoder.OrientedPixelWidth * 0.5)`, which you'll find in the **OnDeferredImageRequestedHandler** event handler. That expression multiplies a `uint` by a `double`, resulting in a `double`. It then casts that to a `uint`. In C++/WinRT, we *could* use a similar-looking C-style cast (`(uint32_t)(imageDecoder.OrientedPixelWidth() * 0.5)`), but it's preferable to make it clear exactly what kind of cast we intend, and in this case we would do that with `static_cast<uint32_t>(imageDecoder.OrientedPixelWidth() * 0.5)`.

The C# version of **CopyImage.OnDeferredImageRequestedHandler** has a `finally` clause, but not a `catch` clause. We went just a little bit further in the C++/WinRT version, and implemented a `catch` clause so that we can report whether or not the delayed rendering was successful.

Porting the remainder of this XAML page doesn't yield anything new to discuss. Remember to delete the dummy **ClickHandler** function. And, just like with **CopyFiles**, the last step in the port is to select the entire contents of `CopyImage.xaml`, and paste it into the same file in the C++/WinRT project.

## **CopyText**

You can port `CopyText.xaml` and `CopyText.xaml.cs` using techniques we've already covered.

## **HistoryAndRoaming**

There are some points of interest that arise while porting the **HistoryAndRoaming** XAML page type.

First, take a look at the C# source code, and follow the flow of control from **OnNavigatedTo** through the **OnHistoryEnabledChanged** event handler, and finally to the asynchronous function **CheckHistoryAndRoaming** (which is not awaited, so it's essentially fire and forget). Because **CheckHistoryAndRoaming** is asynchronous, we'll need to be careful in C++/WinRT about the lifetime of the `this` pointer. You can see the outcome if you look at the implementation in the `HistoryAndRoaming.cpp` source code file. First, when we attach delegates to the **Clipboard::HistoryEnabledChanged** and **Clipboard::RoamingEnabledChanged** events, we take only a weak reference to the **HistoryAndRoaming** page object. We do that by creating the delegate with a dependency on the value returned from [**winrt::get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsget_weak-function), instead of a dependency on the `this` pointer. Which means that the delegate itself, which eventually calls into asynchronous code, doesn't keep the **HistoryAndRoaming** page alive, should we navigate away from it.

And second, when we do finally reach our fire-and-forget **CheckHistoryAndRoaming** coroutine, the first thing we do is to take a strong reference to `this` to guarantee that the **HistoryAndRoaming** page lives at least until the coroutine finally completes. For more info about both of the aspects just described, see [Strong and weak references in C++/WinRT](./weak-references.md).

We find another point of interest while porting **CheckHistoryAndRoaming**. It contains code to update the UI; so we need to be certain that we're doing that on the main UI thread. The thread that initially calls into an event handler is the main UI thread. But typically, an asynchronous method can execute and/or resume on any arbitrary thread. In C#, the solution is to call [**CoreDispatcher.RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync), and update the UI from within the lambda function. In C++/WinRT, we can use the [**winrt::resume_foreground**](/uwp/cpp-ref-for-winrt/resume-foreground) function together with the `this` pointer's [**Dispatcher**](/uwp/api/windows.ui.xaml.dependencyobject.dispatcher) to suspend the coroutine and immediately resume on the main UI thread.

The relevant expression is `co_await winrt::resume_foreground(Dispatcher());`. Alternatively, although with less clarity, we could express that simply as `co_await Dispatcher();`. The shorter version is achieved courtesy of a conversion operator supplied by C++/WinRT.

Porting the remainder of this XAML page doesn't yield anything new to discuss. Remember to delete the dummy **ClickHandler** function, and to copy over the XAML markup.

## **OtherScenarios**

You can port `OtherScenarios.xaml` and `OtherScenarios.xaml.cs` using techniques we've already covered.

## Conclusion

Hopefully this walkthrough has armed you with sufficient porting info and techniques that you can now go ahead and port your own C# applications to C++/WinRT. By way of a refresher, you can continue to refer back to the *before* (C#) and *after* (C++/WinRT) versions of the source code in the [Clipboard sample](/samples/microsoft/windows-universal-samples/clipboard/), and compare them side by side to see the correspondence.

## Related topics

* [Move to C++/WinRT from C#](./move-to-winrt-from-csharp.md)
