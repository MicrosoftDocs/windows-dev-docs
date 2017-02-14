---
author: Karl-Bridge-Microsoft
Description: Enable keyboard access using tab navigation and access keys so users can navigate across UI elements with the keyboard.
title: Access keys
ms.assetid: C2F3F3CE-737F-4652-98B7-5278A462F9D3
label: Access keys
template: detail.hbs
keywords: Access keys, keyboard, accessibility, user interaction, input
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Access keys

Users who have difficulty using a mouse, such as those with motor disabilities, often rely on the keyboard to navigate and interact with an app.  The XAML framework enables you to provide keyboard access to UI elements through tab navigation and access keys.

- Tab navigation is a basic keyboard accessibility affordance (enabled by default) that lets users move focus between UI elements using the tab and arrow keys on the keyboard.
- Access keys are a supplementary accessibility affordance (that you implement in your app) for quick access to app commands using a combination of keyboard modifier (Alt key) and one or more alphnumeric keys (typically a letter associated with the command). Common access keys include _Alt+F_ to open the File menu and _Alt+AL_ to align left.  

For more info about keyboard navigation and accessibility, see [Keyboard interaction](https://msdn.microsoft.com/windows/uwp/input-and-devices/keyboard-interactions) and [Keyboard accessibility](https://msdn.microsoft.com/windows/uwp/accessibility/keyboard-accessibility). This article assumes you understand the concepts discussed in those articles.

## Access key overview

Access keys let users directly invoke buttons or set focus with the keyboard without requiring them to repeatedly press the arrow keys and tab. Access keys are intended to be easily discoverable, so you should document them directly in the UI; for example, a floating badge over the control with the access key.

![Example of access keys and associated key tips in Microsoft Word](images/keyboard/accesskeys-keytips.png)

_Figure 1: Example of access keys and associated key tips in Microsoft Word._

An access key is one or several alphanumeric characters associated with a UI element. For example, Microsoft Word uses _H_ for the Home tab, _2_ for Undo button, or _JI_ for the Draw tab.

**Access key scope**

An access key belongs to a specific scope. For example, in Figure 1, _F_, _H_, _N_, and _JI_, belong to the page’s scope.  When the user presses _H_, the scope changes to the Home tab’s scope and its access keys are shown as seen in Figure 2. The access keys, _V_, _FP_, _FF_, and _FS_ belong to the Home tab’s scope.

![Example of access keys and associated key tips for the Home tab scope in Microsoft Word](images/keyboard/accesskeys-keytips-hometab.png)

_Figure 2: Example of access keys and associated key tips for the Home tab scope in Microsoft Word._

Two elements can have the same access keys if the elements belong to different scopes. For example, _2_ is the access key for Undo on the page’s scope (Figure 1), and also for Italic in the Home tab’s scope (Figure 2). All access keys belong to the default scope unless another scope is specified.

**Access key sequence**

Access key combinations are typically pressed one key at a time to achieve the action rather than pressing the keys simultaneously. (There is an exception to this that we discuss in the next section.) The sequence of keystrokes needed to achieve the action is an _access key sequence_. The user presses the Alt key to initiate the access key sequence. An access key is invoked when the user presses the last key in an access key sequence. For example, to open the View tab in Word, the user would press the _Alt, W_ access key sequence.

A user can invoke several access keys in an access key sequence. For example, to open the Format Painter in a Word document, the user presses Alt to initialize the sequence, then presses _H_ to navigate to the Home section and change the access key scope, then _F_, and eventually _P_. _H_ and _FP_ are the access keys for the Home tab and the Format Painter button respectively.

Some elements finalize an access key sequence after they’re invoked (like the Format Painter button) and others don’t (like the Home tab). Invoking an access key can result in executing a command, moving the focus, changing the access key scope, or some other action associated with it.

## Access Key User Interaction

To understand the Access Key APIs, it is necessary to first understand the user interaction model. Below you can find a summary of the access key user interaction model:

- When the user presses the Alt key, the access key sequence starts, even when the focus is on an input control. Then, the user can press the access key to invoke the associated action. This user interaction requires that you document the available access keys within the UI with some visual affordance, such as floating badges, that are shown when the Alt key is pressed
- When the user presses the Alt key plus the access key simultaneously, the access key is invoked immediately. This is similar to having a keyboard shorcut defined by Alt+_access key_. In this case, the access key visual affordances are not shown. However, invoking an access key could result in changing the access key scope. In this case, an access key sequence is initiated and the visual affordances are shown for the new scope.
    > [!NOTE]
    > Only access keys with one character can take advantage of this user interaction. The Alt+_access key_ combination is not supported for access keys with more than one character.    
- When there are several multi-character access keys that share some characters, when the user presses a shared character, the access keys are filtered. For example, assume there are three access keys shown: _A1_, _A2_, and _C_. If the user presses _A_, then only the _A1_ and _A2_ access key are shown and the visual affordance for C is hidden.
- The Esc key removes one level the filtering. For example, if there are access keys _B_, _ABC_, _ACD_, and _ABD_ and the user presses _A_, then only _ABC_, _ACD_ and _ABD_ are shown. If the user then presses _B_, only _ABC_ and _ABD_ are shown. If user presses Esc, one level of filtering is removed and _ABC_, _ACD_ and _ABD_ access keys are shown. If the user presses Esc again, another level of filtering is removed and all the access keys -   _B_, _ABC_, _ACD_, and _ABD_ – are enabled and their visual affordances are shown.
- The Esc key navigates back to the previous scope. Access keys can belong to different scopes to make it easier to navigate across apps that have a lot of commands. The access key sequence always starts on the main scope. All access keys belong to the main scope except those that specify a particular UI element as their scope owner. When the user invokes the access key of an element that is a scope owner, the XAML framework automatically moves the scope to it and adds it to an internal access key navigation stack. The Esc key moves back through the access key navigation stack.
- There are several ways to dismiss the access key sequence:
    - The user can press Alt to dismiss an access key sequence that is in progress. Remember that pressing Alt initiates the access key sequence as well.
    - The Esc key dismisses the access key sequence if it is in the main scope and is not filtered.
        > [!NOTE]
        > The Esc keystroke is passed to the UI layer to be handled there as well.
    - The Tab key dismisses the access key sequence and returns to the Tab navigation.
    - The Enter key dismisses the access key sequence and sends the keystroke to the element that has the focus.
    - The arrow keys dismiss the access key sequence and send the keystroke to the element that has the focus.
    - A pointer down event such a mouse click or a touch dismisses the access key sequence.
    - By default, when an access key is invoked, the access key sequence is dismissed.  However, you can override this behavior by setting the [ExitDisplayModeOnAccessKeyInvoked](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.exitdisplaymodeonaccesskeyinvoked.aspx) property to **false**.
- Access key collisions occur when a deterministic finite automaton is not possible. Access key collisions are not desirable but can happen because of a large number of commands, localization issues, or runtime generation of access keys.

 There are two cases where collisions happen:
 - When two UI elements have the same access key value and belong to the same access key scope. For example, an access key _A1_ for a `button1` and access key _A1_ for a `button2` that belongs to the default scope. In this case, the system resolves the collision by processing the access key of the first element added to the visual tree. The rest are ignored.
 - When there is more than one computational option in the same access key scope. For example, _A_ and _A1_. When user presses _A_, the system has two options: invoke the _A_ access key or keep going and consume the A character from the _A1_ access key. In this case, the system will process only the first access key invocation reached by the automata. For the example, _A_ and _A1_, the system will only invoke the _A_ access key.
- 	When the user presses an invalid access key value in an access key sequence, nothing happens. There are two categories of keys considered as valid access keys in an access key sequence:
 - Special keys to exit the access key sequence: This is Esc, Alt, the arrow keys, Enter, and Tab.
 - The alphanumeric characters assigned to the access keys.

## Access key APIs

To support the access key user interaction, the XAML framework provides the APIs described here.

**AccessKeyManager**

The [AccessKeyManager](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.accesskeymanager.aspx) is a helper class that you can use to manage your UI when access keys are shown or hidden. The [IsDisplayModeEnabledChanged](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.accesskeymanager.isdisplaymodeenabledchanged.aspx) event is raised each time the app enters and exits from the access key sequence. You can query the [IsDisplayModeEnabled](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.accesskeymanager.isdisplaymodeenabled.aspx) property to determine whether the visual affordances are shown or hidden.  You can also call [ExitDisplayMode](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.accesskeymanager.exitdisplaymode.aspx) to force dismissal of an access key sequence.

> [!NOTE]
> There is no built-in implementation of the access key's visual; you have to provide it.  

**AccessKey**

The [AccessKey](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskey.aspx) property lets you specify an access key on a UIElement or [TextElement](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.documents.textelement.accesskey.aspx). If two elements have the same access key and the same scope, only the first element added to the visual tree will be processed.

To ensure the XAML Framework processes the access keys, the UI elements must be realized in the visual tree. If there are no elements in the visual tree with an access key, no access key events are raised.

Access key APIs don’t support characters that need two keystrokes to be generated. An individual character must correspond to a key on a particular language’s native keyboard layout.  

**AccessKeyDisplayRequested/Dismissed**

The [AccessKeyDisplayRequested](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskeydisplayrequested.aspx) and the [AccessKeyDisplayDismissed](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskeydisplaydismissed.aspx) events are raised when an access key visual affordance should be displayed or dismissed. These events are not raised for elements with their [Visibility](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.visibility.aspx) property set to **Collapsed**. The AccessKeyDisplayRequested event is raised during an access key sequence every time the user presses a character that is used by the access key. For example, if an access key is set to _AB_, this event is raised when the user presses Alt, and again when the user presses _A_. When user presses _B_, the AccessKeyDisplayDismissed event is raised

**AccessKeyInvoked**

The [AccessKeyInvoked](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskeyinvoked.aspx) event is raised when a user reaches the last character of an access key. An access key can have one or several characters. For example, for access keys _A_ and _BC_, when a user presses _Alt, A_, or _Alt,  B, C_, the event is raised, but not when the user presses just _Alt, B_. This event is raised when the key is pressed, not when it’s released.

**IsAccessKeyScope**

The [IsAccessKeyScope](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.isaccesskeyscope.aspx) property lets you specify that a UIElement is the root of an access key scope. The AccessKeyDisplayRequested event is raised for this element, but not for its children. When a user invokes this element, the XAML framework changes the scope automatically and raises the AccessKeyDisplayRequested event on its children and the AccessKeyDisplayDismissed event on other UI elements (including the parent).  The access key sequence is not exited when the scope is changed.

**AccessKeyScopeOwner**

To make an element participate in the scope of another element (the source) that is not its parent in the visual tree, you can set the [AccessKeyScopeOwner](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskeyscopeowner.aspx) property. The element bound to the AccessKeyScopeOwner property must have IsAccessKeyScope set to **true**. Otherwise, an exception is thrown.

**ExitDisplayModeOnAccessKeyInvoked**

By default, when an access key is invoked and the element is not a scope owner, the access key sequence is finalized and the [AccessKeyManager.IsDisplayModeEnabledChanged](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.accesskeymanager.isdisplaymodeenabledchanged.aspx) event is raised. You can set the [ExitDisplayModeOnAccessKeyInvoked](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.exitdisplaymodeonaccesskeyinvoked.aspx) property to **false** to override this behavior and prevent exiting from the access key sequence after its invoked. (This property is on both [UIElement](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.exitdisplaymodeonaccesskeyinvoked.aspx) and [TextElement](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.documents.textelement.exitdisplaymodeonaccesskeyinvoked.aspx)).

> [!NOTE]
> If the element is a scope owner (`IsAccessKeyScope="True"`), the app enters a new access key scope and the IsDisplayModeEnabledChanged event is not raised.

**Localization**

Access keys can be localized in multiple languages and loaded at runtime using the [ResourceLoader](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.resources.resourceloader.aspx) APIs.

## Control patterns used when an access key is invoked

Control patterns are interface implementations that expose common control functionality; for example, buttons implement the **Invoke** control pattern and this raises the **Click** event. When an access key is invoked, the XAML framework looks up whether the invoked element implements a control pattern and executes it if it does. If the element has more than one control pattern, only one is invoked, the rest are ignored. Control patterns are searched in the following order:

1.	Invoke. For example, a Button.
2.	Toggle. For example, a Checkbox.
3.	Selection. For example, a RadioButton.
4.	Expand/Collapse. For example, a ComboBox.

If a control pattern is not found, the access key invocation will appear as a no-op and a debug message is recorded to assist you in debugging this situation: "No automation patterns for this component found. Implement desired behavior in the event handler for AccessKeyInvoked. Setting Handled to true in your event handler will suppress this message."

> [!NOTE]
> The debugger's Application process type must be _Mixed (Managed and Native)_ or _Native_ in Visual Studio's Debug Settings to see this message.

If you do not want an access key to execute its default control pattern, or if the element does not have a control pattern, you should handle the [AccessKeyInvoked](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskeyinvoked.aspx) event and implement the desired behavior.
```csharp
private void OnAccessKeyInvoked(UIElement sender, AccessKeyInvokedEventArgs args)
{
    args.Handled = true;
    //Do something
}
```

For more info about control patterns, see [UI Automation Control Patterns Overview](https://msdn.microsoft.com/library/windows/desktop/ee671194.aspx).

## Access keys and Narrator

Windows Runtime has UI Automation providers that expose properties on Microsoft UI Automation elements. These properties enable UI Automation client applications to discover information about pieces of the user interface. The [AutomationProperties.AccessKey](https://msdn.microsoft.com/library/windows/apps/hh759763) property lets clients, such as Narrator, discover the access key associated with an element. Narrator will read this property every time an element gets focus. If AutomationProperties.AccessKey is does not have value, the XAML framework returns the [AccessKey](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.accesskey.aspx) property value from the UIElement or TextElement. You don't need to setup AutomationProperties.AccessKey if the AccessKey property already has a value.

## Example: Access key for button

This example shows how to create an access key for a Button. It uses Tooltips as a visual affordance to implement a floating badge that contains the access key.

> [!NOTE]
> Tooltip is used for simplicity, but we recommend that you create your own control to display it using, for example, [Popup](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.primitives.popup.aspx).

The XAML framework automatically calls the handler for the Click event, so you don't need to handle the AccessKeyInvoked event. The example provides visual affordances for only the characters that are remaining to invoke the access key by using the [AccessKeyDisplayRequestedEventArgs.PressedKeys](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.input.accesskeydisplayrequestedeventargs.pressedkeys.aspx) property. For example, if there are three displayed access keys: _A1_, _A2_, and _C_, and the user presses _A_, then only _A1_ and _A2_ access key are unfiltered, and are displayed as _1_ and _2_ instead of _A1_ and _A2_.

```xaml
<StackPanel
        VerticalAlignment="Center"
        HorizontalAlignment="Center"
        Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Button Content="Press"
                AccessKey="PB"
                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"
                Click="DoSomething" />
        <TextBlock Text="" x:Name="textBlock" />
    </StackPanel>
```

```csharp
 public sealed partial class ButtonSample : Page
    {
        public ButtonSample()
        {
            this.InitializeComponent();
        }

        private void DoSomething(object sender, RoutedEventArgs args)
        {
            textBlock.Text = "Access Key is working!";
        }

        private void OnAccessKeyDisplayRequested(UIElement sender, AccessKeyDisplayRequestedEventArgs args)
        {
            var tooltip = ToolTipService.GetToolTip(sender) as ToolTip;

            if (tooltip == null)
            {
                tooltip = new ToolTip();
                tooltip.Background = new SolidColorBrush(Windows.UI.Colors.Black);
                tooltip.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                tooltip.Padding = new Thickness(4, 4, 4, 4);
                tooltip.VerticalOffset = -20;
                tooltip.Placement = PlacementMode.Bottom;
                ToolTipService.SetToolTip(sender, tooltip);
            }

            if (string.IsNullOrEmpty(args.PressedKeys))
            {
                tooltip.Content = sender.AccessKey;
            }
            else
            {
                tooltip.Content = sender.AccessKey.Remove(0, args.PressedKeys.Length);
            }

            tooltip.IsOpen = true;
        }
        private void OnAccessKeyDisplayDismissed(UIElement sender, AccessKeyDisplayDismissedEventArgs args)
        {
            var tooltip = ToolTipService.GetToolTip(sender) as ToolTip;
            if (tooltip != null)
            {
                tooltip.IsOpen = false;
                //Fix to avoid show tooltip with mouse
                ToolTipService.SetToolTip(sender, null);
            }
        }
    }
```

## Example: Scoped access keys

This example shows how to create scoped access keys. The PivotItem’s IsAccessKeyScope property prevents the access keys of the PivotItem's child elements from showing when user presses Alt. These access keys are shown only when the user invokes the PivotItem because the XAML framework automatically switches the scope. The framework also hides the access keys of the other scopes.

This example also shows how to handle the AccessKeyInvoked event. The PivotItem doesn’t implement any control pattern, so the XAML framework doesn't invoke any action by default. This implementation shows how to select the PivotItem that was invoked using the access key.

Finally, the example shows the IsDisplayModeChanged event where you can do something when the display mode changes. In this example, the Pivot control is collapsed until the user presses Alt. When the user finishes interacting with the Pivot, it collapses again. You can use IsDisplayModeEnabled to check if the access key display mode is enabled or disabled.

```xaml   
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Pivot x:Name="MyPivot" VerticalAlignment="Center" HorizontalAlignment="Center" >
            <Pivot.Items>
                <PivotItem
                    x:Name="PivotItem1"
                    AccessKey="A"
                    AccessKeyInvoked="OnAccessKeyInvoked"
                    AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                    AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"
                    IsAccessKeyScope="True">
                    <PivotItem.Header>
                        <TextBlock Text="A Options"/>
                    </PivotItem.Header>
                    <StackPanel Orientation="Horizontal" >
                        <Button Content="ButtonAA" AccessKey="A"
                                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested" />
                        <Button Content="ButtonAD1" AccessKey="D1"
                                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"  />
                        <Button Content="ButtonAD2" AccessKey="D2"
                                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"/>
                    </StackPanel>
                </PivotItem>
                <PivotItem
                    x:Name="PivotItem2"
                    AccessKeyInvoked="OnAccessKeyInvoked"
                    AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                    AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"
                    AccessKey="B"
                    IsAccessKeyScope="true">
                    <PivotItem.Header>
                        <TextBlock Text="B Options"/>
                    </PivotItem.Header>
                    <StackPanel Orientation="Horizontal">
                        <Button AccessKey="B" Content="ButtonBB"
                                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"  />
                        <Button AccessKey="F1" Content="ButtonBF1"
                                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"  />
                        <Button AccessKey="F2" Content="ButtonBF2"  
                                AccessKeyDisplayDismissed="OnAccessKeyDisplayDismissed"
                                AccessKeyDisplayRequested="OnAccessKeyDisplayRequested"/>
                    </StackPanel>
                </PivotItem>
            </Pivot.Items>
        </Pivot>
    </Grid>
```

```csharp
public sealed partial class ScopedAccessKeys : Page
    {
        public ScopedAccessKeys()
        {
            this.InitializeComponent();
            AccessKeyManager.IsDisplayModeEnabledChanged += OnDisplayModeEnabledChanged;
            this.Loaded += OnLoaded;
        }

        void OnLoaded(object sender, object e)
        {
            //To let the framework discover the access keys, the elements should be realized
            //on the visual tree. If there are no elements in the visual
            //tree with access key, the framework won't raise the events.
            //In this sample, if you define the Pivot as collapsed on the constructor, the Pivot
            //will have a lazy loading and the access keys won't be enabled.
            //For this reason, we make it visible when creating the object
            //and we collapse it when we load the page.
            MyPivot.Visibility = Visibility.Collapsed;
        }

        void OnAccessKeyInvoked(UIElement sender, AccessKeyInvokedEventArgs args)
        {
            args.Handled = true;
            MyPivot.SelectedItem = sender as PivotItem;
        }
        void OnDisplayModeEnabledChanged(object sender, object e)
        {
            if (AccessKeyManager.IsDisplayModeEnabled)
            {
                MyPivot.Visibility = Visibility.Visible;
            }
            else
            {
                MyPivot.Visibility = Visibility.Collapsed;

            }
        }

        DependencyObject AdjustTarget(UIElement sender)
        {
            DependencyObject target = sender;
            if (sender is PivotItem)
            {
                PivotItem pivotItem = target as PivotItem;
                target = (sender as PivotItem).Header as TextBlock;
            }
            return target;
        }

        void OnAccessKeyDisplayRequested(UIElement sender, AccessKeyDisplayRequestedEventArgs args)
        {
            DependencyObject target = AdjustTarget(sender);
            var tooltip = ToolTipService.GetToolTip(target) as ToolTip;

            if (tooltip == null)
            {
                tooltip = new ToolTip();
                tooltip.Background = new SolidColorBrush(Windows.UI.Colors.Black);
                tooltip.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                tooltip.Padding = new Thickness(4, 4, 4, 4);
                tooltip.VerticalOffset = -20;
                tooltip.Placement = PlacementMode.Bottom;
                ToolTipService.SetToolTip(target, tooltip);
            }

            if (string.IsNullOrEmpty(args.PressedKeys))
            {
                tooltip.Content = sender.AccessKey;
            }
            else
            {
                tooltip.Content = sender.AccessKey.Remove(0, args.PressedKeys.Length);
            }

            tooltip.IsOpen = true;
        }
        void OnAccessKeyDisplayDismissed(UIElement sender, AccessKeyDisplayDismissedEventArgs args)
        {
            DependencyObject target = AdjustTarget(sender);

            var tooltip = ToolTipService.GetToolTip(target) as ToolTip;
            if (tooltip != null)
            {
                tooltip.IsOpen = false;
                //Fix to avoid show tooltip with mouse
                ToolTipService.SetToolTip(target, null);
            }
        }
    }
```
