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

//<SnippetFramesUsing>
using Windows.Media.Capture.Frames;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.MediaProperties;
using Windows.Graphics.Imaging;
using System.Threading;
using Windows.UI.Core;
using System.Threading.Tasks;
//</SnippetFramesUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Frames_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //<SnippetDeclareMediaCapture>
        MediaCapture _mediaCapture;
        //</SnippetDeclareMediaCapture>

        //<SnippetDeclareMediaFrameReader>
        MediaFrameReader _mediaFrameReader;
        //</SnippetDeclareMediaFrameReader>

        //<SnippetDeclareBackBuffer>
        private SoftwareBitmap _backBuffer;
        private bool _taskRunning = false;
        //</SnippetDeclareBackBuffer>

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            //<SnippetImageElementSource>
            _imageElement.Source = new SoftwareBitmapSource();
            //</SnippetImageElementSource>

            //<SnippetFindAllAsync>
            var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();
            //</SnippetFindAllAsync>

            // Color, infrared, and depth


            //<SnippetSelectColor>
            var selectedGroupObjects = frameSourceGroups.Select(group =>
               new
               {
                   sourceGroup = group,
                   colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
                   {
                       return sourceInfo.MediaStreamType == MediaStreamType.VideoPreview
                       && sourceInfo.SourceKind == MediaFrameSourceKind.Color
                       && sourceInfo.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front;
                   })

               }).Where(t => t.colorSourceInfo != null)
               .FirstOrDefault();

            MediaFrameSourceGroup selectedGroup = selectedGroupObjects?.sourceGroup;
            MediaFrameSourceInfo colorSourceInfo = selectedGroupObjects?.colorSourceInfo;

            if (selectedGroup == null)
            {
                return;
            }
            //</SnippetSelectColor>

            //<SnippetInitMediaCapture>
            _mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings()
            {
                SourceGroup = selectedGroup,
                SharingMode = MediaCaptureSharingMode.ExclusiveControl,
                MemoryPreference = MediaCaptureMemoryPreference.Cpu,
                StreamingCaptureMode = StreamingCaptureMode.Video
            };
            try
            {
                await _mediaCapture.InitializeAsync(settings);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed: " + ex.Message);
                return;
            }
            //</SnippetInitMediaCapture>


            var colorFrameSource = _mediaCapture.FrameSources[colorSourceInfo.Id];
            var preferredFormat = colorFrameSource.SupportedFormats.Where(format =>
            {
                return format.VideoFormat.Width == 1080;
            }).FirstOrDefault();

            if (preferredFormat == null)
            {
                // Our desired format is not supported
                return;
            }
            await colorFrameSource.SetFormatAsync(preferredFormat);

            //<SnippetCreateFrameReader>
            _mediaFrameReader = await _mediaCapture.CreateFrameReaderAsync(colorFrameSource, MediaEncodingSubtypes.Argb32);
            _mediaFrameReader.FrameArrived += ColorFrameReader_FrameArrived;
            await _mediaFrameReader.StartAsync();
            //</SnippetCreateFrameReader>
        }
        //<SnippetFrameArrived>
        private void ColorFrameReader_FrameArrived(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {
            var mediaFrameReference = sender.TryAcquireLatestFrame();
            var videoMediaFrame = mediaFrameReference?.VideoMediaFrame;
            var softwareBitmap = videoMediaFrame?.SoftwareBitmap;

            if (softwareBitmap != null)
            {
                if (softwareBitmap.BitmapPixelFormat != Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8 ||
                    softwareBitmap.BitmapAlphaMode != Windows.Graphics.Imaging.BitmapAlphaMode.Premultiplied)
                {
                    softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                }

                // Swap the processed frame to _backBuffer and dispose of the unused image.
                softwareBitmap = Interlocked.Exchange(ref _backBuffer, softwareBitmap);
                softwareBitmap?.Dispose();

                // Changes to XAML ImageElement must happen on UI thread through Dispatcher
                var task = _imageElement.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        // Don't let two copies of this task run at the same time.
                        if (_taskRunning)
                        {
                            return;
                        }
                        _taskRunning = true;

                        // Keep draining frames from the backbuffer until the backbuffer is empty.
                        SoftwareBitmap latestBitmap;
                        while ((latestBitmap = Interlocked.Exchange(ref _backBuffer, null)) != null)
                        {
                            var imageSource = (SoftwareBitmapSource)_imageElement.Source;
                            await imageSource.SetBitmapAsync(latestBitmap);
                            latestBitmap.Dispose();
                        }

                        _taskRunning = false;
                    });
            }

            mediaFrameReference.Dispose();
        }
        //</SnippetFrameArrived>

        private async Task Cleanup()
        {
            //<SnippetCleanup>
            await _mediaFrameReader.StopAsync();
            _mediaFrameReader.FrameArrived -= ColorFrameReader_FrameArrived;
            _mediaCapture.Dispose();
            _mediaCapture = null;
            //</SnippetCleanup>
        }
        private async Task SimpleSelect()
        {
            //<SnippetSimpleSelect>
            var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();

            MediaFrameSourceGroup selectedGroup = null;
            MediaFrameSourceInfo colorSourceInfo = null;

            foreach (var sourceGroup in frameSourceGroups)
            {
                foreach (var sourceInfo in sourceGroup.SourceInfos)
                {
                    if (sourceInfo.MediaStreamType == MediaStreamType.VideoPreview
                        && sourceInfo.SourceKind == MediaFrameSourceKind.Color)
                    {
                        colorSourceInfo = sourceInfo;
                        break;
                    }
                }
                if (colorSourceInfo != null)
                {
                    selectedGroup = sourceGroup;
                    break;
                }
            }
            //</SnippetSimpleSelect>
        }
        private async Task LinqSelectColorDepthInfrared()
        {
            //<SnippetColorInfraredDepth>
            var allGroups = await MediaFrameSourceGroup.FindAllAsync();
            var eligibleGroups = allGroups.Select(g => new
            {
                Group = g,

                // For each source kind, find the source which offers that kind of media frame,
                // or null if there is no such source.
                SourceInfos = new MediaFrameSourceInfo[]
                {
                    g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Color),
                    g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Depth),
                    g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Infrared),
                }
            }).Where(g => g.SourceInfos.Any(info => info != null)).ToList();

            if (eligibleGroups.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No source group with color, depth or infrared found.");
                return;
            }

            var selectedGroupIndex = 0; // Select the first eligible group
            MediaFrameSourceGroup selectedGroup = eligibleGroups[selectedGroupIndex].Group;
            MediaFrameSourceInfo colorSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[0];
            MediaFrameSourceInfo infraredSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[1];
            MediaFrameSourceInfo depthSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[2];
            //</SnippetColorInfraredDepth>
        }
        private async void GetRGB32PreferredFormat()
        {
            var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();

            var selectedGroupObjects = frameSourceGroups.Select(group =>
               new
               {
                   sourceGroup = group,
                   colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
                   {
                       return sourceInfo.MediaStreamType == MediaStreamType.VideoPreview
                       && sourceInfo.SourceKind == MediaFrameSourceKind.Color
                       && sourceInfo.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front;
                   })

               }).Where(t => t.colorSourceInfo != null)
               .FirstOrDefault();

            MediaFrameSourceGroup selectedGroup = selectedGroupObjects?.sourceGroup;
            MediaFrameSourceInfo colorSourceInfo = selectedGroupObjects?.colorSourceInfo;

            if (selectedGroup == null)
            {
                return;
            }

            //<SnippetGetPreferredFormat>
            var colorFrameSource = _mediaCapture.FrameSources[colorSourceInfo.Id];
            var preferredFormat = colorFrameSource.SupportedFormats.Where(format =>
            {
                return format.VideoFormat.Width >= 1080
                && format.Subtype == MediaEncodingSubtypes.Argb32;

            }).FirstOrDefault();

            if (preferredFormat == null)
            {
                // Our desired format is not supported
                return;
            }

            await colorFrameSource.SetFormatAsync(preferredFormat);
            //</SnippetGetPreferredFormat>
        }

        FrameRenderer _frameRenderer;

        private async void ActionButton2_Click(object sender, RoutedEventArgs e)
        {

            var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();


            // Color, infrared, and depth

            var selectedGroupObjects = frameSourceGroups.Select(group =>
               new
               {
                   sourceGroup = group,
                   //colorSourceInfo = group.SourceInfos.FirstOrDefault()
                   colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
                   {
                       //return sourceInfo.MediaStreamType == MediaStreamType.VideoPreview,
                       return sourceInfo.SourceKind == MediaFrameSourceKind.Color;
                       //    && sourceInfo.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front;
                   })

               }).Where(t => t.colorSourceInfo != null)
               .FirstOrDefault();

            MediaFrameSourceGroup selectedGroup = selectedGroupObjects?.sourceGroup;
            MediaFrameSourceInfo colorSourceInfo = selectedGroupObjects?.colorSourceInfo;

            if (selectedGroup == null)
            {
                return;
            }

            _mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings()
            {
                SourceGroup = selectedGroup,
                SharingMode = MediaCaptureSharingMode.ExclusiveControl,
                MemoryPreference = MediaCaptureMemoryPreference.Cpu,
                StreamingCaptureMode = StreamingCaptureMode.Video
            };
            try
            {
                await _mediaCapture.InitializeAsync(settings);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed: " + ex.Message);
                return;
            }

            var colorFrameSource = _mediaCapture.FrameSources[colorSourceInfo.Id];
            var preferredFormat = colorFrameSource.SupportedFormats.Where(format =>
            {
                return format.VideoFormat.Width == 1080;
            }).FirstOrDefault();

            /*
            if (preferredFormat == null)
            {
                // Our desired format is not supported
                return;
            }
            await colorFrameSource.SetFormatAsync(preferredFormat);
            */

            _mediaFrameReader = await _mediaCapture.CreateFrameReaderAsync(colorFrameSource, MediaEncodingSubtypes.Argb32);
            _mediaFrameReader.FrameArrived += ColorFrameReader_FrameArrived_FrameRenderer;

            _frameRenderer = new FrameRenderer(_imageElement);


            await _mediaFrameReader.StartAsync();
        }
        private void ColorFrameReader_FrameArrived_FrameRenderer(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {


            _frameRenderer.ProcessFrame(sender.TryAcquireLatestFrame());
        }
    }
}
