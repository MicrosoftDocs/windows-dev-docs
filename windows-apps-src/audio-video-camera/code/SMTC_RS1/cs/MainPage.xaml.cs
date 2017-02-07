using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Media.Core;
using Windows.Media.Playback;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SMTC_RS1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MediaPlayer _mediaPlayer;
        MediaPlaybackList _mediaPlaybackList;

        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void Action1_Click(object sender, RoutedEventArgs e)
        {
            await PlayMediaPlaybackList();
            AddNextHandler();
            AddPreviousHandler();
            _mediaPlayer.Play();
        }
        private void Action2_Click(object sender, RoutedEventArgs e)
        {
            EnableNextButton();
        }

        private void AddNextHandler()
        {
            //<SnippetAddNextHandler>
            _mediaPlayer.CommandManager.NextReceived += CommandManager_NextReceived;
            _mediaPlayer.CommandManager.NextBehavior.IsEnabledChanged += NextBehavior_IsEnabledChanged;
            //</SnippetAddNextHandler>
        }

        

        //<SnippetNextReceived>
        int _nextPressCount = 0;
        private void CommandManager_NextReceived(MediaPlaybackCommandManager sender, MediaPlaybackCommandManagerNextReceivedEventArgs args)
        {
            _nextPressCount++;
            if (_nextPressCount > 5)
            {
                sender.NextBehavior.EnablingRule = MediaCommandEnablingRule.Never;
                // Perform app tasks while the Next button is disabled
            }
        }
        //</SnippetNextReceived>

        
        private void EnableNextButton()
        {
            //<SnippetEnableNextButton>
            _mediaPlayer.CommandManager.NextBehavior.EnablingRule = MediaCommandEnablingRule.Auto;
            _nextPressCount = 0;
            //</SnippetEnableNextButton>
        }
        //<SnippetIsEnabledChanged>
        private void NextBehavior_IsEnabledChanged(MediaPlaybackCommandManagerCommandBehavior sender, object args)
        {
            MyNextButton.IsEnabled = sender.IsEnabled;
        }
        //</SnippetIsEnabledChanged>

        private void MyNextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPreviousHandler()
        {
            //<SnippetAddPreviousHandler>
            _mediaPlayer.CommandManager.PreviousReceived += CommandManager_PreviousReceived;
            //</SnippetAddPreviousHandler>
        }
        //<SnippetPreviousReceived>
        private async void CommandManager_PreviousReceived(MediaPlaybackCommandManager sender, MediaPlaybackCommandManagerPreviousReceivedEventArgs args)
        {
            var deferral = args.GetDeferral();
            MediaPlaybackItem mediaPlaybackItem = await GetPreviousStation();

            if(args.Handled != true)
            {
                args.Handled = true;
                sender.MediaPlayer.Source = mediaPlaybackItem;
                sender.MediaPlayer.Play();
            }
            deferral.Complete();
        }
        //</SnippetPreviousReceived>

        private async Task<MediaPlaybackItem> GetPreviousStation()
        {
            FileOpenPicker filePicker = new FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.FileTypeFilter.Add(".mkv");
            filePicker.FileTypeFilter.Add(".mp3");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            var file = await filePicker.PickSingleFileAsync();
            return new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
        }

        private async Task PlayMediaPlaybackList()
        {
            //Create a new picker
            FileOpenPicker filePicker = new FileOpenPicker();

            //Add filetype filters.  In this case wmv and mp4.
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.FileTypeFilter.Add(".mkv");
            filePicker.FileTypeFilter.Add(".mp3");

            //Set picker start location to the video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            //<SnippetPlayMediaPlaybackList>
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

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = _mediaPlaybackList;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            //</SnippetPlayMediaPlaybackList>

            for (int i = 0; i < _mediaPlaybackList.Items.Count; i++)
            {
                var props = _mediaPlaybackList.Items[i].GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Music;
                props.MusicProperties.Title = "Track title " + i;
                props.MusicProperties.Artist = "Track artist " + i;
                _mediaPlaybackList.Items[i].ApplyDisplayProperties(props);
            }
        }


        Queue<MediaPlaybackItem> _playbackItemQueue = new Queue<MediaPlaybackItem>();
        int maxCachedItems = 3;

        private void MediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            _playbackItemQueue.Enqueue(args.OldItem);
            if (_playbackItemQueue.Count > maxCachedItems)
            {
                MediaPlaybackItem oldestItem = _playbackItemQueue.Dequeue();

                // If the oldest item doesn't have another entry in the queue and it's not the currently playing item
                if (!(oldestItem == null || _playbackItemQueue.Contains<MediaPlaybackItem>(oldestItem) || oldestItem != args.NewItem))
                {
                    oldestItem.Source.Reset();
                }
            }
        }

        private void MediaPlaybackList_ItemOpened(MediaPlaybackList sender, MediaPlaybackItemOpenedEventArgs args)
        {


        }

        private void MediaPlaybackList_ItemFailed(MediaPlaybackList sender, MediaPlaybackItemFailedEventArgs args)
        {

        }

        

        
    }
}
