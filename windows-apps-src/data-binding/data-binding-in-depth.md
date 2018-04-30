---
author: mcleblanc
ms.assetid: 41E1B4F1-6CAF-4128-A61A-4E400B149011
title: Data binding in depth
description: Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data.
ms.author: markl
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Data binding in depth



**Important APIs**

-   [**Binding class**](https://msdn.microsoft.com/library/windows/apps/BR209820)
-   [**DataContext**](https://msdn.microsoft.com/library/windows/apps/BR208713)
-   [**INotifyPropertyChanged**](https://msdn.microsoft.com/library/windows/apps/BR209899)

> [!Note]
> This topic describes data binding features in detail. For a short, practical introduction, see [Data binding overview](data-binding-quickstart.md).


Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. Data binding allows you to separate the concern of data from the concern of UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app.

You can use data binding to simply display values from a data source when the UI is first shown, but not to respond to changes in those values. This is called one-time binding, and it works well for data whose values don't change during run-time. Additionally, you can choose to "observe" the values and to update the UI when they change. This is called one-way binding, and it works well for read-only data. Ultimately, you can choose to both observe and update, so that changes that the user makes to values in the UI are automatically pushed back into the data source. This is called two-way binding, and it works well for read-write data. Here are some examples.

-   You could use one-time binding to bind an [**Image**](https://msdn.microsoft.com/library/windows/apps/BR242752) to the current user's photo.
-   You could use one-way binding to bind a [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878) to a collection of real-time news articles grouped by newspaper section.
-   You could use two-way binding to bind a [**TextBox**](https://msdn.microsoft.com/library/windows/apps/BR209683) to a customer's name in a form.

There are two kinds of binding, and they're both typically declared in UI markup. You can choose to use either the [{x:Bind} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204783) or the [{Binding} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204782). And you can even use a mixture of the two in the same app—even on the same UI element. {x:Bind} is new for Windows 10 and it has better performance. All the details described in this topic apply to both kinds of binding unless we explicitly say otherwise.

**Sample apps that demonstrate {x:Bind}**

-   [{x:Bind} sample](http://go.microsoft.com/fwlink/p/?linkid=619989).
-   [QuizGame](https://github.com/Microsoft/Windows-appsample-quizgame).
-   [XAML UI Basics sample](http://go.microsoft.com/fwlink/p/?linkid=619992).

**Sample apps that demonstrate {Binding}**

-   Download the [Bookstore1](http://go.microsoft.com/fwlink/?linkid=532950) app.
-   Download the [Bookstore2](http://go.microsoft.com/fwlink/?linkid=532952) app.

## Every binding involves these pieces

-   A *binding source*. This is the source of the data for the binding, and it can be an instance of any class that has members whose values you want to display in your UI.
-   A *binding target*. This is a [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/BR242362) of the [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/BR208706) in your UI that displays the data.
-   A *binding object*. This is the piece that transfers data values from the source to the target, and optionally from the target back to the source. The binding object is created at XAML load time from your [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) or [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) markup extension.

In the following sections, we'll take a closer look at the binding source, the binding target, and the binding object. And we'll link the sections together with the example of binding a button's content to a string property named **NextButtonText**, which belongs to a class named **HostViewModel**.

### Binding source

Here's a very rudimentary implementation of a class that we could use as a binding source.

**Note**  If you're using [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) with Visual C++ component extensions (C++/CX) then you'll need to add the [**BindableAttribute**](https://msdn.microsoft.com/library/windows/apps/Hh701872) attribute to your binding source class. If you're using [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) then you don't need that attribute. See [Adding a details view](data-binding-quickstart.md#adding-a-details-view) for a code snippet.

```csharp
public class HostViewModel
{
    public HostViewModel()
    {
        this.NextButtonText = "Next";
    }

    public string NextButtonText { get; set; }
}
```

That implementation of **HostViewModel**, and its property **NextButtonText**, are only appropriate for one-time binding. But one-way and two-way bindings are extremely common, and in those kinds of binding the UI automatically updates in response to changes in the data values of the binding source. In order for those kinds of binding to work correctly, you need to make your binding source "observable" to the binding object. So in our example, if we want to one-way or two-way bind to the **NextButtonText** property, then any changes that happen at run-time to the value of that property need to be made observable to the binding object.

One way of doing that is to derive the class that represents your binding source from [**DependencyObject**](https://msdn.microsoft.com/library/windows/apps/BR242356), and expose a data value through a [**DependencyProperty**](https://msdn.microsoft.com/library/windows/apps/BR242362). That's how a [**FrameworkElement**](https://msdn.microsoft.com/library/windows/apps/BR208706) becomes observable. **FrameworkElements** are good binding sources right out of the box.

A more lightweight way of making a class observable—and a necessary one for classes that already have a base class—is to implement [**System.ComponentModel.INotifyPropertyChanged**](https://msdn.microsoft.com/library/windows/apps/xaml/system.componentmodel.inotifypropertychanged.aspx). This really just involves implementing a single event named **PropertyChanged**. An example using **HostViewModel** is below.

**Note**  For C++/CX, you implement [**Windows::UI::Xaml::Data::INotifyPropertyChanged**](https://msdn.microsoft.com/library/windows/apps/BR209899), and the binding source class must either have the [**BindableAttribute**](https://msdn.microsoft.com/library/windows/apps/Hh701872) or implement [**ICustomPropertyProvider**](https://msdn.microsoft.com/library/windows/apps/BR209878).

```csharp
public class HostViewModel : INotifyPropertyChanged
{
    private string nextButtonText;

    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    public HostViewModel()
    {
        this.NextButtonText = "Next";
    }

    public string NextButtonText
    {
        get { return this.nextButtonText; }
        set
        {
            this.nextButtonText = value;
            this.OnPropertyChanged();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        // Raise the PropertyChanged event, passing the name of the property whose value has changed.
        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Now the **NextButtonText** property is observable. When you author a one-way or a two-way binding to that property (we'll show how later), the resulting binding object subscribes to the **PropertyChanged** event. When that event is raised, the binding object's handler receives an argument containing the name of the property that has changed. That's how the binding object knows which property's value to go and read again.

So that you don't have to implement the pattern shown above multiple times, you can just derive from the **BindableBase** bass class that you'll find in the [QuizGame](https://github.com/Microsoft/Windows-appsample-quizgame) sample (in the "Common" folder). Here's an example of how that looks.

```csharp
public class HostViewModel : BindableBase
{
    private string nextButtonText;

    public HostViewModel()
    {
        this.NextButtonText = "Next";
    }

    public string NextButtonText
    {
        get { return this.nextButtonText; }
        set { this.SetProperty(ref this.nextButtonText, value); }
    }
}
```

Raising the **PropertyChanged** event with an argument of [**String.Empty**](https://msdn.microsoft.com/library/windows/apps/xaml/system.string.empty.aspx) or **null** indicates that all non-indexer properties on the object should be re-read. You can raise the event to indicate that indexer properties on the object have changed by using an argument of "Item\[*indexer*\]" for specific indexers (where *indexer* is the index value), or a value of "Item\[\]" for all indexers.

A binding source can be treated either as a single object whose properties contain data, or as a collection of objects. In C# and Visual Basic code, you can one-time bind to an object that implements [**List(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/6sh2ey19.aspx) to display a collection that does not change at run-time. For an observable collection (observing when items are added to and removed from the collection), one-way bind to [**ObservableCollection(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/ms668604.aspx) instead. In C++ code, you can bind to [**Vector&lt;T&gt;**](https://msdn.microsoft.com/library/dn858385.aspx) for both observable and non-observable collections. To bind to your own collection classes, use the guidance in the following table.

| Scenario                                                        | C# and VB (CLR)                                                                                                                                                                                                                                                                                                                                                                                                                   | C++/CX                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
|-----------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Bind to an object.                                              | Can be any object.                                                                                                                                                                                                                                                                                                                                                                                                                 | Object must have [**BindableAttribute**](https://msdn.microsoft.com/library/windows/apps/Hh701872) or implement [**ICustomPropertyProvider**](https://msdn.microsoft.com/library/windows/apps/BR209878).                                                                                                                                                                                                                                                                                                             |
| Get property change updates from a bound object.                | Object must implement [**System.ComponentModel. INotifyPropertyChanged**](https://msdn.microsoft.com/library/windows/apps/xaml/system.componentmodel.inotifypropertychanged.aspx).                                                                                                                                                                                                                                                                                                         | Object must implement [**Windows.UI.Xaml.Data. INotifyPropertyChanged**](https://msdn.microsoft.com/library/windows/apps/BR209899).                                                                                                                                                                                                                                                                                                                                                           |
| Bind to a collection.                                           | [**List(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/6sh2ey19.aspx)                                                                                                                                                                                                                                                                                                                                                                            | [**Platform::Collections::Vector&lt;T&gt;**](https://msdn.microsoft.com/library/windows/apps/xaml/hh441570.aspx)                                                                                                                                                                                                                                                                                                                                                                                         |
| Get collection change updates from a bound collection.          | [**ObservableCollection(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/ms668604.aspx)                                                                                                                                                                                                                                                                                                                                        | [**Windows::Foundation::Collections::IObservableVector&lt;T&gt;**](https://msdn.microsoft.com/library/windows/apps/br226052.aspx)                                                                                                                                                                                                                                                                                                                                                                                         |
| Implement a collection that supports binding.                   | Extend [**List(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/6sh2ey19.aspx) or implement [**IList**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.ilist.aspx), [**IList**](https://msdn.microsoft.com/library/windows/apps/xaml/5y536ey6.aspx)(Of [**Object**](https://msdn.microsoft.com/library/windows/apps/xaml/system.object.aspx)), [**IEnumerable**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.ienumerable.aspx), or [**IEnumerable**](https://msdn.microsoft.com/library/windows/apps/xaml/9eekhta0.aspx)(Of **Object**). Binding to generic **IList(Of T)** and **IEnumerable(Of T)** is not supported. | Implement [**IBindableVector**](https://msdn.microsoft.com/library/windows/apps/Hh701979), [**IBindableIterable**](https://msdn.microsoft.com/library/windows/apps/Hh701957), [**IVector**](https://msdn.microsoft.com/library/windows/apps/BR206631)&lt;[**Object**](https://msdn.microsoft.com/library/windows/apps/xaml/system.object.aspx)^&gt;, [**IIterable**](https://msdn.microsoft.com/library/windows/apps/BR226024)&lt;**Object**^&gt;, **IVector**&lt;[**IInspectable**](https://msdn.microsoft.com/library/BR205821)\*&gt;, or **IIterable**&lt;**IInspectable**\*&gt;. Binding to generic **IVector&lt;T&gt;** and **IIterable&lt;T&gt;** is not supported. |
| Implement a collection that supports collection change updates. | Extend [**ObservableCollection(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/ms668604.aspx) or implement (non-generic) [**IList**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.ilist.aspx) and [**INotifyCollectionChanged**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.specialized.inotifycollectionchanged.aspx).                                                                                                                                                               | Implement [**IBindableVector**](https://msdn.microsoft.com/library/windows/apps/Hh701979) and [**IBindableObservableVector**](https://msdn.microsoft.com/library/windows/apps/Hh701974).                                                                                                                                                                                                                                                                                                                       |
| Implement a collection that supports incremental loading.       | Extend [**ObservableCollection(Of T)**](https://msdn.microsoft.com/library/windows/apps/xaml/ms668604.aspx) or implement (non-generic) [**IList**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.ilist.aspx) and [**INotifyCollectionChanged**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.specialized.inotifycollectionchanged.aspx). Additionally, implement [**ISupportIncrementalLoading**](https://msdn.microsoft.com/library/windows/apps/Hh701916).                                                          | Implement [**IBindableVector**](https://msdn.microsoft.com/library/windows/apps/Hh701979), [**IBindableObservableVector**](https://msdn.microsoft.com/library/windows/apps/Hh701974), and [**ISupportIncrementalLoading**](https://msdn.microsoft.com/library/windows/apps/Hh701916).                                                                                                                                                                                                                                         |

You can bind list controls to arbitrarily large data sources, and still achieve high performance, by using incremental loading. For example, you can bind list controls to Bing image query results without having to load all the results at once. Instead, you load only some results immediately, and load additional results as needed. To support incremental loading, you must implement [**ISupportIncrementalLoading**](https://msdn.microsoft.com/library/windows/apps/Hh701916) on a data source that supports collection change notification. When the data binding engine requests more data, your data source must make the appropriate requests, integrate the results, and then send the appropriate notifications in order to update the UI.

### Binding target

In the two examples below, the **Button.Content** property is the binding target, and its value is set to a markup extension which declares the binding object. First [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) is shown, and then [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782). Declaring bindings in markup is the common case (it's convenient, readable, and toolable). But you can avoid markup and imperatively (programmatically) create an instance of the [**Binding**](https://msdn.microsoft.com/library/windows/apps/BR209820) class instead if you need to.

<!-- XAML lang specifier not yet supported in OP. Using XML for now. -->
```xml
<Button Content="{x:Bind ...}" ... />
```

```xml
<Button Content="{Binding ...}" ... />
```

### Binding object declared using {x:Bind}

There's one step we need to do before we author our [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) markup. We need to expose our binding source class from the class that represents our page of markup. We do that by adding a property (of type **HostViewModel** in this case) to our **HostView** page class.

```csharp
namespace QuizGame.View
{
    public sealed partial class HostView : Page
    {
        public HostView()
        {
            this.InitializeComponent();
            this.ViewModel = new HostViewModel();
        }
    
        public HostViewModel ViewModel { get; set; }
    }
}
```

That done, we can now take a closer look at the markup that declares the binding object. The example below uses the same **Button.Content** binding target we used in the "Binding target" section earlier, and shows it being bound to the **HostViewModel.NextButtonText** property.

```xml
<Page x:Class="QuizGame.View.HostView" ... >
    <Button Content="{x:Bind Path=ViewModel.NextButtonText, Mode=OneWay}" ... />
</Page>
```

Notice the value that we specify for **Path**. This value is interpreted in the context of the page itself, and in this case the path begins by referencing the **ViewModel** property that we just added to the **HostView** page. That property returns a **HostViewModel** instance, and so we can dot into that object to access the **HostViewModel.NextButtonText** property. And we specify **Mode**, to override the [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) default of one-time.

The [**Path**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.path) property supports a variety of syntax options for binding to nested properties, attached properties, and integer and string indexers. For more info, see [Property-path syntax](https://msdn.microsoft.com/library/windows/apps/Mt185586). Binding to string indexers gives you the effect of binding to dynamic properties without having to implement [**ICustomPropertyProvider**](https://msdn.microsoft.com/library/windows/apps/BR209878). For other settings, see [{x:Bind} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204783).

**Note**  Changes to [**TextBox.Text**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.textbox.text) are sent to a two-way bound source when the [**TextBox**](https://msdn.microsoft.com/library/windows/apps/BR209683) loses focus, and not after every user keystroke.

**DataTemplate and x:DataType**

Inside a [**DataTemplate**](https://msdn.microsoft.com/library/windows/apps/BR242348) (whether used as an item template, a content template, or a header template), the value of **Path** is not interpreted in the context of the page, but in the context of the data object being templated. When using {x:Bind} in a data template, so that its bindings can be validated (and efficient code generated for them) at compile-time, the **DataTemplate** needs to declare the type of its data object using **x:DataType**. The example given below could be used as the **ItemTemplate** of an items control bound to a collection of **SampleDataGroup** objects.

```xml
<DataTemplate x:Key="SimpleItemTemplate" x:DataType="data:SampleDataGroup">
    <StackPanel Orientation="Vertical" Height="50">
      <TextBlock Text="{x:Bind Title}"/>
      <TextBlock Text="{x:Bind Description}"/>
    </StackPanel>
  </DataTemplate>
```

**Weakly-typed objects in your Path**

Consider for example that you have a type named SampleDataGroup, which implements a string property named Title. And you have a property MainPage.SampleDataGroupAsObject, which is of type object but which actually returns an instance of SampleDataGroup. The binding `<TextBlock Text="{x:Bind SampleDataGroupAsObject.Title}"/>` will result in a compile error because the Title property is not found on the type object. The remedy for this is to add a cast to your Path syntax like this: `<TextBlock Text="{x:Bind ((data:SampleDataGroup)SampleDataGroupAsObject).Title}"/>`. Here's another example where Element is declared as object but is actually a TextBlock: `<TextBlock Text="{x:Bind Element.Text}"/>`. And a cast remedies the issue: `<TextBlock Text="{x:Bind ((TextBlock)Element).Text}"/>`.

**If your data loads asynchronously**

Code to support **{x:Bind}** is generated at compile-time in the partial classes for your pages. These files can be found in your `obj` folder, with names like (for C#) `<view name>.g.cs`. The generated code includes a handler for your page's [**Loading**](https://msdn.microsoft.com/library/windows/apps/BR208706) event, and that handler calls the **Initialize** method on a generated class that represent's your page's bindings. **Initialize** in turn calls **Update** to begin moving data between the binding source and the target. **Loading** is raised just before the first measure pass of the page or user control. So if your data is loaded asynchronously it may not be ready by the time **Initialize** is called. So, after you've loaded data, you can force one-time bindings to be initialized by calling `this.Bindings.Update();`. If you only need one-time bindings for asynchronously-loaded data then it’s much cheaper to initialize them this way than it is to have one-way bindings and to listen for changes. If your data does not undergo fine-grained changes, and if it's likely to be updated as part of a specific action, then you can make your bindings one-time, and force a manual update at any time with a call to **Update**.

**Limitations**

**{x:Bind}** is not suited to late-bound scenarios, such as navigating the dictionary structure of a JSON object, nor duck typing which is a weak form of typing based on lexical matches on property names ("if it walks, swims, and quacks like a duck then it's a duck"). With duck typing, a binding to the Age property would be equally satisfied with a Person or a Wine object. For these scenarios, use **{Binding}**.

### Binding object declared using {Binding}

[{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) assumes, by default, that you're binding to the [**DataContext**](https://msdn.microsoft.com/library/windows/apps/BR208713) of your markup page. So we'll set the **DataContext** of our page to be an instance of our binding source class (of type **HostViewModel** in this case). The example below shows the markup that declares the binding object. We use the same **Button.Content** binding target we used in the "Binding target" section earlier, and we bind to the **HostViewModel.NextButtonText** property.

```xml
<Page xmlns:viewmodel="using:QuizGame.ViewModel" ... >
    <Page.DataContext>
        <viewmodel:HostViewModel/>
    </Page.DataContext>
    ...
    <Button Content="{Binding Path=NextButtonText}" ... />
</Page>
```

Notice the value that we specify for **Path**. This value is interpreted in the context of the page's [**DataContext**](https://msdn.microsoft.com/library/windows/apps/BR208713), which in this example is set to an instance of **HostViewModel**. The path references the **HostViewModel.NextButtonText** property. We can omit **Mode**, because the [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) default of one-way works here.

The default value of [**DataContext**](https://msdn.microsoft.com/library/windows/apps/BR208713) for a UI element is the inherited value of its parent. You can of course override that default by setting **DataContext** explicitly, which is in turn inherited by children by default. Setting **DataContext** explicitly on an element is useful when you want to have multiple bindings that use the same source.

A binding object has a **Source** property, which defaults to the [**DataContext**](https://msdn.microsoft.com/library/windows/apps/BR208713) of the UI element on which the binding is declared. You can override this default by setting **Source**, **RelativeSource**, or **ElementName** explicitly on the binding (see [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) for details).

Inside a [**DataTemplate**](https://msdn.microsoft.com/library/windows/apps/BR242348), the [**DataContext**](https://msdn.microsoft.com/library/windows/apps/BR208713) is set to the data object being templated. The example given below could be used as the **ItemTemplate** of an items control bound to a collection of any type that has string properties named **Title** and **Description**.

```xml
<DataTemplate x:Key="SimpleItemTemplate">
    <StackPanel Orientation="Vertical" Height="50">
      <TextBlock Text="{Binding Title}"/>
      <TextBlock Text="{Binding Description"/>
    </StackPanel>
  </DataTemplate>
```

**Note**  By default, changes to [**TextBox.Text**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.textbox.text) are sent to a two-way bound source when the [**TextBox**](https://msdn.microsoft.com/library/windows/apps/BR209683) loses focus. To cause changes to be sent after every user keystroke, set **UpdateSourceTrigger** to **PropertyChanged** on the binding in markup. You can also completely take control of when changes are sent to the source by setting **UpdateSourceTrigger** to **Explicit**. You then handle events on the text box (typically [**TextBox.TextChanged**](https://msdn.microsoft.com/library/windows/apps/BR209683)), call [**GetBindingExpression**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.frameworkelement.getbindingexpression) on the target to get a [**BindingExpression**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.bindingexpression.aspx) object, and finally call [**BindingExpression.UpdateSource**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.bindingexpression.updatesource.aspx) to programmatically update the data source.

The [**Path**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.path) property supports a variety of syntax options for binding to nested properties, attached properties, and integer and string indexers. For more info, see [Property-path syntax](https://msdn.microsoft.com/library/windows/apps/Mt185586). Binding to string indexers gives you the effect of binding to dynamic properties without having to implement [**ICustomPropertyProvider**](https://msdn.microsoft.com/library/windows/apps/BR209878). The [**ElementName**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.elementname) property is useful for element-to-element binding. The [**RelativeSource**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.relativesource) property has several uses, one of which is as a more powerful alternative to template binding inside a [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/BR209391). For other settings, see [{Binding} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204782) and the [**Binding**](https://msdn.microsoft.com/library/windows/apps/BR209820) class.

## What if the source and the target are not the same type?

If you want to control the visibility of a UI element based on the value of a boolean property, or if you want to render a UI element with a color that's a function of a numeric value's range or trend, or if you want to display a date and/or time value in a UI element property that expects a string, then you'll need to convert values from one type to another. There will be cases where the right solution is to expose another property of the right type from your binding source class, and keep the conversion logic encapsulated and testable there. But that isn't flexible nor scalable when you have large numbers, or large combinations, of source and target properties. In that case you have a couple of options:

* If using {x:Bind} then you can bind directly to a function to do that conversion
* Or you can specify a value converter which is an object designed to perform the conversion 

## Value Converters

Here's a value converter, suitable for a one-time or a one-way binding, that converts a [**DateTime**](https://msdn.microsoft.com/library/windows/apps/xaml/system.datetime.aspx) value to a string value containing the month. The class implements [**IValueConverter**](https://msdn.microsoft.com/library/windows/apps/BR209903).

```csharp
public class DateToStringConverter : IValueConverter
{
    // Define the Convert method to convert a DateTime value to 
    // a month string.
    public object Convert(object value, Type targetType, 
        object parameter, string language)
    {
        // value is the data from the source object.
        DateTime thisdate = (DateTime)value;
        int monthnum = thisdate.Month;
        string month;
        switch (monthnum)
        {
            case 1:
                month = "January";
                break;
            case 2:
                month = "February";
                break;
            default:
                month = "Month not found";
                break;
        }
        // Return the value to pass to the target.
        return month;
    }

    // ConvertBack is not implemented for a OneWay binding.
    public object ConvertBack(object value, Type targetType, 
        object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
```

```vbnet
Public Class DateToStringConverter
    Implements IValueConverter

    ' Define the Convert method to change a DateTime object to
    ' a month string.
    Public Function Convert(ByVal value As Object, -
        ByVal targetType As Type, ByVal parameter As Object, -
        ByVal language As String) As Object -
        Implements IValueConverter.Convert

        ' value is the data from the source object.
        Dim thisdate As DateTime = CType(value, DateTime)
        Dim monthnum As Integer = thisdate.Month
        Dim month As String
        Select Case (monthnum)
            Case 1
                month = "January"
            Case 2
                month = "February"
            Case Else
                month = "Month not found"
        End Select
        ' Return the value to pass to the target.
        Return month

    End Function

    ' ConvertBack is not implemented for a OneWay binding.
    Public Function ConvertBack(ByVal value As Object, -
        ByVal targetType As Type, ByVal parameter As Object, -
        ByVal language As String) As Object -
        Implements IValueConverter.ConvertBack

        Throw New NotImplementedException

    End Function
End Class
```

And here's how you consume that value converter in your binding object markup.

```xml
<UserControl.Resources>
  <local:DateToStringConverter x:Key="Converter1"/>
</UserControl.Resources>

...

<TextBlock Grid.Column="0" 
  Text="{x:Bind ViewModel.Month, Converter={StaticResource Converter1}}"/>

<TextBlock Grid.Column="0" 
  Text="{Binding Month, Converter={StaticResource Converter1}}"/>
```

The binding engine calls the [**Convert**](https://msdn.microsoft.com/library/windows/apps/hh701934) and [**ConvertBack**](https://msdn.microsoft.com/library/windows/apps/hh701938) methods if the [**Converter**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.converter) parameter is defined for the binding. When data is passed from the source, the binding engine calls **Convert** and passes the returned data to the target. When data is passed from the target (for a two-way binding), the binding engine calls **ConvertBack** and passes the returned data to the source.

The converter also has optional parameters: [**ConverterLanguage**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.converterlanguage), which allows specifying the language to be used in the conversion, and [**ConverterParameter**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.binding.converterparameter), which allows passing a parameter for the conversion logic. For an example that uses a converter parameter, see [**IValueConverter**](https://msdn.microsoft.com/library/windows/apps/BR209903).

**Note**  If there is an error in the conversion, do not throw an exception. Instead, return [**DependencyProperty.UnsetValue**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.dependencyproperty.unsetvalue), which will stop the data transfer.

To display a default value to use whenever the binding source cannot be resolved, set the **FallbackValue** property on the binding object in markup. This is useful to handle conversion and formatting errors. It is also useful to bind to source properties that might not exist on all objects in a bound collection of heterogeneous types.

If you bind a text control to a value that is not a string, the data binding engine will convert the value to a string. If the value is a reference type, the data binding engine will retrieve the string value by calling [**ICustomPropertyProvider.GetStringRepresentation**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.icustompropertyprovider.getstringrepresentation) or [**IStringable.ToString**](https://msdn.microsoft.com/library/Dn302136) if available, and will otherwise call [**Object.ToString**](https://msdn.microsoft.com/library/windows/apps/system.object.tostring.aspx). Note, however, that the binding engine will ignore any **ToString** implementation that hides the base-class implementation. Subclass implementations should override the base class **ToString** method instead. Similarly, in native languages, all managed objects appear to implement [**ICustomPropertyProvider**](https://msdn.microsoft.com/library/windows/apps/BR209878) and [**IStringable**](https://msdn.microsoft.com/library/Dn302135). However, all calls to **GetStringRepresentation** and **IStringable.ToString** are routed to **Object.ToString** or an override of that method, and never to a new **ToString** implementation that hides the base-class implementation.

> [!NOTE]
> Starting in Windows 10, version 1607, the XAML framework provides a built in boolean to Visibility converter. The converter maps **true** to the **Visible** enumeration value and **false** to **Collapsed** so you can bind a Visibility property to a boolean without creating a converter. To use the built in converter, your app's minimum target SDK version must be 14393 or later. You can't use it when your app targets earlier versions of Windows 10. For more info about target versions, see [Version adaptive code](https://msdn.microsoft.com/windows/uwp/debug-test-perf/version-adaptive-code).

## Function binding in {x:Bind}

{x:Bind} enables the final step in a binding path to be a function. This can be used to perform conversions, and to perform bindings that depend on more than one property. See [**{x:Bind} Markup Extension**](https://msdn.microsoft.com/windows/uwp/xaml-platform/x-bind-markup-extension)

<span id="resource-dictionaries-with-x-bind"/>

## Resource dictionaries with {x:Bind}

The [{x:Bind} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204783) depends on code generation, so it needs a code-behind file containing a constructor that calls **InitializeComponent** (to initialize the generated code). You re-use the resource dictionary by instantiating its type (so that **InitializeComponent** is called) instead of referencing its filename. Here's an example of what to do if you have an existing resource dictionary and you want to use {x:Bind} in it.

TemplatesResourceDictionary.xaml

```xml
<ResourceDictionary
    x:Class="ExampleNamespace.TemplatesResourceDictionary"
    .....
    xmlns:examplenamespace="using:ExampleNamespace">
    
    <DataTemplate x:Key="EmployeeTemplate" x:DataType="examplenamespace:IEmployee">
        <Grid>
            <TextBlock Text="{x:Bind Name}"/>
        </Grid>
    </DataTemplate>
</ResourceDictionary>
```

TemplatesResourceDictionary.xaml.cs

```csharp
using Windows.UI.Xaml.Data;
 
namespace ExampleNamespace
{
    public partial class TemplatesResourceDictionary
    {
        public TemplatesResourceDictionary()
        {
            InitializeComponent();
        }
    }
}
```

MainPage.xaml

```xml
<Page x:Class="ExampleNamespace.MainPage"
    ....
    xmlns:examplenamespace="using:ExampleNamespace">

    <Page.Resources>
        <ResourceDictionary>
            .... 
            <ResourceDictionary.MergedDictionaries>
                <examplenamespace:TemplatesResourceDictionary/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Page.Resources>
</Page>
```

## Event binding and ICommand

[{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) supports a feature called event binding. With this feature, you can specify the handler for an event using a binding, which is an additional option on top of handling events with a method on the code-behind file. Let's say you have a **RootFrame** property on your **MainPage** class.

```csharp
    public sealed partial class MainPage : Page
    {
        ....    
        public Frame RootFrame { get { return Window.Current.Content as Frame; } }
    }
```

You can then bind a button's **Click** event to a method on the **Frame** object returned by the **RootFrame** property like this. Note that we also bind the button's **IsEnabled** property to another member of the same **Frame**.

```xml
    <AppBarButton Icon="Forward" IsCompact="True"
    IsEnabled="{x:Bind RootFrame.CanGoForward, Mode=OneWay}"
    Click="{x:Bind RootFrame.GoForward}"/>
```

Overloaded methods cannot be used to handle an event with this technique. Also, if the method that handles the event has parameters then they must all be assignable from the types of all of the event's parameters, respectively. In this case, [**Frame.GoForward**](https://msdn.microsoft.com/library/windows/apps/BR242693) is not overloaded and it has no parameters (but it would still be valid even if it took two **object** parameters). [**Frame.GoBack**](https://msdn.microsoft.com/library/windows/apps/Dn996568) is overloaded, though, so we can't use that method with this technique.

The event binding technique is similar to implementing and consuming commands (a command is a property that returns an object that implements the [**ICommand**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.icommand.aspx) interface). Both [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) and [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) work with commands. So that you don't have to implement the command pattern multiple times, you can use the **DelegateCommand** helper class that you'll find in the [QuizGame](https://github.com/Microsoft/Windows-appsample-quizgame) sample (in the "Common" folder).


## Binding to a collection of folders or files

You can use the APIs in the [**Windows.Storage**](https://msdn.microsoft.com/library/windows/apps/BR227346) namespace to retrieve folder and file data. However, the various **GetFilesAsync**, **GetFoldersAsync**, and **GetItemsAsync** methods do not return values that are suitable for binding to list controls. Instead, you must bind to the return values of the [**GetVirtualizedFilesVector**](https://msdn.microsoft.com/library/windows/apps/Hh701422), [**GetVirtualizedFoldersVector**](https://msdn.microsoft.com/library/windows/apps/Hh701428), and [**GetVirtualizedItemsVector**](https://msdn.microsoft.com/library/windows/apps/Hh701430) methods of the [**FileInformationFactory**](https://msdn.microsoft.com/library/windows/apps/BR207501) class. The following code example from the [StorageDataSource and GetVirtualizedFilesVector sample](http://go.microsoft.com/fwlink/p/?linkid=228621) shows the typical usage pattern. Remember to declare the **picturesLibrary** capability in your app package manifest, and confirm that there are pictures in your Pictures library folder.

```csharp
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var library = Windows.Storage.KnownFolders.PicturesLibrary;
            var queryOptions = new Windows.Storage.Search.QueryOptions();
            queryOptions.FolderDepth = Windows.Storage.Search.FolderDepth.Deep;
            queryOptions.IndexerOption = Windows.Storage.Search.IndexerOption.UseIndexerWhenAvailable;

            var fileQuery = library.CreateFileQueryWithOptions(queryOptions);

            var fif = new Windows.Storage.BulkAccess.FileInformationFactory(
                fileQuery,
                Windows.Storage.FileProperties.ThumbnailMode.PicturesView,
                190,
                Windows.Storage.FileProperties.ThumbnailOptions.UseCurrentScale,
                false
                );

            var dataSource = fif.GetVirtualizedFilesVector();
            this.PicturesListView.ItemsSource = dataSource;
        }
```

You will typically use this approach to create a read-only view of file and folder info. You can create two-way bindings to the file and folder properties, for example to let users rate a song in a music view. However, any changes are not persisted until you call the appropriate **SavePropertiesAsync** method (for example, [**MusicProperties.SavePropertiesAsync**](https://msdn.microsoft.com/library/windows/apps/BR207760)). You should commit changes when the item loses focus because this triggers a selection reset.

Note that two-way binding using this technique works only with indexed locations, such as Music. You can determine whether a location is indexed by calling the [**FolderInformation.GetIndexedStateAsync**](https://msdn.microsoft.com/library/windows/apps/BR207627) method.

Note also that a virtualized vector can return **null** for some items before it populates their value. For example, you should check for **null** before you use the [**SelectedItem**](https://msdn.microsoft.com/library/windows/apps/BR209770) value of a list control bound to a virtualized vector, or use [**SelectedIndex**](https://msdn.microsoft.com/library/windows/apps/BR209768) instead.

## Binding to data grouped by a key

If you take a flat collection of items—books, for example, represented by a **BookSku** class—and you group the items by using a common property as a key—the **BookSku.AuthorName** property, for example—then the result is called grouped data. When you group data, it is no longer a flat collection. Grouped data is a collection of group objects, where each group object has a) a key and b) a collection of items whose property matches that key. To take the books example again, the result of grouping the books by author name results in a collection of author name groups where each group has a) a key, which is an author name, and b) a collection of the **BookSku**s whose **AuthorName** property matches the group's key.

In general, to display a collection, you bind the [**ItemsSource**](https://msdn.microsoft.com/library/windows/apps/BR242828) of an items control (such as [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878) or [**GridView**](https://msdn.microsoft.com/library/windows/apps/BR242705)) directly to a property that returns a collection. If that's a flat collection of items then you don't need to do anything special. But if it's a collection of group objects (as it is when binding to grouped data) then you need the services of an intermediary object called a [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833) which sits between the items control and the binding source. You bind the **CollectionViewSource** to the property that returns grouped data, and you bind the items control to the **CollectionViewSource**. An extra value-add of a **CollectionViewSource** is that it keeps track of the current item, so you can keep more than one items control in sync by binding them all to the same **CollectionViewSource**. You can also access the current item programmatically through the [**ICollectionView.CurrentItem**](https://msdn.microsoft.com/library/windows/apps/BR209857) property of the object returned by the [**CollectionViewSource.View**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.collectionviewsource.view) property.

To activate the grouping facility of a [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833), set [**IsSourceGrouped**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.collectionviewsource.issourcegrouped) to **true**. Whether you also need to set the [**ItemsPath**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.data.collectionviewsource.itemspath) property depends on exactly how you author your group objects. There are two ways to author a group object: the "is-a-group" pattern, and the "has-a-group" pattern. In the "is-a-group" pattern, the group object derives from a collection type (for example, **List&lt;T&gt;**), so the group object actually is itself the group of items. With this pattern you do not need to set **ItemsPath**. In the "has-a-group" pattern, the group object has one or more properties of a collection type (such as **List&lt;T&gt;**), so the group "has a" group of items in the form of a property (or several groups of items in the form of several properties). With this pattern you need to set **ItemsPath** to the name of the property that contains the group of items.

The example below illustrates the "has-a-group" pattern. The page class has a property named [**ViewModel**](https://msdn.microsoft.com/library/windows/apps/BR208713), which returns an instance of our view model. The [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833) binds to the **Authors** property of the view model (**Authors** is the collection of group objects) and also specifies that it's the **Author.BookSkus** property that contains the grouped items. Finally, the [**GridView**](https://msdn.microsoft.com/library/windows/apps/BR242705) is bound to the **CollectionViewSource**, and has its group style defined so that it can render the items in groups.

```csharp
    <Page.Resources>
        <CollectionViewSource
        x:Name="AuthorHasACollectionOfBookSku"
        Source="{x:Bind ViewModel.Authors}"
        IsSourceGrouped="true"
        ItemsPath="BookSkus"/>
    </Page.Resources>
    ...

    <GridView
    ItemsSource="{Binding Source={StaticResource AuthorHasACollectionOfBookSku}}" ...>
        <GridView.GroupStyle>
            <GroupStyle
                HeaderTemplate="{StaticResource AuthorGroupHeaderTemplateWide}" ... />
        </GridView.GroupStyle>
    </GridView>
```

Note that the [**ItemsSource**](https://msdn.microsoft.com/library/windows/apps/BR242828) must use [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) (and not [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783)) because it needs to set the **Source** property to a resource. To see the above example in the context of the complete app, download the [Bookstore2](http://go.microsoft.com/fwlink/?linkid=532952) sample app. Unlike the markup shown above, [Bookstore2](http://go.microsoft.com/fwlink/?linkid=532952) uses {Binding} exclusively.

You can implement the "is-a-group" pattern in one of two ways. One way is to author your own group class. Derive the class from **List&lt;T&gt;** (where *T* is the type of the items). For example, `public class Author : List<BookSku>`. The second way is to use a [LINQ](http://msdn.microsoft.com/library/bb397926.aspx) expression to dynamically create group objects (and a group class) from like property values of the **BookSku** items. This approach—maintaining only a flat list of items and grouping them together on the fly—is typical of an app that accesses data from a cloud service. You get the flexibility to group books by author or by genre (for example) without needing special group classes such as **Author** and **Genre**.

The example below illustrates the "is-a-group" pattern using [LINQ](http://msdn.microsoft.com/library/bb397926.aspx). This time we group books by genre, displayed with the genre name in the group headers. This is indicated by the "Key" property path in reference to the group [**Key**](https://msdn.microsoft.com/library/windows/apps/bb343251.aspx) value.

```csharp
    using System.Linq;

    ...

    private IOrderedEnumerable<IGrouping<string, BookSku>> genres;

    public IOrderedEnumerable<IGrouping<string, BookSku>> Genres
    {
        get
        {
            if (this.genres == null)
            {
                this.genres = from book in this.bookSkus
                group book by book.genre into grp
                orderby grp.Key select grp;
            }
            return this.genres;
        }
    }
```

Remember that when using [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) with data templates we need to indicate the type being bound to by setting an **x:DataType** value. If the type is generic then we can't express that in markup so we need to use [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) instead in the group style header template.

```xml
    <Grid.Resources>
        <CollectionViewSource x:Name="GenreIsACollectionOfBookSku"
        Source="{Binding Genres}"
        IsSourceGrouped="true"/>
    </Grid.Resources>
    <GridView ItemsSource="{Binding Source={StaticResource GenreIsACollectionOfBookSku}}">
        <GridView.ItemTemplate x:DataType="local:BookTemplate">
            <DataTemplate>
                <TextBlock Text="{x:Bind Title}"/>
            </DataTemplate>
        </GridView.ItemTemplate>
        <GridView.GroupStyle>
            <GroupStyle>
                <GroupStyle.HeaderTemplate>
                    <DataTemplate>
                        <TextBlock Text="{Binding Key}"/>
                    </DataTemplate>
                </GroupStyle.HeaderTemplate>
            </GroupStyle>
        </GridView.GroupStyle>
    </GridView>
```

A [**SemanticZoom**](https://msdn.microsoft.com/library/windows/apps/Hh702601) control is a great way for your users to view and navigate grouped data. The [Bookstore2](http://go.microsoft.com/fwlink/?linkid=532952) sample app illustrates how to use the **SemanticZoom**. In that app, you can view a list of books grouped by author (the zoomed-in view) or you can zoom out to see a jump list of authors (the zoomed-out view). The jump list affords much quicker navigation than scrolling through the list of books. The zoomed-in and zoomed-out views are actually **ListView** or **GridView** controls bound to the same **CollectionViewSource**.

![An illustration of a SemanticZoom](images/sezo.png)

When you bind to hierarchical data—such as subcategories within categories—you can choose to display the hierarchical levels in your UI with a series of items controls. A selection in one items control determines the contents of subsequent items controls. You can keep the lists synchronized by binding each list to its own [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833) and binding the **CollectionViewSource** instances together in a chain. This is called a master/details (or list/details) view. For more info, see [How to bind to hierarchical data and create a master/details view](how-to-bind-to-hierarchical-data-and-create-a-master-details-view.md).

<span id="debugging"/>

## Diagnosing and debugging data binding problems

Your binding markup contains the names of properties (and, for C#, sometimes fields and methods). So when you rename a property, you'll also need to change any binding that references it. Forgetting to do that leads to a typical example of a data binding bug, and your app either won't compile or won't run correctly.

The binding objects created by [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) and [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) are largely functionally equivalent. But {x:Bind} has type information for the binding source, and it generates source code at compile-time. With {x:Bind} you get the same kind of problem detection that you get with the rest of your code. That includes compile-time validation of your binding expressions, and debugging by setting breakpoints in the source code generated as the partial class for your page. These classes can be found in the files in your `obj` folder, with names like (for C#) `<view name>.g.cs`). If you have a problem with a binding then turn on **Break On Unhandled Exceptions** in the Microsoft Visual Studio debugger. The debugger will break execution at that point, and you can then debug what has gone wrong. The code generated by {x:Bind} follows the same pattern for each part of the graph of binding source nodes, and you can use the info in the **Call Stack** window to help determine the sequence of calls that led up to the problem.

[{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782) does not have type information for the binding source. But when you run your app with the debugger attached, any binding errors appear in the **Output** window in Visual Studio.

## Creating bindings in code

**Note**  This section only applies to [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782), because you can't create [{x:Bind}](https://msdn.microsoft.com/library/windows/apps/Mt204783) bindings in code. However, some of the same benefits of {x:Bind} can be achieved with [**DependencyObject.RegisterPropertyChangedCallback**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.dependencyobject.registerpropertychangedcallback.aspx), which enables you to register for change notifications on any dependency property.

You can also connect UI elements to data using procedural code instead of XAML. To do this, create a new [**Binding**](https://msdn.microsoft.com/library/windows/apps/BR209820) object, set the appropriate properties, then call [**FrameworkElement.SetBinding**](https://msdn.microsoft.com/library/windows/apps/br244257.aspx) or [**BindingOperations.SetBinding**](https://msdn.microsoft.com/library/windows/apps/br244376.aspx). Creating bindings programmatically is useful when you want to choose the binding property values at run-time or share a single binding among multiple controls. Note, however, that you cannot change the binding property values after you call **SetBinding**.

The following example shows how to implement a binding in code.

```xml
<TextBox x:Name="MyTextBox" Text="Text"/>
```

```csharp
// Create an instance of the MyColors class 
// that implements INotifyPropertyChanged.
MyColors textcolor = new MyColors();

// Brush1 is set to be a SolidColorBrush with the value Red.
textcolor.Brush1 = new SolidColorBrush(Colors.Red);

// Set the DataContext of the TextBox MyTextBox.
MyTextBox.DataContext = textcolor;

// Create the binding and associate it with the text box.
Binding binding = new Binding() { Path = new PropertyPath("Brush1") };
MyTextBox.SetBinding(TextBox.ForegroundProperty, binding);
```

```vbnet
' Create an instance of the MyColors class 
' that implements INotifyPropertyChanged. 
Dim textcolor As New MyColors()

' Brush1 is set to be a SolidColorBrush with the value Red. 
textcolor.Brush1 = New SolidColorBrush(Colors.Red)

' Set the DataContext of the TextBox MyTextBox. 
MyTextBox.DataContext = textcolor

' Create the binding and associate it with the text box.
Dim binding As New Binding() With {.Path = New PropertyPath("Brush1")}
MyTextBox.SetBinding(TextBox.ForegroundProperty, binding)
```

## {x:Bind} and {Binding} feature comparison

| Feature | {x:Bind} | {Binding} | Notes |
|---------|----------|-----------|-------|
| Path is the default property | `{x:Bind a.b.c}` | `{Binding a.b.c}` | | 
| Path property | `{x:Bind Path=a.b.c}` | `{Binding Path=a.b.c}` | In x:Bind, Path is rooted at the Page by default, not the DataContext. | 
| Indexer | `{x:Bind Groups[2].Title}` | `{Binding Groups[2].Title}` | Binds to the specified item in the collection. Only integer-based indexes are supported. | 
| Attached properties | `{x:Bind Button22.(Grid.Row)}` | `{Binding Button22.(Grid.Row)}` | Attached properties are specified using parentheses. If the property is not declared in a XAML namespace, then prefix it with an xml namespace, which should be mapped to a code namespace at the head of the document. | 
| Casting | `{x:Bind groups[0].(data:SampleDataGroup.Title)}` | Not needed. | Casts are specified using parentheses. If the property is not declared in a XAML namespace, then prefix it with an xml namespace, which should be mapped to a code namespace at the head of the document. | 
| Converter | `{x:Bind IsShown, Converter={StaticResource BoolToVisibility}}` | `{Binding IsShown, Converter={StaticResource BoolToVisibility}}` | Converters must be declared at the root of the Page/ResourceDictionary, or in App.xaml. | 
| ConverterParameter, ConverterLanguage | `{x:Bind IsShown, Converter={StaticResource BoolToVisibility}, ConverterParameter=One, ConverterLanguage=fr-fr}` | `{Binding IsShown, Converter={StaticResource BoolToVisibility}, ConverterParameter=One, ConverterLanguage=fr-fr}` | Converters must be declared at the root of the Page/ResourceDictionary, or in App.xaml. | 
| TargetNullValue | `{x:Bind Name, TargetNullValue=0}` | `{Binding Name, TargetNullValue=0}` | Used when the leaf of the binding expression is null. Use single quotes for a string value. | 
| FallbackValue | `{x:Bind Name, FallbackValue='empty'}` | `{Binding Name, FallbackValue='empty'}` | Used when any part of the path for the binding (except for the leaf) is null. | 
| ElementName | `{x:Bind slider1.Value}` | `{Binding Value, ElementName=slider1}` | With {x:Bind} you're binding to a field; Path is rooted at the Page by default, so any named element can be accessed via its field. | 
| RelativeSource: Self | `<Rectangle x:Name="rect1" Width="200" Height="{x:Bind rect1.Width}" ... />` | `<Rectangle Width="200" Height="{Binding Width, RelativeSource={RelativeSource Self}}" ... />` | With {x:Bind}, name the element and use its name in Path. | 
| RelativeSource: TemplatedParent | Not supported | `{Binding <path>, RelativeSource={RelativeSource TemplatedParent}}` | Regular template binding can be used in control templates for most uses. But use TemplatedParent where you need to use a converter, or a two-way binding.< | 
| Source | Not supported | `<ListView ItemsSource="{Binding Orders, Source={StaticResource MyData}}"/>` | For {x:Bind} use a property or a static path instead. | 
| Mode | `{x:Bind Name, Mode=OneWay}` | `{Binding Name, Mode=TwoWay}` | Mode can be OneTime, OneWay, or TwoWay. {x:Bind} defaults to OneTime; {Binding} defaults to OneWay. | 
| UpdateSourceTrigger | `{x:Bind Name, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}` | `{Binding UpdateSourceTrigger=PropertyChanged}` | UpdateSourceTrigger can be Default, LostFocus, or PropertyChanged. {x:Bind} does not support UpdateSourceTrigger=Explicit. {x:Bind} uses PropertyChanged behavior for all cases except TextBox.Text, where it uses LostFocus behavior. | 


