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

// <SnippetFramesUsing>
using Windows.Media.Capture.Frames;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.MediaProperties;
using Windows.Graphics.Imaging;
using System.Threading;
using Windows.UI.Core;
using System.Threading.Tasks;
using Windows.Media.Core;
using System.Diagnostics;
using Windows.Media;
using Windows.Media.Devices;
using Windows.Media.Audio;
// </SnippetFramesUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Frames_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        // <SnippetDeclareMediaCapture>
        MediaCapture mediaCapture;
        // </SnippetDeclareMediaCapture>

        // <SnippetDeclareMediaFrameReader>
        MediaFrameReader mediaFrameReader;
        // </SnippetDeclareMediaFrameReader>

        // <SnippetDeclareBackBuffer>
        private SoftwareBitmap backBuffer;
        private bool taskRunning = false;
        // </SnippetDeclareBackBuffer>

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            // <SnippetImageElementSource>
            imageElement.Source = new SoftwareBitmapSource();
            // </SnippetImageElementSource>

            // <SnippetFindAllAsync>
            var frameSourceGroups = await MediaFrameSourceGroup.FindAllAsync();
            // </SnippetFindAllAsync>

            // Color, infrared, and depth


            // <SnippetSelectColor>
            var selectedGroupObjects = frameSourceGroups.Select(group =>
               new
               {
                   sourceGroup = group,
                   colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
                   {
                       // On Xbox/Kinect, omit the MediaStreamType and EnclosureLocation tests
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
            // </SnippetSelectColor>

            // <SnippetInitMediaCapture>
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
            // </SnippetInitMediaCapture>


            var colorFrameSource = mediaCapture.FrameSources[colorSourceInfo.Id];
            var preferredFormat = colorFrameSource.SupportedFormats.Where(format =>
            {
                return format.VideoFormat.Width == 1920;
            }).FirstOrDefault();

            if (preferredFormat == null)
            {
                // Our desired format is not supported
                return;
            }
            await colorFrameSource.SetFormatAsync(preferredFormat);

            // <SnippetCreateFrameReader>
            mediaFrameReader = await mediaCapture.CreateFrameReaderAsync(colorFrameSource, MediaEncodingSubtypes.Argb32);
            mediaFrameReader.FrameArrived += ColorFrameReader_FrameArrived;
            await mediaFrameReader.StartAsync();
            // </SnippetCreateFrameReader>
        }
        // <SnippetFrameArrived>
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
                softwareBitmap = Interlocked.Exchange(ref backBuffer, softwareBitmap);
                softwareBitmap?.Dispose();

                // Changes to XAML ImageElement must happen on UI thread through Dispatcher
                var task = imageElement.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        // Don't let two copies of this task run at the same time.
                        if (taskRunning)
                        {
                            return;
                        }
                        taskRunning = true;

                        // Keep draining frames from the backbuffer until the backbuffer is empty.
                        SoftwareBitmap latestBitmap;
                        while ((latestBitmap = Interlocked.Exchange(ref backBuffer, null)) != null)
                        {
                            var imageSource = (SoftwareBitmapSource)imageElement.Source;
                            await imageSource.SetBitmapAsync(latestBitmap);
                            latestBitmap.Dispose();
                        }

                        taskRunning = false;
                    });
            }

            mediaFrameReference.Dispose();
        }
        // </SnippetFrameArrived>

        private async Task Cleanup()
        {
            // <SnippetCleanup>
            await mediaFrameReader.StopAsync();
            mediaFrameReader.FrameArrived -= ColorFrameReader_FrameArrived;
            mediaCapture.Dispose();
            mediaCapture = null;
            // </SnippetCleanup>
        }
        private async Task SimpleSelect()
        {
            // <SnippetSimpleSelect>
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
            // </SnippetSimpleSelect>
        }
        private async Task LinqSelectColorDepthInfrared()
        {
            // <SnippetColorInfraredDepth>
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
            // </SnippetColorInfraredDepth>
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

            // <SnippetGetPreferredFormat>
            var colorFrameSource = mediaCapture.FrameSources[colorSourceInfo.Id];
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
            // </SnippetGetPreferredFormat>
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
                   colorSourceInfo = group.SourceInfos.FirstOrDefault((sourceInfo) =>
                   {
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
            var preferredFormat = colorFrameSource.SupportedFormats.Where(format =>
            {
                return format.VideoFormat.Width == 1920;
            }).FirstOrDefault();


            if (preferredFormat == null)
            {
                // Our desired format is not supported
                return;
            }
            await colorFrameSource.SetFormatAsync(preferredFormat);


            mediaFrameReader = await mediaCapture.CreateFrameReaderAsync(colorFrameSource, MediaEncodingSubtypes.Argb32);
            mediaFrameReader.FrameArrived += ColorFrameReader_FrameArrived_FrameRenderer;

            _frameRenderer = new FrameRenderer(imageElement);


            await mediaFrameReader.StartAsync();
        }
        private void ColorFrameReader_FrameArrived_FrameRenderer(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {
            

            _frameRenderer.ProcessFrame(sender.TryAcquireLatestFrame());
        }

        private void MultiFrameButton_Click(object sender, RoutedEventArgs e)
        {
            InitMultiFrame();
        }

        // <SnippetMultiFrameDeclarations>
        private MultiSourceMediaFrameReader _multiFrameReader = null;
        private string _colorSourceId = null;
        private string _depthSourceId = null;

        
        private readonly ManualResetEventSlim _frameReceived = new ManualResetEventSlim(false);
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        public event EventHandler CorrelationFailed;
        // </SnippetMultiFrameDeclarations>

        private async void InitMultiFrame()
        {
            // <SnippetSelectColorAndDepth>
            var allGroups = await MediaFrameSourceGroup.FindAllAsync();
            var eligibleGroups = allGroups.Select(g => new
            {
                Group = g,

                // For each source kind, find the source which offers that kind of media frame,
                // or null if there is no such source.
                SourceInfos = new MediaFrameSourceInfo[]
                {
                    g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Color),
                    g.SourceInfos.FirstOrDefault(info => info.SourceKind == MediaFrameSourceKind.Depth)
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
            MediaFrameSourceInfo depthSourceInfo = eligibleGroups[selectedGroupIndex].SourceInfos[1];
            // </SnippetSelectColorAndDepth>


            // <SnippetMultiFrameInitMediaCapture>
            mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings()
            {
                SourceGroup = selectedGroup,
                SharingMode = MediaCaptureSharingMode.ExclusiveControl,
                MemoryPreference = MediaCaptureMemoryPreference.Cpu,
                StreamingCaptureMode = StreamingCaptureMode.Video
            };

            await mediaCapture.InitializeAsync(settings);
            // </SnippetMultiFrameInitMediaCapture>


            // <SnippetGetColorAndDepthSource>
            MediaFrameSource colorSource =
                mediaCapture.FrameSources.Values.FirstOrDefault(
                    s => s.Info.SourceKind == MediaFrameSourceKind.Color);

            MediaFrameSource depthSource =
                mediaCapture.FrameSources.Values.FirstOrDefault(
                    s => s.Info.SourceKind == MediaFrameSourceKind.Depth);

            if (colorSource == null || depthSource == null)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture doesn't have the Color and Depth streams");
                return;
            }

            _colorSourceId = colorSource.Info.Id;
            _depthSourceId = depthSource.Info.Id;
            // </SnippetGetColorAndDepthSource>

            // <SnippetInitMultiFrameReader>
            _multiFrameReader = await mediaCapture.CreateMultiSourceFrameReaderAsync(
                new[] { colorSource, depthSource });

            _multiFrameReader.FrameArrived += MultiFrameReader_FrameArrived;

            _frameRenderer = new FrameRenderer(imageElement);

            MultiSourceMediaFrameReaderStartStatus startStatus =
                await _multiFrameReader.StartAsync();

            if (startStatus != MultiSourceMediaFrameReaderStartStatus.Success)
            {
                throw new InvalidOperationException(
                    "Unable to start reader: " + startStatus);
            }

            this.CorrelationFailed += MainPage_CorrelationFailed;
            Task.Run(() => NotifyAboutCorrelationFailure(_tokenSource.Token));
            // </SnippetInitMultiFrameReader>
        }


        // <SnippetMultiFrameArrived>
        private void MultiFrameReader_FrameArrived(MultiSourceMediaFrameReader sender, MultiSourceMediaFrameArrivedEventArgs args)
        {
            using (MultiSourceMediaFrameReference muxedFrame =
                sender.TryAcquireLatestFrame())
            using (MediaFrameReference colorFrame =
                muxedFrame.TryGetFrameReferenceBySourceId(_colorSourceId))
            using (MediaFrameReference depthFrame =
                muxedFrame.TryGetFrameReferenceBySourceId(_depthSourceId))
            {
                // Notify the listener thread that the frame has been received.
                _frameReceived.Set();
                _frameRenderer.ProcessFrame(depthFrame);
            }
        }
        // </SnippetMultiFrameArrived>

        // <SnippetNotifyCorrelationFailure>
        private void NotifyAboutCorrelationFailure(CancellationToken token)
        {
            // If in 5 seconds the token is not cancelled and frame event is not signaled,
            // correlation is most likely failed.
            if (WaitHandle.WaitAny(new[] { token.WaitHandle, _frameReceived.WaitHandle }, 5000)
                    == WaitHandle.WaitTimeout)
            {
                CorrelationFailed?.Invoke(this, EventArgs.Empty);
            }
        }
        // </SnippetNotifyCorrelationFailure>
        // <SnippetCorrelationFailure>
        private async void MainPage_CorrelationFailed(object sender, EventArgs e)
        {
            await _multiFrameReader.StopAsync();
            _multiFrameReader.FrameArrived -= MultiFrameReader_FrameArrived;
            mediaCapture.Dispose();
            mediaCapture = null;
        }
        // </SnippetCorrelationFailure>

        private void SetBufferedFrameAcquisitionMode()
        {
            // <SnippetSetBufferedFrameAcquisitionMode>
            mediaFrameReader.AcquisitionMode = MediaFrameReaderAcquisitionMode.Buffered;
            // </SnippetSetBufferedFrameAcquisitionMode>
        }

        private async void MediaSourceFromFrameSource_Click(object sender, RoutedEventArgs e)
        {





            // <SnippetMediaSourceSelectGroup>
            var allGroups = await MediaFrameSourceGroup.FindAllAsync();
            var eligibleGroups = allGroups.Select(g => new
            {
                Group = g,

                // For each source kind, find the source which offers that kind of media frame,
                // or null if there is no such source.
                SourceInfos = new MediaFrameSourceInfo[]
                {
                    g.SourceInfos.FirstOrDefault(info => info.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front
                        && info.SourceKind == MediaFrameSourceKind.Color),
                    g.SourceInfos.FirstOrDefault(info => info.DeviceInformation?.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back
                        && info.SourceKind == MediaFrameSourceKind.Color)
                }
            }).Where(g => g.SourceInfos.Any(info => info != null)).ToList();

            if (eligibleGroups.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No source group with front and back-facing camera found.");
                return;
            }

            var selectedGroupIndex = 0; // Select the first eligible group
            MediaFrameSourceGroup selectedGroup = eligibleGroups[selectedGroupIndex].Group;
            MediaFrameSourceInfo frontSourceInfo = selectedGroup.SourceInfos[0];
            MediaFrameSourceInfo backSourceInfo = selectedGroup.SourceInfos[1];
            // </SnippetMediaSourceSelectGroup>

            // <SnippetMediaSourceInitMediaCapture>
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
            // </SnippetMediaSourceInitMediaCapture>


            // <SnippetMediaSourceMediaPlayer>
            var frameMediaSource1 = MediaSource.CreateFromMediaFrameSource(mediaCapture.FrameSources[frontSourceInfo.Id]);
            mediaPlayerElement1.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer());
            mediaPlayerElement1.MediaPlayer.Source = frameMediaSource1;
            mediaPlayerElement1.AutoPlay = true;

            var frameMediaSource2 = MediaSource.CreateFromMediaFrameSource(mediaCapture.FrameSources[backSourceInfo.Id]);
            mediaPlayerElement2.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer());
            mediaPlayerElement2.MediaPlayer.Source = frameMediaSource2;
            mediaPlayerElement2.AutoPlay = true;
            // </SnippetMediaSourceMediaPlayer>

        }
        #region Audio frames
        AudioDeviceController audioDeviceController;

        private void AudioFrame1Button_Click(object sender, RoutedEventArgs e)
        {

            InitAudioFrameReader();
        }
        private async void InitAudioFrameReader()
        {
            //<SnippetInitAudioFrameSource>
            mediaCapture = new MediaCapture();
            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings()
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio,
            };
            await mediaCapture.InitializeAsync(settings);

            var audioFrameSources = mediaCapture.FrameSources.Where(x => x.Value.Info.MediaStreamType == MediaStreamType.Audio);

            if (audioFrameSources.Count() == 0)
            {
                Debug.WriteLine("No audio frame source was found.");
                return;
            }

            MediaFrameSource frameSource = audioFrameSources.FirstOrDefault().Value;
            
            MediaFrameFormat format = frameSource.CurrentFormat;
            if (format.Subtype != MediaEncodingSubtypes.Float)
            {
                return;
            }

            if (format.AudioEncodingProperties.ChannelCount != 1
                || format.AudioEncodingProperties.SampleRate != 48000)
            {
                return;
            }
            //</SnippetInitAudioFrameSource>

            //<SnippetCreateAudioFrameReader>
            mediaFrameReader = await mediaCapture.CreateFrameReaderAsync(frameSource);

            // Optionally set acquisition mode. Buffered is the default mode for audio.
            mediaFrameReader.AcquisitionMode = MediaFrameReaderAcquisitionMode.Buffered;

            mediaFrameReader.FrameArrived += MediaFrameReader_AudioFrameArrived;

            var status = await mediaFrameReader.StartAsync();

            if (status != MediaFrameReaderStartStatus.Success)
            {
                Debug.WriteLine("The MediaFrameReader couldn't start.");
            }
            //</SnippetCreateAudioFrameReader>
        }



        //<SnippetProcessAudioFrame>
        private void MediaFrameReader_AudioFrameArrived(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
        {
            using (MediaFrameReference reference = sender.TryAcquireLatestFrame())
            {
                if (reference != null)
                {
                    ProcessAudioFrame(reference.AudioMediaFrame);
                }
            }
        }
        unsafe private void ProcessAudioFrame(AudioMediaFrame audioMediaFrame)
        {

            using (AudioFrame audioFrame = audioMediaFrame.GetAudioFrame())
            using (AudioBuffer buffer = audioFrame.LockBuffer(AudioBufferAccessMode.Read))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;
                float* dataInFloat;


                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);
                
                // The requested format was float
                dataInFloat = (float*)dataInBytes;

                // Get the number of samples by multiplying the duration by sampling rate: 
                // duration [s] x sampling rate [samples/s] = # samples 

                // Duration can be gotten off the frame reference OR the audioFrame
                TimeSpan duration = audioMediaFrame.FrameReference.Duration;

                // frameDurMs is in milliseconds, while SampleRate is given per second.
                uint frameDurMs = (uint)duration.TotalMilliseconds;
                uint sampleRate = audioMediaFrame.AudioEncodingProperties.SampleRate;
                uint sampleCount = (frameDurMs * sampleRate) / 1000;

            }
        }
        //</SnippetProcessAudioFrame>

        private void MuteAudioDeviceController(MediaFrameSource frameSource)
        {
            //<SnippetAudioDeviceController>
            audioDeviceController = frameSource.Controller.AudioDeviceController;
            //</SnippetAudioDeviceController>

            //<SnippetAudioDeviceControllerMute>
            audioDeviceController.Muted = true;
            //</SnippetAudioDeviceControllerMute>
        }

        AudioGraph audioGraph;
        AudioFrameInputNode audioFrameInputNode;
        private async void MockAudioGraph(AudioFrame audioFrame)
        {
            AudioGraphSettings settings = new AudioGraphSettings(Windows.Media.Render.AudioRenderCategory.Media);
            var result = await AudioGraph.CreateAsync(settings);
            var graph = result.Graph;

            audioFrameInputNode = graph.CreateFrameInputNode();

            //<SnippetAudioFrameInputNode>
            audioFrameInputNode.AddFrame(audioFrame);
            //</SnippetAudioFrameInputNode>
        }
        #endregion

        //<SnippetGetSettingsWithProfile>
        public async Task<MediaCaptureInitializationSettings> FindHdrWithWcgPhotoProfile()
        {
            IReadOnlyList<MediaFrameSourceGroup> sourceGroups = await MediaFrameSourceGroup.FindAllAsync();
            MediaCaptureInitializationSettings settings = null;

            foreach (MediaFrameSourceGroup sourceGroup in sourceGroups)
            {
                // Find a device that support AdvancedColorPhoto
                IReadOnlyList<MediaCaptureVideoProfile> profileList = MediaCapture.FindKnownVideoProfiles(
                                              sourceGroup.Id,
                                              KnownVideoProfile.HdrWithWcgPhoto);

                if (profileList.Count > 0)
                {
                    settings = new MediaCaptureInitializationSettings();
                    settings.VideoProfile = profileList[0];
                    settings.VideoDeviceId = sourceGroup.Id;
                    break;
                }
            }
            return settings;
        }

        private void StartDeviceWatcherButton_Click(object sender, RoutedEventArgs e)
        {
            var remoteCameraHelper = new RemoteCameraPairingHelper(this.Dispatcher);
        }

        //</SnippetGetSettingsWithProfile>


    }
}
