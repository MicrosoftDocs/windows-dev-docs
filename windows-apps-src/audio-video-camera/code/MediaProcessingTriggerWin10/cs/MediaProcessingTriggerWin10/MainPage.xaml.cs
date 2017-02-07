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

//<SnippetForegroundUsing>
using Windows.ApplicationModel.Background;
using Windows.Storage;
//</SnippetForegroundUsing>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaProcessingTriggerWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetForegroundMembers>
        MediaProcessingTrigger mediaProcessingTrigger;
        string backgroundTaskBuilderName = "TranscodingBackgroundTask";
        BackgroundTaskRegistration taskRegistration;
        //</SnippetForegroundMembers>

        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PickFilesToTranscode();
 
        }
        //<SnippetPickFilesToTranscode>
        private async void PickFilesToTranscode()
        {
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

            if(source == null || destination == null)
            {
                return;
            }

            var storageItemAccessList = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
            storageItemAccessList.Add(source);
            storageItemAccessList.Add(destination);

            ApplicationData.Current.LocalSettings.Values["InputFileName"] = source.Path;
            ApplicationData.Current.LocalSettings.Values["OutputFileName"] = destination.Path;
        }
        //</SnippetPickFilesToTranscode>

        //<SnippetRegisterBackgroundTask>
        private void RegisterBackgroundTask()
        {
            // New a MediaProcessingTrigger
            mediaProcessingTrigger = new MediaProcessingTrigger();

            var builder = new BackgroundTaskBuilder();

            builder.Name = backgroundTaskBuilderName;
            builder.TaskEntryPoint = "MediaProcessingBackgroundTask.MediaProcessingTask";
            builder.SetTrigger(mediaProcessingTrigger);

            // unregister old ones
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == backgroundTaskBuilderName)
                {
                    cur.Value.Unregister(true);
                }
            }

            taskRegistration = builder.Register();
            taskRegistration.Progress += new BackgroundTaskProgressEventHandler(OnProgress);
            taskRegistration.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);

            return;
        }
        //</SnippetRegisterBackgroundTask>

        

        //<SnippetLaunchBackgroundTask>
        private async void LaunchBackgroundTask()
        {
            var success = true;

            if (mediaProcessingTrigger != null)
            {
                MediaProcessingTriggerResult activationResult;
                activationResult = await mediaProcessingTrigger.RequestAsync();

                switch (activationResult)
                {
                    case MediaProcessingTriggerResult.Allowed:
                        // Task starting successfully
                        break;

                    case MediaProcessingTriggerResult.CurrentlyRunning:
                    // Already Triggered

                    case MediaProcessingTriggerResult.DisabledByPolicy:
                    // Disabled by system policy

                    case MediaProcessingTriggerResult.UnknownError:
                        // All other failures
                        success = false;
                        break;
                }

                if (!success)
                {
                    // Unregister the media processing trigger background task
                    taskRegistration.Unregister(true);
                }
            }

        }
        //</SnippetLaunchBackgroundTask>

        //<SnippetOnProgress>
        private void OnProgress(IBackgroundTaskRegistration task, BackgroundTaskProgressEventArgs args)
        {
            string progress = "Progress: " + args.Progress + "%";
            Debug.WriteLine(progress);
        }
        //</SnippetOnProgress>

        //<SnippetOnCompleted>
        private void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
        {
            Debug.WriteLine(" background task complete");
        }
        //</SnippetOnCompleted>

        private void START_Click(object sender, RoutedEventArgs e)
        {
            RegisterBackgroundTask();
            LaunchBackgroundTask();
        }
    }
}
