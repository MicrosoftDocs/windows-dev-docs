---
author: QuinnRadich
title: Understanding generated code
description: Projects created with Windows Template Studio provide certain core functions that are useful no matter what sort of app you're building.
keywords: template, Windows Template Studio, template studio, generated code, project type, pages
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Understanding generated code from Windows Template Studio

Projects created with Windows Template Studio are a starting point, which will require modification and extension before they're finished.

When creating a project with Windows Template Studio, you'll choose from options in the following areas in order to establish the starting point of your app:

* **Project Types** define the basic look, feel, and general structure of your app.
* **Design Patterns** define the coding pattern that will be used across the project, tying together your UI and code in a unified strategy that fits your app's needs.
* **Pages** are the individual components of your app, configured for uses such as media players, data grids, settings, and many other similar items.
* **Features** are capabilities of your app that extend beyond a single page. This includes things such as background tasts, toast notifications, and suspend and resume.

The [features and pages](features-and-pages.md) topic has more information about the specific options and implications in each of these areas. However, the generated code also provides a set of capabilities regardless of the choices you make. These features will be useful to understand when working with Windows Template Studio, regardless of the app you create:

| Feature | Description |
| ----- | ----- |
| [Application activation](application-activation.md) | App activation in Windows Template Studio allows for easy intigration with external services. |
| [Navigation between pages](navigation-between-pages.md) | The configuration of navigation in your project depends on the Project Type you select. |
| [Notifications in Windows Template Studio](notifications.md) | All Windows Template Studio projects support toast notifications, store notifications, and hub notifications. |