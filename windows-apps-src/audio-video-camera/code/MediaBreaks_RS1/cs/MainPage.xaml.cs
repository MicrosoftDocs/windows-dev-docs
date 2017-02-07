using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace MediaBreaks_RS1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MediaPlayer _mediaPlayer;

        MediaPlaybackList _mediaPlaybackList;

        MediaPlaybackItem _globalPlaybackItem;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Action1Button_Click(object sender, RoutedEventArgs e)
        {
            await CreateMediaBreakScheduleTest();
            RegisterMediaBreakEvents();
        }

        //http://www.fabrikam.com/
        private void CreateMediaBreakSchedule()
        {
            //<SnippetMoviePlaybackItem>
            MediaPlaybackItem moviePlaybackItem =
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/movie.mkv")));
            //</SnippetMoviePlaybackItem>

            //<SnippetPreRollBreak>   
            MediaBreak preRollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt);
            MediaPlaybackItem prerollAd = 
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/preroll_ad.mp4")));
            prerollAd.CanSkip = false;
            preRollMediaBreak.PlaybackList.Items.Add(prerollAd);

            moviePlaybackItem.BreakSchedule.PrerollBreak = preRollMediaBreak;
            //</SnippetPreRollBreak>


            //<SnippetPostRollBreak>
            MediaBreak postrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt);
            MediaPlaybackItem postRollAd =
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/postroll_ad.mp4")));
            postrollMediaBreak.PlaybackList.Items.Add(postRollAd);

            moviePlaybackItem.BreakSchedule.PostrollBreak = postrollMediaBreak;
            //</SnippetPostRollBreak>


            //<SnippetMidrollBreak>
            MediaBreak midrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt, TimeSpan.FromMinutes(10));
            midrollMediaBreak.PlaybackList.Items.Add(
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/midroll_ad_1.mp4"))));
            midrollMediaBreak.PlaybackList.Items.Add(
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/midroll_ad_2.mp4"))));
            moviePlaybackItem.BreakSchedule.InsertMidrollBreak(midrollMediaBreak);
            //</SnippetMidrollBreak>

            //<SnippetMidrollBreak2>
            midrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Replace, TimeSpan.FromMinutes(20));
            MediaPlaybackItem ad = 
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/midroll_ad_3.mp4")),
                TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(15));
            ad.CanSkip = false;
            midrollMediaBreak.PlaybackList.Items.Add(ad);
            //</SnippetMidrollBreak2>


            //<SnippetPlay>
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.AutoPlay = true;
            _mediaPlayer.Source = moviePlaybackItem;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            //</SnippetPlay>
        }
        private async Task CreateMediaBreakScheduleTest()
        {
            //SnippetMoviePlaybackItem
            MediaPlaybackItem moviePlaybackItem = await GetMoviePlaybackItem();
            ///SnippetMoviePlaybackItem>

            //SnippetPreRollBreak 
            MediaBreak preRollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt);
            MediaPlaybackItem prerollAd = await GetAdPlaybackItem();
            prerollAd.CanSkip = false;
            preRollMediaBreak.PlaybackList.Items.Add(prerollAd);

            moviePlaybackItem.BreakSchedule.PrerollBreak = preRollMediaBreak;
            ///SnippetPreRollBreak

            /*
            //SnippetPostRollBreak
            MediaBreak postrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt);
            MediaPlaybackItem postRollAd = await GetAdPlaybackItem();
            postrollMediaBreak.PlaybackList.Items.Add(postRollAd);

            moviePlaybackItem.BreakSchedule.PostrollBreak = postrollMediaBreak;
            ///SnippetPostRollBreak


            ///SnippetMidrollBreak
            MediaBreak midrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Replace, TimeSpan.FromSeconds(10));
            midrollMediaBreak.PlaybackList.Items.Add(await GetAdPlaybackItem());
            midrollMediaBreak.PlaybackList.Items.Add(await GetAdPlaybackItem());
            //MediaPlaybackItem ad = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://myserver/midroll_ad_3.mp4")),
            //TimeSpan.FromSeconds(5),
            //TimeSpan.FromSeconds(15));
            //MediaPlaybackItem ad = await GetScheduledMediaPlaybackItem();
            //ad.CanSkip = false;
            //midrollMediaBreak.PlaybackList.Items.Add(ad);

            moviePlaybackItem.BreakSchedule.InsertMidrollBreak(midrollMediaBreak);

            ///SnippetMidrollBreak
            */
            //Play
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.AutoPlay = true;
            _mediaPlayer.Source = moviePlaybackItem;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            ///Play
        }
        //<SnippetSkipButtonClick>
        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.BreakManager.SkipCurrentBreak();
        }
        //</SnippetSkipButtonClick>

        
        private void RegisterMediaBreakEvents()
        {
            //<SnippetRegisterMediaBreakEvents>
            _mediaPlayer.BreakManager.BreakStarted += BreakManager_BreakStarted;
            _mediaPlayer.BreakManager.BreakEnded += BreakManager_BreakEnded;
            _mediaPlayer.BreakManager.BreakSkipped += BreakManager_BreakSkipped;
            _mediaPlayer.BreakManager.BreaksSeekedOver += BreakManager_BreaksSeekedOver;
            //</SnippetRegisterMediaBreakEvents>
        }
        //<SnippetBreakStarted>
        private async void BreakManager_BreakStarted(MediaBreakManager sender, MediaBreakStartedEventArgs args)
        {
            MediaBreak currentBreak = sender.CurrentBreak;
            var currentIndex = currentBreak.PlaybackList.CurrentItemIndex;
            var itemCount = currentBreak.PlaybackList.Items.Count;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                statusTextBlock.Text = String.Format("Playing ad {0} of {1}", currentIndex + 1, itemCount);
            });
        }
        //</SnippetBreakStarted>

        //<SnippetGetCurrentBreakItemIndex>
        public int GetCurrentBreakItemIndex()
        {
            MediaBreak mediaBreak = _mediaPlayer.BreakManager.CurrentBreak;
            if(mediaBreak != null)
            {
                return (int)mediaBreak.PlaybackList.CurrentItemIndex;
            } 
            else 
            {
                return -1;
            }
        }
        //</SnippetGetCurrentBreakItemIndex>


        //<SnippetBreakEnded>
        private async void BreakManager_BreakEnded(MediaBreakManager sender, MediaBreakEndedEventArgs args)
        {
            // Update UI to show that the MediaBreak is no longer playing
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                statusTextBlock.Text = "";
            });

            args.MediaBreak.CanStart = false;
        }
        //</SnippetBreakEnded>

        //<SnippetBreakSkipped>
        private async void BreakManager_BreakSkipped(MediaBreakManager sender, MediaBreakSkippedEventArgs args)
        {
            // Update UI to show that the MediaBreak is no longer playing
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                statusTextBlock.Text = "";
            });

            MediaPlaybackItem currentItem = _mediaPlayer.Source as MediaPlaybackItem;
            if(currentItem.BreakSchedule.PrerollBreak !=  null 
                && currentItem.BreakSchedule.PrerollBreak == args.MediaBreak)
            {
                MediaBreak mediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt, TimeSpan.FromMinutes(10));
                mediaBreak.PlaybackList.Items.Add(await GetAdPlaybackItem());
                currentItem.BreakSchedule.InsertMidrollBreak(mediaBreak);
            }
        }
        //</SnippetBreakSkipped>

        //<SnippetBreakSeekedOver>
        private void BreakManager_BreaksSeekedOver(MediaBreakManager sender, MediaBreakSeekedOverEventArgs args)
        {
            if(args.SeekedOverBreaks.Count > 1
                && args.NewPosition.TotalMinutes > args.OldPosition.TotalMinutes
                && args.NewPosition.TotalMinutes - args.OldPosition.TotalMinutes < 10.0)
            {
                _mediaPlayer.BreakManager.PlayBreak(args.SeekedOverBreaks[0]);
            }
        }
        //</SnippetBreakSeekedOver>


        private void GetSessionInfo()
        {
            //<SnippetRegisterBufferingProgressChanged>
            _mediaPlayer.BreakManager.PlaybackSession.BufferingProgressChanged += PlaybackSession_BufferingProgressChanged;
            //</SnippetRegisterBufferingProgressChanged>
        }
        //<SnippetBufferingProgressChanged>
        private async void PlaybackSession_BufferingProgressChanged(MediaPlaybackSession sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                bufferingProgressBar.Value = sender.BufferingProgress;
            });
        }
        //</SnippetBufferingProgressChanged>
        private async Task<MediaPlaybackItem> GetMoviePlaybackItem()
        {
            return await GetMediaPlaybackItem();
        }

        private async Task<MediaPlaybackItem> GetAdPlaybackItem()
        {
            _globalPlaybackItem = await GetMediaPlaybackItem();
            return _globalPlaybackItem;
        }

        private async Task<MediaPlaybackItem> GetMediaPlaybackItem()
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

            return new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));

         
        }
        private async Task<MediaPlaybackItem> GetScheduledMediaPlaybackItem()
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

            return new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(15));


        }

        
    }
}
