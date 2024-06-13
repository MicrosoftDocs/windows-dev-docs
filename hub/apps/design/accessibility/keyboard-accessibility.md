---
description: If your app does not provide good keyboard access, users who are blind or have mobility issues can have difficulty using your app or may not be able to use it at all.
ms.assetid: DDAE8C4B-7907-49FE-9645-F105F8DFAD8B
title: Keyboard accessibility
label: Keyboard accessibility
template: detail.hbs
ms.date: 06/07/2024
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Keyboard accessibility

Building keyboard accessibility (for traditional, modified, or keyboard emulation hardware) into your app, provides users who are blind, have low vision, motor disabilities, or have little or no use of their hands, the ability to navigate through and use the full functionality of your app. In addition, users without disabilities might choose the keyboard for navigation due to preference or efficiency.

If your app does not provide good keyboard access, users who are blind or have mobility issues might have difficulty using your app.

## Keyboard navigation among UI elements

To interact with a control using the keyboard, the control must have focus. To receive focus (without using a pointer), the control must be accessible through tab navigation. By default, the tab order of controls is the same as the order in which they are added to a design surface, declared in XAML, or programmatically added to a container.

Typically, the default tab order is based on how controls are defined in XAML, particularly as that is the order in which the controls are traversed by screen readers. However, the default order does not necessarily correspond to the visual order. The actual display position might depend on the parent layout container and various properties of child elements that can affect the layout.

To ensure your app has an optimal tab order, test the behavior yourself. If you use a grid or table for your layout, the order in which users might read the screen versus the tab order could be very different. This isn't always a problem, but just make sure to test your app's functionality through both touch and keyboard to verify that your UI is optimized for both input methods.

You can make the tab order match the visual order by either adjusting the XAML or overriding the default tab order. THe following example shows how to use the [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) property with a [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) layout that uses column-first tab navigation.

```xaml
<Grid>
  <Grid.RowDefinitions>...</Grid.RowDefinitions>
  <Grid.ColumnDefinitions>...</Grid.ColumnDefinitions>

  <TextBlock Grid.Column="1" HorizontalAlignment="Center">Groom</TextBlock>
  <TextBlock Grid.Column="2" HorizontalAlignment="Center">Bride</TextBlock>

  <TextBlock Grid.Row="1">First name</TextBlock>
  <TextBox x:Name="GroomFirstName" Grid.Row="1" Grid.Column="1" TabIndex="1"/>
  <TextBox x:Name="BrideFirstName" Grid.Row="1" Grid.Column="2" TabIndex="3"/>

  <TextBlock Grid.Row="2">Last name</TextBlock>
  <TextBox x:Name="GroomLastName" Grid.Row="2" Grid.Column="1" TabIndex="2"/>
  <TextBox x:Name="BrideLastName" Grid.Row="2" Grid.Column="2" TabIndex="4"/>
</Grid>
```

In some cases, you might want to exclude a specific control from the tab order. This is typically accomplished by making the control noninteractive by setting its [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled) property to **false**. A disabled control is automatically excluded from the tab order.

If you want to exclude an interactive control from the tab order, you can set the [**IsTabStop**](/uwp/api/windows.ui.xaml.controls.control.istabstop) property to **false**.

By default, UI elements that support focus are typically included in the tab order. Some exceptions to this include certain text-display types (such as [**RichTextBlock**](/uwp/api/Windows.UI.Xaml.Controls.RichTextBlock)) that support focus for text selection and clipboard access but are not in the tab order because they are static text elements. These controls are not conventionally interactive (they can't be invoked, and don't require text input, but do support the [Text control pattern](/windows/desktop/WinAuto/uiauto-controlpatternsoverview) that supports finding and adjusting selection points in text). Text controls will still be detected by assistive technologies, and read aloud in screen readers, but that relies on techniques other than tab order.

Whether you adjust [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) values or use the default order, these rules apply:

- If [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) is not set on an element, the default value is [Int32.MaxValue](/dotnet/api/system.int32.maxvalue) and the tab order is based on declaration order in the XAML or child collections.
- If [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) is set on an element:
  - UI elements with [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) equal to 0 are added to the tab order based on declaration order in XAML or child collections.
  - UI elements with [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) greater than 0 are added to the tab order based on the **TabIndex** value.
  - UI elements with [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) less than 0 are added to the tab order and appear before any zero value.

The following code nippet shows a collection of elements with various [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) settings (`B` is assigned the value of [Int32.MaxValue](/dotnet/api/system.int32.maxvalue), or 2,147,483,647).

```xaml
<StackPanel Background="#333">
  <StackPanel Background="#FF33FF">
    <Button>A</Button>
    <Button TabIndex="2147483647">B</Button>
    <Button>C</Button>
  </StackPanel>
  <StackPanel Background="#33FFFF">
    <Button TabIndex="1">D</Button>
    <Button TabIndex="1">E</Button>
    <Button TabIndex="0">F</Button>
  </StackPanel>
</StackPanel>
```

This results in the following tab order:

1. F
1. D
1. E
1. A
1. B
1. C

### Keyboard navigation between application panes with F6

An application pane is a logical area of prominent, related UI within an application window (for example, Microsoft Edge panes include the address bar, the bookmark bar, the tab bar, and the content panel). The F6 key can be used to navigate between these panes, where groups of child elements can then be accessed using standard keyboard navigation.

While keyboard navigation can provide an accessibly-compliant UI, making an accessibly-*usable* UI often require a few more steps. Typically, this includes:

- Listening to **F6** to navigate between important sections of your UI.
- Adding **keyboard shortcuts** for common actions in your UI.
- Adding **access keys** to important controls in your UI.

See [Keybord shortcuts](#keyboard-shortcuts) below and [Access keys](../input/access-keys.md) for more guidance about implementing shortcuts and access keys.

#### Optimize for F6

F6 lets keyboard users efficiently navigate between panes of UI without tabbing through potentially hundreds of controls.

For example, F6 in Microsoft Edge cycles between the address bar, the bookmark bar, the tab bar, and the content panel. As a web page can potentially have hundreds of tabbable controls, F6 can make it easier for keyboard users to reach the tab bar and address bar without using application-specific shortcuts.

The F6 tab cycle can also loosely correspond to [landmarks or headings](landmarks-and-headings.md) in content, though it doesn't need to match exactly. F6 should focus on large, distinct regions in your UI, whereas landmarks can be more granular. For example, you might mark an app bar and its search box as landmarks, but only include the app bar itself in the F6 cycle.

> [!IMPORTANT]
> You must implement F6 navigation in your app as it is not supported natively.

Where possible, regions in the F6 cycle should have an accessible name: either through a landmark or by manually adding an [**AutomationProperties.Name**](/uwp/api/windows.ui.xaml.automation.automationproperties.nameproperty) to the "root" element of the region.

**Shift-F6** should cycle in the opposite direction.

## Keyboard navigation within a UI element

For composite controls, it is important to ensure proper inner navigation among the contained elements. A composite control can manage the currently active child element to reduce the overhead of having all child elements support focus. The composite control is included in the tab order and handles keyboard navigation events itself. Many composite controls already have some inner navigation logic built into their event handling. For example, arrow-key traversal of items is enabled by default on the [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView), [**GridView**](/uwp/api/windows.ui.xaml.controls.gridview), [**ListBox**](/uwp/api/Windows.UI.Xaml.Controls.ListBox) and [**FlipView**](/uwp/api/Windows.UI.Xaml.Controls.FlipView) controls.

## Keyboard alternatives to pointer actions and events for specific control elements

UI elements that can be clicked should also be invokable through the keyboard. To use the keyboard with a UI element, the element must have focus (only classes that derive from [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control) support focus and tab navigation).

For UI elements that can be invoked, implement keyboard event handlers for the Spacebar and Enter keys. This ensures basic keyboard accessibility support and lets users reach all interactive UI elements and activate functionality by using the keyboard only.

Where an element does not support focus, you can create your own custom control. In this case, to enable focus, you must set the [**IsTabStop**](/uwp/api/windows.ui.xaml.controls.control.istabstop) property to **true** and you must provide a visual indication of the focused visual state with a focus indicator.

However, it can be easier to use control composition so that the support for tab stops, focus, and Microsoft UI Automation peers and patterns are handled by the control within which you choose to compose your content. For example, instead of handling a pointer-pressed event on an [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image), wrap that element in a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) to get pointer, keyboard, and focus support.

```xaml
<!--Don't do this.-->
<Image Source="sample.jpg" PointerPressed="Image_PointerPressed"/>

<!--Do this instead.-->
<Button Click="Button_Click"><Image Source="sample.jpg"/></Button>
```

## Keyboard shortcuts

In addition to implementing keyboard navigation and activation, it is also good practice to implement keyboard shortcuts such as [keyboard accelerators](../input/keyboard-accelerators.md) and [access keys](../input/access-keys.md) for important or frequently used functionality.

A *shortcut* is a keyboard combination that provides an efficient way for the user to access app functionality. There are two kinds of shortcut:

- *Accelerators* are shortcuts that invoke an app command. Your app may or may not provide specific UI that corresponds to the command. Accelerators typically consist of the Ctrl key plus a letter key.
- *Access keys* are shortcuts that set focus to specific UI in your application. Access keys typicaly consist of the Alt key plus a letter key.

Always provide an easy way for users who rely on screen readers and other assistive technology to discover your app's shortcut keys. Communicate shortcut keys by using tooltips, accessible names, accessible descriptions, or some other form of on-screen communication. At a minimum, shortcut keys should be well documented in your app's Help content.

You can document access keys through screen readers by setting the [**AutomationProperties.AccessKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.accesskeyproperty) attached property to a string that describes the shortcut key. There is also an [**AutomationProperties.AcceleratorKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.acceleratorkeyproperty) attached property for documenting non-mnemonic shortcut keys, although screen readers generally treat both properties the same way. Try to document shortcut keys in multiple ways, using tooltips, automation properties, and written Help documentation.

The following example demonstrates how to document shortcut keys for media play, pause, and stop buttons.

```xaml
<Grid KeyDown="Grid_KeyDown">

  <Grid.RowDefinitions>
    <RowDefinition Height="Auto" />
    <RowDefinition Height="Auto" />
  </Grid.RowDefinitions>

  <MediaElement x:Name="DemoMovie" Source="xbox.wmv"
    Width="500" Height="500" Margin="20" HorizontalAlignment="Center" />

  <StackPanel Grid.Row="1" Margin="10"
    Orientation="Horizontal" HorizontalAlignment="Center">

    <Button x:Name="PlayButton" Click="MediaButton_Click"
      ToolTipService.ToolTip="Shortcut key: Ctrl+P"
      AutomationProperties.AcceleratorKey="Control P">
      <TextBlock>Play</TextBlock>
    </Button>

    <Button x:Name="PauseButton" Click="MediaButton_Click"
      ToolTipService.ToolTip="Shortcut key: Ctrl+A"
      AutomationProperties.AcceleratorKey="Control A">
      <TextBlock>Pause</TextBlock>
    </Button>

    <Button x:Name="StopButton" Click="MediaButton_Click"
      ToolTipService.ToolTip="Shortcut key: Ctrl+S"
      AutomationProperties.AcceleratorKey="Control S">
      <TextBlock>Stop</TextBlock>
    </Button>
  </StackPanel>
</Grid>
```

> [!IMPORTANT]
> Setting the [**AutomationProperties.AcceleratorKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.acceleratorkeyproperty) or [**AutomationProperties.AccessKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.accesskeyproperty) doesn't enable keyboard functionality. This only indicates what keys should be used to the UI Automation framework and then can be passed on to users via assistive technologies.

Key handling is implemented in code-behind, not XAML. You still need to attach handlers for [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) or [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events on the relevant control in order to actually implement the keyboard shortcut behavior in your app. Also, the underline text decoration for an access key is not provided automatically. You must explicitly underline the text for the specific key in your mnemonic as inline [**Underline**](/uwp/api/Windows.UI.Xaml.Documents.Underline) formatting if you wish to show underlined text in the UI.

For simplicity, the preceding example omits the use of resources for strings such as "Ctrl+A". However, you must also consider shortcut keys during localization. Localizing shortcut keys is relevant because the choice of key to use as the shortcut key typically depends on the visible text label for the element.

For more guidance about implementing shortcut keys, see [Shortcut keys](/windows/win32/uxguide/inter-keyboard) in the Windows User Experience Interaction Guidelines.

### Implementing a key event handler

Input events (such as the key events) use an event concept called *routed events*. A routed event can bubble up through the child elements of a parent composite control, such that the parent control can handle events for multiple child elements. This event model is convenient for defining shortcut key actions for a control that contains several child elememts, none of which can have focus or be part of the tab order.

For example code that shows how to write a key event handler that includes checking for modifiers such as the Ctrl key, see [Keyboard interactions](../input/keyboard-interactions.md).

## Keyboard navigation for custom controls

We recommend using the arrow keys as keyboard shortcuts for navigating among child elements in cases where the child elements have a spacial relationship to each other. If tree-view nodes have separate sub-elements for handling expand-collapse and node activation, use the left and right arrow keys to provide keyboard expand-collapse functionality. If you have an oriented control that supports directional traversal within the control content, use the appropriate arrow keys.

Generally you implement custom key handling for custom controls by including an override of the [**OnKeyDown**](/uwp/api/windows.ui.xaml.controls.control.onkeydown) and [**OnKeyUp**](/uwp/api/windows.ui.xaml.controls.control.onkeyup) methods as part of the class logic.

## An example of a visual state for a focus indicator

As mentioned earlier, any custom control that supports focus should have a visual focus indicator. Typically, that focus indicator is just a rectangle outlining the bounding rectangle of the control. The [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) for visual focus is a peer element to the rest of the control's composition in a control template, but is initially set with a [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) value of **Collapsed** because the control isn't focused yet. When the control does get focus, a visual state is invoked that specifically sets the **Visibility** of the focus visual to **Visible**. Once focus is moved elsewhere, another visual state is called, and the **Visibility** becomes **Collapsed**.

All focusable XAML controls display an appropriate visual focus indicator when focused. The user's selected them can also affect the indicator appearance (particularly if the user is using a high contrast mode). If you're using the XAML controls in your UI (and are not replacing the control templates), you don't need to do anything extra to get default visual focus indicators. However, if you intend to retemplate a control, or if you're curious about how XAML controls provide their visual focus indicators, the remainder of this section explains how this is done in XAML and the control logic.

Here's some example XAML that comes from the default XAML template for a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button).

XAML
```xml
<ControlTemplate TargetType="Button">
...
    <Rectangle
      x:Name="FocusVisualWhite"
      IsHitTestVisible="False"
      Stroke="{ThemeResource FocusVisualWhiteStrokeThemeBrush}"
      StrokeEndLineCap="Square"
      StrokeDashArray="1,1"
      Opacity="0"
      StrokeDashOffset="1.5"/>
    <Rectangle
      x:Name="FocusVisualBlack"
      IsHitTestVisible="False"
      Stroke="{ThemeResource FocusVisualBlackStrokeThemeBrush}"
      StrokeEndLineCap="Square"
      StrokeDashArray="1,1"
      Opacity="0"
      StrokeDashOffset="0.5"/>
...
</ControlTemplate>
```

So far this is just the composition. To control the focus indicator's visibility, you define visual states that toggle the [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) property. This is done using the [VisualStateManager](/uwp/api/Windows.UI.Xaml.VisualStateManager) and the VisualStateManager.VisualStateGroups attached property, as applied to the root element that defines the composition.

```xaml
<ControlTemplate TargetType="Button">
  <Grid>
    <VisualStateManager.VisualStateGroups>
       <!--other visual state groups here-->
       <VisualStateGroup x:Name="FocusStates">
         <VisualState x:Name="Focused">
           <Storyboard>
             <DoubleAnimation
               Storyboard.TargetName="FocusVisualWhite"
               Storyboard.TargetProperty="Opacity"
               To="1" Duration="0"/>
             <DoubleAnimation
               Storyboard.TargetName="FocusVisualBlack"
               Storyboard.TargetProperty="Opacity"
               To="1" Duration="0"/>
         </VisualState>
         <VisualState x:Name="Unfocused" />
         <VisualState x:Name="PointerFocused" />
       </VisualStateGroup>
     <VisualStateManager.VisualStateGroups>
<!--composition is here-->
   </Grid>
</ControlTemplate>
```

Note how only one of the named states adjusts [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) directly whereas the others are apparently empty. With visual states, as soon as the control uses another state from the same [**VisualStateGroup**](/uwp/api/Windows.UI.Xaml.VisualStateGroup), any animations applied by the previous state are immediately canceled. Because the default **Visibility** from composition is **Collapsed**, the rectangle will not appear. The control logic controls this by listening for focus events like [**GotFocus**](/uwp/api/windows.ui.xaml.uielement.gotfocus) and changing the states with [**GoToState**](/uwp/api/windows.ui.xaml.visualstatemanager.gotostate). Often this is already handled for you if you are using a default control or customizing based on a control that already has that behavior.

## Keyboard accessibility and devices without a hardware keyboard

Some devices don't have a dedicated, hardware keyboard and rely on a Soft Input Panel (SIP) instead. Screen readers can read text input from the **Text** SIP and users can discover where their fingers are because the screen reader can detect that the user is scanning keys, and reads the scanned key name aloud. Also, some of the keyboard-oriented accessibility concepts can be mapped to related assistive technology behaviors that don't use a keyboard at all. For example, even though a SIP won't include a Tab key, Narrator supports a touch gesture that's the equivalent of pressing the Tab key, so having a useful tab order through the controls in a UI is still crtical for accessibility. Narrator also supports many other touch gestures, including arrow keys for navigating within complex controls (see [Narrator keyboard commands and touch gestures](https://support.microsoft.com/en-us/windows/appendix-b-narrator-keyboard-commands-and-touch-gestures-8bdab3f4-b3e9-4554-7f28-8b15bd37410a)).

## Examples

> [!TIP]
> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see keyboard accessibility support in action](winui3gallery://item/AccessibilityKeyboard).

> The **WinUI 3 Gallery** app includes interactive examples of most WinUI 3 controls, features, and functionality. Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery)

## Related topics

- [Accessibility](accessibility.md)
- [Keyboard interactions](../input/keyboard-interactions.md)
- [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard)
- [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
