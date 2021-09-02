---
Title: Troubleshooting tile, toast, and badge notifications (Windows Runtime apps) (Windows)
TOCTitle: Troubleshooting tile, toast, and badge notifications (Windows Runtime apps)
ms:assetid: F0F0791E-D887-43B0-8E7D-3CB762E68361
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn457491(v=Win.10)
ms:contentKeyID: 58982757
ms.date: 08/31/2015
mtps_version: v=Win.10
---

# Troubleshooting tile, toast, and badge notifications (Windows Runtime apps)

This topic discusses initial troubleshooting steps you should take when you encounter problems with tile, toast, and badge notifications, including the various notification methods: local, push, periodic, and scheduled notifications.

## Troubleshooting tile notifications

This section addresses some common errors you might encounter while working with tiles and tile templates. Unless specified otherwise, each solution applies to all notification delivery types: local, scheduled, periodic, or push notifications.

### Local tile notification is not displayed

The most common problem in this situation is that the XML used to define your notification is incorrect in some way. However, there are other possible causes, which these steps also walk you through:

  - Check user settings
  - Provide wide or large logo resources in the app manifest
  - Check your image sizes
  - Verify your URLs
  - Examine your image formats
  - Check the syntax of your XML
  - Check your notification's expiration time
  - Ensure that you've enabled the notification queue

### Check the user settings

**Possible cause**: The user or administrator has disabled notifications. Check whether the app has supplied a **Turn live tile on/off** option in the app bar, and that it isn't turned to "off". As for the administrator, there are several group policies which can disable notifications. Check with your administrator to ensure that notifications are enabled.

**Fix**: Enable notifications through the app bar or have an administrator enable notifications through group policy.

For more information, see [**TileUpdater.Setting**](https://msdn.microsoft.com/en-us/library/BR208631).

### Provide wide or large logo resources in the app manifest

**Possible cause**: The app manifest did not specify a default tile resource image for the tile size specified in the notification. For example, if a default wide tile image is not provided, the tile will never display wide-format notification templates. Ideally, tile notifications should provide templates in the notification payload for all possible tile sizes because, unless the tile intentionally uses only a medium image, the sender can never know which tile size will be displayed when the notification arrives. That setting is entirely up to the user.

**Fix**: In your notification payload, provide a version of the update for each type of default logo image that you provided in your manifest. Your tile can be resized to any size that has a default logo image.

### Check your image sizes

**Possible cause**: Each image in a notification must be smaller than 1024 x 1024 pixels and less than 200 KB in size. If any image in a notification exceeds any of these dimensions, the notification will be discarded.

**Fix**: Shrink your images.

For more information, see [Tile and toast image sizes](hh781198\(v=win.10\).md).

### Verify your URLs

**Possible cause**: URL syntax errors.

Images in notifications are specified through a [resource reference](hh831183\(v=win.10\).md) or a literal path. If a path is used, it must be given using one of these three protocols:

<table>
<colgroup>
<col style="width: 33%" />
<col style="width: 33%" />
<col style="width: 33%" />
</colgroup>
<thead>
<tr class="header">
<th>Prefix</th>
<th>Use</th>
<th>Notes</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>http:// and https://</td>
<td>Images stored online</td>
<td><p>These images might be cached locally, so your image server might not receive a request for the image. Query strings may be appended to these URLs. Make sure your web server returns the original image rather than a 404 if you choose to ignore the query string. An example query string: ?scale=100&amp;contrast=blk&amp;lang=en-US</p>
<p>Note that to retrieve any notification content from the Internet, your app must declare the &quot;Internet (Client)&quot; capability in its app manifest.</p></td>
</tr>
<tr class="even">
<td>ms-appx:///</td>
<td>Images included with your app's package</td>
<td>These images are part of your app's installation. Note that this reference requires a triple forward slash after the colon. After that triple forward slash, the Uniform Resource Identifier (URI) accepts either a forward slash (/) or a backward slash (\) to separate folders in a path, but most programming languages require you to use an escape character when you specify a backward slash (\\).</td>
</tr>
<tr class="odd">
<td>ms-appdata:///local/</td>
<td>Images saved locally by your app</td>
<td>This location corresponds to the folder returned by <a href="https://msdn.microsoft.com/library/br241621"><strong>Windows.Storage.ApplicationData.current.localFolder</strong></a>. Note that this reference requires a triple forward slash after the colon. Folder separators in the path must use escape characters (\\).</td>
</tr>
</tbody>
</table>

 

**Note**  The '/' character works as a separator in every specification type. We recommended that you use '/' instead of '\\' at all times to avoid inadvertent confusion with escape characters.

 

Well-formed examples:

<table>
<thead>
<tr class="header">
<th>URL</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>http://www.contoso.com/icon.jpg</td>
</tr>
<tr class="even">
<td>ms-appx:///images/icon.png</td>
</tr>
<tr class="odd">
<td>ms-appdata:///local/myDrawing.jpg</td>
</tr>
</tbody>
</table>

 

Badly-formed examples:

<table>
<thead>
<tr class="header">
<th>URL</th>
<th>Notes</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>http://www.contoso.com\fail.png</td>
<td>An HTTP path must use the / character. Do not use the \ character.</td>
</tr>
<tr class="even">
<td>http:www.contoso.com</td>
<td>An HTTP path requires a double-slash (//) after the colon.</td>
</tr>
<tr class="odd">
<td>&quot;ms-appdata:///local/c:\\images\\Drawing.jpg&quot;</td>
<td>An app cannot reference images outside of its local storage.</td>
</tr>
<tr class="even">
<td>&quot;ms-appx://images/triangle.png&quot;</td>
<td>Use a triple-slash rather than a double-slash with &quot;ms-appx:&quot;.</td>
</tr>
</tbody>
</table>

 

### Examine your image formats

**Possible cause**: Images are in an unsupported format.

Notifications can only use images in .gif, .png, or .jpg/.jpeg format. The format of the image must also match its extension. Merely renaming an unsupported file type with a supported extension won't work.

The most common cause of image format errors is serialization of bitmaps to the [**Windows.Storage.ApplicationData.Current.LocalFolder**](https://msdn.microsoft.com/library/br241621) storage. Be sure to invoke your preferred format, or the image will be stored as a Windows bitmap and its header will include "BMP"—an unsupported type.

**To verify**: First, verify that you can successfully send a text-only notification to narrow the problem down to the image. One way to verify your image format is to load your image into an image processing program and save as a .jpg. If you reference this new .jpg file in your notification and the error does not recur, it was probably an image format error. You can also open the file in the Microsoft Visual Studio binary editor and examine its header.

**Fix**: Change or correct your image formats.

### Check your XML syntax and content

**Possible cause**: XML syntax or validation errors.

Apart from basic syntax, make sure that your XML is complete and correct, especially if you've constructed the payload as a string without using the APIs or the [NotificationsExtensions](dn642158\(v=win.10\).md) library. Some common points of failure in the XML content include the following:

  - Case sensitivity. Tag names, attribute names, and attribute values are all case sensitive. Be sure that your XML has the correct casing.
  - A [**binding**](https://msdn.microsoft.com/en-us/library/BR212854) element must be provided for each tile size. You should provide a **binding** element for each of the tile sizes you support (that is, logo images you provided in your manifest) in each notification you send.
  - Text strings should not contain reserved XML characters. For example, you cannot italicize tile strings by including \<i\> and \</i\> tags. If you intend to show the literal characters "\<i\>", they should be properly escaped. For more information on escape characters in XML, see [XML Character Entities and XAML](https://go.microsoft.com/fwlink/p/?linkid=257891).
  - The values supplied for the *lang* attributes must conform to the [ITEF BCP 47](https://go.microsoft.com/fwlink/p/?linkid=241419) specification.
  - XML strings created locally (for local or scheduled notifications) must use the UTF-16 encoding. When sent through push notifications or polled from a URL, the strings should use the UTF-8 encoding.
  - If you include an [**image**](https://msdn.microsoft.com/en-us/library/BR212855) element in your XML payload with a non-empty *src* attribute, you must be sure to include a reference to a valid image or the notification will be dropped.

You can use the Event Log to check for errors when your tile notification does not display. Look for events involving your tile notification in the Event Viewer under Applications and Services Logs \> Microsoft \> Windows \> Apps \> Microsoft-Windows-TWinUI/Operational.

**To verify**: Use an XML syntax checker, such as the Visual Studio editor, to look for basic syntax errors. Look at the appropriate template reference ([**TileTemplateType**](https://msdn.microsoft.com/en-us/library/BR208621)) to make sure that you have the right number of images and that you're assigning the right images to the right image index.

**Fix**: Change your XML or use a different template to match your content. Also, consider using the [NotificationsExtensions](dn642158\(v=win.10\).md) library to avoid manipulating the XML directly.

### Ensure that your notification hasn't expired

**Possible cause**: The expiration time is set to too small a value.

If you set the expiration time in your notification through the [**ExpirationTime**](https://msdn.microsoft.com/en-us/library/BR208618) method (for a local notification) or the [X-WNS-TTL](hh868245\(v=win.10\).md) header field (in a push notification), be aware that the values represent milliseconds. For example, if you want a tile notification to last for exactly one hour, the value should be 60 \* 60 \* 1000 = 3600000.

**Fix**: Use a larger value.

### Ensure that you've enabled the notification queue if you want cycling notifications

**Possible cause**: The tile notification queue has not been enabled.

By default, tiles display only one update at a time and a new incoming notification replaces the existing one. If you want to display the last five notifications in a rotation, you must call [**TileUpdater.enableNotificationQueue(true)**](https://msdn.microsoft.com/en-us/library/BR208630) in your app's initiation code. This needs to be done only once in your app's lifetime. For more information, see [How to use the notification queue with local notifications](https://msdn.microsoft.com/en-us/library/Hh465429).

**Fix**: Call [**EnableNotificationQueue(true)**](https://msdn.microsoft.com/en-us/library/BR208630) in your initialization code. Also, ensure that notification tags are unique.

## Troubleshooting scheduled notifications

### A scheduled tile or toast does not appear

**Possible cause**: More often than not, if you experience problems with tile updates or toast notifications not appearing, the XML content of the notification is formatted incorrectly. Scheduled tile and toast notifications, as with non-scheduled notifications, must conform to the [tile](https://msdn.microsoft.com/en-us/library/BR212859) and [toast](https://msdn.microsoft.com/en-us/library/BR230849) XML schemas.

**Fix**: Test your XML through a local notification as a first step in debugging delivery problems with scheduled notifications. For more information, see the Local tile notification is not displayed or Local toast notification is not displayed section in this topic.

### The app's call to the AddToSchedule method fails

**Possible cause**: You've exceeded the maximum allowed number of scheduled notifications.

**Fix**: Both [**TileUpdater.AddToSchedule**](https://msdn.microsoft.com/en-us/library/Hh701671) and [**ToastNotifier.AddToSchedule**](https://msdn.microsoft.com/en-us/library/BR208654) will fail if you attempt to schedule more than 4096 notifications. Reduce your number of scheduled notifications.

**Possible cause**: Your notification is scheduled for a time in the past relative to the current system clock time.

**Fix**: Make sure that the scheduled notification time is in the future. Examine the system clock time.

## Troubleshooting periodic (polled) notifications

### Periodic notifications do not update the tile or badge

You might be encountering one or more of several issues that could prevent your periodic notifications from appearing:

  - The web service is not returning a valid XML document that conforms to the [tile XML schema](https://msdn.microsoft.com/en-us/library/BR212859). If you experience problems while implementing periodic notifications, first check that your tile's XML is formatted correctly. When debugging a problem with periodic notifications, as a first step, we recommended that you test your XML through a local notification. For more information, see the Local tile notification is not displayed section in this topic, as well as [Quickstart: Sending a tile update](hh868253\(v=win.10\).md).
  - The text returned from the poll request is not formatted as UTF-8. The UTF-8 encoding is required.
  - Your service is not correctly responding to the HTTP GET request used by Windows when it polls the provided URL for your service. Both the HTTP and HTTPS protocols are supported.
  - Your app did not declare Internet capability in its app manifest file (package.appxmanifest). In the Visual Studio manifest editor, you will find this option under the Capabilities tab as **Internet (Client)**. If this capability is not declared for the app, Windows will not poll your service.
  - Ensure that the values set by the X-WNS-Tag and X-WNS-Expires headers are formatted properly. X-WNS-Expires must use one of the following formats:
      - Sun, 06 Nov 1994 08:49:37 GMT
      - Sunday, 06-Nov-94 08:49:37 GMT
      - Sun Nov 6 08:49:37 1994

### Periodic updates are delayed

  - Windows can delay the polling of your URL by up to 15 minutes if necessary to optimize power and performance.
  - Your service was not available at the time your URL was contacted. When the service is not available, it will not be contacted again until the next polling interval.

## Troubleshooting push notifications

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
      - A [**binding**](https://msdn.microsoft.com/en-us/library/BR212854) element must be provided for each supported tile format. You should provide a **binding** element for each of the tile sizes you support in each notification you send.
      - Text strings should not contain reserved XML characters. For example, you cannot italicize tile or toast strings by including \<i\> and \</i\> tags. If you intend to show the literal characters "\<i\>", they should be properly escaped. For more information on escape characters in XML, see [XML Character Entities and XAML](https://go.microsoft.com/fwlink/p/?linkid=113597).
      - The values supplied for the *lang* attributes must conform to the [ITEF BCP 47](https://go.microsoft.com/fwlink/p/?linkid=241419) specification.
      - XML strings sent through push notifications should use the UTF-8 encoding.
      - If you include an [**image**](https://msdn.microsoft.com/en-us/library/BR212855) element in your XML payload with a non-empty src attribute, you must be sure to include a reference to a valid image or the notification will be dropped.
    
    For more information, see the [Tiles, toast, and badge schemas](https://msdn.microsoft.com/en-us/library/BR212853) documentation.

  - **Cause**: Improper use of push notification API parameters
    
    **Fix**: See the API documentation in the [**Windows.Networking.PushNotifications**](https://msdn.microsoft.com/en-us/library/BR241307) namespace for specifics.

  - **Cause**: Header type does not match notification content. If the X-WNS-Type header is not set to a value—tile, badge, or toast—that corresponds to the notification template specified in the payload, the notification will not be displayed. This mismatch will cause an error on the client and the notification will be dropped.
    
    **Fix**: Refer to [Push notification service request and response headers](hh868245\(v=win.10\).md) to ensure that your app server is using the correct value for the X-WNS-Type header.

  - **Cause**: The time to live (TTL) value, set in the [X-WNS-TTL](hh868245\(v=win.10\).md) header, is too small.
    
    **Fix**: Provide a larger TTL value, being aware that the value is given in seconds.

If you still do not see your notification displayed after addressing the items in the previous steps, see the troubleshooting steps for local notifications in the Local tile notification is not displayed section of this topic for further suggestions.

### Push notification returns a code other than "200 OK"

If WNS doesn't return "200 OK", your notification will not be delivered to the client. If the return code is in the 400s, then you, as a developer, should be able to fix the issue. For information on the meaning of specific codes, see [Windows Push Notification Services (WNS) response code reference](hh868245\(v=win.10\).md). For example code showing how to catch and handle these errors, see [Quickstart: Sending a push notification](hh868252\(v=win.10\).md) or download the [Push and periodic notifications sample](https://go.microsoft.com/fwlink/p/?linkid=231476).

**Note**  For errors not specifically listed here, see [**COM Error Codes (WPN, MBN, P2P, Bluetooth)**](https://msdn.microsoft.com/en-us/library/Hh404142).

 

  - Notification request returns "400 Bad Request"
  - Notification request returns "401 Unauthorized"
  - Notification request returns "401 Unauthorized", token is expired
  - Notification request returns "403 Forbidden"
  - Notification request returns "404 Not Found"
  - Notification request returns "406 Not Acceptable"
  - Notification request returns "410 Gone"

### Notification request returns "400 Bad Request"

**Cause**: The use of one or more WNS headers could be incorrect or the HTTP request was invalid.

**Fix**: Refer to [Push notification service request and response headers](hh868245\(v=win.10\).md) to ensure that your app server is using all custom headers as described.

### Notification request returns "401 Unauthorized"

**Cause**: Your app server must use the correct Package Security Identifier (Package SID) and secret key given to you when registered your app. If you have recently changed your secret key in the Windows Store Dashboard, you will also need to update your app server. For more information, see the [Push notifications overview](hh913756\(v=win.10\).md).

**Fix**: Visit the Windows StoreDashboard to verify your Package SID and secret.

### Notification request returns "401 Unauthorized", token is expired

**Cause**: An access token has a finite lifetime. If you send a notification with an expired access token, your app server's credentials are invalid and the notification cannot be sent.

**Fix**: Request a new access token from WNS by authenticating with WNS using your Package Security Identifier (Package SID) and secret key. For more information, see the [Windows Push Notification Services (WNS) notifications overview](hh913756\(v=win.10\).md).

### Notification request returns "403 Forbidden"

**Cause**: This error occurs when the access token that you presented does not match the credentials required to send notifications to the corresponding channel URL. Every app must be registered with the Windows Store to receive credentials for its app server. For each app, only the credentials provided by the Windows Store can be used to send notifications to that app and they can be used only for that particular app.

**Fix**: Log into the Windows Store Dashboard with your developer account. Select your app and click "Advanced Features" -\> "Manage your cloud service settings". Select "Identifying your app" to read instructions on updating your app manifest to match your cloud service credentials.

### Notification request returns "404 Not Found"

**Cause**: This error typically means that the channel URL is not formed correctly. The channel URL must never be tampered with or modified when you send a notification to WNS. The channel URL should always be treated as an opaque string—you never need to examine or even know its content.

**Fix**: Verify that your code is not modifying the channel URL either by changing one or more of its characters or changing its encoding.

### Notification request returns "406 Not Acceptable"

**Cause**: WNS has protective policies in place to prevent malicious apps from negatively impacting the service for other users and developers. An excessive number of notifications in too short a time period can result in WNS explicitly dropping notifications.

**Fix**: Review your notification frequency to see if it can be decreased or optimized to produce a better user experience.

### Notification request returns "410 Gone"

**Cause**: The channel URL has expired. No further notifications can be sent until your app runs and requests a new channel URL.

**Fix**: Your Windows Store app should request a channel URL each time it is launched. The channel URL that it is assigned is not guaranteed to remain the same. If the URL has changed, the client should update the information on its cloud server. For more information, see [How to manage channel expiration and renewal](hh868221\(v=win.10\).md).

### Errors when attempting to create a push notification channel

  - Creating a notification channel results in an ERROR\_NO\_NETWORK error
  - Creating a notification channel results in an WPN\_E\_CLOUD\_INCAPABLE error
  - Creating a notification channel results in an WPN\_E\_INVALID\_APP error

**Note**  For errors not specifically listed here, see [**COM Error Codes (WPN, MBN, P2P, Bluetooth)**](https://msdn.microsoft.com/en-us/library/Hh404142).

 

### Creating a notification channel results in an ERROR\_NO\_NETWORK error

**Cause**: WNS requires an Internet connection to create a notification channel.

**Fix**: Check your Internet connectivity.

### Creating a notification channel results in an WPN\_E\_CLOUD\_INCAPABLE error

**Cause**: Your app has not declared the Internet capability in its app manifest (package.appxmanifest).

**Fix**: Ensure that your app manifest has declared Internet capability. In the Visual Studio manifest editor, you will find this option under the Capabilities tab as **Internet (Client)**. For more information, see [**Capabilities**](https://msdn.microsoft.com/en-us/library/BR211422).

### Creating a notification channel results in an WPN\_E\_INVALID\_APP error

**Cause**: Your app must use a valid package name. If you have not received one yet, you can get it through the Windows Store portal under "Advanced Features".

**Fix**: For details on retrieving a Package Security Identifier (PKSID) for your Windows Store app, see [How to authenticate with the Windows Push Notification Service (WNS)](hh868206\(v=win.10\).md).

## Troubleshooting toast notifications

This section addresses some common errors you might encounter while working with toast and toast templates. Largely, most of the troubleshooting steps used with toast notifications are the same steps used with tile notifications. Unless specified otherwise, each solution applies to all notification delivery types: local, scheduled, or push notifications.

### Local toast notification is not displayed

The most common problem in this situation is that the XML used to define your notification is incorrect in some way. However, there are other possible causes, which these steps walk you through:

  - Check user settings
  - Check app manifest entries
  - Check your image sizes
  - Verify your URLs
  - Examine your image formats
  - Check the syntax of your XML
  - Check your notification's expiration time

### Check the user settings

**Possible cause**: The user or administrator has disabled notifications through settings. Check the global notification on/off switch and the per-application on/off switches in the PC Settings -\> Notifications page. As for the administrator, there are several group policies which can disable notifications. Check with your administrator to ensure that notifications are enabled.

**Fix**: Enable notifications through settings or have an administrator enable notifications through group policy.

For more information, see [Quickstart: Sending a toast notification](hh868254\(v=win.10\).md).

### Check app manifest entries

**Possible cause**: The app manifest did not have the proper information set to enable toast delivery. Ensure that the "Toast Capable" setting in the app manifest is set to "Yes". If any notification content, such as an image, is retrieved from the Internet, make sure that the "Internet (Client)" capability is declared in the app manifest.

**Fix**: Enable notification-specific entries in the app manifest.

For more information, see [Quickstart: Creating a default tile using the Visual Studio manifest editor](hh868247\(v=win.10\).md).

### Check your image sizes

**Possible cause**: Images for all notifications must be smaller than 1024 x 1024 pixels and less than 200 KB in size. If any image in a notification exceeds any of these dimensions, the notification will be discarded.

**Fix**: Shrink your images.

For more information, see [Tile and toast image sizes](hh781198\(v=win.10\).md).

### Verify your URLs

**Possible cause**: URL syntax errors.

Images in notifications are given as a [resource reference](hh831183\(v=win.10\).md) or a literal path. If a path is used, it must be given using one of these three protocols:

<table>
<colgroup>
<col style="width: 33%" />
<col style="width: 33%" />
<col style="width: 33%" />
</colgroup>
<thead>
<tr class="header">
<th>Prefix</th>
<th>Use</th>
<th>Notes</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>http:// and https://</td>
<td>Images stored online</td>
<td><p>These images might be cached locally, so your image server might not receive a request for the image. Query strings are appended to these URLs. Make sure your web server returns the original image rather than a 404 if you choose to ignore the query string. An example query string: ?scale=100&amp;contrast=blk&amp;lang=en-US</p>
<p>Note that to retrieve any notification content from the Internet, your app must declare the &quot;Internet (Client)&quot; capability in its app manifest.</p></td>
</tr>
<tr class="even">
<td>ms-appx:///</td>
<td>Images included with your app's package</td>
<td>The URI accepts either forward slash (/) or backward slash (\) to separate folders in a path, but most programming languages require you to use an escape character when you specify a backward slash (\\). Note that this reference requires a triple forward slash after the colon.</td>
</tr>
<tr class="odd">
<td>ms-appdata:///local/</td>
<td>Images saved locally by your app</td>
<td>This location corresponds to the folder returned by <a href="https://msdn.microsoft.com/library/br241621"><strong>Windows.Storage.ApplicationData.Current.LocalFolder</strong></a>. Folder separators in the path must use escape characters (\\). Note that this reference requires a triple forward slash after the colon.</td>
</tr>
</tbody>
</table>

 

**Note**  The '/' character works as a separator in every specification type. We recommended that you use '/' instead of '\\' at all times to avoid inadvertent confusion with escape characters.

 

Well-formed examples:

<table>
<thead>
<tr class="header">
<th>URL</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>http://www.contoso.com/icon.jpg</td>
</tr>
<tr class="even">
<td>ms-appx:///images/icon.png</td>
</tr>
<tr class="odd">
<td>ms-appdata:///local/myDrawing.jpg</td>
</tr>
</tbody>
</table>

 

Badly-formed examples:

<table>
<thead>
<tr class="header">
<th>URL</th>
<th>Notes</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>http://www.contoso.com\fail.png</td>
<td>An HTTP path must use the / character. Do not use the \ character.</td>
</tr>
<tr class="even">
<td>http:www.contoso.com</td>
<td>An HTTP path requires a double-slash (//) after the colon.</td>
</tr>
<tr class="odd">
<td>&quot;ms-appdata:///local/c:\\images\\Drawing.jpg&quot;</td>
<td>An app cannot reference images outside of its local storage.</td>
</tr>
<tr class="even">
<td>&quot;ms-appx://images/triangle.png&quot;</td>
<td>Use a triple-slash rather than a double-slash with &quot;ms-appx:&quot;.</td>
</tr>
</tbody>
</table>

 

### Examine your image formats

**Possible cause**: Images are in an unsupported format.

Notifications can only use images in .png, .jpg/.jpeg, or .gif format. The format of the image must also match its extension. Merely renaming an unsupported file type with a supported extension won't work.

The most common cause of image format errors is serialization of bitmaps to the [**Windows.Storage.ApplicationData.Current.LocalFolder**](https://msdn.microsoft.com/library/br241621) storage. Be sure to invoke your preferred format, or the image will be stored as a Windows bitmap and its header will include "BMP".

**To verify**: One way to verify your image format is to load your image into an image processing program and save as a .jpg. If you reference this new .jpg file in your notification and the error does not recur, it was probably an image format error. You can also open the file in the Visual Studio binary editor and examine its header.

**Fix**: Change or correct your image formats.

### Check your XML syntax and content

**Possible cause**: XML syntax or validation errors.

Apart from basic syntax, make sure that your XML is complete and correct. Some common points of failure in the XML content include the following:

  - Case sensitivity. Tag names, attribute names, and attribute values are all case sensitive. Be sure that your XML has the correct casing.
  - Text strings should not contain reserved XML characters. For example, you cannot italicize a string in a toast by including \<i\> and \</i\> tags. If you intend to show the literal characters "\<i\>", they should be properly escaped. For more information on escape characters in XML, see [XML Character Entities and XAML](https://go.microsoft.com/fwlink/p/?linkid=113597).
  - The values supplied for the *lang* attributes must conform to the [ITEF BCP 47](https://go.microsoft.com/fwlink/p/?linkid=241419) specification.
  - XML strings created locally (for local or scheduled notifications) must use the UTF-16 encoding. When sent through push notifications or polled from a URL, the strings should use the UTF-8 encoding.
  - If you include an [**image**](https://msdn.microsoft.com/en-us/library/BR230844) element in your XML payload with a non-empty src attribute, you must be sure to include a reference to a valid image or the notification will fail.

You can use the Event Log to check for errors when your toast notification does not display. Look for events involving your toast notification in the Event Viewer under Applications and Services Logs \> Microsoft \> Windows \> Apps \> Microsoft-Windows-TWinUI \> Operational.

**To verify**: Use an XML syntax checker, such as the Visual Studio editor, to look for basic syntax errors. Look at the appropriate template reference ([**ToastTemplateType**](https://msdn.microsoft.com/en-us/library/BR208660)) to ensure that you are assigning the correct item to the correct element.

**Fix**: Change your XML or use a different template to match your content.

### Ensure that your notification hasn't expired

**Possible cause**: The expiration time is set to too small a value.

If you set the expiration time in your notification through the [**ExpirationTime**](https://msdn.microsoft.com/en-us/library/BR208650) method (for a local notification) or the [X-WNS-TTL](hh868245\(v=win.10\).md) header field (in a push notification), be aware that the values represent milliseconds. For example, if you want a toast notification to last for exactly one hour, the value should be 60 \* 60 \* 1000 = 3600000.

**Fix**: Use a larger value.

## Reporting an issue

If you have tried the solutions suggested in this topic and have not resolved your issue, post a message on the [Microsoft forums](https://go.microsoft.com/fwlink/p/?linkid=250953) to discuss it with both Microsoft developers and other interested parties.

For push notifications, in addition to a description of the problem, you might be asked to provide your channel URL and an example of the response you received from WNS, including both the HTTP error codes and HTTP headers. There are specific headers that your app server should be logging when reporting an issue. For more information, see [Push notification service request and response headers](hh868245\(v=win.10\).md).

## Related topics

[App tiles and badges sample](https://go.microsoft.com/fwlink/p/?linkid=231469)

[Scheduled notifications sample](https://go.microsoft.com/fwlink/p/?linkid=241614)

[Toast notifications sample](https://go.microsoft.com/fwlink/p/?linkid=231503)

[Push and periodic notifications sample](https://go.microsoft.com/fwlink/p/?linkid=227874)

[Quickstart: Creating a default tile using the Visual Studio manifest editor](hh868247\(v=win.10\).md)

[Quickstart: Sending a tile update](hh868253\(v=win.10\).md)

[Quickstart: Sending a badge update](hh868225\(v=win.10\).md)

[Quickstart: Showing notifications on the lock screen](hh868216\(v=win.10\).md)

[Quickstart: Setting up periodic notifications](hh868228\(v=win.10\).md)

[The tile template catalog](hh761491\(v=win.10\).md)

[How to schedule a tile notification](hh868223\(v=win.10\).md)

[How to use the notification queue with local notifications](hh868234\(v=win.10\).md)

[Tiles XML schema](https://msdn.microsoft.com/en-us/library/BR212859)

[Tile and tile notification overview](hh779724\(v=win.10\).md)

[Badge overview](hh779719\(v=win.10\).md)

[Lock screen overview](hh779720\(v=win.10\).md)

[The notification queue](hh781199\(v=win.10\).md)

[Choosing a notification delivery method](hh779721\(v=win.10\).md)

[Guidelines for secondary tiles](https://msdn.microsoft.com/en-us/library/Hh465398)

