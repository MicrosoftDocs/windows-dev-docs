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
    public sealed partial class GetUserCollectionPage : Page
    {
        public GetUserCollectionPage()
        {
            this.InitializeComponent();
        }

        //<GetUserCollection>
        private StoreContext context = null;

        public async void GetUserCollection()
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

            workingProgressRing.IsActive = true;
            StoreProductQueryResult queryResult = await context.GetUserCollectionAsync(filterList);
            workingProgressRing.IsActive = false;

            if (queryResult.ExtendedError != null)
            {
                // The user may be offline or there might be some other server failure.
                textBlock.Text = $"ExtendedError: {queryResult.ExtendedError.Message}";
                return;
            }

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                StoreProduct product = item.Value;

                // Use members of the product object to access info for the product...
            }
        }
        //</GetUserCollection>

        private void getUserCollectionButton_Click(object sender, RoutedEventArgs e)
        {
            GetUserCollection();
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
