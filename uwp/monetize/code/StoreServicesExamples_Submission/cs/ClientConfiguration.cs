//<ClientConfiguration>
namespace DeveloperApiCSharpSample
{
    /// <summary>
    /// Configuration class
    /// </summary>
    public class ClientConfiguration
    {
        /// <summary>
        /// Client Id of your AAD app.
        /// Example" ba3c223b-03ab-4a44-aa32-38aa10c27e32
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client secret of your AAD app
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Service root endpoint.
        /// Example: https://manage.devcenter.microsoft.com
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Token endpoint to which the request is to be made. Specific to your AAD app
        /// Example: https://login.windows.net/d454d300-128e-2d81-334a-27d9b2baf002/oauth2/token
        /// </summary>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// Resource scope. If not provided (set to null), default one is used for the production API
        /// endpoint ("https://manage.devcenter.microsoft.com")
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Application ID.
        /// Example: 9WZANCRD4AMD
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// In-app-product ID;
        /// Example: 9WZBMAAD4VVV
        /// </summary>
        public string InAppProductId { get; set; }

        /// <summary>
        /// Flight Id
        /// Example: 62211033-c2fa-3934-9b03-d72a6b2a171d
        /// </summary>
        public string FlightId { get; set; }
    }
}
//</ClientConfiguration>
