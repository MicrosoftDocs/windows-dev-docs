---
description: Code that integrates with the device itself and its sensors involves input from, and output to, the user.
title: Porting Windows Runtime 8.x to UWP for I/O, device, and app model'
ms.assetid: bb13fb8f-bdec-46f5-8640-57fb0dd2d85b
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Porting Windows Runtime 8.x to UWP for I/O, device, and app model




The previous topic was [Porting XAML and UI](w8x-to-uwp-porting-xaml-and-ui.md).

Code that integrates with the device itself and its sensors involves input from, and output to, the user. It can also involve processing data. But, this code is not generally thought of as either the UI layer *or* the data layer. This code includes integration with the vibration controller, accelerometer, gyroscope, microphone and speaker (which intersect with speech recognition and synthesis), (geo)location, and input modalities such as touch, mouse, keyboard, and pen.

## Application lifecycle (process lifetime management)


For a Universal 8.1 app, there is a two-second "debounce window" of time between the app becoming inactive and the system raising the suspending event. Using this debounce window as extra time to suspend state is unsafe, and for a Universal Windows Platform (UWP) app, there is no debounce window at all; the suspension event is raised as soon as an app becomes inactive.

For more info, see [App lifecycle](../launch-resume/app-lifecycle.md).

## Background audio


For the [**MediaElement.AudioCategory**](/uwp/api/windows.ui.xaml.controls.mediaelement.audiocategory) property, **ForegroundOnlyMedia** and **BackgroundCapableMedia** are deprecated for Windows 10 apps. Use the Windows Phone Store app model instead. For more information, see [Background Audio](../audio-video-camera/background-audio.md).

## Detecting the platform your app is running on


The way of thinking about app-targeting changes with Windows 10. The new conceptual model is that an app targets the Universal Windows Platform (UWP) and runs across all Windows devices. It can then opt to light up features that are exclusive to particular device families. If needed, the app also has the option to limit itself to targeting one or more device families specifically. For more info on what device families are—and how to decide which device family to target—see [Guide to UWP apps](../get-started/universal-application-platform-guide.md).

If you have code in your Universal 8.1 app that detects what operating system it is running on, then you may need to change that depending on the reason for the logic. If the app is passing the value through, and not acting on it, then you may want to continue to collect the operating system info.

**Note**   We recommend that you not use operating system or device family to detect the presence of features. Identifying the current operating system or device family is usually not the best way to determine whether a particular operating system or device family feature is present. Rather than detecting the operating system or device family (and version number), test for the presence of the feature itself (see [Conditional compilation, and adaptive code](w8x-to-uwp-porting-to-a-uwp-project.md)). If you must require a particular operating system or device family, be sure to use it as a minimum supported version, rather than design the test for that one version.

 

To tailor your app's UI to different devices, there are several techniques that we recommend. Continue to use auto-sized elements and dynamic layout panels as you always have. In your XAML markup, continue to use sizes in effective pixels (formerly view pixels) so that your UI adapts to different resolutions and scale factors (see [Effective pixels, viewing distance, and scale factors](w8x-to-uwp-porting-xaml-and-ui.md).). And use Visual State Manager's adaptive triggers and setters to adapt your UI to the window size (see [Guide to UWP apps](../get-started/universal-application-platform-guide.md).).

However, if you have a scenario where it is unavoidable to detect the device family, then you can do that. In this example, we use the [**AnalyticsVersionInfo**](/uwp/api/Windows.System.Profile.AnalyticsVersionInfo) class to navigate to a page tailored for the mobile device family where appropriate, and we make sure to fall back to a default page otherwise.

```csharp
   if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
        rootFrame.Navigate(typeof(MainPageMobile), e.Arguments);
    else
        rootFrame.Navigate(typeof(MainPage), e.Arguments);
```

Your app can also determine the device family that it is running on from the resource selection factors that are in effect. The example below shows how to do this imperatively, and the [**ResourceContext.QualifierValues**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.qualifiervalues) topic describes the more typical use case for the class in loading device family-specific resources based on the device family factor.

```csharp
var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
string deviceFamilyName;
bool isDeviceFamilyNameKnown = qualifiers.TryGetValue("DeviceFamily", out deviceFamilyName);
```

Also, see [Conditional compilation, and adaptive code](w8x-to-uwp-porting-to-a-uwp-project.md).

## Location


When an app that declares the Location capability in its app package manifest runs on Windows 10, the system will prompt the end-user for consent. This is true whether the app is a Windows Phone Store app or a Windows 10 app. So, if your app displays its own custom consent prompt, or if it provides an on-off toggle, then you will want to remove that so that the end-user is only prompted once.

 

 