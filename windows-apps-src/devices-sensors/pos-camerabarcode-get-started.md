---
title: Getting Started with Camera Barcode Scanner
description: Use these step-by-step instructions and code snippets to get started using a camera barcode scanner.
ms.date: 09/02/2019
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Getting started with a camera barcode scanner

The snippets used here are for demonstration purposes only. For a working sample, see the [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner).

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

## Step 4: Enumerate all barcode scanners

If you do not expect the list of devices to change over the lifespan of your application you can enumerate a snapshot just once with [DeviceInformation.FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync), but if you believe that the list of barcode scanners could change over the lifespan of your application you should use a [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) instead.  

> [!Important]
> Using GetDefaultAsync to enumerate PointOfService devices can result in inconsistent behavior as it simply returns the first device found in the class and this can change from session to session.

### **Option A: Enumerate a snapshot of barcode scanners**

```Csharp
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

> [!TIP]
> See [*Enumerate a snapshot of devices*](./enumerate-devices.md#enumerate-a-snapshot-of-devices) for more information on using *FindAllAsync*.

### **Option B: Enumerate available barcode scanners and watch for changes to the available scanners**

```Csharp
DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(selector);
watcher.Added += Watcher_Added;
watcher.Removed += Watcher_Removed;
watcher.Updated += Watcher_Updated;
watcher.Start();
```

> [!TIP]
> See [*Enumerate and watch device changes*](./enumerate-devices.md#enumerate-and-watch-devices) and [*DeviceWatcher*](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) for more information.

## Step 5: Identify camera barcode scanners

A camera barcode scanner is created dynamically as Windows pairs the camera(s) attached to your computer with a software decoder.  Each camera - decoder pair is a fully functional barcode scanner.

For each barcode scanner in the resulting device collection, you can differentiate between camera barcode scanners and physical barcode scanners by checking the [*BarcodeScanner.VideoDeviceID*](/uwp/api/windows.devices.pointofservice.barcodescanner.videodeviceid#Windows_Devices_PointOfService_BarcodeScanner_VideoDeviceId) property.  A non-NULL VideoDeviceID indicates that the barcode scanner object from your device collection is a camera barcode scanner.  If you have more than one camera barcode scanner you might want to build a separate collection which excludes physical barcode scanners.

Camera barcode scanners using the decoder that ships with Windows are identified as:

> Microsoft BarcodeScanner (*name of your camera here*)

If you have more than one camera, and they are built into the chassis of your computer, the name might differentiate between *front* and *rear* cameras.

> [!NOTE]
> In the future, additional software decoders with different naming schemes might be released.

When the DeviceWatcher starts (step 4), it enumerates through each connected device. Here we add the available scanners to a barcode scanner collection and bind the collection to a ListBox.

```csharp
ObservableCollection<BarcodeScannerInfo> barcodeScanners = new ObservableCollection<BarcodeScannerInfo>();

private async void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        barcodeScanners.Add(new BarcodeScannerInfo(args.Name, args.Id));

        // Select the first scanner by default.
        if (barcodeScanners.Count == 1)
        {
            ScannerListBox.SelectedIndex = 0;
        }
    });
}
```

When the SelectedIndex of the ListBox changes (the first item is selected by default in the previous snippet), we query the device info.

```csharp
private async void ScannerSelection_Changed(object sender, SelectionChangedEventArgs args)
{
    var selectedScannerInfo = (BarcodeScannerInfo)args.AddedItems[0];
    var deviceId = selectedScannerInfo.DeviceId;

    await SelectScannerAsync(deviceId);
}
```

## Step 6: Claim the camera barcode scanner

Use [BarcodeScanner.ClaimScannerAsync](/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync#Windows_Devices_PointOfService_BarcodeScanner_ClaimScannerAsync) to obtain exclusive use of the camera barcode scanner.

```csharp
private async Task SelectScannerAsync(string scannerDeviceId)
{
    selectedScanner = await BarcodeScanner.FromIdAsync(scannerDeviceId);

    if (selectedScanner != null)
    {
        claimedScanner = await selectedScanner.ClaimScannerAsync();
        if (claimedScanner != null)
        {
            await claimedScanner.EnableAsync();
        }
        else
        {
            rootPage.NotifyUser("Failed to claim the selected barcode scanner", NotifyType.ErrorMessage);
        }
    }
    else
    {
        rootPage.NotifyUser("Failed to create a barcode scanner object", NotifyType.ErrorMessage);
    }
}
```

## Step 7: System provided preview

A camera preview is needed for the user to successfully aim the camera at barcodes.  Windows provides a simple camera preview that launches a dialog for basic control of the camera barcode scanner.  Simply call [ClaimedBarcodeScanner.ShowVideoPreview](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.showvideopreviewasync) to open the dialog and [ClaimedBarcodeScanner.HideVideoPreview](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.hidevideopreview) to close it when finished.

> [!TIP]
> See [Hosting Preview](pos-camerabarcode-hosting-preview.md) to host the preview for camera barcode scanner in your application.

## Step 8: Initiate scan

You can initiate the scan process by calling [**StartSoftwareTriggerAsync**](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.startsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StartSoftwareTriggerAsync).

Depending on the value of [**IsDisabledOnDataReceived**](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) the scanner might scan only one barcode then stop or scan continuously until you call [**StopSoftwareTriggerAsync**](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.stopsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StopSoftwareTriggerAsync).

Set the desired value of [**IsDisabledOnDataReceived**](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) to control the scanner behavior when a barcode is decoded.

| Value | Description |
| ----- | ----------- |
| True   | Scan only one barcode then stop |
| False  | Continuously scan barcodes without stopping |

## See also

### Samples

- [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)