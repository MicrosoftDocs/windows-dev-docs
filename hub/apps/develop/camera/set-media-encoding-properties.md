---
description: Learn how to use the IMediaEncodingProperties interface to set the resolution and frame rate of the camera preview stream and captured photos and video in a WinUI app.
title: Set format, resolution, and frame rate for MediaCapture
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, windows 11, winui3, camera
ms.localizationpriority: medium
---
# Set format, resolution, and frame rate for MediaCapture in a WinUI app

This article shows you how to use the [**IMediaEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.IMediaEncodingProperties) interface to set the resolution and frame rate of the camera preview stream and captured photos and video. It also shows how to ensure that the aspect ratio of the preview stream matches that of the captured media.

Camera profiles offer a simpler, higher level mechanism for discovering and setting the stream properties of the camera, but they are not supported for all devices. For more information, see [Camera profiles](camera-profiles.md).

## Determine if the preview and capture streams are independent

On some devices, the same hardware pin is used for both preview and capture streams. On these devices, setting the encoding properties such as the format, resolution and frame rate on one will affect both. On devices that use different hardware pins for capture and preview, the properties can be set for each stream independently. Use the following code to determine if the preview and capture streams are independent. This example sets a boolean global variable that can be used to switch the app's behavior if the streams are shared or independent.

```csharp
if (m_mediaCapture.MediaCaptureSettings.VideoDeviceCharacteristic == VideoDeviceCharacteristic.AllStreamsIdentical ||
    m_mediaCapture.MediaCaptureSettings.VideoDeviceCharacteristic == VideoDeviceCharacteristic.PreviewRecordStreamsIdentical)
{
    m_captureAndPreviewStreamsIdentical = true;
}
```

## A media encoding properties helper class

Creating a simple helper class to wrap the functionality of the [**IMediaEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.IMediaEncodingProperties) interface makes it easier to select a set of encoding properties that meet particular criteria. This helper class is particularly useful due to the following behavior of the encoding properties feature:

> [!NOTE]
> The [**VideoDeviceController.GetAvailableMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getavailablemediastreamproperties) method takes a member of the [**MediaStreamType**](/uwp/api/Windows.Media.Capture.MediaStreamType) enumeration, such as **VideoRecord** or **Photo**, and returns a list of either [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) or [**VideoEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.VideoEncodingProperties) objects that convey the stream encoding settings, such as the resolution of the captured photo or video. The results of calling **GetAvailableMediaStreamProperties** may include **ImageEncodingProperties** or **VideoEncodingProperties** regardless of what **MediaStreamType** value is specified. For this reason, you should always check the type of each returned value and cast it to the appropriate type before attempting to access any of the property values.

The helper class defined below handles the type checking and casting for [**ImageEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.ImageEncodingProperties) or [**VideoEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.VideoEncodingProperties) so that your app code doesn't need to distinguish between the two types. In addition to this, the helper class exposes properties for the aspect ratio of the properties, the frame rate (for video encoding properties only), and a friendly name that makes it easier to display the encoding properties in your UI.

```csharp
class StreamPropertiesHelper
{
    private IMediaEncodingProperties _properties;

    public StreamPropertiesHelper(IMediaEncodingProperties properties)
    {
        if (properties == null)
        {
            throw new ArgumentNullException(nameof(properties));
        }

        // This helper class only uses ImageEncodingProperties or VideoEncodingProperties
        if (!(properties is ImageEncodingProperties) && !(properties is VideoEncodingProperties))
        {
            throw new ArgumentException("Argument is of the wrong type. Required: " + typeof(ImageEncodingProperties).Name
                + " or " + typeof(VideoEncodingProperties).Name + ".", nameof(properties));
        }

        // Store the actual instance of the IMediaEncodingProperties for setting them later
        _properties = properties;
    }

    public uint Width
    {
        get
        {
            if (_properties is ImageEncodingProperties)
            {
                return (_properties as ImageEncodingProperties).Width;
            }
            else if (_properties is VideoEncodingProperties)
            {
                return (_properties as VideoEncodingProperties).Width;
            }

            return 0;
        }
    }

    public uint Height
    {
        get
        {
            if (_properties is ImageEncodingProperties)
            {
                return (_properties as ImageEncodingProperties).Height;
            }
            else if (_properties is VideoEncodingProperties)
            {
                return (_properties as VideoEncodingProperties).Height;
            }

            return 0;
        }
    }

    public uint FrameRate
    {
        get
        {
            if (_properties is VideoEncodingProperties)
            {
                if ((_properties as VideoEncodingProperties).FrameRate.Denominator != 0)
                {
                    return (_properties as VideoEncodingProperties).FrameRate.Numerator /
                        (_properties as VideoEncodingProperties).FrameRate.Denominator;
                }
            }

            return 0;
        }
    }

    public double AspectRatio
    {
        get { return Math.Round((Height != 0) ? (Width / (double)Height) : double.NaN, 2); }
    }

    public IMediaEncodingProperties EncodingProperties
    {
        get { return _properties; }
    }

    public string GetFriendlyName(bool showFrameRate = true)
    {
        if (_properties is ImageEncodingProperties ||
            !showFrameRate)
        {
            return Width + "x" + Height + " [" + AspectRatio + "] " + _properties.Subtype;
        }
        else if (_properties is VideoEncodingProperties)
        {
            return Width + "x" + Height + " [" + AspectRatio + "] " + FrameRate + "FPS " + _properties.Subtype;
        }

        return String.Empty;
    }

}
```

## Get a list of available stream properties

Get a list of the available stream properties for a capture device by getting the [**VideoDeviceController**](/uwp/api/Windows.Media.Devices.VideoDeviceController) for your app's [**MediaCapture**](/uwp/api/Windows.Media.Capture.MediaCapture) object and then calling [**GetAvailableMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getavailablemediastreamproperties) and passing in one of the [**MediaStreamType**](/uwp/api/Windows.Media.Capture.MediaStreamType) values, **VideoPreview**, **VideoRecord**, or **Photo**. In this example, a list of **StreamPropertiesHelper** objects, defined previously in this article, is created for each of the [**IMediaEncodingProperties**](/uwp/api/Windows.Media.MediaProperties.IMediaEncodingProperties) values returned from **GetAvailableMediaStreamProperties**. This example orders the returned properties based first on resolution and then on frame rate.

If your app has specific resolution or frame rate requirements, you can select a set of media encoding properties programmatically. A typical camera app will instead expose the list of available properties in the UI and allow the user to select their desired settings. A **ComboBoxItem** is created for each item in the list of **StreamPropertiesHelper** objects in the list. The content is set to the friendly name returned by the helper class and the tag is set to the helper class itself so it can be used later to retrieve the associated encoding properties. Each **ComboBoxItem** is then added to a **ComboBox** defined in the UI.

```csharp
private void bGetStreamProperties_Click(object sender, RoutedEventArgs e)
{
    // Query all properties of the specified stream type
    IEnumerable<StreamPropertiesHelper> allStreamProperties =
        m_mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoRecord).Select(x => new StreamPropertiesHelper(x));

    // Order them by resolution then frame rate
    allStreamProperties = allStreamProperties.OrderByDescending(x => x.Height * x.Width).ThenByDescending(x => x.FrameRate);

    // Populate the combo box with the entries
    foreach (var property in allStreamProperties)
    {
        ComboBoxItem comboBoxItem = new ComboBoxItem();
        comboBoxItem.Content = property.GetFriendlyName();
        comboBoxItem.Tag = property;
        cbStreamProperties.Items.Add(comboBoxItem);
    }
}
```

## Set the desired stream properties

Tell the video device controller to use your desired encoding properties by calling [**SetMediaStreamPropertiesAsync**](/uwp/api/windows.media.devices.videodevicecontroller.setmediastreampropertiesasync), passing in the **MediaStreamType** value indicating whether the photo, video, or preview properties should be set. This example uses the **ComboBox** populated in the example from the previous section, where the media stream properties are retrieved from the tag property from the selected item.

```csharp
private async void bSetStreamProperties_Click(object sender, RoutedEventArgs e)
{

    if (m_exclusiveCameraAccess)
    {
        var selectedItem = cbStreamProperties.SelectedItem as ComboBoxItem;
        var encodingProperties = (selectedItem.Tag as StreamPropertiesHelper).EncodingProperties;
        await m_mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, encodingProperties);
    }
}
```

Note that your app must have exclusive control of the capture device in order to change the media stream properties.

## Match the aspect ratio of the preview and capture streams

A typical camera app will provide UI for the user to select the video or photo capture resolution but will programmatically set the preview resolution. There are a few different strategies for selecting the best preview stream resolution for your app:

- Select the highest available preview resolution, letting the UI framework perform any necessary scaling of the preview.

- Select the preview resolution closest to the capture resolution so that the preview displays the closest representation to the final captured media.

- Select the preview resolution closest to the size of the [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) so that no more pixels than necessary are going through the preview stream pipeline.

> [!NOTE]
> It is possible, on some devices, to set a different aspect ratio for the camera's preview stream and capture stream. Frame cropping caused by this mismatch can result in content being present in the captured media that was not visible in the preview which can result in a negative user experience. It is strongly recommended that you use the same aspect ratio, within a small tolerance window, for the preview and capture streams. It is fine to have entirely different resolutions enabled for capture and preview as long as the aspect ratio match closely.


To ensure that the photo or video capture streams match the aspect ratio of the preview stream, this example calls [**VideoDeviceController.GetMediaStreamProperties**](/uwp/api/windows.media.devices.videodevicecontroller.getmediastreamproperties) and passes in the **VideoPreview** enum value to request the current stream properties for the preview stream. Next a small aspect ratio tolerance window is defined so that we can include aspect ratios that are not exactly the same as the preview stream, as long as they are close. Next, the **StreamPropertiesHelper** objects where the aspect ratio is within the defined tolerance range of the preview stream are selected.

```csharp
// Query all properties of the specified stream type
IEnumerable<StreamPropertiesHelper> allVideoProperties =
    m_mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoRecord).Select(x => new StreamPropertiesHelper(x));

// Query the current preview settings
StreamPropertiesHelper previewProperties = new StreamPropertiesHelper(m_mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview));

// Get all formats that have the same-ish aspect ratio as the preview
// Allow for some tolerance in the aspect ratio comparison
const double ASPECT_RATIO_TOLERANCE = 0.015;
var matchingFormats = allVideoProperties.Where(x => Math.Abs(x.AspectRatio - previewProperties.AspectRatio) < ASPECT_RATIO_TOLERANCE);

// Order them by resolution then frame rate
allVideoProperties = matchingFormats.OrderByDescending(x => x.Height * x.Width).ThenByDescending(x => x.FrameRate);

// Clear out old entries and populate the video combo box with new matching entries
cbStreamProperties.Items.Clear();
foreach (var property in allVideoProperties)
{
    ComboBoxItem comboBoxItem = new ComboBoxItem();
    comboBoxItem.Content = property.GetFriendlyName();
    comboBoxItem.Tag = property;
    cbStreamProperties.Items.Add(comboBoxItem);
}
```

SnippetMatchPreviewAspectRatio