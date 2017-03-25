---
author: Karl-Bridge-Microsoft
Description: Respond to keystroke actions from hardware or software keyboards in your apps using both keyboard and class event handlers.
title: Keyboard interactions
ms.assetid: FF819BAC-67C0-4EC9-8921-F087BE188138
label: Keyboard interactions
template: detail.hbs
keywords: keyboard, accessibility, navigation, focus, text, input, user interactions
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Keyboard interactions
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Keyboard input is an important part of the overall user interaction experience for apps. The keyboard is indispensable to people with certain disabilities or users who just consider it a more efficient way to interact with an app. For example, users should be able to navigate your app by using Tab and arrow keys, activate UI elements by using Spacebar and Enter, and access commands by using keyboard shortcuts.  

![keyboard hero image](images/input-patterns/input-keyboard-small.jpg)

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941)</li>
<li>[**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942)</li>
<li>[**KeyRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh943072)</li>
</ul>
</div>
 


A well-designed keyboard UI is an important aspect of software accessibility. It enables users with vision impairments or who have certain motor disabilities to navigate an app and interact with its features. Such users might not be able to operate a mouse and instead rely on various assistive technologies such as keyboard enhancement tools, on-screen keyboards, screen enlargers, screen readers, and voice input utilities.

Users can interact with universal apps through a hardware keyboard and two software keyboards: the On-Screen Keyboard (OSK) and the touch keyboard.

On-Screen Keyboard  
The On-Screen Keyboard is a visual, software keyboard that you can use instead of the physical keyboard to type and enter data using touch, mouse, pen/stylus or other pointing device (a touch screen is not required). The On-Screen Keyboard is provided for systems that don't have a physical keyboard, or for users whose mobility impairments prevent them from using traditional physical input devices. The On-Screen Keyboard emulates most, if not all, the functionality of a hardware keyboard.

The On-Screen Keyboard can be turned on from the Keyboard page in Settings &gt; Ease of access.

**Note**  The On-Screen Keyboard has priority over the touch keyboard, which won't be shown if the On-Screen Keyboard is present.

 

![the on-screen keyboard](images/input-patterns/osk.png)

<sup>On-Screen Keyboard</sup>

Touch keyboard  
The touch keyboard is a visual, software keyboard used for text entry with touch input. It is not a replacement for the On-Screen Keyboard as it's used for text input only (it doesn't emulate the hardware keyboard).

Depending on the device, the touch keyboard appears when a text field or other editable text control gets focus, or when the user manually enables it through the **Notification Center**:

![touch keyboard icon in the notification center](images/input-patterns/touch-keyboard-notificationcenter.png)

**Note**  The user might have to go to the **Tablet mode** screen in Settings &gt; System and turn on "Make Windows more touch-friendly when using your device as a tablet" to enable the automatic appearance of the touch keyboard.

 

If your app sets focus programmatically to a text input control, the touch keyboard is not invoked. This eliminates unexpected behaviors not instigated directly by the user. However, the keyboard does automatically hide when focus is moved programmatically to a non-text input control.

The touch keyboard typically remains visible while the user navigates between controls in a form. This behavior can vary based on the other control types within the form.

The following is a list of non-edit controls that can receive focus during a text entry session using the touch keyboard without dismissing the keyboard. Rather than needlessly churn the UI and potentially disorient the user, the touch keyboard remains in view because the user is likely to go back and forth between these controls and text entry with the touch keyboard.

-   Check box
-   Combo box
-   Radio button
-   Scroll bar
-   Tree
-   Tree item
-   Menu
-   Menu bar
-   Menu item
-   Toolbar
-   List
-   List item

Here are examples of different modes for the touch keyboard. The first image is the default layout, the second is the thumb layout (which might not be available in all languages).

Here are examples of different modes for the touch keyboard. The first image is the default layout, the second is the thumb layout (which might not be available in all languages).
<table>
<tr>
	<td>**The touch keyboard in default layout mode:  **</td>
	<td>![the touch keyboard in default layout mode](images/touchkeyboard-standard.png)</td>
</tr>
<tr>
	<td>**The touch keyboard in expanded layout mode:  **</td>
	<td>![the touch keyboard in expanded layout mode](images/touchkeyboard-expanded.png)</td>
</tr>
<tr>
	<td>**The touch keyboard in default thumb layout mode:  **</td>
	<td>![the touch keyboard in thumb layout mode](images/touchkeyboard-thumb.png)</td>
</tr>
<tr>
	<td>**The touch keyboard in numeric thumb layout mode:  **</td>
	<td>![the touch keyboard in numeric thumb layout mode](images/touchkeyboard-numeric-thumb.png)</td>
</tr>
</table>


Successful keyboard interactions enable users to accomplish basic app scenarios using only the keyboard; that is, users can reach all interactive elements and activate default functionality. A number of factors can affect the degree of success, including keyboard navigation, access keys for accessibility, and accelerator (or shortcut) keys for advanced users.

**Note**  The touch keyboard does not support toggle and most system commands (see [Patterns](#keyboard_command_patterns)).

## Navigation


To use a control (including navigation elements) with the keyboard, the control must have focus. One way for a control to receive keyboard focus is to make it accessible via tab navigation. A well designed keyboard navigation model provides a logical and predictable tab order that enables a user to explore and use your app quickly and efficiently.

All interactive controls should have tab stops (unless they are in a group), whereas non-interactive controls, such as labels, should not.

A set of related controls can be made into a control group and assigned a single tab stop. Control groups are used for sets of controls that behave like a single control, such as radio buttons. They can also be used when there too many controls to navigate efficiently with the Tab key alone. The arrow keys, Home, End, Page Up, and Page Down move input focus among the controls within a group (it is not possible to navigate out of a control group using these keys).

You should set initial keyboard focus on the element that users will intuitively (or most likely) interact with first when your app starts. Often, this is the main content view of the app so that a user can immediately start using the arrow keys to scroll the app content.

Don’t set initial keyboard focus on an element with potentially negative, or even disastrous, results. This can prevent loss of data or system access.

Try to rank and present the most important commands, controls, and content first in both the tab order and the display order (or visual hierarchy). However, the actual display position can depend on the parent layout container and certain properties of the child elements that influence the layout. In particular, layouts that use a grid metaphor or a table metaphor can have a reading order quite different from the tab order. This is not always a problem, but you should test your app's functionality, both as a touchable UI and as a keyboard-accessible UI.

Tab order should follow reading order, whenever possible. This can reduce confusion and is dependent on locale and language.

Associate keyboard buttons with appropriate UI (back and forward buttons) in your app.

Try to make navigating back to the start screen of your app and between key content as easy and straightforward as possible.

Use the arrow keys as keyboard shortcuts for proper inner navigation among child elements of composite elements. If tree view nodes have separate child elements for handling expand–collapse and node activation, use the left and right arrow keys to provide keyboard expand–collapse functionality. This is consistent with the platform controls.

Because the touch keyboard occludes a large portion of the screen, the Universal Windows Platform (UWP) ensures that the input field with focus scrolls into view as a user navigates through the controls on the form, including controls that are not currently in view. Custom controls should emulate this behavior.

![a form with and without the touch keyboard showing](images/input-patterns/touch-keyboard-pan1.png)

In some cases, there are UI elements that should stay on the screen the entire time. Design the UI so that the form controls are contained in a panning region and the important UI elements are static. For example:

![a form that contains areas that should always stay in view](images/input-patterns/touch-keyboard-pan2.png)
## Activation


A control can be activated in a number of different ways, whether it currently has focus or not.

Spacebar, Enter, and Esc  
The spacebar should activate the control with input focus. The Enter key should activate a default control or the control with input focus. A default control is the control with initial focus or one that responds exclusively to the Enter key (typically it changes with input focus). In addition, the Esc key should close or exit transitory UI, such as menus and dialogs.

The Calculator app shown here uses the spacebar to activate the button with focus, locks the Enter key to the “=” button, and locks the Esc key to the “C” button.

![the calculator app](images/input-patterns/calculator.png)

Keyboard modifiers  
Keyboard modifiers fall into the following categories:


| Category | Description |
|----------|-------------|
| Shortcut key | Perform a common action without UI such as "Ctrl-S" for **Save**. Implement keyboard shortcuts for key app functionality. Not every command has, or requires, a shortcut. |   
| Access key/Hot key | Assigned to every visible, top-level control such as "Alt-F" for the **File** menu. An access key does not invoke or activate a command. |
| Accelerator key | Perform default system or app-defined commands such as "Alt-PrtScrn" for screen capture, "Alt-Tab" to switch apps, or "F1" for help. A command associated with an accelerator key does not have to be a menu item. |
| Application key/Menu key | Show context menu. |
| Window key/Command key | Activate system commands such as **System Menu**, **Lock Screen**, or **Show Desktop**. |

Access keys and accelerator keys support interaction with controls directly instead of navigating to them using the Tab key.
> While some controls have intrinsic labels, such as command buttons, check boxes, and radio buttons, other controls have external labels, such as list views. For controls with external labels, the access key is assigned to the label, which, when invoked, sets focus to an element or value within the associated control.


The example here, shows the access keys for the **Page Layout** tab in **Word**.

![the access keys for the page layout tab in word](images/input-patterns/accesskeys-show.png)

Here, the Indent Left text field value is highlighted after entering the access key identified in the associated label.

![the indent left text field value is highlighted after entering the access key identified in the associated label](images/input-patterns/accesskeys-entered.png)

## Usability and accessibility


A well-designed keyboard interaction experience is an important aspect of software accessibility. It enables users with vision impairments or who have certain motor disabilities to navigate an app and interact with its features. Such users might be unable to operate a mouse and must, instead, rely on various assistive technologies that include keyboard enhancement tools and on-screen keyboards (along with screen enlargers, screen readers, and voice input utilities). For these users, comprehensiveness is more important than consistency.

Experienced users often have a strong preference for using the keyboard, because keyboard-based commands can be entered more quickly and don't require removing their hands from the keyboard. For these users, efficiency and consistency are crucial; comprehensiveness is important only for the most frequently used commands.

There are subtle distinctions when designing for usability and accessibility, which is why two different keyboard access mechanisms are supported.

Access keys have the following characteristics:

-   An access key is a shortcut to a UI element in your app.
-   They use the Alt key plus an alphanumeric key.
-   They are primarily for accessibility.
-   They are assigned to all menus and most dialog box controls.
-   They aren't intended to be memorized, so they are documented directly in the UI by underlining the corresponding control label character.
-   They have effect only in the current window, and navigate to the corresponding menu item or control.
-   They aren't assigned consistently because they can't always be. However, access keys should be assigned consistently for commonly used commands, especially commit buttons.
-   They are localized.

Because access keys aren't intended to be memorized, they are assigned to a character that is early in the label to make them easy to find, even if there is a keyword that appears later in the label.

In contrast, accelerator keys have the following characteristics:

-   An accelerator key is a shortcut to an app command.
-   They primarily use Ctrl and Function key sequences (Windows system shortcut keys also use Alt+non-alphanumeric keys and the Windows logo key).
-   They are primarily for efficiency for advanced users.
-   They are assigned only to the most commonly used commands.
-   They are intended to be memorized, and are documented only in menus, tooltips, and Help.
-   They have effect throughout the entire program, but have no effect if they don't apply.
-   They must be assigned consistently because they are memorized and not directly documented.
-   They aren't localized.

Because accelerator keys are intended to be memorized, the most frequently used accelerator keys ideally use letters from the first or most memorable characters within the command's keywords, such as Ctrl+C for Copy and Ctrl+Q for Request.

Users should be able to accomplish all tasks supported by your app using only the hardware keyboard or the On-Screen Keyboard.

You should provide an easy way for users who rely on screen readers and other assistive technology to discover your app's accelerator keys. Communicate accelerator keys by using tooltips, accessible names, accessible descriptions, or some other form of on-screen communication. At a minimum, access and accelerator keys should be well documented in your app's Help content.

Don’t assign well-known or standard accelerator keys to other functionality. For example, Ctrl+F is typically used for find or search.

Don’t bother trying to assign access keys to all interactive controls in a dense UI. Just ensure the most important and the most used have access keys, or use control groups and assign an access key to the control group label.

Don't change commands using keyboard modifiers. Doing so is undiscoverable and can cause confusion.

Don't disable a control while it has input focus. This can interfere with keyboard input.

To ensure successful keyboard interaction experiences, it is critical to test your app thoroughly and exclusively with the keyboard.

## Text input


Always query the device capabilities when relying on keyboard input. On some devices (such as phone), the touch keyboard can only be used for text input as it does not provide many of the accelerators or command keys found on a hardware keyboard (such as alt, the function keys, or the Windows Logo key).

Don't make users navigate the app using the touch keyboard. Depending on the control getting focus, the touch keyboard might get dismissed.

Try to display the keyboard throughout the entire interaction with your form. This eliminates UI churn that can disorient the user in the middle of a form or text entry flow.

Ensure that users can always see the input field that they're typing into. The touch keyboard occludes half of the screen, so the input field with focus should scroll into view as the user traverses the form.

A standard hardware keyboard or OSK consists of seven types of keys, each supporting unique functionality:

-   Character key: sends a literal character to the window with input focus.
-   Modifier key: alters the function of a primary key when pressed simultaneously, such as Ctrl, Alt, Shift, and the Windows logo key.
-   Navigation key: moves input focus or text input location, such as the Tab, Home, End, Page Up, Page Down, and directional arrow keys.
-   Editing key: manipulates text, such as the Shift, Tab, Enter, Insert, Backspace, and Delete keys.
-   Function key: performs a special function, such as F1 through F12 keys.
-   Toggle key: puts the system into a mode, such as Caps Lock, ScrLk, and Num Lock keys.
-   Command key: performs a system task or command activation, such as Spacebar, Enter, Esc, Pause/Break, and Print Screen keys.

In addition to these categories, a secondary class of keys and key combinations exist that can be used as shortcuts to app functionality:

-   Access key: exposes controls or menu items by pressing the Alt key with a character key, indicated by underlining of the access key character assignment in a menu, or displaying of the access key character(s) in an overlay.
-   Accelerator key: exposes app commands by pressing a function key or the Ctrl key with a character key. Your app might or might not have UI that corresponds to the command.

Another class of key combinations, known as secure attention sequence (SAS), cannot be intercepted by an app. This is a security feature intended to protect the user's system during login, and include Ctrl-Alt-Del and Win-L.

The Notepad app is shown here with the expanded File menu that includes both access keys and accelerator keys.

![the notepad app with expanded file menu that includes both access keys and accelerator keys.](images/input-patterns/notepad.png)

## Keyboard commands


The following is a comprehensive list of the keyboard interactions provided across the various devices that support keyboard input. Some devices and platforms require native keystrokes and interactions, these are noted.

When designing custom controls and interactions, use this keyboard language consistently to make your app feel familiar, dependable, and easy to learn.

Don't redefine the default keyboard shortcuts.

The following tables list frequently used keyboard commands. For a complete list of keyboard commands, see [Windows Keyboard Shortcut Keys](http://go.microsoft.com/fwlink/p/?linkid=325424).

**Navigation commands**

| Action                               | Key command                                      |
|--------------------------------------|--------------------------------------------------|
| Back                                 | Alt+Left or the back button on special keyboards |
| Forward                              | Alt+Right                                        |
| Up                                   | Alt+Up                                           |
| Cancel or Escape from current mode   | Esc                                              |
| Move through items in a list         | Arrow key (Left, Right, Up, Down)                |
| Jump to next list of items           | Ctrl+Left                                        |
| Semantic zoom                        | Ctrl++ or Ctrl+-                                 |
| Jump to a named item in a collection | Start typing item name                           |
| Next page                            | Page Up, Page Down or Spacebar                   |
| Next tab                             | Ctrl+Tab                                         |
| Previous tab                         | Ctrl+Shift+Tab                                   |
| Open app bar                         | Windows+Z                                        |
| Activate or Navigate into an item    | Enter                                            |
| Select                               | Spacebar                                         |
| Continuously select                  | Shift+Arrow key                                  |
| Select all                           | Ctrl+A                                           |

 

**Common commands**

| Action                                                 | Key command     |
|--------------------------------------------------------|-----------------|
| Pin an item                                            | Ctrl+Shift+1    |
| Save                                                   | Ctrl+S          |
| Find                                                   | Ctrl+F          |
| Print                                                  | Ctrl+P          |
| Copy                                                   | Ctrl+C          |
| Cut                                                    | Ctrl+X          |
| New item                                               | Ctrl+N          |
| Paste                                                  | Ctrl+V          |
| Open                                                   | Ctrl+O          |
| Open address (for example, a URL in Internet Explorer) | Ctrl+L or Alt+D |

 

**Media navigation commands**

| Action       | Key command |
|--------------|-------------|
| Play/Pause   | Ctrl+P      |
| Next item    | Ctrl+F      |
| Preview item | Ctrl+B      |

 

Note: The media navigation key commands for Play/Pause and Next item are the same as the key commands for Print and Find, respectively. Common commands should take priority over media navigation commands. For example, if an app supports both plays media and prints, the key command Ctrl+P should print.
## Visual feedback


Use focus rectangles only with keyboard interactions. If the user initiates a touch interaction, make the keyboard UI gradually fade away. This keeps the UI clean and uncluttered.

Don't display visual feedback if an element doesn't support interaction (such as static text). Again, this keeps the UI clean and uncluttered.

Try to display visual feedback concurrently for all elements that represent the same input target.

Try to provide on-screen buttons (such as + and -) as hints for emulating touch-based manipulations such as panning, rotating, zooming, and so on.

For more general guidance on visual feedback, see [Guidelines for visual feedback](guidelines-for-visualfeedback.md).


## Keyboard events and focus


The following keyboard events can occur for both hardware and touch keyboards.

| Event                                      | Description                    |
|--------------------------------------------|--------------------------------|
| [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) | Occurs when a key is pressed.  |
| [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942)     | Occurs when a key is released. |


**Important**  
Some Windows Runtime controls handle input events internally. In these cases, it might appear that an input event doesn't occur because your event listener doesn't invoke the associated handler. Typically, this subset of keys is processed by the class handler to provide built in support of basic keyboard accessibility. For example, the [**Button**](https://msdn.microsoft.com/library/windows/apps/br209265) class overrides the [**OnKeyDown**](https://msdn.microsoft.com/library/windows/apps/hh967982) events for both the Space key and the Enter key (as well as [**OnPointerPressed**](https://msdn.microsoft.com/library/windows/apps/hh967989)) and routes them to the [**Click**](https://msdn.microsoft.com/library/windows/apps/br227737) event of the control. When a key press is handled by the control class, the [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events are not raised.

This provides a built-in keyboard equivalent for invoking the button, similar to tapping it with a finger or clicking it with a mouse. Keys other than Space or Enter still fire [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events. For more info about how class-based handling of events works (specifically, the "Input event handlers in controls" section), see [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/mt185584).


Controls in your UI generate keyboard events only when they have input focus. An individual control gains focus when the user clicks or taps directly on that control in the layout, or uses the Tab key to step into a tab sequence within the content area.

You can also call a control's [**Focus**](https://msdn.microsoft.com/library/windows/apps/hh702161) method to force focus. This is necessary when you implement shortcut keys, because keyboard focus is not set by default when your UI loads. For more info, see the [Shortcut keys example](#shortcut_keys_example) later in this topic.

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
    this->Loaded+=ref new RoutedEventHandler(this, &MainPage::ProgrammaticFocus);
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


void KeyboardSupport::MainPage::Grid_KeyDown(Platform::Object^ sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs^ e)
{
    if (e->Key == VirtualKey::Control) isCtrlKeyPressed = true;
}


void KeyboardSupport::MainPage::Grid_KeyUp(Platform::Object^ sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs^ e)
{
    if (e->Key == VirtualKey::Control) isCtrlKeyPressed = false;
    else if (isCtrlKeyPressed) {
        if (e->Key==VirtualKey::P) {
            DemoMovie->Play();
        }
        if (e->Key==VirtualKey::A) {DemoMovie->Pause();}
        if (e->Key==VirtualKey::S) {DemoMovie->Stop();}
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

private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
{
    if (e.Key == VirtualKey.Control) isCtrlKeyPressed = false;
}

private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
{
    if (e.Key == VirtualKey.Control) isCtrlKeyPressed = true;
    else if (isCtrlKeyPressed)
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

Private Sub Grid_KeyUp(sender As Object, e As KeyRoutedEventArgs)
    If e.Key = Windows.System.VirtualKey.Control Then
        isCtrlKeyPressed = False
    End If
End Sub

Private Sub Grid_KeyDown(sender As Object, e As KeyRoutedEventArgs)
    If e.Key = Windows.System.VirtualKey.Control Then isCtrlKeyPressed = True
    If isCtrlKeyPressed Then
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

When the touch keyboard appears, it automatically repositions your UI to ensure that the focused element remains visible. This can cause other important areas of your UI to move off screen. However, you can disable the default behavior and make your own UI adjustments when the touch keyboard appears. For more info, see [Responding to the appearance of the on-screen keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=231633).

If you create a custom control that requires text input, but does not derive from a standard text input control, you can add touch keyboard support by implementing the correct UI Automation control patterns. For more info, see [Respond to the presence of the touch keyboard](respond-to-the-presence-of-the-touch-keyboard.md) and the [Touch keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=246019).

Key presses on the touch keyboard raise [**KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208941) and [**KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208942) events just like key presses on hardware keyboards. However, the touch keyboard will not raise input events for Ctrl+A, Ctrl+Z, Ctrl+X, Ctrl+C, and Ctrl+V, which are reserved for text manipulation in the input control.

You can make it much faster and easier for users to enter data in your app by setting the input scope of the text control to match the kind of data you expect the user to enter. The input scope provides a hint at the type of text input expected by the control so the system can provide a specialized touch keyboard layout for the input type. For example, if a text box is used only to enter a 4-digit PIN, set the [**InputScope**](https://msdn.microsoft.com/library/windows/apps/hh702632) property to [**Number**](https://msdn.microsoft.com/library/windows/apps/hh702028). This tells the system to show the numeric keypad layout, which makes it easier for the user to enter the PIN. For more detail, see [Use input scope to change the touch keyboard](https://msdn.microsoft.com/library/windows/apps/mt280229).


## Additional articles in this section

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>[Respond to the presence of the touch keyboard](respond-to-the-presence-of-the-touch-keyboard.md)</p></td>
<td align="left"><p>Learn how to tailor the UI of your app when showing or hiding the touch keyboard.</p></td>
</tr>
</tbody>
</table>

## Related articles

**Developers**
* [Identify input devices](identify-input-devices.md)
* [Respond to the presence of the touch keyboard](respond-to-the-presence-of-the-touch-keyboard.md)

**Designers**
* [Keyboard design guidelines](https://msdn.microsoft.com/library/windows/apps/hh972345)

**Samples**
* [Basic input sample](http://go.microsoft.com/fwlink/p/?LinkID=620302)
* [Low latency input sample](http://go.microsoft.com/fwlink/p/?LinkID=620304)
* [Focus visuals sample](http://go.microsoft.com/fwlink/p/?LinkID=619895)

**Archive Samples**
* [Input sample](http://go.microsoft.com/fwlink/p/?linkid=226855)
* [Input: Device capabilities sample](http://go.microsoft.com/fwlink/p/?linkid=231530)
* [Input: Touch keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=246019)
* [Responding to the appearance of the on-screen keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=231633)
* [XAML text editing sample](http://go.microsoft.com/fwlink/p/?LinkID=251417)
 

 
