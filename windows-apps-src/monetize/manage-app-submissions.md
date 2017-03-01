---
author: mcleanbyron
ms.assetid: C7428551-4B31-4259-93CD-EE229007C4B8
description: Use these methods in the Windows Store submission API to manage submissions for apps that are registered to your Windows Dev Center account.
title: Manage app submissions
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Windows Store submission API, app submissions
---

# Manage app submissions

The Windows Store submission API provides methods you can use to manage submissions for your apps, including gradual package rollouts. For an introduction to the Windows Store submission API, including prerequisites for using the API, see [Create and manage submissions using Windows Store services](create-and-manage-submissions-using-windows-store-services.md).

>**Note**&nbsp;&nbsp;These methods can only be used for Windows Dev Center accounts that have been given permission to use the Windows Store submission API. This permission is being enabled to developer accounts in stages, and not all accounts have this permission enabled at this time. To request earlier access, log on to the Dev Center dashboard, click **Feedback** at the bottom of the dashboard, select **Submission API** for the feedback area, and submit your request. You'll receive an email when this permission is enabled for your account.

>**Important**&nbsp;&nbsp;If you use the Windows Store submission API to create a submission for an app, be sure to make further changes to the submission only by using the API, rather than the Dev Center dashboard. If you use the dashboard to change a submission that you originally created by using the API, you will no longer be able to change or commit that submission by using the API. In some cases, the submission could be left in an error state where it cannot proceed in the submission process. If this occurs, you must delete the submission and create a new submission.

<span id="methods-for-app-submissions" />
## Methods for managing app submissions

Use the following methods to get, create, update, commit, or delete an app submission. Before you can use these methods, the app must already exist in your Dev Center account and you must first create one submission for the app in the dashboard. For more information, see the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites).

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
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}```</td>
<td align="left">[Get an existing app submission](get-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">GET</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/status```</td>
<td align="left">[Get the status of an existing app submission](get-status-for-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions```</td>
<td align="left">[Create a new app submission](create-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">PUT</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}```</td>
<td align="left">[Update an existing app submission](update-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/commit```</td>
<td align="left">[Commit a new or updated app submission](commit-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">DELETE</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}```</td>
<td align="left">[Delete an app submission](delete-an-app-submission.md)</td>
</tr>
</tbody>
</table>

<span id="create-an-app-submission">
### Create an app submission

To create a submission for an app, follow this process.

1. If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Windows Store submission API.

  >**Note**&nbsp;&nbsp;Make sure the app already has at least one completed submission with the [age ratings](https://msdn.microsoft.com/windows/uwp/publish/age-ratings) information completed.

2. [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token). You must pass this access token to the methods in the Windows Store submission API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

3. [Create an app submission](create-an-app-submission.md) by executing the following method in the Windows Store submission API. This method creates a new in-progress submission, which is a copy of your last published submission.

  > [!div class="tabbedCodeSnippets"]
  ``` syntax
  POST https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions
  ```

  The response body contains three items: the ID of the new submission, the data for the new submission (including all the listings and pricing information), and the shared access signature (SAS) URI for uploading any app packages and listing images for the submission to Azure Blob storage.

  >**Note**&nbsp;&nbsp;A SAS URI provides access to a secure resource in Azure storage without requiring account keys. For background information about SAS URIs and their use with Azure Blob storage, see [Shared Access Signatures, Part 1: Understanding the SAS model](https://azure.microsoft.com/documentation/articles/storage-dotnet-shared-access-signature-part-1) and [Shared Access Signatures, Part 2: Create and use a SAS with Blob storage](https://azure.microsoft.com/documentation/articles/storage-dotnet-shared-access-signature-part-2/).

4. If you are adding new packages or images for the submission, [prepare the app packages](https://msdn.microsoft.com/windows/uwp/publish/app-package-requirements) and [prepare the app screenshots and images](https://msdn.microsoft.com/windows/uwp/publish/app-screenshots-and-images). Add all of these files to a ZIP archive.

5. Revise the submission data with any required changes for the new submission, and execute the following method to [update the app submission](update-an-app-submission.md).

  > [!div class="tabbedCodeSnippets"]
  ``` syntax
  PUT https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}
  ```

  <span/>
  >**Note**&nbsp;&nbsp;If you are adding new packages or images for the submission, make sure you update the submission data to refer to the name and relative path of these files in the ZIP archive.

4. If you are adding new packages or images for the submission, upload the ZIP archive to [Azure Blob storage](https://docs.microsoft.com/azure/storage/storage-introduction#blob-storage) using the SAS URI that was provided in the response body of the POST method you called earlier. There are different Azure libraries you can use to do this on a variety of platforms, including:

  * [Azure Storage Client Library for .NET](https://docs.microsoft.com/azure/storage/storage-dotnet-how-to-use-blobs)
  * [Azure Storage SDK for Java](https://docs.microsoft.com/azure/storage/storage-java-how-to-use-blob-storage)
  * [Azure Storage SDK for Python](https://docs.microsoft.com/azure/storage/storage-python-how-to-use-blob-storage)

  <span/>

  The following C# code example demonstrates how to upload a ZIP archive to Azure Blob storage using the [CloudBlockBlob](https://msdn.microsoft.com/library/azure/microsoft.windowsazure.storage.blob.cloudblockblob.aspx) class in the Azure Storage Client Library for .NET. This example assumes that the ZIP archive has already been written to a stream object.

  > [!div class="tabbedCodeSnippets"]
  ```csharp
  string sasUrl = "https://productingestionbin1.blob.core.windows.net/ingestion/26920f66-b592-4439-9a9d-fb0f014902ec?sv=2014-02-14&sr=b&sig=usAN0kNFNnYE2tGQBI%2BARQWejX1Guiz7hdFtRhyK%2Bog%3D&se=2016-06-17T20:45:51Z&sp=rwl";
  Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockBob =
      new Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob(new System.Uri(sasUrl));
  await blockBob.UploadFromStreamAsync(stream);
  ```

5. [Commit the app submission](commit-an-app-submission.md) by executing the following method. This will alert Dev Center that you are done with your submission and that your updates should now be applied to your account.

  > [!div class="tabbedCodeSnippets"]
  ``` syntax
  POST https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/commit
  ```

6. Check on the commit status by executing the following method to [get the status of the app submission](get-status-for-an-app-submission.md).

  > [!div class="tabbedCodeSnippets"]
  ``` syntax
  GET https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/status
  ```

  To confirm the submission status, review the *status* value in the response body. This value should change from **CommitStarted** to either **PreProcessing** if the request succeeds or to **CommitFailed** if there are errors in the request. If there are errors, the *statusDetails* field contains further details about the error.

7. After the commit has successfully completed, the submission is sent to the Store for ingestion. You can continue to monitor the submission progress by using the previous method, or by visiting the Dev Center dashboard.

<span/>
### Code examples for managing app submissions

The following articles provide detailed code examples that demonstrate how to create an app submission in several different programming languages:

* [C# code examples](csharp-code-examples-for-the-windows-store-submission-api.md)
* [Java code examples](java-code-examples-for-the-windows-store-submission-api.md)
* [Python code examples](python-code-examples-for-the-windows-store-submission-api.md)

>**Note**&nbsp;&nbsp;In addition to the code examples listed above, we also provide an open-source PowerShell module which implements a command-line interface on top of the Windows Store submission API. This module is called [StoreBroker](https://aka.ms/storebroker). You can use this module to manage your app, flight, and add-on submissions from the command line instead of calling the Windows Store submission API directly, or you can simply browse the source to see more examples for how to call this API. The StoreBroker module is actively used within Microsoft as the primary way that many first-party applications are submitted to the Store. For more information, see our [StoreBroker page on GitHub](https://aka.ms/storebroker).

<span id="manage-gradual-package-rollout">
## Methods for managing a gradual package rollout

You can gradually roll out the updated packages in an app submission to a percentage of your app’s customers on Windows 10. This allows you to monitor feedback and analytic data for the specific packages to make sure you’re confident about the update before rolling it out more broadly. You can change the rollout percentage (or halt the update) for a published submission without having to create a new submission. For more details, including instructions for how to enable and manage a gradual package rollout in the Dev Center dashboard, see [this article](../publish/gradual-package-rollout.md).

To programmatically enable a gradual package rollout for an app submission, follow this process using methods in the Windows Store submission API:

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
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/packagerollout```</td>
<td align="left">[Get the gradual rollout info for an app submission](get-package-rollout-info-for-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/updatepackagerolloutpercentage```</td>
<td align="left">[Update the gradual rollout percentage for an app submission](update-the-package-rollout-percentage-for-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/haltpackagerollout```</td>
<td align="left">[Halt the gradual rollout for an app submission](halt-the-package-rollout-for-an-app-submission.md)</td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">```https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/submissions/{submissionId}/finalizepackagerollout```</td>
<td align="left">[Finalize the gradual rollout for an app submission](finalize-the-package-rollout-for-an-app-submission.md)</td>
</tr>
</tbody>
</table>

<span/>
## Data resources
The Windows Store submission API methods for managing app submissions use the following JSON data resources.

<span id="app-submission-object" />
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
  "friendlyName": "Submission 2"
}
```

This resource has the following values.

| Value      | Type   | Description      |
|------------|--------|-------------------|
| id            | string  | The ID of the submission.  |
| applicationCategory           | string  |   A string that specifies the [category and/or subcategory](https://msdn.microsoft.com/windows/uwp/publish/category-and-subcategory-table) for your app. Categories and subcategories are combined into a single string with the underscore '_' character, such as **BooksAndReference_EReader**.      |  
| pricing           |  object  | A [pricing resource](#pricing-object) that contains pricing info for the app.        |   
| visibility           |  string  |  The visibility of the app. This can be one of the following values: <ul><li>Hidden</li><li>Public</li><li>Private</li><li>NotSet</li></ul>       |   
| targetPublishMode           | string  | The publish mode for the submission. This can be one of the following values: <ul><li>Immediate</li><li>Manual</li><li>SpecificDate</li></ul> |
| targetPublishDate           | string  | The publish date for the submission in ISO 8601 format, if the *targetPublishMode* is set to SpecificDate.  |  
| listings           |   object  |  A dictionary of key and value pairs, where each key is a country code and each value is a [listing resource](#listing-object) that contains listing info for the app.       |   
| hardwarePreferences           |  array  |   An array of strings that define the [hardware preferences](https://msdn.microsoft.com/windows/uwp/publish/enter-app-properties#hardware_preferences) for your app. This can be one of the following values: <ul><li>Touch</li><li>Keyboard</li><li>Mouse</li><li>Camera</li><li>NfcHce</li><li>Nfc</li><li>BluetoothLE</li><li>Telephony</li></ul>     |   
| automaticBackupEnabled           |  boolean  |   Indicates whether Windows can include your app's data in automatic backups to OneDrive. For more information, see [App declarations](https://msdn.microsoft.com/windows/uwp/publish/app-declarations).   |   
| canInstallOnRemovableMedia           |  boolean  |   Indicates whether customers can install your app to removable storage. For more information, see [App declarations](https://msdn.microsoft.com/windows/uwp/publish/app-declarations).     |   
| isGameDvrEnabled           |  boolean |   Indicates whether game DVR is enabled for the app.    |   
| hasExternalInAppProducts           |     boolean          |   Indicates whether your app allows users to make purchase outside the Windows Store commerce system. For more information, see [App declarations](https://msdn.microsoft.com/windows/uwp/publish/app-declarations).     |   
| meetAccessibilityGuidelines           |    boolean           |  Indicates whether your app has been tested to meet accessibility guidelines. For more information, see [App declarations](https://msdn.microsoft.com/windows/uwp/publish/app-declarations).      |   
| notesForCertification           |  string  |   Contains [notes for certification](https://msdn.microsoft.com/windows/uwp/publish/notes-for-certification) for your app.    |    
| status           |   string  |  The status of the submission. This can be one of the following values: <ul><li>None</li><li>Canceled</li><li>PendingCommit</li><li>CommitStarted</li><li>CommitFailed</li><li>PendingPublication</li><li>Publishing</li><li>Published</li><li>PublishFailed</li><li>PreProcessing</li><li>PreProcessingFailed</li><li>Certification</li><li>CertificationFailed</li><li>Release</li><li>ReleaseFailed</li></ul>      |    
| statusDetails           |   object  | A [status details resource](#status-details-object) that contains additional details about the status of the submission, including information about any errors.       |    
| fileUploadUrl           |   string  | The shared access signature (SAS) URI for uploading any packages for the submission. If you are adding new packages or images for the submission, upload the ZIP archive that contains the packages and images to this URI. For more information, see [Create an app submission](#create-an-app-submission).       |    
| applicationPackages           |   array  | An array of [application package resources](#application-package-object) that provide details about each package in the submission. |    
| packageDeliveryOptions    | object  | A [package delivery options resource](#package-delivery-options-object) that contains gradual package rollout and mandatory update settings for the submission.  |
| enterpriseLicensing           |  string  |  One of the [enterprise licensing values](#enterprise-licensing) values that indicate the enterprise licensing behavior for the app.  |    
| allowMicrosftDecideAppAvailabilityToFutureDeviceFamilies           |  boolean   |  Indicates whether Microsoft is allowed to [make the app available to future Windows 10 device families](https://msdn.microsoft.com/windows/uwp/publish/set-app-pricing-and-availability#windows-10-device-families).    |    
| allowTargetFutureDeviceFamilies           | object   |  A dictionary of key and value pairs, where each key is a [Windows 10 device family](https://msdn.microsoft.com/windows/uwp/publish/set-app-pricing-and-availability#windows-10-device-families) and each value is a boolean that indicates whether your app is allowed to target the specified device family.     |    
| friendlyName           |   string  |  The friendly name of the submission, as shown in the Dev Center dashboard. This value is generated for you when you create the submission.       |  


<span id="listing-object" />
### Listing resource

This resource contains listing info for an app. This resource has the following values.

| Value           | Type    | Description                  |
|-----------------|---------|------|
|  baseListing               |   object      |  The [base listing](#base-listing-object) info for the app, which defines the default listing info for all platforms.   |     
|  platformOverrides               | object |   A dictionary of key and value pairs, where each key is string that identifies a platform for which to override the listing info, and each value is a [base listing](#base-listing-object) resource (containing only the values from description to title) that specifies the listing info to override for the specified platform. The keys can have the following values: <ul><li>Unknown</li><li>Windows80</li><li>Windows81</li><li>WindowsPhone71</li><li>WindowsPhone80</li><li>WindowsPhone81</li></ul>     |      |     

<span id="base-listing-object" />
### Base listing resource

This resource contains base listing info for an app. This resource has the following values.

| Value           | Type    | Description       |
|-----------------|---------|------|
|  copyrightAndTrademarkInfo                |   string      |  Optional [copyright and/or trademark info](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#copyright-and-trademark-info).  |
|  keywords                |  array       |  An array of [keyword](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#keywords) to help your app appear in search results.    |
|  licenseTerms                |    string     | The optional [license terms](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#additional-license-terms) for your app.     |
|  privacyPolicy                |   string      |   The URL for the [privacy policy](../publish/create-app-store-listings.md#privacy-policy) for your app.    |
|  supportContact                |   string      |  The URL or email address for the [support contact info](../publish/create-app-store-listings.md#support-contact-info) for your app.     |
|  websiteUrl                |   string      |  The URL of the [web page](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#website) for your app.    |    
|  description               |    string     |   The [description](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#description) for the app listing.   |     
|  features               |    array     |  An array of up to 20 strings that list the [features](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#app-features) for your app.     |
|  releaseNotes               |  string       |  The [release notes](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#release-notes) for your app.    |
|  images               |   array      |  An array of [image and icon](#image-object) resources for the app listing.  |
|  recommendedHardware               |   array      |  An array of up to 11 strings that list the [recommended hardware configurations](https://msdn.microsoft.com/windows/uwp/publish/create-app-descriptions#recommended-hardware) for your app.     |
|  title               |     string    |   The title for the app listing.   |  

<span id="image-object" />
### Image resource

This resource contains image and icon data for an app listing. For more information about images and icons for listing, see [App screenshots and images](https://msdn.microsoft.com/windows/uwp/publish/app-screenshots-and-images). This resource has the following values.

| Value           | Type    | Description           |
|-----------------|---------|------|
|  fileName               |    string     |   The name of the image file in the ZIP archive that you uploaded for the submission.    |     
|  fileStatus               |   string      |  The status of the image file. This can be one of the following values: <ul><li>None</li><li>PendingUpload</li><li>Uploaded</li><li>PendingDelete</li></ul>   |
|  id  |  string  | The ID for the image, as specified by Dev Center.  |
|  description  |  string  | The description for the image.  |
|  imageType  |  string  | One of the following strings that indicates the type of the image: <ul><li>Unknown</li><li>Screenshot</li><li>PromotionalArtwork414X180</li><li>PromotionalArtwork846X468</li><li>PromotionalArtwork558X756</li><li>PromotionalArtwork414X468</li><li>PromotionalArtwork558X558</li><li>PromotionalArtwork2400X1200</li><li>Icon</li><li>WideIcon358X173</li><li>BackgroundImage1000X800</li><li>SquareIcon358X358</li><li>MobileScreenshot</li><li>XboxScreenshot</li><li>SurfaceHubScreenshot</li><li>HoloLensScreenshot</li></ul>      |


<span id="pricing-object" />
### Pricing resource

This resource contains pricing info for the app. This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  trialPeriod               |    string     |  A string that specifies the trial period for the app. This can be one of the following values: <ul><li>NoFreeTrial</li><li>OneDay</li><li>TrialNeverExpires</li><li>SevenDays</li><li>FifteenDays</li><li>ThirtyDays</li></ul>    |
|  marketSpecificPricings               |    object     |  A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [price tier](#price-tiers). These items represent the [custom prices for your app in specific markets](https://msdn.microsoft.com/windows/uwp/publish/define-pricing-and-market-selection#markets-and-custom-prices). Any items in this dictionary override the base price specified by the *priceId* value for the specified market.      |     
|  sales               |   array      |  **Deprecated**. An array of [sale resources](#sale-object) that contain sales information for the app.   |     
|  priceId               |   string      |  A [price tier](#price-tiers) that specifies the [base price](https://msdn.microsoft.com/windows/uwp/publish/define-pricing-and-market-selection#base-price) for the app.   |     
|  isAdvancedPricingModel               |   boolean      |  If **true**, your developer account has access to the expanded set of price tiers from .99 USD to 1999.99 USD. If **false**, your developer account has access to the original set of price tiers from .99 USD to 999.99 USD. For more information about the different tiers, see [price tiers](#price-tiers).<br/><br/>**Note**&nbsp;&nbsp;This field is read-only.   |


<span id="sale-object" />
### Sale resource

This resources contains sale info for an app.

>**Important**&nbsp;&nbsp;The **Sale** resource is no longer supported, and currently you cannot get or modify the sale data for an app submission using the Windows Store submission API:

   > * After calling the [GET method to get an app submission](get-an-app-submission.md), the *sales* value will be empty. You can continue to use the Dev Center dashboard to get the sale data for your app submission.
   > * When calling the [PUT method to update an app submission](update-an-app-submission.md), the information in the *sales* value is ignored. You can continue to use the Dev Center dashboard to change the sale data for your app submission.

> In the future, we will update the Windows Store submission API to introduce a new way to programmatically access sales information for app submissions.

This resource has the following values.

| Value           | Type    | Description    |
|-----------------|---------|------|
|  name               |    string     |   The name of the sale.    |     
|  basePriceId               |   string      |  The [price tier](#price-tiers) to use for the base price of the sale.    |     
|  startDate               |   string      |   The start date for the sale in ISO 8601 format.  |     
|  endDate               |   string      |  The end date for the sale in ISO 8601 format.      |     
|  marketSpecificPricings               |   object      |   A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [price tier](#price-tiers). These items represent the [custom prices for your app in specific markets](https://msdn.microsoft.com/windows/uwp/publish/define-pricing-and-market-selection#markets-and-custom-prices). Any items in this dictionary override the base price specified by the *basePriceId* value for the specified market.    |


<span id="status-details-object" />
### Status details resource

This resource contains additional details about the status of a submission. This resource has the following values.

| Value           | Type    | Description         |
|-----------------|---------|------|
|  errors               |    object     |   An array of [status detail resources](#status-detail-object) that contain error details for the submission.    |     
|  warnings               |   object      | An array of [status detail resources](#status-detail-object) that contain warning details for the submission.      |
|  certificationReports               |     object    |   An array of [certification report resources](#certification-report-object) that provide access to the certification report data for the submission. You can examine these reports for more information if the certification fails.   |  


<span id="status-detail-object" />
### Status detail resource

This resource contains additional information about any related errors or warnings for a submission. This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  code               |    string     |   A [submission status code](#submission-status-code) that describes the type of error or warning.   |     
|  details               |     string    |  A message with more details about the issue.     |


<span id="application-package-object" />
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

>**Note**&nbsp;&nbsp;When calling the [update an app submission](update-an-app-submission.md) method, only the *fileName*, *fileStatus*, *minimumDirectXVersion*, and *minimumSystemRam* values of this object are required in the request body. The other values are populated by Dev Center.

| Value           | Type    | Description                   |
|-----------------|---------|------|
| fileName   |   string      |  The name of the package.    |  
| fileStatus    | string    |  The status of the package. This can be one of the following values: <ul><li>None</li><li>PendingUpload</li><li>Uploaded</li><li>PendingDelete</li></ul>    |  
| id    |  string   |  An ID that uniquely identifies the package. This value is used by Dev Center.   |     
| version    |  string   |  The version of the app package. For more information, see [Package version numbering](https://msdn.microsoft.com/windows/uwp/publish/package-version-numbering).   |   
| architecture    |  string   |  The architecture of the package (for example, ARM).   |     
| languages    | array    |  An array of language codes for the languages the app supports. For more information, see For more information, see [Supported languages](https://msdn.microsoft.com/windows/uwp/publish/supported-languages).    |     
| capabilities    |  array   |  An array of capabilities required by the package. For more information about capabilities, see [App capability declarations](https://msdn.microsoft.com/windows/uwp/packaging/app-capability-declarations).   |     
| minimumDirectXVersion    |  string   |  The minimum DirectX version that is supported by the app package. This can be set only for apps that target Windows 8.x; it is ignored for apps that target other versions. This can be one of the following values: <ul><li>None</li><li>DirectX93</li><li>DirectX100</li></ul>   |     
| minimumSystemRam    | string    |  The minimum RAM that is required by the app package. This can be set only for apps that target Windows 8.x; it is ignored for apps that target other versions. This can be one of the following values: <ul><li>None</li><li>Memory2GB</li></ul>   |       
| targetDeviceFamilies    | array    |  An array of strings that represent the device families that the package targets. This value is used only for packages that target Windows 10; for packages that target earlier releases, this value has the value **None**. The following device family strings are currently supported for Windows 10 packages, where *{0}* is a Windows 10 version string such as 10.0.10240.0, 10.0.10586.0 or 10.0.14393.0: <ul><li>Windows.Universal min version *{0}*</li><li>Windows.Desktop min version *{0}*</li><li>Windows.Mobile min version *{0}*</li><li>Windows.Xbox min version *{0}*</li><li>Windows.Holographic min version *{0}*</li></ul>   |    

<span/>

<span id="certification-report-object" />
### Certification report resource

This resource provides access to the certification report data for a submission. This resource has the following values.

| Value           | Type    | Description             |
|-----------------|---------|------|
|     date            |    string     |  The date and time the report was generated, in in ISO 8601 format.    |
|     reportUrl            |    string     |  The URL at which you can access the report.    |


<span id="package-delivery-options-object" />
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

<span id="package-rollout-object" />
### Package rollout resource

This resource contains gradual [package rollout settings](#manage-gradual-package-rollout) for the submission. This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
| isPackageRollout   |   boolean      |  Indicates whether gradual package rollout is enabled for the submission.    |  
| packageRolloutPercentage    | float    |  The percentage of users who will receive the packages in the gradual rollout.    |  
| packageRolloutStatus    |  string   |  One of the following strings that indicates the status of the gradual package rollout: <ul><li>PackageRolloutNotStarted</li><li>PackageRolloutInProgress</li><li>PackageRolloutComplete</li><li>PackageRolloutStopped</li></ul>  |  
| fallbackSubmissionId    |  string   |  The ID of the submission that will be received by customers who do not get the gradual rollout packages.   |          

>**Note**&nbsp;&nbsp;The *packageRolloutStatus* and *fallbackSubmissionId* values are assigned by Dev Center, and are not intended to be set by the developer. If you include these values in a request body, these values will be ignored. 

<span/>

## Enums

These methods use the following enums.

<span id="price-tiers" />
### Price tiers

The following values represent available price tiers in the [pricing resource](#pricing-object) resource for an app submission.

| Value           | Description        |
|-----------------|------|
|  Base               |   The price tier is not set; use the base price for the app.      |     
|  NotAvailable              |   The app is not available in the specified region.    |     
|  Free              |   The app is free.    |    
|  Tier*xxx*               |   A string that specifies the price tier for the app, in the format **Tier<em>xxxx</em>**. Currently, the following ranges of price tiers are supported:<br/><br/><ul><li>If the *isAdvancedPricingModel* value of the [pricing resource](#pricing-object) is **true**, the available price tier values for your account are **Tier1012** - **Tier1424**.</li><li>If the *isAdvancedPricingModel* value of the [pricing resource](#pricing-object) is **false**, the available price tier values for your account are **Tier2** - **Tier96**.</li></ul>To see the complete table of price tiers that are available for your developer account, including the market-specific prices that are associated with each tier, go to the **Pricing and availability** page for any of your app submissions in the Dev Center dashboard and click the **view table** link in the **Markets and custom prices** section (for some developer accounts, this link is in the **Pricing** section).    |


<span id="enterprise-licensing" />
### Enterprise licensing values

The following values represent the enterprise licensing behavior for the app. For more information about these options, see [Organizational licensing options](https://msdn.microsoft.com/windows/uwp/publish/organizational-licensing).

| Value           |  Description      |
|-----------------|---------------|
| None            |     Do not make your app available to enterprises with Store-managed (online) volume licensing.         |     
| Online        |     Make your app available to enterprises with Store-managed (online) volume licensing.  |
| OnlineAndOffline | Make your app available to enterprises with Store-managed (online) volume licensing, and make your app available to enterprises via disconnected (offline) licensing. |


<span id="submission-status-code" />
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

* [Create and manage submissions using Windows Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Get app data using the Windows Store submission API](get-app-data.md)
* [App submissions in the Dev Center dashboard](https://msdn.microsoft.com/windows/uwp/publish/app-submissions)
