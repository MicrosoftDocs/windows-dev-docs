---
description: A property that can be effectively bound to a XAML control is known as an *observable* property. This topic shows how to implement and consume an observable property, and how to bind a XAML control to it.
title: XAML controls; bind to a C++/WinRT property
ms.date: 09/25/2020
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, XAML, control, binding, property
ms.localizationpriority: medium
---

# XAML controls; bind to a C++/WinRT property

A property that can be effectively bound to a XAML control is known as an *observable* property. This idea is based on the software design pattern known as the *observer pattern*. This topic shows how to implement observable properties in [C++/WinRT](./intro-to-using-cpp-with-winrt.md), and how to bind XAML controls to them (for background info, see [Data binding](../data-binding/index.md)).

> [!IMPORTANT]
> For essential concepts and terms that support your understanding of how to consume and author runtime classes with C++/WinRT, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## What does *observable* mean for a property?

Let's say that a runtime class named **BookSku** has a property named **Title**. If **BookSku** raises the [**INotifyPropertyChanged::PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged) event whenever the value of **Title** changes, then that means that **Title** is an observable property. It's the behavior of **BookSku** (raising or not raising the event) that determines which, if any, of its properties are observable.

A XAML text element, or control, can bind to, and handle, these events. Such an element or control handles the event by retrieving the updated value(s), and then updating itself to show the new value.

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Create a Blank App (Bookstore)

Begin by creating a new project in Microsoft Visual Studio. Create a **Blank App (C++/WinRT)** project, and name it *Bookstore*. Make sure that **Place solution and project in the same directory** is unchecked. Target the latest generally-available (that is, not preview) version of the Windows SDK.

We're going to author a new class to represent a book that has an observable title property. We're authoring and consuming the class within the same compilation unit. But we want to be able to bind to this class from XAML, and for that reason it's going to be a runtime class. And we're going to use C++/WinRT to both author and consume it.

The first step in authoring a new runtime class is to add a new **Midl File (.idl)** item to the project. Name the new item `BookSku.idl`. Delete the default contents of `BookSku.idl`, and paste in this runtime class declaration.

```idl
// BookSku.idl
namespace Bookstore
{
    runtimeclass BookSku : Windows.UI.Xaml.Data.INotifyPropertyChanged
    {
        BookSku(String title);
        String Title;
    }
}
```

> [!NOTE]
> Your view model classes&mdash;in fact, any runtime class that you declare in your application&mdash;need not derive from a base class. The **BookSku** class declared above is an example of that. It implements an interface, but it doesn't derive from any base class.
>
> Any runtime class that you declare in the application that *does* derive from a base class is known as a *composable* class. And there are constraints around composable classes. For an application to pass the [Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md) tests used by Visual Studio and by the Microsoft Store to validate submissions (and therefore for the application to be successfully ingested into the Microsoft Store), a composable class must ultimately derive from a Windows base class. Meaning that the class at the very root of the inheritance hierarchy must be a type originating in a Windows.* namespace. If you do need to derive a runtime class from a base class&mdash;for example, to implement a **BindableBase** class for all of your view models to derive from&mdash;then you can derive from [**Windows.UI.Xaml.DependencyObject**](/uwp/api/windows.ui.xaml.dependencyobject).
>
> A view model is an abstraction of a view, and so it's bound directly to the view (the XAML markup). A data model is an abstraction of data, and it's consumed only from your view models, and not bound directly to XAML. So you can declare your data models not as runtime classes, but as C++ structs or classes. They don't need to be declared in MIDL, and you're free to use whatever inheritance hierarchy you like.

Save the file, and build the project. The build won't (entirely) succeed yet, but it will do some necessary things for us. Specifically, during the build process the `midl.exe` tool is run to create a Windows Runtime metadata file that describes the runtime class (the file is placed on disk at `\Bookstore\Debug\Bookstore\Unmerged\BookSku.winmd`). Then, the `cppwinrt.exe` tool is run to generate source code files to support you in authoring and consuming your runtime class. Those files include stubs to get you started implementing the **BookSku** runtime class that you declared in your IDL. We'll find them on disk in a moment, but those stubs are `\Bookstore\Bookstore\Generated Files\sources\BookSku.h` and `BookSku.cpp`.

So now right-click the project node in Visual Studio, and click **Open Folder in File Explorer**. That opens the project folder in File Explorer. You should now be looking at the contents of the `\Bookstore\Bookstore\` folder. From there, navigate into the `\Generated Files\sources\` folder, and copy the stub files `BookSku.h` and `BookSku.cpp` to the clipboard. Navigate back up to the project folder (`\Bookstore\Bookstore\`), and paste the two files you just copied. Lastly, in **Solution Explorer** with the project node selected, make sure **Show All Files** is toggled on. Right-click the stub files that you copied, and click **Include In Project**.

## Implement **BookSku**
Now let's open `\Bookstore\Bookstore\BookSku.h` and `BookSku.cpp` and implement our runtime class. First, you'll see a `static_assert` at the top of `BookSku.h` and `BookSku.cpp`, which you'll need to remove.

Next, in `BookSku.h`, make these changes.

- On the default constructor, change `= default` to `= delete`. That's because we don't want a default constructor.
- Add a private member to store the title string. Note that we have a constructor that takes a [**winrt::hstring**](/uwp/cpp-ref-for-winrt/hstring) value. That value is the title string.
- Add another private member for the event that we'll raise when the title changes.

After making these changes, your `BookSku.h` will look like this.

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
namespace winrt::Bookstore::factory_implementation
{
    struct BookSku : BookSkuT<BookSku, implementation::BookSku>
    {
    };
}
```

In `BookSku.cpp`, implement the functions like this.

```cppwinrt
// BookSku.cpp
#include "pch.h"
#include "BookSku.h"
#include "BookSku.g.cpp"

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

In the **Title** mutator function, we check whether a value is being set that's different from the current value. And, if so, then we update the title and also raise the [**INotifyPropertyChanged::PropertyChanged**](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged) event with an argument equal to the name of the property that has changed. This is so that the user-interface (UI) will know which property's value to re-query.

The project will build again now, if you want to check that.

## Declare and implement **BookstoreViewModel**
Our main XAML page is going to bind to a main view model. And that view model is going to have several properties, including one of type **BookSku**. In this step, we'll declare and implement our main view model runtime class.

Add a new **Midl File (.idl)** item named `BookstoreViewModel.idl`. But also see [Factoring runtime classes into Midl files (.idl)](./author-apis.md#factoring-runtime-classes-into-midl-files-idl).

```idl
// BookstoreViewModel.idl
import "BookSku.idl";

namespace Bookstore
{
    runtimeclass BookstoreViewModel
    {
        BookstoreViewModel();
        BookSku BookSku{ get; };
    }
}
```

Save and build (the build won't entirely succeed yet, but the reason we're building is to generate stub files again).

Copy `BookstoreViewModel.h` and `BookstoreViewModel.cpp` from the `Generated Files\sources` folder into the project folder, and include them in the project. Open those files (removing the `static_assert` again), and implement the runtime class as shown below. Note how, in `BookstoreViewModel.h`, we're including `BookSku.h`, which declares the implementation type for **BookSku** (which is **winrt::Bookstore::implementation::BookSku**). And we're removing `= default` from the default constructor.

> [!NOTE]
> In the listings below for `BookstoreViewModel.h` and `BookstoreViewModel.cpp`, the code illustrates the default way of constructing the *m_bookSku* data member. That's the method that dates back to the first release of C++/WinRT, and it's a good idea to be at least familiar with the pattern. With C++/WinRT version 2.0 and later, there's an optimized form of construction available to you known as *uniform construction* (see [News, and changes, in C++/WinRT 2.0](./news.md#news-and-changes-in-cwinrt-20)). Later in this topic, we'll show an example of uniform construction.

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
namespace winrt::Bookstore::factory_implementation
{
    struct BookstoreViewModel : BookstoreViewModelT<BookstoreViewModel, implementation::BookstoreViewModel>
    {
    };
}
```

```cppwinrt
// BookstoreViewModel.cpp
#include "pch.h"
#include "BookstoreViewModel.h"
#include "BookstoreViewModel.g.cpp"

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

The project will build again now.

## Add a property of type **BookstoreViewModel** to **MainPage**
Open `MainPage.idl`, which declares the runtime class that represents our main UI page.

- Add an `import` directive to import `BookstoreViewModel.idl`.
- Add a read-only property named **MainViewModel**, of type **BookstoreViewModel**.
- Remove the **MyProperty** property.

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

Save the file. The project won't entirely succeed in building yet, but building now is a useful thing to do because it regenerates the source code files in which the **MainPage** runtime class is implemented (`\Bookstore\Bookstore\Generated Files\sources\MainPage.h` and `MainPage.cpp`). So go ahead and build now. The build error you can expect to see at this stage is **'MainViewModel': is not a member of 'winrt::Bookstore::implementation::MainPage'**.

If you omit the include of `BookstoreViewModel.idl` (see the listing of `MainPage.idl` above), then you'll see the error **expecting \< near "MainViewModel"**. Another tip is to make sure that you leave all types in the same namespace&mdash;the namespace that's shown in the code listings.

To resolve the error that we expect to see, you'll now need to copy the accessor stubs for the **MainViewModel** property out of the generated files (`\Bookstore\Bookstore\Generated Files\sources\MainPage.h` and `MainPage.cpp`) and into `\Bookstore\Bookstore\MainPage.h` and `MainPage.cpp`. The steps to do that are described next.

In `\Bookstore\Bookstore\MainPage.h`, perform these steps.

- Include `BookstoreViewModel.h`, which declares the implementation type for **BookstoreViewModel** (which is **winrt::Bookstore::implementation::BookstoreViewModel**).
- Add a private member to store the view model. Note that the property accessor function (and the member *m_mainViewModel*) is implemented in terms of the projected type for **BookstoreViewModel** (which is **Bookstore::BookstoreViewModel**).
- The implementation type is in the same project (compilation unit) as the application, so we construct *m_mainViewModel* via the constructor overload that takes **std::nullptr_t**.
- Remove the **MyProperty** property.

> [!NOTE]
> In the pair of listings below for `MainPage.h` and `MainPage.cpp`, the code illustrates the default way of constructing the *m_mainViewModel* data member. In the section that follows, we'll show a version that uses uniform construction instead.

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

In `\Bookstore\Bookstore\MainPage.cpp`, as shown in the listing below, make the following changes.

- Call [**winrt::make**](/uwp/cpp-ref-for-winrt/make) (with the **BookstoreViewModel** implementation type) to assign a new instance of the projected **BookstoreViewModel** type to *m_mainViewModel*. As we saw above, the **BookstoreViewModel** constructor creates a new **BookSku** object as a private data member, setting its title initially to `L"Atticus"`.
- In the button's event handler (**ClickHandler**), update the book's title to its published title.
- Implement the accessor for the **MainViewModel** property.
- Remove the **MyProperty** property.

```cppwinrt
// MainPage.cpp
#include "pch.h"
#include "MainPage.h"
#include "MainPage.g.cpp"

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

### Uniform construction
To use uniform construction instead of [**winrt::make**](/uwp/cpp-ref-for-winrt/make), in `MainPage.h` declare and initialize *m_mainViewModel* in just one step, as shown below.

```cppwinrt
// MainPage.h
...
#include "BookstoreViewModel.h"
...
struct MainPage : MainPageT<MainPage>
{
    ...
private:
    Bookstore::BookstoreViewModel m_mainViewModel;
};
...
```

And then, in the **MainPage** constructor in `MainPage.cpp`, there's no need for the code `m_mainViewModel = winrt::make<Bookstore::implementation::BookstoreViewModel>();`.

For more info about uniform construction, and code examples, see [Opt in to uniform construction, and direct implementation access](./author-apis.md#opt-in-to-uniform-construction-and-direct-implementation-access).

## Bind the button to the **Title** property
Open `MainPage.xaml`, which contains the XAML markup for our main UI page. As shown in the listing below, remove the name from the button, and change its **Content** property value from a literal to a binding expression. Note the `Mode=OneWay` property on the binding expression (one-way from the view model to the UI). Without that property, the UI will not respond to property changed events.

```xaml
<Button Click="ClickHandler" Content="{x:Bind MainViewModel.BookSku.Title, Mode=OneWay}"/>
```

Now build and run the project. Click the button to execute the **Click** event handler. That handler calls the book's title mutator function; that mutator raises an event to let the UI know that the **Title** property has changed; and the button re-queries that property's value to update its own **Content** value.

## Using the {Binding} markup extension with C++/WinRT
For the currently released version of C++/WinRT, in order to be able to use the {Binding} markup extension you'll need to implement the [ICustomPropertyProvider](/uwp/api/windows.ui.xaml.data.icustompropertyprovider) and [ICustomProperty](/uwp/api/windows.ui.xaml.data.icustomproperty) interfaces.

## Element-to-element binding

You can bind the property of one XAML element to the property of another XAML element. Here's an example of how that looks in markup.

```xaml
<TextBox x:Name="myTextBox" />
<TextBlock Text="{x:Bind myTextBox.Text, Mode=OneWay}" />
```

You'll need to declare the named XAML entity `myTextBox` as a read-only property in your Midl file (.idl).

```idl
// MainPage.idl
runtimeclass MainPage : Windows.UI.Xaml.Controls.Page
{
    MainPage();
    Windows.UI.Xaml.Controls.TextBox myTextBox{ get; };
}
```

Here's the reason for this necessity. All types that the XAML compiler needs to validate (including those used in [{x:Bind}](../xaml-platform/x-bind-markup-extension.md)) are read from Windows Metadata (WinMD). All you need to do is to add the read-only property to your Midl file. Don't implement it, because the autogenerated XAML code-behind provides the implementation for you.

## Consuming objects from XAML markup

All entities consumed by using the XAML [**{x:Bind} markup extension**](../xaml-platform/x-bind-markup-extension.md) must be exposed publicly in IDL. Furthermore, if XAML markup contains a reference to another element that's also in markup, then the getter for that markup must be present in IDL.

```xaml
<Page x:Name="MyPage">
    <StackPanel>
        <CheckBox x:Name="UseCustomColorCheckBox" Content="Use custom color"
             Click="UseCustomColorCheckBox_Click" />
        <Button x:Name="ChangeColorButton" Content="Change color"
            Click="{x:Bind ChangeColorButton_OnClick}"
            IsEnabled="{x:Bind UseCustomColorCheckBox.IsChecked.Value, Mode=OneWay}"/>
    </StackPanel>
</Page>
```

The *ChangeColorButton* element refers to the *UseCustomColorCheckBox* element via binding. So the IDL for this page must declare a read-only property named *UseCustomColorCheckBox* in order for it to be accessible to binding.

The click event handler delegate for *UseCustomColorCheckBox* uses classic XAML delegate syntax, so that doesn't need an entry in the IDL; it just needs to be public in your implementation class. On the other hand, *ChangeColorButton* also has an `{x:Bind}` click event handler, which must also go into the IDL.

```idl
runtimeclass MyPage : Windows.UI.Xaml.Controls.Page
{
    MyPage();

    // These members are consumed by binding.
    void ChangeColorButton_OnClick();
    Windows.UI.Xaml.Controls.CheckBox UseCustomColorCheckBox{ get; };
}
```

You don't need to provide an implementation for the **UseCustomColorCheckBox** property. The XAML code generator does that for you.

### Binding to Boolean

You might do this in a diagnostic mode:

```xaml
<TextBlock Text="{Binding CanPair}"/>
```

That displays `true` or `false` in C++/CX; but it displays ```Windows.Foundation.IReference`1<Boolean>``` in C++/WinRT.

Instead, use `x:Bind` when binding to a Boolean.

```xaml
<TextBlock Text="{x:Bind CanPair}"/>
```

## Using the Windows Implementation Libraries (WIL)

The [Windows Implementation Libraries (WIL)](https://github.com/Microsoft/wil) provides helpers to ease writing bindable properties. See [Notifying Properties](https://github.com/microsoft/wil/wiki/CppWinRT-authoring-helpers#notifying-properties-inotifypropertychanged) in the WIL documentation.

## Important APIs
* [INotifyPropertyChanged::PropertyChanged](/uwp/api/windows.ui.xaml.data.inotifypropertychanged.PropertyChanged)
* [winrt::make function template](/uwp/cpp-ref-for-winrt/make)

## Related topics
* [Consume APIs with C++/WinRT](consume-apis.md)
* [Author APIs with C++/WinRT](author-apis.md)
