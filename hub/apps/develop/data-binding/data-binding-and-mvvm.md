---
ms.assetid: b7a8ec88-3013-4e5a-a110-fab3f20ee4bf
title: Data binding and MVVM
description: Data binding is at the core of the Model-View-ViewModel (MVVM) UI architectural design pattern, and enables loose coupling between UI and non-UI code.
ms.date: 12/12/2022
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui
ms.localizationpriority: medium
---

# Data binding and MVVM

Model-View-ViewModel (MVVM) is a UI architectural design pattern for decoupling UI and non-UI code. With MVVM, you define your UI declaratively in XAML and use data binding markup to link it to other layers containing data and commands. The data binding infrastructure provides a loose coupling that keeps the UI and the linked data synchronized and routes user input to the appropriate commands.

Because it provides loose coupling, the use of data binding reduces hard dependencies between different kinds of code. This makes it easier to change individual code units (methods, classes, controls, etc.) without causing unintended side effects in other units. This decoupling is an example of the *separation of concerns*, which is an important concept in many design patterns.
