---
author: Xansky
description: Use this method in the Microsoft Store analytics API to download the CAB file for an error in your Xbox One game.
title: Download the CAB file for an error in your Xbox One game
ms.author: mhopkins
ms.date: 11/06/2018
ms.topic: article


keywords: windows 10, uwp, Microsoft Store analytics API, download CAB
ms.localizationpriority: medium
---

# Download the CAB file for an error in your Xbox One game

Use this method in the Microsoft Store analytics API to download the CAB file that is associated with a particular error in your Xbox One game that was ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics Partner Center dashboard. This method can only download the CAB file for an error that occurred in the last 30 days.

Before you can use this method, you must first use the [get details for an error in your Xbox One game](get-details-for-an-error-in-your-xbox-one-game.md) method to retrieve the ID of the CAB file you want to download.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID of the CAB file you want to download. To get this ID, use the [get details for an error in your Xbox One game](get-details-for-an-error-in-your-xbox-one-game.md) method to retrieve details for a specific error in your app, and use the **cabId** value in the response body of that method.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/cabdownload``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  |
|---------------|--------|---------------|------|
| applicationId | string | The product ID of the Xbox One game for which you are downloading the CAB file. To get the product ID of your game, navigate to your game in the Xbox Developer Portal (XDP) and retrieve the product ID from the URL. Alternatively, if you download your health data from the Windows Partner Center analytics report, the product ID is included in the .tsv file. |  Yes  |
| cabId | string | The unique ID of the CAB file you want to download. To get this ID, use the [get details for an error in your Xbox One game](get-details-for-an-error-in-your-xbox-one-game.md) method to retrieve details for a specific error in your app, and use the **cabId** value in the response body of that method. |  Yes  |

 
### Request example

The following example demonstrates how to download a CAB file using this method. Replace the *applicationId* and *cabId* parameters with the appropriate values for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/cabdownload?applicationId=BRRT4NJ9B3D1&cabId=1336373323853 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

This method returns a 302 (redirect) response code, and the **Location** header in the response is assigned to the shared access signature (SAS) URI of the CAB file. The caller is redirected to this URI to automatically download the CAB file.

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data for your Xbox One game](get-error-reporting-data-for-your-xbox-one-game.md)
* [Get details for an error in your Xbox One game](get-details-for-an-error-in-your-xbox-one-game.md)
* [Get the stack trace for an error in your Xbox One game](get-the-stack-trace-for-an-error-in-your-xbox-one-game.md)
