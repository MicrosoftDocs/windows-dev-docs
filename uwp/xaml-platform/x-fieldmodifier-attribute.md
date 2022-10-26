---
description: Modifies XAML compilation behavior, such that fields for named object references are defined with public access rather than the private default behavior.
title: xFieldModifier attribute
ms.assetid: 6FBCC00B-848D-4454-8B1F-287CA8406DDF
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:FieldModifier attribute


Modifies XAML compilation behavior, such that fields for named object references are defined with **public** access rather than the **private** default behavior.

## XAML attribute usage

``` syntax
<object x:FieldModifier="public".../>
```

## Dependencies

[x:Name attribute](x-name-attribute.md) must also be provided on the same element.

## Remarks

The value for the **x:FieldModifier** attribute will vary by programming language. Valid values are **private**, **public**, **protected**, **internal** or **friend**. For C#, Microsoft Visual Basic or Visual C++ component extensions (C++/CX), you can give the string value "public" or "Public"; the parser doesn't enforce case on this attribute value.

**Private** access is the default.

**x:FieldModifier** is only relevant for elements with an [x:Name attribute](x-name-attribute.md), because that name is used to reference the field once it is public.

**Note**  Windows Runtime XAML doesn't support **x:ClassModifier** or **x:Subclass**.

