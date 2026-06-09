---
title: Scan from your app
description: Learn here how to scan content from your app by using a flatbed, feeder, or auto-configured scan source.
ms.date: 05/27/2026
ms.topic: how-to
ms.localizationpriority: medium
---

# Scan from your app

This topic describes how to scan content from your app by using a flatbed, feeder, or auto-configured scan source.

> [!div class="checklist"]
>
> - **Important APIs:** [Windows.Devices.Scanners](/uwp/api/Windows.Devices.Scanners), [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation), [DeviceClass](/uwp/api/Windows.Devices.Enumeration.DeviceClass)

## Get a scanner device

To scan from your app, you must first get an available scanner that is attached to the computer. Only scanners that are installed locally with [Windows Image Acquisition (WIA) drivers](/windows-hardware/drivers/image/windows-image-acquisition-drivers) are listed and available to your app.

To get an attached scanner, you can either use the system [DevicePicker](/uwp/api/windows.devices.enumeration.devicepicker) UI, or use other [Windows.Devices.Enumeration](/uwp/api/windows.devices.enumeration) APIs as shown in [Enumerate devices](enumerate-devices.md). In either case, you can use the [DeviceClass.ImageScanner](/uwp/api/windows.devices.enumeration.deviceclass) value as a filter so the DevicePicker or DeviceWatcher shows only image scanner devices.

The device enumeration APIs give you a [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) object that represents a device attached to the computer. After you have the DeviceInformation object for a scanner, you can use it to create an [ImageScanner](/uwp/api/windows.devices.scanners.imagescanner) object. The ImageScanner represents the scanner in your app code and gives you access to properties and methods you'll use for scanning.

### Pick an available scanner

In this example, you use the system [DevicePicker](/uwp/api/windows.devices.enumeration.devicepicker) to let the user choose an attached scanner.

```csharp
DeviceInformation scannerDeviceInfo;
ImageScanner selectedScannerDevice;

private async Task InitializeScannerDevice()
{
    scannerDeviceInfo = await PickScannerDevice();
    if (scannerDeviceInfo is not null)
    {
        selectedScannerDevice = await ImageScanner.FromIdAsync(scannerDeviceInfo.Id);
        StatusBar.Message = $"Scanner selected: {scannerDeviceInfo.Name}.";
    }
    else
    {
        // Either no scanner is attached, or the user cancelled the device picker.
        StatusBar.Message = $"No scanner is selected.";
    }
}

private async Task<DeviceInformation> PickScannerDevice()
{
    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
    DevicePicker devicePicker = new();
    // Initialize the device picker with the window handle (HWND).
    WinRT.Interop.InitializeWithWindow.Initialize(devicePicker, hWnd);

    // Filter to show only image scanner devices in the picker.
    devicePicker.Filter.SupportedDeviceClasses.Add(DeviceClass.ImageScanner);

    scannerDeviceInfo = await devicePicker.PickSingleDeviceAsync(new Rect());
    return scannerDeviceInfo;
}
```

### Enumerate available scanners

In this example, the scanner device enumeration is done using the [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) class from the [Windows.Devices.Enumeration](/uwp/api/windows.devices.enumeration) namespace. For more info, see [Enumerate devices](enumerate-devices.md).

1.  First, add these using statements to your class definition file.

``` csharp
    using Windows.Devices.Enumeration;
    using Windows.Devices.Scanners;
```

1.  Next, implement a device watcher to start enumerating scanners.

```csharp
ObservableCollection<DeviceInformation> scannerDeviceList = new();

private async void WatchScanners()
{
    scannerDeviceList.Clear();
    deviceWatcher = DeviceInformation.CreateWatcher(DeviceClass.ImageScanner);

    deviceWatcher.Added += DeviceWatcher_Added;
    deviceWatcher.Removed += DeviceWatcher_Removed;
    deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;

    if (deviceWatcher.Status == DeviceWatcherStatus.Created)
    {
        deviceWatcher.Start();
    }
}
```

1. Create an event handler for when the device enumeration is complete.

```csharp
private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
{
    DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        if (scannerDeviceList.Count == 0)
        {
            StatusBar.Message = "No scanners found. Please make sure that your scanner is on and properly connected.";
        }
        else
        {
            StatusBar.Message = "Enumeration of scanners is complete.";
        }
    });
}
```

1.  Create event handlers for when a scanner is added or removed.

```csharp
private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
{
    DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        scannerDeviceList.Remove(scannerDeviceList.Where(x => x.Id == args.Id).FirstOrDefault());
        StatusBar.Message = $"Scanner removed: {args.Id}";
    });
}

private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        scannerDeviceList.Add(args);
        StatusBar.Message = $"Scanner added: {args.Name}";
    });
}
```

## Scan

You use APIs from the [Windows.Devices.Scanners](/uwp/api/windows.devices.scanners) namespace to configure and scan from a scanner. The [ImageScanner](/uwp/api/windows.devices.scanners.imagescanner) object represents the scanner in your app code and gives you access to properties and methods you'll use for scanning. 

As shown in the previous section, you create an instance of ImageScanner by calling [ImageScanner.FromIdAsync](/uwp/api/windows.devices.scanners.imagescanner.fromidasync) and passing in the [DeviceInformation.Id](/uwp/api/windows.devices.enumeration.deviceinformation.id) of the scanner you want to use.

```csharp
ImageScanner selectedScannerDevice;
selectedScannerDevice = await ImageScanner.FromIdAsync(scannerDeviceInfo.Id);
```

To perform a scan, call [ImageScanner.ScanFileToFolderAsync](/uwp/api/windows.devices.scanners.imagescanner.scanfilestofolderasync) and specify the scan source and the destination folder. Here, a folder called `Scans` is created in the Pictures library as a destination for scan results.
 
```csharp
StorageFolder folder = await KnownFolders.PicturesLibrary.CreateFolderAsync("Scans", CreationCollisionOption.OpenIfExists);
                
ImageScannerScanResult result = await selectedScannerDevice.ScanFilesToFolderAsync(ImageScannerScanSource.Default,
        folder);
```

### Scan sources

A scanner can have a flatbed, a feeder, or both, as a scan source. Some scanners also support _auto-configured scanning_. These scan sources are identified by the [ImageScannerScanSource](/uwp/api/windows.devices.scanners.imagescannerscansource) enumeration and are represented by these classes:

- [ImageScannerFlatbedConfiguration](/uwp/api/windows.devices.scanners.imagescannerflatbedconfiguration) ([ImageScanner.FlatbedConfiguration](/uwp/api/windows.devices.scanners.imagescanner.flatbedconfiguration) property)
- [ImageScannerFeederConfiguration](/uwp/api/windows.devices.scanners.imagescannerfeederconfiguration) ([ImageScanner.FeederConfiguration](/uwp/api/windows.devices.scanners.imagescanner.feederconfiguration) property)
- [ImageScannerAutoConfiguration](/uwp/api/windows.devices.scanners.imagescannerautoconfiguration) ([ImageScanner.AutoConfiguration](/uwp/api/windows.devices.scanners.imagescanner.autoconfiguration) property)

You can use the scan source object to:

- Check whether the scan source is supported by the scanner.
    ```csharp
    selectedScannerDevice.IsScanSourceSupported(ImageScannerScanSource.AutoConfigured);
    ```
- Check whether the scan source supports preview scans.
    ```csharp
    selectedScannerDevice.IsPreviewSupported(ImageScannerScanSource.Feeder);
    ```
- Specify which source to scan from.
    ```csharp
    selectedScannerDevice.ScanFilesToFolderAsync(ImageScannerScanSource.Flatbed, folder);
    ```

> [!IMPORTANT]
> When you preview or scan from a source other than the Default, you should first check that preview scans and the specified scan source are supported.

You can also use the [ImageScannerFlatbedConfiguration](/uwp/api/windows.devices.scanners.imagescannerflatbedconfiguration) and [ImageScannerFeederConfiguration](/uwp/api/windows.devices.scanners.imagescannerfeederconfiguration) objects to read and set many properties of the corresponding scan source.

#### Default scan source

To get a scanner's default scan source, use the [ImageScanner.DefaultScanSource](/uwp/api/windows.devices.scanners.imagescanner.defaultscansource) property. You can also use the [ImageScannerScanSource.Default](/uwp/api/windows.devices.scanners.imagescannerscansource) enumeration value to specify that the default scan source be used.

For example, these two calls are equivalent:

```csharp
selectedScannerDevice.ScanFilesToFolderAsync(ImageScannerScanSource.Default, folder);
selectedScannerDevice.ScanFilesToFolderAsync(selectedScannerDevice.DefaultScanSource, folder);
```

#### Auto-configured scanning

To use auto-configured settings, the scanner must be enabled for auto-configuration and must not be equipped with both a flatbed and a feeder scanner. For more info, see [Auto-Configured Scanning](/windows-hardware/drivers/image/auto-configured-scanning).

Your app can use the device's [Auto-Configured Scanning](/windows-hardware/drivers/image/auto-configured-scanning) to scan with the most optimal scan settings. With this option, the device itself can determine the best scan settings, like color mode and scan resolution, based on the content being scanned. The device selects the scan settings at run time for each new scan job.

> [!NOTE]
> Not all scanners support this feature, so you must call [IsScanSourceSupported](/uwp/api/windows.devices.scanners.imagescanner.isscansourcesupported) to check if the scanner supports this feature before using this setting.

In this example, you first check if the scanner is capable of auto-configuration and only perform the scan if it is.

```csharp
if (selectedScannerDevice.IsScanSourceSupported(ImageScannerScanSource.AutoConfigured))
{
    // Scan API call to start scanning with Auto-Configured settings.
    ImageScannerScanResult result = await selectedScannerDevice.ScanFilesToFolderAsync(
        ImageScannerScanSource.AutoConfigured, folder);
}
```

## Preview the scan

If the scanner device supports it, you let the user preview the scan before scanning to a folder. In this example, the app checks if the `Feeder` scan source supports preview, then previews the scan if it does.

```csharp
if (selectedScannerDevice.IsPreviewSupported(ImageScannerScanSource.Feeder))
{
    IRandomAccessStream stream = new InMemoryRandomAccessStream();
    // Scan API call to get preview from the Feeder.
    ImageScannerScanResult result = 
        await selectedScannerDevice.ScanPreviewToStreamAsync(ImageScannerScanSource.Feeder, stream);
    if (result.Succeeded)
    {
        // ScanPreviewImage is a XAML Image control (not shown).
        SetImageSourceFromStream(stream, ScanPreviewImage);
        StatusBar.Message = "Preview scan is complete.";
    }
    else
    {
        StatusBar.Message = $"Failed to preview from Feeder.";
    }
}
else
{
    StatusBar.Message = $"The selected scanner does not support preview from Feeder.";
}
```

This method shows how to show the preview in your app UI.

```csharp
static async public void SetImageSourceFromStream(IRandomAccessStream stream, Image img)
{
    BitmapImage bitmap = await GetImageFromFile(stream);

    if ((bitmap.PixelHeight > img.Height) || (bitmap.PixelWidth > img.Width))
    {
        img.Stretch = Stretch.Uniform;
    }
    else
    {
        img.Stretch = Stretch.None;
    }
    img.Source = bitmap;
}
```

## Scanning to the pictures library

Users can scan to any folder dynamically using the [FolderPicker](/uwp/api/Windows.Storage.Pickers.FolderPicker) class, but you must declare the *Pictures Library* capability in the manifest to allow users to scan to that folder. For more info on app capabilities, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations).
 
 ## Related topics

- [Scanning sample (UWP)](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/Scan%20Runtime%20API%20Sample/%5BC%23%5D-Scan%20Runtime%20API%20Sample/C%23)
