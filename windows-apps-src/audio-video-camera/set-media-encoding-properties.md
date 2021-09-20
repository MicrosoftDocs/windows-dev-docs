---
ms.assetid: 09BA9250-A476-4803-910E-52F0A51704B1
description: This article shows you how to use the IMediaEncodingProperties interface to set the resolution and frame rate of the camera preview stream and captured photos and video.
title: Set format, resolution, and frame rate for MediaCapture
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Set format, resolution, and frame rate for MediaCapture



This article shows you how to use the [**IMediaEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.IMediaEncodingProperties) interface to set the resolution and frame rate of the camera preview stream and captured photos and video. It also shows how to ensure that the aspect ratio of the preview stream matches that of the captured media.

Camera profiles offer a more advanced way of discovering and setting the stream properties of the camera, but they are not supported for all devices. For more information, see [Camera profiles](camera-profiles.md).

The code in this article was adapted from the [CameraResolution sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraResolution). You can download the sample to see the code used in context or to use the sample as a starting point for your own app.

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. It is recommended that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

## A media encoding properties helper class

Creating a simple helper class to wrap the functionality of the [**IMediaEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.IMediaEncodingProperties) interface makes it easier to select a set of encoding properties that meet particular criteria. This helper class is particularly useful due to the following behavior of the encoding properties feature:

**Warning**  
The [**VideoDeviceController.GetAvailableMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getavailablemediastreamproperties) method takes a member of the [**MediaStreamType**](/uwp/api/Windows.Media.Capture.MediaStreamType) enumeration, such as **VideoRecord** or **Photo**, and returns a list of either [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) or [**VideoEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.VideoEncodingProperties) objects that convey the stream encoding settings, such as the resolution of the captured photo or video. The results of calling **GetAvailableMediaStreamProperties** may include **ImageEncodingProperties** or **VideoEncodingProperties** regardless of what **MediaStreamType** value is specified. For this reason, you should always check the type of each returned value and cast it to the appropriate type before attempting to access any of the property values.

The helper class defined below handles the type checking and casting for [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) or [**VideoEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.VideoEncodingProperties) so that your app code doesn't need to distinguish between the two types. In addition to this, the helper class exposes properties for the aspect ratio of the properties, the frame rate (for video encoding properties only), and a friendly name that makes it easier to display the encoding properties in the app's UI.

You must include the [**Windows.Media.MediaProperties**](/uwp/api/Windows.Media.MediaProperties) namespace in the source file for the helper class.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetMediaEncodingPropertiesUsing":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/StreamPropertiesHelper.cs" id="SnippetStreamPropertiesHelper":::

## Determine if the preview and capture streams are independent

On some devices, the same hardware pin is used for both preview and capture streams. On these devices, setting the encoding properties of one will also set the other. On devices that use different hardware pins for capture and preview, the properties can be set for each stream independently. Use the following code to determine if the preview and capture streams are independent. You should adjust your UI to enable or disable the setting of the streams independently based on the result of this test.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCheckIfStreamsAreIdentical":::

## Get a list of available stream properties

Get a list of the available stream properties for a capture device by getting the [**VideoDeviceController**](/uwp/api/Windows.Media.Devices.VideoDeviceController) for your app's [MediaCapture](./index.md) object and then calling [**GetAvailableMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getavailablemediastreamproperties) and passing in one of the [**MediaStreamType**](/uwp/api/Windows.Media.Capture.MediaStreamType) values, **VideoPreview**, **VideoRecord**, or **Photo**. In this example, Linq syntax is used to create a list of **StreamPropertiesHelper** objects, defined previously in this article, for each of the [**IMediaEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.IMediaEncodingProperties) values returned from **GetAvailableMediaStreamProperties**. This example first uses Linq extension methods to order the returned properties based first on resolution and then on frame rate.

If your app has specific resolution or frame rate requirements, you can select a set of media encoding properties programmatically. A typical camera app will instead expose the list of available properties in the UI and allow the user to select their desired settings. A **ComboBoxItem** is created for each item in the list of **StreamPropertiesHelper** objects in the list. The content is set to the friendly name returned by the helper class and the tag is set to the helper class itself so it can be used later to retrieve the associated encoding properties. Each **ComboBoxItem** is then added to the **ComboBox** passed into the method.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetPopulateStreamPropertiesUI":::

## Set the desired stream properties

Tell the video device controller to use your desired encoding properties by calling [**SetMediaStreamPropertiesAsync**](/uwp/api/windows.media.devices.videodevicecontroller.setmediastreampropertiesasync), passing in the **MediaStreamType** value indicating whether the photo, video, or preview properties should be set. This example sets the requested encoding properties when the user selects an item in one of the **ComboBox** objects populated with the **PopulateStreamPropertiesUI** helper method.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetPreviewSettingsChanged":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetPhotoSettingsChanged":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetVideoSettingsChanged":::

## Match the aspect ratio of the preview and capture streams

A typical camera app will provide UI for the user to select the video or photo capture resolution but will programmatically set the preview resolution. There are a few different strategies for selecting the best preview stream resolution for your app:

-   Select the highest available preview resolution, letting the UI framework perform any necessary scaling of the preview.

-   Select the preview resolution closest to the capture resolution so that the preview displays the closest representation to the final captured media.

-   Select the preview resolution closest to the size of the [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) so that no more pixels than necessary are going through the preview stream pipeline.

**Important**  
It is possible, on some devices, to set a different aspect ratio for the camera's preview stream and capture stream. Frame cropping caused by this mismatch can result in content being present in the captured media that was not visible in the preview which can result in a negative user experience. It is strongly recommended that you use the same aspect ratio, within a small tolerance window, for the preview and capture streams. It is fine to have entirely different resolutions enabled for capture and preview as long as the aspect ratio match closely.


To ensure that the photo or video capture streams match the aspect ratio of the preview stream, this example calls [**VideoDeviceController.GetMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getmediastreamproperties) and passes in the **VideoPreview** enum value to request the current stream properties for the preview stream. Next a small aspect ratio tolerance window is defined so that we can include aspect ratios that are not exactly the same as the preview stream, as long as they are close. Next, a Linq extension method is used to select just the **StreamPropertiesHelper** objects where the aspect ratio is within the defined tolerance range of the preview stream.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetMatchPreviewAspectRatio":::

 

 
