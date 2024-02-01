---
description: Explains how to define a ResourceDictionary element and keyed resources, and how XAML resources relate to other resources that you define as part of your app or app package.
MS-HAID: dev\_ctrl\_layout\_txt.resourcedictionary\_and\_xaml\_resource\_references
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: ResourceDictionary and XAML resource references
ms.assetid: E3CBFA3D-6AF5-44E1-B9F9-C3D3EA8A25CE
label: ResourceDictionary and XAML resource references
template: detail.hbs
ms.date: 01/24/2022
ms.topic: conceptual
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# ResourceDictionary and XAML resource references

You can define the UI or resources for your app using XAML. Resources are typically definitions of some object that you expect to use more than once. To refer to a XAML resource later, you specify a key for a resource that acts like its name. You can reference a resource throughout an app or from any XAML page within it. You can define your resources using a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) element from the Windows Runtime XAML. Then, you can reference your resources by using a [StaticResource markup extension](/windows/uwp/xaml-platform/staticresource-markup-extension) or [ThemeResource markup extension](/windows/uwp/xaml-platform/themeresource-markup-extension).

The XAML elements you might want to declare most often as XAML resources include [Style](/uwp/api/Windows.UI.Xaml.Style), [ControlTemplate](/uwp/api/Windows.UI.Xaml.Controls.ControlTemplate), animation components, and [Brush](/uwp/api/Windows.UI.Xaml.Media.Brush) subclasses. Here, we explain how to define a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) and keyed resources, and how XAML resources relate to other resources that you define as part of your app or app package. We also explain resource dictionary advanced features such as [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries) and [ThemeDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries).

## Prerequisites

A solid understanding of XAML markup. We recommend reading [XAML overview](/windows/uwp/xaml-platform/xaml-overview).

## Define and use XAML resources

XAML resources are objects that are referenced from markup more than once. Resources are defined in a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), typically in a separate file or at the top of the markup page, like this.

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Page.Resources>
        <x:String x:Key="greeting">Hello world</x:String>
        <x:String x:Key="goodbye">Goodbye world</x:String>
    </Page.Resources>

    <TextBlock Text="{StaticResource greeting}" Foreground="Gray" VerticalAlignment="Center"/>
</Page>
```

In this example:

-   `<Page.Resources>…</Page.Resources>` - Defines the resource dictionary.
-   `<x:String>` - Defines the resource with the key "greeting".
-   `{StaticResource greeting}` - Looks up the resource with the key "greeting", which is assigned to the [Text](/uwp/api/windows.ui.xaml.controls.textblock.text) property of the [TextBlock](/uwp/api/Windows.UI.Xaml.Controls.TextBlock).

> **Note**&nbsp;&nbsp;Don't confuse the concepts related to [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) with the **Resource** build action, resource (.resw) files, or other "resources" that are discussed in the context of structuring the code project that produces your app package.

Resources don't have to be strings; they can be any shareable object, such as styles, templates, brushes, and colors. However, controls, shapes, and other [FrameworkElement](/uwp/api/Windows.UI.Xaml.FrameworkElement)s are not shareable, so they can't be declared as reusable resources. For more info about sharing, see the [XAML resources must be shareable](#xaml-resources-must-be-shareable) section later in this topic.

Here, both a brush and a string are declared as resources and used by controls in a page.

```XAML
<Page
    x:Class="SpiderMSDN.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Page.Resources>
        <SolidColorBrush x:Key="myFavoriteColor" Color="green"/>
        <x:String x:Key="greeting">Hello world</x:String>
    </Page.Resources>

    <TextBlock Foreground="{StaticResource myFavoriteColor}" Text="{StaticResource greeting}" VerticalAlignment="Top"/>
    <Button Foreground="{StaticResource myFavoriteColor}" Content="{StaticResource greeting}" VerticalAlignment="Center"/>
</Page>
```

All resources need to have a key. Usually that key is a string defined with `x:Key="myString"`. However, there are a few other ways to specify a key:

-   [Style](/uwp/api/Windows.UI.Xaml.Style) and [ControlTemplate](/uwp/api/Windows.UI.Xaml.Controls.ControlTemplate) require a **TargetType**, and will use the **TargetType** as the key if [x:Key](/windows/uwp/xaml-platform/x-key-attribute) is not specified. In this case, the key is the actual Type object, not a string. (See examples below)
-   [DataTemplate](/uwp/api/Windows.UI.Xaml.DataTemplate) resources that have a **TargetType** will use the **TargetType** as the key if [x:Key](/windows/uwp/xaml-platform/x-key-attribute) is not specified. In this case, the key is the actual Type object, not a string.
-   [x:Name](/windows/uwp/xaml-platform/x-name-attribute) can be used instead of [x:Key](/windows/uwp/xaml-platform/x-key-attribute). However, x:Name also generates a code behind field for the resource. As a result, x:Name is less efficient than x:Key because that field needs to be initialized when the page is loaded.

The [StaticResource markup extension](/windows/uwp/xaml-platform/staticresource-markup-extension) can retrieve resources only with a string name ([x:Key](/windows/uwp/xaml-platform/x-key-attribute) or [x:Name](/windows/uwp/xaml-platform/x-name-attribute)). However, the XAML framework also looks for implicit style resources (those which use **TargetType** rather than x:Key or x:Name) when it decides which style & template to use for a control that hasn't set the [Style](/uwp/api/windows.ui.xaml.frameworkelement.style) and [ContentTemplate](/uwp/api/windows.ui.xaml.controls.contentcontrol.contenttemplate) or [ItemTemplate](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemtemplate) properties.

Here, the [Style](/uwp/api/Windows.UI.Xaml.Style) has an implicit key of **typeof(Button)**, and since the [Button](/uwp/api/Windows.UI.Xaml.Controls.Button) at the bottom of the page doesn't specify a [Style](/uwp/api/windows.ui.xaml.frameworkelement.style) property, it looks for a style with key of **typeof(Button)**:

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Page.Resources>
        <Style TargetType="Button">
            <Setter Property="Background" Value="Red"/>
        </Style>
    </Page.Resources>
    <Grid>
       <!-- This button will have a red background. -->
       <Button Content="Button" Height="100" VerticalAlignment="Center" Width="100"/>
    </Grid>
</Page>
```

For more info about implicit styles and how they work, see [Styling controls](xaml-styles.md) and [Control templates](xaml-control-templates.md).

## Look up resources in code

You access members of the resource dictionary like any other dictionary.

> [!WARNING]
> When you perform a resource lookup in code, only the resources in the `Page.Resources` dictionary are looked at. Unlike the [StaticResource markup extension](/windows/uwp/xaml-platform/staticresource-markup-extension), the code doesn't fall back to the `Application.Resources` dictionary if the resources aren't found in the first dictionary.

 

This example shows how to retrieve the `redButtonStyle` resource out of a page's resource dictionary:

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Page.Resources>
        <Style TargetType="Button" x:Key="redButtonStyle">
            <Setter Property="Background" Value="red"/>
        </Style>
    </Page.Resources>
</Page>
```

```csharp
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Style redButtonStyle = (Style)this.Resources["redButtonStyle"];
        }
    }
```
```cppwinrt
    MainPage::MainPage()
    {
        InitializeComponent();
        Windows::UI::Xaml::Style style = Resources().TryLookup(winrt::box_value(L"redButtonStyle")).as<Windows::UI::Xaml::Style>();
    }
```

To look up app-wide resources from code, use **Application.Current.Resources** to get the app's resource dictionary, as shown here.

```XAML
<Application
    x:Class="MSDNSample.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:SpiderMSDN">
    <Application.Resources>
        <Style TargetType="Button" x:Key="appButtonStyle">
            <Setter Property="Background" Value="red"/>
        </Style>
    </Application.Resources>

</Application>
```

```csharp
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Style appButtonStyle = (Style)Application.Current.Resources["appButtonStyle"];
        }
    }
```
```cppwinrt
    MainPage::MainPage()
    {
        InitializeComponent();
        Windows::UI::Xaml::Style style = Application::Current().Resources()
                                                               .TryLookup(winrt::box_value(L"appButtonStyle"))
                                                               .as<Windows::UI::Xaml::Style>();
    }
```

You can also add an application resource in code.

There are two things to keep in mind when doing this.

-   First, you need to add the resources before any page tries to use the resource.
-   Second, you can't add resources in the App's constructor.

You can avoid both problems if you add the resource in the [Application.OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method, like this.

```csharp
// App.xaml.cs
    
sealed partial class App : Application
{
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        Frame rootFrame = Window.Current.Content as Frame;
        if (rootFrame == null)
        {
            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0)); // green
            this.Resources["brush"] = brush;
            // … Other code that VS generates for you …
        }
    }
}
```
```cppwinrt
// App.cpp

void App::OnLaunched(LaunchActivatedEventArgs const& e)
{
    Frame rootFrame{ nullptr };
    auto content = Window::Current().Content();
    if (content)
    {
        rootFrame = content.try_as<Frame>();
    }

    // Do not repeat app initialization when the Window already has content,
    // just ensure that the window is active
    if (rootFrame == nullptr)
    {
        Windows::UI::Xaml::Media::SolidColorBrush brush{ Windows::UI::ColorHelper::FromArgb(255, 0, 255, 0) };
        Resources().Insert(winrt::box_value(L"brush"), winrt::box_value(brush));
        // … Other code that VS generates for you …
```

## Every FrameworkElement can have a ResourceDictionary

[FrameworkElement](/uwp/api/Windows.UI.Xaml.FrameworkElement) is a base class that controls inherit from, and it has a [Resources](/uwp/api/windows.ui.xaml.frameworkelement.resources) property. So, you can add a local resource dictionary to any **FrameworkElement**.

Here, both the [Page](/uwp/api/Windows.UI.Xaml.Controls.Page) and the [Border](/uwp/api/Windows.UI.Xaml.Controls.Border) have resource dictionaries, and they both have a resource called "greeting". The [TextBlock](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) named 'textBlock2' is inside the **Border**, so its resource lookup looks first to the **Border**'s resources, then the **Page**'s resources, and then the [Application](/uwp/api/Windows.UI.Xaml.Application) resources. The **TextBlock** will read "Hola mundo".

To access that element's resources from code, use that element's [Resources](/uwp/api/windows.ui.xaml.frameworkelement.resources) property. Accessing a [FrameworkElement](/uwp/api/Windows.UI.Xaml.FrameworkElement)'s resources in code, rather than XAML, will look only in that dictionary, not in parent element's dictionaries.

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Page.Resources>
        <x:String x:Key="greeting">Hello world</x:String>
    </Page.Resources>
    
    <StackPanel>
        <!-- Displays "Hello world" -->
        <TextBlock x:Name="textBlock1" Text="{StaticResource greeting}"/>

        <Border x:Name="border">
            <Border.Resources>
                <x:String x:Key="greeting">Hola mundo</x:String>
            </Border.Resources>
            <!-- Displays "Hola mundo" -->
            <TextBlock x:Name="textBlock2" Text="{StaticResource greeting}"/>
        </Border>

        <!-- Displays "Hola mundo", set in code. -->
        <TextBlock x:Name="textBlock3"/>
    </StackPanel>
</Page>

```

```csharp
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            textBlock3.Text = (string)border.Resources["greeting"];
        }
    }
```
```cppwinrt
    MainPage::MainPage()
    {
        InitializeComponent();
        textBlock3().Text(unbox_value<hstring>(border().Resources().TryLookup(winrt::box_value(L"greeting"))));
    }
```

## Merged resource dictionaries

A *merged resource dictionary* combines one resource dictionary into another, usually in another file.

> **Tip**&nbsp;&nbsp;You can create a resource dictionary file in Microsoft Visual Studio by using the **Add &gt; New Item… &gt; Resource Dictionary** option from the **Project** menu.

Here, you define a resource dictionary in a separate XAML file called Dictionary1.xaml.

```XAML
<!-- Dictionary1.xaml -->
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MSDNSample">

    <SolidColorBrush x:Key="brush" Color="Red"/>

</ResourceDictionary>

```

To use that dictionary, you merge it with your page's dictionary:

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Page.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml"/>
            </ResourceDictionary.MergedDictionaries>

            <x:String x:Key="greeting">Hello world</x:String>

        </ResourceDictionary>
    </Page.Resources>

    <TextBlock Foreground="{StaticResource brush}" Text="{StaticResource greeting}" VerticalAlignment="Center"/>
</Page>
```

Here's what happens in this example. In `<Page.Resources>`, you declare `<ResourceDictionary>`. The XAML framework implicitly creates a resource dictionary for you when you add resources to `<Page.Resources>`; however, in this case, you don't want just any resource dictionary, you want one that contains merged dictionaries.

So you declare `<ResourceDictionary>`, then add things to its `<ResourceDictionary.MergedDictionaries>` collection. Each of those entries takes the form `<ResourceDictionary Source="Dictionary1.xaml"/>`. To add more than one dictionary, just add a `<ResourceDictionary Source="Dictionary2.xaml"/>` entry after the first entry.

After `<ResourceDictionary.MergedDictionaries>…</ResourceDictionary.MergedDictionaries>`, you can optionally put additional resources in your main dictionary. You use resources from a merged to dictionary just like a regular dictionary. In the example above, `{StaticResource brush}` finds the resource in the child/merged dictionary (Dictionary1.xaml), while `{StaticResource greeting}` finds its resource in the main page dictionary.

In the resource-lookup sequence, a [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries) dictionary is checked only after a check of all the other keyed resources of that [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary). After searching that level, the lookup reaches the merged dictionaries, and each item in **MergedDictionaries** is checked. If multiple merged dictionaries exist, these dictionaries are checked in the inverse of the order in which they are declared in the **MergedDictionaries** property. In the following example, if both Dictionary2.xaml and Dictionary1.xaml declared the same key, the key from Dictionary2.xaml is used first because it's last in the **MergedDictionaries** set.

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Page.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml"/>
                <ResourceDictionary Source="Dictionary2.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Page.Resources>

    <TextBlock Foreground="{StaticResource brush}" Text="greetings!" VerticalAlignment="Center"/>
</Page>
```

Within the scope of any one [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), the dictionary is checked for key uniqueness. However, that scope does not extend across different items in different [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries) files.

You can use the combination of the lookup sequence and lack of unique key enforcement across merged-dictionary scopes to create a fallback value sequence of [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) resources. For example, you might store user preferences for a particular brush color in the last merged resource dictionary in the sequence, using a resource dictionary that synchronizes to your app's state and user preference data. However, if no user preferences exist yet, you can define that same key string for a **ResourceDictionary** resource in the initial [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries) file, and it can serve as the fallback value. Remember that any value you provide in a primary resource dictionary is always checked before the merged dictionaries are checked, so if you want to use the fallback technique, don't define that resource in a primary resource dictionary.

## Theme resources and theme dictionaries

A [ThemeResource](/windows/uwp/xaml-platform/themeresource-markup-extension) is similar to a [StaticResource](/windows/uwp/xaml-platform/staticresource-markup-extension), but the resource lookup is reevaluated when the theme changes.

In this example, you set the foreground of a [TextBlock](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) to a value from the current theme.

```XAML
<TextBlock Text="hello world" Foreground="{ThemeResource FocusVisualWhiteStrokeThemeBrush}" VerticalAlignment="Center"/>
```

A theme dictionary is a special type of merged dictionary that holds the resources that vary with the theme a user is currently using on his or her device. For example, the "light" theme might use a white color brush whereas the "dark" theme might use a dark color brush. The brush changes the resource that it resolves to, but otherwise the composition of a control that uses the brush as a resource could be the same. To reproduce the theme-switching behavior in your own templates and styles, instead of using [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries) as the property to merge items into the main dictionaries, use the [ThemeDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) property.

Each [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) element within [ThemeDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries) must have an [x:Key](/windows/uwp/xaml-platform/x-key-attribute) value. The value is a string that names the relevant theme—for example, "Default", "Dark", "Light", or "HighContrast". Typically, `Dictionary1` and `Dictionary2` will define resources that have the same names but different values.

Here, you use red text for the light theme and blue text for the dark theme.

```XAML
<!-- Dictionary1.xaml -->
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MSDNSample">

    <SolidColorBrush x:Key="brush" Color="Red"/>

</ResourceDictionary>

<!-- Dictionary2.xaml -->
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MSDNSample">

    <SolidColorBrush x:Key="brush" Color="blue"/>

</ResourceDictionary>
```

In this example, you set the foreground of a [TextBlock](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) to a value from the current theme.

```XAML
<Page
    x:Class="MSDNSample.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Page.Resources>
        <ResourceDictionary>
            <ResourceDictionary.ThemeDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml" x:Key="Light"/>
                <ResourceDictionary Source="Dictionary2.xaml" x:Key="Dark"/>
            </ResourceDictionary.ThemeDictionaries>
        </ResourceDictionary>
    </Page.Resources>
    <TextBlock Foreground="{StaticResource brush}" Text="hello world" VerticalAlignment="Center"/>
</Page>
```

For theme dictionaries, the active dictionary to be used for resource lookup changes dynamically, whenever [ThemeResource markup extension](/windows/uwp/xaml-platform/themeresource-markup-extension) is used to make the reference and the system detects a theme change. The lookup behavior that is done by the system is based on mapping the active theme to the [x:Key](/windows/uwp/xaml-platform/x-key-attribute) of a specific theme dictionary.

It can be useful to examine the way that the theme dictionaries are structured in the default XAML design resources, which parallel the templates that the Windows Runtime uses by default for its controls. Open the XAML files in \\(Program Files)\\Windows Kits\\10\\DesignTime\\CommonConfiguration\\Neutral\\UAP\\&lt;SDK version&gt;\\Generic using a text editor or your IDE. Note how the theme dictionaries are defined first in generic.xaml, and how each theme dictionary defines the same keys. Each such key is then referenced by elements of composition in the various keyed elements that are outside the theme dictionaries and defined later in the XAML. There's also a separate themeresources.xaml file for design that contains only the theme resources and extra templates, not the default control templates. The theme areas are duplicates of what you'd see in generic.xaml.

When you use XAML design tools to edit copies of styles and templates, the design tools extract sections from the XAML design resource dictionaries and place them as local copies of XAML dictionary elements that are part of your app and project.

For more info and for a list of the theme-specific and system resources that are available to your app, see [XAML theme resources](xaml-theme-resources.md).

## Lookup behavior for XAML resource references

*Lookup behavior* is the term that describes how the XAML resources system tries to find a XAML resource. The lookup occurs when a key is referenced as a XAML resource reference from somewhere in the app's XAML. First, the resources system has predictable behavior for where it will check for the existence of a resource based on scope. If a resource isn't found in the initial scope, the scope expands. The lookup behavior continues on throughout the locations and scopes that a XAML resource could possibly be defined by an app or by the system. If all possible resource lookup attempts fail, an error often results. It's usually possible to eliminate these errors during the development process.

The lookup behavior for XAML resource references starts with the object where the actual usage is applied and its own [Resources](/uwp/api/windows.ui.xaml.frameworkelement.resources) property. If a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) exists there, that **ResourceDictionary** is checked for an item that has the requested key. This first level of lookup is rarely relevant because you usually do not define and then reference a resource on the same object. In fact, a **Resources** property often doesn't exist here. You can make XAML resource references from nearly anywhere in XAML; you aren't limited to properties of [FrameworkElement](/uwp/api/Windows.UI.Xaml.FrameworkElement) subclasses.

The lookup sequence then checks the next parent object in the runtime object tree of the app. If a [FrameworkElement.Resources](/uwp/api/windows.ui.xaml.frameworkelement.resources) exists and holds a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), the dictionary item with the specified key string is requested. If the resource is found, the lookup sequence stops and the object is provided to the location where the reference was made. Otherwise, the lookup behavior advances to the next parent level towards the object tree root. The search continues recursively upwards until the root element of the XAML is reached, exhausting the search of all possible immediate resource locations.

> [!NOTE]
> It is a common practice to define all the immediate resources at the root level of a page, both to take advantage of this resource-lookup behavior and also as a convention of XAML markup style.

If the requested resource is not found in the immediate resources, the next lookup step is to check the [Application.Resources](/uwp/api/windows.ui.xaml.application.resources) property. **Application.Resources** is the best place to put any app-specific resources that are referenced by multiple pages in your app's navigation structure.

> [!IMPORTANT]
> The order of resources added to a ResourceDictionary affects the order in which they are applied. The `XamlControlsResources` dictionary overrides many default resource keys and should therefore be added to `Application.Resources` first so that it doesn't override any other custom styles or resources in your app.

Control templates have another possible location in the reference lookup: theme dictionaries. A theme dictionary is a single XAML file that has a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) element as its root. The theme dictionary might be a merged dictionary from [Application.Resources](/uwp/api/windows.ui.xaml.application.resources). The theme dictionary might also be the control-specific theme dictionary for a templated custom control.

Finally, there is a resource lookup against platform resources. Platform resources include the control templates that are defined for each of the system UI themes, and which define the default appearance of all the controls that you use for UI in a Windows Runtime app. Platform resources also include a set of named resources that relate to system-wide appearance and themes. These resources are technically a [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries) item, and thus are available for lookup from XAML or code once the app has loaded. For example, the system theme resources include a resource named "SystemColorWindowTextColor" that provides a [Color](/uwp/api/Windows.UI.Color) definition to match app text color to a system window's text color that comes from the operating system and user preferences. Other XAML styles for your app can refer to this style, or your code can get a resource lookup value (and cast it to **Color** in the example case).

For more info and for a list of the theme-specific and system resources that are available to a Windows app that uses XAML, see [XAML theme resources](xaml-theme-resources.md).

If the requested key is still not found in any of these locations, a XAML parsing error/exception occurs. In certain circumstances, the XAML parse exception may be a run-time exception that is not detected either by a XAML markup compile action, or by a XAML design environment.

Because of the tiered lookup behavior for resource dictionaries, you can deliberately define multiple resource items that each have the same string value as the key, as long as each resource is defined at a different level. In other words, although keys must be unique within any given [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), the uniqueness requirement does not extend to the lookup behavior sequence as a whole. During lookup, only the first such object that's successfully retrieved is used for the XAML resource reference, and then the lookup stops. You could use this behavior to request the same XAML resource by key at various positions within your app's XAML but get different resources back, depending on the scope from which the XAML resource reference was made and how that particular lookup behaves.

##  Forward references within a ResourceDictionary


XAML resource references within a particular resource dictionary must reference a resource that has already been defined with a key, and that resource must appear lexically before the resource reference. Forward references cannot be resolved by a XAML resource reference. For this reason, if you use XAML resource references from within another resource, you must design your resource dictionary structure so that the resources that are used by other resources are defined first in a resource dictionary.

Resources defined at the app level cannot make references to immediate resources. This is equivalent to attempting a forward reference, because the app resources are actually processed first (when the app first starts, and before any navigation-page content is loaded). However, any immediate resource can make a reference to an app resource, and this can be a useful technique for avoiding forward-reference situations.

## XAML resources must be shareable


For an object to exist in a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), that object must be *shareable*.

Being shareable is required because, when the object tree of an app is constructed and used at run time, objects cannot exist at multiple locations in the tree. Internally, the resource system creates copies of resource values to use in the object graph of your app when each XAML resource is requested.

A [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) and Windows Runtime XAML in general supports these objects for shareable usage:

-   Styles and templates ([Style](/uwp/api/Windows.UI.Xaml.Style) and classes derived from [FrameworkTemplate](/uwp/api/Windows.UI.Xaml.FrameworkTemplate))
-   Brushes and colors (classes derived from [Brush](/uwp/api/Windows.UI.Xaml.Media.Brush), and [Color](/uwp/api/Windows.UI.Color) values)
-   Animation types including [Storyboard](/uwp/api/Windows.UI.Xaml.Media.Animation.Storyboard)
-   Transforms (classes derived from [GeneralTransform](/uwp/api/Windows.UI.Xaml.Media.GeneralTransform))
-   [Matrix](/uwp/api/Windows.UI.Xaml.Media.Matrix) and [Matrix3D](/uwp/api/Windows.UI.Xaml.Media.Media3D.Matrix3D)
-   [Point](/uwp/api/Windows.Foundation.Point) values
-   Certain other UI-related structures such as [Thickness](/uwp/api/Windows.UI.Xaml.Thickness) and [CornerRadius](/uwp/api/Windows.UI.Xaml.CornerRadius)
-   [XAML intrinsic data types](/windows/uwp/xaml-platform/xaml-intrinsic-data-types)

You can also use custom types as a shareable resource if you follow the necessary implementation patterns. You define such classes in your backing code (or in runtime components that you include) and then instantiate those classes in XAML as a resource. Examples are object data sources and [IValueConverter](/uwp/api/Windows.UI.Xaml.Data.IValueConverter) implementations for data binding.

Custom types must have a default constructor, because that's what a XAML parser uses to instantiate a class. Custom types used as resources can't have the [UIElement](/uwp/api/Windows.UI.Xaml.UIElement) class in their inheritance, because a **UIElement** can never be shareable (it's always intended to represent exactly one UI element that exists at one position in the object graph of your runtime app).

## UserControl usage scope


A [UserControl](/uwp/api/Windows.UI.Xaml.Controls.UserControl) element has a special situation for resource-lookup behavior because it has the inherent concepts of a definition scope and a usage scope. A **UserControl** that makes a XAML resource reference from its definition scope must be able to support the lookup of that resource within its own definition-scope lookup sequence—that is, it cannot access app resources. From a **UserControl** usage scope, a resource reference is treated as being within the lookup sequence towards its usage page root (just like any other resource reference made from an object in a loaded object tree) and can access app resources.

## ResourceDictionary and XamlReader.Load

You can use a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) as either the root or a part of the XAML input for the [XamlReader.Load](/uwp/api/windows.ui.xaml.markup.xamlreader.load) method. You can also include XAML resource references in that XAML if all such references are completely self-contained in the XAML submitted for loading. **XamlReader.Load** parses the XAML in a context that is not aware of any other **ResourceDictionary** objects, not even [Application.Resources](/uwp/api/windows.ui.xaml.application.resources). Also, don't use `{ThemeResource}` from within XAML submitted to **XamlReader.Load**.

## Using a ResourceDictionary from code

Most of the scenarios for a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) are handled exclusively in XAML. You declare the **ResourceDictionary** container and the resources within as a XAML file or set of XAML nodes in a UI definition file. And then you use XAML resource references to request those resources from other parts of XAML. Still, there are certain scenarios where your app might want to adjust the contents of a **ResourceDictionary** using code that executes while the app is running, or at least to query the contents of a **ResourceDictionary** to see if a resource is already defined. These code calls are made on a **ResourceDictionary** instance, so you must first retrieve one—either an immediate **ResourceDictionary** somewhere in the object tree by getting [FrameworkElement.Resources](/uwp/api/windows.ui.xaml.frameworkelement.resources), or `Application.Current.Resources`.

In C\# or Microsoft Visual Basic code, you can reference a resource in a given [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) by using the indexer ([Item](/dotnet/api/system.windows.resourcedictionary.item)). A **ResourceDictionary** is a string-keyed dictionary, so the indexer uses the string key instead of an integer index. In Visual C++ component extensions (C++/CX) code, use [Lookup](/uwp/api/windows.ui.xaml.resourcedictionary.lookup).

When using code to examine or change a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), the behavior for APIs like [Lookup](/uwp/api/windows.ui.xaml.resourcedictionary.lookup) or [Item](/dotnet/api/system.windows.resourcedictionary.item) does not traverse from immediate resources to app resources; that's a XAML parser behavior that only happens as XAML pages are loaded. At run time, scope for keys is self-contained to the **ResourceDictionary** instance that you are using at the time. However, that scope does extend into [MergedDictionaries](/uwp/api/windows.ui.xaml.resourcedictionary.mergeddictionaries).

Also, if you request a key that does not exist in the [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary), there may not be an error; the return value may simply be provided as **null**. You may still get an error, though, if you try to use the returned **null** as a value. The error would come from the property's setter, not your **ResourceDictionary** call. The only way you'd avoid an error is if the property accepted **null** as a valid value. Note how this behavior contrasts with XAML lookup behavior at XAML parse time; a failure to resolve the provided key from XAML at parse time results in a XAML parse error, even in cases where the property could have accepted **null**.

Merged resource dictionaries are included into the index scope of the primary resource dictionary that references the merged dictionary at run time. In other words, you can use **Item** or [Lookup](/uwp/api/windows.ui.xaml.resourcedictionary.lookup) of the primary dictionary to find any objects that were actually defined in the merged dictionary. In this case, the lookup behavior does resemble the parse-time XAML lookup behavior: if there are multiple objects in merged dictionaries that each have the same key, the object from the last-added dictionary is returned.

You are permitted to add items to an existing [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) by calling **Add** (C\# or Visual Basic) or [Insert](/uwp/api/windows.ui.xaml.resourcedictionary.insert) (C++/CX). You could add the items to either immediate resources or app resources. Either of these API calls requires a key, which satisfies the requirement that each item in a **ResourceDictionary** must have a key. However, items that you add to a **ResourceDictionary** at run time are not relevant to XAML resource references. The necessary lookup for XAML resource references happens when that XAML is first parsed as the app is loaded (or a theme change is detected). Resources added to collections at run time weren't available then, and altering the **ResourceDictionary** doesn't invalidate an already retrieved resource from it even if you change the value of that resource.

You also can remove items from a [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) at run time, make copies of some or all items, or other operations. The members listing for **ResourceDictionary** indicates which APIs are available. Note that because **ResourceDictionary** has a projected API to support its underlying collection interfaces, your API options differ depending on whether you are using C\# or Visual Basic versus C++/CX.

## ResourceDictionary and localization


A XAML [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary) might initially contain strings that are to be localized. If so, store these strings as project resources instead of in a **ResourceDictionary**. Take the strings out of the XAML, and instead give the owning element an [x:Uid directive](/windows/uwp/xaml-platform/x-uid-directive) value. Then, define a resource in a resources file. Provide a resource name in the form *XUIDValue*.*PropertyName* and a resource value of the string that should be localized.

## Custom resource lookup

For advanced scenarios, you can implement a class that can have different behavior than the XAML resource reference lookup behavior described in this topic. To do this, you implement the class [CustomXamlResourceLoader](/uwp/api/Windows.UI.Xaml.Resources.CustomXamlResourceLoader), and then you can access that behavior by using the [CustomResource markup extension](/windows/uwp/xaml-platform/customresource-markup-extension) for resource references rather than using [StaticResource](/windows/uwp/xaml-platform/staticresource-markup-extension) or [ThemeResource](/windows/uwp/xaml-platform/themeresource-markup-extension). Most apps won't have scenarios that require this. For more info, see [CustomXamlResourceLoader](/uwp/api/Windows.UI.Xaml.Resources.CustomXamlResourceLoader).

 
## Related topics

* [ResourceDictionary](/uwp/api/Windows.UI.Xaml.ResourceDictionary)
* [XAML overview](/windows/uwp/xaml-platform/xaml-overview)
* [StaticResource markup extension](/windows/uwp/xaml-platform/staticresource-markup-extension)
* [ThemeResource markup extension](/windows/uwp/xaml-platform/themeresource-markup-extension)
* [XAML theme resources](xaml-theme-resources.md)
* [Styling controls](xaml-styles.md)
* [x:Key attribute](/windows/uwp/xaml-platform/x-key-attribute)

 

 
