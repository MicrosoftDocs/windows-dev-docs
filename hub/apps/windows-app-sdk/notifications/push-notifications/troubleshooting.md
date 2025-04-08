---
title: Troubleshooting push notifications
description: Troubleshooting guide for push notifications
ms.topic: article
ms.date: 7/26/2021
keywords: push, notification
ms.localizationpriority: medium
---

# Troubleshooting push notifications

## WNS channel request errors

In case the **CreateChannelAsync** call fails, these are the common HRESULTS and our recommended actions.

| HRESULT     | Definition           | Description    |
|-------------|----------------------|----------------|
| 0x880403E8L | WNP_E_NOT_CONNECTED  | The app is not connected to the WNS Server after retries. |
| 0x880403E9L | WNP_E_RECONNECTING   | The app is in the process of reconnecting to the WNS Server. Try requesting a channelURI again after several minutes. |
| 0x880403FEL | WNP_E_BIND_USER_BUSY | The WNS client is having connectivity issues with the WNS server. Try requesting a channelURI again after several minutes. |

## WNS HTTP response codes

|HTTP response code           | Description    | Recommended action |
|-----------------------------|----------------|--------------------|
|200 Ok                       | The notification was accepted by WNS. | None required. |
|400 Bad Request              | One or more headers were specified incorrectly or conflict with another header. | Log the details of your request. Inspect your request and compare against this documentation. |
|401 Unauthorized             | The cloud service did not present a valid authentication ticket. The OAuth ticket may be invalid. | Request a valid access token by authenticating your cloud service using the access token request. |
|403 Forbidden                | The cloud service is not authorized to send a notification to this URI even though they are authenticated. | The access token provided in the request does not match the credentials of the app that requested the channel URI. Ensure that your package name in your app's manifest matches the cloud service credentials given to your app in the Dashboard. |
|404 Not Found                | The channel URI is not valid or is not recognized by WNS. | Log the details of your request. Do not send further notifications to this channel; notifications to this address will fail. |
|405 Method Not Allowed       | Invalid method (GET, CREATE); only POST | Log the details of your request. Switch to using HTTP POST. |
|406 Not Acceptable           | The cloud service exceeded its throttle limit. | Log the details of your request. Reduce the rate at which you are sending notifications. |
|410 Gone                     | The channel expired. | Log the details of your request. Do not send further notifications to this channel. Have your app request a new channel URI. |
|413 Request Entity Too Large | The notification payload exceeds the 5000 byte size limit. | Log the details of your request. Inspect the payload to ensure it is within the size limitations. |
|429 Monthly Quota Exceeded   | The app is over the monthly quota limit. | Wait until monthly quota limit is reset or move to a higher WNS tier. |
|500 Internal Server Error    | An internal failure caused notification delivery to fail. | Log the details of your request. Report this issue in the [Windows App SDK Issues](https://github.com/microsoft/WindowsAppSDK/issues) with the *area-Notifications* label. |
|503 Service Unavailable      | The server is currently unavailable. | Log the details of your request. Report this issue in the [Windows App SDK Issues](https://github.com/microsoft/WindowsAppSDK/issues) with the *area-Notifications* label. |
