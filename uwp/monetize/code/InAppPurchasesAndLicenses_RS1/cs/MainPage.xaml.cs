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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InAppPurchasesAndLicenses_UWP
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void launchGetAppInfoPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetAppInfoPage));
        }

        private void launchGetAddOnInfoPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetAddOnInfoPage));
        }

        private void launchGetProductInfoPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetProductInfoPage));
        }

        private void launchGetUserCollectionPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetUserCollectionPage));
        }

        private void launchGetLicenseInfoPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetLicenseInfoPage));
        }

        private void launchPurchaseAddOnPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PurchaseAddOnPage));
        }

        private void launchConsumeAddOnPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ConsumeAddOnPage));
        }

        private void launchGetRemainingAddOnBalancePageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetRemainingAddOnBalancePage));
        }

        private void launchImplementTrialPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImplementTrialPage));
        }
    }
}
