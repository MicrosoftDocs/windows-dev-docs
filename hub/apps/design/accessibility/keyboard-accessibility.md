---
description: If your app does not provide good keyboard access, users who are blind or have mobility issues can have difficulty using your app or may not be able to use it at all.
ms.assetid: DDAE8C4B-7907-49FE-9645-F105F8DFAD8B
title: Keyboard accessibility
label: Keyboard accessibility
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Keyboard accessibility  



If your app does not provide good keyboard access, users who are blind or have mobility issues can have difficulty using your app or may not be able to use it at all.

<span id="keyboard_navigation_among_UI_elements"></span>
<span id="keyboard_navigation_among_ui_elements"></span>
<span id="KEYBOARD_NAVIGATION_AMONG_UI_ELEMENTS"></span>

## Keyboard navigation among UI elements  
To use the keyboard with a control, the control must have focus, and to receive focus (without using a pointer) the control must be accessible in a UI design via tab navigation. By default, the tab order of controls is the same as the order in which they are added to a design surface, listed in XAML, or programmatically added to a container.

In most cases, the default order based on how you defined controls in XAML is the best order, especially because that is the order in which the controls are read by screen readers. However, the default order does not necessarily correspond to the visual order. The actual display position might depend on the parent layout container and certain properties that you can set on the child elements to influence the layout. To be sure your app has a good tab order, test this behavior yourself. Especially if you have a grid metaphor or table metaphor for your layout, the order in which users might read versus the tab order could end up different. That's not always a problem in and of itself. But just make sure to test your app's functionality both as a touchable UI and as a keyboard-accessible UI and verify that your UI makes sense either way.

You can make the tab order match the visual order by adjusting the XAML. Or you can override the default tab order by setting the [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) property, as shown in the following example of a [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) layout that uses column-first tab navigation.

XAML
```xml
<!--Custom tab order.-->
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

You may want to exclude a control from the tab order. You typically do this only by making the control noninteractive, for example by setting its [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled) property to **false**. A disabled control is automatically excluded from the tab order. But occasionally you might want to exclude a control from the tab order even if it is not disabled. In this case, you can set the [**IsTabStop**](/uwp/api/windows.ui.xaml.controls.control.istabstop) property to **false**.

Any elements that can have focus are usually in the tab order by default. The exception to this is that certain text-display types such as [**RichTextBlock**](/uwp/api/Windows.UI.Xaml.Controls.RichTextBlock) can have focus so that they can be accessed by the clipboard for text selection; however, they're not in the tab order because it is not expected for static text elements to be in the tab order. They're not conventionally interactive (they can't be invoked, and don't require text input, but do support the [Text control pattern](/windows/desktop/WinAuto/uiauto-controlpatternsoverview) that supports finding and adjusting selection points in text). Text should not have the connotation that setting focus to it will enable some action that's possible. Text elements will still be detected by assistive technologies, and read aloud in screen readers, but that relies on techniques other than finding those elements in the practical tab order.

Whether you adjust [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) values or use the default order, these rules apply:

- If [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) is not set on an element, the default value used is [Int32.MaxValue](/dotnet/api/system.int32.maxvalue) and the tab order is based on declaration order in the XAML or child collections.
- If [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) is set on an element:
  - UI elements with [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) equal to 0 are added to the tab order based on declaration order in XAML or child collections.
  - UI elements with [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) greater than 0 are added to the tab order based on the **TabIndex** value.
  - UI elements with [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) less than 0 are added to the tab order and appear before any zero value. This potentially differs from HTML's handling of its **tabindex** attribute (and negative **tabindex** was not supported in older HTML specifications).

For example, the following snippet shows a collection of elements with varying TabIndex settings (B is assigned the value of [Int32.MaxValue](/dotnet/api/system.int32.maxvalue), or 2,147,483,647).

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

### Keyboard navigation between prominent sections of UI

While implementing tab and keyboard navigation alone can provide an accessibly-compliant UI, making an accessibly-*usable* UI often require a few more steps. Typically, that includes:

* Listening to **F6** to navigate between important sections of your UI
* Adding **keyboard shortcuts** for common actions in your UI
* Adding **access keys** to important controls in your UI

See [Keybord shortcuts](#keyboard-shortcuts) below and [Access keys](../input/access-keys.md) for more guidance about implementing shortcuts and access keys.

#### Cycling regions of UI with F6

F6 lets keyboard users navigate between sections of your UI without, potentially, needing to tab through dozens of controls.

For example, pressing F6 in Edge will cycle between the tab list, the navigation bar/app bar, and the webpage content. Since a webpage potentially has hundreds of tabbable controls, this makes it easy for keyboard users to reach the tab list and navigation bar without knowing specific shortcuts. This is also helpful in simpler UIs: the Windows taskbar cycles through Start, Search, the apps list, and parts of the systray when you press F6.

The F6 cycle will often correspond to accessible [landmarks or headings](landmarks-and-headings.md), though it doesn't need to match exactly. F6 should focus on large, distinct regions in your UI, whereas landmarks can be more granular. You might mark an app bar and its search box as landmarks, but only include the app bar itself in the F6 cycle.

If possible, though, regions in the F6 cycle should have an accessible name: either via a landmark or by manually adding [**AutomationProperties.Name**](/uwp/api/windows.ui.xaml.automation.automationproperties.nameproperty) to the "root" element of the region.

Typically, **Shift-F6** should cycle in the opposite direction.

## Keyboard navigation within a UI element

For composite elements, it is important to ensure proper inner navigation among the contained elements. A composite element can manage its current active child to reduce the overhead of having all child elements able to have focus. Such a composite element is included in the tab order, and it handles keyboard navigation events itself. Many of the composite controls already have some inner navigation logic built into the into control's event handling. For example, arrow-key traversal of items is enabled by default on the [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView), [**GridView**](/uwp/api/windows.ui.xaml.controls.gridview), [**ListBox**](/uwp/api/Windows.UI.Xaml.Controls.ListBox) and [**FlipView**](/uwp/api/Windows.UI.Xaml.Controls.FlipView) controls.

## Keyboard alternatives to pointer actions and events for specific control elements

Ensure that UI elements that can be clicked can also be invoked by using the keyboard. To use the keyboard with a UI element, the element must have focus. Only classes that derive from [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control) support focus and tab navigation.

For UI elements that can be invoked, implement keyboard event handlers for the Spacebar and Enter keys. This makes the basic keyboard accessibility support complete and enables users to accomplish basic app scenarios by using only the keyboard; that is, users can reach all interactive UI elements and activate the default functionality.

In cases where an element that you want to use in the UI cannot have focus, you could create your own custom control. You must set the [**IsTabStop**](/uwp/api/windows.ui.xaml.controls.control.istabstop) property to **true** to enable focus and you must provide a visual indication of the focused state by creating a visual state that decorates the UI with a focus indicator. However, it is often easier to use control composition so that the support for tab stops, focus, and Microsoft UI Automation peers and patterns are handled by the control within which you choose to compose your content.

For example, instead of handling a pointer-pressed event on an [**Image**](/uwp/api/Windows.UI.Xaml.Controls.Image), you could wrap that element in a [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) to get pointer, keyboard, and focus support.

XAML
```xml
<!--Don't do this.-->
<Image Source="sample.jpg" PointerPressed="Image_PointerPressed"/>

<!--Do this instead.-->
<Button Click="Button_Click"><Image Source="sample.jpg"/></Button>
```

<span id="keyboard_shortcuts"></span>
<span id="KEYBOARD_SHORTCUTS"></span>

## Keyboard shortcuts  
In addition to implementing keyboard navigation and activation for your app, it is a good practice to implement shortcuts for your app's functionality. Tab navigation provides a good, basic level of keyboard support, but with complex forms you may want to add support for shortcut keys as well. This can make your application more efficient to use, even for people who use both a keyboard and pointing devices.

A *shortcut* is a keyboard combination that enhances productivity by providing an efficient way for the user to access app functionality. There are two kinds of shortcut:

* An *access key* is a shortcut to a piece of UI in your app. Access keys consist of the Alt key plus a letter key.
* An *accelerator key* is a shortcut to an app command. Your app may or may not have UI that corresponds exactly to the command. Accelerator keys consist of the Ctrl key plus a letter key.

It is imperative that you provide an easy way for users who rely on screen readers and other assistive technology to discover your app's shortcut keys. Communicate shortcut keys by using tooltips, accessible names, accessible descriptions, or some other form of on-screen communication. At a minimum, shortcut keys should be well documented in your app's Help content.

You can document access keys through screen readers by setting the [**AutomationProperties.AccessKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.accesskeyproperty) attached property to a string that describes the shortcut key. There is also an [**AutomationProperties.AcceleratorKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.acceleratorkeyproperty) attached property for documenting non-mnemonic shortcut keys, although screen readers generally treat both properties the same way. Try to document shortcut keys in multiple ways, using tooltips, automation properties, and written Help documentation.

The following example demonstrates how to document shortcut keys for media play, pause, and stop buttons.

XAML
```xml
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
> Setting the [**AutomationProperties.AcceleratorKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.acceleratorkeyproperty) or [**AutomationProperties.AccessKey**](/uwp/api/windows.ui.xaml.automation.automationproperties.accesskeyproperty) doesn't enable keyboard functionality. It only reports to the UI Automation framework what keys should be used, so that such information can be passed on to users via assistive technologies. The implementation for key handling still needs to be done in code, not XAML. You will still need to attach handlers for [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) or [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events on the relevant control in order to actually implement the keyboard shortcut behavior in your app. Also, the underline text decoration for an access key is not provided automatically. You must explicitly underline the text for the specific key in your mnemonic as inline [**Underline**](/uwp/api/Windows.UI.Xaml.Documents.Underline) formatting if you wish to show underlined text in the UI.

For simplicity, the preceding example omits the use of resources for strings such as "Ctrl+A". However, you must also consider shortcut keys during localization. Localizing shortcut keys is relevant because the choice of key to use as the shortcut key typically depends on the visible text label for the element.

For more guidance about implementing shortcut keys, see [Shortcut keys](/windows/win32/uxguide/inter-keyboard) in the Windows User Experience Interaction Guidelines.

<span id="Implementing_a_key_event_handler"></span>
<span id="implementing_a_key_event_handler"></span>
<span id="IMPLEMENTING_A_KEY_EVENT_HANDLER"></span>

### Implementing a key event handler  
Input events such as the key events use an event concept called *routed events*. A routed event can bubble up through the child elements of a composited control, such that a common control parent can handle events for multiple child elements. This event model is convenient for defining shortcut key actions for a control that contains several composite parts that by design cannot have focus or be part of the tab order.

For example code that shows how to write a key event handler that includes checking for modifiers such as the Ctrl key, see [Keyboard interactions](../input/keyboard-interactions.md).

<span id="Keyboard_navigation_for_custom_controls"></span>
<span id="keyboard_navigation_for_custom_controls"></span>
<span id="KEYBOARD_NAVIGATION_FOR_CUSTOM_CONTROLS"></span>

## Keyboard navigation for custom controls  
We recommend the use of arrow keys as keyboard shortcuts for navigating among child elements, in cases where the child elements have a spacial relationship to each other. If tree-view nodes have separate sub-elements for handling expand-collapse and node activation, use the left and right arrow keys to provide keyboard expand-collapse functionality. If you have an oriented control that supports directional traversal within the control content, use the appropriate arrow keys.

Generally you implement custom key handling for custom controls by including an override of [**OnKeyDown**](/uwp/api/windows.ui.xaml.controls.control.onkeydown) and [**OnKeyUp**](/uwp/api/windows.ui.xaml.controls.control.onkeyup) methods as part of the class logic.

<span id="An_example_of_a_visual_state_for_a_focus_indicator"></span>
<span id="an_example_of_a_visual_state_for_a_focus_indicator"></span>
<span id="AN_EXAMPLE_OF_A_VISUAL_STATE_FOR_A_FOCUS_INDICATOR"></span>

## An example of a visual state for a focus indicator  
We mentioned earlier that any custom control that enables the user to focus it should have a visual focus indicator. Usually that focus indicator is as simple as drawing a rectangle shape immediately around the control's normal bounding rectangle. The [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) for visual focus is a peer element to the rest of the control's composition in a control template, but is initially set with a [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) value of **Collapsed** because the control isn't focused yet. Then, when the control does get focus, a visual state is invoked that specifically sets the **Visibility** of the focus visual to **Visible**. Once focus is moved elsewhere, another visual state is called, and the **Visibility** becomes **Collapsed**.

All of the default XAML controls will display an appropriate visual focus indicator when focused (if they can be focused). There are also potentially different looks depending on the user's selected theme (particularly if the user is using a high contrast mode.) If you're using the XAML controls in your UI and not replacing the control templates, you don't need to do anything extra to get visual focus indicators on controls that behave and display correctly. But if you're intending to retemplate a control, or if you're curious about how XAML controls provide their visual focus indicators, the remainder of this section explains how this is done in XAML and in the control logic.

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

XAML
```xml
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

Note how only one of the named states adjusts [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) directly whereas the others are seemingly empty. The way that visual states work is that as soon as the control uses another state from the same [**VisualStateGroup**](/uwp/api/Windows.UI.Xaml.VisualStateGroup), any animations applied by the previous state are immediately canceled. Because the default **Visibility** from composition is **Collapsed**, this means the rectangle will not appear. The control logic controls this by listening for focus events like [**GotFocus**](/uwp/api/windows.ui.xaml.uielement.gotfocus) and changing the states with [**GoToState**](/uwp/api/windows.ui.xaml.visualstatemanager.gotostate). Often this is already handled for you if you are using a default control or customizing based on a control that already has that behavior.

<span id="Keyboard_accessibility_and_Windows_Phone"></span>
<span id="keyboard_accessibility_and_windows_phone"></span>
<span id="KEYBOARD_ACCESSIBILITY_AND_WINDOWS_PHONE"></span>

## Keyboard accessibility and Windows Phone
A Windows Phone device typically doesn't have a dedicated, hardware keyboard. However, a Soft Input Panel (SIP) can support several keyboard accessibility scenarios. Screen readers can read text input from the **Text** SIP, including announcing deletions. Users can discover where their fingers are because the screen reader can detect that the user is scanning keys, and it reads the scanned key name aloud. Also, some of the keyboard-oriented accessibility concepts can be mapped to related assistive technology behaviors that don't use a keyboard at all. For example, even though a SIP won't include a Tab key, Narrator supports a touch gesture that's the equivalent of pressing the Tab key, so having a useful tab order through the controls in a UI is still an important accessibility principle. Arrow keys as used for navigating the parts within complex controls are also supported through Narrator touch gestures. Once focus has reached a control that's not for text input, Narrator supports a gesture that invokes that control's action.

Keyboard shortcuts aren't typically relevant for Windows Phone apps, because a SIP won't include Control or Alt keys.

<span id="related_topics"></span>

## Examples

> [!TIP]
> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see keyboard accessibility support in action](winui3gallery://item/AccessibilityKeyboard).

> The **WinUI 3 Gallery** app includes interactive examples of most WinUI 3 controls, features, and functionality. Get the app from the [Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) or get the source code on [GitHub](https://github.com/microsoft/WinUI-Gallery)

## Related topics

* [Accessibility](accessibility.md)
* [Keyboard interactions](../input/keyboard-interactions.md)
* [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard)
* [XAML accessibility sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/XAML%20accessibility%20sample)
