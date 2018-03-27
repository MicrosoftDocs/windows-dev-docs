---
author: mcleanbyron
Description: Learn how to register your UWP app to receive push notifications that you send from Windows Dev Center.
title: Configure your app for targeted push notifications
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Microsoft Store Services SDK, targeted push notifications, Dev Center
ms.assetid: 30c832b7-5fbe-4852-957f-7941df8eb85a
ms.localizationpriority: high
---

# Configure your app for targeted push notifications

You can use the **Push notifications** page in the Windows Dev Center dashboard to directly engage with customers by sending targeted push notifications to the devices on which your Universal Windows Platform (UWP) app is installed. For example, you can use targeted push notifications to encourage your customers to take an action, such as rating your app or trying a new feature. You can send several different types of push notifications, including toast notifications, tile notifications, and raw XML notifications. You can also track the rate of app launches that resulted from your push notifications. For more information about this feature, see [Send push notifications to your app's customers](../publish/send-push-notifications-to-your-apps-customers.md).

Before you can send targeted push notifications to your customers from Dev Center, you must use a method of the [StoreServicesEngagementManager](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.aspx) class in the Microsoft Store Services SDK to register your app to receive notifications. You can use additional methods of this class to notify Dev Center that your app was launched in response to a targeted push notification (if you want to track the rate of app launches that resulted from your notifications) and to stop receiving notifications.

## Configure your project

Before you write any code, follow these steps to add a reference to the Microsoft Store Services SDK in your project:

1. If you have not done so already, [Install the Microsoft Store Services SDK](microsoft-store-services-sdk.md#install-the-sdk) on your development computer. 
2. Open your project in Visual Studio.
3. In Solution Explorer, right-click the **References** node for your project and click **Add Reference**.
4. In **Reference Manager**, expand **Universal Windows** and click **Extensions**.
5. In the list of SDKs, click the check box next to **Microsoft Engagement Framework** and click **OK**.

## Register for push notifications

To register your app to receive targeted push notifications from Dev Center:

1. In your project, locate a section of code that runs during startup in which you can register your app to receive notifications.
2. Add the following statement to the top of the code file.

    [!code-cs[DevCenterNotifications](./code/StoreSDKSamples/cs/DevCenterNotifications.cs#EngagementNamespace)]

3. Get a [StoreServicesEngagementManager](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.aspx) object and call one of the [RegisterNotificationChannelAsync](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.registernotificationchannelasync.aspx) overloads in the startup code you identified earlier. This method should be called each time that your app is launched.

  * If you want Dev Center to create its own channel URI for the notifications, call the [RegisterNotificationChannelAsync()](https://msdn.microsoft.com/library/windows/apps/mt771190.aspx) overload.

      [!code-cs[DevCenterNotifications](./code/StoreSDKSamples/cs/DevCenterNotifications.cs#RegisterNotificationChannelAsync1)]
      > [!IMPORTANT]
      > If your app also calls [CreatePushNotificationChannelForApplicationAsync](https://docs.microsoft.com/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanager.createpushnotificationchannelforapplicationasync) to create a notification channel for WNS, make sure that your code does not call [CreatePushNotificationChannelForApplicationAsync](https://docs.microsoft.com/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanager.createpushnotificationchannelforapplicationasync) and the [RegisterNotificationChannelAsync()](https://msdn.microsoft.com/library/windows/apps/mt771190.aspx) overload simultaneously. If you need to call both of these methods, make sure that you call them sequentially and await the return of one method before calling the other.

  * If you want to specify the channel URI to use for targeted push notifications from Dev Center, call the [RegisterNotificationChannelAsync(StoreServicesNotificationChannelParameters)](https://msdn.microsoft.com/library/windows/apps/mt771191.aspx) overload. For example, might want to do this if your app already uses Windows Push Notification Services (WNS) and you want to use the same channel URI. You must first create a [StoreServicesNotificationChannelParameters](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesnotificationchannelparameters.aspx) object and assign the [CustomNotificationChannelUri](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesnotificationchannelparameters.customnotificationchanneluri.aspx) property to your channel URI.

      [!code-cs[DevCenterNotifications](./code/StoreSDKSamples/cs/DevCenterNotifications.cs#RegisterNotificationChannelAsync2)]

> [!NOTE]
> When you call the **RegisterNotificationChannelAsync** method, a file named MicrosoftStoreEngagementSDKId.txt is created in the local app data store for your app (the folder returned by the [ApplicationData.LocalFolder](https://docs.microsoft.com/uwp/api/Windows.Storage.ApplicationData.LocalFolder) property). This file contains an ID that is used by the targeted push notifications infrastructure. Make sure that your app does not modify or delete this file. Otherwise, your users may receive multiple instances of notifications, or the notifications may not behave properly in other ways.

<span id="notification-customers" />

### How targeted push notifications are routed to customers

When your app calls **RegisterNotificationChannelAsync**, this method collects the Microsoft account of the customer who is currently signed in to the device. Later, when you send a targeted push notification to a segment that includes this customer, Dev Center sends the notification to devices that are associated with this customer's Microsoft account.

If the customer who started your app gives their device to someone else to use while they are still signed in to the device with their Microsoft account, be aware that the other person may see the notification that was targeted at the original customer. This can have unintended consequences, particularly for apps that offer services that customers can sign in to use. To prevent other users from seeing your targeted notifications in this scenario, call the [UnregisterNotificationChannelAsync](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.unregisternotificationchannelasync) method when customers sign out of your app. For more information, see [Unregister for push notifications](#unregister) later in this article.

### How your app responds when the user launches your app

After your app is registered to receive notifications and you [send a push notification to your app's customers from Dev Center](../publish/send-push-notifications-to-your-apps-customers.md), one of the following entry points in your app will be called when the user launches your app in response to your push notification. If you have some code that you want to run when the user launches your app, you can add the code to one of these entry points in your app.

  * If the push notification has a foreground activation type, override the [OnActivated](https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.onactivated) method of the **App** class in your project and add your code to this method.

  * If the push notification has a background activation type, add your code to the [Run](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method for your [background task](../launch-resume/support-your-app-with-background-tasks.md).

For example, you might want to reward the users of your app that have purchased any paid add-ons in your app by granting them a free add-on. In this case, you can send a push notification to a [customer segment](../publish/create-customer-segments.md) that targets these users. Then, you can add code to grant them a free [in-app purchase](in-app-purchases-and-trials.md) in one of the entry points listed above.

## Notify Dev Center of your app launch

If you select the **Track app launch rate** option for your targeted push notification in Dev Center, call the [ParseArgumentsAndTrackAppLaunch](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.parseargumentsandtrackapplaunch.aspx) method from the appropriate entry point in your app to notify Dev Center that your app was launched in response to a push notification.

This method also returns the original launch arguments for your app. When you choose to track the app launch rate for your push notification, an opaque tracking ID is added to the launch arguments to help track the app launch in Dev Center. You must pass the launch arguments for your app to the [ParseArgumentsAndTrackAppLaunch](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.parseargumentsandtrackapplaunch.aspx) method, and this method sends the tracking ID to Dev Center, removes the tracking ID from the launch arguments, and returns the original launch arguments to your code.

The way you call this method depends on the activation type of the push notification:

* If the push notification has a foreground activation type, call this method from the [OnActivated](https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.onactivated) method override in your app and pass the arguments that are available in the [ToastNotificationActivatedEventArgs](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs) object that is passed to this method. The following code example assumes that your code file has **using** statements for the **Microsoft.Services.Store.Engagement** and  **Windows.ApplicationModel.Activation** namespaces.

  [!code-cs[DevCenterNotifications](./code/StoreSDKSamples/cs/App.xaml.cs#OnActivated)]

* If the push notification has a background activation type, call this method from the [Run](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method for your [background task](../launch-resume/support-your-app-with-background-tasks.md) and pass the arguments that are available in the [ToastNotificationActionTriggerDetail](https://docs.microsoft.com/uwp/api/Windows.UI.Notifications.ToastNotificationActionTriggerDetail) object that is passed to this method. The following code example assumes that your code file has **using** statements for the **Microsoft.Services.Store.Engagement**, **Windows.ApplicationModel.Background**, and **Windows.UI.Notifications** namespaces.

  [!code-cs[DevCenterNotifications](./code/StoreSDKSamples/cs/DevCenterNotifications.cs#Run)]

<span id="unregister" />

## Unregister for push notifications

If you want your app to stop receiving targeted push notifications from Dev Center, call the [UnregisterNotificationChannelAsync](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.unregisternotificationchannelasync) method.

[!code-cs[DevCenterNotifications](./code/StoreSDKSamples/cs/DevCenterNotifications.cs#UnregisterNotificationChannelAsync)]

Note that this method invalidates the channel that is being used for notifications so the app no longer receives push notifications from *any* services. After it has been closed, the channel cannot be used again for any services, including targeted push notifications from Dev Center and other notifications using WNS. To resume sending push notifications to this app, the app must request a new channel.

## Related topics

* [Send push notifications to your app's customers](../publish/send-push-notifications-to-your-apps-customers.md)
* [Windows Push Notification Services (WNS) overview](https://docs.microsoft.com/windows/uwp/design/shell/tiles-and-notifications/windows-push-notification-services--wns--overview)
* [How to request, create, and save a notification channel](https://docs.microsoft.com/previous-versions/windows/apps/hh868221(v=win.10))
* [Microsoft Store Services SDK](https://docs.microsoft.com/windows/uwp/monetize/microsoft-store-services-sdk)
