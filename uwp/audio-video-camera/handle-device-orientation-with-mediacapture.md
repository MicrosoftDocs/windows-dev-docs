---
ms.assetid: af3941c0-3508-4ba2-a79e-fc71657c605f
description: This article shows you how to handle device orientation when capturing photos and videos by using a helper class.
title: Handle device orientation with MediaCapture
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Handle device orientation with MediaCapture
When your app captures a photo or video that is intended to be viewed outside of your app, such as saving to a file on the user's device or sharing online, it's important that you encode the image with the proper orientation metadata so that when another app or device displays the image, it is oriented correctly. Determining the correct orientation data to include in a media file can be a complex task because there are several variables to consider, including the orientation of the device chassis, the orientation of the display, and the placement of the camera on the chassis (whether it is a front or back-facing camera). 

To simplify the process of handling orientation, we recommend using a helper class, **CameraRotationHelper**, for which the full definition is provided at the end of this article. You can add this class to your project and then follow the steps in this article to add orientation support to your camera app. The helper class also makes it easier for you to rotate the controls in your camera UI so that they are rendered correctly from the user's point of view.

> [!NOTE] 
> This article builds on the code and concepts discussed in the article [**Basic photo, video, and audio capture with MediaCapture**](basic-photo-video-and-audio-capture-with-mediacapture.md). We recommend that you familiarize yourself with the basic concepts of using the **MediaCapture** class before adding orientation support to your app.

## Namespaces used in this article
The example code in this article uses APIs from the following namespaces that you should include in your code. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetOrientationUsing":::

The first step in adding orientation support to your app is to lock the display so that it doesn't automatically rotate when the device is rotated. Automatic UI rotation works well for most types of apps, but it is unintuitive for users when the camera preview rotates. Lock the display orientation by setting the [**DisplayInformation.AutoRotationPreferences**](/uwp/api/windows.graphics.display.displayinformation.autorotationpreferences) property to [**DisplayOrientations.Landscape**](/uwp/api/Windows.Graphics.Display.DisplayOrientations). 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetAutoRotationPreference":::

## Tracking the camera device location
To calculate the correct orientation for captured media, you app must determine the location of the camera device on the chassis. Add a boolean member variable to track whether the camera is external to the device, such as a USB web cam. Add another boolean variable to track whether the preview should be mirrored, which is the case if a front-facing camera is used. Also, add a variable for storing a **DeviceInformation** object that represents the selected camera.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetCameraDeviceLocationBools":::
:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetDeclareCameraDevice":::

## Select a camera device and initialize the MediaCapture object
The article [**Basic photo, video, and audio capture with MediaCapture**](basic-photo-video-and-audio-capture-with-mediacapture.md) shows you how to initialize the **MediaCapture** object with just a couple of lines of code. To support camera orientation, we will add a few more steps to the initialization process.

First, call [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) passing in the device selector [**DeviceClass.VideoCapture**](/uwp/api/Windows.Devices.Enumeration.DeviceClass) to get a list of all available video capture devices. Next, select the first device in the list where the panel location of the camera is known and where it matches the supplied value, which in this example is a front-facing camera. If no camera is found on the desired panel, the first or default available camera is used.

If a camera device is found, a new [**MediaCaptureInitializationSettings**](/uwp/api/Windows.Media.Capture.MediaCaptureInitializationSettings) object is created and the [**VideoDeviceId**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.videodeviceid) property is set to the selected device. Next, create the MediaCapture object and call [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync), passing in the settings object to tell the system to use the selected camera.

Finally, check to see if the selected device panel is null or unknown. If so, the camera is external, which means that its rotation is unrelated to the rotation of the device. If the panel is known and is on the front of the device chassis, we know the preview should be mirrored, so the variable tracking this is set.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetInitMediaCaptureWithOrientation":::
## Initialize the CameraRotationHelper class

Now we begin using the **CameraRotationHelper** class. Declare a class member variable to store the object. Call the constructor, passing in the enclosure location of the selected camera. The helper class uses this information to calculate the correct orientation for captured media, the preview stream, and the UI. Register a handler for the helper class's **OrientationChanged** event, which will be raised when we need to update the orientation of the UI or the preview stream.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetDeclareRotationHelper":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetInitRotationHelper":::

## Add orientation data to the camera preview stream
Adding the correct orientation to the metadata of the preview stream does not affect how the preview appears to the user, but it helps the system encode any frames captured from the preview stream correctly.

You start the camera preview by calling [**MediaCapture.StartPreviewAsync**](/uwp/api/windows.media.capture.mediacapture.startpreviewasync). Before you do this, check the member variable to see if the preview should be mirrored (for a front-facing camera). If so, set the [**FlowDirection**](/uwp/api/windows.ui.xaml.frameworkelement.flowdirection) property of the [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement), named *PreviewControl* in this example, to [**FlowDirection.RightToLeft**](/uwp/api/Windows.UI.Xaml.FlowDirection). After starting the preview, call the helper method **SetPreviewRotationAsync** to set the preview rotation. Following is the implementation of this method.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetStartPreviewWithRotationAsync":::

We set the preview rotation in a separate method so that it can be updated when the phone orientation changes without reinitializing the preview stream. If the camera is external to the device, no action is taken. Otherwise, the **CameraRotationHelper** method **GetCameraPreviewOrientation** is called and returns the proper orientation for the preview stream. 

To set the metadata, the preview stream properties are retrieved by calling [**VideoDeviceController.GetMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getmediastreamproperties). Next, create the GUID representing the Media Foundation Transform (MFT) attribute for video stream rotation. In C++ you can use the constant [**MF_MT_VIDEO_ROTATION**](/windows/desktop/medfound/mf-mt-video-rotation), but in C# you must manually specify the GUID value. 

Add a property value to the stream properties object, specifying the GUID as the key and the preview rotation as the value. This property expects values to be in units of counterclockwise degrees, so the **CameraRotationHelper** method **ConvertSimpleOrientationToClockwiseDegrees** is used to convert the simple orientation value. Finally, call [**SetEncodingPropertiesAsync**](/uwp/api/Windows.Media.Capture.MediaCapture#Windows_Media_Capture_MediaCapture_SetEncodingPropertiesAsync_Windows_Media_Capture_MediaStreamType_Windows_Media_MediaProperties_IMediaEncodingProperties_Windows_Media_MediaProperties_MediaPropertySet_) to apply the new rotation property to the stream.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetSetPreviewRotationAsync":::

Next, add the handler for the **CameraRotationHelper.OrientationChanged** event. This event passes in an argument that lets you know whether the preview stream needs to be rotated. If the orientation of the device was changed to face up or face down, this value will be false. If the preview does need to be rotated, call **SetPreviewRotationAsync** which was defined previously.

Next, in the **OrientationChanged** event handler, update your UI if needed. Get the current recommended UI orientation from the helper class by calling **GetUIOrientation** and convert the value to clockwise degrees, which is used for XAML transforms. Create a [**RotateTransform**](/uwp/api/Windows.UI.Xaml.Media.RotateTransform) from the orientation value and set the [**RenderTransform**](/uwp/api/windows.ui.xaml.uielement.rendertransform) property of your XAML controls. Depending on your UI layout, you may need to make additional adjustments here in addition to simply rotating the controls. Also, remember that all updates to your UI must be made on the UI thread, so you should place this code inside a call to [**RunAsync**](/uwp/api/Windows.UI.Core.CoreDispatcher#Windows_UI_Core_CoreDispatcher_RunAsync_Windows_UI_Core_CoreDispatcherPriority_Windows_UI_Core_DispatchedHandler_).  

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetHelperOrientationChanged":::

## Capture a photo with orientation data
The article [**Basic photo, video, and audio capture with MediaCapture**](basic-photo-video-and-audio-capture-with-mediacapture.md) shows you how to capture a photo to a file by capturing first to an in-memory stream and then using a decoder to read the image data from the stream and an encoder to transcode the image data to a file. Orientation data, obtained from the **CameraRotationHelper** class, can be added to the image file during the transcoding operation.

In the following example, a photo is captured to an [**InMemoryRandomAccessStream**](/uwp/api/Windows.Storage.Streams.InMemoryRandomAccessStream) with a call to [**CapturePhotoToStreamAsync**](/uwp/api/windows.media.capture.mediacapture.capturephototostreamasync) and a [**BitmapDecoder**](/uwp/api/Windows.Graphics.Imaging.BitmapDecoder) is created from the stream. Next a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) is created and opened to retrieve an [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream) for writing to the file. 

Before transcoding the file, the photo's orientation is retrieved from the helper class method **GetCameraCaptureOrientation**. This method returns a [**SimpleOrientation**](/uwp/api/Windows.Devices.Sensors.SimpleOrientation) object which is converted to a [**PhotoOrientation**](/uwp/api/Windows.Storage.FileProperties.PhotoOrientation) object with the helper method **ConvertSimpleOrientationToPhotoOrientation**. Next, a new **BitmapPropertySet** object is created and a property is added where the key is "System.Photo.Orientation" and the value is the photo orientation, expressed as a **BitmapTypedValue**. "System.Photo.Orientation" is one of many Windows properties that can be added as metadata to an image file. For a list of all of the photo-related properties, see [**Windows Properties - Photo**](/previous-versions/ff516600(v=vs.85)). For more information about working with metadata in images, see [**Image metadata**](image-metadata.md).

Finally, the property set which includes the orientation data is set for the encoder by with a call to [**SetPropertiesAsync**](../develop/index.md) and the image is transcoded with a call to [**FlushAsync**](/uwp/api/windows.graphics.imaging.bitmapencoder.flushasync).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetCapturePhotoWithOrientation":::

## Capture a video with orientation data
Basic video capture is described in the article [**Basic photo, video, and audio capture with MediaCapture**](basic-photo-video-and-audio-capture-with-mediacapture.md). Adding orientation data to the encoding of the captured video is done using the same technique as described earlier in the section about adding orientation data to the preview stream.

In the following example, a file is created to which the captured video will be written. An MP4 encoding profile is create using the static method [**CreateMp4**](/uwp/api/windows.media.mediaproperties.mediaencodingprofile.createmp4). The proper orientation for the video is obtained from the **CameraRotationHelper** class with a call to **GetCameraCaptureOrientation** Because the rotation property requires the orientation to be expressed in counterclockwise degrees, the **ConvertSimpleOrientationToClockwiseDegrees** helper method is called to convert the orientation value. Next, create the GUID representing the Media Foundation Transform (MFT) attribute for video stream rotation. In C++ you can use the constant [**MF_MT_VIDEO_ROTATION**](/windows/desktop/medfound/mf-mt-video-rotation), but in C# you must manually specify the GUID value. Add a property value to the stream properties object, specifying the GUID as the key and the rotation as the value. Finally call [**StartRecordToStorageFileAsync**](/uwp/api/Windows.Media.Capture.MediaCapture#Windows_Media_Capture_MediaCapture_StartRecordToStorageFileAsync_Windows_Media_MediaProperties_MediaEncodingProfile_Windows_Storage_IStorageFile_) to begin recording video encoded with orientation data.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/MainPage.xaml.cs" id="SnippetStartRecordingWithOrientationAsync":::

## CameraRotationHelper full code listing
The following code snippet lists the full code for the **CameraRotationHelper** class that manages the hardware orientation sensors, calculates the proper orientation values for photos and videos, and provides helper methods to convert between the different representations of orientation that are used by different Windows features. If you follow the guidance shown in the article above, you can add this class to your project as-is without having to make any changes. Of course, you can feel free to customize the following code to meet the needs of your particular scenario.

This helper class uses the device's [**SimpleOrientationSensor**](/uwp/api/Windows.Devices.Sensors.SimpleOrientationSensor) to determine the current orientation of the device chassis and the [**DisplayInformation**](/uwp/api/Windows.Graphics.Display.DisplayInformation) class to determine the current orientation of the display. Each of these classes provide events that are raised when the current orientation changes. The panel on which the capture device is mounted - front-facing, back-facing, or external - is used to determine whether the preview stream should be mirrored. Also, the [**EnclosureLocation.RotationAngleInDegreesClockwise**](/uwp/api/windows.devices.enumeration.enclosurelocation.rotationangleindegreesclockwise) property, supported by some devices, is used to determine the orientation in which the camera is mounted on the chassis.

The following methods can be used to get recommended orientation values for the specified camera app tasks:

* **GetUIOrientation** - Returns the suggested orientation for camera UI elements.
* **GetCameraCaptureOrientation** - Returns the suggested orientation for encoding into image metadata.
* **GetCameraPreviewOrientation** - Returns the suggested orientation for the preview stream to provide a natural user experience.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/SimpleCameraPreview_Win10/cs/CameraRotationHelper.cs" id="SnippetCameraRotationHelperFull":::



## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
 

 
