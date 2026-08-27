---
title: Show the camera preview in a WinUI app
description: Learn how to show the camera preview in a WinUI app. 
ms.topic: article
ms.date: 08/23/2026
ms.localizationpriority: medium
#customer intent: As a developer, I want to access the camera in a Windows app using WinUI.
---

# Show the camera preview in a WinUI 3 app

In this quickstart, you will learn how to create a basic WinUI camera app that displays the camera preview. In a WinUI app, you use the [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) control in the [Microsoft.UI.Xaml.Controls](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls) namespace to render the camera preview and the WinRT class [MediaCapture](/uwp/api/windows.media.capture.mediacapture) to access the device's camera preview stream. **MediaCapture** provides APIs for performing a wide range of camera-related tasks such as such as capturing photos and videos and configuring the camera's device driver. See the other articles in this section for details about other **MediaCapture** features.

The code in this walkthrough is adapted from the [MediaCapture WinUI sample on github](https://github.com/microsoft/Windows-Camera/tree/master/Samples/MediaCaptureWinUI3). 

## Prerequisites

- Your device must have developer mode enabled. For more information see [Settings for developers](/windows/advanced-settings/developer-mode).
- Visual Studio 2026 or later with the **WinUI application development** workload.

## Create a new WinUI app

In Visual Studio, create a new project. In the **Create a new project** dialog, set the language filter to "C#" and the platform filter to "Windows", then select the "WinUI Blank App (Packaged)" project template.


## Create the UI

The simple UI for this example includes a **MediaPlayerElement** control for displaying the camera preview, a [ComboBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.combobox) that allows you to select from the device's cameras, and buttons for initializing the **MediaCapture** class, starting and stopping the camera preview, and resetting the sample. We also include a [TextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock) for displaying status messages.

In your project's MainWindow.xml file, replace the default **StackPanel** control with the following XAML.

```xaml
<Grid ColumnDefinitions="4*,*" ColumnSpacing="4">
    <MediaPlayerElement x:Name="mpePreview" Grid.Row="0" Grid.Column="0"  AreTransportControlsEnabled="False" ManipulationMode="None"/>
    <StackPanel Orientation="Vertical"  Grid.Row="0" Grid.Column="1" HorizontalAlignment="Stretch"  VerticalAlignment="Top">
        <TextBlock Text="Status:" Margin="0,0,10,0"/>
        <TextBlock x:Name="tbStatus" Text=""/>
        <TextBlock Text="Preview Source:" Margin="0,0,10,0"/>
        <ComboBox x:Name="cbDeviceList" HorizontalAlignment="Stretch" SelectionChanged="cbDeviceList_SelectionChanged"></ComboBox>
        <Button x:Name="bStartMediaCapture" Content="Initialize MediaCapture" IsEnabled="False" Click="bStartMediaCapture_Click"/>
        <Button x:Name="bStartPreview" Content="Start preview" IsEnabled="False" Click="bStartPreview_Click"/>
        <Button x:Name="bStopPreview" Content="Stop preview" IsEnabled="False" Click="bStopPreview_Click"/>
        <Button x:Name="bReset" Content="Reset" Click="bReset_Click" />
    </StackPanel>
</Grid>
```


## Update the MainWindow class definition

The rest of the code in this article will be added to the **MainWindow** class definition in your project's MainWindow.xaml.cs file. First, add a few class variables that will persist throughout the lifetime of the window. These variables include:

- A [DeviceInformationCollection](/uwp/api/windows.devices.enumeration.deviceinformationcollection) that will store a [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) object for each available camera. The **DeviceInformation** object conveys information such as the unique identifier and the friendly name for the camera.
- A **MediaCapture** object that handles interactions with the selected camera's driver and allows you to retrieve the camera's video stream.
- A [MediaFrameSource](/uwp/api/windows.media.capture.frames.mediaframesource) object that represents a source of media frames, such as a video stream.
- A boolean to track when the camera preview is running. Some camera settings can't be changed while the preview is running, so it's a good practice to track the state of the camera preview.

```csharp
private DeviceInformationCollection m_deviceList;
private MediaCapture m_mediaCapture;
private MediaFrameSource m_frameSource;
private MediaPlayer m_mediaPlayer;
private bool m_isPreviewing;
```


## Populate the list of available cameras

Next we'll create a helper method to detect the cameras that are present on the current device and populate the **ComboBox** in the UI with the camera names, allowing the user to select a camera to preview.The [DeviceInformation.FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) allows you to query for many different kinds of devices. We use [MediaDevice.GetVideoCaptureSelector](/uwp/api/windows.media.devices.mediadevice.getvideocaptureselector) to retrieve the identifier that specifies that we only want to retrieve video capture devices.

```csharp
private async void PopulateCameraList()
{
    cbDeviceList.Items.Clear();

    m_deviceList = await DeviceInformation.FindAllAsync(MediaDevice.GetVideoCaptureSelector());

    if(m_deviceList.Count == 0)
    {
        tbStatus.Text = "No video capture devices found.";
        return;
    }

    foreach (var device in m_deviceList)
    {
        cbDeviceList.Items.Add(device.Name);
        bStartMediaCapture.IsEnabled = true;
    }
}
```

Add a call to this helper method to the **MainWindow** class constructor so that the **ComboBox** gets populated when the window loads.

```csharp
public MainWindow()
{
    this.InitializeComponent();

    PopulateCameraList();

}
```

## Handle camera selection changes

When the user selects a different camera from the **ComboBox**, the **SelectionChanged** handler disposes of the existing **MediaCapture** and **MediaPlayer** objects, since they are initialized for a specific camera device. The handler resets the UI state so that the user can click **Initialize MediaCapture** to set up the newly selected camera.

```csharp
private void cbDeviceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (cbDeviceList.SelectedIndex < 0)
    {
        return;
    }

    // If previewing, stop the preview since the selected camera has changed.
    if (m_isPreviewing)
    {
        m_mediaPlayer.Pause();
        m_isPreviewing = false;
    }

    // Dispose of existing MediaCapture and MediaPlayer, since they are
    // initialized for a specific camera device.
    if (m_mediaPlayer != null)
    {
        m_mediaPlayer.Dispose();
        m_mediaPlayer = null;
    }

    if (m_mediaCapture != null)
    {
        m_mediaCapture.Dispose();
        m_mediaCapture = null;
    }

    m_frameSource = null;

    bStartMediaCapture.IsEnabled = true;
    bStartPreview.IsEnabled = false;
    bStopPreview.IsEnabled = false;

    tbStatus.Text = "Camera selection changed. Click 'Initialize MediaCapture' to continue.";
}
```


## Initialize the MediaCapture object 

Initialize the **MediaCapture** object by calling [InitializeAsync](/uwp/api/windows.media.capture.mediacapture.initializeasync), passing in a [MediaCaptureInitializationSettings](/uwp/api/windows.media.capture.mediacaptureinitializationsettings) object containing the requested initialization parameters. There are a lot of optional initialization parameters that enable different scenarios. See the API reference page for the complete list. In this simple example we specify a few basic settings, including:

- The [VideoDeviceId](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.videodeviceid) property specifies the unique identifier of the camera that the **MediaCapture** will attach to. We get the device ID from the **DeviceInformationCollection**, using the selected index of the **ComboBox**.
- The [SharingMode](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.sharingmode) property specifies whether the app is requesting shared, read-only access to the camera, which allows you to view and capture from the video stream, or exclusive control of the camera, which allows you to change the camera configuration. Multiple apps can read from a camera simultaneously, but only one app at a time can have exclusive control.
- The [StreamingCaptureMode](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.streamingcapturemode) property specifies whether we want to capture video, audio, or audio and video.
- The [MediaCaptureMemoryPreference](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.memorypreference) allows us to request to specifically use CPU memory for video frames. The value **Auto** lets the system use GPU memory if it's available.

Before initializing the **MediaCapture** object we call [AppCapability.CheckAccess](/uwp/api/windows.security.authorization.appcapabilityaccess.appcapability.checkaccess) method to determine if the user has denied our app access to the camera in Windows Settings.

> [!NOTE]
> Windows allows users to grant or deny access to the device's camera in Windows Settings, under **Privacy & Security -> Camera**. When initializing the capture device, apps should check whether they have access to the camera and handle the case where access is denied by the user. For more information, see [Handle the Windows camera privacy setting](camera-privacy-setting.md).

The **InitializeAsync** call is made from inside a **try** block so that we can recover if initialization fails. Apps should handle initialization failure gracefully. In this simple example, we'll just display an error message on failure.

```csharp
private async void bStartMediaCapture_Click(object sender, RoutedEventArgs e)
{
    if (m_mediaCapture != null)
    {
        tbStatus.Text = "MediaCapture already initialized.";
        return;
    }

    // Supported in Windows Build 18362 and later
    if(AppCapability.Create("Webcam").CheckAccess() != AppCapabilityAccessStatus.Allowed)
    {
        tbStatus.Text = "Camera access denied. Launching settings.";

        bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));

        if (AppCapability.Create("Webcam").CheckAccess() != AppCapabilityAccessStatus.Allowed)
        {
            tbStatus.Text = "Camera access denied in privacy settings.";
            return;
        }
    }

    try
    {
        m_mediaCapture = new MediaCapture();
        var mediaCaptureInitializationSettings = new MediaCaptureInitializationSettings()
        {
            VideoDeviceId = m_deviceList[cbDeviceList.SelectedIndex].Id,
            SharingMode = MediaCaptureSharingMode.ExclusiveControl,
            StreamingCaptureMode = StreamingCaptureMode.Video,
            MemoryPreference = MediaCaptureMemoryPreference.Auto
        };

        await m_mediaCapture.InitializeAsync(mediaCaptureInitializationSettings);

        tbStatus.Text = "MediaCapture initialized successfully.";

        bStartPreview.IsEnabled = true;
    }
    catch (Exception ex)
    {
        tbStatus.Text = "Initialize media capture failed: " + ex.Message;
    }
}
```

## Initialize the camera preview

When the user clicks the start preview button, we will attempt to create a **MediaFrameSource** for a video stream from the camera device with which the **MediaCapture** object was initialized. The available frame sources are exposed by the [MediaCapture.FrameSources](/uwp/api/windows.media.capture.mediacapture.framesources) property.

 To find a frame source that is color video data, as opposed to a depth camera for example, we look for a frame source that has a [SourceKind](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.sourcekind) of [Color](/uwp/api/windows.media.capture.frames.mediaframesourcekind). Some camera drivers provide a dedicated preview stream that is separate from the record stream. To get the preview video stream, we try to select a frame source that has a [MediaStreamType](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.mediastreamtype) of [VideoPreview](/uwp/api/windows.media.capture.mediastreamtype). If no preview streams are found, we can get the record video stream by selecting a **MediaStreamType** of **VideoRecord**. If neither of these frame sources are available, then this capture device can't be used for video preview.

Once we have selected a frame source, we create a new [MediaPlayer](/uwp/api/windows.media.playback.mediaplayer) object that will be rendered by the **MediaPlayerElement** in our UI. We set the [Source](/uwp/api/windows.media.playback.mediaplayer.source) property of the **MediaPlayer** to a new [MediaSource](/uwp/api/windows.media.core.mediasource) object we create from our selected **MediaFrameSource**.

Call [Play](/uwp/api/windows.media.playback.mediaplayer.play) on the **MediaPlayer** object to begin rendering the video stream.

```csharp
private void bStartPreview_Click(object sender, RoutedEventArgs e)
{

    m_frameSource = null;

    // Find preview source.
    // The preferred preview stream from a camera is defined by MediaStreamType.VideoPreview on the RGB camera (SourceKind == color).
    var previewSource = m_mediaCapture.FrameSources.FirstOrDefault(source => source.Value.Info.MediaStreamType == MediaStreamType.VideoPreview
                                                                                && source.Value.Info.SourceKind == MediaFrameSourceKind.Color).Value;

    if (previewSource != null)
    {
        m_frameSource = previewSource;
    }
    else
    {
        var recordSource = m_mediaCapture.FrameSources.FirstOrDefault(source => source.Value.Info.MediaStreamType == MediaStreamType.VideoRecord
                                                                                   && source.Value.Info.SourceKind == MediaFrameSourceKind.Color).Value;
        if (recordSource != null)
        {
            m_frameSource = recordSource;
        }
    }

    if (m_frameSource == null)
    {
        tbStatus.Text = "No video preview or record stream found.";
        return;
    }



    // Create MediaPlayer with the preview source
    m_mediaPlayer = new MediaPlayer();
    m_mediaPlayer.RealTimePlayback = true;
    m_mediaPlayer.AutoPlay = false;
    m_mediaPlayer.Source = MediaSource.CreateFromMediaFrameSource(m_frameSource);
    m_mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;

    // Set the mediaPlayer on the MediaPlayerElement
    mpePreview.SetMediaPlayer(m_mediaPlayer);

    // Start preview
    m_mediaPlayer.Play();


    tbStatus.Text = "Start preview succeeded!";
    m_isPreviewing = true;
    bStartPreview.IsEnabled = false;
    bStopPreview.IsEnabled = true;
}
```

Implement a handler for the [MediaFailed](/uwp/api/windows.media.playback.mediaplayer.mediafailed) event so that you can handle errors rendering the preview.

```csharp
private void MediaPlayer_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
{
    tbStatus.Text = "MediaPlayer error: " + args.ErrorMessage;
}
```

## Stop the camera preview

To stop the camera preview, call [Pause](/uwp/api/windows.media.playback.mediaplayer.pause) on the **MediaPlayer** object.

```csharp
private void bStopPreview_Click(object sender, RoutedEventArgs e)
{
    // Stop preview
    m_mediaPlayer.Pause();
    m_isPreviewing = false;
    bStartPreview.IsEnabled = true;
    bStopPreview.IsEnabled = false;
}
```


## Reset the app

To make it easier to test out the sample app, add a method to reset the state of the app. Camera apps should always dispose of the camera and associated resources when the camera is no longer needed.

```csharp
private void bReset_Click(object sender, RoutedEventArgs e)
{
    if (m_mediaCapture != null)
    {
        m_mediaCapture.Dispose();
        m_mediaCapture = null;
    }

    if(m_mediaPlayer != null)
    {
        m_mediaPlayer.Dispose();
        m_mediaPlayer = null;
    }

    m_frameSource = null;


    bStartMediaCapture.IsEnabled = false;
    bStartPreview.IsEnabled = false;
    bStopPreview.IsEnabled = false;

    PopulateCameraList();

}
```
