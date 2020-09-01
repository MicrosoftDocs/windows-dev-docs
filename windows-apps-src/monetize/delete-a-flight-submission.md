---
ms.assetid: 1A69A388-B1CC-4D2C-886B-EA07E6E60252
description: Use this method in the Microsoft Store submission API to delete an existing package flight submission.
title: Delete a package flight submission
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, flight submission, delete, package flight
ms.localizationpriority: medium
---
# Delete a package flight submission

Use this method in the Microsoft Store submission API to delete an existing package flight submission.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| DELETE    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationid}/flights/{flightId}/submissions/{submissionId}` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app that contains the package flight submission you want to delete. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |
| flightId | string | Required. The ID of the package flight that contains the submission to delete. This ID is available in the response data for requests to [create a package flight](create-a-flight.md) and [get package flights for an app](get-flights-for-an-app.md). For a flight that was created in Partner Center, this ID is also available in the URL for the flight page in Partner Center.  |
| submissionId | string | Required. The ID of the submission to delete. This ID is available in the response data for requests to [create a package flight submission](create-a-flight-submission.md). For a submission that was created in  Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |


### Request body

Do not provide a request body for this method.


### Request example

The following example demonstrates how to delete a submission for a package flight.

```json
DELETE https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/flights/43e448df-97c9-4a43-a0bc-2a445e736bcd/submissions/1152921504621243649 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

If successful, this method returns an empty response body.

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 400  | The request parameters are invalid. |
| 404  | The specified submission could not be found. |
| 409  | The specified submission was found but it could not be deleted in its current state, or the app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage package flight submissions](manage-flight-submissions.md)
* [Get a package flight submission](get-a-flight-submission.md)
* [Create a package flight submission](create-a-flight-submission.md)
* [Commit a package flight submission](commit-a-flight-submission.md)
* [Update a package flight submission](update-a-flight-submission.md)
* [Get the status of a package flight submission](get-status-for-a-flight-submission.md)