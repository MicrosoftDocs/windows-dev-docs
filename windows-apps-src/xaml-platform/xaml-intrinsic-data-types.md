---
description: Lists language-level support in XAML for the Windows Runtime for certain data types in the common language runtime (CLR) and in other programming languages such as C++.
title: XAML intrinsic data types
ms.assetid: D50E6127-395D-4E27-BAA2-2FE627F4B711
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML intrinsic data types


XAML for the Windows Runtime provides language-level support for several data types that are frequently used primitives in the common language runtime (CLR) and in other programming languages such as C++.

The most common place you'll see XAML intrinsic data type usages is when resources are defined in a XAML resource dictionary. You might define constants there, for example numbers that you use for multiple values. Or you might use a storyboarded animation that animates using a string or Boolean value, and you'll then need a XAML object element representing the string or Boolean to fill the keyframe of your [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames) definition. The Windows Runtime default XAML templates use both these techniques.

XAML for the Windows Runtime provides language-level support for these types.

| XAML primitive | Description |
|-------|-------------|
| **x:Boolean**  | For CLR support, corresponds to [**Boolean**](/dotnet/api/system.boolean). XAML parses values for **x:Boolean** as case insensitive. Note that "x:Bool" is not an accepted alternative. |
| **x:String**   | For CLR support, corresponds to [**String**](/dotnet/api/system.string). Encoding for the string defaults to the surrounding XML encoding. |
| **x:Double**   | For CLR support, corresponds to [**Double**](/dotnet/api/system.double). In addition to the numeric values, text syntax for **x:Double** permits the token "NaN", which is how "Auto" for layout behavior can be stored as a resource value. The tokens are treated as case sensitive. You can use scientific notation, for example "1+E06" for `1,000,000`. |
| **x:Int32**    | For CLR support, corresponds to [**Int32**](/dotnet/api/system.int32). **x:Int32** is treated as signed, and you can include the minus ("-") symbol for a negative integer. In XAML, the absence of a sign in text syntax implies a positive signed value. |

These XAML language primitives are generally the only cases in which you define an object element that uses the **x:** prefix in your XAML. All other XAML language features are typically used in attribute form, or as a markup extension.

**Note**  By convention, the language primitives for XAML and all other XAML language elements are shown with the "x:" prefix. This is how XAML language elements are typically used in real-world markup. This convention is followed in the documentation for XAML and also in the XAML specification.

## Other XAML primitives

The XAML 2009 specification notes other XAML language-level primitives such as **x:Uri** and **x:Single**. Unless listed in the table in this topic, other XAML language primitives as defined by other XAML vocabularies or by the XAML 2009 specification are not currently supported in XAML for the Windows Runtime.

**Note**  Dates and times (properties that use [**DateTime**](/uwp/api/Windows.Foundation.DateTime) or [**DateTimeOffset**](/dotnet/api/system.datetimeoffset), [**TimeSpan**](/uwp/api/Windows.Foundation.TimeSpan) or [**System.TimeSpan**](/dotnet/api/system.timespan)) aren't settable with a XAML primitive. These properties generally aren't settable in XAML at all, because there's no default from-string conversion behavior in the Windows Runtime XAML parser for dates and times. For initialization values of any date and time properties, you'll have to use code-behind that runs when a page or element loads.

## Related topics

* [XAML overview](xaml-overview.md)
* [XAML syntax guide](xaml-syntax-guide.md)
* [Storyboarded animations](../design/motion/storyboarded-animations.md)
 