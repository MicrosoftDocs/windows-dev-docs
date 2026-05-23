---
description: This article shows you how to enumerate MIDI (Musical Instrument Digital Interface) devices and send and receive MIDI messages from a WinUI app.
title: MIDI
ms.date: 05/07/2026
ms.topic: article
keywords: windows, winui, midi, musical instrument digital interface
ms.localizationpriority: medium
---
# MIDI

This article shows you how to enumerate MIDI (Musical Instrument Digital Interface) devices and send and receive MIDI messages from a WinUI app. Windows supports MIDI over USB (class-compliant and most proprietary drivers), MIDI over Bluetooth LE, and through freely-available third-party products, MIDI over Ethernet and routed MIDI.

## Create a device watcher helper class

The [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) namespace provides the [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) which can notify your app if devices are added or removed from the system, or if the information for a device is updated. Since MIDI-enabled apps typically are interested in both input and output devices, this example creates a helper class that implements the **DeviceWatcher** pattern, so that the same code can be used for both MIDI input and MIDI output devices, without the need for duplication.

Add a new class to your project to serve as your device watcher. In this example the class is named **MidiDeviceWatcher**. The rest of the code in this section is used to implement the helper class.

Add some member variables to the class:

-   A [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) object that will monitor for device changes.
-   A device selector string that will contain the MIDI in port selector string for one instance and the MIDI out port selector string for another instance.
-   A [**ListBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listbox) control that will be populated with the names of the available devices.
-   A [**DispatcherQueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) that is required to update the UI from a thread other than the UI thread.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetWatcherVariables":::

Add a [**DeviceInformationCollection**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationCollection) property that is used to access the current list of devices from outside the helper class.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetDeclareDeviceInformationCollection":::

In the class constructor, the caller passes in the MIDI device selector string, the **ListBox** for listing the devices, and the **DispatcherQueue** needed to update the UI.

Call [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher) to create a new instance of the **DeviceWatcher** class, passing in the MIDI device selector string.

Register handlers for the watcher's event handlers.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetWatcherConstructor":::

The **DeviceWatcher** has the following events:

-   [**Added**](/uwp/api/windows.devices.enumeration.devicewatcher.added) - Raised when a new device is added to the system.
-   [**Removed**](/uwp/api/windows.devices.enumeration.devicewatcher.removed) - Raised when a device is removed from the system.
-   [**Updated**](/uwp/api/windows.devices.enumeration.devicewatcher.updated) - Raised when the information associated with an existing device is updated.
-   [**EnumerationCompleted**](/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted) - Raised when the watcher has completed its enumeration of the requested device type.

In the event handler for each of these events, a helper method, **UpdateDevices**, is called to update the **ListBox** with the current list of devices. Because **UpdateDevices** updates UI elements and these event handlers are not called on the UI thread, each call must be wrapped in a call to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetWatcherEventHandlers":::

The **UpdateDevices** helper method calls [**DeviceInformation.FindAllAsync**](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) and updates the **ListBox** with the names of the returned devices as described previously in this article.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetWatcherUpdateDevices":::

Add methods to start the watcher, using the **DeviceWatcher** object's [**Start**](/uwp/api/windows.devices.enumeration.devicewatcher.start) method, and to stop the watcher, using the [**Stop**](/uwp/api/windows.devices.enumeration.devicewatcher.stop) method.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetWatcherStopStart":::

Provide a destructor to unregister the watcher event handlers and set the device watcher to null.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MidiDeviceWatcher.cs" id="SnippetWatcherDestructor":::

## Create MIDI ports to send and receive messages

In the code behind for your window, declare member variables to hold two instances of the **MidiDeviceWatcher** helper class, one for input devices and one for output devices.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetDeclareDeviceWatchers":::

Also declare member variables for the MIDI input and output port objects.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetDeclareMidiPorts":::

Create a new instance of the watcher helper classes, passing in the device selector string, the **ListBox** to be populated, and the **DispatcherQueue** object. Then, call the method to start each object's **DeviceWatcher**.

Shortly after each **DeviceWatcher** is started, it will finish enumerating the current devices connected to the system and raise its [**EnumerationCompleted**](/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted) event, which will cause each **ListBox** to be updated with the current MIDI devices.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetStartWatchers":::

When the user selects an item in the MIDI input **ListBox**, the [**SelectionChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectionchanged) event is raised. In the handler for this event, access the **DeviceInformationCollection** property of the helper class to get the current list of devices. If there are entries in the list, select the **DeviceInformation** object with the index corresponding to the **ListBox** control's [**SelectedIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectedindex).

Create the [**MidiInPort**](/uwp/api/Windows.Devices.Midi.MidiInPort) object representing the selected input device by calling [**MidiInPort.FromIdAsync**](/uwp/api/windows.devices.midi.midiinport.fromidasync), passing in the [**Id**](/uwp/api/windows.devices.enumeration.deviceinformation.id) property of the selected device.

Register a handler for the [**MessageReceived**](/uwp/api/windows.devices.midi.midiinport.messagereceived) event, which is raised whenever a MIDI message is received through the specified device.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetInPortSelectionChanged":::

When the **MessageReceived** handler is called, the message is contained in the [**Message**](/uwp/api/Windows.Devices.Midi.MidiMessageReceivedEventArgs) property of the **MidiMessageReceivedEventArgs**. The [**Type**](/uwp/api/windows.devices.midi.imidimessage.type) of the message object is a value from the [**MidiMessageType**](/uwp/api/Windows.Devices.Midi.MidiMessageType) enumeration indicating the type of message that was received. The data of the message depends on the type of the message. This example checks to see if the message is a note on message and, if so, outputs the midi channel, note, and velocity of the message.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetMessageReceived":::

The [**SelectionChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectionchanged) handler for the output device **ListBox** works the same as the handler for input devices, except no event handler is registered.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetOutPortSelectionChanged":::

Once the output device is created, you can send a message by creating a new [**IMidiMessage**](/uwp/api/Windows.Devices.Midi.IMidiMessage) for the type of message you want to send. In this example, the message is a [**NoteOnMessage**](/uwp/api/Windows.Devices.Midi.MidiNoteOnMessage). The [**SendMessage**](/uwp/api/windows.devices.midi.imidioutport.sendmessage) method of the [**IMidiOutPort**](/uwp/api/Windows.Devices.Midi.IMidiOutPort) object is called to send the message.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetSendMessage":::

When your app is closing, be sure to clean up your app's resources. Unregister your event handlers and set the MIDI in port and out port objects to null. Stop the device watchers and set them to null.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/midi-winui/cs/MidiWinUI/MainWindow.xaml.cs" id="SnippetCleanUp":::

## Using the built-in Windows General MIDI synth

When you enumerate output MIDI devices using the technique described above, your app will discover a MIDI device called "Microsoft GS Wavetable Synth". This is a built-in General MIDI synthesizer that you can play from your app.

The UWP Extension SDK for General MIDI ("Microsoft General MIDI DLS for Universal Windows Apps") is not available in WinUI 3 projects. The old **Add reference > Extensions** dialog was specific to UWP. However, for most desktop scenarios, the GS Wavetable Synth works without the extension SDK because desktop apps can access the system's `gm.dls` sound bank directly. If you find that MIDI output produces no sound, verify that a MIDI output device is selected and that your audio output is configured correctly.

## Related topics

* [Windows.Devices.Midi namespace](/uwp/api/Windows.Devices.Midi)
