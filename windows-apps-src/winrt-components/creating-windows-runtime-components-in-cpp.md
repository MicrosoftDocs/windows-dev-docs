---
title: Windows Runtime components with C++/CX
description: This topic shows how to use C++/CX to create a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language.
ms.assetid: F7E06AA2-DCEC-427E-BD5D-9CA2A0ED2612
ms.date: 05/14/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Windows Runtime components with C++/CX

> [!NOTE]
> This topic exists to help you maintain your C++/CX application. But we recommend that you use [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) for new applications. C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. To learn how to create a Windows Runtime component using C++/WinRT, see [Windows Runtime components with C++/WinRT](./create-a-windows-runtime-component-in-cppwinrt.md).

This topic shows how to use C++/CX to create a Windows Runtime component&mdash;a component that's callable from a Universal Windows app built using any Windows Runtime language (C#, Visual Basic, C++, or Javascript).

There are several reasons for building a Windows Runtime component in C++.
- To get the performance advantage of C++ in complex or computationally intensive operations.
- To reuse code that's already written and tested.

When you build a solution that contains a JavaScript or .NET project, and a Windows Runtime component project, the JavaScript project files and the compiled DLL are merged into one package, which you can debug locally in the simulator or remotely on a tethered device. You can also distribute just the component project as an Extension SDK. For more information, see [Creating a Software Development Kit](/visualstudio/extensibility/creating-a-software-development-kit?view=vs-2015).

In general, when you code your C++/CX component, use the regular C++ library and built-in types, except at the abstract binary interface (ABI) boundary where you are passing data to and from code in another .winmd package. There, use Windows Runtime types and the special syntax that C++/CX supports for creating and manipulating those types. In addition, in your C++/CX code, use types such as delegate and event to implement events that can be raised from your component and handled in JavaScript, Visual Basic, C++, or C#. For more information about the C++/CX syntax, see [Visual C++ Language Reference (C++/CX)](/cpp/cppcx/visual-c-language-reference-c-cx).

## Casing and naming rules

### JavaScript
JavaScript is case-sensitive. Therefore, you must follow these casing conventions:

-   When you reference C++ namespaces and classes, use the same casing that's used on the C++ side.
-   When you call methods, use camel casing even if the method name is capitalized on the C++ side. For example, a C++ method GetDate() must be called from JavaScript as getDate().
-   An activatable class name and namespace name can't contain UNICODE characters.

### .NET
The .NET languages follow their normal casing rules.

## Instantiating the object
Only Windows Runtime types can be passed across the ABI boundary. The compiler will raise an error if the component has a type like std::wstring as a return type or parameter in a public method. The Visual C++ component extensions (C++/CX) built-in types include the usual scalars such as int and double, and also their typedef equivalents int32, float64, and so on. For more information, see [Type System (C++/CX)](/cpp/cppcx/type-system-c-cx).

```cpp
// ref class definition in C++
public ref class SampleRefClass sealed
{
    // Class members...

    // #include <valarray>
public:
    double LogCalc(double input)
    {
        // Use C++ standard library as usual.
        return std::log(input);
    }

};
```

```javascript
//Instantiation in JavaScript (requires "Add reference > Project reference")
var nativeObject = new CppComponent.SampleRefClass();
```

```csharp
//Call a method and display result in a XAML TextBlock
var num = nativeObject.LogCalc(21.5);
ResultText.Text = num.ToString();
```

## C++/CX built-in types, library types, and Windows Runtime types
An activatable class (also known as a ref class) is one that can be instantiated from another language such as JavaScript, C# or Visual Basic. To be consumable from another language, a component must contain at least one activatable class.

A Windows Runtime component can contain multiple public activatable classes as well as additional classes that are known only internally to the component. Apply the [WebHostHidden](/uwp/api/windows.foundation.metadata.webhosthiddenattribute) attribute to C++/CX types that are not intended to be visible to JavaScript.

All public classes must reside in the same root namespace which has the same name as the component metadata file. For example, a class that's named A.B.C.MyClass can be instantiated only if it's defined in a metadata file that's named A.winmd or A.B.winmd or A.B.C.winmd. The name of the DLL is not required to match the .winmd file name.

Client code creates an instance of the component by using the **new** (**New** in Visual Basic) keyword just as for any class.

An activatable class must be declared as **public ref class sealed**. The **ref class** keyword tells the compiler to create the class as a Windows Runtime compatible type, and the sealed keyword specifies that the class cannot be inherited. The Windows Runtime does not currently support a generalized inheritance model; a limited inheritance model supports creation of custom XAML controls. For more information, see [Ref classes and structs (C++/CX)](/cpp/cppcx/ref-classes-and-structs-c-cx).

For C++/CX, all the numeric primitives are defined in the default namespace. The [Platform](/cpp/cppcx/platform-namespace-c-cx) namespace contains C++/CX classes that are specific to the Windows Runtime type system. These include [Platform::String](/cpp/cppcx/platform-string-class) class and [Platform::Object](/cpp/cppcx/platform-object-class) class. The concrete collection types such as [Platform::Collections::Map](/cpp/cppcx/platform-collections-map-class) class and [Platform::Collections::Vector](/cpp/cppcx/platform-collections-vector-class) class are defined in the [Platform::Collections](/cpp/cppcx/platform-collections-namespace) namespace. The public interfaces that these types implement are defined in [Windows::Foundation::Collections Namespace (C++/CX)](/cpp/cppcx/windows-foundation-collections-namespace-c-cx). It is these interface types that are consumed by JavaScript, C# and Visual Basic. For more information, see [Type System (C++/CX)](/cpp/cppcx/type-system-c-cx).

## Method that returns a value of built-in type
```cpp
    // #include <valarray>
public:
    double LogCalc(double input)
    {
        // Use C++ standard library as usual.
        return std::log(input);
    }
```

```javascript
//Call a method
var nativeObject = new CppComponent.SampleRefClass;
var num = nativeObject.logCalc(21.5);
document.getElementById('P2').innerHTML = num;
```

## Method that returns a custom value struct
```cpp
namespace CppComponent
{
    // Custom struct
    public value struct PlayerData
    {
        Platform::String^ Name;
        int Number;
        double ScoringAverage;
    };

    public ref class Player sealed
    {
    private:
        PlayerData m_player;
    public:
        property PlayerData PlayerStats
        {
            PlayerData get(){ return m_player; }
            void set(PlayerData data) {m_player = data;}
        }
    };
}
```

To pass user-defined value structs across the ABI, define a JavaScript object that has the same members as the value struct that's defined in C++/CX. You can then pass that object as an argument to a C++/CX method so that the object is implicitly converted to the C++/CX type.

```javascript
// Get and set the value struct
function GetAndSetPlayerData() {
    // Create an object to pass to C++
    var myData =
        { name: "Bob Homer", number: 12, scoringAverage: .357 };
    var nativeObject = new CppComponent.Player();
    nativeObject.playerStats = myData;

    // Retrieve C++ value struct into new JavaScript object
    var myData2 = nativeObject.playerStats;
    document.getElementById('P3').innerHTML = myData.name + " , " + myData.number + " , " + myData.scoringAverage.toPrecision(3);
}
```

Another approach is to define a class that implements IPropertySet (not shown).

In the .NET languages, you just create a variable of the type that's defined in the C++/CX component.

```csharp
private void GetAndSetPlayerData()
{
    // Create a ref class
    var player = new CppComponent.Player();

    // Create a variable of a value struct
    // type that is defined in C++
    CppComponent.PlayerData myPlayer;
    myPlayer.Name = "Babe Ruth";
    myPlayer.Number = 12;
    myPlayer.ScoringAverage = .398;

    // Set the property
    player.PlayerStats = myPlayer;

    // Get the property and store it in a new variable
    CppComponent.PlayerData myPlayer2 = player.PlayerStats;
    ResultText.Text += myPlayer.Name + " , " + myPlayer.Number.ToString() +
        " , " + myPlayer.ScoringAverage.ToString();
}
```

## Overloaded Methods
A C++/CX public ref class can contain overloaded methods, but JavaScript has limited ability to differentiate overloaded methods. For example, it can tell the difference between these signatures:

```cpp
public ref class NumberClass sealed
{
public:
    int GetNumber(int i);
    int GetNumber(int i, Platform::String^ str);
    double GetNumber(int i, MyData^ d);
};
```

But it can’t tell the difference between these:

```cpp
int GetNumber(int i);
double GetNumber(double d);
```

In ambiguous cases, you can ensure that JavaScript always calls a specific overload by applying the [Windows::Foundation::Metadata::DefaultOverload](/uwp/api/windows.foundation.metadata.defaultoverloadattribute) attribute to the method signature in the header file.

This JavaScript always calls the attributed overload:

```javascript
var nativeObject = new CppComponent.NumberClass();
var num = nativeObject.getNumber(9);
document.getElementById('P4').innerHTML = num;
```

## .NET
The .NET languages recognize overloads in a C++/CX ref class just as in any .NET class.

## DateTime
In the Windows Runtime, a [Windows::Foundation::DateTime](/uwp/api/windows.foundation.datetime) object is just a 64-bit signed integer that represents the number of 100-nanosecond intervals either before or after January 1, 1601. There are no methods on a Windows:Foundation::DateTime object. Instead, each language projects the DateTime in the way that is native to that language: the Date object in JavaScript and the System.DateTime and System.DateTimeOffset types in .NET.

```cpp
public  ref class MyDateClass sealed
{
public:
    property Windows::Foundation::DateTime TimeStamp;
    void SetTime(Windows::Foundation::DateTime dt)
    {
        auto cal = ref new Windows::Globalization::Calendar();
        cal->SetDateTime(dt);
        TimeStamp = cal->GetDateTime(); // or TimeStamp = dt;
    }
};
```

When you pass a DateTime value from C++/CX to JavaScript, JavaScript accepts it as a Date object and displays it by default as a long-form date string.

```javascript
function SetAndGetDate() {
    var nativeObject = new CppComponent.MyDateClass();

    var myDate = new Date(1956, 4, 21);
    nativeObject.setTime(myDate);

    var myDate2 = nativeObject.timeStamp;

    //prints long form date string
    document.getElementById('P5').innerHTML = myDate2;

}
```

When a .NET language passes a System.DateTime to a C++/CX component, the method accepts it as a Windows::Foundation::DateTime. When the component passes a Windows::Foundation::DateTime to a .NET method, the Framework method accepts it as a DateTimeOffset.

```csharp
private void DateTimeExample()
{
    // Pass a System.DateTime to a C++ method
    // that takes a Windows::Foundation::DateTime
    DateTime dt = DateTime.Now;
    var nativeObject = new CppComponent.MyDateClass();
    nativeObject.SetTime(dt);

    // Retrieve a Windows::Foundation::DateTime as a
    // System.DateTimeOffset
    DateTimeOffset myDate = nativeObject.TimeStamp;

    // Print the long-form date string
    ResultText.Text += myDate.ToString();
}
```

## Collections and arrays
Collections are always passed across the ABI boundary as handles to Windows Runtime types such as Windows::Foundation::Collections::IVector^ and Windows::Foundation::Collections::IMap^. For example, if you return a handle to a Platform::Collections::Map, it implicitly converts to a Windows::Foundation::Collections::IMap^. The collection interfaces are defined in a namespace that's separate from the C++/CX classes that provide the concrete implementations. JavaScript and .NET languages consume the interfaces. For more information, see [Collections (C++/CX)](/cpp/cppcx/collections-c-cx) and [Array and WriteOnlyArray (C++/CX)](/cpp/cppcx/array-and-writeonlyarray-c-cx).

## Passing IVector
```cpp
// Windows::Foundation::Collections::IVector across the ABI.
//#include <algorithm>
//#include <collection.h>
Windows::Foundation::Collections::IVector<int>^ SortVector(Windows::Foundation::Collections::IVector<int>^ vec)
{
    std::sort(begin(vec), end(vec));
    return vec;
}
```

```javascript
var nativeObject = new CppComponent.CollectionExample();
// Call the method to sort an integer array
var inVector = [14, 12, 45, 89, 23];
var outVector = nativeObject.sortVector(inVector);
var result = "Sorted vector to array:";
for (var i = 0; i < outVector.length; i++)
{
    outVector[i];
    result += outVector[i].toString() + ",";
}
document.getElementById('P6').innerHTML = result;
```

The .NET languages see IVector&lt;T&gt; as IList&lt;T&gt;.

```csharp
private void SortListItems()
{
    IList<int> myList = new List<int>();
    myList.Add(5);
    myList.Add(9);
    myList.Add(17);
    myList.Add(2);

    var nativeObject = new CppComponent.CollectionExample();
    IList<int> mySortedList = nativeObject.SortVector(myList);

    foreach (var item in mySortedList)
    {
        ResultText.Text += " " + item.ToString();
    }
}
```

## Passing IMap
```cpp
// #include <map>
//#include <collection.h>
Windows::Foundation::Collections::IMap<int, Platform::String^> ^GetMap(void)
{    
    Windows::Foundation::Collections::IMap<int, Platform::String^> ^ret =
        ref new Platform::Collections::Map<int, Platform::String^>;
    ret->Insert(1, "One ");
    ret->Insert(2, "Two ");
    ret->Insert(3, "Three ");
    ret->Insert(4, "Four ");
    ret->Insert(5, "Five ");
    return ret;
}
```

```javascript
// Call the method to get the map
var outputMap = nativeObject.getMap();
var mStr = "Map result:" + outputMap.lookup(1) + outputMap.lookup(2)
    + outputMap.lookup(3) + outputMap.lookup(4) + outputMap.lookup(5);
document.getElementById('P7').innerHTML = mStr;
```

The .NET languages see IMap and IDictionary&lt;K, V&gt;.

```csharp
private void GetDictionary()
{
    var nativeObject = new CppComponent.CollectionExample();
    IDictionary<int, string> d = nativeObject.GetMap();
    ResultText.Text += d[2].ToString();
}
```

## Properties
A public ref class in C++/CX component extensions exposes public data members as properties, by using the property keyword. The concept is identical to .NET properties. A trivial property resembles a data member because its functionality is implicit. A non-trivial property has explicit get and set accessors and a named private variable that's the "backing store" for the value. In this example, the private member variable \_propertyAValue is the backing store for PropertyA. A property can fire an event when its value changes, and a client app can register to receive that event.

```cpp
//Properties
public delegate void PropertyChangedHandler(Platform::Object^ sender, int arg);
public ref class PropertyExample  sealed
{
public:
    PropertyExample(){}

    // Event that is fired when PropertyA changes
    event PropertyChangedHandler^ PropertyChangedEvent;

    // Property that has custom setter/getter
    property int PropertyA
    {
        int get() { return m_propertyAValue; }
        void set(int propertyAValue)
        {
            if (propertyAValue != m_propertyAValue)
            {
                m_propertyAValue = propertyAValue;
                // Fire event. (See event example below.)
                PropertyChangedEvent(this, propertyAValue);
            }
        }
    }

    // Trivial get/set property that has a compiler-generated backing store.
    property Platform::String^ PropertyB;

private:
    // Backing store for propertyA.
    int m_propertyAValue;
};
```

```javascript
var nativeObject = new CppComponent.PropertyExample();
var propValue = nativeObject.propertyA;
document.getElementById('P8').innerHTML = propValue;

//Set the string property
nativeObject.propertyB = "What is the meaning of the universe?";
document.getElementById('P9').innerHTML += nativeObject.propertyB;
```

The .NET languages access properties on a native C++/CX object just as they would on a .NET object.

```csharp
private void GetAProperty()
{
    // Get the value of the integer property
    // Instantiate the C++ object
    var obj = new CppComponent.PropertyExample();

    // Get an integer property
    var propValue = obj.PropertyA;
    ResultText.Text += propValue.ToString();

    // Set a string property
    obj.PropertyB = " What is the meaning of the universe?";
    ResultText.Text += obj.PropertyB;

}
```

## Delegates and events
A delegate is a Windows Runtime type that represents a function object. You can use delegates in connection with events, callbacks, and asynchronous method calls to specify an action to be performed later. Like a function object, the delegate provides type-safety by enabling the compiler to verify the return type and parameter types of the function. The declaration of a delegate resembles a function signature, the implementation resembles a class definition, and the invocation resembles a function invocation.

## Adding an event listener
You can use the event keyword to declare a public member of a specified delegate type. Client code subscribes to the event by using the standard mechanisms that are provided in the particular language.

```cpp
public:
    event SomeHandler^ someEvent;
```

This example uses the same C++ code as for the previous properties section.

```javascript
function Button_Click() {
    var nativeObj = new CppComponent.PropertyExample();
    // Define an event handler method
    var singlecasthandler = function (ev) {
        document.getElementById('P10').innerHTML = "The button was clicked and the value is " + ev;
    };

    // Subscribe to the event
    nativeObj.onpropertychangedevent = singlecasthandler;

    // Set the value of the property and fire the event
    var propValue = 21;
    nativeObj.propertyA = 2 * propValue;

}
```

In the .NET languages, subscribing to an event in a C++ component is the same as subscribing to an event in a .NET class:

```csharp
//Subscribe to event and call method that causes it to be fired.
private void TestMethod()
{
    var objWithEvent = new CppComponent.PropertyExample();
    objWithEvent.PropertyChangedEvent += objWithEvent_PropertyChangedEvent;

    objWithEvent.PropertyA = 42;
}

//Event handler method
private void objWithEvent_PropertyChangedEvent(object __param0, int __param1)
{
    ResultText.Text = "the event was fired and the result is " +
         __param1.ToString();
}
```

## Adding multiple event listeners for one event
JavaScript has an addEventListener method that enables multiple handlers to subscribe to a single event.

```cpp
public delegate void SomeHandler(Platform::String^ str);

public ref class LangSample sealed
{
public:
    event SomeHandler^ someEvent;
    property Platform::String^ PropertyA;

    // Method that fires an event
    void FireEvent(Platform::String^ str)
    {
        someEvent(Platform::String::Concat(str, PropertyA->ToString()));
    }
    //...
};
```

```javascript
// Add two event handlers
var multicast1 = function (ev) {
    document.getElementById('P11').innerHTML = "Handler 1: " + ev.target;
};
var multicast2 = function (ev) {
    document.getElementById('P12').innerHTML = "Handler 2: " + ev.target;
};

var nativeObject = new CppComponent.LangSample();
//Subscribe to the same event
nativeObject.addEventListener("someevent", multicast1);
nativeObject.addEventListener("someevent", multicast2);

nativeObject.propertyA = "42";

// This method should fire an event
nativeObject.fireEvent("The answer is ");
```

In C#, any number of event handlers can subscribe to the event by using the += operator as shown in the previous example.

## Enums
A Windows Runtime enum in C++/CX is declared by using public class enum; it resembles a scoped enum in standard C++.

```cpp
public enum class Direction {North, South, East, West};

public ref class EnumExampleClass sealed
{
public:
    property Direction CurrentDirection
    {
        Direction  get(){return m_direction; }
    }

private:
    Direction m_direction;
};
```

Enum values are passed between C++/CX and JavaScript as integers. You can optionally declare a JavaScript object that contains the same named values as the C++/CX enum and use it as follows.

```javascript
var Direction = { 0: "North", 1: "South", 2: "East", 3: "West" };
//. . .

var nativeObject = new CppComponent.EnumExampleClass();
var curDirection = nativeObject.currentDirection;
document.getElementById('P13').innerHTML =
Direction[curDirection];
```

Both C# and Visual Basic have language support for enums. These languages see a C++ public enum class just as they would see a .NET enum.

## Asynchronous methods
To consume asynchronous methods that are exposed by other Windows Runtime objects, use the [task Class (Concurrency Runtime)](/cpp/parallel/concrt/reference/task-class). For more information, see and [Task Parallelism (Concurrency Runtime)](/cpp/parallel/concrt/task-parallelism-concurrency-runtime).

To implement asynchronous methods in C++/CX, use the [create\_async](/cpp/parallel/concrt/reference/concurrency-namespace-functions?view=vs-2017) function that's defined in ppltasks.h. For more information, see [Creating Asynchronous Operations in C++/CX for UWP apps](/cpp/parallel/concrt/creating-asynchronous-operations-in-cpp-for-windows-store-apps). For an example, see [Walkthrough of creating a C++/CX Windows Runtime component, and calling it from JavaScript or C#](walkthrough-creating-a-basic-windows-runtime-component-in-cpp-and-calling-it-from-javascript-or-csharp.md). The .NET languages consume C++/CX asynchronous methods just as they would any asynchronous method that's defined in .NET.

## Exceptions
You can throw any exception type that's defined by the Windows Runtime. You cannot derive custom types from any Windows Runtime exception type. However, you can throw COMException and provide a custom HRESULT that can be accessed by the code that catches the exception. There's no way to specify a custom Message in a COMException.

## Debugging tips
When you debug a JavaScript solution that has a component DLL, you can set the debugger to enable either stepping through script, or stepping through native code in the component, but not both at the same time. To change the setting, select the JavaScript project node in Solution Explorer and then choose Properties, Debugging, Debugger Type.

Be sure to select appropriate capabilities in the package designer. For example, if you are attempting to open an image file in the user's Pictures library by using the Windows Runtime APIs, be sure to select the Pictures Library check box in the Capabilities pane of the manifest designer.

If your JavaScript code doesn't seem to be recognizing the public properties or methods in the component, make sure that in JavaScript you are using camel casing. For example, the LogCalc C++/CX method must be referenced as logCalc in JavaScript.

If you remove a C++/CX Windows Runtime component project from a solution, you must also manually remove the project reference from the JavaScript project. Failure to do so prevents subsequent debug or build operations. If necessary, you can then add an assembly reference to the DLL.

## Related topics
* [Walkthrough of creating a C++/CX Windows Runtime component, and calling it from JavaScript or C#](walkthrough-creating-a-basic-windows-runtime-component-in-cpp-and-calling-it-from-javascript-or-csharp.md)