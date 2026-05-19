---
description: If your app does not provide good keyboard access, users who are blind or have mobility issues can have difficulty using your app or may not be able to use it at all.
ms.assetid: DDAE8C4B-7907-49FE-9645-F105F8DFAD8B
title: Keyboard accessibility
label: Keyboard accessibility
template: detail.hbs
ms.date: 03/17/2026
ms.topic: article
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Keyboard accessibility

Keyboard accessibility should be treated as a primary interaction model, not a secondary fallback. A robust keyboard experience supports users who have various disabilities and limitations (including vision, learning, dexterity/mobility, and language/communication disabilities). It also improves productivity for users who prefer keyboard-first interaction for speed and precision.

If keyboard access is incomplete or inconsistent, users can lose access to core app functionality even when pointer interaction appears fully implemented.

## Keyboard navigation among UI elements

To interact with a control using the keyboard, controls must be focusable and reachable through focus traversal. To receive focus (without using a pointer), the control must be accessible through tab navigation. By default, the tab order of controls is the same as the order in which they are added to a design surface, declared in XAML, or programmatically added to a container.

In many UIs, this default behavior is acceptable and aligns with reading flow. However, visual order and keyboard order can diverge depending on container layout and child positioning. This divergence should be intentional and tested.

Validate tab behavior explicitly. Grid, table, and similar layouts are common sources of mismatch between perceived reading order and focus order. Test both keyboard-only and touch interaction paths to ensure traversal remains efficient and predictable.

To align traversal with visual flow, you can restructure XAML or explicitly assign [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex). The following example uses a [**Grid**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Grid) with column-first tab sequencing.

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

In some scenarios, an element should be excluded from tab traversal. The standard approach is to set [**IsEnabled**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.isenabled) to **false**, which also disables interaction. Disabled controls are automatically removed from tab order.

If an element remains interactive through other mechanisms but should not be reached with Tab, set [**IsTabStop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.istabstop) to **false**.

Most focus-capable controls are included in tab order by default. A common exception is text-display controls such as [**RichTextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.RichTextBlock), which can support focus for text selection and clipboard operations but are typically not tab stops because they are not command-invokable controls. These controls can still be discovered by assistive technologies through automation semantics such as the [Text control pattern](/windows/desktop/WinAuto/uiauto-controlpatternsoverview).

Whether you use default traversal or explicit [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex), the following rules apply:

- If [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) is not set on an element, the default value is [Int32.MaxValue](/dotnet/api/system.int32.maxvalue), and order is based on declaration/insertion order.
- If [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) is set on an element:
  - Elements with [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) equal to 0 are added based on declaration/insertion order.
  - Elements with [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) greater than 0 are added in ascending **TabIndex** order.
  - Elements with [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) less than 0 are added before elements with zero values.

The following snippet shows mixed [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) settings (`B` uses [Int32.MaxValue](/dotnet/api/system.int32.maxvalue), or 2,147,483,647).

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

This produces the following tab order:

1. F
1. D
1. E
1. A
1. B
1. C

### Keyboard navigation between application panes with F6

An application pane is a prominent task region inside a window. In Microsoft Edge, for example, panes include the address bar, bookmark bar, tab strip, and content area. F6 is commonly used to move focus between these panes with child elements accessible using standard keyboard navigation.

While a compliant keyboard navigation model is the baseline, a usable keyboard navigation model also typically includes:

- Listening for **F6** to move between major UI regions.
- Providing **keyboard shortcuts** for high-frequency commands.
- Providing **access keys** for important controls.

See [Keyboard shortcuts](#keyboard-shortcuts) and [Access keys](../input/access-keys.md) for implementation guidance.

#### Optimize for F6

F6 significantly reduces traversal cost by letting users jump between major regions rather than tab through every child control.

For example, F6 in Microsoft Edge cycles between the address bar, bookmark bar, tab strip, and content. Because a page can contain many tab stops, this makes common navigation tasks much more efficient.

The F6 sequence can align with [landmarks or headings](landmarks-and-headings.md), but it does not need to match exactly. Use F6 for broad region-level movement and landmarks/headings for semantic structure within and across regions.

> [!IMPORTANT]
> You must implement F6 navigation explicitly in your app; it is not provided automatically.

Where possible, each F6 target region should have a clear accessible name, either from landmark semantics or by setting [**AutomationProperties.Name**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.nameproperty) on the region root.

**Shift+F6** should traverse the same cycle in reverse order.

## Keyboard navigation within a UI element

Composite controls should provide predictable inner navigation among child elements. A common pattern is to keep the composite root in tab order and manage active descendants internally, rather than exposing every child as a separate tab stop.

Many built-in controls already implement this behavior. For example, arrow-key traversal is available by default for [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ListView), [**GridView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.gridview), [**ListBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.ListBox), and [**FlipView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.FlipView).

## Keyboard alternatives to pointer actions and events for specific control elements

Any UI that can be activated by pointer should also be invokable by keyboard. Activation requires the element have focus (only classes that derive from [**Control**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Control) support focus and tab navigation).

For controls that can be invoked, implement keyboard event handlers for the Spacebar and Enter keys. This provides basic keyboard parity with pointer interactions.


If an element is not focus-capable by default, either use a focusable control type or implement a custom control with explicit focus behavior. In that case, set [**IsTabStop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.istabstop) to **true** and provide a visible focus indicator.

In many cases, composition is simpler and more robust than custom pointer-only behavior. For example, instead of handling pointer input directly on an [**Image**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Image), place it inside a [**Button**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) to inherit keyboard activation, focus handling, and automation behavior.

```xaml
<!--Don't do this.-->
<Image Source="sample.jpg" PointerPressed="Image_PointerPressed"/>

<!--Do this instead.-->
<Button Click="Button_Click"
        AutomationProperties.Name="Open profile photo">
  <Image Source="Assets/profile-photo.png"/>
</Button>
```

## Keyboard shortcuts

In addition to navigation and activation, implement a *shortcut* (a key combination that provides efficient access to app functionality) for important or frequently used commands with [keyboard accelerators](../input/keyboard-accelerators.md) and [access keys](../input/access-keys.md).

Two common types of *shortcut* are:

- *Accelerators*: invoke commands directly, with or without a corresponding visible control.
- *Access keys*: move focus to specific controls in your UI.


Always make shortcuts discoverable for users of assistive technology. Communicate them through tooltips, automation metadata, visible UI affordances, and help documentation.

To expose shortcut metadata to assistive technologies, use [**AutomationProperties.AccessKey**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.accesskeyproperty) for mnemonic shortcuts and [**AutomationProperties.AcceleratorKey**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.acceleratorkeyproperty) for command shortcuts. Because screen readers may present these similarly, document shortcuts in multiple channels.

The following example demonstrates how to document shortcut keys for media play, pause, and stop buttons.

```xaml
<Grid>

  <Grid.RowDefinitions>
    <RowDefinition Height="Auto" />
    <RowDefinition Height="Auto" />
  </Grid.RowDefinitions>

  <MediaPlayerElement x:Name="DemoPlayer"
    Width="500" Height="300" Margin="20"
    HorizontalAlignment="Center"
    AutoPlay="False"
    AreTransportControlsEnabled="True" />

  <StackPanel Grid.Row="1" Margin="10"
    Orientation="Horizontal" HorizontalAlignment="Center">

    <Button x:Name="PlayButton" Click="MediaButton_Click"
      ToolTipService.ToolTip="Shortcut key: Ctrl+P"
      AutomationProperties.AcceleratorKey="Ctrl+P"
      AutomationProperties.AccessKey="Alt+P">
      <Button.KeyboardAccelerators>
        <KeyboardAccelerator Modifiers="Control" Key="P"/>
      </Button.KeyboardAccelerators>
      <TextBlock>Play</TextBlock>
    </Button>

    <Button x:Name="PauseButton" Click="MediaButton_Click"
      ToolTipService.ToolTip="Shortcut key: Ctrl+A"
      AutomationProperties.AcceleratorKey="Ctrl+A"
      AutomationProperties.AccessKey="Alt+A">
      <Button.KeyboardAccelerators>
        <KeyboardAccelerator Modifiers="Control" Key="A"/>
      </Button.KeyboardAccelerators>
      <TextBlock>Pause</TextBlock>
    </Button>

    <Button x:Name="StopButton" Click="MediaButton_Click"
      ToolTipService.ToolTip="Shortcut key: Ctrl+S"
      AutomationProperties.AcceleratorKey="Ctrl+S"
      AutomationProperties.AccessKey="Alt+S">
      <Button.KeyboardAccelerators>
        <KeyboardAccelerator Modifiers="Control" Key="S"/>
      </Button.KeyboardAccelerators>
      <TextBlock>Stop</TextBlock>
    </Button>
  </StackPanel>
</Grid>
```

> [!IMPORTANT]
> Setting [**AutomationProperties.AcceleratorKey**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.acceleratorkeyproperty) or [**AutomationProperties.AccessKey**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.accesskeyproperty) does not implement keyboard behavior. These properties expose metadata to UI Automation so assistive technologies can announce the expected shortcuts.

Keyboard behavior must still be implemented in code. Use declarative [**KeyboardAccelerator**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.input.keyboardaccelerator) definitions when possible, and use [**KeyDown**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.keydown) or [**KeyUp**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.keyup) handlers where you need custom routing logic. Also note that access-key underline styling is not automatic. If you want visible mnemonic underlines, render them explicitly (for example, with [**Underline**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Documents.Underline)).

For brevity, the sample omits string resources such as "Ctrl+A". In production, localize shortcut text and validate mnemonic choices per locale, since key selection often depends on translated labels.

For additional guidance, see [Shortcut keys](/windows/win32/uxguide/inter-keyboard) in the [Windows User Experience Interaction Guidelines](/windows/apps/design/guidelines-overview).

### Implementing a key event handler

Key input uses *routed events*. Routed events can bubble from children to a parent container, which allows the parent to process shortcuts for multiple descendant elements. This event model is convenient for defining shortcut key actions for a control that contains several child elements, none of which can have focus or be part of the tab order.

For code examples that include modifier-key checks (for example, Ctrl), see [Keyboard interactions](../input/keyboard-interactions.md).

## Keyboard navigation for custom controls

For custom controls, use arrow keys when child elements are spatially related. In tree scenarios where expand/collapse and activation are separate interactions, map left and right arrows to expand/collapse behavior. For oriented controls, map directional keys to the control's visual orientation.

Custom key behavior is commonly implemented by overriding [**OnKeyDown**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.onkeydown) and [**OnKeyUp**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.control.onkeyup).

## An example of a visual state for a focus indicator

Any focusable custom control should expose a clear visual focus indicator. A common template pattern uses a [**Rectangle**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Shapes.Rectangle) overlay that starts hidden via [**Visibility**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.visibility) and appears when focus enters.

Built-in XAML controls already provide focus indicators. The exact appearance can vary with theme settings, including high contrast mode. If you retemplate controls, preserve equivalent focus visibility behavior.

The following example is adapted from the default [**Button**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Button) template.

```xaml
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

To toggle focus-indicator visibility, use [VisualStateManager](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.VisualStateManager) and `VisualStateManager.VisualStateGroups` on the template root.

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
          </Storyboard>
        </VisualState>
        <VisualState x:Name="Unfocused" />
        <VisualState x:Name="PointerFocused" />
      </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>

    <!--composition is here-->
  </Grid>
</ControlTemplate>
```

Only one state in this group explicitly modifies the focus visual. The other states can remain empty because transitions inside the same [**VisualStateGroup**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.VisualStateGroup) cancel prior state animations. Focus events such as [**GotFocus**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.gotfocus), combined with [**GoToState**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visualstatemanager.gotostate), typically drive these transitions.

## Keyboard accessibility and devices without a hardware keyboard

Some devices rely on a Soft Input Panel (SIP) instead of a hardware keyboard. Screen readers can detect that the user is scanning keys and announce a user's SIP key exploration, and many keyboard accessibility concepts still apply through gesture equivalents.

For example, even without a physical Tab key, Narrator supports gestures that map to Tab-like traversal. That means coherent tab order is still critical. Narrator also provides gesture equivalents for directional navigation in complex controls (see [Narrator keyboard commands and touch gestures](https://support.microsoft.com/en-us/windows/appendix-b-narrator-keyboard-commands-and-touch-gestures-8bdab3f4-b3e9-4554-7f28-8b15bd37410a)).

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see keyboard accessibility support in action](winui3gallery://item/AccessibilityKeyboard)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## Related topics

- [Accessibility overview](accessibility-overview.md)
- [Keyboard interactions](../input/keyboard-interactions.md)
- [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard) (legacy sample)
- [XAML accessibility sample](https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/Samples/ControlPages/Accessibility/AccessibilityKeyboardPage.xaml)
