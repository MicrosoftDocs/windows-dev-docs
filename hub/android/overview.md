---
title: Android development on Windows
description: A guide to help you get started developing for Android on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.date: 09/28/2023
---

# Overview of Android development on Windows

A guide to help you set up your development environment on a Windows 10 or Windows 11 machine for developing Android apps. Android is a trademark of Google LLC. If you're a developer interested in using Windows operating system to build apps that work on Android devices and across other device platforms, this guide is for you.

You can also learn about using Windows Subsystem for Android™️ to update and test your Android application so that it will run on a Windows 11 device using the Amazon Appstore. [Learn more](./wsa/index.md).

## Windows as your development environment

There are multiple paths for developing an Android device app using the Windows operating system. These paths fall into three main types: **[Native Android development](#native-android)**, **[Cross-platform development](#cross-platform)**, and **[Android game development](#game-development)**. This overview will help you decide which development path to follow for developing an Android app and then provide [next steps](#next-steps) to help you get started using Windows to develop with:

- [Native Android](native-android.md)
- [.NET MAUI](/dotnet/maui/what-is-maui)
- [React Native](../dev-environment/javascript/react-native-for-android.md)
- [PWA with Cordova or Ionic](pwa.md)
- [C/C++ for game development](native-android.md#use-c-or-c-for-android-game-development)

*If you have been using Xamarin for cross-platform apps, see [Migrate from Xamarin to .NET MAUI](/dotnet/maui/migration/).

In addition, this guide will provide tips on using Windows to:

- [Test on an Android device or emulator](emulator.md)
- [Update Windows Defender settings to improve performance](defender-settings.md)
- [Develop dual-screen apps for Android and get the Surface Duo device SDK](/dual-screen/android/)

### Native Android

[Native Android development on Windows](./native-android.md) means that your app is targeting only Android (not iOS or Windows devices). You can use [Android Studio](https://developer.android.com/studio/install#windows) or [Visual Studio](https://visualstudio.microsoft.com/vs/android/) to develop within the ecosystem designed specifically for the Android operating system. Performance will be optimized for Android devices, the user-interface look and feel will be consistent with other native apps on the device, and any features or capabilities of the user's device will be straight-forward to access and utilize. Developing your app in a native format will help it to just 'feel right' because it follows all of the interaction patterns and user experience standards established specifically for Android devices.

### Cross-platform

Cross-platform frameworks provide a single codebase that can (mostly) be shared between Android, iOS, and Windows devices. Using a cross-platform framework can help your app to maintain the same look, feel, and experience across device platforms, as well as benefiting from the automatic rollout of updates and fixes. Instead of needing to understand a variety of device-specific code languages, the app is developed in a shared codebase, typically in one language.

While cross-platform frameworks aim to look and feel as close to native apps as possible, they will never be as seamlessly integrated as a natively developed app and may suffer from reduced speed and degraded performance. Additionally, the tools used to build cross-platform apps may not have all of the features offered by each different device platform, potentially requiring workarounds.

A codebase is typically made up of **UI code**, for creating the user interface like pages, buttons controls, labels, lists, etc., and **logic code**, for calling web services, accessing a database, invoking hardware capabilities and managing state. On average, 90% of  this can be reused, though there is typically some need to customize code for each device platform. This generalization largely depends on the type of app you're building, but provides a bit of context that hopefully will help with your decision-making.  

### Choosing a cross-platform framework

[.NET MAUI](/dotnet/maui/)

- A cross-platform framework for creating native mobile and desktop apps with C# and XAML.
- Develop apps that can run on Android, iOS, macOS, and Windows from a single shared code-base, with deep access to every aspect of each native platform from a single unified API that enables a write-once, run-anywhere dev experience.
- Share UI layout and design across platforms.
- An open-source evolution of Xamarin.Forms, extended from mobile to desktop scenarios, with UI controls rebuilt for performance and extensibility.
- [Migrate Xamarin.Android projects to .NET MAUI](/dotnet/maui/migration/android-projects)

[React Native](../dev-environment/javascript/react-native-for-android.md)

- UI code: JavaScript
- Logic code: JavaScript
- The goal of React Native isn't to write the code once and run it on any platform, rather to learn-once (the React way) and write-anywhere.
- The community has added tools such as Expo and Create React Native App to help those wanting to build apps without using Xcode or Android Studio.
- Similar to .NET MAUI (C#), React Native (JavaScript) calls native UI elements (without the need for writing Java/Kotlin or Swift).

[Progressive Web Apps (PWAs)](pwa.md)

- UI code: HTML, CSS, JavaScript
- Logic code: JavaScript
- PWAs are web apps built with standard patterns to allow them to take advantage of both web and native app features. They can be built without a framework, but a couple of popular frameworks to consider are [Ionic](https://ionicframework.com/docs/intro) and [Apache Cordova](https://cordova.apache.org).
- PWAs can be installed on a device (Android, iOS, or Windows) and can work offline thanks to the incorporation of a service-worker.
- PWAs can be distributed and installed without an app store using only a web URL. The Microsoft Store and Google Play Store allow PWAs to be listed, the Apple Store currently does not, though they can still be installed on any iOS device running 12.2 or later.
- To learn more, check out this [introduction to PWAs](https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps/Introduction) on MDN.

### Game development

Game development for Android is often unique from developing a standard Android app since games typically use custom rendering logic, often written in OpenGL or Vulkan. For this reason, and because of the many C libraries available that support game development, it's common for developers to use [C/C++ with Visual Studio](/cpp/cross-platform/), along with the Android [Native Development Kit (NDK)](/cpp/cross-platform/create-an-android-native-activity-app), to create games for Android. [Get started with C/C++ for game development](native-android.md#use-c-or-c-for-android-game-development).

For more guidance on developing Android games, see the Android Developer site: [Game development basics](https://developer.android.com/games/guides/basics). You will find guidance on using a game engine (like Unity, Unreal, Defold, Godot), as well as using IDEs (like Android Studio or Visual Studio).

## Next steps

- [Get started with native Android development on Windows](native-android.md)
- [Get started with Windows Subsystem for Android](./wsa/index.md)
- [Get started developing for Android using .NET MAUI](/dotnet/maui)
- [Get started developing for Android using React Native](../dev-environment/javascript/react-native-for-android.md)
- [Get started developing a PWA for Android](pwa.md)
- [Develop Dual-screen apps for Android and get the Surface Duo device SDK](/dual-screen/android/)
- [Enable Virtualization support to improve emulator performance](emulator.md#enable-virtualization-support)
