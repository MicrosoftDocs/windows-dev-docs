using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.Media.Capture;
using Windows.UI.Xaml;

// <SnippetVideoStabilizationEffectUsing>
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Media.Effects;
using Windows.Media;
// </SnippetVideoStabilizationEffectUsing>

namespace SimpleCameraPreview_Win10
{
    public sealed partial class MainPage : Page
    {
        public async void InitializeMediaCaptureWithExclusiveControl()
        {
            // <SnippetInitMediaCaptureWithExclusiveControl>
            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
            settings.SharingMode = MediaCaptureSharingMode.ExclusiveControl;

            mediaCapture = new MediaCapture();

            try
            {
                await mediaCapture.InitializeAsync(settings);
            }
            catch (UnauthorizedAccessException)
            {
                // This will be thrown if the user denied access to the camera in privacy settings
                System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
            }

            // </SnippetInitMediaCapture>
        }

        private void RegisterForExclusiveControlEvent()
        {
            mediaCapture.CaptureDeviceExclusiveControlStatusChanged += MediaCapture_ExclusiveControlStatusChanged;
        }

        private void MediaCapture_ExclusiveControlStatusChanged(MediaCapture sender, MediaCaptureDeviceExclusiveControlStatusChangedEventArgs args)
        {
            switch(args.Status)
            {
                case MediaCaptureDeviceExclusiveControlStatus.ExclusiveControlAvailable:
                    InitializeMediaCaptureWithExclusiveControl();
                    break;
                case MediaCaptureDeviceExclusiveControlStatus.SharedReadOnlyAvailable:
                    ShowMessageToUser("The app can use the camera, but can't currently update the camera settings.");
                    break;
            }
        }


    }
}
