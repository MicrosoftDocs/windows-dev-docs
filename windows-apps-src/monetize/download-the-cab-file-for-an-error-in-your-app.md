---
ms.assetid:
description: Use this method in the Microsoft Store analytics API to download the CAB file for an error in your app.
title: Download the CAB file for an error in your app
ms.date: 06/16/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store analytics API, download CAB
ms.localizationpriority: medium
---
# Download the CAB file for an error in your app

Use this method in the Microsoft Store analytics API to download the CAB file that is associated with a particular error in your app that has been reported to Partner Center. This method can only download the CAB file for an app error that occurred in the last 30 days. CAB file downloads are also available in the **Failures** section of the [Health report](../publish/health-report.md) in Partner Center.

Before you can use this method, you must first use the [get details for an error in your app](get-details-for-an-error-in-your-app.md) method to retrieve the ID of the CAB file you want to download.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID of the CAB file you want to download. To get this ID, use the [get details for an error in your app](get-details-for-an-error-in-your-app.md) method to retrieve details for a specific error in your app, and use the **cabId** value in the response body of that method.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/cabdownload``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  |
|---------------|--------|---------------|------|
| applicationId | string | The Store ID of the app for which you want to download a CAB file. The Store ID is available on the [App identity page](../publish/view-app-identity-details.md) of Partner Center. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| cabId | string | The unique ID of the CAB file you want to download. To get this ID, use the [get details for an error in your app](get-details-for-an-error-in-your-app.md) method to retrieve details for a specific error in your app, and use the **cabId** value in the response body of that method. |  Yes  |

 
### Request example

The following example demonstrates how to download a CAB file using this method. Replace the *applicationId* and *cabId* parameters with the appropriate values for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/cabdownload?applicationId=9NBLGGGZ5QDR&cabId=1336373323853 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

This method returns a 302 (redirect) response code, and the **Location** header in the response is assigned to the shared access signature (SAS) URI of the CAB file. The caller is redirected to this URI to automatically download the CAB file.

## Related topics

* [Health report](../publish/health-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data](get-error-reporting-data.md)
* [Get details for an error in your app](get-details-for-an-error-in-your-app.md)
* [Get the stack trace for an error in your app](get-the-stack-trace-for-an-error-in-your-app.md)
