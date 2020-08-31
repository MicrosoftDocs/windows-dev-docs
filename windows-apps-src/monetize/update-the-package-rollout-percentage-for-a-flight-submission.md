---
description: Use this method in the Microsoft Store submission API to update the package rollout percentage for a package flight submission.
title: Update the rollout percentage for a flight submission
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, package rollout, flight submission, update, percentage
ms.assetid: ee9aa223-e945-4c11-b430-1f4b1e559743
ms.localizationpriority: medium
---
# Update the rollout percentage for a flight submission


Use this method in the Microsoft Store submission API to [update the rollout percentage](../publish/gradual-package-rollout.md#setting-the-rollout-percentage) for a package flight submission. For more information about the process of process of creating a package flight submission by using the Microsoft Store submission API, see [Manage package flight submissions](manage-flight-submissions.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Create a submission for one of your apps. You can do this in Partner Center, or you can do this by using the [create an app submission](create-an-app-submission.md) method.
* Enable a gradual package rollout for the submission. You can do this [in Partner Center](../publish/gradual-package-rollout.md), or you can do this by [using the Microsoft Store submission API](manage-flight-submissions.md#manage-gradual-package-rollout).

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request parameters.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| POST   | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/updatepackagerolloutpercentage` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app that contains the package flight submission with the package rollout percentage you want to update. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |
| flightId | string | Required. The ID of the package flight that contains the submission with the package rollout percentage you want to update. This ID is available in the response data for requests to [create a package flight](create-a-flight.md) and [get package flights for an app](get-flights-for-an-app.md). For a flight that was created in Partner Center, this ID is also available in the URL for the flight page in Partner Center.  |
| submissionId | string | Required. The ID of the submission with the package rollout percentage you want to update. This ID is available in the response data for requests to [create a package flight submission](create-a-flight-submission.md). For a submission that was created in Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |
| percentage  |  float  |  Required. The percentage of users who will receive the gradual rollout package.  |


### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to update the package rollout percentage for a package flight submission.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/flights/43e448df-97c9-4a43-a0bc-2a445e736bcd/submissions/1152921504621243680/updatepackagerolloutpercentage?percentage=25 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body for a successful call to this method. For more details about the values in the response body, see [Package rollout resource](manage-flight-submissions.md#package-rollout-object).

```json
{
    "isPackageRollout": true,
    "packageRolloutPercentage": 25.0,
    "packageRolloutStatus": "PackageRolloutInProgress",
    "fallbackSubmissionId": "1212922684621243058"
}
```

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 404  | The package flight submission could not be found. |
| 409  | This code indicates one of the following errors:<br/><br/><ul><li>The submission is not in a valid state for the gradual rollout operation (before calling this method, the submission must be published and the [packageRolloutStatus](manage-flight-submissions.md#package-rollout-object) value must be set to **PackageRolloutInProgress**).</li><li>The submission does not belong to the specified app.</li><li>The app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported).</li></ul> |   


## Related topics

* [Gradual package rollout](../publish/gradual-package-rollout.md)
* [Manage package flight submissions using the Microsoft Store submission API](manage-flight-submissions.md)
* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)