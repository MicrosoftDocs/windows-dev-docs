---
author: JoshuaPartlow
description: Photo Editor is a UWP sample application that showcases development with the C++/WinRT language projection. The sample application allows you to retrieve photos from the Pictures library, and then edit the selected image with assorted photo effects.
title: Photo Editor C++/WinRT sample application
ms.author: wdg-dev-content
ms.date: 06/08/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, sample, application, photo, editor
ms.localizationpriority: medium
---

# Photo Editor C++/WinRT sample application
You can clone or download the sample application from the [Photo Editor C++/WinRT sample application](https://github.com/Microsoft/Windows-appsample-photo-editor) GitHub repository.

The Photo Editor application is a Universal Windows Platform (UWP) sample application that showcases development with the [C++/WinRT](intro-to-using-cpp-with-winrt.md) language projection. The sample application allows you to retrieve photos from the **Pictures** library, and then edit the selected image with assorted photo effects. In the sample's source code, you'll see a number of common practices&mdash;such as [data binding](binding-property.md), and [asynchronous actions and operations](concurrency.md)&mdash;performed using the C++/WinRT projection. Here are some of the specific features demonstrated by the sample.
	
- Use of Standard C++17 syntax and libraries with Windows Runtime (WinRT) APIs.
- Use of coroutines, including the use of co_await, co_return, [**IAsyncAction**](/uwp/api/windows.foundation.iasyncaction), and [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation_tresult_).
- Creation and use of custom Windows Runtime class (runtime class) projected types and implementation types. For more info about these terms, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).
- [Event handling](handle-events.md), including the use of auto-revoking event tokens.
- Use of the external Win2D NuGet package, and [Windows::UI::Composition](/uwp/api/windows.ui.composition), for image effects.
- XAML data binding, including the [{x:Bind} markup extension](https://docs.microsoft.com/windows/uwp/xaml-platform/x-bind-markup-extension).
- XAML styling and UI customization, including [connected animations](../design/motion/connected-animation.md).
