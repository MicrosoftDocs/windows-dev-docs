using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace InAppPurchasesAndLicenses
{
    class ManageCatalog
    {
        private async void MakePurchaseRequest()
        {
            //<MakePurchaseRequest>
            string offerId = "1234";
            string displayPropertiesName = "MusicOffer1";
            var displayProperties = new ProductPurchaseDisplayProperties(displayPropertiesName);

            try
            {
                PurchaseResults purchaseResults = await CurrentAppSimulator.RequestProductPurchaseAsync(
                    "product1", offerId, displayProperties);
                switch (purchaseResults.Status)
                {
                    case ProductPurchaseStatus.Succeeded:
                        // Grant the user their purchase here, and then pass the product ID and transaction ID
                        // to currentAppSimulator.reportConsumableFulfillment to indicate local fulfillment to
                        // the Windows Store.
                        break;
                    case ProductPurchaseStatus.NotFulfilled:
                        // First check for unfulfilled purchases and grant any unfulfilled purchases from an
                        // earlier transaction. Once products are fulfilled pass the product ID and transaction
                        // ID to currentAppSimulator.reportConsumableFulfillment to indicate local fulfillment
                        // to the Windows Store.
                        break;
                    case ProductPurchaseStatus.NotPurchased:
                        // Notify user that the purchase was not completed due to cancellation or error.
                        break;
                }
            }
            catch (Exception)
            {
                // Notify the user of the purchase error.
            }
            //</MakePurchaseRequest>
        }

        private async void ReportFulfillment()
        {
            ListingInformation listing = await CurrentAppSimulator.LoadListingInformationAsync();
            var product1 = listing.ProductListings["product1"];
            string product1ListingName = string.Empty;
            string productId = product1.ProductId;
            Guid transactionId = new Guid();

            //<ReportFulfillment>
            string offerId = "1234";
            product1ListingName = product1.Name;
            string displayPropertiesName = "MusicOffer1";

            if (String.IsNullOrEmpty(displayPropertiesName))
            {
                displayPropertiesName = product1ListingName;
            }
            var offerIdMsg = " with offer id " + offerId;
            if (String.IsNullOrEmpty(offerId))
            {
                offerIdMsg = " with no offer id";
            }

            FulfillmentResult result = await CurrentAppSimulator.ReportConsumableFulfillmentAsync(productId, transactionId);
            switch (result)
            {
                case FulfillmentResult.Succeeded:
                    Log("You bought and fulfilled " + displayPropertiesName + offerIdMsg);
                    break;
                case FulfillmentResult.NothingToFulfill:
                    Log("There is no purchased product 1 to fulfill.");
                    break;
                case FulfillmentResult.PurchasePending:
                    Log("You bought product 1. The purchase is pending so we cannot fulfill the product.");
                    break;
                case FulfillmentResult.PurchaseReverted:
                    Log("You bought product 1. But your purchase has been reverted.");
                    // Since the user' s purchase was revoked, they got their money back.
                    // You may want to revoke the user' s access to the consumable content that was granted.
                    break;
                case FulfillmentResult.ServerError:
                    Log("You bought product 1. There was an error when fulfilling.");
                    break;
            }
            //</ReportFulfillment>
        }

        private void Log(string message)
        {

        }
    }
}
