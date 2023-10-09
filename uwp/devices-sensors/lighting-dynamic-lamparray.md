---

title: Dynamic lighting
description: This topic describes how to control device lamp arrays using the Windows.Devices.Lights namespace.
ms.date: 10/05/2023
ms.topic: article
ms.localizationpriority: medium
---

# Dynamic lighting

This topic describes how your Windows apps can provide dynamic lighting effects across connected devices that implement the open [Human Interface Devices (HID)](https://usb.org/sites/default/files/hut1_4.pdf) [Lighting and Illumination standard](https://www.usb.org/sites/default/files/hutrr84_-_lighting_and_illumination_page.pdf). In particular, the LampArray specification for devices that have one or more lamps (lights, LEDs, bulbs, and so on).

**Important APIs**

- [**Windows.Devices.Lights**](/uwp/api/windows.devices.lights)

    > [!NOTE]
    > The [Lamp](/uwp/api/windows.devices.lights.lamp) class does not apply to HID [LampArray](/uwp/api/windows.devices.lights.lamparray) functionality and is not relevant to this topic.

## Overview

Dynamic lighting support in Windows lets both app developers and end users control and synchronize lighting effects across peripherals and other connected devices.

**Supported app types and platforms**

- Win10 version 1809 (October 2018) and later.
  - Applies to foreground UWP and Win32 apps.
- Windows 11 Build 23466 (Preview) and later.
  - Applies to foreground and background (ambient) UWP and Win32 apps.
- Xbox GDK March 2023 Update 1 and later.
  - See the [Lighting API](/gaming/gdk/_content/gc/lighting/gc-lighting-toc) in the [Game Development Kit (GDK)](/gaming/gdk/).

**Supported devices and device types**

- Keyboard or keypad
- Mouse
- Game controller (gamepad, flightstick, steering wheel, and so on).
- Peripheral (general devices such as speakers, mouse pads, microphones, webcams, and so on).
- Scene (room/stage/area devices such as light bulbs, spotlights, strobe lights, billboards, camera flashes, and so on).
- Notification (user attention devices such as alarms, voice assistants, and so on).
- Chassis (internal PC components such as RAM, motherboard, fan, and so on).
- Wearable (accessories such as headsets, watches, fitness trackers, shoes, and so on).
- Furniture (such as chairs, desks, bookcases, and so on).
- Art (such as a painting or sculpture).
- Headset (accessories designed specifically for the head, such as headphones or microphones).

A Windows app can control HID LampArray devices when the app is in the foreground (starting with Windows 10) and when it's in the background (also known as *ambient lighting*, starting with Windows 11).

Users can customize their LampArray device experience (both foreground and background) through the **Settings -> Personalization -> Dynamic lighting** screen, letting them synchronize devices from different manufacturers, control brightness and effects across selected devices and form factors, and prioritize access to devices by ambient background apps. These features enable your apps to entertain users, make them more productive, make their experiences across Windows more accessible, and deliver cohesive experiences across the set of Dynamic Lighting-compatible devices.

:::image type="content" source="images/lighting/settings-dynamic-lighting.png" alt-text="Screenshot of the Dynamic Lighting settings screen.":::

## Device prioritization

Windows prioritizes dynamic lighting based on app state. By default, a foreground app is always assigned control of a LampArray device unless the user has specified otherwise in Settings. In cases where two or more ambient background apps are trying to control a LampArray device, the system will assign control to the app prioritized in Settings.

### Background (ambient) lighting

The "ambient" APIs in [**Windows.Devices.Lights**](/uwp/api/windows.devices.lights) enable background applications to control LampArray devices while the user is interacting with an unrelated app in the foreground (such as music apps that drive synchronized lighting effects).

Apps can receive [LampArray.AvailabilityChanged](/uwp/api/windows.devices.lights.lamparray.availabilitychanged) events, depending on user settings. In conjunction with the [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) class, apps can track and manage all connected/disconnected LampArray devices and see which the user expects the app to control. One example usage is a UI that renders an icon for each connected device, unavailable ones greyed out, and links to the Dynamic Lighting settings page where the user can make changes to foreground/background app preferences.

## User settings

Users can control and configure their HID LampArray devices at both the individual and global level through the Dynamic Lighting page in **Settings -> Personalization -> Dynamic Lighting**. This page will appear in Settings when at least one compatible device is connected to the PC.

:::image type="content" source="images/lighting/settings-dynamic-lighting.png" alt-text="Screenshot of the Dynamic Lighting settings screen.":::

1. When connected, compatible devices will show up in the device cards along the top of the page where users can change individual device settings.
2. Global Dynamic Lighting settings are located below the device cards (changes to these settings affect all connected devices).
   1. The *Use Dynamic Lighting on my devices* toggle lets users turn Dynamic Lighting on or off. When Dynamic Lighting is off, devices should function with their default non-Dynamic Lighting behavior. Dynamic lighting includes a built-in set of basic effects.
   2. The *Compatible apps in the foreground always control lighting* allows users to turn the default Dynamic Lighting app behavior on or off. When this feature is toggled off, a background app can control its associated devices even when a foreground app that wants control is active.
   3. The *Background light control* section lets users prioritize installed apps that have registered themselves as ambient background controllers. Dragging an app to the top of the list will prioritize it and ensure that it can control devices over other apps in the list. Ambient background settings are tied to a device and the port it's connected on.  If you unplug and then plug the LampArray into a different (USB) port, it will appear as a different device.
   4. The *Brightness* slider lets users set the LED brightness on their devices.
   5. The *Effects* dropdown lets users select colors and effects for their devices.

   :::image type="content" source="images/lighting/settings-dynamic-lighting-effects.png" alt-text="Screenshot of the Dynamic Lighting settings Effects screen.":::

> [!NOTE]
> When a device is not selected for *Background light control*, it operates in "Autonomous mode", which means that the device reverts to default firmware behavior.

## Packaging and app identity

Ambient background applications must declare the "com.microsoft.windows.lighting" AppExtension in the app manifest (for more detail on how to do this, see [Create and host an app extension](/windows/uwp/launch-resume/how-to-create-an-extension)). This requirement is enforced by the AmbientLightingServer, which only accepts connections from an AmbientLightingClient in a process with package identity (packaged app) supporting the extension. This requirement is necessary to enable the user to define policy for installed apps and to then correlate that policy at runtime.

App identity is required for ambient applications such that user preferences can be determined at runtime. Once an application is installed, if it is using the ambient APIs, the system needs to correlate the running instance of an app with the user's preferences. Furthermore, making your app available to the user in the settings requires a post installation artifact that indicates to the system that your app is a legitimate user of the ambient lighting APIs.

This identity requirement is achieved via [MSIX packaging](/windows/msix/).

If you are already using MSIX packaging for packaging and installation, there are no further requirements.

If you have an unpackaged app, there are additional steps required to obtain the application identity. You can either migrate your installation to full MSIX, or you can use the simplified [Sparse Packaging and External Location](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps) feature of MSIX. Sparse Packaging with External Location was designed to let existing app installations gain the benefit of app identity without requiring a full conversion of the setup/install to MSIX. It is a new step in your setup/installation that uses tools to create an MSIX package to represent the app you are installing.

You must define an [AppXManifest.xml packaging manifest](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual) that describes your installation. The MSIX package is created with the MakeAppXPackage tool. At install time, you install the MSIX package using a Package Manager API that specifies the location of your installed executable. For convenience, the [add-appxpackage PowerShell script](/powershell/module/appx/add-appxpackage) wraps this API behavior as well. Alternatively, [AddPackageByUriAsync](/uwp/api/windows.management.deployment.packagemanager.addpackagebyuriasync) can be used at install time to install the MSIX package.

For unpackaged app installations, there is also a [side-by-side application manifest](/windows/win32/sbscs/application-manifests) requirement for your executable.

See the [Deployment overview](/windows/apps/package-and-deploy/) for a more in-depth explanation of packaged and unpackaged apps.

## Glossary

The following terms and concepts are used to describe various ambient lighting system components.

- Autonomous Mode

  Defined in the HID spec as a mode where the hardware falls back to default behavior as defined by its firmware. For example, a device might have a preprogrammed visual effect that is the default when the OS is not actively controlling the device or if the user has opted out of OS involvement for the device. The device must respond to the HID command to return from Autonomous mode to ensure smooth interaction with user expectations.

- Ambient Apps

  [**Windows.Devices.Lights**](/uwp/api/windows.devices.lights) API consumers that also have package identity and support the required app extension. Ambient apps receive notifications from the AmbientLightingClient. The events inform the app of devices they have access to. In this way, an app could show UI enumerating the connected lighting devices, and grey out the devices that are currently inaccessible due to user policy settings. Ambient Apps utilize the [**Windows.Devices.Lights**](/uwp/api/windows.devices.lights) APIs to drive effects across available devices.

- Settings applet

  Stores per device user preferences in the HKEY_CURRENT_USER (HKCU) of the registry. The user can define on a per device basis the prioritized set of ambient apps for the given device. The user can also opt out of dynamic lighting.

- App Identity

  An app model concept. An app that has an app identity can be identified by the system at runtime.

- MSIX

  A Microsoft deployment and packaging technology formerly known as APPX.

## Examples

:::row:::
    :::column:::

        [LampArray sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/LampArray)
        
        Demonstrates how to control RGB lighting of peripheral devices using the [**Windows.Devices.Lights**](/uwp/api/windows.devices.lights) and [**Windows.Devices.Lights.Effects**](/uwp/api/windows.devices.lights.effects) APIs.

    :::column-end:::
    :::column:::

        [AutoRGB Sample](https://github.com/microsoft/Dynamic-Lighting-AutoRGB)
        
        Demonstrates how to extract a single, representative color from a desktop screen and use it to illuminate LED lamps on a connected RGB device.
            
    :::column-end:::
:::row-end:::

## See also

- [**Windows.Devices.Lights**](/uwp/api/windows.devices.lights)
- [**Windows.Devices.Lights.Effects**](/uwp/api/windows.devices.lights.effects)
- [Lighting and Illumination (www.usb.org)](https://www.usb.org/sites/default/files/hutrr84_-_lighting_and_illumination_page.pdf)
