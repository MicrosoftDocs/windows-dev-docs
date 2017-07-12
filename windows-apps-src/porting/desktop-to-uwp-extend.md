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
---

# Extend your desktop application with modern UWP components

Before you begin, decide whether your Windows 10 experience requires a separate UWP project. Most of them don't. To create many Windows 10 experiences, you can call UWP APIs directly from your desktop application. We often use the term "enhance" to describe that process, and you can read more about it here: [Enhance for Windows 10](desktop-to-uwp-enhance.md).

 Users won't know this though, and you can create a seamless connection with that UI by using an interprocess communication mechanism called app services.  

 This sample shows you how to "extend" your desktop application with a modern map control. We use the term "extend" because like most modern touch-enabled UIs, the map control must run in a UWP process, and because this process is separated from your desktop application process, it behaves more like an extension to your desktop application as oppose to an enhancement.

If you're ready, let's start.

## Show a modern XAML UI

As part of your application flow, you can incorporate modern XAML-based user interfaces into your desktop application. These user interfaces are naturally adaptive to different screen sizes and resolutions and support modern interactive models such as touch and ink.

For example, to help users visualize a location, you could use a modern map control and with a relatively small amount of XAML markup, you can present users with powerful map-related visualization features. This image shows a VB6 application that opens a XAML-based modern UI that contains a map control.

![adaptive-design](images\desktop-to-uwp\extend-xaml-ui.png)

### Check it out

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Add-a-XAML-UI-and-Toast-Notification-to-a-VB6-Application-OsJHC7WhD_8006218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/vb6-app-with-xaml-sample/9n191ncxf2f6)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/VB6withXaml)

### The design pattern

To show a XAML-based UI, do these things:

:one: [Add a UWP project to your solution](#project).

:two: [Add a protocol extension to that project](#protocol).

:three: [Start the UWP app from your desktop app](#start).

:four: [Parse those parameters in the UWP project and show the page that you want](#parse).

<span id="project" />
### Add a UWP project

Add a **Blank App (Universal Windows)** project to your solution.

<span id="protocol" />
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

<span id="start" />
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

```
Private Declare Function LaunchMap Lib "UWPWrappers.dll" _
  (ByVal lat As Double, ByVal lon As Double) As Boolean
 
Private Sub EiffelTower_Click()
    LaunchMap 48.858222, 2.2945
End Sub
```

Here's the C++ function:

```c++

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
<span id="parse" />
### Parse parameters and show a page

In the **App** class of your UWP project, override the **OnActivated** event handler. If the app is activated by your protocol, parse the parameters and then open the page that you want.

```c++
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

## Exposing a UWP app service

Description, Image & Key Highlights

### Check it out

Watch a demo, run the app, explore the code.

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Expose-an-AppService-from-a-Windows-Forms-Data-Application-GiqNS7WhD_706218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/winforms-appservice/9p7d9b6nk5tn)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/WinformsAppService)

### Similar Samples

[App service bridge sample](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/AppServiceBridgeSample)

[App service bridge sample with C++ win32 app](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/AppServiceBridgeSample_C%2B%2B)


## Making your PC software a share target

Description, Image & Key Highlights

### Check it out

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Make-a-WPF-Application-a-Share-Target-xd6Fu6WhD_8406218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/wpf-app-as-sharetarget/9pjcjljlck37)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/WPFasShareTarget)

## Add a background task

Description, Image & Key Highlights

### Check it out

:heavy_check_mark: [Watch a video](https://mva.microsoft.com/en-US/training-courses/developers-guide-to-the-desktop-bridge-17373/Demo-Extend-an-MFC-Client-Application-to-Receive-Push-Notifications-from-a-Server-pneoh7WhD_2506218965)

:heavy_check_mark: [Get the app](https://www.microsoft.com/en-us/store/p/mfc-app-with-push-notification/9nrhrdq505qv)

:heavy_check_mark: [Browse the code](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/MFCwithPush)

### Similar Samples

[Background tasks sample](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/BackgroundTasksSample)

[Desktop Bridge OPOS scale sample](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopBridgeOPOSscale)

## See Also

[Calling WinRT components from a Win32 process via the Desktop Bridge](https://blogs.windows.com/buildingapps/2017/07/06/calling-winrt-components-win32-process-via-desktop-bridge/#d7odgh6HgehxC1tx.97)

## Support and feedback

**Find answers to specific questions**

Our team monitors these [StackOverflow tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge).

**Give feedback or make feature suggestions**

See [UserVoice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial)

**Give feedback about this article**

Use the comments section below.
