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
using Microsoft.Advertising.WinRT.UI;
using Windows.System.Profile;
// </Snippet1>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MigrateToAdControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // <Snippet2>
        private const int AD_WIDTH = 320;
        private const int AD_HEIGHT = 50;
        private const string WAPPLICATIONID = "";
        private const string WADUNITID = "";
        private const string MAPPLICATIONID = "";
        private const string MADUNITID = "";
        // </Snippet2>

        // <Snippet3>
        // Declare an AdControl.
        private AdControl myAdControl = null;

        // Application ID and ad unit ID values for Microsoft advertising. By default,
        // assign these to non-mobile ad unit info.
        private string myAppId = WAPPLICATIONID;
        private string myAdUnitId = WADUNITID;
        // </Snippet3>


        public MainPage()
        {
            this.InitializeComponent();

            // <Snippet4>
            myAdGrid.Width = AD_WIDTH;
            myAdGrid.Height = AD_HEIGHT;

            // For mobile device families, use the mobile ad unit info.
            if ("Windows.Mobile" == AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                myAppId = MAPPLICATIONID;
                myAdUnitId = MADUNITID;
            }

            // Initialize the AdControl.
            myAdControl = new AdControl();
            myAdControl.ApplicationId = myAppId;
            myAdControl.AdUnitId = myAdUnitId;
            myAdControl.Width = AD_WIDTH;
            myAdControl.Height = AD_HEIGHT;
            myAdControl.IsAutoRefreshEnabled = true;

            myAdGrid.Children.Add(myAdControl);
            // </Snippet4>

        }
    }
}
