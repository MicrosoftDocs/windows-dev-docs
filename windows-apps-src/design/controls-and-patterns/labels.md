---
author: Jwmsft
Description: Use a label to indicate to the user what they should enter into an adjacent control. You can also label a group of related controls, or display instructional text near a group of related controls.
title: Labels
ms.assetid: CFACCCD4-749F-43FB-947E-2591AE673804
label: Labels
template: detail.hbs
ms.author: jimwalk
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: miguelrb
design-contact: ksulliv
doc-status: Published
localizationpriority: medium
---
# Labels

 

A label is the name or title of a control or a group of related controls.

> **Important APIs**: Header property, [TextBlock class](https://msdn.microsoft.com/library/windows/apps/br209652)

In XAML, many controls have a built-in Header property that you use to display the label. For controls that don't have a Header property, or to label groups of controls, you can use a [TextBlock](https://msdn.microsoft.com/library/windows/apps/br209652) instead.

![a screenshot that illustrates the standard label control](images/label-standard.png)

## Recommendations


-   Use a label to indicate to the user what they should enter into an adjacent control. You can also label a group of related controls, or display instructional text near a group of related controls.
-   When labeling controls, write the label as a noun or a concise noun phrase, not as a sentence, and not as instructional text. Avoid colons or other punctuation.
-   When you do have instructional text in a label, you can be more generous with text-string length and also use punctuation.


## Get the sample code
* [XAML UI basics sample](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/XamlUIBasics)

## Related topics
* [Text controls](text-controls.md)
* [TextBox.Header property](https://msdn.microsoft.com/library/windows/apps/dn252861)
* [PasswordBox.Header property](https://msdn.microsoft.com/library/windows/apps/dn299051)
* [ToggleSwitch.Header property](https://msdn.microsoft.com/library/windows/apps/br209713)
* [DatePicker.Header property](https://msdn.microsoft.com/library/windows/apps/dn279460)
* [TimePicker.Header property](https://msdn.microsoft.com/library/windows/apps/dn299286)
* [Slider.Header property](https://msdn.microsoft.com/library/windows/apps/dn252829)
* [ComboBox.Header property](https://msdn.microsoft.com/library/windows/apps/dn279416)
* [RichEditBox.Header property](https://msdn.microsoft.com/library/windows/apps/dn252726)
* [TextBlock class](https://msdn.microsoft.com/library/windows/apps/br209652)

 

 




