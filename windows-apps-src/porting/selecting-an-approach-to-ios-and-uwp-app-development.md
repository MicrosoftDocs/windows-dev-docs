---
description: Learn about tools and techniques that can help you write apps that support Windows, iOS, and Android.
title: Selecting an approach to iOS and UWP app development
ms.assetid: 5CDAB313-07B7-4A32-A49B-026361DCC853
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Selecting an approach to iOS and UWP app development


What are the choices when developing cross-platform apps?

## What's the best way to support both iOS and Windows?

Windows and iOS may seem to be very different beasts, but a growing number of tools and techniques can greatly assist you if you need to write apps that support both platforms (and Android too). The best solution depends on the type of app you are writing, and whether you are starting from scratch or porting an existing project.

## Writing a new app

With a clean slate, you have many options at your disposal, including:

-   [Xamarin](https://xamarin.com/)

    With Xamarin, you can write your app in C#, have it run on Windows, and create native iOS apps too. Support for Xamarin is built into Visual Studio; just select the correct project type.

-   [Apache Cordova](https://www.microsoft.com/?ref=go)

    If Javascript and HTML is more your thing, Apache Cordova (aka PhoneGap) will help you create cross-platform apps for iOS, Windows, and Android. This project type is also built into Visual Studio.

-   Game-engines

    With tools like [Unity3D](https://www.unity3d.com/) and [Unreal Engine](https://www.unrealengine.com/en-US/) at your disposal, you can write AAA-quality games for Windows and many other platforms, including iOS. Unity supports C# scripting; Unreal uses C++.

-   [MonoGame](http://www.monogame.net/)

    The spiritual successor to XNA. Now, it's an open-source cross-platform framework, which means you can write apps in C# for many platforms with support for physics engines, and 2D and 3D graphics.

## Adapting an existing app

With an existing iOS app, your options are a little more limited. However, all is most certainly not lost.

-   [Windows Bridge for iOS](https://github.com/Microsoft/WinObjC)

    Also known as Project Islandwood, this is a still-in-development tool that can import Xcode projects directly into Visual Studio. Objective-C code can be built and debugged from within Visual Studio. If your project makes use of libraries such as Cocos for graphics, you might find this a useful way to quickly port your app.

-   Repurpose your C++ code.

    If your core business logic is written in C++, rather than Objective-C or Swift, you can often use this code with only minor changes in your project. You can then use XAML to define your UI, as with other Windows apps, and call into the C++ code when necessary.

-   [Use ANGLE to run OpenGL ES on Windows](https://github.com/microsoft/angle/wiki)

    An intermediate step to porting your OpenGL ES 2.0 project is to use ANGLE. ANGLE allows you to run OpenGL ES content on Windows by translating OpenGL ES API calls to DirectX 11 API calls.

## Other cross-platform authoring tools

-   [GameSalad](https://gamesalad.com/)

    A game authoring environment.

-   [Construct 2]( https://www.scirra.com/)

    A game authoring environment.

-   [Titanium Studio](https://www.appcelerator.com/platform/titanium-studio/)

    A cross-platform authoring environment.

-   [Cocos2D-x](https://www.cocos2d-x.org/)

    A cross-platform code library for sprite handling and physics modeling.

-   [Impact.js](https://impactjs.com/)

    An HTML based game library.

-   [Marmalade](http://madewithmarmalade.com/)

    A cross-platform SDK.

-   [OpenFL](https://www.openfl.org/)

    A cross-platform development tool.

-   [GameMaker](https://www.yoyogames.com/gamemaker/studio)

    An authoring environment specifically for games.

-   [PlayCanvas](https://playcanvas.com/)

    An HTML based game development tool.

