---
description: A collection of frequently asked questions - the official Windows Developer FAQ.
title: Windows Developer FAQ
ms.topic: article
ms.date: 01/24/2022
keywords: windows win32, desktop development
ms.author: mikben
author: mikben
ms.localizationpriority: medium
ms.collection: windows11
---

# Windows Developer FAQ

The following FAQ is meant to promote a common understanding of the Windows development experience and product direction.

<!--
  1. List and categorize v1 questions [done]
  2. Answer the questions [we are here]
  2. Align on messaging - do we have a Product marketing manager contact to work with? Who on the product side should we align with?
  2a. WinUI3 vs WinUI 3? 
  3. Remove comments, link out to resources when helpful, format for instant answer previews in google, follow the FAQ template
  4. Add this doc to stable release checklist
  -
  - This is an example of the pain we're trying to alleviate through clear messaging: https://github.com/microsoft/WindowsAppSDK/discussions/1615 
 -->


### Getting started

##### What's the latest and recommended way to build Windows Apps?
If you're targeting Windows only, Windows App SDK + WinUI 3 are the future of Windows app development. 

If you're targeting multiple platforms, .NET MAUI lets you build cross-platform apps that use WinUI 3 when running on Windows.

If you're a React developer, React Native will soon use WinUI 3 on Windows. 

For web developers, WebView2 gives your web applications a way to feel like Windows desktop apps while accessing the Windows platform and Windows App SDK.  

[Learn more about choosing a development technology](./index.md).


##### Can I incrementally migrate my old app to Windows App SDK and WinUI 3?
Yes - you can gradually migrate your WinForms, WPF, and UWP apps to WinUI 3.


##### Do I need to use Visual Studio to build WinUI 3 apps?
We strongly recommend using Visual Studio for at least initial project creation. Visual Studio Community Edition is supported. You can also use `msbuild.exe` and VS Code together, although this isn't recommended because it hasn't been fully tested.



##### What limitations are there when building with WinUI 3 and Windows App SDK?

 - Multiple window support is currently unsupported. This is currently in-scope for Windows App SDK v1.1, which is scheduled to ship in Q2 2022. 
 - Non-store apps can't send push notifications. This is currently in-scope for Windows App SDK v1.1.
 - Local toast notifications are currently unsupported. This is currently in-scope for Windows App SDK v1.1.
 - XAML Islands are currently unsupported.
 - Media, maps, and inking controls are currently unsupported.
 - ARM64 is currently unsupported for unpackaged scenarios. This is currently in-scope for Windows App SDK v1.1.
 - Mica (Win11) and Acrylic (Win10) backgrounds are currently unsupported. This is currently in-scope for Windows App SDK v1.1.
 - App lifecycle management is currently unsupported.


##### When I build an app using Windows App SDK and WinUI 3, am I building a "WinUI 3 app"?
Yes - this is the recommended nomenclature.


--------------


### Selecting a Windows development technology


##### Where can I find a straightforward comparison of Windows development technologies?
See [Select a development technology](./index.md).


##### Can I use Blazor to build desktop apps?
Yes, but the application will be a hybrid web/desktop app that combines web and native UI together within a native client application. 

<!-- See: https://github.com/dotnet/maui/issues/2536 -->


##### What should I use if I want to target user devices running versions of Windows older than version 1809?
TODO


--------------


### Windows App SDK and WinUI 3

##### What's the focus of Windows App SDK?
The Windows App SDK lifts libraries from the OS into a standalone SDK that you can use to build backwards-compatible desktop apps. [Learn more on Github](https://github.com/microsoft/WindowsAppSDK/discussions/1615).


##### What does Windows App SDK mean for UWP and desktop developers?
UWP developers don't need to migrate your project type. WinUI 2.x and the Windows SDK will continue to support UWP project types, including bug, reliability, and security fixes. Windows actually uses UWP project types for several of our own Windows apps. If you want to use the Windows App SDK or .NET 5 in your existing UWP app, you can migrate your project to a desktop project type by following our [migration guidance](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy). 

If you're working on a desktop project type (WPF, Windows, or .NET 5+), you can simply add the Windows App SDK as a NuGet package and get access to all the new APIs.

Over time, Windows App SDK will become the superset of the capabilities of both desktop and UWP. [Learn more on Github](https://github.com/microsoft/WindowsAppSDK/discussions/1615).


#### How should I think about the future of app development on Windows?



##### When I use WinUI 3, do I need to use UWP?
No. WinUI 3 is the next evolution of WinUI 2, which succeeded UWP.


##### Do you need to use XAML if you want to use WinUI 3?
No. UI controls can be created in code.


##### Does WinUI 3 have a designer?
No.

TODO: Will we?

##### Do I need to use WinForms or WPF when creating GUIs in WinUI 3 apps?
No. WinUI 3 is a full-fledged UI framework.


##### Does Windows App SDK include UWP and WinUI?
Windows App SDK includes WinUI 3; WinUI 3 is a subset of Windows App SDK. WinUI 2 and UWP are not included as part of Windows App SDK.


##### Does Windows App SDK include multiple versions of WinUI?
TODO


##### Can I do everything that I can do with WinUI 2 using WinUI 3?
Not quite. WinUI 3 isn't an in-place upgrade of WinUI 2. WinUI 3 is a new technology that's meant to replace WinUI 2.


##### Can I use WinUI 3 without using Windows App SDK?
No. WinUI 3 ships as a subset of Windows App SDK.


##### Could I build a clone of Notepad or Paint with Windows App SDK and WinUI 3?
TODO


##### Can I use WinUI 3 in Win32 apps?
Yes, but the WinUI 3 UI controls can't be mixed with Win32 UI controls.

##### What's the difference between XAML Islands and WinUI 3?
TODO

##### Can XAML Islands be used with Windows App SDK?
TODO

##### Can WinUI 2 be used with WinUI 3?
No. WinUI 3 is a subset of Windows App SDK, which uses the `Microsoft.*` namespace. WinUI 2 uses the `Windows.*` namespace, and cannot be mixed with WinUI 3.


##### Is WinUI 3 compatible with UWP?
No. WinUI 2 is a control library for UWP.

##### If I use WinUI 3, will my app look modern on both Windows 10 and Windows 11? Does it matter if my app is packaged or unpackaged?
Yes, your app will look modern on both Windows 11 and Windows 10 down to version 1809. 

##### Can I use WinUI 3 with React Native?
TODO

##### What's the difference between the "Windows SDK" and the "Windows App SDK"?
TODO

##### Can I use Mica (Windows 11) or Acrylic (Windows 10) backgrounds in apps built with Windows App SDK?
Not quite. This is currently in-scope for Windows App SDK v1.1.


--------------


### UWP

##### Can UWP apps be distributed outside of the Microsoft Store?
Yes, as long as they're signed with a valid certificate. We recommend developing new apps with Windows App SDK and WinUI 3.


##### Can I mix UWP UI controls with Win32 UI controls?
TODO



--------------


### XAML Islands

##### What can I do with XAML Islands?
XAML Islands lets you use new UI components in existing desktop (Win32, WinForms WPF) apps. As of Windows 10 version 1903, you can use it to [host WinRT XAML controls in non-UWP desktop apps](https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/xaml-islands). 
<!-- note: definition duplicated in glossary -->

##### Can I mix UWP UI controls with Win32 UI controls?
TODO



--------------



### Combining Windows development technologies

##### Can I start with WinUI 3 and App SDK, and later integrate .NET MAUI if I eventually want to target cross-platform scenarios?
TODO

##### Can I combine WPF and WinUI 3? When would I want to?
Yes, you can integrate WinUI 3 into your WPF app. This can be useful in situations where you need browser controls from your WPF app.




--------------


### Upgrading apps to Windows App SDK

##### Is it hard to migrate UWP apps to Windows App SDK and WinUI 3?
No. The Windows App SDK API closely resembles the UWP API in most cases. Learn more about migrating your UWP apps. TODO - link


##### If I have an existing UWP app in the Store, can I publish a new packaged app using the same identifiers?
Yes, upgraded apps can be published without having to update your application's identity. Users who have the old version will get updated to the new version.


--------------


### Cross-platform development


##### What should I use if I want to build apps that work on Windows and Xbox?
We recommend using Windows App SDK and WinUI 3 to build Windows apps. If your app needs to support Xbox, we recommend using UWP. For game development, we recommend using [Microsoft Game Development Kit](https://github.com/microsoft/GDK).

TODO: Are there future plans to allow devs to build xbox apps using win app sdk?


##### What should I use if I want to build apps that work on Windows and Surface Hub?
TODO


##### What should I use if I want to build apps that work on Windows and Hololens?
We recommend using Windows App SDK and WinUI 3 to build Windows apps. For Hololens apps, you'll want to use OpenXR + Win32. [Learn more about OpenXR](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/native/openxr).


##### What should I use if I want to build apps that work on Windows and Windows Mixed Reality?
TODO


--------------


### Samples and real-world apps

##### Where can I find an example of a small and easy-to-deploy WinUI 3 app?
TODO

##### How was MS Paint in Windows 11 built?
MS Paint on Windows 11 uses XAML islands to render UWP components.


--------------


### Packaging, deployment, and updates

##### What's the difference between packaged and unpackaged apps?
Packaged apps use MSIX to give users an easy installation, uninstallation, and update experience. Unpackaged apps don't use MSIX. Both types of applications can be published to the Microsoft Store.

##### Can I create an unpackaged WinUI 3 app?
WinUI 3 apps without MSIX packaging can be deployed on Windows versions 1809 and above. See [Create a WinUI 3 app](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/create-your-first-winui3-app?pivots=winui3-unpackaged-csharp).


##### Can I configure my WinUI 3 app to auto-update?
TODO - see https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1500094 

##### Can applications that don't use MSBuild use Windows App SDK?
TODO

<!-- see: https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1499451 -->



-----------


### Security


##### Can I use encrypted binaries with WinUI 3 apps? This will help me migrate my UWP app to WinUI 3.
TODO


##### Where can I learn more about security best practices when developing apps for Windows?
TODO


-----------

### Performance and optimization

##### Does Windows App SDK support native compilation?
TODO

##### How do I make sure my apps run smoothly on all targeted devices and platforms?
TODO

##### My built WinUI 3 app is large. How to I reduce the size of my build?
TODO


-----------

### Compatibility

##### Will my users ever have to update Windows to use my WinUI 3 app?
Users who have Windows 10 version 1809 will be able to install your WinUI 3 apps without updating their OS as long as the project is configured to target Windows 10 version 1809.


##### Will my UWP app work on future versions of Windows?
TODO


##### Will my WPF app work on future versions of Windows?
TODO

##### Can I target ARM64 with my WinUI 3 app?
ARM64 is supported for packaged WinUI 3 apps. Targeting ARM64 with unpackaged WinUI 3 apps is currently unsupported.



-------------


### Deprecations and migrations

##### Is UWP deprecated?
No. UWP is still fully supported for production scenarios and the Windows SDK will continue to support UWP project types, including bug, reliability, and security fixes. 

UWP's functionality is being incrementally ported into Windows App SDK. There will eventually be parity between UWP and Windows App SDK, but we aren't planning to deprecate UWP any time soon.

<!--see MSFT representing UWP status here: https://www.reddit.com/r/csharp/comments/r9ecz4/what_is_the_main_framework_used_for_making/  -->


##### When should I migrate my UWP app to WinUI 3?
We encourage you to migrate your UWP project to a WinUI 3 desktop project if you need to use .NET 5/6 because .NET 5/6 won't be coming to UWP project types.

Otherwise, there's no need to migrate your UWP app to WinUI 3. We're working towards feature parity between UWP and Windows App SDK (which includes WinUI 3), but you can use UWP if Windows App SDK doesn't yet meet your needs.

If you're happy with your current UWP functionality, there's no need to migrate. 

<!-- https://github.com/microsoft/WindowsAppSDK/discussions/1615 -->


##### When should I *not* migrate my UWP app to WinUI 3?
We recommend using UWP if:

- You're building an app for Xbox
- You're building an app for Hololens
- You're building an app for Surface Hub 
- You're building an app that targets Windows 10 versio
- You need application lifecycle management 
- You need ultra-high-performance UIs <!-- see: https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1500116  -->



##### Is WPF deprecated?
TODO

##### When should I migrate my WPF app to WinUI 3? Is guidance available?
TODO

##### Is WinUI 2 deprecated?
No. WinUI 2 will continue adding new features and controls for UWP developers.
<!-- see: https://twitter.com/marbtweeting/status/1450305292730712064 -->


##### When should I migrate my WinUI 2 app to WinUI 3? Is guidance available?
TODO

##### Is WinForms deprecated?
No. WinForms is very much alive and supported.

##### When should I migrate my WinForms app to WinUI 3? Is guidance available?
TODO

##### Is WinRT deprecated?
TODO

##### When should I migrate my WinRT app to WinUI 3? Is guidance available?
TODO


----------


### Release notes, future plans, and roadmaps

##### Will WinUI 3 and Windows App SDK be open-sourced?
The team is currently focused on delivering stable features, but open source *is* on our internal roadmap. We'll keep the developer community updated through our community calls.


##### Where can I find release notes for Windows App SDK?
See [Stable channel release notes](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/stable-channel).


##### Where can I find a public roadmap for Windows App SDK?

 - [Windows App SDK roadmap](https://portal.productboard.com/winappsdk/1-windows-app-sdk/tabs/2-planned)
 - [WinUI 3 roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/main/docs/roadmap.md#winui-3)



##### Where can I find a roadmap for React Native Windows + Windows App SDK integration?
https://github.com/microsoft/react-native-windows/discussions/8906

