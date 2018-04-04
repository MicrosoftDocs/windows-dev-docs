---
author: QuinnRadich
title: Notifications in Windows Template Studio
description: All Windows Template Studio projects support toast notifications, hub notifications, and store notifications.
keywords: template, Windows Template Studio, template studio, notifications, toast, hub, store
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Notifications in Windows Template Studio

All Windows Template Studio projects support three different type of notifications for UWP: toast notifications, hub notifications, and store notifications. For more information about notifications in UWP apps, see [toast notifications in UWP](../shell/tiles-and-notifications/adaptive-interactive-toasts.md) and related docs.

## Toast notifications

ToastNotificationService is in change of sending notifications directly from code in the application. The service contains a method `ShowToastNotification` that sends a notification to the Windows action center. The feature code also includes a `ShowToastNotificationSample` class that shows how to create and send a notification from code.

ToastNotificationsService extends `ActivationHandler` to handle application activation from a toast notification. This code is not implemented on the template because the logic is application dependent. The following [ToastNotificationSample](https://github.com/Microsoft/WindowsTemplateStudio/tree/dev/samples/notifications/ToastNotificationSample) contains an example of one way to handle application activation from a toast notification.
Check out the [activation documentation](application-activation.md) to learn more about handling app activation.
The relevant parts of the sample app that handle activation are shown below.

```csharp
// ToastNotificationService.cs
protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
{
    // Handle the app activation from a ToastNotification
    NavigationService.Navigate<Views.ActivatedFromToastPage>(args);
    await Task.CompletedTask;
}

// ActivatedFromToastPage.xaml.cs
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    base.OnNavigatedTo(e);
    ViewModel.Initialize(e.Parameter as ToastNotificationActivatedEventArgs);
}

// ActivatedFromToastViewModel.cs
public void Initialize(ToastNotificationActivatedEventArgs args)
{
    // Check args looking for information about the toast notification
    if (args.Argument == "ToastButtonActivationArguments")
    {
        // ToastNotification was clicked on OK Button
        ActivationSource = "ActivationSourceButtonOk".GetLocalized();
    }
    else if(args.Argument == "ToastContentActivationParams")
    {
        // ToastNotification was clicked on main content
        ActivationSource = "ActivationSourceContent".GetLocalized();
    }
}
```

Full toast notification documentation for UWP [here](https://developer.microsoft.com/windows/uwp-community-toolkit/api/microsoft_toolkit_uwp_notifications_toastcontent).

## Hub notifications

HubNotificationsService is in change of configuring the application with the Azure notifications service to allow the application to receive push notifications from a remote service in Azure. The service contains the `InitializeAsync` method that sets up the Hub Notifications. You must specify the hub name and the access signature before start working with Hub Notifications. There is more documentation about how to create and connect an Azure notifications service [here](https://docs.microsoft.com/azure/app-service-mobile/app-service-mobile-windows-store-dotnet-get-started-push).

Toast Notifications sent from Azure notification service shoudl be handled in the same way as locally generated ones. See the above referenced [ToastNotificationSample](https://github.com/Microsoft/WindowsTemplateStudio/tree/dev/samples/notifications/ToastNotificationSample) for more.

## Store notifications

StoreNotificationsService is in change of configuring the application with the Windows Dev Center notifications service to allow the application to receive push notifications from Windows Dev Center remote service. The service contains the `InitializeAsync` method that sets up the Store Notifications. This feature use the Store API to configure the notifications.

See the official documentation on how to [configure your app for targeted push notifications](../../monetize/configure-your-app-to-receive-dev-center-notifications.md) and how to [send notifications to your app's customers](../../publish/send-push-notifications-to-your-apps-customers.md).

To handle app activation from a Toast Notification that is sent from Windows Dev Center service will need you to add a similar implementation to that detailed above. The key difference is to call `ParseArgumentsAndTrackAppLaunch` to notify the Windows Dev Center that the app was launched in response to a targeted push notification and to get the original arguments. An example of this is shown below.

```csharp
protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
{
    var toastActivationArgs = args as ToastNotificationActivatedEventArgs;

    StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
    string originalArgs = engagementManager.ParseArgumentsAndTrackAppLaunch(toastActivationArgs.Argument);

    //// Use the originalArgs variable to access the original arguments passed to the app.
    NavigationService.Navigate<Views.ActivatedFromStorePage>(originalArgs);

    await Task.CompletedTask;
}
```