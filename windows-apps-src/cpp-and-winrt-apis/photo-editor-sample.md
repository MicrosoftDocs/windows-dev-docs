---
author: JoshuaPartlow
description: The photo editor app is a UWP sample that highlights development with C++/WinRT, demonstrating common practices such as databinding and async operations. The sample allows you to retrieve photos from the Pictures library and edit the image with phto effects.
title: C++/WinRT sample
ms.author: wdg-dev-content
ms.date: 05/06/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, sample
ms.localizationpriority: medium
---

# C++ /WinRT code sample

>[!NOTE]
> At this time the sample is not yet available for download. When it is, a link to the repository will be updated on this page. 

The photo editor app is a UWP sample that highlights development with C++/WinRT. The sample allows you to retrieve photos from the Pictures library and edit a selected image with assorted photo effects. By examining the sample you'll be able to see a number of common practices, such as databinding and async operations, performed using the C++/WinRT projection. Some of the specific features demonstrated by the sample are:
	
* Use of Standard C++17 syntax and libraries with WinRT apis.
* Use of co-routines including co_await, co_return, IAsyncAction and IAsyncOperation.
* Creation and use of custom runtime class projected and implemention types.
* Event handling including the use of auto revoking event tokens.
* Use of the external Win2D nuget package and composition for image effects. 
* XAML data binding including the [{x:Bind} markup extension](https://docs.microsoft.com/windows/uwp/xaml-platform/x-bind-markup-extension).
* XAML styling and UI customization including connected animations.
