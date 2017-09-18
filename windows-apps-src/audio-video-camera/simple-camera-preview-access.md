---
author: drewbatgit
ms.assetid: 9BA3F85A-970F-411C-ACB1-B65768B8548A
description: This article describes how to quickly display the camera preview stream within a XAML page in a Universal Windows Platform (UWP) app.
title: Display the camera preview
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Display the camera preview

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

This article describes how to quickly display the camera preview stream within a XAML page in a Universal Windows Platform (UWP) app. Creating an app that captures photos and videos using the camera requires you to perform tasks like handling device and camera orientation or setting encoding options for the captured file. For some app scenarios, you may want to just simply show the preview stream from the camera without worrying about these other considerations. This article shows you how to do that with a minimum of code. Note that you should always shut down the preview stream properly when you are done with it by following the steps below.

For information on writing a camera app that captures photos or videos, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md).

## Add capability declarations to the app manifest

In order for your app to access a device's camera, you must declare that your app uses the *webcam* and *microphone* device capabilities. 

**Add capabilities to the app manifest**

1.  In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2.  Select the **Capabilities** tab.
3.  Check the box for **Webcam** and the box for **Microphone**.

## Add a CaptureElement to your page

Use a [**CaptureElement**](https://msdn.microsoft.com/library/windows/apps/br209278) to display the preview stream within your XAML page.

[!code-xml[CaptureElement](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml#SnippetCaptureElement)]



## Use MediaCapture to start the preview stream

The [**MediaCapture**](https://msdn.microsoft.com/library/windows/apps/br241124) object is your app's interface to the device's camera. This class is a member of the Windows.Media.Capture namespace. The example in this article also uses APIs from the [**Windows.ApplicationModel**](https://msdn.microsoft.com/library/windows/apps/br224691) and [System.Threading.Tasks](https://msdn.microsoft.com/library/windows/apps/xaml/system.threading.tasks.aspx) namespaces, in addition to those included by the default project template.

Add using directives to include the following namespaces in your page's .cs file.

[!code-cs[SimpleCameraPreviewUsing](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetSimpleCameraPreviewUsing)]

Declare a class member variable for the **MediaCapture** object and a boolean to track whether the camera is currently previewing. 

[!code-cs[DeclareMediaCapture](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetDeclareMediaCapture)]

Declare a variable of type [**DisplayRequest**](https://msdn.microsoft.com/library/windows/apps/Windows.System.Display.DisplayRequest) that will be used to make sure the display does not turn off while the preview is running.

[!code-cs[DeclareDisplayRequest](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetDeclareDisplayRequest)]

Create a helper method to start up the camera preview, called **StartPreviewAsync** in this example. Depending on your app's scenario, you may want to call this from the **OnNavigatedTo** event handler that is called when the page is loaded or wait and launch the preview in response to UI events.

Create a new instance of the **MediaCapture** class and call [**InitializeAsync**](https://msdn.microsoft.com/library/windows/apps/br226598) to initialize the capture device. This method may fail, on devices that don't have a camera for example, so you should call it from within a **try** block. An **UnauthorizedAccessException** will be thrown when you attempt to initialize the camera if the user has disabled camera access in the device's privacy settings. You will also see this exception during development if you have neglected to add the proper capabilities to your app manifest.

**Important** On some device families, a user consent prompt is displayed to the user before your app is granted access to the device's camera. For this reason, you must only call [**MediaCapture.InitializeAsync**](https://msdn.microsoft.com/library/windows/apps/br226598) from the main UI thread. Attempting to initialize the camera from another thread may result in initialization failure.

Connect the **MediaCapture** to the **CaptureElement** by setting the [**Source**](https://msdn.microsoft.com/library/windows/apps/br209280) property. Start the preview by calling [**StartPreviewAsync**](https://msdn.microsoft.com/library/windows/apps/br226613). This method will throw a **FileLoadException** if another app has exclusive control of the capture device. See the next section for information listening for changes in exclusive control.

Call [**RequestActive**](https://msdn.microsoft.com/library/windows/apps/Windows.System.Display.DisplayRequest.RequestActive) to make sure the device doesn't go to sleep while the preview is running. Finally, set the [**DisplayInformation.AutoRotationPreferences**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences) property to [**Landscape**](https://msdn.microsoft.com/library/windows/apps/Windows.Graphics.Display.DisplayOrientations) to prevent the UI and the **CaptureElement** from rotating when the user changes the device orientation. For more information on handling device orientation changes, see [**Handle device orientation with MediaCapture**](handle-device-orientation-with-mediacapture.md).  

[!code-cs[StartPreviewAsync](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetStartPreviewAsync)]

## Handle changes in exclusive control
As stated in the previous section, **StartPreviewAsync** will throw a **FileLoadException** if another app has exclusive control of the capture device. Starting with Windows 10, version 1703, you can register a handler for the [MediaCapture.CaptureDeviceExclusiveControlStatusChanged](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture#Windows_Media_Capture_MediaCapture_CaptureDeviceExclusiveControlStatusChanged) event, which is raised whenever the exclusive control status of the device changes. In the handler for this event, check the [MediaCaptureDeviceExclusiveControlStatusChangedEventArgs.Status](https://docs.microsoft.com/en-us/uwp/api/windows.media.capture.mediacapturedeviceexclusivecontrolstatuschangedeventargs#Windows_Media_Capture_MediaCaptureDeviceExclusiveControlStatusChangedEventArgs_Status) property to see what the current status is. If the new status is **SharedReadOnlyAvailable**, then you know you can't currently start the preview and you may want to update your UI to alert the user. If the new status is **ExclusiveControlAvailable**, then you can try starting the camera preview again.

[!code-cs[ExclusiveControlStatusChanged](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetExclusiveControlStatusChanged)]

## Shut down the preview stream

When you are done using the preview stream, you should always shut down the stream and properly dispose of the associated resources to ensure that the camera is available to other apps on the device. The required steps for shutting down the preview stream are:

-   If the camera is currently previewing, call [**StopPreviewAsync**](https://msdn.microsoft.com/library/windows/apps/br226622) to stop the preview stream. An exception will be thrown if you call **StopPreviewAsync** while the preview is not running.
-   Set the [**Source**](https://msdn.microsoft.com/library/windows/apps/br209280) property of the **CaptureElement** to null. Use [**CoreDispatcher.RunAsync**](https://msdn.microsoft.com/library/windows/apps/windows.ui.core.coredispatcher.runasync.aspx) to make sure this call is executed on the UI thread.
-   Call the **MediaCapture** object's [**Dispose**](https://msdn.microsoft.com/library/windows/apps/dn278858) method to release the object. Again, use [**CoreDispatcher.RunAsync**](https://msdn.microsoft.com/library/windows/apps/windows.ui.core.coredispatcher.runasync.aspx) to make sure this call is executed on the UI thread.
-   Set the **MediaCapture** member variable to null.
-   Call [**RequestRelease**](https://msdn.microsoft.com/library/windows/apps/Windows.System.Display.DisplayRequest.RequestRelease) to allow the screen to turn off when inactive.

[!code-cs[CleanupCameraAsync](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetCleanupCameraAsync)]

You should shut down the preview stream when the user navigates away from your page by overriding the [**OnNavigatedFrom**](https://msdn.microsoft.com/library/windows/apps/br227507) method.

[!code-cs[OnNavigatedFrom](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetOnNavigatedFrom)]

You should also shut down the preview stream properly when your app is suspending. To do this, register a handler for the [**Application.Suspending**](https://msdn.microsoft.com/library/windows/apps/br205860) event in your page's constructor.

[!code-cs[RegisterSuspending](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetRegisterSuspending)]

In the **Suspending** event handler, first check to make sure that the page is being displayed the application's [**Frame**](https://msdn.microsoft.com/library/windows/apps/br242682) by comparing the page type to the [**CurrentSourcePageType**](https://msdn.microsoft.com/library/windows/apps/hh702390) property. If the page is not currently being displayed, then the **OnNavigatedFrom** event should already have been raised and the preview stream shut down. If the page is currently being displayed, get a [**SuspendingDeferral**](https://msdn.microsoft.com/library/windows/apps/br224684) object from the event args passed into the handler to make sure the system does not suspend your app until the preview stream has been shut down. After shutting down the stream, call the deferral's [**Complete**](https://msdn.microsoft.com/library/windows/apps/br224685) method to let the system continue suspending your app.

[!code-cs[SuspendingHandler](./code/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs#SnippetSuspendingHandler)]


## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Get a preview frame](get-a-preview-frame.md)
