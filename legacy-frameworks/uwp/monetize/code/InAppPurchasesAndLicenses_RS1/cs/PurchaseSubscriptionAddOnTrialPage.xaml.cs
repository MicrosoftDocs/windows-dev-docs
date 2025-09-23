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
using System.Threading.Tasks;

namespace InAppPurchasesAndLicenses_UWP
{

    public sealed partial class PurchaseSubscriptionAddOnTrialPage : Page
    {
        public PurchaseSubscriptionAddOnTrialPage()
        {
            this.InitializeComponent();
        }

        //<PurchaseTrialSubscription>
        private StoreContext context = null;
        StoreProduct subscriptionStoreProduct;

        // Assign this variable to the Store ID of your subscription add-on.
        private string subscriptionStoreId = "";  

        // This is the entry point method for the example.
        public async Task SetupSubscriptionInfoAsync()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            bool userOwnsSubscription = await CheckIfUserHasSubscriptionAsync();
            if (userOwnsSubscription)
            {
                // Unlock all the subscription add-on features here.
                return;
            }

            // Get the StoreProduct that represents the subscription add-on.
            subscriptionStoreProduct = await GetSubscriptionProductAsync();
            if (subscriptionStoreProduct == null)
            {
                return;
            }

            // Check if the first SKU is a trial and notify the customer that a trial is available.
            // If a trial is available, the Skus array will always have 2 purchasable SKUs and the
            // first one is the trial. Otherwise, this array will only have one SKU.
            StoreSku sku = subscriptionStoreProduct.Skus[0];
            if (sku.SubscriptionInfo.HasTrialPeriod)
            {
                // You can display the subscription trial info to the customer here. You can use 
                // sku.SubscriptionInfo.TrialPeriod and sku.SubscriptionInfo.TrialPeriodUnit 
                // to get the trial details.
            }
            else
            {
                // You can display the subscription purchase info to the customer here. You can use 
                // sku.SubscriptionInfo.BillingPeriod and sku.SubscriptionInfo.BillingPeriodUnit
                // to provide the renewal details.
            }

            // Prompt the customer to purchase the subscription.
            await PromptUserToPurchaseAsync();
        }

        private async Task<bool> CheckIfUserHasSubscriptionAsync()
        {
            StoreAppLicense appLicense = await context.GetAppLicenseAsync();

            // Check if the customer has the rights to the subscription.
            foreach (var addOnLicense in appLicense.AddOnLicenses)
            {
                StoreLicense license = addOnLicense.Value;
                if (license.SkuStoreId.StartsWith(subscriptionStoreId))
                {
                    if (license.IsActive)
                    {
                        // The expiration date is available in the license.ExpirationDate property.
                        return true;
                    }
                }
            }

            // The customer does not have a license to the subscription.
            return false;
        }

        private async Task<StoreProduct> GetSubscriptionProductAsync()
        {
            // Load the sellable add-ons for this app and check if the trial is still 
            // available for this customer. If they previously acquired a trial they won't 
            // be able to get a trial again, and the StoreProduct.Skus property will 
            // only contain one SKU.
            StoreProductQueryResult result =
                await context.GetAssociatedStoreProductsAsync(new string[] { "Durable" });

            if (result.ExtendedError != null)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong while getting the add-ons. " +
                    "ExtendedError:" + result.ExtendedError);
                return null;
            }

            // Look for the product that represents the subscription.
            foreach (var item in result.Products)
            {
                StoreProduct product = item.Value;
                if (product.StoreId == subscriptionStoreId)
                {
                    return product;
                }
            }

            System.Diagnostics.Debug.WriteLine("The subscription was not found.");
            return null;
        }

        private async Task PromptUserToPurchaseAsync()
        {
            // Request a purchase of the subscription product. If a trial is available it will be offered 
            // to the customer. Otherwise, the non-trial SKU will be offered.
            StorePurchaseResult result = await subscriptionStoreProduct.RequestPurchaseAsync();

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    // Show a UI to acknowledge that the customer has purchased your subscription 
                    // and unlock the features of the subscription. 
                    break;

                case StorePurchaseStatus.NotPurchased:
                    System.Diagnostics.Debug.WriteLine("The purchase did not complete. " +
                        "The customer may have cancelled the purchase. ExtendedError: " + extendedError);
                    break;

                case StorePurchaseStatus.ServerError:
                case StorePurchaseStatus.NetworkError:
                    System.Diagnostics.Debug.WriteLine("The purchase was unsuccessful due to a server or network error. " +
                        "ExtendedError: " + extendedError);
                    break;

                case StorePurchaseStatus.AlreadyPurchased:
                    System.Diagnostics.Debug.WriteLine("The customer already owns this subscription." +
                            "ExtendedError: " + extendedError);
                    break;
            }
        }
        //</PurchaseTrialSubscription>
    }
}
