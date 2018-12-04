---
ms.assetid: 831123A7-1F40-4B74-AE9F-69AC9883B4AD
description: This article shows you how to use manual device controls to enable enhanced photo and video capture scenarios including optical image stabilization and smooth zoom.
title: Manual camera controls for photo and video capture
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Manual camera controls for photo and video capture



This article shows you how to use manual device controls to enable enhanced photo and video capture scenarios including optical image stabilization and smooth zoom.

The controls discussed in this article are all added to your app using the same pattern. First, check to see if the control is supported on the current device on which your app is running. If the control is supported, set the desired mode for the control. Typically, if a particular control is unsupported on the current device, you should disable or hide the UI element that allows the user to enable the feature.

The code in this article was adapted from the [Camera Manual Controls SDK sample](https://go.microsoft.com/fwlink/?linkid=845228). You can download the sample to see the code used in context or to use the sample as a starting point for your own app.

> [!NOTE]
> This article builds on concepts and code discussed in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md), which describes the steps for implementing basic photo and video capture. We recommend that you familiarize yourself with the basic media capture pattern in that article before moving on to more advanced capture scenarios. The code in this article assumes that your app already has an instance of MediaCapture that has been properly initialized.

All of the device control APIs discussed in this article are members of the [**Windows.Media.Devices**](https://msdn.microsoft.com/library/windows/apps/br206902) namespace.

[!code-cs[VideoControllersUsing](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetVideoControllersUsing)]

## Exposure

The [**ExposureControl**](https://msdn.microsoft.com/library/windows/apps/dn278910) allows you to set the shutter speed used during photo or video capture.

This example uses a [**Slider**](https://msdn.microsoft.com/library/windows/apps/br209614) control to adjust the current exposure value and a checkbox to toggle automatic exposure adjustment.

[!code-xml[ExposureXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetExposureXAML)]

Check to see if the current capture device supports the **ExposureControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297710) property. If the control is supported, you can show and enable the UI for this feature. Set the checked state of the checkbox to indicate if automatic exposure adjustment is currently active to the value of the [**Auto**](https://msdn.microsoft.com/library/windows/apps/dn278911) property.

The exposure value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](https://msdn.microsoft.com/library/windows/apps/dn278919), [**Max**](https://msdn.microsoft.com/library/windows/apps/dn278914), and [**Step**](https://msdn.microsoft.com/library/windows/apps/dn278930) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **ExposureControl** after unregistering the [**ValueChanged**](https://msdn.microsoft.com/library/windows/apps/br209737) event handler so that the event is not triggered when the value is set.

[!code-cs[ExposureControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetExposureControl)]

In the **ValueChanged** event handler, get the current value of the control and the set the exposure value by calling [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn278927).

[!code-cs[ExposureSlider](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetExposureSlider)]

In the **CheckedChanged** event handler of the auto exposure checkbox, turn automatic exposure adjustment on or off by calling [**SetAutoAsync**](https://msdn.microsoft.com/library/windows/apps/dn278920) and passing in a boolean value.

[!code-cs[ExposureCheckBox](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetExposureCheckBox)]

> [!IMPORTANT]
> Automatic exposure mode is only supported while the preview stream is running. Check to make sure that the preview stream is running before turning on automatic exposure.

## Exposure compensation

The [**ExposureCompensationControl**](https://msdn.microsoft.com/library/windows/apps/dn278897) allows you to set the exposure compensation used during photo or video capture.

This example uses a [**Slider**](https://msdn.microsoft.com/library/windows/apps/br209614) control to adjust the current exposure compensation value.

[!code-xml[EvXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetEvXAML)]

Check to see if the current capture device supports the **ExposureCompensationControl** by checking the [Supported](supported-codecs.md) property. If the control is supported, you can show and enable the UI for this feature.

The exposure compensation value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](https://msdn.microsoft.com/library/windows/apps/dn278901), [**Max**](https://msdn.microsoft.com/library/windows/apps/dn278899), and [**Step**](https://msdn.microsoft.com/library/windows/apps/dn278904) properties, which are used to set the corresponding properties of the slider control.

Set slider control's value to the current value of the **ExposureCompensationControl** after unregistering the [**ValueChanged**](https://msdn.microsoft.com/library/windows/apps/br209737) event handler so that the event is not triggered when the value is set.

[!code-cs[EvControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetEvControl)]

In the **ValueChanged** event handler, get the current value of the control and the set the exposure value by calling [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn278903).

[!code-cs[EvValueChanged](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetEvValueChanged)]

## Flash

The [**FlashControl**](https://msdn.microsoft.com/library/windows/apps/dn297725) allows you to enable or disable the flash or to enable automatic flash, where the system dynamically determines whether to use the flash. This control also allows you to enable automatic red eye reduction on devices that support it. These settings all apply to capturing photos. The [**TorchControl**](https://msdn.microsoft.com/library/windows/apps/dn279077) is a separate control for turning the torch on or off for video capture.

This example uses a set of radio buttons to allow the user to switch between on, off, and auto flash settings. A checkbox is also provided to allow toggling of red eye reduction and the video torch.

[!code-xml[FlashXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetFlashXAML)]

Check to see if the current capture device supports the **FlashControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297837) property. If the control is supported, you can show and enable the UI for this feature. If the **FlashControl** is supported, automatic red eye reduction may or may not be supported, so check the [**RedEyeReductionSupported**](https://msdn.microsoft.com/library/windows/apps/dn297766) property before enabling the UI. Because the **TorchControl** is separate from the flash control, you must also check its [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn279081) property before using it.

In the [**Checked**](https://msdn.microsoft.com/library/windows/apps/br209796) event handler for each of the flash radio buttons, enable or disable the appropriate corresponding flash setting. Note that to set the flash to always be used, you must set the [**Enabled**](https://msdn.microsoft.com/library/windows/apps/dn297733) property to true and the [**Auto**](https://msdn.microsoft.com/library/windows/apps/dn297728) property to false.

[!code-cs[FlashControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetFlashControl)]

[!code-cs[FlashRadioButtons](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetFlashRadioButtons)]

In the handler for the red eye reduction checkbox, set the [**RedEyeReduction**](https://msdn.microsoft.com/library/windows/apps/dn297758) property to the appropriate value.

[!code-cs[RedEye](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetRedEye)]

Finally, in the handler for the video torch checkbox, set the [**Enabled**](https://msdn.microsoft.com/library/windows/apps/dn279078) property to the appropriate value.

[!code-cs[Torch](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetTorch)]

> [!NOTE] 
>  On some devices the torch will not emit light, even if [**TorchControl.Enabled**](https://msdn.microsoft.com/library/windows/apps/dn279078) is set to true, unless the device has a preview stream running and is actively capturing video. The recommended order of operations is to turn on the video preview, turn on the torch by setting **Enabled** to true, and then initiate video capture. On some devices the torch will light up after the preview is started. On other devices, the torch may not light up until video capture is started.

## Focus

Three different commonly used methods for adjusting the focus of the camera are supported by the [**FocusControl**](https://msdn.microsoft.com/library/windows/apps/dn297788) object, continuous autofocus, tap to focus, and manual focus. A camera app may support all three of these methods, but for readability, this article discusses each technique separately. This section also discusses how to enable the focus assist light.

### Continuous autofocus

Enabling continuous autofocus instructs the camera to adjust the focus dynamically to try to keep the subject of the photo or video in focus. This example uses a radio button to toggle continuous autofocus on and off.

[!code-xml[CAFXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetCAFXAML)]

Check to see if the current capture device supports the **FocusControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297785) property. Next, determine if continuous autofocus is supported by checking the [**SupportedFocusModes**](https://msdn.microsoft.com/library/windows/apps/dn608079) list to see if it contains the value [**FocusMode.Continuous**](https://msdn.microsoft.com/library/windows/apps/dn608084), and if so, show the continuous autofocus radio button.

[!code-cs[CAF](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetCAF)]

In the [**Checked**](https://msdn.microsoft.com/library/windows/apps/br209796) event handler for the continuous autofocus radio button, use the [**VideoDeviceController.FocusControl**](https://msdn.microsoft.com/library/windows/apps/dn279091) property to get an instance of the control. Call [**UnlockAsync**](https://msdn.microsoft.com/library/windows/apps/dn608081) to unlock the control in case your app has previously called [**LockAsync**](https://msdn.microsoft.com/library/windows/apps/dn608075) to enable one of the other focus modes.

Create a new [**FocusSettings**](https://msdn.microsoft.com/library/windows/apps/dn608085) object and set the [**Mode**](https://msdn.microsoft.com/library/windows/apps/dn608090) property to **Continuous**. Set the [**AutoFocusRange**](https://msdn.microsoft.com/library/windows/apps/dn608086) property to a value appropriate for your app scenario or selected by the user from your UI. Pass your **FocusSettings** object into the [**Configure**](https://msdn.microsoft.com/library/windows/apps/dn608067) method, and then call [**FocusAsync**](https://msdn.microsoft.com/library/windows/apps/dn297794) to initiate continuous autofocus.

[!code-cs[CafFocusRadioButton](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetCafFocusRadioButton)]

> [!IMPORTANT]
> Autofocus mode is only supported while the preview stream is running. Check to make sure that the preview stream is running before turning on continuous autofocus.

### Tap to focus

The tap-to-focus technique uses the [**FocusControl**](https://msdn.microsoft.com/library/windows/apps/dn297788) and the [**RegionsOfInterestControl**](https://msdn.microsoft.com/library/windows/apps/dn279064) to specify a subregion of the capture frame where the capture device should focus. The region of focus is determined by the user tapping on the screen displaying the preview stream.

This example uses a radio button to enable and disable tap-to-focus mode.

[!code-xml[TapFocusXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetTapFocusXAML)]

Check to see if the current capture device supports the **FocusControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297785) property. The **RegionsOfInterestControl** must be supported, and must support at least one region, in order to use this technique. Check the [**AutoFocusSupported**](https://msdn.microsoft.com/library/windows/apps/dn279066) and [**MaxRegions**](https://msdn.microsoft.com/library/windows/apps/dn279069) properties to determine whether to show or hide the radio button for tap-to-focus.

[!code-cs[TapFocus](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetTapFocus)]

In the [**Checked**](https://msdn.microsoft.com/library/windows/apps/br209796) event handler for the tap-to-focus radio button, use the [**VideoDeviceController.FocusControl**](https://msdn.microsoft.com/library/windows/apps/dn279091) property to get an instance of the control. Call [**LockAsync**](https://msdn.microsoft.com/library/windows/apps/dn608075) to lock the control in case your app has previously called [**UnlockAsync**](https://msdn.microsoft.com/library/windows/apps/dn608081) to enable continuous autofocus, and then wait for the user to tap the screen to change the focus.

[!code-cs[TapFocusRadioButton](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetTapFocusRadioButton)]

This example focuses on a region when the user taps the screen, and then removes the focus from that region when the user taps again, like a toggle. Use a boolean variable to track the current toggled state.

[!code-cs[IsFocused](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetIsFocused)]

The next step is to listen for the event when the user taps the screen by handling the [**Tapped**](https://msdn.microsoft.com/library/windows/apps/br208985) event of the [**CaptureElement**](https://msdn.microsoft.com/library/windows/apps/br209278) that is currently displaying the capture preview stream. If the camera isn't currently previewing, or if tap-to-focus mode is disabled, return from the handler without doing anything.

If the tracking variable *\_isFocused* is toggled to false, and if the camera isn't currently in the process of focus (determined by the [**FocusState**](https://msdn.microsoft.com/library/windows/apps/dn608074) property of the **FocusControl**), begin the tap-to-focus process. Get the position of the user's tap from the event args passed into the handler. This example also uses this opportunity to pick the size of the region that will be focused upon. In this case, the size is 1/4 of the smallest dimension of the capture element. Pass the tap position and the region size into the **TapToFocus** helper method that is defined in the next section.

If the *\_isFocused* toggle is set to true, the user tap should clear the focus from the previous region. This is done in the **TapUnfocus** helper method shown below.

[!code-cs[TapFocusPreviewControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetTapFocusPreviewControl)]

In the **TapToFocus** helper method, first set the *\_isFocused* toggle to true so that the next screen tap will release the focus from the tapped region.

The next task in this helper method is to determine the rectangle within the preview stream that will be assigned to the focus control. This requires two steps. The first step is to determine the rectangle that the preview stream takes up within the [**CaptureElement**](https://msdn.microsoft.com/library/windows/apps/br209278) control. This depends on the dimensions of the preview stream and the orientation of the device. The helper method **GetPreviewStreamRectInControl**, shown at the end of this section, performs this task and returns the rectangle containing the preview stream.

The next task in **TapToFocus** is to convert the tap location and desired focus rectangle size, which were determined within the **CaptureElement.Tapped** event handler, into coordinates within capture stream. The **ConvertUiTapToPreviewRect** helper method, shown later in this section, performs this conversion and returns the rectangle, in capture stream coordinates, where the focus will be requested.

Now that the target rectangle has been obtained, create a new [**RegionOfInterest**](https://msdn.microsoft.com/library/windows/apps/dn279058) object, setting the [**Bounds**](https://msdn.microsoft.com/library/windows/apps/dn279062) property to the target rectangle obtained in the previous steps.

Get the capture device's [**FocusControl**](https://msdn.microsoft.com/library/windows/apps/dn297788). Create a new [**FocusSettings**](https://msdn.microsoft.com/library/windows/apps/dn608085) object and set the [**Mode**](https://msdn.microsoft.com/library/windows/apps/dn608076) and [**AutoFocusRange**](https://msdn.microsoft.com/library/windows/apps/dn608086) to your desired values, after checking to make sure that they are supported by the **FocusControl**. Call [**Configure**](https://msdn.microsoft.com/library/windows/apps/dn608067) on the **FocusControl** to make your settings active and signal the device to begin focusing on the specified region.

Next, get the capture device's [**RegionsOfInterestControl**](https://msdn.microsoft.com/library/windows/apps/dn279064) and call [**SetRegionsAsync**](https://msdn.microsoft.com/library/windows/apps/dn279070) to set the active region. Multiple regions of interest can be set on devices that support it, but this example only sets a single region.

Finally, call [**FocusAsync**](https://msdn.microsoft.com/library/windows/apps/dn297794) on the **FocusControl** to initiate focusing.

> [!IMPORTANT]
> When implementing tap to focus, the order of operations is important. You should call these APIs in the following order:
>
> 1. [**FocusControl.Configure**](https://msdn.microsoft.com/library/windows/apps/dn608067)
> 2. [**RegionsOfInterestControl.SetRegionsAsync**](https://msdn.microsoft.com/library/windows/apps/dn279070)
> 3. [**FocusControl.FocusAsync**](https://msdn.microsoft.com/library/windows/apps/dn297794)

[!code-cs[TapToFocus](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetTapToFocus)]

In the **TapUnfocus** helper method, obtain the **RegionsOfInterestControl** and call [**ClearRegionsAsync**](https://msdn.microsoft.com/library/windows/apps/dn279068) to clear the region that was registered with the control within the **TapToFocus** helper method. Then, get the **FocusControl** and call [**FocusAsync**](https://msdn.microsoft.com/library/windows/apps/dn297794) to cause the device to refocus without a region of interest.

[!code-cs[TapUnfocus](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetTapUnfocus)]

The **GetPreviewStreamRectInControl** helper method uses the resolution of the preview stream and the orientation of the device to determine the rectangle within the preview element that contains the preview stream, trimming off any letterboxed padding that the control may provide to maintain the stream's aspect ratio. This method uses class member variables defined in the basic media capture example code found in [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md).

[!code-cs[GetPreviewStreamRectInControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetGetPreviewStreamRectInControl)]

The **ConvertUiTapToPreviewRect** helper method takes as arguments the location of the tap event, the desired size of the focus region, and the rectangle containing the preview stream obtained from the **GetPreviewStreamRectInControl** helper method. This method uses these values and the device's current orientation to calculate the rectangle within the preview stream that contains the desired region. Once again, this method uses class member variables defined in the basic media capture example code found in [Capture Photos and Video with MediaCapture](capture-photos-and-video-with-mediacapture.md).

[!code-cs[ConvertUiTapToPreviewRect](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetConvertUiTapToPreviewRect)]

### Manual focus

The manual focus technique uses a **Slider** control to set the current focus depth of the capture device. A radio button is used to toggle manual focus on and off.

[!code-xml[ManualFocusXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetManualFocusXAML)]

Check to see if the current capture device supports the **FocusControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297837) property. If the control is supported, you can show and enable the UI for this feature.

The focus value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](https://msdn.microsoft.com/library/windows/apps/dn297808), [**Max**](https://msdn.microsoft.com/library/windows/apps/dn297802), and [**Step**](https://msdn.microsoft.com/library/windows/apps/dn297833) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **FocusControl** after unregistering the [**ValueChanged**](https://msdn.microsoft.com/library/windows/apps/br209737) event handler so that the event is not triggered when the value is set.

[!code-cs[Focus](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetFocus)]

In the **Checked** event handler for the manual focus radio button, get the **FocusControl** object and call [**LockAsync**](https://msdn.microsoft.com/library/windows/apps/dn608075) in case your app had previously unlocked the focus with a call to [**UnlockAsync**](https://msdn.microsoft.com/library/windows/apps/dn608081).

[!code-cs[ManualFocusChecked](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetManualFocusChecked)]

In the **ValueChanged** event handler of the manual focus slider, get the current value of the control and the set the focus value by calling [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn297828).

[!code-cs[FocusSlider](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetFocusSlider)]

### Enable the focus light

On devices that support it, you can enable a focus assist light to help the device focus. This example uses a checkbox to enable or disable the focus assist light.

[!code-xml[FocusLightXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetFocusLightXAML)]

Check to see if the current capture device supports the **FlashControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297785) property. Also check the [**AssistantLightSupported**](https://msdn.microsoft.com/library/windows/apps/dn608066) to make sure the assist light is also supported. If these are both supported, you can show and enable the UI for this feature.

[!code-cs[FocusLight](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetFocusLight)]

In the **CheckedChanged** event handler, get the capture devices [**FlashControl**](https://msdn.microsoft.com/library/windows/apps/dn297725) object. Set the [**AssistantLightEnabled**](https://msdn.microsoft.com/library/windows/apps/dn608065) property to enable or disable the focus light.

[!code-cs[FocusLightCheckBox](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetFocusLightCheckBox)]

## ISO speed

The [**IsoSpeedControl**](https://msdn.microsoft.com/library/windows/apps/dn297850) allows you to set the ISO speed used during photo or video capture.

This example uses a [**Slider**](https://msdn.microsoft.com/library/windows/apps/br209614) control to adjust the current exposure compensation value and a checkbox to toggle automatic ISO speed adjustment.

[!code-xml[IsoXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetIsoXAML)]

Check to see if the current capture device supports the **IsoSpeedControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn297869) property. If the control is supported, you can show and enable the UI for this feature. Set the checked state of the checkbox to indicate if automatic ISO speed adjustment is currently active to the value of the [**Auto**](https://msdn.microsoft.com/library/windows/apps/dn608093) property.

The ISO speed value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](https://msdn.microsoft.com/library/windows/apps/dn608095), [**Max**](https://msdn.microsoft.com/library/windows/apps/dn608094), and [**Step**](https://msdn.microsoft.com/library/windows/apps/dn608129) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **IsoSpeedControl** after unregistering the [**ValueChanged**](https://msdn.microsoft.com/library/windows/apps/br209737) event handler so that the event is not triggered when the value is set.

[!code-cs[IsoControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetIsoControl)]

In the **ValueChanged** event handler, get the current value of the control and the set the ISO speed value by calling [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn608128).

[!code-cs[IsoSlider](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetIsoSlider)]

In the **CheckedChanged** event handler of the auto ISO speed checkbox, turn on automatic ISO speed adjustment by calling [**SetAutoAsync**](https://msdn.microsoft.com/library/windows/apps/dn608127). Turn automatic ISO speed adjustment off by calling [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn608128) and passing in the current value of the slider control.

[!code-cs[IsoCheckBox](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetIsoCheckBox)]

## Optical image stabilization

Optical image stabilization (OIS) stabilizes a the captured video stream by mechanically manipulating the hardware capture device, which can provide a superior result than digital stabilization. On devices that don't support OIS, you can use the VideoStabilizationEffect to perform digital stabilization on your captured vide. For more information, see [Effects for video capture](effects-for-video-capture.md).

Determine if OIS is supported on the current device by checking the [**OpticalImageStabilizationControl.Supported**](https://msdn.microsoft.com/library/windows/apps/dn926689) property.

The OIS control supports three modes: on, off, and automatic, which means that the device dynamically determines if OIS would improve the media capture and, if so, enables OIS. To determine if a particular mode is supported on a device, check to see if the [**OpticalImageStabilizationControl.SupportedModes**](https://msdn.microsoft.com/library/windows/apps/dn926690) collection contains the desired mode.

Enable or disable OIS by setting the [**OpticalImageStabilizationControl.Mode**](https://msdn.microsoft.com/library/windows/apps/dn926691) to the desired mode.

[!code-cs[SetOpticalImageStabilizationMode](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetSetOpticalImageStabilizationMode)]

## Powerline frequency
Some camera devices support anti-flicker processing that depends on knowing the AC frequency of the powerlines in the current environment. Some devices support automatic determination of the powerline frequency, while others require that the frequency be set manually. The following code example shows how to determine powerline frequency support on the device and, if needed, how to set the frequency manually. 

First, call the **VideoDeviceController** method [**TryGetPowerlineFrequency**](https://msdn.microsoft.com/library/windows/apps/br206898), passing in an output parameter of type [**PowerlineFrequency**](https://msdn.microsoft.com/library/windows/apps/Windows.Media.Capture.PowerlineFrequency); if this call fails, the powerline frequency control is not supported on the current device. If the feature is supported, you can determine if automatic mode is available on the device by trying to set auto mode. Do this by calling [**TrySetPowerlineFrequency**](https://msdn.microsoft.com/library/windows/apps/br206899) and passing in the value **Auto**. If the call succeeds, that means that your auto powerline frequency is supported. If the powerline frequency controller is supported on the device but automatic frequency detection is not, you can still manually set the frequency by using **TrySetPowerlineFrequency**. In this example, **MyCustomFrequencyLookup** is a custom method that you implement to determine the correct frequency for the device's current location. 

[!code-cs[PowerlineFrequency](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetPowerlineFrequency)]

## White balance

The [**WhiteBalanceControl**](https://msdn.microsoft.com/library/windows/apps/dn279104) allows you to set the white balance used during photo or video capture.

This example uses a [**ComboBox**](https://msdn.microsoft.com/library/windows/apps/br209348) control to select from built-in color temperature presets and a [**Slider**](https://msdn.microsoft.com/library/windows/apps/br209614) control for manual white balance adjustment.

[!code-xml[WhiteBalanceXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetWhiteBalanceXAML)]

Check to see if the current capture device supports the **WhiteBalanceControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn279120) property. If the control is supported, you can show and enable the UI for this feature. Set the items of the combo box to the values of the [**ColorTemperaturePreset**](https://msdn.microsoft.com/library/windows/apps/dn278894) enumeration. And set the selected item to the current value of the [**Preset**](https://msdn.microsoft.com/library/windows/apps/dn279110) property.

For manual control, the white balance value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](https://msdn.microsoft.com/library/windows/apps/dn279109), [**Max**](https://msdn.microsoft.com/library/windows/apps/dn279107), and [**Step**](https://msdn.microsoft.com/library/windows/apps/dn279119) properties, which are used to set the corresponding properties of the slider control. Before enabling manual control, check to make sure that the range between the minimum and maximum supported values is greater than the step size. If it is not, manual control is not supported on the current device.

Set the slider control's value to the current value of the **WhiteBalanceControl** after unregistering the [**ValueChanged**](https://msdn.microsoft.com/library/windows/apps/br209737) event handler so that the event is not triggered when the value is set.

[!code-cs[WhiteBalance](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetWhiteBalance)]

In the [**SelectionChanged**](https://msdn.microsoft.com/library/windows/apps/br209776) event handler of the color temperature preset combo box, get the currently selected preset and set the value of the control by calling [**SetPresetAsync**](https://msdn.microsoft.com/library/windows/apps/dn279113). If the selected preset value is not **Manual**, disable the manual white balance slider.

[!code-cs[WhiteBalanceComboBox](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetWhiteBalanceComboBox)]

In the **ValueChanged** event handler, get the current value of the control and the set the white balance value by calling [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn278927).

[!code-cs[WhiteBalanceSlider](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetWhiteBalanceSlider)]

> [!IMPORTANT]
> Adjusting the white balance is only supported while the preview stream is running. Check to make sure that the preview stream is running before setting the white balance value or preset.

> [!IMPORTANT]
> The **ColorTemperaturePreset.Auto** preset value instructs the system to automatically adjust the white balance level. For some scenarios, such as capturing a photo sequence where the white balance levels should be the same for each frame, you may want to lock the control to the current automatic value. To do this, call [**SetPresetAsync**](https://msdn.microsoft.com/library/windows/apps/dn279113) and specify the **Manual** preset and do not set a value on the control using [**SetValueAsync**](https://msdn.microsoft.com/library/windows/apps/dn279114). This will cause the device to lock the current value. Do not attempt to read the current control value and then pass the returned value into **SetValueAsync** because this value is not guaranteed to be correct.

## Zoom

The [**ZoomControl**](https://msdn.microsoft.com/library/windows/apps/dn608149) allows you to set the zoom level used during photo or video capture.

This example uses a [**Slider**](https://msdn.microsoft.com/library/windows/apps/br209614) control to adjust the current zoom level. The following section shows how to adjust zoom based on a pinch gesture on the screen.

[!code-xml[ZoomXAML](./code/BasicMediaCaptureWin10/cs/MainPage.xaml#SnippetZoomXAML)]

Check to see if the current capture device supports the **ZoomControl** by checking the [**Supported**](https://msdn.microsoft.com/library/windows/apps/dn633819) property. If the control is supported, you can show and enable the UI for this feature.

The zoom level value must be within the range supported by the device and must be an increment of the supported step size. Get the supported values for the current device by checking the [**Min**](https://msdn.microsoft.com/library/windows/apps/dn633817), [**Max**](https://msdn.microsoft.com/library/windows/apps/dn608150), and [**Step**](https://msdn.microsoft.com/library/windows/apps/dn633818) properties, which are used to set the corresponding properties of the slider control.

Set the slider control's value to the current value of the **ZoomControl** after unregistering the [**ValueChanged**](https://msdn.microsoft.com/library/windows/apps/br209737) event handler so that the event is not triggered when the value is set.

[!code-cs[ZoomControl](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetZoomControl)]

In the **ValueChanged** event handler, create a new instance of the [**ZoomSettings**](https://msdn.microsoft.com/library/windows/apps/dn926722) class, setting the [**Value**](https://msdn.microsoft.com/library/windows/apps/dn926724) property to the current value of the zoom slider control. If the [**SupportedModes**](https://msdn.microsoft.com/library/windows/apps/dn926721) property of the **ZoomControl** contains [**ZoomTransitionMode.Smooth**](https://msdn.microsoft.com/library/windows/apps/dn926726), it means the device supports smooth transitions between zoom levels. Since this modes provides a better user experience, you will typically want to use this value for the [**Mode**](https://msdn.microsoft.com/library/windows/apps/dn926723) property of the **ZoomSettings** object.

Finally, change the current zoom settings by passing your **ZoomSettings** object into the [**Configure**](https://msdn.microsoft.com/library/windows/apps/dn926719) method of the **ZoomControl** object.

[!code-cs[ZoomSlider](./code/BasicMediaCaptureWin10/cs/MainPage.ManualControls.xaml.cs#SnippetZoomSlider)]

### Smooth zoom using pinch gesture

As discussed in the previous section, on devices that support it, smooth zoom mode allows the capture device to smoothly transition between digital zoom levels, allowing the user to dynamically adjust the zoom level during the capture operation without discrete and jarring transitions. This section describes how to adjust the zoom level in response to a pinch gesture.

First, determine if the digital zoom control is supported on the current device by checking the [**ZoomControl.Supported**](https://msdn.microsoft.com/library/windows/apps/dn633819) property. Next, determine if smooth zoom mode is available by checking the [**ZoomControl.SupportedModes**](https://msdn.microsoft.com/library/windows/apps/dn926721) to see if it contains the value [**ZoomTransitionMode.Smooth**](https://msdn.microsoft.com/library/windows/apps/dn926726).

[!code-cs[IsSmoothZoomSupported](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetIsSmoothZoomSupported)]

On a multi-touch enabled device, a typical scenario is to adjust the zoom factor based on a two-finger pinch gesture. Set the [**ManipulationMode**](https://msdn.microsoft.com/library/windows/apps/br208948) property of the [**CaptureElement**](https://msdn.microsoft.com/library/windows/apps/br209278) control to [**ManipulationModes.Scale**](https://msdn.microsoft.com/library/windows/apps/br227934) to enable the pinch gesture. Then, register for the [**ManipulationDelta**](https://msdn.microsoft.com/library/windows/apps/br208946) event which is raised when the pinch gesture changes size.

[!code-cs[RegisterPinchGestureHandler](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetRegisterPinchGestureHandler)]

In the handler for the **ManipulationDelta** event, update the zoom factor based on the change in the user's pinch gesture. The [**ManipulationDelta.Scale**](https://msdn.microsoft.com/library/windows/apps/br242016) value represents the change in scale of the pinch gesture such that a small increase in the size of the pinch is a number slightly larger than 1.0 and a small decrease in the pinch size is a number slightly smaller than 1.0. In this example, the current value of the zoom control is multiplied by the scale delta.

Before setting the zoom factor, you must make sure that the value is not less than the minimum value supported by the device as indicated by the [**ZoomControl.Min**](https://msdn.microsoft.com/library/windows/apps/dn633817) property. Also, make sure that the value is less than or equal to the [**ZoomControl.Max**](https://msdn.microsoft.com/library/windows/apps/dn608150) value. Finally, you must make sure that the zoom factor is a multiple of the zoom step size supported by the device as indicated by the [**Step**](https://msdn.microsoft.com/library/windows/apps/dn633818) property. If your zoom factor does not meet these requirements, an exception will be thrown when you attempt to set the zoom level on the capture device.

Set the zoom level on the capture device by creating a new [**ZoomSettings**](https://msdn.microsoft.com/library/windows/apps/dn926722) object. Set the [**Mode**](https://msdn.microsoft.com/library/windows/apps/dn926723) property to [**ZoomTransitionMode.Smooth**](https://msdn.microsoft.com/library/windows/apps/dn926726) and then set the [**Value**](https://msdn.microsoft.com/library/windows/apps/dn926724) property to your desired zoom factor. Finally, call [**ZoomControl.Configure**](https://msdn.microsoft.com/library/windows/apps/dn926719) to set the new zoom value on the device. The device will smoothly transition to the new zoom value.

[!code-cs[ManipulationDelta](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetManipulationDelta)]

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
