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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InAppPurchasesAndLicenses_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchaseSubscriptionAddOnPage : Page
    {
        public PurchaseSubscriptionAddOnPage()
        {
            this.InitializeComponent();
        }

        //<PurchaseSubscription>
        private StoreContext context = null;

        public async void PurchaseSubscription(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            // First, get the StoreProduct object for the subscription add-on. This example
            // assumes you already know the Store ID for the add-on and you have passed
            // it to this method.
            string[] productKinds = { "Durable" };
            List<String> filterList = new List<string>(productKinds);
            string[] storeIds = new string[] { storeId };

            workingProgressRing.IsActive = true;
            StoreProductQueryResult queryResult =
                await context.GetStoreProductsAsync(filterList, storeIds);
            workingProgressRing.IsActive = false;

            StoreProduct mySubscription = queryResult.Products.FirstOrDefault().Value;

            // Make sure the user has not already acquired the subscription add-on, then 
            // offer it for purchase to the user.
            if (!mySubscription.IsInUserCollection)
            {
                workingProgressRing.IsActive = true;
                StorePurchaseResult result = await mySubscription.RequestPurchaseAsync();
                workingProgressRing.IsActive = false;

                // Capture the error message for the operation, if any.
                string extendedError = string.Empty;
                if (result.ExtendedError != null)
                {
                    extendedError = result.ExtendedError.Message;
                }

                switch (result.Status)
                {
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
        }
        //</PurchaseSubscription>
    }
}
