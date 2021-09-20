---
description: Learn how to use native ads, a component-based ad format where each piece of the ad is delivered to your app as an individual element.
title: Native ads
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, ad control, native ad
ms.localizationpriority: medium
---
# Native ads

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

A native ad is a component-based ad format where each piece of the ad creative (such as the title, image, description, and call-to-action text) is delivered to your app as an individual element. You can integrate these elements into your app using your own fonts, colors, animations, and other UI components to stitch together an unobtrusive user experience that fits the look and feel of your app while also earning high yield from the ads.

For advertisers, native ads provide high-performing placements, because the ad experience is tightly integrated into the app and users therefore tend to interact more with these types of ads.

> [!NOTE]
> Native ads are currently supported only for XAML-based UWP apps for Windows 10. Support for UWP apps written using HTML and JavaScript is planned for a future release of the Microsoft Advertising SDK.

## Prerequisites

* Install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) with Visual Studio 2015 or a later release of Visual Studio. For installation instructions, see [this article](install-the-microsoft-advertising-libraries.md).

## Integrate a native ad into your app

Follow these instructions to integrate a native ad into your app and confirm that your native ad implementation shows a test ad.

1. In Visual Studio, open your project or create a new project.
    > [!NOTE]
    > If you're using an existing project, open the Package.appxmanifest file in your project and ensure that the **Internet (Client)** capability is selected. Your app needs this capability to receive test ads and live ads.

2. If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft Advertising SDK in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

3. Add a reference to the Microsoft Advertising SDK in your project:

    1. From the **Solution Explorer** window, right click **References**, and select **Add Reference…**
    2.  In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).
    3.  In **Reference Manager**, click OK.

4. In the appropriate code file in your app (for example, in MainPage.xaml.cs or a code file for some other page), add the following namespace references.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs" id="Namespaces":::

5.  In an appropriate location in your app (for example, in ```MainPage``` or some other page), declare a [NativeAdsManagerV2](/uwp/api/microsoft.advertising.winrt.ui.nativeadsmanagerv2) object and several string fields that represent the application ID and ad unit ID for your native ad. The following code example assigns the `myAppId` and `myAdUnitId` fields to the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for native ads.
    > [!NOTE]
    > Every **NativeAdsManagerV2** has a corresponding *ad unit* that is used by our services to serve ads to the native ad control, and every ad unit consists of an *ad unit ID* and *application ID*. In these steps, you assign test ad unit ID and application ID values to your control. These test values can only be used in a test version of your app. Before you publish your app to the Store, you must [replace these test values with live values](#release) from Partner Center.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs" id="Variables":::

6.  In code that runs on startup (for example, in the constructor for the page), instantiate the **NativeAdsManagerV2** object and wire up event handlers for the **AdReady** and **ErrorOccurred** events of the object.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs" id="ConfigureNativeAd":::

7.  When you're ready to show a native ad, call the **RequestAd** method to fetch an ad.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs" id="RequestAd":::

8.  When a native ad is ready for your app, your [AdReady](/uwp/api/microsoft.advertising.winrt.ui.nativeadsmanagerv2.adready) event handler is called, and a [NativeAdV2](/uwp/api/microsoft.advertising.winrt.ui.nativeadv2) object that represents the native ad is passed to the *e* parameter. Use the **NativeAdV2** properties to get each element of the native ad and display these elements on your page. Be sure to also call the **RegisterAdContainer** method to register the UI element that acts as a container for the native ad; this is required to properly track ad impressions and clicks.
    > [!NOTE]
    > Some elements of the native ad are required and must always be shown in your app. For more information, see our [guidelines for native ads](ui-and-user-experience-guidelines.md#guidelines-for-native-ads).

    For example, assume that your app contains a ```MainPage``` (or some other page) with the following **StackPanel**. This **StackPanel** contains a series of controls that display different elements of a native ad, including the title, description, images, *sponsored by* text, and a button that will show the *call to action* text.

    ``` xml
    <StackPanel x:Name="NativeAdContainer" Background="#555555" Width="Auto" Height="Auto"
                Orientation="Vertical">
        <Image x:Name="AdIconImage" HorizontalAlignment="Left" VerticalAlignment="Center"
               Margin="20,20,20,20"/>
        <TextBlock x:Name="TitleTextBlock" HorizontalAlignment="Left" VerticalAlignment="Center"
               Text="The ad title will go here" FontSize="24" Foreground="White" Margin="20,0,0,10"/>
        <TextBlock x:Name="DescriptionTextBlock" HorizontalAlignment="Left" VerticalAlignment="Center"
                   Foreground="White" TextWrapping="Wrap" Text="The ad description will go here"
                   Margin="20,0,0,0" Visibility="Collapsed"/>
        <Image x:Name="MainImageImage" HorizontalAlignment="Left"
               VerticalAlignment="Center" Margin="20,20,20,20" Visibility="Collapsed"/>
        <Button x:Name="CallToActionButton" Background="Gray" Foreground="White"
                HorizontalAlignment="Left" VerticalAlignment="Center" Width="Auto" Height="Auto"
                Content="The call to action text will go here" Margin="20,20,20,20"
                Visibility="Collapsed"/>
        <StackPanel x:Name="SponsoredByStackPanel" Orientation="Horizontal" Margin="20,20,20,20">
            <TextBlock x:Name="SponsoredByTextBlock" Text="The ad sponsored by text will go here"
                       FontSize="24" Foreground="White" Margin="20,0,0,0" HorizontalAlignment="Left"
                       VerticalAlignment="Center" Visibility="Collapsed"/>
            <Image x:Name="IconImageImage" Margin="40,20,20,20" HorizontalAlignment="Left"
                   VerticalAlignment="Center" Visibility="Collapsed"/>
        </StackPanel>
    </StackPanel>
    ```

    The following code example demonstrates an **AdReady** event handler that displays each element of the native ad in the controls in the **StackPanel** and then calls the **RegisterAdContainer** method to register the **StackPanel**. This code assumes that it is run from the code-behind file for the page that contains the **StackPanel**.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs" id="AdReady":::

9.  Define an event handler for the **ErrorOccurred** event to handle errors related to the native ad. The following example writes error information to the Visual Studio **Output** window during testing.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs" id="ErrorOccurred":::

10.  Compile and run the app to see it with a test ad.

<span id="release" />

## Release your app with live ads

After you confirm that your native ad implementation successfully shows a test ad, follow these instructions to configure your app to show real ads and submit your updated app to the Store.

1.  Make sure that your native ad implementation follows our [guidelines for native ads](ui-and-user-experience-guidelines.md#guidelines-for-native-ads).

2.  In Partner Center, go to the [In-app ads](../publish/in-app-ads.md) page and [create an ad unit](set-up-ad-units-in-your-app.md#live-ad-units). For the ad unit type, specify **Native**. Make note of both the ad unit ID and the application ID.
    > [!NOTE]
    > The application ID values for test ad units and live UWP ad units have different formats. Test application ID values are GUIDs. When you create a live UWP ad unit in Partner Center, the application ID value for the ad unit always matches the Store ID for your app (an example Store ID value looks like 9NBLGGH4R315).

3. You can optionally enable ad mediation for the native ad by configuring the settings in the [Mediation settings](../publish/in-app-ads.md#mediation) section on the [In-app ads](../publish/in-app-ads.md) page. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks.

4.  In your code, replace the test ad unit values (that is, the *applicationId* and *adUnitId* parameters of the [NativeAdsManagerV2](/uwp/api/microsoft.advertising.winrt.ui.nativeadsmanagerv2.-ctor) constructor) with the live values you generated in Partner Center.

5.  [Submit your app](../publish/app-submissions.md) to the Store using Partner Center.

6.  Review your [advertising performance reports](../publish/advertising-performance-report.md) in Partner Center.

## Manage ad units for multiple native ads in your app

You can use multiple native ad placements in a single app. In this scenario, we recommend that you assign a different ad unit to each native ad placements. Using different ad units for native ad enables you to separately [configure the mediation settings](../publish/in-app-ads.md#mediation) and get discrete [reporting data](../publish/advertising-performance-report.md) for each control. This also enables our services to better optimize the ads we serve to your app.

> [!IMPORTANT]
> You can use each ad unit in only one app. If you use an ad unit in more than one app, ads will not be served for that ad unit.

## Related topics

* [Guidelines for native ads](ui-and-user-experience-guidelines.md#guidelines-for-native-ads)
* [In-app ads](../publish/in-app-ads.md)
* [Set up ad units for your app](set-up-ad-units-in-your-app.md)
