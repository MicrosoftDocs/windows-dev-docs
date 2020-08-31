---
ms.assetid: B48E21AB-0EA5-444B-8333-393DD8D1B76D
title: Enterprise Shared Storage
description: Enterprise shared storage defines local data locations for line of business apps to share data.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Enterprise Shared Storage

The shared storage consists of two locations, where apps with the restricted capability  **enterpriseDeviceLockdown** and an Enterprise certificate have full read and write access. Note that the **enterpriseDeviceLockdown** capability allows apps to use the device lock down API and access the enterprise shared storage folders. For more information about the API, see [**Windows.Embedded.DeviceLockdown**](/uwp/api/Windows.Embedded.DeviceLockdown) namespace.  

These locations are set on the local drive:
- \Data\SharedData\Enterprise\Persistent
- \Data\SharedData\Enterprise\Non-Persistent

## Scenarios

Enterprise shared storage provides support for the following scenarios.

- You can share data within an instance of an app, between instances of the same app, or even between apps assuming they both have the appropriate capability and certificate.
- You can store data on the local hard drive in the \Data\SharedData\Enterprise\Persistent folder and it persists even after the device has been reset.
- Manipulate files, including read, write, and delete of files on a device via Mobile Device Management (MDM) service. For more information on how to use enterprise shared storage through the MDM service, see [EnterpriseExtFileSystem CSP](/windows/client-management/mdm/enterpriseextfilessystem-csp).

## Access enterprise shared storage

The following example shows how to declare the capability to access enterprise shared storage in the package manifest, and how to access the shared storage folders by using the Windows.Storage.StorageFolder class.

In your app package manifest, include the following capability:

```xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  IgnorableNamespaces="uap mp rescap">

…

<Capabilities>
    <rescap:Capability Name="enterpriseDeviceLockdown"/>
</Capabilities>
```

To access the shared data location, your app would use the following code.

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;

…

// Get the Enterprise Shared Storage folder.
var enterprisePersistentFolderRoot = @"C:\Data\SharedData\Enterprise\Persistent";

StorageFolder folder =
    await StorageFolder.GetFolderFromPathAsync(enterprisePersistentFolderRoot);

// Get the files in the folder.
IReadOnlyList<StorageFile> sortedItems =
    await folder.GetFilesAsync();

// Iterate over the results and print the list of files
// to the Visual Studio Output window.
foreach (StorageFile file in sortedItems)
    Debug.WriteLine(file.Name + ", " + file.DateCreated);
```