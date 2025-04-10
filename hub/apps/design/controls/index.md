---
description: Get design guidance and coding instructions for adding controls to your Windows app. Find  over 45 powerful controls you can use with your app.
title: Windows Controls and patterns - Windows app development
keywords: uwp controls, user interface, app controls, windows controls
label: Controls & patterns
template: detail.hbs
ms.date: 02/27/2025
ms.topic: article
ms.assetid: ce2e611c-c419-4a14-9095-b88ac711d1b8
ms.localizationpriority: medium
---

# Controls for Windows apps

In Windows app development, a *control* is a UI element that displays content or enables interaction. Controls are the building blocks of the user interface. A *pattern* is a recipe for combining several controls to make something new.

We provide 45+ controls for you to use, ranging from simple buttons to powerful data controls like the grid view.  These controls are a part of the Fluent Design System and can help you create a bold, scalable UI that looks great on all devices and screen sizes.

The articles in this section provide design guidance and coding instructions for adding controls & patterns to your Windows app.

## General instructions and code examples

The topics highlighted here provide instructions and code examples for adding and styling controls in XAML and C#.

:::row:::
    :::column:::
      [**Add controls and handle events**](controls-and-events-intro.md)

      There are 3 key steps to adding controls to your app: Add a control to your app UI, set properties on the control, and add code to the control's event handlers so that it does something.</p>
    :::column-end:::
    :::column:::
      [**Styling controls**](../../develop/platform/xaml/xaml-styles.md)

      You can customize the appearance of your apps in many ways by using the XAML framework. Styles let you set control properties and reuse those settings for a consistent appearance across multiple controls.</p>
    :::column-end:::
:::row-end:::

## Get WinUI

:::row:::
   :::column:::
      ![WinUI logo](images/winui-logo-64x64.png)
   :::column-end:::
   :::column span="3":::
      Some controls are only available in WinUI, a NuGet package that contains new controls and UI features. To get it, see the [WinUI overview and installation instructions](/uwp/toolkits/winui/).
   :::column-end:::
   :::column:::

   :::column-end:::
:::row-end:::

## Index of controls

The following table lists the common Windows app controls and patterns along with those that are exclusive to WinUI.

:::row:::
    :::column span="2":::
        **Common Windows app controls**
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png":::  **WinUI only**
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Auto-suggest box](auto-suggest-box.md)
    :::column-end:::
    :::column:::
        [Button](buttons.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Animated icon](animated-icon.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Calendar date picker](calendar-date-picker.md)
    :::column-end:::
    :::column:::
        [Calendar view](calendar-view.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: Animated visual player (see [Lottie](/windows/communitytoolkit/animations/lottie))
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Checkbox](checkbox.md)
    :::column-end:::
    :::column:::
        [Combo box](combo-box.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Breadcrumb bar](breadcrumbbar.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Command bar](command-bar.md)
    :::column-end:::
    :::column:::
        [Contact card](contact-card.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Color picker](color-picker.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Content dialog](dialogs-and-flyouts/dialogs.md)
    :::column-end:::
    :::column:::
        [Content link](content-links.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Command bar flyout](command-bar-flyout.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Context menu](menus.md)
    :::column-end:::
    :::column:::
        [Date picker](date-picker.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Drop down button](buttons.md#create-a-drop-down-button) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Dialogs and flyouts](dialogs-and-flyouts/index.md)
    :::column-end:::
    :::column:::
        [Flip view](flipview.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Expander](expander.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Flyout](dialogs-and-flyouts/flyouts.md)
    :::column-end:::
    :::column:::
        [Forms](forms.md) (pattern)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Info bar](infobar.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Grid view](listview-and-gridview.md)
    :::column-end:::
    :::column:::
        [Hyperlink](hyperlinks.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Menu bar](menus.md#create-a-menu-bar) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Hyperlink button](hyperlinks.md#create-a-hyperlinkbutton)
    :::column-end:::
    :::column:::
        [Images and image brushes](images-imagebrushes.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Navigation view](navigationview.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Inking controls](inking-controls.md)
    :::column-end:::
    :::column:::
        [List/details](list-details.md) (pattern)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Number box](number-box.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [List view](listview-and-gridview.md)
    :::column-end:::
    :::column:::
        [Map control](/windows/uwp/maps-and-location/display-maps)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Parallax view](..\motion\parallax.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Media playback](media-playback.md)
    :::column-end:::
    :::column:::
        [Menu flyout](menus.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Person picture](person-picture.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Password box](password-box.md)
    :::column-end:::
    :::column:::
        [Repeat button](buttons.md#create-a-repeat-button)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Pips pager](pipspager.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Rich edit box](rich-edit-box.md)
    :::column-end:::
    :::column:::
        [Rich text block](rich-text-block.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Progress bar](progress-controls.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Scroll viewer](scroll-controls.md)
    :::column-end:::
    :::column:::
        [Semantic zoom](semantic-zoom.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Progress ring](progress-controls.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Shapes](shapes.md)
    :::column-end:::
    :::column:::
        [Slider](slider.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Radio button](radio-button.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Split view](split-view.md)
    :::column-end:::
    :::column:::
        [Text block](text-block.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Rating control](rating.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Text box](text-box.md)
    :::column-end:::
    :::column:::
        [Time picker](time-picker.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Split button](buttons.md#create-a-split-button) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Toggle switch](toggles.md)
    :::column-end:::
    :::column:::
        [Toggle button](buttons.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Swipe control](swipe.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Toggle split button](buttons.md#create-a-toggle-split-button)
    :::column-end:::
    :::column:::
        [Tooltips](tooltips.md)
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Tab view](tab-view.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [Web view](web-view.md)
    :::column-end:::
    :::column:::
        
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Teaching tip](dialogs-and-flyouts/teaching-tip.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        
    :::column-end:::
    :::column:::
        
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Tree view](tree-view.md) 
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
        
    :::column-end:::
    :::column:::
        
    :::column-end:::
    :::column:::
        :::image type="icon" source="images/winui-logo-16x16.png"::: [Two-pane view](two-pane-view.md) 
    :::column-end:::
:::row-end:::

## WinUI Gallery

Get the *WinUI Gallery* apps from the Microsoft Store to see XAML controls and the [Fluent Design System](https://developer.microsoft.com/fluentui#/) in action. The **WinUI 3 Gallery** and **WinUI 2 Gallery** apps include interactive examples of most WinUI 3 and WinUI 2 controls, features, and functionality. The apps are an interactive companion to this website. When you have them installed, you can use links on individual control pages to launch the app and see the control in action.

> [!div class="checklist"]
>
> - Get the [**WinUI 3 Gallery**](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) and the [**WinUI 2 Gallery**](https://www.microsoft.com/store/productId/9MSVH128X2ZT) from the Microsoft Store.
> - Get the source code for both from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (use the *main* branch for WinUI 3 and the *winui2* branch for WinUI 2).

## Additional controls

Additional controls for Windows development are available from companies such as <a href="https://www.telerik.com/">Telerik</a>, <a href="https://www.syncfusion.com/uwp-ui-controls">SyncFusion</a>, <a href="https://www.devexpress.com/Products/NET/Controls/Win10Apps/">DevExpress</a>,
<a href="https://www.infragistics.com/products/universal-windows-platform">Infragistics</a>, <a href="https://www.componentone.com/Studio/Platform/UWP">ComponentOne</a>, and <a href="https://www.actiprosoftware.com/products/controls/universal">ActiPro</a>. These controls provide additional support for enterprise and .NET developers by augmenting the standard system controls with custom controls and services.
