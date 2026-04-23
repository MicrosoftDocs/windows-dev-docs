---
description: Learn to use the x:DefaultBindMode attribute in XAML markup to specify a default mode for x:Bind other than OneTime.
title: xDefaultBindMode attribute
ms.date: 02/08/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:DefaultBindMode attribute

In XAML markup, specifies a default mode for x:Bind.

**x:DefaultBindMode** is available starting in Windows 10, version 1607 (Anniversary Update), SDK version 14393.

## XAML attribute usage

``` syntax
<object x:DefaultBindMode="OneTime \| OneWay \| TwoWay" .../>
```

## Remarks

[x:Bind](x-bind-markup-extension.md) has a default mode of **OneTime**. This was chosen for performance reasons, as using **OneWay** causes more code to be generated to hookup and handle change detection. You can use **x:DefaultBindMode** to change the default mode for x:Bind for a specific segment of the markup tree. The specified mode applies to any x:Bind expressions on that element and its children, that do not explicitly specify a mode as part of the binding.

## Related topics

* [x:Bind markup extension](x-bind-markup-extension.md)
