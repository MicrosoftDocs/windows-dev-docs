---
title: Walkthrough of creating a C++/CX Windows Runtime component, and calling it from JavaScript or C#
description: This walkthrough shows how to create a basic Windows Runtime component DLL that's callable from JavaScript, C#, or Visual Basic.
ms.assetid: 764CD9C6-3565-4DFF-88D7-D92185C7E452
ms.date: 05/14/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Walkthrough of creating a C++/CX Windows Runtime component, and calling it from JavaScript or C#

> [!NOTE]
> This topic exists to help you maintain your C++/CX application. But we recommend that you use [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) for new applications. C++/WinRT is an entirely standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows API. To learn how to create a Windows Runtime component using C++/WinRT, see [Windows Runtime components with C++/WinRT](./create-a-windows-runtime-component-in-cppwinrt.md).

This walkthrough shows how to create a basic Windows Runtime component DLL that's callable from JavaScript, C#, or Visual Basic. Before you begin this walkthrough, make sure that you understand concepts such as the Abstract Binary Interface (ABI), ref classes, and the Visual C++ Component Extensions that make working with ref classes easier. For more information, see [Windows Runtime components with C++/CX](creating-windows-runtime-components-in-cpp.md) and [Visual C++ Language Reference (C++/CX)](/cpp/cppcx/visual-c-language-reference-c-cx).

## Creating the C++ component DLL
In this example, we create the component project first, but you could create the JavaScript project first. The order doesn’t matter.

Notice that the main class of the component contains examples of property and method definitions, and an event declaration. These are provided just to show you how it's done. They are not required, and in this example, we'll replace all of the generated code with our own code.

### **To create the C++ component project**
1. On the Visual Studio menu bar, choose **File, New, Project**.

2. In the **New Project** dialog box, in the left pane, expand **Visual C++** and then select the node for Universal Windows apps.

3. In the center pane, select **Windows Runtime Component** and then name the project WinRT\_CPP.

4. Choose the **OK** button.

## **To add an activatable class to the component**
An activatable class is one that client code can create by using a **new** expression (**New** in Visual Basic, or **ref new** in C++). In your component, you declare it as **public ref class sealed**. In fact, the Class1.h and .cpp files already have a ref class. You can change the name, but in this example we’ll use the default name—Class1. You can define additional ref classes or regular classes in your component if they are required. For more information about ref classes, see [Type System (C++/CX)](/cpp/cppcx/type-system-c-cx).

Add these \#include directives to Class1.h:

```cpp
#include <collection.h>
#include <ppl.h>
#include <amp.h>
#include <amp_math.h>
```

collection.h is the header file for C++ concrete classes such as the Platform::Collections::Vector class and the Platform::Collections::Map class, which implement language-neutral interfaces that are defined by the Windows Runtime. The amp headers are used to run computations on the GPU. They have no Windows Runtime equivalents, and that’s fine because they are private. In general, for performance reasons you should use ISO C++ code and standard libraries internally within the component; it’s just the Windows Runtime interface that must be expressed in Windows Runtime types.

## To add a delegate at namespace scope
A delegate is a construct that defines the parameters and return type for methods. An event is an instance of a particular delegate type, and any event handler method that subscribes to the event must have the signature that's specified in the delegate. The following code defines a delegate type that takes an int and returns void. Next the code declares a public event of this type; this enables client code to provide methods that are invoked when the event is fired.

Add the following delegate declaration at namespace scope in Class1.h, just before the Class1 declaration.

```cpp
public delegate void PrimeFoundHandler(int result);
```

If the code isn’t lining up correctly when you paste it into Visual Studio, just press Ctrl+K+D to fix the indentation for the entire file.

## To add the public members
The class exposes three public methods and one public event. The first method is synchronous because it always executes very fast. Because the other two methods might take some time, they are asynchronous so that they don’t block the UI thread. These methods return IAsyncOperationWithProgress and IAsyncActionWithProgress. The former defines an async method that returns a result, and the latter defines an async method that returns void. These interfaces also enable client code to receive updates on the progress of the operation.

```cpp
public:

        // Synchronous method.
        Windows::Foundation::Collections::IVector<double>^  ComputeResult(double input);

        // Asynchronous methods
        Windows::Foundation::IAsyncOperationWithProgress<Windows::Foundation::Collections::IVector<int>^, double>^
            GetPrimesOrdered(int first, int last);
        Windows::Foundation::IAsyncActionWithProgress<double>^ GetPrimesUnordered(int first, int last);

        // Event whose type is a delegate "class"
        event PrimeFoundHandler^ primeFoundEvent;

```
## To add the private members
The class contains three private members: two helper methods for the numeric computations and a CoreDispatcher object that’s used to marshal the event invocations from worker threads back to the UI thread.

```cpp
private:
        bool is_prime(int n);
        Windows::UI::Core::CoreDispatcher^ m_dispatcher;
```

### To add the header and namespace directives
1. In Class1.cpp, add these #include directives:

```cpp
#include <ppltasks.h>
#include <concurrent_vector.h>
```

2. Now add these using statements to pull in the required namespaces:

```cpp
using namespace concurrency;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
```

## To add the implementation for ComputeResult
In Class1.cpp, add the following method implementation. This method executes synchronously on the calling thread, but it is very fast because it uses C++ AMP to parallelize the computation on the GPU. For more information, see C++ AMP Overview. The results are appended to a Platform::Collections::Vector<T> concrete type, which is implicitly converted to a Windows::Foundation::Collections::IVector<T> when it is returned.

```cpp
//Public API
IVector<double>^ Class1::ComputeResult(double input)
{
    // Implement your function in ISO C++ or
    // call into your C++ lib or DLL here. This example uses AMP.
    float numbers[] = { 1.0, 10.0, 60.0, 100.0, 600.0, 10000.0 };
    array_view<float, 1> logs(6, numbers);

    // See http://msdn.microsoft.com/library/hh305254.aspx
    parallel_for_each(
        logs.extent,
        [=] (index<1> idx) restrict(amp)
    {
        logs[idx] = concurrency::fast_math::log10(logs[idx]);
    }
    );

    // Return a Windows Runtime-compatible type across the ABI
    auto res = ref new Vector<double>();
    int len = safe_cast<int>(logs.extent.size());
    for(int i = 0; i < len; i++)
    {      
        res->Append(logs[i]);
    }

    // res is implicitly cast to IVector<double>
    return res;
}
```
## To add the implementation for GetPrimesOrdered and its helper method
In Class1.cpp, add the implementations for GetPrimesOrdered and the is_prime helper method. GetPrimesOrdered uses a concurrent_vector class and a parallel_for function loop to divide up the work and use the maximum resources of the computer on which the program is running to produce results. After the results are computed, stored, and sorted, they are added to a Platform::Collections::Vector<T> and returned as Windows::Foundation::Collections::IVector<T> to client code.

Notice the code for the progress reporter, which enables the client to hook up a progress bar or other UI to show the user how much longer the operation is going to take. Progress reporting has a cost. An event must be fired on the component side and handled on the UI thread, and the progress value must be stored on each iteration. One way to minimize the cost is by limiting the frequency at which a progress event is fired. If the cost is still prohibitive, or if you can't estimate the length of the operation, then consider using a progress ring, which shows that an operation is in progress but doesn't show time remaining until completion.

```cpp
// Determines whether the input value is prime.
bool Class1::is_prime(int n)
{
    if (n < 2)
        return false;
    for (int i = 2; i < n; ++i)
    {
        if ((n % i) == 0)
            return false;
    }
    return true;
}

// This method computes all primes, orders them, then returns the ordered results.
IAsyncOperationWithProgress<IVector<int>^, double>^ Class1::GetPrimesOrdered(int first, int last)
{
    return create_async([this, first, last]
    (progress_reporter<double> reporter) -> IVector<int>^ {
        // Ensure that the input values are in range.
        if (first < 0 || last < 0) {
            throw ref new InvalidArgumentException();
        }
        // Perform the computation in parallel.
        concurrent_vector<int> primes;
        long operation = 0;
        long range = last - first + 1;
        double lastPercent = 0.0;

        parallel_for(first, last + 1, [this, &primes, &operation,
            range, &lastPercent, reporter](int n) {

                // Increment and store the number of times the parallel
                // loop has been called on all threads combined. There
                // is a performance cost to maintaining a count, and
                // passing the delegate back to the UI thread, but it's
                // necessary if we want to display a determinate progress
                // bar that goes from 0 to 100%. We can avoid the cost by
                // setting the ProgressBar IsDeterminate property to false
                // or by using a ProgressRing.
                if(InterlockedIncrement(&operation) % 100 == 0)
                {
                    reporter.report(100.0 * operation / range);
                }

                // If the value is prime, add it to the local vector.
                if (is_prime(n)) {
                    primes.push_back(n);
                }
        });

        // Sort the results.
        std::sort(begin(primes), end(primes), std::less<int>());		
        reporter.report(100.0);

        // Copy the results to a Vector object, which is
        // implicitly converted to the IVector return type. IVector
        // makes collections of data available to other
        // Windows Runtime components.
        return ref new Vector<int>(primes.begin(), primes.end());
    });
}
```

## To add the implementation for GetPrimesUnordered
The last step to create the C++ component is to add the implementation for the GetPrimesUnordered in Class1.cpp. This method returns each result as it is found, without waiting until all results are found. Each result is returned in the event handler and displayed on the UI in real time. Again, notice that a progress reporter is used. This method also uses the is_prime helper method.

```cpp
// This method returns no value. Instead, it fires an event each time a
// prime is found, and passes the prime through the event.
// It also passes progress info.
IAsyncActionWithProgress<double>^ Class1::GetPrimesUnordered(int first, int last)
{

    auto window = Windows::UI::Core::CoreWindow::GetForCurrentThread();
    m_dispatcher = window->Dispatcher;


    return create_async([this, first, last](progress_reporter<double> reporter) {

        // Ensure that the input values are in range.
        if (first < 0 || last < 0) {
            throw ref new InvalidArgumentException();
        }

        // In this particular example, we don't actually use this to store
        // results since we pass results one at a time directly back to
        // UI as they are found. However, we have to provide this variable
        // as a parameter to parallel_for.
        concurrent_vector<int> primes;
        long operation = 0;
        long range = last - first + 1;
        double lastPercent = 0.0;

        // Perform the computation in parallel.
        parallel_for(first, last + 1,
            [this, &primes, &operation, range, &lastPercent, reporter](int n)
        {
            // Store the number of times the parallel loop has been called  
            // on all threads combined. See comment in previous method.
            if(InterlockedIncrement(&operation) % 100 == 0)
            {
                reporter.report(100.0 * operation / range);
            }

            // If the value is prime, pass it immediately to the UI thread.
            if (is_prime(n))
            {                
                // Since this code is probably running on a worker
                // thread, and we are passing the data back to the
                // UI thread, we have to use a CoreDispatcher object.
                m_dispatcher->RunAsync( CoreDispatcherPriority::Normal,
                    ref new DispatchedHandler([this, n, operation, range]()
                {
                    this->primeFoundEvent(n);

                }, Platform::CallbackContext::Any));

            }
        });
        reporter.report(100.0);
    });
}
```

## Creating a JavaScript client app (Visual Studio 2017)

If you want to create a C# client, then you can skip this section.

> [!NOTE]
> Universal Windows Platform (UWP) projects are not supported in Visual Studio 2019. See [JavaScript and TypeScript in Visual Studio 2019](/visualstudio/javascript/javascript-in-vs-2019?view=vs-2019#projects). To follow along with this section, we recommend that you use Visual Studio 2017. See [JavaScript in Visual Studio 2017](/visualstudio/javascript/javascript-in-vs-2017).

### To create a JavaScript project
1. In Solution Explorer (in Visual Studio 2017; see **Note** above), open the shortcut menu for the Solution node and choose **Add, New Project**.

2. Expand JavaScript (it might be nested under **Other Languages**) and choose **Blank App (Universal Windows)**.

3. Accept the default name&mdash;App1&mdash;by choosing the **OK** button.

4. Open the shortcut menu for the App1 project node and choose **Set as Startup Project**.

5. Add a project reference to WinRT_CPP:

6. Open the shortcut menu for the References node and choose **Add Reference**.

7. In the left pane of the References Manager dialog box, select **Projects** and then select **Solution**.

8. In the center pane, select WinRT_CPP and then choose the **OK** button

## To add the HTML that invokes the JavaScript event handlers
Paste this HTML into the <body> node of the default.html page:

```HTML
<div id="LogButtonDiv">
     <button id="logButton">Logarithms using AMP</button>
 </div>
 <div id="LogResultDiv">
     <p id="logResult"></p>
 </div>
 <div id="OrderedPrimeButtonDiv">
     <button id="orderedPrimeButton">Primes using parallel_for with sort</button>
 </div>
 <div id="OrderedPrimeProgress">
     <progress id="OrderedPrimesProgressBar" value="0" max="100"></progress>
 </div>
 <div id="OrderedPrimeResultDiv">
     <p id="orderedPrimes">
         Primes found (ordered):
     </p>
 </div>
 <div id="UnorderedPrimeButtonDiv">
     <button id="ButtonUnordered">Primes returned as they are produced.</button>
 </div>
 <div id="UnorderedPrimeDiv">
     <progress id="UnorderedPrimesProgressBar" value="0" max="100"></progress>
 </div>
 <div id="UnorderedPrime">
     <p id="unorderedPrimes">
         Primes found (unordered):
     </p>
 </div>
 <div id="ClearDiv">
     <button id="Button_Clear">Clear</button>
 </div>
```

## To add styles
In default.css, remove the body style and then add these styles:

```css
#LogButtonDiv {
border: orange solid 1px;
-ms-grid-row: 1; /* default is 1 */
-ms-grid-column: 1; /* default is 1 */
}
#LogResultDiv {
background: black;
border: red solid 1px;
-ms-grid-row: 1;
-ms-grid-column: 2;
}
#UnorderedPrimeButtonDiv, #OrderedPrimeButtonDiv {
border: orange solid 1px;
-ms-grid-row: 2;   
-ms-grid-column:1;
}
#UnorderedPrimeProgress, #OrderedPrimeProgress {
border: red solid 1px;
-ms-grid-column-span: 2;
height: 40px;
}
#UnorderedPrimeResult, #OrderedPrimeResult {
border: red solid 1px;
font-size:smaller;
-ms-grid-row: 2;
-ms-grid-column: 3;
-ms-overflow-style:scrollbar;
}
```

## To add the JavaScript event handlers that call into the component DLL
Add the following functions at the end of the default.js file. These functions are called when the buttons on the main page are chosen. Notice how JavaScript activates the C++ class, and then calls its methods and uses the return values to populate the HTML labels.

```JavaScript
var nativeObject = new WinRT_CPP.Class1();

function LogButton_Click() {

    var val = nativeObject.computeResult(0);
    var result = "";

    for (i = 0; i < val.length; i++) {
        result += val[i] + "<br/>";
    }

    document.getElementById('logResult').innerHTML = result;
}

function ButtonOrdered_Click() {
    document.getElementById('orderedPrimes').innerHTML = "Primes found (ordered): ";

    nativeObject.getPrimesOrdered(2, 10000).then(
        function (v) {
            for (var i = 0; i < v.length; i++)
                document.getElementById('orderedPrimes').innerHTML += v[i] + " ";
        },
        function (error) {
            document.getElementById('orderedPrimes').innerHTML += " " + error.description;
        },
        function (p) {
            var progressBar = document.getElementById("OrderedPrimesProgressBar");
            progressBar.value = p;
        });
}

function ButtonUnordered_Click() {
    document.getElementById('unorderedPrimes').innerHTML = "Primes found (unordered): ";
    nativeObject.onprimefoundevent = handler_unordered;

    nativeObject.getPrimesUnordered(2, 10000).then(
        function () { },
        function (error) {
            document.getElementById("unorderedPrimes").innerHTML += " " + error.description;
        },
        function (p) {
            var progressBar = document.getElementById("UnorderedPrimesProgressBar");
            progressBar.value = p;
        });
}

var handler_unordered = function (n) {
    document.getElementById('unorderedPrimes').innerHTML += n.target.toString() + " ";
};

function ButtonClear_Click() {

    document.getElementById('logResult').innerHTML = "";
    document.getElementById("unorderedPrimes").innerHTML = "";
    document.getElementById('orderedPrimes').innerHTML = "";
    document.getElementById("UnorderedPrimesProgressBar").value = 0;
    document.getElementById("OrderedPrimesProgressBar").value = 0;
}
```

Add code to add the event listeners by replacing the existing call to WinJS.UI.processAll in app.onactivated in default.js with the following code that implements event registration in a then block. For a detailed explanation of this, see [Create a "Hello, World" app (JS)](../get-started/create-a-hello-world-app-js-uwp.md).

```JavaScript
args.setPromise(WinJS.UI.processAll().then( function completed() {
    var logButton = document.getElementById("logButton");
    logButton.addEventListener("click", LogButton_Click, false);
    var orderedPrimeButton = document.getElementById("orderedPrimeButton");
    orderedPrimeButton.addEventListener("click", ButtonOrdered_Click, false);
    var buttonUnordered = document.getElementById("ButtonUnordered");
    buttonUnordered.addEventListener("click", ButtonUnordered_Click, false);
    var buttonClear = document.getElementById("Button_Clear");
    buttonClear.addEventListener("click", ButtonClear_Click, false);
}));
```

Press F5 to run the app.

## Creating a C# client app

### To create a C# project
1. In Solution Explorer, open the shortcut menu for the Solution node and then choose **Add, New Project**.

2. Expand Visual C# (it might be nested under **Other Languages**), select **Windows** and then **Universal** in the left pane, and then select **Blank App** in the middle pane.

3. Name this app CS_Client and then choose the **OK** button.

4. Open the shortcut menu for the CS_Client project node and choose **Set as Startup Project**.

5. Add a project reference to WinRT_CPP:

   - Open the shortcut menu for the **References** node and choose **Add Reference**.

   - In the left pane of the **References Manager** dialog box, select **Projects** and then select **Solution**.

   - In the center pane, select WinRT_CPP and then choose the **OK** button.

## To add the XAML that defines the user interface
Copy the following code into the Grid element in MainPage.xaml.

```xaml
<ScrollViewer>
            <StackPanel Width="1400">

                <Button x:Name="Button1" Width="340" Height="50"  Margin="0,20,20,20" Content="Synchronous Logarithm Calculation" FontSize="16" Click="Button1_Click_1"/>
                <TextBlock x:Name="Result1" Height="100" FontSize="14"></TextBlock>
            <Button x:Name="PrimesOrderedButton" Content="Prime Numbers Ordered" FontSize="16" Width="340" Height="50" Margin="0,20,20,20" Click="PrimesOrderedButton_Click_1"></Button>
            <ProgressBar x:Name="PrimesOrderedProgress" IsIndeterminate="false" Height="40"></ProgressBar>
                <TextBlock x:Name="PrimesOrderedResult" MinHeight="100" FontSize="10" TextWrapping="Wrap"></TextBlock>
            <Button x:Name="PrimesUnOrderedButton" Width="340" Height="50" Margin="0,20,20,20" Click="PrimesUnOrderedButton_Click_1" Content="Prime Numbers Unordered" FontSize="16"></Button>
            <ProgressBar x:Name="PrimesUnOrderedProgress" IsIndeterminate="false" Height="40" ></ProgressBar>
            <TextBlock x:Name="PrimesUnOrderedResult" MinHeight="100" FontSize="10" TextWrapping="Wrap"></TextBlock>

            <Button x:Name="Clear_Button" Content="Clear" HorizontalAlignment="Left" Margin="0,20,20,20" VerticalAlignment="Top" Width="341" Click="Clear_Button_Click" FontSize="16"/>
        </StackPanel>
</ScrollViewer>
```

## To add the event handlers for the buttons
In Solution Explorer, open MainPage.xaml.cs. (The file might be nested under MainPage.xaml.) Add a using directive for System.Text, and then add the event handler for the Logarithm calculation in the MainPage class.

```csharp
private void Button1_Click_1(object sender, RoutedEventArgs e)
{
    // Create the object
    var nativeObject = new WinRT_CPP.Class1();

    // Call the synchronous method. val is an IList that
    // contains the results.
    var val = nativeObject.ComputeResult(0);
    StringBuilder result = new StringBuilder();
    foreach (var v in val)
    {
        result.Append(v).Append(System.Environment.NewLine);
    }
    this.Result1.Text = result.ToString();
}
```

Add the event handler for the ordered result:

```csharp
async private void PrimesOrderedButton_Click_1(object sender, RoutedEventArgs e)
{
    var nativeObject = new WinRT_CPP.Class1();

    StringBuilder sb = new StringBuilder();
    sb.Append("Primes found (ordered): ");

    PrimesOrderedResult.Text = sb.ToString();

    // Call the asynchronous method
    var asyncOp = nativeObject.GetPrimesOrdered(2, 100000);

    // Before awaiting, provide a lambda or named method
    // to handle the Progress event that is fired at regular
    // intervals by the asyncOp object. This handler updates
    // the progress bar in the UI.
    asyncOp.Progress = (asyncInfo, progress) =>
        {
            PrimesOrderedProgress.Value = progress;
        };

    // Wait for the operation to complete
    var asyncResult = await asyncOp;

    // Convert the results to strings
    foreach (var result in asyncResult)
    {
        sb.Append(result).Append(" ");
    }

    // Display the results
    PrimesOrderedResult.Text = sb.ToString();
}
```

Add the event handler for the unordered result, and for the button that clears the results so that you can run the code again.

```csharp
private void PrimesUnOrderedButton_Click_1(object sender, RoutedEventArgs e)
{
    var nativeObject = new WinRT_CPP.Class1();

    StringBuilder sb = new StringBuilder();
    sb.Append("Primes found (unordered): ");
    PrimesUnOrderedResult.Text = sb.ToString();

    // primeFoundEvent is a user-defined event in nativeObject
    // It passes the results back to this thread as they are produced
    // and the event handler that we define here immediately displays them.
    nativeObject.primeFoundEvent += (n) =>
    {
        sb.Append(n.ToString()).Append(" ");
        PrimesUnOrderedResult.Text = sb.ToString();
    };

    // Call the async method.
    var asyncResult = nativeObject.GetPrimesUnordered(2, 100000);

    // Provide a handler for the Progress event that the asyncResult
    // object fires at regular intervals. This handler updates the progress bar.
    asyncResult.Progress += (asyncInfo, progress) =>
        {
            PrimesUnOrderedProgress.Value = progress;
        };
}

private void Clear_Button_Click(object sender, RoutedEventArgs e)
{
    PrimesOrderedProgress.Value = 0;
    PrimesUnOrderedProgress.Value = 0;
    PrimesUnOrderedResult.Text = "";
    PrimesOrderedResult.Text = "";
    Result1.Text = "";
}
```

## Running the app
Select either the C# project or JavaScript project as the startup project by opening the shortcut menu for the project node in Solution Explorer and choosing **Set As Startup Project**. Then press F5 to run with debugging, or Ctrl+F5 to run without debugging.

## Inspecting your component in Object Browser (optional)
In Object Browser, you can inspect all Windows Runtime types that are defined in .winmd files. This includes the types in the Platform namespace and the default namespace. However, because the types in the Platform::Collections namespace are defined in the header file collections.h, not in a winmd file, they don’t appear in Object Browser.

### **To inspect a component**
1. On the menu bar, choose **View, Object Browser** (Ctrl+Alt+J).

2. In the left pane of the Object Browser, expand the WinRT\_CPP node to show the types and methods that are defined on your component.

## Debugging tips
For a better debugging experience, download the debugging symbols from the public Microsoft symbol servers:

### **To download debugging symbols**
1. On the menu bar, choose **Tools, Options**.

2. In the **Options** dialog box, expand **Debugging** and select **Symbols**.

3. Select **Microsoft Symbol Servers** and the choose the **OK** button.

It might take some time to download the symbols the first time. For faster performance the next time you press F5, specify a local directory in which to cache the symbols.

When you debug a JavaScript solution that has a component DLL, you can set the debugger to enable either stepping through script or stepping through native code in the component, but not both at the same time. To change the setting, open the shortcut menu for the JavaScript project node in Solution Explorer and choose **Properties, Debugging, Debugger Type**.

Be sure to select appropriate capabilities in the package designer. You can open the package designer by opening the Package.appxmanifest file. For example, if you are attempting to programmatically access files in the Pictures folder, be sure to select the **Pictures Library** check box in the **Capabilities** pane of the package designer.

If your JavaScript code doesn't recognize the public properties or methods in the component, make sure that in JavaScript you are using camel casing. For example, the `ComputeResult` C++ method must be referenced as `computeResult` in JavaScript.

If you remove a C++ Windows Runtime component project from a solution, you must also manually remove the project reference from the JavaScript project. Failure to do so prevents subsequent debug or build operations. If necessary, you can then add an assembly reference to the DLL.

## Related topics
* [Windows Runtime components with C++/CX](creating-windows-runtime-components-in-cpp.md)