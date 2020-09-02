---
description: Uniquely identifies object elements for access to the instantiated object from code-behind or general code.
title: xName attribute
ms.assetid: 4FF1F3ED-903A-4305-B2BD-DCD29E0C9E6D
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:Name attribute


Uniquely identifies object elements for access to the instantiated object from code-behind or general code. Once applied to a backing programming model, **x:Name** can be considered equivalent to the variable holding an object reference, as returned by a constructor.

## XAML attribute usage

``` syntax
<object x:Name="XAMLNameValue".../>
```

## XAML values

| Term | Description |
|------|-------------|
| XAMLNameValue | A string that conforms to the restrictions of the XamlName grammar. |

##  XamlName grammar

The following is the normative grammar for a string that is used as a key in this XAML implementation:

``` syntax
XamlName ::= NameStartChar (NameChar)*
NameStartChar ::= LetterCharacter | '_'
NameChar ::= NameStartChar | DecimalDigit
LetterCharacter ::= ('a'-'z') | ('A'-'Z')
DecimalDigit ::= '0'-'9'
CombiningCharacter::= none
```

-   Characters are restricted to the lower ASCII range, and more specifically to Roman alphabet uppercase and lowercase letters, digits, and the underscore (\_) character.
-   The Unicode character range is not supported.
-   A name cannot begin with a digit. Some tool implementations prepend an underscore (\_) to a string if the user supplies a digit as the initial character, or the tool autogenerates **x:Name** values based on other values that contain digits.

## Remarks

The specified **x:Name** becomes the name of a field that is created in the underlying code when XAML is processed, and that field holds a reference to the object. The process of creating this field is performed by the MSBuild target steps, which also are responsible for joining the partial classes for a XAML file and its code-behind. This behavior is not necessarily XAML-language specified; it is the particular implementation that Universal Windows Platform (UWP) programming for XAML applies to use **x:Name** in its programming and application models.

Each defined **x:Name** must be unique within a XAML namescope. Generally, a XAML namescope is defined at the root element level of a loaded page and contains all elements under that element in a single XAML page. Additional XAML namescopes are defined by any control template or data template that is defined on that page. At run time, another XAML namescope is created for the root of the object tree that is created from an applied control template, and also by object trees created from a call to [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load). For more info, see [XAML namescopes](xaml-namescopes.md).

Design tools often autogenerate **x:Name** values for elements when they are introduced to the design surface. The autogeneration scheme varies depending on which designer you are using, but a typical scheme is to generate a string that starts with the class name that backs the element, followed by an advancing integer. For example, if you introduce the first [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) element to the designer, you might see that in the XAML this element has the **x:Name** attribute value of "Button1".

**x:Name** cannot be set in XAML property element syntax, or in code using [**SetValue**](/uwp/api/windows.ui.xaml.dependencyobject.setvalue). **x:Name** can only be set using XAML attribute syntax on elements.

**Note**  Specifically for C++/CX apps, a backing field for an **x:Name** reference is not created for the root element of a XAML file or page. If you need to reference the root object from C++ code-behind, use other APIs or tree traversal. For example you can call [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) for a known named child element and then call [**Parent**](/uwp/api/windows.ui.xaml.frameworkelement.parent).

### x:Name and other Name properties

Some types used in UWP XAML also have a property named **Name**. For example, [**FrameworkElement.Name**](/uwp/api/windows.ui.xaml.frameworkelement.name) and [**TextElement.Name**](/uwp/api/windows.ui.xaml.documents.textelement.name).

If **Name** is available as a settable property on an element, **Name** and **x:Name** can be used interchangeably in XAML, but an error results if both attributes are specified on the same element. There are also cases where there's a **Name** property but it's read-only (like [**VisualState.Name**](/uwp/api/windows.ui.xaml.visualstate.name)). If that's the case you always use **x:Name** to name that element in the XAML and the read-only **Name** exists for some less-common code scenario.

**Note**  [**FrameworkElement.Name**](/uwp/api/windows.ui.xaml.frameworkelement.name) generally should not be used as a way to change values originally set by **x:Name**, although there are some scenarios that are exceptions to that general rule. In typical scenarios, the creation and definition of XAML namescopes is a XAML processor operation. Modifying **FrameworkElement.Name** at run time can result in an inconsistent XAML namescope / private field naming alignment, which is hard to keep track of in your code-behind.

### x:Name and x:Key

**x:Name** can be applied as an attribute to elements within a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary) to act as a substitute for the [x:Key attribute](x-key-attribute.md). (It's a rule that all elements in a **ResourceDictionary** must have an x:Key or x:Name attribute.) This is common for [Storyboarded animations](../design/motion/storyboarded-animations.md). For more info, see section of [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md).