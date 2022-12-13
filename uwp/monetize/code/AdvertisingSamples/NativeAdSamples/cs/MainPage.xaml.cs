using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//<Namespaces>
using Microsoft.Advertising.WinRT.UI;
using Windows.UI.Xaml.Media.Imaging;
//</Namespaces>

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NativeAdSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //<Variables>
        NativeAdsManagerV2 myNativeAdsManager = null;
        string myAppId = "d25517cb-12d4-4699-8bdc-52040c712cab";
        string myAdUnitId = "test";
        //</Variables>

        public MainPage()
        {
            this.InitializeComponent();

            //<ConfigureNativeAd>
            myNativeAdsManager = new NativeAdsManagerV2(myAppId, myAdUnitId);
            myNativeAdsManager.AdReady += MyNativeAd_AdReady;
            myNativeAdsManager.ErrorOccurred += MyNativeAdsManager_ErrorOccurred;
            //</ConfigureNativeAd>

            //<RequestAd>
            myNativeAdsManager.RequestAd();
            //</RequestAd>
        }

        //<AdReady>
        void MyNativeAd_AdReady(object sender, NativeAdReadyEventArgs e)
        {
            NativeAdV2 nativeAd = e.NativeAd;

            // Show the ad icon.
            if (nativeAd.AdIcon != null)
            {
                AdIconImage.Source = nativeAd.AdIcon.Source;

                // Adjust the Image control to the height and width of the 
                // provided ad icon.
                AdIconImage.Height = nativeAd.AdIcon.Height;
                AdIconImage.Width = nativeAd.AdIcon.Width;
            }

            // Show the ad title.
            TitleTextBlock.Text = nativeAd.Title;

            // Show the ad description.
            if (!string.IsNullOrEmpty(nativeAd.Description))
            {
                DescriptionTextBlock.Text = nativeAd.Description;
                DescriptionTextBlock.Visibility = Visibility.Visible;
            }

            // Display the first main image for the ad. Note that the service
            // might provide multiple main images. 
            if (nativeAd.MainImages.Count > 0)
            {
                NativeImage mainImage = nativeAd.MainImages[0];
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(mainImage.Url);
                MainImageImage.Source = bitmapImage;

                // Adjust the Image control to the height and width of the 
                // main image.
                MainImageImage.Height = mainImage.Height;
                MainImageImage.Width = mainImage.Width;
                MainImageImage.Visibility = Visibility.Visible;
            }

            // Add the call to action string to the button.
            if (!string.IsNullOrEmpty(nativeAd.CallToActionText))
            {
                CallToActionButton.Content = nativeAd.CallToActionText;
                CallToActionButton.Visibility = Visibility.Visible;
            }

            // Show the ad sponsored by value.
            if (!string.IsNullOrEmpty(nativeAd.SponsoredBy))
            {
                SponsoredByTextBlock.Text = nativeAd.SponsoredBy;
                SponsoredByTextBlock.Visibility = Visibility.Visible;
            }

            // Show the icon image for the ad.
            if (nativeAd.IconImage != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(nativeAd.IconImage.Url);
                IconImageImage.Source = bitmapImage;

                // Adjust the Image control to the height and width of the 
                // icon image.
                IconImageImage.Height = nativeAd.IconImage.Height;
                IconImageImage.Width = nativeAd.IconImage.Width;
                IconImageImage.Visibility = Visibility.Visible;
            }

            // Register the container of the controls that display
            // the native ad elements for clicks/impressions.
            nativeAd.RegisterAdContainer(NativeAdContainer);
        }
        //</AdReady>

        //<ErrorOccurred>
        private void MyNativeAdsManager_ErrorOccurred(object sender, NativeAdErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("NativeAd error " + e.ErrorMessage +
                " ErrorCode: " + e.ErrorCode.ToString());
        }
        //</ErrorOccurred>
    }
}