using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// <SnippetLLFUsing>
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
// </SnippetLLFUsing>

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LowLightFusionSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            LowLightFuseSelectedPicturesAsync();
        }



        // Return a vector of software bitmaps of maximum count of maxFrames selected by a user and loaded from a stream  
        public async Task<List<SoftwareBitmap>> GetSelectedSoftwareBitmaps()
        {
            // <SnippetGetMaxLLFFrames>
            // Query the supported max number of input bitmap frames for Low Light Fusion 
            int maxFrames = LowLightFusion.MaxSupportedFrameCount;

            // The bitmap frames to perform Low Light Fusion on.
            var framelist = new List<SoftwareBitmap>(maxFrames);
            // </SnippetGetMaxLLFFrames>



            // <SnippetGetFrames>
            var fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            var inputFiles = await fileOpenPicker.PickMultipleFilesAsync();

            if (inputFiles == null)
            {
                // The user cancelled the picking operation
                return null;
            }
            if(inputFiles.Count >= maxFrames)
            {
                Debug.WriteLine("You can only choose up to {0} image(s) to input.", maxFrames);
            }
            // </SnippetGetFrames>


            // <SnippetDecodeFrames>
            SoftwareBitmap softwareBitmap;

            // Decode the images into bitmaps
            for (int i = 0; i < inputFiles.Count; i++)
            {
                using (IRandomAccessStream stream = await inputFiles[0].OpenAsync(FileAccessMode.Read))
                {
                    // Create the decoder from the stream
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                    // Get the SoftwareBitmap representation of the file
                    softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                    framelist.Add(softwareBitmap);
                }
            }
            
            // Ensure that the selected bitmap frames have an acceptable pixel format for Low Light Fusion.
            // For this sample, we'll use the pixel format at index 0.
            IReadOnlyList<BitmapPixelFormat> formats = LowLightFusion.SupportedBitmapPixelFormats;
            BitmapPixelFormat llfFormat = formats[0];
            for (int i = 0; i < framelist.Count; i++)
            {
                if (framelist[i].BitmapPixelFormat == llfFormat)
                {
                    // The pixel format is acceptable
                }
                else
                {
                    // Convert the pixel format
                    framelist[i] = SoftwareBitmap.Convert(framelist[i], llfFormat);
                }
            }           
            return framelist;
            // </SnippetDecodeFrames>
        }

        // Encode the resulting bitmap frame and save it to a file
        public async Task EncodeAndSaveToFileAsync(SoftwareBitmap frame)
        {
            // <SnippetEncodeFrame>
            // Convert bitmap to Rgba16, Rgba8, or Bgra8 for encoding.
            var frameConverted = SoftwareBitmap.Convert(frame, BitmapPixelFormat.Bgra8);


            var fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileSavePicker.FileTypeChoices.Add("PNG files", new List<string>() { ".png" });
            fileSavePicker.SuggestedFileName = "LLF Output";

            var outputFile = await fileSavePicker.PickSaveFileAsync();
            if (outputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }

            // Encode and save the image
            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // Create an encoder with the desired format
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

                // Set the software bitmap
                encoder.SetSoftwareBitmap(frameConverted);

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    switch (err.HResult)
                    {
                        case unchecked((int)0x88982F81): // WINCODEC_ERR_UNSUPPORTEDOPERATION
                                                         // If the encoder does not support writing a thumbnail, then try again
                                                         // but disable thumbnail generation.
                            encoder.IsThumbnailGenerated = false;
                            break;
                        default:
                            throw err;
                    }
                }

                if (encoder.IsThumbnailGenerated == false)
                {
                    await encoder.FlushAsync();
                }
            }
            // </SnippetEncodeFrame>
        }

        // Perform Low Light Fusion on user selected images
        public async Task LowLightFuseSelectedPicturesAsync()
        {
            // 1. Get user selected pictures and condition the bitmaps so they match a supported pixel format for fusion
            List<SoftwareBitmap> pictures = await GetSelectedSoftwareBitmaps();

            // 2. Fuse the bitmaps and perform Low Light Fusion to get the resulting frame
            // <SnippetFuseFrames>
            LowLightFusionResult result = await LowLightFusion.FuseAsync(pictures);
            // </SnippetFuseFrames>

            // 3. Encode the resulting Low Light Fusion frame to a file 
            await EncodeAndSaveToFileAsync(result.Frame);
        }
    }
}
