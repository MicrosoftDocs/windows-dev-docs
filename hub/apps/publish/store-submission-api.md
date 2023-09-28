---
description: Use the Microsoft Store Submission API to automate your store submissions
title: Microsoft Store submission API for MSI or EXE app
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, price, available, discoverable, free trial, trials, trial, apps, release date
ms.localizationpriority: medium
---

# Microsoft Store submission API for MSI or EXE app

Use the Microsoft Store submission API for MSI or EXE app to programmatically query and create submissions for MSI or EXE apps for your or your organization's Partner Center account. This API is useful if your account manages many apps, and you want to automate and optimize the submission process for these assets. This API uses Azure Active Directory (Azure AD) to authenticate the calls from your app or service.

The following steps describe the end-to-end process of using the Microsoft Store submission API:

1. Make sure that you have completed all the prerequisites.
2. Before you call a method in the Microsoft Store submission API, obtain an Azure AD access token. After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token.
3. Call the Microsoft Store submission API for MSI or EXE app.

## Step 1: Complete prerequisites for using the Microsoft Store submission API

Before you start writing code to call the Microsoft Store submission API for MSI or EXE app, make sure that you have completed the following prerequisites.

- You (or your organization) must have an Azure AD directory and you must have Global administrator permission for the directory. If you already use Microsoft 365 or other business services from Microsoft, you already have Azure AD directory. Otherwise, you can [create a new Azure AD in Partner Center](partner-center/manage-azure-ad-applications-in-partner-center.md) for no additional charge.
- You must associate an Azure AD application with your Partner Center account and obtain your tenant ID, client ID and key. You need these values to obtain an Azure AD access token, which you will use in calls to the Microsoft Store submission API.
- Prepare your app for use with the Microsoft Store submission API:
  - If your app does not yet exist in Partner Center, you must [create your app by reserving its name](publish-your-app/reserve-your-apps-name.md?pivots=store-installer-msi-exe) in Partner Center. You cannot use the Microsoft Store submission API to create an app in Partner Center; you must work in Partner Center to create it, and then after that you can use the API to access the app and programmatically create submissions for it.
  - Before you can create a submission for a given app using this API, you must first [create one submission for the app](publish-your-app/create-app-submission.md?pivots=store-installer-msi-exe)] in Partner Center, including answering the [age ratings](publish-your-app/age-ratings.md?pivots=store-installer-msi-exe) questionnaire. After you do this, you will be able to programmatically create new submissions for this app using the API.
  - If you are creating or updating an app submission and you need to include new package, [prepare the package details](publish-your-app/upload-app-packages.md?pivots=store-installer-msi-exe).
  - If you are creating or updating an app submission and you need to include screenshots or images for the Store listing, [prepare the app screenshots and images](publish-your-app/create-app-store-listing.md?pivots=store-installer-msi-exe).

### How to associate an Azure AD application with your Partner Center account

Before you can use the Microsoft Store submission API for MSI or EXE app, you must associate an Azure AD application with your Partner Center account, retrieve the tenant ID and client ID for the application and generate a key. The Azure AD application represents the app or service from which you want to call the Microsoft Store submission API. You need the tenant ID, client ID and key to obtain an Azure AD access token that you pass to the API.

> [!NOTE]
> You only need to perform this task one time. After you have the tenant ID, client ID and key, you can reuse them any time you need to create a new Azure AD access token.

1. In Partner Center, [associate your organization's Partner Center account with your organization's Azure AD directory](partner-center/associate-azure-ad-with-partner-center.md).
2. Next, from the Users page in the Account settings section of Partner Center, [add the Azure AD application](partner-center/manage-azure-ad-applications-in-partner-center.md) that represents the app or service that you will use to access submissions for your Partner Center account. Make sure you assign this application the Manager role. If the application doesn't exist yet in your Azure AD directory, you can [create a new Azure AD application in Partner Center](partner-center/manage-azure-ad-applications-in-partner-center.md#create-a-new-azure-ad-application-account-in-your-organizations-directory-and-add-it-to-your-partner-center-account).
3. Return to the Users page, click the name of your Azure AD application to go to the application settings, and copy down the Tenant ID and Client ID values.
4. To add a new key or Client secret, see the following instructions or refer instructions to [register app through Azure Portal](/azure/active-directory/develop/quickstart-register-app):

To register your app:

1. Sign in to the [Azure portal](https://portal.azure.com/).
2. If you have access to multiple tenants, use the Directories + subscriptions filter   in the top menu to switch to the tenant in which you want to register the application.
3. Search for and select Azure Active Directory.
4. Under Manage, select App registrations > Select your application.
5. Select Certificates & secrets > Client secrets > New client secret.
6. Add a description for your client secret.
7. Select an expiration for the secret or specify a custom lifetime.
8. Client secret lifetime is limited to two years (24 months) or less. You can't specify a custom lifetime longer than 24 months.

    > [!NOTE]
    > Microsoft recommends that you set an expiration value of less than 12 months.

9. Select Add.
10. Record the secret's value for use in your client application code. This secret value is never displayed again after you leave this page.

## Step 2: Obtain an Azure AD access token

Before you call any of the methods in the Microsoft Store submission API for MSI or EXE app, you must first obtain an Azure AD access token that you pass to the Authorization header of each method in the API. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can refresh the token so you can continue to use it in further calls to the API.

To obtain the access token, follow the instructions in [Service to Service Calls Using Client Credentials]/azure/active-directory/azuread-dev/v1-oauth2-client-creds-grant-flow) to send an HTTP POST to the https://login.microsoftonline.com/<tenant_id>/oauth2/token endpoint. Here is a sample request.

```json
POST https://login.microsoftonline.com/<tenant-id>/oauth2/v2.0/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded; charset=utf-8

grant_type=client_credentials
&client_id=<your_client_id>
&client_secret=<your_client_secret>
&scope=https://api.store.microsoft.com/.default
```

For the `tenant_id` value in the POST URI and the `client_id` and `client_secret` parameters, specify the tenant ID, client ID and the key for your application that you retrieved from Partner Center in the previous section. For the scope parameter, you must specify `https://api.store.microsoft.com/.default`.

After your access token expires, you can refresh it by following the instructions here.

For examples that demonstrate how to obtain an access token by using C# or Node.js, see the [code examples](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services#code-examples) for Microsoft Store submission API for MSI or EXE app.

## Step 3: Use the Microsoft Store submission API

After you have an Azure AD access token, you can call methods in the Microsoft Store submission API for MSI or EXE app. The API includes many methods that are grouped into scenarios for apps. To create or update submissions, you typically call multiple methods in a specific order. For information about each scenario and the syntax of each method, see the following sections:

> [!NOTE]
> After you obtain an access token, you have 60 minutes to call methods in the Microsoft Store submission API for MSI or EXE app before the token expires.

## Base URL

The base URL for the Microsoft Store Submission API for EXE or MSI app is: `https://api.store.microsoft.com`

## API Contracts

### Get Current Draft Submission Metadata API

Fetches metadata in each module (listings, properties or availability) under current draft submission.

**Path [All Modules]**: /submission/v1/product/{productId}/metadata?languages={languages}&includelanguagelist={true/false}<br>
**Path [Single Module]**: /submission/v1/product/{productId}/metadata/{moduleName}?languages={languages}&includelanguagelist={true/false}<br>
**Method**: GET

**Path Parameters**

| Parameter  | Description                                                  |
|------------|--------------------------------------------------------------|
| productId  | The Partner Center ID of the product                         |
| moduleName | Partner Center module – listings, properties or availability |

**Query Parameters**

| Parameter           | Description                                                  |
|---------------------|--------------------------------------------------------------|
| languages           | *Optional* The listing languages filter as comma separated string [limit of up to 200 Languages].<br><br>If absent, the first 200 available listing languages metadata is retrieved. [ e.g., “en-us, en-gb"]. |
| includelanguagelist | *Optional* Boolean – if true, returns the list of added listing languages and their completeness status. |

**Required Headers**

| Header                          | Value                                                      |
|---------------------------------|------------------------------------------------------------|
| `Authorization: Bearer <Token>` | The Azure AD app ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account                        |

**Response Headers**

| Header             | Value                                                                                                   |
|--------------------|---------------------------------------------------------------------------------------------------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support Team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting.      |

**Response Parameters**

| Name                     | Type             | Description |
|--------------------------|------------------|-------------|
| accessibilitySupport     | Boolean          |             |
| additionalLicenseTerms   | String           |             |
| availability             | Object           | Availability module data |
| category                 | String           | See list of categories below |
| certificationNotes       | String           |             |
| code                     | String           | The error code of the Message |
| contactInfo              | String           |             |
| copyright                | String           |             |
| dependsOnDriversOrNT     | Boolean          |             |
| description              | String           |             |
| developedBy              | String           |             |
| discoverability          | String           | [DISCOVERABLE, DEEPLINK_ONLY] |
| enableInFutureMarkets    | Boolean          |             |
| errors                   | Array of objects | The list of error or warning messages if any |
| freeTrial                | String           | [NO_FREE_TRIAL, FREE_TRIAL] |
| hardwareItemType         | String           |             |
| isPrivacyPolicyRequired  | Boolean          |             |
| isRecommended            | Boolean          |             |
| isRequired               | Boolean          |             |
| isSuccess                | Boolean          |             |
| isSystemFeatureRequired  | Array of objects |             |
| language                 | String           | See list of languages below |
| listings                 | Array of objects | Listings module data for each language |
| markets                  | Array of strings | See list of markets below |
| message                  | String           | The description of the error |
| minimumHardware          | String           |             |
| minimumRequirement       | String           |             |
| penAndInkSupport         | Boolean          |             |
| pricing                  | String           | [FREE, FREEMIUM, SUBSCRIPTION, PAID] |
| privacyPolicyUrl         | String           |             |
| productDeclarations      | Object           |             |
| productFeatures          | Array of strings |             |
| properties               | Object           | Properties module data |
| recommendedHardware      | String           |             |
| recommendedRequirement   | String           |             |
| responseData             | Object           | Contains actual response Payload for the Request |
| requirements             | Array of objects |             |
| searchTerms              | Array of strings |             |
| shortDescription         | String           |             |
| subcategory              | String           | See list of sub-categories below |
| supportContactInfo       | String           |             |
| systemRequirementDetails | Array of objects |             |
| target                   | String           | The entity from which the Error originated |
| website                  | String           |             |
| whatsNew                 | String           |             |

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "availability":{
            "markets": ["US"],
            "discoverability": "DISCOVERABLE",
            "enableInFutureMarkets": true,
            "pricing": "PAID",
            "freeTrial": "NO_FREE_TRIAL"
        },
        "properties":{
            "isPrivacyPolicyRequired": true,
            "privacyPolicyUrl": "http://contoso.com",
            "website": "http://contoso.com",
            "supportContactInfo": "http://contoso.com",
            "certificationNotes": "Certification Notes",
            "category": "DeveloperTools",
            "subcategory": "Database",
            "productDeclarations": {
                "dependsOnDriversOrNT": false,
                "accessibilitySupport": false,
                "penAndInkSupport": false
            },
            "isSystemFeatureRequired": [
                {
                    "isRequired": true,
                    "isRecommended": false,
                    "hardwareItemType": "Touch"
                },
                {
                    "isRequired": true,
                    "isRecommended": false,
                    "hardwareItemType": "Keyboard"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "Mouse"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "Camera"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "NFC_HCE"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "NFC_Proximity"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "Bluetooth_LE"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "Telephony"
                },
                {
                    "isRequired": false,
                    "isRecommended": false,
                    "hardwareItemType": "Microphone"
                }
            ],
            "systemRequirementDetails": [
                {
                    "minimumRequirement": "1GB",
                    "recommendedRequirement": "4GB",
                    "hardwareItemType": "Memory"
                },
                {
                    "minimumRequirement": "",
                    "recommendedRequirement": "",
                    "hardwareItemType": "DirectX"
                },
                {
                    "minimumRequirement": "",
                    "recommendedRequirement": "",
                    "hardwareItemType": "Video_Memory"
                },
                {
                    "minimumRequirement": "",
                    "recommendedRequirement": "",
                    "hardwareItemType": "Processor"
                },
                {
                    "minimumRequirement": "",
                    "recommendedRequirement": "",
                    "hardwareItemType": "Graphics"
                }
            ]
        },
        "listings":[{
            "language": "en-us",
            "description": "Description",
            "whatsNew": "What's New",
            "productFeatures": ["Feature 1"],
            "shortDescription": "Short Description",
            "searchTerms": ["Search Ter 1"],
            "additionalLicenseTerms": "License Terms",
            "copyright": "Copyright Information",
            "developedBy": "Developer Details",
            "sortTitle": "Product 101",
            "requirements": [
                {
                    "minimumHardware": "Pentium4",
                    "recommendedHardware": "Corei9"
                }
            ],
            "contactInfo": "contactus@contoso.com"               
        }],      
        "listingLanguages": [{"language":"en-us", "isComplete": true}]
    }
}
```

### Update Current Draft Submission Metadata API

Updates metadata in each Module under draft submission. The API checks

- For Active Submission. If Exists, Fail with Error Message.
- If all modules are in ready status to allow Save Draft operation.
- Each field in the submission is validated as per requirements of the Store
- System Requirement Details validation rules:
  - Allowed Values in hardwareItemType = Memory:  300MB, 750MB, 1GB, 2GB, 4GB, 6GB, 8GB, 12GB, 16GB, 20GB
  - Allowed Values in hardwareItemType = DirectX:  DX9, DX10, DX11, DX12-FEATURELEVEL11, DX12-FEATURELEVEL12
  - Allowed Values in hardwareItemType = Video_Memory:  1GB, 2GB, 4GB, 6GB

**Path [Full Module Update]**: /submission/v1/product/{productId}/metadata<br>
**Method**: PUT

**Path [Module Patch Update]**: /submission/v1/product/{productId}/metadata<br>
**Method**: PATCH

**API Behavior**

In the case of Full Module Update API – entire Module Data needs to be present in Request for full update of every field. Any field which is not present in Request, its default value is used to overwrite current value for that specific Module. <br />
In the case of Patch Module Update API – only fields which are to be updated need to be present in Request. These field values from Request will overwrite their existing values, keeping all other fields which are not present in the Request, same as current for that specific Module. <br />

**Path Parameters**

| Parameter  | Description                          |
|------------|--------------------------------------|
| productId  | The Partner Center ID of the product |

**Required Headers**

| Header                          | Value                                                      |
|---------------------------------|------------------------------------------------------------|
| `Authorization: Bearer <Token>` | The Azure AD app ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account                        |

**Request Parameters**

| Name                     | Type             | Description |
|--------------------------|------------------|-------------|
| availability             | Object           | Object to hold Availability Module metadata |
| markets                  | Array of strings | *Required* See list of markets below |
| discoverability          | String           | *Required* [DISCOVERABLE, DEEPLINK_ONLY] |
| enableInFutureMarkets    | Boolean          | *Required*  |
| pricing                  | String           | *Required* [FREE, FREEMIUM, SUBSCRIPTION, PAID] |
| freeTrial                | String           | *Required if Pricing is PAID or SUBSCRIPTION* [NO_FREE_TRIAL, FREE_TRIAL] |
| properties               | Object           | Object to hold Properties Module metadata |
| isPrivacyPolicyRequired  | Boolean          | *Required*  | 
| privacyPolicyUrl         | String           | *Required if isPrivacyPolicyRequired = true* Must be a valid URL |
| website                  | String           | Must be a valid URL |
| supportContactInfo       | String           | Must be a valid URL or Email address |
| certificationNotes       | String           | *Recommended* Character limit = 2000 |
| category                 | String           | *Required* See list of categories below |
| subcategory              | String           | *Required* See list of sub-categories below |
| productDeclarations      | Object           | *Required*  |
| isSystemFeatureRequired  | Array of objects | [Touch, Keyboard, Mouse, Camera, NFC_HCE, NFC_Proximity, Bluetooth_LE, Telephony, Microphone] |
| isRequired               | Boolean          | *Required*  |
| isRecommended            | Boolean          | *Required*  |
| hardwareItemType         | String           | *Required*  |
| systemRequirementDetails | Array of objects | [Processor, Graphics, Memory, DirectX, Video_Memory]
| minimumRequirement       | String           | *Required* For systemRequirementsText, MaxLength = 200<br><br>Allowed Values in hardwareItemType = Memory: [300MB, 750MB, 1GB, 2GB, 4GB, 6GB, 8GB, 12GB, 16GB, 20GB]<br><br>Allowed Values in hardwareItemType = DirectX: [DX9, DX10, DX11, DX12-FEATURELEVEL11, DX12-FEATURELEVEL12]<br><br>Allowed Values in hardwareItemType = Video_Memory: [1GB, 2GB, 4GB, 6GB]
| recommendedRequirement   | String           | *Required* For systemRequirementsText, MaxLength = 200<br><br>Allowed Values in hardwareItemType = Memory: [300MB, 750MB, 1GB, 2GB, 4GB, 6GB, 8GB, 12GB, 16GB, 20GB]<br><br>Allowed Values in hardwareItemType = DirectX: [DX9, DX10, DX11, DX12-FEATURELEVEL11, DX12-FEATURELEVEL12]<br><br>Allowed Values in hardwareItemType = Video_Memory: [1GB, 2GB, 4GB, 6GB]
| dependsOnDriversOrNT     | Boolean          | *Required*  |
| accessibilitySupport     | Boolean          | *Required*  |
| penAndInkSupport         | Boolean          | *Required*  |
| listings                 | Object           | Object to listing module data for a single language |
| language                 | String           | *Required* See list of languages below |
| description              | String           | *Required* Character limit = 10000 |
| whatsNew                 | String           | Character limit = 1500 |
| productFeatures          | Array of String  | 200 characters per feature; Up to 20 features |
| shortDescription         | String           | Character limit = 1000 |
| searchTerms              | Array of String  | 30 characters per search term; Up to 7 search terms<br><br>21 unique words TOTAL across all search terms |
| additionalLicenseTerms   | String           | *Required* Character limit = 10000 |
| copyright                | String           | Character limit = 200 |
| developedBy              | String           | Character limit = 255 |
| requirements             | Array of objects | 200 characters per item; Up to 11 items TOTAL between minimum and recommended] | 
| minimumHardware          | String           | Character limit = 200 |
| recommendedHardware      | String           | Character limit = 200 |
| contactInfo              | String           | Character limit = 200 |
| listingsToAdd            | Array of strings | See list of languages below |
| listingsToRemove         | Array of strings | See list of languages below |

**Markets**

| Market | Abbreviation |
|--------|--------------|
| Afghanistan | AF |
| Albania | AL |
| Algeria | DZ |
| American Samoa | AS |
| Andorra | AD |
| Angola | AO |
| Anguilla | AI |
| Antarctica | AQ |
| Antigua and Barbuda | AG |
| Argentina | AR |
| Armenia | AM |
| Aruba | AW |
| Australia | AU |
| Austria | AT |
| Azerbaijan | AZ |
| Bahamas | BS |
| Bahrain | BH |
| Bangladesh | BD |
| Barbados | BB |
| Belarus | BY |
| Belgium | BE |
| Belize | BZ |
| Benin | BJ |
| Bermuda | BM |
| Bhutan | BT |
| Bolivarian Republic of Venezuela | VE |
| Bolivia | BO |
| Bonaire | BQ |
| Bosnia and Herzegovina | BA |
| Botswana | BW |
| Bouvet Island | BV |
| Brazil | BR |
| British Indian Ocean Territory | IO |
| British Virgin Islands | VG |
| Brunei | BN |
| Bulgaria | BG |
| Burkina Faso | BF |
| Burundi | BI |
| Cambodia | KH |
| Cameroon | CM |
| Canada | CA |
| Cabo Verde | CV |
| Cayman Islands | KY |
| Central African Republic | CF |
| Chad | TD |
| Chile | CL |
| China | CN |
| Christmas Island | CX |
| Cocos (Keeling) Islands | CC |
| Colombia | CO |
| Comoros | KM |
| Congo | CG |
| Congo (DRC) | CD |
| Cook Islands | CK |
| Costa Rica | CR |
| Croatia | HR |
| Curaçao | CW |
| Cyprus | CY |
| Czech Republic | CZ |
| Côte d'Ivoire | CI |
| Denmark | DK |
| Djibouti | DJ |
| Dominica | DM |
| Dominican Republic | DO |
| Ecuador | EC |
| Egypt | EG |
| El Salvador | SV |
| Equatorial Guinea | GQ |
| Eritrea | ER |
| Estonia | EE |
| Ethiopia | ET |
| Falkland Islands | FK |
| Faroe Islands | FO |
| Fiji | FJ |
| Finland | FI |
| France | FR |
| French Guiana | GF |
| French Polynesia | PF |
| French Southern and Antarctic Lands | TF |
| Gabon | GA |
| Gambia | GM |
| Georgia | GE |
| Germany | DE |
| Ghana | GH |
| Gibraltar | GI |
| Greece | GR |
| Greenland | GL |
| Grenada | GD |
| Guadeloupe | GP |
| Guam | GU |
| Guatemala | GT |
| Guernsey | GG |
| Guinea | GN |
| Guinea-Bissau | GW |
| Guyana | GY |
| Haiti | HT |
| Heard Island and McDonald Islands | HM |
| Vatican City | VA |
| Honduras | HN |
| Hong Kong SAR | HK |
| Hungary | HU |
| Iceland | IS |
| India | IN |
| Indonesia | ID |
| Iraq | IQ |
| Ireland | IE |
| Israel | IL |
| Italy | IT |
| Jamaica | JM |
| Japan | JP |
| Jersey | JE |
| Jordan | JO |
| Kazakhstan | KZ |
| Kenya | KE |
| Kiribati | KI |
| Korea | KR |
| Kuwait | KW |
| Kyrgyzstan | KG |
| Laos | LA |
| Latvia | LV |
| Lebanon | LB |
| Lesotho | LS |
| Liberia | LR |
| Libya | LY |
| Liechtenstein | LI |
| Lithuania | LT |
| Luxembourg | LU |
| Macao SAR | MO |
| North Macedonia | MK |
| Madagascar | MG |
| Malawi | MW |
| Malaysia | MY |
| Maldives | MV |
| Mali | ML |
| Malta | MT |
| Isle of Man | IM |
| Marshall Islands | MH |
| Martinique | MQ |
| Mauritania | MR |
| Mauritius | MU |
| Mayotte | YT |
| Mexico | MX |
| Micronesia | FM |
| Moldova | MD |
| Monaco | MC |
| Mongolia | MN |
| Montenegr  - ME |
| Montserrat | MS |
| Morocco | MA |
| Mozambique | MZ |
| Myanmar | MM |
| Namibia | NA |
| Nauru | NR |
| Nepal | NP |
| Netherlands | NL |
| New Caledonia | NC |
| New Zealand | NZ |
| Nicaragua | NI |
| Niger | NE |
| Nigeria | NG |
| Niue | NU |
| Norfolk Island | NF |
| Northern Mariana Islands | MP |
| Norway | NO |
| Oman | OM |
| Pakistan | PK |
| Palau | PW |
| Palestinian Authority | PS |
| Panama | PA |
| Papua New Guinea | PG |
| Paraguay | PY |
| Peru | PE |
| Philippines | PH |
| Pitcairn Islands | PN |
| Poland | PL |
| Portugal | PT |
| Qatar | QA |
| Reunion | RE |
| Romania | RO |
| Russia | RU |
| Rwanda | RW |
| Saint Barthélemy | BL |
| Saint Helena, Ascension and Tristan da Cunha | SH |
| Saint Kitts and Nevis | KN |
| Saint Lucia | LC |
| Saint Martin (French Part) | MF |
| Saint Pierre and Miquelon | PM |
| Saint Vincent and the Grenadines | VC |
| Samoa | WS |
| San Marino | SM |
| Saudi Arabia | SA |
| Senegal | SN |
| Serbia | RS |
| Seychelles | SC |
| Sierra Leone | SL |
| Singapore | SG |
| Sint Maarten (Dutch Part) | SX |
| Slovakia | SK |
| Slovenia | SI |
| Solomon Islands | SB |
| Somalia | SO |
| South Africa | ZA |
| South Georgia and the South Sandwich Islands | GS |
| Spain | ES |
| Sri Lanka | LK |
| Suriname | SR |
| Svalbard and Jan Mayen | SJ |
| Swaziland | SZ |
| Sweden | SE |
| Switzerland | CH |
| São Tomé and Príncipe | ST |
| Taiwan | TW |
| Tajikistan | TJ |
| Tanzania | TZ |
| Thailand | TH |
| Timor-Leste | TL |
| Tog  - TG |
| Tokelau | TK |
| Tonga | TO |
| Trinidad and Tobag  - TT |
| Tunisia | TN |
| Türkiye | TR |
| Turkmenistan | TM |
| Turks and Caicos Islands | TC |
| Tuvalu | TV |
| U.S. Minor Outlying Islands | UM |
| U.S. Virgin Islands | VI |
| Uganda | UG |
| Ukraine | UA |
| United Arab Emirates | AE |
| United Kingdom | GB |
| United States | US |
| Uruguay | UY |
| Uzbekistan | UZ |
| Vanuatu | VU |
| Vietnam | VN |
| Wallis and Futuna | WF |
| Yemen | YE |
| Zambia | ZM |
| Zimbabwe | ZW |
| Åland Islands | AX |

**Categories and Sub-categories**

| Category              | Subcategories |
|-----------------------|---------------|
| BooksAndReference     | EReader, Fiction, Nonfiction, Reference |
| Business              | AccountingAndfinance, Collaboration, CRM, DataAndAnalytics, FileManagement, InventoryAndlogistics, LegalAndHR, ProjectManagement, RemoteDesktop, SalesAndMarketing, TimeAndExpenses |
| DeveloperTools        | Database, DesignTools, DevelopmentKits, Networking, ReferenceAndTraining, Servers, Utilities, WebHosting
| Education             | EducationBooksAndReference, EarlyLearning, InstructionalTools, Language, StudyAids |
| Entertainment         | (None)        |
| FoodAndDining         | (None)        |
| GovernmentAndPolitics | (None)        |
| HealthAndFitness      | (None)        |
| KidsAndFamily         | KidsAndFamilyBooksAndReference, KidsAndFamilyEntertainment, HobbiesAndToys, SportsAndActivities, KidsAndFamilyTravel |
| Lifestyle             | Automotive, DYI, HomeAndGarden, Relationships, SpecialInterest, StyleAndFashion |
| Medical               | (None)        |
| MultimediaDesign      | IllustrationAndGraphicDesign, MusicProduction, PhotoAndVideoProduction |
| Music                 | (None)        |
| NavigationAndMaps     | (None)        |
| NewsAndWeather        | News, Weather |
| PersonalFinance       | BankingAndInvestments, BudgetingAndTaxes |
| Personalization       | RingtonesAndSounds, themes, WallpaperAndLockScreens |
| PhotoAndVideo         | (None)        |
| Productivity          | (None)        |
| Security              | PCProtection, PersonalSecurity |
| Shopping              | (None)        |
| Social                | (None)        |
| Sports                | (None)        |
| Travel                | CityGuides, Hotels |
| UtilitiesAndTools     | BackupAndManage, FileManager |

**Languages**

| Language name | Supported language codes |
|---------------|--------------------------|
| Afrikaans | af, af-za |
| Albanian | sq, sq-al |
| Amharic | am, am-et |
| Armenian | hy, hy-am |
| Assamese | as, as-in |
| Azerbaijani | az-arab, az-arab-az, az-cyrl, az-cyrl-az, az-latn, az-latn-az |
| Basque (Basque) | eu, eu-es |
| Belarusian | be, be-by |
| Bangla | bn, bn-bd, bn-in |
| Bosnian | bs, bs-cyrl, bs-cyrl-ba, bs-latn, bs-latn-ba |
| Bulgarian | bg, bg-bg |
| Catalan | ca, ca-es, ca-es-valencia |
| Cherokee | chr-cher, chr-cher-us, chr-latn |
| Chinese (Simplified) | zh-Hans, zh-cn, zh-hans-cn, zh-sg, zh-hans-sg |
| Chinese (Traditional) | zh-Hant, zh-hk, zh-mo, zh-tw, zh-hant-hk, zh-hant-mo, zh-hant-tw, zh-mo, zh-tw, zh-hant-hk, zh-hant-mo, zh-hant-tw |
| Croatian | hr, hr-hr, hr-ba |
| Czech | cs, cs-cz |
| Danish | da, da-dk |
| Dari | prs, prs-af, prs-arab |
| Dutch | nl, nl-nl, nl-be |
| English | en, en-au, en-ca, en-gb, en-ie, en-in, en-nz, en-sg, en-us, en-za, en-bz, en-hk, en-id, en-jm, en-kz, en-mt, en-my, en-ph, en-pk, en-tt, en-vn, en-zw |
| Estonian | et, et-ee |
| Filipin  - fil, fil-latn, fil-ph |
| Finnish | fi, fi-fi |
| French | fr, fr-be , fr-ca , fr-ch , fr-fr , fr-lu, fr-cd, fr-ci, fr-cm, fr-ht, fr-ma, fr-mc, fr-ml, fr-re, frc-latn, frp-latn |
| Galician | gl, gl-es |
| Georgian | ka, ka-ge |
| German | de, de-at, de-ch, de-de, de-lu, de-li |
| Greek | el, el-gr |
| Gujarati | gu, gu-in |
| Hausa | ha, ha-latn, ha-latn-ng |
| Hebrew | he, he-il |
| Hindi | hi, hi-in |
| Hungarian | hu, hu-hu |
| Icelandic | is, is-is |
| Igb  - ig-latn, ig-ng |
| Indonesian | id, id-id |
| Inuktitut (Latin) | iu-cans, iu-latn, iu-latn-ca |
| Irish | ga, ga-ie |
| isiXhosa | xh, xh-za |
| isiZulu | zu, zu-za |
| Italian | it, it-it, it-ch |
| Japanese | ja , ja-jp |
| Kannada | kn, kn-in |
| Kazakh | kk, kk-kz |
| Khmer | km, km-kh |
| K'iche' | quc-latn, qut-gt, qut-latn |
| Kinyarwanda | rw, rw-rw |
| KiSwahili | sw, sw-ke |
| Konkani | kok, kok-in |
| Korean | ko, ko-kr |
| Kurdish | ku-arab, ku-arab-iq |
| Kyrgyz | ky-kg, ky-cyrl |
| Lao | lo, lo-la |
| Latvian | lv, lv-lv |
| Lithuanian | lt, lt-lt |
| Luxembourgish | lb, lb-lu |
| Macedonian | mk, mk-mk |
| Malay | ms, ms-bn, ms-my |
| Malayalam | ml, ml-in |
| Maltese | mt, mt-mt |
| Maori | mi, mi-latn, mi-nz |
| Marathi | mr, mr-in |
| Mongolian (Cyrillic) | mn-cyrl, mn-mong, mn-mn, mn-phag |
| Nepali | ne, ne-np |
| Norwegian | nb, nb-no, nn, nn-no, no, no-no |
| Odia | or, or-in |
| Persian | fa, fa-ir |
| Polish | pl, pl-pl |
| Portuguese (Brazil) | pt-br |
| Portuguese (Portugal) | pt, pt-pt |
| Punjabi | pa, pa-arab, pa-arab-pk, pa-deva, pa-in |
| Quechua | quz, quz-bo, quz-ec, quz-pe |
| Romanian | ro, ro-ro |
| Russian | ru , ru-ru |
| Scottish Gaelic | gd-gb, gd-latn |
| Serbian (Latin) | sr-Latn, sr-latn-cs, sr, sr-latn-ba, sr-latn-me, sr-latn-rs |
| Serbian (Cyrillic) | sr-cyrl, sr-cyrl-ba, sr-cyrl-cs, sr-cyrl-me, sr-cyrl-rs |
| Sesotho sa Leboa | nso, nso-za |
| Setswana | tn, tn-bw, tn-za |
| Sindhi | sd-arab, sd-arab-pk, sd-deva |
| Sinhala | si, si-lk |
| Slovak | sk, sk-sk |
| Slovenian | sl, sl-si |
| Spanish | es, es-cl, es-co, es-es, es-mx, es-ar, es-bo, es-cr, es-do, es-ec, es-gt, es-hn, es-ni, es-pa, es-pe, es-pr, es-py, es-sv, es-us, es-uy, es-ve |
| Swedish | sv, sv-se, sv-fi |
| Tajik (Cyrillic) | tg-arab, tg-cyrl, tg-cyrl-tj, tg-latn |
| Tamil | ta, ta-in |
| Tatar | tt-arab, tt-cyrl, tt-latn, tt-ru |
| Telugu | te, te-in |
| Thai | th, th-th |
| Tigrinya | ti, ti-et |
| Turkish | tr, tr-tr |
| Turkmen | tk-cyrl, tk-latn, tk-tm, tk-latn-tr, tk-cyrl-tr |
| Ukrainian | uk, uk-ua |
| Urdu | ur, ur-pk |
| Uyghur | ug-arab, ug-cn, ug-cyrl, ug-latn |
| Uzbek (Latin) | uz, uz-cyrl, uz-latn, uz-latn-uz |
| Vietnamese | vi, vi-vn |
| Welsh | cy, cy-gb |
| Wolof | wo, wo-sn |
| Yoruba | yo-latn, yo-ng |

**Sample Request**

```json
{
    "availability":{
        "markets": ["US"],
        "discoverability": "DISCOVERABLE",
        "enableInFutureMarkets": true,
        "pricing": "PAID",
        "freeTrial": "NO_FREE_TRIAL"
    },
    "properties":{
        "isPrivacyPolicyRequired": true,
        "privacyPolicyUrl": "http://contoso.com",
        "website": "http://contoso.com",
        "supportContactInfo": "http://contoso.com",
        "certificationNotes": "Certification Notes",
        "category": "DeveloperTools",
        "subcategory": "Database",
        "productDeclarations": {
            "dependsOnDriversOrNT": false,
            "accessibilitySupport": false,
            "penAndInkSupport": false
        },
        "isSystemFeatureRequired": [
        {
            "isRequired": true,
                "isRecommended": false,
                "hardwareItemType": "Touch"
            },
            {
                "isRequired": true,
                "isRecommended": false,
                "hardwareItemType": "Keyboard"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "Mouse"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "Camera"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "NFC_HCE"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "NFC_Proximity"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "Bluetooth_LE"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "Telephony"
            },
            {
                "isRequired": false,
                "isRecommended": false,
                "hardwareItemType": "Microphone"
            }
        ],
        "systemRequirementDetails": [
            {
                "minimumRequirement": "1GB",
                "recommendedRequirement": "4GB",
                "hardwareItemType": "Memory"
            },
            {
                "minimumRequirement": "",
                "recommendedRequirement": "",
                "hardwareItemType": "DirectX"
            },
            {
                "minimumRequirement": "",
                "recommendedRequirement": "",
                "hardwareItemType": "Video_Memory"
            },
            {
                "minimumRequirement": "",
                "recommendedRequirement": "",
                "hardwareItemType": "Processor"
            },
            {
                "minimumRequirement": "",
                "recommendedRequirement": "",
                "hardwareItemType": "Graphics"
            }
        ]
    },
    "listings":{
        "language": "en-us",
        "description": "Description",
        "whatsNew": "What's New",
        "productFeatures": ["Feature 1"],
        "shortDescription": "Short Description",
        "searchTerms": ["Search Ter 1"],
        "additionalLicenseTerms": "License Terms",
        "copyright": "Copyright Information",
        "developedBy": "Developer Details",
        "sortTitle": "Product 101",
        "requirements": [
            {
                "minimumHardware": "Pentium4",
                "recommendedHardware": "Corei9"
            }
        ],
        "contactInfo": "contactus@contoso.com"               
    },      
    "listingsToAdd": ["en-au"],
    "listingsToRemove": ["en-gb"]
}
```

**Response Headers**

| Header             | Value                                                      |
|--------------------|------------------------------------------------------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support Team for analyzing any issue.
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | The list of error or warning messages if any
| code                | String           | The error code of the Message
| message             | String           | The description of the error
| target              | String           | The entity from which the Error originated
| responseData        | Object           | Contains actual response Payload for the Request
| pollingUrl          | String           | Polling URL to get status of any In-Progress Submission |
| ongoingSubmissionId | String           | Submission Id of any Already In-Progress Submission |

Sample Response

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "pollingUrl": "/submission/v1/product/{productId}/submission/{submissionId}/status",
        "ongoingSubmissionId": ""
    } 
}
```

### Get Current Draft Packages API

Fetches package details under current draft submission.

**Path [All Packages]**: /submission/v1/product/{productId}/packages<br>
**Method**: GET

**Path [Single Package]**: /submission/v1/product/{productId}/packages/{packageId}<br>
**Method**: GET

**Path Parameters**

| Name      | Description                           |
|-----------|---------------------------------------|
| productId | The Partner Center ID of the product  |
| packageId | The unique ID of the package to fetch |

**Required Headers**

| Header             | Value                                                                         |
|--------------------|-------------------------------------------------------------------------------|
| `Authorization: Bearer <Token>` | Using the Azure AD app ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account                              |

**Response Headers**

| Header             | Value                                                      |
|--------------------|------------------------------------------------------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                 | Type             | Description |
|----------------------|------------------|-------------|
| isSuccess            | Boolean          |             |
| errors               | Array of objects | The list of errors or warning messages if any |
| code                 | String           | The error code of the Message |
| message              | String           | The description of the error |
| target               | String           | The entity from which the error originated |
| responseData         | Object           |             |
| packages             | Array of objects | Object to hold package module data |
| packageId            | String           |             |
| packageUrl           | String           |             |
| languages            | Array of strings |             |
| architectures        | Array of strings | [Neutral, X86, X64, Arm, Arm64] |
| isSilentInstall      | Boolean          | This should be marked as true if your installer runs in silent mode without requiring switches or else false |
| installerParameters  | String           |             |
| genericDocUrl        | String           |             |
| errorDetails         | Array of objects |             |
| errorScenario        | String           |             |
| errorScenarioDetails | Array of objects |             |
| errorValue           | String           |             |
| errorUrl             | String           |             |
| packageType          | String           |             |

**Sample Response**

```json
{   
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
    }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData":{
        "packages":[{
            "packageId": "pack0832",
            "packageUrl": "https://www.contoso.com/downloads/1.1/setup.exe",
            "languages": ["en-us"],
            "architectures": ["X86"],
            "isSilentInstall": true,
            "installerParameters": "/s",
            "genericDocUrl": "https://docs.contoso.com/doclink",
            "errorDetails": [{
                "errorScenario": "rebootRequired",
                "errorScenarioDetails": [{
                    "errorValue": "ERR001001",
                    "errorUrl": "https://errors.contoso.com/errors/ERR001001"
                }]
            }],
            "packageType": "exe",
        }]
    }
}
```

### Update Current Draft Packages API

Updates package details under current draft submission.

**Path [Full Module Update]**: /submission/v1/product/{productId}/packages<br>
**Method**: PUT

**Path [Single Package Patch Update]**: /submission/v1/product/{productId}/packages/{packageId}<br>
**Method**: PATCH

**API Behavior**

In the case of Full Module Update API – entire packages data needs to be present in request for full update of every field. Any field which is not present in request, its default value is used to overwrite current value for that specific module. This results in overwriting of all existing packages with a new set of packages from request. This will result in regeneration of Package Ids and user should call GET Packages API for latest Package Ids.

In the case of Single Package Patch Update API – only fields which are to be updated for a given package need to be present in request. These field values from request will overwrite their existing values, keeping all other fields which are not present in the request, same as current for that specific package. Other packages in the set remain as is.

**Path Parameters**

| Name      | Description                           |
|-----------|---------------------------------------|
| productId | The Partner Center ID of the product  |
| packageId | The unique ID of the package          |

**Required Headers**

| Header                          | Value                                                            |
|---------------------------------|------------------------------------------------------------------|
| `Authorization: Bearer <Token>` | Using the Azure AD app ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account                              |

**Request Parameters**

| Name                 | Type             | Description |
|----------------------|------------------|-------------|
| packages             | Array of Objects | Object to hold Package Module data [Only Required for Full Module Update] |
| packageUrl           | String           | *Required*  |
| languages            | Array of strings | *Required*  |
| architectures        | Array of strings | *Required* Should contain a single architecture - Neutral, X86, X64, Arm, Arm64 |
| isSilentInstall      | Boolean          | *Required*  This should be marked as true if your installer runs in silent mode without requiring switches or else false|
| installerParameters  | String           | *Required if isSilentInstall is false* |
| genericDocUrl        | String           | *Required if packageType is exe* Link to document containing details of custom error codes for the EXE type installer |
| errorDetails         | Array of Objects | Metadata to hold custom error codes and details for EXE type Installers. |
| errorScenario        | String           | Identify the specific error scenario. [installationCancelledByUser, applicationAlreadyExists, installationAlreadyInProgress, diskSpaceIsFull, rebootRequired, networkFailure, packageRejectedDuringInstallation, installationSuccessful, miscellaneous] |
| errorScenarioDetails | Array of Objects |             |
| errorValue           | String           | Error code which can be present during Installation |
| errorUrl             | String           | URL to have details on the error |
| packageType          | String           | *Required* [exe, msi] |

**Sample Request [Full Module Update]**

```json
{
    "packages":[{
        "packageUrl": "https://www.contoso.com/downloads/1.1/setup.exe",
        "languages": ["en-us"],
        "architectures": ["X86"],
        "isSilentInstall": true,
        "installerParameters": "/s",
        "genericDocUrl": "https://docs.contoso.com/doclink",
        "errorDetails": [{
            "errorScenario": "rebootRequired",
            "errorScenarioDetails": [{
                "errorValue": "ERR001001",
                "errorUrl": "https://errors.contoso.com/errors/ERR001001"
            }]
        }],
        "packageType": "exe",
    }]
}
```

**Sample Request [Single Package Patch Update]**

```json
{
    "packageUrl": "https://www.contoso.com/downloads/1.1/setup.exe",
    "languages": ["en-us"],
    "architectures": ["X86"],
    "isSilentInstall": true,
    "installerParameters": "/s",
    "genericDocUrl": "https://docs.contoso.com/doclink",
    "errorDetails": [{
        "errorScenario": "rebootRequired",
        "errorScenarioDetails": [{
            "errorValue": "ERR001001",
            "errorUrl": "https://errors.contoso.com/errors/ERR001001"
        }]
    }],
    "packageType": "exe",
}
```

**Response Headers**

| Header             | Value          |
|--------------------|----------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | [The list of error or warning messages if any]
| code                | String           | The Error Code of the Message
| message             | String           | The Description of the Error
| target              | String           | The Entity from which the Error originated
| responseData        | Object           |             |
| pollingUrl          | String           | [Polling URL to get Submission Status in case of any Already In-Progress Submission]
| ongoingSubmissionId | String           | [Submission ID of any Already In-Progress Submission]

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "pollingUrl": "/submission/v1/product/{productId}/submission/{submissionId}/status",
        "ongoingSubmissionId": ""
    } 
}
```

### Commit Packages API

Commits the new set of Packages updated using Package Update APIs under current draft submission. This API returns a Polling URL to track the Package Upload.

**Path**: /submission/v1/product/{productId}/packages/commit<br>
**Method**: POST

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Required Headers**

| Header                          | Value                                                            |
|---------------------------------|------------------------------------------------------------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account                              |

**Response Headers**

| Header             | Value                                                            |
|--------------------|------------------------------------------------------------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue.
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting.

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | [The list of error or warning messages if any]
| code                | String           | The error code of the Message
| message             | String           | The description of the error
| target              | String           | The entity from which the error originated
| responseData        | Object           |             |
| pollingUrl          | String           | [Polling URL to get status of Package Upload or Submission Status in case of any Already In-Progress Submission]
| ongoingSubmissionId | String           | [Submission Id of any Already In-Progress Submission]

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "pollingUrl": "/submission/v1/product/{productId}/status",
        "ongoingSubmissionId": ""
    } 
}
```

### Get Current Draft Listing Assets API

Fetches listing asset details under current draft submission.

**Path**: /submission/v1/product/{productId}/listings/assets?languages={languages}<br>
**Method**: GET

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Query Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| languages | [Optional] The listing languages filter as comma separated string [limit of up to 200 languages]. If absent, the first 200 available listing language’s assets data are retrieved. (e.g., “en-us, en-gb") |

**Required Headers**

| Header                          | Value                                                            |
|---------------------------------|------------------------------------------------------------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account                              |

**Response Headers**

| Header                          | Value                                                            |
|---------------------------------|------------------------------------------------------------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue.
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting.

**Response Parameters**

| Name          | Type              | Description |
|---------------|-------------------|-------------|
| isSuccess     | Boolean           |             |
| errors        | Array of objects  | The list of error or warning messages if any |
| code          | String            | The error code of the Message |
| message       | String            | The description of the error |
| target        | String            | The entity from which the error originated |
| responseData  | Object            |             |
| listingAssets | Array of objects  | Listing asset details for each language |
| language      | String            |             |
| storeLogos    | Array of objects  |             |
| screenshots   | Array of objects  |             |
| id            | String            |             |
| assetUrl      | String            | Must be a valid URL |
| imageSize     | Object            |             |
| width         | Integer           |             |
| height        | Integer           |             |

**Sample Response**

```json
{   
"isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData":{
        "listingAssets": [{
            "language": "en-us",
            "storeLogos": [
                {
                    "id": "1234567890abcdefgh",
                    "assetUrl": "https://contoso.com/blob=1234567890abcdefgh",
                    "imageSize": {
                        "width": 2160,
                        "height": 2160
                    }
                }
            ],
            "screenshots": [
                {
                    "id": "1234567891abcdefgh",
                    "assetUrl": "https://contoso.com/blob=1234567891abcdefgh",
                    "imageSize": {
                        "width": 2160,
                        "height": 2160
                    }
                }
            ]
        }]
    }
}
```

### Create Listing Assets API

Creates new Listing Asset Upload under current draft submission.

#### Update of Listing assets

Microsoft Store Submission API for EXE or MSI app use runtime-generated SAS URLs to Blob Stores for each individual image asset uploads, along with a Commit API call after uploading is successful.
To have the ability to update listing assets, and in turn, to be able to add/remove locales in listing module, the following approach can be used:

1. Use the Create Listing Asset API to send request regarding asset upload along with language, type and count of assets.
2. Based on the number of assets requested, Asset IDs are created on demand and would create a short-term SAS URL and send it back in Response Body under type of assets. You can use this URL to upload Image assets of specific type using HTTP Clients [Put Blob (REST API) - Azure Storage | Microsoft Docs].
3. After uploading, you can use the Commit Listing Assets API to also send the new Asset ID information received earlier from previous API call. The single API will internally commit the listing assets data after validation. 
4. This approach will effectively overwrite the entire set of previous Images of the Asset Type under specific Language which is being sent in Request. Hence, previously uploaded Assets will be removed.

**Path**: /submission/v1/product/{productId}/listings/assets/create<br>
**Method**: POST

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Required Headers**

| Header                          | Description |
|---------------------------------|-------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account |

**Request Parameters**

| Name               | Type    | Description |
|--------------------|---------|-------------|
| language           | String  | *Required*  |
| createAssetRequest | Object  | *Required*  |
| Screenshot         | Integer | *Required if ISV needs to update screenshots or add new listing language* [1 - 10] |
| Logo               | Integer | *Required if ISV needs to update logos or add new listing language* [1 or 2] |

**Response Headers**

| Header             | Description |
|--------------------|-------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                    | Type             | Description |
|-------------------------|------------------|-------------|
| isSuccess               | Boolean          |             |
| errors                  | Array of objects | The list of error or warning messages if any |
| code                    | String           | The error code of the Message
| message                 | String           | The description of the error
| target                  | String           | The entity from which the error originated
| responseData            | Object           |             |
| listingAssets           | Object           | Object containing details of StoreLogos and Screenshots to be uploaded |
| language                | String           |             |
| storeLogos              | Array of objects |             |
| screenshots             | Array of objects |             |
| id                      | String           |             |
| primaryAssetUploadUrl   | String           | Primary URL to upload listing asset using Azure Blob REST API
| secondaryAssetUploadUrl | String           | Secondary URL to upload listing asset using Azure Blob REST API
| httpMethod              | HTTP Method      | The HTTP method needed to be used to upload Assets via the Asset Upload URLs – Primary or Secondary |
| httpHeaders             | Object           | An object with keys as Required headers to be present in the Upload API call to Asset Upload URLs. If the value is non-empty, the headers need to have specific values. Else, values are calculated during API call.

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "listingAssets": {
            "language": "en-us",
            "storeLogos":[{
                "id": "1234567890abcdefgh",
                "primaryAssetUploadUrl": "https://contoso.com/upload?blob=1234567890abcdefgh&sig=12345",
                "secondaryAssetUploadUrl": "https://contoso.com/upload?blob=0987654321abcdfger&sig=54326",
                "httpMethod": "PUT",
                "httpHeaders": {"Required Header Name": "Header Value"}
            }],
            "screenshots":[{
                "id": "0987654321abcdfger",
                "primaryAssetUploadUrl": "https://contoso.com/upload?blob=0987654321abcdfger&sig=54321",
                "secondaryAssetUploadUrl": "https://contoso.com/upload?blob=0987654321abcdfger&sig=54322",
                "httpMethod": "PUT",
                "httpHeaders": {"Required Header Name": "Header Value"}

            }]
        }
    } 
}
```

### Commit Listing Assets API

Commits the new Listing Asset Uploaded using the details from Create Assets API under current draft submission.

**Path**: /submission/v1/product/{productId}/listings/assets/commit<br>
**Method**: PUT

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Required Headers**

| Header                          | Description |
|---------------------------------|-------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account |

**Request Parameters**

| Name          | Type            | Description |
|---------------|-----------------|-------------|
| listingAssets | Object          |             |
| language      | String          |             |
| storeLogos    | Array of Object |             |
| screenshots   | Array of Object |             |
| id            | String          | Should be either an existing ID which user wants to persist from Get Current Listing Assets API or new ID under which a new Asset was uploaded in Create Listing Assets API.
| assetUrl      | String          | Should be either existing Asset’s URL which user wants to persist from Get Current Listing Assets API or the Upload URL – Primary or Secondary, using which a new Asset was Uploaded in Create Listing Assets API. Must be a valid URL

**Sample Request**
 
```json
{
    "listingAssets": { 
        "language": "en-us",    
        "storeLogos": [
            {
                "id": "1234567890abcdefgh",
                "assetUrl": "https://contoso.com/blob=1234567890abcdefgh",
            }
        ],
        "screenshots": [
            {
                "id": "1234567891abcdefgh",
                "assetUrl": "https://contoso.com/blob=1234567891abcdefgh",
            }
        ]
    }
}
```

**Response Headers**

| Header             | Description |
|--------------------|-------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | The list of error or warning messages if any
| code                | String           | The error code of the Message
| message             | String           | The description of the error
| target              | String           | The entity from which the error originated
| responseData        | Object           |             |
| pollingUrl          | String           | Polling URL to get status of any in-progress submission
| ongoingSubmissionId | String           | Submission ID of any already in-progress submission

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "pollingUrl": "/submission/v1/product/{productId}/submission/{submissionId}/status",
        "ongoingSubmissionId": ""
    } 
}
```

### Module Status Polling API

API to check the module readiness before submission can be created. Also validates the package upload status. 

**Path**: /submission/v1/product/{productId}/status<br>
**Method**: GET

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Required Headers**

| Header                          | Description |
|---------------------------------|-------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account |

**Response Headers**

| Header             | Description |
|--------------------|-------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | The list of error or warning messages if any
| code                | String           | The error code of the Message
| message             | String           | The description of the error
| target              | String           | The entity from which the error originated
| responseData        | Object           |             |
| isReady             | Boolean          | Indicates if all Modules are in ready state including package upload
| ongoingSubmissionId | String           | Submission ID of any already in-progress submission

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "isReady": true,
        "ongoingSubmissionId": ""
    }
}
```

### Create Submission API

Creates a submission from current draft for MSI or EXE app. The API checks:

- for active submission and fails with error message if an active submission exists.
- if all modules are in ready status to create submission.
- each field in the submission is validated as per requirements of the Store

**Path**:/submission/v1/product/{productId}/submit<br>
**Method**: POST

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Required Headers**

| Header                          | Description |
|---------------------------------|-------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account |

**Response Headers**

| Header             | Description |
|--------------------|-------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | The list of error or warning messages if any |
| code                | String           | The error code of the Message |
| message             | String           | The description of the error |
| target              | String           | The entity from which the error originated |
| responseData        | Object           |              |
| pollingUrl          | String           | Polling URL to get status of module readiness including package upload for submission |
| submissionId        | String           | The ID for the newly created Submission |
| ongoingSubmissionId | String           | Submission ID of any already in-progress submission |

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "submissionId": "1234567890", 
        "pollingUrl": "/submission/v1/product/{productId}/submission/{submissionId}/status",
        "ongoingSubmissionId": ""
    }
}
```

### Submission Status Polling API

API to check the Submission Status.

**Path**: /submission/v1/product/{productId}/submission/{submissionId}/status<br>
**Method**: GET

**Path Parameters**

| Name      | Description                          |
|-----------|--------------------------------------|
| productId | The Partner Center ID of the product |

**Required Headers**

| Header                          | Description |
|---------------------------------|-------------|
| `Authorization: Bearer <Token>` | Using the Azure AD App ID registered with Partner Center account |
| `X-Seller-Account-Id`           | Seller ID of Partner Center account |

**Response Headers**

| Header             | Description |
|--------------------|-------------|
| `X-Correlation-ID` | The GUID type unique ID for each request. This can be shared with Support team for analyzing any issue. |
| `Retry-After`      | The time in seconds which client needs to wait before calling the APIs again due to rate limiting. |

**Response Parameters**

| Name                | Type             | Description |
|---------------------|------------------|-------------|
| isSuccess           | Boolean          |             |
| errors              | Array of objects | The list of error or warning messages if any
| code                | String           | The error code of the Message
| message             | String           | The description of the error
| target              | String           | The entity from which the error originated
| responseData        | Object           |             |
| publishingStatus    | String           | Publishing Status of Submission - [INPROGRESS, PUBLISHED, FAILED, UNKNOWN]
| hasFailed           | Boolean          |Indicates if Publishing has Failed and won’t be retried

**Sample Response**

```json
{
    "isSuccess": true,
    "errors": [{
        "code": "badrequest",
        "message": "Error Message 1",
        "target": "listings"
        }, {
        "code": "warning",
        "message": "Warning Message 1",
        "target": "properties"
    }],
    "responseData": {
        "publishingStatus": "INPROGRESS",
        "hasFailed": false
    }
}
```

## Code examples

The following articles provide detailed code examples that demonstrate how to use the Microsoft Store submission API in different programming languages:

### C# sample: Microsoft Store Submission API for MSI or EXE app

This article provides C# code examples that demonstrate how to use the Microsoft Store submission API for MSI or EXE app. You can review each example to learn more about the task it demonstrates, or you can build all the code examples in this article into a console application. 

**Prerequisites**
These examples use the following library:
- Newtonsoft.Json NuGet package from Newtonsoft.

**Main program**
The following example implements a command line program that calls the other example methods in this article to demonstrate different ways to use the Microsoft Store submission API. To adapt this program for your own use:
- Assign the SellerId property to the Seller ID of your Partner Center account.
- Assign the ApplicationId property to the ID of the app you want to manage.
- Assign the ClientId and ClientSecret properties to the client ID and key for your app, and replace the tenantid string in the TokenEndpoint URL with the tenant ID for your app. For more information, see How to associate an Azure AD application with your Partner Center account

```csharp
using System;
using System.Threading.Tasks;

namespace Win32SubmissionApiCSharpSample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ClientConfiguration()
            {
                ApplicationId = "...",
                ClientId = "...",
                ClientSecret = "...",
                Scope = "https://api.store.microsoft.com/.default",
                ServiceUrl = "https://api.store.microsoft.com",
                TokenEndpoint = "...",
                SellerId = 0
            };

            await new AppSubmissionUpdateSample(config).RunAppSubmissionUpdateSample();

        }
    }
}
```

### ClientConfiguration helper class using C#

The sample app uses the ClientConfiguration helper class to pass Azure Active Directory data and app data to each of the example methods that use the Microsoft Store submission API.

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace Win32SubmissionApiCSharpSample
{
    public class ClientConfiguration
    {
        /// <summary>
        /// Client Id of your Azure Active Directory app.
        /// Example" ba3c223b-03ab-4a44-aa32-38aa10c27e32
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client secret of your Azure Active Directory app
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Service root endpoint.
        /// Example: "https://api.store.microsoft.com"
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Token endpoint to which the request is to be made. Specific to your Azure Active Directory app
        /// Example: https://login.microsoftonline.com/d454d300-128e-2d81-334a-27d9b2baf002/oauth2/v2.0/token
        /// </summary>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// Resource scope. If not provided (set to null), default one is used for the production API
        /// endpoint ("https://api.store.microsoft.com/.default")
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Partner Center Application ID.
        /// Example: 3e31a9f9-84e8-4d2d-9eba-487878d02ebf
        /// </summary>
        public string ApplicationId { get; set; }


        /// <summary>
        /// The Partner Center Seller Id
        /// Example: 123456892
        /// </summary>
        public int SellerId { get; set; }
    }
}
```

### Create an app submission using C#

The following example implements a class that uses several methods in the Microsoft Store submission API to update an app submission. 

```csharp
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Win32SubmissionApiCSharpSample
{
    public class AppSubmissionUpdateSample
    {
        private ClientConfiguration ClientConfig;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">An instance of ClientConfiguration that contains all parameters populated</param>
        public AppSubmissionUpdateSample(ClientConfiguration configuration)
        {
            this.ClientConfig = configuration;
        }

        /// <summary>
        /// Main method to Run the Sample Application
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task RunAppSubmissionUpdateSample()
        {
            // **********************
            //       SETTINGS
            // **********************
            var appId = this.ClientConfig.ApplicationId;
            var clientId = this.ClientConfig.ClientId;
            var clientSecret = this.ClientConfig.ClientSecret;
            var serviceEndpoint = this.ClientConfig.ServiceUrl;
            var tokenEndpoint = this.ClientConfig.TokenEndpoint;
            var scope = this.ClientConfig.Scope;

            // Get authorization token.
            Console.WriteLine("Getting authorization token");
            var accessToken = await SubmissionClient.GetClientCredentialAccessToken(
                tokenEndpoint,
                clientId,
                clientSecret,
                scope);

            var client = new SubmissionClient(accessToken, serviceEndpoint);

            client.DefaultHeaders = new Dictionary<string, string>()
            {
                {"X-Seller-Account-Id", this.ClientConfig.SellerId.ToString() }
            };

            Console.WriteLine("Getting Current Application Draft Status");
            
            dynamic AppDraftStatus = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.ProductDraftStatusPollingUrlTemplate,
                SubmissionClient.Version, appId), null);
            
            Console.WriteLine(AppDraftStatus.ToString());

            Console.WriteLine("Getting Application Packages ");

            dynamic PackagesResponse = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.PackagesUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(PackagesResponse.ToString());

            Console.WriteLine("Getting Single Package");

            dynamic SinglePackageResponse = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.PackageByIdUrlTemplate,
                SubmissionClient.Version, appId, (string)PackagesResponse.responseData.packages[0].packageId), null);

            Console.WriteLine(SinglePackageResponse.ToString());

            Console.WriteLine("Updating Entire Package Set");

            // Update data in Packages list to have final set of updated Packages

            // Example - Updating Installer Parameters
            PackagesResponse.responseData.packages[0].installerParameters = "/s /r new-args";

            dynamic PackagesUpdateRequest = new
            {
                packages = PackagesResponse.responseData.packages
            };

            dynamic PackagesUpdateResponse = await client.Invoke<dynamic>(HttpMethod.Put, string.Format(SubmissionClient.PackagesUrlTemplate,
                SubmissionClient.Version, appId), PackagesUpdateRequest);

            Console.WriteLine(PackagesUpdateResponse.ToString());

            Console.WriteLine("Updating Single Package's Download Url");

            // Update data in the SinglePackage object

            var SinglePackageUpdateRequest = SinglePackageResponse.responseData.packages[0];

            // Example - Updating Installer Parameters
            SinglePackageUpdateRequest.installerParameters = "/s /r /t new-args";

            dynamic PackageUpdateResponse = await client.Invoke<dynamic>(HttpMethod.Patch, string.Format(SubmissionClient.PackageByIdUrlTemplate,
                SubmissionClient.Version, appId, SinglePackageUpdateRequest.packageId), SinglePackageUpdateRequest);

            Console.WriteLine("Committing Packages");

            dynamic PackageCommitResponse = await client.Invoke<dynamic>(HttpMethod.Post, string.Format(SubmissionClient.PackagesCommitUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(PackageCommitResponse.ToString());

            Console.WriteLine("Polling Package Upload Status");

            AppDraftStatus = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.ProductDraftStatusPollingUrlTemplate,
                SubmissionClient.Version, appId), null);

            while (!((bool)AppDraftStatus.responseData.isReady))
            {
                AppDraftStatus = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.ProductDraftStatusPollingUrlTemplate,
                    SubmissionClient.Version, appId), null);

                Console.WriteLine("Waiting for Upload to finish");

                await Task.Delay(TimeSpan.FromSeconds(2));

                if(AppDraftStatus.errors != null && AppDraftStatus.errors.Count > 0)
                {
                    for(var index = 0; index < AppDraftStatus.errors.Count; index++)
                    {
                        if(AppDraftStatus.errors[index].code == "packageuploaderror")
                        {
                            throw new InvalidOperationException("Package Upload Failed. Please try commiting packages again.");
                        }
                    }
                }
            }

            Console.WriteLine("Getting Application Metadata - All Modules");

            dynamic AppMetadata = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.AppMetadataUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(AppMetadata.ToString());

            Console.WriteLine("Getting Application Metadata - Listings");

            dynamic AppListingsMetadata = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.AppListingsFetchMetadataUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(AppListingsMetadata.ToString());

            Console.WriteLine("Updating Listings Metadata - Description");

            // Update Required Fields in Listings Metadata Object - Per Language. For eg. AppListingsMetadata.responseData.listings[0]

            // Example - Updating Description
            AppListingsMetadata.responseData.listings[0].description = "New Description Updated By C# Sample Code";

            dynamic ListingsUpdateRequest = new
            {
                listings = AppListingsMetadata.responseData.listings[0]
            };

            dynamic UpdateListingsMetadataResponse = await client.Invoke<dynamic>(HttpMethod.Put, string.Format(SubmissionClient.AppMetadataUrlTemplate,
                SubmissionClient.Version, appId), ListingsUpdateRequest);

            Console.WriteLine(UpdateListingsMetadataResponse.ToString());

            Console.WriteLine("Getting All Listings Assets");

            dynamic ListingAssets = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.ListingAssetsUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(ListingAssets.ToString());

            Console.WriteLine("Creating Listing Assets for 1 Screenshot");

            
            dynamic AssetCreateRequest = new
            {
                language = ListingAssets.responseData.listingAssets[0].language,
                createAssetRequest = new Dictionary<string, int>()
                {
                    {"Screenshot", 1 },
                    {"Logo", 0 }
                }
            };

            dynamic AssetCreateResponse = await client.Invoke<dynamic>(HttpMethod.Post, string.Format(SubmissionClient.ListingAssetsCreateUrlTemplate,
               SubmissionClient.Version, appId), AssetCreateRequest);

            Console.WriteLine(AssetCreateResponse.ToString());

            Console.WriteLine("Uploading Listing Assets");

            // Path to PNG File to be Uploaded as Screenshot / Logo
            var PathToFile = "./Image.png";
            var AssetToUpload = File.OpenRead(PathToFile);

            await client.UploadAsset(AssetCreateResponse.responseData.listingAssets.screenshots[0].primaryAssetUploadUrl.Value as string, AssetToUpload);

            Console.WriteLine("Committing Listing Assets");

            dynamic AssetCommitRequest = new
            {
                listingAssets = new
                {
                    language = ListingAssets.responseData.listingAssets[0].language,
                    storeLogos = ListingAssets.responseData.listingAssets[0].storeLogos,
                    screenshots = JToken.FromObject(new List<dynamic>() { new
                {
                    id = AssetCreateResponse.responseData.listingAssets.screenshots[0].id.Value as string,
                    assetUrl = AssetCreateResponse.responseData.listingAssets.screenshots[0].primaryAssetUploadUrl.Value as string
                }
                }.ToArray())
                }
            };

            dynamic AssetCommitResponse = await client.Invoke<dynamic>(HttpMethod.Put, string.Format(SubmissionClient.ListingAssetsCommitUrlTemplate,
               SubmissionClient.Version, appId), AssetCommitRequest);

            Console.WriteLine(AssetCommitResponse.ToString());

            Console.WriteLine("Getting Current Application Draft Status before Submission");

            AppDraftStatus = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.ProductDraftStatusPollingUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(AppDraftStatus.ToString());

            if (AppDraftStatus == null || !((bool)AppDraftStatus.responseData.isReady))
            {
                throw new InvalidOperationException("Application Current Status is not in Ready Status for All Modules");
            }

            Console.WriteLine("Creating Submission");

            dynamic SubmissionCreationResponse = await client.Invoke<dynamic>(HttpMethod.Post, string.Format(SubmissionClient.CreateSubmissionUrlTemplate,
                SubmissionClient.Version, appId), null);

            Console.WriteLine(SubmissionCreationResponse.ToString());

            Console.WriteLine("Current Submission Status");

            dynamic SubmissionStatus = await client.Invoke<dynamic>(HttpMethod.Get, string.Format(SubmissionClient.SubmissionStatusPollingUrlTemplate,
                SubmissionClient.Version, appId, SubmissionCreationResponse.responseData.submissionId.Value as string), null);

            Console.Write(SubmissionStatus.ToString());

            // User can Poll on this API to know if Submission Status is INPROGRESS, PUBLISHED or FAILED.
            // This Process involves File Scanning, App Certification and Publishing and can take more than a day.
        }
    }
}
```

### IngestionClient helper class using C#

The IngestionClient class provides helper methods that are used by other methods in the sample app to perform the following tasks:

- Obtain an Azure AD access token that can be used to call methods in the Microsoft Store submission API. After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token.
- Process the HTTP requests for the Microsoft Store submission API.

```csharp
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Win32SubmissionApiCSharpSample
{
    /// <summary>
    /// This class is a proxy that abstracts the functionality of the API service
    /// </summary>
    public class SubmissionClient : IDisposable
    {
        public static readonly string Version = "1";
        private HttpClient httpClient;
        private HttpClient imageUploadClient;

        private readonly string accessToken;

        public static readonly string PackagesUrlTemplate = "/submission/v{0}/product/{1}/packages";
        public static readonly string PackageByIdUrlTemplate = "/submission/v{0}/product/{1}/packages/{2}";
        public static readonly string PackagesCommitUrlTemplate = "/submission/v{0}/product/{1}/packages/commit";
        public static readonly string AppMetadataUrlTemplate = "/submission/v{0}/product/{1}/metadata";
        public static readonly string AppListingsFetchMetadataUrlTemplate = "/submission/v{0}/product/{1}/metadata/listings";
        public static readonly string ListingAssetsUrlTemplate = "/submission/v{0}/product/{1}/listings/assets";
        public static readonly string ListingAssetsCreateUrlTemplate = "/submission/v{0}/product/{1}/listings/assets/create";
        public static readonly string ListingAssetsCommitUrlTemplate = "/submission/v{0}/product/{1}/listings/assets/commit";
        public static readonly string ProductDraftStatusPollingUrlTemplate = "/submission/v{0}/product/{1}/status";
        public static readonly string CreateSubmissionUrlTemplate = "/submission/v{0}/product/{1}/submit";
        public static readonly string SubmissionStatusPollingUrlTemplate = "/submission/v{0}/product/{1}/submission/{2}/status";

        public const string JsonContentType = "application/json";
        public const string PngContentType = "image/png";
        public const string BinaryStreamContentType = "application/octet-stream";

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmissionClient" /> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token. This is JWT a token obtained from Azure Active Directory allowing the caller to invoke the API
        /// on behalf of a user
        /// </param>
        /// <param name="serviceUrl">The service URL.</param>
        public SubmissionClient(string accessToken, string serviceUrl)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException("accessToken");
            }

            if (string.IsNullOrEmpty(serviceUrl))
            {
                throw new ArgumentNullException("serviceUrl");
            }

            this.accessToken = accessToken;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(serviceUrl)
            };
            this.imageUploadClient = new HttpClient();
            this.DefaultHeaders = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or Sets the default headers.
        /// </summary>
        public Dictionary<string, string> DefaultHeaders { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.httpClient != null)
            {
                this.httpClient.Dispose();
                this.httpClient = null;
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Gets the authorization token for the provided client id, client secret, and the scope.
        /// This token is usually valid for 1 hour, so if your submission takes longer than that to complete,
        /// make sure to get a new one periodically.
        /// </summary>
        /// <param name="tokenEndpoint">Token endpoint to which the request is to be made. Specific to your
        /// Azure Active Directory app. Example: https://login.microsoftonline.com/d454d300-128e-2d81-334a-27d9b2baf002/oauth2/v2.0/token </param>
        /// <param name="clientId">Client Id of your Azure Active Directory app. Example" ba3c223b-03ab-4a44-aa32-38aa10c27e32</param>
        /// <param name="clientSecret">Client secret of your Azure Active Directory app</param>
        /// <param name="scope">Scope. If not provided, default one is used for the production API endpoint.</param>
        /// <returns>Autorization token. Prepend it with "Bearer: " and pass it in the request header as the
        /// value for "Authorization: " header.</returns>
        public static async Task<string> GetClientCredentialAccessToken(
            string tokenEndpoint,
            string clientId,
            string clientSecret,
            string scope = null)
        {
            if (scope == null)
            {
                scope = "https://api.store.microsoft.com/.default";
            }

            dynamic result;
            using (HttpClient client = new HttpClient())
            {
                string tokenUrl = tokenEndpoint;
                using (
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Post,
                        tokenUrl))
                {
                    string strContent =
                        string.Format(
                            "grant_type=client_credentials&client_id={0}&client_secret={1}&scope={2}",
                            clientId,
                            clientSecret,
                            scope);

                    request.Content = new StringContent(strContent, Encoding.UTF8,
                        "application/x-www-form-urlencoded");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject(responseContent);
                    }
                }
            }

            return result.access_token;
        }


        /// <summary>
        /// Invokes the specified HTTP method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="requestContent">Content of the request.</param>
        /// <returns>instance of the type T</returns>
        /// <exception cref="ServiceException"></exception>
        public async Task<T> Invoke<T>(HttpMethod httpMethod,
            string relativeUrl,
            object requestContent)
        {
            using (var request = new HttpRequestMessage(httpMethod, relativeUrl))
            {
                this.SetRequest(request, requestContent);

                using (HttpResponseMessage response = await this.httpClient.SendAsync(request))
                {
                    T result;
                    if (this.TryHandleResponse(response, out result))
                    {
                        return result;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var resource = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                        return resource;
                    }

                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

        /// <summary>
        /// Uploads a given Image Asset file to Asset Storage
        /// </summary>
        /// <param name="assetUploadUrl">Asset Storage Url</param>
        /// <param name="fileStream">The Stream instance of file to be uploaded</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UploadAsset(string assetUploadUrl, Stream fileStream)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Put, assetUploadUrl))
            {
                request.Headers.Add("x-ms-blob-type", "BlockBlob");
                request.Content = new StreamContent(fileStream);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(PngContentType);
                using (HttpResponseMessage response = await this.imageUploadClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

        /// <summary>
        /// Sets the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="requestContent">Content of the request.</param>
        protected virtual void SetRequest(HttpRequestMessage request, object requestContent)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);

            foreach (var header in this.DefaultHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (requestContent != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(requestContent),
                        Encoding.UTF8,
                        JsonContentType);
                
            }
        }


        /// <summary>
        /// Tries the handle response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <param name="result">The result.</param>
        /// <returns>true if the response was handled</returns>
        protected virtual bool TryHandleResponse<T>(HttpResponseMessage response, out T result)
        {
            result = default(T);
            return false;
        }
    }
}
```

## Node.js sample: Microsoft Store Submission API for MSI or EXE app

This article provides Node.js code examples that demonstrate how to use the Microsoft Store submission API for MSI or EXE app.
You can review each example to learn more about the task it demonstrates, or you can build all the code examples in this article into a console application. 

**Prerequisites**
These examples use the following library:

- node-fetch v2 [npm install node-fetch@2]

### Create an app submission using node.js

The following example calls the other example methods in this article to demonstrate different ways to use the Microsoft Store submission API. To adapt this program for your own use:

- Assign the SellerId property to the Seller ID of your Partner Center account.
- Assign the ApplicationId property to the ID of the app you want to manage.
- Assign the ClientId and ClientSecret properties to the client ID and key for your app, and replace the tenantid string in the TokenEndpoint URL with the tenant ID for your app. For more information, see How to associate an Azure AD application with your Partner Center account

The following example implements a class that uses several methods in the Microsoft Store submission API to update an app submission.

```nodejs
const config = require('./Configuration');
const submissionClient = require('./SubmissionClient');
const fs = require('fs');

var client = new submissionClient(config);

/**
 * Main entry method to Run the Store Submission API Node.js Sample
 */
async function RunNodeJsSample(){
    print('Getting Access Token');
    await client.getAccessToken();
    
    print('Getting Current Application Draft Status');
    var currentDraftStatus = await client.callStoreAPI(client.productDraftStatusPollingUrlTemplate, 'get');
    print(currentDraftStatus);

    print('Getting Application Packages');
    var currentPackages = await client.callStoreAPI(client.packagesUrlTemplate, 'get');
    print(currentPackages);

    print('Getting Single Package');
    var packageId = currentPackages.responseData.packages[0].packageId;
    var packageIdUrl = `${client.packageByIdUrlTemplate}`.replace('{packageId}', packageId);
    var singlePackage = await client.callStoreAPI(packageIdUrl, 'get');
    print(singlePackage);

    print('Updating Entire Package Set');
    // Update data in Packages list to have final set of updated Packages
    currentPackages.responseData.packages[0].installerParameters = "/s /r new-args";
    var packagesUpdateRequest = {
        'packages': currentPackages.responseData.packages
    };
    print(packagesUpdateRequest);
    var packagesUpdateResponse = await client.callStoreAPI(client.packagesUrlTemplate, 'put', packagesUpdateRequest);
    print(packagesUpdateResponse);

    print('Updating Single Package\'s Download Url');
    // Update data in the SinglePackage object
    singlePackage.responseData.packages[0].installerParameters = "/s /r /t new-args";
    var singlePackageUpdateResponse = await client.callStoreAPI(packageIdUrl, 'patch', singlePackage.responseData.packages[0]);
    print(singlePackageUpdateResponse);

    print('Committing Packages');
    var commitPackagesResponse = await client.callStoreAPI(client.packagesCommitUrlTemplate, 'post');
    print(commitPackagesResponse);

    await poll(async ()=>{
        print('Waiting for Upload to finish');
        return await client.callStoreAPI(client.productDraftStatusPollingUrlTemplate, 'get');
    }, 2);

    print('Getting Application Metadata - All Modules');
    var appMetadata = await client.callStoreAPI(client.appMetadataUrlTemplate, 'get');
    print(appMetadata);

    print('Getting Application Metadata - Listings');
    var appListingMetadata = await client.callStoreAPI(client.appListingsFetchMetadataUrlTemplate, 'get');
    print(appListingMetadata);

    print('Updating Listings Metadata - Description');   
    // Update Required Fields in Listings Metadata Object - Per Language. For eg. AppListingsMetadata.responseData.listings[0]
    // Example - Updating Description
    appListingMetadata.responseData.listings[0].description = 'New Description Updated By Node.js Sample Code';
    var listingsUpdateRequest = {
        'listings': appListingMetadata.responseData.listings[0]
    };
    var listingsMetadataUpdateResponse = await client.callStoreAPI(client.appMetadataUrlTemplate, 'put', listingsUpdateRequest);
    print(listingsMetadataUpdateResponse);

    print('Getting All Listings Assets');
    var listingAssets = await client.callStoreAPI(client.listingAssetsUrlTemplate, 'get');
    print(listingAssets);

    print('Creating Listing Assets for 1 Screenshot');
    var listingAssetCreateRequest = {
        'language': listingAssets.responseData.listingAssets[0].language,
        'createAssetRequest': {
            'Screenshot': 1,
            'Logo': 0
        }
    };
    var listingAssetCreateResponse = await client.callStoreAPI(client.listingAssetsCreateUrlTemplate, 'post', listingAssetCreateRequest);
    print(listingAssetCreateResponse);

    print('Uploading Listing Assets');
    const pathToFile = './Image.png';
    const stats = fs.statSync(pathToFile);
    const fileSize = stats.size;
    const fileStream = fs.createReadStream(pathToFile);
    await client.uploadAssets(listingAssetCreateResponse.responseData.listingAssets.screenshots[0].primaryAssetUploadUrl, fileStream, fileSize);

    print('Committing Listing Assets');
    var assetCommitRequest = {
        'listingAssets': {
            'language': listingAssets.responseData.listingAssets[0].language,
            'storeLogos': listingAssets.responseData.listingAssets[0].storeLogos,
            'screenshots': [{
                'id': listingAssetCreateResponse.responseData.listingAssets.screenshots[0].id,
                'assetUrl': listingAssetCreateResponse.responseData.listingAssets.screenshots[0].primaryAssetUploadUrl
            }]
        }
    };
    var assetCommitResponse = await client.callStoreAPI(client.listingAssetsCommitUrlTemplate, 'put', assetCommitRequest);
    print(assetCommitResponse);

    print('Getting Current Application Draft Status before Submission');
    currentDraftStatus = await client.callStoreAPI(client.productDraftStatusPollingUrlTemplate, 'get');
    print(currentDraftStatus);
    if(!currentDraftStatus.responseData.isReady){
        throw new Error('Application Current Status is not in Ready Status for All Modules');
    }

    print('Creating Submission');
    var submissionCreationResponse = await client.callStoreAPI(client.createSubmissionUrlTemplate, 'post');
    print(submissionCreationResponse);

    print('Current Submission Status');
    var submissionStatusUrl = `${client.submissionStatusPollingUrlTemplate}`.replace('{submissionId}', submissionCreationResponse.responseData.submissionId);
    var submissionStatusResponse = await client.callStoreAPI(submissionStatusUrl, 'get');
    print(submissionStatusResponse);

    // User can Poll on this API to know if Submission Status is INPROGRESS, PUBLISHED or FAILED.
    // This Process involves File Scanning, App Certification and Publishing and can take more than a day.
}

/**
 * Utility Method to Poll using a given function and time interval in seconds
 * @param {*} func 
 * @param {*} intervalInSeconds 
 * @returns 
 */
async function poll(func, intervalInSeconds){
var result = await func();
if(result.responseData.isReady){
    Promise.resolve(true);
}
else if(result.errors && result.errors.length > 0 && result.errors.find(element => element.code == 'packageuploaderror') != undefined){
throw new Error('Package Upload Failed');
}
else{
    await new Promise(resolve => setTimeout(resolve, intervalInSeconds*1000));
    return await poll(func, intervalInSeconds); 
}
}

/**
 * Utility function to Print a Json or normal string
 * @param {*} json 
 */
function print(json){
    if(typeof(json) == 'string'){
        console.log(json);
    }
    else{
        console.log(JSON.stringify(json));
    }
    console.log("\n");
}

/** Run the Node.js Sample Application */
RunNodeJsSample();
```

### ClientConfiguration helper 
The sample app uses the ClientConfiguration helper class to pass Azure Active Directory data and app data to each of the example methods that use the Microsoft Store submission API.
```nodejs
/** Configuration Object for Store Submission API */
var config = {
    version : "1",
    applicationId : "...",
    clientId : "...",
    clientSecret : "...",
    serviceEndpoint : "https://api.store.microsoft.com",
    tokenEndpoint : "...",
    scope : "https://api.store.microsoft.com/.default",
    sellerId : "...",
    jsonContentType : "application/json",
    pngContentType : "image/png",
    binaryStreamContentType : "application/octet-stream"
};

module.exports = config;
```

### IngestionClient helper using node.js

The IngestionClient class provides helper methods that are used by other methods in the sample app to perform the following tasks:

- Obtain an Azure AD access token that can be used to call methods in the Microsoft Store submission API. After you obtain a token, you have 60 minutes to use this token in calls to the Microsoft Store submission API before the token expires. After the token expires, you can generate a new token.
- Process the HTTP requests for the Microsoft Store submission API.

```nodejs
const fetch = require('node-fetch');
/**
 * Submission Client to invoke all available Store Submission API and Asset Upload to Blob Store
 */
class SubmissionClient{

    constructor(config){
        this.configuration = config;
        this.accessToken = "";
        this.packagesUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/packages`;
        this.packageByIdUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/packages/{packageId}`;
        this.packagesCommitUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/packages/commit`;
        this.appMetadataUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/metadata`;
        this.appListingsFetchMetadataUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/metadata/listings`;
        this.listingAssetsUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/listings/assets`;
        this.listingAssetsCreateUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/listings/assets/create`;
        this.listingAssetsCommitUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/listings/assets/commit`;
        this.productDraftStatusPollingUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/status`;
        this.createSubmissionUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/submit`;
        this.submissionStatusPollingUrlTemplate = `/submission/v${this.configuration.version}/product/${this.configuration.applicationId}/submission/{submissionId}/status`;
    }
    
    async getAccessToken(){
        var params = new URLSearchParams();
        params.append('grant_type','client_credentials');
        params.append('client_id',this.configuration.clientId);
        params.append('client_secret',this.configuration.clientSecret);
        params.append('scope',this.configuration.scope);
        var response = await fetch(this.configuration.tokenEndpoint,{
            method: "POST",
            body: params
        });    
        var data = await response.json();
        this.accessToken = data.access_token;
    }

    async callStoreAPI(url, method, data){
        var request = {
            method: method,
            headers:{
                'Authorization': `Bearer ${this.accessToken}`,
                'Content-Type': this.configuration.jsonContentType,
                'X-Seller-Account-Id': this.configuration.sellerId
            },            
        };
        if(data){
            request.body = JSON.stringify(data);
        }
        var response = await fetch(`${this.configuration.serviceEndpoint}${url}`,request);
        var jsonResponse = await response.json();
        return jsonResponse;
    }

    async uploadAssets(url, stream, size){
        var request = {
            method: 'put',
            headers:{
                'Content-Type': this.configuration.pngContentType,
                'x-ms-blob-type': 'BlockBlob',
                "Content-length": size
            },            
            body: stream
        };
        var response = await fetch(`${url}`,request);
        if(response.ok){
            return response;
        }
        else{
            throw new Error('Uploading of assets failed');
        }
    }
}
module.exports = SubmissionClient;
```

## Additional help

If you have questions about the Microsoft Store submission API or need assistance managing your submissions with this API, use the following resources:

- Ask your questions on [our forums](https://social.msdn.microsoft.com/Forums/windowsapps/home?forum=wpsubmit).
- Visit our [support page](https://developer.microsoft.com/windows/support) and request one of the assisted support options for Partner Center. If you are prompted to choose a problem type and category, choose App submission and certification and Submitting an app, respectively.
