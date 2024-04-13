---
author: drewbatgit
description: Learn about the fundamentals of designing the UI for a Windows widget.
title: Widget design fundamentals
ms.author: drewbat
ms.date: 01/19/2022
ms.topic: article
keywords: windows 11, widgets
ms.localizationpriority: medium
---

# Widget design fundamentals

This article provides detailed guidance for designing the UI for a Windows widget.


## Widget sizes

![A screenshot showing blank widget templates illustrating small, medium, and large sizes.](./images/widgets-sizes-1.png)
![A screenshot showing examples of small, medium, and large sizes for a weather widget.](./images/widgets-sizes-2.png)
![A screenshot showing examples of small, medium, and large sizes for a traffic widget.](./images/widgets-sizes-3.png)

Widgets provide three sizes for the user to choose from. It is recommended that you create and consider all 3 sizes and adapt your design specifically for each size. Small and medium sizes provide better discoverability as they get surfaced more often within the dynamic feed. Large sizes are useful for displaying more in-depth information. Supporting multiple sizes allows flexibility in users customizing the widgets they choose to pin to the widgets board.  



### Small

The widget principles *glanceable* and *focused* become more important in design decisions made for the small size widget. The small size widget should not try to force all of the functionality that could comfortably fit in a large widget. Focus on one user interaction or piece of key information that can be surfaced here with 1 touch target. 

### Medium

The medium size widget allows more room compared to the small, and so more functionality or additional information can be included. The medium widget could also provide the same focused experience as the small widget, but provide 2-3 touch targets.

### Large  

Large sizes allow for more information to be presented, but the content should still be focused and easily consumable. Alternatively, a large size card could highlight one image or topic and have a more immersive experience. The large size should have no more than 3-4 touch targets.


## Color and theming

![Three example widget templates demonstrating the light theme. The first is an empty widget with a white backgronud. The second is an empty widget with a light gradient background. The third is a widget with an image background. All three have the word "text" in dark font to demonstrate the contrast with the light background. ](./images/widgets-color-theme-1.png)

![Three example widget templates demonstrating the dark theme. The first is an empty widget with a black backgroud. The second is an empty widget with a dark gradient background. The third is a widget with an image background. All three have the word "text" in a light font to demonstrate the contrast with the dark background.](./images/widgets-color-theme-2.png)

Windows 11 supports two color modes: light and dark. Each mode consists of a set of neutral color values that are automatically adjusted to ensure optimal contrast. For each widget size you support, make sure to create separate designs for light and dark themes so that the widget integrates seamlessly within the wider operating system and user’s theme choice. The widget background supports customization with either a solid light/dark background, gradient tint, or image background. 

![Two widget examples side by side. The left example has a light gradient background and text in a light grey font. The image is marked with a red X to indicate that the low contrast makes the text unreadable. The right image has a light gradient background and text in a dark black font. The image is marked with a green check to indicate that the high contrast makes the text legible.](./images/widgets-light-theme-font-color.png)

![Two widget examples side by side. The left example has a highly saturated color background image and text in a dark font. The image is marked with a red X to indicate that the low contrast makes the text unreadable. The right image has a desaturated color background and text in a dark black font. The image is marked with a green check to indicate that the high contrast makes the text legible.](./images/widgets-background-image-font-color.png)

When choosing background colors, images, and content, make sure that there is enough color contrast to ensure legibility and accessibility.  

The Web Content Accessibility Guidelines (WCAG) 2.0 level AA requires a contrast ratio of at least 4.5:1 for normal text and 3:1 for large text. WCAG 2.1 requires a contrast ratio of at least 3:1 for graphics and user interface components (such as form input borders). WCAG Level AAA requires a contrast ratio of at least 7:1 for normal text and 4.5:1 for large text. Large text is defined as 14 point (typically 18.66px) and bold or larger, or 18 point (typically 24px) or larger. 

## Margins

![A diagram of a widget with guidelines indicating the margins. Next to this is a diagram of a widget where the area inside the margins is colored blue to show the content area.](./images/widgets-margins.png)

Each widget has a 16px margin around it and a 48px [Attribution area](widgets-states-and-ui.md#attribution-area) in which content cannot be placed. The only component that can live in the right side margin and bottom margin are the pagination dots. For examples of the positioning of the pagination dots, see the pagination section of [Widget interaction design guidance](../../design/widgets/widgets-interaction-design.md#pagination).

![Two widget examples side by side. The left image shows guide lines dividing the widget into three columns, illustrating 4 pixel gutters between the columns. The right image shows guide lines dividing the widget into three rows, illustrating 4 pixel gutters between the rows.](./images/widgets-gutters.png)

For widgets that use containers, the gutter between each element is 4px, and the containers should touch the edges of the margins. Your content should also use spacing and sizing values of [Multiples of Four Px](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design#multiples-of-four) to achieve a clean, pixel perfect design across different screen resolutions.
 
You should also consult the guidance for spacing and gutters in [Content design basics for Windows apps](/windows/apps/design/basics/content-basics ) when designing your content.

## Typography

![Two strings of the phrase "The quick brown fox jumped over the lazy dog" side by side. The one on the right has a heavier font weight.](./images/widgets-font-weights-1.png)

![A table showing example text for different type elements of a widget. The table is recreated in plain text without the rendered appearance within the text below the image.](./images/widgets-font-weights-2.png)

For accessibility, the following table presents the text of the table shown in the image above. 

| Example | Size / Line height | Adaptive cards formula |
|---------|--------------------|------------------------|
| Caption  | 12/16 epx | Small, Lighter |
| Body     | 14/20 epx | Default, Lighter |
| Body (for hyperlinks) | 14/20 epx | Default, Lighter, Accent |
| Body Strong | 14/20 epx | Default, Bolder |
| Body Large | 18/24 epx | Medium, Lighter |
| Body Largest | 18/24 epx | Medium, Bolder |
| Subtitle | 20/28 epx | Large, Bolder |
| Title | 28/36 epx | Extra Large, Bolder | 

Segoe UI is the typeface used in Widgets and across Windows. The above type ramp includes the formulations of how to properly set the right styles in the Adaptive Cards Designer. Typeface styling should not deviate from the specified formulas above. For more information on using the Adaptive Cards Designer to create widget templates, see [Create a widget template with the Adaptive Card Designer](widgets-create-a-template.md).

![Two example widgets with the phrase "The quick brown fox jumped over the lazy dog" and the text "Hyperlink". The left image has dark text on a light background. The right image has light text on a dark background. The hyperlink text is blue in both images.](./images/widgets-designer-default-font-colors.png)

Within the Adaptive Cards Designer, titles and body copy use the default color associated with the widget theme. An additional option to differentiate title from body copy further is to use the subtle version of the default color. The accent color is only used for hyperlinks.  

## Iconography

## Profile pictures

![Four instances of a circular profile picture in descending sizes from left to right. The images are labeled "96", "48", "32", and "24".](./images/widgets-profile-pictures.png)

If your widget includes showing user profiles (for example, a social media feed or stream) use one of the following allowed circle profile sizes: 96x96px, 48x48px, 32x32px, or 24x24px.

## Tool tips

![An image of a calendar widget showing a calendar appointment. The mouse cursor is hovering over the appointment subject line, which is truncated, and a tool-tip shows the full subject line.](./images/widgets-tool-tips.png)

Tool tips can be used when title text gets truncated in the widget. For best practices, text should fit neatly within the widget space and not need truncation however, that may not always happen depending on scenarios like language localization, system text scaling, or when quoting something (i.e. article title, name of a song). This does not apply to body text on a widget.
