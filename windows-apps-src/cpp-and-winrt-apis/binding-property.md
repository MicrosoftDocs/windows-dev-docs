---
description: A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it.
title: XAML controls; bind to a C++/WinRT property
ms.date: 08/21/2018
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, control, binding, property
ms.localizationpriority: medium
---
# XAML controls; bind to a C++/WinRT property
A property that can be effectively bound to a XAML control is known as an *observable* property. This idea is based on the software design pattern known as the *observer pattern*. This topic shows how to implement observable properties in [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt), and how to bind XAML controls to them.

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## What does *observable* mean for a property?
Let's say that a runtime class named **BookSku** has a property named **Title**. If **BookSku** chooses to raise the [**INotifyPropertyChanged::PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged) event whenever the value of **Title** changes, then **Title** is an observable property. It's the behavior of **BookSku** (raising or not raising the event) that determines which, if any, of its properties are observable.

A XAML text element, or control, can bind to, and handle, these events by retrieving the updated value(s) and then updating itself to show the new value.

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

## Create a Blank App (Bookstore)
Begin by creating a new project in Microsoft Visual Studio. Create a **Visual C++** > **Windows Universal** > **Blank App (C++/WinRT)** project, and name it *Bookstore*.

We're going to author a new class to represent a book that has an observable title property. We're authoring and consuming the class within the same compilation unit. But we want to be able to bind to this class from XAML, and for that reason it's going to be a runtime class. And we're going to use C++/WinRT to both author and consume it.

The first step in authoring a new runtime class is to add a new **Midl File (.idl)** item to the project. Name it `BookSku.idl`. Delete the default contents of `BookSku.idl`, and paste in this runtime class declaration.

```idl
// BookSku.idl
namespace Bookstore
{
    runtimeclass BookSku : Windows.UI.Xaml.Data.INotifyPropertyChanged
    {
        String Title;
    }
}
```

> [!NOTE]
> Your view model classes&mdash;in fact, any runtime class that you declare in your application&mdash;need not derive from a base class. The **BookSku** class declared above is an example of that. It implements an interface, but it doesn't derive from any base class.
>
> Any runtime class that you declare in the application that *does* derive from a base class is known as a *composable* class. And there are constraints around composable classes. For an application to pass the [Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md) tests used by Visual Studio and by the Microsoft Store to validate submissions (and therefore for the application to be successfully ingested into the Microsoft Store), a composable class must ultimately derive from a Windows base class. Meaning that the class at the very root of the inheritance hierarchy must be a type originating in a Windows.* namespace. If you do need to derive a runtime class from a base class&mdash;for example, to implement a **BindableBase** class for all of your view models to derive from&mdash;then you can derive from [**Windows.UI.Xaml.DependencyObject**](/uwp/api/windows.ui.xaml.dependencyobject).
>
> A view model is an abstraction of a view, and so it's bound directly to the view (the XAML markup). A data model is an abstraction of data, and it's consumed only from your view models, and not bound directly to XAML. So, you can declare your data models not as runtime classes, but as C++ structs or classes. They don't need to be declared in MIDL, and you're free to use whatever inheritance hierarchy you like.

Save the file and build the project. During the build process, the `midl.exe` tool is run to create a Windows Runtime metadata file (`\Bookstore\Debug\Bookstore\Unmerged\BookSku.winmd`) describing the runtime class. Then, the `cppwinrt.exe` tool is run to generate source code files to support you in authoring and consuming your runtime class. These files include stubs to get you started implementing the **BookSku** runtime class that you declared in your IDL. Those stubs are `\Bookstore\Bookstore\Generated Files\sources\BookSku.h` and `BookSku.cpp`.

Copy the stub files `BookSku.h` and `BookSku.cpp` from `\Bookstore\Bookstore\Generated Files\sources\` into the project folder, which is `\Bookstore\Bookstore\`. In **Solution Explorer**, make sure **Show All Files** is toggled on. Right-click the stub files that you copied, and click **Include In Project**.

## Implement **BookSku**
Now, let's open `\Bookstore\Bookstore\BookSku.h` and `BookSku.cpp` and implement our runtime class. In `BookSku.h`, add a constructor that takes a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring), a private member to store the title string, and another for the event that we'll raise when the title changes. After making these changes, your `BookSku.h` will look like this.

```cppwinrt
// BookSku.h
#pragma once

#include "BookSku.g.h"

namespace winrt::Bookstore::implementation
{
    struct BookSku : BookSkuT<BookSku>
    {
        BookSku() = delete;
        BookSku(winrt::hstring const& title);

        winrt::hstring Title();
        void Title(winrt::hstring const& value);
        winrt::event_token PropertyChanged(Windows::UI::Xaml::Data::PropertyChangedEventHandler const& value);
        void PropertyChanged(winrt::event_token const& token);
    
    private:
        winrt::hstring m_title;
        winrt::event<Windows::UI::Xaml::Data::PropertyChangedEventHandler> m_propertyChanged;
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
    BookSku::BookSku(winrt::hstring const& title) : m_title{ title }
	{
	}

    winrt::hstring BookSku::Title()
    {
        return m_title;
    }

    void BookSku::Title(winrt::hstring const& value)
    {
        if (m_title != value)
        {
            m_title = value;
            m_propertyChanged(*this, Windows::UI::Xaml::Data::PropertyChangedEventArgs{ L"Title" });
        }
    }

    winrt::event_token BookSku::PropertyChanged(Windows::UI::Xaml::Data::PropertyChangedEventHandler const& handler)
    {
        return m_propertyChanged.add(handler);
    }

    void BookSku::PropertyChanged(winrt::event_token const& token)
    {
        m_propertyChanged.remove(token);
    }
}
```

In the **Title** mutator function, we check whether a value is being set that's different from the current value. And, if so, we update the title and also raise the [**INotifyPropertyChanged::PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged) event with an argument equal to the name of the property that has changed. This is so that the user-interface (UI) will know which property's value to re-query.

## Declare and implement **BookstoreViewModel**
Our main XAML page is going to bind to a main view model. And that view model is going to have several properties, including one of type **BookSku**. In this step, we'll declare and implement our main view model runtime class.

Add a new **Midl File (.idl)** item named `BookstoreViewModel.idl`.

```idl
// BookstoreViewModel.idl
import "BookSku.idl";

namespace Bookstore
{
    runtimeclass BookstoreViewModel
    {
        BookSku BookSku{ get; };
    }
}
```

Save and build. Copy `BookstoreViewModel.h` and `BookstoreViewModel.cpp` from the `Generated Files` folder into the project folder, and include them in the project. Open those files and implement the runtime class as shown below. Note how, in `BookstoreViewModel.h`, we're including `BookSku.h`, which declares the implementation type (**winrt::Bookstore::implementation::BookSku**). And we're restoring the default constructor by removing `= delete`.

```cppwinrt
// BookstoreViewModel.h
#pragma once

#include "BookstoreViewModel.g.h"
#include "BookSku.h"

namespace winrt::Bookstore::implementation
{
    struct BookstoreViewModel final : BookstoreViewModelT<BookstoreViewModel>
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
        m_bookSku = winrt::make<Bookstore::implementation::BookSku>(L"Atticus");
    }

    Bookstore::BookSku BookstoreViewModel::BookSku()
    {
        return m_bookSku;
    }
}
```

> [!NOTE]
> The type of `m_bookSku` is the projected type (**winrt::Bookstore::BookSku**), and the template parameter that you use with [**winrt::make**](/uwp/cpp-ref-for-winrt/make) is the implementation type (**winrt::Bookstore::implementation::BookSku**). Even so, **make** returns an instance of the projected type.

## Add a property of type **BookstoreViewModel** to **MainPage**
Open `MainPage.idl`, which declares the runtime class that represents our main UI page. Add an import statement to import `BookstoreViewModel.idl`, and add a read-only property named MainViewModel of type **BookstoreViewModel**. Also remove the **MyProperty** property. Also note the `import` directive in the listing below.

```idl
// MainPage.idl
import "BookstoreViewModel.idl";

namespace Bookstore
{
    runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
    {
        MainPage();
        BookstoreViewModel MainViewModel{ get; };
    }
}
```

Save the file. The project won't build to completion at the moment, but building now is a useful thing to do because it regenerates the source code files in which the **MainPage** runtime class is implemented (`\Bookstore\Bookstore\Generated Files\sources\MainPage.h` and `MainPage.cpp`). So go ahead and build now. The build error you can expect to see at this stage is **'MainViewModel': is not a member of 'winrt::Bookstore::implementation::MainPage'**.

If you omit the include of `BookstoreViewModel.idl` (see the listing of `MainPage.idl` above), then you'll see the error **expecting \< near "MainViewModel"**. Another tip is to make sure that you leave all types in the same namespace: the namespace that's shown in the code listings.

To resolve the error that we expect to see, you'll now need to copy the accessor stubs for the **MainViewModel** property out of the generated files (`\Bookstore\Bookstore\Generated Files\sources\MainPage.h` and `MainPage.cpp`) and into `\Bookstore\Bookstore\MainPage.h` and `MainPage.cpp`.

In `\Bookstore\Bookstore\MainPage.h`, include `BookstoreViewModel.h`, which declares the implementation type (**winrt::Bookstore::implementation::BookstoreViewModel**). Add a private member to store the view model. Note that the property accessor function (and the member m_mainViewModel) is implemented in terms of **Bookstore::BookstoreViewModel**, which is the projected type. The implementation type is in the same project (compilation unit) as the application, so we construct m_mainViewModel via the constructor overload that takes `nullptr_t`. Also remove the **MyProperty** property.

```cppwinrt
// MainPage.h
...
#include "BookstoreViewModel.h"
...
namespace winrt::Bookstore::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
        MainPage();

        Bookstore::BookstoreViewModel MainViewModel();

        void ClickHandler(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::RoutedEventArgs const&);

    private:
        Bookstore::BookstoreViewModel m_mainViewModel{ nullptr };
    };
}
...
```

In `\Bookstore\Bookstore\MainPage.cpp`, call [**winrt::make**](/uwp/cpp-ref-for-winrt/make) (with the implementation type) to assign a new instance of the projected type to m_mainViewModel. Assign an initial value for the book's title. Implement the accessor for the MainViewModel property. And finally, update the book's title in the button's event handler. Also remove the **MyProperty** property.

```cppwinrt
// MainPage.cpp
#include "pch.h"
#include "MainPage.h"

using namespace winrt;
using namespace Windows::UI::Xaml;

namespace winrt::Bookstore::implementation
{
    MainPage::MainPage()
    {
        m_mainViewModel = winrt::make<Bookstore::implementation::BookstoreViewModel>();
        InitializeComponent();
    }

    void MainPage::ClickHandler(Windows::Foundation::IInspectable const& /* sender */, Windows::UI::Xaml::RoutedEventArgs const& /* args */)
    {
        MainViewModel().BookSku().Title(L"To Kill a Mockingbird");
    }

    Bookstore::BookstoreViewModel MainPage::MainViewModel()
    {
        return m_mainViewModel;
    }
}
```

## Bind the button to the **Title** property
Open `MainPage.xaml`, which contains the XAML markup for our main UI page. As shown in the listing below, remove the name from the button, and change its **Content** property value from a literal to a binding expression. Note the `Mode=OneWay` property on the binding expression (one-way from the view model to the UI). Without that property, the UI will not respond to property changed events.

```xaml
<Button Click="ClickHandler" Content="{x:Bind MainViewModel.BookSku.Title, Mode=OneWay}"/>
```

Now build and run the project. Click the button to execute the **Click** event handler. That handler calls the book's title mutator function; that mutator raises an event to let the UI know that the **Title** property has changed; and the button re-queries that property's value to update its own **Content** value.

## Using the {Binding} markup extension with C++/WinRT
For the currently released version of C++/WinRT, in order to be able to use the {Binding} markup extension you'll need to implement the [ICustomPropertyProvider](/uwp/api/windows.ui.xaml.data.icustompropertyprovider) and [ICustomProperty](/uwp/api/windows.ui.xaml.data.icustomproperty) interfaces.

## Important APIs
* [INotifyPropertyChanged::PropertyChanged](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged)
* [winrt::make function template](/uwp/cpp-ref-for-winrt/make)

## Related topics
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Author APIs with C++/WinRT](author-apis.md)
