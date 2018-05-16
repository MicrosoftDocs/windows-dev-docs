---
author: Karl-Bridge-Microsoft
Description: Respond to keystroke actions from hardware or software keyboards in your apps using both keyboard and class event handlers.
title: Keyboard events
ms.assetid: ac500772-d6ed-4a3a-825b-210a9c3c8f59
label: Keyboard events
template: detail.hbs
keywords: keyboard, gamepad, remote, accessibility, navigation, focus, text, input, user interactions, key up, key down
ms.author: kbridge
ms.date: 03/29/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
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
| [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) | Occurs when a key is pressed.  |
| [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942)     | Occurs when a key is released. |

> [!IMPORTANT]
> Some Windows Runtime controls handle input events internally. In these cases, it might appear that an input event doesn't occur because your event listener doesn't invoke the associated handler. Typically, this subset of keys is processed by the class handler to provide built in support of basic keyboard accessibility. For example, the [**Button**](https://msdn.microsoft.com/library/windows/apps/br209265) class overrides the [**OnKeyDown**](https://msdn.microsoft.com/library/windows/apps/hh967982) events for both the Space key and the Enter key (as well as [**OnPointerPressed**](https://msdn.microsoft.com/library/windows/apps/hh967989)) and routes them to the [**Click**](https://msdn.microsoft.com/library/windows/apps/br227737) event of the control. When a key press is handled by the control class, the [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events are not raised.  
> This provides a built-in keyboard equivalent for invoking the button, similar to tapping it with a finger or clicking it with a mouse. Keys other than Space or Enter still fire [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events. For more info about how class-based handling of events works (specifically, the "Input event handlers in controls" section), see [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/mt185584).


Controls in your UI generate keyboard events only when they have input focus. An individual control gains focus when the user clicks or taps directly on that control in the layout, or uses the Tab key to step into a tab sequence within the content area.

You can also call a control's [**Focus**](https://msdn.microsoft.com/library/windows/apps/hh702161) method to force focus. This is necessary when you implement shortcut keys, because keyboard focus is not set by default when your UI loads. For more info, see the **Shortcut keys example** later in this topic.

For a control to receive input focus, it must be enabled, visible, and have [**IsTabStop**](https://msdn.microsoft.com/library/windows/apps/br209422) and [**HitTestVisible**](https://msdn.microsoft.com/library/windows/apps/br208933) property values of **true**. This is the default state for most controls. When a control has input focus, it can raise and respond to keyboard input events as described later in this topic. You can also respond to a control that is receiving or losing focus by handling the [**GotFocus**](https://msdn.microsoft.com/library/windows/apps/br208927) and [**LostFocus**](https://msdn.microsoft.com/library/windows/apps/br208943) events.

By default, the tab sequence of controls is the order in which they appear in the Extensible Application Markup Language (XAML). However, you can modify this order by using the [**TabIndex**](https://msdn.microsoft.com/library/windows/apps/br209461) property. For more info, see [Implementing keyboard accessibility](https://msdn.microsoft.com/library/windows/apps/hh868161).

## Keyboard event handlers


An input event handler implements a delegate that provides the following information:

-   The sender of the event. The sender reports the object where the event handler is attached.
-   Event data. For keyboard events, that data will be an instance of [**KeyRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh943072). The delegate for handlers is [**KeyEventHandler**](https://msdn.microsoft.com/library/windows/apps/br227904). The most relevant properties of **KeyRoutedEventArgs** for most handler scenarios are [**Key**](https://msdn.microsoft.com/library/windows/apps/hh943074) and possibly [**KeyStatus**](https://msdn.microsoft.com/library/windows/apps/hh943075).
-   [**OriginalSource**](https://msdn.microsoft.com/library/windows/apps/br208810). Because the keyboard events are routed events, the event data provides **OriginalSource**. If you deliberately allow events to bubble up through an object tree, **OriginalSource** is sometimes the object of concern rather than sender. However, that depends on your design. For more information about how you might use **OriginalSource** rather than sender, see the "Keyboard Routed Events" section of this topic, or [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/mt185584).

### Attaching a keyboard event handler

You can attach keyboard event-handler functions for any object that includes the event as a member. This includes any [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) derived class. The following XAML example shows how to attach handlers for the [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) event for a [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704).

```xaml
<Grid KeyUp="Grid_KeyUp">
  ...
</Grid>
```

You can also attach an event handler in code. For more info, see [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/mt185584).

### Defining a keyboard event handler

The following example shows the incomplete event handler definition for the [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) event handler that was attached in the preceding example.

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

All keyboard events use [**KeyRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh943072) for event data, and **KeyRoutedEventArgs** contains the following properties:

-   [**Key**](https://msdn.microsoft.com/library/windows/apps/hh943074)
-   [**KeyStatus**](https://msdn.microsoft.com/library/windows/apps/hh943075)
-   [**Handled**](https://msdn.microsoft.com/library/windows/apps/hh943073)
-   [**OriginalSource**](https://msdn.microsoft.com/library/windows/apps/br208810) (inherited from [**RoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/br208809))

### Key

The [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) event is raised if a key is pressed. Likewise, [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) is raised if a key is released. Usually, you listen to the events to process a specific key value. To determine which key is pressed or released, check the [**Key**](https://msdn.microsoft.com/library/windows/apps/hh943074) value in the event data. **Key** returns a [**VirtualKey**](https://msdn.microsoft.com/library/windows/apps/br241812) value. The **VirtualKey** enumeration includes all the supported keys.

### Modifier keys

Modifier keys are keys such as Ctrl or Shift that users typically press in combination with other keys. Your app can use these combinations as keyboard shortcuts to invoke app commands.

You detect shortcut key combinations by using code in your [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) event handlers. You can then track the pressed state of the modifier keys you are interested in. When a keyboard event occurs for a non-modifier key, you can check whether a modifier key is in the pressed state at the same time.

> [!NOTE]
> The Alt key is represented by the **VirtualKey.Menu** value.

 

### Shortcut keys example


The following example demonstrates how to implement shortcut keys. In this example, users can control media playback using Play, Pause, and Stop buttons or Ctrl+P, Ctrl+A, and Ctrl+S keyboard shortcuts. The button XAML shows the shortcuts by using tooltips and [**AutomationProperties**](https://msdn.microsoft.com/library/windows/apps/br209081) properties in the button labels. This self-documentation is important to increase the usability and accessibility of your app. For more info, see [Keyboard accessibility](https://msdn.microsoft.com/library/windows/apps/mt244347).

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
void MainPage::ProgrammaticFocus(Object^ sender, RoutedEventArgs^ e) {
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
    if (IsCtrlKeyPressed()) {
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
> Setting [**AutomationProperties.AcceleratorKey**](https://msdn.microsoft.com/library/windows/apps/hh759762) or [**AutomationProperties.AccessKey**](https://msdn.microsoft.com/library/windows/apps/hh759763) in XAML provides string information, which documents the shortcut key for invoking that particular action. The information is captured by Microsoft UI Automation clients such as Narrator, and is typically provided directly to the user.
>
> Setting **AutomationProperties.AcceleratorKey** or **AutomationProperties.AccessKey** does not have any action on its own. You will still need to attach handlers for [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) or [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events in order to actually implement the keyboard shortcut behavior in your app. Also, the underline text decoration for an access key is not provided automatically. You must explicitly underline the text for the specific key in your mnemonic as inline [**Underline**](https://msdn.microsoft.com/library/windows/apps/br209982) formatting if you wish to show underlined text in the UI.

 

## Keyboard routed events


Certain events are routed events, including [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942). Routed events use the bubbling routing strategy. The bubbling routing strategy means that an event originates from a child object and is then routed up to successive parent objects in the object tree. This presents another opportunity to handle the same event and interact with the same event data.

Consider the following XAML example, which handles [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events for a [**Canvas**](https://msdn.microsoft.com/library/windows/apps/br209267) and two [**Button**](https://msdn.microsoft.com/library/windows/apps/br209265) objects. In this case, if you release a key while focus is held by either **Button** object, it raises the **KeyUp** event. The event is then bubbled up to the parent **Canvas**.

```xaml
<StackPanel KeyUp="StackPanel_KeyUp">
  <Button Name="ButtonA" Content="Button A"/>
  <Button Name="ButtonB" Content="Button B"/>
  <TextBlock Name="statusTextBlock"/>
</StackPanel>
```

The following example shows how to implement the [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) event handler for the corresponding XAML content in the preceding example.

```csharp
void StackPanel_KeyUp(object sender, KeyRoutedEventArgs e)
{
    statusTextBlock.Text = String.Format(
        "The key {0} was pressed while focus was on {1}",
        e.Key.ToString(), (e.OriginalSource as FrameworkElement).Name);
}
```

Notice the use of the [**OriginalSource**](https://msdn.microsoft.com/library/windows/apps/br208810) property in the preceding handler. Here, **OriginalSource** reports the object that raised the event. The object could not be the [**StackPanel**](https://msdn.microsoft.com/library/windows/apps/br209635) because the **StackPanel** is not a control and cannot have focus. Only one of the two buttons within the **StackPanel** could possibly have raised the event, but which one? You use **OriginalSource** to distinguish the actual event source object, if you are handling the event on a parent object.

### The Handled property in event data

Depending on your event handling strategy, you might want only one event handler to react to a bubbling event. For instance, if you have a specific [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) handler attached to one of the [**Button**](https://msdn.microsoft.com/library/windows/apps/br209265) controls, it would have the first opportunity to handle that event. In this case, you might not want the parent panel to also handle the event. For this scenario, you can use the [**Handled**](https://msdn.microsoft.com/library/windows/apps/hh943073) property in the event data.

The purpose of the [**Handled**](https://msdn.microsoft.com/library/windows/apps/hh943073) property in a routed event data class is to report that another handler you registered earlier on the event route has already acted. This influences the behavior of the routed event system. When you set **Handled** to **true** in an event handler, that event stops routing and is not sent to successive parent elements.

### AddHandler and already-handled keyboard events

You can use a special technique for attaching handlers that can act on events that you already marked as handled. This technique uses the [**AddHandler**](https://msdn.microsoft.com/library/windows/apps/hh702399) method to register a handler, rather than using XAML attributes or language-specific syntax for adding handlers, such as += in C\#.

A general limitation of this technique is that the **AddHandler** API takes a parameter of type [**RoutedEvent**](https://msdn.microsoft.com/library/windows/apps/br208808) idnentifying the routed event in question. Not all routed events provide a **RoutedEvent** identifier, and this consideration thus affects which routed events can still be handled in the [**Handled**](https://msdn.microsoft.com/library/windows/apps/hh943073) case. The [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events have routed event identifiers ([**KeyDownEvent**](https://msdn.microsoft.com/library/windows/apps/hh702416) and [**KeyUpEvent**](https://msdn.microsoft.com/library/windows/apps/hh702418)) on [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911). However, other events such as [**TextBox.TextChanged**](https://msdn.microsoft.com/library/windows/apps/br209706) do not have routed event identifiers and thus cannot be used with the **AddHandler** technique.

### Overriding keyboard events and behavior

You can override key events for specific controls (such as [**GridView**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.GridView)) to provide consistent focus navigation for various input devices, including keyboard and gamepad.

In the following example, we subclass the control and override the KeyDown behavior to move focus to the the GridView content when any arrow key is pressed.

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
> If using a GridView for layout only, consider using other controls such as [**ItemsControl**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.ItemsControl) with [**ItemsWrapGrid**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.ItemsWrapGrid).

## Commanding

A small number of UI elements provide built-in support for commanding. Commanding uses input-related routed events in its underlying implementation. It enables processing of related UI input, such as a certain pointer action or a specific accelerator key, by invoking a single command handler.

If commanding is available for a UI element, consider using its commanding APIs instead of any discrete input events. For more info, see [**ButtonBase.Command**](https://msdn.microsoft.com/library/windows/apps/br227740).

You can also implement [**ICommand**](https://msdn.microsoft.com/library/windows/apps/br227885) to encapsulate command functionality that you invoke from ordinary event handlers. This enables you to use commanding even when there is no **Command** property available.

## Text input and controls

Certain controls react to keyboard events with their own handling. For instance, [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683) is a control that is designed to capture and then visually represent text that was entered by using the keyboard. It uses [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) and [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) in its own logic to capture keystrokes, then also raises its own [**TextChanged**](https://msdn.microsoft.com/library/windows/apps/br209706) event if the text actually changed.

You can still generally add handlers for [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) and [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) to a [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683), or any related control that is intended to process text input. However, as part of its intended design, a control might not respond to all key values that are directed to it through key events. Behavior is specific to each control.

As an example, [**ButtonBase**](https://msdn.microsoft.com/library/windows/apps/br227736) (the base class for [**Button**](https://msdn.microsoft.com/library/windows/apps/br209265)) processes [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) so that it can check for the Spacebar or Enter key. **ButtonBase** considers **KeyUp** equivalent to a mouse left button down for purposes of raising a [**Click**](https://msdn.microsoft.com/library/windows/apps/br227737) event. This processing of the event is accomplished when **ButtonBase** overrides the virtual method [**OnKeyUp**](https://msdn.microsoft.com/library/windows/apps/hh967983). In its implementation, it sets [**Handled**](https://msdn.microsoft.com/library/windows/apps/hh943073) to **true**. The result is that any parent of a button that is listening for a key event, in the case of a Spacebar, would not receive the already-handled event for its own handlers.

Another example is [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683). Some keys, such as the ARROW keys, are not considered text by **TextBox** and are instead considered specific to the control UI behavior. The **TextBox** marks these event cases as handled.

Custom controls can implement their own similar override behavior for key events by overriding [**OnKeyDown**](https://msdn.microsoft.com/library/windows/apps/hh967982) / [**OnKeyUp**](https://msdn.microsoft.com/library/windows/apps/hh967983). If your custom control processes specific accelerator keys, or has control or focus behavior that is similar to the scenario described for [**TextBox**](https://msdn.microsoft.com/library/windows/apps/br209683), you should place this logic in your own **OnKeyDown** / **OnKeyUp** overrides.

## The touch keyboard

Text input controls provide automatic support for the touch keyboard. When the user sets the input focus to a text control by using touch input, the touch keyboard appears automatically. When the input focus is not on a text control, the touch keyboard is hidden.

When the touch keyboard appears, it automatically repositions your UI to ensure that the focused element remains visible. This can cause other important areas of your UI to move off screen. However, you can disable the default behavior and make your own UI adjustments when the touch keyboard appears. For more info, see the [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard).

If you create a custom control that requires text input, but does not derive from a standard text input control, you can add touch keyboard support by implementing the correct UI Automation control patterns. For more info, see the [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard).

Key presses on the touch keyboard raise [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events just like key presses on hardware keyboards. However, the touch keyboard will not raise input events for Ctrl+A, Ctrl+Z, Ctrl+X, Ctrl+C, and Ctrl+V, which are reserved for text manipulation in the input control.

You can make it much faster and easier for users to enter data in your app by setting the input scope of the text control to match the kind of data you expect the user to enter. The input scope provides a hint at the type of text input expected by the control so the system can provide a specialized touch keyboard layout for the input type. For example, if a text box is used only to enter a 4-digit PIN, set the [**InputScope**](https://msdn.microsoft.com/library/windows/apps/hh702632) property to [**Number**](https://msdn.microsoft.com/library/windows/apps/hh702028). This tells the system to show the numeric keypad layout, which makes it easier for the user to enter the PIN. For more detail, see [Use input scope to change the touch keyboard](https://msdn.microsoft.com/library/windows/apps/mt280229).

## Related articles

**Developers**
* [Keyboard interactions](keyboard-interactions.md)
* [Identify input devices](identify-input-devices.md)
* [Respond to the presence of the touch keyboard](respond-to-the-presence-of-the-touch-keyboard.md)

**Designers**
* [Keyboard design guidelines](https://msdn.microsoft.com/library/windows/apps/hh972345)

**Samples**
* [Touch keyboard sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/TouchKeyboard)
* [Basic input sample](http://go.microsoft.com/fwlink/p/?LinkID=620302)
* [Low latency input sample](http://go.microsoft.com/fwlink/p/?LinkID=620304)
* [Focus visuals sample](http://go.microsoft.com/fwlink/p/?LinkID=619895)

**Archive Samples**
* [Input sample](http://go.microsoft.com/fwlink/p/?linkid=226855)
* [Input: Device capabilities sample](http://go.microsoft.com/fwlink/p/?linkid=231530)
* [Input: Touch keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=246019)
* [Responding to the appearance of the on-screen keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=231633)
* [XAML text editing sample](http://go.microsoft.com/fwlink/p/?LinkID=251417)
 

 
