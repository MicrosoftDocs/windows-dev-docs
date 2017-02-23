---
author: mukin
title: Cash Drawer
description: This article contains information about the Cash Drawer point of service family of devices
ms.author: wdg-dev-content
ms.date: 02/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid:
---

# Cash Drawer

Enables application developers to interact with cash drawers.
This topic covers the following:
+	Members
+	Requirements
+ Device support

## Members
The cash drawer device type has these types of members:
+	Classes
+	Enumerations
+	Interfaces

### Classes
| Class | Description |
|-------|-------------|
| [CashDrawer](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawer) | Represents a cash drawer device in a retail scenario. |
| [CashDrawerCapabilities](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawercapabilities) | Represents the cash drawer capabilities. |
| [CashDrawerCloseAlarm](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawerclosealarm) | The cash drawer close alarm. Parameter defaults are provided, however the user can update them as appropriate. |
| [CashDrawerClosedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawerclosedeventargs) | This object is passed as a parameter to the event handlers for the DrawerClosed event. |
| [CashDrawerEventSource](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawereventsource) | Provides event sources that allow a developer to detect when the cash drawer is opened or closed. |
| [CashDrawerOpenedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdraweropenedeventargs) | This object is passed as a parameter to the event handlers for the DrawerOpened event. |
| [CashDrawerStatus](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawerstatus) | Provides the current power and availability status of the cash drawer. |
| [CashDrawerStatusUpdatedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawerstatusupdatedeventargs) | This object is passed as a parameter to the event handlers for the StatusUpdated event. |
| [ClaimedCashDrawer](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.claimedcashdrawer) | Provides access to exclusive and privileged methods, properties, and events on a point-of-service cash drawer device. |

### Enumerations
| Enumeration |	Description |
|-------------|-------------|
| [CashDrawerStatusKind](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.cashdrawerstatuskind) | Defines the constants that indicates the barcode scanner status. |

### Interfaces
| Interface |	Description |
|-----------|-------------|
| [ICashDrawerEventSourceEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice.icashdrawereventsourceeventargs) | Defines the constants that indicates the barcode scanner status. |

## Requirements
Applications which require this namespace require the addition of “pointOfService” [DeviceCapability](https://msdn.microsoft.com/library/4353c4fd-f038-4986-81ed-d2ec0c6235ef) to the app package manifest.

## Device support
Connection directly to the cash drawer can be made over the network or through Bluetooth, depending on the capabilities of the cash drawer unit. Additionally, cash drawers that don’t have network or Bluetooth capabilities can be connected via the DK port on a supported POS printer, or the Star Micronics DK-AirCash accessory. At this time, there is no support for cash drawers connected via USB or serial port.

**Note:**  Contact Star Micronics for more information about the DK-AirCash.

### Network/Bluetooth
| Manufacturer |	Model(s) |
|--------------|-----------|
| APG Cash Drawer |	NetPRO, BluePRO |

## Examples
See the cash drawer sample for an example implementation.
+	[Cash drawer sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CashDrawer)
