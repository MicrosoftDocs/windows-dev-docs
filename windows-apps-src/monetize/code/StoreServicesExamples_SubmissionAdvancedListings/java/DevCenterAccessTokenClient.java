import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.text.MessageFormat;
import javax.json.Json;
import javax.json.JsonReader;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;

/**
 * A client for getting access tokens to the Dev Center API.
 * @author Microsoft
 */
public final class DevCenterAccessTokenClient {
    private String tenantId;
    private String clientId;
    private String clientSecret;
    
    /**
     * Creates a new access token client for Dev Center.
     * @param tenantId Your tenant ID for the app.
     * @param clientId Your client ID for the app.
     * @param clientSecret Your client secret string for the app.
     */
    public DevCenterAccessTokenClient(String tenantId, String clientId, String clientSecret) {
        this.tenantId = tenantId;
        this.clientId = clientId;
        this.clientSecret = clientSecret;
    }
    
    /**
     * Gets an access token for the specific resource.
     * @param resource The full URI to the resource to be accessed.
     * @return An access token for that resource, good for one hour.
     */
    public String getAccessToken(String resource) {        
        // Generate access token. Access token is valid for 1 hour.
        String tokenEndpoint = "https://login.microsoftonline.com/{0}/oauth2/token";       

        HttpPost tokenRequest = new HttpPost(MessageFormat.format(tokenEndpoint, this.tenantId));
        String tokenRequestBody = MessageFormat.format("grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}", this.clientId, this.clientSecret, resource);
        tokenRequest.setEntity(new StringEntity(tokenRequestBody, "utf-8"));
        tokenRequest.addHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
        
        CloseableHttpClient httpclient = HttpClients.createDefault();
        ResponseHandler<String> responseHandler = new BasicResponseHandler();
        
        try {
            String tokenResponse = httpclient.execute(tokenRequest, responseHandler);
            JsonReader reader = Json.createReader(new ByteArrayInputStream(tokenResponse.getBytes("UTF-8")));
            String accessToken = reader.readObject().getString("access_token");
            
            return accessToken;
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        } catch (ClientProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        
        return null;
    }
}
