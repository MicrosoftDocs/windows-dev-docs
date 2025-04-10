---
title: Get started with camera barcode scanner
description: Set up a basic camera barcode scanner in a UWP application.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Get started with a camera barcode scanner

This topic describes how to set up a basic camera barcode scanner in a UWP application.

> [!NOTE]
> The software decoder built into Windows 10/11 is provided by [*Digimarc Corporation*](https://www.digimarc.com/).

The following code snippets are for demonstration purposes only. For a complete working sample, see the [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BarcodeScanner).

## Step 1: Add capability declarations to your app manifest

1. In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2. Select the **Capabilities** tab.
3. Check the boxes for **Webcam** and **PointOfService**.

>[!NOTE]
> The **Webcam** capability is required for the software decoder to receive frames from the camera to both decode the barcode and provide a preview in your application.

## Step 2: Add `using` directives

```Csharp
using Windows.Devices.Enumeration;
using Windows.Devices.PointOfService;
```

## Step 3: Define your device selector

Use one of the [**BarcodeScanner.GetDeviceSelector**](/uwp/api/windows.devices.pointofservice.barcodescanner.getdeviceselector) methods to obtain a [**BarcodeScanner**](/uwp/api/windows.devices.pointofservice.barcodescanner) object for each connected barcode scanner.

### Option A: Find all barcode scanners

```Csharp
string selector = BarcodeScanner.GetDeviceSelector();
```

### Option B: Find all barcode scanners based on scope (for this example, we filter on [Local](/uwp/api/windows.devices.pointofservice.posconnectiontypes) connection type)

```Csharp
string selector = BarcodeScanner.GetDeviceSelector(PosConnectionTypes.Local);
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

## Step 4: Enumerate barcode scanners

If you do not expect the list of devices to change during the lifespan of your application, use [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) to get a one-time snapshot. However, if the list of barcode scanners could change during the lifespan of your application, use a [**DeviceWatcher**](/uwp/api/windows.devices.enumeration.devicewatcher) instead.

> [!IMPORTANT]
> Using [**GetDefaultAsync**](/uwp/api/windows.devices.pointofservice.barcodescanner.getdefaultasync) to enumerate [**PointOfService**](/uwp/api/windows.devices.pointofservice) devices can result in inconsistent behavior as it only returns the first device found in the class (which can change from session to session).

### Option A: Enumerate a snapshot of all connected barcode scanners based on the *selector* created in Step 3

In this snippet, we create a [DeviceInformationCollection](/uwp/api/windows.devices.enumeration.deviceinformationcollection) object and use ****[**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) to populate it.

```Csharp
DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(selector);
```

> [!TIP]
> See [Enumerate a snapshot of devices](enumerate-devices.md#enumerate-a-snapshot-of-devices)  for more information on using [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync).

### Option B: Enumerate available barcode scanners based on the *selector* created in Step 3 and watch for changes to that collection

In this snippet, we create a [**DeviceWatcher**](/uwp/api/windows.devices.enumeration.devicewatcher) using [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher).

```Csharp
DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(selector);
watcher.Added += Watcher_Added;
watcher.Removed += Watcher_Removed;
watcher.Updated += Watcher_Updated;
watcher.Start();
```

> [!TIP]
> For more information, see [Enumerate and watch devices](enumerate-devices.md#enumerate-and-watch-devices) and [*DeviceWatcher*](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher).

## Step 5: Identify camera barcode scanners

A camera barcode scanner consists of a camera (attached to a computer) combined with a software decoder, which Windows dynamically pairs to create a fully functional [barcode scanner](pos-barcodescanner.md) for Universal Windows Platform (UWP) apps.

[*BarcodeScanner.VideoDeviceID*](/uwp/api/windows.devices.pointofservice.barcodescanner.videodeviceid#Windows_Devices_PointOfService_BarcodeScanner_VideoDeviceId) can be used to differentiate between camera barcode scanners and physical barcode scanners. A non-NULL VideoDeviceID indicates that the barcode scanner object from your device collection is a camera barcode scanner. If you have more than one camera barcode scanner you might want to build a separate collection that excludes physical barcode scanners.

Camera barcode scanners using the decoder that ships with Windows are identified as:

> Microsoft BarcodeScanner (*name of your camera here*)

If there is more than one camera, and they are built into the chassis of the computer, the name might differentiate between *front* and *rear* cameras.

When the [*DeviceWatcher*](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) starts (see [Step 4: Enumerate barcode scanners](#step-4-enumerate-barcode-scanners)), it enumerates through each connected device. In the following snippet, we add each available scanner to a [**BarcodeScanner**](/uwp/api/windows.devices.pointofservice.barcodescanner) collection and bind the collection to a [**ListBox**](/uwp/api/windows.ui.xaml.controls.listbox).

```csharp
ObservableCollection<BarcodeScanner> barcodeScanners = 
  new ObservableCollection<BarcodeScanner>();

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

When the [**SelectedIndex**](/uwp/api/windows.ui.xaml.controls.primitives.selector.selectedindex) of the [**ListBox**](/uwp/api/windows.ui.xaml.controls.listbox) changes (the first item is selected by default in the previous snippet), we query the device info (the `SelectScannerAsync` task is implemented in [Step 6: Claim the camera barcode scanner](#step-6-claim-the-camera-barcode-scanner)).

```csharp
private async void ScannerSelection_Changed(object sender, SelectionChangedEventArgs args)
{
    var selectedScannerInfo = (BarcodeScanner)args.AddedItems[0];
    var deviceId = selectedScannerInfo.DeviceId;

    await SelectScannerAsync(deviceId);
}
```

## Step 6: Claim the camera barcode scanner

Call [**BarcodeScanner.ClaimScannerAsync**](/uwp/api/windows.devices.pointofservice.barcodescanner.claimscannerasync#Windows_Devices_PointOfService_BarcodeScanner_ClaimScannerAsync) to obtain exclusive use of the camera barcode scanner.

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

A camera preview is required to help the user aim the camera at a barcode. Windows provides a basic camera preview that launches a dialog to control of the camera barcode scanner.

Call [**ClaimedBarcodeScanner.ShowVideoPreview**](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.showvideopreviewasync) to open the dialog and [**ClaimedBarcodeScanner.HideVideoPreview**](/uwp/api/windows.devices.pointofservice.claimedbarcodescanner.hidevideopreview) to close it.

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

- [JustScanIt - Windows Store app](https://aka.ms/justscanit)
- [BarcodeScanner sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BarcodeScanner)
