---
ms.assetid: cf0d2709-21a1-4d56-9341-d4897e405f5d
title: Error handling in XAML/C# walkthrough
description: Learn how to catch and handle errors from an AdControl in a XAML/C# app by following this walkthrough.
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, error handling, XAML, c#
ms.localizationpriority: medium
---
# Error handling in XAML/C# walkthrough

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This walkthrough demonstrates how to catch ad-related errors in your app. This walkthrough uses an [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) to display a banner ad, but the general concepts in it also apply to interstitial ads and native ads.

These examples assume that you have a XAML/C# app that contains an **AdControl**. For step-by-step instructions that demonstrate how to add an **AdControl** to your app, see [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md). 

1.  In your MainPage.xaml file, locate the definition for the **AdControl**. That code looks like this.
    ``` xml
    <UI:AdControl
      ApplicationId="3f83fe91-d6be-434d-a0ae-7351c5a997f1"
      AdUnitId="test"
      HorizontalAlignment="Left"
      Height="250"
      Margin="10,10,0,0"
      VerticalAlignment="Top"
      Width="300" />
    ```

2.   After the **Width** property, but before the closing tag, assign a name of an error event handler to the [ErrorOccurred](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.erroroccurred) event. In this walkthrough, the name of the error event handler is **OnAdError**.
    ``` xml
    <UI:AdControl
      ApplicationId="3f83fe91-d6be-434d-a0ae-7351c5a997f1"
      AdUnitId="test"
      HorizontalAlignment="Left"
      Height="250"
      Margin="10,10,0,0"
      VerticalAlignment="Top"
      Width="300"
      ErrorOccurred="OnAdError"/>
    ```

3.  To generate an error at runtime, create a second **AdControl** with a different application ID. Because all **AdControl** objects in an app must use the same application ID, creating an additional **AdControl** with a different application id will throw an error.

    Define a second **AdControl** in MainPage.xaml just after the first **AdControl**, and set the [ApplicationId](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.applicationid) property to zero (“0”).
    ``` xml
    <UI:AdControl
        ApplicationId="0"
        AdUnitId="test"
        HorizontalAlignment="Left"
        Height="250"
        Margin="10,265,0,0"
        VerticalAlignment="Top"
        Width="300"
        ErrorOccurred="OnAdError" />
    ```

4.  In MainPage.xaml.cs, add the following **OnAdError** event handler to the **MainPage** class. This event handler writes information to the Visual Studio **Output** window.
    ``` csharp
    private void OnAdError(object sender, AdErrorEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("AdControl error (" + ((AdControl)sender).Name +
            "): " + e.ErrorMessage + " ErrorCode: " + e.ErrorCode.ToString());
    }
    ```

4.  Build and run the project. After the app is running, you will see a message similar to the one below in the **Output** window of Visual Studio.
    ```json
    AdControl error (): MicrosoftAdvertising.Shared.AdException: all ad requests must use the same application ID within a single application (0, d25517cb-12d4-4699-8bdc-52040c712cab) ErrorCode: ClientConfiguration
    ```

## Related topics

* [Advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising)