---
ms.assetid: 87708690-079A-443D-807E-D2BF9F614DDF
description: Use this method in the Microsoft Store submission API to get data for a package flight for an app that is registered to your Partner Center account.
title: Get a package flight
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, flight, package flight
ms.localizationpriority: medium
---
# Get a package flight

Use this method in the Microsoft Store submission API to get data for a package flight for an app that is registered to your Partner Center account.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app that contains the package flight you want to get. The Store ID for the app is available in Partner Center.  |
| flightId | string | Required. The ID of the package flight to get. This ID is available in the response data for requests to [create a package flight](create-a-flight.md) and [get package flights for an app](get-flights-for-an-app.md). For a flight that was created in Partner Center, this ID is also available in the URL for the flight page in Partner Center.  |


### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to retrieve information about a package flight with the ID 43e448df-97c9-4a43-a0bc-2a445e736bcd for an app with the Store ID value 9WZDNCRD91MD.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/flights/43e448df-97c9-4a43-a0bc-2a445e736bcd HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body for a successful call to this method. For more details about the values in the response body, see the following sections.

```json
{
  "flightId": "43e448df-97c9-4a43-a0bc-2a445e736bcd",
  "friendlyName": "myflight",
  "lastPublishedFlightSubmission": {
    "id": "1152921504621086517",
    "resourceLocation": "flights/43e448df-97c9-4a43-a0bc-2a445e736bcd/submissions/1152921504621086517"
  },
  "pendingFlightSubmission": {
    "id": "115292150462124364",
    "resourceLocation": "flights/43e448df-97c9-4a43-a0bc-2a445e736bcd/submissions/1152921504621243647"
  },
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
| friendlyName           | string  | The name of the package flight, as specified by the developer.   |  
| lastPublishedFlightSubmission       | object | An object that provides information about the last published submission for the package flight. For more information, see the [Submission object](#submission_object) section below.  |
| pendingFlightSubmission        | object  |  An object that provides information about the current pending submission for the package flight. For more information, see the [Submission object](#submission_object) section below.  |   
| groupIds           | array  | An array of strings that contain the IDs of the flight groups that are associated with the package flight. For more information about flight groups, see [Package flights](../publish/package-flights.md).   |
| rankHigherThan           | string  | The friendly name of the package flight that is ranked immediately lower than the current package flight. For more information about ranking flight groups, see [Package flights](../publish/package-flights.md).  |


<span id="submission_object" />

### Submission object

The *lastPublishedFlightSubmission* and *pendingFlightSubmission* values in the response body contain objects that provide resource information about a submission for the package flight. These objects have the following values.

| Value           | Type    | Description                                                                                                                                                                                                                          |
|-----------------|---------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| id            | string  | The ID of the submission.    |
| resourceLocation   | string  | A relative path that you can append to the base `https://manage.devcenter.microsoft.com/v1.0/my/` request URI to retrieve the complete data for the submission.               |


## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description     |
|--------|---------------------  |
| 400  | The request is invalid. |
| 404  | The specified package flight could not be found.   |   
| 409  | The app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |                                                                                                 


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Create a package flight](create-a-flight.md)
* [Delete a package flight](delete-a-flight.md)