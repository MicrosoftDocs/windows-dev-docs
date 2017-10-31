---
author: stevewhims
description: WebSockets provide a mechanism for fast, secure two-way communication between a client and a server over the web using HTTP(S).
title: WebSockets
ms.assetid: EAA9CB3E-6A3A-4C13-9636-CCD3DE46E7E2
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# WebSockets


**Important APIs**

-   [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842)
-   [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923)

WebSockets provide a mechanism for fast, secure two-way communication between a client and a server over the web using HTTP(S).

Under the [WebSocket Protocol](http://tools.ietf.org/html/rfc6455), data is transferred immediately over a full-duplex single socket connection, allowing messages to be sent and received from both endpoints in real time. WebSockets are ideal for use in real-time gaming where instant social network notifications and up-to-date displays of information (game statistics ) need to be secure and use fast data transfer. Universal Windows Platform (UWP) developers can use the [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) and [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) classes to connect with servers that support the Websocket protocol.

| MessageWebSocket                                                         | StreamWebSocket                                                                               |
|--------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------|
| Suitable for typical scenarios where messages are not extremely large.   | Suitable for scenarios in which large files (such as photos or videos) are being transferred. |
| Enables notification that an entire WebSocket message has been received. | Allows sections of a message to be read with each read operation.                             |
| Supports both UTF-8 and binary messages.                                 | Supports only binary messages.                                                                |
| Similar to a UDP or datagram socket.                                     | Similar to a TCP or stream socket.                                                            |

In most cases you'll want to use a secure WebSocket connection to encrypt data sent and received. This will also increase the chances that your connection will succeed, as many proxies will reject unencrypted WebSocket connections. The WebSocket protocol defines the following two URI schemes.

-   ws: - use for unencrypted connections.
-   wss: - use for secure connections that should be encrypted.

To encrypt a WebSocket connection, use the wss: URI scheme, for example, `wss://www.contoso.com/mywebservice`.

## Using MessageWebSocket

The [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) allows sections of a message to be read with each read operation. A **MessageWebSocket** is typically used in scenarios where messages are not extremely large. Both UTF-8 and binary files are supported.

The code in this section creates a new [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842), connects to a WebSocket server, and sends data to the server. Once a successful connection is established, the app waits for the [**MessageWebSocket.MessageReceived**](https://msdn.microsoft.com/library/windows/apps/br241358) event to be triggered, indicating that data was received.

This sample uses the WebSocket.org echo server, a service which simply echoes back to the sender any string sent to it. By using the "wss:" protocol specifier, this sample uses a secure connection to send and receive messages.

> [!div class="tabbedCodeSnippets"]
> ```cpp
> void Game::InitWebSockets()
> {
>     // Create a new web socket
>     m_messageWebSocket = ref new MessageWebSocket();
> 
>     // Set the message type to UTF-8
>     m_messageWebSocket->Control->MessageType = Windows::Networking::Sockets::SocketMessageType::Utf8;
> 
>     // Register callbacks for notifications of interest
>     m_messageWebSocket->MessageReceived += 
>        ref new TypedEventHandler<MessageWebSocket^, MessageWebSocketMessageReceivedEventArgs^>(this, &Game::WebSocketMessageReceived);
>     m_messageWebSocket->Closed += ref new TypedEventHandler<IWebSocket^, WebSocketClosedEventArgs^>(this, &Game::WebSocketClosed);
> 
>     // This test code uses the websocket.org echo service to illustrate sending a string and receiving the echoed string back
>     // Note that wss: makes this an encrypted connection.
>     m_serverUri = ref new Uri("wss://echo.websocket.org");
> 
>     // Establish the connection, and set m_socketConnected on success
>     create_task(m_messageWebSocket->ConnectAsync(m_serverUri)).then([this] (task<void> previousTask)
>     {
>         try
>         {
>             // Try getting all exceptions from the continuation chain above this point.
>             previousTask.get();
> 
>             // websocket connected. update state variable
>             m_socketConnected = true;
>             OutputDebugString(L"Successfully initialized websockets\n");
>         }
>         catch (Platform::COMException^ exception)
>         {
>             // Add code here to handle any exceptions
>             // HandleException(exception);
> 
>         }
>     });
> }
> ```
> ```cs
> MessageWebSocket webSock = new MessageWebSocket();
> 
> //In this case we will be sending/receiving a string so we need to set the MessageType to Utf8.
> webSock.Control.MessageType = SocketMessageType.Utf8;
> 
> //Add the MessageReceived event handler.
> webSock.MessageReceived += WebSock_MessageReceived;
> 
> //Add the Closed event handler.
> webSock.Closed += WebSock_Closed;
> 
> Uri serverUri = new Uri("wss://echo.websocket.org");
> 
> try
> {
>     //Connect to the server.
>     await webSock.ConnectAsync(serverUri);
> 
>     //Send a message to the server.
>     await WebSock_SendMessage(webSock, "Hello, world!");
> }
> catch (Exception ex)
> {
>     //Add code here to handle any exceptions
> }
> ```

Once you have initialized the WebSocket connection, your code must perform the following activities to properly send and receive data.

### Implement a callback for the MessageWebSocket.MessageReceived event

Before establishing a connection and sending data with a WebSocket, your app needs to register an event callback to receive notification when data is received. When the [**MessageWebSocket.MessageReceived**](https://msdn.microsoft.com/library/windows/apps/br241358) event occurs, the registered callback is called and receives data from [**MessageWebSocketMessageReceivedEventArgs**](https://msdn.microsoft.com/library/windows/apps/br226852). This example is written with the assumption that the messages being sent are in UTF-8 format.

The following sample function receives a string from a connected WebSocket server and prints the string to the debugger output window.

> [!div class="tabbedCodeSnippets"]
>```cpp
>void Game::WebSocketMessageReceived(MessageWebSocket^ sender, MessageWebSocketMessageReceivedEventArgs^ args)
>{
>    DataReader^ messageReader = args->GetDataReader();
>    messageReader->UnicodeEncoding = Windows::Storage::Streams::UnicodeEncoding::Utf8;
>
>    String^ readString = messageReader->ReadString(messageReader->UnconsumedBufferLength);
>    // Data has been read and is now available from the readString variable.
>    swprintf(m_debugBuffer, 511, L"WebSocket Message received: %s\n", readString->Data());
>    OutputDebugString(m_debugBuffer);
>}
>```
>```csharp
>//The MessageReceived event handler.
>private void WebSock_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
>{
>    DataReader messageReader = args.GetDataReader();
>    messageReader.UnicodeEncoding = UnicodeEncoding.Utf8;
>    string messageString = messageReader.ReadString(messageReader.UnconsumedBufferLength);
>
>    //Add code here to do something with the string that is received.
>}
>```

###  Implement a callback for the MessageWebSocket.Closed event

Before establishing a connection and sending data with a WebSocket, your app needs to register an event callback to receive notification when the WebSocket is closed by the WebSocket server. When the [**MessageWebSocket.Closed**](https://msdn.microsoft.com/library/windows/apps/hh701364) event occurs, the registered callback is called to indicate the connection was closed by the WebSocket server.

> [!div class="tabbedCodeSnippets"]
>```cpp
>void Game::WebSocketClosed(IWebSocket^ sender, WebSocketClosedEventArgs^ args)
>{
>    // The method may be triggered remotely by the server sending unsolicited close frame or locally by Close()/delete operator.
>    // This method assumes we saved the connected WebSocket to a variable called m_messageWebSocket
>    if (m_messageWebSocket != nullptr)
>    {
>        delete m_messageWebSocket;
>        m_messageWebSocket = nullptr;
>        OutputDebugString(L"Socket was closed\n");
>    }
>    m_socketConnected = false;
> }
>```
>```csharp
>//The Closed event handler
>private void WebSock_Closed(IWebSocket sender, WebSocketClosedEventArgs args)
>{
>    //Add code here to do something when the connection is closed locally or by the server
>}
>```

###  Send a message on a WebSocket

Once a connection is established, the WebSocket client can send data to the server. The [**DataWriter.StoreAsync**](https://msdn.microsoft.com/library/windows/apps/br208171) method returns a parameter that maps to an unsigned int. This changes how we define the task to send the message compared with the task to make the connection.

**Note**   When you create a new DataWriter object using the MessageWebSocket's OutputStream, the DataWriter takes ownership of the OutputStream, and will deallocate the Outputstream when the DataWriter goes out of scope. This causes any subsequent attempts to use the OutputStream to fail with an HRESULT value of 0x80000013. To avoid deallocating the OutputStream, this code calls the DataWriter's DetachStream method, which returns ownership of the stream to the WebSocket object.

The following function sends the given string to a connected WebSocket, and prints a verification message in the debugger output window.

> [!div class="tabbedCodeSnippets"]
>```cpp
>void Game::SendWebSocketMessage(Windows::Networking::Sockets::MessageWebSocket^ sendingSocket, Platform::String^ message)
>{
>    if (m_socketConnected)
>    {
>        // WebSocket is connected, so send a message
>        m_messageWriter = ref new DataWriter(sendingSocket->OutputStream);
>
>        m_messageWriter->WriteString(message);
>
>        // Send the data as one complete message
>        create_task(m_messageWriter->StoreAsync()).then([this] (unsigned int)
>        {
>            // Send Completed
>            m_messageWriter->DetachStream();    // give the stream back to m_messageWebSocket
>            OutputDebugString(L"Sent websocket message\n");
>        })
>            .then([this] (task<void>> previousTask)
>        {
>            try
>            {
>                // Try getting all exceptions from the continuation chain above this point.
>                previousTask.get();
>            }
>            catch (Platform::COMException ^ex)
>            {
>                // Add code to handle the exception
>                // HandleException(exception);
>            }
>        });
>    }
>}
>```
>```csharp
>//Send a message to the server.
>private async Task WebSock_SendMessage(MessageWebSocket webSock, string message)
>{
>    DataWriter messageWriter = new DataWriter(webSock.OutputStream);
>    messageWriter.WriteString(message);
>    await messageWriter.StoreAsync();
>}
>```

## Using advanced controls with WebSockets

[**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) and [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) follow the same model for using advanced controls. Corresponding with each of the above primary classes are related classes to access advanced controls.

[**MessageWebSocketControl**](https://msdn.microsoft.com/library/windows/apps/br226843) provides socket control data on a [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) object.
[**StreamWebSocketControl**](https://msdn.microsoft.com/library/windows/apps/br226924) provides socket control data on a [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) object.
The basic model to use advanced controls is the same for both types of WebSockets. The following discussion uses a [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) as an example, but the same process can be used with a [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842).

1.  Create the [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) object.
2.  Use the [**StreamWebSocket.Control**](https://msdn.microsoft.com/library/windows/apps/br226934) property to retrieve the [**StreamWebSocketControl**](https://msdn.microsoft.com/library/windows/apps/br226924) instance associated with this [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) object.
3.  Get or set properties on the [**StreamWebSocketControl**](https://msdn.microsoft.com/library/windows/apps/br226924) instance to get or set specific advanced controls.

Both [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) and [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) impose requirements on when advanced controls can be set.

-   For all advanced controls on the [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923), the app must always set the property before issuing a Connect operation. Because of this requirement, it is best practice to set all control properties immediately after creating the **StreamWebSocket** object. Do not try to set a control property after the [**StreamWebSocket.ConnectAsync**](https://msdn.microsoft.com/library/windows/apps/br226933) method has been called.
-   For all of the advanced controls on the [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) except the message type, you must set the property before issuing a Connect operation. It is best practice to set all control properties immediately after creating the **MessageWebSocket** object. Except for the message type, do not attempt to change a control property after [**MessageWebSocket.ConnectAsync**](https://msdn.microsoft.com/library/windows/apps/br226859) has been called.

## WebSocket information classes

[**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) and [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) each have a corresponding class that provides additional information about a WebSocket instance.

[**MessageWebSocketInformation**](https://msdn.microsoft.com/library/windows/apps/br226849) provides information about a [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842), and you retrieve an instance of the information class using the [**MessageWebSocket.Information**](https://msdn.microsoft.com/library/windows/apps/br226861) property.

[**StreamWebSocketInformation**](https://msdn.microsoft.com/library/windows/apps/br226929) provides information about a [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923), and you retrieve an instance of the information class using the [**StreamWebSocket.Information**](https://msdn.microsoft.com/library/windows/apps/br226935) property.

Note that all of the properties on both Information classes are read-only, and that you can retrieve current information at any time during the lifetime of a web socket object.

## Handling network exceptions

An error encountered on a [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) or [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) operation is returned as an **HRESULT** value. The [**WebSocketError.GetStatus**](https://msdn.microsoft.com/library/windows/apps/hh701529) method is used to convert a network error from a WebSocket operation to a [**WebErrorStatus**](https://msdn.microsoft.com/library/windows/apps/hh747818) enumeration value. Most of the **WebErrorStatus** enumeration values correspond to an error returned by the native HTTP client operation. Your app can filter on specific **WebErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

For parameter validation errors, an app can also use the **HRESULT** from the exception to learn more detailed information on the error that caused the exception. Possible **HRESULT** values are listed in the *Winerror.h* header file. For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**.

## Setting timeouts on WebSocket operations

The MessageWebSocket and StreamWebSocket classes uses an internal system service to send WebSocket client requests and receive responses from a server. The default timeout value used for a WebSocket connect operation is 60 seconds. If the HTTP server that supports WebSockets is temporarily down or blocked by a network outage and the server doesn't or can't respond to the WebSocket connection request, the internal system service waits the default 60 seconds before it returns an error which causes an exception to be thrown on the WebSocket ConnectAsync method. If the name query for an HTTP server name in the URI returns multiple IP addresses for the name, the internal system service tries up to 5 IP addresses for the site each with a default timeout of 60 seconds before it fails. An app making a WebSocket connection request could wait several minutes trying to connect to multiple IP addresses before an error is returned and an exception is thrown. This behavior could appear to the user as if the app stopped working. The default timeout used for send and receive operations after a WebSocket connection has been established is 30 seconds.

To make your app more responsive and minimize these issues, it can set a shorter timeout on connection requests so that the operation fails due to timeout sooner than from the default settings.

You set timeouts similarly for both StreamWebSockets and MessageWebSockets. The following example shows how to set a timeout on StreamWebSocket, but the process is similar for a MessageWebSocket.

1.  Create a task that completes after a specified delay using a timer.
2.  Create a task for the WebSocket operation with a cancellation\_token\_source to support cancellation.
3.  If the task with the specified timeout delay completes before the WebSocket connect operation, cancel the task for the WebSocket operation.

The following example creates one task that completes after a specified delay, and creates a second task that cancels after a specified delay. These classes can be used with StreamWebSocket and MessageWebSocket when trying to establish a connection to set a specific timeout. An example usage would be calling the StreamWebSocket.ConnectAsync method in a task with a cancellation\_token\_source that supports cancellation. If timeout completes first, then the cancellation\_token\_source is used to cancel the task for the WebSocket connect operation.

```cpp
    #include <agents.h>
    #include <ppl.h>
    #include <ppltasks.h>

    using namespace concurrency;
    using namespace std;

    // Creates a task that completes after the specified delay.
    task<void> complete_after(unsigned int timeout)
    {
        // A task completion event that is set when a timer fires.
        task_completion_event<void> tce;

        // Create a non-repeating timer.
        shared_ptr<timer<int>> fire_once(new timer<int>(timeout, 0, nullptr, false));
        
        // Create a call object that sets the completion event after the timer fires.
        shared_ptr<call<int>> callback(new call<int>([tce](int)
        {
            tce.set();
        }));

        // Connect the timer to the callback and start the timer.
        fire_once->link_target(callback.get());
        fire_once->start();

        // Create a task that completes after the completion event is set.
        task<void> event_set(tce);

        // Create a continuation task that cleans up resources and
        // and return that continuation task.
        return event_set.then([callback, fire_once]()
        {
        });
    }

    // Cancels the provided task after the specifed delay, if the task
    // did not complete.
    template<typename T>
    task<T> cancel_after_timeout(task<T> t, cancellation_token_source cts, unsigned int timeout)
    {
        // Create a task that returns true after the specified task completes.
        task<bool> success_task = t.then([](T)
        {
            return true;
        });
        // Create a task that returns false after the specified timeout.
        task<bool> failure_task = complete_after(timeout).then([]
        {
            return false;
        });

        // Create a continuation task that cancels the overall task  
        // if the timeout task finishes first. 
        return (failure_task || success_task).then([t, cts](bool success)
        {
            if (!success)
            {
                // Set the cancellation token. The task that is passed as the 
                // t parameter should respond to the cancellation and stop 
                // as soon as it can.
                cts.cancel();
            }
 
            // Return the original task.
            return t;
        });
    }
```

