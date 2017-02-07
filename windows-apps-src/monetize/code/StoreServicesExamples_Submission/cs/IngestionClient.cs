//<IngestionClient>
namespace DeveloperApiCSharpSample
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// This class is a proxy that abstracts the functionality of the API service
    /// </summary>
    public class IngestionClient : IDisposable
    {
        public static readonly string Version = "1.0";
        public static readonly string Tenant = "my";

        public static readonly string GetSubmissionUrlTemplate = "/v{0}/{1}/applications/{2}/submissions/{3}";
        public static readonly string CommitSubmissionUrlTemplate = "/v{0}/{1}/applications/{2}/submissions/{3}/commit";
        public static readonly string UpdateUrlTemplate = "/v{0}/{1}/applications/{2}/submissions/{3}/";
        public static readonly string ApplicationUrl = "/v{0}/{1}/applications";
        public static readonly string ApplicationUrlWithContinuation = "/v{0}/{1}/{2}";
        public static readonly string GetApplicationUrlTemplate = "/v{0}/{1}/applications/{2}";
        public static readonly string GetApplicationIapsWithContinuationUrlTemplate = "/v{0}/{1}/{2}";
        public static readonly string CreateSubmissionUrlTemplate = "/v{0}/{1}/applications/{2}/submissions";

        public static readonly string GetApplicationIapsUrlTemplate = "/v{0}/{1}/applications/{2}/listinappproducts";
        public static readonly string CreateInAppUrlTemplate = "/v{0}/{1}/inappproducts";
        public static readonly string GetInAppUrlTemplate = "/v{0}/{1}/inappproducts/{2}";
        public static readonly string InAppSubmissionUrlTemplate = "/v{0}/{1}/inappproducts/{2}/submissions/{3}";
        public static readonly string InAppSubmissionUrl = "/v{0}/{1}/inappproducts/{2}/submissions";
        public static readonly string InAppProductCommitSubmissionUrlTemplate = "/v{0}/{1}/inappproducts/{2}/submissions/{3}/commit";

        public static readonly string GetApplicationFlightsUrlTemplate = "/v{0}/{1}/applications/{2}/listflights";
        public static readonly string GetApplicationFlightsWithContinuationUrlTemplate = "/v{0}/{1}/{2}";
        public static readonly string CreateNewFlightUrlTemplate = "/v{0}/{1}/applications/{2}/flights";
        public static readonly string GetFlightUrlTemplate = "/v{0}/{1}/applications/{2}/flights/{3}";
        public static readonly string CreateFlightSubmissionUrlTemplate = "/v{0}/{1}/applications/{2}/flights/{3}/submissions";
        public static readonly string GetFlightSubmissionUrlTemplate = "/v{0}/{1}/applications/{2}/flights/{3}/submissions/{4}";
        public static readonly string CommitFlightSubmissionUrlTemplate = "/v{0}/{1}/applications/{2}/flights/{3}/submissions/{4}/commit";

        public static readonly string FlightSubmissionStatusUrlTemplate = "/v{0}/{1}/applications/{2}/flights/{3}/submissions/{4}/status";
        public static readonly string ApplicationSubmissionStatusUrlTemplate = "/v{0}/{1}/applications/{2}/submissions/{3}/status";
        public static readonly string InAppSubmissionStatusUrlTemplate = "/v{0}/{1}/inappproducts/{2}/submissions/{3}/status";

        private HttpClient httpClient;
        private readonly string accessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="IngestionClient" /> class.
        /// </summary>
        /// <param name="accessToken">
        /// The acces token. This is JWT a token obtained from AAD allowing the caller to invoke the API
        /// on behalf of a user
        /// </param>
        /// <param name="serviceUrl">The service URL.</param>
        public IngestionClient(string accessToken, string serviceUrl)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException("accessToken");
            }

            if (string.IsNullOrEmpty(serviceUrl))
            {
                throw new ArgumentNullException("serviceUrl");
            }

            this.accessToken = accessToken;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(serviceUrl)
            };
            this.DefaultHeaders = new Dictionary<string, string>();
        }


        /// <summary>
        /// Gets the default headers.
        /// </summary>
        public Dictionary<string, string> DefaultHeaders { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.httpClient != null)
            {
                this.httpClient.Dispose();
                this.httpClient = null;
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Gets the authorization token for the provided client id, client secret, and the scope.
        /// This token is usually valid for 1 hour, so if your submission takes longer than that to complete,
        /// make sure to get a new one periodically.
        /// </summary>
        /// <param name="tokenEndpoint">Token endpoint to which the request is to be made. Specific to your
        /// AAD app. Example: https://login.windows.net/d454d300-128e-2d81-334a-27d9b2baf002/oauth2/token </param>
        /// <param name="clientId">Client Id of your AAD app. Example" ba3c223b-03ab-4a44-aa32-38aa10c27e32</param>
        /// <param name="clientSecret">Client secret of your AAD app</param>
        /// <param name="scope">Scope. If not provided, default one is used for the production API endpoint.</param>
        /// <returns>Autorization token. Prepend it with "Bearer: " and pass it in the request header as the
        /// value for "Authorization: " header.</returns>
        public static async Task<string> GetClientCredentialAccessToken(
            string tokenEndpoint,
            string clientId,
            string clientSecret,
            string scope = null)
        {
            if (scope == null)
            {
                scope = "https://manage.devcenter.microsoft.com";
            }

            dynamic result;
            using (HttpClient client = new HttpClient())
            {
                string tokenUrl = tokenEndpoint;
                using (
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Post,
                        tokenUrl))
                {
                    string strContent =
                        string.Format(
                            "grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}",
                            clientId,
                            clientSecret,
                            scope);

                    request.Content = new StringContent(strContent, Encoding.UTF8,
                        "application/x-www-form-urlencoded");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject(responseContent);
                    }
                }
            }

            return result.access_token;
        }

        /// <summary>
        /// Uploads a file to blob using a SAS url
        /// </summary>
        /// <param name="fileName">Path to your zip file</param>
        /// <param name="sasUrl">The SAS url which was returned to you when you cloned the submission
        /// in FileUploadUrl</param>
        /// <returns>A task which will complete when the file finishes uploading</returns>
        public static async Task UploadFileToBlob(string fileName, string sasUrl)
        {
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                var blockBob = new CloudBlockBlob(new Uri(sasUrl));
                await blockBob.UploadFromStreamAsync(stream);
            }
        }

        /// <summary>
        /// Invokes the specified HTTP method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="requestContent">Content of the request.</param>
        /// <returns>instance of the type T</returns>
        /// <exception cref="ServiceException"></exception>
        public async Task<T> Invoke<T>(HttpMethod httpMethod,
            string relativeUrl,
            object requestContent)
        {
            using (var request = new HttpRequestMessage(httpMethod, relativeUrl))
            {
                this.SetRequest(request, requestContent);

                using (HttpResponseMessage response = await this.httpClient.SendAsync(request))
                {
                    T result;
                    if (this.TryHandleResponse(response, out result))
                    {
                        return result;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var resource = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                        return resource;
                    }

                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        /// <summary>
        /// Sets the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="requestContent">Content of the request.</param>
        protected virtual void SetRequest(HttpRequestMessage request, object requestContent)
        {
            request.Headers.Add(Constants.RequestHeaders.CorrelationIdHeader, Guid.NewGuid().ToString());
            request.Headers.Add(Constants.RequestHeaders.MSRequestIdHeader, Guid.NewGuid().ToString());


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);

            foreach (var header in this.DefaultHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (requestContent != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(requestContent),
                    Encoding.UTF8,
                    Constants.HttpMimeTypes.JsonContentType);
            }
        }


        /// <summary>
        /// Tries the handle response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <param name="result">The result.</param>
        /// <returns>true if the response was handled</returns>
        protected virtual bool TryHandleResponse<T>(HttpResponseMessage response, out T result)
        {
            result = default(T);
            return false;
        }

        private static class Constants
        {
            public static class RequestHeaders
            {
                /// <summary>
                /// Corresponds to TraceCorrelationId in SLL. This is a GUID that is newly generated
                /// by FD for every request coming from the client.  
                /// </summary>
                public const string CorrelationIdHeader = "MS-CorrelationId";

                /// <summary>
                /// Corresponds to RequestCorrelationId in SLL. This is a GUID that is newly generated  
                /// by FD for every request that it makes to the downstream services.  
                /// </summary>
                public const string MSRequestIdHeader = "MS-RequestId";
            }

            public static class HttpMimeTypes
            {
                /// <summary>
                /// The json content type
                /// </summary>
                public const string JsonContentType = "application/json";
            }
        }
    }
}
//</IngestionClient>
