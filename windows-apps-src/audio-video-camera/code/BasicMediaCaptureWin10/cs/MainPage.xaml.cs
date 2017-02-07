//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Diagnostics;
using System.Linq;

using Windows.ApplicationModel;

using Windows.Foundation;
using Windows.Foundation.Metadata;





using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;


//<SnippetUsing>
using System.Threading.Tasks;         // Used to implement asynchronous methods
using Windows.Devices.Enumeration;    // Used to enumerate cameras on the device
using Windows.Devices.Sensors;        // Orientation sensor is used to rotate the camera preview
using Windows.Graphics.Display;       // Used to determine the display orientation
using Windows.Graphics.Imaging;       // Used for encoding captured images
using Windows.Media;                  // Provides SystemMediaTransportControls
using Windows.Media.Capture;          // MediaCapture APIs
using Windows.Media.MediaProperties;  // Used for photo and video encoding
using Windows.Storage;                // General file I/O
using Windows.Storage.FileProperties; // Used for image file encoding
using Windows.Storage.Streams;        // General file I/O
using Windows.System.Display;         // Used to keep the screen awake during preview and capture
using Windows.UI.Core;                // Used for updating UI from within async operations
//</SnippetUsing>


//<SnippetPhoneUsing>
using Windows.Phone.UI.Input;
//</SnippetPhoneUsing>


//<SnippetFaceDetectionUsing>
using Windows.Media.Core;
//</SnippetFaceDetectionUsing>

//<SnippetSceneAnalysisUsing>
using Windows.Media.Core;
using Windows.Media.Devices;
//</SnippetSceneAnalysisUsing>

//<SnippetHDRPhotoUsing>
using Windows.Media.Core;
using Windows.Media.Devices;
//</SnippetHDRPhotoUsing>

//<SnippetVideoControllersUsing>
using Windows.Media.Devices;
//</SnippetVideoControllersUsing>

//<SnippetVideoTransformEffectUsing>
using Windows.Media.Effects;
//</SnippetVideoTransformEffectUsing>

//<SnippetVideoStabilizationEffectUsing>
using Windows.Media.Core;
//</SnippetVideoStabilizationEffectUsing>

//<SnippetVPSUsing>
using Windows.Media.Capture.Core;
using Windows.Media.Devices.Core;
//</SnippetVPSUsing>

//<SnippetPreviewFrameUsing>
using Windows.Media;
//</SnippetPreviewFrameUsing>

//<SnippetMediaEncodingPropertiesUsing>
using Windows.Media.MediaProperties;
//</SnippetMediaEncodingPropertiesUsing>


namespace BasicCameraWin10
{

    public sealed partial class MainPage : Page
    {
        // Receive notifications about rotation of the device and UI and apply any necessary rotation to the preview stream and UI controls
        //<SnippetDisplayInformationAndOrientation>       
        private readonly DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();
        private DisplayOrientations _displayOrientation = DisplayOrientations.Portrait;

        private readonly SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();
        private SimpleOrientation _deviceOrientation = SimpleOrientation.NotRotated;
        //</SnippetDisplayInformationAndOrientation> 

        // Rotation metadata to apply to the preview stream and recorded videos (MF_MT_VIDEO_ROTATION)
        // Reference: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868174.aspx
        //<SnippetRotationKey>
        private static readonly Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");
        //</SnippetRotationKey>

        // Prevent the screen from sleeping while the camera is running
        //<SnippetDisplayRequest>
        private readonly DisplayRequest _displayRequest = new DisplayRequest();
        //</SnippetDisplayRequest>

        // MediaCapture and its state variables
        //<SnippetMediaCaptureVariables>
        private MediaCapture _mediaCapture;
        private bool _isInitialized;
        private bool _isPreviewing;
        private bool _isRecording;

        //</SnippetMediaCaptureVariables>

        //<SnippetPreviewVariables>
        private bool _externalCamera;
        private bool _mirroringPreview;
        //</SnippetPreviewVariables>
        // Information about the camera device


        //<SnippetDeclareSystemMediaTransportControls>
        private readonly SystemMediaTransportControls _systemMediaControls = SystemMediaTransportControls.GetForCurrentView();
        //</SnippetDeclareSystemMediaTransportControls>

        #region Constructor, lifecycle and navigation

        public MainPage()
        {
            this.InitializeComponent();

            // Do not cache the state of the UI when suspending/navigating
            NavigationCacheMode = NavigationCacheMode.Disabled;

            //<SnippetRegisterAppLifetimeEvents>
            Application.Current.Suspending += Application_Suspending;
            Application.Current.Resuming += Application_Resuming;
            //</SnippetRegisterAppLifetimeEvents>
        }
        //<SnippetSuspending>
        private async void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                var deferral = e.SuspendingOperation.GetDeferral();

                UnregisterOrientationEventHandlers();

                _systemMediaControls.PropertyChanged -= SystemMediaControls_PropertyChanged;

                await CleanupCameraAsync();

                deferral.Complete();
            }
        }
        //</SnippetSuspending>
        //<SnippetResuming>
        private async void Application_Resuming(object sender, object o)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                RegisterOrientationEventHandlers();

                _systemMediaControls.PropertyChanged += SystemMediaControls_PropertyChanged;

                await InitializeCameraAsync();
            }
        }
        //</SnippetResuming>

        //<SnippetOnNavigatedTo>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            RegisterOrientationEventHandlers();

            _systemMediaControls.PropertyChanged += SystemMediaControls_PropertyChanged;

            await InitializeCameraAsync();

            SetPowerlineFrequency();
        }
        //</SnippetOnNavigatedTo>
        //<SnippetOnNavigatingFrom>
        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            UnregisterOrientationEventHandlers();

            _systemMediaControls.PropertyChanged -= SystemMediaControls_PropertyChanged;

            await CleanupCameraAsync();

        }
        //</SnippetOnNavigatingFrom>

        //<SnippetSystemMediaControlsPropertyChanged>
        private async void SystemMediaControls_PropertyChanged(SystemMediaTransportControls sender, SystemMediaTransportControlsPropertyChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                // Only handle this event if this page is currently being displayed
                if (args.Property == SystemMediaTransportControlsProperty.SoundLevel && Frame.CurrentSourcePageType == typeof(MainPage))
                {
                    // Check to see if the app is being muted. If so, it is being minimized.
                    // Otherwise if it is not initialized, it is being brought into focus.
                    if (sender.SoundLevel == SoundLevel.Muted)
                    {
                        await CleanupCameraAsync();
                    }
                    else if (!_isInitialized)
                    {
                        await InitializeCameraAsync();
                    }
                }
            });
        }
        //</SnippetSystemMediaControlsPropertyChanged>
        private async void ExtractedFromNavigationAppLifetimeEvents()
        {
            await SetupUiAsync();

            await CleanupUiAsync();
        }
        #endregion Constructor, lifecycle and navigation


        #region Event handlers

        /// <summary>
        /// Occurs each time the simple orientation sensor reports a new sensor reading.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data.</param>
        //<SnippetSimpleOrientationChanged>
        private void OrientationSensor_OrientationChanged(SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            if (args.Orientation != SimpleOrientation.Faceup && args.Orientation != SimpleOrientation.Facedown)
            {
                _deviceOrientation = args.Orientation;
            }
        }
        //</SnippetSimpleOrientationChanged>

        /// <summary>
        /// This event will fire when the page is rotated, when the DisplayInformation.AutoRotationPreferences value set in the SetupUiAsync() method cannot be not honored.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data.</param>
        /// 
        // <SnippetDisplayOrientationChanged>
        private async void DisplayInformation_OrientationChanged(DisplayInformation sender, object args)
        {
            _displayOrientation = sender.CurrentOrientation;

            if (_isPreviewing)
            {
                await SetPreviewRotationAsync();
            }

        }
        // </SnippetDisplayOrientationChanged>

        private async void PhotoButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //await TakePhotoAsync();

            InitRotationHelper();
            //IsLowLightPhotoSupported();
            //if (_lowLightSupported)
            //{
            //    await CreateAdvancedCaptureLowLightAsync();
            //    await CaptureLowLightAsync();
            //}

            IsHdrPhotoSupported();
            if (_hdrSupported)
            {
                await CreateAdvancedCaptureAsync();
                CaptureWithContext();
            }
        }

        private async void VideoButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!_isRecording)
            {
                await StartRecordingAsync();
            }
            else
            {
                await StopRecordingAsync();
            }

            // After starting or stopping video recording, update the UI to reflect the MediaCapture state
            UpdateCaptureControls();
        }
        //<SnippetCameraPressed>
        private async void HardwareButtons_CameraPressed(object sender, CameraEventArgs e)
        {
            await TakePhotoAsync();
        }
        //</SnippetCameraPressed>

        //<SnippetRecordLimitationExceeded>
        private async void MediaCapture_RecordLimitationExceeded(MediaCapture sender)
        {
            await StopRecordingAsync();
        }
        //</SnippetRecordLimitationExceeded>

        private async void RemovedFromCaptureFailureEvent()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateCaptureControls());
        }
        //<SnippetCaptureFailed>
        private async void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            await CleanupCameraAsync();
        }
        //</SnippetCaptureFailed>


        #endregion Event handlers


        #region MediaCapture methods

        /// <summary>
        /// Initializes the MediaCapture, registers events, gets camera device information for mirroring and rotating, starts preview and unlocks the UI
        /// </summary>
        /// <returns></returns>
        //<SnippetInitializeCameraAsync>
        private async Task InitializeCameraAsync()
        {

            if (_mediaCapture == null)
            {

                // Get available devices for capturing pictures
                var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

                // Get the desired camera by panel
                DeviceInformation cameraDevice =
                    allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null &&
                    x.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back);

                // If there is no camera on the specified panel, get any camera
                cameraDevice = cameraDevice ?? allVideoDevices.FirstOrDefault();

                if (cameraDevice == null)
                {
                    ShowMessageToUser("No camera device found.");
                    return;
                }

                // Create MediaCapture and its settings
                _mediaCapture = new MediaCapture();

                // Register for a notification when video recording has reached the maximum time and when something goes wrong
                _mediaCapture.RecordLimitationExceeded += MediaCapture_RecordLimitationExceeded;

                var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };

                // Initialize MediaCapture
                try
                {
                    await _mediaCapture.InitializeAsync(mediaInitSettings);
                    _isInitialized = true;
                }
                catch (UnauthorizedAccessException)
                {
                    ShowMessageToUser("The app was denied access to the camera");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception when initializing MediaCapture with {0}: {1}", cameraDevice.Id, ex.ToString());
                }

                // If initialization succeeded, start the preview
                if (_isInitialized)
                {
                    // Figure out where the camera is located
                    if (cameraDevice.EnclosureLocation == null || cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Unknown)
                    {
                        // No information on the location of the camera, assume it's an external camera, not integrated on the device
                        _externalCamera = true;
                    }
                    else
                    {
                        // Camera is fixed on the device
                        _externalCamera = false;

                        // Only mirror the preview if the camera is on the front panel
                        _mirroringPreview = (cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
                    }

                    await StartPreviewAsync();

                    UpdateCaptureControls();
                }
            }
        }
        //</SnippetInitializeCameraAsync>

        /// <summary>
        /// Starts the preview and adjusts it for for rotation and mirroring after making a request to keep the screen on
        /// </summary>
        /// <returns></returns>
        //<SnippetStartPreviewAsync>
        private async Task StartPreviewAsync()
        {
            // Prevent the device from sleeping while the preview is running
            _displayRequest.RequestActive();

            // Set the preview source in the UI and mirror it if necessary
            PreviewControl.Source = _mediaCapture;
            PreviewControl.FlowDirection = _mirroringPreview ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            // Start the preview
            try
            {
                await _mediaCapture.StartPreviewAsync();
                _isPreviewing = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when starting the preview: {0}", ex.ToString());
            }

            // Initialize the preview to the current orientation
            if (_isPreviewing)
            {
                await SetPreviewRotationAsync();
            }
        }
        //</SnippetStartPreviewAsync>

        /// <summary>
        /// Gets the current orientation of the UI in relation to the device (when AutoRotationPreferences cannot be honored) and applies a corrective rotation to the preview
        /// </summary>
        //<SnippetSetPreviewRotation>
        private async Task SetPreviewRotationAsync()
        {
            // Only need to update the orientation if the camera is mounted on the device
            if (_externalCamera) return;

            // Populate orientation variables with the current state
            _displayOrientation = _displayInformation.CurrentOrientation;

            // Calculate which way and how far to rotate the preview
            int rotationDegrees = ConvertDisplayOrientationToDegrees(_displayOrientation);

            // The rotation direction needs to be inverted if the preview is being mirrored
            if (_mirroringPreview)
            {
                rotationDegrees = (360 - rotationDegrees) % 360;
            }

            // Add rotation metadata to the preview stream to make sure the aspect ratio / dimensions match when rendering and getting preview frames
            var props = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview);
            props.Properties.Add(RotationKey, rotationDegrees);
            await _mediaCapture.SetEncodingPropertiesAsync(MediaStreamType.VideoPreview, props, null);

        }
        //</SnippetSetPreviewRotation>

        /// <summary>
        /// Stops the preview and deactivates a display request, to allow the screen to go into power saving modes
        /// </summary>
        /// <returns></returns>
        //<SnippetStopPreviewAsync>
        private async Task StopPreviewAsync()
        {
            // Stop the preview
            try
            {
                _isPreviewing = false;
                await _mediaCapture.StopPreviewAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when stopping the preview: {0}", ex.ToString());
            }

            // Use the dispatcher because this method is sometimes called from non-UI threads
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Cleanup the UI
                PreviewControl.Source = null;

                // Allow the device screen to sleep now that the preview is stopped
                _displayRequest.RequestRelease();
            });
        }
        //</SnippetStopPreviewAsync>

        /// <summary>
        /// Takes a photo to a StorageFile and adds rotation metadata to it
        /// </summary>
        /// <returns></returns>
        /// 
        //<SnippetTakePhotoAsync>
        private async Task TakePhotoAsync()
        {
            var stream = new InMemoryRandomAccessStream();

            try
            {
                Debug.WriteLine("Taking photo...");
                await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);
                Debug.WriteLine("Photo taken!");

                var photoOrientation = ConvertOrientationToPhotoOrientation(GetCameraOrientation());
                await ReencodeAndSavePhotoAsync(stream, "photo.jpg", photoOrientation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when taking a photo: {0}", ex.ToString());
            }

        }
        //</SnippetTakePhotoAsync>

        private void VideoButtonExtractedFromTakePhotoAsync()
        {
            // While taking a photo, keep the video button enabled only if the camera supports simultaneously taking pictures and recording video
            VideoButton.IsEnabled = _mediaCapture.MediaCaptureSettings.ConcurrentRecordAndPhotoSupported;
            VideoButton.Opacity = 0;

            // Done taking a photo, so re-enable the button
            VideoButton.IsEnabled = true;
            VideoButton.Opacity = 1;
        }

        /// <summary>
        /// Records an MP4 video to a StorageFile and adds rotation metadata to it
        /// </summary>
        /// <returns></returns>
        /// 
        //<SnippetStartRecordingAsync>
        private async Task StartRecordingAsync()
        {
            try
            {
                // Create storage file in Pictures Library
                var videoFile = await KnownFolders.PicturesLibrary.CreateFileAsync("SimpleVideo.mp4", CreationCollisionOption.GenerateUniqueName);

                var encodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto);
           
                // Calculate rotation angle, taking mirroring into account if necessary
                var rotationAngle = 360 - ConvertDeviceOrientationToDegrees(GetCameraOrientation());
                encodingProfile.Video.Properties.Add(RotationKey, PropertyValue.CreateInt32(rotationAngle));

                Debug.WriteLine("Starting recording...");

                await _mediaCapture.StartRecordToStorageFileAsync(encodingProfile, videoFile);
                _isRecording = true;

                Debug.WriteLine("Started recording!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when starting video recording: {0}", ex.ToString());
            }
        }
        //</SnippetStartRecordingAsync>

        /// <summary>
        /// Stops recording a video
        /// </summary>
        /// <returns></returns>
        /// 
        //<SnippetStopRecordingAsync>
        private async Task StopRecordingAsync()
        {
            try
            {
                Debug.WriteLine("Stopping recording...");

                _isRecording = false;
                await _mediaCapture.StopRecordAsync();

                Debug.WriteLine("Stopped recording!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when stopping video recording: {0}", ex.ToString());
            }
        }
        //</SnippetStopRecordingAsync>

        
        private async void PauseRecordingAsync()
        {
            //<SnippetPauseRecordingAsync>
            await _mediaCapture.PauseRecordAsync(MediaCapturePauseBehavior.RetainHardwareResources);
            //</SnippetPauseRecordingAsync>
        }


        
        private async void ResumeRecordingAsync()
        {
            //<SnippetResumeRecordingAsync>
            await _mediaCapture.ResumeRecordAsync();
            //</SnippetResumeRecordingAsync>
        }


        /// <summary>
        /// Cleans up the camera resources (after stopping any video recording and/or preview if necessary) and unregisters from MediaCapture events
        /// </summary>
        /// <returns></returns>
        /// 
        //<SnippetCleanupCameraAsync>
        private async Task CleanupCameraAsync()
        {
            Debug.WriteLine("CleanupCameraAsync");

            if (_isInitialized)
            {
                // If a recording is in progress during cleanup, stop it to save the recording
                if (_isRecording)
                {
                    await StopRecordingAsync();
                }

                if (_isPreviewing)
                {
                    // The call to MediaCapture.Dispose() will automatically stop the preview
                    // but manually stopping the preview is good practice
                    await StopPreviewAsync();
                }

                _isInitialized = false;
            }

            if (_mediaCapture != null)
            {
                _mediaCapture.RecordLimitationExceeded -= MediaCapture_RecordLimitationExceeded;
                _mediaCapture.Failed -= MediaCapture_Failed;
                _mediaCapture.Dispose();
                _mediaCapture = null;
            }
        }
        //</SnippetCleanupCameraAsync>
        #endregion MediaCapture methods


        #region Helper functions


        private void SetAutoRotationPreferences()
        {
            //<SnippetSetAutoRotationPreferences>
            // Attempt to lock page to landscape orientation to prevent the CaptureElement from rotating, as this gives a better experience
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            //</SnippetSetAutoRotationPreferences>

            //<SnippetRevertAutoRotationPreferences>
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
            //</SnippetRevertAutoRotationPreferences>
        }


        /// <summary>
        /// Attempts to lock the page orientation, hide the StatusBar (on Phone) and registers event handlers for hardware buttons and orientation sensors
        /// </summary>
        /// <returns></returns>
        private async Task SetupUiAsync()
        {

            //<SnippetHideStatusBar>
            // Hide the status bar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            }
            //</SnippetHideStatusBar>

            //<SnippetRegisterCameraButtonHandler>
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.CameraPressed += HardwareButtons_CameraPressed;
            }
            //</SnippetRegisterCameraButtonHandler>

        }

        /// <summary>
        /// Unregisters event handlers for hardware buttons and orientation sensors, allows the StatusBar (on Phone) to show, and removes the page orientation lock
        /// </summary>
        /// <returns></returns>
        private async Task CleanupUiAsync()
        {
            //<SnippetShowStatusBar>
            // Show the status bar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ShowAsync();
            }
            //</SnippetShowStatusBar>

            //<SnippetUnregisterCameraButtonHandler>
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.CameraPressed -= HardwareButtons_CameraPressed;
            }
            //</SnippetUnregisterCameraButtonHandler>
        }

        /// <summary>
        /// This method will update the icons, enable/disable and show/hide the photo/video buttons depending on the current state of the app and the capabilities of the device
        /// </summary>
        /// 
        //<SnippetUpdateCaptureControls>
        private void UpdateCaptureControls()
        {
            // The buttons should only be enabled if the preview started sucessfully
            PhotoButton.IsEnabled = _isPreviewing;
            VideoButton.IsEnabled = _isPreviewing;

            // Update recording button to show "Stop" icon instead of red "Record" icon
            StartRecordingIcon.Visibility = _isRecording ? Visibility.Collapsed : Visibility.Visible;
            StopRecordingIcon.Visibility = _isRecording ? Visibility.Visible : Visibility.Collapsed;

            // If the camera doesn't support simultaneosly taking pictures and recording video, disable the photo button on record
            if (_isInitialized && !_mediaCapture.MediaCaptureSettings.ConcurrentRecordAndPhotoSupported)
            {
                PhotoButton.IsEnabled = !_isRecording;

                // Make the button invisible if it's disabled, so it's obvious it cannot be interacted with
                PhotoButton.Opacity = PhotoButton.IsEnabled ? 1 : 0;
            }
        }
        //</SnippetUpdateCaptureControls>

        //<SnippetRegisterOrientationEventHandlers>
        private void RegisterOrientationEventHandlers()
        {
            // If there is an orientation sensor present on the device, register for notifications
            if (_orientationSensor != null)
            {
                _orientationSensor.OrientationChanged += OrientationSensor_OrientationChanged;
                _deviceOrientation = _orientationSensor.GetCurrentOrientation();
            }

            _displayInformation.OrientationChanged += DisplayInformation_OrientationChanged;
            _displayOrientation = _displayInformation.CurrentOrientation;


        }
        //</SnippetRegisterOrientationEventHandlers>

        private async void SnippetUpdateButtonOrientation()
        {
            // Update orientation of buttons with the current orientation
            UpdateButtonOrientation();

            // OR

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateButtonOrientation());
        }

        //<SnippetUnregisterOrientationEventHandlers>
        private void UnregisterOrientationEventHandlers()
        {
            if (_orientationSensor != null)
            {
                _orientationSensor.OrientationChanged -= OrientationSensor_OrientationChanged;
            }

            _displayInformation.OrientationChanged -= DisplayInformation_OrientationChanged;
        }
        //</SnippetUnregisterOrientationEventHandlers>



        /*
        /// <summary>
        /// Attempts to find and return a device mounted on the panel specified, and on failure to find one it will return the first device listed
        /// </summary>
        /// <param name="desiredPanel">The desired panel on which the returned device should be mounted, if available</param>
        /// <returns></returns>
        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {
            // Get available devices for capturing pictures
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // Get the desired camera by panel
            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

            // If there is no device mounted on the desired panel, return the first device found
            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }
        */

        /// <summary>
        /// Applies the given orientation to a photo stream and saves it as a StorageFile
        /// </summary>
        /// <param name="stream">The photo stream</param>
        /// <param name="photoOrientation">The orientation metadata to apply to the photo</param>
        /// <returns></returns>
        /// 
        //<SnippetReencodeAndSavePhotoAsync>
        private static async Task ReencodeAndSavePhotoAsync(IRandomAccessStream stream, string filename, PhotoOrientation photoOrientation)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);

                var file = await KnownFolders.PicturesLibrary.CreateFileAsync(filename, CreationCollisionOption.GenerateUniqueName);

                using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                    var properties = new BitmapPropertySet { { "System.Photo.Orientation", new BitmapTypedValue(photoOrientation, PropertyType.UInt16) } };

                    await encoder.BitmapProperties.SetPropertiesAsync(properties);
                    await encoder.FlushAsync();
                }
            }
        }
        //</SnippetReencodeAndSavePhotoAsync>


        #endregion Helper functions


        #region Rotation helpers

        /// <summary>
        /// Calculates the current camera orientation from the device orientation by taking into account whether the camera is external or facing the user
        /// </summary>
        /// <returns>The camera orientation in space, with an inverted rotation in the case the camera is mounted on the device and is facing the user</returns>
        //<SnippetGetCameraOrientation>
        private SimpleOrientation GetCameraOrientation()
        {
            if (_externalCamera)
            {
                // Cameras that are not attached to the device do not rotate along with it, so apply no rotation
                return SimpleOrientation.NotRotated;
            }

            // If the preview is being mirrored for a front-facing camera, then the rotation should be inverted
            if (_mirroringPreview)
            {
                // This only affects the 90 and 270 degree cases, because rotating 0 and 180 degrees is the same clockwise and counter-clockwise
                switch (_deviceOrientation)
                {
                    case SimpleOrientation.Rotated90DegreesCounterclockwise:
                        return SimpleOrientation.Rotated270DegreesCounterclockwise;
                    case SimpleOrientation.Rotated270DegreesCounterclockwise:
                        return SimpleOrientation.Rotated90DegreesCounterclockwise;
                }
            }

            return _deviceOrientation;
        }

            
        //</SnippetGetCameraOrientation>

        /// <summary>
        /// Converts the given orientation of the device in space to the corresponding rotation in degrees
        /// </summary>
        /// <param name="orientation">The orientation of the device in space</param>
        /// <returns>An orientation in degrees</returns>
        /// 
        //<SnippetConvertDeviceOrientationToDegrees>
        private static int ConvertDeviceOrientationToDegrees(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return 90;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return 180;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return 270;
                case SimpleOrientation.NotRotated:
                default:
                    return 0;
            }
        }
        //</SnippetConvertDeviceOrientationToDegrees>

        /// <summary>
        /// Converts the given orientation of the app on the screen to the corresponding rotation in degrees
        /// </summary>
        /// <param name="orientation">The orientation of the app on the screen</param>
        /// <returns>An orientation in degrees</returns>
        /// 
        //<SnippetConvertDisplayOrientationToDegrees>
        private static int ConvertDisplayOrientationToDegrees(DisplayOrientations orientation)
        {
            switch (orientation)
            {
                case DisplayOrientations.Portrait:
                    return 90;
                case DisplayOrientations.LandscapeFlipped:
                    return 180;
                case DisplayOrientations.PortraitFlipped:
                    return 270;
                case DisplayOrientations.Landscape:
                default:
                    return 0;
            }
        }
        //</SnippetConvertDisplayOrientationToDegrees>


        /// <summary>
        /// Converts the given orientation of the device in space to the metadata that can be added to captured photos
        /// </summary>
        /// <param name="orientation">The orientation of the device in space</param>
        /// <returns></returns>
        //<SnippetConvertOrientationToPhotoOrientation>
        private static PhotoOrientation ConvertOrientationToPhotoOrientation(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return PhotoOrientation.Rotate90;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return PhotoOrientation.Rotate180;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return PhotoOrientation.Rotate270;
                case SimpleOrientation.NotRotated:
                default:
                    return PhotoOrientation.Normal;
            }
        }
        //</SnippetConvertOrientationToPhotoOrientation>

        /// <summary>
        /// Uses the current device orientation in space and page orientation on the screen to calculate the rotation
        /// transformation to apply to the controls
        /// </summary>
        /// 
        //<SnippetUpdateButtonOrientation>
        private void UpdateButtonOrientation()
        {
            int device = ConvertDeviceOrientationToDegrees(_deviceOrientation);
            int display = ConvertDisplayOrientationToDegrees(_displayOrientation);

            if (_displayInformation.NativeOrientation == DisplayOrientations.Portrait)
            {
                device -= 90;
            }

            // Combine both rotations and make sure that 0 <= result < 360
            var angle = (360 + display + device) % 360;

            // Rotate the buttons in the UI to match the rotation of the device
            var transform = new RotateTransform { Angle = angle };

            PhotoButton.RenderTransform = transform;
            VideoButton.RenderTransform = transform;
        }
        //</SnippetUpdateButtonOrientation>
        #endregion Rotation helpers

        #region Face detection

        //<SnippetDeclareFaceDetectionEffect>
        FaceDetectionEffect _faceDetectionEffect;
        //</SnippetDeclareFaceDetectionEffect>


        private bool AreFaceFocusAndExposureSupported()
        {
            //<SnippetAreFaceFocusAndExposureSupported>
            var regionsControl = _mediaCapture.VideoDeviceController.RegionsOfInterestControl;
            bool faceDetectionFocusAndExposureSupported =
                regionsControl.MaxRegions > 0 &&
                (regionsControl.AutoExposureSupported || regionsControl.AutoFocusSupported);
            //</SnippetAreFaceFocusAndExposureSupported>

            return faceDetectionFocusAndExposureSupported;
        }


        private async Task CreateFaceDetectionEffectAsync()
        {
            //<SnippetCreateFaceDetectionEffectAsync>

            // Create the definition, which will contain some initialization settings
            var definition = new FaceDetectionEffectDefinition();

            // To ensure preview smoothness, do not delay incoming samples
            definition.SynchronousDetectionEnabled = false;

            // In this scenario, choose detection speed over accuracy
            definition.DetectionMode = FaceDetectionMode.HighPerformance;

            // Add the effect to the preview stream
            _faceDetectionEffect = (FaceDetectionEffect)await _mediaCapture.AddVideoEffectAsync(definition, MediaStreamType.VideoPreview);

            // Choose the shortest interval between detection events
            _faceDetectionEffect.DesiredDetectionInterval = TimeSpan.FromMilliseconds(33);

            // Start detecting faces
            _faceDetectionEffect.Enabled = true;

            //</SnippetCreateFaceDetectionEffectAsync>
        }

        private async Task CleanUpFaceDetectionEffectAsync()
        {
            //<SnippetCleanUpFaceDetectionEffectAsync>
            // Disable detection
            _faceDetectionEffect.Enabled = false;

            // Unregister the event handler
            _faceDetectionEffect.FaceDetected -= FaceDetectionEffect_FaceDetected;

            // Remove the effect from the preview stream
            await _mediaCapture.ClearEffectsAsync(MediaStreamType.VideoPreview);

            // Clear the member variable that held the effect instance
            _faceDetectionEffect = null;
            //</SnippetCleanUpFaceDetectionEffectAsync>
        }

        private void RegisterFaceDetectionHandler()
        {
            //<SnippetRegisterFaceDetectionHandler>
            // Register for face detection events
            _faceDetectionEffect.FaceDetected += FaceDetectionEffect_FaceDetected;
            //</SnippetRegisterFaceDetectionHandler>
        }

        //<SnippetFaceDetected>
        private void FaceDetectionEffect_FaceDetected(FaceDetectionEffect sender, FaceDetectedEventArgs args)
        {
            foreach (Windows.Media.FaceAnalysis.DetectedFace face in args.ResultFrame.DetectedFaces)
            {
                BitmapBounds faceRect = face.FaceBox;

                // Draw a rectangle on the preview stream for each face
            }
        }
        //</SnippetFaceDetected>

        #endregion Face detection

        #region Scene Analysis


        // !!!! Be sure to include SceneAnalysis snippet

        //<SnippetDeclareSceneAnalysisEffect>
        private SceneAnalysisEffect _sceneAnalysisEffect;
        //</SnippetDeclareSceneAnalysisEffect>

        private async Task CreateSceneAnalysisEffectAsync()
        {
            //<SnippetCreateSceneAnalysisEffectAsync>
            // Create the definition
            var definition = new SceneAnalysisEffectDefinition();

            // Add the effect to the video record stream
            _sceneAnalysisEffect = (SceneAnalysisEffect)await _mediaCapture.AddVideoEffectAsync(definition, MediaStreamType.VideoPreview);

            // Subscribe to notifications about scene information
            _sceneAnalysisEffect.SceneAnalyzed += SceneAnalysisEffect_SceneAnalyzed;

            // Enable HDR analysis
            _sceneAnalysisEffect.HighDynamicRangeAnalyzer.Enabled = true;
            //</SnippetCreateSceneAnalysisEffectAsync>
       
        }

        double MyCertaintyCap = .5;
        //<SnippetSceneAnalyzed>
        private void SceneAnalysisEffect_SceneAnalyzed(SceneAnalysisEffect sender, SceneAnalyzedEventArgs args)
        {
            double hdrCertainty = args.ResultFrame.HighDynamicRange.Certainty;
            
            // Certainty value is between 0.0 and 1.0
            if(hdrCertainty > MyCertaintyCap)
            {
                ShowMessageToUser("Enabling HDR capture is recommended.");
            }
        }
        //</SnippetSceneAnalyzed>

        private async Task CleanSceneAnalysisEffectAsync()
        {
            //<SnippetCleanUpSceneAnalysisEffectAsync>
            // Disable detection
            _sceneAnalysisEffect.HighDynamicRangeAnalyzer.Enabled = false;

            _sceneAnalysisEffect.SceneAnalyzed -= SceneAnalysisEffect_SceneAnalyzed;

            // Remove the effect from the preview stream
            await _mediaCapture.ClearEffectsAsync(MediaStreamType.VideoPreview);

            // Clear the member variable that held the effect instance
            _sceneAnalysisEffect = null;
            //</SnippetCleanUpSceneAnalysisEffectAsync>
        }

        #endregion Scene Analysis

        #region HDR Photo

        AdvancedCapturedPhoto _testHDRImage;
        CapturedFrame _testReferenceImage;

        // Temp hack to allow CameraRotationHelper in TH1 snippet project
        CameraRotationHelper _rotationHelper;
        public async Task InitRotationHelper()
        {
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // Get the desired camera by panel
            DeviceInformation cameraDevice =
                allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null &&
                x.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back);

            // If there is no camera on the specified panel, get any camera
            cameraDevice = cameraDevice ?? allVideoDevices.FirstOrDefault();

            _rotationHelper = new CameraRotationHelper(cameraDevice.EnclosureLocation);
        }

        //<SnippetDeclareAdvancedCapture>
        private AdvancedPhotoCapture _advancedCapture;
        //</SnippetDeclareAdvancedCapture>

        //<SnippetHdrSupported>
        bool _hdrSupported;
        private void IsHdrPhotoSupported()
        {
            _hdrSupported = _mediaCapture.VideoDeviceController.AdvancedPhotoControl.SupportedModes.Contains(Windows.Media.Devices.AdvancedPhotoMode.Hdr);
        }
        //</SnippetHdrSupported>
        private async Task CreateAdvancedCaptureAsync()
        {
            

            // No work to be done if there already is an AdvancedCapture
            if (_advancedCapture != null) return;

            //<SnippetCreateAdvancedCaptureAsync>
            if (_hdrSupported == false) return;

            // Choose HDR mode
            var settings = new AdvancedPhotoCaptureSettings { Mode = AdvancedPhotoMode.Hdr };

            // Configure the mode
            _mediaCapture.VideoDeviceController.AdvancedPhotoControl.Configure(settings);

            // Prepare for an advanced capture
            _advancedCapture = 
                await _mediaCapture.PrepareAdvancedPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Nv12));

            // Register for events published by the AdvancedCapture
            _advancedCapture.AllPhotosCaptured += AdvancedCapture_AllPhotosCaptured;
            _advancedCapture.OptionalReferencePhotoCaptured += AdvancedCapture_OptionalReferencePhotoCaptured;
            //</SnippetCreateAdvancedCaptureAsync>
        }
        
        private async Task CaptureHdrPhotoAsync()
        {
            //<SnippetCaptureHdrPhotoAsync>
            try
            {

                // Start capture, and pass the context object
                AdvancedCapturedPhoto advancedCapturedPhoto = await _advancedCapture.CaptureAsync();

                using (var frame = advancedCapturedPhoto.Frame)
                {
                    // Read the current orientation of the camera and the capture time
                    var photoOrientation = CameraRotationHelper.ConvertSimpleOrientationToPhotoOrientation(
                        _rotationHelper.GetCameraCaptureOrientation());
                    var fileName = String.Format("SimplePhoto_{0}_HDR.jpg", DateTime.Now.ToString("HHmmss"));
                    await SaveCapturedFrameAsync(frame, fileName, photoOrientation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when taking an HDR photo: {0}", ex.ToString());
            }
            //</SnippetCaptureHdrPhotoAsync>
        }
        

        //<SnippetAdvancedCaptureContext>
        public class MyAdvancedCaptureContextObject
        {
            public string CaptureFileName;
            public PhotoOrientation CaptureOrientation;
        }
        //</SnippetAdvancedCaptureContext>
        private async void CaptureWithContext()
        {
            //<SnippetCaptureWithContext>
            // Read the current orientation of the camera and the capture time
            var photoOrientation = CameraRotationHelper.ConvertSimpleOrientationToPhotoOrientation(
                    _rotationHelper.GetCameraCaptureOrientation());
            var fileName = String.Format("SimplePhoto_{0}_HDR.jpg", DateTime.Now.ToString("HHmmss"));

            // Create a context object, to identify the capture in the OptionalReferencePhotoCaptured event
            var context = new MyAdvancedCaptureContextObject()
            {
                CaptureFileName = fileName,
                CaptureOrientation = photoOrientation
            };

            // Start capture, and pass the context object
            AdvancedCapturedPhoto advancedCapturedPhoto = await _advancedCapture.CaptureAsync(context);
            //</SnippetCaptureWithContext>

            _testHDRImage = advancedCapturedPhoto;
        }

        //<SnippetOptionalReferencePhotoCaptured>
        private async void AdvancedCapture_OptionalReferencePhotoCaptured(AdvancedPhotoCapture sender, OptionalReferencePhotoCapturedEventArgs args)
        {
            // Retrieve the context (i.e. what capture does this belong to?)
            var context = args.Context as MyAdvancedCaptureContextObject;

            // Remove "_HDR" from the name of the capture to create the name of the reference
            var referenceName = context.CaptureFileName.Replace("_HDR", "");

            using (var frame = args.Frame)
            {
                await SaveCapturedFrameAsync(frame, referenceName, context.CaptureOrientation);
            }
        }
        //</SnippetOptionalReferencePhotoCaptured>

        //<SnippetAllPhotosCaptured>
        private void AdvancedCapture_AllPhotosCaptured(AdvancedPhotoCapture sender, object args)
        {
            // Update UI to enable capture button
        }
        //</SnippetAllPhotosCaptured>


        public async void CleanUpAdvancedPhotoCapture()
        {
            //<SnippetCleanUpAdvancedPhotoCapture>
            await _advancedCapture.FinishAsync();
            _advancedCapture = null;
            //</SnippetCleanUpAdvancedPhotoCapture>
        }


        #endregion HDR Photo
        #region Low light photo

        //<SnippetLowLightSupported1>
        bool _lowLightSupported;
        //</SnippetLowLightSupported1>
        private void IsLowLightPhotoSupported()
        {
            //<SnippetLowLightSupported2>
            _lowLightSupported = 
            _mediaCapture.VideoDeviceController.AdvancedPhotoControl.SupportedModes.Contains(Windows.Media.Devices.AdvancedPhotoMode.LowLight);
            //</SnippetLowLightSupported2>

        }
        //</SnippetLowLightSupported>
        private async Task CreateAdvancedCaptureLowLightAsync()
        {
            // No work to be done if there already is an AdvancedCapture
            if (_advancedCapture != null) return;

            //<SnippetCreateAdvancedCaptureLowLightAsync>
            if (_lowLightSupported == false) return;

            // Choose LowLight mode
            var settings = new AdvancedPhotoCaptureSettings { Mode = AdvancedPhotoMode.LowLight };
            _mediaCapture.VideoDeviceController.AdvancedPhotoControl.Configure(settings);

            // Prepare for an advanced capture
            _advancedCapture = 
                await _mediaCapture.PrepareAdvancedPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Nv12));
            //</SnippetCreateAdvancedCaptureLowLightAsync>
        }

        private async Task CaptureLowLightAsync()
        {
            //<SnippetCaptureLowLight>
            AdvancedCapturedPhoto advancedCapturedPhoto = await _advancedCapture.CaptureAsync();
            var photoOrientation = ConvertOrientationToPhotoOrientation(GetCameraOrientation());
            var fileName = String.Format("SimplePhoto_{0}_LowLight.jpg", DateTime.Now.ToString("HHmmss"));
            await SaveCapturedFrameAsync(advancedCapturedPhoto.Frame, fileName, photoOrientation);
            //</SnippetCaptureLowLight>

            //<SnippetSoftwareBitmapFromCapturedFrame>
            SoftwareBitmap bitmap;
            if (advancedCapturedPhoto.Frame.SoftwareBitmap != null)
            {
                bitmap = advancedCapturedPhoto.Frame.SoftwareBitmap;
            }
            //</SnippetSoftwareBitmapFromCapturedFrame>

        }
        private async Task ShowUncompressedNv12()
        {
            //<SnippetUncompressedNv12>
            _advancedCapture =
                await _mediaCapture.PrepareAdvancedPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Nv12));
            //</SnippetUncompressedNv12>
        }

        //<SnippetSaveCapturedFrameAsync>
        private static async Task<StorageFile> SaveCapturedFrameAsync(CapturedFrame frame, string fileName, PhotoOrientation photoOrientation)
        {
            var folder = await KnownFolders.PicturesLibrary.CreateFolderAsync("MyApp", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

            using (var inputStream = frame)
            {
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var decoder = await BitmapDecoder.CreateAsync(inputStream);
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(fileStream, decoder);
                    var properties = new BitmapPropertySet {
                        { "System.Photo.Orientation", new BitmapTypedValue(photoOrientation, PropertyType.UInt16) } };
                    await encoder.BitmapProperties.SetPropertiesAsync(properties);
                    await encoder.FlushAsync();
                }
            }
            return file;
        }
        //</SnippetSaveCapturedFrameAsync>
        #endregion

        #region HDR Video
        //<SnippetSetHdrVideoMode>
        private void SetHdrVideoMode(HdrVideoMode mode)
        {
            if (!_mediaCapture.VideoDeviceController.HdrVideoControl.Supported)
            {
                ShowMessageToUser("HDR Video not available");
                return;
            }

            var hdrVideoModes = _mediaCapture.VideoDeviceController.HdrVideoControl.SupportedModes;

            if (!hdrVideoModes.Contains(mode))
            {
                ShowMessageToUser("HDR Video setting not supported");
                return;
            }

            _mediaCapture.VideoDeviceController.HdrVideoControl.Mode = mode;
        }
        //</SnippetSetHdrVideoMode>

        #endregion HDR Video

        #region Optical image stablization

        //<SnippetSetOpticalImageStabilizationMode>
        private void SetOpticalImageStabilizationMode(OpticalImageStabilizationMode mode)
        {
            if (!_mediaCapture.VideoDeviceController.OpticalImageStabilizationControl.Supported)
            {
                ShowMessageToUser("Optical image stabilization not available");
                return;
            }

            var stabilizationModes = _mediaCapture.VideoDeviceController.OpticalImageStabilizationControl.SupportedModes;

            if (!stabilizationModes.Contains(mode))
            {
                ShowMessageToUser("Optical image stabilization setting not supported");
                return;
            }

            _mediaCapture.VideoDeviceController.OpticalImageStabilizationControl.Mode = mode;
        }
        //</SnippetSetOpticalImageStabilizationMode>
        #endregion Optical image stablization

        #region Exposure priority

        private void EnableExposurePriority()
        {
            //<SnippetEnableExposurePriority>
            if (!_mediaCapture.VideoDeviceController.ExposurePriorityVideoControl.Supported)
            {
                ShowMessageToUser("Exposure priority not available");
                return;
            }
            _mediaCapture.VideoDeviceController.ExposurePriorityVideoControl.Enabled = true;
            //</SnippetEnableExposurePriority>
        }
        #endregion Exposure priority

        #region Video stabilization effect

        //<SnippetDeclareVideoStabilizationEffect>
        private VideoStabilizationEffect _videoStabilizationEffect;
        private VideoEncodingProperties _inputPropertiesBackup;
        private VideoEncodingProperties _outputPropertiesBackup;
        private MediaEncodingProfile _encodingProfile;
        //</SnippetDeclareVideoStabilizationEffect>


        public async Task CreateVideoStabilizationEffectAsync()
        {
            //<SnippetCreateVideoStabilizationEffect>
            // Create the effect definition
            VideoStabilizationEffectDefinition stabilizerDefinition = new VideoStabilizationEffectDefinition();

            // Add the video stabilization effect to media capture
            _videoStabilizationEffect = 
                (VideoStabilizationEffect)await _mediaCapture.AddVideoEffectAsync(stabilizerDefinition, MediaStreamType.VideoRecord);

            _videoStabilizationEffect.EnabledChanged += VideoStabilizationEffect_EnabledChanged;

            await SetUpVideoStabilizationRecommendationAsync();

            _videoStabilizationEffect.Enabled = true;
            //</SnippetCreateVideoStabilizationEffect>

        }

        public void EncodingProfileMember()
        { 
            //<SnippetEncodingProfileMember>
            _encodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto);
            //</SnippetEncodingProfileMember>
        }
        //<SnippetSetUpVideoStabilizationRecommendationAsync>
        private async Task SetUpVideoStabilizationRecommendationAsync()
        {

            // Get the recommendation from the effect based on our current input and output configuration
            var recommendation = _videoStabilizationEffect.GetRecommendedStreamConfiguration(_mediaCapture.VideoDeviceController, _encodingProfile.Video);

            // Handle the recommendation for the input into the effect, which can contain a larger resolution than currently configured, so cropping is minimized
            if (recommendation.InputProperties != null)
            {
                // Back up the current input properties from before VS was activated
                _inputPropertiesBackup = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoRecord) as VideoEncodingProperties;

                // Set the recommendation from the effect (a resolution higher than the current one to allow for cropping) on the input
                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoRecord, recommendation.InputProperties);
            }

            // Handle the recommendations for the output from the effect
            if (recommendation.OutputProperties != null)
            {
                // Back up the current output properties from before VS was activated
                _outputPropertiesBackup = _encodingProfile.Video;

                // Apply the recommended encoding profile for the output
                _encodingProfile.Video = recommendation.OutputProperties;
            }
        }
        //</SnippetSetUpVideoStabilizationRecommendationAsync>
        //<SnippetVideoStabilizationEnabledChanged>
        private async void VideoStabilizationEffect_EnabledChanged(VideoStabilizationEffect sender, VideoStabilizationEffectEnabledChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Update your UI to reflect the change in status
                ShowMessageToUser("video stabilization status: " + sender.Enabled + ". Reason: " + args.Reason);
            });
        }
        //</SnippetVideoStabilizationEnabledChanged>
        public async Task CleanUpVideoStabilizationEffectAsync()
        {
            //<SnippetCleanUpVisualStabilizationEffect>
            // Clear all effects in the pipeline
            await _mediaCapture.ClearEffectsAsync(MediaStreamType.VideoRecord);

            // If backed up settings (stream properties and encoding profile) exist, restore them and clear the backups
            if (_inputPropertiesBackup != null)
            {
                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoRecord, _inputPropertiesBackup);
                _inputPropertiesBackup = null;
            }

            if (_outputPropertiesBackup != null)
            {
                _encodingProfile.Video = _outputPropertiesBackup;
                _outputPropertiesBackup = null;
            }

            _videoStabilizationEffect.EnabledChanged -= VideoStabilizationEffect_EnabledChanged;

            _videoStabilizationEffect = null;
            //</SnippetCleanUpVisualStabilizationEffect>
        }

        #endregion Video stabilization effect

        #region Smooth zoom

        //<SnippetIsSmoothZoomSupported>
        private bool IsSmoothZoomSupported()
        {
            if (!_mediaCapture.VideoDeviceController.ZoomControl.Supported)
            {
                ShowMessageToUser("Digital zoom is not supported on this device.");
                return false;
            }

            var zoomModes = _mediaCapture.VideoDeviceController.ZoomControl.SupportedModes;

            if (!zoomModes.Contains(ZoomTransitionMode.Smooth))
            {
                ShowMessageToUser("Smooth zoom not supported");
                return false;
            }

            return true;
        }
        //</SnippetIsSmoothZoomSupported>


        //<SnippetRegisterPinchGestureHandler>
        private void RegisterPinchGestureHandler()
        {
            if (!IsSmoothZoomSupported())
            {
                return;
            }

            // Enable pinch/zoom gesture for the preview control
            PreviewControl.ManipulationMode = ManipulationModes.Scale;
            PreviewControl.ManipulationDelta += PreviewControl_ManipulationDelta;
        }
        //</SnippetRegisterPinchGestureHandler>

        //<SnippetManipulationDelta>
        private void PreviewControl_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var zoomControl = _mediaCapture.VideoDeviceController.ZoomControl;

            // Example zoom factor calculation based on size of scale gesture
            var zoomFactor = zoomControl.Value * e.Delta.Scale;

            if (zoomFactor < zoomControl.Min) zoomFactor = zoomControl.Min;
            if (zoomFactor > zoomControl.Max) zoomFactor = zoomControl.Max;
            zoomFactor = zoomFactor - (zoomFactor % zoomControl.Step);

            var settings = new ZoomSettings();
            settings.Mode = ZoomTransitionMode.Smooth;
            settings.Value = zoomFactor;

            _mediaCapture.VideoDeviceController.ZoomControl.Configure(settings);

        }
        //</SnippetManipulationDelta>

        #endregion Smooth zoom

        #region Video transform effect

        // Include VideoTransformEffectUsing snippet

        public async Task CreateVideoTransformEffectAsync()
        {
            //<SnippetCreateVideoTransformEffect>
            // Create the effect definition
            VideoTransformEffectDefinition transformDefinition = new VideoTransformEffectDefinition();

            var inputProperties = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoRecord) as VideoEncodingProperties;
            
            transformDefinition.CropRectangle =
                new Rect((inputProperties.Width - 320) / 2, (inputProperties.Height - 320) / 2, 320, 320);
                
            transformDefinition.OutputSize = new Size(320, 320);

            transformDefinition.PaddingColor = Windows.UI.Colors.Blue;

            // Add the video stabilization effect to the preview stream and the record stream
            await _mediaCapture.AddVideoEffectAsync(transformDefinition, MediaStreamType.VideoRecord);

            // Add the video stabilization effect to the preview stream and the record stream
            //await _mediaCapture.AddVideoEffectAsync(transformDefinition, MediaStreamType.VideoPreview);

            _encodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto);
            _encodingProfile.Video.Width = 320;
            _encodingProfile.Video.Height = 320;
            //</SnippetCreateVideoTransformEffect>

        }


        public async Task CleanUpVideoTransformEffectAsync()
        {
            //<SnippetCleanUpVideoTransformEffectAsync>
            // Clear all effects in the pipeline
            await _mediaCapture.ClearEffectsAsync(MediaStreamType.VideoRecord);
            //</SnippetCleanUpVideoTransformEffectAsync>
        }

        #endregion Video transform effect

        #region Camera Profiles

        public void BasicInitExample()
        {   
            /*
            //<SnippetBasicInitExample>
            var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };
            //</SnippetBasicInitExample>
            */
        }
        //<SnippetGetVideoProfileSupportedDeviceIdAsync>
        public async Task<string> GetVideoProfileSupportedDeviceIdAsync(Windows.Devices.Enumeration.Panel panel)
        {
            string deviceId = string.Empty;

            // Finds all video capture devices
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            
            foreach (var device in devices)
            {
                // Check if the device on the requested panel supports Video Profile
                if (MediaCapture.IsVideoProfileSupported(device.Id) && device.EnclosureLocation.Panel == panel)
                {
                    // We've located a device that supports Video Profiles on expected panel
                    deviceId = device.Id;
                    break;
                }
            }

            return deviceId;
        }
        //</SnippetGetVideoProfileSupportedDeviceIdAsync>

        public async void GetWVGA30FPSProfile()
        {
            //<SnippetGetDeviceWithProfileSupport>
            string videoDeviceId = await GetVideoProfileSupportedDeviceIdAsync(Windows.Devices.Enumeration.Panel.Back);

            if (string.IsNullOrEmpty(videoDeviceId))
            {
                // No devices on the specified panel support video profiles. .
                return;
            }
            //</SnippetGetDeviceWithProfileSupport>

            //<SnippetFindWVGA30FPSProfile>

            var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = videoDeviceId };

            IReadOnlyList<MediaCaptureVideoProfile> profiles = MediaCapture.FindAllVideoProfiles(videoDeviceId);

            var match = (from profile in profiles
                         from desc in profile.SupportedRecordMediaDescription
                         where desc.Width == 640 && desc.Height == 480 && Math.Round(desc.FrameRate) == 30
                         select new { profile, desc }).FirstOrDefault();

            if (match != null)
            {
                mediaInitSettings.VideoProfile = match.profile;
                mediaInitSettings.RecordMediaDescription = match.desc;
            }
            else
            {
                // Could not locate a WVGA 30FPS profile, use default video recording profile
                mediaInitSettings.VideoProfile = profiles[0];
            }
            //</SnippetFindWVGA30FPSProfile>

            // Initialize MediaCapture
            //<SnippetInitCaptureWithProfile>
            await _mediaCapture.InitializeAsync(mediaInitSettings);
            //</SnippetInitCaptureWithProfile>


        }

        private async void GetConcurrentProfile()
        {
            // Initialize our concurrency support flag.
            //<SnippetConcurrencySetup>
            MediaCapture mediaCaptureFront = new MediaCapture();
            MediaCapture mediaCaptureBack = new MediaCapture();

            MediaCaptureInitializationSettings mediaInitSettingsFront = new MediaCaptureInitializationSettings();
            MediaCaptureInitializationSettings mediaInitSettingsBack = new MediaCaptureInitializationSettings();

            string frontVideoDeviceId = string.Empty;
            string backVideoDeviceId = string.Empty;

            bool concurrencySupported = false;
            //</SnippetConcurrencySetup>

            //<SnippetFindConcurrencyDevices>
            // Find front and back Device ID of capture device that supports Video Profile
            frontVideoDeviceId = await GetVideoProfileSupportedDeviceIdAsync(Windows.Devices.Enumeration.Panel.Front);
            backVideoDeviceId = await GetVideoProfileSupportedDeviceIdAsync(Windows.Devices.Enumeration.Panel.Back);

            // First check if the devices support video profiles, if not there's no reason to proceed
            if (string.IsNullOrEmpty(frontVideoDeviceId) || string.IsNullOrEmpty(backVideoDeviceId))
            {
                // Either the front or back camera doesn't support video profiles
                return;
            }

            mediaInitSettingsFront.VideoDeviceId = frontVideoDeviceId;
            mediaInitSettingsBack.VideoDeviceId = backVideoDeviceId;
            //</SnippetFindConcurrencyDevices>

            //<SnippetFindConcurrentProfiles>
            // Acquire concurrent profiles available to front and back capture devices, then locate a concurrent
            // profile Id that matches for both devices
            var concurrentProfile = (from frontProfile in MediaCapture.FindConcurrentProfiles(frontVideoDeviceId)
                                     from backProfile in MediaCapture.FindConcurrentProfiles(backVideoDeviceId)
                                     where frontProfile.Id == backProfile.Id
                                     select new { frontProfile, backProfile }).FirstOrDefault();

            if (concurrentProfile != null)
            {
                mediaInitSettingsFront.VideoProfile = concurrentProfile.frontProfile;
                mediaInitSettingsBack.VideoProfile = concurrentProfile.backProfile;
                concurrencySupported = true;
            }
            else
            {
                // There are no concurrent profiles available on this device.
                // Set the Video profile to null to indicate that each camera 
                // must be managed individually.
                mediaInitSettingsFront.VideoProfile = null;
                mediaInitSettingsBack.VideoProfile = null;
            }
            //</SnippetFindConcurrentProfiles>

            //<SnippetInitConcurrentMediaCaptures>
            await mediaCaptureFront.InitializeAsync(mediaInitSettingsFront);
            if (concurrencySupported)
            {
                // Only initialize the back camera if concurrency is available.  
                await mediaCaptureBack.InitializeAsync(mediaInitSettingsBack);
            }
            //</SnippetInitConcurrentMediaCaptures>
        }

        private async void GetHdrProfile()
        {
            //<SnippetGetHdrProfileSetup>
            MediaCaptureInitializationSettings mediaInitSettings = new MediaCaptureInitializationSettings();
            string videoDeviceId = string.Empty;
            bool HdrVideoSupported = false;
            //</SnippetGetHdrProfileSetup>

            //<SnippetFindDeviceHDR>
            // Select the first video capture device found on the back of the device
            videoDeviceId = await GetVideoProfileSupportedDeviceIdAsync(Windows.Devices.Enumeration.Panel.Back);

            if (string.IsNullOrEmpty(videoDeviceId))
            {
                // No devices on the specified panel support video profiles. .
                return;
            }
            //</SnippetFindDeviceHDR>

            //<SnippetFindHDRProfile>
            IReadOnlyList<MediaCaptureVideoProfile> profiles =
                MediaCapture.FindKnownVideoProfiles(videoDeviceId, KnownVideoProfile.VideoRecording);

            // Walk through available profiles, look for first profile with HDR supported Video Profile
            foreach (MediaCaptureVideoProfile profile in profiles)
            {
                IReadOnlyList<MediaCaptureVideoProfileMediaDescription> recordMediaDescription =
                    profile.SupportedRecordMediaDescription;
                foreach (MediaCaptureVideoProfileMediaDescription videoProfileMediaDescription in recordMediaDescription)
                {
                    if (videoProfileMediaDescription.IsHdrVideoSupported)
                    {
                        // We've located the profile and description for HDR Video, set profile and flag
                        mediaInitSettings.VideoProfile = profile;
                        mediaInitSettings.RecordMediaDescription = videoProfileMediaDescription;
                        HdrVideoSupported = true;
                        break;
                    }
                }

                if (HdrVideoSupported)
                {
                    // Profile with HDR support found. Stop looking.
                    break;
                }
            }
            //</SnippetFindHDRProfile>
        }
        
        private async void GetPhotoAndVideoSupport()
        {
            string videoDeviceId = string.Empty;

            // Select the first video capture device found on the back of the device
            videoDeviceId = await GetVideoProfileSupportedDeviceIdAsync(Windows.Devices.Enumeration.Panel.Back);

            if (string.IsNullOrEmpty(videoDeviceId))
            {
                // No devices on the specified panel support video profiles. .
                return;
            }
            var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = videoDeviceId };

            //<SnippetGetPhotoAndVideoSupport>
            var simultaneousPhotoAndVideoSupported = false;

            IReadOnlyList<MediaCaptureVideoProfile> profiles = MediaCapture.FindAllVideoProfiles(videoDeviceId);

            var match = (from profile in profiles
                         where profile.SupportedPhotoMediaDescription.Any() &&
                         profile.SupportedRecordMediaDescription.Any()
                         select profile).FirstOrDefault();

            if (match != null)
            {
                // Simultaneous photo and video supported
                simultaneousPhotoAndVideoSupported = true;
            }
            else
            {
                // Simultaneous photo and video not supported
                simultaneousPhotoAndVideoSupported = false;
            }
            //</SnippetGetPhotoAndVideoSupport>
        }
        #endregion Camera Profiles

        #region Variable Photo Sequence
        //<SnippetVPSMemberVariables>
        VariablePhotoSequenceCapture _photoSequenceCapture;
        SoftwareBitmap[] _images;
        CapturedFrameControlValues[] _frameControlValues;
        int _photoIndex;
        //</SnippetVPSMemberVariables>

        public async Task PrepareVariablePhotoSequence()
        {
            //<SnippetIsVPSSupported>
            var varPhotoSeqController = _mediaCapture.VideoDeviceController.VariablePhotoSequenceController;
            
            if (!varPhotoSeqController.Supported)
            {
                ShowMessageToUser("Variable Photo Sequence is not supported");
                return;
            }
            //</SnippetIsVPSSupported>

            //<SnippetIsExposureCompensationSupported>
            var frameCapabilities = varPhotoSeqController.FrameCapabilities;

            if (!frameCapabilities.ExposureCompensation.Supported)
            {
                ShowMessageToUser("EVCompenstaion is not supported in FrameController");
                return;
            }
            //</SnippetIsExposureCompensationSupported>

            //<SnippetInitFrameControllers>
            var frame0 = new FrameController();
            var frame1 = new FrameController();
            var frame2 = new FrameController();

            frame0.ExposureCompensationControl.Value = -1.0f;
            frame1.ExposureCompensationControl.Value = 0.0f;
            frame2.ExposureCompensationControl.Value = 1.0f;

            varPhotoSeqController.DesiredFrameControllers.Clear();
            varPhotoSeqController.DesiredFrameControllers.Add(frame0);
            varPhotoSeqController.DesiredFrameControllers.Add(frame1);
            varPhotoSeqController.DesiredFrameControllers.Add(frame2);
            //</SnippetInitFrameControllers>

            

            //<SnippetPrepareVPS>
            try
            {
                var imageEncodingProperties = ImageEncodingProperties.CreateJpeg();

                _photoSequenceCapture = await _mediaCapture.PrepareVariablePhotoSequenceCaptureAsync(imageEncodingProperties);

                _photoSequenceCapture.PhotoCaptured += OnPhotoCaptured;
                _photoSequenceCapture.Stopped += OnStopped;
            }
            catch (Exception ex)
            {
                ShowMessageToUser("Exception in PrepareVariablePhotoSequence: " + ex.Message);
            }
            //</SnippetPrepareVPS>
        }
        

        //<SnippetStartVPSCapture>
        private async void StartPhotoCapture()
        {
            _images = new SoftwareBitmap[3];
            _frameControlValues = new CapturedFrameControlValues[3];
            _photoIndex = 0;
            _isRecording = true;

            await _photoSequenceCapture.StartAsync();
        }
        //</SnippetStartVPSCapture>

        //<SnippetOnPhotoCaptured>
        void OnPhotoCaptured(VariablePhotoSequenceCapture s, VariablePhotoCapturedEventArgs args)
        {

            _images[_photoIndex] = args.Frame.SoftwareBitmap;
            _frameControlValues[_photoIndex] = args.CapturedFrameControlValues;
            _photoIndex++;
        }
        //</SnippetOnPhotoCaptured>

        //<SnippetOnStopped>
        void OnStopped(object s, object e)
        {
            _isRecording = false;
            MyPostProcessingFunction(_images, _frameControlValues, 3);
        }
        //</SnippetOnStopped>

        private void MyPostProcessingFunction(SoftwareBitmap[] images, CapturedFrameControlValues[] values, int count)
        {

        }
        
        private void UpdateFrameControllers()
        {
            //<SnippetUpdateFrameControllers>
            var varPhotoSeqController = _mediaCapture.VideoDeviceController.VariablePhotoSequenceController;

            if (varPhotoSeqController.FrameCapabilities.Flash.Supported &&
                varPhotoSeqController.FrameCapabilities.Flash.PowerSupported)
            {
                for (int i = 0; i < varPhotoSeqController.DesiredFrameControllers.Count; i++)
                {
                    varPhotoSeqController.DesiredFrameControllers[i].FlashControl.Mode = FrameFlashMode.Enable;
                    varPhotoSeqController.DesiredFrameControllers[i].FlashControl.PowerPercent = 100;
                }
            }
            //</SnippetUpdateFrameControllers>
        }
        private async void CleanUpVPS()
        {
            //<SnippetCleanUpVPS>
            await _photoSequenceCapture.FinishAsync();
            _photoSequenceCapture.PhotoCaptured -= OnPhotoCaptured;
            _photoSequenceCapture.Stopped -= OnStopped;
            _photoSequenceCapture = null;
            //</SnippetCleanUpVPS>
        }
        #endregion Variable Photo Sequence

        #region Get Preview Frame

        private async Task GetPreviewFrameAsSoftwareBitmapAsync()
        {
            //<SnippetCreateFormatFrame>
            // Get information about the preview
            var previewProperties = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;

            // Create a video frame in the desired format for the preview frame
            VideoFrame videoFrame = new VideoFrame(BitmapPixelFormat.Bgra8, (int)previewProperties.Width, (int)previewProperties.Height);
            //</SnippetCreateFormatFrame>

            //<SnippetGetPreviewFrameAsync>
            VideoFrame previewFrame = await _mediaCapture.GetPreviewFrameAsync(videoFrame);
            //</SnippetGetPreviewFrameAsync>

            //<SnippetGetPreviewBitmap>
            SoftwareBitmap previewBitmap = previewFrame.SoftwareBitmap;
            //</SnippetGetPreviewBitmap>

            //<SnippetGetPreviewSurface>
            var previewSurface = previewFrame.Direct3DSurface;
            //</SnippetGetPreviewSurface>

            //<SnippetCleanUpPreviewFrame>
            previewFrame.Dispose();
            previewFrame = null;
            //</SnippetCleanUpPreviewFrame>
        }

        /// <summary>
        /// Gets the current preview frame as a Direct3DSurface and displays its properties in a TextBlock
        /// </summary>
        /// <returns></returns>

        #endregion Get Preview Frame

        #region Set Resolution
       
        //<SnippetCheckIfStreamsAreIdentical>
        private void CheckIfStreamsAreIdentical()
        {
            if (_mediaCapture.MediaCaptureSettings.VideoDeviceCharacteristic == VideoDeviceCharacteristic.AllStreamsIdentical ||
                _mediaCapture.MediaCaptureSettings.VideoDeviceCharacteristic == VideoDeviceCharacteristic.PreviewRecordStreamsIdentical)
            {
                ShowMessageToUser("Preview and video streams for this device are identical. Changing one will affect the other");
            }
        }
        //</SnippetCheckIfStreamsAreIdentical>


        //<SnippetPopulateStreamPropertiesUI>
        private void PopulateStreamPropertiesUI(MediaStreamType streamType, ComboBox comboBox, bool showFrameRate = true)
        {
            // Query all properties of the specified stream type 
            IEnumerable<StreamPropertiesHelper> allStreamProperties = 
                _mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(streamType).Select(x => new StreamPropertiesHelper(x));

            // Order them by resolution then frame rate
            allStreamProperties = allStreamProperties.OrderByDescending(x => x.Height * x.Width).ThenByDescending(x => x.FrameRate);

            // Populate the combo box with the entries
            foreach (var property in allStreamProperties)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = property.GetFriendlyName(showFrameRate);
                comboBoxItem.Tag = property;
                comboBox.Items.Add(comboBoxItem);
            }
        }
        //</SnippetPopulateStreamPropertiesUI>

        //<SnippetPreviewSettingsChanged>
        private async void PreviewSettings_Changed(object sender, RoutedEventArgs e)
        {
            if (_isPreviewing)
            {
                var selectedItem = (sender as ComboBox).SelectedItem as ComboBoxItem;
                var encodingProperties = (selectedItem.Tag as StreamPropertiesHelper).EncodingProperties;
                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, encodingProperties);
            }
        }
        //</SnippetPreviewSettingsChanged>
        //<SnippetPhotoSettingsChanged>
        private async void PhotoSettings_Changed(object sender, RoutedEventArgs e)
        {
            if (_isPreviewing)
            {
                var selectedItem = (sender as ComboBox).SelectedItem as ComboBoxItem;
                var encodingProperties = (selectedItem.Tag as StreamPropertiesHelper).EncodingProperties;
                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, encodingProperties);
            }
        }
        //</SnippetPhotoSettingsChanged>
        //<SnippetVideoSettingsChanged>
        private async void VideoSettings_Changed(object sender, RoutedEventArgs e)
        {
            if (_isPreviewing)
            {
                var selectedItem = (sender as ComboBox).SelectedItem as ComboBoxItem;
                var encodingProperties = (selectedItem.Tag as StreamPropertiesHelper).EncodingProperties;
                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoRecord, encodingProperties);
            }
        }
        //</SnippetVideoSettingsChanged>
        //<SnippetMatchPreviewAspectRatio>
        private void MatchPreviewAspectRatio(MediaStreamType streamType, ComboBox comboBox)
        {
            // Query all properties of the specified stream type
            IEnumerable<StreamPropertiesHelper> allVideoProperties = 
                _mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(streamType).Select(x => new StreamPropertiesHelper(x));

            // Query the current preview settings
            StreamPropertiesHelper previewProperties = new StreamPropertiesHelper(_mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview));

            // Get all formats that have the same-ish aspect ratio as the preview
            // Allow for some tolerance in the aspect ratio comparison
            const double ASPECT_RATIO_TOLERANCE = 0.015;
            var matchingFormats = allVideoProperties.Where(x => Math.Abs(x.AspectRatio - previewProperties.AspectRatio) < ASPECT_RATIO_TOLERANCE);

            // Order them by resolution then frame rate
            allVideoProperties = matchingFormats.OrderByDescending(x => x.Height * x.Width).ThenByDescending(x => x.FrameRate);

            // Clear out old entries and populate the video combo box with new matching entries
            comboBox.Items.Clear();
            foreach (var property in allVideoProperties)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = property.GetFriendlyName();
                comboBoxItem.Tag = property;
                comboBox.Items.Add(comboBoxItem);
            }
            comboBox.SelectedIndex = -1;
        }
        //</SnippetMatchPreviewAspectRatio>

        #endregion Set Resolution
        private void ShowMessageToUser(string message)
        {


        }

    }
}