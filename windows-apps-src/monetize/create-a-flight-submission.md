---
ms.assetid: CD866083-EB7F-4389-A907-FC43DC2FCB5E
description: Use this method in the Microsoft Store submission API to create a new package flight submission for an app that is registered to your Partner Center account.
title: Create a package flight submission
ms.date: 08/03/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, create flight submission
ms.localizationpriority: medium
---

# Create a package flight submission

Use this method in the Microsoft Store submission API to create a new submission for a package flight for an app. After you successfully create a new submission by using this method, [update the submission](update-a-flight-submission.md) to make any necessary changes to the submission data, and then [commit the submission](commit-a-flight-submission.md) for ingestion and publishing.

For more information about how this method fits into the process of creating a package flight submission by using the Microsoft Store submission API, see [Manage package flight submissions](manage-flight-submissions.md).

> [!NOTE]
> This method creates a submission for an existing package flight. To create a package flight, use the [create a package flight](create-a-flight.md) method.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Create a package flight for an app. You can do this in Partner Center, or you can do this by using the [create a package flight](create-a-flight.md) method.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| POST    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app for which you want to create a package flight submission. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |
| flightId | string | Required. The ID of the package flight for which you want to add the submission. This ID is available in the response data for requests to [create a package flight](create-a-flight.md) and [get package flights for an app](get-flights-for-an-app.md).  |


### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to create a new package flight submission for an app that has the Store ID 9WZDNCRD91MD.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/flights/43e448df-97c9-4a43-a0bc-2a445e736bcd/submissions HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body for a successful call to this method. The response body contains information about the new submission. For more details about the values in the response body, see [Package flight submission resource](manage-flight-submissions.md#flight-submission-object).

```json
{
  "id": "1152921504621243649",
  "flightId": "cd2e368a-0da5-4026-9f34-0e7934bc6f23",
  "status": "PendingCommit",
  "statusDetails": {
    "errors": [],
    "warnings": [],
    "certificationReports": []
  },
  "flightPackages": [
    {
      "fileName": "newPackage.appx",
      "fileStatus": "PendingUpload",
      "id": "",
      "version": "1.0.0.0",
      "languages": ["en-us"],
      "capabilities": [],
      "minimumDirectXVersion": "None",
      "minimumSystemRam": "None"
    }
  ],
  "packageDeliveryOptions": {
    "packageRollout": {
        "isPackageRollout": false,
        "packageRolloutPercentage": 0.0,
        "packageRolloutStatus": "PackageRolloutNotStarted",
        "fallbackSubmissionId": "0"
    },
    "isMandatoryUpdate": false,
    "mandatoryUpdateEffectiveDate": "1601-01-01T00:00:00.0000000Z"
  },
  "fileUploadUrl": "https://productingestionbin1.blob.core.windows.net/ingestion/8b389577-5d5e-4cbe-a744-1ff2e97a9eb8?sv=2014-02-14&sr=b&sig=wgMCQPjPDkuuxNLkeG35rfHaMToebCxBNMPw7WABdXU%3D&se=2016-06-17T21:29:44Z&sp=rwl",
  "targetPublishMode": "Immediate",
  "targetPublishDate": "",
  "notesForCertification": "No special steps are required for certification of this app."
}
```

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 400  | The package flight submission could not be created because the request is invalid. |
| 409  | The package flight submission could not be created because of the current state of the app, or the app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |   

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage package flight submissions](manage-flight-submissions.md)
* [Get a package flight submission](get-a-flight-submission.md)
* [Commit a package flight submission](commit-a-flight-submission.md)
* [Update a package flight submission](update-a-flight-submission.md)
* [Delete a package flight submission](delete-a-flight-submission.md)
* [Get the status of a package flight submission](get-status-for-a-flight-submission.md)