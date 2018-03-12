---
author: mcleanbyron
description: Learn how to use the app metadata REST API to access certain types metadata for apps. This API is intended to be used by advertising networks to retrieve information about apps in the Microsoft Store so they can improve how they sell ad space to advertisers.
title: App metadata API for advertising networks
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, advertising network, app metadata
ms.assetid: f0904086-d61f-4adb-82b6-25968cbec7f3
ms.localizationpriority: medium
---

# App metadata API for advertising networks

Advertising networks can use the *app metadata API* to programmatically retrieve metadata about apps in the Microsoft Store, including details such as the description and category for the Store listing of the app and whether the app is targeted to children under 13. Access to this API is currently restricted to developers who are granted permission to the API by Microsoft.

This article provides instructions for how to request access to the API using the [app metadata API portal](https://admetadata.portal.azure-api.net/), how to get your subscription key to access the API, and how to call the API.

## Request access

Advertising networks can request access to the app metadata API by following these instructions:

1. Go to the [https://admetadata.portal.azure-api.net/signup](https://admetadata.portal.azure-api.net/signup) page of the app metadata API portal.
2. Enter the required information and click the **Sign up** button.
3. On the same site, click the **Products** tab and then click **App details for advertising**.
4. On the next page, click the **Subscribe** button. This submits your request to access the app metadata API to Microsoft.

After your request is submitted, you will receive an email within approximately 24 hours that notifies you if your request was granted or denied.

<span id="get-key" />

## Get your subscription key

If you are granted access to the app metadata API, follow these instructions to get your subscription key. You must pass this key in the request header of calls to the API.

1. Go to the [https://admetadata.portal.azure-api.net/signin](https://admetadata.portal.azure-api.net/signin) page of the app metadata API portal and sign in with your email and password.
2. Click your name in the top-right corner of the site and then click **Profile**.
3. In the **Your subscriptions** section of the page, click **Show** next to **Primary key**. This is your subscription key. Copy the key so you can use it later when you call the API.

<span id="call-the-api" />

## Call the API

After you have your subscription key, you are ready to call the API using HTTP REST syntax from the programming language of your choice. For information about the syntax of the API, see the [API syntax](#syntax) section below. To see code examples in C#, JavaScript, Python, and several other languages, click the **APIs** tab of the app metadata API portal, click **App details**, and then see the **Code samples** section on the bottom of the page.

Alternatively, you can call the API using the UI provided by the app metadata API portal:
  1. In the portal, click the **APIs** tab and then click **App details**.
  2. On the following page, enter the [app_id](#request-parameters) of the app for which you want to retrieve metadata in the **app_id** field and enter your subscription key in the **Ocp_Apim_Subscription-Key** field.
  3. Click **Send**. The response appears at the bottom of the page.


<span id="syntax" />

## API syntax

This method has the following request syntax.

| Method | Request URI                                                      |
|--------|------------------------------------------------------------------|
| GET   | ```https://admetadata.azure-api.net/v1/app/{app_id}``` |

<span/>
 

### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Ocp-Apim-Subscription-Key | string | Required. The subscription key that you [retrieved from the app metadata API portal](#get-key).  |

<span/>

### Request parameters

| Name        | Type   | Description                                                                 |
|---------------|--------|-----------------------|
| app_id | string | Required. The ID of the app for which you want retrieve metadata. This can be one of the following values:<br/><br/><ul><li>The Store ID for the app. An example Store ID is 9NBLGGH29DM8.</li><li>The Product ID (also sometimes called the *app ID*) for an app that was originally built for Windows 8.x or Windows Phone 8.x. The Product ID is a GUID.</li></ul> |

<span/>

### Request example

The following example demonstrates how to retrieve metadata for an app that has the Store ID 9NBLGGH29DM8.

```syntax
GET https://admetadata.azure-api.net/v1/app/9NBLGGH29DM8 HTTP/1.1
Ocp-Apim-Subscription-Key: <subscription key>
```

### Response body

The following example demonstrates the JSON response body for a successful call to this method.

```json
{
    "storeId":"9NBLGGH29DM8",
    "name":"Contoso Sports App",
    "description":"Catch the latest scores and replays using Contoso Sports App!",
    "phoneStoreGuid":"920217d7-90da-4019-99e8-46e4a6bd2594",
    "windowsStoreGuid":"d7e982e7-fbf3-42b5-9dad-72b65bd4e248",
    "storeCategory":"Sports",
    "iabCategory":"Sports",
    "iabCategoryId":"IAB17",
    "coppa":false,
    "downloadUrl":"https://www.microsoft.com/store/apps/9nblggh29dm8",
    "isLive":true,
    "iconUrls":[
      "//store-images.microsoft.com/image/apps.15753.13510798883747357.b6981489-6fdd-4fec-b655-a822247d5a76.33529096-a723-4204-9da9-a5dd8b328d4e"
    ],
    "type":"App",
    "devices":[
      "PC",
      "Phone"
    ],
    "platformVersions":[
      "Windows.Universal"
    ],
    "screenshotUrls":[
      "//store-images.microsoft.com/image/apps.15901.19810723133740207.c9781069-6fef-5fba-a055-c922051d59b6.40129896-d083-5604-ab79-aba68bfa084f"
    ]
}
```

For more details about the values in the response body, see the following table.

| Value      | Type   | Description    |
|------------|--------|--------------------|
| storeId           | string  | The Store ID of the app. An example Store ID is 9NBLGGH29DM8.     |  
| name           | string  | The name of the app.   |
| description           | string  | The description from the Store listing for the app.  |
| phoneStoreGuid           | string  | The Product ID (Windows Phone 8.x) for the app. This is a GUID.  |
| windowsStoreGuid           | string  | The Product ID (Windows 8.x) for the app. This is a GUID. |
| storeCategory           | string  | The category for the app in the Store. For the supported values, see the [category and subcategory table](../publish/category-and-subcategory-table.md) for apps in the Store.  |
| iabCategory           | string  | The content category for the app as defined by the Interactive Advertising Bureau (IAB). For example, **News** or **Sports**. For a list of content categories, see the [IAB Tech Lab Content Taxonomy](https://www.iab.com/guidelines/iab-quality-assurance-guidelines-qag-taxonomy) page on the IAB web site.   |
| iabCategoryId           | string  | The ID of the content category for the app. For example, **IAB12** is the ID for the News category, and **IAB17** is the ID for the Sports category. For a list of content category IDs, see section 5.1 in the [OpenRTB API Specification](http://www.iab.com/wp-content/uploads/2015/05/OpenRTB_API_Specification_Version_2_3_1.pdf). |
| coppa           | Boolean  | True if the app is directed at children under the age of 13 and therefore has obligations under the Children's Online Privacy Protection Act (COPPA); otherwise, false.  |
| downloadUrl           | string  | The link to the app's listing in the Store. This link is in the format ```https://www.microsoft.com/store/apps/<Store ID>```.  |
| isLive           | Boolean  | True if the app is currently available in the Store; otherwise, false.  |
| iconUrls           | array  |  An array of one or more strings that contain the relative paths to the icon URLs associated with the app. To retrieve the icons, prepend *http* or *https* to the URLs.  |
| type           | string  | One of the following strings: **App** or **Game**.  |
| devices           |  array  | An array of one or more of the following strings that specify the device types that the app supports: **PC**, **Phone**, **Xbox**, **IoT**, **Server**, and **Holographic**.  |
| platformVersions           | array  |  An array of one or more of the following strings that specify the platforms that the app supports: **Windows.Universal**, **Windows.Windows8x**, and **Windows.WindowsPhone8x**.  |
| screenshotUrls           | array  | An array of one or more strings that contain the relative paths to the screenshot URLs for this app. To retrieve the screenshots, prepend *http* or *https* to the URLs.  |

<span/>



 

 
