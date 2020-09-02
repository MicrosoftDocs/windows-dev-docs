---
title: Windows Runtime components with C# and Visual Basic
description: Starting with .NET 4.5, you can use managed code to create your own Windows Runtime types, packaged in a Windows Runtime component.
ms.assetid: A5672966-74DF-40AB-B01E-01E3FCD0AD7A
ms.date: 12/04/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
- csharp
- vb
---

# Windows Runtime components with C# and Visual Basic

You can use managed code to create your own Windows Runtime types and package them in a Windows Runtime component. You can use your component in Universal Windows Platform (UWP) apps that are written in C++, JavaScript, Visual Basic, or C#. This topic outlines the rules for creating a component, and discusses some aspects of .NET support for the Windows Runtime. In general, that support is designed to be transparent to the .NET programmer. However, when you create a component to use with JavaScript or C++, you need to be aware of differences in the way those languages support the Windows Runtime.

If you are creating a component for use only in UWP apps that are written in Visual Basic or C#, and the component does not contain UWP controls, then consider using the **Class Library** template instead of the **Windows Runtime Component** project template in Microsoft Visual Studio. There are fewer restrictions on a simple class library.

## Declaring types in Windows Runtime components

Internally, the Windows Runtime types in your component can use any .NET functionality that's allowed in a UWP app. For more info, see [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0).

Externally, the members of your types can expose only Windows Runtime types for their parameters and return values. The following list describes the limitations on .NET types that are exposed from a Windows Runtime component.

- The fields, parameters, and return values of all the public types and members in your component must be Windows Runtime types. This restriction includes the Windows Runtime types that you author as well as types that are provided by the Windows Runtime itself. It also includes a number of .NET types. The inclusion of these types is part of the support that .NET provides to enable the natural use of the Windows Runtime in managed code&mdash;your code appears to use familiar .NET types instead of the underlying Windows Runtime types. For example, you can use .NET primitive types such as **Int32** and **Double**, certain fundamental types such as **DateTimeOffset** and **Uri**, and some commonly used generic interface types such as **IEnumerable&lt;T&gt;** (IEnumerable(Of T) in Visual Basic) and **IDictionary&lt;TKey,TValue&gt;**. Note that the type arguments of these generic types must be Windows Runtime types. This is discussed in the sections [Passing Windows Runtime types to managed code](#passing-windows-runtime-types-to-managed-code) and [Passing managed types to the Windows Runtime](#passing-managed-types-to-the-windows-runtime), later in this topic.

- Public classes and interfaces can contain methods, properties, and events. You can declare delegates for your events, or use the **EventHandler&lt;T&gt;** delegate. A public class or interface can't:
    - Be generic.
    - Implement an interface that is not a Windows Runtime interface (however, you can create your own Windows Runtime interfaces and implement them).
    - Derive from types that are not in the Windows Runtime, such as **System.Exception** and **System.EventArgs**.

- All public types must have a root namespace that matches the assembly name, and the assembly name must not begin with "Windows".

    > **Tip**. By default, Visual Studio projects have namespace names that match the assembly name. In Visual Basic, the Namespace statement for this default namespace is not shown in your code.

- Public structures can't have any members other than public fields, and those fields must be value types or strings.
- Public classes must be **sealed** (**NotInheritable** in Visual Basic). If your programming model requires polymorphism, then you can create a public interface, and implement that interface on the classes that must be polymorphic.

## Debugging your component

If both your UWP app and your component are built with managed code, then you can debug them both at the same time.

When you're testing your component as part of a UWP app using C++, you can debug managed and native code at the same time. The default is native code only.

## To debug both native C++ code and managed code
1.  Open the shortcut menu for your Visual C++ project, and choose **Properties**.
2.  In the property pages, under **Configuration Properties**, choose **Debugging**.
3.  Choose **Debugger Type**, and in the drop-down list box change **Native Only** to **Mixed (Managed and Native)**. Choose **OK**.
4.  Set breakpoints in native and managed code.

When you're testing your component as part of a UWP app using JavaScript, by default the solution is in JavaScript debugging mode. In Visual Studio, you can't debug JavaScript and managed code at the same time.

## To debug managed code instead of JavaScript
1.  Open the shortcut menu for your JavaScript project, and choose **Properties**.
2.  In the property pages, under **Configuration Properties**, choose **Debugging**.
3.  Choose **Debugger Type**, and in the drop-down list box change **Script Only** to **Managed Only**. Choose **OK**.
4.  Set breakpoints in managed code and debug as usual.

## Passing Windows Runtime types to managed code
As mentioned previously in the section [Declaring types in Windows Runtime components](#declaring-types-in-windows-runtime-components), certain .NET types can appear in the signatures of members of public classes. This is part of the support that .NET provides to enable the natural use of the Windows Runtime in managed code. It includes primitive types and some classes and interfaces. When your component is used from JavaScript, or from C++ code, it's important to know how your .NET types appear to the caller. See [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md) for examples with JavaScript. This section discusses commonly used types.

In .NET, primitive types such as the **Int32** structure have many useful properties and methods, such as the **TryParse** method. By contrast, primitive types and structures in the Windows Runtime only have fields. When you pass these types to managed code, they appear to be .NET types, and you can use the properties and methods of .NET types as you normally would. The following list summarizes the substitutions that are made automatically in the IDE:

-   For the Windows Runtime primitives **Int32**, **Int64**, **Single**, **Double**, **Boolean**, **String** (an immutable collection of Unicode characters), **Enum**, **UInt32**, **UInt64**, and **Guid**, use the type of the same name in the System namespace.
-   For **UInt8**, use **System.Byte**.
-   For **Char16**, use **System.Char**.
-   For the **IInspectable** interface, use **System.Object**.

If C# or Visual Basic provides a language keyword for any of these types, then you can use the language keyword instead.

In addition to primitive types, some basic, commonly used Windows Runtime types appear in managed code as their .NET equivalents. For example, suppose your JavaScript code uses the **Windows.Foundation.Uri** class, and you want to pass it to a C# or Visual Basic method. The equivalent type in managed code is the .NET **System.Uri** class, and that's the type to use for the method parameter. You can tell when a Windows Runtime type appears as a .NET type, because IntelliSense in Visual Studio hides the Windows Runtime type when you're writing managed code, and presents the equivalent .NET type. (Usually the two types have the same name. However, note that the **Windows.Foundation.DateTime** structure appears in managed code as **System.DateTimeOffset** and not as **System.DateTime**.)

For some commonly used collection types, the mapping is between the interfaces that are implemented by a Windows Runtime type and the interfaces that are implemented by the corresponding .NET type. As with the types mentioned above, you declare parameter types by using the .NET type. This hides some differences between the types and makes writing .NET code more natural.

The following table lists the most common of these generic interface types, along with other common class and interface mappings. For a complete list of Windows Runtime types that .NET maps, see [.NET mappings of Windows Runtime types](net-framework-mappings-of-windows-runtime-types.md).

| Windows Runtime                                  | .NET                                    |
|-|-|
| IIterable&lt;T&gt;                               | IEnumerable&lt;T&gt;                              |
| IVector&lt;T&gt;                                 | IList&lt;T&gt;                                    |
| IVectorView&lt;T&gt;                             | IReadOnlyList&lt;T&gt;                            |
| IMap&lt;K, V&gt;                                 | IDictionary&lt;TKey, TValue&gt;                   |
| IMapView&lt;K, V&gt;                             | IReadOnlyDictionary&lt;TKey, TValue&gt;           |
| IKeyValuePair&lt;K, V&gt;                        | KeyValuePair&lt;TKey, TValue&gt;                  |
| IBindableIterable                                | IEnumerable                                       |
| IBindableVector                                  | IList                                             |
| Windows.UI.Xaml.Data.INotifyPropertyChanged      | System.ComponentModel.INotifyPropertyChanged      |
| Windows.UI.Xaml.Data.PropertyChangedEventHandler | System.ComponentModel.PropertyChangedEventHandler |
| Windows.UI.Xaml.Data.PropertyChangedEventArgs    | System.ComponentModel.PropertyChangedEventArgs    |

When a type implements more than one interface, you can use any of the interfaces it implements as a parameter type or return type of a member. For example, you can pass or return a **Dictionary&lt;int, string&gt;** (**Dictionary(Of Integer, String)** in Visual Basic) as **IDictionary&lt;int, string&gt;**, **IReadOnlyDictionary&lt;int, string&gt;**, or **IEnumerable&lt;System.Collections.Generic.KeyValuePair&lt;TKey, TValue&gt;&gt;**.

> [!IMPORTANT]
> JavaScript uses the interface that appears first in the list of interfaces that a managed type implements. For example, if you return **Dictionary&lt;int, string&gt;** to JavaScript code, it appears as **IDictionary&lt;int, string&gt;** no matter which interface you specify as the return type. This means that if the first interface doesn't include a member that appears on later interfaces, that member isn't visible to JavaScript.

In the Windows Runtime, **IMap&lt;K, V&gt;** and **IMapView&lt;K, V&gt;** are iterated by using IKeyValuePair. When you pass them to managed code, they appear as **IDictionary&lt;TKey, TValue&gt;** and **IReadOnlyDictionary&lt;TKey, TValue&gt;**, so naturally you use **System.Collections.Generic.KeyValuePair&lt;TKey, TValue&gt;** to enumerate them.

The way interfaces appear in managed code affects the way types that implement these interfaces appear. For example, the **PropertySet** class implements **IMap&lt;K, V&gt;**, which appears in managed code as **IDictionary&lt;TKey, TValue&gt;**. **PropertySet** appears as if it implemented **IDictionary&lt;TKey, TValue&gt;** instead of **IMap&lt;K, V&gt;**, so in managed code it appears to have an **Add** method, which behaves like the **Add** method on .NET dictionaries. It doesn't appear to have an **Insert** method. You can see this example in the topic [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md).

## Passing managed types to the Windows Runtime

As discussed in the previous section, some Windows Runtime types can appear as .NET types in the signatures of your component's members, or in the signatures of Windows Runtime members when you use them in the IDE. When you pass .NET types to these members or use them as the return values of your component's members, they appear to the code on the other side as the corresponding Windows Runtime type. For examples of the effects this can have when your component is called from JavaScript, see the "Returning managed types from your component" section in [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md).

## Overloaded methods

In the Windows Runtime, methods can be overloaded. However, if you declare multiple overloads with the same number of parameters, you must apply the [**Windows.Foundation.Metadata.DefaultOverloadAttribute**](/uwp/api/windows.foundation.metadata.defaultoverloadattribute) attribute to only one of those overloads. That overload is the only one you can call from JavaScript. For example, in the following code the overload that takes an **int** (**Integer** in Visual Basic) is the default overload.

```csharp
public string OverloadExample(string s)
{
    return s;
}

[Windows.Foundation.Metadata.DefaultOverload()]
public int OverloadExample(int x)
{
    return x;
}
```

```vb
Public Function OverloadExample(ByVal s As String) As String
    Return s
End Function

<Windows.Foundation.Metadata.DefaultOverload> _
Public Function OverloadExample(ByVal x As Integer) As Integer
    Return x
End Function
```

> [IMPORTANT]
> JavaScript allows you to pass any value to **OverloadExample**, and coerces the value to the type that is required by the parameter. You can call **OverloadExample** with "forty-two", "42", or 42.3, but all those values are passed to the default overload. The default overload in the previous example returns 0, 42, and 42, respectively.

You can't apply the **DefaultOverloadAttribut**e attribute to constructors. All the constructors in a class must have different numbers of parameters.

## Implementing IStringable

Starting with Windows 8.1, the Windows Runtime includes an **IStringable** interface whose single method, **IStringable.ToString**, provides basic formatting support comparable to that provided by **Object.ToString**. If you do choose to implement **IStringable** in a public managed type that is exported in a Windows Runtime component, the following restrictions apply:

-   You can define the **IStringable** interface only in a "class implements" relationship, such as the following code in C#:

    ```cs
    public class NewClass : IStringable
    ```

    Or the following Visual Basic code:

    ```vb
    Public Class NewClass : Implements IStringable
    ```

-   You can't implement **IStringable** on an interface.
-   You can't declare a parameter to be of type **IStringable**.
-   **IStringable** can't be the return type of a method, property, or field.
-   You can't hide your **IStringable** implementation from base classes by using a method definition such as the following:

    ```cs
    public class NewClass : IStringable
    {
       public new string ToString()
       {
          return "New ToString in NewClass";
       }
    }
    ```

    Instead, the **IStringable.ToString** implementation must always override the base class implementation. You can hide a **ToString** implementation only by invoking it on a strongly typed class instance.

> [!NOTE]
> Under a variety of conditions, calls from native code to a managed type that implements **IStringable** or hides its **ToString** implementation can produce unexpected behavior.

## Asynchronous operations

To implement an asynchronous method in your component, add "Async" to the end of the method name and return one of the Windows Runtime interfaces that represent asynchronous actions or operations: **IAsyncAction**, **IAsyncActionWithProgress&lt;TProgress&gt;**, **IAsyncOperation&lt;TResult&gt;**, or **IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**.

You can use .NET tasks (the [**Task**](/dotnet/api/system.threading.tasks.task) class and generic [**Task&lt;TResult&gt;**](/dotnet/api/system.threading.tasks.task-1) class) to implement your asynchronous method. You must return a task that represents an ongoing operation, such as a task that is returned from an asynchronous method written in C# or Visual Basic, or a task that is returned from the [**Task.Run**](/dotnet/api/system.threading.tasks.task.run) method. If you use a constructor to create the task, you must call its [Task.Start](/dotnet/api/system.threading.tasks.task.start) method before returning it.

A method that uses `await` (`Await` in Visual Basic) requires the `async` keyword (`Async` in Visual Basic). If you expose such a method from a Windows Runtime component, apply the `async` keyword to the delegate that you pass to the **Run** method.

For asynchronous actions and operations that do not support cancellation or progress reporting, you can use the [WindowsRuntimeSystemExtensions.AsAsyncAction](/dotnet/api/system) or [AsAsyncOperation&lt;TResult&gt;](/dotnet/api/system) extension method to wrap the task in the appropriate interface. For example, the following code implements an asynchronous method by using the **Task.Run&lt;TResult&gt;** method to start a task. The **AsAsyncOperation&lt;TResult&gt;** extension method returns the task as a Windows Runtime asynchronous operation.

```csharp
public static IAsyncOperation<IList<string>> DownloadAsStringsAsync(string id)
{
    return Task.Run<IList<string>>(async () =>
    {
        var data = await DownloadDataAsync(id);
        return ExtractStrings(data);
    }).AsAsyncOperation();
}
```

```vb
Public Shared Function DownloadAsStringsAsync(ByVal id As String) _
     As IAsyncOperation(Of IList(Of String))

    Return Task.Run(Of IList(Of String))(
        Async Function()
            Dim data = Await DownloadDataAsync(id)
            Return ExtractStrings(data)
        End Function).AsAsyncOperation()
End Function
```

The following JavaScript code shows how the method could be called by using a [**WinJS.Promise**](/previous-versions/windows/apps/br211867(v=win.10)) object. The function that is passed to the then method is executed when the asynchronous call completes. The stringList parameter contains the list of strings that is returned by the **DownloadAsStringAsync** method, and the function does whatever processing is required.

```javascript
function asyncExample(id) {

    var result = SampleComponent.Example.downloadAsStringAsync(id).then(
        function (stringList) {
            // Place code that uses the returned list of strings here.
        });
}
```

For asynchronous actions and operations that support cancellation or progress reporting, use the [**AsyncInfo**](/dotnet/api/system.runtime.interopservices.windowsruntime) class to generate a started task and to hook up the cancellation and progress reporting features of the task with the cancellation and progress reporting features of the appropriate Windows Runtime interface. For an example that supports both cancellation and progress reporting, see [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md).

Note that you can use the methods of the **AsyncInfo** class even if your asynchronous method doesn't support cancellation or progress reporting. If you use a Visual Basic lambda function or a C# anonymous method, don't supply parameters for the token and [**IProgress&lt;T&gt;**](/dotnet/api/system.iprogress-1) interface. If you use a C# lambda function, supply a token parameter but ignore it. The previous example, which used the AsAsyncOperation&lt;TResult&gt; method, looks like this when you use the [**AsyncInfo.Run&lt;TResult&gt;(Func&lt;CancellationToken, Task&lt;TResult&gt;&gt;**](/dotnet/api/system.runtime.interopservices.windowsruntime)) method overload instead.

```csharp
public static IAsyncOperation<IList<string>> DownloadAsStringsAsync(string id)
{
    return AsyncInfo.Run<IList<string>>(async (token) =>
    {
        var data = await DownloadDataAsync(id);
        return ExtractStrings(data);
    });
}
```

```vb
Public Shared Function DownloadAsStringsAsync(ByVal id As String) _
    As IAsyncOperation(Of IList(Of String))

    Return AsyncInfo.Run(Of IList(Of String))(
        Async Function()
            Dim data = Await DownloadDataAsync(id)
            Return ExtractStrings(data)
        End Function)
End Function
```

If you create an asynchronous method that optionally supports cancellation or progress reporting, consider adding overloads that don't have parameters for a cancellation token or the **IProgress&lt;T&gt;** interface.

## Throwing exceptions

You can throw any exception type that is included in the .NET for Windows apps. You can't declare your own public exception types in a Windows Runtime component, but you can declare and throw non-public types.

If your component doesn't handle the exception, a corresponding exception is raised in the code that called your component. The way the exception appears to the caller depends on the way the calling language supports the Windows Runtime.

-   In JavaScript, the exception appears as an object in which the exception message is replaced by a stack trace. When you debug your app in Visual Studio, you can see the original message text displayed in the debugger exception dialog box, identified as "WinRT Information". You can't access the original message text from JavaScript code.

    > **Tip**. Currently, the stack trace contains the managed exception type, but we don't recommend parsing the trace to identify the exception type. Instead, use an HRESULT value as described later in this section.

-   In C++, the exception appears as a platform exception. If the managed exception's HResult property can be mapped to the HRESULT of a specific platform exception, the specific exception is used; otherwise, a [**Platform::COMException**](/cpp/cppcx/platform-comexception-class) exception is thrown. The message text of the managed exception is not available to C++ code. If a specific platform exception was thrown, the default message text for that exception type appears; otherwise, no message text appears. See [Exceptions (C++/CX)](/cpp/cppcx/exceptions-c-cx).
-   In C# or Visual Basic, the exception is a normal managed exception.

When you throw an exception from your component, you can make it easier for a JavaScript or C++ caller to handle the exception by throwing a non-public exception type whose HResult property value is specific to your component. The HRESULT is available to a JavaScript caller through the exception object's number property, and to a C++ caller through the [**COMException::HResult**](/cpp/cppcx/platform-comexception-class#hresult) property.

> [!NOTE]
> Use a negative value for your HRESULT. A positive value is interpreted as success, and no exception is thrown in the JavaScript or C++ caller.

## Declaring and raising events

When you declare a type to hold the data for your event, derive from Object instead of from EventArgs, because EventArgs is not a Windows Runtime type. Use [**EventHandler&lt;TEventArgs&gt;**](/dotnet/api/system.eventhandler-1) as the type of the event, and use your event argument type as the generic type argument. Raise the event just as you would in a .NET application.

When your Windows Runtime component is used from JavaScript or C++, the event follows the Windows Runtime event pattern that those languages expect. When you use the component from C# or Visual Basic, the event appears as an ordinary .NET event. An example is provided in [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](./walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md).

If you implement custom event accessors (declare an event with the **Custom** keyword, in Visual Basic), you must follow the Windows Runtime event pattern in your implementation. See [Custom events and event accessors in Windows Runtime components](custom-events-and-event-accessors-in-windows-runtime-components.md). Note that when you handle the event from C# or Visual Basic code, it still appears to be an ordinary .NET event.

## Next steps

After you’ve created a Windows Runtime component for your own use, you may find that the functionality it encapsulates is useful to other developers. You have two options for packaging a component for distribution to other developers. See [Distributing a managed Windows Runtime component](/previous-versions/windows/apps/jj614475(v=vs.140)).

For more information about Visual Basic and C# language features, and .NET support for the Windows Runtime, see [Visual Basic and C# language reference](/visualstudio/welcome-to-visual-studio-2015?view=vs-2015).

## Related topics
* [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0)
* [Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript](walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript.md)