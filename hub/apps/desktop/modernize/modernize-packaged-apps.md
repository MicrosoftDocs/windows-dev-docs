---
Description: Learn how to add modern experiences for Windows 10 users in a desktop application that you have packaged in a Windows app package.
title: Modernize packaged desktop apps
ms.date: 04/22/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: RS5
---

# Features that require package identity

If you want to update your desktop app with [modern Windows 10 experiences](index.md), many features are available only in desktop apps that are packaged in an MSIX package.

MSIX is a modern Windows app package format that provides a universal packaging experience for all Windows apps, WPF, Windows Forms and Win32 apps. Packaging your desktop Windows apps enables you to integrate modern Windows 10 experiences such as live tiles and notifications into your apps. It also gets you access to a robust installation and updating experience, a managed security model with a flexible capability system, support for the Microsoft Store, enterprise management, and many custom distribution models. For more information, see [Package desktop applications](https://docs.microsoft.com/windows/msix/desktop/desktop-to-uwp-root) in the MSIX documentation.

If you package your desktop app, you can then use UWP APIs that require package identity, package extensions, and UWP components in your packaged app. For more information, see these articles.

## Use UWP APIs that require package identity

Some UWP APIs require [package identity](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-identity) to be used in a desktop app. The MSIX package (including the package manifest) provides this identity.

For more information, see [this list of APIs](desktop-to-uwp-supported-api.md#list-of-apis).

## Integrate with package extensions

If your application needs to integrate with the system (For example: establish firewall rules), describe those things in the package manifest of your application and the system will do the rest. For most of these tasks, you won't have to write any code at all. With a bit of XML in the manifest, you can do things like start a process when the user logs on, integrate your application into File Explorer, and add your application a list of print targets that appear in other apps.

For more information, see [Integrate your desktop app with package extensions](desktop-to-uwp-extensions.md).

## Extend with UWP components

Some Windows 10 experiences (For example: a touch-enabled UI page) must run inside of a modern app container. In general, you should first determine whether you can add your experience by [enhancing](desktop-to-uwp-enhance.md) your existing desktop application with UWP APIs. If you have to use a UWP component, to achieve the experience, then you can add a UWP project to your solution and use app services to communicate between your desktop application and the UWP component.

For more information, see [Extend your desktop app with UWP components](desktop-to-uwp-extend.md).

## Distribute

You can distribute your application by publishing it the Microsoft Store or by sideloading it onto other systems.

See [Distribute your packaged desktop app](desktop-to-uwp-distribute.md).
