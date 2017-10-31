---
author: QuinnRadich
ms.assetid: C9787269-B54F-4FFA-A884-D4A3BF28F80D
title: What's a Universal Windows Platform (UWP) app?
description: Learn about the different types of Universal Windows apps--UWP apps, Windows Phone Store apps, and Windows Runtime apps.
ms.author: quradic
ms.date: 03/22/2017
ms.topic: article
pms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, what, universal
localizationpriority: high
---

# What's a Universal Windows Platform (UWP) app?

The Universal Windows Platform (UWP) is the app platform for Windows 10. You can develop apps for UWP with just one API set, one app package, and one store to reach all Windows 10 devices – PC, tablet, phone, Xbox, HoloLens, Surface Hub and more. It’s easier to support a number of screen sizes, and also a variety of interaction models, whether it be touch, mouse and keyboard, a game controller, or a pen. At the core of UWP apps is the idea that users want their *experiences* to be mobile across ALL their devices, and they want to use whatever device is most convenient or productive for the task at hand.

UWP is also flexible: you don't have to use C# and XAML if you don't want to. Do you like developing in Unity or MonoGame? Prefer JavaScript? Not a problem, use them all you want. Have a C++ desktop app that you want to extend with UWP features and sell in the store? That's okay, too. 

The bottom line: You can spend your time working with familiar programming languages, frameworks and APIs, all in single project, and have the very same code run on the huge range of Windows hardware that exists today. Once you've written your UWP app, you can then publish it to the store for the world to see.

![Windows-powered devices](images/1894834-hig-device-primer-01-500.png)
 
##So, what *exactly* is a UWP app?

What makes a UWP app special? Here are some of the characteristics that make UWP apps on Windows 10 different.

-   **There's a common API surface across all devices.**

    The Universal Windows Platform (UWP) core APIs are the same for all classes of Windows device. If your app uses only the core APIs, it will run on any Windows 10 device, no matter if you are targeting a desktop PC, an Xbox or a Mixed Reality headset.

-   **Extension SDKs let your app do cool stuff on specific device types.**

    Extension SDKs add specialized APIs for each device class. For example, if your UWP app targets HoloLens, you can add HoloLens features in addition to the normal UWP core APIs.
    If you target the universal APIs, your app package can run on all devices that run Windows 10. But if you want your UWP app to take advantage of device specific APIs in the event it is running on a particular class of device, you can check at run-time if an API exists before calling it. 

-   **Apps are packaged using the .AppX packaging format and distributed from the Store.**

    All UWP apps are distributed as an AppX package. This provides a trustworthy installation mechanism and ensures that your apps can be deployed and updated seamlessly.

-   **There's one store for all devices.**

    After you register as an app developer, you can submit your app to the store and make it available on all types device, or only those you choose. You submit and manage all your apps for Windows devices in one place.

-   **Apps support adaptive controls and input**

    UI elements use *effective pixels* (see [Responsive design 101 for UWP apps](https://msdn.microsoft.com/library/windows/apps/Dn958435)), so they can respond with a layout that works based on the number of screen pixels available on the device. And they work well with multiple types of input such as keyboard, mouse, touch, pen, and Xbox One controllers. If you need to further tailor your UI to a specific screen size or device, new layout panels and tooling help you adapt your UI to the devices your app may run on.



## Use a language you already know


UWP apps use the Windows Runtime, a native API built into the operating system. This API is implemented in C++ and supported in C#, Visual Basic, C++, and JavaScript. Some options for writing apps in UWP include:
-   XAML UI and a C#, VB, or C++ backend
-   DirectX UI and a C++ backend
-   JavaScript and HTML

Microsoft Visual Studio 2017 provides a UWP app template for each language that lets you create a single project for all devices. When your work is finished, you can produce an app package and submit it to the Microsoft Store from within Visual Studio to get your app out to customers on any Windows 10 device.

## UWP apps come to life on Windows


On Windows, your app can deliver relevant, real-time info to your users and keep them coming back for more. In the modern app economy, your app has to be engaging to stay at the front of your users’ lives. Windows provides you with lots of resources to help keep your users returning to your app:

-   Live tiles and the lock screen show contextually relevant and timely info at a glance.

-   Push notifications bring real-time, breaking alerts to your user’s attention when they're needed.

-   The Action Center is a place where you can organize and display notifications and content that users need to take action on.

-   Background execution and triggers bring your app to life just when the user needs it.

-   Your app can use voice and Bluetooth LE devices to help users interact with the world around them.

-   Support for rich, digital ink and the innovative Dial.

-   Cortana adds personality to your software.

-   XAML provides you with the tools to create smooth, animated user interfaces.

Finally, you can use roaming data and the Windows Credential Locker to enable a consistent roaming experience across all of the Windows screens where users run your app. Roaming data gives you an easy way to store a user’s preferences and settings in the cloud, without having to build your own sync infrastructure. And you can store user credentials in the Credential Locker, where security and reliability are the top priority.

##  Monetize your app


On Windows, you can choose how you'll monetize your app—across phones, tablets, PCs, and other devices. We give you a number of ways to make money with your app and the services it delivers. All you need to do is choose the one that works best for you:

-   A paid download is the simplest option. Just name your price.
-   Trials let users try your app before buying it, providing easier discoverability and conversion than the more traditional "freemium" options.
-   Use sale prices for apps and add-ons.
-   In-app purchases and ads are also available.

## Let's get started


For a more detailed look at the UWP, read the [Guide to Universal Windows Platform apps](universal-application-platform-guide.md). 
Then, check out [Get set up](get-set-up.md) to download the tools you need to start creating apps, and then write [your first app](your-first-app.md)!


## More advanced topics

* [.NET Native - What it means for Universal Windows Platform (UWP) developers](https://blogs.windows.com/buildingapps/2015/08/20/net-native-what-it-means-for-universal-windows-platform-uwp-developers/#TYsD3tJuBJpK3Hc7.97)
* [Universal Windows apps in .NET](https://blogs.msdn.microsoft.com/dotnet/2015/07/30/universal-windows-apps-in-net)
* [.NET for UWP apps](https://msdn.microsoft.com/library/mt185501.aspx)
