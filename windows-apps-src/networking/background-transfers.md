---
author: stevewhims
description: Use the background transfer API to copy files reliably over the network.
title: Background transfers
ms.assetid: 1207B089-BC16-4BF0-BBD4-FD99950C764B
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Background transfers

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]


**Important APIs**

-   [**Windows.Networking.BackgroundTransfer**](https://msdn.microsoft.com/library/windows/apps/br207242)
-   [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998)
-   [**Windows.Networking.Sockets**](https://msdn.microsoft.com/library/windows/apps/br226960)

Use the background transfer API to copy files reliably over the network. The background transfer API provides advanced upload and download features that run in the background during app suspension and persist beyond app termination. The API monitors network status and automatically suspends and resumes transfers when connectivity is lost, and transfers are also Data Sense-aware and Battery Sense-aware, meaning that download activity adjusts based on your current connectivity and device battery status. The API is ideal for uploading and downloading large files using HTTP(S). FTP is also supported, but only for downloads.

Background Transfer runs separately from the calling app and is primarily designed for long-term transfer operations for resources like video, music, and large images. For these scenarios, using Background Transfer is essential because downloads continue to progress even when the app is suspended.

If you are downloading small resources that are likely to complete quickly, you should use [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) APIs instead of Background Transfer.

## Using Windows.Networking.BackgroundTransfer


### How does the Background Transfer feature work?

When an app uses Background Transfer to initiate a transfer, the request is configured and initialized using [**BackgroundDownloader**](https://msdn.microsoft.com/library/windows/apps/br207126) or [**BackgroundUploader**](https://msdn.microsoft.com/library/windows/apps/br207140) class objects. Each transfer operation is handled individually by the system and separate from the calling app. Progress information is available if you want to give status to the user in your app's UI, and your app can pause, resume, cancel, or even read from the data while the transfer is occurring. The way transfers are handled by the system promotes smart power usage and prevents problems that can arise when a connected app encounters events such as app suspension, termination, or sudden network status changes.

In addition, Background Transfer uses System Event Broker events. As such, the number of downloads is limited by the number of events available on the system. By default this is 500 events, but those events are shared across all processes. Therefore, a single application should not create more than 100 background transfers at a time.

When an application starts a background transfer, the application must call [**AttachAsync**](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.BackgroundTransfer.DownloadOperation#methods_) on all existing [**DownloadOperation**](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.BackgroundTransfer.DownloadOperation) objects. Not doing this may cause a leak of those events, and thus render the Background Transfer feature useless.

### Performing authenticated file requests with Background Transfer

Background Transfer provides methods that support basic server and proxy credentials, cookies, and the use of custom HTTP headers (via [**SetRequestHeader**](https://msdn.microsoft.com/library/windows/apps/br207146)) for each transfer operation.

### How does this feature adapt to network status changes or unexpected shutdowns?

The Background Transfer feature maintains a consistent experience for each transfer operation when network status changes occur, by intelligently leveraging connectivity and carrier data-plan status information provided by the [Connectivity](https://msdn.microsoft.com/library/windows/apps/hh452990) feature. To define behavior for different network scenarios, an app sets a cost policy for each operation using values defined by [**BackgroundTransferCostPolicy**](https://msdn.microsoft.com/library/windows/apps/br207138).

For example, the cost policy defined for an operation can indicate that the operation should be paused automatically when the device is using a metered network. The transfer is then automatically resumed (or restarted) when a connection to an "unrestricted" network has been established. For more information on how networks are defined by cost, see [**NetworkCostType**](https://msdn.microsoft.com/library/windows/apps/br207292).

While the Background Transfer feature has its own mechanisms for handling network status changes, there are other general connectivity considerations for network-connected apps. Read [Leveraging available network connection information](https://msdn.microsoft.com/library/windows/apps/hh452983) for additional info.

> **Note**  For apps running on mobile devices, there are features that allow the user to monitor and restrict the amount of data that is transferred based on the type of connection, roaming status, and the user's data plan. Because of this, background transfers may be paused on the phone even when the [**BackgroundTransferCostPolicy**](https://msdn.microsoft.com/library/windows/apps/br207138) indicates that the transfer should proceed.

The following table indicates when background transfers are allowed on the phone for each [**BackgroundTransferCostPolicy**](https://msdn.microsoft.com/library/windows/apps/br207138) value, given the current state of the phone. You can use the [**ConnectionCost**](https://msdn.microsoft.com/library/windows/apps/br207244) class to determine the phone's current state.

| Device State                                                                                                                      | UnrestrictedOnly | Default | Always |
|-----------------------------------------------------------------------------------------------------------------------------------|------------------|---------|--------|
| Connected to WiFi                                                                                                                 | Allow            | Allow   | Allow  |
| Metered Connection, not roaming, under data limit, on track to stay under limit                                                   | Deny             | Allow   | Allow  |
| Metered Connection, not roaming, under data limit, on track to exceed limit                                                       | Deny             | Deny    | Allow  |
| Metered Connection, roaming, under data limit                                                                                     | Deny             | Deny    | Allow  |
| Metered Connection, over data limit. This state only occurs when the user enables "Restrict background data in the Data Sense UI. | Deny             | Deny    | Deny   |

 

## Uploading files


When using Background Transfer an upload exists as an [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224) that exposes a number of control methods that are used to restart or cancel the operation. App events (e.g. suspension or termination) and connectivity changes are handled automatically by the system per **UploadOperation**; uploads will continue during app suspension periods or pause and persist beyond app termination. Additionally, setting the [**CostPolicy**](https://msdn.microsoft.com/library/windows/apps/hh701018) property will indicate whether or not your app will start uploads while a metered network is being used for Internet connectivity.

The following examples will walk you through the creation and initialization of a basic upload and how to enumerate and reintroduce operations persisted from a previous app session.

### Uploading a single file

The creation of an upload begins with [**BackgroundUploader**](https://msdn.microsoft.com/library/windows/apps/br207140). This class is used to provide the methods that enable your app to configure the upload before creating the resultant [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224). The following example shows how to do this with the required [**Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) and [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) objects.

**Identify the file and destination for the upload**

Before we can begin with the creation of an [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224), we first need to identify the URI of the location to upload to, and the file that will be uploaded. In the following example, the *uriString* value is populated using a string from UI input, and the *file* value using the [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) object returned by a [**PickSingleFileAsync**](https://msdn.microsoft.com/library/windows/apps/jj635275) operation.

[!code-js[uploadFile](./code/backgroundtransfer/upload_quickstart/js/main.js#Snippetupload_quickstart_B "Identify the file and destination for the upload")]

**Create and initialize the upload operation**

In the previous step the *uriString* and *file* values are passed to an instance of our next example, UploadOp, where they are used to configure and start the new upload operation. First, *uriString* is parsed to create the required [**Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) object.

Next, the properties of the provided [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) (*file*) are used by [**BackgroundUploader**](https://msdn.microsoft.com/library/windows/apps/br207140) to populate the request header and set the *SourceFile* property with the **StorageFile** object. The [**SetRequestHeader**](https://msdn.microsoft.com/library/windows/apps/br207146) method is then called to insert the file name, provided as a string, and the [**StorageFile.Name**](https://msdn.microsoft.com/library/windows/apps/br227220) property.

Finally, [**BackgroundUploader**](https://msdn.microsoft.com/library/windows/apps/br207140) creates the [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224) (*upload*).

[!code-js[uploadFile](./code/backgroundtransfer/upload_quickstart/js/main.js#Snippetupload_quickstart_A "Create and initialize the upload operation")]

Note the asynchronous method calls defined using JavaScript promises. Looking at a line from the last example:

```javascript
promise = upload.startAsync().then(complete, error, progress);
```

The async method call is followed by a then statement which indicates methods, defined by the app, that are called when a result from the async method call is returned. For more information on this programming pattern, see [Asynchronous programming in JavaScript using promises](http://msdn.microsoft.com/library/windows/apps/hh464930.aspx).

### Uploading multiple files

**Identify the files and destination for the upload**

In a scenario involving multiple files transferred with a single [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224), the process begins as it usually does by first providing the required destination URI and local file information. Similar to the example in the previous section, the URI is provided as a string by the end-user and [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/br207847) can be used to provide the ability to indicate files through the user interface as well. However, in this scenario the app should instead call the [**PickMultipleFilesAsync**](https://msdn.microsoft.com/library/windows/apps/br207851) method to enable the selection of multiple files through the UI.

```javascript
function uploadFiles() {
       var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
       filePicker.fileTypeFilter.replaceAll(["*"]);

       filePicker.pickMultipleFilesAsync().then(function (files) {
          if (files === 0) {
             printLog("No file selected");
                return;
          }

          var upload = new UploadOperation();
          var uriString = document.getElementById("serverAddressField").value;
          upload.startMultipart(uriString, files);

          // Persist the upload operation in the global array.
          uploadOperations.push(upload);
       });
    }
```

**Create objects for the provided parameters**

The next two examples use code contained in a single example method, **startMultipart**, which was called at the end of the last step. For the purpose of instruction the code in the method that creates an array of [**BackgroundTransferContentPart**](https://msdn.microsoft.com/library/windows/apps/hh923029) objects has been split from the code that creates the resultant [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224).

First, the URI string provided by the user is initialized as a [**Uri**](https://msdn.microsoft.com/library/windows/apps/br225998). Next, the array of [**IStorageFile**](https://msdn.microsoft.com/library/windows/apps/br227102) objects (**files**) passed to this method is iterated through, each object is used to create a new [**BackgroundTransferContentPart**](https://msdn.microsoft.com/library/windows/apps/hh923029) object which is then placed in the **contentParts** array.

```javascript
    upload.startMultipart = function (uriString, files) {
        try {
            var uri = new Windows.Foundation.Uri(uriString);
            var uploader = new Windows.Networking.BackgroundTransfer.BackgroundUploader();

            var contentParts = [];
            files.forEach(function (file, index) {
                var part = new Windows.Networking.BackgroundTransfer.BackgroundTransferContentPart("File" + index, file.name);
                part.setFile(file);
                contentParts.push(part);
            });
```

**Create and initialize the multi-part upload operation**

With our contentParts array populated with all of the [**BackgroundTransferContentPart**](https://msdn.microsoft.com/library/windows/apps/hh923029) objects representing each [**IStorageFile**](https://msdn.microsoft.com/library/windows/apps/br227102) for upload, we are ready to call [**CreateUploadAsync**](https://msdn.microsoft.com/library/windows/apps/hh923973) using the [**Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) to indicate where the request will be sent.

```javascript
        // Create a new upload operation.
            uploader.createUploadAsync(uri, contentParts).then(function (uploadOperation) {

               // Start the upload and persist the promise to be able to cancel the upload.
               upload = uploadOperation;
               promise = uploadOperation.startAsync().then(complete, error, progress);
            });

         } catch (err) {
             displayError(err);
         }
     };
```

### Restarting interrupted upload operations

On completion or cancellation of an [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224), any associated system resources are released. However, if your app is terminated before either of these things can occur, any active operations are paused and the resources associated with each remain occupied. If these operations are not enumerated and re-introduced to the next app session, they will not be completed and will continue to occupy device resources.

1.  Before defining the function that enumerates persisted operations, we need to create an array that will contain the [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224) objects that it will return:

    [!code-js[uploadFile](./code/backgroundtransfer/upload_quickstart/js/main.js#Snippetupload_quickstart_C "Restart interrupted upload operation")]

1.  Next we define the function that enumerates persisted operations and stores them in our array. Note that the **load** method called to re-assign callbacks to the [**UploadOperation**](https://msdn.microsoft.com/library/windows/apps/br207224), should it persist through app termination, is in the UploadOp class we define later in this section.

    [!code-js[uploadFile](./code/backgroundtransfer/upload_quickstart/js/main.js#Snippetupload_quickstart_D "Enumerate persisted operations")]

## Downloading files

When using Background Transfer, each download exists as a [**DownloadOperation**](https://msdn.microsoft.com/library/windows/apps/br207154) that exposes a number of control methods used to pause, resume, restart, and cancel the operation. App events (e.g. suspension or termination) and connectivity changes are handled automatically by the system per **DownloadOperation**; downloads will continue during app suspension periods or pause and persist beyond app termination. For mobile network scenarios, setting the [**CostPolicy**](https://msdn.microsoft.com/library/windows/apps/hh701018) property will indicate whether or not your app will begin or continue downloads while a metered network is being used for Internet connectivity.

If you are downloading small resources that are likely to complete quickly, you should use [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) APIs instead of Background Transfer.

Due to per-app resource constraints, an app should not have more than 200 transfers (DownloadOperations + UploadOperations) at any given time. Exceeding that limit may leave the app’s transfer queue in an unrecoverable state.

The following examples will walk you through the creation and initialization of a basic download, and how to enumerate and reintroduce operations persisted from a previous app session.

### Configure and start a Background Transfer file download

The following example demonstrates how strings representing a URI and a file name can be used to create a [**Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) object and the [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) that will contain the requested file. In this example, the new file is automatically placed in a pre-defined location. Alternatively, [**FileSavePicker**](https://msdn.microsoft.com/library/windows/apps/br207871) can be used allow users to indicate where to save the file on the device. Note that the **load** method called to re-assign callbacks to the [**DownloadOperation**](https://msdn.microsoft.com/library/windows/apps/br207154), should it persist through app termination, is in the DownloadOp class defined later in this section.

[!code-js[uploadFile](./code/backgroundtransfer/download_quickstart/js/main.js#Snippetdownload_quickstart_A)]

Note the asynchronous method calls defined using JavaScript promises. Looking at line 17 from the previous code example:

```javascript
promise = download.startAsync().then(complete, error, progress);
```

The async method call is followed by a then statement which indicates methods, defined by the app, that are called when a result from the async method call is returned. For more information on this programming pattern, see [Asynchronous programming in JavaScript using promises](http://msdn.microsoft.com/library/windows/apps/hh464930.aspx).

### Adding additional operation control methods

The level of control can be increased by implementing additional [**DownloadOperation**](https://msdn.microsoft.com/library/windows/apps/br207154) methods. For example, adding the following code to the example above will introduce the ability to cancel the download.

[!code-js[uploadFile](./code/backgroundtransfer/download_quickstart/js/main.js#Snippetdownload_quickstart_B)]

### Enumerating persisted operations at start-up

On completion or cancellation of a [**DownloadOperation**](https://msdn.microsoft.com/library/windows/apps/br207154), any associated system resources are released. However, if your app is terminated before either of these events occur, downloads will pause and persist in the background. The following examples demonstrate how to re-introduce persisted downloads into a new app session.

1.  Before defining the function that enumerates persisted operations, we need to create an array that will contain the [**DownloadOperation**](https://msdn.microsoft.com/library/windows/apps/br207154) objects that it will return:

    [!code-js[uploadFile](./code/backgroundtransfer/download_quickstart/js/main.js#Snippetdownload_quickstart_D)]

1.  Next we define the function that enumerates persisted operations and stores them in our array. Note that the **load** method called to re-assign callbacks for a persisted [**DownloadOperation**](https://msdn.microsoft.com/library/windows/apps/br207154) is in the DownloadOp example we define later in this section.

    [!code-js[uploadFile](./code/backgroundtransfer/download_quickstart/js/main.js#Snippetdownload_quickstart_E)]

1.  You can now use the populated list to restart pending operations.

## Post-processing

A new feature in Windows 10 is the ability to run application code at the completion of a background transfer even when the app is not running. For example, your app might want to update a list of available movies after a movie has finished downloading, rather than have your app scan for new movies every time it starts. Or your app might want to handle a failed file transfer by trying again using a different server or port. Post-processing is invoked for both successful and failed transfers, so you can use it to implement custom error-handling and retry logic.

Postprocessing uses the existing background task infrastructure. You create a background task and associate it with your transfers before you start the transfers. The transfers are then executed in the background, and when they are complete, your background task is called to perform post-processing.

Post-processing uses a new class, [**BackgroundTransferCompletionGroup**](https://msdn.microsoft.com/library/windows/apps/dn804209). This class is similar to the existing [**BackgroundTransferGroup**](https://msdn.microsoft.com/library/windows/apps/dn279030) in that it allows you to group background transfers together, but **BackgroundTransferCompletionGroup** adds the ability to designate a background task to be run when the transfer is complete.

You initiate a background transfer with post-processing as follows.

1.  Create a [**BackgroundTransferCompletionGroup**](https://msdn.microsoft.com/library/windows/apps/dn804209) object. Then, create a [**BackgroundTaskBuilder**](https://msdn.microsoft.com/library/windows/apps/br224768) object. Set the **Trigger** property of the builder object to the completion group object, and the **TaskEntryPoint** property of the builder to the entry point of the background task that should execute on transfer completion. Finally, call the [**BackgroundTaskBuilder.Register**](https://msdn.microsoft.com/library/windows/apps/br224772) method to register your background task. Note that many completion groups can share one background task entry point, but you can have only one completion group per background task registration.

   ```csharp
    var completionGroup = new BackgroundTransferCompletionGroup();
    BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

    builder.Name = "MyDownloadProcessingTask";
    builder.SetTrigger(completionGroup.Trigger);
    builder.TaskEntryPoint = "Tasks.BackgroundDownloadProcessingTask";

    BackgroundTaskRegistration downloadProcessingTask = builder.Register();
    ```

2.  Next you associate background transfers with the completion group. Once all transfers are created, enable the completion group.

   ```csharp
    BackgroundDownloader downloader = new BackgroundDownloader(completionGroup);
    DownloadOperation download = downloader.CreateDownload(uri, file);
    Task<DownloadOperation> startTask = download.StartAsync().AsTask();

    // App still sees the normal completion path
    startTask.ContinueWith(ForegroundCompletionHandler);

    // Do not enable the CompletionGroup until after all downloads are created.
    downloader.CompletinGroup.Enable();
    ```

3.  The code in the background task extracts the list of operations from the trigger details, and your code can then inspect the details for each operation and perform appropriate post-processing for each operation.

   ```csharp
    public class BackgroundDownloadProcessingTask : IBackgroundTask
    {
      public async void Run(IBackgroundTaskInstance taskInstance)
      {
        var details = (BackgroundTransferCompletionGroupTriggerDetails)taskInstance.TriggerDetails;
        IReadOnlyList<DownloadOperation> downloads = details.Downloads;

        // Do post-processing on each finished operation in the list of downloads
      }
    }
    ```

The post-processing task is a regular background task. It is part of the pool of all background tasks, and it is subject to the same resource management policy as all background tasks.

Also, note that post-processing does not replace foreground completion handlers. If your app defines a foreground completion handler, and your app is running when the file transfer completes, then both your foreground completion handler and your background completion handler will be called. The order in which foreground and background tasks are called is not guaranteed. If you define both, you should ensure that the two tasks will work properly and not interfere with each other if they are running concurrently.

## Request timeouts

There are two primary connection timeout scenarios to take into consideration:

-   When establishing a new connection for a transfer, the connection request is aborted if it is not established within five minutes.

-   After a connection has been established, an HTTP request message that has not received a response within two minutes is aborted.

> **Note**  In either scenario, assuming there is Internet connectivity, Background Transfer will retry a request up to three times automatically. In the event Internet connectivity is not detected, additional requests will wait until it is.

## Debugging guidance

Stopping a debugging session in Microsoft Visual Studio is comparable to closing your app; PUT uploads are paused and POST uploads are terminated. Even while debugging, your app should enumerate and then restart or cancel any persisted uploads. For example, you can have your app cancel enumerated persisted upload operations at app startup if there is no interest in previous operations for that debug session.

While enumerating downloads/uploads on app startup during a debug session, you can have your app cancel them if there is no interest in previous operations for that debug session. Note that if there are Visual Studio project updates, like changes to the app manifest, and the app is uninstalled and re-deployed, [**GetCurrentUploadsAsync**](https://msdn.microsoft.com/library/windows/apps/hh701149) cannot enumerate operations created using the previous app deployment.

When using Background Transfer during development, you may get into a situation where the internal caches of active and completed transfer operations can get out of sync. This may result in the inability to start new transfer operations or interact with existing operations and [**BackgroundTransferGroup**](https://msdn.microsoft.com/library/windows/apps/dn279030) objects. In some cases, attempting to interact with existing operations may trigger a crash. This result can occur if the [**TransferBehavior**](https://msdn.microsoft.com/library/windows/apps/dn279033) property is set to **Parallel**. This issue occurs only in certain scenarios during development and is not applicable to end users of your app.

Four scenarios using Visual Studio can cause this issue.

-   You create a new project with the same app name as an existing project, but a different language (from C++ to C#, for example).
-   You change the target architecture (from x86 to x64, for example) in an existing project.
-   You change the culture (from neutral to en-US, for example) in an existing project.
-   You add or remove a capability in the package manifest (adding **Enterprise Authentication**, for example) in an existing project.

Regular app servicing, including manifest updates which add or remove capabilities, do not trigger this issue on end user deployments of your app.
To work around this issue, completely uninstall all versions of the app and re-deploy with the new language, architecture, culture, or capability. This can be done via the **Start** screen or using PowerShell and the **Remove-AppxPackage** cmdlet.

## Exceptions in Windows.Networking.BackgroundTransfer

An exception is thrown when an invalid string for a the Uniform Resource Identifier (URI) is passed to the constructor for the [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) object.

**.NET:  **The [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) type appears as [**System.Uri**](https://msdn.microsoft.com/library/windows/apps/xaml/system.uri.aspx) in C# and VB.

In C# and Visual Basic, this error can be avoided by using the [**System.Uri**](https://msdn.microsoft.com/library/windows/apps/xaml/system.uri.aspx) class in the .NET 4.5 and one of the [**System.Uri.TryCreate**](https://msdn.microsoft.com/library/windows/apps/xaml/system.uri.trycreate.aspx) methods to test the string received from the app user before the URI is constructed.

In C++, there is no method to try and parse a string to a URI. If an app gets input from the user for the [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998), the constructor should be in a try/catch block. If an exception is thrown, the app can notify the user and request a new hostname.

The [**Windows.Networking.backgroundTransfer**](https://msdn.microsoft.com/library/windows/apps/br207242) namespace has convenient helper methods and uses enumerations in the [**Windows.Networking.Sockets**](https://msdn.microsoft.com/library/windows/apps/br226960) namespace for handling errors. This can be useful for handling specific network exceptions differently in your app.

An error encountered on an asynchronous method in the [**Windows.Networking.backgroundTransfer**](https://msdn.microsoft.com/library/windows/apps/br207242) namespace is returned as an **HRESULT** value. The [**BackgroundTransferError.GetStatus**](https://msdn.microsoft.com/library/windows/apps/hh701093) method is used to convert a network error from a background transfer operation to a [**WebErrorStatus**](https://msdn.microsoft.com/library/windows/apps/hh747818) enumeration value. Most of the **WebErrorStatus** enumeration values correspond to an error returned by the native HTTP or FTP client operation. An app can filter on specific **WebErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

For parameter validation errors, an app can also use the **HRESULT** from the exception to learn more detailed information on the error that caused the exception. Possible **HRESULT** values are listed in the *Winerror.h* header file. For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**.

