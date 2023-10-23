---
title: Push notification service request and response headers (Windows Runtime apps) (Windows)
description: This topic describes the service-to-service web APIs and protocols required to send a push notification.
ms.topic: article
ms.date: 10/06/2021
---

# Push notification service request and response headers (Windows Runtime apps)

This topic describes the service-to-service web APIs and protocols required to send a push notification.

See the [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md) for a conceptual discussion of push notification and WNS concepts, requirements, and operation.

## Requesting and receiving an access token

This section describes the request and response parameters involved when you authenticate with the WNS.

### Access token request

An HTTP request is sent to WNS to authenticate the cloud service and retrieve an access token in return. The request is issued to `https://login.live.com/accesstoken.srf` using Secure Sockets Layer (SSL).

### Access token request parameters

The cloud service submits these required parameters in the HTTP request body, using the "application/x-www-form-urlencoded" format. You must ensure that all parameters are URL encoded.

| Parameter     | Required | Description |
|---------------|----------|-------------|
| grant_type    | TRUE     | Must be set to `client_credentials`. |
| client_id     | TRUE     | Package security identifier (SID) for your cloud service as assigned when you registered your app with the Microsoft Store. |
| client_secret | TRUE     | Secret key for your cloud service as assigned when you registered your app with the Microsoft Store. |
| scope         | TRUE     | Must be set to `notify.windows.com` |

### Access token response

WNS authenticates the cloud service and, if successful, responds with a "200 OK", including the access token. Otherwise, WNS responds with an appropriate HTTP error code as described in the [OAuth 2.0 protocol draft](https://go.microsoft.com/fwlink/p/?linkid=226787).

### Access token response parameters

An access token is returned in the HTTP response if the cloud service successfully authenticated. This access token can be used in notification requests until it expires. The HTTP response uses the "application/json" media type.

| Parameter    | Required | Description |
|--------------|----------|-------------|
| access_token | TRUE     | The access token that the cloud service will use when it sends a notification. |
| token_type   | FALSE    | Always returned as `bearer`.

### Response code

| HTTP response code | Description                 |
|--------------------|-----------------------------|
| 200 OK             | The request was successful. |
| 400 Bad Request    | The authentication failed. See the [OAuth draft](https://go.microsoft.com/fwlink/p/?linkid=226787) Request for Comments (RFC) for response parameters. |

### Example

The following shows an example of a successful authentication response:

``` json
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

| Header name            | Required | Description |
|------------------------|----------|-------------|
| Authorization          | TRUE     | Standard HTTP authorization header used to authenticate your notification request. Your cloud service provides its access token in this header. |
| Content-Type           | TRUE     | Standard HTTP authorization header. For toast, tile, and badge notifications, this header must be set to `text/xml`. For raw notifications, this header must be set to `application/octet-stream`. |
| Content-Length         | TRUE     | Standard HTTP authorization header to denote the size of the request payload. |
| X-WNS-Type             | TRUE     | Defines the notification type in the payload: tile, toast, badge, or raw. |
| X-WNS-Cache-Policy     | FALSE    | Enables or disables notification caching. This header applies only to tile, badge, and raw notifications. |
| X-WNS-RequestForStatus | FALSE    | Requests device status and WNS connection status in the notification response. |
| X-WNS-Tag              | FALSE    | String used to provide a notification with an identifying label, used for tiles that support the notification queue. This header applies only to tile notifications. |
| X-WNS-TTL              | FALSE    | Integer value, expressed in seconds, that specifies the time to live (TTL). |
| MS-CV | FALSE |  [Correlation Vector](https://github.com/microsoft/CorrelationVector/blob/master/cV%20-%202.1.md) value used for your request. |


### Important notes

- Content-Length and Content-Type are the only standard HTTP headers that are included in the notification delivered to the client, regardless of whether other standard headers were included in the request.
- All other standard HTTP headers are either ignored or return an error if the functionality is not supported.
- Starting in February 2023,  WNS will cache only one tile notification when the device is offline. 

### Authorization

The authorization header is used to specify the credentials of the calling party, following the [OAuth 2.0](https://go.microsoft.com/fwlink/p/?linkid=226787) authorization method for [bearer](https://go.microsoft.com/fwlink/p/?linkid=226848) tokens.

The syntax consists of a string literal "Bearer", followed by a space, followed by your access token. This access token is retrieved by issuing the access token request described above. The same access token can be used on subsequent notification requests until it expires.

This header is required.

```json
Authorization: Bearer <access-token>
```

### X-WNS-Type

These are the notification types supported by WNS. This header indicates the type of notification and how WNS should handle it. After the notification reaches the client, the actual payload is validated against this specified type. This header is required.

```json
X-WNS-Type: wns/toast | wns/badge | wns/tile | wns/raw
```

| Value     | Description |
|-----------|-------------|
| wns/badge | A notification to create a badge overlay on the tile. The Content-Type header included in the notification request must be set to `text/xml`. |
| wns/tile  | A notification to update the tile content. The Content-Type header included in the notification request must be set to `text/xml`. |
| wns/toast | A notification to raise a toast on the client. The Content-Type header included in the notification request must be set to `text/xml`. |
| wns/raw   | A notification which can contain a custom payload and is delivered directly to the app. The Content-Type header included in the notification request must be set to `application/octet-stream`. |

### X-WNS-Cache-Policy

When the notification target device is offline, WNS will cache one badge, one tile, and one toast notification for each channel URI. By default, raw notifications are not cached, but if raw notification caching is enabled, one raw notification is cached. Items are not held in the cache indefinitely and will be dropped after a reasonable period of time. Otherwise, the cached content is delivered when the device next comes online.

```json
X-WNS-Cache-Policy: cache | no-cache
```

| Value    | Description |
|----------|-------------|
| cache    |Default. Notifications will be cached if the user is offline. This is the default setting for tile and badge notifications. |
| no-cache |The notification will not be cached if the user is offline. This is the default setting for raw notifications. |

### X-WNS-RequestForStatus

Specifies whether the response should include the device status and WNS connection status. This header is optional.

```json
    X-WNS-RequestForStatus: true | false
```

| Value | Description                                                       |
|-------|-------------------------------------------------------------------|
| true  | Return the device status and notification status in the response. |
| false | Default. Do not return the device status and notification status. |

### X-WNS-Tag

Assigns a [**tag**](/uwp/api/Windows.UI.Notifications.TileNotification#Windows_UI_Notifications_TileNotification_Tag) label to a notification. The tag is used in the replacement policy of the tile in the notification queue when the app has opted for notification cycling. If a notification with this tag already exists in the queue, a new notification with the same tag takes its place.

> [!NOTE]
> This header is optional and used only when sending tile notifications.

```json
    X-WNS-Tag: <string value>
```

| Value        | Description                                           |
|--------------|-------------------------------------------------------|
| string value | An alphanumeric string of no more than 16 characters. |

### X-WNS-TTL

Specifies the TTL (expiration time) for a notification. This is not typically needed, but can be used if you want to ensure that your notifications are not displayed later than a certain time. The TTL is specified in seconds and is relative to the time that WNS receives the request. When a TTL is specified, the device will not display the notification after that time. Note that this could result in the notification never being shown at all if the TTL is too short. In general, an expiration time will be measured in at least minutes.

This header is optional. If no value is specified, the notification does not expire and will be replaced under the normal notification replacement scheme.

X-WNS-TTL: `<integer value>`

| Value         | Description                                                                    |
|---------------|--------------------------------------------------------------------------------|
| integer value | The life span of the notification, in seconds, after WNS receives the request. |

### X-WNS-SuppressPopup

> [!NOTE]
> For Windows Phone Store apps, you have the option to suppress a toast notification's UI, instead sending the notification directly to the action center. This lets your notification be delivered silently, a potentially superior option for less urgent notifications. This header is optional and only used on Windows Phone channels. If you include this header on a Windows channel, your notification will be dropped and you will receive an error response from WNS.

X-WNS-SuppressPopup: true | false

| Value | Description                                                                           |
|-------|---------------------------------------------------------------------------------------|
| true  | Send the toast notification directly to the action center; do not raise the toast UI. |
| false | Default. Raise the toast UI as well as adding the notification to the action center.  |

### X-WNS-Group

> [!NOTE]
> The action center for Windows Phone Store apps can display multiple toast notifications with the same tag only if they are labelled as belonging to different groups. For example, consider a recipe book app. Each recipe would be identified by a tag. A toast that contains a comment on that recipe would have the recipe's tag, but a comment group label. A toast that contains a rating for that recipe would again have that recipe's tag, but would have a rating group label. Those different group labels would allow both toast notifications to be shown at once in the action center. This header is optional.

X-WNS-Group: `<string value>`

| Value        | Description                                           |
|--------------|-------------------------------------------------------|
| string value | An alphanumeric string of no more than 16 characters. |

### X-WNS-Match

> [!NOTE]
> Used with the HTTP DELETE method to remove a specific toast, a set of toasts (by either tag or group), or all toasts from the action center for Windows Phone Store apps. This header can specify a group, a tag, or both. This header is required in an HTTP DELETE notification request. Any payload included with this notification request is ignored.

X-WNS-Match: type:wns/toast;group=`<string value>`;tag=`<string value>` | type:wns/toast;group=`<string value>` | type:wns/toast;tag=`<string value>` | type:wns/toast;all

| Value                                                      | Description |
|------------------------------------------------------------|-------------|
| type:wns/toast;group=`<string value>`;tag=`<string value>` | Remove a single notification labelled with both the specified tag and group. |
| type:wns/toast;group=`<string value>`                      | Remove all notifications labelled with the specified group. |
| type:wns/toast;tag=`<string value>`                        | Remove all notifications labelled with the specified tag. |
| type:wns/toast;all                                         | Clear all of your app's notifications from the action center. |

### Send notification response

After WNS processes the notification request, it sends an HTTP message in response. This section discusses the parameters and headers that can be found in that response.

### Response parameters

| Header name                  | Required | Description |
|------------------------------|----------|-------------|
| X-WNS-Debug-Trace            | FALSE    | Debugging information that should be logged to help troubleshoot issues when reporting a problem. |
| X-WNS-DeviceConnectionStatus | FALSE    | The device status, returned only if requested in the notification request through the X-WNS-RequestForStatus header. |
| X-WNS-Error-Description      | FALSE    | A human-readable error string that should be logged to help with debugging. |
| X-WNS-Msg-ID                 | FALSE    | A unique identifier for the notification, used for debugging purposes. When reporting a problem, this information should be logged to help in troubleshooting. |
| X-WNS-Status                 | FALSE    | Indicates whether WNS successfully received and processed the notification. When reporting a problem, this information should be logged to help in troubleshooting. |
| MS-CV                        | FALSE    | Debugging information that should be logged to help troubleshoot issues when reporting a problem. |

### X-WNS-Debug-Trace

This header returns useful debugging information as a string. We recommend that this header be logged to help developers debug issues. This header, together with the X-WNS-Msg-ID header and MS-CV, are required when reporting an issue to WNS.

X-WNS-Debug-Trace: `<string value>`

| Value        | Description             |
|--------------|-------------------------|
| string value | An alphanumeric string. |

### X-WNS-DeviceConnectionStatus

This header returns the device status to the calling application, if requested in the X-WNS-RequestForStatus header of the notification request.

X-WNS-DeviceConnectionStatus: connected | disconnected | tempdisconnected

| Value         | Description |
|---------------|-------------|
| connected     | The device is online and connected to WNS. |
| disconnected  | The device is offline and not connected to WNS. |
| tempconnected | The device temporarily lost connection to WNS, for instance when a 3G connection is dropped or the wireless switch on a laptop is thrown. It is seen by the Notification Client Platform as a temporary interruption rather than an intentional disconnection. |

### X-WNS-Error-Description

This header provides a human-readable error string that should be logged to help with debugging.

X-WNS-Error-Description: `<string value>`

| Value        | Description             |
|--------------|-------------------------|
| string value | An alphanumeric string. |

### X-WNS-Msg-ID

This header is used to provide the caller with an identifier for the notification. We recommended that this header be logged to help debug issues. This header, together with the X-WNS-Debug-Trace and MS-CV, are required when reporting an issue to WNS.

X-WNS-Msg-ID: `<string value>`

| Value        | Description                                           |
|--------------|-------------------------------------------------------|
| string value | An alphanumeric string of no more than 16 characters. |

### X-WNS-Status

This header describes how WNS handled the notification request. This can be used rather than interpreting response codes as success or failure.

X-WNS-Status: received | dropped | channelthrottled

| Value            | Description |
|------------------|-------------|
| received         | The notification was received and processed by WNS. **Note**: This does not guarantee that the device received the notification. |
| dropped          | The notification was explicitly dropped because of an error or because the client has explicitly rejected these notifications. Toast notifications will also be dropped if the device is offline. |
| channelthrottled | The notification was dropped because the app server exceeded the rate limit for this specific channel. |

### MS-CV
This header provides a Correlation Vector related to the request which is primarily used for debugging. If a CV is provided as part of the request then WNS will use this value, else WNS will generate and respond back with a CV. This header, together with the X-WNS-Debug-Trace and X-WNS-Msg-ID header, are required when reporting an issue to WNS.
> [!IMPORTANT]
> Please generate a new CV for each push notification request if you are providing your own CV.

MS-CV: `<string value>`

| Value        | Description |
|--------------|-------------|
| string value | Follows the [Correlation Vector standard](https://github.com/microsoft/CorrelationVector/blob/master/cV%20-%202.1.md) |

### Response codes

Each HTTP message contains one of these response codes. WNS recommends that developers log the response code for use in debugging. When developers report an issue to WNS, they are required to provide response codes and header information.

| HTTP response code           | Description | Recommended action |
|------------------------------|-------------|--------------------|
| 200 OK                       | The notification was accepted by WNS. | None required. |
| 400 Bad Request              | One or more headers were specified incorrectly or conflict with another header. | Log the details of your request. Inspect your request and compare against this documentation. |
| 401 Unauthorized             | The cloud service did not present a valid authentication ticket. The OAuth ticket may be invalid. | Request a valid access token by authenticating your cloud service using the access token request. |
| 403 Forbidden                | The cloud service is not authorized to send a notification to this URI even though they are authenticated. | The access token provided in the request does not match the credentials of the app that requested the channel URI. Ensure that your package name in your app's manifest matches the cloud service credentials given to your app in the Dashboard. |
| 404 Not Found                | The channel URI is not valid or is not recognized by WNS. | Log the details of your request. Do not send further notifications to this channel; notifications to this address will fail. |
| 405 Method Not Allowed       | Invalid method (GET, CREATE); only POST (Windows or Windows Phone) or DELETE (Windows Phone only) is allowed. | Log the details of your request. Switch to using HTTP POST. |
| 406 Not Acceptable           | The cloud service exceeded its throttle limit. | Please send your request after the Retry-After header value in the response |
| 410 Gone                     | The channel expired. | Log the details of your request. Do not send further notifications to this channel. Have your app request a new channel URI. |
| 410 Domain Blocked                     | The sending domain has been blocked by WNS. | Do not send further notifications to this channel. The sending domain has been blocked by WNS for abusing push notifications. |
| 413 Request Entity Too Large | The notification payload exceeds the 5000 byte size limit. | Log the details of your request. Inspect the payload to ensure it is within the size limitations. |
| 500 Internal Server Error    | An internal failure caused notification delivery to fail. | Log the details of your request. Report this issue through the developer forums. |
| 503 Service Unavailable      | The server is currently unavailable. | Log the details of your request. Report this issue through the developer forums. If the Retry-After header is observed then please send your request after the Retry-After header value in the response. |

### Unsupported HTTP features

The WNS Web Interface supports HTTP 1.1 but does not support the following features:

- Chunking
- Pipelining (POST is not idempotent)
- Although supported, developers should disable Expect-100 as that introduces latency when sending a notification.
