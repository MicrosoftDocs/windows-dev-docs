---
author: mcleanbyron
ms.assetid: F94AF8F6-0742-4A3F-938E-177472F96C00
description: Use this method in the Windows Store submission API to commit a new or updated package flight submission to Windows Dev Center.
title: Commit a package flight submission
ms.author: mcleans
ms.date: 08/03/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Windows Store submission API, commit flight submission
---

# Commit a package flight submission




Use this method in the Windows Store submission API to commit a new or updated package flight submission to Windows Dev Center. The commit action alerts Dev Center that the submission data has been uploaded (including any related packages). In response, Dev Center commits the changes to the submission data for ingestion and publishing. After the commit operation succeeds, the changes to the submission are shown in the Dev Center dashboard.

For more information about how the commit operation fits into the process of creating a package flight submission by using the Windows Store submission API, see [Manage package flight submissions](manage-flight-submissions.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Windows Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* [Create a package flight submission](create-a-flight-submission.md) and then [update the submission](update-a-flight-submission.md) with any necessary changes to the submission data.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| POST    | ```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/commit``` |

<span/>
 

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |

<span/>

### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app that contains the package flight submission you want to commit. The Store ID for the app is available on the Dev Center dashboard.  |
| flightId | string | Required. The ID of the package flight that contains the submission to commit. This ID is available in the response data for requests to [create a package flight](create-a-flight.md) and [get package flights for an app](get-flights-for-an-app.md).  |
| submissionId | string | Required. The ID of the submission to commit. This ID is available in the Dev Center dashboard, and it is included in the response data for requests to [create a package flight submission](create-a-flight-submission.md).  |

<span/>

### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to commit a package flight submission.

```
POST https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/flights/43e448df-97c9-4a43-a0bc-2a445e736bcd/submissions/1152921504621243649/commit HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body for a successful call to this method. For more details about the values in the response body, see the following sections.

```json
{
  "status": "CommitStarted"
}
```

### Response body

| Value      | Type   | Description                                                                                                                                                                                                                                                                         |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| status           | string  | The status of the submission. This can be one of the following values: <ul><li>None</li><li>Canceled</li><li>PendingCommit</li><li>CommitStarted</li><li>CommitFailed</li><li>PendingPublication</li><li>Publishing</li><li>Published</li><li>PublishFailed</li><li>PreProcessing</li><li>PreProcessingFailed</li><li>Certification</li><li>CertificationFailed</li><li>Release</li><li>ReleaseFailed</li></ul>  |

<span/>

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 400  | The request parameters are invalid. |
| 404  | The specified submission could not be found. |
| 409  | The specified submission was found but it could not be committed in its current state, or the app uses a Dev Center dashboard feature that is [currently not supported by the Windows Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |

<span/>


## Related topics

* [Create and manage submissions using Windows Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage package flight submissions](manage-flight-submissions.md)
* [Get a package flight submission](get-a-flight-submission.md)
* [Create a package flight submission](create-a-flight-submission.md)
* [Update a package flight submission](update-a-flight-submission.md)
* [Delete a package flight submission](delete-a-flight-submission.md)
* [Get the status of a package flight submission](get-status-for-a-flight-submission.md)
