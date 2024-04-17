---
description: Discover how to integrate packaged apps with the Windows Share Sheet.
title: Integrate packaged apps with Windows Share Sheet
ms.topic: article
ms.date: 04/16/2024
ms.localizationpriority: medium
---

# Integrate packaged apps with Windows Share Sheet

The Windows Share Sheet is a system-provided UI that enables users to share content from your app with other apps. The Share Sheet is available in the Windows shell and is accessible from any app that is registered as a Share Target. The Share Sheet provides a consistent and familiar experience for users, and it's a great way to increase the discoverability of your app.

## What is Share Target?

Share Target is a feature that was introduced in Windows 8, and it allows an app to receive data from another app. Share Target works like a Clipboard but with dynamic content.

For the default share target registration to work with Win32 apps, the app needs to have a package identity and also handle the share arguments as `ShareTargetActivatedEventArgs`, which is a live object from the source app. It isn't a static memory content that is sent to the target app.

> [!NOTE]
> In a C++ app, use the [GetCurrentPackageFullName](/windows/win32/api/appmodel/nf-appmodel-getcurrentpackagefullname) API to check if the running app has package identity. The API returns the `APPMODEL_ERROR_NO_PACKAGE` error code if it isn't running with package identity.

## Prerequisites

To support `ShareTargetActivatedEventArgs`, the app must target Windows 10, version 2004 (build 10.0.19041.0) or later. This is the minimum target version for the feature.

## Register as a Share Target

There are two steps required to implement the Share contract in your app.

### Add a share target extension to appxmanifest

In Visual Studio's Solution Explorer, open the `package.appxmanifest` file of the Packaging project in your solution and add the share target extension.

```xml
<Extensions>
      <uap:Extension
          Category="windows.shareTarget">
        <uap:ShareTarget>
          <uap:SupportedFileTypes>
            <uap:SupportsAnyFileType />
          </uap:SupportedFileTypes>
          <uap:DataFormat>Bitmap</uap:DataFormat>
        </uap:ShareTarget>
      </uap:Extension>
</Extensions>
```

Add the supported data format that is supported by your application to the `DataFormat` configuration. In this case, the app supports sharing images, so the `DataFormat` is set to `Bitmap`.

### Fetch Share Event arguments

Starting in Windows 10, version 1809, packaged apps can call the `AppInstance.GetActivatedEventArgs` method to retrieve certain kinds of app activation info during startup. For example, you can call this method to get information about app activation; whether it was triggered by opening a file, clicking an interactive toast, or using a registered protocol.

However, `ShareTargetActivatedEventArgs` activation info is supported only on Windows 10, version 2004, and later. So, the application should target to devices with this specific minimum version.

To see a Windows App SDK implementation, see the `OnLaunched()` method in the [Share Target sample app](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp/WinUI-CS-ShareTargetSampleApp/App.xaml.cs).

For other packaged apps, in the `Main()` method of the application, check for `AppInstance.GetActivatedEventArgs()`. This is part of the `Windows.ApplicationModel` namespace.

```csharp
public static void Main(string[] cmdArgs)
{
    ...
    if (isRunningWithIdentity())
    {
        var activationArgs = AppInstance.GetActivatedEventArgs();
        if (activationArgs != null)
        {
            switch (activationArgs.Kind)
            {
                case ActivationKind.Launch:
                    HandleLaunch(activationArgs as LaunchActivatedEventArgs);
                    break;
                case ActivationKind.ToastNotification:
                    HandleToastNotification(activationArgs as ToastNotificationActivatedEventArgs);                                     
                    break;
                case ActivationKind.ShareTarget:
                    HandleShareAsync(activationArgs as ShareTargetActivatedEventArgs);
                    break;
                default:
                    HandleLaunch(null);
                    break;
            }
        }
    }
}
```

See the [Photo Store Demo](https://github.com/microsoft/AppModelSamples/blob/master/Samples/SparsePackages/PhotoStoreDemo/StartUp.cs) app for a complete implementation.

## Handle shared files

```csharp
static async void HandleShareAsync(ShareTargetActivatedEventArgs args)
{
    ShareOperation shareOperation = args.ShareOperation;
    shareOperation.ReportStarted();

    if (shareOperation.Data.Contains( 
        Windows.ApplicationModel.DataTransfer.StandardDataFormats.StorageItems))
    {
        try
        {
            IReadOnlyList<IStorageItem> items = await shareOperation.Data.GetStorageItemsAsync();
            var file = (IStorageFile)items[0]; 
            string path = file.Path;
            var image = new ImageFile(path);
            image.AddToCache();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    shareOperation.ReportCompleted();

    // app launch code
}
```

## See also

- [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
- [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)
- [Migrate from UWP to the Windows App SDK](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Advantages and Disadvantages of packaging an application - Deployment overview](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app)
- [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/)
- [Share Contract Implementation for WinAppSDK App](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [Share Contract Implementation for Sparse Packaged based Apps](https://github.com/microsoft/AppModelSamples/blob/master/Samples/SparsePackages/PhotoStoreDemo/StartUp.cs)
