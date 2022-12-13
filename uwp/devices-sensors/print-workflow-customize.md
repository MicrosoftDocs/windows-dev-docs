---
ms.assetid: 67a46812-881c-404b-9f3b-c6786f39e72b
title: Customize the print workflow
description: Create custom print workflow experiences to meet the needs of your organization.
ms.date: 07/03/2020
ms.topic: article
keywords: windows 10, uwp, printing
ms.localizationpriority: medium
---

# Customize the print workflow

## Overview

Developers can customize the printing workflow experience through the use of a print workflow app. Print workflow apps are UWP apps that expand on the functionality of [Microsoft Store devices apps (WSDAs)](/windows-hardware/drivers/devapps/), so it will be helpful to have some familiarity with WSDAs before going further.

Just as in the case of WSDAs, when the user of a source application elects to print something and navigates through the print dialog, the system checks whether a workflow app is associated with that printer. If it is, the print workflow app launches (primarily as a background task; more on this below). A workflow app is able to alter both the print ticket (the XML document that configures the printer device settings for the current print task) and the actual XPS content to be printed. It can optionally expose this functionality to the user by launching a UI midway through the process. After doing its work, it passes the print content and print ticket on to the driver.

Because it involves background and foreground components, and because it is functionally coupled with other app(s), a print workflow app can be more complicated to implement than other categories of UWP apps. It is recommended that you inspect the [Workflow app sample](https://github.com/Microsoft/print-oem-samples) while reading this guide to better understand how the different features can be implemented. Some features, such as various error checks and UI management, are absent from this guide for the sake of simplicity.

## Getting started

The workflow app must indicate its entry point to the print system so that it can be launched at the appropriate time. This is done by inserting the following declaration in the `Application/Extensions` element of the UWP project's *package.appxmanifest* file.

```xml
<uap:Extension Category="windows.printWorkflowBackgroundTask"  
    EntryPoint="WFBackgroundTasks.WfBackgroundTask" />
```

> [!IMPORTANT]
> There are many scenarios in which the print customization does not require user input. For this reason, Print workflow apps run as background tasks by default.

If a workflow app is associated with the source application that started the print job (see later section for instructions on this), the print system examines its manifest files for a background task entry point.

## Do background work on the print ticket

The first thing the print system does with the workflow app is activate its background task (In this case, the `WfBackgroundTask` class in the `WFBackgroundTasks` namespace). In the background task's `Run` method, you should cast the task's trigger details as a **[PrintWorkflowTriggerDetails](/uwp/api/windows.graphics.printing.workflow.printworkflowtriggerdetails)** instance. This will provide the special functionality for a print workflow background task. It exposes the **[PrintWorkflowSession](/uwp/api/windows.graphics.printing.workflow.printworkflowtriggerdetails.PrintWorkflowSession)** property, which is an instance of **[PrintWorkFlowBackgroundSession](/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsession)**. Print workflow session classes - both the background and foreground varieties - will control the sequential steps of the print workflow app.

Then register handler methods for the two events that this session class will raise. You will define these methods later on.

```csharp
public void Run(IBackgroundTaskInstance taskInstance) {
    // Take out a deferral here and complete once all the callbacks are done
    runDeferral = taskInstance.GetDeferral();

    // Associate a cancellation handler with the background task.
    taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);

    // cast the task's trigger details as PrintWorkflowTriggerDetails
    PrintWorkflowTriggerDetails workflowTriggerDetails = taskInstance.TriggerDetails as PrintWorkflowTriggerDetails;

    // Get the session manager, which is unique to this print job
    PrintWorkflowBackgroundSession sessionManager = workflowTriggerDetails.PrintWorkflowSession;

    // add the event handler callback routines
    sessionManager.SetupRequested += OnSetupRequested;
    sessionManager.Submitted += OnXpsOMPrintSubmitted;

    // Allow the event source to start
    // This call blocks until all of the workflow callbacks complete
    sessionManager.Start();
}
```

When the `Start` method is called, the session manager will raise the **[SetupRequested](/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsession.SetupRequested)** event first. This event exposes general information about the print task, as well as the print ticket. At this stage, the print ticket can be edited in the background.

```csharp
private void OnSetupRequested(PrintWorkflowBackgroundSession sessionManager, PrintWorkflowBackgroundSetupRequestedEventArgs printTaskSetupArgs) {
    // Take out a deferral here and complete once all the callbacks are done
    Deferral setupRequestedDeferral = printTaskSetupArgs.GetDeferral();

    // Get general information about the source application, print job title, and session ID
    string sourceApplicationName = printTaskSetupArgs.Configuration.SourceAppDisplayName;
    string jobTitle = printTaskSetupArgs.Configuration.JobTitle;
    string sessionId = printTaskSetupArgs.Configuration.SessionId;

    // edit the print ticket
    WorkflowPrintTicket printTicket = printTaskSetupArgs.GetUserPrintTicketAsync();

    // ...
```

Importantly, it is in the handling of the **SetupRequested** that the app will determine whether to launch a foreground component. This could depend on a setting that was previously saved to local storage, or an event that occurred during the editing of the print ticket, or it may be a static setting of your particular app.

```csharp
// ...

if (UIrequested) {
    printTaskSetupArgs.SetRequiresUI();

    // Any data that is to be passed to the foreground task must be stored the app's local storage.
    // It should be prefixed with the sourceApplicationName string and the SessionId string, so that
    // it can be identified as pertaining to this workflow app session.
}

// Complete the deferral taken out at the start of OnSetupRequested
setupRequestedDeferral.Complete();
```

## Do foreground work on the print job (optional)

If the **[SetRequiresUI](/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsetuprequestedeventargs.SetRequiresUI)** method was called, then the print system will examine the manifest file for the entry point to the foreground application. The `Application/Extensions` element of your *package.appxmanifest* file must have the following lines. Replace the value of `EntryPoint` with name of the foreground app.

```xml
<uap:Extension Category="windows.printWorkflowForegroundTask"  
    EntryPoint="MyWorkFlowForegroundApp.App" />
```

Next, the print system calls the **OnActivated** method for the given app entry point. In the **OnActivated** method of its _App.xaml.cs_ file, the workflow app should check the activation kind to verify that it is a workflow activation. If so, the workflow app can cast the activation arguments to a **[PrintWorkflowUIActivatedEventArgs](/uwp/api/windows.graphics.printing.workflow.printworkflowuiactivatedeventargs)** object, which exposes a **[PrintWorkflowForegroundSession](/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsession)** object as a property. This object, like its background counterpart in the previous section, contains events that are raised by the print system, and you can assign handlers to these. In this case, the event-handling functionality will be implemented in a separate class called `WorkflowPage`.

First, in the _App.xaml.cs_ file:

```csharp
protected override void OnActivated(IActivatedEventArgs args){

    if (args.Kind == ActivationKind.PrintWorkflowForegroundTask) {

        // the app should instantiate a new UI view so that it can properly handle the case when
        // several print jobs are active at the same time.
        Frame rootFrame = new Frame();
        if (null == Window.Current.Content)
        {
            rootFrame.Navigate(typeof(WorkflowPage));
            Window.Current.Content = rootFrame;
        }

        // Get the main page
        WorkflowPage workflowPage = (WorkflowPage)rootFrame.Content;

        // Make sure the page knows it's handling a foreground task activation
        workflowPage.LaunchType = WorkflowPage.WorkflowPageLaunchType.ForegroundTask;

        // Get the activation arguments
        PrintWorkflowUIActivatedEventArgs printTaskUIEventArgs = args as PrintWorkflowUIActivatedEventArgs;

        // Get the session manager
        PrintWorkflowForegroundSession taskSessionManager = printTaskUIEventArgs.PrintWorkflowSession;

        // Add the callback handlers - these methods are in the workflowPage class
        taskSessionManager.SetupRequested += workflowPage.OnSetupRequested;
        taskSessionManager.XpsDataAvailable += workflowPage.OnXpsDataAvailable;

        // start raising the print workflow events
        taskSessionManager.Start();
    }
}
```

Once the UI has attached event handlers and the **OnActivated** method has exited, the print system will fire the **[SetupRequested](/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsession.SetupRequested)** event for the UI to handle. This event provides the same data that the background task setup event provided, including the print job info and print ticket document, but without the ability to request the launch of additional UI. In the _WorkflowPage.xaml.cs_ file:

```csharp
internal void OnSetupRequested(PrintWorkflowForegroundSession sessionManager, PrintWorkflowForegroundSetupRequestedEventArgs printTaskSetupArgs) {
    // If anything asynchronous is going to be done, you need to take out a deferral here,
    // since otherwise the next callback happens once this one exits, which may be premature
    Deferral setupRequestedDeferral = printTaskSetupArgs.GetDeferral();

    // Get information about the source application, print job title, and session ID
    string sourceApplicationName = printTaskSetupArgs.Configuration.SourceAppDisplayName;
    string jobTitle = printTaskSetupArgs.Configuration.JobTitle;
    string sessionId = printTaskSetupArgs.Configuration.SessionId;
    // the following string should be used when storing data that pertains to this workflow session
    // (such as user input data that is meant to change the print content later on)
    string localStorageVariablePrefix = string.Format("{0}::{1}::", sourceApplicationName, sessionID);

    try
    {
        // receive and store user input
        // ...
    }
    catch (Exception ex)
    {
        string errorMessage = ex.Message;
        Debug.WriteLine(errorMessage);
    }
    finally
    {
        // Complete the deferral taken out at the start of OnSetupRequested
        setupRequestedDeferral.Complete();
    }
}
```

Next, the print system will raise the **[XpsDataAvailable](/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsession.XpsDataAvailable)** event for the UI. In the handler for this event, the workflow app can access all of the data available to the setup event and can additionally read the XPS data directly, either as a stream of raw bytes or as an object model. Access to the XPS data allows the UI to provide print preview services and to provide additional information to the user about the operations that the workflow app will execute on the data.

As part of this event handler, the workflow app must acquire a deferral object if it will continue to interact with the user. Without a deferral, the print system will consider the UI task complete when the **XpsDataAvailable** event handler exits or when it calls an async method. When the app has gathered all required information from the user's interaction with the UI, it should complete the deferral so that the print system can then advance.

```csharp
internal async void OnXpsDataAvailable(PrintWorkflowForegroundSession sessionManager, PrintWorkflowXpsDataAvailableEventArgs printTaskXpsAvailableEventArgs)
{
    // Take out a deferral
    Deferral xpsDataAvailableDeferral = printTaskXpsAvailableEventArgs.GetDeferral();

    SpoolStreamContent xpsStream = printTaskXpsAvailableEventArgs.Operation.XpsContent.GetSourceSpoolDataAsStreamContent();

    IInputStream inputStream = xpsStream.GetInputSpoolStream();

    using (var inputReader = new Windows.Storage.Streams.DataReader(inputStream))
    {
        // Read the XPS data from input stream
        byte[] xpsData = new byte[inputReader.UnconsumedBufferLength];
        while (inputReader.UnconsumedBufferLength > 0)
        {
            inputReader.ReadBytes(xpsData);
            // Do something with the XPS data, e.g. preview
            // ...
        }
    }

    // Complete the deferral taken out at the start of this method
    xpsDataAvailableDeferral.Complete();
}
```

Additionally, the **[PrintWorkflowSubmittedOperation](/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedoperation)** instance exposed by the event args provides the option to cancel the print job or to indicate that the job is successful but that no output print job will be needed. This is done by calling the **[Complete](/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedoperation.Complete)** method with a **[PrintWorkflowSubmittedStatus](/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedstatus)** value.

> [!NOTE]
> If the workflow app cancels the print job, it is highly recommended that it provide a toast notification indicating why the job was cancelled.

## Do final background work on the print content

Once the UI has completed the deferral in the **PrintTaskXpsDataAvailable** event (or if the UI step was bypassed), the print system will fire the **[Submitted](/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsession.Submitted)** event for the background task. In the handler for this event, the workflow app can get access to all of the same data provided by the **XpsDataAvailable** event. However, unlike any of the previous events, **Submitted** also provides *write* access to the final print job content through a **[PrintWorkflowTarget](/uwp/api/windows.graphics.printing.workflow.printworkflowtarget)** instance.

The object that is used to spool the data for final printing depends on whether the source data is accessed as a raw byte stream or as the XPS object model. When the workflow app accesses the source data through a byte stream, an output byte stream is provided to write the final job data to. When the workflow app accesses the source data through the object model, a document writer is provided to write objects to the output job. In either case, the workflow app should read all of the source data, modify any data required, and write the modified data to the output target.

When the background task finishes writing the data, it should call **Complete** on the corresponding **PrintWorkflowSubmittedOperation** object. Once the workflow app completes this step and the **Submitted** event handler exits, the workflow session is closed and the user can monitor the status of the final print job through the standard print dialogs.

## Final steps

### Register the print workflow app to the printer

Your workflow app is associated with a printer using the same type of metadata file submission as for WSDAs. In fact, a single UWP application can act as both a workflow app and a WSDA that provides print task settings functionality. Follow the corresponding [WSDA steps for creating the metadata association](/windows-hardware/drivers/devapps/step-2--create-device-metadata).

The difference is that while WSDAs are automatically activated for the user (the app will always launch when that user prints on the associated device), workflow apps are not. They have a separate policy that must be set.

### Set the workflow app's policy

The workflow app policy is set by Powershell commands on the device that is to run the workflow app. The Set-Printer, Add-Printer (existing port) and Add-Printer (new WSD port) commands will be modified to allow Workflow policies to be set.

* `Disabled`: Workflow apps will not be activated.
* `Uninitialized`: Workflow apps will be activated if the Workflow DCA is installed in the system. If the app is not installed, printing will still proceed.
* `Enabled`: Workflow contract will be activated if the Workflow DCA is installed in the system. If the app is not installed, printing will fail.

The following command makes the workflow app required on the specified printer.

```Powershell
Set-Printer –Name "Microsoft XPS Document Writer" -WorkflowPolicy Enabled
```

A local user can run this policy on a local printer, or, for enterprise implementation, the printer administrator can run this policy on the Print Server. The policy will then be synchronized to all client connections. The printer admin can use this policy whenever a new printer is added.

## See also

[Workflow app sample](https://github.com/Microsoft/print-oem-samples)

[Windows.Graphics.Printing.Workflow namespace](/uwp/api/windows.graphics.printing.workflow)