---
ms.assetid: 66400066-24BF-4AF2-B52A-577F5C3CA474
description: Use these methods in the Microsoft Store submission API to manage add-on submissions for apps that are registered to your Partner Center account.
title: Manage add-on submissions
ms.date: 04/17/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, add-on submissions, in-app product, IAP
ms.localizationpriority: medium
---
# Manage add-on submissions

The Microsoft Store submission API provides methods you can use to manage add-on (also known as in-app product or IAP) submissions for your apps. For an introduction to the Microsoft Store submission API, including prerequisites for using the API, see [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md).

> [!IMPORTANT]
> If you use the Microsoft Store submission API to create a submission for an add-on, be sure to make further changes to the submission only by using the API, rather than making changes in Partner Center. If you use Partner Center to change a submission that you originally created by using the API, you will no longer be able to change or commit that submission by using the API. In some cases, the submission could be left in an error state where it cannot proceed in the submission process. If this occurs, you must delete the submission and create a new submission.

<span id="methods-for-add-on-submissions"></span>

## Methods for managing add-on submissions

Use the following methods to get, create, update, commit, or delete an add-on submission. Before you can use these methods, the add-on must already exist in your Partner Center account. You can create an add-on in Partner Center by [defining its product type and product ID](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on) or by using the Microsoft Store submission API methods in described in [Manage add-ons](manage-add-ons.md).

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
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}</td>
<td align="left"><a href="get-an-add-on-submission.md">Get an existing add-on submission</a></td>
</tr>
<tr>
<td align="left">GET</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}/status</td>
<td align="left"><a href="get-status-for-an-add-on-submission.md">Get the status of an existing add-on submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions</td>
<td align="left"><a href="create-an-add-on-submission.md">Create a new add-on submission</a></td>
</tr>
<tr>
<td align="left">PUT</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}</td>
<td align="left"><a href="update-an-add-on-submission.md">Update an existing add-on submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}/commit</td>
<td align="left"><a href="commit-an-add-on-submission.md">Commit a new or updated add-on submission</a></td>
</tr>
<tr>
<td align="left">DELETE</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}</td>
<td align="left"><a href="delete-an-add-on-submission.md">Delete an add-on submission</a></td>
</tr>
</tbody>
</table>

<span id="create-an-add-on-submission">

## Create an add-on submission

To create a submission for an add-on, follow this process.

1. If you have not yet done so, complete the prerequisites described in [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md), including associating an Azure AD application with your Partner Center account and obtaining your client ID and key. You only need to do this one time; after you have the client ID and key, you can reuse them any time you need to create a new Azure AD access token.  

2. [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token). You must pass this access token to the methods in the Microsoft Store submission API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

3. Execute the following method in the Microsoft Store submission API. This method creates a new in-progress submission, which is a copy of your last published submission. For more information, see [Create an add-on submission](create-an-add-on-submission.md).

    ```json
    POST https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions
    ```

    The response body contains an [add-on submission](#add-on-submission-object) resource that includes the ID of the new submission, the shared access signature (SAS) URI for uploading any add-on icons for the submission to Azure Blob Storage, and all of the data for the new submission (such as the listings and pricing information).

    > [!NOTE]
    > A SAS URI provides access to a secure resource in Azure storage without requiring account keys. For background information about SAS URIs and their use with Azure Blob Storage, see [Shared Access Signatures, Part 1: Understanding the SAS model](/azure/storage/common/storage-sas-overview) and [Shared Access Signatures, Part 2: Create and use a SAS with Blob storage](/azure/storage/common/storage-sas-overview).

4. If you are adding new icons for the submission, [prepare the icons](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on) and add them to a ZIP archive.

5. Update the [add-on submission](#add-on-submission-object) data with any required changes for the new submission, and execute the following method to update the submission. For more information, see [Update an add-on submission](update-an-add-on-submission.md).

    ```json
    PUT https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}
    ```
      > [!NOTE]
      > If you are adding new icons for the submission, make sure you update the submission data to refer to the name and relative path of these files in the ZIP archive.

4. If you are adding new icons for the submission, upload the ZIP archive to [Azure Blob Storage](/azure/storage/blobs/storage-blobs-introduction) using the SAS URI that was provided in the response body of the POST method you called earlier. There are different Azure libraries you can use to do this on a variety of platforms, including:

    * [Azure Storage Client Library for .NET](/azure/storage/storage-dotnet-how-to-use-blobs)
    * [Azure Storage SDK for Java](/azure/storage/storage-java-how-to-use-blob-storage)
    * [Azure Storage SDK for Python](/azure/storage/storage-python-how-to-use-blob-storage)

    The following C# code example demonstrates how to upload a ZIP archive to Azure Blob Storage using the [CloudBlockBlob](/dotnet/api/microsoft.azure.storage.blob.cloudblockblob?view=azure-dotnet-legacy&preserve-view=true) class in the Azure Storage Client Library for .NET. This example assumes that the ZIP archive has already been written to a stream object.

    ```csharp
    string sasUrl = "https://productingestionbin1.blob.core.windows.net/ingestion/26920f66-b592-4439-9a9d-fb0f014902ec?sv=2014-02-14&sr=b&sig=usAN0kNFNnYE2tGQBI%2BARQWejX1Guiz7hdFtRhyK%2Bog%3D&se=2016-06-17T20:45:51Z&sp=rwl";
    Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockBob =
      new Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob(new System.Uri(sasUrl));
    await blockBob.UploadFromStreamAsync(stream);
    ```

5. Commit the submission by executing the following method. This will alert Partner Center that you are done with your submission and that your updates should now be applied to your account. For more information, see [Commit an add-on submission](commit-an-add-on-submission.md).

    ```json
    POST https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}/commit
    ```

6. Check on the commit status by executing the following method. For more information, see [Get the status of an add-on submission](get-status-for-an-add-on-submission.md).

    ```json
    GET https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{id}/submissions/{submissionId}/status
    ```

    To confirm the submission status, review the *status* value in the response body. This value should change from **CommitStarted** to either **PreProcessing** if the request succeeds or to **CommitFailed** if there are errors in the request. If there are errors, the *statusDetails* field contains further details about the error.

7. After the commit has successfully completed, the submission is sent to the Store for ingestion. You can continue to monitor the submission progress by using the previous method, or by visiting Partner Center.

<span/>

## Code examples

The following articles provide detailed code examples that demonstrate how to create an add-on submission in several different programming languages:

* [C# code examples](csharp-code-examples-for-the-windows-store-submission-api.md)
* [Java code examples](java-code-examples-for-the-windows-store-submission-api.md)
* [Python code examples](python-code-examples-for-the-windows-store-submission-api.md)

## StoreBroker PowerShell module

As an alternative to calling the Microsoft Store submission API directly, we also provide an open-source PowerShell module which implements a command-line interface on top of the API. This module is called [StoreBroker](https://github.com/Microsoft/StoreBroker). You can use this module to manage your app, flight, and add-on submissions from the command line instead of calling the Microsoft Store submission API directly, or you can simply browse the source to see more examples for how to call this API. The StoreBroker module is actively used within Microsoft as the primary way that many first-party applications are submitted to the Store.

For more information, see our [StoreBroker page on GitHub](https://github.com/Microsoft/StoreBroker).

<span/>

## Data resources

The Microsoft Store submission API methods for managing add-on submissions use the following JSON data resources.

<span id="add-on-submission-object"></span>

### Add-on submission resource

This resource describes an add-on submission.

```json
{
  "id": "1152921504621243680",
  "contentType": "EMagazine",
  "keywords": [
    "books"
  ],
  "lifetime": "FiveDays",
  "listings": {
    "en": {
      "description": "English add-on description",
      "icon": {
        "fileName": "add-on-en-us-listing2.png",
        "fileStatus": "Uploaded"
      },
      "title": "Add-on Title (English)"
    },
    "ru": {
      "description": "Russian add-on description",
      "icon": {
        "fileName": "add-on-ru-listing.png",
        "fileStatus": "Uploaded"
      },
      "title": "Add-on Title (Russian)"
    }
  },
  "pricing": {
    "marketSpecificPricings": {
      "RU": "Tier3",
      "US": "Tier4",
    },
    "sales": [],
    "priceId": "Free",
    "isAdvancedPricingModel": true
  },
  "targetPublishDate": "2016-03-15T05:10:58.047Z",
  "targetPublishMode": "Immediate",
  "tag": "SampleTag",
  "visibility": "Public",
  "status": "PendingCommit",
  "statusDetails": {
    "errors": [
      {
        "code": "None",
        "details": "string"
      }
    ],
    "warnings": [
      {
        "code": "ListingOptOutWarning",
        "details": "You have removed listing language(s): []"
      }
    ],
    "certificationReports": [
      {
      }
    ]
  },
  "fileUploadUrl": "https://productingestionbin1.blob.core.windows.net/ingestion/26920f66-b592-4439-9a9d-fb0f014902ec?sv=2014-02-14&sr=b&sig=usAN0kNFNnYE2tGQBI%2BARQWejX1Guiz7hdFtRhyK%2Bog%3D&se=2016-06-17T20:45:51Z&sp=rwl",
  "friendlyName": "Submission 2"
}
```

This resource has the following values.

| Value      | Type   | Description        |
|------------|--------|----------------------|
| id            | string  | The ID of the submission. This ID is available in the response data for requests to [create an add-on submission](create-an-add-on-submission.md), [get all add-ons](get-all-add-ons.md), and [get an add-on](get-an-add-on.md). For a submission that was created in Partner Center, this ID is also available in the URL for the submission page in Partner Center.  |
| contentType           | string  |  The [type of content](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-add-on#content-type) that is provided in the add-on. This can be one of the following values: <ul><li>NotSet</li><li>BookDownload</li><li>EMagazine</li><li>ENewspaper</li><li>MusicDownload</li><li>MusicStream</li><li>OnlineDataStorage</li><li>VideoDownload</li><li>VideoStream</li><li>Asp</li><li>OnlineDownload</li></ul> |  
| keywords           | array  | An array of strings that contain up to 10 [keywords](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-add-on#keywords) for the add-on. Your app can query for add-ons using these keywords.   |
| lifetime           | string  |  The lifetime of the add-on. This can be one of the following values: <ul><li>Forever</li><li>OneDay</li><li>ThreeDays</li><li>FiveDays</li><li>OneWeek</li><li>TwoWeeks</li><li>OneMonth</li><li>TwoMonths</li><li>ThreeMonths</li><li>SixMonths</li><li>OneYear</li></ul> |
| listings           | object  |  A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [listing resource](#listing-object) that contains listing info for the add-on.  |
| pricing           | object  | A [pricing resource](#pricing-object) that contains pricing info for the add-on.   |
| targetPublishMode           | string  | The publish mode for the submission. This can be one of the following values: <ul><li>Immediate</li><li>Manual</li><li>SpecificDate</li></ul> |
| targetPublishDate           | string  | The publish date for the submission in ISO 8601 format, if the *targetPublishMode* is set to SpecificDate.  |
| tag           | string  |  The [custom developer data](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-add-on#custom-developer-data) for the add-on (this information was previously called the *tag*).   |
| visibility  | string  |  The visibility of the add-on. This can be one of the following values: <ul><li>Hidden</li><li>Public</li><li>Private</li><li>NotSet</li></ul>  |
| status  | string  |  The status of the submission. This can be one of the following values: <ul><li>None</li><li>Canceled</li><li>PendingCommit</li><li>CommitStarted</li><li>CommitFailed</li><li>PendingPublication</li><li>Publishing</li><li>Published</li><li>PublishFailed</li><li>PreProcessing</li><li>PreProcessingFailed</li><li>Certification</li><li>CertificationFailed</li><li>Release</li><li>ReleaseFailed</li></ul>   |
| statusDetails           | object  |  A [status details resource](#status-details-object) that contains additional details about the status of the submission, including information about any errors. |
| fileUploadUrl           | string  | The shared access signature (SAS) URI for uploading any packages for the submission. If you are adding new packages for the submission, upload the ZIP archive that contains the packages to this URI. For more information, see [Create an add-on submission](#create-an-add-on-submission).  |
| friendlyName  | string  |  The friendly name of the submission, as shown in Partner Center. This value is generated for you when you create the submission.  |

<span id="listing-object"></span>

### Listing resource

This resource contains [listing info for an add-on](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on). This resource has the following values.

| Value           | Type    | Description       |
|-----------------|---------|------|
|  description               |    string     |   The description for the add-on listing.   |     
|  icon               |   object      |An [icon resource](#icon-object) that contains data for the icon for the add-on listing.    |
|  title               |     string    |   The title for the add-on listing.   |  

<span id="icon-object"></span>

### Icon resource

This resource contains icon data for an add-on listing. This resource has the following values.

| Value           | Type    | Description     |
|-----------------|---------|------|
|  fileName               |    string     |   The name of the icon file in the ZIP archive that you uploaded for the submission. The icon must be a .png file that measures exactly 300 x 300 pixels.   |     
|  fileStatus               |   string      |  The status of the icon file. This can be one of the following values: <ul><li>None</li><li>PendingUpload</li><li>Uploaded</li><li>PendingDelete</li></ul>   |

<span id="pricing-object"></span>

### Pricing resource

This resource contains pricing info for the add-on. This resource has the following values.

| Value           | Type    | Description    |
|-----------------|---------|------|
|  marketSpecificPricings               |    object     |  A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [price tier](#price-tiers). These items represent the [custom prices for your add-on in specific markets](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-add-on). Any items in this dictionary override the base price specified by the *priceId* value for the specified market.     |     
|  sales               |   array      |  **Deprecated**. An array of [sale resources](#sale-object) that contain sales information for the add-on.     |     
|  priceId               |   string      |  A [price tier](#price-tiers) that specifies the [base price](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-add-on) for the add-on.    |    
|  isAdvancedPricingModel               |   boolean      |  If **true**, your developer account has access to the expanded set of price tiers from .99 USD to 1999.99 USD. If **false**, your developer account has access to the original set of price tiers from .99 USD to 999.99 USD. For more information about the different tiers, see [price tiers](#price-tiers).<br/><br/>**Note**&nbsp;&nbsp;This field is read-only.   |


<span id="sale-object"></span>

### Sale resource

This resources contains sale info for an add-on.

> [!IMPORTANT]
> The **Sale** resource is no longer supported, and currently you cannot get or modify the sale data for an add-on submission using the Microsoft Store submission API. In the future, we will update the Microsoft Store submission API to introduce a new way to programmatically access sales information for add-on submissions.
>    * After calling the [GET method to get an add-on submission](get-an-add-on-submission.md), the *sales* value will be empty. You can continue to use Partner Center to get the sale data for your add-on submission.
>    * When calling the [PUT method to update an add-on submission](update-an-add-on-submission.md), the information in the *sales* value is ignored. You can continue to use Partner Center to change the sale data for your add-on submission.

This resource has the following values.

| Value           | Type    | Description           |
|-----------------|---------|------|
|  name               |    string     |   The name of the sale.    |     
|  basePriceId               |   string      |  The [price tier](#price-tiers) to use for the base price of the sale.    |     
|  startDate               |   string      |   The start date for the sale in ISO 8601 format.  |     
|  endDate               |   string      |  The end date for the sale in ISO 8601 format.      |     
|  marketSpecificPricings               |   object      |   A dictionary of key and value pairs, where each key is a two-letter ISO 3166-1 alpha-2 country code and each value is a [price tier](#price-tiers). These items represent the [custom prices for your add-on in specific markets](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-add-on). Any items in this dictionary override the base price specified by the *basePriceId* value for the specified market.    |

<span id="status-details-object"></span>

### Status details resource

This resource contains additional details about the status of a submission. This resource has the following values.

| Value           | Type    | Description       |
|-----------------|---------|------|
|  errors               |    object     |   An array of [status detail resources](#status-detail-object) that contain error details for the submission.   |     
|  warnings               |   object      | An array of [status detail resources](#status-detail-object) that contain warning details for the submission.     |
|  certificationReports               |     object    |   An array of [certification report resources](#certification-report-object) that provide access to the certification report data for the submission. You can examine these reports for more information if the certification fails.    |  

<span id="status-detail-object"></span>

### Status detail resource

This resource contains additional information about any related errors or warnings for a submission. This resource has the following values.

| Value           | Type    | Description    |
|-----------------|---------|------|
|  code               |    string     |   A [submission status code](#submission-status-code) that describes the type of error or warning.   |     
|  details               |     string    |  A message with more details about the issue.     |

<span id="certification-report-object"></span>

### Certification report resource

This resource provides access to the certification report data for a submission. This resource has the following values.

| Value           | Type    | Description               |
|-----------------|---------|------|
|     date            |    string     |  The date and time the report was generated, in ISO 8601 format.    |
|     reportUrl            |    string     |  The URL at which you can access the report.    |

## Enums

These methods use the following enums.

<span id="price-tiers"></span>

### Price tiers

The following values represent available price tiers in the [pricing resource](#pricing-object) resource for an add-on submission.

| Value           | Description       |
|-----------------|------|
|  Base               |   The price tier is not set; use the base price for the add-on.      |     
|  NotAvailable              |   The add-on is not available in the specified region.    |     
|  Free              |   The add-on is free.    |    
|  Tier*xxxx*               |   A string that specifies the price tier for the add-on, in the format **Tier<em>xxxx</em>**. Currently, the following ranges of price tiers are supported:<br/><br/><ul><li>If the *isAdvancedPricingModel* value of the [pricing resource](#pricing-object) is **true**, the available price tier values for your account are **Tier1012** - **Tier1424**.</li><li>If the *isAdvancedPricingModel* value of the [pricing resource](#pricing-object) is **false**, the available price tier values for your account are **Tier2** - **Tier96**.</li></ul>To see the complete table of price tiers that are available for your developer account, including the market-specific prices that are associated with each tier, go to the **Pricing and availability** page for any of your app submissions in Partner Center and click the **view table** link in the **Markets and custom prices** section (for some developer accounts, this link is in the **Pricing** section).     |

<span id="submission-status-code"></span>

### Submission status code

The following values represent the status code of a submission.

| Value           |  Description      |
|-----------------|---------------|
|  None            |     No code was specified.         |     
|      InvalidArchive        |     The ZIP archive containing the package is invalid or has an unrecognized archive format.  |
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
* [Manage add-ons using the Microsoft Store submission API](manage-add-ons.md)
* [Add-on submissions in Partner Center](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on)