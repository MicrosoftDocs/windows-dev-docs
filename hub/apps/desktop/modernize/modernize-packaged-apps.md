---
description: Learn how to add modern experiences for Windows users in a desktop application that you have packaged in a Windows app package.
title: Modernize packaged desktop apps
ms.date: 04/22/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: RS5
---

# Features that require package identity

Some [modern Windows experiences](index.md) are available only to desktop apps that have [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) at runtime. These features include certain Windows Runtime APIs, package extensions, and UWP components.

Only packaged apps have package identity at runtime. For definitions of apps that are packaged, unpackaged, and packaged with external location, see [Deployment overview](/windows/apps/package-and-deploy/). If your desktop app has package identity, then you can use the following features in your app.

## Integrate with package extensions

If your application needs to integrate with the system (For example: establish firewall rules), describe those things in the package manifest of your application and the system will do the rest. For most of these tasks, you won't have to write any code at all. With a bit of XML in the manifest, you can do things like start a process when the user logs on, integrate your application into File Explorer, and add your application a list of print targets that appear in other apps.

For more information, see [Integrate your desktop app with package extensions](desktop-to-uwp-extensions.md).

## Get activation info for packaged apps

Starting in Windows 10, version 1809, packaged desktop apps can retrieve certain kinds of activation info during startup. For example, you can get info related to app activation from opening a file, clicking an interactive toast, or using a protocol.

For more information, see [Get activation info for packaged apps](get-activation-info-for-packaged-apps.md).

## Extend with UWP components

Some Windows experiences (For example: a touch-enabled UI page) must run inside of a modern app container. In general, you should first determine whether you can add your experience by [enhancing](desktop-to-uwp-enhance.md) your existing desktop application with Windows Runtime APIs. If you have to use a UWP component, to achieve the experience, then you can add a UWP project to your solution and use app services to communicate between your desktop application and the UWP component.

For more information, see [Extend your desktop app with UWP components](desktop-to-uwp-extend.md).

## Distribute

If you package your app in an MSIX package, you can distribute it by publishing it the Microsoft Store or by sideloading it onto other systems.

See [Distribute your packaged desktop app](desktop-to-uwp-distribute.md).