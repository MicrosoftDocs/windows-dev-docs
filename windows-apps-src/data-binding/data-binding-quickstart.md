---
author: mcleblanc
ms.assetid: A9D54DEC-CD1B-4043-ADE4-32CD4977D1BF
title: Data binding overview
description: This topic shows you how to bind a control (or other UI element) to a single item or bind an item's control to a collection of items in a Universal Windows Platform (UWP) app.
ms.author: markl
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
Data binding overview
=====================



This topic shows you how to bind a control (or other UI element) to a single item or bind an items control to a collection of items in a Universal Windows Platform (UWP) app. In addition, we show how to control the rendering of items, implement a details view based on a selection, and convert data for display. For more detailed info, see [Data binding in depth](data-binding-in-depth.md).

Prerequisites
-------------------------------------------------------------------------------------------------------------

This topic assumes that you know how to create a basic UWP app. For instructions on creating your first UWP app, see [Get started with Windows apps](https://developer.microsoft.com/windows/getstarted).

Create the project
---------------------------------------------------------------------------------------------------------------------------------

Create a new **Blank Application (Windows Universal)** project. Name it "Quickstart".

Binding to a single item
---------------------------------------------------------------------------------------------------------------------------------------------------------

Every binding consists of a binding target and a binding source. Typically, the target is a property of a control or other UI element, and the source is a property of a class instance (a data model, or a view model). This example shows how to bind a control to a single item. The target is the **Text** property of a **TextBlock**. The source is an instance of a simple class named **Recording** that represents an audio recording. Let's look at the class first.

Add a new class to your project, name it Recording.cs (if you're using C#, C++ snippets provided below as well), and add this code to it.

> [!div class="tabbedCodeSnippets"]
```csharp
namespace Quickstart
{
    public class Recording
    {
        public string ArtistName { get; set; }
        public string CompositionName { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public Recording()
        {
            this.ArtistName = "Wolfgang Amadeus Mozart";
            this.CompositionName = "Andante in C for Piano";
            this.ReleaseDateTime = new DateTime(1761, 1, 1);
        }
        public string OneLineSummary
        {
            get
            {
                return $"{this.CompositionName} by {this.ArtistName}, released: "
                    + this.ReleaseDateTime.ToString("d");
            }
        }
    }
    public class RecordingViewModel
    {
        private Recording defaultRecording = new Recording();
        public Recording DefaultRecording { get { return this.defaultRecording; } }
    }
}
```
```cpp
    #include <sstream>
    namespace Quickstart
    {
        public ref class Recording sealed
        {
        private:
            Platform::String^ artistName;
            Platform::String^ compositionName;
            Windows::Globalization::Calendar^ releaseDateTime;
        public:
            Recording(Platform::String^ artistName, Platform::String^ compositionName,
                Windows::Globalization::Calendar^ releaseDateTime) :
                artistName{ artistName },
                compositionName{ compositionName },
                releaseDateTime{ releaseDateTime } {}
            property Platform::String^ ArtistName
            {
                Platform::String^ get() { return this->artistName; }
            }
            property Platform::String^ CompositionName
            {
                Platform::String^ get() { return this->compositionName; }
            }
            property Windows::Globalization::Calendar^ ReleaseDateTime
            {
                Windows::Globalization::Calendar^ get() { return this->releaseDateTime; }
            }
            property Platform::String^ OneLineSummary
            {
                Platform::String^ get()
                {
                    std::wstringstream wstringstream;
                    wstringstream << this->CompositionName->Data();
                    wstringstream << L" by " << this->ArtistName->Data();
                    wstringstream << L", released: " << this->ReleaseDateTime->MonthAsNumericString()->Data();
                    wstringstream << L"/" << this->ReleaseDateTime->DayAsString()->Data();
                    wstringstream << L"/" << this->ReleaseDateTime->YearAsString()->Data();
                    return ref new Platform::String(wstringstream.str().c_str());
                }
            }
        };
        public ref class RecordingViewModel sealed
        {
        private:
            Recording^ defaultRecording;
        public:
            RecordingViewModel()
            {
                Windows::Globalization::Calendar^ releaseDateTime = ref new Windows::Globalization::Calendar();
                releaseDateTime->Month = 1;
                releaseDateTime->Day = 1;
                releaseDateTime->Year = 1761;
                this->defaultRecording = ref new Recording{ L"Wolfgang Amadeus Mozart", L"Andante in C for Piano", releaseDateTime };
            }
            property Recording^ DefaultRecording
            {
                Recording^ get() { return this->defaultRecording; };
            }
        };
    }
```

Next, expose the binding source class from the class that represents your page of markup. We do that by adding a property of type **RecordingViewModel** to **MainPage**.

> [!div class="tabbedCodeSnippets"]
```csharp
    namespace Quickstart
    {
        public sealed partial class MainPage : Page
        {
            public MainPage()
            {
                this.InitializeComponent();
                this.ViewModel = new RecordingViewModel();
            }
            public RecordingViewModel ViewModel { get; set; }
        }
    }
```
```cpp
    namespace Quickstart
    {
        public ref class MainPage sealed
        {
        private:
            RecordingViewModel^ viewModel;
        public:
            MainPage()
            {
                InitializeComponent();
                this->viewModel = ref new RecordingViewModel();
            }
            property RecordingViewModel^ ViewModel
            {
                RecordingViewModel^ get() { return this->viewModel; };
            }
        };
    }
```

The last piece is to bind a **TextBlock** to the **ViewModel.DefaultRecording.OneLiner** property.

```xml
    <Page x:Class="Quickstart.MainPage" ... >
        <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
            <TextBlock Text="{x:Bind ViewModel.DefaultRecording.OneLineSummary}"
            HorizontalAlignment="Center"
            VerticalAlignment="Center"/>
        </Grid>
    </Page>
```

Here's the result.

![Binding a textblock](images/xaml-databinding0.png)

Binding to a collection of items
------------------------------------------------------------------------------------------------------------------

A common scenario is to bind to a collection of business objects. In C# and Visual Basic, the generic [**ObservableCollection&lt;T&gt;**](https://msdn.microsoft.com/library/windows/apps/xaml/ms668604.aspx) class is a good collection choice for data binding, because it implements the [**INotifyPropertyChanged**](https://msdn.microsoft.com/library/windows/apps/xaml/system.componentmodel.inotifypropertychanged.aspx) and [**INotifyCollectionChanged**](https://msdn.microsoft.com/library/windows/apps/xaml/system.collections.specialized.inotifycollectionchanged.aspx) interfaces. These interfaces provide change notification to bindings when items are added or removed or a property of the list itself changes. If you want your bound controls to update with changes to properties of objects in the collection, the business object should also implement **INotifyPropertyChanged**. For more info, see [Data binding in depth](data-binding-in-depth.md).

This next example binds a [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878) to a collection of `Recording` objects. Let's start by adding the collection to our view model. Just add these new members to the **RecordingViewModel** class.

> [!div class="tabbedCodeSnippets"]
```csharp
    public class RecordingViewModel
    {
        ...
        private ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();
        public ObservableCollection<Recording> Recordings { get { return this.recordings; } }
        public RecordingViewModel()
        {
            this.recordings.Add(new Recording() { ArtistName = "Johann Sebastian Bach",
            CompositionName = "Mass in B minor", ReleaseDateTime = new DateTime(1748, 7, 8) });
            this.recordings.Add(new Recording() { ArtistName = "Ludwig van Beethoven",
            CompositionName = "Third Symphony", ReleaseDateTime = new DateTime(1805, 2, 11) });
            this.recordings.Add(new Recording() { ArtistName = "George Frideric Handel",
            CompositionName = "Serse", ReleaseDateTime = new DateTime(1737, 12, 3) });
        }
    }
```
```cpp
    public ref class RecordingViewModel sealed
    {
    private:
        ...
        Windows::Foundation::Collections::IVector<Recording^>^ recordings;
    public:
        RecordingViewModel()
        {
            ...
            releaseDateTime = ref new Windows::Globalization::Calendar();
            releaseDateTime->Month = 7;
            releaseDateTime->Day = 8;
            releaseDateTime->Year = 1748;
            Recording^ recording = ref new Recording{ L"Johann Sebastian Bach", L"Mass in B minor", releaseDateTime };
            this->Recordings->Append(recording);
            releaseDateTime = ref new Windows::Globalization::Calendar();
            releaseDateTime->Month = 2;
            releaseDateTime->Day = 11;
            releaseDateTime->Year = 1805;
            recording = ref new Recording{ L"Ludwig van Beethoven", L"Third Symphony", releaseDateTime };
            this->Recordings->Append(recording);
            releaseDateTime = ref new Windows::Globalization::Calendar();
            releaseDateTime->Month = 12;
            releaseDateTime->Day = 3;
            releaseDateTime->Year = 1737;
            recording = ref new Recording{ L"George Frideric Handel", L"Serse", releaseDateTime };
            this->Recordings->Append(recording);
        }
        ...
        property Windows::Foundation::Collections::IVector<Recording^>^ Recordings
        {
            Windows::Foundation::Collections::IVector<Recording^>^ get()
            {
                if (this->recordings == nullptr)
                {
                    this->recordings = ref new Platform::Collections::Vector<Recording^>();
                }
                return this->recordings;
            };
        }
    };
```

And then bind a [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878) to the **ViewModel.Recordings** property.

```xml
<Page x:Class="Quickstart.MainPage" ... >
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <ListView ItemsSource="{x:Bind ViewModel.Recordings}"
        HorizontalAlignment="Center" VerticalAlignment="Center"/>
    </Grid>
</Page>
```

We haven't yet provided a data template for the **Recording** class, so the best the UI framework can do is to call [**ToString**](https://msdn.microsoft.com/library/windows/apps/system.object.tostring.aspx) for each item in the [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878). The default implementation of **ToString** is to return the type name.

![Binding a list view](images/xaml-databinding1.png)

To remedy this we can either override [**ToString**](https://msdn.microsoft.com/library/windows/apps/system.object.tostring.aspx) to return the value of **OneLineSummary**, or we can provide a data template. The data template option is more common and arguably more flexible. You specify a data template by using the [**ContentTemplate**](https://msdn.microsoft.com/library/windows/apps/BR209369) property of a content control or the [**ItemTemplate**](https://msdn.microsoft.com/library/windows/apps/BR242830) property of an items control. Here are two ways we could design a data template for **Recording** together with an illustration of the result.

```xml
    <ListView ItemsSource="{x:Bind ViewModel.Recordings}"
        HorizontalAlignment="Center" VerticalAlignment="Center">
        <ListView.ItemTemplate>
            <DataTemplate x:DataType="local:Recording">
                <TextBlock Text="{x:Bind OneLineSummary}"/>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
```

![Binding a list view](images/xaml-databinding2.png)

```xml
    <ListView ItemsSource="{x:Bind ViewModel.Recordings}"
    HorizontalAlignment="Center" VerticalAlignment="Center">
        <ListView.ItemTemplate>
            <DataTemplate x:DataType="local:Recording">
                <StackPanel Orientation="Horizontal" Margin="6">
                    <SymbolIcon Symbol="Audio" Margin="0,0,12,0"/>
                    <StackPanel>
                        <TextBlock Text="{x:Bind ArtistName}" FontWeight="Bold"/>
                        <TextBlock Text="{x:Bind CompositionName}"/>
                    </StackPanel>
                </StackPanel>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
```

![Binding a list view](images/xaml-databinding3.png)

For more information about XAML syntax, see [Create a UI with XAML](https://msdn.microsoft.com/library/windows/apps/Mt228349). For more information about control layout, see [Define layouts with XAML](https://msdn.microsoft.com/library/windows/apps/Mt228350).

Adding a details view
-----------------------------------------------------------------------------------------------------

You can choose to display all the details of **Recording** objects in [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878) items. But that takes up a lot of space. Instead, you can show just enough data in the item to identify it and then, when the user makes a selection, you can display all the details of the selected item in a separate piece of UI known as the details view. This arrangement is also known as a master/details view, or a list/details view.

There are two ways to go about this. You can bind the details view to the [**SelectedItem**](https://msdn.microsoft.com/library/windows/apps/BR209770) property of the [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878). Or you can use a [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833): bind both the **ListView** and the details view to the **CollectionViewSource** (which will take care of the currently-selected item for you). Both techniques are shown below, and they both give the same results shown in the illustration.

> [!NOTE]
> So far in this topic we've only used the [{x:Bind} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204783), but both of the techniques we'll show below require the more flexible (but less performant) [{Binding} markup extension](https://msdn.microsoft.com/library/windows/apps/Mt204782).

First, here's the [**SelectedItem**](https://msdn.microsoft.com/library/windows/apps/BR209770) technique. If you're using Visual C++ component extensions (C++/CX) then, because we'll be using [{Binding}](https://msdn.microsoft.com/library/windows/apps/Mt204782), you'll need to add the [**BindableAttribute**](https://msdn.microsoft.com/library/windows/apps/Hh701872) attribute to the **Recording** class.

```cpp
    [Windows::UI::Xaml::Data::Bindable]
    public ref class Recording sealed
    {
        ...
    };
```

The only other change necessary is to the markup.

```xml
<Page x:Class="Quickstart.MainPage" ... >
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
            <ListView x:Name="recordingsListView" ItemsSource="{x:Bind ViewModel.Recordings}">
                <ListView.ItemTemplate>
                    <DataTemplate x:DataType="local:Recording">
                        <StackPanel Orientation="Horizontal" Margin="6">
                            <SymbolIcon Symbol="Audio" Margin="0,0,12,0"/>
                            <StackPanel>
                                <TextBlock Text="{x:Bind CompositionName}"/>
                            </StackPanel>
                        </StackPanel>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
            <StackPanel DataContext="{Binding SelectedItem, ElementName=recordingsListView}"
            Margin="0,24,0,0">
                <TextBlock Text="{Binding ArtistName}"/>
                <TextBlock Text="{Binding CompositionName}"/>
                <TextBlock Text="{Binding ReleaseDateTime}"/>
            </StackPanel>
        </StackPanel>
    </Grid>
</Page>
```

For the [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833) technique, first add a **CollectionViewSource** as a page resource.

```xml
    <Page.Resources>
        <CollectionViewSource x:Name="RecordingsCollection" Source="{x:Bind ViewModel.Recordings}"/>
    </Page.Resources>
```

And then adjust the bindings on the [**ListView**](https://msdn.microsoft.com/library/windows/apps/BR242878) (which no longer needs to be named) and on the details view to use the [**CollectionViewSource**](https://msdn.microsoft.com/library/windows/apps/BR209833). Note that by binding the details view directly to the **CollectionViewSource**, you're implying that you want to bind to the current item in bindings where the path cannot be found on the collection itself. There's no need to specify the **CurrentItem** property as the path for the binding, although you can do that if there's any ambiguity).

```xml
    ...

    <ListView ItemsSource="{Binding Source={StaticResource RecordingsCollection}}">

    ...

    <StackPanel DataContext="{Binding Source={StaticResource RecordingsCollection}}" ...>
    ...
```

And here's the identical result in each case.

![Binding a list view](images/xaml-databinding4.png)

Formatting or converting data values for display
--------------------------------------------------------------------------------------------------------------------------------------------

There is one small issue with the rendering above. The **ReleaseDateTime** property is not just a date, it's a [**DateTime**](https://msdn.microsoft.com/library/windows/apps/xaml/system.datetime.aspx), so it's being displayed with more precision than we need. One solution is to add a string property to the **Recording** class that returns `this.ReleaseDateTime.ToString("d")`. Naming that property **ReleaseDate** would indicate that it returns a date, not a date-and-time. Naming it **ReleaseDateAsString** would further indicate that it returns a string.

A more flexible solution is to use something known as a value converter. Here's an example of how to author your own value converter. Add this code to your Recording.cs source code file.

```csharp
public class StringFormatter : Windows.UI.Xaml.Data.IValueConverter
{
    // This converts the value object to the string to display.
    // This will work with most simple types.
    public object Convert(object value, Type targetType,
        object parameter, string language)
    {
        // Retrieve the format string and use it to format the value.
        string formatString = parameter as string;
        if (!string.IsNullOrEmpty(formatString))
        {
            return string.Format(formatString, value);
        }

        // If the format string is null or empty, simply
        // call ToString() on the value.
        return value.ToString();
    }

    // No need to implement converting back on a one-way binding
    public object ConvertBack(object value, Type targetType,
        object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
```

Now we can add an instance of **StringFormatter** as a page resource and use it in our binding. We pass the format string into the converter from markup for ultimate formatting flexibility.

```xml
    <Page.Resources>
        <local:StringFormatter x:Key="StringFormatterValueConverter"/>
    </Page.Resources>
    ...

    <TextBlock Text="{Binding ReleaseDateTime,
        Converter={StaticResource StringFormatterValueConverter},
        ConverterParameter=Released: \{0:d\}}"/>

    ...
```

Here's the result.

![displaying a date with custom formatting](images/xaml-databinding5.png)

> [!NOTE]
> Starting in Windows 10, version 1607, the XAML framework provides a built in boolean to Visibility converter. The converter maps **true** to the **Visible** enumeration value and **false** to **Collapsed** so you can bind a Visibility property to a boolean without creating a converter. To use the built in converter, your app's minimum target SDK version must be 14393 or later. You can't use it when your app targets earlier versions of Windows 10. For more info about target versions, see [Version adaptive code](https://msdn.microsoft.com/windows/uwp/debug-test-perf/version-adaptive-code).

## See also
- [Data binding](index.md)
