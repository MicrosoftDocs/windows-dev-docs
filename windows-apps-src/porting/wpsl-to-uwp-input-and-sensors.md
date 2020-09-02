---
description: Code that integrates with the device itself and its sensors involves input from, and output to, the user.
title: Porting Windows Phone Silverlight to UWP for I/O, device, and app model'
ms.assetid: bf9f2c03-12c1-49e4-934b-e3fa98919c53
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
#  Porting Windows Phone Silverlight to UWP for I/O, device, and app model


The previous topic was [Porting XAML and UI](wpsl-to-uwp-porting-xaml-and-ui.md).

Code that integrates with the device itself and its sensors involves input from, and output to, the user. It can also involve processing data. But, this code is not generally thought of as either the UI layer or the data layer. This code includes integration with the vibration controller, accelerometer, gyroscope, microphone and speaker (which intersect with speech recognition and synthesis), (geo)location, and input modalities such as touch, mouse, keyboard, and pen.

## Application lifecycle (process lifetime management)

Your Windows Phone Silverlight app contains code to save and restore its application state and its view state in order to support being tombstoned and subsequently re-activated. The app lifecycle of Universal Windows Platform (UWP) apps has strong parallels with that of Windows Phone Silverlight apps, since they're both designed with the same goal of maximizing the resources available to whichever app the user has chosen to have in the foreground at any moment. You'll find that your code will adapt to the new system reasonable easily.

**Note**   Pressing the hardware **Back** button automatically terminates a Windows Phone Silverlight app. Pressing the hardware **Back** button on a mobile device *does not* automatically terminate a UWP app. Instead, it becomes suspended, and then it may be terminated. But, those details are transparent to an app that responds appropriately to application lifecycle events.

A "debounce window" is the period of time between the app becoming inactive and the system raising the suspending event. For a UWP app, there is no debounce window; the suspension event is raised as soon as an app becomes inactive.

For more info, see [App lifecycle](../launch-resume/app-lifecycle.md).

## Camera

Windows Phone Silverlight camera capture code uses the **Microsoft.Devices.Camera**, **Microsoft.Devices.PhotoCamera**, or **Microsoft.Phone.Tasks.CameraCaptureTask** classes. To port that code to the Universal Windows Platform (UWP), you can use the [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) class. There is a code example in the [**CapturePhotoToStorageFileAsync**](/uwp/api/windows.media.capture.mediacapture.capturephototostoragefileasync) topic. That method allows you to capture a photo to a storage file, and it requires the **microphone** and **webcam** [**device capabilities**](/uwp/schemas/appxpackage/uapmanifestschema/element-devicecapability) to be set in the app package manifest.

Another option is the [**CameraCaptureUI**](/uwp/api/Windows.Media.Capture.CameraCaptureUI) class, which also requires the **microphone** and **webcam** [**device capabilities**](/uwp/schemas/appxpackage/uapmanifestschema/element-devicecapability).

Lens apps are not supported for UWP apps.

## Detecting the platform your app is running on

The way of thinking about app-targeting changes with Windows 10. The new conceptual model is that an app targets the Universal Windows Platform (UWP) and runs across all Windows devices. It can then opt to light up features that are exclusive to particular device families. If needed, the app also has the option to limit itself to targeting one or more device families specifically. For more info on what device families are—and how to decide which device family to target—see [Guide to UWP apps](../get-started/universal-application-platform-guide.md).

**Note**   We recommend that you not use operating system or device family to detect the presence of features. Identifying the current operating system or device family is usually not the best way to determine whether a particular operating system or device family feature is present. Rather than detecting the operating system or device family (and version number), test for the presence of the feature itself (see [Conditional compilation, and adaptive code](wpsl-to-uwp-porting-to-a-uwp-project.md)). If you must require a particular operating system or device family, be sure to use it as a minimum supported version, rather than design the test for that one version.

To tailor your app's UI to different devices, there are several techniques that we recommend. Continue to use auto-sized elements and dynamic layout panels as you always have. In your XAML markup, continue to use sizes in effective pixels (formerly view pixels) so that your UI adapts to different resolutions and scale factors (see [View/effective pixels, viewing distance, and scale factors](wpsl-to-uwp-porting-xaml-and-ui.md).). And use Visual State Manager's adaptive triggers and setters to adapt your UI to the window size (see [Guide to UWP apps](../get-started/universal-application-platform-guide.md).).

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

Also, see [Conditional compilation, and adaptive code](wpsl-to-uwp-porting-to-a-uwp-project.md).

## Device status

A Windows Phone Silverlight app can use the **Microsoft.Phone.Info.DeviceStatus** class to get info about the device on which the app is running. While there is no direct UWP equivalent for the **Microsoft.Phone.Info** namespace, here are some properties and events that you can use in a UWP app in place of calls to members of the **DeviceStatus** class.

| Windows Phone Silverlight                                                               | UWP                                                                                                                                                                                                                                                                                                                                |
|-----------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **ApplicationCurrentMemoryUsage** and **ApplicationCurrentMemoryUsageLimit** properties | [**MemoryManager.AppMemoryUsage**](/uwp/api/windows.system.memorymanager.appmemoryusage) and [**AppMemoryUsageLimit**](/uwp/api/windows.system.memorymanager.appmemoryusagelimit) properties                                                                                                                                    |
| **ApplicationPeakMemoryUsage** property                                                 | Use the memory profiling tools in Visual Studio. For more info, see [Analyze memory usage](/visualstudio/welcome-to-visual-studio-2015?view=vs-2015).                                                                                                                                                                          |
| **DeviceFirmwareVersion** property                                                      | [**EasClientDeviceInformation.SystemFirmwareVersion**](/uwp/api/windows.security.exchangeactivesyncprovisioning.easclientdeviceinformation.systemfirmwareversion) property (desktop device family only)                                                                                                                                                                             |
| **DeviceHardwareVersion** property                                                      | [**EasClientDeviceInformation.SystemHardwareVersion**](/uwp/api/windows.security.exchangeactivesyncprovisioning.easclientdeviceinformation.systemhardwareversion) property (desktop device family only)                                                                                                                                                                             |
| **DeviceManufacturer** property                                                         | [**EasClientDeviceInformation.SystemManufacturer**](/uwp/api/windows.security.exchangeactivesyncprovisioning.easclientdeviceinformation.systemmanufacturer) property (desktop device family only)                                                                                                                                                                                |
| **DeviceName** property                                                                 | [**EasClientDeviceInformation.SystemProductName**](/uwp/api/windows.security.exchangeactivesyncprovisioning.easclientdeviceinformation.systemproductname) property (desktop device family only)                                                                                                                                                                                 |
| **DeviceTotalMemory** property                                                          | No equivalent                                                                                                                                                                                                                                                                                                                      |
| **IsKeyboardDeployed** property                                                         | No equivalent. This property provides information about hardware keyboards for mobile devices, which are not commonly used.                                                                                                                                                                                                        |
| **IsKeyboardPresent** property                                                          | No equivalent. This property provides information about hardware keyboards for mobile devices, which are not commonly used.                                                                                                                                                                                                        |
| **KeyboardDeployedChanged** event                                                       | No equivalent. This property provides information about hardware keyboards for mobile devices, which are not commonly used.                                                                                                                                                                                                        |
| **PowerSource** property                                                                | No equivalent                                                                                                                                                                                                                                                                                                                      |
| **PowerSourceChanged** event                                                            | Handle the [**RemainingChargePercentChanged**](/uwp/api/windows.phone.devices.power.battery.remainingchargepercentchanged) event (mobile device family only). The event is raised when the value of the [**RemainingChargePercent**](/uwp/api/windows.phone.devices.power.battery.remainingchargepercent) property (mobile device family only) decreases by 1%. |

## Location

When an app that declares the Location capability in its app package manifest runs on Windows 10, the system will prompt the end-user for consent. So, if your app displays its own custom consent prompt, or if it provides an on-off toggle, then you will want to remove that so that the end-user is only prompted once.

## Orientation

The UWP app equivalent of the **PhoneApplicationPage.SupportedOrientations** and **Orientation** properties is the [**uap:InitialRotationPreference**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-splashscreen) element in the app package manifest. Select the **Application** tab if it isn't already selected and select one or more check boxes under **Supported rotations** to record your preferences.

You're encouraged, however, to design the UI of your UWP app to look great regardless of device orientation and screen size. There's more about that in [Porting for form factors and user experience](wpsl-to-uwp-form-factors-and-ux.md), which is the topic after next.

The next topic is [Porting business and data layers](wpsl-to-uwp-business-and-data.md).