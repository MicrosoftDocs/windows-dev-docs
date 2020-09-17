---
title: Convert an app service to run in the same process as its host app
description: Convert app service code that ran in a separate background process into code that runs inside the same process as your app service provider.
ms.date: 11/03/2017
ms.topic: article
keywords: windows 10, uwp, app service
ms.assetid: 30aef94b-1b83-4897-a2f1-afbb4349696a
ms.localizationpriority: medium
---
# Convert an app service to run in the same process as its host app

An [AppServiceConnection](/uwp/api/windows.applicationmodel.appservice.appserviceconnection) enables another application to wake up your app in the background and start a direct line of communication with it.

With the introduction of in-process App Services, two running foreground applications can have a direct line of communication via an app service connection. App Services can now run in the same process as the foreground application which makes communication between apps much easier and removes the need to separate the service code into a separate project.

Turning an out-of-process model App Service into an in-process model requires two changes. The first is a manifest change.

> ```xml
> <Package
>    ...
>   <Applications>
>       <Application Id=...
>           ...
>           EntryPoint="...">
>           <Extensions>
>               <uap:Extension Category="windows.appService">
>                   <uap:AppService Name="InProcessAppService" />
>               </uap:Extension>
>           </Extensions>
>           ...
>       </Application>
>   </Applications>
> ```

Remove the `EntryPoint` attribute from the `<Extension>` element because now [OnBackgroundActivated()](/uwp/api/windows.ui.xaml.application.onbackgroundactivated) is the entry point that will be used when the app service is invoked.

The second change is to move the service logic from its separate background task project into methods that can be called from **OnBackgroundActivated()**.

Now your application can directly run your App Service. For example, in App.xaml.cs:

[!NOTE] The code below is different than the one provided for example 1 (out-of-process service). The code below is provided for illustration purposes only and should not be used as part of example 2 (in-process service).  To continue the article’s transition from example 1 (out-of-process service) into example 2 (in-process service) continue to use the code provided  for example 1 instead of the illustrative code below.

``` cs
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
...

sealed partial class App : Application
{
  private AppServiceConnection _appServiceConnection;
  private BackgroundTaskDeferral _appServiceDeferral;

  ...

  protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
  {
      base.OnBackgroundActivated(args);
      IBackgroundTaskInstance taskInstance = args.TaskInstance;
      AppServiceTriggerDetails appService = taskInstance.TriggerDetails as AppServiceTriggerDetails;
      _appServiceDeferral = taskInstance.GetDeferral();
      taskInstance.Canceled += OnAppServicesCanceled;
      _appServiceConnection = appService.AppServiceConnection;
      _appServiceConnection.RequestReceived += OnAppServiceRequestReceived;
      _appServiceConnection.ServiceClosed += AppServiceConnection_ServiceClosed;
  }

  private async void OnAppServiceRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
  {
      AppServiceDeferral messageDeferral = args.GetDeferral();
      ValueSet message = args.Request.Message;
      string text = message["Request"] as string;

      if ("Value" == text)
      {
          ValueSet returnMessage = new ValueSet();
          returnMessage.Add("Response", "True");
          await args.Request.SendResponseAsync(returnMessage);
      }
      messageDeferral.Complete();
  }

  private void OnAppServicesCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
  {
      _appServiceDeferral.Complete();
  }

  private void AppServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
  {
      _appServiceDeferral.Complete();
  }
}
```

In the code above the `OnBackgroundActivated` method handles the app service activation. All of the events required for communication through an [AppServiceConnection](/uwp/api/windows.applicationmodel.appservice.appserviceconnection) are registered, and the task deferral object is stored so that it can be marked as complete when the communication between the applications is done.

When the app receives a request and reads the [ValueSet](/uwp/api/windows.foundation.collections.valueset) provided to see if the `Key` and `Value` strings are present. If they are present then the app service returns a pair of `Response` and `True` string values back to the app on the other side of the **AppServiceConnection**.

Learn more about connecting and communicating with other apps at [Create and Consume an App Service](./how-to-create-and-consume-an-app-service.md?f=255&MSPPError=-2147217396).