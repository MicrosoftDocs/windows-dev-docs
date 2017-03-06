---
author: QuinnRadich
title: What's New in Windows 10 Creators Update preview - Develop UWP apps
description: Preview the Windows 10 Creators Update, which will continue to provide the tools, features, and experiences powered by the Universal Windows Platform.
ms.assetid: 27a9ce65-c811-4f79-bf65-3493337199c8
keywords: what's new, whats new, preview, update, updates, features, new, Windows 10, creators
ms.author: quradic
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# What's New in the Windows 10 Creators Update preview

The Windows 10 Creators Update will continue to provide the tools, features, and experiences powered by the Universal Windows Platform. [Install the tools and SDK](http://go.microsoft.com/fwlink/?LinkId=821431) on Windows 10 and you’re ready to either [create a new Universal Windows app](https://msdn.microsoft.com/library/windows/apps/bg124288) or explore how you can use your [existing app code on Windows](https://msdn.microsoft.com/library/windows/apps/mt238321).

These features will not be made publically available until the release of the Windows 10 Creators Update. For now, they can be used in preview builds accesible to [Windows Insiders](https://insider.windows.com/). This page may be updated with information on more upcoming features as more documentation becomes available.

Additionally, you can [explore prerelease documentation for new and updated API namespaces in preview builds.](windows-10-build-15021-api-diff.md)

For more information on the highlighted features of this and other Windows updates, see [the Windows Developer Day site](https://developer.microsoft.com/en-us/windows/projects/campaigns/windows-developer-day) and [What's cool in Windows 10](http://go.microsoft.com/fwlink/?LinkId=823181). In addition, see [Windows Developer Platform features](https://developer.microsoft.com/en-us/windows/platform/features) for a high-level overview of both past and future feature additions.

Feature | Description
 :---- | :----
**Modernized UWP Docs** | Universal Windows Platform documentation is now available on docs.microsoft.com. You can now easily submit code samples, add your technical expertise, or provide feedback. For instructions, see this [One Dev Minute video.](https://channel9.msdn.com/Blogs/One-Dev-Minute/Modernizing-the-Windows-UWP-Docs)
**Customer orders database sample update** | The [Customer orders database](https://github.com/Microsoft/Windows-appsample-customers-orders-database) sample on GitHub was updated to make use of the data grid control and data entry validation from Telerik, which is part of their UI for UWP suite. The UI for UWP suite is a collection of over 20 controls that is available as an open source project through the .NET foundation.
**Ink Analysis** | Windows Ink can now categorize ink strokes into either writing or drawing strokes, and recognize text, shapes, and basic layout structures.
**Project Rome SDK for Android** | The Project Rome feature for UWP has come to the Android platform. Now you can use a Windows *or* Android device to remotely launch apps and continue tasks on any of your Windows devices. See the official [Project Rome repo for cross-platform scenarios](https://github.com/Microsoft/project-rome) to get started.
**XAML Controls** | ContentDialog now has three buttons: Primary, Secondary, and Close. You can also set one of the buttons to be the Default action. <br> Use the ShowAsMonochrome property to show bitmap icons in a single color or full color. <br> Use the new SelectionChangedTrigger to change how the ComboBox handles selection by keyboard. <br> New PrepareConnectedAnimation and TryStartConnectedAnimationAsync APIs on ListViewBase make it easier to use connected animations with list and grid views. <br> Use the new Icon property to add an icon to a MenuFlyoutItem or MenuFlyoutSubItem. <br> Use the SvgImageSource class to add an SVG image in XAML. <br> Use the LoadedImageSource class to add a composition surface in XAML. <br> Use the XAMLLight class and UIElement.Lights property to add CompositionLight effects in XAML. <br> Use the XamlCompositionBrushBase to use composition brushes in XAML.
