import java.io.File;
import java.io.IOException;
import java.io.StringWriter;
import java.text.MessageFormat;
import javax.json.Json;
import javax.json.JsonObject;
import javax.json.JsonReader;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpDelete;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpPut;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.entity.FileEntity;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;

/**
 * A client for accessing commands in the Dev Center API.
 * @author Microsoft
 */
public final class DevCenterClient implements ResponseHandler<JsonObject> {
    private String baseUri;
    private String accessToken;
    
    /**
     * Creates a new dev center client instance.
     * @param baseUri The base URI for the Dev Center API.
     * @param accessToken The access token for web call authentication.
     */
    public DevCenterClient(String baseUri, String accessToken) {
        this.baseUri = baseUri;
        this.accessToken = accessToken;
    }
    
    /**
     * Returns the application JSON object from the Dev Center API.
     * @param applicationId The application ID. 
     * @return A JSON object from Dev Center.
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public JsonObject getApplicationJsonObject(String applicationId) throws ClientProtocolException, IOException {
        String path = MessageFormat.format("/v1.0/my/applications/{0}", applicationId);
        return get(path);
    }
    
    /**
     * Cancels and deletes the in-progress submission from the application.
     * @param applicationId The application ID.
     * @param submissionId The submission ID.
     * @return A JSON object from Dev Center.
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public JsonObject cancelInProgressSubmission(String applicationId, String submissionId) throws ClientProtocolException, IOException {
        String path = MessageFormat.format("/v1.0/my/applications/{0}/submissions/{1}", applicationId, submissionId);
        return delete(path);
    }
    
    /**
     * Creates a new submission in Dev Center.
     * @param applicationId The application ID.
     * @return The submission JSON object from Dev Center.
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public JsonObject createSubmission(String applicationId) throws ClientProtocolException, IOException {
        String path = MessageFormat.format("/v1.0/my/applications/{0}/submissions", applicationId);
        return post(path);
    }
    
    /**
     * Updates the submission in Dev Center.
     * @param applicationId The application ID.
     * @param submissionId The submission ID.
     * @param submission The submission JSON object.
     * @return The updated submission JSON object from Dev Center. 
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public JsonObject updateSubmission(String applicationId, String submissionId, JsonObject submission) throws ClientProtocolException, IOException {
        String path = MessageFormat.format("/v1.0/my/applications/{0}/submissions/{1}", applicationId, submissionId);
        return put(path, submission);
    }
    
    /**
     * Gets the submission in Dev Center.
     * @param applicationId The application ID.
     * @param submissionId The submission ID.
     * @return The submission JSON object from Dev Center. 
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public JsonObject getSubmission(String applicationId, String submissionId) throws ClientProtocolException, IOException {
        String path = MessageFormat.format("/v1.0/my/applications/{0}/submissions/{1}", applicationId, submissionId);
        return get(path);
    }
    
    /**
     * Commits the submission to Dev Center.
     * @param applicationId The application ID.
     * @param submissionId The submission ID.
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public void commitSubmission(String applicationId, String submissionId) throws ClientProtocolException, IOException {
        String path = MessageFormat.format("/v1.0/my/applications/{0}/submissions/{1}/commit", applicationId, submissionId);
        post(path);
    }
    
    /**
     * Returns the submission status of this submission.
     * @param applicationId The application ID.
     * @param submissionId The submission ID.
     * @return The status of the submission in Dev Center.
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public String getSubmissionStatus(String applicationId, String submissionId) throws ClientProtocolException, IOException {
        JsonObject response = getSubmission(applicationId, submissionId);
        String status = response.getString("status");
        if (status == null || status.isEmpty())
        {
            return "Unknown";
        }
        
        return status;
    }
    
    /**
     * Uploads a ZIP archive containing the binaries, image assets, trailers, and other components to the submission.
     * @param applicationId The application ID.
     * @param submissionId The submission ID.
     * @param zipFilePath The file path to the ZIP file.
     * @throws IOException Thrown when a serialization exception occurs on the read.
     * @throws ClientProtocolException Thrown when an HTTP communication error occurs.
     */
    public void uploadZipFileForSubmission(String applicationId, String submissionId, String zipFilePath) throws ClientProtocolException, IOException {
        JsonObject submission = getSubmission(applicationId, submissionId); 
        String fileUploadUrl = submission.getString("fileUploadUri");
        
        CloseableHttpClient httpclient = HttpClients.createDefault();
        File uploadFile = new File(zipFilePath);
        HttpPut uploadFileRequest = new HttpPut(fileUploadUrl.replace("+", "%2B")); // Encode '+', otherwise it will be decoded as ' ' 
        uploadFileRequest.addHeader("x-ms-blob-type", "BlockBlob");
        uploadFileRequest.setEntity(new FileEntity(uploadFile));
        httpclient.execute(uploadFileRequest);
    }
    
    private JsonObject get(String path) throws ClientProtocolException, IOException {
        HttpGet request = new HttpGet(this.baseUri + path);
        return invoke(request);
    }
    
    private JsonObject put(String path, JsonObject obj) throws ClientProtocolException, IOException {
        HttpPut request = new HttpPut(this.baseUri + path);
        request.addHeader("Content-Type", "application/json; charset=utf-8");
        request.setEntity(new StringEntity(SerializeJsonObject(obj)));
        return invoke(request);
    }
    
    private JsonObject post(String path) throws ClientProtocolException, IOException {
        return post(path, null);
    }
    
    private JsonObject post(String path, JsonObject obj) throws ClientProtocolException, IOException {
        HttpPost request = new HttpPost(this.baseUri + path);
        request.addHeader("Content-Type", "application/json; charset=utf-8");
        if (obj != null) 
        {
            request.setEntity(new StringEntity(SerializeJsonObject(obj)));        
        }
        return invoke(request);
    }
    
    private JsonObject delete(String path) throws ClientProtocolException, IOException {
        HttpDelete request = new HttpDelete(this.baseUri + path);
        return invoke(request);
    }
    
    private JsonObject invoke(HttpUriRequest request) throws ClientProtocolException, IOException {
        CloseableHttpClient client = HttpClients.createDefault();    
        request.addHeader("Authorization", "Bearer " + accessToken);
        request.addHeader("User-Agent", "Java");
        JsonObject response = client.execute(request, this);
        return response;
    }   

    public JsonObject handleResponse(HttpResponse response) throws ClientProtocolException, IOException  {
        StatusLine status = response.getStatusLine();
        int statusCode = status.getStatusCode();
        String reasonPhrase = status.getReasonPhrase();
        HttpEntity entity = response.getEntity();
        JsonObject returnValue = null;
        if(entity != null && entity.getContentLength() != 0) {
            JsonReader reader = Json.createReader(entity.getContent());
            returnValue = reader.readObject();
        }
        
        if (statusCode < 200 || statusCode > 299)  {
            if (returnValue != null) {
                throw new HttpResponseException(statusCode, returnValue.toString());
            }
            
            throw new HttpResponseException(statusCode, reasonPhrase);
        }
        
        return returnValue;
    }
    
    private static String SerializeJsonObject(JsonObject obj) {
        StringWriter writer = new StringWriter();
        Json.createWriter(writer).writeObject(obj);
        String body = writer.toString();
        return body;
    }
}