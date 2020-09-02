---
title: Port an out-of-process background task to an in-process background task
description: Port an out-of-process background task to an in-process background task that runs inside your foreground app process.
ms.date: 09/19/2018
ms.topic: article
keywords: windows 10, uwp, background task, app service
ms.assetid: 5327e966-b78d-4859-9b97-5a61c362573e
ms.localizationpriority: medium
---
# Port an out-of-process background task to an in-process background task

The simplest way to port your out-of-process (OOP) background activity to in-process activity is to bring your [IBackgroundTask.Run](/uwp/api/windows.applicationmodel.background.ibackgroundtask.run?f=255&MSPPError=-2147217396) method code inside your application, and initiate it from [OnBackgroundActivated](/uwp/api/windows.ui.xaml.application.onbackgroundactivated). The technique being described here is not about creating a shim from an OOP background task to an in-process background task; it's about rewriting (or porting) an OOP version to an in-process version.

If your app has multiple background tasks, the [Background Activation Sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/BackgroundActivation) shows how you can use `BackgroundActivatedEventArgs.TaskInstance.Task.Name` to identify which task is being initiated.

If you are currently communicating between background and foreground processes, you can remove that state management and communication code.

## Background tasks and trigger types that cannot be converted

* In-process background tasks don't support activating a VoIP background task.
* In-process background tasks don't support the following triggers:  [DeviceUseTrigger](/uwp/api/windows.applicationmodel.background.deviceusetrigger?f=255&MSPPError=-2147217396), [DeviceServicingTrigger](/uwp/api/windows.applicationmodel.background.deviceservicingtrigger) and **IoTStartupTask**