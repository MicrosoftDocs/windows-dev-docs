//<DevCenterClient>
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DevCenterApiSample
{
    /// <summary>
    /// A client for accessing the Dev Center APIs.
    /// </summary>
    public class DevCenterClient
    {
        private string _accessToken;
        private Uri _baseUri;

        /// <summary>
        /// Creates a new instance of the <see cref="DevCenterClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token to authenticate to the service with.</param>
        public DevCenterClient(string accessToken)
        {
            _baseUri = new Uri("https://manage.devcenter.microsoft.com");
            _accessToken = accessToken;
        }

        /// <summary>
        /// Retrieves the JSON object representing the application from Dev Center.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>A JObject that may be navigated.</returns>
        /// <remarks>
        /// The application ID is taken from your app dashboard page's URI in Dev Center,
		/// e.g. https://developer.microsoft.com/en-us/dashboard/apps/{application_id}/
        /// </remarks>
        public JObject GetApplication(string applicationId) 
            => Invoke(HttpMethod.Get, $"/v1.0/my/applications/{applicationId}");

        /// <summary>
        /// Cancels an in-progress submission for the app.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="submissionId">The submission ID.</param>
        /// <returns></returns>
        public void CancelInProgressSubmission(string applicationId, string submissionId)
            => Invoke(HttpMethod.Delete, $"/v1.0/my/applications/{applicationId}/submissions/{submissionId}");

        /// <summary>
        /// Creates a new in-progress submission for the application.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>A JObject that may navigated.</returns>
        public JObject CreateSubmission(string applicationId)
            => Invoke(HttpMethod.Post, $"/v1.0/my/applications/{applicationId}/submissions");

        /// <summary>
        /// Updates the submission with the new data provided.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="submissionId">The submission ID.</param>
        /// <param name="submission">The submission body.</param>
        /// <returns>The updated submission JObject.</returns>
        public JObject UpdateSubmission(string applicationId, string submissionId, JObject submission)
            => Invoke(HttpMethod.Put, $"/v1.0/my/applications/{applicationId}/submissions/{submissionId}", submission);

        /// <summary>
        /// Gets the submission from Dev Center.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="submissionId">The submission ID.</param>
        /// <returns>The submission object from Dev Center.</returns>
        public JObject GetSubmission(string applicationId, string submissionId)
            => Invoke(HttpMethod.Get, $"/v1.0/my/applications/{applicationId}/submissions/{submissionId}");

        /// <summary>
        /// Commits the submission to Dev Center.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="submissionId">The submission ID.</param>
        /// <remarks>
        /// Once a submission is committed, Dev Center will begin processing and certifying it;
        /// it can no longer be changed after this point.
        /// </remarks>
        public void CommitSubmission(string applicationId, string submissionId)
            => Invoke(HttpMethod.Post, $"/v1.0/my/applications/{applicationId}/submissions/{submissionId}/commit");

        /// <summary>
        /// Returns the current submission commit status.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="submissionId">The submission ID.</param>
        /// <returns>The submission status.</returns>
        public string GetSubmissionStatus(string applicationId, string submissionId)
        {
            JObject response = GetSubmission(applicationId, submissionId);
            string status = response.Value<string>("status") ?? "Unknown";
            return status;
        }

        /// <summary>
        /// Uploads the ZIP file containing assets for the submission to the submission in Dev Center.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="submissionId">The submission ID.</param>
        /// <param name="zipFilePath">The path to the ZIP file.</param>
        public void UploadZipFileForSubmission(string applicationId, string submissionId, string zipFilePath)
        {
            JObject submission = GetSubmission(applicationId, submissionId);
            string fileUploadUrl = submission["fileUploadUri"].Value<string>();

            HttpRequestMessage uploadRequest = new HttpRequestMessage(HttpMethod.Put, fileUploadUrl.Replace("+", "%2B")); // Encode '+', otherwise it will be decoded as ' ' 
            uploadRequest.Content = new StreamContent(File.OpenRead(zipFilePath));
            uploadRequest.Headers.Add("x-ms-blob-type", "BlockBlob");

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage uploadResponse = httpClient.SendAsync(uploadRequest).GetAwaiter().GetResult();
            uploadResponse.EnsureSuccessStatusCode();

            uploadRequest.Dispose();
            uploadResponse.Dispose();
            httpClient.Dispose();
        }

        private JObject Invoke(HttpMethod method, string path, JObject body = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, new Uri(_baseUri, path));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Headers.UserAgent.ParseAdd("C-Sharp");
            if (body != null)
            {
                request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                string message = string.IsNullOrEmpty(responseContent) ? response.ReasonPhrase : responseContent;
                throw new HttpException((int)response.StatusCode, message);
            }

            if (string.IsNullOrEmpty(responseContent))
            {
                return null;
            }

            client.Dispose();
            request.Dispose();
            response.Dispose();

            JObject responseObject = (JObject)JsonConvert.DeserializeObject(responseContent);
            return responseObject;
        }
    }
}
//</DevCenterClient>
