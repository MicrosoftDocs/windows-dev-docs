---
title: What's New in Windows Docs in December 2017 - Develop UWP apps
description: New features, videos, and developer guidance have been added to the Windows 10 developer documentation for December 2017
keywords: what's new, update, features, developer guidance, Windows 10, december
ms.date: 12/14/2017
ms.topic: article


ms.localizationpriority: medium
---
# What's New in the Windows Developer Docs in December 2017

The Windows Developer Documentation is constantly being updated with information on new features available to developers across the Windows platform. The following feature overviews, developer guidance, and samples have been made available after the release of the Fall Creators Update, containing new and updated information for Windows developers.

[Install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank) on Windows 10 and you’re ready to either [create a new Universal Windows app](../get-started/create-uwp-apps.md) or explore how you can use your [existing app code on Windows](../porting/index.md).

## Features

### Windows Mixed Reality: Enthusiast's Guide

Targeting tech enthusiasts diving in to the world of Mixed Reality, the [Enthusiast Guide](/windows/mixed-reality/enthusiast-guide/) answers the top questions people have about Windows Mixed Reality. 

In the guide you will find: 
- Before you buy FAQs, 
- How-to check your PC's compatibility, 
- Setup instructions, 
- How to use your headset and controllers, 
- How to download and play immersive games, 360 videos, 2D apps, WebVR, and SteamVR, 
- How to troubleshoot issues, and more.

![Windows Mixed Reality headset user and motion controllers](images/BeforeYouBegin-tile.jpg)

### Keyboard Interactions

Design and optimize your UWP apps to provide both an accessible experience and features for power users with updated [Keyboard interactions](../design/input/keyboard-interactions.md). We've updated our recommendations and guidance to reflect the new improvements to these interactions added in the Fall Creators Update.

See [Keyboard accelerators](../design/input/keyboard-accelerators.md) and [Custom keyboard interactions](../design/input/focus-navigation.md) to expand the keyboard functionality of your apps.

On devices that support touch interactions, add keyboard functionality with the [Respond to the presence of the touch keyboard](../design/input/respond-to-the-presence-of-the-touch-keyboard.md) and [Use input scope to change the touch keyboard](../design/input/use-input-scope-to-change-the-touch-keyboard.md) articles.

### Microsoft Collaborate

The Microsoft Collaborate portal provides tools and services to streamline engineering collaboration within the Microsoft ecosystem by enabling the sharing of engineering system work items (bugs, feature requests, etc.) and the distribution of content (builds, documents, specs). [Learn more](/collaborate/).

![Microsoft Collaborate in Partner Center](images/microsoft_collaborate_screenshot.PNG)

### Package desktop applications with UWP projects

Visual Studio 2017 version 15.5 has updated the **Windows Application Packaging Project** template so that it's much easier to include a UWP project. You no longer have to use a JavaScript-based packaging project, and then manually tweak the package manifest.  

See [Package an app by using Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) for guidance on how to use this new template to package your desktop application.

See [Extend your desktop application with modern UWP components](/windows/apps/desktop/modernize/desktop-to-uwp-extend) for guidance about how to add a UWP project to your package.

### Subscription add-ons are now available to developers in the Windows Dev Center Insider program

All developers who have joined the Dev Center Insider program can now use subscription add-ons to sell digital products in their apps app (such as app features or digital content) with automated recurring billing periods. For more details, see [Enable subscription add-ons for your app](../monetize/enable-subscription-add-ons-for-your-app.md).

## Developer Guidance

### Color

We've added some new guidance on how to use color in your apps for the best possible user experience. This includes API usage scenarios as well as general guidance about UI design and accessibility. We've also updated the list of user accent colors available on Xbox. [Check out the updated Color article here.](../design/style/color.md)

![universal windows color palette](../design/basics/images/colors.png)

### Data access guides

We've added a [SQL Server guide](../data-access/sql-server-databases.md) to show how your app can directly access a SQL Server database. No service layer required.

Also, we've completely remodeled our [SQLite guide](../data-access/sqlite-databases.md) with cleaner look and feel, and we've included our latest best practices for storing and retrieving data in a light-weight database on the users device.

### Forms

We've added a new article on [how to construct forms in your apps](../design/controls-and-patterns/forms.md), to collect and submit data from users. This includes specific information about implementing forms and general guidance for when and where to use them.

### Intro to app design

The Universal Windows Platform (UWP) design guidance is a resource to help you design and build beautiful, polished apps. [Our new introduction](../design/basics/design-and-ui-intro.md) provides an overview of the universal design features that are included in every UWP app, and how you can use the docs to build user interfaces (UI) that scale beautifully across a range of devices.


### Request ratings and reviews

We've added a new article that shows how you can [request ratings and reviews for your app](../monetize/request-ratings-and-reviews.md). You can show a rating and review dialog in the context of your app, or you can open the rating and review page for your app in the Store.

## Samples

### Customer orders

The [Customer Orders Database](https://github.com/Microsoft/Windows-appsample-customers-orders-database) sample has been updated to show best practices around data access, like using the repository pattern and how to connect to multiple data sources (including Sqlite, SQL Azure, and a REST service).

## Videos

### Package a .NET app in Visual Studio

It's easier than ever to bring your desktop app to the Universal Windows Platform. [Watch the video](https://www.youtube.com/watch?v=fJkbYPyd08w) to learn how to package your .NET app for distribution, then [check out this page](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) for more information.