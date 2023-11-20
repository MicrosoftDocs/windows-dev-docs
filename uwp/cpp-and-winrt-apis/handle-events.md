---
description: This topic shows how to register and revoke event-handling delegates using C++/WinRT.
title: Handle events by using delegates in C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projected, projection, handle, event, delegate
ms.localizationpriority: medium
---

# Handle events by using delegates in C++/WinRT

This topic shows how to register and revoke event-handling delegates using [C++/WinRT](./intro-to-using-cpp-with-winrt.md). You can handle an event using any standard C++ function-like object.

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) and the NuGet package (which together provide project template and build support), see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Using Visual Studio to add an event handler

A convenient way of adding an event handler to your project is by using the XAML Designer user interface (UI) in Visual Studio. With your XAML page open in the XAML Designer, select the control whose event you want to handle. Over in the property page for that control, click on the lightning-bolt icon to list all of the events that are sourced by that control. Then, double-click on the event that you want to handle; for example, *OnClicked*.

The XAML Designer adds the appropriate event handler function prototype (and a stub implementation) to your source files, ready for you to replace with your own implementation.

> [!NOTE]
> Typically, your event handlers don't need to be described in your Midl file (`.idl`). So, the XAML Designer doesn't add event handler function prototypes to your Midl file. It only adds them your `.h` and `.cpp` files.

## Register a delegate to handle an event

A simple example is handling a button's click event. It's typical to use XAML markup to register a member function to handle the event, like this.

```xaml
// MainPage.xaml
<Button x:Name="myButton" Click="ClickHandler">Click Me</Button>
```

```cppwinrt
// MainPage.h
void ClickHandler(
    winrt::Windows::Foundation::IInspectable const& sender,
    winrt::Windows::UI::Xaml::RoutedEventArgs const& args);

// MainPage.cpp
void MainPage::ClickHandler(
    IInspectable const& /* sender */,
    RoutedEventArgs const& /* args */)
{
    myButton().Content(box_value(L"Clicked"));
}
```

The code above is taken from the **Blank App (C++/WinRT)** project in Visual Studio. The code `myButton()` calls a generated accessor function, which returns the **Button** that we named *myButton*. If you change the `x:Name` of that **Button** element, then the name of the generated accessor function changes, too.

> [!NOTE]
> In this case, the event source (the object that raises the event) is the **Button** named *myButton*. And the event recipient (the object handling the event) is an instance of **MainPage**. There's more info later in this topic about managing the lifetime of event sources and event recipients.

Instead of doing it declaratively in markup, you can imperatively register a member function to handle an event. It may not be obvious from the code example below, but the argument to the [**ButtonBase::Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) call is an instance of the [**RoutedEventHandler**](/uwp/api/windows.ui.xaml.routedeventhandler) delegate. In this case, we're using the **RoutedEventHandler** constructor overload that takes an object and a pointer-to-member-function.

```cppwinrt
// MainPage.cpp
MainPage::MainPage()
{
    InitializeComponent();

    myButton().Click({ this, &MainPage::ClickHandler });
}
```

> [!IMPORTANT]
> When registering the delegate, the code example above passes a raw *this* pointer (pointing to the current object). To learn how to establish a strong or a weak reference to the current object, see [If you use a member function as a delegate](weak-references.md#if-you-use-a-member-function-as-a-delegate).

Here's an example that uses a static member function; note the simpler syntax.

```cppwinrt
// MainPage.h
static void ClickHandler(
    winrt::Windows::Foundation::IInspectable const& sender,
    winrt::Windows::UI::Xaml::RoutedEventArgs const& args);

// MainPage.cpp
MainPage::MainPage()
{
    InitializeComponent();

    myButton().Click( MainPage::ClickHandler );
}
void MainPage::ClickHandler(
    IInspectable const& /* sender */,
    RoutedEventArgs const& /* args */) { ... }
```

There are other ways to construct a **RoutedEventHandler**. Below is the syntax block taken from the documentation topic for [**RoutedEventHandler**](/uwp/api/windows.ui.xaml.routedeventhandler) (choose *C++/WinRT* from the **Language** drop-down in the upper-right corner of the webpage). Notice the various constructors: one takes a lambda; another a free function; and another (the one we used above) takes an object and a pointer-to-member-function.

```cppwinrt
struct RoutedEventHandler : winrt::Windows::Foundation::IUnknown
{
    RoutedEventHandler(std::nullptr_t = nullptr) noexcept;
    template <typename L> RoutedEventHandler(L lambda);
    template <typename F> RoutedEventHandler(F* function);
    template <typename O, typename M> RoutedEventHandler(O* object, M method);
    /* ... other constructors ... */
    void operator()(winrt::Windows::Foundation::IInspectable const& sender,
        winrt::Windows::UI::Xaml::RoutedEventArgs const& e) const;
};
```

The syntax of the function call operator is also helpful to see. It tells you what your delegate's parameters need to be. As you can see, in this case the function call operator syntax matches the parameters of our **MainPage::ClickHandler**.

> [!NOTE]
> For any given event, to figure out the details of its delegate, and that delegate's parameters, go first to the documentation topic for the event itself. Let's take the [UIElement.KeyDown event](/uwp/api/windows.ui.xaml.uielement.keydown) as an example. Visit that topic, and  choose *C++/WinRT* from the **Language** drop-down. In the syntax block at the beginning of the topic, you'll see this.
> 
> ```cppwinrt
> // Register
> event_token KeyDown(KeyEventHandler const& handler) const;
> ```
>
> That info tells us that the **UIElement.KeyDown** event (the topic we're on) has a delegate type of **KeyEventHandler**, since that's the type that you pass when you register a delegate with this event type. So, now follow the link on the topic to that [KeyEventHandler delegate](/uwp/api/windows.ui.xaml.input.keyeventhandler) type. Here, the syntax block contains a function call operator. And, as mentioned above, that tells you what your delegate's parameters need to be.
>
>```cppwinrt
>void operator()(
>   winrt::Windows::Foundation::IInspectable const& sender,
>   winrt::Windows::UI::Xaml::Input::KeyRoutedEventArgs const& e) const;
>```
>
>  As you can see, the delegate needs to be declared to take an **IInspectable** as the sender, and an instance of the [KeyRoutedEventArgs class](/uwp/api/windows.ui.xaml.input.keyroutedeventargs) as the args.
>
> To take another example, let's look at the [Popup.Closed event](/uwp/api/windows.ui.xaml.controls.primitives.popup.closed). Its delegate type is [EventHandler\<IInspectable\>](/uwp/api/windows.foundation.eventhandler-1). So, your delegate will take an **IInspectable** as the sender, and another **IInspectable** (because that's the **EventHandler**'s type parameter ) as the args.

If you're not doing much work in your event handler, then you can use a lambda function instead of a member function. Again, it may not be obvious from the code example below, but a **RoutedEventHandler** delegate is being constructed from a lambda function which, again, needs to match the syntax of the function call operator that we discussed above.

```cppwinrt
MainPage::MainPage()
{
    InitializeComponent();

    myButton().Click([this](IInspectable const& /* sender */, RoutedEventArgs const& /* args */)
    {
        myButton().Content(box_value(L"Clicked"));
    });
}
```

You can choose to be a little more explicit when you construct your delegate. For example, if you want to pass it around, or use it more than once.

```cppwinrt
MainPage::MainPage()
{
    InitializeComponent();

    auto click_handler = [](IInspectable const& sender, RoutedEventArgs const& /* args */)
    {
        sender.as<winrt::Windows::UI::Xaml::Controls::Button>().Content(box_value(L"Clicked"));
    };
    myButton().Click(click_handler);
    AnotherButton().Click(click_handler);
}
```

## Revoke a registered delegate

When you register a delegate, typically a token is returned to you. You can subsequently use that token to revoke your delegate; meaning that the delegate is unregistered from the event, and won't be called should the event be raised again.

For the sake of simplicity, none of the code examples above showed how to do that. But this next code example stores the token in the struct's private data member, and revokes its handler in the destructor.

```cppwinrt
struct Example : ExampleT<Example>
{
    Example(winrt::Windows::UI::Xaml::Controls::Button const& button) : m_button(button)
    {
        m_token = m_button.Click([this](IInspectable const&, RoutedEventArgs const&)
        {
            // ...
        });
    }
    ~Example()
    {
        m_button.Click(m_token);
    }

private:
    winrt::Windows::UI::Xaml::Controls::Button m_button;
    winrt::event_token m_token;
};
```

Instead of a strong reference, as in the example above, you can store a weak reference to the button (see [Strong and weak references in C++/WinRT](weak-references.md)).

> [!NOTE]
> When an event source raises its events synchronously, you can revoke your handler and be confident that you won't receive any more events. But for asynchronous events, even after revoking (and especially when revoking within the destructor), an in-flight event might reach your object after it has started destructing. Finding a place to unsubscribe prior to destruction might mitigate the issue, or for a robust solution see [Safely accessing the *this* pointer with an event-handling delegate](weak-references.md#safely-accessing-the-this-pointer-with-an-event-handling-delegate).

Alternatively, when you register a delegate, you can specify **winrt::auto_revoke** (which is a value of type [**winrt::auto_revoke_t**](/uwp/cpp-ref-for-winrt/auto-revoke-t)) to request an event revoker (of type [**winrt::event_revoker**](/uwp/cpp-ref-for-winrt/event-revoker)). The event revoker holds a weak reference to the event source (the object that raises the event) for you. You can manually revoke by calling the **event_revoker::revoke** member function; but the event revoker calls that function itself automatically when it goes out of scope. The **revoke** function checks whether the event source still exists and, if so, revokes your delegate. In this example, there's no need to store the event source, and no need for a destructor.

```cppwinrt
struct Example : ExampleT<Example>
{
    Example(winrt::Windows::UI::Xaml::Controls::Button button)
    {
        m_event_revoker = button.Click(
            winrt::auto_revoke,
            [this](IInspectable const& /* sender */,
            RoutedEventArgs const& /* args */)
        {
            // ...
        });
    }

private:
    winrt::Windows::UI::Xaml::Controls::Button::Click_revoker m_event_revoker;
};
```

Below is the syntax block taken from the documentation topic for the [**ButtonBase::Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event. It shows the three different registration and revoking functions. You can see exactly what type of event revoker you need to declare from the third overload. And you can pass the same kinds of delegates to both the *register* and the *revoke with event_revoker* overloads.

```cppwinrt
// Register
winrt::event_token Click(winrt::Windows::UI::Xaml::RoutedEventHandler const& handler) const;

// Revoke with event_token
void Click(winrt::event_token const& token) const;

// Revoke with event_revoker
Button::Click_revoker Click(winrt::auto_revoke_t,
    winrt::Windows::UI::Xaml::RoutedEventHandler const& handler) const;
```

> [!NOTE]
> In the code example above, `Button::Click_revoker` is a type alias for `winrt::event_revoker<winrt::Windows::UI::Xaml::Controls::Primitives::IButtonBase>`. A similar pattern applies to all C++/WinRT events. Each Windows Runtime event has a revoke function overload that returns an event revoker, and that revoker's type is a member of the event source. So, to take another example, the [**CoreWindow::SizeChanged**](/uwp/api/windows.ui.core.corewindow.sizechanged) event has a registration function overload that returns a value of type **CoreWindow::SizeChanged_revoker**.

You might consider revoking handlers in a page-navigation scenario. If you're repeatedly navigating into a page and then back out, then you could revoke any handlers when you navigate away from the page. Alternatively, if you're re-using the same page instance, then check the value of your token and only register if it's not yet been set (`if (!m_token){ ... }`). A third option is to store an event revoker in the page as a data member. And a fourth option, as described later in this topic, is to capture a strong or a weak reference to the *this* object in your lambda function.

### If your auto-revoke delegate fails to register

If you try to specify [**winrt::auto_revoke**](/uwp/cpp-ref-for-winrt/auto-revoke-t) when registering a delegate, and the result is a [**winrt::hresult_no_interface**](/uwp/cpp-ref-for-winrt/error-handling/hresult-no-interface) exception, then that usually means that the event source doesn't support weak references. That's a common situation in the [**Windows.UI.Composition**](/uwp/api/windows.ui.composition) namespace, for example. In this situation, you can't use the auto-revoke feature. You'll have to fall back to manually revoking your event handlers.

## Delegate types for asynchronous actions and operations

The examples above use the **RoutedEventHandler** delegate type, but there are of course many other delegate types. For example, asynchronous actions and operations (with and without progress) have completed and/or progress events that expect delegates of the corresponding type. For example, the progress event of an asynchronous operation with progress (which is anything that implements [**IAsyncOperationWithProgress**](/uwp/api/windows.foundation.iasyncoperationwithprogress-2)) requires a delegate of type [**AsyncOperationProgressHandler**](/uwp/api/windows.foundation.asyncoperationprogresshandler-2). Here's a code example of authoring a delegate of that type using a lambda function. The example also shows how to author an [**AsyncOperationWithProgressCompletedHandler**](/uwp/api/windows.foundation.asyncoperationwithprogresscompletedhandler-2) delegate.

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Syndication.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

void ProcessFeedAsync()
{
    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;

    auto async_op_with_progress = syndicationClient.RetrieveFeedAsync(rssFeedUri);

    async_op_with_progress.Progress(
        [](
            IAsyncOperationWithProgress<SyndicationFeed,
            RetrievalProgress> const& /* sender */,
            RetrievalProgress const& args)
        {
            uint32_t bytes_retrieved = args.BytesRetrieved;
            // use bytes_retrieved;
        });

    async_op_with_progress.Completed(
        [](
            IAsyncOperationWithProgress<SyndicationFeed,
            RetrievalProgress> const& sender,
            AsyncStatus const /* asyncStatus */)
        {
            SyndicationFeed syndicationFeed = sender.GetResults();
            // use syndicationFeed;
        });

    // or (but this function must then be a coroutine, and return IAsyncAction)
    // SyndicationFeed syndicationFeed{ co_await async_op_with_progress };
}
```

As the "coroutine" comment above suggests, instead of using a delegate with the completed events of asynchronous actions and operations, you'll probably find it more natural to use coroutines. For details, and code examples, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md).

> [!NOTE]
> It's not correct to implement more than one *completion handler* for an asynchronous action or operation. You can have either a single delegate for its completed event, or you can `co_await` it. If you have both, then the second will fail.

If you stick with delegates instead of a coroutine, then you can opt for a simpler syntax.

```cppwinrt
async_op_with_progress.Completed(
    [](auto&& /*sender*/, AsyncStatus const /* args */)
{
    // ...
});
```

## Delegate types that return a value

Some delegate types must themselves return a value. An example is [**ListViewItemToKeyHandler**](/uwp/api/windows.ui.xaml.controls.listviewitemtokeyhandler), which returns a string. Here's an example of authoring a delegate of that type (note that the lambda function returns a value).

```cppwinrt
using namespace winrt::Windows::UI::Xaml::Controls;

winrt::hstring f(ListView listview)
{
    return ListViewPersistenceHelper::GetRelativeScrollPosition(listview, [](IInspectable const& item)
    {
        return L"key for item goes here";
    });
}
```

## Safely accessing the *this* pointer with an event-handling delegate

If you handle an event with an object's member function, or from within a lambda function inside an object's member function, then you need to think about the relative lifetimes of the event recipient (the object handling the event) and the event source (the object raising the event). For more info, and code examples, see [Strong and weak references in C++/WinRT](weak-references.md#safely-accessing-the-this-pointer-with-an-event-handling-delegate).

## Important APIs
* [winrt::auto_revoke_t marker struct](/uwp/cpp-ref-for-winrt/auto-revoke-t)
* [winrt::implements::get_weak function](/uwp/cpp-ref-for-winrt/implements#implementsget_weak-function)
* [winrt::implements::get_strong function](/uwp/cpp-ref-for-winrt/implements#implementsget_strong-function)

## Related topics
* [Author events in C++/WinRT](./author-events.md)
* [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md)
* [Strong and weak references in C++/WinRT](./weak-references.md)
