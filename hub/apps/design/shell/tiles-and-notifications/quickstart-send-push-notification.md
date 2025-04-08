---
title: 'Quickstart: Sending a push notification (XAML)'
description: how to send push notifications using WNS
ms:assetid: ADA6A0B8-9085-421f-B409-86806EA6BD75
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Hh868252(v=Win.10)
ms:contentKeyID: 45725055
ms.date: 10/06/2021
ms.topic: article
mtps_version: v=Win.10
dev_langs:
- csharp
---

# Quickstart: Sending a push notification (XAML)

Your cloud server can send a push notification to your app through the Windows Push Notification Services (WNS). This procedure applies to tile, toast, badge, and raw push notifications.

**Objective:** To create and send a tile, toast, badge, or raw push notification.

## Prerequisites

To understand this topic or to use the code it provides, you will need:

- A familiarity with HTTP communications.
- An authenticated cloud server. For more information, see [How to authenticate with the Windows Push Notification Service (WNS)](/previous-versions/windows/apps/hh868206(v=win.10)).
- A registered channel over which your cloud server can communicate with your app. For more information, see [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh868221(v=win.10)).
- An existing tile for your app, defined in your app's manifest, to receive the notification (unless you're sending a raw notification). For more information, see [Quickstart: Creating a default tile using the Microsoft Visual Studio manifest editor](/previous-versions/windows/apps/hh868247(v=win.10)).
- A familiarity with XML and its manipulation through Document Object Model (DOM) APIs.
- In the case of raw notifications, your app must be configured to receive raw notifications. For more information, see [Quickstart: Intercepting push notifications for running apps](/previous-versions/windows/apps/jj709907(v=win.10)) and [Quickstart: Creating and registering a raw notification background task](/previous-versions/windows/apps/jj709906(v=win.10)).

## Instructions

### 1. Include the necessary namespace references

The examples given in this topic can be used as-is, but require that your code include these namespace references:

``` csharp
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Text;
```

### 2. Create an HTTP POST request

The `uri` parameter is the channel Uniform Resource Identifier (URI) requested by the app and passed to the cloud server. For more information, see [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh868221(v=win.10)).

``` csharp
HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
request.Method = "POST";
```

### 3. Add the required headers

There are four required headers that must be included in all push notifications: [X-WNS-Type](/previous-versions/windows/apps/hh868245(v=win.10)), Content-Type, Content-Length, and [Authorization](/previous-versions/windows/apps/hh868245(v=win.10)).

- The [X-WNS-Type](/previous-versions/windows/apps/hh868245(v=win.10)) header specifies whether this is a tile, toast, badge, or raw notification.
- The Content-Type is set depending on the value of the [X-WNS-Type](/previous-versions/windows/apps/hh868245(v=win.10)).
- The Content-Length gives the size of the included notification payload.
- The [Authorization](/previous-versions/windows/apps/hh868245(v=win.10)) header specifies the authentication credential that allows you to send a push notification to this user over this channel.

The *accessToken* parameter of the [Authorization](/previous-versions/windows/apps/hh868245(v=win.10)) header specifies the access token, stored on the server, that was received from WNS when the cloud server requested authentication. Without the access token, your notification will be rejected.

For a complete list of possible headers, see [Push notification service request and response headers](/previous-versions/windows/apps/hh868245(v=win.10)).

``` csharp
request.Headers.Add("X-WNS-Type", notificationType);
request.ContentType = contentType;
request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.AccessToken));
```

### 4. Add the prepared content

As far as the HTTP request is concerned, the XML content of the notification is a data blob in the request body. For instance, no verification is made that the XML matches the X-WNS-Type specification. The content is specified as an XML payload and here is added to the request as a stream of bytes.

``` csharp
byte[] contentInBytes = Encoding.UTF8.GetBytes(xml);
                        
using (Stream requestStream = request.GetRequestStream())
    requestStream.Write(contentInBytes, 0, contentInBytes.Length);
```

### 5. Listen for a response from WNS that acknowledges receipt of the notification

> [!NOTE]
> You will never receive a delivery confirmation for a notification, just an acknowledgment that it was received by WNS.

``` csharp
using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
    return webResponse.StatusCode.ToString();
```

### 6. Handle WNS response codes

There are many response codes that your app service can receive when it sends a notification. Some of these response codes are more common than others and can be easily dealt with in a catch block.

``` csharp
catch (WebException webException)
{
    HttpStatusCode status = ((HttpWebResponse)webException.Response).StatusCode;
```

**HttpStatusCode.Unauthorized**: The access token you presented has expired. Get a new one and then try sending your notification again. Because your cached access token expires after 24 hours, you can expect to get this response from WNS at least once a day. We recommend that you implement a maximum retry policy.

``` csharp
    if (status == HttpStatusCode.Unauthorized)
    {
        GetAccessToken(secret, sid);
        return PostToWns(uri, xml, secret, sid, notificationType, contentType);
    }
```

**HttpStatusCode.Gone / HttpStatusCode.NotFound**: The channel URI is no longer valid. Remove this channel from your database to prevent further attempts to send notification to it. The next time this user launches your app, request a new WNS channel. Your app should detect that its channel has changed, which should trigger the app to send the new channel URI to your app server. For more information, see [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh868221(v=win.10)).

``` csharp
    else if (status == HttpStatusCode.Gone || status == HttpStatusCode.NotFound)
    {
        return "";
    }
```

**HttpStatusCode.NotAcceptable**: This channel is being throttled by WNS. Implement a retry strategy that exponentially reduces the amount of notifications being sent in order to prevent being throttled again. Also, rethink scenarios that are causing your notifications to be throttled. You will provide a richer user experience by limiting the notifications you send to those that add true value.

``` csharp
    else if (status == HttpStatusCode.NotAcceptable)
    {
        return "";
    }
```

**Other response codes**: WNS responded with a less common response code. Log this code to assist in debugging. See [Push notification service request and response headers](/previous-versions/windows/apps/hh868245(v=win.10)) for a full list of WNS response codes.

``` csharp
    else
    {
        string[] debugOutput = {
                                   status.ToString(),
                                   webException.Response.Headers["X-WNS-Debug-Trace"],
                                   webException.Response.Headers["X-WNS-Error-Description"],
                                   webException.Response.Headers["X-WNS-Msg-ID"],
                                   webException.Response.Headers["X-WNS-Status"]
                               };
        return string.Join(" | ", debugOutput);            
    }
```

### 7. Encapsulate the code into a single function

The following example packages the code given in the preceding steps into a single function. This function composes the HTTP POST request that contains a notification to be sent to WNS. By changing the value of the *type* parameter and adjusting additional headers, this code can be used for toast, tile, badge, or raw push notifications. You can use this function as part of your cloud server code.

Note that the error handling in this function includes the situation where the access token has expired. In this case, it calls another cloud server function that re-authenticates with WNS to obtain a new access token. It then makes a new call to the original function.

``` csharp
// Post to WNS
public string PostToWns(string secret, string sid, string uri, string xml, string notificationType, string contentType)
{
    try
    {
        // You should cache this access token.
        var accessToken = GetAccessToken(secret, sid);

        byte[] contentInBytes = Encoding.UTF8.GetBytes(xml);

        HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
        request.Method = "POST";
        request.Headers.Add("X-WNS-Type", notificationType);
        request.ContentType = contentType;
        request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken.AccessToken));

        using (Stream requestStream = request.GetRequestStream())
            requestStream.Write(contentInBytes, 0, contentInBytes.Length);

        using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            return webResponse.StatusCode.ToString();
    }
    
    catch (WebException webException)
    {
        HttpStatusCode status = ((HttpWebResponse)webException.Response).StatusCode;

        if (status == HttpStatusCode.Unauthorized)
        {
            // The access token you presented has expired. Get a new one and then try sending
            // your notification again.
              
            // Because your cached access token expires after 24 hours, you can expect to get 
            // this response from WNS at least once a day.

            GetAccessToken(secret, sid);

            // We recommend that you implement a maximum retry policy.
            return PostToWns(uri, xml, secret, sid, notificationType, contentType);
        }
        else if (status == HttpStatusCode.Gone || status == HttpStatusCode.NotFound)
        {
            // The channel URI is no longer valid.

            // Remove this channel from your database to prevent further attempts
            // to send notifications to it.

            // The next time that this user launches your app, request a new WNS channel.
            // Your app should detect that its channel has changed, which should trigger
            // the app to send the new channel URI to your app server.

            return "";
        }
        else if (status == HttpStatusCode.NotAcceptable)
        {
            // This channel is being throttled by WNS.

            // Implement a retry strategy that exponentially reduces the amount of
            // notifications being sent in order to prevent being throttled again.

            // Also, consider the scenarios that are causing your notifications to be throttled. 
            // You will provide a richer user experience by limiting the notifications you send 
            // to those that add true value.

            return "";
        }
        else
        {
            // WNS responded with a less common error. Log this error to assist in debugging.

            // You can see a full list of WNS response codes here:
            // https://msdn.microsoft.com/library/windows/apps/hh868245.aspx#wnsresponsecodes

            string[] debugOutput = {
                                       status.ToString(),
                                       webException.Response.Headers["X-WNS-Debug-Trace"],
                                       webException.Response.Headers["X-WNS-Error-Description"],
                                       webException.Response.Headers["X-WNS-Msg-ID"],
                                       webException.Response.Headers["X-WNS-Status"]
                                   };
            return string.Join(" | ", debugOutput);            
        }
    }

    catch (Exception ex)
    {
        return "EXCEPTION: " + ex.Message;
    }
}

// Authorization
[DataContract]
public class OAuthToken
{
    [DataMember(Name = "access_token")]
    public string AccessToken { get; set; }
    [DataMember(Name = "token_type")]
    public string TokenType { get; set; }
}

private OAuthToken GetOAuthTokenFromJson(string jsonString)
{
    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
    {
        var ser = new DataContractJsonSerializer(typeof(OAuthToken));
        var oAuthToken = (OAuthToken)ser.ReadObject(ms);
        return oAuthToken;
    }
}

protected OAuthToken GetAccessToken(string secret, string sid)
{
    var urlEncodedSecret = HttpUtility.UrlEncode(secret);
    var urlEncodedSid = HttpUtility.UrlEncode(sid);

    var body = String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com", 
                             urlEncodedSid, 
                             urlEncodedSecret);

    string response;
    using (var client = new WebClient())
    {
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        response = client.UploadString("https://login.live.com/accesstoken.srf", body);
    }
    return GetOAuthTokenFromJson(response);
}
```

The following shows example content for an HTTP POST request for a toast push notification.

``` csharp
POST https://db3.notify.windows.com/?token=AgUAAADCQmTg7OMlCg%2fK0K8rBPcBqHuy%2b1rTSNPMuIzF6BtvpRdT7DM4j%2fs%2bNNm8z5l1QKZMtyjByKW5uXqb9V7hIAeA3i8FoKR%2f49ZnGgyUkAhzix%2fuSuasL3jalk7562F4Bpw%3d HTTP/1.1
Authorization: Bearer EgAaAQMAAAAEgAAACoAAPzCGedIbQb9vRfPF2Lxy3K//QZB79mLTgK
X-WNS-RequestForStatus: true
X-WNS-Type: wns/toast
Content-Type: text/xml
Host: db3.notify.windows.com
Content-Length: 196

<toast launch="">
  <visual lang="en-US">
    <binding template="ToastImageAndText01">
      <image id="1" src="World" />
      <text id="1">Hello</text>
    </binding>
  </visual>
</toast>
```

The following shows an example HTTP response, sent to the cloud server by WNS in response to the HTTP POST request.

``` csharp
HTTP/1.1 200 OK
Content-Length: 0
X-WNS-DEVICECONNECTIONSTATUS: connected
X-WNS-STATUS: received
X-WNS-MSG-ID: 3CE38FF109E03A74
X-WNS-DEBUG-TRACE: DB3WNS4011534
```

## Summary

In this Quickstart, you composed an HTTP POST request to send to WNS. WNS, in turn, delivers the notification to your app. By this point, you have registered your app, authenticated your cloud server with WNS, created XML content to define your notification, and sent that notification from your server to your app.

## Related topics

- [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh868221(v=win.10))
- [Push notification service request and response headers](/previous-versions/windows/apps/hh868245(v=win.10))
- [Windows Push Notification Services (WNS) overview](/previous-versions/windows/apps/hh913756(v=win.10))
- [Get started with Mobile Services](/azure/)
- [Push and periodic notifications sample](/samples/browse/)