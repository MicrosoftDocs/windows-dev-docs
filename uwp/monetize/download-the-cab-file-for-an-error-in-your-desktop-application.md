---
description: Use this method in the Microsoft Store analytics API to download the CAB file for an error in your desktop application.
title: Download the CAB file for an error in your desktop application
ms.date: 03/06/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store analytics API, download CAB, desktop application
ms.localizationpriority: medium
---
# Download the CAB file for an error in your desktop application

Use this method in the Microsoft Store analytics API to download the CAB file that is associated with a particular error for a desktop application that you have added to the [Windows Desktop Application program](/windows/desktop/appxpkg/windows-desktop-application-program). This method can only download the CAB file for an app error that occurred in the last 30 days. CAB file downloads are also available in the [Health report](/windows/desktop/appxpkg/windows-desktop-application-program) for desktop applications in Partner Center.

Before you can use this method, you must first use the [get details for an error in your desktop application](get-details-for-an-error-in-your-desktop-application.md) method to retrieve the ID hash of the CAB file you want to download.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID hash of the CAB file you want to download. To get this value, use the [get details for an error in your desktop application](get-details-for-an-error-in-your-desktop-application.md) method to retrieve details for a specific error in your app, and use the **cabIdHash** value in the response body of that method.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/cabdownload``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  |
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the desktop application for which you want to download a CAB file. To get the product ID of a desktop application, open any [Partner Center analytics report for your desktop application](/windows/desktop/appxpkg/windows-desktop-application-program) (such as the **Health report**) and retrieve the product ID from the URL. |  Yes  |
| cabIdHash | string | The unique ID hash of the CAB file you want to download. To get this value, use the [get details for an error in your desktop application](get-details-for-an-error-in-your-desktop-application.md) method to retrieve details for a specific error in your application, and use the **cabIdHash** value in the response body of that method. |  Yes  |


### Request example

The following example demonstrates how to download a CAB file using this method. Replace the *applicationId* and *cabIdHash* parameters with the appropriate values for your desktop application.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/desktop/cabdownload?applicationId=10238467886765136388&cabIdHash=54ffb83a-e159-41d2-8158-f36f306cc01e HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

This method returns a 302 (redirect) response code, and the **Location** header in the response is assigned to the shared access signature (SAS) URI of the CAB file. The caller is redirected to this URI to automatically download the CAB file.

## Related topics

* [Health report](../publish/health-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data for your desktop application](get-desktop-application-error-reporting-data.md)
* [Get details for an error in your desktop application](get-details-for-an-error-in-your-desktop-application.md)
* [Get the stack trace for an error in your desktop application](get-the-stack-trace-for-an-error-in-your-desktop-application.md)