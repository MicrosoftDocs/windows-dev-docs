

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

using Windows.Storage;
using Windows.Storage.Pickers;

//<SnippetUsing>
using Windows.Media.Core;
using Windows.Media.Playback;
//</SnippetUsing>


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaSource_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetDeclareMediaSource>
        MediaSource mediaSource;
        //</SnippetDeclareMediaSource>

        //<SnippetDeclareMediaPlaybackItem>
        MediaPlaybackItem mediaPlaybackItem;
        //</SnippetDeclareMediaPlaybackItem>

        MediaSource mediaSource2;
        MediaPlaybackItem mediaPlaybackItem2;

        //<SnippetDeclareMediaPlaybackList>
        MediaPlaybackList mediaPlaybackList;
        //</SnippetDeclareMediaPlaybackList>

        public MainPage()
        {
            this.InitializeComponent();

            //PlayMediaSource();
            //PlayMediaPlaybackItem();
            //PlayMediaPlaybackItemWithCustomTracks();
            PlayMediaPlaybackList();
        }
        #region MediaSource
        private async void PlayMediaSource()
        {
            //<SnippetPlayMediaSource>
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
                mediaSource = MediaSource.CreateFromStorageFile(file);
                mediaElement.SetPlaybackSource(mediaSource);
            }
            //</SnippetPlayMediaSource>
        }
        #endregion

        #region MediaPlaybackItem
        private async void PlayMediaPlaybackItem()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.FileTypeFilter.Add(".mkv");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            //<SnippetPlayMediaPlaybackItem>
            mediaSource = MediaSource.CreateFromStorageFile(file);
            mediaPlaybackItem = new MediaPlaybackItem(mediaSource);

            mediaPlaybackItem.AudioTracksChanged += PlaybackItem_AudioTracksChanged;
            mediaPlaybackItem.VideoTracksChanged += MediaPlaybackItem_VideoTracksChanged;
            mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

            mediaElement.SetPlaybackSource(mediaPlaybackItem);
            //</SnippetPlayMediaPlaybackItem>
        }
       

        //<SnippetVideoTracksChanged>
        private async void MediaPlaybackItem_VideoTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                videoTracksComboBox.Items.Clear();
                for (int index = 0; index < sender.VideoTracks.Count; index++)
                {
                    var videoTrack = sender.VideoTracks[index];
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = String.IsNullOrEmpty(videoTrack.Label) ? "Track " + index : videoTrack.Label;
                    item.Tag = index;
                    videoTracksComboBox.Items.Add(item);
                }
            });
        }
        //</SnippetVideoTracksChanged>

        //<SnippetVideoTracksSelectionChanged>
        private void videoTracksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int trackIndex = (int)((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            mediaPlaybackItem.VideoTracks.SelectedIndex = trackIndex;
        }
        //</SnippetVideoTracksSelectionChanged>

        //<SnippetAudioTracksChanged>
        private async void PlaybackItem_AudioTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                audioTracksComboBox.Items.Clear();
                for (int index = 0; index < sender.AudioTracks.Count; index++)
                {
                    var audioTrack = sender.AudioTracks[index];
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = String.IsNullOrEmpty(audioTrack.Label) ? "Track " + index : audioTrack.Label;
                    item.Tag = index;
                    audioTracksComboBox.Items.Add(item);
                }
            });
        }
        //</SnippetAudioTracksChanged>

        //<SnippetAudioTracksSelectionChanged>
        private void audioTracksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int trackIndex = (int)((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            mediaPlaybackItem.AudioTracks.SelectedIndex = trackIndex;
        }
        //</SnippetAudioTracksSelectionChanged>

        //<SnippetTimedMetadataTracksChanged>
        private async void MediaPlaybackItem_TimedMetadataTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                {
                    var timedMetadataTrack = sender.TimedMetadataTracks[index];

                    ToggleButton toggle = new ToggleButton()
                    {
                        Content = String.IsNullOrEmpty(timedMetadataTrack.Label) ? "Track " + index : timedMetadataTrack.Label,
                        Tag = (uint)index
                    };
                    toggle.Checked += Toggle_Checked;
                    toggle.Unchecked += Toggle_Unchecked;
                   
                    MetadataButtonPanel.Children.Add(toggle);
                }
            });
        }
        //</SnippetTimedMetadataTracksChanged>

        //<SnippetToggleChecked>
        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
                TimedMetadataTrackPresentationMode.PlatformPresented);
        }
        //</SnippetToggleChecked>
        //<SnippetToggleUnchecked>
        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
                TimedMetadataTrackPresentationMode.Disabled);
        }
        //</SnippetToggleUnchecked>






        #endregion

        #region timedtextsource
        //<SnippetTimedTextSourceMap>
        Dictionary<TimedTextSource, Uri> timedTextSourceMap;
        //</SnippetTimedTextSourceMap>
        private void AddExternalMetadata()
        {
            //<SnippetTimedTextSource>
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
            mediaSource.ExternalTimedTextSources.Add(timedTextSource_En);
            mediaSource.ExternalTimedTextSources.Add(timedTextSource_Pt);

            mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

            mediaElement.SetPlaybackSource(mediaPlaybackItem);
            //</SnippetTimedTextSource>
        }
        //<SnippetTimedTextSourceResolved>
        private void TimedTextSource_Resolved(TimedTextSource sender, TimedTextSourceResolveResultEventArgs args)
        {
            var timedTextSourceUri = timedTextSourceMap[sender];

            if(args.Error != null)
            {
                // Show that there was an error in your UI
                ShowMessageToUser("There was an error resolving track: " + timedTextSourceUri);
                return;
            }

            // Add a label for each resolved track
            var timedTextSourceUriString = timedTextSourceUri.AbsoluteUri;
            if(timedTextSourceUriString.Contains("_en"))
            {
                args.Tracks[0].Label = "English";
            }
            else if(timedTextSourceUriString.Contains("_pt"))
            {
                args.Tracks[0].Label = "Portuguese";
            }
        }
        //</SnippetTimedTextSourceResolved>
        #endregion

        #region custommetadata
        private async void PlayMediaPlaybackItemWithCustomTracks()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.FileTypeFilter.Add(".mkv");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();

            mediaSource = MediaSource.CreateFromStorageFile(file);
            mediaPlaybackItem = new MediaPlaybackItem(mediaSource);

            

            mediaPlaybackItem.AudioTracksChanged += PlaybackItem_AudioTracksChanged;
            mediaPlaybackItem.VideoTracksChanged += MediaPlaybackItem_VideoTracksChanged;
            mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

            AddTimedMetaDataTrack_Data();
            AddTimedMetaDataTrack_Text();

            mediaElement.SetPlaybackSource(mediaPlaybackItem);
        }

        private void AddTimedMetaDataTrack_Data()
        {
            //<SnippetAddDataTrack>
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
                DataCue cue = new DataCue()
                {
                    Id = "ID_" + i,
                    Data = buffer,
                    StartTime = TimeSpan.FromSeconds(3 + i * 3),
                    Duration = TimeSpan.FromSeconds(2)
                };

                metadataTrack.AddCue(cue);

            }

            mediaSource.ExternalTimedMetadataTracks.Add(metadataTrack);
            //</SnippetAddDataTrack>
        }

        private void MetadataTrack_CueExited(TimedMetadataTrack sender, MediaCueEventArgs args)
        {
            
        }

        //<SnippetDataCueEntered>
        private void MetadataTrack_DataCueEntered(TimedMetadataTrack sender, MediaCueEventArgs args)
        {
            DataCue cue = (DataCue)args.Cue;
            string data = System.Text.Encoding.Unicode.GetString(cue.Data.ToArray());
            System.Diagnostics.Debug.WriteLine("Cue entered: " + data);
        }
        //</SnippetDataCueEntered>

        private void AddTimedMetaDataTrack_Text()
        {

            //<SnippetAddTextTrack>
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

            mediaSource.ExternalTimedMetadataTracks.Add(metadataTrack);
            //</SnippetAddTextTrack>
        }

        //<SnippetTextCueEntered>
        private void MetadataTrack_TextCueEntered(TimedMetadataTrack sender, MediaCueEventArgs args)
        {
            TimedTextCue cue = (TimedTextCue)args.Cue;
            System.Diagnostics.Debug.WriteLine("Cue entered: " + cue.Id + " " + cue.Lines[0].Text);
        }
        //</SnippetTextCueEntered>

        
        #endregion

        #region MediaPlaybackList

        private async void PlayMediaPlaybackList()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.FileTypeFilter.Add(".mkv");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //<SnippetPlayMediaPlaybackList>
            StorageFile file = await filePicker.PickSingleFileAsync();
            mediaSource = MediaSource.CreateFromStorageFile(file);
            mediaSource.CustomProperties["Title"] = "Clip 1 title";
            mediaPlaybackItem = new MediaPlaybackItem(mediaSource);


            file = await filePicker.PickSingleFileAsync();
            mediaSource2 = MediaSource.CreateFromStorageFile(file);
            mediaSource2.CustomProperties["Title"] = "Clip 2 title";
            mediaPlaybackItem2 = new MediaPlaybackItem(mediaSource2);

            mediaPlaybackList = new MediaPlaybackList();
            mediaPlaybackList.Items.Add(mediaPlaybackItem);
            mediaPlaybackList.Items.Add(mediaPlaybackItem2);

            mediaPlaybackList.CurrentItemChanged += MediaPlaybackList_CurrentItemChanged;

            mediaElement.SetPlaybackSource(mediaPlaybackList);
            //</SnippetPlayMediaPlaybackList>
        }

        //<SnippetMediaPlaybackListItemChanged>
        private async void MediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            if(args.NewItem.Source.CustomProperties["Title"] != null)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    clipTitleTextBlock.Text = args.NewItem.Source.CustomProperties["Title"] as string;
                });
            }
        }
        //</SnippetMediaPlaybackListItemChanged>

        //<SnippetPrevButton>
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlaybackList.MovePrevious();
        }
        //</SnippetPrevButton>

        //<SnippetNextButton>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlaybackList.MoveNext();
        }
        //</SnippetNextButton>

        //<SnippetShuffleButton>
        private async void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlaybackList.ShuffleEnabled = !mediaPlaybackList.ShuffleEnabled;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                shuffleButton.FontWeight =
                    mediaPlaybackList.ShuffleEnabled ? Windows.UI.Text.FontWeights.Bold : Windows.UI.Text.FontWeights.Light;
            });
        }
        //</SnippetShuffleButton>

        //<SnippetRepeatButton>
        private async void autoRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlaybackList.AutoRepeatEnabled = !mediaPlaybackList.AutoRepeatEnabled;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                autoRepeatButton.FontWeight =
                    mediaPlaybackList.AutoRepeatEnabled ? Windows.UI.Text.FontWeights.Bold : Windows.UI.Text.FontWeights.Light;
            });
        }
        //</SnippetRepeatButton>
        #endregion

        private void ShowMessageToUser(string s) { }
    }
}
