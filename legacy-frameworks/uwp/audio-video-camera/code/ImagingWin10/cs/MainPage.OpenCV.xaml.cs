using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// <SnippetOpenCVMainPageUsing>
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
// </SnippetOpenCVMainPageUsing>

namespace ImagingWin10
{
    public sealed partial class MainPage : Page
    {


        private async void OpenCVButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // <SnippetOpenCVBlur>
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            var inputFile = await fileOpenPicker.PickSingleFileAsync();

            if (inputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }

            SoftwareBitmap inputBitmap;
            using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file
                inputBitmap = await decoder.GetSoftwareBitmapAsync();
            }

            if (inputBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8
                        || inputBitmap.BitmapAlphaMode != BitmapAlphaMode.Premultiplied)
            {
                inputBitmap = SoftwareBitmap.Convert(inputBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }
                
            SoftwareBitmap outputBitmap = new SoftwareBitmap(inputBitmap.BitmapPixelFormat, inputBitmap.PixelWidth, inputBitmap.PixelHeight, BitmapAlphaMode.Premultiplied);


            var helper = new OpenCVBridge.OpenCVHelper();
            helper.Blur(inputBitmap, outputBitmap);

            var bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(outputBitmap);
            imageControl.Source = bitmapSource;
            // </SnippetOpenCVBlur>
        }
    }
}
