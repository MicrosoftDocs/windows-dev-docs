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
            //<SnippetMultiRecordFindSensorGroups>
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
            //</SnippetMultiRecordFindSensorGroups>

            //<SnippetMultiRecordInitMediaCapture>
            var settings = new MediaCaptureInitializationSettings()
            {
                SourceGroup = foundGroup.group
            };

            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync(settings);
            //</SnippetMultiRecordInitMediaCapture>


            //<SnippetMultiRecordMediaEncodingProfile>
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
            //</SnippetMultiRecordMediaEncodingProfile>


            Debug.WriteLine("started");
            //<SnippetMultiRecordToFile>
            var recordFile = await Windows.Storage.KnownFolders.CameraRoll.CreateFileAsync("record.mp4", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
            await mediaCapture.StartRecordToStorageFileAsync(profile, recordFile);
            await Task.Delay(8000);
            await mediaCapture.StopRecordAsync();
            //</SnippetMultiRecordToFile>
            Debug.WriteLine("done");
        }
    }
}
