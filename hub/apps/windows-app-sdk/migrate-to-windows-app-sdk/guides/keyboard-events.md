---
title: Keyboard API and events parity between UWP and WinUI 3
description: The migration of apps from UWP to the WinUI 3 might require a few changes in the way input keyboard events are handled.
ms.topic: article
ms.date: 09/16/2024
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, input, keyboard, events
ms.localizationpriority: medium
---

# Keyboard API and events parity between UWP and WinUI 3

The migration of apps from the Universal Windows Platform (UWP) to the Windows App SDK (specifically, WinUI 3) might require a few changes in the way input keyboard events are handled, primarily due to the app model differences. This topic describes commonly identified differences.

By using **UIElement**-specific WinUI 3 event handlers, and developing custom solutions for event routing, you can easily transition your apps to modern Windows applications.

## Mapping to WinUI 3 APIs from UWP APIs

### UWP APIs

In UWP, **CoreWindow** serves as the fundamental component attached directly to each application window, handling global input events across the application. In this topic, some of the commonly used events in conjunction with **CoreWindow** are discussed:

* **KeyDown**. Event raised by any key press when the app window has focus.
* **KeyUp**. Event raised when any key is released.
* **CharacterReceived**. Event raised when a new character is received by the input queue.
* **CoreDispatcher.AcceleratorKeyActivated**. Event raised when an accelerator key is activated (either pressed or held down).

```csharp
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        var coreWindow = CoreWindow.GetForCurrentThread();
        coreWindow.KeyDown += CoreWindow_KeyDown;
        coreWindow.KeyUp += CoreWindow_KeyUp;
        coreWindow.CharacterReceived += CoreWindow_CharacterReceived;
        coreWindow.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;
    }

    private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine($"KeyDown: {args.VirtualKey}");
    }
    private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine($"KeyUp: {args.VirtualKey}");
    }

    private void CoreWindow_CharacterReceived(CoreWindow sender, CharacterReceivedEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine($"CharacterReceived: {(char)args.KeyCode}");
    }

    private void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
    {
        if (args.EventType == CoreAcceleratorKeyEventType.KeyDown)
        {
            bool isCtrlPressed = (Window.Current.CoreWindow.GetKeyState(VirtualKey.Control) & CoreVirtualKeyStates.Down) ==
            CoreVirtualKeyStates.Down;
            if (isCtrlPressed && args.VirtualKey == VirtualKey.C)
            {
                System.Diagnostics.Debug.WriteLine("Ctrl+C Pressed");
            }
            else if (args.VirtualKey == VirtualKey.F1)
            {
                System.Diagnostics.Debug.WriteLine("F1 Key Pressed");
            }
        }
    }
}
```

### WinUI 3 APIs (Windows App SDK)

WinUI 3 (Windows App SDK) has a different concept from **CoreWindow**. Instead, WinUI 3 provides an input event handling mechanism using **Microsoft.Ui.Xaml.UIElement** (UIElement). Each class that inherits from **UIElement** can implement various keyboard or pointer events (which are also present in UWP's **CoreWindow**) such as **KeyUp**, **KeyDown**, **CharacterReceived**, and others. This approach allows event handling at the level of a specific UI control.

For detailed info about event handling, see [Keyboard events](/windows/apps/design/input/keyboard-events). You can associate keyboard events either XAML or in imperative source code. These examples demonstrate key event handling through code and accelerator key events in XAML.

```xaml
<Window
    x:Class="SampleApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:SampleApp"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <StackPanel Orientation="Horizontal" x:Name="stackPanelControl" HorizontalAlignment="Center" VerticalAlignment="Center">
        <Button x:Name="myButton" Click="myButton_Click">Click Me</Button>
        <StackPanel.KeyboardAccelerators>
            <KeyboardAccelerator Key="C" Modifiers="Control" Invoked="OnCtrlCInvoked"/>
        </StackPanel.KeyboardAccelerators>
    </StackPanel>
</Window>
```

```csharp
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        stackPanelControl.KeyDown += OnKeyDownHandler;
        stackPanelControl.KeyUp += OnKeyUpHandler;
        stackPanelControl.CharacterReceived += OnCharacterReceivedHandler;
    }

    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        myButton.Content = "Clicked";
    }

    private void OnKeyDownHandler(object sender, KeyRoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"KeyDown: {e.Key}");
    }

    private void OnKeyUpHandler(object sender, KeyRoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"KeyUp: {e.Key}");
    }

    private void OnCharacterReceivedHandler(UIElement sender, CharacterReceivedRoutedEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine($"CharacterReceived: {(char)args.Character}");
    }

    private void OnCtrlCInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        if (args.KeyboardAccelerator.Key == VirtualKey.C && args.KeyboardAccelerator.Modifiers == VirtualKeyModifiers.Control)
        {
            System.Diagnostics.Debug.WriteLine("Ctrl+C Pressed");
            args.Handled = true;
        }
    }
}
```

## UWP sample apps for keyboard events

* [UWP custom edit control sample app](https://github.com/microsoft/Windows-universal-samples/blob/0db108e9d0af7386767194b3293c4082c1c8daa7/Samples/CustomEditControl/cs/CustomEditControl.xaml.cs#L55).
* [UWP basic suspension sample app](https://github.com/microsoft/Windows-universal-samples/blob/0db108e9d0af7386767194b3293c4082c1c8daa7/Samples/BasicSuspension/cs/Common/NavigationHelper.cs#L194).

## WinUI 3 sample apps for keyboard events and KeyboardAccelerator

* [Input sample apps: KeyboardShortcutManager.xaml](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Input/cs-winui/KeyboardShortcutManager.xaml)
* [Input sample apps: KeyboardShortcutManager.xaml.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Input/cs-winui/KeyboardShortcutManager.xaml.cs)

## See also

### UWP APIs

* [CoreWindow class (Windows.UI.Core)](/uwp/api/windows.ui.core.corewindow)
* [KeyboardAccelerator class (Windows.UI.Xaml.Input)](/uwp/api/windows.ui.xaml.input.keyboardaccelerator)
* [UIElement.CharacterReceived event (Windows.UI.Xaml)](/uwp/api/windows.ui.xaml.uielement.characterreceived)
* [UIElement.KeyDown event (Windows.UI.Xaml)](/uwp/api/windows.ui.xaml.uielement.keydown)
* [UIElement.KeyUp event (Windows.UI.Xaml)](/uwp/api/windows.ui.xaml.uielement.keyup)

### WinUI 3 APIs (Windows App SDK)

* [KeyboardAccelerator class (Microsoft.UI.Xaml.Input)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.input.keyboardaccelerator)
* [UIElement.CharacterReceived event (Microsoft.UI.Xaml)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.characterreceived)
* [UIElement.KeyDown event (Microsoft.UI.Xaml)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.keydown)
* [UIElement.KeyUp event (Microsoft.UI.Xaml)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.keyup)
