---
Description: This topic describes the new Windows UI for selecting and manipulating text, images, and controls and provides user experience guidelines that should be considered when using these new selection and manipulation mechanisms in your Windows app.
title: Selecting text and images
ms.assetid: d973ffd8-602e-47b5-ab0b-4b2a964ec53d
label: Selecting text and images
template: detail.hbs
keywords: keyboard, text, input, user interactions
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Selecting text and images


This article describes selecting and manipulating text, images, and controls and provides user experience guidelines that should be considered when using these mechanisms in your apps.

> **Important APIs**: [**Windows.UI.Xaml.Input**](/uwp/api/Windows.UI.Xaml.Input), [**Windows.UI.Input**](/uwp/api/Windows.UI.Input)
 


## Dos and don'ts


-   Use font glyphs when implementing your own gripper UI. The gripper is a combination of two Segoe UI fonts that are available system-wide. Using font resources simplifies rendering issues at different dpi and works well with the various UI scaling plateaus. When implementing your own grippers, they should share the following UI traits:

    -   Circular shape
    -   Visible against any background
    -   Consistent size
-   Provide a margin around the selectable content to accommodate the gripper UI. If your app enables text selection in a region that doesn't pan/scroll, allow a 1/2 gripper margin on the left and right sides of the text area and 1 gripper height on the top and bottom sides of the text area (as shown in the following images). This ensures that the entire gripper UI is exposed to the user and minimizes unintended interactions with other edge-based UI.

    ![text selection gripper margins](images/textselection-gripper-margins.png)

-   Hide grippers UI during interaction. Eliminates occlusion by the grippers during the interaction. This is useful when a gripper isn't completely obscured by the finger or there are multiple text selection grippers. This eliminates visual artifacts when displaying child windows.

-   Don't allow selection of UI elements such as controls, labels, images, proprietary content, and so on. Typically, Windows applications allow selection only within specific controls. Controls such as buttons, labels, and logos are not selectable. Assess whether selection is an issue for your app and, if so, identify the areas of the UI where selection should be prohibited. 

## Additional usage guidance


Text selection and manipulation is particularly susceptible to user experience challenges introduced by touch interactions. Mouse, pen/stylus, and keyboard input are highly granular: a mouse click or pen/stylus contact is typically mapped to a single pixel, and a key is pressed or not pressed. Touch input is not granular; it's difficult to map the entire surface of a fingertip to a specific x-y location on the screen to place a text caret accurately.

**Considerations and recommendations**

Use the built-in controls exposed through the language frameworks in Windows to build apps that provide the full platform user interaction experience, including selection and manipulation behaviors. You'll find the interaction functionality of the built-in controls sufficient for the majority of Windows apps.

When using standard Windows text controls, the selection behaviors and visuals described in this topic cannot be customized.

**Text selection**

If your app requires a custom UI that supports text selection, we recommend that you follow the Windows selection behaviors described here.

**Editable and non-editable content**


With touch, selection interactions are performed primarily through gestures such as a tap to set an insertion cursor or select a word, and a slide to modify a selection. As with other Windows touch interactions, timed interactions are limited to the press and hold gesture to display informational UI. For more information, see [Guidelines for visual feedback](guidelines-for-visualfeedback.md).

Windows recognizes two possible states for selection interactions, editable and non-editable, and adjusts selection UI, feedback, and functionality accordingly.

**Editable content**

Tapping within the left half of a word places the cursor to the immediate left of the word, while tapping within the right half places the cursor to the immediate right of the word.

The following image demonstrates how to place an initial insertion cursor with gripper by tapping near the beginning or ending of a word.

![tap (or press and hold) left side of a word to place a caret and gripper at the beginning of that word. tap (or press and hold) the right side of a word to place a caret and gripper at the end of that word.](images/textselection-place-caret.png)

The following image demonstrates how to adjust a selection by dragging the gripper.

![drag the gripper in either direction to adjust selection (the first gripper remains anchored and a second gripper is displayed). drag either gripper to make subsequent adjustments.](images/adjust-selection.png)

The following images demonstrate how to invoke the context menu by tapping within the selection or on a gripper (press and hold can also be used).

![tap (or press and hold) within the selection or on a gripper to invoke the context menu.](images/textselection-show-context.png)

**Note**  These interactions vary somewhat in the case of a misspelled word. Tapping a word that is marked as misspelled will both highlight the entire word and invoke the suggested spelling context menu.

 

**Non-editable content**

The following image demonstrates how to select a word by tapping within the word (no spaces are included in the initial selection).

![tap within a word to select it (no spaces are included in the initial selection).](images/select-word.png)

Follow the same procedures as for editable text to adjust the selection and display the context menu.

**Object manipulation**

Wherever possible, use the same (or similar) gripper resources as text selection when implementing custom object manipulation in a Windows app. This helps provide a consistent interaction experience across the platform.

For example, grippers can also be used in image processing apps that support resizing and cropping or media player apps that provide adjustable progress bars, as shown in the following images.

![media player with progress gripper](images/gripper-mediaplayer.png)

*Media player with adjustable progress bar.*

![image with crop grippers](images/gripper-imagemanip.png)

*Image editor with cropping grippers.*

## Related articles

### For developers

- [Custom user interactions](../layout/index.md)

### Samples

- [Basic input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicInput)
- [Low latency input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LowLatencyInput)
- [User interaction mode sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/UserInteractionMode)
- [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals)

### Archive samples

- [Input: XAML user input events sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20XAML%20user%20input%20events%20sample)
- [Input: Device capabilities sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Input%20Device%20capabilities%20sample%20(Windows%208))
- [Input: Touch hit testing sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20desktop%20samples/%5BC%2B%2B%5D-Windows%208%20desktop%20samples/C%2B%2B/Windows%208%20desktop%20samples/Input%20Touch%20hit%20testing%20sample)
- [XAML scrolling, panning, and zooming sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Universal%20Windows%20app%20samples/111487-Universal%20Windows%20app%20samples/XAML%20scrolling%2C%20panning%2C%20and%20zooming%20sample)
- [Input: Simplified ink sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20Simplified%20ink%20sample)
- [Input: Windows 8 gestures sample](/samples/browse/?redirectedfrom=MSDN-samples)
- [Input: Manipulations and gestures sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20Gestures%20and%20manipulations%20with%20GestureRecognizer)
- [DirectX touch input sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%2B%2B%5D-Windows%208%20app%20samples/C%2B%2B/Windows%208%20app%20samples/DirectX%20touch%20input%20sample%20(Windows%208))