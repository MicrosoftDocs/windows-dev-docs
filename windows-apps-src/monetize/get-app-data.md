---
ms.assetid: 8D4AE532-22EF-4743-9555-A828B24B8F16
description: Use these methods in the Microsoft Store submission API to retrieve data for apps that are registered to your Partner Center account.
title: Get app data
ms.date: 02/28/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, app data
ms.localizationpriority: medium
---
# Get app data

Use the following methods in the Microsoft Store submission API to get data for existing apps in your Partner Center account. For an introduction to the Microsoft Store submission API, including prerequisites for using the API, see [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md).

Before you can use these methods, the app must already exist in your Partner Center account. To create or manage submissions for apps, see the methods in [Manage app submissions](manage-app-submissions.md).

| Method | URI                                                                                             | Description                                                 |
|------- |------------------------------------------------------------------------------------------------ |------------------------------------------------------------ |
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications`                                   | [Get data for all your apps](get-all-apps.md)               |
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}`                   | [Get data for a specific app](get-an-app.md)                |
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/listinappproducts` | [Get add-ons for an app](get-add-ons-for-an-app.md)         |
| GET    | `https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/listflights`       | [Get package flights for an app](get-flights-for-an-app.md) |

## Prerequisites

If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API before trying to use any of these methods.

## Data resources

The Microsoft Store submission API methods for getting app data use the following JSON data resources.

<span id="application_object" />

### Application resource

This resource represents an app that is registered to your account.

```json
{
  "id": "9NBLGGH4R315",
  "primaryName": "ApiTestApp",
  "packageFamilyName": "30481DevCenterAPITester.ApiTestAppForDevbox_ng6try80pwt52",
  "packageIdentityName": "30481DevCenterAPITester.ApiTestAppForDevbox",
  "publisherName": "CN=…",
  "firstPublishedDate": "1601-01-01T00:00:00Z",
  "lastPublishedApplicationSubmission": {
    "id": "1152921504621086517",
    "resourceLocation": "applications/9NBLGGH4R315/submissions/1152921504621086517"
  },
  "pendingApplicationSubmission": {
    "id": "1152921504621243487",
    "resourceLocation": "applications/9NBLGGH4R315/submissions/1152921504621243487"
  },
  "hasAdvancedListingPermission": true
}
```

This resource has the following values.

| Value           | Type    | Description       |
|-----------------|---------|---------------------|
| id            | string  | The Store ID of the app. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).   |
| primaryName   | string  | The primary name of the app.      |
| packageFamilyName | string  | The package family name of the app.      |
| packageIdentityName          | string  | The package identity name of the app.                       |
| publisherName       | string  | The Windows publisher ID that is associated with the app. This corresponds to the **Package/Identity/Publisher** value that appears on the [App identity](../publish/view-app-identity-details.md) page for the app in Partner Center.       |
| firstPublishedDate      | string  | The date the app was first published, in ISO 8601 format.   |
| lastPublishedApplicationSubmission       | object | A [submission resource](#submission_object) that provides information about the last published submission for the app.    |
| pendingApplicationSubmission        | object  |  A [submission resource](#submission_object) that provides information about the current pending submission for the app.   |   
| hasAdvancedListingPermission        | boolean  |  Indicates whether you can configure the [gamingOptions](manage-app-submissions.md#gaming-options-object) or [trailers](manage-app-submissions.md#trailer-object) for submissions for the app. This value is true for submissions created after May 2017. |  |


<span id="add-on-object" />

### Add-on resource

This resource provides information about an add-on.

```json
{
    "inAppProductId": "9WZDNCRD7DLK"
}
```

This resource has the following values.

| Value           | Type    | Description         |
|-----------------|---------|----------------------|
| inAppProductId            | string  | The Store ID of the add-on. This value is supplied by the Store. An example Store ID is 9NBLGGH4TNMP.   |


<span id="flight-object" />

### Flight resource

This resource provides information about a package flight for an app.

```json
{
    "flightId": "7bfc11d5-f710-47c5-8a98-e04bb5aad310",
    "friendlyName": "myflight",
    "lastPublishedFlightSubmission": {
        "id": "1152921504621086517",
        "resourceLocation": "flights/7bfc11d5-f710-47c5-8a98-e04bb5aad310/submissions/1152921504621086517"
    },
    "pendingFlightSubmission": {
        "id": "1152921504621215786",
        "resourceLocation": "flights/7bfc11d5-f710-47c5-8a98-e04bb5aad310/submissions/1152921504621215786"
    },
    "groupIds": [
        "1152921504606962205"
    ],
    "rankHigherThan": "Non-flighted submission"
}
```

This resource has the following values.

| Value           | Type    | Description           |
|-----------------|---------|------------------------|
| flightId            | string  | The ID for the package flight. This value is supplied by Partner Center.  |
| friendlyName           | string  | The name of the package flight, as specified by the developer.   |
| lastPublishedFlightSubmission       | object | A [submission resource](#submission_object) that provides information about the last published submission for the package flight.   |
| pendingFlightSubmission        | object  |  A [submission resource](#submission_object) that provides information about the current pending submission for the package flight.  |    
| groupIds           | array  | An array of strings that contain the IDs of the flight groups that are associated with the package flight. For more information about flight groups, see [Package flights](../publish/package-flights.md).   |
| rankHigherThan           | string  | The friendly name of the package flight that is ranked immediately lower than the current package flight. For more information about ranking flight groups, see [Package flights](../publish/package-flights.md).  |


<span id="submission_object" />

### Submission resource

This resource provides information about a submission. The following example demonstrates the format of this resource.

```json
{
  "pendingApplicationSubmission": {
    "id": "1152921504621243487",
    "resourceLocation": "applications/9WZDNCRD9MMD/submissions/1152921504621243487"
  }
}
```

This resource has the following values.

| Value              | Type   | Description               |
|--------------------|--------|---------------------------|
| id                 | string | The ID of the submission. |
| resourceLocation   | string | A relative path that you can append to the base ```https://manage.devcenter.microsoft.com/v1.0/my/``` request URI to retrieve the complete data for the submission. |

 
## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage app submissions using the Microsoft Store submission API](manage-app-submissions.md)
* [Get all apps](get-all-apps.md)
* [Get an app](get-an-app.md)
* [Get add-ons for an app](get-add-ons-for-an-app.md)
* [Get package flights for an app](get-flights-for-an-app.md)