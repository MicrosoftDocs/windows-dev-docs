import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URISyntaxException;
import java.text.MessageFormat;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.*;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.entity.FileEntity;
import org.apache.http.entity.StringEntity;

import javax.json.Json;
import javax.json.JsonObject;
import javax.json.JsonReader;

public class IngestionServiceJavaSamples {

    public static void main(String[] args) throws InterruptedException, URISyntaxException, IOException {

        // Generate access token. Access token is valid for 1 hour. Regenerate access token when needed
        String tenantId = "";      //{Your tenant ID}
        String clientId = "";      //{Your client ID}
        String clientSecret = "";  //{Your client secret}

        String accessToken = GenerateAccessToken(tenantId, clientId, clientSecret);

        // Application submission sample
        String applicationId = "";               //{Your application ID}
        String appSubmissionRequestJson = "";    //{Your submission request JSON}
        String appSubmissionZipFilePath = "";    //{Your zip file path}

        SubmitNewApplicationSubmission(accessToken, applicationId, appSubmissionRequestJson, 
            appSubmissionZipFilePath);

        // Flight samples
        String flightId = "";                       //{Your flight ID}
        String flightRequestJson = "";              //{Your flight request JSON}        
        String flightSubmissionRequestJson = "";    //{Your submission request JSON}
        String flightSubmissionZipFilePath = "";    //{Your zip file path}    

        CreateNewFlight(accessToken, applicationId, flightRequestJson);
        SubmitNewFlightSubmission(accessToken, applicationId, flightId, flightSubmissionRequestJson, 
            flightSubmissionZipFilePath);

        // In-app-product samples
        String inAppProductId = "";              //{Your in-app-product ID}
        String inAppProductRequestJson = "";     //{Your in-app-product request JSON}    
        String iapSubmissionRequestJson = "";    //{Your submission request JSON}
        String iapSubmissionZipFilePath = "";    //{Your zip file path}

        CreateNewInAppProduct(accessToken, inAppProductRequestJson);
        SubmitNewInAppProductSubmission(accessToken, inAppProductId, iapSubmissionRequestJson, 
            iapSubmissionZipFilePath);
    }
}