---
author: mcleanbyron
description: Learn how to add native ads to your UWP app.
title: Native ads
ms.author: mcleans
ms.date: 06/02/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, native ad
---

# Native ads

A native ad is a component-based ad format where each piece of the ad creative (such as the title, image, description, and call-to-action text) is delivered to your app as an individual element. You can integrate these elements into your app using your own fonts, colors, animations, and other UI components to stitch together an unobtrusive user experience that fits the look and feel of your app while also earning high yield from the ads.

For advertisers, native ads provide high-performing placements, because the ad experience is tightly integrated into the app and users therefore tend to interact more with these types of ads.

> [!NOTE]
> To serve native ads to the public version of your app in the Store, you must create a **Native** ad unit from the **Monetize with ads** page in the Dev Center dashboard. The ability to create **Native** ad units is currently available only to select developers who are participating in a pilot program, but we intend to make  this feature available to all developers soon. If you are interested in joining our pilot program, reach out to us at aiacare@microsoft.com.

> [!NOTE]
> Native ads are currently supported only for XAML-based UWP apps for Windows 10. Support for UWP apps written using HTML and JavaScript is planned for a future release of the Microsoft Advertising SDK.

## Prerequisites

* Install the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp) (version 10.0.4 or later) with Visual Studio 2015 or a later release of Visual Studio. For installation instructions, see [this article](install-the-microsoft-advertising-libraries.md). You can install the SDK on your development computer via the MSI installer, or you can alternatively install the SDK for use in a specific project via the NuGet package.

## Integrate a native ad into your app

Follow these instructions to integrate a native ad into your app and confirm that your native ad implementation shows a test ad.

1. In Visual Studio, open your project or create a new project.

2. If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft Advertising SDK in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

3.  From the **Solution Explorer** window, right click **References**, and select **Add Reference…**

4.  In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).

5.  In **Reference Manager**, click OK.

6. In the appropriate code file in your app (for example, in MainPage.xaml.cs or a code file for some other page), add the following namespace references.

    [!code-cs[NativeAd](./code/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs#Namespaces)]

7.  In an appropriate location in your app (for example, in ```MainPage``` or some other page), declare a **NativeAdsManager** object and several string fields that represent the application ID and ad unit ID for your native ad. The following code example assigns the `myAppId` and `myAdUnitId` fields to the [test values](test-mode-values.md) for native ads. These values are only used for testing; you must [replace them with live values](#live-ads) from Windows Dev Center before you publish your app.

    [!code-cs[NativeAd](./code/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs#Variables)]

8.  In code that runs on startup (for example, in the constructor for the page), instantiate the **NativeAdsManager** object and wire up event handlers for the **AdReady** and **ErrorOccurred** events of the object.

    [!code-cs[NativeAd](./code/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs#ConfigureNativeAd)]

9.  When you're ready to show a native ad, call the **RequestAd** method to fetch an ad.

    [!code-cs[NativeAd](./code/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs#RequestAd)]

10.  When a native ad is ready for your app, your **AdReady** event handler is called, and a **NativeAd** object that represents the native ad is passed to the *e* parameter. Use the **NativeAd** properties to get each element of the native ad and display these elements on your page. Be sure to also call the **RegisterAdContainer** method to register the UI element that acts as a container for the native ad; this is required to properly track ad impressions and clicks.
  > [!NOTE]
  > Some elements of the native ad are required and must always be shown in your app. For more information, see the [requirements and guidelines](#requirements-and-guidelines).

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

    [!code-cs[NativeAd](./code/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs#AdReady)]

11.  Define an event handler for the **ErrorOccurred** event to handle errors related to the native ad. The following example writes error information to the Visual Studio **Output** window during testing.

    [!code-cs[NativeAd](./code/AdvertisingSamples/NativeAdSamples/cs/MainPage.xaml.cs#ErrorOccurred)]

12.  Compile and run the app to see it with a test ad.

<span id="live-ads" />
## Release your app with live ads

After you confirm that your native ad implementation successfully shows a test ad, follow these instructions to configure your app to show real ads and submit your updated app to the Store.

1.  Make sure that your native ad implementation follows the [requirements and guidelines](#requirements-and-guidelines) for native ads.

2.  In the Dev Center dashboard, go to the **Monetization** &gt; **Monetize with ads** page for your app, and [create an ad unit](../publish/monetize-with-ads.md#create-ad-unit). For the ad unit type, specify **Native**. Make note of both the ad unit ID and the application ID.

3. You can optionally enable ad mediation for the native ad by configuring the settings in the **Ad mediation** section on the **Monetize with ads** page for your app in the dashboard. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks. For more information, see [Monetize with ads](../publish/monetize-with-ads.md).

4.  In your code, replace the test ad unit values (that is, the *applicationId* and *adUnitId* parameters of the **NativeAdsManager** constructor) with the live values you generated in Dev Center.

5.  [Submit your app](../publish/app-submissions.md) to the Store using the Dev Center dashboard.

6.  Review your [advertising performance reports](../publish/advertising-performance-report.md) in the Dev Center dashboard.

<span id="requirements-and-guidelines" />
## Requirements and guidelines

Native ads give you have a lot of control over how you present advertising content to your users. Follow these requirements and guidelines to help ensure that the advertiser's message reaches your users while also helping to avoid delivering a confusing native ads experience to your users.

### Register the container for your native ad

In your code, you must call the **RegisterAdContainer** method of the **NativeAd** object to register the UI element that acts as a container for the native ad and optionally any specific controls that you want to register as clickable targets for the ad. This is required to properly track ad impressions and clicks.

There are two overloads for the **RegisterAdContainer** method that you can use:

* If you want the entire container for all the individual native ad elements to be clickable, call the **RegisterAdContainer(FrameworkElement)** method and pass the container control to the method. For example, if you display all of the native ad elements in separate controls that are all hosted in a **StackPanel** and you want the entire **StackPanel** to be clickable, pass the **StackPanel** to this method.

* If you want only certain native ad elements to be clickable, call the **RegisterAdContainer(FrameworkElement, IVector(FrameworkElement))** method. Only the controls that you pass to the second parameter will be clickable.

### Required native ad elements

At a minimum, you must always show the following native ad elements to the user in your native ad design. If you fail to include these elements, you may see poor performance and low yields for your ad unit.

1. Always display the title of the native ad (available in the **Title** property of the **NativeAd** object). Provide enough space to display at least 25 characters. If the title is longer, replace the additional text with an ellipsis.
2. Always display least one of the following elements to help differentiate the native ad experience from the rest of your app and clearly call out that the content is provided by an advertiser:
  * The distinguishable *ad* icon (available in the **AdIcon** property of the **NativeAd** object). This icon is supplied by Microsoft.
  * The *sponsored by* text (available in the **SponsoredBy** property of the **NativeAd** object). This text is supplied by the advertiser.
  * As an alternative to the *sponsored by* text, you can choose to display some other text that helps differentiate the native ad experience from the rest of your app, such as "Sponsored content", "Promotional content", "Recommended content", etc.

### User experience

Your native ad should be clearly delineated from the rest of your app and have space around it to prevent accidental clicks. Use borders, different backgrounds, or some other UI to separate the ad content from the rest of your app. Keep in mind that accidental ad clicks are not beneficial for your ad-based revenue or your end user experience in the long term.

### Description

If you choose to show the description for the ad (available in the **Description** property of the **NativeAd** object), provide enough space to display at least 75 characters. We recommend that you use an animation to show the full content of the ad description.

### Call to action

The *call to action* text (available in the **CallToAction** property of the **NativeAd** object) is a critical component of the ad. If you choose to show this text, follow these guidelines:

* Always display the *call to action* text to the user on a clickable control such as a button or hyperlink.
* Always display the *call to action* text in its entirety.
* Ensure that that the *call to action* text is separated from the rest of the promotional text from the advertiser.

### Learn and optimize

We recommend that you create and use different ad units for each different native ad placement in your app. This enables you to get separate reporting data for each native ad placement, and you can use this data to make changes that optimize the performance of each native ad placement.
