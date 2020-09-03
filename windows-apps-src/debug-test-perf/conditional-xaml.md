---
title: Conditional XAML
description: Use new APIs in XAML markup while maintaining compatibility with previous versions
ms.date: 10/10/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Conditional XAML

*Conditional XAML* provides a way to use the [ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent) method in XAML markup. This lets you set properties and instantiate objects in markup based on the presence of an API without needing to use code behind. It selectively parses elements or attributes to determine whether they will be available at runtime. Conditional statements are evaluated at runtime, and elements qualified with a conditional XAML tag are parsed if they evaluate to **true**; otherwise, they are ignored.

Conditional XAML is available starting with the Creators Update (version 1703, build 15063). To use conditional XAML, the Minimum Version of your Visual Studio project must be set to build 15063 (Creators Update) or later, and the Target Version be set to a later version than the Minimum. See [Version adaptive apps](version-adaptive-apps.md) for more info about configuring your Visual Studio project.

> [!NOTE]
> To create a version adaptive app with a Minimum Version less than build 15063, you must use [version adaptive code](version-adaptive-code.md), not XAML.

For important background info about ApiInformation and API contracts, see [Version adaptive apps](version-adaptive-apps.md).

## Conditional namespaces

To use a conditional method in XAML, you must first declare a conditional [XAML namespace](../xaml-platform/xaml-namespaces-and-namespace-mapping.md) at the top of your page. Here's a psuedo-code example of a conditional namespace:

```xaml
xmlns:myNamespace="schema?conditionalMethod(parameter)"
```

A conditional namespace can be broken down into two parts separated by the '?' delimiter. 

- The content preceding the delimiter indicates the namespace or schema that contains the API being referenced. 
- The content after the '?' delimiter represents the conditional method that determines whether the conditional namespace evaluates to **true** or **false**.

In most cases, the schema will be the default XAML namespace:

```xaml
xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
```

Conditional XAML supports the following conditional methods:

Method | Inverse
------ | -------
IsApiContractPresent(ContractName, VersionNumber) | IsApiContractNotPresent(ContractName, VersionNumber)
IsTypePresent(ControlType) | IsTypeNotPresent(ControlType)
IsPropertyPresent(ControlType, PropertyName) | IsPropertyNotPresent(ControlType, PropertyName)

We discuss these methods further in later sections of this article.

> [!NOTE]
> We recommend you use IsApiContractPresent and IsApiContractNotPresent. Other conditionals are not fully supported in the Visual Studio design experience.

## Create a namespace and set a property

In this example, you display, "Hello, Conditional XAML", as the content of a text block if the app runs on the Fall Creators Update or later, and default to no content if it's on a previous version.

First, define a custom namespace with the prefix 'contract5Present' and use the default XAML namespace (https://schemas.microsoft.com/winfx/2006/xaml/presentation) as the schema containing the [TextBlock.Text](/uwp/api/windows.ui.xaml.controls.textblock.Text) property. To make this a conditional namespace, add the ‘?’ delimiter after the schema.

You then define a conditional that returns **true** on devices that are running the Fall Creators Update or later. You use the ApiInformation method **IsApiContractPresent** to check for the 5th version of the UniversalApiContract. Version 5 of the UniversalApiContract was released with the Fall Creators Update (SDK 16299).

```xaml
xmlns:contract5Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,5)"
```

After the namespace is defined, you prepend the namespace prefix to the Text property of your TextBox to qualify it as a property that should be set conditionally at runtime.

```xaml
<TextBlock contract5Present:Text="Hello, Conditional XAML"/>
```

Here's the complete XAML.

```xaml
<Page
    x:Class="ConditionalTest.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:contract5Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,5)">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <TextBlock contract5Present:Text="Hello, Conditional XAML"/>
    </Grid>
</Page>
```

When you run this example on the Fall Creators Update, the text, "Hello, Conditional XAML" is shown; when you run it on the Creators Update, no text is shown.

Conditional XAML lets you perform the API checks you can do in code in your markup instead. Here's the equivalent code for this check.

```csharp
if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5))
{
    textBlock.Text = "Hello, Conditional XAML";
}
```

Notice that even though the IsApiContractPresent method takes a string for the *contractName* parameter, you don't put it in quotes (" ") in the XAML namespace declaration.

## Use if/else conditions

In the previous example, the Text property is set only when the app runs on the Fall Creators Update. But what if you want to show different text when it runs on the Creators Update? You could try to set the Text property without a conditional qualifier, like this.

```xaml
<!-- DO NOT USE -->
<TextBlock Text="Hello, World" contract5Present:Text="Hello, Conditional XAML"/>
```

This will work when it runs on the Creators Update, but when it runs on the Fall Creators Update, you get an error saying that the Text property is set more than once.

To set different text when the app runs on different versions of Windows 10, you need another condition. Conditional XAML provides an inverse of each supported ApiInformation method to let you create if/else conditional scenarios like this.

The IsApiContractPresent method returns **true** if the current device contains the specified contract and version number. For example, assume your app is running on the Creators Update, which has the 4th version of the universal API Contract.

Various calls to IsApiContractPresent would have these results:

- IsApiContractPresent(Windows.Foundation.UniversalApiContract, 5) = **false**
- IsApiContractPresent(Windows.Foundation.UniversalApiContract, 4) = true
- IsApiContractPresent(Windows.Foundation.UniversalApiContract, 3) = true
- IsApiContractPresent(Windows.Foundation.UniversalApiContract, 2) = true
- IsApiContractPresent(Windows.Foundation.UniversalApiContract, 1) = true.

IsApiContractNotPresent returns the inverse of IsApiContractPresent. Calls to IsApiContractNotPresent would have these results:

- IsApiContractNotPresent(Windows.Foundation.UniversalApiContract, 5) = **true**
- IsApiContractNotPresent(Windows.Foundation.UniversalApiContract, 4) = false
- IsApiContractNotPresent(Windows.Foundation.UniversalApiContract, 3) = false
- IsApiContractNotPresent(Windows.Foundation.UniversalApiContract, 2) = false
- IsApiContractNotPresent(Windows.Foundation.UniversalApiContract, 1) = false

To use the inverse condition, you create a second conditional XAML namespace that uses the **IsApiContractNotPresent** conditional. Here, it has the prefix 'contract5NotPresent'.

```xaml
xmlns:contract5NotPresent="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractNotPresent(Windows.Foundation.UniversalApiContract,5)"

xmlns:contract5Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,5)"
```

With both namespaces defined, you can set the Text property twice as long as you prefix them with qualifiers that ensure only one property setting is used at runtime, like this:

```xaml
<TextBlock contract5NotPresent:Text="Hello, World"
           contract5Present:Text="Hello, Fall Creators Update"/>
```

Here's another example that sets the background of a button. The [Acrylic material](../design/style/acrylic.md) feature is available starting with the Fall Creators Update, so you’ll use Acrylic for the background when the app runs on the Fall Creators Update. It's not available on earlier versions, so in those cases, you set the background to red.

```xaml
<Button Content="Button"
        contract5NotPresent:Background="Red"
        contract5Present:Background="{ThemeResource SystemControlAcrylicElementBrush}"/>
```

## Create controls and bind properties

So far, you’ve seen how to set properties using conditional XAML, but you can also conditionally instantiate controls based on the API contract available at runtime.

Here, a [ColorPicker](/uwp/api/windows.ui.xaml.controls.colorpicker) is instantiated when the app runs on the Fall Creators Update where the control is available. The ColorPicker isn't available prior to the Fall Creators Update, so when the app runs on earlier versions, you use a [ComboBox](/uwp/api/windows.ui.xaml.controls.combobox) to provide simplified color choices to the user.

```xaml
<contract5Present:ColorPicker x:Name="colorPicker"
                              Grid.Column="1"
                              VerticalAlignment="Center"/>

<contract5NotPresent:ComboBox x:Name="colorComboBox"
                              PlaceholderText="Pick a color"
                              Grid.Column="1"
                              VerticalAlignment="Center">
```

You can use conditional qualifiers with different forms of [XAML property syntax](../xaml-platform/xaml-syntax-guide.md). Here, the rectangle’s Fill property is set using property element syntax for the Fall Creators Update, and using attribute syntax for previous versions.

```xaml
<Rectangle x:Name="colorRectangle" Width="200" Height="200"
           contract5NotPresent:Fill="{x:Bind ((SolidColorBrush)((FrameworkElement)colorComboBox.SelectedItem).Tag), Mode=OneWay}">
    <contract5Present:Rectangle.Fill>
        <SolidColorBrush contract5Present:Color="{x:Bind colorPicker.Color, Mode=OneWay}"/>
    </contract5Present:Rectangle.Fill>
</Rectangle>
```

When you bind a property to another property that depends on a conditional namespace, you must use the same condition on both properties. Here, `colorPicker.Color` depends on the 'contract5Present' conditional namespace, so you must also place the 'contract5Present' prefix on the SolidColorBrush.Color property. (Or, you can place the 'contract5Present' prefix on the SolidColorBrush instead of on the Color property.) If you don’t, you’ll get a compile-time error.

```xaml
<SolidColorBrush contract5Present:Color="{x:Bind colorPicker.Color, Mode=OneWay}"/>
```

Here's the complete XAML that demonstrates these scenarios. This example contains a rectangle and a UI that lets you set the color of the rectangle.

When the app runs on the Fall Creators Update, you use a ColorPicker to let the user set the color. The ColorPicker isn't available prior to the Fall Creators Update, so when the app runs on earlier versions, you use a combo box to provide simplified color choices to the user.

```xaml
<Page
    x:Class="ConditionalTest.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:contract5Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,5)"
    xmlns:contract5NotPresent="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractNotPresent(Windows.Foundation.UniversalApiContract,5)">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid.ColumnDefinitions>
            <ColumnDefinition/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>

        <Rectangle x:Name="colorRectangle" Width="200" Height="200"
                   contract5NotPresent:Fill="{x:Bind ((SolidColorBrush)((FrameworkElement)colorComboBox.SelectedItem).Tag), Mode=OneWay}">
            <contract5Present:Rectangle.Fill>
                <SolidColorBrush contract5Present:Color="{x:Bind colorPicker.Color, Mode=OneWay}"/>
            </contract5Present:Rectangle.Fill>
        </Rectangle>

        <contract5Present:ColorPicker x:Name="colorPicker"
                                      Grid.Column="1"
                                      VerticalAlignment="Center"/>

        <contract5NotPresent:ComboBox x:Name="colorComboBox"
                                      PlaceholderText="Pick a color"
                                      Grid.Column="1"
                                      VerticalAlignment="Center">
            <ComboBoxItem>Red
                <ComboBoxItem.Tag>
                    <SolidColorBrush Color="Red"/>
                </ComboBoxItem.Tag>
            </ComboBoxItem>
            <ComboBoxItem>Blue
                <ComboBoxItem.Tag>
                    <SolidColorBrush Color="Blue"/>
                </ComboBoxItem.Tag>
            </ComboBoxItem>
            <ComboBoxItem>Green
                <ComboBoxItem.Tag>
                    <SolidColorBrush Color="Green"/>
                </ComboBoxItem.Tag>
            </ComboBoxItem>
        </contract5NotPresent:ComboBox>
    </Grid>
</Page>
```

## Related articles

- [Guide to UWP apps](../get-started/universal-application-platform-guide.md)
- [Dynamically detecting features with API contracts](https://blogs.windows.com/buildingapps/2015/09/15/dynamically-detecting-features-with-api-contracts-10-by-10/)
- [API Contracts](https://channel9.msdn.com/Events/Build/2015/3-733) (Build 2015 video)
- [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview)
