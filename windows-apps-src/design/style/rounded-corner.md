---
title: Corner radius
description: Learn about rounded corners principles, design approaches, and customization options.
ms.date: 10/08/2019
ms.topic: article
keywords: windows 10, uwp, corner radius, rounded
---

# Corner radius

Starting with version 2.2 of the [Windows UI Library](/uwp/toolkits/winui/) (WinUI), the default style for many controls has been updated to use rounded corners. These new styles are intended to evoke warmth and trust, and make the UI easier for users to visually process.

Here are two Button controls, the first without rounded corners and the second using the new rounded corner style.

![Buttons without and with rounded corners](images/rounded-corner/my-button.png)

When you install the NuGet package for WinUI 2.2 or later, new default styles are installed for both WinUI controls and platform controls. These styles are used automatically when you use WinUI 2.2 in your app; there is no further action you need to take to use the new styles. However, later in this article we show how to customize the rounded corners if you need to do so.

> [!IMPORTANT]
> Some controls are available both in the platform ([Windows.UI.Xaml.Controls](/uwp/api/windows.ui.xaml.controls)) and in WinUI ([Microsoft.UI.Xaml.Controls](/uwp/api/microsoft.ui.xaml.controls?view=winui-2.2)); for example, **TreeView** or **ColorPicker**. When you use WinUI in your app, you should use the WinUI version of the control. Corner rounding might be applied inconsistently in the platform version when used with WinUI.

> **Important APIs**: [Control.CornerRadius property](/uwp/api/windows.ui.xaml.controls.control.cornerradius)

## Default control designs

There are three areas of the controls where the rounded corner styles are used: rectangular elements, flyout elements, and bar elements.

### Corners of rectangle UI elements

- These UI elements include basic controls like buttons that users see on screen at all times.
- The default radius value we use for these UI elements is **2px**.

![Button with rounded corners highlighted](images/rounded-corner/button.png)

**Controls**

- AutoSuggestBox
- Button
  - ContentDialog buttons
- CalendarDatePicker
- CheckBox
  - TreeView multi-select check boxes
- ComboBox
- DatePicker
- DropDownButton
- FlipView
- PasswordBox
- RichEditBox
- SplitButton
- TextBox
- TimePicker
- ToggleButton
- ToggleSplitButton

### Corners of flyout and overlay UI elements

- These can be transient UI elements that appear on screen temporarily, like MenuFlyout, or elements that overlay other UI, like TabView tabs.
- The default radius value we use for these UI elements is **4px**.

![Flyout example](images/rounded-corner/flyout.png)

**Controls**

- CommandBarFlyout
- ContentDialog
- Flyout
- MenuFlyout
- TabView tabs
- TeachingTip
- ToolTip
- Flyout part (when open)
  - AutoSuggestBox
  - CalendarDatePicker
  - ComboBox
  - DatePicker
  - DropDownButton
  - MenuBar
  - SplitButton
  - TimePicker
  - ToggleSplitButton

### Bar elements

- These UI elements are shaped like bars or lines; for example, ProgressBar.
- The default radius values we use here are **2px**.

![Progress bar example](images/rounded-corner/bars.png)

**Controls**

- NavigationView selection indicator
- Pivot selection indicator
- ProgressBar
- ScrollBar (when `IndicatorMode=TouchIndicator`)
- Slider
  - ColorPicker color slider
  - MediaTransportControls seek bar slider

## Customization options

The default corner radii values that we provide are not set in stone and there are a few ways you can easily modify the amount of rounding on the corners. This can be done through two global resources, or through the [CornerRadius](/uwp/api/windows.ui.xaml.controls.control.cornerradius) property directly on the control, depending on the level of customization granularity you want.

### When not to round

There are instances where the corner of a control should not be rounded, and we don't round these by default.

- When multiple UI elements that are housed inside a container touch each other, such as the two parts of a SplitButton. There should be no space when they contact.

![SplitButton](images/rounded-corner/split-button-2.png)

- When a control is housed inside another container, like a ScrollBar's bar and buttons that are part of the ScrollBar container, which is also part of a ScrollViewer.

![ScrollBar](images/rounded-corner/scrollbar.png)

- When a flyout UI element is connected to a UI that invokes the flyout on one side.

![AutoSuggest](images/rounded-corner/autosuggest.png)

### Keyboard focus rectangle and shadow

Our default design does not do any special work to round the corners of the keyboard focus rectangle or control shadow. Using a higher corner radius value will not break them functionally; however, it is good to be aware of this to avoid unwanted visual glitches that could be introduced with a larger value.

Here is an example of how a larger corner radius can make the shadow look undesirable:

![ContentDialogShadow](images/rounded-corner/larger-corner-radius.png)

### Rounded corners and performance

Rendering rounded corners naturally uses more drawing power than rendering square corners. When selecting the default corner radius values, we not only considered the design principles but we were also careful to ensure our default controls perform well when you use them in your apps.

When thinking about app performance in this context, you should primarily consider page load time and app launch time. Consider that rounded corners on a larger UI surface have a greater impact on performance. Avoid drawing rounded corners on a full screen app UI. This is less of an issue if the UI is displayed briefly and after the page is loaded, like a ContentDialog.

### Page or app-wide CornerRadius changes

There are 2 app resources that control the corner radii of all the controls:

- `ControlCornerRadius` - default is 2px.
- `OverlayCornerRadius` - default is 4px.

If you override the value of these resources at any scope, it will affect all controls within that scope accordingly.

This means if you want to change the roundness of all controls where roundness could be applied, you can define both resources at the app level with the new CornerRadius values like this:

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
            <ResourceDictionary>
                <CornerRadius x:Key="OverlayCornerRadius">0</CornerRadius>
                <CornerRadius x:Key="ControlCornerRadius">0</CornerRadius>
            </ResourceDictionary>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

Alternatively, if you want to change all controls' roundness within a particular scope, like at a page or container level, you can follow a similar pattern:

```xaml
<Grid>
    <Grid.Resources>
        <CornerRadius x:Key="ControlCornerRadius">8</CornerRadius>
    </Grid.Resources>
    <Button Content="Button"/>
</Grid>
```

> [!NOTE]
> The `OverlayCornerRadius` resource must be defined at the app level in order to take effect.
>
>This is because popups and flyouts are dynamic and created at the root element in the Visual Tree, so any resources that they use must also be defined there. Otherwise, they're out of scope.

### Per-control CornerRadius changes

You can modify the [CornerRadius](/uwp/api/windows.ui.xaml.controls.control.cornerradius) property on controls directly if you want to change the roundness of only a select number of controls.

|Default | Property modified |
|:-- |:-- |
|![DefaultCheckBox](images/rounded-corner/default-checkbox.png)| ![CustomCheckBox](images/rounded-corner/custom-checkbox.png)|
|`<CheckBox Content="Checkbox"/>` | `<CheckBox Content="Checkbox" CornerRadius="5"/> ` |

Not all controls' corners will respond to their `CornerRadius` property being modified. To ensure that the control whose corners you wish to round will indeed respond to their `CornerRadius` property the way you expect, first check that the `ControlCornerRadius` or `OverlayCornerRadius` global resources affect the control in question. If they do not, check that the control you wish to round has corners at all. Many of our controls do not render actual edges and therefore cannot make proper use of the `CornerRadius` property.

### Basing custom styles on WinUI

You can base your custom styles on the WinUI rounded corner styles by specifying the correct `BasedOn` attribute in your style. For example to create a custom button style based on WinUI button style, do the following:

```xaml
<Style x:Key="MyCustomButtonStyle" BasedOn="{StaticResource DefaultButtonStyle}">
   ...
</Style>
```

In general, WinUI control styles follow a consistent naming convention: "DefaultXYZStyle" where "XYZ" is the name of the control. For full reference, you can browse the XAML files in the WinUI repository.
