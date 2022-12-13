//<CreateAndSubmitSubmissionExample>
using System;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace DevCenterApiSample
{
    public class CreateAndSubmitSubmissionExample
    {
        public static void Execute()
        {
            // Add your tenant ID, client ID, and client secret here.
            string tenantId = "";
            string clientId = "";
            string clientSecret = "";
            var accessTokenClient = new DevCenterAccessTokenClient(tenantId, clientId, clientSecret);

            string accessToken = accessTokenClient.GetAccessToken("https://manage.devcenter.microsoft.com");
            var devCenter = new DevCenterClient(accessToken);

            // The application ID is taken from your app dashboard page's URI in Dev Center,
            // e.g. https://developer.microsoft.com/en-us/dashboard/apps/{application_id}/
            string applicationId = "{application_id}";

            // Get the application object, and cancel any in progress submissions.
            JObject app = devCenter.GetApplication(applicationId);
            JToken inProgressSubmission = app.GetValue("pendingApplicationSubmission");
            if (inProgressSubmission != null)
            {
                string inProgressSubmissionId = inProgressSubmission.Value<string>("id");
                devCenter.CancelInProgressSubmission(applicationId, inProgressSubmissionId);
            }

            // Create a new submission, based on the last published submission.
            JObject submission = devCenter.CreateSubmission(applicationId);
            string submissionId = submission.GetValue("id").Value<string>();

            // The following fields are required.
            submission["applicationCategory"] = "Games_Fighting";
            submission["listings"] = GetListingsObject();
            submission["pricing"] = GetPricingObject();
            submission["packages"] = new JArray() { GetPackageObject() };
            submission["allowTargetFutureDeviceFamilies"] = GetDeviceFamiliesObject();

            // The app must have the hasAdvancedListingPermission set to True in order for gaming options
            // and trailers to be applied. If that's not the case, you can still update the app and
            // its submissions through the API, but gaming options and trailers won't be saved.
            if (app["hasAdvancedListingPermission"] == null || app["hasAdvancedListingPermission"].Value<bool>() == false)
            {
                Console.WriteLine("This application does not support gaming options or trailers.");
            }
            else
            {
                // Gaming options is an array. A maximum of one value may be provided.
                submission["gamingOptions"] = new JArray(GetGamingOptionsObject());

                // A maximum of 15 trailers may be provided in the trailers array.
                submission["trailers"] = new JArray(GetTrailerObject());
            }                

            // Continue updating the submission_json object with additional options as needed.
            // After you've finished, call the Update API with the code below to save it.
            JObject updatedSubmission = devCenter.UpdateSubmission(applicationId, submissionId, submission);

            // All images and packages should be located in a single ZIP file. In the submission JSON, 
            // the file names for all objects requiring them (icons, packages, etc.) must exactly 
            // match the file names from the ZIP file.
            string zipFilePath = "";
            devCenter.UploadZipFileForSubmission(applicationId, submissionId, zipFilePath);

            // Committing the submission will start the submission process for it. Once committed,
            // the submission can no longer be changed.
            devCenter.CommitSubmission(applicationId, submissionId);

            // After committing, you can poll the commit API for the status of the submission's process using
            // the following code.
            bool waitingForCommitToStart = true;
            while (waitingForCommitToStart)
            {
                string status = devCenter.GetSubmissionStatus(applicationId, submissionId);
                Console.WriteLine($"Submission status: {status}");
                waitingForCommitToStart = status.Equals("CommitStarted");
                if (waitingForCommitToStart)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1)); // Wait to check Dev Center again.	
                }
            }
        }

        private static JObject GetListingsObject()
        {
            // This structure holds basic information to display in the store.
            var baseListing = new JObject();
            baseListing.Add("copyrightAndTrademarkInfo", "(C) 2017 Microsoft");
            baseListing.Add("licenseTerms", "http://example.com/licenseTerms.aspx");
            baseListing.Add("privacyPolicy", "http://example.com/privacyPolicy.aspx");
            baseListing.Add("supportContact", "support@example.com");
            baseListing.Add("websiteUrl", "http://example.com");
            baseListing.Add("description", "A sample game showing off gameplay options code.");
            baseListing.Add("releaseNotes", "Initial release");

            // The title of the app must match a reserved name for the app in Dev Center.
            // If it doesn't, attempting to update the submission will fail.
            baseListing.Add("title", "Super Game Options API Simulator 2017");

            var keywords = new JArray();
            keywords.Add("SampleApp");
            keywords.Add("SampleFightingGame");
            keywords.Add("GameOptions");
            baseListing.Add("keywords", keywords);

            var features = new JArray();
            features.Add("Doesn't crash");
            features.Add("Likes to eat chips");
            baseListing.Add("features", features);

            // If your app works better with specific hardware (or needs it), you can
            // add or update values here.
            var hardwarePreferences = new JArray()
            {
                "Keyboard",
                "Mouse"
            };
            baseListing.Add("hardwarePreferences", hardwarePreferences);

            var images = new JArray();

            // There are several types of images available; at least one screenshot
            // is required.
            var image = new JObject();

            // The file name is relative to the root of the uploaded ZIP file.
            image.Add("fileName", "img/screenshot.png");
            image.Add("description", "A basic screenshot of the app.");
            image.Add("imageType", "Screenshot");
            images.Add(image);
            baseListing.Add("images", images);

            var listing = new JObject();
            listing.Add("baseListing", baseListing);

            // If there are any specific overrides to above information for Windows 8,
            // Windows 8.1, Windows Phone 7.1, 8.0, or 8.1, you can add information here.
            listing.Add("platformOverrides", new JObject());

            // Each listing is targeted at a specific language-locale code, e.g. EN-US.
            var listings = new JObject();
            listings.Add("en-us", listing);
            return listings;
        }

        private static JObject GetPackageObject()
        {
            var package = new JObject()
            {
                // The file name is relative to the root of the uploaded ZIP file.
                ["fileName"] = "bin/super_dev_ctr_api_sim.appxupload",

                // If you haven't begun to upload the file yet, set this value to "PendingUpload".
                ["fileStatus"] = "PendingUpload"
            };
            return package;
        }

        private static JObject GetPricingObject()
        {
            var pricing = new JObject();

            // How long the trial period is, if one is allowed. Valid values are NoFreeTrial,
            // OneDay, SevenDays, FifteenDays, ThirtyDays, or TrialNeverExpires.
            pricing.Add("trialPeriod", "NoFreeTrial");

            // Maps to the default price for the app.
            pricing.Add("priceId", "Free");

            // If you'd like to offer your app in different markets at different prices, you
            // can provide priceId values per language/locale code.
            pricing.Add("marketSpecificPricing", new JObject());
            return pricing;
        }

        private static JObject GetDeviceFamiliesObject()
        {
            var futureDeviceFamilies = new JObject();

            // Supported values are Desktop, Mobile, Xbox, and Holographic. To make
            // the app available on that specific platform, set the value to True.
            futureDeviceFamilies.Add("Desktop", true);
            futureDeviceFamilies.Add("Mobile", false);
            futureDeviceFamilies.Add("Xbox", true);
            futureDeviceFamilies.Add("Holographic", false);
            return futureDeviceFamilies;
        }
        
        private static JObject GetTrailerObject()
        {
            // Add an example trailer.
            var trailer = new JObject();

            // This is the filename of the trailer. The file name is a relative path to the
            // root of the ZIP file to be uploaded to the API.
            trailer["VideoFileName"] = "trailers/main/my_awesome_trailer.mpeg";

            // Aside from the video itself, a trailer can have image assets such as screenshots
            // or alternate images.
            var trailerAssets = new JObject();
            trailer["TrailerAssets"] = trailerAssets;

            // Add trailer assets for the EN-US market.
            var trailerAsset = new JObject();
            trailerAssets["en-us"] = trailerAsset;

            // The title of the trailer to display in the store.
            trailerAsset["Title"] = "Main Trailer";

            // The list of images provided with the trailer that are shown
            // when the trailer isn't playing.
            var imageList = new JArray();
            trailerAsset["ImageList"] = imageList;

            // Add a few images to the image list.
            var thumbnailImage = new JObject()
            {
                // The file name of the image. The file name is a relative
                // path to the root of the ZIP
                // file to be uploaded to the API.
                ["FileName"] = "trailers/main/thumbnail.png",

                // A plaintext description of what the image represents.
                ["Description"] = "The thumbnail for the trailer shown " + "before the user clicks play"
            };
            imageList.Add(thumbnailImage);

            var altImage = new JObject()
            {
                ["FileName"] = "trailers/main/alt-img.png",
                ["Description"] = "The image to show after the trailer plays"
            };
            imageList.Add(altImage);

            return trailer;
        }

        private static JObject GetGamingOptionsObject()
        {
            var gamingOptions = new JObject();

            // The genres of your game.
            var genres = new JArray();
            genres.Add("Games_Fighting");
            gamingOptions["genres"] = genres;

            // Set this to true if your game supports local multiplayer. This field
            // is required.
            gamingOptions["isLocalMultiplayer"] = true;

            // If local multiplayer is supported, you must provide the minimum and
            // maximum players supported. Valid values are between 2 and 1000 inclusive.
            gamingOptions["localMultiplayerMinPlayers"] = 2;
            gamingOptions["localMultiplayerMaxPlayers"] = 4;

            // Set this to True if your game supports local co-op play. This field is required.
            gamingOptions["isLocalCooperative"] = true;

            // If local co-op is supported, you must provide the minimum and maximum players
            // supported. Valid values are between 2 and 1000 inclusive.			
            gamingOptions["localCooperativeMinPlayers"] = 2;
            gamingOptions["localCooperativeMaxPlayers"] = 4;

            // Set this to True if your game supports online multiplayer. This field is required.
            gamingOptions["isOnlineMultiplayer"] = true;

            // If online multiplayer is supported, you must provide the minimum and maximum players
            // supported. Valid values are between 2 and 1000 inclusive.
            gamingOptions["onlineMultiplayerMinPlayers"] = 2;
            gamingOptions["onlineMultiplayerMaxPlayers"] = 4;

            // Set this to true if your game supports online co-op play. This field is required. 
            gamingOptions["isOnlineCooperative"] = true;

            // If online co-op is supported, you must provide the minimum and maximum players
            // supported. Valid values are between 2 and 1000 inclusive.
            gamingOptions["onlineCooperativeMinPlayers"] = 2;
            gamingOptions["onlineCooperativeMaxPlayers"] = 4;

            // If your game supports broadcasting a stream to other players, set this field to True.
            // This field is required.
            gamingOptions["isBroadcastingPrivilegeGranted"] = true;

            // If your game supports cross-device play (e.g. a player can play on an Xbox One with
            // their friend who's playing on a PC), set this field to True. This field is required.
            gamingOptions["isCrossPlayEnabled"] = true;

            // If your game supports Kinect usage, set this field to "Enabled", otherwise, set it to
            // "Disabled". This field is required.
            gamingOptions["kinectDataForExternal"] = "Disabled";

            // Free text about any other peripherals that your game supports. This field is optional.
            gamingOptions["otherPeripherals"] = "Supports the usage of all fighting joysticks.";
            return gamingOptions;
        }
    }
}
//</CreateAndSubmitSubmissionExample>
