---
title: Launch an app for results
description: Learn how to launch an app from another app and exchange data between the two. This is called launching an app for results.
ms.assetid: AFC53D75-B3DD-4FF6-9FC0-9335242EE327
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Launch an app for results




**Important APIs**

-   [**LaunchUriForResultsAsync**](/uwp/api/windows.system.launcher.launchuriforresultsasync)
-   [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet)

Learn how to launch an app from another app and exchange data between the two. This is called *launching an app for results*. The example here shows you how to use [**LaunchUriForResultsAsync**](/uwp/api/windows.system.launcher.launchuriforresultsasync) to launch an app for results.

New app-to-app communication APIs in Windows 10 make it possible for Windows apps (and Windows Web apps) to launch an app and exchange data and files. This enables you to build mash-up solutions from multiple apps. Using these new APIs, complex tasks that would have required the user to use multiple apps can now be handled seamlessly. For example, your app could launch a social networking app to choose a contact, or launch a checkout app to complete a payment process.

The app that you'll launch for results will be referred to as the launched app. The app that launches the app will be referred to as the calling app. For this example you will write both the calling app and the launched app.

## Step 1: Register the protocol to be handled in the app that you'll launch for results


In the Package.appxmanifest file of the launched app, add a protocol extension to the **&lt;Application&gt;** section. The example here uses a fictional protocol named **test-app2app**.

The **ReturnResults** attribute in the protocol extension accepts one of these values:

-   **optional**—The app can be launched for results by using the [**LaunchUriForResultsAsync**](/uwp/api/windows.system.launcher.launchuriforresultsasync) method, or not for results by using [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync). When you use **optional**, the launched app must determine whether it was launched for results. It can do that by checking the [**OnActivated**](/uwp/api/windows.ui.xaml.application.onactivated) event argument. If the argument's [**IActivatedEventArgs.Kind**](/uwp/api/windows.applicationmodel.activation.iactivatedeventargs.kind) property returns [**ActivationKind.ProtocolForResults**](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind), or if the type of the event argument is [**ProtocolActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs), the app was launched via **LaunchUriForResultsAsync**.
-   **always**—The app can be launched only for results; that is, it can respond only to [**LaunchUriForResultsAsync**](/uwp/api/windows.system.launcher.launchuriforresultsasync).
-   **none**—The app cannot be launched for results; it can respond only to [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync).

In this protocol-extension example, the app can be launched only for results. This simplifies the logic inside the **OnActivated** method, discussed below, because we have to handle only the "launched for results" case and not the other ways that the app could be activated.

```xml
<Applications>
   <Application ...>

     <Extensions>
       <uap:Extension Category="windows.protocol">
         <uap:Protocol Name="test-app2app" ReturnResults="always">
           <uap:DisplayName>Test app-2-app</uap:DisplayName>
         </uap:Protocol>
       </uap:Extension>
     </Extensions>

   </Application>
</Applications>
```

## Step 2: Override Application.OnActivated in the app that you'll launch for results


If this method does not already exist in the launched app, create it within the `App` class defined in App.xaml.cs.

In an app that lets you pick your friends in a social network, this function could be where you open the people-picker page. In this next example, a page named **LaunchedForResultsPage** is displayed when the app is activated for results. Ensure that the **using** statement is included at the top of the file.

```cs
using Windows.ApplicationModel.Activation;
...
protected override void OnActivated(IActivatedEventArgs args)
{
    // Window management
    Frame rootFrame = Window.Current.Content as Frame;
    if (rootFrame == null)
    {
        rootFrame = new Frame();
        Window.Current.Content = rootFrame;
    }

    // Code specific to launch for results
    var protocolForResultsArgs = (ProtocolForResultsActivatedEventArgs)args;
    // Open the page that we created to handle activation for results.
    rootFrame.Navigate(typeof(LaunchedForResultsPage), protocolForResultsArgs);

    // Ensure the current window is active.
    Window.Current.Activate();
}
```

Because the protocol extension in the Package.appxmanifest file specifies **ReturnResults** as **always**, the code just shown can cast `args` directly to [**ProtocolForResultsActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.ProtocolForResultsActivatedEventArgs) with confidence that only **ProtocolForResultsActivatedEventArgs** will be sent to **OnActivated** for this app. If your app can be activated in ways other than launching for results, you can check whether [**IActivatedEventArgs.Kind**](/uwp/api/windows.applicationmodel.activation.iactivatedeventargs.kind) property returns [**ActivationKind.ProtocolForResults**](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind) to tell whether the app was launched for results.

## Step 3: Add a ProtocolForResultsOperation field to the app you launch for results


```cs
private Windows.System.ProtocolForResultsOperation _operation = null;
```

You'll use the [**ProtocolForResultsOperation**](/uwp/api/windows.applicationmodel.activation.protocolforresultsactivatedeventargs.protocolforresultsoperation) field to signal when the launched app is ready to return the result to the calling app. In this example, the field is added to the **LaunchedForResultsPage** class because you'll complete the launch-for-results operation from that page and will need access to it.

## Step 4: Override OnNavigatedTo() in the app you launch for results


Override the [**OnNavigatedTo**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedto) method on the page that you'll display when your app is launched for results. If this method does not already exist, create it within the class for the page defined in &lt;pagename&gt;.xaml.cs. Ensure that the following **using** statement is included at the top of the file:

```cs
using Windows.ApplicationModel.Activation
```

The [**NavigationEventArgs**](/uwp/api/Windows.UI.Xaml.Navigation.NavigationEventArgs) object in the [**OnNavigatedTo**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedto) method contains the data passed from the calling app. The data may not exceed 100KB and is stored in a [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet) object.

In this example code, the launched app expects the data sent from the calling app to be in a [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet) under a key named **TestData**, because that's what the example's calling app is coded to send.

```cs
using Windows.ApplicationModel.Activation;
...
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    var protocolForResultsArgs = e.Parameter as ProtocolForResultsActivatedEventArgs;
    // Set the ProtocolForResultsOperation field.
    _operation = protocolForResultsArgs.ProtocolForResultsOperation;

    if (protocolForResultsArgs.Data.ContainsKey("TestData"))
    {
        string dataFromCaller = protocolForResultsArgs.Data["TestData"] as string;
    }
}
...
private Windows.System.ProtocolForResultsOperation _operation = null;
```

## Step 5: Write code to return data to the calling app


In the launched app, use [**ProtocolForResultsOperation**](/uwp/api/windows.applicationmodel.activation.protocolforresultsactivatedeventargs.protocolforresultsoperation) to return data to the calling app. In this example code, a [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet) object is created that contains the value to return to the calling app. The **ProtocolForResultsOperation** field is then used to send the value to the calling app.

```cs
    ValueSet result = new ValueSet();
    result["ReturnedData"] = "The returned result";
    _operation.ReportCompleted(result);
```

## Step 6: Write code to launch the app for results and get the returned data


Launch the app from within an async method in your calling app as shown in this example code. Note the **using** statements, which are necessary for the code to compile:

```cs
using System.Threading.Tasks;
using Windows.System;
...

async Task<string> LaunchAppForResults()
{
    var testAppUri = new Uri("test-app2app:"); // The protocol handled by the launched app
    var options = new LauncherOptions();
    options.TargetApplicationPackageFamilyName = "67d987e1-e842-4229-9f7c-98cf13b5da45_yd7nk54bq29ra";

    var inputData = new ValueSet();
    inputData["TestData"] = "Test data";

    string theResult = "";
    LaunchUriResult result = await Windows.System.Launcher.LaunchUriForResultsAsync(testAppUri, options, inputData);
    if (result.Status == LaunchUriStatus.Success &&
        result.Result != null &&
        result.Result.ContainsKey("ReturnedData"))
    {
        ValueSet theValues = result.Result;
        theResult = theValues["ReturnedData"] as string;
    }
    return theResult;
}
```

In this example, a [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet) containing the key **TestData** is passed to the launched app. The launched app creates a **ValueSet** with a key named **ReturnedData** that contains the result returned to the caller.

You must build and deploy the app that you'll launch for results before running your calling app. Otherwise, [**LaunchUriResult.Status**](/uwp/api/Windows.System.LaunchUriStatus) will report **LaunchUriStatus.AppUnavailable**.

You'll need the family name of the launched app when you set the [**TargetApplicationPackageFamilyName**](/uwp/api/windows.system.launcheroptions.targetapplicationpackagefamilyname). One way to get the family name is to make the following call from within the launched app:

```cs
string familyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
```

## Remarks


The example in this how-to provides a "hello world" introduction to launching an app for results. The key things to note are that the new [**LaunchUriForResultsAsync**](/uwp/api/windows.system.launcher.launchuriforresultsasync) API lets you asynchronously launch an app and communicate via the [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet) class. Passing data via a **ValueSet** is limited to 100KB. If you need to pass larger amounts of data, you can share files by using the [**SharedStorageAccessManager**](/uwp/api/Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager) class to create file tokens that you can pass between apps. For example, given a **ValueSet** named `inputData`, you could store the token to a file that you want to share with the launched app:

```cs
inputData["ImageFileToken"] = SharedStorageAccessManager.AddFile(myFile);
```

Then pass it to the launched app via **LaunchUriForResultsAsync**.

## Related topics


* [**LaunchUri**](/uwp/api/windows.system.launcher.launchuriasync)
* [**LaunchUriForResultsAsync**](/uwp/api/windows.system.launcher.launchuriforresultsasync)
* [**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet)

 

 