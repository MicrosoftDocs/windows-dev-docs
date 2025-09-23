//<DevCenterAccessTokenClient>
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevCenterApiSample
{
    /// <summary>
    /// A client for getting access tokens to the Dev Center API.
    /// </summary>
    public class DevCenterAccessTokenClient
    {
        private string _tenantId;
        private string _clientId;
        private string _clientSecret;

        /// <summary>
        /// Creates a new instance of the <see cref="DevCenterAccessTokenClient"/> class.
        /// </summary>
        /// <param name="tenantId">The AAD tenant ID.</param>
        /// <param name="clientId">The AAD client ID.</param>
        /// <param name="clientSecret">The AAD client secret.</param>
        public DevCenterAccessTokenClient(string tenantId, string clientId, string clientSecret)
        {
            _tenantId = tenantId;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <summary>
        /// Generates an access token to the specified resource URI.
        /// </summary>
        /// <param name="resource">The resource URI.</param>
        /// <returns>An access token for authentication.</returns>
        public string GetAccessToken(string resource)
        {
            // Generate access token. Access token is valid for 1 hour. Regenerate access token when needed
            HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"https://login.microsoftonline.com/{_tenantId}/oauth2/token");
            string tokenRequestBody = $"grant_type=client_credentials&client_id={_clientId}&client_secret={_clientSecret}&resource={resource}";
            tokenRequest.Content = new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.SendAsync(tokenRequest).GetAwaiter().GetResult();
            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            JObject responseJson = (JObject)JsonConvert.DeserializeObject(responseBody);

            tokenRequest.Dispose();
            client.Dispose();
            response.Dispose();

            return responseJson["access_token"].Value<string>() ?? string.Empty;
        }
    }
}
//</DevCenterAccessTokenClient>
