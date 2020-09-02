---
ms.assetid: E8751EBF-AE0F-4107-80A1-23C186453B1C
description: Use this method in the Microsoft Store submission API to update an existing app submission.
title: Update an app submission
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, app submission, update
ms.localizationpriority: medium
---
# Update an app submission

Use this method in the Microsoft Store submission API to update an existing app submission. After you successfully update a submission by using this method, you must [commit the submission](commit-an-app-submission.md) for ingestion and publishing.

For more information about how this method fits into the process of creating an app submission by using the Microsoft Store submission API, see [Manage app submissions](manage-app-submissions.md).

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
* [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Create a submission for one of your apps. You can do this in Partner Center, or you can do this by using the [create an app submission](create-an-app-submission.md) method.

## Request

This method has the following syntax. See the following sections for usage examples and descriptions of the header and request body.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| PUT   | ```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}  ``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| applicationId | string | Required. The Store ID of the app for which you want to update a submission. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).  |
| submissionId | string | Required. The ID of the submission to update. This ID is available in the response data for requests to [create an app submission](create-an-app-submission.md). For a submission that was created in Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |


### Request body

The request body has the following parameters.

| Value      | Type   | Description                                                                                                                                                                                                                                                                         |
|------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| applicationCategory           | string  |   A string that specifies the [category and/or subcategory](../publish/category-and-subcategory-table.md) for your app. Categories and subcategories are combined into a single string with the underscore '_' character, such as **BooksAndReference_EReader**.      |  
| pricing           |  object  | An object that contains pricing info for the app. For more information, see the [Pricing resource](manage-app-submissions.md#pricing-object) section.       |   
| visibility           |  string  |  The visibility of the app. This can be one of the following values: <ul><li>Hidden</li><li>Public</li><li>Private</li><li>NotSet</li></ul>       |   
| targetPublishMode           | string  | The publish mode for the submission. This can be one of the following values: <ul><li>Immediate</li><li>Manual</li><li>SpecificDate</li></ul> |
| targetPublishDate           | string  | The publish date for the submission in ISO 8601 format, if the *targetPublishMode* is set to SpecificDate.  |  
| listings           |   object  |  A dictionary of key and value pairs, where each key is a country code and each value is a [Listing resource](manage-app-submissions.md#listing-object) object that contains listing info for the app.       |   
| hardwarePreferences           |  array  |   An array of strings that define the [hardware preferences](../publish/enter-app-properties.md) for your app. This can be one of the following values: <ul><li>Touch</li><li>Keyboard</li><li>Mouse</li><li>Camera</li><li>NfcHce</li><li>Nfc</li><li>BluetoothLE</li><li>Telephony</li></ul>     |   
| automaticBackupEnabled           |  boolean  |   Indicates whether Windows can include your app's data in automatic backups to OneDrive. For more information, see [App declarations](../publish/product-declarations.md).   |   
| canInstallOnRemovableMedia           |  boolean  |   Indicates whether customers can install your app to removable storage. For more information, see [App declarations](../publish/product-declarations.md).     |   
| isGameDvrEnabled           |  boolean |   Indicates whether game DVR is enabled for the app.    |   
| gamingOptions           |  object |   An array that contains one [gaming options resource](manage-app-submissions.md#gaming-options-object) that defines game-related settings for the app.     |   
| hasExternalInAppProducts           |     boolean          |   Indicates whether your app allows users to make purchase outside the Microsoft Store commerce system. For more information, see [App declarations](../publish/product-declarations.md).     |   
| meetAccessibilityGuidelines           |    boolean           |  Indicates whether your app has been tested to meet accessibility guidelines. For more information, see [App declarations](../publish/product-declarations.md).      |   
| notesForCertification           |  string  |   Contains [notes for certification](../publish/notes-for-certification.md) for your app.    |    
| applicationPackages           |   array  | Contains objects that provide details about each package in the submission. For more information, see the [Application package](manage-app-submissions.md#application-package-object) section. When calling this method to update an app submission, only the *fileName*, *fileStatus*, *minimumDirectXVersion*, and *minimumSystemRam* values of these objects are required in the request body. The other values are populated by Partner Center.   |    
| packageDeliveryOptions    | object  | Contains gradual package rollout and mandatory update settings for the submission. For more information, see [Package delivery options object](manage-app-submissions.md#package-delivery-options-object).  |
| enterpriseLicensing           |  string  |  One of the [enterprise licensing values](manage-app-submissions.md#enterprise-licensing) values that indicate the enterprise licensing behavior for the app.  |    
| allowMicrosftDecideAppAvailabilityToFutureDeviceFamilies           |  boolean   |  Indicates whether Microsoft is allowed to [make the app available to future Windows 10 device families](../publish/set-app-pricing-and-availability.md).    |    
| allowTargetFutureDeviceFamilies           | boolean   |  Indicates whether your app is allowed to [target future Windows 10 device families](../publish/set-app-pricing-and-availability.md).     |   
| trailers           |  array |   An array that contains up to [trailer resources](manage-app-submissions.md#trailer-object) that represent video trailers for the app listing.   |   


### Request example

The following example demonstrates how to update an app submission.

```json
PUT https://manage.devcenter.microsoft.com/v1.0/my/applications/9NBLGGH4R315/submissions/1152921504621230023 HTTP/1.1
Authorization: Bearer <your access token>
Content-Type: application/json
{
  "applicationCategory": "BooksAndReference_EReader",
  "pricing": {
    "trialPeriod": "FifteenDays",
    "marketSpecificPricings": {},
    "sales": [],
    "priceId": "Tier2"
  },
  "visibility": "Public",
  "targetPublishMode": "Manual",
  "targetPublishDate": "1601-01-01T00:00:00Z",
  "listings": {
    "en-us": {
      "baseListing": {
        "copyrightAndTrademarkInfo": "",
        "keywords": [
		      "epub"
		    ],
        "licenseTerms": "",
        "privacyPolicy": "",
        "supportContact": "",
        "websiteUrl": "",
        "description": "Description",
        "features": [
		      "Free ebook reader"
		    ],
        "releaseNotes": "",
        "images": [
          {
            "fileName": "contoso.png",
            "fileStatus": "Uploaded",
            "id": "1152921504672272757",
            "imageType": "Screenshot"
          }
        ],
        "recommendedHardware": [],
        "title": "Contoso ebook reader"
      },
      "platformOverrides": {
        "Windows81": {
          "description": "Ebook reader for Windows 8.1"
        }
      }
    }
  },
  "hardwarePreferences": [
    "Touch"
  ],
  "automaticBackupEnabled": false,
  "canInstallOnRemovableMedia": true,
  "isGameDvrEnabled": false,
  "gamingOptions": [],
  "hasExternalInAppProducts": false,
  "meetAccessibilityGuidelines": true,
  "notesForCertification": "",
  "applicationPackages": [
    {
      "fileName": "contoso_app.appx",
      "fileStatus": "PendingUpload",
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
  "enterpriseLicensing": "Online",
  "allowMicrosoftDecideAppAvailabilityToFutureDeviceFamilies": true,
  "allowTargetFutureDeviceFamilies": {
    "Desktop": false,
    "Mobile": true,
    "Holographic": true,
    "Xbox": false,
    "Team": true
  },
  "trailers": []
}
```

## Response

The following example demonstrates the JSON response body for a successful call to this method. The response body contains information about the updated submission. For more details about the values in the response body, see [App submission resource](manage-app-submissions.md#app-submission-object).

```json
{
  "id": "1152921504621243540",
  "applicationCategory": "BooksAndReference_EReader",
  "pricing": {
    "trialPeriod": "FifteenDays",
    "marketSpecificPricings": {},
    "sales": [],
    "priceId": "Tier2"
  },
  "visibility": "Public",
  "targetPublishMode": "Manual",
  "targetPublishDate": "1601-01-01T00:00:00Z",
  "listings": {
    "en-us": {
      "baseListing": {
        "copyrightAndTrademarkInfo": "",
        "keywords": [
           "epub"
        ],
        "licenseTerms": "",
        "privacyPolicy": "",
        "supportContact": "",
        "websiteUrl": "",
        "description": "Description",
        "features": [
          "Free ebook reader"
        ],
        "releaseNotes": "",
        "images": [
          {
            "fileName": "contoso.png",
            "fileStatus": "Uploaded",
            "id": "1152921504672272757",
            "imageType": "Screenshot"
          }
        ],
        "recommendedHardware": [],
        "title": "Contoso ebook reader"
      },
      "platformOverrides": {
        "Windows81": {
          "description": "Ebook reader for Windows 8.1",
        }
      }
    }
  },
  "hardwarePreferences": [
    "Touch"
  ],
  "automaticBackupEnabled": false,
  "canInstallOnRemovableMedia": true,
  "isGameDvrEnabled": false,
  "gamingOptions": [],
  "hasExternalInAppProducts": false,
  "meetAccessibilityGuidelines": true,
  "notesForCertification": "",
  "status": "PendingCommit",
  "statusDetails": {
    "errors": [],
    "warnings": [],
    "certificationReports": []
  },
  "fileUploadUrl": "https://productingestionbin1.blob.core.windows.net/ingestion/387a9ea8-a412-43a9-8fb3-a38d03eb483d?sv=2014-02-14&sr=b&sig=sdd12JmoaT6BhvC%2BZUrwRweA%2Fkvj%2BEBCY09C2SZZowg%3D&se=2016-06-17T18:32:26Z&sp=rwl",
  "applicationPackages": [
    {
      "fileName": "contoso_app.appx",
      "fileStatus": "PendingUpload",
      "id": "1152921504620138797",
      "version": "1.0.0.0",
      "architecture": "ARM",
      "languages": [
        "en-US"
      ],
      "capabilities": [
        "ID_RESOLUTION_HD720P",
        "ID_RESOLUTION_WVGA",
        "ID_RESOLUTION_WXGA"
      ],
      "minimumDirectXVersion": "None",
      "minimumSystemRam": "None",
      "targetDeviceFamilies": [
        "Windows.Mobile min version 10.0.10240.0"
      ]
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
  "enterpriseLicensing": "Online",
  "allowMicrosoftDecideAppAvailabilityToFutureDeviceFamilies": true,
  "allowTargetFutureDeviceFamilies": {
    "Desktop": false,
    "Mobile": true,
    "Holographic": true,
    "Xbox": false,
    "Team": true
  },
  "friendlyName": "Submission 2",
  "trailers": []
}
```

## Error codes

If the request cannot be successfully completed, the response will contain one of the following HTTP error codes.

| Error code |  Description   |
|--------|------------------|
| 400  | The submission could not be updated because the request is invalid. |
| 409  | The submission could not be updated because of the current state of the app, or the app uses a Partner Center feature that is [currently not supported by the Microsoft Store submission API](create-and-manage-submissions-using-windows-store-services.md#not_supported). |   


## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get an app submission](get-an-app-submission.md)
* [Create an app submission](create-an-app-submission.md)
* [Commit an app submission](commit-an-app-submission.md)
* [Delete an app submission](delete-an-app-submission.md)
* [Get the status of an app submission](get-status-for-an-app-submission.md)