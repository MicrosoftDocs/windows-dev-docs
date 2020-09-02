---
description: Get design guidance and coding instructions for adding controls &amp; patterns to your Windows app. Find  over 45 powerful controls for you to use with your app.
title: Windows Controls and patterns - Windows app development
keywords: uwp controls, user interface, app controls, windows controls
label: Controls & patterns
template: detail.hbs
ms.date: 03/23/2020
ms.topic: article
ms.assetid: ce2e611c-c419-4a14-9095-b88ac711d1b8
ms.localizationpriority: medium
---

# Controls for Windows apps

![Controls](../images/controls-2x.png)

In Windows app development, a <i>control</i> is a UI element that displays content or enables interaction. Controls are the building blocks of the user interface. A <i>pattern</i> is a recipe for combining several controls to make something new.

We provide 45+ controls for you to use, ranging from simple buttons to powerful data controls like the grid view.  These controls are a part of the Fluent Design System and can help you create a bold, scalable UI that looks great on all devices and screen sizes.

The articles in this section provide design guidance and coding instructions for adding controls & patterns to your Windows app.

## Intro

General instructions and code examples for adding and styling controls in XAML and C#.

:::row:::
    :::column:::
      <p><b><a href="controls-and-events-intro.md">Add controls and handle events</a></b> <br/>
      There are 3 key steps to adding controls to your app: Add a control to your app UI, set properties on the control, and add code to the control's event handlers so that it does something.</p>
    :::column-end:::
    :::column:::
      <p><b><a href="xaml-styles.md">Styling controls</a></b> <br/>
      You can customize the appearance of your apps in many ways by using the XAML framework. Styles let you set control properties and reuse those settings for a consistent appearance across multiple controls.</p>
    :::column-end:::
:::row-end:::

## Get the Windows UI Library

|  |  |
| - | - |
| ![WinUI logo](images/winui-logo-64x64.png) | Some controls are only available in the Windows UI Library (WinUI), a NuGet package that contains new controls and UI features. To get it, see the [Windows UI Library overview and installation instructions](/uwp/toolkits/winui/).<br/>Starting with WinUI 2.2, the default style for many controls has been updated to use rounded corners. For more info, see [Corner radius](../style/rounded-corner.md). |

## Alphabetical index

Detailed information about specific controls and patterns. (For a list sorted by function, see [Index of controls by function](controls-by-function.md).)

:::row:::
    :::column:::

- Animated visual player (see [Lottie](/windows/communitytoolkit/animations/lottie)) ![WinUI logo](images/winui-logo-16x16.png)
- [Auto-suggest box](auto-suggest-box.md)
- [Button](buttons.md)
- [Calendar date picker](calendar-date-picker.md)
- [Calendar view](calendar-view.md)
- [Checkbox](checkbox.md)
- [Color picker](color-picker.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Combo box](combo-box.md)
- [Command bar](app-bars.md)
- [Command bar flyout](command-bar-flyout.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Contact card](contact-card.md)
- [Content dialog](dialogs-and-flyouts/dialogs.md)
- [Content link](content-links.md)
- [Context menu](menus.md)
- [Date picker](date-picker.md)
- [Dialogs and flyouts](dialogs-and-flyouts/index.md)
- [Drop down button](buttons.md#create-a-drop-down-button) ![WinUI logo](images/winui-logo-16x16.png)
- [Flip view](flipview.md)
- [Flyout](dialogs-and-flyouts/flyouts.md)
- [Forms](forms.md) (pattern)
- [Grid view](listview-and-gridview.md)
- [Hyperlink](hyperlinks.md)
- [Hyperlink button](hyperlinks.md#create-a-hyperlinkbutton)
- [Images and image brushes](images-imagebrushes.md)
- [Inking controls](inking-controls.md)
- [List view](listview-and-gridview.md)
- [Map control](../../maps-and-location/display-maps.md)
- [Master/details](master-details.md) (pattern)
- [Media playback](media-playback.md)
- [Menu bar](menus.md#create-a-menu-bar) ![WinUI logo](images/winui-logo-16x16.png)
- [Menu flyout](menus.md)
- [Navigation view](navigationview.md) ![WinUI logo](images/winui-logo-16x16.png)

    :::column-end:::
    :::column:::

- [Number box](number-box.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Parallax view](..\motion\parallax.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Password box](password-box.md)
- [Person picture](person-picture.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Pivot](pivot.md)
- [Progress bar](progress-controls.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Progress ring](progress-controls.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Radio button](radio-button.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Rating control](rating.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Repeat button](buttons.md#create-a-repeat-button)
- [Rich edit box](rich-edit-box.md)
- [Rich text block](rich-text-block.md)
- [Scroll viewer](scroll-controls.md)
- [Search](search.md) (pattern)
- [Semantic zoom](semantic-zoom.md)
- [Shapes](shapes.md)
- [Slider](slider.md)
- [Split button](buttons.md#create-a-split-button) ![WinUI logo](images/winui-logo-16x16.png)
- [Split view](split-view.md)
- [Swipe control](swipe.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Tab view](tab-view.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Teaching tip](dialogs-and-flyouts/teaching-tip.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Text block](text-block.md)
- [Text box](text-box.md)
- [Time picker](time-picker.md)
- [Toggle switch](toggles.md)
- [Toggle button](buttons.md)
- [Toggle split button](buttons.md#create-a-toggle-split-button)
- [Tooltips](tooltips.md)
- [Tree view](tree-view.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Two-pane view](two-pane-view.md) ![WinUI logo](images/winui-logo-16x16.png)
- [Web view](web-view.md)

    :::column-end:::
:::row-end:::




## XAML Controls Gallery

Get the _XAML Controls Gallery_ app from the Microsoft Store to see these controls and the Fluent Design System in action. The app is an interactive companion to this website. When you have it installed, you can use links on individual control pages to launch the app and see the control in action.

<a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a>

<a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a>

<img src="images/xaml-controls-gallery.png" alt="XAML Controls Gallery screen" />

## Additional controls

Additional controls for Windows development are available from companies such as <a href="https://www.telerik.com/">Telerik</a>, <a href="https://www.syncfusion.com/uwp-ui-controls">SyncFusion</a>, <a href="https://www.devexpress.com/Products/NET/Controls/Win10Apps/">DevExpress</a>,
<a href="https://www.infragistics.com/products/universal-windows-platform">Infragistics</a>, <a href="https://www.componentone.com/Studio/Platform/UWP">ComponentOne</a>, and <a href="https://www.actiprosoftware.com/products/controls/universal">ActiPro</a>. These controls provide additional support for enterprise and .NET developers by augmenting the standard system controls with custom controls and services.