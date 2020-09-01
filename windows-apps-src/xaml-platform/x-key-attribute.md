---
description: Uniquely identifies elements that are created and referenced as resources, and which exist within a ResourceDictionary.
title: xKey attribute
ms.assetid: 141FC5AF-80EE-4401-8A1B-17CB22C2277A
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:Key attribute


Uniquely identifies elements that are created and referenced as resources, and which exist within a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary).

## XAML attribute usage

``` syntax
<ResourceDictionary>
  <object x:Key="stringKeyValue".../>
</ResourceDictionary>
```

## XAML attribute usage (implicit **ResourceDictionary**)

``` syntax
<object.Resources>
  <object x:Key="stringKeyValue".../>
</object.Resources>
```

## XAML values

| Term | Description |
|------|-------------|
| object | Any object that is shareable. See [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md). |
| stringKeyValue | A true string used as a key, which must conform to the _XamlName_> grammar. See "XamlName grammar" below. | 

##  XamlName grammar

The following is the normative grammar for a string that is used as a key in the Universal Windows Platform (UWP) XAML implementation:

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
-   A name cannot begin with a digit.

## Remarks

Child elements of a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary) generally include an **x:Key** attribute that specifies a unique key value within that dictionary. Key uniqueness is enforced at load time by the XAML processor. Non-unique **x:Key** values will result in XAML parse exceptions. If requested by [{StaticResource} markup extension](staticresource-markup-extension.md), a non-resolved key will also result in XAML parse exceptions.

**x:Key** and [x:Name](x-name-attribute.md) are not identical concepts. **x:Key** is used exclusively in resource dictionaries. x:Name is used for all areas of XAML. A [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) call using a key value will not retrieve a keyed resource. Objects defined in a resource dictionary may have an **x:Key**, an **x:Name** or both. The key and name are not required to match.

Note that in the implicit syntax shown, the [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary) object is implicit in how the XAML processor produces a new object to populate a [**Resources**](/uwp/api/windows.ui.xaml.frameworkelement.resources) collection.

The code equivalent of specifying **x:Key** is any operation that uses a key with the underlying [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). For example, an **x:Key** applied in markup for a resource is equivalent to the value of the *key* parameter of **Insert** when you add the resource to a **ResourceDictionary**.

An item in a resource dictionary can omit a value for **x:Key** if it is a targeted [**Style**](/uwp/api/Windows.UI.Xaml.Style) or [**ControlTemplate**](/uwp/api/Windows.UI.Xaml.Controls.ControlTemplate); in each of these cases the implicit key of the resource item is the **TargetType** value interpreted as a string. For more info, see [Quickstart: styling controls](/previous-versions/windows/apps/hh465498(v=win.10)) and [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md).