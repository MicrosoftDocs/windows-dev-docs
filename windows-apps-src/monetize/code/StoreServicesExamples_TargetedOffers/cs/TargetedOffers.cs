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

        public async void DemonstrateTargetedOffers()
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

            if (availableOfferData == null || availableOfferData.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("There was an error retrieving targeted offers," +
                    "or there are no targeted offers available for the current user.");
                return;
            }

            // Get the product ID of the add-on that is associated with the first available offer
            // in the response data.
            TargetedOfferData offerData = availableOfferData[0];
            string productId = offerData.Offers[0];

            // Get the Store ID of the add-on that has the matching product ID, and then purchase the add-on.
            List<String> filterList = new List<string>(productKinds);
            StoreProductQueryResult queryResult = await storeContext.GetAssociatedStoreProductsAsync(filterList);
            foreach (KeyValuePair<string, StoreProduct> result in queryResult.Products)
            {
                if (result.Value.InAppOfferToken == productId)
                {
                    await PurchaseOfferAsync(result.Value.StoreId);
                    return;
                }
            }

            System.Diagnostics.Debug.WriteLine("No add-on with the specified product ID could be found " +
                "for the current app.");
            return;
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

        private async Task PurchaseOfferAsync(string storeId)
        {
            if (string.IsNullOrEmpty(storeId))
            {
                System.Diagnostics.Debug.WriteLine("storeId is null or empty.");
                return;
            }

            // Purchase the add-on for the current user. Typically, a game or app would first show
            // a UI that prompts the user to buy the add-on; for simplicity, this example
            // simply purchases the add-on.
            StorePurchaseResult result = await storeContext.RequestPurchaseAsync(storeId);

            // Capture the error message for the purchase operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    System.Diagnostics.Debug.WriteLine("The user has already purchased the product.");
                    break;

                case StorePurchaseStatus.Succeeded:
                    System.Diagnostics.Debug.WriteLine("The purchase was successful.");
                    break;

                case StorePurchaseStatus.NotPurchased:
                    System.Diagnostics.Debug.WriteLine("The purchase did not complete. " +
                        "The user may have cancelled the purchase. ExtendedError: " + extendedError);
                    break;

                case StorePurchaseStatus.NetworkError:
                    System.Diagnostics.Debug.WriteLine("The purchase was unsuccessful due to a network error. " +
                        "ExtendedError: " + extendedError);
                    break;

                case StorePurchaseStatus.ServerError:
                    System.Diagnostics.Debug.WriteLine("The purchase was unsuccessful due to a server error. " +
                        "ExtendedError: " + extendedError);
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("The purchase was unsuccessful due to an unknown error. " +
                        "ExtendedError: " + extendedError);
                    break;
            }
        }
    }

    public class TargetedOfferData
    {
        [JsonProperty(PropertyName = "offers")]
        public IList<string> Offers { get; } = new List<string>();

        [JsonProperty(PropertyName = "trackingId")]
        public string TrackingId { get; set; }
    }
}
//</GetTargetedOffersSample>