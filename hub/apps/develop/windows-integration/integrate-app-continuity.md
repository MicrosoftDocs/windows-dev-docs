---
title: Use WNS Notifications to implement Cross Device Resume (XDR)
description: Learn how to implement Cross Device Resume (XDR) using Windows Push Notification Service (WNS) raw notifications.
#customer intent: As a Windows developer, I want to learn how to update my app to integrate application continuity with Windows Push Notifications raw notifications.
author: drewbatgit
ms.author: drewbat
ms.date: 11/03/2025
ms.topic: how-to
---

# Use WNS Notifications to implement Cross Device Resume (XDR)

This step-by-step guide for first and third parties provides a detailed overview on how to integrate application continuity (resume) with Windows Push Notification Service (WNS) raw notifications. It includes prerequisites, references to relevant public documentation, and code snippets for making POST requests to the channel URI.

## Onboarding to Resume in Windows

Resume is a Limited Access Feature (LAF). To gain access to this feature, you need to get approval from Microsoft to enable your application on Windows.

To request access, email <wincrossdeviceapi@microsoft.com> with the following information: 

- Status of your application WNS registration and provide your application “Package SID”. 
- Description of your user experience.
- Screenshot of your application where user is performing an action that can be resumed on their Windows PC.

If Microsoft approves your request, you receive instructions on how to unlock the feature. Approvals are based on your communication.

## Prerequisites

Before proceeding with the integration, ensure the following tasks are completed: 

- Register Application with the Windows Push Notification Service (WNS): You must register your application with WNS to receive notifications. See [Send notifications to Universal Windows Platform apps using Azure Notification Hubs](/azure/notification-hubs/notification-hubs-windows-store-dotnet-get-started-wns-push-notification) for more information.
- Obtain Access Credentials: Acquire the Package Security Identifier (SID) and client secret from the Azure portal. 
- Configure the Channel URI: Ensure the app can request and store the channel URI for notifications. See [How to request, create, and save a notification channel](/windows/apps/design/shell/tiles-and-notifications/request-create-save-notification-channel) for more information.
- WNS notifications use XML payloads for transmission. See [App notification content](/windows/apps/develop/notifications/app-notifications/app-notifications-content) for more information. For Resume, we are employing raw notifications. Ensure your Windows application supports payload associated with wns/raw notification. See [Raw notification overview](/windows/apps/design/shell/tiles-and-notifications/raw-notification-overview) for more information.

## Implementation steps

The following steps outline the process to integrate application continuity using WNS raw notifications.

### Step 1 - Configuring channel URI

Configure the Channel URI from your Windows application and send it to your app server: Ensure the app can request and store the channel URI for notifications. For more information, see [How to request, create, and save a notification channel](/windows/apps/design/shell/tiles-and-notifications/request-create-save-notification-channel).

### Step 2 - Initiate resume scenario

From your mobile application, call the necessary application service API to initiate the WNS-based resume request.

### Step 3 - Set up the HTTP request

When the app server receives the API request from the mobile client, it prepares the POST request to the WNS channel URI. The request must include the required headers, such as Authorization, Content-Type, and X-WNS-Type. To add your new header, include it in the request header configuration.

### Step 3.1 - Example for sending resume POST request

The following code snippets show how to send a new WNS notification with a resume header by using Python and JavaScript.

The following Python code snippet demonstrates how to send a WNS raw notification with the necessary headers for application continuity:

```python
import requests
# Define the channel URI
channel_uri = "[URL]"
# Define the notification payload
payload = """
Sample Notification
This is a sample message
"""
# Define the headers
headers = {
    "Content-Type": "application/octet-stream",
    "X-WNS-Type": "wns/raw",
    "Authorization": "Bearer YOUR_ACCESS_TOKEN",
    "X-WNS-RawNotificationType": "wns/raw/resume",
    "X-WNS-ResumeMetadata": {"title":"Continue call from…","expiry":"300", "type":"1"},
}
# Send the POST request
response = requests.post(channel_uri, data=payload, headers=headers)
# Print the response status
print(f"Response Status: {response.status_code}")
print(f"Response Body: {response.text}")
```

The following JavaScript code snippet demonstrates how to send a WNS raw notification with the necessary headers for application continuity:

```javascript
const axios = require('axios');

// Define the channel URI
const channelUri = "[URL]";

// Define the notification payload
const payload = `Sample Notification
This is a sample message`;

// Define the headers
const headers = {
  "Content-Type": "application/octet-stream",
  "X-WNS-Type": "wns/raw",
  "Authorization": "Bearer YOUR_ACCESS_TOKEN",
  "X-WNS-RawNotificationType": "wns/raw/resume",
  "X-WNS-ResumeMetadata": JSON.stringify({
    title: "Continue call from…",
    expiry: "300",
    type: "1"
  })
};

// Send the POST request
axios.post(channelUri, payload, { headers })
  .then(response => {
    console.log(`Response Status: ${response.status}`);
    console.log(`Response Body: ${response.data}`);
  })
  .catch(error => {
    console.error(`Error Status: ${error.response?.status}`);
    console.error(`Error Body: ${error.response?.data}`);
  });
```

The "**X-WNS-RawNotificationType**" header specifies the type of raw notification you're sending. You usually don't need to include this header, but it helps you categorize the notification for different purposes, such as "Resume". Possible values for this header might include types like **"wns/raw/resume,"** which signals the application continuation from Windows.

The "**X-WNS-ResumeMetadata**" header provides metadata about the resume notification you're sending. While not strictly required, it enhances the notification by adding information such as a title or expiration time. You pass the metadata as a JSON/Dictionary object (any other type throws validation exception) and it can include fields such as:

- **title**: A descriptive title for the notification (for example, "Continue call from...").
- **expiry**: The lifespan of the notification in seconds (for example, "300" for 5 minutes).
- **type**: Notification type - 1 for new resume request, 2 for delete (no action if missing).

The following JavaScript code snippet demonstrates how to send a WNS raw notification with the necessary headers for updating a resume notification:

```javascript
const axios = require('axios');

// Define the channel URI
const channelUri = "[URL]";

// Define the notification payload
const payload = `Sample Notification
This is a sample message`;

// Define the headers
const headers = {
  "Content-Type": "application/octet-stream",
  "X-WNS-Type": "wns/raw",
  "Authorization": "Bearer YOUR_ACCESS_TOKEN",
  "X-WNS-RawNotificationType": "wns/raw/resume",
  "X-WNS-ResumeMetadata": JSON.stringify({
    title: "Continue call from…",
    expiry: "300",
    type: "2"  // 2-represents update type.
  })
};

// Send the POST request
axios.post(channelUri, payload, { headers })
  .then(response => {
    console.log(`Response Status: ${response.status}`);
    console.log(`Response Body: ${response.data}`);
  })
  .catch(error => {
    console.error(`Error Status: ${error.response?.status}`);
    console.error(`Error Body: ${error.response?.data}`);
  });
```

The following JavaScript code snippet demonstrates how to send a WNS raw notification with the necessary headers for deleting a resume notification:

```javascript
const axios = require('axios');

// Define the channel URI
const channelUri = "[URL]";

// Define the notification payload
const payload = `Sample Notification
This is a sample message`;

// Define the headers
const headers = {
  "Content-Type": "application/octet-stream",
  "X-WNS-Type": "wns/raw",
  "Authorization": "Bearer YOUR_ACCESS_TOKEN",
  "X-WNS-RawNotificationType": "wns/raw/resume",
  "X-WNS-ResumeMetadata": JSON.stringify({
    title: "Continue call from…",
    expiry: "300",
    type: "3"  // 3-represents delete type.
  })
};

// Send the POST request
axios.post(channelUri, payload, { headers })
  .then(response => {
    console.log(`Response Status: ${response.status}`);
    console.log(`Response Body: ${response.data}`);
  })
  .catch(error => {
    console.error(`Error Status: ${error.response?.status}`);
    console.error(`Error Body: ${error.response?.data}`);
  });
```

Both headers offer flexibility in tailoring raw notifications to fit an application's functional requirements. 

### Step 4 - Validate the implementation

Ensure your application successfully registers for notifications and that the resume headers are included in the POST request. You can use tools like vscode REST Client, Postman, or Fiddler to inspect the HTTP request and response.

Here's an example of sending a request using the Visual Studio Code REST Client extension. The `X-WNS-RawNotificationType` and `X-WNS-ResumeMetadata` headers are required for application continuity:

```console
POST {{channel_uri}}
Content-Type: application/octet-stream
X-WNS-Type: wns/raw
X-WNS-RequestForStatus: true
X-WNS-RawNotificationType: wns/raw/resume
X-WNS-ResumeMetadata: {"title": "Continue call from...", "expiry": "300", "type": "1"}
Authorization: Bearer {{bearer}}

[{"hello"}]
```

The response from the VS Code REST client is displayed in the same format as existing notification responses. For details regarding status codes, see the reference link: [Send a Windows Push Notification Services (WNS) native notification](/rest/api/notificationhubs/send-wns-native-notification).

## Conclusion

By following the steps outlined in this guide, first and third parties can successfully add custom headers to WNS notifications. Ensure that you fulfill all prerequisites and test thoroughly to guarantee seamless integration.

For any queries or assistance regarding the implementation, contact our team at the following:

**Email: wincrossdeviceapi@microsoft.com**

We're here to help ensure a smooth integration process.

## Related content

- [Raw notification overview](/windows/apps/design/shell/tiles-and-notifications/raw-notification-overview)
- [How to request, create, and save a notification channel](/windows/apps/design/shell/tiles-and-notifications/request-create-save-notification-channel)
- Overview of WNS - [Send notifications to Universal Windows Platform apps using Azure Notification Hubs](/azure/notification-hubs/notification-hubs-windows-store-dotnet-get-started-wns-push-notification)
