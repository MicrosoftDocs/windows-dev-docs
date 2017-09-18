---
author: TylerMSFT
title: Convert an app service to run in the same process as its host app
description: Convert app service code that ran in a separate background process into code that runs inside the same process as your app service provider.
ms.author: twhitney
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 30aef94b-1b83-4897-a2f1-afbb4349696a
---

# Convert an app service to run in the same process as its host app

An [AppServiceConnection](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.appservice.appserviceconnection.aspx) enables another application to wake up your app in the background and start a direct line of communication with it.

With the introduction of in-process App Services, two running foreground applications can have a direct line of communication via an app service connection. App Services can now run in the same process as the foreground application which makes communication between apps much easier and removes the need to separate the service code into a separate project.

Turning an out-of-process model App Service into an in-process model requires two changes. The first is a manifest change.

> ```xml
>  <uap:Extension Category="windows.appService">
>          <uap:AppService Name="InProcessAppService" />
>  </uap:Extension>
> ```

Remove the `EntryPoint` attribute. Now the  [OnBackgroundActivated()](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.application.onbackgroundactivated.aspx) callback will be used as the callback method when the app service is invoked.

The second change is to move the service logic from its separate background task project into methods that can be called from **OnBackgroundActivated()**.

Now your application can directly run your App Service.  For example:

> ``` cs
> private AppServiceConnection appServiceConnection;
> private BackgroundTaskDeferral appServiceDeferral;
> protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
> {
>     base.OnBackgroundActivated(args);
>     IBackgroundTaskInstance taskInstance = args.TaskInstance;
>     AppServiceTriggerDetails appService = taskInstance.TriggerDetails as AppServiceTriggerDetails;
>     appServiceDeferral = taskInstance.GetDeferral();
>     taskInstance.Canceled += OnAppServicesCanceled;
>     appServiceConnection = appService.AppServiceConnection;
>     appServiceConnection.RequestReceived += OnAppServiceRequestReceived;
>     appServiceConnection.ServiceClosed += AppServiceConnection_ServiceClosed;
> }
>
> private void OnAppServiceRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
> {
>     AppServiceDeferral messageDeferral = args.GetDeferral();
>     ValueSet message = args.Request.Message;
>     string text = message["Request"] as string;
>              
>     if ("Value" == text)
>     {
>         ValueSet returnMessage = new ValueSet();
>         returnMessage.Add("Response", "True");
>         await args.Request.SendResponseAsync(returnMessage);
>     }
>     messageDeferral.Complete();
> }
>
> private void OnAppServicesCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
> {
>     appServiceDeferral.Complete();
> }
>
> private void AppServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
> {
>     appServiceDeferral.Complete();
> }
> ```

In the code above the `OnBackgroundActivated` method handles the app service activation. All of the events required for communication through an [AppServiceConnection](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.appservice.appserviceconnection.aspx) are registered, and the task deferral object is stored so that it can be marked as complete when the communication between the applications is done.

When the app receives a request and reads the [ValueSet](https://msdn.microsoft.com/library/windows/apps/windows.foundation.collections.valueset.aspx) provided to see if the `Key` and `Value` strings are present. If they are present then the app service returns a pair of `Response` and `True` string values back to the app on the other side of the **AppServiceConnection**.

Learn more about connecting and communicating with other apps at [Create and Consume an App Service](https://msdn.microsoft.com/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service?f=255&MSPPError=-2147217396).
