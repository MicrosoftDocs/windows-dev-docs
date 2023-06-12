---
title: PointOfService device capability
description: The PointOfService device capability is required to use APIs in the Windows.Devices.PointOfService namespace.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# PointOfService device capability

The PointOfService device capability is required to use APIs in the [Windows.Devices.PointOfService](/uwp/api/windows.devices.pointofservice) namespace.

You request access to the PointOfService APIs by declaring the capability in your application package manifest. Most capabilities can be declared through the Manifest Designer in Microsoft Visual Studio, or you can add them manually.

> [!IMPORTANT]
> You will receive the error **System.UnauthorizedAccessException** when you attempt to use an API in the Windows.Devices.PointOfService namespace if you do not declare the **pointOfService** capability in your application manifest. 

## Declare capability using Manifest Designer

1. In **Solution Explorer**, expand the project node of your UWP application.
2. Double-click the **Package.appxmanifest** file.  
*If the manifest file is already open in the XML code view, Visual Studio prompts you to close the file.*
3. Click the **Capabilities** tab
4. Click the checkbox next to **Point of Service** in the list of capabilities to enable the Point of Service device capability


## Declare capability manually

1. In **Solution Explorer**, expand the project node of your UWP application.
2. Right-click the **Package.appxmanifest** file and choose **View Code**
3. Add the PointOfService DeviceCapability element to Capabilities section of your application manifest.  

```xml
  <Capabilities>
    <DeviceCapability Name="pointOfService" />
  </Capabilities>
   ```
