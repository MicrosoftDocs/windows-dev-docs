---
description: Learn how to use manual device controls to enable enhanced photo and video capture scenarios including optical image stabilization and smooth zoom.
title: Manual camera controls for photo and video capture
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, winui 3
ms.localizationpriority: medium
---

# Manual camera controls for photo and video capture

This article shows you how to use manual device controls to enable enhanced photo and video capture scenarios including optical image stabilization and smooth zoom.

The controls discussed in this article are all added to your app using the same pattern. First, check to see if the control is supported on the current device on which your app is running. If the control is supported, set the desired mode for the control. Typically, if a particular control is unsupported on the current device, you should disable or hide the UI element that allows the user to enable the feature.

> [!NOTE]
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of **MediaCapture** that has been properly initialized.

## Exposure

The [**ExposureControl**](/uwp/api/Windows.Media.Devices.ExposureControl) allows you to set the shutter speed used during photo or video capture.

This example uses a [**Slider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.slider) control to adjust the current exposure value and a checkbox to toggle automatic exposure adjustment.

```xml
<Slider Name="slExposure" ValueChanged="slExposure_ValueChanged"/>
<TextBlock Name="tbExposure" Text="{Binding ElementName=slExposure,Path=Value}"/>
<CheckBox Name="cbExposureAuto" Content="Auto" Checked="cbExposure_CheckedChanged" Unchecked="cbExposure_CheckedChanged"/>
```

Check to see if the current capture device supports the **ExposureControl** by checking the [**Supported**](/uwp/api/windows.media.devices.exposurecontrol.supported) property. If the control is supported, you can show and enable the UI for this feature. Set the checked state of the checkbox to indicate if automatic exposure adjustment is currently active to the value of the [**Auto**](/uwp/api/windows.media.devices.exposurecontrol.auto) property.

The exposure value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](/uwp/api/windows.media.devices.exposurecontrol.min), [**Max**](/uwp/api/windows.media.devices.exposurecontrol.max), and [**Step**](/uwp/api/windows.media.devices.exposurecontrol.step) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **ExposureControl** after unregistering the [**ValueChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.rangebase.valuechanged) event handler so that the event is not triggered when the value is set.

```csharp
var exposureControl = m_mediaCapture.VideoDeviceController.ExposureControl;

if (exposureControl.Supported)
{
    cbExposureAuto.Visibility = Visibility.Visible;
    slExposure.Visibility = Visibility.Visible;

    cbExposureAuto.IsChecked = exposureControl.Auto;

    slExposure.Minimum = exposureControl.Min.Ticks;
    slExposure.Maximum = exposureControl.Max.Ticks;
    slExposure.StepFrequency = exposureControl.Step.Ticks;

    slExposure.ValueChanged -= slExposure_ValueChanged;
    var value = exposureControl.Value;
    slExposure.Value = value.Ticks;
    slExposure.ValueChanged += slExposure_ValueChanged;
}
else
{
    cbExposureAuto.Visibility = Visibility.Collapsed;
    slExposure.Visibility = Visibility.Collapsed;
}
```

In the **ValueChanged** event handler, get the current value of the control and the set the exposure value by calling [**SetValueAsync**](/uwp/api/windows.media.devices.exposurecontrol.setvalueasync).

```csharp
private async void slExposure_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
{
    var value = TimeSpan.FromTicks((long)(sender as Slider).Value);
    await m_mediaCapture.VideoDeviceController.ExposureControl.SetValueAsync(value);
}
```

In the **CheckedChanged** event handler of the auto exposure checkbox, turn automatic exposure adjustment on or off by calling [**SetAutoAsync**](/uwp/api/windows.media.devices.exposurecontrol.setautoasync) and passing in a boolean value.

```csharp
private async void cbExposure_CheckedChanged(object sender, RoutedEventArgs e)
{
    if (!m_isPreviewing)
    {
        // Auto exposure only supported while preview stream is running.
        return;
    }

    var autoExposure = ((sender as CheckBox).IsChecked == true);
    await m_mediaCapture.VideoDeviceController.ExposureControl.SetAutoAsync(autoExposure);
}
```

> [!IMPORTANT]
> Automatic exposure mode is only supported while the preview stream is running. Check to make sure that the preview stream is running before turning on automatic exposure.

## Exposure compensation

The [**ExposureCompensationControl**](/uwp/api/Windows.Media.Devices.ExposureCompensationControl) allows you to set the exposure compensation used during photo or video capture.

This example uses a [**Slider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.slider) control to adjust the current exposure compensation value.

```xml
<Slider Name="slEV" ValueChanged="slEV_ValueChanged"/>
<TextBlock Text="{Binding ElementName=slEV,Path=Value}" Name="tbEV"/>
```

Check to see if the current capture device supports the **ExposureCompensationControl** by checking the [Supported](/uwp/api/windows.media.devices.exposurecompensationcontrol.supported) property. If the control is supported, you can show and enable the UI for this feature.

The exposure compensation value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](/uwp/api/windows.media.devices.exposurecompensationcontrol.min), [**Max**](/uwp/api/windows.media.devices.exposurecompensationcontrol.max), and [**Step**](/uwp/api/windows.media.devices.exposurecompensationcontrol.step) properties, which are used to set the corresponding properties of the slider control.

Set slider control's value to the current value of the **ExposureCompensationControl** after unregistering the [**ValueChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.rangebase.valuechanged) event handler so that the event is not triggered when the value is set.

```csharp
var exposureCompensationControl = m_mediaCapture.VideoDeviceController.ExposureCompensationControl;

if (exposureCompensationControl.Supported)
{
    slEV.Visibility = Visibility.Visible;
    slEV.Minimum = exposureCompensationControl.Min;
    slEV.Maximum = exposureCompensationControl.Max;
    slEV.StepFrequency = exposureCompensationControl.Step;

    slEV.ValueChanged -= slEV_ValueChanged;
    slEV.Value = exposureCompensationControl.Value;
    slEV.ValueChanged += slEV_ValueChanged;
}
else
{
    slEV.Visibility = Visibility.Collapsed;
}
```

In the **ValueChanged** event handler, get the current value of the control and the set the exposure value by calling [**SetValueAsync**](/uwp/api/windows.media.devices.exposurecompensationcontrol.setvalueasync).

```csharp
private async void slEV_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
{
    var value = (sender as Slider).Value;
    await m_mediaCapture.VideoDeviceController.ExposureCompensationControl.SetValueAsync((float)value);
}
```

## Flash

The [**FlashControl**](/uwp/api/Windows.Media.Devices.FlashControl) allows you to enable or disable the flash or to enable automatic flash, where the system dynamically determines whether to use the flash. This control also allows you to enable automatic red eye reduction on devices that support it. These settings all apply to capturing photos. The [**TorchControl**](/uwp/api/Windows.Media.Devices.TorchControl) is a separate control for turning the torch on or off for video capture.

This example uses a set of radio buttons to allow the user to switch between on, off, and auto flash settings. A checkbox is also provided to allow toggling of red eye reduction and the video torch.

```xml
<RadioButton Name="rbFlashOn" Content="On" Checked="rbFlashOn_Checked"/>
<RadioButton Name="rbFlashAuto" Content="Auto" Checked="rbFlashAuto_Checked"/>
<RadioButton Name="rbFlashOff" Content="Off" Checked="rbFlashOff_Checked"/>
<CheckBox Name="cbRedEyeFlash" Content="Red Eye" Visibility="Collapsed" Checked="cbRedEyeFlash_CheckedChanged" Unchecked="cbRedEyeFlash_CheckedChanged"/>
<CheckBox Name="cbTorch" Content="Video Light" Visibility="Collapsed" Checked="cbTorch_CheckedChanged" Unchecked="cbTorch_CheckedChanged"/>
```

Check to see if the current capture device supports the **FlashControl** by checking the [**Supported**](/uwp/api/windows.media.devices.flashcontrol.supported) property. If the control is supported, you can show and enable the UI for this feature. If the **FlashControl** is supported, automatic red eye reduction may or may not be supported, so check the [**RedEyeReductionSupported**](/uwp/api/windows.media.devices.flashcontrol.redeyereductionsupported) property before enabling the UI. Because the **TorchControl** is separate from the flash control, you must also check its [**Supported**](/uwp/api/windows.media.devices.torchcontrol.supported) property before using it.

In the [**Checked**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.togglebutton.checked) event handler for each of the flash radio buttons, enable or disable the appropriate corresponding flash setting. Note that to set the flash to always be used, you must set the [**Enabled**](/uwp/api/windows.media.devices.flashcontrol.enabled) property to true and the [**Auto**](/uwp/api/windows.media.devices.flashcontrol.auto) property to false.

```csharp
var flashControl = m_mediaCapture.VideoDeviceController.FlashControl;

if (flashControl.Supported)
{
    rbFlashAuto.Visibility = Visibility.Visible;
    rbFlashOn.Visibility = Visibility.Visible;
    rbFlashOff.Visibility = Visibility.Visible;

    rbFlashAuto.IsChecked = true;

    if (flashControl.RedEyeReductionSupported)
    {
        cbRedEyeFlash.Visibility = Visibility.Visible;
    }

    // Video light is not strictly part of flash, but users might expect to find it there
    if (m_mediaCapture.VideoDeviceController.TorchControl.Supported)
    {
        cbTorch.Visibility = Visibility.Visible;
    }
}
else
{
    rbFlashAuto.Visibility = Visibility.Collapsed;
    rbFlashOn.Visibility = Visibility.Collapsed;
    rbFlashOff.Visibility = Visibility.Collapsed;
}

```

```csharp
private void rbFlashOn_Checked(object sender, RoutedEventArgs e)
{
    m_mediaCapture.VideoDeviceController.FlashControl.Enabled = true;
    m_mediaCapture.VideoDeviceController.FlashControl.Auto = false;
}

private void rbFlashAuto_Checked(object sender, RoutedEventArgs e)
{
    m_mediaCapture.VideoDeviceController.FlashControl.Enabled = true;
    m_mediaCapture.VideoDeviceController.FlashControl.Auto = true;
}

private void rbFlashOff_Checked(object sender, RoutedEventArgs e)
{
    m_mediaCapture.VideoDeviceController.FlashControl.Enabled = false;
}
```

In the handler for the red eye reduction checkbox, set the [**RedEyeReduction**](/uwp/api/windows.media.devices.flashcontrol.redeyereduction) property to the appropriate value.

```csharp

//private void cbRedEyeFlash_CheckedChanged(object sender, RoutedEventArgs e)
private void cbRedEyeFlash_CheckedChanged(object sender, RoutedEventArgs e)
{
    m_mediaCapture.VideoDeviceController.FlashControl.RedEyeReduction = (cbRedEyeFlash.IsChecked == true);
}
```

Finally, in the handler for the video torch checkbox, set the [**Enabled**](/uwp/api/windows.media.devices.torchcontrol.enabled) property to the appropriate value.

```csharp
private void cbTorch_CheckedChanged(object sender, RoutedEventArgs e)
{
    m_mediaCapture.VideoDeviceController.TorchControl.Enabled = (cbTorch.IsChecked == true);

    if(! (m_isPreviewing && m_isRecording))
    {
        System.Diagnostics.Debug.WriteLine("Torch may not emit light if preview and video capture are not running.");
    }
}
```

> [!NOTE] 
>  On some devices the torch will not emit light, even if [**TorchControl.Enabled**](/uwp/api/windows.media.devices.torchcontrol.enabled) is set to true, unless the device has a preview stream running and is actively capturing video. The recommended order of operations is to turn on the video preview, turn on the torch by setting **Enabled** to true, and then initiate video capture. On some devices the torch will light up after the preview is started. On other devices, the torch may not light up until video capture is started.

## Focus

Three different commonly used methods for adjusting the focus of the camera are supported by the [**FocusControl**](/uwp/api/Windows.Media.Devices.FocusControl) object, continuous autofocus, tap to focus, and manual focus. A camera app may support all three of these methods, but for readability, this article discusses each technique separately. This section also discusses how to enable the focus assist light.

### Continuous autofocus

Enabling continuous autofocus instructs the camera to adjust the focus dynamically to try to keep the subject of the photo or video in focus. This example uses a radio button to toggle continuous autofocus on and off.

```xml
<RadioButton Content="CAF" Name="rbContinuousAutoFocus" Checked="rbContinuousAutoFocus_Checked"/>
```

Check to see if the current capture device supports the **FocusControl** by checking the [**Supported**](/uwp/api/windows.media.devices.focuscontrol.supported) property. Next, determine if continuous autofocus is supported by checking the [**SupportedFocusModes**](/uwp/api/windows.media.devices.focuscontrol.supportedfocusmodes) list to see if it contains the value [**FocusMode.Continuous**](/uwp/api/Windows.Media.Devices.FocusMode), and if so, show the continuous autofocus radio button.

```csharp
var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;

if (focusControl.Supported)
{
    rbContinuousAutoFocus.Visibility = focusControl.SupportedFocusModes.Contains(FocusMode.Continuous)
        ? Visibility.Visible : Visibility.Collapsed;
}
else
{
    rbContinuousAutoFocus.Visibility = Visibility.Collapsed;
}
```

In the [**Checked**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.togglebutton.checked) event handler for the continuous autofocus radio button, use the [**VideoDeviceController.FocusControl**](/uwp/api/windows.media.devices.videodevicecontroller.focuscontrol) property to get an instance of the control. Call [**UnlockAsync**](/uwp/api/windows.media.devices.focuscontrol.unlockasync) to unlock the control in case your app has previously called [**LockAsync**](/uwp/api/windows.media.devices.focuscontrol.lockasync) to enable one of the other focus modes.

Create a new [**FocusSettings**](/uwp/api/Windows.Media.Devices.FocusSettings) object and set the [**Mode**](/uwp/api/windows.media.devices.focussettings.mode) property to **Continuous**. Set the [**AutoFocusRange**](/uwp/api/windows.media.devices.focussettings.autofocusrange) property to a value appropriate for your app scenario or selected by the user from your UI. Pass your **FocusSettings** object into the [**Configure**](/uwp/api/windows.media.devices.focuscontrol.configure) method, and then call [**FocusAsync**](/uwp/api/windows.media.devices.focuscontrol.focusasync) to initiate continuous autofocus.

```csharp
private async void rbContinuousAutoFocus_Checked(object sender, RoutedEventArgs e)
{
    if(! m_isPreviewing)
    {
        // Autofocus only supported while preview stream is running.
        return;
    }

    var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;
    await focusControl.UnlockAsync();
    var settings = new FocusSettings { Mode = FocusMode.Continuous, AutoFocusRange = AutoFocusRange.FullRange };
    focusControl.Configure(settings);
    await focusControl.FocusAsync();
}
```

> [!IMPORTANT]
> Autofocus mode is only supported while the preview stream is running. Check to make sure that the preview stream is running before turning on continuous autofocus.

### Tap to focus

The tap-to-focus technique uses the [**FocusControl**](/uwp/api/Windows.Media.Devices.FocusControl) and the [**RegionsOfInterestControl**](/uwp/api/Windows.Media.Devices.RegionsOfInterestControl) to specify a subregion of the capture frame where the capture device should focus. The region of focus is determined by the user tapping on the screen displaying the preview stream.

This example uses a radio button to enable and disable tap-to-focus mode.

```xml
<RadioButton Content="rbTapToFocus" Name="TapFocusRadioButton" Checked="rbTapToFocus_Checked"/>
```

Check to see if the current capture device supports the **FocusControl** by checking the [**Supported**](/uwp/api/windows.media.devices.focuscontrol.supported) property. The **RegionsOfInterestControl** must be supported, and must support at least one region, in order to use this technique. Check the [**AutoFocusSupported**](/uwp/api/windows.media.devices.regionsofinterestcontrol.autofocussupported) and [**MaxRegions**](/uwp/api/windows.media.devices.regionsofinterestcontrol.maxregions) properties to determine whether to show or hide the radio button for tap-to-focus.

```csharp
var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;

if (focusControl.Supported)
{
    TapFocusRadioButton.Visibility = (m_mediaCapture.VideoDeviceController.RegionsOfInterestControl.AutoFocusSupported &&
                                      m_mediaCapture.VideoDeviceController.RegionsOfInterestControl.MaxRegions > 0)
                                      ? Visibility.Visible : Visibility.Collapsed;
}
else
{
    TapFocusRadioButton.Visibility = Visibility.Collapsed;
}
```

In the [**Checked**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.togglebutton.checked) event handler for the tap-to-focus radio button, use the [**VideoDeviceController.FocusControl**](/uwp/api/windows.media.devices.videodevicecontroller.focuscontrol) property to get an instance of the control. Call [**LockAsync**](/uwp/api/windows.media.devices.focuscontrol.lockasync) to lock the control in case your app has previously called [**UnlockAsync**](/uwp/api/windows.media.devices.focuscontrol.unlockasync) to enable continuous autofocus, and then wait for the user to tap the screen to change the focus.

```csharp
private async void rbTapToFocus_Checked(object sender, RoutedEventArgs e)
{
    // Lock focus in case Continuous Autofocus was active when switching to Tap-to-focus
    var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;
    await focusControl.LockAsync();
    // Wait for user tap
}
```

This example focuses on a region when the user taps the screen, and then removes the focus from that region when the user taps again, like a toggle. Use a boolean variable to track the current toggled state.

```csharp
bool _isFocused = false;
```

The next step is to listen for the event when the user taps the screen by handling the [**Tapped**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tapped) event of the [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) that is currently displaying the capture preview stream. If the camera isn't currently previewing, or if tap-to-focus mode is disabled, return from the handler without doing anything.

If the tracking variable *\_isFocused* is toggled to false, and if the camera isn't currently in the process of focus (determined by the [**FocusState**](/uwp/api/windows.media.devices.focuscontrol.focusstate) property of the **FocusControl**), begin the tap-to-focus process. Get the position of the user's tap from the event args passed into the handler. This example also uses this opportunity to pick the size of the region that will be focused upon. In this case, the size is 1/4 of the smallest dimension of the capture element. Pass the tap position and the region size into the **TapToFocus** helper method that is defined in the next section.

If the *\_isFocused* toggle is set to true, the user tap should clear the focus from the previous region. This is done in the **TapUnfocus** helper method shown below.

```csharp
private async void mpePreview_Tapped(object sender, TappedRoutedEventArgs e)
{
    if (!m_isPreviewing || (TapFocusRadioButton.IsChecked != true)) return;

    if (!_isFocused && m_mediaCapture.VideoDeviceController.FocusControl.FocusState != MediaCaptureFocusState.Searching)
    {
        var smallEdge = Math.Min(mpePreview.ActualWidth, mpePreview.ActualHeight);

        // Make the focus rectangle 1/4th the length of the preview control's shortest edge.
        var size = new Size(smallEdge / 4, smallEdge / 4);
        var position = e.GetPosition(sender as UIElement);

        // Note that at this point, a rect at "position" with size "size" could extend beyond the preview area. The following method will reposition the rect if that is the case
        await TapToFocus(position, size);
    }
    else
    {
        await TapUnfocus();
    }
}
```

In the **TapToFocus** helper method, first set the *\_isFocused* toggle to true so that the next screen tap will release the focus from the tapped region.

The next task in this helper method is to determine the rectangle within the preview stream that will be assigned to the focus control. This requires two steps. The first step is to determine the rectangle that the preview stream takes up within the [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) control. This depends on the dimensions of the preview stream and the orientation of the device. The helper method **GetPreviewStreamRectInControl**, shown at the end of this section, performs this task and returns the rectangle containing the preview stream.

The next task in **TapToFocus** is to convert the tap location and desired focus rectangle size, which were determined within the **CaptureElement.Tapped** event handler, into coordinates within capture stream. The **ConvertUiTapToPreviewRect** helper method, shown later in this section, performs this conversion and returns the rectangle, in capture stream coordinates, where the focus will be requested.

Now that the target rectangle has been obtained, create a new [**RegionOfInterest**](/uwp/api/Windows.Media.Devices.RegionOfInterest) object, setting the [**Bounds**](/uwp/api/windows.media.devices.regionofinterest.bounds) property to the target rectangle obtained in the previous steps.

Get the capture device's [**FocusControl**](/uwp/api/Windows.Media.Devices.FocusControl). Create a new [**FocusSettings**](/uwp/api/Windows.Media.Devices.FocusSettings) object and set the [**Mode**](/uwp/api/windows.media.devices.focuscontrol.mode) and [**AutoFocusRange**](/uwp/api/windows.media.devices.focussettings.autofocusrange) to your desired values, after checking to make sure that they are supported by the **FocusControl**. Call [**Configure**](/uwp/api/windows.media.devices.focuscontrol.configure) on the **FocusControl** to make your settings active and signal the device to begin focusing on the specified region.

Next, get the capture device's [**RegionsOfInterestControl**](/uwp/api/Windows.Media.Devices.RegionsOfInterestControl) and call [**SetRegionsAsync**](/uwp/api/windows.media.devices.regionsofinterestcontrol.setregionsasync) to set the active region. Multiple regions of interest can be set on devices that support it, but this example only sets a single region.

Finally, call [**FocusAsync**](/uwp/api/windows.media.devices.focuscontrol.focusasync) on the **FocusControl** to initiate focusing.

> [!IMPORTANT]
> When implementing tap to focus, the order of operations is important. You should call these APIs in the following order:
>
> 1. [**FocusControl.Configure**](/uwp/api/windows.media.devices.focuscontrol.configure)
> 2. [**RegionsOfInterestControl.SetRegionsAsync**](/uwp/api/windows.media.devices.regionsofinterestcontrol.setregionsasync)
> 3. [**FocusControl.FocusAsync**](/uwp/api/windows.media.devices.focuscontrol.focusasync)

```csharp
public async Task TapToFocus(Point position, Size size)
{
    _isFocused = true;

    var previewRect = GetPreviewStreamRectInControl();
    var focusPreview = ConvertUiTapToPreviewRect(position, size, previewRect);

    // Note that this Region Of Interest could be configured to also calculate exposure
    // and white balance within the region
    var regionOfInterest = new RegionOfInterest
    {
        AutoFocusEnabled = true,
        BoundsNormalized = true,
        Bounds = focusPreview,
        Type = RegionOfInterestType.Unknown,
        Weight = 100,
    };


    var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;
    var focusRange = focusControl.SupportedFocusRanges.Contains(AutoFocusRange.FullRange) ? AutoFocusRange.FullRange : focusControl.SupportedFocusRanges.FirstOrDefault();
    var focusMode = focusControl.SupportedFocusModes.Contains(FocusMode.Single) ? FocusMode.Single : focusControl.SupportedFocusModes.FirstOrDefault();
    var settings = new FocusSettings { Mode = focusMode, AutoFocusRange = focusRange };
    focusControl.Configure(settings);

    var roiControl = m_mediaCapture.VideoDeviceController.RegionsOfInterestControl;
    await roiControl.SetRegionsAsync(new[] { regionOfInterest }, true);

    await focusControl.FocusAsync();
}
```

In the **TapUnfocus** helper method, obtain the **RegionsOfInterestControl** and call [**ClearRegionsAsync**](/uwp/api/windows.media.devices.regionsofinterestcontrol.clearregionsasync) to clear the region that was registered with the control within the **TapToFocus** helper method. Then, get the **FocusControl** and call [**FocusAsync**](/uwp/api/windows.media.devices.focuscontrol.focusasync) to cause the device to refocus without a region of interest.

```csharp
private async Task TapUnfocus()
{
    _isFocused = false;

    var roiControl = m_mediaCapture.VideoDeviceController.RegionsOfInterestControl;
    await roiControl.ClearRegionsAsync();

    var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;
    await focusControl.FocusAsync();
}
```

The **GetPreviewStreamRectInControl** helper method uses the resolution of the preview stream and the orientation of the device to determine the rectangle within the preview element that contains the preview stream, trimming off any letterboxed padding that the control may provide to maintain the stream's aspect ratio. This method uses class member variables defined in the basic media capture example code found in [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md).

```csharp
public Rect GetPreviewStreamRectInControl()
{
    var result = new Rect();

    var previewResolution = m_mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;

    // In case this function is called before everything is initialized correctly, return an empty result
    if (mpePreview == null || mpePreview.ActualHeight < 1 || mpePreview.ActualWidth < 1 ||
        previewResolution == null || previewResolution.Height == 0 || previewResolution.Width == 0)
    {
        return result;
    }

    var streamWidth = previewResolution.Width;
    var streamHeight = previewResolution.Height;

    // For portrait orientations, the width and height need to be swapped
    if (m_displayOrientation == DisplayOrientations.Portrait || m_displayOrientation == DisplayOrientations.PortraitFlipped)
    {
        streamWidth = previewResolution.Height;
        streamHeight = previewResolution.Width;
    }

    // Start by assuming the preview display area in the control spans the entire width and height both (this is corrected in the next if for the necessary dimension)
    result.Width = mpePreview.ActualWidth;
    result.Height = mpePreview.ActualHeight;

    // If UI is "wider" than preview, letterboxing will be on the sides
    if ((mpePreview.ActualWidth / mpePreview.ActualHeight > streamWidth / (double)streamHeight))
    {
        var scale = mpePreview.ActualHeight / streamHeight;
        var scaledWidth = streamWidth * scale;

        result.X = (mpePreview.ActualWidth - scaledWidth) / 2.0;
        result.Width = scaledWidth;
    }
    else // Preview stream is "wider" than UI, so letterboxing will be on the top+bottom
    {
        var scale = mpePreview.ActualWidth / streamWidth;
        var scaledHeight = streamHeight * scale;

        result.Y = (mpePreview.ActualHeight - scaledHeight) / 2.0;
        result.Height = scaledHeight;
    }

    return result;
}
```

The **ConvertUiTapToPreviewRect** helper method takes as arguments the location of the tap event, the desired size of the focus region, and the rectangle containing the preview stream obtained from the **GetPreviewStreamRectInControl** helper method. This method uses these values and the device's current orientation to calculate the rectangle within the preview stream that contains the desired region. Once again, this method uses class member variables defined in the basic media capture example code found in [Capture Photos and Video with MediaCapture](basic-photo-capture.md).



```csharp
private Rect ConvertUiTapToPreviewRect(Point tap, Size size, Rect previewRect)
{
    // Adjust for the resulting focus rectangle to be centered around the position
    double left = tap.X - size.Width / 2, top = tap.Y - size.Height / 2;

    // Get the information about the active preview area within the CaptureElement (in case it's letterboxed)
    double previewWidth = previewRect.Width, previewHeight = previewRect.Height;
    double previewLeft = previewRect.Left, previewTop = previewRect.Top;

    // Transform the left and top of the tap to account for rotation
    switch (m_displayOrientation)
    {
        case DisplayOrientations.Portrait:
            var tempLeft = left;

            left = top;
            top = previewRect.Width - tempLeft;
            break;
        case DisplayOrientations.LandscapeFlipped:
            left = previewRect.Width - left;
            top = previewRect.Height - top;
            break;
        case DisplayOrientations.PortraitFlipped:
            var tempTop = top;

            top = left;
            left = previewRect.Width - tempTop;
            break;
    }

    // For portrait orientations, the information about the active preview area needs to be rotated
    if (m_displayOrientation == DisplayOrientations.Portrait || m_displayOrientation == DisplayOrientations.PortraitFlipped)
    {
        previewWidth = previewRect.Height;
        previewHeight = previewRect.Width;
        previewLeft = previewRect.Top;
        previewTop = previewRect.Left;
    }

    // Normalize width and height of the focus rectangle
    var width = size.Width / previewWidth;
    var height = size.Height / previewHeight;

    // Shift rect left and top to be relative to just the active preview area
    left -= previewLeft;
    top -= previewTop;

    // Normalize left and top
    left /= previewWidth;
    top /= previewHeight;

    // Ensure rectangle is fully contained within the active preview area horizontally
    left = Math.Max(left, 0);
    left = Math.Min(1 - width, left);

    // Ensure rectangle is fully contained within the active preview area vertically
    top = Math.Max(top, 0);
    top = Math.Min(1 - height, top);

    // Create and return resulting rectangle
    return new Rect(left, top, width, height);
}
```

### Manual focus

The manual focus technique uses a **Slider** control to set the current focus depth of the capture device. A radio button is used to toggle manual focus on and off.

```xml
<Slider Name="slFocus" IsEnabled="{Binding ElementName=rbManualFocus,Path=IsChecked}" ValueChanged="slFocus_ValueChanged"/>
<TextBlock Text="{Binding ElementName=slFocus,Path=Value,FallbackValue='0'}"/>
<RadioButton Content="Manual" Name="rbManualFocus" Checked="rbManualFocus_Checked" IsChecked="False"/>
```

Check to see if the current capture device supports the **FocusControl** by checking the [**Supported**](/uwp/api/windows.media.devices.focuscontrol.supported) property. If the control is supported, you can show and enable the UI for this feature.

The focus value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](/uwp/api/windows.media.devices.focuscontrol.min), [**Max**](/uwp/api/windows.media.devices.focuscontrol.max), and [**Step**](/uwp/api/windows.media.devices.focuscontrol.step) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **FocusControl** after unregistering the [**ValueChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.rangebase.valuechanged) event handler so that the event is not triggered when the value is set.

```csharp
var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;

if (focusControl.Supported)
{
    slFocus.Visibility = Visibility.Visible;
    rbManualFocus.Visibility = Visibility.Visible;

    slFocus.Minimum = focusControl.Min;
    slFocus.Maximum = focusControl.Max;
    slFocus.StepFrequency = focusControl.Step;


    slFocus.ValueChanged -= slFocus_ValueChanged;
    slFocus.Value = focusControl.Value;
    slFocus.ValueChanged += slFocus_ValueChanged;
}
else
{
    slFocus.Visibility = Visibility.Collapsed;
    rbManualFocus.Visibility = Visibility.Collapsed;
}
```

In the **Checked** event handler for the manual focus radio button, get the **FocusControl** object and call [**LockAsync**](/uwp/api/windows.media.devices.focuscontrol.lockasync) in case your app had previously unlocked the focus with a call to [**UnlockAsync**](/uwp/api/windows.media.devices.focuscontrol.unlockasync).

```csharp
private async void rbManualFocus_Checked(object sender, RoutedEventArgs e)
{
    var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;
    await focusControl.LockAsync();
}
```

In the **ValueChanged** event handler of the manual focus slider, get the current value of the control and the set the focus value by calling [**SetValueAsync**](/uwp/api/windows.media.devices.focuscontrol.setvalueasync).

```csharp
private async void slFocus_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
{
    var value = (sender as Slider).Value;
    await m_mediaCapture.VideoDeviceController.FocusControl.SetValueAsync((uint)value);
}
```

### Enable the focus light

On devices that support it, you can enable a focus assist light to help the device focus. This example uses a checkbox to enable or disable the focus assist light.

```xml
<CheckBox Content="Assist Light" Name="cbFocusLight" IsEnabled="{Binding ElementName=rbTapToFocus,Path=IsChecked}"
Checked="cbFocusLight_CheckedChanged" Unchecked="cbFocusLight_CheckedChanged"/>

```

Check to see if the current capture device supports the **FlashControl** by checking the [**Supported**](/uwp/api/windows.media.devices.flashcontrol.supported) property. Also check the [**AssistantLightSupported**](/uwp/api/windows.media.devices.flashcontrol.assistantlightsupported) to make sure the assist light is also supported. If these are both supported, you can show and enable the UI for this feature.

```csharp
var focusControl = m_mediaCapture.VideoDeviceController.FocusControl;

if (focusControl.Supported)
{

    cbFocusLight.Visibility = (m_mediaCapture.VideoDeviceController.FlashControl.Supported &&
                                     m_mediaCapture.VideoDeviceController.FlashControl.AssistantLightSupported) ? Visibility.Visible : Visibility.Collapsed;
}
else
{
    cbFocusLight.Visibility = Visibility.Collapsed;
}
```

In the **CheckedChanged** event handler, get the capture devices [**FlashControl**](/uwp/api/Windows.Media.Devices.FlashControl) object. Set the [**AssistantLightEnabled**](/uwp/api/windows.media.devices.flashcontrol.assistantlightenabled) property to enable or disable the focus light.

```csharp
private void cbFocusLight_CheckedChanged(object sender, RoutedEventArgs e)
{
    var flashControl = m_mediaCapture.VideoDeviceController.FlashControl;

    flashControl.AssistantLightEnabled = (cbFocusLight.IsChecked == true);
}
```

## ISO speed

The [**IsoSpeedControl**](/uwp/api/Windows.Media.Devices.IsoSpeedControl) allows you to set the ISO speed used during photo or video capture.

This example uses a [**Slider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.slider) control to adjust the current exposure compensation value and a checkbox to toggle automatic ISO speed adjustment.

```xml
<Slider Name="slIso" ValueChanged="slIso_ValueChanged"/>
<TextBlock Text="{Binding ElementName=slIso,Path=Value}" Visibility="{Binding ElementName=slIso,Path=Visibility}"/>
<CheckBox Name="cbIsoAuto" Content="Auto" Checked="cbIsoAuto_CheckedChanged" Unchecked="cbIsoAuto_CheckedChanged"/>
```

Check to see if the current capture device supports the **IsoSpeedControl** by checking the [**Supported**](/uwp/api/windows.media.devices.isospeedcontrol.supported) property. If the control is supported, you can show and enable the UI for this feature. Set the checked state of the checkbox to indicate if automatic ISO speed adjustment is currently active to the value of the [**Auto**](/uwp/api/windows.media.devices.isospeedcontrol.auto) property.

The ISO speed value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](/uwp/api/windows.media.devices.isospeedcontrol.min), [**Max**](/uwp/api/windows.media.devices.isospeedcontrol.max), and [**Step**](/uwp/api/windows.media.devices.isospeedcontrol.step) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **IsoSpeedControl** after unregistering the [**ValueChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.rangebase.valuechanged) event handler so that the event is not triggered when the value is set.

```csharp
private void bUpdateIsoControlCapabilities_Click(object sender, RoutedEventArgs e)
{
    var isoSpeedControl = m_mediaCapture.VideoDeviceController.IsoSpeedControl;

    if (isoSpeedControl.Supported)
    {
        cbIsoAuto.Visibility = Visibility.Visible;
        slIso.Visibility = Visibility.Visible;

        cbIsoAuto.IsChecked = isoSpeedControl.Auto;

        slIso.Minimum = isoSpeedControl.Min;
        slIso.Maximum = isoSpeedControl.Max;
        slIso.StepFrequency = isoSpeedControl.Step;

        slIso.ValueChanged -= slIso_ValueChanged;
        slIso.Value = isoSpeedControl.Value;
        slIso.ValueChanged += slIso_ValueChanged;
    }
    else
    {
        cbIsoAuto.Visibility = Visibility.Collapsed;
        slIso.Visibility = Visibility.Collapsed;
    }
}
```

In the **ValueChanged** event handler, get the current value of the control and the set the ISO speed value by calling [**SetValueAsync**](/uwp/api/windows.media.devices.isospeedcontrol.setvalueasync).

```csharp
private async void slIso_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
{
    var value = (sender as Slider).Value;
    await m_mediaCapture.VideoDeviceController.IsoSpeedControl.SetValueAsync((uint)value);
}
```

In the **CheckedChanged** event handler of the auto ISO speed checkbox, turn on automatic ISO speed adjustment by calling [**SetAutoAsync**](/uwp/api/windows.media.devices.isospeedcontrol.setautoasync). Turn automatic ISO speed adjustment off by calling [**SetValueAsync**](/uwp/api/windows.media.devices.isospeedcontrol.setvalueasync) and passing in the current value of the slider control.

```csharp
private async void cbIsoAuto_CheckedChanged(object sender, RoutedEventArgs e)
{
    var autoIso = (sender as CheckBox).IsChecked == true;

    if (autoIso)
    {
        await m_mediaCapture.VideoDeviceController.IsoSpeedControl.SetAutoAsync();
    }
    else
    {
        await m_mediaCapture.VideoDeviceController.IsoSpeedControl.SetValueAsync((uint)slIso.Value);
    }
}
```

## Optical image stabilization

Optical image stabilization (OIS) stabilizes a captured video stream by mechanically manipulating the hardware capture device, which can provide a superior result than digital stabilization. On devices that don't support OIS, you can use the VideoStabilizationEffect to perform digital stabilization on your captured video. For more information, see [Effects for video capture](effects-for-video-capture.md).

Determine if OIS is supported on the current device by checking the [**OpticalImageStabilizationControl.Supported**](/uwp/api/windows.media.devices.opticalimagestabilizationcontrol.supported) property.

The OIS control supports three modes: on, off, and automatic, which means that the device dynamically determines if OIS would improve the media capture and, if so, enables OIS. To determine if a particular mode is supported on a device, check to see if the [**OpticalImageStabilizationControl.SupportedModes**](/uwp/api/windows.media.devices.opticalimagestabilizationcontrol.supportedmodes) collection contains the desired mode.

Enable or disable OIS by setting the [**OpticalImageStabilizationControl.Mode**](/uwp/api/Windows.Media.Devices.OpticalImageStabilizationMode) to the desired mode.

```csharp
private void SetOpticalImageStabilizationMode(OpticalImageStabilizationMode mode)
{
    if (!m_mediaCapture.VideoDeviceController.OpticalImageStabilizationControl.Supported)
    {
        tbStatus.Text = "Optical image stabilization not available";
        return;
    }

    var stabilizationModes = m_mediaCapture.VideoDeviceController.OpticalImageStabilizationControl.SupportedModes;

    if (!stabilizationModes.Contains(mode))
    {
        tbStatus.Text = "Optical image stabilization setting not supported";
        return;
    }

    m_mediaCapture.VideoDeviceController.OpticalImageStabilizationControl.Mode = mode;
}
```

## Powerline frequency

Some camera devices support anti-flicker processing that depends on knowing the AC frequency of the powerlines in the current environment. Some devices support automatic determination of the powerline frequency, while others require that the frequency be set manually. The following code example shows how to determine powerline frequency support on the device and, if needed, how to set the frequency manually. 

First, call the **VideoDeviceController** method [**TryGetPowerlineFrequency**](/uwp/api/windows.media.devices.videodevicecontroller.trygetpowerlinefrequency), passing in an output parameter of type [**PowerlineFrequency**](/uwp/api/Windows.Media.Capture.PowerlineFrequency); if this call fails, the powerline frequency control is not supported on the current device. If the feature is supported, you can determine if automatic mode is available on the device by trying to set auto mode. Do this by calling [**TrySetPowerlineFrequency**](/uwp/api/windows.media.devices.videodevicecontroller.trysetpowerlinefrequency) and passing in the value **Auto**. If the call succeeds, that means that your auto powerline frequency is supported. If the powerline frequency controller is supported on the device but automatic frequency detection is not, you can still manually set the frequency by using **TrySetPowerlineFrequency**. In this example, **MyCustomFrequencyLookup** is a custom method that you implement to determine the correct frequency for the device's current location. 

```csharp
PowerlineFrequency getFrequency;

if (!m_mediaCapture.VideoDeviceController.TryGetPowerlineFrequency(out getFrequency))
{
    // Powerline frequency is not supported on this device.
    return;
}

if (!m_mediaCapture.VideoDeviceController.TrySetPowerlineFrequency(PowerlineFrequency.Auto))
{
    // Set the frequency manually.
    PowerlineFrequency setFrequency = MyCustomFrequencyLookup();
    if (m_mediaCapture.VideoDeviceController.TrySetPowerlineFrequency(setFrequency))
    {
        System.Diagnostics.Debug.WriteLine(String.Format("Powerline frequency manually set to {0}.", setFrequency));
    }
}

```

## White balance

The [**WhiteBalanceControl**](/uwp/api/windows.media.devices.videodevicecontroller.whitebalancecontrol) allows you to set the white balance used during photo or video capture.

This example uses a [**ComboBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.combobox) control to select from built-in color temperature presets and a [**Slider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.slider) control for manual white balance adjustment.

```xml
<Slider Name="slWhiteBalance" ValueChanged="slWhiteBalance_ValueChanged"/>
<TextBlock Name="tbWhiteBalance" Text="{Binding ElementName=slWhiteBalance,Path=Value}" Visibility="{Binding ElementName=slWhiteBalance,Path=Visibility}"/>
<ComboBox Name="cbWhiteBalance" SelectionChanged="cbWhiteBalance_SelectionChanged"/>
```

Check to see if the current capture device supports the **WhiteBalanceControl** by checking the [**Supported**](/uwp/api/windows.media.devices.whitebalancecontrol.supported) property. If the control is supported, you can show and enable the UI for this feature. Set the items of the combo box to the values of the [**ColorTemperaturePreset**](/uwp/api/Windows.Media.Devices.ColorTemperaturePreset) enumeration. And set the selected item to the current value of the [**Preset**](/uwp/api/windows.media.devices.whitebalancecontrol.preset) property.

For manual control, the white balance value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](/uwp/api/windows.media.devices.whitebalancecontrol.min), [**Max**](/uwp/api/windows.media.devices.whitebalancecontrol.max), and [**Step**](/uwp/api/windows.media.devices.whitebalancecontrol.step) properties, which are used to set the corresponding properties of the slider control. Before enabling manual control, check to make sure that the range between the minimum and maximum supported values is greater than the step size. If it is not, manual control is not supported on the current device.

Set the slider control's value to the current value of the **WhiteBalanceControl** after unregistering the [**ValueChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.rangebase.valuechanged) event handler so that the event is not triggered when the value is set.

```csharp
var whiteBalanceControl = m_mediaCapture.VideoDeviceController.WhiteBalanceControl;

if (whiteBalanceControl.Supported)
{
    slWhiteBalance.Visibility = Visibility.Visible;
    cbWhiteBalance.Visibility = Visibility.Visible;

    if (cbWhiteBalance.ItemsSource == null)
    {
        cbWhiteBalance.ItemsSource = Enum.GetValues(typeof(ColorTemperaturePreset)).Cast<ColorTemperaturePreset>();
    }

    cbWhiteBalance.SelectedItem = whiteBalanceControl.Preset;

    if (whiteBalanceControl.Max - whiteBalanceControl.Min > whiteBalanceControl.Step)
    {
        slWhiteBalance.Minimum = whiteBalanceControl.Min;
        slWhiteBalance.Maximum = whiteBalanceControl.Max;
        slWhiteBalance.StepFrequency = whiteBalanceControl.Step;

        slWhiteBalance.ValueChanged -= slWhiteBalance_ValueChanged;
        slWhiteBalance.Value = whiteBalanceControl.Value;
        slWhiteBalance.ValueChanged += slWhiteBalance_ValueChanged;
    }
    else
    {
        slWhiteBalance.Visibility = Visibility.Collapsed;
    }
}
else
{
    slWhiteBalance.Visibility = Visibility.Collapsed;
    cbWhiteBalance.Visibility = Visibility.Collapsed;
}
```

In the [**SelectionChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectionchanged) event handler of the color temperature preset combo box, get the currently selected preset and set the value of the control by calling [**SetPresetAsync**](/uwp/api/windows.media.devices.whitebalancecontrol.setpresetasync). If the selected preset value is not **Manual**, disable the manual white balance slider.

```csharp
private async void cbWhiteBalance_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if(!m_isPreviewing)
    {
        // Do not set white balance values unless the preview stream is running.
        return;
    }

    var selected = (ColorTemperaturePreset)cbWhiteBalance.SelectedItem;
    slWhiteBalance.IsEnabled = (selected == ColorTemperaturePreset.Manual);
    await m_mediaCapture.VideoDeviceController.WhiteBalanceControl.SetPresetAsync(selected);

}
```

In the **ValueChanged** event handler, get the current value of the control and the set the white balance value by calling [**SetValueAsync**](/uwp/api/windows.media.devices.whitebalancecontrol.setvalueasync).

```csharp
private async void slWhiteBalance_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
{
    if (!m_isPreviewing)
    {
        // Do not set white balance values unless the preview stream is running.
        return;
    }

    var value = (sender as Slider).Value;
    await m_mediaCapture.VideoDeviceController.WhiteBalanceControl.SetValueAsync((uint)value);
}
```

> [!IMPORTANT]
> Adjusting the white balance is only supported while the preview stream is running. Check to make sure that the preview stream is running before setting the white balance value or preset.

> [!IMPORTANT]
> The **ColorTemperaturePreset.Auto** preset value instructs the system to automatically adjust the white balance level. For some scenarios, such as capturing a photo sequence where the white balance levels should be the same for each frame, you may want to lock the control to the current automatic value. To do this, call [**SetPresetAsync**](/uwp/api/windows.media.devices.whitebalancecontrol.setpresetasync) and specify the **Manual** preset and do not set a value on the control using [**SetValueAsync**](/uwp/api/windows.media.devices.whitebalancecontrol.setvalueasync). This will cause the device to lock the current value. Do not attempt to read the current control value and then pass the returned value into **SetValueAsync** because this value is not guaranteed to be correct.

## Zoom

The [**ZoomControl**](/uwp/api/Windows.Media.Devices.ZoomControl) allows you to set the zoom level used during photo or video capture.

This example uses a [**Slider**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.slider) control to adjust the current zoom level. The following section shows how to adjust zoom based on a pinch gesture on the screen.

```xml
<Slider Name="slZoom" Grid.Row="0" Orientation="Vertical" HorizontalAlignment="Center" VerticalAlignment="Stretch" ValueChanged="slZoom_ValueChanged"/>
<TextBlock Grid.Row="1" HorizontalAlignment="Center" Text="{Binding ElementName=slZoom,Path=Value}"/>
<Button x:Name="bRegisterPinchGestureHandler" Content="Register pinch gesture handler" Click="bRegisterPinchGestureHandler_Click"/>
```

Check to see if the current capture device supports the **ZoomControl** by checking the [**Supported**](/uwp/api/windows.media.devices.zoomcontrol.supported) property. If the control is supported, you can show and enable the UI for this feature.

The zoom level value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](/uwp/api/windows.media.devices.zoomcontrol.min), [**Max**](/uwp/api/windows.media.devices.zoomcontrol.max), and [**Step**](/uwp/api/windows.media.devices.zoomcontrol.step) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **ZoomControl** after unregistering the [**ValueChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.rangebase.valuechanged) event handler so that the event is not triggered when the value is set.

```csharp
var zoomControl = m_mediaCapture.VideoDeviceController.ZoomControl;

if (zoomControl.Supported)
{
    slZoom.Visibility = Visibility.Visible;

    slZoom.Minimum = zoomControl.Min;
    slZoom.Maximum = zoomControl.Max;
    slZoom.StepFrequency = zoomControl.Step;

    slZoom.ValueChanged -= slZoom_ValueChanged;
    slZoom.Value = zoomControl.Value;
    slZoom.ValueChanged += slZoom_ValueChanged;
}
else
{
    slZoom.Visibility = Visibility.Collapsed;
}
```

In the **ValueChanged** event handler, create a new instance of the [**ZoomSettings**](/uwp/api/Windows.Media.Devices.ZoomSettings) class, setting the [**Value**](/uwp/api/windows.media.devices.zoomsettings.value) property to the current value of the zoom slider control. If the [**SupportedModes**](/uwp/api/windows.media.devices.zoomcontrol.supportedmodes) property of the **ZoomControl** contains [**ZoomTransitionMode.Smooth**](/uwp/api/Windows.Media.Devices.ZoomTransitionMode), it means the device supports smooth transitions between zoom levels. Since this modes provides a better user experience, you will typically want to use this value for the [**Mode**](/uwp/api/windows.media.devices.zoomsettings.mode) property of the **ZoomSettings** object.

Finally, change the current zoom settings by passing your **ZoomSettings** object into the [**Configure**](/uwp/api/windows.media.devices.zoomcontrol.configure) method of the **ZoomControl** object.

```csharp
private void slZoom_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
{
    var level = (float)slZoom.Value;
    var settings = new ZoomSettings { Value = level };

    var zoomControl = m_mediaCapture.VideoDeviceController.ZoomControl;
    if (zoomControl.SupportedModes.Contains(ZoomTransitionMode.Smooth))
    {
        settings.Mode = ZoomTransitionMode.Smooth;
    }
    else
    {
        settings.Mode = zoomControl.SupportedModes.First();
    }

    zoomControl.Configure(settings);
}
```

### Smooth zoom using pinch gesture

As discussed in the previous section, on devices that support it, smooth zoom mode allows the capture device to smoothly transition between digital zoom levels, allowing the user to dynamically adjust the zoom level during the capture operation without discrete and jarring transitions. This section describes how to adjust the zoom level in response to a pinch gesture.

First, determine if the digital zoom control is supported on the current device by checking the [**ZoomControl.Supported**](/uwp/api/windows.media.devices.zoomcontrol.supported) property. Next, determine if smooth zoom mode is available by checking the [**ZoomControl.SupportedModes**](/uwp/api/windows.media.devices.zoomcontrol.supportedmodes) to see if it contains the value [**ZoomTransitionMode.Smooth**](/uwp/api/Windows.Media.Devices.ZoomTransitionMode).

```csharp
private bool IsSmoothZoomSupported()
{
    if (!m_mediaCapture.VideoDeviceController.ZoomControl.Supported)
    {
        tbStatus.Text = "Digital zoom is not supported on this device.";
        return false;
    }

    var zoomModes = m_mediaCapture.VideoDeviceController.ZoomControl.SupportedModes;

    if (!zoomModes.Contains(ZoomTransitionMode.Smooth))
    {
        tbStatus.Text = "Smooth zoom not supported";
        return false;
    }

    return true;
}
```

On a multi-touch enabled device, a typical scenario is to adjust the zoom factor based on a two-finger pinch gesture. Set the [**ManipulationMode**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.manipulationmode) property of the [**CaptureElement**](/uwp/api/Windows.UI.Xaml.Controls.CaptureElement) control to [**ManipulationModes.Scale**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.input.manipulationmodes) to enable the pinch gesture. Then, register for the [**ManipulationDelta**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.manipulationdelta) event which is raised when the pinch gesture changes size.

```csharp
private void RegisterPinchGestureHandler()
{
    if (!IsSmoothZoomSupported())
    {
        return;
    }

    // Enable pinch/zoom gesture for the MediaCaptureElement
    mpePreview.ManipulationMode = ManipulationModes.Scale;
    mpePreview.ManipulationDelta += MpePreview_ManipulationDelta;
}
```

In the handler for the **ManipulationDelta** event, update the zoom factor based on the change in the user's pinch gesture. The [**ManipulationDelta.Scale**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.manipulationdelta) value represents the change in scale of the pinch gesture such that a small increase in the size of the pinch is a number slightly larger than 1.0 and a small decrease in the pinch size is a number slightly smaller than 1.0. In this example, the current value of the zoom control is multiplied by the scale delta.

Before setting the zoom factor, you must make sure that the value is not less than the minimum value supported by the device as indicated by the [**ZoomControl.Min**](/uwp/api/windows.media.devices.zoomcontrol.min) property. Also, make sure that the value is less than or equal to the [**ZoomControl.Max**](/uwp/api/windows.media.devices.zoomcontrol.max) value. Finally, you must make sure that the zoom factor is a multiple of the zoom step size supported by the device as indicated by the [**Step**](/uwp/api/windows.media.devices.zoomcontrol.step) property. If your zoom factor does not meet these requirements, an exception will be thrown when you attempt to set the zoom level on the capture device.

Set the zoom level on the capture device by creating a new [**ZoomSettings**](/uwp/api/Windows.Media.Devices.ZoomSettings) object. Set the [**Mode**](/uwp/api/windows.media.devices.zoomsettings.mode) property to [**ZoomTransitionMode.Smooth**](/uwp/api/Windows.Media.Devices.ZoomTransitionMode) and then set the [**Value**](/uwp/api/windows.media.devices.zoomsettings.value) property to your desired zoom factor. Finally, call [**ZoomControl.Configure**](/uwp/api/windows.media.devices.zoomcontrol.configure) to set the new zoom value on the device. The device will smoothly transition to the new zoom value.

```csharp
private void MpePreview_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
{
    var zoomControl = m_mediaCapture.VideoDeviceController.ZoomControl;

    // Example zoom factor calculation based on size of scale gesture
    var zoomFactor = zoomControl.Value * e.Delta.Scale;

    if (zoomFactor < zoomControl.Min) zoomFactor = zoomControl.Min;
    if (zoomFactor > zoomControl.Max) zoomFactor = zoomControl.Max;
    zoomFactor = zoomFactor - (zoomFactor % zoomControl.Step);

    var settings = new ZoomSettings();
    settings.Mode = ZoomTransitionMode.Smooth;
    settings.Value = zoomFactor;

    m_mediaCapture.VideoDeviceController.ZoomControl.Configure(settings);

}
```

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
