//<CompleteSample>
using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// <Snippet1>
using Microsoft.Advertising.WinRT.UI;
// </Snippet1>

namespace InterstitialAdSamplesCSharp
{
    public sealed partial class MainPage : Page
    {
        // Assign myAppId and myAdUnitId to test values. Replace these values with live values 
        // from Dev Center before you submit your app to the Store.
        // <Snippet2>
        InterstitialAd myInterstitialAd = null;
        string myAppId = "d25517cb-12d4-4699-8bdc-52040c712cab";
        string myAdUnitId = "test";
        // </Snippet2>

        public MainPage()
        {
            this.InitializeComponent();

            // <Snippet3>
            myInterstitialAd = new InterstitialAd();
            myInterstitialAd.AdReady += MyInterstitialAd_AdReady;
            myInterstitialAd.ErrorOccurred += MyInterstitialAd_ErrorOccurred;
            myInterstitialAd.Completed += MyInterstitialAd_Completed;
            myInterstitialAd.Cancelled += MyInterstitialAd_Cancelled;
            // </Snippet3>            
        }

        // This method requests an interstitial ad when the "Request ad" button is clicked. In a real app, 
        // you should request the interstitial ad close to when you think it will be shown, but with 
        // enough advance time to make the request and prepare the ad (say 30 seconds to a few minutes).
        // To show an interstitial banner ad instead of an interstitial video ad, replace AdType.Video 
        // with AdType.Display.
        private void requestAdButton_Click(object sender, RoutedEventArgs e)
        {
            // <Snippet4>
            myInterstitialAd.RequestAd(AdType.Video, myAppId, myAdUnitId);
            // </Snippet4>
        }

        // This method attempts to show the interstitial ad when the "Show ad" button is clicked.
        private void showAdButton_Click(object sender, RoutedEventArgs e)
        {
            // <Snippet5>
            if (InterstitialAdState.Ready == myInterstitialAd.State)
            {
                myInterstitialAd.Show();
            }
            // </Snippet5>
        }

        // <Snippet6>
        void MyInterstitialAd_AdReady(object sender, object e)
        {
            // Your code goes here.
        }

        void MyInterstitialAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            // Your code goes here.
        }

        void MyInterstitialAd_Completed(object sender, object e)
        {
            // Your code goes here.
        }

        void MyInterstitialAd_Cancelled(object sender, object e)
        {
            // Your code goes here.
        }
        // </Snippet6>
    }
}
//</CompleteSample>