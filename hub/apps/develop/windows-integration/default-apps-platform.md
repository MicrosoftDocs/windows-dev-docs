---
title: Windows app defaults platform
description: Learn how to integrate with the default apps platform on Windows and how to direct users to change the default app settings in Windows 11. 
ms.topic: article
ms.date: 10/25/2024
ms.localizationpriority: medium
---

# Windows app defaults platform

This article provides information for Windows app developers about how to integrate with the default apps platform on Windows and how to direct users to change the default app settings in Windows 11.

The app defaults platform enables developers to register their apps for supporting file and link types on Windows in support of two main scenarios:

* Allow users to specify which apps Windows uses to open specific file types or link types using the **Default apps** UX in **Settings**.
* Allow developers to facilitate app-to-app launches by invoking a file or link type.

## Default app experience for end users

Windows 11 allows users to change default apps via Windows Settings and other system UI.

* Windows will automatically prompt the user when they open a file or link type when a new app is installed that registered for that file or link type.
* Apps can also direct the user to Settings to change default app settings, guiding users through this process using in-app prompts or documentation.

## Default app settings for app developers

Your app can register to become the default handler for a file and link types. Both Windows desktop applications and WinUI apps can register to be a default handler. If the user chooses your app as the default handler, Windows will activate your app when that type of file or link is invoked.

## Default apps platform best practices for developers

* Use the `ms-settings:defaultapps` URI to launch the Default Apps settings page or your app’s page within Default Apps directly. For more information, see [Launch the Default Apps settings page](/windows/apps/develop/launch/launch-default-apps-settings).
* Prompt users thoughtfully. Use contextual prompts when your app opens a file type it supports but is not the default.
* Provide Clear Instructions: Include screenshots or step-by-step guides in your app or support site.
* Respect User Choice: Avoid aggressive prompts or repeated notifications.
* Only register for a type if you expect to handle all launches for that type. For example, if your app only needs to use the file type internally, then you don't need to register to be the default handler. If you do choose to register for a type, you must provide the end user with the functionality that is expected when your app is activated for that type.

## Use default app link types to perform app-to-app launches

Apps can hand off to another app, by calling  [Launcher.LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync) for example, to tell Windows to launch the user configured default app for the specified link type.

Apps can direct Windows to use the user configured default by invoking a well-known URI scheme, such as `https:`.

Apps can explicitly choose another app to launch if they know the link type it registers in the app defaults platform, such as `ms-settings:`.

URI schemes may be an official standard, documented publicly, or proprietary. For example,

* `https:` is documented as a Permanent scheme by the Internet Assigned Numbers Authority as RFC8615.
* Spotify publicly documents a `spotify:` scheme, see [Spotify URIs and IDs](https://developer.spotify.com/documentation/web-api/concepts/spotify-uris-ids).
* Other schemes may be proprietary and would create a broken end-to-end experience for the user if an app registers and is set for the default for a URI scheme it doesn’t know how to implement.

## Register for file and link types

All apps can participate in the app defaults platform by registering for types they support. Packaged apps can use the [uap:FileTypeAssociation](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation) element in their app package manifest schema file to declare supported file types. Other file association mechanisms are supported for non-packaged apps. For more information see [File Types and File Associations](/windows/win32/shell/fa-intro) and [Guidelines for File Associations and Default Programs](/windows/win32/shell/fa-intro).

## Handle activation

When a Windows app is launched, Windows provides information to the app that allows it to determine that it has been launched from a file association or link type invocation. For more information, see [Handle file activation in a Windows app](/windows/apps/develop/launch/handle-file-activation) and [Handle URI activation with a Windows app](/windows/apps/develop/launch/handle-uri-activation).

## Security considerations for the app defaults platform

To help protect users' default app choices from malware changing settings without the user being aware, Windows requires that app default settings must be set through the Windows system UI.

* Windows does not allow programmatic changes to default apps without user interaction in system UI. For more information, see [App defaults in managed environments](#app-defaults-in-managed-environments)
* User setting data for app defaults is obfuscated in registry data stores. Registry-based changes are not supported for apps.
* User setting data for app defaults are protected by a Windows filter driver (UCPD.sys) that blocks apps from writing app defaults data.
* Apps can query which app is the default for a given type. For more information, see [IApplicationAssociationRegistration::QueryCurrentDefault](/windows/win32/api/shobjidl_core/nf-shobjidl_core-iapplicationassociationregistration-querycurrentdefault).
* Apps that are distributed by the Microsoft Store must abide by Microsoft Store policy, specifically [Section 10.2.8](/windows/apps/publish/store-policies#102-security) which requires that apps only use supported methods for updating Windows settings, including app default settings.


## App defaults in managed environments

On a managed PC, IT admins can control app defaults through policy. The app defaults platform provides Group Policy and Mobile Device Management (MDM) policies to facilitate these management scenarios. These policies also work with roaming user profiles to support more complex environments. Solutions that don’t use these policies may not work correctly because of the security considerations noted above. For more information, see [ApplicationDefaults Policy CSP](/windows/client-management/mdm/policy-csp-applicationdefaults) and [Deploy roaming user profiles](/windows-server/storage/folder-redirection/deploy-roaming-user-profiles)

