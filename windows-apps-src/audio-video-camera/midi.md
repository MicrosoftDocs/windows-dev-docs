---
ms.assetid: 9146212C-8480-4C16-B74C-D7F08C7086AF
description: This article shows you how to enumerate MIDI (Musical Instrument Digital Interface) devices and send and receive MIDI messages from a Universal Windows app.
title: MIDI
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# MIDI



This article shows you how to enumerate MIDI (Musical Instrument Digital Interface) devices and send and receive MIDI messages from a Universal Windows app. Windows 10 supports MIDI over USB (class-compliant and most proprietary drivers), MIDI over Bluetooth LE (Windows 10 Anniversary Edition and later), and through freely-available third-party products, MIDI over Ethernet and routed MIDI.

## Enumerate MIDI devices

Before enumerating and using MIDI devices, add the following namespaces to your project.

[!code-cs[Using](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetUsing)]

Add a [**ListBox**](https://msdn.microsoft.com/library/windows/apps/br242868) control to your XAML page that will allow the user to select one of the MIDI input devices attached to the system. Add another one to list the MIDI output devices.

[!code-xml[MidiListBoxes](./code/MIDIWin10/cs/MainPage.xaml#SnippetMidiListBoxes)]

The [**FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/br225432) method [**DeviceInformation**](https://msdn.microsoft.com/library/windows/apps/br225393) class is used to enumerate many different types of devices that are recognized by Windows. To specify that you only want the method to find MIDI input devices, use the selector string returned by [**MidiInPort.GetDeviceSelector**](https://msdn.microsoft.com/library/windows/apps/dn894779). **FindAllAsync** returns a [**DeviceInformationCollection**](https://msdn.microsoft.com/library/windows/apps/br225395) that contains a **DeviceInformation** for each MIDI input device registered with the system. If the returned collection contains no items, then there are no available MIDI input devices. If there are items in the collection, loop through the **DeviceInformation** objects and add the name of each device to the MIDI input device **ListBox**.

[!code-cs[EnumerateMidiInputDevices](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetEnumerateMidiInputDevices)]

Enumerating MIDI output devices works the exact same way as enumerating input devices, except that you should specify the selector string returned by [**MidiOutPort.GetDeviceSelector**](https://msdn.microsoft.com/library/windows/apps/dn894845) when calling **FindAllAsync**.

[!code-cs[EnumerateMidiOutputDevices](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetEnumerateMidiOutputDevices)]



## Create a device watcher helper class

The [**Windows.Devices.Enumeration**](https://msdn.microsoft.com/library/windows/apps/br225459) namespace provides the [**DeviceWatcher**](https://msdn.microsoft.com/library/windows/apps/br225446) which can notify your app if devices are added or removed from the system, or if the information for a device is updated. Since MIDI-enabled apps typically are interested in both input and output devices, this example creates a helper class that implements the **DeviceWatcher** pattern, so that the same code can be used for both MIDI input and MIDI output devices, without the need for duplication.

Add a new class to your project to serve as your device watcher. In this example the class is named **MyMidiDeviceWatcher**. The rest of the code in this section is used to implement the helper class.

Add some member variables to the class:

-   A [**DeviceWatcher**](https://msdn.microsoft.com/library/windows/apps/br225446) object that will monitor for device changes.
-   A device selector string that will contain the MIDI in port selector string for one instance and the MIDI out port selector string for another instance.
-   A [**ListBox**](https://msdn.microsoft.com/library/windows/apps/br242868) control that will be populated with the names of the available devices.
-   A [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) that is required to update the UI from a thread other than the UI thread.

[!code-cs[WatcherVariables](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetWatcherVariables)]

Add a [**DeviceInformationCollection**](https://msdn.microsoft.com/library/windows/apps/br225395) property that is used to access the current list of devices from outside the helper class.

[!code-cs[DeclareDeviceInformationCollection](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetDeclareDeviceInformationCollection)]

In class constructor, the caller passes in the MIDI device selector string, the **ListBox** for listing the devices, and the **Dispatcher** needed to update the UI.

Call [**DeviceInformation.CreateWatcher**](https://msdn.microsoft.com/library/windows/apps/br225427) to create a new instance of the **DeviceWatcher** class, passing in the MIDI device selector string.

Register handlers for the watcher's event handlers.

[!code-cs[WatcherConstructor](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetWatcherConstructor)]

The **DeviceWatcher** has the following events:

-   [**Added**](https://msdn.microsoft.com/library/windows/apps/br225450) - Raised when a new device is added to the system.
-   [**Removed**](https://msdn.microsoft.com/library/windows/apps/br225453) - Raised when a device is removed from the system.
-   [**Updated**](https://msdn.microsoft.com/library/windows/apps/br225458) - Raised when the information associated with an existing device is updated.
-   [**EnumerationCompleted**](https://msdn.microsoft.com/library/windows/apps/br225451) - Raised when the watcher has completed its enumeration of the requested device type.

In the event handler for each of these events, a helper method, **UpdateDevices**, is called to update the **ListBox** with the current list of devices. Because **UpdateDevices** updates UI elements and these event handlers are not called on the UI thread, each call must be wrapped in a call to [**RunAsync**](https://msdn.microsoft.com/library/windows/apps/hh750317), which causes the specified code to be run on the UI thread.

[!code-cs[WatcherEventHandlers](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetWatcherEventHandlers)]

The **UpdateDevices** helper method calls [**DeviceInformation.FindAllAsync**](https://msdn.microsoft.com/library/windows/apps/br225432) and updates the **ListBox** with the names of the returned devices as described previously in this article.

[!code-cs[WatcherUpdateDevices](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetWatcherUpdateDevices)]

Add methods to start the watcher, using the **DeviceWatcher** object's [**Start**](https://msdn.microsoft.com/library/windows/apps/br225454) method, and to stop the watcher, using the [**Stop**](https://msdn.microsoft.com/library/windows/apps/br225456) method.

[!code-cs[WatcherStopStart](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetWatcherStopStart)]

Provide a destructor to unregister the watcher event handlers and set the device watcher to null.

[!code-cs[WatcherDestructor](./code/MIDIWin10/cs/MyMidiDeviceWatcher.cs#SnippetWatcherDestructor)]

## Create MIDI ports to send and receive messages

In the code behind for your page, declare member variables to hold two instances of the **MyMidiDeviceWatcher** helper class, one for input devices and one for output devices.

[!code-cs[DeclareDeviceWatchers](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetDeclareDeviceWatchers)]

Create a new instance of the watcher helper classes, passing in the device selector string, the **ListBox** to be populated, and the **CoreDispatcher** object that can be accessed through the page's **Dispatcher** property. Then, call the method to start each object's **DeviceWatcher**.

Shortly after each **DeviceWatcher** is started, it will finish enumerating the current devices connected to the system and raise its [**EnumerationCompleted**](https://msdn.microsoft.com/library/windows/apps/br225451) event, which will cause each **ListBox** to be updated with the current MIDI devices.

[!code-cs[StartWatchers](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetStartWatchers)]

When the user selects an item in the MIDI input **ListBox**, the [**SelectionChanged**](https://msdn.microsoft.com/library/windows/apps/br209776) event is raised. In the handler for this event, access the **DeviceInformationCollection** property of the helper class to get the current list of devices. If there are entries in the list, select the **DeviceInformation** object with the index corresponding to the **ListBox** control's [**SelectedIndex**](https://msdn.microsoft.com/library/windows/apps/br209768).

Create the [**MidiInPort**](https://msdn.microsoft.com/library/windows/apps/dn894770) object representing the selected input device by calling [**MidiInPort.FromIdAsync**](https://msdn.microsoft.com/library/windows/apps/dn894776), passing in the [**Id**](https://msdn.microsoft.com/library/windows/apps/br225437) property of the selected device.

Register a handler for the [**MessageReceived**](https://msdn.microsoft.com/library/windows/apps/dn894781) event, which is raised whenever a MIDI message is received through the specified device.

[!code-cs[DeclareMidiPorts](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetDeclareMidiPorts)]

[!code-cs[InPortSelectionChanged](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetInPortSelectionChanged)]

When the **MessageReceived** handler is called, the message is contained in the [**Message**](https://msdn.microsoft.com/library/windows/apps/dn894783) property of the **MidiMessageReceivedEventArgs**. The [**Type**](https://msdn.microsoft.com/library/windows/apps/dn894726) of the message object is a value from the [**MidiMessageType**](https://msdn.microsoft.com/library/windows/apps/dn894786) enumeration indicating the type of message that was received. The data of the message depends on the type of the message. This example checks to see if the message is a note on message and, if so, outputs the midi channel, note, and velocity of the message.

[!code-cs[MessageReceived](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetMessageReceived)]

The [**SelectionChanged**](https://msdn.microsoft.com/library/windows/apps/br209776) handler for the output device **ListBox** works the same as the handler for input devices, except no event handler is registered.

[!code-cs[OutPortSelectionChanged](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetOutPortSelectionChanged)]

Once the output device is created, you can send a message by creating a new [**IMidiMessage**](https://msdn.microsoft.com/library/windows/apps/dn911508) for the type of message you want to send. In this example, the message is a [**NoteOnMessage**](https://msdn.microsoft.com/library/windows/apps/dn894817). The [**SendMessage**](https://msdn.microsoft.com/library/windows/apps/dn894730) method of the [**IMidiOutPort**](https://msdn.microsoft.com/library/windows/apps/dn894727) object is called to send the message.

[!code-cs[SendMessage](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetSendMessage)]

When your app is deactivated, be sure to clean up your apps resources. Unregister your event handlers and set the MIDI in port and out port objects to null. Stop the device watchers and set them to null.

[!code-cs[CleanUp](./code/MIDIWin10/cs/MainPage.xaml.cs#SnippetCleanUp)]

## Using the built-in Windows General MIDI synth

When you enumerate output MIDI devices using the technique described above, your app will discover a MIDI device called "Microsoft GS Wavetable Synth". This is a built-in General MIDI synthesizer that you can play from your app. However, attempting to create a MIDI outport to this device will fail unless you have included the SDK extension for the built-in synth in your project.

**To include the General MIDI Synth SDK extension in your app project**

1.  In **Solution Explorer**, under your project, right-click **References** and select **Add reference...**
2.  Expand the **Universal Windows** node.
3.  Select **Extensions**.
4.  From the list of extensions, select **Microsoft General MIDI DLS for Universal Windows Apps**.
    > [!NOTE] 
	> If there are multiple versions of the extension, be sure to select the version that matches the target for your app. You can see which SDK version your app is targeting on the **Application** tab of the project Properties.

 

 




