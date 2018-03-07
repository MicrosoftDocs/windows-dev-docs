---
author: stevewhims
description: This topic demonstrates how to author a Windows Runtime Component containing a type that raises events. It also demonstrates an app that consumes the component and handles the events.
title: Events; how to author and handle
ms.author: stwhi
ms.date: 03/01/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, event, handle, handling
ms.localizationpriority: medium
---

# Events; how to author and handle
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

This topic demonstrates how to author a Windows Runtime Component containing a type representing a bank account, which raises an event when its balance goes into debit. It also demonstrates an app that consumes the bank account type, calls a function to adjust the balance, and handles any events that result.

To follow these steps, you'll need to download and install the C++/WinRT Visual Studio Extension (VSIX) from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/).

## Create a Windows Runtime Component (BankAccountWRC)
Begin by creating a new project in Microsoft Visual Studio. Create a **Visual C++ Windows Runtime Component (C++/WinRT)** project, and name it *BankAccountWRC* (for "bank account Windows Runtime Component").

The newly-created project contains a file named `Class.idl`. Rename that file `BankAccountWRC.idl` so that, when you build, your component's metadata file is named for the component itself. In `BankAccountWRC.idl`, define your interface as in the listing below.

```idl
import "Windows.Foundation.idl";

namespace BankAccountWRC
{
	[version(1.0), uuid(07D89D0A-8081-42F6-B899-DF9857037160)]
	interface IBankAccount : IInspectable
	{
		[eventadd] HRESULT AccountIsInDebitEvent([in] Windows.Foundation.EventHandler<float>* handler, [out][retval] EventRegistrationToken* cookie);
		[eventremove] HRESULT AccountIsInDebitSimpleEvent([in] EventRegistrationToken cookie);

		HRESULT AdjustBalance([in] float value);
	};

	[version(1.0), activatable(1.0)]
	runtimeclass BankAccount
	{
		[default] interface IBankAccount;
	};
}
```

Save the file and build the project. The build won't succeed just yet. But, during the build process, the `midl.exe` tool is run to create your component's metadata file (which is `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd`). Then, the `cppwinrt.exe` tool is run (with the `-component` option) to generate source code files to support you in authoring your component. These files include stubs to get you started implementing the `BankAccount` runtime type that you declared in your IDL. Those stubs are `\BankAccountWRC\BankAccountWRC\Generated Files\sources\BankAccount.h` and `BankAccount.cpp`.

Copy the stub files `BankAccount.h` and `BankAccount.cpp` from `\BankAccountWRC\BankAccountWRC\Generated Files\sources\` into the project folder, which is `\BankAccountWRC\BankAccountWRC\`. In **Solution Explorer**, make sure **Show All Files** is toggled on. Right-click the stub files that you copied, and click **Include In Project**. Also, right-click `Class.h` and `Class.cpp`, and click **Exclude From Project**.

Now, let's open `BankAccount.h` and `BankAccount.cpp` and implement our type. In `BankAccount.h`, add two private members to the implementation (*not* the factory implementation) of BankAccount.

```cppwinrt
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
```

In `BankAccount.cpp`, implement the functions like this.

```cppwinrt
namespace winrt::BankAccountWRC::implementation
{
	event_token BankAccount::AccountIsInDebitEvent(Windows::Foundation::EventHandler<float> const& handler)
	{
		return this->accountIsInDebitEvent.add(handler);
	}

	void BankAccount::AccountIsInDebitEvent(event_token const& cookie)
	{
		this->accountIsInDebitEvent.remove(cookie);
	}

	void BankAccount::AdjustBalance(float value)
	{
		this->balance += value;
		if (this->balance < 0.f) this->accountIsInDebitEvent(*this, this->balance);
	}
}
```
Build the project again.

## Create a Core App (BankAccountCoreApp) to test the Windows Runtime Component
Now create a new project, either in your `BankAccountWRC` solution or in a new one. Create a **Visual C++ Core App (C++/WinRT)** project, and name it *BankAccountCoreApp*.

Add a reference, and browse to `\BankAccountWRC\Debug\BankAccountWRC\BankAccountWRC.winmd`. Click **Add**, and then **OK**. Now build BankAccountCoreApp. If you see an error that the payload file `readme.txt` doesn't exist, then exclude that file from the Windows Runtime Component project, rebuild it, then rebuild BankAccountCoreApp.

During the build process, the `cppwinrt.exe` tool is run to process the referenced `.winmd` file into source code files to support you in consuming your component. One of these files is the header file for your component&mdash;named `BankAccountWRC.h`. It's generated into the folder `\BankAccountCoreApp\BankAccountCoreApp\Generated Files\winrt\`.

Include the header in `App.cpp`.

```cppwinrt
#include "winrt/BankAccountWRC.h"
```

Also in `App.cpp`, add the following code to instantiate a BankAccount, register an event handler, and then cause the account to go into debit. To demonstrate that the event is being raised, put a breakpoint inside the lambda expression, run the app, and click inside the window.

```cppwinrt
struct App : implements<App, IFrameworkViewSource, IFrameworkView>
{
	BankAccountWRC::BankAccount bankAccount;
	event_token eventToken;
	...
	
	void Initialize(CoreApplicationView const &)
	{
		this->eventToken = this->bankAccount.AccountIsInDebitEvent([](const auto &, float balance)
		{
			assert(balance < 0.f);
		});
	}
	...

	void Uninitialize()
	{
		this->bankAccount.AccountIsInDebitEvent(this->eventToken);
	}
	...

	void OnPointerPressed(IInspectable const &, PointerEventArgs const & args)
	{
		this->bankAccount.AdjustBalance(-1.f);
		...
	}
	...
};
```