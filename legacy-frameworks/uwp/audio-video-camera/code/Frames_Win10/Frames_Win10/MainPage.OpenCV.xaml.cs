using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Windows.Media.MediaProperties;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Frames_Win10
{
    public sealed partial class MainPage : Page
    {

        OpenCVBridge.OpenCVHelper openCVHelper;

        private void OpenCV_Click(object sender, RoutedEventArgs e)
        {
            openCVHelper = new OpenCVBridge.OpenCVHelper();

            InitOpenCVFrameReader();
        }

        private async void InitOpenCVFrameReader()
        {

            // <SnippetOpenCVFrameSourceGroups>
            var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();
            var selectedGroupObjects = frameSourceGroups.Select(group =>
               new
               {
                   sourceGroup = group,
                   colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
                   {
                       // On Xbox/Kinect, omit the MediaStreamType and EnclosureLocation tests
                       return sourceInfo.SourceKind == MediaFrameSourceKind.Color;

                   })

               }).Where(t => t.colorSourceInfo != null)
               .FirstOrDefault();

            MediaFrameSourceGroup selectedGroup = selectedGroupObjects?.sourceGroup;
            MediaFrameSourceInfo colorSourceInfo = selectedGroupObjects?.colorSourceInfo;

            if (selectedGroup == null)
            {
                return;
            }
            // </SnippetOpenCVFrameSourceGroups>

            // <SnippetOpenCVInitMediaCapture>
            mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings()
            {
                SourceGroup = selectedGroup,
                SharingMode = MediaCaptureSharingMode.ExclusiveControl,
                MemoryPreference = MediaCaptureMemoryPreference.Cpu,
                StreamingCaptureMode = StreamingCaptureMode.Video
            };
            try
            {
                await mediaCapture.InitializeAsync(settings);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed: " + ex.Message);
                return;
            }

            var colorFrameSource = mediaCapture.FrameSources[colorSourceInfo.Id];
            // </SnippetOpenCVInitMediaCapture>

            // <SnippetOpenCVFrameReader>
            BitmapSize size = new BitmapSize() // Choose a lower resolution to make the image processing more performant
            {
                Height = 480,
                Width = 640
            };

            mediaFrameReader = await mediaCapture.CreateFrameReaderAsync(colorFrameSource, MediaEncodingSubtypes.Argb32, size);
            mediaFrameReader.FrameArrived += ColorFrameReader_FrameArrived_OpenCV;

            imageElement.Source = new SoftwareBitmapSource();
            _frameRenderer = new FrameRenderer(imageElement);

            await mediaFrameReader.StartAsync();
            // </SnippetOpenCVFrameReader>
        }
        // <SnippetOpenCVFrameArrived>
        private void ColorFrameReader_FrameArrived_OpenCV(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {

            var mediaFrameReference = sender.TryAcquireLatestFrame();
            if (mediaFrameReference != null)
            {

                SoftwareBitmap openCVInputBitmap = null;
                var inputBitmap = mediaFrameReference.VideoMediaFrame?.SoftwareBitmap;
                if (inputBitmap != null)
                {
                    //The XAML Image control can only display images in BRGA8 format with premultiplied or no alpha
                    if (inputBitmap.BitmapPixelFormat == BitmapPixelFormat.Bgra8
                        && inputBitmap.BitmapAlphaMode == BitmapAlphaMode.Premultiplied)
                    {
                        openCVInputBitmap = SoftwareBitmap.Copy(inputBitmap);
                    }
                    else
                    {
                        openCVInputBitmap = SoftwareBitmap.Convert(inputBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    }

                    SoftwareBitmap openCVOutputBitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8, openCVInputBitmap.PixelWidth, openCVInputBitmap.PixelHeight, BitmapAlphaMode.Premultiplied);

                    // operate on the image and render it
                    openCVHelper.Blur(openCVInputBitmap, openCVOutputBitmap);
                    _frameRenderer.PresentSoftwareBitmap(openCVOutputBitmap);

                }
            }
        }
        // </SnippetOpenCVFrameArrived>
    }
}
