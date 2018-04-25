---
author: stevewhims
description: This topic demonstrates how to author a Windows Runtime Component containing a runtime class that raises events. It also demonstrates an app that consumes the component and handles the events.
title: Author events in C++/WinRT
ms.author: stwhi
ms.date: 04/23/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, author, event
ms.localizationpriority: medium
---

# Author events in [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md)
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic demonstrates how to author a Windows Runtime Component containing a runtime class representing a bank account, which raises an event when its balance goes into debit. It also demonstrates a Core App that consumes the bank account runtime class, calls a function to adjust the balance, and handles any events that result.

> [!NOTE]
> For info about the current availability of the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt).

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Windows::Foundation::EventHandler&lt;T&gt; and TypedEventHandler&lt;T&gt;
If you want to raise an event from a runtime class implemented in a Windows Runtime Component, then you should use [**Windows::Foundation::EventHandler**](/uwp/api/windows.foundation.eventhandler) or [**TypedEventHandler**](/uwp/api/windows.foundation.eventhandler) for your event's delegate type. The type parameters must be Windows Runtime types, so primitive types are allowed as well as third-party runtime classes.

The compiler will help you with a "*must be WinRT type*" error if you forget this constraint.

## winrt::delegate&lt;...T&gt;
If you want to raise an event from a C++ type (authored and consumed within the same project), then you can use C++/WinRT's **winrt::delegate&lt;...T&gt;** for your event's delegate type. In that case, the type parameter needn't be a Windows Runtime type.

## Create a Windows Runtime Component (BankAccountWRC)
Begin by creating a new project in Microsoft Visual Studio. Create a **Visual C++ Windows Runtime Component (C++/WinRT)** project, and name it *BankAccountWRC* (for "bank account Windows Runtime Component").

The newly-created project contains a file named `Class.idl`. Rename that file `BankAccountWRC.idl` so that, when you build, your component's Windows Runtime metadata file is named for the component itself. In `BankAccountWRC.idl`, define your interface as in the listing below.

```idl
// BankAccountWRC.idl
namespace BankAccountWRC
{
	runtimeclass BankAccount
	{
		BankAccount();
		event Windows.Foundation.EventHandler<Single> AccountIsInDebit;
		void AdjustBalance(Single value);
	};
}
```

Save the file and build the project. The build won't succeed just yet. But, during the build process, the `midl.exe` tool is run to create your component's Windows Runtime metadata file (which is `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd`). Then, the `cppwinrt.exe` tool is run (with the `-component` option) to generate source code files to support you in authoring your component. These files include stubs to get you started implementing the `BankAccount` runtime class that you declared in your IDL. Those stubs are `\BankAccountWRC\BankAccountWRC\Generated Files\sources\BankAccount.h` and `BankAccount.cpp`.

Copy the stub files `BankAccount.h` and `BankAccount.cpp` from `\BankAccountWRC\BankAccountWRC\Generated Files\sources\` into the project folder, which is `\BankAccountWRC\BankAccountWRC\`. In **Solution Explorer**, make sure **Show All Files** is toggled on. Right-click the stub files that you copied, and click **Include In Project**. Also, right-click `Class.h` and `Class.cpp`, and click **Exclude From Project**.

Now, let's open `BankAccount.h` and `BankAccount.cpp` and implement our runtime class. In `BankAccount.h`, add two private members to the implementation (*not* the factory implementation) of BankAccount.

```cppwinrt
// BankAccount.h
...
namespace winrt::BankAccountWRC::implementation
{
    struct BankAccount : BankAccountT<BankAccount>
    {
        ...

	private:
		event<Windows::Foundation::EventHandler<float>> accountIsInDebitEvent;
		float balance{ 0.f };
	};
}
...
```

In `BankAccount.cpp`, implement the functions like this.

```cppwinrt
// BankAccount.cpp
...
namespace winrt::BankAccountWRC::implementation
{
	event_token BankAccount::AccountIsInDebit(Windows::Foundation::EventHandler<float> const& handler)
	{
		return accountIsInDebitEvent.add(handler);
	}

	void BankAccount::AccountIsInDebit(event_token const& token)
	{
		accountIsInDebitEvent.remove(token);
	}

	void BankAccount::AdjustBalance(float value)
	{
		balance += value;
		if (balance < 0.f) accountIsInDebitEvent(*this, balance);
	}
}
```

The implementation of the **AdjustBalance** function raises the **AccountIsInDebit** event if the balance goes negative.

If any warnings prevent you from building, then set the project property **C/C++** > **General** > **Treat Warnings As Errors** to **No (/WX-)**, and build the project again.

## Create a Core App (BankAccountCoreApp) to test the Windows Runtime Component
Now create a new project (either in your `BankAccountWRC` solution, or in a new one). Create a **Visual C++ Core App (C++/WinRT)** project, and name it *BankAccountCoreApp*.

Add a reference, and browse to `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd`. Click **Add**, and then **OK**. Now build BankAccountCoreApp. If you see an error that the payload file `readme.txt` doesn't exist, then exclude that file from the Windows Runtime Component project, rebuild it, then rebuild BankAccountCoreApp.

During the build process, the `cppwinrt.exe` tool is run to process the referenced `.winmd` file into source code files containing projected types to support you in consuming your component. The header for the projected types for your component's runtime classes&mdash;named `BankAccountWRC.h`&mdash;is generated into the folder `\BankAccountCoreApp\BankAccountCoreApp\Generated Files\winrt\`.

Include that header in `App.cpp`.

```cppwinrt
#include <winrt/BankAccountWRC.h>
```

Also in `App.cpp`, add the following code to instantiate a BankAccount (using the projected type's default constructor), register an event handler, and then cause the account to go into debit.

```cppwinrt
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
	BankAccountWRC::BankAccount bankAccount;
	event_token eventToken;
	...
	
	void Initialize(CoreApplicationView const &)
	{
		eventToken = bankAccount.AccountIsInDebit([](const auto &, float balance)
		{
			assert(balance < 0.f);
		});
	}
	...

	void Uninitialize()
	{
		bankAccount.AccountIsInDebit(eventToken);
	}
	...

	void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
	{
		bankAccount.AdjustBalance(-1.f);
		...
	}
	...
};
```

Each time you click the window, you subtract 1 from the bank account's balance. To demonstrate that the event is being raised as expected, put a breakpoint inside the lambda expression, run the app, and click inside the window.

## Related topics
* [Author APIs with C++/WinRT](author-apis.md)
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Handle events by using delegates in C++/WinRT](handle-events.md)
