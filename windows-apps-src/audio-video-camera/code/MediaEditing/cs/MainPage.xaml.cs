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



//<SnippetNamespace1>
using Windows.Media.Editing;
using Windows.Media.Core;
using Windows.Media.Playback;
using System.Threading.Tasks;
//</SnippetNamespace1>


//<SnippetNamespace2>
using Windows.Media.Transcoding;
using Windows.UI.Core;
//</SnippetNamespace2>

using AudioEffectComponent;
using Windows.Media.Effects;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaEditingSnippets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        //<SnippetDeclareMediaComposition>
        private MediaComposition composition;
        //</SnippetDeclareMediaComposition>


        public MainPage()
        {
            this.InitializeComponent();

            //Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Clear();
        }
        public async void CreateComposition()
        {
            //<SnippetMediaCompositionConstructor>
            composition = new MediaComposition();
            //</SnippetMediaCompositionConstructor>

            await PickFileAndAddClip();

            UpdateMediaElementSource();

            // await PickFileAndAddClip();

            // UpdateMediaElementSource();



        }


        //<SnippetDeclareMediaStreamSource>
        private MediaStreamSource mediaStreamSource;
        //</SnippetDeclareMediaStreamSource>

        //<SnippetUpdateMediaElementSource> 
        public void UpdateMediaElementSource()
        {

            mediaStreamSource = composition.GeneratePreviewMediaStreamSource(
                (int)mediaPlayerElement.ActualWidth,
                (int)mediaPlayerElement.ActualHeight);

            mediaPlayerElement.Source = MediaSource.CreateFromMediaStreamSource(mediaStreamSource);

        }
        //</SnippetUpdateMediaElementSource>

        //<SnippetOnNavigatedFrom>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            mediaPlayerElement.Source = null;
            mediaStreamSource = null;
            base.OnNavigatedFrom(e);

        }
        //</SnippetOnNavigatedFrom>

        //<SnippetPickFileAndAddClip>
        private async Task PickFileAndAddClip()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".mp4");
            Windows.Storage.StorageFile pickedFile = await picker.PickSingleFileAsync();
            if (pickedFile == null)
            {
                ShowErrorMessage("File picking cancelled");
                return;
            }

            // These files could be picked from a location that we won't have access to later
            var storageItemAccessList = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
            storageItemAccessList.Add(pickedFile);

            var clip = await MediaClip.CreateFromFileAsync(pickedFile);
            composition.Clips.Add(clip);

        }
        //</SnippetPickFileAndAddClip>

        //<SnippetRenderCompositionToFile>
        private async Task RenderCompositionToFile()
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeChoices.Add("MP4 files", new List<string>() { ".mp4" });
            picker.SuggestedFileName = "RenderedComposition.mp4";

            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                // Call RenderToFileAsync
                var saveOperation = composition.RenderToFileAsync(file, MediaTrimmingPreference.Precise);

                saveOperation.Progress = new AsyncOperationProgressHandler<TranscodeFailureReason, double>(async (info, progress) =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                    {
                        ShowErrorMessage(string.Format("Saving file... Progress: {0:F0}%", progress));
                    }));
                });
                saveOperation.Completed = new AsyncOperationWithProgressCompletedHandler<TranscodeFailureReason, double>(async (info, status) =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                    {
                        try
                        {
                            var results = info.GetResults();
                            if (results != TranscodeFailureReason.None || status != AsyncStatus.Completed)
                            {
                                ShowErrorMessage("Saving was unsuccessful");
                            }
                            else
                            {
                                ShowErrorMessage("Trimmed clip saved to file");
                            }
                        }
                        finally
                        {
                                // Update UI whether the operation succeeded or not
                            }

                    }));
                });
            }
            else
            {
                ShowErrorMessage("User cancelled the file selection");
            }
        }
        //</SnippetRenderCompositionToFile>

        private async void RenderButton_Click(object sender, RoutedEventArgs e)
        {
            await RenderCompositionToFile();
        }

        //<SnippetTrimClipBeforeCurrentPosition>
        private void TrimClipBeforeCurrentPosition()
        {
            var currentClip = composition.Clips.FirstOrDefault(
                mc => mc.StartTimeInComposition <= mediaPlayerElement.MediaPlayer.PlaybackSession.Position &&
                mc.EndTimeInComposition >= mediaPlayerElement.MediaPlayer.PlaybackSession.Position);

            TimeSpan positionFromStart = mediaPlayerElement.MediaPlayer.PlaybackSession.Position - currentClip.StartTimeInComposition;
            currentClip.TrimTimeFromStart = positionFromStart;

        }
        //</SnippetTrimClipBeforeCurrentPosition>


        //<SnippetAddBackgroundAudioTrack>
        private async Task AddBackgroundAudioTrack()
        {
            // Add background audio
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wav");
            picker.FileTypeFilter.Add(".flac");
            Windows.Storage.StorageFile audioFile = await picker.PickSingleFileAsync();
            if (audioFile == null)
            {
                ShowErrorMessage("File picking cancelled");
                return;
            }

            // These files could be picked from a location that we won't have access to later
            var storageItemAccessList = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
            storageItemAccessList.Add(audioFile);

            var backgroundTrack = await BackgroundAudioTrack.CreateFromFileAsync(audioFile);

            composition.BackgroundAudioTracks.Add(backgroundTrack);

        }
        //</SnippetAddBackgroundAudioTrack>

        private void AddCustomAudioEffect()
        {
            //<SnippetAddCustomAudioEffect>
            // Create a property set and add a property/value pair
            PropertySet echoProperties = new PropertySet();
            echoProperties.Add("Mix", 0.5f);

            // Instantiate the custom effect defined in the 'AudioEffectComponent' project
            AudioEffectDefinition echoEffectDefinition = new AudioEffectDefinition(typeof(ExampleAudioEffect).FullName, echoProperties);

            // Add custom audio effect to the current clip in the timeline
            var currentClip = composition.Clips.FirstOrDefault(
                mc => mc.StartTimeInComposition <= mediaPlayerElement.MediaPlayer.PlaybackSession.Position &&
                mc.EndTimeInComposition >= mediaPlayerElement.MediaPlayer.PlaybackSession.Position);
            currentClip.AudioEffectDefinitions.Add(echoEffectDefinition);

            // Add custom audio effect to the first background audio track
            if (composition.BackgroundAudioTracks.Count > 0)
            {
                composition.BackgroundAudioTracks[0].AudioEffectDefinitions.Add(echoEffectDefinition);
            }
            //</SnippetAddCustomAudioEffect>
        }

        private async void AudioButton_Click(object sender, RoutedEventArgs e)
        {
            await AddBackgroundAudioTrack();

            AddCustomAudioEffect();


        }

        private void OverlayButton_Click(object sender, RoutedEventArgs e)
        {
            var overlayMediaClip = composition.Clips[0].Clone();

            AddOverlay(overlayMediaClip, .3, 0, 0, .25);

            AddOverlay(composition.Clips[0].Clone(), .3, 200, 0, 1.0);

            UpdateMediaElementSource();

        }
        //<SnippetAddOverlay>
        private void AddOverlay(MediaClip overlayMediaClip, double scale, double left, double top, double opacity)
        {
            Windows.Media.MediaProperties.VideoEncodingProperties encodingProperties =
                overlayMediaClip.GetVideoEncodingProperties();

            Rect overlayPosition;

            overlayPosition.Width = (double)encodingProperties.Width * scale;
            overlayPosition.Height = (double)encodingProperties.Height * scale;
            overlayPosition.X = left;
            overlayPosition.Y = top;

            MediaOverlay mediaOverlay = new MediaOverlay(overlayMediaClip);
            mediaOverlay.Position = overlayPosition;
            mediaOverlay.Opacity = opacity;

            MediaOverlayLayer mediaOverlayLayer = new MediaOverlayLayer();
            mediaOverlayLayer.Overlays.Add(mediaOverlay);

            composition.OverlayLayers.Add(mediaOverlayLayer);
        }
        //</SnippetAddOverlay>

        private void AppendButton_Click(object sender, RoutedEventArgs e)
        {
            CreateComposition();
        }
        private void TrimButton_Click(object sender, RoutedEventArgs e)
        {
            TrimClipBeforeCurrentPosition();

            UpdateMediaElementSource();
        }

        //<SnippetAddVideoEffect>
        private void AddVideoEffect()
        {
            var currentClip = composition.Clips.FirstOrDefault(
                mc => mc.StartTimeInComposition <= mediaPlayerElement.MediaPlayer.PlaybackSession.Position &&
                mc.EndTimeInComposition >= mediaPlayerElement.MediaPlayer.PlaybackSession.Position);

            VideoStabilizationEffectDefinition videoEffect = new VideoStabilizationEffectDefinition();
            currentClip.VideoEffectDefinitions.Add(videoEffect);
        }
        //</SnippetAddVideoEffect>

        private void EffectsButton_Click(object sender, RoutedEventArgs e)
        {

            AddVideoEffect();


            UpdateMediaElementSource();
        }
        /*
        Windows.Media.Effects.VideoTransformEffectDefinition videoEffect =
                new Windows.Media.Effects.VideoTransformEffectDefinition();

        videoEffect.Mirror = Windows.Media.MediaProperties.MediaMirroringOptions.Vertical;
            videoEffect.OutputSize = new Size(mediaElement.ActualWidth, mediaElement.ActualHeight);
        */

        //<SnippetSaveComposition>
        private async Task SaveComposition()
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeChoices.Add("Composition files", new List<string>() { ".cmp" });
            picker.SuggestedFileName = "SavedComposition";

            Windows.Storage.StorageFile compositionFile = await picker.PickSaveFileAsync();
            if (compositionFile == null)
            {
                ShowErrorMessage("User cancelled the file selection");
            }
            else
            {
                var action = composition.SaveAsync(compositionFile);
                action.Completed = (info, status) =>
                {
                    if (status != AsyncStatus.Completed)
                    {
                        ShowErrorMessage("Error saving composition");
                    }

                };
            }
        }
        //</SnippetSaveComposition>
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveComposition();
        }

        //<SnippetOpenComposition>
        private async Task OpenComposition()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".cmp");

            Windows.Storage.StorageFile compositionFile = await picker.PickSingleFileAsync();
            if (compositionFile == null)
            {
                ShowErrorMessage("File picking cancelled");
            }
            else
            {
                composition = null;
                composition = await MediaComposition.LoadAsync(compositionFile);

                if (composition != null)
                {
                    UpdateMediaElementSource();

                }
                else
                {
                    ShowErrorMessage("Unable to open composition");
                }
            }
        }
        //</SnippetOpenComposition>
        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            await OpenComposition();
        }
        public void ShowErrorMessage(string message)
        {
            MessageTextBlock.Text = message;
        }
    }
}
