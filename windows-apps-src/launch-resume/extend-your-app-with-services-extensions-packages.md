---
title: Extend your app with services, extensions, and packages
description: Describes how to create a background task that runs when your Universal Windows Platform (UWP) store app is updated.
ms.date: 05/07/2018
ms.topic: article
keywords: windows 10, uwp, extend, componentize, app service, package, extension
ms.localizationpriority: medium
---
# Extend your app with services, extensions, and packages

There are many technologies in Windows 10 for extending and componentizing your app. This table should help you determine which technology you should use depending on requirements. It is followed by a brief description of the scenarios and technologies.

| Scenario                           | Resource package   | Asset package      | Optional package   | Flat bundle        | App Extension      | App service        | Streaming Install  |
|------------------------------------|:------------------:|:------------------:|:------------------:|:------------------:|:------------------:|:------------------:|:------------------:|
| Third-party code plug-ins            |                    |                    |                    |                    | :heavy_check_mark: |                    |                    |
| In-proc code plug-ins              |                    |                    | :heavy_check_mark: |                    |                    |                    |                    |
| UX Assets (strings/images)         | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |                    | :heavy_check_mark: |                    | :heavy_check_mark: |
| On demand content <br/> (for example, additional Levels) |      |                    | :heavy_check_mark: |                    | :heavy_check_mark: |                    | :heavy_check_mark: |
| Separate licensing and acquisition |                    |                    | :heavy_check_mark: |                    | :heavy_check_mark: | :heavy_check_mark: |                    |
| In-app acquisition                 |                    |                    | :heavy_check_mark: |                    | :heavy_check_mark: |                    |                    |
| Optimize install time              | :heavy_check_mark: |                    | :heavy_check_mark: |                    | :heavy_check_mark: |                    | :heavy_check_mark: |
| Reduce disk footprint              | :heavy_check_mark: |                    | :heavy_check_mark: |                    |                    |                    |                    |
| Optimize packaging                 |                    | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |                    |                    |                    |
| Reduce publishing time             | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |                    |                    |                    |

## Scenario descriptions (the rows in the table above)

**Third-party plug-ins**  

Code that you can download from the store and run from your app. For example, extensions for the Microsoft Edge browser.

**In-proc code plug-ins**  

Code that runs in-process with your app. May also include content. Because the code runs in-process, a higher level of trust is assumed. You may choose not to expose this kind of extensibility to a third-party.

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

**Reduce disk footprint**
Reduces the size of an app by only including necessary apps or resources.

**Optimize packaging**
Optimizes the app packaging process for large-scale or complex apps.

**Reduce publishing time**
Minimize the amount of time it takes to publish your app in the Store, local share, or web server.

## Technology descriptions (the columns in the table above)

**Resource package**

Resource packages are asset-only packages that allow your app to adapt to multiple display sizes and system languages. The resource package targets user language, system scale, and DirectX features, allowing the app to be tailored to a variety of user scenarios. Although an app package can contain several resources, the OS will only download the relevant resources per user device, saving bandwidth and disk space.

**Asset package**
Asset packages are a common, centralized source of executable, or non-executable files for use by your app. These are typically non-processor or language-specific files. For example, this might include a collection of pictures in one asset package, and videos in another asset package, both of which are used by the app. If your app supports multiple architectures and multiple languages, these assets could be included in the architecture package or resource package, but that also means the assets would be duplicated multiple times across the various architecture packages, taking up disk space. If asset packages are used, they only need to be included in the overall app package once. See [Introduction to asset packages](/windows/msix/package/asset-packages) to learn more.

**Optional package**

Optional packages are used to either supplement or extend the original functionality of an app package. It's possible to publish an app, followed by publishing optional packages at a later time, or to publish both the app and optional packages simultaneously. By extending your app via an optional package, you have the advantages of distributing and monetizing content as a separate app package. Optional packages are typically intended to be developed by the original app developer, since they run with the identity of the main app (unlike app extensions). Depending on how you define your optional package, you can load code, assets, or code and assets from your optional package to your main app. If you need to enhance your app with content that can be monetized, licensed, and distributed separately, then optional packages might be the right choice for you. For implementation details, see [Optional packages and related set authoring](/windows/msix/package/optional-packages).

**Flat bundle**
[Flat bundle app packages](/windows/msix/package/flat-bundles) are similar to regular app bundles, except that instead of including all of the app packages within the folder, the flat bundle only contains *references* to those app packages. By containing references to app packages instead of the files themselves, a flat bundle will reduce the amount of time it takes to package and download an app.

**App Extension**

[App extensions](/uwp/api/windows.applicationmodel.appextensions) enable your UWP app to host content provided by other UWP apps. Discover, enumerate, and access read-only content from those apps.

If an app supports extensions, any developer can submit an extension for the app. Thus, the host app needs to be robust when it loads an extension that it hasn't been pre-tested with. Extensions should be considered untrusted.

Applications cannot load code from extensions. If you need code execution, consider App Services.

**App Service**

Windows app services enable app-to-app communication by allowing your UWP app to provide services to another Universal Windows app. App services let you create UI-less services that apps can call on the same device, and starting with Windows 10, version 1607, on remote devices. See [Create and consume an app service](./how-to-create-and-consume-an-app-service.md) for details.

App services are UWP apps that provide services to other UWP apps. They are analogous to web services on a device. An app service runs as a background task in the host app and can provide its service to other apps. For example, an app service might provide a bar code scanner service that other apps could use. Or perhaps an Enterprise suite of apps has a common spell checking app service that is available to the other apps in the suite.

**UWP App Streaming install**

Streaming Install is a way to optimize how your app is delivered to users. Rather than waiting for the entire app to download before you can use it, users can engage with the app as soon as a required portion has been downloaded. It's up to you, as a developer, to segment your app into a required section for basic activation and launch and additional content for the rest of the app. See [UWP App Streaming Install](/windows/msix/package/streaming-install) for more information and implementation details.

## See Also

[Create and consume an app service](./how-to-create-and-consume-an-app-service.md)  
[Introduction to asset packages](/windows/msix/package/asset-packages)  
[Package creation with the packaging layout](/windows/msix/package/packaging-layout)  
[Optional packages and related set authoring](/windows/msix/package/optional-packages)  
[Developing with asset packages and package folding](/windows/msix/package/package-folding)  
[UWP App Streaming Install](/windows/msix/package/streaming-install)  
[Flat bundle app packages](/windows/msix/package/flat-bundles)  
[Windows.ApplicationModel.AppService namespace](/uwp/api/Windows.ApplicationModel.AppService)  
[Windows.ApplicationModel.Extensions namespace](/uwp/api/windows.applicationmodel.appextensions)