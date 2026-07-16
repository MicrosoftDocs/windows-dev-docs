---
description: Learn how to use functions in x:Bind to simplify value conversion and create complex bindings in WinUI apps.
title: Functions in x:Bind with WinUI
ms.date: 07/15/2026
ms.topic: concept-article
keywords: windows 10, windows 11, windows app sdk, xaml, winui, windows ui, xBind, winui 3
ms.localizationpriority: medium
# Customer intent: As a Windows developer, I want to learn how to use functions in x:Bind so that I can simplify value conversion and create more complex bindings in my WinUI apps.
---

# Functions in x:Bind with WinUI

WinUI apps allow you to use functions as the leaf step of the data binding path in the `{x:Bind}` markup extension. This feature simplifies value conversion and enables bindings to depend on multiple parameters, making your app more dynamic and efficient.

> [!TIP]
> For general info about using data binding in your app with `{x:Bind}` (and for an all-up comparison between `{x:Bind}` and `{Binding}`), see [Data binding in depth](data-binding-in-depth.md) and [{x:Bind} Markup Extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension).

In the following example, the background and foreground of the item are bound to functions that do conversion based on the color parameter.

``` xaml
<DataTemplate x:DataType="local:ColorEntry">
    <Grid Background="{x:Bind local:ColorEntry.Brushify(Color), Mode=OneWay}" Width="240">
        <TextBlock Text="{x:Bind ColorName}" Foreground="{x:Bind TextColor(Color)}" Margin="10,5" />
    </Grid>
</DataTemplate>
```

``` csharp
public class ColorEntry
{
    public string ColorName { get; set; }
    public Color Color { get; set; }

    public static SolidColorBrush Brushify(Color c)
    {
        return new SolidColorBrush(c);
    }

    public SolidColorBrush TextColor(Color c)
    {
        return new SolidColorBrush(((c.R * 0.299 + c.G * 0.587 + c.B * 0.114) > 150) ? Colors.Black : Colors.White);
    }
}
```

## XAML attribute usage

``` xaml
<object property="{x:Bind pathToFunction.FunctionName(functionParameter1, functionParameter2, ...), bindingProperties}" ... />
```

## Path to the function

Specify the [path to the function](/windows/apps/develop/platform/xaml/x-bind-markup-extension#property-path) like other property paths. The path can include [dots](/windows/apps/develop/platform/xaml/x-bind-markup-extension#property-path-resolution) (.), [indexers](/windows/apps/develop/platform/xaml/x-bind-markup-extension#collections), or [casts](/windows/apps/develop/platform/xaml/x-bind-markup-extension#casting) to locate the function.

Use the `XMLNamespace:ClassName.MethodName` syntax to specify static functions. For example, use the following syntax to bind to static functions in code-behind.

``` xaml
<Window 
     xmlns:local="using:MyNamespace">
     ...
    <StackPanel>
        <TextBlock x:Name="BigTextBlock" FontSize="20" Text="Big text" />
        <TextBlock FontSize="{x:Bind local:MyHelpers.Half(BigTextBlock.FontSize)}" 
                   Text="Small text" />
    </StackPanel>
</Window>
```

``` csharp
namespace MyNamespace
{
    static public class MyHelpers
    {
        public static double Half(double value) => value / 2.0;
    }
}
```

You can also use system functions directly in markup to accomplish simple scenarios like date formatting, text formatting, text concatenations, and more. For example:

``` xaml
<Window 
     xmlns:sys="using:System"
     xmlns:local="using:MyNamespace">
     ...
     <CalendarDatePicker Date="{x:Bind sys:DateTime.Parse(TextBlock1.Text)}" />
     <TextBlock Text="{x:Bind sys:String.Format('{0} is now available in {1}', local:MyPage.personName, local:MyPage.location)}" />
</Window>
```

If you set the mode to OneWay or TwoWay, the function path supports change detection. The binding engine re-evaluates the binding if those objects change.

The function you're binding to needs to:

- Be accessible to the code and metadata – so internal or private works in C#, but C++ needs methods to be public WinRT methods
- Support overloading based on the number of arguments, not type, and it tries to match the first overload with that many arguments
- Have argument types that match the data being passed in – the binding engine doesn't perform narrowing conversions
- Have a return type that matches the type of the property that uses the binding

The binding engine reacts to property change notifications fired with the function name and re-evaluates bindings as necessary. For example:

``` xaml
<DataTemplate x:DataType="local:Person">
   <StackPanel>
      <TextBlock Text="{x:Bind FullName}" />
      <Image Source="{x:Bind IconToBitmap(Icon, CancellationToken), Mode=OneWay}" />
   </StackPanel>
</DataTemplate>
```

``` csharp
public class Person : INotifyPropertyChanged
{
    //Implementation for an Icon property and a CancellationToken property with PropertyChanged notifications
    ...

    //IconToBitmap function is essentially a multi binding converter between several options.
    public Uri IconToBitmap (Uri icon, Uri cancellationToken)
    {
        var foo = new Uri(...);        
        if (isCancelled)
        {
            foo = cancellationToken;
        }
        else 
        {
            if (fullName.Contains("Sr"))
            {
               //pass a different Uri back
               foo = new Uri(...);
            }
            else
            {
                foo = icon;
            }
        }
        return foo;
    }

    //Ensure FullName property handles change notification on itself as well as IconToBitmap since the function uses it
    public string FullName
    {
        get { return fullName; }
        set
        {
            fullName = value;
            OnPropertyChanged();
            OnPropertyChanged("IconToBitmap"); 
            //this ensures Image.Source binding re-evaluates when FullName changes in addition to Icon and CancellationToken
        }
    }
}
```

> [!TIP]
> Use functions in `x:Bind` to achieve the same scenarios as what was supported through Converters and MultiBinding in WPF.

## Function arguments

Specify multiple function arguments separated by commas (,).

- Binding Path – Use the same syntax as if you were binding directly to that object.
  - If you set the mode to OneWay or TwoWay, the binding detects changes and reevaluates when the object changes.
- Constant string enclosed in quotes – Include quotes to designate it as a string. Use the hat (^) to escape quotes in strings.
- Constant number – For example, -123.456.
- Boolean – Specify as "x:True" or "x:False".

> [!TIP]
> [TargetNullValue](/windows/apps/develop/platform/xaml/x-bind-markup-extension#properties-that-you-can-set-with-xbind) applies to the result of the function call, not to any bound arguments.

### Two-way function bindings

In a two-way binding scenario, you must specify a second function for the reverse direction of the binding. Use the `BindBack` binding property for this function. In the following example, the function takes one argument, which is the value that needs to be pushed back to the model.

``` xaml
<TextBlock Text="{x:Bind a.MyFunc(b), BindBack=a.MyFunc2, Mode=TwoWay}" />
```

## See also

- [{x:Bind} Markup Extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension)
- [Data binding in depth](data-binding-in-depth.md)
- [Data binding overview](data-binding-overview.md)
- [WinUI Gallery](https://github.com/microsoft/WinUI-Gallery)
