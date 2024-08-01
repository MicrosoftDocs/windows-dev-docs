---
ms.assetid: 
description: Learn how to capture game video, audio, and screenshots, and how to submit metadata that the system embeds in captured and broadcast media.
title: Capture game audio, video, screenshots, and metadata
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, game, capture, audio, video, metadata
ms.localizationpriority: medium
---
# Capture game audio, video, screenshots, and metadata
This article describes how to capture game video, audio, and screenshots, and how to submit metadata that the system will embed in captured and broadcast media, allowing your app and others to create dynamic experiences that are synchronized to gameplay events. 

There are two different ways that gameplay can be captured in a UWP app. The user can initiate capture using the built-in system UI. Media that is captured using this technique is ingested into the Microsoft gaming ecosystem, can be viewed and shared through first-party experiences such as the Xbox app, and is not directly available to your app or to users. The first sections of this article will show you how to enable and disable system-implemented app capture and how to receive notifications when app capture starts or stops.

The other way to capture media is to use the APIs of the **[Windows.Media.AppRecording](/uwp/api/windows.media.apprecording)** namespace. If capturing is enabled on the device, your app can start capturing gameplay and then, after some time has passed, you can stop the capture, at which point the media is written to a file. If the user has enabled historical capture, then you can also record gameplay that has already occurred by specifying a start time in the past and a duration to record. Both of these techniques produce a video file that can be accessed by your app, and depending on where you choose to save the files, by the user. The middle sections of this article walk you through the implementation of these scenarios.

The **[Windows.Media.Capture](/uwp/api/windows.media.capture)** namespace provides APIs for creating metadata that describes the gameplay being captured or broadcast. This can include text or numeric values, with a text label identifying each data item. Metadata can represent an "event" which occurs at a single moment, such as when the user finishes a lap in a racing game, or it can represent a "state" that persists over a span of time, such as the current game map the user is playing in. The metadata is written to a cache that is allocated and managed for your app by the system. The metadata is embedded into broadcast streams and captured video files, including both the built-in system capture or custom app capture techniques. The final sections of this article show you how to write gameplay metadata.

> [!NOTE] 
> Because the gameplay metadata can be embedded in media files that can potentially be shared over the network, out of the user's control, you should not include personally identifiable information or other potentially sensitive data in the metadata.


## Enable and disable system app capture
System app capture is initiated by the user with the built-in system UI. The files are ingested by the Windows gaming ecosystem and is not available to your app or the user, except for through first party experiences like the Xbox app. Your app can disable and enable system-initiated app capture, allowing you to prevent the user from capturing certain content or gameplay. 

To enable or disable system app capture, simply call the static method **[AppCapture.SetAllowedAsync](/uwp/api/windows.media.capture.appcapture.setallowedasync)** and passing **false** to disable capture or **true** to enable capture.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetSetAppCaptureAllowed":::


## Receive notifications when system app capture starts and stops
To receive a notification when system app capture begins or ends, first get an instance of the **[AppCapture](/uwp/api/windows.media.capture.appcapture)** class by calling the factory method **[GetForCurrentView](/uwp/api/windows.media.capture.appcapture.GetForCurrentView)**. Next, register a handler for the **[CapturingChanged](/uwp/api/windows.media.capture.appcapture.CapturingChanged)** event.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetRegisterCapturingChanged":::

In the handler for the **CapturingChanged** event, you can check the **[IsCapturingAudio](/uwp/api/windows.media.capture.appcapture.IsCapturingAudio)** and the **[IsCapturingVideo](/uwp/api/windows.media.capture.appcapture.IsCapturingVideo)** properties to determine if audio or video are being captured respectively. You may want to update your app's UI to indicate the current capturing status.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetOnCapturingChanged":::

## Add the Windows Desktop Extensions for the UWP to your app
The APIs for recording audio and video and for capturing screenshots directly from your app, found in the **[Windows.Media.AppRecording](/uwp/api/windows.media.apprecording)** namespace, are not included in the Universal API contract. To access the APIs, you must add a reference to the Windows Desktop Extensions for the UWP to your app with the following steps.

1. In Visual Studio, in **Solution Explorer**, expand your UWP project and right-click **References** and then select **Add Reference...**. 
2. Expand the **Universal Windows** node and select **Extensions**.
3. In the list of extensions check the checkbox next to the **Windows Desktop Extensions for the UWP** entry that matches the target build for your project. For the app broadcast features, the version must be 1709 or greater.
4. Click **OK**.

## Get an instance of AppRecordingManager
The **[AppRecordingManager](/uwp/api/windows.media.apprecording.apprecordingmanager)** class is the central API you will use to manage app recording. Get an instance of this class by calling the factory method **[GetDefault](/uwp/api/windows.media.apprecording.apprecordingmanager.GetDefault)**. Before using any of the APIs in the **Windows.Media.AppRecording** namespace, you should check for their presence on the current device. The APIs are not available on devices running an OS version earlier than Windows 10, version 1709. Rather than check for a specific OS version, use the **[ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent)** method to query for the *Windows.Media.AppBroadcasting.AppRecordingContract* version 1.0. If this contract is present, then the recording APIs are available on the device. The example code in this article checks for the APIs once and then checks if the **AppRecordingManager** is null before subsequent operations.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetGetAppRecordingManager":::

## Determine if your app can currently record
There are several reasons that your app may not currently be able to capture audio or video, including if the current device doesn't meet the hardware requirements for recording or if another app is currently broadcasting. Before initiating a recording, you can check to see if your app is currently able to record. Call the **[GetStatus](/uwp/api/windows.media.apprecording.apprecordingmanager.GetStatus)** method of the **AppRecordingManager** object and then check the **[CanRecord](/uwp/api/windows.media.apprecording.apprecordingstatus.CanRecord)** property of the returned **[AppRecordingStatus](/uwp/api/windows.media.apprecording.apprecordingstatus)** object. If **CanRecord** returns **false**, meaning that your app can't currently record, you can check the **[Details](/uwp/api/windows.media.apprecording.apprecordingstatus.Details)** property to determine the reason. Depending on the reason, you may want to display the status to the user or show instructions for enabling app recording.



:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetCanRecord":::

## Manually start and stop recording your app to a file

After verifying that your app is able to record, you can start a new recording by calling the **[StartRecordingToFileAsync](/uwp/api/windows.media.apprecording.apprecordingmanager.startrecordingtofileasync)** method of the **AppRecordingManager** object.

In the following example, the first **then** block executes when the asynchronous task fails. The second **then** block attempts to access the result of the task and, if the result is null, then the task has completed. In both cases, the **OnRecordingComplete** helper method, shown below, is called to handle the result. 

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetStartRecordToFile":::

When the recording operation completes, check the **[Succeeded](/uwp/api/windows.media.apprecording.apprecordingresult.Succeeded)** property of the returned **[AppRecordingResult](/uwp/api/windows.media.apprecording.apprecordingresult)** object to determine if the record operation was successful. If so, you can check the **[IsFileTruncated](/uwp/api/windows.media.apprecording.apprecordingresult.IsFileTruncated)** property to determine if, for storage reasons, the system was forced to truncate the captured file. You can check the **[Duration](/uwp/api/windows.media.apprecording.apprecordingresult.Duration)** property to discover the actual duration of the recorded file which, if the file is truncated, may be shorter than the duration of the recording operation.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetOnRecordingComplete":::

The following examples show some basic code for starting and stopping the recording operation shown in the previous example.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetCallStartRecordToFile":::

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetFinishRecordToFile":::

## Record a historical time span to a file
If the user has enabled historical recording for your app in the system settings, you can record a time span of gameplay that has previously transpired. A previous example in this article showed how to confirm that your app can currently record gameplay. There is an additional check to determine if historical capture is enabled. Once again, call **[GetStatus](/uwp/api/windows.media.apprecording.apprecordingmanager.GetStatus)** and check the **[CanRecordTimeSpan](/uwp/api/windows.media.apprecording.apprecordingstatus.CanRecordTimeSpan)** property of the returned **AppRecordingStatus** object. This example also returns the **[HistoricalBufferDuration](/uwp/api/windows.media.apprecording.apprecordingstatus.HistoricalBufferDuration)** property of the **AppRecordingStatus** which will be used to determine a valid start time for the recording operation.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetCanRecordTimeSpan":::

To capture a historical timespan, you must specify a start time for the recording and a duration. The start time is provided as a **[DateTime](/uwp/api/windows.foundation.datetime)** struct. The start time must be a time before the current time, within the length of the historical recording buffer. For this example, the buffer length is retrieved as part of the check to see if historical recording is enabled, which is shown in the previous code example. The duration of the historical recording is provided as  **[TimeSpan](/uwp/api/windows.foundation.timespan)** struct, which should also be equal to or smaller than the duration of the historical buffer. Once you have determined your desired start time and duration, call **[RecordTimeSpanToFileAsync](/uwp/api/windows.media.apprecording.apprecordingmanager.recordtimespantofileasync)** to start the recording operation.

Like recording with manual start and stop, when a historical recording completes, you can check the **[Succeeded](/uwp/api/windows.media.apprecording.apprecordingresult.Succeeded)** property of the returned **[AppRecordingResult](/uwp/api/windows.media.apprecording.apprecordingresult)** object to determine if the record operation was successful, and you can check the **[IsFileTruncated](/uwp/api/windows.media.apprecording.apprecordingresult.IsFileTruncated)** and **[Duration](/uwp/api/windows.media.apprecording.apprecordingresult.Duration)** property to discover the actual duration of the recorded file which, if the file is truncated, may be shorter than the duration of the requested time window.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetRecordTimeSpanToFile":::

The following example shows some basic code for initiating the historical record operation shown in the previous example.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetCallRecordTimeSpanToFile":::

## Save screenshot images to files
Your app can initiate a screenshot capture that will save the current contents of the app's window to one image file or to multiple image files with different image encodings. To specify the image encodings you would like to use, create a list of strings where each represents an image type. The properties of the **[ImageEncodingSubtypes](/uwp/api/windows.media.mediaproperties.mediaencodingsubtypes)** provide the correct string for each supported image type, such as **MediaEncodingSubtypes.Png** or **MediaEncodingSubtypes.JpegXr**.

Initiate screen capture by calling the **[SaveScreenshotToFilesAsync](/uwp/api/windows.media.apprecording.apprecordingmanager.savescreenshottofilesasync)** method of the **AppRecordingManager** object. The first parameter to this method is a **StorageFolder** where the image files will be saved. The second parameter is a filename prefix to which the system will append the extension for each image type saved, such as ".png".

The third parameter to **SaveScreenshotToFilesAsync** is necessary for the system to be able to do the proper colorspace conversion if the current window to be captured is displaying HDR content. If HDR content is present, this parameter should be set to **AppRecordingSaveScreenshotOption.HdrContentVisible**. Otherwise, use **AppRecordingSaveScreenshotOption.None**. The final parameter to the method is the list of image formats to which the screen should be captured.

When the asynchronous call to **SaveScreenshotToFilesAsync** completes, it returns a **[AppRecordingSavedScreenshotInfo](/uwp/api/windows.media.apprecording.apprecordingsavedscreenshotinfo)** object that provides the **StorageFile** and associated **MediaEncodingSubtypes** value indicating the image type for each saved image.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetSaveScreenShotToFiles":::

The following example shows some basic code for initiating the screenshot operation shown in the previous example.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetCallSaveScreenShotToFiles":::

## Add game metadata for system and app-initiated capture
The following sections of this article describe how to provide metadata that the system will embed into the MP4 stream of captured or broadcast gameplay. Metadata can be embedded in media that is captured using the built-in system UI and media that is captured by the app with **AppRecordingManager**. This metadata can be extracted by your app and other apps during media playback in order to provide contextually-aware experiences that are synchronized with the captured or broadcast gameplay.

### Get an instance of AppCaptureMetadataWriter
The primary class for managing app capture metadata is **[AppCaptureMetadataWriter](/uwp/api/windows.media.capture.appcapturemetadatawriter)**. Before initializing an instance of this class, use the **[ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent)** method to query for the *Windows.Media.Capture.AppCaptureMetadataContract* version 1.0 to verify that the API is available on the current device.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetGetMetadataWriter":::

### Write metadata to the system cache for your app
Each metadata item has a string label, identifying the metadata item, an associated data value which can be a string, an integer, or a double value, and a value from the **[AppCaptureMetadataPriority](/uwp/api/windows.media.capture.appcapturemetadatapriority)** enumeration indicating the relative priority of the data item. A metadata item can either be considered an "event", which occurs at a single point in time, or a "state" which maintains a value over a time window. Metadata is written to a memory cache that is allocated and managed for your app by the system. The system enforces a size limit on the metadata memory cache and, when the limit is reached, will purge data based on the priority with which each metadata item was written. The next section of this article shows how to manage your app's metadata memory allocation.

A typical app may choose to write some metadata at the beginning of the capture session to provide some context for the subsequent data. For this scenario it is recommended that you use instantaneous "event" data. This example calls **[AddStringEvent](/uwp/api/windows.media.capture.appcapturemetadatawriter.addstringevent)**, **[AddDoubleEvent](/uwp/api/windows.media.capture.appcapturemetadatawriter.adddoubleevent)**, and **[AddInt32Event](/uwp/api/windows.media.capture.appcapturemetadatawriter.addint32event)** to set instantaneous values for each data type.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetStartSession":::

A common scenario for using "state" data that persists over time is to track the game map that the player is currently within. This example calls **[StartStringState](/uwp/api/windows.media.capture.appcapturemetadatawriter.startstringstate)** to set the state value. 

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetStartMap":::

Call **[StopState](/uwp/api/windows.media.capture.appcapturemetadatawriter.stopstate)** to record that a particular state has ended.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetEndMap":::

You can overwrite a state by setting a new value with an existing state label.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetLevelUp":::

You can end all currently open states by calling **[StopAllStates](/uwp/api/windows.media.capture.appcapturemetadatawriter.StopAllStates)**.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetRaceComplete":::

### Manage metadata cache storage limit
The metadata that you write with **AppCaptureMetadataWriter** is cached by the system until it is written to the associated media stream. The system defines a size limit for each app's metadata cache. Once the cache size limit has been reached, the system will begin purging cached metadata. The system will delete metadata that was written with **[AppCaptureMetadataPriority.Informational](/uwp/api/windows.media.capture.appcapturemetadatapriority)** priority value before deleting metadata with the **[AppCaptureMetadataPriority.Important](/uwp/api/windows.media.capture.appcapturemetadatapriority)** priority.

At any point, you can check to see the number of bytes available in your app's metadata cache by calling **[RemainingStorageBytesAvailable](/uwp/api/windows.media.capture.appcapturemetadatawriter.RemainingStorageBytesAvailable)**. You can choose to set your own app-defined threshold after which you can choose to reduce the amount of metadata that you write to the cache. The following example shows a simple implementation of this pattern.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetCheckMetadataStorage":::

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetComboExecuted":::

### Receive notifications when the system purges metadata
You can register to receive a notification when the system begins purging metadata for your app by registering a handler for the **[MetadataPurged](/uwp/api/windows.media.capture.appcapturemetadatawriter.MetadataPurged)** event.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetRegisterMetadataPurged":::

In the handler for the **MetadataPurged** event, you can clear up some room in the metadata cache by ending lower-priority states, you can implement app-defined logic for reducing the amount of metadata you write to the cache, or you can do nothing and let the system continue to purge the cache based on the priority with which it was written.

:::code language="cpp" source="~/../snippets-windows/windows-uwp/gaming/AppRecordingExample/cpp/AppRecordingExample/App.cpp" id="SnippetOnMetadataPurged":::

## Related topics

* [Gaming](index.md)
 

 
