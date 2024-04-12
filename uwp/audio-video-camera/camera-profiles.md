---
ms.assetid: 42A06423-670F-4CCC-88B7-3DCEEDDEBA57
description: This article discusses how to use camera profiles to discover and manage the capabilities of different video capture devices. This includes tasks such as selecting profiles that support specific resolutions or frame rates, profiles that support simultaneous access to multiple cameras, and profiles that support HDR.
title: Discover and select camera capabilities with camera profiles
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Discover and select camera capabilities with camera profiles



This article discusses how to use camera profiles to discover and manage the capabilities of different video capture devices. This includes tasks such as selecting profiles that support specific resolutions or frame rates, profiles that support simultaneous access to multiple cameras, and profiles that support HDR.

> [!NOTE] 
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. It is recommended that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

 

## About camera profiles

Cameras on different devices support different capabilities including the set of supported capture resolutions, frame rate for video captures, and whether HDR or variable frame rate captures are supported. The Universal Windows Platform (UWP) media capture framework stores this set of capabilities in a [**MediaCaptureVideoProfileMediaDescription**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfileMediaDescription). A camera profile, represented by a [**MediaCaptureVideoProfile**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfile) object, has three collections of media descriptions; one for photo capture, one for video capture, and another for video preview.

Before initializing your [MediaCapture](./index.md) object, you can query the capture devices on the current device to see what profiles are supported. When you select a supported profile, you know that the capture device supports all of the capabilities in the profile's media descriptions. This eliminates the need for a trial and error approach to determining which combinations of capabilities are supported on a particular device.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetBasicInitExample":::

The code examples in this article replace this minimal initialization with the discovery of camera profiles supporting various capabilities, which are then used to initialize the media capture device.

## Find a video device that supports camera profiles

Before searching for supported camera profiles, you should find a capture device that supports the use of camera profiles. The **GetVideoProfileSupportedDeviceIdAsync** helper method defined in the example below uses the [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) method to retrieve a list of all available video capture devices. It loops through all of the devices in the list, calling the static method, [**IsVideoProfileSupported**](/uwp/api/windows.media.capture.mediacapture.isvideoprofilesupported), for each device to see if it supports video profiles. Also, the [**EnclosureLocation.Panel**](/uwp/api/windows.devices.enumeration.enclosurelocation.panel) property for each device, allowing you to specify whether you want a camera on the front or back of the device.

If a device that supports camera profiles is found on the specified panel, the [**Id**](/uwp/api/windows.devices.enumeration.deviceinformation.id) value, containing the device's ID string, is returned.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetVideoProfileSupportedDeviceIdAsync":::

If the device ID returned from the **GetVideoProfileSupportedDeviceIdAsync** helper method is null or an empty string, there is no device on the specified panel that supports camera profiles. In this case, you should initialize your media capture device without using profiles.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetDeviceWithProfileSupport":::

## Select a profile based on supported resolution and frame rate

To select a profile with particular capabilities, such as with the ability to achieve a particular resolution and frame rate, you should first call the helper method defined above to get the ID of a capture device that supports using camera profiles.

Create a new [**MediaCaptureInitializationSettings**](/uwp/api/Windows.Media.Capture.MediaCaptureInitializationSettings) object, passing in the selected device ID. Next, call the static method [**MediaCapture.FindAllVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findallvideoprofiles) to get a list of all camera profiles supported by the device.

This example uses a Linq query method, included in the using **System.Linq** namespace, to select a profile that contains a [**SupportedRecordMediaDescription**](/uwp/api/windows.media.capture.mediacapturevideoprofile.supportedrecordmediadescription) object where the [**Width**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.width), [**Height**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.height), and [**FrameRate**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.framerate) properties match the requested values. If a match is found, [**VideoProfile**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.videoprofile) and [**RecordMediaDescription**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.recordmediadescription) of the **MediaCaptureInitializationSettings** are set to the values from the anonymous type returned from the Linq query. If no match is found, the default profile is used.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetFindWVGA30FPSProfile":::

After you populate the **MediaCaptureInitializationSettings** with your desired camera profile, you simply call [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync) on your media capture object to configure it to the desired profile.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetInitCaptureWithProfile":::

## Use media frame source groups to get profiles

Starting with Windows 10, version 1803, you can use the [**MediaFrameSourceGroup**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup) class to get camera profiles with specific capabilities before initializing the **MediaCapture** object. Frame source groups allow device manufacturers to represent groups of sensors or capture capabilities as a single virtual device. This enables computational photography scenarios such as using depth and color cameras together, but can also be used to select camera profiles for simple capture scenarios. For more information on using **MediaFrameSourceGroup**, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md).

The example method below shows how to use **MediaFrameSourceGroup** objects to find a camera profile that supports a known video profile, such as one that supports HDR or variable photo sequence. First, call [**MediaFrameSourceGroup.FindAllAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.findallasync) to get a list of all media frame source groups available on the current device. Loop through each source group and call [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) to get a list of all of the video profiles for the current source group that support the specified profile, in this case HDR with WCG photo. If a profile that meets the criteria is found, create a new **MediaCaptureInitializationSettings** object and set the **VideoProfile** to the select profile and the **VideoDeviceId** to the **Id** property of the current media frame source group. So, for example, you could pass the value **KnownVideoProfile.HdrWithWcgVideo** into this method to get media capture settings that support HDR video. Pass **KnownVideoProfile.VariablePhotoSequence** to get settings that support variable photo sequence.

 :::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetFindKnownVideoProfile":::

## Use known profiles to find a profile that supports HDR video (legacy technique)

> [!NOTE] 
> The APIs described in this section are deprecated starting with Windows 10, version 1803. See the previous section, **Use media frame source groups to get profiles**.

Selecting a profile that supports HDR begins like the other scenarios. Create a **MediaCaptureInitializationSettings** and a string to hold the capture device ID. Add a boolean variable that will track whether HDR video is supported.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetHdrProfileSetup":::

Use the **GetVideoProfileSupportedDeviceIdAsync** helper method defined above to get the device ID for a capture device that supports camera profiles.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetFindDeviceHDR":::

The static method [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) returns the camera profiles supported by the specified device that is categorized by the specified [**KnownVideoProfile**](/uwp/api/Windows.Media.Capture.KnownVideoProfile) value. For this scenario, the **VideoRecording** value is specified to limit the returned camera profiles to ones that support video recording.

Loop through the returned list of camera profiles. For each camera profile, loop through each [**VideoProfileMediaDescription**](/uwp/api/Windows.Media.Capture.MediaCaptureVideoProfileMediaDescription) in the profile checking to see if the [**IsHdrVideoSupported**](/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription.ishdrvideosupported) property is true. After a suitable media description is found, break out of the loop and assign the profile and description objects to the **MediaCaptureInitializationSettings** object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetFindHDRProfile":::

## Determine if a device supports simultaneous photo and video capture

Many devices support capturing photos and video simultaneously. To determine if a capture device supports this, call [**MediaCapture.FindAllVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findallvideoprofiles) to get all of the camera profiles supported by the device. Use a link query to find a profile that has at least one entry for both [**SupportedPhotoMediaDescription**](/uwp/api/windows.media.capture.mediacapturevideoprofile.supportedphotomediadescription) and [**SupportedRecordMediaDescription**](/uwp/api/windows.media.capture.mediacapturevideoprofile.supportedrecordmediadescription) which means that the profile supports simultaneous capture.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetGetPhotoAndVideoSupport":::

You can refine this query to look for profiles that support specific resolutions or other capabilities in addition to simultaneous video record. You can also use the [**MediaCapture.FindKnownVideoProfiles**](/uwp/api/windows.media.capture.mediacapture.findknownvideoprofiles) and specify the [**BalancedVideoAndPhoto**](/uwp/api/Windows.Media.Capture.KnownVideoProfile) value to retrieve profiles that support simultaneous capture, but querying all profiles will provide more complete results.

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
 

 
