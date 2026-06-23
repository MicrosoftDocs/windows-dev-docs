---
description: Learn how to implement Share on Windows - enable your app to send content to other Windows apps through the system Share Sheet.
title: "Share content from your app - integrate Windows Share"
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 06/22/2026
ms.localizationpriority: medium
---

# Share content from your app - integrate Windows Share

The Windows Share Sheet is a system-provided UI that lets users send content from your app to other Windows apps. This guide explains how to implement the Share contract across packaged apps (MSIX), Progressive Web Apps (PWAs), and unpackaged Win32 apps.

## In this article

| Section | What you'll find |
|--|--|
| [Choose your sharing approach](#choose-your-sharing-approach) | Pick the right API set for UWP, desktop, or PWA apps |
| [Implement Share for UWP apps](#implement-share-for-uwp-apps) | `DataTransferManager.GetForCurrentView` and `ShowShareUI` |
| [Implement Share for PWAs](#implement-share-for-progressive-web-apps-pwas) | Web Share API integration |
| [Implement Share for desktop apps](#implement-share-for-desktop-apps-winui-3-wpf-winforms) | `IDataTransferManagerInterop` per-window sharing for WinUI 3, WPF, WinForms |
| [Source-side events](#source-side-events) | Observe target selection, completion, and cancellation |
| [Best practices for Share](#best-practices-for-share) | Recommendations for reliable source-side behavior |

## Choose your sharing approach

| App type | Approach | API set |
|--|--|--|
| **UWP apps** | Use `DataTransferManager.GetForCurrentView` and `ShowShareUI` | Windows.ApplicationModel.DataTransfer |
| **Desktop apps (WinUI 3, WPF, WinForms)** | Use `IDataTransferManagerInterop` for per-window sharing (packaged or unpackaged) | Windows Runtime via COM interop |
| **Progressive Web Apps (PWAs)** | Use the Web Share API + Windows integration | W3C Web Share |

## Implement Share for UWP apps

> [!IMPORTANT]
> `DataTransferManager.GetForCurrentView` and `ShowShareUI` are supported only in UWP apps. Desktop apps (WinUI 3, WPF, or WinForms - packaged or unpackaged) must use the `IDataTransferManagerInterop` pattern shown in [Implement Share for desktop apps](#implement-share-for-desktop-apps-winui-3-wpf-winforms).

### 1. Get a DataTransferManager

In your page initialization, obtain a reference to the `DataTransferManager`:

```csharp
using Windows.ApplicationModel.DataTransfer;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();

        DataTransferManager dtm = DataTransferManager.GetForCurrentView();
        dtm.DataRequested += OnDataRequested;
    }
}
```

### 2. Populate a DataPackage

When the user initiates share (for example, clicks a Share button), create a `DataPackage` with the content and metadata:

```csharp
private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
{
    DataRequest request = args.Request;
    DataPackage data = request.Data;

    // Set a title (required)
    data.Properties.Title = "My shared content";

    // Set content - choose one or more:
    data.SetText("Here's some text to share");

    // For URLs, use SetWebLink to enable rich link previews:
    // data.SetWebLink(new Uri("https://example.com"));

    // For files or images:
    // IStorageItem item = await StorageFile.GetFileFromPathAsync(filePath);
    // data.SetStorageItems(new[] { item });

    // Optional: add description and thumbnail
    data.Properties.Description = "A brief description";
    // data.Properties.Thumbnail = /* RandomAccessStreamReference */;
}
```

> [!TIP]
> When you share a URL, use `SetWebLink` (or `SetApplicationLink` for deep links) instead of `SetText`. Target apps can then generate rich link previews and handle navigation correctly, instead of treating it as plain text.

### 3. Show the Share UI

Trigger the Share Sheet from a button click or menu command:

```csharp
private void ShareButton_Click(object sender, RoutedEventArgs e)
{
    // ShowShareUI is a static method on DataTransferManager.
    // The DataRequested handler was registered in step 1.
    DataTransferManager.ShowShareUI();
}
```

## Implement Share for Progressive Web Apps (PWAs)

PWAs use the W3C Web Share API. Make sure your PWA has the required manifest properties to integrate with Windows:

```json
{
  "name": "My PWA",
  "short_name": "MyPWA",
  "share_target": {
    "action": "/share",
    "method": "POST",
    "enctype": "multipart/form-data",
    "params": {
      "files": [
        {
          "name": "media",
          "accept": ["image/*"]
        }
      ]
    }
  }
}
```

In your PWA JavaScript, use the Web Share API:

```javascript
async function shareContent() {
  if (navigator.share) {
    try {
      await navigator.share({
        title: 'Check this out',
        text: 'Great content',
        url: 'https://example.com/page'
      });
    } catch (err) {
      if (err.name !== 'AbortError') {
        console.error('Share failed:', err);
      }
    }
  }
}
```

## Implement Share for desktop apps (WinUI 3, WPF, WinForms)

Desktop apps - whether packaged or unpackaged - use the `IDataTransferManagerInterop` interface to access the Share Sheet on a per-window basis. This applies to WinUI 3, WPF, and WinForms apps.

### 1. Declare the interop interface and acquire a DataTransferManager

```csharp
using Windows.ApplicationModel.DataTransfer;

[System.Runtime.InteropServices.ComImport]
[System.Runtime.InteropServices.Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
[System.Runtime.InteropServices.InterfaceType(
    System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
interface IDataTransferManagerInterop
{
    IntPtr GetForWindow([System.Runtime.InteropServices.In] IntPtr appWindow,
        [System.Runtime.InteropServices.In] ref Guid riid);
    void ShowShareUIForWindow(IntPtr appWindow);
}

public sealed partial class MainWindow // WinUI 3 Window, WPF Window, or WinForms Form
{
    // IID of DataTransferManager, passed as the riid to GetForWindow:
    static readonly Guid _dtm_iid =
        new Guid(0xa5caee9b, 0x8708, 0x49d1, 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c);

    private DataTransferManager _dtm;

    // Call this from your window or form constructor (or load handler):
    private void InitializeShare()
    {
        // Retrieve the window handle (HWND) for the current window:
        //   WinUI 3:  IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        //   WPF:      IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        //   WinForms: IntPtr hWnd = this.Handle;
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        IDataTransferManagerInterop interop =
            DataTransferManager.As<IDataTransferManagerInterop>();
        _dtm = WinRT.MarshalInterface<DataTransferManager>.FromAbi(
            interop.GetForWindow(hWnd, _dtm_iid));

        _dtm.DataRequested += (sender, args) => OnDataRequested(args);
    }
}
```

### 2. Populate and show

```csharp
private void OnDataRequested(DataRequestedEventArgs args)
{
    DataRequest request = args.Request;
    DataPackage data = request.Data;

    data.Properties.Title = "Share from my desktop app";
    data.SetText("Shared content");

    // For URLs:
    // data.SetWebLink(new Uri("https://example.com"));

    // For files:
    // var item = await StorageFile.GetFileFromPathAsync(filePath);
    // data.SetStorageItems(new[] { item });
}

// In your Share button handler:
private void ShareButton_Click()
{
    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
    var interop = DataTransferManager.As<IDataTransferManagerInterop>();
    interop.ShowShareUIForWindow(hWnd);
}
```

For a complete example, see the [WPF Share Source sample](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/ShareSource).

## Source-side events

Use these events in source apps to observe what happened after the user opens Share.

| API | When it fires | Why use it |
|--|--|--|
| `DataTransferManager.DataRequested` | The user starts a share operation | Build and attach the `DataPackage` |
| `DataTransferManager.TargetApplicationChosen` | The user chooses a target app | Optional telemetry for target selection |
| `DataPackage.ShareCompleted` | The share completes | Optional success telemetry |
| `DataPackage.ShareCanceled` | The user cancels the share | Optional cancellation telemetry |

> [!NOTE]
> This example uses `GetForCurrentView` for brevity, which applies to UWP apps. In desktop apps, acquire the `DataTransferManager` through `IDataTransferManagerInterop.GetForWindow` as shown earlier, then attach the same events.

```csharp
private void RegisterShareEvents()
{
  var dtm = DataTransferManager.GetForCurrentView();
  dtm.DataRequested += OnDataRequested;
  dtm.TargetApplicationChosen += OnTargetChosen;
}

private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
{
  DataRequest request = args.Request;
  request.Data.Properties.Title = "Share from my app";
  request.Data.SetText("Hello from Windows Share");

  request.Data.ShareCompleted += OnShareCompleted;
  request.Data.ShareCanceled += OnShareCanceled;
}

private void OnTargetChosen(DataTransferManager sender, TargetApplicationChosenEventArgs args)
{
  // Optional: telemetry only
  Debug.WriteLine($"Target app: {args.ApplicationName}");
}

private void OnShareCompleted(DataPackage sender, ShareCompletedEventArgs args)
{
  Debug.WriteLine("Share completed");
}

private void OnShareCanceled(DataPackage sender, object args)
{
  Debug.WriteLine("Share canceled");
}
```

> [!NOTE]
> `DataPackage.OperationCompleted` and `DataPackage.Destroyed` are primarily for Clipboard and paste workflows. They are generally not needed for Share source scenarios.

## Best practices for Share

Use this checklist to keep source-side behavior predictable.

| Recommended | Avoid | Why it matters |
|--|--|--|
| Use `SetWebLink` or `SetApplicationLink` for URLs | Use `SetText` for URLs | Links render and route correctly in target apps |
| Set `Title` and optional metadata (`Description`, thumbnail) | Sending content without metadata | Improves Share UI clarity and target rendering |
| Handle `TargetApplicationChosen`, `ShareCompleted`, and `ShareCanceled` if you need telemetry | Assuming these signals come from `ShareOperation` in source apps | These are source-side signals for post-share insight |
| Keep shared payloads focused and valid for the selected action | Sending unrelated or oversized payloads by default | Reduces failures and improves share success rate |

## Related content

- [Receive content through Windows Share](integrate-sharesheet-receive.md)
- [DataFormat & FileType reference](dataformat-reference.md)
- [People on Windows (Cross-device People API)](cross-device-people-api.md)
- [DataTransferManager class](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager)
- [DataPackage class](/uwp/api/windows.applicationmodel.datatransfer.datapackage)
- [W3C Web Share API](https://www.w3.org/TR/web-share/)
- [IDataTransferManagerInterop (COM interface)](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop)
