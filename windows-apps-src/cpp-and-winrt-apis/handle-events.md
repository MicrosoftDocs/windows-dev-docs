---
author: stevewhims
description: This topic shows how to register and revoke event-handling delegates using C++/WinRT.
title: Handle events by using delegates in C++/WinRT
ms.author: stwhi
ms.date: 05/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projected, projection, handle, event, delegate
ms.localizationpriority: medium
---

# Handle events by using delegates in [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt)
This topic shows how to register and revoke event-handling delegates using C++/WinRT. You can handle an event using any standard C++ function-like object.

> [!NOTE]
> For info about installing and using the C++/WinRT Visual Studio Extension (VSIX) (which provides project template support, as well as C++/WinRT MSBuild properties and targets) see [Visual Studio support for C++/WinRT, and the VSIX](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-and-the-vsix).

## Register a delegate to handle an event
A simple example is handling a button's click event. It's typical to use XAML markup to register a member function to handle the event, like this.

```xaml
// MainPage.xaml
<Button x:Name="Button" Click="ClickHandler">Click Me</Button>
```

```cppwinrt
// MainPage.cpp
void MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
{
    Button().Content(box_value(L"Clicked"));
}
```

Instead of doing it declaratively in markup, you can imperatively register a member function to handle an event. It may not be obvious from the code example below, but the argument to the [**ButtonBase::Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) call is an instance of the [**RoutedEventHandler**](/uwp/api/windows.ui.xaml.routedeventhandler) delegate. In this case, we're using the **RoutedEventHandler** constructor overload that takes an object and a pointer-to-member-function.

```cppwinrt
// MainPage.cpp
MainPage::MainPage()
{
    InitializeComponent();

    Button().Click({ this, &MainPage::ClickHandler });
}
```

There are other ways to construct a **RoutedEventHandler**. Below is the syntax block taken from the documentation topic for [**RoutedEventHandler**](/uwp/api/windows.ui.xaml.routedeventhandler) (choose *C++/WinRT* from the **Language** drop-down on the page). Notice the various constructors: one takes a lambda; another a free function; and another (the one we used above) takes an object and a pointer-to-member-function.

```cppwinrt
struct RoutedEventHandler : winrt::Windows::Foundation::IUnknown
{
    RoutedEventHandler(std::nullptr_t = nullptr) noexcept;
    template <typename L> RoutedEventHandler(L lambda);
    template <typename F> RoutedEventHandler(F* function);
    template <typename O, typename M> RoutedEventHandler(O* object, M method);
    void operator()(winrt::Windows::Foundation::IInspectable const& sender,
        winrt::Windows::UI::Xaml::RoutedEventArgs const& e) const;
};
```

The syntax of the function call operator is also helpful to see. It tells you what your delegate's parameters need to be. As you can see, in this case the function call operator syntax matches the parameters of our **MainPage::ClickHandler**.

If you're not doing much work in your event handler, then you can use a lambda function instead of a member function. Again, it may not be obvious from the code example below, but a **RoutedEventHandler** delegate is being constructed from a lambda function which, again, needs to match the syntax of the function call operator.

```cppwinrt
MainPage::MainPage()
{
    InitializeComponent();

    Button().Click([this](IInspectable const&, RoutedEventArgs const&)
    {
        Button().Content(box_value(L"Clicked"));
    });
}
```

You can choose to be a little more explicit when you construct your delegate. For example, if you want to pass it around, or use it more than once.

```cppwinrt
MainPage::MainPage()
{
    InitializeComponent();

    auto click_handler = [](IInspectable const& sender, RoutedEventArgs const&)
    {
        sender.as<winrt::Windows::UI::Xaml::Controls::Button>().Content(box_value(L"Clicked"));
    };
    Button().Click(click_handler);
    AnotherButton().Click(click_handler);
}
```

## Revoke a registered delegate
When you register a delegate, typically a token is returned to you. You can subsequently use that token to revoke your delegate; meaning that the delegate is unregistered from the event, and won't be called should the event be raised again. For the sake of simplicity, none of the code examples above showed how to do that. But this next code example stores the token in the struct's private data member, and revokes its handler in the destructor.

```cppwinrt
struct Example : ExampleT<Example>
{
    Example(winrt::Windows::UI::Xaml::Controls::Button const& button) : m_button(button)
    {
        m_token = m_button.Click([this](IInspectable const&, RoutedEventArgs const&)
        {
            ...
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

Instead of a strong reference, as in the example above, you can store a weak reference to the button (see [Weak references in C++/WinRT](weak-references.md)).

Alternatively, when you register a delegate, you can specify **winrt::auto_revoke** (which is a value of type [**winrt::auto_revoke_t**](/uwp/cpp-ref-for-winrt/auto-revoke-t)) to request an event revoker (of type **winrt::event_revoker**). The event revoker holds a weak reference to the event source (the object raising the event) for you. You can manually revoke by calling the **event_revoker::revoke** member function; but the event revoker calls that function itself automatically when it goes out of scope. The **revoke** function checks whether the event source still exists and, if so, revokes your delegate. In this example, there's no need to store the event source, and no need for a destructor.

```cppwinrt
struct Example : ExampleT<Example>
{
    Example(winrt::Windows::UI::Xaml::Controls::Button button)
    {
        m_event_revoker = button.Click(winrt::auto_revoke, [this](IInspectable const&, RoutedEventArgs const&)
        {
            ...
        });
    }

private:
    winrt::event_revoker<winrt::Windows::UI::Xaml::Controls::Primitives::IButtonBase> m_event_revoker;
};
```

Below is the syntax block taken from the documentation topic for the [**ButtonBase::Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event. It shows the three different registration and revoking functions. You can see exactly what type of event revoker you need to declare from the third overload.

```cppwinrt
// Register
winrt::event_token Click(winrt::Windows::UI::Xaml::RoutedEventHandler const& handler) const;

// Revoke with event_token
void Click(winrt::event_token const& token) const;

// Revoke with event_revoker
winrt::event_revoker<winrt::Windows::UI::Xaml::Controls::Primitives::IButtonBase> Click(winrt::auto_revoke_t,
    winrt::Windows::UI::Xaml::RoutedEventHandler const& handler) const;
```

A similar pattern applies to all C++/WinRT events.

You might consider revoking handlers in a page-navigation scenario. If you're repeatedly navigating into a page and then back out, then you could revoke any handlers when you navigate away from the page. Alternatively, if you're re-using the same page instance, then check the value of your token and only register if it's not yet been set (`if (!m_token){ ... }`). A third option is to store an event revoker in the page as a data member. And a fourth option, as described later in this topic, is to capture a strong or a weak reference to the *this* object in your lambda function.

## Delegate types for asynchronous actions and operations
The examples above use the **RoutedEventHandler** delegate type, but there are of course many other delegate types. For example, asynchronous actions and operations (with and without progress) have completed and/or progress events that expect delegates of the corresponding type. For example, the progress event of an asynchronous operation with progress (which is anything that implements [**IAsyncOperationWithProgress**](/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_)) requires a delegate of type [**AsyncOperationProgressHandler**](/uwp/api/windows.foundation.asyncoperationprogresshandler). Here's a code example of authoring a delegate of that type using a lambda function. The example also shows how to author an [**AsyncOperationWithProgressCompletedHandler**](/uwp/api/windows.foundation.asyncoperationwithprogresscompletedhandler) delegate.

```cppwinrt
using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Web::Syndication;

void ProcessFeedAsync()
{
    Uri rssFeedUri{ L"https://blogs.windows.com/feed" };
    SyndicationClient syndicationClient;

    auto async_op_with_progress = syndicationClient.RetrieveFeedAsync(rssFeedUri);

    async_op_with_progress.Progress(
        [](IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress> const&, RetrievalProgress const& args)
    {
        uint32_t bytes_retrieved = args.BytesRetrieved;
        // use bytes_retrieved;
    });

    async_op_with_progress.Completed(
        [](IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress> const& sender, AsyncStatus const)
    {
        SyndicationFeed syndicationFeed = sender.GetResults();
        // use syndicationFeed;
    });
    
    // or (but this function must then be a coroutine and return IAsyncAction)
    // SyndicationFeed syndicationFeed{ co_await async_op_with_progress };
}
```

As the "coroutine" comment above suggests, instead of using a delegate with the completed events of asynchronous actions and operations, you'll probably find it more natural to use coroutines. For details, and code examples, see [Concurrency and asynchronous operations with C++/WinRT](concurrency.md).

But if you do stick with delegates, you can opt for a simpler syntax.

```cppwinrt
async_op_with_progress.Completed(
    [](auto&& /*sender*/, AsyncStatus const)
{
	....
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

## Using the *this* object in an event handler
If you handle an event from within a lambda function inside an object's member function, then you need to think about the relative lifetimes of the event recipient (the object handling the event) and the event source (the object raising the event).

In many cases, a recipient outlives all dependencies on its *this* pointer from within a given lambda function. Some of these cases are obvious, such as when a UI page handles an event raised by a control that's on the page. The button doesn't outlive the page, so neither does the handler. This holds true any time the recipient owns the source (as a data member, for example), or any time the recipient and the source are siblings and directly owned by some other object. If you're sure you have a case where the handler won't outlive the *this* that it depends on, then you can capture *this* normally, without consideration for strong or weak lifetime.

But there are still cases where *this* doesn't outlive its use in a handler (including handlers for completion and progress events raised by asynchronous actions and operations).

- If you're authoring a coroutine to implement an asynchronous method, then it's possible.
- In rare cases with certain XAML UI framework objects ([**SwapChainPanel**](/uwp/api/windows.ui.xaml.controls.swapchainpanel), for example), then it's possible, if the recipient is finalized without unregistering from the event source.

In these cases, an access violation results from code in a handler or in a coroutine's continuation attempting to use the invalid *this* object.

> [!IMPORTANT]
> If you encounter one of these situations, then you'll need to think about the lifetime of the *this* object; and whether or not the captured *this* object outlives the capture. If it doesn't, then capture it with a strong or a weak reference, as appropriate. See [**implements::get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsgetstrong-function), and [**implements::get_weak**](/uwp/cpp-ref-for-winrt/implements#implementsgetweak-function).
> Or&mdash;if it makes sense for your scenario and if threading considerations make it even possible&mdash;then another option is to revoke the handler after the recipient is done with the event, or in the recipient's destructor.

This code example uses the [**SwapChainPanel.CompositionScaleChanged**](/uwp/api/windows.ui.xaml.controls.swapchainpanel.compositionscalechanged) event as an illustration. It registers an event handler using a lambda that captures a weak reference to the recipient. For more info about weak references, see [Weak references in C++/WinRT](weak-references.md). 

```cppwinrt
winrt::Windows::UI::Xaml::Controls::SwapChainPanel m_swapChainPanel;
winrt::event_token m_compositionScaleChangedEventToken;

void RegisterEventHandler()
{
    m_compositionScaleChangedEventToken = m_swapChainPanel.CompositionScaleChanged([weakReferenceToThis{ get_weak() }]
        (Windows::UI::Xaml::Controls::SwapChainPanel const& sender,
        Windows::Foundation::IInspectable const& object)
    {
        if (auto strongReferenceToThis = weakReferenceToThis.get())
        {
            strongReferenceToThis->OnCompositionScaleChanged(sender, object);
        }
    });
}

void OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel const& sender,
    Windows::Foundation::IInspectable const& object)
{
    // Here, we know that the "this" object is valid.
}
```

In the lamba capture clause, a temporary variable is created, representing a weak reference to *this*. In the body of the lambda, if a strong reference to *this* can be obtained, then the **OnCompositionScaleChanged** function is called. That way, inside **OnCompositionScaleChanged**, *this* can safely be used.

## Important APIs
* [winrt::auto_revoke_t](/uwp/cpp-ref-for-winrt/auto-revoke-t)
* [winrt::implements::get_weak function](/uwp/cpp-ref-for-winrt/implements#implementsgetweak-function)
* [winrt::implements::get_strong function](/uwp/cpp-ref-for-winrt/implements#implementsgetstrong-function)

## Related topics
* [Author events in C++/WinRT](author-events.md)
* [Concurrency and asynchronous operations with C++/WinRT](concurrency.md)
* [Weak references in C++/WinRT](weak-references.md)
