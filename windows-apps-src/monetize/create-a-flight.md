---
ms.assetid: 8C1E9E36-13AF-4386-9D0F-F9CB320F02F5
description: Use this method in the Microsoft Store submission API to create a package flight for an app that is registered to your Partner Center account.
title: Create a package flight
ms.date: 04/16/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, create flight
ms.localizationpriority: medium
---
# Create a package flight

Use this method in the Microsoft Store submission API to create a package flight for an app that is registered to your Partner Center account.

> [!NOTE]
> This method creates a package flight without any submissions. To create a submission for package flight, see the methods in [Manage package flight submissions](manage-flight-submissions.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| POST    | ```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app for which you want to create a package flight. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |


### Request body

The request body has the following parameters.

|  Parameter  |  Type  |  Description  |  Required  |
|------|------|------|------|
|  friendlyName  |  string  |  The name of the package flight, as specified by the developer.  |  No  |
|  groupIds  |  array  |  An array of strings that contain the IDs of the flight groups that are associated with the package flight. For more information about flight groups, see [Package flights](../publish/package-flights.md).  |  No  |
|  rankHigherThan  |  string  |  The friendly name of the package flight that is ranked immediately lower than the current package flight. If you do not set this parameter, the new package flight will have the highest rank of all package flights. For more information about ranking flight groups, see [Package flights](../publish/package-flights.md).    |  No  |


### Request example

The following example demonstrates how to create a new package flight for an app that has the Store ID 9WZDNCRD911W.

```syntax
POST https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/flights HTTP/1.1
Authorization: Bearer eyJ0eXAiOiJKV1Q...
Content-Type: application/json
{
  "friendlyName": "myflight",
  "groupIds": [
    0
  ],
  "rankHigherThan": null
}

```

## Response

The following example demonstrates the JSON response body for a successful call to this method. For more details about the values in the response body, see the following sections.

```json
{
  "flightId": "43e448df-97c9-4a43-a0bc-2a445e736bcd",
  "friendlyName": "myflight",
  "groupIds": [
    "0"
  ],
  "rankHigherThan": "671c2857-725e-4faf-9e9e-ea1191ef879c"
}
```

### Response body

| Value      | Type   | Description                                                                                                                                                                                                                                                                         |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| flightId            | string  | The ID for the package flight. This value is supplied by Partner Center.  |
| friendlyName           | string  | The name of the package flight, as specified in the request.   |  
| groupIds           | array  | An array of strings that contain the IDs of the flight groups that are associated with the package flight, as specified in the request. For more information about flight groups, see [Package flights](../publish/package-flights.md).   |
| rankHigherThan           | string  | The friendly name of the package flight that is ranked immediately lower than the current package flight, as specified in the request. For more information about ranking flight groups, see [Package flights](../publish/package-flights.md).  |


## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 400  | The request is invalid. |
| 409  | The package flight could not be created because of its current state, or the app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |   


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get a package flight](get-a-flight.md)
* [Delete a package flight](delete-a-flight.md)