---
author: stevewhims
description: A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it.
title: XAML controls; binding to a C++/WinRT property
ms.author: stwhi
ms.date: 03/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, control, binding, property
ms.localizationpriority: medium
---

# XAML controls; binding to a C++/WinRT property
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

A property that can be effectively bound to a XAML control is known as an *observable* property. This idea is based on the software design pattern known as the *observer pattern*. This topic shows how to implement observable properties in C++/WinRT, and how to bind XAML controls to them.

> [!NOTE]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md).

## What does *observable* mean for a property?
Let's say that a runtime class named **BookSku** has a property named **Title**. If **BookSku** chooses to raise the [**INotifyPropertyChanged::PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged) event whenever the value of **Title** changes, then **Title** is an observable property. It's the behavior of **BookSku** (raising or not raising the event) that determines which, if any, of its properties are observable.

A XAML text element, or control, can bind to, and handle, these events by retrieving the updated value(s) and then updating itself to show the new value.

To follow the steps below, you'll need to download and install the C++/WinRT Visual Studio Extension (VSIX) from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/).

## Create a Blank App (Bookstore)
Begin by creating a new project in Microsoft Visual Studio. Create a **Visual C++ Blank App (C++/WinRT)** project, and name it *Bookstore*.

We're going to author a new runtime class, and the first step in doing that is to add a new **Midl File (.idl)** item to the project. Name it `BookSku.idl`. Delete the default contents of `BookSku.idl`, and paste in this runtime class declaration.

```idl
// BookSku.idl
import "Windows.UI.Xaml.Data.idl";

namespace Bookstore
{
	runtimeclass BookSku : Windows.UI.Xaml.Data.INotifyPropertyChanged
	{
		String Title;
	}
}
```

Save the file and build the project. During the build process, the `midl.exe` tool is run to create a Windows Metadata file (`\Bookstore\Debug\Bookstore\Unmerged\BookSku.winmd`) describing the runtime class. Then, the `cppwinrt.exe` tool is run to generate source code files to support you in authoring and consuming your runtime class. These files include stubs to get you started implementing the **BookSku** runtime class that you declared in your IDL. Those stubs are `\Bookstore\Bookstore\Generated Files\sources\BookSku.h` and `BookSku.cpp`.

Copy the stub files `BookSku.h` and `BookSku.cpp` from `\Bookstore\Bookstore\Generated Files\sources\` into the project folder, which is `\Bookstore\Bookstore\`. In **Solution Explorer**, make sure **Show All Files** is toggled on. Right-click the stub files that you copied, and click **Include In Project**.

## Implement **BookSku**
Now, let's open `\Bookstore\Bookstore\BookSku.h` and `BookSku.cpp` and implement our runtime class. In `BookSku.h`, add a constructor that takes a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring), a private member to store the title string, and another for the event that we'll raise when the title changes. After adding those, your `BookSku.h` will look like this.

```cppwinrt
// BookSku.h
#pragma once

#include "BookSku.g.h"

namespace winrt::Bookstore::implementation
{
    struct BookSku : BookSkuT<BookSku>
    {
        BookSku() = delete;
		BookSku(hstring const& title);

        hstring Title();
        void Title(hstring const& value);
        event_token PropertyChanged(Windows::UI::Xaml::Data::PropertyChangedEventHandler const& value);
        void PropertyChanged(event_token const& token);
	
	private:
		hstring title;
		event<Windows::UI::Xaml::Data::PropertyChangedEventHandler> propertyChanged;
	};
}
```

In `BookSku.cpp`, implement the functions like this.

```cppwinrt
// BookSku.cpp
#include "pch.h"
#include "BookSku.h"

namespace winrt::Bookstore::implementation
{
	BookSku::BookSku(hstring const& title)
	{
		Title(title);
	}

    hstring BookSku::Title()
    {
		return title;
    }

    void BookSku::Title(hstring const& value)
    {
		if (title != value)
		{
			title = value;
			propertyChanged(*this, Windows::UI::Xaml::Data::PropertyChangedEventArgs{ L"Title" });
		}
    }

    event_token BookSku::PropertyChanged(Windows::UI::Xaml::Data::PropertyChangedEventHandler const& handler)
    {
		return propertyChanged.add(handler);
    }

    void BookSku::PropertyChanged(event_token const& token)
    {
		propertyChanged.remove(token);
    }
}
```

In the **Title** mutator function, we check whether a different value is being set and, if so, we update the title and also raise the [**INotifyPropertyChanged::PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged) event with an argument equal to the name of the property that has changed. This is so that the user-interface (UI) will know which property's value to re-query.

## Declare and implement **BookstoreViewModel**
Our main XAML page is going to bind to a main view model. And that view model is going to have several properties, including one of type **BookSku**. In this step, we'll declare and implement our main view model runtime class.

Add a new **Midl File (.idl)** item named `BookstoreViewModel.idl`.

```idl
// BookstoreViewModel.idl
import "Windows.Foundation.idl";
import "BookSku.idl";

namespace Bookstore
{
	runtimeclass BookstoreViewModel
	{
		BookSku BookSku{ get; };
	}
}
```

Save and build. Copy `BookstoreViewModel.h` and `BookstoreViewModel.cpp` from the `Generated Files` folder into the project folder, and include them in the project. Open those files and implement the runtime class like this.

```cppwinrt
// BookstoreViewModel.h
#pragma once

#include "BookstoreViewModel.g.h"
#include "BookSku.h"

namespace winrt::Bookstore::implementation
{
	struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel>
	{
		BookstoreViewModel();
		Bookstore::BookSku BookSku();

	private:
		Bookstore::BookSku m_bookSku{ nullptr };
	};
}
```

```cppwinrt
// BookstoreViewModel.cpp
#include "pch.h"
#include "BookstoreViewModel.h"

namespace winrt::Bookstore::implementation
{
	BookstoreViewModel::BookstoreViewModel()
	{
		m_bookSku = make<Bookstore::implementation::BookSku>(L"Atticus");
	}

	Bookstore::BookSku BookstoreViewModel::BookSku()
	{
		return m_bookSku;
	}
}
```

> [!NOTE]
> The type of `m_bookSku` is the projected type (**winrt::Bookstore::BookSku**), and the template parameter that you use with **make** is the implementation type (**winrt::Bookstore::implementation::BookSku**). Even so, **make** returns an instance of the projected type.

## Add a property of type **BookstoreViewModel** to **MainPage**
Open `MainPage.idl`, which declares the runtime class that represents our main UI page. Add an additional import statement to import `BookstoreViewModel.idl`, and add a read-only property named MainViewModel of type **BookstoreViewModel**.

```idl
// MainPage.idl
import "Windows.UI.Xaml.Controls.idl";
import "Windows.UI.Xaml.Markup.idl";
import "BookstoreViewModel.idl";

namespace BookstoreCPPWinRT
{
	runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
	{
		MainPage();
		BookstoreViewModel MainViewModel{ get; };
	}
}
```

Rebuild the project to regenerate the source code files in which the **MainPage** runtime class is implemented (`\Bookstore\Bookstore\Generated Files\sources\MainPage.h` and `MainPage.cpp`). Copy the accessor stubs for the ViewModel property out of the generated files and into `\Bookstore\Bookstore\MainPage.h` and `MainPage.cpp`.

To `\Bookstore\Bookstore\MainPage.h`, add a private member to store the view model. Note that the property accessor function (and the member m_mainViewModel) is implemented in terms of **Bookstore::BookstoreViewModel**, which is the projected type. The implementation type is in the same project (compilation unit), so we construct m_mainViewModel via the constructor overload that takes `nullptr`.

```cppwinrt
// MainPage.h
...
namespace winrt::Bookstore::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
        MainPage();

		Bookstore::BookstoreViewModel MainViewModel();

    private:
        void ClickHandler(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args);

        Bookstore::BookstoreViewModel m_mainViewModel{ nullptr };

        friend class MainPageT<MainPage>;
    };
}
...
```

In `\Bookstore\Bookstore\MainPage.cpp`, include `BookstoreViewModel.h`, which declares the implementation type. Call [**winrt::make**](/uwp/cpp-ref-for-winrt/make) (with the implementation type) to assign a new instance of the projected type to m_mainViewModel. Assign an initial value for the book's title. Implement the accessor for the MainViewModel property. And finally, update the book's title in the button's event handler.

```cppwinrt
// MainPage.cpp
#include "pch.h"
#include "MainPage.h"
#include "BookstoreViewModel.h"

using namespace winrt;
using namespace Windows::UI::Xaml;

namespace winrt::Bookstore::implementation
{
	MainPage::MainPage()
	{
		m_mainViewModel = make<Bookstore::implementation::BookstoreViewModel>();
		InitializeComponent();
	}

	Bookstore::BookstoreViewModel MainPage::MainViewModel()
	{
		return m_mainViewModel;
	}

	void MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
	{
		MainViewModel().BookSku().Title(L"To Kill a Mockingbird");
	}
}
```

## Bind the button to the **Title** property
Open `MainPage.xaml`, which contains the XAML markup for our main UI page. Remove the name from the button, and change its **Content** property value from a literal to a binding expression. Note the `Mode=OneWay` property on the binding expression (one-way from the view model to the UI). Without that property, the UI will not respond to property changed events.

```xaml
<Button Click="ClickHandler" Content="{x:Bind MainViewModel.BookSku.Title, Mode=OneWay}"/>
```

Now build and run the project. Click the button to execute the **Click** event handler. That handler calls the book's title mutator function; that mutator raises an event to let the UI know that the **Title** property has changed; and the button re-queries that property's value to update its own **Content** value.

## Important APIs
* [INotifyPropertyChanged::PropertyChanged](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged)
* [winrt::make](/uwp/cpp-ref-for-winrt/make)
