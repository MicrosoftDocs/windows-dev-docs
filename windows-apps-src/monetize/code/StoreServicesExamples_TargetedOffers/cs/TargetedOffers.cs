//<GetTargetedOffersSample>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.ApplicationModel.Store;
using Windows.Services.Store;
using Windows.Security.Authentication.Web.Core;

namespace DocumenationExamples
{
    public class TargetedOffersExample
    {
        private const string storeOffersUri = "https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user";
        private const string jsonMediaType = "application/json";
        private static string[] productKinds = { "Durable", "Consumable", "UnmanagedConsumable" };
        private static StoreContext storeContext = StoreContext.GetDefault();

        public async void GetAndClaimTargetedOffer()
        {
            // Get the Microsoft Account token for the current user.
            string msaToken = await GetMicrosoftAccountTokenAsync();

            if (string.IsNullOrEmpty(msaToken))
            {
                System.Diagnostics.Debug.WriteLine("Microsoft Account token could not be retrieved.");
                return;
            }

            // Get the targeted Store offers for the current user.
            List<TargetedOfferData> availableOfferData =
                await GetTargetedOffersForUserAsync(msaToken);

            if (availableOfferData == null || availableOfferData.Count != 0)
            {
                System.Diagnostics.Debug.WriteLine("There was an error retrieving targeted offers," +
                    "or there are no targeted offers available for the current user.");
                return;
            }

            // Get the product ID of the add-on that is associated with the first available offer
            // in the response data.
            TargetedOfferData offerData = availableOfferData[0];
            string productId = offerData.Offers[0];

            // Get and claim a targeted offer for the current user.
            // If the Windows.Services.Store APIs are available (the app is running
            // on Windows 10, version 1607, or later), use those APIs.
            // Otherwise, use the Windows.ApplicationMoel.Store APIs.
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(
                "Windows.Services.Store.StoreContext"))
            {
                await ClaimOfferOnWindows1607AndLaterAsync(productId, offerData, msaToken);
            }
            else
            {
                await ClaimOfferOnAnyVersionWindows10Async(productId, offerData, msaToken);
            }
        }

        //<GetMSAToken>
        private async Task<string> GetMicrosoftAccountTokenAsync()
        {
            var msaProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync(
                "https://login.microsoft.com", "consumers");

            WebTokenRequest request = new WebTokenRequest(msaProvider, "devcenter_implicit.basic,wl.basic");
            WebTokenRequestResult result = await WebAuthenticationCoreManager.RequestTokenAsync(request);

            if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                return result.ResponseData[0].Token;
            }
            else
            {
                return string.Empty;
            }
        }
        //</GetMSAToken>

        //<GetTargetedOffers>
        private async Task<List<TargetedOfferData>> GetTargetedOffersForUserAsync(string msaToken)
        {
            if (string.IsNullOrEmpty(msaToken))
            {
                System.Diagnostics.Debug.WriteLine("Microsoft Account token is null or empty.");
                return null;
            }

            HttpClient httpClientGetOffers = new HttpClient();
            httpClientGetOffers.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", msaToken);
            List<TargetedOfferData> availableOfferData = null;

            try
            {
                string getOffersResponse = await httpClientGetOffers.GetStringAsync(new Uri(storeOffersUri));
                availableOfferData = 
                    Newtonsoft.Json.JsonConvert.DeserializeObject<List<TargetedOfferData>>(getOffersResponse);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return availableOfferData;
        }
        //</GetTargetedOffers>

        // This method uses the Windows.Services.Store APIs to purchase and claim a targeted offer.
        // Only call this method if the app is running on Windows 10, version 1607, or later.
        //<ClaimOfferOnWindows1607AndLater>
        private async Task ClaimOfferOnWindows1607AndLaterAsync(
            string productId, TargetedOfferData offerData, string msaToken)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(msaToken))
            {
                System.Diagnostics.Debug.WriteLine("Product ID or Microsoft Account token is null or empty.");
                return;
            }

            // Purchase the add-on for the current user. Typically, a game or app would first show
            // a UI that prompts the user to buy the add-on; for simplicity, this example
            // simply purchases the add-on.
            StorePurchaseResult result = await storeContext.RequestPurchaseAsync(productId);

            if (result.Status == StorePurchaseStatus.Succeeded)
            {
                // Get the StoreProduct in the user's collection that matches the targeted offer.
                List<String> filterList = new List<string>(productKinds);
                StoreProductQueryResult queryResult = await storeContext.GetUserCollectionAsync(filterList);
                KeyValuePair<string, StoreProduct> offer = 
                    queryResult.Products.Where(p => p.Key == productId).SingleOrDefault();
                
                if (offer == null)
                {
                    System.Diagnostics.Debug.WriteLine("No StoreProduct with the specified product ID could be found.");
                    return;
                }

                StoreProduct product = offer.Value;

                // Parse the JSON string returned by StoreProduct.ExtendedJsonData to get the order ID.
                string extendedJsonData = product.ExtendedJsonData;
                string skuAvailability =
                    JObject.Parse(extendedJsonData)["DisplaySkuAvailabilities"].FirstOrDefault().ToString();
                string sku = JObject.Parse(skuAvailability)["Sku"].ToString();
                string collectionData = JObject.Parse(sku)["CollectionData"].ToString();
                string orderId = JObject.Parse(collectionData)["orderId"].ToString();

                var claim = new TargetedOfferClaim
                {
                    StoreOffer = offerData,
                    Receipt = orderId
                };

                // Submit a request to claim the offer by sending a POST message to the
                // Store endpoint for targeted offers.
                using (var httpClientClaimOffer = new HttpClient())
                {
                    var uri = new Uri(storeOffersUri, UriKind.Absolute);

                    using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", msaToken);

                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(claim),
                            Encoding.UTF8,
                            jsonMediaType);

                        using (var response = await httpClientClaimOffer.SendAsync(request))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
            }
        }
        //</ClaimOfferOnWindows1607AndLater>

        // This method uses the Windows.ApplicationModel.Store APIs to purchase and claim a targeted offer.
        // This method can be used on any version of Windows 10.
        //<ClaimOfferOnAnyVersionWindows10>
        private async Task ClaimOfferOnAnyVersionWindows10Async(
            string productId, TargetedOfferData offerData, string msaToken)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(msaToken))
            {
                System.Diagnostics.Debug.WriteLine("Product ID or Microsoft Account token is null or empty.");
                return;
            }

            // Purchase the add-on for the current user and report it to the Store as fulfilled
            // if the purchase was successful. Typically, a game or app would first show
            // a UI that prompts the user to buy the add-on; for simplicity, this example
            // simply purchases the add-on.
            var purchaseResult = await CurrentApp.RequestProductPurchaseAsync(productId);
            if (purchaseResult.Status == ProductPurchaseStatus.Succeeded)
            {
                CurrentApp.ReportProductFulfillment(productId);
                var claim = new TargetedOfferClaim
                {
                    StoreOffer = offerData,
                    Receipt = purchaseResult.ReceiptXml
                };

                // Submit a request to claim the offer by sending a POST message to the
                // Store endpoint for targeted offers.
                using (var httpClientClaimOffer = new HttpClient())
                {
                    var uri = new Uri(storeOffersUri, UriKind.Absolute);

                    using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", msaToken);

                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(claim),
                            Encoding.UTF8,
                            jsonMediaType);

                        using (var response = await httpClientClaimOffer.SendAsync(request))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
            }
        }
        //</ClaimOfferOnAnyVersionWindows10>
    }
}

public class TargetedOfferData
{
    [JsonProperty(PropertyName = "offers")]
    public IList<string> Offers { get; } = new List<string>();

    [JsonProperty(PropertyName = "trackingId")]
    public string TrackingId { get; set; }
}

public class TargetedOfferClaim
{
    [JsonProperty(PropertyName = "receipt")]
    public string Receipt { get; set; }

    [JsonProperty(PropertyName = "storeOffer")]
    public TargetedOfferData StoreOffer { get; set; }
}
//</GetTargetedOffersSample>