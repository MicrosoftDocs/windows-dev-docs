using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BackgroundAudio_RS1
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

            this.Unloaded += MainPage_Unloaded;
            Window.Current.Activated += Current_Activated;
        }

        private void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            throw new NotImplementedException();
        }
        //<SnippetUnloaded>
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.Activated -= Current_Activated;
            GC.Collect();
        }
        //</SnippetUnloaded>

        protected async override void OnNavigatedTo(NavigationEventArgs e)
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

            //Retrieve file from picker
            StorageFile file = await filePicker.PickSingleFileAsync();




            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
            _mediaPlayer.Play();
            //</SnippetPlayMediaPlaybackItem>

        }
    }
}
