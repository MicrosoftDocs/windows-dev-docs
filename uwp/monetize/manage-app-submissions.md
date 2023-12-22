---
ms.assetid: C7428551-4B31-4259-93CD-EE229007C4B8
description: Use these methods in the Microsoft Store submission API to manage submissions for apps that are registered to your Partner Center account.
title: Manage app submissions
ms.date: 04/30/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, app submissions
ms.localizationpriority: medium
---
# Manage app submissions

The Microsoft Store submission API provides methods you can use to manage submissions for your apps, including gradual package rollouts. For an introduction to the Microsoft Store submission API, including prerequisites for using the API, see [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md).

> [!IMPORTANT]
> If you use the Microsoft Store submission API to create a submission for an app, be sure to make further changes to the submission only by using the API, rather than Partner Center. If you use Partner Center to change a submission that you originally created by using the API, you will no longer be able to change or commit that submission by using the API. In some cases, the submission could be left in an error state where it cannot proceed in the submission process. If this occurs, you must delete the submission and create a new submission.

> [!IMPORTANT]
> You cannot use this API to publish submissions for [volume purchases through the Microsoft Store for Business and Microsoft Store for Education](/windows/apps/publish/organizational-licensing) or to publish submissions for [LOB apps](/windows/apps/publish/distribute-lob-apps-to-enterprises) directly to enterprises. For both of these scenarios, you must use Partner Center to publish the submission.


<span id="methods-for-app-submissions"></span>

## Methods for managing app submissions

Use the following methods to get, create, update, commit, or delete an app submission. Before you can use these methods, the app must already exist in your Partner Center account and you must first create one submission for the app in Partner Center. For more information, see the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites).

<table>
<colgroup>
<col width="10%" />
<col width="30%" />
<col width="60%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Method</th>
<th align="left">URI</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">GET</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}</td>
<td align="left"><a href="get-an-app-submission.md">Get an existing app submission</a></td>
</tr>
<tr>
<td align="left">GET</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/status</td>
<td align="left"><a href="get-status-for-an-app-submission.md">Get the status of an existing app submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions</td>
<td align="left"><a href="create-an-app-submission.md">Create a new app submission</a></td>
</tr>
<tr>
<td align="left">PUT</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}</td>
<td align="left"><a href="update-an-app-submission.md">Update an existing app submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/commit</td>
<td align="left"><a href="commit-an-app-submission.md">Commit a new or updated app submission</a></td>
</tr>
<tr>
<td align="left">DELETE</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}</td>
<td align="left"><a href="delete-an-app-submission.md">Delete an app submission</a></td>
</tr>
</tbody>
</table>

<span id="create-an-app-submission">

### Create an app submission

To create a submission for an app, follow this process.

1. If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API.
    > [!NOTE]
    > Make sure the app already has at least one completed submission with the [age ratings](/windows/apps/publish/publish-your-app/age-ratings?pivots=store-installer-msix) information completed.

2. [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token). You must pass this access token to the methods in the Microsoft Store submission API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

3. [Create an app submission](create-an-app-submission.md) by executing the following method in the Microsoft Store submission API. This method creates a new in-progress submission, which is a copy of your last published submission.

    ```json
    POST https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions
    ```

    The response body contains an [app submission](#app-submission-object) resource that includes the ID of the new submission, the shared access signature (SAS) URI for uploading any related files for the submission to Azure Blob Storage (such as app packages, listing images, and trailer files), and all of the data for the new submission (such as the listings and pricing information).

    > [!NOTE]
    > A SAS URI provides access to a secure resource in Azure storage without requiring account keys. For background information about SAS URIs and their use with Azure Blob Storage, see [Shared Access Signatures, Part 1: Understanding the SAS model](/azure/storage/common/storage-sas-overview) and [Shared Access Signatures, Part 2: Create and use a SAS with Blob storage](/azure/storage/common/storage-sas-overview).

4. If you are adding new packages, listing images, or trailer files for the submission, [prepare the app packages](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix) and [prepare the app screenshots, images, and trailers](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix). Add all of these files to a ZIP archive.

5. Revise the [app submission](#app-submission-object) data with any required changes for the new submission, and execute the following method to [update the app submission](update-an-app-submission.md).

    ```json
    PUT https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}
    ```
      > [!NOTE]
      > If you are adding new files for the submission, make sure you update the submission data to refer to the name and relative path of these files in the ZIP archive.

4. If you are adding new packages, listing images, or trailer files for the submission, upload the ZIP archive to [Azure Blob Storage](/azure/storage/blobs/storage-blobs-introduction) using the SAS URI that was provided in the response body of the POST method you called earlier. There are different Azure libraries you can use to do this on a variety of platforms, including:

    * [Azure Storage Client Library for .NET](/azure/storage/storage-dotnet-how-to-use-blobs)
    * [Azure Storage SDK for Java](/azure/storage/storage-java-how-to-use-blob-storage)
    * [Azure Storage SDK for Python](/azure/storage/storage-python-how-to-use-blob-storage)

    The following C# code example demonstrates how to upload a ZIP archive to Azure Blob Storage using the [CloudBlockBlob](/dotnet/api/microsoft.azure.storage.blob.cloudblockblob) class in the Azure Storage Client Library for .NET. This example assumes that the ZIP archive has already been written to a stream object.

    ```csharp
    string sasUrl = "https://productingestionbin1.blob.core.windows.net/ingestion/26920f66-b592-4439-9a9d-fb0f014902ec?sv=2014-02-14&sr=b&sig=usAN0kNFNnYE2tGQBI%2BARQWejX1Guiz7hdFtRhyK%2Bog%3D&se=2016-06-17T20:45:51Z&sp=rwl";
    Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockBob =
        new Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob(new System.Uri(sasUrl));
    await blockBob.UploadFromStreamAsync(stream);
    ```

5. [Commit the app submission](commit-an-app-submission.md) by executing the following method. This will alert Partner Center that you are done with your submission and that your updates should now be applied to your account.

    ```json
    POST https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/commit
    ```

6. Check on the commit status by executing the following method to [get the status of the app submission](get-status-for-an-app-submission.md).

    ```json
    GET https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/status
    ```

    To confirm the submission status, review the *status* value in the response body. This value should change from **CommitStarted** to either **PreProcessing** if the request succeeds or to **CommitFailed** if there are errors in the request. If there are errors, the *statusDetails* field contains further details about the error.

7. After the commit has successfully completed, the submission is sent to the Store for ingestion. You can continue to monitor the submission progress by using the previous method, or by visiting Partner Center.

<span id="manage-gradual-package-rollout">

## Methods for managing a gradual package rollout

You can gradually roll out the updated packages in an app submission to a percentage of your app’s customers on Windows 10 and Windows 11. This allows you to monitor feedback and analytic data for the specific packages to make sure you’re confident about the update before rolling it out more broadly. You can change the rollout percentage (or halt the update) for a published submission without having to create a new submission. For more details, including instructions for how to enable and manage a gradual package rollout in Partner Center, see [this article](/windows/apps/publish/gradual-package-rollout).

To programmatically enable a gradual package rollout for an app submission, follow this process using methods in the Microsoft Store submission API:

  1. [Create an app submission](create-an-app-submission.md) or [get an existing app submission](get-an-app-submission.md).
  2. In the response data, locate the [packageRollout](#package-rollout-object) resource, set the *isPackageRollout* field to **true**, and set the *packageRolloutPercentage* field to the percentage of your app's customers who should get the updated packages.
  3. Pass the updated app submission data to the [update an app submission](update-an-app-submission.md) method.

After a gradual package rollout is enabled for an app submission, you can use the following methods to programmatically get, update, halt, or finalize the gradual rollout.

<table>
<colgroup>
<col width="10%" />
<col width="30%" />
<col width="60%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Method</th>
<th align="left">URI</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">GET</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/packagerollout</td>
<td align="left"><a href="get-package-rollout-info-for-an-app-submission.md">Get the gradual rollout info for an app submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/updatepackagerolloutpercentage</td>
<td align="left"><a href="update-the-package-rollout-percentage-for-an-app-submission.md">Update the gradual rollout percentage for an app submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/haltpackagerollout</td>
<td align="left"><a href="halt-the-package-rollout-for-an-app-submission.md">Halt the gradual rollout for an app submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/finalizepackagerollout</td>
<td align="left"><a href="finalize-the-package-rollout-for-an-app-submission.md">Finalize the gradual rollout for an app submission</a></td>
</tr>
</tbody>
</table>


## Code examples for managing app submissions

The following articles provide detailed code examples that demonstrate how to create an app submission in several different programming languages:

* [C# sample: submissions for apps, add-ons, and flights](csharp-code-examples-for-the-windows-store-submission-api.md)
* [C# sample: app submission with game options and trailers](csharp-code-examples-for-submissions-game-options-and-trailers.md)
* [Java sample: submissions for apps, add-ons, and flights](java-code-examples-for-the-windows-store-submission-api.md)
* [Java sample: app submission with game options and trailers](java-code-examples-for-submissions-game-options-and-trailers.md)
* [Python sample: submissions for apps, add-ons, and flights](python-code-examples-for-the-windows-store-submission-api.md)
* [Python sample: app submission with game options and trailers](python-code-examples-for-submissions-game-options-and-trailers.md)

## StoreBroker PowerShell module

As an alternative to calling the Microsoft Store submission API directly, we also provide an open-source PowerShell module which implements a command-line interface on top of the API. This module is called [StoreBroker](https://github.com/Microsoft/StoreBroker). You can use this module to manage your app, flight, and add-on submissions from the command line instead of calling the Microsoft Store submission API directly, or you can simply browse the source to see more examples for how to call this API. The StoreBroker module is actively used within Microsoft as the primary way that many first-party applications are submitted to the Store.

For more information, see our [StoreBroker page on GitHub](https://github.com/Microsoft/StoreBroker).

<span/>

## Data resources
The Microsoft Store submission API methods for managing app submissions use the following JSON data resources.

<span id="app-submission-object"></span>

### App submission resource

This resource describes an app submission.

```json
{
  "id": "1152921504621243540",
  "applicationCategory": "BooksAndReference_EReader",
  "pricing": {
    "trialPeriod": "FifteenDays",
    "marketSpecificPricings": {},
    "sales": [],
    "priceId": "Tier2",
    "isAdvancedPricingModel": true
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
            "description": "Main page",
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
      "fileStatus": "Uploaded",
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

This resource has the following values.

| Value      | Type   | Description      |
|------------|--------|-------------------|
| id            | string  | The ID of the submission. This ID is available in the response data for requests to [create an app submission](create-an-app-submission.md), [get all apps](get-all-apps.md), and [get an app](get-an-app.md). For a submission that was created in Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |
| applicationCategory           | string  |   A string that specifies the [category and/or subcategory](/windows/apps/publish/publish-your-app/categories-and-subcategories?pivots=store-installer-msix) for your app. Categories and subcategories are combined into a single string with the underscore '_' character, such as **BooksAndReference_EReader**.      |  
| pricing           |  object  | A [pricing resource](#pricing-object) that contains pricing info for the app.        |   
| visibility           |  string  |  The visibility of the app. This can be one of the following values: <ul><li>Hidden</li><li>Public</li><li>Private</li><li>NotSet</li></ul>       |   
| targetPublishMode           | string  | The publish mode for the submission. This can be one of the following values: <ul><li>Immediate</li><li>Manual</li><li>SpecificDate</li></ul> |
| targetPublishDate           | string  | The publish date for the submission in ISO 8601 format, if the *targetPublishMode* is set to SpecificDate.  |  
| listings           |   object  |  A dictionary of key and value pairs, where each key is a country code and each value is a [listing resource](#listing-object) that contains listing info for the app.       |   
| hardwarePreferences           |  array  |   An array of strings that define the [hardware preferences](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-msix) for your app. This can be one of the following values: <ul><li>Touch</li><li>Keyboard</li><li>Mouse</li><li>Camera</li><li>NfcHce</li><li>Nfc</li><li>BluetoothLE</li><li>Telephony</li></ul>     |   
| automaticBackupEnabled           |  boolean  |   Indicates whether Windows can include your app's data in automatic backups to OneDrive. For more information, see [App declarations](/windows/apps/publish/publish-your-app/product-declarations?pivots=store-installer-msix).   |   
| canInstallOnRemovableMedia           |  boolean  |   Indicates whether customers can install your app to removable storage. For more information, see [App declarations](/windows/apps/publish/publish-your-app/product-declarations?pivots=store-installer-msix).     |   
| isGameDvrEnabled           |  boolean |   Indicates whether game DVR is enabled for the app.    |   
| gamingOptions           |  array |   An array that contains one [gaming options resource](#gaming-options-object) that defines game-related settings for the app.     |   
| hasExternalInAppProducts           |     boolean          |   Indicates whether your app allows users to make purchase outside the Microsoft Store commerce system. For more information, see [App declarations](/windows/apps/publish/publish-your-app/product-declarations?pivots=store-installer-msix).     |   
| meetAccessibilityGuidelines           |    boolean           |  Indicates whether your app has been tested to meet accessibility guidelines. For more information, see [App declarations](/windows/apps/publish/publish-your-app/product-declarations?pivots=store-installer-msix).      |   
| notesForCertification           |  string  |   Contains [notes for certification](/windows/apps/publish/publish-your-app/notes-for-certification?pivots=store-installer-msix) for your app.    |    
| status           |   string  |  The status of the submission. This can be one of the following values: <ul><li>None</li><li>Canceled</li><li>PendingCommit</li><li>CommitStarted</li><li>CommitFailed</li><li>PendingPublication</li><li>Publishing</li><li>Published</li><li>PublishFailed</li><li>PreProcessing</li><li>PreProcessingFailed</li><li>Certification</li><li>CertificationFailed</li><li>Release</li><li>ReleaseFailed</li></ul>      |    
| statusDetails           |   object  | A [status details resource](#status-details-object) that contains additional details about the status of the submission, including information about any errors.       |    
| fileUploadUrl           |   string  | The shared access signature (SAS) URI for uploading any packages for the submission. If you are adding new packages, listing images, or trailer files for the submission, upload the ZIP archive that contains the packages and images to this URI. For more information, see [Create an app submission](#create-an-app-submission).       |    
| applicationPackages           |   array  | An array of [application package resources](#application-package-object) that provide details about each package in the submission. |    
| packageDeliveryOptions    | object  | A [package delivery options resource](#package-delivery-options-object) that contains gradual package rollout and mandatory update settings for the submission.  |
| enterpriseLicensing           |  string  |  One of the [enterprise licensing values](#enterprise-licensing) values that indicate the enterprise licensing behavior for the app.  |    
| allowMicrosoftDecideAppAvailabilityToFutureDeviceFamilies           |  boolean   |  Indicates whether Microsoft is allowed to [make the app available to future Windows 10 and Windows 11 device families](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix).    |    
| allowTargetFutureDeviceFamilies           | object   |  A dictionary of key and value pairs, where each key is a [Windows 10 and Windows 11 device family](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix) and each value is a boolean that indicates whether your app is allowed to target the specified device family.     |    
| friendlyName           |   string  |  The friendly name of the submission, as shown in Partner Center. This value is generated for you when you create the submission.       |  
| trailers           |  array |   An array that contains up to 15 [trailer resources](#trailer-object) that represent video trailers for the app listing.<br/><br/>   |  
| isSeekEnabled | boolean | For IngestionAPI when "IsSeekEnabled" property on the Rollout Entity for the flight is set to true (which is same as the checking the check box on the UX for "Always provide the newest packages when customers manually check for updates") and if the user is not part of the staged rollout (% rollout) and if the user check for the updates, then the latest version will be installed for the user. |


<span id="pricing-object"></span>

### Pricing resource

This resource contains pricing info for the app. This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  trialPeriod               |    string     |  A string that specifies the trial period for the app. This can be one of the following values: <ul><li>NoFreeTrial</li><li>OneDay</li><li>TrialNeverExpires</li><li>SevenDays</li><li>FifteenDays</li><li>ThirtyDays</li></ul>    |
|  marketSpecificPricings               |    object     |  A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [price tier](#price-tiers). These items represent the [custom prices for your app in specific markets](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix). Any items in this dictionary override the base price specified by the *priceId* value for the specified market.      |     
|  sales               |   array      |  **Deprecated**. An array of [sale resources](#sale-object) that contain sales information for the app.   |     
|  priceId               |   string      |  A [price tier](#price-tiers) that specifies the [base price](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix) for the app.   |     
|  isAdvancedPricingModel               |   boolean      |  If **true**, your developer account has access to the expanded set of price tiers from .99 USD to 1999.99 USD. If **false**, your developer account has access to the original set of price tiers from .99 USD to 999.99 USD. For more information about the different tiers, see [price tiers](#price-tiers).<br/><br/>**Note**&nbsp;&nbsp;This field is read-only.   |


<span id="sale-object"></span>

### Sale resource

This resources contains sale info for an app.

> [!IMPORTANT]
> The **Sale** resource is no longer supported, and currently you cannot get or modify the sale data for an app submission using the Microsoft Store submission API. In the future, we will update the Microsoft Store submission API to introduce a new way to programmatically access sales information for app submissions.
>    * After calling the [GET method to get an app submission](get-an-app-submission.md), the *sales* value will be empty. You can continue to use Partner Center to get the sale data for your app submission.
>    * When calling the [PUT method to update an app submission](update-an-app-submission.md), the information in the *sales* value is ignored. You can continue to use Partner Center to change the sale data for your app submission.

This resource has the following values.

| Value           | Type    | Description    |
|-----------------|---------|------|
|  name               |    string     |   The name of the sale.    |     
|  basePriceId               |   string      |  The [price tier](#price-tiers) to use for the base price of the sale.    |     
|  startDate               |   string      |   The start date for the sale in ISO 8601 format.  |     
|  endDate               |   string      |  The end date for the sale in ISO 8601 format.      |     
|  marketSpecificPricings               |   object      |   A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [price tier](#price-tiers). These items represent the [custom prices for your app in specific markets](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix). Any items in this dictionary override the base price specified by the *basePriceId* value for the specified market.    |


<span id="listing-object"></span>

### Listing resource

This resource contains listing info for an app. This resource has the following values.

| Value           | Type    | Description                  |
|-----------------|---------|------|
|  baseListing               |   object      |  The [base listing](#base-listing-object) info for the app, which defines the default listing info for all platforms.   |     
|  platformOverrides               | object |   A dictionary of key and value pairs, where each key is string that identifies a platform for which to override the listing info, and each value is a [base listing](#base-listing-object) resource (containing only the values from description to title) that specifies the listing info to override for the specified platform. The keys can have the following values: <ul><li>Unknown</li><li>Windows80</li><li>Windows81</li><li>WindowsPhone71</li><li>WindowsPhone80</li><li>WindowsPhone81</li></ul>     |

<span id="base-listing-object"></span>

### Base listing resource

This resource contains base listing info for an app. This resource has the following values.

| Value           | Type    | Description       |
|-----------------|---------|------|
|  copyrightAndTrademarkInfo                |   string      |  Optional [copyright and/or trademark info](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix).  |
|  keywords                |  array       |  An array of [keyword](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix) to help your app appear in search results.    |
|  licenseTerms                |    string     | The optional [license terms](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix) for your app.     |
|  privacyPolicy                |   string      |   This value is obsolete. To set or change the privacy policy URL for your app, you must do this on the [Properties](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-msix#privacy-policy-url) page in Partner Center. You can omit this value from your calls to the submission API. If you set this value, it will be ignored.       |
|  supportContact                |   string      |  This value is obsolete. To set or change the support contact URL or email address for your app, you must do this on the  [Properties](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-msix#support-contact-info) page in Partner Center. You can omit this value from your calls to the submission API. If you set this value, it will be ignored.        |
|  websiteUrl                |   string      |  This value is obsolete. To set or change the URL of the web page for your app, you must do this on the  [Properties](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-msix#website) page in Partner Center. You can omit this value from your calls to the submission API. If you set this value, it will be ignored.      |    
|  description               |    string     |   The [description](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix) for the app listing.   |     
|  features               |    array     |  An array of up to 20 strings that list the [features](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix) for your app.     |
|  releaseNotes               |  string       |  The [release notes](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix) for your app.    |
|  images               |   array      |  An array of [image and icon](#image-object) resources for the app listing.  |
|  recommendedHardware               |   array      |  An array of up to 11 strings that list the [recommended hardware configurations](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix#additional-information) for your app.     |
|  minimumHardware               |     string    |  An array of up to 11 strings that list the [minimum hardware configurations](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-msix#additional-information) for your app.    |  
|  title               |     string    |   The title for the app listing.   |  
|  shortDescription               |     string    |  Only used for games. This description appears in the **Information** section of the Game Hub on Xbox One, and helps customers understand more about your game.   |  
|  shortTitle               |     string    |  A shorter version of your product’s name. If provided, this shorter name may appear in various places on Xbox One (during installation, in Achievements, etc.) in place of the full title of your product.    |  
|  sortTitle               |     string    |   If your product could be alphabetized in different ways, you can enter another version here. This may help customers find the product more quickly when searching.    |  
|  voiceTitle               |     string    |   An alternate name for your product that, if provided, may be used in the audio experience on Xbox One when using Kinect or a headset.    |  
|  devStudio               |     string    |   Specify this value if you want to include a **Developed by** field in the listing. (The **Published by** field will list the publisher display name associated with your account, whether or not you provide a *devStudio* value.)    |  

<span id="image-object"></span>

### Image resource

This resource contains image and icon data for an app listing. For more information about images and icons for an app listing, see [App screenshots and images](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix). This resource has the following values.

| Value           | Type    | Description           |
|-----------------|---------|------|
|  fileName               |    string     |   The name of the image file in the ZIP archive that you uploaded for the submission.    |     
|  fileStatus               |   string      |  The status of the image file. This can be one of the following values: <ul><li>None</li><li>PendingUpload</li><li>Uploaded</li><li>PendingDelete</li></ul>   |
|  id  |  string  | The ID for the image. This value is supplied by Partner Center.  |
|  description  |  string  | The description for the image.  |
|  imageType  |  string  | Indicates the type of the image. The following strings are currently supported. <p/>[Screenshot images](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix#screenshots): <ul><li>Screenshot (use this value for the desktop screenshot)</li><li>MobileScreenshot</li><li>XboxScreenshot</li><li>SurfaceHubScreenshot</li><li>HoloLensScreenshot</li></ul><p/>[Store logos](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix#store-logos):<ul><li>StoreLogo9x16 </li><li>StoreLogoSquare</li><li>Icon (use this value for the 1:1 300 x 300 pixels logo)</li></ul><p/>[Promotional images](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix#promotional-images): <ul><li>PromotionalArt16x9</li><li>PromotionalArtwork2400X1200</li></ul><p/>[Xbox images](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix#xbox-images): <ul><li>XboxBrandedKeyArt</li><li>XboxTitledHeroArt</li><li>XboxFeaturedPromotionalArt</li></ul><p/>[Optional promotional images](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix#optional-promotional-images): <ul><li>SquareIcon358X358</li><li>BackgroundImage1000X800</li><li>PromotionalArtwork414X180</li></ul><p/> <!-- The following strings are also recognized for this field, but they correspond to image types that are no longer for listings in the Store.<ul><li>PromotionalArtwork846X468</li><li>PromotionalArtwork558X756</li><li>PromotionalArtwork414X468</li><li>PromotionalArtwork558X558</li><li>WideIcon358X173</li><li>Unknown</li></ul> -->   |


<span id="gaming-options-object"></span>

### Gaming options resource

This resource contains game-related settings for the app. The values in this resource correspond to the [game settings](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-msix#game-settings) for submissions in Partner Center.

```json
{
  "gamingOptions": [
    {
      "genres": [
        "Games_ActionAndAdventure",
        "Games_Casino"
      ],
      "isLocalMultiplayer": true,
      "isLocalCooperative": true,
      "isOnlineMultiplayer": false,
      "isOnlineCooperative": false,
      "localMultiplayerMinPlayers": 2,
      "localMultiplayerMaxPlayers": 12,
      "localCooperativeMinPlayers": 2,
      "localCooperativeMaxPlayers": 12,
      "isBroadcastingPrivilegeGranted": true,
      "isCrossPlayEnabled": false,
      "kinectDataForExternal": "Enabled"
    }
  ],
}
```

This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  genres               |    array     |  An array of one or more of the following strings that describe the genres of the game: <ul><li>Games_ActionAndAdventure</li><li>Games_CardAndBoard</li><li>Games_Casino</li><li>Games_Educational</li><li>Games_FamilyAndKids</li><li>Games_Fighting</li><li>Games_Music</li><li>Games_Platformer</li><li>Games_PuzzleAndTrivia</li><li>Games_RacingAndFlying</li><li>Games_RolePlaying</li><li>Games_Shooter</li><li>Games_Simulation</li><li>Games_Sports</li><li>Games_Strategy</li><li>Games_Word</li></ul>    |
|  isLocalMultiplayer               |    boolean     |  Indicates whether the game supports local multiplayer.      |     
|  isLocalCooperative               |   boolean      |  Indicates whether the game supports local co-op.    |     
|  isOnlineMultiplayer               |   boolean      |  Indicates whether the game supports online multiplayer.    |     
|  isOnlineCooperative               |   boolean      |  Indicates whether the game supports online co-op.    |     
|  localMultiplayerMinPlayers               |   int      |   Specifies the minimum number of players the game supports for local multiplayer.   |     
|  localMultiplayerMaxPlayers               |   int      |   Specifies the maximum number of players the game supports for local multiplayer.  |     
|  localCooperativeMinPlayers               |   int      |   Specifies the minimum number of players the game supports for local co-op.  |     
|  localCooperativeMaxPlayers               |   int      |   Specifies the maximum number of players the game supports for local co-op.  |     
|  isBroadcastingPrivilegeGranted               |   boolean      |  Indicates whether the game supports broadcasting.   |     
|  isCrossPlayEnabled               |   boolean      |   Indicates whether the game supports multiplayer sessions between players on Windows 10 and Windows 11 PCs and Xbox.  |     
|  kinectDataForExternal               |   string      |  One of the following string values that indicates whether the game can collect Kinect data and send it to external services: <ul><li>NotSet</li><li>Unknown</li><li>Enabled</li><li>Disabled</li></ul>   |

> [!NOTE]
> The *gamingOptions* resource was added in May 2017, after the Microsoft Store submission API was first released to developers. If you created a submission for an app via the submission API before this resource was introduced and this submission is still in progress, this resource will be null for submissions for the app until you successfully commit the submission or you delete it. If the *gamingOptions* resource is not available for submissions for an app, the *hasAdvancedListingPermission* field of the [Application resource](get-app-data.md#application_object) returned by the [get an app](get-an-app.md) method is false.

<span id="status-details-object"></span>

### Status details resource

This resource contains additional details about the status of a submission. This resource has the following values.

| Value           | Type    | Description         |
|-----------------|---------|------|
|  errors               |    object     |   An array of [status detail resources](#status-detail-object) that contain error details for the submission.    |     
|  warnings               |   object      | An array of [status detail resources](#status-detail-object) that contain warning details for the submission.      |
|  certificationReports               |     object    |   An array of [certification report resources](#certification-report-resource) that provide access to the certification report data for the submission. You can examine these reports for more information if the certification fails.   |  


<span id="status-detail-object"></span>

### Status detail resource

This resource contains additional information about any related errors or warnings for a submission. This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  code               |    string     |   A [submission status code](#submission-status-code) that describes the type of error or warning.   |     
|  details               |     string    |  A message with more details about the issue.     |


<span id="application-package-object"></span>

### Application package resource

This resource contains details about an app package for the submission.

```json
{
  "applicationPackages": [
    {
      "fileName": "contoso_app.appx",
      "fileStatus": "Uploaded",
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
}
```

This resource has the following values.  

> [!NOTE]
> When calling the [update an app submission](update-an-app-submission.md) method, only the *fileName*, *fileStatus*, *minimumDirectXVersion*, and *minimumSystemRam* values of this object are required in the request body. The other values are populated by Partner Center.

| Value           | Type    | Description                   |
|-----------------|---------|------|
| fileName   |   string      |  The name of the package.    |  
| fileStatus    | string    |  The status of the package. This can be one of the following values: <ul><li>None</li><li>PendingUpload</li><li>Uploaded</li><li>PendingDelete</li></ul>    |  
| id    |  string   |  An ID that uniquely identifies the package. This value is provided by Partner Center.   |     
| version    |  string   |  The version of the app package. For more information, see [Package version numbering](/windows/apps/publish/publish-your-app/package-version-numbering?pivots=store-installer-msix).   |   
| architecture    |  string   |  The architecture of the package (for example, ARM).   |     
| languages    | array    |  An array of language codes for the languages the app supports. For more information, see [Supported languages](/windows/apps/publish/publish-your-app/supported-languages?pivots=store-installer-msix).    |     
| capabilities    |  array   |  An array of capabilities required by the package. For more information about capabilities, see [App capability declarations](../packaging/app-capability-declarations.md).   |     
| minimumDirectXVersion    |  string   |  The minimum DirectX version that is supported by the app package. This can be set only for apps that target Windows 8.x. For apps that target other OS versions, this value must be present when calling the [update an app submission](update-an-app-submission.md) method but the value you specify is ignored. This can be one of the following values: <ul><li>None</li><li>DirectX93</li><li>DirectX100</li></ul>   |     
| minimumSystemRam    | string    |  The minimum RAM that is required by the app package. This can be set only for apps that target Windows 8.x. For apps that target other OS versions, this value must be present when calling the [update an app submission](update-an-app-submission.md) method but the value you specify is ignored. This can be one of the following values: <ul><li>None</li><li>Memory2GB</li></ul>   |       
| targetDeviceFamilies    | array    |  An array of strings that represent the device families that the package targets. This value is used only for packages that target Windows 10; for packages that target earlier releases, this value has the value **None**. The following device family strings are currently supported for Windows 10 and Windows 11 packages, where *{0}* is a Windows 10 or Windows 11 version string such as 10.0.10240.0, 10.0.10586.0 or 10.0.14393.0: <ul><li>Windows.Universal min version *{0}*</li><li>Windows.Desktop min version *{0}*</li><li>Windows.Mobile min version *{0}*</li><li>Windows.Xbox min version *{0}*</li><li>Windows.Holographic min version *{0}*</li></ul>   |

</span>

<span>id="certification-report-resource"</span>

### Certification report resource

This resource provides access to the certification report data for a submission. This resource has the following values.

| Value           | Type    | Description             |
|-----------------|---------|------|
|     date            |    string     |  The date and time the report was generated, in ISO 8601 format.    |
|     reportUrl            |    string     |  The URL at which you can access the report.    |


<span id="package-delivery-options-object"></span>

### Package delivery options resource

This resource contains gradual package rollout and mandatory update settings for the submission.

```json
{
  "packageDeliveryOptions": {
    "packageRollout": {
        "isPackageRollout": false,
        "packageRolloutPercentage": 0,
        "packageRolloutStatus": "PackageRolloutNotStarted",
        "fallbackSubmissionId": "0"
    },
    "isMandatoryUpdate": false,
    "mandatoryUpdateEffectiveDate": "1601-01-01T00:00:00.0000000Z"
  },
}
```

This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
| packageRollout   |   object      |  A [package rollout resource](#package-rollout-object) that contains gradual package rollout settings for the submission.   |  
| isMandatoryUpdate    | boolean    |  Indicates whether you want to treat the packages in this submission as mandatory for self-installing app updates. For more information about mandatory packages for self-installing app updates, see [Download and install package updates for your app](../packaging/self-install-package-updates.md).    |  
| mandatoryUpdateEffectiveDate    |  date   |  The date and time when the packages in this submission become mandatory, in ISO 8601 format and UTC time zone.   |        

<span id="package-rollout-object"></span>

### Package rollout resource

This resource contains gradual [package rollout settings](#manage-gradual-package-rollout) for the submission. This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
| isPackageRollout   |   boolean      |  Indicates whether gradual package rollout is enabled for the submission.    |  
| packageRolloutPercentage    | float    |  The percentage of users who will receive the packages in the gradual rollout.    |  
| packageRolloutStatus    |  string   |  One of the following strings that indicates the status of the gradual package rollout: <ul><li>PackageRolloutNotStarted</li><li>PackageRolloutInProgress</li><li>PackageRolloutComplete</li><li>PackageRolloutStopped</li></ul>  |  
| fallbackSubmissionId    |  string   |  The ID of the submission that will be received by customers who do not get the gradual rollout packages.   |          

> [!NOTE]
> The *packageRolloutStatus* and *fallbackSubmissionId* values are assigned by Partner Center, and are not intended to be set by the developer. If you include these values in a request body, these values will be ignored.

<span id="trailer-object"></span>

### Trailers resource

This resource represents a video trailer for the app listing. The values in this resource correspond to the [trailers](/windows/apps/publish/publish-your-app/screenshots-and-images?pivots=store-installer-msix#trailers) options for submissions in Partner Center.

You can add up to 15 trailer resources to the *trailers* array in an [app submission resource](#app-submission-object). To upload trailer video files and thumbnail images for a submission, add these files to the same ZIP archive that contains the packages and listing images for the submission, and then upload this ZIP archive to the shared access signature (SAS) URI for the submission. For more information uploading the ZIP archive to the SAS URI, see [Create an app submission](#create-an-app-submission).

```json
{
  "trailers": [
    {
      "id": "1158943556954955699",
      "videoFileName": "Trailers\\ContosoGameTrailer.mp4",
      "videoFileId": "1159761554639123258",
      "trailerAssets": {
        "en-us": {
          "title": "Contoso Game",
          "imageList": [
            {
              "fileName": "Images\\ContosoGame-Thumbnail.png",
              "id": "1155546904097346923",
              "description": "This is a still image from the video."
            }
          ]
        }
      }
    }
  ]
}
```

This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  id               |    string     |   The ID for the trailer. This value is provided by Partner Center.   |
|  videoFileName               |    string     |    The name of the trailer video file in the ZIP archive that contains files for the submission.    |     
|  videoFileId               |   string      |  The ID for the trailer video file. This value is provided by Partner Center.   |     
|  trailerAssets               |   object      |  A dictionary of key and value pairs, where each key is a language code and each value is a [trailer assets resource](#trailer-assets-object) that contains additional locale-specific assets for the trailer. For more information about the supported language codes, see [Supported languages](/windows/apps/publish/publish-your-app/supported-languages?pivots=store-installer-msix).    |     

> [!NOTE]
> The *trailers* resource was added in May 2017, after the Microsoft Store submission API was first released to developers. If you created a submission for an app via the submission API before this resource was introduced and this submission is still in progress, this resource will be null for submissions for the app until you successfully commit the submission or you delete it. If the *trailers* resource is not available for submissions for an app, the *hasAdvancedListingPermission* field of the [Application resource](get-app-data.md#application_object) returned by the [get an app](get-an-app.md) method is false.

<span id="trailer-assets-object"></span>

### Trailer assets resource

This resource contains additional locale-specific assets for a trailer that is defined in a [trailer resource](#trailer-object). This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
| title   |   string      |  The localized title of the trailer. The title is displayed when the user plays the trailer in full screen mode.     |  
| imageList    | array    |   An array that contains one [image](#image-for-trailer-object) resource that provides the thumbnail image for the trailer. You can only include one [image](#image-for-trailer-object) resource in this array.  |   


<span id="image-for-trailer-object"></span>

### Image resource (for a trailer)

This resource describes the thumbnail image for a trailer. This resource has the following values.

| Value           | Type    | Description           |
|-----------------|---------|------|
|  fileName               |    string     |   The name of the thumbnail image file in the ZIP archive that you uploaded for the submission.    |     
|  id  |  string  | The ID for the thumbnail image. This value is provided by Partner Center.  |
|  description  |  string  | The description for the thumbnail image. This value is metadata only, and is not displayed to users.   |

<span/>

## Enums

These methods use the following enums.

<span id="price-tiers"></span>

### Price tiers

The following values represent available price tiers in the [pricing resource](#pricing-object) resource for an app submission.

| Value           | Description        |
|-----------------|------|
|  Base               |   The price tier is not set; use the base price for the app.      |     
|  NotAvailable              |   The app is not available in the specified region.    |     
|  Free              |   The app is free.    |    
|  Tier*xxx*               |   A string that specifies the price tier for the app, in the format **Tier<em>xxxx</em>**. Currently, the following ranges of price tiers are supported:<br/><br/><ul><li>If the *isAdvancedPricingModel* value of the [pricing resource](#pricing-object) is **true**, the available price tier values for your account are **Tier1012** - **Tier1424**.</li><li>If the *isAdvancedPricingModel* value of the [pricing resource](#pricing-object) is **false**, the available price tier values for your account are **Tier2** - **Tier96**.</li></ul>To see the complete table of price tiers that are available for your developer account, including the market-specific prices that are associated with each tier, go to the **Pricing and availability** page for any of your app submissions in Partner Center and click the **view table** link in the **Markets and custom prices** section (for some developer accounts, this link is in the **Pricing** section).    |


<span id="enterprise-licensing"></span>

### Enterprise licensing values

The following values represent the organizational licensing behavior for the app. For more information about these options, see [Organizational licensing options](/windows/apps/publish/organizational-licensing).

> [!NOTE]
> Although you can configure the organizational licensing options for an app submission via the submission API, you cannot use this API to publish submissions for [volume purchases through the Microsoft Store for Business and Microsoft Store for Education](/windows/apps/publish/organizational-licensing). To publish submissions to the Microsoft Store for Business and Microsoft Store for Education, you must use Partner Center.


| Value           |  Description      |
|-----------------|---------------|
| None            |     Do not make your app available to enterprises with Store-managed (online) volume licensing.         |     
| Online        |     Make your app available to enterprises with Store-managed (online) volume licensing.  |
| OnlineAndOffline | Make your app available to enterprises with Store-managed (online) volume licensing, and make your app available to enterprises via disconnected (offline) licensing. |


<span id="submission-status-code"></span>

### Submission status code

The following values represent the status code of a submission.

| Value           |  Description      |
|-----------------|---------------|
| None            |     No code was specified.         |     
| InvalidArchive        |     The ZIP archive containing the package is invalid or has an unrecognized archive format.  |
| MissingFiles | The ZIP archive does not have all files which were listed in your submission data, or they are in the wrong location in the archive. |
| PackageValidationFailed | One or more packages in your submission failed to validate. |
| InvalidParameterValue | One of the parameters in the request body is invalid. |
| InvalidOperation | The operation you attempted is invalid. |
| InvalidState | The operation you attempted is not valid for the current state of the package flight. |
| ResourceNotFound | The specified package flight could not be found. |
| ServiceError | An internal service error prevented the request from succeeding. Try the request again. |
| ListingOptOutWarning | The developer removed a listing from a previous submission, or did not include listing information that is supported by the package. |
| ListingOptInWarning  | The developer added a listing. |
| UpdateOnlyWarning | The developer is trying to insert something that only has update support. |
| Other  | The submission is in an unrecognized or uncategorized state. |
| PackageValidationWarning | The package validation process resulted in a warning. |

<span/>

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get app data using the Microsoft Store submission API](get-app-data.md)
* [App submissions in Partner Center](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix)
