---
description: Respond to keystroke actions from hardware or software keyboards in your apps using both keyboard and class event handlers.
title: Keyboard events
ms.assetid: ac500772-d6ed-4a3a-825b-210a9c3c8f59
label: Keyboard events
template: detail.hbs
keywords: keyboard, gamepad, remote, accessibility, navigation, focus, text, input, user interactions, key up, key down
ms.date: 07/09/2021
ms.topic: article
pm-contact: chigy
design-contact: kimsea
dev-contact: niallm
doc-status: Published
ms.localizationpriority: medium
---
# Keyboard events

## Keyboard events and focus

The following keyboard events can occur for both hardware and touch keyboards.

| Event                                      | Description                    |
|--------------------------------------------|--------------------------------|
| [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) | Occurs when a key is pressed.  |
| [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup)     | Occurs when a key is released. |

> [!IMPORTANT]
> Some XAML controls handle input events internally. In these cases, it might appear that an input event doesn't occur because your event listener doesn't invoke the associated handler. Typically, this subset of keys is processed by the class handler to provide built in support of basic keyboard accessibility. For example, the [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) class overrides the [**OnKeyDown**](/uwp/api/windows.ui.xaml.controls.control.onkeydown) events for both the Space key and the Enter key (as well as [**OnPointerPressed**](/uwp/api/windows.ui.xaml.controls.control.onpointerpressed)) and routes them to the [**Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event of the control. When a key press is handled by the control class, the [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events are not raised.  
> This provides a built-in keyboard equivalent for invoking the button, similar to tapping it with a finger or clicking it with a mouse. Keys other than Space or Enter still fire [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events. For more info about how class-based handling of events works (specifically, the "Input event handlers in controls" section), see [Events and routed events overview](/windows/uwp/xaml-platform/events-and-routed-events-overview).


Controls in your UI generate keyboard events only when they have input focus. An individual control gains focus when the user clicks or taps directly on that control in the layout, or uses the Tab key to step into a tab sequence within the content area.

You can also call a control's [**Focus**](/uwp/api/windows.ui.xaml.controls.control.focus) method to force focus. This is necessary when you implement shortcut keys, because keyboard focus is not set by default when your UI loads. For more info, see the **Shortcut keys example** later in this topic.

For a control to receive input focus, it must be enabled, visible, and have [**IsTabStop**](/uwp/api/windows.ui.xaml.controls.control.istabstop) and [**HitTestVisible**](/uwp/api/windows.ui.xaml.uielement.ishittestvisible) property values of **true**. This is the default state for most controls. When a control has input focus, it can raise and respond to keyboard input events as described later in this topic. You can also respond to a control that is receiving or losing focus by handling the [**GotFocus**](/uwp/api/windows.ui.xaml.uielement.gotfocus) and [**LostFocus**](/uwp/api/windows.ui.xaml.uielement.lostfocus) events.

By default, the tab sequence of controls is the order in which they appear in the Extensible Application Markup Language (XAML). However, you can modify this order by using the [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) property. For more info, see [Implementing keyboard accessibility](/previous-versions/windows/apps/hh868161(v=win.10)).

## Keyboard event handlers

An input event handler implements a delegate that provides the following information:

- The sender of the event. The sender reports the object where the event handler is attached.
- Event data. For keyboard events, that data will be an instance of [**KeyRoutedEventArgs**](/uwp/api/Windows.UI.Xaml.Input.KeyRoutedEventArgs). The delegate for handlers is [**KeyEventHandler**](/uwp/api/windows.ui.xaml.input.keyeventhandler). The most relevant properties of **KeyRoutedEventArgs** for most handler scenarios are [**Key**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.key) and possibly [**KeyStatus**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.keystatus).
- [**OriginalSource**](/uwp/api/windows.ui.xaml.routedeventargs.originalsource). Because the keyboard events are routed events, the event data provides **OriginalSource**. If you deliberately allow events to bubble up through an object tree, **OriginalSource** is sometimes the object of concern rather than sender. However, that depends on your design. For more information about how you might use **OriginalSource** rather than sender, see the "Keyboard Routed Events" section of this topic, or [Events and routed events overview](/windows/uwp/xaml-platform/events-and-routed-events-overview).


### Attaching a keyboard event handler

You can attach keyboard event-handler functions for any object that includes the event as a member. This includes any [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) derived class. The following XAML example shows how to attach handlers for the [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) event for a [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid).

```xaml
<Grid KeyUp="Grid_KeyUp">
  ...
</Grid>
```

You can also attach an event handler in code. For more info, see [Events and routed events overview](/windows/uwp/xaml-platform/events-and-routed-events-overview).

### Defining a keyboard event handler

The following example shows the incomplete event handler definition for the [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) event handler that was attached in the preceding example.

```csharp
void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
{
    //handling code here
}
```

```vb
Private Sub Grid_KeyUp(ByVal sender As Object, ByVal e As KeyRoutedEventArgs)
    ' handling code here
End Sub
```

```c++
void MyProject::MainPage::Grid_KeyUp(
  Platform::Object^ sender,
  Windows::UI::Xaml::Input::KeyRoutedEventArgs^ e)
  {
      //handling code here
  }
```

### Using KeyRoutedEventArgs

All keyboard events use [**KeyRoutedEventArgs**](/uwp/api/Windows.UI.Xaml.Input.KeyRoutedEventArgs) for event data, and **KeyRoutedEventArgs** contains the following properties:

- [**Key**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.key)
- [**KeyStatus**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.keystatus)
- [**Handled**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.handled)
- [**OriginalSource**](/uwp/api/windows.ui.xaml.routedeventargs.originalsource) (inherited from [**RoutedEventArgs**](/uwp/api/Windows.UI.Xaml.RoutedEventArgs))

### Virtual keys

The [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) event is raised if a key is pressed. Likewise, [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) is raised if a key is released. Usually, you listen to the events to process a specific key value. To determine which key is pressed or released, check the [**Key**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.key) value in the event data. **Key** returns a [**VirtualKey**](/uwp/api/Windows.System.VirtualKey) value. The **VirtualKey** enumeration includes all the supported keys.

### Modifier keys

Modifier keys are keys such as Ctrl or Shift that users typically press in combination with other keys. Your app can use these combinations as custom keyboard shortcuts to invoke app commands.

> [!NOTE]
> For built-in keyboard shortcuts, see [Access keys](access-keys.md) and [Keyboard accelerators](keyboard-accelerators.md).

You can detect shortcut key combinations in the [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) event handlers. When a keyboard event occurs for a non-modifier key, you can then check whether a modifier key is in the pressed state.

Alternatively, the [**GetKeyState()**](/uwp/api/windows.ui.core.corewindow.getkeystate) function of the [**CoreWindow**](/uwp/api/windows.ui.core.corewindow) (obtained through [**CoreWindow.GetForCurrentThread()**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread)) can also be used to check modifier state when a non-modifier key is pressed.

The following examples implement this second method while also including stub code for the first implementation.

> [!NOTE]
> The Alt key is represented by the **VirtualKey.Menu** value.

### Shortcut keys example

The following example demonstrates how to implement a set of custom shortcut keys. In this example, users can control media playback using Play, Pause, and Stop buttons or Ctrl+P, Ctrl+A, and Ctrl+S keyboard shortcuts. The button XAML shows the shortcuts by using tooltips and [**AutomationProperties**](/uwp/api/Windows.UI.Xaml.Automation.AutomationProperties) properties in the button labels. This self-documentation is important to increase the usability and accessibility of your app. For more info, see [Keyboard accessibility](../accessibility/keyboard-accessibility.md).

Note also that the page sets input focus to itself when it is loaded. Without this step, no control has initial input focus, and the app does not raise input events until the user sets the input focus manually (for example, by tabbing to or clicking a control).

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

```c++
//showing implementations but not header definitions
void MainPage::OnNavigatedTo(NavigationEventArgs^ e)
{
    (void) e;    // Unused parameter
    this->Loaded+=ref new RoutedEventHandler(this,&amp;MainPage::ProgrammaticFocus);
}
void MainPage::ProgrammaticFocus(Object^ sender, RoutedEventArgs^ e) 
{
    this->Focus(Windows::UI::Xaml::FocusState::Programmatic);
}

void KeyboardSupport::MainPage::MediaButton_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
    FrameworkElement^ fe = safe_cast<FrameworkElement^>(sender);
    if (fe->Name == "PlayButton") {DemoMovie->Play();}
    if (fe->Name == "PauseButton") {DemoMovie->Pause();}
    if (fe->Name == "StopButton") {DemoMovie->Stop();}
}


bool KeyboardSupport::MainPage::IsCtrlKeyPressed()
{
    auto ctrlState = CoreWindow::GetForCurrentThread()->GetKeyState(VirtualKey::Control);
    return (ctrlState & CoreVirtualKeyStates::Down) == CoreVirtualKeyStates::Down;
}

void KeyboardSupport::MainPage::Grid_KeyDown(Platform::Object^ sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs^ e)
{
    if (e->Key == VirtualKey::Control) isCtrlKeyPressed = true;
}


void KeyboardSupport::MainPage::Grid_KeyUp(Platform::Object^ sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs^ e)
{
    if (IsCtrlKeyPressed()) 
    {
        if (e->Key==VirtualKey::P) { DemoMovie->Play(); }
        if (e->Key==VirtualKey::A) { DemoMovie->Pause(); }
        if (e->Key==VirtualKey::S) { DemoMovie->Stop(); }
    }
}
```

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    // Set the input focus to ensure that keyboard events are raised.
    this.Loaded += delegate { this.Focus(FocusState.Programmatic); };
}

private void MediaButton_Click(object sender, RoutedEventArgs e)
{
    switch ((sender as Button).Name)
    {
        case "PlayButton": DemoMovie.Play(); break;
        case "PauseButton": DemoMovie.Pause(); break;
        case "StopButton": DemoMovie.Stop(); break;
    }
}

private static bool IsCtrlKeyPressed()
{
    var ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
    return (ctrlState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
}

private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
{
    if (IsCtrlKeyPressed())
    {
        switch (e.Key)
        {
            case VirtualKey.P: DemoMovie.Play(); break;
            case VirtualKey.A: DemoMovie.Pause(); break;
            case VirtualKey.S: DemoMovie.Stop(); break;
        }
    }
}
```

```VisualBasic
Private isCtrlKeyPressed As Boolean
Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)

End Sub

Private Function IsCtrlKeyPressed As Boolean
    Dim ctrlState As CoreVirtualKeyStates = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
    Return (ctrlState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
End Function

Private Sub Grid_KeyDown(sender As Object, e As KeyRoutedEventArgs)
    If IsCtrlKeyPressed() Then
        Select Case e.Key
            Case Windows.System.VirtualKey.P
                DemoMovie.Play()
            Case Windows.System.VirtualKey.A
                DemoMovie.Pause()
            Case Windows.System.VirtualKey.S
                DemoMovie.Stop()
        End Select
    End If
End Sub

Private Sub MediaButton_Click(sender As Object, e As RoutedEventArgs)
    Dim fe As FrameworkElement = CType(sender, FrameworkElement)
    Select Case fe.Name
        Case "PlayButton"
            DemoMovie.Play()
        Case "PauseButton"
            DemoMovie.Pause()
        Case "StopButton"
            DemoMovie.Stop()
    End Select
End Sub
```

> [!NOTE]
> Setting [**AutomationProperties.AcceleratorKey**](/dotnet/api/system.windows.automation.automationproperties.acceleratorkey) or [**AutomationProperties.AccessKey**](/dotnet/api/system.windows.automation.automationproperties.accesskey) in XAML provides string information, which documents the shortcut key for invoking that particular action. The information is captured by Microsoft UI Automation clients such as Narrator, and is typically provided directly to the user.
>
> Setting **AutomationProperties.AcceleratorKey** or **AutomationProperties.AccessKey** does not have any action on its own. You will still need to attach handlers for [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) or [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events in order to actually implement the keyboard shortcut behavior in your app. Also, the underline text decoration for an access key is not provided automatically. You must explicitly underline the text for the specific key in your mnemonic as inline [**Underline**](/uwp/api/Windows.UI.Xaml.Documents.Underline) formatting if you wish to show underlined text in the UI.

 

## Keyboard routed events


Certain events are routed events, including [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup). Routed events use the bubbling routing strategy. The bubbling routing strategy means that an event originates from a child object and is then routed up to successive parent objects in the object tree. This presents another opportunity to handle the same event and interact with the same event data.

Consider the following XAML example, which handles [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events for a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) and two [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) objects. In this case, if you release a key while focus is held by either **Button** object, it raises the **KeyUp** event. The event is then bubbled up to the parent **Canvas**.

```xaml
<StackPanel KeyUp="StackPanel_KeyUp">
  <Button Name="ButtonA" Content="Button A"/>
  <Button Name="ButtonB" Content="Button B"/>
  <TextBlock Name="statusTextBlock"/>
</StackPanel>
```

The following example shows how to implement the [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) event handler for the corresponding XAML content in the preceding example.

```csharp
void StackPanel_KeyUp(object sender, KeyRoutedEventArgs e)
{
    statusTextBlock.Text = String.Format(
        "The key {0} was pressed while focus was on {1}",
        e.Key.ToString(), (e.OriginalSource as FrameworkElement).Name);
}
```

Notice the use of the [**OriginalSource**](/uwp/api/windows.ui.xaml.routedeventargs.originalsource) property in the preceding handler. Here, **OriginalSource** reports the object that raised the event. The object could not be the [**StackPanel**](/uwp/api/Windows.UI.Xaml.Controls.StackPanel) because the **StackPanel** is not a control and cannot have focus. Only one of the two buttons within the **StackPanel** could possibly have raised the event, but which one? You use **OriginalSource** to distinguish the actual event source object, if you are handling the event on a parent object.

### The Handled property in event data

Depending on your event handling strategy, you might want only one event handler to react to a bubbling event. For instance, if you have a specific [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) handler attached to one of the [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) controls, it would have the first opportunity to handle that event. In this case, you might not want the parent panel to also handle the event. For this scenario, you can use the [**Handled**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.handled) property in the event data.

The purpose of the [**Handled**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.handled) property in a routed event data class is to report that another handler you registered earlier on the event route has already acted. This influences the behavior of the routed event system. When you set **Handled** to **true** in an event handler, that event stops routing and is not sent to successive parent elements.

### AddHandler and already-handled keyboard events

You can use a special technique for attaching handlers that can act on events that you already marked as handled. This technique uses the [**AddHandler**](/uwp/api/windows.ui.xaml.uielement.addhandler) method to register a handler, rather than using XAML attributes or language-specific syntax for adding handlers, such as += in C\#.

A general limitation of this technique is that the **AddHandler** API takes a parameter of type [**RoutedEvent**](/uwp/api/Windows.UI.Xaml.RoutedEvent) identifying the routed event in question. Not all routed events provide a **RoutedEvent** identifier, and this consideration thus affects which routed events can still be handled in the [**Handled**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.handled) case. The [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events have routed event identifiers ([**KeyDownEvent**](/uwp/api/windows.ui.xaml.uielement.keydownevent) and [**KeyUpEvent**](/uwp/api/windows.ui.xaml.uielement.keyupevent)) on [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement). However, other events such as [**TextBox.TextChanged**](/uwp/api/windows.ui.xaml.controls.textbox.textchanged) do not have routed event identifiers and thus cannot be used with the **AddHandler** technique.

### Overriding keyboard events and behavior

You can override key events for specific controls (such as [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView)) to provide consistent focus navigation for various input devices, including keyboard and gamepad.

In the following example, we subclass the control and override the KeyDown behavior to move focus to the GridView content when any arrow key is pressed.

```csharp
  public class CustomGridView : GridView
  {
    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
      // Override arrow key behaviors.
      if (e.Key != Windows.System.VirtualKey.Left && e.Key !=
        Windows.System.VirtualKey.Right && e.Key !=
          Windows.System.VirtualKey.Down && e.Key !=
            Windows.System.VirtualKey.Up)
              base.OnKeyDown(e);
      else
        FocusManager.TryMoveFocus(FocusNavigationDirection.Down);
    }
  }
```

> [!NOTE]
> If using a GridView for layout only, consider using other controls such as [**ItemsControl**](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl) with [**ItemsWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.ItemsWrapGrid).

## Commanding

A small number of UI elements provide built-in support for commanding. Commanding uses input-related routed events in its underlying implementation. It enables processing of related UI input, such as a certain pointer action or a specific accelerator key, by invoking a single command handler.

If commanding is available for a UI element, consider using its commanding APIs instead of any discrete input events. For more info, see [**ButtonBase.Command**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.command).

You can also implement [**ICommand**](/uwp/api/Windows.UI.Xaml.Input.ICommand) to encapsulate command functionality that you invoke from ordinary event handlers. This enables you to use commanding even when there is no **Command** property available.

## Text input and controls

Certain controls react to keyboard events with their own handling. For instance, [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox) is a control that is designed to capture and then visually represent text that was entered by using the keyboard. It uses [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) and [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) in its own logic to capture keystrokes, then also raises its own [**TextChanged**](/uwp/api/windows.ui.xaml.controls.textbox.textchanged) event if the text actually changed.

You can still generally add handlers for [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) and [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) to a [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox), or any related control that is intended to process text input. However, as part of its intended design, a control might not respond to all key values that are directed to it through key events. Behavior is specific to each control.

As an example, [**ButtonBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ButtonBase) (the base class for [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button)) processes [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) so that it can check for the Spacebar or Enter key. **ButtonBase** considers **KeyUp** equivalent to a mouse left button down for purposes of raising a [**Click**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event. This processing of the event is accomplished when **ButtonBase** overrides the virtual method [**OnKeyUp**](/uwp/api/windows.ui.xaml.controls.control.onkeyup). In its implementation, it sets [**Handled**](/uwp/api/windows.ui.xaml.input.keyroutedeventargs.handled) to **true**. The result is that any parent of a button that is listening for a key event, in the case of a Spacebar, would not receive the already-handled event for its own handlers.

Another example is [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox). Some keys, such as the arrow keys, are not considered text by **TextBox** and are instead considered specific to the control UI behavior. The **TextBox** marks these event cases as handled.

Custom controls can implement their own similar override behavior for key events by overriding [**OnKeyDown**](/uwp/api/windows.ui.xaml.controls.control.onkeydown) / [**OnKeyUp**](/uwp/api/windows.ui.xaml.controls.control.onkeyup). If your custom control processes specific accelerator keys, or has control or focus behavior that is similar to the scenario described for [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox), you should place this logic in your own **OnKeyDown** / **OnKeyUp** overrides.

## The touch keyboard

Text input controls provide automatic support for the touch keyboard. When the user sets the input focus to a text control by using touch input, the touch keyboard appears automatically. When the input focus is not on a text control, the touch keyboard is hidden.

When the touch keyboard appears, it automatically repositions your UI to ensure that the focused element remains visible. This can cause other important areas of your UI to move off screen. However, you can disable the default behavior and make your own UI adjustments when the touch keyboard appears. For more info, see the [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard).

If you create a custom control that requires text input, but does not derive from a standard text input control, you can add touch keyboard support by implementing the correct UI Automation control patterns. For more info, see the [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard).

Key presses on the touch keyboard raise [**KeyDown**](/uwp/api/windows.ui.xaml.uielement.keydown) and [**KeyUp**](/uwp/api/windows.ui.xaml.uielement.keyup) events just like key presses on hardware keyboards. However, the touch keyboard will not raise input events for Ctrl+A, Ctrl+Z, Ctrl+X, Ctrl+C, and Ctrl+V, which are reserved for text manipulation in the input control.

You can make it much faster and easier for users to enter data in your app by setting the input scope of the text control to match the kind of data you expect the user to enter. The input scope provides a hint at the type of text input expected by the control so the system can provide a specialized touch keyboard layout for the input type. For example, if a text box is used only to enter a 4-digit PIN, set the [**InputScope**](/uwp/api/windows.ui.xaml.controls.textbox.inputscope) property to [**Number**](/uwp/api/Windows.UI.Xaml.Input.InputScopeNameValue). This tells the system to show the numeric keypad layout, which makes it easier for the user to enter the PIN. For more detail, see [Use input scope to change the touch keyboard](./use-input-scope-to-change-the-touch-keyboard.md).

## Related articles

### Developers

- [Keyboard interactions](keyboard-interactions.md)
- [Identify input devices](identify-input-devices.md)
- [Respond to the presence of the touch keyboard](respond-to-the-presence-of-the-touch-keyboard.md)

### Designers

- [Keyboard design guidelines](./keyboard-interactions.md)

### Samples

- [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard)
- [Basic input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicInput)
- [Low latency input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LowLatencyInput)
- [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals)

### Archive Samples

- [Input sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20XAML%20user%20input%20events%20sample)
- [Input: Device capabilities sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Input%20Device%20capabilities%20sample%20(Windows%208))
- [Input: Touch keyboard sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Input%20Touch%20keyboard%20sample%20(Windows%208))
- [Responding to the appearance of the on-screen keyboard sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Responding%20to%20the%20appearance%20of%20the%20on-screen%20keyboard%20sample)
- [XAML text editing sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BVB%5D-Windows%208%20app%20samples/VB/Windows%208%20app%20samples/XAML%20text%20editing%20sample%20(Windows%208))
