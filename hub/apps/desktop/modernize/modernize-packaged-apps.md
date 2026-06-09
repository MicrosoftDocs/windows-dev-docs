---
description: Learn how to add modern experiences for Windows users in a desktop app that you have packaged in a Windows app package.
title: Modernize packaged desktop apps
ms.date: 05/11/2023
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: RS5
---

# Features that require package identity

Some [modern Windows experiences](./index.md) require your app to have [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) at runtime (in other words, your app needs to be *packaged*). Those experiences include certain Windows features, certain Windows Runtime APIs, package extensions, and UWP components.

Universal Windows Platform (UWP) apps receive package identity by default because they can be distributed only via MSIX packages. Other types of Windows apps, including WPF apps, can also be deployed via MSIX packages to obtain package identity. But apps *packaged with external location* also has package identity. For more info about these terms, see [Advantages and disadvantages of packaging your app](/windows/apps/package-and-deploy/).

Only packaged apps (including apps packaged with external location) have package identity at runtime. If your app has package identity, then you can use the following features in your app.

## Notifications

The Windows App SDK [notifications APIs](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager) require your app to have package identity.

## Integrate with package extensions

If your app needs to integrate with the system (for example, establish firewall rules), then describe those things in the package manifest of your app, and the system will do the rest. For most of these tasks, you won't have to write any code at all. With a bit of XML in the manifest, you can do things such as: start a process when the user logs on; integrate your app into File Explorer; and add your app a list of print targets that appear in other apps.

For more info, see [Integrate your desktop app with package extensions](desktop-to-uwp-extensions.md).

## Get activation info for packaged apps

Starting in Windows 10, version 1809, packaged apps can retrieve certain kinds of activation info during startup. For example, you can get info related to app activation from opening a file, from clicking an interactive toast, or from using a protocol.

For more info, see [Get activation info for packaged apps](get-activation-info-for-packaged-apps.md).

## Use the Windows App SDK in an existing project

You can use the Windows App SDK to add modern Windows features—such as WinUI 3 controls, push notifications, and app lifecycle management—to your existing Win32 or .NET desktop app without requiring a full rewrite.

For more information, see [Add Windows App SDK features to your existing project](/windows/apps/windows-app-sdk/use-windows-app-sdk-in-existing-project).

## Distribute

If you package your app in an MSIX package, then it's very easy to distribute it by publishing it to the Microsoft Store, or by sideloading it onto systems.

For more info, see [Package and deploy your app](/windows/apps/package-and-deploy/).
