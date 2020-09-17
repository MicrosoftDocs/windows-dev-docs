---
title: Using Windows Runtime objects in a multithreaded environment | Microsoft Docs
description: This article discusses the way the .NET Framework handles calls from C# and Visual Basic code to objects that are provided by the Windows Runtime or by Windows Runtime components.
ms.date: 01/14/2017
ms.topic: article
ms.assetid: 43ffd28c-c4df-405c-bf5c-29c94e0d142b
keywords: windows 10, uwp, timer, threads
ms.localizationpriority: medium
---
# Using Windows Runtime objects in a multithreaded environment
This article discusses the way the .NET Framework handles calls from C# and Visual Basic code to objects that are provided by the Windows Runtime or by Windows Runtime components.

In the .NET Framework, you can access any object from multiple threads by default, without special handling. All you need is a reference to the object. In the Windows Runtime, such objects are called *agile*. Most Windows Runtime classes are agile, but a few classes are not, and even agile classes may require special handling.

Wherever possible, the common language runtime (CLR) treats objects from other sources, such as the Windows Runtime, as if they were .NET Framework objects:

- If the object implements the [IAgileObject](/windows/desktop/api/objidl/nn-objidl-iagileobject) interface, or has the [MarshalingBehaviorAttribute](/uwp/api/Windows.Foundation.Metadata.MarshalingBehaviorAttribute) attribute with [MarshalingType.Agile](/uwp/api/Windows.Foundation.Metadata.MarshalingType), the CLR treats it as agile.

- If CLR can marshal a call from the thread where it was made to the threading context of the target object, it does so transparently.

- If the object has the [MarshalingBehaviorAttribute](/uwp/api/Windows.Foundation.Metadata.MarshalingBehaviorAttribute) attribute with [MarshalingType.None](/uwp/api/Windows.Foundation.Metadata.MarshalingType), the class does not provide marshaling information. The CLR cannot marshal the call, so it throws an [InvalidCastException](/dotnet/api/system.invalidcastexception) exception with a message indicating that the object can be used only in the threading context where it was created.

The following sections describe the effects of this behavior on objects from various sources.

## Objects from a Windows Runtime component that is written in C# or Visual Basic
All the types in the component that can be activated are agile by default.

> [!NOTE]
>  Agility doesn't imply thread safety. In both the Windows Runtime and the .NET Framework, most classes are not thread safe because thread safety has a performance cost, and most objects are never accessed by multiple threads. It's more efficient to synchronize access to individual objects (or to use thread-safe classes) only as necessary.

When you author a Windows Runtime component, you can override the default. See the [ICustomQueryInterface](/dotnet/api/system.runtime.interopservices.icustomqueryinterface) interface and the [IAgileObject](/windows/desktop/api/objidl/nn-objidl-iagileobject) interface.

## Objects from the Windows Runtime
Most classes in the Windows Runtime are agile, and the CLR treats them as agile. The documentation for these classes lists "MarshalingBehaviorAttribute(Agile)" among the class attributes. However, the members of some of these agile classes, such as XAML controls, throw exceptions if they aren't called on the UI thread. For example, the following code tries to use a background thread to set a property of the button that was clicked. The button's [Content](/uwp/api/Windows.UI.Xaml.Controls.ContentControl) property throws an exception.

```csharp
private async void Button_Click_2(object sender, RoutedEventArgs e)
{
    Button b = (Button) sender;
    await Task.Run(() => {
        b.Content += ".";
    });
}
```

```vb
Private Async Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
    Dim b As Button = CType(sender, Button)
    Await Task.Run(Sub()
                       b.Content &= "."
                   End Sub)
End Sub
```

You can access the button safely by using its [Dispatcher](/uwp/api/Windows.UI.Xaml.DependencyObject) property, or the `Dispatcher` property of any object that exists in the context of the UI thread (such as the page the button is on). The following code uses the [CoreDispatcher](/uwp/api/Windows.UI.Core.CoreDispatcher) object's [RunAsync](/uwp/api/Windows.UI.Core.CoreDispatcher) method to dispatch the call on the UI thread.

```csharp
private async void Button_Click_2(object sender, RoutedEventArgs e)
{
    Button b = (Button) sender;
    await b.Dispatcher.RunAsync(
        Windows.UI.Core.CoreDispatcherPriority.Normal,
        () => {
            b.Content += ".";
    });
}

```

```vb
Private Async Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
    Dim b As Button = CType(sender, Button)
    Await b.Dispatcher.RunAsync(
        Windows.UI.Core.CoreDispatcherPriority.Normal,
        Sub()
            b.Content &= "."
        End Sub)
End Sub
```

> [!NOTE]
>  The `Dispatcher` property does not throw an exception when it's called from another thread.

The lifetime of a Windows Runtime object that is created on the UI thread is bounded by the lifetime of the thread. Do not try to access objects on a UI thread after the window has closed.

If you create your own control by inheriting a XAML control, or by composing a set of XAML controls, your control is agile because it's a .NET Framework object. However, if it calls members of its base class or constituent classes, or if you call inherited members, those members will throw exceptions when they are called from any thread except the UI thread.

### Classes that can't be marshaled
Windows Runtime classes that do not provide marshaling information have the [MarshalingBehaviorAttribute](/uwp/api/Windows.Foundation.Metadata.MarshalingBehaviorAttribute) attribute with [MarshalingType.None](/uwp/api/Windows.Foundation.Metadata.MarshalingType). The documentation for such a class lists "MarshalingBehaviorAttribute(None)" among its attributes.

The following code creates a [CameraCaptureUI](/uwp/api/Windows.Media.Capture.CameraCaptureUI) object on the UI thread, and then tries to set a property of the object from a thread pool thread. The CLR is unable to marshal the call, and throws a [System.InvalidCastException](/dotnet/api/system.invalidcastexception) exception with a message indicating that the object can be used only in the threading context where it was created.

```csharp
Windows.Media.Capture.CameraCaptureUI ccui;

private async void Button_Click_1(object sender, RoutedEventArgs e)
{
    ccui = new Windows.Media.Capture.CameraCaptureUI();

    await Task.Run(() => {
        ccui.PhotoSettings.AllowCropping = true;
    });
}

```

```vb
Private ccui As Windows.Media.Capture.CameraCaptureUI

Private Async Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
    ccui = New Windows.Media.Capture.CameraCaptureUI()

    Await Task.Run(Sub()
                       ccui.PhotoSettings.AllowCropping = True
                   End Sub)
End Sub
```

The documentation for [CameraCaptureUI](/uwp/api/Windows.Media.Capture.CameraCaptureUI) also lists "ThreadingAttribute(STA)" among the class's attributes, because it must be created in a single-threaded context such as the UI thread.

If you want to access the [CameraCaptureUI](/uwp/api/Windows.Media.Capture.CameraCaptureUI) object from another thread, you can cache the [CoreDispatcher](/uwp/api/Windows.UI.Core.CoreDispatcher) object for the UI thread and use it later to dispatch the call on that thread. Or you can obtain the dispatcher from a XAML object such as the page, as shown in the following code.

```csharp
Windows.Media.Capture.CameraCaptureUI ccui;

private async void Button_Click_3(object sender, RoutedEventArgs e)
{
    ccui = new Windows.Media.Capture.CameraCaptureUI();

    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        () => {
            ccui.PhotoSettings.AllowCropping = true;
        });
}

```

```vb
Dim ccui As Windows.Media.Capture.CameraCaptureUI

Private Async Sub Button_Click_3(sender As Object, e As RoutedEventArgs)

    ccui = New Windows.Media.Capture.CameraCaptureUI()

    Await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                                Sub()
                                    ccui.PhotoSettings.AllowCropping = True
                                End Sub)
End Sub
```

## Objects from a Windows Runtime component that is written in C++
By default, classes in the component that can be activated are agile. However, C++ allows a significant amount of control over threading models and marshaling behavior. As described earlier in this article, the CLR recognizes agile classes, tries to marshal calls when classes are not agile, and throws a [System.InvalidCastException](/dotnet/api/system.invalidcastexception) exception when a class has no marshaling information.

For objects that run on the UI thread and throw exceptions when they are called from a thread other than the UI thread, you can use the UI thread’s [CoreDispatcher](/uwp/api/Windows.UI.Core.CoreDispatcher) object to dispatch the call.

## See Also
[C# Guide](/dotnet/csharp/)

[Visual Basic Guide](/dotnet/visual-basic/)