---
description: Photo Editor is a UWP sample application that showcases development with the C++/WinRT language projection. The sample application allows you to retrieve photos from the Pictures library, and then edit the selected image with assorted photo effects.
title: Photo Editor C++/WinRT sample application
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, sample, application, photo, editor
ms.localizationpriority: medium
---

# Photo Editor C++/WinRT sample application

> [!NOTE]
> The sample is targeted and tested for Windows 10, version 1903 (10.0; Build 18362), and Visual Studio 2019. If you prefer, you can use project properties to retarget the project(s) to Windows 10, version 1809 (10.0; Build 17763), and/or open the sample with Visual Studio 2017.

To clone or download the sample application, see [Photo Editor C++/WinRT sample application](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/) on the code samples gallery.

The Photo Editor application is a Universal Windows Platform (UWP) sample application that showcases development with the [C++/WinRT](intro-to-using-cpp-with-winrt.md) language projection. The sample application allows you to retrieve photos from the **Pictures** library, and then edit the selected image with assorted photo effects. In the sample's source code, you'll see a number of common practices&mdash;such as [data binding](binding-property.md), and [asynchronous actions and operations](concurrency.md)&mdash;performed using the C++/WinRT projection. Here are some of the specific features demonstrated by the sample.

- Use of Standard C++17 syntax and libraries with Windows Runtime (WinRT) APIs.
- Use of coroutines, including the use of co_await, co_return, [**IAsyncAction**](/uwp/api/windows.foundation.iasyncaction), and [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation-1).
- Creation and use of custom Windows Runtime class (runtime class) projected types and implementation types. For more info about these terms, see [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).
- [Event handling](handle-events.md), including the use of auto-revoking event tokens.
- Use of the external Win2D NuGet package, and [Windows::UI::Composition](/uwp/api/windows.ui.composition), for image effects.
- XAML data binding, including the [{x:Bind} markup extension](../xaml-platform/x-bind-markup-extension.md).
- XAML styling and UI customization, including [connected animations](../design/motion/connected-animation.md).