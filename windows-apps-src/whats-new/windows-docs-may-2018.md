---
title: What's New in Windows Docs in May 2018 - Develop UWP apps
description: New features, videos, and developer guidance have been added to the Windows 10 developer documentation for May 2018 and the Microsoft Build conference.
keywords: what's new, update, features, developer guidance, Windows 10, may, build
ms.date: 05/07/2018
ms.topic: article
ms.localizationpriority: medium
---
# What's New in the Windows Developer Docs in May 2018

The Windows Developer Documentation is constantly being updated with information on new features available to developers across the Windows platform. The following feature overviews, developer guidance, videos, and samples have been made available in the month of May to coincide with the [Microsoft Build 2018](https://www.microsoft.com/build/) developer conference.

[Install the tools and SDK](https://developer.microsoft.com/windows/downloads#_blank) on Windows 10 and you’re ready to either [create a new Universal Windows app](../get-started/create-uwp-apps.md) or explore how you can use your [existing app code on Windows](../porting/index.md).

## Features

### Motion in Fluent Design

The user of motion in the Fluent Design System is evolving, built on the fundamentals of timing, easing, directionality, and gravity. Applying these fundamentals will help guide the user through your app, and connects them with their digital experience by reflecting the natural world. Learn more in this articles:

* [The Motion overview](../design/motion/index.md) has been updated to reflect these fundamentals.
* [Motion-in-practice](../design/motion/motion-in-practice.md) provides examples of how to apply these fundamentals within your app.
* [Directionality and gravity](../design/motion/directionality-and-gravity.md) solidifies the user's mental model of your app.
* [Timing and easing](../design/motion/timing-and-easing.md) adds realism to the motion in your app.

![Motion in action](../design/motion/images/contextual.gif)

### Fluent Design Updates

Visual updates and minor changes have been made to the following Fluent Design pages:

* [Alignment, padding, margins](../design/layout/alignment-margin-padding.md)
* [Color](../design/style/color.md)
* [Command basics](../design/basics/commanding-basics.md)
* [Fluent Design for Windows apps](/windows/apps/fluent-design-system)
* [Introduction to app design](../design/basics/design-and-ui-intro.md)
* [Navigation basics](../design/basics/navigation-basics.md)
* [Responsive design techniques](../design/layout/responsive-design.md)
* [Screen sizes and breakpoints](../design/layout/screen-sizes-and-breakpoints-for-responsive-design.md)
* [Style overview](../design/style/index.md)
* [Writing style](../design/style/writing-style.md)

In addition, we've rewritten the following pages with all-new information on their content areas:

* [Icons](../design/style/icons.md) now provides practical recommendations for using icons and making them clickable.
* [Typography](../design/style/typography.md) consolidates information from similar articles, putting everything in a single place with updated guidance and illustrations.

![Color palette image](../design/style/images/color/accent-color-palette.svg)

### App Installer files in Visual Studio

App Installer files can now be created with Visual Studio 2017, Update 15.7, and later versions. [Learn how to use Visual Studio to create an App Installer file](/windows/msix/app-installer/create-appinstallerfile-vs) and enable automatic updates to your apps. If you're running into problems, see [troubleshooting installation issues with the App Installer file](/windows/msix/app-installer/troubleshoot-appinstaller-issues) to view common issues and solutions.

### Edge WebView control for Windows Forms and WPF applications

Show web content in your desktop application by using the WebView control, previously only available to UWP applications. This control uses the Microsoft Edge rendering engine to embed a view that renders richly formatted HTML content from a remote web server, dynamically generated code, or content files. Find the WebView control in the latest release of the [Windows Community Toolkit.](/windows/uwpcommunitytoolkit/)

Look for other controls like WebView in future releases of the Windows Community Toolkit. For more information, see [Host UWP controls in WPF and Windows Forms applications.](/windows/apps/desktop/modernize/xaml-islands)

### Gaze input and interactions

[Track a user's gaze, attention, and presence based on the location and movement of their eyes.](../design/input/gaze-interactions.md) This powerful new way to use and interact with your UWP apps is especially useful as an assistive technology. Gaze input also provides compelling opportunities for both gaming (including target acquisition and tracking) and other interactive scenarios where traditional input devices (keyboard, mouse, touch) are not available.

### MSIX Packaging Format

Announced at the Microsoft Build 2018 conference, MSIX is a new containerization package format that applies to all Windows applications including Win32, Windows Forms, WPF, and UWP. This new format inherits great features from UWP:

* Robust installation and updating. 
* Managed security model with a flexible capability system.
* Support for the Microsoft Store, enterprise management, and many custom distribution models.

Tools to create these packages will be available in a future release of Visual Studio and Windows SDK.

The MSIX packaging format is an open source format which makes it easier for our partners to support the MSIX ecosystem with their tools and solutions. To learn more about the MSIX packaging format, see [MSIX SDK](https://github.com/Microsoft/msix-packaging). 

![MSIX packaging image](images/msix.png)

### Optional packages with executable code

Optional packages in your app can now contain executable C# code. [Learn how to use Visual Studio to configure optional add-on packages to support your main app package.](/windows/msix/package/optional-packages)

### Page transitions

[Page transitions](../design/motion/page-transitions.md) navigate users between pages in an app. They help users understand where they are in the navigation hierarchy, and provide feedback about the relationship between pages.

### Project Rome

The Project Rome team has overhauled their iOS and Android SDKs, adding new features like User Activities and refactoring much of their code to provide a consistent programming experience across the different SDKs. [All new API reference and how-to docs](/windows/project-rome/) will go live  during the Build 2018 developer conference.

### Sets

The Sets feature is available in Windows Insider preview builds. When using the Sets feature, you app is drawn into a window that might be shared with other apps, with each app having its own tab in the title bar. 

## Developer Guidance

### Get started

We've revitalized our Get started content with new learning tracks. These new topics aim to provide new Windows 10 developers with information on some common tasks they might want to accomplish. They're not tutorials and don't provide a hand-held walkthrough, but instead point out where existing documentation exists and how to use it. Check out the revamped [Start coding](../get-started/create-uwp-apps.md) page, or explore each individual learning track:

* [Construct a form](../get-started/construct-form-learning-track.md)
* [Display customers in a list](../get-started/display-customers-in-list-learning-track.md)
* [Save and load settings](../get-started/settings-learning-track.md)
* [Work with files](../get-started/fileio-learning-track.md)

![Get started image](../get-started/images/build-your-app.png)

### Advertising performance report

The [Advertising performance report](../publish/advertising-performance-report.md) in Partner Center now provides viewability metrics. We also added the [Optimize the viewability of your ad units](../monetize/optimize-ad-unit-viewability.md) article to provide recommendations for optimizing the viewability of your ads.

### Targeted push notifications

The [Notifications](../publish/send-push-notifications-to-your-apps-customers.md) page in Partner Center now provides additional analytics data for all your notifications in graph and world map views.

## Videos

### C++/WinRT

C++/WinRT is a new way of authoring and consuming Windows Runtime APIs. It's implemented sole in header files, and designed to provide you with first-class access to modern app features. [Watch the video](https://www.youtube.com/watch?v=TLSul1XxppA&feature=youtu.be) to learn how it works, then [read the developer docs](../cpp-and-winrt-apis/index.md) for more info.

### Multi-instance UWP apps

Windows now allows you to run multiple instances of your UWP app, with each in its own separate process. [Watch the video](https://www.youtube.com/watch?v=clnnf4cigd0&feature=youtu.be) to learn how to create a new app that supports this feature, then [read the developer docs](../launch-resume/multi-instance-uwp.md) for more guidance on how and why to use this feature.

## Samples

### Customer database tutorial

This tutorial creates a basic UWP app for managing a list of customers, and introduces concepts and practices useful in enterprise development. It walks you through implementing UI elements and adding operations against a local SQLite database, and provides loose guidance for connecting to a remote REST database if you wish to go further. [Check out the tutorial here](../enterprise/customer-database-tutorial.md)