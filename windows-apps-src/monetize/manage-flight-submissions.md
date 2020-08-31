---
ms.assetid: 2A454057-FF14-40D2-8ED2-CEB5F27E0226
description: Use these methods in the Microsoft Store submission API to manage package flight submissions for apps that are registered to your Partner Center account.
title: Manage package flight submissions
ms.date: 04/16/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, flight submissions
ms.localizationpriority: medium
---
# Manage package flight submissions

The Microsoft Store submission API provides methods you can use to manage package flight submissions for your apps, including gradual package rollouts. For an introduction to the Microsoft Store submission API, including prerequisites for using the API, see [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md).

> [!IMPORTANT]
> If you use the Microsoft Store submission API to create a submission for a package flight, be sure to make further changes to the submission only by using the API, rather than Partner Center. If you use the dashboard to change a submission that you originally created by using the API, you will no longer be able to change or commit that submission by using the API. In some cases, the submission could be left in an error state where it cannot proceed in the submission process. If this occurs, you must delete the submission and create a new submission.

<span id="methods-for-package-flight-submissions" />

## Methods for managing package flight submissions

Use the following methods to get, create, update, commit, or delete a package flight submission. Before you can use these methods, the package flight must already exist in Partner Center. You can create a package flight [in Partner Center](../publish/package-flights.md) or by using the Microsoft Store submission API methods in described in [Manage package flights](manage-flights.md).

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
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}</td>
<td align="left"><a href="get-a-flight-submission.md">Get an existing package flight submission</a></td>
</tr>
<tr>
<td align="left">GET</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/status</td>
<td align="left"><a href="get-status-for-a-flight-submission.md">Get the status of an existing package flight submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions</td>
<td align="left"><a href="create-a-flight-submission.md">Create a new package flight submission</a></td>
</tr>
<tr>
<td align="left">PUT</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}</td>
<td align="left"><a href="update-a-flight-submission.md">Update an existing package flight submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/commit</td>
<td align="left"><a href="commit-a-flight-submission.md">Commit a new or updated package flight submission</a></td>
</tr>
<tr>
<td align="left">DELETE</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}</td>
<td align="left"><a href="delete-a-flight-submission.md">Delete a package flight submission</a></td>
</tr>
</tbody>
</table>

<span id="create-a-package-flight-submission">

## Create a package flight submission

To create a submission for a package flight, follow this process.

1. If you have not yet done so, complete the prerequisites described in [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md), including associating an Azure AD application with your Partner Center account and obtaining your client ID and key. You only need to do this one time; after you have the client ID and key, you can reuse them any time you need to create a new Azure AD access token.  

2. [Obtain an Azure AD access token](create-and-manage-submissions-using-windows-store-services.md#obtain-an-azure-ad-access-token). You must pass this access token to the methods in the Microsoft Store submission API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

3. [Create a package flight submission](create-a-flight-submission.md) by executing the following method in the Microsoft Store submission API. This method creates a new in-progress submission, which is a copy of your last published submission.

    ```json
    POST https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions
    ```

    The response body contains a [flight submission](#flight-submission-object) resource that includes the ID of the new submission, the shared access signature (SAS) URI for uploading any packages for the submission to Azure Blob storage, and the data for the new submission (including all the listings and pricing information).

    > [!NOTE]
    > A SAS URI provides access to a secure resource in Azure storage without requiring account keys. For background information about SAS URIs and their use with Azure Blob storage, see [Shared Access Signatures, Part 1: Understanding the SAS model](/azure/storage/common/storage-sas-overview) and [Shared Access Signatures, Part 2: Create and use a SAS with Blob storage](/azure/storage/common/storage-sas-overview).

4. If you are adding new packages for the submission, [prepare the packages](../publish/app-package-requirements.md) and add them to a ZIP archive.

5. Revise the [flight submission](#flight-submission-object) data with any required changes for the new submission, and execute the following method to [update the package flight submission](update-a-flight-submission.md).

    ```json
    PUT https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}
    ```
      > [!NOTE]
      > If you are adding new packages for the submission, make sure you update the submission data to refer to the name and relative path of these files in the ZIP archive.

4. If you are adding new packages for the submission, upload the ZIP archive to [Azure Blob storage](/azure/storage/storage-introduction#blob-storage) using the SAS URI that was provided in the response body of the POST method you called earlier. There are different Azure libraries you can use to do this on a variety of platforms, including:

    * [Azure Storage Client Library for .NET](/azure/storage/storage-dotnet-how-to-use-blobs)
    * [Azure Storage SDK for Java](/azure/storage/storage-java-how-to-use-blob-storage)
    * [Azure Storage SDK for Python](/azure/storage/storage-python-how-to-use-blob-storage)

    The following C# code example demonstrates how to upload a ZIP archive to Azure Blob storage using the [CloudBlockBlob](/dotnet/api/microsoft.windowsazure.storage.blob.cloudblockblob) class in the Azure Storage Client Library for .NET. This example assumes that the ZIP archive has already been written to a stream object.

    ```csharp
    string sasUrl = "https://productingestionbin1.blob.core.windows.net/ingestion/26920f66-b592-4439-9a9d-fb0f014902ec?sv=2014-02-14&sr=b&sig=usAN0kNFNnYE2tGQBI%2BARQWejX1Guiz7hdFtRhyK%2Bog%3D&se=2016-06-17T20:45:51Z&sp=rwl";
    Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockBob =
        new Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob(new System.Uri(sasUrl));
    await blockBob.UploadFromStreamAsync(stream);
    ```

5. [Commit the package flight submission](commit-a-flight-submission.md) by executing the following method. This will alert Partner Center that you are done with your submission and that your updates should now be applied to your account.

    ```json
    POST https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/commit
    ```

6. Check on the commit status by executing the following method to [get the status of the package flight submission](get-status-for-a-flight-submission.md).

    ```json
    GET https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/status
    ```

    To confirm the submission status, review the *status* value in the response body. This value should change from **CommitStarted** to either **PreProcessing** if the request succeeds or to **CommitFailed** if there are errors in the request. If there are errors, the *statusDetails* field contains further details about the error.

7. After the commit has successfully completed, the submission is sent to the Store for ingestion. You can continue to monitor the submission progress by using the previous method, or by visiting Partner Center.

<span/>

## Code examples

The following articles provide detailed code examples that demonstrate how to create a package flight submission in several different programming languages:

* [C# code examples](csharp-code-examples-for-the-windows-store-submission-api.md)
* [Java code examples](java-code-examples-for-the-windows-store-submission-api.md)
* [Python code examples](python-code-examples-for-the-windows-store-submission-api.md)

## StoreBroker PowerShell module

As an alternative to calling the Microsoft Store submission API directly, we also provide an open-source PowerShell module which implements a command-line interface on top of the API. This module is called [StoreBroker](https://github.com/Microsoft/StoreBroker). You can use this module to manage your app, flight, and add-on submissions from the command line instead of calling the Microsoft Store submission API directly, or you can simply browse the source to see more examples for how to call this API. The StoreBroker module is actively used within Microsoft as the primary way that many first-party applications are submitted to the Store.

For more information, see our [StoreBroker page on GitHub](https://github.com/Microsoft/StoreBroker).

<span id="manage-gradual-package-rollout">

## Manage a gradual package rollout for a package flight submission

You can gradually roll out the updated packages in a package flight submission to a percentage of your app’s customers on Windows 10. This allows you to monitor feedback and analytic data for the specific packages to make sure you’re confident about the update before rolling it out more broadly. You can change the rollout percentage (or halt the update) for a published submission without having to create a new submission. For more details, including instructions for how to enable and manage a gradual package rollout in Partner Center, see [this article](../publish/gradual-package-rollout.md).

To programmatically enable a gradual package rollout for a package flight submission, follow this process using methods in the Microsoft Store submission API:

  1. [Create a package flight submission](create-a-flight-submission.md) or [get a package flight submission](get-a-flight-submission.md).
  2. In the response data, locate the [packageRollout](#package-rollout-object) resource, set the *isPackageRollout* field to true, and set the *packageRolloutPercentage* field to the percentage of your app's customers who should get the updated packages.
  3. Pass the updated package flight submission data to the [update a package flight submission](update-a-flight-submission.md) method.

After a gradual package rollout is enabled for a package flight submission, you can use the following methods to programmatically get, update, halt, or finalize the gradual rollout.

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
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/packagerollout</td>
<td align="left"><a href="get-package-rollout-info-for-a-flight-submission.md">Get the gradual rollout info for a package flight submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/updatepackagerolloutpercentage</td>
<td align="left"><a href="update-the-package-rollout-percentage-for-a-flight-submission.md">Update the gradual rollout percentage for a package flight submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/haltpackagerollout</td>
<td align="left"><a href="halt-the-package-rollout-for-a-flight-submission.md">Halt the gradual rollout for a package flight submission</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/applications/{applicationId}/flights/{flightId}/submissions/{submissionId}/finalizepackagerollout</td>
<td align="left"><a href="finalize-the-package-rollout-for-a-flight-submission.md">Finalize the gradual rollout for a package flight submission</a></td>
</tr>
</tbody>
</table>

<span/>

## Data resources

The Microsoft Store submission API methods for managing package flight submissions use the following JSON data resources.

<span id="flight-submission-object" />

### Flight submission resource

This resource describes a package flight submission.

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

This resource has the following values.

| Value      | Type   | Description              |
|------------|--------|------------------------------|
| id            | string  | The ID for the submission.  |
| flightId           | string  |  The ID of the package flight that the submission is associated with.  |  
| status           | string  | The status of the submission. This can be one of the following values: <ul><li>None</li><li>Canceled</li><li>PendingCommit</li><li>CommitStarted</li><li>CommitFailed</li><li>PendingPublication</li><li>Publishing</li><li>Published</li><li>PublishFailed</li><li>PreProcessing</li><li>PreProcessingFailed</li><li>Certification</li><li>CertificationFailed</li><li>Release</li><li>ReleaseFailed</li></ul>   |
| statusDetails           | object  |  A [status details resource](#status-details-object) that contains additional details about the status of the submission, including information about any errors.  |
| flightPackages           | array  | Contains [flight package resources](#flight-package-object) that provide details about each package in the submission.   |
| packageDeliveryOptions    | object  | A [package delivery options resource](#package-delivery-options-object) that contains gradual package rollout and mandatory update settings for the submission.   |
| fileUploadUrl           | string  | The shared access signature (SAS) URI for uploading any packages for the submission. If you are adding new packages for the submission, upload the ZIP archive that contains the packages to this URI. For more information, see [Create a package flight submission](#create-a-package-flight-submission).  |
| targetPublishMode           | string  | The publish mode for the submission. This can be one of the following values: <ul><li>Immediate</li><li>Manual</li><li>SpecificDate</li></ul> |
| targetPublishDate           | string  | The publish date for the submission in ISO 8601 format, if the *targetPublishMode* is set to SpecificDate.  |
| notesForCertification           | string  |  Provides additional info for the certification testers, such as test account credentials and steps to access and verify features. For more information, see [Notes for certification](../publish/notes-for-certification.md). |

<span id="status-details-object" />

### Status details resource

This resource contains additional details about the status of a submission. This resource has the following values.

| Value           | Type    | Description                   |
|-----------------|---------|------|
|  errors               |    object     |   An array of [status detail resources](#status-detail-object) that contain error details for the submission.   |     
|  warnings               |   object      | An array of [status detail resources](#status-detail-object) that contain warning details for the submission.     |
|  certificationReports               |     object    |   An array of [certification report resources](#certification-report-object) that provide access to the certification report data for the submission. You can examine these reports for more information if the certification fails.    |  


<span id="status-detail-object" />

### Status detail resource

This resource contains additional information about any related errors or warnings for a submission. This resource has the following values.

| Value           | Type    | Description       |
|-----------------|---------|------|
|  code               |    string     |   A [submission status code](#submission-status-code) that describes the type of error or warning. |  
|  details               |     string    |  A message with more details about the issue.     |


<span id="certification-report-object" />

### Certification report resource

This resource provides access to the certification report data for a submission. This resource has the following values.

| Value           | Type    | Description         |
|-----------------|---------|------|
|     date            |    string     |  The date and time the report was generated, in ISO 8601 format.    |
|     reportUrl            |    string     |  The URL at which you can access the report.    |


<span id="flight-package-object" />

### Flight package resource

This resource provides details about a package in a submission.

```json
{
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
}
```

This resource has the following values.

> [!NOTE]
> When calling the [update a package flight submission](update-a-flight-submission.md) method, only the *fileName*, *fileStatus*, *minimumDirectXVersion*, and *minimumSystemRam* values of this object are required in the request body. The other values are populated by Partner Center.

| Value           | Type    | Description              |
|-----------------|---------|------|
| fileName   |   string      |  The name of the package.    |  
| fileStatus    | string    |  The status of the package. This can be one of the following values: <ul><li>None</li><li>PendingUpload</li><li>Uploaded</li><li>PendingDelete</li></ul>    |  
| id    |  string   |  An ID that uniquely identifies the package. This value is used by Partner Center.   |     
| version    |  string   |  The version of the app package. For more information, see [Package version numbering](../publish/package-version-numbering.md).   |   
| architecture    |  string   |  The architecture of the app package (for example, ARM).   |     
| languages    | array    |  An array of language codes for the languages the app supports. For more information, see For more information, see [Supported languages](../publish/supported-languages.md).    |     
| capabilities    |  array   |  An array of capabilities required by the package. For more information about capabilities, see [App capability declarations](../packaging/app-capability-declarations.md).   |     
| minimumDirectXVersion    |  string   |  The minimum DirectX version that is supported by the app package. This can be set only for apps that target Windows 8.x; it is ignored for apps that target other versions. This can be one of the following values: <ul><li>None</li><li>DirectX93</li><li>DirectX100</li></ul>   |     
| minimumSystemRam    | string    |  The minimum RAM that is required by the app package. This can be set only for apps that target Windows 8.x; it is ignored for apps that target other versions. This can be one of the following values: <ul><li>None</li><li>Memory2GB</li></ul>   |    


<span id="package-delivery-options-object" />

### Package delivery options resource

This resource contains gradual package rollout and mandatory update settings for the submission.

```json
{
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
}
```

This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|------|
| packageRollout   |   object      |   A [package rollout resource](#package-rollout-object) that contains gradual package rollout settings for the submission.    |  
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

> [!NOTE]
> The *packageRolloutStatus* and *fallbackSubmissionId* values are assigned by Partner Center, and are not intended to be set by the developer. If you include these values in a request body, these values will be ignored.

<span/>

## Enums

These methods use the following enums.

<span id="submission-status-code" />

### Submission status code

The following codes represent the status of a submission.

| Code           |  Description      |
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
* [Manage package flights using the Microsoft Store submission API](manage-flights.md)
* [Get a package flight submission](get-a-flight-submission.md)
* [Create a package flight submission](create-a-flight-submission.md)
* [Update a package flight submission](update-a-flight-submission.md)
* [Commit a package flight submission](commit-a-flight-submission.md)
* [Delete a package flight submission](delete-a-flight-submission.md)
* [Get the status of a package flight submission](get-status-for-a-flight-submission.md)