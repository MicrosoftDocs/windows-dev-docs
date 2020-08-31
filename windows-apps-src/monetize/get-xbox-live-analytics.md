---
description: Use this method in the Microsoft Store analytics API to get Xbox Live analytics data.
title: Get Xbox Live analytics data
ms.date: 06/04/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox Live analytics
ms.localizationpriority: medium
---
# Get Xbox Live analytics data

Use this method in the Microsoft Store analytics API to get the last 30 days of general analytics data for customers playing your [Xbox Live-enabled game](/gaming/xbox-live/index.md), including device accessory usage, internet connection type, gamerscore distribution, game statistics, and friends and followers data. This information is also available in the [Xbox analytics report](../publish/xbox-analytics-report.md) in Partner Center.

> [!IMPORTANT]
> This method only supports games for Xbox or games that use Xbox Live services. These games must go through the [concept approval process](../gaming/concept-approval.md), which includes games published by [Microsoft partners](/gaming/xbox-live/developer-program-overview.md#microsoft-partners) and games submitted via the [ID@Xbox program](/gaming/xbox-live/developer-program-overview.md#id). This method does not currently support games published via the [Xbox Live Creators Program](/gaming/xbox-live/get-started-with-creators/get-started-with-xbox-live-creators.md).

Additional analytics data for Xbox Live-enabled games is available via the following methods:
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live health data](get-xbox-live-health-data.md)
* [Get Xbox Live Game Hub data](get-xbox-live-game-hub-data.md)
* [Get Xbox Live club data](get-xbox-live-club-data.md)
* [Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)
* [Get Xbox Live concurrent usage data](get-xbox-live-concurrent-usage-data.md)

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI       |
|--------|----------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  
|---------------|--------|---------------|------|
| applicationId | string | The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you want to retrieve general Xbox Live analytics data.  |  Yes  |
| metricType | string | A string that specifies the type of Xbox Live analytics data to retrieve. For this method, specify the value **productvalues**.  |  Yes  |


### Request example

The following example demonstrates a request for getting general analytics data for customers playing your Xbox Live-enabled game. Replace the *applicationId* value with the Store ID for your game.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/gameanalytics?applicationId=9NBLGGGZ5QDR&metrictype=productvalues HTTP/1.1
Authorization: Bearer <your access token>
```

## Response

This method returns a *Value* array that contains the following objects.

| Object      | Description                  |
|-------------|---------------------------------------------------|
| ProductData   |   Contains one [DeviceProperties](#deviceproperties) object and one [UserProperties](#userproperties) object that contain the last 30 days of device and user analytics data for your game.    |  
| XboxwideData   |  Contains one [DeviceProperties](#deviceproperties) object and one [UserProperties](#userproperties) object that contain the last 30 days of average device and user analytics data for all Xbox Live customers, as percentages. This data is included for comparison purposes with the data for your game.   |                                           


### DeviceProperties

This resource contains device usage data for your game or average device usage data for all Xbox Live customers during the last 30 days.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  applicationId               |    string     |  The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you retrieved analytics data.   |
|  connectionTypeDistribution               |    array     |   Contains objects that indicate how many customers use a wired internet connection versus a wireless internet connection on Xbox. Each object has two string fields: <ul><li>**conType**: Specifies the connection type.</li><li>**deviceCount**: In the **ProductData** object, this field specifies the number of your game's customers that use the connection type. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that use the connection type.</li></ul>   |     
|  deviceCount               |   string      |  In the **ProductData** object, this field specifies the number of customer devices on which your game has been played during the last 30 days. In the **XboxwideData** object, this field is always 1, indicating a starting percentage of 100% for data for all Xbox Live customers.   |     
|  eliteControllerPresentDeviceCount               |   string      |  In the **ProductData** object, this field specifies the number of your game's customers that use the Xbox Elite Wireless Controller. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that use the Xbox Elite Wireless Controller.  |     
|  externalDrivePresentDeviceCount               |   string      |  In the **ProductData** object, this field specifies the number of your game's customers that use an external hard drive on Xbox. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that use an external hard drive on Xbox.  |


### UserProperties

This resource contains user data for your game or average user data for all Xbox Live customers during the last 30 days.

| Value           | Type    | Description        |
|-----------------|---------|------|
|  applicationId               |    string     |   The [Store ID](in-app-purchases-and-trials.md#store-ids) of the game for which you retrieved analytics data.  |
|  userCount               |    string     |   In the **ProductData** object, this field specifies the number of customers that have played your game during the last 30 days. In the **XboxwideData** object, this field is always 1, indicating a starting percentage of 100% for data for all Xbox Live customers.   |     
|  dvrUsageCounts               |   array      |  Contains objects that indicate how many customers have used game DVR to record and view gameplay. Each object has two string fields: <ul><li>**dvrName**: Specifies the game DVR feature used. Possible values are **gameClipUploads**, **gameClipViews**, **screenshotUploads**, and **screenshotViews**.</li><li>**userCount**: In the **ProductData** object, this field specifies the number of your game's customers that used the specified game DVR feature. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that used the specified game DVR feature.</li></ul>   |     
|  followerCountPercentiles               |   array      |  Contains objects that provide details about the number of followers for customers. Each object has two string fields: <ul><li>**percentage**: Currently, this value is always 50, indicating that the follower data is provided as a median value.</li><li>**value**: In the **ProductData** object, this field specifies the median number of followers for your game's customers. In the **XboxwideData** object, this field specifies the median number of followers for all Xbox Live customers.</li></ul>  |   
|  friendCountPercentiles               |   array      |  Contains objects that provide details about the number of friends for customers. Each object has two string fields: <ul><li>**percentage**: Currently, this value is always 50, indicating that the friends data is provided as a median value.</li><li>**value**: In the **ProductData** object, this field specifies the median number of friends for your game's customers. In the **XboxwideData** object, this field specifies the median number of friends for all Xbox Live customers.</li></ul>  |     
|  gamerScoreRangeDistribution               |   array      |  Contains objects that provide details about the gamerscore distribution for customers. Each object has two string fields: <ul><li>**scoreRange**: The gamerscore range for which the following field provides usage data. For example, **10K-25K**.</li><li>**userCount**: In the **ProductData** object, this field specifies the number of your game's customers that have a  gamerscore in the specified range for all games they have played. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that have a gamerscore in the specified range for all games they have played.</li></ul>  |
|  titleGamerScoreRangeDistribution               |   array      |  Contains objects that provide details about the gamerscore distribution for your game. Each object has two string fields: <ul><li>**scoreRange**: The gamerscore range for which the following field provides usage data. For example, **100-200**.</li><li>**userCount**: In the **ProductData** object, this field specifies the number of your game's customers that have a  gamerscore in the specified range for your game. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that have a gamerscore in the specified range for your game.</li></ul>   |
|  socialUsageCounts               |   array      |  Contains objects that provide details about the social usage for customers. Each object has two string fields: <ul><li>**scName**: The type of social usage. For example, **gameInvites** and **textMessages**.</li><li>**userCount**: In the **ProductData** object, this field specifies the number of your game's customers that have participated in the specified social usage type. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that have participated in the specified social usage type.</li></ul>   |
|  streamingUsageCounts               |   array      |  Contains objects that provide details about the streaming usage for customers. Each object has two string fields: <ul><li>**stName**: The type of streaming platform. For example, **youtubeUsage**, **twitchUsage**, and **mixerUsage**.</li><li>**userCount**: In the **ProductData** object, this field specifies the number of your game's customers that have used the specified streaming platform. In the **XboxwideData** object, this field specifies the percentage of all Xbox Live customers that have used the specified streaming platform.</li></ul>  |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "ProductData": {
        "DeviceProperties": [
          {
            "applicationId": "9NBLGGGZ5QDR",
            "connectionTypeDistribution": [
              {
                "conType": "WIRED",
                "deviceCount": "43806"
              },
              {
                "conType": "WIRELESS",
                "deviceCount": "104035"
              }
            ],
            "deviceCount": "148063",
            "eliteControllerPresentDeviceCount": "10615",
            "externalDrivePresentDeviceCount": "46388"
          }
        ],
        "UserProperties": [
          {
            "applicationId": "9NBLGGGZ5QDR",
            "userCount": "142345",
            "dvrUsageCounts": [
              {
                "dvrName": "gameClipUploads",
                "userCount": "31264"
              },
              {
                "dvrName": "gameClipViews",
                "userCount": "52236"
              },
              {
                "dvrName": "screenshotUploads",
                "userCount": "27051"
              },
              {
                "dvrName": "screenshotViews",
                "userCount": "45640"
              }
            ],
            "followerCountPercentiles": [
              {
                "percentage": "50",
                "value": "11"
              }
            ],
            "friendCountPercentiles": [
              {
                "percentage": "50",
                "value": "11"
              }
            ],
            "gamerScoreRangeDistribution": [
              {
                "scoreRange": "10K-25K",
                "userCount": "30015"
              },
              {
                "scoreRange": "25K-50K",
                "userCount": "20495"
              },
              {
                "scoreRange": "3K-10K",
                "userCount": "32438"
              },
              {
                "scoreRange": "50K-100K",
                "userCount": "10608"
              },
              {
                "scoreRange": "<3K",
                "userCount": "45726"
              },
              {
                "scoreRange": ">100K",
                "userCount": "3063"
              }
            ],
            "titleGamerScoreRangeDistribution": [
              {
                "scoreRange": "400-600",
                "userCount": "133875"
              },
              {
                "scoreRange": "800-1000",
                "userCount": "45960"
              },
              {
                "scoreRange": "<100",
                "userCount": "269137"
              },
              {
                "scoreRange": "≥1K",
                "userCount": "11634"
              },
              {
                "scoreRange": "100-200",
                "userCount": "334471"
              },
              {
                "scoreRange": "600-800",
                "userCount": "123044"
              },
              {
                "scoreRange": "200-400",
                "userCount": "396725"
              }
            ],
            "socialUsageCounts": [
              {
                "scName": "gameInvites",
                "userCount": "82390"
              },
              {
                "scName": "textMessages",
                "userCount": "91880"
              },
              {
                "scName": "partySessionCount",
                "userCount": "68129"
              }
            ],
            "streamingUsageCounts": [
              {
                "stName": "youtubeUsage",
                "userCount": "74092"
              },
              {
                "stName": "twitchUsage",
                "userCount": "13401"
              }
              {
                "stName": "mixerUsage",
                "userCount": "22907"
              }
            ]
          }
        ]
      },
      "XboxwideData": {
        "DeviceProperties": [
          {
            "applicationId": "XBOXWIDE",
            "connectionTypeDistribution": [
              {
                "conType": "WIRED",
                "deviceCount": "0.213677732584786"
              },
              {
                "conType": "WIRELESS",
                "deviceCount": "0.786322267415214"
              }
            ],
            "deviceCount": "1",
            "eliteControllerPresentDeviceCount": "0.0476609278128012",
            "externalDrivePresentDeviceCount": "0.173747147416134"
          }
        ],
        "UserProperties": [
          {
            "applicationId": "XBOXWIDE",
            "userCount": "1",
            "dvrUsageCounts": [
              {
                "dvrName": "gameClipUploads",
                "userCount": "0.173210623993245"
              },
              {
                "dvrName": "gameClipViews",
                "userCount": "0.202104713778096"
              },
              {
                "dvrName": "screenshotUploads",
                "userCount": "0.136682414274251"
              },
              {
                "dvrName": "screenshotViews",
                "userCount": "0.158057895120314"
              }
            ],
            "followerCountPercentiles": [
              {
                "percentage": "50",
                "value": "5"
              }
            ],
            "friendCountPercentiles": [
              {
                "percentage": "50",
                "value": "5"
              }
            ],
            "gamerScoreRangeDistribution": [
              {
                "scoreRange": "10K-25K",
                "userCount": "0.134709282586519"
              },
              {
                "scoreRange": "25K-50K",
                "userCount": "0.0549468789343825"
              },
              {
                "scoreRange": "50K-100K",
                "userCount": "0.017301313342277"
              },
              {
                "scoreRange": "3K-10K",
                "userCount": "0.216512780268453"
              },
              {
                "scoreRange": "<3K",
                "userCount": "0.573515440094644"
              },
              {
                "scoreRange": ">100K",
                "userCount": "0.00301430477372488"
              }
            ],
            "titleGamerScoreRangeDistribution": [
              {
                "scoreRange": "100-200",
                "userCount": "0.178055695637076"
              },
              {
                "scoreRange": "200-400",
                "userCount": "0.173283639825241"
              },
              {
                "scoreRange": "400-600",
                "userCount": "0.0986865193958259"
              },
              {
                "scoreRange": "600-800",
                "userCount": "0.0506375775462092"
              },
              {
                "scoreRange": "800-1000",
                "userCount": "0.0232398822856435"
              },
              {
                "scoreRange": "<100",
                "userCount": "0.456443551582991"
              },
              {
                "scoreRange": "≥1K",
                "userCount": "0.0196531337270126"
              }
            ],
            "socialUsageCounts": [
              {
                "scName": "gameInvites",
                "userCount": "0.460375855738335"
              },
              {
                "scName": "textMessages",
                "userCount": "0.429256324070832"
              },
              {
                "scName": "partySessionCount",
                "userCount": "0.378446577751268"
              },
              {
                "scName": "gamehubViews",
                "userCount": "0.000197115778147329"
              }
            ],
            "streamingUsageCounts": [
              {
                "stName": "youtubeUsage",
                "userCount": "0.330320919178683"
              },
              {
                "stName": "twitchUsage",
                "userCount": "0.040666241835399"
              }
              {
                "stName": "mixerUsage",
                "userCount": "0.140193816053558"
              }
            ]
          }
        ]
      }
    }
  ],
  "@nextLink": null,
  "TotalCount": 4
}
```

## Related topics

* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get Xbox Live achievements data](get-xbox-live-achievements-data.md)
* [Get Xbox Live health data](get-xbox-live-health-data.md)
* [Get Xbox Live game hub data](get-xbox-live-game-hub-data.md)
* [Get Xbox Live club data](get-xbox-live-club-data.md)
* [Get Xbox Live multiplayer data](get-xbox-live-multiplayer-data.md)
* [Get Xbox Live concurrent usage data](get-xbox-live-concurrent-usage-data.md)