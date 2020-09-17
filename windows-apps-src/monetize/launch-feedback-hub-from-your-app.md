---
Description: You can encourage your customers to leave feedback by launching Feedback Hub from your app.
title: Launch Feedback Hub from your app
ms.assetid: 070B9CA4-6D70-4116-9B18-FBF246716EF0
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Feedback Hub, launch
ms.localizationpriority: medium
---
# Launch Feedback Hub from your app

You can encourage your customers to leave feedback by adding a control (such as a button) to your Universal Windows Platform (UWP) app that launches Feedback Hub. Feedback Hub is a preinstalled app that provides a single place to gather feedback on Windows and installed apps. All customer feedback that is submitted for your app through Feedback Hub is collected and presented to you in the [Feedback report](../publish/feedback-report.md) in Partner Center, so you can see the problems, suggestions, and upvotes that your customers have submitted in one report.

To launch Feedback Hub from your app, use an API that is provided by the [Microsoft Store Services SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftStoreServicesSDK). We recommend that you use this API to launch Feedback Hub from a UI element in your app that follows our design guidelines.

> [!NOTE]
> Feedback Hub is available only on devices that run version 10.0.14271 or later of a Windows 10 OS that is based on the desktop and mobile [device families](../get-started/universal-application-platform-guide.md). We recommend that you show a feedback control in your app only if the Feedback Hub is available on the user's device. The code in this topic demonstrates how to do this.

## How to launch Feedback Hub from your app

To launch Feedback Hub from your app:

1. [Install the Microsoft Store Services SDK](microsoft-store-services-sdk.md#install-the-sdk).
2. Open your project in Visual Studio.
3. In Solution Explorer, right-click the **References** node for your project and click **Add Reference**.
4. In **Reference Manager**, expand **Universal Windows** and click **Extensions**.
5. In the list of SDKs, click the check box next to **Microsoft Engagement Framework** and click **OK**.
6. In your project, add the control that you want to show to users to launch Feedback Hub, such as a button. We recommend that you configure the control as follows:
  * Set the font of the content shown in the control to **Segoe MDL2 Assets**.
  * Set the text in the control to the hexadecimal Unicode character code E939. This is the character code for the recommended feedback icon in the **Segoe MDL2 Assets** font.
  * Set the visibility of the control to hidden.
    > [!NOTE]
    > We recommend that you hide your feedback control by default and show it in your initialization code only if the Feedback Hub is available on the user's device. The next step demonstrates how to do this.

    The following code demonstrates the XAML definition of a [Button](/uwp/api/Windows.UI.Xaml.Controls.Button) that is configured as described above.

    ```XML
    <Button x:Name="feedbackButton" FontFamily="Segoe MDL2 Assets" Content="&#xE939;" HorizontalAlignment="Left" Margin="138,352,0,0" VerticalAlignment="Top" Visibility="Collapsed"  Click="feedbackButton_Click"/>
    ```

7. In your initialization code for the app page that hosts your feedback control, use the static [IsSupported](/uwp/api/microsoft.services.store.engagement.storeservicesfeedbacklauncher.issupported) method of the [StoreServicesFeedbackLauncher](/uwp/api/microsoft.services.store.engagement.storeservicesfeedbacklauncher) class to determine whether the Feedback Hub is available on the user's device. Feedback Hub is available only on devices that run version 10.0.14271 or later of a Windows 10 OS that is based on the desktop and mobile [device families](../get-started/universal-application-platform-guide.md).

    If this property returns **true**, make the control visible. The following code demonstrates how to do this for a [Button](/uwp/api/windows.ui.xaml.controls.button).

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/FeedbackPage.xaml.cs" id="ToggleFeedbackVisibility":::
      > [!NOTE]
      > Although Feedback Hub is not supported on Xbox devices at this time, the **IsSupported** property currently returns **true** on Xbox devices that run version 10.0.14271 or later of Windows 10. This is a known issue that will be fixed in a future release of the Microsoft Store Services SDK.  

8. In the event handler that runs when the user clicks the control, get a [StoreServicesFeedbackLauncher](/uwp/api/microsoft.services.store.engagement.storeservicesfeedbacklauncher) object and call the [LaunchAsync](/uwp/api/microsoft.services.store.engagement.storeservicesfeedbacklauncher.launchasync) method to launch the Feedback Hub app. There are two overloads for this method: one without parameters, and another one that accepts a dictionary of key and value pairs that contain metadata that you want to associate with the feedback. The following example demonstrates how to launch Feedback Hub in the [Click](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event handler for a [Button](/uwp/api/Windows.UI.Xaml.Controls.Button).

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/FeedbackPage.xaml.cs" id="FeedbackButtonClick":::

## Design recommendations for your feedback UI

To launch Feedback Hub, we recommend that you add a UI element in your app (such as a button) that displays the following standard feedback icon from the Segoe MDL2 Assets font and the character code E939.

![Feedback icon](images/feedback_icon.PNG)

We also recommend that you use one or more of the following placement options for linking to Feedback Hub in your app.
* **Directly in the app bar**. Depending on your implementation, you may wish to use the icon only or add text (as shown below).

  ![Feedback icon](images/feedback_appbar_placement.png)

* **In your app's settings**. This is a more subtle way to provide access to Feedback Hub. In the example below, the Feedback link appears as one of the links under App.

  ![Feedback icon](images/feedback_settings_placement.png)

* **In an event-driven flyout**. This is useful when you want to query your customers about a specific question before launching into the Windows Feedback Hub. For example, after your app uses a certain feature, you might prompt the customer with a specific question about their satisfaction with that feature. If the customer chooses to respond, your app launches Feedback Hub.


## Related topics

* [Feedback report](../publish/feedback-report.md)
