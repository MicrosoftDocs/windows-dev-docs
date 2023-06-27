---
description: Learn about using functions as the leaf step of the data binding path in the xBind markup extension.
title: Functions in x:Bind with Windows App SDK
ms.date: 12/13/2022
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, xaml, winui, windows ui, xBind
ms.localizationpriority: medium
---

# Functions in x:Bind with Windows App SDK

> [!NOTE]
> For general info about using data binding in your app with `{x:Bind}` (and for an all-up comparison between `{x:Bind}` and `{Binding}`), see [Data binding in depth](data-binding-in-depth.md) and [{x:Bind} Markup Extension](/windows/uwp/xaml-platform/x-bind-markup-extension).

In Windows App SDK apps, **{x:Bind}** supports using a function as the leaf step of the binding path. This enables:

- A simpler way to achieve value conversion
- A way for bindings to depend on more than one parameter

In the following example, the background and foreground of the item are bound to functions to do conversion based on the color parameter

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

The [path to the function](/windows/uwp/xaml-platform/x-bind-markup-extension#property-path) is specified like other property paths and can include [dots](/windows/uwp/xaml-platform/x-bind-markup-extension#property-path-resolution) (.), [indexers](/windows/uwp/xaml-platform/x-bind-markup-extension#collections) or [casts](/windows/uwp/xaml-platform/x-bind-markup-extension#casting) to locate the function.

Static functions can be specified using `XMLNamespace:ClassName.MethodName` syntax. For example, use the below syntax for binding to static functions in code-behind.

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

You can also use system functions directly in markup to accomplish simple scenarios like date formatting, text formatting, text concatenations, etc. For example:

``` xaml
<Window 
     xmlns:sys="using:System"
     xmlns:local="using:MyNamespace">
     ...
     <CalendarDatePicker Date="{x:Bind sys:DateTime.Parse(TextBlock1.Text)}" />
     <TextBlock Text="{x:Bind sys:String.Format('{0} is now available in {1}', local:MyPage.personName, local:MyPage.location)}" />
</Window>
```

If the mode is OneWay/TwoWay, then the function path will have change detection performed on it, and the binding will be re-evaluated if there are changes to those objects.

The function being bound to needs to:

- Be accessible to the code and metadata – so internal / private work in C#, but C++ will need methods to be public WinRT methods
- Overloading is based on the number of arguments, not type, and it will try to match to the first overload with that many arguments
- The argument types need to match the data being passed in – we don’t do narrowing conversions
- The return type of the function needs to match the type of the property that is using the binding

The binding engine reacts to property change notifications fired with the function name and re-evaluate bindings as necessary. For example:

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
> You can use functions in `x:Bind` to achieve the same scenarios as what was supported through Converters and MultiBinding in WPF.

## Function arguments

Multiple function arguments can be specified, separated by comma's (,)

- Binding Path – Same syntax as if you were binding directly to that object.
  - If the mode is OneWay/TwoWay then change detection will be performed and the binding re-evaluated upon object changes
- Constant string enclosed in quotes – quotes are needed to designate it as a string. Hat (^) can be used to escape quotes in strings
- Constant Number - for example -123.456
- Boolean – specified as "x:True" or "x:False"

### Two way function bindings

In a two-way binding scenario, a second function must be specified for the reverse direction of the binding. This is done using the `BindBack` binding property. In the below example, the function should take one argument which is the value that needs to be pushed back to the model.

``` xaml
<TextBlock Text="{x:Bind a.MyFunc(b), BindBack=a.MyFunc2, Mode=TwoWay}" />
```

## See also

- [{x:Bind} Markup Extension](/windows/uwp/xaml-platform/x-bind-markup-extension)
