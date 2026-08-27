---
description: Learn how to capture a variable photo sequence, which allows you to capture multiple frames of images in rapid succession and configure each frame to use different focus, flash, ISO, exposure, and exposure compensation settings.
title: Variable photo sequence
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, windows 11, winui3, camera
ms.localizationpriority: medium
---
# Variable photo sequence

This article shows you how to capture a variable photo sequence, which allows you to capture multiple frames of images in rapid succession and configure each frame to use different focus, flash, ISO, exposure, and exposure compensation settings. This feature enables scenarios like creating High Dynamic Range (HDR) images.

If you want to capture HDR images but don't want to implement your own processing algorithm, you can use the [**AdvancedPhotoCapture**](/uwp/api/Windows.Media.Capture.AdvancedPhotoCapture) API to use the HDR capabilities built-in to Windows. For more information, see [High dynamic range (HDR) and low-light photo capture](hdr-low-light-photo-capture.md).

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md), which describes the steps for implementing basic photo and video capture. It is recommended that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

## Set up your app to use variable photo sequence capture

Declare a member variable to store the [**VariablePhotoSequenceCapture**](/uwp/api/Windows.Media.Capture.Core.VariablePhotoSequenceCapture) object, which is used to initiate the photo sequence capture. Declare an array of [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects to store each captured image in the sequence. Also, declare an array to store the [**CapturedFrameControlValues**](/uwp/api/Windows.Media.Capture.CapturedFrameControlValues) object for each frame. This can be used by your image processing algorithm to determine what settings were used to capture each frame. Finally, declare an index that will be used to track which image in the sequence is currently being captured.

```csharp
VariablePhotoSequenceCapture m_photoSequenceCapture;
SoftwareBitmap[] m_images;
CapturedFrameControlValues[] m_frameControlValues;
int m_photoIndex;
```

## Prepare the variable photo sequence capture

After you have initialized your [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture), make sure that variable photo sequences are supported on the current device by getting an instance of the [**VariablePhotoSequenceController**](/uwp/api/Windows.Media.Devices.Core.VariablePhotoSequenceController) from the media capture's [**VideoDeviceController**](/uwp/api/Windows.Media.Devices.VideoDeviceController) and checking the [**Supported**](/uwp/api/windows.media.devices.core.variablephotosequencecontroller.supported) property.

```csharp
var varPhotoSeqController = m_mediaCapture.VideoDeviceController.VariablePhotoSequenceController;

if (!varPhotoSeqController.Supported)
{
    tbStatus.Text = "Variable Photo Sequence is not supported";
    return;
}
```

Get a [**FrameControlCapabilities**](/uwp/api/Windows.Media.Devices.Core.FrameControlCapabilities) object from the variable photo sequence controller. This object has a property for every setting that can be configured per frame of a photo sequence. These include:

-   [**Exposure**](/uwp/api/windows.media.devices.core.framecontrolcapabilities.exposure)
-   [**ExposureCompensation**](/uwp/api/windows.media.devices.core.framecontrolcapabilities.exposurecompensation)
-   [**Flash**](/uwp/api/windows.media.devices.core.framecontrolcapabilities.flash)
-   [**Focus**](/uwp/api/windows.media.devices.core.framecontrolcapabilities.focus)
-   [**IsoSpeed**](/uwp/api/windows.media.devices.core.framecontrolcapabilities.isospeed)
-   [**PhotoConfirmation**](/uwp/api/windows.media.devices.core.framecontrolcapabilities.photoconfirmationsupported)

This example will set a different exposure compensation value for each frame. To verify that exposure compensation is supported for photo sequences on the current device, check the [**Supported**](/uwp/api/windows.media.devices.exposurecompensationcontrol.supported) property of the [**FrameExposureCompensationCapabilities**](/uwp/api/Windows.Media.Devices.Core.FrameExposureCompensationCapabilities) object accessed through the **ExposureCompensation** property.

```csharp
var frameCapabilities = varPhotoSeqController.FrameCapabilities;

if (!frameCapabilities.ExposureCompensation.Supported)
{
    tbStatus.Text = "EVCompenstaion is not supported in FrameController";
    return;
}
```

Create a new [**FrameController**](/uwp/api/Windows.Media.Devices.Core.FrameController) object for each frame you want to capture. This example captures three frames. Set the values for the controls you want to vary for each frame. Then, clear the [**DesiredFrameControllers**](/uwp/api/windows.media.devices.core.variablephotosequencecontroller.desiredframecontrollers) collection of the **VariablePhotoSequenceController** and add each frame controller to the collection.

```csharp
var frame0 = new FrameController();
var frame1 = new FrameController();
var frame2 = new FrameController();

frame0.ExposureCompensationControl.Value = -1.0f;
frame1.ExposureCompensationControl.Value = 0.0f;
frame2.ExposureCompensationControl.Value = 1.0f;

varPhotoSeqController.DesiredFrameControllers.Clear();
varPhotoSeqController.DesiredFrameControllers.Add(frame0);
varPhotoSeqController.DesiredFrameControllers.Add(frame1);
varPhotoSeqController.DesiredFrameControllers.Add(frame2);
```

Create an [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) object to set the encoding you want to use for the captured images. Call the static method [**MediaCapture.PrepareVariablePhotoSequenceCaptureAsync**](/uwp/api/windows.media.capture.mediacapture.preparevariablephotosequencecaptureasync), passing in the encoding properties. This method returns a [**VariablePhotoSequenceCapture**](/uwp/api/Windows.Media.Capture.Core.VariablePhotoSequenceCapture) object. Finally, register event handlers for the [**PhotoCaptured**](/uwp/api/windows.media.capture.core.variablephotosequencecapture.photocaptured) and [**Stopped**](/uwp/api/windows.media.capture.core.variablephotosequencecapture.stopped) events.

```csharp
try
{
    var imageEncodingProperties = ImageEncodingProperties.CreateJpeg();

    m_photoSequenceCapture = await m_mediaCapture.PrepareVariablePhotoSequenceCaptureAsync(imageEncodingProperties);

    m_photoSequenceCapture.PhotoCaptured += OnPhotoCaptured;
    m_photoSequenceCapture.Stopped += OnStopped;
}
catch (Exception ex)
{
    tbStatus.Text = "Exception in PrepareVariablePhotoSequence: " + ex.Message;
}
```

## Start the variable photo sequence capture

To start the capture of the variable photo sequence, call [**VariablePhotoSequenceCapture.StartAsync**](/uwp/api/windows.media.capture.core.variablephotosequencecapture.startasync). Be sure to initialize the arrays for storing the captured images and frame control values and set the current index to 0. Set your app's recording state variable and update your UI to disable starting another capture while this capture is in progress.

```csharp
private async void bStartVPS_Click(object sender, RoutedEventArgs e)
{
    m_images = new SoftwareBitmap[3];
    m_frameControlValues = new CapturedFrameControlValues[3];
    m_photoIndex = 0;
    m_isRecording = true;

    await m_photoSequenceCapture.StartAsync();
}
```

## Receive the captured frames

The [**PhotoCaptured**](/uwp/api/windows.media.capture.core.variablephotosequencecapture.photocaptured) event is raised for each captured frame. Save the frame control values and captured image for the frame and then increment the current frame index. This example shows how to get a [**SoftwareBitmap**](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) representation of each frame. For more information on using **SoftwareBitmap**, see [Imaging](/windows/uwp/audio-video-camera/imaging).

```csharp
void OnPhotoCaptured(VariablePhotoSequenceCapture s, VariablePhotoCapturedEventArgs args)
{

    m_images[m_photoIndex] = args.Frame.SoftwareBitmap;
    m_frameControlValues[m_photoIndex] = args.CapturedFrameControlValues;
    m_photoIndex++;
}
```

## Handle the completion of the variable photo sequence capture

The [**Stopped**](/uwp/api/windows.media.capture.core.variablephotosequencecapture.stopped) event is raised when all of the frames in the sequence have been captured. Update the recording state of your app and update your UI to allow the user to initiate new captures. At this point, you can pass the captured images and frame control values to your image processing code.

```csharp
void OnStopped(object s, object e)
{
    m_isRecording = false;
    MyPostProcessingFunction(m_images, m_frameControlValues, 3);
}
```

## Update frame controllers

If you want to perform another variable photo sequence capture with different per frame settings, you don't need to completely reinitialize the **VariablePhotoSequenceCapture**. You can either clear the [**DesiredFrameControllers**](/uwp/api/windows.media.devices.core.variablephotosequencecontroller.desiredframecontrollers) collection and add new frame controllers or you can modify the existing frame controller values. The following example checks the [**FrameFlashCapabilities**](/uwp/api/Windows.Media.Devices.Core.FrameFlashCapabilities) object to verify that the current device supports flash and flash power for variable photo sequence frames. If so, each frame is updated to enable the flash at 100% power. The exposure compensation values that were previously set for each frame are still active.

```csharp
var varPhotoSeqController = m_mediaCapture.VideoDeviceController.VariablePhotoSequenceController;

if (varPhotoSeqController.FrameCapabilities.Flash.Supported &&
    varPhotoSeqController.FrameCapabilities.Flash.PowerSupported)
{
    for (int i = 0; i < varPhotoSeqController.DesiredFrameControllers.Count; i++)
    {
        varPhotoSeqController.DesiredFrameControllers[i].FlashControl.Mode = FrameFlashMode.Enable;
        varPhotoSeqController.DesiredFrameControllers[i].FlashControl.PowerPercent = 100;
    }
}
```

## Clean up the variable photo sequence capture

When you are done capturing variable photo sequences or your app is suspending, clean up the variable photo sequence object by calling [**FinishAsync**](/uwp/api/windows.media.capture.core.variablephotosequencecapture.finishasync). Unregister the object's event handlers and set it to null.

```csharp
await m_photoSequenceCapture.FinishAsync();
m_photoSequenceCapture.PhotoCaptured -= OnPhotoCaptured;
m_photoSequenceCapture.Stopped -= OnStopped;
m_photoSequenceCapture = null;
```

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
 

 
