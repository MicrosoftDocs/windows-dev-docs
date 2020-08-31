---
ms.assetid: 96C090C1-88F8-42E7-AED1-AFA9031E952B
description: Use this method in the Microsoft Store submission API to delete an existing app submission.
title: Delete an app submission
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, app submission, delete
ms.localizationpriority: medium
---
# Delete an app submission

Use this method in the Microsoft Store submission API to delete an existing app submission.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| DELETE    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app that contains the submission to delete. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |
| submissionId | string | Required. The ID of the submission to delete. This ID is available in the response data for requests to [create an app submission](create-an-app-submission.md). For a submission that was created in Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |


### Request body

Do not provide a request body for this method.


### Request example

The following example demonstrates how to delete an app submission.

```json
DELETE https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/submissions/1152921504621243610 HTTP/1.1
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
* [Get an app submission](get-an-app-submission.md)
* [Create an app submission](create-an-app-submission.md)
* [Commit an app submission](commit-an-app-submission.md)
* [Update an app submission](update-an-app-submission.md)
* [Get the status of an app submission](get-status-for-an-app-submission.md)