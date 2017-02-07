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
    public sealed partial class PurchaseAddOnPage : Page
    {
        public PurchaseAddOnPage()
        {
            this.InitializeComponent();
        }

        //<PurchaseAddOn>
        private StoreContext context = null;

        public async void PurchaseAddOn(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            workingProgressRing.IsActive = true;
            StorePurchaseResult result = await context.RequestPurchaseAsync(storeId);
            workingProgressRing.IsActive = false;

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    textBlock.Text = "The user has already purchased the product.";
                    break;

                case StorePurchaseStatus.Succeeded:
                    textBlock.Text = "The purchase was successful.";
                    break;

                case StorePurchaseStatus.NotPurchased:
                    textBlock.Text = "The purchase did not complete. " +
                        "The user may have cancelled the purchase. ExtendedError: " + extendedError;
                    break;

                case StorePurchaseStatus.NetworkError:
                    textBlock.Text = "The purchase was unsuccessful due to a network error. " +
                        "ExtendedError: " + extendedError;
                    break;

                case StorePurchaseStatus.ServerError:
                    textBlock.Text = "The purchase was unsuccessful due to a server error. " +
                        "ExtendedError: " + extendedError;
                    break;

                default:
                    textBlock.Text = "The purchase was unsuccessful due to an unknown error. " +
                        "ExtendedError: " + extendedError;
                    break;
            }
        }
        //</PurchaseAddOn>

        private void purchaseAddOn_Click(object sender, RoutedEventArgs e)
        {
            PurchaseAddOn("test");
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
