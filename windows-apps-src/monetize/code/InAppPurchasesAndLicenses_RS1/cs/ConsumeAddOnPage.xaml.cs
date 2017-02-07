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
    public sealed partial class ConsumeAddOnPage : Page
    {
        public ConsumeAddOnPage()
        {
            this.InitializeComponent();
        }

        //<ConsumeAddOn>
        private StoreContext context = null;

        public async void ConsumeAddOn(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            // This is an example for a Store-managed consumable, where you specify the actual number
            // of units that you want to report as consumed so the Store can update the remaining
            // balance. For a developer-managed consumable where you maintain the balance, specify 1
            // to just report the add-on as fulfilled to the Store.
            uint quantity = 10;
            string addOnStoreId = "9NBLGGH4TNNR";
            Guid trackingId = Guid.NewGuid();

            workingProgressRing.IsActive = true;
            StoreConsumableResult result = await context.ReportConsumableFulfillmentAsync(
                addOnStoreId, quantity, trackingId);
            workingProgressRing.IsActive = false;

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }

            switch (result.Status)
            {
                case StoreConsumableStatus.Succeeded:
                    textBlock.Text = "The fulfillment was successful. " + 
                        $"Remaining balance: {result.BalanceRemaining}";
                    break;

                case StoreConsumableStatus.InsufficentQuantity:
                    textBlock.Text = "The fulfillment was unsuccessful because the remaining " +
                        $"balance is insufficient. Remaining balance: {result.BalanceRemaining}";
                    break;

                case StoreConsumableStatus.NetworkError:
                    textBlock.Text = "The fulfillment was unsuccessful due to a network error. " +
                        "ExtendedError: " + extendedError;
                    break;

                case StoreConsumableStatus.ServerError:
                    textBlock.Text = "The fulfillment was unsuccessful due to a server error. " +
                        "ExtendedError: " + extendedError;
                    break;

                default:
                    textBlock.Text = "The fulfillment was unsuccessful due to an unknown error. " +
                        "ExtendedError: " + extendedError;
                    break;
            }
        }
        //</ConsumeAddOn>

        private void consumeAddOnButton_Click(object sender, RoutedEventArgs e)
        {
            ConsumeAddOn("test");
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
