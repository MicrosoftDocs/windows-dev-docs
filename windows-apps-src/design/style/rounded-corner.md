---
title: Corner Radius
description: Learn about rounded corners principles, design approaches, and customization options.
ms.date: 10/08/19
ms.topic: article
keywords: windows 10, uwp, corner radius, rounded
---

# Corner Radius
![button](images/rounded-corner/my-button.png)

## Principles
Rounded corners lessen cognitive load for users to visually process UI and evoke warmth and trust.

## Default control designs
There are mainly 3 types of UI parts where the radii of the corners are being rounded and here are the descriptions of how this is applied to default controls.

*Corners of rectangle UI elements*
- These UI elements include basic controls like buttons that users see on screen at all times.
- The default radius value we use are 2px for these UI elements.
![Button](images/rounded-corner/button.png)

**Controls being rounded**
- Button
- CheckBox
- ComboBox
- DropDownButton
- Slider
- SplitButton
- ToggleButton
- ToggleSplitButton
- Flipview
- GridView
- ListView
- TreeView
- ContentDialog
- AutoSuggestBox
- PasswordBox
- RichEditBox
- TextBox
- DatePicker
- CalendarDatePicker
- Tab control

*Corners of flyout UI elements*
- These are transient UI elements that appear on screen temporarily, for example MenuFlyout or Flyout.
- The default radius value we use here are 4px.

![flyout](images/rounded-corner/flyout.png)

**Controls being rounded**
- CalendarDatePicker
- DatePicker
- TimePicker
- Flyout
- TeachingTip
- ToolTip
- DropDownButton
- SplitButton
- Slider (tooltip flyout only)
- AutoSuggestBox
- CommandBarFlyout
- MenuFlyout
- ComboBox
- ColorPicker
- MediaPlayerElement
- ContentDialog
- MenuBar
- ToggleSplitButton

*Rounding of bars*
- These UI elements are shaped like bars or lines, for example ProgressBar.
- The default radius values we use here are 2px. One exception is the slider in ColorPicker which will be 6px because the bar is wider.
![bars](images/rounded-corner/bars.png)

**Controls being rounded**
- NavigationView's selection indicator
- Pivot's selection indicator
- ScrollIndicator
- ProgressBar
- Slider
- ColorPicker's slider
- MediaPlayerElement (tooltip flyout only)

Note: WebView uses the scroll indicator coming from HTML directly. So this will not be rounded until scroll indicator from hosted HTML gets the rounded treatment.

### When not to round
- When multiple UI elements that are housed inside a container touch each other (e.g. ScrollBar's bar and buttons touch each other inside the ScrollBar container), there should be no space when they contact.
![SplitButton](images/rounded-corner/split-button-2.png)
- When a control is housed inside another container (e.g. ScrollBar's bar and buttons that are part of the ScrollBar container which is also part of a ScrollViewer).
![ScrollBar](images/rounded-corner/scrollbar.png)
- When a flyout UI element is connected to a UI that invokes the flyout on one side.
![AutoSuggest](images/rounded-corner/autosuggest.png)

## Note on keyboard focus rectangle and shadow
Currently our default design does not require any special work to round the corners of keyboard focus rectangle and shadow. Using a higher corner radius value will not break them functionally, however it is good to be aware of this to avoid unwanted visual glitches that could be introduced with a larger value.

Here is an example of how a larger corner radius can make the shadow look undesirable:
![ContentDialogShadow](images/rounded-corner/larger-corner-radius.png)

## Rounded Corner and Performance
People may not always be aware that drawing rounded corners naturally use more drawing power as compared to square corners. When selecting the value of corner radius, we not only considered the design principles but we also were very careful to ensure developers who use our default controls are able to successfully deliver performant applications.

To simplify this section, think of performance issues mentioned here to be mainly about page load time as well as app launch time.

Key information to note:
- Rounded corners on a larger surface UI are less performant. Avoid drawing rounded corners on a full screen app UI. This is less of an issue if the UI is displayed briefly (e.g. ContentDialog).

# Customization options
The default corner radii values that we provide are not set in stone and there are a few ways the app-developer can easily modify the rounded amount. This can be done through two global resources, or through the 'CornerRadius' property directly on the control, depending on the level of customization granularity desired.

## Page or app-wide CornerRadius changes
The two main resources that control the corner radii of all the controls are:

- `OverlayCornerRadius`
- `ControlCornerRadius`

Changing these two resources' values at any scope will affect all controls within that scope accordingly.

This means if you want to change the roundness of all controls where roundness could be applied, you can define both resources at the app level with the new CornerRadius values like so:

``` xml
<Application.Resources>
  <Thickness x:Key="OverlayCornerRadius">4</Thickness>
  <Thickness x:Key="ControlCornerRadius">8</Thickness>
</Application.Resources>
```

Alternatively, if you want to change all controls' roundness within a particular scope, like at a page or container level, you can follow a similar pattern:

``` xml
<Grid x:Name="myGrid">
  <Grid.Resources>
    <Thickness x:Key="ControlCornerRadius">8</Thickness>
  </Grid.Resources>
</Grid>
```

**Can I define OverlayCornerRadius at a page or container level?**

The `OverlayCornerRadius` must be defined at the app level in order to take effect.

This is because our popups and flyouts are dynamic and created at the root element in the Visual Tree. Meaning any resources that they use should also be defined there. Otherwise they're out of scope.

## Per-control CornerRadius changes
The `CornerRadius` property on controls can also be modified directly if changing the roundness of only a select number of controls is desired.

|default | property modified |
|:-- |:-- |
|![DefaultCheckBox](images/rounded-corner/default-checkbox.png)| ![CustomCheckBox](images/rounded-corner/custom-checkbox.png)|
|`<CheckBox Content="Checkbox"/>` | `<CheckBox Content="Checkbox" CornerRadius="5"/> ` |

**It's important to note** that not all controls' corners will respond to their `CornerRadius` property being modified.

To ensure that the control whose corners you wish to round will indeed respond to their `CornerRadius` property the way you expect, first check that the `ControlCornerRadius` or `OverlayCornerRadius` global resources affect the control in question. If they do not, check that the control you wish to round has corners at all. Many of our controls do not render actual edges and therefore cannot make proper use of the `CornerRadius` property.

# Appendix
Here are relevant visual comp files:
- Raw files: https://github.com/microsoft/microsoft-ui-xaml-specs/tree/user/chigy/roundedcorner/active/RoundedCorner/ImageFiles
- In an easier to consume format thanks to community: https://github.com/mrlacey/microsoft-ui-xaml-specs/blob/RoundedCornerVisualizations/active/RoundedCorner/ImageFiles/index.md
