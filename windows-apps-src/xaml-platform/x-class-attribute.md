---
description: Configures XAML compilation to join partial classes between markup and code-behind. The code partial class is defined in a separate code file, and the markup partial class is created by code generation during XAML compilation.
title: xClass attribute
ms.assetid: 40A7C036-133A-44DF-9D11-0D39232C948F
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:Class attribute


Configures XAML compilation to join partial classes between markup and code-behind. The code partial class is defined in a separate code file, and the markup partial class is created by code generation during XAML compilation.

## XAML attribute usage


``` syntax
<object x:Class="namespace.classname"...>
  ...
</object>
```

## XAML values

| Term | Description |
|------|-------------|
| namespace | Optional. Specifies a namespace that contains the partial class identified by _classname_. If _namespace_ is specified, a dot (.) separates _namespace_ and _classname_. If _namespace_ is omitted, _classname_ is assumed to have no namespace. |
| classname | Required. Specifies the name of the partial class that connects the loaded XAML and your code-behind for that XAML. | 

## Remarks

**x:Class** can be declared as an attribute for any element that is the root of a XAML file/object tree and is being compiled by build actions, or for the [**Application**](/uwp/api/Windows.UI.Xaml.Application) root in the application definition of a compiled application. Declaring **x:Class** on any element other than a root node, and under any circumstances for a XAML file that is not compiled with the **Page** build action, results in a compile-time error.

The class used as **x:Class** cannot be a nested class.

The value of the **x:Class** attribute must be a string that specifies the fully qualified name of a class. You can omit namespace information so long as that is how the code-behind is structured also (your class definition starts at the class level). The code-behind file for a page or application definition must be within a code file that is included as part of the project. The code-behind class must be public. The code-behind class must be partial.

## CLR language rules

Although your code-behind file can be a C++ file, there are certain conventions that still follow the CLR language form, so that there is no difference in the XAML syntax. In particular, the separator between the namespace and classname components of any **x:Class** value is always a dot ("."), even though the separator between namespace and classname in the C++ code file associated with the XAML is "::". If you declare nested namespaces in C++, then the separator between the successive nested namespace strings should also be "." rather than "::" when you specify the *namespace* part of the **x:Class** value.