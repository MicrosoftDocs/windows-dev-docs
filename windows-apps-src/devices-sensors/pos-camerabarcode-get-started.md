---
author: TerryWarwick
title: Getting Started with Camera Barcode Scanner
description: Learning how to use camera barcode scanner
ms.author: jken
ms.date: 05/1/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Getting started with a camera barcode scanner
## Step 1: Add capability declarations to your app manifest
1. In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2. Select the **Capabilities** tab
3. Check the boxes for **Webcam** and **PointOfService** 

>[!NOTE] 
> The **Webcam** capability is required to for the software decoder to receive frames from the camera to decode as well as to provide a preview from your application

## Step 2: Add using directives

```Csharp
using Windows.Devices.Enumeration;
using Windows.Devices.PointOfService;
```
## Step 3: Define your device selector

### **Option A: Find all barcode scanners**

```Csharp
string selector = BarcodeScanner.GetDeviceSelector();       
```

### **Option B: Scoping device selector to connection type**

```Csharp
string selector = BarcodeScanner.GetDeviceSelector(PosConnectionTypes.Local);
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

## Step 4: Enumerate barcode scanners
If you do not expect the list of devices to change over the lifespan of your application you can enumerate a snapshot just once with *FindAllAsync*, but if you believe that the list of barcode scanners could change over the lifespan of your application you should use a *DeviceWatcher* instead.  

> [!Important] 
> Using GetDefaultAsync to enumerate PointOfService devices can result in inconsistent behavior as it simply returns the first device found in the class and this can change from session to session.

### **Option A: Enumerate a snapshot of barcode scanners**
```Csharp
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

> [!TIP]
> See [*Enumerate a snapshot of devices*](https://docs.microsoft.com/windows/uwp/devices-sensors/enumerate-devices#enumerate-a-snapshot-of-devices) for more information on using *FindAllAsync*.

### **Option B: Enumerate and watch for changes in available barcode scanners**
```Csharp
DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(selector);

// TODO: Add Event Listeners and Handlers
```
> [!TIP]
> See [*Enumerate and watch device changes*](https://docs.microsoft.com/windows/uwp/devices-sensors/enumerate-devices#enumerate-and-watch-devices) and [*DeviceWatcher*](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) for more information.

## Step 5: Identify camera barcode scanners
A camera barcode scanner is created dynamically as Windows pairs the camera(s) attached to your computer with a software decoder.  Each camera - decoder pair is a fully functional barcode scanner.

For each barcode scanner in the resulting device collection, you can determine which are camera barcode scanners verses physical barcode scanners by checking the [*BarcodeScanner.VideoDeviceID*](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.videodeviceid#Windows_Devices_PointOfService_BarcodeScanner_VideoDeviceId) property.  A non-NULL VideoDeviceID will indicate that the barcode scanner object from your device collection is a camera barcode scanner.  If you have more than one camera barcode scanner you may want to build a separate collection which excludes physical barcode scanners. 

Camera barcode scanners using the decoder that ships with Windows will appear as: 

> Microsoft BarcodeScanner (*name of your camera here*)

If your camera is built in to the chassis of your computer, the name may differentiate between *front* and *rear* if you have more than one camera.  In the future, additional software decoders may be available and carry a different naming scheme.

## Step 6: Claim the camera barcode scanner 
Use [BarcodeScanner.ClaimScannerAsync](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync#Windows_Devices_PointOfService_BarcodeScanner_ClaimScannerAsync) to obtain exclusive use of the camera barcode scanner.

## Step 7: System provided preview
A camera preview is needed for the user to successfully aim the camera at barcodes.  Windows provides a simple camera preview that will launch a dialog that enables basic control of the camera barcode scanner.  Simply call [ClaimedBarcodeScanner.ShowVideoPreview](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.showvideopreviewasync) to open the dialog and [ClaimedBarcodeScanner.HideVideoPreview](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.hidevideopreview) to close it when finished.

> [!TIP]
> See [Hosting Preview](pos-camerabarcode-hosting-preview.md) to host the preview for camera barcode scanner in your application.

## Step 8: Initiate scan 
You can initiate the scan process by calling [**StartSoftwareTriggerAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.startsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StartSoftwareTriggerAsync).  
Depending on the value of [**IsDisabledOnDataReceived**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) the scanner may scan only one barcode then stop or scan continuously until you call 
[**StopSoftwareTriggerAsync**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.stopsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StopSoftwareTriggerAsync).

Set the desired value of [**IsDisabledOnDataReceived**](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) to control the scanner behavior when a barcode is decoded.

| Value | Description |
| ----- | ----------- |
| True   | Scan only one barcode then stop |
| False  | Continuously scan barcodes without stopping |