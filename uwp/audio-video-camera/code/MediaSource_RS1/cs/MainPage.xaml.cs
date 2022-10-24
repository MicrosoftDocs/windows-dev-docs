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
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Threading.Tasks;

// <SnippetUsing>
using Windows.Media.Core;
using Windows.Media.Playback;

// </SnippetUsing>

using Windows.Media.Streaming.Adaptive;
using Windows.Networking.BackgroundTransfer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaSource_RS1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // <SnippetDeclareMediaSource>
        MediaSource _mediaSource;
        // </SnippetDeclareMediaSource>

        // <SnippetDeclareMediaPlaybackItem>
        MediaPlaybackItem _mediaPlaybackItem;
        // </SnippetDeclareMediaPlaybackItem>

        MediaSource _mediaSource2;
        MediaPlaybackItem _mediaPlaybackItem2;

        // <SnippetDeclareMediaPlaybackList>
        MediaPlaybackList _mediaPlaybackList;
        // </SnippetDeclareMediaPlaybackList>

        // <SnippetDeclareMediaPlayer>
        MediaPlayer _mediaPlayer;
        // </SnippetDeclareMediaPlayer>

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //PlayMediaPlaybackItemWithCustomTracks();
            //PlayMediaPlaybackList();
            //InitSpeechCueScenario();
        }
        
        #region MediaSource
        private async void PlayMediaSourceButton_Click(object sender, RoutedEventArgs e) => await PlayMediaSource();        

        private async Task PlayMediaSource()
        {
            // <SnippetPlayMediaSource>
            //Create a new picker
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            
            //make a collection of all video types you want to support (for testing we are adding just 3).
            string[] fileTypes = new string[] {".wmv", ".mp4", ".mkv"};   
               
            //Add your fileTypes to the FileTypeFilter list of filePicker.
            foreach (string fileType in fileTypes)
            {
                filePicker.FileTypeFilter.Add(fileType);
            }
            
            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            if (!(file is null))
            {
                _mediaSource = MediaSource.CreateFromStorageFile(file);
                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Source = _mediaSource;
                mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            }
            // </SnippetPlayMediaSource>

            // <SnippetPlay>
            _mediaPlayer.Play();
            // </SnippetPlay>

            // <SnippetAutoPlay>
            _mediaPlayer.AutoPlay = true;
            // </SnippetAutoPlay>
        }
        #endregion

        #region MediaPlaybackItem
        private void PlayMediaPlaybackItemButton_Click(object sender, RoutedEventArgs e) => PlayMediaPlaybackItem();

        private async void PlayMediaPlaybackItem()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //make a collection of all video types you want to support (for testing we are adding just 3).
            string[] fileTypes = new string[] {".wmv", ".mp4", ".mkv"};   
               
            //Add your fileTypes to the FileTypeFilter list of filePicker.
            foreach (string fileType in fileTypes)
            {
                filePicker.FileTypeFilter.Add(fileType);
            }

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            if (!(file is null))
            {
                // <SnippetPlayMediaPlaybackItem>
                _mediaSource = MediaSource.CreateFromStorageFile(file);
                _mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);

                _mediaPlaybackItem.AudioTracksChanged += PlaybackItem_AudioTracksChanged;
                _mediaPlaybackItem.VideoTracksChanged += MediaPlaybackItem_VideoTracksChanged;
                _mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Source = _mediaPlaybackItem;
                mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
                // </SnippetPlayMediaPlaybackItem>
            }
            
            if (!(_mediaPlaybackItem is null))
            {
                var mediaPlaybackItem = _mediaPlaybackItem;
                // <SnippetSetVideoProperties>
                MediaItemDisplayProperties props = mediaPlaybackItem.GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Video;
                props.VideoProperties.Title = "Video title";
                props.VideoProperties.Subtitle = "Video subtitle";
                props.VideoProperties.Genres.Add("Documentary");
                mediaPlaybackItem.ApplyDisplayProperties(props);
                // </SnippetSetVideoProperties>

                // <SnippetSetMusicProperties>
                props = mediaPlaybackItem.GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Music;
                props.MusicProperties.Title = "Song title";
                props.MusicProperties.Artist = "Song artist";
                props.MusicProperties.Genres.Add("Polka");
                mediaPlaybackItem.ApplyDisplayProperties(props);
                // </SnippetSetMusicProperties>
            }
        }


        // <SnippetVideoTracksChanged>
        private async void MediaPlaybackItem_VideoTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                videoTracksComboBox.Items.Clear();
                for (int index = 0; index < sender.VideoTracks.Count; index++)
                {
                    var videoTrack = sender.VideoTracks[index];
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = String.IsNullOrEmpty(videoTrack.Label) ? $"Track {index}" : videoTrack.Label;
                    item.Tag = index;
                    videoTracksComboBox.Items.Add(item);
                }
            });
        }
        // </SnippetVideoTracksChanged>

        // <SnippetVideoTracksSelectionChanged>
        private void videoTracksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int trackIndex = (int)((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            _mediaPlaybackItem.VideoTracks.SelectedIndex = trackIndex;
        }
        // </SnippetVideoTracksSelectionChanged>

        // <SnippetAudioTracksChanged>
        private async void PlaybackItem_AudioTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                audioTracksComboBox.Items.Clear();
                for (int index = 0; index < sender.AudioTracks.Count; index++)
                {
                    var audioTrack = sender.AudioTracks[index];
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = String.IsNullOrEmpty(audioTrack.Label) ? $"Track {index}" : audioTrack.Label;
                    item.Tag = index;
                    videoTracksComboBox.Items.Add(item);
                }
            });
        }
        // </SnippetAudioTracksChanged>

        // <SnippetAudioTracksSelectionChanged>
        private void audioTracksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int trackIndex = (int)((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            _mediaPlaybackItem.AudioTracks.SelectedIndex = trackIndex;
        }
        // </SnippetAudioTracksSelectionChanged>

        // <SnippetTimedMetadataTracksChanged>
        private async void MediaPlaybackItem_TimedMetadataTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                {
                    var timedMetadataTrack = sender.TimedMetadataTracks[index];

                    ToggleButton toggle = new ToggleButton()
                    {
                        Content = String.IsNullOrEmpty(timedMetadataTrack.Label) ? $"Track {index}" : timedMetadataTrack.Label,
                        Tag = (uint)index
                    };
                    toggle.Checked += Toggle_Checked;
                    toggle.Unchecked += Toggle_Unchecked;

                    MetadataButtonPanel.Children.Add(toggle);
                }
            });
        }
        // </SnippetTimedMetadataTracksChanged>

        // <SnippetToggleChecked>
        private void Toggle_Checked(object sender, RoutedEventArgs e) =>         
            _mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
                TimedMetadataTrackPresentationMode.PlatformPresented);
        
        // </SnippetToggleChecked>
        // <SnippetToggleUnchecked>
        private void Toggle_Unchecked(object sender, RoutedEventArgs e) =>         
            _mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
                TimedMetadataTrackPresentationMode.Disabled);
        
        // </SnippetToggleUnchecked>

        // <SnippetAudioTracksChanged_CodecCheck>
        private async void SnippetAudioTracksChanged_CodecCheck(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            if (args.CollectionChange == CollectionChange.ItemInserted)
            {
                var insertedTrack = sender.AudioTracks[(int)args.Index];

                var decoderStatus = insertedTrack.SupportInfo.DecoderStatus;
                if (decoderStatus != MediaDecoderStatus.FullySupported)
                {
                    if (decoderStatus == MediaDecoderStatus.Degraded)
                    {
                        ShowMessageToUser($"Track {insertedTrack.Name} can play but playback will be degraded. {insertedTrack.SupportInfo.DegradationReason}");
                    }
                    else
                    {
                        // status is MediaDecoderStatus.UnsupportedSubtype or MediaDecoderStatus.UnsupportedEncoderProperties
                        ShowMessageToUser($"Track {insertedTrack.Name} uses an unsupported media format.");
                    }

                    Windows.Media.MediaProperties.AudioEncodingProperties props = insertedTrack.GetEncodingProperties();
                    await HelpUserInstallCodec(props);
                }
                else
                {
                    insertedTrack.OpenFailed += InsertedTrack_OpenFailed;
                }
            }

        }
        // </SnippetAudioTracksChanged_CodecCheck>

        // <SnippetOpenFailed>
        private async void InsertedTrack_OpenFailed(AudioTrack sender, AudioTrackOpenFailedEventArgs args)
        {
            LogError(args.ExtendedError.HResult);

            if (sender.SupportInfo.MediaSourceStatus == MediaSourceStatus.Unknown)
            {
                await SelectAnotherTrackOrSkipPlayback(sender.PlaybackItem);
            }
        }
        // </SnippetOpenFailed>


        public async Task HelpUserInstallCodec(Windows.Media.MediaProperties.AudioEncodingProperties props)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // This is fake async so the method can be called async
            });
        }
        public async Task SelectAnotherTrackOrSkipPlayback(MediaPlaybackItem item)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // This is fake async so the method can be called async
            });
        }

        public void LogError(int hresult)
        {

        }
        public void LogError(string error)
        {

        }


        #endregion

        #region timedtextsource
        // <SnippetTimedTextSourceMap>
        Dictionary<TimedTextSource, Uri> timedTextSourceMap;
        // </SnippetTimedTextSourceMap>
        private void AddExternalMetadata()
        {
            // <SnippetTimedTextSource>
            // Create the TimedTextSource and add entry to URI map
            var timedTextSourceUri_En = new Uri("http://contoso.com/MyClipTimedText_en.srt");
            var timedTextSource_En = TimedTextSource.CreateFromUri(timedTextSourceUri_En);
            timedTextSourceMap[timedTextSource_En] = timedTextSourceUri_En;
            timedTextSource_En.Resolved += TimedTextSource_Resolved;

            var timedTextSourceUri_Pt = new Uri("http://contoso.com/MyClipTimedText_pt.srt");
            var timedTextSource_Pt = TimedTextSource.CreateFromUri(timedTextSourceUri_Pt);
            timedTextSourceMap[timedTextSource_Pt] = timedTextSourceUri_Pt;
            timedTextSource_Pt.Resolved += TimedTextSource_Resolved;

            // Add the TimedTextSource to the MediaSource
            _mediaSource.ExternalTimedTextSources.Add(timedTextSource_En);
            _mediaSource.ExternalTimedTextSources.Add(timedTextSource_Pt);

            _mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);
            _mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = _mediaPlaybackItem;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);

            // </SnippetTimedTextSource>
        }
        // <SnippetTimedTextSourceResolved>
        private void TimedTextSource_Resolved(TimedTextSource sender, TimedTextSourceResolveResultEventArgs args)
        {
            var timedTextSourceUri = timedTextSourceMap[sender];

            if (!(args.Error is null))
            {
                // Show that there was an error in your UI
                ShowMessageToUser($"There was an error resolving track: {timedTextSourceUri}");
                return;
            }

            // Add a label for each resolved track
            var timedTextSourceUriString = timedTextSourceUri.AbsoluteUri;
            if (timedTextSourceUriString.Contains("_en"))
            {
                args.Tracks[0].Label = "English";
            }
            else if (timedTextSourceUriString.Contains("_pt"))
            {
                args.Tracks[0].Label = "Portuguese";
            }
        }
        // </SnippetTimedTextSourceResolved>
        #endregion

        #region custommetadata
        private void PlayMediaPlaybackItemWithCustomTracks_Click(object sender, RoutedEventArgs e) => PlayMediaPlaybackItemWithCustomTracks();
        
        private async void PlayMediaPlaybackItemWithCustomTracks()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //make a collection of all video types you want to support (for testing we are adding just 3).
            string[] fileTypes = new string[] {".wmv", ".mp4", ".mkv"};   
               
            //Add your fileTypes to the FileTypeFilter list of filePicker.
            foreach (string fileType in fileTypes)
            {
                filePicker.FileTypeFilter.Add(fileType);
            }

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            if(!(file is null))
            { 
                _mediaSource = MediaSource.CreateFromStorageFile(file);
                _mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);

                _mediaPlaybackItem.AudioTracksChanged += PlaybackItem_AudioTracksChanged;
                _mediaPlaybackItem.VideoTracksChanged += MediaPlaybackItem_VideoTracksChanged;
                _mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

                AddTimedMetaDataTrack_Data();
                AddTimedMetaDataTrack_Text();

                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Source = _mediaPlaybackItem;
                mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            }
        }

        private void AddTimedMetaDataTrack_Data()
        {
            // <SnippetAddDataTrack>
            TimedMetadataTrack metadataTrack = new TimedMetadataTrack("ID_0", "en-us", TimedMetadataKind.Data);
            metadataTrack.Label = "Custom data track";
            metadataTrack.CueEntered += MetadataTrack_DataCueEntered;
            metadataTrack.CueExited += MetadataTrack_CueExited;

            // Example cue data
            string data = "Cue data";
            byte[] bytes = new byte[data.Length * sizeof(char)];
            System.Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);
            Windows.Storage.Streams.IBuffer buffer = bytes.AsBuffer();

            for (int i = 0; i < 10; i++)
            {
                DataCue cue = new DataCue();
                cue.Id = "ID_" + i;
                cue.Data = buffer;
                cue.Properties["AdUrl"] = "http://contoso.com/ads/123";
                cue.StartTime = TimeSpan.FromSeconds(3 + i * 3);
                cue.Duration = TimeSpan.FromSeconds(2);

                metadataTrack.AddCue(cue);
            }

            _mediaSource.ExternalTimedMetadataTracks.Add(metadataTrack);
            // </SnippetAddDataTrack>
        }

        private void MetadataTrack_CueExited(TimedMetadataTrack sender, MediaCueEventArgs args)
        {
        }

        // <SnippetDataCueEntered>
        private void MetadataTrack_DataCueEntered(TimedMetadataTrack sender, MediaCueEventArgs args)
        {
            DataCue cue = (DataCue)args.Cue;
            string data = System.Text.Encoding.Unicode.GetString(cue.Data.ToArray());
            System.Diagnostics.Debug.WriteLine("Cue entered: " + data);
            System.Diagnostics.Debug.WriteLine("Custom prop value: " + cue.Properties["AdUrl"]);
        }
        // </SnippetDataCueEntered>

        private void AddTimedMetaDataTrack_Text()
        {
            // <SnippetAddTextTrack>
            TimedMetadataTrack metadataTrack = new TimedMetadataTrack("TrackID_0", "en-us", TimedMetadataKind.Caption);
            metadataTrack.Label = "Custom text track";
            metadataTrack.CueEntered += MetadataTrack_TextCueEntered;

            for (int i = 0; i < 10; i++)
            {
                TimedTextCue cue = new TimedTextCue()
                {
                    Id = "TextCueID_" + i,
                    StartTime = TimeSpan.FromSeconds(i * 3),
                    Duration = TimeSpan.FromSeconds(2)
                };

                cue.Lines.Add(new TimedTextLine() { Text = "This is a custom timed text cue." });
                metadataTrack.AddCue(cue);
            }

            _mediaSource.ExternalTimedMetadataTracks.Add(metadataTrack);
            // </SnippetAddTextTrack>
        }

        // <SnippetTextCueEntered>
        private void MetadataTrack_TextCueEntered(TimedMetadataTrack sender, MediaCueEventArgs args)
        {
            TimedTextCue cue = (TimedTextCue)args.Cue;
            System.Diagnostics.Debug.WriteLine("Cue entered: " + cue.Id + " " + cue.Lines[0].Text);
        }
        // </SnippetTextCueEntered>


        #endregion

        #region MediaPlaybackList

        private void MediaPlaybackListButton_Click(object sender, RoutedEventArgs e) => PlayMediaPlaybackList();        

        private async void PlayMediaPlaybackList()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //make a collection of all video types you want to support (for testing we are adding just 4).
            string[] fileTypes = new string[] {".wmv", ".mp4", ".mkv", ".mp3"};   
               
            //Add your fileTypes to the FileTypeFilter list of filePicker.
            foreach (string fileType in fileTypes)
            {
                filePicker.FileTypeFilter.Add(fileType);
            }

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            // <SnippetPlayMediaPlaybackList>
            _mediaPlaybackList = new MediaPlaybackList();

            var files = await filePicker.PickMultipleFilesAsync();

            foreach (var file in files)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
                _mediaPlaybackList.Items.Add(mediaPlaybackItem);
            }

            _mediaPlaybackList.CurrentItemChanged += MediaPlaybackList_CurrentItemChanged;
            _mediaPlaybackList.ItemOpened += MediaPlaybackList_ItemOpened;
            _mediaPlaybackList.ItemFailed += MediaPlaybackList_ItemFailed;

            _mediaPlaybackList.MaxPlayedItemsToKeepOpen = 3;

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = _mediaPlaybackList;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            // </SnippetPlayMediaPlaybackList>

            for (int i = 0; i < _mediaPlaybackList.Items.Count; i++)
            {

                var props = _mediaPlaybackList.Items[i].GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Music;
                props.MusicProperties.Title = $"Track title {i}";
                props.MusicProperties.Artist = $"Track artist {i}";
                _mediaPlaybackList.Items[i].ApplyDisplayProperties(props);
            }
        }

        // <SnippetDeclareItemQueue>
        Queue<MediaPlaybackItem> _playbackItemQueue = new Queue<MediaPlaybackItem>();
        int maxCachedItems = 3;
        // </SnippetDeclareItemQueue>

        // <SnippetMediaPlaybackListItemChanged>
        private void MediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args) => 
            LogTelemetryData($"CurrentItemChanged reason: {args.Reason.ToString()}");
        
        // </SnippetMediaPlaybackListItemChanged>

        // <SnippetPrevButton>
        private void prevButton_Click(object sender, RoutedEventArgs e) =>  _mediaPlaybackList.MovePrevious();        
        // </SnippetPrevButton>

        // <SnippetNextButton>
        private void nextButton_Click(object sender, RoutedEventArgs e) => _mediaPlaybackList.MoveNext();
        // </SnippetNextButton>

        // <SnippetShuffleButton>
        private async void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlaybackList.ShuffleEnabled = !_mediaPlaybackList.ShuffleEnabled;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                shuffleButton.FontWeight =
                    _mediaPlaybackList.ShuffleEnabled ? Windows.UI.Text.FontWeights.Bold : Windows.UI.Text.FontWeights.Light;
            });
        }
        // </SnippetShuffleButton>

        // <SnippetRepeatButton>
        private async void autoRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlaybackList.AutoRepeatEnabled = !_mediaPlaybackList.AutoRepeatEnabled;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                autoRepeatButton.FontWeight =
                    _mediaPlaybackList.AutoRepeatEnabled ? Windows.UI.Text.FontWeights.Bold : Windows.UI.Text.FontWeights.Light;
            });
        }
        // </SnippetRepeatButton>
        
        // <SnippetItemOpened>
        private void MediaPlaybackList_ItemOpened(MediaPlaybackList sender, MediaPlaybackItemOpenedEventArgs args)
        {
        }
        // </SnippetItemOpened>

        // <SnippetItemFailed>
        private void MediaPlaybackList_ItemFailed(MediaPlaybackList sender, MediaPlaybackItemFailedEventArgs args)
        {
            LogError(args.Error.ErrorCode.ToString());
            LogError(args.Error.ExtendedError.HResult);
        }
        // </SnippetItemFailed>

        private void DisablePlaybackForItemsButton_Click(object sender, RoutedEventArgs e)
        {
            // <SnippetRegisterNetworkStatusChanged>
            Windows.Networking.Connectivity.NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            // </SnippetRegisterNetworkStatusChanged>
        }
        // <SnippetNetworkStatusChanged>
        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            if (Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile() == null)
            {
                // Check download status of each item in the list. (TotalDownloadProgress < 1 means not completely downloaded)
                foreach (var item in _mediaPlaybackList.Items)
                {
                    if (item.TotalDownloadProgress < 1)
                    {
                        item.IsDisabledInPlaybackList = true;
                    }
                }
            }
            else
            {
                // Connected to internet, re-enable all playlist items
                foreach (var item in _mediaPlaybackList.Items)
                {
                    item.IsDisabledInPlaybackList = true;
                }
            }
        }
        // </SnippetNetworkStatusChanged>
        #endregion

        #region MediaBinder

        private void MediaBinderButton_Click(object sender, RoutedEventArgs e)
        {
            InitMediaBinder_StorageFile();
        }
        private void InitMediaBinder()
        {
            // <SnippetInitMediaBinder>
            _mediaPlaybackList = new MediaPlaybackList();

            var binder = new MediaBinder();
            binder.Token = "MyBindingToken1";
            binder.Binding += Binder_Binding;
            _mediaPlaybackList.Items.Add(new MediaPlaybackItem(MediaSource.CreateFromMediaBinder(binder)));

            binder = new MediaBinder();
            binder.Token = "MyBindingToken2";
            binder.Binding += Binder_Binding;
            _mediaPlaybackList.Items.Add(new MediaPlaybackItem(MediaSource.CreateFromMediaBinder(binder)));

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = _mediaPlaybackList;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            // </SnippetInitMediaBinder>
        }
        // <SnippetBinderBinding>
        private void Binder_Binding(MediaBinder sender, MediaBindingEventArgs args)
        {
            // Get a deferral if you need to perform async operations
            // var deferral = args.GetDeferral();

            var contentUri = new Uri("http://contoso.com/media/" + args.MediaBinder.Token);
            args.SetUri(contentUri);

            // Call complete after your async operations are complete
            // deferral.Complete();
        }
        // </SnippetBinderBinding>

        IReadOnlyList<StorageFile> binderFiles;

        private async void InitMediaBinder_StorageFile()
        {
            var filePicker = new FileOpenPicker();

            //make a collection of all video types you want to support (for testing we are adding just 4).
            string[] fileTypes = new string[] {".wmv", ".mp4", ".mkv", ".mp3"};   
               
            //Add your fileTypes to the FileTypeFilter list of filePicker.
            foreach (string fileType in fileTypes)
            {
                filePicker.FileTypeFilter.Add(fileType);
            }

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            _mediaPlaybackList = new MediaPlaybackList();

            binderFiles = await filePicker.PickMultipleFilesAsync();

            _mediaPlaybackList = new MediaPlaybackList();

            var binder = new MediaBinder();
            binder.Token = "MyBindingToken1";
            binder.Binding += Binder_Binding_StorageFile;
            _mediaPlaybackList.Items.Add(new MediaPlaybackItem(MediaSource.CreateFromMediaBinder(binder)));

            binder = new MediaBinder();
            binder.Token = "MyBindingToken2";
            binder.Binding += Binder_Binding_StorageFile;
            _mediaPlaybackList.Items.Add(new MediaPlaybackItem(MediaSource.CreateFromMediaBinder(binder)));

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = _mediaPlaybackList;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            mediaPlayerElement.MediaPlayer.Play();
        }

        private void Binder_Binding_StorageFile(MediaBinder sender, MediaBindingEventArgs args)
        {
            switch (args.MediaBinder.Token)
            {
                case "MyBindingToken1":
                    args.SetStorageFile(binderFiles[0]);
                    break;
                case "MyBindingToken2":
                    args.SetStorageFile(binderFiles[1]);
                    break;
            }
        }

        // <SnippetBinderBindingAMS>
        private async void Binder_Binding_AdaptiveMediaSource(MediaBinder sender, MediaBindingEventArgs args)
        {
            var deferral = args.GetDeferral();

            var contentUri = new Uri($"http://contoso.com/media/{args.MediaBinder.Token}");
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(contentUri);

            if (result.MediaSource != null)
            {
                args.SetAdaptiveMediaSource(result.MediaSource);
            }
            args.SetUri(contentUri);

            deferral.Complete();
        }
        // </SnippetBinderBindingAMS>
        // <SnippetAMSBindingCurrentItemChanged>
        private void AMSMediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            if (!(args.NewItem is null))
            {
                var ams = args.NewItem.Source.AdaptiveMediaSource;
                if (!(ams is null))
                {
                    ams.PlaybackBitrateChanged += Ams_PlaybackBitrateChanged;
                }
            }
        }
        // </SnippetAMSBindingCurrentItemChanged>

        private void Ams_PlaybackBitrateChanged(AdaptiveMediaSource sender, AdaptiveMediaSourcePlaybackBitrateChangedEventArgs args) => throw new NotImplementedException();
        #endregion

        #region DownloadOperation

        private async void MediaSourceDownloadOperation()
        {
            //<SnippetCreateMediaSourceFromDownload>
            StorageFile destinationFile = await KnownFolders.VideosLibrary.CreateFileAsync("file.mp4", CreationCollisionOption.GenerateUniqueName);

            var downloader = new BackgroundDownloader();
            var downloadOperation = downloader.CreateDownload(new Uri("http://server.com/file.mp4"), destinationFile);
            MediaSource mediaSource =
                  MediaSource.CreateFromDownloadOperation(downloadOperation);
            //</SnippetCreateMediaSourceFromDownload>
            //<SnippetStartDownload>
            downloadOperation.IsRandomAccessRequired = true;
            var startAsyncTask = downloadOperation.StartAsync().AsTask();
            mediaPlayerElement.Source = mediaSource;
            //</SnippetStartDownload>

        }

        #endregion

        private void ShowMessageToUser(string s) { }
        private void LogTelemetryData(string s) { }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(_mediaPlayer is null))
            {
                _mediaPlayer.Play();
            }
        }
    }
}
