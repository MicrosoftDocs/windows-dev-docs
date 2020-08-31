---
title: Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript
description: This walkthrough shows how you can use .NET with Visual Basic or C# to create your own Windows Runtime types, packaged in a Windows Runtime component, and how to call the component from your UWP application built for Windows using JavaScript.
ms.assetid: 1565D86C-BF89-4EF3-81FE-35367DB8D671
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Walkthrough of creating a C# or Visual Basic Windows Runtime component, and calling it from JavaScript

This walkthrough shows how you can use .NET with Visual Basic or C# to create your own Windows Runtime types, packaged in a Windows Runtime component, and how then to call that component from a JavaScript Universal Windows Platform (UWP) app.

Visual Studio makes it easy to author and deploy your own custom Windows Runtime types inside a Windows Runtime component (WRC) project written with C# or Visual Basic, and then to reference that WRC from a JavaScript application project, and to consume those custom types from that application.

Internally, your Windows Runtime types can use any .NET functionality that's allowed in a UWP application.

> [!NOTE]
> For more info, see [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md) and [.NET for UWP apps overview](/dotnet/api/index?view=dotnet-uwp-10.0).

Externally, the members of your type can expose only Windows Runtime types for their parameters and return values. When you build your solution, Visual Studio builds your .NET WRC project, and then executes a build step that creates a Windows metadata (.winmd) file. This is your Windows Runtime component, which Visual Studio includes in your app.

> [!NOTE]
> .NET automatically maps some commonly used .NET types, such as primitive data types and collection types, to their Windows Runtime equivalents. These .NET types can be used in the public interface of a Windows Runtime component, and will appear to users of the component as the corresponding Windows Runtime types. See [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md).

## Prerequisites:

- Windows 10
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/)

## Creating a simple Windows Runtime class

This section creates a JavaScript UWP application, and adds to the solution a Visual Basic or C# Windows Runtime component project. It shows how to define a Windows Runtime type, create an instance of the type from JavaScript, and call static and instance members. The visual display of the example app is deliberately low-key in order to keep the focus on the component.

1. In Visual Studio, create a new JavaScript project: On the menu bar, choose **File, New, Project**. In the **Installed Templates** section of the **New Project** dialog box, choose **JavaScript**, and then choose **Windows**, and then **Universal**. (If Windows is not available, make sure you're using Windows 8 or later.) Choose the **Blank Application** template and enter SampleApp for the project name.
2.  Create the component project: In Solution Explorer, open the shortcut menu for the SampleApp solution and choose **Add**, and then choose **New Project** to add a new C# or Visual Basic project to the solution. In the **Installed Templates** section of the **Add New Project** dialog box, choose **Visual Basic** or **Visual C#**, and then choose **Windows**, and then **Universal**. Choose the **Windows Runtime Component** template and enter **SampleComponent** for the project name.
3.  Change the name of the class to **Example**. Notice that by default, the class is marked **public sealed** (**Public NotInheritable** in Visual Basic). All the Windows Runtime classes you expose from your component must be sealed.
4.  Add two simple members to the class, a **static** method (**Shared** method in Visual Basic) and an instance property:

    > [!div class="tabbedCodeSnippets"]
    > ```csharp
    > namespace SampleComponent
    > {
    >     public sealed class Example
    >     {
    >         public static string GetAnswer()
    >         {
    >             return "The answer is 42.";
    >         }
    >
    >         public int SampleProperty { get; set; }
    >     }
    > }
    > ```
    > ```vb
    > Public NotInheritable Class Example
    >     Public Shared Function GetAnswer() As String
    >         Return "The answer is 42."
    >     End Function
    >
    >     Public Property SampleProperty As Integer
    > End Class
    > ```

5.  Optional: To enable IntelliSense for the newly added members, in Solution Explorer, open the shortcut menu for the SampleComponent project, and then choose **Build**.
6.  In Solution Explorer, in the JavaScript project, open the shortcut menu for **References**, and then choose **Add Reference** to open the **Reference Manager**. Choose **Projects**, and then choose **Solution**. Select the check box for the SampleComponent project and choose **OK** to add a reference.

## Call the component from JavaScript

To use the Windows Runtime type from JavaScript, add the following code in the anonymous function in the default.js file (in the js folder of the project) that is provided by the Visual Studio template. It should go after the app.oncheckpoint event handler and before the call to app.start.

```javascript
var ex;

function basics1() {
   document.getElementById('output').innerHTML =
        SampleComponent.Example.getAnswer();

    ex = new SampleComponent.Example();

   document.getElementById('output').innerHTML += "<br/>" +
       ex.sampleProperty;

}

function basics2() {
    ex.sampleProperty += 1;
    document.getElementById('output').innerHTML += "<br/>" +
        ex.sampleProperty;
}
```

Notice that the first letter of each member name is changed from uppercase to lowercase. This transformation is part of the support that JavaScript provides to enable the natural use of the Windows Runtime. Namespaces and class names are Pascal-cased. Member names are camel-cased except for event names, which are all lowercase. See [Using the Windows Runtime in JavaScript](/scripting/jswinrt/using-the-windows-runtime-in-javascript). The rules for camel casing can be confusing. A series of initial uppercase letters normally appears as lowercase, but if three uppercase letters are followed by a lowercase letter, only the first two letters appear in lowercase: for example, a member named IDStringKind appears as idStringKind. In Visual Studio, you can build your Windows Runtime component project and then use IntelliSense in your JavaScript project to see the correct casing.

In similar fashion, .NET provides support to enable the natural use of the Windows Runtime in managed code. This is discussed in subsequent sections of this article, and in the articles [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md) and [.NET support for UWP apps and the Windows Runtime](/dotnet/standard/cross-platform/support-for-windows-store-apps-and-windows-runtime).

## Create a simple user interface

In your JavaScript project, open the default.html file and update the body as shown in the following code. This code includes the complete set of controls for the example app and specifies the function names for the click events.

> **Note**  When you first run the app, only the Basics1 and Basics2 button are supported.

```html
<body>
            <div id="buttons">
            <button id="button1" >Basics 1</button>
            <button id="button2" >Basics 2</button>

            <button id="runtimeButton1">Runtime 1</button>
            <button id="runtimeButton2">Runtime 2</button>

            <button id="returnsButton1">Returns 1</button>
            <button id="returnsButton2">Returns 2</button>

            <button id="events1Button">Events 1</button>

            <button id="btnAsync">Async</button>
            <button id="btnCancel" disabled="disabled">Cancel Async</button>
            <progress id="primeProg" value="25" max="100" style="color: yellow;"></progress>
        </div>
        <div id="output">
        </div>
</body>
```

In your JavaScript project, in the css folder, open default.css. Modify the body section as shown, and add styles to control the layout of buttons and the placement of output text.

```css
body
{
    -ms-grid-columns: 1fr;
    -ms-grid-rows: 1fr 14fr;
    display: -ms-grid;
}

#buttons {
    -ms-grid-rows: 1fr;
    -ms-grid-columns: auto;
    -ms-grid-row-align: start;
}
#output {
    -ms-grid-row: 2;
    -ms-grid-column: 1;
}
```

Now add the event listener registration code by adding a then clause to the processAll call in app.onactivated in default.js. Replace the existing line of code that calls setPromise and change it to the following code:

```javascript
args.setPromise(WinJS.UI.processAll().then(function () {
    var button1 = document.getElementById("button1");
    button1.addEventListener("click", basics1, false);
    var button2 = document.getElementById("button2");
    button2.addEventListener("click", basics2, false);
}));
```

This is a better way to add events to HTML controls than adding a click event handler directly in HTML. See [Create a "Hello, World" app (JS)](../get-started/create-a-hello-world-app-js-uwp.md).

## Build and run the app

Before you build, change the target platform for all projects to ARM, x64, or x86, as appropriate for your computer.

To build and run the solution, choose the F5 key. (If you get a run-time error message stating that SampleComponent is undefined, the reference to the class library project is missing.)

Visual Studio first compiles the class library, and then executes an MSBuild task that runs [Winmdexp.exe (Windows Runtime Metadata Export Tool)](/dotnet/framework/tools/winmdexp-exe-windows-runtime-metadata-export-tool) to create your Windows Runtime component. The component is included in a .winmd file that contains both the managed code and the Windows metadata that describes the code. WinMdExp.exe generates build error messages when you write code that's invalid in a Windows Runtime component, and the error messages are displayed in the Visual Studio IDE. Visual Studio adds your component to the app package (.appx file) for your UWP application, and generates the appropriate manifest.

Choose the Basics 1 button to assign the return value from the static GetAnswer method to the output area, create an instance of the Example class, and display the value of its SampleProperty property in the output area. The output is shown here:

``` syntax
"The answer is 42."
0
```

Choose the Basics 2 button to increment the value of the SampleProperty property and to display the new value in the output area. Primitive types such as strings and numbers can be used as parameter types and return types, and can be passed between managed code and JavaScript. Because numbers in JavaScript are stored in double-precision floating-point format, they are converted to .NET Framework numeric types.

> **Note**  By default, you can set breakpoints only in your JavaScript code. To debug your Visual Basic or C# code, see Creating Windows Runtime components in C# and Visual Basic.

To stop debugging and close your app, switch from the app to Visual Studio, and choose Shift+F5.

## Using the Windows Runtime from JavaScript and managed code

The Windows Runtime can be called from either JavaScript or managed code. Windows Runtime objects can be passed back and forth between the two, and events can be handled from either side. However, the ways you use Windows Runtime types in the two environments differ in some details, because JavaScript and .NET support the Windows Runtime differently. The following example demonstrates these differences, using the [Windows.Foundation.Collections.PropertySet](/uwp/api/windows.foundation.collections.propertyset) class. In this example, you create an instance of the PropertySet collection in managed code and register an event handler to track changes in the collection. Then you add JavaScript code that gets the collection, registers its own event handler, and uses the collection. Finally, you add a method that makes changes to the collection from managed code and shows JavaScript handling a managed exception.

> **Important**  In this example, the event is being fired on the UI thread. If you fire the event from a background thread, for example in an async call, you will need to do some extra work in order for JavaScript to handle the event. For more information, see [Raising Events in Windows Runtime components](raising-events-in-windows-runtime-components.md).

In the SampleComponent project, add a new **public sealed** class (**Public NotInheritable** class in Visual Basic) named PropertySetStats. The class wraps a PropertySet collection and handles its MapChanged event. The event handler tracks the number of changes of each kind that occur, and the DisplayStats method produces a report that is formatted in HTML. Notice the additional **using** statement (**Imports** statement in Visual Basic); be careful to add this to the existing **using** statements rather than overwriting them.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> using Windows.Foundation.Collections;
>
> namespace SampleComponent
> {
>     public sealed class PropertySetStats
>     {
>         private PropertySet _ps;
>         public PropertySetStats()
>         {
>             _ps = new PropertySet();
>             _ps.MapChanged += this.MapChangedHandler;
>         }
>
>         public PropertySet PropertySet { get { return _ps; } }
>
>         int[] counts = { 0, 0, 0, 0 };
>         private void MapChangedHandler(IObservableMap<string, object> sender,
>             IMapChangedEventArgs<string> args)
>         {
>             counts[(int)args.CollectionChange] += 1;
>         }
>
>         public string DisplayStats()
>         {
>             StringBuilder report = new StringBuilder("<br/>Number of changes:<ul>");
>             for (int i = 0; i < counts.Length; i++)
>             {
>                 report.Append("<li>" + (CollectionChange)i + ": " + counts[i] + "</li>");
>             }
>             return report.ToString() + "</ul>";
>         }
>     }
> }
> ```
> ```vb
> Imports System.Text
>
> Public NotInheritable Class PropertySetStats
>     Private _ps As PropertySet
>     Public Sub New()
>         _ps = New PropertySet()
>         AddHandler _ps.MapChanged, AddressOf Me.MapChangedHandler
>     End Sub
>
>     Public ReadOnly Property PropertySet As PropertySet
>         Get
>             Return _ps
>         End Get
>     End Property
>
>     Dim counts() As Integer = {0, 0, 0, 0}
>     Private Sub MapChangedHandler(ByVal sender As IObservableMap(Of String, Object),
>         ByVal args As IMapChangedEventArgs(Of String))
>
>         counts(CInt(args.CollectionChange)) += 1
>     End Sub
>
>     Public Function DisplayStats() As String
>         Dim report As New StringBuilder("<br/>Number of changes:<ul>")
>         For i As Integer = 0 To counts.Length - 1
>             report.Append("<li>" & CType(i, CollectionChange).ToString() &
>                           ": " & counts(i) & "</li>")
>         Next
>         Return report.ToString() & "</ul>"
>     End Function
> End Class
> ```

The event handler follows the familiar .NET Framework event pattern, except that the sender of the event (in this case, the PropertySet object) is cast to the IObservableMap&lt;string, object&gt; interface (IObservableMap(Of String, Object) in Visual Basic), which is an instantiation of the Windows Runtime interface [IObservableMap&lt;K, V&gt;](/uwp/api/Windows.Foundation.Collections.IObservableMap_K_V_). (You can cast the sender to its type if necessary.) Also, the event arguments are presented as an interface rather than as an object.

In the default.js file, add the Runtime1 function as shown. This code creates a PropertySetStats object, gets its PropertySet collection, and adds its own event handler, the onMapChanged function, to handle the MapChanged event. After making changes to the collection, runtime1 calls the DisplayStats method to show a summary of change types.

```javascript
var propertysetstats;

function runtime1() {
    document.getElementById('output').innerHTML = "";

    propertysetstats = new SampleComponent.PropertySetStats();
    var propertyset = propertysetstats.propertySet;

    propertyset.addEventListener("mapchanged", onMapChanged);

    propertyset.insert("FirstProperty", "First property value");
    propertyset.insert("SuperfluousProperty", "Unnecessary property value");
    propertyset.insert("AnotherProperty", "A property value");

    propertyset.insert("SuperfluousProperty", "Altered property value")
    propertyset.remove("SuperfluousProperty");

    document.getElementById('output').innerHTML +=
        propertysetstats.displayStats();
}

function onMapChanged(change) {
    var result
    switch (change.collectionChange) {
        case Windows.Foundation.Collections.CollectionChange.reset:
            result = "All properties cleared";
            break;
        case Windows.Foundation.Collections.CollectionChange.itemInserted:
            result = "Inserted " + change.key + ": '" +
                change.target.lookup(change.key) + "'";
            break;
        case Windows.Foundation.Collections.CollectionChange.itemRemoved:
            result = "Removed " + change.key;
            break;
        case Windows.Foundation.Collections.CollectionChange.itemChanged:
            result = "Changed " + change.key + " to '" +
                change.target.lookup(change.key) + "'";
            break;
        default:
            break;
     }

     document.getElementById('output').innerHTML +=
         "<br/>" + result;
}
```

The way you handle Windows Runtime events in JavaScript is very different from the way you handle them in .NET Framework code. The JavaScript event handler takes only one argument. When you view this object in the Visual Studio debugger, the first property is the sender. The members of the event argument interface also appear directly on this object.

To run the app, choose the F5 key. If the class is not sealed, you get the error message, "Exporting unsealed type 'SampleComponent.Example' is not currently supported. Please mark it as sealed."

Choose the **Runtime 1** button. The event handler displays changes as elements are added or changed, and at the end the DisplayStats method is called to produce a summary of counts. To stop debugging and close the app, switch back to Visual Studio and choose Shift+F5.

To add two more items to the PropertySet collection from managed code, add the following code to the PropertySetStats class:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> public void AddMore()
> {
>     _ps.Add("NewProperty", "New property value");
>     _ps.Add("AnotherProperty", "A property value");
> }
> ```
> ```vb
> Public Sub AddMore()
>     _ps.Add("NewProperty", "New property value")
>     _ps.Add("AnotherProperty", "A property value")
> End Sub
> ```

This code highlights another difference in the way you use Windows Runtime types in the two environments. If you type this code yourself, you'll notice that IntelliSense doesn't show the insert method you used in the JavaScript code. Instead, it shows the Add method commonly seen on collections in .NET. This is because some commonly used collection interfaces have different names but similar functionality in the Windows Runtime and .NET. When you use these interfaces in managed code, they appear as their .NET Framework equivalents. This is discussed in [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md). When you use the same interfaces in JavaScript, the only change from the Windows Runtime is that uppercase letters at the beginning of member names become lowercase.

Finally, to call the AddMore method with exception handling, add the runtime2 function to default.js.

```javascript
function runtime2() {
   try {
      propertysetstats.addMore();
    }
   catch(ex) {
       document.getElementById('output').innerHTML +=
          "<br/><b>" + ex + "<br/>";
   }

   document.getElementById('output').innerHTML +=
       propertysetstats.displayStats();
}
```

Add the event handler registration code the same way you did previously.

```javascript
var runtimeButton1 = document.getElementById("runtimeButton1");
runtimeButton1.addEventListener("click", runtime1, false);
var runtimeButton2 = document.getElementById("runtimeButton2");
runtimeButton2.addEventListener("click", runtime2, false);
```

To run the app, choose the F5 key. Choose **Runtime 1** and then **Runtime 2**. The JavaScript event handler reports the first change to the collection. The second change, however, has a duplicate key. Users of .NET Framework dictionaries expect the Add method to throw an exception, and that is what happens. JavaScript handles the .NET exception.

> **Note**  You can't display the exception's message from JavaScript code. The message text is replaced by a stack trace. For more information, see "Throwing exceptions" in Creating Windows Runtime components in C# and Visual Basic.

By contrast, when JavaScript called the insert method with a duplicate key, the value of the item was changed. This difference in behavior is due to the different ways that JavaScript and .NET support the Windows Runtime, as explained in [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md).

## Returning managed types from your component

As discussed previously, you can pass native Windows Runtime types back and forth freely between your JavaScript code and your C# or Visual Basic code. Most of the time, the type names and member names will be the same in both cases (except that the member names start with lowercase letters in JavaScript). However, in the preceding section, the PropertySet class appeared to have different members in managed code. (For example, in JavaScript you called the insert method, and in the .NET code you called the Add method.) This section explores the way those differences affect .NET Framework types passed to JavaScript.

In addition to returning Windows Runtime types that you created in your component or passed to your component from JavaScript, you can return a managed type, created in managed code, to JavaScript as if it were the corresponding Windows Runtime type. Even in the first, simple example of a runtime class, the parameters and return types of the members were Visual Basic or C# primitive types, which are .NET Framework types. To demonstrate this for collections, add the following code to the Example class, to create a method that returns a generic dictionary of strings, indexed by integers:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> public static IDictionary<int, string> GetMapOfNames()
> {
>     Dictionary<int, string> retval = new Dictionary<int, string>();
>     retval.Add(1, "one");
>     retval.Add(2, "two");
>     retval.Add(3, "three");
>     retval.Add(42, "forty-two");
>     retval.Add(100, "one hundred");
>     return retval;
> }
> ```
> ```vb
> Public Shared Function GetMapOfNames() As IDictionary(Of Integer, String)
>     Dim retval As New Dictionary(Of Integer, String)
>     retval.Add(1, "one")
>     retval.Add(2, "two")
>     retval.Add(3, "three")
>     retval.Add(42, "forty-two")
>     retval.Add(100, "one hundred")
>     Return retval
> End Function
> ```

Notice that the dictionary must be returned as an interface that is implemented by [Dictionary&lt;TKey, TValue&gt;](/dotnet/api/system.collections.generic.dictionary-2), and that maps to a Windows Runtime interface. In this case, the interface is IDictionary&lt;int, string&gt; (IDictionary(Of Integer, String) in Visual Basic). When the Windows Runtime type IMap&lt;int, string&gt; is passed to managed code, it appears as IDictionary&lt;int, string&gt;, and the reverse is true when the managed type is passed to JavaScript.

**Important**  When a managed type implements multiple interfaces, JavaScript uses the interface that appears first in the list. For example, if you return Dictionary&lt;int, string&gt; to JavaScript code, it appears as IDictionary&lt;int, string&gt; no matter which interface you specify as the return type. This means that if the first interface doesn't include a member that appears on later interfaces, that member isn't visible to JavaScript.

 

To test the new method and use the dictionary, add the returns1 and returns2 functions to default.js:

```javascript
var names;

function returns1() {
    names = SampleComponent.Example.getMapOfNames();
    document.getElementById('output').innerHTML = showMap(names);
}

var ct = 7;

function returns2() {
    if (!names.hasKey(17)) {
        names.insert(43, "forty-three");
        names.insert(17, "seventeen");
    }
    else {
        var err = names.insert("7", ct++);
        names.insert("forty", "forty");
    }
    document.getElementById('output').innerHTML = showMap(names);
}

function showMap(map) {
    var item = map.first();
    var retval = "<ul>";

    for (var i = 0, len = map.size; i < len; i++) {
        retval += "<li>" + item.current.key + ": " + item.current.value + "</li>";
        item.moveNext();
    }
    return retval + "</ul>";
}
```

Add the event registration code to the same then block as the other event registration code:

```javascript
var returnsButton1 = document.getElementById("returnsButton1");
returnsButton1.addEventListener("click", returns1, false);
var returnsButton2 = document.getElementById("returnsButton2");
returnsButton2.addEventListener("click", returns2, false);
```

There are a few interesting things to observe about this JavaScript code. First of all, it includes a showMap function to display the contents of the dictionary in HTML. In the code for showMap, notice the iteration pattern. In .NET, there's no First method on the generic IDictionary interface, and the size is returned by a Count property rather than by a Size method. To JavaScript, IDictionary&lt;int, string&gt; appears to be the Windows Runtime type IMap&lt;int, string&gt;. (See the [IMap&lt;K,V&gt;](/uwp/api/Windows.Foundation.Collections.IMap_K_V_) interface.)

In the returns2 function, as in earlier examples, JavaScript calls the Insert method (insert in JavaScript) to add items to the dictionary.

To run the app, choose the F5 key. To create and display the initial contents of the dictionary, choose the **Returns 1** button. To add two more entries to the dictionary, choose the **Returns 2** button. Notice that the entries are displayed in order of insertion, as you would expect from Dictionary&lt;TKey, TValue&gt;. If you want them sorted, you can return a SortedDictionary&lt;int, string&gt; from GetMapOfNames. (The PropertySet class used in earlier examples has a different internal organization from Dictionary&lt;TKey, TValue&gt;.)

Of course, JavaScript is not a strongly typed language, so using strongly typed generic collections can lead to some surprising results. Choose the **Returns 2** button again. JavaScript obligingly coerces the "7" to a numeric 7, and the numeric 7 that's stored in ct to a string. And it coerces the string "forty" to zero. But that's only the beginning. Choose the **Returns 2** button a few more times. In managed code, the Add method would generate duplicate key exceptions, even if the values were cast to the correct types. In contrast, the Insert method updates the value associated with an existing key and returns a Boolean value that indicates whether a new key was added to the dictionary. This is why the value associated with the key 7 keeps changing.

Another unexpected behavior: If you pass an unassigned JavaScript variable as a string argument, what you get is the string "undefined". In short, be careful when you pass .NET Framework collection types to your JavaScript code.

> **Note**  If you have large quantities of text to concatenate, you can do it more efficiently by moving the code into a .NET Framework method and using the StringBuilder class, as shown in the showMap function.

Although you can't expose your own generic types from a Windows Runtime component, you can return .NET Framework generic collections for Windows Runtime classes by using code such as the following:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> public static object GetListOfThis(object obj)
> {
>     Type target = obj.GetType();
>     return Activator.CreateInstance(typeof(List<>).MakeGenericType(target));
> }
> ```
> ```vb
> Public Shared Function GetListOfThis(obj As Object) As Object
>     Dim target As Type = obj.GetType()
>     Return Activator.CreateInstance(GetType(List(Of )).MakeGenericType(target))
> End Function
> ```

List&lt;T&gt; implements IList&lt;T&gt;, which appears as the Windows Runtime type IVector&lt;T&gt; in JavaScript.

## Declaring events


You can declare events by using the standard .NET Framework event pattern or other patterns used by the Windows Runtime. The .NET Framework supports equivalence between the System.EventHandler&lt;TEventArgs&gt; delegate and the Windows Runtime EventHandler&lt;T&gt; delegate, so using EventHandler&lt;TEventArgs&gt; is a good way to implement the standard .NET Framework pattern. To see how this works, add the following pair of classes to the SampleComponent project:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> namespace SampleComponent
> {
>     public sealed class Eventful
>     {
>         public event EventHandler<TestEventArgs> Test;
>         public void OnTest(string msg, long number)
>         {
>             EventHandler<TestEventArgs> temp = Test;
>             if (temp != null)
>             {
>                 temp(this, new TestEventArgs()
>                 {
>                     Value1 = msg,
>                     Value2 = number
>                 });
>             }
>         }
>     }
>
>     public sealed class TestEventArgs
>     {
>         public string Value1 { get; set; }
>         public long Value2 { get; set; }
>     }
> }
> ```
> ```vb
> Public NotInheritable Class Eventful
>     Public Event Test As EventHandler(Of TestEventArgs)
>     Public Sub OnTest(ByVal msg As String, ByVal number As Long)
>         RaiseEvent Test(Me, New TestEventArgs() With {
>                             .Value1 = msg,
>                             .Value2 = number
>                             })
>     End Sub
> End Class
>
> Public NotInheritable Class TestEventArgs
>     Public Property Value1 As String
>     Public Property Value2 As Long
> End Class
> ```

When you expose an event in the Windows Runtime, the event argument class inherits from System.Object. It doesn't inherit from System.EventArgs, as it would in .NET, because EventArgs is not a Windows Runtime type.

If you declare custom event accessors for your event (**Custom** keyword in Visual Basic), you must use the Windows Runtime event pattern. See [Custom events and event accessors in Windows Runtime components](custom-events-and-event-accessors-in-windows-runtime-components.md).

To handle the Test event, add the events1 function to default.js. The events1 function creates an event handler function for the Test event, and immediately invokes the OnTest method to raise the event. If you place a breakpoint in the body of the event handler, you can see that the object passed to the single parameter includes the source object and both members of TestEventArgs.

```javascript
var ev;

function events1() {
   ev = new SampleComponent.Eventful();
   ev.addEventListener("test", function (e) {
       document.getElementById('output').innerHTML = e.value1;
       document.getElementById('output').innerHTML += "<br/>" + e.value2;
   });
   ev.onTest("Number of feet in a mile:", 5280);
}
```

Add the event registration code to the same then block as the other event registration code:

```javascript
var events1Button = document.getElementById("events1Button");
events1Button.addEventListener("click", events1, false);
```

## Exposing asynchronous operations


The .NET Framework has a rich set of tools for asynchronous processing and parallel processing, based on the Task and generic [Task&lt;TResult&gt;](/dotnet/api/system.threading.tasks.task-1) classes. To expose task-based asynchronous processing in a Windows Runtime component, use the Windows Runtime interfaces [IAsyncAction](/windows/desktop/api/windows.foundation/nn-windows-foundation-iasyncaction), [IAsyncActionWithProgress&lt;TProgress&gt;](/previous-versions/br205784(v=vs.85)), [IAsyncOperation&lt;TResult&gt;](/previous-versions/br205802(v=vs.85)), and [IAsyncOperationWithProgress&lt;TResult, TProgress&gt;](/previous-versions/br205807(v=vs.85)). (In the Windows Runtime, operations return results, but actions do not.)

This section demonstrates a cancelable asynchronous operation that reports progress and returns results. The GetPrimesInRangeAsync method uses the [AsyncInfo](/dotnet/api/system.runtime.interopservices.windowsruntime) class to generate a task and to connect its cancellation and progress-reporting features to a WinJS.Promise object. Begin by adding the GetPrimesInRangeAsync method to the example class:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> using System.Runtime.InteropServices.WindowsRuntime;
> using Windows.Foundation;
>
> public static IAsyncOperationWithProgress<IList<long>, double>
> GetPrimesInRangeAsync(long start, long count)
> {
>     if (start < 2 || count < 1) throw new ArgumentException();
>
>     return AsyncInfo.Run<IList<long>, double>((token, progress) =>
>
>         Task.Run<IList<long>>(() =>
>         {
>             List<long> primes = new List<long>();
>             double onePercent = count / 100;
>             long ctProgress = 0;
>             double nextProgress = onePercent;
>
>             for (long candidate = start; candidate < start + count; candidate++)
>             {
>                 ctProgress += 1;
>                 if (ctProgress >= nextProgress)
>                 {
>                     progress.Report(ctProgress / onePercent);
>                     nextProgress += onePercent;
>                 }
>                 bool isPrime = true;
>                 for (long i = 2, limit = (long)Math.Sqrt(candidate); i <= limit; i++)
>                 {
>                     if (candidate % i == 0)
>                     {
>                         isPrime = false;
>                         break;
>                     }
>                 }
>                 if (isPrime) primes.Add(candidate);
>
>                 token.ThrowIfCancellationRequested();
>             }
>             progress.Report(100.0);
>             return primes;
>         }, token)
>     );
> }
> ```
> ```vb
> Imports System.Runtime.InteropServices.WindowsRuntime
>
> Public Shared Function GetPrimesInRangeAsync(ByVal start As Long, ByVal count As Long)
> As IAsyncOperationWithProgress(Of IList(Of Long), Double)
>
>     If (start < 2 Or count < 1) Then Throw New ArgumentException()
>
>     Return AsyncInfo.Run(Of IList(Of Long), Double)( _
>         Function(token, prog)
>             Return Task.Run(Of IList(Of Long))( _
>                 Function()
>                     Dim primes As New List(Of Long)
>                     Dim onePercent As Long = count / 100
>                     Dim ctProgress As Long = 0
>                     Dim nextProgress As Long = onePercent
>
>                     For candidate As Long = start To start + count - 1
>                         ctProgress += 1
>
>                         If ctProgress >= nextProgress Then
>                             prog.Report(ctProgress / onePercent)
>                             nextProgress += onePercent
>                         End If
>
>                         Dim isPrime As Boolean = True
>                         For i As Long = 2 To CLng(Math.Sqrt(candidate))
>                             If (candidate Mod i) = 0 Then
>                                 isPrime = False
>                                 Exit For
>                             End If
>                         Next
>
>                         If isPrime Then primes.Add(candidate)
>
>                         token.ThrowIfCancellationRequested()
>                     Next
>                     prog.Report(100.0)
>                     Return primes
>                 End Function, token)
>         End Function)
> End Function
> ```

GetPrimesInRangeAsync is a very simple prime number finder, and that's by design. The focus here is on implementing an asynchronous operation, so simplicity is important, and a slow implementation is an advantage when we're demonstrating cancellation. GetPrimesInRangeAsync finds primes by brute force: It divides a candidate by all the integers that are less than or equal to its square root, rather than using only the prime numbers. Stepping through this code:

-   Before starting an asynchronous operation, perform housekeeping activities such as validating parameters and throwing exceptions for invalid input.
-   The key to this implementation is the [AsyncInfo.Run&lt;TResult, TProgress&gt;(Func&lt;CancellationToken, IProgress&lt;TProgress&gt;, Task&lt;TResult&gt;](/dotnet/api/system.runtime.interopservices.windowsruntime)&gt;) method, and the delegate that is the method's only parameter. The delegate must accept a cancellation token and an interface for reporting progress, and must return a started task that uses those parameters. When JavaScript calls the GetPrimesInRangeAsync method, the following steps occur (not necessarily in the order given here):

    -   The [WinJS.Promise](/previous-versions/windows/apps/br211867(v=win.10)) object supplies functions to process the returned results, react to cancellation, and handle progress reports.
    -   The AsyncInfo.Run method creates a cancellation source and an object that implements the IProgress&lt;T&gt; interface. To the delegate, it passes both a [CancellationToken](/dotnet/api/system.threading.cancellationtoken) token from the cancellation source, and the [IProgress&lt;T&gt;](/dotnet/api/system.iprogress-1) interface.

        > **Note**  If the Promise object doesn't supply a function to react to cancellation, AsyncInfo.Run still passes a cancelable token, and cancellation can still occur. If the Promise object doesn't supply a function to handle progress updates, AsyncInfo.Run still supplies an object that implements IProgress&lt;T&gt;, but its reports are ignored.

    -   The delegate uses the [Task.Run&lt;TResult&gt;(Func&lt;TResult&gt;, CancellationToken](/dotnet/api/system.threading.tasks.task.run#System_Threading_Tasks_Task_Run__1_System_Func___0__System_Threading_CancellationToken_)) method to create a started task that uses the token and the progress interface. The delegate for the started task is provided by a lambda function that computes the desired result. More about that in a moment.
    -   The AsyncInfo.Run method creates an object that implements the [IAsyncOperationWithProgress&lt;TResult, TProgress&gt;](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_) interface, connects the Windows Runtime cancellation mechanism with the token source, and connects the Promise object's progress-reporting function with the IProgress&lt;T&gt; interface.
    -   The IAsyncOperationWithProgress&lt;TResult, TProgress&gt; interface is returned to JavaScript.

-   The lambda function that is represented by the started task doesn't take any arguments. Because it's a lambda function, it has access to the token and the IProgress interface. Each time a candidate number is evaluated, the lambda function:

    -   Checks to see whether the next percentage point of progress has been reached. If it has, the lambda function calls the IProgress&lt;T&gt;.Report method, and the percentage is passed through to the function that the Promise object specified for reporting progress.
    -   Uses the cancellation token to throw an exception if the operation has been canceled. If the [IAsyncInfo.Cancel](/uwp/api/windows.foundation.iasyncinfo.cancel) method (which the IAsyncOperationWithProgress&lt;TResult, TProgress&gt; interface inherits) has been called, the connection that the AsyncInfo.Run method set up ensures that the cancellation token is notified.
-   When the lambda function returns the list of prime numbers, the list is passed to the function that the WinJS.Promise object specified for processing the results.

To create the JavaScript promise and set up the cancellation mechanism, add the asyncRun and asyncCancel functions to default.js.

```javascript
var resultAsync;
function asyncRun() {
    document.getElementById('output').innerHTML = "Retrieving prime numbers.";
    btnAsync.disabled = "disabled";
    btnCancel.disabled = "";

    resultAsync = SampleComponent.Example.getPrimesInRangeAsync(10000000000001, 2500).then(
        function (primes) {
            for (i = 0; i < primes.length; i++)
                document.getElementById('output').innerHTML += " " + primes[i];

            btnCancel.disabled = "disabled";
            btnAsync.disabled = "";
        },
        function () {
            document.getElementById('output').innerHTML += " -- getPrimesInRangeAsync was canceled. -- ";

            btnCancel.disabled = "disabled";
            btnAsync.disabled = "";
        },
        function (prog) {
            document.getElementById('primeProg').value = prog;
        }
    );
}

function asyncCancel() {    
    resultAsync.cancel();
}
```

Don't forget the event registration code the same as you did previously.

```javascript
var btnAsync = document.getElementById("btnAsync");
btnAsync.addEventListener("click", asyncRun, false);
var btnCancel = document.getElementById("btnCancel");
btnCancel.addEventListener("click", asyncCancel, false);
```

By calling the asynchronous GetPrimesInRangeAsync method, the asyncRun function creates a WinJS.Promise object. The object's then method takes three functions that process the returned results, react to errors (including cancellation), and handle progress reports. In this example, the returned results are printed in the output area. Cancellation or completion resets the buttons that launch and cancel the operation. Progress reporting updates the progress control.

The asyncCancel function just calls the cancel method of the WinJS.Promise object.

To run the app, choose the F5 key. To start the asynchronous operation, choose the **Async** button. What happens next depends on how fast your computer is. If the progress bar zips to completion before you have time to blink, increase the size of the starting number that is passed to GetPrimesInRangeAsync by one or more factors of ten. You can fine-tune the duration of the operation by increasing or decreasing the count of numbers to test, but adding zeros in the middle of the starting number will have a bigger impact. To cancel the operation, choose the **Cancel Async** button.

## Related topics

* [.NET for UWP apps overview](/previous-versions/windows/apps/br230302(v=vs.140))
* [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0)