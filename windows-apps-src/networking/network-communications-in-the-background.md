---
description: To continue network communication while it's not in the background, an app can use background tasks and either socket broker or control channel triggers.
title: Network communications in the background
ms.assetid: 537F8E16-9972-435D-85A5-56D5764D3AC2
ms.date: 06/14/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Network communications in the background
To continue network communication while it's not in the foreground, your app can use background tasks and one of these two options.
- Socket broker. If your app uses sockets for long-term connections then, when it leaves the foreground, it can delegate ownership of a socket to a system socket broker. The broker then: activates your app when traffic arrives on the socket; transfers ownership back to your app; and your app then processes the arriving traffic.
- Control channel triggers. 

## Performing network operations in background tasks
- Use a [SocketActivityTrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.socketactivitytrigger) to activate the background task when a packet is received and you need to perform a short-lived task. After performing the task, the background task should terminate in order to save power.
- Use a [ControlChannelTrigger](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) to activate the background task when a packet is received and you need to perform a long-lived task.

**Network-related conditions and flags**

- Add the **InternetAvailable** condition to your background task [BackgroundTaskBuilder.AddCondition](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) to delay triggering the background task until the network stack is running. This condition saves power because the background task won't execute until the network is up. This condition does not provide real-time activation.

Regardless of the trigger you use, set [IsNetworkRequested](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) on your background task to ensure that the network stays up while the background task runs. This tells the background task infrastructure to keep the network up while the task is executing, even if the device has entered Connected Standby mode. If your background task does not use **IsNetworkRequested**, then your background task will not be able to access the network when in Connected Standby mode (for example, when a phone's screen is turned off).

## Socket broker and the SocketActivityTrigger
If your app uses [**DatagramSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.DatagramSocket), [**StreamSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket), or [**StreamSocketListener**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocketListener) connections, then you should use [**SocketActivityTrigger**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger) and the socket broker to be notified when traffic arrives for your app while it's not in the foreground.

In order for your app to receive and process data received on a socket when your app is not active, your app must perform some one-time setup at startup, and then transfer socket ownership to the socket broker when it is transitioning to a state where it is not active.

The one-time setup steps are to create a trigger, to register a background task for the trigger, and to enable the socket for the socket broker:
  - Create a **SocketActivityTrigger** and register a background task for the trigger with the TaskEntryPoint parameter set to your code for processing a received packet.
```csharp
            var socketTaskBuilder = new BackgroundTaskBuilder();
            socketTaskBuilder.Name = _backgroundTaskName;
            socketTaskBuilder.TaskEntryPoint = _backgroundTaskEntryPoint;
            var trigger = new SocketActivityTrigger();
            socketTaskBuilder.SetTrigger(trigger);
            _task = socketTaskBuilder.Register();
```
  - Call **EnableTransferOwnership** on the socket, before you bind the socket.
```csharp
           _tcpListener = new StreamSocketListener();

           // Note that EnableTransferOwnership() should be called before bind,
           // so that tcpip keeps required state for the socket to enable connected
           // standby action. Background task Id is taken as a parameter to tie wake pattern
           // to a specific background task.  
           _tcpListener. EnableTransferOwnership(_task.TaskId,SocketActivityConnectedStandbyAction.Wake);
           _tcpListener.ConnectionReceived += OnConnectionReceived;
           await _tcpListener.BindServiceNameAsync("my-service-name");
```

Once your socket is properly set up, when your app is about to suspend, call **TransferOwnership** on the socket to transfer it to a socket broker. The broker monitors the socket and activates your background task when data is received. The following example includes a utility **TransferOwnership** function to perform the transfer for **StreamSocketListener** sockets. (Note that the different types of sockets each have their own **TransferOwnership** method, so you must call the method appropriate for the socket whose ownership you are transferring. Your code would probably contain an overloaded **TransferOwnership** helper with one implementation for each socket type you use, so that the **OnSuspending** code remains easy to read.)

An app transfers ownership of a socket to a socket broker and passes the ID for the background task using the appropriate one of the following methods:
-   One of the [**TransferOwnership**](https://docs.microsoft.com/uwp/api/windows.networking.sockets.datagramsocket.transferownership) methods on a [**DatagramSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.DatagramSocket).
-   One of the [**TransferOwnership**](https://docs.microsoft.com/uwp/api/windows.networking.sockets.streamsocket.transferownership) methods on a [**StreamSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket).
-   One of the [**TransferOwnership**](https://docs.microsoft.com/uwp/api/windows.networking.sockets.streamsocketlistener.transferownership) methods on a [**StreamSocketListener**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocketListener).

```csharp

// declare int _transferOwnershipCount as a field.

private void TransferOwnership(StreamSocketListener tcpListener)
{
    await tcpListener.CancelIOAsync();

    var dataWriter = new DataWriter();
    ++_transferOwnershipCount;
    dataWriter.WriteInt32(transferOwnershipCount);
    var context = new SocketActivityContext(dataWriter.DetachBuffer());
    tcpListener.TransferOwnership(_socketId, context);
}

private void OnSuspending(object sender, SuspendingEventArgs e)
{
    var deferral = e.SuspendingOperation.GetDeferral();

    TransferOwnership(_tcpListener);
    deferral.Complete();
}
```
In your background task's event handler:
   -  First, get a background task deferral so that you can handle the event using asynchronous methods.
```csharp
var deferral = taskInstance.GetDeferral();
```
   -  Next, extract the SocketActivityTriggerDetails from the event arguments, and find the reason that the event was raised:
```csharp
var details = taskInstance.TriggerDetails as SocketActivityTriggerDetails;
    var socketInformation = details.SocketInformation;
    switch (details.Reason)
```
   -   If the event was raised because of socket activity, create a DataReader on the socket, load the reader asynchronously, and then use the data according to your app's design. Note that you must return ownership of the socket back to the socket broker, in order to be notified of further socket activity again.

   In the following example, the text received on the socket is displayed in a toast.

```csharp
case SocketActivityTriggerReason.SocketActivity:
            var socket = socketInformation.StreamSocket;
            DataReader reader = new DataReader(socket.InputStream);
            reader.InputStreamOptions = InputStreamOptions.Partial;
            await reader.LoadAsync(250);
            var dataString = reader.ReadString(reader.UnconsumedBufferLength);
            ShowToast(dataString);
            socket.TransferOwnership(socketInformation.Id); /* Important! */
            break;
```

   -   If the event was raised because a keep alive timer expired, then your code should send some data over the socket in order to keep the socket alive and restart the keep alive timer. Again, it is important to return ownership of the socket back to the socket broker in order to receive further event notifications:

```csharp
case SocketActivityTriggerReason.KeepAliveTimerExpired:
            socket = socketInformation.StreamSocket;
            DataWriter writer = new DataWriter(socket.OutputStream);
            writer.WriteBytes(Encoding.UTF8.GetBytes("Keep alive"));
            await writer.StoreAsync();
            writer.DetachStream();
            writer.Dispose();
            socket.TransferOwnership(socketInformation.Id); /* Important! */
            break;
```

   -   If the event was raised because the socket was closed, re-establish the socket, making sure that after you create the new socket, you transfer ownership of it to the socket broker. In this sample, the hostname and port are stored in local settings so that they can be used to establish a new socket connection:

```csharp
case SocketActivityTriggerReason.SocketClosed:
            socket = new StreamSocket();
            socket.EnableTransferOwnership(taskInstance.Task.TaskId, SocketActivityConnectedStandbyAction.Wake);
            if (ApplicationData.Current.LocalSettings.Values["hostname"] == null)
            {
                break;
            }
            var hostname = (String)ApplicationData.Current.LocalSettings.Values["hostname"];
            var port = (String)ApplicationData.Current.LocalSettings.Values["port"];
            await socket.ConnectAsync(new HostName(hostname), port);
            socket.TransferOwnership(socketId);
            break;
```

   -   Don't forget to Complete your deferral, once you have finished processing the event notification:

```csharp
  deferral.Complete();
```

For a complete sample demonstrating the use of the [**SocketActivityTrigger**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger) and socket broker, see the [SocketActivityStreamSocket sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SocketActivityStreamSocket). The initialization of the socket is performed in Scenario1\_Connect.xaml.cs, and the background task implementation is in SocketActivityTask.cs.

You will probably notice that the sample calls **TransferOwnership** as soon as it creates a new socket or acquires an existing socket, rather than using the **OnSuspending** even handler to do so as described in this topic. This is because the sample focuses on demonstrating the [**SocketActivityTrigger**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger), and doesn't use the socket for any other activity while it is running. Your app will probably be more complex, and should use **OnSuspending** to determine when to call **TransferOwnership**.

## Control channel triggers
First, ensure that you're using control channel triggers (CCTs) appropriately. If you're using [**DatagramSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.DatagramSocket), [**StreamSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket), or [**StreamSocketListener**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocketListener) connections, then we recommend that you use [**SocketActivityTrigger**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger). You can use CCTs for **StreamSocket**, but they use more resources and might not work in Connected Standby mode.

If you are using WebSockets, [**IXMLHTTPRequest2**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2), [**System.Net.Http.HttpClient**](https://docs.microsoft.com/uwp/api/Windows.Web.Http.HttpClient), or [**Windows.Web.Http.HttpClient**](/uwp/api/windows.web.http.httpclient), then you must use [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger).

## ControlChannelTrigger with WebSockets
Some special considerations apply when using [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). There are some transport-specific usage patterns and best practices that should be followed when using a **MessageWebSocket** or **StreamWebSocket** with **ControlChannelTrigger**. In addition, these considerations affect the way that requests to receive packets on the **StreamWebSocket** are handled. Requests to receive packets on the **MessageWebSocket** are not affected.

The following usage patterns and best practices should be followed when using [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger):

-   An outstanding socket receive must be kept posted at all times. This is required to allow the push notification tasks to occur.
-   The WebSocket protocol defines a standard model for keep-alive messages. The [**WebSocketKeepAlive**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.WebSocketKeepAlive) class can send client-initiated WebSocket protocol keep-alive messages to the server. The **WebSocketKeepAlive** class should be registered as the TaskEntryPoint for a KeepAliveTrigger by the app.

Some special considerations affect the way that requests to receive packets on the [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) are handled. In particular, when using a **StreamWebSocket** with the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger), your app must use a raw async pattern for handling reads instead of the **await** model in C# and VB.NET or Tasks in C++. The raw async pattern is illustrated in a code sample later in this section.

Using the raw async pattern allows Windows to synchronize the [**IBackgroundTask.Run**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method on the background task for the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) with the return of the receive completion callback. The **Run** method is invoked after the completion callback returns. This ensures that the app has received the data/errors before the **Run** method is invoked.

It is important to note that the app has to post another read before it returns control from the completion callback. It is also important to note that the [**DataReader**](https://docs.microsoft.com/uwp/api/Windows.Storage.Streams.DataReader) cannot be directly used with the [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) transport since that breaks the synchronization described above. It is not supported to use the [**DataReader.LoadAsync**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.loadasync) method directly on top of the transport. Instead, the [**IBuffer**](https://docs.microsoft.com/uwp/api/Windows.Storage.Streams.IBuffer) returned by the [**IInputStream.ReadAsync**](https://docs.microsoft.com/uwp/api/windows.storage.streams.iinputstream.readasync) method on the [**StreamWebSocket.InputStream**](https://docs.microsoft.com/uwp/api/windows.networking.sockets.streamwebsocket.inputstream) property can be later passed to [**DataReader.FromBuffer**](https://docs.microsoft.com/uwp/api/windows.storage.streams.datareader.frombuffer) method for further processing.

The following sample shows how to use a raw async pattern for handling reads on the [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket).

```csharp
void PostSocketRead(int length)
{
    try
    {
        var readBuf = new Windows.Storage.Streams.Buffer((uint)length);
        var readOp = socket.InputStream.ReadAsync(readBuf, (uint)length, InputStreamOptions.Partial);
        readOp.Completed = (IAsyncOperationWithProgress<IBuffer, uint>
            asyncAction, AsyncStatus asyncStatus) =>
        {
            switch (asyncStatus)
            {
                case AsyncStatus.Completed:
                case AsyncStatus.Error:
                    try
                    {
                        // GetResults in AsyncStatus::Error is called as it throws a user friendly error string.
                        IBuffer localBuf = asyncAction.GetResults();
                        uint bytesRead = localBuf.Length;
                        readPacket = DataReader.FromBuffer(localBuf);
                        OnDataReadCompletion(bytesRead, readPacket);
                    }
                    catch (Exception exp)
                    {
                        Diag.DebugPrint("Read operation failed:  " + exp.Message);
                    }
                    break;
                case AsyncStatus.Canceled:

                    // Read is not cancelled in this sample.
                    break;
           }
       };
   }
   catch (Exception exp)
   {
       Diag.DebugPrint("failed to post a read failed with error:  " + exp.Message);
   }
}
```

The read completion handler is guaranteed to fire before the [**IBackgroundTask.Run**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method on the background task for the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) is invoked. Windows has internal synchronization to wait for an app to return from the read completion callback. The app typically quickly processes the data or the error from the [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) in the read completion callback. The message itself is processed within the context of the **IBackgroundTask.Run** method. In this sample below, this point is illustrated by using a message queue that the read completion handler inserts the message into and the background task later processes.

The following sample shows the read completion handler to use with a raw async pattern for handling reads on the [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket).

```csharp
public void OnDataReadCompletion(uint bytesRead, DataReader readPacket)
{
    if (readPacket == null)
    {
        Diag.DebugPrint("DataReader is null");

        // Ideally when read completion returns error,
        // apps should be resilient and try to
        // recover if there is an error by posting another recv
        // after creating a new transport, if required.
        return;
    }
    uint buffLen = readPacket.UnconsumedBufferLength;
    Diag.DebugPrint("bytesRead: " + bytesRead + ", unconsumedbufflength: " + buffLen);

    // check if buffLen is 0 and treat that as fatal error.
    if (buffLen == 0)
    {
        Diag.DebugPrint("Received zero bytes from the socket. Server must have closed the connection.");
        Diag.DebugPrint("Try disconnecting and reconnecting to the server");
        return;
    }

    // Perform minimal processing in the completion
    string message = readPacket.ReadString(buffLen);
    Diag.DebugPrint("Received Buffer : " + message);

    // Enqueue the message received to a queue that the push notify
    // task will pick up.
    AppContext.messageQueue.Enqueue(message);

    // Post another receive to ensure future push notifications.
    PostSocketRead(MAX_BUFFER_LENGTH);
}
```

An additional detail for Websockets is the keep-alive handler. The WebSocket protocol defines a standard model for keep-alive messages.

When using [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket), register a [**WebSocketKeepAlive**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.WebSocketKeepAlive) class instance as the [**TaskEntryPoint**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder.taskentrypoint) for a KeepAliveTrigger to allow the app to be unsuspended and send keep-alive messages to the server (remote endpoint) periodically. This should be done as part of the background registration app code as well as in the package manifest.

This task entry point of [**Windows.Sockets.WebSocketKeepAlive**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.WebSocketKeepAlive) needs to be specified in two places:

-   When creating KeepAliveTrigger trigger in the source code (see example below).
-   In the app package manifest for the keepalive background task declaration.

The following sample adds a network trigger notification and a keepalive trigger under the &lt;Application&gt; element in an app manifest.

```xml
  <Extensions>
    <Extension Category="windows.backgroundTasks"
         Executable="$targetnametoken$.exe"
         EntryPoint="Background.PushNotifyTask">
      <BackgroundTasks>
        <Task Type="controlChannel" />
      </BackgroundTasks>
    </Extension>
    <Extension Category="windows.backgroundTasks"
         Executable="$targetnametoken$.exe"
         EntryPoint="Windows.Networking.Sockets.WebSocketKeepAlive">
      <BackgroundTasks>
        <Task Type="controlChannel" />
      </BackgroundTasks>
    </Extension>
  </Extensions>
```

An app must be extremely careful when using an **await** statement in the context of a [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) and an asynchronous operation on a [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket), [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket), or [**StreamSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket). A **Task&lt;bool&gt;** object can be used to register a **ControlChannelTrigger** for push notification and WebSocket keep-alives on the **StreamWebSocket** and connect the transport. As part of the registration, the **StreamWebSocket** transport is set as the transport for the **ControlChannelTrigger** and a read is posted. The **Task.Result** will block the current thread until all steps in the task execute and return statements in message body. The task is not resolved until the method returns either true or false. This guarantees that the whole method is executed. The **Task** can contain multiple **await** statements that are protected by the **Task**. This pattern should be used with the **ControlChannelTrigger** object when a **StreamWebSocket** or **MessageWebSocket** is used as the transport. For those operations that may take a long period of time to complete (a typical async read operation, for example), the app should use the raw async pattern discussed previously.

The following sample registers [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) for push notification and WebSocket keep-alives on the [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket).

```csharp
private bool RegisterWithControlChannelTrigger(string serverUri)
{
    // Make sure the objects are created in a system thread
    // Demonstrate the core registration path
    // Wait for the entire operation to complete before returning from this method.
    // The transport setup routine can be triggered by user control, by network state change
    // or by keepalive task
    Task<bool> registerTask = RegisterWithCCTHelper(serverUri);
    return registerTask.Result;
}

async Task<bool> RegisterWithCCTHelper(string serverUri)
{
    bool result = false;
    socket = new StreamWebSocket();

    // Specify the keepalive interval expected by the server for this app
    // in order of minutes.
    const int serverKeepAliveInterval = 30;

    // Specify the channelId string to differentiate this
    // channel instance from any other channel instance.
    // When background task fires, the channel object is provided
    // as context and the channel id can be used to adapt the behavior
    // of the app as required.
    const string channelId = "channelOne";

    // For websockets, the system does the keepalive on behalf of the app
    // But the app still needs to specify this well known keepalive task.
    // This should be done here in the background registration as well
    // as in the package manifest.
    const string WebSocketKeepAliveTask = "Windows.Networking.Sockets.WebSocketKeepAlive";

    // Try creating the controlchanneltrigger if this has not been already
    // created and stored in the property bag.
    ControlChannelTriggerStatus status;

    // Create the ControlChannelTrigger object and request a hardware slot for this app.
    // If the app is not on LockScreen, then the ControlChannelTrigger constructor will
    // fail right away.
    try
    {
        channel = new ControlChannelTrigger(channelId, serverKeepAliveInterval,
                                   ControlChannelTriggerResourceType.RequestHardwareSlot);
    }
    catch (UnauthorizedAccessException exp)
    {
        Diag.DebugPrint("Is the app on lockscreen? " + exp.Message);
        return result;
    }

    Uri serverUriInstance;
    try
    {
        serverUriInstance = new Uri(serverUri);
    }
    catch (Exception exp)
    {
        Diag.DebugPrint("Error creating URI: " + exp.Message);
        return result;
    }

    // Register the apps background task with the trigger for keepalive.
    var keepAliveBuilder = new BackgroundTaskBuilder();
    keepAliveBuilder.Name = "KeepaliveTaskForChannelOne";
    keepAliveBuilder.TaskEntryPoint = WebSocketKeepAliveTask;
    keepAliveBuilder.SetTrigger(channel.KeepAliveTrigger);
    keepAliveBuilder.Register();

    // Register the apps background task with the trigger for push notification task.
    var pushNotifyBuilder = new BackgroundTaskBuilder();
    pushNotifyBuilder.Name = "PushNotificationTaskForChannelOne";
    pushNotifyBuilder.TaskEntryPoint = "Background.PushNotifyTask";
    pushNotifyBuilder.SetTrigger(channel.PushNotificationTrigger);
    pushNotifyBuilder.Register();

    // Tie the transport method to the ControlChannelTrigger object to push enable it.
    // Note that if the transport' s TCP connection is broken at a later point of time,
    // the ControlChannelTrigger object can be reused to plug in a new transport by
    // calling UsingTransport API again.
    try
    {
        channel.UsingTransport(socket);

        // Connect the socket
        //
        // If connect fails or times out it will throw exception.
        // ConnectAsync can also fail if hardware slot was requested
        // but none are available
        await socket.ConnectAsync(serverUriInstance);

        // Call WaitForPushEnabled API to make sure the TCP connection has
        // been established, which will mean that the OS will have allocated
        // any hardware slot for this TCP connection.
        //
        // In this sample, the ControlChannelTrigger object was created by
        // explicitly requesting a hardware slot.
        //
        // On systems that without connected standby, if app requests hardware slot as above,
        // the system will fallback to a software slot automatically.
        //
        // On systems that support connected standby,, if no hardware slot is available, then app
        // can request a software slot by re-creating the ControlChannelTrigger object.
        status = channel.WaitForPushEnabled();
        if (status != ControlChannelTriggerStatus.HardwareSlotAllocated
            && status != ControlChannelTriggerStatus.SoftwareSlotAllocated)
        {
            throw new Exception(string.Format("Neither hardware nor software slot could be allocated. ChannelStatus is {0}", status.ToString()));
        }

        // Store the objects created in the property bag for later use.
        CoreApplication.Properties.Remove(channel.ControlChannelTriggerId);

        var appContext = new AppContext(this, socket, channel, channel.ControlChannelTriggerId);
        ((IDictionary<string, object>)CoreApplication.Properties).Add(channel.ControlChannelTriggerId, appContext);
        result = true;

        // Almost done. Post a read since we are using streamwebsocket
        // to allow push notifications to be received.
        PostSocketRead(MAX_BUFFER_LENGTH);
    }
    catch (Exception exp)
    {
         Diag.DebugPrint("RegisterWithCCTHelper Task failed with: " + exp.Message);

         // Exceptions may be thrown for example if the application has not
         // registered the background task class id for using real time communications
         // broker in the package manifest.
    }
    return result
}
```

For more information on using [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger), see the [ControlChannelTrigger StreamWebSocket sample](https://code.msdn.microsoft.com/windowsapps/ControlChannelTrigger-91f6bed8).

## ControlChannelTrigger with HttpClient
Some special considerations apply when using [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). There are some transport-specific usage patterns and best practices that should be followed when using a [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) with **ControlChannelTrigger**. In addition, these considerations affect the way that requests to receive packets on the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) are handled.

**Note**  [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) using SSL is not currently supported using the network trigger feature and [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger).
 
The following usage patterns and best practices should be followed when using [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger):

-   The app may need to set various properties and headers on the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) or [HttpClientHandler](https://msdn.microsoft.com/library/system.net.http.httpclienthandler(VS.110).aspx) object in the [System.Net.Http](https://msdn.microsoft.com/library/system.net.http(VS.110).aspx) namespace before sending the request to the specific URI.
-   An app may need to make need to an initial request to test and setup the transport properly before creating the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) transport to be used with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). Once the app determines that the transport can be properly setup, an [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) object can be configured as the transport object used with the **ControlChannelTrigger** object. This process is designed prevent some scenarios from breaking the connection established over the transport. Using SSL with a certificate, an app may require a dialog to be displayed for PIN entry or if there are multiple certificates to choose from. Proxy authentication and server authentication may be required. If the proxy or server authentication expires, the connection may be closed. One way an app can deal with these authentication expiration issues is to set a timer. When an HTTP redirect is required, it is not guaranteed that the second connection can be established reliably. An initial test request will ensure that the app can use the most up-to-date redirected URL before using the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) object as the transport with the **ControlChannelTrigger** object.

Unlike other network transports, the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) object cannot be directly passed into the [**UsingTransport**](https://docs.microsoft.com/uwp/api/windows.networking.sockets.controlchanneltrigger.usingtransport) method of the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) object. Instead, an [HttpRequestMessage](https://msdn.microsoft.com/library/system.net.http.httprequestmessage) object must be specially constructed for use with the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) object and the **ControlChannelTrigger**. The [HttpRequestMessage](https://msdn.microsoft.com/library/system.net.http.httprequestmessage) object is created using the [RtcRequestFactory.Create](https://msdn.microsoft.com/library/system.net.http.rtcrequestfactory.create) method. The [HttpRequestMessage](https://msdn.microsoft.com/library/system.net.http.httprequestmessage) object that is created is then passed to **UsingTransport** method.

The following sample shows how to construct an [HttpRequestMessage](https://msdn.microsoft.com/library/system.net.http.httprequestmessage) object for use with the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) object and the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger).

```csharp
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

public HttpRequestMessage httpRequest;
public HttpClient httpClient;
public HttpRequestMessage httpRequest;
public ControlChannelTrigger channel;
public Uri serverUri;

private void SetupHttpRequestAndSendToHttpServer()
{
    try
    {
        // For HTTP based transports that use the RTC broker, whenever we send next request, we will abort the earlier
        // outstanding http request and start new one.
        // For example in case when http server is taking longer to reply, and keep alive trigger is fired in-between
        // then keep alive task will abort outstanding http request and start a new request which should be finished
        // before next keep alive task is triggered.
        if (httpRequest != null)
        {
            httpRequest.Dispose();
        }

        httpRequest = RtcRequestFactory.Create(HttpMethod.Get, serverUri);

        SendHttpRequest();
    }
        catch (Exception e)
    {
        Diag.DebugPrint("Connect failed with: " + e.ToString());
        throw;
    }
}
```

Some special considerations affect the way that requests to send HTTP requests on the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) to initiate receiving a response are handled. In particular, when using a [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) with the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger), your app must use a Task for handling sends instead of the **await** model.

Using [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx), there is no synchronization with the [**IBackgroundTask.Run**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method on the background task for the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) with the return of the receive completion callback. For this reason, the app can only use the blocking HttpResponseMessage technique in the **Run** method and wait until the whole response is received.

Using [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) is noticeably different from the [**StreamSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamSocket), [**MessageWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.MessageWebSocket) or [**StreamWebSocket**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.StreamWebSocket) transports . The [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) receive callback is delivered via a Task to the app since the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) code. This means that the **ControlChannelTrigger** push notification task will fire as soon as the data or error is dispatched to the app. In the sample below, the code stores the responseTask returned by [HttpClient.SendAsync](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) method into global storage that the push notify task will pick up and process inline.

The following sample shows how to handle send requests on the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) when used with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger).

```csharp
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

private void SendHttpRequest()
{
    if (httpRequest == null)
    {
        throw new Exception("HttpRequest object is null");
    }

    // Tie the transport method to the controlchanneltrigger object to push enable it.
    // Note that if the transport' s TCP connection is broken at a later point of time,
    // the controlchanneltrigger object can be reused to plugin a new transport by
    // calling UsingTransport API again.
    channel.UsingTransport(httpRequest);

    // Call the SendAsync function to kick start the TCP connection establishment
    // process for this http request.
    Task<HttpResponseMessage> httpResponseTask = httpClient.SendAsync(httpRequest);

    // Call WaitForPushEnabled API to make sure the TCP connection has been established,
    // which will mean that the OS will have allocated any hardware slot for this TCP connection.
    ControlChannelTriggerStatus status = channel.WaitForPushEnabled();
    Diag.DebugPrint("WaitForPushEnabled() completed with status: " + status);
    if (status != ControlChannelTriggerStatus.HardwareSlotAllocated
        && status != ControlChannelTriggerStatus.SoftwareSlotAllocated)
    {
        throw new Exception("Hardware/Software slot not allocated");
    }

    // The HttpClient receive callback is delivered via a Task to the app.
    // The notification task will fire as soon as the data or error is dispatched
    // Enqueue the responseTask returned by httpClient.sendAsync
    // into a queue that the push notify task will pick up and process inline.
    AppContext.messageQueue.Enqueue(httpResponseTask);
}
```

The following sample shows how to read responses received on the [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) when used with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger).

```csharp
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public string ReadResponse(Task<HttpResponseMessage> httpResponseTask)
{
    string message = null;
    try
    {
        if (httpResponseTask.IsCanceled || httpResponseTask.IsFaulted)
        {
            Diag.DebugPrint("Task is cancelled or has failed");
            return message;
        }
        // We' ll wait until we got the whole response.
        // This is the only supported scenario for HttpClient for ControlChannelTrigger.
        HttpResponseMessage httpResponse = httpResponseTask.Result;
        if (httpResponse == null || httpResponse.Content == null)
        {
            Diag.DebugPrint("Cannot read from httpresponse, as either httpResponse or its content is null. try to reset connection.");
        }
        else
        {
            // This is likely being processed in the context of a background task and so
            // synchronously read the Content' s results inline so that the Toast can be shown.
            // before we exit the Run method.
            message = httpResponse.Content.ReadAsStringAsync().Result;
        }
    }
    catch (Exception exp)
    {
        Diag.DebugPrint("Failed to read from httpresponse with error:  " + exp.ToString());
    }
    return message;
}
```

For more information on using [HttpClient](https://msdn.microsoft.com/library/system.net.http.httpclient(VS.110).aspx) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger), see the [ControlChannelTrigger HttpClient sample](https://code.msdn.microsoft.com/windowsapps/ControlChannelTrigger-HTTP-9d7a6b3d).

## ControlChannelTrigger with IXMLHttpRequest2
Some special considerations apply when using [**IXMLHTTPRequest2**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). There are some transport-specific usage patterns and best practices that should be followed when using a **IXMLHTTPRequest2** with **ControlChannelTrigger**. Using **ControlChannelTrigger** does not affect the way that requests to send or receive HTTP requests on the **IXMLHTTPRequest2** are handled.

Usage patterns and best practices when using [**IXMLHTTPRequest2**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger)

-   An [**IXMLHTTPRequest2**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2) object when used as the transport has a lifetime of only one request/response. When used with the [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger) object, it is convenient to create and set up the **ControlChannelTrigger** object once and then call the [**UsingTransport**](https://docs.microsoft.com/uwp/api/windows.networking.sockets.controlchanneltrigger.usingtransport) method repeatedly, each time associating a new **IXMLHTTPRequest2** object. An app should delete the previous **IXMLHTTPRequest2** object before supplying a new **IXMLHTTPRequest2** object to ensure that the app does not exceed the allocated resource limits.
-   The app may need to call the [**SetProperty**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nf-msxml6-ixmlhttprequest2-setproperty) and [**SetRequestHeader**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nf-msxml6-ixmlhttprequest2-setrequestheader) methods to set up the HTTP transport before calling [**Send**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nf-msxml6-ixmlhttprequest2-send) method.
-   An app may need to make need to an initial [**Send**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nf-msxml6-ixmlhttprequest2-send) request to test and setup the transport properly before creating the transport to be used with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). Once the app determines that the transport is properly setup, the [**IXMLHTTPRequest2**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2) object can be configured as the transport object used with the **ControlChannelTrigger**. This process is designed prevent some scenarios from breaking the connection established over the transport. Using SSL with a certificate, an app may require a dialog to be displayed for PIN entry or if there are multiple certificates to choose from. Proxy authentication and server authentication may be required. If the proxy or server authentication expires, the connection may be closed. One way an app can deal with these authentication expiration issues is to set a timer. When an HTTP redirect is required, it is not guaranteed that the second connection can be established reliably. An initial test request will ensure that the app can use the most up-to-date redirected URL before using the **IXMLHTTPRequest2** object as the transport with the **ControlChannelTrigger** object.

For more information on using [**IXMLHTTPRequest2**](https://docs.microsoft.com/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2) with [**ControlChannelTrigger**](https://docs.microsoft.com/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger), see the [ControlChannelTrigger with IXMLHTTPRequest2 sample](https://code.msdn.microsoft.com/windowsapps/ControlChannelTrigger-HTTP-9d7a6b3d/).

## Important APIs
* [SocketActivityTrigger](/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger)
* [ControlChannelTrigger](/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger)
