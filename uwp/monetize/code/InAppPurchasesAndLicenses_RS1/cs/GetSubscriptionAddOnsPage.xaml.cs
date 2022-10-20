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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InAppPurchasesAndLicenses_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetSubscriptionAddOnsPage : Page
    {
        public GetSubscriptionAddOnsPage()
        {
            this.InitializeComponent();
        }

        //<GetSubscriptions>
        private StoreContext context = null;

        public async Task GetSubscriptionsInfo()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            // Subscription add-ons are Durable products.
            string[] productKinds = { "Durable" };
            List<String> filterList = new List<string>(productKinds);

            StoreProductQueryResult queryResult =
                await context.GetAssociatedStoreProductsAsync(productKinds);

            if (queryResult.ExtendedError != null)
            {
                // The user may be offline or there might be some other server failure.
                System.Diagnostics.Debug.WriteLine($"ExtendedError: {queryResult.ExtendedError.Message}");
                return;
            }

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                // Access the Store product info for the add-on.
                StoreProduct product = item.Value;

                // For each add-on, the subscription info is available in the SKU objects in the add-on. 
                foreach (StoreSku sku in product.Skus)
                {
                    if (sku.IsSubscription)
                    {
                        // Use the sku.SubscriptionInfo property to get info about the subscription. 
                        // For example, the following code gets the units and duration of the 
                        // subscription billing period.
                        StoreDurationUnit billingPeriodUnit = sku.SubscriptionInfo.BillingPeriodUnit;
                        uint billingPeriod = sku.SubscriptionInfo.BillingPeriod;
                    }
                }
            }
        }
        //</GetSubscriptions>
    }
}
