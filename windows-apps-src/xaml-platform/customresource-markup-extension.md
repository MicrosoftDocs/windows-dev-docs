---
author: jwmsft
description: Provides a value for any XAML attribute by evaluating a reference to a resource that comes from a custom resource-lookup implementation. Resource lookup is performed by a CustomXamlResourceLoader class implementation.
title: CustomResource markup extension
ms.assetid: 3A59A8DE-E805-4F04-B9D9-A91E053F3642
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# {CustomResource} markup extension


Provides a value for any XAML attribute by evaluating a reference to a resource that comes from a custom resource-lookup implementation. Resource lookup is performed by a [**CustomXamlResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br243327) class implementation.

## XAML attribute usage

``` syntax
<object property="{CustomResource key}" .../>
```

## XAML values

| Term | Description |
|------|-------------|
| key | The key for the requested resource. How the key is initially assigned is specific to the implementation of the [**CustomXamlResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br243327) class that is currently registered for use. |

## Remarks

**CustomResource** is a technique for obtaining values that are defined elsewhere in a custom resource repository. This technique is relatively advanced and isn't used by most Windows Runtime app scenarios.

How a **CustomResource** resolves to a resource dictionary is not described in this topic, because that can vary widely depending on how [**CustomXamlResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br243327) is implemented.

The [**GetResource**](https://msdn.microsoft.com/library/windows/apps/br243340) method of the [**CustomXamlResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br243327) implementation is called by the Windows Runtime XAML parser whenever it encounters a `{CustomResource}` usage in markup. The *resourceId* that is passed to **GetResource** comes from the *key* argument, and the other input parameters come from context, such as which property the usage is applied to.

A `{CustomResource}` usage doesn't work by default (the base implementation of [**GetResource**](https://msdn.microsoft.com/library/windows/apps/br243340) is incomplete). To make a valid `{CustomResource}` reference, you must perform each of these steps:

1.  Derive a custom class from [**CustomXamlResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br243327) and override [**GetResource**](https://msdn.microsoft.com/library/windows/apps/br243340) method. Do not call base in the implementation.
2.  Set [**CustomXamlResourceLoader.Current**](https://msdn.microsoft.com/library/windows/apps/br243328) to reference your class in initialization logic. This must happen before any page-level XAML that includes the `{CustomResource}` extension usage is loaded. One place to set **CustomXamlResourceLoader.Current** is in the [**Application**](https://msdn.microsoft.com/library/windows/apps/br242324) subclass constructor that's generated for you in the App.xaml code-behind templates.
3.  Now you can use `{CustomResource}` extensions in the XAML that your app loads as pages, or from within XAML resource dictionaries.

**CustomResource** is a markup extension. Markup extensions are typically implemented when there is a requirement to escape attribute values to be other than literal values or handler names, and the requirement is more global than just putting type converters on certain types or properties. All markup extensions in XAML use the "\{" and "\}" characters in their attribute syntax, which is the convention by which a XAML processor recognizes that a markup extension must process the attribute.

## Related topics

* [ResourceDictionary and XAML resource references](https://msdn.microsoft.com/library/windows/apps/mt187273)
* [**CustomXamlResourceLoader**](https://msdn.microsoft.com/library/windows/apps/br243327)
* [**GetResource**](https://msdn.microsoft.com/library/windows/apps/br243340)

