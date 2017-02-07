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

//<SnippetUsingCaptureUI>
using Windows.Media.Capture;
using Windows.Storage;
//</SnippetUsingCaptureUI>

//<SnippetUsingSoftwareBitmap>
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
//</SnippetUsingSoftwareBitmap>

//<SnippetUsingSoftwareBitmapSource>
using Windows.UI.Xaml.Media.Imaging;
//</SnippetUsingSoftwareBitmapSource>

//<SnippetUsingMediaComposition>
using Windows.Media.Editing;
using Windows.Media.Core;
//</SnippetUsingMediaComposition>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CameraCaptureUIWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<SnippetDeclareMediaComposition>
        MediaComposition mediaComposition;
        MediaStreamSource mediaStreamSource;
        //</SnippetDeclareMediaComposition>

        public MainPage()
        {
            this.InitializeComponent();

            //<SnippetInitComposition>
            mediaComposition = new MediaComposition();
            //</SnippetInitComposition>
        }

        private void launchCameraUIButton_Click(object sender, RoutedEventArgs e)
        {
            

            CapturePhoto();
            //CaptureVideo();
 
        }
        private async void CapturePhoto()
        {
            //<SnippetCapturePhoto>
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200); 

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            
            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
            //</SnippetCapturePhoto>

            //<SnippetCopyAndDeletePhoto>
            StorageFolder destinationFolder = 
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("ProfilePhotoFolder", 
                    CreationCollisionOption.OpenIfExists);

            await photo.CopyAsync(destinationFolder, "ProfilePhoto.jpg", NameCollisionOption.ReplaceExisting);
            await photo.DeleteAsync();
            //</SnippetCopyAndDeletePhoto>

            //<SnippetSoftwareBitmap>
            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            //</SnippetSoftwareBitmap>

            //<SnippetSetImageSource>
            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
                    BitmapPixelFormat.Bgra8, 
                    BitmapAlphaMode.Premultiplied);

            SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

            imageControl.Source = bitmapSource;
            //</SnippetSetImageSource>

        }
        public async void CaptureVideo()
        {
            //<SnippetCaptureVideo>
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;

            StorageFile videoFile = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Video);

            if (videoFile == null)
            {
                // User cancelled photo capture
                return;
            }
            //</SnippetCaptureVideo>

            //<SnippetAddToComposition>
            MediaClip mediaClip = await MediaClip.CreateFromFileAsync(videoFile);

            mediaComposition.Clips.Add(mediaClip);
            mediaStreamSource = mediaComposition.GeneratePreviewMediaStreamSource(
                (int)mediaElement.ActualWidth,
                (int)mediaElement.ActualHeight);
            //</SnippetAddToComposition>

            //<SnippetSetMediaElementSource>
            mediaElement.SetMediaStreamSource(mediaStreamSource);
            //</SnippetSetMediaElementSource>

        }
    }
}
