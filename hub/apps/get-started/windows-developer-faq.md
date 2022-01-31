---
description: A glossary of terms related to Windows application development.
title: Windows Developer Glossary
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
 - Developers are saying "WinUI 3 app" more than they're saying "Win App SDK app" on social media. Can/should we embrace that?
  - WinUI3 vs WinUI 3? 
 -->


### Getting started

##### What's the latest and recommended way to build Windows Apps?
If you're targeting Windows only, Windows App SDK + WinUI 3 are the future of Windows app development. 

If you're targeting multiple platforms, .NET Maui lets you build cross-platform apps that use WinUI 3 when running on Windows.

If you're a React developer, React Native will soon use WinUI 3 on Windows. 

For web developers, WebView2 gives your web applications a way to feel like Windows desktop apps while accessing the Windows platform and Windows App SDK.  


##### What limitations are there when building with WinUI 3 and Windows App SDK?
TODO





### Windows development technologies

##### Where can I find a technology comparison chart?
TODO

##### Can I create an app using WinUI 3, but not UWP?
TODO


##### Can I start with WinUI 3 and App SDK, and later integrate .NET Maui if I eventually want to target cross-platform?
TODO

##### Do I need to use WinForms or WPF when creating GUIs?
TODO

##### Can I combine WPF and WinUI 3? When would I want to?
Yes, you can integrate WinUI 3 into your WPF app. This can be useful in situations where you need browser controls from your WPF app.


##### Does Windows App SDK include UWP and WinUI?
TODO


##### Does Windows App SDK include multiple versions of WinUI?
TODO


##### Can I do everything that I can do with WinUI 2 using WinUI 3?
WinUI 3 isn't quite an upgraded version of WinUI 2.


##### Could I theoretically build Notepad or Paint with Windows App SDK and WinUI 3?
TODO

##### Can I use WinUI 3 in Win32 apps?
Yes, but the WinUI 3 UI controls can't be mixed with Win32 UI controls.


##### Can I mix UWP UI controls with Win32 UI controls?
Yes, but the WinUI 3 UI controls can't be mixed with Win32 UI controls.



##### What's the difference between XAML Islands and WinUI 3?




##### How do I know when to use each of the available development technologies?
Visit the app development options page for a comparison chart.

##### Can XAML Islands be used with Windows App SDK?
TODO


##### Can WinUI 2 be used with WinUI 3?
No. WinUI 3 is a subset of Windows App SDK, which uses the `Microsoft.*` namespace. WinUI 2 uses the `Windows.*` namespace.


##### Is WinUI3 compatible with UWP?
WinUI 2 is a control library for UWP.


##### If I use WinUI 3, will my app look modern on both Windows 10 and Windows 11? Does it matter if my app is packaged or unpackaged?
Yes, your app will look modern on both Win10 and Win11.


##### Can I use WinUI 3 with React Native?
TODO





### Samples and real-world apps

##### Where can I find an example of a small and easy to deploy WinUI 3 app?
TODO

##### How was MS Paint built?
MS Paint uses XAML islands to render UWP components.







### Packaging and deployment

##### What's the difference between packaged and unpackaged apps?
TODO

##### Can I create an unpackaged WinUI 3 app?
TODO



### Performance and optimization

##### How do I make sure my apps run smoothly on all targeted devices and platforms?
TODO

##### My built WinUI3 app is large. How to I reduce the size of my build?
TODO


### Compatibility

##### Will my users ever have to update Windows to use my WinUI 3 app?
No. Users who have Windows _____ will be able to install your WinUI 3 apps without updating their operating system.

##### Will future versions of Windows support older Windows apps?
TODO



### Deprecations and migrations

##### Is UWP deprecated?
TODO

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


### Developing across platforms

##### Can I build 
TODO


### Future plans and roadmaps

##### Will WinUI3 and Windows App SDK be open-sourced?
TODO

##### Where can I find a roadmap for React Native Windows + Windows App SDK integration?
https://github.com/microsoft/react-native-windows/discussions/8906