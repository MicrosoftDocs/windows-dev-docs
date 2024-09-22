---
title: Keyboard API and events parity between UWP and Windows App SDK
description: The migration of apps from UWP to the WinUI 3 might require a few changes in the way input keyboard events are handled.
ms.topic: article
ms.date: 09/16/2024
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, input, keyboard, events
ms.localizationpriority: medium
---

# Keyboard API and events parity between UWP and Windows App SDK

The migration of apps from the Universal Windows Platform (UWP) to the Windows App SDK (specifically, WinUI 3) might require a few changes in the way input keyboard events are handled, primarily due to the app model differences. This topic describes commonly identified differences.

By using **UIElement**-specific WinUI 3 event handlers, and developing custom solutions for event routing, you can easily transition your apps to modern Windows applications.

## Mapping to Windows App SDK APIs for UWP APIs

### UWP APIs

In UWP, **CoreWindow** serves as the fundamental component attached directly to each application window, handling global input events across the application. In this topic, some of the commonly used events in conjunction with **CoreWindow** are discussed:

* **KeyDown**. Event raised by any key press when the app window has focus.
* **KeyUp**. Event raised when any key is released.
* **CharacterReceived**. Event raised when a new character is received by the input queue.
* **CoreDispatcher.AcceleratorKeyActivated**. Event raised when an accelerator key is activated (either pressed or held down).

### Windows App SDK APIs (WinUI 3)

The Windows App SDK (WinUI 3) has a different concept from **CoreWindow**. Instead, WinUI 3 provides an input event handling mechanism using **Microsoft.Ui.Xaml.UIElement** (UIElement). Each class that inherits from **UIElement** can implement various keyboard or pointer events (which are also present in UWP's **CoreWindow**) such as **KeyUp**, **KeyDown**, **CharacterReceived**, and others. This approach allows event handling at the level of a specific UI control.

## UWP sample apps for keyboard events and KeyboardAccelerator

* [UWP custom edit control sample app](https://github.com/microsoft/Windows-universal-samples/blob/0db108e9d0af7386767194b3293c4082c1c8daa7/Samples/CustomEditControl/cs/CustomEditControl.xaml.cs#L55).
* [UWP basic suspension sample app](https://github.com/microsoft/Windows-universal-samples/blob/0db108e9d0af7386767194b3293c4082c1c8daa7/Samples/BasicSuspension/cs/Common/NavigationHelper.cs#L194).
* [UWP sample apps containing 'AcceleratorKeyEventArgs'](https://github.com/search?q=repo%3Amicrosoft%2FWindows-universal-samples+AcceleratorKeyEventArgs+&type=code).

## Windows App SDK sample apps for keyboard events and KeyboardAccelerator

* [Input sample apps: KeyboardShortcutManager.xaml](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Input/cs-winui/KeyboardShortcutManager.xaml)
* [Input sample apps: KeyboardShortcutManager.xaml.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Input/cs-winui/KeyboardShortcutManager.xaml.cs)

## See also

### UWP APIs

* [CoreWindow class (Windows.UI.Core)](/uwp/api/windows.ui.core.corewindow)
* [KeyboardAccelerator class (Windows.UI.Xaml.Input)](/uwp/api/windows.ui.xaml.input.keyboardaccelerator)
* [UIElement.CharacterReceived event (Windows.UI.Xaml)](/uwp/api/windows.ui.xaml.uielement.characterreceived)
* [UIElement.KeyDown event (Windows.UI.Xaml)](/uwp/api/windows.ui.xaml.uielement.keydown)
* [UIElement.KeyUp event (Windows.UI.Xaml)](/uwp/api/windows.ui.xaml.uielement.keyup)

### Windows App SDK APIs (WinUI 3)

* [KeyboardAccelerator class (Microsoft.UI.Xaml.Input)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.input.keyboardaccelerator)
* [UIElement.CharacterReceived event (Microsoft.UI.Xaml)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.characterreceived)
* [UIElement.KeyDown event (Microsoft.UI.Xaml)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.keydown)
* [UIElement.KeyUp event (Microsoft.UI.Xaml)](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.keyup)
