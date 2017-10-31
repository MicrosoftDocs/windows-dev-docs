---
author: TylerMSFT
title: Extend your app with services, extensions, and packages
description: Learn how to create a background task that runs when your Universal Windows Platform (UWP) store app is updated.
ms.author: twhitney
ms.date: 05/21/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, extend, componentize, app service, package, extension
localizationpriority: medium
---

# Extend your app with services, extensions, and packages

There are different technologies in Windows 10 that will help you extend and componentize your app. This table should help you determine which technology you should use for your scenario. It is followed by a brief description of the scenarios and technologies.


| Scenario                           | Resource package | Optional package | App Extension    | App service      | Streaming Install |
|------------------------------------|:----------------:|:----------------:|:----------------:|:----------------:|:-----------------:|
| 3rd party code plug-ins            |                  |                  |:heavy_check_mark:|                  |                   |
| In-proc code plug-ins              |                  |:heavy_check_mark:|                  |                  |                   |
| UX Assets (strings/images)         |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|                  |:heavy_check_mark: |
| On demand content <br/> (e.g. additional Levels) |    |:heavy_check_mark:|:heavy_check_mark:|                  |:heavy_check_mark: |
| Separate licensing and acquisition |                  |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|                   |
| In-app acquisition                 |                  |:heavy_check_mark:|:heavy_check_mark:|                  |                   |
| Optimize install time              |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|                  |:heavy_check_mark: |  

## Scenario descriptions (rows in the table)

**3rd party plug-ins**  

Code that you can download from the store and run from your app. For example, extensions for the Microsoft Edge browser.

**In-proc code plug-ins**  

Code that runs in-process with your app. Only C++ is supported. May also include content. Because the code runs in-process, a higher level of trust is assumed. You may choose not to open up this kind of extensibility to a third-party.

**UX Assets (string/images)**  

User interface assets such as localized strings, images, and any other UI content that you want to factor based on locale or any other reason.

**On demand content**  

Content that you want to download at a later time. For example, in-app purchases that allow you to download new levels, skins, or functionality.

**Separate licensing and acquisition**  

The ability to license and acquire the content independently of the app.

**In-app acquisition**  

Indicates whether there is programmatic support to acquire the content from within the app.

**Optimize install time**

Provides functionality to decrease the time it takes to acquire the app from the store and start running.

## Technology descriptions (columns in the table)

**Resource package**

Resource packages are asset-only packages that allow your app to adapt to multiple display sizes and system languages. The resource package targets user language, system scale, and DirectX features, allowing the app to be tailored to a variety of user scenarios. Although an app package can contain several resources, the OS will only download the relevant resources per user device, saving bandwidth and disk space.

**Optional package**

Optional packages are used to either supplement or extend the original functionality of an app package. It's possible to publish an app, followed by publishing optional packages at a later time, or to publish both the app and optional packages simultaneously. By extending your app via an optional package, you have the advantages of distributing and monetizing content as a separate app package. Optional packages are typically intended to be developed by the original app developer, since they run with the identity of the main app (unlike app extensions). Depending on how you define your optional package, you can load code, assets, or code and assets from your optional package to your main app. If you're looking to enhance your app with content that can be monetized, licensed, and distributed separately, then optional packages might be the right choice for you. For implementation details, see [Optional packages and related set authoring](https://docs.microsoft.com/windows/uwp/packaging/optional-packages).

**App Extension**

[App extensions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appextensions) enable your UWP app to host content provided by other UWP apps. Discover, enumerate, and access read-only content from those apps.

If an app supports extensions, any developer can submit an extension for the app. Thus, the host app needs to be robust when it loads an extension that it hasn't been pre-tested with. Extensions should be considered untrusted.

Applications cannot load code from extensions. If you need code execution, consider App Services.

**App Service**

Windows app services enable app-to-app communication by allowing your UWP app to provide services to other Universal Windows app. App services let you create UI-less services that apps can call on the same device, and starting with Windows 10, version 1607, on remote devices. See [Create and consume an app service](https://docs.microsoft.com/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service) for details.

App services are UWP apps that provide services to other UWP apps. They are analogous to web services, on a device. An app service runs as a background task in the host app and can provide its service to other apps. For example, an app service might provide a bar code scanner service that other apps could use. Or perhaps an Enterprise suite of apps has a common spell checking app service that is available to the other apps in the suite.

**UWP App Streaming install**

Streaming Install is a way to optimize how your app is delivered to users. Rather than waiting for the entire app to download before you can use it, users can engage with the app as soon as a required portion has been downloaded. It's up to you, as a developer, to segment your app into a required section for basic activation and launch and additional content for the rest of the app. See [UWP App Streaming Install](https://docs.microsoft.com/windows/uwp/packaging/streaming-install) for more information and implementation details.

## See Also

[Create and consume an app service](https://docs.microsoft.com/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service)  
[Optional packages and related set authoring](https://docs.microsoft.com/windows/uwp/packaging/optional-packages)  
[Windows.ApplicationModel.Extensions namespace](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appextensions)  
[UWP App Streaming Install](https://docs.microsoft.com/windows/uwp/packaging/streaming-install)  
[Windows.ApplicationModel.AppService namespace](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.AppService)    
