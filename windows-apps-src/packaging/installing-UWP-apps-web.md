---
author: laurenhughes
title: Installing UWP apps from a web page
description: In this section, we will review the steps you need to take to allow users to install your apps directly from the web page.
ms.author: lahugh
ms.date: 11/16/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, app installer, AppInstaller, sideload, related set, optional packages
ms.localizationpriority: medium
---

# Installing UWP apps from a web page

Typically, an app needs to be locally available on a device before it can be installed with the App Installer. For the web scenario, this means that the user must download the app package from the web server, after which it can be installed with App Installer. This is inefficient and wastes disk space, which is why App Installer now has built in features to streamline the process.

App Installer can install an app directly from a web server. When the user clicks on an app package hosted web link, App Installer is invoked automatically. The user is then taken to the app info view in App Installer and is then one click away from engaging directly with the app. 

The direct app install is only available in the Windows 10 Fall Creators Update and newer. Previous versions of Windows (going back to the Windows 10 Anniversary Update) will be supported by the [web install experience on previous versions of Windows 10](#web-install-experience). This experience is not as fluid as the direct app install, but it provides significant improvements to the existing app install procedure.
  
> [!NOTE]
> App Installer version must be greater than 1.0.12271.0 to support this feature.

## Protocol Activation Scheme
In this mechanism, App Installer registers with the operating system for a protocol activation scheme. When user clicks on a web link, the browser checks with the OS for apps that are registered to that web link. If the scheme matches the protocol activation scheme specified by App Installer, then App Installer is invoked. It's important to note that this mechanism is browser independent. This is beneficial to site administrators, for example, who don't need to consider web browser differences while incorporating this into a webpage. 

### Requirements for protocol activation scheme

1. Web servers need to have support for byte range requests (HTTP/1.1)
    - Servers that support HTTP/1.1 protocol should have support for byte range requests 
2. Web servers will need to know about the Windows 10 app package content types
    - Here's how to declare the new content types as part of [web config file](web-install-IIS.md#step-7---configure-the-web-app-for-app-package-mime-types)

### How to enable this on a webpage 
App developers who want to host app packages on their web sites need to follow this step:

Prefix your app package URIs with the activation scheme `'ms-appinstaller:?source='` that App Installer is registered to when referencing them on your webpage. See the example for **MyApp Web Page** for details. 
``` html
<html>
    <body>
        <h1> MyApp Web Page </h1>
        <a href="ms-appinstaller:?source=http://mywebservice.azureedge.net/HubApp.appx"> Install app package </a>
        <a href="ms-appinstaller:?source=http://mywebservice.azureedge.net/HubAppBundle.appxbundle"> Install app bundle  </a>
        <a href="ms-appinstaller:?source=http://mywebservice.azureedge.net/HubAppSet.appinstaller"> Install related set </a>
    </body>
</html>
```

## Signing the app package
For users to install your app, you will need to sign the app package with a trusted certificate. You can use a third party paid certificate from a trusted certification authority to sign your app package. If a third party certificate is used, the user will need to have their device in either sideload or developer mode to install and run your app.

If you are deploying an app to employees within an enterprise, you can use an enterprise issued certificate to sign the app. It's important to note that the enterprise certificate must be deployed to any devices which the app will be installed on. For more information on deploying enterprise apps, see [Enterprise app management](https://docs.microsoft.com/windows/client-management/mdm/enterprise-app-management).

## Web install experience on previous versions of Windows 10<a name="web-install-experience"></a>

Invoking App Installer from the browser is supported on all versions of Windows 10 where App Installer is available (starting with the Anniversary Update). However, the functionality to install directly from the web without the need to download the package first is only available on the Windows 10 Fall Creators Update.  

Users of previous versions of Windows 10 (with App Installer available) can also take advantage of web install of UWP apps via App Installer, but will have a different user experience. When these users click the web link, App Installer will prompt to **Download** the package instead of **Install**. After download, App Installer will initiate the launch of the downloaded package automatically. Because the app package is downloaded from the web, these files will pass through Microsoft SmartScreen for a security check. Once the user provides permission to continue and then one more click on **Install**, the app is ready for use!

Although this flow isn't quite as seamless as the direct install on Windows 10 Fall Creators Update, users can still quickly engage with the app. Additionally, with this flow the user doesn't have to worry about app package files unnecessarily taking up space in drives. App Installer efficiently manages space by downloading the package to its app data folder and clearing packages when they are no longer needed. 

Here's a quick comparison of the Windows 10 Fall Creators update version of App Installer and the previous version of App Installer:

| App Installer, Latest Version | App Installer, Previous Version |
|------------------------------|----------------------------------|
| App Installer shows app info before the download starts | Browser prompts the user to choose to download  |
| App Installer performs the download | User has to manually initiate the launch of the app package |
| After package download, App Installer automatically launches the app package | User must click **Install** and manually launch the app package |
| App Installer will take care of disposal of downloaded packages | User must manually delete the downloaded files |

## Web install experience on previous versions of Windows 10
On versions prior to the Windows 10 Fall Creators Update, App Installer cannot directly install an app from the web. On these versions, App Installer can only install app packages that are locally available. Instead, App Installer will download the package and require the user to double click the downloaded package to install.


## Microsoft SmartScreen integration

Microsoft SmartScreen has always been part of the installation process for installing apps via App Installer. SmartScreen ensures users are safeguarded from malcontent that can make its way on to their devices. With the latest update to App Installer, SmartScreen integration is more seamless and robust, providing warnings when installing unknown apps and protecting devices from harm. 
