using Windows.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

//<SnippetNamespaces>
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
//</SnippetNamespaces>

using Windows.Devices.Geolocation;
using Windows.Storage.FileProperties;


//<SnippetDirect3DNamespace>
using Windows.Graphics.DirectX.Direct3D11;
//</SnippetDirect3DNamespace>

//<SnippetInteropNamespace>
using System.Runtime.InteropServices;
//</SnippetInteropNamespace>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ImagingWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    //<SnippetCOMImport>
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }
    //</SnippetCOMImport>

    public sealed partial class MainPage : Page
    {
        SoftwareBitmap softwareBitmap;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OpenButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            //<SnippetPickInputFile>
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
            //</SnippetPickInputFile>

            softwareBitmap = await CreateSoftwareBitmapFromFile(inputFile);


            //<SnippetPickOuputFile>
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileSavePicker.FileTypeChoices.Add("JPEG files", new List<string>() { ".jpg" });
            fileSavePicker.SuggestedFileName = "image";

            var outputFile = await fileSavePicker.PickSaveFileAsync();

            if (outputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }
            //</SnippetPickOuputFile>


            SaveSoftwareBitmapToFile(softwareBitmap, outputFile);

            SoftwareBitmapToWriteableBitmap(softwareBitmap);

            WriteableBitmapToSoftwareBitmap((WriteableBitmap)imageControl.Source);

        }

        private async Task<SoftwareBitmap> CreateSoftwareBitmapFromFile(StorageFile inputFile)
        {
            //<SnippetCreateSoftwareBitmapFromFile>
            SoftwareBitmap softwareBitmap;

            using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }
            //</SnippetCreateSoftwareBitmapFromFile>

            return softwareBitmap;
        }


        //<SnippetSaveSoftwareBitmapToFile>
        private async void SaveSoftwareBitmapToFile(SoftwareBitmap softwareBitmap, StorageFile outputFile)
        {
            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // Create an encoder with the desired format
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

                // Set the software bitmap
                encoder.SetSoftwareBitmap(softwareBitmap);

                // Set additional encoding parameters, if needed
                encoder.BitmapTransform.ScaledWidth = 320;
                encoder.BitmapTransform.ScaledHeight = 240;
                encoder.BitmapTransform.Rotation = Windows.Graphics.Imaging.BitmapRotation.Clockwise90Degrees;
                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant;
                encoder.IsThumbnailGenerated = true;

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    switch (err.HResult)
                    {
                        case unchecked((int)0x88982F81): //WINCODEC_ERR_UNSUPPORTEDOPERATION
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
        }
        //</SnippetSaveSoftwareBitmapToFile>

        private async void UseEncodingOptions(StorageFile outputFile)
        {

            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                //<SnippetUseEncodingOptions>
                var propertySet = new Windows.Graphics.Imaging.BitmapPropertySet();
                var qualityValue = new Windows.Graphics.Imaging.BitmapTypedValue(
                    1.0, // Maximum quality
                    Windows.Foundation.PropertyType.Single
                    );

                propertySet.Add("ImageQuality", qualityValue);

                await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(
                    Windows.Graphics.Imaging.BitmapEncoder.JpegEncoderId,
                    stream,
                    propertySet
                );
                //</SnippetUseEncodingOptions>
            }

        }





        private async void SoftwareBitmapToWriteableBitmap(SoftwareBitmap softwareBitmap)
        {
            softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight);

            // CHANGE THIS SNIPPET NAME - LEAVING NOW TO AVOID RISK OF BUILD BREAK
            //<SnippetSoftwareBitmapToWriteableBitmap>
            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
                softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            var source = new SoftwareBitmapSource();
            await source.SetBitmapAsync(softwareBitmap);

            // Set the source of the Image control
            imageControl.Source = source;
            //</SnippetSoftwareBitmapToWriteableBitmap>
        }
        private void WriteableBitmapToSoftwareBitmap(WriteableBitmap writeableBitmap)
        {
            //<SnippetWriteableBitmapToSoftwareBitmap>
            SoftwareBitmap outputBitmap = SoftwareBitmap.CreateCopyFromBuffer(
                writeableBitmap.PixelBuffer,
                BitmapPixelFormat.Bgra8,
                writeableBitmap.PixelWidth,
                writeableBitmap.PixelHeight
            );
            //</SnippetWriteableBitmapToSoftwareBitmap>
        }

        private unsafe void CreateNewSoftwareBitmap()
        {
            //<SnippetCreateNewSoftwareBitmap>
            softwareBitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8, 100, 100);

            using (BitmapBuffer buffer = softwareBitmap.LockBuffer(BitmapBufferAccessMode.Write))
            {
                using (var reference = buffer.CreateReference())
                {
                    byte* dataInBytes;
                    uint capacity;
                    ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacity);

                    // Fill-in the BGRA plane
                    BitmapPlaneDescription bufferLayout = buffer.GetPlaneDescription(0);
                    for (int i = 0; i < bufferLayout.Height; i++)
                    {
                        for (int j = 0; j < bufferLayout.Width; j++)
                        {

                            byte value = (byte)((float)j / bufferLayout.Width * 255);
                            dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 0] = value;
                            dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 1] = value;
                            dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 2] = value;
                            dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 3] = (byte)255;
                        }
                    }
                }
            }
            //</SnippetCreateNewSoftwareBitmap>
        }

        private void ConvertToBGR8()
        {
            //<SnippetConvert>
            SoftwareBitmap bitmapBGRA8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            //</SnippetConvert>
        }
        //<SnippetCreateSoftwareBitmapFromSurface>
        private async void CreateSoftwareBitmapFromSurface(IDirect3DSurface surface)
        {
            softwareBitmap = await SoftwareBitmap.CreateCopyFromSurfaceAsync(surface);
        }
        //</SnippetCreateSoftwareBitmapFromSurface>

        //<SnippetTranscodeImageFile>
        private async void TranscodeImageFile(StorageFile imageFile)
        {


            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                var memStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(memStream, decoder);

                encoder.BitmapTransform.ScaledWidth = 320;
                encoder.BitmapTransform.ScaledHeight = 240;

                await encoder.FlushAsync();

                memStream.Seek(0);
                fileStream.Seek(0);
                fileStream.Size = 0;
                await RandomAccessStream.CopyAsync(memStream, fileStream);

                memStream.Dispose();
            }
        }
        //</SnippetTranscodeImageFile>




        //<SnippetGetImageProperties>
        private async void GetImageProperties(StorageFile imageFile)
        {
            ImageProperties props = await imageFile.Properties.GetImagePropertiesAsync();

            string title = props.Title;
            if (title == null)
            {
                // Format does not support, or image does not contain Title property
            }

            DateTimeOffset date = props.DateTaken;
            if (date == null)
            {
                // Format does not support, or image does not contain DateTaken property
            }
        }
        //</SnippetGetImageProperties>

        private async void GetWindowsProperties(StorageFile imageFile)
        {
            //<SnippetGetWindowsProperties>
            ImageProperties props = await imageFile.Properties.GetImagePropertiesAsync();

            var requests = new System.Collections.Generic.List<string>();
            requests.Add("System.Photo.Orientation");
            requests.Add("System.Photo.Aperture");

            IDictionary<string, object> retrievedProps = await props.RetrievePropertiesAsync(requests);

            ushort orientation;
            if (retrievedProps.ContainsKey("System.Photo.Orientation"))
            {
                orientation = (ushort)retrievedProps["System.Photo.Orientation"];
            }

            double aperture;
            if (retrievedProps.ContainsKey("System.Photo.Aperture"))
            {
                aperture = (double)retrievedProps["System.Photo.Aperture"];
            }
            //</SnippetGetWindowsProperties>
        }

        private async void SetGeoDataFromPoint(StorageFile imageFile)
        {
            //<SnippetSetGeoDataFromPoint>
            var point = new Geopoint(
            new BasicGeoposition
            {
                Latitude = 48.8567,
                Longitude = 2.3508,
            });

            await GeotagHelper.SetGeotagAsync(imageFile, point);
            //</SnippetSetGeoDataFromPoint>

        }

        private async void SetGeoDataFromGeolocator(StorageFile imageFile)
        {
            //<SnippetSetGeoDataFromGeolocator>
            var locator = new Geolocator();

            // Shows the user consent UI if needed
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                await GeotagHelper.SetGeotagFromGeolocatorAsync(imageFile, locator);
            }
            //</SnippetSetGeoDataFromGeolocator>
        }




        // CAPABILITY!!!!!!!
        private async void GetGeoData(StorageFile imageFile)
        {
            //<SnippetGetGeoData>
            Geopoint geoPoint = await GeotagHelper.GetGeotagAsync(imageFile);
            //</SnippetGetGeoData>
        }
        //<SnippetReadImageMetadata>
        private async void ReadImageMetadata(BitmapDecoder bitmapDecoder)
        {

            var requests = new System.Collections.Generic.List<string>();
            requests.Add("System.Photo.Orientation"); // Windows property key for EXIF orientation
            requests.Add("/xmp/dc:creator"); // WIC metadata query for Dublin Core creator

            try
            {
                var retrievedProps = await bitmapDecoder.BitmapProperties.GetPropertiesAsync(requests);

                ushort orientation;
                if (retrievedProps.ContainsKey("System.Photo.Orientation"))
                {
                    orientation = (ushort)retrievedProps["System.Photo.Orientation"].Value;
                }

                string creator;
                if (retrievedProps.ContainsKey("/xmp/dc:creator"))
                {
                    creator = (string)retrievedProps["/xmp/dc:creator"].Value;
                }
            }
            catch (Exception err)
            {
                switch (err.HResult)
                {
                    case unchecked((int)0x88982F41): // WINCODEC_ERR_PROPERTYNOTSUPPORTED
                                                     // The file format does not support the requested metadata.
                        break;
                    case unchecked((int)0x88982F81): // WINCODEC_ERR_UNSUPPORTEDOPERATION
                                                     // The file format does not support any metadata.
                    default:
                        throw err;
                }
            }
        }
        //</SnippetReadImageMetadata>
        //<SnippetWriteImageMetadata>
        private async void WriteImageMetadata(BitmapEncoder bitmapEncoder)
        {
            var propertySet = new Windows.Graphics.Imaging.BitmapPropertySet();
            var orientationValue = new Windows.Graphics.Imaging.BitmapTypedValue(
                1, // Defined as EXIF orientation = "normal"
                Windows.Foundation.PropertyType.UInt16
                );

            propertySet.Add("System.Photo.Orientation", orientationValue);

            try
            {
                await bitmapEncoder.BitmapProperties.SetPropertiesAsync(propertySet);
            }
            catch (Exception err)
            {
                switch (err.HResult)
                {
                    case unchecked((int)0x88982F41): // WINCODEC_ERR_PROPERTYNOTSUPPORTED
                                                     // The file format does not support this property.
                        break;
                    default:
                        throw err;
                }
            }
        }
        //</SnippetWriteImageMetadata>

    }
}
