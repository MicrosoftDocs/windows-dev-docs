---
title: Learning track - Display customers in a list
description: Learn what you need to do to display a collection of Customer objects in a list.
ms.date: 05/07/2018
ms.topic: article
keywords: get started, uwp, windows 10, learning track, data binding, list
ms.localizationpriority: medium
ms.custom: RS5
---
# Display customers in a list

Displaying and manipulating real data in the UI is crucial to the functionality of many apps. This article will show you what you need to know to display a collection of Customer objects in a list.

This is not a tutorial. If you want one, see our [data binding tutorial](../data-binding/xaml-basics-data-binding.md), which will provide you with a step-by-step guided experience.

We’ll start with a quick discussion of data binding - what it is and how it works. Then we'll add a **ListView** to the UI, add data binding, and customize the data binding with additional features.

## What do you need to know?

Data binding is a way to display an app's data in its UI. This allows for *separation of concerns* in your app, keeping your UI separate from your other code. This creates a cleaner conceptual model that’s easier to read and maintain.

Every data binding has two pieces:

* A source which provides the data to be bound.
* A target in the UI where the data is displayed.

To implement a data binding, you'll need to add code to your source that provides data to the binding. You'll also need to add one of two markup extensions to your XAML to specify the data source properties. Here's the key difference between the two:

* [**x:Bind**](../xaml-platform/x-bind-markup-extension.md) is strongly typed, and generates code at compile time for better performance. x:Bind defaults to a one-time binding, which optimizes for the fast display of read-only data that doesn't change.
* [**Binding**](../xaml-platform/binding-markup-extension.md) is weakly typed and assembled at runtime. This results in poorer performance than with x:Bind. In almost all cases, you should use x:Bind instead of Binding. However, you're likely to encounter it in older code. Binding defaults to one-way data transfer, which optimizes for read-only data that can change at the source.

We recommend you use **x:Bind** whenever possible, and we'll be showing it in the snippets in this article. For more information on the differences, see [{x:Bind} and {Binding} feature comparison](../data-binding/data-binding-in-depth.md#xbind-and-binding-feature-comparison).

## Create a data source

First, you'll need a class to represent your Customer data. To give you a reference point, we'll be showing the process on this bare-bones example:

```csharp
public class Customer
{
    public string Name { get; set; }
}
```

## Create a list

Before you can display any customers, you need to create the list to hold them. The [List View](../design/controls-and-patterns/listview-and-gridview.md) is a basic XAML control which is ideal for this task. Your ListView currently requires a position on the page, and will shortly need a value for its **ItemSource** property.

```xaml
<ListView ItemsSource=""
    HorizontalAlignment="Center"
    VerticalAlignment="Center"/>
```

Once you have bound data to your ListView, we encourage you to return to the documentation, and experiment with customizing its appearance and layout to best fit your needs.

## Bind data to your list

Now that you've made a basic UI to hold your bindings, you need to configure your source to provide them. Here's an example of how this may be done:

```csharp
public sealed partial class MainPage : Page
{
    public ObservableCollection<Customer> Customers { get; }
        = new ObservableCollection<Customer>();

    public MainPage()
    {
        this.InitializeComponent();
          // Add some customers
        this.Customers.Add(new Customer() { Name = "NAME1"});
        this.Customers.Add(new Customer() { Name = "NAME2"});
        this.Customers.Add(new Customer() { Name = "NAME3"});
    }
}
```
```xaml
<ListView ItemsSource="{x:Bind Customers}"
    HorizontalAlignment="Center"
    VerticalAlignment="Center">
    <ListView.ItemTemplate>
        <DataTemplate x:DataType="local:Customer">
            <TextBlock Text="{x:Bind Name}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

The [Data Binding overview](../data-binding/data-binding-quickstart.md#binding-to-a-collection-of-items) walks you through a similar problem, in its section about binding to a collection of items. Our example here shows the following crucial steps:

* In the code-behind of your UI, create a property of type **ObservableCollection<T>** to hold your Customer objects.
* Bind your ListView’s **ItemSource** to that property.
* Provide a basic **ItemTemplate** for the ListView, which will configure how each item in the list is displayed.

Feel free to look back at the [List View](../design/controls-and-patterns/listview-and-gridview.md) docs if you want to customize layout, add item selection, or tweak the **DataTemplate** you just made. But what if you want to edit your Customers?

## Edit your Customers through the UI

You’ve displayed customers in a list, but data binding lets you do more. What if you could edit your data directly from the UI? To do this, let’s first talk about the three modes of data binding:

* *One-Time*: This data binding is only activated once, and doesn’t react to changes.
* *One-Way*: This data binding will update the UI with any changes made to the data source.
* *Two-Way*: This data binding will update the UI with any changes made to the data source, and also update the data with any changes made within the UI.

If you've followed the code snippets from earlier, the binding you made uses x:Bind and doesn't specify a mode, making it a One-Time binding. If you want to edit your Customers directly from the UI, you'll need to change it to a Two-Way binding, so that changes from the data will be passed back to the Customer objects. [Data binding in depth](../data-binding/data-binding-in-depth.md) has more information.

Two-way binding will also update the UI if the data source is changed. For this to work, you must implement [**INotifyPropertyChanged**](/dotnet/api/system.componentmodel.inotifypropertychanged) on the source and ensure its property setters raise the **PropertyChanged** event. Common practice is to have them call a helper method like the **OnPropertyChanged** method, as shown below:

```csharp
public class Customer : INotifyPropertyChanged
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged();
                }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```
Then make the text in your ListView editable by using a **TextBox** instead of a **TextBlock**, and ensure that you set the **Mode** on your data bindings to **TwoWay**.

```xaml
<ListView ItemsSource="{x:Bind Customers}"
    HorizontalAlignment="Center"
    VerticalAlignment="Center">
    <ListView.ItemTemplate>
        <DataTemplate x:DataType="local:Customer">
            <TextBox Text="{x:Bind Name, Mode=TwoWay}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

A quick way to ensure that this works is to add a second ListView with TextBox controls and OneWay bindings. The values in the second list will automatically change as you edit the first one.

> [!NOTE]
> Editing directly inside a ListView is a simple way to show Two-Way binding in action, but can lead to usability complications. If you're looking to take your app further, consider using [other XAML controls](../design/controls-and-patterns/controls-and-events-intro.md) to edit your data, and keep your ListView as display-only.

## Going Further

Now that you’ve created a list of customers with two-way binding, feel free to go back through the docs we’ve linked you to and experiment. You can also check out our [data binding tutorial](../data-binding/xaml-basics-data-binding.md) if you want a step-by-step walkthrough of basic and advanced bindings, or investigate controls like the [master/details pattern](../design/controls-and-patterns/master-details.md) to make a more robust UI.

## Useful APIs and docs

Here's a quick summary of APIs and other useful documentation to help you get started working with Data Binding.

### Useful APIs

| API | Description |
|------|---------------|
| [Data template](/uwp/api/Windows.UI.Xaml.DataTemplate) | Describes the visual structure of a data object, allowing for the display of specific elements in the UI. |
| [x:Bind](../xaml-platform/x-bind-markup-extension.md) | Documentation on the recommended x:Bind markup extension. |
| [Binding](../xaml-platform/binding-markup-extension.md) | Documentation on the older Binding markup extension. |
| [ListView](/uwp/api/Windows.UI.Xaml.Controls.ListView) | A UI control that displays data items in a vertical stack. |
| [TextBox](/uwp/api/Windows.UI.Xaml.Controls.TextBox) | A basic text control for displaying editable text data in the UI. |
| [INotifyPropertyChanged](/dotnet/api/system.componentmodel.inotifypropertychanged) | The interface for making data observable, providing it to a data binding. |
| [ItemsControl](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl) | The **ItemsSource** property of this class allows a ListView to bind to a data source. |

### Useful docs

| Topic | Description |
|-------|----------------|
| [Data binding in depth](../data-binding/data-binding-in-depth.md) | A basic overview of data binding principles |
| [Data Binding overview](../data-binding/data-binding-quickstart.md) | Detailed conceptual information on data binding. |
| [List View](../design/controls-and-patterns/listview-and-gridview.md) | Information on creating and configuring a ListView, including implementation of a **DataTemplate** |

## Useful code samples

| Code sample | Description |
|-----------------|---------------|
| [Data binding tutorial](../data-binding/xaml-basics-data-binding.md) | A step-by-step guided experience through the basics of data binding. |
| [ListView and GridView](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlListView) | Explore more elaborate ListViews with data binding. |
| [QuizGame](https://github.com/Microsoft/Windows-appsample-networkhelper) | See data binding in action, including the **BindableBase** class (in the "Common" folder) for a standard implementation of **INotifyPropertyChanged**. |