using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//<SnippetBackgroundUsing>
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
using System.Threading;
//</SnippetBackgroundUsing>

namespace MediaProcessingBackgroundTask
{
    //<SnippetBackgroundClass>
    public sealed class MediaProcessingTask : IBackgroundTask
    {
        //</SnippetBackgroundClass>

        //<SnippetBackgroundMembers>
        IBackgroundTaskInstance backgroundTaskInstance;
        BackgroundTaskDeferral deferral;
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        MediaTranscoder transcoder;
        //</SnippetBackgroundMembers>

        //<SnippetRun>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("In background task Run method");

            backgroundTaskInstance = taskInstance;
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
            taskInstance.Progress = 0;

            deferral = taskInstance.GetDeferral();
            Debug.WriteLine("Background " + taskInstance.Task.Name + " is called @ " + (DateTime.Now).ToString());

            try
            {
                await TranscodeFileAsync();
                ApplicationData.Current.LocalSettings.Values["TranscodingStatus"] = "Completed Successfully";
                SendToastNotification("File transcoding complete.");

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception type: {0}", e.ToString());
                ApplicationData.Current.LocalSettings.Values["TranscodingStatus"] = "Error ocurred: " + e.ToString();
            }


            deferral.Complete();
        }
        //</SnippetRun>

        //<SnippetTranscodeFileAsync>
        private async Task TranscodeFileAsync()
        {
            transcoder = new MediaTranscoder();

            try
            {
                var settings = ApplicationData.Current.LocalSettings;

                settings.Values["TranscodingStatus"] = "Started";

                var inputFileName = ApplicationData.Current.LocalSettings.Values["InputFileName"] as string;
                var outputFileName = ApplicationData.Current.LocalSettings.Values["OutputFileName"] as string;

                if (inputFileName == null || outputFileName == null)
                {
                    return;
                }
      

                // retrieve the transcoding information
                var inputFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(inputFileName);
                var outputFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(outputFileName);

                // create video encoding profile                
                MediaEncodingProfile encodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD720p);

                Debug.WriteLine("PrepareFileTranscodeAsync");
                settings.Values["TranscodingStatus"] = "Preparing to transcode ";
                PrepareTranscodeResult preparedTranscodeResult = await transcoder.PrepareFileTranscodeAsync(
                    inputFile, 
                    outputFile, 
                    encodingProfile);

                if (preparedTranscodeResult.CanTranscode)
                {
                    var startTime = TimeSpan.FromMilliseconds(DateTime.Now.Millisecond);
                    Debug.WriteLine("Starting transcoding @" + startTime);

                    var progress = new Progress<double>(TranscodeProgress);
                    settings.Values["TranscodingStatus"] = "Transcoding ";
                    settings.Values["ProcessingFileName"] = inputFileName;
                    await preparedTranscodeResult.TranscodeAsync().AsTask(cancelTokenSource.Token, progress);

                }
                else
                {
                    Debug.WriteLine("Source content could not be transcoded.");
                    Debug.WriteLine("Transcode status: " + preparedTranscodeResult.FailureReason.ToString());
                    var endTime = TimeSpan.FromMilliseconds(DateTime.Now.Millisecond);
                    Debug.WriteLine("End time = " + endTime);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception type: {0}", e.ToString());
                throw;
            }
        }
        //</SnippetTranscodeFileAsync>

        //<SnippetProgress>
        void TranscodeProgress(double percent)
        {
            Debug.WriteLine("Transcoding progress:  " + percent.ToString().Split('.')[0] + "%");
            backgroundTaskInstance.Progress = (uint)percent;
        }
        //</SnippetProgress>

        //<SnippetOnCanceled>
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested..." + reason.ToString());
        }
        //</SnippetOnCanceled>

        //<SnippetSendToastNotification>
        private void SendToastNotification(string toastMessage)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            //Supply text content for your notification
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(toastMessage));

            //Create the toast notification based on the XML content you've specified.
            ToastNotification toast = new ToastNotification(toastXml);

            //Send your toast notification.
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
        //</SnippetSendToastNotification>

        

        

        

    }
}
