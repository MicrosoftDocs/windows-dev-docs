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
using Windows.Services.Store;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace InAppPurchasesAndLicenses_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetAppInfoPage : Page
    {
        public GetAppInfoPage()
        {
            this.InitializeComponent();
            
        }

        //<GetAppInfo>
        private StoreContext context = null;

        public async void GetAppInfo()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            // Get app store product details. Because this might take several moments,   
            // display a ProgressRing during the operation.
            workingProgressRing.IsActive = true;
            StoreProductResult queryResult = await context.GetStoreProductForCurrentAppAsync();
            workingProgressRing.IsActive = false;

            if (queryResult.Product == null)
            {
                // The Store catalog returned an unexpected result.
                textBlock.Text = "Something went wrong, and the product was not returned.";

                // Show additional error info if it is available.
                if (queryResult.ExtendedError != null)
                {
                    textBlock.Text += $"\nExtendedError: {queryResult.ExtendedError.Message}";
                }

                return;
            }

            // Display the price of the app.
            textBlock.Text = $"The price of this app is: {queryResult.Product.Price.FormattedBasePrice}";
        }
        //</GetAppInfo>

        private void getAppInfoButton_Click(object sender, RoutedEventArgs e)
        {
            GetAppInfo();
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
