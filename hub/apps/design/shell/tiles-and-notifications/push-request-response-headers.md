---
title: Push notification service request and response headers (Windows Runtime apps) (Windows)
description: WNS Request and response Headers
ms.topic: article
ms:assetid: 721585B5-A500-4dcc-9216-33785C4BACDC
ms:contentKeyID: 45725048
ms.date: 08/31/2015
mtps_version: v=Win.10
---

# Push notification service request and response headers (Windows Runtime apps)

This topic describes the service-to-service web APIs and protocols required to send a push notification.

See the [Windows Push Notification Services (WNS) overview](.\windows-push-notification-services--wns--overview.md) for a conceptual discussion of push notification and WNS concepts, requirements, and operation.

## Requesting and receiving an access token

This section describes the request and response parameters involved when you authenticate with the WNS.

### Access token request

An HTTP request is sent to WNS to authenticate the cloud service and retrieve an access token in return. The request is issued to the following fully qualified domain name (FQDN) by using Secure Sockets Layer (SSL).

    https://login.live.com/accesstoken.srf

### Access token request parameters

The cloud service submits these required parameters in the HTTP request body, using the "application/x-www-form-urlencoded" format. You must ensure that all parameters are URL encoded.

<table>
<colgroup>
<col style="width: 33%" />
<col style="width: 33%" />
<col style="width: 33%" />
</colgroup>
<thead>
<tr class="header">
<th>Parameter</th>
<th>Required/Optional</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>grant_type</td>
<td>Required</td>
<td>Must be set to &quot;client_credentials&quot;.</td>
</tr>
<tr class="even">
<td>client_id</td>
<td>Required</td>
<td>Package security identifier (SID) for your cloud service as assigned when you <a href="hh868206(v=win.10).md">registered your app</a> with the Windows Store.</td>
</tr>
<tr class="odd">
<td>client_secret</td>
<td>Required</td>
<td>Secret key for your cloud service as assigned when you <a href="hh868206(v=win.10).md">registered your app</a> with the Windows Store.</td>
</tr>
<tr class="even">
<td>scope</td>
<td>Required</td>
<td>Must be set to:
<ul>
<li><strong>Windows</strong>: &quot;notify.windows.com&quot;</li>
<li><strong>Windows Phone</strong>: &quot;notify.windows.com&quot; or &quot;s.notify.live.net&quot;</li>
</ul></td>
</tr>
</tbody>
</table>

 

### Access token response

WNS authenticates the cloud service and, if successful, responds with a "200 OK", including the access token. Otherwise, WNS responds with an appropriate HTTP error code as described in the [OAuth 2.0 protocol draft](https://go.microsoft.com/fwlink/p/?linkid=226787).

### Access token response parameters

An access token is returned in the HTTP response if the cloud service successfully authenticated. This access token can be used in notification requests until it expires. The HTTP response uses the "application/json" media type.

<table>
<thead>
<tr class="header">
<th>Parameter</th>
<th>Required/Optional</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>access_token</td>
<td>Required</td>
<td>The access token that the cloud service will use when it sends a notification.</td>
</tr>
<tr class="even">
<td>token_type</td>
<td>Optional</td>
<td>Always returned as &quot;bearer&quot;.</td>
</tr>
</tbody>
</table>

 

### Response code

<table>
<thead>
<tr class="header">
<th>HTTP response code</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>200 OK</td>
<td>The request was successful.</td>
</tr>
<tr class="even">
<td>400 Bad Request</td>
<td>The authentication failed. See the <a href="https://go.microsoft.com/fwlink/p/?linkid=226787">OAuth</a> draft Request for Comments (RFC) for the response parameters.</td>
</tr>
</tbody>
</table>

 

### Example

The following shows an example of a successful authentication response:

``` 
 HTTP/1.1 200 OK   
 Cache-Control: no-store
 Content-Length: 422
 Content-Type: application/json
 
 {
     "access_token":"EgAcAQMAAAAALYAAY/c+Huwi3Fv4Ck10UrKNmtxRO6Njk2MgA=", 
     "token_type":"bearer",
     "expires_in": 86400
 }
```

## Sending a notification request and receiving a response

This section describes the headers involved in an HTTP request to WNS to deliver a notification and those involved in the reply.

  - Send notification request
  - Send notification response
  - Unsupported HTTP features

### Send notification request

When sending a notification request, the calling app makes an HTTP request over SSL, addressed to the channel Uniform Resource Identifier (URI). "Content-Length" is a standard HTTP header that must be specified in the request. All other standard headers are either optional or not supported.

In addition, the custom request headers listed here can be used in the notification request. Some headers are required while others are optional.

### Request parameters

<table>
<thead>
<tr class="header">
<th>Header name</th>
<th>Required/Optional</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>Authorization</td>
<td>Required</td>
<td>Standard HTTP authorization header used to authenticate your notification request. Your cloud service provides its access token in this header.</td>
</tr>
<tr class="even">
<td>Content-Type</td>
<td>Required</td>
<td>Standard HTTP authorization header. For toast, tile, and badge notifications, this header must be set to &quot;text/xml&quot;. For raw notifications, this header must be set to &quot;application/octet-stream&quot;.</td>
</tr>
<tr class="odd">
<td>Content-Length</td>
<td>Required</td>
<td>Standard HTTP authorization header to denote the size of the request payload.</td>
</tr>
<tr class="even">
<td>X-WNS-Type</td>
<td>Required</td>
<td>Defines the notification type in the payload: tile, toast, badge, or raw.</td>
</tr>
<tr class="odd">
<td>X-WNS-Cache-Policy</td>
<td>Optional</td>
<td>Enables or disables notification caching. This header applies only to tile, badge, and raw notifications.</td>
</tr>
<tr class="even">
<td>X-WNS-RequestForStatus</td>
<td>Optional</td>
<td>Requests device status and WNS connection status in the notification response.</td>
</tr>
<tr class="odd">
<td>X-WNS-Tag</td>
<td>Optional</td>
<td>String used to provide a notification with an identifying label, used for tiles that support the notification queue. This header applies only to tile notifications.</td>
</tr>
<tr class="even">
<td>X-WNS-TTL</td>
<td>Optional</td>
<td>Integer value, expressed in seconds, that specifies the time to live (TTL).</td>
</tr>
</table>

 

### Important notes

  - Content-Length and Content-Type are the only standard HTTP headers that are included in the notification delivered to the client, regardless of whether other standard headers were included in the request.
  - All other standard HTTP headers are either ignored or return an error if the functionality is not supported.

### Authorization

The authorization header is used to specify the credentials of the calling party, following the [OAuth 2.0](https://go.microsoft.com/fwlink/p/?linkid=226787) authorization method for [bearer](https://go.microsoft.com/fwlink/p/?linkid=226848) tokens.

The syntax consists of a string literal "Bearer", followed by a space, followed by your access token. This access token is retrieved by issuing the access token request described above. The same access token can be used on subsequent notification requests until it expires.

This header is required.

    Authorization: Bearer <access-token>

### X-WNS-Type

These are the notification types supported by WNS. This header indicates the type of notification and how WNS should handle it. After the notification reaches the client, the actual payload is validated against this specified type. This header is required.

    X-WNS-Type: wns/toast | wns/badge | wns/tile | wns/raw

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>wns/badge</td>
<td>A notification to create a badge overlay on the tile. The Content-Type header included in the notification request must be set to &quot;text/xml&quot;.</td>
</tr>
<tr class="even">
<td>wns/tile</td>
<td>A notification to update the tile content. The Content-Type header included in the notification request must be set to &quot;text/xml&quot;.</td>
</tr>
<tr class="odd">
<td>wns/toast</td>
<td>A notification to raise a toast on the client. The Content-Type header included in the notification request must be set to &quot;text/xml&quot;.</td>
</tr>
<tr class="even">
<td>wns/raw</td>
<td>A notification which can contain a custom payload and is delivered directly to the app. The Content-Type header included in the notification request must be set to &quot;application/octet-stream&quot;.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Cache-Policy

When the notification target device is offline, WNS will cache one badge and one tile notification per app. If notification cycling is enabled for the app, WNS will cache up to five tile notifications. By default, raw notifications are not cached, but if raw notification caching is enabled, one raw notification is cached. Items are not held in the cache indefinitely and will be dropped after a reasonable period of time. Otherwise, the cached content is delivered when the device next comes online.

This header is optional and should be used only in cases where the cloud service wants to override the default caching behavior.

    X-WNS-Cache-Policy: cache | no-cache

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>cache</td>
<td><strong>Default</strong>. Notifications will be cached if the user is offline. This is the default setting for tile and badge notifications.</td>
</tr>
<tr class="even">
<td>no-cache</td>
<td>The notification will not be cached if the user is offline. This is the default setting for raw notifications.</td>
</tr>
</tbody>
</table>

### X-WNS-RequestForStatus

Specifies whether the response should include the device status and WNS connection status. This header is optional.

    X-WNS-RequestForStatus: true | false

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>true</td>
<td>Return the device status and notification status in the response.</td>
</tr>
<tr class="even">
<td>false</td>
<td><strong>Default</strong>. Do not return the device status and notification status.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Tag

Assigns a label to a notification. The tag is used in the replacement policy of the tile in the notification queue when the app has opted for notification cycling. If a notification with this tag already exists in the queue, a new notification with the same tag takes its place.

**Note**  This header is optional and used only when sending tile notifications.

 

**Note**  For Windows Phone Store apps, the X-WNS-Tag header can be used with the X-WNS-Group header to allow multiple notifications with the same tag to be shown in the action center.

    X-WNS-Tag: <string value>

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>string value</td>
<td>An alphanumeric string of no more than 16 characters.</td>
</tr>
</tbody>
</table>

 

### X-WNS-TTL

Specifies the TTL (expiration time) for a notification. This is not typically needed, but can be used if you want to ensure that your notifications are not displayed later than a certain time. The TTL is specified in seconds and is relative to the time that WNS receives the request. When a TTL is specified, the device will not display the notification after that time. Note that this could result in the notification never being shown at all if the TTL is too short. In general, an expiration time will be measured in at least minutes.

This header is optional. If no value is specified, the notification does not expire and will be replaced under the normal notification replacement scheme.

    X-WNS-TTL: <integer value>

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>integer value</td>
<td>The life span of the notification, in seconds, after WNS receives the request.</td>
</tr>
</tbody>
</table>

 

### X-WNS-SuppressPopup

**Note**  For Windows Phone Store apps, you have the option to suppress a toast notification's UI, instead sending the notification directly to the action center. This lets your notification be delivered silently, a potentially superior option for less urgent notifications. This header is optional and only used on Windows Phone channels. If you include this header on a Windows channel, your notification will be dropped and you will receive an error response from WNS.

    X-WNS-SuppressPopup: true | false

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>true</td>
<td>Send the toast notification directly to the action center; do not raise the toast UI.</td>
</tr>
<tr class="even">
<td>false</td>
<td><strong>Default</strong>. Raise the toast UI as well as adding the notification to the action center.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Group

**Note**  The action center for Windows Phone Store apps can display multiple toast notifications with the same tag only if they are labelled as belonging to different groups. For example, consider a recipe book app. Each recipe would be identified by a tag. A toast that contains a comment on that recipe would have the recipe's tag, but a comment group label. A toast that contains a rating for that recipe would again have that recipe's tag, but would have a rating group label. Those different group labels would allow both toast notifications to be shown at once in the action center. This header is optional.

    X-WNS-Group: <string value>

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>string value</td>
<td>An alphanumeric string of no more than 16 characters.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Match

**Note**  Used with the HTTP DELETE method to remove a specific toast, a set of toasts (by either tag or group), or all toasts from the action center for Windows Phone Store apps. This header can specify a group, a tag, or both. This header is required in an HTTP DELETE notification request. Any payload included with this notification request is ignored.

    X-WNS-Match: type:wns/toast;group=<string value>;tag=<string value> | type:wns/toast;group=<string value> | type:wns/toast;tag=<string value> | type:wns/toast;all

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>type:wns/toast;group=&lt;string value&gt;;tag=&lt;string value&gt;</td>
<td>Remove a single notification labelled with both the specified tag and group.</td>
</tr>
<tr class="even">
<td>type:wns/toast;group=&lt;string value&gt;</td>
<td>Remove all notifications labelled with the specified group.</td>
</tr>
<tr class="odd">
<td>type:wns/toast;tag=&lt;string value&gt;</td>
<td>Remove all notifications labelled with the specified tag.</td>
</tr>
<tr class="even">
<td>type:wns/toast;all</td>
<td>Clear all of your app's notifications from the action center.</td>
</tr>
</tbody>
</table>

 

### Send notification response

After WNS processes the notification request, it sends an HTTP message in response. This section discusses the parameters and headers that can be found in that response.

### Response parameters

<table>
<thead>
<tr class="header">
<th>Header name</th>
<th>Required/Optional</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>X-WNS-Debug-Trace</td>
<td>Optional</td>
<td>Debugging information that should be logged to help troubleshoot issues when reporting a problem.</td>
</tr>
<tr class="even">
<td>X-WNS-DeviceConnectionStatus</td>
<td>Optional</td>
<td>The device status, returned only if requested in the notification request through the X-WNS-RequestForStatus header.</td>
</tr>
<tr class="odd">
<td>X-WNS-Error-Description</td>
<td>Optional</td>
<td>A human-readable error string that should be logged to help with debugging.</td>
</tr>
<tr class="even">
<td>X-WNS-Msg-ID</td>
<td>Optional</td>
<td>A unique identifier for the notification, used for debugging purposes. When reporting a problem, this information should be logged to help in troubleshooting.</td>
</tr>
<tr class="odd">
<td>X-WNS-Status</td>
<td>Optional</td>
<td>Indicates whether WNS successfully received and processed the notification. When reporting a problem, this information should be logged to help in troubleshooting.</td>
</tr>
<tr class="odd">
<td>MS-CV</td>
<td>Optional</td>
<td>Debugging information that should be logged to help troubleshoot issues when reporting a problem.</td>
</tr>

</tbody>
</table>

 

### X-WNS-Debug-Trace

This header returns useful debugging information as a string. We recommend that this header be logged to help developers debug issues. This header, together with the X-WNS-Msg-ID header and MS-CV, are required when reporting an issue to WNS.

    X-WNS-Debug-Trace: <string value>

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>string value</td>
<td>An alphanumeric string.</td>
</tr>
</tbody>
</table>

 

### X-WNS-DeviceConnectionStatus

This header returns the device status to the calling application, if requested in the X-WNS-RequestForStatus header of the notification request.

    X-WNS-DeviceConnectionStatus: connected | disconnected | tempdisconnected

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>connected</td>
<td>The device is online and connected to WNS.</td>
</tr>
<tr class="even">
<td>disconnected</td>
<td>The device is offline and not connected to WNS.</td>
</tr>
<tr class="odd">
<td>tempconnected</td>
<td>The device temporarily lost connection to WNS, for instance when a 3G connection is dropped or the wireless switch on a laptop is thrown. It is seen by the Notification Client Platform as a temporary interruption rather than an intentional disconnection.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Error-Description

This header provides a human-readable error string that should be logged to help with debugging.

    X-WNS-Error-Description: <string value>

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>string value</td>
<td>An alphanumeric string.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Msg-ID

This header is used to provide the caller with an identifier for the notification. We recommended that this header be logged to help debug issues. This header, together with the X-WNS-Debug-Trace and MS-CV, are required when reporting an issue to WNS.

    X-WNS-Msg-ID: <string value>

<table>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>string value</td>
<td>An alphanumeric string of no more than 16 characters.</td>
</tr>
</tbody>
</table>

 

### X-WNS-Status

This header describes how WNS handled the notification request. This can be used rather than interpreting response codes as success or failure.

    X-WNS-Status: received | dropped | channelthrottled

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th>Value</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>received</td>
<td>The notification was received and processed by WNS.
<div class="alert">
<strong>Note</strong>  This does not guarantee that the device received the notification.
</div>
<div>
 
</div></td>
</tr>
<tr class="even">
<td>dropped</td>
<td>The notification was explicitly dropped because of an error or because the client has explicitly rejected these notifications. Toast notifications will also be dropped if the device is offline.</td>
</tr>
<tr class="odd">
<td>channelthrottled</td>
<td>The notification was dropped because the app server exceeded the rate limit for this specific channel.</td>
</tr>
</tbody>
</table>

### MS-CV
This header provides a Correlation Vector related to the request which is primarily used for debugging. If a CV is provided as part of the request then WNS will use this value, else WNS will generate and respond back with a CV. This header, together with the X-WNS-Debug-Trace and X-WNS-Msg-ID header, are required when reporting an issue to WNS.
> [!IMPORTANT]
> Please generate a new CV for each push notification request if you are providing your own CV.

    MS-CV: <string value>

| Value | Description|
|-------|------------|
|string value | Follows the [Correlation Vector standard](https://github.com/microsoft/CorrelationVector/blob/master/cV%20-%202.1.md)|

### Response codes

Each HTTP message contains one of these response codes. WNS recommends that developers log the response code for use in debugging. When developers report an issue to WNS, they are required to provide response codes and header information.

<table>
<thead>
<tr class="header">
<th>HTTP response code</th>
<th>Description</th>
<th>Recommended action</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>200 OK</td>
<td>The notification was accepted by WNS.</td>
<td>None required.</td>
</tr>
<tr class="even">
<td>400 Bad Request</td>
<td>One or more headers were specified incorrectly or conflict with another header.</td>
<td>Log the details of your request. Inspect your request and compare against this documentation.</td>
</tr>
<tr class="odd">
<td>401 Unauthorized</td>
<td>The cloud service did not present a valid authentication ticket. The OAuth ticket may be invalid.</td>
<td>Request a valid access token by authenticating your cloud service using the access token request.</td>
</tr>
<tr class="even">
<td>403 Forbidden</td>
<td>The cloud service is not authorized to send a notification to this URI even though they are authenticated.</td>
<td>The access token provided in the request does not match the credentials of the app that requested the channel URI. Ensure that your package name in your app's manifest matches the cloud service credentials given to your app in the Dashboard.</td>
</tr>
<tr class="odd">
<td>404 Not Found</td>
<td>The channel URI is not valid or is not recognized by WNS.</td>
<td>Log the details of your request. Do not send further notifications to this channel; notifications to this address will fail.</td>
</tr>
<tr class="even">
<td>405 Method Not Allowed</td>
<td>Invalid method (GET, CREATE); only POST (Windows or Windows Phone) or DELETE (Windows Phone only) is allowed.</td>
<td>Log the details of your request. Switch to using HTTP POST.</td>
</tr>
<tr class="odd">
<td>406 Not Acceptable</td>
<td>The cloud service exceeded its throttle limit.</td>
<td>Log the details of your request. Reduce the rate at which you are sending notifications.</td>
</tr>
<tr class="even">
<td>410 Gone</td>
<td>The channel expired.</td>
<td>Log the details of your request. Do not send further notifications to this channel. Have your app <a href="hh868221(v=win.10).md">request a new channel URI</a>.</td>
</tr>
<tr class="odd">
<td>413 Request Entity Too Large</td>
<td>The notification payload exceeds the 5000 byte size limit.</td>
<td>Log the details of your request. Inspect the payload to ensure it is within the size limitations.</td>
</tr>
<tr class="even">
<td>500 Internal Server Error</td>
<td>An internal failure caused notification delivery to fail.</td>
<td>Log the details of your request. Report this issue through the <a href="https://go.microsoft.com/fwlink/p/?linkid=241434">developer forums</a>.</td>
</tr>
<tr class="odd">
<td>503 Service Unavailable</td>
<td>The server is currently unavailable.</td>
<td>Log the details of your request. Report this issue through the <a href="https://go.microsoft.com/fwlink/p/?linkid=241434">developer forums</a>.</td>
</tr>
</tbody>
</table>

 

For detailed troubleshooting information concerning specific response codes, see [Troubleshooting tile, toast, and badge notifications](dn457491\(v=win.10\).md). Also see [**COM Error Codes (WPN, MBN, P2P, Bluetooth)**](https://msdn.microsoft.com/en-us/library/Hh404142).

### Unsupported HTTP features

The WNS Web Interface supports HTTP 1.1 but does not support the following features:
  - Chunking
  - Pipelining (POST is not idempotent)
  - Although supported, developers should disable Expect-100 as that introduces latency when sending a notification.