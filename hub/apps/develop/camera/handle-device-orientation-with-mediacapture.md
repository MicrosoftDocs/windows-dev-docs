---
title: Handle device orientation with MediaCapture
description: Learn how to handle device orientation when capturing photos and video with MediaCapture in a WinUI 3 desktop app using a rotation helper class.
ms.topic: how-to
ms.date: 07/10/2026
author: GrantMeStrength
ms.author: jken
---

# Handle device orientation with MediaCapture

When your app captures a photo or video for use outside the app, such as saving to a file or sharing, you need to encode the image with the correct orientation metadata so the content displays properly in other apps and devices. This article shows how to use a helper class to manage camera orientation in a WinUI 3 desktop app.

## Prerequisites

Before you begin, make sure you have:

- A WinUI 3 desktop project (packaged or unpackaged).
- A compatible camera connected to your device.
- Camera access declared for your app:
    - **Packaged apps**: Add the `webcam` device capability to `Package.appxmanifest`:

        ```xml
        <Capabilities>
            <DeviceCapability Name="webcam" />
        </Capabilities>
        ```

    - **Unpackaged apps**: There's no manifest capability to declare. Camera access is instead controlled by the **Let desktop apps access your camera** setting under **Settings** > **Privacy & security** > **Camera** on the user's device.

The user can still deny camera access at the OS level in either case. Check for access before you initialize the camera and handle the denied case gracefully, as described in [Handle the Windows camera privacy setting](/windows/apps/develop/camera/camera-privacy-setting).

## Orientation concepts

Desktop apps typically run on devices with fixed screens, so you don't need to handle continuous rotation the way a mobile app does. However, you still need to account for:

- **Camera sensor orientation** — The physical mounting angle of the camera sensor, which varies by device.
- **External camera rotation** — External webcams can be rotated by the user.

The key idea is to apply a rotation correction when encoding a captured photo or video so the output matches what the user sees in the preview.

## Create a CameraRotationHelper class

The following helper class manages rotation values based on the camera sensor orientation and device orientation sensors:

```csharp
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Media.Capture;
using Windows.Storage.FileProperties;

public class CameraRotationHelper
{
    private readonly EnclosureLocation _cameraEnclosureLocation;
    private readonly SimpleOrientationSensor _orientationSensor;
    private SimpleOrientation _deviceOrientation =
        SimpleOrientation.NotRotated;

    public event EventHandler<bool> OrientationChanged;

    public CameraRotationHelper(
        EnclosureLocation cameraEnclosureLocation)
    {
        _cameraEnclosureLocation = cameraEnclosureLocation;

        _orientationSensor =
            SimpleOrientationSensor.GetDefault();

        if (_orientationSensor != null)
        {
            _orientationSensor.OrientationChanged +=
                OrientationSensor_OrientationChanged;
        }
    }

    private void OrientationSensor_OrientationChanged(
        SimpleOrientationSensor sender,
        SimpleOrientationSensorOrientationChangedEventArgs args)
    {
        if (args.Orientation != SimpleOrientation.Faceup &&
            args.Orientation != SimpleOrientation.Facedown)
        {
            _deviceOrientation = args.Orientation;
            OrientationChanged?.Invoke(this, true);
        }
    }

    public static bool IsEnclosureLocationExternal(
        EnclosureLocation enclosureLocation)
    {
        return enclosureLocation == null ||
            enclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Unknown;
    }

    private bool IsCameraMirrored()
    {
        // Front panel cameras are mirrored by convention
        return _cameraEnclosureLocation?.Panel == Windows.Devices.Enumeration.Panel.Front;
    }

    private SimpleOrientation GetCameraOrientation()
    {
        if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
        {
            return SimpleOrientation.NotRotated;
        }

        // Get the sensor orientation from the device
        return _deviceOrientation;
    }

    /// <summary>
    /// Gets the rotation to apply to the camera preview stream.
    /// </summary>
    public VideoRotation GetCameraPreviewOrientation()
    {
        if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
        {
            return VideoRotation.None;
        }

        return ConvertSimpleOrientationToVideoRotation(
            GetCameraOrientation());
    }

    /// <summary>
    /// Gets the rotation to apply when encoding a photo.
    /// </summary>
    public PhotoOrientation GetCapturePhotoOrientation()
    {
        if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
        {
            return PhotoOrientation.Normal;
        }

        int encodingRotation = ConvertDeviceOrientationToDegrees(
            GetCameraOrientation());

        if (IsCameraMirrored())
        {
            encodingRotation = (360 - encodingRotation) % 360;
        }

        return ConvertDegreesToPhotoOrientation(encodingRotation);
    }

    /// <summary>
    /// Gets the clockwise rotation to apply when encoding a video.
    /// </summary>
    public int GetCaptureVideoOrientation()
    {
        if (IsEnclosureLocationExternal(_cameraEnclosureLocation))
        {
            return 0;
        }

        int rotation = ConvertDeviceOrientationToDegrees(
            GetCameraOrientation());

        if (IsCameraMirrored())
        {
            rotation = (360 - rotation) % 360;
        }

        return rotation;
    }

    public void Dispose()
    {
        if (_orientationSensor != null)
        {
            _orientationSensor.OrientationChanged -=
                OrientationSensor_OrientationChanged;
        }
    }

    private static int ConvertDeviceOrientationToDegrees(
        SimpleOrientation orientation)
    {
        // TODO: This mapping from counterclockwise SimpleOrientation values
        // to clockwise degree values (for example, mapping
        // Rotated90DegreesCounterclockwise to 90) was carried over from the
        // original UWP sample this article is based on. Verify this mapping
        // against physical devices before relying on it in production; do
        // not change these values without hardware verification.
        return orientation switch
        {
            SimpleOrientation.Rotated90DegreesCounterclockwise => 90,
            SimpleOrientation.Rotated180DegreesCounterclockwise => 180,
            SimpleOrientation.Rotated270DegreesCounterclockwise => 270,
            _ => 0,
        };
    }

    private static VideoRotation ConvertSimpleOrientationToVideoRotation(
        SimpleOrientation orientation)
    {
        // TODO: See the verification note on ConvertDeviceOrientationToDegrees
        // above — this CCW-to-CW mapping needs the same device verification
        // before the values are changed.
        return orientation switch
        {
            SimpleOrientation.Rotated90DegreesCounterclockwise =>
                VideoRotation.Clockwise90Degrees,
            SimpleOrientation.Rotated180DegreesCounterclockwise =>
                VideoRotation.Clockwise180Degrees,
            SimpleOrientation.Rotated270DegreesCounterclockwise =>
                VideoRotation.Clockwise270Degrees,
            _ => VideoRotation.None,
        };
    }

    private static PhotoOrientation ConvertDegreesToPhotoOrientation(
        int degrees)
    {
        return degrees switch
        {
            90 => PhotoOrientation.Rotate90,
            180 => PhotoOrientation.Rotate180,
            270 => PhotoOrientation.Rotate270,
            _ => PhotoOrientation.Normal,
        };
    }
}
```

## Use the helper class

Initialize the helper after you create your `MediaCapture` instance and know the camera's enclosure location:

```csharp
private CameraRotationHelper _rotationHelper;
private MediaCapture _mediaCapture;

private async Task InitializeCameraAsync()
{
    _mediaCapture = new MediaCapture();
    await _mediaCapture.InitializeAsync();

    var cameraDevice = _mediaCapture.MediaCaptureSettings;

    // Find the camera device info to get enclosure location
    var devices = await DeviceInformation.FindAllAsync(
        DeviceClass.VideoCapture);
    var deviceInfo = devices.FirstOrDefault(
        d => d.Id == cameraDevice.VideoDeviceId);

    _rotationHelper = new CameraRotationHelper(
        deviceInfo?.EnclosureLocation);

    _rotationHelper.OrientationChanged += (s, e) =>
    {
        // Update preview rotation when device orientation changes
        DispatcherQueue.TryEnqueue(UpdatePreviewRotation);
    };
}
```

## Apply rotation to the preview

Set the preview rotation on your `MediaCapture` instance when orientation changes:

```csharp
private void UpdatePreviewRotation()
{
    var rotation = _rotationHelper.GetCameraPreviewOrientation();
    _mediaCapture.SetPreviewRotation(rotation);
}
```

## Apply rotation when capturing a photo

Set the orientation metadata when you save a captured photo:

```csharp
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.FileProperties;

private async Task CapturePhotoWithOrientationAsync()
{
    var file = await ApplicationData.Current.LocalFolder
        .CreateFileAsync("photo.jpg",
            CreationCollisionOption.GenerateUniqueName);

    await _mediaCapture.CapturePhotoToStorageFileAsync(
        ImageEncodingProperties.CreateJpeg(), file);

    // Set the orientation metadata. ImageProperties.Orientation is
    // read-only, so save the EXIF orientation value directly through
    // the file's property store instead.
    var photoOrientation =
        _rotationHelper.GetCapturePhotoOrientation();

    var propertiesToSave = new List<KeyValuePair<string, object>>
    {
        new KeyValuePair<string, object>(
            "System.Photo.Orientation", photoOrientation)
    };
    await file.Properties.SavePropertiesAsync(propertiesToSave);
}
```

> [!IMPORTANT]
> `ApplicationData.Current.LocalFolder` requires package identity (MSIX). Unpackaged apps cannot use `ApplicationData` without package identity. For unpackaged apps, use `Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)` or another Win32 file path instead.

> [!NOTE]
> `SimpleOrientationSensor` is not available on all desktop devices. Check for a `null` return from `SimpleOrientationSensor.GetDefault()` and handle the case where no orientation sensor is present. For external cameras, rotation is typically `NotRotated`.

## Related content

- [Camera preview access](simple-camera-preview-access.md)
- [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
- [Camera](camera.md)
