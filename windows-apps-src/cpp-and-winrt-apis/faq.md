---
author: stevewhims
description: Answers to questions that you're likely to have about authoring and consuming WinRT APIs with C++/WinRT.
title: Frequently-asked questions about C++/WinRT
ms.author: stwhi
ms.date: 03/14/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, frequently, asked, questions, faq
ms.localizationpriority: medium
---

# Frequently-asked questions about C++/WinRT
> [!NOTE]
> **Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

Answers to questions that you're likely to have about authoring and consuming WinRT APIs with C++/WinRT.

## What's a *runtime class*?
A runtime class is a type that can be activated and consumed via modern COM interfaces, typically across executable boundaries. However, a runtime class can also be used within the compilation unit that implements it. You declare a runtime class in Interface Definition Language (IDL), and you can implement it in standard C++ using C++/WinRT.

## What do *the projected type* and *the implementation type* mean?
See [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md).

## Should I implement [**Windows::Foundation::IClosable**](/uwp/api/windows.foundation.iclosable) and, if so, how?
If you have a runtime class that frees resources in its destructor, and that runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by WinRT client apps), then we recommend that you also implement **IClosable** in order to support the consumption of your runtime class by languages that lack deterministic finalization. Make sure that your resources are freed whether the destructor, [**IClosable::Close**](/uwp/api/windows.foundation.iclosable.Close), or both are called. **IClosable::Close** may be called an arbitrary number of times.

## Do I need to call [**IClosable::Close**](/uwp/api/windows.foundation.iclosable#Windows_Foundation_IClosable_Close_) on runtime classes that I consume?
**IClosable** exists to support languages that lack deterministic finalization. So, you shouldn't call **IClosable::Close** from C++/WinRT, except in very rare cases involving shutdown races or semi-deadly embraces. If you're using **Windows.UI.Composition** types, as an example, then you may encounter cases where you want to dispose objects in a set sequence, as an alternative to allowing the destruction of the C++/WinRT wrapper do the work for you.

## Do I need to declare a constructor in my runtime class's IDL?
Only if the runtime class is designed to be consumed from outside its implementing compilation unit (it's a Windows Runtime component intended for general consumption by WinRT client apps). For full details on the purpose and consequences of declaring constructor(s) in IDL, see [Implementation and projected types for a C++/WinRT runtime class](ctors-runtimeclass-activation.md).