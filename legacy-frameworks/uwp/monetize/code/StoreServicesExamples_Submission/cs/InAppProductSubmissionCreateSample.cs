//<InAppProductSubmissionCreateSample>
namespace DeveloperApiCSharpSample
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Sample code for how to create add-ons, and how to create and update add-on submissions.
    /// </summary>
    public class InAppProductSubmissionCreateSample
    {
        private ClientConfiguration ClientConfig;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="c">An instance of ClientConfiguration that contains all parameters populated</param>
        public InAppProductSubmissionCreateSample(ClientConfiguration c)
        {
            this.ClientConfig = c;
        }

        public void RunInAppProductSubmissionCreateSample()
        {
            // **********************
            //       SETTINGS
            // **********************
            var appId = this.ClientConfig.ApplicationId;
            var clientId = this.ClientConfig.ClientId;
            var clientSecret = this.ClientConfig.ClientSecret;
            var serviceEndpoint = this.ClientConfig.ServiceUrl;
            var tokenEndpoint = this.ClientConfig.TokenEndpoint;

            // Get authorization token
            Console.WriteLine("Getting authorization token ");
            var accessToken = IngestionClient.GetClientCredentialAccessToken(
                tokenEndpoint,
                clientId,
                clientSecret).Result;

            Console.WriteLine("Creating a new add-on");
            dynamic newIap = new
            {
                applicationIds = new List<string>() { appId },
                productType = "Durable",
                productId = "Sample-" + Guid.NewGuid().ToString(),
            };

            var client = new IngestionClient(accessToken, serviceEndpoint);
            dynamic iapCreated = client.Invoke<dynamic>(
                HttpMethod.Post,
                relativeUrl: string.Format(
                    CultureInfo.InvariantCulture,
                    IngestionClient.CreateInAppUrlTemplate,
                    IngestionClient.Version,
                    IngestionClient.Tenant),
                requestContent: newIap).Result;
            Console.WriteLine(iapCreated.ToString());
            var iapId = iapCreated.id.Value as string;

            // Create a new submission, which will be an exact copy of the last published submission
            Console.WriteLine("Creating a new submission");
            dynamic newSubmission = new
            {
                contentType = "BookDownload",
                keywords = new List<string> { "book", "download" },
                lifeTime = "ThreeDays",
                targetPublishMode = "Immediate",
                visibility = "Public",
                pricing = new
                {
                    priceId = "Free",
                },
                listings = new Dictionary<string, dynamic>()
                {
                    {
                        "en-us",
                        new
                        {
                            description = "Sample IAP description",
                            title = "Sample IAP title",
                            icon = new
                            {
                                FileName = "icon300x300.png",
                                FileStatus = "PendingUpload",
                            },
                        }
                    }
                }
            };

            // Because it's a new add-on, we are going to create a new submission instead of
            // modifying the last published one. If you had a published add-on, you could
            // pass "null" as request body to clone the latest published submission and then
            // perform a PUT call. Alternatively, you can always post the new submission entirely
            // even if you already have a published submission but you'll have to upload the image each time.
            dynamic createdSubmission = client.Invoke<dynamic>(
                HttpMethod.Post,
                relativeUrl: string.Format(
                    CultureInfo.InvariantCulture,
                    IngestionClient.InAppSubmissionUrl,
                    IngestionClient.Version,
                    IngestionClient.Tenant,
                    iapId),
                requestContent: newSubmission).Result;
            Console.WriteLine(createdSubmission);
            var submissionId = createdSubmission.id.Value as string;

            // Upload the zip archive with all new files to the SAS URL returned with the submission.
            var fileUploadUrl = createdSubmission.fileUploadUrl.Value as string;
            Console.WriteLine("FileUploadUrl: " + fileUploadUrl);
            Console.WriteLine("Uploading file");
            IngestionClient.UploadFileToBlob(@"..\..\files.zip", fileUploadUrl).Wait();

            // Tell the system that we are done updating the submission.
            // Update the submission
            Console.WriteLine("Committing the submission");
            client.Invoke<dynamic>(
                HttpMethod.Post,
                relativeUrl: string.Format(
                    CultureInfo.InvariantCulture,
                    IngestionClient.InAppProductCommitSubmissionUrlTemplate,
                    IngestionClient.Version,
                    IngestionClient.Tenant,
                    iapId,
                    submissionId),
                requestContent: null).Wait();

            // Periodically check the status until it changes from "CommitsStarted" to either
            // successful status or a failure.
            Console.WriteLine("Waiting for the submission commit processing to complete. This may take a couple of minutes.");
            string submissionStatus = null;
            do
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                dynamic statusResource = client.Invoke<dynamic>(
                    HttpMethod.Get,
                    relativeUrl: string.Format(
                        CultureInfo.InvariantCulture,
                        IngestionClient.InAppSubmissionStatusUrlTemplate,
                        IngestionClient.Version,
                        IngestionClient.Tenant,
                        iapId,
                        submissionId),
                    requestContent: null).Result;

                submissionStatus = statusResource.status.Value as string;
                Console.WriteLine("Current status: " + submissionStatus);
            }
            while ("CommitStarted".Equals(submissionStatus));

            if ("CommitFailed".Equals(submissionStatus))
            {
                Console.WriteLine("Submission has failed. Please check the Errors collection of the submissionResource response.");
                return;
            }
            else
            {
                Console.WriteLine("Submission commit success!");
            }
        }

    }
}
//</InAppProductSubmissionCreateSample>
