using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.Media.Capture.Frames;
using Windows.Media.MediaProperties;
using Windows.Media.Core;
using System.Diagnostics;

namespace SimpleCameraPreview_Win10
{
    public sealed partial class MainPage : Page
    {
        private async void MultiRecord_Click(object sender, RoutedEventArgs e)
        {
            // <SnippetMultiRecordFindSensorGroups>
            var sensorGroups = await MediaFrameSourceGroup.FindAllAsync();

            var foundGroup = sensorGroups.Select(g => new
            {
                group = g,
                color1 = g.SourceInfos.Where(info => info.SourceKind == MediaFrameSourceKind.Color && info.DeviceInformation.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front).FirstOrDefault(),
                color2 = g.SourceInfos.Where(info => info.SourceKind == MediaFrameSourceKind.Color && info.DeviceInformation.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back).FirstOrDefault()
            }).Where(g => g.color1 != null && g.color2 != null).FirstOrDefault();

            if (foundGroup == null)
            {
                Debug.WriteLine("No groups found.");
                return;
            }
            // </SnippetMultiRecordFindSensorGroups>

            // <SnippetMultiRecordInitMediaCapture>
            var settings = new MediaCaptureInitializationSettings()
            {
                SourceGroup = foundGroup.group
            };

            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync(settings);
            // </SnippetMultiRecordInitMediaCapture>


            // <SnippetMultiRecordMediaEncodingProfile>
            var profile = new MediaEncodingProfile();
            profile.Container = new ContainerEncodingProperties();
            profile.Container.Subtype = MediaEncodingSubtypes.Mpeg4;

            List<VideoStreamDescriptor> streams = new List<VideoStreamDescriptor>();

            var encodeProps = VideoEncodingProperties.CreateH264();
            encodeProps.Subtype = MediaEncodingSubtypes.H264;
            var stream1Desc = new VideoStreamDescriptor(encodeProps);
            stream1Desc.Label = foundGroup.color1.Id;
            streams.Add(stream1Desc);

            var encodeProps2 = VideoEncodingProperties.CreateH264();
            encodeProps2.Subtype = MediaEncodingSubtypes.H264;
            var stream2Desc = new VideoStreamDescriptor(encodeProps2);
            stream2Desc.Label = foundGroup.color2.Id;
            streams.Add(stream2Desc);

            profile.SetVideoTracks(streams);
            profile.Audio = null;
            // </SnippetMultiRecordMediaEncodingProfile>


            Debug.WriteLine("started");
            // <SnippetMultiRecordToFile>
            var recordFile = await Windows.Storage.KnownFolders.CameraRoll.CreateFileAsync("record.mp4", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
            await mediaCapture.StartRecordToStorageFileAsync(profile, recordFile);
            await Task.Delay(8000);
            await mediaCapture.StopRecordAsync();
            // </SnippetMultiRecordToFile>
            Debug.WriteLine("done");
        }

        public TimedMetadataStreamDescriptor CreateStreamDescriptorForGpmdEncodingSubtype()
        {
            //<SnippetGetStreamDescriptor>
            TimedMetadataEncodingProperties encodingProperties = new TimedMetadataEncodingProperties
            {
                Subtype = "{67706D64-BF10-48B4-BC18-593DC1DB950F}"
            };
 
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
