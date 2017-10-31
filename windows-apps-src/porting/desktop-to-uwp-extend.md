---
author: normesta
Description: Extend your desktop application with Windows UIs and components
Search.Product: eADQiWindows 10XVcnh
title: Extend your desktop application with Windows UIs and components
ms.author: normesta
ms.date: 07/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Extend your desktop application with modern UWP components

Some Windows 10 experiences (For example: a touch-enabled UI page) must run inside of a modern app container . If you want to add these experiences, extend your desktop application with UWP component.

In many cases you can call UWP APIs directly from your desktop application, so before you review this guide, see [Enhance for Windows 10](desktop-to-uwp-enhance.md).

>[!NOTE]
>This guide assumes that you've created a Windows app package for your desktop application by using the Desktop Bridge. If you haven't yet done this, see [Desktop Bridge](desktop-to-uwp-root.md).

If you're ready, let's start.

## Show a modern XAML UI

As part of your application flow, you can incorporate modern XAML-based user interfaces into your desktop application. These user interfaces are naturally adaptive to different screen sizes and resolutions and support modern interactive models such as touch and ink.

For example, with a small amount of XAML markup, you can give users with powerful map-related visualization features.

This image shows a VB6 application that opens a XAML-based modern UI that contains a map control.

![adaptive-design](images\desktop-to-uwp\extend-xaml-ui.png)

### Have a closer look at this app

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Add-a-XAML-UI-and-Toast-Notification-to-a-VB6-Application-OsJHC7WhD_8006218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/vb6-app-with-xaml-sample/9n191ncxf2f6)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/VB6withXaml)

### The design pattern

To show a XAML-based UI, do these things:

:one: [Add a UWP project to your solution](#project)

:two: [Add a protocol extension to that project](#protocol)

:three: [Start the UWP app from your desktop app](#start)

:four: [In the UWP project, show the page that you want](#parse)

<a id="project" />
### Add a UWP project

Add a **Blank App (Universal Windows)** project to your solution.

<a id="protocol" />
### Add a protocol extension

In **Solution Explorer**, open the **package.appxmanifest** file of the project and add the extension.

```xml
<Extensions>
      <uap:Extension
          Category="windows.protocol"
          Executable="MapUI.exe"
          EntryPoint=" MapUI.App">
        <uap:Protocol Name="desktopbridgemapsample" />
      </uap:Extension>
    </Extensions>     
```

Give the protocol a name, provide the name of the executable produced by the UWP project, and the name of the entry point class.

You can also open the **package.appxmanifest** in the designer, choose the **Declarations** tab, and then add the extension there.

![declarations-tab](images\desktop-to-uwp\protocol-properties.png)



> [!NOTE]
> Map controls download data from the internet so if you use one, you'll have to add the "internet client" capability to your manifest as well.

<a id="start" />
### Start the UWP app

First, create a [Uri](https://msdn.microsoft.com/library/system.uri.aspx) that includes the protocol name and any parameters you want to pass into the UWP app. Then, call the [LaunchUriAsync](https://docs.microsoft.com/uwp/api/windows.system.launcher#Windows_System_Launcher_LaunchUriAsync_Windows_Foundation_Uri_) method.

Here's a basic example in C#.

```csharp

private async void showMap(double lat, double lon)
{
    string str = "desktopbridgemapsample://";

    Uri uri = new Uri(str + "location?lat=" +
        lat.ToString() + "&?lon=" + lon.ToString());

    var success = await Windows.System.Launcher.LaunchUriAsync(uri);

    if (success)
    {
        // URI launched
    }
    else
    {
        // URI launch failed
    }
}
```
In our sample, we're doing something a bit more indirect. We've wrapped the call in a VB6-callable interop function named ``LaunchMap``. That function is written by using C++.

Here's the VB block:

```VB
Private Declare Function LaunchMap Lib "UWPWrappers.dll" _
  (ByVal lat As Double, ByVal lon As Double) As Boolean
 
Private Sub EiffelTower_Click()
    LaunchMap 48.858222, 2.2945
End Sub
```

Here's the C++ function:

```C++

DllExport bool __stdcall LaunchMap(double lat, double lon)
{
  try
  {
    String ^str = ref new String(L"desktopbridgemapsample://");
    Uri ^uri = ref new Uri(
      str + L"location?lat=" + lat.ToString() + L"&?lon=" + lon.ToString());
 
    // now launch the UWP component
    Launcher::LaunchUriAsync(uri);
  }
  catch (Exception^ ex) { return false; }
  return true;
}

```

<a id="parse" />
### Parse parameters and show a page

In the **App** class of your UWP project, override the **OnActivated** event handler. If the app is activated by your protocol, parse the parameters and then open the page that you want.

```C++
void App::OnActivated(Windows::ApplicationModel::Activation::IActivatedEventArgs^ e)
{
  if (e->Kind == ActivationKind::Protocol)
  {
    ProtocolActivatedEventArgs^ protocolArgs = (ProtocolActivatedEventArgs^)e;
    Uri ^uri = protocolArgs->Uri;
    if (uri->SchemeName == "desktopbridgemapsample")
    {
      Frame ^rootFrame = ref new Frame();
      Window::Current->Content = rootFrame;
      rootFrame->Navigate(TypeName(MainPage::typeid), uri->Query);
      Window::Current->Activate();
    }
  }
}
```

### Similar Samples

[Northwind sample: End-to-end example for UWA UI & Win32 legacy code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/NorthwindSample)

[Northwind sample: UWP app connecting to SQL Server](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/SQLServer)

## Provide services to other apps

You add a service that other apps can consume. For example, you can add a service that gives other apps controlled access to the database behind your app. By implementing a background task, apps can reach the service even if your desktop app is not running.

Here's a sample that does this.

![adaptive-design](images\desktop-to-uwp\winforms-app-service.png)

### Have a closer look at this app

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Expose-an-AppService-from-a-Windows-Forms-Data-Application-GiqNS7WhD_706218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/winforms-appservice/9p7d9b6nk5tn)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/WinformsAppService)

### The design pattern

To show provide a service, do these things:

:one: [Add a Windows Runtime Component](#component)

:two: [Add an app service extension](#extension)

:three: [Implement the app service](#appservice)

:four: [Test the app service](#test)

<a id="component" />

### Add a Windows Runtime component

Add a **Windows Runtime Component (Universal Windows)** project to your solution.

Then, reference the project of that runtime component from your UWP packaging project.

<a id="extension" />
### Add an app service extension

In **Solution Explorer**, open the **package.appxmanifest** file of your packaging project and add an app service extension.

```xml
<Extensions>
      <uap:Extension
          Category="windows.appService"
          EntryPoint="MyAppService.AppServiceTask">
        <uap:AppService Name="com.microsoft.samples.winforms" />
      </uap:Extension>
    </Extensions>    
```

Give the app service a name and provide the name of the entry point class. This is the class that you'll use to implement your app service.

<a id="appservice" />
### Implement the app service

Here's where you'll validate and handle requests from other apps.

```csharp
public sealed class AppServiceTask : IBackgroundTask
{
    private BackgroundTaskDeferral backgroundTaskDeferral;
 
    public void Run(IBackgroundTaskInstance taskInstance)
    {
        this.backgroundTaskDeferral = taskInstance.GetDeferral();
        taskInstance.Canceled += OnTaskCanceled;
        var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
        details.AppServiceConnection.RequestReceived += OnRequestReceived;
    }
 
    private async void OnRequestReceived(AppServiceConnection sender,
                                         AppServiceRequestReceivedEventArgs args)
    {
        var messageDeferral = args.GetDeferral();
        ValueSet message = args.Request.Message;
        string id = message["ID"] as string;
        ValueSet returnData = DataBase.GetData(id);
        await args.Request.SendResponseAsync(returnData);
        messageDeferral.Complete();
    }
 
 
    private void OnTaskCanceled(IBackgroundTaskInstance sender,
                                BackgroundTaskCancellationReason reason)
    {
        if (this.backgroundTaskDeferral != null)
        {
            this.backgroundTaskDeferral.Complete();
        }
    }
}
```

<a id="test" />
### Test the app service

Test your service by calling it from another app.

```csharp
private async void button_Click(object sender, RoutedEventArgs e)
{
    AppServiceConnection dataService = new AppServiceConnection();
    dataService.AppServiceName = "com.microsoft.samples.winforms";
    dataService.PackageFamilyName = "Microsoft.SDKSamples.WinformWithAppService";
 
    var status = await dataService.OpenAsync();
    if (status == AppServiceConnectionStatus.Success)
    {
        string id = int.Parse(textBox.Text);
        var message = new ValueSet();
        message.Add("ID", id);
        AppServiceResponse response = await dataService.SendMessageAsync(message);
        string result = "";
 
        if (response.Status == AppServiceResponseStatus.Success)
        {
            if (response.Message["Status"] as string == "OK")
            {
                DisplayResult(response.Message["Result"]);
            }
        }
    }
}
```

Learn more about app services here: [Create and consume an app service](https://docs.microsoft.com/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service).

### Similar Samples

[App service bridge sample](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/AppServiceBridgeSample)

[App service bridge sample with C++ win32 app](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/AppServiceBridgeSample_C%2B%2B)

[MFC application that receives push notifications](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/MFCwithPush)


## Making your desktop application a share target

You can make your desktop application a share target so that users can easily share data such as pictures from other apps that support sharing.

For example, users could choose your app to share pictures from Microsoft Edge, the Photos app. Here's a WPF sample app that has that capability.

![share target](images\desktop-to-uwp\share-target.png)

### Have a closer look at this app

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Make-a-WPF-Application-a-Share-Target-xd6Fu6WhD_8406218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/wpf-app-as-sharetarget/9pjcjljlck37)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/WPFasShareTarget)

### The design pattern

To make your application a share target, do these things:

:one: [Add a UWP project to your solution](#project2)

:two: [Add a share target extension](#share-extension)

:three: [Override the OnNavigatedTo event handler](#override)

<a id="project2" />
### Add a UWP project to your solution

Add a **Blank App (Universal Windows)** project to your solution.

<a id="share-extension" />
### Add a share target extension

In **Solution Explorer**, open the **package.appxmanifest** file of the project and add the extension.

```xml
<Extensions>
      <uap:Extension
          Category="windows.shareTarget"
          Executable="ShareTarget.exe"
          EntryPoint="ShareTarget.App">
        <uap:ShareTarget>
          <uap:SupportedFileTypes>
            <uap:SupportsAnyFileType />
          </uap:SupportedFileTypes>
          <uap:DataFormat>Bitmap</uap:DataFormat>
        </uap:ShareTarget>
      </uap:Extension>
</Extensions>  
```

Provide the name of the executable produced by the UWP project, and the name of the entry point class. You'll also have to specify what types of files can be shared with your app.

<a id="override" />
### Override the OnNavigatedTo event handler

Override the **OnNavigatedTo** event handler in the **App** class of your UWP project.

This event handler is called when users choose your app to share their files.

```csharp
protected override async void OnNavigatedTo(NavigationEventArgs e)
{
  this.shareOperation = (ShareOperation)e.Parameter;
  if (this.shareOperation.Data.Contains(StandardDataFormats.StorageItems))
  {
      this.sharedStorageItems =
        await this.shareOperation.Data.GetStorageItemsAsync();
       
      foreach (StorageFile item in this.sharedStorageItems)
      {
          ProcessSharedFile(item);
      }
  }
}
```

## Support and feedback

**Find answers to your questions**

Have questions? Ask us on Stack Overflow. Our team monitors these [tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge).

**Give feedback or make feature suggestions**

See [UserVoice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial)
