---
author: tbd
description: In XAML markup, specifies a default mode for x:Bind
title: xDefaultBindMode markup extension
ms.assetid: 
ms.author: 
ms.date: 
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# {x:DefaultBindMode} markup extension


In XAML markup, specifies a default mode for x:Bind.

## XAML attribute usage

``` syntax
<object x:DefaultBindMode="OneTime \| OneWay \| TwoWay" .../>
```

## Remarks

x:Bind has a default mode of OneTime, and this was chosen for performance reasons as using OneTime will cause more code to be generated to hookup and handle the change detection. **x:DefaultBindMode** can be used to change the default mode for x:Bind for a specific segment of the markup tree. The mode selected will apply any x:Bind expressions on that element and its children, that do not explicitly specify a mode as part of the binding.

## Related topics

* [**x:Bind**](https://docs.microsoft.com/en-us/windows/uwp/xaml-platform/x-bind-markup-extension)
