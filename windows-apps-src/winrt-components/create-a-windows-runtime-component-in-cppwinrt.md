---
title: Windows Runtime components with C++/WinRT
description: This topic shows how to use [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) to create and consume a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language.
ms.date: 07/06/2020
ms.topic: article
keywords: windows 10, uwp, windows, runtime, component, components, Windows Runtime Component, WRC, C++/WinRT
ms.localizationpriority: medium
---

# Windows Runtime components with C++/WinRT

This topic shows how to use [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) to create and consume a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language.

There are several reasons for building a Windows Runtime component in C++/WinRT.
- To enjoy the performance advantage of C++ in complex or computationally intensive operations.
- To reuse standard C++ code that's already written and tested.
- To expose Win32 functionality to a Universal Windows Platform (UWP) app written in, for example, C#.

In general, when you author your C++/WinRT component, you can use types from the standard C++ library, and built-in types, except at the application binary interface (ABI) boundary where you're passing data to and from code in another `.winmd` package. At the ABI, use Windows Runtime types. In addition, in your C++/WinRT code, use types such as delegate and event to implement events that can be raised from your component and handled in another language. See [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) for more info about C++/WinRT.

The remainder of this topic walks you through how to author a Windows Runtime component in C++/WinRT, and then how to consume it from an application.

The Windows Runtime component that you'll build in this topic contains a runtime class representing a bank account. The topic also demonstrates a Core App that consumes the bank account runtime class, and calls a function to adjust the balance.

> [!NOTE]
> For info about installing and using the [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](../cpp-and-winrt-apis/consume-apis.md) and [Author APIs with C++/WinRT](../cpp-and-winrt-apis/author-apis.md).

## Create a Windows Runtime component (BankAccountWRC)

Begin by creating a new project in Microsoft Visual Studio. Create a **Windows Runtime Component (C++/WinRT)** project, and name it *BankAccountWRC* (for "bank account Windows Runtime component"). Make sure that **Place solution and project in the same directory** is unchecked. Target the latest generally-available (that is, not preview) version of the Windows SDK. Naming the project *BankAccountWRC* will give you the easiest experience with the rest of the steps in this topic. 

Don't build the project yet.

The newly-created project contains a file named `Class.idl`. In Solution Explorer, rename that file `BankAccount.idl` (renaming the `.idl` file automatically renames the dependent `.h` and `.cpp` files, too). Replace the contents of `BankAccount.idl` with the listing below.

> [!NOTE]
> Needless to say, you wouldn't implement production financial software this way; we use `Single` in this example solely for convenience.

```idl
// BankAccountWRC.idl
namespace BankAccountWRC
{
    runtimeclass BankAccount
    {
        BankAccount();
        void AdjustBalance(Single value);
    };
}
```

Save the file. The project won't build to completion at the moment, but building now is a useful thing to do because it generates the source code files in which you'll implement the **BankAccount** runtime class. So go ahead and build now (the build errors you can expect to see at this stage have to do with `Class.h` and `Class.g.h` not being found).

During the build process, the `midl.exe` tool is run to create your component's Windows Runtime metadata file (which is `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd`). Then, the `cppwinrt.exe` tool is run (with the `-component` option) to generate source code files to support you in authoring your component. These files include stubs to get you started implementing the **BankAccount** runtime class that you declared in your IDL. Those stubs are `\BankAccountWRC\BankAccountWRC\Generated Files\sources\BankAccount.h` and `BankAccount.cpp`.

Right-click the project node and click **Open Folder in File Explorer**. This opens the project folder in File Explorer. There, copy the stub files `BankAccount.h` and `BankAccount.cpp` from the folder `\BankAccountWRC\BankAccountWRC\Generated Files\sources\` and into the folder that contains your project files, which is `\BankAccountWRC\BankAccountWRC\`, and replace the files in the destination. Now, let's open `BankAccount.h` and `BankAccount.cpp` and implement our runtime class. In `BankAccount.h`, add a new private member to the implementation (*not* the factory implementation) of **BankAccount**.

```cppwinrt
// BankAccount.h
...
namespace winrt::BankAccountWRC::implementation
{
    struct BankAccount : BankAccountT<BankAccount>
    {
        ...

    private:
        float m_balance{ 0.f };
    };
}
...
```

In `BankAccount.cpp`, implement the **AdjustBalance** method as shown in the listing below.

```cppwinrt
// BankAccount.cpp
...
namespace winrt::BankAccountWRC::implementation
{
    void BankAccount::AdjustBalance(float value)
    {
        m_balance += value;
    }
}
```

You'll also need to delete the `static_assert` from both files.

If any warnings prevent you from building, then either resolve them or set the project property **C/C++** > **General** > **Treat Warnings As Errors** to **No (/WX-)**, and build the project again.

## Create a Core App (BankAccountCoreApp) to test the Windows Runtime component

Now create a new project (either in your *BankAccountWRC* solution, or in a new one). Create a **Core App (C++/WinRT)** project, and name it *BankAccountCoreApp*. Set *BankAccountCoreApp* as the startup project if the two projects are in the same solution.

> [!NOTE]
> As mentioned earlier, the Windows Runtime metadata file for your Windows Runtime component (whose project you named *BankAccountWRC*) is created in the folder `\BankAccountWRC\Debug\BankAccountWRC\`. The first segment of that path is the name of the folder that contains your solution file; the next segment is the subdirectory of that named `Debug`; and the last segment is the subdirectory of that named for your Windows Runtime component. If you didn't name your project *BankAccountWRC*, then your metadata file will be in the folder `\<YourProjectName>\Debug\<YourProjectName>\`.

Now, in your Core App project (*BankAccountCoreApp*), add a reference, and browse to `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd` (or add a project-to-project reference, if the two projects are in the same solution). Click **Add**, and then **OK**. Now build *BankAccountCoreApp*. In the unlikely event that you see an error that the payload file `readme.txt` doesn't exist, exclude that file from the Windows Runtime component project, rebuild it, then rebuild *BankAccountCoreApp*.

During the build process, the `cppwinrt.exe` tool is run to process the referenced `.winmd` file into source code files containing projected types to support you in consuming your component. The header for the projected types for your component's runtime classes&mdash;named `BankAccountWRC.h`&mdash;is generated into the folder `\BankAccountCoreApp\BankAccountCoreApp\Generated Files\winrt\`.

Include that header in `App.cpp`.

```cppwinrt
// App.cpp
...
#include <winrt/BankAccountWRC.h>
...
```

Also in `App.cpp`, add the following code to instantiate a **BankAccount** object (using the projected type's default constructor), and call a method on the bank account object.

```cppwinrt
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
    BankAccountWRC::BankAccount m_bankAccount;
    ...
    
    void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
    {
        m_bankAccount.AdjustBalance(1.f);
        ...
    }
    ...
};
```

Each time you click the window, you increment the bank account object's balance. You can set breakpoints if you want to step through the code to confirm that the application really is calling into the Windows Runtime component.

## Next steps

To add even more functionality, or new Windows Runtime types, to your C++/WinRT Windows Runtime component, you can follow the same patterns shown above. First, use IDL to define the functionality you want to expose. Then build the project in Visual Studio to generate a stub implementation. And then complete the implementation as appropriate. Any methods, properties, and events that you define in IDL are visible to the application that consumes your Windows Runtime Component. For more info about IDL, see [Introduction to Microsoft Interface Definition Language 3.0](/uwp/midl-3/intro).

For an example of how to add an event to your Windows Runtime Component, see [Author events in C++/WinRT](../cpp-and-winrt-apis/author-events.md).