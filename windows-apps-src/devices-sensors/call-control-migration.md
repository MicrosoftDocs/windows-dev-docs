---
title: Call Control Migration
description: #Required; article description that is displayed in search results. 
author: mamoodyb #Required; your GitHub user alias, with correct capitalization.
ms.author: mamoodyb #Required; microsoft alias of author; optional team alias.
ms.service: #Required; service per approved list. slug assigned by ACOM.
ms.topic: conceptual #Required; leave this attribute/value as-is.
ms.date: 12/6/2021 #Required; mm/dd/yyyy format.
ms.custom: template-concept #Required; leave this attribute/value as-is.
---

<!--Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

<!--
This template provides the basic structure of a concept article.
See the [concept guidance](contribute-how-write-concept.md) in the contributor guide.

To provide feedback on this template contact 
[the templates workgroup](mailto:templateswg@microsoft.com).
-->

<!-- 1. H1
Required. Set expectations for what the content covers, so customers know the 
content meets their needs. Should NOT begin with a verb.
-->

# Migration from system to app hosted call control experience

<!-- 2. Introductory paragraph 
Required. Lead with a light intro that describes what the article covers. Answer the 
fundamental “why would I want to know this?” question. Keep it short.
-->

In Windows 11, new APIs became available offering apps the ability to host their own calling UX. We want Windows calling developers to be able to build, innovate, and differentiate their own calling experiences. With the upcoming Windows 2022 release (Fall 2022), Windows Call Control will no longer be supported as a system experience. This document is intended to provide an overview of the key changes to Windows Call Control found in the new APIs including general guidance to developers and code snippets.

<!-- 3. H2s
Required. Give each H2 a heading that sets expectations for the content that follows. 
Follow the H2 headings with a sentence about how the section contributes to the whole.
-->

## General Guidance
To determine if the app is running on a Windows release which no longer supports system call control:

App needs to query the API contract ([Windows.ApplicationModel.Calls.CallsPhoneContract](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.callsphonecontract?view=winrt-22000)) with version 7 (via [ApiInformation.IsApiContractPresent](https://docs.microsoft.com/en-us/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent?view=winrt-20348) Method) to determine if it’s running on a Windows release which no longer supports system call-control experience, and if so, app should notify users accordingly for example, to update to a newer version that supports in-app call-control.

Apps must opt in to host in-app call control experience:
•	App needs to register [IncomingCallNotification](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.background.phonetriggertype?view=winrt-22000) phone trigger to receive incoming call notification to notify the user.
•	App needs to declare in appxmanifest file "windows.phonecallactivation“ as an uap13:Extension Category; otherwise, the app cannot be activated to host call progress.

Notes for existing apps using the system call control experience:
•	[CallOrigin](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.provider?view=winrt-insider) related APIs and [EnableTextReply](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phoneline.enabletextreply?view=winrt-insider) APIs will no longer work after deprecation.
•	The [CallOrigin](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.background.phonetriggertype?view=winrt-insider) & [IncomingCallDismissed](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.background.phonetriggertype?view=winrt-22000&viewFallbackFrom=winrt-insider) phone triggers will no longer be fired after deprecation.
•	Apps should register [PhoneLineWatcher.LineRemoved](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinewatcher.lineremoved?view=winrt-20348) Event  so that if the app has not declared "windows.phonecallactivation“ as an uap13:Extension Category in the appxmanifest file, it will get notified of hands-free line registration being removed after upgrading to a new Windows build that no longer supports system call control. This will allow the app to notify users explicitly in a timely fashion.

## Handling HFP Device Registration and PhoneLineTransport Devices
Handling HFP Device Registration and PhoneLineTransport Devices
Hands-free calling capable Bluetooth devices are represented and accessed through a [PhoneLineTransportDevice](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice?view=winrt-22000) object. This allows your app to register the device for calling, and to handle [AudioRoutingStatusChanged](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice.audioroutingstatuschanged?view=winrt-22000) and [InBandRingingEnabledChanged](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice.inbandringingenabledchanged?view=winrt-22000) events.

Your app should use a [DeviceWatcher](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.background.devicewatchertrigger?view=winrt-22000) to monitor relevant device additions, removals, and updates. The result of [PhoneLineTransportDevice.GetDeviceSelector](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice.getdeviceselector?view=winrt-22000)(PhoneLineTransport.Bluetooth) should be passed as the DeviceClass parameter to DeviceInformation.CreateWatcher.

On a device addition, your app should:
1.	Retrieve a [PhoneLineTransportDevice](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice?view=winrt-22000) object using the device ID via [PhoneLineTransportDevice.FromId](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice.fromid?view=winrt-22000#Windows_ApplicationModel_Calls_PhoneLineTransportDevice_FromId_System_String_)
2.	Call [RequestAccessAsync](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice.requestaccessasync?view=winrt-22000) on the transport device
3.	If access is allowed, start tracking the new PhoneLineTransportDevice and prompt the user to enable calling for (register) the device.
On a device update, your app should:
1.	Retrieve the PhoneLineTransportDevice object using the device ID via PhoneLineTransportDevice.FromId
2.	Check if the PhoneLineTransportDevice is still registered and prompt the user to re-enable calling for (register) the device if needed.
On a device removal, your app should:
1.	Stop tracking the PhoneLineTransportDevice and prompt the user to re-enable calling for (register) the device
(see code sample)

```c#
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

public sealed partial class Scenario_Client : Page
{
    private MainPage rootPage = MainPage.Current;

    private DeviceWatcher deviceWatcher = null;

    private ObservableCollection<PhoneLineTransportDevice> transportDevices =
        new ObservableCollection<PhoneLineTransportDevice>();

    public Scenario_Client()
    {
        // Initialize components
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
         // Use Windows.Devices.Enumeration.DeviceWatcher, so app can get notified when 
         // the BT hands-free device is added (paired), removed (un-paired), and updated 
         // (disconnected, connected, PhoneLineTransportDevice::UnregisterApp is called, or 
         // the app revoke the PhoneCall capability).
         // When the BT device is added, App is ready to register the device for HF-calling.
         // When the BT device is removed, App needs to re-register the device for HF-calling.
         // When the BT device is disconnected, App cannot make or receive HF calling. If there was
         // a call in progress when the BT device is disconnected, the call audio will push back to
         // the phone to continue the call.
        StartDeviceWatcher();
    }

    protected override async void OnNavigatedFrom(NavigationEventArgs e)
    {
        StopDeviceWatcher();
    }

    private void StartDeviceWatcher()
    {
        deviceWatcher = DeviceInformation.CreateWatcher(
            PhoneLineTransportDevice.GetDeviceSelector(PhoneLineTransport.Bluetooth));

        // Register event handlers before starting the watcher.
        deviceWatcher.Added += DeviceWatcher_Added;
        deviceWatcher.Updated += DeviceWatcher_Updated;
        deviceWatcher.Removed += DeviceWatcher_Removed;

        // Start the watcher.
        deviceWatcher.Start();
    }    

    private void StopDeviceWatcher()
    {
        if (deviceWatcher != null)
        {
            // Unregister the event handlers.
            deviceWatcher.Added -= DeviceWatcher_Added;
            deviceWatcher.Updated -= DeviceWatcher_Updated;
            deviceWatcher.Removed -= DeviceWatcher_Removed;

            // Stop the watcher.
            deviceWatcher.Stop();
            deviceWatcher = null;
        }
    }

    private PhoneLineTransportDevice FindPhoneLineTransportDevice(string id)
    {
        foreach (PhoneLineTransportDevice transportDevice in transportDevices)
        {
            if (transportDevice.DeviceId == id)
            {
                return transportDevice;
            }
        }
        return null;
    }

    private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation deviceInfo)
    {
        // We must update the collection on the UI thread because the collection is databound 
        // to a UI element.
        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
        {
            Debug.WriteLine(String.Format("Added {0}{1}", deviceInfo.Id, deviceInfo.Name));

            // Protect against race condition if the task runs after the app stopped the deviceWatcher.
            if (sender != deviceWatcher)
            {
                return;
            }

            // Make sure device isn't already present in the list.
            if (FindPhoneLineTransportDevice(deviceInfo.Id) != null)
            {
                return;
            }

            PhoneLineTransportDevice transportDevice = PhoneLineTransportDevice.FromId(deviceInfo.Id);
            if(transportDevice == null)
            {
                return;
            }

            DeviceAccessStatus accessStatus = await transportDevice.RequestAccessAsync();
            if(accessStatus == DeviceAccessStatus.Allowed)
            {
                lock (this)
                {
                    transportDevices.Add(transportDevice);

                    // ...
                    // Update UX to indicate a new device is added; and enable “Activate calling”
                    // button to allow user to register the device (phone) for HF calling – 
                    // demonstrated in example 2.2
                    // ...
                }
            }
        });
    }

    private async void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
    {
        // We must update the collection on the UI thread because the collection is databound 
        // to a UI element.
        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            lock (this)
            {
                Debug.WriteLine(String.Format("Updated {0}{1}", deviceInfoUpdate.Id, ""));

                // Protect against race condition if the task runs after the app stopped the 
                // deviceWatcher.
                if (sender != deviceWatcher)
                {
                    return;
                }

                PhoneLineTransportDevice transportDevice = FindPhoneLineTransportDevice(deviceInfoUpdate.Id);
                if (transportDevice != null)
                {
                    // ...
                    // Iterate through changed properties
                    // ...

                    // ...
                    // Update UX e.g. if (transportDevice.IsRegistered() == false), UX needs to 
                    // notify user the registered device is lost and needs to re-register again.
                    // ...

                    return;
                }
            }
        });
    }

    private async void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
    {
        // We must update the collection on the UI thread because the collection is databound 
        // to a UI element.
        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            lock (this)
            {
                Debug.WriteLine(String.Format("Removed {0}{1}", deviceInfoUpdate.Id,""));

                // Protect against race condition if the task runs after the app stopped the 
                // deviceWatcher.
                if (sender != deviceWatcher)
                {
                    return;
                }

                // Find the corresponding DeviceInformation in the collection and remove it.
                PhoneLineTransportDevice transportDevice = FindPhoneLineTransportDevice(deviceInfoUpdate.Id);
                if (transportDevice != null)
                { 
                    transportDevices.Remove(transportDevice);

                    // ...
                    // Update UX to indicate device is removed; either prompt user whether to 
                    // re-register the device for HF calling, or auto re-register.  Both of which
                    // amounts to calling the following APIs. An example of their usage is in the 
                    // next section.
                    // PhoneLineTransportDevice.RegisterApp()
                    // PhoneLineTransportDevice AudioRoutingStatusChanged
                    // PhoneLineTransportDevice.InBandRingingEnabledChaned
                }
            }
        });
    }

}

```

Registering a device for calling should be done in response the user clicking an “enable calling” (or similarly labeled) button, and involves the following:
1.	Registering by calling RegisterApp on the PhoneLineTransportDevice object
2.	Adding an event handler for the PhoneLineTransportDevice.AudioRoutingStatusChanged event
3.	Adding an event handler for the PhoneLineTransportDevice. InBandRingingEnabledChanged event

```cpp
// NOTE: Continuing from previous example, ActivateCallingButton_Click is a member method of 
    // Scenario_Client class 
    private void ActivateCallingButton_Click()
    {
        PhoneLineTransportDevice transportDevice = ResultsListView.SelectedItem as PhoneLineTransportDevice;
        if (transportDevice.IsRegistered() == false)
        {
            // If the phone somehow became unpaired it could lost the registration. 
            // Also, it’s prudent to assume app could unregister the phone at any time for any reason.
            transportDevice.RegisterApp();
            ActivateCallingButton.Content = "Calling activated";

           transportDevice.AudioRoutingStatusChanged += (o, args) =>
     {
         // App’s helper method to process audio routing status change
         // update UX to inform user when applicable for audio routing change,
         // e.g. when call audio cannot be routed to the PC due to bluetooth disconnected
  // or due to audio device not available on the PC. 
         ProcessTransportDeviceAudioRoutingState(transportDevice);
     };

     transportDevice.InBandRingingEnabledChanged += (o, args) =>
     {
         // App’s helper method to process in-band ringing enabled change
         ProcessTransportDeviceInBandRingingEnabledState(transportDevice);
     };
        }
    }

    private void ProcessTransportDeviceAudioRoutingState(PhoneLineTransportDevice transportDevice)
    {
        // pass transportDevice.DeviceId & transportDevice.AudioRoutingStatus to UX viewmodel 
        // that is associated with the corresponding PhoneLine.TransportDeviceId

    }

    private void ProcessTransportDeviceInBandRingingEnabledState(PhoneLineTransportDevice transportDevice)
    {
        // pass transportDevice.DeviceId & transportDevice.InBandRingingEnabled to UX viewmodel 
        // that is associated with the corresponding PhoneLine.TransportDeviceId.  
        // If InBandRingingEnabled is false, the app will play its own ring tone for incoming
	 // calls.
    }

```


## Placing an Outgoing Call
When a Bluetooth HFP device is connected and ready to be used for calling, your app should provide the user with controls for placing an outgoing call (such as by dialing a number or clicking the call button for a contact). When the user attempts to do so, your app should do the following:
1.	Check that audio can be routed to the local device (PC) by checking the PhoneLineTransportDevice.AudioRoutingStatus property. If not, direct the user to make the call from the remote device (phone) instead.
2.	Access the [PhoneCallStore](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonecallstore?view=winrt-22000) via [PhoneCallManager.RequestStoreAsync](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonecallmanager.requeststoreasync?view=winrt-22000)
3.	Get the ID for the default PhoneLine from the store via PhoneCallStore. GetDefaultLineAsync
4.	Retrieve the [PhoneLine](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phoneline?view=winrt-22000) using the line ID via PhoneLine.FromIdAsync
5.	Place the call, passing the number to be dialed and the contact / display name (if known), via PhoneLine.DialWithResultAsync
6.	Check the DialCallStatus property of the returned PhoneLineDialResult and update the UX accordingly. On success, your app should show the UI for a call being dialed and begin monitoring for PhoneCall status changes (see page on ongoing calls)

```c#
    private async void DialPhoneCallOnHfpLine(string number, string displayName)
    {
        try
        {
            var transportDevice = MainPage.Current.TransportDeviceListView.SelectedItem as PhoneLineTransportDevice;
	     if (transportDevice.AudioRoutingStatus == TransportDeviceAudioRoutingStatus.Unknown || transportDevice.AudioRoutingStatus == TransportDeviceAudioRoutingStatus.CannotRouteToLocalDevice)
            {
                // update UX to inform user the audio cannot be routed to the local device
                // or the routing status is unknown, so user is aware to use the remote device (Phone) 
                // instead for the outgoing call being placed.
                // This is a non-blocking call
            }

     // Get the phone line
     PhoneCallStore phoneCallStore = await PhoneCallManager.RequestStoreAsync();
     if (phoneCallStore == null)
     {
         return;
     }

     Guid lineId = await phoneCallStore.GetDefaultLineAsync();
     if (lineId == Guid.Empty)
     {
         return;
     }

     PhoneLine line = await PhoneLine.FromIdAsync(lineId);
  	     if (line == null)
     {
         return;
     }
       
        // Dial call
        PhoneLineDialResult dialResult = await line.DialWithResultAsync(number, displayName);
        if (dialResult.DialCallStatus == PhoneCallOperationStatus.Succeeded)
        {
            PhoneCall phoneCall = dialResult.DialedCall;

            // app’s viewModel to show UI based on phone call status
            this.viewModel.ProcessPhoneCallState(phoneCall);

            // app’s viewModel to monitor the phone call 
            this.viewModel.MonitorPhoneCallChanges(phoneCall);
        }
        else if (dialResult.DialCallStatus == PhoneCallOperationStatus.ConnectionLost)
        {
            // Failed to dial call due to lost connection, inform user
        }
        else if (dialResult.DialCallStatus == PhoneCallOperationStatus.TimedOut)
        {
            // Failed to dial call, update UX accordingly
        }
        else if (dialResult.DialCallStatus == PhoneCallOperationStatus.OtherFailure)
        {
            // Failed to dial call, update UX accordingly
        }
 }
        catch (Exception ex)
        {
            Logger.Instance.LogErrorMessage($"Fail to get the PhoneLine:{ex.Message}");
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////
    // Under ViewModel class
    //   A viewModel object is created and maintained in the app
    //

```

## Managing an Ongoing Call
Managing an Ongoing Call
When there is an active call, your app should provide UX controls for the following actions:
•	Muting the microphone for the call via PhoneCall.Mute()
•	Unmuting the microphone for the call via PhoneCall.UnMute()
•	Sending DTMF tones with the keypad, via PhoneCall.SendDtmfKey() 
•	Holding the call via PhoneCall.Hold()
•	Resuming the held call via PhoneCall.ResumeFromHold()
•	Transferring the call audio to the local device (PC) or remote device (phone) via PhoneCall.ChangeAudioDevice()
•	Ending the call via PhoneCall.End()
For each of these cases, the [PhoneCallOperationStatus](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.calls.phonecalloperationstatus?view=winrt-22000) returned should be checked, so that the user can be informed in the case of an error.

```csharp
    private void HoldCallButtonClick(object sender, RoutedEventArgs e)
    {
        this.viewModel.HoldCallButtonClick();
    }

    private void HoldCallButtonClick()
    {
        PhoneCallOperationStatus status = this.m_phoneCall.Hold();
        // app will get StatusChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
              ErrorHandler("Hold", status);
        }
    }

    private void EndCallButtonClick(object sender, RoutedEventArgs e)
    {
        this.viewModel.EndCallButtonClick();
    }

    private void EndCallButtonClick()
    {
        PhoneCallOperationStatus status = this.m_phoneCall.End();
        // app will get StatusChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
                ErrorHandler("End", status);
        }
    }

    private void SendDtmfKeyButtonClick(object sender, KeyRoutedEventArgs e)
    {
        this.viewModel.SendDtmfKeyButtonClick(e.Key, DtmfToneLocalAudioPlayback.Play);
    }

    private void SendDtmfKeyButtonClick(DtmfKey key, DtmfToneLocalAudioPlayback playDtmfTone)
    {
        PhoneCallOperationStatus status =  this.m_phoneCall.SendDtmfKey(key, playDtmfTone);
        // UI will display key pressed and only notify user if SendDtmfKey fails 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("SendDtmfKey", status);
        }
    }

    private void ResumeFromHoldCallButtonClick(object sender, RoutedEventArgs e)
    {
        this.viewModel.ResumeFromHoldCallButtonClick();
    }

    private void ResumeFromHoldCallButtonClick()
    {
        PhoneCallOperationStatus status = this.m_phoneCall.ResumeFromHold();
        // app will get StatusChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("ResumeFromHold", status);
        }
    }

    private void MuteCallButtonClick(object sender, RoutedEventArgs e)
    {
        this.viewModel.MuteCallButtonClick();
    }

    private void MuteCallButtonClick()
    {
        PhoneCallOperationStatus status = this.m_phoneCall.Mute();
        // app will get IsMutedChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("Mute", status);
        }
    }

    private void UnmuteCallButtonClick(object sender, RoutedEventArgs e)
    {
        this.viewModel.UnMmteCallButtonClick();
    }

    private void UnmuteCallButtonClick()
    {
        PhoneCallOperationStatus status = this.m_phoneCall.Unmute();
        // app will get IsMutedChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("Unmute", status);
        }
    }

    private void SetCallAudioTransferButtonClick(object sender, RoutedEventArgs e)
    {
        this.viewModel.SetCallAudioTransferButtonClick( );
    }

    private void SetCallAudioTransferButtonClick()
    {
        // First, update the UX to disable the CallAudioTransfer button.
	 PhoneCallAudioDevice audioDeviceUpdate = PhoneCallAudioDevice.Unknown;
        if (this.phoneCall.AudioDevice == PhoneCallAudioDevice.RemoteDevice)
        {
            audioDeviceUpdate = PhoneCallAudioDevice.LocalDevice;
        }
        else
        {
            audioDeviceUpdate = PhoneCallAudioDevice.RemoteDevice;
        }
        
        PhoneCallOperationStatus status = this.m_phoneCall.ChangeAudioDevice(
            audioDeviceUpdate);
        // app will get AudioDeviceChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX for the CallAudioTransfer button
        // E.g. if the current CallAudioTransfer button says “Local device: PC”, after the 
        // button is clicked and notification is received, if the updated phoneCall.AudioDevice 
        // is RemoteDevice, the new CallAudioTransfer button will be “Remote device: Phone”.
        // Last step is to re-enable the CallAudioTransfer button.
        // NOTE: This is a similar behavior in our system call-control UX.

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("ChangeAudioDevice", status);

            // Need to update the UX to re-enable the CallAudioTransfer button even though
            // the operation not succeeded. 
        }
    }

    private void ErrorHandler(string callControlAction, PhoneCallOperationStatus status)
    {
        if (status == PhoneCallOperationStatus.ConnectionLost)
        {
            // Failed to perform callControlAction on call due to lost connection, inform user
        }
        else if (status == PhoneCallOperationStatus.TimedOut)
        {
            // Failed to perform callControlAction  on call, update UX accordingly
        }
        else if (status == PhoneCallOperationStatus.Failed)
        {
                // Failed to perform callControlAction on call, update UX accordingly
        }
    }

```

These will each trigger an event on either PhoneCall.StatusChanged, PhoneCall.IsMutedChanged, or PhoneCall.AudioDeviceChanged. Your app should add handlers for each of these when a call is dialed, an incoming call arrives, or in the event of a surprise call.

```csharp
    private void MonitorPhoneCallChanges(PhoneCall newPhoneCall)
    {
        // set the PhoneCall member
        m_phoneCall = newPhoneCall;

	 if (m_phoneCall == null)
	 {
		return;
	 }

        m_phoneCall.StatusChanged += (o, args) =>
        {
            // App’s helper method to process phone call state change
            // update UX depending on call state,
            // e.g. dialing to talking - show transition from dial UI to call progress UI
            ProcessPhoneCallState(m_phoneCall);

        };

        m_phoneCall.IsMutedChanged += (o, args) =>
        {
            // App’s helper method to process phone call mute state changed
            // update Mute button state in UX,
            // e.g. mute state changed from muted to unmuted
            ProcessCallMuteState (m_phoneCall.Id, m_phoneCall.IsMuted);

        };

        m_phoneCall.AudioDeviceChanged += (o, args) =>
        {
            // App’s helper method to process phone call audio device change
            // update UX for audio device change,
            // e.g. when call audio transfer from PC to phone 
            ProcessCallAudioDevice (m_phoneCall.Id, m_phoneCall.AudioDevice);

        };
    }
    private void ProcessPhoneCallState(PhoneCall phoneCall)
    {
        switch (phoneCall.Status)
        {
            case PhoneCallStatus.Lost:
                // notify UI view to show notification UI of losing  
  // connection of this call
                break;

            case PhoneCallStatus.Incoming:
                // notify UI view to show incoming call UI of this call
                break;

            case PhoneCallStatus.Dialing:
                // notify UI view to show/update call dialing UI of this call
                break;

            case PhoneCallStatus.Talking:
                // notify UI view to show/update call progress UI of this call
                // in talking state.
                break;

            case PhoneCallStatus.Held:
                // notify UI view to show/update call progress UI of this call 
  // in held state
                break;

            case PhoneCallStatus.Ended:
                // notify UI view to end this call in the call progress UI
                break;

            default:
                break;
        }
    }       
       private void ProcessCallMuteState(string callId, Boolean isMuted)
   {
	if (isMuted)
       {
		// notify UI view to update the mute button of this call
       }
	else
	{
		// notify UI view to update the mute button of this call
}
   }

       private void ProcessCallAudioDevice(string callId, PhoneCallAudioDevice audioDevice)
   {
	switch (audioDevice)
	{
            case PhoneCallAudioDevice.RemoteDevice:
  		   // notify UI view to update the audioDevice button of this call, 
   // indicating audio device is on the remote device (Phone).
                break;

            case PhoneCallAudioDevice.LocalDevice:
		  // notify UI view to update the audioDevice button of this call,
 		  // indicating audio device is on the local device (PC).
                break;

            default:
                break;
	}
   }

```

## Handling IncomingCallNotification Trigger
Your app needs to register for the IncomingCallNotification PhoneTrigger type to be informed when an incoming call arrives. The trigger will activate your app’s background task, and include the CallId and LineId in the trigger details. The PhoneCall can then be retrieved by either
•	Calling PhoneCall.GetFromId() and passing the CallId (primary method)
•	Getting the PhoneLine via PhoneLine.FromIdAsync(), passing the LineId, and then retrieving the list of active PhoneCall objects with PhoneLine.GetAllActivePhoneCallsAsync(). Note that this will include past (now inactive) calls, and your app should filter these out. [Add note on multiple ongoing calls / a link to page discussing it]
Once a valid call (or calls) is/are retrieved, your app should display either the incoming call or ongoing call UI, depending on PhoneCall.Status
```csharp
// Background task is in-proc to the app
    // https://docs.microsoft.com/en-us/windows/uwp/launch-resume/create-and-register-an-inproc-background-task
    public sealed class IncomingCallNotificationTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance bgtaskInstance)
        {
            BackgroundTaskDeferral _deferral = bgtaskInstance.GetDeferral();

            PhoneIncomingCallNotificationTriggerDetails triggerDetails =
                (PhoneIncomingCallNotificationTriggerDetails)taskInstance.TriggerDetails;

            // triggerDetails will contain callId of incoming call 
            Phonecall phoneCall = PhoneCall.GetFromId(triggerDetails.CallId);

            if (phoneCall != null)
            {
                // app to further process the phone call.
                // In this case, a new incoming call notification UI will show to the user
                ProcessPhoneCallState(phoneCall);
            }
            else
            {
                // App to retrieve all current phone calls on the phone line, and enumerate
                // them if there is any call not being managed in the app at the current moment.
                // Reason: the call could have been cancelled (thus it’s removed from the 
                // system service) by the time PhoneCall.GetFromId is invoked.  There 
                // could be other reasons e.g. Bluetooth disconnects momentarily,
                // and as a result the call will be momentarily removed before Bluetooth
                // reconnects where the call will be added back to the system service.
                PhoneLine line = null;
                try
    	         {
                   line = await PhoneLine.FromIdAsync(triggerDetails.LineId);
                }
                catch (Exception ex)
                {
  	             Logger.Instance.LogErrorMessage($"PhoneLine.FromIdAsync failed:{ex.Message}");
                }

		  if (line != null)
		  {
                    PhoneCallsResult result = await line->GetAllActivePhoneCallsAsync();
                if (result.OperationStatus == PhoneLineOperationStatus.Succeeded)
                {

                    var phonecallList = result.AllActivePhoneCalls;
                    foreach (PhoneCall phonecall in phonecallList)
                    {
                        // app to further process each phone call based on the status
                        // to notify the UI layer to show / update the given call 
                        ProcessPhoneCallState(phoneCall);
                    }
                }
         }
            }
            _deferral.Complete();
        }
    }

```
Incoming Call UI
When an incoming call arrives, your app should display UI with the option to either accept or reject the call via PhoneCall.AcceptIncomingAsync() or PhoneCall.RejectIncomingAsync, checking the returned PhoneCallOperationStatus for success and handling any errors.
```csharp
private async void AcceptIncomingCallButtonClick(object sender, RoutedEventArgs e)
    {
	this.viewModel.AcceptIncomingCallButtonClick();
    }    

    private async void AcceptIncomingCallButtonClick()
    {
        PhoneCallOperationStatus status = await this.m_phoneCall.AcceptIncomingAsync();
        // app will get StatusChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("AcceptIncomingAsync", status);
        }
    }
   

    private async void RejectIncomingCallButtonClick(object sender, RoutedEventArgs e)
    {
	this.viewModel.RejectIncomingCallButtonClick();
    }    

    private async void RejectIncomingCallButtonClick()
    {
        PhoneCallOperationStatus status = await this.m_phoneCall.RejectIncomingAsync();
        // app will get StatusChanged event notification via MonitorPhoneCallChanges and
        // ProcessPhoneCallState will update the UX 

        if (status != PhoneCallOperationStatus.Succeeded)
        {
            ErrorHandler("RejectIncomingCall", status);
        }
    }


```
## Handling App Activation by Phone Service (Surprise Calls)
Your app must also handle being activated directly by the phone service, in the event a call is placed or an active call is discovered through a previously registered PhoneLineTransportDevice. This can occur if:
•	The user’s phone is connected via Bluetooth, and they place an outgoing call vi the phone, rather than the app.
•	A user’s phone has become disconnected, and Bluetooth reconnects while there is an ongoing call on the phone.
In these cases, the app will be activated with [ActivationKind.PhoneCallActivation](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.activation.activationkind?view=winrt-22000), and the [PhoneCallActivatedEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.activation.phonecallactivatedeventargs?view=winrt-22000) will contain the LineId for the PhoneLine that the call is on. Your app should then retrieve the PhoneLine via PhoneLine.FromIdAsync(), passing the LineId, and get the outgoing call(s) on the line with PhoneLine.GetAllActivePhoneCallsAsync(). The appropriate UI for an incoming or ongoing call should then be displayed.

```csharp
protected override async void OnActivated(IActivatedEventArgs args)
    {
        if (args.Kind == ActivationKind.PhoneCallActivation)
        {
            PhoneCallActivatedEventArgs phoneCallArgs = args as PhoneCallActivatedEventArgs;

            // attempts to retrieve all current phone calls on the phone line
            PhoneLine line = null;
            try
            {
		  Guid lineId = phoneCallArgs.LineId;
                line = await PhoneLine.FromIdAsync(lineId);
            }
            catch (Exception ex)
            {
         Logger.Instance.LogErrorMessage($"PhoneLine.FromIdAsync failed:{ex.Message}");
            }

            if (line != null)
	     {
                PhoneCallsResult result = await line->GetAllActivePhoneCallsAsync();
            if (result.OperationStatus == PhoneLineOperationStatus.Succeeded)
            {
                var phonecallList = result.AllActivePhoneCalls;
                foreach (PhoneCall phonecall in phonecallList)
                {
                    // app to further process each phone call and 
                    // show or update UI based on phone call status
                    ProcessPhoneCallState(phoneCall);
                }
            }
            }
            else
            {
	         // handle the case where no PhoneLine obj was returned
                // e.g. fire a toast notification 
            }

            Window.Current.Activate();
        }
    }

```
## Behavior of Deprecated API
Existing system call control related APIs will still be present but will have no effect when called. See the corresponding pages for each class, method, and trigger for more details on post-deprecation behavior. Some notable cases to consider:
CallOrigin and IncomingCallDismissed background triggers will no longer be fired when a call is dialed/arrives or when an incoming call is rejected, respectively. If your app listens for these events post-deprecation, they will not be received.
PhoneLineTransportDevice.RequestAccessAsync() will now return DeviceAccessStatus.deniedBySystem, unless windows.phonecallactivation is properly declared in the appxmanifest. If there are previous Hands-free Bluetooth device registrations for the app, they will be removed and a new PhoneLineWatcher.LineRemoved event will be sent to the app, unless windows.phonecallactivation is properly declared in the appxmanifest. It is suggested that a handler for this event be included in the app version prior to supporting the new API, so that users who have not yet updated their app post-deprecation can be notified that their hands-free device is no longer registered.

## Where to get help
For questions, please reach out to windowscallcontrol@microsoft.com

## Timeline
New APIs allowing apps to host their own UX are currently available in Windows 11. System Call Control will be removed in Windows 2022 (Fall 2022 release). This section will be updated with flighting and release timelines when available.