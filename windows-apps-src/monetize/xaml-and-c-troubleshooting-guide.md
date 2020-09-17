---
ms.assetid: 141900dd-f1d3-4432-ac8b-b98eaa0b0da2
description: Learn about solutions to common development issues with the Microsoft advertising libraries in XAML apps.
title: XAML and C# troubleshooting guide
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, AdControl, troubleshooting, XAML, c#
ms.localizationpriority: medium
---
# XAML and C# troubleshooting guide

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This topic contains solutions to common development issues with the Microsoft advertising libraries in XAML apps.

* [XAML](#xaml)
  * [AdControl not appearing](#xaml-notappearing)
  * [Black box blinks and disappears](#xaml-blackboxblinksdisappears)
  * [Ads not refreshing](#xaml-adsnotrefreshing)

* [C#](#csharp)
  * [AdControl not appearing](#csharp-adcontrolnotappearing)
  * [Black box blinks and disappears](#csharp-blackboxblinksdisappears)
  * [Ads not refreshing](#csharp-adsnotrefreshing)

<span id="xaml"/>

## XAML

<span id="xaml-notappearing"/>

### AdControl not appearing

1.  Ensure that the **Internet (Client)** capability is selected in Package.appxmanifest.

2.  Check the application ID and ad unit ID. These IDs must match the application ID and ad unit ID that you obtained in Partner Center. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <UI:AdControl AdUnitId="{AdUnitID}" ApplicationId="{ApplicationID}"
                  Width="728" Height="90" />
    ```

3.  Check the **Height** and **Width** properties. These must be set to one of the [Supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md).

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <UI:AdControl AdUnitId="{AdUnitID}"
                  ApplicationId="{ApplicationID}"
                  Width="728" Height="90" />
    ```

4.  Check the element position. The [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) must be inside the viewable area.

5.  Check the **Visibility** property. The optional **Visibility** property must not be set to collapsed or hidden. This property can be set inline (as shown below) or in an external style sheet.

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <UI:AdControl AdUnitId="{AdUnitID}"
                  ApplicationId="{ApplicationID}"
                  Visibility="Visible"
                  Width="728" Height="90" />
    ```

6.  Check the parent of the **AdControl**. If the **AdControl** element resides in a parent element, the parent must be active and visible.

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <StackPanel>
        <UI:AdControl AdUnitId="{AdUnitID}"
                      ApplicationId="{ApplicationID}"
                      Width="728" Height="90" />
    </StackPanel>
    ```

7.  Ensure the **AdControl** is not hidden from the viewport. The **AdControl** must be visible for ads to display properly.

8.  Live values for **ApplicationId** and **AdUnitId** should not be tested in the emulator. To ensure the **AdControl** is functioning as expected, use the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for both **ApplicationId** and **AdUnitId**.

<span id="xaml-blackboxblinksdisappears"/>

### Black box blinks and disappears

1.  Double-check all steps in the previous [AdControl not appearing](#xaml-notappearing) section.

2.  Handle the **ErrorOccurred** event, and use the message that is passed to the event handler to determine whether an error occurred and what type of error was thrown. See [Error handling in XAML/C# walkthrough](error-handling-in-xamlc-walkthrough.md) for more information.

    This example demonstrates an **ErrorOccurred** event handler. The first snippet is the XAML UI markup.

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <UI:AdControl AdUnitId="{AdUnitID}"
                  ApplicationId="{ApplicationID}"
                  Width="728" Height="90"
                  ErrorOccurred="adControl_ErrorOccurred" />
    <TextBlock x:Name="TextBlock1" TextWrapping="Wrap" Width="500" Height="250" />
    ```

    This example demonstrates the corresponding C# code.

    > [!div class="tabbedCodeSnippets"]
    ``` cs
    private void adControl_ErrorOccurred(object sender,               
        Microsoft.Advertising.WinRT.UI.AdErrorEventArgs e)
    {
        TextBlock1.Text = e.ErrorMessage;
    }
    ```

    The most common error that causes a black box is “No ad available.” This error means there is no ad available to return from the request.

3.  The [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) is behaving normally.

    By default, the **AdControl** will collapse when it cannot display an ad. If other elements are children of the same parent they may move to fill the gap of the collapsed **AdControl** and expand when the next request is made.

<span id="xaml-adsnotrefreshing"/>

### Ads not refreshing

1.  Check the [IsAutoRefreshEnabled](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.isautorefreshenabled) property. By default, this optional property is set to **True**. When set to **False**, the [Refresh](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.refresh) method must be used to retrieve another ad.

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <UI:AdControl AdUnitId="{AdUnitID}"
                  ApplicationId="{ApplicationID}"
                  Width="728" Height="90"
                  IsAutoRefreshEnabled="True" />
    ```

2.  Check calls to the **Refresh** method. When using automatic refresh, **Refresh** cannot be used to retrieve another ad. When using manual refresh, **Refresh** should be called only after a minimum of 30 to 60 seconds depending on the device’s current data connection.

    The following code snippets show an example of how to use the **Refresh** method. The first snippet is the XAML UI markup.

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <UI:AdControl x:Name="adControl1"
                  AdUnitId="{AdUnit_ID}"
                  ApplicationId="{ApplicationID}"
                  Width="728" Height="90"
                  IsAutoRefreshEnabled="False" />
    ```

    This code snippet shows an example of the C# code behind the UI markup.

    > [!div class="tabbedCodeSnippets"]
    ``` cs
    public RefreshAds()
    {
        var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(60) };
        timer.Tick += (s, e) => adControl1.Refresh();
        timer.Start();
    }
    ```

3.  The **AdControl** is behaving normally. Sometimes the same ad will appear more than once in a row giving the appearance that ads are not refreshing.

<span id="csharp"/>

## C\# #

<span id="csharp-adcontrolnotappearing"/>

### AdControl not appearing

1.  Ensure that the **Internet (Client)** capability is selected in Package.appxmanifest.

2.  Ensure the **AdControl** is instantiated. If the **AdControl** is not instantiated it will not be available.

    > [!div class="tabbedCodeSnippets"]
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdControlSamples/cs/MiscellaneousSnippets.cs" id="Snippet1":::

3.  Check the application ID and ad unit ID. These IDs must match the application ID and ad unit ID that you obtained in Partner Center. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

    > [!div class="tabbedCodeSnippets"]
    ``` cs
    adControl = new AdControl();
    adControl.ApplicationId = "{ApplicationID}";adControl.AdUnitId = "{AdUnitID}";
    adControl.Height = 90;
    adControl.Width = 728;
    ```

4.  Check the **Height** and **Width** parameters. These must be set to one of the [supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md).

    > [!div class="tabbedCodeSnippets"]
    ``` cs
    adControl = new AdControl();
    adControl.ApplicationId = "{ApplicationID}";
    adControl.AdUnitId = "{AdUnitID}";
    adControl.Height = 90;adControl.Width = 728;
    ```

5.  Ensure the **AdControl** is added to a parent element. To display, the **AdControl** must be added as a child to a parent control (for example, a **StackPanel** or **Grid**).

    > [!div class="tabbedCodeSnippets"]
    ``` cs
    ContentPanel.Children.Add(adControl);
    ```

6.  Check the **Margin** parameter. The **AdControl** must be inside the viewable area.

7.  Check the **Visibility** property. The optional **Visibility** property must be set to **Visible**.

    > [!div class="tabbedCodeSnippets"]
    ``` cs
    adControl = new AdControl();
    adControl.ApplicationId = "{ApplicationID}";
    adControl.AdUnitId = "{AdUnitID}";
    adControl.Height = 90;
    adControl.Width = 728;
    adControl.Visibility = System.Windows.Visibility.Visible;
    ```

8.  Check the parent of the **AdControl**. The parent must be active and visible.

9. Live values for **ApplicationId** and **AdUnitId** should not be tested in the emulator. To ensure the **AdControl** is functioning as expected, use the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for both **ApplicationId** and **AdUnitId**.

<span id="csharp-blackboxblinksdisappears"/>

### Black box blinks and disappears

1.  Double-check all steps in the [AdControl not appearing](#csharp-adcontrolnotappearing) section above.

2.  Handle the **ErrorOccurred** event, and use the message that is passed to the event handler to determine whether an error occurred and what type of error was thrown. See [Error handling in XAML/C# walkthrough](error-handling-in-xamlc-walkthrough.md) for more information.

    The following examples show the basic code needed to implement an error call. This XAML code defines a **TextBlock** that is used to display the error message.

    > [!div class="tabbedCodeSnippets"]
    ``` xml
    <TextBlock x:Name="TextBlock1" TextWrapping="Wrap" Width="500" Height="250" />
    ```

    This C# code retrieves the error message and displays it in the **TextBlock**.

    > [!div class="tabbedCodeSnippets"]
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdControlSamples/cs/MiscellaneousSnippets.cs" id="Snippet2":::

    The most common error that causes a black box is “No ad available.” This error means there is no ad available to return from the request.

3.  **AdControl** is behaving normally. Sometimes the same ad will appear more than once in a row giving the appearance that ads are not refreshing.

<span id="csharp-adsnotrefreshing"/>

### Ads not refreshing

1.  Check whether the [IsAutoRefreshEnabled](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.isautorefreshenabled.aspx) property of your **AdControl** is set to false. By default, this optional property is set to **true**. When set to **false**, the **Refresh** method must be used to retrieve another ad.

2.  Check calls to the [Refresh](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.refresh.aspx) method. When using automatic refresh (**IsAutoRefreshEnabled** is **true**), **Refresh** cannot be used to retrieve another ad. When using manual refresh (**IsAutoRefreshEnabled** is **false**), **Refresh** should be called only after a minimum of 30 to 60 seconds depending on the device’s current data connection.

    The following example demonstrates how to call the **Refresh** method.

    > [!div class="tabbedCodeSnippets"]
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdControlSamples/cs/MiscellaneousSnippets.cs" id="Snippet3":::

3.  The **AdControl** is behaving normally. Sometimes the same ad will appear more than once in a row giving the appearance that ads are not refreshing.

 

 
