import java.io.IOException;
import java.text.MessageFormat;
import javax.json.Json;
import javax.json.JsonArrayBuilder;
import javax.json.JsonObject;
import javax.json.JsonObjectBuilder;
import org.apache.http.client.ClientProtocolException;

public class CreateAndSubmitSubmissionExample {

    public static void main(String[] args) {
        
        // Add your tenant ID, client ID, and client secret here.
        String tenantId = "";
        String clientId = "";
        String clientSecret = "";
        DevCenterAccessTokenClient accessTokenClient = new DevCenterAccessTokenClient(tenantId, clientId, clientSecret); 
        String accessToken = accessTokenClient.getAccessToken("https://manage.devcenter.microsoft.com");
        
        // The application ID is taken from your app dashboard page's URI in Dev Center,
        // e.g. https://developer.microsoft.com/en-us/dashboard/apps/{application_id}/
        String applicationId = "";
        DevCenterClient devCenter = new DevCenterClient("https://manage.devcenter.microsoft.com", accessToken);
        
        try {
            // Get the application object, and cancel any in progress submissions.
            JsonObject app = devCenter.getApplicationJsonObject(applicationId);
            JsonObject inProgressSubmission = app.getJsonObject("pendingApplicationSubmission");
            if (inProgressSubmission != null) {
                String inProgressSubmissionId = inProgressSubmission.getString("id");
                devCenter.cancelInProgressSubmission(applicationId, inProgressSubmissionId);
            }
            
            // Create a new submission, based on the last published submission.
            JsonObject submission = devCenter.createSubmission(applicationId);
            String submissionId = submission.getString("id");
            
            // JsonObjects are immutable, so we'll build up our changes to the submission and
            // then merge it with the submission object.
            JsonObjectBuilder submissionChanges = Json.createObjectBuilder();
        
            // The following fields are required:
            submissionChanges.add("applicationCategory", "Games_Fighting");
            submissionChanges.add("listings", getListingsObject());
            submissionChanges.add("pricing", getPricingObject());
            submissionChanges.add("packages", Json.createArrayBuilder().add(getPackageObject()));
            submissionChanges.add("allowTargetFutureDeviceFamilies", getDeviceFamiliesObject());
            
            // Add new Gaming Options to the submission.
            JsonObject gamingOptions = getGamingOptionsJsonObject();
            submissionChanges.add("gamingOptions", Json.createArrayBuilder().add(gamingOptions));
            
            // Add new Trailers to the submission.
            JsonObject trailer = getTrailerObject();
            submissionChanges.add("trailers", Json.createArrayBuilder().add(trailer));
            
            // Continue updating the submission_json object with additional options as needed.
            // After you've finished, call the Update API with the code below to save it:
            JsonObject submissionToUpdate = mergeJsonObjects(submission, submissionChanges.build());
            JsonObject updatedSubmission = devCenter.updateSubmission(applicationId, submissionId, submissionToUpdate);
            
            // All images and packages should be located in a single ZIP file. In the submission JSON, 
            // the file names for all objects requiring them (icons, packages, etc.) must exactly 
            // match the file names from the ZIP file.
            String zipFilePath = "";
            devCenter.uploadZipFileForSubmission(applicationId, submissionId, zipFilePath);
            
            // Committing the submission will start the submission process for it. Once committed,
            // the submission can no longer be changed.
            devCenter.commitSubmission(applicationId, submissionId);
            
            // After committing, you can poll the commit API for the status of the submission's process using
            // the following code.
            boolean waitingForCommitToStart = true;
            while (waitingForCommitToStart) {
                String status = devCenter.getSubmissionStatus(applicationId, submissionId);
                System.out.println(MessageFormat.format("Submission status: {0}", status));
                waitingForCommitToStart = status.equals("CommitStarted");
                if (waitingForCommitToStart) {
                    try {
                        Thread.sleep(60000); // Wait one minute to check Dev Center again.  
                    } catch (InterruptedException iex) {
                        System.out.println("The sleep was interrupted. Checking Dev Center now.");
                    }
                }
            }           
        } catch (ClientProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    
    private static JsonObject mergeJsonObjects(JsonObject baseObject, JsonObject mergeObject) {
        JsonObjectBuilder builder = Json.createObjectBuilder();
        
        for (String baseKey : baseObject.keySet()) {
            if (mergeObject.containsKey(baseKey)) {
                builder.add(baseKey, mergeObject.get(baseKey));
            } else {
                builder.add(baseKey, baseObject.get(baseKey));
            }
        }
        
        for (String mergeKey : mergeObject.keySet()) {
            if (!baseObject.containsKey(mergeKey)) {
                builder.add(mergeKey, mergeObject.get(mergeKey));
            }
        }
        
        return builder.build();
    }
    
    private static JsonObject getListingsObject() {
        // This structure holds basic information to display in the store.
        JsonObjectBuilder baseListing = Json.createObjectBuilder();
        baseListing.add("copyrightAndTrademarkInfo", "(C) 2017 Microsoft");
        baseListing.add("licenseTerms", "http://example.com/licenseTerms.aspx");
        baseListing.add("privacyPolicy", "http://example.com/privacyPolicy.aspx");
        baseListing.add("supportContact", "support@example.com");
        baseListing.add("websiteUrl", "http://example.com");
        baseListing.add("description", "A sample game showing off gameplay options code.");
        baseListing.add("releaseNotes", "Initial release");
                
        // The title of the app must match a reserved name for the app in Dev Center.
        // If it doesn't, attempting to update the submission will fail.
        baseListing.add("title", "Super Dev Center API Simulator 2017");
        
        // Up to 7 keywords may be provided in a listing.
        JsonArrayBuilder keywords = Json.createArrayBuilder();
        keywords.add("SampleApp").add("SampleFightingGame").add("GameOptions");
        baseListing.add("keywords", keywords);
        
        JsonArrayBuilder features = Json.createArrayBuilder();
        features.add("Doesn't crash");
        features.add("Likes to eat chips");
        baseListing.add("features", features);
        
        // If your app works better with specific hardware (or needs it), you can
        // add or update values here.
        JsonArrayBuilder hardwarePreferences = Json.createArrayBuilder();
        hardwarePreferences.add("Keyboard");
        hardwarePreferences.add("Mouse");
        baseListing.add("hardwarePreferences", hardwarePreferences);
        
        JsonArrayBuilder images = Json.createArrayBuilder();
        
        // There are several types of images available; at least one screenshot
        // is required.
        JsonObjectBuilder image = Json.createObjectBuilder();
        image.add("fileName", "tile.png");
        image.add("description", "The tile as it appears in the store.");
        image.add("imageType", "Icon");
        images.add(image);
        baseListing.add("images", images);
        
        JsonObjectBuilder listing = Json.createObjectBuilder();
        listing.add("baseListing", baseListing);
        
        // If there are any specific overrides to above information for Windows 8,
        // Windows 8.1, Windows Phone 7.1, 8.0, or 8.1, you can add information here.
        listing.add("platformOverrides", Json.createObjectBuilder());
        
        JsonObjectBuilder listings = Json.createObjectBuilder();
        // Each listing is targeted at a specific language-locale code, e.g. EN-US.
        listings.add("en-us", listing);
        return listings.build();
    }
    
    private static JsonObject getPackageObject() {
        JsonObjectBuilder pkg = Json.createObjectBuilder();
        // The file name is relative to the root of the uploaded ZIP file.
        pkg.add("fileName", "bin/super_dev_ctr_api_sim.appxupload");
        
        // If you haven't begun to upload the file yet, set this value to "PendingUpload".
        pkg.add("fileStatus", "PendingUpload");
        return pkg.build();
    }
    
    private static JsonObject getPricingObject() {
        JsonObjectBuilder pricing = Json.createObjectBuilder();
        
        // How long the trial period is, if one is allowed. Valid values are NoFreeTrial,
        // OneDay, SevenDays, FifteenDays, ThirtyDays, or TrialNeverExpires.
        pricing.add("trialPeriod", "NoFreeTrial");
        
        // Maps to the default price for the app.
        pricing.add("priceId", "Free");
        
        // If you'd like to offer your app in different markets at different prices, you
        // can provide priceId values per language/locale code.
        pricing.add("marketSpecificPricing", Json.createObjectBuilder().build());
        return pricing.build();
    }
    
    private static JsonObject getDeviceFamiliesObject() {
        JsonObjectBuilder futureDeviceFamilies = Json.createObjectBuilder();
        
        // Supported values are Desktop, Mobile, Xbox, and Holographic. To make
        // the app available on that specific platform, set the value to True.
        futureDeviceFamilies.add("Desktop", true);
        futureDeviceFamilies.add("Mobile", false);
        futureDeviceFamilies.add("Xbox", true);
        futureDeviceFamilies.add("Holographic", false);
        return futureDeviceFamilies.build();
    }


    private static JsonObject getGamingOptionsJsonObject() {
        JsonObjectBuilder gamingOptions = Json.createObjectBuilder();
        
        // The genres of your game.
        JsonArrayBuilder genres = Json.createArrayBuilder();
        genres.add("Games_Fighting");
        gamingOptions.add("genres", genres);
        
        // Set this to true if your game supports local multiplayer. This field
        // is required.
        gamingOptions.add("isLocalMultiplayer", true);
        
        // If local multiplayer is supported, you must provide the minimum and
        // maximum players supported. Valid values are between 2 and 1000 inclusive.
        gamingOptions.add("localMultiplayerMinPlayers", 2);
        gamingOptions.add("localMultiplayerMaxPlayers", 4);
        
        // Set this to True if your game supports local co-op play. This field is required.
        gamingOptions.add("isLocalCooperative", true);

        // If local co-op is supported, you must provide the minimum and maximum players
        // supported. Valid values are between 2 and 1000 inclusive.            
        gamingOptions.add("localCooperativeMinPlayers", 2);
        gamingOptions.add("localCooperativeMaxPlayers", 4);
        
        // Set this to True if your game supports online multiplayer. This field is required.
        gamingOptions.add("isOnlineMultiplayer", true);
        
        // If online multiplayer is supported, you must provide the minimum and maximum players
        // supported. Valid values are between 2 and 1000 inclusive.
        gamingOptions.add("onlineMultiplayerMinPlayers", 2);
        gamingOptions.add("onlineMultiplayerMaxPlayers", 4);
        
        // Set this to true if your game supports online co-op play. This field is required. 
        gamingOptions.add("isOnlineCooperative", true);
        
        // If online co-op is supported, you must provide the minimum and maximum players
        // supported. Valid values are between 2 and 1000 inclusive.
        gamingOptions.add("onlineCooperativeMinPlayers", 2);
        gamingOptions.add("onlineCooperativeMaxPlayers", 4);
        
        // If your game supports broadcasting a stream to other players, set this field to True.
        // This field is required.
        gamingOptions.add("isBroadcastingPrivilegeGranted", true);
        
        // If your game supports cross-device play (e.g. a player can play on an Xbox One with
        // their friend who's playing on a PC), set this field to True. This field is required.
        gamingOptions.add("isCrossPlayEnabled", true);
        
        // If your game supports Kinect usage, set this field to "Enabled", otherwise, set it to
        // "Disabled". This field is required.
        gamingOptions.add("kinectDataForExternal", "Disabled");
        
        // Free text about any other peripherals that your game supports. This field is optional.
        gamingOptions.add("otherPeripherals", "Supports the usage of all fighting joysticks.");
        
        return gamingOptions.build();
    }
    
    private static JsonObject getTrailerObject() {
        JsonObjectBuilder trailer = Json.createObjectBuilder();

        // This is the filename of the trailer. The file name is a relative path to the
        // root of the ZIP file to be uploaded to the API.
        trailer.add("VideoFileName", "trailers/main/my_awesome_trailer.mpeg");
        
        // Aside from the video itself, a trailer can have metadata assets including a title and images 
        // such as screenshots or alternate images. These are keyed by market code (see the end of this
        // method for an example.
        JsonObjectBuilder trailerAssetsByCountry = Json.createObjectBuilder();
                
        JsonObjectBuilder trailerAssetSet = Json.createObjectBuilder();
                
        // The title of the trailer to display in the store.
        trailerAssetSet.add("Title", "Main Trailer");
        
        // The list of images provided with the trailer that are shown when the trailer isn't playing.
        JsonArrayBuilder trailerImageAssets = Json.createArrayBuilder();
        
        JsonObjectBuilder mainTrailerImage = Json.createObjectBuilder();
        
        // The file name of the image. The file name is a relative path to the root of the ZIP
        // file to be uploaded to the API.
        mainTrailerImage.add("FileName", "trailers/main/thumbnail.png");
        
        // A plaintext description of what the image represents.
        mainTrailerImage.add("Description", "The thumbnail for the trailer shown before the user clicks play");
        trailerImageAssets.add(mainTrailerImage);
        
        // Add a second image.
        JsonObjectBuilder altImage = Json.createObjectBuilder();
        altImage.add("FileName", "trailers/main/alt-img.png");
        altImage.add("Description", "The image to show after the trailer plays");
        trailerImageAssets.add(altImage);
        
        trailerAssetSet.add("ImageList", trailerImageAssets);
        
        // This line creates the trailer asset for en-us.
        trailerAssetsByCountry.add("en-us", trailerAssetSet);
        
        trailer.add("TrailerAssets", trailerAssetsByCountry);
        
        return trailer.build();
    }
}
