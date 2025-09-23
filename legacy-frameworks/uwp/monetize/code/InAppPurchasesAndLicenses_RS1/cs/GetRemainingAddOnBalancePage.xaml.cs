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
    public sealed partial class GetRemainingAddOnBalancePage : Page
    {
        public GetRemainingAddOnBalancePage()
        {
            this.InitializeComponent();
        }

        //<GetRemainingAddOnBalance>
        private StoreContext context = null;

        public async void GetRemainingBalance(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            string addOnStoreId = "9NBLGGH4TNNR";

            workingProgressRing.IsActive = true;
            StoreConsumableResult result = await context.GetConsumableBalanceRemainingAsync(addOnStoreId);
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
                    textBlock.Text = "Remaining balance: " + result.BalanceRemaining;
                    break;

                case StoreConsumableStatus.NetworkError:
                    textBlock.Text = "Could not retrieve balance due to a network error. " +
                        "ExtendedError: " + extendedError;
                    break;

                case StoreConsumableStatus.ServerError:
                    textBlock.Text = "Could not retrieve balance due to a server error. " +
                        "ExtendedError: " + extendedError;
                    break;

                default:
                    textBlock.Text = "Could not retrieve balance due to an unknown error. " +
                        "ExtendedError: " + extendedError;
                    break;
            }
        }
        //</GetRemainingAddOnBalance>

        private void getRemainingBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            GetRemainingBalance("test");
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
