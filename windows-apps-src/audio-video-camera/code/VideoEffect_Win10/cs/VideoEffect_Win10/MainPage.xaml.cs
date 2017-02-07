using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


using Windows.Storage;
using Windows.Media.Editing;


using Windows.Media.Effects;
using Windows.Media;


using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
using Windows.Media.Core;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VideoEffect_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaCapture mediaCapture;


        public MainPage()
        {
            this.InitializeComponent();

            Application.Current.Suspending += Application_Suspending;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            int scenario = 3;

            switch (scenario)
            {
                case 0: // MediaCapture SoftwareBitmap
                    PreviewControl.Visibility = Visibility.Visible;
                    mediaElement.Visibility = Visibility.Collapsed;
                    await StartPreviewAsync();
                    break;
                case 1: // MediaCapture Win2D
                    PreviewControl.Visibility = Visibility.Visible;
                    mediaElement.Visibility = Visibility.Collapsed;
                    await StartPreviewAsyncWin2D();
                    break;
                case 2: // MediaComposition
                    PreviewControl.Visibility = Visibility.Collapsed;
                    mediaElement.Visibility = Visibility.Visible;
                    AddEffectToMediaClip();
                    break;
                case 3: // Transcode
                    TranscodeWithEffect();
                    break;
            }
        }
        private async Task StartPreviewAsync()
        {
            try
            {
                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();
                PreviewControl.Source = mediaCapture;


                //<SnippetAddVideoEffectAsync>
                var videoEffectDefinition = new VideoEffectDefinition("VideoEffectComponent.ExampleVideoEffect");

                IMediaExtension videoEffect =
                   await mediaCapture.AddVideoEffectAsync(videoEffectDefinition, MediaStreamType.VideoPreview);

                videoEffect.SetProperties(new PropertySet() { { "FadeValue", .25 } });

                await mediaCapture.StartPreviewAsync();
                //</SnippetAddVideoEffectAsync>
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed.");
            }
        }

        private async Task StartPreviewAsyncWin2D()
        {
            try
            {
                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();
                PreviewControl.Source = mediaCapture;

                //TEMP
                IEnumerable<StreamPropertiesHelper> allStreamProperties =
                    mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoPreview).Select(x => new StreamPropertiesHelper(x));
                allStreamProperties = allStreamProperties.OrderByDescending(x => x.Height * x.Width);
                await mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, allStreamProperties.Last().EncodingProperties);


                //<SnippetAddVideoEffectAsyncWin2D>
                var videoEffectDefinition = new VideoEffectDefinition("VideoEffectComponent.ExampleVideoEffectWin2D");

                IMediaExtension videoEffect =
                   await mediaCapture.AddVideoEffectAsync(videoEffectDefinition, MediaStreamType.VideoPreview);

                videoEffect.SetProperties(new PropertySet() { { "BlurAmount", 1.0 } });

                await mediaCapture.StartPreviewAsync();
                //</SnippetAddVideoEffectAsyncWin2D>
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed.");
            }
        }

        private async void TranscodeWithEffect()
        {

            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            openPicker.FileTypeFilter.Add(".wmv");
            openPicker.FileTypeFilter.Add(".mp4");

            StorageFile sourceFile = await openPicker.PickSingleFileAsync();

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.VideosLibrary;

            savePicker.DefaultFileExtension = ".mp4";
            savePicker.SuggestedFileName = "New Video";

            savePicker.FileTypeChoices.Add("MPEG4", new string[] { ".mp4" });

            StorageFile destinationFile = await savePicker.PickSaveFileAsync();


            MediaEncodingProfile mediaEncodingProfile =
                MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD720p);

            //<SnippetTrancodeWithEffect>
            MediaTranscoder transcoder = new MediaTranscoder();

            // Using the in-box video stabilization effect works
            //VideoStabilizationEffectDefinition videoEffect = new VideoStabilizationEffectDefinition();
            //transcoder.AddVideoEffect(videoEffect.ActivatableClassId);

            // My custom effect throws an exception
            var customEffectDefinition = new VideoEffectDefinition("VideoEffectComponent.ExampleVideoEffect", new PropertySet() { { "FadeValue", .25 } });
            transcoder.AddVideoEffect(customEffectDefinition.ActivatableClassId);

            PrepareTranscodeResult prepareOp = await
                transcoder.PrepareFileTranscodeAsync(sourceFile, destinationFile, mediaEncodingProfile);

            if (prepareOp.CanTranscode)
            {
                var transcodeOp = prepareOp.TranscodeAsync();
            }
            //</SnippetTrancodeWithEffect>
        }
        private async void AddEffectToMediaClip()
        {
   

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".mp4");
            Windows.Storage.StorageFile pickedFile = await picker.PickSingleFileAsync();
            if (pickedFile == null)
            {
                //ShowErrorMessage("File picking cancelled");
                return;
            }

            // These files could be picked from a location that we won't have access to later
            var storageItemAccessList = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
            storageItemAccessList.Add(pickedFile);

            //<SnippetAddEffectToComposition>
            MediaComposition composition = new MediaComposition();
            var clip = await MediaClip.CreateFromFileAsync(pickedFile);
            composition.Clips.Add(clip);

            var videoEffectDefinition = new VideoEffectDefinition("VideoEffectComponent.ExampleVideoEffect", new PropertySet() { { "FadeValue", .5 } });

            clip.VideoEffectDefinitions.Add(videoEffectDefinition);
            //</SnippetAddEffectToComposition>

            var mediaStreamSource = composition.GeneratePreviewMediaStreamSource(
                (int)PreviewControl.ActualWidth,
                (int)PreviewControl.ActualHeight);

            mediaElement.SetMediaStreamSource(mediaStreamSource);
        }
        private async Task CleanupCameraAsync()
        {
            await mediaCapture.ClearEffectsAsync(MediaStreamType.VideoPreview);
            await mediaCapture.StopPreviewAsync();
            PreviewControl.Source = null;
            mediaCapture.Dispose();
            mediaCapture = null;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

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

    }
}

