---
title: Localize your WinUI 3 app
description: This guide shows you how to localize your WinUI 3 application 
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, Windows UI Library, localize, localization
ms.date: 11/12/2021
ms.topic: article
---

# Localize your WinUI 3 app

For more info about the value proposition of localizing your app, see [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal).

To support multiple languages in a WinUI desktop app, and ensure proper localization of your packaged project, add the appropriate resources to the project (see [App resources and the Resource Management System](/windows/uwp/app-resources/)) and declare each supported language in the **package.appxmanifest** file of your project. When you build the project, the specified languages are added to the generated app manifest (**AppxManifest.xml**) and the corresponding resources are used.
 > [!NOTE]
 > As [unpackaged WinUI 3 apps](create-your-first-winui3-app.md?pivots=winui3-unpackaged-csharp) do not contain a `package.appxmanifest` file, no further action is needed after [adding the appropriate resources](/windows/uwp/app-resources/localize-strings-ui-manifest#localize-the-string-resources) to the project.

1. Open the .wapproj's `package.appxmanifest` in a text editor and locate the following section:

    ```xml
    <Resources>
        <Resource Language="x-generate"/>
    </Resources>
    ```

2. Replace the `<Resource Language="x-generate">` with `<Resource />` elements for each of your supported languages. For example, the following markup specifies that "en-US" and "es-ES" localized resources are available:

    ```xml
    <Resources>
        <Resource Language="en-US"/>
        <Resource Language="es-ES"/>
    </Resources>
    ```
