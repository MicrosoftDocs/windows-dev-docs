using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Windows.ApplicationModel.Store;
using Windows.Security.Authentication.Web.Core;

// This code requires the Json.NET package from Newtonsoft.

namespace GetTargetedOffersSample
{
    //<GetTargetedOffersSample>
    public class TargetedOffers
    {
        private const string storeOffersUri = "https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user";
        private const string jsonMediaType = "application/json";

        public async void GetAndClaimTargetedOffer()
        {
            // Get the Microsoft Account token for the current user.
            var msaToken = await GetMicrosoftAccountTokenAsync();

            // Get all of the targeted offers for the current user.            
            var httpClientGetOffers = new HttpClient();

            if (!string.IsNullOrEmpty(msaToken))
                httpClientGetOffers.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", msaToken);

            List<TargetedOfferData> availableOfferData = null;
            try
            {
                var getOffersResponse = await httpClientGetOffers.GetStringAsync(new Uri(storeOffersUri));
                availableOfferData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TargetedOfferData>>(getOffersResponse);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            if (availableOfferData.Count != 0)
            {
                // Get the product ID of the add-on that is associated with the first available offer 
                // in the response data.
                TargetedOfferData offerData = availableOfferData[0];
                string productId = offerData.Offers[0];

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
                        TrackingId = offerData.TrackingId,
                        Confirmation = purchaseResult.ReceiptXml
                    };

                    // Submit a request to claim the offer by sending a POST message to the 
                    // Store endpoint for targeted offers.
                    using (var httpClientClaimOffer = new HttpClient())
                    {
                        var uri = new Uri(storeOffersUri, UriKind.Absolute);

                        using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
                        {
                            if (!string.IsNullOrEmpty(msaToken))
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
        }

        // Gets the Microsoft Account token for the current user.
        // <GetMSAToken>
        private static async Task<string> GetMicrosoftAccountTokenAsync()
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
        // </GetMSAToken>
    }

    // Represents each object in the response body for the get targeted offers method.
    public class TargetedOfferData
    {
        [JsonProperty(PropertyName = "offers")]
        public IList<string> Offers { get; } = new List<string>();

        [JsonProperty(PropertyName = "trackingId")]
        public string TrackingId { get; set; }
    }

    // Represents the data in the request body for the claim targeted offer method.
    public class TargetedOfferClaim
    {
        [JsonProperty(PropertyName = "confirmation")]
        public string Confirmation { get; set; }

        [JsonProperty(PropertyName = "trackingId")]
        public string TrackingId { get; set; }
    }
    //</GetTargetedOffersSample>
}
