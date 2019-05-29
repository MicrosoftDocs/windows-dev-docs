---
ms.assetid: E0189423-1DF3-4052-AB2E-846EA18254C4
description: This topic shows you how to apply effects to the camera preview and recording video streams and shows you how to use the video stabilization effect.
title: Effects for video capture
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Effects for video capture


This topic shows you how to apply effects to the camera preview and recording video streams and shows you how to use the video stabilization effect.

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

## Adding and removing effects from the camera video stream
To capture or preview video from the device's camera, you use the [**MediaCapture**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaCapture) object as described in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md). After you have initialized the **MediaCapture** object, you can add one or more video effects to the preview or capture stream by calling [**AddVideoEffectAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.addvideoeffectasync), passing in an [**IVideoEffectDefinition**](https://docs.microsoft.com/uwp/api/Windows.Media.Effects.IVideoEffectDefinition) object representing the effect to be added, and a member of the [**MediaStreamType**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaStreamType) enumeration indicating whether the effect should be added to the camera's preview stream or the record stream.

> [!NOTE]
> On some devices, the preview stream and the capture stream are the same, which means that if you specify **MediaStreamType.VideoPreview** or **MediaStreamType.VideoRecord** when you call **AddVideoEffectAsync**, the effect will be applied to both preview and record streams. You can determine whether the preview and record streams are the same on the current device by checking the [**VideoDeviceCharacteristic**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapturesettings.videodevicecharacteristic) property of the [**MediaCaptureSettings**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.mediacapturesettings) for the **MediaCapture** object. If the value of this property is **VideoDeviceCharacteristic.AllStreamsIdentical** or **VideoDeviceCharacteristic.PreviewRecordStreamsIdentical**, then the streams are the same and any effect you apply to one will affect the other.

The following example adds an effect to both the camera preview and record streams. This example illustrates checking to see if the record and preview streams are the same.

[!code-cs[BasicAddEffect](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetBasicAddEffect)]

Note that **AddVideoEffectAsync** returns an object that implements [**IMediaExtension**](https://docs.microsoft.com/uwp/api/Windows.Media.IMediaExtension) that represents the added video effect. Some effects allow you to change the effect settings by passing a [**PropertySet**](https://docs.microsoft.com/uwp/api/Windows.Foundation.Collections.PropertySet) into the [**SetProperties**](https://docs.microsoft.com/uwp/api/windows.media.imediaextension.setproperties) method.

Starting with Windows 10, version 1607, you can also use the object returned by **AddVideoEffectAsync** to remove the effect from the video pipeline by passing it into [**RemoveEffectAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.removeeffectasync). **RemoveEffectAsync** automatically determines whether the effect object parameter was added to the preview or record stream, so you don't need to specify the stream type when making the call.

[!code-cs[RemoveOneEffect](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetRemoveOneEffect)]

You can also remove all effects from the preview or capture stream by calling [**ClearEffectsAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.cleareffectsasync) and specifying the stream for which all effects should be removed.

[!code-cs[ClearAllEffects](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetClearAllEffects)]

## Video stabilization effect

The video stabilization effect manipulates the frames of a video stream to minimize shaking caused by holding the capture device in your hand. Because this technique causes the pixels to be shifted right, left, up, and down, and because the effect can't know what the content just outside the video frame is, the stabilized video is cropped slightly from the original video. A utility function is provided to allow you to adjust your video encoding settings to optimally manage the cropping performed by the effect.

On devices that support it, Optical Image Stabilization (OIS) stabilizes video by mechanically manipulating the capture device and, therefore, does not need to crop the edges of the video frames. For more information, see [Capture device controls for video capture](capture-device-controls-for-video-capture.md).

### Set up your app to use video stabilization

In addition to the namespaces required for basic media capture, using the video stabilization effect requires the following namespace.

[!code-cs[VideoStabilizationEffectUsing](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetVideoStabilizationEffectUsing)]

Declare a member variable to store the [**VideoStabilizationEffect**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.VideoStabilizationEffect) object. As part of the effect implementation, you will modify the encoding properties that you use to encode the captured video. Declare two variables to store a backup copy of the initial input and output encoding properties so that you can restore them later when the effect is disabled. Finally, declare a member variable of type [**MediaEncodingProfile**](https://docs.microsoft.com/uwp/api/Windows.Media.MediaProperties.MediaEncodingProfile) because this object will be accessed from multiple locations within your code.

[!code-cs[DeclareVideoStabilizationEffect](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetDeclareVideoStabilizationEffect)]

For this scenario, you should assign the media encoding profile object to a member variable so that you can access it later.

[!code-cs[EncodingProfileMember](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetEncodingProfileMember)]

### Initialize the video stabilization effect

After your **MediaCapture** object has been initialized, create a new instance of the [**VideoStabilizationEffectDefinition**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.VideoStabilizationEffectDefinition) object. Call [**MediaCapture.AddVideoEffectAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.addvideoeffectasync) to add the effect to the video pipeline and retrieve an instance of the [**VideoStabilizationEffect**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.VideoStabilizationEffect) class. Specify [**MediaStreamType.VideoRecord**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.MediaStreamType) to indicate that the effect should be applied to the video record stream.

Register an event handler for the [**EnabledChanged**](https://docs.microsoft.com/uwp/api/windows.media.core.videostabilizationeffect.enabledchanged) event and call the helper method **SetUpVideoStabilizationRecommendationAsync**, both of which are discussed later in this article. Finally, set the [**Enabled**](https://docs.microsoft.com/uwp/api/windows.media.core.videostabilizationeffect.enabled) property of the effect to true to enable the effect.

[!code-cs[CreateVideoStabilizationEffect](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetCreateVideoStabilizationEffect)]

### Use recommended encoding properties

As discussed earlier in this article, the technique that the video stabilization effect uses necessarily causes the stabilized video to be cropped slightly from the source video. Define the following helper function in your code in order to adjust the video encoding properties to optimally handle this limitation of the effect. This step is not required in order to use the video stabilization effect, but if you don't perform this step, the resulting video will be upscaled slightly and therefore have slightly lower visual fidelity.

Call [**GetRecommendedStreamConfiguration**](https://docs.microsoft.com/uwp/api/windows.media.core.videostabilizationeffect.getrecommendedstreamconfiguration) on your video stabilization effect instance, passing in the [**VideoDeviceController**](https://docs.microsoft.com/uwp/api/Windows.Media.Devices.VideoDeviceController) object, which informs the effect about your current input stream encoding properties, and your **MediaEncodingProfile** which lets the effect know your current output encoding properties. This method returns a [**VideoStreamConfiguration**](https://docs.microsoft.com/uwp/api/Windows.Media.Capture.VideoStreamConfiguration) object containing new recommended input and output stream encoding properties.

The recommended input encoding properties are, if it is supported by the device, a higher resolution that the initial settings you provided so that there is minimal loss in resolution after the effect's cropping is applied.

Call [**VideoDeviceController.SetMediaStreamPropertiesAsync**](https://docs.microsoft.com/uwp/api/windows.media.devices.videodevicecontroller.setmediastreampropertiesasync) to set the new encoding properties. Before setting the new properties, use the member variable to store the initial encoding properties so that you can change the settings back when you disable the effect.

If the video stabilization effect must crop the output video, the recommended output encoding properties will be the size of the cropped video. This means that the output resolution will match the cropped video size. If you do not use the recommended output properties, the video will be scaled up to match the initial output size, which will result in a loss of visual fidelity.

Set the [**Video**](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile.video) property of the **MediaEncodingProfile** object. Before setting the new properties, use the member variable to store the initial encoding properties so that you can change the settings back when you disable the effect.

[!code-cs[SetUpVideoStabilizationRecommendationAsync](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetSetUpVideoStabilizationRecommendationAsync)]

### Handle the video stabilization effect being disabled

The system may automatically disable the video stabilization effect if the pixel throughput is too high for the effect to handle or if it detects that the effect is running slowly. If this occurs, the EnabledChanged event is raised. The **VideoStabilizationEffect** instance in the *sender* parameter indicates the new state of the effect, enabled or disabled. The [**VideoStabilizationEffectEnabledChangedEventArgs**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.VideoStabilizationEffectEnabledChangedEventArgs) has a [**VideoStabilizationEffectEnabledChangedReason**](https://docs.microsoft.com/uwp/api/Windows.Media.Core.VideoStabilizationEffectEnabledChangedReason) value indicating why the effect was enabled or disabled. Note that this event is also raised if you programmatically enable or disable the effect, in which case the reason will be **Programmatic**.

Typically, you would use this event to adjust your app's UI to indicate the current status of video stabilization.

[!code-cs[VideoStabilizationEnabledChanged](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetVideoStabilizationEnabledChanged)]

### Clean up the video stabilization effect

To clean up the video stabilization effect, call [**RemoveEffectAsync**](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapture.removeeffectasync) to remove the effect from the video pipeline. If the member variables containing the initial encoding properties are not null, use them to restore the encoding properties. Finally, remove the **EnabledChanged** event handler and set the effect to null.

[!code-cs[CleanUpVisualStabilizationEffect](./code/SimpleCameraPreview_Win10/cs/MainPage.Effects.xaml.cs#SnippetCleanUpVisualStabilizationEffect)]

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
 

 




