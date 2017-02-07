using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.Media.Capture;
using Windows.UI.Xaml;

//<SnippetVideoStabilizationEffectUsing>
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Media.Effects;
using Windows.Media;
//</SnippetVideoStabilizationEffectUsing>

namespace SimpleCameraPreview_Win10
{
    public sealed partial class MainPage : Page
    {
        #region Basic add/remove
        
        IVideoEffectDefinition myEffectDefinition;
        IMediaExtension myPreviewEffect;
        IMediaExtension myRecordEffect;

        private async void BasicAddEffect()
        {
            //<SnippetBasicAddEffect>
            if (_mediaCapture.MediaCaptureSettings.VideoDeviceCharacteristic == VideoDeviceCharacteristic.AllStreamsIdentical ||
                _mediaCapture.MediaCaptureSettings.VideoDeviceCharacteristic == VideoDeviceCharacteristic.PreviewRecordStreamsIdentical)
            {
                // This effect will modify both the preview and the record streams, because they are the same stream.
                myRecordEffect = await _mediaCapture.AddVideoEffectAsync(myEffectDefinition, MediaStreamType.VideoRecord);
            }
            else
            {
                myRecordEffect = await _mediaCapture.AddVideoEffectAsync(myEffectDefinition, MediaStreamType.VideoRecord);
                myPreviewEffect = await _mediaCapture.AddVideoEffectAsync(myEffectDefinition, MediaStreamType.VideoPreview);
            }
            //</SnippetBasicAddEffect>
        }
        public async void RemoveOneEffect()
        {
            //<SnippetRemoveOneEffect>
            if (myRecordEffect != null)
            {
                await _mediaCapture.RemoveEffectAsync(myRecordEffect);
            }
            if(myPreviewEffect != null)
            {
                await _mediaCapture.RemoveEffectAsync(myPreviewEffect);
            }
            //</SnippetRemoveOneEffect>
        }
        public async void RemoveAllEffects()
        {
            //<SnippetClearAllEffects>
            await _mediaCapture.ClearEffectsAsync(MediaStreamType.VideoPreview);
            await _mediaCapture.ClearEffectsAsync(MediaStreamType.VideoRecord);
            //</SnippetClearAllEffects>
        }

        #endregion

        #region Video stabilization effect

        private async void AddStabilizationEffectButton_Click(object sender, RoutedEventArgs e)
        {
            InitEncodingProfileMember();
            await CreateVideoStabilizationEffectAsync();
        }
        private async void RemoveStabilizationEffectButton_Click(object sender, RoutedEventArgs e)
        {
            await CleanUpVideoStabilizationEffectAsync();
        }

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

        public void InitEncodingProfileMember()
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
                await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, recommendation.InputProperties);
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
            await _mediaCapture.RemoveEffectAsync(_videoStabilizationEffect);

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


        private void ShowMessageToUser(string message)
        {


        }
    }
}
