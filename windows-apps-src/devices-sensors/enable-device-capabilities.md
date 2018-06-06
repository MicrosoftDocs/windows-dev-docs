---
author: muhsinking
ms.assetid: 949D1CE0-DD7D-420E-904D-758FADEBE85A
title: Enable device capabilities
description: This tutorial describes how to declare device capabilities in Microsoft Visual Studio. This enables your app to use cameras, microphones, location sensors, and other devices.
ms.author: mukin
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Enable device capabilities



This tutorial describes how to declare device capabilities in Microsoft Visual Studio. This enables your app to use cameras, microphones, location sensors, and other devices.

## Specify the device capabilities your app will use


Windows apps require you to specify in the app package manifest when you use certain types of devices. In Visual Studio, you can declare most capabilities by using [Manifest Designer](https://msdn.microsoft.com/library/windows/apps/xaml/br230259.aspx) or you can add them manually as described in [How to specify device capabilities in a package manifest (manually)](https://msdn.microsoft.com/library/windows/apps/Dn263092). This tutorial assumes you're using Manifest Designer.

**Note**  
Some types of devices, such as printers, scanners, and sensors, don't need to be declared in the app package manifest.

-   In Visual Studio Solution Explorer, double-click the package manifest file, **Package.appxmanifest**.
-   Open the **Capabilities** tab.
-   Select the device capabilities that your app uses. If you don't see the capability you're looking for in Manifest Designer, add it manually. For more info, see [How to specify device capabilities in a package manifest](https://msdn.microsoft.com/library/windows/apps/Dn263092).

| Device Capability | Manifest Designer | Description |
|-------------------|-------------------|-------------|    
| AllJoyn | ![Available in Manifest Designer](images/ap-tools.png) | Allows AllJoyn-enabled apps and devices on a network to discover and interact with each other. App apps that access APIs in the [**Windows.Devices.AllJoyn**](https://msdn.microsoft.com/library/windows/apps/Dn894971) namespace must use this capability. |
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
| Point of Service | ![Available in Manifest Designer](images/ap-tools.png) | Provides access to Point of Service peripherals. This capability is required to access APIs in the [**Windows.Devices.PointOfService**](https://docs.microsoft.com/uwp/api/Windows.Devices.PointOfService) namespace. | 
| Private Networks (Client &amp; Server) | ![Available in Manifest Designer](images/ap-tools.png) | Provides inbound and outbound access to Intranet networks that have an authenticated domain controller, or that the user has designated as either home or work networks. Inbound access to critical ports is always blocked. | 
| Proximity | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to connect to devices in close proximity to the PC via near-field communication (NFC). Near-field proximity may be used to send files or communicate with an app on the nearby device. | 
| Removable Storage | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to add, change, or delete files on removable storage devices. The app can only access the file types on removable storage that are defined in the manifest using the **File Type Associations** declaration. The app can't access removable storage on **HomeGroup** PCs. | 
| Shared User Certificates | ![Available in Manifest Designer](images/ap-tools.png) | This capability is subject to the Microsoft Store policy. It provides the capability to access software and hardware certificates, such as smart card certificates, for validating a user's identity. When related APIs are invoked at runtime, the user must take action (insert card, select certificate, etc.). This capability is not necessary if your app includes a private certificate via a **Certificates** declaration. | 
| User Account Information | ![Available in Manifest Designer](images/ap-tools.png) | Gives apps the ability to access the user's name and picture. This capability is required to access some APIs in the [**Windows.System.UserProfile**](https://msdn.microsoft.com/library/windows/apps/BR241881) namespace. | 
| Videos Library | ![Available in Manifest Designer](images/ap-tools.png) | Provides the capability to add, change, or delete files in the **Videos Library** for the local PC and **HomeGroup** PCs. | 
| VOIP Calling | ![Available in Manifest Designer](images/ap-tools.png) | Allows apps to access the VOIP calling APIs in the [**Windows.ApplicationModel.Calls**](https://msdn.microsoft.com/library/windows/apps/Dn297266) namespace. | 
| Webcam | ![Available in Manifest Designer](images/ap-tools.png) | Provides access to the built-in camera or attached webcam's video feed. This allows the app to capture snapshots and movies. | 
| USB | | Provides access to custom USB devices. This capability requires child elements. This feature is not supported on Windows Phone. | 
| Human Interface Device (HID) | | Provides access to Human Interface Devices (HID). This capability requires child elements. For more info, see [How to specify device capabilities for HID](https://msdn.microsoft.com/library/windows/apps/Dn263091). | 
| Bluetooth GATT | | Provides access to Bluetooth LE devices through a collection of primary services, included services, characteristics, and descriptors. This capability requires child elements. For more info, see [How to specify device capabilities for Bluetooth](https://msdn.microsoft.com/library/windows/apps/Dn263090). | 
| Bluetooth RFCOMM |  | Provides access to APIs that support the Basic Rate/Extended Data Rate (BR/EDR) transport and also lets your UWP app access a device that implements Serial Port Profile (SPP). This capability requires child elements. For more info, see [How to specify device capabilities for Bluetooth](https://msdn.microsoft.com/library/windows/apps/Dn263090). |

## Use the Windows Runtime API for communicating with your device

The following table connects some of the capabilities to Windows Runtime APIs.

| Device Capability        | API             | 
|--------------------------|-----------------|
| AllJoyn                  | [**Windows.Devices.AllJoyn**](https://msdn.microsoft.com/library/windows/apps/Dn894971) | 
| Blocked Chat Messages    | [**Windows.ApplicationModel.CommunicationBlocking**](https://msdn.microsoft.com/library/windows/apps/Dn974207) | 
| Location                 | See [Maps and location overview](https://msdn.microsoft.com/library/windows/apps/Mt219699) for more information. | 
| Phone Call               | [**Windows.ApplicationModel.Calls**](https://msdn.microsoft.com/library/windows/apps/Dn297266) | 
| User Account Information | [**Windows.System.UserProfile**](https://msdn.microsoft.com/library/windows/apps/BR241881) | 
| VOIP Calling             | [**Windows.ApplicationModel.Calls**](https://msdn.microsoft.com/library/windows/apps/Dn297266) | 
| USB                      | [**Windows.Devices.Usb**](https://msdn.microsoft.com/library/windows/apps/Dn278466) | 
| HID                      | [**Windows.Devices.HumanInterfaceDevice**](https://msdn.microsoft.com/library/windows/apps/Dn264174) | 
| Bluetooth GATT           | [**Windows.Devices.Bluetooth.GenericAttributeProfile**](https://msdn.microsoft.com/library/windows/apps/Dn297685) | 
| Bluetooth RFCOMM         | [**Windows.Devices.Bluetooth.Rfcomm**](https://msdn.microsoft.com/library/windows/apps/Dn263529) | 
| Point of Service         | [**Windows.Devices.PointOfService**](https://msdn.microsoft.com/library/windows/apps/Dn298071) |

