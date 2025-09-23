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
using Microsoft.Advertising.WinRT.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdControlSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            myAdControl.ErrorOccurred += OnAdError;
            myAdControl.AdRefreshed += OnAdRefresh;
            myAdControl.IsEngagedChanged += OnAdEngagedChanged;
        }

        //<EventHandlers>
        private void OnAdError(object sender, AdErrorEventArgs e)
        {
            // Add code to gracefully handle errors that occurred while serving an ad.
            // For example, you may opt to show a default experience, or reclaim the grid 
            // display area for other purposes.
            return;
        }

        private void OnAdRefresh(object sender, RoutedEventArgs e)
        {
            // Add code here that you wish to execute when the ad refreshes.
            return;
        }

        private void OnAdEngagedChanged(object sender, RoutedEventArgs e)
        {
            AdControl control = sender as AdControl;
            if (true == control.IsEngaged)
            {
                // Add code here to change behavior while the user engaged with ad.
                // For example, if the app is a game, you might pause the game.
            }
            else
            {
                // Add code here to update app behavior after the user is no longer
                // engaged with the ad. For example, you might unpause a game. 
            }

            return;
        }
        //</EventHandlers>
    }
}
