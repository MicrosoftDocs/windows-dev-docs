---
title: Overview of Android development on Windows
description: A guide to help you get started developing for Android on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows, xamarin.android, react native, cordova, ionic, phonegap, c++ android game, windows defender, emulator
ms.date: 04/28/2020
---

# Overview of Android development on Windows

There are multiple paths for developing an Android device app using the Windows operating system. These paths fall into three main types: **[Native Android development](#native-android)**, **[Cross-platform development](#cross-platform)**, and **[Android game development](#game-development)**. This overview will help you decide which development path to follow for developing an Android app and then provide [next steps](#next-steps) to help you get started using Windows to develop with:

- [Native Android](native-android.md)
- [Xamarin.Android](xamarin-android.md)
- [Xamarin.Forms](xamarin-forms.md)
- [React Native](react-native.md)
- [Cordova, Ionic, or PhoneGap](pwa.md)
- [C/C++ for game development](native-android.md#use-c-or-c-for-android-game-development)

In addition, this guide will provide tips on using Windows to:

- [Test on an Android device or emulator](emulator.md)
- [Update Windows Defender settings to improve performance](defender-settings.md)
- [Develop dual-screen apps for Android and get the Surface Duo device SDK](/dual-screen/android/)

## Native Android

[Native Android development on Windows](./native-android.md) means that your app is targeting only Android (not iOS or Windows devices). You can use [Android Studio](https://developer.android.com/studio/install#windows) or [Visual Studio](https://visualstudio.microsoft.com/vs/android/) to develop within the ecosystem designed specifically for the Android operating system. Performance will be optimized for Android devices, the user-interface look and feel will be consistent with other native apps on the device, and any features or capabilities of the user's device will be straight-forward to access and utilize. Developing your app in a native format will help it to just 'feel right' because it follows all of the interaction patterns and user experience standards established specifically for Android devices.

## Cross-platform

Cross-platform frameworks provide a single codebase that can (mostly) be shared between Android, iOS, and Windows devices. Using a cross-platform framework can help your app to maintain the same look, feel, and experience across device platforms, as well as benefiting from the automatic rollout of updates and fixes. Instead of needing to understand a variety of device-specific code languages, the app is developed in a shared codebase, typically in one language.

While cross-platform frameworks aim to look and feel as close to native apps as possible, they will never be as seamlessly integrated as a natively developed app and may suffer from reduced speed and degraded performance. Additionally, the tools used to build cross-platform apps may not have all of the features offered by each different device platform, potentially requiring workarounds.

A codebase is typically made up of **UI code**, for creating the user interface like pages, buttons controls, labels, lists, etc., and **logic code**, for calling web services, accessing a database, invoking hardware capabilities and managing state. On average, 90% of  this can be reused, though there is typically some need to customize code for each device platform. This generalization largely depends on the type of app you're building, but provides a bit of context that hopefully will help with your decision-making.  

## Choosing a cross-platform framework

[Xamarin Native (Xamarin.Android)](xamarin-android.md)

- UI code: XML with Android Designer, and Material Theme
- Logic code: C# or F#
- Still able to tap into some native Android elements, but good for reuse of the code base for other platforms (iOS, Windows).
- Only logic code is shared across platforms, not UI code.
- Great for more complex apps with a device-specific user interface.

[Xamarin Forms (Xamarin.Forms)](xamarin-forms.md)

- UI code: XAML and .NET (with Visual Studio)
- Logic code: C#
- Shares around 60–90% of the logic and UI code across Android, iOS, and Windows device apps. 
- Uses common user controls like Button, Label, Entry, ListView, StackLayout, Calendar, TabbedPage, etc. Create a Button and Xamarin Forms will figure out how to call the native button for each platform using the Binding Library to call Java or Swift code from C#.
- Great for simple apps, like internal or Line Of Business (LOB) apps, prototypes or MVPs. Any app that can look somewhat standard or generic, utilizing a simple user interface.

[React Native](react-native.md)

- UI code: JavaScript
- Logic code: JavaScript
- The goal of React Native isn't to write the code once and run it on any platform, rather to learn-once (the React way) and write-anywhere.
- The community has added tools such as Expo and Create React Native App to help those wanting to build apps without using Xcode or Android Studio.
- Similar to Xamarin (C#), React Native (JavaScript) calls native UI elements (without the need for writing Java/Kotlin or Swift).

[Progressive Web Apps (PWAs)](pwa.md)

- UI code: HTML, CSS, JavaScript
- Logic code: JavaScript
- PWAs are web apps built with standard patterns to allow them to take advantage of both web and native app features. They can be built without a framework, but a couple of popular frameworks to consider are [Ionic](https://ionicframework.com/docs/intro) and [PhoneGap](https://phonegap.com/about/).
- PWAs can be installed on a device (Android, iOS, or Windows) and can work offline thanks to the incorporation of a service-worker.
- PWAs can be distributed and installed without an app store using only a web URL. The Microsoft Store and Google Play Store allow PWAs to be listed, the Apple Store currently does not, though they can still be installed on any iOS device running 12.2 or later.
- To learn more, check out this [introduction to PWAs](https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps/Introduction) on MDN.

## Game development

Game development for Android is often unique from developing a standard Android app since games typically use custom rendering logic, often written in OpenGL or Vulkan. For this reason, and because of the many C libraries available that support game development, it's common for developers to use [C/C++ with Visual Studio](/cpp/cross-platform/?view=vs-2019), along with the Android [Native Development Kit (NDK)](/cpp/cross-platform/create-an-android-native-activity-app?view=vs-2019), to create games for Android. [Get started with C/C++ for game development](native-android.md#use-c-or-c-for-android-game-development).

Another common path for developing games for Android is to use a game engine. There are many free and open-source engines available, such as [Unity with Visual Studio](/visualstudio/cross-platform/visual-studio-tools-for-unity?view=vs-2019), [Unreal Engine](https://docs.unrealengine.com/en-US/Platforms/Mobile/Android/GettingStarted/index.html), [MonoGame with Xamarin](/xamarin/graphics-games/monogame/introduction/), [UrhoSharp with Xamarin](/xamarin/graphics-games/urhosharp/introduction), [SkiaSharp with Xamarin.Forms](/xamarin/xamarin-forms/user-interface/graphics/skiasharp/) CocoonJS, App Game Kit, Fusion, Corona SDK, Cocos 2d, and more.

## Next steps

- [Get started with native Android development on Windows](native-android.md)
- [Get started developing for Android using Xamarin.Android](xamarin-android.md)
- [Get started developing for Android using Xamarin.Forms](xamarin-forms.md)
- [Get started developing for Android using React Native](react-native.md)
- [Get started developing a PWA for Android](pwa.md)
- [Develop Dual-screen apps for Android and get the Surface Duo device SDK](/dual-screen/android/)
- [Add Windows Defender exclusions to improve performance](defender-settings.md)
- [Enable Virtualization support to improve emulator performance](emulator.md#enable-virtualization-support)