---
title: 'Windows app restore: maximize the value of your app'
description: This topic defines the tenets of Windows app restore that will enable your app to deliver the best backup and restore experience it can.
ms.topic: article
ms.date: 09/29/2023
keywords: Windows, App, SDK, Windows app restore
ms.localizationpriority: medium
---

# Windows app restore: maximize the value of your app

To maximize retention of your users as they move to a new device, your app (in conjunction with Windows app restore) should offer the best possible restore experience. This topic defines the tenets of Windows app restore that will enable your app to deliver that experience and keep as many of your customers engaged as possible.

## Why app restore is critical

When the users of your app move to their next Windows PC, they need the peace of mind that their apps will transfer over to their new device.

Windows will back up the user's app list to the cloud; during restore, it will create pinned app placeholders on the new Windows PC so that users find their apps right where they expect them to be. This makes installation of the app very straightforward. However, in addition to the application install, users also want the rest of their app configuration and settings to transfer over to their new device.

If you ensure that your apps are following best practices to maximize the effectiveness of Windows app restore capabilities, then your users will be able to get back to productivity as quickly as possible on their new PC. This will, in turn, allow your app to retain your users on their new PC. The rest of this topic discusses those best practices.

## App restore tenets

These tenets are guidelines and best practices for you to enable an optimal backup and restore experience for the users of your apps. These are a collection of existing best practices that we've gathered.

* [**Publish your app to the Microsoft Store**](#publish-your-app-to-the-microsoft-store). Your app should enable trusted distribution through the Microsoft Store (see [Publish Windows apps and games](/windows/apps/publish/)). This is the easiest way for you to provide a trusted and seamless (think fewer clicks) experience for your users.
* [**Package your app**](#package-your-app). Your app should be *packaged* (for definitions, see [Deployment overview](/windows/apps/package-and-deploy/)). A packaged app enables the system to better understand the files, data, and settings that matter to an app; and enables the system to more easily restore apps on demand.
* [**Store critical app state in the cloud**](#store-critical-app-state-in-the-cloud). Your app should store its critical app state in the cloud. Having an app installed on a new device is only the first step. Getting users seamless back to their prior app state&mdash;their recents, their favorites, their preferences&mdash;is the goal; and the best way to do this is to store that critical user state information in the cloud. Local state should be thought of as only a temporary optimization.
* [**Write user-generated content to Known Folders**](#write-user-generated-content-to-known-folders). Your app should write user-generated content to the Windows known folders (see the [**KnownFolders**](/uwp/api/windows.storage.knownfolders) class). Keep it separate from app state&mdash;if your app produces user-generated content (files, sound clips, videos, etc.), then you should write that content to the Windows Known Folders (Documents, Pictures, Music, Videos, etc.). This enables Windows, via OneDrive, to back up those files to the cloud and fluidly keep in sync across devices using its files-on-demand technology.

## Publish your app to the Microsoft Store

The Microsoft Store is the most reliable distribution cloud for your Windows apps (see [Publish Windows apps and games](/windows/apps/publish/)). Users can easily search for and find your app for installation.

When a user installs a new operating system (OS), and chooses to restore from their previous PC, those apps that are in the Microsoft Store will automatically be listed in the **All Apps** list, and have pins available in the same locations on the **Start** menu and taskbar as before. Those shortcuts allow the user to immediately access the app and install it.

## Package your app

Another key to creating a great experience for your users is ensuring that the app gets installed, and behaves correctly. The best way to do that is with a *packaged* app (see [Deployment overview](/windows/apps/package-and-deploy/)).

A packaged app (either a packaged desktop app or a Universal Windows Platform app) is packaged using MSIX, and it's run inside of a lightweight app container. The packaged app process and its child processes run inside the container; and they're isolated using file system and registry virtualization. It's these aspects of packaging that make installation extremely reliable, and ensure that the app doesn't misbehave or leave registry configuration or app files on the PC when the user uninstalls.

For more info about the benefits of using MSIX for packaging, see [What is MSIX?](/windows/msix/overview).

The benefits of the MSIX format, and packaged apps, don't end with app reliability. Packaging your app also means that it will be able to be quickly installed when users migrate to a new PC. Following an install, Windows will begin rehydrating the packaged apps that it restored. Since rehydrating takes time, if the user clicks the link before the app is rehydrated, then Windows will immediately download and install the app, allowing the user to run it as early as possible.

## Store critical app state in the cloud

As you can see, Windows does a great job in helping your customers find and install your app on their new PC. But what about app data, such as app settings? To deliver the best user experience, we recommend that you use the cloud to store your app's state. By storing app data in the cloud, your users can have a consistent experience across devices. And when users don't need to reconfigure their app settings, your user satisfaction rises dramatically.

Storing app settings to the cloud requires a service. To provide as rich an experience as possible, Microsoft provides a variety of services that eliminate the need to spin up servers, or pick your database, or worry about scale or security. Those services provide a great developer experience that lets you store application data in the cloud by using SQL or NoSQL APIs. To help build scalable and robust applications, you can also sync data on all devices, and enable the application to work with or without a network connection. For more info about Microsoft services, see [Store, sync, and query mobile application data from the cloud](/azure/developer/mobile-apps/data-storage).

For more info about the best practices of storing app data, see [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data).

## Write user-generated content to Known Folders

Windows introduced [known folders](/windows/win32/shell/known-folders) with Windows Vista. Since that time, users have come to expect that they can find the content they create with their apps in those locations. Writing user-generated content to those locations has the added benefit that OneDrive will back up those folders, if enabled, to ensure they're available to the user on their new PC (see [Back up your folders with OneDrive](https://support.microsoft.com/office/back-up-your-folders-with-onedrive-d61a7930-a6fb-4b95-b28a-6552e77c3057)). By using standard Windows APIs to write your user-generated content to the known folders, you're improving the user experience, and decreasing friction in adopting your app.

### User-visible files

You should store files that you wish a user to see and to interact with in the appropriate folder in the user's profile. You should store general files in the `FOLDERID_Documents` location; typically in a sub-folder. And you should store pictures, music, and video in their appropriate `FOLDERID_Pictures`, `FOLDERID_Music`, and `FOLDERID_Videos` locations.

### Machine-specific app data

You should store data that's specific to the machine on which the app is currently running in the `FOLDERID_LocalAppData` folder; normally in a sub-folder. That includes data such as:

* System performance metrics. Information gathered and persisted about the current machine, and used to optimize the behavior of the app on that specific machine. For example, if you've gathered info about the machine's graphics capabilities and performance (in order to determine the optimal rendering quality), then you shouldn't roam that data.
* User customizations connected with machine-specific capabilities. An app that optimizes its rendering performance based on the machine's graphics capabilities and performance should also store any changes that it allows the user to make to those preferences as machine-specific data. That ensures that the user enjoys what they determine to be the best experience for the machine they happen to be running on the app on.

> [!TIP]
> The reason we advise not to store machine-specific data in known folders is that those user-specific folders travel with the user between machines (they *roam*). So storing machine-specific data can result in conflicts and problems when users use your app on multiple machines, or after an upgrade.

### App data that's not machine-specific

You should store data that's not machine-specific in the `FOLDERID_Documents` location; typically in a sub-folder. Those files often contain user-provided app customization such as: default action to perform on launch; custom backgrounds; or other data that shouldn't change from one machine to another.

## Best practices for unpackaged apps

If you can't package your app, then be sure that your installer implements the recommendations below. That will ensure that it's possible to backup and restore the **Start** menu shortcuts that enable installing on a new machine that's restored from backup.

* Make sure that your installer specifies an `InstallLocation` value in its uninstall registry key. When using [Windows Installer](/windows/win32/msi/windows-installer-portal) specify this using [ARPINSTALLLOCATION](/windows/win32/msi/arpinstalllocation). That's needed in order to enable the mapping of the **Start** menu shortcuts to the product.
* Make sure that that location is specific to the product; usually the sub-directory under `C:\Program Files\<Publisher>\<Application>`.
* Make sure that your **Start** menu shortcuts have machine-independent `System.AppUserModel.ID` (AMUID) values. That's best done by specifying them explicitly in the shortcut metadata. For more info, see [Where to Assign an AppUserModelID](/windows/win32/shell/appids#where-to-assign-an-appusermodelid).
