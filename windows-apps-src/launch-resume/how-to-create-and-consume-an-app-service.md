---
author: TylerMSFT
title: Create and consume an app service
description: Learn how to write a Universal Windows Platform (UWP) app that can provide services to other UWP apps, and how to consume those services.
ms.assetid: 6E48B8B6-D3BF-4AE2-85FB-D463C448C9D3
keywords: app to app communication, interprocess communication, IPC, Background messaging, background communication, app to app
ms.author: twhitney
ms.date: 09/18/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Create and consume an app service

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

Learn how to write a Universal Windows Platform (UWP) app that can provide a service to other UWP apps, and how to consume that service.

Starting in Windows 10, version 1607, you can create app services that run in the same process as the host app. This article focuses on creating app services that run in a separate background process. See [Convert an app service to run in the same process as its host app](convert-app-service-in-process.md) for more details about running an app service in the same process as the provider.

For more app service samples, see [Universal Windows Platform (UWP) app samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AppServices).

## Create a new app service provider project

In this how-to, we'll create everything in one solution for simplicity.

-   In Microsoft Visual Studio, create a new UWP app project and name it **AppServiceProvider**. (In the **New Project** dialog box, select **Templates &gt; Other Languages &gt; Visual C# &gt; Windows &gt; Windows Universal &gt; Blank app (Windows Universal)**). This will be the app that makes the app service available to other UWP apps.
-   When asked to select a **Target Version** for the project, select at least **10.0.14393**. If you want to use the new `SupportsMultipleInstances` attribute, you must be using Visual Studio 2017 and target **10.0.15063** (**Windows 10 Creator's Update**) or higher.

<span id="appxmanifest"/>
## Add an app service extension to package.appxmanifest

In the AppServiceProvider project's Package.appxmanifest file, add the following AppService extension inside the `&lt;Application&gt;` element. This example advertises the `com.Microsoft.Inventory` service and is what identifies this app as an app service provider. The actual service will be implemented as a background task. The app service project exposes the service to other apps. We recommend using a reverse domain name style for the service name.

Note that the `xmlns:uap4` namespace prefix and the `uap4:SupportsMultipleInstances` attribute are only valid if you are targeting Windows SDK version 10.0.15063 or higher. You can safely remove them if you are targeting older SDK versions.

``` xml
<Package
    ...
    xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
    xmlns:uap4="http://schemas.microsoft.com/appx/manifest/uap/windows10/4"
    ...
    <Applications>
        <Application Id="AppServicesProvider.App"
          Executable="$targetnametoken$.exe"
          EntryPoint="AppServicesProvider.App">
          ...
          <Extensions>
            <uap:Extension Category="windows.appService" EntryPoint="MyAppService.Inventory">
              <uap3:AppService Name="com.microsoft.inventory" uap4:SupportsMultipleInstances="true"/>
            </uap:Extension>
          </Extensions>
          ...
        </Application>
    </Applications>
```

The **Category** attribute identifies this application as an app service provider.

The **EntryPoint** attribute identifies the namespace qualified class that implements the service, which we'll implement next.

The **SupportsMultipleInstances** attribute indicates that each time the app service is called that it should run in a new process. This is not required but is available to you if you need that functionality and are targeting the `10.0.15063` SDK (**Windows 10 Creator's Update**) or higher. It also should be prefaced by the `uap4` namespace.

## Create the app service

1.  An app service can be implemented as a background task. This enables a foreground application to invoke an app service in another application. To create an app service as a background task, add a new Windows Runtime Component project to the solution (**File &gt; Add &gt; New Project**) named MyAppService. (In the **Add New Project** dialog box, choose **Installed &gt; Other Languages &gt; Visual C# &gt; Windows &gt; Windows Universal &gt; Windows Runtime Component (Windows Universal)**
2.  In the **AppServiceProvider** project, add a project-to-project reference to the new **MyAppService** project (In the Solution Explorer, right-click on the **AppServiceProvider** project > **Add** > **Reference** > **Projects** > **Solution**, select **MyAppService** > **OK**). This step is critical because if you do not add the reference, the app service won't connect at runtime.
3.  In the MyappService project, add the following **using** statements to the top of Class1.cs:
    ```cs
    using Windows.ApplicationModel.AppService;
    using Windows.ApplicationModel.Background;
    using Windows.Foundation.Collections;
    ```

4.  Replace the stub code for **Class1** with a new background task class named **Inventory**:

    ```cs
    public sealed class Inventory : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;
        private String[] inventoryItems = new string[] { "Robot vacuum", "Chair" };
        private double[] inventoryPrices = new double[] { 129.99, 88.99 };

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            this.backgroundTaskDeferral = taskInstance.GetDeferral(); // Get a deferral so that the service isn't terminated.
            taskInstance.Canceled += OnTaskCanceled; // Associate a cancellation handler with the background task.

            // Retrieve the app service connection and set up a listener for incoming app service requests.
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // This function is called when the app service receives a request
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.backgroundTaskDeferral != null)
            {
                // Complete the service deferral.
                this.backgroundTaskDeferral.Complete();
            }
        }
    }
    ```

    This class is where the app service will do its work.

    **Run()** is called when the background task is created. Because background tasks are terminated after **Run** completes, the code takes out a deferral so that the background task will stay up to serve requests. An app service that is implemented as a background task will stay alive for about 30 seconds after it receives a call unless it is called again within that time window or a deferral is taken out. If the app service is implemented in the same process as the caller, the lifetime of the app service is tied to the lifetime of the caller.

    The lifetime of the app service depends on the caller:

    1. If the caller is in foreground, the app service lifetime is the same as the caller.
    2. If the caller is in background, the app service gets 30 seconds to run. Taking out a deferral provides an additional one time 5 seconds.

    **OnTaskCanceled()** is called when the task is canceled. The task is cancelled when the client app disposes the [**AppServiceConnection**](https://msdn.microsoft.com/library/windows/apps/dn921704), the client app is suspended, the OS is shut down or sleeps, or the OS runs out of resources to run the task.

## Write the code for the app service

**OnRequestReceived()** is where the code for the app service goes. Replace the stub **OnRequestReceived()** in MyAppService's Class1.cs with the code from this example. This code gets an index for an inventory item and passes it, along with a command string, to the service to retrieve the name and the price of the specified inventory item. For your own projects, add error handling code.

```cs
private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
{
    // Get a deferral because we use an awaitable API below to respond to the message
    // and we don't want this call to get cancelled while we are waiting.
    var messageDeferral = args.GetDeferral();

    ValueSet message = args.Request.Message;
    ValueSet returnData = new ValueSet();

    string command = message["Command"] as string;
    int? inventoryIndex = message["ID"] as int?;

    if ( inventoryIndex.HasValue &&
         inventoryIndex.Value >= 0 &&
         inventoryIndex.Value < inventoryItems.GetLength(0))
    {
        switch (command)
        {
            case "Price":
            {
                returnData.Add("Result", inventoryPrices[inventoryIndex.Value]);
                returnData.Add("Status", "OK");
                break;
            }

            case "Item":
            {
                returnData.Add("Result", inventoryItems[inventoryIndex.Value]);
                returnData.Add("Status", "OK");
                break;
            }

            default:
            {
                returnData.Add("Status", "Fail: unknown command");
                break;
            }
        }
    }
    else
    {
        returnData.Add("Status", "Fail: Index out of range");
    }

    try
    {
        await args.Request.SendResponseAsync(returnData); // Return the data to the caller.
    }
    catch (Exception e)
    {
        // your exception handling code here
    }
    finally
    {
        // Complete the deferral so that the platform knows that we're done responding to the app service call.
        // Note for error handling: this must be called even if SendResponseAsync() throws an exception.
        messageDeferral.Complete();
    }
}
```

Note that **OnRequestReceived()** is **async** because we make an awaitable method call to [**SendResponseAsync**](https://msdn.microsoft.com/library/windows/apps/dn921722) in this example.

A deferral is taken so that the service can use **async** methods in the OnRequestReceived handler. It ensures that the call to **OnRequestReceived** does not complete until it is done processing the message.  [**SendResponseAsync**](https://msdn.microsoft.com/library/windows/apps/dn921722) sends the result to the caller. **SendResponseAsync** does not signal the completion of the call. It is the completion of the deferral that signals to [**SendMessageAsync**](https://msdn.microsoft.com/library/windows/apps/dn921712) that **OnRequestReceived** has completed. The call to **SendResponseAsync()** is wrapped in a try/finally block because you must complete the deferral even if **SendResponseAsync()** throws an exception.

App services use a [**ValueSet**](https://msdn.microsoft.com/library/windows/apps/dn636131) to exchange information. The size of the data you may pass is only limited by system resources. There are no predefined keys for you to use in your **ValueSet**. You must determine which key values you will use to define the protocol for your app service. The caller must be written with that protocol in mind. In this example, we have chosen a key named `Command` that has a value that indicates whether we want the app service to provide the name of the inventory item or its price. The index of the inventory name is stored under the `ID` key. The return value is stored under the `Result` key.

An [**AppServiceClosedStatus**](https://msdn.microsoft.com/library/windows/apps/dn921703) enum is returned to the caller to indicate whether the call to the app service succeeded or failed. An example of how the call to the app service could fail is if the OS aborts the service endpoint because its resources have been exceeded. You can return additional error information via the [**ValueSet**](https://msdn.microsoft.com/library/windows/apps/dn636131). In this example, we use a key named `Status` to return more detailed error information to the caller.

The call to [**SendResponseAsync**](https://msdn.microsoft.com/library/windows/apps/dn921722) returns the [**ValueSet**](https://msdn.microsoft.com/library/windows/apps/dn636131) to the caller.

## Deploy the service app and get the package family name

The app service provider app must be deployed before you can call it from a client. You will also need the package family name of the app service app in order to call it.

One way to get the package family name of the app service application is to call [**Windows.ApplicationModel.Package.Current.Id.FamilyName**](https://msdn.microsoft.com/library/windows/apps/br224670) from within the **AppServiceProvider** project (for example, from `public App()` in App.xaml.cs) and note the result. To run AppServiceProvider in Microsoft Visual Studio, set it as the startup project in the Solution Explorer window and run the project.

Another way to get the package family name is to deploy the solution (**Build &gt; Deploy solution**) and note the full package name in the output window (**View &gt; Output**). You must remove the platform information from the string in the output window to derive the package name. For example, if the full package name reported in the output window was `Microsoft.SDKSamples.AppServicesProvider.CPP_1.0.0.0_x86__8wekyb3d8bbwe`, you would extract `1.0.0.0\_x86\_\_" leaving "Microsoft.SDKSamples.AppServicesProvider.CPP_8wekyb3d8bbwe` as the package family name.

## Write a client to call the app service

1.  Add a new blank Windows Universal app project to the solution with **File &gt; Add &gt; New Project**. In the **Add New Project** dialog box, choose **Installed &gt; Other languages &gt; Visual C# &gt; Windows &gt; Windows Universal &gt; Blank App (Windows Universal)** and name it **ClientApp**.
2.  In the ClientApp project, add the following **using** statement to the top of MainPage.xaml.cs:
    ```cs
    >using Windows.ApplicationModel.AppService;
    ```
3.  Add a text box and a button to MainPage.xaml.
4.  Add a button click handler for the button and add the keyword **async** to the button handler's signature.
5.  Replace the stub of your button click handler with the following code. Be sure to include the `inventoryService` field declaration.

   ```cs
   private AppServiceConnection inventoryService;
   private async void button_Click(object sender, RoutedEventArgs e)
   {
       // Add the connection.
       if (this.inventoryService == null)
       {
           this.inventoryService = new AppServiceConnection();

           // Here, we use the app service name defined in the app service provider's Package.appxmanifest file in the <Extension> section.
           this.inventoryService.AppServiceName = "com.microsoft.inventory";

           // Use Windows.ApplicationModel.Package.Current.Id.FamilyName within the app service provider to get this value.
           this.inventoryService.PackageFamilyName = "replace with the package family name";

           var status = await this.inventoryService.OpenAsync();
           if (status != AppServiceConnectionStatus.Success)
           {
               textBox.Text= "Failed to connect";
               return;
           }
       }

       // Call the service.
       int idx = int.Parse(textBox.Text);
       var message = new ValueSet();
       message.Add("Command", "Item");
       message.Add("ID", idx);
       AppServiceResponse response = await this.inventoryService.SendMessageAsync(message);
       string result = "";

       if (response.Status == AppServiceResponseStatus.Success)
       {
           // Get the data  that the service sent  to us.
           if (response.Message["Status"] as string == "OK")
           {
               result = response.Message["Result"] as string;
           }
       }

       message.Clear();
       message.Add("Command", "Price");
       message.Add("ID", idx);
       response = await this.inventoryService.SendMessageAsync(message);

       if (response.Status == AppServiceResponseStatus.Success)
       {
           // Get the data that the service sent to us.
           if (response.Message["Status"] as string == "OK")
           {
               result += " : Price = " + response.Message["Result"] as string;
           }
       }

       textBox.Text = result;
   }
   ```
Replace the package family name in the line `this.inventoryService.PackageFamilyName = "replace with the package family name";` with the package family name of the **AppServiceProvider** project that you obtained above in [Deploy the service app and get the package family name](#deploy-the-service-app-and-get-the-package-family-name).

The code first establishes a connection with the app service. The connection will remain open until you dispose `this.inventoryService`. The app service name must match the **AppService Name** attribute that you added to the AppServiceProvider project's Package.appxmanifest file. In this example, it is `<uap:AppService Name="com.microsoft.inventory"/>`.

A [**ValueSet**](https://msdn.microsoft.com/library/windows/apps/dn636131) named **message** is created to specify the command that we want to send to the app service. The example app service expects a command to indicate which of two actions to take. We get the index from the textbox in the client app, and then call the service with the `Item` command to get the description of the item. Then, we make the call with the `Price` command to get the item's price. The button text is set to the result.

Because [**AppServiceResponseStatus**](https://msdn.microsoft.com/library/windows/apps/dn921724) only indicates whether the operating system was able to connect the call to the app service, we check the `Status` key in the [**ValueSet**](https://msdn.microsoft.com/library/windows/apps/dn636131) we receive from the app service to ensure that it was able to fulfill the request.

6.  In Visual Studio, set the ClientApp project to be the startup project in the Solution Explorer window and run the solution. Enter the number 1 into the text box and click the button. You should get "Chair : Price = 88.99" back from the service.

    ![sample app displaying chair price=88.99](images/appserviceclientapp.png)

If the app service call fails, check the following in the ClientApp:

1.  Verify that the package family name assigned to the inventory service connection matches the package family name of the AppServiceProvider app. See: **button\_Click()**`this.inventoryService.PackageFamilyName = "...";`).
2.  In **button\_Click()**, verify that the app service name that is assigned to the inventory service connection matches the app service name in the AppServiceProvider's Package.appxmanifest file. See: `this.inventoryService.AppServiceName = "com.microsoft.inventory";`.
3.  Ensure that the AppServiceProvider app has been deployed (In the Solution Explorer, right-click the solution and choose **Deploy**).

## Debug the app service

1.  Ensure that the solution is deployed before debugging because the app service provider app must be deployed before the service can be called. (In Visual Studio, **Build &gt; Deploy Solution**).
2.  In the Solution Explorer, right-click the **AppServiceProvider** project and choose **Properties**. From the **Debug** tab, change the **Start action** to **Do not launch, but debug my code when it starts**. (Note, if you were using C++ to implement your app service provider, from the **Debugging** tab you would change **Launch Application** to **No**).
3.  In the MyAppService project, in the Class1.cs file, set a breakpoint in `OnRequestReceived()`.
4.  Set the AppServiceProvider project to be the startup project and press F5.
5.  Start ClientApp from the Start menu (not from Visual Studio).
6.  Enter the number 1 into the text box and press the button. The debugger will stop in the app service call on the breakpoint in your app service.

## Debug the client

1.  Follow the instructions in the preceding step to debug the client that calls the app service.
2.  Launch ClientApp from the Start menu.
3.  Attach the debugger to the ClientApp.exe process (not the ApplicationFrameHost.exe process). (In Visual Studio, choose **Debug &gt; Attach to Process...**.)
4.  In the ClientApp project, set a breakpoint in **button\_Click()**.
5.  The breakpoints in both the client and the app service will now be hit when you enter the number 1 into the text box of the ClientApp and click the button.

## General app service troubleshooting ##

If you encounter a **AppUnavailable** status after trying to connect to an app service, check the following:

- Ensure that the app service provider project and app service project are deployed. Both need to be deployed before running the client because otherwise the client won't have anything to connect to. You can deploy from Visual Studio by using **Build** > **Deploy Solution**.
- In the solution explorer, ensure that your app service provider project has a project-to-project reference to the project that implements the app service.
- Verify that the `<Extensions>` entry, and its child elements, have been added to the Package.appxmanifest file belonging to the app service provider project as specified above in [Add an app service extension to package.appxmanifest](#appxmanifest).
- Ensure that the `AppServiceConnection.AppServiceName` string in your client that calls the app service provider matches the `<uap3:AppService Name="..." />` specified in the app service provider project's Package.appxmanifest file.
- Ensure that the `AppServiceConnection.PackageFamilyName` matches the package family name of the app service provider component as specified above in [Add an app service extension to package.appxmanifest](#appxmanifest)
- For out-of-proc app services such as the one in this example, validate that the `EntryPoint` specified in the `<uap:Extension ...>` element of your app service provider project's Package.appxmanifest file matches the namespace and class name of the public class that implements `IBackgroundTask` in your app service project.

### Troubleshoot debugging

If the debugger does not stop at breakpoints in your app service provider or app service projects, check the following:

- Ensure that the app service provider project and app service project are deployed. Both need to be deployed before running the client. You can deploy them from Visual Studio by using **Build** > **Deploy Solution**.
- Ensure that the project you want to debug is set as the startup project and that the debugging properties for that project are set to not run the project when F5 is pressed. Right-click the project, then click **Properties**, and then **Debug** (or **Debugging** in C++). In C#, change the **Start action** to **Do not launch, but debug my code when it starts**. In C++, set **Launch Application** to **No**.

## Remarks

This example provides an introduction to creating an app service that runs as a background task and calling it from another app. The key things to note are the creation of a background task to host the app service, the addition of the windows.appservice extension to the app service provider app's Package.appxmanifest file, obtaining the package family name of the app service provider app so that we can connect to it from the client app, adding a project-to-project reference from the app service provider project to the app service project, and using [**Windows.ApplicationModel.AppService.AppServiceConnection**](https://msdn.microsoft.com/library/windows/apps/dn921704) to call the service.

## Full code for MyAppService

```cs
using System;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace MyAppService
{
    public sealed class Inventory : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;
        private String[] inventoryItems = new string[] { "Robot vacuum", "Chair" };
        private double[] inventoryPrices = new double[] { 129.99, 88.99 };

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            this.backgroundTaskDeferral = taskInstance.GetDeferral(); // Get a deferral so that the service isn't terminated.
            taskInstance.Canceled += OnTaskCanceled; // Associate a cancellation handler with the background task.

            // Retrieve the app service connection and set up a listener for incoming app service requests.
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // Get a deferral because we use an awaitable API below to respond to the message
            // and we don't want this call to get cancelled while we are waiting.
            var messageDeferral = args.GetDeferral();

            ValueSet message = args.Request.Message;
            ValueSet returnData = new ValueSet();

            string command = message["Command"] as string;
            int? inventoryIndex = message["ID"] as int?;

            if (inventoryIndex.HasValue &&
                 inventoryIndex.Value >= 0 &&
                 inventoryIndex.Value < inventoryItems.GetLength(0))
            {
                switch (command)
                {
                    case "Price":
                        {
                            returnData.Add("Result", inventoryPrices[inventoryIndex.Value]);
                            returnData.Add("Status", "OK");
                            break;
                        }

                    case "Item":
                        {
                            returnData.Add("Result", inventoryItems[inventoryIndex.Value]);
                            returnData.Add("Status", "OK");
                            break;
                        }

                    default:
                        {
                            returnData.Add("Status", "Fail: unknown command");
                            break;
                        }
                }
            }
            else
            {
                returnData.Add("Status", "Fail: Index out of range");
            }

            await args.Request.SendResponseAsync(returnData); // Return the data to the caller.
            // Complete the deferral so that the platform knows that we're done responding to the app service call.
            // Note for error handling: this must be called even if SendResponseAsync() throws an exception.
            messageDeferral.Complete();
        }


        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.backgroundTaskDeferral != null)
            {
                // Complete the service deferral.
                this.backgroundTaskDeferral.Complete();
            }
        }
    }
}
```

## Related topics

* [Convert an app service to run in the same process as its host app](convert-app-service-in-process.md)
* [Support your app with background tasks](support-your-app-with-background-tasks.md)
* [Universal Windows Platform (UWP) app samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AppServices)
