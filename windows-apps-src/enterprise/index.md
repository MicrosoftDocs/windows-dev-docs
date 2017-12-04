---
ms.assetid: 4b0c86d3-f05b-450b-bf9c-6ab4d3f07d31
description: This roadmap provides an overview of key enterprise features for Windows 10 and Universal Windows Platform (UWP) apps.
title: Enterprise
author: awkoren
ms.author: alkoren
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Enterprise



This roadmap provides an overview of key enterprise features for Windows 10 Universal Windows Platform (UWP) apps. Windows 10 lets you write once and deploy across all devices, creating one app that tailors to any device. This lets you build the great experiences your users expect, while providing control over the security, management, and configuration required by your organization.

**Note**  This article is targeted towards developers writing enterprise UWP apps. For general UWP development, see the [How-to guides for Windows 10 apps](https://msdn.microsoft.com/library/windows/apps/mt244352). For WPF, Windows Forms, or Win32 development, visit the [Desktop dev center](https://dev.windows.com/desktop). For IT professional resources, like deploying Windows 10 or managing enterprise security features, see [Windows 10 on TechNet](https://msdn.microsoft.com/library/dn986868).

 

## Security


Windows 10 provides a suite of security features for app developers to protect the identity of their users, the security of corporate networks, and any business data stored on devices. New for Windows 10 is Microsoft Passport, an easy-to-deploy two-factor password alternative that is accessible by using a PIN or Windows Hello, which provides enterprise grade security and supports fingerprint, facial, and iris based recognition.

| Topic | Description |
|-------|-------------|
| [Intro to secure Windows app development](https://msdn.microsoft.com/library/windows/apps/mt622741) | This introductory article explains various Windows security features across the stages of authentication, data-in-flight, and data-at-rest. It also describes how you can integrate those stages into your apps. It covers a large range of topics, and is aimed primarily at helping app architects better understand the Windows features that make creating Universal Windows   Platform apps quick and easy. |
| [Authentication and user identity](https://msdn.microsoft.com/library/windows/apps/mt270184) | UWP apps have several options for user authentication which are outlined in this article. For the enterprise, the new Microsoft Passport feature is strongly recommended. Microsoft Passport replaces passwords with strong   two-factor authentication (2FA) by verifying existing credentials and by creating a device-specific credential that a biometric or PIN-based user gesture protects, resulting in a both convenient and highly secure experience. |
| [Cryptography](https://msdn.microsoft.com/library/windows/apps/mt270191) | The cryptography section provides an overview of the cryptography features available to UWP apps. Articles range from introductory walkthroughs on how to easily encrypt sensitive business data, to advanced to advanced topics such as manipulating cryptographic keys and working with MACs, hashes, and signatures. |
| [Windows Information Protection (WIP)](wip-hub.md) | This is a hub topic covering the full developer picture of how Windows Information Protection (WIP) relates to files, buffers, clipboard, networking, background tasks, and data protection under lock. |

 

## Data binding and databases


Data binding is a way for your app's UI to display data from an external source, such as a database, and optionally to stay in sync with that data. Data binding allows you to separate the concern of data from the concern of UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app.

| Topic | Description |
|-------|-------------|
| [Data binding overview](https://msdn.microsoft.com/library/windows/apps/mt269383) | This topic shows you how to bind a control (or other UI element) to a   single item or bind an items control to a collection of items in a Universal Windows Platform (UWP) app. In addition, it shows how to control the rendering of items, implement a details view based on a selection, and convert data for display. |
| [Entity Framework 7 for UWP](https://msdn.microsoft.com/library/windows/apps/mt592863) | Performing complex queries against large data sets is vastly simplified using Entity Framework 7, which supports UWP. In this walkthrough, you will build a UWP app that performs basic data access against a local SQLite   database using Entity Framework. |
| [SQLite local database](https://channel9.msdn.com/Series/A-Developers-Guide-to-Windows-10/10) | This video is a comprehensive developer's guide to using SQLite, the recommended solution for local app databases. Visit [SQLite](https://www.sqlite.org/download.html) to download the latest version for UWP, or use the version that's already provided with the Windows 10 SDK. |

 

## Networking and data serialization


Line-of-business apps often need to communicate with or store data on a variety of other systems. This is typically accomplished by connecting to a network service (using protocols such as REST or SOAP) and then serializing or deserializing data into a common format. Working with networks and data serialization in UWP apps similar to WPF, WinForms, and ASP.NET applications. See the following articles for more information.

| Topic | Description |
|-------|-------------|
| [Networking basics](https://msdn.microsoft.com/library/windows/apps/mt280233) | This walkthrough explains basic networking concepts relevant to all UWP apps, regardless of the communication protocols in use.  |
| [Which networking technology?](https://msdn.microsoft.com/library/windows/apps/mt280235) | A quick overview of the networking technologies available for UWP apps, with suggestions on how to choose the technologies that are the best fit for your app. |
| [XML and SOAP serialization](https://msdn.microsoft.com/library/90c86ass.aspx) | XML serialization converts objects into an XML stream that conforms to a   specific XML Schema definition language (XSD). To convert between XML and a strongly-typed class, you can use the native [XDocument](https://msdn.microsoft.com/library/system.xml.linq.xdocument.aspx) class, or an external library. |
| [JSON serialization](https://msdn.microsoft.com/library/windows/apps/br240639) | JSON (JavaScript object notation) serialization is a popular format for   communicating with REST APIs. The [Newtonsoft Json.NET](http://www.newtonsoft.com/json), which is fully supported for UWP apps. |

 

## Devices


In order to integrate with line-of-business tools, like printers, barcode scanners, or smart card readers, you may find it necessary to integrate external devices or sensors into your app. Here are some examples of features that you can add to your app using the technology described in this section.

| Topic  | Description |
|--------|-------------|
| [Enumerate devices](https://msdn.microsoft.com/library/windows/apps/mt187355) | This article explains how to use the [Windows.Devices.Enumeration](https://msdn.microsoft.com/library/windows/apps/br225459) namespace to find devices that are internally connected to the system, externally connected, or detectable over wireless or networking protocols. Start here if you're building any app that works with devices. |
| [Printing and scanning](https://msdn.microsoft.com/library/windows/apps/mt204544) | Describes how to print and scan from your app, including connecting to   and working with business devices like point-of-sale (POS) systems, receipt printers, and high-capacity feeder scanners. |
| [Bluetooth](https://msdn.microsoft.com/library/windows/apps/mt270288) | In addition to using traditional Bluetooth connections to send and receive data or control devices, Windows 10 enables using Bluetooth Low Energy (BTLE) to send or receive beacons in the background. Use this to display notifications or enable functionality when a user gets close to or leaves a particular location. |
| [Enterprise shared storage](enterprise-shared-storage.md) | In device lockdown scenarios, learn how data can be shared within the same app, between instances of an app, or even between apps. |

 

## Device targeting


Many users today are bringing their own phone or tablet to work, which have varying form factors and screen sizes. With the Universal Windows Platform (UWP), you can write a single line-of-business app that runs seamlessly on all different types of devices, including desktop PCs and PPI displays, allowing you to maximize the reach of your app and the efficiency of your code.

| Topic | Description |
|-------|-------------|
| [Guide to UWP apps](https://msdn.microsoft.com/library/windows/apps/dn894631) | In this introductory guide, you'll get acquainted with the Windows 10UWP platform, including: what a device family is and how to decide which one to target, new UI controls and panels that allow you to adapt your UI to different device form factors, and how to understand and control the API surface that is available to your app. |
| [Adaptive XAML UI code sample](http://go.microsoft.com/fwlink/p/?LinkId=619992) | This code sample shows all the possible layout options and controls for   your app, regardless of device type, and allows you to interact with the panels to show how to achieve any layout you are looking for. In addition to showing how each control responds to different form factors, the app itself is responsive and shows various methods for achieving adaptive UI. |

 

## Deployment


You have options for distributing apps to your organization’s users. You can use Microsoft Store for Business, existing mobile device management or you can sideload apps to devices. You can also make your apps available to the general public by publishing to the Microsoft Store.

| Topic | Description |
|-------|-------------|
| [Distribute LOB apps to enterprises](https://msdn.microsoft.com/library/windows/apps/mt608995) | You can publish line-of-business apps directly to enterprises for volume acquisition via the Microsoft Store for Business, without making the apps broadly available to the public. |
| [Sideload apps](https://technet.microsoft.com/library/mt269549) | When you sideload an app, you deploy a signed app package to a device. You maintain the signing, hosting, and deployment of these apps. The process for sideloading apps is streamlined for Windows 10.             |
| [Publish apps to the Microsoft Store](https://dev.windows.com/publish) | The unified Microsoft Store lets you publish and manage all of your apps for all Windows devices. Customize your app’s availability with per-market pricing, distribution and visibility controls, and other options. |

 

## Patterns and practices


Code bases for large scale, enterprise-grade apps can become unwieldy. Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in WPF, Windows 10 UWP, and Xamarin Forms. Prism provides an implementation of a collection of design patterns that are helpful in writing well-structured and maintainable XAML applications, including MVVM, dependency injection, commands, EventAggregator, and others.

For more information on Prism, see the [GitHub repo](https://github.com/PrismLibrary/Prism).

 

 
