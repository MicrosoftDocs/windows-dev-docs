---
ms.assetid: 4F9657E5-1AF8-45E0-9617-45AF64E144FC
description: Use these methods in the Microsoft Store submission API to manage add-ons for apps that are registered to your Partner Center account.
title: Manage add-ons
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store submission API, add-ons, in-app product, IAP
ms.localizationpriority: medium
---
# Manage add-ons

Use the following methods in the Microsoft Store submission API to manage add-ons for your apps. For an introduction to the Microsoft Store submission API, including prerequisites for using the API, see [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md).

These methods can only be used to get, create, or delete add-ons. To create submissions for add-ons, see the methods in [Manage add-on submissions](manage-add-on-submissions.md).

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
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts</td>
<td align="left"><a href="get-all-add-ons.md">Get all add-ons for your apps</a></td>
</tr>
<tr>
<td align="left">GET</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{inAppProductId}</td>
<td align="left"><a href="get-an-add-on.md">Get a specific add-on</a></td>
</tr>
<tr>
<td align="left">POST</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts</td>
<td align="left"><a href="create-an-add-on.md">Create an add-on</a></td>
</tr>
<tr>
<td align="left">DELETE</td>
<td align="left">https://manage.devcenter.microsoft.com/v1.0/my/inappproducts/{inAppProductId}</td>
<td align="left"><a href="delete-an-add-on.md">Delete an add-on</a></td>
</tr>
</tbody>
</table>

## Prerequisites

If you have not done so already, complete all the [prerequisites](create-and-manage-submissions-using-windows-store-services.md#prerequisites) for the Microsoft Store submission API before trying to use any of these methods.

## Data resources

The Microsoft Store submission API methods for managing add-ons use the following JSON data resources.

<span id="add-on-object" />

### Add-on resource

This resource describes an add-on.

```json
{
  "applications": {
    "value": [
      {
        "id": "9NBLGGH4R315",
        "resourceLocation": "applications/9NBLGGH4R315"
      }
    ],
    "totalCount": 1
  },
  "id": "9NBLGGH4TNMP",
  "productId": "TestAddOn",
  "productType": "Durable",
  "pendingInAppProductSubmission": {
    "id": "1152921504621243619",
    "resourceLocation": "inappproducts/9NBLGGH4TNMP/submissions/1152921504621243619"
  },
  "lastPublishedInAppProductSubmission": {
    "id": "1152921504621243705",
    "resourceLocation": "inappproducts/9NBLGGH4TNMP/submissions/1152921504621243705"
  }
}
```

This resource has the following values.

| Value      | Type   | Description        |
|------------|--------|--------------|
| applications      | array  | An array that contains one [application resource](#application-object) that represents the app that this add-on is associated with. Only one item is supported in this array.  |
| id | string  | The Store ID of the add-on. This value is supplied by the Store. An example Store ID is 9NBLGGH4TNMP.  |
| productId | string  | The product ID of the add-on. This is the ID that was provided by the developer when the add-on was created. For more information, see [Set your product type and product ID](../publish/set-your-add-on-product-id.md). |
| productType | string  | The product type of the add-on. The following values are supported: **Durable** and **Consumable**.  |
| lastPublishedInAppProductSubmission       | object | A [submission resource](#submission-object) that provides information about the last published submission for the add-on.         |
| pendingInAppProductSubmission        | object  |  A [submission resource](#submission-object) that provides information about the current pending submission for the add-on.  |   |

<span id="application-object" />

### Application resource

This resource descries the app that an add-on is associated with. The following example demonstrates the format of this resource.

```json
{
  "applications": {
    "value": [
      {
        "id": "9NBLGGH4R315",
        "resourceLocation": "applications/9NBLGGH4R315"
      }
    ],
    "totalCount": 1
  },
}
```

This resource has the following values.

| Value           | Type    | Description        |
|-----------------|---------|-----------|
| value            | object  |  An object that contains the following values: <br/><br/> <ul><li>*id*. The Store ID of the app. For more information about the Store ID, see [View app identity details](../publish/view-app-identity-details.md).</li><li>*resourceLocation*. A relative path that you can append to the base ```https://manage.devcenter.microsoft.com/v1.0/my/``` request URI to retrieve the complete data for the app.</li></ul>   |
| totalCount   | int  | The number of app objects in the *applications* array of the response body.                                                                                                                                                 |

<span id="submission-object" />

### Submission resource

This resource provides information about a submission for an add-on. The following example demonstrates the format of this resource.

```json
{
  "pendingInAppProductSubmission": {
    "id": "1152921504621243619",
    "resourceLocation": "inappproducts/9NBLGGH4TNMP/submissions/1152921504621243619"
  },
}
```

This resource has the following values.

| Value           | Type    | Description     |
|-----------------|---------|------------------|
| id            | string  | The ID of the submission.    |
| resourceLocation   | string  | A relative path that you can append to the base ```https://manage.devcenter.microsoft.com/v1.0/my/``` request URI to retrieve the complete data for the submission.     |
 
<span/>

## Related topics

* [Create and manage submissions using Microsoft Store services](create-and-manage-submissions-using-windows-store-services.md)
* [Manage add-on submissions using the Microsoft Store submission API](manage-add-on-submissions.md)
* [Get all add-ons](get-all-add-ons.md)
* [Get an add-on](get-an-add-on.md)
* [Create an add-on](create-an-add-on.md)
* [Delete an add-on](delete-an-add-on.md)