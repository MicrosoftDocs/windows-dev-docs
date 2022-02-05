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


##### What limitations are there when building with WinUI 3 and Windows App SDK?
TODO


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


--------------


### UWP

##### Can UWP apps be distributed outside of the Microsoft Store?
Yes, as long as they're signed with a valid certificate. We recommend developing new apps with Windows App SDK and WinUI 3.


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
TODO

##### Can I create an unpackaged WinUI 3 app?
TODO

##### Can I configure my WinUI 3 app to auto-update?
TODO - see https://github.com/microsoft/WindowsAppSDK/discussions/1615#discussioncomment-1500094 


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
Users who have Windows 10 version 1809 will be able to install your WinUI 3 apps without updating their OS. TODO - clarify.


##### Will my UWP app work on future versions of Windows?
TODO


##### Will my WPF app work on future versions of Windows?
TODO


-------------


### Deprecations and migrations

##### Is UWP deprecated?
UWP is in maintenance mode. While UWP is still fully supported for use in production, we recommend building new Windows apps with Windows App SDK and WinUI 3. The only time we recommend building new apps with UWP is if you're building an app for Xbox, Hololens, or Windows 10 versions ______(TODO).

TODO: how will this story change in the future?

<!--see MSFT representing UWP status here: https://www.reddit.com/r/csharp/comments/r9ecz4/what_is_the_main_framework_used_for_making/  -->

##### When should I migrate my UWP app to WinUI 3? Is guidance available?
If you need to use .NET 5/6, we recommend migrating your UWP project to a WinUI 3 desktop project. .NET 5/6 won't be coming to UWP project types.

If you're happy with your current UWP functionality, there's no need to migrate. WinUI 2.x and the Windows SDK will continue to support UWP project types, including bug, reliability, and security fixes.

<!-- https://github.com/microsoft/WindowsAppSDK/discussions/1615 -->



##### Is WPF deprecated?
TODO

##### When should I migrate my WPF app to WinUI 3? Is guidance available?
TODO

##### Is WinUI 2 deprecated?
TODO

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


### Future plans and roadmaps

##### Will WinUI 3 and Windows App SDK be open-sourced?
TODO

##### Where can I find a roadmap for Windows App SDK?
TODO

##### Where can I find a roadmap for React Native Windows + Windows App SDK integration?
https://github.com/microsoft/react-native-windows/discussions/8906

