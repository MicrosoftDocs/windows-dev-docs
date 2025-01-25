---
description: Learn how to use camera profiles to support specific resolutions, frame rates, simultaneous access to multiple cameras, and and HDR in a WinUI 3 app.
title: Discover and select camera capabilities with camera profiles
ms.date: 08/13/2024
ms.topic: article
keywords: windows 10, windows 11, winui3, camera
ms.localizationpriority: medium
#customer intent: As a developer, I want to use camera profiles to select camera capabilities with MediaCapture in a Windows app using WinUI 3.
---


# Discover and select camera capabilities with camera profiles in a WinUI 3 app

This article shows how to use camera profiles to discover and manage the capabilities of different video capture devices. This includes tasks such as selecting profiles that support specific resolutions or frame rates, profiles that support simultaneous access to multiple cameras, and profiles that support HDR.

## About camera profiles

Cameras on different devices support different capabilities including the set of supported capture resolutions, frame rate for video captures, and whether HDR or variable frame rate captures are supported. A set of supported capabilities is defined in a [**MediaCaptureVideoProfileMediaDescription**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfileMediaDescription) object. A camera profile, represented by a [**MediaCaptureVideoProfile**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfile) object, has three collections of media descriptions; one for photo capture, one for video capture, and another for video preview.

Before initializing your [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object, you can query the capture devices on the current device to see what profiles are supported. When you select a supported profile, you know that the capture device supports all of the capabilities in the profile's media descriptions. This eliminates the need for a trial and error approach to determining which combinations of capabilities are supported on a particular device.

## Find a camera that supports camera profiles

To use camera profiles, you must first check for a camera device that supports the use of camera profiles. The example below shows how to use the [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) method to retrieve a list of all available video capture devices on either the front or back panel of the current device. It loops through all of the devices in the list, calling the static method, [**IsVideoProfileSupported**](/uwp/api/windows.media.capture.mediacapture.isvideoprofilesupported), for each device to see if it supports video profiles.

If a device that supports camera profiles is found on the specified panel, the [**Id**](/uwp/api/windows.devices.enumeration.deviceinformation.id) value, containing the device's ID string, is returned.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetGetVideoProfileSupportedDeviceIdAsync":::

If the device ID returned from the **GetVideoProfileSupportedDeviceIdAsync** helper method is null or an empty string, there is no device on the specified panel that supports camera profiles. In this case, you should initialize your media capture device without using profiles.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetGetDeviceWithProfileSupport":::

## Select a profile based on supported resolution and frame rate

To select a profile with particular capabilities, such as with the ability to achieve a particular resolution and frame rate, use the method shown previously in this article to get the ID of a capture device that supports using camera profiles.

Create a new [**MediaCaptureInitializationSettings**](/uwp/api/Windows.Media.Capture.MediaCaptureInitializationSettings) object, passing in the selected device ID. Call the static method [**MediaCapture.FindAllVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findallvideoprofiles) to get a list of all camera profiles supported by the device.

This example selects a profile that contains a [**SupportedRecordMediaDescription**](/uwp/api/windows.media.capture.mediacapturevideoprofile.supportedrecordmediadescription) object where the [**Width**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.width), [**Height**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.height), and [**FrameRate**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.framerate) properties match the requested values. If a match is found, [**VideoProfile**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.videoprofile) and [**RecordMediaDescription**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.recordmediadescription) of the **MediaCaptureInitializationSettings** are set to the values returned from the query. If no match is found, the default profile is used.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetFindWVGA30FPSProfile":::

After you populate the **MediaCaptureInitializationSettings** with your desired camera profile, you simply call [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync) on your media capture object to configure it to the desired profile.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetInitCaptureWithProfile":::


## Select devices with KnownVideoProfile

You can use the [**MediaFrameSourceGroup**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup) class to get camera profiles with specific capabilities before initializing the **MediaCapture** object. Frame source groups allow device manufacturers to represent groups of sensors or capture capabilities as a single virtual device. This enables computational photography scenarios such as using depth and color cameras together, but can also be used to select camera profiles for simple capture scenarios. For more information on using **MediaFrameSourceGroup**, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md).

The example below shows how to use **MediaFrameSourceGroup** to find a camera profile that supports that supports the desired scenario. Call [**MediaFrameSourceGroup.FindAllAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.findallasync) to get a list of all media frame source groups available on the current device. Loop through each source group and call [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) to get a list of all of the video profiles for the current source group that support the specified [KnownCameraProfile](/uwp/api/windows.media.capture.knownvideoprofile), in this example the value **KnownVideoProfile.HighQualityPhoto** is used. Other values include support for HDR and variable photo sequences, for example. If a profile that meets the requested criteria is found, create a new **MediaCaptureInitializationSettings** object and set the **VideoProfile** to the select profile and the **VideoDeviceId** to the **Id** property of the current media frame source group. Use this **MediaCaptureInitializationSettings** object to initialize the **MediaCapture** object.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetKnownVideoProfile":::


## Determine if a device supports simultaneous photo and video capture

Many devices support capturing photos and video simultaneously. To determine if a capture device supports this, call [**MediaCapture.FindAllVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findallvideoprofiles) to get all of the camera profiles supported by the device. Use a link query to find a profile that has at least one entry for both [**SupportedPhotoMediaDescription**](/uwp/api/windows.media.capture.mediacapturevideoprofile.supportedphotomediadescription) and [**SupportedRecordMediaDescription**](/uwp/api/windows.media.capture.mediacapturevideoprofile.supportedrecordmediadescription) which means that the profile supports simultaneous capture.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetSimultaneousPhotoAndVideo":::

You can refine this query to look for profiles that support specific resolutions or other capabilities in addition to simultaneous video record. You can also use the [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) and specify the [**BalancedVideoAndPhoto**](/uwp/api/Windows.Media.Capture.KnownVideoProfile) value to retrieve profiles that support simultaneous capture, but querying all profiles will provide more complete results.

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)