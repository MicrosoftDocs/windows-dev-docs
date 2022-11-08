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
using System.Diagnostics;

// <SnippetFaceDetectionUsing>
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.Media.FaceAnalysis;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
// </SnippetFaceDetectionUsing>

// <SnippetFaceTrackingUsing>
using Windows.Media;
using System.Threading;
using Windows.System.Threading;
// </SnippetFaceTrackingUsing>

using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FaceDetection_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // <SnippetClassVariables1>
        FaceDetector faceDetector;
        IList<DetectedFace> detectedFaces;
        // </SnippetClassVariables1>

        // <SnippetClassVariables2>
        private readonly SolidColorBrush lineBrush = new SolidColorBrush(Windows.UI.Colors.Yellow);
        private readonly double lineThickness = 2.0;
        private readonly SolidColorBrush fillBrush = new SolidColorBrush(Windows.UI.Colors.Transparent);
        // </SnippetClassVariables2>

        bool isPreviewStreaming = false;
        MediaCapture mediaCapture;
        VideoEncodingProperties videoProperties;

        public MainPage()
        {
            this.InitializeComponent();

            App.Current.Suspending += this.OnSuspending;
        }

        public async void DetectFaces()
        {
            // <SnippetPicker>
            FileOpenPicker photoPicker = new FileOpenPicker();
            photoPicker.ViewMode = PickerViewMode.Thumbnail;
            photoPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            photoPicker.FileTypeFilter.Add(".jpg");
            photoPicker.FileTypeFilter.Add(".jpeg");
            photoPicker.FileTypeFilter.Add(".png");
            photoPicker.FileTypeFilter.Add(".bmp");

            StorageFile photoFile = await photoPicker.PickSingleFileAsync();
            if (photoFile == null)
            {
                return;
            }
            // </SnippetPicker>

            // <SnippetDecode>
            IRandomAccessStream fileStream = await photoFile.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

            BitmapTransform transform = new BitmapTransform();
            const float sourceImageHeightLimit = 1280;

            if (decoder.PixelHeight > sourceImageHeightLimit)
            {
                float scalingFactor = (float)sourceImageHeightLimit / (float)decoder.PixelHeight;
                transform.ScaledWidth = (uint)Math.Floor(decoder.PixelWidth * scalingFactor);
                transform.ScaledHeight = (uint)Math.Floor(decoder.PixelHeight * scalingFactor);
            }

            SoftwareBitmap sourceBitmap = await decoder.GetSoftwareBitmapAsync(decoder.BitmapPixelFormat, BitmapAlphaMode.Premultiplied, transform, ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
            // </SnippetDecode>


            // <SnippetFormat>
            // Use FaceDetector.GetSupportedBitmapPixelFormats and IsBitmapPixelFormatSupported to dynamically
            // determine supported formats
            const BitmapPixelFormat faceDetectionPixelFormat = BitmapPixelFormat.Gray8;

            SoftwareBitmap convertedBitmap;

            if (sourceBitmap.BitmapPixelFormat != faceDetectionPixelFormat)
            {
                convertedBitmap = SoftwareBitmap.Convert(sourceBitmap, faceDetectionPixelFormat);
            }
            else
            {
                convertedBitmap = sourceBitmap;
            }
            // </SnippetFormat>

            // <SnippetDetect>
            if (faceDetector == null)
            {
                faceDetector = await FaceDetector.CreateAsync();
            }

            detectedFaces = await faceDetector.DetectFacesAsync(convertedBitmap);
            ShowDetectedFaces(sourceBitmap, detectedFaces);
            // </SnippetDetect>

            // <SnippetDispose>
            sourceBitmap.Dispose();
            fileStream.Dispose();
            convertedBitmap.Dispose();
            // </SnippetDispose>
        }
        // <SnippetShowDetectedFaces>
        private async void ShowDetectedFaces(SoftwareBitmap sourceBitmap, IList<DetectedFace> faces)
        {
            ImageBrush brush = new ImageBrush();
            SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(sourceBitmap);
            brush.ImageSource = bitmapSource;
            brush.Stretch = Stretch.Fill;
            this.VisualizationCanvas.Background = brush;

            if (detectedFaces != null)
            {
                double widthScale = sourceBitmap.PixelWidth / this.VisualizationCanvas.ActualWidth;
                double heightScale = sourceBitmap.PixelHeight / this.VisualizationCanvas.ActualHeight;

                foreach (DetectedFace face in detectedFaces)
                {
                    // Create a rectangle element for displaying the face box but since we're using a Canvas
                    // we must scale the rectangles according to the image’s actual size.
                    // The original FaceBox values are saved in the Rectangle's Tag field so we can update the
                    // boxes when the Canvas is resized.
                    Rectangle box = new Rectangle();
                    box.Tag = face.FaceBox;
                    box.Width = (uint)(face.FaceBox.Width / widthScale);
                    box.Height = (uint)(face.FaceBox.Height / heightScale);
                    box.Fill = this.fillBrush;
                    box.Stroke = this.lineBrush;
                    box.StrokeThickness = this.lineThickness;
                    box.Margin = new Thickness((uint)(face.FaceBox.X / widthScale), (uint)(face.FaceBox.Y / heightScale), 0, 0);

                    this.VisualizationCanvas.Children.Add(box);
                }
            }
        }
        // </SnippetShowDetectedFaces>


        // <SnippetClassVariables3>
        private FaceTracker faceTracker;
        private ThreadPoolTimer frameProcessingTimer;
        private SemaphoreSlim frameProcessingSemaphore = new SemaphoreSlim(1);
        // </SnippetClassVariables3>


        public async void TrackFaces()
        {
            // <SnippetTrackingInit>
            this.faceTracker = await FaceTracker.CreateAsync();
            TimeSpan timerInterval = TimeSpan.FromMilliseconds(66); // 15 fps
            this.frameProcessingTimer = Windows.System.Threading.ThreadPoolTimer.CreatePeriodicTimer(new Windows.System.Threading.TimerElapsedHandler(ProcessCurrentVideoFrame), timerInterval);
            // </SnippetTrackingInit>
        }

        // <SnippetProcessCurrentVideoFrame>
        public async void ProcessCurrentVideoFrame(ThreadPoolTimer timer)
        {
            if (!frameProcessingSemaphore.Wait(0))
            {
                return;
            }

            VideoFrame currentFrame = await GetLatestFrame();

            // Use FaceDetector.GetSupportedBitmapPixelFormats and IsBitmapPixelFormatSupported to dynamically
            // determine supported formats
            const BitmapPixelFormat faceDetectionPixelFormat = BitmapPixelFormat.Nv12;

            if (currentFrame.SoftwareBitmap.BitmapPixelFormat != faceDetectionPixelFormat)
            {
                return;
            }

            try
            {
                IList<DetectedFace> detectedFaces = await faceTracker.ProcessNextFrameAsync(currentFrame);

                var previewFrameSize = new Windows.Foundation.Size(currentFrame.SoftwareBitmap.PixelWidth, currentFrame.SoftwareBitmap.PixelHeight);
                var ignored = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    this.SetupVisualization(previewFrameSize, detectedFaces);
                });
            }
            catch (Exception e)
            {
                // Face tracking failed
            }
            finally
            {
                frameProcessingSemaphore.Release();
            }

            currentFrame.Dispose();
        }
        // </SnippetProcessCurrentVideoFrame>

        private void SetupVisualization(Windows.Foundation.Size framePixelSize, IList<DetectedFace> faces)
        {
            this.VisualizationCanvas.Children.Clear();

            if (faces != null)
            {
                double widthScale = framePixelSize.Width / this.VisualizationCanvas.ActualWidth;
                double heightScale = framePixelSize.Height / this.VisualizationCanvas.ActualHeight;

                foreach (DetectedFace face in faces)
                {
                    // Create a rectangle element for displaying the face box but since we're using a Canvas
                    // we must scale the rectangles according to the frames's actual size.
                    Rectangle box = new Rectangle();
                    box.Width = (uint)(face.FaceBox.Width / widthScale);
                    box.Height = (uint)(face.FaceBox.Height / heightScale);
                    box.Fill = this.fillBrush;
                    box.Stroke = this.lineBrush;
                    box.StrokeThickness = this.lineThickness;
                    box.Margin = new Thickness((uint)(face.FaceBox.X / widthScale), (uint)(face.FaceBox.Y / heightScale), 0, 0);

                    this.VisualizationCanvas.Children.Add(box);
                }
            }
        }
        public async Task<VideoFrame> GetLatestFrame()
        {
            if(isPreviewStreaming)
            {
                const BitmapPixelFormat InputPixelFormat = BitmapPixelFormat.Nv12;
                VideoFrame previewFrame = new VideoFrame(InputPixelFormat, (int)this.videoProperties.Width, (int)this.videoProperties.Height);
                await this.mediaCapture.GetPreviewFrameAsync(previewFrame);
                return previewFrame;
            }else
            {
                return null; // This will explode
            }
        }


        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            DetectFaces();

            //await InitializeMediaCapture();


            //TrackFaces();
        }
        private async Task InitializeMediaCapture()
        {
            try
            {
                this.mediaCapture = new MediaCapture();

                // For this scenario, we only need Video (not microphone) so specify this in the initializer.
                // NOTE: the appxmanifest only declares "webcam" under capabilities and if this is changed to include
                // microphone (default constructor) you must add "microphone" to the manifest or initialization will fail.
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
                settings.StreamingCaptureMode = StreamingCaptureMode.Video;
                await this.mediaCapture.InitializeAsync(settings);
                //this.mediaCapture.CameraStreamStateChanged += this.MediaCapture_CameraStreamStateChanged;

                // Cache the media properties as we'll need them later.
                var deviceController = this.mediaCapture.VideoDeviceController;
                this.videoProperties = deviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;

                // Immediately start streaming to our CaptureElement UI.
                // NOTE: CaptureElement's Source must be set before streaming is started.
                this.captureElement.Source = this.mediaCapture;
                await this.mediaCapture.StartPreviewAsync();
                this.isPreviewStreaming = true;

                // Use a 66 milisecond interval for our timer, i.e. 15 frames per second 
                TimeSpan timerInterval = TimeSpan.FromMilliseconds(66);
                this.frameProcessingTimer = Windows.System.Threading.ThreadPoolTimer.CreatePeriodicTimer(new Windows.System.Threading.TimerElapsedHandler(ProcessCurrentVideoFrame), timerInterval);
            }
            catch (System.UnauthorizedAccessException)
            {

            }
            catch (Exception ex)
            {

            }
        }

        private async void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            if (this.isPreviewStreaming)
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                try
                {
                    if (this.frameProcessingTimer != null)
                    {
                        this.frameProcessingTimer.Cancel();
                    }

                    if (this.mediaCapture != null)
                    {
                        if (this.mediaCapture.CameraStreamState == Windows.Media.Devices.CameraStreamState.Streaming)
                        {
                            await this.mediaCapture.StopPreviewAsync();
                        }

                        this.mediaCapture.Dispose();
                    }

                    this.frameProcessingTimer = null;
                    var ignored = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { this.captureElement.Source = null; } );
                    
                    this.mediaCapture = null;
                }
                finally
                {
                    deferral.Complete();
                }
            }
        }
    }
}
