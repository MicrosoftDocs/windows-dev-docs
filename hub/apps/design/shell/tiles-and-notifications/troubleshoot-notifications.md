---
title: Troubleshooting WNS push notifications 
description: How to troubleshoot common errors with push notifications
ms:assetid: F0F0791E-D887-43B0-8E7D-3CB762E68361
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn457491(v=Win.10)
ms:contentKeyID: 58982757
ms.date: 10/06/2021
ms.topic: article
mtps_version: v=Win.10
---

# Troubleshooting WNS push notifications 

This topic discusses initial troubleshooting steps you should take when you encounter problems with tile, toast, and badge notifications, including the various notification methods: local, push, periodic, and scheduled notifications.

## Troubleshooting specific errors

This section addresses some common errors you might encounter while working with push notifications.

- Check your event logs
- Push notification receives a "200 OK" response, but does not display
- Push notification returns a code other than "200 OK"
- Errors when attempting to create a push notification channel

### Check your event logs

If tile or toast push notifications are not displaying as expected, have a look at the event logs.

- **If the notification is received but not shown**: Launch the Event Viewer and examine the *Microsoft-Windows-TWinUI/Operational* log under Applications and Services\\Microsoft\\Windows\\Apps.
- **If the notification is not received at all**: Launch the Event Viewer and examine the Operational log under Applications and Services\\Microsoft\\Windows\\PushNotifications-Platform.

### Push notification receives a "200 OK" response, but does not display

If Windows Push Notification Services (WNS) returns a "200 OK" response, then it will deliver the notification to the client if the client is online. If you have verified that the client is online but not displaying the notification, walk through these steps:

- **Cause**: XML errors in the notification content.

  **Fix**: Verify the basic XML syntax and make sure that your XML is complete and correct. Some common points of failure in the XML content include the following:

  - Case sensitivity. Tag names, attribute names, and attribute values are all case sensitive. Be sure that your XML has the correct casing.
  - A [**binding**](/uwp/schemas/tiles/tilesschema/element-binding) element must be provided for each supported tile format. You should provide a **binding** element for each of the tile sizes you support in each notification you send.
  - Text strings should not contain reserved XML characters. For example, you cannot italicize tile or toast strings by including \<i\> and \</i\> tags. If you intend to show the literal characters "\<i\>", they should be properly escaped. For more information on escape characters in XML, see [XML Character Entities and XAML](/previous-versions/dotnet/netframework-3.5/ms748250(v=vs.90)).
  - The values supplied for the *lang* attributes must conform to the [ITEF BCP 47](https://go.microsoft.com/fwlink/p/?linkid=241419) specification.
  - XML strings sent through push notifications should use the UTF-8 encoding.
  - If you include an [**image**](/uwp/schemas/tiles/tilesschema/element-image) element in your XML payload with a non-empty src attribute, you must be sure to include a reference to a valid image or the notification will be dropped.

- **Cause**: Improper use of push notification API parameters

  **Fix**: See the API documentation in the [**Windows.Networking.PushNotifications**](/uwp/api/Windows.Networking.PushNotifications) namespace for specifics.

- **Cause**: Header type does not match notification content. If the X-WNS-Type header is not set to a value—tile, badge, or toast—that corresponds to the notification template specified in the payload, the notification will not be displayed. This mismatch will cause an error on the client and the notification will be dropped.
  
  **Fix**: Refer to [Push notification service request and response headers](.\push-request-response-headers.md) to ensure that your app server is using the correct value for the X-WNS-Type header.

- **Cause**: The time to live (TTL) value, set in the X-WNS-TTL header, is too small.

  **Fix**: Provide a larger TTL value, being aware that the value is given in seconds.

If you still do not see your notification displayed after addressing the items in the previous steps, see the troubleshooting steps for local notifications in the Local tile notification is not displayed section of this topic for further suggestions.

### Push notification returns a code other than "200 OK"

If WNS doesn't return "200 OK", your notification will not be delivered to the client. If the return code is in the 400s, then you, as a developer, should be able to fix the issue.

> [!NOTE]
> For errors not specifically listed here, see [**COM Error Codes (WPN, MBN, P2P, Bluetooth)**](/windows/win32/com/com-error-codes-9).

- Notification request returns "400 Bad Request"
- Notification request returns "401 Unauthorized"
- Notification request returns "401 Unauthorized", token is expired
- Notification request returns "403 Forbidden"
- Notification request returns "404 Not Found"
- Notification request returns "406 Not Acceptable"
- Notification request returns "410 Gone"

### Notification request returns "400 Bad Request"

- **Cause**: The use of one or more WNS headers could be incorrect or the HTTP request was invalid.

  **Fix**: Refer to [Push notification service request and response headers](.\push-request-response-headers.md) to ensure that your app server is using all custom headers as described.

### Notification request returns "401 Unauthorized"

- **Cause**: Your app server must use the correct Package Security Identifier (Package SID) and secret key given to you when registered your app. If you have recently changed your secret key in the Windows Store Dashboard, you will also need to update your app server. 

  **Fix**: Visit the Windows StoreDashboard to verify your Package SID and secret.

### Notification request returns "401 Unauthorized", token is expired

- **Cause**: An access token has a finite lifetime. If you send a notification with an expired access token, your app server's credentials are invalid and the notification cannot be sent.

  **Fix**: Request a new access token from WNS by authenticating with WNS using your Package Security Identifier (Package SID) and secret key. For more information, see the [Windows Push Notification Services (WNS) overview](.\windows-push-notification-services--wns--overview.md)

### Notification request returns "403 Forbidden"

- **Cause**: This error occurs when the access token that you presented does not match the credentials required to send notifications to the corresponding channel URL. Every app must be registered with the Windows Store to receive credentials for its app server. For each app, only the credentials provided by the Windows Store can be used to send notifications to that app and they can be used only for that particular app.

  **Fix**: Log into the Windows Store Dashboard with your developer account. Select your app and click "Advanced Features" -\> "Manage your cloud service settings". Select "Identifying your app" to read instructions on updating your app manifest to match your cloud service credentials.

### Notification request returns "404 Not Found"

- **Cause**: This error typically means that the channel URL is not formed correctly. The channel URL must never be tampered with or modified when you send a notification to WNS. The channel URL should always be treated as an opaque string—you never need to examine or even know its content.

  **Fix**: Verify that your code is not modifying the channel URL either by changing one or more of its characters or changing its encoding.

### Notification request returns "406 Not Acceptable"

- **Cause**: WNS has protective policies in place to prevent malicious apps from negatively impacting the service for other users and developers. An excessive number of notifications in too short a time period can result in WNS explicitly dropping notifications.

  **Fix**: Review your notification frequency to see if it can be decreased or optimized to produce a better user experience.

### Notification request returns "410 Gone"

- **Cause**: The channel URL has expired. No further notifications can be sent until your app runs and requests a new channel URL.

  **Fix**: Your Windows Store app should request a channel URL each time it is launched. The channel URL that it is assigned is not guaranteed to remain the same. If the URL has changed, the client should update the information on its cloud server. 

### Errors when attempting to create a push notification channel

- Creating a notification channel results in an ERROR\_NO\_NETWORK error
- Creating a notification channel results in an WPN\_E\_CLOUD\_INCAPABLE error
- Creating a notification channel results in an WPN\_E\_INVALID\_APP error

> [!NOTE]
> For errors not specifically listed here, see [**COM Error Codes (WPN, MBN, P2P, Bluetooth)**](/windows/win32/com/com-error-codes-9).

### Creating a notification channel results in an ERROR\_NO\_NETWORK error

- **Cause**: WNS requires an Internet connection to create a notification channel.

  **Fix**: Check your Internet connectivity.

### Creating a notification channel results in an WPN\_E\_CLOUD\_INCAPABLE error

- **Cause**: Your app has not declared the Internet capability in its app manifest (package.appxmanifest).

  **Fix**: Ensure that your app manifest has declared Internet capability. In the Visual Studio manifest editor, you will find this option under the Capabilities tab as **Internet (Client)**. For more information, see [**Capabilities**](/uwp/schemas/appxpackage/appxmanifestschema/element-capabilities).

### Creating a notification channel results in an WPN\_E\_INVALID\_APP error

- **Cause**: Your app must use a valid package name. If you have not received one yet, you can get it through the Windows Store portal under "Advanced Features".

  **Fix**: For details on retrieving a Package Security Identifier (PKSID) for your Windows Store app, see [Windows Push Notification Services (WNS) overview](.\windows-push-notification-services--wns--overview.md).

## Reporting an issue

If you have tried the solutions suggested in this topic and have not resolved your issue, post a message on the [Microsoft forums](https://go.microsoft.com/fwlink/p/?linkid=250953) to discuss it with both Microsoft developers and other interested parties.

For push notifications, in addition to a description of the problem, you might be asked to provide your channel URL and an example of the response you received from WNS, including both the HTTP error codes and HTTP headers. There are specific headers that your app server should be logging when reporting an issue. For more information, see [Push notification service request and response headers](/previous-versions/windows/apps/hh868245(v=win.10)).