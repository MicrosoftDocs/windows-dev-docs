---
Description: Just as people use a combination of voice and gesture when communicating with each other, multiple types and modes of input can also be useful when interacting with an app.
title: Multiple inputs design guidelines
ms.assetid: 03EB5388-080F-467C-B272-C92BE00F2C69
label: Multiple inputs
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Multiple inputs


Just as people use a combination of voice and gesture when communicating with each other, multiple types and modes of input can also be useful when interacting with an app.


To accommodate as many users and devices as possible, we recommend that you design your apps to work with as many input types as possible (gesture, speech, touch, touchpad, mouse, and keyboard). Doing so will maximize flexibility, usability, and accessibility.

To begin, consider the various scenarios in which your app handles input. Try to be consistent throughout your app, and remember that the platform controls provide built-in support for multiple input types.

-   Can users interact with the application through multiple input devices?
-   Are all input methods supported at all times? With certain controls? At specific times or circumstances?
-   Does one input method take priority?

## Single (or exclusive)-mode interactions


With single-mode interactions, multiple input types are supported, but only one can be used per action. For example, speech recognition for commands, and gestures for navigation; or, text entry using touch or gestures, depending on proximity.

## Multimodal interactions

With multimodal interactions, multiple input methods in sequence are used to complete a single action.

Speech + gesture  
The user points to a product, and then says “Add to cart.”

Speech + touch  
The user selects a photo using press and hold, and then says “Send photo.”



