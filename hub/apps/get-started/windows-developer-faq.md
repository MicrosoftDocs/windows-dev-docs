---
description: The official Windows Developer FAQ.
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
  2. Try to answer the questions [we are here]
  3. Align + refine with team + Ryan Demo + PMM
  4. Remove comments, link out to resources when helpful, format for instant answer previews in google, follow the FAQ template, link to glossary
  5. Add this doc to stable release checklist
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

 - Multi-windowing isn't supported yet. This is currently in-scope for Windows App SDK v1.1, which is scheduled to ship in Q2 2022. 
 - Non-store apps can't send push notifications. This is currently in-scope for Windows App SDK v1.1.
 - Local toast notifications aren't supported yet. This is currently in-scope for Windows App SDK v1.1.
 - XAML Islands aren't supported yet.
 - Media, map, and inking controls aren't supported yet.
 - ARM64 isn't yet supported for unpackaged scenarios. This is currently in-scope for Windows App SDK v1.1.
 - Mica (Win11) and Acrylic (Win10) backgrounds aren't supported yet. This is currently in-scope for Windows App SDK v1.1.
 - App lifecycle management isn't supported yet.


##### When I build an app using Windows App SDK and WinUI 3, am I building a "WinUI 3 app"?
Yes - this is the recommended nomenclature.


--------------


### Selecting a Windows development technology


##### Where can I find a straightforward comparison of Windows development technologies?
See [Select a development technology](./index.md).


##### Can I use Blazor to build desktop apps?
Yes, but the application will be a hybrid web/desktop app that combines web and native UI together within a native client application. 

<!-- See: https://github.com/dotnet/maui/issues/2536 -->



--------------


### Windows App SDK and WinUI 3

##### What's the focus of Windows App SDK?
The Windows App SDK lifts libraries from the OS into a standalone SDK that you can use to build backwards-compatible desktop apps. [Learn more on Github](https://github.com/microsoft/WindowsAppSDK/discussions/1615).


##### What does Windows App SDK mean for UWP and desktop developers?
UWP developers don't need to migrate your project type. WinUI 2.x and the Windows SDK will continue to support UWP project types, including bug, reliability, and security fixes. Windows actually uses UWP project types for several of our own Windows apps. If you want to use the Windows App SDK or .NET 5 in your existing UWP app, you can migrate your project to a desktop project type by following our [migration guidance](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy). 

If you're working on a desktop project type (WPF, Windows, or .NET 5+), you can simply add the Windows App SDK as a NuGet package and get access to all the new APIs.

Over time, Windows App SDK will become the superset of the capabilities of both desktop and UWP. [Learn more on Github](https://github.com/microsoft/WindowsAppSDK/discussions/1615).


#### How should I think about the future of app development on Windows?
Windows App SDK is meant to decouple Windows development APIs from the operating system so that native applications can be built without depending on operating systems being updated. This is going to take a while, but the end result will allow developers to reach more users by removing dependencies on OS versions.

It's best to think of Windows App SDK / WinUI 3 as an OS-decoupled superset of UWP / WinUI 2 that will eventually reach feature parity. UWP will be supported by the Windows SDK in the meantime, with the caveat that .NET 5/6 won't be coming to UWP.


##### When I use WinUI 3, do I need to use UWP?
No. WinUI 3 is the next evolution of WinUI 2, which succeeded UWP.


##### Do you need to use XAML if you want to use WinUI 3?
No. UI controls can be created in code. You can also use XAML if preferred, which means that you can reuse your XAML when migrating from UWP/WPF to WinUI 3.


##### Does WinUI 3 have a designer?
Not yet.

TODO: Will we?


##### Do I need to use WinForms or WPF when creating GUIs in WinUI 3 apps?
No. WinUI 3 is a full-fledged UI framework. You can integrate WinUI 3 into your WinForms / WPF apps, but WinUI 3 doesn't have a dependency on WinForms or WPF.


##### Does Windows App SDK include WinUI 3?
Windows App SDK includes WinUI 3; WinUI 3 is a subset of Windows App SDK. 

##### Does Windows App SDK include WinUI 2?
WinUI 2 and UWP are not included as part of Windows App SDK. WinUI 2 ships as a standalone NuGet package.


##### Can I do everything that I can do with WinUI 2 using WinUI 3?
Not quite. WinUI 3 isn't an in-place upgrade of WinUI 2. WinUI 3 is a new technology that's meant to replace WinUI 2 as we work towards feature parity between UWP / WinUI 2.x and Windows App SDK / WinUI 3.


##### Can I use WinUI 3 without using Windows App SDK?
No. WinUI 3 ships as a subset of Windows App SDK.


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


##### Is WinUI 3 pure XAML like UWP/WPF?
It can be, but you can also code your UI


--------------


### UWP

##### Can UWP apps be distributed outside of the Microsoft Store?
Yes, as long as they're signed with a valid certificate. We recommend developing new apps with Windows App SDK and WinUI 3.


##### Can I mix UWP UI controls with Win32 UI controls?
TODO



--------------


### XAML Islands

##### What can I do with XAML Islands?
As of Windows 10 version 1903, you can use XAML Islands to [host WinRT XAML controls in non-UWP desktop apps](https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/xaml-islands). [Learn more about XAML Islands](https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/xaml-islands).


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

<!-- see: https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1510738 -->

##### What should I use if I want to build apps that work on Windows and Surface Hub?
If you're targeting both Windows and Surface hub, we recommend using UWP. You'll eventually be able to target Surface Hub with Windows App SDK / WinUI 3, but we aren't there yet.


##### What should I use if I want to build apps that work on Windows and Hololens/Mixed Reality?
We recommend using Windows App SDK and WinUI 3 to build Windows apps. For Hololens apps, we recommend using OpenXR + Win32. [Learn more about OpenXR](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/native/openxr). Mixed Reality apps can be built [using the Windows SDK and Visual Studio](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/install-the-tools).



--------------


### Samples and real-world apps

##### Where can I find an example of a small and easy-to-deploy WinUI 3 app?
TODO


##### How was MS Paint in Windows 11 built?
MS Paint on Windows 11 uses XAML islands to render UWP components within a ______ shell.


--------------


### Packaging, deployment, and updates

##### What's the difference between packaged and unpackaged apps?
Packaged apps use MSIX to give users an easy installation, uninstallation, and update experience. Unpackaged apps don't use MSIX. Both types of applications can be published to the Microsoft Store.

##### Can I create an unpackaged WinUI 3 app?
WinUI 3 apps without MSIX packaging can be deployed on Windows versions 1809 and above. See [Create a WinUI 3 app](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/create-your-first-winui3-app?pivots=winui3-unpackaged-csharp).


##### Can I configure my WinUI 3 app to auto-update?
TODO - ask product team to confirm - see https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1500094 


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

##### What can I do to make my Windows app feel great?
See [Make apps great for Windows](https://docs.microsoft.com/en-us/windows/apps/get-started/make-apps-great-for-windows).


-----------

### Compatibility

##### Will my users ever have to update Windows to use my WinUI 3 app?
Users who have Windows 10, version 1809 and beyond will be able to install your WinUI 3 apps without updating their OS as long as the project is configured to target Windows 10 version 1809.


##### Can I target ARM64 with my WinUI 3 app?
ARM64 is supported for packaged WinUI 3 apps. Targeting ARM64 with unpackaged WinUI 3 apps is currently unsupported.



-------------


### Deprecations and migrations

##### Are UWP / WinUI 2 deprecated?
No. UWP and WinUI 2 are still fully supported for production scenarios and the Windows SDK will continue to support UWP project types, including bug, reliability, and security fixes. 

UWP's functionality is being incrementally ported into Windows App SDK. There will eventually be parity between UWP and Windows App SDK, but we aren't planning to deprecate UWP any time soon.

<!--see MSFT representing UWP status here: https://www.reddit.com/r/csharp/comments/r9ecz4/what_is_the_main_framework_used_for_making/  -->


##### When should I migrate my UWP / WinUI 2 app to WinUI 3?
We encourage you to migrate your UWP project to a WinUI 3 desktop project if you need to use .NET 5/6 because .NET 5/6 won't be coming to UWP project types.

Otherwise, there's no need to migrate your UWP app to WinUI 3. We're working towards feature parity between UWP and Windows App SDK (which includes WinUI 3), but you can use UWP if Windows App SDK doesn't yet meet your needs.

If you're happy with your current UWP functionality, there's no need to migrate. 

<!-- https://github.com/microsoft/WindowsAppSDK/discussions/1615 -->


##### When should I *not* migrate my UWP / WinUI 2 app to WinUI 3?
We recommend using UWP if:

- You're building an app for Xbox
- You're building an app for Hololens
- You're building an app for Surface Hub 
- You're building an app that targets Windows 10 versio
- You need application lifecycle management 
- You need ultra-high-performance UIs <!-- see: https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1500116  -->



##### Is WPF deprecated?
WPF is in maintenance mode, but is still supported for production scenarios.

##### When should I migrate my WPF app to WinUI 3? Is guidance available?
TODO


##### Is WinForms deprecated?
WinForms is in maintenance mode, but is still supported for production scenarios. 

##### When should I migrate my WinForms app to WinUI 3? Is guidance available?
TODO

##### Is WinRT deprecated?
WinRT is in maintenance mode, but is still supported for production scenarios.

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

See [the roadmap on Github](https://github.com/microsoft/react-native-windows/discussions/8906).

