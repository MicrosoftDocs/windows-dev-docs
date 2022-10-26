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
    public sealed partial class GetProductInfoPage : Page
    {
        public GetProductInfoPage()
        {
            this.InitializeComponent();
        }

        //<GetProductInfo>
        private StoreContext context = null;

        public async void GetProductInfo()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            // Specify the kinds of add-ons to retrieve.
            string[] productKinds = { "Durable" };
            List<String> filterList = new List<string>(productKinds);

            // Specify the Store IDs of the products to retrieve.
            string[] storeIds = new string[] { "9NBLGGH4TNMP", "9NBLGGH4TNMN" };

            workingProgressRing.IsActive = true;
            StoreProductQueryResult queryResult =
                await context.GetStoreProductsAsync(filterList, storeIds);
            workingProgressRing.IsActive = false;

            if (queryResult.ExtendedError != null)
            {
                // The user may be offline or there might be some other server failure.
                textBlock.Text = $"ExtendedError: {queryResult.ExtendedError.Message}";
                return;
            }

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                // Access the Store info for the product.
                StoreProduct product = item.Value;

                // Use members of the product object to access info for the product...
            }
        }
        //</GetProductInfo>

        private void getProductInfoButton_Click(object sender, RoutedEventArgs e)
        {
            GetProductInfo();
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
