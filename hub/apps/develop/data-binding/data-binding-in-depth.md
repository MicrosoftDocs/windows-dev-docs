---
ms.assetid: 2a50c798-6244-4fda-9091-a10a9e87fae2
title: Data binding in depth
description: Learn how to use data binding in Windows App SDK applications
ms.date: 12/12/2022
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# Data binding in depth

## Important APIs

- [**{x:Bind} markup extension**](/windows/uwp/xaml-platform/x-bind-markup-extension)
- [**Binding class**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.binding)
- [**DataContext**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.datacontext)
- [**INotifyPropertyChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.inotifypropertychanged)

## Introduction

> [!NOTE]
> This topic describes data binding features in detail. For a short, practical introduction, see [Data binding overview](data-binding-overview.md).

This topic is about data binding for the APIs that reside in the [**Microsoft.UI.Xaml.Data** namespace](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data).

Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. Data binding allows you to separate the concern of data from the concern of UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app.

You can use data binding to simply display values from a data source when the UI is first shown, but not to respond to changes in those values. This is a mode of binding called *one-time*, and it works well for a value that doesn't change during run-time. Alternatively, you can choose to "observe" the values and to update the UI when they change. This mode is called *one-way*, and it works well for read-only data. Ultimately, you can choose to both observe and update, so that changes that the user makes to values in the UI are automatically pushed back into the data source. This mode is called *two-way*, and it works well for read-write data. Here are some examples.

- You could use the one-time mode to bind an [**Image**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) to the current user's photo.
- You could use the one-way mode to bind a [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) to a collection of real-time news articles grouped by newspaper section.
- You could use the two-way mode to bind a [**TextBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox) to a customer's name in a form.

Independent of mode, there are two kinds of binding, and they're both typically declared in UI markup. You can choose to use either the [{x:Bind} markup extension](/windows/uwp/xaml-platform/x-bind-markup-extension) or the [{Binding} markup extension](/windows/uwp/xaml-platform/binding-markup-extension). And you can even use a mixture of the two in the same app—even on the same UI element. `{x:Bind}` was new in UWP for Windows 10 and it has better performance. All the details described in this topic apply to both kinds of binding unless we explicitly say otherwise.

### UWP Sample apps that demonstrate {x:Bind}

- [{x:Bind} sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlBind).
- [QuizGame](https://github.com/microsoft/Windows-appsample-networkhelper).
- [XAML UI Basics sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlUIBasics).

### UWP Sample apps that demonstrate {Binding}

- Download the [UWP Bookstore1](https://codeload.github.com/MicrosoftDocs/windows-topic-specific-samples/zip/Bookstore1Universal_10) app.
- Download the [Bookstore2](https://codeload.github.com/MicrosoftDocs/windows-topic-specific-samples/zip/Bookstore2Universal_10) app.

## Every binding involves these pieces

- A *binding source*. This is the source of the data for the binding, and it can be an instance of any class that has members whose values you want to display in your UI.
- A *binding target*. This is a [**DependencyProperty**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyproperty) of the [**FrameworkElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement) in your UI that displays the data.
- A *binding object*. This is the piece that transfers data values from the source to the target, and optionally from the target back to the source. The binding object is created at XAML load time from your [{x:Bind}](/windows/uwp/xaml-platform/x-bind-markup-extension) or [{Binding}](/windows/uwp/xaml-platform/binding-markup-extension) markup extension.

In the following sections, we'll take a closer look at the binding source, the binding target, and the binding object. And we'll link the sections together with the example of binding a button's content to a string property named `NextButtonText`, which belongs to a class named `HostViewModel`.

### Binding source

Here's a very rudimentary implementation of a class that we could use as a binding source.

``` csharp
public class HostViewModel
{
    public HostViewModel()
    {
        NextButtonText = "Next";
    }

    public string NextButtonText { get; set; }
}
```

That implementation of `HostViewModel`, and its property `NextButtonText`, are only appropriate for one-time binding. But one-way and two-way bindings are extremely common, and in those kinds of binding the UI automatically updates in response to changes in the data values of the binding source. In order for those kinds of binding to work correctly, you need to make your binding source "observable" to the binding object. So in our example, if we want to one-way or two-way bind to the `NextButtonText` property, then any changes that happen at run-time to the value of that property need to be made observable to the binding object.

One way of doing that is to derive the class that represents your binding source from [**DependencyObject**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyobject), and expose a data value through a [**DependencyProperty**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyproperty). That's how a [**FrameworkElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement) becomes observable. A `FrameworkElement` is a good binding source right out of the box.

A more lightweight way of making a class observable—and a necessary one for classes that already have a base class—is to implement [**System.ComponentModel.INotifyPropertyChanged**](/dotnet/api/system.componentmodel.inotifypropertychanged). This really just involves implementing a single event named `PropertyChanged`. An example using `HostViewModel` is below.

``` csharp
...
using System.ComponentModel;
using System.Runtime.CompilerServices;
...
public class HostViewModel : INotifyPropertyChanged
{
    private string nextButtonText;

    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    public HostViewModel()
    {
        NextButtonText = "Next";
    }

    public string NextButtonText
    {
        get { return nextButtonText; }
        set
        {
            nextButtonText = value;
            OnPropertyChanged();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        // Raise the PropertyChanged event, passing the name of the property whose value has changed.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Now the `NextButtonText` property is observable. When you author a one-way or a two-way binding to that property (we'll show how later), the resulting binding object subscribes to the `PropertyChanged` event. When that event is raised, the binding object's handler receives an argument containing the name of the property that has changed. That's how the binding object knows which property's value to go and read again.

So that you don't have to implement the pattern shown above multiple times, if you're using C# then you can just derive from the `BindableBase` base class that you'll find in the [QuizGame](https://github.com/microsoft/Windows-appsample-networkhelper) sample (in the "Common" folder). Here's an example of how that looks.

``` csharp
public class HostViewModel : BindableBase
{
    private string nextButtonText;

    public HostViewModel()
    {
        NextButtonText = "Next";
    }

    public string NextButtonText
    {
        get { return nextButtonText; }
        set { SetProperty(ref nextButtonText, value); }
    }
}
```

Raising the `PropertyChanged` event with an argument of [**String.Empty**](/dotnet/api/system.string.empty) or `null` indicates that all non-indexer properties on the object should be re-read. You can raise the event to indicate that indexer properties on the object have changed by using an argument of "Item\[*indexer*\]" for specific indexers (where *indexer* is the index value), or a value of "Item\[\]" for all indexers.

A binding source can be treated either as a single object whose properties contain data, or as a collection of objects. In C# code, you can one-time bind to an object that implements [**List(Of T)**](/dotnet/api/system.collections.generic.list-1) to display a collection that doesn't change at run-time. For an observable collection (observing when items are added to and removed from the collection), one-way bind to [**ObservableCollection(Of T)**](/dotnet/api/system.collections.objectmodel.observablecollection-1) instead. To bind to your own collection classes, use the guidance in the following table.

| Scenario | C# (CLR) | C++/WinRT |
|-|-|-|
| Bind to an object. | Can be any object. | Can be any object. |
| Get property change notifications from a bound object. | Object must implement [**INotifyPropertyChanged**](/dotnet/api/system.componentmodel.inotifypropertychanged). | Object must implement [**INotifyPropertyChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.inotifypropertychanged). |
| Bind to a collection. | [**List(Of T)**](/dotnet/api/system.collections.generic.list-1) | [**IVector**](/uwp/api/windows.foundation.collections.ivector_t_) of [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable), or [**IBindableObservableVector**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.interop.ibindableobservablevector). See [XAML items controls; bind to a C++/WinRT collection](/windows/uwp/cpp-and-winrt-apis/binding-collection) and [Collections with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/collections). |
| Get collection change notifications from a bound collection. | [**ObservableCollection(Of T)**](/dotnet/api/system.collections.objectmodel.observablecollection-1)|[**IObservableVector**](/uwp/api/windows.foundation.collections.iobservablevector_t_) of [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable). For example, [**winrt::single_threaded_observable_vector&lt;T&gt;**](/uwp/cpp-ref-for-winrt/single-threaded-observable-vector). |
| Implement a collection that supports binding. | Extend [**List(Of T)**](/dotnet/api/system.collections.generic.list-1) or implement [**IList**](/dotnet/api/system.collections.ilist), [**IList**](/dotnet/api/system.collections.generic.ilist-1)(Of [**Object**](/dotnet/api/system.object)), [**IEnumerable**](/dotnet/api/system.collections.ienumerable), or [**IEnumerable**](/dotnet/api/system.collections.generic.ienumerable-1)(Of **Object**). Binding to generic `IList(Of T)` and `IEnumerable(Of T)` is not supported. | Implement [**IVector**](/uwp/api/windows.foundation.collections.ivector_t_) of [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable). See [XAML items controls; bind to a C++/WinRT collection](/windows/uwp/cpp-and-winrt-apis/binding-collection) and [Collections with C++/WinRT](/windows/uwp/cpp-and-winrt-apis/collections). |
| Implement a collection that supports collection change notifications. | Extend [**ObservableCollection(Of T)**](/dotnet/api/system.collections.objectmodel.observablecollection-1) or implement (non-generic) [**IList**](/dotnet/api/system.collections.ilist) and [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged). | Implement [**IObservableVector**](/uwp/api/windows.foundation.collections.iobservablevector_t_) of [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable), or [**IBindableObservableVector**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.interop.ibindableobservablevector). |
| Implement a collection that supports incremental loading. | Extend [**ObservableCollection(Of T)**](/dotnet/api/system.collections.objectmodel.observablecollection-1) or implement (non-generic) [**IList**](/dotnet/api/system.collections.ilist) and [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged). Additionally, implement [**ISupportIncrementalLoading**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading). | Implement [**IObservableVector**](/uwp/api/windows.foundation.collections.iobservablevector_t_) of [**IInspectable**](/windows/win32/api/inspectable/nn-inspectable-iinspectable), or [**IBindableObservableVector**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.interop.ibindableobservablevector). Additionally, implement [**ISupportIncrementalLoading**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading) |
