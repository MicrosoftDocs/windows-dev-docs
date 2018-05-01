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

// <SnippetUsing>
using Windows.Storage;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
// </SnippetUsing>

// <SnippetCodecQueryUsing>
using Windows.Media.Core;
// </SnippetCodecQueryUsing>

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TranscodeWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }




        private async void TranscodeVideoFile()
        {
            // <SnippetTranscodeGetFile>
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            openPicker.FileTypeFilter.Add(".wmv");
            openPicker.FileTypeFilter.Add(".mp4");

            StorageFile source = await openPicker.PickSingleFileAsync();

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.VideosLibrary;

            savePicker.DefaultFileExtension = ".mp4";
            savePicker.SuggestedFileName = "New Video";

            savePicker.FileTypeChoices.Add("MPEG4", new string[] { ".mp4" });

            StorageFile destination = await savePicker.PickSaveFileAsync();
            // </SnippetTranscodeGetFile>

            // <SnippetTranscodeMediaProfile>
            MediaEncodingProfile profile =
                MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD720p);
            // </SnippetTranscodeMediaProfile>

            // <SnippetTranscodeTranscodeFile>
            MediaTranscoder transcoder = new MediaTranscoder();

            PrepareTranscodeResult prepareOp = await
                transcoder.PrepareFileTranscodeAsync(source, destination, profile);

            if (prepareOp.CanTranscode)
            {
                var transcodeOp = prepareOp.TranscodeAsync();

                transcodeOp.Progress +=
                    new AsyncActionProgressHandler<double>(TranscodeProgress);
                transcodeOp.Completed +=
                    new AsyncActionWithProgressCompletedHandler<double>(TranscodeComplete);
            }
            else
            {
                switch (prepareOp.FailureReason)
                {
                    case TranscodeFailureReason.CodecNotFound:
                        System.Diagnostics.Debug.WriteLine("Codec not found.");
                        break;
                    case TranscodeFailureReason.InvalidProfile:
                        System.Diagnostics.Debug.WriteLine("Invalid profile.");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unknown failure.");
                        break;
                }
            }
            // </SnippetTranscodeTranscodeFile>
        }



        // <SnippetTranscodeCallbacks>
        void TranscodeProgress(IAsyncActionWithProgress<double> asyncInfo, double percent)
        {
            // Display or handle progress info.
        }

        void TranscodeComplete(IAsyncActionWithProgress<double> asyncInfo, AsyncStatus status)
        {
            asyncInfo.GetResults();
            if (asyncInfo.Status == AsyncStatus.Completed)
            {
                // Display or handle complete info.
            }
            else if (asyncInfo.Status == AsyncStatus.Canceled)
            {
                // Display or handle cancel info.
            }
            else
            {
                // Display or handle error info.
            }
        }
        // </SnippetTranscodeCallbacks>

        private void TranscodeButton_Click(object sender, RoutedEventArgs e)
        {
            TranscodeVideoFile();
        }

        

        private async void CodecQueryButton_Click(object sender, RoutedEventArgs e)
        {
            // <SnippetNewCodecQuery>
            var codecQuery = new CodecQuery();
            // </SnippetNewCodecQuery>

            // <SnippetFindAllEncoders>
            IReadOnlyList<CodecInfo> result =
                await codecQuery.FindAllAsync(CodecKind.Video, CodecCategory.Encoder, "");

            foreach (var codecInfo in result)
            {
                this.codecResultsTextBox.Text += "============================================================\n";
                this.codecResultsTextBox.Text += string.Format("Codec: {0}\n", codecInfo.DisplayName);
                this.codecResultsTextBox.Text += string.Format("Kind: {0}\n", codecInfo.Kind.ToString());
                this.codecResultsTextBox.Text += string.Format("Category: {0}\n", codecInfo.Category.ToString());
                this.codecResultsTextBox.Text += string.Format("Trusted: {0}\n", codecInfo.IsTrusted.ToString());

                foreach (string subType in codecInfo.Subtypes)
                {
                    this.codecResultsTextBox.Text += string.Format("   Subtype: {0}\n", subType);
                }
            }
            // </SnippetFindAllEncoders>


            // <SnippetIsH264Supported>
            IReadOnlyList<CodecInfo> h264Result = await codecQuery.FindAllAsync(CodecKind.Video, CodecCategory.Decoder, "H264");

            if (h264Result.Count > 0)
            {
                this.codecResultsTextBox.Text = "H264 decoder is present.";
            }
            // </SnippetIsH264Supported>


            // <SnippetIsFLACSupported>
            IReadOnlyList<CodecInfo> flacResult = 
                await codecQuery.FindAllAsync(CodecKind.Audio, CodecCategory.Encoder, CodecSubtypes.AudioFormatFlac);

            if (flacResult.Count > 0)
            {
                AudioEncodingProperties audioProps = new AudioEncodingProperties();
                audioProps.Subtype = CodecSubtypes.AudioFormatFlac;
                audioProps.SampleRate = 44100;
                audioProps.ChannelCount = 2;
                audioProps.Bitrate = 128000;
                audioProps.BitsPerSample = 32;

                MediaEncodingProfile encodingProfile = new MediaEncodingProfile();
                encodingProfile.Audio = audioProps;
                encodingProfile.Video = null;
            }
            // </SnippetIsFLACSupported>

        }

        public TimedMetadataStreamDescriptor CreateStreamDescriptorForGpmdEncodingSubtype()
        {
            //<SnippetGetStreamDescriptor>
            TimedMetadataEncodingProperties encodingProperties = new TimedMetadataEncodingProperties
            {
                Subtype = "{67706D64-BF10-48B4-BC18-593DC1DB950F}"
            };
            // The FormatUserData is set to information that describes the format found in the 'gpmd' file.
            // If the 'gpmd' box is empty, there is no need to specify FormatUserData.
            byte[] streamDescriptionData = GetStreamDescriptionDataForGpmdEncodingSubtype();
            encodingProperties.SetFormatUserData(streamDescriptionData);

            TimedMetadataStreamDescriptor descriptor = new TimedMetadataStreamDescriptor(encodingProperties)
            {
                Name = "GPS Info",
                Label = "GPS Info"
            };
            //</SnippetGetStreamDescriptor>
            return descriptor;
        }

        private byte[] GetStreamDescriptionDataForGpmdEncodingSubtype()
        {
            return new byte[1];
        }
        //<SnippetGetMediaEncodingProfile>
        public MediaEncodingProfile CreateProfileForTranscoder(VideoStreamDescriptor videoStream1, VideoStreamDescriptor videoStream2, AudioStreamDescriptor audioStream, TimedMetadataStreamDescriptor timedMetadataStream)
        {
            ContainerEncodingProperties container = new ContainerEncodingProperties()
            {
                Subtype = MediaEncodingSubtypes.Mpeg4
            };

            MediaEncodingProfile profile = new MediaEncodingProfile()
            {
                Container = container
            };


            VideoStreamDescriptor encodingVideoStream1 = videoStream1.Copy();
            encodingVideoStream1.EncodingProperties.Subtype = MediaEncodingSubtypes.H264;
            encodingVideoStream1.Label = videoStream1.Name;

            VideoStreamDescriptor encodingVideoStream2 = videoStream2.Copy();
            encodingVideoStream2.EncodingProperties.Subtype = MediaEncodingSubtypes.H264;
            encodingVideoStream2.Label = videoStream2.Name;

            AudioStreamDescriptor encodingAudioStream = audioStream.Copy();
            encodingAudioStream.EncodingProperties.Subtype = MediaEncodingSubtypes.Ac3;
            encodingAudioStream.Label = audioStream.Name;

            TimedMetadataStreamDescriptor encodingTimedMetadataStream = timedMetadataStream.Copy();

            profile.SetTimedMetadataTracks(new TimedMetadataStreamDescriptor[] { encodingTimedMetadataStream });
            profile.SetVideoTracks(new VideoStreamDescriptor[] { encodingVideoStream1, encodingVideoStream2 });
            profile.SetAudioTracks(new AudioStreamDescriptor[] { encodingAudioStream });
            return profile;
        }
        //</SnippetGetMediaEncodingProfile>

    }
}
