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
using Windows.UI.Core;

// <SnippetAdaptiveStreamingUsing>
using Windows.Media.Streaming.Adaptive;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Media.Playback;
using Windows.Media.Core;
// </SnippetAdaptiveStreamingUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdaptiveStreaming_RS1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // <SnippetDeclareMediaPlayer>
        MediaPlayer _mediaPlayer;
        // </SnippetDeclareMediaPlayer>

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ManifestSource();
            InitializeAdaptiveMediaSource(new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)"));
        }

        public void ManifestSourceNoUI()
        {
            // <SnippetManifestSourceNoUI>
            System.Uri manifestUri = new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)");
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(manifestUri);
            _mediaPlayer.Play();
            // </SnippetManifestSourceNoUI>
        }

        public void ManifestSource()
        {
            // <SnippetManifestSource>
            System.Uri manifestUri = new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)");
            mediaPlayerElement.Source = MediaSource.CreateFromUri(manifestUri);
            mediaPlayerElement.MediaPlayer.Play();
            // </SnippetManifestSource>

        }

        // <SnippetDeclareAMS>
        AdaptiveMediaSource ams;
        // </SnippetDeclareAMS>

        // <SnippetInitializeAMS>
        async private void InitializeAdaptiveMediaSource(System.Uri uri)
        {
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(uri);

            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                ams = result.MediaSource;
                mediaPlayerElement.SetMediaPlayer(new MediaPlayer());
                mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromAdaptiveMediaSource(ams);
                mediaPlayerElement.MediaPlayer.Play();


                ams.InitialBitrate = ams.AvailableBitrates.Max<uint>();

                //Register for download requests
                ams.DownloadRequested += DownloadRequested;

                //Register for download failure and completion events
                ams.DownloadCompleted += DownloadCompleted;
                ams.DownloadFailed += DownloadFailed;

                //Register for bitrate change events
                ams.DownloadBitrateChanged += DownloadBitrateChanged;
                ams.PlaybackBitrateChanged += PlaybackBitrateChanged;

                //Register for diagnostic event
                ams.Diagnostics.DiagnosticAvailable += DiagnosticAvailable;
            }
            else
            {
                // Handle failure to create the adaptive media source
                MyLogMessageFunction($"Adaptive source creation failed: {uri} - {result.ExtendedError}");
            }
        }

        

        // </SnippetInitializeAMS>


        // <SnippetDeclareHttpClient>
        Windows.Web.Http.HttpClient httpClient;
        // </SnippetDeclareHttpClient>

        async private void InitializeAdaptiveMediaSourceWithCustomHeaders(System.Uri uri)
        {
            System.Uri manifestUri = new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)");

            // <SnippetInitializeAMSWithHttpClient>
            httpClient = new Windows.Web.Http.HttpClient();
            httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-CustomHeader", "This is a custom header");
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(manifestUri, httpClient);
            // </SnippetInitializeAMSWithHttpClient>

            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                ams = result.MediaSource;
                mediaPlayerElement.SetMediaPlayer(new MediaPlayer());
                mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromAdaptiveMediaSource(ams);
                mediaPlayerElement.MediaPlayer.Play();

                txtDownloadBitrate.Text = ams.InitialBitrate.ToString();
                txtPlaybackBitrate.Text = ams.InitialBitrate.ToString();

                //Register for download requests
                ams.DownloadRequested += DownloadRequested;

                //Register for bitrate change events
                ams.DownloadBitrateChanged += DownloadBitrateChanged;
                ams.PlaybackBitrateChanged += PlaybackBitrateChanged;
            }
            else
            {
                // Handle failure to create the adaptive media source
                MyLogMessageFunction($"Adaptive source creation failed: {manifestUri} - {result.ExtendedError}");
            }
        }

        // <SnippetAMSDownloadRequested>
        private async void DownloadRequested(AdaptiveMediaSource sender, AdaptiveMediaSourceDownloadRequestedEventArgs args)
        {

            // rewrite key URIs to replace http:// with https://
            if (args.ResourceType == AdaptiveMediaSourceResourceType.Key)
            {
                string originalUri = args.ResourceUri.ToString();
                string secureUri = originalUri.Replace("http:", "https:");

                // override the URI by setting property on the result sub object
                args.Result.ResourceUri = new Uri(secureUri);
            }

            if (args.ResourceType == AdaptiveMediaSourceResourceType.Manifest)
            {
                AdaptiveMediaSourceDownloadRequestedDeferral deferral = args.GetDeferral();
                args.Result.Buffer = await CreateMyCustomManifest(args.ResourceUri);
                deferral.Complete();
            }

            if (args.ResourceType == AdaptiveMediaSourceResourceType.MediaSegment)
            {
                var resourceUri = args.ResourceUri.ToString() + "?range=" + 
                    args.ResourceByteRangeOffset + "-" + (args.ResourceByteRangeLength - 1);

                // override the URI by setting a property on the result sub object
                args.Result.ResourceUri = new Uri(resourceUri);

                // clear the byte range properties on the result sub object
                args.Result.ResourceByteRangeOffset = null;
                args.Result.ResourceByteRangeLength = null;
            }
        }
    
        // </SnippetAMSDownloadRequested>

        private Task<IBuffer> CreateMyCustomManifest(Uri resourceUri)
        {
            httpClient = new Windows.Web.Http.HttpClient();
            return httpClient.GetBufferAsync(resourceUri).AsTask<IBuffer, Windows.Web.Http.HttpProgress>();
        }

        // <SnippetAMSDownloadCompleted>
        private void DownloadCompleted(AdaptiveMediaSource sender, AdaptiveMediaSourceDownloadCompletedEventArgs args)
        {
            var statistics = args.Statistics;

            MyLogMessageFunction("download completed for: " + args.ResourceType + " - " +
             args.ResourceUri +
             " – RequestId:" + args.RequestId +
             " – Position:" + args.Position +
             " - Duration:" + args.ResourceDuration +
             " - ContentType:" + args.ResourceContentType +
             " - TimeToHeadersReceived:" + statistics.TimeToHeadersReceived + 
             " - TimeToFirstByteReceived:" + statistics.TimeToFirstByteReceived + 
             " - TimeToLastByteReceived:" + statistics.TimeToLastByteReceived +
             " - ContentBytesReceivedCount:" + statistics.ContentBytesReceivedCount);

        }
        // </SnippetAMSDownloadCompleted>

        // <SnippetAMSDownloadFailed>
        private void DownloadFailed(AdaptiveMediaSource sender, AdaptiveMediaSourceDownloadFailedEventArgs args)
        {
            var statistics = args.Statistics;

            MyLogMessageFunction("download failed for: " + args.ResourceType + 
             " - " + args.ResourceUri +
             " – Error:" + args.ExtendedError.HResult +
             " - RequestId" + args.RequestId + 
             " – Position:" + args.Position +
             " - Duration:" + args.ResourceDuration +
             " - ContentType:" + args.ResourceContentType +
             " - TimeToHeadersReceived:" + statistics.TimeToHeadersReceived + 
             " - TimeToFirstByteReceived:" + statistics.TimeToFirstByteReceived + 
             " - TimeToLastByteReceived:" + statistics.TimeToLastByteReceived +
             " - ContentBytesReceivedCount:" + statistics.ContentBytesReceivedCount);

        }
        // </SnippetAMSDownloadFailed>

        // <SnippetAMSBitrateEvents>
        private async void DownloadBitrateChanged(AdaptiveMediaSource sender, AdaptiveMediaSourceDownloadBitrateChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
            {
                txtDownloadBitrate.Text = args.NewValue.ToString();
            }));
        }

        private async void PlaybackBitrateChanged(AdaptiveMediaSource sender, AdaptiveMediaSourcePlaybackBitrateChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
            {
                txtPlaybackBitrate.Text = args.NewValue.ToString();
            }));
        }
        // </SnippetAMSBitrateEvents>

        // <SnippetAMSDiagnosticAvailable>
        private void DiagnosticAvailable(AdaptiveMediaSourceDiagnostics sender, AdaptiveMediaSourceDiagnosticAvailableEventArgs args)
        {
            MySendTelemetryFunction(args.RequestId, args.Position,
                                    args.DiagnosticType, args.SegmentId,
                                    args.ResourceType, args.ResourceUri,
                                    args.ResourceDuration, args.ResourceContentType,
                                    args.ResourceByteRangeOffset,
                                    args.ResourceByteRangeLength, 
                                    args.Bitrate,
                                    args.ExtendedError);

        }
        // </SnippetAMSDiagnosticAvailable>


        private void MyLogMessageFunction(string s)
        {

        }
        private void MySendTelemetryFunction(int? requestId, TimeSpan? position,
            AdaptiveMediaSourceDiagnosticType? type, ulong? segmentId,
            AdaptiveMediaSourceResourceType? resourceType, Uri resourceUri,
            TimeSpan? duration, string resourceContentType,
            ulong? byteRangeOffset, ulong? byteRangeLength, uint? bitrate, Exception extendedError)
        {

        }
    }
}
