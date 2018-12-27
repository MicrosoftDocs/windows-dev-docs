---
ms.assetid: d305746a-d370-4404-8cde-c85765bf3578
description: Use this method in the Microsoft Store promotions API to manage targeting profiles for promotional ad campaigns.
title: Manage targeting profiles
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store promotions API, ad campaigns
ms.localizationpriority: medium
---
# Manage targeting profiles


Use these methods in the Microsoft Store promotions API to select the users, geographies and inventory types that you want to target for each delivery line in a promotional ad campaign. Targeting profiles can be created and reused across multiple delivery lines.

For more information about the relationship between targeting profiles and ad campaigns, delivery lines, and creatives, see [Run ad campaigns using Microsoft Store services](run-ad-campaigns-using-windows-store-services.md#call-the-windows-store-promotions-api).

## Prerequisites

To use these methods, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](run-ad-campaigns-using-windows-store-services.md#prerequisites) for the Microsoft Store promotions API.
* [Obtain an Azure AD access token](run-ad-campaigns-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for these methods. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request

These methods have the following URIs.

| Method type | Request URI                                                      |  Description  |
|--------|------------------------------------------------------------------|---------------|
| POST   | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/targeting-profile``` |  Creates a new targeting profile.  |
| PUT    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/targeting-profile/{targetingProfileId}``` |  Edits the targeting profile specified by *targetingProfileId*.  |
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/promotion/targeting-profile/{targetingProfileId}``` |  Gets the targeting profile specified by *targetingProfileId*.  |


### Header

| Header        | Type   | Description         |
|---------------|--------|---------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |
| Tracking ID   | GUID   | Optional. An ID that tracks the call flow.                                  |


### Request body

The POST and PUT methods require a JSON request body with the required fields of a [Targeting profile](#targeting-profile) object and any additional fields you want to set or change.


### Request examples

The following example demonstrates how to call the POST method to create a targeting profile.

```json
POST https://manage.devcenter.microsoft.com/v1.0/my/promotion/targeting-profile HTTP/1.1
Authorization: Bearer <your access token>

{
    "name": "Contoso App Campaign - Targeting Profile 1",
    "targetingType": "Manual",
    "age": [
      651,
      652],
    "gender": [
      700
    ],
    "country": [
      11,
      12
    ],
    "osVersion": [
      504
    ],
    "deviceType": [
      710
    ],
    "supplyType": [
      11470
    ]
}
```

The following example demonstrates how to call the GET method to retrieve a targeting profile.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/targeting-profile/310023951  HTTP/1.1
Authorization: Bearer <your access token>
```

<span/>

## Response

These methods return a JSON response body with a [Targeting profile](#targeting-profile) object that contains information about the targeting profile that was created, updated, or retrieved. The following example demonstrates a response body for these methods.

```json
{
  "Data": {
    "id": 310021746,
    "name": "Contoso App Campaign - Targeting Profile 1",
    "targetingType": "Manual",
    "age": [
      651,
      652
    ],
    "gender": [
      700
    ],
    "country": [
      6,
      13,
      29
    ],
    "osVersion": [
      504,
      505,
      506,
      507,
      508
    ],
    "deviceType": [
      710,
      711
    ],
    "supplyType": [
      11470
    ]
  }
}
```

<span id="targeting-profile"/>

## Targeting profile object

The request and response bodies for these methods contain the following fields. This table shows which fields are read-only (meaning that they cannot be changed in the PUT method) and which fields are required in the request body for the POST method.

| Field        | Type   |  Description      |  Read only  | Default  | Required for POST |  
|--------------|--------|---------------|------|-------------|------------|
|  id   |  integer   |  The ID of the targeting profile.     |   Yes    |       |   No      |       
|  name   |  string   |   The name of the targeting profile.    |    No   |      |  Yes     |       
|  targetingType   |  string   |  One of the following values: <ul><li>**Auto**: Specify this value to allow Microsoft to choose the targeting profile based on the settings for your app in Partner Center.</li><li>**Manual**: Specify this value to define your own targeting profile.</li></ul>     |  No     |  Auto    |   Yes    |       
|  age   |  array   |   One or more integers that identify the age ranges of the users to target. For a complete list of integers, see [Age values](#age-values) in this article.    |    No    |  null    |     No    |       
|  gender   |  array   |  One or more integers that identify the genders of the users to target. For a complete list of integers, see [Gender values](#gender-values) in this article.       |  No    |  null    |     No    |       
|  country   |  array   |  One or more integers that identify the country codes of the users to target. For a complete list of integers, see [Country code values](#country-code-values) in this article.    |  No    |  null   |      No   |       
|  osVersion   |  array   |   One or more integers that identify the OS versions of the users to target. For a complete list of integers, see [OS version values](#osversion-values) in this article.     |   No    |  null   |     No    |       
|  deviceType   | array    |  One or more integers that identify the device types of the users to target. For a complete list of integers, see [Device type values](#devicetype-values) in this article.       |   No    |  null    |    No     |       
|  supplyType   |  array   |  One or more integers that identify the type of inventory where the campaign's ads will be shown. For a complete list of integers, see [Supply type values](#supplytype-values) in this article.      |   No    |  null   |     No    |   |  


<span id="age-values"/>

### Age values

The *age* field in the [TargetingProfile](#targeting-profile) object contains one or more of the following integers that identify the age ranges of the users to target.

|  Integer value for *age* field  |  Corresponding age range  |  
|---------------------------------|---------------------------|
|     651     |            13 to 17             |
|     652     |           18 to 24             |
|     653     |            25 to 34             |
|     654     |            35 to 49             |
|     655     |            50 and above             |

To get the supported values for the *age* field programmatically, you can call the following GET method.  For the ```Authorization``` header, pass your Azure AD access token in the form **Bearer** &lt;*token*&gt;.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/reference/age
Authorization: Bearer <your access token>
```

The following example shows the response body for this method.

```json
{
  "Data": {
    "Age": {
      "651": "Age13To17",
      "652": "Age18To24",
      "653": "Age25To34",
      "654": "Age35To49",
      "655": "Age50AndAbove"
    }
  }
}
```

<span id="gender-values"/>

### Gender values

The *gender* field in the [TargetingProfile](#targeting-profile) object contains one or more of the following integers that identify the genders of the users to target.

|  Integer value for *gender* field  |  Corresponding gender  |  
|---------------------------------|---------------------------|
|     700     |            Male             |
|     701     |           Female             |

To get the supported values for the *gender* field programmatically, you can call the following GET method.  For the ```Authorization``` header, pass your Azure AD access token in the form **Bearer** &lt;*token*&gt;.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/reference/gender
Authorization: Bearer <your access token>
```

The following example shows the response body for this method.

```json
{
  "Data": {
    "Gender": {
      "700": "Male",
      "701": "Female"
    }
  }
}
```


<span id="osversion-values"/>

### OS version values

The *osVersion* field in the [TargetingProfile](#targeting-profile) object contains one or more of the following integers that identify the OS versions of the users to target.

|  Integer value for *osVersion* field  |  Corresponding OS version  |  
|---------------------------------|---------------------------|
|     500     |            Windows Phone 7             |
|     501     |           Windows Phone 7.1             |
|     502     |           Windows Phone 7.5             |
|     503     |           Windows Phone 7.8             |
|     504     |           Windows Phone 8.0             |
|     505     |           Windows Phone 8.1             |
|     506     |           Windows 8.0             |
|     507     |           Windows 8.1             |
|     508     |           Windows 10             |
|     509     |           Windows 10 Mobile             |

To get the supported values for the *osVersion* field programmatically, you can call the following GET method.  For the ```Authorization``` header, pass your Azure AD access token in the form **Bearer** &lt;*token*&gt;.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/reference/osversion
Authorization: Bearer <your access token>
```

The following example shows the response body for this method.

```json
{
  "Data": {
    "OsVersion": {
      "500": "WindowsPhone70",
      "501": "WindowsPhone71",
      "502": "WindowsPhone75",
      "503": "WindowsPhone78",
      "504": "WindowsPhone80",
      "505": "WindowsPhone81",
      "506": "Windows80",
      "507": "Windows81",
      "508": "Windows10",
      "509": "WindowsPhone10"
    }
  }
}
```


<span id="devicetype-values"/>

### Device type values

The *deviceType* field in the [TargetingProfile](#targeting-profile) object contains one or more of the following integers that identify the device types of the users to target.

|  Integer value for *deviceType* field  |  Corresponding device type  |  Description  |
|---------------------------------|---------------------------|---------------------------|
|     710     |  Windows   |  This represents devices running a desktop version of Windows 10 or Windows 8.x.  |
|     711     |  Phone     |  This represents devices running Windows 10 Mobile, Windows Phone 8.x, or Windows Phone 7.x.

To get the supported values for the *deviceType* field programmatically, you can call the following GET method.  For the ```Authorization``` header, pass your Azure AD access token in the form **Bearer** &lt;*token*&gt;.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/reference/devicetype
Authorization: Bearer <your access token>
```

The following example shows the response body for this method.

```json
{
  "Data": {
    "DeviceType": {
      "710": "Windows",
      "711": "Phone"
    }
  }
}
```


<span id="supplytype-values"/>

### Supply type values

The *supplyType* field in the [TargetingProfile](#targeting-profile) object contains one or more of the following integers that identify the type of inventory where the campaign's ads will be shown.

|  Integer value for *supplyType* field  |  Corresponding supply type  |  Description  |
|---------------------------------|---------------------------|---------------------------|
|     11470     |  App        |  This refers to ads that appear in apps only.  |
|     11471     |  Universal        |  This refers to ads that appear in apps, on the Web, and on and other display surfaces.  |

To get the supported values for the *supplyType* field programmatically, you can call the following GET method.  For the ```Authorization``` header, pass your Azure AD access token in the form **Bearer** &lt;*token*&gt;.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/reference/supplytype
Authorization: Bearer <your access token>
```

The following example shows the response body for this method.

```json
{
  "Data": {
    "SupplyType": {
      "11470": "App",
      "11471": "Universal"
    }
  }
}
```

<span id="country-code-values"/>

### Country code values

The *country* field in the [TargetingProfile](#targeting-profile) object contains one or more of the following integers that identify the [ISO 3166-1 alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2) country codes of the users to target.

|  Integer value for *country* field  |  Corresponding country code  |  
|-------------------------------------|------------------------------|
|     1      |            US                  |
|     2      |            AU                  |
|     3      |            AT                  |
|     4      |            BE                  |
|     5      |            BR                  |
|     6      |            CA                  |
|     7      |            DK                  |
|     8      |            FI                  |
|     9      |            FR                  |
|     10      |            DE                  |
|     11      |            GR                  |
|     12      |            HK                  |
|     13      |            IN                  |
|     14      |            IE                  |
|     15      |            IT                  |
|     16      |            JP                  |
|     17      |            LU                  |
|     18      |            MX                  |
|     19      |            NL                  |
|     20      |            NZ                  |
|     21      |            NO                  |
|     22      |            PL                  |
|     23      |            PT                  |
|     24      |            SG                  |
|     25      |            ES                  |
|     26      |            SE                  |
|     27      |            CH                  |
|     28      |            TW                  |
|     29      |            GB                  |
|     30      |            RU                  |
|     31      |            CL                  |
|     32      |            CO                  |
|     33      |            CZ                  |
|     34      |            HU                  |
|     35      |            ZA                  |
|     36      |            KR                  |
|     37      |            CN                  |
|     38      |            RO                  |
|     39      |            TR                  |
|     40      |            SK                  |
|     41      |            IL                  |
|     42      |            ID                  |
|     43      |            AR                  |
|     44      |            MY                  |
|     45      |            PH                  |
|     46      |            PE                  |
|     47      |            UA                  |
|     48      |            AE                  |
|     49      |            TH                  |
|     50      |            IQ                  |
|     51      |            VN                  |
|     52      |            CR                  |
|     53      |            VE                  |
|     54      |            QA                  |
|     55      |            SI                  |
|     56      |            BG                  |
|     57      |            LT                  |
|     58      |            RS                  |
|     59      |            HR                  |
|     60      |            HR                  |
|     61      |            LV                  |
|     62      |            EE                  |
|     63      |            IS                  |
|     64      |            KZ                  |
|     65      |            SA                  |
|     67      |            AL                  |
|     68      |            DZ                  |
|     70      |            AO                  |
|     72      |            AM                  |
|     73      |            AZ                  |
|     74      |            BS                  |
|     75      |            BD                  |
|     76      |            BB                  |
|     77      |            BY                  |
|     81      |            BO                  |
|     82      |            BA                  |
|     83      |            BW                  |
|     87      |            KH                  |
|     88      |            CM                  |
|     94      |            CD                  |
|     95      |            CI                  |
|     96      |            CY                  |
|     99      |            DO                  |
|     100      |            EC                  |
|     101      |            EG                  |
|     102      |            SV                  |
|     107      |            FJ                  |
|     108      |            GA                  |
|     110      |            GE                  |
|     111      |            GH                  |
|     114      |            GT                  |
|     118      |            HT                  |
|     119      |            HN                  |
|     120      |            JM                  |
|     121      |            JO                  |
|     122      |            KE                  |
|     124      |            KW                  |
|     125      |            KG                  |
|     126      |            LA                  |
|     127      |            LB                  |
|     133      |            MK                  |
|     135      |            MW                  |
|     138      |            MT                  |
|     141      |            MU                  |
|     145      |            ME                  |
|     146      |            MA                  |
|     147      |            MZ                  |
|     148      |            NA                  |
|     150      |            NP                  |
|     151      |            NI                  |
|     153      |            NG                  |
|     154      |            OM                  |
|     155      |            PK                  |
|     157      |            PA                  |
|     159      |            PY                  |
|     167      |            SN                  |
|     172      |            LK                  |
|     176      |            TZ                  |
|     180      |            TT                  |
|     181      |            TN                  |
|     184      |            UG                  |
|     185      |            UY                  |
|     186      |            UZ                  |
|     189      |            ZM                  |
|     190      |            ZW                  |
|     219      |            MD                  |
|     224      |            PS                  |
|     225      |            RE                  |
|     246      |            PR                  |

To get the supported values for the *country* field programmatically, you can call the following GET method.  For the ```Authorization``` header, pass your Azure AD access token in the form **Bearer** &lt;*token*&gt;.

```json
GET https://manage.devcenter.microsoft.com/v1.0/my/promotion/reference/country
Authorization: Bearer <your access token>
```

The following example shows the response body for this method.

```json
{
  "Data": {
    "Country": {
      "1": "US",
      "2": "AU",
      "3": "AT",
      "4": "BE",
      "5": "BR",
      "6": "CA",
      "7": "DK",
      "8": "FI",
      "9": "FR",
      "10": "DE",
      "11": "GR",
      "12": "HK",
      "13": "IN",
      "14": "IE",
      "15": "IT",
      "16": "JP",
      "17": "LU",
      "18": "MX",
      "19": "NL",
      "20": "NZ",
      "21": "NO",
      "22": "PL",
      "23": "PT",
      "24": "SG",
      "25": "ES",
      "26": "SE",
      "27": "CH",
      "28": "TW",
      "29": "GB",
      "30": "RU",
      "31": "CL",
      "32": "CO",
      "33": "CZ",
      "34": "HU",
      "35": "ZA",
      "36": "KR",
      "37": "CN",
      "38": "RO",
      "39": "TR",
      "40": "SK",
      "41": "IL",
      "42": "ID",
      "43": "AR",
      "44": "MY",
      "45": "PH",
      "46": "PE",
      "47": "UA",
      "48": "AE",
      "49": "TH",
      "50": "IQ",
      "51": "VN",
      "52": "CR",
      "53": "VE",
      "54": "QA",
      "55": "SI",
      "56": "BG",
      "57": "LT",
      "58": "RS",
      "59": "HR",
      "60": "BH",
      "61": "LV",
      "62": "EE",
      "63": "IS",
      "64": "KZ",
      "65": "SA",
      "67": "AL",
      "68": "DZ",
      "70": "AO",
      "72": "AM",
      "73": "AZ",
      "74": "BS",
      "75": "BD",
      "76": "BB",
      "77": "BY",
      "81": "BO",
      "82": "BA",
      "83": "BW",
      "87": "KH",
      "88": "CM",
      "94": "CD",
      "95": "CI",
      "96": "CY",
      "99": "DO",
      "100": "EC",
      "101": "EG",
      "102": "SV",
      "107": "FJ",
      "108": "GA",
      "110": "GE",
      "111": "GH",
      "114": "GT",
      "118": "HT",
      "119": "HN",
      "120": "JM",
      "121": "JO",
      "122": "KE",
      "124": "KW",
      "125": "KG",
      "126": "LA",
      "127": "LB",
      "133": "MK",
      "135": "MW",
      "138": "MT",
      "141": "MU",
      "145": "ME",
      "146": "MA",
      "147": "MZ",
      "148": "NA",
      "150": "NP",
      "151": "NI",
      "153": "NG",
      "154": "OM",
      "155": "PK",
      "157": "PA",
      "159": "PY",
      "167": "SN",
      "172": "LK",
      "176": "TZ",
      "180": "TT",
      "181": "TN",
      "184": "UG",
      "185": "UY",
      "186": "UZ",
      "189": "ZM",
      "190": "ZW",
      "219": "MD",
      "224": "PS",
      "225": "RE",
      "246": "PR"
    }
  }
}
```

## Related topics

* [Run ad campaigns using Microsoft Store Services](run-ad-campaigns-using-windows-store-services.md)
* [Manage ad campaigns](manage-ad-campaigns.md)
* [Manage delivery lines for ad campaigns](manage-delivery-lines-for-ad-campaigns.md)
* [Manage creatives for ad campaigns](manage-creatives-for-ad-campaigns.md)
* [Get ad campaign performance data](get-ad-campaign-performance-data.md)
