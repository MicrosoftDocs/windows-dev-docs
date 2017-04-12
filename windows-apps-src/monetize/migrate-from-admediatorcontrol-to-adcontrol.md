---
author: mcleanbyron
ms.assetid: 9621641A-7462-425D-84CC-101877A738DA
description: Learn about how to migrate from the AdMediatorControl to AdControl in your UWP apps.
title: Migrate from AdMediatorControl to AdControl
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, AdMediatorControl, AdControl, migrate
---

# Migrate from AdMediatorControl to AdControl

Previous advertising SDK releases from Microsoft enabled Universal Windows Platform (UWP) apps to display banner ads using the **AdMediatorControl** class, which enabled developers to optimize their ad revenue by displaying banner ads from our partner networks (AOL and AppNexus) as well as AdDuplex. The [Microsoft Store Services SDK](http://aka.ms/store-em-sdk) no longer supports the **AdMediatorControl** class. If you have an existing app that uses the **AdMediatorControl** class from a previous SDK and you want to migrate it to a UWP app that uses the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk), follow the instructions in this article to update your code to use the **AdControl** class instead of the **AdMediatorControl** class. You can optionally configure your app to mediate ads with AdDuplex, using a weighted or ranked approach.

>**Note**&nbsp;&nbsp;The code examples in this article are provided for illustration purposes only. You may need to make adjustments to the code examples to work in your app.

## Prerequisites

* A UWP app that is currently using the AdMediatorControl and is published in the Windows Store.
* A development computer with Visual Studio 2015 and the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk) installed.
* If you want to mediate ads with AdDuplex, you must also have the [AdDuplex Windows 10 SDK](https://visualstudiogallery.msdn.microsoft.com/6930860a-e64b-4b46-9d72-62d7fddda077) installed on your development computer.

  >**Note**&nbsp;&nbsp;As an alternative to running the AdDuplex SDK installer from the link above, you can install the AdDuplex libraries for your UWP app project in Visual Studio 2015. With your UWP app project open in Visual Studio 2015, click **Project** > **Manage NuGet Packages**, search for the NuGet package named **AdDuplexWin10**, and install the package.

## Step 1: Retrieve your application IDs and ad unit IDs

When you migrate your code to use the **AdControl** class, you must know your application IDs and ad unit IDs. The best way to get the most recent IDs is to retrieve them from your mediation configuration file.

1. Sign in to the Windows Dev Center dashboard and click the app that is currently using **AdMediatorControl**.
2. Click **Monetization** and then **Monetize with ads**.
3. In the **Windows ad mediation** section, click the **Download mediation configuration** link and open the AdMediator.config file in a text editor such as Notepad.
4. In the file, locate the ```<AdAdapterInfo>``` element with the child element ```<Name>MicrosoftAdvertising</Name>```. This section contains the configuration for Microsoft paid ads.
5. In this ```<AdAdapterInfo>``` element, locate the ```<Property>``` elements that contain ```<Key>``` elements with the values **WApplicationId** and **WAdUnitId**. In the example below, the values of the ```<Value>``` elements are examples.

  ```xml
  <Metadata>
      <Property>
          <Key>WApplicationId</Key>
          <Value>364d4938-c0f5-4c3d-8aae-090206211dc9</Value>
      </Property>
      <Property>
          <Key>WAdUnitId</Key>
          <Value>301568</Value>
      </Property>
  </Metadata>
  ```

6. Copy both of the values in these ```<Value>``` elements for use later. These values contain the application ID and ad unit ID for the non-mobile ad unit for Microsoft paid ads.
5. In the same ```<AdAdapterInfo>``` element, locate the ```<Property>``` elements that contain ```<Key>``` elements with the values **MApplicationId** and **MAdUnitId**. In the example below, the values of the ```<Value>``` elements are examples.

  ```xml
  <Metadata>
      <Property>
          <Key>MApplicationId</Key>
          <Value>e2cf8388-7018-4a11-8ab0de90f2a7a401</Value>
      </Property>
      <Property>
          <Key>MAdUnitId</Key>
          <Value>301056</Value>
      </Property>
  </Metadata>
  ```

6. Copy both of the values in the ```<Value>``` elements for use later. These values contain the application ID and ad unit ID for the mobile ad unit for Microsoft paid ads.
7. If you use [house ads](../publish/about-house-ads.md), locate the ```<AdAdapterInfo>``` element with the child element ```<Name>MicrosoftAdvertisingHouse</Name>```. In this element, locate ```<Key>``` elements with the values **MAdUnitId** and **WAdUnitId**, and save the values of the corresponding ```<Value>``` elements for use later. These are the mobile and non-mobile ad unit IDs for Microsoft house ads, respectively.
8. If you use AdDuplex, locate the ```<AdAdapterInfo>``` element with the child element ```<Name>AdDuplex</Name>```. In this element, locate the ```<Key>``` elements with the values **AppKey** and **AdUnitId**, and save the values of the corresponding ```<Value>``` elements for use later. This is your AdDuplex app key and ad unit ID, respectively.

## Step 2: Update your app code

Now that you have your application IDs and ad unit IDs, you are ready to update the code in your app to use **AdControl** instead of **AdMediatorControl**.

### Microsoft paid ads only

If you only use Microsoft paid ads in your ad mediation configuration, follow these steps.

  >**Note**&nbsp;&nbsp;These steps assume that the app page on which you want to display ads contains an empty grid named **myAdGrid**, for example: ```<Grid x:Name="myAdGrid"/>```. In these steps, you will create and configure the ad controls entirely in code, rather than XAML.

1. In Visual Studio, open your UWP app project.
2.  From the **Solution Explorer** window, right click **References**, and select **Add Reference…**.
In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).
3. In **Reference Manager**, click OK.
4. Remove the **AdMediatorControl** declaration from your XAML and remove any code that uses this **AdMediatorControl** object, including any related event handlers.
5. Open the code file for the app **Page** on which you want to display ads.
6. Add the following statement to the top of the code file, if it does not already exist.

  [!code-cs[TrialVersion](./code/AdvertisingSamples/MigrateToAdControl/cs/MainPage.xaml.cs#Snippet1)]

7. Add the following constant declarations to your **Page** class.

  [!code-cs[TrialVersion](./code/AdvertisingSamples/MigrateToAdControl/cs/MainPage.xaml.cs#Snippet2)]

7. For each of these constant declarations, replace the values as described below:

  * **AD_WIDTH** and **AD_HEIGHT**: Assign these to one of the [supported ad sizes for banner ads]( https://msdn.microsoft.com/windows/uwp/monetize/supported-ad-sizes-for-banner-ads).
  * **WAPPLICATIONID** and **WADUNITID**: Assign these to the **WApplicationId** and **WAdUnitId** values for Microsoft paid ads that you retrieved earlier from the mediation configuration file (these values are for the non-mobile ad unit for paid ads).
  * **MAPPLICATIONID** and **MADUNITID**: Assign these to the **MApplicationId** and **MAdUnitId** values for Microsoft paid ads that you retrieved earlier from the mediation configuration file (these values are for the mobile ad unit for paid ads).

8. Add the following variable declarations to your **Page** class.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/MainPage.xaml.cs#Snippet3)]

5. Add the following code to your **Page** class constructor, after the call to the **InitializeComponent()** method.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/MainPage.xaml.cs#Snippet4)]

### Microsoft paid ads, house ads, and AdDuplex

If you use Microsoft house ads or AdDuplex as well as Microsoft paid ads and you want to continue to mediate ads with AdDuplex, follow the steps in this section. The code examples support both AdDuplex and Microsoft house ads. If you use AdDuplex but not Microsoft house ads or vice versa, remove the code that doesn't apply to your scenario.

  >**Note**&nbsp;&nbsp;These steps assume that the app page on which you want to display ads contains an empty grid named **myAdGrid**, for example: ```<Grid x:Name="myAdGrid"/>```. In these steps, you will create and configure the ad controls entirely in code, rather than XAML.

1. In Visual Studio, open your UWP app project.
2.  From the **Solution Explorer** window, right click **References**, and select **Add Reference…**.
In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).
3. In **Reference Manager**, click OK.
4. Remove the **AdMediatorControl** declaration from your XAML and remove any code that uses this **AdMediatorControl** object, including any related event handlers.
5. Open the code file for the app **Page** on which you want to display ads.
6. Add the following statements to the top of the code file, if they do not already exist.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/ExamplePage1.xaml.cs#Snippet1)]

7. Add the following constant declarations to your **Page** class.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/ExamplePage1.xaml.cs#Snippet2)]

4. For these constant declarations, replace the values as described below:

  * **AD_WIDTH** and **AD_HEIGHT**: Assign these to one of the [supported ad sizes for banner ads]( https://msdn.microsoft.com/windows/uwp/monetize/supported-ad-sizes-for-banner-ads).
  * **HOUSE_AD_WEIGHT**: Assign this to an integer from 0 to 100 that specifies the weight value you want to apply to Microsoft house ads compared to Microsoft paid ads (where 0 is never show house ads and 100 is always show house ads).
  * **WAPPLICATIONID** and **WADUNITID_PAID**: Assign these to the **WApplicationId** and **WAdUnitId** values for Microsoft paid ads that you retrieved earlier from the mediation configuration file (these values are for the non-mobile ad unit for paid ads).
  * **WADUNITID_HOUSE**: Assign this to the **WAdUnitId** for house ads that you retrieved earlier from the mediation configuration file (this value is for the non-mobile ad unit for house ads).
  * **MAPPLICATIONID** and **MADUNITID_PAID**: Assign these to the **MApplicationId** and **MAdUnitId** values for Microsoft paid ads that you retrieved earlier from the mediation configuration file (these values are for the mobile ad unit for paid ads).
  * **MADUNITID_HOUSE**: Assign this to the **MAdUnitId** for house ads that you retrieved earlier from the mediation configuration file (this value is for the mobile ad unit for house ads).
  * **ADDUPLEX_APPKEY** and **ADDUPLEX_ADUNIT**: Assign these to the AdDuplex app key and ad unit ID values you retrieved earlier from the mediation configuration file.

  >**Note**&nbsp;&nbsp;Do not change the **AD_REFRESH_SECONDS** and **MAX_ERRORS_PER_REFRESH** values shown in the previous example.

5. Add the following variable declarations to your **Page** class.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/ExamplePage1.xaml.cs#Snippet3)]

5. Add the following code to your **Page** class constructor, after the call to the **InitializeComponent()** method.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/ExamplePage1.xaml.cs#Snippet4)]

6. Finally, add the following methods to your **Page** class. These methods instantiate the Microsoft **AdControl** and AdDuplex **AdControl** objects, and they use a random number generator in conjunction with given weight values to refresh banner ads in these controls at regular timer intervals.

  [!code-cs[AdControl](./code/AdvertisingSamples/MigrateToAdControl/cs/ExamplePage1.xaml.cs#Snippet5)]
