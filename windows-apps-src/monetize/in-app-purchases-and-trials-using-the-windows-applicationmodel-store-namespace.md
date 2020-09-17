---
ms.assetid: 32572890-26E3-4FBB-985B-47D61FF7F387
description: Learn how to enable in-app purchases and trials in UWP apps that target releases before Windows 10, version 1607.
title: In-app purchases and trials using the Windows.ApplicationModel.Store namespace
ms.date: 08/25/2017
ms.topic: article
keywords: uwp, in-app purchases, IAPs, add-ons, trials, Windows.ApplicationModel.Store
ms.localizationpriority: medium
---
# In-app purchases and trials using the Windows.ApplicationModel.Store namespace

You can use members in the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace to add in-app purchases and trial functionality to your Universal Windows Platform (UWP) app to help monetize your app. These APIs also provide access to the license info for your app.

The articles in this section provide in-depth guidance and code examples for using the members in the **Windows.ApplicationModel.Store** namespace for several common scenarios. For an overview of basic concepts related to in-app purchases in UWP apps, see [In-app purchases and trials](in-app-purchases-and-trials.md). For a complete sample that demonstrates how to implement trials and in-app purchases using the **Windows.ApplicationModel.Store** namespace, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store).

> [!IMPORTANT]
> The **Windows.ApplicationModel.Store** namespace is no longer being updated with new features. If your project targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio (that is, you are targeting Windows 10, version 1607, or later), we recommend that you use the [Windows.Services.Store](/uwp/api/windows.services.store) namespace instead. For more information, see [In-app purchases and trials](./in-app-purchases-and-trials.md). The **Windows.ApplicationModel.Store** namespace is not supported in Windows desktop applications that use the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop) or in apps or games that use a development sandbox in Partner Center (for example, this is the case for any game that integrates with Xbox Live). These products must use the **Windows.Services.Store** namespace to implement in-app purchases and trials.

## Get started with the CurrentApp and CurrentAppSimulator classes

The main entry point to the **Windows.ApplicationModel.Store** namespace is the [CurrentApp](/uwp/api/windows.applicationmodel.store.currentapp) class. This class provides static properties and methods you can use to get info for the current app and its available add-ons, get license info for the current app or its add-ons, purchase an app or add-on for the current user, and perform other tasks.

The [CurrentApp](/uwp/api/windows.applicationmodel.store.currentapp) class obtains its data from the Microsoft Store, so you must have a developer account and the app must be published in the Store before you can successfully use this class in your app. Before you submit your app to the Store, you can test your code with a simulated version of this class called [CurrentAppSimulator](/uwp/api/windows.applicationmodel.store.currentappsimulator). After you test your app, and before you submit it to the Microsoft Store, you must replace the instances of **CurrentAppSimulator** with **CurrentApp**. Your app will fail certification if it uses **CurrentAppSimulator**.

When the **CurrentAppSimulator** is used, the initial state of your app's licensing and in-app products is described in a local file on your development computer named WindowsStoreProxy.xml. For more information about this file, see [Using the WindowsStoreProxy.xml file with CurrentAppSimulator](#proxy).

For more information about common tasks you can perform using **CurrentApp** and **CurrentAppSimulator**, see the following articles.

| Topic       | Description                 |
|----------------------------|-----------------------------|
| [Exclude or limit features in a trial version](exclude-or-limit-features-in-a-trial-version-of-your-app.md) | If you enable customers to use your app for free during a trial period, you can entice your customers to upgrade to the full version of your app by excluding or limiting some features during the trial period. |
| [Enable in-app product purchases](enable-in-app-product-purchases.md)      |  Whether your app is free or not, you can sell content, other apps, or new app functionality (such as unlocking the next level of a game) from right within the app. Here we show you how to enable these products in your app.  |
| [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md)      | Offer consumable in-app products—items that can be purchased, used, and purchased again—through the Store commerce platform to provide your customers with a purchase experience that is both robust and reliable. This is especially useful for things like in-game currency (gold, coins, etc.) that can be purchased and then used to purchase specific power-ups. |
| [Manage a large catalog of in-app products](manage-a-large-catalog-of-in-app-products.md)      |   If your app offers a large in-app product catalog, you can optionally follow the process described in this topic to help manage your catalog.    |
| [Use receipts to verify product purchases](use-receipts-to-verify-product-purchases.md)      |   Each Microsoft Store transaction that results in a successful product purchase can optionally return a transaction receipt that provides information about the listed product and monetary cost to the customer. Having access to this information supports scenarios where your app needs to verify that a user purchased your app, or has made in-app product purchases from the Microsoft Store. |

<span id="proxy" />

## Using the WindowsStoreProxy.xml file with CurrentAppSimulator

When the **CurrentAppSimulator** is used, the initial state of your app's licensing and in-app products is described in a local file on your development computer named WindowsStoreProxy.xml. **CurrentAppSimulator** methods that alter the app's state, for example by buying a license or handling an in-app purchase, only update the state of the **CurrentAppSimulator** object in memory. The contents of WindowsStoreProxy.xml are not changed. When the app starts again, the license state reverts to what is described in WindowsStoreProxy.xml.

A WindowsStoreProxy.xml file is created by default at the following location: %UserProfile%\AppData\Local\Packages\\&lt;app package folder&gt;\LocalState\Microsoft\Windows Store\ApiData. You can edit this file to define the scenario that you want to simulate in the **CurrentAppSimulator** properties.

Although you can modify the values in this file, we recommend that you create your own WindowsStoreProxy.xml file (in a data folder of your Visual Studio project) for **CurrentAppSimulator** to use instead. When simulating the transaction, call [ReloadSimulatorAsync](/uwp/api/windows.applicationmodel.store.currentappsimulator.reloadsimulatorasync) to load your file. If you do not call **ReloadSimulatorAsync** to load your own WindowsStoreProxy.xml file, **CurrentAppSimulator** will create/load (but not overwrite) the default WindowsStoreProxy.xml file.

> [!NOTE]
> Be aware that **CurrentAppSimulator** is not fully initialized until **ReloadSimulatorAsync** completes. And, since **ReloadSimulatorAsync** is an asynchronous method, care should be taken to avoid the race condition of querying **CurrentAppSimulator** on one thread while it is being initialized on another. One technique is to use a flag to indicate that initialization is complete. An app that is installed from the Microsoft Store must use **CurrentApp** instead of **CurrentAppSimulator**, and in that case **ReloadSimulatorAsync** is not called and therefore the race condition just mentioned does not apply. For this reason, design your code so that it will work in both cases, both asynchronously and synchronously.


<span id="proxy-examples" />

### Examples

This example is a WindowsStoreProxy.xml file (UTF-16 encoded) that describes an app with a trial mode that expires at 05:00 (UTC) on Jan. 19, 2015.

> [!div class="tabbedCodeSnippets"]
```xml
<?xml version="1.0" encoding="UTF-16"?>
<CurrentApp>
  <ListingInformation>
    <App>
      <AppId>2B14D306-D8F8-4066-A45B-0FB3464C67F2</AppId>
      <LinkUri>http://apps.windows.microsoft.com/app/2B14D306-D8F8-4066-A45B-0FB3464C67F2</LinkUri>
      <CurrentMarket>en-US</CurrentMarket>
      <AgeRating>3</AgeRating>
      <MarketData xml:lang="en-us">
        <Name>App with a trial license</Name>
        <Description>Sample app for demonstrating trial license management</Description>
        <Price>4.99</Price>
        <CurrencySymbol>$</CurrencySymbol>
      </MarketData>
    </App>
  </ListingInformation>
  <LicenseInformation>
    <App>
      <IsActive>true</IsActive>
      <IsTrial>true</IsTrial>
      <ExpirationDate>2015-01-19T05:00:00.00Z</ExpirationDate>
    </App>
  </LicenseInformation>
  <Simulation SimulationMode="Automatic">
    <DefaultResponse MethodName="LoadListingInformationAsync_GetResult" HResult="E_FAIL"/>
  </Simulation>
</CurrentApp>
```

The next example is a WindowsStoreProxy.xml file (UTF-16 encoded) that describes an app that has been purchased, has a feature that expires at 05:00 (UTC) on Jan. 19, 2015, and has a consumable in-app purchase.

> [!div class="tabbedCodeSnippets"]
```xml
<?xml version="1.0" encoding="utf-16" ?>
<CurrentApp>
  <ListingInformation>
    <App>
      <AppId>988b90e4-5d4d-4dea-99d0-e423e414ffbc</AppId>
      <LinkUri>http://apps.windows.microsoft.com/app/988b90e4-5d4d-4dea-99d0-e423e414ffbc</LinkUri>
      <CurrentMarket>en-us</CurrentMarket>
      <AgeRating>3</AgeRating>
      <MarketData xml:lang="en-us">
        <Name>App with several in-app products</Name>
        <Description>Sample app for demonstrating an expiring in-app product and a consumable in-app product</Description>
        <Price>5.99</Price>
        <CurrencySymbol>$</CurrencySymbol>
      </MarketData>
    </App>
    <Product ProductId="feature1" LicenseDuration="10" ProductType="Durable">
      <MarketData xml:lang="en-us">
        <Name>Expiring Item</Name>
        <Price>1.99</Price>
        <CurrencySymbol>$</CurrencySymbol>
      </MarketData>
    </Product>
    <Product ProductId="consumable1" LicenseDuration="0" ProductType="Consumable">
      <MarketData xml:lang="en-us">
        <Name>Consumable Item</Name>
        <Price>2.99</Price>
        <CurrencySymbol>$</CurrencySymbol>
      </MarketData>
    </Product>
  </ListingInformation>
  <LicenseInformation>
    <App>
      <IsActive>true</IsActive>
      <IsTrial>false</IsTrial>
    </App>
    <Product ProductId="feature1">
      <IsActive>true</IsActive>
      <ExpirationDate>2015-01-19T00:00:00.00Z</ExpirationDate>
    </Product>
  </LicenseInformation>
  <ConsumableInformation>
    <Product ProductId="consumable1" TransactionId="00000001-0000-0000-0000-000000000000" Status="Active"/>
  </ConsumableInformation>
</CurrentApp>
```


<span id="proxy-schema" />

### Schema

This section lists the XSD file that defines the structure of the WindowsStoreProxy.xml file. To apply this schema to the XML editor in Visual Studio when working with your WindowsStoreProxy.xml file, do the following:

1. Open the WindowsStoreProxy.xml file in Visual Studio.
2. On the **XML** menu, click **Create Schema**. This will create a temporary WindowsStoreProxy.xsd file based on the contents of the XML file.
3. Replace the contents of that .xsd file with the schema below.
4. Save the file to a location where you can apply it to multiple app projects.
5. Switch to your WindowsStoreProxy.xml file in Visual Studio.
6. On the **XML** menu, click **Schemas**, then locate the row in the list for the WindowsStoreProxy.xsd file. If the location for the file is not the one you want (for example, if the temporary file is still shown), click **Add**. Navigate to the right file, then click **OK**. You should now see that file in the list. Make sure a checkmark appears in the **Use** column for that schema.

Once you've done this, edits you make to WindowsStoreProxy.xml will be subject to the schema. For more information, see [How to: Select the XML Schemas to Use](/visualstudio/xml-tools/how-to-select-the-xml-schemas-to-use).

> [!div class="tabbedCodeSnippets"]
```xml
<?xml version="1.0" encoding="utf-8"?>
<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:import namespace="http://www.w3.org/XML/1998/namespace"/>
  <xs:element name="CurrentApp" type="CurrentAppDefinition"></xs:element>
  <xs:complexType name="CurrentAppDefinition">
    <xs:sequence>
      <xs:element name="ListingInformation" type="ListingDefinition" minOccurs="1" maxOccurs="1"/>
      <xs:element name="LicenseInformation" type="LicenseDefinition" minOccurs="1" maxOccurs="1"/>
      <xs:element name="ConsumableInformation" type="ConsumableDefinition" minOccurs="0" maxOccurs="1"/>
      <xs:element name="Simulation" type="SimulationDefinition" minOccurs="0" maxOccurs="1"/>
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="ResponseCodes">
    <xs:restriction base="xs:string">
      <xs:enumeration value="S_OK">
        <xs:annotation>
          <xs:documentation>0x00000000</xs:documentation>
        </xs:annotation>
      </xs:enumeration>
      <xs:enumeration value="E_INVALIDARG">
        <xs:annotation>
          <xs:documentation>0x80070057</xs:documentation>
        </xs:annotation>
      </xs:enumeration>
      <xs:enumeration value="E_CANCELLED">
        <xs:annotation>
          <xs:documentation>0x800704C7</xs:documentation>
        </xs:annotation>
      </xs:enumeration>
      <xs:enumeration value="E_FAIL">
        <xs:annotation>
          <xs:documentation>0x80004005</xs:documentation>
        </xs:annotation>
      </xs:enumeration>
      <xs:enumeration value="E_OUTOFMEMORY">
        <xs:annotation>
          <xs:documentation>0x8007000E</xs:documentation>
        </xs:annotation>
      </xs:enumeration>
      <xs:enumeration value="ERROR_ALREADY_EXISTS">
        <xs:annotation>
          <xs:documentation>0x800700B7</xs:documentation>
        </xs:annotation>
      </xs:enumeration>
    </xs:restriction>
  </xs:simpleType>
  <xs:simpleType name="ConsumableStatus">
    <xs:restriction base="xs:string">
      <xs:enumeration value="Active"/>
      <xs:enumeration value="PurchaseReverted"/>
      <xs:enumeration value="PurchasePending"/>
      <xs:enumeration value="ServerError"/>
    </xs:restriction>
  </xs:simpleType>
  <xs:simpleType name="StoreMethodName">
    <xs:restriction base="xs:string">
      <xs:enumeration value="RequestAppPurchaseAsync_GetResult" id="RPPA"/>
      <xs:enumeration value="RequestProductPurchaseAsync_GetResult" id="RFPA"/>
      <xs:enumeration value="LoadListingInformationAsync_GetResult" id="LLIA"/>
      <xs:enumeration value="ReportConsumableFulfillmentAsync_GetResult" id="RPFA"/>
      <xs:enumeration value="LoadListingInformationByKeywordsAsync_GetResult" id="LLIKA"/>
      <xs:enumeration value="LoadListingInformationByProductIdAsync_GetResult" id="LLIPA"/>
      <xs:enumeration value="GetUnfulfilledConsumablesAsync_GetResult" id="GUC"/>
      <xs:enumeration value="GetAppReceiptAsync_GetResult" id="GARA"/>
    </xs:restriction>
  </xs:simpleType>
  <xs:simpleType name="SimulationMode">
    <xs:restriction base="xs:string">
      <xs:enumeration value="Interactive"/>
      <xs:enumeration value="Automatic"/>
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="ListingDefinition">
    <xs:sequence>
      <xs:element name="App" type="AppListingDefinition"/>
      <xs:element name="Product" type="ProductListingDefinition" minOccurs="0" maxOccurs="unbounded"/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ConsumableDefinition">
    <xs:sequence>
      <xs:element name="Product" type="ConsumableProductDefinition" minOccurs="0" maxOccurs="unbounded"/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="AppListingDefinition">
    <xs:sequence>
      <xs:element name="AppId" type="xs:string" minOccurs="1" maxOccurs="1"/>
      <xs:element name="LinkUri" type="xs:anyURI" minOccurs="1" maxOccurs="1"/>
      <xs:element name="CurrentMarket" type="xs:language" minOccurs="1" maxOccurs="1"/>
      <xs:element name="AgeRating" type="xs:unsignedInt" minOccurs="1" maxOccurs="1"/>
      <xs:element name="MarketData" type="MarketSpecificAppData" minOccurs="1" maxOccurs="unbounded"/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="MarketSpecificAppData">
    <xs:sequence>
      <xs:element name="Name" type="xs:string" minOccurs="1" maxOccurs="1"/>
      <xs:element name="Description" type="xs:string" minOccurs="1" maxOccurs="1"/>
      <xs:element name="Price" type="xs:float" minOccurs="1" maxOccurs="1"/>
      <xs:element name="CurrencySymbol" type="xs:string" minOccurs="1" maxOccurs="1"/>
      <xs:element name="CurrencyCode" type="xs:string" minOccurs="0" maxOccurs="1"/>
    </xs:sequence>
    <xs:attribute ref="xml:lang" use="required"/>
  </xs:complexType>
  <xs:complexType name="MarketSpecificProductData">
    <xs:sequence>
      <xs:element name="Name" type="xs:string" minOccurs="1" maxOccurs="1"/>
      <xs:element name="Price" type="xs:float" minOccurs="1" maxOccurs="1"/>
      <xs:element name="CurrencySymbol" type="xs:string" minOccurs="1" maxOccurs="1"/>
      <xs:element name="CurrencyCode" type="xs:string" minOccurs="0" maxOccurs="1"/>
      <xs:element name="Description" type="xs:string" minOccurs="0" maxOccurs="1"/>
      <xs:element name="Tag" type="xs:string" minOccurs="0" maxOccurs="1"/>
      <xs:element name="Keywords" type="KeywordDefinition" minOccurs="0" maxOccurs="1"/>
      <xs:element name="ImageUri" type="xs:anyURI" minOccurs="0" maxOccurs="1"/>
    </xs:sequence>
    <xs:attribute ref="xml:lang" use="required"/>
  </xs:complexType>
  <xs:complexType name="ProductListingDefinition">
    <xs:sequence>
      <xs:element name="MarketData" type="MarketSpecificProductData" minOccurs="1" maxOccurs="unbounded"/>
    </xs:sequence>
    <xs:attribute name="ProductId" use="required">
      <xs:simpleType>
        <xs:restriction base="xs:string">
          <xs:maxLength value="100"/>
          <xs:pattern value="[^,]*"/>
        </xs:restriction>
      </xs:simpleType>
    </xs:attribute>
    <xs:attribute name="LicenseDuration" type="xs:integer" use="optional"/>
    <xs:attribute name="ProductType" type="xs:string" use="optional"/>
  </xs:complexType>
  <xs:simpleType name="guid">
    <xs:restriction base="xs:string">
      <xs:pattern value="[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12}"/>
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="ConsumableProductDefinition">
    <xs:attribute name="ProductId" use="required">
      <xs:simpleType>
        <xs:restriction base="xs:string">
          <xs:maxLength value="100"/>
          <xs:pattern value="[^,]*"/>
        </xs:restriction>
      </xs:simpleType>
    </xs:attribute>
    <xs:attribute name="TransactionId" type="guid" use="required"/>
    <xs:attribute name="Status" type="ConsumableStatus" use="required"/>
    <xs:attribute name="OfferId" type="xs:string" use="optional"/>
  </xs:complexType>
  <xs:complexType name="LicenseDefinition">
    <xs:sequence>
      <xs:element name="App" type="AppLicenseDefinition"/>
      <xs:element name="Product" type="ProductLicenseDefinition" minOccurs="0" maxOccurs="unbounded"/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="AppLicenseDefinition">
    <xs:sequence>
      <xs:element name="IsActive" type="xs:boolean" minOccurs="1" maxOccurs="1"/>
      <xs:element name="IsTrial" type="xs:boolean" minOccurs="1" maxOccurs="1"/>
      <xs:element name="ExpirationDate" type="xs:dateTime" minOccurs="0" maxOccurs="1"/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ProductLicenseDefinition">
    <xs:sequence>
      <xs:element name="IsActive" type="xs:boolean" minOccurs="1" maxOccurs="1"/>
      <xs:element name="ExpirationDate" type="xs:dateTime" minOccurs="0" maxOccurs="1"/>
    </xs:sequence>
    <xs:attribute name="ProductId" type="xs:string" use="required"/>
    <xs:attribute name="OfferId" type="xs:string" use="optional"/>
  </xs:complexType>
  <xs:complexType name="SimulationDefinition" >
    <xs:sequence>
      <xs:element name="DefaultResponse" type="DefaultResponseDefinition" minOccurs="0" maxOccurs="unbounded"/>
    </xs:sequence>
    <xs:attribute name="SimulationMode" type="SimulationMode" use="optional"/>
  </xs:complexType>
  <xs:complexType name="DefaultResponseDefinition">
    <xs:attribute name="MethodName" type="StoreMethodName" use="required"/>
    <xs:attribute name="HResult" type="ResponseCodes" use="required"/>
  </xs:complexType>
  <xs:complexType name="KeywordDefinition">
    <xs:sequence>
      <xs:element name="Keyword" type="xs:string" minOccurs="0" maxOccurs="10"/>
    </xs:sequence>
  </xs:complexType>
</xs:schema>
```


<span id="proxy-descriptions" />

### Element and attribute descriptions

This section describes the elements and attributes in the WindowsStoreProxy.xml file.

The root element of this file is the **CurrentApp** element, which represents the current app. This element contains the following child elements.

|  Element  |  Required  |  Quantity  |  Description   |
|-------------|------------|--------|--------|
|  [ListingInformation](#listinginformation)  |    Yes        |  1  |  Contains data from the app's listing.            |
|  [LicenseInformation](#licenseinformation)  |     Yes       |   1    |   Describes the licenses available for this app and its durable add-ons.     |
|  [ConsumableInformation](#consumableinformation)  |      No      |   0 or 1   |   Describes the consumable add-ons that are available for this app.      |
|  [Simulation](#simulation)  |     No       |      0 or 1      |   Describes how calls to various [CurrentAppSimulator](/uwp/api/windows.applicationmodel.store.currentappsimulator) methods will work in the app during testing.    |

<span id="listinginformation" />

#### ListingInformation element

This element contains data from the app's listing. **ListingInformation** is a required child of the **CurrentApp** element.

**ListingInformation** contains the following child elements.

|  Element  |  Required  |  Quantity  |  Description   |
|-------------|------------|--------|--------|
|  [App](#app-child-of-listinginformation)  |    Yes   |  1   |    Provides data about the app.         |
|  [Product](#product-child-of-listinginformation)  |    No  |  0 or more   |      Describes an add-on for the app.     |     |

<span id="app-child-of-listinginformation"/>

#### App element (child of ListingInformation)

This element describes the app's license. **App** is a required child of the [ListingInformation](#listinginformation) element.

**App** contains the following child elements.

|  Element  |  Required  |  Quantity  | Description   |
|-------------|------------|--------|--------|
|  **AppId**  |    Yes   |  1   |   The GUID that identifies the app in the Store. This can be any GUID for testing.        |
|  **LinkUri**  |    Yes  |  1   |    The URI of the listing page in the store. This can be any valid URI for testing.         |
|  **CurrentMarket**  |    Yes  |  1   |    The customer's country/region.         |
|  **AgeRating**  |    Yes  |  1   |     An integer that represents the minimum age rating of the app. This is the same value you would specify in Partner Center when you submit the app. The values used by the Store are: 3, 7, 12, and 16. For more info on these ratings, see [Age ratings](../publish/age-ratings.md).        |
|  [MarketData](#marketdata-child-of-app)  |    Yes  |  1 or more      |    Contains info about the app for a given country/region. For each country/region in which the app is listed, you must include a **MarketData** element.       |    |

<span id="marketdata-child-of-app"/>

#### MarketData element (child of App)

This element provides info about the app for a given country/region. For each country/region in which the app is listed, you must include a **MarketData** element. **MarketData** is a required child of the [App](#app-child-of-listinginformation) element.

**MarketData** contains the following child elements.

|  Element  |  Required  |  Quantity  | Description   |
|-------------|------------|--------|--------|
|  **Name**  |    Yes   |  1   |   The name of the app in this country/region.        |
|  **Description**  |    Yes  |  1   |      The description of the app for this country/region.       |
|  **Price**  |    Yes  |  1   |     The price of the app in this country/region.        |
|  **CurrencySymbol**  |    Yes  |  1   |     The currency symbol used in this country/region.        |
|  **CurrencyCode**  |    No  |  0 or 1      |      The currency code used in this country/region.         |  |

**MarketData** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **xml:lang**  |    Yes        |     Specifies the country/region for which the market data info applies.          |  |

<span id="product-child-of-listinginformation"/>

#### Product element (child of ListingInformation)

This element describes an add-on for the app. **Product** is an optional child of the [ListingInformation](#listinginformation) element, and it contains one or more [MarketData](#marketdata-child-of-product) elements.

**Product** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **ProductId**  |    Yes        |    Contains the string used by the app to identify the add-on.           |
|  **LicenseDuration**  |    No        |    Indicates the number of days for which the license will be valid after the item has been purchased. The expiration date of the new license created by a product purchase is the purchase date plus the license duration. This attribute is used only if the **ProductType** attribute is **Durable**; this attribute is ignored for consumable add-ons.           |
|  **ProductType**  |    No        |    Contains a value to identify the persistence of the in-app product. The supported values are **Durable** (the default) and **Consumable**. For durable types, additional information is described by a [Product](#product-child-of-licenseinformation) element under [LicenseInformation](#licenseinformation); for consumable types, additional information is described by a [Product](#product-child-of-consumableinformation) element under [ConsumableInformation](#consumableinformation).           |  |

<span id="marketdata-child-of-product"/>

#### MarketData element (child of Product)

This element provides info about the add-on for a given country/region. For each country/region in which the add-on is listed, you must include a **MarketData** element. **MarketData** is a required child of the [Product](#product-child-of-listinginformation) element.

**MarketData** contains the following child elements.

|  Element  |  Required  |  Quantity  | Description   |
|-------------|------------|--------|--------|
|  **Name**  |    Yes   |  1   |   The name of the add-on in this country/region.        |
|  **Price**  |    Yes  |  1   |     The price of the add-on in this country/region.        |
|  **CurrencySymbol**  |    Yes  |  1   |     The currency symbol used in this country/region.        |
|  **CurrencyCode**  |    No  |  0 or 1      |      The currency code used in this country/region.         |  
|  **Description**  |    No  |   0 or 1   |      The description of the add-on for this country/region.       |
|  **Tag**  |    No  |   0 or 1   |      The [custom developer data](../publish/enter-add-on-properties.md#custom-developer-data) (also called tag) for the add-on.       |
|  **Keywords**  |    No  |   0 or 1   |      Contains up to 10 **Keyword** elements that contain the [keywords](../publish/enter-add-on-properties.md#keywords) for the add-on.       |
|  **ImageUri**  |    No  |   0 or 1   |      The [URI for the image](../publish/create-add-on-store-listings.md#icon) in the add-on's listing.           |  |

**MarketData** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **xml:lang**  |    Yes        |     Specifies the country/region for which the market data info applies.          |  |

<span id="licenseinformation"/>

#### LicenseInformation element

This element describes the licenses available for this app and its durable in-app products. **LicenseInformation** is a required child of the **CurrentApp** element.

**LicenseInformation** contains the following child elements.

|  Element  |  Required  |  Quantity  | Description   |
|-------------|------------|--------|--------|
|  [App](#app-child-of-licenseinformation)  |    Yes   |  1   |    Describes the app's license.         |
|  [Product](#product-child-of-licenseinformation)  |    No  |  0 or more   |      Describes the license status of a durable add-on in the app.         |   |

The following table shows how to simulate some common conditions by combining values under the **App** and **Product** elements.

|  Condition to simulate  |  IsActive  |  IsTrial  | ExpirationDate   |
|-------------|------------|--------|--------|
|  Fully licensed  |    true   |  false  |    Absent. It actually may be present and specify a future date, but you're advised to omit the element from the XML file. If it is present and specifies a date in the past, then **IsActive** will be ignored and taken to be false.          |
|  In trial period  |    true  |  true   |      &lt;a datetime in the future&gt; This element must be present because **IsTrial** is true. You can visit a website showing the current Coordinated Universal Time (UTC) to know how far in the future to set this to get the remaining trial period you want.         |
|  Expired trial  |    false  |  true   |      &lt;a datetime in the past&gt; This element must be present because **IsTrial** is true. You can visit a website showing the current Coordinated Universal Time (UTC) to know when "the past" is in UTC.         |
|  Invalid  |    false  | false       |     &lt;any value or omitted&gt;          |  |

<span id="app-child-of-licenseinformation"/>

#### App element (child of LicenseInformation)

This element describes the app's license. **App** is a required child of the [LicenseInformation](#licenseinformation) element.

**App** contains the following child elements.

|  Element  |  Required  |  Quantity  | Description   |
|-------------|------------|--------|--------|
|  **IsActive**  |    Yes   |  1   |    Describes the current license state of this app. The value **true** indicates the license is valid; **false** indicates an invalid license. Normally this value is **true**, whether the app has a trial mode or not.  Set this value to **false** to test how your app behaves when it has an invalid license.           |
|  **IsTrial**  |    Yes  |  1   |      Describes the current trial state of this app. The value **true** indicates the app is being used during the trial period; **false** indicates the app is not in a trial, either because the app has been purchased or the trial period has expired.         |
|  **ExpirationDate**  |    No  |  0 or 1       |     The date the trial period for this app expires, in Coordinated Universal Time (UTC). The date must be expressed as: yyyy-mm-ddThh:mm:ss.ssZ. For example, 05:00 on January 19, 2015 would be specified as 2015-01-19T05:00:00.00Z. This element is required when **IsTrial** is **true**. Otherwise, it is not required.          |  |

<span id="product-child-of-licenseinformation"/>

#### Product element (child of LicenseInformation)

This element describes the license status of a durable add-on in the app. **Product** is an optional child of the [LicenseInformation](#licenseinformation) element.

**Product** contains the following child elements.

|  Element  |  Required  |  Quantity  | Description   |
|-------------|------------|--------|--------|
|  **IsActive**  |    Yes   |  1     |    Describes the current license state of this add-on. The value **true** indicates the add-on can be used; **false** indicates the add-on cannot be used or has not been purchased           |
|  **ExpirationDate**  |    No   |  0 or 1     |     The date the add-on expires, in Coordinated Universal Time (UTC). The date must be expressed as: yyyy-mm-ddThh:mm:ss.ssZ. For example, 05:00 on January 19, 2015 would be specified as 2015-01-19T05:00:00.00Z. If this element is present, the add-on has an expiration date. If it's not present, the add-on does not expire.  |  

**Product** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **ProductId**  |    Yes        |   Contains the string used by the app to identify the add-on.            |
|  **OfferId**  |     No       |   Contains the string used by the app to identify the category in which the add-on belongs. This provides support for large item catalogs, as described in [Manage a large catalog of in-app products](manage-a-large-catalog-of-in-app-products.md).           |

<span id="simulation"/>

#### Simulation element

This element describes how calls to various [CurrentAppSimulator](/uwp/api/windows.applicationmodel.store.currentappsimulator) methods will work in the app during testing. **Simulation** is an optional child of the **CurrentApp** element, and it contains zero or more [DefaultResponse](#defaultresponse) elements.

**Simulation** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **SimulationMode**  |    No        |      Values can be **Interactive** or **Automatic**. When this attribute is set to **Automatic**, the methods will automatically return the specified HRESULT error codes. This can be used when running automated test cases.       |

<span id="defaultresponse"/>

#### DefaultResponse element

This element describes the default error code returned by a **CurrentAppSimulator** method. **DefaultResponse** is an optional child of the [Simulation](#simulation) element.

**DefaultResponse** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **MethodName**  |    Yes        |   Assign this attribute to one of the enum values shown for the **StoreMethodName** type in the [schema](#schema). Each of these enum values represents a **CurrentAppSimulator** method for which you want to simulate an error code return value in your app during testing. For example, the value **RequestAppPurchaseAsync_GetResult** indicates you want to simulate the error code return value of the [RequestAppPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentappsimulator.requestapppurchaseasync) method.            |
|  **HResult**  |     Yes       |   Assign this attribute to one of the enum values shown for the **ResponseCodes** type in the [schema](#schema). Each of these enum values represents the error code you want to return for the method that is assigned to the **MethodName** attribute for this **DefaultResponse** element.           |

<span id="consumableinformation"/>

#### ConsumableInformation element

This element describes the consumable add-ons available for this app. **ConsumableInformation** is an optional child of the **CurrentApp** element, and it can contain zero or more [Product](#product-child-of-consumableinformation) elements.

<span id="product-child-of-consumableinformation"/>

#### Product element (child of ConsumableInformation)

This element describes a consumable add-on. **Product** is an optional child of the [ConsumableInformation](#consumableinformation) element.

**Product** has the following attributes.

|  Attribute  |  Required  |  Description   |
|-------------|------------|----------------|
|  **ProductId**  |    Yes        |   Contains the string used by the app to identify the consumable add-on.            |
|  **TransactionId**  |     Yes       |   Contains a GUID (as a string) used by the app to track the purchase transaction of a consumable through the process of fulfillment. See [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md).            |
|  **Status**  |      Yes      |  Contains the string used by the app to indicate the fulfillment status of a consumable. Values can be **Active**, **PurchaseReverted**, **PurchasePending**, or **ServerError**.             |
|  **OfferId**  |     No       |    Contains the string used by the app to identify the category in which the consumable belongs. This provides support for large item catalogs, as described in [Manage a large catalog of in-app products](manage-a-large-catalog-of-in-app-products.md).           |