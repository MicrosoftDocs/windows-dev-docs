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

//<SnippetUsing>
using Windows.Media.Capture;
using Windows.Foundation.Metadata;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.ApplicationModel;
//</SnippetUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ScreenCaptureWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetMembers>
        MediaCapture _mediaCapture;
        bool _isScreenCaptureAvailable = false;
        bool _isRecording = false;
        //</SnippetMembers>

        public MainPage()
        {
            this.InitializeComponent();

            //<SnippetApiInformation>
            if (ApiInformation.IsTypePresent("Windows.Media.Capture.ScreenCapture"))
            {
                _isScreenCaptureAvailable = true;
            }
            //</SnippetApiInformation>

            //<SnippetRegisterAppLifetimeEvents>
            Application.Current.Suspending += Application_Suspending;
            Application.Current.Resuming += Application_Resuming;
            //</SnippetRegisterAppLifetimeEvents>
        }

        //<SnippetInitMediaCapture>
        public async void InitMediaCapture()
        {
            if (!_isScreenCaptureAvailable)
            {
                return;
            }

            try
            {
                // Get instance of the ScreenCapture object
                var screenCapture = ScreenCapture.GetForCurrentView();

                // Set the MediaCaptureInitializationSettings to use the ScreenCapture as the
                // audio and video source.
                var settings = new MediaCaptureInitializationSettings();
                settings.VideoSource = screenCapture.VideoSource;
                settings.AudioSource = screenCapture.AudioSource;
                settings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.AudioAndVideo;

                // Initialize the MediaCapture with the initialization settings.
                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync(settings);

                // Hook up events for the Failed, RecordingLimitationExceeded, and SourceSuspensionChanged events
                _mediaCapture.Failed += new Windows.Media.Capture.MediaCaptureFailedEventHandler(MediaCapture_Failed);
                _mediaCapture.RecordLimitationExceeded +=
                    new Windows.Media.Capture.RecordLimitationExceededEventHandler(MediaCapture_RecordLimitationExceeded);
                screenCapture.SourceSuspensionChanged +=
                    new Windows.Foundation.TypedEventHandler<ScreenCapture, SourceSuspensionChangedEventArgs>(SourceSuspensionChanged);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("InitMediaCapture Exception: " + ex.Message);
            }
        }
        //</SnippetInitMediaCapture>

        //<SnippetStartRecording>
        private async void StartRecording()
        {
            if(_mediaCapture == null)
            {
                return;
            }

            try
            {
                // Create a file to record to.                 
                var videoFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("recording.mp4", CreationCollisionOption.ReplaceExisting);

                // Create an encoding profile to use.                  
                var profile = Windows.Media.MediaProperties.MediaEncodingProfile.CreateMp4(Windows.Media.MediaProperties.VideoEncodingQuality.HD1080p);

                // Start recording
                await _mediaCapture.StartRecordToStorageFileAsync(profile, videoFile);
                _isRecording = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StartRecording Exception: " + ex.Message);
            }

        }
        //</SnippetStartRecording>

        //<SnippetStopRecording>
        private async void StopRecording()
        {
            try
            {
                //Stop Screen Recorder                  
                await _mediaCapture.StopRecordAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StartRecording Exception: " + ex.Message);
            }
        }
        //</SnippetStopRecording>
        //<SnippetSourceSuspensionChanged>
        private void SourceSuspensionChanged(ScreenCapture sender, SourceSuspensionChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("SourceSuspensionChanged Event. Args: IsAudioSuspended:" +
                args.IsAudioSuspended.ToString() +
                " IsVideoSuspended:" +
                args.IsVideoSuspended.ToString());
        }
        //</SnippetSourceSuspensionChanged>

        //<SnippetRecordLimitationExceeded>
        private void MediaCapture_RecordLimitationExceeded(MediaCapture sender)
        {
            StopRecording();
        }
        //</SnippetRecordLimitationExceeded>

        //<SnippetCaptureFailed>
        private async void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            await CleanupCaptureResources();
        }
        //</SnippetCaptureFailed>

        //<SnippetCleanupCaptureResources>
        public async Task CleanupCaptureResources()
        {
            if (_isRecording && _mediaCapture != null)
            {
                await _mediaCapture.StopRecordAsync();
                _isRecording = false;
            }

            if (_mediaCapture != null)
            {
                _mediaCapture.Dispose();
            }
        }
        //</SnippetCleanupCaptureResources>

        //<SnippetSuspending>
        private async void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                var deferral = e.SuspendingOperation.GetDeferral();

                await CleanupCaptureResources();

                deferral.Complete();
            }
        }
        //</SnippetSuspending>
        //<SnippetResuming>
        private void Application_Resuming(object sender, object o)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                InitMediaCapture();
            }
        }
        //</SnippetResuming>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_isRecording)
            {
                InitMediaCapture();
                StartRecording();
            }
            else
            {
                StopRecording();
            }
            
        }
        


    }
}
