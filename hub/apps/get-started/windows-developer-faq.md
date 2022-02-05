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

The following FAQ is meant to promote a common understanding of the Windows Development experience and product direction.

<!--
 - It feels like developers are saying "WinUI 3 app" more than they're saying "Win App SDK app". Can/should we embrace that? Product marketing manager contact?
  - WinUI3 vs WinUI 3? 
  - Needs to align with easy-answer previews in SEO
 -->


### Getting started

##### What's the latest and recommended way to build Windows Apps?
If you're targeting Windows only, Windows App SDK + WinUI 3 are the future of Windows app development. 

If you're targeting multiple platforms, .NET MAUI lets you build cross-platform apps that use WinUI 3 when running on Windows.

If you're a React developer, React Native will soon use WinUI 3 on Windows. 

For web developers, WebView2 gives your web applications a way to feel like Windows desktop apps while accessing the Windows platform and Windows App SDK.  


##### What limitations are there when building with WinUI 3 and Windows App SDK?
TODO





### Windows development technologies

##### Where can I find a technology comparison chart?
TODO

##### Can I create an app using WinUI 3 without using UWP?
TODO





##### Do you need to use XAML if you want to use WinUI 3?
No, controls can be created in code.


##### Does WinUI 3 have a designer?
No.




##### Can I start with WinUI 3 and App SDK, and later integrate .NET MAUI if I eventually want to target cross-platform?
TODO

##### Do I need to use WinForms or WPF when creating GUIs?
TODO

##### Can I combine WPF and WinUI 3? When would I want to?
Yes, you can integrate WinUI 3 into your WPF app. This can be useful in situations where you need browser controls from your WPF app.


##### Does Windows App SDK include UWP and WinUI?
Windows App SDK includes WinUI 3; WinUI 3 is a subset of Windows App SDK. WinUI 2 is not included as part of Windows App SDK.



##### Can UWP apps be distributed outside of the Microsoft Store?
Yes, as long as they're signed with a valid certificate. Though we recommend developing new apps with Windows App SDK and WinUI 3.


##### Can I use Blazor to build desktop apps?
TODO


##### Does Windows App SDK include multiple versions of WinUI?
TODO


##### Can I do everything that I can do with WinUI 2 using WinUI 3?
No. WinUI 3 isn't an upgraded version of WinUI 2. WinUI 3 is a new technology that's meant to replace WinUI 2.


##### Can I use WinUI 3 without using Windows App SDK?
No.


##### Could I build a clone of Notepad or Paint with Windows App SDK and WinUI 3?
TODO


##### Can I use WinUI 3 in Win32 apps?
Yes, but the WinUI 3 UI controls can't be mixed with Win32 UI controls.


##### Can I mix UWP UI controls with Win32 UI controls?
Yes, but the WinUI 3 UI controls can't be mixed with Win32 UI controls.



##### What's the difference between XAML Islands and WinUI 3?
TODO



##### How do I know when to use each of the available development technologies?
Visit the app development options page for a comparison chart.



##### Can XAML Islands be used with Windows App SDK?
TODO


##### Can WinUI 2 be used with WinUI 3?
No. WinUI 3 is a subset of Windows App SDK, which uses the `Microsoft.*` namespace. WinUI 2 uses the `Windows.*` namespace, and cannot be mixed with WinUI 3.


##### Is WinUI3 compatible with UWP?
WinUI 2 is a control library for UWP.


##### If I use WinUI 3, will my app look modern on both Windows 10 and Windows 11? Does it matter if my app is packaged or unpackaged?
Yes, your app will look modern on both Windows 11 and Windows 10 down to version 1809. 


##### Can I use WinUI 3 with React Native?
TODO




### Upgrading apps to Windows App SDK

##### Is it hard to migrate UWP apps to Windows App SDK and WinUI 3?
No - the Windows App SDK API closely resembles the UWP API in most cases. Learn more about migrating your UWP apps.


##### If I have an existing UWP app in the Store, can I publish a new packaged app using the same identifiers?
Yes, upgraded apps can be published without having to update your application's identity. Users who have the old version will get updated to the new version.


### Cross-platform development


##### What should I use if I want to build apps that work on Windows and Xbox?
We recommend using Windows App SDK and WinUI 3 to build Windows apps. If you need to support Xbox, we recommend using UWP. For game development, we recommend using [Microsoft Game Development Kit](https://github.com/microsoft/GDK).

TODO: Are there future plans to allow devs to build xbox apps using win app sdk?


##### What should I use if I want to build apps that work on Windows and Surface Hub?
TODO


##### What should I use if I want to build apps that work on Windows and Hololens?
We recommend using Windows App SDK and WinUI 3 to build Windows apps. For Hololens apps, you'll want to use OpenXR + Win32. [Learn more about OpenXR](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/native/openxr).


##### What should I use if I want to build apps that work on Windows and Windows Mixed Reality?
TODO





### Samples and real-world apps

##### Where can I find an example of a small and easy to deploy WinUI 3 app?
TODO

##### How was MS Paint in Windows 11 built?
MS Paint on Windows 11 uses XAML islands to render UWP components.







### Packaging, deployment, and updates

##### What's the difference between packaged and unpackaged apps?
TODO

##### Can I create an unpackaged WinUI 3 app?
TODO

##### Can I configure my WinUI 3 app to auto-update?
TODO - see https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1500094 


### Security


##### Can I use encrypted binaries? This will help me migrate my UWP app to WinUI 3.
TODO


### Performance and optimization

##### Does Windows App SDK support native compilation?
TODO

##### How do I make sure my apps run smoothly on all targeted devices and platforms?
TODO

##### My built WinUI3 app is large. How to I reduce the size of my build?
TODO


### Compatibility

##### Will my users ever have to update Windows to use my WinUI 3 app?
Users who have Windows 10 version 1809 will be able to install your WinUI 3 apps without updating their OS. TODO - clarify.


##### Will future versions of Windows support older Windows apps?
TODO



### Deprecations and migrations

##### Is UWP deprecated?
UWP is in maintenance mode. We recommend building new Windows apps with Windows App SDK and WinUI 3.

##### When should I migrate my UWP app to WinUI 3? Is guidance available?
If you need to use .NET 5/6, we recommend migrating your UWP project to a WinUI 3 desktop project. .NET 5/6 won't be coming to UWP project types.

If you're happy with your current UWP functionality, there's no need to migrate. WinUI 2.x and the Windows SDK will continue to support UWP project types, including bug, reliability, and security fixes. https://github.com/microsoft/WindowsAppSDK/discussions/1615 


##### Is WPF deprecated?
TODO

##### When should I migrate my WPF app to WinUI 3? Is guidance available?
TODO

##### Is WinUI 2 deprecated?
TODO

##### When should I migrate my WinUI 2 app to WinUI 3? Is guidance available?
TODO

##### Is WinForms deprecated?
TODO

##### When should I migrate my WinForms app to WinUI 3? Is guidance available?
TODO

##### Is WinRT deprecated?
TODO

##### When should I migrate my WinRT app to WinUI 3? Is guidance available?
TODO


### Developing across platforms

##### Can I build 
TODO


### Future plans and roadmaps

##### Will WinUI3 and Windows App SDK be open-sourced?
TODO

##### Where can I find a roadmap for React Native Windows + Windows App SDK integration?
https://github.com/microsoft/react-native-windows/discussions/8906