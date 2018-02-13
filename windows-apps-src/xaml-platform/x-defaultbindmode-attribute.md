---
author: jwmsft
description: In XAML markup, specifies a default mode for x:Bind
title: xDefaultBindMode markup extension
ms.author: jimwalk
ms.date: 02/08/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# {x:DefaultBindMode} markup extension


In XAML markup, specifies a default mode for x:Bind.

## XAML attribute usage

``` syntax
<object x:DefaultBindMode="OneTime \| OneWay \| TwoWay" .../>
```

## Remarks

x:Bind has a default mode of OneTime, and this was chosen for performance reasons as using OneWay will cause more code to be generated to hookup and handle the change detection. **x:DefaultBindMode** can be used to change the default mode for x:Bind for a specific segment of the markup tree. The mode selected will apply any x:Bind expressions on that element and its children, that do not explicitly specify a mode as part of the binding.

## Related topics

* [x:Bind markup extension](x-bind-markup-extension.md)
