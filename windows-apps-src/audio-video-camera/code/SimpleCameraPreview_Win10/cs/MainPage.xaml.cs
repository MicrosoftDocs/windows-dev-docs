using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//<SnippetSimpleCameraPreviewUsing>
using Windows.Media.Capture;
using Windows.ApplicationModel;
using System.Threading.Tasks;
using Windows.System.Display;
using Windows.Graphics.Display;
//</SnippetSimpleCameraPreviewUsing>

//<SnippetSimpleCaptureUsing>
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
//</SnippetSimpleCaptureUsing>


//<SnippetOrientationUsing>
using Windows.Devices.Enumeration;
using Windows.UI.Core;

//</SnippetOrientationUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleCameraPreview_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        #region Simple preview access
        //<SnippetDeclareMediaCapture>
        MediaCapture _mediaCapture;
        bool _isPreviewing;
        //</SnippetDeclareMediaCapture>

        //<SnippetDeclareDisplayRequest>
        DisplayRequest _displayRequest;
        //</SnippetDeclareDisplayRequest>

        //<SnippetRegisterSuspending>
        public MainPage()
        {
            this.InitializeComponent();

            Application.Current.Suspending += Application_Suspending;
        }
        //</SnippetRegisterSuspending>


        //<SnippetStartPreviewAsync>
        private async Task StartPreviewAsync()
        {
            try
            {
 
                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync();

                PreviewControl.Source = _mediaCapture;
                await _mediaCapture.StartPreviewAsync();
                _isPreviewing = true;

                _displayRequest.RequestActive();
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            }
            catch (UnauthorizedAccessException)
            {
                // This will be thrown if the user denied access to the camera in privacy settings
                System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed. {0}", ex.Message);
            }
        }
        //</SnippetStartPreviewAsync>

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
             
        }
        //<SnippetCleanupCameraAsync>
        private async Task CleanupCameraAsync()
        {
            if (_mediaCapture != null)
            {
                if (_isPreviewing)
                {
                    await _mediaCapture.StopPreviewAsync();
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PreviewControl.Source = null;
                    if (_displayRequest != null)
                    {
                        _displayRequest.RequestRelease();
                    }

                    _mediaCapture.Dispose();
                    _mediaCapture = null;
                });
            }
            
        }
        //</SnippetCleanupCameraAsync>

        //<SnippetOnNavigatedFrom>
        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            await CleanupCameraAsync();
        }
        //</SnippetOnNavigatedFrom>

        //<SnippetSuspendingHandler>
        private async void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                await CleanupCameraAsync();
                deferral.Complete();
            }
        }
        //</SnippetSuspendingHandler>

        #endregion

        #region Simple capture
        public async void InitializeMediaCapture()
        {
            //<SnippetInitMediaCapture>
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();
            _mediaCapture.Failed += MediaCapture_Failed;
            //</SnippetInitMediaCapture>
        }

        

        public async Task CaptureToSoftwareBitmap()
        {
            //<SnippetCaptureToSoftwareBitmap>
            // Prepare and capture photo
            var lowLagCapture = await _mediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Bgra8));

            var capturedPhoto = await lowLagCapture.CaptureAsync();
            var softwareBitmap = capturedPhoto.Frame.SoftwareBitmap;

            await lowLagCapture.FinishAsync();            
            //</SnippetCaptureToSoftwareBitmap>
        }

        public async Task CaptureToFile()
        {
            //<SnippetCaptureToFile>
            var myPictures = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
            StorageFile file = await myPictures.SaveFolder.CreateFileAsync("photo.jpg", CreationCollisionOption.GenerateUniqueName);

            using (var captureStream = new InMemoryRandomAccessStream())
            {
                await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), captureStream);

                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var decoder = await BitmapDecoder.CreateAsync(captureStream);
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(fileStream, decoder);

                    var properties = new BitmapPropertySet {
                        { "System.Photo.Orientation", new BitmapTypedValue(PhotoOrientation.Normal, PropertyType.UInt16) }
                    };
                    await encoder.BitmapProperties.SetPropertiesAsync(properties);

                    await encoder.FlushAsync();
                }
            }
            //</SnippetCaptureToFile>
        }


        //<SnippetLowLagMediaRecording>
        LowLagMediaRecording _mediaRecording;
        //</SnippetLowLagMediaRecording>

        public async Task StartVideoCapture()
        {

            //<SnippetRecordLimitationExceeded>
            _mediaCapture.RecordLimitationExceeded += MediaCapture_RecordLimitationExceeded;
            //</SnippetRecordLimitationExceeded>

            //<SnippetStartVideoCapture>
            var myVideos = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Videos);
            StorageFile file = await myVideos.SaveFolder.CreateFileAsync("video.mp4", CreationCollisionOption.GenerateUniqueName);
            _mediaRecording = await _mediaCapture.PrepareLowLagRecordToStorageFileAsync(
                    MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto), file);
            await _mediaRecording.StartAsync();
            //</SnippetStartVideoCapture>
        }



        public async Task StopVideoCapture()
        {
            //<SnippetStopRecording>
            await _mediaRecording.StopAsync();
            //</SnippetStopRecording>

            //<SnippetFinishAsync>
            await _mediaRecording.FinishAsync();
            //</SnippetFinishAsync>
        }


        public async Task PauseVideoCaptureSimple()
        {
            //<SnippetPauseRecordingSimple>
            await _mediaRecording.PauseAsync(Windows.Media.Devices.MediaCapturePauseBehavior.ReleaseHardwareResources);
            //</SnippetPauseRecordingSimple>
        }
        public async Task ResumeVideoCaptureSimple()
        {
            //<SnippetResumeRecordingSimple>
            await _mediaRecording.ResumeAsync();
            //</SnippetResumeRecordingSimple>
        }

        TimeSpan _totalRecordedTime = TimeSpan.Zero;
        public async Task PauseVideoCaptureWithResult()
        {
            //<SnippetPauseCaptureWithResult>
            MediaCapturePauseResult result = 
                await _mediaRecording.PauseWithResultAsync(Windows.Media.Devices.MediaCapturePauseBehavior.RetainHardwareResources);
            
            var pausedFrame = result.LastFrame.SoftwareBitmap;
            if(pausedFrame.BitmapPixelFormat != BitmapPixelFormat.Bgra8 || pausedFrame.BitmapAlphaMode != BitmapAlphaMode.Ignore)
            {
                pausedFrame = SoftwareBitmap.Convert(pausedFrame, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore);
            }

            var source = new SoftwareBitmapSource();
            await source.SetBitmapAsync(pausedFrame);

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PauseImage.Source = source;
                PauseImage.Visibility = Visibility.Visible;
            });

            _totalRecordedTime += result.RecordDuration;
            //</SnippetPauseCaptureWithResult>

        }
        public async Task ResumeVideoCaptureWithResult()
        {
            //<SnippetResumeCaptureWithResult>
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PauseImage.Source = null;
                PauseImage.Visibility = Visibility.Collapsed;
            });

            await _mediaRecording.ResumeAsync();
            //</SnippetResumeCaptureWithResult>
        }

        public async Task StartAudioCapture()
        {

            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();

            //<SnippetStartAudioCapture>
            _mediaCapture.RecordLimitationExceeded += MediaCapture_RecordLimitationExceeded;

            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync("audio.mp3", CreationCollisionOption.GenerateUniqueName);
            _mediaRecording = await _mediaCapture.PrepareLowLagRecordToStorageFileAsync(
                    MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High), file);
            await _mediaRecording.StartAsync();
            //</SnippetStartAudioCapture>
        }
        //<SnippetRecordLimitationExceededHandler>
        private async void MediaCapture_RecordLimitationExceeded(MediaCapture sender)
        {
            await _mediaRecording.StopAsync();
            System.Diagnostics.Debug.WriteLine("Record limitation exceeded.");
        }
        //</SnippetRecordLimitationExceededHandler>

        private void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            System.Diagnostics.Debug.WriteLine("MediaCapture.Failed: {0}", errorEventArgs.Message);
        }

        #endregion

        #region Test button click

        private async void StartPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            await StartPreviewAsync();
        }
        private async void CaptureToSoftwareBitmapButton_Click(object sender, RoutedEventArgs e)
        {
            await CaptureToSoftwareBitmap();
        }
        private async void CaptureToFileButton_Click(object sender, RoutedEventArgs e)
        {
            await CaptureToFile();
        }
        private async void StartVideoCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            await StartVideoCapture();
        }
        private async void StopVideoCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            await StopVideoCapture();
        }
        private async void PauseVideoCaptureSimpleButton_Click(object sender, RoutedEventArgs e)
        {
            await PauseVideoCaptureSimple();
        }
        private async void ResumeVideoCaptureSimpleButton_Click(object sender, RoutedEventArgs e)
        {
            await ResumeVideoCaptureSimple();
        }
        private async void PauseVideoCaptureWithResultButton_Click(object sender, RoutedEventArgs e)
        {
            await PauseVideoCaptureWithResult();
        }
        private async void ResumeVideoCaptureWithResultButton_Click(object sender, RoutedEventArgs e)
        {
            await ResumeVideoCaptureWithResult();
        }

        private async void CaptureAudioButton_Click(object sender, RoutedEventArgs e)
        {
            await StartAudioCapture();
        }
        private async void InitializeMediaCaptureWithOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            SetAutoRotationPreference();
            await InitializeMediaCaptureWithOrientation();
            InitRotationHelper();
            await StartPreviewWithRotationAsync();
        }
        private async void CapturePhotoWithOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            await CapturePhotoWithOrientationAsync();
        }
        private async void StartRecordingWithOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            await StartRecordingWithOrientationAsync();
        }

        private async void StopRecordingWithOrientation_Click(object sender, RoutedEventArgs e)
        {
            await StopRecordingWithOrientationAsync();
        }

        bool _isPaused = false;
        private async void Button3_Click(object sender, RoutedEventArgs e)
        {
            // Simple capture
            
            //if (!_isPaused)
            //{
            //    //await PauseVideoCaptureWithResult();
            //    await PauseVideoCaptureSimple();
            //    _isPaused = true;
            //}
            //else
            //{
            //    //await ResumeVideoCaptureWithResult();
            //    await ResumeVideoCaptureSimple();
            //    _isPaused = false;
            //}



            // Orientation
            //await StopRecordingWithOrientationAsync();

        }

        #endregion

        #region Handle device orientation
        public void SetAutoRotationPreference()
        {
            //<SnippetAutoRotationPreference>
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            //</SnippetAutoRotationPreference>
        }

        //<SnippetCameraDeviceLocationBools>
        private bool _externalCamera;
        private bool _mirroringPreview;
        //</SnippetCameraDeviceLocationBools>

        //<SnippetFindCameraDeviceByPanelAsync>
        //private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        //{
        //    var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
        //    DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);
        //    return desiredDevice ?? allVideoDevices.FirstOrDefault();
        //}
        //</SnippetFindCameraDeviceByPanelAsync>


        //<SnippetDeclareCameraDevice>
        DeviceInformation _cameraDevice;
        //</SnippetDeclareCameraDevice>


        
        private async Task InitializeMediaCaptureWithOrientation()
        {
            //<SnippetInitMediaCaptureWithOrientation>
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null 
                && x.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
            _cameraDevice = desiredDevice ?? allVideoDevices.FirstOrDefault();


            if (_cameraDevice == null)
            {
                System.Diagnostics.Debug.WriteLine("No camera device found!");
                return;
            }

            var settings = new MediaCaptureInitializationSettings { VideoDeviceId = _cameraDevice.Id };

            _mediaCapture = new MediaCapture();
            _mediaCapture.RecordLimitationExceeded += MediaCapture_RecordLimitationExceeded;
            _mediaCapture.Failed += MediaCapture_Failed;

            try
            {
                await _mediaCapture.InitializeAsync(settings);
            }
            catch (UnauthorizedAccessException)
            {
                System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
                return;
            }

            // Handle camera device location
            if (_cameraDevice.EnclosureLocation == null || 
                _cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Unknown)
            {
                _externalCamera = true;
            }
            else
            {
                _externalCamera = false;
                _mirroringPreview = (_cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
            }

            //</SnippetInitMediaCaptureWithOrientation>

        }

        //<SnippetDeclareRotationHelper>
        private CameraRotationHelper _rotationHelper;
        //</SnippetDeclareRotationHelper>
        
        private void InitRotationHelper()
        {
            //<SnippetInitRotationHelper>
            _rotationHelper = new CameraRotationHelper(_cameraDevice.EnclosureLocation);
            _rotationHelper.OrientationChanged += RotationHelper_OrientationChanged;
            //</SnippetInitRotationHelper>
        }

        

        
        private async Task StartPreviewWithRotationAsync()
        {
            //<SnippetStartPreviewWithRotationAsync>
            PreviewControl.Source = _mediaCapture;
            PreviewControl.FlowDirection = _mirroringPreview ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            await _mediaCapture.StartPreviewAsync();
            await SetPreviewRotationAsync();
            //</SnippetStartPreviewWithRotationAsync>
        }

        //<SnippetSetPreviewRotationAsync>
        private async Task SetPreviewRotationAsync()
        {
            if (!_externalCamera)
            {
                // Add rotation metadata to the preview stream to make sure the aspect ratio / dimensions match when rendering and getting preview frames
                var rotation = _rotationHelper.GetCameraPreviewOrientation();
                var props = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview);
                Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");
                props.Properties.Add(RotationKey, CameraRotationHelper.ConvertSimpleOrientationToClockwiseDegrees(rotation));
                await _mediaCapture.SetEncodingPropertiesAsync(MediaStreamType.VideoPreview, props, null);
            }
        }
        //</SnippetSetPreviewRotationAsync>

        //<SnippetHelperOrientationChanged>
        private async void RotationHelper_OrientationChanged(object sender, bool updatePreview)
        {
            if (updatePreview)
            {
                await SetPreviewRotationAsync();
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                // Rotate the buttons in the UI to match the rotation of the device
                var angle = CameraRotationHelper.ConvertSimpleOrientationToClockwiseDegrees(_rotationHelper.GetUIOrientation());
                var transform = new RotateTransform { Angle = angle };

                // The RenderTransform is safe to use (i.e. it won't cause layout issues) in this case, because these buttons have a 1:1 aspect ratio
                CapturePhotoButton.RenderTransform = transform;
                CapturePhotoButton.RenderTransform = transform;
            });
        }
        //</SnippetHelperOrientationChanged>

        //<SnippetCapturePhotoWithOrientation>
        private async Task CapturePhotoWithOrientationAsync()
        {
            var captureStream = new InMemoryRandomAccessStream();

            try
            {
                await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), captureStream);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception when taking a photo: {0}", ex.ToString());
                return;
            }


            var decoder = await BitmapDecoder.CreateAsync(captureStream);
            var file = await KnownFolders.PicturesLibrary.CreateFileAsync("SimplePhoto.jpeg", CreationCollisionOption.GenerateUniqueName);

            using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);
                var photoOrientation = CameraRotationHelper.ConvertSimpleOrientationToPhotoOrientation(
                    _rotationHelper.GetCameraCaptureOrientation());
                var properties = new BitmapPropertySet {
                    { "System.Photo.Orientation", new BitmapTypedValue(photoOrientation, PropertyType.UInt16) } };
                await encoder.BitmapProperties.SetPropertiesAsync(properties);
                await encoder.FlushAsync();
            }
        }
        //</SnippetCapturePhotoWithOrientation>

        //<SnippetStartRecordingWithOrientationAsync>
        private async Task StartRecordingWithOrientationAsync()
        {
            try
            {
                var videoFile = await KnownFolders.VideosLibrary.CreateFileAsync("SimpleVideo.mp4", CreationCollisionOption.GenerateUniqueName);

                var encodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto);

                var rotationAngle = CameraRotationHelper.ConvertSimpleOrientationToClockwiseDegrees(
                    _rotationHelper.GetCameraCaptureOrientation());
                Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");
                encodingProfile.Video.Properties.Add(RotationKey, PropertyValue.CreateInt32(rotationAngle));

                await _mediaCapture.StartRecordToStorageFileAsync(encodingProfile, videoFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception when starting video recording: {0}", ex.ToString());
            }
        }
        //</SnippetStartRecordingWithOrientationAsync>

        private async Task StopRecordingWithOrientationAsync()
        {
            try
            {
                await _mediaCapture.StopRecordAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception when stopping video recording: {0}", ex.ToString());
            }
        }
        #endregion

        
    }



}

