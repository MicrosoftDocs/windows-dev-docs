---
title: Launch the Default Apps settings page
description: Learn how to launch the Windows Settings app to display the Default Apps settings page from your app using the ms-settings URI scheme.
ms.assetid: a1819f4b-af98-4366-b2de-a7aea26da3a9
ms.date: 06/21/2024
ms.topic: article
keywords: windows, windows 11, uwp, default apps, windows app sdk
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# Launch the Default Apps settings page

Learn how to launch the Windows Settings app to display the Default Apps settings page from your app using the ms-settings URI scheme.

Windows defines a set of URIs that allow apps to launch the Windows Settings app and display a particular settings page. This article explains how to launch the Windows Settings app directly to the Default Apps settings page and, optionally, navigate directly to the settings for a specified default application. For more information, see [Launch the Windows Settings app](launch-settings-app.md).

## The Default Apps settings URL

`ms-settings:defaultapps` launches the Windows Settings app and navigates to the Default Apps settings page. Starting with Windows 11, version 21H2 (with 2023-04 Cumulative Update), 22H2 (with 2023-04 Cumulative Update), and 23H2 or later, you can append an additional query string parameter in escaped URI format to launch directly to the settings page for a specific application.

There are three query string parameters. The query string parameter to be used depends on how the application was installed.

| Query string parameter | Value to pass |
|--------|--------|--------|
| registeredAppUser | Named value from HKEY_CURRENT_USER\Software\RegisteredApplications<br/><br/>Use when the app was installed per user, and the registration for the app was written to HKEY_CURRENT_USER\Software\RegisteredApplications. |
| registeredAppMachine | Named value from HKEY_LOCAL_MACHINE\Software\RegisteredApplications<br/><br/>Use when the app was installed per machine, and the registration for the app was written to HKEY_LOCAL_MACHINE\Software\RegisteredApplications. |
| registeredAUMID | Application User Model ID <br/><br/>Use when the app was registered with Package Manager using a manifest declaring that the app handles File Types ([uap:FileTypeAssociation](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation)) or URI schemes ([uap:Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol)). |

>[!NOTE]
>To get the registeredAUMID query string parameter to work after an OS upgrade, an app may need to increment its TargetDeviceFamily...MaxVersionTested value in its manifest. This will ensure that the app is reindexed for the user, which in turn will update the appropriate definitions used to process the deep link via protocol activation. MaxVersionTested should be updated to `10.0.22000.1817` for Windows 11, version 21H2 or `10.0.22621.1555` for Windows 11, version 22H2.

In the following example, `LaunchUriAsync` is called to launch the Windows Settings app. The `ms-settings:defaultapps` Uri specifies that the Default Apps settings page should be shown. Next, the app that should be launched is determined. As an example, “Microsoft Edge” was registered by the app in HKEY_LOCAL_MACHINE\Software\RegisteredApplications. Since it is a per machine installed app, `registeredAppMachine` is the query string parameter that should be used. The optional query string parameter `registeredAppMachine` is set to the registered name, escaped with a call to `Url.EscapeDataString`, to specify that the page for **Microsoft Edge** should be shown.

```csharp
private async void LaunchSettingsPage_Click(object sender, RoutedEventArgs e)
{
    bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:defaultapps?registeredAppMachine=" + Uri.EscapeDataString(("Microsoft Edge")));
}
```

```cppwinrt
bool result = co_await Windows::System::Launcher::LaunchUriAsync(Windows::Foundation::Uri(L"ms-settings:defaultapps?registeredAppMachine=" + Uri::EscapeDataString(L"Microsoft Edge")));
```

## See also

[Launch the Windows Settings app](launch-settings-app.md)

[Launch the default app for a URI](launch-default-app.md)
