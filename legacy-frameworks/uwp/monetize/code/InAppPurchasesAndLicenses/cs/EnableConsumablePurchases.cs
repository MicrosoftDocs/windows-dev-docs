using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace InAppPurchasesAndLicenses
{
    class EnableConsumablePurchases
    {
        async void Example1()
        {
            Guid product1TempTransactionId = new Guid();

            //<MakePurchaseRequest>
            PurchaseResults purchaseResults = await CurrentAppSimulator.RequestProductPurchaseAsync("product1");
            switch (purchaseResults.Status)
            {
                case ProductPurchaseStatus.Succeeded:
                    product1TempTransactionId = purchaseResults.TransactionId;

                    // Grant the user their purchase here, and then pass the product ID and transaction ID to
                    // CurrentAppSimulator.ReportConsumableFulfillment to indicate local fulfillment to the
                    // Windows Store.
                    break;

                case ProductPurchaseStatus.NotFulfilled:
                    product1TempTransactionId = purchaseResults.TransactionId;

                    // First check for unfulfilled purchases and grant any unfulfilled purchases from an
                    // earlier transaction. Once products are fulfilled pass the product ID and transaction ID
                    // to CurrentAppSimulator.ReportConsumableFulfillment to indicate local fulfillment to the
                    // Windows Store.
                    break;
            }
            //</MakePurchaseRequest>
        }

        
        private Dictionary<string, List<Guid>> grantedConsumableTransactionIds = 
            new Dictionary<string, List<Guid>>();

        //<GrantFeatureLocally>
        private void GrantFeatureLocally(string productId, Guid transactionId)
        {
            if (!grantedConsumableTransactionIds.ContainsKey(productId))
            {
                grantedConsumableTransactionIds.Add(productId, new List<Guid>());
            }
            grantedConsumableTransactionIds[productId].Add(transactionId);

            // Grant the user their content. You will likely increase some kind of gold/coins/some other asset count.
        }
        //</GrantFeatureLocally>

        //<IsLocallyFulfilled>
        private Boolean IsLocallyFulfilled(string productId, Guid transactionId)
        {
            return grantedConsumableTransactionIds.ContainsKey(productId) &&
                grantedConsumableTransactionIds[productId].Contains(transactionId);
        }
        //</IsLocallyFulfilled>

        private Guid product2TempTransactionId = new Guid();

        private async void Example2()
        {
            //<ReportFulfillment>
            FulfillmentResult result = await CurrentAppSimulator.ReportConsumableFulfillmentAsync(
                "product2", product2TempTransactionId);
            //</ReportFulfillment>
        }

        private IReadOnlyList<UnfulfilledConsumable> products;
        private string logMessage = string.Empty;

        //<GetUnfulfilledConsumables>
        private async void GetUnfulfilledConsumables()
        {
            products = await CurrentApp.GetUnfulfilledConsumablesAsync();

            foreach (UnfulfilledConsumable product in products)
            {
                logMessage += "\nProduct Id: " + product.ProductId + " Transaction Id: " + product.TransactionId;
                // This is where you would pass the product ID and transaction ID to
                // currentAppSimulator.reportConsumableFulfillment to indicate local fulfillment to the Windows Store.
            }
        }
        //</GetUnfulfilledConsumables>
    }
}
