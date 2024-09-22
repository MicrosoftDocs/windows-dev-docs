---
description: This article explains how apps should handle the Windows camera privacy setting.
title: Handle the Windows camera privacy setting
ms.date: 06/07/2024
ms.topic: article
keywords: windows 10, uwp
dev_langs:
- csharp
ms.localizationpriority: medium
---


# Handle the Windows camera privacy setting

Windows allows users to grant or deny access to the device's camera in the Windows Settings app, under **Privacy & Security -> Camera**. Camera access can be disabled for the entire device, for all unpackaged apps, or for individual packaged apps. This article describes the best practices for checking whether your app has access to the camera and handling the case where access is denied by the user.

## Check for access before initializing the camera

For packaged apps, you should check to see if your app has camera access before initializing the camera. Use the [AppCapability](/uwp/api/windows.security.authorization.appcapabilityaccess.appcapability) class to determine if your app has access.

### [C#](#tab/cs)

```csharp
bool cameraCapabilityAccess = false;
private void CheckCameraAccessStatus()
{
    var status = AppCapability.Create("Webcam").CheckAccess();
    
    if (status == AppCapabilityAccessStatus.Allowed)
    {
        cameraCapabilityAccess = true;
        cameraButton.IsEnabled = true;
    }
    else
    {
        cameraCapabilityAccess = false;
        cameraButton.IsEnabled = false;
    }
}
```

### [C++](#tab/cpp)

```cpp
bool cameraCapabilityAccess;
void MainWindow::CheckCameraAccessStatus()
{
    auto status = AppCapability::Create(L"Webcam").CheckAccess();

    if (status == AppCapabilityAccessStatus::Allowed)
    {
        cameraCapabilityAccess = true;
        cameraButton().IsEnabled(true);
    }
    else
    {
        cameraCapabilityAccess = false;
        cameraButton().IsEnabled(false);
    }
}
```
---

## Handle the access denied error

The Windows camera capture APIs will return the error **E_ACCESSDENIED** when apps attempt to access the camera capture device if the user has disabled camera in the camera privacy Settings page. Apps should check for this error when initializing the capture device. If the initialization fails with this error, it is recommended that you direct the user to the camera privacy Settings page and potentially enable access for your app. The camera privacy Settings page can be launched using the URI `ms-settings:privacy-webcam`.

The following example illustrates how to check for E_ACCESSDENIED when calling [MediaCapture.InitializeAsync](/uwp/api/windows.media.capture.mediacapture.initializeasync).

### [C#](#tab/cs)

```csharp
try
{
    await mediaCapture.InitializeAsync(mediaCaptureInitializationSettings);
}
catch (System.UnauthorizedAccessException ex)
{
    // E_ACCESSDENIED, 0x80070005 in hexadecimal, -2147024891 in decimal
    if (ex.HResult == -2147024891)
    {
        StatusTextBlock.Text = "Access to the camera has been denied." +
            "Click the Settings button to check the camera privacy settings";               
    }

    return;
}
...
// Launch the camera privacy Settings page
private async void LaunchSettingsButton_Click(object sender, RoutedEventArgs e)
{
    bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
}
```

### [C++](#tab/cpp)

```cpp
try
{
    co_await mediaCapture.InitializeAsync(mediaCaptureInitializationSettings);
}
catch (winrt::hresult_error const& ex)
{
    winrt::hresult hr = ex.code();
    if (hr == 0x80070005)
    {
        StatusTextBlock().Text(L"Access to the camera has been denied. Click the Settings button to check the camera privacy settings.");
	}
    
    co_return;
}
```

---

The following example illustrates handling the E_ACCESSDENIED error returned from [IMFActivate::ActivateObject](/windows/win32/api/mfobjects/nf-mfobjects-imfactivate-activateobject) when initializing an [IMFMediaSource](/windows/win32/api/mfidl/nn-mfidl-imfmediasource) for a capture device.

```cpp
IMFMediaSource* pSource = NULL;
IMFAttributes* pAttributes = NULL;
IMFActivate** ppDevices = NULL;

// Create an attribute store to specify the enumeration parameters.
HRESULT hr = MFCreateAttributes(&pAttributes, 1);
if (FAILED(hr))
{
    goto done;
}

// Source type: video capture devices
hr = pAttributes->SetGUID(
    MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE,
    MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID
);
if (FAILED(hr))
{
    goto done;
}

// Enumerate devices.
UINT32 count;
hr = MFEnumDeviceSources(pAttributes, &ppDevices, &count);
if (FAILED(hr))
{
    goto done;
}

if (count == 0)
{
    hr = E_FAIL;
    goto done;
}

// Create the media source object.
hr = ppDevices[0]->ActivateObject(IID_PPV_ARGS(&pSource));
if (FAILED(hr))
{
    if (hr == E_ACCESSDENIED)
    {
        int response = MessageBox(hWnd, L"Access to the camera was denied. Open the camera privacy settings?", L"Error", MB_YESNO);
        if (response == IDYES)
        {
            ShellExecute(NULL, L"open", L"ms-settings:privacy-webcam", L"", L".", SW_SHOWDEFAULT);
        }
    } 
    goto done;
}
```

## Implement fallback behavior

Apps should implement the previous steps to alert the user detect and alert the user that camera access is restricted due to privacy settings and to direct the user to the camera privacy Settings page to allow them to update their settings. After these steps, the app should retry camera initialization to see if access has been granted. If the user declines to update their settings to allow your app to access to the camera, consider providing alternative functionality. For example, you could disable camera features, switch to a different mode, or display a placeholder image in place of the camera preview.

