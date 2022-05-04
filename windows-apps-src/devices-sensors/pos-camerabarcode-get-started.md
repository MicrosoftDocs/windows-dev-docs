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

### Option A: Find all barcode scanners

Use <xref:Windows.Devices.PointOfService.BarcodeScanner.GetDeviceSelector%2A?displayProperty=nameWithType> to retrieve an Advanced Query Syntax (AQS) string for listing the available barcode scanners.

```Csharp
string selector = BarcodeScanner.GetDeviceSelector();
```

### Option B: Scoping device selector to connection type

Use <xref:Windows.Devices.PointOfService.BarcodeScanner.GetDeviceSelector(Windows.Devices.PointOfService.PosConnectionTypes)?displayProperty=nameWithType> to retrieve an Advanced Query Syntax (AQS) string for listing the available barcode scanners over the specified connection types.

```Csharp
string selector = BarcodeScanner.GetDeviceSelector(PosConnectionTypes.Local);
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

## Step 4: Enumerate all barcode scanners

If you do not expect the list of devices to change over the lifespan of your application you can enumerate a snapshot just once with [DeviceInformation.FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync), but if you believe that the list of barcode scanners could change over the lifespan of your application you should use a [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) instead.  

> [!Important]
> Each PointOfService object (such as <xref:Windows.Devices.PointOfService.BarcodeScanner>, <xref:Windows.Devices.PointOfService.MagneticStripeReader>, <xref:Windows.Devices.PointOfService.PosPrinter>, and so on) supports a GetDefaultAsync method to enumerate the PointOfService devices. However, this simply returns the first device found in the class, which can change from session to session.

### Option A: Enumerate a snapshot of barcode scanners

This example shows how to enumerate the currently detected barcode scanners using <xref:Windows.Devices.Enumeration.DeviceInformation.FindAllAsync?displayProperty=nameWithType>.

```Csharp
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

> [!TIP]
> See [*Enumerate a snapshot of devices*](./enumerate-devices.md#enumerate-a-snapshot-of-devices) for more information on using *FindAllAsync*.

### Option B: Enumerate available barcode scanners and watch for changes to the available scanners

This example shows how to create a watcher for changes to the collection of currently detected barcode scanners using <xref:Windows.Devices.Enumeration.DeviceInformation.CreateWatcher><xref:Windows.Devices.Enumeration.DeviceInformation.CreateWatcher(System.String)?displayProperty=nameWithType>.

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

A camera barcode scanner is created dynamically as Windows pairs the camera(s) attached to your computer with a software decoder.  Each camera-decoder pair is a fully functional barcode scanner.

For each barcode scanner in the resulting device collection, you can differentiate between camera barcode scanners and physical barcode scanners by checking the <xref:Windows.Devices.PointOfService.BarcodeScanner.VideoDeviceId?displayProperty=nameWithType> property. A non-NULL device ID indicates that the barcode scanner object from your device collection is a camera barcode scanner.  If you have more than one camera barcode scanner you might want to build a separate collection which excludes physical barcode scanners.

Camera barcode scanners using the decoder that ships with Windows are identified as:

> Microsoft BarcodeScanner (*name of your camera here*)

If you have more than one camera, and they are built into the chassis of your computer, the name might differentiate between *front* and *rear* cameras.

When the DeviceWatcher starts (see [Step 4](#step-4-enumerate-all-barcode-scanners)), it enumerates through each connected device. Here we add the available scanners to a barcode scanner collection and bind that collection to a ListBox.

```csharp
ObservableCollection<BarcodeScanner> barcodeScanners = new ObservableCollection<BarcodeScanner>();

private async void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        barcodeScanners.Add(new BarcodeScanner(args.Name, args.Id));

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
    var selectedScannerInfo = (BarcodeScanner)args.AddedItems[0];
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

You can initiate the scan process by calling [StartSoftwareTriggerAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.startsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StartSoftwareTriggerAsync).

Depending on the value of [IsDisabledOnDataReceived](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) the scanner might scan only one barcode then stop or scan continuously until you call [StopSoftwareTriggerAsync](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.stopsoftwaretriggerasync#Windows_Devices_PointOfService_ClaimedBarcodeScanner_StopSoftwareTriggerAsync).

Set the desired value of [IsDisabledOnDataReceived](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.isdisabledondatareceived#Windows_Devices_PointOfService_ClaimedBarcodeScanner_IsDisabledOnDataReceived) to control the scanner behavior when a barcode is decoded.

| Value | Description |
| ----- | ----------- |
| True   | Scan only one barcode then stop |
| False  | Continuously scan barcodes without stopping |

## See also

### Samples

- [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)