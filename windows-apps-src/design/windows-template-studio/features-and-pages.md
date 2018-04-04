---
author: QuinnRadich
title: Features and pages in Windows Template Studio
description: Learn specific information on the features, pages, project types, and design patterns enabled by Windows Template Studio.
keywords: template, Windows Template Studio, template studio, features, pages, design patterns
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Features and pages in Windows Template Studio

Upon creating a new app, Windows Template Studio provides the following sets of options to choose from. These options, or templates, are what allow you to customize your app and easily create versatile and robust UIs.

For more information on the features that all projects created with Windows Template Studio have, see [understanding your generated code](understanding-generated-code.md).

## Project Types
**Project types** define the basic look and feel of your UWP app. You may only select one project type, and you can't change it after you've created your app.

| Project type | Description |
|-------------:|:------------|
| Basic | This basic project is a blank canvas upon which to build your app. It provides no scaffolding and leaves everything up to you. |
| [Navigation Pane](navigation-pane.md) | This project includes a navigation pane (or 'hamburger menu') at the side of the screen to enable easy user navigation between pages. This style is popular in mobile apps, but also works well on larger screens. The menu can be hidden when space is limited, or when it isn't needed.|
| Pivot and Tabs | Tabs across the top allow for quickly navigating between pages. The pivot control is useful for navigating between related or frequently accessed pages. The user can navigate between pivot panes (pages) by selecting from the text headers, which are always displayed.|

## Design patterns

**App Design patterns** define the coding pattern that will be used across the project, tying your UI and code together. You may only select one design pattern, and you can't change it after you've created your app.

| Design pattern| Description |
|--------------:|:------------|
| Code Behind   | Code is coupled directly with a XAML file using a .xaml.cs extension. If you developed in WinForms and feel comfortable with that style of development, this is a great option for you. |
| MVVM Basic    | A generic implementation of the [Model-View-ViewModel (MVVM) pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel), which can be used on all XAML platforms. Its intent is to provide a clean separation of concerns between the user interface (UI) controls and their logic. |
| MVVMLight    | [The MVVM Light Toolkit](http://www.mvvmlight.net/) is a popular, 3rd party framework by Laurent Bugnion, which is based on the [Model-View-ViewModel (MVVM) pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel). The MVVM Light Toolkit helps you separate your View from your Model, which creates applications that are cleaner and easier to extend and maintain. This toolkit puts a special emphasis on the "blend ability" of the created application (the ability to open and edit the user interface into Blend), including the creation of design-time data to enable Blend users to "see something" when they work with data controls. |
| Caliburn.Micro  | [Caliburn.Micro](https://caliburnmicro.com/) is a framework designed for building applications across all XAML platforms. Its strong support for MV* patterns will enable you to build your solution quickly, without the need to sacrifice code quality or testability.<br />WTS only supports the use of Caliburn.Micro with projects created in C#. |
| Prism  | [Prism](https://github.com/PrismLibrary/Prism) is a framework for building loosely coupled, maintainable, and testable XAML applications.<br />WTS only supports the use of Prism with projects created in C#. |

## Pages
**Pages** are the individual and baseline components of your app. You can add as many pages to your app as you need to, even multiple pages of the same type. You can also add more pages after your app has been created.

| Page        | Description |
|------------:|:------------|
| Blank       | This is a blank page, with no templates applied. |
| Settings | [The settings page](settings-configuration.md) is intended to hold configuration settings for your application, such as a dark / light theme toggle. This could also include any licenses, version number, or your privacy terms.|
| Web View | The web view page embeds a view into your app that renders web content using the Microsoft Edge rendering engine. |
| Media Player | A page for displaying video. It includes the MediaPlayer and has the default Media Transport controls enabled.|
| Master/Detail | The master-detail page has a master pane and a details pane for content. When an item in the master list is selected, the details pane is updated. This pattern is frequently used for email and address books. |
| Grid | A page displaying a [RadDataGrid control](http://www.telerik.com/universal-windows-platform-ui/grid), powered by [Telerik UI for UWP](http://www.telerik.com/universal-windows-platform-ui), an extention available both [commercially](http://www.telerik.com/purchase/universal-windows-platform) and through [open source](https://github.com/telerik/UI-For-UWP). The grid offers advanced UI virtualization, customizable columns, single and multi-column sorting, data editing, selection and filtering.|
| Chart | A page displaying a [RadChart control](http://www.telerik.com/universal-windows-platform-ui/chart), powered by [Telerik UI for UWP](http://www.telerik.com/universal-windows-platform-ui), an extension available both [commercially](http://www.telerik.com/purchase/universal-windows-platform) and through [open source](https://github.com/telerik/UI-For-UWP). RadChart control for Windows 10 apps features a rich set of chart series, from Bar, Line, Area, Pie, Scatter and Polar charts to different financial series.|
| Tabbed | The tabbed page is used for navigating frequently accessed, distinct content categories. |
| Map | The map page is based around the Windows Map Control. The template includes code for adding a Map Icon and getting the user's location. |
| Camera | A page for capturing a photo from the camera. Includes handling previewing, mirroring, and orientation.|
| Image Gallery | A page displaying a image gallery. The template provides code that allows the user to navigate between the gallery and an image detail page.|

### Features
**Features** are capabilities of your app that extend beyond a single page. You can add as many features to your app as you want. Features can also be added later, after your app has been created.

| Application Lifecycle | Feature Description |
|-------------------:|:------------|
| Settings Storage | [Setting storage](https://docs.microsoft.com/uwp/api/windows.storage.applicationdata) is a class that simplifies storage of your application's data. It handles loading, saving, serialization, and facilitates data access. |
| Suspend and Resume | [Suspend and resume](suspend-and-resume.md) enables your app to better handle when a user suspends your app. This feature hooks into the UWP's suspend and resume service, so your app can resume right where it left off. |

| Background Work    | Feature Description |
|-------------------:|:------------|
| Background Task | Creates an [in-process background task](../../launch-resume/create-and-register-an-inproc-background-task.md), allowing your app to run code when it is not in the foreground. The in-process model enhances the lifecycle of your app with improved notifications, whether your app is in the foreground or background. |

| User Interactions  | Feature Description |
|-------------------:|:------------|
| Toast Notification | Adaptive and interactive toast notifications let you create flexible pop-up notifications that provide users with content, optional inline images, and optional user interactions. For these notifications, you can use pictures, buttons, text inputs, actions, and more. |
| Azure Notification Hubs | [Azure Notification Hubs](https://docs.microsoft.com/azure/notification-hubs/notification-hubs-push-notification-overview) provide an easy-to-use, multi-platform way to push targeted notifications on a large scale. |
| Dev Center Notification | Register your app to receive notifications from the Microsoft Store, and enbale your app to be launched through those notifications. |
| Live Tile | Enables modification and updates to your app's presence on the Windows 10 Start Menu, providing the ability to change the app's visual state and to provide additional context or information. |
| First Run Prompt | Display a prompt when the app is used for the first time. |
| What's New Prompt | Display a prompt when the app is first used after an update with [this feature](whats-new-prompt.md) |
| Uri Scheme | Add the ability to launch and deep link into the app with a [custom URI scheme](uri-scheme-activation.md).|