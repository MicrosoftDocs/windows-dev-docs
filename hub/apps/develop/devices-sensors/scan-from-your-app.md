---

title: Scan from your app
description: Learn here how to scan content from your app by using a flatbed, feeder, or auto-configured scan source.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Scan from your app

This topic describes how to scan content from your app by using a flatbed, feeder, or auto-configured scan source.

**Important APIs**

-   [**Windows.Devices.Scanners**](/uwp/api/Windows.Devices.Scanners)
-   [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation)
-   [**DeviceClass**](/uwp/api/Windows.Devices.Enumeration.DeviceClass)

To scan from your app, you must first list the available scanners by declaring a new [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object and getting the [**DeviceClass**](/uwp/api/Windows.Devices.Enumeration.DeviceClass) type. Only scanners that are installed locally with WIA drivers are listed and available to your app.

After your app has listed available scanners, it can use the auto-configured scan settings based on the scanner type, or just scan using the available flatbed or feeder scan source. To use auto-configured settings, the scanner must be enabled for auto-configuration and must not be equipped with both a flatbed and a feeder scanner. For more info, see [Auto-Configured Scanning](/windows-hardware/drivers/image/auto-configured-scanning).

## Enumerate available scanners

Windows does not detect scanners automatically. You must perform this step in order for your app to communicate with the scanner. In this example, the scanner device enumeration is done using the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) namespace.

1.  First, add these using statements to your class definition file.

``` csharp
    using Windows.Devices.Enumeration;
    using Windows.Devices.Scanners;
```

2.  Next, implement a device watcher to start enumerating scanners. For more info, see [Enumerate devices](/windows/uwp/devices-sensors/enumerate-devices).

```csharp
    void InitDeviceWatcher()
    {
       // Create a Device Watcher class for type Image Scanner for enumerating scanners
       scannerWatcher = DeviceInformation.CreateWatcher(DeviceClass.ImageScanner);

       scannerWatcher.Added += OnScannerAdded;
       scannerWatcher.Removed += OnScannerRemoved;
       scannerWatcher.EnumerationCompleted += OnScannerEnumerationComplete;
    }
```

3.  Create an event handler for when a scanner is added.

```csharp
    private async void OnScannerAdded(DeviceWatcher sender,  DeviceInformation deviceInfo)
    {
       await
       MainPage.Current.Dispatcher.RunAsync(
             Windows.UI.Core.CoreDispatcherPriority.Normal,
             () =>
             {
                MainPage.Current.NotifyUser(String.Format("Scanner with device id {0} has been added", deviceInfo.Id), NotifyType.StatusMessage);

                // search the device list for a device with a matching device id
                ScannerDataItem match = FindInList(deviceInfo.Id);

                // If we found a match then mark it as verified and return
                if (match != null)
                {
                   match.Matched = true;
                   return;
                }

                // Add the new element to the end of the list of devices
                AppendToList(deviceInfo);
             }
       );
    }
```

## Scan

1.  **Get an ImageScanner object**

For each [**ImageScannerScanSource**](/uwp/api/Windows.Devices.Scanners.ImageScannerScanSource) enumeration type, whether it's **Default**, **AutoConfigured**, **Flatbed**, or **Feeder**, you must first create an [**ImageScanner**](/uwp/api/Windows.Devices.Scanners.ImageScanner) object by calling the [**ImageScanner.FromIdAsync**](/uwp/api/windows.devices.scanners.imagescanner.fromidasync) method, like this.

 ```csharp
    ImageScanner myScanner = await ImageScanner.FromIdAsync(deviceId);
 ```

2.  **Just scan**

To scan with the default settings, your app relies on the [**Windows.Devices.Scanners**](/uwp/api/Windows.Devices.Scanners) namespace to select a scanner and scans from that source. No scan settings are changed. The possible scanners are auto-configure, flatbed, or feeder. This type of scan will most likely produce a successful scan operation, even if it scans from the wrong source, like flatbed instead of feeder.

**Note**  If the user places the document to scan in the feeder, the scanner will scan from the flatbed instead. If the user tries to scan from an empty feeder, the scan job won't produce any scanned files.
 
```csharp
    var result = await myScanner.ScanFilesToFolderAsync(ImageScannerScanSource.Default,
        folder).AsTask(cancellationToken.Token, progress);
```

3.  **Scan from Auto-configured, Flatbed, or Feeder source**

Your app can use the device's [Auto-Configured Scanning](/windows-hardware/drivers/image/auto-configured-scanning) to scan with the most optimal scan settings. With this option, the device itself can determine the best scan settings, like color mode and scan resolution, based on the content being scanned. The device selects the scan settings at run time for each new scan job.

**Note**  Not all scanners support this feature, so the app must check if the scanner supports this feature before using this setting.

In this example, the app first checks if the scanner is capable of auto-configuration and then scans. To specify either flatbed or feeder scanner, simply replace **AutoConfigured** with **Flatbed** or **Feeder**.

```csharp
    if (myScanner.IsScanSourceSupported(ImageScannerScanSource.AutoConfigured))
    {
        ...
        // Scan API call to start scanning with Auto-Configured settings.
        var result = await myScanner.ScanFilesToFolderAsync(
            ImageScannerScanSource.AutoConfigured, folder).AsTask(cancellationToken.Token, progress);
        ...
    }
```

## Preview the scan

You can add code to preview the scan before scanning to a folder. In the example below, the app checks if the **Flatbed** scanner supports preview, then previews the scan.

```csharp
if (myScanner.IsPreviewSupported(ImageScannerScanSource.Flatbed))
{
    rootPage.NotifyUser("Scanning", NotifyType.StatusMessage);
                // Scan API call to get preview from the flatbed.
                var result = await myScanner.ScanPreviewToStreamAsync(
                    ImageScannerScanSource.Flatbed, stream);
```

## Cancel the scan

You can let users cancel the scan job midway through a scan, like this.

```csharp
void CancelScanning()
{
    if (ModelDataContext.ScenarioRunning)
    {
        if (cancellationToken != null)
        {
            cancellationToken.Cancel();
        }                
        DisplayImage.Source = null;
        ModelDataContext.ScenarioRunning = false;
        ModelDataContext.ClearFileList();
    }
}
```

## Scan with progress

1.  Create a **System.Threading.CancellationTokenSource** object.

```csharp
cancellationToken = new CancellationTokenSource();
```

2.  Set up the progress event handler and get the progress of the scan.

```csharp
    rootPage.NotifyUser("Scanning", NotifyType.StatusMessage);
    var progress = new Progress<UInt32>(ScanProgress);
```

## Scanning to the pictures library

Users can scan to any folder dynamically using the [**FolderPicker**](/uwp/api/Windows.Storage.Pickers.FolderPicker) class, but you must declare the *Pictures Library* capability in the manifest to allow users to scan to that folder. For more info on app capabilities, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations).
