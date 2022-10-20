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

// <Snippet1>
using Windows.System.UserProfile;
using Microsoft.Advertising.WinRT.UI;
using Windows.System.Profile;
// </Snippet1>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MigrateToAdControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExamplePage1 : Page
    {
        // <Snippet2>
        private const int AD_WIDTH = 320;
        private const int AD_HEIGHT = 50;
        private const int HOUSE_AD_WEIGHT = 0;
        private const int AD_REFRESH_SECONDS = 35;
        private const int MAX_ERRORS_PER_REFRESH = 3;
        private const string WAPPLICATIONID = "";
        private const string WADUNITID_PAID = "";
        private const string WADUNITID_HOUSE = "";
        private const string MAPPLICATIONID = "";
        private const string MADUNITID_PAID = "";
        private const string MADUNITID_HOUSE = "";
        private const string ADDUPLEX_APPKEY = "";
        private const string ADDUPLEX_ADUNIT = "";
        // </Snippet2>

        // <Snippet3>
        // Dispatch timer to fire at each ad refresh interval.
        private DispatcherTimer myAdRefreshTimer = new DispatcherTimer();

        // Global variables used for mediation decisions.
        private Random randomGenerator = new Random();
        private int errorCountCurrentRefresh = 0;  // Prevents infinite redirects.
        private int adDuplexWeight = 0;            // Will be set by GetAdDuplexWeight().

        // Microsoft and AdDuplex controls for banner ads.
        private AdControl myMicrosoftBanner = null;
        private AdDuplex.AdControl myAdDuplexBanner = null;

        // Application ID and ad unit ID values for Microsoft advertising. By default,
        // assign these to non-mobile ad unit info.
        private string myMicrosoftAppId = WAPPLICATIONID;
        private string myMicrosoftPaidUnitId = WADUNITID_PAID;
        private string myMicrosoftHouseUnitId = WADUNITID_HOUSE;
        // </Snippet3>

        public ExamplePage1()
        {
            this.InitializeComponent();

            // <Snippet4>
            myAdGrid.Width = AD_WIDTH;
            myAdGrid.Height = AD_HEIGHT;
            adDuplexWeight = GetAdDuplexWeight();
            RefreshBanner();

            // Start the timer to refresh the banner at the desired interval.
            myAdRefreshTimer.Interval = new TimeSpan(0, 0, AD_REFRESH_SECONDS);
            myAdRefreshTimer.Tick += myAdRefreshTimer_Tick;
            myAdRefreshTimer.Start();

            // For mobile device families, use the mobile ad unit info.
            if ("Windows.Mobile" == AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                myMicrosoftAppId = MAPPLICATIONID;
                myMicrosoftPaidUnitId = MADUNITID_PAID;
                myMicrosoftHouseUnitId = MADUNITID_HOUSE;
            }
            // </Snippet4>
        }

        // <Snippet5>
        private int GetAdDuplexWeight()
        {
            // TODO: Change this logic to fit your needs.
            // This example uses Microsoft ads first in Canada and Mexico, then
            // AdDuplex as fallback. In France, AdDuplex is first. In other regions,
            // this example uses a weighted average approach, with 50% to AdDuplex.

            int returnValue = 0;
            switch (GlobalizationPreferences.HomeGeographicRegion)
            {
                case "CA":
                case "MX":
                    returnValue = 0;
                    break;
                case "FR":
                    returnValue = 100;
                    break;
                default:
                    returnValue = 50;
                    break;
            }
            return returnValue;
        }

        private void ActivateMicrosoftBanner()
        {
            // Return if you hit the error limit for this refresh interval.
            if (errorCountCurrentRefresh >= MAX_ERRORS_PER_REFRESH)
            {
                myAdGrid.Visibility = Visibility.Collapsed;
                return;
            }

            // Use random number generator and house ads weight to determine whether
            // to use paid ads or house ads. Paid is the default. You could alternatively
            // write a method similar to GetAdDuplexWeight and override by region.
            string myAdUnit = myMicrosoftPaidUnitId;
            int houseWeight = HOUSE_AD_WEIGHT;
            int randomInt = randomGenerator.Next(0, 100);
            if (randomInt < houseWeight)
            {
                myAdUnit = myMicrosoftHouseUnitId;
            }

            // Hide the AdDuplex control if it is showing.
            if (null != myAdDuplexBanner)
            {
                myAdDuplexBanner.Visibility = Visibility.Collapsed;
            }

            // Initialize or display the Microsoft control.
            if (null == myMicrosoftBanner)
            {
                myMicrosoftBanner = new AdControl();
                myMicrosoftBanner.ApplicationId = myMicrosoftAppId;
                myMicrosoftBanner.AdUnitId = myAdUnit;
                myMicrosoftBanner.Width = AD_WIDTH;
                myMicrosoftBanner.Height = AD_HEIGHT;
                myMicrosoftBanner.IsAutoRefreshEnabled = false;

                myMicrosoftBanner.AdRefreshed += myMicrosoftBanner_AdRefreshed;
                myMicrosoftBanner.ErrorOccurred += myMicrosoftBanner_ErrorOccurred;

                myAdGrid.Children.Add(myMicrosoftBanner);
            }
            else
            {
                myMicrosoftBanner.AdUnitId = myAdUnit;
                myMicrosoftBanner.Visibility = Visibility.Visible;
                myMicrosoftBanner.Refresh();
            }
        }

        private void ActivateAdDuplexBanner()
        {
            // Return if you hit the error limit for this refresh interval.
            if (errorCountCurrentRefresh >= MAX_ERRORS_PER_REFRESH)
            {
                myAdGrid.Visibility = Visibility.Collapsed;
                return;
            }

            // Hide the Microsoft control if it is showing.
            if (null != myMicrosoftBanner)
            {
                myMicrosoftBanner.Visibility = Visibility.Collapsed;
            }

            // Initialize or display the AdDuplex control.
            if (null == myAdDuplexBanner)
            {
                myAdDuplexBanner = new AdDuplex.AdControl();
                myAdDuplexBanner.AppKey = ADDUPLEX_APPKEY;
                myAdDuplexBanner.AdUnitId = ADDUPLEX_ADUNIT;
                myAdDuplexBanner.Width = AD_WIDTH;
                myAdDuplexBanner.Height = AD_HEIGHT;
                myAdDuplexBanner.RefreshInterval = AD_REFRESH_SECONDS;

                myAdDuplexBanner.AdLoaded += myAdDuplexBanner_AdLoaded;
                myAdDuplexBanner.AdCovered += myAdDuplexBanner_AdCovered;
                myAdDuplexBanner.AdLoadingError += myAdDuplexBanner_AdLoadingError;
                myAdDuplexBanner.NoAd += myAdDuplexBanner_NoAd;

                myAdGrid.Children.Add(myAdDuplexBanner);
            }
            myAdDuplexBanner.Visibility = Visibility.Visible;
        }

        private void myAdRefreshTimer_Tick(object sender, object e)
        {
            RefreshBanner();
        }

        private void RefreshBanner()
        {
            // Reset the error counter for this refresh interval and
            // make sure the ad grid is visible.
            errorCountCurrentRefresh = 0;
            myAdGrid.Visibility = Visibility.Visible;

            // Display ad from AdDuplex.
            if (100 == adDuplexWeight)
            {
                ActivateAdDuplexBanner();
            }
            // Display Microsoft ad.
            else if (0 == adDuplexWeight)
            {
                ActivateMicrosoftBanner();
            }
            // Use weighted approach.
            else
            {
                int randomInt = randomGenerator.Next(0, 100);
                if (randomInt < adDuplexWeight) ActivateAdDuplexBanner();
                else ActivateMicrosoftBanner();
            }
        }

        private void myMicrosoftBanner_AdRefreshed(object sender, RoutedEventArgs e)
        {
            // Add your code here as necessary.
        }

        private void myMicrosoftBanner_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            errorCountCurrentRefresh++;
            ActivateAdDuplexBanner();
        }

        private void myAdDuplexBanner_AdLoaded(object sender, AdDuplex.Banners.Models.BannerAdLoadedEventArgs e)
        {
            // Add your code here as necessary.
        }

        private void myAdDuplexBanner_NoAd(object sender, AdDuplex.Common.Models.NoAdEventArgs e)
        {
            errorCountCurrentRefresh++;
            ActivateMicrosoftBanner();
        }

        private void myAdDuplexBanner_AdLoadingError(object sender, AdDuplex.Common.Models.AdLoadingErrorEventArgs e)
        {
            errorCountCurrentRefresh++;
            ActivateMicrosoftBanner();
        }

        private void myAdDuplexBanner_AdCovered(object sender, AdDuplex.Banners.Core.AdCoveredEventArgs e)
        {
            errorCountCurrentRefresh++;
            ActivateMicrosoftBanner();
        }
        // </Snippet5>
    }
}
