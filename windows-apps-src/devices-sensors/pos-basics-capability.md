---
title: PointOfService device capability
description: The PointOfService capability is required for use of Windows.Devices.PointOfService namespace
ms.date: 05/02/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# PointOfService device capability
You request access to the PointOfService APIs by declaring the capability in your application package manifest]  You can declare most capabilities by using the Manifest Designer, in Microsoft Visual Studio, or you can add them manually.  

> [!Important]
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
