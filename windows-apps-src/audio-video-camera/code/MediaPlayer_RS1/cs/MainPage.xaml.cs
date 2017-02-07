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

using Windows.Storage.Pickers;


using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Media.Devices;
using Windows.Devices.Enumeration;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using Windows.Media;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaPlayer_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer _mediaPlayer;


        public MainPage()
        {
            this.InitializeComponent();

            
        }

        private async void ActionButton1_Click(object sender, RoutedEventArgs e)
        {
            await SimpleFilePlaybackPicker();
            SimpleFilePlayback();
            BindPlayerToElement();
            //GetPlayerFromElement();
            //await SetAudioEndpoint();  
            //RegisterGestureHandler();
            //SetVideoVisual();
            SetTimelineController();
            GetMediaSourceLength();
           
            
            if (_mediaPlayer != null) _mediaPlayer.IsMuted = true;
            if (_mediaPlayer2 != null) _mediaPlayer2.IsMuted = true;
        }
        private async void ActionButton2_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
        public async Task SimpleFilePlaybackPicker()
        {
            //Create a new picker
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.FileTypeFilter.Add(".mkv");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            if (file != null)
            {
                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Source = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
                _mediaPlayer.Play();
            }
        }
        private void SimpleFilePlayback()
        {
            //<SnippetSimpleFilePlayback>
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/example_video.mkv"));
            _mediaPlayer.Play();
            //</SnippetSimpleFilePlayback>
        }

        private void Close()
        {
            //<SnippetCloseMediaPlayer>
            _mediaPlayer.Dispose();
            //</SnippetCloseMediaPlayer>
        }

        private void BindPlayerToElement()
        {
            //<SnippetSetMediaPlayer>
            _mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            //</SnippetSetMediaPlayer>
        }
        private void GetPlayerFromElement()
        {
            //<SnippetGetPlayerFromElement>
            _mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/example_video.mkv"));
            _mediaPlayer = _mediaPlayerElement.MediaPlayer;
            _mediaPlayer.Play();
            //</SnippetGetPlayerFromElement>
        }


        private async Task SetAudioEndpoint()
        {
            //<SnippetSetAudioEndpointEnumerate>
            string audioSelector = MediaDevice.GetAudioRenderSelector();
            var outputDevices = await DeviceInformation.FindAllAsync(audioSelector);
            foreach(var device in outputDevices)
            {
                var deviceItem = new ComboBoxItem();
                deviceItem.Content = device.Name;
                deviceItem.Tag = device;
                _audioDeviceComboBox.Items.Add(deviceItem);
            }
            //</SnippetSetAudioEndpointEnumerate>

        }
        //<SnippetSetAudioEndpontSelectionChanged>
        private void _audioDeviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceInformation selectedDevice = (DeviceInformation)((ComboBoxItem)_audioDeviceComboBox.SelectedItem).Tag;
            if (selectedDevice != null)
            {
                _mediaPlayer.AudioDevice = selectedDevice;
            }
        }
        //</SnippetSetAudioEndpontSelectionChanged>

        private void SetAudioCategory()
        {
            //<SnippetSetAudioCategory>
            _mediaPlayer.AudioCategory = MediaPlayerAudioCategory.Media;
            //</SnippetSetAudioCategory>
        }
        private void VariousMediaPlayerFeatures()
        {
            _mediaPlayer.SourceChanged += _mediaPlayer_SourceChanged;
        }
        MediaSource _lastSource;
        private void _mediaPlayer_SourceChanged(MediaPlayer sender, object args)
        {
            if(_lastSource != null)
            {
                _lastSource.OpenOperationCompleted -= MediaSource_OpenOperationCompleted;
            }

            _lastSource = ((MediaSource)sender.Source);
            _lastSource.OpenOperationCompleted += MediaSource_OpenOperationCompleted;
        }

        

        //<SnippetSkipForwardClick>
        private void _skipForwardButton_Click(object sender, RoutedEventArgs e)
        {
            var session = _mediaPlayer.PlaybackSession;
            session.Position = session.Position + TimeSpan.FromSeconds(10);
        }
        //</SnippetSkipForwardClick>

        //<SnippetSpeedChecked>
        private void _speedToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.PlaybackSession.PlaybackRate = 2.0;
        }
        private void _speedToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.PlaybackSession.PlaybackRate = 1.0;
        }
        //</SnippetSpeedChecked>

        private void RegisterGestureHandler()
        {
            //<SnippetRegisterPinchZoomEvents>
            _mediaPlayerElement.ManipulationMode = ManipulationModes.Scale | ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _mediaPlayerElement.ManipulationDelta += _mediaPlayerElement_ManipulationDelta;
            _mediaPlayerElement.DoubleTapped += _mediaPlayerElement_DoubleTapped;
            //</SnippetRegisterPinchZoomEvents>
        }


        //<SnippetDeclareSourceRect>
        Rect _sourceRect = new Rect(0, 0, 1, 1);
        //</SnippetDeclareSourceRect>

        //<SnippetManipulationDelta>
        private void _mediaPlayerElement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

            if (e.Delta.Scale != 1)
            {
                var halfWidth = _sourceRect.Width / 2;
                var halfHeight = _sourceRect.Height / 2;

                var centerX = _sourceRect.X + halfWidth;
                var centerY = _sourceRect.Y + halfHeight;

                var scale = e.Delta.Scale;
                var newHalfWidth = (_sourceRect.Width * e.Delta.Scale) / 2;
                var newHalfHeight = (_sourceRect.Height * e.Delta.Scale) / 2;

                if (centerX - newHalfWidth > 0 && centerX + newHalfWidth <= 1.0 &&
                    centerY - newHalfHeight > 0 && centerY + newHalfHeight <= 1.0)
                {
                    _sourceRect.X = centerX - newHalfWidth;
                    _sourceRect.Y = centerY - newHalfHeight;
                    _sourceRect.Width *= e.Delta.Scale;
                    _sourceRect.Height *= e.Delta.Scale;
                }
            }
            else
            {
                var translateX = -1 * e.Delta.Translation.X / _mediaPlayerElement.ActualWidth;
                var translateY = -1 * e.Delta.Translation.Y / _mediaPlayerElement.ActualHeight;

                if (_sourceRect.X + translateX >= 0 && _sourceRect.X + _sourceRect.Width + translateX <= 1.0 &&
                    _sourceRect.Y + translateY >= 0 && _sourceRect.Y + _sourceRect.Height + translateY <= 1.0)
                {
                    _sourceRect.X += translateX;
                    _sourceRect.Y += translateY;
                }
            }

            _mediaPlayer.PlaybackSession.NormalizedSourceRect = _sourceRect;
        }
        //</SnippetManipulationDelta>
        //<SnippetDoubleTapped>
        private void _mediaPlayerElement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            _sourceRect = new Rect(0, 0, 1, 1);
            _mediaPlayer.PlaybackSession.NormalizedSourceRect = _sourceRect;
        }
        //</SnippetDoubleTapped>


        //private void SetVideoVisual()
        //{
        //    //<SnippetCompositor>
        //    Visual elementVisual = ElementCompositionPreview.GetElementVisual(_compositionCanvas);
        //    var compositor = elementVisual.Compositor;

        //    _mediaPlayer.SetSurfaceSize(new Size(_compositionCanvas.ActualWidth, _compositionCanvas.ActualHeight));
        //    MediaPlayerSurface surface = _mediaPlayer.GetSurface(compositor);

        //    SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
        //    spriteVisual.Brush = compositor.CreateSurfaceBrush(surface.CompositionSurface);

        //    ElementCompositionPreview.SetElementChildVisual(_compositionCanvas, spriteVisual);
        //    //</SnippetCompositor>
        //}

        private void SetVideoVisual()
        {
            //<SnippetCompositor>
            _mediaPlayer.SetSurfaceSize(new Size(_compositionCanvas.ActualWidth, _compositionCanvas.ActualHeight));

            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            MediaPlayerSurface surface = _mediaPlayer.GetSurface(compositor);
            
            SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Size = 
                new System.Numerics.Vector2((float)_compositionCanvas.ActualWidth, (float)_compositionCanvas.ActualHeight);

            CompositionBrush brush = compositor.CreateSurfaceBrush(surface.CompositionSurface);
            spriteVisual.Brush = brush;

            ContainerVisual container = compositor.CreateContainerVisual();
            container.Children.InsertAtTop(spriteVisual);

            ElementCompositionPreview.SetElementChildVisual(_compositionCanvas, container);
            //</SnippetCompositor>
        }

        MediaPlayer _mediaPlayer2;
        
        //<SnippetDeclareMediaTimelineController>
        MediaTimelineController _mediaTimelineController;
        //</SnippetDeclareMediaTimelineController>

        public void SetTimelineController()
        {
            //<SnippetSetTimelineController>
            _mediaPlayer = new MediaPlayer();      
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/example_video.mkv"));
            _mediaPlayerElement.SetMediaPlayer(_mediaPlayer);


            _mediaPlayer2 = new MediaPlayer();
            _mediaPlayer2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/example_video_2.mkv"));
            _mediaPlayerElement2.SetMediaPlayer(_mediaPlayer2);

            _mediaTimelineController = new MediaTimelineController();

            _mediaPlayer.CommandManager.IsEnabled = false;
            _mediaPlayer.TimelineController = _mediaTimelineController;

            _mediaPlayer2.CommandManager.IsEnabled = false;
            _mediaPlayer2.TimelineController = _mediaTimelineController;
            //</SnippetSetTimelineController>
        }
        //<SnippetPlayButtonClick>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaTimelineController.Start();
        }
        //</SnippetPlayButtonClick>

        //<SnippetPauseButtonClick>
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if(_mediaTimelineController.State == MediaTimelineControllerState.Running)
            {
                _mediaTimelineController.Pause();
                _pauseButton.Content = "Resume";
            }
            else
            {
                _mediaTimelineController.Resume();
                _pauseButton.Content = "Pause";
            }
        }
        //</SnippetPauseButtonClick>

        //<SnippetFastForwardButtonClick>
        private void FastForwardButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaTimelineController.ClockRate = 2.0;
        }
        //</SnippetFastForwardButtonClick>

        //<SnippetRewindButtonClick>
        private void RewindButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaTimelineController.ClockRate = -.5;
        }
        //</SnippetRewindButtonClick>

        

        
        private void GetMediaSourceLength()
        {
            //<SnippetCreateSourceWithOpenCompleted>
            var mediaSource = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/example_video.mkv"));
            mediaSource.OpenOperationCompleted += MediaSource_OpenOperationCompleted;
            _mediaPlayer.Source = mediaSource;
            _mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            //</SnippetCreateSourceWithOpenCompleted>

            //<SnippetRegisterPositionChanged>
            _mediaTimelineController.PositionChanged += _mediaTimelineController_PositionChanged;
            //</SnippetRegisterPositionChanged>

            //<SnippetRegisterStateChanged>
            _mediaTimelineController.StateChanged += _mediaTimelineController_StateChanged;
            //</SnippetRegisterStateChanged>

            // Do not include in snippet.
            mediaSource = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/example_video.mkv"));
            mediaSource.OpenOperationCompleted += MediaSource_OpenOperationCompleted1;
            _mediaPlayer.Source = mediaSource;
        }

        

        //<SnippetDeclareDuration>
        TimeSpan _duration;
        //</SnippetDeclareDuration>

        //<SnippetOpenCompleted>
        private async void MediaSource_OpenOperationCompleted(MediaSource sender, MediaSourceOpenOperationCompletedEventArgs args)
        {
            _duration = sender.Duration.GetValueOrDefault();

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                _positionSlider.Minimum = 0;
                _positionSlider.Maximum = _duration.TotalSeconds;
                _positionSlider.StepFrequency = 1;
            }); 
        }
        //</SnippetOpenCompleted>

        
        //<SnippetPositionChanged>
        private async void _mediaTimelineController_PositionChanged(MediaTimelineController sender, object args)
        {
            if (_duration != TimeSpan.Zero)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    _positionSlider.Value = sender.Position.TotalSeconds / (float)_duration.TotalSeconds;
                });
            }
        }
        //</SnippetPositionChanged>

        private void _positionSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            return;
            _mediaTimelineController.PositionChanged -= _mediaTimelineController_PositionChanged;
            _mediaTimelineController.Position = TimeSpan.FromSeconds(_positionSlider.Value * _duration.TotalSeconds);
            _mediaTimelineController.PositionChanged += _mediaTimelineController_PositionChanged;
        }

        TimeSpan _duration2;
        private async void MediaSource_OpenOperationCompleted1(MediaSource sender, MediaSourceOpenOperationCompletedEventArgs args)
        {
            _duration = sender.Duration.GetValueOrDefault();
            _duration2 = sender.Duration.GetValueOrDefault();

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //<SnippetOffsetSliders>
                _timelineOffsetSlider1.Minimum = -1 * _duration.TotalSeconds;
                _timelineOffsetSlider1.Maximum = _duration.TotalSeconds;
                _timelineOffsetSlider1.StepFrequency = 1;

                _timelineOffsetSlider2.Minimum = -1 * _duration2.TotalSeconds;
                _timelineOffsetSlider2.Maximum = _duration2.TotalSeconds;
                _timelineOffsetSlider2.StepFrequency = 1;
                //</SnippetOffsetSliders>
            });
        }

        //<SnippetTimelineOffset>
        private void _timelineOffsetSlider1_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _mediaPlayer.TimelineControllerPositionOffset = TimeSpan.FromSeconds(_timelineOffsetSlider1.Value);
        }

        private void _timelineOffsetSlider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _mediaPlayer2.TimelineControllerPositionOffset = TimeSpan.FromSeconds(_timelineOffsetSlider2.Value);
        }
        //</SnippetTimelineOffset>


        //<SnippetStateChanged>
        private void _mediaTimelineController_StateChanged(MediaTimelineController sender, object args)
        {

        }
        //</SnippetStateChanged>
    }

}

