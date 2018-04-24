---
author: stevewhims
description: Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT.
title: Frequently-asked questions about C++/WinRT
ms.author: stwhi
ms.date: 04/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, frequently, asked, questions, faq
ms.localizationpriority: medium
---

# Frequently-asked questions about [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md)
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

Answers to questions that you're likely to have about authoring and consuming Windows Runtime APIs with C++/WinRT.

## What are the requirements for the C++/WinRT Visual Studio Extension (VSIX)?
The VSIX enforces a minimum Windows SDK target version of 10.0.17134.0 (Windows 10, version 1803). You'll also need Visual Studio 2017 Version 15.6, or later. You can identify a project that uses the VSIX by the presence of `<CppWinRTEnabled>true</CppWinRTEnabled>` in `<PropertyGroup Label="Globals">` in the `.vcxproj` file. For more info, see [Visual Studio support for C++/WinRT](intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt).

## What's a *runtime class*?
A runtime class is a type that can be activated and consumed via modern COM interfaces, typically across executable boundaries. However, a runtime class can also be used within the compilation unit that implements it. You declare a runtime class in Interface Definition Language (IDL), and you can implement it in standard C++ using C++/WinRT.

## What do *the projected type* and *the implementation type* mean?
See [Consume APIs with C++/WinRT](consume-apis.md) and [Author APIs with C++/WinRT](author-apis.md).

## Should I implement [**Windows::Foundation::IClosable**](/uwp/api/windows.foundation.iclosable) and, if so, how?
If you have a runtime class that frees resources in its destructor, and that runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by Windows Runtime client apps), then we recommend that you also implement **IClosable** in order to support the consumption of your runtime class by languages that lack deterministic finalization. Make sure that your resources are freed whether the destructor, [**IClosable::Close**](/uwp/api/windows.foundation.iclosable.Close), or both are called. **IClosable::Close** may be called an arbitrary number of times.

## Do I need to call [**IClosable::Close**](/uwp/api/windows.foundation.iclosable#Windows_Foundation_IClosable_Close_) on runtime classes that I consume?
**IClosable** exists to support languages that lack deterministic finalization. So, you shouldn't call **IClosable::Close** from C++/WinRT, except in very rare cases involving shutdown races or semi-deadly embraces. If you're using **Windows.UI.Composition** types, as an example, then you may encounter cases where you want to dispose objects in a set sequence, as an alternative to allowing the destruction of the C++/WinRT wrapper do the work for you.

## Do I need to declare a constructor in my runtime class's IDL?
Only if the runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by Windows Runtime client apps). For full details on the purpose and consequences of declaring constructor(s) in IDL, see [Runtime class constructors](author-apis.md#runtime-class-constructors).