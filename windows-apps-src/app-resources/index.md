---
Description: This section shows you how to author, package, and consume your app's string, image, and file resources.
title: App resources and the Resource Management System
label: Intro
template: detail.hbs
ms.date: 10/20/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# App resources and the Resource Management System


This section shows you how to author, package, and consume your app's string, image, and file resources. For example, you might package a file along with your casual game containing a definition of the game's levels, and load the file at run-time. We also show you how maintaining your resources independently of the app's logic makes it easy to localize and customize your app for different locales, device displays, accessibility settings, and other user and machine contexts. Resources such as strings and images typically need to exist in multiple language, scale, and contrast variants. For resources such as that, you have the support of the [Resource Management System](resource-management-system.md).

There are two types of app resource.
- A file resource is a resource stored as a file on disk. A file resource can contain a bitmap image, XAML, XML, HTML, or any other kind of data.
- An embedded resource is a resource that is embedded within some containing resource file. The most common example is a string resource embedded within a Resources File (.resw or .resjson).

For more info about the value proposition of localizing your app, see [Globalization and localization](../design/globalizing/globalizing-portal.md).

| Article | Description |
|---------|-------------|
| [Resource Management System](resource-management-system.md) | At build time, the Resource Management System creates an index of all the different variants of the resources that are packaged up with your app. At run-time, the system detects the user and machine settings that are in effect and loads the resources that are the best match for those settings. |
| [How the Resource Management System matches and chooses resources](how-rms-matches-and-chooses-resources.md) | When a resource is requested, there may be several candidates that match the current resource context to some degree. The Resource Management System will analyze all of the candidates and determine the best candidate to return. This topic describes that process in detail and gives examples. |
| [How the Resource Management System matches language tags](how-rms-matches-lang-tags.md) | The previous topic ([How the Resource Management System matches and chooses resources](how-rms-matches-and-chooses-resources.md)) looks at qualifier-matching in general. This topic focuses on language-tag-matching in more detail. |
| [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md) | This topic explains the general concept of resource qualifiers, how to use them, and the purpose of each of the qualifier names. |
| [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md) | If you want your app to support different display languages, and you have string literals in your code or XAML markup or app package manifest, then move those strings into a Resources File (.resw). You can then make a translated copy of that Resources File for each language that your app supports. |
| [Load images and assets tailored for scale, theme, high contrast, and others](images-tailored-for-scale-theme-contrast.md) | Your app can load image resource files containing images tailored for display scale factor, theme, high contrast, and other runtime contexts. |
| [URI schemes](uri-schemes.md) | There are several URI (Uniform Resource Identifier) schemes that you can use to refer to files that come from your app's package, your app's data folders, or the cloud. You can also use a URI scheme to refer to strings loaded from your app's Resources Files (.resw). |
| [Specify the default resources that your app uses](specify-default-resources-installed.md) | If your app doesn't have resources that match the particular settings of a customer device, then the app's default resources are used. This topic explains how to specify what those default resources are. |
| [Build resources into your app package, instead of into a resource pack](build-resources-into-app-package.md) | Some kinds of apps (multilingual dictionaries, translation tools, etc.) need to override the default behavior of an app bundle, and build resources into the app package instead of having them in separate resource packages. This topic explains how to do that. |
| [Package resource indexing (PRI) APIs and custom build systems](pri-apis-custom-build-systems.md) | With the [package resource indexing (PRI) APIs](/windows/desktop/menurc/pri-indexing-reference), you can develop a custom build system for your UWP app's resources. The build system will be able to create, version, and dump (as XML) package resource index (PRI) files to whatever level of complexity your UWP app needs. |
| [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md) | MakePri.exe is a command line tool that you can use to create and dump PRI files. It is integrated as part of MSBuild within Microsoft Visual Studio, but it could be useful to you for creating packages manually or with a custom build system. |
| [Use the Windows 10 Resource Management System in a legacy app or game](using-mrt-for-converted-desktop-apps-and-games.md) | By packaging your .NET or Win32 app or game as an .msix or .appx package, you can leverage the Resource Management System to load app resources tailored to the run-time context. This in-depth topic describes the techniques. |

Also see [Tile and toast notification support for language, scale, and high contrast](../design/shell/tiles-and-notifications/tile-toast-language-scale-contrast.md).