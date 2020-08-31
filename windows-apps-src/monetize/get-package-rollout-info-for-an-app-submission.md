---
description: Use this method in the Microsoft Store submission API to get package rollout info for an app submission.
title: Get rollout info for an app submission
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, package rollout, app submission
ms.assetid: 9ada5ac3-a86e-4bb6-8ebc-915ba9649e3c
ms.localizationpriority: medium
---
# Get rollout info for an app submission


Use this method in the Microsoft Store submission API to get [package rollout](../publish/gradual-package-rollout.md) info for a package flight submission. For more information about the process of process of creating an app submission by using the Microsoft Store submission API, see [Manage app submissions](manage-app-submissions.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Create a submission for one of your apps. You can do this in Partner Center, or you can do this by using the [create an app submission](create-an-app-submission.md) method.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request parameters.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET   | ```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/packagerollout   ``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app that contains the submission with the package rollout info you want to get. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |
| submissionId | string | Required. The ID of the submission with the package rollout info you want to get. This ID is available in the response data for requests to [create an app submission](create-an-app-submission.md). For a submission that was created in Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |


### Request body

Do not provide a request body for this method.

### Request example

The following example demonstrates how to get the package rollout info for an app submission.

```
GET https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/submissions/1152921504621243649/packagerollout HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

The following example demonstrates the JSON response body for a successful call to this method for an app submission with gradual package rollout enabled. For more details about the values in the response body, see [Package rollout resource](manage-app-submissions.md#package-rollout-object).

```json
{
    "isPackageRollout": true,
    "packageRolloutPercentage": 25.0,
    "packageRolloutStatus": "PackageRolloutInProgress",
    "fallbackSubmissionId": "1212922684621243058"
}
```

If the app submission does not have gradual package rollout enabled, the following response body will be returned.

```json
{
    "isPackageRollout": false,
    "packageRolloutPercentage": 0.0,
    "packageRolloutStatus": "PackageRolloutNotStarted",
    "fallbackSubmissionId": "0"
}
```

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 404  | The submission could not be found. |
| 409  | The submission does not belong to the specified app, or the app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |   


## Related topics

* [Gradual package rollout](../publish/gradual-package-rollout.md)
* [Manage app submissions using the Microsoft Store submission API](manage-app-submissions.md)
* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)