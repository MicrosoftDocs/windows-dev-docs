---

title: Enable device capabilities
description: This topic explains how to declare device capabilities in Microsoft Visual Studio to use cameras, microphones, location sensors, and other devices in a Windows app.
ms.date: 03/18/2026
ms.topic: article
keywords: windows 10, windows 11, uwp, winui, windows app sdk, device capabilities
ms.localizationpriority: medium
---

# Enable device capabilities for a Windows app

This topic explains how to declare device capabilities in Microsoft Visual Studio when using cameras, microphones, location sensors, and other devices in a Windows app.

> [!NOTE]
> Not all devices (such as printers and scanners) need to be declared in the app package manifest.

## Specify the device capabilities your app will use

You must declare in the app package manifest specific types of devices being used by your app. In Visual Studio, you can declare most using the [Manifest Designer](/visualstudio/extensibility/vsix-manifest-designer) or you can add them manually as described in [How to specify device capabilities in a package manifest (manually)](/uwp/schemas/appxpackage/how-to-specify-device-capabilities-in-a-package-manifest). This tutorial uses the Manifest Designer.

> [!NOTE]
> For Windows App SDK (WinUI 3) packaged apps, device capabilities are declared in the same **Package.appxmanifest** file using the same capability elements as UWP apps. The device capability model is shared across both platforms.

- In Visual Studio Solution Explorer, double-click the package manifest file, **Package.appxmanifest**.
- Open the **Capabilities** tab.
- Select the device capabilities that your app uses. If you don't see the capability you're looking for in Manifest Designer, add it manually. For more info, see [How to specify device capabilities in a package manifest](/uwp/schemas/appxpackage/how-to-specify-device-capabilities-in-a-package-manifest).

| Device Capability | Manifest Designer | Description |
|-------------------|-------------------|-------------|    
| Blocked Chat Messages | ![Available in Manifest Designer](images/ap-tools.png) | Allows apps to read SMS and MMS messages that have been blocked by the Spam Filter app. |
| Chat Message Access | ![Available in Manifest Designer](images/ap-tools.png) | Allows apps to read and delete Text Messages. It also allows apps to store chat messages in the system data store. |
| Code Generation | ![Available in Manifest Designer](images/ap-tools.png) | Allows apps to generate code dynamically. |
| Enterprise Authentication | ![Available in Manifest Designer](images/ap-tools.png) | This capability is subject to the Microsoft Store policy. It provides the capability to connect to enterprise intranet resources that require domain credentials. This capability is not typically needed for most apps. | 
| Internet (Client) | ![Available in Manifest Designer](images/ap-tools.png) | Provides outbound access to the Internet and networks in public places like airports and coffee shops. For example, Intranet networks where the user has designated the network as public. Most apps that require Internet access should use the capability. |
| Internet (Client &amp; Server) | ![Available in Manifest Designer](images/ap-tools.png) | Provides inbound and outbound access to the Internet and the networks in public places like airports and coffee shops. This capability is a superset of **Internet (Client)**. **Internet (Client)** doesn't need to be enabled if this capability is also enabled. Inbound access to critical ports is always blocked. |
| Location| ![Available in Manifest Designer](images/ap-tools.png) | Provides access to the current location. This is obtained from dedicated hardware like a GPS sensor in the PC, or derived from available network information. | 
| Microphone | ![Available in Manifest Designer](images/ap-tools.png) | Provides access to the microphone's audio feed. This allows the app to record from connected microphones. | 
| Music Library | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to add, change, or delete files in the **Music Library** for the local PC and **HomeGroup** PCs. | 
| Objects 3D | ![Available in Manifest Designer](images/ap-tools.png) | Provides programmatic access to the user's **3D Objects**, allowing the app to enumerate and access all files in the library without user interaction. This capability is typically used in 3D apps and games that need to access the entire **3D Objects** library. | 
| Phone Call | ![Available in Manifest Designer](images/ap-tools.png) | Allows apps to access all of the phone lines on the device and perform the following functions: place a call on the phone and show the system dialer without prompting the user; access line-related metadata; access line-related triggers. Allows the user-selected spam filter app to set and check the block list and call origin information. | 
| Pictures Library | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to add, change, or delete files in the **Pictures Library** for the local PC and **HomeGroup** PCs. | 
| Point of Service | ![Available in Manifest Designer](images/ap-tools.png) | Provides access to Point of Service peripherals. |
| Private Networks (Client &amp; Server) | ![Available in Manifest Designer](images/ap-tools.png) | Provides inbound and outbound access to Intranet networks that have an authenticated domain controller, or that the user has designated as either home or work networks. Inbound access to critical ports is always blocked. | 
| Proximity | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to connect to devices in close proximity to the PC via near-field communication (NFC). Near-field proximity may be used to send files or communicate with an app on the nearby device. | 
| Removable Storage | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to add, change, or delete files on removable storage devices. The app can only access the file types on removable storage that are defined in the manifest using the **File Type Associations** declaration. The app can't access removable storage on **HomeGroup** PCs. | 
| Shared User Certificates | ![Available in Manifest Designer](images/ap-tools.png) | This capability is subject to the Microsoft Store policy. It provides the capability to access software and hardware certificates, such as smart card certificates, for validating a user's identity. When related APIs are invoked at runtime, the user must take action (insert card, select certificate, etc.). This capability is not necessary if your app includes a private certificate via a **Certificates** declaration. | 
| User Account Information | ![Available in Manifest Designer](images/ap-tools.png) | Gives apps the ability to access the user's name and picture. |
| Videos Library | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to add, change, or delete files in the **Videos Library** for the local PC and **HomeGroup** PCs. | 
| VOIP Calling | ![Available in Manifest Designer](images/ap-tools.png) | Allows apps to access the VOIP calling APIs. | 
| Webcam | ![Available in Manifest Designer](images/ap-tools.png) | Provides access to the built-in camera or attached webcam's video feed. This allows the app to capture snapshots and movies. | 
| USB | | Provides access to custom USB devices. This capability requires child elements. This feature is not supported on Windows Phone. | 
| Human Interface Device (HID) | | Provides access to Human Interface Devices (HID). This capability requires child elements. For more info, see [How to specify device capabilities for HID](/uwp/schemas/appxpackage/how-to-specify-device-capabilities-for-hid). | 
| Bluetooth GATT | | Provides access to Bluetooth LE devices through a collection of primary services, included services, characteristics, and descriptors. This capability requires child elements. For more info, see [How to specify device capabilities for Bluetooth](/uwp/schemas/appxpackage/how-to-specify-device-capabilities-for-bluetooth). | 
| Bluetooth RFCOMM |  | Provides access to APIs that support the Basic Rate/Extended Data Rate (BR/EDR) transport and also lets your app access a device that implements Serial Port Profile (SPP). This capability requires child elements. For more info, see [How to specify device capabilities for Bluetooth](/uwp/schemas/appxpackage/how-to-specify-device-capabilities-for-bluetooth). |

## Windows App SDK considerations

When using the Windows App SDK, keep the following in mind regarding device capabilities:

- **Packaged apps**: Device capabilities are declared in `Package.appxmanifest` the same way as UWP apps. The capability names and XML elements are identical.
- **Unpackaged apps**: Unpackaged Windows App SDK apps do not use an app package manifest. Instead, device access is governed by OS-level permissions and user consent prompts at runtime. No capability declarations are needed, but users may still be prompted to grant access (for example, for camera or microphone).
- **Windows Runtime APIs**: The Windows Runtime device APIs (such as `Windows.Devices.Bluetooth`, `Windows.Devices.Usb`, and `Windows.Media.Capture`) are available to both UWP and Windows App SDK apps. You can call these APIs directly from WinUI 3 projects.
