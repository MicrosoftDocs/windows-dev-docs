---
title: Connect devices through remote sessions
description: Create shared experiences across multiple devices by joining them in a remote session.
ms.assetid: 1c8dba9f-c933-4e85-829e-13ad784dd3e2
ms.date: 06/28/2017
ms.topic: article
keywords: windows 10, uwp, connected devices, remote systems, rome, project rome
ms.localizationpriority: medium
---
# Connect devices through remote sessions

The Remote Sessions feature allows an app to connect to other devices through a session, either for explicit app messaging or for brokered exchange of system-managed data, such as the **[SpatialEntityStore](/uwp/api/windows.perception.spatial.spatialentitystore)** for holographic sharing between Windows Holographic devices.

Remote sessions can be created by any Windows device, and any Windows device can request to join (although sessions can have invite-only visibility), including devices signed in by other users. This guide provides basic sample code for all of the major scenarios that make use of remote sessions. This code can be incorporated into an existing app project and modified as necessary. For an end-to-end implementation, see the [Quiz Game sample app](https://github.com/microsoft/Windows-appsample-remote-system-sessions)).

## Preliminary setup

### Add the remoteSystem capability

In order for your app to launch an app on a remote device, you must add the `remoteSystem` capability to your app package manifest. You can use the package manifest designer to add it by selecting **Remote System** on the **Capabilities** tab, or you can manually add the following line to your project's _Package.appxmanifest_ file.

``` xml
<Capabilities>
   <uap3:Capability Name="remoteSystem"/>
</Capabilities>
```

### Enable cross-user discovering on the device
Remote Sessions is geared toward connecting multiple different users, so the devices involved will need to have Cross-User Sharing enabled. This is a system setting that can be queried with a static method in the **RemoteSystem** class:

```csharp
if (!RemoteSystem.IsAuthorizationKindEnabled(RemoteSystemAuthorizationKind.Anonymous)) {
	// The system is not authorized to connect to cross-user devices. 
	// Inform the user that they can discover more devices if they
	// update the setting to "Everyone nearby".
}
```

To change this setting, the user must open the **Settings** app. In the **System** > **Shared experiences** > **Share across devices** menu, there is a drop-down box where the user can specify which devices their system can share with.

![shared experiences settings page](images/shared-experiences-settings.png)

### Include the necessary namespaces
In order to use all of the code snippets in this guide, you will need the following `using` statements in your class file(s).

```csharp
using System.Runtime.Serialization.Json;
using Windows.Foundation.Collections;
using Windows.System.RemoteSystems;
```

## Create a remote session

To create a remote session instance, you must start with a **[RemoteSystemSessionController](/uwp/api/windows.system.remotesystems.remotesystemsessioncontroller)** object. Use the following framework to create a new session and handle join requests from other devices.

```csharp
public async void CreateSession() {
    
    // create a session controller
    RemoteSystemSessionController manager = new RemoteSystemSessionController("Bob’s Minecraft game");
    
    // register the following code to handle the JoinRequested event
    manager.JoinRequested += async (sender, args) => {
        // Get the deferral
        var deferral = args.GetDeferral();
        
        // display the participant (args.JoinRequest.Participant) on UI, giving the 
        // user an opportunity to respond
        // ...
        
        // If the user chooses "accept", accept this remote system as a participant
        args.JoinRequest.Accept();
    };
    
    // create and start the session
    RemoteSystemSessionCreationResult createResult = await manager.CreateSessionAsync();
    
    // handle the creation result
    if (createResult.Status == RemoteSystemSessionCreationStatus.Success) {
        // creation was successful, get a reference to the session
        RemoteSystemSession currentSession = createResult.Session;
        
        // optionally subscribe to the disconnection event
        currentSession.Disconnected += async (sender, args) => {
            // update the UI, using args.Reason
            //...
        };
    
        // Use session (see later section)
        //...
    
    } else if (createResult.Status == RemoteSystemSessionCreationStatus.SessionLimitsExceeded) {
        // creation failed. Optionally update UI to indicate that there are too many sessions in progress
    } else {
        // creation failed for an unknown reason. Optionally update UI
    }
}
```

### Make a remote session invite-only

If you wish to keep your remote session from being publicly discoverable, you can make it invite-only. Only the devices that receive an invitation will be able to send join requests. 

The procedure is mostly the same as above, but when constructing the **[RemoteSystemSessionController](/uwp/api/windows.system.remotesystems.remotesystemsessioncontroller)** instance, you will pass in a configured **[RemoteSystemSessionOptions](/uwp/api/windows.system.remotesystems.RemoteSystemSessionOptions)** object.

```csharp
// define the session options with the invite-only designation
RemoteSystemSessionOptions sessionOptions = new RemoteSystemSessionOptions();
sessionOptions.IsInviteOnly = true;

// create the session controller
RemoteSystemSessionController manager = new RemoteSystemSessionController("Bob's Minecraft game", sessionOptions);

//...
```

To send an invitation, you must have a reference to the receiving remote system (acquired through normal remote system discovery). Simply pass this reference into the session object's **[SendInvitationAsync](/uwp/api/windows.system.remotesystems.remotesystemsession.sendinvitationasync)** method. All of the participants in a session have a reference to the remote session (see next section), so any participant can send an invitation.

```csharp
// "currentSession" is a reference to a RemoteSystemSession.
// "guestSystem" is a previously discovered RemoteSystem instance
currentSession.SendInvitationAsync(guestSystem); 
```

## Discover and join a remote session

The process of discovering remote sessions is handled by the **[RemoteSystemSessionWatcher](/uwp/api/windows.system.remotesystems.remotesystemsessionwatcher)** class and is similar to discovering individual remote systems.

```csharp
public void DiscoverSessions() {
    
    // create a watcher for remote system sessions
    RemoteSystemSessionWatcher sessionWatcher = RemoteSystemSession.CreateWatcher();
    
    // register a handler for the "added" event
    sessionWatcher.Added += async (sender, args) => {
        
        // get a reference to the info about the discovered session
        RemoteSystemSessionInfo sessionInfo = args.SessionInfo;
        
        // Optionally update the UI with the sessionInfo.DisplayName and 
        // sessionInfo.ControllerDisplayName strings. 
        // Save a reference to this RemoteSystemSessionInfo to use when the
        // user selects this session from the UI
        //...
    };
    
    // Begin watching
    sessionWatcher.Start();
}
```

When a **[RemoteSystemSessionInfo](/uwp/api/windows.system.remotesystems.remotesystemsessioninfo)** instance is obtained, it can be used to issue a join request to the device that controls the corresponding session. An accepted join request will asynchronously return a **[RemoteSystemSessionJoinResult](/uwp/api/windows.system.remotesystems.remotesystemsessionjoinresult)** object that contains a reference to the joined session.

```csharp
public async void JoinSession(RemoteSystemSessionInfo sessionInfo) {

    // issue a join request and wait for result.
    RemoteSystemSessionJoinResult joinResult = await sessionInfo.JoinAsync();
    if (joinResult.Status == RemoteSystemSessionJoinStatus.Success) {
        // Join request was approved

        // RemoteSystemSession instance "currentSession" was declared at class level.
        // Assign the value obtained from the join result.
        currentSession = joinResult.Session;
        
        // note connection and register to handle disconnection event
        bool isConnected = true;
        currentSession.Disconnected += async (sender, args) => {
            isConnected = false;

            // update the UI with args.Reason value
        };
        
        if (isConnected) {
            // optionally use the session here (see next section)
            //...
        }
    } else {
        // Join was unsuccessful.
        // Update the UI, using joinResult.Status value to show cause of failure.
    }
}
```

A device can be joined to multiple sessions at the same time. For this reason, it may be desirable to separate the joining functionality from the actual interaction with each session. As long as a reference to the **[RemoteSystemSession](/uwp/api/windows.system.remotesystems.remotesystemsession)** instance is maintained in the app, communication can be attempted over that session.

## Share messages and data through a remote session

### Receive messages

You can exchange messages and data with other participant devices in the session by using a **[RemoteSystemSessionMessageChannel](/uwp/api/windows.system.remotesystems.remotesystemsessionmessagechannel)** instance, which represents a single session-wide communication channel. As soon as it's initialized, it begins listening for incoming messages.

>[!NOTE]
>Messages must be serialized and deserialized from byte arrays upon sending and receiving. This functionality is included in the following examples, but it can be implemented separately for better code modularity. See the [sample app](https://github.com/microsoft/Windows-appsample-remote-system-sessions)) for an example of this.

```csharp
public async void StartReceivingMessages() {
    
    // Initialize. The channel name must be known by all participant devices 
    // that will communicate over it.
    RemoteSystemSessionMessageChannel messageChannel = new RemoteSystemSessionMessageChannel(currentSession, 
        "Everyone in Bob's Minecraft game", 
        RemoteSystemSessionMessageChannelReliability.Reliable);
    
    // write the handler for incoming messages on this channel
    messageChannel.ValueSetReceived += async (sender, args) => {
        
        // Update UI: a message was received from the participant args.Sender
        
        // Deserialize the message 
        // (this app must know what key to use and what object type the value is expected to be)
        ValueSet receivedMessage = args.Message;
        object rawData = receivedMessage["appKey"]);
        object value = new ExpectedType(); // this must be whatever type is expected

        using (var stream = new MemoryStream((byte[])rawData)) {
            value = new DataContractJsonSerializer(value.GetType()).ReadObject(stream);
        }
        
        // do something with the "value" object
        //...
    };
}
```

### Send messages

When the channel is established, sending a message to all of the session participants is straightforward.

```csharp
public async void SendMessageToAllParticipantsAsync(RemoteSystemSessionMessageChannel messageChannel, object value){

    // define a ValueSet message to send
    ValueSet message = new ValueSet();
    
    // serialize the "value" object to send
    using (var stream = new MemoryStream()){
        new DataContractJsonSerializer(value.GetType()).WriteObject(stream, value);
        byte[] rawData = stream.ToArray();
            message["appKey"] = rawData;
    }
    
    // Send message to all participants. Ordering is not guaranteed.
    await messageChannel.BroadcastValueSetAsync(message);
}
```

In order to send a message to only certain participant(s), you must first initiate a discovery process to acquire references to the remote systems participating in the session. This is similar to the process of discovering remote systems outside of a session. Use a **[RemoteSystemSessionParticipantWatcher](/uwp/api/windows.system.remotesystems.remotesystemsessionparticipantwatcher)** instance to find a session's participant devices.

```csharp
public void WatchForParticipants() {
    // "currentSession" is a reference to a RemoteSystemSession.
    RemoteSystemSessionParticipantWatcher watcher = currentSession.CreateParticipantWatcher();

    watcher.Added += (sender, participant) => {
        // save a reference to "participant"
        // optionally update UI
    };   

    watcher.Removed += (sender, participant) => {
        // remove reference to "participant"
        // optionally update UI
    };

    watcher.EnumerationCompleted += (sender, args) => {
        // Apps can delay data model render up until this point if they wish.
    };

    // Begin watching for session participants
    watcher.Start();
}
```

When a list of references to session participants is obtained you can send a message to any set of them.

To send a message to a single participant (ideally selected on-screen by the user), simply pass the reference into a method like the following.

```csharp
public async void SendMessageToParticipantAsync(RemoteSystemSessionMessageChannel messageChannel, RemoteSystemSessionParticipant participant, object value) {
    
    // define a ValueSet message to send
    ValueSet message = new ValueSet();
    
    // serialize the "value" object to send
    using (var stream = new MemoryStream()){
        new DataContractJsonSerializer(value.GetType()).WriteObject(stream, value);
        byte[] rawData = stream.ToArray();
            message["appKey"] = rawData;
    }

    // Send message to the participant
    await messageChannel.SendValueSetAsync(message,participant);
}
```

To send a message to multiple participants (ideally selected on-screen by the user), add them to a list object and pass the list into a method like the following.

```csharp
public async void SendMessageToListAsync(RemoteSystemSessionMessageChannel messageChannel, IReadOnlyList<RemoteSystemSessionParticipant> myTeam, object value){

    // define a ValueSet message to send
    ValueSet message = new ValueSet();
    
    // serialize the "value" object to send
    using (var stream = new MemoryStream()){
        new DataContractJsonSerializer(value.GetType()).WriteObject(stream, value);
        byte[] rawData = stream.ToArray();
            message["appKey"] = rawData;
    }

    // Send message to specific participants. Ordering is not guaranteed.
    await messageChannel.SendValueSetToParticipantsAsync(message, myTeam);   
}
```

## Related topics
* [Connected apps and devices (Project Rome)](connected-apps-and-devices.md)
* [Remote Systems API reference](/uwp/api/Windows.System.RemoteSystems)