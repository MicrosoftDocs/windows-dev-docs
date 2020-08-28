---
description: Learn about the rules used by XAML for processing the whitespace characters space, linefeed, and tab.
title: XAML and whitespace
ms.assetid: 025F4A8E-9479-4668-8AFD-E20E7262DC24
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML and whitespace


Learn about the whitespace processing rules as used by XAML.

## Whitespace processing

Consistent with XML, whitespace characters in XAML are space, linefeed, and tab. These correspond to the Unicode values 0020, 000A, and 0009 respectively. By default this whitespace normalization occurs when a XAML processor encounters any inner text found between elements in a XAML file:

-   Linefeed characters between East Asian characters are removed.
-   All whitespace characters (space, linefeed, tab) are converted into spaces.
-   All consecutive spaces are deleted and replaced by one space.
-   A space immediately following the start tag is deleted.
-   A space immediately before the end tag is deleted.
-   *East Asian characters* is defined as a set of Unicode character ranges U+20000 to U+2FFFD and U+30000 to U+3FFFD. This subset is also sometimes referred to as *CJK ideographs*. For more information, see http://www.unicode.org.

"Default" corresponds to the state denoted by the default value of the **xml:space** attribute.

### Whitespace in inner text, and string primitives

The above normalization rules apply to inner text within XAML elements. After normalization, a XAML processor converts any inner text into an appropriate type like this:

-   If the type of the property is not a collection, but is not directly an **Object** type, the XAML processor tries to convert to that type using its type converter. A failed conversion here results in a XAML parse error.
-   If the type of the property is a collection, and the inner text is contiguous (no intervening element tags), the inner text is parsed as a single **String**. If the collection type cannot accept **String**, this also results in a XAML parser error.
-   If the type of the property is **Object**, then the inner text is parsed as a single **String**. If there are intervening element tags, this results in a XAML parser error, because the **Object** type implies a single object (**String** or otherwise).
-   If the type of the property is a collection, and the inner text is not contiguous, then the first substring is converted into a **String** and added as a collection item, the intervening element is added as a collection item, and finally the trailing substring (if any) is added to the collection as a third **String** item.

### Whitespace and text content models

In practice, preserving whitespace is of concern only for a subset of all possible content models. That subset is composed of content models that can take a singleton **String** type in some form, a dedicated **String** collection, or a mixture of **String** and other types in lists, collections, or dictionaries.

Even for content models that can take strings, the default behavior within these content models is that any whitespace that remains is not treated as significant.

### Preserving whitespace

Several techniques for preserving whitespace in the source XAML for eventual presentation are not affected by XAML processor whitespace normalization.

`xml:space="preserve"`: Specify this attribute at the level of the element where whitespace needs to be preserved. Note that this preserves all whitespace, including the spaces that might be added by code editors or design surfaces to align markup elements as a visually intuitive nesting. Whether those spaces render is again a matter of the content model for the containing element. We don't recommend that you specify `xml:space="preserve"` at the root level, because the majority of object models don't consider whitespace as significant one way or another. It is a better practice to only set the attribute specifically at the level of elements that render whitespace within strings, or are whitespace significant collections.

Entities and nonbreaking spaces: XAML supports placing any Unicode entity within a text object model. You can use dedicated entities such as nonbreaking space (in UTF-8 encoding). You can also use rich text controls that support nonbreaking space characters. Be cautious if you are using entities to simulate layout characteristics such as indents, because the run-time output of the entities vary based on a greater number of factors than would the general layout facilities, such as proper use of panels and margins.

