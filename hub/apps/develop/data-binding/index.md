---
ms.assetid: adfa70f3-a4d9-45d1-8957-c26a7703a276
title: Data binding in Windows apps
description: Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data.
ms.date: 12/12/2022
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui
ms.localizationpriority: medium
---

# Data binding in Windows apps

Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. Data binding allows you to separate the concern of data from the concern of UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app. In XAML markup, you can choose to use either the [{x:Bind} markup extension](/windows/uwp/xaml-platform/x-bind-markup-extension) or the [{Binding} markup extension](/windows/uwp/xaml-platform/binding-markup-extension). And you can even use a mixture of the two in the same app—even on the same UI element. `{x:Bind}` was new for UWP in Windows 10, is also available in Windows App SDK, and it has better performance.
