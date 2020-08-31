---
description: Learn how you can use the x:Null markup extension in XAML markup to specify a null value for a property.
title: xNull markup extension
ms.assetid: E6A4038E-4ADA-4E82-9824-582FC16AB037
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# {x:Null} markup extension


In XAML markup, specifies a **null** value for a property.

## XAML attribute usage

``` syntax
<object property="{x:Null}" .../>
```

## Remarks

**null** is the null reference keyword for C# and C++. The Microsoft Visual Basic keyword for a null reference is **Nothing**.

The initial default value can vary between dependency properties, and it is not necessarily **null**. Further, many dependency properties will not accept **null** as a value (whether through markup or code) due to their internal implementation. In such cases, setting a XAML attribute value with **{x:Null}** can result in a parser exception.

Some Windows Runtime types are nullable. In cases where a nullable type does not already have **null** as the default, you could use **{x:Null}** in XAML to set to the **null** value. If using Visual C++ component extensions (C++/CX), nullable types are represented as [**Platform::IBox<T>**](/cpp/cppcx/platform-ibox-interface). If using Microsoft .NET languages, nullable types are represented as [**Nullable<T>**](/dotnet/api/system.nullable-1).

## Related topics

* [**Nullable<T>**](/dotnet/api/system.nullable-1)
* [**IReference<T>**](/uwp/api/Windows.Foundation.IReference_T_)
 